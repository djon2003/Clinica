Option Strict Off
Option Explicit On
Friend Class clientSearch
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New(Optional ByVal setAsMdiForm As Boolean = True)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.
        If setAsMdiForm = True Then Me.MdiParent = myMainWin

        'Set default settings
        ReDim thisSettings(6)
        thisSettings(0) = 135
        thisSettings(1) = 157
        thisSettings(2) = 103
        thisSettings(3) = 86
        thisSettings(4) = 121
        thisSettings(5) = True
        thisSettings(6) = "Nom complet"


        Me.SearchConditions1 = New Clinica.SearchConditions()
        '
        'SearchConditions1
        '
        Me.SearchConditions1.bordered = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SearchConditions1.caption = "Conditions de recherche en format SQL"
        Me.SearchConditions1.endingPosition = 0
        Me.SearchConditions1.Location = New System.Drawing.Point(168, -320)
        Me.SearchConditions1.movingSpeed = 10
        Me.SearchConditions1.Name = "SearchConditions1"
        Me.SearchConditions1.Size = New System.Drawing.Size(288, 328)
        Me.SearchConditions1.startingPosition = -320
        Me.SearchConditions1.TabIndex = 35
        Me.SearchConditions1.Visible = False

        Me.frame1.Controls.Add(Me.SearchConditions1)
        Me.SearchConditions1.BringToFront()

        'Chargement des images
        Me.ajouter.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.delete.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.selectionner.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.searchbutton.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("loupe16.ico"), New Size(16, 16))
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance.getImage("searchClient16.gif"))
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
    Public WithEvents selectionner As System.Windows.Forms.Button
    Public WithEvents searchbutton As System.Windows.Forms.Button
    Public WithEvents searchfields As System.Windows.Forms.ListBox
    Public WithEvents _Labels_7 As System.Windows.Forms.Label
    Public WithEvents _Labels_5 As System.Windows.Forms.Label
    Public WithEvents frame1 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Symbol1 As System.Windows.Forms.Label
    Friend WithEvents Symbol2 As System.Windows.Forms.Label
    Friend WithEvents Symbol3 As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents frame2 As System.Windows.Forms.GroupBox
    Friend WithEvents DataSetForGrid As System.Data.DataSet
    Friend WithEvents Symbol6 As System.Windows.Forms.Label
    Friend WithEvents Symbol5 As System.Windows.Forms.Label
    Friend WithEvents Symbol4 As System.Windows.Forms.Label
    Friend WithEvents searchwords As Clinica.ManagedCombo
    Friend WithEvents searchword As System.Windows.Forms.TextBox
    Friend WithEvents Expression As System.Windows.Forms.RadioButton
    Friend WithEvents TexteBrut As System.Windows.Forms.RadioButton
    'Friend WithEvents Conditions As Clinica.SearchConditions
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SearchConditions1 As Clinica.SearchConditions
    Public WithEvents noClientFound As System.Windows.Forms.Label
    Public WithEvents ajouter As System.Windows.Forms.Button
    Public WithEvents delete As System.Windows.Forms.Button
    Friend WithEvents Table As System.Data.DataTable
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    Friend WithEvents DataColumn2 As System.Data.DataColumn
    Friend WithEvents DataColumn3 As System.Data.DataColumn
    Friend WithEvents DataColumn4 As System.Data.DataColumn
    Friend WithEvents DataColumn5 As System.Data.DataColumn
    Friend WithEvents Symbol7 As System.Windows.Forms.Label
    Friend WithEvents ClientsTrouves As DataGridPlus
    Public WithEvents viewFolders As System.Windows.Forms.Button
    Friend WithEvents Symbol8 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.frame1 = New System.Windows.Forms.GroupBox
        Me.Symbol8 = New System.Windows.Forms.Label
        Me.Symbol7 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.searchwords = New ManagedCombo
        Me.searchword = New System.Windows.Forms.TextBox
        Me._Labels_5 = New System.Windows.Forms.Label
        Me.searchbutton = New System.Windows.Forms.Button
        Me.searchfields = New System.Windows.Forms.ListBox
        Me._Labels_7 = New System.Windows.Forms.Label
        Me.frame2 = New System.Windows.Forms.GroupBox
        Me.ajouter = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.noClientFound = New System.Windows.Forms.Label
        Me.viewFolders = New System.Windows.Forms.Button
        Me.selectionner = New System.Windows.Forms.Button
        Me.ClientsTrouves = New CI.Base.Windows.Forms.DataGridPlus
        Me.Symbol6 = New System.Windows.Forms.Label
        Me.Symbol4 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.Symbol2 = New System.Windows.Forms.Label
        Me.Symbol5 = New System.Windows.Forms.Label
        Me.Expression = New System.Windows.Forms.RadioButton
        Me.Symbol1 = New System.Windows.Forms.Label
        Me.Symbol3 = New System.Windows.Forms.Label
        Me.TexteBrut = New System.Windows.Forms.RadioButton
        Me.DataSetForGrid = New System.Data.DataSet
        Me.Table = New System.Data.DataTable
        Me.DataColumn1 = New System.Data.DataColumn
        Me.DataColumn2 = New System.Data.DataColumn
        Me.DataColumn3 = New System.Data.DataColumn
        Me.DataColumn4 = New System.Data.DataColumn
        Me.DataColumn5 = New System.Data.DataColumn
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.frame1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.frame2.SuspendLayout()
        CType(Me.ClientsTrouves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSetForGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Table, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'frame1
        '
        Me.frame1.BackColor = System.Drawing.SystemColors.Control
        Me.frame1.Controls.Add(Me.Symbol8)
        Me.frame1.Controls.Add(Me.Symbol7)
        Me.frame1.Controls.Add(Me.Panel1)
        Me.frame1.Controls.Add(Me.searchfields)
        Me.frame1.Controls.Add(Me._Labels_7)
        Me.frame1.Controls.Add(Me.frame2)
        Me.frame1.Controls.Add(Me.Symbol6)
        Me.frame1.Controls.Add(Me.Symbol4)
        Me.frame1.Controls.Add(Me.label1)
        Me.frame1.Controls.Add(Me.Symbol2)
        Me.frame1.Controls.Add(Me.Symbol5)
        Me.frame1.Controls.Add(Me.Expression)
        Me.frame1.Controls.Add(Me.Symbol1)
        Me.frame1.Controls.Add(Me.Symbol3)
        Me.frame1.Controls.Add(Me.TexteBrut)
        Me.frame1.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame1.Location = New System.Drawing.Point(8, 0)
        Me.frame1.Name = "frame1"
        Me.frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame1.Size = New System.Drawing.Size(465, 360)
        Me.frame1.TabIndex = 0
        Me.frame1.TabStop = False
        Me.frame1.Text = "Éléments de recherche"
        '
        'Symbol8
        '
        Me.Symbol8.AutoSize = True
        Me.Symbol8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol8.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol8.Location = New System.Drawing.Point(424, 128)
        Me.Symbol8.Name = "Symbol8"
        Me.Symbol8.Size = New System.Drawing.Size(34, 23)
        Me.Symbol8.TabIndex = 33
        Me.Symbol8.Text = ">="
        Me.ToolTip1.SetToolTip(Me.Symbol8, "Opérateur champ/valeur signifiant 'Supérieur ou égal à'")
        '
        'Symbol7
        '
        Me.Symbol7.AutoSize = True
        Me.Symbol7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol7.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol7.Location = New System.Drawing.Point(384, 128)
        Me.Symbol7.Name = "Symbol7"
        Me.Symbol7.Size = New System.Drawing.Size(34, 23)
        Me.Symbol7.TabIndex = 32
        Me.Symbol7.Text = "<="
        Me.ToolTip1.SetToolTip(Me.Symbol7, "Opérateur champ/valeur signifiant 'Inférieur ou égal à'")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.searchwords)
        Me.Panel1.Controls.Add(Me.searchword)
        Me.Panel1.Controls.Add(Me._Labels_5)
        Me.Panel1.Controls.Add(Me.searchbutton)
        Me.Panel1.Location = New System.Drawing.Point(168, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(288, 96)
        Me.Panel1.TabIndex = 1
        '
        'searchwords
        '
        Me.searchwords.acceptAlpha = True
        Me.searchwords.acceptedChars = Nothing
        Me.searchwords.acceptNumeric = True
        Me.searchwords.allCapital = False
        Me.searchwords.allLower = False
        Me.searchwords.autoComplete = False
        Me.searchwords.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.searchwords.autoSizeDropDown = True
        Me.searchwords.BackColor = System.Drawing.Color.White
        Me.searchwords.blockOnMaximum = False
        Me.searchwords.blockOnMinimum = False
        Me.searchwords.cb_AcceptLeftZeros = False
        Me.searchwords.cb_AcceptNegative = False
        Me.searchwords.currencyBox = False
        Me.searchwords.dbField = "SearchList.SearchListItem"
        Me.searchwords.doComboDelete = True
        Me.searchwords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.searchwords.firstLetterCapital = False
        Me.searchwords.firstLettersCapital = False
        Me.searchwords.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchwords.itemsToolTipDuration = 10000
        Me.searchwords.Location = New System.Drawing.Point(0, 16)
        Me.searchwords.manageText = False
        Me.searchwords.matchExp = Nothing
        Me.searchwords.MaxDropDownItems = 15
        Me.searchwords.maximum = 0
        Me.searchwords.minimum = 0
        Me.searchwords.Name = "searchwords"
        Me.searchwords.nbDecimals = CType(-1, Short)
        Me.searchwords.onlyAlphabet = False
        Me.searchwords.pathOfList = Nothing
        Me.searchwords.ReadOnly = False
        Me.searchwords.refuseAccents = False
        Me.searchwords.refusedChars = ""
        Me.searchwords.showItemsToolTip = False
        Me.searchwords.Size = New System.Drawing.Size(256, 22)
        Me.searchwords.TabIndex = 4
        Me.searchwords.trimText = False
        '
        'searchword
        '
        Me.searchword.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchword.Location = New System.Drawing.Point(0, 48)
        Me.searchword.Multiline = True
        Me.searchword.Name = "searchword"
        Me.searchword.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.searchword.Size = New System.Drawing.Size(288, 48)
        Me.searchword.TabIndex = 1
        '
        '_Labels_5
        '
        Me._Labels_5.AutoSize = True
        Me._Labels_5.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_5.Location = New System.Drawing.Point(0, 0)
        Me._Labels_5.Name = "_Labels_5"
        Me._Labels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_5.Size = New System.Drawing.Size(76, 14)
        Me._Labels_5.TabIndex = 16
        Me._Labels_5.Text = "Rechercher ..."
        '
        'searchbutton
        '
        Me.searchbutton.BackColor = System.Drawing.SystemColors.Control
        Me.searchbutton.Cursor = System.Windows.Forms.Cursors.Default
        Me.searchbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.searchbutton.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        Me.searchbutton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.searchbutton.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.searchbutton.Location = New System.Drawing.Point(264, 8)
        Me.searchbutton.Name = "searchbutton"
        Me.searchbutton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.searchbutton.Size = New System.Drawing.Size(24, 25)
        Me.searchbutton.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.searchbutton, "Chercher")
        Me.searchbutton.UseVisualStyleBackColor = False
        '
        'searchfields
        '
        Me.searchfields.BackColor = System.Drawing.SystemColors.Window
        Me.searchfields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.searchfields.Cursor = System.Windows.Forms.Cursors.Default
        Me.searchfields.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchfields.ForeColor = System.Drawing.SystemColors.WindowText
        Me.searchfields.ItemHeight = 14
        Me.searchfields.Location = New System.Drawing.Point(8, 32)
        Me.searchfields.Name = "searchfields"
        Me.searchfields.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.searchfields.Size = New System.Drawing.Size(153, 142)
        Me.searchfields.TabIndex = 5
        '
        '_Labels_7
        '
        Me._Labels_7.AutoSize = True
        Me._Labels_7.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_7.Location = New System.Drawing.Point(8, 16)
        Me._Labels_7.Name = "_Labels_7"
        Me._Labels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_7.Size = New System.Drawing.Size(135, 14)
        Me._Labels_7.TabIndex = 17
        Me._Labels_7.Text = "Champs pour la recherche"
        '
        'frame2
        '
        Me.frame2.BackColor = System.Drawing.Color.Transparent
        Me.frame2.Controls.Add(Me.ajouter)
        Me.frame2.Controls.Add(Me.delete)
        Me.frame2.Controls.Add(Me.noClientFound)
        Me.frame2.Controls.Add(Me.viewFolders)
        Me.frame2.Controls.Add(Me.selectionner)
        Me.frame2.Controls.Add(Me.ClientsTrouves)
        Me.frame2.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame2.Location = New System.Drawing.Point(0, 176)
        Me.frame2.Name = "frame2"
        Me.frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame2.Size = New System.Drawing.Size(465, 248)
        Me.frame2.TabIndex = 2
        Me.frame2.TabStop = False
        Me.frame2.Text = "Compte(s) client(s) trouvé(s)"
        '
        'ajouter
        '
        Me.ajouter.BackColor = System.Drawing.SystemColors.Control
        Me.ajouter.Cursor = System.Windows.Forms.Cursors.Default
        Me.ajouter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ajouter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ajouter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ajouter.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ajouter.Location = New System.Drawing.Point(432, 44)
        Me.ajouter.Name = "ajouter"
        Me.ajouter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ajouter.Size = New System.Drawing.Size(24, 24)
        Me.ajouter.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.ajouter, "Ajout d'un compte client")
        Me.ajouter.UseVisualStyleBackColor = False
        '
        'delete
        '
        Me.delete.BackColor = System.Drawing.SystemColors.Control
        Me.delete.Cursor = System.Windows.Forms.Cursors.Default
        Me.delete.Enabled = False
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.delete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delete.Location = New System.Drawing.Point(432, 74)
        Me.delete.Name = "delete"
        Me.delete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer le compte client sélectionné")
        Me.delete.UseVisualStyleBackColor = False
        '
        'noClientFound
        '
        Me.noClientFound.AutoSize = True
        Me.noClientFound.BackColor = System.Drawing.SystemColors.Control
        Me.noClientFound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.noClientFound.Cursor = System.Windows.Forms.Cursors.Default
        Me.noClientFound.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.noClientFound.ForeColor = System.Drawing.SystemColors.ControlText
        Me.noClientFound.Location = New System.Drawing.Point(96, 88)
        Me.noClientFound.Name = "noClientFound"
        Me.noClientFound.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.noClientFound.Size = New System.Drawing.Size(240, 24)
        Me.noClientFound.TabIndex = 28
        Me.noClientFound.Text = "Aucun compte client trouvé"
        '
        'viewFolders
        '
        Me.viewFolders.BackColor = System.Drawing.SystemColors.Control
        Me.viewFolders.Cursor = System.Windows.Forms.Cursors.Default
        Me.viewFolders.Enabled = False
        Me.viewFolders.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.viewFolders.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.viewFolders.ForeColor = System.Drawing.Color.Red
        Me.viewFolders.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.viewFolders.Location = New System.Drawing.Point(432, 104)
        Me.viewFolders.Name = "viewFolders"
        Me.viewFolders.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.viewFolders.Size = New System.Drawing.Size(24, 25)
        Me.viewFolders.TabIndex = 8
        Me.viewFolders.Text = "D"
        Me.ToolTip1.SetToolTip(Me.viewFolders, "Voir les dossiers du compte client sélectionné")
        Me.viewFolders.UseVisualStyleBackColor = False
        '
        'selectionner
        '
        Me.selectionner.BackColor = System.Drawing.SystemColors.Control
        Me.selectionner.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectionner.Enabled = False
        Me.selectionner.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectionner.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        Me.selectionner.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectionner.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectionner.Location = New System.Drawing.Point(432, 135)
        Me.selectionner.Name = "selectionner"
        Me.selectionner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectionner.Size = New System.Drawing.Size(24, 25)
        Me.selectionner.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.selectionner, "Sélectionner ce compte client")
        Me.selectionner.UseVisualStyleBackColor = False
        '
        'ClientsTrouves
        '
        Me.ClientsTrouves.AllowUserToAddRows = False
        Me.ClientsTrouves.AllowUserToDeleteRows = False
        Me.ClientsTrouves.AllowUserToResizeRows = False
        Me.ClientsTrouves.autoSelectOnDataSourceChanged = True
        Me.ClientsTrouves.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ClientsTrouves.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.ClientsTrouves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ClientsTrouves.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.ClientsTrouves.Location = New System.Drawing.Point(8, 25)
        Me.ClientsTrouves.MultiSelect = False
        Me.ClientsTrouves.Name = "ClientsTrouves"
        Me.ClientsTrouves.ReadOnly = True
        Me.ClientsTrouves.RowHeadersVisible = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientsTrouves.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.ClientsTrouves.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ClientsTrouves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ClientsTrouves.ShowCellErrors = False
        Me.ClientsTrouves.ShowEditingIcon = False
        Me.ClientsTrouves.Size = New System.Drawing.Size(418, 151)
        Me.ClientsTrouves.StandardTab = True
        Me.ClientsTrouves.TabIndex = 3
        Me.ClientsTrouves.VirtualMode = True
        '
        'Symbol6
        '
        Me.Symbol6.AutoSize = True
        Me.Symbol6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol6.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol6.Location = New System.Drawing.Point(360, 128)
        Me.Symbol6.Name = "Symbol6"
        Me.Symbol6.Size = New System.Drawing.Size(22, 23)
        Me.Symbol6.TabIndex = 31
        Me.Symbol6.Text = "<"
        Me.ToolTip1.SetToolTip(Me.Symbol6, "Opérateur champ/valeur signifiant 'Inférieur à'")
        '
        'Symbol4
        '
        Me.Symbol4.AutoSize = True
        Me.Symbol4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol4.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol4.Location = New System.Drawing.Point(296, 128)
        Me.Symbol4.Name = "Symbol4"
        Me.Symbol4.Size = New System.Drawing.Size(34, 23)
        Me.Symbol4.TabIndex = 29
        Me.Symbol4.Text = "<>"
        Me.ToolTip1.SetToolTip(Me.Symbol4, "Opérateur champ/valeur signifiant 'Ne doit pas contenir'")
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(168, 112)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(68, 14)
        Me.label1.TabIndex = 27
        Me.label1.Text = "Opérateurs :"
        '
        'Symbol2
        '
        Me.Symbol2.AutoSize = True
        Me.Symbol2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol2.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol2.Location = New System.Drawing.Point(224, 128)
        Me.Symbol2.Name = "Symbol2"
        Me.Symbol2.Size = New System.Drawing.Size(39, 23)
        Me.Symbol2.TabIndex = 23
        Me.Symbol2.Text = "OR"
        Me.ToolTip1.SetToolTip(Me.Symbol2, "Opétarateur logique OR (Ou)")
        '
        'Symbol5
        '
        Me.Symbol5.AutoSize = True
        Me.Symbol5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol5.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol5.Location = New System.Drawing.Point(336, 128)
        Me.Symbol5.Name = "Symbol5"
        Me.Symbol5.Size = New System.Drawing.Size(22, 23)
        Me.Symbol5.TabIndex = 30
        Me.Symbol5.Text = ">"
        Me.ToolTip1.SetToolTip(Me.Symbol5, "Opérateur champ/valeur signifiant 'Supérieur à'")
        '
        'Expression
        '
        Me.Expression.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Expression.Location = New System.Drawing.Point(328, 160)
        Me.Expression.Name = "Expression"
        Me.Expression.Size = New System.Drawing.Size(88, 16)
        Me.Expression.TabIndex = 26
        Me.Expression.Text = "Expression"
        Me.ToolTip1.SetToolTip(Me.Expression, "L'option expression recherche selon les groupes champ/valeur entrés")
        '
        'Symbol1
        '
        Me.Symbol1.AutoSize = True
        Me.Symbol1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol1.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol1.Location = New System.Drawing.Point(168, 128)
        Me.Symbol1.Name = "Symbol1"
        Me.Symbol1.Size = New System.Drawing.Size(54, 23)
        Me.Symbol1.TabIndex = 22
        Me.Symbol1.Text = "AND"
        Me.ToolTip1.SetToolTip(Me.Symbol1, "Opétarateur logique AND (Et)")
        '
        'Symbol3
        '
        Me.Symbol3.AutoSize = True
        Me.Symbol3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol3.Location = New System.Drawing.Point(264, 128)
        Me.Symbol3.Name = "Symbol3"
        Me.Symbol3.Size = New System.Drawing.Size(22, 23)
        Me.Symbol3.TabIndex = 24
        Me.Symbol3.Text = "="
        Me.ToolTip1.SetToolTip(Me.Symbol3, "Opérateur champ/valeur signifiant 'Doit contenir'")
        '
        'TexteBrut
        '
        Me.TexteBrut.Checked = True
        Me.TexteBrut.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TexteBrut.Location = New System.Drawing.Point(224, 160)
        Me.TexteBrut.Name = "TexteBrut"
        Me.TexteBrut.Size = New System.Drawing.Size(80, 16)
        Me.TexteBrut.TabIndex = 28
        Me.TexteBrut.TabStop = True
        Me.TexteBrut.Text = "Texte brut"
        Me.ToolTip1.SetToolTip(Me.TexteBrut, "L'option texte brut recherche dans les champs Nom et/ou prénom, Adresse ou Téléph" & _
                "ones")
        '
        'DataSetForGrid
        '
        Me.DataSetForGrid.DataSetName = "dsClientsTrouves"
        Me.DataSetForGrid.Locale = New System.Globalization.CultureInfo("fr-CA")
        Me.DataSetForGrid.Tables.AddRange(New System.Data.DataTable() {Me.Table})
        '
        'Table
        '
        Me.Table.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4, Me.DataColumn5})
        Me.Table.TableName = "Table"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "Nom complet"
        '
        'DataColumn2
        '
        Me.DataColumn2.ColumnName = "Adresse"
        '
        'DataColumn3
        '
        Me.DataColumn3.ColumnName = "Téléphones"
        '
        'DataColumn4
        '
        Me.DataColumn4.Caption = "NAM"
        Me.DataColumn4.ColumnName = "NAM"
        '
        'DataColumn5
        '
        Me.DataColumn5.Caption = "# du compte"
        Me.DataColumn5.ColumnName = "# du compte"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'clientSearch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(482, 368)
        Me.Controls.Add(Me.frame1)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.Name = "clientSearch"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Recherche d'un compte client"
        Me.frame1.ResumeLayout(False)
        Me.frame1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.frame2.ResumeLayout(False)
        Me.frame2.PerformLayout()
        CType(Me.ClientsTrouves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSetForGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Table, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
    Private _From As Object
    Private fields() As String
    Private thisSettings() As String
    Private isThereData As Boolean = False

#Region "Propriétés"
    Public Property from() As Object
        Get
            from = _From
        End Get
        Set(ByVal Value As Object)
            _From = Value
        End Set
    End Property
#End Region

    Private Sub recherche_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        searchword.Focus()
    End Sub

    Private Sub recherche_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Enter
        searchword.Focus()
    End Sub

    Private isClosed As Boolean = False

    Private Sub closingForm()
        If isClosed Then Exit Sub

        isClosed = True
        Try
            If Me.from IsNot Nothing AndAlso Me.from.GetType.Name.ToLower = "addvisite" Then CType(from, addvisite).loadSearchWin = False
        Catch
        End Try
    End Sub

    Private Sub recherche_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        closingForm()
    End Sub

    Private Sub recherche_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        searchword.Focus()
    End Sub

    Private Sub recherche_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim sFields() As String
        fields = readFile("Data\Lists\search.lst")

        If Not CStr(fields(0)).Substring(0, 5) = "ERROR" Then
            For i = 0 To fields.Length - 1
                sFields = fields(i).Split(New Char() {"§"})
                searchfields.Items.Add(sFields(0))
            Next i
        End If

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        'Chargement de searchwords
        Dim searchList() As String = DBLinker.getInstance.readOneDBField("SearchList", "SearchListItem", "WHERE ((NoUser)=" & ConnectionsManager.currentUser & ");", , True)
        If Not searchList Is Nothing AndAlso searchList.Length <> 0 Then searchwords.Items.AddRange(searchList)

        'Settings du user pour la fenêtre
        Dim setting As String = UsersManager.currentUser.settings.searchClientStyle
        

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        searchword.Focus()
    End Sub

    Private Sub searchbutton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles searchbutton.Click
        If DataSetForGrid IsNot Nothing Then DataSetForGrid.Clear()
        selectionner.Enabled = False
        searchbutton.Enabled = False
        Dim spaceRemoval() As Char = {" "}
        isThereData = False

        searchword.Text = searchword.Text.TrimStart(spaceRemoval)
        searchword.Text = searchword.Text.TrimEnd(spaceRemoval)

        If TexteBrut.Checked = True Then
            Dim adjustedSearchWord As String = searchword.Text.Replace("'", "''")
            DataSetForGrid = DBLinker.getInstance.readDBForGrid("InfoClients", "Nom + ',' + Prenom AS [Nom complet], Adresse, Telephones AS [Téléphones], NAM, NoClient AS [# du compte]", "WHERE ((Nom + ',' + Prenom) LIKE '%" & adjustedSearchWord & "%' OR Adresse LIKE '%" & adjustedSearchWord & "%' OR Telephones LIKE '%" & adjustedSearchWord & "%');", False)
            If DataSetForGrid IsNot Nothing AndAlso Not DataSetForGrid.Tables("Table") Is Nothing Then
                ClientsTrouves.DataSource = DataSetForGrid.Tables("Table").DefaultView
                isThereData = ClientsTrouves.Rows.Count <> 0
            End If
        Else
            'searchword.Text = searchword.Text.Replace("  ", " ")
            Dim aExp As AnalysedExpression = analyseExpression(fields, searchword.Text)
            If aExp.conditions = "" Then
                searchword.SelectionStart = aExp.ErrorPos
                searchword.SelectionLength = aExp.errorLength
                searchword.Focus()
                searchword.ScrollToCaret()
            Else
                'Get correspondant data into database
                Dim i, sa As Integer
                Dim Tables, PreviousTable, sFields() As String
                PreviousTable = "InfoClients"
                Tables = ""

                For i = 0 To aExp.selTables.Length - 1
                    If aExp.selTables(i) <> "InfoClients" Then
                        sa = searchArray(fields, "§" & aExp.selTables(i) & "§", SearchType.InString)
                        sFields = fields(sa).Split(splitStr("§"))

                        If Tables.IndexOf("RIGHT JOIN") <> -1 Then
                            Tables = Replace(Tables, " " & PreviousTable & " ", " (" & aExp.selTables(i) & " RIGHT JOIN " & PreviousTable & " ON " & PreviousTable & "." & sFields(4) & "=" & aExp.selTables(i) & "." & sFields(4) & ") ")
                        Else
                            Tables &= aExp.selTables(i) & " RIGHT JOIN " & PreviousTable & " ON " & PreviousTable & "." & sFields(4) & "=" & aExp.selTables(i) & "." & sFields(4)
                        End If
                    End If
                Next i

                If Tables = "" Then Tables = PreviousTable

                SearchConditions1.text = aExp.conditions
                SearchConditions1.Visible = True
                DataSetForGrid = DBLinker.getInstance.readDBForGrid(Tables, "Nom + ',' + Prenom AS [Nom complet], Adresse, Telephones AS [Téléphones], NAM, NoClient AS [# du compte]", "WHERE " & aExp.conditions, False)
                If DataSetForGrid IsNot Nothing AndAlso Not DataSetForGrid.Tables("Table") Is Nothing Then
                    ClientsTrouves.DataSource = DataSetForGrid.Tables("Table").DefaultView

                    DBHelper.addItemToADBList("SearchList", "SearchListItem", searchword.Text, "NoSearchList", 15, , , , "NoUser", ConnectionsManager.currentUser)
                    searchwords.Items.Clear()
                    Dim searchList() As String = DBLinker.getInstance.readOneDBField("SearchList", "SearchListItem", "WHERE ((NoUser)=" & ConnectionsManager.currentUser & ");", , True)
                    If Not searchList Is Nothing AndAlso searchList.Length <> 0 Then searchwords.Items.AddRange(searchList)
                    isThereData = ClientsTrouves.Rows.Count <> 0
                End If
            End If
        End If

        If isThereData Then
            ClientsTrouves.Rows(0).Selected = True
            ClientsTrouves.Select()
            noClientFound.Visible = False
        Else
            noClientFound.Visible = True
            delete.Enabled = False
            viewFolders.Enabled = False
            selectionner.Enabled = False
        End If

        If Not thisSettings Is Nothing AndAlso thisSettings.Length <> 0 AndAlso DataSetForGrid IsNot Nothing AndAlso Not DataSetForGrid.Tables("Table") Is Nothing Then
            Dim n As Byte = 0
            Dim myGCS As System.Windows.Forms.DataGridViewColumn
            For Each myGCS In Me.ClientsTrouves.Columns
                myGCS.Width = thisSettings(n)
                n += 1
            Next

            DataSetForGrid.Tables("Table").DefaultView.Sort = thisSettings(6)
        End If

        searchbutton.Enabled = True
    End Sub

    Private Sub searchfields_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles searchfields.KeyDown
        If e.KeyCode = Keys.Enter Then searchfields_DoubleClick(sender, EventArgs.Empty)
    End Sub

    Private Sub searchword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles searchword.KeyDown
        If e.KeyCode = Keys.Enter Then e.Handled = True : searchbutton_Click(searchbutton, EventArgs.Empty)
    End Sub

    Private Sub searchword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchword.TextChanged
        If searchword.Text <> "" Then searchword.Text = searchword.Text.Replace(vbCrLf, "")
    End Sub

    Private Sub selectionner_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectionner.Click
        ReDim Preserve foundClient(foundClient.Length)
        With foundClient(foundClient.Length - 1)
            .nam = ClientsTrouves.Item(3, ClientsTrouves.currentRow.Index).Value
            .noClient = ClientsTrouves.Item(4, ClientsTrouves.currentRow.Index).Value
            .fullName = ClientsTrouves.Item(0, ClientsTrouves.currentRow.Index).Value
        End With
        Dim canceled As Boolean = False
        redirectSearch(from, canceled)
        If Not canceled Then
            If Me.MdiParent Is Nothing OrElse (PreferencesManager.getUserPreferences()("AutoCloseSearchClientOnOpenAccount") = True) Then
                closingForm()
                Me.Close()
            End If
        End If
    End Sub

    Private Sub searchfields_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchfields.DoubleClick
        searchword.SelectedText = "(" & searchfields.GetItemText(searchfields.SelectedItem) & ")"
        Expression.Checked = True
        searchword.Focus()
        searchword.Select(searchword.Text.Length, 0)
    End Sub

    Private Sub symbols_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Symbol1.DoubleClick, Symbol2.DoubleClick, Symbol3.DoubleClick, Symbol4.DoubleClick, Symbol5.DoubleClick, Symbol6.DoubleClick, Symbol7.DoubleClick, Symbol8.DoubleClick
        With CType(sender, Label)
            If .Text = "AND" Or .Text = "OR" Then
                searchword.SelectedText = " " & .Text & " "
            Else
                searchword.SelectedText = .Text
            End If
        End With
        Expression.Checked = True
        searchword.Focus()
        searchword.Select(searchword.Text.Length, 0)
    End Sub

    Private Sub searchwords_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchwords.SelectedIndexChanged
        searchword.Text = searchwords.GetItemText(searchwords.SelectedItem)
        Expression.Checked = True
        searchword.Select()
    End Sub

    Private Sub searchConditions1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SearchConditions1.MouseMove
        If SearchConditions1.text = "" Then SearchConditions1.cancelingMove = True
    End Sub

    Private Sub ajouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ajouter.Click
        Comptes.addClient(Me)
    End Sub

    Private Sub delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete.Click
        'Droit & Accès
        If currentDroitAcces(25) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de supprimer un client." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce compte client ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim delMsg As String = delAccount(ClientsTrouves.Item(4, ClientsTrouves.currentRow.Index).Value)
        If delMsg = "" Then
            Dim curDataRow As DataRow
            For i As Integer = 0 To DataSetForGrid.Tables(0).Rows.Count - 1
                If DataSetForGrid.Tables(0).Rows(i)("# du compte") = ClientsTrouves.currentRow.Cells("# du compte").Value Then curDataRow = DataSetForGrid.Tables(0).Rows(i)
            Next
            DataSetForGrid.Tables(0).Rows.Remove(curDataRow)

            If DataSetForGrid.Tables(0).Rows.Count = 0 Then
                delete.Enabled = False
                selectionner.Enabled = False
                viewFolders.Enabled = False
            End If
        Else
            MessageBox.Show(delMsg, "Suppression impossible")
        End If
    End Sub

    Private Sub recherche_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

    End Sub

    Private Sub recherche_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim myGCS As DataGridViewColumn
        Dim mySettings As String = ""
        If Me.noClientFound.Visible Then
            If thisSettings Is Nothing OrElse thisSettings.Length = 0 Then
                mySettings = "135§157§103§86§121§" & TexteBrut.Checked & "§"
            Else
                mySettings = thisSettings(0) & "§" & thisSettings(1) & "§" & thisSettings(2) & "§" & thisSettings(3) & "§" & thisSettings(4) & "§" & TexteBrut.Checked & "§" & thisSettings(6)
            End If
        Else
            For Each myGCS In Me.ClientsTrouves.Columns
                mySettings &= myGCS.Width & "§"
            Next

            mySettings &= TexteBrut.Checked & "§" & DataSetForGrid.Tables("Table").DefaultView.Sort
        End If

        Dim curUser As User = UsersManager.currentUser
        curUser.settings.searchClientStyle = mySettings
        curUser.settings.saveData()
    End Sub

    Private Sub clientsTrouves_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientsTrouves.DoubleClick
        If selectionner.Enabled = True Then selectionner_Click(sender, e)
    End Sub

    Private Sub clientsTrouves_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ClientsTrouves.KeyDown
        If e.KeyCode = Keys.Enter Then clientsTrouves_DoubleClick(sender, New EventArgs())
    End Sub

    Private Sub clientsTrouves_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ClientsTrouves.RowEnter
        delete.Enabled = True
        selectionner.Enabled = True
        viewFolders.Enabled = True
    End Sub

    Private Sub viewFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles viewFolders.Click
        Dim noClient As Integer = ClientsTrouves.Item(4, ClientsTrouves.currentRow.Index).Value
        'REM_CODES
        Dim results(,) As String = DBLinker.getInstance.readDB("SiteLesion RIGHT OUTER JOIN                       InfoFolders  ON SiteLesion.NoSiteLesion = InfoFolders.NoSiteLesion INNER JOIN Utilisateurs ON Utilisateurs.Nouser=InfoFolders.NoTRPTraitant", "SiteLesion.SiteLesion, InfoFolders.StatutOuvert, InfoFolders.NoCodeUnique, InfoFolders.NoFolder,Utilisateurs.Nom + ',' + Utilisateurs.Prenom + ' (' + CAST(Utilisateurs.NoUser AS VARCHAR(MAX)) + ')' AS TRP", "WHERE ((InfoFolders.NoClient)=" & noClient & ");")

        If results Is Nothing OrElse results.Length = 0 Then
            MessageBox.Show("Il n'existe aucun dossier pour ce compte client", "Aucun dossier")
            Exit Sub
        End If

        Dim myMultiChoice As New multichoice
        Dim statut As String = ""
        Dim dossiers As String = ""
        For i As Integer = 0 To results.GetUpperBound(1)
            If results(1, i) = True Then
                statut = "Actif"
            Else
                statut = "Inactif"
            End If
            dossiers &= "§" & results(3, i) & " - " & results(0, i) & " - " & results(4, i) & " (" & Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNameByNoUnique(results(2, i)) & ") (" & statut & ")"
        Next i
        dossiers = dossiers.Substring(1)
        Dim choice As String = myMultiChoice.GetChoice("Visualisation des dossiers", dossiers, , "§", False)
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
