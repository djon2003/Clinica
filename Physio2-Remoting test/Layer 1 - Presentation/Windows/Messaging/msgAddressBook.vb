Friend Class msgAddressBook
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin

        'Apply user settings
        Dim setting As String = UsersManager.currentUser.settings.mailContact
        If setting <> "" Then
            Dim mySettings() As String = setting.Split(New Char() {"§"})
            Me.Height = mySettings(0)
            Me.Width = mySettings(1)
            TheSplitter.SplitPosition = mySettings(2)
            Me.selectedContact = mySettings(3)
            ContactsList.VirtualMode = False
            ContactsList.Sort(ContactsList.Columns(mySettings(4)), IIf(mySettings(5) = "D", 1, 0))
        End If

        'Draw title bars
        Me.StatusBarPanel1.Icon = DrawingManager.getInstance.getIcon("FolderClosed.ico")
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance.getImage("carnet.gif"))

        With Dossiers.ImageList.Images
            .Add(DrawingManager.getInstance.getImage("user.gif"))
            .Add(DrawingManager.getInstance.getIcon("client16.ico"))
            .Add(DrawingManager.getInstance.getIcon("KP16.ico"))
        End With
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents menuDossiers As System.Windows.Forms.ContextMenu
    Friend WithEvents menuAddFolder As System.Windows.Forms.MenuItem
    Friend WithEvents menuDelFolder As System.Windows.Forms.MenuItem
    Friend WithEvents menuContacts As System.Windows.Forms.ContextMenu
    Friend WithEvents menuAddContact As System.Windows.Forms.MenuItem
    Friend WithEvents menuDelContact As System.Windows.Forms.MenuItem
    Friend WithEvents menuModifContact As System.Windows.Forms.MenuItem
    Friend WithEvents menuSelectContact As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuAddFolder2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuCutLine As System.Windows.Forms.MenuItem
    Friend WithEvents menuCouper As System.Windows.Forms.MenuItem
    Friend WithEvents menuCopier As System.Windows.Forms.MenuItem
    Friend WithEvents menuColler As System.Windows.Forms.MenuItem
    Friend WithEvents menuAffichage As System.Windows.Forms.MenuItem
    Friend WithEvents menuUpSorting As System.Windows.Forms.MenuItem
    Friend WithEvents menuDownSorting As System.Windows.Forms.MenuItem
    Friend WithEvents LeftPanel As System.Windows.Forms.Panel
    Friend WithEvents TitleContact As System.Windows.Forms.StatusBar
    Friend WithEvents TheSplitter As System.Windows.Forms.Splitter
    Friend WithEvents TitleFolder As System.Windows.Forms.StatusBar
    Friend WithEvents Dossiers As Clinica.TreeViewPlus
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents noContacts As System.Windows.Forms.Label
    Friend WithEvents ContactsList As DataGridPlus
    Friend WithEvents menuAddClient As System.Windows.Forms.MenuItem
    Friend WithEvents menuAddKP As System.Windows.Forms.MenuItem
    Friend WithEvents menuOpenAccount As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents filterSortedColumn As System.Windows.Forms.TextBox
    Friend WithEvents menuAddUser As System.Windows.Forms.MenuItem
    Friend WithEvents menuSendEmail As System.Windows.Forms.MenuItem
    Friend WithEvents Afficher As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Courriel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents URL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoContact As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Surnom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Courriels As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextMsgOnly As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Adresse As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ville As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodePostal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pays As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Telephones As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoClient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoKP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoUser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents menuValid As System.Windows.Forms.MenuItem
    Friend WithEvents menuValidSelected As System.Windows.Forms.MenuItem
    Friend WithEvents menuValidList As System.Windows.Forms.MenuItem
    Friend WithEvents menuValidFolder As System.Windows.Forms.MenuItem
    Friend WithEvents menuValidAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuValidOnlyRapport As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.menuDossiers = New System.Windows.Forms.ContextMenu
        Me.menuAddFolder = New System.Windows.Forms.MenuItem
        Me.menuDelFolder = New System.Windows.Forms.MenuItem
        Me.menuContacts = New System.Windows.Forms.ContextMenu
        Me.menuAffichage = New System.Windows.Forms.MenuItem
        Me.menuUpSorting = New System.Windows.Forms.MenuItem
        Me.menuDownSorting = New System.Windows.Forms.MenuItem
        Me.menuAddClient = New System.Windows.Forms.MenuItem
        Me.menuAddKP = New System.Windows.Forms.MenuItem
        Me.menuAddUser = New System.Windows.Forms.MenuItem
        Me.menuAddContact = New System.Windows.Forms.MenuItem
        Me.menuSendEmail = New System.Windows.Forms.MenuItem
        Me.menuOpenAccount = New System.Windows.Forms.MenuItem
        Me.menuModifContact = New System.Windows.Forms.MenuItem
        Me.menuDelContact = New System.Windows.Forms.MenuItem
        Me.menuSelectContact = New System.Windows.Forms.MenuItem
        Me.menuCutLine = New System.Windows.Forms.MenuItem
        Me.menuCouper = New System.Windows.Forms.MenuItem
        Me.menuCopier = New System.Windows.Forms.MenuItem
        Me.menuColler = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.menuAddFolder2 = New System.Windows.Forms.MenuItem
        Me.menuValid = New System.Windows.Forms.MenuItem
        Me.menuValidSelected = New System.Windows.Forms.MenuItem
        Me.menuValidList = New System.Windows.Forms.MenuItem
        Me.menuValidFolder = New System.Windows.Forms.MenuItem
        Me.menuValidAll = New System.Windows.Forms.MenuItem
        Me.LeftPanel = New System.Windows.Forms.Panel
        Me.Dossiers = New Clinica.TreeViewPlus
        Me.TitleFolder = New System.Windows.Forms.StatusBar
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel
        Me.TitleContact = New System.Windows.Forms.StatusBar
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel
        Me.TheSplitter = New System.Windows.Forms.Splitter
        Me.noContacts = New System.Windows.Forms.Label
        Me.ContactsList = New DataGridPlus
        Me.Afficher = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Courriel = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Nom = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.URL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoContact = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Surnom = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Courriels = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TextMsgOnly = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Adresse = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Ville = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CodePostal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Pays = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Telephones = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoClient = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoKP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoUser = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.filterSortedColumn = New System.Windows.Forms.TextBox
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.menuValidOnlyRapport = New System.Windows.Forms.MenuItem
        Me.LeftPanel.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContactsList, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'menuContacts
        '
        Me.menuContacts.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAffichage, Me.menuAddClient, Me.menuAddKP, Me.menuAddUser, Me.menuAddContact, Me.menuSendEmail, Me.menuOpenAccount, Me.menuModifContact, Me.menuDelContact, Me.menuSelectContact, Me.menuCutLine, Me.menuCouper, Me.menuCopier, Me.menuColler, Me.MenuItem1, Me.menuAddFolder2, Me.menuValid})
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
        'menuAddClient
        '
        Me.menuAddClient.Index = 1
        Me.menuAddClient.Text = "Ajouter un compte client"
        '
        'menuAddKP
        '
        Me.menuAddKP.Index = 2
        Me.menuAddKP.Text = "Ajouter un compte personne / organisme clé"
        '
        'menuAddUser
        '
        Me.menuAddUser.Index = 3
        Me.menuAddUser.Text = "Ajouter un compte utilisateur"
        '
        'menuAddContact
        '
        Me.menuAddContact.Index = 4
        Me.menuAddContact.Text = "Ajouter un contact"
        '
        'menuSendEmail
        '
        Me.menuSendEmail.Index = 5
        Me.menuSendEmail.Text = "Envoyer un courriel"
        '
        'menuOpenAccount
        '
        Me.menuOpenAccount.DefaultItem = True
        Me.menuOpenAccount.Index = 6
        Me.menuOpenAccount.Text = "Ouvrir le compte"
        '
        'menuModifContact
        '
        Me.menuModifContact.Index = 7
        Me.menuModifContact.Text = "Modifier le contact"
        '
        'menuDelContact
        '
        Me.menuDelContact.Index = 8
        Me.menuDelContact.Text = "Supprimer le contact"
        '
        'menuSelectContact
        '
        Me.menuSelectContact.Enabled = False
        Me.menuSelectContact.Index = 9
        Me.menuSelectContact.Text = "Sélectionner le(s) contact(s)"
        '
        'menuCutLine
        '
        Me.menuCutLine.Index = 10
        Me.menuCutLine.Text = "-"
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
        'menuValid
        '
        Me.menuValid.Index = 16
        Me.menuValid.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuValidSelected, Me.menuValidList, Me.menuValidFolder, Me.menuValidAll, Me.MenuItem2, Me.menuValidOnlyRapport})
        Me.menuValid.Text = "Valider les adresses de courriel..."
        '
        'menuValidSelected
        '
        Me.menuValidSelected.Index = 0
        Me.menuValidSelected.Text = "... sélectionnées"
        '
        'menuValidList
        '
        Me.menuValidList.Index = 1
        Me.menuValidList.Text = "... de la liste en cours"
        '
        'menuValidFolder
        '
        Me.menuValidFolder.Index = 2
        Me.menuValidFolder.Text = "... du dossier en cours"
        '
        'menuValidAll
        '
        Me.menuValidAll.Index = 3
        Me.menuValidAll.Text = "Toutes"
        '
        'LeftPanel
        '
        Me.LeftPanel.Controls.Add(Me.Dossiers)
        Me.LeftPanel.Controls.Add(Me.TitleFolder)
        Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.LeftPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftPanel.Name = "LeftPanel"
        Me.LeftPanel.Size = New System.Drawing.Size(192, 399)
        Me.LeftPanel.TabIndex = 15
        '
        'Dossiers
        '
        Me.Dossiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dossiers.expandAllNodes = False
        Me.Dossiers.HideSelection = False
        Me.Dossiers.ImageIndex = 0
        Me.Dossiers.Location = New System.Drawing.Point(0, 28)
        Me.Dossiers.Name = "Dossiers"
        Me.Dossiers.SelectedImageIndex = 0
        Me.Dossiers.Size = New System.Drawing.Size(192, 371)
        Me.Dossiers.Sorted = True
        Me.Dossiers.TabIndex = 19
        Me.Dossiers.tree = Nothing
        '
        'TitleFolder
        '
        Me.TitleFolder.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleFolder.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleFolder.Location = New System.Drawing.Point(0, 0)
        Me.TitleFolder.Name = "TitleFolder"
        Me.TitleFolder.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1})
        Me.TitleFolder.ShowPanels = True
        Me.TitleFolder.Size = New System.Drawing.Size(192, 28)
        Me.TitleFolder.SizingGrip = False
        Me.TitleFolder.TabIndex = 18
        Me.TitleFolder.Text = "StatusBar2"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "Dossiers"
        Me.StatusBarPanel1.Width = 192
        '
        'TitleContact
        '
        Me.TitleContact.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleContact.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleContact.Location = New System.Drawing.Point(194, 0)
        Me.TitleContact.Name = "TitleContact"
        Me.TitleContact.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel2})
        Me.TitleContact.ShowPanels = True
        Me.TitleContact.Size = New System.Drawing.Size(494, 28)
        Me.TitleContact.SizingGrip = False
        Me.TitleContact.TabIndex = 17
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Contacts"
        Me.StatusBarPanel2.ToolTipText = "Liste des contacts du dossier présentement sélectionné"
        Me.StatusBarPanel2.Width = 494
        '
        'TheSplitter
        '
        Me.TheSplitter.BackColor = System.Drawing.SystemColors.Control
        Me.TheSplitter.Location = New System.Drawing.Point(192, 0)
        Me.TheSplitter.MinExtra = 375
        Me.TheSplitter.MinSize = 120
        Me.TheSplitter.Name = "TheSplitter"
        Me.TheSplitter.Size = New System.Drawing.Size(2, 399)
        Me.TheSplitter.TabIndex = 20
        Me.TheSplitter.TabStop = False
        '
        'noContacts
        '
        Me.noContacts.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.noContacts.AutoSize = True
        Me.noContacts.BackColor = System.Drawing.SystemColors.Window
        Me.noContacts.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.noContacts.Location = New System.Drawing.Point(373, 195)
        Me.noContacts.Name = "noContacts"
        Me.noContacts.Size = New System.Drawing.Size(125, 20)
        Me.noContacts.TabIndex = 24
        Me.noContacts.Text = "Aucun contact"
        '
        'ContactsList
        '
        Me.ContactsList.AllowUserToAddRows = False
        Me.ContactsList.AllowUserToDeleteRows = False
        Me.ContactsList.AllowUserToOrderColumns = True
        Me.ContactsList.AllowUserToResizeRows = False
        Me.ContactsList.BackgroundColor = System.Drawing.SystemColors.Window
        Me.ContactsList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.ContactsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ContactsList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Afficher, Me.Courriel, Me.Nom, Me.URL, Me.NoContact, Me.Surnom, Me.Courriels, Me.TextMsgOnly, Me.Adresse, Me.Ville, Me.CodePostal, Me.Pays, Me.Telephones, Me.NoClient, Me.NoKP, Me.NoUser})
        Me.ContactsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContactsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.ContactsList.Location = New System.Drawing.Point(194, 28)
        Me.ContactsList.MinimumSize = New System.Drawing.Size(0, 148)
        Me.ContactsList.Name = "ContactsList"
        Me.ContactsList.RowHeadersVisible = False
        Me.ContactsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ContactsList.Size = New System.Drawing.Size(494, 371)
        Me.ContactsList.TabIndex = 23
        Me.ContactsList.VirtualMode = True
        '
        'Afficher
        '
        Me.Afficher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Afficher.DataPropertyName = "Afficher"
        Me.Afficher.HeaderText = "Afficher"
        Me.Afficher.Name = "Afficher"
        Me.Afficher.Width = 68
        '
        'Courriel
        '
        Me.Courriel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Courriel.DataPropertyName = "Courriel"
        Me.Courriel.HeaderText = "Courriel"
        Me.Courriel.MinimumWidth = 200
        Me.Courriel.Name = "Courriel"
        '
        'Nom
        '
        Me.Nom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Nom.DataPropertyName = "Nom"
        Me.Nom.HeaderText = "Nom"
        Me.Nom.Name = "Nom"
        Me.Nom.Width = 54
        '
        'URL
        '
        Me.URL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.URL.DataPropertyName = "URL"
        Me.URL.HeaderText = "Site internet"
        Me.URL.Name = "URL"
        Me.URL.Width = 88
        '
        'NoContact
        '
        Me.NoContact.DataPropertyName = "NoContact"
        Me.NoContact.HeaderText = "NoContact"
        Me.NoContact.Name = "NoContact"
        Me.NoContact.Visible = False
        '
        'Surnom
        '
        Me.Surnom.DataPropertyName = "Surnom"
        Me.Surnom.HeaderText = "Surnom"
        Me.Surnom.Name = "Surnom"
        Me.Surnom.Visible = False
        '
        'Courriels
        '
        Me.Courriels.DataPropertyName = "Courriels"
        Me.Courriels.HeaderText = "Courriels"
        Me.Courriels.Name = "Courriels"
        Me.Courriels.Visible = False
        '
        'TextMsgOnly
        '
        Me.TextMsgOnly.DataPropertyName = "TextMsgOnly"
        Me.TextMsgOnly.HeaderText = "TextMsgOnly"
        Me.TextMsgOnly.Name = "TextMsgOnly"
        Me.TextMsgOnly.Visible = False
        '
        'Adresse
        '
        Me.Adresse.DataPropertyName = "Adresse"
        Me.Adresse.HeaderText = "Adresse"
        Me.Adresse.Name = "Adresse"
        Me.Adresse.Visible = False
        '
        'Ville
        '
        Me.Ville.DataPropertyName = "Ville"
        Me.Ville.HeaderText = "Ville"
        Me.Ville.Name = "Ville"
        Me.Ville.Visible = False
        '
        'CodePostal
        '
        Me.CodePostal.DataPropertyName = "CodePostal"
        Me.CodePostal.HeaderText = "CodePostal"
        Me.CodePostal.Name = "CodePostal"
        Me.CodePostal.Visible = False
        '
        'Pays
        '
        Me.Pays.DataPropertyName = "Pays"
        Me.Pays.HeaderText = "Pays"
        Me.Pays.Name = "Pays"
        Me.Pays.Visible = False
        '
        'Telephones
        '
        Me.Telephones.DataPropertyName = "Telephones"
        Me.Telephones.HeaderText = "Telephones"
        Me.Telephones.Name = "Telephones"
        Me.Telephones.Visible = False
        '
        'NoClient
        '
        Me.NoClient.DataPropertyName = "NoClient"
        Me.NoClient.HeaderText = "NoClient"
        Me.NoClient.Name = "NoClient"
        Me.NoClient.Visible = False
        '
        'NoKP
        '
        Me.NoKP.DataPropertyName = "NoKP"
        Me.NoKP.HeaderText = "NoKP"
        Me.NoKP.Name = "NoKP"
        Me.NoKP.Visible = False
        '
        'NoUser
        '
        Me.NoUser.DataPropertyName = "NoUser"
        Me.NoUser.HeaderText = "NoUser"
        Me.NoUser.Name = "NoUser"
        Me.NoUser.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(411, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Filtrer la colonne triée par :"
        '
        'filterSortedColumn
        '
        Me.filterSortedColumn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.filterSortedColumn.Location = New System.Drawing.Point(548, 4)
        Me.filterSortedColumn.Name = "filterSortedColumn"
        Me.filterSortedColumn.Size = New System.Drawing.Size(134, 20)
        Me.filterSortedColumn.TabIndex = 26
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 4
        Me.MenuItem2.Text = "-"
        '
        'menuValidOnlyRapport
        '
        Me.menuValidOnlyRapport.Checked = True
        Me.menuValidOnlyRapport.Index = 5
        Me.menuValidOnlyRapport.Text = "Rapport seulement"
        '
        'msgAddressBook
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(688, 399)
        Me.Controls.Add(Me.filterSortedColumn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.noContacts)
        Me.Controls.Add(Me.ContactsList)
        Me.Controls.Add(Me.TitleContact)
        Me.Controls.Add(Me.TheSplitter)
        Me.Controls.Add(Me.LeftPanel)
        Me.MinimumSize = New System.Drawing.Size(588, 248)
        Me.Name = "msgAddressBook"
        Me.ShowInTaskbar = False
        Me.Text = "Carnet d'adresses de courriel"
        Me.LeftPanel.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContactsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private contactsListSelectionChangedDisabled As Boolean = False
    Private _UseWinAsSelection As Boolean = False
    Private contactSelected As DataGridViewRow
    Private _From As Object
    Private curMouseBouton As MouseButtons
    Private contactslistMousePosition As Point
    Private _SelectedContact As String = ""
    Private foldersLoaded As Boolean = False
    Private selectingContact As Boolean = False
    Private contactWasSelected As Boolean = False

#Region "Propriétés"
    Public Property selectedContact() As String
        Get
            Return _SelectedContact
        End Get
        Set(ByVal value As String)
            _SelectedContact = value
            If foldersLoaded Then selectContact()
        End Set
    End Property

    Public Property useWinAsSelection() As Boolean
        Get
            Return _UseWinAsSelection
        End Get
        Set(ByVal Value As Boolean)
            _UseWinAsSelection = Value
            If Value = True Then
                menuSelectContact.Enabled = True
            Else
                menuSelectContact.Enabled = False
            End If
        End Set
    End Property

    Public Property from() As Object
        Get
            Return _From
        End Get
        Set(ByVal Value As Object)
            _From = Value
        End Set
    End Property
#End Region

    Private Sub selectFolder()
        Dossiers.selectNode(selectedContact.Substring(0, selectedContact.Length - getLastDir(selectedContact).Length - 1))
    End Sub

    Private Sub selectContact()
        selectingContact = True

        Dim noColumnName As String = "NoContact"
        If Dossiers.SelectedNode.FullPath.StartsWith("C") Then
            noColumnName = "NoClient"
        ElseIf Dossiers.SelectedNode.FullPath.StartsWith("P") Then
            noColumnName = "NoKP"
        ElseIf Dossiers.SelectedNode.FullPath = "Utilisateurs" Then
            noColumnName = "NoUser"
        End If

        ContactsList.SuspendLayout()
        ContactsList.ClearSelection()
        For i As Integer = 0 To ContactsList.Rows.Count - 1
            If ContactsList.Rows(i).Cells(noColumnName).Value = getLastDir(selectedContact) Then
                ContactsList.Rows(i).Selected = True
                ContactsList.currentCell = ContactsList.Rows(i).Cells(0)
                Exit For
            End If
        Next i
        ContactsList.ResumeLayout()
        selectingContact = False
    End Sub

    Private Sub sortClick(ByVal sender As Object, ByVal e As EventArgs) Handles menuDownSorting.Click, menuUpSorting.Click
        With ContactsList
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

    Public Sub loadContacts(Optional ByVal doReselection As Boolean = True)
        If Dossiers.SelectedNode Is Nothing Then Exit Sub

        Dim curContactFolder As ContactFolder = Dossiers.SelectedNode.Tag

        Dim msgSelected As DataGridViewSelectedRowCollection = ContactsList.SelectedRows
        Dim noMsgSelected As New ArrayList
        For i As Integer = 0 To msgSelected.Count - 1
            noMsgSelected.Add(msgSelected(i).Cells("NoContact").Value)
        Next i
        Dim orderingColIndex As Integer = ContactsList.SortedColumn.Index
        Dim order As SortOrder = ContactsList.SortOrder
        If Dossiers.SelectedNode.FullPath.StartsWith("C") Then
            ContactsList.DataSource = DBLinker.getInstance().readDBForGrid("InfoClients LEFT JOIN Villes ON InfoClients.NoVille=Villes.NoVille", "Prenom + ' ' + Nom   AS Afficher,Courriel,Nom + ',' + Prenom AS Nom,URL,0 AS NoContact,'' AS Surnom,Courriel AS Courriels,0 AS TextMsgOnly,Adresse,NomVille AS Ville,CodePostal,'' AS Pays,Telephones,NoClient,0 as NoKP, 0 AS NoUser", "WHERE Courriel<>''").Tables(0).DefaultView
        ElseIf Dossiers.SelectedNode.FullPath.StartsWith("P") Then
            ContactsList.DataSource = DBLinker.getInstance().readDBForGrid("KeyPeople LEFT JOIN Villes ON KeyPeople.NoVille=Villes.NoVille", "Nom AS Afficher,Courriel,Nom,URL,0 AS NoContact,'' AS Surnom,Courriel AS Courriels,0 AS TextMsgOnly,Adresse,NomVille AS Ville,CodePostal,'' AS Pays,Telephones,0 AS NoClient,NoKP,0 AS NoUser", "WHERE Courriel<>''").Tables(0).DefaultView
        ElseIf Dossiers.SelectedNode.FullPath = "Utilisateurs" Then
            If currentDroitAcces(102) Then
                ContactsList.DataSource = DBLinker.getInstance().readDBForGrid("Utilisateurs LEFT JOIN Villes ON Utilisateurs.NoVille=Villes.NoVille", "Prenom + ' ' + Nom AS Afficher,Courriel,Nom + ',' + Prenom AS Nom,URL,0 AS NoContact,'' AS Surnom,Courriel AS Courriels,0 AS TextMsgOnly,Adresse,NomVille AS Ville,CodePostal,'' AS Pays,Telephones,0 AS NoClient,0 AS NoKP, NoUser", "WHERE Courriel<>''").Tables(0).DefaultView
            Else
                ContactsList.DataSource = DBLinker.getInstance().readDBForGrid("Utilisateurs LEFT JOIN Villes ON Utilisateurs.NoVille=Villes.NoVille", "Prenom + ' ' + Nom AS Afficher,Courriel,Nom + ',' + Prenom AS Nom,URL,0 AS NoContact,'' AS Surnom,Courriel AS Courriels,0 AS TextMsgOnly,Adresse,NomVille AS Ville,CodePostal,'' AS Pays,Telephones,0 AS NoClient,0 AS NoKP, NoUser", "WHERE Courriel<>'' AND 1=2").Tables(0).DefaultView
            End If
        Else
            ContactsList.DataSource = DBLinker.getInstance().readDBForGrid("Contacts INNER JOIN ContactFolders ON ContactFolders.NoContactFolder=Contacts.NoContactFolder LEFT JOIN Villes ON Contacts.NoVille=Villes.NoVille LEFT JOIN Pays ON Pays.NoPays =Contacts.NoPays", "Afficher,Courriel,Nom + ',' + Prenom AS Nom,URL,NoContact,Surnom,Courriels,TextMsgOnly,Adresse,NomVille AS Ville,CodePostal,Pays,Telephones,NoClient,NoKP,0 AS NoUser", "WHERE Contacts.NoContactFolder=" & curContactFolder.noContactFolder).Tables(0).DefaultView
        End If

        If order = SortOrder.Ascending Then
            ContactsList.Sort(ContactsList.Columns(orderingColIndex), System.ComponentModel.ListSortDirection.Ascending)
        Else
            ContactsList.Sort(ContactsList.Columns(orderingColIndex), System.ComponentModel.ListSortDirection.Descending)
        End If

        If filterSortedColumn.Text <> "" Then CType(ContactsList.DataSource, DataView).RowFilter = ContactsList.SortedColumn.DataPropertyName & " LIKE '%" & filterSortedColumn.Text.Replace("'", "''") & "%'"

        If CType(ContactsList.DataSource, DataView).Count = 0 Then
            noContacts.Visible = True
        Else
            noContacts.Visible = False
            If doReselection AndAlso noMsgSelected.Count <> 0 Then
                For i As Integer = 0 To ContactsList.Rows.Count - 1
                    If noMsgSelected.Contains(ContactsList.Rows(i).Cells("NoContact").Value) Then
                        ContactsList.Rows(i).Selected = True
                    Else
                        ContactsList.Rows(i).Selected = False
                    End If
                Next i
            End If
        End If
    End Sub

    Public Sub loadFolders()
        If ConnectionsManager.currentUser = 0 Then MessageBox.Show("L'administrateur n'a pas accès à ce secteur.Merci!", "Administrateur interdit") : Exit Sub


        Dim curcontactfolders() As ContactFolder
        Dim curContactFolders2 As Generic.List(Of ContactFolder) = ContactsManager.getInstance.getContactFolders
        ReDim curcontactfolders(curContactFolders2.Count)

        curcontactfolders(0) = New ContactFolder()
        curcontactfolders(0).noUser = -1
        curcontactfolders(0).contactFolder = "Utilisateurs"
        curcontactfolders(0).iconIndex = 2
        curcontactfolders(0).iconSelectedIndex = 2

        Dim curIndex As Byte = 0
        If currentDroitAcces(100) Then
            curIndex += 1
            ReDim Preserve curcontactfolders(curcontactfolders.Length)
            curcontactfolders(curIndex) = New ContactFolder()
            curcontactfolders(curIndex).noUser = -1
            curcontactfolders(curIndex).contactFolder = "Clients"
            curcontactfolders(curIndex).iconIndex = 3
            curcontactfolders(curIndex).iconSelectedIndex = 3
        End If
        If currentDroitAcces(101) Then
            curIndex += 1
            ReDim Preserve curcontactfolders(curcontactfolders.Length)
            curcontactfolders(curIndex) = New ContactFolder()
            curcontactfolders(curIndex).noUser = -1
            curcontactfolders(curIndex).contactFolder = "Personnes/organismes clés"
            curcontactfolders(curIndex).iconIndex = 4
            curcontactfolders(curIndex).iconSelectedIndex = 4
        End If

        curContactFolders2.CopyTo(0, curcontactfolders, 1 + curIndex, curContactFolders2.Count)

        Dossiers.tree = curcontactfolders
        Dim expandedTree As TreeNode = Nothing
        If Not Dossiers.SelectedNode Is Nothing Then expandedTree = Dossiers.SelectedNode
        Dossiers.refreshTree(expandedTree)
    End Sub

    Private Sub msgAddressBook_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub msgAddressBook_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Enregistrement de l'affichage personnalisée de la réception de messages
        Dim curNoContact As Integer = 0
        Try
            If Dossiers.SelectedNode.FullPath.StartsWith("C") Then
                curNoContact = Me.ContactsList.currentRow.Cells("NoClient").Value
            ElseIf Dossiers.SelectedNode.FullPath.StartsWith("P") Then
                curNoContact = Me.ContactsList.currentRow.Cells("NoKP").Value
            ElseIf Dossiers.SelectedNode.FullPath = "Utilisateurs" Then
                curNoContact = Me.ContactsList.currentRow.Cells("NoUser").Value
            Else
                curNoContact = Me.ContactsList.currentRow.Cells("NoContact").Value
            End If
        Catch
            'No message in the list
        End Try
        Dim curContact As String = ""
        If Me.Dossiers.SelectedNode IsNot Nothing Then curContact = Me.Dossiers.SelectedNode.FullPath & "\" & curNoContact

        Dim curUser As User = UsersManager.currentUser
        curUser.settings.mailContact = Me.Height & "§" & Me.Width & "§" & TheSplitter.SplitPosition & "§" & curContact & "§" & ContactsList.SortedColumn.Name & "§" & ContactsList.SortOrder.ToString.Substring(0, 1)
        curUser.settings.saveData()
    End Sub

    Private Sub msgAddressBook_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus

    End Sub

    Private Sub msgAddressBook_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ContactsList.VirtualMode = False
        If ContactsList.SortedColumn Is Nothing Then ContactsList.Sort(ContactsList.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        menuDownSorting.Checked = True
        menuUpSorting.Checked = False

        loadFolders()

        If selectedContact <> "" Then selectFolder()

        foldersLoaded = True
    End Sub

    Private Sub Dossiers_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Dossiers.MouseClick
        Dim baseUserPath As String = ""
        Dim myNoUser As Integer = -1

        If Dossiers.GetNodeAt(New Point(e.X, e.Y)) Is Nothing Then Exit Sub
        If Dossiers.GetNodeAt(New Point(e.X, e.Y)).Text <> "" Then
            Dossiers.SelectedNode = Dossiers.GetNodeAt(New Point(e.X, e.Y))
            Dim curPath As String = Dossiers.SelectedNode.FullPath

            If curPath = "Utilisateurs" Or curPath = "Clients" Or curPath.StartsWith("P") Then Exit Sub
            If curPath.StartsWith("U") Then
                Try
                    Dim sMyNoUser() As String = curPath.Split(New String() {"\"}, StringSplitOptions.None)
                    sMyNoUser = sMyNoUser(1).Split(New String() {" ("}, StringSplitOptions.None)
                    myNoUser = CInt(sMyNoUser(1).Substring(0, sMyNoUser(1).Length - 1))
                    baseUserPath = "Utilisateurs\" & UsersManager.getInstance.getUser(myNoUser).toString()
                Catch
                    curPath = ""
                End Try
            End If

            If curPath = "" Then Exit Sub

            If curPath = "Généraux" Or curPath = baseUserPath Then
                menuDelFolder.Enabled = False
            Else
                menuDelFolder.Enabled = True
            End If

            'Droit & Accès
            If (currentDroitAcces(35) = True And myNoUser = 0) Or (currentDroitAcces(37) = True And myNoUser <> 0) Or (currentDroitAcces(37) = False And myNoUser = ConnectionsManager.currentUser) Then
                'If allowed, show menu
                If e.Button = MouseButtons.Right Then menuDossiers.Show(Dossiers, New Point(e.X, e.Y))
            End If
        End If
    End Sub

    Private Sub menuAddFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddFolder.Click, menuAddFolder2.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ".§,§:§<§>§(§)"
        Dim myFolderName As String = myInputBoxPlus("Veuillez entrer le nom du nouveau dossier", "Nom du dossier")
        If myFolderName = "" Then Exit Sub

        Dim returnOfAdding As String = ContactsManager.getInstance.addContactFolder(Dossiers.SelectedNode.FullPath & "\" & myFolderName)
        If returnOfAdding <> "" Then
            MessageBox.Show(returnOfAdding, "Erreur")
            Exit Sub
        End If
    End Sub

    Private Sub menuDelFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDelFolder.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce dossier et ces contacts ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        CType(Me.Dossiers.SelectedNode.Tag, ContactFolder).delete()
        loadFolders()
    End Sub

    Private Sub menuAddContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddContact.Click
        Dim curContactFolder As ContactFolder = ContactsManager.getInstance.getContactFolder(Dossiers.SelectedNode.FullPath)

        Dim myMsgContact As msgContact = openUniqueWindow(New msgContact())
        myMsgContact.Visible = False
        myMsgContact.loading(curContactFolder.noContactFolder)
        myMsgContact.MdiParent = Nothing
        myMsgContact.StartPosition = FormStartPosition.CenterScreen
        myMsgContact.ShowDialog()
        loadContacts()
    End Sub

    Private Sub menuDelContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDelContact.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce contact ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        delContact()
    End Sub

    Private Sub delContact()
        Dim oneSelItem As DataGridViewRow

        Dim nosToDel As String = ""
        With ContactsList
            For Each oneSelItem In .SelectedRows
                nosToDel &= "," & oneSelItem.Cells("NoContact").Value
            Next
        End With
        If nosToDel <> "" Then
            nosToDel = nosToDel.Substring(1)
            DBLinker.getInstance.delDB("Contacts", "NoContact", "(" & nosToDel & ")", False, , , , , "IN")
        End If
        InternalUpdatesManager.getInstance.sendUpdate("Contacts(" & CType(Dossiers.SelectedNode.Tag, ContactFolder).noContactFolder & ")")

        loadContacts()
    End Sub

    Private Sub menuModifContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuModifContact.Click
        Dim curContactFolder As ContactFolder = ContactsManager.getInstance.getContactFolder(Dossiers.SelectedNode.FullPath)

        openContact(curContactFolder, contactSelected.Cells("NoContact").Value)

        loadContacts()
    End Sub

    Private Sub dossiers_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Dossiers.AfterSelect
        loadContacts(False)
    End Sub

    Private Sub contacts_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        sortClick(sender, EventArgs.Empty)
    End Sub

    Private Sub menuCopier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCopier.Click
        Dim myCopyPath As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\ContactCopy"
        deltree(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\ContactCopy")
        ensureGoodPath(myCopyPath)

        Dim oneSelItem As DataGridViewRow
        With ContactsList
            For Each oneSelItem In .SelectedRows
                Dim mycontent As String = ""
                For i As Short = 0 To oneSelItem.Cells.Count - 1
                    mycontent &= oneSelItem.Cells(i).Value.ToString & vbCrLf
                Next i
                IO.Directory.CreateDirectory(myCopyPath & bar(myCopyPath) & oneSelItem.Cells("NoContact").Value.ToString)
                IO.File.WriteAllText(myCopyPath & bar(myCopyPath) & oneSelItem.Cells("NoContact").Value.ToString & "\info", mycontent)
            Next
        End With
    End Sub

    Private Sub menuCouper_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCouper.Click
        menuCopier_Click(sender, EventArgs.Empty)
        delContact()
    End Sub

    Private Sub menuColler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuColler.Click
        Dim curContactFolder As ContactFolder = ContactsManager.getInstance.getContactFolder(Dossiers.SelectedNode.FullPath)

        Dim copyPath As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\ContactCopy"
        Dim msgToCopy() As String = IO.Directory.GetDirectories(copyPath)
        For i As Integer = 0 To msgToCopy.GetUpperBound(0)
            Dim myInfos() As String = readFile(msgToCopy(i) & "\info", , , False)
            Dim noms() As String = myInfos(2).Split(New Char() {","})
            '0Afficher,1Courriel,2Nom + ',' + Prenom AS Nom,3URL,4NoContact,5Surnom,6Courriels,7TextMsgOnly,8Adresse,9NomVille AS Ville,10CodePostal,11Pays,12Telephones,13NoClient,14NoKP
            DBLinker.getInstance.writeDB("Contacts", "NoContactFolder,Nom,Prenom,Surnom,Afficher,Courriel,Courriels,TextMsgOnly,Adresse,NoVille,CodePostal,NoPays,Telephones,URL,NoClient,NoKP", _
            curContactFolder.noContactFolder & ",'" & noms(0).Replace("'", "''") & "','" & noms(1).Replace("'", "''") & "','" & myInfos(5).Replace("'", "''") & "','" & myInfos(0).Replace("'", "''") & "','" & myInfos(1).Replace("'", "''") & "','" & _
            myInfos(6).Replace("'", "''") & "','" & myInfos(7) & "','" & myInfos(8).Replace("'", "''") & "'," & DBHelper.addItemToADBList("Villes", "NomVille", myInfos(9), "NoVille") & ",'" & myInfos(10) & "'," & _
            DBHelper.addItemToADBList("Pays", "Pays", myInfos(11), "NoPays") & ",'" & myInfos(12).Replace("'", "''") & "','" & myInfos(3).Replace("'", "''") & "'," & IIf(Integer.TryParse(myInfos(13), 0), myInfos(13), "null") & "," & IIf(Integer.TryParse(myInfos(14), 0), myInfos(14), "null"))
        Next i
        InternalUpdatesManager.getInstance.sendUpdate("Contacts(" & CType(Dossiers.SelectedNode.Tag, ContactFolder).noContactFolder & ")")

        loadContacts()
    End Sub

    Private Function getCourrielsSelected() As String()
        Dim courriels() As String = {}
        Dim n As Integer = 0

        With ContactsList
            ReDim courriels(.SelectedRows.Count - 1)
            For Each OneSelItem As DataGridViewRow In .SelectedRows
                courriels(n) = IIf(OneSelItem.Cells("Afficher").Value.ToString = "", "", OneSelItem.Cells("Afficher").Value.ToString & " <") & OneSelItem.Cells("Courriel").Value.ToString & IIf(OneSelItem.Cells("Afficher").Value.ToString = "", "", ">")
                n += 1
            Next
        End With

        Return courriels
    End Function

    Private Sub menuSelectContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSelectContact.Click
        Dim courriels() As String = getCourrielsSelected()

        redirectAddressBook(from, Dossiers.SelectedNode.FullPath, courriels)
        Me.Close()
    End Sub

    Private Sub contacts_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub contactsList_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ContactsList.CellMouseDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        contactSelected = ContactsList.Rows(e.RowIndex)

        If useWinAsSelection = True Then
            menuSelectContact_Click(sender, e)
        Else
            If contactSelected.Cells("NoContact").Value > 0 Then
                menuModifContact_Click(sender, e)
            Else
                menuOpenAccount_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub contactsList_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ContactsList.CellMouseDown
        curMouseBouton = e.Button
    End Sub

    Private Sub contactsList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ContactsList.KeyDown
        If e.KeyCode = Keys.Enter Then
            contactsList_CellMouseDoubleClick(sender, New System.Windows.Forms.DataGridViewCellMouseEventArgs(0, contactSelected.Index, MousePosition.X, MousePosition.Y, New MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 2, MousePosition.X, MousePosition.Y, 0)))
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub contactsList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ContactsList.MouseDown
        contactslistMousePosition = e.Location
        contactSelected = Nothing
        Dim hitInfo As DataGridView.HitTestInfo = ContactsList.HitTest(e.X, e.Y)
        If hitInfo.RowIndex <> -1 Then contactSelected = ContactsList.Rows(hitInfo.RowIndex)

        If e.Button = System.Windows.Forms.MouseButtons.Right AndAlso hitInfo.RowIndex = -1 Then showListContextMenu()
    End Sub

    Private Sub contactsList_RowContextMenuStripNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs) Handles ContactsList.RowContextMenuStripNeeded
        If Dossiers.SelectedNode Is Nothing Then Exit Sub

        Dim allowed As Boolean = True
        'Droit & Accès
        If Dossiers.SelectedNode.FullPath.StartsWith("G") Or Dossiers.SelectedNode.FullPath.StartsWith("C") Or Dossiers.SelectedNode.FullPath.StartsWith("P") Or Dossiers.SelectedNode.FullPath = "Utilisateurs" Then
            If currentDroitAcces(40) = False Then allowed = False
        Else
            Dim sFullPath() As String = Dossiers.SelectedNode.FullPath.Split(New Char() {"\"})
            If currentDroitAcces(42) = False And sFullPath(1) <> currentUserName & " (" & ConnectionsManager.currentUser & ")" Then
                allowed = False
            End If
        End If

        If curMouseBouton = MouseButtons.Right Then
            contactsListSelectionChangedDisabled = True
            contactSelected = ContactsList.Rows(e.RowIndex)
            If Control.ModifierKeys <> Keys.Control And contactSelected.Selected = False Then ContactsList.ClearSelection()
            contactSelected.Selected = True
            contactsListSelectionChangedDisabled = False
            showListContextMenu()
        End If
    End Sub



    Private Sub showListContextMenu()
        If Dossiers.SelectedNode Is Nothing Then Exit Sub

        menuAddFolder2.Visible = True
        MenuItem1.Visible = True
        menuAddContact.Visible = True
        menuCopier.Visible = True
        menuCouper.Visible = True
        menuColler.Visible = True
        menuCutLine.Visible = True
        menuModifContact.Visible = True
        menuDelContact.Visible = True
        menuOpenAccount.Visible = True
        menuAddKP.Visible = True
        menuAddClient.Visible = True
        menuAddUser.Visible = True


        menuAddContact.Enabled = True
        menuAddFolder2.Enabled = True

        If Dossiers.SelectedNode.FullPath.StartsWith("P") Or Dossiers.SelectedNode.FullPath.StartsWith("C") Or Dossiers.SelectedNode.FullPath = "Utilisateurs" Then
            menuAddContact.Visible = False
            menuCopier.Visible = False
            menuCouper.Visible = False
            menuColler.Visible = False
            menuCutLine.Visible = False
            menuModifContact.Visible = False
            menuDelContact.Visible = False
            menuAddFolder2.Visible = False
            MenuItem1.Visible = False
            If Dossiers.SelectedNode.FullPath.StartsWith("P") Then
                menuAddClient.Visible = False
                menuAddUser.Visible = False
            ElseIf Dossiers.SelectedNode.FullPath.StartsWith("U") Then
                menuAddKP.Visible = False
                menuAddClient.Visible = False
            Else
                menuAddKP.Visible = False
                menuAddUser.Visible = False
            End If
        Else
            menuOpenAccount.Visible = False
            menuAddKP.Visible = False
            menuAddClient.Visible = False
            menuAddUser.Visible = False
        End If

        If Not contactSelected Is Nothing Then
            menuCopier.Enabled = True
            menuCouper.Enabled = True
            menuModifContact.Enabled = True
            menuDelContact.Enabled = True
            menuOpenAccount.Enabled = True
            menuSendEmail.Enabled = True
            If useWinAsSelection = True Then menuSelectContact.Enabled = True
        Else
            menuSendEmail.Enabled = False
            menuCopier.Enabled = False
            menuCouper.Enabled = False
            menuSelectContact.Enabled = False
            menuModifContact.Enabled = False
            menuDelContact.Enabled = False
            menuOpenAccount.Enabled = False
        End If
        menuValidSelected.Enabled = ContactsList.SelectedRows.Count <> 0

        If IO.Directory.Exists(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\ContactCopy") = False Then
            menuColler.Enabled = False
        Else
            menuColler.Enabled = True
        End If

        If useWinAsSelection = True Then
            menuModifContact.DefaultItem = False
            menuOpenAccount.DefaultItem = False
            menuSelectContact.DefaultItem = True
        Else
            menuModifContact.DefaultItem = True
            menuOpenAccount.DefaultItem = True
            menuSelectContact.DefaultItem = False
        End If

        If Dossiers.SelectedNode.FullPath.IndexOf("Utilisateurs\") >= 0 Then
            Dim sMyNoUser() As String = Dossiers.SelectedNode.FullPath.Split(New Char() {"\"})
            Dim ssMyNoUser() As String = sMyNoUser(1).Split(New Char() {"("})
            Dim myNoUser As Integer = CInt(ssMyNoUser(1).Substring(0, ssMyNoUser(1).Length - 1))
            'Droit & Accès
            If currentDroitAcces(37) = False And myNoUser <> ConnectionsManager.currentUser Then
                'disable not allowed items
                menuAddContact.Enabled = False
                menuAddFolder2.Enabled = False
                menuCopier.Enabled = False
                menuCouper.Enabled = False
                menuSelectContact.Enabled = False
                menuModifContact.Enabled = False
                menuDelContact.Enabled = False
                menuColler.Enabled = False
            End If
        Else
            'Droit & Accès
            If currentDroitAcces(35) = False Then
                'disable not allowed items
                menuAddContact.Enabled = False
                menuAddFolder2.Enabled = False
                menuCopier.Enabled = False
                menuCouper.Enabled = False
                menuSelectContact.Enabled = False
                menuModifContact.Enabled = False
                menuDelContact.Enabled = False
                menuColler.Enabled = False
            End If
        End If


        menuContacts.Show(ContactsList, contactslistMousePosition)
    End Sub

    Private Sub menuOpenAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpenAccount.Click
        If contactSelected.Cells("NoKP").Value.ToString <> "" AndAlso contactSelected.Cells("NoKP").Value.ToString <> "0" Then
            openAccount(contactSelected.Cells("NoKP").Value, CompteType.KP)
        ElseIf contactSelected.Cells("NoUser").Value.ToString <> "" AndAlso contactSelected.Cells("NoUser").Value.ToString <> "0" Then
            openAccount(contactSelected.Cells("NoUser").Value, CompteType.User)
        Else
            openAccount(contactSelected.Cells("NoClient").Value)
        End If
    End Sub

    Private Sub menuAddClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddClient.Click
        Comptes.addClient(Me)
    End Sub

    Private Sub menuAddKP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddKP.Click
        Comptes.addKP("", "", "", "", "", "", "", "", "", "", "", Me)
    End Sub

    Private Sub filterSortedColumn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles filterSortedColumn.TextChanged
        If ContactsList.DataSource IsNot Nothing Then CType(ContactsList.DataSource, DataView).RowFilter = ContactsList.SortedColumn.DataPropertyName & " LIKE '%" & filterSortedColumn.Text.Replace("'", "''") & "%'"
    End Sub

    Private Sub contactsList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContactsList.SelectionChanged
        If contactsListSelectionChangedDisabled = False Then contactSelected = ContactsList.currentRow
        'If ContactWasSelected = False And FoldersLoaded = True And SelectingContact = False And SelectedContact <> "" Then
        '    ContactWasSelected = True
        '    SelectContact()
        'End If
    End Sub

    Private Sub menuAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAddUser.Click
        Comptes.addUser()
    End Sub

    Private Sub menuSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSendEmail.Click
        Dim courriels() As String = getCourrielsSelected()
        sendemailTo(String.Join(";", courriels) & ";")
    End Sub

    Private Sub contactsList_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContactsList.Sorted
        With ContactsList
            If .SortOrder = SortOrder.Ascending Then
                menuDownSorting.Checked = False
                menuUpSorting.Checked = True
            Else
                menuDownSorting.Checked = True
                menuUpSorting.Checked = False
            End If
        End With
    End Sub

    Private Sub msgAddressBook_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If TheSplitter.SplitPosition > (Me.DisplayRectangle.Width - TheSplitter.MinExtra) Then
            TheSplitter.SplitPosition = Me.DisplayRectangle.Width - TheSplitter.MinExtra
        End If
    End Sub

    Private Function getWhereForValidation(ByVal onlySelected As Boolean) As String
        If contactSelected Is Nothing Then Return "1=2"

        Dim where As String = ""
        Dim field As String = ""
        If contactSelected.Cells("NoKP").Value.ToString <> "" AndAlso contactSelected.Cells("NoKP").Value.ToString <> "0" Then
            field = "NoKP"
        ElseIf contactSelected.Cells("NoUser").Value.ToString <> "" AndAlso contactSelected.Cells("NoUser").Value.ToString <> "0" Then
            field = "NoUser"
        ElseIf contactSelected.Cells("NoClient").Value.ToString <> "" AndAlso contactSelected.Cells("NoClient").Value.ToString <> "0" Then
            field = "NoClient"
        Else
            field = "NoContact"
        End If

        For Each curRow As DataGridViewRow In ContactsList.Rows
            If onlySelected = False OrElse curRow.Selected Then where &= "," & curRow.Cells(field).Value
        Next
        If where <> "" Then where = field & " IN (" & where.Substring(1) & ")"

        Return where
    End Function

    Private Sub menuValidSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuValidSelected.Click
        Dim validable As IEmailsValidable = selectValidable()
        EmailValidator.validate(validable, getWhereForValidation(True), menuValidOnlyRapport.Checked)
    End Sub

    Private Sub menuValidList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuValidList.Click
        Dim validable As IEmailsValidable = selectValidable()
        EmailValidator.validate(validable, getWhereForValidation(False), menuValidOnlyRapport.Checked)
    End Sub

    Private Sub menuValidFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuValidFolder.Click
        Dim validable As IEmailsValidable = selectValidable()
        Dim where As String = ""
        If ContactsManager.getInstance.Equals(validable) Then where = "NoContactFolder=" & ContactsManager.getInstance.getContactFolder(Dossiers.SelectedNode.FullPath).noContactFolder
        EmailValidator.validate(validable, where, menuValidOnlyRapport.Checked)
    End Sub

    Private Sub menuValidAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuValidAll.Click
        EmailValidator.validate(Nothing, "", menuValidOnlyRapport.Checked)
    End Sub

    Private Function selectValidable() As IEmailsValidable
        Dim validable As IEmailsValidable
        If Dossiers.SelectedNode.FullPath.StartsWith("P") Then
            validable = KeyPeopleManager.getInstance()
        ElseIf Dossiers.SelectedNode.FullPath.StartsWith("C") Then
            validable = ClientsManager.getInstance()
        ElseIf Dossiers.SelectedNode.FullPath = "Utilisateurs" Then
            validable = UsersManager.getInstance()
        Else
            validable = ContactsManager.getInstance()
        End If

        Return validable
    End Function

    Private Sub menuValidOnlyRapport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuValidOnlyRapport.Click
        menuValidOnlyRapport.Checked = Not menuValidOnlyRapport.Checked
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function.StartsWith("FLB-" & ContactsManager.getInstance.folderType.ToString) Then loadFolders()
        If dataReceived.fromExternal = True AndAlso dataReceived.function = "Contacts" AndAlso dataReceived.params(0) = CType(Dossiers.SelectedNode.Tag, ContactFolder).noContactFolder Then
            loadContacts()
        End If
    End Sub

    Private Sub msgAddressBook_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If selectedContact <> "" Then selectContact()
    End Sub
End Class
