Public Class FolderInfos
    Inherits UserControl

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ctlFrequence.Items.AddRange(Accounts.Clients.Folders.ClientFolder.frequencies)
        Me.ctlDateReference.BackColor = SystemColors.Control
        Me.ctlDateAccident.BackColor = SystemColors.Control
        Me.ctlDateRechute.BackColor = SystemColors.Control

        With DrawingManager.getInstance
            Me.selectmedecin.Image = .getImage("selection16.gif")
            Me.selectcode.Image = .getImage("selection16.gif")
            Me.selectTRP.Image = .getImage("selection16.gif")
        End With
    End Sub

    Private _NoFolder As Integer = 0
    Private _NoClient As Integer = 0
    Private dossierModified As Boolean = False
    Private isLocked As Boolean = False
    Private curCode As Accounts.Clients.Folders.Codifications.FolderCode

#Region "Propriétés"
    Public Property isFolderModified() As Boolean
        Get
            Return dossierModified
        End Get
        Set(ByVal value As Boolean)
            dossierModified = value
        End Set
    End Property

    Public Property noClient() As Integer
        Get
            Return _NoClient
        End Get
        Set(ByVal value As Integer)
            _NoClient = value
        End Set
    End Property

    Public Property noFolder() As Integer
        Get
            Return _NoFolder
        End Get
        Set(ByVal value As Integer)
            _NoFolder = value
        End Set
    End Property

    Public Property dateAppel() As String
        Get
            Return ctlDateAppel.Text
        End Get
        Set(ByVal value As String)
            ctlDateAppel.Text = value
        End Set
    End Property

    Public Property nbPresences() As String
        Get
            Return ctlNbPresences.Text
        End Get
        Set(ByVal value As String)
            ctlNbPresences.Text = value
        End Set
    End Property

    Public Property nbAbsences() As String
        Get
            Return ctlNbAbsences.Text
        End Get
        Set(ByVal value As String)
            ctlNbAbsences.Text = value
        End Set
    End Property

    Public Property dateEval() As String
        Get
            Return ctlDateEval.Text
        End Get
        Set(ByVal value As String)
            ctlDateEval.Text = value
        End Set
    End Property

    Public Property dateDebut() As String
        Get
            Return ctlDateDebut.Text
        End Get
        Set(ByVal value As String)
            ctlDateDebut.Text = value
        End Set
    End Property

    Public Property dateFin() As String
        Get
            Return ctlDateFin.Text
        End Get
        Set(ByVal value As String)
            ctlDateFin.Text = value
        End Set
    End Property

    Public Property dateAccident() As String
        Get
            Return ctlDateAccident.Text
        End Get
        Set(ByVal value As String)
            ctlDateAccident.Text = value
        End Set
    End Property

    Public Property dateRechute() As String
        Get
            Return ctlDateRechute.Text
        End Get
        Set(ByVal value As String)
            ctlDateRechute.Text = value
        End Set
    End Property

    Public Property dateReference() As String
        Get
            Return ctlDateReference.Text
        End Get
        Set(ByVal value As String)
            ctlDateReference.Text = value
        End Set
    End Property

    Public Property dateReferenceReception() As String
        Get
            Return ctlReceptionReferenceDate.Text
        End Get
        Set(ByVal value As String)
            ctlReceptionReferenceDate.Text = value
        End Set
    End Property

    Public Property noRef() As String
        Get
            Return ctlnoref.Text
        End Get
        Set(ByVal value As String)
            ctlnoref.Text = value
        End Set
    End Property

    Public Property noRefMedecin() As String
        Get
            Return ctlNoRefMedecin.Text
        End Get
        Set(ByVal value As String)
            ctlNoRefMedecin.Text = value
        End Set
    End Property

    Public Property diagnostic() As String
        Get
            Return ctlDiagnostic.Text
        End Get
        Set(ByVal value As String)
            ctlDiagnostic.Text = value
        End Set
    End Property

    Public Property trpTraitant() As String
        Get
            Return ctltherapeute.Text
        End Get
        Set(ByVal value As String)
            ctltherapeute.Text = value

            Dim noTRP As Integer = User.extractNo(value)
            loadServices(noTRP)
        End Set
    End Property

    Public Property siteLesion() As String
        Get
            Return ctlregion_Renamed.Text
        End Get
        Set(ByVal value As String)
            ctlregion_Renamed.Text = value
        End Set
    End Property

    Private currentNoCodeUser As Integer

    Public Property codeDossier(ByVal noCodeUser As Integer) As Accounts.Clients.Folders.Codifications.FolderCode
        Get
            Return curCode
        End Get
        Set(ByVal value As Accounts.Clients.Folders.Codifications.FolderCode)
            curCode = value
            If value Is Nothing Then Exit Property

            currentNoCodeUser = noCodeUser
            Dim trp As User = UsersManager.getInstance.getUser(noCodeUser)
            If trp Is Nothing Then trp = UserDefault.getInstance()
            ctlcodedossier.Text = trp.toString & ":" & curCode.name
            ctlcodedossier.SelectionStart = ctlcodedossier.TextLength
        End Set
    End Property

    Public Property trpDemande() As Integer
        Get
            If ctlTRPdemande.SelectedIndex <= 0 Then Return 0

            Dim sNoTRP() As String = ctlTRPdemande.Text.Split(New Char() {"("})

            Return sNoTRP(1).Substring(0, sNoTRP(1).Length - 1)
        End Get
        Set(ByVal value As Integer)
            If ctlTRPdemande.Items.Count = 0 Then Exit Property

            If Me.DesignMode = False Then
                If value = 0 Then
                    ctlTRPdemande.SelectedIndex = 0
                Else
                    ctlTRPdemande.SelectedIndex = ctlTRPdemande.FindStringExact(UsersManager.getInstance.getUser(value).toString)
                End If
            End If

            If ctlTRPdemande.SelectedIndex < 0 Then ctlTRPdemande.SelectedIndex = 0
        End Set
    End Property

    Public Property trpToTransfer() As Integer
        Get
            If ctlTRPToTransfer.SelectedIndex <= 0 Then Return 0

            Dim sNoTRP() As String = ctlTRPToTransfer.Text.Split(New Char() {"("})

            Return sNoTRP(1).Substring(0, sNoTRP(1).Length - 1)
        End Get
        Set(ByVal value As Integer)
            If Me.DesignMode = False Then
                If value = 0 Then
                    ctlTRPToTransfer.SelectedIndex = 0
                Else
                    ctlTRPToTransfer.SelectedIndex = ctlTRPToTransfer.FindStringExact(UsersManager.getInstance.getUser(value).toString)
                End If
            End If

            If ctlTRPToTransfer.SelectedIndex < 0 And ctlTRPToTransfer.Items.Count <> 0 Then ctlTRPToTransfer.SelectedIndex = 0
        End Set
    End Property

    Public Property frequence() As Integer
        Get
            Return ctlFrequence.SelectedIndex
        End Get
        Set(ByVal value As Integer)
            ctlFrequence.SelectedIndex = value
        End Set
    End Property

    Public Property duree() As Integer
        Get
            Return ctlDuree.SelectedIndex
        End Get
        Set(ByVal value As Integer)
            ctlDuree.SelectedIndex = value
        End Set
    End Property

    Public Property medecin() As String
        Get
            Return ctlmedecin.Text
        End Get
        Set(ByVal value As String)
            ctlmedecin.Text = value
        End Set
    End Property

    Public Property service() As String
        Get
            Return ctlService.Text
        End Get
        Set(ByVal value As String)
            ctlService.SelectedIndex = ctlService.FindStringExact(value)
        End Set
    End Property

    Public Property trpNoPermis() As String
        Get
            Return ctlNoPermis.Text
        End Get
        Set(ByVal value As String)
            ctlNoPermis.Text = value
        End Set
    End Property

    Public Property remarques() As String
        Get
            Return ctlRemarques.Text
        End Get
        Set(ByVal value As String)
            ctlRemarques.Text = value
        End Set
    End Property
#End Region

    Public Sub resetFields()
        ctlTRPdemande.SelectedIndex = 0
        ctlTRPToTransfer.SelectedIndex = 0
        ctlDateAccident.Text = ""
        ctlcodedossier.Text = ""
        ctlDateAppel.Text = ""
        ctlDateDebut.Text = ""
        ctlDateEval.Text = ""
        ctlDateFin.Text = ""
        ctlDateRechute.Text = ""
        ctlDateReference.Text = ""
        ctlReceptionReferenceDate.Text = ""
        ctlDiagnostic.Text = ""
        ctlDuree.SelectedIndex = 0
        ctlFrequence.SelectedIndex = 0
        ctlmedecin.Text = ""
        ctlNbAbsences.Text = "0"
        ctlNbPresences.Text = "0"
        ctlNoPermis.Text = ""
        ctlNoRefMedecin.Text = ""
        ctlregion_Renamed.Text = ""
        ctlRemarques.Text = ""
        If ctlService.Items.Count > 0 Then ctlService.SelectedIndex = 0
        ctltherapeute.Text = ""
        ctlTRPdemande.SelectedIndex = 0
        ctlTRPToTransfer.SelectedIndex = 0
    End Sub

    Public Sub lockFolderInfo(ByVal trueFalse As Boolean)
        isLocked = trueFalse
        Dim opposite As Boolean = Not trueFalse

        If trueFalse Then
            ctlDateAccident.BackColor = SystemColors.ControlLight
            ctlDateRechute.BackColor = SystemColors.ControlLight
            ctlDateReference.BackColor = SystemColors.ControlLight
            ctlReceptionReferenceDate.BackColor = SystemColors.ControlLight
        Else
            ctlDateAccident.BackColor = Color.White
            ctlDateRechute.BackColor = Color.White
            ctlDateReference.BackColor = Color.White
            ctlReceptionReferenceDate.BackColor = Color.White
        End If

        selectmedecin.Enabled = opposite
        selectcode.Enabled = opposite
        selectTRP.Enabled = opposite
        ctlDiagnostic.ReadOnly = trueFalse
        ctlregion_Renamed.ReadOnly = trueFalse
        ctlnoref.ReadOnly = trueFalse
        ctlDuree.ReadOnly = trueFalse
        ctlFrequence.ReadOnly = trueFalse
        ctlRemarques.ReadOnly = trueFalse
        ctlService.ReadOnly = trueFalse
        ctlTRPdemande.ReadOnly = trueFalse
        ctlTRPToTransfer.ReadOnly = trueFalse
    End Sub

    Public Sub lockCodeSelection(ByVal trueFalse As Boolean)
        selectcode.Enabled = Not trueFalse
    End Sub

    Private Sub menuSelectMedecin_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.Visible = False
        myKeyPeople.selected = True
        myKeyPeople.specificCat = PreferencesManager.getGeneralPreferences()("MedecinCategorie")
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            Dim noRef() As String = DBLinker.getInstance.readOneDBField("KeyPeople", "NoRef", "WHERE NoKP=" & kpChosen.noKP)
            'TODO: These two lines are bad because refering to parent form, the form shall refer to properties of this control instead
            CType(Me.ParentForm, viewmodifclients).curDoctorNo = kpChosen.noKP
            CType(Me.ParentForm, viewmodifclients).setFolderModified = True
            medecin = kpChosen.kpFullName
            noRefMedecin = noRef(0)
        End If
    End Sub

    Private Sub menuRemoveMedecin_Click(ByVal sender As Object, ByVal e As EventArgs)
        CType(Me.FindForm, viewmodifclients).curDoctorNo = ""
        medecin = "Aucun médecin sélectionné"
        noRefMedecin = ""
        dossierModified = True
    End Sub


    Private Sub selectmedecin_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectmedecin.Click
        showSelectMenu(New EventHandler(AddressOf menuSelectMedecin_Click), New EventHandler(AddressOf menuRemoveMedecin_Click), Nothing)
    End Sub

    Private Sub selectcode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectcode.Click
        Dim noTRP As Integer = User.extractNo(ctltherapeute.Text)
        Dim selectedCode As Accounts.Clients.Folders.Codifications.FolderCode = codifications.chooseCode(noTRP, Date.Today)
        If selectedCode IsNot Nothing Then Me.codeDossier(noTRP) = selectedCode
    End Sub

    Private Sub selectTRP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectTRP.Click
        Dim selectedTRP As String = ""
        If ctltherapeute.Text.IndexOf("(") > 0 Then selectedTRP = ctltherapeute.Text
        If ctlTRPToTransfer.SelectedIndex > 0 And ctltherapeute.Text <> ctlTRPToTransfer.Text Then selectedTRP = ctlTRPToTransfer.Text
        Dim myTRP As User = UsersManager.getInstance.chooseUser(, , , selectedTRP)

        If myTRP IsNot Nothing AndAlso myTRP.toString() <> ctltherapeute.Text Then
            ctltherapeute.Text = myTRP.toString()
            ctlNoPermis.Text = myTRP.noPermit

            loadServices(myTRP.noUser)
        End If
    End Sub

    Private Sub menuSelectDateAcc_Click(ByVal sender As Object, ByVal e As EventArgs)
        dateDossier_Click(ctlDateAccident, MouseEventArgs.Empty)
    End Sub

    Private Sub menuRemoveDateAcc_Click(ByVal sender As Object, ByVal e As EventArgs)
        ctlDateAccident.Text = ""
        dossierModified = True
    End Sub

    Private Sub menuCopyDateAcc_Click(ByVal sender As Object, ByVal e As EventArgs)
        Clipboard.SetText(ctlDateAccident.Text, TextDataFormat.Text)
    End Sub

    Private Sub ctlDateAccident_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlDateAccident.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right And ctlDateAccident.Cursor Is Cursors.Hand Then
            showSelectMenu(New EventHandler(AddressOf menuSelectDateAcc_Click), New EventHandler(AddressOf menuRemoveDateAcc_Click), New EventHandler(AddressOf menuCopyDateAcc_Click))
        End If
    End Sub

    Private Sub showSelectMenu(ByVal selectEventHandler As EventHandler, ByVal removeEventHandler As EventHandler, ByVal copyEventHandler As EventHandler)
        Dim selectMenu As New ContextMenuStrip()
        selectMenu.ShowCheckMargin = False
        selectMenu.ShowImageMargin = False
        selectMenu.ShowItemToolTips = False
        selectMenu.Items.Add("Sélectionner", Nothing, selectEventHandler)
        If copyEventHandler IsNot Nothing Then selectMenu.Items.Add("Copier", Nothing, copyEventHandler)
        selectMenu.Items.Add("-")
        selectMenu.Items.Add("Aucun(e)", Nothing, removeEventHandler)
        selectMenu.Show(Control.MousePosition)
    End Sub

    Private Sub menuSelectDateRec_Click(ByVal sender As Object, ByVal e As EventArgs)
        dateDossier_Click(ctlDateRechute, MouseEventArgs.Empty)
    End Sub

    Private Sub menuRemoveDateRec_Click(ByVal sender As Object, ByVal e As EventArgs)
        ctlDateRechute.Text = ""
        dossierModified = True
    End Sub

    Private Sub menuCopyDateRec_Click(ByVal sender As Object, ByVal e As EventArgs)
        Clipboard.SetText(ctlDateRechute.Text, TextDataFormat.Text)
    End Sub

    Private Sub ctlDateRechute_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlDateRechute.MouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right And ctlDateRechute.Cursor Is Cursors.Hand Then
            showSelectMenu(New EventHandler(AddressOf menuSelectDateRec_Click), New EventHandler(AddressOf menuRemoveDateRec_Click), New EventHandler(AddressOf menuCopyDateRec_Click))
        End If
    End Sub

    Private Sub menuSelectDateRef_Click(ByVal sender As Object, ByVal e As EventArgs)
        dateDossier_Click(ctlDateReference, MouseEventArgs.Empty)
    End Sub

    Private Sub menuRemoveDateRef_Click(ByVal sender As Object, ByVal e As EventArgs)
        ctlDateReference.Text = ""
        dossierModified = True
    End Sub

    Private Sub menuCopyDateRef_Click(ByVal sender As Object, ByVal e As EventArgs)
        Clipboard.SetText(ctlDateReference.Text, TextDataFormat.Text)
    End Sub

    Private Sub ctlDateReference_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlDateReference.MouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right And ctlDateReference.Cursor Is Cursors.Hand Then
            showSelectMenu(New EventHandler(AddressOf menuSelectDateRef_Click), New EventHandler(AddressOf menuRemoveDateRef_Click), New EventHandler(AddressOf menuCopyDateRef_Click))
        End If
    End Sub


    Private Sub menuSelectRecepDateRef_Click(ByVal sender As Object, ByVal e As EventArgs)
        dateDossier_Click(ctlReceptionReferenceDate, MouseEventArgs.Empty)
    End Sub

    Private Sub menuRemoveRecepDateRef_Click(ByVal sender As Object, ByVal e As EventArgs)
        ctlReceptionReferenceDate.Text = ""
        dossierModified = True
    End Sub

    Private Sub menuCopyRecepDateRef_Click(ByVal sender As Object, ByVal e As EventArgs)
        Clipboard.SetText(ctlReceptionReferenceDate.Text, TextDataFormat.Text)
    End Sub

    Private Sub ctlReceptionReferenceDate_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlReceptionReferenceDate.MouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right And ctlReceptionReferenceDate.Cursor Is Cursors.Hand Then
            showSelectMenu(New EventHandler(AddressOf menuSelectRecepDateRef_Click), New EventHandler(AddressOf menuRemoveRecepDateRef_Click), New EventHandler(AddressOf menuCopyRecepDateRef_Click))
        End If
    End Sub

    Private Sub dossierTextBoxes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctlcodedossier.TextChanged, ctlDateAccident.TextChanged, ctlDateRechute.TextChanged, ctlDateReference.TextChanged, ctlDiagnostic.TextChanged, ctlmedecin.TextChanged, ctlnoref.TextChanged, ctlregion_Renamed.TextChanged, ctlRemarques.TextChanged, ctltherapeute.TextChanged
        dossierModified = True
    End Sub

    Private Sub dossierFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctlDuree.SelectedIndexChanged, ctlFrequence.SelectedIndexChanged, ctlService.SelectedIndexChanged, ctlTRPdemande.SelectedIndexChanged, ctlTRPToTransfer.SelectedIndexChanged
        dossierModified = True
    End Sub

    Private Sub dateDossier_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlDateAccident.MouseMove, ctlDateRechute.MouseMove, ctlDateReference.MouseMove, ctlReceptionReferenceDate.MouseMove
        With CType(sender, TextBox)
            If isLocked = False Then
                'REM_CODES
                Dim noTRP As Integer = User.extractNo(ctltherapeute.Text)
                If sender.name = "ctlDateAccident" Then
                    If Me.codeDossier(currentNoCodeUser).accidentDate = True Then
                        If Not .Cursor Is Cursors.Hand Then .Cursor = Cursors.Hand
                        ToolTip1.SetToolTip(sender, "Simple-clique pour changer la date")
                    Else
                        If Not .Cursor Is Cursors.Default Then .Cursor = Cursors.Default
                        ToolTip1.SetToolTip(sender, "")
                    End If
                ElseIf sender.name = "ctlDateRechute" Then
                    If Me.codeDossier(currentNoCodeUser).relaspeDate = True Then
                        If Not .Cursor Is Cursors.Hand Then .Cursor = Cursors.Hand
                        ToolTip1.SetToolTip(sender, "Simple-clique pour changer la date")
                    Else
                        If Not .Cursor Is Cursors.Default Then .Cursor = Cursors.Default
                        ToolTip1.SetToolTip(sender, "")
                    End If
                Else
                    If Not .Cursor Is Cursors.Hand Then .Cursor = Cursors.Hand
                    ToolTip1.SetToolTip(sender, "Simple-clique pour changer la date")
                End If
            Else
                If Not .Cursor Is Cursors.Default Then .Cursor = Cursors.Default
                ToolTip1.SetToolTip(sender, "")
            End If
        End With
    End Sub

    Private Sub dateDossier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctlDateAccident.Click, ctlDateRechute.Click, ctlDateReference.Click, ctlReceptionReferenceDate.Click
        If sender.cursor Is Cursors.Hand Then
            Dim curDate As Date = Date.Now
            Dim myDateChoice As New DateChoice()
            Dim MyDate As Generic.List(Of Date)

            If sender.text <> "" Then curDate = CDate(sender.text)

            Dim startingYear As Integer = firstUsageDate.Year - 100
            MyDate = myDateChoice.choose(startingYear, Date.Today.Year + 1, , , , , , True, , , , , curDate)
            If MyDate.Count <> 0 Then
                sender.text = DateFormat.getTextDate(MyDate(0))
            End If
        End If
    End Sub

    Private Sub folderInfos_ParentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ParentChanged
        If myMainWin IsNot Nothing Then loading()
    End Sub

    Private Sub loading()
        ''Load SiteLesion
        ctlregion_Renamed.Items.Clear()

        Dim siteLesions() As String = DBLinker.getInstance.readOneDBField("SiteLesion", "SiteLesion", , True)
        If Not siteLesions Is Nothing AndAlso siteLesions.Length <> 0 Then ctlregion_Renamed.Items.AddRange(siteLesions)

        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(, True)
        ctlTRPdemande.Items.Clear()
        ctlTRPToTransfer.Items.Clear()
        Me.ctlTRPdemande.Items.Add(New UserGeneral("* Aucun thérapeute demandé *"))
        Me.ctlTRPToTransfer.Items.Add(New UserGeneral("* Aucun thérapeute à transférer *"))
        ctlTRPdemande.Items.AddRange(users.ToArray)
        ctlTRPToTransfer.Items.AddRange(users.ToArray)
        ctlTRPdemande.SelectedIndex = 0
        ctlTRPToTransfer.SelectedIndex = 0
    End Sub

    Private Sub loadServices(ByVal noUser As Integer)
        If Me.DesignMode Then Exit Sub

        Dim lastService As String = ctlService.Text

        ctlService.Items.Clear()

        Dim curTRP As User = UsersManager.getInstance.getUser(noUser)
        If curTRP Is Nothing OrElse curTRP.services = "" Then Exit Sub

        Dim services() As String = curTRP.services.Split(New Char() {"§"})
        ctlService.Items.AddRange(services)

        Me.service = lastService
    End Sub
End Class
