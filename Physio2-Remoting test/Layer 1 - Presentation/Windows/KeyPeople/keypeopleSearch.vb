Option Strict Off
Option Explicit On
Friend Class keypeopleSearch
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
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance.getImage("searchKP16.gif"))
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
    Public WithEvents delete As System.Windows.Forms.Button
    Public WithEvents selectionner As System.Windows.Forms.Button
    Public WithEvents ajouter As System.Windows.Forms.Button
    Public WithEvents affcat As System.Windows.Forms.CheckBox
    Public WithEvents categorie As ManagedCombo
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents frame1 As System.Windows.Forms.GroupBox
    Friend WithEvents SearchConditions1 As Clinica.SearchConditions
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents searchword As System.Windows.Forms.TextBox
    Public WithEvents _Labels_5 As System.Windows.Forms.Label
    Friend WithEvents searchwords As Clinica.ManagedCombo
    Public WithEvents searchbutton As System.Windows.Forms.Button
    Public WithEvents searchfields As System.Windows.Forms.ListBox
    Public WithEvents _Labels_7 As System.Windows.Forms.Label
    Public WithEvents frame2 As System.Windows.Forms.GroupBox
    Public WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents Expression As System.Windows.Forms.RadioButton
    Friend WithEvents TexteBrut As System.Windows.Forms.RadioButton
    Public WithEvents noKPFound As System.Windows.Forms.Label
    Friend WithEvents DataSetForGrid As System.Data.DataSet
    Friend WithEvents Symbol6 As System.Windows.Forms.Label
    Friend WithEvents Symbol4 As System.Windows.Forms.Label
    Friend WithEvents Symbol2 As System.Windows.Forms.Label
    Friend WithEvents Symbol5 As System.Windows.Forms.Label
    Friend WithEvents Symbol1 As System.Windows.Forms.Label
    Friend WithEvents Symbol3 As System.Windows.Forms.Label
    Friend WithEvents Table1 As System.Data.DataTable
    Friend WithEvents Table2 As System.Data.DataTable
    Friend WithEvents Nom As System.Data.DataColumn
    Friend WithEvents Adresse As System.Data.DataColumn
    Friend WithEvents Téléphones As System.Data.DataColumn
    Friend WithEvents Noducompte As System.Data.DataColumn
    Friend WithEvents Catégorie As System.Data.DataColumn
    Friend WithEvents Nom2 As System.Data.DataColumn
    Friend WithEvents Adresse2 As System.Data.DataColumn
    Friend WithEvents Téléphones2 As System.Data.DataColumn
    Friend WithEvents Noducompte2 As System.Data.DataColumn
    Friend WithEvents Symbol8 As System.Windows.Forms.Label
    Friend WithEvents KPTrouves As DataGridPlus
    Friend WithEvents Symbol7 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim dataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.delete = New System.Windows.Forms.Button
        Me.selectionner = New System.Windows.Forms.Button
        Me.ajouter = New System.Windows.Forms.Button
        Me.affcat = New System.Windows.Forms.CheckBox
        Me.categorie = New Clinica.ManagedCombo
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.searchbutton = New System.Windows.Forms.Button
        Me.Symbol6 = New System.Windows.Forms.Label
        Me.Symbol4 = New System.Windows.Forms.Label
        Me.Symbol2 = New System.Windows.Forms.Label
        Me.Symbol5 = New System.Windows.Forms.Label
        Me.Symbol1 = New System.Windows.Forms.Label
        Me.Symbol3 = New System.Windows.Forms.Label
        Me.Expression = New System.Windows.Forms.RadioButton
        Me.Symbol8 = New System.Windows.Forms.Label
        Me.Symbol7 = New System.Windows.Forms.Label
        Me.TexteBrut = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.frame1 = New System.Windows.Forms.GroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.searchwords = New Clinica.ManagedCombo
        Me.searchword = New System.Windows.Forms.TextBox
        Me._Labels_5 = New System.Windows.Forms.Label
        Me.searchfields = New System.Windows.Forms.ListBox
        Me._Labels_7 = New System.Windows.Forms.Label
        Me.frame2 = New System.Windows.Forms.GroupBox
        Me.noKPFound = New System.Windows.Forms.Label
        Me.KPTrouves = New DataGridPlus
        Me.label3 = New System.Windows.Forms.Label
        Me.DataSetForGrid = New System.Data.DataSet
        Me.Table1 = New System.Data.DataTable
        Me.Nom = New System.Data.DataColumn
        Me.Adresse = New System.Data.DataColumn
        Me.Téléphones = New System.Data.DataColumn
        Me.Noducompte = New System.Data.DataColumn
        Me.Table2 = New System.Data.DataTable
        Me.Catégorie = New System.Data.DataColumn
        Me.Nom2 = New System.Data.DataColumn
        Me.Adresse2 = New System.Data.DataColumn
        Me.Téléphones2 = New System.Data.DataColumn
        Me.Noducompte2 = New System.Data.DataColumn
        Me.frame1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.frame2.SuspendLayout()
        CType(Me.KPTrouves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSetForGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Table1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Table2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.delete.Location = New System.Drawing.Point(432, 88)
        Me.delete.Name = "delete"
        Me.delete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer la personne ou l'organisme clé sélectionné(e)")
        Me.delete.UseVisualStyleBackColor = False
        '
        'selectionner
        '
        Me.selectionner.BackColor = System.Drawing.SystemColors.Control
        Me.selectionner.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectionner.Enabled = False
        Me.selectionner.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectionner.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectionner.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectionner.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectionner.Location = New System.Drawing.Point(432, 128)
        Me.selectionner.Name = "selectionner"
        Me.selectionner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectionner.Size = New System.Drawing.Size(24, 24)
        Me.selectionner.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.selectionner, "Sélectionner la personne ou l'organisme clé")
        Me.selectionner.UseVisualStyleBackColor = False
        '
        'ajouter
        '
        Me.ajouter.BackColor = System.Drawing.SystemColors.Control
        Me.ajouter.Cursor = System.Windows.Forms.Cursors.Default
        Me.ajouter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ajouter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ajouter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ajouter.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ajouter.Location = New System.Drawing.Point(432, 48)
        Me.ajouter.Name = "ajouter"
        Me.ajouter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ajouter.Size = New System.Drawing.Size(24, 24)
        Me.ajouter.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.ajouter, "Ajout d'une personne ou d'un organisme clé")
        Me.ajouter.UseVisualStyleBackColor = False
        '
        'affcat
        '
        Me.affcat.BackColor = System.Drawing.SystemColors.Control
        Me.affcat.Cursor = System.Windows.Forms.Cursors.Default
        Me.affcat.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affcat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affcat.Location = New System.Drawing.Point(304, 200)
        Me.affcat.Name = "affcat"
        Me.affcat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.affcat.Size = New System.Drawing.Size(152, 32)
        Me.affcat.TabIndex = 7
        Me.affcat.Text = "Afficher la catégorie de chaque résultat"
        Me.affcat.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.affcat.UseVisualStyleBackColor = False
        '
        'categorie
        '
        Me.categorie.acceptAlpha = True
        Me.categorie.acceptedChars = Nothing
        Me.categorie.acceptNumeric = True
        Me.categorie.allCapital = False
        Me.categorie.allLower = False
        Me.categorie.autoComplete = True
        Me.categorie.autoSizeDropDown = True
        Me.categorie.BackColor = System.Drawing.Color.White
        Me.categorie.cb_AcceptNegative = False
        Me.categorie.currencyBox = False
        Me.categorie.Cursor = System.Windows.Forms.Cursors.Default
        Me.categorie.dbField = "KPCategorie.Categorie"
        Me.categorie.doComboDelete = True
        Me.categorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.categorie.firstLetterCapital = False
        Me.categorie.firstLettersCapital = False
        Me.categorie.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.categorie.ForeColor = System.Drawing.SystemColors.WindowText
        Me.categorie.Location = New System.Drawing.Point(8, 208)
        Me.categorie.manageText = False
        Me.categorie.matchExp = Nothing
        Me.categorie.Name = "categorie"
        Me.categorie.nbDecimals = CType(-1, Short)
        Me.categorie.onlyAlphabet = False
        Me.categorie.pathOfList = Nothing
        Me.categorie.ReadOnly = False
        Me.categorie.refuseAccents = False
        Me.categorie.refusedChars = Nothing
        Me.categorie.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.categorie.Size = New System.Drawing.Size(288, 22)
        Me.categorie.Sorted = True
        Me.categorie.TabIndex = 6
        Me.categorie.trimText = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
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
        'Symbol6
        '
        Me.Symbol6.AutoSize = True
        Me.Symbol6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol6.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol6.Location = New System.Drawing.Point(360, 128)
        Me.Symbol6.Name = "Symbol6"
        Me.Symbol6.Size = New System.Drawing.Size(22, 23)
        Me.Symbol6.TabIndex = 41
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
        Me.Symbol4.TabIndex = 39
        Me.Symbol4.Text = "<>"
        Me.ToolTip1.SetToolTip(Me.Symbol4, "Opérateur champ/valeur signifiant 'Ne doit pas contenir'")
        '
        'Symbol2
        '
        Me.Symbol2.AutoSize = True
        Me.Symbol2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol2.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol2.Location = New System.Drawing.Point(224, 128)
        Me.Symbol2.Name = "Symbol2"
        Me.Symbol2.Size = New System.Drawing.Size(39, 23)
        Me.Symbol2.TabIndex = 37
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
        Me.Symbol5.TabIndex = 40
        Me.Symbol5.Text = ">"
        Me.ToolTip1.SetToolTip(Me.Symbol5, "Opérateur champ/valeur signifiant 'Supérieur à'")
        '
        'Symbol1
        '
        Me.Symbol1.AutoSize = True
        Me.Symbol1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol1.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol1.Location = New System.Drawing.Point(168, 128)
        Me.Symbol1.Name = "Symbol1"
        Me.Symbol1.Size = New System.Drawing.Size(54, 23)
        Me.Symbol1.TabIndex = 36
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
        Me.Symbol3.TabIndex = 38
        Me.Symbol3.Text = "="
        Me.ToolTip1.SetToolTip(Me.Symbol3, "Opérateur champ/valeur signifiant 'Doit contenir'")
        '
        'Expression
        '
        Me.Expression.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Expression.Location = New System.Drawing.Point(328, 168)
        Me.Expression.Name = "Expression"
        Me.Expression.Size = New System.Drawing.Size(88, 16)
        Me.Expression.TabIndex = 26
        Me.Expression.Text = "Expression"
        Me.ToolTip1.SetToolTip(Me.Expression, "L'option expression recherche selon les groupes champ/valeur entrés")
        '
        'Symbol8
        '
        Me.Symbol8.AutoSize = True
        Me.Symbol8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Symbol8.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Symbol8.Location = New System.Drawing.Point(424, 128)
        Me.Symbol8.Name = "Symbol8"
        Me.Symbol8.Size = New System.Drawing.Size(34, 23)
        Me.Symbol8.TabIndex = 43
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
        Me.Symbol7.TabIndex = 42
        Me.Symbol7.Text = "<="
        Me.ToolTip1.SetToolTip(Me.Symbol7, "Opérateur champ/valeur signifiant 'Inférieur ou égal à'")
        '
        'TexteBrut
        '
        Me.TexteBrut.Checked = True
        Me.TexteBrut.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TexteBrut.Location = New System.Drawing.Point(224, 168)
        Me.TexteBrut.Name = "TexteBrut"
        Me.TexteBrut.Size = New System.Drawing.Size(80, 16)
        Me.TexteBrut.TabIndex = 28
        Me.TexteBrut.Text = "Texte brut"
        Me.ToolTip1.SetToolTip(Me.TexteBrut, "L'option texte brut recherche dans les champs Nom, Adresse ou Téléphones")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 192)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 15)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Catégorie :"
        '
        'Frame1
        '
        Me.frame1.BackColor = System.Drawing.SystemColors.Control
        Me.frame1.Controls.Add(Me.categorie)
        Me.frame1.Controls.Add(Me.Symbol8)
        Me.frame1.Controls.Add(Me.Symbol7)
        Me.frame1.Controls.Add(Me.Symbol6)
        Me.frame1.Controls.Add(Me.Symbol4)
        Me.frame1.Controls.Add(Me.Symbol2)
        Me.frame1.Controls.Add(Me.Symbol5)
        Me.frame1.Controls.Add(Me.Symbol1)
        Me.frame1.Controls.Add(Me.Symbol3)
        Me.frame1.Controls.Add(Me.Panel1)
        Me.frame1.Controls.Add(Me.searchfields)
        Me.frame1.Controls.Add(Me._Labels_7)
        Me.frame1.Controls.Add(Me.frame2)
        Me.frame1.Controls.Add(Me.label3)
        Me.frame1.Controls.Add(Me.Expression)
        Me.frame1.Controls.Add(Me.TexteBrut)
        Me.frame1.Controls.Add(Me.affcat)
        Me.frame1.Controls.Add(Me.Label1)
        Me.frame1.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame1.Location = New System.Drawing.Point(8, 0)
        Me.frame1.Name = "Frame1"
        Me.frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame1.Size = New System.Drawing.Size(465, 416)
        Me.frame1.TabIndex = 8
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
        Me.searchwords.autoSizeDropDown = True
        Me.searchwords.BackColor = System.Drawing.Color.White
        Me.searchwords.cb_AcceptNegative = False
        Me.searchwords.currencyBox = False
        Me.searchwords.dbField = "KPSearchList.KPSearchListItem"
        Me.searchwords.doComboDelete = True
        Me.searchwords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.searchwords.firstLetterCapital = False
        Me.searchwords.firstLettersCapital = False
        Me.searchwords.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchwords.Location = New System.Drawing.Point(0, 16)
        Me.searchwords.manageText = False
        Me.searchwords.matchExp = Nothing
        Me.searchwords.MaxDropDownItems = 15
        Me.searchwords.Name = "searchwords"
        Me.searchwords.nbDecimals = CType(-1, Short)
        Me.searchwords.onlyAlphabet = False
        Me.searchwords.pathOfList = Nothing
        Me.searchwords.ReadOnly = False
        Me.searchwords.refuseAccents = False
        Me.searchwords.refusedChars = ""
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
        Me.searchfields.Size = New System.Drawing.Size(153, 156)
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
        'Frame2
        '
        Me.frame2.BackColor = System.Drawing.Color.Transparent
        Me.frame2.Controls.Add(Me.noKPFound)
        Me.frame2.Controls.Add(Me.ajouter)
        Me.frame2.Controls.Add(Me.delete)
        Me.frame2.Controls.Add(Me.selectionner)
        Me.frame2.Controls.Add(Me.KPTrouves)
        Me.frame2.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame2.Location = New System.Drawing.Point(0, 232)
        Me.frame2.Name = "Frame2"
        Me.frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame2.Size = New System.Drawing.Size(465, 248)
        Me.frame2.TabIndex = 2
        Me.frame2.TabStop = False
        Me.frame2.Text = "Personne(s) / Organime(s) clé(s) trouvé(e)(s)"
        '
        'NoKPFound
        '
        Me.noKPFound.AutoSize = True
        Me.noKPFound.BackColor = System.Drawing.SystemColors.Control
        Me.noKPFound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.noKPFound.Cursor = System.Windows.Forms.Cursors.Default
        Me.noKPFound.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.noKPFound.ForeColor = System.Drawing.SystemColors.ControlText
        Me.noKPFound.Location = New System.Drawing.Point(16, 88)
        Me.noKPFound.Name = "NoKPFound"
        Me.noKPFound.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.noKPFound.Size = New System.Drawing.Size(377, 24)
        Me.noKPFound.TabIndex = 28
        Me.noKPFound.Text = "Aucun(e) personne/organisme clé trouvé(e)"
        '
        'KPTrouves
        '
        Me.KPTrouves.AllowUserToAddRows = False
        Me.KPTrouves.AllowUserToDeleteRows = False
        Me.KPTrouves.AllowUserToResizeRows = False
        Me.KPTrouves.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.KPTrouves.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.KPTrouves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.KPTrouves.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.KPTrouves.Location = New System.Drawing.Point(8, 24)
        Me.KPTrouves.MultiSelect = False
        Me.KPTrouves.Name = "KPTrouves"
        Me.KPTrouves.ReadOnly = True
        Me.KPTrouves.RowHeadersVisible = False
        dataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KPTrouves.RowsDefaultCellStyle = dataGridViewCellStyle1
        Me.KPTrouves.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.KPTrouves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.KPTrouves.ShowCellErrors = False
        Me.KPTrouves.ShowEditingIcon = False
        Me.KPTrouves.Size = New System.Drawing.Size(418, 151)
        Me.KPTrouves.StandardTab = True
        Me.KPTrouves.TabIndex = 3
        Me.KPTrouves.VirtualMode = True
        '
        'Label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.SystemColors.Control
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(168, 112)
        Me.label3.Name = "Label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(68, 14)
        Me.label3.TabIndex = 27
        Me.label3.Text = "Opérateurs :"
        '
        'DataSetForGrid
        '
        Me.DataSetForGrid.DataSetName = "dsClientsTrouves"
        Me.DataSetForGrid.Locale = New System.Globalization.CultureInfo("fr-CA")
        Me.DataSetForGrid.Tables.AddRange(New System.Data.DataTable() {Me.Table1, Me.Table2})
        '
        'Table1
        '
        Me.Table1.Columns.AddRange(New System.Data.DataColumn() {Me.Nom, Me.Adresse, Me.Téléphones, Me.Noducompte})
        Me.Table1.TableName = "Table1"
        '
        'Nom
        '
        Me.Nom.ColumnName = "Nom"
        '
        'Adresse
        '
        Me.Adresse.ColumnName = "Adresse"
        '
        'Téléphones
        '
        Me.Téléphones.ColumnName = "Téléphones"
        '
        'Noducompte
        '
        Me.Noducompte.ColumnName = "# du compte"
        '
        'Table2
        '
        Me.Table2.Columns.AddRange(New System.Data.DataColumn() {Me.Catégorie, Me.Nom2, Me.Adresse2, Me.Téléphones2, Me.Noducompte2})
        Me.Table2.TableName = "Table2"
        '
        'Catégorie
        '
        Me.Catégorie.ColumnName = "Catégorie"
        '
        'Nom2
        '
        Me.Nom2.ColumnName = "Nom"
        '
        'Adresse2
        '
        Me.Adresse2.ColumnName = "Adresse"
        '
        'Téléphones2
        '
        Me.Téléphones2.ColumnName = "Téléphones"
        '
        'Noducompte2
        '
        Me.Noducompte2.ColumnName = "# du compte"
        '
        'keypeople
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(482, 424)
        Me.Controls.Add(Me.frame1)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(292, 233)
        Me.MaximizeBox = False
        Me.Name = "keypeople"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Recherche d'un(e) personne / organisme clé"
        Me.frame1.ResumeLayout(False)
        Me.frame1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.frame2.ResumeLayout(False)
        Me.frame2.PerformLayout()
        CType(Me.KPTrouves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSetForGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Table1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Table2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
    Dim sel As Boolean = False
    Dim sc As String = ""
    Private ThisSettings(), Fields(), oldCategorie As String
    Private _TableUsed As String = "Table1"
    Private kpSelected As KPSelectorReturn

    Shadows Function showDialog() As KPSelectorReturn
        kpSelected = New KPSelectorReturn()
        MyBase.ShowDialog()
        Return kpSelected
    End Function

#Region "Propriétés"
    Private Property tableUsed() As String
        Get
            Return _TableUsed
        End Get
        Set(ByVal Value As String)
            _TableUsed = Value
        End Set
    End Property

    Public Property selected() As Boolean
        Get
            selected = sel
        End Get
        Set(ByVal Value As Boolean)
            sel = Value
        End Set
    End Property

    Public Property specificCat() As String
        Get
            specificCat = sc
        End Get
        Set(ByVal Value As String)
            sc = Value
            If Value = "" Then
                categorie.Enabled = True
            Else
                categorie.Enabled = False
            End If
        End Set
    End Property
#End Region

    Private Sub searchword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles searchword.KeyDown
        If e.KeyCode = Keys.Enter Then e.Handled = True : searchbutton_Click(searchbutton, EventArgs.Empty)
    End Sub

    Private Sub searchword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchword.TextChanged
        If searchword.Text <> "" Then searchword.Text = searchword.Text.Replace(vbCrLf, "")
    End Sub

    Private Sub ajouter_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ajouter.Click
        addKP("", "", "", "", "", "", "", "", "", "", "", Me)
    End Sub

    Private Sub delete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles delete.Click
        'Droit & Accès
        If currentDroitAcces(52) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de supprimer une personne / organisme clé." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If MessageBox.Show("Êtes-vous certain de vouloir supprimer " & KPTrouves.Item(If(affcat.Checked, 1, 0), KPTrouves.currentRow.Index).Value & " ?", "Confirmation de suppresion", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim addingOne As Byte = 0
        If affcat.Checked = True Then addingOne = 1

        Dim delMsg As String = delAccount(KPTrouves.Item(3 + addingOne, KPTrouves.currentRow.Index).Value, True)
        If delMsg = "" Then
            Dim curDataRow As DataRow
            For i As Integer = 0 To DataSetForGrid.Tables(0).Rows.Count - 1
                If DataSetForGrid.Tables(0).Rows(i)("# du compte") = KPTrouves.currentRow.Cells("# du compte").Value Then curDataRow = DataSetForGrid.Tables(0).Rows(i)
            Next
            DataSetForGrid.Tables(0).Rows.Remove(curDataRow)

            If DataSetForGrid.Tables(0).Rows.Count = 0 Then
                delete.Enabled = False
                selectionner.Enabled = False
            End If
        Else
            MessageBox.Show(delMsg, "Suppression impossible")
        End If
    End Sub

    Private Sub keypeople_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim FSIndex, i As Integer
        Dim cat() As String
        'Chargement des champs
        Dim sFields() As String
        Fields = readFile("Data\Lists\searchkp.lst")

        If Not Microsoft.VisualBasic.Left(CStr(Fields(0)), 5) = "ERROR" Then
            For i = 0 To Fields.Length - 1
                sFields = Fields(i).Split(New Char() {"§"})
                searchfields.Items.Add(sFields(0))
            Next i
        End If

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        'Chargement des catégories
        cat = DBLinker.getInstance.readOneDBField("KPCategorie", "Categorie", , True)
        If Not cat Is Nothing AndAlso cat.Length <> 0 Then categorie.Items.AddRange(cat)
        categorie.Items.Add("* Toutes les catégories *")

        If selected = True And Not specificCat = "" Then
            FSIndex = categorie.findString(specificCat)
            If FSIndex >= 0 Then
                categorie.SelectedIndex = FSIndex
            Else
                categorie.SelectedIndex = 0
            End If
        Else
            categorie.SelectedIndex = 0
        End If

        'Chargement de searchwords
        Dim searchList() As String = DBLinker.getInstance.readOneDBField("KPSearchList", "KPSearchListItem", "WHERE ((NoUser)=" & ConnectionsManager.currentUser & ");", , True)
        If Not searchList Is Nothing AndAlso searchList.Length <> 0 Then searchwords.Items.AddRange(searchList)

        'Settings du user pour la fenêtre
        Dim setting As String = UsersManager.currentUser.settings.searchKPStyle
        If setting = "" Then
            ReDim ThisSettings(8)
            ThisSettings(0) = 171
            ThisSettings(1) = 144
            ThisSettings(2) = 150
            ThisSettings(3) = 103
            ThisSettings(4) = 116
            ThisSettings(5) = True
            ThisSettings(6) = "Nom"
            ThisSettings(7) = ""
            ThisSettings(8) = False
        Else
            ThisSettings = setting.Split(New Char() {"§"})

            TexteBrut.Checked = CBool(ThisSettings(5))
            Expression.Checked = Not CBool(ThisSettings(5))
            If selected = False Then categorie.Text = ThisSettings(7)
            affcat.Checked = ThisSettings(8)
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
    End Sub

    Private Sub selectionner_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectionner.Click
        kpSelected = New KPSelectorReturn
        kpSelected.noKP = KPTrouves.Item("# du compte", KPTrouves.currentRow.Index).Value
        kpSelected.kpFullName = KPTrouves.Item("Nom", KPTrouves.currentRow.Index).Value

        If Me.MdiParent IsNot Nothing Then openAccount(kpSelected.noKP, CompteType.KP)

        Me.Close()
    End Sub

    Private Sub searchfields_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchfields.DoubleClick
        searchword.SelectedText = "(" & searchfields.GetItemText(searchfields.SelectedItem) & ")"
        Expression.Checked = True
        searchword.Focus()
        searchword.Select(searchword.Text.Length, 0)
    End Sub

    Private Sub searchfields_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles searchfields.KeyDown
        If e.KeyCode = Keys.Enter Then searchfields_DoubleClick(sender, EventArgs.Empty)
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

    Private Sub searchbutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchbutton.Click
        Dim selCat As String = "", CatField As String = ""
        tableUsed = "Table1"
        If DataSetForGrid IsNot Nothing Then DataSetForGrid.Clear()
        selectionner.Enabled = False
        searchbutton.Enabled = False
        Dim spaceRemoval() As Char = {" "}
        Dim isThereData As Boolean = False

        searchword.Text = searchword.Text.TrimStart(spaceRemoval)
        searchword.Text = searchword.Text.TrimEnd(spaceRemoval)

        If TexteBrut.Checked = True Then
            Dim adjustedSearchWord As String = searchword.Text.Replace("'", "''")
            If categorie.SelectedIndex > 0 Then selCat = " AND ((Categorie) LIKE '" & categorie.Text.Replace("'", "''") & ":%' OR (Categorie) = '" & categorie.Text.Replace("'", "''") & "')"
            If affcat.Checked = True Then CatField = "Categorie AS Catégorie, " : tableUsed = "Table2"
            DataSetForGrid = DBLinker.getInstance.readDBForGrid("KeyPeople LEFT JOIN KPCategorie ON KeyPeople.NoCategorie=KPCategorie.NoCategorie", CatField & "Nom, Adresse, Telephones AS [Téléphones], NoKP AS [# du compte]", "WHERE (((Nom) LIKE '%" & adjustedSearchWord & "%' OR Adresse LIKE '%" & adjustedSearchWord & "%' OR Telephones LIKE '%" & adjustedSearchWord & "%')" & selCat & ");", False, , tableUsed)
            If DataSetForGrid IsNot Nothing AndAlso Not DataSetForGrid.Tables(tableUsed) Is Nothing Then
                KPTrouves.DataSource = DataSetForGrid.Tables(tableUsed).DefaultView
                If affcat.Checked = True Then KPTrouves.Columns("Catégorie").DisplayIndex = 0
                isThereData = KPTrouves.Rows.Count <> 0
            End If
        Else
            searchword.Text = searchword.Text.Replace("  ", " ")
            Dim aExp As AnalysedExpression = analyseExpression(Fields, searchword.Text)
            If aExp.conditions = "" Then
                searchword.SelectionStart = aExp.ErrorPos
                searchword.SelectionLength = aExp.errorLength
                searchword.Focus()
                searchword.ScrollToCaret()
            Else
                'Get correspondant data into database
                Dim i, sa As Integer
                Dim Tables, PreviousTable, sFields() As String
                PreviousTable = "KeyPeople"
                Tables = ""

                If categorie.SelectedIndex > 0 Then aExp.conditions = "((" & aExp.conditions & ") AND KPCategorie.Categorie LIKE '" & categorie.Text.Replace("'", "''") & "%');"

                If (categorie.SelectedIndex > 0 Or affcat.Checked = True) And searchArray(aExp.selTables, "KPCategorie", SearchType.ExactMatch) < 0 Then
                    ReDim Preserve aExp.selTables(aExp.selTables.Length)
                    aExp.selTables(aExp.selTables.Length - 1) = "KPCategorie"
                End If

                For i = 0 To aExp.selTables.Length - 1
                    If aExp.selTables(i) <> "KeyPeople" Then
                        sa = searchArray(Fields, "§" & aExp.selTables(i) & "§", SearchType.InString)
                        sFields = Fields(sa).Split(New Char() {"§"})

                        If Tables.IndexOf("RIGHT JOIN") <> -1 Then
                            Tables = Tables.Replace(" " & PreviousTable & " ", " (" & aExp.selTables(i) & " RIGHT JOIN " & PreviousTable & " ON " & PreviousTable & "." & sFields(4) & "=" & aExp.selTables(i) & "." & sFields(4) & ") ")
                        Else
                            Tables &= aExp.selTables(i) & " RIGHT JOIN " & PreviousTable & " ON " & PreviousTable & "." & sFields(4) & "=" & aExp.selTables(i) & "." & sFields(4)
                        End If
                    End If
                Next i

                If Tables = "" Then Tables = PreviousTable

                SearchConditions1.text = aExp.conditions
                SearchConditions1.Visible = True
                If affcat.Checked = True Then CatField = "Categorie AS Catégorie, " : tableUsed = "Table2"
                DataSetForGrid = DBLinker.getInstance.readDBForGrid(Tables, CatField & "Nom, Adresse, Telephones AS [Téléphones], NoKP AS [# du compte]", "WHERE " & aExp.conditions, False, , tableUsed)
                If DataSetForGrid IsNot Nothing AndAlso Not DataSetForGrid.Tables(tableUsed) Is Nothing Then
                    KPTrouves.DataSource = DataSetForGrid.Tables(tableUsed).DefaultView
                    If affcat.Checked = True Then KPTrouves.Columns("Catégorie").DisplayIndex = 0
                    DBHelper.addItemToADBList("KPSearchList", "KPSearchListItem", searchword.Text, "NoKPSearchList", 15, , , , "NoUser", ConnectionsManager.currentUser)
                    searchwords.Items.Clear()
                    Dim searchList() As String = DBLinker.getInstance.readOneDBField("KPSearchList", "KPSearchListItem", "WHERE ((NoUser)=" & ConnectionsManager.currentUser & ");", , True)
                    If Not searchList Is Nothing AndAlso searchList.Length <> 0 Then searchwords.Items.AddRange(searchList)
                    isThereData = KPTrouves.Rows.Count <> 0
                End If
            End If
        End If

        If isThereData Then
            KPTrouves.Rows(0).Selected = True
            KPTrouves.Select()
            noKPFound.Visible = False
        Else
            noKPFound.Visible = True
            delete.Enabled = False
            selectionner.Enabled = False
        End If

        If Not ThisSettings Is Nothing AndAlso ThisSettings.Length <> 0 AndAlso DataSetForGrid IsNot Nothing AndAlso Not DataSetForGrid.Tables(tableUsed) Is Nothing Then
            Dim n As Byte = 0
            If tableUsed = "Table1" Then n = 1
            Dim myGCS As DataGridViewColumn
            For Each myGCS In Me.KPTrouves.Columns
                myGCS.Width = ThisSettings(n)
                n += 1
            Next

            Try
                DataSetForGrid.Tables(tableUsed).DefaultView.Sort = ThisSettings(6)
            Catch
            End Try
        End If

        searchbutton.Enabled = True
    End Sub

    Private Sub keypeople_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim myGCS As DataGridViewColumn
        Dim mySettings As String = ""
        Dim isThereData As Boolean = False

        If Not DataSetForGrid.Tables(tableUsed) Is Nothing Then If DataSetForGrid.Tables(tableUsed).Rows.Count > 0 Then isThereData = True

        If isThereData = False Then
            If ThisSettings Is Nothing OrElse ThisSettings.Length = 0 Then
                mySettings = "171§144§150§103§116§" & TexteBrut.Checked & "§"
            Else
                mySettings = ThisSettings(0) & "§" & ThisSettings(1) & "§" & ThisSettings(2) & "§" & ThisSettings(3) & "§" & ThisSettings(4) & "§" & TexteBrut.Checked & "§" & ThisSettings(6)
            End If
        Else
            If tableUsed = "Table1" Then
                If ThisSettings Is Nothing OrElse ThisSettings.Length = 0 Then
                    mySettings = "171§"
                Else
                    mySettings = ThisSettings(0) & "§"
                End If
            End If
            For Each myGCS In Me.KPTrouves.Columns
                mySettings &= myGCS.Width & "§"
            Next

            mySettings &= TexteBrut.Checked & "§" & DataSetForGrid.Tables(tableUsed).DefaultView.Sort
        End If

        If selected = False Then oldCategorie = categorie.Text
        mySettings &= "§" & oldCategorie & "§" & affcat.Checked

        Dim curUser As User = UsersManager.currentUser
        curUser.settings.searchKPStyle = mySettings
        curUser.settings.saveData()
    End Sub

    Private Sub kpTrouves_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles KPTrouves.DoubleClick
        If selectionner.Enabled = True Then selectionner_Click(sender, e)
    End Sub

    Private Sub kpTrouves_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles KPTrouves.KeyDown
        If e.KeyCode = Keys.Enter Then kpTrouves_DoubleClick(sender, New EventArgs())
    End Sub

    Private Sub kpTrouves_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles KPTrouves.RowEnter
        delete.Enabled = True
        selectionner.Enabled = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
