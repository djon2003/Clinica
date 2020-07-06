Friend Class ReportGeneration

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MdiParent = myMainWin

        RapportDisplay.htmlPageURL = emptyHTMLPath
        RapportDisplay.showPage()

        Save.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance.getImage("rapports23.gif"))
        Me.print.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("print16.ico"), New Size(16, 16))

        AlertUser.Checked = PreferencesManager.getUserPreferences()("AlertUserOnRapportGeneration")
    End Sub

    Private myReport As Report
    Private generating As Boolean = False
    Private myRapportTitle As String = ""
    Private winSettings() As String
    Private _DoAutoGenerate As Boolean = True

#Region "Form's events"
    Private Sub rapportGeneration_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If generating Then
            If MessageBox.Show("Voulez-vous arrêter la génération du présent rapport ?", "Rapport en génération", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            End If

            myReport.stopHTMLGeneration()
        End If
    End Sub


    Private Sub rapportGeneration_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub

    Private Sub rapportGeneration_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim setting As String = UsersManager.currentUser.settings.reportGeneration
        If setting <> "" Then winSettings = setting.Split(New Char() {"§"})
        If Me.doAutoGenerate AndAlso PreferencesManager.getUserPreferences()("GenerateAutoRapportOnOpening") = True Then generate_Click(Me, eventArgs.Empty)
    End Sub

    Private Sub rapportGeneration_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        If Not winSettings Is Nothing AndAlso winSettings.Length <> 0 Then
            Dim curUser As User = UsersManager.currentUser
            curUser.settings.reportGeneration = String.Join("§", winSettings)
            curUser.settings.saveData()
        End If
    End Sub
#End Region

    Public Property doAutoGenerate() As Boolean
        Get
            Return _DoAutoGenerate
        End Get
        Set(ByVal value As Boolean)
            _DoAutoGenerate = False
        End Set
    End Property

    Delegate Sub setboolCallback(ByVal bool As Boolean)
    Delegate Sub setGenerationCallback(ByVal rapportTitle As String, ByVal tpFilter As FilteringComposite, ByVal bodyTable As DataTable)

    Private Shared Sub startGeneration(ByVal rapportTitle As String, ByVal tpFilter As FilteringComposite, ByVal bodyTable As DataTable)
        Dim myRapportGeneration As ReportGeneration = openUniqueWindow(New ReportGeneration, , , True)
        myRapportGeneration.doAutoGenerate = False
        myRapportGeneration.Show()

        Dim customVars As New Hashtable
        customVars.Add("###CLINICAPATH###", appPath & bar(appPath))
        customVars.Add("###TAUXAUTONOMIEMAX###", PreferencesManager.getGeneralPreferences()("NbEvalTo100TauxAutonomie"))
        customVars.Add("###TAX1NAME###", PreferencesManager.getGeneralPreferences()("tax1Name"))
        customVars.Add("###TAX2NAME###", PreferencesManager.getGeneralPreferences()("tax2Name"))
        Dim newRapport As Report = ReportsManager.getInstance.createReport(rapportTitle, tpFilter, customVars, bodyTable)
        If newRapport Is Nothing Then Exit Sub
        myRapportGeneration.setRapport(newRapport)

        newRapport.startHTMLGeneration()
    End Sub

    Public Shared Sub startRapportGeneration(ByVal rapportTitle As String, ByVal tpFilter As FilteringComposite, Optional ByVal bodyTable As DataTable = Nothing)
        If myMainWin.InvokeRequired Then
            Dim d As New setGenerationCallback(AddressOf startGeneration)
            myMainWin.BeginInvoke(d, New Object() {rapportTitle, tpFilter, bodyTable})
        Else
            startGeneration(rapportTitle, tpFilter, bodyTable)
        End If
    End Sub

    Private Sub setRapport(ByVal curRapport As Report)
        myReport = curRapport
        If Not myReport Is Nothing Then
            myRapportTitle = myReport.reportTitle
            RapportDisplay.sethtml("<h1>Génération du rapport...</h1>")
            AddHandler myReport.htmlGenerated, AddressOf rapportGenerated
            lockItems(True)
            generating = True
        Else
            setItemsEnabelity(False)
        End If
    End Sub

    Private Sub setGenerateEnabelity(ByVal bool As Boolean)
        Generate.Enabled = bool
    End Sub

    Private Sub setItemsEnabelity(ByVal bool As Boolean)
        Save.Enabled = bool
        print.Enabled = bool
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal onlyGenBtn As Boolean = False)
        If Me.InvokeRequired Then
            Dim d As New setboolCallback(AddressOf setGenerateEnabelity)
            Me.BeginInvoke(d, New Object() {Not trueFalse})
            If onlyGenBtn = False Then
                d = New setboolCallback(AddressOf setItemsEnabelity)
                Me.BeginInvoke(d, New Object() {Not trueFalse})
            End If
        Else
            setGenerateEnabelity(Not trueFalse)
            If onlyGenBtn = False Then setItemsEnabelity(Not trueFalse)
        End If
    End Sub

    Private Sub generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Generate.Click
        'Droit & Accès
        If currentDroitAcces(70) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de générer un rapport." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim Hauteur, largeur As Integer
        If Not winSettings Is Nothing AndAlso winSettings.Length <> 0 Then
            If winSettings.Length >= 2 Then
                Hauteur = winSettings(0)
                largeur = winSettings(1)
            End If
        End If
        myRapportTitle = ReportsManager.getInstance.selectReportType(Hauteur, largeur)
        If Not winSettings Is Nothing AndAlso winSettings.Length <> 0 Then
            If winSettings.Length < 2 Then ReDim winSettings(1)
        Else
            ReDim winSettings(1)
        End If
        winSettings(0) = Hauteur
        winSettings(1) = largeur

        If myRapportTitle = "ERROR" Then Exit Sub

        Dim customVars As New Hashtable
        customVars.Add("###CLINICAPATH###", appPath & bar(appPath))
        customVars.Add("###TAUXAUTONOMIEMAX###", PreferencesManager.getGeneralPreferences()("NbEvalTo100TauxAutonomie"))
        customVars.Add("###TAX1NAME###", PreferencesManager.getGeneralPreferences()("tax1Name"))
        customVars.Add("###TAX2NAME###", PreferencesManager.getGeneralPreferences()("tax2Name"))
        myReport = ReportsManager.getInstance.createReport(myRapportTitle, , customVars)
        If Not myReport Is Nothing Then
            RapportDisplay.sethtml("<h1>Génération du rapport...</h1>")
            AddHandler myReport.htmlGenerated, AddressOf rapportGenerated
            lockItems(True)
            generating = True
            myReport.startHTMLGeneration()
        End If
    End Sub

    Private Sub save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        saveToDB()
    End Sub

    Private Sub saveToDB()
        'Droit & Accès
        If currentDroitAcces(71) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'enregistrer un rapport." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myInputBoxPlus As New InputBoxPlus(True, "Users\Lists\" & ConnectionsManager.currentUser & "\rapporttitle.lst")
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = "\§/§:§*§?§""§<§>§|§%"
        Dim myNom As String = myInputBoxPlus("Veuillez entrer le nom que vous voulez donner au rapport", "Nom du rapport")
        If myNom = "" Then Exit Sub

        Dim mySelectDBCat As New SelectDBCat
        Dim myPath As String = mySelectDBCat("")
        If myPath = "" Then Exit Sub

        Dim myMotsCles() As String = {"Rapport", myReport.reportTitle}
        Dim myDescription() As String = {""}

        myReport.saveToDB(myNom, myPath, myMotsCles, myDescription)

        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\rapporttitle.lst", myNom, , True, 15, False)
    End Sub

    Private Sub saveToClient()
        'Droit & Accès
        If currentDroitAcces(71) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'enregistrer un rapport." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myRecherche As New clientSearch
        myRecherche.from = Me
        myRecherche.Visible = False
        myRecherche.MdiParent = Nothing
        Dim nbFoundClient As Integer = foundClient.Length
        myRecherche.ShowDialog()
        If nbFoundClient < foundClient.Length Then
            myReport.saveToClient(foundClient(foundClient.Length - 1).noClient)
        End If
    End Sub

    Private Sub saveToKP()
        'Droit & Accès
        If currentDroitAcces(71) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'enregistrer un rapport." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myKeypeople As New keypeopleSearch
        myKeypeople.MdiParent = Nothing
        myKeypeople.Visible = False
        Dim kpChosen As KPSelectorReturn = myKeypeople.showDialog()
        If kpChosen.noKP <> 0 Then
            myReport.saveToKP(kpChosen.noKP)
        End If
    End Sub

    Private Sub rapportGenerated(ByVal html As String, ByVal ishtmlGenerated As Boolean)
        Threading.Thread.Sleep(100) 'This line is really important, otherwise, sometimes, the HTML doesn't show up
        Console.WriteLine("RapportGenerated")
        RapportDisplay.setHtml(html)

        If ishtmlGenerated Then
            If AlertUser.Checked Then AlertsManager.getInstance.addAlert("Le rapport " & myRapportTitle & " a été généré le " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.YYYYMMDD) & " à " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.ShortTime), ConnectionsManager.currentUser, AlertsManager.AType.OpenRapportGenerator, , Date.Now.AddMinutes(15), , , True)
            lockItems(False)
        Else
            lockItems(False, True)
        End If
        generating = False
    End Sub

    Private Sub print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print.Click
        PrintingHelper.printHtml(RapportDisplay, myReport.reportTitle)
    End Sub

    Private Sub dansLaBanqueDeDonnéesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DansLaBanqueDeDonnéesToolStripMenuItem.Click
        saveToDB()
    End Sub

    Private Sub dansLesCommunicationsDunClientToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DansLesCommunicationsDunClientToolStripMenuItem.Click
        saveToClient()
    End Sub

    Private Sub dansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem.Click
        saveToKP()
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property
End Class
