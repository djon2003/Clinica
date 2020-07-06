
Friend Class msgSystem
    Inherits SingleWindow


#Region "Controls definition"
    Friend WithEvents menuRead As System.Windows.Forms.MenuItem
    Friend WithEvents menuUnread As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuRepondre As System.Windows.Forms.MenuItem
    Friend WithEvents menuRepondreAll As System.Windows.Forms.MenuItem
    Friend WithEvents menuViewSource As System.Windows.Forms.MenuItem
    Friend WithEvents Toolbar As System.Windows.Forms.ToolStrip
    Friend WithEvents tlbBtnGetMessages As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tlbProgess As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tlbStatus As System.Windows.Forms.ToolStripLabel
    Friend WithEvents hidingStatusTimer As System.Windows.Forms.Timer
    Friend WithEvents menuTransferMSG As System.Windows.Forms.MenuItem
    Friend WithEvents IsFileAttached As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents IsMailRead As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents IsCompteLie As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents AffDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BothTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BothFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subject As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoMail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents noMessages As System.Windows.Forms.Label
#End Region

#Region " Windows Form Designer generated code "

    Private starting As Date = Date.Now()

    Private notReadMessageLineFormat As Font
    Private notReadMessageLineStyle As DataGridViewCellStyle

    Private emptyImage As Image = DrawingManager.getInstance.getImage("point.gif")
    Private clientAcountImage As Image = DrawingManager.getInstance.getIcon("client16.ico").ToBitmap
    Private fileAttachedImage As Image = DrawingManager.getInstance.getImage("fileattached.png")
    Private newMailImage As Image = DrawingManager.getInstance.getImage("new-mail.png")

    Private curFolderMails As New Generic.Dictionary(Of Integer, Mail)

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin
        Me.ToolTip1.ShowAlways = True
        notReadMessageLineStyle = MessagesList.DefaultCellStyle.Clone()
        notReadMessageLineFormat = New Font(MessagesList.Font, FontStyle.Bold)
        notReadMessageLineStyle.Font = notReadMessageLineFormat
        MessagesList.VirtualMode = False
        MessagesList.ReadOnly = True
        
        'Chargement des comptes courriels existants
        Dim i As Short = 0

        For Each curAccount As MailAccount In MailsManager.getInstance.getMailAccounts
            tlbBtnGetMessages.DropDownItems.Add(i & "- " & curAccount.accountName & " (" & curAccount.sendingName & " <" & curAccount.email & ">)", Nothing, New EventHandler(AddressOf Me.accountsMenuClick))
            i += 1
        Next

        MessageInHTML.editorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")

        'Apply user settings
        Dim setting As String = UsersManager.currentUser.settings.mailSystem
        If setting <> "" Then
            Dim mySettings() As String = setting.Split(New Char() {"§"})
            Me.Height = mySettings(0)
            Me.Width = mySettings(1)
            Splitter1.SplitPosition = mySettings(2)
            Splitter2.SplitPosition = mySettings(3)
            Me.selectedMessage = mySettings(4)
            REM TO REACTIVATE
            MessagesList.Sort(MessagesList.Columns(mySettings(5)), IIf(mySettings(6) = "D", 1, 0))
        End If

        'Images
        Me.tlbBtnGetMessages.Image = DrawingManager.resizeImage(DrawingManager.getInstance.getImage("mailbox.gif"), 16, 16)
        Me.StatusBarPanel3.Icon = DrawingManager.getInstance.getIcon("FolderClosed.ico")
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.resizeImage(DrawingManager.getInstance.getImage("mailbox.gif"), 16, 16))
        'MailFolders.ImageList = New ImageList()
        Try
            With MailFolders.ImageList.Images
                .Add(DrawingManager.getInstance.getImage("user.gif"))
            End With
        Catch
        End Try
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            If Not notReadMessageLineFormat Is Nothing Then notReadMessageLineFormat.Dispose()
            'For Each curAccount As MailAccount In MailsManager.getInstance.getMailAccounts()
            '    RemoveHandler curAccount.pop3Message, AddressOf popMessage
            '    RemoveHandler curAccount.pop3Downloading, AddressOf chargementEnCours
            'Next
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents menuDossiers As System.Windows.Forms.ContextMenu
    Friend WithEvents menuAddFolder As System.Windows.Forms.MenuItem
    Friend WithEvents menuDelFolder As System.Windows.Forms.MenuItem
    Friend WithEvents menuMessages As System.Windows.Forms.ContextMenu
    Friend WithEvents menuAffichage As System.Windows.Forms.MenuItem
    Friend WithEvents menuUpSorting As System.Windows.Forms.MenuItem
    Friend WithEvents menuDownSorting As System.Windows.Forms.MenuItem
    Friend WithEvents menuSeeMSG As System.Windows.Forms.MenuItem
    Friend WithEvents menuCouper As System.Windows.Forms.MenuItem
    Friend WithEvents menuCopier As System.Windows.Forms.MenuItem
    Friend WithEvents menuColler As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuAddFolder2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents menuDelMSG As System.Windows.Forms.MenuItem
    Friend WithEvents LeftPanel As System.Windows.Forms.Panel
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents MailFolders As Clinica.TreeViewPlus
    Friend WithEvents RightPart As System.Windows.Forms.Panel
    Friend WithEvents RightBottomPart As System.Windows.Forms.Panel
    Friend WithEvents RigthTopPart As System.Windows.Forms.Panel
    Friend WithEvents MessagesList As DataGridPlus
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents TitleFolder As System.Windows.Forms.StatusBar
    Friend WithEvents TitleMessage As System.Windows.Forms.StatusBar
    Friend WithEvents TitleMessages As System.Windows.Forms.StatusBar
    Friend WithEvents MessageInRTFFormat As Clinica.TextControl
    Friend WithEvents MessageInHTML As WebTextControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(msgSystem))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.menuDossiers = New System.Windows.Forms.ContextMenu
        Me.menuAddFolder = New System.Windows.Forms.MenuItem
        Me.menuDelFolder = New System.Windows.Forms.MenuItem
        Me.menuMessages = New System.Windows.Forms.ContextMenu
        Me.menuAffichage = New System.Windows.Forms.MenuItem
        Me.menuUpSorting = New System.Windows.Forms.MenuItem
        Me.menuDownSorting = New System.Windows.Forms.MenuItem
        Me.menuRepondre = New System.Windows.Forms.MenuItem
        Me.menuRepondreAll = New System.Windows.Forms.MenuItem
        Me.menuDelMSG = New System.Windows.Forms.MenuItem
        Me.menuTransferMSG = New System.Windows.Forms.MenuItem
        Me.menuViewSource = New System.Windows.Forms.MenuItem
        Me.menuSeeMSG = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.menuRead = New System.Windows.Forms.MenuItem
        Me.menuUnread = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.menuCouper = New System.Windows.Forms.MenuItem
        Me.menuCopier = New System.Windows.Forms.MenuItem
        Me.menuColler = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.menuAddFolder2 = New System.Windows.Forms.MenuItem
        Me.LeftPanel = New System.Windows.Forms.Panel
        Me.MailFolders = New TreeViewPlus
        Me.TitleFolder = New System.Windows.Forms.StatusBar
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel
        Me.RightPart = New System.Windows.Forms.Panel
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.RightBottomPart = New System.Windows.Forms.Panel
        Me.MessageInHTML = New WebTextControl
        Me.MessageInRTFFormat = New TextControl
        Me.TitleMessage = New System.Windows.Forms.StatusBar
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel
        Me.RigthTopPart = New System.Windows.Forms.Panel
        Me.noMessages = New System.Windows.Forms.Label
        Me.MessagesList = New DataGridPlus
        Me.IsFileAttached = New System.Windows.Forms.DataGridViewImageColumn
        Me.IsMailRead = New System.Windows.Forms.DataGridViewImageColumn
        Me.IsCompteLie = New System.Windows.Forms.DataGridViewImageColumn
        Me.AffDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BothTo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BothFrom = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Subject = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoMail = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TitleMessages = New System.Windows.Forms.StatusBar
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Toolbar = New System.Windows.Forms.ToolStrip
        Me.tlbBtnGetMessages = New System.Windows.Forms.ToolStripSplitButton
        Me.tlbProgess = New System.Windows.Forms.ToolStripProgressBar
        Me.tlbStatus = New System.Windows.Forms.ToolStripLabel
        Me.hidingStatusTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LeftPanel.SuspendLayout()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RightPart.SuspendLayout()
        Me.RightBottomPart.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RigthTopPart.SuspendLayout()
        CType(Me.MessagesList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Toolbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuDossiers
        '
        Me.menuDossiers.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAddFolder, Me.menuDelFolder})
        '
        'menuAddFolder
        '
        Me.menuAddFolder.Index = 0
        Me.menuAddFolder.Text = "Ajouter un dossier"
        '
        'menuDelFolder
        '
        Me.menuDelFolder.Index = 1
        Me.menuDelFolder.Text = "Supprimer le dossier"
        '
        'menuMessages
        '
        Me.menuMessages.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAffichage, Me.menuRepondre, Me.menuRepondreAll, Me.menuDelMSG, Me.menuTransferMSG, Me.menuViewSource, Me.menuSeeMSG, Me.MenuItem3, Me.menuRead, Me.menuUnread, Me.MenuItem2, Me.menuCouper, Me.menuCopier, Me.menuColler, Me.MenuItem1, Me.menuAddFolder2})
        '
        'menuAffichage
        '
        Me.menuAffichage.Index = 0
        Me.menuAffichage.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuUpSorting, Me.menuDownSorting})
        Me.menuAffichage.Text = "Affichage"
        '
        'menuUpSorting
        '
        Me.menuUpSorting.Checked = True
        Me.menuUpSorting.Index = 0
        Me.menuUpSorting.RadioCheck = True
        Me.menuUpSorting.Text = "Ascendant"
        '
        'menuDownSorting
        '
        Me.menuDownSorting.Index = 1
        Me.menuDownSorting.RadioCheck = True
        Me.menuDownSorting.Text = "Descendant"
        '
        'menuRepondre
        '
        Me.menuRepondre.Index = 1
        Me.menuRepondre.Text = "Répondre à l'expéditeur"
        '
        'menuRepondreAll
        '
        Me.menuRepondreAll.Index = 2
        Me.menuRepondreAll.Text = "Répondre à tous les expéditeurs"
        '
        'menuDelMSG
        '
        Me.menuDelMSG.Index = 3
        Me.menuDelMSG.Text = "Supprimer le(s) message(s)"
        '
        'menuTransferMSG
        '
        Me.menuTransferMSG.Index = 4
        Me.menuTransferMSG.Text = "Transférer le message"
        '
        'menuViewSource
        '
        Me.menuViewSource.Index = 5
        Me.menuViewSource.Text = "Voir la source du message externe"
        '
        'menuSeeMSG
        '
        Me.menuSeeMSG.DefaultItem = True
        Me.menuSeeMSG.Index = 6
        Me.menuSeeMSG.Text = "Voir le message"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 7
        Me.MenuItem3.Text = "-"
        '
        'menuRead
        '
        Me.menuRead.Index = 8
        Me.menuRead.Text = "Marquer comme étant lu(s)"
        '
        'menuUnread
        '
        Me.menuUnread.Index = 9
        Me.menuUnread.Text = "Marquer comme étant non lu(s)"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 10
        Me.MenuItem2.Text = "-"
        '
        'menuCouper
        '
        Me.menuCouper.Index = 11
        Me.menuCouper.Text = "Couper"
        '
        'menuCopier
        '
        Me.menuCopier.Index = 12
        Me.menuCopier.Text = "Copier"
        '
        'menuColler
        '
        Me.menuColler.Index = 13
        Me.menuColler.Text = "Coller"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 14
        Me.MenuItem1.Text = "-"
        '
        'menuAddFolder2
        '
        Me.menuAddFolder2.Index = 15
        Me.menuAddFolder2.Text = "Ajout d'un dossier"
        '
        'LeftPanel
        '
        Me.LeftPanel.Controls.Add(Me.MailFolders)
        Me.LeftPanel.Controls.Add(Me.TitleFolder)
        Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.LeftPanel.Location = New System.Drawing.Point(0, 25)
        Me.LeftPanel.MinimumSize = New System.Drawing.Size(152, 461)
        Me.LeftPanel.Name = "LeftPanel"
        Me.LeftPanel.Size = New System.Drawing.Size(152, 468)
        Me.LeftPanel.TabIndex = 8
        '
        'MailFolders
        '
        Me.MailFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MailFolders.expandAllNodes = False
        Me.MailFolders.HideSelection = False
        Me.MailFolders.ImageIndex = 0
        Me.MailFolders.Location = New System.Drawing.Point(0, 28)
        Me.MailFolders.MinimumSize = New System.Drawing.Size(152, 433)
        Me.MailFolders.Name = "MailFolders"
        Me.MailFolders.readOnly = False
        Me.MailFolders.SelectedImageIndex = 0
        Me.MailFolders.showImages = True
        Me.MailFolders.Size = New System.Drawing.Size(152, 440)
        Me.MailFolders.Sorted = True
        Me.MailFolders.TabIndex = 22
        Me.MailFolders.tree = Nothing
        '
        'TitleFolder
        '
        Me.TitleFolder.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleFolder.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleFolder.Location = New System.Drawing.Point(0, 0)
        Me.TitleFolder.Name = "TitleFolder"
        Me.TitleFolder.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel3})
        Me.TitleFolder.ShowPanels = True
        Me.TitleFolder.Size = New System.Drawing.Size(152, 28)
        Me.TitleFolder.SizingGrip = False
        Me.TitleFolder.TabIndex = 21
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        Me.StatusBarPanel3.Text = "Dossiers"
        Me.StatusBarPanel3.ToolTipText = "Liste des dossiers"
        Me.StatusBarPanel3.Width = 152
        '
        'RightPart
        '
        Me.RightPart.Controls.Add(Me.Splitter2)
        Me.RightPart.Controls.Add(Me.RightBottomPart)
        Me.RightPart.Controls.Add(Me.RigthTopPart)
        Me.RightPart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RightPart.Location = New System.Drawing.Point(154, 25)
        Me.RightPart.MinimumSize = New System.Drawing.Size(261, 0)
        Me.RightPart.Name = "RightPart"
        Me.RightPart.Size = New System.Drawing.Size(582, 468)
        Me.RightPart.TabIndex = 9
        '
        'Splitter2
        '
        Me.Splitter2.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 176)
        Me.Splitter2.MinExtra = 148
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(582, 2)
        Me.Splitter2.TabIndex = 16
        Me.Splitter2.TabStop = False
        '
        'RightBottomPart
        '
        Me.RightBottomPart.Controls.Add(Me.MessageInHTML)
        Me.RightBottomPart.Controls.Add(Me.MessageInRTFFormat)
        Me.RightBottomPart.Controls.Add(Me.TitleMessage)
        Me.RightBottomPart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RightBottomPart.Location = New System.Drawing.Point(0, 176)
        Me.RightBottomPart.MinimumSize = New System.Drawing.Size(261, 0)
        Me.RightBottomPart.Name = "RightBottomPart"
        Me.RightBottomPart.Size = New System.Drawing.Size(582, 292)
        Me.RightBottomPart.TabIndex = 15
        '
        'MessageInHTML
        '
        Me.MessageInHTML.activateLinksOnEdit = True
        Me.MessageInHTML.allowContextMenu = True
        Me.MessageInHTML.allowEditorContextMenu = True
        Me.MessageInHTML.allowNavigation = False
        Me.MessageInHTML.allowRefresh = False
        Me.MessageInHTML.allowUndo = True
        Me.MessageInHTML.ancre = Nothing
        Me.MessageInHTML.ancreActif = False
        Me.MessageInHTML.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MessageInHTML.editorContextMenu = Nothing
        Me.MessageInHTML.editorHeight = 350
        Me.MessageInHTML.editorURL = ""
        Me.MessageInHTML.editorWidth = 460
        Me.MessageInHTML.htmlPageURL = Nothing
        Me.MessageInHTML.Location = New System.Drawing.Point(0, 24)
        Me.MessageInHTML.Name = "MessageInHTML"
        Me.MessageInHTML.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MessageInHTML.Size = New System.Drawing.Size(582, 268)
        Me.MessageInHTML.startupPos = 0
        Me.MessageInHTML.TabIndex = 24
        Me.MessageInHTML.toolBarStyles = 1
        Me.MessageInHTML.Visible = False
        '
        'MessageInRTFFormat
        '
        Me.MessageInRTFFormat.ancre = Nothing
        Me.MessageInRTFFormat.ancreON = False
        Me.MessageInRTFFormat.ancreRemove = False
        Me.MessageInRTFFormat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MessageInRTFFormat.Location = New System.Drawing.Point(0, 24)
        Me.MessageInRTFFormat.Name = "MessageInRTFFormat"
        Me.MessageInRTFFormat.ReadOnly = True
        Me.MessageInRTFFormat.showImgMenu = True
        Me.MessageInRTFFormat.showMenu = True
        Me.MessageInRTFFormat.Size = New System.Drawing.Size(582, 268)
        Me.MessageInRTFFormat.TabIndex = 23
        Me.MessageInRTFFormat.tabSpacing = CType(0, Short)
        Me.MessageInRTFFormat.Text = ""
        '
        'TitleMessage
        '
        Me.TitleMessage.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleMessage.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleMessage.Location = New System.Drawing.Point(0, 0)
        Me.TitleMessage.Name = "TitleMessage"
        Me.TitleMessage.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1})
        Me.TitleMessage.ShowPanels = True
        Me.TitleMessage.Size = New System.Drawing.Size(582, 24)
        Me.TitleMessage.SizingGrip = False
        Me.TitleMessage.TabIndex = 21
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "Visionnement du message"
        Me.StatusBarPanel1.Width = 582
        '
        'RigthTopPart
        '
        Me.RigthTopPart.Controls.Add(Me.noMessages)
        Me.RigthTopPart.Controls.Add(Me.MessagesList)
        Me.RigthTopPart.Controls.Add(Me.TitleMessages)
        Me.RigthTopPart.Dock = System.Windows.Forms.DockStyle.Top
        Me.RigthTopPart.Location = New System.Drawing.Point(0, 0)
        Me.RigthTopPart.MinimumSize = New System.Drawing.Size(261, 176)
        Me.RigthTopPart.Name = "RigthTopPart"
        Me.RigthTopPart.Size = New System.Drawing.Size(582, 176)
        Me.RigthTopPart.TabIndex = 14
        '
        'noMessages
        '
        Me.noMessages.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.noMessages.AutoSize = True
        Me.noMessages.BackColor = System.Drawing.SystemColors.Window
        Me.noMessages.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.noMessages.Location = New System.Drawing.Point(228, 99)
        Me.noMessages.Name = "noMessages"
        Me.noMessages.Size = New System.Drawing.Size(137, 20)
        Me.noMessages.TabIndex = 22
        Me.noMessages.Text = "Aucun message"
        '
        'MessagesList
        '
        Me.MessagesList.AllowUserToAddRows = False
        Me.MessagesList.AllowUserToDeleteRows = False
        Me.MessagesList.AllowUserToOrderColumns = True
        Me.MessagesList.AllowUserToResizeRows = False
        Me.MessagesList.autoSelectOnDataSourceChanged = True
        Me.MessagesList.BackgroundColor = System.Drawing.SystemColors.Window
        Me.MessagesList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.MessagesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MessagesList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IsFileAttached, Me.IsCompteLie, Me.IsMailRead, Me.AffDate, Me.BothTo, Me.BothFrom, Me.Subject, Me.NoMail})
        Me.MessagesList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MessagesList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.MessagesList.Location = New System.Drawing.Point(0, 28)
        Me.MessagesList.MinimumSize = New System.Drawing.Size(0, 148)
        Me.MessagesList.Name = "MessagesList"
        Me.MessagesList.RowHeadersVisible = False
        Me.MessagesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MessagesList.Size = New System.Drawing.Size(582, 148)
        Me.MessagesList.TabIndex = 21
        Me.MessagesList.VirtualMode = True
        '
        'IsMailRead
        '
        Me.IsMailRead.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.IsMailRead.DataPropertyName = "IsMailRead"
        Me.IsMailRead.HeaderText = ""
        Me.IsMailRead.MinimumWidth = 16
        Me.IsMailRead.Name = "IsMailRead"
        Me.IsMailRead.Width = 16
        '
        '
        'IsFileAttached
        '
        Me.IsFileAttached.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.IsFileAttached.DataPropertyName = "IsFileAttached"
        Me.IsFileAttached.HeaderText = ""
        Me.IsFileAttached.MinimumWidth = 16
        Me.IsFileAttached.Name = "IsFileAttached"
        Me.IsFileAttached.Width = 16
        '
        'IsCompteLie
        '
        Me.IsCompteLie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.IsCompteLie.DataPropertyName = "IsCompteLie"
        Me.IsCompteLie.HeaderText = ""
        Me.IsCompteLie.MinimumWidth = 16
        Me.IsCompteLie.Name = "IsCompteLie"
        Me.IsCompteLie.Width = 16
        '
        'AffDate
        '
        Me.AffDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.AffDate.DataPropertyName = "AffDate"
        Me.AffDate.HeaderText = "Date"
        Me.AffDate.Name = "AffDate"
        Me.AffDate.Width = 55
        '
        'BothTo
        '
        Me.BothTo.DataPropertyName = "BothTo"
        Me.BothTo.HeaderText = "À"
        Me.BothTo.Name = "BothTo"
        Me.BothTo.Visible = False
        '
        'BothFrom
        '
        Me.BothFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.BothFrom.DataPropertyName = "BothFrom"
        Me.BothFrom.HeaderText = "De"
        Me.BothFrom.Name = "BothFrom"
        Me.BothFrom.Width = 46
        '
        'Subject
        '
        Me.Subject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Subject.DataPropertyName = "Subject"
        Me.Subject.HeaderText = "Sujet"
        Me.Subject.Name = "Subject"
        '
        'NoMail
        '
        Me.NoMail.DataPropertyName = "NoMail"
        Me.NoMail.HeaderText = "NoMail"
        Me.NoMail.Name = "NoMail"
        Me.NoMail.Visible = False
        '
        'TitleMessages
        '
        Me.TitleMessages.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleMessages.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleMessages.Location = New System.Drawing.Point(0, 0)
        Me.TitleMessages.Name = "TitleMessages"
        Me.TitleMessages.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel2})
        Me.TitleMessages.ShowPanels = True
        Me.TitleMessages.Size = New System.Drawing.Size(582, 28)
        Me.TitleMessages.SizingGrip = False
        Me.TitleMessages.TabIndex = 20
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Messages"
        Me.StatusBarPanel2.ToolTipText = "Liste des messages du dossier présentement sélectionné"
        Me.StatusBarPanel2.Width = 582
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(152, 25)
        Me.Splitter1.MinExtra = 261
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(2, 468)
        Me.Splitter1.TabIndex = 23
        Me.Splitter1.TabStop = False
        '
        'Toolbar
        '
        Me.Toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbBtnGetMessages, Me.tlbProgess, Me.tlbStatus})
        Me.Toolbar.Location = New System.Drawing.Point(0, 0)
        Me.Toolbar.Name = "Toolbar"
        Me.Toolbar.Size = New System.Drawing.Size(736, 25)
        Me.Toolbar.TabIndex = 24
        Me.Toolbar.Text = "ToolStrip1"
        '
        'tlbBtnGetMessages
        '
        Me.tlbBtnGetMessages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tlbBtnGetMessages.Image = CType(resources.GetObject("tlbBtnGetMessages.Image"), System.Drawing.Image)
        Me.tlbBtnGetMessages.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbBtnGetMessages.Name = "tlbBtnGetMessages"
        Me.tlbBtnGetMessages.Size = New System.Drawing.Size(32, 22)
        Me.tlbBtnGetMessages.Text = "Recevoir"
        Me.tlbBtnGetMessages.ToolTipText = "Recevoir les messages depuis le(s) serveur(s)"
        '
        'tlbProgess
        '
        Me.tlbProgess.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tlbProgess.Name = "tlbProgess"
        Me.tlbProgess.Size = New System.Drawing.Size(150, 22)
        Me.tlbProgess.Step = 5
        Me.tlbProgess.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.tlbProgess.Visible = False
        '
        'tlbStatus
        '
        Me.tlbStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tlbStatus.Name = "tlbStatus"
        Me.tlbStatus.Size = New System.Drawing.Size(19, 22)
        Me.tlbStatus.Text = "..."
        Me.tlbStatus.Visible = False
        '
        'hidingStatusTimer
        '
        Me.hidingStatusTimer.Interval = 10000
        '
        'msgSystem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(736, 493)
        Me.Controls.Add(Me.RightPart)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.LeftPanel)
        Me.Controls.Add(Me.Toolbar)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(664, 527)
        Me.Name = "msgSystem"
        Me.ShowInTaskbar = False
        Me.Text = "Réception des messages"
        Me.LeftPanel.ResumeLayout(False)
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RightPart.ResumeLayout(False)
        Me.RightBottomPart.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RigthTopPart.ResumeLayout(False)
        Me.RigthTopPart.PerformLayout()
        CType(Me.MessagesList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Toolbar.ResumeLayout(False)
        Me.Toolbar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Toolbar action"
    Private Sub accountsMenuClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Droit & Accès
        If currentDroitAcces(107) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de télécharger les messages d'un compte messagerie externe." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        nbAccountsLeftToDownload = 1
        Dim accountNoClicked() As String = sender.text.Split(New Char() {"- "})
        Try
            gatherEmails(accountNoClicked(0))
        Catch ex As NoInternetException
            MessageBox.Show("Impossible de télécharger les courriels d'un compte de messagerie externe car il n'y a aucun accès à Internet", "Téléchargement impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.tlbBtnGetMessages.Enabled = True
        Catch ex As UserAlreadyUsingException
            accountDownloading = Nothing
            myMainWin.StatusText = ex.Message
            MessageBox.Show(ex.Message, "Compte de courriel en cours de téléchargement", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.tlbBtnGetMessages.Enabled = True
        End Try
    End Sub

    Private Sub gatherEmails(ByVal noMailAccount As Integer)
        Dim curAccount As MailAccount = MailsManager.getInstance.getMailAccounts(noMailAccount)

        AddHandler curAccount.pop3Downloading, AddressOf chargementEnCours
        AddHandler curAccount.pop3Message, AddressOf popMessage
        AddHandler curAccount.pop3DownloadEnded, AddressOf popEnd
        AddHandler curAccount.pop3Cancelled, AddressOf popCancelled
        accountDownloading = curAccount
        Me.tlbBtnGetMessages.Enabled = False
        curAccount.gatherEmails()
    End Sub

    Private Sub popCancelled(ByVal curAccount As MailAccount)
        nbAccountsLeftToDownload -= 1

        RemoveHandler curAccount.pop3Downloading, AddressOf chargementEnCours
        RemoveHandler curAccount.pop3Message, AddressOf popMessage
        RemoveHandler curAccount.pop3DownloadEnded, AddressOf popEnd
        RemoveHandler curAccount.pop3Cancelled, AddressOf popCancelled

        If accountDownloading IsNot Nothing AndAlso accountDownloading.Equals(curAccount) Then accountDownloading = Nothing

        activateDownloadBtn()
    End Sub

    Private Sub popEnd(ByVal curAccount As MailAccount, ByVal pop As Protocols.POP3)
        popCancelled(curAccount)

        myMainWin.StatusText = "Téléchargement terminé des courriels pour le compte le courriel " & curAccount.toString & "." & vbCrLf & _
        "Il y a eu " & pop.newMessagesDownloadedCount & " / " & pop.newMessagesCount & " nouveau(x) message(s) de téléchargé(s) pour une taille totale de " & Fichiers.transformFileSizeToText(pop.newMessagesDownloadedSize) & " / " & Fichiers.transformFileSizeToText(pop.newMessagesSize) & "."
    End Sub

    Private Delegate Sub downloadBtnActivation()

    Private Sub activateDownloadBtn()
        If Me.IsDisposed OrElse Not Me.IsHandleCreated OrElse nbAccountsLeftToDownload <> 0 Then Exit Sub

        If Me.InvokeRequired Then
            Dim d As New downloadBtnActivation(AddressOf activateDownloadBtn)
            Me.BeginInvoke(d)
        Else
            Me.tlbBtnGetMessages.Enabled = True
        End If
    End Sub

    Private Sub popMessage(ByVal message As String)
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Exit Sub

        If Me.InvokeRequired Then
            Try
                Me.Invoke(New receiveMessage(AddressOf popMessage), New Object() {message})
            Catch ex As ObjectDisposedException
                'Nothing to do
            Catch ex As InvalidOperationException
                'Nothing to do
            End Try
            Exit Sub
        End If

        Me.tlbStatus.Text = message
        Me.tlbStatus.ToolTipText = message
        Me.tlbStatus.Visible = True
    End Sub

    Public Delegate Sub downloading(ByVal status As POP3DownloadingStatus)
    Public Delegate Sub receiveMessage(ByVal message As String)

    Private Sub chargementEnCours(ByVal status As POP3DownloadingStatus)
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Exit Sub

        If Me.InvokeRequired Then
            If Me.IsDisposed = False Then
                Try
                    Me.Invoke(New downloading(AddressOf chargementEnCours), New Object() {status})
                Catch ex As ObjectDisposedException
                    'Even with all the protections of IsDisposed... still an error
                End Try
            End If
            Exit Sub
        End If

        Select Case status.numEtape
            Case 1
                Me.hidingStatusTimer.Stop()
                Me.tlbStatus.Visible = True
                Me.tlbProgess.Visible = True

            Case 3
                Me.hidingStatusTimer.Start()
                Me.tlbProgess.Visible = False
        End Select
        Dim message As String = "Messages - (" & status.numMail & "/" & status.nbrNouveauMessage & ") : " & status.numEtape & " - " & status.etatChargement
        Me.tlbStatus.Text = message
        Me.tlbStatus.ToolTipText = message
        Me.tlbProgess.Value = status.pourcentMail
    End Sub

#End Region

    Private Shared accountDownloading As MailAccount
    Private Shared nbAccountsLeftToDownload As Integer = 0
    Public accountsParam() As String
    Private changeReadWaitingSeconds As Integer = 5
    Private isCutting As Boolean = False
    Private curMouseBouton As MouseButtons
    Private curMailFolder As New MailFolder
    Private curMail As Mail
    Private messageslistMousePosition As Point
    Private curRowMessage As DataGridViewRow
    Private MessageSizes(), TotalEmail, totalSizeReceived As Integer
    Private _SelectedMessage As String = ""
    Private foldersLoaded As Boolean = False

#Region "Propriétés"
    Public ReadOnly Property currentMailFolder() As MailFolder
        Get
            Return curMailFolder
        End Get
    End Property

    Public ReadOnly Property currentNoUser() As Integer
        Get
            If curMailFolder Is Nothing Then Return -1

            Return curMailFolder.noUser
        End Get
    End Property

    Public ReadOnly Property currentNoMailFolder() As Integer
        Get
            If curMailFolder Is Nothing Then Return 0

            Return curMailFolder.noMailFolder
        End Get
    End Property

    Public Property selectedMessage() As String
        Get
            Return _SelectedMessage
        End Get
        Set(ByVal value As String)
            _SelectedMessage = value
            If foldersLoaded Then selectMessage()
        End Set
    End Property
#End Region

#Region "Message menu actions"
    Private Sub menuCopier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCopier.Click
        copyMessage()
    End Sub

    Private Sub menuSeeMSG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSeeMSG.Click
        If MessagesList.SelectedRows.Count = 0 Then Exit Sub
        If curRowMessage Is Nothing Then Exit Sub

        Dim myMsgMessage As msgMessage = openUniqueWindow(New msgMessage, , , True)
        myMsgMessage.noMail = curRowMessage.Cells("NoMail").Value
        myMsgMessage.Show()
    End Sub



    Private Sub menuRepondre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRepondre.Click
        If curRowMessage Is Nothing Then Exit Sub

        Dim curMail As Mail = curFolderMails(curRowMessage.Cells("NoMail").Value)

        Dim replyTemplate As String = ""
        If PreferencesManager.getUserPreferences()("InsertOriginMSGOnRespond") Then
            Dim client As String = ""
            If curMail.noClient <> 0 Then client = getClientName(curMail.noClient) & " (" & curMail.noClient & ")"
            replyTemplate = MailsManager.fillReplyTemplate(curMail.message, curRowMessage.Cells("BothFrom").Value, client, curRowMessage.Cells("BothTo").Value.ToString, curRowMessage.Cells("AffDate").Value, curRowMessage.Cells("Subject").Value)
        End If
        If curMail.noUserFrom = 0 Then
            sendemailTo(curMail.from, True, replyTemplate, "Re: " & curMail.subject)
        Else
            Dim myMsgSending As msgSending = openUniqueWindow(New msgSending)
            If replyTemplate <> "" Then myMsgSending.setmessage(replyTemplate)
            myMsgSending.isAnswering = True
            myMsgSending.setInternalTo(curMail.noUserFrom)
            myMsgSending.setsujet("Re: " & curMail.subject)

            myMsgSending.Show()
        End If
    End Sub

    Private Sub menuRepondreAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRepondreAll.Click
        If curRowMessage Is Nothing Then Exit Sub

        Dim curMail As Mail = curFolderMails(curRowMessage.Cells("NoMail").Value)

        Dim from As String = curMail.from & IIf(curMail.from.EndsWith(";") OrElse curMail.from = "", "", ";") & curMail.cc
        Dim replyTemplate As String = ""
        If PreferencesManager.getUserPreferences()("InsertOriginMSGOnRespond") Then
            Dim client As String = ""
            If curMail.noClient <> 0 Then client = getClientName(curMail.noClient) & " (" & curMail.noClient & ")"
            replyTemplate = MailsManager.fillReplyTemplate(curMail.message, curMail.from, client, curMail.[to], curMail.affDate, curMail.subject)
        End If

        sendemailTo(from, True, replyTemplate, "Re: " & curMail.subject)
    End Sub

    Private Sub messagesList_RowContextMenuStripNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs) Handles MessagesList.RowContextMenuStripNeeded
        If MailFolders.SelectedNode Is Nothing Then Exit Sub
        If MailFolders.SelectedNode.Tag.ToString = "Utilisateurs" Then Exit Sub

        If curMouseBouton = MouseButtons.Right Then
            '            
            If e.RowIndex <> -1 And MessagesList.SelectedRows.Contains(MessagesList.Rows(e.RowIndex)) = False Then
                If Form.ModifierKeys <> Keys.Control Then MessagesList.ClearSelection()
                'Ensure message loaded
                MessagesList.Rows(e.RowIndex).Selected = True
                rowSelected(MessagesList.Rows(e.RowIndex))
            ElseIf e.RowIndex <> -1 Then
                rowSelected(MessagesList.Rows(e.RowIndex))
            End If

            showListContextMenu(True)
        End If
    End Sub


    Private Sub menuCouper_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCouper.Click
        isCutting = True
        copyMessage()
        delMessage()
        isCutting = False
    End Sub

    Private Sub menuColler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuColler.Click
        Dim cMailFolder As MailFolder = MailsManager.getInstance.getMailFolder(MailFolders.SelectedNode.Tag.ToString)
        If cMailFolder Is Nothing OrElse cMailFolder.noUser = -1 Then Exit Sub

        Dim mailsToCopy() As String = IO.Directory.GetDirectories(Mail.lastCopyPath)
        For i As Integer = 0 To mailsToCopy.Length - 1
            Dim curNoMail As Integer = getLastDir(mailsToCopy(i))
            Mail.paste(curNoMail, cMailFolder)
        Next i

        ' LoadMessagesList()
        InternalUpdatesManager.getInstance.sendUpdate("MessagesList(" & currentNoMailFolder & ")")
        curMailFolder.checkIfUnreadMails()
    End Sub


    Private Sub menuRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRead.Click
        startChangeReadStatus(True, 0)
    End Sub

    Private Sub menuUnread_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuUnread.Click
        startChangeReadStatus(False, 0)
    End Sub

    Private Sub menuViewSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuViewSource.Click
        Dim curMail As Mail = curFolderMails(curRowMessage.Cells("NoMail").Value)


        TextWindow.getInstance.texteType = RichTextBoxStreamType.PlainText
        TextWindow.getInstance.currentData = curMail.source.Replace("\n", vbCrLf)
        TextWindow.getInstance.Text = "Visualisation : Source du message : " & curMail.subject
        TextWindow.getInstance.isHTML = False
        TextWindow.getInstance.isLocked = True

        Dim pos As String = TextWindow.getInstance.ShowTexteModif(0)
    End Sub

    Private Sub menuTransferMSG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuTransferMSG.Click
        Dim curMail As Mail = curFolderMails(curRowMessage.Cells("NoMail").Value)

        Dim myMsgSending As msgSending = openUniqueWindow(New msgSending)
        myMsgSending.setsujet("Tr: " & curMail.subject)
        Dim replyTemplate As String = ""
        Dim client As String = ""
        If curMail.noClient <> 0 Then client = getClientName(curMail.noClient) & " (" & curMail.noClient & ")"
        replyTemplate = MailsManager.fillReplyTemplate(curMail.message, curRowMessage.Cells("BothFrom").Value, client, curRowMessage.Cells("BothTo").Value.ToString, curMail.affDate, curMail.subject)
        myMsgSending.setmessage(replyTemplate)
        myMsgSending.setCompteLie(curMail.noClient)

        Dim filesAttached As String = ""
        If curMail.filesAttached <> "" Then
            Dim curMsgPath As String = appPath & bar(appPath) & "Data\Mails\" & curMail.noMail & "\attach\"
            Dim tmpPath As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\trMsg" & (New Random()).Next()
            IO.Directory.CreateDirectory(tmpPath)
            tmpPath &= "\"
            For Each attach As String In curMail.filesAttached.Split(New Char() {"§"})
                Dim curAttach() As String = attach.Split(New Char() {"|"})
                Select Case curAttach(0)
                    Case "DB"
                        filesAttached &= "DB:\" & curAttach(1) & ";"
                    Case "FILE"
                        IO.File.Copy(curMsgPath & curAttach(2), tmpPath & curAttach(1), True)
                        filesAttached &= tmpPath & curAttach(1) & ";"
                End Select
            Next
        End If

        myMsgSending.setAttachements(filesAttached)

        myMsgSending.Show()
    End Sub
#End Region

    Private Sub checkAccountsEmail()
        'Droit & Accès
        If currentDroitAcces(107) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de télécharger les messages d'un compte messagerie externe." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim nbAccounts As Integer = MailsManager.getInstance.getMailAccounts.Count
        If nbAccounts = 0 Then Exit Sub

        Me.tlbBtnGetMessages.Enabled = False

        nbAccountsLeftToDownload = 0

        'Count nb accounts to download
        For i As Integer = 0 To nbAccounts - 1
            If MailsManager.getInstance.getMailAccounts(i).includeInGeneralReception Then
                nbAccountsLeftToDownload += 1
            End If
        Next i

        'Start download of accounts
        Dim hadError As Boolean = False
        Try
            For i As Integer = 0 To nbAccounts - 1
                If MailsManager.getInstance.getMailAccounts(i).includeInGeneralReception Then
                    Try
                        gatherEmails(i)
                    Catch ex As UserAlreadyUsingException
                        accountDownloading = Nothing
                        hadError = True
                        myMainWin.StatusText = ex.Message
                        Me.tlbBtnGetMessages.Enabled = True
                    End Try
                End If
            Next i
        Catch ex As NoInternetException
            MessageBox.Show("Impossible de télécharger les courriels d'un compte de messagerie externe car il n'y a aucun accès à Internet", "Téléchargement impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.tlbBtnGetMessages.Enabled = True
        End Try

        If hadError Then MessageBox.Show("Au moins un compte de courriel était déjà en cours de téléchargement. Veuillez consulter l'historique des actions pour plus de détails.", "Compte de courriel en cours de téléchargement", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    End Sub

    Private Sub msgSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Enregistrement de l'affichage personnalisée de la réception de messages
        Dim curNoMail As Integer = 0
        Try
            If MessagesList.Rows.Count > 0 Then curNoMail = Me.curRowMessage.Cells("NoMail").Value
        Catch
            'No message in the list
        End Try
        Dim curMail As String = ""
        If Me.MailFolders.SelectedNode IsNot Nothing Then curMail = Me.MailFolders.SelectedNode.Tag.ToString & "\" & curNoMail
        Dim curUser As User = UsersManager.currentUser
        REM TO REACTIVATE
        curUser.settings.mailSystem = Me.Height & "§" & Me.Width & "§" & Splitter1.SplitPosition & "§" & Splitter2.SplitPosition & "§" & curMail & "§" & MessagesList.SortedColumn.Name & "§" & MessagesList.SortOrder.ToString.Substring(0, 1)
        curUser.settings.saveData()
    End Sub

    Private Sub msgSystem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If PreferencesManager.getUserPreferences()("NbSecBeforeMarkAsRead") > 0 Then changeReadWaitingSeconds = PreferencesManager.getUserPreferences()("NbSecBeforeMarkAsRead")
        MessagesList.VirtualMode = False
        If MessagesList.SortedColumn Is Nothing Then MessagesList.Sort(MessagesList.Columns("AffDate"), System.ComponentModel.ListSortDirection.Descending)
        menuDownSorting.Checked = True
        menuUpSorting.Checked = False

        loadFolders()

        If selectedMessage <> "" Then selectMessage()

        foldersLoaded = True

        If accountDownloading IsNot Nothing Then
            AddHandler accountDownloading.pop3Downloading, AddressOf chargementEnCours
            AddHandler accountDownloading.pop3Message, AddressOf popMessage
            AddHandler accountDownloading.pop3DownloadEnded, AddressOf popEnd
        End If

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Loaded - " & (Date.Now.Subtract(starting).TotalMilliseconds)
    End Sub

    Private Sub selectMessage()
        MailFolders.selectNode(selectedMessage.Substring(0, selectedMessage.Length - getLastDir(selectedMessage).Length - 1))
        MessagesList.ClearSelection()
        For i As Integer = 0 To MessagesList.Rows.Count - 1
            If MessagesList.Rows(i).Cells("NoMail").Value = getLastDir(selectedMessage) Then
                MessagesList.Rows(i).Selected = True
                MessagesList.FirstDisplayedScrollingRowIndex = i
                rowSelected(MessagesList.Rows(i))
                Exit For
            End If
        Next i
    End Sub

    Private Delegate Sub loading()
    Private isLoading As Boolean = False
    Private Shared tries As Integer = 0

    Public Sub loadFolders()
        If Me.InvokeRequired Then
            Me.Invoke(New loading(AddressOf loadFolders))
            Exit Sub
        End If

        If ConnectionsManager.currentUser = 0 Then MessageBox.Show("L'administrateur n'a pas accès à ce secteur.Merci!", "Administrateur interdit") : Exit Sub

        tries += 1
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "------- TRY #" & tries


        Dim start As Date = Date.Now

        Dim expanded As Generic.List(Of String) = MailFolders.getExpanded()

        isLoading = True
        Dim curMailFolders() As MailFolder
        Dim curMailFolders2 As Generic.List(Of MailFolder) = MailsManager.getInstance.getMailFolders()
        ReDim curMailFolders(curMailFolders2.Count)
        curMailFolders(0) = New MailFolder()
        curMailFolders(0).noUser = -1
        curMailFolders(0).mailFolder = "Utilisateurs"
        curMailFolders(0).iconIndex = 2
        curMailFolders(0).iconSelectedIndex = 2
        curMailFolders2.CopyTo(0, curMailFolders, 1, curMailFolders2.Count)

        'Reload tree as it was
        Dim oldFolder As String = If(curMailFolder Is Nothing, "", curMailFolder.toString())
        MailFolders.SuspendLayout()
        MailFolders.tree = curMailFolders
        Dim selectedTreeNode As Object = Nothing
        If Not MailFolders.SelectedNode Is Nothing Then selectedTreeNode = MailFolders.SelectedNode
        MailFolders.refreshTree(expanded)
        If selectedTreeNode IsNot Nothing Then
            MailFolders.expansion(selectedTreeNode.Tag.ToString)
            curMailFolder = selectedTreeNode.Tag
        End If

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "MF - T : " & (Date.Now.Subtract(start).TotalMilliseconds)
        start = Date.Now

        isLoading = False
        If Not isShown OrElse curMailFolder.toString() <> oldFolder Then
            loadMessagesList()
        End If

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T : " & (Date.Now.Subtract(start).TotalMilliseconds)

    End Sub

    Private Sub MailFolders_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MailFolders.MouseClick
        Dim allowedGen As Boolean = True
        Dim allowedUser As Boolean = True
        'Droit & Accès
        If currentDroitAcces(40) = False Then allowedGen = False
        If currentDroitAcces(42) = False Then allowedUser = False

        If MailFolders.GetNodeAt(New Point(e.X, e.Y)) Is Nothing Then Exit Sub
        If MailFolders.GetNodeAt(New Point(e.X, e.Y)).Text <> "" Then
            MailFolders.SelectedNode = MailFolders.GetNodeAt(New Point(e.X, e.Y))

            If MailFolders.SelectedNode.Tag.ToString = "Généraux" Or MailFolders.SelectedNode.Tag.ToString = "Utilisateurs" Then
                menuDelFolder.Enabled = False
                If MailFolders.SelectedNode.Tag.ToString = "Utilisateurs" Then
                    menuAddFolder.Enabled = False
                Else
                    menuAddFolder.Enabled = allowedGen
                End If
            Else
                If MailFolders.SelectedNode.Tag.ToString.StartsWith("Utilisateurs") Then
                    Dim sFullPath() As String = MailFolders.SelectedNode.Tag.ToString.Split(New Char() {"\"})
                    If sFullPath(1) = currentUserName & " (" & ConnectionsManager.currentUser & ")" Then allowedUser = True

                    menuAddFolder.Enabled = allowedUser
                    If sFullPath.Length > 2 Then
                        menuDelFolder.Enabled = allowedUser
                    Else
                        menuDelFolder.Enabled = False
                    End If
                Else
                    menuAddFolder.Enabled = allowedGen
                    menuDelFolder.Enabled = allowedGen
                End If
            End If
            If e.Button = MouseButtons.Right Then menuDossiers.Show(MailFolders, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub menuAddFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddFolder.Click, menuAddFolder2.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ".§,§:§<§>§(§)"
        Dim myFolderName As String = myInputBoxPlus("Veuillez entrer le nom du nouveau dossier", "Nom du dossier")
        If myFolderName = "" Then Exit Sub

        Dim returnOfAdding As String = MailsManager.getInstance.addMailFolder(MailFolders.SelectedNode.Tag.ToString & "\" & myFolderName, , CType(MailFolders.SelectedNode.Tag, MailFolder).isSendingFolder)
        If returnOfAdding <> "" Then
            MessageBox.Show(returnOfAdding, "Erreur")
            Exit Sub
        End If
    End Sub

    Private Sub menuDelFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDelFolder.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce dossier et ces messages ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        CType(MailFolders.SelectedNode.Tag, MailFolder).delete()
        loadFolders()
    End Sub

    Private Sub mailFolders_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles MailFolders.AfterSelect
        If isLoading Then Exit Sub

        loadMessagesList(False)
        If MessagesList.Rows.Count > 0 Then
            MessagesList.ClearSelection()
            MessagesList.Rows(0).Selected = True
            curMail = Nothing
            rowSelected(MessagesList.Rows(0))
            MessagesList.Focus()
        End If
    End Sub

    Public Sub loadMessagesList(Optional ByVal doReselection As Boolean = True)
        Dim start As Date = Date.Now

        resetMessageBoxes()

        Dim noMailFolder As Integer = 0
        Dim cMailFolder As MailFolder
        If MailFolders.SelectedNode IsNot Nothing Then cMailFolder = MailFolders.SelectedNode.Tag
        If cMailFolder IsNot Nothing Then noMailFolder = cMailFolder.noMailFolder

        'Show or hide BothTo/BothFrom columns upon the selected folder (Only one can be visible)
        If cMailFolder IsNot Nothing AndAlso cMailFolder.isSendingFolder Then
            MessagesList.Columns("BothTo").Visible = True
            MessagesList.Columns("BothFrom").Visible = False
        Else
            MessagesList.Columns("BothTo").Visible = False
            MessagesList.Columns("BothFrom").Visible = True
        End If

        'Load data
        If cMailFolder IsNot Nothing AndAlso curMailFolder IsNot Nothing AndAlso curMailFolder.noMailFolder = cMailFolder.noMailFolder Then doReselection = True
        curMailFolder = cMailFolder
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1.0 : " & (Date.Now.Subtract(start).TotalMilliseconds)
        Dim mails As Generic.List(Of Mail) = MailsManager.getInstance().loadMails(noMailFolder)
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1.1 for " & mails.Count & " mails : " & (Date.Now.Subtract(start).TotalMilliseconds)

        'Build table
        Dim mailTable As New DataTable()
        With mailTable.Columns
            .Add(New DataColumn("IsCompteLie", GetType(Image)))
            .Add(New DataColumn("IsFileAttached", GetType(Image)))
            .Add(New DataColumn("IsMailRead", GetType(Image)))
            .Add(New DataColumn("AffDate", GetType(Date)))
            .Add(New DataColumn("BothTo", GetType(String)))
            .Add(New DataColumn("BothFrom", GetType(String)))
            .Add(New DataColumn("Subject", GetType(String)))
            .Add(New DataColumn("NoMail", GetType(Integer)))
        End With

        'Save which messages are currently selected
        Dim msgSelected As DataGridViewSelectedRowCollection = MessagesList.SelectedRows
        Dim noMsgSelected As New Generic.List(Of Integer)(MessagesList.GetCellCount(DataGridViewElementStates.Selected))
        For i As Integer = 0 To msgSelected.Count - 1
            noMsgSelected.Add(msgSelected(i).Cells("NoMail").Value)
        Next i
        Dim oldFirstRowIndex As Integer = MessagesList.FirstDisplayedScrollingRowIndex
        Dim oldFirstColumnIndex As Integer = MessagesList.FirstDisplayedScrollingColumnIndex

        'Save ordering properties
        Dim compteLieIndex As Integer = MessagesList.Columns("IsCompteLie").DisplayIndex
        REM TO REACTIVATE
        Dim orderingColName As String = MessagesList.SortedColumn.Name
        If orderingColName = "" Then orderingColName = "Date"
        Dim order As SortOrder = MessagesList.SortOrder

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1.2 : " & (Date.Now.Subtract(start).TotalMilliseconds)

        'Load table
        curFolderMails.Clear()

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1.3 : " & (Date.Now.Subtract(start).TotalMilliseconds)

        For Each curMail As Mail In mails
            curFolderMails.Add(curMail.noMail, curMail)

            'Manage icons
            Dim isCompteLie As Image = emptyImage
            Dim isFileAttached As Image = emptyImage
            Dim isMailRead As Image = emptyImage
            If curMail.noClient <> 0 Then isCompteLie = clientAcountImage
            If curMail.filesAttached <> "" Then isFileAttached = fileAttachedImage
            If Not curMail.isRead Then isMailRead = newMailImage

            'Manage From column
            Dim bothFrom As String = curMail.from
            If curMail.noUserFrom <> 0 Then bothFrom = UsersManager.getInstance.getUser(curMail.noUserFrom).toString

            'Manage To column
            Dim bothTo As String = curMail.[to]
            If bothTo = "" AndAlso curMail.noUserTo <> 0 Then bothTo = UsersManager.getInstance.getUser(curMail.noUserTo).toString

            'Add row to table
            mailTable.Rows.Add(New Object() {isCompteLie, isFileAttached, isMailRead, curMail.affDate, bothTo, bothFrom, curMail.subject, curMail.noMail})
        Next

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1.4 : " & (Date.Now.Subtract(start).TotalMilliseconds)

        'Associate table with control
        MessagesList.DataSource = mailTable

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1.5 : " & (Date.Now.Subtract(start).TotalMilliseconds)

        'Resort messages list
        REM TO REACTIVATE
        If order = SortOrder.Ascending Then
            MessagesList.Sort(MessagesList.Columns(orderingColName), System.ComponentModel.ListSortDirection.Ascending)
        Else
            MessagesList.Sort(MessagesList.Columns(orderingColName), System.ComponentModel.ListSortDirection.Descending)
        End If

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1.6 : " & (Date.Now.Subtract(start).TotalMilliseconds)

        MessagesList.Columns("IsCompteLie").DisplayIndex = compteLieIndex

        'Show no message control if appropriate, otherwise, reselect elements
        If CType(MessagesList.DataSource, DataTable).Rows.Count = 0 Then
            noMessages.Visible = True
        Else
            noMessages.Visible = False
            If Not deletingMessage AndAlso doReselection AndAlso noMsgSelected.Count <> 0 Then
                For i As Integer = 0 To MessagesList.Rows.Count - 1
                    Dim noMail As Integer = MessagesList.Rows(i).Cells("NoMail").Value
                    If noMsgSelected.Contains(noMail) Then
                        MessagesList.Rows(i).Selected = True
                    Else
                        MessagesList.Rows(i).Selected = False
                    End If
                Next i
            End If

            With MessagesList
                '.SuspendLayout()
                '.SuspendPaint()
                'For i As Integer = 0 To .Rows.Count - 1
                '    Dim noMail As Integer = .Rows(i).Cells("NoMail").Value
                '    If Not curFolderMails(noMail).isRead Then .Rows(i).DefaultCellStyle = notReadMessageLineStyle
                'Next i
                '.ResumeLayout()
                '.ResumePaint()

            End With

            'Ensure same scrollbars position
            If oldFirstRowIndex <> -1 AndAlso oldFirstRowIndex < MessagesList.RowCount Then MessagesList.FirstDisplayedScrollingRowIndex = oldFirstRowIndex
            If oldFirstColumnIndex <> -1 AndAlso oldFirstColumnIndex < MessagesList.ColumnCount Then MessagesList.FirstDisplayedScrollingColumnIndex = oldFirstColumnIndex
        End If

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "ML - T1 : " & (Date.Now.Subtract(start).TotalMilliseconds)
    End Sub

    Private Sub resetMessageBoxes()
        MessageInRTFFormat.Rtf = ""
        MessageInHTML.setHtml("")
    End Sub

    Private Sub sortClick(ByVal sender As Object, ByVal e As EventArgs) Handles menuDownSorting.Click, menuUpSorting.Click
        With MessagesList
            If .SortOrder = SortOrder.Ascending Then
                .Sort(.SortedColumn, System.ComponentModel.ListSortDirection.Descending)
                menuDownSorting.Checked = True
                menuUpSorting.Checked = False
            Else
                .Sort(.SortedColumn, System.ComponentModel.ListSortDirection.Ascending)
                menuDownSorting.Checked = False
                menuUpSorting.Checked = True
            End If
        End With
    End Sub

    Private Sub messagesList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles MessagesList.CellMouseDoubleClick
        If e.RowIndex >= 0 Then menuSeeMSG_Click(sender, EventArgs.Empty)
    End Sub

    Private Sub messagesList_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles MessagesList.CellMouseDown
        curMouseBouton = e.Button
    End Sub
    Private Sub messagesList_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MessagesList.MouseDown
        messageslistMousePosition = e.Location
        If e.Button = System.Windows.Forms.MouseButtons.Right AndAlso MessagesList.HitTest(e.X, e.Y).RowIndex = -1 Then showListContextMenu(False)
    End Sub

    Private Sub copyMessage()
        Mail.clearTempCopyFolder()
        Mail.changeLastCopyPath()

        With MessagesList
            For Each OneSelItem As DataGridViewRow In .SelectedRows
                Dim curMail As Mail = curFolderMails(OneSelItem.Cells("NoMail").Value)
                curMail.copy()
            Next
        End With
    End Sub

    Private deletingMessage As Boolean = False

    Private Sub delMessage()
        deletingMessage = True

        Dim oneSelItem As DataGridViewRow

        Dim mailsToDelete As New Generic.List(Of Mail)
        For Each oneSelItem In MessagesList.SelectedRows
            Dim curMail As Mail = curFolderMails(oneSelItem.Cells("NoMail").Value)
            mailsToDelete.Add(curMail)
        Next

        Dim n As Integer = 0
        For Each curMail As Mail In mailsToDelete
            If isCutting = False AndAlso curMail.hasSentFeedBack = False AndAlso curMail.isRead = False Then curMail.sendFeedBack()
            Try
                curMail.delete()
                n += 1
            Catch ex As DBItemableUndeletable
                MessageBox.Show("Impossible de supprimer le message (" & curMail.subject & "), car l'un des ses fichiers attachés est en cours de visualisation.", "Erreur de suppression", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next

        Me.curMail = Nothing

        'LoadMessagesList()
        If n > 0 Then
            InternalUpdatesManager.getInstance.sendUpdate("MessagesList(" & currentNoMailFolder & ")")
            curMailFolder.checkIfUnreadMails()
        End If

        deletingMessage = False
    End Sub

    Private Sub menuDelMSG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDelMSG.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce(s) message(s) ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        delMessage()
    End Sub

    Private messageLoaded As Boolean = False

    Private Sub MessageInHTML_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MessageInHTML.MouseClick
        'MessageInHTML.focus()
    End Sub

    Private Sub messageInHTML_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MessageInHTML.MouseEnter
        'If Me.ActiveControl.Equals(MessageInHTML) = False Then Me.ActiveControl = MessageInHTML
        'MessageInHTML.focus()
    End Sub

    Private Sub messageInHTML_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MessageInHTML.MouseHover
        'MessageInHTML.focus()
    End Sub

    Private Sub messageInHTML_NavigateComplete(ByVal url As String) Handles MessageInHTML.navigateComplete
        If selectedMessage <> "" And messageLoaded = False And foldersLoaded Then selectMessage() : messageLoaded = True
    End Sub

    Private justSelectRow As Boolean = False

    Private Sub messagesList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MessagesList.SelectionChanged
        If MessagesList.SelectedRows.Count = 0 Then
            resetMessageBoxes()
        Else
            Dim row As DataGridViewRow = MessagesList.currentRow
            If row.Selected = False Then row = MessagesList.SelectedRows(0)

            rowSelected(row)
        End If
    End Sub

    Private isShown As Boolean = False

    Private Sub rowSelected(ByVal row As DataGridViewRow)
        justSelectRow = True
        curRowMessage = row
        Dim curMail As Mail = curFolderMails(row.Cells("NoMail").Value)

        startChangeReadStatus(True, changeReadWaitingSeconds)

        If isShown = False OrElse Me.curMail Is Nothing OrElse Not curMail.Equals(Me.curMail) Then
            MessageInHTML.setHtml(curMail.message.Replace("\n", vbCrLf))
            MessageInHTML.Visible = True
            MessageInRTFFormat.Visible = False

            Me.curMail = curMail
        End If
    End Sub

    Private Sub messagesList_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MessagesList.RowPrePaint
        If curFolderMails.ContainsKey(MessagesList.Rows(e.RowIndex).Cells("NoMail").Value) = False Then Exit Sub

        REM TO REACTIVATE
        'Dim curMail As Mail = Me.curFolderMails(MessagesList.Rows(e.RowIndex).Cells("NoMail").Value)
        'If Not curMail.isRead Then
        '    MessagesList.Rows(e.RowIndex).DefaultCellStyle = notReadMessageLineStyle
        'End If
    End Sub

    Private Sub startChangeReadStatus(ByVal isRead As Boolean, ByVal waitingSeconds As Integer)
        Dim mailNos As New Generic.List(Of Integer)
        Dim mailIndexes As New Generic.List(Of Integer)
        For i As Integer = 0 To MessagesList.SelectedRows.Count - 1
            Dim curMail As Mail = curFolderMails(MessagesList.SelectedRows(i).Cells("NoMail").Value)

            'Only those that have to be changed are added
            If curMail.isRead <> isRead Then
                mailNos.Add(curMail.noMail)
                mailIndexes.Add(MessagesList.SelectedCells(i).RowIndex)
            End If
        Next i

        'Only if there is some mails to change
        If mailNos.Count <> 0 Then
            startChangeReadStatus(isRead, waitingSeconds, mailNos, mailIndexes)
        Else
            ChangeReadStatusParams.nbThreads += 1 'Ensure other threads aren't ran
        End If
    End Sub

    Private Sub startChangeReadStatus(ByVal isRead As Boolean, ByVal waitingSeconds As Integer, ByVal mailNos As Generic.List(Of Integer), ByVal mailIndexes As Generic.List(Of Integer))
        Dim threadParams As New ChangeReadStatusParams
        threadParams.isRead = isRead
        threadParams.mailNos = mailNos
        threadParams.mailIndexes = mailIndexes
        threadParams.waitingSeconds = waitingSeconds

        Dim newChangeThread As New Threading.Thread(AddressOf changeReadStatus)
        newChangeThread.Start(threadParams)
    End Sub

    Private Class ChangeReadStatusParams
        Public isRead As Boolean
        Public mailNos As Generic.List(Of Integer)
        Public mailIndexes As Generic.List(Of Integer)
        Public waitingSeconds As Integer
        Public Shared nbThreads As Integer = 0
        Public Shared isStopped As Boolean = False

        Public Sub New()
            nbThreads += 1
        End Sub
    End Class

    Private Delegate Sub applyingNewReadStatus(ByVal params As ChangeReadStatusParams)

    Private Sub applyNewReadStatus(ByVal params As ChangeReadStatusParams)
        If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Exit Sub

        If Me.InvokeRequired Then
            Try
                Me.Invoke(New applyingNewReadStatus(AddressOf applyNewReadStatus), New Object() {params})
            Catch ex As ObjectDisposedException
                'Nothing to do
            Catch ex As InvalidOperationException
                'Nothing to do
            End Try
            Exit Sub
        End If

        Dim isMailReadImage As Image = If(params.isRead, emptyImage, newMailImage)
        For Each curRow As DataGridViewRow In MessagesList.Rows
            Dim noMail As Integer = curRow.Cells("NoMail").Value
            If params.mailNos.IndexOf(noMail) = -1 Then Continue For

            curRow.Cells("IsMailRead").Value = isMailReadImage
        Next
    End Sub

    Private Sub changeReadStatus(ByVal paramsIn As Object)
        Try
            Dim params As ChangeReadStatusParams = paramsIn
            Dim myNbThread As Integer = ChangeReadStatusParams.nbThreads

            'Waiting
            Threading.Thread.Sleep(params.waitingSeconds * 1000)

            'Exit conditions
            If Me.IsDisposed Or Me.Disposing Or Me.IsHandleCreated = False Then Exit Sub
            If myNbThread <> ChangeReadStatusParams.nbThreads Then Exit Sub

            'Send feedback if needed + Create list of no mails
            Dim nos As String = ""
            Dim noMailFolder As Integer
            For i As Integer = 0 To params.mailNos.Count - 1
                If curFolderMails.ContainsKey(params.mailNos(i)) = False Then Continue For

                Dim curMail As Mail = curFolderMails(params.mailNos(i))
                If curMail.isRead = params.isRead Then Continue For 'Already set to demanded status

                nos &= "," & curMail.noMail
                curMail.isRead = params.isRead
                If params.isRead And curMail.hasSentFeedBack = False Then curMail.sendFeedBack()
                noMailFolder = curMail.noMailFolder
            Next i

            If nos <> "" Then
                nos = nos.Substring(1)
                'REM SHOULD BE Transfered to MailManager
                DBLinker.getInstance.updateDB("Mails", "IsRead='" & params.isRead & "'", "NoMail", "(" & nos & ")", False, " IN ")

                curMailFolder.checkIfUnreadMails()

                applyNewReadStatus(params)

                InternalUpdatesManager.getInstance.sendUpdate("MessagesList-IsRead(" & noMailFolder & ")")

                If currentNoMailFolder <> noMailFolder Then
                    InternalUpdatesManager.getInstance.sendUpdate("MessagesList-IsRead(" & currentNoMailFolder & ")")
                End If
            End If
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Private Sub showListContextMenu(ByVal fromRow As Boolean)
        If MailFolders.SelectedNode.Tag.ToString = "Utilisateurs" Then Exit Sub

        Dim allowed As Boolean = True
        'Droit & Accès
        If MailFolders.SelectedNode.Tag.ToString.StartsWith("G") Then
            If currentDroitAcces(40) = False Then allowed = False
        Else
            Dim sFullPath() As String = MailFolders.SelectedNode.Tag.ToString.Split(New Char() {"\"})
            If currentDroitAcces(42) = False And sFullPath(1) <> currentUserName & " (" & ConnectionsManager.currentUser & ")" Then
                allowed = False
            End If
        End If

        'If fromRow Then
        Dim hasOneRead As Boolean = False
        Dim hasOneUnRead As Boolean = False
        For i As Integer = 0 To MessagesList.SelectedRows.Count - 1
            Dim thatMail As Mail = curFolderMails(MessagesList.SelectedRows(i).Cells("NoMail").Value)
            If thatMail.isRead Then
                hasOneRead = True
            Else
                hasOneUnRead = True
            End If

            If hasOneUnRead And hasOneRead Then Exit For
        Next i

        Dim curMail As Mail = Nothing
        If curRowMessage IsNot Nothing AndAlso curRowMessage.DataGridView IsNot Nothing AndAlso curFolderMails.ContainsKey(curRowMessage.Cells("NoMail").Value) Then curMail = curFolderMails(curRowMessage.Cells("NoMail").Value)
        menuUnread.Enabled = fromRow AndAlso IIf(allowed, hasOneRead, False)
        menuRead.Enabled = fromRow AndAlso IIf(allowed, hasOneUnRead, False)
        menuCopier.Enabled = fromRow AndAlso allowed
        menuCouper.Enabled = fromRow AndAlso allowed
        menuDelMSG.Enabled = fromRow AndAlso allowed
        menuSeeMSG.Enabled = fromRow
        menuRepondre.Enabled = fromRow
        menuTransferMSG.Enabled = fromRow
        menuRepondreAll.Enabled = fromRow AndAlso curMail IsNot Nothing AndAlso curMail.cc.ToString <> ""
        menuViewSource.Enabled = fromRow AndAlso curMail IsNot Nothing AndAlso curMail.hasSource
        'Else
        'menuViewSource.Enabled = False
        'menuCopier.Enabled = False
        'menuCouper.Enabled = False
        'menuSeeMSG.Enabled = False
        'menuDelMSG.Enabled = False
        'menuRead.Enabled = False
        'menuUnread.Enabled = False
        'menuRepondre.Enabled = False
        'menuRepondre.Enabled = False
        'menuRepondreAll.Enabled = False
        'End If

        If IO.Directory.Exists(Mail.lastCopyPath) = False Then
            menuColler.Enabled = False
        Else
            menuColler.Enabled = allowed
        End If

        menuAddFolder2.Enabled = allowed

        menuMessages.Show(MessagesList, messageslistMousePosition)
    End Sub

    Private Sub messageInHTML_TextChanged(ByVal theText As String) Handles MessageInHTML.textChanged
        'MessagesList.Focus()
    End Sub

    Private Sub messagesList_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MessagesList.LostFocus
        'If Me.MessagesList.ContainsFocus Then MessagesList.Focus()
    End Sub

    Private Sub messagesList_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles MessagesList.Sorted
        With MessagesList
            If .SortOrder = SortOrder.Ascending Then
                menuDownSorting.Checked = False
                menuUpSorting.Checked = True
            Else
                menuDownSorting.Checked = True
                menuUpSorting.Checked = False
            End If
        End With
    End Sub

    Private Sub messagesList_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MessagesList.KeyUp

    End Sub

    Private Sub messagesList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MessagesList.KeyDown
        If e.KeyCode = Keys.Enter Then
            messagesList_CellMouseDoubleClick(sender, New System.Windows.Forms.DataGridViewCellMouseEventArgs(0, curRowMessage.Index, MousePosition.X, MousePosition.Y, New MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 2, MousePosition.X, MousePosition.Y, 0)))
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub msgSystem_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Splitter1.SplitPosition > (Me.DisplayRectangle.Width - Splitter1.MinExtra) Then
            Splitter1.SplitPosition = Me.DisplayRectangle.Width - Splitter1.MinExtra
        End If
        If Splitter2.SplitPosition > (Me.DisplayRectangle.Height - Splitter2.MinExtra) Then
            Splitter2.SplitPosition = Me.DisplayRectangle.Height - Splitter2.MinExtra
        End If
    End Sub

    Private Sub messageInHTML_TextMouseMove(ByVal e As mshtml.IHTMLEventObj) Handles MessageInHTML.textMouseMove
        e = e
    End Sub

    Private Sub messageInHTML_TextMouseOut(ByVal e As mshtml.IHTMLEventObj) Handles MessageInHTML.textMouseOut
        e = e
    End Sub

    Private Sub messageInHTML_TextMouseOver(ByVal e As mshtml.IHTMLEventObj) Handles MessageInHTML.textMouseOver
        'Dim a As Boolean = MessageInHTML.Focus()
    End Sub

    Private Sub messageInHTML_TextPreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles MessageInHTML.textPreviewKeyDown
        If MessagesList.SelectedRows.Count = 0 Then Exit Sub

        Dim curIndex As Integer = MessagesList.SelectedRows(0).Index
        If curIndex <> (MessagesList.Rows.Count - 1) AndAlso e.KeyCode = Keys.Down Then
            curIndex += 1

            If (Control.ModifierKeys And Keys.Shift) = False Then
                MessagesList.ClearSelection()
                If Control.ModifierKeys = Keys.Control Then
                    curIndex = MessagesList.Rows.Count - 1
                    MessagesList.Rows(curIndex).Selected = True
                Else
                    MessagesList.Rows(curIndex).Selected = True
                End If
            Else
                If (Control.ModifierKeys And Keys.Control) = False Then
                    If MessagesList.Rows(curIndex).Selected Then curIndex -= 1
                    MessagesList.Rows(curIndex).Selected = Not MessagesList.Rows(curIndex).Selected
                Else
                    MessagesList.ClearSelection()
                    For i As Integer = MessagesList.Rows.Count - 1 To curIndex - 1 Step -1
                        MessagesList.Rows(i).Selected = True
                    Next i
                End If
            End If

            If MessagesList.GetCellDisplayRectangle(0, curIndex, True).Height <> MessagesList.Rows(curIndex).Height Then MessagesList.FirstDisplayedScrollingRowIndex = curIndex
        End If
        If curIndex <> 0 AndAlso e.KeyCode = Keys.Up Then
            curIndex -= 1

            If (Control.ModifierKeys And Keys.Shift) = False Then
                MessagesList.ClearSelection()
                If Control.ModifierKeys = Keys.Control Then
                    MessagesList.Rows(0).Selected = True
                    curIndex = 0
                Else
                    MessagesList.Rows(curIndex).Selected = True
                End If
            Else
                If (Control.ModifierKeys And Keys.Control) = False Then
                    If MessagesList.Rows(curIndex).Selected Then curIndex += 1
                    MessagesList.Rows(curIndex).Selected = Not MessagesList.Rows(curIndex).Selected
                Else
                    MessagesList.ClearSelection()
                    For i As Integer = 0 To curIndex + 1
                        MessagesList.Rows(i).Selected = True
                    Next i
                End If
            End If

            If MessagesList.GetCellDisplayRectangle(0, curIndex, True).Height <> MessagesList.Rows(curIndex).Height Then MessagesList.FirstDisplayedScrollingRowIndex = curIndex
        End If
    End Sub

    Private Sub messagesList_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MessagesList.MouseEnter
        If Me.MdiParent.ActiveMdiChild Is Me Then MessagesList.Focus()
    End Sub

    Private Sub messagesList_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MessagesList.MouseHover
        'If Me.MdiParent.ActiveMdiChild Is Me Then MessagesList.Focus()
    End Sub

    Private Sub messagesList_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MessagesList.MouseMove
        If MessagesList.Focused = False AndAlso Me.MdiParent.ActiveMdiChild Is Me Then MessagesList.Focus()
    End Sub

    Private Sub tlbBtnGetMessages_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbBtnGetMessages.ButtonClick
        checkAccountsEmail()
    End Sub

    Private Sub hidingStatusTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hidingStatusTimer.Tick
        tlbStatus.Visible = False
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function.StartsWith("MessagesList") AndAlso currentNoMailFolder = dataReceived.params(0) Then
            If dataReceived.function <> "MessagesList-IsRead" OrElse dataReceived.fromExternal Then
                loadMessagesList()
            End If
        End If

        If dataReceived.function.StartsWith("FLB-" & MailsManager.getInstance.folderType.ToString) Then
            loadFolders()
        End If
    End Sub

    Private Sub msgSystem_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        isShown = True

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Shown - " & (Date.Now.Subtract(starting).TotalMilliseconds)
    End Sub
End Class
