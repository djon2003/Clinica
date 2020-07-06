Option Strict Off
Option Explicit On
Friend Class MainWin
    Inherits System.Windows.Forms.Form
    Implements IDataConsumer(Of DataInternalUpdate)

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        initializeToolbars()

        'printingJobs
        printingJobs = New PrintingJobs()
        printingJobs.Name = "printingJobs"
        printingJobs.Location = New Point(0, 0)
        printingJobs.visible = False

        'RVMenu
        Me.RVMenu = New FutureVisites
        Me.RVMenu.BackColor = System.Drawing.SystemColors.Control
        Me.RVMenu.Location = New System.Drawing.Point(264, 32)
        Me.RVMenu.Name = "RVMenu"
        Me.RVMenu.visible = False

        'AlertMessages
        Me.AlertMessages = New Clinica.InstantMessages()
        Me.AlertMessages.visible = False

        '
        'PunchClock
        '
        Me.PunchClock = New Clinica.Punch
        Me.PunchClock.BackColor = System.Drawing.SystemColors.Control
        Me.PunchClock.Location = New System.Drawing.Point(543, 174)
        Me.PunchClock.Name = "PunchClock"
        Me.PunchClock.Size = New System.Drawing.Size(145, 105)
        Me.PunchClock.TabIndex = 24
        Me.PunchClock.visible = False

        Me.menupersonnecle.Name = "menupersonnecle"


        '
        'BlockingMouseLabel
        '
        Me.BlockingMouseLabel = New TransparentControl(Nothing)
        Me.BlockingMouseLabel.BackColor = System.Drawing.Color.Transparent
        Me.BlockingMouseLabel.Location = New System.Drawing.Point(28, 298)
        Me.BlockingMouseLabel.Name = "BlockingMouseLabel"
        Me.BlockingMouseLabel.Size = New System.Drawing.Size(39, 13)
        Me.BlockingMouseLabel.TabIndex = 23
        Me.Controls.Add(Me.BlockingMouseLabel)

        linkmenujourArray()
        linkmenusemaineArray()
        dateFormatter.firstLettersCapital = True

        InternalUpdatesManager.getInstance.addConsumer(Me)

        'Add commands handlers
        AddHandler CommandsHolder.getInstance.newAgenda.enabilityChanged, AddressOf Me.newAgendaEnability
        'addhandler commandsholder.GetInstance.NewAgenda.Click,addressof

        'Connecte le menu qui affiche les fenêtres en cours
        Me.mainWinMenu.MdiWindowListItem = FenêtresToolStripMenuItem
        Me.mainWinMenu.MdiWindowListItem.TextImageRelation = TextImageRelation.ImageBeforeText
        'Me.MainWinMenu.MdiWindowListItem.

        'Affectation des Mouse_Move de tous les objets à celui de la fenêtre MDI
        Dim i As Integer
        For i = 0 To Me.Controls.Count - 1
            AddHandler Controls(i).MouseMove, AddressOf mainWin_MouseMove
        Next i

        'MainMenu icons
        With DrawingManager.getInstance
            menuaffagenda.Image = .getImage("agenda23.gif")
            menuRechercherDB.Image = .getImage("SearchDB23.gif")
            MenuItem4.Image = DrawingManager.iconToImage(.getIcon("client16.ico"), New Size(16, 16))
            MenuItem5.Image = DrawingManager.iconToImage(.getIcon("KP16.ico"), New Size(16, 16))
            menunouveau.Image = DrawingManager.iconToImage(.getIcon("newclient16.ico"), New Size(16, 16))
            menuopen.Image = .getImage("searchClient16.gif")
            menuQueueList.Image = DrawingManager.iconToImage(.getIcon("QL24.ico"), New Size(16, 16))
            MenuNewKP.Image = DrawingManager.iconToImage(.getIcon("NewKP16.ico"), New Size(16, 16))
            menupersonnecle.Image = .getImage("searchKP16.gif")
            OuvrirToolStripMenuItem.Image = .getImage("viewBills16.jpg")
            NouvelleToolStripMenuItem.Image = .getImage("newBill16.jpg")
            menumodifmodele.Image = .getImage("modeles16.gif")
            menuchangeuser.Image = .getImage("quitchange23.gif")
            menuuser.Image = .getImage("user.gif")
            menucodedossier.Image = .getImage("codedossier.gif")
            menuAddressBook.Image = .getImage("carnet.gif")
            menuGetMail.Image = .getImage("mailbox.gif")
            menuParamMSG.Image = .getImage("mailaccount.gif")
            menuSendMail.Image = .getImage("send16.gif")
            Me.Icon = .getIcon("Clinica.ico")
        End With

        centeredLogo.Visible = False
        'centeredLogo.Width = centeredLogo.Image.Width
        'centeredLogo.Height = centeredLogo.Image.Height
        AddHandler PrintingHelper.JobsChanged, AddressOf printJobsChanged
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            RemoveHandler CommandsHolder.getInstance.newAgenda.enabilityChanged, AddressOf Me.newAgendaEnability
            For i As Integer = 0 To Me.Controls.Count - 1
                RemoveHandler Controls(i).MouseMove, AddressOf mainWin_MouseMove
            Next i

            InternalUpdatesManager.getInstance.removeConsumer(Me)
            AlarmManager.getInstance.dispose()

            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents barMainObjects As Clinica.ctlWorkPlace
    Public WithEvents copyBox As ClientCopyBox
    Public WithEvents myClientCopyBoxStrip As ClientCopyBoxStrip
    Public WithEvents formOuvertes As ListCombo
    Public WithEvents labelFO As System.Windows.Forms.Label
    Public WithEvents menuaffagenda As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BlockingMouseLabel As TransparentControl
    Public WithEvents _menujour_1 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menujour_2 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menujour_3 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menujour_4 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menujour_5 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menujour_6 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menujour As BaseObjArray
    Public WithEvents menujourup As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menusemaine_1 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menusemaine_2 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menusemaine_3 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _menusemaine_4 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menusemaine As BaseObjArray
    Public WithEvents menusemaineup As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menumois As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuperiode As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menupdateday As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuadateday As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menupdateweek As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuadateweek As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuchangedate As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuagenda As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menunouveau As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuopen As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuvisite As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menurapport As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuequipement As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menumodifmodele As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menupersonnecle As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menupreferences As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menugestion As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuchangeuser As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuquitlogiciel As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuquitter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RVMenu As Clinica.FutureVisites
    Friend WithEvents AlertMessages As Clinica.InstantMessages
    Friend WithEvents menuWorkHours As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PunchClock As Clinica.Punch
    Friend WithEvents menuPunch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BarOutils As System.Windows.Forms.ToolStrip
    Friend WithEvents MenuItem1 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents _LigneMenu_0 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FenêtresToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents mainWinMenu As System.Windows.Forms.MenuStrip
    Public WithEvents menutherapeute As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents AdminBox As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents AdminButton As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents menuDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOpenDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFavoris As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFileType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRechercherDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuaddfavoris As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuorganizefavoris As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainStatusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusTimer As System.Windows.Forms.Timer
    Friend WithEvents menupdatemonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuadatemonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents FDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents menuQueueList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuNewRV As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRVFutur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuMessagerie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuParamMSG As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSendMail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuGetMail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuAddressBook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusBarPrincipal As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents StatusBarTime As System.Windows.Forms.ToolStripItem
    Friend WithEvents StatusBarPrint As System.Windows.Forms.ToolStripItem
    Friend WithEvents StatusBarDate As System.Windows.Forms.ToolStripItem
    Friend WithEvents StatusBarNews As System.Windows.Forms.ToolStripItem
    Friend WithEvents DateTimeTimer As System.Windows.Forms.Timer
    Friend WithEvents menuAide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuAPropos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents menuAlert As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menucompte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuNewKP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusHistory As System.Windows.Forms.TextBox
    Friend WithEvents menuAffichage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuToolBar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStatusBar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuAgendaContextuel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFactures As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdminTasksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RecountDBTypesCounterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TypesDutilisateurToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuFinDeMois As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NouvelleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OuvrirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CleanUpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EverythingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MettreÀJourClinicaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents centeredLogo As System.Windows.Forms.PictureBox
    Friend WithEvents ModifierVotreMotDePasseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UtilisateursConnectésToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSpecialDates As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AjusterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LesAgendasHorizontalementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LagendaEnCoursEnPleineÉcranToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuReportsCreator As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuGenerateReportsInGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuServerTasks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ConfigurationsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuClinique As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menuuser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DossierClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AnalyseDesTextesDesDossiersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menucodedossier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NouveauDossierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TypeDalerteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TypesDeTexteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExporterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DésactivationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPublipostage As System.Windows.Forms.ToolStripMenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me._menujour_1 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_2 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_3 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_4 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_5 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_6 = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_1 = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_2 = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_3 = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_4 = New System.Windows.Forms.ToolStripMenuItem
        Me.mainWinMenu = New System.Windows.Forms.MenuStrip
        Me.menuagenda = New System.Windows.Forms.ToolStripMenuItem
        Me.menuaffagenda = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.AjusterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LesAgendasHorizontalementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LagendaEnCoursEnPleineÉcranToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.menuchangedate = New System.Windows.Forms.ToolStripMenuItem
        Me.menupdateday = New System.Windows.Forms.ToolStripMenuItem
        Me.menuadateday = New System.Windows.Forms.ToolStripMenuItem
        Me.menupdateweek = New System.Windows.Forms.ToolStripMenuItem
        Me.menuadateweek = New System.Windows.Forms.ToolStripMenuItem
        Me.menupdatemonth = New System.Windows.Forms.ToolStripMenuItem
        Me.menuadatemonth = New System.Windows.Forms.ToolStripMenuItem
        Me.menuperiode = New System.Windows.Forms.ToolStripMenuItem
        Me.menujourup = New System.Windows.Forms.ToolStripMenuItem
        Me.menusemaineup = New System.Windows.Forms.ToolStripMenuItem
        Me.menumois = New System.Windows.Forms.ToolStripMenuItem
        Me.menutherapeute = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAgendaContextuel = New System.Windows.Forms.ToolStripMenuItem
        Me.menuDB = New System.Windows.Forms.ToolStripMenuItem
        Me.menuOpenDB = New System.Windows.Forms.ToolStripMenuItem
        Me.menuFavoris = New System.Windows.Forms.ToolStripMenuItem
        Me.menuaddfavoris = New System.Windows.Forms.ToolStripMenuItem
        Me.menuorganizefavoris = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRechercherDB = New System.Windows.Forms.ToolStripMenuItem
        Me.menuFileType = New System.Windows.Forms.ToolStripMenuItem
        Me.menucompte = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.menunouveau = New System.Windows.Forms.ToolStripMenuItem
        Me.menuopen = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.DossierClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NouveauDossierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExporterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.AnalyseDesTextesDesDossiersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.menucodedossier = New System.Windows.Forms.ToolStripMenuItem
        Me.TypeDalerteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TypesDeTexteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.menuvisite = New System.Windows.Forms.ToolStripMenuItem
        Me.menuNewRV = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRVFutur = New System.Windows.Forms.ToolStripMenuItem
        Me.menuQueueList = New System.Windows.Forms.ToolStripMenuItem
        Me.menuClinique = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuNewKP = New System.Windows.Forms.ToolStripMenuItem
        Me.menupersonnecle = New System.Windows.Forms.ToolStripMenuItem
        Me.menuuser = New System.Windows.Forms.ToolStripMenuItem
        Me.menugestion = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAffichage = New System.Windows.Forms.ToolStripMenuItem
        Me.menuStatusBar = New System.Windows.Forms.ToolStripMenuItem
        Me.menuToolBar = New System.Windows.Forms.ToolStripMenuItem
        Me.menuequipement = New System.Windows.Forms.ToolStripMenuItem
        Me.menuFactures = New System.Windows.Forms.ToolStripMenuItem
        Me.NouvelleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OuvrirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.menuFinDeMois = New System.Windows.Forms.ToolStripMenuItem
        Me.menuSpecialDates = New System.Windows.Forms.ToolStripMenuItem
        Me.menumodifmodele = New System.Windows.Forms.ToolStripMenuItem
        Me.menuPunch = New System.Windows.Forms.ToolStripMenuItem
        Me.menuWorkHours = New System.Windows.Forms.ToolStripMenuItem
        Me.menuServerTasks = New System.Windows.Forms.ToolStripMenuItem
        Me.TypesDutilisateurToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me._LigneMenu_0 = New System.Windows.Forms.ToolStripSeparator
        Me.ConfigurationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ModifierVotreMotDePasseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.menupreferences = New System.Windows.Forms.ToolStripMenuItem
        Me.menuMessagerie = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAddressBook = New System.Windows.Forms.ToolStripMenuItem
        Me.menuSendMail = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAlert = New System.Windows.Forms.ToolStripMenuItem
        Me.menuPublipostage = New System.Windows.Forms.ToolStripMenuItem
        Me.menuGetMail = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.menuParamMSG = New System.Windows.Forms.ToolStripMenuItem
        Me.menurapport = New System.Windows.Forms.ToolStripMenuItem
        Me.menuquitter = New System.Windows.Forms.ToolStripMenuItem
        Me.menuchangeuser = New System.Windows.Forms.ToolStripMenuItem
        Me.menuquitlogiciel = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAide = New System.Windows.Forms.ToolStripMenuItem
        Me.MettreÀJourClinicaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UtilisateursConnectésToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.menuAPropos = New System.Windows.Forms.ToolStripMenuItem
        Me.AdminTasksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RecountDBTypesCounterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CleanUpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EverythingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.menuReportsCreator = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuGenerateReportsInGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AdminBox = New System.Windows.Forms.GroupBox
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.AdminButton = New System.Windows.Forms.Button
        Me.StatusTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FDialog = New System.Windows.Forms.OpenFileDialog
        Me.DateTimeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.BarOutils = New System.Windows.Forms.ToolStrip
        Me.FenêtresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.centeredLogo = New System.Windows.Forms.PictureBox
        Me.barMainObjects = New CI.Clinica.ctlWorkPlace
        Me.DésactivationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mainWinMenu.SuspendLayout()
        Me.AdminBox.SuspendLayout()
        CType(Me.centeredLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_menujour_1
        '
        Me._menujour_1.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menujour_1.MergeIndex = 0
        Me._menujour_1.Name = "_menujour_1"
        Me._menujour_1.Size = New System.Drawing.Size(128, 22)
        Me._menujour_1.Text = "1 journée"
        '
        '_menujour_2
        '
        Me._menujour_2.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menujour_2.MergeIndex = 1
        Me._menujour_2.Name = "_menujour_2"
        Me._menujour_2.Size = New System.Drawing.Size(128, 22)
        Me._menujour_2.Text = "2 journées"
        '
        '_menujour_3
        '
        Me._menujour_3.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menujour_3.MergeIndex = 2
        Me._menujour_3.Name = "_menujour_3"
        Me._menujour_3.Size = New System.Drawing.Size(128, 22)
        Me._menujour_3.Text = "3 journées"
        '
        '_menujour_4
        '
        Me._menujour_4.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menujour_4.MergeIndex = 3
        Me._menujour_4.Name = "_menujour_4"
        Me._menujour_4.Size = New System.Drawing.Size(128, 22)
        Me._menujour_4.Text = "4 journées"
        '
        '_menujour_5
        '
        Me._menujour_5.Checked = True
        Me._menujour_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._menujour_5.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menujour_5.MergeIndex = 4
        Me._menujour_5.Name = "_menujour_5"
        Me._menujour_5.Size = New System.Drawing.Size(128, 22)
        Me._menujour_5.Text = "5 journées"
        '
        '_menujour_6
        '
        Me._menujour_6.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menujour_6.MergeIndex = 5
        Me._menujour_6.Name = "_menujour_6"
        Me._menujour_6.Size = New System.Drawing.Size(128, 22)
        Me._menujour_6.Text = "6 journées"
        '
        '_menusemaine_1
        '
        Me._menusemaine_1.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menusemaine_1.MergeIndex = 0
        Me._menusemaine_1.Name = "_menusemaine_1"
        Me._menusemaine_1.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_1.Text = "1 semaine"
        '
        '_menusemaine_2
        '
        Me._menusemaine_2.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menusemaine_2.MergeIndex = 1
        Me._menusemaine_2.Name = "_menusemaine_2"
        Me._menusemaine_2.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_2.Text = "2 semaines"
        '
        '_menusemaine_3
        '
        Me._menusemaine_3.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menusemaine_3.MergeIndex = 2
        Me._menusemaine_3.Name = "_menusemaine_3"
        Me._menusemaine_3.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_3.Text = "3 semaines"
        '
        '_menusemaine_4
        '
        Me._menusemaine_4.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._menusemaine_4.MergeIndex = 3
        Me._menusemaine_4.Name = "_menusemaine_4"
        Me._menusemaine_4.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_4.Text = "4 semaines"
        '
        'mainWinMenu
        '
        Me.mainWinMenu.BackColor = System.Drawing.SystemColors.ControlLight
        Me.mainWinMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuagenda, Me.menuDB, Me.menucompte, Me.menugestion, Me.menuMessagerie, Me.menurapport, Me.menuquitter, Me.menuAide, Me.AdminTasksToolStripMenuItem})
        Me.mainWinMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.mainWinMenu.Location = New System.Drawing.Point(0, 0)
        Me.mainWinMenu.Name = "mainWinMenu"
        Me.mainWinMenu.Size = New System.Drawing.Size(775, 24)
        Me.mainWinMenu.TabIndex = 19
        '
        'menuagenda
        '
        Me.menuagenda.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuaffagenda, Me.MenuItem1, Me.AjusterToolStripMenuItem, Me.menuchangedate, Me.menuperiode, Me.menutherapeute, Me.menuAgendaContextuel})
        Me.menuagenda.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuagenda.MergeIndex = 0
        Me.menuagenda.Name = "menuagenda"
        Me.menuagenda.Size = New System.Drawing.Size(60, 20)
        Me.menuagenda.Text = "Agenda"
        '
        'menuaffagenda
        '
        Me.menuaffagenda.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuaffagenda.MergeIndex = 0
        Me.menuaffagenda.Name = "menuaffagenda"
        Me.menuaffagenda.Size = New System.Drawing.Size(163, 22)
        Me.menuaffagenda.Text = "Nouvel agenda"
        '
        'MenuItem1
        '
        Me.MenuItem1.MergeIndex = 1
        Me.MenuItem1.Name = "MenuItem1"
        Me.MenuItem1.Size = New System.Drawing.Size(160, 6)
        '
        'AjusterToolStripMenuItem
        '
        Me.AjusterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LesAgendasHorizontalementToolStripMenuItem, Me.LagendaEnCoursEnPleineÉcranToolStripMenuItem})
        Me.AjusterToolStripMenuItem.Enabled = False
        Me.AjusterToolStripMenuItem.Name = "AjusterToolStripMenuItem"
        Me.AjusterToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.AjusterToolStripMenuItem.Text = "Ajuster ..."
        '
        'LesAgendasHorizontalementToolStripMenuItem
        '
        Me.LesAgendasHorizontalementToolStripMenuItem.Name = "LesAgendasHorizontalementToolStripMenuItem"
        Me.LesAgendasHorizontalementToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.LesAgendasHorizontalementToolStripMenuItem.Text = "les agendas horizontalement"
        '
        'LagendaEnCoursEnPleineÉcranToolStripMenuItem
        '
        Me.LagendaEnCoursEnPleineÉcranToolStripMenuItem.Name = "LagendaEnCoursEnPleineÉcranToolStripMenuItem"
        Me.LagendaEnCoursEnPleineÉcranToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.LagendaEnCoursEnPleineÉcranToolStripMenuItem.Text = "l'agenda en cours au maximum"
        '
        'menuchangedate
        '
        Me.menuchangedate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menupdateday, Me.menuadateday, Me.menupdateweek, Me.menuadateweek, Me.menupdatemonth, Me.menuadatemonth})
        Me.menuchangedate.Enabled = False
        Me.menuchangedate.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuchangedate.MergeIndex = 2
        Me.menuchangedate.Name = "menuchangedate"
        Me.menuchangedate.Size = New System.Drawing.Size(163, 22)
        Me.menuchangedate.Text = "Changer la date"
        '
        'menupdateday
        '
        Me.menupdateday.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menupdateday.MergeIndex = 0
        Me.menupdateday.Name = "menupdateday"
        Me.menupdateday.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.menupdateday.Size = New System.Drawing.Size(223, 22)
        Me.menupdateday.Text = "Une journée précédente"
        '
        'menuadateday
        '
        Me.menuadateday.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuadateday.MergeIndex = 1
        Me.menuadateday.Name = "menuadateday"
        Me.menuadateday.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.menuadateday.Size = New System.Drawing.Size(223, 22)
        Me.menuadateday.Text = "Une journée suivante"
        '
        'menupdateweek
        '
        Me.menupdateweek.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menupdateweek.MergeIndex = 2
        Me.menupdateweek.Name = "menupdateweek"
        Me.menupdateweek.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.menupdateweek.Size = New System.Drawing.Size(223, 22)
        Me.menupdateweek.Text = "Une semaine précédente"
        '
        'menuadateweek
        '
        Me.menuadateweek.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuadateweek.MergeIndex = 3
        Me.menuadateweek.Name = "menuadateweek"
        Me.menuadateweek.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.menuadateweek.Size = New System.Drawing.Size(223, 22)
        Me.menuadateweek.Text = "Une semaine suivante"
        '
        'menupdatemonth
        '
        Me.menupdatemonth.MergeIndex = 4
        Me.menupdatemonth.Name = "menupdatemonth"
        Me.menupdatemonth.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.menupdatemonth.Size = New System.Drawing.Size(223, 22)
        Me.menupdatemonth.Text = "Un mois précédent"
        '
        'menuadatemonth
        '
        Me.menuadatemonth.MergeIndex = 5
        Me.menuadatemonth.Name = "menuadatemonth"
        Me.menuadatemonth.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.menuadatemonth.Size = New System.Drawing.Size(223, 22)
        Me.menuadatemonth.Text = "Un mois suivant"
        '
        'menuperiode
        '
        Me.menuperiode.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menujourup, Me.menusemaineup, Me.menumois})
        Me.menuperiode.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuperiode.MergeIndex = 3
        Me.menuperiode.Name = "menuperiode"
        Me.menuperiode.Size = New System.Drawing.Size(163, 22)
        Me.menuperiode.Text = "Période"
        '
        'menujourup
        '
        Me.menujourup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._menujour_1, Me._menujour_2, Me._menujour_3, Me._menujour_4, Me._menujour_5, Me._menujour_6})
        Me.menujourup.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menujourup.MergeIndex = 0
        Me.menujourup.Name = "menujourup"
        Me.menujourup.Size = New System.Drawing.Size(119, 22)
        Me.menujourup.Text = "Jour"
        '
        'menusemaineup
        '
        Me.menusemaineup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._menusemaine_1, Me._menusemaine_2, Me._menusemaine_3, Me._menusemaine_4})
        Me.menusemaineup.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menusemaineup.MergeIndex = 1
        Me.menusemaineup.Name = "menusemaineup"
        Me.menusemaineup.Size = New System.Drawing.Size(119, 22)
        Me.menusemaineup.Text = "Semaine"
        '
        'menumois
        '
        Me.menumois.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menumois.MergeIndex = 2
        Me.menumois.Name = "menumois"
        Me.menumois.Size = New System.Drawing.Size(119, 22)
        Me.menumois.Text = "Mois"
        '
        'menutherapeute
        '
        Me.menutherapeute.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menutherapeute.MergeIndex = 4
        Me.menutherapeute.Name = "menutherapeute"
        Me.menutherapeute.Size = New System.Drawing.Size(163, 22)
        Me.menutherapeute.Text = "Thérapeute"
        '
        'menuAgendaContextuel
        '
        Me.menuAgendaContextuel.MergeIndex = 5
        Me.menuAgendaContextuel.Name = "menuAgendaContextuel"
        Me.menuAgendaContextuel.Size = New System.Drawing.Size(163, 22)
        Me.menuAgendaContextuel.Text = "Menu contextuel"
        Me.menuAgendaContextuel.Visible = False
        '
        'menuDB
        '
        Me.menuDB.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuOpenDB, Me.menuFavoris, Me.menuRechercherDB, Me.menuFileType})
        Me.menuDB.MergeIndex = 1
        Me.menuDB.Name = "menuDB"
        Me.menuDB.Size = New System.Drawing.Size(123, 20)
        Me.menuDB.Text = "Banque de données"
        '
        'menuOpenDB
        '
        Me.menuOpenDB.MergeIndex = 0
        Me.menuOpenDB.Name = "menuOpenDB"
        Me.menuOpenDB.Size = New System.Drawing.Size(161, 22)
        Me.menuOpenDB.Text = "Ouvrir"
        '
        'menuFavoris
        '
        Me.menuFavoris.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuaddfavoris, Me.menuorganizefavoris, Me.MenuItem3})
        Me.menuFavoris.MergeIndex = 1
        Me.menuFavoris.Name = "menuFavoris"
        Me.menuFavoris.Size = New System.Drawing.Size(161, 22)
        Me.menuFavoris.Text = "Favoris v2.0"
        Me.menuFavoris.Visible = False
        '
        'menuaddfavoris
        '
        Me.menuaddfavoris.MergeIndex = 0
        Me.menuaddfavoris.Name = "menuaddfavoris"
        Me.menuaddfavoris.Size = New System.Drawing.Size(125, 22)
        Me.menuaddfavoris.Text = "Ajouter"
        '
        'menuorganizefavoris
        '
        Me.menuorganizefavoris.MergeIndex = 1
        Me.menuorganizefavoris.Name = "menuorganizefavoris"
        Me.menuorganizefavoris.Size = New System.Drawing.Size(125, 22)
        Me.menuorganizefavoris.Text = "Organiser"
        '
        'MenuItem3
        '
        Me.MenuItem3.MergeIndex = 2
        Me.MenuItem3.Name = "MenuItem3"
        Me.MenuItem3.Size = New System.Drawing.Size(125, 22)
        Me.MenuItem3.Text = "-"
        '
        'menuRechercherDB
        '
        Me.menuRechercherDB.MergeIndex = 2
        Me.menuRechercherDB.Name = "menuRechercherDB"
        Me.menuRechercherDB.Size = New System.Drawing.Size(161, 22)
        Me.menuRechercherDB.Text = "Rechercher"
        '
        'menuFileType
        '
        Me.menuFileType.MergeIndex = 3
        Me.menuFileType.Name = "menuFileType"
        Me.menuFileType.Size = New System.Drawing.Size(161, 22)
        Me.menuFileType.Text = "Types de fichiers"
        '
        'menucompte
        '
        Me.menucompte.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItem4, Me.menuClinique, Me.MenuItem5, Me.menuuser})
        Me.menucompte.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menucompte.MergeIndex = 2
        Me.menucompte.Name = "menucompte"
        Me.menucompte.Size = New System.Drawing.Size(67, 20)
        Me.menucompte.Text = "Comptes"
        '
        'MenuItem4
        '
        Me.MenuItem4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menunouveau, Me.menuopen, Me.ToolStripMenuItem2, Me.DossierClientToolStripMenuItem, Me.menuvisite})
        Me.MenuItem4.MergeIndex = 0
        Me.MenuItem4.Name = "MenuItem4"
        Me.MenuItem4.Size = New System.Drawing.Size(220, 22)
        Me.MenuItem4.Text = "Client"
        '
        'menunouveau
        '
        Me.menunouveau.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menunouveau.MergeIndex = 0
        Me.menunouveau.Name = "menunouveau"
        Me.menunouveau.Size = New System.Drawing.Size(152, 22)
        Me.menunouveau.Text = "Nouveau"
        '
        'menuopen
        '
        Me.menuopen.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuopen.MergeIndex = 1
        Me.menuopen.Name = "menuopen"
        Me.menuopen.Size = New System.Drawing.Size(152, 22)
        Me.menuopen.Text = "Ouvrir"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 6)
        '
        'DossierClientToolStripMenuItem
        '
        Me.DossierClientToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NouveauDossierToolStripMenuItem, Me.DésactivationToolStripMenuItem, Me.ExporterToolStripMenuItem, Me.ToolStripMenuItem3, Me.AnalyseDesTextesDesDossiersToolStripMenuItem, Me.menucodedossier, Me.TypeDalerteToolStripMenuItem, Me.TypesDeTexteToolStripMenuItem})
        Me.DossierClientToolStripMenuItem.Name = "DossierClientToolStripMenuItem"
        Me.DossierClientToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DossierClientToolStripMenuItem.Text = "Dossier"
        '
        'NouveauDossierToolStripMenuItem
        '
        Me.NouveauDossierToolStripMenuItem.Name = "NouveauDossierToolStripMenuItem"
        Me.NouveauDossierToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.NouveauDossierToolStripMenuItem.Text = "Nouveau"
        '
        'ExporterToolStripMenuItem
        '
        Me.ExporterToolStripMenuItem.Name = "ExporterToolStripMenuItem"
        Me.ExporterToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.ExporterToolStripMenuItem.Text = "Exportation"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(176, 6)
        '
        'AnalyseDesTextesDesDossiersToolStripMenuItem
        '
        Me.AnalyseDesTextesDesDossiersToolStripMenuItem.Name = "AnalyseDesTextesDesDossiersToolStripMenuItem"
        Me.AnalyseDesTextesDesDossiersToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.AnalyseDesTextesDesDossiersToolStripMenuItem.Text = "Analyse des textes"
        '
        'menucodedossier
        '
        Me.menucodedossier.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menucodedossier.MergeIndex = 2
        Me.menucodedossier.Name = "menucodedossier"
        Me.menucodedossier.Size = New System.Drawing.Size(179, 22)
        Me.menucodedossier.Text = "Codification dossier"
        '
        'TypeDalerteToolStripMenuItem
        '
        Me.TypeDalerteToolStripMenuItem.Name = "TypeDalerteToolStripMenuItem"
        Me.TypeDalerteToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.TypeDalerteToolStripMenuItem.Text = "Types d'alerte"
        '
        'TypesDeTexteToolStripMenuItem
        '
        Me.TypesDeTexteToolStripMenuItem.Name = "TypesDeTexteToolStripMenuItem"
        Me.TypesDeTexteToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.TypesDeTexteToolStripMenuItem.Text = "Types de texte"
        '
        'menuvisite
        '
        Me.menuvisite.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuNewRV, Me.menuRVFutur, Me.menuQueueList})
        Me.menuvisite.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuvisite.MergeIndex = 2
        Me.menuvisite.Name = "menuvisite"
        Me.menuvisite.Size = New System.Drawing.Size(152, 22)
        Me.menuvisite.Text = "Rendez-vous"
        '
        'menuNewRV
        '
        Me.menuNewRV.MergeIndex = 0
        Me.menuNewRV.Name = "menuNewRV"
        Me.menuNewRV.Size = New System.Drawing.Size(180, 22)
        Me.menuNewRV.Text = "Nouveau"
        '
        'menuRVFutur
        '
        Me.menuRVFutur.MergeIndex = 1
        Me.menuRVFutur.Name = "menuRVFutur"
        Me.menuRVFutur.Size = New System.Drawing.Size(180, 22)
        Me.menuRVFutur.Text = "Futur(s)"
        '
        'menuQueueList
        '
        Me.menuQueueList.MergeIndex = 2
        Me.menuQueueList.Name = "menuQueueList"
        Me.menuQueueList.Size = New System.Drawing.Size(180, 22)
        Me.menuQueueList.Text = "Voir la liste d'attente"
        '
        'menuClinique
        '
        Me.menuClinique.MergeIndex = 1
        Me.menuClinique.Name = "menuClinique"
        Me.menuClinique.Size = New System.Drawing.Size(220, 22)
        Me.menuClinique.Text = "Clinique"
        '
        'MenuItem5
        '
        Me.MenuItem5.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuNewKP, Me.menupersonnecle})
        Me.MenuItem5.MergeIndex = 1
        Me.MenuItem5.Name = "MenuItem5"
        Me.MenuItem5.Size = New System.Drawing.Size(220, 22)
        Me.MenuItem5.Text = "Personnes / Organimes clés"
        '
        'MenuNewKP
        '
        Me.MenuNewKP.MergeIndex = 0
        Me.MenuNewKP.Name = "MenuNewKP"
        Me.MenuNewKP.Size = New System.Drawing.Size(122, 22)
        Me.MenuNewKP.Text = "Nouveau"
        '
        'menupersonnecle
        '
        Me.menupersonnecle.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menupersonnecle.MergeIndex = 1
        Me.menupersonnecle.Name = "menupersonnecle"
        Me.menupersonnecle.Size = New System.Drawing.Size(122, 22)
        Me.menupersonnecle.Text = "Ouvrir"
        '
        'menuuser
        '
        Me.menuuser.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuuser.MergeIndex = 8
        Me.menuuser.Name = "menuuser"
        Me.menuuser.Size = New System.Drawing.Size(220, 22)
        Me.menuuser.Text = "Utilisateurs"
        '
        'menugestion
        '
        Me.menugestion.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuAffichage, Me.menuequipement, Me.menuFactures, Me.menuFinDeMois, Me.menuSpecialDates, Me.menumodifmodele, Me.menuPunch, Me.menuWorkHours, Me.menuServerTasks, Me.TypesDutilisateurToolStripMenuItem, Me._LigneMenu_0, Me.ConfigurationsToolStripMenuItem, Me.ModifierVotreMotDePasseToolStripMenuItem, Me.menupreferences})
        Me.menugestion.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menugestion.MergeIndex = 3
        Me.menugestion.Name = "menugestion"
        Me.menugestion.Size = New System.Drawing.Size(59, 20)
        Me.menugestion.Text = "Gestion"
        '
        'menuAffichage
        '
        Me.menuAffichage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStatusBar, Me.menuToolBar})
        Me.menuAffichage.MergeIndex = 0
        Me.menuAffichage.Name = "menuAffichage"
        Me.menuAffichage.Size = New System.Drawing.Size(222, 22)
        Me.menuAffichage.Text = "Affichage"
        '
        'menuStatusBar
        '
        Me.menuStatusBar.Checked = True
        Me.menuStatusBar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.menuStatusBar.MergeIndex = 0
        Me.menuStatusBar.Name = "menuStatusBar"
        Me.menuStatusBar.Size = New System.Drawing.Size(143, 22)
        Me.menuStatusBar.Text = "Barre d'état"
        '
        'menuToolBar
        '
        Me.menuToolBar.Checked = True
        Me.menuToolBar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.menuToolBar.MergeIndex = 1
        Me.menuToolBar.Name = "menuToolBar"
        Me.menuToolBar.Size = New System.Drawing.Size(143, 22)
        Me.menuToolBar.Text = "Barre d'outils"
        '
        'menuequipement
        '
        Me.menuequipement.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuequipement.MergeIndex = 3
        Me.menuequipement.Name = "menuequipement"
        Me.menuequipement.Size = New System.Drawing.Size(222, 22)
        Me.menuequipement.Text = "Équipement"
        '
        'menuFactures
        '
        Me.menuFactures.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NouvelleToolStripMenuItem, Me.OuvrirToolStripMenuItem})
        Me.menuFactures.MergeIndex = 4
        Me.menuFactures.Name = "menuFactures"
        Me.menuFactures.Size = New System.Drawing.Size(222, 22)
        Me.menuFactures.Text = "Factures"
        '
        'NouvelleToolStripMenuItem
        '
        Me.NouvelleToolStripMenuItem.Name = "NouvelleToolStripMenuItem"
        Me.NouvelleToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.NouvelleToolStripMenuItem.Text = "Nouvelle"
        '
        'OuvrirToolStripMenuItem
        '
        Me.OuvrirToolStripMenuItem.Name = "OuvrirToolStripMenuItem"
        Me.OuvrirToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.OuvrirToolStripMenuItem.Text = "Ouvrir"
        '
        'menuFinDeMois
        '
        Me.menuFinDeMois.Name = "menuFinDeMois"
        Me.menuFinDeMois.Size = New System.Drawing.Size(222, 22)
        Me.menuFinDeMois.Text = "Fin de mois"
        '
        'menuSpecialDates
        '
        Me.menuSpecialDates.Name = "menuSpecialDates"
        Me.menuSpecialDates.Size = New System.Drawing.Size(222, 22)
        Me.menuSpecialDates.Text = "Journées spéciales"
        '
        'menumodifmodele
        '
        Me.menumodifmodele.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menumodifmodele.MergeIndex = 5
        Me.menumodifmodele.Name = "menumodifmodele"
        Me.menumodifmodele.Size = New System.Drawing.Size(222, 22)
        Me.menumodifmodele.Text = "Modèles de texte"
        '
        'menuPunch
        '
        Me.menuPunch.MergeIndex = 6
        Me.menuPunch.Name = "menuPunch"
        Me.menuPunch.Size = New System.Drawing.Size(222, 22)
        Me.menuPunch.Text = "'Punch' virtuel v2.0"
        Me.menuPunch.Visible = False
        '
        'menuWorkHours
        '
        Me.menuWorkHours.MergeIndex = 7
        Me.menuWorkHours.Name = "menuWorkHours"
        Me.menuWorkHours.Size = New System.Drawing.Size(222, 22)
        Me.menuWorkHours.Text = "Quarts de travail v2.0"
        Me.menuWorkHours.Visible = False
        '
        'menuServerTasks
        '
        Me.menuServerTasks.Name = "menuServerTasks"
        Me.menuServerTasks.Size = New System.Drawing.Size(222, 22)
        Me.menuServerTasks.Text = "Tâches du serveur"
        '
        'TypesDutilisateurToolStripMenuItem
        '
        Me.TypesDutilisateurToolStripMenuItem.Name = "TypesDutilisateurToolStripMenuItem"
        Me.TypesDutilisateurToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.TypesDutilisateurToolStripMenuItem.Text = "Types d'utilisateurs"
        '
        '_LigneMenu_0
        '
        Me._LigneMenu_0.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me._LigneMenu_0.MergeIndex = 9
        Me._LigneMenu_0.Name = "_LigneMenu_0"
        Me._LigneMenu_0.Size = New System.Drawing.Size(219, 6)
        '
        'ConfigurationsToolStripMenuItem
        '
        Me.ConfigurationsToolStripMenuItem.Name = "ConfigurationsToolStripMenuItem"
        Me.ConfigurationsToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.ConfigurationsToolStripMenuItem.Text = "Configurations du poste"
        '
        'ModifierVotreMotDePasseToolStripMenuItem
        '
        Me.ModifierVotreMotDePasseToolStripMenuItem.Name = "ModifierVotreMotDePasseToolStripMenuItem"
        Me.ModifierVotreMotDePasseToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.ModifierVotreMotDePasseToolStripMenuItem.Text = "Modifier votre mot de passe"
        '
        'menupreferences
        '
        Me.menupreferences.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menupreferences.MergeIndex = 10
        Me.menupreferences.Name = "menupreferences"
        Me.menupreferences.Size = New System.Drawing.Size(222, 22)
        Me.menupreferences.Text = "Préférences"
        '
        'menuMessagerie
        '
        Me.menuMessagerie.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuAddressBook, Me.menuSendMail, Me.menuAlert, Me.menuPublipostage, Me.menuGetMail, Me.MenuItem2, Me.menuParamMSG})
        Me.menuMessagerie.MergeIndex = 4
        Me.menuMessagerie.Name = "menuMessagerie"
        Me.menuMessagerie.Size = New System.Drawing.Size(78, 20)
        Me.menuMessagerie.Text = "Messagerie"
        '
        'menuAddressBook
        '
        Me.menuAddressBook.MergeIndex = 0
        Me.menuAddressBook.Name = "menuAddressBook"
        Me.menuAddressBook.Size = New System.Drawing.Size(262, 22)
        Me.menuAddressBook.Text = "Carnet d'adresses de courriel"
        '
        'menuSendMail
        '
        Me.menuSendMail.MergeIndex = 1
        Me.menuSendMail.Name = "menuSendMail"
        Me.menuSendMail.Size = New System.Drawing.Size(262, 22)
        Me.menuSendMail.Text = "Envoyer un message"
        '
        'menuAlert
        '
        Me.menuAlert.MergeIndex = 2
        Me.menuAlert.Name = "menuAlert"
        Me.menuAlert.Size = New System.Drawing.Size(262, 22)
        Me.menuAlert.Text = "Message(s) instantané(s) && Note(s)"
        '
        'menuPublipostage
        '
        Me.menuPublipostage.MergeIndex = 3
        Me.menuPublipostage.Name = "menuPublipostage"
        Me.menuPublipostage.Size = New System.Drawing.Size(262, 22)
        Me.menuPublipostage.Text = "Publipostage"
        '
        'menuGetMail
        '
        Me.menuGetMail.MergeIndex = 4
        Me.menuGetMail.Name = "menuGetMail"
        Me.menuGetMail.Size = New System.Drawing.Size(262, 22)
        Me.menuGetMail.Text = "Réception des messages"
        '
        'MenuItem2
        '
        Me.MenuItem2.MergeIndex = 5
        Me.MenuItem2.Name = "MenuItem2"
        Me.MenuItem2.Size = New System.Drawing.Size(259, 6)
        '
        'menuParamMSG
        '
        Me.menuParamMSG.MergeIndex = 6
        Me.menuParamMSG.Name = "menuParamMSG"
        Me.menuParamMSG.Size = New System.Drawing.Size(262, 22)
        Me.menuParamMSG.Text = "Paramètres des comptes de courriel"
        '
        'menurapport
        '
        Me.menurapport.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menurapport.MergeIndex = 5
        Me.menurapport.Name = "menurapport"
        Me.menurapport.Size = New System.Drawing.Size(61, 20)
        Me.menurapport.Text = "Rapport"
        '
        'menuquitter
        '
        Me.menuquitter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuchangeuser, Me.menuquitlogiciel})
        Me.menuquitter.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuquitter.MergeIndex = 6
        Me.menuquitter.Name = "menuquitter"
        Me.menuquitter.Size = New System.Drawing.Size(56, 20)
        Me.menuquitter.Text = "Quitter"
        '
        'menuchangeuser
        '
        Me.menuchangeuser.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuchangeuser.MergeIndex = 0
        Me.menuchangeuser.Name = "menuchangeuser"
        Me.menuchangeuser.Size = New System.Drawing.Size(184, 22)
        Me.menuchangeuser.Text = "Changer d'utilisateur"
        '
        'menuquitlogiciel
        '
        Me.menuquitlogiciel.MergeAction = System.Windows.Forms.MergeAction.Remove
        Me.menuquitlogiciel.MergeIndex = 1
        Me.menuquitlogiciel.Name = "menuquitlogiciel"
        Me.menuquitlogiciel.Size = New System.Drawing.Size(184, 22)
        Me.menuquitlogiciel.Text = "Quitter le logiciel"
        '
        'menuAide
        '
        Me.menuAide.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MettreÀJourClinicaToolStripMenuItem, Me.UtilisateursConnectésToolStripMenuItem, Me.ToolStripMenuItem1, Me.menuAPropos})
        Me.menuAide.MergeIndex = 7
        Me.menuAide.Name = "menuAide"
        Me.menuAide.Size = New System.Drawing.Size(24, 20)
        Me.menuAide.Text = "?"
        '
        'MettreÀJourClinicaToolStripMenuItem
        '
        Me.MettreÀJourClinicaToolStripMenuItem.Name = "MettreÀJourClinicaToolStripMenuItem"
        Me.MettreÀJourClinicaToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.MettreÀJourClinicaToolStripMenuItem.Text = "Mettre à jour Clinica"
        '
        'UtilisateursConnectésToolStripMenuItem
        '
        Me.UtilisateursConnectésToolStripMenuItem.Name = "UtilisateursConnectésToolStripMenuItem"
        Me.UtilisateursConnectésToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.UtilisateursConnectésToolStripMenuItem.Text = "Utilisateurs connectés"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(186, 6)
        '
        'menuAPropos
        '
        Me.menuAPropos.MergeIndex = 0
        Me.menuAPropos.Name = "menuAPropos"
        Me.menuAPropos.Size = New System.Drawing.Size(189, 22)
        Me.menuAPropos.Text = "À propos de Clinica"
        '
        'AdminTasksToolStripMenuItem
        '
        Me.AdminTasksToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RecountDBTypesCounterToolStripMenuItem, Me.CleanUpToolStripMenuItem, Me.ReportsToolStripMenuItem})
        Me.AdminTasksToolStripMenuItem.Name = "AdminTasksToolStripMenuItem"
        Me.AdminTasksToolStripMenuItem.Size = New System.Drawing.Size(86, 20)
        Me.AdminTasksToolStripMenuItem.Text = "Admin Tasks"
        Me.AdminTasksToolStripMenuItem.Visible = False
        '
        'RecountDBTypesCounterToolStripMenuItem
        '
        Me.RecountDBTypesCounterToolStripMenuItem.Name = "RecountDBTypesCounterToolStripMenuItem"
        Me.RecountDBTypesCounterToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.RecountDBTypesCounterToolStripMenuItem.Text = "Recount DB types counter"
        '
        'CleanUpToolStripMenuItem
        '
        Me.CleanUpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EverythingToolStripMenuItem, Me.SelectedToolStripMenuItem})
        Me.CleanUpToolStripMenuItem.Name = "CleanUpToolStripMenuItem"
        Me.CleanUpToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.CleanUpToolStripMenuItem.Text = "Clean up..."
        '
        'EverythingToolStripMenuItem
        '
        Me.EverythingToolStripMenuItem.Name = "EverythingToolStripMenuItem"
        Me.EverythingToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.EverythingToolStripMenuItem.Text = "Everything"
        '
        'SelectedToolStripMenuItem
        '
        Me.SelectedToolStripMenuItem.Name = "SelectedToolStripMenuItem"
        Me.SelectedToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.SelectedToolStripMenuItem.Text = "Selected"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuReportsCreator, Me.MenuGenerateReportsInGroupToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'menuReportsCreator
        '
        Me.menuReportsCreator.Name = "menuReportsCreator"
        Me.menuReportsCreator.Size = New System.Drawing.Size(228, 22)
        Me.menuReportsCreator.Text = "Créateur de rapports"
        '
        'MenuGenerateReportsInGroupToolStripMenuItem
        '
        Me.MenuGenerateReportsInGroupToolStripMenuItem.Name = "MenuGenerateReportsInGroupToolStripMenuItem"
        Me.MenuGenerateReportsInGroupToolStripMenuItem.Size = New System.Drawing.Size(228, 22)
        Me.MenuGenerateReportsInGroupToolStripMenuItem.Text = "Générateur de rapports en lot"
        '
        'toolTip1
        '
        Me.toolTip1.ShowAlways = True
        '
        'AdminBox
        '
        Me.AdminBox.Controls.Add(Me.Button7)
        Me.AdminBox.Controls.Add(Me.Button6)
        Me.AdminBox.Controls.Add(Me.Button5)
        Me.AdminBox.Controls.Add(Me.Button1)
        Me.AdminBox.Controls.Add(Me.Button4)
        Me.AdminBox.Controls.Add(Me.Button3)
        Me.AdminBox.Controls.Add(Me.TextBox3)
        Me.AdminBox.Controls.Add(Me.TextBox2)
        Me.AdminBox.Controls.Add(Me.TextBox1)
        Me.AdminBox.Controls.Add(Me.Button2)
        Me.AdminBox.Location = New System.Drawing.Point(419, 52)
        Me.AdminBox.Name = "AdminBox"
        Me.AdminBox.Size = New System.Drawing.Size(264, 167)
        Me.AdminBox.TabIndex = 14
        Me.AdminBox.TabStop = False
        Me.AdminBox.Text = "Administration"
        Me.AdminBox.Visible = False
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(160, 88)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(64, 24)
        Me.Button7.TabIndex = 16
        Me.Button7.Text = "Browser"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(72, 88)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(88, 24)
        Me.Button6.TabIndex = 15
        Me.Button6.Text = "OpenAccount"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(8, 112)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(40, 24)
        Me.Button5.TabIndex = 14
        Me.Button5.Text = "Test"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(48, 112)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(128, 24)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "ChangeHoraireFilter"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(72, 64)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(40, 24)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Save"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(8, 64)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(56, 24)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Prendre"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(8, 40)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(16, 20)
        Me.TextBox3.TabIndex = 3
        Me.TextBox3.Text = "1"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(32, 40)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(224, 20)
        Me.TextBox2.TabIndex = 2
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(8, 16)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(224, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(240, 16)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(16, 16)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "X"
        '
        'AdminButton
        '
        Me.AdminButton.Location = New System.Drawing.Point(419, 52)
        Me.AdminButton.Name = "AdminButton"
        Me.AdminButton.Size = New System.Drawing.Size(64, 24)
        Me.AdminButton.TabIndex = 16
        Me.AdminButton.Text = "Admin"
        Me.AdminButton.Visible = False
        '
        'StatusTimer
        '
        Me.StatusTimer.Interval = 2000
        '
        'FDialog
        '
        Me.FDialog.Title = "Sélection du fichier à importer"
        '
        'DateTimeTimer
        '
        Me.DateTimeTimer.Enabled = True
        Me.DateTimeTimer.Interval = 1000
        '
        'BarOutils
        '
        Me.BarOutils.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BarOutils.Location = New System.Drawing.Point(0, 24)
        Me.BarOutils.Name = "BarOutils"
        Me.BarOutils.Size = New System.Drawing.Size(775, 25)
        Me.BarOutils.TabIndex = 18
        Me.BarOutils.Text = "Bar d'outils"
        '
        'FenêtresToolStripMenuItem
        '
        Me.FenêtresToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.FenêtresToolStripMenuItem.Name = "FenêtresToolStripMenuItem"
        Me.FenêtresToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.FenêtresToolStripMenuItem.Text = "Fenêtres"
        '
        'centeredLogo
        '
        Me.centeredLogo.Location = New System.Drawing.Point(247, 147)
        Me.centeredLogo.Name = "centeredLogo"
        Me.centeredLogo.Size = New System.Drawing.Size(99, 47)
        Me.centeredLogo.TabIndex = 21
        Me.centeredLogo.TabStop = False
        '
        'barMainObjects
        '
        Me.barMainObjects.isMovingPanel = False
        Me.barMainObjects.isPanelBlocked = False
        Me.barMainObjects.Location = New System.Drawing.Point(0, 0)
        Me.barMainObjects.Name = "barMainObjects"
        Me.barMainObjects.Size = New System.Drawing.Size(0, 0)
        Me.barMainObjects.TabIndex = 0
        Me.barMainObjects.textShowedCharByCharVertically = False
        '
        'DésactivationToolStripMenuItem
        '
        Me.DésactivationToolStripMenuItem.Name = "DésactivationToolStripMenuItem"
        Me.DésactivationToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.DésactivationToolStripMenuItem.Text = "Désactivation"
        '
        'MainWin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(775, 409)
        Me.Controls.Add(Me.barMainObjects)
        Me.Controls.Add(Me.BarOutils)
        Me.Controls.Add(Me.AdminBox)
        Me.Controls.Add(Me.AdminButton)
        Me.Controls.Add(Me.mainWinMenu)
        Me.Controls.Add(Me.centeredLogo)
        Me.DoubleBuffered = True
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(137, 62)
        Me.MainMenuStrip = Me.mainWinMenu
        Me.Name = "MainWin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clinica"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.mainWinMenu.ResumeLayout(False)
        Me.mainWinMenu.PerformLayout()
        Me.AdminBox.ResumeLayout(False)
        Me.AdminBox.PerformLayout()
        CType(Me.centeredLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub linkmenujourArray()
        Me.menujour = New BaseObjArray()
        menujour.add(Me._menujour_1)
        menujour.add(Me._menujour_1)
        menujour.add(Me._menujour_2)
        menujour.add(Me._menujour_3)
        menujour.add(Me._menujour_4)
        menujour.add(Me._menujour_5)
        menujour.add(Me._menujour_6)
    End Sub

    Private Sub linkmenusemaineArray()
        Me.menusemaine = New BaseObjArray()
        menusemaine.add(Me._menusemaine_1)
        menusemaine.add(Me._menusemaine_1)
        menusemaine.add(Me._menusemaine_2)
        menusemaine.add(Me._menusemaine_3)
        menusemaine.add(Me._menusemaine_4)
    End Sub

    Private Sub adjustAgendasSize()
        'TODO : Disabled code because finding it too long to load

        'Dim oldFocusForm As Form = myMainWin.ActiveMdiChild

        'For Each curItem As CI.Controls.ListItem In formOuvertes.items
        '    Dim curWindow As SingleWindow = curItem.valueA
        '    If TypeOf (curWindow) Is Agenda Then
        '        curWindow.MinimumSize = New Size(myMainWin.mdiChildRectangle.Width, curWindow.MinimumSize.Height)
        '        curWindow.Width = myMainWin.mdiChildRectangle.Width
        '        With CType(curWindow, Agenda)
        '            .buildStructure(.NoVertical, .NoHorizontal, .DebutDate)
        '            .loadAgenda()
        '        End With
        '    End If
        'Next

        'If oldFocusForm IsNot Nothing Then ActivateMdiChild(oldFocusForm)
    End Sub

    Private lastSideBarVisible As Boolean = False

    Private Sub barVisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim curSideBarVisible As Boolean = barMainObjects.isBarVisibleForced
        If lastSideBarVisible <> curSideBarVisible Then
            lastSideBarVisible = curSideBarVisible
            adjustAgendasSize()
        End If
    End Sub

    Private Sub initializeToolbars()
        AddHandler Me.barMainObjects.barShowed, AddressOf barVisibleChanged
        AddHandler Me.barMainObjects.barHidden, AddressOf barVisibleChanged
        Me.barMainObjects.Dock = DockStyle.Right
        CType(Me.barMainObjects, Control).Visible = True
        Me.barMainObjects.Width = 100

        initToolBar()
        initStatusBar()

        'Listbox fenêtres ouvertes avec son label
        Me.formOuvertes = New ListCombo
        Me.labelFO = New System.Windows.Forms.Label
        '
        'FormOuvertes
        '
        Me.formOuvertes.BackColor = System.Drawing.SystemColors.Window
        Me.formOuvertes.Cursor = System.Windows.Forms.Cursors.Default
        Me.formOuvertes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.formOuvertes.ForeColor = System.Drawing.SystemColors.WindowText
        Me.formOuvertes.Location = New System.Drawing.Point(416, 0)
        Me.formOuvertes.maxDropDownItems = 15
        Me.formOuvertes.Name = "FormOuvertes"
        Me.formOuvertes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.formOuvertes.Size = New System.Drawing.Size(337, 22)
        Me.formOuvertes.sorted = True
        Me.formOuvertes.TabIndex = 3
        Me.formOuvertes.config()
        Me.formOuvertes.draw = True
        Me.formOuvertes.autoSizeHorizontally = True
        Me.formOuvertes.autoSizeVertically = True
        Me.formOuvertes.Text = "Choisir une fenêtre"
        Me.formOuvertes.autoCloseDropDownOnSelectionByUser = True
        Me.formOuvertes.showSelectedFirstLineWhenDropDownClosed = True
        Me.formOuvertes.showDropDownOnHeaderClick = True
        '
        'LabelFO
        '
        Me.labelFO.AutoSize = True
        Me.labelFO.BackColor = mainWinMenu.BackColor
        Me.labelFO.Cursor = System.Windows.Forms.Cursors.Default
        Me.labelFO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelFO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labelFO.Location = New System.Drawing.Point(368, 3)
        Me.labelFO.Name = "LabelFO"
        Me.labelFO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labelFO.Size = New System.Drawing.Size(59, 14)
        Me.labelFO.TabIndex = 2
        Me.labelFO.Text = "Fenêtres : "
    End Sub

    Private Sub initToolBar()
        'Build BarOutils
        Me.BarOutils.SuspendLayout()
        Me.BarOutils.Stretch = True
        Me.BarOutils.Items.Add(CommandsHolder.getInstance.newAgenda)
        Me.BarOutils.Items.Add(CommandsHolder.getInstance.searchClient)
        Me.BarOutils.Items.Add(CommandsHolder.getInstance.searchDB)
        Me.BarOutils.Items.Add(CommandsHolder.getInstance.futurVisites)
        Me.BarOutils.Items.Add(CommandsHolder.getInstance.queueList)
        Me.BarOutils.Items.Add(CommandsHolder.getInstance.rapport)
        Me.BarOutils.Items.Add(CommandsHolder.getInstance.quitChange)
        Dim copyBoxLabel As New ToolStripLabel("Client copié :")
        copyBoxLabel.Dock = DockStyle.Right
        Me.BarOutils.Items.Add(copyBoxLabel)
        copyBox = New ClientCopyBox
        myClientCopyBoxStrip = New ClientCopyBoxStrip(copyBox)
        Me.BarOutils.Items.Add(myClientCopyBoxStrip)
        Me.BarOutils.ResumeLayout(False)
        Me.BarOutils.PerformLayout()
    End Sub

    Private Sub initStatusBar()
        'Status bar
        Me.MainStatusBar = New System.Windows.Forms.StatusStrip
        Me.StatusBarPrincipal = New System.Windows.Forms.ToolStripDropDownButton
        Me.StatusBarDate = New System.Windows.Forms.ToolStripButton
        Me.StatusBarNews = New System.Windows.Forms.ToolStripButton
        Me.StatusBarTime = New System.Windows.Forms.ToolStripButton
        Me.StatusBarPrint = New System.Windows.Forms.ToolStripButton
        Me.StatusHistory = New System.Windows.Forms.TextBox
        'CType(Me.StatusBarPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.StatusBarDate, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.StatusBarTime, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'MainStatusBar
        '
        Me.MainStatusBar.Location = New System.Drawing.Point(0, 440)
        Me.MainStatusBar.Name = "MainStatusBar"
        Me.MainStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusBarPrincipal, StatusBarPrint, StatusBarNews, Me.StatusBarDate, Me.StatusBarTime})
        'Me.MainStatusBar.ShowPanel = True
        Me.MainStatusBar.Size = New System.Drawing.Size(760, 24)
        Me.MainStatusBar.SizingGrip = False
        Me.MainStatusBar.TabIndex = 18
        Me.MainStatusBar.ShowItemToolTips = True
        '
        'StatusBarPrincipal
        '
        'Me.StatusBarPrincipal.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        Me.StatusBarPrincipal.AutoSize = False
        Me.StatusBarPrincipal.DropDownDirection = ToolStripDropDownDirection.AboveLeft
        Me.StatusBarPrincipal.TextAlign = ContentAlignment.MiddleLeft
        Me.StatusBarPrincipal.Name = "StatusBarPrincipal"
        Me.StatusBarPrincipal.Width = 800
        'Me.StatusBarPrincipal.Dock = DockStyle.Fill
        '
        'StatusBarPrint
        '
        'Me.StatusBarPrint.Dock = DockStyle.Right
        Me.StatusBarPrint.TextAlign = ContentAlignment.MiddleCenter
        Me.StatusBarPrint.Name = "StatusBarPrint"
        Me.StatusBarPrint.Text = String.Empty
        Me.StatusBarPrint.Width = 20
        Me.StatusBarPrint.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("print16.ico"), New Size(16, 16))
        Me.StatusBarPrint.Visible = False
        '
        'StatusBarDate
        '
        Me.StatusBarDate.AutoSize = True
        'Me.StatusBarDate.Dock = DockStyle.Right
        Me.StatusBarDate.TextAlign = ContentAlignment.MiddleCenter
        Me.StatusBarDate.Name = "StatusBarDate"
        Me.StatusBarDate.Text = "DATE"
        Me.StatusBarDate.Text = 44
        '
        'StatusBarNews
        '
        'Me.StatusBarNews.Dock = DockStyle.Right
        Me.StatusBarNews.TextAlign = ContentAlignment.MiddleCenter
        Me.StatusBarNews.Name = "StatusBarNews"
        Me.StatusBarNews.Text = "0"
        Me.StatusBarNews.Width = 20
        Me.StatusBarNews.Visible = True
        Me.StatusBarNews.AutoToolTip = True
        Me.StatusBarNews.Font = New Font(Me.StatusBarNews.Font, FontStyle.Bold)
        Me.StatusBarNews.ToolTipText = "Double-cliquer pour accéder aux nouveautés"
        '
        'StatusBarTime
        '
        'Me.StatusBarTime.Dock = DockStyle.Right
        Me.StatusBarTime.TextAlign = ContentAlignment.MiddleCenter
        Me.StatusBarTime.Name = "StatusBarTime"
        Me.StatusBarTime.Text = "TIME"
        Me.StatusBarTime.Width = 41
        '
        'StatusHistory
        '
        Me.StatusHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.StatusHistory.Location = New System.Drawing.Point(0, 312)
        Me.StatusHistory.Multiline = True
        Me.StatusHistory.Name = "StatusHistory"
        Me.StatusHistory.ReadOnly = True
        Me.StatusHistory.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.StatusHistory.Size = New System.Drawing.Size(672, 152)
        Me.StatusHistory.TabIndex = 22
        Me.StatusHistory.Text = "Historique des actions :"
        Me.StatusHistory.Visible = False

        Me.Controls.Add(Me.MainStatusBar)
        Me.Controls.Add(Me.StatusHistory)

        Me.StatusHistory.BringToFront()

        'CType(Me.StatusBarPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.StatusBarDate, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.StatusBarTime, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#End Region


    Private Const spacing_mdi_window_buttons As Integer = 62 'L'espacement pour les boutons des fenêtres maximisés
    Private Const bar_spacing As Byte = 1

    Friend printingJobs As PrintingJobs

    Private adjustedStatusBar As Boolean = False
    Private _IsLocked As Boolean = False
    Private dateFormatter As New ManagedText()
    Private shiftKeyDown As Boolean = False
    Private quitToChange As Boolean
    Private quitQuestion As Boolean = True
    Private lastFormOuvertesText As String
    Private statusPanelClicked As Byte
    Private lastUpdatesFileSize As Long = 0
    Private curWindowName As String = ""
    Private lastTwoWindows As New Generic.List(Of SingleWindow)

#Region "Propriétés"
    Protected Overrides ReadOnly Property canRaiseEvents() As Boolean
        Get
            Return True
        End Get
    End Property

    Shadows ReadOnly Property ClientRectangle() As Drawing.Rectangle
        Get
            Return calculateMdiFormSpace()
        End Get
    End Property

    Private Function calculateMdiFormSpace(Optional ByVal minimalSize As Boolean = False) As Rectangle
        Dim OffSetY, offSetHeight, offsetWidth As Integer
        OffSetY = Me.mainWinMenu.Height
        If minimalSize OrElse Me.BarOutils.Visible Then OffSetY += Me.BarOutils.Height
        offSetHeight = OffSetY
        If minimalSize OrElse Me.MainStatusBar.Visible Then offSetHeight += Me.MainStatusBar.Height

        If minimalSize OrElse barMainObjects.isBarVisible Then offsetWidth = barMainObjects.barWidth

        Dim width As Integer = maximizedDisplaySize.Width - offsetWidth + 12 '- 6
        If width < 0 Then width = 0
        Dim height As Integer = maximizedDisplaySize.Height - offSetHeight + 12 ' - 6
        If height < 0 Then height = 0

        Return New Drawing.Rectangle(MyBase.DisplayRectangle.Left, MyBase.DisplayRectangle.Top + OffSetY, width, height)
    End Function

    Public ReadOnly Property minimumMdiChildRectangle() As Drawing.Rectangle
        Get
            Return calculateMdiFormSpace(True)
        End Get
    End Property

    Public ReadOnly Property mdiChildRectangle() As Drawing.Rectangle
        Get
            Return calculateMdiFormSpace()
        End Get
    End Property

    Public WriteOnly Property SetQuitToChange(Optional ByVal askingQuitQuestion As Boolean = True) As Boolean
        Set(ByVal Value As Boolean)
            quitToChange = Value
            quitQuestion = askingQuitQuestion
        End Set
    End Property

    Delegate Sub setTextCallback(ByVal [text] As String)
    Delegate Sub setStatusTextCallback(ByVal [text] As String, ByVal isDialog As Boolean)

    Private statutActivated As Boolean = False

    Private Sub setText(ByVal [text] As String, ByVal isDialog As Boolean)
        If isDialog Then
            MessageBox.Show(text, "Information importante", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        StatusTimer.Stop()

        StatusHistory.Text &= vbCrLf & "- (" & DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.ShortTime) & ") " & [text]
        StatusHistory.SelectionStart = StatusHistory.Text.Length
        StatusHistory.ScrollToCaret()
        StatusBarPrincipal.Text = [text]
        StatusBarPrincipal.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
        'StatutActivated = True

        StatusTimer.Interval = PreferencesManager.getGeneralPreferences()("IntervalToShowTextInStatusBar") * 1000
        StatusTimer.Start()
    End Sub

    Public WriteOnly Property StatusText(Optional ByVal showAsDialog As Boolean = False) As String
        Set(ByVal Value As String)
            If MainStatusBar.InvokeRequired Then
                Dim d As New setStatusTextCallback(AddressOf setText)
                Me.BeginInvoke(d, New Object() {Value, showAsDialog})
            Else
                setText(Value, showAsDialog)
            End If
        End Set
    End Property
#End Region

#Region "Administrator Code"

    Private testMutex As New System.Threading.Mutex()
    Private testMutexCounter As Integer

    Private Sub button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
    
        'Dim t As New DateChoice()
        'Dim wholeWeek As Boolean = False
        'Dim trp As String = "T (18)"
        't.choose(1900, 2020, , , True, , , True, Date.Today.AddDays(-10), trp, , , Date.Today, wholeWeek, True, , , , Date.Today.AddDays(10))

        'User.validateNAM("BOIJ83072718")
        'User.validateNAM("BOIG58060919")
        'User.validateNAM("LEBJ82092310")

        'Dim newTestWin As New testWin
        'testWin.ShowDialog()

        'DBLinker.executeSQLScript("USE ClinicaDev1", False)
        'Dim t As New ClientSearch2()
        't.ShowDialog()
        'TEST BUTTON
    End Sub

    Private pop3Server As Protocols.POP3

    Private lastNumMail As Integer = 0
    Private lastPourcent As Double = 0
    Private lastNumEtape As Integer = 0

    Private Sub chargementEnCours(ByVal etatChargement As String, ByVal numMail As Int32, ByVal numEtape As Integer, ByVal pourcentMail As Double)
        'Modif of vars
        pourcentMail = Math.Round(pourcentMail, 0)
        If pourcentMail > 100 Then pourcentMail = 100

        If numEtape = 2 And pourcentMail = 100 Then
            Dim a As Byte = 0
        End If

        'Ensure not too much messages
        If pourcentMail > 0 AndAlso pourcentMail <= 100 AndAlso lastNumEtape = numEtape AndAlso lastNumMail = numMail AndAlso (lastPourcent + 2) > pourcentMail Then Exit Sub

        Me.StatusText = "Téléchargement de messages - (" & numMail & "/" & pop3Server.newMessagesDownloadedCount & ") : " & numEtape & " - " & etatChargement & " (" & pourcentMail & " %)"
        lastNumMail = numMail
        lastNumEtape = numEtape
        lastPourcent = pourcentMail
    End Sub

    Private Sub button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim myBrowser As Browser = openUniqueWindow(New Browser())
        myBrowser.Show()
    End Sub

    Private Sub button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        openAccount(myInputBoxPlus("NoClient", "NoClient", "1420"))
    End Sub

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim fileNum As Integer = FreeFile()
            Dim myString As String = ""
            FileOpen(fileNum, appPath & bar(appPath) & TextBox1.Text, OpenMode.Random)
            FileGetObject(fileNum, myString, TextBox3.Text)
            TextBox2.Text = myString
            FileClose(fileNum)
        Catch ex As Exception
            MessageBox.Show("Erreur")
        End Try
    End Sub

    Private Sub button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim fileNum As Integer = FreeFile()
            FileOpen(fileNum, appPath & bar(appPath) & TextBox1.Text, OpenMode.Random)
            FilePutObject(fileNum, TextBox2.Text, TextBox3.Text)
            FileClose(fileNum)
        Catch ex As Exception
            MessageBox.Show("Erreur")
        End Try
    End Sub

    Private Sub adminButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminButton.Click
        AdminBox.Visible = True
    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        AdminBox.Visible = False
    End Sub
#End Region

#Region "Events des Menus"
#Region "menujour Events"
    Private Sub _menujour_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_1.Click
        menujour_Click(1, sender, e)
    End Sub
    Private Sub _menujour_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_2.Click
        menujour_Click(2, sender, e)
    End Sub
    Private Sub _menujour_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_3.Click
        menujour_Click(3, sender, e)
    End Sub
    Private Sub _menujour_4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_4.Click
        menujour_Click(4, sender, e)
    End Sub
    Private Sub _menujour_5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_5.Click
        menujour_Click(5, sender, e)
    End Sub
    Private Sub _menujour_6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_6.Click
        menujour_Click(6, sender, e)
    End Sub
#End Region

#Region "menusemaine Events"
    Private Sub _menusemaine_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_1.Click
        menusemaine_Click(1, sender, e)
    End Sub
    Private Sub _menusemaine_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_2.Click
        menusemaine_Click(2, sender, e)
    End Sub
    Private Sub _menusemaine_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_3.Click
        menusemaine_Click(3, sender, e)
    End Sub
    Private Sub _menusemaine_4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_4.Click
        menusemaine_Click(4, sender, e)
    End Sub
#End Region

    Private maximizedDisplaySize As New Size(0, 0)

#Region "Preloading"
    Private Sub preloadWinConfig()
        With Me
            .lockItems(True)
            .Text = .Text & " : " & currentUserName & " (" & ConnectionsManager.currentUser & ")"
            .Top = 0
            .Left = 0
            .Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
            .Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
            .WindowState = System.Windows.Forms.FormWindowState.Maximized
            maximizedDisplaySize = New Size(MyBase.DisplayRectangle.Width, MyBase.DisplayRectangle.Height)

            .barMainObjects.textShowedCharByCharVertically = PreferencesManager.getGeneralPreferences()("TextShowedCharByCharVertically")

            If currentUserName = "Administrateur" Then .AdminButton.Visible = True

            Loading.getInstance.forward("Charge les barres") 'Avance de un le chargement

            'Reconstruction de la barre d'outils
            .formOuvertes.Left = .mainWinMenu.Width - .formOuvertes.Width - bar_spacing - spacing_mdi_window_buttons
            .formOuvertes.Top = (.mainWinMenu.Height - .formOuvertes.Height) / 2
            .labelFO.Left = .formOuvertes.Left - .labelFO.Width
            .labelFO.Top = (.mainWinMenu.Height - .labelFO.Height) / 2
            .Controls.Add(.formOuvertes)
            .Controls.Add(.labelFO)
            .mainWinMenu.SendToBack()
            Dim maxPref As String = PreferencesManager.getUserPreferences("NbMaxWindowsListBox")
            .formOuvertes.maxDropDownItems = If(maxPref = "", 15, Integer.Parse(maxPref))
            .formOuvertes.BringToFront()
            .labelFO.BringToFront()
            'Type d'affichage pour la liste des fenêtres
            If PreferencesManager.getGeneralPreferences()("TypeAffListeFenetres").ToString.IndexOf("menu") <> -1 Then
                .mainWinMenu.Items.Add(.FenêtresToolStripMenuItem)
                .formOuvertes.Visible = False
                .labelFO.Visible = False
            End If

            .Visible = False
            .Invalidate(False)

            'Sélection de la période préférée
            uncheckedPeriode(myMainWin)
            Dim defPeriode As String = PreferencesManager.getUserPreferences()("StartingPeriode").ToString
            Select Case True
                Case defPeriode = ""
                    .menujour(5).Checked = True
                Case defPeriode.Substring(0, 4) = "Jour"
                    .menujour(defPeriode.Substring(defPeriode.Length - 1, 1)).Checked = True
                Case defPeriode.Substring(0, 4) = "Sema"
                    .menusemaine(defPeriode.Substring(defPeriode.Length - 1, 1)).Checked = True
                Case defPeriode.Substring(0, 4) = "Mois"
                    .menumois.Checked = True
            End Select
        End With
    End Sub

    Public Sub preloadMainObjects()
        With Me
            myMainWin.barMainObjects.addControl(myMainWin.AlertMessages)
            myMainWin.barMainObjects.addControl(myMainWin.RVMenu)
            myMainWin.barMainObjects.addControl(myMainWin.PunchClock)
            myMainWin.barMainObjects.addControl(myMainWin.printingJobs)

            'InstantMessages & FuturRV & Punch settings
            Loading.getInstance.forward("Charges les propriétés des objets principaux") 'Avance de un le chargement

            Dim curSettings As UserSettings
            Dim lineSplit() As String
            If UsersManager.currentUser IsNot Nothing Then
                curSettings = UsersManager.currentUser.settings
                If curSettings.newRV <> "" Then
                    lineSplit = Split(curSettings.newRV, "§")
                    RVMenu.Top = lineSplit(0)
                    RVMenu.Left = lineSplit(1)
                    RVMenu.blockMove = CType(lineSplit(2), Boolean)
                    RVMenu.IsSwitchedToToolbar(False) = CType(lineSplit(3), Boolean)
                    RVMenu.AskExtraQuestion.Checked = lineSplit(4)
                End If
                If curSettings.instantMSG <> "" Then
                    lineSplit = Split(curSettings.instantMSG, "§")
                    AlertMessages.Top = lineSplit(0)
                    AlertMessages.Left = lineSplit(1)
                    AlertMessages.blockMove = CType(lineSplit(2), Boolean)
                    AlertMessages.IsSwitchedToToolbar(False) = CType(lineSplit(3), Boolean)
                End If
                If curSettings.punch <> "" Then
                    lineSplit = Split(curSettings.punch, "§")
                    PunchClock.Top = lineSplit(0)
                    PunchClock.Left = lineSplit(1)
                    PunchClock.blockMove = CType(lineSplit(2), Boolean)
                    PunchClock.IsSwitchedToToolbar(False) = CType(lineSplit(3), Boolean)
                End If
            End If
            AlertMessages.applyNotesSettings()

            If RVMenu.IsSwitchedToToolbar = False Then
                Me.barMainObjects.mainObjectsPanel.Controls.Remove(RVMenu)
                Me.Controls.Add(RVMenu)
            End If
            If AlertMessages.IsSwitchedToToolbar = False Then
                Me.barMainObjects.mainObjectsPanel.Controls.Remove(AlertMessages)
                Me.Controls.Add(AlertMessages)
            End If
            If PunchClock.IsSwitchedToToolbar = False Then
                Me.barMainObjects.mainObjectsPanel.Controls.Remove(PunchClock)
                Me.Controls.Add(PunchClock)
            End If

            .RVMenu.loading()
            .PunchClock.loading()

            Loading.getInstance.forward("Charges les propriétés des objets principaux") 'Avance de un le chargement

            'Settings
            .StatusTimer.Interval = PreferencesManager.getGeneralPreferences()("IntervalToShowTextInStatusBar") * 1000

            If curSettings IsNot Nothing AndAlso curSettings.mainWin <> "" Then
                lineSplit = Split(curSettings.mainWin, "§")
                .menuStatusBar.Checked = CType(lineSplit(0), Boolean)
                .MainStatusBar.Visible = .menuStatusBar.Checked
                .menuToolBar.Checked = CType(lineSplit(1), Boolean)
                .BarOutils.Visible = .menuToolBar.Checked
                .barMainObjects.isPanelBlocked = True
                If CStr(PreferencesManager.getUserPreferences()("OpenFuturRV")).StartsWith("Ro") Then .menuRVFutur.Checked = CType(lineSplit(2), Boolean)
                If CStr(PreferencesManager.getUserPreferences()("OpenFuturRV")).StartsWith("Ou") Then .menuRVFutur.Checked = True
                If .menuRVFutur.Checked Then .RVMenu.visible = True
                If CStr(PreferencesManager.getUserPreferences()("OpenInstantMSG")).StartsWith("Ro") Then .menuAlert.Checked = CType(lineSplit(3), Boolean)
                If CStr(PreferencesManager.getUserPreferences()("OpenInstantMSG")).StartsWith("Ou") Then .menuAlert.Checked = True
                If .menuAlert.Checked Then .AlertMessages.visible = True
                If CStr(PreferencesManager.getUserPreferences()("OpenPunch")).StartsWith("Ro") Then .menuPunch.Checked = CType(lineSplit(4), Boolean)
                If CStr(PreferencesManager.getUserPreferences()("OpenPunch")).StartsWith("Ou") Then .menuPunch.Checked = True
                'REM Disabled function                     If .menuPunch.Checked Then .PunchClock.Visible = True
                .menuPunch.Checked = False
                .barMainObjects.isPanelBlocked = False
            End If
            .barMainObjects.BringToFront()
        End With
    End Sub

    Private Sub loadNewsNotification()
        'Look for updates
        Dim nbNews() As String = DBLinker.getInstance.readOneDBField("SoftwareNews INNER JOIN SoftwareNewsUsers ON SoftwareNews.NoSoftwareNews = SoftwareNewsUsers.NoSoftwareNews", "COUNT(*)", "WHERE SoftwareNewsUsers.Viewed=0 AND NoUser=" & ConnectionsManager.currentUser)
        If nbNews IsNot Nothing AndAlso nbNews.Length <> 0 AndAlso nbNews(0) > 0 Then
            StatusBarNews.Text = "* " & nbNews(0) & " *"
            StatusBarNews.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
            StatusBarNews.Visible = True
            StatusBarDate.Width += 20
        Else
            Software.setNewsHasSeen()
            StatusBarNews.Visible = False
        End If
    End Sub

    Private Sub loadAlertsWindow()
        'Charge les Alertes|Alarmes & notes
        AlertMessages.activatedColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
        If ConnectionsManager.currentUser <> 0 Then
            Dim myStyle As FontStyle = FontStyle.Regular
            If PreferencesManager.getGeneralPreferences()("NewAlertGras") Then myStyle += FontStyle.Bold
            If PreferencesManager.getGeneralPreferences()("NewAlertItalic") Then myStyle += FontStyle.Italic
            If PreferencesManager.getGeneralPreferences()("NewAlertStrike") Then myStyle += FontStyle.Strikeout
            If PreferencesManager.getGeneralPreferences()("NewAlertUnder") Then myStyle += FontStyle.Underline

            Dim newFontSize As Double = 8
            If Double.TryParse(PreferencesManager.getGeneralPreferences()("NewAlertFontSize").Replace(",", "."), newFontSize) = False Then
                Double.TryParse(PreferencesManager.getGeneralPreferences()("NewAlertFontSize").Replace(".", ","), newFontSize)
            End If

            AlertMessages.newAlertFont = New Font(PreferencesManager.getGeneralPreferences()("NewRvFont").ToString, CType(newFontSize, Single), myStyle)

            AlertMessages.loadAlerts()
        End If
    End Sub

    Private Sub windows_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        Me.barMainObjects.hideBar()
    End Sub

    Public Shadows Sub show()
        Me.preloadWinConfig()

        'Register to mouse enter event of child windows
        AddHandler WindowsManager.getInstance().windowsMouseEnter, AddressOf windows_MouseEnter

        'Show window
        MyBase.Show()

        'Load le menu Thérapeute
        Loading.getInstance.forward("Met à jour le menu des thérapeutes") 'Avance de un le chargement
        updateTRPMenu()

        'Load news
        Loading.getInstance.forward("Charge les nouveautés") 'Avance de un le chargement
        loadNewsNotification()

        'Ouverture des agendas
        Loading.getInstance.forward("Ouvre les agendas") 'Avance de un le chargement
        Dim openingAgendas() As String = PreferencesManager.getUserPreferences()("OpeningAgendas").Split(New Char() {vbTab})
        If Not openingAgendas Is Nothing AndAlso PreferencesManager.getUserPreferences()("OpeningAgendas") <> "" Then
            For i As Integer = 0 To openingAgendas.Length - 1
                newAgenda(openingAgendas(i))
                Loading.getInstance.forward("Ouvre les agendas") 'Avance de un le chargement
            Next i
        End If

        'Load instant messaging
        Loading.getInstance.forward("Charge les messages instantannés") 'Avance de un le chargement
        Loading.getInstance.TopMost = False 'Ensuring that FolderText due are not under the Loading window

        loadAlertsWindow()

        Loading.getInstance.TopMost = True

        'Load all other main objects
        Loading.getInstance.forward("Charge les autres objets principaux") 'Avance de un le chargement
        preloadMainObjects()

        'Show window
        'REM Is this useful ??
        Visible = True
    End Sub
#End Region

    Private Sub typeDalerteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeDalerteToolStripMenuItem.Click
        MessageBox.Show("Fonction non disponible actuellement", "Fonction indisponible")
        Exit Sub

        'Droit & Accès
        If currentDroitAcces(105) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Gestion des types d'alerte des dossiers des clients." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myFenFolderAlerts As FolderAlertTypesWin = openUniqueWindow(New FolderAlertTypesWin)
        myFenFolderAlerts.ShowDialog()
    End Sub

    Private Sub typesDeTexteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypesDeTexteToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(106) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Gestion des types de texte des dossiers des clients." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myFenFolderTexteTypes As FolderTexteTypesWin = openUniqueWindow(New FolderTexteTypesWin)
        myFenFolderTexteTypes.ShowDialog()
    End Sub

    Private Sub menurapport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menurapport.Click
        openRapport()
    End Sub

    Private Sub menuWorkHours_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuWorkHours.Click
        Dim myWorkHours As workHours = openUniqueWindow(New workHours())
        myWorkHours.Show()
    End Sub

    Private Sub menuPunch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuPunch.Click
        menuPunch.Checked = Not menuPunch.Checked
        PunchClock.visible = menuPunch.Checked
    End Sub

    Private Sub menuGetMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuGetMail.Click
        Dim myMSGSystem As msgSystem = openUniqueWindow(New msgSystem())
        myMSGSystem.Show()
    End Sub

    Private Sub menuPublipostage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuPublipostage.Click
        'Droit & Accès
        If currentDroitAcces(43) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Publipostage." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        'Vérifie s'il y a un publipostage qui a planté
        If IO.File.Exists(appPath & bar(appPath) & "Data\publipostage.done") Then
            If MessageBox.Show("Le dernier envoi de publipostage a échoué en partie. Désirez-vous reprendre là où l'erreur s'est produite ?", "Reprise d'un publipostage", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = System.Windows.Forms.DialogResult.Yes Then
                MassMailing.restartSending()
                Exit Sub
            Else
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.sav")
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.done")
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.infos")
            End If
        Else
            If IO.File.Exists(appPath & bar(appPath) & "Data\publipostage.sav") Then
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.sav")
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.infos")
            End If
        End If

        Dim myMsgPublipostage As MassMailing = openUniqueWindow(New MassMailing())
        myMsgPublipostage.Show()
    End Sub

    Private Sub analyseDesTextesDesDossiersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalyseDesTextesDesDossiersToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(104) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Analyse des textes des dossiers." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myFenAnalyseFT As FolderTextsAnalysis = openUniqueWindow(New FolderTextsAnalysis())
        myFenAnalyseFT.Show()
    End Sub

    Private Sub menuParamMSG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuParamMSG.Click
        'Droit & Accès
        If currentDroitAcces(44) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Paramètres des comptes courriel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myMSGParam As msgParam = openUniqueWindow(New msgParam())
        myMSGParam.Show()
    End Sub

    Private Sub menuSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSendMail.Click
        Dim myMSGSending As msgSending = openUniqueWindow(New msgSending())
        myMSGSending.Show()
    End Sub

    Private Sub menuAddressBook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAddressBook.Click
        Dim myMsgAddressBook As msgAddressBook = openUniqueWindow(New msgAddressBook())
        myMsgAddressBook.Show()
    End Sub

    Private Sub menuAPropos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAPropos.Click
        Dim myAPropos As About = openUniqueWindow(New About())
        myAPropos.Show()
    End Sub

    Private Sub menuAlert_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAlert.CheckedChanged
        If menuAlert.Checked AndAlso Me.AlertMessages.isClosed AndAlso Me.AlertMessages.Parent IsNot Nothing AndAlso Me.AlertMessages.Parent.Equals(Me) = False Then barMainObjects.showPanel()
    End Sub

    Private Sub menuAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAlert.Click
        menuAlert.Checked = Not menuAlert.Checked
        AlertMessages.visible = menuAlert.Checked
    End Sub

    Private Sub menuNewKP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuNewKP.Click
        addKP("", "", "", "", "", "", "", "", "", "", "", Me)
    End Sub

    Private Sub menuStatusBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuStatusBar.Click
        menuStatusBar.Checked = Not menuStatusBar.Checked
        MainStatusBar.Visible = menuStatusBar.Checked
        ensureControlsGoodPoint() 'To reposition main controls
        Me.Refresh()
    End Sub

    Private Sub ensureControlsGoodPoint()
        For Each CurControl As Control In Me.Controls
            If CurControl.Visible AndAlso TypeOf CurControl Is IMovableObject Then
                Dim curPoint As Point = CurControl.Location
                With CType(CurControl, IMovableObject)
                    .setCoord(.ensureGoodCoord(curPoint.Y, curPoint.X, False))
                End With
            End If
        Next
        For Each CurControl As Control In Me.barMainObjects.mainObjectsPanel.Controls
            If CurControl.Visible AndAlso TypeOf CurControl Is IMovableObject Then
                Dim curPoint As Point = CurControl.Location
                With CType(CurControl, IMovableObject)
                    .setCoord(.ensureGoodCoord(curPoint.Y, curPoint.X, True))
                End With
            End If
        Next
    End Sub

    Private Sub menuToolBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuToolBar.Click
        menuToolBar.Checked = Not menuToolBar.Checked
        BarOutils.Visible = menuToolBar.Checked
        ensureControlsGoodPoint() 'To reposition main controls
        Me.Refresh() 'To reposition the logo
    End Sub

    Public Sub menuadateday_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuadateday.Click
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .DebutDate = .DebutDate.AddDays(1)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Public Sub menuadateweek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuadateweek.Click
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .DebutDate = .DebutDate.AddDays(7)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Public Sub menuaffagenda_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuaffagenda.Click
        newAgenda()
    End Sub

    Public Sub menuchangeuser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuchangeuser.Click
        Software.restart()
    End Sub

    Public Sub menucodedossier_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menucodedossier.Click
        'Droit & Accès
        If currentDroitAcces(28) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Codification dossier." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myCodifications As codifications = openUniqueWindow(New codifications())
        myCodifications.Show()
    End Sub

    Public Sub menumodifmodele_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menumodifmodele.Click
        Dim myModeleNote As ModelOfTextWin = openUniqueWindow(New ModelOfTextWin())
        myModeleNote.Show()
    End Sub

    Public Sub menumois_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menumois.Click
        uncheckedPeriode(Me)
        menumois.Checked = True
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Public Sub menunouveau_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menunouveau.Click
        Comptes.addClient(Me)
    End Sub

    Public Sub menuopen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuopen.Click
        Dim myRecherche As clientSearch = openUniqueWindow(New clientSearch())
        myRecherche.from = menuopen
        myRecherche.Show()
    End Sub

    Public Sub menupdateday_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupdateday.Click
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .DebutDate = .DebutDate.AddDays(-1)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Public Sub menupdateweek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupdateweek.Click
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .DebutDate = .DebutDate.AddDays(-7)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Public Sub menupersonnecle_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupersonnecle.Click
        Dim myKeyPeople As keypeopleSearch = openUniqueWindow(New keypeopleSearch())
        myKeyPeople.selected = False
        myKeyPeople.Show()
    End Sub

    Public Sub menupreferences_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupreferences.Click
        Dim myPreferences As preferencesWin = openUniqueWindow(New preferencesWin())
        myPreferences.Show()
    End Sub

    Public Sub menuquitlogiciel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuquitlogiciel.Click
        Me.Close()
    End Sub

    Public Sub menutherapeute_Click(ByVal sender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim myMenuItem As ToolStripMenuItem
        myMenuItem = CType(sender, ToolStripMenuItem)
        Dim index As Short = CType(myMenuItem.OwnerItem, ToolStripMenuItem).DropDownItems.IndexOf(myMenuItem)

        Dim noTRP As Integer = User.extractNo(myMenuItem.Text)

        'Droit & Accès
        If currentDroitAcces(0) = False And noTRP <> ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To menutherapeute.DropDownItems.Count - 1
            CType(menutherapeute.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next i

        CType(menutherapeute.DropDownItems(index), ToolStripMenuItem).Checked = True
        If formOuvertes.selectedItem IsNot Nothing AndAlso TypeOf formOuvertes.selectedItem.ValueA Is Agenda Then
            With CType(Me.ActiveMdiChild, Agenda)
                For i = 0 To menutherapeute.DropDownItems.Count - 1
                    Try
                        If menutherapeute.DropDownItems(i).Text.EndsWith("(" & .noTRP & ")") Then menutherapeute.DropDownItems(i).Enabled = True
                    Catch ex As Exception
                        Dim count As Integer = 0
                        If menutherapeute IsNot Nothing Then count = menutherapeute.DropDownItems.Count
                        addErrorLog(New Exception("Trying to fix a null pointer exception. menutherapeute is Nothing=" & (menutherapeute Is Nothing) & ",i=" & i & ".menutherapeute.DropDownItems.Count=" & count & ",Me.ActiveMdiChild Is Nothing=" & (Me.ActiveMdiChild Is Nothing), ex))
                    End Try
                Next i
                menutherapeute.DropDownItems(index).Enabled = False
                updateText(Me.ActiveMdiChild, "Agenda : " & menutherapeute.DropDownItems(index).Text)
                .noTRP = noTRP
                .updateStructure(noTRP)
            End With
        End If
    End Sub

    Public Sub menuuser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuuser.Click
        'Droit & Accès
        If currentDroitAcces(45) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Gestion des utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myModifUsers As modifusers = openUniqueWindow(New modifusers())
        myModifUsers.Show()
    End Sub

    Private Sub menuequipement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuequipement.Click
        'Droit & Accès
        If currentDroitAcces(30) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Équipement." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myEquipementWin As EquipmentWin = openUniqueWindow(New EquipmentWin())
        myEquipementWin.Show()
    End Sub

    Private Sub menujour_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles menujour.click
        uncheckedPeriode(Me)

        menujour(index).Checked = True
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Private Sub menusemaine_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles menusemaine.click
        uncheckedPeriode(Me)

        menusemaine(index).Checked = True
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Private Sub menuOpenDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuOpenDB.Click
        'Droit & Accès
        If currentDroitAcces(1) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim newDB As DB = openUniqueWindow(New DB())
        newDB.Show()
    End Sub

    Private Sub menuFileType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuFileType.Click
        'Droit & Accès
        If currentDroitAcces(7) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim newTypeDB As TypeDB = openUniqueWindow(New TypeDB())
        newTypeDB.Show()
    End Sub

    Private Sub menuRechercherDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRechercherDB.Click
        'Droit & Accès
        If currentDroitAcces(6) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB())
        mySearchDB.Show()
    End Sub

    Private Sub menupdatemonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menupdatemonth.Click
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .DebutDate = .DebutDate.AddMonths(-1)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Private Sub menuadatemonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuadatemonth.Click
        If formOuvertes.items.Count > 0 Then
            With CType(WindowsManager.getInstance.selected, Agenda)
                .DebutDate = .DebutDate.AddMonths(1)
                .updateStructure(.noTRP)
            End With
        End If
    End Sub

    Private Sub menuQueueList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuQueueList.Click
        openQL()
    End Sub

    Private Sub menuClinique_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClinique.Click
        'Droit & Accès
        If currentDroitAcces(27) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Clinique." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myClinique As Clinic = openUniqueWindow(New Clinic())
        myClinique.Show()
    End Sub

    Private Sub menuRVFutur_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRVFutur.CheckedChanged
        If menuRVFutur.Checked And Me.RVMenu.Parent.Equals(Me) = False And Me.RVMenu.isClosed Then barMainObjects.showPanel()
    End Sub

    Private Sub menuRVFutur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRVFutur.Click
        menuRVFutur.Checked = Not menuRVFutur.Checked
        RVMenu.visible = menuRVFutur.Checked
    End Sub

    Private Sub menuNewRV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuNewRV.Click
        openNewRV()
    End Sub
#End Region

#Region "MainWin Events"

    Private Sub mainWin_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Me.formOuvertes.items.Count = 0 OrElse Me.formOuvertes.selected = -1 Then Exit Sub

        'Resélectionne le bonne item lorsque le focus retourne d'une fenêtre en mode modal
        WindowsManager.getInstance.selected(Me.ActiveMdiChild)
    End Sub

    Private Sub mainWin_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MdiChildActivate
        Dim a As String  'Variable qui une fois assignée permet de corriger 
        a = "a"          'un bug de retour de focus des fenêtres
        Try
            WindowsManager.getInstance.selected(Me.ActiveMdiChild)
            If barMainObjects.visible Then
                If Me.ActiveMdiChild IsNot Nothing AndAlso Me.ActiveMdiChild.MdiParent IsNot Nothing Then Me.barMainObjects.hideBar()
            End If
        Catch ex As NullReferenceException
            'Objects are not created
        End Try
    End Sub

    Private Sub main_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        For Each ctl As Control In Me.Controls
            If TypeOf (ctl) Is MdiClient Then
                AddHandler ctl.Paint, AddressOf mainWin_Paint
                Exit For
            End If
        Next

        Dim logsUserPath As String = appPath & bar(appPath) & "Users\Logs\" & ConnectionsManager.currentUser
        IO.Directory.CreateDirectory(logsUserPath)
        logsUserPath &= "\" & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.YYYYMMDD, , "-") & ".log"
        If IO.File.Exists(logsUserPath) Then StatusHistory.Text = IO.File.ReadAllText(logsUserPath)

        Me.mainWin_Resize(Me, eventArgs.Empty)
        quitToChange = False
        If currentUserName = "Administrateur" Then AdminTasksToolStripMenuItem.Visible = True

        'REM for testing with two clinica
        Me.Size = New Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height / 2)
    End Sub

    Private Sub main_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        Try
            Dim connectedPath As String = appPath & bar(appPath) & "Users\Connected"
            Dim userConnectionFile As String = connectedPath & "\" & ConnectionsManager.currentUser & "-" & Environment.MachineName
            If IO.File.Exists(userConnectionFile) = True Then IO.File.Delete(userConnectionFile)

            'Si aucun utilisateur de connecté, fait le ménage
            'TODO : Replace by a call to the server to know how many people connected and clean also connected dir
            If hddListing(connectedPath, False, True).Count = 0 Then
                deltree(appPath & bar(appPath) & "Data\UniqueNoQueue")
                ensureGoodPath(appPath & bar(appPath) & "Data\UniqueNoQueue")
                deltree(appPath & bar(appPath) & "Data\Queues")
                ensureGoodPath(appPath & bar(appPath) & "Data\Queues")
                DBLinker.getInstance.delDB("FacturesNumbers", , , , , False, , False)
                deltree(appPath & bar(appPath) & "Data\LockSecteur")
                IO.Directory.CreateDirectory(appPath & bar(appPath) & "Data\LockSecteur")
            End If

            If quitToChange = False Then
                Software.doEndProcess()
                End
            End If
        Catch ex As Exception
            addErrorLog(ex)
            MessageBox.Show(ex.Message, "Erreur inconnue")
        End Try
    End Sub

    Private Sub main_Closing(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If quitToChange = False Then
            If closingWin() = False Then
                eventArgs.Cancel = True
            End If
        End If
    End Sub

    Private endTimeForFixStatusPrincipal As DateTime = LIMIT_DATE

    Public Sub mainWin_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'Fix a problem with the status bar : Size of principal part is not good
        If adjustedStatusBar = False Then
            mainWin_Resize(sender, EventArgs.Empty)
            If endTimeForFixStatusPrincipal = LIMIT_DATE Then endTimeForFixStatusPrincipal = Date.Now.AddMilliseconds(750)
            If Date.Now > endTimeForFixStatusPrincipal Then adjustedStatusBar = True
        End If

        Try
            'If BarMainObjects.Visible And (TypeOf (sender) Is Windows.Forms.MdiClient Or TypeOf (sender) Is Windows.Forms.Form) Then
            '    If TypeOf (sender) Is Windows.Forms.MdiClient OrElse CType(sender, Form).MdiParent IsNot Nothing Then Me.BarMainObjects.HideBar()
            'End If
            If barMainObjects.visible AndAlso barMainObjects.isPanelVisible = False Then
                Me.barMainObjects.hideBar()
            End If
        Catch ex As NullReferenceException
            'Objects are not created
        End Try
    End Sub
#End Region

#Region "Fonctions publiques"
    Public Sub lockItems(ByVal trueFalse As Boolean)
        _IsLocked = trueFalse
        BarOutils.Enabled = Not trueFalse
        Dim i As Integer
        With BarOutils.Items
            For i = 0 To .Count - 1
                Try
                    .Item(i).Enabled = Not trueFalse
                Catch ex As Exception
                    'No Enable property
                End Try
            Next i
        End With
        Me.barMainObjects.Enabled = Not trueFalse
        Me.BlockingMouseLabel.Dock = DockStyle.Fill
        Me.BlockingMouseLabel.Visible = trueFalse
        Me.BlockingMouseLabel.BringToFront()
        lockMenu(trueFalse)
    End Sub

    Public Sub lockMenu(ByVal trueFalse As Boolean)
        Try 'Line 0
            If Me Is Nothing Then Exit Sub

            Dim i As Integer
            With Me.MainMenuStrip.Items
                For i = 0 To .Count - 1
                    .Item(i).Enabled = Not trueFalse
                Next i
            End With
        Catch ex As Exception
            addErrorLog(New Exception("Line 10", ex))
        End Try
    End Sub

    Public Function closingWin() As Boolean
        If MassMailing.sendingThread IsNot Nothing Then
            MessageBox.Show("Veuillez attendre que le processus d'envoi du publipostage soit terminé avant de fermer le logiciel.", "Processus en cours")
            quitToChange = False
            Return False
        End If

        Dim i As Integer
        Dim filesToDel() As String
        Dim rep As DialogResult = System.Windows.Forms.DialogResult.No
        If quitQuestion = True Then
            If quitToChange = False Then
                rep = MessageBox.Show("Voulez-vous vraiment fermer le programme ?", "Fermeture du programme", MessageBoxButtons.YesNo)
            Else
                rep = MessageBox.Show("Voulez-vous vraiment changer d'utilisateur ?", "Changement d'utilisateur", MessageBoxButtons.YesNo)
            End If
        Else
            rep = System.Windows.Forms.DialogResult.Yes
        End If

        If rep = System.Windows.Forms.DialogResult.Yes Then
            'Ensure notes are saved
            Dim curAlert As Alert
            For Each curAlert In AlertsManager.getInstance.getItemables
                If TypeOf curAlert Is AlertOfPersoNote Then CType(curAlert, AlertOfPersoNote).persoNote.saveNote()
            Next

            'Save Settings
            If UsersManager.currentUser IsNot Nothing Then
                Dim curSettings As UserSettings = UsersManager.currentUser.settings
                curSettings.newRV = RVMenu.Top & "§" & RVMenu.Left & "§" & RVMenu.blockMove & "§" & RVMenu.IsSwitchedToToolbar & "§" & RVMenu.AskExtraQuestion.Checked
                curSettings.instantMSG = AlertMessages.Top & "§" & AlertMessages.Left & "§" & AlertMessages.blockMove & "§" & AlertMessages.IsSwitchedToToolbar
                curSettings.mainWin = menuStatusBar.Checked & "§" & menuToolBar.Checked & "§" & menuRVFutur.Checked & "§" & menuAlert.Checked & "§" & menuPunch.Checked
                curSettings.punch = PunchClock.Top & "§" & PunchClock.Left & "§" & PunchClock.blockMove & "§" & RVMenu.IsSwitchedToToolbar
                Dim persoSettings As String = AlertMessages.getNotesSettings()
                curSettings.persoNotes = persoSettings
                curSettings.saveData()
            End If

            'Save Logs
            Dim logSeparator As String = "-------------------------------------------------------------"
            Dim logsUserPath As String = appPath & bar(appPath) & "Users\Logs\" & ConnectionsManager.currentUser
            IO.Directory.CreateDirectory(logsUserPath)
            Dim sCurLog() As String = StatusHistory.Text.Split(New String() {logSeparator}, StringSplitOptions.RemoveEmptyEntries)
            Dim curLog As String = sCurLog(sCurLog.Length - 1) & vbCrLf & logSeparator
            IO.File.AppendAllText(logsUserPath & "\" & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.YYYYMMDD, , "-") & ".log", curLog)

            'Set all news has read, if the window has been opened once
            If Software.isNewNewsSeen Then DBLinker.getInstance.updateDB("SoftwareNewsUsers", "Viewed=1", "NoUser", ConnectionsManager.currentUser, False)

            'Unlock used sectors not closed
            For Each curLock As String In Fenetres.sectorsLocked
                If IO.File.Exists(appPath & bar(appPath) & curLock) AndAlso Not curLock = String.Empty Then IO.File.Delete(appPath & bar(appPath) & curLock)
            Next

            'Delete Temporary Files
            deltree(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser)
            Do
                'Attente
            Loop While System.Windows.Forms.Cursor.Current Is System.Windows.Forms.Cursors.WaitCursor

            Return True
        Else
            quitToChange = False
            Return False
        End If
    End Function

    Public Sub newAgenda(Optional ByRef noTRP As Integer = 0)
        If noTRP = 0 Then
            Dim users() As User = UsersManager.getInstance.getUsers(False, True).ToArray
            If users.Length = 0 Then
                MessageBox.Show("Il n'existe aucun thérapeute.Veuillez changer un utilisateur en thérapeute via son type d'utilisateur ou créer un nouvel utilisateur.", "Aucun thérapeute")
                Exit Sub
            End If

            Dim myChoice As User = UsersManager.getInstance.chooseUser(False, True, , PreferencesManager.getUserPreferences()("DefaultTRP"), , False)
            If myChoice Is Nothing Then Exit Sub

            noTRP = myChoice.noUser
        End If
        'Removed because anyhow, if selecting an existing one, it will show up the window
        'If FindFreeAgenda() = 0 Then MessageBox.Show("Tous les agendas ont déjà été chargé. Veuillez sélectionner à partir de la liste des fenêtres", "Agenda") : Exit Sub

        CommandsHolder.getInstance.newAgenda.execute(noTRP)
    End Sub

    Private Sub newAgendaEnability(ByVal enabled As Boolean)
        If _IsLocked = False Then
            menuaffagenda.Enabled = enabled
            menuagenda.Enabled = enabled
        End If
    End Sub

    Public Sub updateTRPMenu()
        'REM update This method shall not exist

        If Me.Visible = False Then Exit Sub
        Dim i, j As Integer

        With Me
            'Clean handlers & menus
            For i = 0 To .menutherapeute.DropDownItems.Count - 1
                RemoveHandler .menutherapeute.DropDownItems(i).Click, AddressOf Me.menutherapeute_Click
            Next i
            .menutherapeute.DropDownItems.Clear()
            'Load le menu Thérapeute
            Dim myUsers As Generic.List(Of User) = UsersManager.getInstance.getUsers(False, True)
            For Each curUser As User In myUsers
                Dim myMenuItem As ToolStripMenuItem
                myMenuItem = New ToolStripMenuItem(curUser.toString())
                AddHandler myMenuItem.Click, AddressOf Me.menutherapeute_Click

                .menutherapeute.DropDownItems.Add(myMenuItem)

                Dim myTitle As String = "Agenda : " & curUser.toString()
                Dim mySingleWindows As Generic.List(Of SingleWindow) = WindowsManager.getInstance.findWindowsByName("Agenda")
                If mySingleWindows.Count <> 0 Then
                    For j = 0 To mySingleWindows.Count - 1
                        Dim n As Integer = 0
                        With CType(mySingleWindows(j), Agenda)
                            If .noTRP = curUser.noUser And .Text <> myTitle Then updateText(mySingleWindows(j), myTitle)
                        End With
                    Next j
                End If

                If myMainWin.formOuvertes.findStringExact("Agenda : " & curUser.toString()) >= 0 Then myMenuItem.Enabled = False
                If PreferencesManager.getUserPreferences()("DefaultTRP") = curUser.toString() Then myMenuItem.Checked = True
            Next
            If PreferencesManager.getUserPreferences()("DefaultTRP").ToString = "" AndAlso .menutherapeute.DropDownItems.Count <> 0 Then CType(.menutherapeute.DropDownItems(0), ToolStripMenuItem).Checked = True

            If .menutherapeute.DropDownItems.Count = 0 Then .menutherapeute.DropDownItems.Add("Aucun")
        End With
    End Sub
#End Region

#Region "Events des objets de la fenêtre principale autres que les toolbars et les menus"
    Private Sub objectsMovable_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles RVMenu.move, AlertMessages.move
        If CType(sender, IMovableObject).blockObjectInArea = False Then CType(sender, IMovableObject).blockObjectInArea = True
    End Sub
#End Region

#Region "Events des objects des Toolbars"
    Private Sub formOuvertes_SelectedChange() Handles formOuvertes.selectedChange
        If Me.formOuvertes.selectedItem Is Nothing OrElse Not TypeOf Me.formOuvertes.selectedItem.ValueA Is SingleWindow Then Exit Sub

        changeMenus()

        'Keep in memory curWindow + lastWindow
        If Me.lastTwoWindows.Count = 2 Then Me.lastTwoWindows.RemoveAt(1)
        Me.lastTwoWindows.Insert(0, Me.formOuvertes.selectedItem.ValueA)
    End Sub

    Private Sub statusTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusTimer.Tick
        StatusBarPrincipal.Text = ""
        'StatutActivated = False
        StatusBarPrincipal.BackColor = SystemColors.Control
        StatusTimer.Stop()
    End Sub

    Private Sub dateTimeTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimeTimer.Tick
        If StatusBarDate Is Nothing Then Exit Sub

        dateFormatter.Text = DateFormat.getTextDate(Date.Today, DateFormat.TextDateOptions.FullDayMonthNames)
        StatusBarDate.Text = dateFormatter.Text
        StatusBarTime.Text = DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.FullTime)
    End Sub

    Private Sub mainStatusBar_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MainStatusBar.MouseDoubleClick
        If MainStatusBar.GetItemAt(e.X, e.Y) Is StatusBarPrincipal Then
            StatusHistory.Width = StatusBarPrincipal.Width
            StatusHistory.Top = MainStatusBar.Top - StatusHistory.Height + MainStatusBar.Height
            StatusHistory.Visible = Not StatusHistory.Visible
            StatusHistory.SelectionStart = StatusHistory.Text.Length
            StatusHistory.ScrollToCaret()
        ElseIf MainStatusBar.GetItemAt(e.X, e.Y) Is StatusBarNews Then
            Dim myAPropos As About = openUniqueWindow(New About)
            myAPropos.Show()
            myAPropos.TabControl1.SelectedIndex = 1
        ElseIf MainStatusBar.GetItemAt(e.X, e.Y) Is StatusBarDate Or MainStatusBar.GetItemAt(e.X, e.Y) Is StatusBarTime Then
            Dim myDateChoice As New DateChoice()
            myDateChoice.texteEnHaut.Text = "Calendrier :"
            myDateChoice.choose(firstUsageDate.Year - 10, Date.Now.Year + 10, False, , , , , True)
        End If
    End Sub

    Private Sub statusHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusHistory.DoubleClick
        StatusHistory.Visible = False
    End Sub
#End Region

    Private Sub recountDBTypesCounterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecountDBTypesCounterToolStripMenuItem.Click
        'DBManager.GetInstance.RecountTypesCounter()
        MessageBox.Show("Useless")
    End Sub

    Private Sub typesDutilisateurToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypesDutilisateurToolStripMenuItem.Click
        openTypesUser(Me)
    End Sub

    Private Sub menuFinDeMois_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuFinDeMois.Click
        openFinDeMois()
    End Sub

    Private Sub ouvrirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OuvrirToolStripMenuItem.Click
        Dim myViewModifBills As viewmodifBills = openUniqueWindow(New viewmodifBills())
        myViewModifBills.Show()
    End Sub

    Private Sub nouvelleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NouvelleToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(84) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Nouvelle facture." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myAddBill As addBill = openUniqueWindow(New addBill())
        myAddBill.Show()
        myAddBill.mode = addBill.Modes.All
    End Sub

    Private Sub everythingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EverythingToolStripMenuItem.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer toutes les données de Clinica ?", "Suppression", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        'Suppression des dossiers et fichiers
        deltree(appPath & bar(appPath) & "Clients")
        deltree(appPath & bar(appPath) & "Users")
        deltree(appPath & bar(appPath) & "KP")
        deltree(appPath & bar(appPath) & "DB")
        deltree(appPath & bar(appPath) & "Data\Mails")
        deltree(appPath & bar(appPath) & "Data\Lists")
        deltree(appPath & bar(appPath) & "Data\LockSecteur")
        deltree(appPath & bar(appPath) & "Data\Queues")
        deltree(appPath & bar(appPath) & "Data\UniqueNoQueue")

        'Vidage des tables de la BD
        DBLinker.getInstance.executeStoredProcedure("cleanAllTables", Nothing)
        DBLinker.getInstance.executeStoredProcedure("createBaseData", Nothing)

        'Recréation des dossiers nécessaires
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "Clients")
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "KP")
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "Users\Connected")
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "Data\Lists")
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "Data\LockSecteur")
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "Data\Mails")
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "DB")

        xCopy(appPath & bar(appPath) & "Data\DefLists", appPath & bar(appPath) & "Data\Lists")

        If MessageBox.Show("All data have been destroyed. Do you want to restart the software ? Otherwise it will close.", "Data reset", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Software.restart(False)
        Else
            Software.doEndProcess()
            End
        End If
    End Sub

    Private Sub mettreÀJourClinicaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MettreÀJourClinicaToolStripMenuItem.Click
        'Droit & Acces
        If currentDroitAcces(86) = False Then
            MessageBox.Show("Vous n'avez pas le droit de mettre à jour le logiciel")
            Exit Sub
        End If

        If Me.formOuvertes.items.Count <> 0 Then MessageBox.Show("Veuillez fermer toutes les fenêtres dans Clinica avant de faire la mise à jour", "Fenêtre(s) ouverte(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

        If MessageBox.Show("Êtes-vous sûr de vouloir mettre à jour Clinica ?", "Confirmation de mise à jour", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Software.checkExternalUpdates()
    End Sub

    Private Sub selectedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedToolStripMenuItem.Click
        openDelTables()
    End Sub

    Private Sub mainWin_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Try
            e.Graphics.Clear(SystemColors.AppWorkspace)
            Dim myLogo As Image = DrawingManager.getInstance.getImage("Logo.gif")
            Dim myHeight As Integer = maximizedDisplaySize.Height
            Dim myTop As Integer = 0
            If BarOutils.Visible Then
                myHeight -= BarOutils.Height
                myTop += BarOutils.Height
            End If
            If MainStatusBar IsNot Nothing AndAlso MainStatusBar.Visible Then
                myHeight -= MainStatusBar.Height
            End If
            e.Graphics.DrawImage(myLogo, New Point((Me.Width - myLogo.Width) / 2, (myHeight - myLogo.Height * 1.5) / 2))
            myLogo = Nothing
        Catch ex As Exception
            'If background can't print now, it will next time
        End Try
    End Sub

    Private Function getDesiredPrincipalStatusWidth() As Integer
        Return Me.ClientRectangle.Width - StatusBarDate.Width - StatusBarTime.Width - If(Me.StatusBarNews.Visible = True, Me.StatusBarNews.Width, 0) - If(Me.StatusBarPrint.Visible = True, Me.StatusBarPrint.Width, 0)
    End Function

    Private Sub mainWin_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.StatusBarPrincipal IsNot Nothing Then
            Me.StatusBarPrincipal.Width = getDesiredPrincipalStatusWidth()
            Me.StatusHistory.Width = Me.StatusBarPrincipal.Width
        End If
        If MainStatusBar IsNot Nothing Then MainStatusBar.PerformLayout()
        If Me.formOuvertes IsNot Nothing Then
            Me.formOuvertes.MaximumSize = New Size(Me.ClientSize.Width - Me.formOuvertes.Left - bar_spacing - spacing_mdi_window_buttons, Me.formOuvertes.Height)
            Me.formOuvertes.adjustMySize()
        End If
        Me.Refresh()
    End Sub

    Private Sub menuMessagerie_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuMessagerie.DropDownOpening
        menuPublipostage.Enabled = MassMailing.sendingThread Is Nothing
    End Sub

    Private Sub modifierVotreMotDePasseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifierVotreMotDePasseToolStripMenuItem.Click
        Dim curUser As User = UsersManager.currentUser
        curUser.changePassword()
    End Sub

    Private Sub utilisateursConnectésToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UtilisateursConnectésToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(93) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myConnectedUsers As connectedUsers = openUniqueWindow(New connectedUsers)
        myConnectedUsers.Show()
    End Sub


    Private Sub changeMenus()
        If formOuvertes.selectedItem IsNot Nothing AndAlso TypeOf formOuvertes.selectedItem.ValueA Is Agenda Then
            With CType(formOuvertes.selectedItem.ValueA, Agenda)
                menuperiode.Enabled = True
                menutherapeute.Enabled = True
                menuchangedate.Enabled = True
                uncheckedPeriode(Me, .getNbDays)
                'CType(.mnuperiode, ToolStripMenuItem).Checked = True
                For i As Integer = 0 To menutherapeute.DropDownItems.Count - 1
                    CType(menutherapeute.DropDownItems(i), ToolStripMenuItem).Checked = False
                    If CType(menutherapeute.DropDownItems(i), ToolStripMenuItem).Text.EndsWith("(" & .noTRP & ")") Then CType(menutherapeute.DropDownItems(i), ToolStripMenuItem).Checked = True
                Next i
                menuAgendaContextuel.Enabled = True
                AjusterToolStripMenuItem.Enabled = True

                'Enable shortcuts
                menupdateday.ShortcutKeys = Keys.F2
                menuadateday.ShortcutKeys = Keys.F3
                menupdateweek.ShortcutKeys = Keys.F4
                menuadateweek.ShortcutKeys = Keys.F5
                menupdatemonth.ShortcutKeys = Keys.F6
                menuadatemonth.ShortcutKeys = Keys.F7
            End With
        Else
            menuperiode.Enabled = False
            menutherapeute.Enabled = False
            menuchangedate.Enabled = False
            menuAgendaContextuel.Enabled = False
            AjusterToolStripMenuItem.Enabled = False

            'Disable shortcuts
            menupdateday.ShortcutKeys = Nothing
            menuadateday.ShortcutKeys = Nothing
            menupdateweek.ShortcutKeys = Nothing
            menuadateweek.ShortcutKeys = Nothing
            menupdatemonth.ShortcutKeys = Nothing
            menuadatemonth.ShortcutKeys = Nothing
        End If
    End Sub

    Private Sub formOuvertes_SelectedChangeByUser() Handles formOuvertes.selectedChangeByUser
        If formOuvertes.listCount = 0 Then Exit Sub

        WindowsManager.getInstance.selected(CType(formOuvertes.selectedItem.ValueA, SingleWindow))

        changeMenus()

        If formOuvertes.selectedItem Is Nothing Then Exit Sub

        With CType(formOuvertes.selectedItem.ValueA, SingleWindow)
            If justAppliedFocus = False And .Visible = True Then
                .Focus()
                If .WindowState = FormWindowState.Minimized Then .WindowState = FormWindowState.Normal
            End If

            If .MdiParent IsNot Nothing Then
                'REM Tests to scroll form into view.. nothing does it.
                Dim tmpPT As Point = myMainWin.ScrollToControl(formOuvertes.selectedItem.ValueA)
                'tmpPT.Y = Math.Abs(tmpPT.Y)
                myMainWin.AutoScrollOffset = tmpPT
                myMainWin.AutoScrollPosition = tmpPT
                'MyMainWin.VerticalScroll.Value = Math.Abs(tmpPT.Y) / MyMainWin.ClientRectangle.Height
                'MyMainWin.ScrollControlIntoView(.GetForm)

                myMainWin.barMainObjects.hideBar()
            End If
        End With

        'JustAppliedFocus = False
    End Sub

    Private Sub menuSpecialDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSpecialDates.Click
        'Droit & Accès
        If currentDroitAcces(103) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les journées spéciales." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim mySpecialDates As specialDates = openUniqueWindow(New specialDates)
        mySpecialDates.Show()
    End Sub

    Private Sub lesAgendasHorizontalementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LesAgendasHorizontalementToolStripMenuItem.Click
        If Me.formOuvertes.selected = -1 Then Exit Sub

        Dim curTop As Integer = 0
        For Each curItem As CI.Controls.IListItem In Me.formOuvertes.items
            Dim curWin As SingleWindow = curItem.ValueA
            If curWin.Name.StartsWith("Agenda") Then
                With curWin
                    .Top = curTop
                    .Height = .MinimumSize.Height
                    If curTop = 0 Then curTop = .Height Else curTop = 0
                End With
            End If
        Next

        With CType(Me.formOuvertes.selectedItem.ValueA, SingleWindow)
            .Top = 0
        End With

        If Me.lastTwoWindows.Count = 2 AndAlso Me.lastTwoWindows(1).Name.StartsWith("Agenda") Then
            lastTwoWindows(1).Top = lastTwoWindows(1).Height
        End If

    End Sub

    Private Sub lagendaEnCoursEnPleineÉcranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LagendaEnCoursEnPleineÉcranToolStripMenuItem.Click
        If Me.formOuvertes.selected = -1 Then Exit Sub

        With CType(Me.formOuvertes.selectedItem.ValueA, SingleWindow)
            .Height = Me.mdiChildRectangle.Height - 6
            .Top = 0
        End With
    End Sub

    Public Sub mdiClosed(ByVal window As SingleWindow)
        Dim n As Integer = formOuvertes.findValue(window)
        formOuvertes.remove(n)
        If formOuvertes.items.Count = 0 Then changeMenus()
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return 0
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function

    Public Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
        Select Case dataReceived.function
            Case "ALLTRPMenu"
                updatingALLTRPMenu(False)

            Case "Close"
                Dim isAddressToMe As Boolean = dataReceived.params(0) = ConnectionsManager.currentUser OrElse dataReceived.params(0) = 0
                Dim isTheSameComputer As Boolean = dataReceived.params.Length = 1 OrElse dataReceived.params(1) = Environment.MachineName
                If isAddressToMe AndAlso isTheSameComputer AndAlso WindowsManager.getInstance.saveAllWindows() Then
                    myMainWin.SetQuitToChange(False) = False
                    myMainWin.closingWin()
                    myMainWin.Close()
                End If

            Case "Message"
                If dataReceived.params(0) = ConnectionsManager.currentUser Then
                    MessageBox.Show(dataReceived.params(2), "Message de " & UsersManager.getInstance.getUser(dataReceived.params(1)).toString())
                End If

            Case "Punch"
                updatePunch(dataReceived.params(0), , False)
        End Select
    End Sub

    Private Sub menuGenerateReportsInGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuGenerateReportsInGroupToolStripMenuItem.Click
        Dim myReportGenerationInGroup As ReportGenerationInGroup = openUniqueWindow(New ReportGenerationInGroup())
        myReportGenerationInGroup.Show()
    End Sub

    Private Sub menuReportsCreator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuReportsCreator.Click
        Dim newRapportCreator As New ReportCreator
        newRapportCreator.Show()
    End Sub

    Private Sub menuServerTasks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuServerTasks.Click
        'Droit & Accès
        If currentDroitAcces(108) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à la gestion des tâches du serveur." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim newRemoteTasksManagerWin As RemoteTasksManagerWin = openUniqueWindow(New RemoteTasksManagerWin())
        newRemoteTasksManagerWin.Show()
    End Sub

    Private Sub NouveauDossierToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NouveauDossierToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(16) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Ajout d'un rendez-vous." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myAddFolder As addvisite = openUniqueWindow(New addvisite(addvisite.UsageModes.AddingFolder), "Ajout d'un dossier client")

        myAddFolder.Show()
        myAddFolder.Select()
    End Sub

    Private Sub ConfigurationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigurationsToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(109) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Configurations du poste." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Base.ConfigurationsManager.getInstance().showConfigs()
    End Sub

    Private Sub StatusBarPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusBarPrint.Click
        printingJobs.visible = True
        printingJobs.IsSwitchedToToolbar = False
    End Sub

    Private Sub printJobsChanged()
        printJobsChanged(Nothing, Nothing)
    End Sub

    Private Sub printJobsChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandler(AddressOf printJobsChanged))
            Exit Sub
        End If

        StatusBarPrint.Visible = PrintingHelper.jobsCount <> 0
        Me.StatusBarPrincipal.Width = getDesiredPrincipalStatusWidth()
        Me.StatusHistory.Width = Me.StatusBarPrincipal.Width
    End Sub

    Private Sub ExporterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExporterToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(110) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Exportation de dossiers." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myExportWindow As FolderExportation = openUniqueWindow(New FolderExportation())
        myExportWindow.Show()
    End Sub

    Private Sub DésactivationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DésactivationToolStripMenuItem.Click
        'Droit & Accès
        If currentDroitAcces(111) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Désactivation de dossiers en lot." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If
        'Droit & Accès
        If currentDroitAcces(55) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel, car vous n'avez le droit de changer le statut d'un dossier." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myClosingWindow As FolderClosing = openUniqueWindow(New FolderClosing())
        myClosingWindow.Show()
    End Sub
End Class
