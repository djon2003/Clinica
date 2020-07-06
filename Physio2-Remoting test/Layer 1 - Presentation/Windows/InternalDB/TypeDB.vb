Friend Class TypeDB
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin
        configList(AllNames)

        'Chargement des images
        Me.Adding.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.addExt.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.DelExt.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.Deleting.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.Modifying.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.Renaming.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("rename16.ico"), New Size(16, 16))
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DBType As ManagedCombo
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NewExtension As ManagedText
    Friend WithEvents addExt As System.Windows.Forms.Button
    Friend WithEvents DBExtensions As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DelExt As System.Windows.Forms.Button
    Friend WithEvents Adding As System.Windows.Forms.Button
    Friend WithEvents Deleting As System.Windows.Forms.Button
    Friend WithEvents Renaming As System.Windows.Forms.Button
    Friend WithEvents Internal As System.Windows.Forms.RadioButton
    Friend WithEvents External As System.Windows.Forms.RadioButton
    Friend WithEvents Modifying As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents DBReadOnly As System.Windows.Forms.CheckBox
    Friend WithEvents DBHidden As System.Windows.Forms.CheckBox
    Friend WithEvents DBSearchIn As System.Windows.Forms.CheckBox
    Friend WithEvents NbItems As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DBPrintable As System.Windows.Forms.CheckBox
    Friend WithEvents DBSelectable As System.Windows.Forms.CheckBox
    Friend WithEvents AllNames As CI.Controls.List
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TypeDB))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.DBSelectable = New System.Windows.Forms.CheckBox
        Me.DBPrintable = New System.Windows.Forms.CheckBox
        Me.DBSearchIn = New System.Windows.Forms.CheckBox
        Me.DBHidden = New System.Windows.Forms.CheckBox
        Me.DBReadOnly = New System.Windows.Forms.CheckBox
        Me.DelExt = New System.Windows.Forms.Button
        Me.Internal = New System.Windows.Forms.RadioButton
        Me.External = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.DBExtensions = New System.Windows.Forms.ListBox
        Me.addExt = New System.Windows.Forms.Button
        Me.NewExtension = New ManagedText
        Me.Label2 = New System.Windows.Forms.Label
        Me.DBType = New ManagedCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.AllNames = New CI.Controls.List
        Me.NbItems = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Adding = New System.Windows.Forms.Button
        Me.Deleting = New System.Windows.Forms.Button
        Me.Modifying = New System.Windows.Forms.Button
        Me.Renaming = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DBSelectable)
        Me.GroupBox1.Controls.Add(Me.DBPrintable)
        Me.GroupBox1.Controls.Add(Me.DBSearchIn)
        Me.GroupBox1.Controls.Add(Me.DBHidden)
        Me.GroupBox1.Controls.Add(Me.DBReadOnly)
        Me.GroupBox1.Controls.Add(Me.DelExt)
        Me.GroupBox1.Controls.Add(Me.Internal)
        Me.GroupBox1.Controls.Add(Me.External)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.DBExtensions)
        Me.GroupBox1.Controls.Add(Me.addExt)
        Me.GroupBox1.Controls.Add(Me.NewExtension)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.DBType)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(176, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(184, 244)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'DBSelectable
        '
        Me.DBSelectable.BackColor = System.Drawing.Color.Transparent
        Me.DBSelectable.Enabled = False
        Me.DBSelectable.Location = New System.Drawing.Point(-8, 210)
        Me.DBSelectable.Name = "DBSelectable"
        Me.DBSelectable.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DBSelectable.Size = New System.Drawing.Size(184, 35)
        Me.DBSelectable.TabIndex = 7
        Me.DBSelectable.Text = "  : Sélectionnable dans la modification d'un item"
        Me.DBSelectable.UseVisualStyleBackColor = False
        '
        'DBPrintable
        '
        Me.DBPrintable.BackColor = System.Drawing.Color.Transparent
        Me.DBPrintable.Enabled = False
        Me.DBPrintable.Location = New System.Drawing.Point(-8, 194)
        Me.DBPrintable.Name = "DBPrintable"
        Me.DBPrintable.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DBPrintable.Size = New System.Drawing.Size(184, 16)
        Me.DBPrintable.TabIndex = 7
        Me.DBPrintable.Text = "  : Imprimable"
        Me.DBPrintable.UseVisualStyleBackColor = False
        '
        'DBSearchIn
        '
        Me.DBSearchIn.BackColor = System.Drawing.Color.Transparent
        Me.DBSearchIn.Location = New System.Drawing.Point(-8, 178)
        Me.DBSearchIn.Name = "DBSearchIn"
        Me.DBSearchIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DBSearchIn.Size = New System.Drawing.Size(184, 16)
        Me.DBSearchIn.TabIndex = 7
        Me.DBSearchIn.Text = "  : Rechercher dans le contenu"
        Me.DBSearchIn.UseVisualStyleBackColor = False
        '
        'DBHidden
        '
        Me.DBHidden.BackColor = System.Drawing.Color.Transparent
        Me.DBHidden.Location = New System.Drawing.Point(112, 162)
        Me.DBHidden.Name = "DBHidden"
        Me.DBHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DBHidden.Size = New System.Drawing.Size(64, 16)
        Me.DBHidden.TabIndex = 6
        Me.DBHidden.Text = ": Caché"
        Me.DBHidden.UseVisualStyleBackColor = False
        '
        'DBReadOnly
        '
        Me.DBReadOnly.BackColor = System.Drawing.Color.Transparent
        Me.DBReadOnly.Location = New System.Drawing.Point(-4, 162)
        Me.DBReadOnly.Name = "DBReadOnly"
        Me.DBReadOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DBReadOnly.Size = New System.Drawing.Size(104, 16)
        Me.DBReadOnly.TabIndex = 5
        Me.DBReadOnly.Text = ": Lecture seule"
        Me.DBReadOnly.UseVisualStyleBackColor = False
        '
        'DelExt
        '
        Me.DelExt.Enabled = False
        Me.DelExt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelExt.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelExt.Location = New System.Drawing.Point(151, 112)
        Me.DelExt.Name = "DelExt"
        Me.DelExt.Size = New System.Drawing.Size(24, 24)
        Me.DelExt.TabIndex = 9
        Me.DelExt.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DelExt, "Enlever une extension")
        '
        'Internal
        '
        Me.Internal.BackColor = System.Drawing.Color.Transparent
        Me.Internal.Enabled = False
        Me.Internal.Location = New System.Drawing.Point(128, 146)
        Me.Internal.Name = "Internal"
        Me.Internal.Size = New System.Drawing.Size(64, 16)
        Me.Internal.TabIndex = 11
        Me.Internal.Text = "Interne"
        Me.Internal.UseVisualStyleBackColor = False
        '
        'External
        '
        Me.External.BackColor = System.Drawing.Color.Transparent
        Me.External.Checked = True
        Me.External.Location = New System.Drawing.Point(64, 146)
        Me.External.Name = "External"
        Me.External.Size = New System.Drawing.Size(64, 16)
        Me.External.TabIndex = 4
        Me.External.TabStop = True
        Me.External.Text = "Externe"
        Me.External.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 146)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Ouverture :"
        '
        'DBExtensions
        '
        Me.DBExtensions.Location = New System.Drawing.Point(8, 80)
        Me.DBExtensions.Name = "DBExtensions"
        Me.DBExtensions.ScrollAlwaysVisible = True
        Me.DBExtensions.Size = New System.Drawing.Size(137, 56)
        Me.DBExtensions.Sorted = True
        Me.DBExtensions.TabIndex = 3
        '
        'addExt
        '
        Me.addExt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addExt.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.addExt.Location = New System.Drawing.Point(151, 56)
        Me.addExt.Name = "addExt"
        Me.addExt.Size = New System.Drawing.Size(25, 24)
        Me.addExt.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.addExt, "Ajouter une extension")
        '
        'NewExtension
        '
        Me.NewExtension.acceptAlpha = True
        Me.NewExtension.acceptedChars = ""
        Me.NewExtension.acceptNumeric = True
        Me.NewExtension.allCapital = True
        Me.NewExtension.allLower = False
        Me.NewExtension.blockOnMaximum = False
        Me.NewExtension.blockOnMinimum = False
        Me.NewExtension.cb_AcceptLeftZeros = False
        Me.NewExtension.cb_AcceptNegative = False
        Me.NewExtension.currencyBox = False
        Me.NewExtension.firstLetterCapital = False
        Me.NewExtension.firstLettersCapital = False
        Me.NewExtension.Location = New System.Drawing.Point(8, 56)
        Me.NewExtension.manageText = True
        Me.NewExtension.matchExp = ""
        Me.NewExtension.maximum = 0
        Me.NewExtension.MaxLength = 10
        Me.NewExtension.minimum = 0
        Me.NewExtension.Name = "NewExtension"
        Me.NewExtension.nbDecimals = CType(-1, Short)
        Me.NewExtension.onlyAlphabet = True
        Me.NewExtension.refuseAccents = True
        Me.NewExtension.refusedChars = ""
        Me.NewExtension.showInternalContextMenu = True
        Me.NewExtension.Size = New System.Drawing.Size(137, 20)
        Me.NewExtension.TabIndex = 1
        Me.NewExtension.trimText = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Extensions :"
        '
        'DBType
        '
        Me.DBType.acceptAlpha = True
        Me.DBType.acceptedChars = Nothing
        Me.DBType.acceptNumeric = True
        Me.DBType.allCapital = False
        Me.DBType.allLower = False
        Me.DBType.autoComplete = True
        Me.DBType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.DBType.autoSizeDropDown = True
        Me.DBType.BackColor = System.Drawing.Color.White
        Me.DBType.blockOnMaximum = False
        Me.DBType.blockOnMinimum = False
        Me.DBType.cb_AcceptLeftZeros = False
        Me.DBType.cb_AcceptNegative = False
        Me.DBType.currencyBox = False
        Me.DBType.dbField = Nothing
        Me.DBType.doComboDelete = True
        Me.DBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DBType.firstLetterCapital = False
        Me.DBType.firstLettersCapital = False
        Me.DBType.Items.AddRange(New Object() {"Document", "Image", "Lien", "Son", "Vidéo"})
        Me.DBType.itemsToolTipDuration = 10000
        Me.DBType.Location = New System.Drawing.Point(96, 16)
        Me.DBType.manageText = True
        Me.DBType.matchExp = Nothing
        Me.DBType.maximum = 0
        Me.DBType.minimum = 0
        Me.DBType.Name = "DBType"
        Me.DBType.nbDecimals = CType(-1, Short)
        Me.DBType.onlyAlphabet = False
        Me.DBType.pathOfList = Nothing
        Me.DBType.ReadOnly = False
        Me.DBType.refuseAccents = False
        Me.DBType.refusedChars = Nothing
        Me.DBType.showItemsToolTip = False
        Me.DBType.Size = New System.Drawing.Size(80, 21)
        Me.DBType.Sorted = True
        Me.DBType.TabIndex = 0
        Me.DBType.trimText = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Type spécifique :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.AllNames)
        Me.GroupBox2.Controls.Add(Me.NbItems)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(176, 244)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Noms"
        '
        'AllNames
        '
        Me.AllNames.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.AllNames.autoAdjust = True
        Me.AllNames.autoKeyDownSelection = True
        Me.AllNames.autoSizeHorizontally = False
        Me.AllNames.autoSizeVertically = False
        Me.AllNames.BackColor = System.Drawing.Color.White
        Me.AllNames.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.AllNames.baseBackColor = System.Drawing.Color.White
        Me.AllNames.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.AllNames.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.AllNames.bgColor = System.Drawing.Color.White
        Me.AllNames.borderColor = System.Drawing.Color.Empty
        Me.AllNames.borderSelColor = System.Drawing.Color.Empty
        Me.AllNames.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.AllNames.CausesValidation = False
        Me.AllNames.clickEnabled = True
        Me.AllNames.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.AllNames.do3D = False
        Me.AllNames.draw = False
        Me.AllNames.extraWidth = 0
        Me.AllNames.hScrollColor = System.Drawing.SystemColors.Control
        Me.AllNames.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.AllNames.hScrolling = True
        Me.AllNames.hsValue = 0
        Me.AllNames.icons = CType(resources.GetObject("AllNames.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.AllNames.itemBorder = 0
        Me.AllNames.itemMargin = 0
        Me.AllNames.items = CType(resources.GetObject("AllNames.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.AllNames.Location = New System.Drawing.Point(8, 16)
        Me.AllNames.mouseMove3D = False
        Me.AllNames.mouseSpeed = 0
        Me.AllNames.Name = "AllNames"
        Me.AllNames.objMaxHeight = 0.0!
        Me.AllNames.objMaxWidth = 0.0!
        Me.AllNames.objMinHeight = 0.0!
        Me.AllNames.objMinWidth = 0.0!
        Me.AllNames.reverseSorting = False
        Me.AllNames.selected = -1
        Me.AllNames.selectedClickAllowed = False
        Me.AllNames.selectMultiple = False
        Me.AllNames.Size = New System.Drawing.Size(152, 209)
        Me.AllNames.sorted = True
        Me.AllNames.TabIndex = 0
        Me.AllNames.TabStop = False
        Me.AllNames.toolTipText = Nothing
        Me.AllNames.vScrollColor = System.Drawing.SystemColors.Control
        Me.AllNames.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.AllNames.vScrolling = True
        Me.AllNames.vsValue = 0
        '
        'NbItems
        '
        Me.NbItems.AutoSize = True
        Me.NbItems.BackColor = System.Drawing.Color.Transparent
        Me.NbItems.Location = New System.Drawing.Point(90, 228)
        Me.NbItems.Name = "NbItems"
        Me.NbItems.Size = New System.Drawing.Size(13, 13)
        Me.NbItems.TabIndex = 6
        Me.NbItems.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(6, 228)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Nombre d'items : "
        '
        'Adding
        '
        Me.Adding.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Adding.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Adding.Location = New System.Drawing.Point(368, 28)
        Me.Adding.Name = "Adding"
        Me.Adding.Size = New System.Drawing.Size(24, 24)
        Me.Adding.TabIndex = 0
        Me.Adding.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Adding, "Ajout d'un type")
        '
        'Deleting
        '
        Me.Deleting.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Deleting.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Deleting.Location = New System.Drawing.Point(368, 148)
        Me.Deleting.Name = "Deleting"
        Me.Deleting.Size = New System.Drawing.Size(24, 24)
        Me.Deleting.TabIndex = 3
        Me.Deleting.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Deleting, "Supprimer le type sélectionné")
        '
        'Modifying
        '
        Me.Modifying.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Modifying.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Modifying.Location = New System.Drawing.Point(368, 68)
        Me.Modifying.Name = "Modifying"
        Me.Modifying.Size = New System.Drawing.Size(24, 24)
        Me.Modifying.TabIndex = 1
        Me.Modifying.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Modifying, "Enregistrer le type sélectionné")
        '
        'Renaming
        '
        Me.Renaming.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Renaming.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Renaming.Location = New System.Drawing.Point(368, 108)
        Me.Renaming.Name = "Renaming"
        Me.Renaming.Size = New System.Drawing.Size(24, 24)
        Me.Renaming.TabIndex = 2
        Me.Renaming.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Renaming, "Renommer le type sélectionné")
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'TypeDB
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(402, 263)
        Me.Controls.Add(Me.Renaming)
        Me.Controls.Add(Me.Modifying)
        Me.Controls.Add(Me.Deleting)
        Me.Controls.Add(Me.Adding)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "TypeDB"
        Me.ShowInTaskbar = False
        Me.Text = "Types de fichiers"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private formModified As Boolean = False

    Private Sub typeDB_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DBType.SelectedIndex = 0
        loadNames()

        'Droit & Accès
        If currentDroitAcces(8) = False Then
            lockItems(True, , True)
            Exit Sub
        End If

        If lockSecteur("TypeDB.modif", True, "Types de fichiers") Then
            lockItems(True)
        Else
            lockItems(True, , True)
        End If
        DBPrintable.Enabled = False
        If currentUserName = "Administrateur" Then
            DBPrintable.Enabled = True
            Internal.Enabled = True
        End If
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal emptying As Boolean = False, Optional ByVal allItems As Boolean = False)
        DBType.ReadOnly = trueFalse
        NewExtension.ReadOnly = trueFalse
        addExt.Enabled = Not trueFalse
        DelExt.Enabled = False
        DBExtensions.Enabled = Not trueFalse
        External.Enabled = Not trueFalse
        Modifying.Enabled = Not trueFalse
        Renaming.Enabled = Not trueFalse
        Deleting.Enabled = Not trueFalse
        DBSearchIn.Enabled = Not trueFalse
        DBHidden.Enabled = Not trueFalse
        DBReadOnly.Enabled = Not trueFalse
        If currentUserName = "Administrateur" Then
            Internal.Enabled = Not trueFalse
            DBPrintable.Enabled = Not trueFalse
            DBSelectable.Enabled = Not trueFalse
        End If
        If allItems = True Then
            Adding.Enabled = Not trueFalse
        End If

        If emptying = True Then
            NewExtension.Text = ""
            DBExtensions.Items.Clear()
            External.Checked = True
            DBHidden.Checked = False
            DBReadOnly.Checked = False
            DBSearchIn.Checked = False
        End If
    End Sub

    Public Sub loadNames(Optional ByVal nameToSelect As String = "")
        If nameToSelect = "" And AllNames.selected <> -1 Then nameToSelect = AllNames.ItemText(AllNames.selected)
        AllNames.cls()

        Dim myTypes As Generic.List(Of TypeFile) = TypesFilesManager.getInstance.getItemables()

        Dim n As Integer
        For Each CurType As TypeFile In myTypes
            n = AllNames.add(CurType.toString)
            AllNames.ItemValueA(n) = CurType
            AllNames.ItemToolTipText(n) = CurType.toString
        Next CurType

        formModified = False
        AllNames.draw = True : AllNames.draw = False
        If nameToSelect <> "" Then AllNames.selected = AllNames.findStringExact(nameToSelect)
    End Sub

    Private Sub adding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Adding.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = "\§/§:§*§?§""§<§>§|§%"
        Dim myName As String = myInputBoxPlus.Prompt("Veuillez entrer un nom pour ce nouveau type", "Nouveau type", "Nouveau type")
        If myName = "" Then Exit Sub
        If AllNames.findStringExact(myName) >= 0 Then MessageBox.Show("Un type possède déjà ce nom. Veuillez en choisir un autre", "Nom déjà existant") : Exit Sub

        TypesFilesManager.addTypeFile(myName)
        AllNames.selected = AllNames.findStringExact(myName)
        'LoadNames(MyName)
    End Sub

    Private Sub modifying_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modifying.Click
        Dim curType As TypeFile = AllNames.ItemValueA(AllNames.selected)
        If DBType.SelectedItem.ToString <> "" Then curType.baseFileType = [Enum].Parse(curType.baseFileType.GetType, DBType.SelectedItem.ToString, True)
        curType.isInternal = Internal.Checked
        Dim myExts() As String
        ReDim myExts(DBExtensions.Items.Count - 1)
        DBExtensions.Items.CopyTo(myExts, 0)
        curType.extensions = String.Join(";", myExts)
        curType.isHidden = DBHidden.Checked
        curType.isReadOnly = DBReadOnly.Checked
        curType.searchInContent = DBSearchIn.Checked
        curType.printable = DBPrintable.Checked

        curType.saveData()

        myMainWin.StatusText = "Types de fichiers : Type " & AllNames.ItemText(AllNames.selected) & " modifié"
        formModified = False
    End Sub

    Private Sub renaming_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Renaming.Click
        If formModified = True And Adding.Enabled = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modifying_Click(AllNames, EventArgs.Empty)
        formModified = False

        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = "\§/§:§*§?§""§<§>§|§%"
        Dim oldName As String = AllNames.ItemText(AllNames.selected)
        Dim myName As String = myInputBoxPlus("Veuillez entrer un nouveau nom pour ce type", "Nouveau nom", oldName)
        If myName = "" Or myName = oldName Then Exit Sub
        If AllNames.findStringExact(myName) <> -1 And myName.ToUpper <> oldName.ToUpper Then MessageBox.Show("Un type possède déjà ce nom. Veuillez en choisir un autre", "Nom déjà existant") : Exit Sub

        Dim curType As TypeFile = AllNames.ItemValueA(AllNames.selected)
        curType.fileType = myName
        curType.saveData()

        AllNames.ItemText(AllNames.selected) = myName
        myMainWin.StatusText = "Types de fichiers : Type " & oldName & " renommé pour " & myName
        loadNames(myName)
    End Sub

    Private Sub deleting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Deleting.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce type ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim curType As TypeFile = AllNames.ItemValueA(AllNames.selected)
        curType.delete()

        myMainWin.StatusText = "Types de fichiers : Type " & AllNames.ItemText(AllNames.selected) & " supprimé"
        loadNames()
        lockItems(True, True)
    End Sub

    Private Sub addExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addExt.Click
        If NewExtension.Text = "" Then MessageBox.Show("Veuillez entrer une extension dans la boîte ci-contre", "Information manquante") : Exit Sub
        If DBExtensions.FindStringExact(NewExtension.Text) >= 0 Then MessageBox.Show("Veuillez entrer une extension différente de celles existantes", "Extension déjà existante") : Exit Sub

        DBExtensions.Items.Add(NewExtension.Text)
        formModified = True
    End Sub

    Private Sub dbExtensions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBExtensions.SelectedIndexChanged
        If Internal.Checked = False Or ConnectionsManager.currentUser = 0 Then
            If DBExtensions.SelectedIndex <> -1 Then
                DelExt.Enabled = True
            Else
                DelExt.Enabled = False
            End If
        End If
    End Sub

    Private Sub delExt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelExt.Click
        DBExtensions.Items.RemoveAt(DBExtensions.SelectedIndex)
        DelExt.Enabled = False
        formModified = True
    End Sub

    Private Sub dbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBType.SelectedIndexChanged
        If DBType.SelectedItem = "Document" Then
            DBSearchIn.Enabled = True
            DBPrintable.Enabled = True
        Else
            DBSearchIn.Checked = False
            DBSearchIn.Enabled = False
            DBPrintable.Checked = False
            DBPrintable.Enabled = False
        End If
        formModified = True
    End Sub

    Private Sub allNames_SelectedChange() Handles AllNames.selectedChange
        If AllNames.selected <> -1 Then
            lockItems(False, True)

            Dim curType As TypeFile = AllNames.ItemValueA(AllNames.selected)

            DBType.SelectedIndex = DBType.FindStringExact(curType.baseFileTypeName)
            If curType.isInternal Then
                Internal.Checked = True
                If currentUserName <> "Administrateur" Then
                    External.Enabled = False
                    Deleting.Enabled = False
                    Renaming.Enabled = False
                    addExt.Enabled = False
                    DelExt.Enabled = False
                    DBType.ReadOnly = True
                    DBPrintable.Enabled = False
                    DBSelectable.Enabled = False
                End If
            Else
                DBType.ReadOnly = False
                addExt.Enabled = True
                Deleting.Enabled = True
                Renaming.Enabled = True
                External.Checked = True
                External.Enabled = True
                DBPrintable.Enabled = False
                DBSelectable.Enabled = True
            End If

            If curType.extensions <> "" Then
                Dim sExt() As String = curType.extensions.Split(New Char() {";"})
                Dim myExt As String
                For Each myExt In sExt
                    DBExtensions.Items.Add(myExt)
                Next myExt
            End If

            DBHidden.Checked = curType.isHidden
            DBReadOnly.Checked = curType.isReadOnly
            DBSearchIn.Checked = curType.searchInContent
            DBPrintable.Checked = curType.printable
            DBSelectable.Checked = curType.dbSelectable

            If curType.nbItems > 0 Then Deleting.Enabled = False

            NbItems.Text = curType.nbItems
        Else
            lockItems(True, True)
            NbItems.Text = 0
        End If

        If Adding.Enabled = False Then lockItems(True)

        formModified = False
    End Sub

    Private Sub allNames_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles AllNames.willSelect
        If formModified = True And Adding.Enabled = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modifying_Click(AllNames, EventArgs.Empty)
        formModified = False
    End Sub

    Private Sub typeDB_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Adding.Enabled = True Then
            lockSecteur("TypeDB.modif", False)
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modifying_Click(AllNames, EventArgs.Empty)
        End If
    End Sub

    Private Sub typeDB_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If Adding.Enabled = True Then
            lockSecteur("TypeDB.modif", False)
            If formModified = True Then modifying_Click(AllNames, EventArgs.Empty)
        End If
    End Sub

    Private Sub allObject_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles External.CheckedChanged, DBReadOnly.CheckedChanged, DBHidden.CheckedChanged, DBSearchIn.CheckedChanged, DBPrintable.CheckedChanged, DBSelectable.CheckedChanged
        formModified = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf typeDB_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function <> "TypesFiles" Then Exit Sub

        loadNames(AllNames.ItemText(AllNames.selected))
    End Sub

End Class
