
Public Class WebTextControl
    Inherits Base.Windows.Forms.WebControl

    Public Const PROTOCOL_CLINICA As String = "clinica://"

    Private _FoundItemFromDB, _Ancre As String
    Private _AncreActif As Boolean = False

    Public Event addedLink()
    Public Event addedImage()

#Region "Propriétés"
    Public Property ancre() As String
        Get
            Return _Ancre
        End Get
        Set(ByVal value As String)
            _Ancre = value
            MyBase.anchorChanged()
        End Set
    End Property

    Public Property ancreActif() As Boolean
        Get
            Return _AncreActif
        End Get
        Set(ByVal value As Boolean)
            _AncreActif = value
            MyBase.anchorChanged()
        End Set
    End Property
#End Region

    Private Sub executeLink(ByVal url As String, ByRef cancel As Boolean)
        If url.StartsWith("javascript:") Then Exit Sub
        If url.StartsWith("#") Then Exit Sub
        If url.StartsWith("about:") Then Exit Sub

        If url.StartsWith(PROTOCOL_CLINICA) Then
            Dim myURL As String = url.Substring(10)
            myURL = Web.HttpUtility.UrlDecode(myURL)
            If myURL.EndsWith("/") Then myURL = myURL.Substring(0, myURL.Length - 1)
            Dim surl() As String = myURL.Split(New Char() {"|"})

            Dim myFullPath As String = PROTOCOL_CLINICA & myURL
            Dim options As IOpenableOptions
            cancel = True
            If surl(0) = "CLIENT" Then
                openAccount(surl(1))
                Exit Sub
            ElseIf surl(0) = "KP" Then
                openAccount(surl(1), CompteType.KP)
                Exit Sub
            ElseIf surl(0) = "CONTACT" Then
                Dim curContactFolder As ContactFolder = ContactsManager.getInstance.getContactFolder(Integer.Parse(surl(1)))
                openContact(curContactFolder, surl(2))
                Exit Sub
            ElseIf surl(0) = "USER" Then
                openAccount(surl(1), CompteType.User)
                Exit Sub
            ElseIf surl(0) = "DB" Then
                options = New InternalType_InternalDBItem.InternalType_InternalDBItem_Options(Me.ParentForm.MdiParent Is Nothing)
            ElseIf surl(0) = "COMM" Then
                Dim noClient As Integer = surl(1)

                If surl(2) = "DB" Then
                    myFullPath = PROTOCOL_CLINICA & surl(2) & "|" & surl(3)
                    options = New InternalType_InternalDBItem.InternalType_InternalDBItem_Options(Me.ParentForm.MdiParent Is Nothing)
                Else
                    myFullPath = appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & surl(3)
                End If
            Else
                myFullPath = appPath & bar(appPath) & surl(1)
            End If

            TypesFilesOpener.getInstance.open(myFullPath, options)

            Exit Sub
        End If

        If url.StartsWith("mailto:") Then
            url = url.Substring(7)
            If url.StartsWith("//") Then url = url.Substring(2)
            url = System.Web.HttpUtility.UrlDecode(url)
            sendemailTo(url)
            cancel = True
            Exit Sub
        End If

        Dim testingURL As String = url.Replace("file:///", "").Replace("\", "/")
        Dim testingPage As String = myPage.Replace("\", "/")
        Dim testingEditor As String = Me.editorURL.Replace("\", "/")
        If testingURL <> testingPage AndAlso (Me.editorURL = "" OrElse (url.StartsWith(testingEditor) = False And url.IndexOf("#" & testingEditor) < 0)) Then
            cancel = True
            'Page autre que celle de l'éditeur de texte
            'Ouverture du browser interne

            TypesFilesOpener.getInstance.open(url, Nothing)
        End If
    End Sub

    Private lastAncreFound As Integer = -1
    Private lastURL As String = ""
    Private showedPage As Boolean = False

    Public Shadows Sub showPage(Optional ByVal html As String = "")
        showedPage = True
        If html = "" And Me.htmlPageURL <> "" And IO.File.Exists(Me.htmlPageURL) Then html = IO.File.ReadAllText(Me.htmlPageURL)
        MyBase.showPage(html)
    End Sub

    Private Sub webTextControl_EditorLinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles Me.editorLinkClicked
        executeLink(e.LinkText, False)
    End Sub

    Private Sub webTextControl_NavigateComplete(ByVal url As String) Handles Me.navigateComplete
        If Editing = False And showedPage Then
            showedPage = False
            lastURL = url
            MyBase.insertHtml("<style>P {margin-top:0px;margin-bottom:0px;} </style>")
        End If
    End Sub

    Protected Overrides Function isAnchorActif() As Boolean
        Return ancreActif AndAlso ancre <> ""
    End Function

    Protected Overrides Sub onTextPreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
        MyBase.onTextPreviewKeyDown(sender, e)
        If isAnchorActif() AndAlso Editing = True AndAlso e.Control = True AndAlso e.Shift = True AndAlso (e.KeyCode = Keys.ControlKey Or e.KeyCode = Keys.ShiftKey) Then
            goNextAnchor()
        End If
    End Sub

    Private Sub goNextAnchor()
        'Gestion de l'ancre avec la combinaison des touches CTRL et SHIFT
        Dim selectAnchor As Boolean = PreferencesManager.getGeneralPreferences()("AutoSelectAncre")

        'TODO : WEBTEXTPAGE-POS - Adjust
        Dim curPos As Integer = getPos()
        Dim isOnLastAnchor As Boolean = curPos = lastAncreFound
        If isOnLastAnchor Then
            curPos += 1 + If(selectAnchor, ancre.Length, 0)
        End If

        MyBase.searchAndSelect(ancre, curPos, Not selectAnchor)
        lastAncreFound = getPos()

        'If PreferencesManager.getGeneralPreferences()("AutoSelectAncre") = False Then MyBase.setPos(lastAncreFound + ancre.Length)
        MyBase.focus()
    End Sub

    Protected Overrides Sub onAddLink(ByRef handled As Boolean)
        MyBase.onAddLink(handled)
        If handled Then Exit Sub

        Dim myChoice As Byte

        'Droit & Accès
        If currentDroitAcces(53) = False And currentDroitAcces(6) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'ajouter de liens." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        ElseIf currentDroitAcces(53) = False Then
            myChoice = 1 'Force l'utilisateur a utilisé les liens internes
        Else
            Dim myMsgBox As New MsgBox1
            myChoice = myMsgBox("Voulez ajouter un lien depuis la banque de données ou externe ?", "Ajout d'un lien", 2, "Lien interne", "Lien externe")
            If myChoice = 0 Then Exit Sub
        End If

        Dim myURL As String = ""
        Dim myURLEncoded As String = ""
        Dim myTitre As String = ""

        If myChoice = 1 Then myURLEncoded = InternalDBManager.getInstance.getURLFromDB(myTitre)

        'Demander l'adresse directement du lien
        If myChoice = 2 Then
            myURL = getURLFromInput()
            If myURL <> "" Then myURLEncoded = "http://" & Web.HttpUtility.UrlEncode(myURL.Substring(7))
        End If

        addLink(myURLEncoded, myURL, myTitre)
    End Sub

    Protected Overrides Sub onAddImage(ByRef handled As Boolean)
        MyBase.onAddImage(handled)
        If handled Then Exit Sub

        'Droit & Accès
        If currentDroitAcces(6) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'ajouter des images." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myURL As String = ""
        Dim myURLEncoded As String = ""

        Dim curDBItem As InternalDBItem = InternalDBManager.getInstance.getImageFromDB()
        If curDBItem Is Nothing Then Exit Sub

        myURLEncoded = "file:///" & (appPath & bar(appPath)).Replace("\", "/") & "DB/" & curDBItem.dbItemFile

        addImage(myURLEncoded, myURL, "DB:\" & curDBItem.uniqueNo & "\" & curDBItem.getDBFolder.toString() & "\" & curDBItem.dbItem)
    End Sub

    Protected Overrides Sub onInsertDate(ByVal fieldId As String, ByVal selectedDate As String, ByRef handled As Boolean)
        MyBase.onInsertDate(fieldId, selectedDate, handled)

        Dim myDateChoice As New DateChoice()
        Dim curSelectedDate As Date = Date.Today
        If Date.TryParse(selectedDate, curSelectedDate) = False Then curSelectedDate = Date.Today
        Dim myDate As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Today.Year + 50, , True, , , , True, , , , , curSelectedDate)
        If myDate.Count = 0 Then Exit Sub

        callEditionJavaScript("setFieldValue('" & fieldId & "',  '" & DateFormat.getTextDate(myDate(0)) & "');")
    End Sub

    Protected Overrides Sub onBeforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean)
        executeLink(url, cancel)
        MyBase.onBeforeNavigate(url, flags, targetFrameName, postData, headers, cancel)
    End Sub

    Public Function getURLFromInput(Optional ByVal returnEncoded As Boolean = False) As String
        'Lien externe
        Dim myInputBoxPlus As New InputBoxPlus(True, "Users\Lists\" & ConnectionsManager.currentUser & "\createdlink.lst")
        Dim myURL As String = myInputBoxPlus("Veuillez entrer l'adresse du lien", "Ajout d'un lien externe", "http://")
        If myURL <> "http://" And myURL <> "" Then
            If returnEncoded Then
                If myURL.StartsWith("http://") Then Return "http://" & Web.HttpUtility.UrlEncode(myURL.Substring(7))

                Return "http://" & Web.HttpUtility.UrlEncode(myURL)
            Else
                If myURL.StartsWith("http://") Then Return myURL

                Return "http://" & myURL
            End If
        End If

        Return ""
    End Function

    Public Sub addLink(ByVal urlEncoded As String, Optional ByVal url As String = "", Optional ByVal linkTitle As String = "")
        If urlEncoded = "" Then Exit Sub

        'Demande du titre du lien
        Dim myInputBoxPlus As New InputBoxPlus(True, "Users\Lists\" & ConnectionsManager.currentUser & "\createdlinktitle.lst")
        Dim myTitre As String = myInputBoxPlus("Veuillez entrer le titre du lien", "Ajout d'un lien - Titre", linkTitle)
        If myTitre = "" Then Exit Sub

        'Ajout du lien dans la page
        insertHtml("<a href=""" & urlEncoded & """>" & myTitre & "</a>")

        'Enregistrement du titre et du lien
        If url <> "" Then addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\createdlink.lst", url, , True, 20, False)
        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\createdlinktitle.lst", myTitre, , True, 20, False)

        RaiseEvent addedLink()
    End Sub

    Public Sub addImage(ByVal urlEncoded As String, Optional ByVal url As String = "", Optional ByVal imageDBPath As String = "")
        If urlEncoded = "" Then Exit Sub
        imageDBPath = IIf(imageDBPath <> "", "imageDBPath=""" & imageDBPath & """ ", "")

        'Ajout de l'image
        insertHtml("<a href=""" & PROTOCOL_CLINICA & Web.HttpUtility.UrlEncode("DB|" & InternalDBManager.getInstance.foundItemFromDB) & """><img " & imageDBPath & "src=""" & urlEncoded & """ border=0></a>")

        RaiseEvent addedImage()
    End Sub

    Private Sub webTextControl_RequestNextAnchor() Handles Me.requestNextAnchor
        goNextAnchor()
    End Sub
End Class
