Friend Class SearchDB
    Inherits SingleWindow

    Friend WithEvents ItemsFound As FileListView
    Friend WithEvents SearchConditions1 As Clinica.SearchConditions
    Public WithEvents frame1 As System.Windows.Forms.GroupBox
    Friend WithEvents Symbol8 As System.Windows.Forms.Label
    Friend WithEvents Symbol7 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents searchwords As Clinica.ManagedCombo
    Friend WithEvents searchword As System.Windows.Forms.TextBox
    Public WithEvents _Labels_5 As System.Windows.Forms.Label
    Public WithEvents searchbutton As System.Windows.Forms.Button
    Public WithEvents searchfields As System.Windows.Forms.ListBox
    Public WithEvents _Labels_7 As System.Windows.Forms.Label
    Public WithEvents frame2 As System.Windows.Forms.GroupBox
    Friend WithEvents Symbol6 As System.Windows.Forms.Label
    Friend WithEvents Symbol4 As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents Symbol2 As System.Windows.Forms.Label
    Friend WithEvents Symbol5 As System.Windows.Forms.Label
    Friend WithEvents Expression As System.Windows.Forms.RadioButton
    Friend WithEvents Symbol1 As System.Windows.Forms.Label
    Friend WithEvents Symbol3 As System.Windows.Forms.Label
    Friend WithEvents TexteBrut As System.Windows.Forms.RadioButton
    Private _SelectedFileType As String = ""
    Private _SelectedCat As String
    Private winLoaded As Boolean = False
    Private _UseWinAsSelection As Boolean = False
    Private _From As Object = Nothing
    Private lastCategoriesSelected() As String
    Private fields() As String
    Private isThereData As Boolean = False

#Region " Windows Form Designer generated code "

    Private Sub loading()
        Me.MdiParent = myMainWin
        Me.toolTip1.ShowAlways = True

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

        Me.ItemsFound = New FileListView(True)
        CType(Me.ItemsFound, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'ItemsFound
        '
        Me.ItemsFound.AllowUserToAddRows = False
        Me.ItemsFound.AllowUserToDeleteRows = False
        Me.ItemsFound.AllowUserToOrderColumns = True
        Me.ItemsFound.AllowUserToResizeRows = False
        Me.ItemsFound.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ItemsFound.BackgroundColor = System.Drawing.Color.White
        Me.ItemsFound.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.ItemsFound.ColumnHeadersDefaultCellStyle.Font = New Font(Me.Font, FontStyle.Regular)
        Me.ItemsFound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ItemsFound.DefaultCellStyle.Font = New Font(Me.Font, FontStyle.Regular)
        Me.ItemsFound.dbFolder = Nothing
        Me.ItemsFound.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.ItemsFound.Location = New System.Drawing.Point(6, 25)
        Me.ItemsFound.MultiSelect = True
        Me.ItemsFound.Name = "ItemsFound"
        Me.ItemsFound.RowHeadersVisible = False
        Me.ItemsFound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ItemsFound.Size = New System.Drawing.Size(453, 224)
        Me.ItemsFound.TabIndex = 0
        Me.ItemsFound.showHiddenFiles = currentDroitAcces(2)
        Me.ItemsFound.showMenuAdd = False
        Me.ItemsFound.showMenuPaste = False
        Me.ItemsFound.showMenuAddCat = False
        Me.ItemsFound.showMenuSearchDB = currentDroitAcces(1)
        Me.ItemsFound.showMenuSearchIn = False
        CType(Me.ItemsFound, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frame2.Controls.Add(Me.ItemsFound)
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        loading()

        ItemsFound.Sort(ItemsFound.Columns(1), System.ComponentModel.ListSortDirection.Ascending)

        Dim setting As String = UsersManager.currentUser.settings.searchDBStyle
        If setting <> "" Then
            Dim thisSettings() As String = setting.Split(New Char() {"§"})

            ItemsFound.ColumnSize("Titre") = thisSettings(1)
            ItemsFound.ColumnSize("Dossier") = thisSettings(2)
            ItemsFound.ColumnSize("Taille") = thisSettings(3)
            ItemsFound.ColumnSize("Mots-clés") = thisSettings(4)
            ItemsFound.ColumnSize("Description") = thisSettings(5)
            ItemsFound.ColumnSize("Modifié le...") = thisSettings(6)
            ItemsFound.ColumnSize("Caché") = thisSettings(7)

            If ItemsFound.Columns(thisSettings(0)) IsNot Nothing Then ItemsFound.Sort(ItemsFound.Columns(thisSettings(0)), IIf(thisSettings(8) = "D", 1, 0))

            TexteBrut.Checked = thisSettings(9)
            Expression.Checked = Not CBool(thisSettings(9))
            lastCategoriesSelected = thisSettings(10).Split(New Char() {"?"})
        End If

        Me.searchbutton.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("loupe16.ico"), New Size(16, 16))
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance.getImage("SearchDB23.gif"))
        Me.categories.imageList.Images.Add(DrawingManager.getInstance.getImage("user.gif"))
        Me.categories.BringToFront()
        Me.SearchConditions1.BringToFront()
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
    Private WithEvents categories As DirectoryTreeViewCombo
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.categories = New DirectoryTreeViewCombo
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Symbol8 = New System.Windows.Forms.Label
        Me.Symbol7 = New System.Windows.Forms.Label
        Me.searchbutton = New System.Windows.Forms.Button
        Me.Symbol6 = New System.Windows.Forms.Label
        Me.Symbol4 = New System.Windows.Forms.Label
        Me.Symbol2 = New System.Windows.Forms.Label
        Me.Symbol5 = New System.Windows.Forms.Label
        Me.Expression = New System.Windows.Forms.RadioButton
        Me.Symbol1 = New System.Windows.Forms.Label
        Me.Symbol3 = New System.Windows.Forms.Label
        Me.TexteBrut = New System.Windows.Forms.RadioButton
        Me.frame1 = New System.Windows.Forms.GroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.searchwords = New ManagedCombo
        Me.searchword = New System.Windows.Forms.TextBox
        Me._Labels_5 = New System.Windows.Forms.Label
        Me.searchfields = New System.Windows.Forms.ListBox
        Me._Labels_7 = New System.Windows.Forms.Label
        Me.frame2 = New System.Windows.Forms.GroupBox
        Me.label1 = New System.Windows.Forms.Label
        Me.frame1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'categories
        '
        Me.categories.downText = "Cliquer sur la flèche pour cacher les dossiers"
        Me.categories.dropDownHeight = 220
        Me.categories.droppedDown = False
        Me.categories.expandAllNodes = True
        Me.categories.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.categories.Location = New System.Drawing.Point(8, 182)
        Me.categories.Name = "categories"
        Me.categories.pathSeparator = "\"
        Me.categories.readOnly = False
        Me.categories.showLines = True
        Me.categories.showRootLines = True
        Me.categories.Size = New System.Drawing.Size(448, 20)
        Me.categories.sorted = True
        Me.categories.TabIndex = 3
        Me.categories.tooltipTitle = "Dossier(s) sélectionné(s) :"
        Me.categories.tree = Nothing
        Me.categories.upText = "Cliquer sur la flèche pour afficher les dossiers"
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
        Me.toolTip1.SetToolTip(Me.Symbol8, "Opérateur champ/valeur signifiant 'Supérieur ou égal à'")
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
        Me.toolTip1.SetToolTip(Me.Symbol7, "Opérateur champ/valeur signifiant 'Inférieur ou égal à'")
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
        Me.toolTip1.SetToolTip(Me.searchbutton, "Chercher")
        Me.searchbutton.UseVisualStyleBackColor = False
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
        Me.toolTip1.SetToolTip(Me.Symbol6, "Opérateur champ/valeur signifiant 'Inférieur à'")
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
        Me.toolTip1.SetToolTip(Me.Symbol4, "Opérateur champ/valeur signifiant 'Ne doit pas contenir'")
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
        Me.toolTip1.SetToolTip(Me.Symbol2, "Opétarateur logique OR (Ou)")
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
        Me.toolTip1.SetToolTip(Me.Symbol5, "Opérateur champ/valeur signifiant 'Supérieur à'")
        '
        'Expression
        '
        Me.Expression.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Expression.Location = New System.Drawing.Point(328, 160)
        Me.Expression.Name = "Expression"
        Me.Expression.Size = New System.Drawing.Size(88, 16)
        Me.Expression.TabIndex = 26
        Me.Expression.Text = "Expression"
        Me.toolTip1.SetToolTip(Me.Expression, "L'option expression recherche selon les groupes champ/valeur entrés")
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
        Me.toolTip1.SetToolTip(Me.Symbol1, "Opétarateur logique AND (Et)")
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
        Me.toolTip1.SetToolTip(Me.Symbol3, "Opérateur champ/valeur signifiant 'Doit contenir'")
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
        Me.toolTip1.SetToolTip(Me.TexteBrut, "L'option texte brut recherche dans les champs Nom, Mots-clés et Description")
        '
        'frame1
        '
        Me.frame1.BackColor = System.Drawing.SystemColors.Control
        Me.frame1.Controls.Add(Me.Symbol8)
        Me.frame1.Controls.Add(Me.categories)
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
        Me.frame1.Location = New System.Drawing.Point(4, 4)
        Me.frame1.Name = "frame1"
        Me.frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame1.Size = New System.Drawing.Size(465, 463)
        Me.frame1.TabIndex = 4
        Me.frame1.TabStop = False
        Me.frame1.Text = "Éléments de recherche"
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
        Me.searchwords.dbField = "DBSearchList.DBSearchListItem"
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
        Me.frame2.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame2.Location = New System.Drawing.Point(0, 208)
        Me.frame2.Name = "frame2"
        Me.frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame2.Size = New System.Drawing.Size(465, 255)
        Me.frame2.TabIndex = 2
        Me.frame2.TabStop = False
        Me.frame2.Text = "Item(s) trouvé(s)"
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
        'SearchDB
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(473, 472)
        Me.Controls.Add(Me.frame1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "SearchDB"
        Me.ShowInTaskbar = False
        Me.Text = "Recherche dans la banque de données"
        Me.frame1.ResumeLayout(False)
        Me.frame1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Propriétés"
    Public Property multiSelect() As Boolean
        Get
            Return Me.ItemsFound.MultiSelect
        End Get
        Set(ByVal value As Boolean)
            Me.ItemsFound.MultiSelect = value
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

    Public Property useWinAsSelection() As Boolean
        Get
            Return _UseWinAsSelection
        End Get
        Set(ByVal Value As Boolean)
            _UseWinAsSelection = Value
            ItemsFound.showMenuSelect = Value
        End Set
    End Property

    Public Property selectedFileType() As String
        Get
            Return _SelectedFileType
        End Get
        Set(ByVal Value As String)
            _SelectedFileType = Value
        End Set
    End Property

    Public Property selectedCat() As String
        Get
            Return _SelectedCat
        End Get
        Set(ByVal Value As String)
            _SelectedCat = Value
            If winLoaded = True Then
                categories.checkAll(True)
                categories.expansion(Value, True)
            End If
        End Set
    End Property
#End Region

    Private Sub searchDB_enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        searchword.Focus()
    End Sub

    Private Sub searchDB_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        closingForm()
    End Sub

    Private Sub searchDB_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
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

    Private Sub searchDB_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim sFields() As String
        fields = readFile("Data\Lists\searchdb.lst")

        If Not CStr(fields(0)).Substring(0, 5) = "ERROR" Then
            For i = 0 To fields.Length - 1
                sFields = fields(i).Split(New Char() {"§"})
                searchfields.Items.Add(sFields(0))
            Next i
        End If

        'Chargement de searchwords
        Dim searchList() As String = DBLinker.getInstance.readOneDBField("DBSearchList", "DBSearchListItem", "WHERE ((NoUser)=" & ConnectionsManager.currentUser & ");", , True)
        If Not searchList Is Nothing AndAlso searchList.Length <> 0 Then searchwords.Items.AddRange(searchList)

        categories.refreshTree()
        If selectedCat = "" Then
            If lastCategoriesSelected Is Nothing OrElse lastCategoriesSelected.Length = 0 Then
                categories.checkAll(True)
            Else
                categories.setSelected(lastCategoriesSelected)
            End If
        Else
            categories.expansion(selectedCat, True)
        End If

        winLoaded = True
    End Sub

    Private Sub searchDB_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'Enregistrement de l'affichage personnalisée de la DB
        Dim curUser As User = UsersManager.currentUser
        curUser.settings.searchDBStyle = ItemsFound.SortedColumn.Name & "§" & ItemsFound.ColumnSize("Titre") & "§" & ItemsFound.ColumnSize("Dossier") & "§" & ItemsFound.ColumnSize("Taille") & "§" & ItemsFound.ColumnSize("Mots-clés") & "§" & ItemsFound.ColumnSize("Description") & "§" & ItemsFound.ColumnSize("Modifié le...") & "§" & ItemsFound.ColumnSize("Caché") & "§" & IIf(ItemsFound.SortOrder = SortOrder.Ascending, "A", "D") & "§" & TexteBrut.Checked & "§" & String.Join("?", categories.getSelected())
        curUser.settings.saveData()
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

    Private Sub itemsFound_ItemSelected(ByVal sender As Object, ByVal selectedItem() As InternalDBItem) Handles ItemsFound.itemSelected
        redirectSearchDB(from, selectedItem)
        Me.Close()
    End Sub

    Private Sub searchbutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchbutton.Click
        searchbutton.Enabled = False
        Dim spaceRemoval() As Char = {" "}
        isThereData = False

        searchword.Text = searchword.Text.TrimStart(spaceRemoval)
        searchword.Text = searchword.Text.TrimEnd(spaceRemoval)

        Dim dossiersWhere As String = ""
        Dim dbfolders() As String = categories.getSelected(categories.isCheckedNone)
        If dbfolders IsNot Nothing AndAlso dbfolders.Length <> 0 Then
            For f As Integer = 0 To dbfolders.GetUpperBound(0)
                Dim curDBFolder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(dbfolders(f))
                If Not curDBFolder Is Nothing Then dossiersWhere &= " OR DBItems.NoDBFolder=" & curDBFolder.noDBFolder
            Next f
            dossiersWhere = " AND (" & dossiersWhere.Substring(4) & ")"
        End If

        Dim typeFileWhere As String = ""
        If _SelectedFileType <> "" Then
            typeFileWhere = " AND NoFileType=" & TypesFilesManager.getInstance.getItemable(_SelectedFileType).noTypeFile
        End If

        If TexteBrut.Checked = True Then
            Dim adjustedSearchWord As String = searchword.Text.Replace("'", "''")
            ItemsFound.showFiles("DBItems", "WHERE (DBItem LIKE '%" & adjustedSearchWord & "%' OR [dbo].[fnGetDBItemMotsCles](NoDBItem) LIKE '%" & adjustedSearchWord & "%' OR Description LIKE '%" & adjustedSearchWord & "%')" & dossiersWhere & typeFileWhere)
            If Not ItemsFound.DataSource Is Nothing Then isThereData = True
        Else
            searchword.Text = searchword.Text.Replace("  ", " ")
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
                PreviousTable = "DBItems"
                Tables = ""

                For i = 0 To aExp.selTables.Length - 1
                    If aExp.selTables(i) <> "DBItems" Then
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

                aExp.conditions &= dossiersWhere & typeFileWhere
                SearchConditions1.text = aExp.conditions
                SearchConditions1.Visible = True

                ItemsFound.showFiles(Tables, "WHERE " & aExp.conditions)
                If Not ItemsFound.DataSource Is Nothing Then
                    DBHelper.addItemToADBList("DBSearchList", "DBSearchListItem", searchword.Text, "NoDBSearchList", 15, , , , "NoUser", ConnectionsManager.currentUser)
                    searchwords.Items.Clear()
                    Dim searchList() As String = DBLinker.getInstance.readOneDBField("DBSearchList", "DBSearchListItem", "WHERE ((NoUser)=" & ConnectionsManager.currentUser & ");", , True)
                    If Not searchList Is Nothing AndAlso searchList.Length <> 0 Then searchwords.Items.AddRange(searchList)
                    isThereData = True
                End If
            End If
        End If

        searchbutton.Enabled = True
    End Sub

    Private Sub searchword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles searchword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            searchbutton_Click(searchbutton, EventArgs.Empty)
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
