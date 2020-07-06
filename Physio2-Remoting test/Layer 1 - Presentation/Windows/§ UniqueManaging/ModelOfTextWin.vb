Option Strict Off
Option Explicit On

Friend Class ModelOfTextWin
    Inherits SingleWindow

    Private types As New Generic.List(Of RadioButton)

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
        Me.types.Add(Me._types_0)
        Me.types.add(Me._types_1)

        With modele
            .Text = ""
            .editorHeight = 316
            .editorWidth = 528
            .editorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")
            .htmlPageURL = emptyHTMLPath
            .ancreActif = True
            .ancre = PreferencesManager.getGeneralPreferences()("Ancre")
        End With

        'Chargement des images
        With DrawingManager.getInstance
            Me.enlever.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.save.Image = .getImage("save.jpg")
            Me.deplacer.Image = DrawingManager.iconToImage(.getIcon("deplacer16.ico"), New Size(16, 16))
            Me.emptybox.Image = .getImage("eraser.jpg")
            Me.saveas.Image = DrawingManager.iconToImage(.getIcon("saveas.ico"), New Size(16, 16))
            Me.maximise.Image = .getImage("FDehors.jpg")
            Me.Icon = DrawingManager.imageToIcon(.getImage("modeles16.gif"))
        End With
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            RemoveHandler modele.pageLoaded, AddressOf pageLoaded
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents enlever As System.Windows.Forms.Button
    Public WithEvents deplacer As System.Windows.Forms.Button
    Public WithEvents emptybox As System.Windows.Forms.Button
    Public WithEvents saveas As System.Windows.Forms.Button
    Public WithEvents save As System.Windows.Forms.Button
    Public WithEvents nommodele As System.Windows.Forms.ComboBox
    Public WithEvents _types_1 As System.Windows.Forms.RadioButton
    Public WithEvents _types_0 As System.Windows.Forms.RadioButton
    Public WithEvents frameType As System.Windows.Forms.GroupBox
    Public WithEvents categorie As System.Windows.Forms.ComboBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents modele As WebTextControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents maximise As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.enlever = New System.Windows.Forms.Button
        Me.deplacer = New System.Windows.Forms.Button
        Me.emptybox = New System.Windows.Forms.Button
        Me.saveas = New System.Windows.Forms.Button
        Me.save = New System.Windows.Forms.Button
        Me.nommodele = New System.Windows.Forms.ComboBox
        Me.frameType = New System.Windows.Forms.GroupBox
        Me._types_1 = New System.Windows.Forms.RadioButton
        Me._types_0 = New System.Windows.Forms.RadioButton
        Me.categorie = New System.Windows.Forms.ComboBox
        Me.modele = New Clinica.WebTextControl
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.maximise = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.frameType.SuspendLayout()
        Me.SuspendLayout()
        '
        'enlever
        '
        Me.enlever.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.enlever.BackColor = System.Drawing.SystemColors.Control
        Me.enlever.Cursor = System.Windows.Forms.Cursors.Default
        Me.enlever.Enabled = False
        Me.enlever.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.enlever.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.enlever.ForeColor = System.Drawing.SystemColors.ControlText
        Me.enlever.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.enlever.Location = New System.Drawing.Point(520, 0)
        Me.enlever.Name = "enlever"
        Me.enlever.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.enlever.Size = New System.Drawing.Size(24, 24)
        Me.enlever.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.enlever, "Enlever")
        Me.enlever.UseVisualStyleBackColor = False
        '
        'deplacer
        '
        Me.deplacer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.deplacer.BackColor = System.Drawing.SystemColors.Control
        Me.deplacer.Cursor = System.Windows.Forms.Cursors.Default
        Me.deplacer.Enabled = False
        Me.deplacer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.deplacer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.deplacer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.deplacer.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.deplacer.Location = New System.Drawing.Point(496, 0)
        Me.deplacer.Name = "deplacer"
        Me.deplacer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.deplacer.Size = New System.Drawing.Size(24, 24)
        Me.deplacer.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.deplacer, "Déplacer")
        Me.deplacer.UseVisualStyleBackColor = False
        '
        'emptybox
        '
        Me.emptybox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.emptybox.BackColor = System.Drawing.SystemColors.Control
        Me.emptybox.Cursor = System.Windows.Forms.Cursors.Default
        Me.emptybox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.emptybox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.emptybox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.emptybox.Location = New System.Drawing.Point(472, 0)
        Me.emptybox.Name = "emptybox"
        Me.emptybox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.emptybox.Size = New System.Drawing.Size(24, 24)
        Me.emptybox.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.emptybox, "Vider")
        Me.emptybox.UseVisualStyleBackColor = False
        '
        'saveas
        '
        Me.saveas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.saveas.BackColor = System.Drawing.SystemColors.Control
        Me.saveas.Cursor = System.Windows.Forms.Cursors.Default
        Me.saveas.Enabled = False
        Me.saveas.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.saveas.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveas.ForeColor = System.Drawing.SystemColors.ControlText
        Me.saveas.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.saveas.Location = New System.Drawing.Point(424, 0)
        Me.saveas.Name = "saveas"
        Me.saveas.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.saveas.Size = New System.Drawing.Size(24, 24)
        Me.saveas.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.saveas, "Enregistrer sous...")
        Me.saveas.UseVisualStyleBackColor = False
        '
        'save
        '
        Me.save.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.save.BackColor = System.Drawing.SystemColors.Control
        Me.save.Cursor = System.Windows.Forms.Cursors.Default
        Me.save.Enabled = False
        Me.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.save.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save.ForeColor = System.Drawing.SystemColors.ControlText
        Me.save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.save.Location = New System.Drawing.Point(400, 0)
        Me.save.Name = "save"
        Me.save.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.save.Size = New System.Drawing.Size(24, 24)
        Me.save.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.save, "Enregistrer")
        Me.save.UseVisualStyleBackColor = False
        '
        'nommodele
        '
        Me.nommodele.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nommodele.BackColor = System.Drawing.SystemColors.Window
        Me.nommodele.Cursor = System.Windows.Forms.Cursors.Default
        Me.nommodele.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.nommodele.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nommodele.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nommodele.Location = New System.Drawing.Point(264, 24)
        Me.nommodele.Name = "nommodele"
        Me.nommodele.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nommodele.Size = New System.Drawing.Size(280, 22)
        Me.nommodele.Sorted = True
        Me.nommodele.TabIndex = 5
        '
        'FrameType
        '
        Me.frameType.BackColor = System.Drawing.SystemColors.Control
        Me.frameType.Controls.Add(Me._types_1)
        Me.frameType.Controls.Add(Me._types_0)
        Me.frameType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameType.Location = New System.Drawing.Point(8, 0)
        Me.frameType.Name = "FrameType"
        Me.frameType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameType.Size = New System.Drawing.Size(177, 41)
        Me.frameType.TabIndex = 1
        Me.frameType.TabStop = False
        Me.frameType.Text = "Type"
        '
        '_types_1
        '
        Me._types_1.Appearance = System.Windows.Forms.Appearance.Button
        Me._types_1.BackColor = System.Drawing.SystemColors.Control
        Me._types_1.Checked = True
        Me._types_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._types_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._types_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._types_1.Location = New System.Drawing.Point(88, 16)
        Me._types_1.Name = "_types_1"
        Me._types_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._types_1.Size = New System.Drawing.Size(81, 17)
        Me._types_1.TabIndex = 3
        Me._types_1.TabStop = True
        Me._types_1.Text = "Utilisateurs"
        Me._types_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._types_1.UseVisualStyleBackColor = False
        '
        '_types_0
        '
        Me._types_0.Appearance = System.Windows.Forms.Appearance.Button
        Me._types_0.BackColor = System.Drawing.SystemColors.Control
        Me._types_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._types_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._types_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._types_0.Location = New System.Drawing.Point(8, 16)
        Me._types_0.Name = "_types_0"
        Me._types_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._types_0.Size = New System.Drawing.Size(81, 17)
        Me._types_0.TabIndex = 2
        Me._types_0.TabStop = True
        Me._types_0.Text = "Généraux"
        Me._types_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._types_0.UseVisualStyleBackColor = False
        '
        'categorie
        '
        Me.categorie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.categorie.BackColor = System.Drawing.SystemColors.Window
        Me.categorie.Cursor = System.Windows.Forms.Cursors.Default
        Me.categorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.categorie.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.categorie.ForeColor = System.Drawing.SystemColors.WindowText
        Me.categorie.Location = New System.Drawing.Point(264, 0)
        Me.categorie.Name = "categorie"
        Me.categorie.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.categorie.Size = New System.Drawing.Size(128, 22)
        Me.categorie.Sorted = True
        Me.categorie.TabIndex = 0
        '
        'modele
        '
        Me.modele.activateLinksOnEdit = True
        Me.modele.allowContextMenu = True
        Me.modele.allowEditorContextMenu = True
        Me.modele.allowNavigation = False
        Me.modele.allowRefresh = False
        Me.modele.allowUndo = True
        Me.modele.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.modele.ancre = Nothing
        Me.modele.ancreActif = False
        Me.modele.editorContextMenu = Nothing
        Me.modele.editorHeight = 350
        Me.modele.editorURL = ""
        Me.modele.editorWidth = 460
        Me.modele.htmlPageURL = Nothing
        Me.modele.Location = New System.Drawing.Point(8, 48)
        Me.modele.Name = "modele"
        Me.modele.Size = New System.Drawing.Size(536, 408)
        Me.modele.startupPos = 0
        Me.modele.TabIndex = 12
        Me.modele.toolBarStyles = 1
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'maximise
        '
        Me.maximise.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.maximise.BackColor = System.Drawing.SystemColors.Control
        Me.maximise.Cursor = System.Windows.Forms.Cursors.Default
        Me.maximise.Enabled = False
        Me.maximise.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.maximise.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maximise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.maximise.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.maximise.Location = New System.Drawing.Point(448, 0)
        Me.maximise.Name = "maximise"
        Me.maximise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.maximise.Size = New System.Drawing.Size(24, 24)
        Me.maximise.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.maximise, "Maximiser")
        Me.maximise.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(200, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Catégorie :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(200, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 14)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Modèle :"
        '
        'modelenote
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(554, 464)
        Me.Controls.Add(Me.maximise)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.modele)
        Me.Controls.Add(Me.deplacer)
        Me.Controls.Add(Me.emptybox)
        Me.Controls.Add(Me.saveas)
        Me.Controls.Add(Me.save)
        Me.Controls.Add(Me.nommodele)
        Me.Controls.Add(Me.frameType)
        Me.Controls.Add(Me.categorie)
        Me.Controls.Add(Me.enlever)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(562, 498)
        Me.Name = "modelenote"
        Me.Text = "Modèles de texte"
        Me.frameType.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private formModified As Boolean = False
    Private oldModele As String
    Private oldCategorie As Integer = -1
    Private oldType As Boolean = True
    Private _AllowModification As Boolean = True

#Region "Propriétés"
    Public Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal Value As Boolean)
            _AllowModification = Value
            lockItems(Not Value, True)
            modele.Editing(True) = Value
        End Set
    End Property
#End Region

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal saveItems As Boolean = False, Optional ByVal listItems As Boolean = False)
        deplacer.Enabled = Not trueFalse
        enlever.Enabled = Not trueFalse
        maximise.Enabled = Not trueFalse
        If saveItems = True Then
            save.Enabled = Not trueFalse
            saveas.Enabled = Not trueFalse
        End If
        If listItems Then
            emptybox.Enabled = Not trueFalse
            _types_0.Enabled = Not trueFalse
            _types_1.Enabled = Not trueFalse
            categorie.Enabled = Not trueFalse
            nommodele.Enabled = Not trueFalse
        End If
    End Sub

    Private Sub categorie_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles categorie.SelectedIndexChanged
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then save_Click(categorie, eventArgs.Empty)

        oldCategorie = categorie.SelectedIndex
        loadModele()
    End Sub

    Private Sub deplacer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles deplacer.Click
        Dim MSGReturn, Cat2, curNomModele As String, Choices As String = ""
        Dim i As Integer
        For i = 0 To categorie.Items.Count - 1
            If Not categorie.GetItemText(categorie.Items.Item(i)) = categorie.GetItemText(categorie.SelectedItem) Then Choices &= "§" & categorie.GetItemText(categorie.Items.Item(i))
        Next i

        Choices = Choices.Substring(1)
        Dim myMultiChoice As New multichoice()
        MSGReturn = myMultiChoice.GetChoice("Sélectionner la catégorie", Choices, , "§")

        If MSGReturn = "ERROR" Then Exit Sub

        Cat2 = MSGReturn
        Dim newNoCat As Integer
        For Each curCat As ModelCategory In categorie.Items
            If curCat.category = Cat2 Then
                newNoCat = curCat.noCategory
                Exit For
            End If
        Next

        curNomModele = nommodele.Text

        Dim exists() As String = DBLinker.getInstance().readOneDBField("Modeles", "NoModele", "WHERE NoCategorie=" & newNoCat & " AND Nom='" & nommodele.Text.Replace("'", "''") & "' AND NoUser" & IIf(types(0).Checked = True, " is null", "=" & ConnectionsManager.currentUser))
        If exists IsNot Nothing AndAlso exists.Length <> 0 AndAlso MessageBox.Show("Un modèle du même nom existe déjà dans cette catégorie." & vbCrLf & "Voulez-vous le remplacer ?", "Modèle existant", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        DBLinker.getInstance.updateDB("Modeles", "NoCategorie=" & newNoCat, "NoCategorie", CType(categorie.SelectedItem, ModelCategory).noCategory & " AND Nom='" & nommodele.Text.Replace("'", "''") & "' AND NoUser" & IIf(types(0).Checked = True, " IS NULL", "=" & ConnectionsManager.currentUser), False)

        categorie.SelectedIndex = categorie.FindStringExact(Cat2)
        loadModele()
        nommodele.SelectedIndex = nommodele.FindStringExact(curNomModele)
    End Sub

    Private Sub emptybox_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles emptybox.Click
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then save_Click(emptybox, eventArgs.Empty)

        modele.htmlPageURL = emptyHTMLPath
        Try
            modele.sethtml("")
        Catch
            'Page not finish loading
        End Try
        formModified = False
        lockItems(True)
        oldModele = ""
        nommodele.SelectedIndex = -1
    End Sub

    Private Sub enlever_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles enlever.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir enlever ce modèle ?", "Suppression de modèle", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        DBLinker.getInstance.delDB("Modeles", "Nom", "'" & nommodele.Text.Replace("'", "''") & "' AND NoCategorie=" & CType(categorie.SelectedItem, ModelCategory).noCategory & " AND NoUser" & IIf(types(0).Checked = True, " IS NULL", "=" & ConnectionsManager.currentUser), False)

        oldModele = ""
        loadModele()
    End Sub

    Private Sub modelenote_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Ajout ds la barre d'outils
        lockItems(True, True, True)

        types(1).Checked = True
        Dim modelesCats As Generic.List(Of ModelCategory) = ModelsManager.getInstance.getModelsCategories(Nothing)
        categorie.Items.AddRange(modelesCats.ToArray)
        categorie.SelectedIndex = 0
        emptybox_Click(emptybox, New System.EventArgs())

        If types(0).checked = True Then
            oldType = True
        Else
            oldType = False
        End If

        loadModele()

        AddHandler modele.pageLoaded, AddressOf pageLoaded
        modele.Editing = allowModification

        formModified = False
    End Sub

    Private Sub pageLoaded()
        lockItems(False, True, True)
        lockItems(True)
    End Sub

    Public Sub loadModele(Optional ByVal setNullText As Boolean = True)
        If types Is Nothing Then Exit Sub

        Dim curModel As String = nommodele.Text

        modele.htmlPageURL = emptyHTMLPath
        If setNullText Then
            Try
                modele.sethtml("")
            Catch
                'Page not loaded
            End Try
        End If

        lockItems(True)
        formModified = False
        If currentUserName = "Administrateur" And types(1).Checked = True Then types(0).Checked = True : Exit Sub

        nommodele.Items.Clear()
        Dim modeles() As String = DBLinker.getInstance().readOneDBField("Modeles", "Nom", "WHERE NoCategorie=" & CType(categorie.SelectedItem, ModelCategory).noCategory & " AND NoUser" & IIf(types(0).Checked = True, " is null", "=" & ConnectionsManager.currentUser).ToString)
        If modeles IsNot Nothing AndAlso modeles.Length <> 0 Then nommodele.Items.AddRange(modeles)

        If setNullText Then
            oldModele = ""
            formModified = False
        Else
            nommodele.Text = curModel
            loadModeleHTML()
        End If
    End Sub

    Private Sub nommodele_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles nommodele.SelectedIndexChanged
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then save_Click(nommodele, eventArgs.Empty)

        If nommodele.SelectedIndex < 0 Then lockItems(True) : Exit Sub
        If nommodele.GetItemText(nommodele.SelectedItem) = "" Then lockItems(True) : Exit Sub

        If allowModification = True Then
            lockItems(False, True)
        Else
            maximise.Enabled = True
        End If

        loadModeleHTML()
        modele.focus()
        formModified = False
        oldModele = nommodele.Text
    End Sub

    Private Sub loadModeleHTML()
        Dim contenu() As String = DBLinker.getInstance.readOneDBField("Modeles", "Modele", "WHERE NoCategorie=" & CType(categorie.SelectedItem, ModelCategory).noCategory & " AND Nom='" & nommodele.Text.Replace("'", "''") & "' AND NoUser" & IIf(types(0).Checked = True, " is null", "=" & ConnectionsManager.currentUser))
        Dim html As String = ""
        If contenu.Length <> 0 Then html = contenu(0)
        modele.setHtml(html)
    End Sub

    Private Sub save_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles save.Click
        saving()
    End Sub

    Private Function saving() As String
        If oldModele = "" Then Return savingAs()

        DBLinker.getInstance().updateDB("Modeles", "Modele='" & modele.getHTML().Replace("'", "''") & "'", "Nom", "'" & oldModele.Replace("'", "''") & "' AND NoCategorie=" & CType(categorie.Items(oldCategorie), ModelCategory).noCategory & " AND NoUser" & IIf(types(0).Checked = True, " is null", "=" & ConnectionsManager.currentUser), False)

        myMainWin.StatusText = "Modèles : " & oldModele & " enregistré"
        formModified = False
        Return "DONE"
    End Function

    Private Sub saveas_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles saveas.Click
        savingAs()
    End Sub

    Private Function savingAs() As String
        Dim Cat, MPath, nom, DNom, msgReturn As String, Choices As String = ""
        Dim i As Integer

        'Choix de la catégorie
        For i = 0 To categorie.Items.Count - 1
            Choices &= "§" & categorie.GetItemText(categorie.Items.Item(i))
        Next i

        Choices = Choices.Substring(1)
        Dim myMultiChoice As New multichoice()
        msgReturn = myMultiChoice.GetChoice("Sélectionner la catégorie", Choices, , "§", , categorie.Text)

        If msgReturn.StartsWith("ERROR") Then Exit Function

        MPath = appPath & bar(appPath)
        Cat = msgReturn
        Dim newNoCat As Integer
        For Each curCat As ModelCategory In categorie.Items
            If curCat.category = Cat Then
                newNoCat = curCat.noCategory
                Exit For
            End If
        Next

        'Choix du nouveau nom
        Dim myInputBoxPlus As InputBoxPlus
        DNom = oldModele
StartBack:
        myInputBoxPlus = New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = "\§/§:§*§?§""§<§>§|§%§,"
        nom = myInputBoxPlus.Prompt("Veuillez entrer un nouveau nom pour ce modèle", "Nom du modèle", DNom)
        If nom = "" Then Exit Function

        Dim exists() As String = DBLinker.getInstance().readOneDBField("Modeles", "NoModele", "WHERE NoCategorie=" & newNoCat & " AND Nom='" & nom.Replace("'", "''") & "' AND NoUser" & IIf(types(0).Checked = True, " is null", "=" & ConnectionsManager.currentUser))
        Dim replacing As Boolean = False

        If exists IsNot Nothing AndAlso exists.Length <> 0 Then
            Dim myMsgBox As New MsgBox1()
            msgReturn = myMsgBox.Message("Le nom saisi entre en conflit avec ceux déjà existants." & vbCrLf & "Que voulez-vous faire ?", "Nom déjà utilisé", 3, "Changer de nom", "Remplacer celui existant", "Annuler")
            If msgReturn = 3 Then Exit Function 'Annuler
            If msgReturn = 1 Then DNom = nom : GoTo StartBack 'Changer de nom
            replacing = True
        End If

        If replacing Then
            Dim noModele As Integer = exists(0)
            DBLinker.getInstance().updateDB("Modeles", "Modele='" & modele.getHTML().Replace("'", "''") & "'", "NoModele", noModele, False)
        Else
            DBLinker.getInstance.writeDB("Modeles", "NoUser,NoCategorie,Nom,Modele", IIf(types(0).Checked = True, "null", ConnectionsManager.currentUser) & "," & newNoCat & ",'" & nom.Replace("'", "''") & "','" & modele.getHTML().Replace("'", "''") & "'")
        End If
        If types(0).Checked Then InternalUpdatesManager.getInstance.sendUpdate("ModelOfText(" & newNoCat & ")")

        categorie.SelectedIndex = categorie.FindStringExact(Cat)
        loadModele()
        nommodele.SelectedIndex = nommodele.FindStringExact(nom)

        If oldModele = "" Then
            myMainWin.StatusText = "Modèles : Nouveau modèle enregistré sous " & nom
        Else
            myMainWin.StatusText = "Modèles : " & oldModele & " enregistré sous " & nom
        End If

        formModified = False

        Return "DONE"
    End Function

    Private Sub modelenote_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If allowModification = True Then
            lockSecteur("modelenote.modif", False, "Modèles de texte")
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If saving() = "" Then e.Cancel = True
        End If
    End Sub

    Private Sub modelenote_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If allowModification = True Then
            lockSecteur("modelenote.modif", False, "Modèles de texte")
            If formModified = True Then saving()
        End If
    End Sub

    Private Sub maximise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maximise.Click
        Dim contenu As String = modele.getHTML()

        TextWindow.getInstance.currentData = contenu
        TextWindow.getInstance.Text = "Visualisation : Modèles de texte - " & oldModele
        TextWindow.getInstance.isLocked = Not modele.Editing
        TextWindow.getInstance.isHTML = True
        Dim mySel As String = TextWindow.getInstance.ShowTexteModif(modele.getPos)

        If modele.Editing = True And contenu <> TextWindow.getInstance.currentData Then
            modele.sethtml(TextWindow.getInstance.currentData)
            formModified = True
        End If

        modele.focus()
        modele.setPos(mySel)
    End Sub

    Private Sub _types_0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _types_0.CheckedChanged
        'Droit & Accès
        If currentDroitAcces(31) = False And _types_0.Checked = True Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier les modèles de texte généraux." & vbCrLf & "Merci!", "Droit & Accès")
            _types_1.Checked = True
        End If

        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then save_Click(types(0), EventArgs.Empty)
        formModified = False

        Dim setNullText As Boolean = True
        modele.htmlPageURL = emptyHTMLPath

        Try
            If types(0).Checked = True Then
                If lockSecteur("modelenote.modif", True, "Modèles de texte") = False Then
                    If allowModification = True Then allowModification = False : setNullText = False
                Else
                    If allowModification = False Then allowModification = True : setNullText = False
                End If
                oldType = True
            Else
                If allowModification = True Then lockSecteur("modelenote.modif", False, "Modèles de texte")

                If allowModification = False Then allowModification = True : setNullText = False
                oldType = False
            End If
        Catch
        End Try

        loadModele(setNullText)
    End Sub

    Private Shadows Sub modele_TextChanged(ByVal theText As String) Handles modele.textChanged
        If allowModification = False Then Exit Sub

        formModified = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf modelenote_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.fromExternal = False OrElse dataReceived.function <> "ModelOfText" Then Exit Sub
        If dataReceived.params(0) <> CType(categorie.SelectedItem, ModelCategory).noCategory Then Exit Sub

        loadModele(False)
    End Sub
End Class
