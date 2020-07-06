Friend Class AddModifDB
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        'HTMLEditBox
        HTMLEditBox.editorHeight = 267
        HTMLEditBox.editorWidth = 529
        HTMLEditBox.editorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")
        HTMLEditBox.htmlPageURL = emptyHTMLPath
        HTMLEditBox.Editing = True
        HTMLEditBox.ancreActif = True
        HTMLEditBox.ancre = PreferencesManager.getGeneralPreferences()("Ancre")
        HTMLEditBox.showPage()

        lockItems(True)

        Me.AddMotCle.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.submit.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.maximise.Image = DrawingManager.getInstance.getImage("FDehors.jpg")
        Me.printItem.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("print16.ico"), New Size(16, 16))

        AddHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DBNom As ManagedText
    Private WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents DBType As ManagedCombo
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblAllMotsCles As System.Windows.Forms.Label
    Friend WithEvents DBMotscles As System.Windows.Forms.ListBox
    Friend WithEvents AllMotsCles As System.Windows.Forms.ListBox
    Friend WithEvents DBHidden As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DBDescription As ManagedText
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents NewMotCle As ManagedText
    Friend WithEvents AddMotCle As System.Windows.Forms.Button
    Friend WithEvents submit As System.Windows.Forms.Button
    Friend WithEvents Importing As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents EditGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents EditBox As Clinica.TextControl
    Friend WithEvents HTMLEditBox As Clinica.WebTextControl
    Friend WithEvents FileAttributes As System.Windows.Forms.GroupBox
    Friend WithEvents maximise As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblCatSelected As System.Windows.Forms.Label
    Friend WithEvents printItem As System.Windows.Forms.Button
    Friend WithEvents DBReadOnly As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.DBType = New ManagedCombo
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblAllMotsCles = New System.Windows.Forms.Label
        Me.DBMotscles = New System.Windows.Forms.ListBox
        Me.AllMotsCles = New System.Windows.Forms.ListBox
        Me.DBHidden = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.DBDescription = New ManagedText
        Me.Label6 = New System.Windows.Forms.Label
        Me.AddMotCle = New System.Windows.Forms.Button
        Me.submit = New System.Windows.Forms.Button
        Me.Importing = New System.Windows.Forms.CheckBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.maximise = New System.Windows.Forms.Button
        Me.printItem = New System.Windows.Forms.Button
        Me.EditGroupBox = New System.Windows.Forms.GroupBox
        Me.HTMLEditBox = New Clinica.WebTextControl
        Me.EditBox = New Clinica.TextControl
        Me.DBReadOnly = New System.Windows.Forms.CheckBox
        Me.FileAttributes = New System.Windows.Forms.GroupBox
        Me.NewMotCle = New ManagedText
        Me.DBNom = New ManagedText
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblCatSelected = New System.Windows.Forms.Label
        Me.EditGroupBox.SuspendLayout()
        Me.FileAttributes.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Titre de l'item :"
        '
        'Label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(8, 48)
        Me.label2.Name = "Label2"
        Me.label2.Size = New System.Drawing.Size(201, 13)
        Me.label2.TabIndex = 2
        Me.label2.Text = "Type de fichier pour le contenu de l'item :"
        '
        'DBType
        '
        Me.DBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DBType.Location = New System.Drawing.Point(8, 64)
        Me.DBType.Name = "DBType"
        Me.DBType.Size = New System.Drawing.Size(352, 21)
        Me.DBType.Sorted = True
        Me.DBType.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Mots-clés de l'item :"
        '
        'lblAllMotsCles
        '
        Me.lblAllMotsCles.AutoSize = True
        Me.lblAllMotsCles.Location = New System.Drawing.Point(192, 88)
        Me.lblAllMotsCles.Name = "lblAllMotsCles"
        Me.lblAllMotsCles.Size = New System.Drawing.Size(102, 13)
        Me.lblAllMotsCles.TabIndex = 5
        Me.lblAllMotsCles.Text = "Mots-clés existants :"
        '
        'DBMotscles
        '
        Me.DBMotscles.Location = New System.Drawing.Point(8, 104)
        Me.DBMotscles.Name = "DBMotscles"
        Me.DBMotscles.ScrollAlwaysVisible = True
        Me.DBMotscles.Size = New System.Drawing.Size(168, 82)
        Me.DBMotscles.Sorted = True
        Me.DBMotscles.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.DBMotscles, "Double-cliquer pour transférer un mot-clé")
        '
        'AllMotsCles
        '
        Me.AllMotsCles.Location = New System.Drawing.Point(192, 104)
        Me.AllMotsCles.Name = "AllMotsCles"
        Me.AllMotsCles.ScrollAlwaysVisible = True
        Me.AllMotsCles.Size = New System.Drawing.Size(168, 82)
        Me.AllMotsCles.Sorted = True
        Me.AllMotsCles.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.AllMotsCles, "Double-cliquer pour transférer un mot-clé")
        '
        'DBHidden
        '
        Me.DBHidden.Location = New System.Drawing.Point(9, 16)
        Me.DBHidden.Name = "DBHidden"
        Me.DBHidden.Size = New System.Drawing.Size(96, 16)
        Me.DBHidden.TabIndex = 7
        Me.DBHidden.Text = "Fichier caché"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 235)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Description :"
        '
        'DBDescription
        '
        Me.DBDescription.Location = New System.Drawing.Point(8, 251)
        Me.DBDescription.Multiline = True
        Me.DBDescription.Name = "DBDescription"
        Me.DBDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.DBDescription.Size = New System.Drawing.Size(352, 64)
        Me.DBDescription.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 209)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Ajout d'un mot-clé :"
        '
        'AddMotCle
        '
        Me.AddMotCle.Enabled = False
        Me.AddMotCle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddMotCle.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddMotCle.Location = New System.Drawing.Point(336, 203)
        Me.AddMotCle.Name = "AddMotCle"
        Me.AddMotCle.Size = New System.Drawing.Size(24, 24)
        Me.AddMotCle.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.AddMotCle, "Ajouter un mot-clé")
        '
        'submit
        '
        Me.submit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.submit.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.submit.Location = New System.Drawing.Point(334, 321)
        Me.submit.Name = "submit"
        Me.submit.Size = New System.Drawing.Size(24, 24)
        Me.submit.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.submit, "Ajouter l'item")
        '
        'Importing
        '
        Me.Importing.Location = New System.Drawing.Point(227, 326)
        Me.Importing.Name = "Importing"
        Me.Importing.Size = New System.Drawing.Size(111, 16)
        Me.Importing.TabIndex = 9
        Me.Importing.Text = "Importer le fichier"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'maximise
        '
        Me.maximise.Enabled = False
        Me.maximise.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.maximise.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.maximise.Location = New System.Drawing.Point(334, 347)
        Me.maximise.Name = "maximise"
        Me.maximise.Size = New System.Drawing.Size(24, 24)
        Me.maximise.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.maximise, "Maximiser l'édition interne")
        '
        'printItem
        '
        Me.printItem.Enabled = False
        Me.printItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.printItem.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.printItem.Location = New System.Drawing.Point(308, 347)
        Me.printItem.Name = "printItem"
        Me.printItem.Size = New System.Drawing.Size(24, 24)
        Me.printItem.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.printItem, "Imprimer le contenu de l'item")
        '
        'EditGroupBox
        '
        Me.EditGroupBox.Controls.Add(Me.HTMLEditBox)
        Me.EditGroupBox.Controls.Add(Me.EditBox)
        Me.EditGroupBox.Dock = System.Windows.Forms.DockStyle.Right
        Me.EditGroupBox.Location = New System.Drawing.Point(379, 0)
        Me.EditGroupBox.Name = "EditGroupBox"
        Me.EditGroupBox.Size = New System.Drawing.Size(544, 378)
        Me.EditGroupBox.TabIndex = 18
        Me.EditGroupBox.TabStop = False
        Me.EditGroupBox.Text = "Édition interne"
        '
        'HTMLEditBox
        '
        Me.HTMLEditBox.activateLinksOnEdit = True
        Me.HTMLEditBox.allowNavigation = False
        Me.HTMLEditBox.allowRefresh = False
        Me.HTMLEditBox.ancre = Nothing
        Me.HTMLEditBox.ancreActif = False
        Me.HTMLEditBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HTMLEditBox.editorHeight = 350
        Me.HTMLEditBox.editorURL = ""
        Me.HTMLEditBox.editorWidth = 460
        Me.HTMLEditBox.htmlPageURL = Nothing
        Me.HTMLEditBox.Location = New System.Drawing.Point(3, 16)
        Me.HTMLEditBox.Name = "HTMLEditBox"
        Me.HTMLEditBox.Size = New System.Drawing.Size(538, 359)
        Me.HTMLEditBox.startupPos = 0
        Me.HTMLEditBox.TabIndex = 1
        Me.HTMLEditBox.toolBarStyles = 1
        Me.HTMLEditBox.Visible = False
        '
        'EditBox
        '
        Me.EditBox.ancre = Nothing
        Me.EditBox.ancreON = False
        Me.EditBox.ancreRemove = False
        Me.EditBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EditBox.Location = New System.Drawing.Point(3, 16)
        Me.EditBox.Name = "EditBox"
        Me.EditBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.EditBox.showImgMenu = True
        Me.EditBox.showMenu = True
        Me.EditBox.Size = New System.Drawing.Size(538, 359)
        Me.EditBox.TabIndex = 0
        Me.EditBox.tabSpacing = CType(0, Short)
        Me.EditBox.Text = ""
        '
        'DBReadOnly
        '
        Me.DBReadOnly.Location = New System.Drawing.Point(9, 32)
        Me.DBReadOnly.Name = "DBReadOnly"
        Me.DBReadOnly.Size = New System.Drawing.Size(143, 16)
        Me.DBReadOnly.TabIndex = 8
        Me.DBReadOnly.Text = "Fichier en lecture seule"
        '
        'FileAttributes
        '
        Me.FileAttributes.Controls.Add(Me.DBHidden)
        Me.FileAttributes.Controls.Add(Me.DBReadOnly)
        Me.FileAttributes.Location = New System.Drawing.Point(8, 318)
        Me.FileAttributes.Name = "FileAttributes"
        Me.FileAttributes.Size = New System.Drawing.Size(168, 53)
        Me.FileAttributes.TabIndex = 20
        Me.FileAttributes.TabStop = False
        Me.FileAttributes.Text = "Attributs de l'item"
        '
        'NewMotCle
        '
        Me.NewMotCle.acceptAlpha = True
        Me.NewMotCle.acceptedChars = ""
        Me.NewMotCle.acceptNumeric = True
        Me.NewMotCle.allCapital = False
        Me.NewMotCle.allLower = False
        Me.NewMotCle.cb_AcceptNegative = False
        Me.NewMotCle.currencyBox = False
        Me.NewMotCle.firstLetterCapital = True
        Me.NewMotCle.firstLettersCapital = False
        Me.NewMotCle.Location = New System.Drawing.Point(104, 206)
        Me.NewMotCle.matchExp = ""
        Me.NewMotCle.Name = "NewMotCle"
        Me.NewMotCle.nbDecimals = CType(-1, Short)
        Me.NewMotCle.onlyAlphabet = False
        Me.NewMotCle.refuseAccents = False
        Me.NewMotCle.refusedChars = "§"
        Me.NewMotCle.Size = New System.Drawing.Size(224, 20)
        Me.NewMotCle.TabIndex = 4
        Me.NewMotCle.trimText = False
        '
        'DBNom
        '
        Me.DBNom.acceptAlpha = True
        Me.DBNom.acceptedChars = ""
        Me.DBNom.acceptNumeric = True
        Me.DBNom.allCapital = False
        Me.DBNom.allLower = False
        Me.DBNom.cb_AcceptNegative = False
        Me.DBNom.currencyBox = False
        Me.DBNom.firstLetterCapital = True
        Me.DBNom.firstLettersCapital = False
        Me.DBNom.Location = New System.Drawing.Point(8, 25)
        Me.DBNom.matchExp = ""
        Me.DBNom.Name = "DBNom"
        Me.DBNom.nbDecimals = CType(-1, Short)
        Me.DBNom.onlyAlphabet = False
        Me.DBNom.refuseAccents = False
        Me.DBNom.refusedChars = "\§/§:§*§?§""§<§>§|§%"
        Me.DBNom.Size = New System.Drawing.Size(352, 20)
        Me.DBNom.TabIndex = 0
        Me.DBNom.trimText = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 190)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Mot-clé :"
        '
        'lblCatSelected
        '
        Me.lblCatSelected.AutoSize = True
        Me.lblCatSelected.Location = New System.Drawing.Point(53, 190)
        Me.lblCatSelected.Name = "lblCatSelected"
        Me.lblCatSelected.Size = New System.Drawing.Size(95, 13)
        Me.lblCatSelected.TabIndex = 11
        Me.lblCatSelected.Text = "Aucun sélectionné"
        '
        'AddModifDB
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(923, 378)
        Me.Controls.Add(Me.FileAttributes)
        Me.Controls.Add(Me.printItem)
        Me.Controls.Add(Me.maximise)
        Me.Controls.Add(Me.submit)
        Me.Controls.Add(Me.NewMotCle)
        Me.Controls.Add(Me.DBDescription)
        Me.Controls.Add(Me.DBNom)
        Me.Controls.Add(Me.AllMotsCles)
        Me.Controls.Add(Me.DBMotscles)
        Me.Controls.Add(Me.DBType)
        Me.Controls.Add(Me.EditGroupBox)
        Me.Controls.Add(Me.Importing)
        Me.Controls.Add(Me.AddMotCle)
        Me.Controls.Add(Me.lblCatSelected)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblAllMotsCles)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.KeyPreview = True
        Me.Name = "AddModifDB"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ajout dans la banque de données"
        Me.EditGroupBox.ResumeLayout(False)
        Me.FileAttributes.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _AllowModification As Boolean = True
    Private formModified As Boolean = False
    Private loadInternalExtension As String = ""
    Private curInternalExtension As String = ""
    Private _CurDBFolder As InternalDBFolder
    Private _dbItem As New InternalDBItem()

#Region "Propriétés"
    Public Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal Value As Boolean)
            _AllowModification = Value
            lockItems(Not Value)
        End Set
    End Property

    Public Property curDBFolder() As InternalDBFolder
        Get
            Return _CurDBFolder
        End Get
        Set(ByVal Value As InternalDBFolder)
            _CurDBFolder = Value
        End Set
    End Property

    Public ReadOnly Property curNoDBItem() As Integer
        Get
            Return _dbItem.noDBItem
        End Get
    End Property
#End Region

    Private Function submitting() As String
        Dim myMotsCles() As String
        ReDim myMotsCles(DBMotscles.Items.Count - 1)
        DBMotscles.Items.CopyTo(myMotsCles, 0)

        Dim MyReturn, myCaption As String
        If _dbItem.noDBItem = 0 Then
            myCaption = "Ajout de l'item impossible"
            MyReturn = InternalDBManager.getInstance.addItem(DBNom.Text, curDBFolder, CType(DBType.SelectedItem, TypeFile).fileType, Importing.Checked, myMotsCles, DBDescription.Text.Split(New Char() {vbCrLf}), DBHidden.Checked, DBReadOnly.Checked)
        Else
            myCaption = "Modification de l'item impossible"
            MyReturn = InternalDBManager.getInstance.modifItem(_dbItem.noDBItem, DBNom.Text, curDBFolder, CType(DBType.SelectedItem, TypeFile).fileType, Importing.Checked, myMotsCles, DBDescription.Text.Split(New Char() {vbCrLf}), DBHidden.Checked, DBReadOnly.Checked)
        End If
        RemoveHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
        If MyReturn <> "" Then
            If MyReturn.IndexOf("entrer un titre") <> -1 Then
                MessageBox.Show(MyReturn, "Information manquante")
                DBNom.Focus()
                Exit Function
            ElseIf MyReturn.IndexOf("déjà") <> -1 Then
                MessageBox.Show(MyReturn, "Titre déjà existant")
                DBNom.Focus()
                DBNom.SelectionStart = 0
                DBNom.SelectionLength = DBNom.Text.Length
                Exit Function
            Else
                MessageBox.Show(MyReturn, myCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return ""
            End If
        End If

        formModified = False
        myMainWin.StatusText = "Banque de données : Item enregistré"
        Return "DONE"
    End Function

    Private Sub internalFileSaving(ByVal type As String, ByVal filePath As String)
        If type = "RTF" Then
            EditBox.SaveFile(filePath)
        ElseIf type = "HTML" Or type = "HTM" Or type = "HTMLRPT" Then
            writeFile(filePath, New String() {HTMLEditBox.getHTML}, , , , False)
        Else
            EditBox.SaveFile(filePath, RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        DBNom.ReadOnly = trueFalse
        If Me.Text.StartsWith("Ajout") = False Then DBNom.ReadOnly = True

        DBType.ReadOnly = trueFalse
        'DBMotscles.Enabled = Not TrueFalse 'To leave possibility to select cat
        If _AllowModification = False Then
            DBMotscles.Width = (AllMotsCles.Width + AllMotsCles.Left) - DBMotscles.Left
            DBMotscles.BringToFront()
            lblAllMotsCles.Visible = False
        End If
        AllMotsCles.Enabled = Not trueFalse
        NewMotCle.ReadOnly = trueFalse
        DBDescription.ReadOnly = trueFalse
        Importing.Enabled = Not trueFalse
        DBHidden.Enabled = Not trueFalse
        DBReadOnly.Enabled = Not trueFalse
        EditBox.ReadOnly = trueFalse
        If trueFalse = False Then EditBox.BackColor = Color.White

        If DBType.Text <> "Rapport" Then HTMLEditBox.Editing = Not trueFalse
        submit.Enabled = Not trueFalse
        dbType_SelectedIndexChanged(Me, EventArgs.Empty)
    End Sub

    Public Sub loading(ByVal noDBItem As Integer)
        loading(New InternalDBItem(noDBItem))
    End Sub

    Public Sub loading(ByVal path As String, ByVal name As String)
        loading(New InternalDBItem(path, name))
    End Sub

    Private Sub loadTypes(ByVal showUnselectable As Boolean)
        Dim oldIndex As Integer = DBType.SelectedIndex
        If oldIndex = -1 Then oldIndex = 0
        DBType.Items.Clear()

        Dim types() As TypeFile
        ReDim types(TypesFilesManager.getInstance.getItemables().Count - 1)
        TypesFilesManager.getInstance.getItemables().CopyTo(types)
        For Each curType As TypeFile In types
            If showUnselectable OrElse curType.dbSelectable Then DBType.Items.Add(curType)
        Next
        If DBType.Items.Count > 0 Then DBType.SelectedIndex = oldIndex
    End Sub

    Public Sub loading(Optional ByVal dbItem As InternalDBItem = Nothing)
        Dim i, inAll As Integer

        AllMotsCles.Items.Clear()
        Dim myMotsCles() As String = DBLinker.getInstance.readOneDBField("DBMotsCles", "MotCle")
        If myMotsCles IsNot Nothing AndAlso myMotsCles.Length <> 0 Then AllMotsCles.Items.AddRange(myMotsCles)

        If dbItem Is Nothing Then
            loadTypes(True)
            _dbItem.noDBFolder = curDBFolder.noDBFolder
            Me.Text = "Ajout dans la banque de données"
            Me.submit.Image = Me.AddMotCle.Image

            formModified = False
            Exit Sub
        End If

        Dim oldText As String = ""
        If _dbItem.noDBItem <> 0 And Me.Text.StartsWith("Banque de données :") Then oldText = Me.Text

        _dbItem = dbItem
        _CurDBFolder = _dbItem.getDBFolder()

        'MODIFICATION de la fenêtre
        Dim newText As String = "Modification de la banque de données"
        If allowModification = False Then newText = "Banque de données : " & _dbItem.getDBFolder.toString & "\" & _dbItem.dbItem
        If oldText <> "" AndAlso oldText <> newText Then updateText(Me, newText)
        If Me.Text <> newText Then Me.Text = newText
        ToolTip1.SetToolTip(submit, "Enregistrer l'item")

        DBNom.ReadOnly = True
        Importing.Checked = False

        'Chargement
        DBNom.Text = _dbItem.dbItem

        Dim curType As TypeFile = _dbItem.getTypeFile
        If curType.dbSelectable Then
            loadTypes(False)
            DBType.SelectedIndex = DBType.FindStringExact(curType.fileType)
        Else
            Me.DBType.Items.Add(curType)
            DBType.SelectedIndex = 0
        End If

        If curType.isInternal Then
            Dim dbItemFile As String = appPath & bar(appPath) & "DB\" & _dbItem.dbItemFile
            If IO.File.Exists(dbItemFile) = False Then
                MessageBox.Show("Item : " & _dbItem.getDBFolder.toString() & "\" & _dbItem.dbItem & vbCrLf & "Le contenu de l'item ne peut être ouvert, car le fichier lié n'existe pas. Veuillez importer un fichier à l'item.", "Impossible d'ouvrir l'item")
            Else
                Dim sFile() As String = _dbItem.dbItemFile.Split(New Char() {"."})
                curInternalExtension = sFile(sFile.GetUpperBound(0))
                loadInternalExtension = sFile(sFile.GetUpperBound(0))
                If curInternalExtension.ToUpper = "RTF" Then
                    EditBox.Visible = True
                    HTMLEditBox.Visible = False
                    EditBox.loadFile(dbItemFile, RichTextBoxStreamType.RichText)
                ElseIf curInternalExtension.ToUpper = "HTML" Or curInternalExtension.ToUpper = "HTM" Then
                    HTMLEditBox.Visible = True
                    EditBox.Visible = False
                    HTMLEditBox.htmlPageURL = dbItemFile
                    HTMLEditBox.showPage(IO.File.ReadAllText(dbItemFile))
                ElseIf curInternalExtension.ToUpper = "HTMLRPT" Then
                    HTMLEditBox.Visible = True
                    EditBox.Visible = False
                    HTMLEditBox.htmlPageURL = dbItemFile
                    HTMLEditBox.showPage()
                ElseIf curInternalExtension <> "" Then
                    'REM Some files are UnicodePlainText, how could we detect which one to use
                    EditBox.Visible = True
                    HTMLEditBox.Visible = False
                    EditBox.loadFile(dbItemFile, RichTextBoxStreamType.PlainText)
                    If EditBox.Text.StartsWith("ÿþ") Then EditBox.loadFile(dbItemFile, RichTextBoxStreamType.UnicodePlainText)
                End If
            End If

            EditBox.Modified = False
        End If

        DBReadOnly.Checked = _dbItem.isReadOnly
        DBHidden.Checked = _dbItem.isHidden

        If _dbItem.keywords IsNot Nothing Then
            DBMotscles.Items.Clear()
            For i = 0 To _dbItem.keywords.Length - 1
                DBMotscles.Items.Add(_dbItem.keywords(i))
                inAll = AllMotsCles.FindStringExact(_dbItem.keywords(i))
                If inAll >= 0 Then AllMotsCles.Items.RemoveAt(inAll)
            Next i
        End If

        DBDescription.Text = _dbItem.description

        formModified = False
    End Sub

    Private Sub addMotCle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddMotCle.Click
        If DBMotscles.FindStringExact(NewMotCle.Text) >= 0 Then MessageBox.Show("Le mot-clé entré a déjà été sélectionné", "Ajout d'un mot-clé") : Exit Sub

        Dim inAll As Integer = AllMotsCles.FindStringExact(NewMotCle.Text)
        If inAll >= 0 Then AllMotsCles.Items.RemoveAt(inAll)

        DBMotscles.Items.Add(NewMotCle.Text)
        NewMotCle.Text = ""
        formModified = True
    End Sub

    Private Sub dbMotscles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DBMotscles.Click
        If DBMotscles.SelectedIndex = -1 Then Return
        lblCatSelected.Text = DBMotscles.Items(DBMotscles.SelectedIndex).ToString
    End Sub

    Private Sub dbMotscles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBMotscles.SelectedIndexChanged
        If DBMotscles.SelectedIndex = -1 Then
            lblCatSelected.Text = ""
        Else
            lblCatSelected.Text = DBMotscles.Items(DBMotscles.SelectedIndex).ToString
        End If
    End Sub

    Private Sub dbMotscles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DBMotscles.DoubleClick
        If AllMotsCles.Enabled = False Then Exit Sub
        If DBMotscles.SelectedItem = "" Then Exit Sub
        AllMotsCles.Items.Add(DBMotscles.SelectedItem)
        DBMotscles.Items.RemoveAt(DBMotscles.SelectedIndex)
        formModified = True
    End Sub

    Private Sub allMotsCles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AllMotsCles.Click
        If AllMotsCles.SelectedIndex = -1 Then
            lblCatSelected.Text = ""
        Else
            lblCatSelected.Text = AllMotsCles.Items(AllMotsCles.SelectedIndex).ToString
        End If
    End Sub


    Private Sub allMotsCles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AllMotsCles.SelectedIndexChanged
        If AllMotsCles.SelectedIndex < 0 Then Exit Sub

        lblCatSelected.Text = AllMotsCles.Items(AllMotsCles.SelectedIndex).ToString
    End Sub

    Private Sub allMotsCles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AllMotsCles.DoubleClick
        If AllMotsCles.SelectedItem = "" Then Exit Sub
        DBMotscles.Items.Add(AllMotsCles.SelectedItem)
        AllMotsCles.Items.RemoveAt(AllMotsCles.SelectedIndex)
        formModified = True
    End Sub

    Private Sub dbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBType.SelectedIndexChanged
        printItem.Enabled = False

        Dim curType As TypeFile = DBType.SelectedItem
        If curType Is Nothing Then Exit Sub
        Dim htmlText As String = ""
        If HTMLEditBox.Visible = True Then htmlText = HTMLEditBox.getText

        HTMLEditBox.viewDisableHtmlFields = False
        If curType.extensions.IndexOf("RTF") <> -1 Then
            curInternalExtension = "RTF"
            EditBox.Visible = True
            HTMLEditBox.Visible = False
            EditBox.showMenu = True
            If htmlText <> "" Then EditBox.Text = htmlText
        ElseIf curType.extensions.IndexOf("HTMLRPT") <> -1 Then
            EditBox.Visible = False
            HTMLEditBox.Editing = False
            HTMLEditBox.viewDisableHtmlFields = True
            If curInternalExtension <> "" And HTMLEditBox.Visible = False Then HTMLEditBox.sethtml(EditBox.Text)
            HTMLEditBox.Visible = True
            If HTMLEditBox.isPageLoaded Then printItem.Enabled = True
        ElseIf curType.extensions.IndexOf("HTML") <> -1 Or curType.extensions.IndexOf("HTM") <> -1 Then
            EditBox.Visible = False
            HTMLEditBox.Editing = Not EditBox.ReadOnly
            If curInternalExtension <> "" And HTMLEditBox.Visible = False Then HTMLEditBox.sethtml(EditBox.Text)
            HTMLEditBox.Visible = True
            If loadInternalExtension = "HTML" Or loadInternalExtension = "HTM" Then
                curInternalExtension = loadInternalExtension
            Else
                curInternalExtension = "HTML"
            End If
            If HTMLEditBox.isPageLoaded Then printItem.Enabled = True
        Else
            Dim curText As String = EditBox.Text
            EditBox.ResetText()
            EditBox.Visible = True
            HTMLEditBox.Visible = False
            EditBox.Text = curText
            curInternalExtension = "TXT"
            If htmlText <> "" Then EditBox.Text = htmlText
            EditBox.showMenu = False
        End If

        If allowModification Then Importing.Enabled = True : Importing.Checked = False
        If curType.isInternal And curType.baseFileType = TypeFile.baseFileTypeEnum.Document Then
            Me.Width = 929
            EditGroupBox.Visible = True
            If HTMLEditBox.isPageLoaded Then Me.maximise.Enabled = True
        Else
            Me.maximise.Enabled = False
            EditGroupBox.Visible = False
            If Me.Text.StartsWith("Aj") Then Importing.Enabled = False : Importing.Checked = True
            Me.Width = 376
        End If

        If _dbItem.noDBItem = 0 Then
            DBHidden.Checked = curType.isHidden
            DBReadOnly.Checked = curType.isReadOnly
        End If

        formModified = True
    End Sub

    Private Sub submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles submit.Click
        If submitting() <> "" Then Me.Close()
    End Sub

    Private Sub newMotCle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewMotCle.TextChanged
        If NewMotCle.Text <> "" Then
            AddMotCle.Enabled = True
        Else
            AddMotCle.Enabled = False
        End If
    End Sub

    Private Sub addModifDB_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If allowModification = True Then
            If _dbItem.noDBItem <> 0 Then lockSecteur("DBItem-" & _dbItem.noDBItem, False)
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If submitting() = "" Then e.Cancel = True
        End If
    End Sub

    Private Sub all_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBDescription.TextChanged, DBNom.TextChanged, EditBox.TextChanged
        formModified = True
    End Sub

    Private Sub all_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBHidden.CheckedChanged, DBReadOnly.CheckedChanged
        formModified = True
    End Sub

    Private Sub addModifDB_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RemoveHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
    End Sub

    Private Sub addModifDB_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub maximise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maximise.Click
        Dim texteBoxTitle As String = "Banque de données : " & DBType.Text & ":" & DBNom.Text
        Dim mySel As String = "0"

        Dim contenu As String = ""

        If EditBox.Visible = True Then
            If curInternalExtension = "RTF" Then
                contenu = EditBox.Rtf
                TextWindow.getInstance.texteType = RichTextBoxStreamType.RichText
            Else
                contenu = EditBox.Text
                TextWindow.getInstance.texteType = RichTextBoxStreamType.PlainText
            End If
            mySel = EditBox.SelectionStart
        Else
            contenu = HTMLEditBox.getHTML()
            mySel = HTMLEditBox.getPos()
        End If

        TextWindow.getInstance.currentData = contenu
        TextWindow.getInstance.isLocked = Not HTMLEditBox.Editing
        TextWindow.getInstance.isHTML = Not EditBox.Visible
        TextWindow.getInstance.Text = "Modification d'un item : " & texteBoxTitle

        mySel = TextWindow.getInstance.ShowTexteModif(mySel)

        'Recopie la modification du fichier temporaire à la boîte de texte
        If (HTMLEditBox.Editing = True Or EditBox.ReadOnly = False) And contenu <> TextWindow.getInstance.currentData Then
            If HTMLEditBox.Visible = True Then
                HTMLEditBox.sethtml(TextWindow.getInstance.currentData)
            Else
                If curInternalExtension = "RTF" Then
                    EditBox.Rtf = TextWindow.getInstance.currentData
                Else
                    EditBox.Text = TextWindow.getInstance.currentData
                End If
            End If
            formModified = True
        End If

        'Set position back
        If HTMLEditBox.Visible = True Then
            HTMLEditBox.setPos(mySel)
            HTMLEditBox.focus()
        Else
            EditBox.Focus()
            EditBox.SelectionStart = mySel
        End If
    End Sub

    Private Sub lblCatSelected_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCatSelected.TextChanged
        ToolTip1.SetToolTip(lblCatSelected, lblCatSelected.Text)
    End Sub

    Private Sub htmlEditBox_PageLoaded() Handles HTMLEditBox.pageLoaded
        ''Ajout
        If _dbItem.noDBItem = 0 Then
            lockItems(False)
            EditBox.BackColor = Color.White
            'Droit & Accès
            If (currentDroitAcces(4) = False And Me.curDBFolder.noUser = 0) Or (currentDroitAcces(96) = False And Me.curDBFolder.noUser <> ConnectionsManager.currentUser And Me.curDBFolder.noUser <> 0) Then DBReadOnly.Enabled = False
            If currentDroitAcces(2) = False Then DBHidden.Enabled = False

            formModified = False
            Exit Sub
        End If

        ''Modif
        'Droit & Accès
        If allowModification = True AndAlso ((currentDroitAcces(4) = False And Me.curDBFolder.noUser = 0) Or (currentDroitAcces(96) = False And Me.curDBFolder.noUser <> ConnectionsManager.currentUser And Me.curDBFolder.noUser <> 0)) Then
            If DBReadOnly.Checked = True Then
                MessageBox.Show("Vous n'avez pas le droit de modifier cet item, car il est en lecture seule", "Droit & accès")
                allowModification = False
            Else
                DBReadOnly.Enabled = False
            End If
        End If
        If currentDroitAcces(2) = False Then DBHidden.Enabled = False

        'Vérification si l'item est en cours de modification
        If allowModification = True AndAlso lockSecteur("DBItem-" & _dbItem.noDBItem, True, "Modification de l'item : " & _dbItem.dbItem, False) = False Then
            Me.allowModification = False
            If PreferencesManager.getUserPreferences()("AffMSGModif") = True Then MessageBox.Show("Item en cours de modification." & vbCrLf & "Item en visualisation seulement.", "Impossible de modifier cet item")
        End If

        If allowModification = True Then lockItems(False)
        If HTMLEditBox.Visible = True Then
            Dim html As String = ""
            If IO.File.Exists(appPath & bar(appPath) & "DB\" & _dbItem.dbItemFile) Then
                html = IO.File.ReadAllText(appPath & bar(appPath) & "DB\" & _dbItem.dbItemFile)
            End If
            HTMLEditBox.sethtml(html)
        End If

        Dim curType As TypeFile = DBType.SelectedItem
        If curType.isInternal Then
            maximise.Enabled = True
            printItem.Enabled = curType.printable
        End If

        formModified = False
    End Sub

    Private Sub htmlEditBox_TextChanged(ByVal theText As String) Handles HTMLEditBox.textChanged
        If allowModification = False Then Exit Sub

        formModified = True
    End Sub

    Private Sub printItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles printItem.Click
        PrintingHelper.printHtml(HTMLEditBox.getHTML, Me.Text, True, True)
    End Sub

    Private Sub allMotsCles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AllMotsCles.KeyDown
        If currentDroitAcces(99) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de supprimer les mots-clés existant." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete AndAlso MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce mot-clé ?", "Suppresion d'un mot-clé", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim deleted As Boolean = False
            If DBLinker.getInstance.delDB("DBMotsCles", "MotCle", "'" & AllMotsCles.SelectedItem.ToString.Replace("'", "''") & "' COLLATE French_CI_AS", False, , False) = False Then
                Dim errorMsg As String = "Impossible de supprimer ce mot-clé, car il est déjà en cours d'utilisation par d'autres items."
                If NewMotCle.Text <> "" Or DBMotscles.SelectedIndex <> -1 Then
                    Dim firstChoice As String = NewMotCle.Text
                    Dim secondChoice As String = ""
                    If firstChoice = "" Then
                        firstChoice = DBMotscles.SelectedItem.ToString
                    Else
                        If DBMotscles.SelectedIndex <> -1 Then secondChoice = " ou par """ & DBMotscles.SelectedItem.ToString & """"
                    End If
                    Dim myMsgBox As New MsgBox1()
                    Dim answer As Byte = myMsgBox(errorMsg & vbCrLf & "Souhaitez-vous le remplacer (""" & AllMotsCles.SelectedItem.ToString & """) par """ & firstChoice & """" & secondChoice & " ?", "Remplacement d'un mot-clé", IIf(secondChoice <> "", 3, 2), "Remplacer par """ & firstChoice & """", IIf(secondChoice <> "", "Remplacer par """ & DBMotscles.Text & """", "Annuler"), "Annuler")
                    If answer = 1 Or (answer = 2 And secondChoice <> "") Then
                        Dim curChoice As String = ""
                        If answer = 1 Then curChoice = firstChoice
                        If answer = 2 Then curChoice = DBMotscles.Text
                        Dim curNos(,) As String = DBLinker.getInstance.readDB("DBMotsCles", "NoMotCle,Motcle", "WHERE MotCle='" & AllMotsCles.SelectedItem.ToString.Replace("'", "''") & "' OR MotCle='" & curChoice.Replace("'", "''") & "' COLLATE French_CI_AS")
                        If curNos.GetUpperBound(1) = 0 Then
                            DBHelper.addItemToADBList("DBMotsCles", "MotCle", curChoice, "NoMotCle")
                            curNos = DBLinker.getInstance.readDB("DBMotsCles", "NoMotCle,Motcle", "WHERE MotCle='" & AllMotsCles.SelectedItem.ToString.Replace("'", "''") & "' OR MotCle='" & curChoice.Replace("'", "''") & "' COLLATE French_CI_AS")
                        End If
                        If curNos(1, 0) = curChoice Then
                            'Switch curChoice to second position to ensure that (0,0) = NoMotCle de AllMotsCles
                            curNos(1, 0) = curNos(0, 1)
                            curNos(0, 1) = curNos(0, 0)
                            curNos(0, 0) = curNos(1, 0)
                            curNos(1, 0) = curNos(1, 1)
                            curNos(1, 1) = curChoice
                        End If

                        Dim noMotCle As Integer = curNos(0, 0)
                        DBLinker.getInstance.updateDB("DBItemsMotsCles", "NoMotCle=" & curNos(0, 1), "NoMotCle", noMotCle, False)
                        DBLinker.getInstance.delDB("DBMotsCles", "NoMotCle", curNos(0, 0), False)
                        deleted = True
                    End If
                Else
                    MessageBox.Show(errorMsg, "Suppression impossible")
                End If
            Else
                deleted = True
            End If

            'Update list & DBItems in DB
            If deleted Then
                AllMotsCles.Items.Clear()
                AllMotsCles.Items.AddRange(DBLinker.getInstance.readOneDBField("DBMotsCles", "MotCle", , True))

                InternalUpdatesManager.getInstance.sendUpdate("DBItems()")
            End If
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf submit_Click)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function = "DBItem" Then
            If curNoDBItem = dataReceived.params(0) Then loading(curNoDBItem)
        End If
    End Sub
End Class
