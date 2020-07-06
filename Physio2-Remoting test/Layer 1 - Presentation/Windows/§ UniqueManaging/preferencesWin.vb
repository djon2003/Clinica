Option Strict Off
Option Explicit On
Friend Class preferencesWin
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.
        Me.MdiParent = myMainWin

        Me.sonAlertClient.Text = "* Aucun *"
        Me.sonAlertKP.Text = "* Aucun *"
        Me.sonAlertMSG.Text = "* Aucun *"
        Me.sonAlertNote.Text = "* Aucun *"
        Me.sonAlertQL.Text = "* Aucun *"
        Me.sonAlertRapportGen.Text = "* Aucun *"

        AdministratorPassword.Text = PreferencesManager.ADMINISTRATOR_DEFAULT_PASSWORD

        With CType(Me.AlertExpiries.Columns("ExpiryInMinutes"), DataGridViewComboBoxColumn)
            .ValueMember = "TotalMinutes"
            .DisplayMember = "Periode"
            Dim dtPeriodes As DataTable = DBLinker.getInstance.readDBForGrid("ListePeriode", "Periode,TotalMinutes").Tables(0)
            Dim newDataRow As DataRow = dtPeriodes.NewRow()
            newDataRow("Periode") = "* Aucune *"
            newDataRow("TotalMinutes") = -1
            dtPeriodes.Rows.InsertAt(newDataRow, 0)
            .DataSource = dtPeriodes
        End With

        Me.AlertExpiries.DataSource = DBLinker.getInstance.readDBForGrid("SELECT * FROM [UserAlertsExpiries] WHERE NoUser IS NULL OR NoUser=" & ConnectionsManager.currentUser & " ORDER BY TypeName")
        Me.AlertExpiries.DataMember = "Table"

        'Load Remote Tasks Controls
        createRemoteTasksControls()


        'Chargement des images
        Dim delImage As Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.AddingReferent.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.addingService.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.addingMP.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.addingAutresTypes.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.removingService.Image = delImage
        Me.removingMP.Image = delImage
        Me.removingReferent.Image = delImage
        Me.removingAutresTypes.Image = delImage
        Me.renamingService.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("rename16.ico"), New Size(16, 16))
        Me.renamingMP.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("rename16.ico"), New Size(16, 16))
        Me.renamingReferent.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("rename16.ico"), New Size(16, 16))
        Me.renamingAutresTypes.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("rename16.ico"), New Size(16, 16))
        Me.save.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.selectSonAlertClient.Image = selectImage
        Me.selectSonAlertKP.Image = selectImage
        Me.selectSonAlertMSG.Image = selectImage
        Me.selectSonAlertNote.Image = selectImage
        Me.selectSonAlertQL.Image = selectImage
        Me.selectSonAlertRapportGen.Image = selectImage
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents _Labels_21 As System.Windows.Forms.Label
    Public WithEvents _Labels_22 As System.Windows.Forms.Label
    Public WithEvents _Labels_25 As System.Windows.Forms.Label
    Public WithEvents _Labels_24 As System.Windows.Forms.Label
    Public WithEvents _Labels_23 As System.Windows.Forms.Label
    Public WithEvents _TabPref_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents _Labels_32 As System.Windows.Forms.Label
    Public WithEvents _Labels_30 As System.Windows.Forms.Label
    Public WithEvents _Labels_29 As System.Windows.Forms.Label
    Public WithEvents _Labels_28 As System.Windows.Forms.Label
    Public WithEvents _Labels_27 As System.Windows.Forms.Label
    Public WithEvents _Labels_26 As System.Windows.Forms.Label
    Public WithEvents _Frames_7 As System.Windows.Forms.GroupBox
    Public WithEvents _Labels_20 As System.Windows.Forms.Label
    Public WithEvents _Labels_19 As System.Windows.Forms.Label
    Public WithEvents _Labels_18 As System.Windows.Forms.Label
    Public WithEvents _Labels_17 As System.Windows.Forms.Label
    Public WithEvents _Labels_16 As System.Windows.Forms.Label
    Public WithEvents _Labels_15 As System.Windows.Forms.Label
    Public WithEvents _Labels_14 As System.Windows.Forms.Label
    Public WithEvents _Frames_5 As System.Windows.Forms.GroupBox
    Public WithEvents _Labels_13 As System.Windows.Forms.Label
    Public WithEvents _Labels_12 As System.Windows.Forms.Label
    Public WithEvents _Labels_11 As System.Windows.Forms.Label
    Public WithEvents _Labels_10 As System.Windows.Forms.Label
    Public WithEvents _Labels_9 As System.Windows.Forms.Label
    Public WithEvents _Labels_8 As System.Windows.Forms.Label
    Public WithEvents _Labels_7 As System.Windows.Forms.Label
    Public WithEvents _Labels_6 As System.Windows.Forms.Label
    Public WithEvents _Labels_5 As System.Windows.Forms.Label
    Public WithEvents _Labels_4 As System.Windows.Forms.Label
    Public WithEvents _Labels_3 As System.Windows.Forms.Label
    Public WithEvents _Labels_2 As System.Windows.Forms.Label
    Public WithEvents _Frames_4 As System.Windows.Forms.GroupBox
    Public WithEvents _Frames_3 As System.Windows.Forms.GroupBox
    Public WithEvents _Frames_0 As System.Windows.Forms.GroupBox
    Public WithEvents _Labels_0 As System.Windows.Forms.Label
    Public WithEvents _TabPref_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents tabPref As System.Windows.Forms.TabControl
    Public WithEvents save As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents CDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents FDialog As System.Windows.Forms.FontDialog
    Public WithEvents aListColors1 As System.Windows.Forms.PictureBox
    Public WithEvents aListColors5 As System.Windows.Forms.PictureBox
    Public WithEvents aListColors3 As System.Windows.Forms.PictureBox
    Public WithEvents aListColors4 As System.Windows.Forms.PictureBox
    Public WithEvents aListColors2 As System.Windows.Forms.PictureBox
    Public WithEvents aListColors6 As System.Windows.Forms.PictureBox
    Public WithEvents colors7 As System.Windows.Forms.PictureBox
    Public WithEvents colors3 As System.Windows.Forms.PictureBox
    Public WithEvents colors5 As System.Windows.Forms.PictureBox
    Public WithEvents colors4 As System.Windows.Forms.PictureBox
    Public WithEvents colors2 As System.Windows.Forms.PictureBox
    Public WithEvents colors6 As System.Windows.Forms.PictureBox
    Public WithEvents colors1 As System.Windows.Forms.PictureBox
    Public WithEvents listColors7 As System.Windows.Forms.PictureBox
    Public WithEvents listColors6 As System.Windows.Forms.PictureBox
    Public WithEvents listColors5 As System.Windows.Forms.PictureBox
    Public WithEvents listColors4 As System.Windows.Forms.PictureBox
    Public WithEvents listColors3 As System.Windows.Forms.PictureBox
    Public WithEvents listColors2 As System.Windows.Forms.PictureBox
    Public WithEvents listColors1 As System.Windows.Forms.PictureBox
    Public WithEvents listMouseSpeed As System.Windows.Forms.HScrollBar
    Public WithEvents listFont As System.Windows.Forms.Button
    Public WithEvents copoc5 As System.Windows.Forms.CheckBox
    Public WithEvents copoc7 As System.Windows.Forms.CheckBox
    Public WithEvents copoc6 As System.Windows.Forms.CheckBox
    Public WithEvents copoc4 As System.Windows.Forms.CheckBox
    Public WithEvents copoc3 As System.Windows.Forms.CheckBox
    Public WithEvents copoc1 As System.Windows.Forms.CheckBox
    Public WithEvents copoc2 As System.Windows.Forms.CheckBox
    Public WithEvents affLastUserType As System.Windows.Forms.CheckBox
    Public WithEvents cocc2 As System.Windows.Forms.CheckBox
    Public WithEvents cocc1 As System.Windows.Forms.CheckBox
    Public WithEvents cocc3 As System.Windows.Forms.CheckBox
    Public WithEvents cocc4 As System.Windows.Forms.CheckBox
    Public WithEvents cocc5 As System.Windows.Forms.CheckBox
    Public WithEvents cocc6 As System.Windows.Forms.CheckBox
    Public WithEvents cocc9 As System.Windows.Forms.CheckBox
    Public WithEvents cocc10 As System.Windows.Forms.CheckBox
    Public WithEvents cocc11 As System.Windows.Forms.CheckBox
    Public WithEvents cocc12 As System.Windows.Forms.CheckBox
    Public WithEvents cocc13 As System.Windows.Forms.CheckBox
    Public WithEvents cocc16 As System.Windows.Forms.CheckBox
    Public WithEvents cocc17 As System.Windows.Forms.CheckBox
    Public WithEvents cocc15 As System.Windows.Forms.CheckBox
    Public WithEvents lastUserType As System.Windows.Forms.Label
    Public WithEvents openingAgendas As System.Windows.Forms.CheckedListBox
    Public WithEvents defaultTRP As System.Windows.Forms.ComboBox
    Public WithEvents agendaFontSize As System.Windows.Forms.TextBox
    Public WithEvents infoBulleClient As System.Windows.Forms.CheckBox
    Public WithEvents infoBulleDate As System.Windows.Forms.CheckBox
    Public WithEvents firstDay As System.Windows.Forms.ComboBox
    Public WithEvents startingPeriode As System.Windows.Forms.ComboBox
    Public WithEvents nbPrefU As System.Windows.Forms.Label
    Public WithEvents taxe2 As ManagedText
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents taxe1 As ManagedText
    Public WithEvents label5 As System.Windows.Forms.Label
    Public WithEvents frameComptabilite As System.Windows.Forms.GroupBox
    Public WithEvents do3DOpt As System.Windows.Forms.CheckBox
    Public WithEvents mouseOver3DOpt As System.Windows.Forms.CheckBox
    Public WithEvents label6 As System.Windows.Forms.Label
    Public WithEvents taxeArrondissement As System.Windows.Forms.ComboBox
    Public WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents UserMDPRespectCasse As System.Windows.Forms.CheckBox
    Public WithEvents label10 As System.Windows.Forms.Label
    Public WithEvents label11 As System.Windows.Forms.Label
    Public WithEvents intervalToShowTextInStatusBar As ManagedText
    Public WithEvents copoc10 As System.Windows.Forms.CheckBox
    Public WithEvents copoc8 As System.Windows.Forms.CheckBox
    Public WithEvents copoc9 As System.Windows.Forms.CheckBox
    Public WithEvents cocc18 As System.Windows.Forms.CheckBox
    Public WithEvents cocc19 As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmDateAccident As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmDateRechute As System.Windows.Forms.CheckBox
    Friend WithEvents AutoDelQLAfterNewRV As System.Windows.Forms.CheckBox
    Public WithEvents questionLastNewFolder As System.Windows.Forms.CheckBox
    Public WithEvents autoOpenSearchIfNAMEmpty As System.Windows.Forms.CheckBox
    Public WithEvents autoNewRV As System.Windows.Forms.CheckBox
    Public WithEvents label12 As System.Windows.Forms.Label
    Public WithEvents defaultEquipementType As System.Windows.Forms.ComboBox
    Public WithEvents findPlacesUpTo As System.Windows.Forms.ComboBox
    Public WithEvents label13 As System.Windows.Forms.Label
    Public WithEvents triPaiements As System.Windows.Forms.ComboBox
    Public WithEvents label14 As System.Windows.Forms.Label
    Public WithEvents label15 As System.Windows.Forms.Label
    Public WithEvents label16 As System.Windows.Forms.Label
    Public WithEvents label17 As System.Windows.Forms.Label
    Public WithEvents triRVFuturs As System.Windows.Forms.ComboBox
    Public WithEvents triRVCompte As System.Windows.Forms.ComboBox
    Public WithEvents triFactures As System.Windows.Forms.ComboBox
    Friend WithEvents FrameTriage As System.Windows.Forms.GroupBox
    Public WithEvents label18 As System.Windows.Forms.Label
    Public WithEvents dblClickOnFutureRV As System.Windows.Forms.ComboBox
    Friend WithEvents Services As System.Windows.Forms.ListBox
    Public WithEvents addingService As System.Windows.Forms.Button
    Public WithEvents removingService As System.Windows.Forms.Button
    Friend WithEvents MethodesPaiment As System.Windows.Forms.ListBox
    Public WithEvents removingMP As System.Windows.Forms.Button
    Public WithEvents addingMP As System.Windows.Forms.Button
    Friend WithEvents ConfirmDBDragDrop As System.Windows.Forms.CheckBox
    Public WithEvents label19 As System.Windows.Forms.Label
    Public WithEvents importOriginFileAction As System.Windows.Forms.ComboBox
    Public WithEvents label20 As System.Windows.Forms.Label
    Public WithEvents label21 As System.Windows.Forms.Label
    Public WithEvents clientTabAutoSelect As System.Windows.Forms.ComboBox
    Public WithEvents dossierTabAutoSelect As System.Windows.Forms.ComboBox
    Friend WithEvents AutoSelectLastUserInAcces As System.Windows.Forms.CheckBox
    Friend WithEvents AffAdminInAcces As System.Windows.Forms.CheckBox
    Public WithEvents label22 As System.Windows.Forms.Label
    Public WithEvents ancre As ManagedText
    Public WithEvents affMSGModif As System.Windows.Forms.CheckBox
    Public WithEvents copoc11 As System.Windows.Forms.CheckBox
    Public WithEvents label23 As System.Windows.Forms.Label
    Public WithEvents nbMaxWindowsListBox As ManagedText
    Public WithEvents label24 As System.Windows.Forms.Label
    Public WithEvents label25 As System.Windows.Forms.Label
    Public WithEvents colorWithoutRV As System.Windows.Forms.PictureBox
    Public WithEvents colorWithRV As System.Windows.Forms.PictureBox
    Public WithEvents frameColorsQL As System.Windows.Forms.GroupBox
    Public WithEvents label26 As System.Windows.Forms.Label
    Public WithEvents nbJourForAutoQL As ManagedText
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ActiveFolderAutoOnRVAdding As System.Windows.Forms.CheckBox
    Public WithEvents label27 As System.Windows.Forms.Label
    Public WithEvents label28 As System.Windows.Forms.Label
    Public WithEvents openFuturRV As System.Windows.Forms.ComboBox
    Public WithEvents openInstantMSG As System.Windows.Forms.ComboBox
    Public WithEvents renamingMP As System.Windows.Forms.Button
    Public WithEvents renamingService As System.Windows.Forms.Button
    Friend WithEvents ShowQLOnAgendaRemove As System.Windows.Forms.CheckBox
    Public WithEvents medecinCategorie As ManagedText
    Public WithEvents label7 As System.Windows.Forms.Label
    Public WithEvents autoScrollHiddenBox As System.Windows.Forms.CheckBox
    Public WithEvents removingReferent As System.Windows.Forms.Button
    Public WithEvents label9 As System.Windows.Forms.Label
    Public WithEvents renamingReferent As System.Windows.Forms.Button
    Friend WithEvents PreReferents As System.Windows.Forms.ListBox
    Friend WithEvents AddingReferent As System.Windows.Forms.Button
    Public WithEvents listGras As System.Windows.Forms.CheckBox
    Public WithEvents listItalique As System.Windows.Forms.CheckBox
    Public WithEvents listSouligne As System.Windows.Forms.CheckBox
    Public WithEvents listBarre As System.Windows.Forms.CheckBox
    Public WithEvents rvEvalBarre As System.Windows.Forms.CheckBox
    Public WithEvents rvEvalSouligne As System.Windows.Forms.CheckBox
    Public WithEvents rvEvalItalique As System.Windows.Forms.CheckBox
    Public WithEvents rvEvalGras As System.Windows.Forms.CheckBox
    Public WithEvents rvEvalFont As System.Windows.Forms.Button
    Public WithEvents label29 As System.Windows.Forms.Label
    Friend WithEvents FrameConfirmation As System.Windows.Forms.GroupBox
    Public WithEvents listBorder As ManagedText
    Public WithEvents listMarge As ManagedText
    Public WithEvents listFontSize As ManagedText
    Public WithEvents label31 As System.Windows.Forms.Label
    Public WithEvents htmlEditorPath As ManagedText
    Public WithEvents label32 As System.Windows.Forms.Label
    Friend WithEvents AutresTypesBills As System.Windows.Forms.ListBox
    Public WithEvents renamingAutresTypes As System.Windows.Forms.Button
    Public WithEvents addingAutresTypes As System.Windows.Forms.Button
    Friend WithEvents ActivateWorkHoursApprobation As System.Windows.Forms.CheckBox
    Public WithEvents punchModeArrival As System.Windows.Forms.ComboBox
    Public WithEvents label33 As System.Windows.Forms.Label
    Public WithEvents punchModeDeparture As System.Windows.Forms.ComboBox
    Public WithEvents label34 As System.Windows.Forms.Label
    Public WithEvents label35 As System.Windows.Forms.Label
    Public WithEvents openPunch As System.Windows.Forms.ComboBox
    Public WithEvents actionOnNoRV As System.Windows.Forms.ComboBox
    Public WithEvents label36 As System.Windows.Forms.Label
    Friend WithEvents FrameHidden As System.Windows.Forms.GroupBox
    Public WithEvents typeAffListeFenetres As System.Windows.Forms.ComboBox
    Public WithEvents label37 As System.Windows.Forms.Label
    Friend WithEvents AutoSelectAncre As System.Windows.Forms.CheckBox
    Public WithEvents alertUserOnRapportGeneration As System.Windows.Forms.CheckBox
    Public WithEvents prefixNoRecu As ManagedText
    Public WithEvents label38 As System.Windows.Forms.Label
    Public WithEvents textForPlageBloquee As ManagedText
    Public WithEvents label39 As System.Windows.Forms.Label
    Public WithEvents colorPresencePayee As System.Windows.Forms.PictureBox
    Public WithEvents label40 As System.Windows.Forms.Label
    Public WithEvents colorBloquee As System.Windows.Forms.PictureBox
    Public WithEvents label41 As System.Windows.Forms.Label
    Friend WithEvents AdjustingCommentsForced As System.Windows.Forms.CheckBox
    Friend WithEvents MontantFactureHistoIsCumulative As System.Windows.Forms.CheckBox
    Public WithEvents nbVisiteCAR As ManagedText
    Public WithEvents label42 As System.Windows.Forms.Label
    Public WithEvents nbDayCAR As ManagedText
    Public WithEvents label43 As System.Windows.Forms.Label
    Friend WithEvents PagesUsers As System.Windows.Forms.TabControl
    Friend WithEvents PageUserAgenda As System.Windows.Forms.TabPage
    Friend WithEvents PageUserAutres As System.Windows.Forms.TabPage
    Friend WithEvents PageUserCompteclient As System.Windows.Forms.TabPage
    Friend WithEvents PageUserRapport As System.Windows.Forms.TabPage
    Friend WithEvents PageUserRendezvous As System.Windows.Forms.TabPage
    Friend WithEvents PagesCliniques As System.Windows.Forms.TabControl
    Friend WithEvents PageCliniqueAutres As System.Windows.Forms.TabPage
    Friend WithEvents PageCliniqueUtilisateur As System.Windows.Forms.TabPage
    Friend WithEvents PageCliniqueAffichage As System.Windows.Forms.TabPage
    Friend WithEvents PageCliniqueBD As System.Windows.Forms.TabPage
    Friend WithEvents PageCliniqueCompteclient As System.Windows.Forms.TabPage
    Friend WithEvents PageCliniqueFacturation As System.Windows.Forms.TabPage
    Friend WithEvents PageCliniqueKP As System.Windows.Forms.TabPage
    Friend WithEvents PageCliniqueRendezvous As System.Windows.Forms.TabPage
    Friend WithEvents GroupTriRV As System.Windows.Forms.GroupBox
    Public WithEvents questionChoosingFolder As System.Windows.Forms.CheckBox
    Friend WithEvents DemandeAuthorisationCommentsRequired As System.Windows.Forms.CheckBox
    Friend WithEvents PageCliniquePrinting As System.Windows.Forms.TabPage
    Public WithEvents label44 As System.Windows.Forms.Label
    Public WithEvents colorTitreFactureSouffrance As System.Windows.Forms.PictureBox
    Public WithEvents generateAutoRapportOnOpening As System.Windows.Forms.CheckBox
    Public WithEvents affRapportCatInSelection As System.Windows.Forms.CheckBox
    Friend WithEvents printRecuForClientAuto As System.Windows.Forms.CheckBox
    Friend WithEvents GroupAutresStartup As System.Windows.Forms.GroupBox
    Public WithEvents autoSelectFolderInRV As System.Windows.Forms.CheckBox
    Public WithEvents affRapportCatInFinDeMois As System.Windows.Forms.CheckBox
    Public WithEvents label46 As System.Windows.Forms.Label
    Public WithEvents colorObjActivatedBySoftware As System.Windows.Forms.PictureBox
    Public WithEvents nbEvalTo100TauxAutonomie As ManagedText
    Public WithEvents label47 As System.Windows.Forms.Label
    Public WithEvents autoOpenSideBarOnTransfer As System.Windows.Forms.CheckBox
    Public WithEvents autoCloseSideBarOnTransfer As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents newAlertGras As System.Windows.Forms.CheckBox
    Public WithEvents label48 As System.Windows.Forms.Label
    Public WithEvents newAlertItalic As System.Windows.Forms.CheckBox
    Public WithEvents newAlertStrike As System.Windows.Forms.CheckBox
    Public WithEvents newAlertUnder As System.Windows.Forms.CheckBox
    Public WithEvents newRvFont As System.Windows.Forms.Button
    Public WithEvents label49 As System.Windows.Forms.Label
    Public WithEvents newAlertFontSize As ManagedText
    Public WithEvents textShowedCharByCharVertically As System.Windows.Forms.CheckBox
    Public WithEvents nbSecBeforeMarkAsRead As ManagedText
    Public WithEvents label50 As System.Windows.Forms.Label
    Friend WithEvents PageUserSons As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents sonAlertKP As System.Windows.Forms.TextBox
    Friend WithEvents sonAlertClient As System.Windows.Forms.TextBox
    Public WithEvents selectSonAlertKP As System.Windows.Forms.Button
    Public WithEvents selectSonAlertClient As System.Windows.Forms.Button
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents sonAlertRapportGen As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents sonAlertQL As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents sonAlertNote As System.Windows.Forms.TextBox
    Public WithEvents selectSonAlertRapportGen As System.Windows.Forms.Button
    Friend WithEvents sonAlertMSG As System.Windows.Forms.TextBox
    Public WithEvents selectSonAlertQL As System.Windows.Forms.Button
    Public WithEvents selectSonAlertNote As System.Windows.Forms.Button
    Public WithEvents selectSonAlertMSG As System.Windows.Forms.Button
    Friend WithEvents SelectSonMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AucunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParDéfautToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DepuisLaBanqueDeDonnéesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Public WithEvents publipostageSpacing As ManagedText
    Friend WithEvents PageMessagerie As System.Windows.Forms.TabPage
    Public WithEvents insertOriginMSGOnRespond As System.Windows.Forms.CheckBox
    Public WithEvents receiveFeedBackForMessages As System.Windows.Forms.CheckBox
    Public WithEvents alertUsersByDefOnMsgSending As System.Windows.Forms.CheckBox
    Public WithEvents receivePlageBloqueeAlert As System.Windows.Forms.CheckBox
    Public WithEvents openClientAccountOnFolderTransfer As System.Windows.Forms.CheckBox
    Public WithEvents checkForMissingInfos As System.Windows.Forms.CheckBox
    Friend WithEvents ActiveFolderAutoOnRVStatusChange As System.Windows.Forms.CheckBox
    Friend WithEvents VerifyFrequenceForNewRV As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant As System.Windows.Forms.CheckBox
    Public WithEvents autoCloseSearchClientOnOpenAccount As System.Windows.Forms.CheckBox
    Public WithEvents activateNumLockOnStartup As System.Windows.Forms.CheckBox
    Public WithEvents label60 As System.Windows.Forms.Label
    Public WithEvents dossierTexteAutoSel As System.Windows.Forms.ComboBox
    Public WithEvents nbSecondsForPing As ManagedText
    Public WithEvents label61 As System.Windows.Forms.Label
    Friend WithEvents AskForReplacingDBItem As System.Windows.Forms.CheckBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents addEmailSignOnSend As System.Windows.Forms.CheckBox
    Public WithEvents addEmailSignOnAnswer As System.Windows.Forms.CheckBox
    Public WithEvents redirectInternMailToEmail As System.Windows.Forms.CheckBox
    Public WithEvents affSpecialDatesInCalendar As System.Windows.Forms.CheckBox
    Public WithEvents affSpecialDatesInAgenda As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Public WithEvents colorSpecialDates As System.Windows.Forms.PictureBox
    Public WithEvents label71 As System.Windows.Forms.Label
    Public WithEvents keepATraceOfSentMsg As System.Windows.Forms.CheckBox
    Public WithEvents mailFolderForSentMsg As ManagedText
    Friend WithEvents AffDefCodesInSpecificTRP As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents AlertExpiries As DataGridPlus
    Friend WithEvents TypeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpiryInMinutes As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents NoUserAlertType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoUser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents printingHeader As ManagedText
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Public WithEvents printingFooter As ManagedText
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Public WithEvents publipostageTopMargin As ManagedText
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Public WithEvents sortingInstantMsgAndNotes As System.Windows.Forms.ComboBox
    Public WithEvents label67 As System.Windows.Forms.Label
    Public WithEvents colorHoraireClose As System.Windows.Forms.PictureBox
    Public WithEvents label62 As System.Windows.Forms.Label
    Public WithEvents label59 As System.Windows.Forms.Label
    Public WithEvents colorHoraireOpen As System.Windows.Forms.PictureBox
    Public WithEvents hideEndedUsersFolder As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Public WithEvents label72 As System.Windows.Forms.Label
    Public WithEvents label68 As System.Windows.Forms.Label
    Public WithEvents publipostageSendingInterval As ManagedText
    Public WithEvents publipostageNbSending As ManagedText
    Friend WithEvents AlertTRPOnRVAbsence As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Public WithEvents showExtraInfosInWindowsListBox As System.Windows.Forms.CheckBox
    Friend WithEvents AllowToSkipAbsenceReasonInsertionToText As System.Windows.Forms.CheckBox
    Public WithEvents groupBox9 As System.Windows.Forms.GroupBox
    Public WithEvents colorCommSent As System.Windows.Forms.PictureBox
    Public WithEvents label73 As System.Windows.Forms.Label
    Public WithEvents label74 As System.Windows.Forms.Label
    Public WithEvents colorCommReceived As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Public WithEvents autoupdateOnStart As System.Windows.Forms.CheckBox
    Friend WithEvents AllowUserEmptyPassword As System.Windows.Forms.CheckBox
    Friend WithEvents groupAutoSaveRemoteTask As System.Windows.Forms.GroupBox
    Friend WithEvents panelAutoSaveRemoteTask As System.Windows.Forms.Panel
    Friend WithEvents SelectRemoteTaskDBPath As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NePasEnregistrerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersLaBanqueDeDonnéesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents taxesInteraction As System.Windows.Forms.ComboBox
    Public WithEvents Label75 As System.Windows.Forms.Label
    Public WithEvents Label76 As System.Windows.Forms.Label
    Public WithEvents AdministratorPassword As ManagedText
    Public WithEvents openClientAccountOnNewFolderWORV As System.Windows.Forms.CheckBox
    Public WithEvents mailFolderForDelMsg As ManagedText
    Public WithEvents sendDeleteMailToTrash As System.Windows.Forms.CheckBox
    Friend WithEvents btnChooseEmailSignature As System.Windows.Forms.Button
    Public WithEvents EmailSignatureModel As System.Windows.Forms.Label
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents tax1Name As ManagedText
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Public WithEvents Label66 As System.Windows.Forms.Label
    Public WithEvents Label70 As System.Windows.Forms.Label
    Public WithEvents Label77 As System.Windows.Forms.Label
    Public WithEvents tax2Name As ManagedText
    Public WithEvents textToReplaceAbsNotMotivatedInReceipt As CI.Base.ManagedText
    Friend WithEvents changeAbsenceTypeForSpecificText As System.Windows.Forms.CheckBox
    Public WithEvents removingAutresTypes As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.aListColors1 = New System.Windows.Forms.PictureBox
        Me.aListColors5 = New System.Windows.Forms.PictureBox
        Me.aListColors3 = New System.Windows.Forms.PictureBox
        Me.aListColors4 = New System.Windows.Forms.PictureBox
        Me.aListColors2 = New System.Windows.Forms.PictureBox
        Me.aListColors6 = New System.Windows.Forms.PictureBox
        Me.colors7 = New System.Windows.Forms.PictureBox
        Me.colors3 = New System.Windows.Forms.PictureBox
        Me.colors5 = New System.Windows.Forms.PictureBox
        Me.colors4 = New System.Windows.Forms.PictureBox
        Me.colors2 = New System.Windows.Forms.PictureBox
        Me.colors6 = New System.Windows.Forms.PictureBox
        Me.colors1 = New System.Windows.Forms.PictureBox
        Me.listColors7 = New System.Windows.Forms.PictureBox
        Me.listColors6 = New System.Windows.Forms.PictureBox
        Me.listColors5 = New System.Windows.Forms.PictureBox
        Me.listColors4 = New System.Windows.Forms.PictureBox
        Me.listColors3 = New System.Windows.Forms.PictureBox
        Me.listColors2 = New System.Windows.Forms.PictureBox
        Me.listColors1 = New System.Windows.Forms.PictureBox
        Me.tabPref = New System.Windows.Forms.TabControl
        Me._TabPref_TabPage0 = New System.Windows.Forms.TabPage
        Me.PagesUsers = New System.Windows.Forms.TabControl
        Me.PageUserAgenda = New System.Windows.Forms.TabPage
        Me.openingAgendas = New System.Windows.Forms.CheckedListBox
        Me.agendaFontSize = New System.Windows.Forms.TextBox
        Me._Labels_21 = New System.Windows.Forms.Label
        Me.infoBulleClient = New System.Windows.Forms.CheckBox
        Me.infoBulleDate = New System.Windows.Forms.CheckBox
        Me._Labels_23 = New System.Windows.Forms.Label
        Me.firstDay = New System.Windows.Forms.ComboBox
        Me._Labels_24 = New System.Windows.Forms.Label
        Me.startingPeriode = New System.Windows.Forms.ComboBox
        Me._Labels_25 = New System.Windows.Forms.Label
        Me._Labels_22 = New System.Windows.Forms.Label
        Me.defaultTRP = New System.Windows.Forms.ComboBox
        Me.questionChoosingFolder = New System.Windows.Forms.CheckBox
        Me.questionLastNewFolder = New System.Windows.Forms.CheckBox
        Me.PageUserAutres = New System.Windows.Forms.TabPage
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.label23 = New System.Windows.Forms.Label
        Me.nbMaxWindowsListBox = New CI.Base.ManagedText
        Me.showExtraInfosInWindowsListBox = New System.Windows.Forms.CheckBox
        Me.GroupAutresStartup = New System.Windows.Forms.GroupBox
        Me.label27 = New System.Windows.Forms.Label
        Me.label35 = New System.Windows.Forms.Label
        Me.label28 = New System.Windows.Forms.Label
        Me.openPunch = New System.Windows.Forms.ComboBox
        Me.openFuturRV = New System.Windows.Forms.ComboBox
        Me.openInstantMSG = New System.Windows.Forms.ComboBox
        Me.affMSGModif = New System.Windows.Forms.CheckBox
        Me.autoCloseSideBarOnTransfer = New System.Windows.Forms.CheckBox
        Me.autoOpenSideBarOnTransfer = New System.Windows.Forms.CheckBox
        Me.activateNumLockOnStartup = New System.Windows.Forms.CheckBox
        Me.autoScrollHiddenBox = New System.Windows.Forms.CheckBox
        Me.label19 = New System.Windows.Forms.Label
        Me.importOriginFileAction = New System.Windows.Forms.ComboBox
        Me.PageUserCompteclient = New System.Windows.Forms.TabPage
        Me.autoCloseSearchClientOnOpenAccount = New System.Windows.Forms.CheckBox
        Me.checkForMissingInfos = New System.Windows.Forms.CheckBox
        Me.openClientAccountOnNewFolderWORV = New System.Windows.Forms.CheckBox
        Me.openClientAccountOnFolderTransfer = New System.Windows.Forms.CheckBox
        Me.autoSelectFolderInRV = New System.Windows.Forms.CheckBox
        Me.label20 = New System.Windows.Forms.Label
        Me.label60 = New System.Windows.Forms.Label
        Me.label21 = New System.Windows.Forms.Label
        Me.dossierTexteAutoSel = New System.Windows.Forms.ComboBox
        Me.clientTabAutoSelect = New System.Windows.Forms.ComboBox
        Me.dossierTabAutoSelect = New System.Windows.Forms.ComboBox
        Me.PageMessagerie = New System.Windows.Forms.TabPage
        Me.redirectInternMailToEmail = New System.Windows.Forms.CheckBox
        Me.sortingInstantMsgAndNotes = New System.Windows.Forms.ComboBox
        Me.mailFolderForDelMsg = New CI.Base.ManagedText
        Me.mailFolderForSentMsg = New CI.Base.ManagedText
        Me.sendDeleteMailToTrash = New System.Windows.Forms.CheckBox
        Me.keepATraceOfSentMsg = New System.Windows.Forms.CheckBox
        Me.nbSecBeforeMarkAsRead = New CI.Base.ManagedText
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btnChooseEmailSignature = New System.Windows.Forms.Button
        Me.addEmailSignOnAnswer = New System.Windows.Forms.CheckBox
        Me.addEmailSignOnSend = New System.Windows.Forms.CheckBox
        Me.EmailSignatureModel = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.receiveFeedBackForMessages = New System.Windows.Forms.CheckBox
        Me.Label69 = New System.Windows.Forms.Label
        Me.AlertExpiries = New CI.Base.Windows.Forms.DataGridPlus
        Me.TypeName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExpiryInMinutes = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.NoUserAlertType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoUser = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.label67 = New System.Windows.Forms.Label
        Me.receivePlageBloqueeAlert = New System.Windows.Forms.CheckBox
        Me.insertOriginMSGOnRespond = New System.Windows.Forms.CheckBox
        Me.alertUsersByDefOnMsgSending = New System.Windows.Forms.CheckBox
        Me.label50 = New System.Windows.Forms.Label
        Me.PageUserRapport = New System.Windows.Forms.TabPage
        Me.affRapportCatInSelection = New System.Windows.Forms.CheckBox
        Me.affRapportCatInFinDeMois = New System.Windows.Forms.CheckBox
        Me.generateAutoRapportOnOpening = New System.Windows.Forms.CheckBox
        Me.alertUserOnRapportGeneration = New System.Windows.Forms.CheckBox
        Me.PageUserRendezvous = New System.Windows.Forms.TabPage
        Me.label13 = New System.Windows.Forms.Label
        Me.findPlacesUpTo = New System.Windows.Forms.ComboBox
        Me.autoOpenSearchIfNAMEmpty = New System.Windows.Forms.CheckBox
        Me.autoNewRV = New System.Windows.Forms.CheckBox
        Me.label18 = New System.Windows.Forms.Label
        Me.dblClickOnFutureRV = New System.Windows.Forms.ComboBox
        Me.PageUserSons = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.selectSonAlertRapportGen = New System.Windows.Forms.Button
        Me.selectSonAlertQL = New System.Windows.Forms.Button
        Me.selectSonAlertNote = New System.Windows.Forms.Button
        Me.selectSonAlertMSG = New System.Windows.Forms.Button
        Me.selectSonAlertKP = New System.Windows.Forms.Button
        Me.selectSonAlertClient = New System.Windows.Forms.Button
        Me.Label56 = New System.Windows.Forms.Label
        Me.Label55 = New System.Windows.Forms.Label
        Me.sonAlertRapportGen = New System.Windows.Forms.TextBox
        Me.Label54 = New System.Windows.Forms.Label
        Me.sonAlertQL = New System.Windows.Forms.TextBox
        Me.Label53 = New System.Windows.Forms.Label
        Me.sonAlertNote = New System.Windows.Forms.TextBox
        Me.Label52 = New System.Windows.Forms.Label
        Me.sonAlertMSG = New System.Windows.Forms.TextBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.sonAlertKP = New System.Windows.Forms.TextBox
        Me.sonAlertClient = New System.Windows.Forms.TextBox
        Me.nbPrefU = New System.Windows.Forms.Label
        Me._TabPref_TabPage1 = New System.Windows.Forms.TabPage
        Me.PagesCliniques = New System.Windows.Forms.TabControl
        Me.PageCliniqueAffichage = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.colorSpecialDates = New System.Windows.Forms.PictureBox
        Me.affSpecialDatesInCalendar = New System.Windows.Forms.CheckBox
        Me.label71 = New System.Windows.Forms.Label
        Me.affSpecialDatesInAgenda = New System.Windows.Forms.CheckBox
        Me.textShowedCharByCharVertically = New System.Windows.Forms.CheckBox
        Me._Frames_7 = New System.Windows.Forms.GroupBox
        Me._Labels_32 = New System.Windows.Forms.Label
        Me._Labels_30 = New System.Windows.Forms.Label
        Me._Labels_29 = New System.Windows.Forms.Label
        Me._Labels_28 = New System.Windows.Forms.Label
        Me._Labels_27 = New System.Windows.Forms.Label
        Me._Labels_26 = New System.Windows.Forms.Label
        Me.label46 = New System.Windows.Forms.Label
        Me.label44 = New System.Windows.Forms.Label
        Me.colorObjActivatedBySoftware = New System.Windows.Forms.PictureBox
        Me._Frames_5 = New System.Windows.Forms.GroupBox
        Me._Labels_16 = New System.Windows.Forms.Label
        Me.label40 = New System.Windows.Forms.Label
        Me.colorHoraireClose = New System.Windows.Forms.PictureBox
        Me._Labels_18 = New System.Windows.Forms.Label
        Me._Labels_17 = New System.Windows.Forms.Label
        Me.label62 = New System.Windows.Forms.Label
        Me._Labels_19 = New System.Windows.Forms.Label
        Me.label59 = New System.Windows.Forms.Label
        Me.label41 = New System.Windows.Forms.Label
        Me.colorPresencePayee = New System.Windows.Forms.PictureBox
        Me._Labels_20 = New System.Windows.Forms.Label
        Me._Labels_15 = New System.Windows.Forms.Label
        Me._Labels_14 = New System.Windows.Forms.Label
        Me.colorHoraireOpen = New System.Windows.Forms.PictureBox
        Me.colorBloquee = New System.Windows.Forms.PictureBox
        Me.colorTitreFactureSouffrance = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.newAlertGras = New System.Windows.Forms.CheckBox
        Me.label48 = New System.Windows.Forms.Label
        Me.newAlertItalic = New System.Windows.Forms.CheckBox
        Me.newAlertStrike = New System.Windows.Forms.CheckBox
        Me.newAlertUnder = New System.Windows.Forms.CheckBox
        Me.newRvFont = New System.Windows.Forms.Button
        Me.label49 = New System.Windows.Forms.Label
        Me.newAlertFontSize = New CI.Base.ManagedText
        Me.FrameConfirmation = New System.Windows.Forms.GroupBox
        Me.rvEvalGras = New System.Windows.Forms.CheckBox
        Me.label29 = New System.Windows.Forms.Label
        Me.rvEvalItalique = New System.Windows.Forms.CheckBox
        Me.rvEvalBarre = New System.Windows.Forms.CheckBox
        Me.rvEvalSouligne = New System.Windows.Forms.CheckBox
        Me.rvEvalFont = New System.Windows.Forms.Button
        Me._Frames_4 = New System.Windows.Forms.GroupBox
        Me.listFont = New System.Windows.Forms.Button
        Me.listMarge = New CI.Base.ManagedText
        Me._Labels_2 = New System.Windows.Forms.Label
        Me._Labels_13 = New System.Windows.Forms.Label
        Me.listBorder = New CI.Base.ManagedText
        Me._Labels_12 = New System.Windows.Forms.Label
        Me.listFontSize = New CI.Base.ManagedText
        Me._Labels_11 = New System.Windows.Forms.Label
        Me.listBarre = New System.Windows.Forms.CheckBox
        Me._Labels_10 = New System.Windows.Forms.Label
        Me.listSouligne = New System.Windows.Forms.CheckBox
        Me._Labels_9 = New System.Windows.Forms.Label
        Me.listItalique = New System.Windows.Forms.CheckBox
        Me._Labels_8 = New System.Windows.Forms.Label
        Me.listGras = New System.Windows.Forms.CheckBox
        Me._Labels_7 = New System.Windows.Forms.Label
        Me.listMouseSpeed = New System.Windows.Forms.HScrollBar
        Me._Labels_3 = New System.Windows.Forms.Label
        Me._Labels_6 = New System.Windows.Forms.Label
        Me.mouseOver3DOpt = New System.Windows.Forms.CheckBox
        Me._Labels_4 = New System.Windows.Forms.Label
        Me._Labels_5 = New System.Windows.Forms.Label
        Me.do3DOpt = New System.Windows.Forms.CheckBox
        Me.groupBox9 = New System.Windows.Forms.GroupBox
        Me.colorCommSent = New System.Windows.Forms.PictureBox
        Me.label73 = New System.Windows.Forms.Label
        Me.label74 = New System.Windows.Forms.Label
        Me.colorCommReceived = New System.Windows.Forms.PictureBox
        Me.frameColorsQL = New System.Windows.Forms.GroupBox
        Me.colorWithoutRV = New System.Windows.Forms.PictureBox
        Me.label24 = New System.Windows.Forms.Label
        Me.label25 = New System.Windows.Forms.Label
        Me.colorWithRV = New System.Windows.Forms.PictureBox
        Me.label37 = New System.Windows.Forms.Label
        Me.typeAffListeFenetres = New System.Windows.Forms.ComboBox
        Me.PageCliniqueAutres = New System.Windows.Forms.TabPage
        Me.defaultEquipementType = New System.Windows.Forms.ComboBox
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.autoupdateOnStart = New System.Windows.Forms.CheckBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.label31 = New System.Windows.Forms.Label
        Me.htmlEditorPath = New CI.Base.ManagedText
        Me.label22 = New System.Windows.Forms.Label
        Me.ancre = New CI.Base.ManagedText
        Me.AutoSelectAncre = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.label72 = New System.Windows.Forms.Label
        Me.label68 = New System.Windows.Forms.Label
        Me.publipostageSendingInterval = New CI.Base.ManagedText
        Me.publipostageNbSending = New CI.Base.ManagedText
        Me.label12 = New System.Windows.Forms.Label
        Me.nbSecondsForPing = New CI.Base.ManagedText
        Me.label11 = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.label61 = New System.Windows.Forms.Label
        Me.intervalToShowTextInStatusBar = New CI.Base.ManagedText
        Me.PageCliniqueBD = New System.Windows.Forms.TabPage
        Me.groupAutoSaveRemoteTask = New System.Windows.Forms.GroupBox
        Me.panelAutoSaveRemoteTask = New System.Windows.Forms.Panel
        Me.AskForReplacingDBItem = New System.Windows.Forms.CheckBox
        Me.ConfirmDBDragDrop = New System.Windows.Forms.CheckBox
        Me.PageCliniqueCompteclient = New System.Windows.Forms.TabPage
        Me.PreReferents = New System.Windows.Forms.ListBox
        Me._Frames_0 = New System.Windows.Forms.GroupBox
        Me.cocc19 = New System.Windows.Forms.CheckBox
        Me.cocc18 = New System.Windows.Forms.CheckBox
        Me.cocc2 = New System.Windows.Forms.CheckBox
        Me.cocc1 = New System.Windows.Forms.CheckBox
        Me.cocc3 = New System.Windows.Forms.CheckBox
        Me.cocc4 = New System.Windows.Forms.CheckBox
        Me.cocc5 = New System.Windows.Forms.CheckBox
        Me.cocc6 = New System.Windows.Forms.CheckBox
        Me.cocc9 = New System.Windows.Forms.CheckBox
        Me.cocc10 = New System.Windows.Forms.CheckBox
        Me.cocc11 = New System.Windows.Forms.CheckBox
        Me.cocc12 = New System.Windows.Forms.CheckBox
        Me.cocc13 = New System.Windows.Forms.CheckBox
        Me.cocc16 = New System.Windows.Forms.CheckBox
        Me.cocc17 = New System.Windows.Forms.CheckBox
        Me.cocc15 = New System.Windows.Forms.CheckBox
        Me.ConfirmDateAccident = New System.Windows.Forms.CheckBox
        Me.DemandeAuthorisationCommentsRequired = New System.Windows.Forms.CheckBox
        Me.ConfirmDateRechute = New System.Windows.Forms.CheckBox
        Me.label9 = New System.Windows.Forms.Label
        Me.renamingReferent = New System.Windows.Forms.Button
        Me.removingReferent = New System.Windows.Forms.Button
        Me.AddingReferent = New System.Windows.Forms.Button
        Me.PageCliniqueFacturation = New System.Windows.Forms.TabPage
        Me.nbDayCAR = New CI.Base.ManagedText
        Me.frameComptabilite = New System.Windows.Forms.GroupBox
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.taxe2 = New CI.Base.ManagedText
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label70 = New System.Windows.Forms.Label
        Me.Label77 = New System.Windows.Forms.Label
        Me.tax2Name = New CI.Base.ManagedText
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.taxe1 = New CI.Base.ManagedText
        Me.Label30 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.tax1Name = New CI.Base.ManagedText
        Me.MethodesPaiment = New System.Windows.Forms.ListBox
        Me.removingMP = New System.Windows.Forms.Button
        Me.addingMP = New System.Windows.Forms.Button
        Me.taxesInteraction = New System.Windows.Forms.ComboBox
        Me.taxeArrondissement = New System.Windows.Forms.ComboBox
        Me.Label75 = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.renamingMP = New System.Windows.Forms.Button
        Me.FrameTriage = New System.Windows.Forms.GroupBox
        Me.triFactures = New System.Windows.Forms.ComboBox
        Me.triPaiements = New System.Windows.Forms.ComboBox
        Me.label15 = New System.Windows.Forms.Label
        Me.label14 = New System.Windows.Forms.Label
        Me.label43 = New System.Windows.Forms.Label
        Me.nbVisiteCAR = New CI.Base.ManagedText
        Me.AutresTypesBills = New System.Windows.Forms.ListBox
        Me.label42 = New System.Windows.Forms.Label
        Me.removingAutresTypes = New System.Windows.Forms.Button
        Me.addingAutresTypes = New System.Windows.Forms.Button
        Me.renamingAutresTypes = New System.Windows.Forms.Button
        Me.MontantFactureHistoIsCumulative = New System.Windows.Forms.CheckBox
        Me.label32 = New System.Windows.Forms.Label
        Me.printRecuForClientAuto = New System.Windows.Forms.CheckBox
        Me.AdjustingCommentsForced = New System.Windows.Forms.CheckBox
        Me.label38 = New System.Windows.Forms.Label
        Me.prefixNoRecu = New CI.Base.ManagedText
        Me.PageCliniquePrinting = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label65 = New System.Windows.Forms.Label
        Me.publipostageTopMargin = New CI.Base.ManagedText
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.publipostageSpacing = New CI.Base.ManagedText
        Me.Label58 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.printingFooter = New CI.Base.ManagedText
        Me.printingHeader = New CI.Base.ManagedText
        Me.PageCliniqueKP = New System.Windows.Forms.TabPage
        Me._Frames_3 = New System.Windows.Forms.GroupBox
        Me.copoc11 = New System.Windows.Forms.CheckBox
        Me.copoc9 = New System.Windows.Forms.CheckBox
        Me.copoc8 = New System.Windows.Forms.CheckBox
        Me.copoc5 = New System.Windows.Forms.CheckBox
        Me.copoc10 = New System.Windows.Forms.CheckBox
        Me.copoc7 = New System.Windows.Forms.CheckBox
        Me.copoc6 = New System.Windows.Forms.CheckBox
        Me.copoc4 = New System.Windows.Forms.CheckBox
        Me.copoc3 = New System.Windows.Forms.CheckBox
        Me.copoc1 = New System.Windows.Forms.CheckBox
        Me.copoc2 = New System.Windows.Forms.CheckBox
        Me.label7 = New System.Windows.Forms.Label
        Me.medecinCategorie = New CI.Base.ManagedText
        Me.PageCliniqueRendezvous = New System.Windows.Forms.TabPage
        Me.textForPlageBloquee = New CI.Base.ManagedText
        Me.nbJourForAutoQL = New CI.Base.ManagedText
        Me.GroupTriRV = New System.Windows.Forms.GroupBox
        Me.triRVCompte = New System.Windows.Forms.ComboBox
        Me.triRVFuturs = New System.Windows.Forms.ComboBox
        Me.label17 = New System.Windows.Forms.Label
        Me.label16 = New System.Windows.Forms.Label
        Me.ActiveFolderAutoOnRVStatusChange = New System.Windows.Forms.CheckBox
        Me.ShowQLOnAgendaRemove = New System.Windows.Forms.CheckBox
        Me.AutoDelQLAfterNewRV = New System.Windows.Forms.CheckBox
        Me.label39 = New System.Windows.Forms.Label
        Me.label36 = New System.Windows.Forms.Label
        Me.actionOnNoRV = New System.Windows.Forms.ComboBox
        Me.label26 = New System.Windows.Forms.Label
        Me.VerifyFrequenceForNewRV = New System.Windows.Forms.CheckBox
        Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant = New System.Windows.Forms.CheckBox
        Me.AllowToSkipAbsenceReasonInsertionToText = New System.Windows.Forms.CheckBox
        Me.AlertTRPOnRVAbsence = New System.Windows.Forms.CheckBox
        Me.ActiveFolderAutoOnRVAdding = New System.Windows.Forms.CheckBox
        Me.PageCliniqueUtilisateur = New System.Windows.Forms.TabPage
        Me.AdministratorPassword = New CI.Base.ManagedText
        Me.hideEndedUsersFolder = New System.Windows.Forms.CheckBox
        Me.nbEvalTo100TauxAutonomie = New CI.Base.ManagedText
        Me.Label76 = New System.Windows.Forms.Label
        Me.label47 = New System.Windows.Forms.Label
        Me.Services = New System.Windows.Forms.ListBox
        Me.affLastUserType = New System.Windows.Forms.CheckBox
        Me._Labels_0 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.lastUserType = New System.Windows.Forms.Label
        Me.AffAdminInAcces = New System.Windows.Forms.CheckBox
        Me.renamingService = New System.Windows.Forms.Button
        Me.AffDefCodesInSpecificTRP = New System.Windows.Forms.CheckBox
        Me.addingService = New System.Windows.Forms.Button
        Me.removingService = New System.Windows.Forms.Button
        Me.AllowUserEmptyPassword = New System.Windows.Forms.CheckBox
        Me.UserMDPRespectCasse = New System.Windows.Forms.CheckBox
        Me.AutoSelectLastUserInAcces = New System.Windows.Forms.CheckBox
        Me.FrameHidden = New System.Windows.Forms.GroupBox
        Me.ActivateWorkHoursApprobation = New System.Windows.Forms.CheckBox
        Me.label33 = New System.Windows.Forms.Label
        Me.punchModeArrival = New System.Windows.Forms.ComboBox
        Me.label34 = New System.Windows.Forms.Label
        Me.punchModeDeparture = New System.Windows.Forms.ComboBox
        Me.save = New System.Windows.Forms.Button
        Me.CDialog = New System.Windows.Forms.ColorDialog
        Me.FDialog = New System.Windows.Forms.FontDialog
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SelectSonMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AucunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ParDéfautToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DepuisLaBanqueDeDonnéesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectRemoteTaskDBPath = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NePasEnregistrerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VersLaBanqueDeDonnéesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.changeAbsenceTypeForSpecificText = New System.Windows.Forms.CheckBox
        Me.textToReplaceAbsNotMotivatedInReceipt = New CI.Base.ManagedText
        CType(Me.aListColors1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aListColors5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aListColors3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aListColors4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aListColors2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aListColors6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colors7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colors3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colors5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colors4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colors2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colors6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colors1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listColors7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listColors6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listColors5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listColors4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listColors3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listColors2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.listColors1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPref.SuspendLayout()
        Me._TabPref_TabPage0.SuspendLayout()
        Me.PagesUsers.SuspendLayout()
        Me.PageUserAgenda.SuspendLayout()
        Me.PageUserAutres.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupAutresStartup.SuspendLayout()
        Me.PageUserCompteclient.SuspendLayout()
        Me.PageMessagerie.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.AlertExpiries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PageUserRapport.SuspendLayout()
        Me.PageUserRendezvous.SuspendLayout()
        Me.PageUserSons.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me._TabPref_TabPage1.SuspendLayout()
        Me.PagesCliniques.SuspendLayout()
        Me.PageCliniqueAffichage.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.colorSpecialDates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._Frames_7.SuspendLayout()
        CType(Me.colorObjActivatedBySoftware, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._Frames_5.SuspendLayout()
        CType(Me.colorHoraireClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colorPresencePayee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colorHoraireOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colorBloquee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colorTitreFactureSouffrance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.FrameConfirmation.SuspendLayout()
        Me._Frames_4.SuspendLayout()
        Me.groupBox9.SuspendLayout()
        CType(Me.colorCommSent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colorCommReceived, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameColorsQL.SuspendLayout()
        CType(Me.colorWithoutRV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colorWithRV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PageCliniqueAutres.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.PageCliniqueBD.SuspendLayout()
        Me.groupAutoSaveRemoteTask.SuspendLayout()
        Me.PageCliniqueCompteclient.SuspendLayout()
        Me._Frames_0.SuspendLayout()
        Me.PageCliniqueFacturation.SuspendLayout()
        Me.frameComptabilite.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.FrameTriage.SuspendLayout()
        Me.PageCliniquePrinting.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.PageCliniqueKP.SuspendLayout()
        Me._Frames_3.SuspendLayout()
        Me.PageCliniqueRendezvous.SuspendLayout()
        Me.GroupTriRV.SuspendLayout()
        Me.PageCliniqueUtilisateur.SuspendLayout()
        Me.FrameHidden.SuspendLayout()
        Me.SelectSonMenu.SuspendLayout()
        Me.SelectRemoteTaskDBPath.SuspendLayout()
        Me.SuspendLayout()
        '
        'aListColors1
        '
        Me.aListColors1.BackColor = System.Drawing.SystemColors.Control
        Me.aListColors1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.aListColors1.Cursor = System.Windows.Forms.Cursors.Default
        Me.aListColors1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aListColors1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.aListColors1.Location = New System.Drawing.Point(66, 16)
        Me.aListColors1.Name = "aListColors1"
        Me.aListColors1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.aListColors1.Size = New System.Drawing.Size(25, 17)
        Me.aListColors1.TabIndex = 102
        Me.aListColors1.TabStop = False
        Me.aListColors1.Tag = "55"
        '
        'aListColors5
        '
        Me.aListColors5.BackColor = System.Drawing.SystemColors.Window
        Me.aListColors5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.aListColors5.Cursor = System.Windows.Forms.Cursors.Default
        Me.aListColors5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aListColors5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.aListColors5.Location = New System.Drawing.Point(217, 41)
        Me.aListColors5.Name = "aListColors5"
        Me.aListColors5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.aListColors5.Size = New System.Drawing.Size(25, 17)
        Me.aListColors5.TabIndex = 101
        Me.aListColors5.TabStop = False
        Me.aListColors5.Tag = "59"
        '
        'aListColors3
        '
        Me.aListColors3.BackColor = System.Drawing.SystemColors.Control
        Me.aListColors3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.aListColors3.Cursor = System.Windows.Forms.Cursors.Default
        Me.aListColors3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aListColors3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.aListColors3.Location = New System.Drawing.Point(305, 16)
        Me.aListColors3.Name = "aListColors3"
        Me.aListColors3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.aListColors3.Size = New System.Drawing.Size(25, 17)
        Me.aListColors3.TabIndex = 100
        Me.aListColors3.TabStop = False
        Me.aListColors3.Tag = "57"
        '
        'aListColors4
        '
        Me.aListColors4.BackColor = System.Drawing.Color.Black
        Me.aListColors4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.aListColors4.Cursor = System.Windows.Forms.Cursors.Default
        Me.aListColors4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aListColors4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.aListColors4.Location = New System.Drawing.Point(66, 40)
        Me.aListColors4.Name = "aListColors4"
        Me.aListColors4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.aListColors4.Size = New System.Drawing.Size(25, 17)
        Me.aListColors4.TabIndex = 99
        Me.aListColors4.TabStop = False
        Me.aListColors4.Tag = "58"
        '
        'aListColors2
        '
        Me.aListColors2.BackColor = System.Drawing.Color.Black
        Me.aListColors2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.aListColors2.Cursor = System.Windows.Forms.Cursors.Default
        Me.aListColors2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aListColors2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.aListColors2.Location = New System.Drawing.Point(217, 16)
        Me.aListColors2.Name = "aListColors2"
        Me.aListColors2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.aListColors2.Size = New System.Drawing.Size(25, 17)
        Me.aListColors2.TabIndex = 98
        Me.aListColors2.TabStop = False
        Me.aListColors2.Tag = "56"
        '
        'aListColors6
        '
        Me.aListColors6.BackColor = System.Drawing.Color.Black
        Me.aListColors6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.aListColors6.Cursor = System.Windows.Forms.Cursors.Default
        Me.aListColors6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aListColors6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.aListColors6.Location = New System.Drawing.Point(305, 41)
        Me.aListColors6.Name = "aListColors6"
        Me.aListColors6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.aListColors6.Size = New System.Drawing.Size(25, 17)
        Me.aListColors6.TabIndex = 97
        Me.aListColors6.TabStop = False
        Me.aListColors6.Tag = "60"
        '
        'colors7
        '
        Me.colors7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.colors7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colors7.Cursor = System.Windows.Forms.Cursors.Default
        Me.colors7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colors7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colors7.Location = New System.Drawing.Point(127, 85)
        Me.colors7.Name = "colors7"
        Me.colors7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colors7.Size = New System.Drawing.Size(25, 17)
        Me.colors7.TabIndex = 84
        Me.colors7.TabStop = False
        Me.colors7.Tag = "54"
        '
        'colors3
        '
        Me.colors3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.colors3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colors3.Cursor = System.Windows.Forms.Cursors.Default
        Me.colors3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colors3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colors3.Location = New System.Drawing.Point(305, 86)
        Me.colors3.Name = "colors3"
        Me.colors3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colors3.Size = New System.Drawing.Size(25, 17)
        Me.colors3.TabIndex = 82
        Me.colors3.TabStop = False
        Me.colors3.Tag = "50"
        '
        'colors5
        '
        Me.colors5.BackColor = System.Drawing.Color.Red
        Me.colors5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colors5.Cursor = System.Windows.Forms.Cursors.Default
        Me.colors5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colors5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colors5.Location = New System.Drawing.Point(127, 108)
        Me.colors5.Name = "colors5"
        Me.colors5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colors5.Size = New System.Drawing.Size(25, 17)
        Me.colors5.TabIndex = 80
        Me.colors5.TabStop = False
        Me.colors5.Tag = "52"
        '
        'colors4
        '
        Me.colors4.BackColor = System.Drawing.Color.LightGreen
        Me.colors4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colors4.Cursor = System.Windows.Forms.Cursors.Default
        Me.colors4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colors4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colors4.Location = New System.Drawing.Point(127, 39)
        Me.colors4.Name = "colors4"
        Me.colors4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colors4.Size = New System.Drawing.Size(25, 17)
        Me.colors4.TabIndex = 78
        Me.colors4.TabStop = False
        Me.colors4.Tag = "51"
        '
        'colors2
        '
        Me.colors2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.colors2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colors2.Cursor = System.Windows.Forms.Cursors.Default
        Me.colors2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colors2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colors2.Location = New System.Drawing.Point(127, 16)
        Me.colors2.Name = "colors2"
        Me.colors2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colors2.Size = New System.Drawing.Size(25, 17)
        Me.colors2.TabIndex = 76
        Me.colors2.TabStop = False
        Me.colors2.Tag = "49"
        '
        'colors6
        '
        Me.colors6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.colors6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colors6.Cursor = System.Windows.Forms.Cursors.Default
        Me.colors6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colors6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colors6.Location = New System.Drawing.Point(305, 16)
        Me.colors6.Name = "colors6"
        Me.colors6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colors6.Size = New System.Drawing.Size(25, 17)
        Me.colors6.TabIndex = 74
        Me.colors6.TabStop = False
        Me.colors6.Tag = "53"
        '
        'colors1
        '
        Me.colors1.BackColor = System.Drawing.SystemColors.Control
        Me.colors1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colors1.Cursor = System.Windows.Forms.Cursors.Default
        Me.colors1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colors1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colors1.Location = New System.Drawing.Point(305, 39)
        Me.colors1.Name = "colors1"
        Me.colors1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colors1.Size = New System.Drawing.Size(25, 17)
        Me.colors1.TabIndex = 71
        Me.colors1.TabStop = False
        Me.colors1.Tag = "48"
        '
        'listColors7
        '
        Me.listColors7.BackColor = System.Drawing.Color.Black
        Me.listColors7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listColors7.Cursor = System.Windows.Forms.Cursors.Default
        Me.listColors7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listColors7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listColors7.Location = New System.Drawing.Point(305, 150)
        Me.listColors7.Name = "listColors7"
        Me.listColors7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listColors7.Size = New System.Drawing.Size(25, 17)
        Me.listColors7.TabIndex = 69
        Me.listColors7.TabStop = False
        Me.listColors7.Tag = "43"
        '
        'listColors6
        '
        Me.listColors6.BackColor = System.Drawing.Color.Black
        Me.listColors6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listColors6.Cursor = System.Windows.Forms.Cursors.Default
        Me.listColors6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listColors6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listColors6.Location = New System.Drawing.Point(217, 149)
        Me.listColors6.Name = "listColors6"
        Me.listColors6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listColors6.Size = New System.Drawing.Size(25, 17)
        Me.listColors6.TabIndex = 67
        Me.listColors6.TabStop = False
        Me.listColors6.Tag = "42"
        '
        'listColors5
        '
        Me.listColors5.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.listColors5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listColors5.Cursor = System.Windows.Forms.Cursors.Default
        Me.listColors5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listColors5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listColors5.Location = New System.Drawing.Point(66, 149)
        Me.listColors5.Name = "listColors5"
        Me.listColors5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listColors5.Size = New System.Drawing.Size(25, 17)
        Me.listColors5.TabIndex = 65
        Me.listColors5.TabStop = False
        Me.listColors5.Tag = "41"
        '
        'listColors4
        '
        Me.listColors4.BackColor = System.Drawing.SystemColors.Control
        Me.listColors4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listColors4.Cursor = System.Windows.Forms.Cursors.Default
        Me.listColors4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listColors4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listColors4.Location = New System.Drawing.Point(305, 125)
        Me.listColors4.Name = "listColors4"
        Me.listColors4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listColors4.Size = New System.Drawing.Size(25, 17)
        Me.listColors4.TabIndex = 63
        Me.listColors4.TabStop = False
        Me.listColors4.Tag = "40"
        '
        'listColors3
        '
        Me.listColors3.BackColor = System.Drawing.SystemColors.Window
        Me.listColors3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listColors3.Cursor = System.Windows.Forms.Cursors.Default
        Me.listColors3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listColors3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listColors3.Location = New System.Drawing.Point(217, 125)
        Me.listColors3.Name = "listColors3"
        Me.listColors3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listColors3.Size = New System.Drawing.Size(25, 17)
        Me.listColors3.TabIndex = 61
        Me.listColors3.TabStop = False
        Me.listColors3.Tag = "39"
        '
        'listColors2
        '
        Me.listColors2.BackColor = System.Drawing.SystemColors.Window
        Me.listColors2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listColors2.Cursor = System.Windows.Forms.Cursors.Default
        Me.listColors2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listColors2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listColors2.Location = New System.Drawing.Point(134, 125)
        Me.listColors2.Name = "listColors2"
        Me.listColors2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listColors2.Size = New System.Drawing.Size(25, 17)
        Me.listColors2.TabIndex = 59
        Me.listColors2.TabStop = False
        Me.listColors2.Tag = "38"
        '
        'listColors1
        '
        Me.listColors1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.listColors1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listColors1.Cursor = System.Windows.Forms.Cursors.Default
        Me.listColors1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listColors1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listColors1.Location = New System.Drawing.Point(66, 125)
        Me.listColors1.Name = "listColors1"
        Me.listColors1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listColors1.Size = New System.Drawing.Size(25, 17)
        Me.listColors1.TabIndex = 57
        Me.listColors1.TabStop = False
        Me.listColors1.Tag = "37"
        '
        'tabPref
        '
        Me.tabPref.Controls.Add(Me._TabPref_TabPage0)
        Me.tabPref.Controls.Add(Me._TabPref_TabPage1)
        Me.tabPref.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPref.ItemSize = New System.Drawing.Size(42, 18)
        Me.tabPref.Location = New System.Drawing.Point(12, 12)
        Me.tabPref.Name = "tabPref"
        Me.tabPref.SelectedIndex = 0
        Me.tabPref.Size = New System.Drawing.Size(697, 377)
        Me.tabPref.TabIndex = 1
        '
        '_TabPref_TabPage0
        '
        Me._TabPref_TabPage0.AutoScroll = True
        Me._TabPref_TabPage0.Controls.Add(Me.PagesUsers)
        Me._TabPref_TabPage0.Controls.Add(Me.nbPrefU)
        Me._TabPref_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._TabPref_TabPage0.Name = "_TabPref_TabPage0"
        Me._TabPref_TabPage0.Size = New System.Drawing.Size(689, 351)
        Me._TabPref_TabPage0.TabIndex = 0
        Me._TabPref_TabPage0.Tag = ""
        Me._TabPref_TabPage0.Text = "Utilisateurs"
        Me._TabPref_TabPage0.UseVisualStyleBackColor = True
        '
        'PagesUsers
        '
        Me.PagesUsers.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.PagesUsers.Controls.Add(Me.PageUserAgenda)
        Me.PagesUsers.Controls.Add(Me.PageUserAutres)
        Me.PagesUsers.Controls.Add(Me.PageUserCompteclient)
        Me.PagesUsers.Controls.Add(Me.PageMessagerie)
        Me.PagesUsers.Controls.Add(Me.PageUserRapport)
        Me.PagesUsers.Controls.Add(Me.PageUserRendezvous)
        Me.PagesUsers.Controls.Add(Me.PageUserSons)
        Me.PagesUsers.Location = New System.Drawing.Point(0, 0)
        Me.PagesUsers.Name = "PagesUsers"
        Me.PagesUsers.SelectedIndex = 0
        Me.PagesUsers.Size = New System.Drawing.Size(689, 348)
        Me.PagesUsers.TabIndex = 128
        '
        'PageUserAgenda
        '
        Me.PageUserAgenda.Controls.Add(Me.openingAgendas)
        Me.PageUserAgenda.Controls.Add(Me.agendaFontSize)
        Me.PageUserAgenda.Controls.Add(Me._Labels_21)
        Me.PageUserAgenda.Controls.Add(Me.infoBulleClient)
        Me.PageUserAgenda.Controls.Add(Me.infoBulleDate)
        Me.PageUserAgenda.Controls.Add(Me._Labels_23)
        Me.PageUserAgenda.Controls.Add(Me.firstDay)
        Me.PageUserAgenda.Controls.Add(Me._Labels_24)
        Me.PageUserAgenda.Controls.Add(Me.startingPeriode)
        Me.PageUserAgenda.Controls.Add(Me._Labels_25)
        Me.PageUserAgenda.Controls.Add(Me._Labels_22)
        Me.PageUserAgenda.Controls.Add(Me.defaultTRP)
        Me.PageUserAgenda.Controls.Add(Me.questionChoosingFolder)
        Me.PageUserAgenda.Controls.Add(Me.questionLastNewFolder)
        Me.PageUserAgenda.Location = New System.Drawing.Point(4, 26)
        Me.PageUserAgenda.Name = "PageUserAgenda"
        Me.PageUserAgenda.Padding = New System.Windows.Forms.Padding(3)
        Me.PageUserAgenda.Size = New System.Drawing.Size(681, 318)
        Me.PageUserAgenda.TabIndex = 0
        Me.PageUserAgenda.Text = "Agenda"
        Me.PageUserAgenda.UseVisualStyleBackColor = True
        '
        'openingAgendas
        '
        Me.openingAgendas.BackColor = System.Drawing.SystemColors.Window
        Me.openingAgendas.Cursor = System.Windows.Forms.Cursors.Default
        Me.openingAgendas.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openingAgendas.ForeColor = System.Drawing.SystemColors.WindowText
        Me.openingAgendas.Location = New System.Drawing.Point(6, 19)
        Me.openingAgendas.Name = "openingAgendas"
        Me.openingAgendas.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.openingAgendas.Size = New System.Drawing.Size(307, 184)
        Me.openingAgendas.Sorted = True
        Me.openingAgendas.TabIndex = 85
        Me.openingAgendas.Tag = "0"
        '
        'agendaFontSize
        '
        Me.agendaFontSize.AcceptsReturn = True
        Me.agendaFontSize.BackColor = System.Drawing.SystemColors.Window
        Me.agendaFontSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.agendaFontSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.agendaFontSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.agendaFontSize.Location = New System.Drawing.Point(99, 209)
        Me.agendaFontSize.MaxLength = 2
        Me.agendaFontSize.Name = "agendaFontSize"
        Me.agendaFontSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.agendaFontSize.Size = New System.Drawing.Size(25, 20)
        Me.agendaFontSize.TabIndex = 95
        Me.agendaFontSize.Tag = "4"
        Me.agendaFontSize.Text = "8"
        '
        '_Labels_21
        '
        Me._Labels_21.AutoSize = True
        Me._Labels_21.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_21.Location = New System.Drawing.Point(6, 3)
        Me._Labels_21.Name = "_Labels_21"
        Me._Labels_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_21.Size = New System.Drawing.Size(161, 14)
        Me._Labels_21.TabIndex = 86
        Me._Labels_21.Text = "Agenda à ouvrir au démarrage :"
        '
        'infoBulleClient
        '
        Me.infoBulleClient.BackColor = System.Drawing.Color.Transparent
        Me.infoBulleClient.Cursor = System.Windows.Forms.Cursors.Default
        Me.infoBulleClient.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.infoBulleClient.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoBulleClient.Location = New System.Drawing.Point(6, 251)
        Me.infoBulleClient.Name = "infoBulleClient"
        Me.infoBulleClient.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.infoBulleClient.Size = New System.Drawing.Size(324, 19)
        Me.infoBulleClient.TabIndex = 110
        Me.infoBulleClient.Tag = "6"
        Me.infoBulleClient.Text = "Ne pas afficher l'info-bulle d'une plage avec un rendez-vous"
        Me.infoBulleClient.UseVisualStyleBackColor = False
        '
        'infoBulleDate
        '
        Me.infoBulleDate.BackColor = System.Drawing.SystemColors.Control
        Me.infoBulleDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.infoBulleDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.infoBulleDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoBulleDate.Location = New System.Drawing.Point(6, 235)
        Me.infoBulleDate.Name = "infoBulleDate"
        Me.infoBulleDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.infoBulleDate.Size = New System.Drawing.Size(312, 17)
        Me.infoBulleDate.TabIndex = 109
        Me.infoBulleDate.Tag = "5"
        Me.infoBulleDate.Text = "Ne pas afficher l'info-bulle d'une plage sans rendez-vous"
        Me.infoBulleDate.UseVisualStyleBackColor = False
        '
        '_Labels_23
        '
        Me._Labels_23.AutoSize = True
        Me._Labels_23.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_23.Location = New System.Drawing.Point(322, 3)
        Me._Labels_23.Name = "_Labels_23"
        Me._Labels_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_23.Size = New System.Drawing.Size(119, 14)
        Me._Labels_23.TabIndex = 90
        Me._Labels_23.Text = "Période au démarrage :"
        '
        'firstDay
        '
        Me.firstDay.BackColor = System.Drawing.SystemColors.Window
        Me.firstDay.Cursor = System.Windows.Forms.Cursors.Default
        Me.firstDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.firstDay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.firstDay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.firstDay.Items.AddRange(New Object() {"Aujourd'hui", "Dimanche", "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi"})
        Me.firstDay.Location = New System.Drawing.Point(325, 63)
        Me.firstDay.Name = "firstDay"
        Me.firstDay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.firstDay.Size = New System.Drawing.Size(182, 22)
        Me.firstDay.TabIndex = 93
        Me.firstDay.Tag = "3"
        '
        '_Labels_24
        '
        Me._Labels_24.AutoSize = True
        Me._Labels_24.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_24.Location = New System.Drawing.Point(322, 47)
        Me._Labels_24.Name = "_Labels_24"
        Me._Labels_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_24.Size = New System.Drawing.Size(219, 14)
        Me._Labels_24.TabIndex = 92
        Me._Labels_24.Text = "Première journée à l'ouverture d'un agenda :"
        '
        'startingPeriode
        '
        Me.startingPeriode.BackColor = System.Drawing.SystemColors.Window
        Me.startingPeriode.Cursor = System.Windows.Forms.Cursors.Default
        Me.startingPeriode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.startingPeriode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startingPeriode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.startingPeriode.Items.AddRange(New Object() {"Jour : 1", "Jour : 2", "Jour : 3", "Jour : 4", "Jour : 5", "Jour : 6", "Mois", "Semaine : 1", "Semaine : 2", "Semaine : 3", "Semaine : 4"})
        Me.startingPeriode.Location = New System.Drawing.Point(325, 20)
        Me.startingPeriode.Name = "startingPeriode"
        Me.startingPeriode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.startingPeriode.Size = New System.Drawing.Size(182, 22)
        Me.startingPeriode.TabIndex = 91
        Me.startingPeriode.Tag = "2"
        '
        '_Labels_25
        '
        Me._Labels_25.AutoSize = True
        Me._Labels_25.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_25.Location = New System.Drawing.Point(3, 212)
        Me._Labels_25.Name = "_Labels_25"
        Me._Labels_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_25.Size = New System.Drawing.Size(93, 14)
        Me._Labels_25.TabIndex = 94
        Me._Labels_25.Text = "Taille de la police :"
        '
        '_Labels_22
        '
        Me._Labels_22.AutoSize = True
        Me._Labels_22.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_22.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_22.Location = New System.Drawing.Point(322, 90)
        Me._Labels_22.Name = "_Labels_22"
        Me._Labels_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_22.Size = New System.Drawing.Size(179, 14)
        Me._Labels_22.TabIndex = 88
        Me._Labels_22.Text = "Thérapeute sélectionné par défaut :"
        '
        'defaultTRP
        '
        Me.defaultTRP.BackColor = System.Drawing.SystemColors.Window
        Me.defaultTRP.Cursor = System.Windows.Forms.Cursors.Default
        Me.defaultTRP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.defaultTRP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.defaultTRP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.defaultTRP.Location = New System.Drawing.Point(325, 107)
        Me.defaultTRP.Name = "defaultTRP"
        Me.defaultTRP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.defaultTRP.Size = New System.Drawing.Size(182, 22)
        Me.defaultTRP.TabIndex = 87
        Me.defaultTRP.Tag = "1"
        '
        'questionChoosingFolder
        '
        Me.questionChoosingFolder.BackColor = System.Drawing.Color.Transparent
        Me.questionChoosingFolder.Checked = True
        Me.questionChoosingFolder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.questionChoosingFolder.Cursor = System.Windows.Forms.Cursors.Default
        Me.questionChoosingFolder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.questionChoosingFolder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.questionChoosingFolder.Location = New System.Drawing.Point(6, 292)
        Me.questionChoosingFolder.Name = "questionChoosingFolder"
        Me.questionChoosingFolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.questionChoosingFolder.Size = New System.Drawing.Size(435, 20)
        Me.questionChoosingFolder.TabIndex = 111
        Me.questionChoosingFolder.Tag = "24"
        Me.questionChoosingFolder.Text = "Insérer automatiquement le rendez-vous collé dans le dossier du rendez-vous copié" & _
            ""
        Me.questionChoosingFolder.UseVisualStyleBackColor = False
        '
        'questionLastNewFolder
        '
        Me.questionLastNewFolder.BackColor = System.Drawing.Color.Transparent
        Me.questionLastNewFolder.Checked = True
        Me.questionLastNewFolder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.questionLastNewFolder.Cursor = System.Windows.Forms.Cursors.Default
        Me.questionLastNewFolder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.questionLastNewFolder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.questionLastNewFolder.Location = New System.Drawing.Point(6, 276)
        Me.questionLastNewFolder.Name = "questionLastNewFolder"
        Me.questionLastNewFolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.questionLastNewFolder.Size = New System.Drawing.Size(376, 17)
        Me.questionLastNewFolder.TabIndex = 111
        Me.questionLastNewFolder.Tag = "7"
        Me.questionLastNewFolder.Text = "Insérer automatiquement le rendez-vous collé dans l'unique dossier actif du clien" & _
            "t"
        Me.questionLastNewFolder.UseVisualStyleBackColor = False
        '
        'PageUserAutres
        '
        Me.PageUserAutres.Controls.Add(Me.GroupBox8)
        Me.PageUserAutres.Controls.Add(Me.GroupAutresStartup)
        Me.PageUserAutres.Controls.Add(Me.affMSGModif)
        Me.PageUserAutres.Controls.Add(Me.autoCloseSideBarOnTransfer)
        Me.PageUserAutres.Controls.Add(Me.autoOpenSideBarOnTransfer)
        Me.PageUserAutres.Controls.Add(Me.activateNumLockOnStartup)
        Me.PageUserAutres.Controls.Add(Me.autoScrollHiddenBox)
        Me.PageUserAutres.Controls.Add(Me.label19)
        Me.PageUserAutres.Controls.Add(Me.importOriginFileAction)
        Me.PageUserAutres.Location = New System.Drawing.Point(4, 25)
        Me.PageUserAutres.Name = "PageUserAutres"
        Me.PageUserAutres.Padding = New System.Windows.Forms.Padding(3)
        Me.PageUserAutres.Size = New System.Drawing.Size(681, 319)
        Me.PageUserAutres.TabIndex = 1
        Me.PageUserAutres.Text = "Autres"
        Me.PageUserAutres.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.label23)
        Me.GroupBox8.Controls.Add(Me.nbMaxWindowsListBox)
        Me.GroupBox8.Controls.Add(Me.showExtraInfosInWindowsListBox)
        Me.GroupBox8.Location = New System.Drawing.Point(320, 0)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(361, 57)
        Me.GroupBox8.TabIndex = 129
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Liste déroulante des fenêtres"
        '
        'label23
        '
        Me.label23.AutoSize = True
        Me.label23.BackColor = System.Drawing.SystemColors.Control
        Me.label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.label23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label23.Location = New System.Drawing.Point(6, 16)
        Me.label23.Name = "label23"
        Me.label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label23.Size = New System.Drawing.Size(314, 14)
        Me.label23.TabIndex = 124
        Me.label23.Text = "Nombre de fenêtres maximales avant défilement (approximatif) :"
        '
        'nbMaxWindowsListBox
        '
        Me.nbMaxWindowsListBox.acceptAlpha = False
        Me.nbMaxWindowsListBox.acceptedChars = ""
        Me.nbMaxWindowsListBox.acceptNumeric = True
        Me.nbMaxWindowsListBox.allCapital = False
        Me.nbMaxWindowsListBox.allLower = False
        Me.nbMaxWindowsListBox.BackColor = System.Drawing.SystemColors.Window
        Me.nbMaxWindowsListBox.blockOnMaximum = True
        Me.nbMaxWindowsListBox.blockOnMinimum = True
        Me.nbMaxWindowsListBox.cb_AcceptLeftZeros = False
        Me.nbMaxWindowsListBox.cb_AcceptNegative = False
        Me.nbMaxWindowsListBox.currencyBox = True
        Me.nbMaxWindowsListBox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nbMaxWindowsListBox.firstLetterCapital = False
        Me.nbMaxWindowsListBox.firstLettersCapital = False
        Me.nbMaxWindowsListBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbMaxWindowsListBox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nbMaxWindowsListBox.Location = New System.Drawing.Point(323, 13)
        Me.nbMaxWindowsListBox.manageText = True
        Me.nbMaxWindowsListBox.matchExp = ""
        Me.nbMaxWindowsListBox.maximum = 20
        Me.nbMaxWindowsListBox.MaxLength = 0
        Me.nbMaxWindowsListBox.minimum = 1
        Me.nbMaxWindowsListBox.Name = "nbMaxWindowsListBox"
        Me.nbMaxWindowsListBox.nbDecimals = CType(0, Short)
        Me.nbMaxWindowsListBox.onlyAlphabet = False
        Me.nbMaxWindowsListBox.refuseAccents = False
        Me.nbMaxWindowsListBox.refusedChars = ""
        Me.nbMaxWindowsListBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbMaxWindowsListBox.showInternalContextMenu = True
        Me.nbMaxWindowsListBox.Size = New System.Drawing.Size(32, 20)
        Me.nbMaxWindowsListBox.TabIndex = 126
        Me.nbMaxWindowsListBox.Tag = "17"
        Me.nbMaxWindowsListBox.Text = "10"
        Me.nbMaxWindowsListBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nbMaxWindowsListBox.trimText = False
        '
        'showExtraInfosInWindowsListBox
        '
        Me.showExtraInfosInWindowsListBox.BackColor = System.Drawing.Color.Transparent
        Me.showExtraInfosInWindowsListBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.showExtraInfosInWindowsListBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.showExtraInfosInWindowsListBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.showExtraInfosInWindowsListBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.showExtraInfosInWindowsListBox.Location = New System.Drawing.Point(5, 35)
        Me.showExtraInfosInWindowsListBox.Name = "showExtraInfosInWindowsListBox"
        Me.showExtraInfosInWindowsListBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.showExtraInfosInWindowsListBox.Size = New System.Drawing.Size(350, 16)
        Me.showExtraInfosInWindowsListBox.TabIndex = 127
        Me.showExtraInfosInWindowsListBox.Tag = "20"
        Me.showExtraInfosInWindowsListBox.Text = "Afficher les informations complémentaires des fenêtres"
        Me.showExtraInfosInWindowsListBox.UseVisualStyleBackColor = False
        '
        'GroupAutresStartup
        '
        Me.GroupAutresStartup.Controls.Add(Me.label27)
        Me.GroupAutresStartup.Controls.Add(Me.label35)
        Me.GroupAutresStartup.Controls.Add(Me.label28)
        Me.GroupAutresStartup.Controls.Add(Me.openPunch)
        Me.GroupAutresStartup.Controls.Add(Me.openFuturRV)
        Me.GroupAutresStartup.Controls.Add(Me.openInstantMSG)
        Me.GroupAutresStartup.Location = New System.Drawing.Point(6, 186)
        Me.GroupAutresStartup.Name = "GroupAutresStartup"
        Me.GroupAutresStartup.Size = New System.Drawing.Size(389, 106)
        Me.GroupAutresStartup.TabIndex = 128
        Me.GroupAutresStartup.TabStop = False
        Me.GroupAutresStartup.Text = "Au démarrage du logiciel"
        '
        'label27
        '
        Me.label27.AutoSize = True
        Me.label27.BackColor = System.Drawing.SystemColors.Control
        Me.label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.label27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label27.Location = New System.Drawing.Point(6, 16)
        Me.label27.Name = "label27"
        Me.label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label27.Size = New System.Drawing.Size(180, 14)
        Me.label27.TabIndex = 116
        Me.label27.Text = "Ouverture des rendez-vous futurs :"
        '
        'label35
        '
        Me.label35.AutoSize = True
        Me.label35.BackColor = System.Drawing.SystemColors.Control
        Me.label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.label35.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label35.Location = New System.Drawing.Point(6, 100)
        Me.label35.Name = "label35"
        Me.label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label35.Size = New System.Drawing.Size(140, 14)
        Me.label35.TabIndex = 116
        Me.label35.Text = "Ouverture du 'punch' virtuel"
        Me.label35.Visible = False
        '
        'label28
        '
        Me.label28.AutoSize = True
        Me.label28.BackColor = System.Drawing.SystemColors.Control
        Me.label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.label28.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label28.Location = New System.Drawing.Point(6, 58)
        Me.label28.Name = "label28"
        Me.label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label28.Size = New System.Drawing.Size(195, 14)
        Me.label28.TabIndex = 116
        Me.label28.Text = "Ouverture des messages instantanés :"
        '
        'openPunch
        '
        Me.openPunch.BackColor = System.Drawing.SystemColors.Window
        Me.openPunch.Cursor = System.Windows.Forms.Cursors.Default
        Me.openPunch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.openPunch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openPunch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.openPunch.Items.AddRange(New Object() {"Rouvrir si ouvert à la fermeture", "Ouvrir", "Ne pas ouvrir"})
        Me.openPunch.Location = New System.Drawing.Point(9, 117)
        Me.openPunch.Name = "openPunch"
        Me.openPunch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.openPunch.Size = New System.Drawing.Size(208, 22)
        Me.openPunch.TabIndex = 115
        Me.openPunch.Tag = "21"
        Me.openPunch.Visible = False
        '
        'openFuturRV
        '
        Me.openFuturRV.BackColor = System.Drawing.SystemColors.Window
        Me.openFuturRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.openFuturRV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.openFuturRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openFuturRV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.openFuturRV.Items.AddRange(New Object() {"Rouvrir si ouvert à la fermeture", "Ouvrir", "Ne pas ouvrir"})
        Me.openFuturRV.Location = New System.Drawing.Point(9, 33)
        Me.openFuturRV.Name = "openFuturRV"
        Me.openFuturRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.openFuturRV.Size = New System.Drawing.Size(208, 22)
        Me.openFuturRV.TabIndex = 115
        Me.openFuturRV.Tag = "18"
        '
        'openInstantMSG
        '
        Me.openInstantMSG.BackColor = System.Drawing.SystemColors.Window
        Me.openInstantMSG.Cursor = System.Windows.Forms.Cursors.Default
        Me.openInstantMSG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.openInstantMSG.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openInstantMSG.ForeColor = System.Drawing.SystemColors.WindowText
        Me.openInstantMSG.Items.AddRange(New Object() {"Rouvrir si ouvert à la fermeture", "Ouvrir", "Ne pas ouvrir"})
        Me.openInstantMSG.Location = New System.Drawing.Point(9, 75)
        Me.openInstantMSG.Name = "openInstantMSG"
        Me.openInstantMSG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.openInstantMSG.Size = New System.Drawing.Size(208, 22)
        Me.openInstantMSG.TabIndex = 115
        Me.openInstantMSG.Tag = "19"
        '
        'affMSGModif
        '
        Me.affMSGModif.BackColor = System.Drawing.Color.Transparent
        Me.affMSGModif.Checked = True
        Me.affMSGModif.CheckState = System.Windows.Forms.CheckState.Checked
        Me.affMSGModif.Cursor = System.Windows.Forms.Cursors.Default
        Me.affMSGModif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affMSGModif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affMSGModif.Location = New System.Drawing.Point(15, 6)
        Me.affMSGModif.Name = "affMSGModif"
        Me.affMSGModif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.affMSGModif.Size = New System.Drawing.Size(260, 16)
        Me.affMSGModif.TabIndex = 125
        Me.affMSGModif.Tag = "16"
        Me.affMSGModif.Text = "Afficher les messages de modification en cours"
        Me.affMSGModif.UseVisualStyleBackColor = False
        '
        'autoCloseSideBarOnTransfer
        '
        Me.autoCloseSideBarOnTransfer.AutoSize = True
        Me.autoCloseSideBarOnTransfer.BackColor = System.Drawing.Color.Transparent
        Me.autoCloseSideBarOnTransfer.Checked = True
        Me.autoCloseSideBarOnTransfer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.autoCloseSideBarOnTransfer.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoCloseSideBarOnTransfer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoCloseSideBarOnTransfer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoCloseSideBarOnTransfer.Location = New System.Drawing.Point(15, 80)
        Me.autoCloseSideBarOnTransfer.Name = "autoCloseSideBarOnTransfer"
        Me.autoCloseSideBarOnTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoCloseSideBarOnTransfer.Size = New System.Drawing.Size(462, 18)
        Me.autoCloseSideBarOnTransfer.TabIndex = 127
        Me.autoCloseSideBarOnTransfer.Tag = "28"
        Me.autoCloseSideBarOnTransfer.Text = "Fermer automatiquement la barre de côté lorsqu'un objet principal y est sorti par" & _
            " l'utilisateur"
        Me.autoCloseSideBarOnTransfer.UseVisualStyleBackColor = False
        '
        'autoOpenSideBarOnTransfer
        '
        Me.autoOpenSideBarOnTransfer.AutoSize = True
        Me.autoOpenSideBarOnTransfer.BackColor = System.Drawing.Color.Transparent
        Me.autoOpenSideBarOnTransfer.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoOpenSideBarOnTransfer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoOpenSideBarOnTransfer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoOpenSideBarOnTransfer.Location = New System.Drawing.Point(15, 63)
        Me.autoOpenSideBarOnTransfer.Name = "autoOpenSideBarOnTransfer"
        Me.autoOpenSideBarOnTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoOpenSideBarOnTransfer.Size = New System.Drawing.Size(482, 18)
        Me.autoOpenSideBarOnTransfer.TabIndex = 127
        Me.autoOpenSideBarOnTransfer.Tag = "27"
        Me.autoOpenSideBarOnTransfer.Text = "Ouvrir automatiquement la barre de côté lorsqu'un objet principal y est transféré" & _
            " par l'utilisateur"
        Me.autoOpenSideBarOnTransfer.UseVisualStyleBackColor = False
        '
        'activateNumLockOnStartup
        '
        Me.activateNumLockOnStartup.AutoSize = True
        Me.activateNumLockOnStartup.BackColor = System.Drawing.Color.Transparent
        Me.activateNumLockOnStartup.Cursor = System.Windows.Forms.Cursors.Default
        Me.activateNumLockOnStartup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.activateNumLockOnStartup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.activateNumLockOnStartup.Location = New System.Drawing.Point(15, 114)
        Me.activateNumLockOnStartup.Name = "activateNumLockOnStartup"
        Me.activateNumLockOnStartup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.activateNumLockOnStartup.Size = New System.Drawing.Size(392, 18)
        Me.activateNumLockOnStartup.TabIndex = 127
        Me.activateNumLockOnStartup.Tag = "43"
        Me.activateNumLockOnStartup.Text = "Activer la touche ""Verrouiller le clavier"" (""Numlock"") au démarrage de Clinica"
        Me.activateNumLockOnStartup.UseVisualStyleBackColor = False
        '
        'autoScrollHiddenBox
        '
        Me.autoScrollHiddenBox.BackColor = System.Drawing.Color.Transparent
        Me.autoScrollHiddenBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoScrollHiddenBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoScrollHiddenBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoScrollHiddenBox.Location = New System.Drawing.Point(15, 41)
        Me.autoScrollHiddenBox.Name = "autoScrollHiddenBox"
        Me.autoScrollHiddenBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoScrollHiddenBox.Size = New System.Drawing.Size(240, 16)
        Me.autoScrollHiddenBox.TabIndex = 127
        Me.autoScrollHiddenBox.Tag = "20"
        Me.autoScrollHiddenBox.Text = "Défilement automatique des boîtes cachées"
        Me.autoScrollHiddenBox.UseVisualStyleBackColor = False
        '
        'label19
        '
        Me.label19.AutoSize = True
        Me.label19.BackColor = System.Drawing.SystemColors.Control
        Me.label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.label19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label19.Location = New System.Drawing.Point(402, 186)
        Me.label19.Name = "label19"
        Me.label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label19.Size = New System.Drawing.Size(255, 14)
        Me.label19.TabIndex = 120
        Me.label19.Text = "Action sur le fichier d'origine lors d'une importation :"
        '
        'importOriginFileAction
        '
        Me.importOriginFileAction.BackColor = System.Drawing.SystemColors.Window
        Me.importOriginFileAction.Cursor = System.Windows.Forms.Cursors.Default
        Me.importOriginFileAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.importOriginFileAction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.importOriginFileAction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.importOriginFileAction.Items.AddRange(New Object() {"Demander quoi faire", "Supprimer le fichier", "Laisser le fichier intact"})
        Me.importOriginFileAction.Location = New System.Drawing.Point(404, 203)
        Me.importOriginFileAction.Name = "importOriginFileAction"
        Me.importOriginFileAction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.importOriginFileAction.Size = New System.Drawing.Size(208, 22)
        Me.importOriginFileAction.TabIndex = 119
        Me.importOriginFileAction.Tag = "13"
        '
        'PageUserCompteclient
        '
        Me.PageUserCompteclient.Controls.Add(Me.autoCloseSearchClientOnOpenAccount)
        Me.PageUserCompteclient.Controls.Add(Me.checkForMissingInfos)
        Me.PageUserCompteclient.Controls.Add(Me.openClientAccountOnNewFolderWORV)
        Me.PageUserCompteclient.Controls.Add(Me.openClientAccountOnFolderTransfer)
        Me.PageUserCompteclient.Controls.Add(Me.autoSelectFolderInRV)
        Me.PageUserCompteclient.Controls.Add(Me.label20)
        Me.PageUserCompteclient.Controls.Add(Me.label60)
        Me.PageUserCompteclient.Controls.Add(Me.label21)
        Me.PageUserCompteclient.Controls.Add(Me.dossierTexteAutoSel)
        Me.PageUserCompteclient.Controls.Add(Me.clientTabAutoSelect)
        Me.PageUserCompteclient.Controls.Add(Me.dossierTabAutoSelect)
        Me.PageUserCompteclient.Location = New System.Drawing.Point(4, 25)
        Me.PageUserCompteclient.Name = "PageUserCompteclient"
        Me.PageUserCompteclient.Size = New System.Drawing.Size(681, 319)
        Me.PageUserCompteclient.TabIndex = 2
        Me.PageUserCompteclient.Text = "Compte client"
        Me.PageUserCompteclient.UseVisualStyleBackColor = True
        '
        'autoCloseSearchClientOnOpenAccount
        '
        Me.autoCloseSearchClientOnOpenAccount.AutoSize = True
        Me.autoCloseSearchClientOnOpenAccount.BackColor = System.Drawing.Color.Transparent
        Me.autoCloseSearchClientOnOpenAccount.Checked = True
        Me.autoCloseSearchClientOnOpenAccount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.autoCloseSearchClientOnOpenAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoCloseSearchClientOnOpenAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoCloseSearchClientOnOpenAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoCloseSearchClientOnOpenAccount.Location = New System.Drawing.Point(6, 208)
        Me.autoCloseSearchClientOnOpenAccount.Name = "autoCloseSearchClientOnOpenAccount"
        Me.autoCloseSearchClientOnOpenAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoCloseSearchClientOnOpenAccount.Size = New System.Drawing.Size(432, 18)
        Me.autoCloseSearchClientOnOpenAccount.TabIndex = 127
        Me.autoCloseSearchClientOnOpenAccount.Tag = "42"
        Me.autoCloseSearchClientOnOpenAccount.Text = "Fermer la fenêtre de recherche automatiquement lors de l'ouverture du compte clie" & _
            "nt"
        Me.autoCloseSearchClientOnOpenAccount.UseVisualStyleBackColor = False
        '
        'checkForMissingInfos
        '
        Me.checkForMissingInfos.AutoSize = True
        Me.checkForMissingInfos.BackColor = System.Drawing.Color.Transparent
        Me.checkForMissingInfos.Checked = True
        Me.checkForMissingInfos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkForMissingInfos.Cursor = System.Windows.Forms.Cursors.Default
        Me.checkForMissingInfos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.checkForMissingInfos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.checkForMissingInfos.Location = New System.Drawing.Point(6, 184)
        Me.checkForMissingInfos.Name = "checkForMissingInfos"
        Me.checkForMissingInfos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.checkForMissingInfos.Size = New System.Drawing.Size(494, 18)
        Me.checkForMissingInfos.TabIndex = 127
        Me.checkForMissingInfos.Tag = "41"
        Me.checkForMissingInfos.Text = "Afficher les messages indiquant les informations manquante lors de l'enregistreme" & _
            "nt d'un dossier"
        Me.checkForMissingInfos.UseVisualStyleBackColor = False
        '
        'openClientAccountOnNewFolderWORV
        '
        Me.openClientAccountOnNewFolderWORV.AutoSize = True
        Me.openClientAccountOnNewFolderWORV.BackColor = System.Drawing.Color.Transparent
        Me.openClientAccountOnNewFolderWORV.Checked = True
        Me.openClientAccountOnNewFolderWORV.CheckState = System.Windows.Forms.CheckState.Checked
        Me.openClientAccountOnNewFolderWORV.Cursor = System.Windows.Forms.Cursors.Default
        Me.openClientAccountOnNewFolderWORV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openClientAccountOnNewFolderWORV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.openClientAccountOnNewFolderWORV.Location = New System.Drawing.Point(6, 160)
        Me.openClientAccountOnNewFolderWORV.Name = "openClientAccountOnNewFolderWORV"
        Me.openClientAccountOnNewFolderWORV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.openClientAccountOnNewFolderWORV.Size = New System.Drawing.Size(437, 18)
        Me.openClientAccountOnNewFolderWORV.TabIndex = 127
        Me.openClientAccountOnNewFolderWORV.Tag = "40"
        Me.openClientAccountOnNewFolderWORV.Text = "Ouvrir automatiquement le compte client dont un dossier est ajouté sans rendez-vo" & _
            "us"
        Me.openClientAccountOnNewFolderWORV.UseVisualStyleBackColor = False
        '
        'openClientAccountOnFolderTransfer
        '
        Me.openClientAccountOnFolderTransfer.AutoSize = True
        Me.openClientAccountOnFolderTransfer.BackColor = System.Drawing.Color.Transparent
        Me.openClientAccountOnFolderTransfer.Checked = True
        Me.openClientAccountOnFolderTransfer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.openClientAccountOnFolderTransfer.Cursor = System.Windows.Forms.Cursors.Default
        Me.openClientAccountOnFolderTransfer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openClientAccountOnFolderTransfer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.openClientAccountOnFolderTransfer.Location = New System.Drawing.Point(6, 137)
        Me.openClientAccountOnFolderTransfer.Name = "openClientAccountOnFolderTransfer"
        Me.openClientAccountOnFolderTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.openClientAccountOnFolderTransfer.Size = New System.Drawing.Size(361, 18)
        Me.openClientAccountOnFolderTransfer.TabIndex = 127
        Me.openClientAccountOnFolderTransfer.Tag = "40"
        Me.openClientAccountOnFolderTransfer.Text = "Ouvrir automatiquement le compte client dont un dossier est transféré"
        Me.openClientAccountOnFolderTransfer.UseVisualStyleBackColor = False
        '
        'autoSelectFolderInRV
        '
        Me.autoSelectFolderInRV.BackColor = System.Drawing.Color.Transparent
        Me.autoSelectFolderInRV.Checked = True
        Me.autoSelectFolderInRV.CheckState = System.Windows.Forms.CheckState.Checked
        Me.autoSelectFolderInRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoSelectFolderInRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoSelectFolderInRV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoSelectFolderInRV.Location = New System.Drawing.Point(6, 99)
        Me.autoSelectFolderInRV.Name = "autoSelectFolderInRV"
        Me.autoSelectFolderInRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoSelectFolderInRV.Size = New System.Drawing.Size(425, 32)
        Me.autoSelectFolderInRV.TabIndex = 127
        Me.autoSelectFolderInRV.Tag = "23"
        Me.autoSelectFolderInRV.Text = "Lors de la sélection d'un dossier dans l'onglet Dossiers, ce dossier sera automat" & _
            "iquement choisi comme filtre dans l'onglet Rendez-vous"
        Me.autoSelectFolderInRV.UseVisualStyleBackColor = False
        '
        'label20
        '
        Me.label20.AutoSize = True
        Me.label20.BackColor = System.Drawing.SystemColors.Control
        Me.label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.label20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label20.Location = New System.Drawing.Point(3, 15)
        Me.label20.Name = "label20"
        Me.label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label20.Size = New System.Drawing.Size(250, 14)
        Me.label20.TabIndex = 122
        Me.label20.Text = "Sélection automatique de l'onglet du compte client :"
        '
        'label60
        '
        Me.label60.AutoSize = True
        Me.label60.BackColor = System.Drawing.SystemColors.Control
        Me.label60.Cursor = System.Windows.Forms.Cursors.Default
        Me.label60.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label60.Location = New System.Drawing.Point(3, 63)
        Me.label60.Name = "label60"
        Me.label60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label60.Size = New System.Drawing.Size(226, 14)
        Me.label60.TabIndex = 124
        Me.label60.Text = "Sélection automatique du texte des dossiers :"
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.BackColor = System.Drawing.SystemColors.Control
        Me.label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.label21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label21.Location = New System.Drawing.Point(3, 39)
        Me.label21.Name = "label21"
        Me.label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label21.Size = New System.Drawing.Size(235, 14)
        Me.label21.TabIndex = 124
        Me.label21.Text = "Sélection automatique de l'onglet des dossiers :"
        '
        'dossierTexteAutoSel
        '
        Me.dossierTexteAutoSel.BackColor = System.Drawing.SystemColors.Window
        Me.dossierTexteAutoSel.Cursor = System.Windows.Forms.Cursors.Default
        Me.dossierTexteAutoSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dossierTexteAutoSel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dossierTexteAutoSel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dossierTexteAutoSel.Location = New System.Drawing.Point(259, 63)
        Me.dossierTexteAutoSel.Name = "dossierTexteAutoSel"
        Me.dossierTexteAutoSel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dossierTexteAutoSel.Size = New System.Drawing.Size(208, 22)
        Me.dossierTexteAutoSel.TabIndex = 123
        Me.dossierTexteAutoSel.Tag = "44"
        '
        'clientTabAutoSelect
        '
        Me.clientTabAutoSelect.BackColor = System.Drawing.SystemColors.Window
        Me.clientTabAutoSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.clientTabAutoSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.clientTabAutoSelect.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clientTabAutoSelect.ForeColor = System.Drawing.SystemColors.WindowText
        Me.clientTabAutoSelect.Items.AddRange(New Object() {"* Dernier accédé *", "Bilan de santé", "Communications", "Comptabilité", "Dossiers", "Rendez-vous"})
        Me.clientTabAutoSelect.Location = New System.Drawing.Point(259, 15)
        Me.clientTabAutoSelect.Name = "clientTabAutoSelect"
        Me.clientTabAutoSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.clientTabAutoSelect.Size = New System.Drawing.Size(208, 22)
        Me.clientTabAutoSelect.TabIndex = 121
        Me.clientTabAutoSelect.Tag = "14"
        '
        'dossierTabAutoSelect
        '
        Me.dossierTabAutoSelect.BackColor = System.Drawing.SystemColors.Window
        Me.dossierTabAutoSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.dossierTabAutoSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dossierTabAutoSelect.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dossierTabAutoSelect.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dossierTabAutoSelect.Items.AddRange(New Object() {"* Dernier accédé *", "Équipements", "Informations de base", "Textes"})
        Me.dossierTabAutoSelect.Location = New System.Drawing.Point(259, 39)
        Me.dossierTabAutoSelect.Name = "dossierTabAutoSelect"
        Me.dossierTabAutoSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dossierTabAutoSelect.Size = New System.Drawing.Size(208, 22)
        Me.dossierTabAutoSelect.TabIndex = 123
        Me.dossierTabAutoSelect.Tag = "15"
        '
        'PageMessagerie
        '
        Me.PageMessagerie.AutoScroll = True
        Me.PageMessagerie.Controls.Add(Me.redirectInternMailToEmail)
        Me.PageMessagerie.Controls.Add(Me.sortingInstantMsgAndNotes)
        Me.PageMessagerie.Controls.Add(Me.mailFolderForDelMsg)
        Me.PageMessagerie.Controls.Add(Me.mailFolderForSentMsg)
        Me.PageMessagerie.Controls.Add(Me.sendDeleteMailToTrash)
        Me.PageMessagerie.Controls.Add(Me.keepATraceOfSentMsg)
        Me.PageMessagerie.Controls.Add(Me.nbSecBeforeMarkAsRead)
        Me.PageMessagerie.Controls.Add(Me.GroupBox4)
        Me.PageMessagerie.Controls.Add(Me.GroupBox6)
        Me.PageMessagerie.Controls.Add(Me.label67)
        Me.PageMessagerie.Controls.Add(Me.receivePlageBloqueeAlert)
        Me.PageMessagerie.Controls.Add(Me.insertOriginMSGOnRespond)
        Me.PageMessagerie.Controls.Add(Me.alertUsersByDefOnMsgSending)
        Me.PageMessagerie.Controls.Add(Me.label50)
        Me.PageMessagerie.Location = New System.Drawing.Point(4, 25)
        Me.PageMessagerie.Name = "PageMessagerie"
        Me.PageMessagerie.Size = New System.Drawing.Size(681, 319)
        Me.PageMessagerie.TabIndex = 6
        Me.PageMessagerie.Text = "Messagerie"
        Me.PageMessagerie.UseVisualStyleBackColor = True
        '
        'redirectInternMailToEmail
        '
        Me.redirectInternMailToEmail.BackColor = System.Drawing.Color.Transparent
        Me.redirectInternMailToEmail.Cursor = System.Windows.Forms.Cursors.Default
        Me.redirectInternMailToEmail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.redirectInternMailToEmail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.redirectInternMailToEmail.Location = New System.Drawing.Point(0, 109)
        Me.redirectInternMailToEmail.Name = "redirectInternMailToEmail"
        Me.redirectInternMailToEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.redirectInternMailToEmail.Size = New System.Drawing.Size(328, 34)
        Me.redirectInternMailToEmail.TabIndex = 130
        Me.redirectInternMailToEmail.Tag = "48"
        Me.redirectInternMailToEmail.Text = "Rediriger les messages internes qui me sont destinés vers mon adresse de courriel" & _
            ""
        Me.redirectInternMailToEmail.UseVisualStyleBackColor = False
        '
        'sortingInstantMsgAndNotes
        '
        Me.sortingInstantMsgAndNotes.BackColor = System.Drawing.SystemColors.Window
        Me.sortingInstantMsgAndNotes.Cursor = System.Windows.Forms.Cursors.Default
        Me.sortingInstantMsgAndNotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.sortingInstantMsgAndNotes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sortingInstantMsgAndNotes.ForeColor = System.Drawing.SystemColors.WindowText
        Me.sortingInstantMsgAndNotes.Items.AddRange(New Object() {"Plus récent en premier", "Moins récent en premier"})
        Me.sortingInstantMsgAndNotes.Location = New System.Drawing.Point(354, 29)
        Me.sortingInstantMsgAndNotes.Name = "sortingInstantMsgAndNotes"
        Me.sortingInstantMsgAndNotes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.sortingInstantMsgAndNotes.Size = New System.Drawing.Size(306, 22)
        Me.sortingInstantMsgAndNotes.TabIndex = 135
        Me.sortingInstantMsgAndNotes.Tag = "51"
        '
        'mailFolderForDelMsg
        '
        Me.mailFolderForDelMsg.acceptAlpha = True
        Me.mailFolderForDelMsg.acceptedChars = ""
        Me.mailFolderForDelMsg.acceptNumeric = True
        Me.mailFolderForDelMsg.allCapital = False
        Me.mailFolderForDelMsg.allLower = False
        Me.mailFolderForDelMsg.BackColor = System.Drawing.SystemColors.Window
        Me.mailFolderForDelMsg.blockOnMaximum = False
        Me.mailFolderForDelMsg.blockOnMinimum = False
        Me.mailFolderForDelMsg.cb_AcceptLeftZeros = False
        Me.mailFolderForDelMsg.cb_AcceptNegative = False
        Me.mailFolderForDelMsg.currencyBox = False
        Me.mailFolderForDelMsg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mailFolderForDelMsg.firstLetterCapital = False
        Me.mailFolderForDelMsg.firstLettersCapital = False
        Me.mailFolderForDelMsg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mailFolderForDelMsg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mailFolderForDelMsg.Location = New System.Drawing.Point(354, 171)
        Me.mailFolderForDelMsg.manageText = True
        Me.mailFolderForDelMsg.matchExp = ""
        Me.mailFolderForDelMsg.maximum = 0
        Me.mailFolderForDelMsg.MaxLength = 0
        Me.mailFolderForDelMsg.minimum = 0
        Me.mailFolderForDelMsg.Name = "mailFolderForDelMsg"
        Me.mailFolderForDelMsg.nbDecimals = CType(0, Short)
        Me.mailFolderForDelMsg.onlyAlphabet = False
        Me.mailFolderForDelMsg.refuseAccents = False
        Me.mailFolderForDelMsg.refusedChars = "\"
        Me.mailFolderForDelMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mailFolderForDelMsg.showInternalContextMenu = True
        Me.mailFolderForDelMsg.Size = New System.Drawing.Size(306, 20)
        Me.mailFolderForDelMsg.TabIndex = 126
        Me.mailFolderForDelMsg.Tag = "50"
        Me.mailFolderForDelMsg.Text = "Messages supprimés"
        Me.mailFolderForDelMsg.trimText = False
        '
        'mailFolderForSentMsg
        '
        Me.mailFolderForSentMsg.acceptAlpha = True
        Me.mailFolderForSentMsg.acceptedChars = ""
        Me.mailFolderForSentMsg.acceptNumeric = True
        Me.mailFolderForSentMsg.allCapital = False
        Me.mailFolderForSentMsg.allLower = False
        Me.mailFolderForSentMsg.BackColor = System.Drawing.SystemColors.Window
        Me.mailFolderForSentMsg.blockOnMaximum = False
        Me.mailFolderForSentMsg.blockOnMinimum = False
        Me.mailFolderForSentMsg.cb_AcceptLeftZeros = False
        Me.mailFolderForSentMsg.cb_AcceptNegative = False
        Me.mailFolderForSentMsg.currencyBox = False
        Me.mailFolderForSentMsg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mailFolderForSentMsg.firstLetterCapital = False
        Me.mailFolderForSentMsg.firstLettersCapital = False
        Me.mailFolderForSentMsg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mailFolderForSentMsg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mailFolderForSentMsg.Location = New System.Drawing.Point(354, 116)
        Me.mailFolderForSentMsg.manageText = True
        Me.mailFolderForSentMsg.matchExp = ""
        Me.mailFolderForSentMsg.maximum = 0
        Me.mailFolderForSentMsg.MaxLength = 0
        Me.mailFolderForSentMsg.minimum = 0
        Me.mailFolderForSentMsg.Name = "mailFolderForSentMsg"
        Me.mailFolderForSentMsg.nbDecimals = CType(0, Short)
        Me.mailFolderForSentMsg.onlyAlphabet = False
        Me.mailFolderForSentMsg.refuseAccents = False
        Me.mailFolderForSentMsg.refusedChars = "\"
        Me.mailFolderForSentMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mailFolderForSentMsg.showInternalContextMenu = True
        Me.mailFolderForSentMsg.Size = New System.Drawing.Size(306, 20)
        Me.mailFolderForSentMsg.TabIndex = 126
        Me.mailFolderForSentMsg.Tag = "50"
        Me.mailFolderForSentMsg.Text = "Messages envoyés"
        Me.mailFolderForSentMsg.trimText = False
        '
        'sendDeleteMailToTrash
        '
        Me.sendDeleteMailToTrash.BackColor = System.Drawing.Color.Transparent
        Me.sendDeleteMailToTrash.Checked = True
        Me.sendDeleteMailToTrash.CheckState = System.Windows.Forms.CheckState.Checked
        Me.sendDeleteMailToTrash.Cursor = System.Windows.Forms.Cursors.Default
        Me.sendDeleteMailToTrash.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sendDeleteMailToTrash.ForeColor = System.Drawing.SystemColors.ControlText
        Me.sendDeleteMailToTrash.Location = New System.Drawing.Point(335, 140)
        Me.sendDeleteMailToTrash.Name = "sendDeleteMailToTrash"
        Me.sendDeleteMailToTrash.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.sendDeleteMailToTrash.Size = New System.Drawing.Size(262, 34)
        Me.sendDeleteMailToTrash.TabIndex = 130
        Me.sendDeleteMailToTrash.Tag = "49"
        Me.sendDeleteMailToTrash.Text = "Envoyer les messages supprimés dans le dossier suivant :"
        Me.sendDeleteMailToTrash.UseVisualStyleBackColor = False
        '
        'keepATraceOfSentMsg
        '
        Me.keepATraceOfSentMsg.BackColor = System.Drawing.Color.Transparent
        Me.keepATraceOfSentMsg.Checked = True
        Me.keepATraceOfSentMsg.CheckState = System.Windows.Forms.CheckState.Checked
        Me.keepATraceOfSentMsg.Cursor = System.Windows.Forms.Cursors.Default
        Me.keepATraceOfSentMsg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.keepATraceOfSentMsg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.keepATraceOfSentMsg.Location = New System.Drawing.Point(335, 85)
        Me.keepATraceOfSentMsg.Name = "keepATraceOfSentMsg"
        Me.keepATraceOfSentMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.keepATraceOfSentMsg.Size = New System.Drawing.Size(262, 34)
        Me.keepATraceOfSentMsg.TabIndex = 130
        Me.keepATraceOfSentMsg.Tag = "49"
        Me.keepATraceOfSentMsg.Text = " Garder une copie des messages envoyés dans le dossier suivant :"
        Me.keepATraceOfSentMsg.UseVisualStyleBackColor = False
        '
        'nbSecBeforeMarkAsRead
        '
        Me.nbSecBeforeMarkAsRead.acceptAlpha = False
        Me.nbSecBeforeMarkAsRead.acceptedChars = ""
        Me.nbSecBeforeMarkAsRead.acceptNumeric = True
        Me.nbSecBeforeMarkAsRead.allCapital = False
        Me.nbSecBeforeMarkAsRead.allLower = False
        Me.nbSecBeforeMarkAsRead.BackColor = System.Drawing.SystemColors.Window
        Me.nbSecBeforeMarkAsRead.blockOnMaximum = True
        Me.nbSecBeforeMarkAsRead.blockOnMinimum = False
        Me.nbSecBeforeMarkAsRead.cb_AcceptLeftZeros = False
        Me.nbSecBeforeMarkAsRead.cb_AcceptNegative = False
        Me.nbSecBeforeMarkAsRead.currencyBox = True
        Me.nbSecBeforeMarkAsRead.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nbSecBeforeMarkAsRead.firstLetterCapital = False
        Me.nbSecBeforeMarkAsRead.firstLettersCapital = False
        Me.nbSecBeforeMarkAsRead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbSecBeforeMarkAsRead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nbSecBeforeMarkAsRead.Location = New System.Drawing.Point(242, 9)
        Me.nbSecBeforeMarkAsRead.manageText = True
        Me.nbSecBeforeMarkAsRead.matchExp = ""
        Me.nbSecBeforeMarkAsRead.maximum = 999
        Me.nbSecBeforeMarkAsRead.MaxLength = 0
        Me.nbSecBeforeMarkAsRead.minimum = 0
        Me.nbSecBeforeMarkAsRead.Name = "nbSecBeforeMarkAsRead"
        Me.nbSecBeforeMarkAsRead.nbDecimals = CType(0, Short)
        Me.nbSecBeforeMarkAsRead.onlyAlphabet = False
        Me.nbSecBeforeMarkAsRead.refuseAccents = False
        Me.nbSecBeforeMarkAsRead.refusedChars = ""
        Me.nbSecBeforeMarkAsRead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbSecBeforeMarkAsRead.showInternalContextMenu = True
        Me.nbSecBeforeMarkAsRead.Size = New System.Drawing.Size(29, 20)
        Me.nbSecBeforeMarkAsRead.TabIndex = 126
        Me.nbSecBeforeMarkAsRead.Tag = "30"
        Me.nbSecBeforeMarkAsRead.Text = "2"
        Me.nbSecBeforeMarkAsRead.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nbSecBeforeMarkAsRead.trimText = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnChooseEmailSignature)
        Me.GroupBox4.Controls.Add(Me.addEmailSignOnAnswer)
        Me.GroupBox4.Controls.Add(Me.addEmailSignOnSend)
        Me.GroupBox4.Controls.Add(Me.EmailSignatureModel)
        Me.GroupBox4.Location = New System.Drawing.Point(0, 195)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(660, 77)
        Me.GroupBox4.TabIndex = 133
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Signature électronique"
        '
        'btnChooseEmailSignature
        '
        Me.btnChooseEmailSignature.Location = New System.Drawing.Point(5, 45)
        Me.btnChooseEmailSignature.Name = "btnChooseEmailSignature"
        Me.btnChooseEmailSignature.Size = New System.Drawing.Size(266, 23)
        Me.btnChooseEmailSignature.TabIndex = 131
        Me.btnChooseEmailSignature.Text = "Sélectionner le modèle pour signature par défaut"
        Me.btnChooseEmailSignature.UseVisualStyleBackColor = True
        '
        'addEmailSignOnAnswer
        '
        Me.addEmailSignOnAnswer.AutoSize = True
        Me.addEmailSignOnAnswer.BackColor = System.Drawing.Color.Transparent
        Me.addEmailSignOnAnswer.Cursor = System.Windows.Forms.Cursors.Default
        Me.addEmailSignOnAnswer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addEmailSignOnAnswer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addEmailSignOnAnswer.Location = New System.Drawing.Point(335, 20)
        Me.addEmailSignOnAnswer.Name = "addEmailSignOnAnswer"
        Me.addEmailSignOnAnswer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.addEmailSignOnAnswer.Size = New System.Drawing.Size(238, 18)
        Me.addEmailSignOnAnswer.TabIndex = 130
        Me.addEmailSignOnAnswer.Tag = "46"
        Me.addEmailSignOnAnswer.Text = "Ajouter la signature au message à répondre"
        Me.addEmailSignOnAnswer.UseVisualStyleBackColor = False
        '
        'addEmailSignOnSend
        '
        Me.addEmailSignOnSend.AutoSize = True
        Me.addEmailSignOnSend.BackColor = System.Drawing.Color.Transparent
        Me.addEmailSignOnSend.Cursor = System.Windows.Forms.Cursors.Default
        Me.addEmailSignOnSend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addEmailSignOnSend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addEmailSignOnSend.Location = New System.Drawing.Point(5, 20)
        Me.addEmailSignOnSend.Name = "addEmailSignOnSend"
        Me.addEmailSignOnSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.addEmailSignOnSend.Size = New System.Drawing.Size(234, 18)
        Me.addEmailSignOnSend.TabIndex = 130
        Me.addEmailSignOnSend.Tag = "45"
        Me.addEmailSignOnSend.Text = "Ajouter la signature au message à envoyer"
        Me.addEmailSignOnSend.UseVisualStyleBackColor = False
        '
        'EmailSignatureModel
        '
        Me.EmailSignatureModel.AutoSize = True
        Me.EmailSignatureModel.BackColor = System.Drawing.SystemColors.Control
        Me.EmailSignatureModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.EmailSignatureModel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmailSignatureModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.EmailSignatureModel.Location = New System.Drawing.Point(277, 49)
        Me.EmailSignatureModel.Name = "EmailSignatureModel"
        Me.EmailSignatureModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.EmailSignatureModel.Size = New System.Drawing.Size(134, 14)
        Me.EmailSignatureModel.TabIndex = 136
        Me.EmailSignatureModel.Tag = "3"
        Me.EmailSignatureModel.Text = "Aucun modèle sélectionné"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.receiveFeedBackForMessages)
        Me.GroupBox6.Controls.Add(Me.Label69)
        Me.GroupBox6.Controls.Add(Me.AlertExpiries)
        Me.GroupBox6.Location = New System.Drawing.Point(3, 149)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(316, 159)
        Me.GroupBox6.TabIndex = 134
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Expirations des messages instantannés"
        Me.GroupBox6.Visible = False
        '
        'receiveFeedBackForMessages
        '
        Me.receiveFeedBackForMessages.AutoSize = True
        Me.receiveFeedBackForMessages.BackColor = System.Drawing.Color.Transparent
        Me.receiveFeedBackForMessages.Checked = True
        Me.receiveFeedBackForMessages.CheckState = System.Windows.Forms.CheckState.Checked
        Me.receiveFeedBackForMessages.Cursor = System.Windows.Forms.Cursors.Default
        Me.receiveFeedBackForMessages.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.receiveFeedBackForMessages.ForeColor = System.Drawing.SystemColors.ControlText
        Me.receiveFeedBackForMessages.Location = New System.Drawing.Point(-3, -1)
        Me.receiveFeedBackForMessages.Name = "receiveFeedBackForMessages"
        Me.receiveFeedBackForMessages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.receiveFeedBackForMessages.Size = New System.Drawing.Size(326, 18)
        Me.receiveFeedBackForMessages.TabIndex = 129
        Me.receiveFeedBackForMessages.Tag = "31"
        Me.receiveFeedBackForMessages.Text = "Recevoir un accusé de réception pour les messages envoyés"
        Me.receiveFeedBackForMessages.UseVisualStyleBackColor = False
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(223, 3)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(74, 14)
        Me.Label69.TabIndex = 132
        Me.Label69.Text = "NON TERMINÉ"
        Me.Label69.Visible = False
        '
        'AlertExpiries
        '
        Me.AlertExpiries.AllowUserToAddRows = False
        Me.AlertExpiries.AllowUserToDeleteRows = False
        Me.AlertExpiries.AllowUserToResizeColumns = False
        Me.AlertExpiries.AllowUserToResizeRows = False
        Me.AlertExpiries.autoSelectOnDataSourceChanged = True
        Me.AlertExpiries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AlertExpiries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TypeName, Me.ExpiryInMinutes, Me.NoUserAlertType, Me.NoUser})
        Me.AlertExpiries.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.AlertExpiries.isDoubleBuffered = False
        Me.AlertExpiries.Location = New System.Drawing.Point(9, 17)
        Me.AlertExpiries.MultiSelect = False
        Me.AlertExpiries.Name = "AlertExpiries"
        Me.AlertExpiries.RowHeadersVisible = False
        Me.AlertExpiries.RowHeadersWidth = 10
        Me.AlertExpiries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.AlertExpiries.Size = New System.Drawing.Size(300, 136)
        Me.AlertExpiries.TabIndex = 0
        '
        'TypeName
        '
        Me.TypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.TypeName.DataPropertyName = "TypeName"
        Me.TypeName.HeaderText = "Type d'alerte"
        Me.TypeName.Name = "TypeName"
        Me.TypeName.ReadOnly = True
        '
        'ExpiryInMinutes
        '
        Me.ExpiryInMinutes.AutoComplete = False
        Me.ExpiryInMinutes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ExpiryInMinutes.DataPropertyName = "ExpiryInMinutes"
        Me.ExpiryInMinutes.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.ExpiryInMinutes.HeaderText = "Expiration"
        Me.ExpiryInMinutes.Name = "ExpiryInMinutes"
        Me.ExpiryInMinutes.Width = 60
        '
        'NoUserAlertType
        '
        Me.NoUserAlertType.DataPropertyName = "NoUserAlertType"
        Me.NoUserAlertType.HeaderText = "NoUserAlertType"
        Me.NoUserAlertType.Name = "NoUserAlertType"
        Me.NoUserAlertType.Visible = False
        '
        'NoUser
        '
        Me.NoUser.DataPropertyName = "NoUser"
        Me.NoUser.HeaderText = "NoUser"
        Me.NoUser.Name = "NoUser"
        Me.NoUser.Visible = False
        '
        'label67
        '
        Me.label67.AutoSize = True
        Me.label67.BackColor = System.Drawing.SystemColors.Control
        Me.label67.Cursor = System.Windows.Forms.Cursors.Default
        Me.label67.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label67.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label67.Location = New System.Drawing.Point(351, 12)
        Me.label67.Name = "label67"
        Me.label67.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label67.Size = New System.Drawing.Size(299, 14)
        Me.label67.TabIndex = 136
        Me.label67.Text = "Ordre de triage pour les messages instantanés et les notes :"
        '
        'receivePlageBloqueeAlert
        '
        Me.receivePlageBloqueeAlert.AutoSize = True
        Me.receivePlageBloqueeAlert.BackColor = System.Drawing.Color.Transparent
        Me.receivePlageBloqueeAlert.Checked = True
        Me.receivePlageBloqueeAlert.CheckState = System.Windows.Forms.CheckState.Checked
        Me.receivePlageBloqueeAlert.Cursor = System.Windows.Forms.Cursors.Default
        Me.receivePlageBloqueeAlert.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.receivePlageBloqueeAlert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.receivePlageBloqueeAlert.Location = New System.Drawing.Point(0, 85)
        Me.receivePlageBloqueeAlert.Name = "receivePlageBloqueeAlert"
        Me.receivePlageBloqueeAlert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.receivePlageBloqueeAlert.Size = New System.Drawing.Size(307, 18)
        Me.receivePlageBloqueeAlert.TabIndex = 130
        Me.receivePlageBloqueeAlert.Tag = "39"
        Me.receivePlageBloqueeAlert.Text = "Recevoir les messages instantannés des plages bloquées"
        Me.receivePlageBloqueeAlert.UseVisualStyleBackColor = False
        '
        'insertOriginMSGOnRespond
        '
        Me.insertOriginMSGOnRespond.AutoSize = True
        Me.insertOriginMSGOnRespond.BackColor = System.Drawing.Color.Transparent
        Me.insertOriginMSGOnRespond.Checked = True
        Me.insertOriginMSGOnRespond.CheckState = System.Windows.Forms.CheckState.Checked
        Me.insertOriginMSGOnRespond.Cursor = System.Windows.Forms.Cursors.Default
        Me.insertOriginMSGOnRespond.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.insertOriginMSGOnRespond.ForeColor = System.Drawing.SystemColors.ControlText
        Me.insertOriginMSGOnRespond.Location = New System.Drawing.Point(335, 61)
        Me.insertOriginMSGOnRespond.Name = "insertOriginMSGOnRespond"
        Me.insertOriginMSGOnRespond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.insertOriginMSGOnRespond.Size = New System.Drawing.Size(325, 18)
        Me.insertOriginMSGOnRespond.TabIndex = 130
        Me.insertOriginMSGOnRespond.Tag = "38"
        Me.insertOriginMSGOnRespond.Text = "Insérer le message d'origine lors d'une réponse à un message"
        Me.insertOriginMSGOnRespond.UseVisualStyleBackColor = False
        '
        'alertUsersByDefOnMsgSending
        '
        Me.alertUsersByDefOnMsgSending.AutoSize = True
        Me.alertUsersByDefOnMsgSending.BackColor = System.Drawing.Color.Transparent
        Me.alertUsersByDefOnMsgSending.Checked = True
        Me.alertUsersByDefOnMsgSending.CheckState = System.Windows.Forms.CheckState.Checked
        Me.alertUsersByDefOnMsgSending.Cursor = System.Windows.Forms.Cursors.Default
        Me.alertUsersByDefOnMsgSending.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.alertUsersByDefOnMsgSending.ForeColor = System.Drawing.SystemColors.ControlText
        Me.alertUsersByDefOnMsgSending.Location = New System.Drawing.Point(0, 61)
        Me.alertUsersByDefOnMsgSending.Name = "alertUsersByDefOnMsgSending"
        Me.alertUsersByDefOnMsgSending.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.alertUsersByDefOnMsgSending.Size = New System.Drawing.Size(339, 18)
        Me.alertUsersByDefOnMsgSending.TabIndex = 128
        Me.alertUsersByDefOnMsgSending.Tag = "29"
        Me.alertUsersByDefOnMsgSending.Text = "Alerter le(s) utilisateur(s) par défaut lors de l'envoi d'un message"
        Me.alertUsersByDefOnMsgSending.UseVisualStyleBackColor = False
        '
        'label50
        '
        Me.label50.AutoSize = True
        Me.label50.BackColor = System.Drawing.SystemColors.Control
        Me.label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.label50.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label50.Location = New System.Drawing.Point(-3, 12)
        Me.label50.Name = "label50"
        Me.label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label50.Size = New System.Drawing.Size(331, 14)
        Me.label50.TabIndex = 124
        Me.label50.Text = "Marquer les messages lus après un affichage de             secondes"
        '
        'PageUserRapport
        '
        Me.PageUserRapport.Controls.Add(Me.affRapportCatInSelection)
        Me.PageUserRapport.Controls.Add(Me.affRapportCatInFinDeMois)
        Me.PageUserRapport.Controls.Add(Me.generateAutoRapportOnOpening)
        Me.PageUserRapport.Controls.Add(Me.alertUserOnRapportGeneration)
        Me.PageUserRapport.Location = New System.Drawing.Point(4, 25)
        Me.PageUserRapport.Name = "PageUserRapport"
        Me.PageUserRapport.Size = New System.Drawing.Size(681, 319)
        Me.PageUserRapport.TabIndex = 3
        Me.PageUserRapport.Text = "Rapport"
        Me.PageUserRapport.UseVisualStyleBackColor = True
        '
        'affRapportCatInSelection
        '
        Me.affRapportCatInSelection.AutoSize = True
        Me.affRapportCatInSelection.BackColor = System.Drawing.Color.Transparent
        Me.affRapportCatInSelection.Checked = True
        Me.affRapportCatInSelection.CheckState = System.Windows.Forms.CheckState.Checked
        Me.affRapportCatInSelection.Cursor = System.Windows.Forms.Cursors.Default
        Me.affRapportCatInSelection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affRapportCatInSelection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affRapportCatInSelection.Location = New System.Drawing.Point(8, 64)
        Me.affRapportCatInSelection.Name = "affRapportCatInSelection"
        Me.affRapportCatInSelection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.affRapportCatInSelection.Size = New System.Drawing.Size(330, 18)
        Me.affRapportCatInSelection.TabIndex = 125
        Me.affRapportCatInSelection.Tag = "26"
        Me.affRapportCatInSelection.Text = "Afficher les catégories lors de la sélection d'un type de rapport"
        Me.affRapportCatInSelection.UseVisualStyleBackColor = False
        '
        'affRapportCatInFinDeMois
        '
        Me.affRapportCatInFinDeMois.AutoSize = True
        Me.affRapportCatInFinDeMois.BackColor = System.Drawing.Color.Transparent
        Me.affRapportCatInFinDeMois.Checked = True
        Me.affRapportCatInFinDeMois.CheckState = System.Windows.Forms.CheckState.Checked
        Me.affRapportCatInFinDeMois.Cursor = System.Windows.Forms.Cursors.Default
        Me.affRapportCatInFinDeMois.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affRapportCatInFinDeMois.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affRapportCatInFinDeMois.Location = New System.Drawing.Point(8, 88)
        Me.affRapportCatInFinDeMois.Name = "affRapportCatInFinDeMois"
        Me.affRapportCatInFinDeMois.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.affRapportCatInFinDeMois.Size = New System.Drawing.Size(382, 18)
        Me.affRapportCatInFinDeMois.TabIndex = 127
        Me.affRapportCatInFinDeMois.Tag = "12"
        Me.affRapportCatInFinDeMois.Text = "Afficher les catégories dans la liste des types de rapport de la fin de mois"
        Me.affRapportCatInFinDeMois.UseVisualStyleBackColor = False
        '
        'generateAutoRapportOnOpening
        '
        Me.generateAutoRapportOnOpening.AutoSize = True
        Me.generateAutoRapportOnOpening.BackColor = System.Drawing.Color.Transparent
        Me.generateAutoRapportOnOpening.Checked = True
        Me.generateAutoRapportOnOpening.CheckState = System.Windows.Forms.CheckState.Checked
        Me.generateAutoRapportOnOpening.Cursor = System.Windows.Forms.Cursors.Default
        Me.generateAutoRapportOnOpening.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.generateAutoRapportOnOpening.ForeColor = System.Drawing.SystemColors.ControlText
        Me.generateAutoRapportOnOpening.Location = New System.Drawing.Point(8, 40)
        Me.generateAutoRapportOnOpening.Name = "generateAutoRapportOnOpening"
        Me.generateAutoRapportOnOpening.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.generateAutoRapportOnOpening.Size = New System.Drawing.Size(421, 18)
        Me.generateAutoRapportOnOpening.TabIndex = 125
        Me.generateAutoRapportOnOpening.Tag = "25"
        Me.generateAutoRapportOnOpening.Text = "Afficher la liste des types de rapport automatiquement à l'ouverture du générateu" & _
            "r"
        Me.generateAutoRapportOnOpening.UseVisualStyleBackColor = False
        '
        'alertUserOnRapportGeneration
        '
        Me.alertUserOnRapportGeneration.AutoSize = True
        Me.alertUserOnRapportGeneration.BackColor = System.Drawing.Color.Transparent
        Me.alertUserOnRapportGeneration.Checked = True
        Me.alertUserOnRapportGeneration.CheckState = System.Windows.Forms.CheckState.Checked
        Me.alertUserOnRapportGeneration.Cursor = System.Windows.Forms.Cursors.Default
        Me.alertUserOnRapportGeneration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.alertUserOnRapportGeneration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.alertUserOnRapportGeneration.Location = New System.Drawing.Point(8, 14)
        Me.alertUserOnRapportGeneration.Name = "alertUserOnRapportGeneration"
        Me.alertUserOnRapportGeneration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.alertUserOnRapportGeneration.Size = New System.Drawing.Size(237, 18)
        Me.alertUserOnRapportGeneration.TabIndex = 125
        Me.alertUserOnRapportGeneration.Tag = "22"
        Me.alertUserOnRapportGeneration.Text = "M'avertir lorsque qu'un rapport a été généré"
        Me.alertUserOnRapportGeneration.UseVisualStyleBackColor = False
        '
        'PageUserRendezvous
        '
        Me.PageUserRendezvous.Controls.Add(Me.label13)
        Me.PageUserRendezvous.Controls.Add(Me.findPlacesUpTo)
        Me.PageUserRendezvous.Controls.Add(Me.autoOpenSearchIfNAMEmpty)
        Me.PageUserRendezvous.Controls.Add(Me.autoNewRV)
        Me.PageUserRendezvous.Controls.Add(Me.label18)
        Me.PageUserRendezvous.Controls.Add(Me.dblClickOnFutureRV)
        Me.PageUserRendezvous.Location = New System.Drawing.Point(4, 25)
        Me.PageUserRendezvous.Name = "PageUserRendezvous"
        Me.PageUserRendezvous.Size = New System.Drawing.Size(681, 319)
        Me.PageUserRendezvous.TabIndex = 4
        Me.PageUserRendezvous.Text = "Rendez-vous"
        Me.PageUserRendezvous.UseVisualStyleBackColor = True
        '
        'label13
        '
        Me.label13.BackColor = System.Drawing.SystemColors.Control
        Me.label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label13.Location = New System.Drawing.Point(5, 14)
        Me.label13.Name = "label13"
        Me.label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label13.Size = New System.Drawing.Size(257, 17)
        Me.label13.TabIndex = 115
        Me.label13.Text = "Recherche les places disponibles jusqu'à :"
        '
        'findPlacesUpTo
        '
        Me.findPlacesUpTo.BackColor = System.Drawing.SystemColors.Window
        Me.findPlacesUpTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.findPlacesUpTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.findPlacesUpTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPlacesUpTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.findPlacesUpTo.Items.AddRange(New Object() {"1 semaine", "2 semaines", "3 semaines", "4 semaines"})
        Me.findPlacesUpTo.Location = New System.Drawing.Point(8, 34)
        Me.findPlacesUpTo.Name = "findPlacesUpTo"
        Me.findPlacesUpTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.findPlacesUpTo.Size = New System.Drawing.Size(257, 22)
        Me.findPlacesUpTo.TabIndex = 114
        Me.findPlacesUpTo.Tag = "10"
        '
        'autoOpenSearchIfNAMEmpty
        '
        Me.autoOpenSearchIfNAMEmpty.BackColor = System.Drawing.Color.Transparent
        Me.autoOpenSearchIfNAMEmpty.Checked = True
        Me.autoOpenSearchIfNAMEmpty.CheckState = System.Windows.Forms.CheckState.Checked
        Me.autoOpenSearchIfNAMEmpty.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoOpenSearchIfNAMEmpty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoOpenSearchIfNAMEmpty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoOpenSearchIfNAMEmpty.Location = New System.Drawing.Point(8, 128)
        Me.autoOpenSearchIfNAMEmpty.Name = "autoOpenSearchIfNAMEmpty"
        Me.autoOpenSearchIfNAMEmpty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoOpenSearchIfNAMEmpty.Size = New System.Drawing.Size(429, 32)
        Me.autoOpenSearchIfNAMEmpty.TabIndex = 112
        Me.autoOpenSearchIfNAMEmpty.Tag = "8"
        Me.autoOpenSearchIfNAMEmpty.Text = "Ouverture automatique de la recherche d'un compte client si le champ NAM est vide" & _
            " lors de l'ouverture de la fenêtre d'ajout d'un rendez-vous"
        Me.autoOpenSearchIfNAMEmpty.UseVisualStyleBackColor = False
        '
        'autoNewRV
        '
        Me.autoNewRV.AutoSize = True
        Me.autoNewRV.BackColor = System.Drawing.Color.Transparent
        Me.autoNewRV.Checked = True
        Me.autoNewRV.CheckState = System.Windows.Forms.CheckState.Checked
        Me.autoNewRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoNewRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoNewRV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoNewRV.Location = New System.Drawing.Point(8, 178)
        Me.autoNewRV.Name = "autoNewRV"
        Me.autoNewRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoNewRV.Size = New System.Drawing.Size(488, 18)
        Me.autoNewRV.TabIndex = 113
        Me.autoNewRV.Tag = "9"
        Me.autoNewRV.Text = "Ouverture automatique de l'ajout d'un rendez-vous après l'ouverture d'un nouveau " & _
            "compte client"
        Me.autoNewRV.UseVisualStyleBackColor = False
        '
        'label18
        '
        Me.label18.AutoSize = True
        Me.label18.BackColor = System.Drawing.SystemColors.Control
        Me.label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.label18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label18.Location = New System.Drawing.Point(5, 67)
        Me.label18.Name = "label18"
        Me.label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label18.Size = New System.Drawing.Size(259, 14)
        Me.label18.TabIndex = 117
        Me.label18.Text = "Action du double-clique sur les rendez-vous futurs :"
        '
        'dblClickOnFutureRV
        '
        Me.dblClickOnFutureRV.BackColor = System.Drawing.SystemColors.Window
        Me.dblClickOnFutureRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.dblClickOnFutureRV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dblClickOnFutureRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dblClickOnFutureRV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dblClickOnFutureRV.Items.AddRange(New Object() {"Nouveau rendez-vous", "Ouvrir le compte"})
        Me.dblClickOnFutureRV.Location = New System.Drawing.Point(8, 84)
        Me.dblClickOnFutureRV.Name = "dblClickOnFutureRV"
        Me.dblClickOnFutureRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dblClickOnFutureRV.Size = New System.Drawing.Size(256, 22)
        Me.dblClickOnFutureRV.TabIndex = 116
        Me.dblClickOnFutureRV.Tag = "11"
        '
        'PageUserSons
        '
        Me.PageUserSons.Controls.Add(Me.GroupBox2)
        Me.PageUserSons.Location = New System.Drawing.Point(4, 25)
        Me.PageUserSons.Name = "PageUserSons"
        Me.PageUserSons.Size = New System.Drawing.Size(681, 319)
        Me.PageUserSons.TabIndex = 5
        Me.PageUserSons.Text = "Sons"
        Me.PageUserSons.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.selectSonAlertRapportGen)
        Me.GroupBox2.Controls.Add(Me.selectSonAlertQL)
        Me.GroupBox2.Controls.Add(Me.selectSonAlertNote)
        Me.GroupBox2.Controls.Add(Me.selectSonAlertMSG)
        Me.GroupBox2.Controls.Add(Me.selectSonAlertKP)
        Me.GroupBox2.Controls.Add(Me.selectSonAlertClient)
        Me.GroupBox2.Controls.Add(Me.Label56)
        Me.GroupBox2.Controls.Add(Me.Label55)
        Me.GroupBox2.Controls.Add(Me.sonAlertRapportGen)
        Me.GroupBox2.Controls.Add(Me.Label54)
        Me.GroupBox2.Controls.Add(Me.sonAlertQL)
        Me.GroupBox2.Controls.Add(Me.Label53)
        Me.GroupBox2.Controls.Add(Me.sonAlertNote)
        Me.GroupBox2.Controls.Add(Me.Label52)
        Me.GroupBox2.Controls.Add(Me.sonAlertMSG)
        Me.GroupBox2.Controls.Add(Me.Label51)
        Me.GroupBox2.Controls.Add(Me.sonAlertKP)
        Me.GroupBox2.Controls.Add(Me.sonAlertClient)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(306, 276)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sons des messages instantannés"
        '
        'selectSonAlertRapportGen
        '
        Me.selectSonAlertRapportGen.BackColor = System.Drawing.SystemColors.Control
        Me.selectSonAlertRapportGen.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectSonAlertRapportGen.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSonAlertRapportGen.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectSonAlertRapportGen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectSonAlertRapportGen.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectSonAlertRapportGen.Location = New System.Drawing.Point(6, 245)
        Me.selectSonAlertRapportGen.Name = "selectSonAlertRapportGen"
        Me.selectSonAlertRapportGen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectSonAlertRapportGen.Size = New System.Drawing.Size(24, 24)
        Me.selectSonAlertRapportGen.TabIndex = 0
        Me.selectSonAlertRapportGen.UseVisualStyleBackColor = False
        '
        'selectSonAlertQL
        '
        Me.selectSonAlertQL.BackColor = System.Drawing.SystemColors.Control
        Me.selectSonAlertQL.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectSonAlertQL.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSonAlertQL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectSonAlertQL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectSonAlertQL.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectSonAlertQL.Location = New System.Drawing.Point(6, 203)
        Me.selectSonAlertQL.Name = "selectSonAlertQL"
        Me.selectSonAlertQL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectSonAlertQL.Size = New System.Drawing.Size(24, 24)
        Me.selectSonAlertQL.TabIndex = 0
        Me.selectSonAlertQL.UseVisualStyleBackColor = False
        '
        'selectSonAlertNote
        '
        Me.selectSonAlertNote.BackColor = System.Drawing.SystemColors.Control
        Me.selectSonAlertNote.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectSonAlertNote.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSonAlertNote.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectSonAlertNote.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectSonAlertNote.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectSonAlertNote.Location = New System.Drawing.Point(6, 161)
        Me.selectSonAlertNote.Name = "selectSonAlertNote"
        Me.selectSonAlertNote.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectSonAlertNote.Size = New System.Drawing.Size(24, 24)
        Me.selectSonAlertNote.TabIndex = 0
        Me.selectSonAlertNote.UseVisualStyleBackColor = False
        '
        'selectSonAlertMSG
        '
        Me.selectSonAlertMSG.BackColor = System.Drawing.SystemColors.Control
        Me.selectSonAlertMSG.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectSonAlertMSG.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSonAlertMSG.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectSonAlertMSG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectSonAlertMSG.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectSonAlertMSG.Location = New System.Drawing.Point(6, 119)
        Me.selectSonAlertMSG.Name = "selectSonAlertMSG"
        Me.selectSonAlertMSG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectSonAlertMSG.Size = New System.Drawing.Size(24, 24)
        Me.selectSonAlertMSG.TabIndex = 0
        Me.selectSonAlertMSG.UseVisualStyleBackColor = False
        '
        'selectSonAlertKP
        '
        Me.selectSonAlertKP.BackColor = System.Drawing.SystemColors.Control
        Me.selectSonAlertKP.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectSonAlertKP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSonAlertKP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectSonAlertKP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectSonAlertKP.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectSonAlertKP.Location = New System.Drawing.Point(6, 77)
        Me.selectSonAlertKP.Name = "selectSonAlertKP"
        Me.selectSonAlertKP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectSonAlertKP.Size = New System.Drawing.Size(24, 24)
        Me.selectSonAlertKP.TabIndex = 0
        Me.selectSonAlertKP.UseVisualStyleBackColor = False
        '
        'selectSonAlertClient
        '
        Me.selectSonAlertClient.BackColor = System.Drawing.SystemColors.Control
        Me.selectSonAlertClient.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectSonAlertClient.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSonAlertClient.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectSonAlertClient.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectSonAlertClient.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectSonAlertClient.Location = New System.Drawing.Point(6, 35)
        Me.selectSonAlertClient.Name = "selectSonAlertClient"
        Me.selectSonAlertClient.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectSonAlertClient.Size = New System.Drawing.Size(24, 24)
        Me.selectSonAlertClient.TabIndex = 0
        Me.selectSonAlertClient.UseVisualStyleBackColor = False
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(3, 230)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(191, 14)
        Me.Label56.TabIndex = 2
        Me.Label56.Text = "En lien avec le générateur de rapport :"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(3, 188)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(148, 14)
        Me.Label55.TabIndex = 2
        Me.Label55.Text = "En lien avec la liste d'attente :"
        '
        'sonAlertRapportGen
        '
        Me.sonAlertRapportGen.BackColor = System.Drawing.SystemColors.ControlLight
        Me.sonAlertRapportGen.Location = New System.Drawing.Point(35, 247)
        Me.sonAlertRapportGen.Name = "sonAlertRapportGen"
        Me.sonAlertRapportGen.ReadOnly = True
        Me.sonAlertRapportGen.Size = New System.Drawing.Size(258, 20)
        Me.sonAlertRapportGen.TabIndex = 1
        Me.sonAlertRapportGen.Tag = "37"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(3, 146)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(175, 14)
        Me.Label54.TabIndex = 2
        Me.Label54.Text = "En lien avec une note personnelle :"
        '
        'sonAlertQL
        '
        Me.sonAlertQL.BackColor = System.Drawing.SystemColors.ControlLight
        Me.sonAlertQL.Location = New System.Drawing.Point(35, 205)
        Me.sonAlertQL.Name = "sonAlertQL"
        Me.sonAlertQL.ReadOnly = True
        Me.sonAlertQL.Size = New System.Drawing.Size(258, 20)
        Me.sonAlertQL.TabIndex = 1
        Me.sonAlertQL.Tag = "36"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(3, 104)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(133, 14)
        Me.Label53.TabIndex = 2
        Me.Label53.Text = "En lien avec un message :"
        '
        'sonAlertNote
        '
        Me.sonAlertNote.BackColor = System.Drawing.SystemColors.ControlLight
        Me.sonAlertNote.Location = New System.Drawing.Point(35, 163)
        Me.sonAlertNote.Name = "sonAlertNote"
        Me.sonAlertNote.ReadOnly = True
        Me.sonAlertNote.Size = New System.Drawing.Size(258, 20)
        Me.sonAlertNote.TabIndex = 1
        Me.sonAlertNote.Tag = "35"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(3, 62)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(249, 14)
        Me.Label52.TabIndex = 2
        Me.Label52.Text = "En lien avec un compte personne / organisme clé :"
        '
        'sonAlertMSG
        '
        Me.sonAlertMSG.BackColor = System.Drawing.SystemColors.ControlLight
        Me.sonAlertMSG.Location = New System.Drawing.Point(35, 121)
        Me.sonAlertMSG.Name = "sonAlertMSG"
        Me.sonAlertMSG.ReadOnly = True
        Me.sonAlertMSG.Size = New System.Drawing.Size(258, 20)
        Me.sonAlertMSG.TabIndex = 1
        Me.sonAlertMSG.Tag = "34"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(3, 20)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(152, 14)
        Me.Label51.TabIndex = 2
        Me.Label51.Text = "En lien avec un compte client :"
        '
        'sonAlertKP
        '
        Me.sonAlertKP.BackColor = System.Drawing.SystemColors.ControlLight
        Me.sonAlertKP.Location = New System.Drawing.Point(35, 79)
        Me.sonAlertKP.Name = "sonAlertKP"
        Me.sonAlertKP.ReadOnly = True
        Me.sonAlertKP.Size = New System.Drawing.Size(258, 20)
        Me.sonAlertKP.TabIndex = 1
        Me.sonAlertKP.Tag = "33"
        '
        'sonAlertClient
        '
        Me.sonAlertClient.BackColor = System.Drawing.SystemColors.ControlLight
        Me.sonAlertClient.Location = New System.Drawing.Point(35, 37)
        Me.sonAlertClient.Name = "sonAlertClient"
        Me.sonAlertClient.ReadOnly = True
        Me.sonAlertClient.Size = New System.Drawing.Size(258, 20)
        Me.sonAlertClient.TabIndex = 1
        Me.sonAlertClient.Tag = "32"
        '
        'nbPrefU
        '
        Me.nbPrefU.AutoSize = True
        Me.nbPrefU.BackColor = System.Drawing.SystemColors.Control
        Me.nbPrefU.Cursor = System.Windows.Forms.Cursors.Default
        Me.nbPrefU.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbPrefU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.nbPrefU.Location = New System.Drawing.Point(5, 241)
        Me.nbPrefU.Name = "nbPrefU"
        Me.nbPrefU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbPrefU.Size = New System.Drawing.Size(13, 14)
        Me.nbPrefU.TabIndex = 95
        Me.nbPrefU.Text = "0"
        Me.nbPrefU.Visible = False
        '
        '_TabPref_TabPage1
        '
        Me._TabPref_TabPage1.Controls.Add(Me.PagesCliniques)
        Me._TabPref_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._TabPref_TabPage1.Name = "_TabPref_TabPage1"
        Me._TabPref_TabPage1.Size = New System.Drawing.Size(689, 351)
        Me._TabPref_TabPage1.TabIndex = 1
        Me._TabPref_TabPage1.Text = "Générales"
        Me._TabPref_TabPage1.UseVisualStyleBackColor = True
        '
        'PagesCliniques
        '
        Me.PagesCliniques.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueAffichage)
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueAutres)
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueBD)
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueCompteclient)
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueFacturation)
        Me.PagesCliniques.Controls.Add(Me.PageCliniquePrinting)
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueKP)
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueRendezvous)
        Me.PagesCliniques.Controls.Add(Me.PageCliniqueUtilisateur)
        Me.PagesCliniques.Location = New System.Drawing.Point(0, 0)
        Me.PagesCliniques.Name = "PagesCliniques"
        Me.PagesCliniques.SelectedIndex = 0
        Me.PagesCliniques.Size = New System.Drawing.Size(689, 350)
        Me.PagesCliniques.TabIndex = 4
        '
        'PageCliniqueAffichage
        '
        Me.PageCliniqueAffichage.AutoScroll = True
        Me.PageCliniqueAffichage.Controls.Add(Me.GroupBox5)
        Me.PageCliniqueAffichage.Controls.Add(Me.textShowedCharByCharVertically)
        Me.PageCliniqueAffichage.Controls.Add(Me._Frames_7)
        Me.PageCliniqueAffichage.Controls.Add(Me.label46)
        Me.PageCliniqueAffichage.Controls.Add(Me.label44)
        Me.PageCliniqueAffichage.Controls.Add(Me.colorObjActivatedBySoftware)
        Me.PageCliniqueAffichage.Controls.Add(Me._Frames_5)
        Me.PageCliniqueAffichage.Controls.Add(Me.colorTitreFactureSouffrance)
        Me.PageCliniqueAffichage.Controls.Add(Me.GroupBox1)
        Me.PageCliniqueAffichage.Controls.Add(Me.FrameConfirmation)
        Me.PageCliniqueAffichage.Controls.Add(Me._Frames_4)
        Me.PageCliniqueAffichage.Controls.Add(Me.groupBox9)
        Me.PageCliniqueAffichage.Controls.Add(Me.frameColorsQL)
        Me.PageCliniqueAffichage.Controls.Add(Me.label37)
        Me.PageCliniqueAffichage.Controls.Add(Me.typeAffListeFenetres)
        Me.PageCliniqueAffichage.Location = New System.Drawing.Point(4, 26)
        Me.PageCliniqueAffichage.Name = "PageCliniqueAffichage"
        Me.PageCliniqueAffichage.Size = New System.Drawing.Size(681, 320)
        Me.PageCliniqueAffichage.TabIndex = 2
        Me.PageCliniqueAffichage.Text = "Affichage"
        Me.PageCliniqueAffichage.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.colorSpecialDates)
        Me.GroupBox5.Controls.Add(Me.affSpecialDatesInCalendar)
        Me.GroupBox5.Controls.Add(Me.label71)
        Me.GroupBox5.Controls.Add(Me.affSpecialDatesInAgenda)
        Me.GroupBox5.Location = New System.Drawing.Point(3, 308)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(305, 87)
        Me.GroupBox5.TabIndex = 145
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Journées spéciales"
        '
        'colorSpecialDates
        '
        Me.colorSpecialDates.BackColor = System.Drawing.Color.YellowGreen
        Me.colorSpecialDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorSpecialDates.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorSpecialDates.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorSpecialDates.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorSpecialDates.Location = New System.Drawing.Point(269, 59)
        Me.colorSpecialDates.Name = "colorSpecialDates"
        Me.colorSpecialDates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorSpecialDates.Size = New System.Drawing.Size(25, 17)
        Me.colorSpecialDates.TabIndex = 76
        Me.colorSpecialDates.TabStop = False
        Me.colorSpecialDates.Tag = "134"
        '
        'affSpecialDatesInCalendar
        '
        Me.affSpecialDatesInCalendar.AutoSize = True
        Me.affSpecialDatesInCalendar.BackColor = System.Drawing.SystemColors.Control
        Me.affSpecialDatesInCalendar.Checked = True
        Me.affSpecialDatesInCalendar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.affSpecialDatesInCalendar.Cursor = System.Windows.Forms.Cursors.Default
        Me.affSpecialDatesInCalendar.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affSpecialDatesInCalendar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affSpecialDatesInCalendar.Location = New System.Drawing.Point(6, 19)
        Me.affSpecialDatesInCalendar.Name = "affSpecialDatesInCalendar"
        Me.affSpecialDatesInCalendar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.affSpecialDatesInCalendar.Size = New System.Drawing.Size(266, 18)
        Me.affSpecialDatesInCalendar.TabIndex = 76
        Me.affSpecialDatesInCalendar.Tag = "132"
        Me.affSpecialDatesInCalendar.Text = "Afficher les journées spéciales dans le calendrier"
        Me.affSpecialDatesInCalendar.UseVisualStyleBackColor = False
        '
        'label71
        '
        Me.label71.AutoSize = True
        Me.label71.BackColor = System.Drawing.SystemColors.Control
        Me.label71.Cursor = System.Windows.Forms.Cursors.Default
        Me.label71.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label71.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label71.Location = New System.Drawing.Point(6, 61)
        Me.label71.Name = "label71"
        Me.label71.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label71.Size = New System.Drawing.Size(259, 14)
        Me.label71.TabIndex = 75
        Me.label71.Text = "Couleur de fond (calendrier) ou d'écriture (agenda) :"
        '
        'affSpecialDatesInAgenda
        '
        Me.affSpecialDatesInAgenda.AutoSize = True
        Me.affSpecialDatesInAgenda.BackColor = System.Drawing.SystemColors.Control
        Me.affSpecialDatesInAgenda.Checked = True
        Me.affSpecialDatesInAgenda.CheckState = System.Windows.Forms.CheckState.Checked
        Me.affSpecialDatesInAgenda.Cursor = System.Windows.Forms.Cursors.Default
        Me.affSpecialDatesInAgenda.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affSpecialDatesInAgenda.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affSpecialDatesInAgenda.Location = New System.Drawing.Point(6, 37)
        Me.affSpecialDatesInAgenda.Name = "affSpecialDatesInAgenda"
        Me.affSpecialDatesInAgenda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.affSpecialDatesInAgenda.Size = New System.Drawing.Size(247, 18)
        Me.affSpecialDatesInAgenda.TabIndex = 76
        Me.affSpecialDatesInAgenda.Tag = "133"
        Me.affSpecialDatesInAgenda.Text = "Afficher les journées spéciales dans l'agenda"
        Me.affSpecialDatesInAgenda.UseVisualStyleBackColor = False
        '
        'textShowedCharByCharVertically
        '
        Me.textShowedCharByCharVertically.BackColor = System.Drawing.SystemColors.Control
        Me.textShowedCharByCharVertically.Checked = True
        Me.textShowedCharByCharVertically.CheckState = System.Windows.Forms.CheckState.Checked
        Me.textShowedCharByCharVertically.Cursor = System.Windows.Forms.Cursors.Default
        Me.textShowedCharByCharVertically.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textShowedCharByCharVertically.ForeColor = System.Drawing.SystemColors.ControlText
        Me.textShowedCharByCharVertically.Location = New System.Drawing.Point(13, 258)
        Me.textShowedCharByCharVertically.Name = "textShowedCharByCharVertically"
        Me.textShowedCharByCharVertically.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.textShowedCharByCharVertically.Size = New System.Drawing.Size(283, 53)
        Me.textShowedCharByCharVertically.TabIndex = 76
        Me.textShowedCharByCharVertically.Tag = "124"
        Me.textShowedCharByCharVertically.Text = "Afficher les lettres une au-dessus de l'autre au lieu d'un texte tourné à 90° pou" & _
            "r les objets principaux dans la barre de côté"
        Me.textShowedCharByCharVertically.UseVisualStyleBackColor = False
        '
        '_Frames_7
        '
        Me._Frames_7.BackColor = System.Drawing.SystemColors.Control
        Me._Frames_7.Controls.Add(Me.aListColors1)
        Me._Frames_7.Controls.Add(Me.aListColors5)
        Me._Frames_7.Controls.Add(Me.aListColors3)
        Me._Frames_7.Controls.Add(Me.aListColors4)
        Me._Frames_7.Controls.Add(Me.aListColors2)
        Me._Frames_7.Controls.Add(Me.aListColors6)
        Me._Frames_7.Controls.Add(Me._Labels_32)
        Me._Frames_7.Controls.Add(Me._Labels_30)
        Me._Frames_7.Controls.Add(Me._Labels_29)
        Me._Frames_7.Controls.Add(Me._Labels_28)
        Me._Frames_7.Controls.Add(Me._Labels_27)
        Me._Frames_7.Controls.Add(Me._Labels_26)
        Me._Frames_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Frames_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frames_7.Location = New System.Drawing.Point(324, 184)
        Me._Frames_7.Name = "_Frames_7"
        Me._Frames_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Frames_7.Size = New System.Drawing.Size(336, 65)
        Me._Frames_7.TabIndex = 96
        Me._Frames_7.TabStop = False
        Me._Frames_7.Text = "Couleurs des listes affichant des rendez-vous"
        '
        '_Labels_32
        '
        Me._Labels_32.AutoSize = True
        Me._Labels_32.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_32.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_32.Location = New System.Drawing.Point(8, 16)
        Me._Labels_32.Name = "_Labels_32"
        Me._Labels_32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_32.Size = New System.Drawing.Size(37, 14)
        Me._Labels_32.TabIndex = 108
        Me._Labels_32.Text = "Fond :"
        '
        '_Labels_30
        '
        Me._Labels_30.AutoSize = True
        Me._Labels_30.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_30.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_30.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_30.Location = New System.Drawing.Point(158, 41)
        Me._Labels_30.Name = "_Labels_30"
        Me._Labels_30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_30.Size = New System.Drawing.Size(52, 14)
        Me._Labels_30.TabIndex = 107
        Me._Labels_30.Text = "Bordure :"
        '
        '_Labels_29
        '
        Me._Labels_29.AutoSize = True
        Me._Labels_29.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_29.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_29.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_29.Location = New System.Drawing.Point(253, 16)
        Me._Labels_29.Name = "_Labels_29"
        Me._Labels_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_29.Size = New System.Drawing.Size(46, 14)
        Me._Labels_29.TabIndex = 106
        Me._Labels_29.Text = "Barres :"
        '
        '_Labels_28
        '
        Me._Labels_28.AutoSize = True
        Me._Labels_28.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_28.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_28.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_28.Location = New System.Drawing.Point(8, 40)
        Me._Labels_28.Name = "_Labels_28"
        Me._Labels_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_28.Size = New System.Drawing.Size(57, 14)
        Me._Labels_28.TabIndex = 105
        Me._Labels_28.Text = "Sélection :"
        '
        '_Labels_27
        '
        Me._Labels_27.AutoSize = True
        Me._Labels_27.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_27.Location = New System.Drawing.Point(103, 16)
        Me._Labels_27.Name = "_Labels_27"
        Me._Labels_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_27.Size = New System.Drawing.Size(107, 14)
        Me._Labels_27.TabIndex = 104
        Me._Labels_27.Text = "Flèches des barres :"
        '
        '_Labels_26
        '
        Me._Labels_26.AutoSize = True
        Me._Labels_26.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_26.Location = New System.Drawing.Point(254, 41)
        Me._Labels_26.Name = "_Labels_26"
        Me._Labels_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_26.Size = New System.Drawing.Size(41, 14)
        Me._Labels_26.TabIndex = 103
        Me._Labels_26.Text = "Police :"
        '
        'label46
        '
        Me.label46.AutoSize = True
        Me.label46.BackColor = System.Drawing.SystemColors.Control
        Me.label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.label46.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label46.Location = New System.Drawing.Point(9, 192)
        Me.label46.Name = "label46"
        Me.label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label46.Size = New System.Drawing.Size(242, 14)
        Me.label46.TabIndex = 108
        Me.label46.Text = "Couleur pour l'activation d'un objet par le logiciel :"
        '
        'label44
        '
        Me.label44.AutoSize = True
        Me.label44.BackColor = System.Drawing.SystemColors.Control
        Me.label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.label44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label44.Location = New System.Drawing.Point(9, 167)
        Me.label44.Name = "label44"
        Me.label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label44.Size = New System.Drawing.Size(256, 14)
        Me.label44.TabIndex = 108
        Me.label44.Text = "Couleur pour les titres des factures en souffrance :"
        '
        'colorObjActivatedBySoftware
        '
        Me.colorObjActivatedBySoftware.BackColor = System.Drawing.Color.DarkOrange
        Me.colorObjActivatedBySoftware.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorObjActivatedBySoftware.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorObjActivatedBySoftware.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorObjActivatedBySoftware.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorObjActivatedBySoftware.Location = New System.Drawing.Point(271, 191)
        Me.colorObjActivatedBySoftware.Name = "colorObjActivatedBySoftware"
        Me.colorObjActivatedBySoftware.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorObjActivatedBySoftware.Size = New System.Drawing.Size(25, 17)
        Me.colorObjActivatedBySoftware.TabIndex = 100
        Me.colorObjActivatedBySoftware.TabStop = False
        Me.colorObjActivatedBySoftware.Tag = "116"
        '
        '_Frames_5
        '
        Me._Frames_5.BackColor = System.Drawing.SystemColors.Control
        Me._Frames_5.Controls.Add(Me.colors7)
        Me._Frames_5.Controls.Add(Me._Labels_16)
        Me._Frames_5.Controls.Add(Me.label40)
        Me._Frames_5.Controls.Add(Me.colorHoraireClose)
        Me._Frames_5.Controls.Add(Me.colors3)
        Me._Frames_5.Controls.Add(Me._Labels_18)
        Me._Frames_5.Controls.Add(Me._Labels_17)
        Me._Frames_5.Controls.Add(Me.colors5)
        Me._Frames_5.Controls.Add(Me.label62)
        Me._Frames_5.Controls.Add(Me._Labels_19)
        Me._Frames_5.Controls.Add(Me.label59)
        Me._Frames_5.Controls.Add(Me.label41)
        Me._Frames_5.Controls.Add(Me.colorPresencePayee)
        Me._Frames_5.Controls.Add(Me._Labels_20)
        Me._Frames_5.Controls.Add(Me._Labels_15)
        Me._Frames_5.Controls.Add(Me.colors4)
        Me._Frames_5.Controls.Add(Me.colors1)
        Me._Frames_5.Controls.Add(Me._Labels_14)
        Me._Frames_5.Controls.Add(Me.colorHoraireOpen)
        Me._Frames_5.Controls.Add(Me.colors2)
        Me._Frames_5.Controls.Add(Me.colorBloquee)
        Me._Frames_5.Controls.Add(Me.colors6)
        Me._Frames_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Frames_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frames_5.Location = New System.Drawing.Point(324, 251)
        Me._Frames_5.Name = "_Frames_5"
        Me._Frames_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Frames_5.Size = New System.Drawing.Size(336, 159)
        Me._Frames_5.TabIndex = 70
        Me._Frames_5.TabStop = False
        Me._Frames_5.Text = "Code de couleur des plages horaires"
        '
        '_Labels_16
        '
        Me._Labels_16.AutoSize = True
        Me._Labels_16.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_16.Location = New System.Drawing.Point(6, 16)
        Me._Labels_16.Name = "_Labels_16"
        Me._Labels_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_16.Size = New System.Drawing.Size(78, 14)
        Me._Labels_16.TabIndex = 75
        Me._Labels_16.Text = "Rendez-vous :"
        '
        'label40
        '
        Me.label40.AutoSize = True
        Me.label40.BackColor = System.Drawing.SystemColors.Control
        Me.label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.label40.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label40.Location = New System.Drawing.Point(5, 62)
        Me.label40.Name = "label40"
        Me.label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label40.Size = New System.Drawing.Size(92, 14)
        Me.label40.TabIndex = 77
        Me.label40.Text = "Présence payée :"
        '
        'colorHoraireClose
        '
        Me.colorHoraireClose.BackColor = System.Drawing.Color.AliceBlue
        Me.colorHoraireClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorHoraireClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorHoraireClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorHoraireClose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorHoraireClose.Location = New System.Drawing.Point(305, 132)
        Me.colorHoraireClose.Name = "colorHoraireClose"
        Me.colorHoraireClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorHoraireClose.Size = New System.Drawing.Size(25, 17)
        Me.colorHoraireClose.TabIndex = 82
        Me.colorHoraireClose.TabStop = False
        Me.colorHoraireClose.Tag = "50"
        '
        '_Labels_18
        '
        Me._Labels_18.AutoSize = True
        Me._Labels_18.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_18.Location = New System.Drawing.Point(6, 109)
        Me._Labels_18.Name = "_Labels_18"
        Me._Labels_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_18.Size = New System.Drawing.Size(118, 14)
        Me._Labels_18.TabIndex = 79
        Me._Labels_18.Text = "Absence non motivée :"
        '
        '_Labels_17
        '
        Me._Labels_17.AutoSize = True
        Me._Labels_17.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_17.Location = New System.Drawing.Point(6, 39)
        Me._Labels_17.Name = "_Labels_17"
        Me._Labels_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_17.Size = New System.Drawing.Size(59, 14)
        Me._Labels_17.TabIndex = 77
        Me._Labels_17.Text = "Présence :"
        '
        'label62
        '
        Me.label62.AutoSize = True
        Me.label62.BackColor = System.Drawing.SystemColors.Control
        Me.label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.label62.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label62.Location = New System.Drawing.Point(205, 131)
        Me.label62.Name = "label62"
        Me.label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label62.Size = New System.Drawing.Size(49, 14)
        Me.label62.TabIndex = 81
        Me.label62.Text = "Fermée :"
        '
        '_Labels_19
        '
        Me._Labels_19.AutoSize = True
        Me._Labels_19.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_19.Location = New System.Drawing.Point(205, 85)
        Me._Labels_19.Name = "_Labels_19"
        Me._Labels_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_19.Size = New System.Drawing.Size(60, 14)
        Me._Labels_19.TabIndex = 81
        Me._Labels_19.Text = "Réservée :"
        '
        'label59
        '
        Me.label59.AutoSize = True
        Me.label59.BackColor = System.Drawing.SystemColors.Control
        Me.label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.label59.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label59.Location = New System.Drawing.Point(205, 108)
        Me.label59.Name = "label59"
        Me.label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label59.Size = New System.Drawing.Size(46, 14)
        Me.label59.TabIndex = 72
        Me.label59.Text = "Ouvert :"
        '
        'label41
        '
        Me.label41.AutoSize = True
        Me.label41.BackColor = System.Drawing.SystemColors.Control
        Me.label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.label41.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label41.Location = New System.Drawing.Point(205, 62)
        Me.label41.Name = "label41"
        Me.label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label41.Size = New System.Drawing.Size(52, 14)
        Me.label41.TabIndex = 72
        Me.label41.Text = "Bloquée :"
        '
        'colorPresencePayee
        '
        Me.colorPresencePayee.BackColor = System.Drawing.Color.White
        Me.colorPresencePayee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorPresencePayee.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorPresencePayee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorPresencePayee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorPresencePayee.Location = New System.Drawing.Point(127, 62)
        Me.colorPresencePayee.Name = "colorPresencePayee"
        Me.colorPresencePayee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorPresencePayee.Size = New System.Drawing.Size(25, 17)
        Me.colorPresencePayee.TabIndex = 78
        Me.colorPresencePayee.TabStop = False
        Me.colorPresencePayee.Tag = "104"
        '
        '_Labels_20
        '
        Me._Labels_20.AutoSize = True
        Me._Labels_20.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_20.Location = New System.Drawing.Point(6, 85)
        Me._Labels_20.Name = "_Labels_20"
        Me._Labels_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_20.Size = New System.Drawing.Size(97, 14)
        Me._Labels_20.TabIndex = 83
        Me._Labels_20.Text = "Absence motivée :"
        '
        '_Labels_15
        '
        Me._Labels_15.AutoSize = True
        Me._Labels_15.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_15.Location = New System.Drawing.Point(205, 16)
        Me._Labels_15.Name = "_Labels_15"
        Me._Labels_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_15.Size = New System.Drawing.Size(37, 14)
        Me._Labels_15.TabIndex = 73
        Me._Labels_15.Text = "Libre :"
        '
        '_Labels_14
        '
        Me._Labels_14.AutoSize = True
        Me._Labels_14.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_14.Location = New System.Drawing.Point(205, 39)
        Me._Labels_14.Name = "_Labels_14"
        Me._Labels_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_14.Size = New System.Drawing.Size(50, 14)
        Me._Labels_14.TabIndex = 72
        Me._Labels_14.Text = "Clinique :"
        '
        'colorHoraireOpen
        '
        Me.colorHoraireOpen.BackColor = System.Drawing.Color.Blue
        Me.colorHoraireOpen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorHoraireOpen.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorHoraireOpen.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorHoraireOpen.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorHoraireOpen.Location = New System.Drawing.Point(305, 108)
        Me.colorHoraireOpen.Name = "colorHoraireOpen"
        Me.colorHoraireOpen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorHoraireOpen.Size = New System.Drawing.Size(25, 17)
        Me.colorHoraireOpen.TabIndex = 71
        Me.colorHoraireOpen.TabStop = False
        Me.colorHoraireOpen.Tag = "103"
        '
        'colorBloquee
        '
        Me.colorBloquee.BackColor = System.Drawing.Color.DarkGray
        Me.colorBloquee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorBloquee.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorBloquee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorBloquee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorBloquee.Location = New System.Drawing.Point(305, 62)
        Me.colorBloquee.Name = "colorBloquee"
        Me.colorBloquee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorBloquee.Size = New System.Drawing.Size(25, 17)
        Me.colorBloquee.TabIndex = 71
        Me.colorBloquee.TabStop = False
        Me.colorBloquee.Tag = "103"
        '
        'colorTitreFactureSouffrance
        '
        Me.colorTitreFactureSouffrance.BackColor = System.Drawing.Color.Red
        Me.colorTitreFactureSouffrance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorTitreFactureSouffrance.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorTitreFactureSouffrance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorTitreFactureSouffrance.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorTitreFactureSouffrance.Location = New System.Drawing.Point(271, 166)
        Me.colorTitreFactureSouffrance.Name = "colorTitreFactureSouffrance"
        Me.colorTitreFactureSouffrance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorTitreFactureSouffrance.Size = New System.Drawing.Size(25, 17)
        Me.colorTitreFactureSouffrance.TabIndex = 100
        Me.colorTitreFactureSouffrance.TabStop = False
        Me.colorTitreFactureSouffrance.Tag = "112"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.newAlertGras)
        Me.GroupBox1.Controls.Add(Me.label48)
        Me.GroupBox1.Controls.Add(Me.newAlertItalic)
        Me.GroupBox1.Controls.Add(Me.newAlertStrike)
        Me.GroupBox1.Controls.Add(Me.newAlertUnder)
        Me.GroupBox1.Controls.Add(Me.newRvFont)
        Me.GroupBox1.Controls.Add(Me.label49)
        Me.GroupBox1.Controls.Add(Me.newAlertFontSize)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 75)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(304, 82)
        Me.GroupBox1.TabIndex = 144
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Style des nouvelles entrées dans la msg. instant. && Notes"
        '
        'newAlertGras
        '
        Me.newAlertGras.BackColor = System.Drawing.SystemColors.Control
        Me.newAlertGras.Checked = True
        Me.newAlertGras.CheckState = System.Windows.Forms.CheckState.Checked
        Me.newAlertGras.Cursor = System.Windows.Forms.Cursors.Default
        Me.newAlertGras.Enabled = False
        Me.newAlertGras.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newAlertGras.ForeColor = System.Drawing.SystemColors.ControlText
        Me.newAlertGras.Location = New System.Drawing.Point(14, 58)
        Me.newAlertGras.Name = "newAlertGras"
        Me.newAlertGras.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.newAlertGras.Size = New System.Drawing.Size(58, 17)
        Me.newAlertGras.TabIndex = 76
        Me.newAlertGras.Tag = "119"
        Me.newAlertGras.Text = "Gras"
        Me.newAlertGras.UseVisualStyleBackColor = False
        '
        'label48
        '
        Me.label48.AutoSize = True
        Me.label48.BackColor = System.Drawing.SystemColors.Control
        Me.label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.label48.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label48.Location = New System.Drawing.Point(6, 16)
        Me.label48.Name = "label48"
        Me.label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label48.Size = New System.Drawing.Size(84, 14)
        Me.label48.TabIndex = 47
        Me.label48.Text = "Type d'écriture :"
        '
        'newAlertItalic
        '
        Me.newAlertItalic.BackColor = System.Drawing.SystemColors.Control
        Me.newAlertItalic.Cursor = System.Windows.Forms.Cursors.Default
        Me.newAlertItalic.Enabled = False
        Me.newAlertItalic.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newAlertItalic.ForeColor = System.Drawing.SystemColors.ControlText
        Me.newAlertItalic.Location = New System.Drawing.Point(78, 58)
        Me.newAlertItalic.Name = "newAlertItalic"
        Me.newAlertItalic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.newAlertItalic.Size = New System.Drawing.Size(72, 17)
        Me.newAlertItalic.TabIndex = 77
        Me.newAlertItalic.Tag = "120"
        Me.newAlertItalic.Text = "Italique"
        Me.newAlertItalic.UseVisualStyleBackColor = False
        '
        'newAlertStrike
        '
        Me.newAlertStrike.BackColor = System.Drawing.SystemColors.Control
        Me.newAlertStrike.Cursor = System.Windows.Forms.Cursors.Default
        Me.newAlertStrike.Enabled = False
        Me.newAlertStrike.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newAlertStrike.ForeColor = System.Drawing.SystemColors.ControlText
        Me.newAlertStrike.Location = New System.Drawing.Point(238, 58)
        Me.newAlertStrike.Name = "newAlertStrike"
        Me.newAlertStrike.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.newAlertStrike.Size = New System.Drawing.Size(56, 17)
        Me.newAlertStrike.TabIndex = 79
        Me.newAlertStrike.Tag = "122"
        Me.newAlertStrike.Text = "Barré"
        Me.newAlertStrike.UseVisualStyleBackColor = False
        '
        'newAlertUnder
        '
        Me.newAlertUnder.BackColor = System.Drawing.SystemColors.Control
        Me.newAlertUnder.Cursor = System.Windows.Forms.Cursors.Default
        Me.newAlertUnder.Enabled = False
        Me.newAlertUnder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newAlertUnder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.newAlertUnder.Location = New System.Drawing.Point(158, 58)
        Me.newAlertUnder.Name = "newAlertUnder"
        Me.newAlertUnder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.newAlertUnder.Size = New System.Drawing.Size(72, 17)
        Me.newAlertUnder.TabIndex = 78
        Me.newAlertUnder.Tag = "121"
        Me.newAlertUnder.Text = "Souligné"
        Me.newAlertUnder.UseVisualStyleBackColor = False
        '
        'newRvFont
        '
        Me.newRvFont.BackColor = System.Drawing.SystemColors.Control
        Me.newRvFont.Cursor = System.Windows.Forms.Cursors.Default
        Me.newRvFont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newRvFont.ForeColor = System.Drawing.SystemColors.ControlText
        Me.newRvFont.Location = New System.Drawing.Point(91, 16)
        Me.newRvFont.Name = "newRvFont"
        Me.newRvFont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.newRvFont.Size = New System.Drawing.Size(204, 17)
        Me.newRvFont.TabIndex = 48
        Me.newRvFont.Tag = "118"
        Me.newRvFont.Text = "Aucune"
        Me.newRvFont.UseVisualStyleBackColor = False
        '
        'label49
        '
        Me.label49.AutoSize = True
        Me.label49.BackColor = System.Drawing.SystemColors.Control
        Me.label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.label49.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label49.Location = New System.Drawing.Point(6, 39)
        Me.label49.Name = "label49"
        Me.label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label49.Size = New System.Drawing.Size(107, 14)
        Me.label49.TabIndex = 48
        Me.label49.Text = "Grosseur d'écriture :"
        '
        'newAlertFontSize
        '
        Me.newAlertFontSize.acceptAlpha = False
        Me.newAlertFontSize.acceptedChars = ",§."
        Me.newAlertFontSize.acceptNumeric = True
        Me.newAlertFontSize.AcceptsReturn = True
        Me.newAlertFontSize.allCapital = False
        Me.newAlertFontSize.allLower = False
        Me.newAlertFontSize.BackColor = System.Drawing.SystemColors.Window
        Me.newAlertFontSize.blockOnMaximum = False
        Me.newAlertFontSize.blockOnMinimum = False
        Me.newAlertFontSize.cb_AcceptLeftZeros = False
        Me.newAlertFontSize.cb_AcceptNegative = False
        Me.newAlertFontSize.currencyBox = True
        Me.newAlertFontSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.newAlertFontSize.firstLetterCapital = False
        Me.newAlertFontSize.firstLettersCapital = False
        Me.newAlertFontSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newAlertFontSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.newAlertFontSize.Location = New System.Drawing.Point(119, 36)
        Me.newAlertFontSize.manageText = True
        Me.newAlertFontSize.matchExp = ""
        Me.newAlertFontSize.maximum = 0
        Me.newAlertFontSize.MaxLength = 0
        Me.newAlertFontSize.minimum = 0
        Me.newAlertFontSize.Name = "newAlertFontSize"
        Me.newAlertFontSize.nbDecimals = CType(2, Short)
        Me.newAlertFontSize.onlyAlphabet = False
        Me.newAlertFontSize.refuseAccents = False
        Me.newAlertFontSize.refusedChars = ""
        Me.newAlertFontSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.newAlertFontSize.showInternalContextMenu = True
        Me.newAlertFontSize.Size = New System.Drawing.Size(24, 20)
        Me.newAlertFontSize.TabIndex = 113
        Me.newAlertFontSize.Tag = "123"
        Me.newAlertFontSize.Text = "10"
        Me.newAlertFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.newAlertFontSize.trimText = False
        '
        'FrameConfirmation
        '
        Me.FrameConfirmation.Controls.Add(Me.rvEvalGras)
        Me.FrameConfirmation.Controls.Add(Me.label29)
        Me.FrameConfirmation.Controls.Add(Me.rvEvalItalique)
        Me.FrameConfirmation.Controls.Add(Me.rvEvalBarre)
        Me.FrameConfirmation.Controls.Add(Me.rvEvalSouligne)
        Me.FrameConfirmation.Controls.Add(Me.rvEvalFont)
        Me.FrameConfirmation.Location = New System.Drawing.Point(3, 5)
        Me.FrameConfirmation.Name = "FrameConfirmation"
        Me.FrameConfirmation.Size = New System.Drawing.Size(304, 64)
        Me.FrameConfirmation.TabIndex = 144
        Me.FrameConfirmation.TabStop = False
        Me.FrameConfirmation.Text = "Style des rendez-vous de type ""Évaluation"""
        '
        'rvEvalGras
        '
        Me.rvEvalGras.BackColor = System.Drawing.SystemColors.Control
        Me.rvEvalGras.Checked = True
        Me.rvEvalGras.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rvEvalGras.Cursor = System.Windows.Forms.Cursors.Default
        Me.rvEvalGras.Enabled = False
        Me.rvEvalGras.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rvEvalGras.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rvEvalGras.Location = New System.Drawing.Point(14, 40)
        Me.rvEvalGras.Name = "rvEvalGras"
        Me.rvEvalGras.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rvEvalGras.Size = New System.Drawing.Size(58, 17)
        Me.rvEvalGras.TabIndex = 76
        Me.rvEvalGras.Tag = "89"
        Me.rvEvalGras.Text = "Gras"
        Me.rvEvalGras.UseVisualStyleBackColor = False
        '
        'label29
        '
        Me.label29.AutoSize = True
        Me.label29.BackColor = System.Drawing.SystemColors.Control
        Me.label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.label29.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label29.Location = New System.Drawing.Point(6, 16)
        Me.label29.Name = "label29"
        Me.label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label29.Size = New System.Drawing.Size(84, 14)
        Me.label29.TabIndex = 47
        Me.label29.Text = "Type d'écriture :"
        '
        'rvEvalItalique
        '
        Me.rvEvalItalique.BackColor = System.Drawing.SystemColors.Control
        Me.rvEvalItalique.Cursor = System.Windows.Forms.Cursors.Default
        Me.rvEvalItalique.Enabled = False
        Me.rvEvalItalique.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rvEvalItalique.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rvEvalItalique.Location = New System.Drawing.Point(78, 40)
        Me.rvEvalItalique.Name = "rvEvalItalique"
        Me.rvEvalItalique.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rvEvalItalique.Size = New System.Drawing.Size(72, 17)
        Me.rvEvalItalique.TabIndex = 77
        Me.rvEvalItalique.Tag = "90"
        Me.rvEvalItalique.Text = "Italique"
        Me.rvEvalItalique.UseVisualStyleBackColor = False
        '
        'rvEvalBarre
        '
        Me.rvEvalBarre.BackColor = System.Drawing.SystemColors.Control
        Me.rvEvalBarre.Cursor = System.Windows.Forms.Cursors.Default
        Me.rvEvalBarre.Enabled = False
        Me.rvEvalBarre.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rvEvalBarre.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rvEvalBarre.Location = New System.Drawing.Point(238, 40)
        Me.rvEvalBarre.Name = "rvEvalBarre"
        Me.rvEvalBarre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rvEvalBarre.Size = New System.Drawing.Size(56, 17)
        Me.rvEvalBarre.TabIndex = 79
        Me.rvEvalBarre.Tag = "92"
        Me.rvEvalBarre.Text = "Barré"
        Me.rvEvalBarre.UseVisualStyleBackColor = False
        '
        'rvEvalSouligne
        '
        Me.rvEvalSouligne.BackColor = System.Drawing.SystemColors.Control
        Me.rvEvalSouligne.Cursor = System.Windows.Forms.Cursors.Default
        Me.rvEvalSouligne.Enabled = False
        Me.rvEvalSouligne.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rvEvalSouligne.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rvEvalSouligne.Location = New System.Drawing.Point(158, 40)
        Me.rvEvalSouligne.Name = "rvEvalSouligne"
        Me.rvEvalSouligne.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rvEvalSouligne.Size = New System.Drawing.Size(72, 17)
        Me.rvEvalSouligne.TabIndex = 78
        Me.rvEvalSouligne.Tag = "91"
        Me.rvEvalSouligne.Text = "Souligné"
        Me.rvEvalSouligne.UseVisualStyleBackColor = False
        '
        'rvEvalFont
        '
        Me.rvEvalFont.BackColor = System.Drawing.SystemColors.Control
        Me.rvEvalFont.Cursor = System.Windows.Forms.Cursors.Default
        Me.rvEvalFont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rvEvalFont.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rvEvalFont.Location = New System.Drawing.Point(91, 16)
        Me.rvEvalFont.Name = "rvEvalFont"
        Me.rvEvalFont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rvEvalFont.Size = New System.Drawing.Size(204, 17)
        Me.rvEvalFont.TabIndex = 48
        Me.rvEvalFont.Tag = "88"
        Me.rvEvalFont.Text = "Aucune"
        Me.rvEvalFont.UseVisualStyleBackColor = False
        '
        '_Frames_4
        '
        Me._Frames_4.BackColor = System.Drawing.SystemColors.Control
        Me._Frames_4.Controls.Add(Me.listFont)
        Me._Frames_4.Controls.Add(Me.listColors4)
        Me._Frames_4.Controls.Add(Me.listMarge)
        Me._Frames_4.Controls.Add(Me._Labels_2)
        Me._Frames_4.Controls.Add(Me._Labels_13)
        Me._Frames_4.Controls.Add(Me.listBorder)
        Me._Frames_4.Controls.Add(Me._Labels_12)
        Me._Frames_4.Controls.Add(Me.listFontSize)
        Me._Frames_4.Controls.Add(Me.listColors1)
        Me._Frames_4.Controls.Add(Me._Labels_11)
        Me._Frames_4.Controls.Add(Me.listBarre)
        Me._Frames_4.Controls.Add(Me.listColors2)
        Me._Frames_4.Controls.Add(Me._Labels_10)
        Me._Frames_4.Controls.Add(Me.listSouligne)
        Me._Frames_4.Controls.Add(Me.listColors3)
        Me._Frames_4.Controls.Add(Me._Labels_9)
        Me._Frames_4.Controls.Add(Me.listItalique)
        Me._Frames_4.Controls.Add(Me._Labels_8)
        Me._Frames_4.Controls.Add(Me.listGras)
        Me._Frames_4.Controls.Add(Me.listColors5)
        Me._Frames_4.Controls.Add(Me._Labels_7)
        Me._Frames_4.Controls.Add(Me.listMouseSpeed)
        Me._Frames_4.Controls.Add(Me.listColors6)
        Me._Frames_4.Controls.Add(Me._Labels_3)
        Me._Frames_4.Controls.Add(Me._Labels_6)
        Me._Frames_4.Controls.Add(Me.mouseOver3DOpt)
        Me._Frames_4.Controls.Add(Me.listColors7)
        Me._Frames_4.Controls.Add(Me._Labels_4)
        Me._Frames_4.Controls.Add(Me._Labels_5)
        Me._Frames_4.Controls.Add(Me.do3DOpt)
        Me._Frames_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Frames_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frames_4.Location = New System.Drawing.Point(324, 5)
        Me._Frames_4.Name = "_Frames_4"
        Me._Frames_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Frames_4.Size = New System.Drawing.Size(336, 176)
        Me._Frames_4.TabIndex = 45
        Me._Frames_4.TabStop = False
        Me._Frames_4.Text = "Liste en générale"
        '
        'listFont
        '
        Me.listFont.BackColor = System.Drawing.SystemColors.Control
        Me.listFont.Cursor = System.Windows.Forms.Cursors.Default
        Me.listFont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listFont.ForeColor = System.Drawing.SystemColors.ControlText
        Me.listFont.Location = New System.Drawing.Point(88, 13)
        Me.listFont.Name = "listFont"
        Me.listFont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listFont.Size = New System.Drawing.Size(242, 17)
        Me.listFont.TabIndex = 47
        Me.listFont.Tag = "32"
        Me.listFont.Text = "Aucune"
        Me.listFont.UseVisualStyleBackColor = False
        '
        'listMarge
        '
        Me.listMarge.acceptAlpha = False
        Me.listMarge.acceptedChars = ",§."
        Me.listMarge.acceptNumeric = True
        Me.listMarge.AcceptsReturn = True
        Me.listMarge.allCapital = False
        Me.listMarge.allLower = False
        Me.listMarge.BackColor = System.Drawing.SystemColors.Window
        Me.listMarge.blockOnMaximum = False
        Me.listMarge.blockOnMinimum = False
        Me.listMarge.cb_AcceptLeftZeros = False
        Me.listMarge.cb_AcceptNegative = False
        Me.listMarge.currencyBox = True
        Me.listMarge.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.listMarge.firstLetterCapital = False
        Me.listMarge.firstLettersCapital = False
        Me.listMarge.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listMarge.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listMarge.Location = New System.Drawing.Point(302, 29)
        Me.listMarge.manageText = True
        Me.listMarge.matchExp = ""
        Me.listMarge.maximum = 0
        Me.listMarge.MaxLength = 0
        Me.listMarge.minimum = 0
        Me.listMarge.Name = "listMarge"
        Me.listMarge.nbDecimals = CType(0, Short)
        Me.listMarge.onlyAlphabet = False
        Me.listMarge.refuseAccents = False
        Me.listMarge.refusedChars = ""
        Me.listMarge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listMarge.showInternalContextMenu = True
        Me.listMarge.Size = New System.Drawing.Size(24, 20)
        Me.listMarge.TabIndex = 113
        Me.listMarge.Tag = "35"
        Me.listMarge.Text = "1"
        Me.listMarge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.listMarge.trimText = False
        '
        '_Labels_2
        '
        Me._Labels_2.AutoSize = True
        Me._Labels_2.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_2.Location = New System.Drawing.Point(6, 13)
        Me._Labels_2.Name = "_Labels_2"
        Me._Labels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_2.Size = New System.Drawing.Size(84, 14)
        Me._Labels_2.TabIndex = 46
        Me._Labels_2.Text = "Type d'écriture :"
        '
        '_Labels_13
        '
        Me._Labels_13.AutoSize = True
        Me._Labels_13.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_13.Location = New System.Drawing.Point(253, 149)
        Me._Labels_13.Name = "_Labels_13"
        Me._Labels_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_13.Size = New System.Drawing.Size(41, 14)
        Me._Labels_13.TabIndex = 68
        Me._Labels_13.Text = "Police :"
        '
        'listBorder
        '
        Me.listBorder.acceptAlpha = False
        Me.listBorder.acceptedChars = ",§."
        Me.listBorder.acceptNumeric = True
        Me.listBorder.AcceptsReturn = True
        Me.listBorder.allCapital = False
        Me.listBorder.allLower = False
        Me.listBorder.BackColor = System.Drawing.SystemColors.Window
        Me.listBorder.blockOnMaximum = False
        Me.listBorder.blockOnMinimum = False
        Me.listBorder.cb_AcceptLeftZeros = False
        Me.listBorder.cb_AcceptNegative = False
        Me.listBorder.currencyBox = True
        Me.listBorder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.listBorder.firstLetterCapital = False
        Me.listBorder.firstLettersCapital = False
        Me.listBorder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listBorder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listBorder.Location = New System.Drawing.Point(208, 29)
        Me.listBorder.manageText = True
        Me.listBorder.matchExp = ""
        Me.listBorder.maximum = 0
        Me.listBorder.MaxLength = 0
        Me.listBorder.minimum = 0
        Me.listBorder.Name = "listBorder"
        Me.listBorder.nbDecimals = CType(0, Short)
        Me.listBorder.onlyAlphabet = False
        Me.listBorder.refuseAccents = False
        Me.listBorder.refusedChars = ""
        Me.listBorder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listBorder.showInternalContextMenu = True
        Me.listBorder.Size = New System.Drawing.Size(24, 20)
        Me.listBorder.TabIndex = 113
        Me.listBorder.Tag = "34"
        Me.listBorder.Text = "0"
        Me.listBorder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.listBorder.trimText = False
        '
        '_Labels_12
        '
        Me._Labels_12.AutoSize = True
        Me._Labels_12.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_12.Location = New System.Drawing.Point(107, 149)
        Me._Labels_12.Name = "_Labels_12"
        Me._Labels_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_12.Size = New System.Drawing.Size(107, 14)
        Me._Labels_12.TabIndex = 66
        Me._Labels_12.Text = "Flèches des barres :"
        '
        'listFontSize
        '
        Me.listFontSize.acceptAlpha = False
        Me.listFontSize.acceptedChars = ",§."
        Me.listFontSize.acceptNumeric = True
        Me.listFontSize.AcceptsReturn = True
        Me.listFontSize.allCapital = False
        Me.listFontSize.allLower = False
        Me.listFontSize.BackColor = System.Drawing.SystemColors.Window
        Me.listFontSize.blockOnMaximum = False
        Me.listFontSize.blockOnMinimum = False
        Me.listFontSize.cb_AcceptLeftZeros = False
        Me.listFontSize.cb_AcceptNegative = False
        Me.listFontSize.currencyBox = True
        Me.listFontSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.listFontSize.firstLetterCapital = False
        Me.listFontSize.firstLettersCapital = False
        Me.listFontSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listFontSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listFontSize.Location = New System.Drawing.Point(119, 29)
        Me.listFontSize.manageText = True
        Me.listFontSize.matchExp = ""
        Me.listFontSize.maximum = 0
        Me.listFontSize.MaxLength = 0
        Me.listFontSize.minimum = 0
        Me.listFontSize.Name = "listFontSize"
        Me.listFontSize.nbDecimals = CType(2, Short)
        Me.listFontSize.onlyAlphabet = False
        Me.listFontSize.refuseAccents = False
        Me.listFontSize.refusedChars = ""
        Me.listFontSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listFontSize.showInternalContextMenu = True
        Me.listFontSize.Size = New System.Drawing.Size(24, 20)
        Me.listFontSize.TabIndex = 113
        Me.listFontSize.Tag = "33"
        Me.listFontSize.Text = "8"
        Me.listFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.listFontSize.trimText = False
        '
        '_Labels_11
        '
        Me._Labels_11.AutoSize = True
        Me._Labels_11.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_11.Location = New System.Drawing.Point(6, 149)
        Me._Labels_11.Name = "_Labels_11"
        Me._Labels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_11.Size = New System.Drawing.Size(57, 14)
        Me._Labels_11.TabIndex = 64
        Me._Labels_11.Text = "Sélection :"
        '
        'listBarre
        '
        Me.listBarre.AutoSize = True
        Me.listBarre.BackColor = System.Drawing.SystemColors.Control
        Me.listBarre.Cursor = System.Windows.Forms.Cursors.Default
        Me.listBarre.Enabled = False
        Me.listBarre.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listBarre.ForeColor = System.Drawing.SystemColors.ControlText
        Me.listBarre.Location = New System.Drawing.Point(276, 91)
        Me.listBarre.Name = "listBarre"
        Me.listBarre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listBarre.Size = New System.Drawing.Size(53, 18)
        Me.listBarre.TabIndex = 75
        Me.listBarre.Tag = "46"
        Me.listBarre.Text = "Barré"
        Me.listBarre.UseVisualStyleBackColor = False
        '
        '_Labels_10
        '
        Me._Labels_10.AutoSize = True
        Me._Labels_10.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_10.Location = New System.Drawing.Point(253, 125)
        Me._Labels_10.Name = "_Labels_10"
        Me._Labels_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_10.Size = New System.Drawing.Size(46, 14)
        Me._Labels_10.TabIndex = 62
        Me._Labels_10.Text = "Barres :"
        '
        'listSouligne
        '
        Me.listSouligne.AutoSize = True
        Me.listSouligne.BackColor = System.Drawing.SystemColors.Control
        Me.listSouligne.Cursor = System.Windows.Forms.Cursors.Default
        Me.listSouligne.Enabled = False
        Me.listSouligne.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listSouligne.ForeColor = System.Drawing.SystemColors.ControlText
        Me.listSouligne.Location = New System.Drawing.Point(209, 91)
        Me.listSouligne.Name = "listSouligne"
        Me.listSouligne.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listSouligne.Size = New System.Drawing.Size(67, 18)
        Me.listSouligne.TabIndex = 74
        Me.listSouligne.Tag = "47"
        Me.listSouligne.Text = "Souligné"
        Me.listSouligne.UseVisualStyleBackColor = False
        '
        '_Labels_9
        '
        Me._Labels_9.AutoSize = True
        Me._Labels_9.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_9.Location = New System.Drawing.Point(162, 125)
        Me._Labels_9.Name = "_Labels_9"
        Me._Labels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_9.Size = New System.Drawing.Size(52, 14)
        Me._Labels_9.TabIndex = 60
        Me._Labels_9.Text = "Bordure :"
        '
        'listItalique
        '
        Me.listItalique.AutoSize = True
        Me.listItalique.BackColor = System.Drawing.SystemColors.Control
        Me.listItalique.Cursor = System.Windows.Forms.Cursors.Default
        Me.listItalique.Enabled = False
        Me.listItalique.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listItalique.ForeColor = System.Drawing.SystemColors.ControlText
        Me.listItalique.Location = New System.Drawing.Point(150, 91)
        Me.listItalique.Name = "listItalique"
        Me.listItalique.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listItalique.Size = New System.Drawing.Size(59, 18)
        Me.listItalique.TabIndex = 73
        Me.listItalique.Tag = "45"
        Me.listItalique.Text = "Italique"
        Me.listItalique.UseVisualStyleBackColor = False
        '
        '_Labels_8
        '
        Me._Labels_8.AutoSize = True
        Me._Labels_8.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_8.Location = New System.Drawing.Point(96, 125)
        Me._Labels_8.Name = "_Labels_8"
        Me._Labels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_8.Size = New System.Drawing.Size(38, 14)
        Me._Labels_8.TabIndex = 58
        Me._Labels_8.Text = "Case :"
        '
        'listGras
        '
        Me.listGras.AutoSize = True
        Me.listGras.BackColor = System.Drawing.SystemColors.Control
        Me.listGras.Cursor = System.Windows.Forms.Cursors.Default
        Me.listGras.Enabled = False
        Me.listGras.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listGras.ForeColor = System.Drawing.SystemColors.ControlText
        Me.listGras.Location = New System.Drawing.Point(100, 91)
        Me.listGras.Name = "listGras"
        Me.listGras.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listGras.Size = New System.Drawing.Size(50, 18)
        Me.listGras.TabIndex = 72
        Me.listGras.Tag = "44"
        Me.listGras.Text = "Gras"
        Me.listGras.UseVisualStyleBackColor = False
        '
        '_Labels_7
        '
        Me._Labels_7.AutoSize = True
        Me._Labels_7.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_7.Location = New System.Drawing.Point(6, 125)
        Me._Labels_7.Name = "_Labels_7"
        Me._Labels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_7.Size = New System.Drawing.Size(37, 14)
        Me._Labels_7.TabIndex = 56
        Me._Labels_7.Text = "Fond :"
        '
        'listMouseSpeed
        '
        Me.listMouseSpeed.Cursor = System.Windows.Forms.Cursors.Default
        Me.listMouseSpeed.LargeChange = 50
        Me.listMouseSpeed.Location = New System.Drawing.Point(6, 70)
        Me.listMouseSpeed.Maximum = 449
        Me.listMouseSpeed.Name = "listMouseSpeed"
        Me.listMouseSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listMouseSpeed.Size = New System.Drawing.Size(320, 17)
        Me.listMouseSpeed.SmallChange = 10
        Me.listMouseSpeed.TabIndex = 55
        Me.listMouseSpeed.TabStop = True
        Me.listMouseSpeed.Tag = "36"
        Me.listMouseSpeed.Value = 50
        '
        '_Labels_3
        '
        Me._Labels_3.AutoSize = True
        Me._Labels_3.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_3.Location = New System.Drawing.Point(6, 29)
        Me._Labels_3.Name = "_Labels_3"
        Me._Labels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_3.Size = New System.Drawing.Size(107, 14)
        Me._Labels_3.TabIndex = 48
        Me._Labels_3.Text = "Grosseur d'écriture :"
        '
        '_Labels_6
        '
        Me._Labels_6.AutoSize = True
        Me._Labels_6.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_6.Location = New System.Drawing.Point(6, 52)
        Me._Labels_6.Name = "_Labels_6"
        Me._Labels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_6.Size = New System.Drawing.Size(204, 14)
        Me._Labels_6.TabIndex = 54
        Me._Labels_6.Text = "Vitesse du déroulement avec la roulette :"
        '
        'mouseOver3DOpt
        '
        Me.mouseOver3DOpt.BackColor = System.Drawing.SystemColors.Control
        Me.mouseOver3DOpt.Cursor = System.Windows.Forms.Cursors.Default
        Me.mouseOver3DOpt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mouseOver3DOpt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.mouseOver3DOpt.Location = New System.Drawing.Point(46, 92)
        Me.mouseOver3DOpt.Name = "mouseOver3DOpt"
        Me.mouseOver3DOpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mouseOver3DOpt.Size = New System.Drawing.Size(48, 17)
        Me.mouseOver3DOpt.TabIndex = 71
        Me.mouseOver3DOpt.Tag = "64"
        Me.mouseOver3DOpt.Text = "3D+"
        Me.mouseOver3DOpt.UseVisualStyleBackColor = False
        '
        '_Labels_4
        '
        Me._Labels_4.AutoSize = True
        Me._Labels_4.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_4.Location = New System.Drawing.Point(155, 29)
        Me._Labels_4.Name = "_Labels_4"
        Me._Labels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_4.Size = New System.Drawing.Size(52, 14)
        Me._Labels_4.TabIndex = 50
        Me._Labels_4.Text = "Bordure :"
        '
        '_Labels_5
        '
        Me._Labels_5.AutoSize = True
        Me._Labels_5.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_5.Location = New System.Drawing.Point(262, 29)
        Me._Labels_5.Name = "_Labels_5"
        Me._Labels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_5.Size = New System.Drawing.Size(43, 14)
        Me._Labels_5.TabIndex = 52
        Me._Labels_5.Text = "Marge :"
        '
        'do3DOpt
        '
        Me.do3DOpt.BackColor = System.Drawing.SystemColors.Control
        Me.do3DOpt.Cursor = System.Windows.Forms.Cursors.Default
        Me.do3DOpt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.do3DOpt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.do3DOpt.Location = New System.Drawing.Point(6, 92)
        Me.do3DOpt.Name = "do3DOpt"
        Me.do3DOpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.do3DOpt.Size = New System.Drawing.Size(40, 17)
        Me.do3DOpt.TabIndex = 70
        Me.do3DOpt.Tag = "63"
        Me.do3DOpt.Text = "3D"
        Me.do3DOpt.UseVisualStyleBackColor = False
        '
        'groupBox9
        '
        Me.groupBox9.BackColor = System.Drawing.SystemColors.Control
        Me.groupBox9.Controls.Add(Me.colorCommSent)
        Me.groupBox9.Controls.Add(Me.label73)
        Me.groupBox9.Controls.Add(Me.label74)
        Me.groupBox9.Controls.Add(Me.colorCommReceived)
        Me.groupBox9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupBox9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.groupBox9.Location = New System.Drawing.Point(3, 401)
        Me.groupBox9.Name = "groupBox9"
        Me.groupBox9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.groupBox9.Size = New System.Drawing.Size(304, 40)
        Me.groupBox9.TabIndex = 138
        Me.groupBox9.TabStop = False
        Me.groupBox9.Text = "Couleurs pour les types de communications"
        '
        'colorCommSent
        '
        Me.colorCommSent.BackColor = System.Drawing.Color.LightGray
        Me.colorCommSent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorCommSent.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorCommSent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorCommSent.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorCommSent.Location = New System.Drawing.Point(51, 15)
        Me.colorCommSent.Name = "colorCommSent"
        Me.colorCommSent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorCommSent.Size = New System.Drawing.Size(25, 17)
        Me.colorCommSent.TabIndex = 102
        Me.colorCommSent.TabStop = False
        Me.colorCommSent.Tag = "20"
        '
        'label73
        '
        Me.label73.AutoSize = True
        Me.label73.BackColor = System.Drawing.SystemColors.Control
        Me.label73.Cursor = System.Windows.Forms.Cursors.Default
        Me.label73.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label73.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label73.Location = New System.Drawing.Point(6, 16)
        Me.label73.Name = "label73"
        Me.label73.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label73.Size = New System.Drawing.Size(39, 14)
        Me.label73.TabIndex = 108
        Me.label73.Text = "Envoi :"
        '
        'label74
        '
        Me.label74.AutoSize = True
        Me.label74.BackColor = System.Drawing.SystemColors.Control
        Me.label74.Cursor = System.Windows.Forms.Cursors.Default
        Me.label74.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label74.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label74.Location = New System.Drawing.Point(202, 16)
        Me.label74.Name = "label74"
        Me.label74.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label74.Size = New System.Drawing.Size(61, 14)
        Me.label74.TabIndex = 106
        Me.label74.Text = "Réception :"
        '
        'colorCommReceived
        '
        Me.colorCommReceived.BackColor = System.Drawing.Color.White
        Me.colorCommReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorCommReceived.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorCommReceived.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorCommReceived.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorCommReceived.Location = New System.Drawing.Point(269, 15)
        Me.colorCommReceived.Name = "colorCommReceived"
        Me.colorCommReceived.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorCommReceived.Size = New System.Drawing.Size(25, 17)
        Me.colorCommReceived.TabIndex = 100
        Me.colorCommReceived.TabStop = False
        Me.colorCommReceived.Tag = "17"
        '
        'frameColorsQL
        '
        Me.frameColorsQL.BackColor = System.Drawing.SystemColors.Control
        Me.frameColorsQL.Controls.Add(Me.colorWithoutRV)
        Me.frameColorsQL.Controls.Add(Me.label24)
        Me.frameColorsQL.Controls.Add(Me.label25)
        Me.frameColorsQL.Controls.Add(Me.colorWithRV)
        Me.frameColorsQL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameColorsQL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameColorsQL.Location = New System.Drawing.Point(324, 416)
        Me.frameColorsQL.Name = "frameColorsQL"
        Me.frameColorsQL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameColorsQL.Size = New System.Drawing.Size(336, 40)
        Me.frameColorsQL.TabIndex = 138
        Me.frameColorsQL.TabStop = False
        Me.frameColorsQL.Text = "Couleurs pour la liste d'attente"
        '
        'colorWithoutRV
        '
        Me.colorWithoutRV.BackColor = System.Drawing.Color.LightGray
        Me.colorWithoutRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorWithoutRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorWithoutRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorWithoutRV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorWithoutRV.Location = New System.Drawing.Point(306, 16)
        Me.colorWithoutRV.Name = "colorWithoutRV"
        Me.colorWithoutRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorWithoutRV.Size = New System.Drawing.Size(25, 17)
        Me.colorWithoutRV.TabIndex = 102
        Me.colorWithoutRV.TabStop = False
        Me.colorWithoutRV.Tag = "20"
        '
        'label24
        '
        Me.label24.AutoSize = True
        Me.label24.BackColor = System.Drawing.SystemColors.Control
        Me.label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.label24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label24.Location = New System.Drawing.Point(6, 16)
        Me.label24.Name = "label24"
        Me.label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label24.Size = New System.Drawing.Size(104, 14)
        Me.label24.TabIndex = 108
        Me.label24.Text = "Avec rendez-vous :"
        '
        'label25
        '
        Me.label25.AutoSize = True
        Me.label25.BackColor = System.Drawing.SystemColors.Control
        Me.label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.label25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label25.Location = New System.Drawing.Point(202, 16)
        Me.label25.Name = "label25"
        Me.label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label25.Size = New System.Drawing.Size(103, 14)
        Me.label25.TabIndex = 106
        Me.label25.Text = "Sans rendez-vous :"
        '
        'colorWithRV
        '
        Me.colorWithRV.BackColor = System.Drawing.Color.White
        Me.colorWithRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorWithRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorWithRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorWithRV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.colorWithRV.Location = New System.Drawing.Point(127, 16)
        Me.colorWithRV.Name = "colorWithRV"
        Me.colorWithRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorWithRV.Size = New System.Drawing.Size(25, 17)
        Me.colorWithRV.TabIndex = 100
        Me.colorWithRV.TabStop = False
        Me.colorWithRV.Tag = "17"
        '
        'label37
        '
        Me.label37.AutoSize = True
        Me.label37.BackColor = System.Drawing.SystemColors.Control
        Me.label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.label37.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label37.Location = New System.Drawing.Point(10, 213)
        Me.label37.Name = "label37"
        Me.label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label37.Size = New System.Drawing.Size(206, 14)
        Me.label37.TabIndex = 133
        Me.label37.Text = "Type d'affichage de la liste des fenêtres :"
        '
        'typeAffListeFenetres
        '
        Me.typeAffListeFenetres.BackColor = System.Drawing.SystemColors.Window
        Me.typeAffListeFenetres.Cursor = System.Windows.Forms.Cursors.Default
        Me.typeAffListeFenetres.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.typeAffListeFenetres.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.typeAffListeFenetres.ForeColor = System.Drawing.SystemColors.WindowText
        Me.typeAffListeFenetres.Items.AddRange(New Object() {"Dans une liste déroulante", "Dans un menu"})
        Me.typeAffListeFenetres.Location = New System.Drawing.Point(8, 230)
        Me.typeAffListeFenetres.Name = "typeAffListeFenetres"
        Me.typeAffListeFenetres.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.typeAffListeFenetres.Size = New System.Drawing.Size(288, 22)
        Me.typeAffListeFenetres.TabIndex = 137
        Me.typeAffListeFenetres.Tag = "93"
        '
        'PageCliniqueAutres
        '
        Me.PageCliniqueAutres.Controls.Add(Me.defaultEquipementType)
        Me.PageCliniqueAutres.Controls.Add(Me.GroupBox11)
        Me.PageCliniqueAutres.Controls.Add(Me.GroupBox10)
        Me.PageCliniqueAutres.Controls.Add(Me.GroupBox7)
        Me.PageCliniqueAutres.Controls.Add(Me.label12)
        Me.PageCliniqueAutres.Controls.Add(Me.nbSecondsForPing)
        Me.PageCliniqueAutres.Controls.Add(Me.label11)
        Me.PageCliniqueAutres.Controls.Add(Me.label10)
        Me.PageCliniqueAutres.Controls.Add(Me.label61)
        Me.PageCliniqueAutres.Controls.Add(Me.intervalToShowTextInStatusBar)
        Me.PageCliniqueAutres.Location = New System.Drawing.Point(4, 25)
        Me.PageCliniqueAutres.Name = "PageCliniqueAutres"
        Me.PageCliniqueAutres.Padding = New System.Windows.Forms.Padding(3)
        Me.PageCliniqueAutres.Size = New System.Drawing.Size(681, 321)
        Me.PageCliniqueAutres.TabIndex = 0
        Me.PageCliniqueAutres.Text = "Autres"
        Me.PageCliniqueAutres.UseVisualStyleBackColor = True
        '
        'defaultEquipementType
        '
        Me.defaultEquipementType.BackColor = System.Drawing.SystemColors.Window
        Me.defaultEquipementType.Cursor = System.Windows.Forms.Cursors.Default
        Me.defaultEquipementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.defaultEquipementType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.defaultEquipementType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.defaultEquipementType.Items.AddRange(New Object() {"Prêt", "Vente", "Prêt & Vente", "Inventaire"})
        Me.defaultEquipementType.Location = New System.Drawing.Point(-1, 251)
        Me.defaultEquipementType.Name = "defaultEquipementType"
        Me.defaultEquipementType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.defaultEquipementType.Size = New System.Drawing.Size(304, 22)
        Me.defaultEquipementType.TabIndex = 130
        Me.defaultEquipementType.Tag = "78"
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.autoupdateOnStart)
        Me.GroupBox11.Location = New System.Drawing.Point(0, 175)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(681, 45)
        Me.GroupBox11.TabIndex = 138
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Mise à jour du logiciel"
        '
        'autoupdateOnStart
        '
        Me.autoupdateOnStart.AutoSize = True
        Me.autoupdateOnStart.BackColor = System.Drawing.SystemColors.Control
        Me.autoupdateOnStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoupdateOnStart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoupdateOnStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoupdateOnStart.Location = New System.Drawing.Point(6, 16)
        Me.autoupdateOnStart.Name = "autoupdateOnStart"
        Me.autoupdateOnStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoupdateOnStart.Size = New System.Drawing.Size(565, 18)
        Me.autoupdateOnStart.TabIndex = 31
        Me.autoupdateOnStart.Tag = "105"
        Me.autoupdateOnStart.Text = "Mettre à jour automatiquement le logiciel au démarrage (Ne bloque jamais les mise" & _
            "s à jour des postes seulement)"
        Me.autoupdateOnStart.UseVisualStyleBackColor = False
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.label31)
        Me.GroupBox10.Controls.Add(Me.htmlEditorPath)
        Me.GroupBox10.Controls.Add(Me.label22)
        Me.GroupBox10.Controls.Add(Me.ancre)
        Me.GroupBox10.Controls.Add(Me.AutoSelectAncre)
        Me.GroupBox10.Location = New System.Drawing.Point(0, 82)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(681, 87)
        Me.GroupBox10.TabIndex = 138
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Éditeur HTML"
        '
        'label31
        '
        Me.label31.AutoSize = True
        Me.label31.BackColor = System.Drawing.SystemColors.Control
        Me.label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.label31.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label31.Location = New System.Drawing.Point(6, 16)
        Me.label31.Name = "label31"
        Me.label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label31.Size = New System.Drawing.Size(118, 14)
        Me.label31.TabIndex = 133
        Me.label31.Text = "URL de l'éditeur HTML :"
        Me.ToolTip1.SetToolTip(Me.label31, "0 jour signifie aucun ajout du rendez-vous en liste d'attente")
        '
        'htmlEditorPath
        '
        Me.htmlEditorPath.acceptAlpha = True
        Me.htmlEditorPath.acceptedChars = ""
        Me.htmlEditorPath.acceptNumeric = True
        Me.htmlEditorPath.AcceptsReturn = True
        Me.htmlEditorPath.allCapital = False
        Me.htmlEditorPath.allLower = False
        Me.htmlEditorPath.BackColor = System.Drawing.SystemColors.Window
        Me.htmlEditorPath.blockOnMaximum = False
        Me.htmlEditorPath.blockOnMinimum = False
        Me.htmlEditorPath.cb_AcceptLeftZeros = False
        Me.htmlEditorPath.cb_AcceptNegative = False
        Me.htmlEditorPath.currencyBox = False
        Me.htmlEditorPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.htmlEditorPath.firstLetterCapital = False
        Me.htmlEditorPath.firstLettersCapital = False
        Me.htmlEditorPath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.htmlEditorPath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.htmlEditorPath.Location = New System.Drawing.Point(7, 33)
        Me.htmlEditorPath.manageText = True
        Me.htmlEditorPath.matchExp = ""
        Me.htmlEditorPath.maximum = 0
        Me.htmlEditorPath.MaxLength = 0
        Me.htmlEditorPath.minimum = 0
        Me.htmlEditorPath.Name = "htmlEditorPath"
        Me.htmlEditorPath.nbDecimals = CType(0, Short)
        Me.htmlEditorPath.onlyAlphabet = False
        Me.htmlEditorPath.refuseAccents = False
        Me.htmlEditorPath.refusedChars = ""
        Me.htmlEditorPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.htmlEditorPath.showInternalContextMenu = True
        Me.htmlEditorPath.Size = New System.Drawing.Size(304, 20)
        Me.htmlEditorPath.TabIndex = 123
        Me.htmlEditorPath.Tag = "94"
        Me.htmlEditorPath.Text = "http://localhost/WebTextBox/WebTextPage.aspx"
        Me.htmlEditorPath.trimText = False
        '
        'label22
        '
        Me.label22.AutoSize = True
        Me.label22.BackColor = System.Drawing.SystemColors.Control
        Me.label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.label22.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label22.Location = New System.Drawing.Point(322, 16)
        Me.label22.Name = "label22"
        Me.label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label22.Size = New System.Drawing.Size(199, 14)
        Me.label22.TabIndex = 132
        Me.label22.Text = "Symbole de l'ancre des boîtes de texte :"
        '
        'ancre
        '
        Me.ancre.acceptAlpha = True
        Me.ancre.acceptedChars = ""
        Me.ancre.acceptNumeric = True
        Me.ancre.AcceptsReturn = True
        Me.ancre.allCapital = False
        Me.ancre.allLower = False
        Me.ancre.BackColor = System.Drawing.SystemColors.Window
        Me.ancre.blockOnMaximum = False
        Me.ancre.blockOnMinimum = False
        Me.ancre.cb_AcceptLeftZeros = False
        Me.ancre.cb_AcceptNegative = False
        Me.ancre.currencyBox = False
        Me.ancre.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ancre.firstLetterCapital = False
        Me.ancre.firstLettersCapital = False
        Me.ancre.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ancre.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ancre.Location = New System.Drawing.Point(527, 13)
        Me.ancre.manageText = True
        Me.ancre.matchExp = ""
        Me.ancre.maximum = 0
        Me.ancre.MaxLength = 0
        Me.ancre.minimum = 0
        Me.ancre.Name = "ancre"
        Me.ancre.nbDecimals = CType(0, Short)
        Me.ancre.onlyAlphabet = False
        Me.ancre.refuseAccents = False
        Me.ancre.refusedChars = ""
        Me.ancre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ancre.showInternalContextMenu = True
        Me.ancre.Size = New System.Drawing.Size(88, 20)
        Me.ancre.TabIndex = 123
        Me.ancre.Tag = "86"
        Me.ancre.Text = "-->"
        Me.ancre.trimText = False
        '
        'AutoSelectAncre
        '
        Me.AutoSelectAncre.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AutoSelectAncre.Checked = True
        Me.AutoSelectAncre.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoSelectAncre.Location = New System.Drawing.Point(321, 37)
        Me.AutoSelectAncre.Name = "AutoSelectAncre"
        Me.AutoSelectAncre.Size = New System.Drawing.Size(355, 49)
        Me.AutoSelectAncre.TabIndex = 137
        Me.AutoSelectAncre.Tag = "100"
        Me.AutoSelectAncre.Text = "Sélectionner l'ancre automatiquement lors de l'enfoncement combiné des touches Co" & _
            "ntrôle (CTRL) et Majuscule (SHIFT, la touche au dessus de CTRL) "
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.label72)
        Me.GroupBox7.Controls.Add(Me.label68)
        Me.GroupBox7.Controls.Add(Me.publipostageSendingInterval)
        Me.GroupBox7.Controls.Add(Me.publipostageNbSending)
        Me.GroupBox7.Location = New System.Drawing.Point(0, 34)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(681, 42)
        Me.GroupBox7.TabIndex = 138
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Publipostage"
        '
        'label72
        '
        Me.label72.AutoSize = True
        Me.label72.BackColor = System.Drawing.SystemColors.Control
        Me.label72.Cursor = System.Windows.Forms.Cursors.Default
        Me.label72.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label72.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label72.Location = New System.Drawing.Point(321, 19)
        Me.label72.Name = "label72"
        Me.label72.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label72.Size = New System.Drawing.Size(168, 14)
        Me.label72.TabIndex = 132
        Me.label72.Text = "Interval en minutes entre les lots :"
        '
        'label68
        '
        Me.label68.AutoSize = True
        Me.label68.BackColor = System.Drawing.SystemColors.Control
        Me.label68.Cursor = System.Windows.Forms.Cursors.Default
        Me.label68.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label68.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label68.Location = New System.Drawing.Point(6, 19)
        Me.label68.Name = "label68"
        Me.label68.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label68.Size = New System.Drawing.Size(197, 14)
        Me.label68.TabIndex = 132
        Me.label68.Text = "Nombre d'envoi pour un lot (0 = illimité) :"
        '
        'publipostageSendingInterval
        '
        Me.publipostageSendingInterval.acceptAlpha = False
        Me.publipostageSendingInterval.acceptedChars = ""
        Me.publipostageSendingInterval.acceptNumeric = True
        Me.publipostageSendingInterval.AcceptsReturn = True
        Me.publipostageSendingInterval.allCapital = False
        Me.publipostageSendingInterval.allLower = False
        Me.publipostageSendingInterval.BackColor = System.Drawing.SystemColors.Window
        Me.publipostageSendingInterval.blockOnMaximum = False
        Me.publipostageSendingInterval.blockOnMinimum = False
        Me.publipostageSendingInterval.cb_AcceptLeftZeros = False
        Me.publipostageSendingInterval.cb_AcceptNegative = False
        Me.publipostageSendingInterval.currencyBox = True
        Me.publipostageSendingInterval.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.publipostageSendingInterval.firstLetterCapital = False
        Me.publipostageSendingInterval.firstLettersCapital = False
        Me.publipostageSendingInterval.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.publipostageSendingInterval.ForeColor = System.Drawing.SystemColors.WindowText
        Me.publipostageSendingInterval.Location = New System.Drawing.Point(526, 16)
        Me.publipostageSendingInterval.manageText = True
        Me.publipostageSendingInterval.matchExp = ""
        Me.publipostageSendingInterval.maximum = 600
        Me.publipostageSendingInterval.MaxLength = 0
        Me.publipostageSendingInterval.minimum = 0
        Me.publipostageSendingInterval.Name = "publipostageSendingInterval"
        Me.publipostageSendingInterval.nbDecimals = CType(0, Short)
        Me.publipostageSendingInterval.onlyAlphabet = False
        Me.publipostageSendingInterval.refuseAccents = False
        Me.publipostageSendingInterval.refusedChars = ""
        Me.publipostageSendingInterval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.publipostageSendingInterval.showInternalContextMenu = True
        Me.publipostageSendingInterval.Size = New System.Drawing.Size(88, 20)
        Me.publipostageSendingInterval.TabIndex = 123
        Me.publipostageSendingInterval.Tag = "86"
        Me.publipostageSendingInterval.Text = "0"
        Me.publipostageSendingInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.publipostageSendingInterval.trimText = False
        '
        'publipostageNbSending
        '
        Me.publipostageNbSending.acceptAlpha = False
        Me.publipostageNbSending.acceptedChars = ""
        Me.publipostageNbSending.acceptNumeric = True
        Me.publipostageNbSending.AcceptsReturn = True
        Me.publipostageNbSending.allCapital = False
        Me.publipostageNbSending.allLower = False
        Me.publipostageNbSending.BackColor = System.Drawing.SystemColors.Window
        Me.publipostageNbSending.blockOnMaximum = False
        Me.publipostageNbSending.blockOnMinimum = False
        Me.publipostageNbSending.cb_AcceptLeftZeros = False
        Me.publipostageNbSending.cb_AcceptNegative = False
        Me.publipostageNbSending.currencyBox = True
        Me.publipostageNbSending.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.publipostageNbSending.firstLetterCapital = False
        Me.publipostageNbSending.firstLettersCapital = False
        Me.publipostageNbSending.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.publipostageNbSending.ForeColor = System.Drawing.SystemColors.WindowText
        Me.publipostageNbSending.Location = New System.Drawing.Point(208, 16)
        Me.publipostageNbSending.manageText = True
        Me.publipostageNbSending.matchExp = ""
        Me.publipostageNbSending.maximum = 0
        Me.publipostageNbSending.MaxLength = 0
        Me.publipostageNbSending.minimum = 0
        Me.publipostageNbSending.Name = "publipostageNbSending"
        Me.publipostageNbSending.nbDecimals = CType(0, Short)
        Me.publipostageNbSending.onlyAlphabet = False
        Me.publipostageNbSending.refuseAccents = False
        Me.publipostageNbSending.refusedChars = ""
        Me.publipostageNbSending.showInternalContextMenu = True
        Me.publipostageNbSending.Size = New System.Drawing.Size(88, 20)
        Me.publipostageNbSending.TabIndex = 123
        Me.publipostageNbSending.Tag = "86"
        Me.publipostageNbSending.Text = "0"
        Me.publipostageNbSending.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.publipostageNbSending.trimText = False
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.BackColor = System.Drawing.SystemColors.Control
        Me.label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.label12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label12.Location = New System.Drawing.Point(-1, 235)
        Me.label12.Name = "label12"
        Me.label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label12.Size = New System.Drawing.Size(155, 14)
        Me.label12.TabIndex = 131
        Me.label12.Text = "Type d'équipement par défaut :"
        '
        'nbSecondsForPing
        '
        Me.nbSecondsForPing.acceptAlpha = False
        Me.nbSecondsForPing.acceptedChars = ",§."
        Me.nbSecondsForPing.acceptNumeric = True
        Me.nbSecondsForPing.AcceptsReturn = True
        Me.nbSecondsForPing.allCapital = False
        Me.nbSecondsForPing.allLower = False
        Me.nbSecondsForPing.BackColor = System.Drawing.SystemColors.Window
        Me.nbSecondsForPing.blockOnMaximum = False
        Me.nbSecondsForPing.blockOnMinimum = False
        Me.nbSecondsForPing.cb_AcceptLeftZeros = False
        Me.nbSecondsForPing.cb_AcceptNegative = False
        Me.nbSecondsForPing.currencyBox = True
        Me.nbSecondsForPing.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nbSecondsForPing.firstLetterCapital = False
        Me.nbSecondsForPing.firstLettersCapital = False
        Me.nbSecondsForPing.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbSecondsForPing.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nbSecondsForPing.Location = New System.Drawing.Point(644, 9)
        Me.nbSecondsForPing.manageText = True
        Me.nbSecondsForPing.matchExp = ""
        Me.nbSecondsForPing.maximum = 0
        Me.nbSecondsForPing.MaxLength = 0
        Me.nbSecondsForPing.minimum = 0
        Me.nbSecondsForPing.Name = "nbSecondsForPing"
        Me.nbSecondsForPing.nbDecimals = CType(0, Short)
        Me.nbSecondsForPing.onlyAlphabet = False
        Me.nbSecondsForPing.refuseAccents = False
        Me.nbSecondsForPing.refusedChars = ""
        Me.nbSecondsForPing.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbSecondsForPing.showInternalContextMenu = True
        Me.nbSecondsForPing.Size = New System.Drawing.Size(31, 20)
        Me.nbSecondsForPing.TabIndex = 123
        Me.nbSecondsForPing.Tag = "130"
        Me.nbSecondsForPing.Text = "30"
        Me.nbSecondsForPing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nbSecondsForPing.trimText = False
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.BackColor = System.Drawing.Color.Transparent
        Me.label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label11.Location = New System.Drawing.Point(6, 12)
        Me.label11.Name = "label11"
        Me.label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label11.Size = New System.Drawing.Size(201, 14)
        Me.label11.TabIndex = 123
        Me.label11.Text = "Interval d'affichage dans la barre d'état :"
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.BackColor = System.Drawing.SystemColors.Control
        Me.label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label10.Location = New System.Drawing.Point(246, 12)
        Me.label10.Name = "label10"
        Me.label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label10.Size = New System.Drawing.Size(63, 14)
        Me.label10.TabIndex = 124
        Me.label10.Text = "seconde(s)"
        '
        'label61
        '
        Me.label61.AutoSize = True
        Me.label61.BackColor = System.Drawing.SystemColors.Control
        Me.label61.Cursor = System.Windows.Forms.Cursors.Default
        Me.label61.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label61.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label61.Location = New System.Drawing.Point(321, 12)
        Me.label61.Name = "label61"
        Me.label61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label61.Size = New System.Drawing.Size(317, 14)
        Me.label61.TabIndex = 132
        Me.label61.Text = "Interval pour le ""ping"" avec le serveur de Clinica (en secondes) :"
        '
        'intervalToShowTextInStatusBar
        '
        Me.intervalToShowTextInStatusBar.acceptAlpha = False
        Me.intervalToShowTextInStatusBar.acceptedChars = ""
        Me.intervalToShowTextInStatusBar.acceptNumeric = True
        Me.intervalToShowTextInStatusBar.AcceptsReturn = True
        Me.intervalToShowTextInStatusBar.allCapital = False
        Me.intervalToShowTextInStatusBar.allLower = False
        Me.intervalToShowTextInStatusBar.BackColor = System.Drawing.SystemColors.Window
        Me.intervalToShowTextInStatusBar.blockOnMaximum = False
        Me.intervalToShowTextInStatusBar.blockOnMinimum = False
        Me.intervalToShowTextInStatusBar.cb_AcceptLeftZeros = False
        Me.intervalToShowTextInStatusBar.cb_AcceptNegative = False
        Me.intervalToShowTextInStatusBar.currencyBox = False
        Me.intervalToShowTextInStatusBar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.intervalToShowTextInStatusBar.firstLetterCapital = False
        Me.intervalToShowTextInStatusBar.firstLettersCapital = False
        Me.intervalToShowTextInStatusBar.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.intervalToShowTextInStatusBar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.intervalToShowTextInStatusBar.Location = New System.Drawing.Point(214, 10)
        Me.intervalToShowTextInStatusBar.manageText = True
        Me.intervalToShowTextInStatusBar.matchExp = ""
        Me.intervalToShowTextInStatusBar.maximum = 0
        Me.intervalToShowTextInStatusBar.MaxLength = 0
        Me.intervalToShowTextInStatusBar.minimum = 0
        Me.intervalToShowTextInStatusBar.Name = "intervalToShowTextInStatusBar"
        Me.intervalToShowTextInStatusBar.nbDecimals = CType(0, Short)
        Me.intervalToShowTextInStatusBar.onlyAlphabet = False
        Me.intervalToShowTextInStatusBar.refuseAccents = False
        Me.intervalToShowTextInStatusBar.refusedChars = ""
        Me.intervalToShowTextInStatusBar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.intervalToShowTextInStatusBar.showInternalContextMenu = True
        Me.intervalToShowTextInStatusBar.Size = New System.Drawing.Size(32, 20)
        Me.intervalToShowTextInStatusBar.TabIndex = 122
        Me.intervalToShowTextInStatusBar.Tag = "70"
        Me.intervalToShowTextInStatusBar.Text = "2"
        Me.intervalToShowTextInStatusBar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.intervalToShowTextInStatusBar.trimText = False
        '
        'PageCliniqueBD
        '
        Me.PageCliniqueBD.Controls.Add(Me.groupAutoSaveRemoteTask)
        Me.PageCliniqueBD.Controls.Add(Me.AskForReplacingDBItem)
        Me.PageCliniqueBD.Controls.Add(Me.ConfirmDBDragDrop)
        Me.PageCliniqueBD.Location = New System.Drawing.Point(4, 25)
        Me.PageCliniqueBD.Name = "PageCliniqueBD"
        Me.PageCliniqueBD.Size = New System.Drawing.Size(681, 321)
        Me.PageCliniqueBD.TabIndex = 7
        Me.PageCliniqueBD.Text = "Banque de données"
        Me.PageCliniqueBD.UseVisualStyleBackColor = True
        '
        'groupAutoSaveRemoteTask
        '
        Me.groupAutoSaveRemoteTask.Controls.Add(Me.panelAutoSaveRemoteTask)
        Me.groupAutoSaveRemoteTask.Location = New System.Drawing.Point(12, 77)
        Me.groupAutoSaveRemoteTask.Name = "groupAutoSaveRemoteTask"
        Me.groupAutoSaveRemoteTask.Size = New System.Drawing.Size(358, 121)
        Me.groupAutoSaveRemoteTask.TabIndex = 127
        Me.groupAutoSaveRemoteTask.TabStop = False
        Me.groupAutoSaveRemoteTask.Text = "Enregistrement automatique des résultats des tâches du serveur"
        '
        'panelAutoSaveRemoteTask
        '
        Me.panelAutoSaveRemoteTask.AutoScroll = True
        Me.panelAutoSaveRemoteTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelAutoSaveRemoteTask.Location = New System.Drawing.Point(3, 16)
        Me.panelAutoSaveRemoteTask.Name = "panelAutoSaveRemoteTask"
        Me.panelAutoSaveRemoteTask.Size = New System.Drawing.Size(352, 102)
        Me.panelAutoSaveRemoteTask.TabIndex = 0
        '
        'AskForReplacingDBItem
        '
        Me.AskForReplacingDBItem.Checked = True
        Me.AskForReplacingDBItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AskForReplacingDBItem.Location = New System.Drawing.Point(12, 39)
        Me.AskForReplacingDBItem.Name = "AskForReplacingDBItem"
        Me.AskForReplacingDBItem.Size = New System.Drawing.Size(428, 32)
        Me.AskForReplacingDBItem.TabIndex = 126
        Me.AskForReplacingDBItem.Tag = "131"
        Me.AskForReplacingDBItem.Text = "Demande de remplacement d'un item existant lors de l'ajout d'un nouvel item du mê" & _
            "me nom dans un même dossier"
        '
        'ConfirmDBDragDrop
        '
        Me.ConfirmDBDragDrop.AutoSize = True
        Me.ConfirmDBDragDrop.Checked = True
        Me.ConfirmDBDragDrop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ConfirmDBDragDrop.Location = New System.Drawing.Point(12, 15)
        Me.ConfirmDBDragDrop.Name = "ConfirmDBDragDrop"
        Me.ConfirmDBDragDrop.Size = New System.Drawing.Size(302, 18)
        Me.ConfirmDBDragDrop.TabIndex = 126
        Me.ConfirmDBDragDrop.Tag = "83"
        Me.ConfirmDBDragDrop.Text = "Confirmation du déplacement dans la banque de données"
        '
        'PageCliniqueCompteclient
        '
        Me.PageCliniqueCompteclient.Controls.Add(Me.PreReferents)
        Me.PageCliniqueCompteclient.Controls.Add(Me._Frames_0)
        Me.PageCliniqueCompteclient.Controls.Add(Me.ConfirmDateAccident)
        Me.PageCliniqueCompteclient.Controls.Add(Me.DemandeAuthorisationCommentsRequired)
        Me.PageCliniqueCompteclient.Controls.Add(Me.ConfirmDateRechute)
        Me.PageCliniqueCompteclient.Controls.Add(Me.label9)
        Me.PageCliniqueCompteclient.Controls.Add(Me.renamingReferent)
        Me.PageCliniqueCompteclient.Controls.Add(Me.removingReferent)
        Me.PageCliniqueCompteclient.Controls.Add(Me.AddingReferent)
        Me.PageCliniqueCompteclient.Location = New System.Drawing.Point(4, 25)
        Me.PageCliniqueCompteclient.Name = "PageCliniqueCompteclient"
        Me.PageCliniqueCompteclient.Size = New System.Drawing.Size(681, 321)
        Me.PageCliniqueCompteclient.TabIndex = 6
        Me.PageCliniqueCompteclient.Text = "Compte client"
        Me.PageCliniqueCompteclient.UseVisualStyleBackColor = True
        '
        'PreReferents
        '
        Me.PreReferents.HorizontalScrollbar = True
        Me.PreReferents.ItemHeight = 14
        Me.PreReferents.Location = New System.Drawing.Point(11, 184)
        Me.PreReferents.Name = "PreReferents"
        Me.PreReferents.Size = New System.Drawing.Size(232, 46)
        Me.PreReferents.Sorted = True
        Me.PreReferents.TabIndex = 139
        Me.PreReferents.Tag = "87"
        '
        '_Frames_0
        '
        Me._Frames_0.BackColor = System.Drawing.SystemColors.Control
        Me._Frames_0.Controls.Add(Me.cocc19)
        Me._Frames_0.Controls.Add(Me.cocc18)
        Me._Frames_0.Controls.Add(Me.cocc2)
        Me._Frames_0.Controls.Add(Me.cocc1)
        Me._Frames_0.Controls.Add(Me.cocc3)
        Me._Frames_0.Controls.Add(Me.cocc4)
        Me._Frames_0.Controls.Add(Me.cocc5)
        Me._Frames_0.Controls.Add(Me.cocc6)
        Me._Frames_0.Controls.Add(Me.cocc9)
        Me._Frames_0.Controls.Add(Me.cocc10)
        Me._Frames_0.Controls.Add(Me.cocc11)
        Me._Frames_0.Controls.Add(Me.cocc12)
        Me._Frames_0.Controls.Add(Me.cocc13)
        Me._Frames_0.Controls.Add(Me.cocc16)
        Me._Frames_0.Controls.Add(Me.cocc17)
        Me._Frames_0.Controls.Add(Me.cocc15)
        Me._Frames_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Frames_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frames_0.Location = New System.Drawing.Point(11, 3)
        Me._Frames_0.Name = "_Frames_0"
        Me._Frames_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Frames_0.Size = New System.Drawing.Size(305, 152)
        Me._Frames_0.TabIndex = 13
        Me._Frames_0.TabStop = False
        Me._Frames_0.Text = "Champs obligatoires - Informations de base"
        '
        'cocc19
        '
        Me.cocc19.BackColor = System.Drawing.SystemColors.Control
        Me.cocc19.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc19.Location = New System.Drawing.Point(136, 96)
        Me.cocc19.Name = "cocc19"
        Me.cocc19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc19.Size = New System.Drawing.Size(144, 17)
        Me.cocc19.TabIndex = 32
        Me.cocc19.Tag = "73"
        Me.cocc19.Text = "Adresse du site internet"
        Me.cocc19.UseVisualStyleBackColor = False
        '
        'cocc18
        '
        Me.cocc18.BackColor = System.Drawing.SystemColors.Control
        Me.cocc18.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc18.Location = New System.Drawing.Point(136, 80)
        Me.cocc18.Name = "cocc18"
        Me.cocc18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc18.Size = New System.Drawing.Size(80, 17)
        Me.cocc18.TabIndex = 31
        Me.cocc18.Tag = "72"
        Me.cocc18.Text = "Courriel"
        Me.cocc18.UseVisualStyleBackColor = False
        '
        'cocc2
        '
        Me.cocc2.BackColor = System.Drawing.SystemColors.Control
        Me.cocc2.Checked = True
        Me.cocc2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc2.Location = New System.Drawing.Point(8, 32)
        Me.cocc2.Name = "cocc2"
        Me.cocc2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc2.Size = New System.Drawing.Size(64, 17)
        Me.cocc2.TabIndex = 30
        Me.cocc2.Tag = "3"
        Me.cocc2.Text = "Prénom"
        Me.cocc2.UseVisualStyleBackColor = False
        '
        'cocc1
        '
        Me.cocc1.BackColor = System.Drawing.SystemColors.Control
        Me.cocc1.Checked = True
        Me.cocc1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc1.Location = New System.Drawing.Point(8, 16)
        Me.cocc1.Name = "cocc1"
        Me.cocc1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc1.Size = New System.Drawing.Size(49, 17)
        Me.cocc1.TabIndex = 29
        Me.cocc1.Tag = "2"
        Me.cocc1.Text = "Nom"
        Me.cocc1.UseVisualStyleBackColor = False
        '
        'cocc3
        '
        Me.cocc3.BackColor = System.Drawing.SystemColors.Control
        Me.cocc3.Checked = True
        Me.cocc3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc3.Location = New System.Drawing.Point(8, 48)
        Me.cocc3.Name = "cocc3"
        Me.cocc3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc3.Size = New System.Drawing.Size(65, 17)
        Me.cocc3.TabIndex = 28
        Me.cocc3.Tag = "4"
        Me.cocc3.Text = "Adresse"
        Me.cocc3.UseVisualStyleBackColor = False
        '
        'cocc4
        '
        Me.cocc4.BackColor = System.Drawing.SystemColors.Control
        Me.cocc4.Checked = True
        Me.cocc4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc4.Location = New System.Drawing.Point(8, 64)
        Me.cocc4.Name = "cocc4"
        Me.cocc4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc4.Size = New System.Drawing.Size(48, 17)
        Me.cocc4.TabIndex = 27
        Me.cocc4.Tag = "5"
        Me.cocc4.Text = "Ville"
        Me.cocc4.UseVisualStyleBackColor = False
        '
        'cocc5
        '
        Me.cocc5.BackColor = System.Drawing.SystemColors.Control
        Me.cocc5.Checked = True
        Me.cocc5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc5.Location = New System.Drawing.Point(8, 80)
        Me.cocc5.Name = "cocc5"
        Me.cocc5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc5.Size = New System.Drawing.Size(88, 17)
        Me.cocc5.TabIndex = 26
        Me.cocc5.Tag = "6"
        Me.cocc5.Text = "Code postal"
        Me.cocc5.UseVisualStyleBackColor = False
        '
        'cocc6
        '
        Me.cocc6.BackColor = System.Drawing.SystemColors.Control
        Me.cocc6.Checked = True
        Me.cocc6.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc6.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc6.Location = New System.Drawing.Point(8, 96)
        Me.cocc6.Name = "cocc6"
        Me.cocc6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc6.Size = New System.Drawing.Size(80, 17)
        Me.cocc6.TabIndex = 25
        Me.cocc6.Tag = "7"
        Me.cocc6.Text = "Téléphones"
        Me.cocc6.UseVisualStyleBackColor = False
        '
        'cocc9
        '
        Me.cocc9.BackColor = System.Drawing.SystemColors.Control
        Me.cocc9.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc9.Location = New System.Drawing.Point(136, 64)
        Me.cocc9.Name = "cocc9"
        Me.cocc9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc9.Size = New System.Drawing.Size(80, 17)
        Me.cocc9.TabIndex = 22
        Me.cocc9.Tag = "10"
        Me.cocc9.Text = "Autre nom"
        Me.cocc9.UseVisualStyleBackColor = False
        '
        'cocc10
        '
        Me.cocc10.BackColor = System.Drawing.SystemColors.Control
        Me.cocc10.Checked = True
        Me.cocc10.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc10.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc10.Location = New System.Drawing.Point(8, 112)
        Me.cocc10.Name = "cocc10"
        Me.cocc10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc10.Size = New System.Drawing.Size(120, 17)
        Me.cocc10.TabIndex = 21
        Me.cocc10.Tag = "11"
        Me.cocc10.Text = "Date de naissance"
        Me.cocc10.UseVisualStyleBackColor = False
        '
        'cocc11
        '
        Me.cocc11.BackColor = System.Drawing.SystemColors.Control
        Me.cocc11.Checked = True
        Me.cocc11.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc11.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc11.Location = New System.Drawing.Point(8, 128)
        Me.cocc11.Name = "cocc11"
        Me.cocc11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc11.Size = New System.Drawing.Size(49, 17)
        Me.cocc11.TabIndex = 20
        Me.cocc11.Tag = "12"
        Me.cocc11.Text = "Sexe"
        Me.cocc11.UseVisualStyleBackColor = False
        '
        'cocc12
        '
        Me.cocc12.BackColor = System.Drawing.SystemColors.Control
        Me.cocc12.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc12.Location = New System.Drawing.Point(136, 16)
        Me.cocc12.Name = "cocc12"
        Me.cocc12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc12.Size = New System.Drawing.Size(80, 17)
        Me.cocc12.TabIndex = 19
        Me.cocc12.Tag = "13"
        Me.cocc12.Text = "Employeur"
        Me.cocc12.UseVisualStyleBackColor = False
        '
        'cocc13
        '
        Me.cocc13.BackColor = System.Drawing.SystemColors.Control
        Me.cocc13.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc13.Location = New System.Drawing.Point(136, 32)
        Me.cocc13.Name = "cocc13"
        Me.cocc13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc13.Size = New System.Drawing.Size(57, 17)
        Me.cocc13.TabIndex = 18
        Me.cocc13.Tag = "14"
        Me.cocc13.Text = "Métier"
        Me.cocc13.UseVisualStyleBackColor = False
        '
        'cocc16
        '
        Me.cocc16.BackColor = System.Drawing.SystemColors.Control
        Me.cocc16.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc16.Location = New System.Drawing.Point(136, 112)
        Me.cocc16.Name = "cocc16"
        Me.cocc16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc16.Size = New System.Drawing.Size(80, 17)
        Me.cocc16.TabIndex = 15
        Me.cocc16.Tag = "18"
        Me.cocc16.Text = "Référence"
        Me.cocc16.UseVisualStyleBackColor = False
        '
        'cocc17
        '
        Me.cocc17.BackColor = System.Drawing.SystemColors.Control
        Me.cocc17.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc17.Location = New System.Drawing.Point(136, 128)
        Me.cocc17.Name = "cocc17"
        Me.cocc17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc17.Size = New System.Drawing.Size(81, 17)
        Me.cocc17.TabIndex = 14
        Me.cocc17.Tag = "19"
        Me.cocc17.Text = "Remarques"
        Me.cocc17.UseVisualStyleBackColor = False
        '
        'cocc15
        '
        Me.cocc15.BackColor = System.Drawing.Color.Transparent
        Me.cocc15.Checked = True
        Me.cocc15.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cocc15.Cursor = System.Windows.Forms.Cursors.Default
        Me.cocc15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cocc15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cocc15.Location = New System.Drawing.Point(136, 48)
        Me.cocc15.Name = "cocc15"
        Me.cocc15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cocc15.Size = New System.Drawing.Size(168, 17)
        Me.cocc15.TabIndex = 16
        Me.cocc15.Tag = "16"
        Me.cocc15.Text = "Numéro d'assurance maladie"
        Me.cocc15.UseVisualStyleBackColor = False
        '
        'ConfirmDateAccident
        '
        Me.ConfirmDateAccident.Checked = True
        Me.ConfirmDateAccident.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ConfirmDateAccident.Location = New System.Drawing.Point(11, 251)
        Me.ConfirmDateAccident.Name = "ConfirmDateAccident"
        Me.ConfirmDateAccident.Size = New System.Drawing.Size(216, 16)
        Me.ConfirmDateAccident.TabIndex = 125
        Me.ConfirmDateAccident.Tag = "74"
        Me.ConfirmDateAccident.Text = "Confirmation d'entrer la date d'accident"
        '
        'DemandeAuthorisationCommentsRequired
        '
        Me.DemandeAuthorisationCommentsRequired.Checked = True
        Me.DemandeAuthorisationCommentsRequired.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DemandeAuthorisationCommentsRequired.Location = New System.Drawing.Point(11, 293)
        Me.DemandeAuthorisationCommentsRequired.Name = "DemandeAuthorisationCommentsRequired"
        Me.DemandeAuthorisationCommentsRequired.Size = New System.Drawing.Size(538, 24)
        Me.DemandeAuthorisationCommentsRequired.TabIndex = 126
        Me.DemandeAuthorisationCommentsRequired.Tag = "110"
        Me.DemandeAuthorisationCommentsRequired.Text = "Le commentaire est obligatoire lors d'un changement de statut d'une demande d'aut" & _
            "orisation d'un dossier"
        '
        'ConfirmDateRechute
        '
        Me.ConfirmDateRechute.Location = New System.Drawing.Point(11, 273)
        Me.ConfirmDateRechute.Name = "ConfirmDateRechute"
        Me.ConfirmDateRechute.Size = New System.Drawing.Size(216, 16)
        Me.ConfirmDateRechute.TabIndex = 126
        Me.ConfirmDateRechute.Tag = "75"
        Me.ConfirmDateRechute.Text = "Confirmation d'entrer la date de rechute"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.Color.Transparent
        Me.label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label9.Location = New System.Drawing.Point(11, 168)
        Me.label9.Name = "label9"
        Me.label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label9.Size = New System.Drawing.Size(133, 14)
        Me.label9.TabIndex = 138
        Me.label9.Text = "Référents prédéterminés :"
        '
        'renamingReferent
        '
        Me.renamingReferent.BackColor = System.Drawing.SystemColors.Control
        Me.renamingReferent.Cursor = System.Windows.Forms.Cursors.Default
        Me.renamingReferent.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.renamingReferent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renamingReferent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.renamingReferent.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.renamingReferent.Location = New System.Drawing.Point(251, 208)
        Me.renamingReferent.Name = "renamingReferent"
        Me.renamingReferent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.renamingReferent.Size = New System.Drawing.Size(24, 24)
        Me.renamingReferent.TabIndex = 142
        Me.renamingReferent.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.renamingReferent, "Renommer le référent sélectionné")
        Me.renamingReferent.UseVisualStyleBackColor = False
        '
        'removingReferent
        '
        Me.removingReferent.BackColor = System.Drawing.SystemColors.Control
        Me.removingReferent.Cursor = System.Windows.Forms.Cursors.Default
        Me.removingReferent.Enabled = False
        Me.removingReferent.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.removingReferent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.removingReferent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.removingReferent.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.removingReferent.Location = New System.Drawing.Point(275, 208)
        Me.removingReferent.Name = "removingReferent"
        Me.removingReferent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.removingReferent.Size = New System.Drawing.Size(24, 24)
        Me.removingReferent.TabIndex = 141
        Me.removingReferent.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.removingReferent, "Enlever le référent sélectionné")
        Me.removingReferent.UseVisualStyleBackColor = False
        '
        'AddingReferent
        '
        Me.AddingReferent.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddingReferent.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddingReferent.Location = New System.Drawing.Point(263, 184)
        Me.AddingReferent.Name = "AddingReferent"
        Me.AddingReferent.Size = New System.Drawing.Size(24, 24)
        Me.AddingReferent.TabIndex = 143
        Me.ToolTip1.SetToolTip(Me.AddingReferent, "Ajout d'un référent")
        '
        'PageCliniqueFacturation
        '
        Me.PageCliniqueFacturation.AutoScroll = True
        Me.PageCliniqueFacturation.Controls.Add(Me.textToReplaceAbsNotMotivatedInReceipt)
        Me.PageCliniqueFacturation.Controls.Add(Me.changeAbsenceTypeForSpecificText)
        Me.PageCliniqueFacturation.Controls.Add(Me.nbDayCAR)
        Me.PageCliniqueFacturation.Controls.Add(Me.frameComptabilite)
        Me.PageCliniqueFacturation.Controls.Add(Me.FrameTriage)
        Me.PageCliniqueFacturation.Controls.Add(Me.label43)
        Me.PageCliniqueFacturation.Controls.Add(Me.nbVisiteCAR)
        Me.PageCliniqueFacturation.Controls.Add(Me.AutresTypesBills)
        Me.PageCliniqueFacturation.Controls.Add(Me.label42)
        Me.PageCliniqueFacturation.Controls.Add(Me.removingAutresTypes)
        Me.PageCliniqueFacturation.Controls.Add(Me.addingAutresTypes)
        Me.PageCliniqueFacturation.Controls.Add(Me.renamingAutresTypes)
        Me.PageCliniqueFacturation.Controls.Add(Me.MontantFactureHistoIsCumulative)
        Me.PageCliniqueFacturation.Controls.Add(Me.label32)
        Me.PageCliniqueFacturation.Controls.Add(Me.printRecuForClientAuto)
        Me.PageCliniqueFacturation.Controls.Add(Me.AdjustingCommentsForced)
        Me.PageCliniqueFacturation.Controls.Add(Me.label38)
        Me.PageCliniqueFacturation.Controls.Add(Me.prefixNoRecu)
        Me.PageCliniqueFacturation.Location = New System.Drawing.Point(4, 26)
        Me.PageCliniqueFacturation.Name = "PageCliniqueFacturation"
        Me.PageCliniqueFacturation.Size = New System.Drawing.Size(681, 320)
        Me.PageCliniqueFacturation.TabIndex = 4
        Me.PageCliniqueFacturation.Text = "Facturation"
        Me.PageCliniqueFacturation.UseVisualStyleBackColor = True
        '
        'nbDayCAR
        '
        Me.nbDayCAR.acceptAlpha = False
        Me.nbDayCAR.acceptedChars = ",§.§-"
        Me.nbDayCAR.acceptNumeric = True
        Me.nbDayCAR.AcceptsReturn = True
        Me.nbDayCAR.allCapital = False
        Me.nbDayCAR.allLower = False
        Me.nbDayCAR.BackColor = System.Drawing.SystemColors.Window
        Me.nbDayCAR.blockOnMaximum = False
        Me.nbDayCAR.blockOnMinimum = False
        Me.nbDayCAR.cb_AcceptLeftZeros = False
        Me.nbDayCAR.cb_AcceptNegative = True
        Me.nbDayCAR.currencyBox = True
        Me.nbDayCAR.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nbDayCAR.firstLetterCapital = False
        Me.nbDayCAR.firstLettersCapital = False
        Me.nbDayCAR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbDayCAR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nbDayCAR.Location = New System.Drawing.Point(591, 272)
        Me.nbDayCAR.manageText = True
        Me.nbDayCAR.matchExp = ""
        Me.nbDayCAR.maximum = 0
        Me.nbDayCAR.MaxLength = 0
        Me.nbDayCAR.minimum = 0
        Me.nbDayCAR.Name = "nbDayCAR"
        Me.nbDayCAR.nbDecimals = CType(0, Short)
        Me.nbDayCAR.onlyAlphabet = False
        Me.nbDayCAR.refuseAccents = False
        Me.nbDayCAR.refusedChars = ""
        Me.nbDayCAR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbDayCAR.showInternalContextMenu = True
        Me.nbDayCAR.Size = New System.Drawing.Size(42, 20)
        Me.nbDayCAR.TabIndex = 133
        Me.nbDayCAR.Tag = "109"
        Me.nbDayCAR.Text = "-1"
        Me.nbDayCAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.nbDayCAR, "-1 signifie que le logiciel ne tient pas compte de cette préférence")
        Me.nbDayCAR.trimText = False
        '
        'frameComptabilite
        '
        Me.frameComptabilite.BackColor = System.Drawing.SystemColors.Control
        Me.frameComptabilite.Controls.Add(Me.GroupBox13)
        Me.frameComptabilite.Controls.Add(Me.GroupBox12)
        Me.frameComptabilite.Controls.Add(Me.MethodesPaiment)
        Me.frameComptabilite.Controls.Add(Me.removingMP)
        Me.frameComptabilite.Controls.Add(Me.addingMP)
        Me.frameComptabilite.Controls.Add(Me.taxesInteraction)
        Me.frameComptabilite.Controls.Add(Me.taxeArrondissement)
        Me.frameComptabilite.Controls.Add(Me.Label75)
        Me.frameComptabilite.Controls.Add(Me.label6)
        Me.frameComptabilite.Controls.Add(Me.label5)
        Me.frameComptabilite.Controls.Add(Me.renamingMP)
        Me.frameComptabilite.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameComptabilite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameComptabilite.Location = New System.Drawing.Point(6, 3)
        Me.frameComptabilite.Name = "frameComptabilite"
        Me.frameComptabilite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameComptabilite.Size = New System.Drawing.Size(328, 289)
        Me.frameComptabilite.TabIndex = 111
        Me.frameComptabilite.TabStop = False
        Me.frameComptabilite.Text = "Comptabilité"
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.taxe2)
        Me.GroupBox13.Controls.Add(Me.Label66)
        Me.GroupBox13.Controls.Add(Me.Label70)
        Me.GroupBox13.Controls.Add(Me.Label77)
        Me.GroupBox13.Controls.Add(Me.tax2Name)
        Me.GroupBox13.Location = New System.Drawing.Point(4, 52)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(320, 38)
        Me.GroupBox13.TabIndex = 139
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Taxe 2 (Fédérale)"
        '
        'taxe2
        '
        Me.taxe2.acceptAlpha = False
        Me.taxe2.acceptedChars = ",§."
        Me.taxe2.acceptNumeric = True
        Me.taxe2.AcceptsReturn = True
        Me.taxe2.allCapital = False
        Me.taxe2.allLower = False
        Me.taxe2.BackColor = System.Drawing.SystemColors.Window
        Me.taxe2.blockOnMaximum = False
        Me.taxe2.blockOnMinimum = False
        Me.taxe2.cb_AcceptLeftZeros = False
        Me.taxe2.cb_AcceptNegative = False
        Me.taxe2.currencyBox = True
        Me.taxe2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.taxe2.firstLetterCapital = False
        Me.taxe2.firstLettersCapital = False
        Me.taxe2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.taxe2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.taxe2.Location = New System.Drawing.Point(263, 13)
        Me.taxe2.manageText = True
        Me.taxe2.matchExp = ""
        Me.taxe2.maximum = 0
        Me.taxe2.MaxLength = 0
        Me.taxe2.minimum = 0
        Me.taxe2.Name = "taxe2"
        Me.taxe2.nbDecimals = CType(-1, Short)
        Me.taxe2.onlyAlphabet = False
        Me.taxe2.refuseAccents = False
        Me.taxe2.refusedChars = ""
        Me.taxe2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.taxe2.showInternalContextMenu = True
        Me.taxe2.Size = New System.Drawing.Size(39, 20)
        Me.taxe2.TabIndex = 114
        Me.taxe2.Tag = "62"
        Me.taxe2.Text = "5"
        Me.taxe2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.taxe2.trimText = False
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.Color.Transparent
        Me.Label66.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label66.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label66.Location = New System.Drawing.Point(183, 16)
        Me.Label66.Name = "Label66"
        Me.Label66.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label66.Size = New System.Drawing.Size(74, 14)
        Me.Label66.TabIndex = 112
        Me.Label66.Text = "Pourcentage :"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.BackColor = System.Drawing.Color.Transparent
        Me.Label70.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label70.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label70.Location = New System.Drawing.Point(6, 16)
        Me.Label70.Name = "Label70"
        Me.Label70.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label70.Size = New System.Drawing.Size(34, 14)
        Me.Label70.TabIndex = 112
        Me.Label70.Text = "Nom :"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.BackColor = System.Drawing.SystemColors.Control
        Me.Label77.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label77.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label77.Location = New System.Drawing.Point(301, 16)
        Me.Label77.Name = "Label77"
        Me.Label77.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label77.Size = New System.Drawing.Size(17, 14)
        Me.Label77.TabIndex = 113
        Me.Label77.Text = "%"
        '
        'tax2Name
        '
        Me.tax2Name.acceptAlpha = True
        Me.tax2Name.acceptedChars = ""
        Me.tax2Name.acceptNumeric = True
        Me.tax2Name.AcceptsReturn = True
        Me.tax2Name.allCapital = False
        Me.tax2Name.allLower = False
        Me.tax2Name.BackColor = System.Drawing.SystemColors.Window
        Me.tax2Name.blockOnMaximum = False
        Me.tax2Name.blockOnMinimum = False
        Me.tax2Name.cb_AcceptLeftZeros = False
        Me.tax2Name.cb_AcceptNegative = False
        Me.tax2Name.currencyBox = False
        Me.tax2Name.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tax2Name.firstLetterCapital = False
        Me.tax2Name.firstLettersCapital = False
        Me.tax2Name.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tax2Name.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tax2Name.Location = New System.Drawing.Point(46, 13)
        Me.tax2Name.manageText = True
        Me.tax2Name.matchExp = ""
        Me.tax2Name.maximum = 0
        Me.tax2Name.MaxLength = 0
        Me.tax2Name.minimum = 0
        Me.tax2Name.Name = "tax2Name"
        Me.tax2Name.nbDecimals = CType(0, Short)
        Me.tax2Name.onlyAlphabet = False
        Me.tax2Name.refuseAccents = False
        Me.tax2Name.refusedChars = ""
        Me.tax2Name.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tax2Name.showInternalContextMenu = True
        Me.tax2Name.Size = New System.Drawing.Size(131, 20)
        Me.tax2Name.TabIndex = 133
        Me.tax2Name.Tag = "138"
        Me.tax2Name.Text = "TPS"
        Me.tax2Name.trimText = False
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.taxe1)
        Me.GroupBox12.Controls.Add(Me.Label30)
        Me.GroupBox12.Controls.Add(Me.label1)
        Me.GroupBox12.Controls.Add(Me.label2)
        Me.GroupBox12.Controls.Add(Me.tax1Name)
        Me.GroupBox12.Location = New System.Drawing.Point(4, 13)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(320, 38)
        Me.GroupBox12.TabIndex = 139
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Taxe 1 (Provinciale)"
        '
        'taxe1
        '
        Me.taxe1.acceptAlpha = False
        Me.taxe1.acceptedChars = ",§."
        Me.taxe1.acceptNumeric = True
        Me.taxe1.AcceptsReturn = True
        Me.taxe1.allCapital = False
        Me.taxe1.allLower = False
        Me.taxe1.BackColor = System.Drawing.SystemColors.Window
        Me.taxe1.blockOnMaximum = False
        Me.taxe1.blockOnMinimum = False
        Me.taxe1.cb_AcceptLeftZeros = False
        Me.taxe1.cb_AcceptNegative = False
        Me.taxe1.currencyBox = True
        Me.taxe1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.taxe1.firstLetterCapital = False
        Me.taxe1.firstLettersCapital = False
        Me.taxe1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.taxe1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.taxe1.Location = New System.Drawing.Point(263, 13)
        Me.taxe1.manageText = True
        Me.taxe1.matchExp = ""
        Me.taxe1.maximum = 0
        Me.taxe1.MaxLength = 0
        Me.taxe1.minimum = 0
        Me.taxe1.Name = "taxe1"
        Me.taxe1.nbDecimals = CType(-1, Short)
        Me.taxe1.onlyAlphabet = False
        Me.taxe1.refuseAccents = False
        Me.taxe1.refusedChars = ""
        Me.taxe1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.taxe1.showInternalContextMenu = True
        Me.taxe1.Size = New System.Drawing.Size(39, 20)
        Me.taxe1.TabIndex = 111
        Me.taxe1.Tag = "61"
        Me.taxe1.Text = "9,5"
        Me.taxe1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.taxe1.trimText = False
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(183, 16)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(74, 14)
        Me.Label30.TabIndex = 112
        Me.Label30.Text = "Pourcentage :"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(6, 16)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(34, 14)
        Me.label1.TabIndex = 112
        Me.label1.Text = "Nom :"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(301, 16)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(17, 14)
        Me.label2.TabIndex = 113
        Me.label2.Text = "%"
        '
        'tax1Name
        '
        Me.tax1Name.acceptAlpha = True
        Me.tax1Name.acceptedChars = ""
        Me.tax1Name.acceptNumeric = True
        Me.tax1Name.AcceptsReturn = True
        Me.tax1Name.allCapital = False
        Me.tax1Name.allLower = False
        Me.tax1Name.BackColor = System.Drawing.SystemColors.Window
        Me.tax1Name.blockOnMaximum = False
        Me.tax1Name.blockOnMinimum = False
        Me.tax1Name.cb_AcceptLeftZeros = False
        Me.tax1Name.cb_AcceptNegative = False
        Me.tax1Name.currencyBox = False
        Me.tax1Name.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tax1Name.firstLetterCapital = False
        Me.tax1Name.firstLettersCapital = False
        Me.tax1Name.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tax1Name.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tax1Name.Location = New System.Drawing.Point(46, 13)
        Me.tax1Name.manageText = True
        Me.tax1Name.matchExp = ""
        Me.tax1Name.maximum = 0
        Me.tax1Name.MaxLength = 0
        Me.tax1Name.minimum = 0
        Me.tax1Name.Name = "tax1Name"
        Me.tax1Name.nbDecimals = CType(0, Short)
        Me.tax1Name.onlyAlphabet = False
        Me.tax1Name.refuseAccents = False
        Me.tax1Name.refusedChars = ""
        Me.tax1Name.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tax1Name.showInternalContextMenu = True
        Me.tax1Name.Size = New System.Drawing.Size(131, 20)
        Me.tax1Name.TabIndex = 133
        Me.tax1Name.Tag = "137"
        Me.tax1Name.Text = "TVQ"
        Me.tax1Name.trimText = False
        '
        'MethodesPaiment
        '
        Me.MethodesPaiment.HorizontalScrollbar = True
        Me.MethodesPaiment.ItemHeight = 14
        Me.MethodesPaiment.Location = New System.Drawing.Point(8, 195)
        Me.MethodesPaiment.Name = "MethodesPaiment"
        Me.MethodesPaiment.Size = New System.Drawing.Size(284, 88)
        Me.MethodesPaiment.Sorted = True
        Me.MethodesPaiment.TabIndex = 138
        Me.MethodesPaiment.Tag = "22"
        '
        'removingMP
        '
        Me.removingMP.BackColor = System.Drawing.SystemColors.Control
        Me.removingMP.Cursor = System.Windows.Forms.Cursors.Default
        Me.removingMP.Enabled = False
        Me.removingMP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.removingMP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.removingMP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.removingMP.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.removingMP.Location = New System.Drawing.Point(298, 259)
        Me.removingMP.Name = "removingMP"
        Me.removingMP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.removingMP.Size = New System.Drawing.Size(24, 24)
        Me.removingMP.TabIndex = 137
        Me.removingMP.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.removingMP, "Enlever la méthode sélectionnée")
        Me.removingMP.UseVisualStyleBackColor = False
        '
        'addingMP
        '
        Me.addingMP.BackColor = System.Drawing.SystemColors.Control
        Me.addingMP.Cursor = System.Windows.Forms.Cursors.Default
        Me.addingMP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addingMP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addingMP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addingMP.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.addingMP.Location = New System.Drawing.Point(298, 195)
        Me.addingMP.Name = "addingMP"
        Me.addingMP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.addingMP.Size = New System.Drawing.Size(24, 24)
        Me.addingMP.TabIndex = 136
        Me.addingMP.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.addingMP, "Ajout d'une méthode de paiement")
        Me.addingMP.UseVisualStyleBackColor = False
        '
        'taxesInteraction
        '
        Me.taxesInteraction.BackColor = System.Drawing.SystemColors.Window
        Me.taxesInteraction.Cursor = System.Windows.Forms.Cursors.Default
        Me.taxesInteraction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.taxesInteraction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.taxesInteraction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.taxesInteraction.Items.AddRange(New Object() {"Appliquer la Taxe 1 sur le montant avec la Taxe 2", "Appliquer la Taxe 1 sur le montant brut"})
        Me.taxesInteraction.Location = New System.Drawing.Point(8, 153)
        Me.taxesInteraction.Name = "taxesInteraction"
        Me.taxesInteraction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.taxesInteraction.Size = New System.Drawing.Size(314, 22)
        Me.taxesInteraction.TabIndex = 120
        Me.taxesInteraction.Tag = "65"
        '
        'taxeArrondissement
        '
        Me.taxeArrondissement.BackColor = System.Drawing.SystemColors.Window
        Me.taxeArrondissement.Cursor = System.Windows.Forms.Cursors.Default
        Me.taxeArrondissement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.taxeArrondissement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.taxeArrondissement.ForeColor = System.Drawing.SystemColors.WindowText
        Me.taxeArrondissement.Items.AddRange(New Object() {"Arrondir à la cent supérieure", "Toujours à la cent supérieure", "Toujours à la cent inférieure"})
        Me.taxeArrondissement.Location = New System.Drawing.Point(8, 109)
        Me.taxeArrondissement.Name = "taxeArrondissement"
        Me.taxeArrondissement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.taxeArrondissement.Size = New System.Drawing.Size(314, 22)
        Me.taxeArrondissement.TabIndex = 120
        Me.taxeArrondissement.Tag = "65"
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.BackColor = System.Drawing.Color.Transparent
        Me.Label75.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label75.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label75.Location = New System.Drawing.Point(6, 136)
        Me.Label75.Name = "Label75"
        Me.Label75.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label75.Size = New System.Drawing.Size(138, 14)
        Me.Label75.TabIndex = 119
        Me.Label75.Text = "Interaction entre les taxes :"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.Color.Transparent
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(8, 93)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(187, 14)
        Me.label6.TabIndex = 119
        Me.label6.Text = "Arrondissement du calcul des taxes :"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(8, 179)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(129, 14)
        Me.label5.TabIndex = 118
        Me.label5.Text = "Méthode(s) de paiement :"
        '
        'renamingMP
        '
        Me.renamingMP.BackColor = System.Drawing.SystemColors.Control
        Me.renamingMP.Cursor = System.Windows.Forms.Cursors.Default
        Me.renamingMP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.renamingMP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renamingMP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.renamingMP.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.renamingMP.Location = New System.Drawing.Point(298, 227)
        Me.renamingMP.Name = "renamingMP"
        Me.renamingMP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.renamingMP.Size = New System.Drawing.Size(24, 24)
        Me.renamingMP.TabIndex = 137
        Me.renamingMP.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.renamingMP, "Renommer la méthode sélectionnée")
        Me.renamingMP.UseVisualStyleBackColor = False
        '
        'FrameTriage
        '
        Me.FrameTriage.Controls.Add(Me.triFactures)
        Me.FrameTriage.Controls.Add(Me.triPaiements)
        Me.FrameTriage.Controls.Add(Me.label15)
        Me.FrameTriage.Controls.Add(Me.label14)
        Me.FrameTriage.Location = New System.Drawing.Point(340, 3)
        Me.FrameTriage.Name = "FrameTriage"
        Me.FrameTriage.Size = New System.Drawing.Size(316, 106)
        Me.FrameTriage.TabIndex = 132
        Me.FrameTriage.TabStop = False
        Me.FrameTriage.Text = "Ordre de triage"
        '
        'triFactures
        '
        Me.triFactures.BackColor = System.Drawing.SystemColors.Window
        Me.triFactures.Cursor = System.Windows.Forms.Cursors.Default
        Me.triFactures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.triFactures.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.triFactures.ForeColor = System.Drawing.SystemColors.WindowText
        Me.triFactures.Items.AddRange(New Object() {"Ascendant", "Descendant"})
        Me.triFactures.Location = New System.Drawing.Point(8, 72)
        Me.triFactures.Name = "triFactures"
        Me.triFactures.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.triFactures.Size = New System.Drawing.Size(302, 22)
        Me.triFactures.TabIndex = 133
        Me.triFactures.Tag = "80"
        '
        'triPaiements
        '
        Me.triPaiements.BackColor = System.Drawing.SystemColors.Window
        Me.triPaiements.Cursor = System.Windows.Forms.Cursors.Default
        Me.triPaiements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.triPaiements.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.triPaiements.ForeColor = System.Drawing.SystemColors.WindowText
        Me.triPaiements.Items.AddRange(New Object() {"Ascendant", "Descendant"})
        Me.triPaiements.Location = New System.Drawing.Point(8, 32)
        Me.triPaiements.Name = "triPaiements"
        Me.triPaiements.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.triPaiements.Size = New System.Drawing.Size(302, 22)
        Me.triPaiements.TabIndex = 131
        Me.triPaiements.Tag = "79"
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.BackColor = System.Drawing.SystemColors.Control
        Me.label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.label15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label15.Location = New System.Drawing.Point(8, 56)
        Me.label15.Name = "label15"
        Me.label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label15.Size = New System.Drawing.Size(132, 14)
        Me.label15.TabIndex = 134
        Me.label15.Text = "Factures dans le compte :"
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.BackColor = System.Drawing.SystemColors.Control
        Me.label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.label14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label14.Location = New System.Drawing.Point(8, 16)
        Me.label14.Name = "label14"
        Me.label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label14.Size = New System.Drawing.Size(62, 14)
        Me.label14.TabIndex = 132
        Me.label14.Text = "Paiements :"
        '
        'label43
        '
        Me.label43.AutoSize = True
        Me.label43.BackColor = System.Drawing.SystemColors.Control
        Me.label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.label43.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label43.Location = New System.Drawing.Point(348, 274)
        Me.label43.Name = "label43"
        Me.label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label43.Size = New System.Drawing.Size(240, 14)
        Me.label43.TabIndex = 134
        Me.label43.Text = "Nombre maximal de jour d'un compte à recevoir :"
        '
        'nbVisiteCAR
        '
        Me.nbVisiteCAR.acceptAlpha = False
        Me.nbVisiteCAR.acceptedChars = ",§.§-"
        Me.nbVisiteCAR.acceptNumeric = True
        Me.nbVisiteCAR.AcceptsReturn = True
        Me.nbVisiteCAR.allCapital = False
        Me.nbVisiteCAR.allLower = False
        Me.nbVisiteCAR.BackColor = System.Drawing.SystemColors.Window
        Me.nbVisiteCAR.blockOnMaximum = False
        Me.nbVisiteCAR.blockOnMinimum = False
        Me.nbVisiteCAR.cb_AcceptLeftZeros = False
        Me.nbVisiteCAR.cb_AcceptNegative = True
        Me.nbVisiteCAR.currencyBox = True
        Me.nbVisiteCAR.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nbVisiteCAR.firstLetterCapital = False
        Me.nbVisiteCAR.firstLettersCapital = False
        Me.nbVisiteCAR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbVisiteCAR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nbVisiteCAR.Location = New System.Drawing.Point(541, 240)
        Me.nbVisiteCAR.manageText = True
        Me.nbVisiteCAR.matchExp = ""
        Me.nbVisiteCAR.maximum = 0
        Me.nbVisiteCAR.MaxLength = 0
        Me.nbVisiteCAR.minimum = 0
        Me.nbVisiteCAR.Name = "nbVisiteCAR"
        Me.nbVisiteCAR.nbDecimals = CType(0, Short)
        Me.nbVisiteCAR.onlyAlphabet = False
        Me.nbVisiteCAR.refuseAccents = False
        Me.nbVisiteCAR.refusedChars = ""
        Me.nbVisiteCAR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbVisiteCAR.showInternalContextMenu = True
        Me.nbVisiteCAR.Size = New System.Drawing.Size(42, 20)
        Me.nbVisiteCAR.TabIndex = 133
        Me.nbVisiteCAR.Tag = "108"
        Me.nbVisiteCAR.Text = "-1"
        Me.nbVisiteCAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.nbVisiteCAR, "-1 signifie que le logiciel ne tient pas compte de cette préférence")
        Me.nbVisiteCAR.trimText = False
        '
        'AutresTypesBills
        '
        Me.AutresTypesBills.HorizontalScrollbar = True
        Me.AutresTypesBills.ItemHeight = 14
        Me.AutresTypesBills.Location = New System.Drawing.Point(345, 139)
        Me.AutresTypesBills.Name = "AutresTypesBills"
        Me.AutresTypesBills.Size = New System.Drawing.Size(281, 88)
        Me.AutresTypesBills.Sorted = True
        Me.AutresTypesBills.TabIndex = 151
        Me.AutresTypesBills.Tag = "95"
        '
        'label42
        '
        Me.label42.BackColor = System.Drawing.SystemColors.Control
        Me.label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.label42.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label42.Location = New System.Drawing.Point(348, 237)
        Me.label42.Name = "label42"
        Me.label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label42.Size = New System.Drawing.Size(194, 31)
        Me.label42.TabIndex = 134
        Me.label42.Text = "Nombre maximal de rendez-vous pouvant avoir un compte à reçevoir :"
        '
        'removingAutresTypes
        '
        Me.removingAutresTypes.BackColor = System.Drawing.SystemColors.Control
        Me.removingAutresTypes.Cursor = System.Windows.Forms.Cursors.Default
        Me.removingAutresTypes.Enabled = False
        Me.removingAutresTypes.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.removingAutresTypes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.removingAutresTypes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.removingAutresTypes.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.removingAutresTypes.Location = New System.Drawing.Point(632, 203)
        Me.removingAutresTypes.Name = "removingAutresTypes"
        Me.removingAutresTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.removingAutresTypes.Size = New System.Drawing.Size(24, 24)
        Me.removingAutresTypes.TabIndex = 153
        Me.removingAutresTypes.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.removingAutresTypes, "Enlever le service sélectionné")
        Me.removingAutresTypes.UseVisualStyleBackColor = False
        '
        'addingAutresTypes
        '
        Me.addingAutresTypes.BackColor = System.Drawing.SystemColors.Control
        Me.addingAutresTypes.Cursor = System.Windows.Forms.Cursors.Default
        Me.addingAutresTypes.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addingAutresTypes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addingAutresTypes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addingAutresTypes.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.addingAutresTypes.Location = New System.Drawing.Point(632, 139)
        Me.addingAutresTypes.Name = "addingAutresTypes"
        Me.addingAutresTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.addingAutresTypes.Size = New System.Drawing.Size(24, 24)
        Me.addingAutresTypes.TabIndex = 152
        Me.addingAutresTypes.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.addingAutresTypes, "Ajout d'un service")
        Me.addingAutresTypes.UseVisualStyleBackColor = False
        '
        'renamingAutresTypes
        '
        Me.renamingAutresTypes.BackColor = System.Drawing.SystemColors.Control
        Me.renamingAutresTypes.Cursor = System.Windows.Forms.Cursors.Default
        Me.renamingAutresTypes.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.renamingAutresTypes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renamingAutresTypes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.renamingAutresTypes.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.renamingAutresTypes.Location = New System.Drawing.Point(632, 171)
        Me.renamingAutresTypes.Name = "renamingAutresTypes"
        Me.renamingAutresTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.renamingAutresTypes.Size = New System.Drawing.Size(24, 24)
        Me.renamingAutresTypes.TabIndex = 154
        Me.renamingAutresTypes.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.renamingAutresTypes, "Renommer le service sélectionné")
        Me.renamingAutresTypes.UseVisualStyleBackColor = False
        '
        'MontantFactureHistoIsCumulative
        '
        Me.MontantFactureHistoIsCumulative.Location = New System.Drawing.Point(6, 318)
        Me.MontantFactureHistoIsCumulative.Name = "MontantFactureHistoIsCumulative"
        Me.MontantFactureHistoIsCumulative.Size = New System.Drawing.Size(403, 20)
        Me.MontantFactureHistoIsCumulative.TabIndex = 127
        Me.MontantFactureHistoIsCumulative.Tag = "107"
        Me.MontantFactureHistoIsCumulative.Text = "L'historique du montant de la facture affiche le cumultatif des changements"
        '
        'label32
        '
        Me.label32.AutoSize = True
        Me.label32.BackColor = System.Drawing.Color.Transparent
        Me.label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.label32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label32.Location = New System.Drawing.Point(345, 123)
        Me.label32.Name = "label32"
        Me.label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label32.Size = New System.Drawing.Size(146, 14)
        Me.label32.TabIndex = 150
        Me.label32.Text = "Autres types de facturation :"
        '
        'printRecuForClientAuto
        '
        Me.printRecuForClientAuto.AutoSize = True
        Me.printRecuForClientAuto.Checked = True
        Me.printRecuForClientAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.printRecuForClientAuto.Location = New System.Drawing.Point(6, 356)
        Me.printRecuForClientAuto.Name = "printRecuForClientAuto"
        Me.printRecuForClientAuto.Size = New System.Drawing.Size(550, 18)
        Me.printRecuForClientAuto.TabIndex = 127
        Me.printRecuForClientAuto.Tag = "115"
        Me.printRecuForClientAuto.Text = "Imprimer un reçu uniquement pour le client à partir de l'agenda et de la liste de" & _
            "s rendez-vous du compte client"
        '
        'AdjustingCommentsForced
        '
        Me.AdjustingCommentsForced.Checked = True
        Me.AdjustingCommentsForced.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AdjustingCommentsForced.Location = New System.Drawing.Point(6, 337)
        Me.AdjustingCommentsForced.Name = "AdjustingCommentsForced"
        Me.AdjustingCommentsForced.Size = New System.Drawing.Size(456, 20)
        Me.AdjustingCommentsForced.TabIndex = 127
        Me.AdjustingCommentsForced.Tag = "106"
        Me.AdjustingCommentsForced.Text = "Le champs 'Commentaires' lors de l'ajustement d'une facture doit absolument être " & _
            "rempli"
        '
        'label38
        '
        Me.label38.AutoSize = True
        Me.label38.BackColor = System.Drawing.SystemColors.Control
        Me.label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.label38.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label38.Location = New System.Drawing.Point(3, 301)
        Me.label38.Name = "label38"
        Me.label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label38.Size = New System.Drawing.Size(274, 14)
        Me.label38.TabIndex = 134
        Me.label38.Text = "Texte situé à gauche de numéro de reçu d'une facture :"
        '
        'prefixNoRecu
        '
        Me.prefixNoRecu.acceptAlpha = True
        Me.prefixNoRecu.acceptedChars = ""
        Me.prefixNoRecu.acceptNumeric = True
        Me.prefixNoRecu.AcceptsReturn = True
        Me.prefixNoRecu.allCapital = False
        Me.prefixNoRecu.allLower = False
        Me.prefixNoRecu.BackColor = System.Drawing.SystemColors.Window
        Me.prefixNoRecu.blockOnMaximum = False
        Me.prefixNoRecu.blockOnMinimum = False
        Me.prefixNoRecu.cb_AcceptLeftZeros = False
        Me.prefixNoRecu.cb_AcceptNegative = False
        Me.prefixNoRecu.currencyBox = False
        Me.prefixNoRecu.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.prefixNoRecu.firstLetterCapital = False
        Me.prefixNoRecu.firstLettersCapital = False
        Me.prefixNoRecu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prefixNoRecu.ForeColor = System.Drawing.SystemColors.WindowText
        Me.prefixNoRecu.Location = New System.Drawing.Point(280, 298)
        Me.prefixNoRecu.manageText = True
        Me.prefixNoRecu.matchExp = ""
        Me.prefixNoRecu.maximum = 0
        Me.prefixNoRecu.MaxLength = 0
        Me.prefixNoRecu.minimum = 0
        Me.prefixNoRecu.Name = "prefixNoRecu"
        Me.prefixNoRecu.nbDecimals = CType(0, Short)
        Me.prefixNoRecu.onlyAlphabet = False
        Me.prefixNoRecu.refuseAccents = False
        Me.prefixNoRecu.refusedChars = ""
        Me.prefixNoRecu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.prefixNoRecu.showInternalContextMenu = True
        Me.prefixNoRecu.Size = New System.Drawing.Size(54, 20)
        Me.prefixNoRecu.TabIndex = 133
        Me.prefixNoRecu.Tag = "101"
        Me.prefixNoRecu.trimText = False
        '
        'PageCliniquePrinting
        '
        Me.PageCliniquePrinting.Controls.Add(Me.GroupBox3)
        Me.PageCliniquePrinting.Controls.Add(Me.Label63)
        Me.PageCliniquePrinting.Controls.Add(Me.Label45)
        Me.PageCliniquePrinting.Controls.Add(Me.printingFooter)
        Me.PageCliniquePrinting.Controls.Add(Me.printingHeader)
        Me.PageCliniquePrinting.Location = New System.Drawing.Point(4, 25)
        Me.PageCliniquePrinting.Name = "PageCliniquePrinting"
        Me.PageCliniquePrinting.Size = New System.Drawing.Size(681, 321)
        Me.PageCliniquePrinting.TabIndex = 11
        Me.PageCliniquePrinting.Text = "Impression"
        Me.PageCliniquePrinting.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label65)
        Me.GroupBox3.Controls.Add(Me.publipostageTopMargin)
        Me.GroupBox3.Controls.Add(Me.Label57)
        Me.GroupBox3.Controls.Add(Me.Label64)
        Me.GroupBox3.Controls.Add(Me.publipostageSpacing)
        Me.GroupBox3.Controls.Add(Me.Label58)
        Me.GroupBox3.Location = New System.Drawing.Point(361, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(272, 79)
        Me.GroupBox3.TabIndex = 126
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Publipostage"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(6, 55)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(135, 14)
        Me.Label65.TabIndex = 0
        Me.Label65.Text = "Marge en haut de la page :"
        '
        'publipostageTopMargin
        '
        Me.publipostageTopMargin.acceptAlpha = False
        Me.publipostageTopMargin.acceptedChars = ",§."
        Me.publipostageTopMargin.acceptNumeric = True
        Me.publipostageTopMargin.AcceptsReturn = True
        Me.publipostageTopMargin.allCapital = False
        Me.publipostageTopMargin.allLower = False
        Me.publipostageTopMargin.BackColor = System.Drawing.SystemColors.Window
        Me.publipostageTopMargin.blockOnMaximum = True
        Me.publipostageTopMargin.blockOnMinimum = False
        Me.publipostageTopMargin.cb_AcceptLeftZeros = False
        Me.publipostageTopMargin.cb_AcceptNegative = False
        Me.publipostageTopMargin.currencyBox = True
        Me.publipostageTopMargin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.publipostageTopMargin.firstLetterCapital = False
        Me.publipostageTopMargin.firstLettersCapital = False
        Me.publipostageTopMargin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.publipostageTopMargin.ForeColor = System.Drawing.SystemColors.WindowText
        Me.publipostageTopMargin.Location = New System.Drawing.Point(182, 52)
        Me.publipostageTopMargin.manageText = True
        Me.publipostageTopMargin.matchExp = ""
        Me.publipostageTopMargin.maximum = 0.82
        Me.publipostageTopMargin.MaxLength = 0
        Me.publipostageTopMargin.minimum = 0
        Me.publipostageTopMargin.Name = "publipostageTopMargin"
        Me.publipostageTopMargin.nbDecimals = CType(2, Short)
        Me.publipostageTopMargin.onlyAlphabet = False
        Me.publipostageTopMargin.refuseAccents = False
        Me.publipostageTopMargin.refusedChars = ""
        Me.publipostageTopMargin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.publipostageTopMargin.showInternalContextMenu = True
        Me.publipostageTopMargin.Size = New System.Drawing.Size(32, 20)
        Me.publipostageTopMargin.TabIndex = 125
        Me.publipostageTopMargin.Tag = "135"
        Me.publipostageTopMargin.Text = "0,1"
        Me.publipostageTopMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.publipostageTopMargin.trimText = False
        '
        'Label57
        '
        Me.Label57.Location = New System.Drawing.Point(6, 16)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(170, 29)
        Me.Label57.TabIndex = 0
        Me.Label57.Text = "Espacement entre l'adresse du client et l'adresse de la clinique :"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(216, 55)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(43, 14)
        Me.Label64.TabIndex = 0
        Me.Label64.Text = "pouces"
        '
        'publipostageSpacing
        '
        Me.publipostageSpacing.acceptAlpha = False
        Me.publipostageSpacing.acceptedChars = ",§."
        Me.publipostageSpacing.acceptNumeric = True
        Me.publipostageSpacing.AcceptsReturn = True
        Me.publipostageSpacing.allCapital = False
        Me.publipostageSpacing.allLower = False
        Me.publipostageSpacing.BackColor = System.Drawing.SystemColors.Window
        Me.publipostageSpacing.blockOnMaximum = False
        Me.publipostageSpacing.blockOnMinimum = False
        Me.publipostageSpacing.cb_AcceptLeftZeros = False
        Me.publipostageSpacing.cb_AcceptNegative = False
        Me.publipostageSpacing.currencyBox = True
        Me.publipostageSpacing.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.publipostageSpacing.firstLetterCapital = False
        Me.publipostageSpacing.firstLettersCapital = False
        Me.publipostageSpacing.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.publipostageSpacing.ForeColor = System.Drawing.SystemColors.WindowText
        Me.publipostageSpacing.Location = New System.Drawing.Point(182, 19)
        Me.publipostageSpacing.manageText = True
        Me.publipostageSpacing.matchExp = ""
        Me.publipostageSpacing.maximum = 5
        Me.publipostageSpacing.MaxLength = 0
        Me.publipostageSpacing.minimum = 0
        Me.publipostageSpacing.Name = "publipostageSpacing"
        Me.publipostageSpacing.nbDecimals = CType(2, Short)
        Me.publipostageSpacing.onlyAlphabet = False
        Me.publipostageSpacing.refuseAccents = False
        Me.publipostageSpacing.refusedChars = ""
        Me.publipostageSpacing.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.publipostageSpacing.showInternalContextMenu = True
        Me.publipostageSpacing.Size = New System.Drawing.Size(32, 20)
        Me.publipostageSpacing.TabIndex = 125
        Me.publipostageSpacing.Tag = "125"
        Me.publipostageSpacing.Text = "0,8"
        Me.publipostageSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.publipostageSpacing.trimText = False
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(216, 22)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(43, 14)
        Me.Label58.TabIndex = 0
        Me.Label58.Text = "pouces"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(6, 53)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(75, 14)
        Me.Label63.TabIndex = 0
        Me.Label63.Text = "Pied de page :"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(6, 19)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(85, 14)
        Me.Label45.TabIndex = 0
        Me.Label45.Text = "Entête de page :"
        '
        'printingFooter
        '
        Me.printingFooter.acceptAlpha = True
        Me.printingFooter.acceptedChars = ""
        Me.printingFooter.acceptNumeric = True
        Me.printingFooter.AcceptsReturn = True
        Me.printingFooter.allCapital = False
        Me.printingFooter.allLower = False
        Me.printingFooter.BackColor = System.Drawing.SystemColors.Window
        Me.printingFooter.blockOnMaximum = False
        Me.printingFooter.blockOnMinimum = False
        Me.printingFooter.cb_AcceptLeftZeros = False
        Me.printingFooter.cb_AcceptNegative = False
        Me.printingFooter.currencyBox = False
        Me.printingFooter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.printingFooter.firstLetterCapital = False
        Me.printingFooter.firstLettersCapital = False
        Me.printingFooter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.printingFooter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.printingFooter.Location = New System.Drawing.Point(97, 50)
        Me.printingFooter.manageText = True
        Me.printingFooter.matchExp = ""
        Me.printingFooter.maximum = 0
        Me.printingFooter.MaxLength = 0
        Me.printingFooter.minimum = 0
        Me.printingFooter.Name = "printingFooter"
        Me.printingFooter.nbDecimals = CType(2, Short)
        Me.printingFooter.onlyAlphabet = False
        Me.printingFooter.refuseAccents = False
        Me.printingFooter.refusedChars = ""
        Me.printingFooter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.printingFooter.showInternalContextMenu = True
        Me.printingFooter.Size = New System.Drawing.Size(98, 20)
        Me.printingFooter.TabIndex = 125
        Me.printingFooter.Tag = "114"
        Me.printingFooter.Text = "&bPage &p / &P"
        Me.ToolTip1.SetToolTip(Me.printingFooter, "Valeur par défaut : &bPage &p / &P")
        Me.printingFooter.trimText = False
        '
        'printingHeader
        '
        Me.printingHeader.acceptAlpha = True
        Me.printingHeader.acceptedChars = ""
        Me.printingHeader.acceptNumeric = True
        Me.printingHeader.allCapital = False
        Me.printingHeader.allLower = False
        Me.printingHeader.BackColor = System.Drawing.SystemColors.Window
        Me.printingHeader.blockOnMaximum = False
        Me.printingHeader.blockOnMinimum = False
        Me.printingHeader.cb_AcceptLeftZeros = False
        Me.printingHeader.cb_AcceptNegative = False
        Me.printingHeader.currencyBox = False
        Me.printingHeader.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.printingHeader.firstLetterCapital = False
        Me.printingHeader.firstLettersCapital = False
        Me.printingHeader.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.printingHeader.ForeColor = System.Drawing.SystemColors.WindowText
        Me.printingHeader.Location = New System.Drawing.Point(97, 16)
        Me.printingHeader.manageText = True
        Me.printingHeader.matchExp = ""
        Me.printingHeader.maximum = 0
        Me.printingHeader.MaxLength = 0
        Me.printingHeader.minimum = 0
        Me.printingHeader.Name = "printingHeader"
        Me.printingHeader.nbDecimals = CType(2, Short)
        Me.printingHeader.onlyAlphabet = False
        Me.printingHeader.refuseAccents = False
        Me.printingHeader.refusedChars = ""
        Me.printingHeader.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.printingHeader.showInternalContextMenu = True
        Me.printingHeader.Size = New System.Drawing.Size(98, 20)
        Me.printingHeader.TabIndex = 125
        Me.printingHeader.Tag = "113"
        Me.ToolTip1.SetToolTip(Me.printingHeader, "Valer par défaut : Champ vide")
        Me.printingHeader.trimText = False
        '
        'PageCliniqueKP
        '
        Me.PageCliniqueKP.Controls.Add(Me._Frames_3)
        Me.PageCliniqueKP.Controls.Add(Me.label7)
        Me.PageCliniqueKP.Controls.Add(Me.medecinCategorie)
        Me.PageCliniqueKP.Location = New System.Drawing.Point(4, 25)
        Me.PageCliniqueKP.Name = "PageCliniqueKP"
        Me.PageCliniqueKP.Size = New System.Drawing.Size(681, 321)
        Me.PageCliniqueKP.TabIndex = 9
        Me.PageCliniqueKP.Text = "Personnes/organismes clés"
        Me.PageCliniqueKP.UseVisualStyleBackColor = True
        '
        '_Frames_3
        '
        Me._Frames_3.BackColor = System.Drawing.SystemColors.Control
        Me._Frames_3.Controls.Add(Me.copoc11)
        Me._Frames_3.Controls.Add(Me.copoc9)
        Me._Frames_3.Controls.Add(Me.copoc8)
        Me._Frames_3.Controls.Add(Me.copoc5)
        Me._Frames_3.Controls.Add(Me.copoc10)
        Me._Frames_3.Controls.Add(Me.copoc7)
        Me._Frames_3.Controls.Add(Me.copoc6)
        Me._Frames_3.Controls.Add(Me.copoc4)
        Me._Frames_3.Controls.Add(Me.copoc3)
        Me._Frames_3.Controls.Add(Me.copoc1)
        Me._Frames_3.Controls.Add(Me.copoc2)
        Me._Frames_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Frames_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frames_3.Location = New System.Drawing.Point(16, 3)
        Me._Frames_3.Name = "_Frames_3"
        Me._Frames_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Frames_3.Size = New System.Drawing.Size(305, 120)
        Me._Frames_3.TabIndex = 35
        Me._Frames_3.TabStop = False
        Me._Frames_3.Text = "Champs obligatoires - Personnes / Organismes clés"
        '
        'copoc11
        '
        Me.copoc11.BackColor = System.Drawing.SystemColors.Control
        Me.copoc11.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc11.Location = New System.Drawing.Point(8, 96)
        Me.copoc11.Name = "copoc11"
        Me.copoc11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc11.Size = New System.Drawing.Size(80, 17)
        Me.copoc11.TabIndex = 128
        Me.copoc11.Tag = "8"
        Me.copoc11.Text = "Employeur"
        Me.copoc11.UseVisualStyleBackColor = False
        '
        'copoc9
        '
        Me.copoc9.BackColor = System.Drawing.SystemColors.Control
        Me.copoc9.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc9.Location = New System.Drawing.Point(136, 64)
        Me.copoc9.Name = "copoc9"
        Me.copoc9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc9.Size = New System.Drawing.Size(144, 17)
        Me.copoc9.TabIndex = 127
        Me.copoc9.Tag = "71"
        Me.copoc9.Text = "Adresse du site internet"
        Me.copoc9.UseVisualStyleBackColor = False
        '
        'copoc8
        '
        Me.copoc8.BackColor = System.Drawing.SystemColors.Control
        Me.copoc8.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc8.Location = New System.Drawing.Point(136, 48)
        Me.copoc8.Name = "copoc8"
        Me.copoc8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc8.Size = New System.Drawing.Size(64, 17)
        Me.copoc8.TabIndex = 126
        Me.copoc8.Tag = "15"
        Me.copoc8.Text = "Courriel"
        Me.copoc8.UseVisualStyleBackColor = False
        '
        'copoc5
        '
        Me.copoc5.BackColor = System.Drawing.SystemColors.Control
        Me.copoc5.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc5.Location = New System.Drawing.Point(8, 80)
        Me.copoc5.Name = "copoc5"
        Me.copoc5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc5.Size = New System.Drawing.Size(88, 17)
        Me.copoc5.TabIndex = 44
        Me.copoc5.Tag = "28"
        Me.copoc5.Text = "Code postal"
        Me.copoc5.UseVisualStyleBackColor = False
        '
        'copoc10
        '
        Me.copoc10.BackColor = System.Drawing.SystemColors.Control
        Me.copoc10.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc10.Location = New System.Drawing.Point(136, 80)
        Me.copoc10.Name = "copoc10"
        Me.copoc10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc10.Size = New System.Drawing.Size(121, 17)
        Me.copoc10.TabIndex = 42
        Me.copoc10.Tag = "31"
        Me.copoc10.Text = "Autres informations"
        Me.copoc10.UseVisualStyleBackColor = False
        '
        'copoc7
        '
        Me.copoc7.BackColor = System.Drawing.SystemColors.Control
        Me.copoc7.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc7.Location = New System.Drawing.Point(136, 32)
        Me.copoc7.Name = "copoc7"
        Me.copoc7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc7.Size = New System.Drawing.Size(113, 17)
        Me.copoc7.TabIndex = 41
        Me.copoc7.Tag = "30"
        Me.copoc7.Text = "Numéro identifiant"
        Me.copoc7.UseVisualStyleBackColor = False
        '
        'copoc6
        '
        Me.copoc6.BackColor = System.Drawing.SystemColors.Control
        Me.copoc6.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc6.Location = New System.Drawing.Point(136, 16)
        Me.copoc6.Name = "copoc6"
        Me.copoc6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc6.Size = New System.Drawing.Size(81, 17)
        Me.copoc6.TabIndex = 40
        Me.copoc6.Tag = "29"
        Me.copoc6.Text = "Téléphone"
        Me.copoc6.UseVisualStyleBackColor = False
        '
        'copoc4
        '
        Me.copoc4.BackColor = System.Drawing.SystemColors.Control
        Me.copoc4.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc4.Location = New System.Drawing.Point(8, 64)
        Me.copoc4.Name = "copoc4"
        Me.copoc4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc4.Size = New System.Drawing.Size(48, 17)
        Me.copoc4.TabIndex = 39
        Me.copoc4.Tag = "27"
        Me.copoc4.Text = "Ville"
        Me.copoc4.UseVisualStyleBackColor = False
        '
        'copoc3
        '
        Me.copoc3.BackColor = System.Drawing.SystemColors.Control
        Me.copoc3.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc3.Location = New System.Drawing.Point(8, 48)
        Me.copoc3.Name = "copoc3"
        Me.copoc3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc3.Size = New System.Drawing.Size(65, 17)
        Me.copoc3.TabIndex = 38
        Me.copoc3.Tag = "26"
        Me.copoc3.Text = "Adresse"
        Me.copoc3.UseVisualStyleBackColor = False
        '
        'copoc1
        '
        Me.copoc1.BackColor = System.Drawing.SystemColors.Control
        Me.copoc1.Checked = True
        Me.copoc1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.copoc1.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc1.Location = New System.Drawing.Point(8, 16)
        Me.copoc1.Name = "copoc1"
        Me.copoc1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc1.Size = New System.Drawing.Size(49, 17)
        Me.copoc1.TabIndex = 37
        Me.copoc1.Tag = "24"
        Me.copoc1.Text = "Nom"
        Me.copoc1.UseVisualStyleBackColor = False
        '
        'copoc2
        '
        Me.copoc2.BackColor = System.Drawing.SystemColors.Control
        Me.copoc2.Checked = True
        Me.copoc2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.copoc2.Cursor = System.Windows.Forms.Cursors.Default
        Me.copoc2.Enabled = False
        Me.copoc2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copoc2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copoc2.Location = New System.Drawing.Point(8, 32)
        Me.copoc2.Name = "copoc2"
        Me.copoc2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copoc2.Size = New System.Drawing.Size(73, 17)
        Me.copoc2.TabIndex = 36
        Me.copoc2.Tag = "25"
        Me.copoc2.Text = "Catégorie"
        Me.copoc2.UseVisualStyleBackColor = False
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.Color.Transparent
        Me.label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label7.Location = New System.Drawing.Point(13, 147)
        Me.label7.Name = "label7"
        Me.label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label7.Size = New System.Drawing.Size(239, 14)
        Me.label7.TabIndex = 140
        Me.label7.Text = "Nom de la catégorie représentant les médecins :"
        '
        'medecinCategorie
        '
        Me.medecinCategorie.acceptAlpha = True
        Me.medecinCategorie.acceptedChars = ""
        Me.medecinCategorie.acceptNumeric = True
        Me.medecinCategorie.AcceptsReturn = True
        Me.medecinCategorie.allCapital = False
        Me.medecinCategorie.allLower = False
        Me.medecinCategorie.BackColor = System.Drawing.SystemColors.Window
        Me.medecinCategorie.blockOnMaximum = False
        Me.medecinCategorie.blockOnMinimum = False
        Me.medecinCategorie.cb_AcceptLeftZeros = False
        Me.medecinCategorie.cb_AcceptNegative = False
        Me.medecinCategorie.currencyBox = False
        Me.medecinCategorie.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.medecinCategorie.firstLetterCapital = False
        Me.medecinCategorie.firstLettersCapital = False
        Me.medecinCategorie.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.medecinCategorie.ForeColor = System.Drawing.SystemColors.WindowText
        Me.medecinCategorie.Location = New System.Drawing.Point(257, 145)
        Me.medecinCategorie.manageText = True
        Me.medecinCategorie.matchExp = ""
        Me.medecinCategorie.maximum = 0
        Me.medecinCategorie.MaxLength = 0
        Me.medecinCategorie.minimum = 0
        Me.medecinCategorie.Name = "medecinCategorie"
        Me.medecinCategorie.nbDecimals = CType(0, Short)
        Me.medecinCategorie.onlyAlphabet = False
        Me.medecinCategorie.refuseAccents = False
        Me.medecinCategorie.refusedChars = ":"
        Me.medecinCategorie.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.medecinCategorie.showInternalContextMenu = True
        Me.medecinCategorie.Size = New System.Drawing.Size(64, 20)
        Me.medecinCategorie.TabIndex = 124
        Me.medecinCategorie.Tag = "67"
        Me.medecinCategorie.Text = "Médecin"
        Me.medecinCategorie.trimText = False
        '
        'PageCliniqueRendezvous
        '
        Me.PageCliniqueRendezvous.Controls.Add(Me.textForPlageBloquee)
        Me.PageCliniqueRendezvous.Controls.Add(Me.nbJourForAutoQL)
        Me.PageCliniqueRendezvous.Controls.Add(Me.GroupTriRV)
        Me.PageCliniqueRendezvous.Controls.Add(Me.ActiveFolderAutoOnRVStatusChange)
        Me.PageCliniqueRendezvous.Controls.Add(Me.ShowQLOnAgendaRemove)
        Me.PageCliniqueRendezvous.Controls.Add(Me.AutoDelQLAfterNewRV)
        Me.PageCliniqueRendezvous.Controls.Add(Me.label39)
        Me.PageCliniqueRendezvous.Controls.Add(Me.label36)
        Me.PageCliniqueRendezvous.Controls.Add(Me.actionOnNoRV)
        Me.PageCliniqueRendezvous.Controls.Add(Me.label26)
        Me.PageCliniqueRendezvous.Controls.Add(Me.VerifyFrequenceForNewRV)
        Me.PageCliniqueRendezvous.Controls.Add(Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant)
        Me.PageCliniqueRendezvous.Controls.Add(Me.AllowToSkipAbsenceReasonInsertionToText)
        Me.PageCliniqueRendezvous.Controls.Add(Me.AlertTRPOnRVAbsence)
        Me.PageCliniqueRendezvous.Controls.Add(Me.ActiveFolderAutoOnRVAdding)
        Me.PageCliniqueRendezvous.Location = New System.Drawing.Point(4, 25)
        Me.PageCliniqueRendezvous.Name = "PageCliniqueRendezvous"
        Me.PageCliniqueRendezvous.Size = New System.Drawing.Size(681, 321)
        Me.PageCliniqueRendezvous.TabIndex = 8
        Me.PageCliniqueRendezvous.Text = "Rendez-vous"
        Me.PageCliniqueRendezvous.UseVisualStyleBackColor = True
        '
        'textForPlageBloquee
        '
        Me.textForPlageBloquee.acceptAlpha = True
        Me.textForPlageBloquee.acceptedChars = ""
        Me.textForPlageBloquee.acceptNumeric = True
        Me.textForPlageBloquee.AcceptsReturn = True
        Me.textForPlageBloquee.allCapital = False
        Me.textForPlageBloquee.allLower = False
        Me.textForPlageBloquee.BackColor = System.Drawing.SystemColors.Window
        Me.textForPlageBloquee.blockOnMaximum = False
        Me.textForPlageBloquee.blockOnMinimum = False
        Me.textForPlageBloquee.cb_AcceptLeftZeros = False
        Me.textForPlageBloquee.cb_AcceptNegative = False
        Me.textForPlageBloquee.currencyBox = False
        Me.textForPlageBloquee.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.textForPlageBloquee.firstLetterCapital = False
        Me.textForPlageBloquee.firstLettersCapital = False
        Me.textForPlageBloquee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textForPlageBloquee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.textForPlageBloquee.Location = New System.Drawing.Point(7, 178)
        Me.textForPlageBloquee.manageText = True
        Me.textForPlageBloquee.matchExp = ""
        Me.textForPlageBloquee.maximum = 0
        Me.textForPlageBloquee.MaxLength = 0
        Me.textForPlageBloquee.minimum = 0
        Me.textForPlageBloquee.Name = "textForPlageBloquee"
        Me.textForPlageBloquee.nbDecimals = CType(0, Short)
        Me.textForPlageBloquee.onlyAlphabet = False
        Me.textForPlageBloquee.refuseAccents = False
        Me.textForPlageBloquee.refusedChars = ""
        Me.textForPlageBloquee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.textForPlageBloquee.showInternalContextMenu = True
        Me.textForPlageBloquee.Size = New System.Drawing.Size(297, 20)
        Me.textForPlageBloquee.TabIndex = 133
        Me.textForPlageBloquee.Tag = "102"
        Me.textForPlageBloquee.Text = "Plage bloquée par la liste d'attente"
        Me.textForPlageBloquee.trimText = False
        '
        'nbJourForAutoQL
        '
        Me.nbJourForAutoQL.acceptAlpha = False
        Me.nbJourForAutoQL.acceptedChars = ",§."
        Me.nbJourForAutoQL.acceptNumeric = True
        Me.nbJourForAutoQL.AcceptsReturn = True
        Me.nbJourForAutoQL.allCapital = False
        Me.nbJourForAutoQL.allLower = False
        Me.nbJourForAutoQL.BackColor = System.Drawing.SystemColors.Window
        Me.nbJourForAutoQL.blockOnMaximum = False
        Me.nbJourForAutoQL.blockOnMinimum = False
        Me.nbJourForAutoQL.cb_AcceptLeftZeros = False
        Me.nbJourForAutoQL.cb_AcceptNegative = False
        Me.nbJourForAutoQL.currencyBox = True
        Me.nbJourForAutoQL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nbJourForAutoQL.firstLetterCapital = False
        Me.nbJourForAutoQL.firstLettersCapital = False
        Me.nbJourForAutoQL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbJourForAutoQL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nbJourForAutoQL.Location = New System.Drawing.Point(256, 113)
        Me.nbJourForAutoQL.manageText = True
        Me.nbJourForAutoQL.matchExp = ""
        Me.nbJourForAutoQL.maximum = 0
        Me.nbJourForAutoQL.MaxLength = 0
        Me.nbJourForAutoQL.minimum = 0
        Me.nbJourForAutoQL.Name = "nbJourForAutoQL"
        Me.nbJourForAutoQL.nbDecimals = CType(0, Short)
        Me.nbJourForAutoQL.onlyAlphabet = False
        Me.nbJourForAutoQL.refuseAccents = False
        Me.nbJourForAutoQL.refusedChars = ""
        Me.nbJourForAutoQL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbJourForAutoQL.showInternalContextMenu = True
        Me.nbJourForAutoQL.Size = New System.Drawing.Size(32, 20)
        Me.nbJourForAutoQL.TabIndex = 124
        Me.nbJourForAutoQL.Tag = "21"
        Me.nbJourForAutoQL.Text = "10"
        Me.nbJourForAutoQL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.nbJourForAutoQL, "0 jour signifie aucun ajout du rendez-vous en liste d'attente")
        Me.nbJourForAutoQL.trimText = False
        '
        'GroupTriRV
        '
        Me.GroupTriRV.Controls.Add(Me.triRVCompte)
        Me.GroupTriRV.Controls.Add(Me.triRVFuturs)
        Me.GroupTriRV.Controls.Add(Me.label17)
        Me.GroupTriRV.Controls.Add(Me.label16)
        Me.GroupTriRV.Location = New System.Drawing.Point(1, 204)
        Me.GroupTriRV.Name = "GroupTriRV"
        Me.GroupTriRV.Size = New System.Drawing.Size(303, 101)
        Me.GroupTriRV.TabIndex = 131
        Me.GroupTriRV.TabStop = False
        Me.GroupTriRV.Text = "Ordre de triage"
        '
        'triRVCompte
        '
        Me.triRVCompte.BackColor = System.Drawing.SystemColors.Window
        Me.triRVCompte.Cursor = System.Windows.Forms.Cursors.Default
        Me.triRVCompte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.triRVCompte.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.triRVCompte.ForeColor = System.Drawing.SystemColors.WindowText
        Me.triRVCompte.Items.AddRange(New Object() {"Ascendant", "Descendant"})
        Me.triRVCompte.Location = New System.Drawing.Point(6, 32)
        Me.triRVCompte.Name = "triRVCompte"
        Me.triRVCompte.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.triRVCompte.Size = New System.Drawing.Size(288, 22)
        Me.triRVCompte.TabIndex = 135
        Me.triRVCompte.Tag = "81"
        '
        'triRVFuturs
        '
        Me.triRVFuturs.BackColor = System.Drawing.SystemColors.Window
        Me.triRVFuturs.Cursor = System.Windows.Forms.Cursors.Default
        Me.triRVFuturs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.triRVFuturs.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.triRVFuturs.ForeColor = System.Drawing.SystemColors.WindowText
        Me.triRVFuturs.Items.AddRange(New Object() {"Ascendant", "Descendant"})
        Me.triRVFuturs.Location = New System.Drawing.Point(6, 72)
        Me.triRVFuturs.Name = "triRVFuturs"
        Me.triRVFuturs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.triRVFuturs.Size = New System.Drawing.Size(288, 22)
        Me.triRVFuturs.TabIndex = 137
        Me.triRVFuturs.Tag = "82"
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.BackColor = System.Drawing.SystemColors.Control
        Me.label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label17.Location = New System.Drawing.Point(6, 56)
        Me.label17.Name = "label17"
        Me.label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label17.Size = New System.Drawing.Size(110, 14)
        Me.label17.TabIndex = 138
        Me.label17.Text = "Rendez-vous futurs :"
        '
        'label16
        '
        Me.label16.AutoSize = True
        Me.label16.BackColor = System.Drawing.SystemColors.Control
        Me.label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.label16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label16.Location = New System.Drawing.Point(6, 16)
        Me.label16.Name = "label16"
        Me.label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label16.Size = New System.Drawing.Size(154, 14)
        Me.label16.TabIndex = 136
        Me.label16.Text = "Rendez-vous dans le compte :"
        '
        'ActiveFolderAutoOnRVStatusChange
        '
        Me.ActiveFolderAutoOnRVStatusChange.Location = New System.Drawing.Point(333, 0)
        Me.ActiveFolderAutoOnRVStatusChange.Name = "ActiveFolderAutoOnRVStatusChange"
        Me.ActiveFolderAutoOnRVStatusChange.Size = New System.Drawing.Size(340, 23)
        Me.ActiveFolderAutoOnRVStatusChange.TabIndex = 127
        Me.ActiveFolderAutoOnRVStatusChange.Tag = "126"
        Me.ActiveFolderAutoOnRVStatusChange.Text = "Activer le dossier automatiquement lors du changement de statut"
        '
        'ShowQLOnAgendaRemove
        '
        Me.ShowQLOnAgendaRemove.Checked = True
        Me.ShowQLOnAgendaRemove.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowQLOnAgendaRemove.Location = New System.Drawing.Point(333, 210)
        Me.ShowQLOnAgendaRemove.Name = "ShowQLOnAgendaRemove"
        Me.ShowQLOnAgendaRemove.Size = New System.Drawing.Size(304, 26)
        Me.ShowQLOnAgendaRemove.TabIndex = 139
        Me.ShowQLOnAgendaRemove.Tag = "66"
        Me.ShowQLOnAgendaRemove.Text = "Vérification de la liste d'attente lorsqu'une plage se libère"
        '
        'AutoDelQLAfterNewRV
        '
        Me.AutoDelQLAfterNewRV.Checked = True
        Me.AutoDelQLAfterNewRV.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoDelQLAfterNewRV.Location = New System.Drawing.Point(7, 3)
        Me.AutoDelQLAfterNewRV.Name = "AutoDelQLAfterNewRV"
        Me.AutoDelQLAfterNewRV.Size = New System.Drawing.Size(320, 43)
        Me.AutoDelQLAfterNewRV.TabIndex = 127
        Me.AutoDelQLAfterNewRV.Tag = "76"
        Me.AutoDelQLAfterNewRV.Text = "Effacement automatique d'un client sur la liste d'attente sans rendez-vous lors d" & _
            "e la prise d'un nouveau rendez-vous"
        '
        'label39
        '
        Me.label39.AutoSize = True
        Me.label39.BackColor = System.Drawing.SystemColors.Control
        Me.label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.label39.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label39.Location = New System.Drawing.Point(7, 161)
        Me.label39.Name = "label39"
        Me.label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label39.Size = New System.Drawing.Size(301, 14)
        Me.label39.TabIndex = 134
        Me.label39.Text = "Texte à afficher pour une plage bloquée par la liste d'attente :"
        '
        'label36
        '
        Me.label36.BackColor = System.Drawing.Color.Transparent
        Me.label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.label36.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label36.Location = New System.Drawing.Point(7, 49)
        Me.label36.Name = "label36"
        Me.label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label36.Size = New System.Drawing.Size(305, 32)
        Me.label36.TabIndex = 123
        Me.label36.Text = "Lorsqu'un changement de statut est effectué et qu'il n'y a pas de rendez-vous fut" & _
            "ur :"
        '
        'actionOnNoRV
        '
        Me.actionOnNoRV.BackColor = System.Drawing.SystemColors.Window
        Me.actionOnNoRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.actionOnNoRV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.actionOnNoRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.actionOnNoRV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.actionOnNoRV.Items.AddRange(New Object() {"Toujours demander quoi faire", "Demander quoi faire sauf pour le futur", "Demander quoi faire uniquement pour la journée en cours", "Prendre un rendez-vous automatiquement", "Fermer le dossier automatiquement", "Ne rien faire"})
        Me.actionOnNoRV.Location = New System.Drawing.Point(7, 83)
        Me.actionOnNoRV.Name = "actionOnNoRV"
        Me.actionOnNoRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.actionOnNoRV.Size = New System.Drawing.Size(304, 22)
        Me.actionOnNoRV.TabIndex = 130
        Me.actionOnNoRV.Tag = "77"
        '
        'label26
        '
        Me.label26.AutoSize = True
        Me.label26.BackColor = System.Drawing.SystemColors.Control
        Me.label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.label26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label26.Location = New System.Drawing.Point(7, 113)
        Me.label26.Name = "label26"
        Me.label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label26.Size = New System.Drawing.Size(307, 14)
        Me.label26.TabIndex = 133
        Me.label26.Text = "Ajout automatique sur la liste d'attente à partir du             e jour"
        Me.ToolTip1.SetToolTip(Me.label26, "0 jour signifie aucun ajout du rendez-vous en liste d'attente")
        '
        'VerifyFrequenceForNewRV
        '
        Me.VerifyFrequenceForNewRV.AutoSize = True
        Me.VerifyFrequenceForNewRV.Checked = True
        Me.VerifyFrequenceForNewRV.CheckState = System.Windows.Forms.CheckState.Checked
        Me.VerifyFrequenceForNewRV.Location = New System.Drawing.Point(333, 28)
        Me.VerifyFrequenceForNewRV.Name = "VerifyFrequenceForNewRV"
        Me.VerifyFrequenceForNewRV.Size = New System.Drawing.Size(280, 18)
        Me.VerifyFrequenceForNewRV.TabIndex = 127
        Me.VerifyFrequenceForNewRV.Tag = "23"
        Me.VerifyFrequenceForNewRV.Text = "Vérifier la fréquence lors d'un nouveau rendez-vous"
        '
        'ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant
        '
        Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant.Location = New System.Drawing.Point(333, 77)
        Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant.Name = "ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant"
        Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant.Size = New System.Drawing.Size(349, 39)
        Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant.TabIndex = 127
        Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant.Tag = "128"
        Me.ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant.Text = "Confirmer la prise de rendez-vous lorsque le thérapeute traitant n'est pas le thé" & _
            "rapeute réel"
        '
        'AllowToSkipAbsenceReasonInsertionToText
        '
        Me.AllowToSkipAbsenceReasonInsertionToText.Location = New System.Drawing.Point(333, 153)
        Me.AllowToSkipAbsenceReasonInsertionToText.Name = "AllowToSkipAbsenceReasonInsertionToText"
        Me.AllowToSkipAbsenceReasonInsertionToText.Size = New System.Drawing.Size(327, 51)
        Me.AllowToSkipAbsenceReasonInsertionToText.TabIndex = 127
        Me.AllowToSkipAbsenceReasonInsertionToText.Tag = "127"
        Me.AllowToSkipAbsenceReasonInsertionToText.Text = "Permettre de sauter l'insertion de la raison d'une absence au sein du texte par d" & _
            "éfaut du dossier du rendez-vous lorsque le texte est en cours d'utilisation"
        '
        'AlertTRPOnRVAbsence
        '
        Me.AlertTRPOnRVAbsence.Checked = True
        Me.AlertTRPOnRVAbsence.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AlertTRPOnRVAbsence.Location = New System.Drawing.Point(333, 113)
        Me.AlertTRPOnRVAbsence.Name = "AlertTRPOnRVAbsence"
        Me.AlertTRPOnRVAbsence.Size = New System.Drawing.Size(327, 33)
        Me.AlertTRPOnRVAbsence.TabIndex = 127
        Me.AlertTRPOnRVAbsence.Tag = "127"
        Me.AlertTRPOnRVAbsence.Text = "Alerter le thérapeute réel lors d'un changement de statut d'un rendez-vous vers u" & _
            "ne absence"
        '
        'ActiveFolderAutoOnRVAdding
        '
        Me.ActiveFolderAutoOnRVAdding.AutoSize = True
        Me.ActiveFolderAutoOnRVAdding.Location = New System.Drawing.Point(333, 53)
        Me.ActiveFolderAutoOnRVAdding.Name = "ActiveFolderAutoOnRVAdding"
        Me.ActiveFolderAutoOnRVAdding.Size = New System.Drawing.Size(349, 18)
        Me.ActiveFolderAutoOnRVAdding.TabIndex = 127
        Me.ActiveFolderAutoOnRVAdding.Tag = "127"
        Me.ActiveFolderAutoOnRVAdding.Text = "Activer le dossier automatiquement lors de l'ajout d'un rendez-vous"
        '
        'PageCliniqueUtilisateur
        '
        Me.PageCliniqueUtilisateur.Controls.Add(Me.AdministratorPassword)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.hideEndedUsersFolder)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.nbEvalTo100TauxAutonomie)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.Label76)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.label47)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.Services)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.affLastUserType)
        Me.PageCliniqueUtilisateur.Controls.Add(Me._Labels_0)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.label8)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.lastUserType)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.AffAdminInAcces)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.renamingService)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.AffDefCodesInSpecificTRP)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.addingService)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.removingService)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.AllowUserEmptyPassword)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.UserMDPRespectCasse)
        Me.PageCliniqueUtilisateur.Controls.Add(Me.AutoSelectLastUserInAcces)
        Me.PageCliniqueUtilisateur.Location = New System.Drawing.Point(4, 25)
        Me.PageCliniqueUtilisateur.Name = "PageCliniqueUtilisateur"
        Me.PageCliniqueUtilisateur.Padding = New System.Windows.Forms.Padding(3)
        Me.PageCliniqueUtilisateur.Size = New System.Drawing.Size(681, 321)
        Me.PageCliniqueUtilisateur.TabIndex = 1
        Me.PageCliniqueUtilisateur.Text = "Utilisateur"
        Me.PageCliniqueUtilisateur.UseVisualStyleBackColor = True
        '
        'AdministratorPassword
        '
        Me.AdministratorPassword.acceptAlpha = True
        Me.AdministratorPassword.acceptedChars = ""
        Me.AdministratorPassword.acceptNumeric = True
        Me.AdministratorPassword.AcceptsReturn = True
        Me.AdministratorPassword.allCapital = False
        Me.AdministratorPassword.allLower = False
        Me.AdministratorPassword.BackColor = System.Drawing.SystemColors.Window
        Me.AdministratorPassword.blockOnMaximum = False
        Me.AdministratorPassword.blockOnMinimum = False
        Me.AdministratorPassword.cb_AcceptLeftZeros = False
        Me.AdministratorPassword.cb_AcceptNegative = False
        Me.AdministratorPassword.currencyBox = False
        Me.AdministratorPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.AdministratorPassword.firstLetterCapital = False
        Me.AdministratorPassword.firstLettersCapital = False
        Me.AdministratorPassword.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdministratorPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdministratorPassword.Location = New System.Drawing.Point(360, 19)
        Me.AdministratorPassword.manageText = True
        Me.AdministratorPassword.matchExp = ""
        Me.AdministratorPassword.maximum = 0
        Me.AdministratorPassword.MaxLength = 100
        Me.AdministratorPassword.minimum = 0
        Me.AdministratorPassword.Name = "AdministratorPassword"
        Me.AdministratorPassword.nbDecimals = CType(0, Short)
        Me.AdministratorPassword.onlyAlphabet = False
        Me.AdministratorPassword.refuseAccents = False
        Me.AdministratorPassword.refusedChars = ""
        Me.AdministratorPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AdministratorPassword.showInternalContextMenu = True
        Me.AdministratorPassword.Size = New System.Drawing.Size(294, 20)
        Me.AdministratorPassword.TabIndex = 141
        Me.AdministratorPassword.Tag = "94"
        Me.AdministratorPassword.trimText = False
        '
        'hideEndedUsersFolder
        '
        Me.hideEndedUsersFolder.AutoSize = True
        Me.hideEndedUsersFolder.BackColor = System.Drawing.SystemColors.Control
        Me.hideEndedUsersFolder.Checked = True
        Me.hideEndedUsersFolder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.hideEndedUsersFolder.Cursor = System.Windows.Forms.Cursors.Default
        Me.hideEndedUsersFolder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hideEndedUsersFolder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hideEndedUsersFolder.Location = New System.Drawing.Point(6, 134)
        Me.hideEndedUsersFolder.Name = "hideEndedUsersFolder"
        Me.hideEndedUsersFolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hideEndedUsersFolder.Size = New System.Drawing.Size(351, 18)
        Me.hideEndedUsersFolder.TabIndex = 140
        Me.hideEndedUsersFolder.Tag = "9"
        Me.hideEndedUsersFolder.Text = "Cacher les dossiers des utilisateurs ayant une date de fin de travail"
        Me.hideEndedUsersFolder.UseVisualStyleBackColor = False
        '
        'nbEvalTo100TauxAutonomie
        '
        Me.nbEvalTo100TauxAutonomie.acceptAlpha = False
        Me.nbEvalTo100TauxAutonomie.acceptedChars = ",§."
        Me.nbEvalTo100TauxAutonomie.acceptNumeric = True
        Me.nbEvalTo100TauxAutonomie.AcceptsReturn = True
        Me.nbEvalTo100TauxAutonomie.allCapital = False
        Me.nbEvalTo100TauxAutonomie.allLower = False
        Me.nbEvalTo100TauxAutonomie.BackColor = System.Drawing.SystemColors.Window
        Me.nbEvalTo100TauxAutonomie.blockOnMaximum = False
        Me.nbEvalTo100TauxAutonomie.blockOnMinimum = False
        Me.nbEvalTo100TauxAutonomie.cb_AcceptLeftZeros = False
        Me.nbEvalTo100TauxAutonomie.cb_AcceptNegative = False
        Me.nbEvalTo100TauxAutonomie.currencyBox = True
        Me.nbEvalTo100TauxAutonomie.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nbEvalTo100TauxAutonomie.firstLetterCapital = False
        Me.nbEvalTo100TauxAutonomie.firstLettersCapital = False
        Me.nbEvalTo100TauxAutonomie.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbEvalTo100TauxAutonomie.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nbEvalTo100TauxAutonomie.Location = New System.Drawing.Point(512, 173)
        Me.nbEvalTo100TauxAutonomie.manageText = True
        Me.nbEvalTo100TauxAutonomie.matchExp = ""
        Me.nbEvalTo100TauxAutonomie.maximum = 0
        Me.nbEvalTo100TauxAutonomie.MaxLength = 0
        Me.nbEvalTo100TauxAutonomie.minimum = 0
        Me.nbEvalTo100TauxAutonomie.Name = "nbEvalTo100TauxAutonomie"
        Me.nbEvalTo100TauxAutonomie.nbDecimals = CType(0, Short)
        Me.nbEvalTo100TauxAutonomie.onlyAlphabet = False
        Me.nbEvalTo100TauxAutonomie.refuseAccents = False
        Me.nbEvalTo100TauxAutonomie.refusedChars = ""
        Me.nbEvalTo100TauxAutonomie.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbEvalTo100TauxAutonomie.showInternalContextMenu = True
        Me.nbEvalTo100TauxAutonomie.Size = New System.Drawing.Size(32, 20)
        Me.nbEvalTo100TauxAutonomie.TabIndex = 138
        Me.nbEvalTo100TauxAutonomie.Tag = "117"
        Me.nbEvalTo100TauxAutonomie.Text = "5"
        Me.nbEvalTo100TauxAutonomie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nbEvalTo100TauxAutonomie.trimText = False
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.BackColor = System.Drawing.SystemColors.Control
        Me.Label76.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label76.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label76.Location = New System.Drawing.Point(357, 3)
        Me.Label76.Name = "Label76"
        Me.Label76.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label76.Size = New System.Drawing.Size(149, 14)
        Me.Label76.TabIndex = 139
        Me.Label76.Text = "Mot de passe administrateur :"
        '
        'label47
        '
        Me.label47.AutoSize = True
        Me.label47.BackColor = System.Drawing.SystemColors.Control
        Me.label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.label47.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label47.Location = New System.Drawing.Point(3, 176)
        Me.label47.Name = "label47"
        Me.label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label47.Size = New System.Drawing.Size(503, 14)
        Me.label47.TabIndex = 139
        Me.label47.Text = "Nombre de dossiers demandés pour l'utilisateur par semaine pour atteindre 100% du" & _
            " taux d'autonomie :"
        '
        'Services
        '
        Me.Services.HorizontalScrollbar = True
        Me.Services.ItemHeight = 14
        Me.Services.Location = New System.Drawing.Point(6, 19)
        Me.Services.Name = "Services"
        Me.Services.Size = New System.Drawing.Size(232, 46)
        Me.Services.Sorted = True
        Me.Services.TabIndex = 133
        Me.Services.Tag = "68"
        '
        'affLastUserType
        '
        Me.affLastUserType.BackColor = System.Drawing.SystemColors.Control
        Me.affLastUserType.Cursor = System.Windows.Forms.Cursors.Default
        Me.affLastUserType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affLastUserType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affLastUserType.Location = New System.Drawing.Point(6, 297)
        Me.affLastUserType.Name = "affLastUserType"
        Me.affLastUserType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.affLastUserType.Size = New System.Drawing.Size(240, 17)
        Me.affLastUserType.TabIndex = 31
        Me.affLastUserType.Tag = "0"
        Me.affLastUserType.Text = "Afficher le dernier type d'utlisateur visualisé"
        Me.affLastUserType.UseVisualStyleBackColor = False
        '
        '_Labels_0
        '
        Me._Labels_0.AutoSize = True
        Me._Labels_0.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_0.Location = New System.Drawing.Point(321, 297)
        Me._Labels_0.Name = "_Labels_0"
        Me._Labels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_0.Size = New System.Drawing.Size(137, 14)
        Me._Labels_0.TabIndex = 32
        Me._Labels_0.Text = "- Type d'utilisateur affiché :"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(6, 3)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(189, 14)
        Me.label8.TabIndex = 120
        Me.label8.Text = "Services offerts par les thérapeutes :"
        '
        'lastUserType
        '
        Me.lastUserType.AutoSize = True
        Me.lastUserType.BackColor = System.Drawing.SystemColors.Window
        Me.lastUserType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lastUserType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lastUserType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lastUserType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lastUserType.Location = New System.Drawing.Point(457, 298)
        Me.lastUserType.Name = "lastUserType"
        Me.lastUserType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lastUserType.Size = New System.Drawing.Size(2, 16)
        Me.lastUserType.TabIndex = 33
        Me.lastUserType.Tag = "1"
        '
        'AffAdminInAcces
        '
        Me.AffAdminInAcces.AutoSize = True
        Me.AffAdminInAcces.Location = New System.Drawing.Point(6, 236)
        Me.AffAdminInAcces.Name = "AffAdminInAcces"
        Me.AffAdminInAcces.Size = New System.Drawing.Size(310, 18)
        Me.AffAdminInAcces.TabIndex = 137
        Me.AffAdminInAcces.Tag = "85"
        Me.AffAdminInAcces.Text = "Afficher 'Administrateur' dans la fenêtre d'accès au logiciel"
        '
        'renamingService
        '
        Me.renamingService.BackColor = System.Drawing.SystemColors.Control
        Me.renamingService.Cursor = System.Windows.Forms.Cursors.Default
        Me.renamingService.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.renamingService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renamingService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.renamingService.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.renamingService.Location = New System.Drawing.Point(246, 43)
        Me.renamingService.Name = "renamingService"
        Me.renamingService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.renamingService.Size = New System.Drawing.Size(24, 24)
        Me.renamingService.TabIndex = 137
        Me.renamingService.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.renamingService, "Renommer le service sélectionné")
        Me.renamingService.UseVisualStyleBackColor = False
        '
        'AffDefCodesInSpecificTRP
        '
        Me.AffDefCodesInSpecificTRP.Checked = True
        Me.AffDefCodesInSpecificTRP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AffDefCodesInSpecificTRP.Location = New System.Drawing.Point(6, 199)
        Me.AffDefCodesInSpecificTRP.Name = "AffDefCodesInSpecificTRP"
        Me.AffDefCodesInSpecificTRP.Size = New System.Drawing.Size(456, 20)
        Me.AffDefCodesInSpecificTRP.TabIndex = 127
        Me.AffDefCodesInSpecificTRP.Tag = "99"
        Me.AffDefCodesInSpecificTRP.Text = "Afficher les codifications par défaut manquantes à un thérapeute spécifique"
        '
        'addingService
        '
        Me.addingService.BackColor = System.Drawing.SystemColors.Control
        Me.addingService.Cursor = System.Windows.Forms.Cursors.Default
        Me.addingService.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addingService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addingService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addingService.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.addingService.Location = New System.Drawing.Point(258, 19)
        Me.addingService.Name = "addingService"
        Me.addingService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.addingService.Size = New System.Drawing.Size(24, 24)
        Me.addingService.TabIndex = 134
        Me.addingService.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.addingService, "Ajout d'un service")
        Me.addingService.UseVisualStyleBackColor = False
        '
        'removingService
        '
        Me.removingService.BackColor = System.Drawing.SystemColors.Control
        Me.removingService.Cursor = System.Windows.Forms.Cursors.Default
        Me.removingService.Enabled = False
        Me.removingService.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.removingService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.removingService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.removingService.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.removingService.Location = New System.Drawing.Point(270, 43)
        Me.removingService.Name = "removingService"
        Me.removingService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.removingService.Size = New System.Drawing.Size(24, 24)
        Me.removingService.TabIndex = 135
        Me.removingService.Tag = "-1"
        Me.ToolTip1.SetToolTip(Me.removingService, "Enlever le service sélectionné")
        Me.removingService.UseVisualStyleBackColor = False
        '
        'AllowUserEmptyPassword
        '
        Me.AllowUserEmptyPassword.Location = New System.Drawing.Point(6, 100)
        Me.AllowUserEmptyPassword.Name = "AllowUserEmptyPassword"
        Me.AllowUserEmptyPassword.Size = New System.Drawing.Size(288, 20)
        Me.AllowUserEmptyPassword.TabIndex = 121
        Me.AllowUserEmptyPassword.Tag = "69"
        Me.AllowUserEmptyPassword.Text = "Permettre qu'un utilisateur n'est pas de mot de passe"
        '
        'UserMDPRespectCasse
        '
        Me.UserMDPRespectCasse.Checked = True
        Me.UserMDPRespectCasse.CheckState = System.Windows.Forms.CheckState.Checked
        Me.UserMDPRespectCasse.Location = New System.Drawing.Point(6, 82)
        Me.UserMDPRespectCasse.Name = "UserMDPRespectCasse"
        Me.UserMDPRespectCasse.Size = New System.Drawing.Size(280, 16)
        Me.UserMDPRespectCasse.TabIndex = 121
        Me.UserMDPRespectCasse.Tag = "69"
        Me.UserMDPRespectCasse.Text = "Respecter la casse du mot de passe d'un utilisateur"
        '
        'AutoSelectLastUserInAcces
        '
        Me.AutoSelectLastUserInAcces.Checked = True
        Me.AutoSelectLastUserInAcces.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoSelectLastUserInAcces.Location = New System.Drawing.Point(6, 260)
        Me.AutoSelectLastUserInAcces.Name = "AutoSelectLastUserInAcces"
        Me.AutoSelectLastUserInAcces.Size = New System.Drawing.Size(288, 16)
        Me.AutoSelectLastUserInAcces.TabIndex = 136
        Me.AutoSelectLastUserInAcces.Tag = "84"
        Me.AutoSelectLastUserInAcces.Text = "Sélection automatique du dernier utilisateur par poste"
        '
        'FrameHidden
        '
        Me.FrameHidden.Controls.Add(Me.ActivateWorkHoursApprobation)
        Me.FrameHidden.Controls.Add(Me.label33)
        Me.FrameHidden.Controls.Add(Me.punchModeArrival)
        Me.FrameHidden.Controls.Add(Me.label34)
        Me.FrameHidden.Controls.Add(Me.punchModeDeparture)
        Me.FrameHidden.Location = New System.Drawing.Point(29, 395)
        Me.FrameHidden.Name = "FrameHidden"
        Me.FrameHidden.Size = New System.Drawing.Size(389, 126)
        Me.FrameHidden.TabIndex = 155
        Me.FrameHidden.TabStop = False
        Me.FrameHidden.Text = "Préférences non-utilisé dans cette version du logiciel"
        Me.FrameHidden.Visible = False
        '
        'ActivateWorkHoursApprobation
        '
        Me.ActivateWorkHoursApprobation.Checked = True
        Me.ActivateWorkHoursApprobation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ActivateWorkHoursApprobation.Location = New System.Drawing.Point(15, 13)
        Me.ActivateWorkHoursApprobation.Name = "ActivateWorkHoursApprobation"
        Me.ActivateWorkHoursApprobation.Size = New System.Drawing.Size(233, 16)
        Me.ActivateWorkHoursApprobation.TabIndex = 127
        Me.ActivateWorkHoursApprobation.Tag = "96"
        Me.ActivateWorkHoursApprobation.Text = "Activer l'approbation des quarts de travail"
        Me.ActivateWorkHoursApprobation.Visible = False
        '
        'label33
        '
        Me.label33.AutoSize = True
        Me.label33.BackColor = System.Drawing.SystemColors.Control
        Me.label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.label33.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label33.Location = New System.Drawing.Point(19, 33)
        Me.label33.Name = "label33"
        Me.label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label33.Size = New System.Drawing.Size(228, 14)
        Me.label33.TabIndex = 133
        Me.label33.Text = "Mode du 'punch' virtuel pour l'heure d'arrivée :"
        Me.label33.Visible = False
        '
        'punchModeArrival
        '
        Me.punchModeArrival.BackColor = System.Drawing.SystemColors.Window
        Me.punchModeArrival.Cursor = System.Windows.Forms.Cursors.Default
        Me.punchModeArrival.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.punchModeArrival.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.punchModeArrival.ForeColor = System.Drawing.SystemColors.WindowText
        Me.punchModeArrival.Items.AddRange(New Object() {"Arrondir aux 15 minutes d'avant ou d'après", "Toujours arrondir aux 15 minutes d'avant", "Toujours arrondir aux 15 minutes d'après"})
        Me.punchModeArrival.Location = New System.Drawing.Point(16, 48)
        Me.punchModeArrival.Name = "punchModeArrival"
        Me.punchModeArrival.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.punchModeArrival.Size = New System.Drawing.Size(288, 22)
        Me.punchModeArrival.TabIndex = 137
        Me.punchModeArrival.Tag = "97"
        Me.punchModeArrival.Visible = False
        '
        'label34
        '
        Me.label34.AutoSize = True
        Me.label34.BackColor = System.Drawing.SystemColors.Control
        Me.label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.label34.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label34.Location = New System.Drawing.Point(19, 82)
        Me.label34.Name = "label34"
        Me.label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label34.Size = New System.Drawing.Size(232, 14)
        Me.label34.TabIndex = 133
        Me.label34.Text = "Mode du 'punch' virtuel pour l'heure de départ :"
        '
        'punchModeDeparture
        '
        Me.punchModeDeparture.BackColor = System.Drawing.SystemColors.Window
        Me.punchModeDeparture.Cursor = System.Windows.Forms.Cursors.Default
        Me.punchModeDeparture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.punchModeDeparture.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.punchModeDeparture.ForeColor = System.Drawing.SystemColors.WindowText
        Me.punchModeDeparture.Items.AddRange(New Object() {"Arrondir aux 15 minutes d'avant ou d'après", "Toujours arrondir aux 15 minutes d'avant", "Toujours arrondir aux 15 minutes d'après"})
        Me.punchModeDeparture.Location = New System.Drawing.Point(16, 96)
        Me.punchModeDeparture.Name = "punchModeDeparture"
        Me.punchModeDeparture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.punchModeDeparture.Size = New System.Drawing.Size(288, 22)
        Me.punchModeDeparture.TabIndex = 137
        Me.punchModeDeparture.Tag = "98"
        '
        'save
        '
        Me.save.BackColor = System.Drawing.SystemColors.Control
        Me.save.Cursor = System.Windows.Forms.Cursors.Default
        Me.save.Enabled = False
        Me.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.save.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save.ForeColor = System.Drawing.SystemColors.ControlText
        Me.save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.save.Location = New System.Drawing.Point(683, 4)
        Me.save.Name = "save"
        Me.save.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.save.Size = New System.Drawing.Size(24, 24)
        Me.save.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.save, "Enregistrer les préférences")
        Me.save.UseVisualStyleBackColor = False
        '
        'FDialog
        '
        Me.FDialog.ShowColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'SelectSonMenu
        '
        Me.SelectSonMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AucunToolStripMenuItem, Me.ParDéfautToolStripMenuItem, Me.DepuisLaBanqueDeDonnéesToolStripMenuItem})
        Me.SelectSonMenu.Name = "SelectSonMenu"
        Me.SelectSonMenu.ShowCheckMargin = True
        Me.SelectSonMenu.ShowImageMargin = False
        Me.SelectSonMenu.Size = New System.Drawing.Size(230, 70)
        '
        'AucunToolStripMenuItem
        '
        Me.AucunToolStripMenuItem.Checked = True
        Me.AucunToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AucunToolStripMenuItem.Name = "AucunToolStripMenuItem"
        Me.AucunToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.AucunToolStripMenuItem.Text = "Aucun"
        '
        'ParDéfautToolStripMenuItem
        '
        Me.ParDéfautToolStripMenuItem.Name = "ParDéfautToolStripMenuItem"
        Me.ParDéfautToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.ParDéfautToolStripMenuItem.Text = "Par défaut"
        '
        'DepuisLaBanqueDeDonnéesToolStripMenuItem
        '
        Me.DepuisLaBanqueDeDonnéesToolStripMenuItem.Name = "DepuisLaBanqueDeDonnéesToolStripMenuItem"
        Me.DepuisLaBanqueDeDonnéesToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.DepuisLaBanqueDeDonnéesToolStripMenuItem.Text = "Depuis la banque de données"
        '
        'SelectRemoteTaskDBPath
        '
        Me.SelectRemoteTaskDBPath.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NePasEnregistrerToolStripMenuItem, Me.VersLaBanqueDeDonnéesToolStripMenuItem})
        Me.SelectRemoteTaskDBPath.Name = "SelectSonMenu"
        Me.SelectRemoteTaskDBPath.ShowCheckMargin = True
        Me.SelectRemoteTaskDBPath.ShowImageMargin = False
        Me.SelectRemoteTaskDBPath.Size = New System.Drawing.Size(215, 48)
        '
        'NePasEnregistrerToolStripMenuItem
        '
        Me.NePasEnregistrerToolStripMenuItem.Name = "NePasEnregistrerToolStripMenuItem"
        Me.NePasEnregistrerToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.NePasEnregistrerToolStripMenuItem.Text = "Ne pas enregistrer"
        '
        'VersLaBanqueDeDonnéesToolStripMenuItem
        '
        Me.VersLaBanqueDeDonnéesToolStripMenuItem.Name = "VersLaBanqueDeDonnéesToolStripMenuItem"
        Me.VersLaBanqueDeDonnéesToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.VersLaBanqueDeDonnéesToolStripMenuItem.Text = "Vers la banque de données"
        '
        'changeAbsenceTypeForSpecificText
        '
        Me.changeAbsenceTypeForSpecificText.Checked = True
        Me.changeAbsenceTypeForSpecificText.CheckState = System.Windows.Forms.CheckState.Checked
        Me.changeAbsenceTypeForSpecificText.Location = New System.Drawing.Point(6, 380)
        Me.changeAbsenceTypeForSpecificText.Name = "changeAbsenceTypeForSpecificText"
        Me.changeAbsenceTypeForSpecificText.Size = New System.Drawing.Size(485, 20)
        Me.changeAbsenceTypeForSpecificText.TabIndex = 155
        Me.changeAbsenceTypeForSpecificText.Tag = "106"
        Me.changeAbsenceTypeForSpecificText.Text = "Dans les reçus, modifier le type de facture des absences non-motivées pour le tex" & _
            "te suivant :"
        '
        'textToReplaceAbsNotMotivatedInReceipt
        '
        Me.textToReplaceAbsNotMotivatedInReceipt.acceptAlpha = True
        Me.textToReplaceAbsNotMotivatedInReceipt.acceptedChars = ""
        Me.textToReplaceAbsNotMotivatedInReceipt.acceptNumeric = True
        Me.textToReplaceAbsNotMotivatedInReceipt.AcceptsReturn = True
        Me.textToReplaceAbsNotMotivatedInReceipt.allCapital = False
        Me.textToReplaceAbsNotMotivatedInReceipt.allLower = False
        Me.textToReplaceAbsNotMotivatedInReceipt.BackColor = System.Drawing.SystemColors.Window
        Me.textToReplaceAbsNotMotivatedInReceipt.blockOnMaximum = False
        Me.textToReplaceAbsNotMotivatedInReceipt.blockOnMinimum = False
        Me.textToReplaceAbsNotMotivatedInReceipt.cb_AcceptLeftZeros = False
        Me.textToReplaceAbsNotMotivatedInReceipt.cb_AcceptNegative = False
        Me.textToReplaceAbsNotMotivatedInReceipt.currencyBox = False
        Me.textToReplaceAbsNotMotivatedInReceipt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.textToReplaceAbsNotMotivatedInReceipt.firstLetterCapital = False
        Me.textToReplaceAbsNotMotivatedInReceipt.firstLettersCapital = False
        Me.textToReplaceAbsNotMotivatedInReceipt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textToReplaceAbsNotMotivatedInReceipt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.textToReplaceAbsNotMotivatedInReceipt.Location = New System.Drawing.Point(488, 379)
        Me.textToReplaceAbsNotMotivatedInReceipt.manageText = True
        Me.textToReplaceAbsNotMotivatedInReceipt.matchExp = ""
        Me.textToReplaceAbsNotMotivatedInReceipt.maximum = 0
        Me.textToReplaceAbsNotMotivatedInReceipt.MaxLength = 0
        Me.textToReplaceAbsNotMotivatedInReceipt.minimum = 0
        Me.textToReplaceAbsNotMotivatedInReceipt.Name = "textToReplaceAbsNotMotivatedInReceipt"
        Me.textToReplaceAbsNotMotivatedInReceipt.nbDecimals = CType(0, Short)
        Me.textToReplaceAbsNotMotivatedInReceipt.onlyAlphabet = False
        Me.textToReplaceAbsNotMotivatedInReceipt.refuseAccents = False
        Me.textToReplaceAbsNotMotivatedInReceipt.refusedChars = ""
        Me.textToReplaceAbsNotMotivatedInReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.textToReplaceAbsNotMotivatedInReceipt.showInternalContextMenu = True
        Me.textToReplaceAbsNotMotivatedInReceipt.Size = New System.Drawing.Size(145, 20)
        Me.textToReplaceAbsNotMotivatedInReceipt.TabIndex = 156
        Me.textToReplaceAbsNotMotivatedInReceipt.Tag = "101"
        Me.textToReplaceAbsNotMotivatedInReceipt.Text = "Frais administratif"
        Me.textToReplaceAbsNotMotivatedInReceipt.trimText = False
        '
        'preferencesWin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(719, 397)
        Me.Controls.Add(Me.save)
        Me.Controls.Add(Me.tabPref)
        Me.Controls.Add(Me.FrameHidden)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(160, 125)
        Me.MaximizeBox = False
        Me.Name = "preferencesWin"
        Me.ShowInTaskbar = False
        Me.Text = "Préférences"
        CType(Me.aListColors1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aListColors5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aListColors3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aListColors4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aListColors2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aListColors6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colors7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colors3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colors5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colors4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colors2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colors6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colors1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listColors7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listColors6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listColors5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listColors4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listColors3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listColors2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.listColors1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPref.ResumeLayout(False)
        Me._TabPref_TabPage0.ResumeLayout(False)
        Me._TabPref_TabPage0.PerformLayout()
        Me.PagesUsers.ResumeLayout(False)
        Me.PageUserAgenda.ResumeLayout(False)
        Me.PageUserAgenda.PerformLayout()
        Me.PageUserAutres.ResumeLayout(False)
        Me.PageUserAutres.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupAutresStartup.ResumeLayout(False)
        Me.GroupAutresStartup.PerformLayout()
        Me.PageUserCompteclient.ResumeLayout(False)
        Me.PageUserCompteclient.PerformLayout()
        Me.PageMessagerie.ResumeLayout(False)
        Me.PageMessagerie.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.AlertExpiries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PageUserRapport.ResumeLayout(False)
        Me.PageUserRapport.PerformLayout()
        Me.PageUserRendezvous.ResumeLayout(False)
        Me.PageUserRendezvous.PerformLayout()
        Me.PageUserSons.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me._TabPref_TabPage1.ResumeLayout(False)
        Me.PagesCliniques.ResumeLayout(False)
        Me.PageCliniqueAffichage.ResumeLayout(False)
        Me.PageCliniqueAffichage.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.colorSpecialDates, System.ComponentModel.ISupportInitialize).EndInit()
        Me._Frames_7.ResumeLayout(False)
        Me._Frames_7.PerformLayout()
        CType(Me.colorObjActivatedBySoftware, System.ComponentModel.ISupportInitialize).EndInit()
        Me._Frames_5.ResumeLayout(False)
        Me._Frames_5.PerformLayout()
        CType(Me.colorHoraireClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colorPresencePayee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colorHoraireOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colorBloquee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colorTitreFactureSouffrance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.FrameConfirmation.ResumeLayout(False)
        Me.FrameConfirmation.PerformLayout()
        Me._Frames_4.ResumeLayout(False)
        Me._Frames_4.PerformLayout()
        Me.groupBox9.ResumeLayout(False)
        Me.groupBox9.PerformLayout()
        CType(Me.colorCommSent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colorCommReceived, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameColorsQL.ResumeLayout(False)
        Me.frameColorsQL.PerformLayout()
        CType(Me.colorWithoutRV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colorWithRV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PageCliniqueAutres.ResumeLayout(False)
        Me.PageCliniqueAutres.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.PageCliniqueBD.ResumeLayout(False)
        Me.PageCliniqueBD.PerformLayout()
        Me.groupAutoSaveRemoteTask.ResumeLayout(False)
        Me.PageCliniqueCompteclient.ResumeLayout(False)
        Me.PageCliniqueCompteclient.PerformLayout()
        Me._Frames_0.ResumeLayout(False)
        Me.PageCliniqueFacturation.ResumeLayout(False)
        Me.PageCliniqueFacturation.PerformLayout()
        Me.frameComptabilite.ResumeLayout(False)
        Me.frameComptabilite.PerformLayout()
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox13.PerformLayout()
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.FrameTriage.ResumeLayout(False)
        Me.FrameTriage.PerformLayout()
        Me.PageCliniquePrinting.ResumeLayout(False)
        Me.PageCliniquePrinting.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.PageCliniqueKP.ResumeLayout(False)
        Me.PageCliniqueKP.PerformLayout()
        Me._Frames_3.ResumeLayout(False)
        Me.PageCliniqueRendezvous.ResumeLayout(False)
        Me.PageCliniqueRendezvous.PerformLayout()
        Me.GroupTriRV.ResumeLayout(False)
        Me.GroupTriRV.PerformLayout()
        Me.PageCliniqueUtilisateur.ResumeLayout(False)
        Me.PageCliniqueUtilisateur.PerformLayout()
        Me.FrameHidden.ResumeLayout(False)
        Me.FrameHidden.PerformLayout()
        Me.SelectSonMenu.ResumeLayout(False)
        Me.SelectRemoteTaskDBPath.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private selectImage As Image = DrawingManager.getInstance.getImage("selection16.gif")
    Private boxSpacing As Byte = 8
    Private formModified As Boolean = False
    Private modifiedClinique As Boolean = False
    Private oldSorting As Integer = 0
    Private _AllowModification As Boolean = True
    Private cliniquePref As Preferences = PreferencesManager.getGeneralPreferences()
    Private userPref As Preferences = PreferencesManager.getUserPreferences()

#Region "Propriétés"
    Public Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal Value As Boolean)
            _AllowModification = Value
            lockItems(Not Value, _TabPref_TabPage1)
        End Set
    End Property
#End Region

    'this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);

    Private tabColors As Generic.Dictionary(Of TabPage, Color) = New Generic.Dictionary(Of TabPage, Color)()
    Private Sub setTabHeader(ByVal page As TabPage, ByVal color As Color)
        tabColors([page]) = color
        tabPref.Invalidate()
        PagesCliniques.Invalidate()
        PagesUsers.Invalidate()
    End Sub

    ''' <summary>
    ''' 
    ''' Ref : http://stackoverflow.com/questions/5338587/set-tabpage-header-color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tabs_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles tabPref.DrawItem, PagesCliniques.DrawItem, PagesUsers.DrawItem
        Using br As Brush = New SolidBrush(tabColors(CType(sender, TabControl).TabPages(e.Index)))
            e.Graphics.FillRectangle(br, e.Bounds)
            Dim sz As SizeF = e.Graphics.MeasureString(CType(sender, TabControl).TabPages(e.Index).Text, e.Font)
            e.Graphics.DrawString(CType(sender, TabControl).TabPages(e.Index).Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1)

            Dim rect As Rectangle = e.Bounds
            rect.Offset(0, 1)
            rect.Inflate(0, -1)
            e.Graphics.DrawRectangle(Pens.DarkGray, rect)
            e.DrawFocusRectangle()
        End Using
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean, ByVal myObject As Control)
        Dim i As Short
        With myObject
            For i = 0 To .Controls.Count - 1
                If .Controls(i).Name.ToUpper.IndexOf("GROUP") <> -1 Or .Controls(i).Name.ToUpper.IndexOf("FRAME") <> -1 Then
                    lockItems(trueFalse, .Controls(i))
                Else
                    If .Controls(i).Tag <> "" Then .Controls(i).Enabled = Not trueFalse
                End If
            Next i
        End With
    End Sub


    Private Sub preferences_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        createPrefsFile()



        'Load Therapeute
        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(False, True)
        openingAgendas.Items.AddRange(users.ToArray)
        defaultTRP.Items.AddRange(users.ToArray)

        'Load FolderTexteTypes
        Dim ftt() As String = DBLinker.getInstance.readOneDBField("SELECT TexteTitle FROM FolderTexteTypes GROUP BY TexteTitle ORDER BY TexteTitle")
        dossierTexteAutoSel.Items.Add("* Dernier accédé *")
        dossierTexteAutoSel.Items.AddRange(ftt)

        'Default Selection
        startingPeriode.SelectedIndex = 4
        taxeArrondissement.SelectedIndex = 1
        firstDay.SelectedIndex = 2
        findPlacesUpTo.SelectedIndex = 1
        triPaiements.SelectedIndex = 0
        triFactures.SelectedIndex = 1
        triRVCompte.SelectedIndex = 1
        triRVFuturs.SelectedIndex = 0
        dblClickOnFutureRV.SelectedIndex = 0
        importOriginFileAction.SelectedIndex = 0
        clientTabAutoSelect.SelectedIndex = 0
        dossierTabAutoSelect.SelectedIndex = 0
        dossierTexteAutoSel.SelectedIndex = 0
        openFuturRV.SelectedIndex = 0
        openInstantMSG.SelectedIndex = 0
        openPunch.SelectedIndex = 0
        AddingReferent.Image = addingService.Image
        renamingReferent.Image = renamingService.Image
        removingReferent.Image = removingService.Image
        punchModeArrival.SelectedIndex = 0
        punchModeDeparture.SelectedIndex = 0
        actionOnNoRV.SelectedIndex = 0
        typeAffListeFenetres.SelectedIndex = 0
        sortingInstantMsgAndNotes.SelectedIndex = 0
        taxesInteraction.SelectedIndex = 1

        'Loading
        loadClinique()

        If Not ConnectionsManager.currentUser = 0 Then loadUser()
        Me.save.Enabled = True

        'Adjusting Preferences
        lastUserType.Width = lastUserType.Width + boxSpacing
        lastUserType.TextAlign = System.Drawing.ContentAlignment.TopCenter

        'Disabling Effects checks
        listGras.Enabled = False
        listItalique.Enabled = False
        listBarre.Enabled = False
        listSouligne.Enabled = False
        rvEvalGras.Enabled = False
        rvEvalItalique.Enabled = False
        rvEvalBarre.Enabled = False
        rvEvalSouligne.Enabled = False

        formModified = False
    End Sub

    Private Sub colorBoxes_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles colors1.DoubleClick, colors2.DoubleClick, colors3.DoubleClick, colors4.DoubleClick, colors5.DoubleClick, colors6.DoubleClick, colors7.DoubleClick, aListColors1.DoubleClick, aListColors2.DoubleClick, aListColors3.DoubleClick, aListColors4.DoubleClick, aListColors5.DoubleClick, aListColors6.DoubleClick, listColors1.DoubleClick, listColors2.DoubleClick, listColors3.DoubleClick, listColors4.DoubleClick, listColors5.DoubleClick, listColors6.DoubleClick, listColors7.DoubleClick, colorWithRV.DoubleClick, colorWithoutRV.DoubleClick, colorPresencePayee.DoubleClick, colorBloquee.DoubleClick, colorTitreFactureSouffrance.DoubleClick, colorObjActivatedBySoftware.DoubleClick, colorSpecialDates.DoubleClick, colorHoraireOpen.DoubleClick, colorHoraireClose.DoubleClick, colorCommSent.DoubleClick, colorCommReceived.DoubleClick
        CDialog.Color = sender.BackColor
        CDialog.ShowDialog()
        sender.BackColor = CDialog.Color
    End Sub

    Private Function checkAgendaColorCode() As Boolean
        For Each curControl As Control In _Frames_5.Controls
            If TypeOf curControl Is PictureBox Then
                For Each verifControl As Control In _Frames_5.Controls
                    If TypeOf verifControl Is PictureBox AndAlso verifControl.Equals(curControl) = False Then
                        If curControl.BackColor = verifControl.BackColor Then Return False
                    End If
                Next
            End If
        Next

        Return True
    End Function

    Private Function saving() As Boolean
        Dim canSaveClinique As Boolean = tabPref.TabPages("_TabPref_TabPage1") IsNot Nothing
        canSaveClinique = canSaveClinique AndAlso (currentDroitAcces Is Nothing OrElse currentDroitAcces(33))

        If canSaveClinique Then
            'Conditions Clinique
            If Services.Items.Count = 0 Then
                tabPref.SelectedTab = tabPref.TabPages("_TabPref_TabPage1")
                PagesCliniques.SelectedTab = PagesCliniques.TabPages("PageCliniqueUtilisateur")
                MessageBox.Show("Veuillez entrer au moins un service offert", "Préférences : Information manquante", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Function
            End If
            If MethodesPaiment.Items.Count = 0 Then
                tabPref.SelectedTab = tabPref.TabPages("_TabPref_TabPage1")
                PagesCliniques.SelectedTab = PagesCliniques.TabPages("PageCliniqueFacturation")
                MessageBox.Show("Veuillez entrer au moins une méthode de paiement", "Préférences : Information manquante", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Function
            End If
            If checkAgendaColorCode() = False Then
                tabPref.SelectedTab = tabPref.TabPages("_TabPref_TabPage1")
                PagesCliniques.SelectedTab = PagesCliniques.TabPages("PageCliniqueAffichage")
                MessageBox.Show("Les codes de couleur des plages horaires doivent tous être différents", "Préférences : Codes de couleur identiques", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Function
            End If

            If allowModification Then saveClinique()
        End If

        If Not ConnectionsManager.currentUser = 0 Then
            If mailFolderForDelMsg.Text = mailFolderForSentMsg.Text AndAlso mailFolderForSentMsg.Text <> String.Empty Then
                tabPref.SelectedTab = tabPref.TabPages("_TabPref_TabPage0")
                PagesUsers.SelectedTab = PagesUsers.TabPages("PageMessagerie")
                MessageBox.Show("Les dossiers pour les messages envoyés et pour ceux supprimés doivent être différents", "Préférences : Dossiers identiques", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                mailFolderForSentMsg.Focus()
                Exit Function
            End If

            saveUser()
        End If

        If myMainWin IsNot Nothing Then myMainWin.StatusText = "Préférences enregistrées"
        formModified = False

        Return True
    End Function

    Private Sub save_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles save.Click
        If saving() Then Me.Close()
    End Sub

    Public Sub saveClinique()
        '
        'Clinique
        '
        nbClinique = 0
        cliniquePref.clear()
        saving(_TabPref_TabPage1, cliniquePref)
        cliniquePref.saveData()
    End Sub

    Public Sub saveUser()
        If tabPref.TabPages(0) IsNot _TabPref_TabPage0 Then Exit Sub

        '
        'User
        '
        userPref.clear()
        saving(_TabPref_TabPage0, userPref)
        userPref.saveData()

        'Resort Alerts if necessary
        If oldSorting <> sortingInstantMsgAndNotes.SelectedIndex AndAlso myMainWin IsNot Nothing Then myMainWin.AlertMessages.loadAlerts()
    End Sub

    Private lastChosenRemoteTaskDBPath As Label

    Private Sub chooseRemoteTaskAutoSavePath(ByVal sender As Object, ByVal e As EventArgs)
        'Can't select when DBFolders haven't been loaded
        If tabPref.TabPages.Count = 1 Then
            MessageBox.Show("Impossible de sélectionner en ce moment. Veuillez retourner dans la section Préférences lorsque vous êtes connecté à Clinica", "Fonction inaccessible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        Dim lblRemoteTaskDBPath As Label = sender.Tag

        NePasEnregistrerToolStripMenuItem.Checked = False
        VersLaBanqueDeDonnéesToolStripMenuItem.Checked = False

        If lblRemoteTaskDBPath.Text = NePasEnregistrerToolStripMenuItem.Text Then
            NePasEnregistrerToolStripMenuItem.Checked = True
        Else
            VersLaBanqueDeDonnéesToolStripMenuItem.Checked = True
        End If

        lastChosenRemoteTaskDBPath = lblRemoteTaskDBPath

        Me.SelectRemoteTaskDBPath.Show(sender, New Point(0, 0), ToolStripDropDownDirection.Right)
    End Sub

    Private Sub NePasEnregistrerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NePasEnregistrerToolStripMenuItem.Click
        lastChosenRemoteTaskDBPath.Text = NePasEnregistrerToolStripMenuItem.Text
        lastChosenRemoteTaskDBPath.Tag = "1"
        formModified = True
    End Sub

    Private Sub VersLaBanqueDeDonnéesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles VersLaBanqueDeDonnéesToolStripMenuItem.Click
        Dim mySelectDBCat As New SelectDBCat
        Dim myPath As String = mySelectDBCat("", False)
        If myPath = "" Then Exit Sub

        formModified = True
        lastChosenRemoteTaskDBPath.Text = myPath
        lastChosenRemoteTaskDBPath.Tag = "2"
    End Sub

    Private Sub createRemoteTasksControls()
        Dim margin As Integer = 5
        Dim tasksTypes As Generic.List(Of PluginTaskBase) = PluginTasksManager.getInstance.getItemables()
        Dim y As Integer = margin
        panelAutoSaveRemoteTask.Controls.Clear()
        For Each curTaskType As PluginTaskBase In tasksTypes
            Dim taskTypeLabel As New Label()
            taskTypeLabel.Text = curTaskType.name & " :"
            taskTypeLabel.Left = margin
            taskTypeLabel.Top = y
            taskTypeLabel.AutoSize = True
            panelAutoSaveRemoteTask.Controls.Add(taskTypeLabel)

            y += taskTypeLabel.Height

            Dim taskTypeDBPathButton As New Button()
            AddHandler taskTypeDBPathButton.Click, AddressOf chooseRemoteTaskAutoSavePath
            taskTypeDBPathButton.FlatStyle = FlatStyle.Popup
            taskTypeDBPathButton.Text = ""
            taskTypeDBPathButton.Image = selectImage
            taskTypeDBPathButton.Size = New Size(24, 24)
            taskTypeDBPathButton.Top = y
            taskTypeDBPathButton.Left = margin
            panelAutoSaveRemoteTask.Controls.Add(taskTypeDBPathButton)

            Dim taskTypeDBPathLabel As New Label()
            taskTypeDBPathButton.Tag = taskTypeDBPathLabel
            taskTypeDBPathLabel.Tag = "1"
            taskTypeDBPathLabel.Name = "RemoteTask-" & curTaskType.getIdentifier() & "-AutoSaveResultDBPath"
            taskTypeDBPathLabel.Left = taskTypeDBPathButton.Left + taskTypeDBPathButton.Width + margin
            taskTypeDBPathLabel.Text = NePasEnregistrerToolStripMenuItem.Text
            taskTypeDBPathLabel.AutoSize = True
            taskTypeDBPathLabel.Top = y + (taskTypeDBPathButton.Height - taskTypeDBPathLabel.Height) / 2
            panelAutoSaveRemoteTask.Controls.Add(taskTypeDBPathLabel)

            y += taskTypeDBPathButton.Height + margin
        Next
    End Sub

    Public Sub loadClinique()
        If PreferencesManager.getGeneralPreferences Is Nothing OrElse PreferencesManager.getGeneralPreferences.count = 0 Then Exit Sub

        loading(_TabPref_TabPage1, cliniquePref)

        Dim myStyle As FontStyle = FontStyle.Regular
        If listGras.Checked Then myStyle += FontStyle.Bold
        If listItalique.Checked Then myStyle += FontStyle.Italic
        If listBarre.Checked Then myStyle += FontStyle.Strikeout
        If listSouligne.Checked Then myStyle += FontStyle.Underline
        Dim fontSize As Single = Single.Parse(listFontSize.Text.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
        If fontSize = 0 Then fontSize = 8
        FDialog.Font = New Font(FDialog.Font.Name, fontSize, myStyle)
        listFont.Font = FDialog.Font

        myStyle = FontStyle.Regular
        If rvEvalGras.Checked Then myStyle += FontStyle.Bold
        If rvEvalItalique.Checked Then myStyle += FontStyle.Italic
        If rvEvalSouligne.Checked Then myStyle += FontStyle.Strikeout
        If rvEvalBarre.Checked Then myStyle += FontStyle.Underline
        fontSize = listFontSize.Text
        If fontSize = 0 Then
            fontSize = 8
            listFontSize.Text = 8
        End If
        rvEvalFont.Font = New Font(rvEvalFont.Font.Name, fontSize, myStyle)

        myStyle = FontStyle.Regular
        If newAlertGras.Checked Then myStyle += FontStyle.Bold
        If newAlertItalic.Checked Then myStyle += FontStyle.Italic
        If newAlertStrike.Checked Then myStyle += FontStyle.Strikeout
        If newAlertUnder.Checked Then myStyle += FontStyle.Underline
        fontSize = 8
        If newAlertFontSize.Text > 0 Then
            fontSize = newAlertFontSize.Text
        Else
            newAlertFontSize.Text = fontSize
        End If
        newRvFont.Font = New Font(newRvFont.Font.Name, fontSize, myStyle)
    End Sub

    Public Sub loadUser()
        If PreferencesManager.getUserPreferences() Is Nothing OrElse PreferencesManager.getUserPreferences().count = 0 Then Exit Sub

        nbPrefU.Text = loading(_TabPref_TabPage0, userPref)

        If Me.openFuturRV.SelectedIndex < 0 Then Me.openFuturRV.SelectedIndex = 0
        If Me.openInstantMSG.SelectedIndex < 0 Then Me.openInstantMSG.SelectedIndex = 0
        If Me.openPunch.SelectedIndex < 0 Then Me.openPunch.SelectedIndex = 0

        oldSorting = sortingInstantMsgAndNotes.SelectedIndex
    End Sub

    Private nbClinique As Integer = 0

    Public Function saving(ByVal withObj As Control, ByVal curPref As Preferences) As Array
        Dim i, j, n As Short
        Dim myString As String
        n = 0

        With withObj
            For i = 0 To .Controls.Count - 1
                Try
                    If TypeOf (.Controls(i)) Is TabControl Then
                        For j = 0 To CType(.Controls(i), TabControl).TabPages.Count - 1
                            saving(CType(.Controls(i), TabControl).TabPages(j), curPref)
                        Next j

                        Continue For
                    End If
                    If .Controls(i).Controls.Count <> 0 Then saving(withObj.Controls(i), curPref)

                    If .Controls(i).Tag IsNot Nothing AndAlso .Controls(i).Tag.ToString <> "" AndAlso Integer.TryParse(.Controls(i).Tag.ToString, 0) Then
                        If .Controls(i).Tag.ToString >= 0 Then
                            n += 1
                            Select Case True
                                Case TypeOf .Controls(i) Is CheckBox
                                    curPref.setProperty(.Controls(i).Name, CType(.Controls(i), CheckBox).Checked)

                                Case TypeOf .Controls(i) Is TextBox
                                    curPref.setProperty(.Controls(i).Name, .Controls(i).Text)

                                Case TypeOf .Controls(i) Is ManagedText
                                    curPref.setProperty(.Controls(i).Name, .Controls(i).Text)

                                Case TypeOf .Controls(i) Is Label
                                    If .Controls(i).Name = "LastUserType" And affLastUserType.Checked = False Then
                                        curPref.setProperty(lastUserType.Name, "* Défaut *")
                                    Else
                                        curPref.setProperty(.Controls(i).Name, CType(.Controls(i), Label).Text)
                                    End If

                                Case TypeOf .Controls(i) Is PictureBox
                                    curPref.setProperty(.Controls(i).Name, System.Drawing.ColorTranslator.ToOle(.Controls(i).BackColor))

                                Case TypeOf .Controls(i) Is Button
                                    curPref.setProperty(.Controls(i).Name, CType(.Controls(i), Button).Text)

                                Case TypeOf .Controls(i) Is HScrollBar
                                    curPref.setProperty(.Controls(i).Name, CType(.Controls(i), HScrollBar).Value)

                                Case TypeOf .Controls(i) Is CheckedListBox
                                    With CType(.Controls(i), CheckedListBox)
                                        myString = ""
                                        For Each CurCheckedItem As IListable In .CheckedItems
                                            myString &= vbTab & CurCheckedItem.getValue
                                        Next
                                        If Not myString = "" Then myString = myString.Substring(1)
                                        curPref.setProperty(.Name, myString)
                                    End With

                                Case TypeOf .Controls(i) Is ComboBox
                                    curPref.setProperty(.Controls(i).Name, CType(.Controls(i), ComboBox).Text)

                                Case TypeOf .Controls(i) Is ListBox
                                    Dim myArray() As String
                                    With CType(.Controls(i), ListBox)
                                        ReDim myArray(.Items.Count - 1)
                                        .Items.CopyTo(myArray, 0)
                                    End With
                                    curPref.setProperty(.Controls(i).Name, Join(myArray, vbTab))

                                Case TypeOf .Controls(i) Is WebTextControl
                                    curPref.setProperty(.Controls(i).Name, CType(.Controls(i), WebTextControl).getHTML())

                            End Select
                        End If
                    End If
                Catch ex As NullReferenceException
                    'Meanless null exception
                End Try
            Next i
        End With

        nbClinique += n
    End Function

    Public Function loading(ByVal withObj As Control, ByVal curPref As Preferences) As Short
        'TODO  : REMOVED On Error Resume Next
        Dim i, j, n As Short
        n = 0
        With withObj
            .Font = New Font(.Font, FontStyle.Regular)
            .ForeColor = Color.Black

            For i = 0 To .Controls.Count - 1
                Try
                    If TypeOf (.Controls(i)) Is TabControl Then
                        For j = 0 To CType(.Controls(i), TabControl).TabPages.Count - 1
                            n += loading(CType(.Controls(i), TabControl).TabPages(j), curPref)
                        Next j

                        Continue For
                    End If
                    .Font = New Font(.Font, FontStyle.Regular)
                    .ForeColor = Color.Black
                    If .Controls(i).Controls.Count <> 0 Then n += loading(withObj.Controls(i), curPref)

                    If .Controls(i).Tag IsNot Nothing AndAlso .Controls(i).Tag.ToString <> "" AndAlso Integer.TryParse(.Controls(i).Tag.ToString, 0) AndAlso .Controls(i).Tag.ToString <> -1 Then
                        n += 1

                        If Not curPref.existsProperty(.Controls(i).Name) Then
                            'This part of the IF is to enhance the new pref visually

                            Select Case True
                                Case TypeOf .Controls(i) Is PictureBox
                                    CType(.Controls(i), PictureBox).BorderStyle = BorderStyle.Fixed3D
                                Case Else
                            End Select
                            .Controls(i).ForeColor = Color.OrangeRed

                            'TODO : Should only bold and set orange the appropriate tabs, but it does it for all
                            Dim tab As Control = .Controls(i).Parent
                            While Not TypeOf tab Is TabPage
                                tab = tab.Parent
                            End While
                            'tab.Parent.Font = New Font(.Font, FontStyle.Bold)
                            If tab.Text.StartsWith("*") = False Then tab.Text = "*" & tab.Text & "*"
                            setTabHeader(tab, Color.OrangeRed)
                            If tab.Parent.Parent.Text.StartsWith("*") = False Then tab.Parent.Parent.Text = "*" & tab.Parent.Parent.Text & "*"
                            setTabHeader(tab.Parent.Parent, Color.OrangeRed)
                            'tab.Parent.Parent.Parent.ForeColor = Color.OrangeRed
                            'tab.Parent.Parent.Parent.Font = New Font(.Font, FontStyle.Bold)
                        Else
                            Select Case True
                                Case TypeOf .Controls(i) Is CheckBox
                                    If Not curPref.getProperty(.Controls(i).Name).ToString = String.Empty Then CType(.Controls(i), CheckBox).Checked = curPref.getProperty(.Controls(i).Name)
                                    AddHandler CType(.Controls(i), CheckBox).CheckedChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is ManagedText
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then .Controls(i).Text = curPref.getProperty(.Controls(i).Name)
                                    AddHandler CType(.Controls(i), ManagedText).TextChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is TextBox
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then CType(.Controls(i), TextBox).Text = curPref.getProperty(.Controls(i).Name)
                                    AddHandler CType(.Controls(i), TextBox).TextChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is Button
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then .Controls(i).Text = curPref.getProperty(.Controls(i).Name)
                                    AddHandler CType(.Controls(i), Button).TextChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is Label
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then CType(.Controls(i), Label).Text = curPref.getProperty(.Controls(i).Name)
                                    AddHandler CType(.Controls(i), Label).TextChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is PictureBox
                                    If Not curPref.getProperty(.Controls(i).Name).ToString = String.Empty Then .Controls(i).BackColor = System.Drawing.ColorTranslator.FromOle(curPref.getProperty(.Controls(i).Name))
                                    AddHandler CType(.Controls(i), PictureBox).BackColorChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is HScrollBar
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then CType(.Controls(i), HScrollBar).Value = curPref.getProperty(.Controls(i).Name)
                                    AddHandler CType(.Controls(i), HScrollBar).ValueChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is CheckedListBox
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then
                                        With CType(.Controls(i), CheckedListBox)
                                            Dim curCheckedItem As IListable
                                            For j = 0 To .Items.Count - 1
                                                curCheckedItem = .Items(j)
                                                If CStr(curPref.getProperty(.Name)).IndexOf(vbTab & curCheckedItem.getValue.ToString & vbTab) <> -1 Or CStr(curPref.getProperty(.Name)).StartsWith(curCheckedItem.getValue.ToString & vbTab) Or CStr(curPref.getProperty(.Name)).EndsWith(vbTab & curCheckedItem.getValue.ToString) Or curPref.getProperty(.Name).ToString = curCheckedItem.getValue.ToString Then .SetItemChecked(.Items.IndexOf(curCheckedItem), True)
                                            Next j
                                            AddHandler .ItemCheck, AddressOf all_ItemCheck
                                        End With
                                    End If

                                Case TypeOf .Controls(i) Is ComboBox
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then CType(.Controls(i), ComboBox).Text = curPref.getProperty(.Controls(i).Name).ToString
                                    AddHandler CType(.Controls(i), ComboBox).SelectedIndexChanged, AddressOf all_Changed

                                Case TypeOf .Controls(i) Is ListBox
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then CType(.Controls(i), ListBox).Items.AddRange(loadList(CStr(curPref.getProperty(.Controls(i).Name))).ToArray())

                                Case TypeOf .Controls(i) Is WebTextControl
                                    If Not curPref.getProperty(.Controls(i).Name) = String.Empty Then CType(.Controls(i), WebTextControl).setHtml(curPref.getProperty(.Controls(i).Name))

                            End Select
                        End If
                    End If
                Catch ex As NullReferenceException
                    'Meanless null exception
                End Try
            Next i
        End With

        Return n
    End Function

    Private Function loadList(ByVal data As String) As Generic.List(Of String)
        Dim curList As New Generic.List(Of String)(data.Split(New Char() {vbTab}))
        Dim returnedList As New Generic.List(Of String)()
        'Clean doubles
        For Each curData As String In curList
            If Not returnedList.Contains(curData) Then returnedList.Add(curData)
        Next

        Return returnedList
    End Function

    Private Sub listFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listFont.Click
        Dim mySize As Single
        If listFontSize.Text = "" Then
            mySize = "8,25"
        Else
            mySize = listFontSize.Text
        End If

        With FDialog
            .Font = New Font(CType(sender, Button).Text, mySize, CType(sender, Button).Font.Style)
            .Color = listColors7.BackColor
            .ShowDialog()
            If Not .Font.Name = "" Then sender.Text = .Font.Name
            listFontSize.Text = CStr(.Font.SizeInPoints)
            sender.font = .Font
            listColors7.BackColor = .Color
            listGras.Checked = .Font.Bold
            listItalique.Checked = .Font.Italic
            listSouligne.Checked = .Font.Underline
            listBarre.Checked = .Font.Strikeout
        End With
    End Sub

    Private Sub listEffectsChecked(ByVal sender As Object, ByVal e As System.EventArgs) Handles listGras.CheckedChanged, listBarre.CheckedChanged, listSouligne.CheckedChanged, listItalique.CheckedChanged
        Dim fontS As FontStyle = 0
        If listGras.Checked = True Then fontS = fontS + FontStyle.Bold
        If listItalique.Checked = True Then fontS = fontS + FontStyle.Italic
        If listBarre.Checked = True Then fontS = fontS + FontStyle.Strikeout
        If listSouligne.Checked = True Then fontS = fontS + FontStyle.Underline

        listFont.Font = New Font(listFont.Font, fontS)
    End Sub

    Private Sub confirmedEffectsChecked(ByVal sender As Object, ByVal e As System.EventArgs) Handles rvEvalGras.CheckedChanged, rvEvalBarre.CheckedChanged, rvEvalSouligne.CheckedChanged, rvEvalItalique.CheckedChanged
        Dim fontS As FontStyle = 0
        If rvEvalGras.Checked = True Then fontS = fontS + FontStyle.Bold
        If rvEvalItalique.Checked = True Then fontS = fontS + FontStyle.Italic
        If rvEvalBarre.Checked = True Then fontS = fontS + FontStyle.Strikeout
        If rvEvalSouligne.Checked = True Then fontS = fontS + FontStyle.Underline

        rvEvalFont.Font = New Font(rvEvalFont.Font, fontS)
    End Sub

    Private Sub newAlertEffectsChecked(ByVal sender As Object, ByVal e As System.EventArgs) Handles newAlertGras.CheckedChanged, newAlertItalic.CheckedChanged, textShowedCharByCharVertically.CheckedChanged, affSpecialDatesInCalendar.CheckedChanged, affSpecialDatesInAgenda.CheckedChanged
        Dim fontS As FontStyle = 0
        If newAlertGras.Checked = True Then fontS = fontS + FontStyle.Bold
        If newAlertItalic.Checked = True Then fontS = fontS + FontStyle.Italic
        If newAlertStrike.Checked = True Then fontS = fontS + FontStyle.Strikeout
        If newAlertUnder.Checked = True Then fontS = fontS + FontStyle.Underline

        newRvFont.Font = New Font(newRvFont.Font, fontS)
    End Sub

    Private Sub all_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        formModified = True
        If CStr(sender.parent.name).ToLower.IndexOf("fond") <> -1 Or CStr(sender.parent.parent.name).ToLower.IndexOf("fond") <> -1 Then modifiedClinique = True
    End Sub

    Private Sub all_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs)
        formModified = True
        If CStr(sender.parent.name).ToLower.IndexOf("fond") <> -1 Or CStr(sender.parent.parent.name).ToLower.IndexOf("fond") <> -1 Then modifiedClinique = True
    End Sub

    Private Sub preferences_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If formModified = True Then
            If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                If saving() = False Then
                    e.Cancel = True
                    Exit Sub
                End If
            Else
                modifiedClinique = False
            End If
        End If

        If allowModification = True Then lockSecteur("preferences.lock", False)
    End Sub

    Private Sub preferences_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If formModified = True Then
            save_Click(Me, EventArgs.Empty)
        End If

        If allowModification = True Then lockSecteur("preferences.lock", False)
    End Sub

    Private Sub tabPref_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPref.SelectedIndexChanged
        If tabPref.SelectedIndex = 1 Then
            'Droit & Accès
            If currentDroitAcces Is Nothing OrElse currentDroitAcces.Length = 0 Then Exit Sub

            If currentDroitAcces(33) = False Then
                'Message & Exit
                tabPref.SelectedIndex = 0
                MessageBox.Show("Vous n'avez pas le droit de gérer les préférences générales." & vbCrLf & "Merci!", "Droit & Accès")
                Exit Sub
            End If

            If lockSecteur("preferences.lock", True, "Préférences cliniques") = False Then
                tabPref.SelectedIndex = 0
                allowModification = False
            Else
                allowModification = True
            End If
        Else
            If allowModification = True Then lockSecteur("preferences.lock", False)
        End If
    End Sub

#Region "Services Actions"
    Private Sub addingService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addingService.Click
        Dim myService As String = ""
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ":"
        myInputBoxPlus.maxLength = 100
        myService = myInputBoxPlus.Prompt("Veuillez entrer le nom du nouveau service", "Ajout d'un service", myService)
        If myService = "" Then Exit Sub
        If Services.FindStringExact(myService) >= 0 Then MessageBox.Show("Veuillez entrer un service différent de ceux existants", "Conflit") : GoTo GoBack
        If myService = "Travailleur autonome" Or myService = "Prêt" Or myService = "Vente" Or myService = "Facture unifiée" Then MessageBox.Show("Veuillez entrer un service différent, car il s'agit de type de facture interne", "Conflit") : GoTo GoBack

        Services.Items.Add(myService)
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub services_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Services.SelectedIndexChanged
        If Services.SelectedIndex <> -1 Then
            removingService.Enabled = True
            renamingService.Enabled = True
        Else
            removingService.Enabled = False
            renamingService.Enabled = False
        End If
    End Sub

    Private Sub removingService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removingService.Click
        Services.Items.RemoveAt(Services.SelectedIndex)
        removingService.Enabled = False
        renamingService.Enabled = False
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub renamingService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles renamingService.Click
        Dim newName As String
        newName = Services.GetItemText(Services.SelectedItem)
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        newName = myInputBoxPlus.Prompt("Veuillez entrer le nouveau nom du service", "Renommer un service", newName)
        If newName = "" Then Exit Sub
        If Services.FindStringExact(newName) >= 0 Then MessageBox.Show("Veuillez entrer un service différent de ceux existants", "Conflit") : GoTo GoBack
        If newName = "Travailleur autonome" Or newName = "Prêt" Or newName = "Vente" Or newName = "Facture unifiée" Then MessageBox.Show("Veuillez entrer un service différent, car il s'agit de type de facture interne", "Conflit") : GoTo GoBack

        Services.Items(Services.SelectedIndex) = newName
    End Sub
#End Region

#Region "AutresTypes Actions"
    Private Sub addingAutresTypes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addingAutresTypes.Click
        Dim myAutresTypes As String = ""
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.maxLength = 100
        myInputBoxPlus.refusedChars = ":"
        myAutresTypes = myInputBoxPlus.Prompt("Veuillez entrer le nom de la nouvelle type de facturation", "Ajout d'un type de facturation", myAutresTypes)
        If myAutresTypes = "" Then Exit Sub
        If AutresTypesBills.FindStringExact(myAutresTypes) >= 0 Then MessageBox.Show("Veuillez entrer un type différent de ceux existants", "Conflit") : GoTo GoBack
        If Services.FindStringExact(myAutresTypes) >= 0 Then MessageBox.Show("Veuillez entrer un type différent des services existants", "Conflit") : GoTo GoBack
        If myAutresTypes = "Travailleur autonome" Or myAutresTypes = "Prêt" Or myAutresTypes = "Vente" Or myAutresTypes = "Facture unifiée" Then MessageBox.Show("Veuillez entrer un type différent, car il s'agit d'un type interne au logiciel", "Conflit") : GoTo GoBack

        AutresTypesBills.Items.Add(myAutresTypes)
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub autresTypesBills_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutresTypesBills.SelectedIndexChanged
        If AutresTypesBills.SelectedIndex <> -1 Then
            removingAutresTypes.Enabled = True
            renamingAutresTypes.Enabled = True
        Else
            removingAutresTypes.Enabled = False
            renamingAutresTypes.Enabled = False
        End If
    End Sub

    Private Sub removingAutresTypes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removingAutresTypes.Click
        AutresTypesBills.Items.RemoveAt(AutresTypesBills.SelectedIndex)
        removingAutresTypes.Enabled = False
        renamingAutresTypes.Enabled = False
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub renamingAutresTypes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles renamingAutresTypes.Click
        Dim newName As String
        newName = AutresTypesBills.GetItemText(AutresTypesBills.SelectedItem)
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        newName = myInputBoxPlus.Prompt("Veuillez entrer le nouveau nom du type de facturation", "Renommer un type", newName)
        If newName = "" Then Exit Sub
        If AutresTypesBills.FindStringExact(newName) >= 0 Then MessageBox.Show("Veuillez entrer un type différent de ceux existants", "Conflit") : GoTo GoBack
        If Services.FindStringExact(newName) >= 0 Then MessageBox.Show("Veuillez entrer un type différent des services existants", "Conflit") : GoTo GoBack
        If newName = "Travailleur autonome" Or newName = "Prêt" Or newName = "Vente" Or newName = "Facture unifiée" Then MessageBox.Show("Veuillez entrer un type différent, car il s'agit d'un type interne au logiciel", "Conflit") : GoTo GoBack

        AutresTypesBills.Items(AutresTypesBills.SelectedIndex) = newName
    End Sub
#End Region

#Region "Methodes Paiment Actions"
    Private Sub methodesPaiment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MethodesPaiment.SelectedIndexChanged
        If MethodesPaiment.SelectedIndex <> -1 Then
            removingMP.Enabled = True
            renamingMP.Enabled = True
        Else
            removingMP.Enabled = False
            renamingMP.Enabled = False
        End If
    End Sub

    Private Sub addingMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addingMP.Click
        Dim myMP As String = ""
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ":"
        myMP = myInputBoxPlus.Prompt("Veuillez entrer le nom de la nouvelle méthode de paiement", "Ajout d'une méthode de paiement", myMP)
        If myMP = "" Then Exit Sub
        If MethodesPaiment.FindStringExact(myMP) >= 0 Then MessageBox.Show("Veuillez entrer une méthode de paiement différente de celles existantes", "Conflit") : GoTo GoBack

        MethodesPaiment.Items.Add(myMP)
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub removingMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removingMP.Click
        MethodesPaiment.Items.RemoveAt(MethodesPaiment.SelectedIndex)
        removingMP.Enabled = False
        renamingMP.Enabled = False
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub renamingMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles renamingMP.Click
        Dim newName As String
        newName = MethodesPaiment.GetItemText(MethodesPaiment.SelectedItem)
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        newName = myInputBoxPlus.Prompt("Veuillez entrer le nouveau nom de la méthode de paiement", "Renommer une méthode de paiement", newName)
        If newName = "" Then Exit Sub
        If MethodesPaiment.FindStringExact(newName) >= 0 Then MessageBox.Show("Veuillez entrer une méthode de paiement différente de celles existantes", "Conflit") : GoTo GoBack

        MethodesPaiment.Items(MethodesPaiment.SelectedIndex) = newName
    End Sub
#End Region

#Region "Référents prédéterminés Actions"
    Private Sub addingReferent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddingReferent.Click
        Dim myReferent As String = ""
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.maxLength = 100
        myReferent = myInputBoxPlus.Prompt("Veuillez entrer le nom du nouveau référent", "Ajout d'un référent", myReferent)
        If myReferent = "" Then Exit Sub
        If PreReferents.FindStringExact(myReferent) >= 0 Then MessageBox.Show("Veuillez entrer un référent différent de ceux existants", "Conflit") : GoTo GoBack

        PreReferents.Items.Add(myReferent)
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub preReferents_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreReferents.SelectedIndexChanged
        If PreReferents.SelectedIndex <> -1 Then
            removingReferent.Enabled = True
            renamingReferent.Enabled = True
        Else
            removingReferent.Enabled = False
            renamingReferent.Enabled = False
        End If
    End Sub

    Private Sub removingReferent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removingReferent.Click
        PreReferents.Items.RemoveAt(PreReferents.SelectedIndex)
        removingReferent.Enabled = False
        renamingReferent.Enabled = False
        formModified = True
        modifiedClinique = True
    End Sub

    Private Sub renamingReferent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles renamingReferent.Click
        Dim newName As String
        newName = PreReferents.GetItemText(PreReferents.SelectedItem)
GoBack:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        newName = myInputBoxPlus.Prompt("Veuillez entrer le nouveau nom du référent", "Renommer un référent", newName)
        If newName = "" Then Exit Sub
        If PreReferents.FindStringExact(newName) >= 0 Then MessageBox.Show("Veuillez entrer un référent différent de ceux existants", "Conflit") : GoTo GoBack

        PreReferents.Items(PreReferents.SelectedIndex) = newName
    End Sub
#End Region

    Private Sub confirmedFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rvEvalFont.Click
        With FDialog
            .Font = New Font(CType(sender, Button).Text, 8, CType(sender, Button).Font.Style)
            .ShowColor = False
            .ShowDialog()
            .ShowColor = True
            If Not .Font.Name = "" Then sender.Text = .Font.Name
            sender.font = .Font
            rvEvalGras.Checked = .Font.Bold
            rvEvalItalique.Checked = .Font.Italic
            rvEvalSouligne.Checked = .Font.Underline
            rvEvalBarre.Checked = .Font.Strikeout
        End With
    End Sub

    Private Sub newRvFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles newRvFont.Click
        With FDialog
            .Font = New Font(CType(sender, Button).Text, CType(Me.newAlertFontSize.Text, Single), CType(sender, Button).Font.Style)
            .ShowColor = False
            .ShowDialog()
            .ShowColor = True
            If Not .Font.Name = "" Then sender.Text = .Font.Name
            sender.font = .Font
            newAlertGras.Checked = .Font.Bold
            newAlertItalic.Checked = .Font.Italic
            newAlertUnder.Checked = .Font.Underline
            newAlertStrike.Checked = .Font.Strikeout
            newAlertFontSize.Text = .Font.SizeInPoints
        End With
    End Sub

    Private lastselectSon As Button

    Private Sub selectSonAlerts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectSonAlertClient.Click, selectSonAlertMSG.Click, selectSonAlertKP.Click, selectSonAlertNote.Click, selectSonAlertQL.Click, selectSonAlertRapportGen.Click
        Dim txtSon As TextBox = GroupBox2.Controls("s" & CType(sender, Button).Name.Substring(7))
        AucunToolStripMenuItem.Checked = False
        ParDéfautToolStripMenuItem.Checked = False
        DepuisLaBanqueDeDonnéesToolStripMenuItem.Checked = False

        If txtSon.Text = "* Aucun *" Or txtSon.Text = "" Then
            AucunToolStripMenuItem.Checked = True
        ElseIf txtSon.Text = "* Par défaut *" Then
            ParDéfautToolStripMenuItem.Checked = True
        Else
            DepuisLaBanqueDeDonnéesToolStripMenuItem.Checked = True
        End If

        lastselectSon = sender

        Me.SelectSonMenu.Show(sender, New Point(0, 0), ToolStripDropDownDirection.Right)
    End Sub

    Private Sub aucunToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AucunToolStripMenuItem.Click
        Dim txtSon As TextBox = GroupBox2.Controls("s" & lastselectSon.Name.Substring(7))
        txtSon.Text = "* Aucun *"

        AucunToolStripMenuItem.Checked = True
        ParDéfautToolStripMenuItem.Checked = False
        DepuisLaBanqueDeDonnéesToolStripMenuItem.Checked = False
    End Sub

    Private Sub parDéfautToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ParDéfautToolStripMenuItem.Click
        Dim txtSon As TextBox = GroupBox2.Controls("s" & lastselectSon.Name.Substring(7))
        txtSon.Text = "* Par défaut *"

        AucunToolStripMenuItem.Checked = False
        ParDéfautToolStripMenuItem.Checked = True
        DepuisLaBanqueDeDonnéesToolStripMenuItem.Checked = False
    End Sub

    Private Sub depuisLaBanqueDeDonnéesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DepuisLaBanqueDeDonnéesToolStripMenuItem.Click
        Dim txtSon As TextBox = GroupBox2.Controls("s" & lastselectSon.Name.Substring(7))

        Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB())
        mySearchDB.from = txtSon
        mySearchDB.selectedCat = "Généraux"
        mySearchDB.selectedFileType = "Son"
        mySearchDB.useWinAsSelection = True
        mySearchDB.MdiParent = Nothing
        mySearchDB.StartPosition = FormStartPosition.CenterScreen
        mySearchDB.Visible = False
        mySearchDB.ShowDialog()

        AucunToolStripMenuItem.Checked = False
        ParDéfautToolStripMenuItem.Checked = False
        DepuisLaBanqueDeDonnéesToolStripMenuItem.Checked = True
    End Sub

    Public Sub createPrefsFile()
        Dim content As New System.Text.StringBuilder()
        _createCountPref(Me, content)
        IO.File.WriteAllText(My.Application.Info.DirectoryPath & bar(My.Application.Info.DirectoryPath) & "prefs.link", content.ToString)
    End Sub

    Private curPrefScanning As String = ""

    Private Function _createCountPref(ByVal curObj As Control, ByVal fileContent As System.Text.StringBuilder) As Integer
        Dim n As Integer = 0

        With curObj
            For i As Integer = 0 To .Controls.Count - 1
                Try
                    If TypeOf (.Controls(i)) Is TabControl Then
                        For j As Integer = 0 To CType(.Controls(i), TabControl).TabPages.Count - 1
                            If .Controls(i).Name = "TabPref" Then curPrefScanning = CType(.Controls(i), TabControl).TabPages(j).Text
                            n += _createCountPref(CType(.Controls(i), TabControl).TabPages(j), fileContent)
                        Next j

                        Continue For
                    End If
                    If .Controls(i).Controls.Count <> 0 Then n += _createCountPref(.Controls(i), fileContent)

                    If .Controls(i).Tag IsNot Nothing AndAlso .Controls(i).Tag.ToString <> "" AndAlso Integer.TryParse(.Controls(i).Tag.ToString, 0) AndAlso .Controls(i).Tag.ToString >= 0 Then
                        fileContent.Append(curPrefScanning).Append(" : ").Append(.Controls(i).Tag).Append(" : ").AppendLine(.Controls(i).Name)
                        n += 1
                    End If
                Catch ex As NullReferenceException
                    'Meanless null exception
                End Try
            Next i
        End With

        Return n
    End Function

    Public Function countUserPrefs() As Integer
        Return _createCountPref(_TabPref_TabPage0, New System.Text.StringBuilder())
    End Function

    Public Function countGeneralPrefs() As Integer
        Return _createCountPref(_TabPref_TabPage1, New System.Text.StringBuilder())
    End Function

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf preferences_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub

    Private Sub btnChooseEmailSignature_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseEmailSignature.Click
        Dim myContextMenu As ContextMenu = ModelsManager.getInstance.createModelsMenu(New Integer() {1, 6}, New EventHandler(AddressOf menumodelegen_Click), New EventHandler(AddressOf menumodeleperso_Click))
        myContextMenu.Show(btnChooseEmailSignature, New Point(btnChooseEmailSignature.Width, 0))
    End Sub


    Private Sub menumodelegen_Click(ByVal sender As Object, ByVal e As EventArgs)
        applyEmailSignatureModele(CType(sender, MenuItem), 0)
    End Sub

    Private Sub menumodeleperso_Click(ByVal sender As Object, ByVal e As EventArgs)
        applyEmailSignatureModele(CType(sender, MenuItem), ConnectionsManager.currentUser)
    End Sub

    Private Sub applyEmailSignatureModele(ByVal myMenuItem As MenuItem, ByVal noUser As Integer)
        Dim myParentMenuItem As MenuItem
        Dim cat As String

        myParentMenuItem = CType(myMenuItem.Parent, MenuItem)

        If myParentMenuItem.Text = myParentMenuItem.GetContextMenu.MenuItems(0).Text Or myParentMenuItem.Text = myParentMenuItem.GetContextMenu.MenuItems(1).Text Then
            cat = "* Tous *"
        Else
            cat = myParentMenuItem.Text
        End If

        EmailSignatureModel.Text = noUser & "\" & cat & "\" & myMenuItem.Text.Replace("'", "''")
    End Sub

    Public Sub updateGenPref()
        updatePref(0)
    End Sub

    Private Sub updatePref(ByVal tabIndexToRemove As Integer)
        Me.MdiParent = Nothing
        Me.FormBorderStyle = FormBorderStyle.None
        Me.tabPref.TabPages.RemoveAt(tabIndexToRemove)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.ShowDialog()
    End Sub

    Public Sub updateUserPref()
        updatePref(1)
    End Sub
End Class
