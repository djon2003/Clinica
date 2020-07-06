Imports CI.Clinica.Accounts.Clients.Folders.Codifications
Imports CI.Clinica.Accounts.Clients.Folders.RVs

Friend Class QueueList
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin

        imgModifSave = New ImageList
        Try
            With imgModifSave.Images
                .Add(DrawingManager.getInstance.getImage("modifier16.gif"))
                .Add(DrawingManager.getInstance.getImage("save.jpg"))
            End With
        Catch
        End Try

        ColorWithRV.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorWithRV"))
        ColorWithoutRV.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorWithoutRV"))

        Me.Ajout.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.Enlever.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.Selectionner.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.Icon = DrawingManager.getInstance.getIcon("QL24.ico")
        Me.Modifier.Image = imgModifSave.Images(0)
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Remarques As System.Windows.Forms.TextBox
    Friend WithEvents DateAppel As System.Windows.Forms.Label
    Friend WithEvents DateRV As System.Windows.Forms.Label
    Friend WithEvents Periode As System.Windows.Forms.Label
    Friend WithEvents TRP As System.Windows.Forms.Label
    Friend WithEvents listeQueueList As CI.Controls.List
    Friend WithEvents Filtrage As System.Windows.Forms.ComboBox
    Friend WithEvents Ajout As System.Windows.Forms.Button
    Friend WithEvents Enlever As System.Windows.Forms.Button
    Friend WithEvents Selectionner As System.Windows.Forms.Button
    Friend WithEvents Modifier As System.Windows.Forms.Button
    Friend WithEvents DispoBtn As System.Windows.Forms.Button
    Friend WithEvents ville As System.Windows.Forms.TextBox
    Friend WithEvents adresse As System.Windows.Forms.TextBox
    Friend WithEvents nom As System.Windows.Forms.TextBox
    Friend WithEvents codedossier As System.Windows.Forms.TextBox
    Friend WithEvents nam As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Diagnostic As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Telephones As System.Windows.Forms.ComboBox
    Friend WithEvents GroupInfoClient As System.Windows.Forms.GroupBox
    Friend WithEvents ColorWithRV As System.Windows.Forms.Label
    Friend WithEvents ColorWithoutRV As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents FCode As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Remarques = New System.Windows.Forms.TextBox
        Me.ville = New System.Windows.Forms.TextBox
        Me.adresse = New System.Windows.Forms.TextBox
        Me.nom = New System.Windows.Forms.TextBox
        Me.codedossier = New System.Windows.Forms.TextBox
        Me.nam = New System.Windows.Forms.TextBox
        Me.DateAppel = New System.Windows.Forms.Label
        Me.DateRV = New System.Windows.Forms.Label
        Me.Periode = New System.Windows.Forms.Label
        Me.TRP = New System.Windows.Forms.Label
        Me.listeQueueList = New CI.Controls.List
        Me.Filtrage = New System.Windows.Forms.ComboBox
        Me.Ajout = New System.Windows.Forms.Button
        Me.Enlever = New System.Windows.Forms.Button
        Me.Selectionner = New System.Windows.Forms.Button
        Me.Modifier = New System.Windows.Forms.Button
        Me.DispoBtn = New System.Windows.Forms.Button
        Me.Label16 = New System.Windows.Forms.Label
        Me.Diagnostic = New System.Windows.Forms.TextBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Telephones = New System.Windows.Forms.ComboBox
        Me.GroupInfoClient = New System.Windows.Forms.GroupBox
        Me.ColorWithRV = New System.Windows.Forms.Label
        Me.ColorWithoutRV = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.FCode = New System.Windows.Forms.ComboBox
        Me.GroupInfoClient.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(352, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date d'appel :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(216, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Rendez-vous :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nom,Prénom :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Téléphones :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(216, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Code du dossier :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(216, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(113, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Période du traitement :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 120)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Remarques :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(149, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Numéro d'assurance maladie :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Adresse :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Ville :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(216, 64)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Thérapeute :"
        '
        'Remarques
        '
        Me.Remarques.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Remarques.Location = New System.Drawing.Point(8, 136)
        Me.Remarques.Multiline = True
        Me.Remarques.Name = "Remarques"
        Me.Remarques.ReadOnly = True
        Me.Remarques.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.Remarques.Size = New System.Drawing.Size(464, 88)
        Me.Remarques.TabIndex = 13
        '
        'ville
        '
        Me.ville.BackColor = System.Drawing.SystemColors.Control
        Me.ville.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ville.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ville.Location = New System.Drawing.Point(40, 48)
        Me.ville.Name = "ville"
        Me.ville.Size = New System.Drawing.Size(168, 13)
        Me.ville.TabIndex = 17
        '
        'adresse
        '
        Me.adresse.BackColor = System.Drawing.SystemColors.Control
        Me.adresse.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.adresse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adresse.Location = New System.Drawing.Point(64, 32)
        Me.adresse.Name = "adresse"
        Me.adresse.Size = New System.Drawing.Size(144, 13)
        Me.adresse.TabIndex = 18
        '
        'nom
        '
        Me.nom.BackColor = System.Drawing.SystemColors.Control
        Me.nom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.nom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.nom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nom.ForeColor = System.Drawing.Color.Blue
        Me.nom.Location = New System.Drawing.Point(80, 16)
        Me.nom.Name = "nom"
        Me.nom.Size = New System.Drawing.Size(128, 13)
        Me.nom.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.nom, "Cliquer pour accéder au compte du client sélectionné")
        '
        'codedossier
        '
        Me.codedossier.BackColor = System.Drawing.SystemColors.Control
        Me.codedossier.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.codedossier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codedossier.Location = New System.Drawing.Point(304, 32)
        Me.codedossier.Name = "codedossier"
        Me.codedossier.Size = New System.Drawing.Size(168, 13)
        Me.codedossier.TabIndex = 20
        '
        'nam
        '
        Me.nam.BackColor = System.Drawing.SystemColors.Control
        Me.nam.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.nam.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nam.Location = New System.Drawing.Point(160, 104)
        Me.nam.Name = "nam"
        Me.nam.Size = New System.Drawing.Size(192, 13)
        Me.nam.TabIndex = 21
        '
        'DateAppel
        '
        Me.DateAppel.AutoSize = True
        Me.DateAppel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateAppel.Location = New System.Drawing.Point(416, 16)
        Me.DateAppel.Name = "DateAppel"
        Me.DateAppel.Size = New System.Drawing.Size(0, 13)
        Me.DateAppel.TabIndex = 22
        '
        'DateRV
        '
        Me.DateRV.AutoSize = True
        Me.DateRV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateRV.Location = New System.Drawing.Point(288, 16)
        Me.DateRV.Name = "DateRV"
        Me.DateRV.Size = New System.Drawing.Size(0, 13)
        Me.DateRV.TabIndex = 23
        '
        'Periode
        '
        Me.Periode.AutoSize = True
        Me.Periode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Periode.Location = New System.Drawing.Point(328, 48)
        Me.Periode.Name = "Periode"
        Me.Periode.Size = New System.Drawing.Size(0, 13)
        Me.Periode.TabIndex = 24
        '
        'TRP
        '
        Me.TRP.AutoSize = True
        Me.TRP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TRP.Location = New System.Drawing.Point(288, 64)
        Me.TRP.Name = "TRP"
        Me.TRP.Size = New System.Drawing.Size(0, 13)
        Me.TRP.TabIndex = 25
        '
        'listeQueueList
        '
        Me.listeQueueList.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.listeQueueList.autoAdjust = True
        Me.listeQueueList.autoKeyDownSelection = True
        Me.listeQueueList.autoSizeHorizontally = False
        Me.listeQueueList.autoSizeVertically = False
        Me.listeQueueList.BackColor = System.Drawing.Color.White
        Me.listeQueueList.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.listeQueueList.baseBackColor = System.Drawing.Color.White
        Me.listeQueueList.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.listeQueueList.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.listeQueueList.bgColor = System.Drawing.Color.White
        Me.listeQueueList.borderColor = System.Drawing.Color.Empty
        Me.listeQueueList.borderSelColor = System.Drawing.Color.Empty
        Me.listeQueueList.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.listeQueueList.CausesValidation = False
        Me.listeQueueList.clickEnabled = True
        Me.listeQueueList.do3D = False
        Me.listeQueueList.draw = False
        Me.listeQueueList.hScrollColor = System.Drawing.SystemColors.Control
        Me.listeQueueList.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listeQueueList.hScrolling = False
        Me.listeQueueList.hsValue = CType(0, Short)
        Me.listeQueueList.itemBorder = CType(0, Short)
        Me.listeQueueList.Location = New System.Drawing.Point(8, 248)
        Me.listeQueueList.itemMargin = CType(0, Short)
        Me.listeQueueList.mouseMove3D = False
        Me.listeQueueList.mouseSpeed = 0
        Me.listeQueueList.Name = "listeQueueList"
        Me.listeQueueList.objMaxHeight = 0.0!
        Me.listeQueueList.objMaxWidth = 0.0!
        Me.listeQueueList.objMinHeight = 0.0!
        Me.listeQueueList.objMinWidth = 0.0!
        Me.listeQueueList.reverseSorting = False
        Me.listeQueueList.selected = CType(-1, Short)
        Me.listeQueueList.selectedClickAllowed = False
        Me.listeQueueList.Size = New System.Drawing.Size(296, 136)
        Me.listeQueueList.sorted = False
        Me.listeQueueList.TabIndex = 26
        Me.listeQueueList.toolTipText = Nothing
        Me.listeQueueList.vScrollColor = System.Drawing.SystemColors.Control
        Me.listeQueueList.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listeQueueList.vScrolling = True
        Me.listeQueueList.vsValue = CType(0, Short)
        '
        'Filtrage
        '
        Me.Filtrage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Filtrage.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Filtrage.Location = New System.Drawing.Point(8, 16)
        Me.Filtrage.Name = "Filtrage"
        Me.Filtrage.Size = New System.Drawing.Size(160, 24)
        Me.Filtrage.Sorted = True
        Me.Filtrage.TabIndex = 29
        '
        'Ajout
        '
        Me.Ajout.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Ajout.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Ajout.Location = New System.Drawing.Point(312, 360)
        Me.Ajout.Name = "Ajout"
        Me.Ajout.Size = New System.Drawing.Size(24, 24)
        Me.Ajout.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.Ajout, "Ajout d'un compte client sur la liste d'attente")
        '
        'Enlever
        '
        Me.Enlever.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Enlever.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Enlever.Location = New System.Drawing.Point(408, 360)
        Me.Enlever.Name = "Enlever"
        Me.Enlever.Size = New System.Drawing.Size(24, 24)
        Me.Enlever.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.Enlever, "Enlever")
        '
        'Selectionner
        '
        Me.Selectionner.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Selectionner.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Selectionner.Location = New System.Drawing.Point(464, 360)
        Me.Selectionner.Name = "Selectionner"
        Me.Selectionner.Size = New System.Drawing.Size(24, 24)
        Me.Selectionner.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.Selectionner, "Sélectionner pour l'inscription à l'agenda")
        '
        'Modifier
        '
        Me.Modifier.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Modifier.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Modifier.Location = New System.Drawing.Point(368, 360)
        Me.Modifier.Name = "Modifier"
        Me.Modifier.Size = New System.Drawing.Size(24, 24)
        Me.Modifier.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.Modifier, "Modifier")
        '
        'DispoBtn
        '
        Me.DispoBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DispoBtn.Location = New System.Drawing.Point(376, 104)
        Me.DispoBtn.Name = "DispoBtn"
        Me.DispoBtn.Size = New System.Drawing.Size(96, 24)
        Me.DispoBtn.TabIndex = 35
        Me.DispoBtn.Text = "Disponibilités"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(216, 80)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 13)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "Diagnostic :"
        '
        'Diagnostic
        '
        Me.Diagnostic.BackColor = System.Drawing.SystemColors.Control
        Me.Diagnostic.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Diagnostic.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Diagnostic.Location = New System.Drawing.Point(280, 80)
        Me.Diagnostic.Name = "Diagnostic"
        Me.Diagnostic.Size = New System.Drawing.Size(192, 13)
        Me.Diagnostic.TabIndex = 37
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'Telephones
        '
        Me.Telephones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Telephones.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Telephones.Location = New System.Drawing.Point(8, 80)
        Me.Telephones.Name = "Telephones"
        Me.Telephones.Size = New System.Drawing.Size(200, 21)
        Me.Telephones.TabIndex = 38
        '
        'GroupInfoClient
        '
        Me.GroupInfoClient.Controls.Add(Me.Remarques)
        Me.GroupInfoClient.Controls.Add(Me.DateAppel)
        Me.GroupInfoClient.Controls.Add(Me.ville)
        Me.GroupInfoClient.Controls.Add(Me.Label9)
        Me.GroupInfoClient.Controls.Add(Me.adresse)
        Me.GroupInfoClient.Controls.Add(Me.Telephones)
        Me.GroupInfoClient.Controls.Add(Me.nom)
        Me.GroupInfoClient.Controls.Add(Me.codedossier)
        Me.GroupInfoClient.Controls.Add(Me.DispoBtn)
        Me.GroupInfoClient.Controls.Add(Me.nam)
        Me.GroupInfoClient.Controls.Add(Me.DateRV)
        Me.GroupInfoClient.Controls.Add(Me.Periode)
        Me.GroupInfoClient.Controls.Add(Me.Diagnostic)
        Me.GroupInfoClient.Controls.Add(Me.TRP)
        Me.GroupInfoClient.Controls.Add(Me.Label1)
        Me.GroupInfoClient.Controls.Add(Me.Label2)
        Me.GroupInfoClient.Controls.Add(Me.Label3)
        Me.GroupInfoClient.Controls.Add(Me.Label11)
        Me.GroupInfoClient.Controls.Add(Me.Label12)
        Me.GroupInfoClient.Controls.Add(Me.Label4)
        Me.GroupInfoClient.Controls.Add(Me.Label10)
        Me.GroupInfoClient.Controls.Add(Me.Label7)
        Me.GroupInfoClient.Controls.Add(Me.Label8)
        Me.GroupInfoClient.Controls.Add(Me.Label13)
        Me.GroupInfoClient.Controls.Add(Me.Label16)
        Me.GroupInfoClient.Font = New System.Drawing.Font("Times New Roman", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupInfoClient.Location = New System.Drawing.Point(8, 8)
        Me.GroupInfoClient.Name = "GroupInfoClient"
        Me.GroupInfoClient.Size = New System.Drawing.Size(480, 232)
        Me.GroupInfoClient.TabIndex = 39
        Me.GroupInfoClient.TabStop = False
        Me.GroupInfoClient.Text = "Informations sur le client sélectionné de la liste d'attente"
        '
        'ColorWithRV
        '
        Me.ColorWithRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ColorWithRV.Location = New System.Drawing.Point(312, 248)
        Me.ColorWithRV.Name = "ColorWithRV"
        Me.ColorWithRV.Size = New System.Drawing.Size(80, 32)
        Me.ColorWithRV.TabIndex = 40
        Me.ColorWithRV.Text = "Déplacer le rendez-vous"
        Me.ColorWithRV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ColorWithoutRV
        '
        Me.ColorWithoutRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ColorWithoutRV.Location = New System.Drawing.Point(400, 248)
        Me.ColorWithoutRV.Name = "ColorWithoutRV"
        Me.ColorWithoutRV.Size = New System.Drawing.Size(80, 32)
        Me.ColorWithoutRV.TabIndex = 41
        Me.ColorWithoutRV.Text = "Nouveau rendez-vous"
        Me.ColorWithoutRV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.FCode)
        Me.GroupBox1.Controls.Add(Me.Filtrage)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(312, 280)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(176, 72)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtres"
        '
        'FCode
        '
        Me.FCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FCode.Location = New System.Drawing.Point(8, 40)
        Me.FCode.Name = "FCode"
        Me.FCode.Size = New System.Drawing.Size(160, 24)
        Me.FCode.Sorted = True
        Me.FCode.TabIndex = 30
        '
        'QueueList
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(498, 392)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ColorWithoutRV)
        Me.Controls.Add(Me.ColorWithRV)
        Me.Controls.Add(Me.GroupInfoClient)
        Me.Controls.Add(Me.Modifier)
        Me.Controls.Add(Me.Selectionner)
        Me.Controls.Add(Me.Enlever)
        Me.Controls.Add(Me.Ajout)
        Me.Controls.Add(Me.listeQueueList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "QueueList"
        Me.ShowInTaskbar = False
        Me.Text = "Liste d'attente"
        Me.GroupInfoClient.ResumeLayout(False)
        Me.GroupInfoClient.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private isLoading As Boolean = False
    Private imgModifSave As ImageList
    Private curDispo As String
    Private _UseWinAsSelection As Boolean = False
    Private _WeekDayNo As Byte
    Private LastPath, _Filtering(), _SelectedTRP As String
    Private _SelectedTime, _FirstFreeTime, _LastFreeTime, _VisiteDate As Date
    Private formModified As Boolean = False
    Private _AllowModification As Boolean = False
    Private _AddingClient, _Therapeute As Integer

#Region "Propriétés"
    Public Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal Value As Boolean)
            _AllowModification = Value
            lockItems(Not Value)
            Filtrage.Enabled = Not Value
            FCode.Enabled = Not Value
        End Set
    End Property

    Public Property selectedTime() As Date
        Get
            Return _SelectedTime
        End Get
        Set(ByVal Value As Date)
            _SelectedTime = Value
        End Set
    End Property

    Public Property weekDayNo() As Byte
        Get
            Return _WeekDayNo
        End Get
        Set(ByVal Value As Byte)
            _WeekDayNo = Value
        End Set
    End Property

    Public Property visiteDate() As Date
        Get
            Return _VisiteDate
        End Get
        Set(ByVal Value As Date)
            _VisiteDate = Value
        End Set
    End Property

    Public Property therapeute() As Integer
        Get
            Return _Therapeute
        End Get
        Set(ByVal Value As Integer)
            _Therapeute = Value
        End Set
    End Property

    Public Property firstFreeTime() As Date
        Get
            Return _FirstFreeTime
        End Get
        Set(ByVal Value As Date)
            _FirstFreeTime = Value
        End Set
    End Property

    Public Property lastFreeTime() As Date
        Get
            Return _LastFreeTime
        End Get
        Set(ByVal Value As Date)
            _LastFreeTime = Value
        End Set
    End Property

    Public Property filtering() As String()
        Get
            Return _Filtering
        End Get
        Set(ByVal Value As String())
            _Filtering = Value
        End Set
    End Property

    Public Property addingClient() As Integer
        Get
            Return _AddingClient
        End Get
        Set(ByVal Value As Integer)
            _AddingClient = Value
        End Set
    End Property

    Public Property useWinAsSelection() As Boolean
        Get
            Return _UseWinAsSelection
        End Get
        Set(ByVal Value As Boolean)
            _UseWinAsSelection = Value
            loading()
        End Set
    End Property
#End Region

    Public Sub reLoad()
        loading(listeQueueList.selected)
    End Sub

    Private Sub queueList_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If allowModification = True Then
            removeUsed()

            If formModified = True Then
                If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    modifier_Click(listeQueueList, EventArgs.Empty)
                Else
                    allowModification = False
                End If
            End If
        End If
    End Sub

    Private Sub queueList_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If allowModification = True Then
            removeUsed()

            If formModified = True Then
                modifier_Click(listeQueueList, EventArgs.Empty)
            End If
        End If
    End Sub

    Private Sub queueList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        configList(listeQueueList)
        lockItems(True)
        loading()
    End Sub

    Private Sub ajout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ajout.Click
        'Droit & Accès
        If currentDroitAcces(19) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier la liste d'attente." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim MyPeriodeList, myService As String, myServices As String = ""

        Me.addingClient = 0
        Dim myRecherche As New clientSearch()
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.Visible = False
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()

        If addingClient = 0 Then GoTo fin
        If listeQueueList.findString("¶" & addingClient & "¶0¶0¶", False, CI.Controls.List.FindingType.ValueA) >= 0 Then MessageBox.Show("Le client est déjà dans la liste d'attente sans rendez-vous", "Impossible d'ajouter") : Exit Sub

        Dim myTRP As User = UsersManager.getInstance.chooseUser(True, True, , , , False)
        If myTRP Is Nothing Then GoTo fin

        Dim myMultiChoice As New multichoice()
        If myTRP.noUser <> 0 Then
            myServices = myTRP.services
        Else
            If PreferencesManager.getGeneralPreferences()("Services") <> "" Then myServices = PreferencesManager.getGeneralPreferences()("Services").Replace(vbTab, "§")
        End If
        myService = myMultiChoice.GetChoice("Veuillez sélectionner le service", myServices, , "§")
        If myService.ToUpper.StartsWith("ERROR") Then Exit Sub

        MyPeriodeList = "Traitement,Évaluation,15 minutes,30 minutes,45 minutes,1 heure,1h15min,1h30min,1h45min,2 heures"
        Dim periodes() As String = {"Traitement", "Évaluation", "15", "30", "45", "60", "75", "90", "105", "120"}
        myMultiChoice = New multichoice()
        Dim myPeriode As String = myMultiChoice.GetChoice("Veuillez sélectionner la période désirée", MyPeriodeList, "INDEX", ",", False)
        If myPeriode < 0 Then GoTo fin

        Dim selectedCode As Accounts.Clients.Folders.Codifications.FolderCode = codifications.chooseCode(myTRP.noUser, Date.Today)
        If selectedCode Is Nothing Then GoTo fin

        Dim myDisponibilites As New Disponibilities()
        Dim myDispo As String = myDisponibilites.GetDisponisibilites
        If myDispo = "" Then GoTo fin

        If DBLinker.getInstance.writeDB("ListeAttente", "NoFolder,NoClient, NoTRP, DateAppel, Disponibilites, Periode, NoCodeUnique, Service, NoVisite,NoCodeUser", "null," & addingClient & "," & myTRP.noUser & ",'" & Date.Today.Year & "/" & Date.Today.Month & "/" & Date.Today.Day & "','" & myDispo & "','" & periodes(myPeriode) & "'," & selectedCode.noUnique & ",'" & myService.Replace("'", "''") & "', null," & If(selectedCode.noUser = 0, "null", selectedCode.noUser.ToString)) Then myMainWin.StatusText = "Liste d'attente : Ajout du client " & foundClient(foundClient.Length - 1).fullName & "(" & addingClient & ") sur la liste d'attente sans rendez-vous"

        InternalUpdatesManager.getInstance.sendUpdate("QueueList()")
        listeQueueList.selected = 0
fin:
        addingClient = 0
        'Me.Enabled = True
        'Me.Select()
        'Loading(listeQueueList.ListCount - 1)
    End Sub

    Private Sub loading(Optional ByVal noToSelect As Short = -1, Optional ByVal clearFiltrage As Boolean = True)
        isLoading = True
        Modifier.Enabled = False : listeQueueList.cls()
        Dim OldFilterTrp, oldFilterCode As String
        OldFilterTrp = Filtrage.Text
        oldFilterCode = FCode.Text

        If clearFiltrage = True Then
            Filtrage.Items.Clear()
            FCode.Items.Clear()
        End If

        Dim i, j, n As Integer
        Dim myTRP As String
        Dim ql(,) As String = DBLinker.getInstance.readDB("InfoFolders RIGHT OUTER JOIN Villes RIGHT OUTER JOIN InfoClients ON Villes.NoVille = InfoClients.NoVille RIGHT OUTER JOIN ListeAttente ON InfoClients.NoClient = ListeAttente.NoClient ON InfoFolders.NoFolder = ListeAttente.NoFolder LEFT OUTER JOIN InfoVisites ON ListeAttente.NoVisite = InfoVisites.NoVisite", _
        "ListeAttente.NoQL, " & _
        "InfoClients.Nom + ',' + InfoClients.Prenom" & _
        ", InfoClients.Adresse, " & _
        "Villes.NomVille, " & _
        "InfoClients.Telephones, " & _
        "InfoClients.NAM, " & _
        "InfoFolders.Diagnostic, " & _
        "ListeAttente.NoClient, " & _
        "ListeAttente.NoFolder, " & _
        "ListeAttente.NoVisite, " & _
        "ListeAttente.NoTRP, " & _
        "ListeAttente.Remarques, " & _
        "ListeAttente.DateAppel, " & _
        "ListeAttente.Disponibilites, " & _
        "ListeAttente.NoCodeUnique, " & _
        "ListeAttente.Periode, " & _
        "ListeAttente.Service, " & _
        "InfoVisites.DateHeure, " & _
        "0, " & _
        "InfoFolders.Frequence, " & _
        "InfoVisites.Evaluation", _
        "WHERE 1=1 ORDER BY ListeAttente.DateAppel;")
        If ql Is Nothing OrElse ql.Length = 0 Then Exit Sub

        With listeQueueList
            For i = 0 To ql.GetUpperBound(1)
                If ql(10, i) = 0 Then
                    myTRP = "* Tous les thérapeutes *"
                Else
                    myTRP = UsersManager.getInstance.getUser(ql(10, i)).toString()
                End If

                Dim codeName As String = FolderCodesManager.getInstance.getCodeNameByNoUnique(ql(14, i))

                If useWinAsSelection = False Then
                    If (OldFilterTrp = "" Or OldFilterTrp = " Tout afficher " Or OldFilterTrp = myTRP Or myTRP.StartsWith("*")) And (oldFilterCode = "" Or oldFilterCode = " Tout afficher " Or oldFilterCode = codeName) Then
                        n = .add("(" & DateFormat.getTextDate(CDate(ql(12, i))) & ") " & ql(1, i))
                        If ql(9, i) = "" Or ql(9, i) = "0" Then
                            .ItemBackColor(n) = ColorWithoutRV.BackColor
                        Else
                            .ItemBackColor(n) = ColorWithRV.BackColor
                        End If
                        For j = 0 To ql.GetUpperBound(0)
                            .ItemValueA(n) &= ql(j, i) & "¶"
                        Next j
                        If CStr(.ItemValueA(n)).Length > 0 Then .ItemValueA(n) = CStr(.ItemValueA(n)).Substring(0, CStr(.ItemValueA(n)).Length - 1)
                        .ItemValueB(n) = ql(0, i)
                        If clearFiltrage = True Then
                            If Filtrage.FindStringExact(myTRP) < 0 Then Filtrage.Items.Add(myTRP)
                            If FCode.FindStringExact(codeName) < 0 Then FCode.Items.Add(codeName)
                        End If
                    End If
                Else
                    If searchArray(filtering, ql(7, i) & "§" & If(ql(8, i) = "", "0", ql(8, i)) & "§" & If(ql(9, i) = "", "0", ql(9, i)) & "§", SearchType.StartsWith) >= 0 Then
                        n = .add("(" & DateFormat.getTextDate(CDate(ql(12, i))) & ") " & ql(1, i))
                        If ql(9, i) = "0" Or ql(9, i) = "" Then
                            .ItemBackColor(n) = ColorWithoutRV.BackColor
                        Else
                            .ItemBackColor(n) = ColorWithRV.BackColor
                        End If
                        For j = 0 To ql.GetUpperBound(0)
                            .ItemValueA(n) &= ql(j, i) & "¶"
                        Next j
                        .ItemValueB(n) = ql(0, i)
                        If clearFiltrage = True Then
                            If Filtrage.FindStringExact(myTRP) < 0 Then Filtrage.Items.Add(myTRP)
                            If FCode.FindStringExact(codeName) < 0 Then FCode.Items.Add(codeName)
                        End If
                    End If
                End If
            Next i

            If clearFiltrage = True And useWinAsSelection = False Then
                Filtrage.Items.Add(" Tout afficher ")
                FCode.Items.Add(" Tout afficher ")
                If oldFilterCode <> "" Then FCode.SelectedIndex = FCode.FindStringExact(oldFilterCode)
                If FCode.SelectedIndex < 0 Then FCode.SelectedIndex = 0
                If OldFilterTrp <> "" Then Filtrage.SelectedIndex = Filtrage.FindStringExact(OldFilterTrp)
                If Filtrage.SelectedIndex < 0 Then Filtrage.SelectedIndex = 0
            End If
            If clearFiltrage = True And useWinAsSelection = True Then
                Filtrage.Items.Add(" Tout afficher ")
                Filtrage.SelectedIndex = Filtrage.FindStringExact(UsersManager.getInstance.getUser(therapeute).toString())
                If Filtrage.SelectedIndex < 0 Then Filtrage.SelectedIndex = 0
                FCode.Items.Add(" Tout afficher ")
                FCode.SelectedIndex = 0
                If currentDroitAcces(88) = False Then
                    FCode.Enabled = False
                    Filtrage.Enabled = False
                End If
            End If

            .selected = noToSelect
            .draw = True : .draw = False
            .showItem(noToSelect)
        End With

        If Me.allowModification Then
            lockItems(False)
        Else
            formModified = False
        End If

        isLoading = False
    End Sub

    Private Sub emptyFields()
        nom.Text = ""
        adresse.Text = ""
        ville.Text = ""
        Telephones.Items.Clear()
        DateAppel.Text = ""
        DateRV.Text = ""
        codedossier.Text = ""
        Periode.Text = ""
        TRP.Text = ""
        nam.Text = ""
        Remarques.Text = ""
        lockItems(True)
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        Modifier.Enabled = True
        DispoBtn.Enabled = True
        If useWinAsSelection = False Then
            If allowModification = True Then
                Enlever.Enabled = Not trueFalse
                Remarques.ReadOnly = trueFalse
            Else
                Enlever.Enabled = False
                Remarques.ReadOnly = True
            End If
            Selectionner.Enabled = False
        Else
            Modifier.Enabled = False
            Enlever.Enabled = False
            Remarques.ReadOnly = True
            Selectionner.Enabled = Not trueFalse
        End If
    End Sub

    Private Sub filtrage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Filtrage.SelectedIndexChanged
        If isLoading = False Then loading(, False)
    End Sub

    Private Sub listeQueueList_SelectedChange() Handles listeQueueList.selectedChange
        If isLoading AndAlso Me.Remarques.ReadOnly = False Then Exit Sub 'Quitte si la liste a été rechargé, mais que l'utilisateur modifie actuellement une entrée

        emptyFields()

        If listeQueueList.selected = -1 Then
            DispoBtn.Enabled = False : Modifier.Enabled = False : Enlever.Enabled = False
        Else
            Dim myQL() As String = CStr(listeQueueList.ItemValueA(listeQueueList.selected)).Split(New Char() {"¶"})
            nom.Text = myQL(1)
            adresse.Text = myQL(2)
            ville.Text = myQL(3)
            If myQL(4) <> "" Then
                Dim phones() As String = myQL(4).Split(New Char() {"§"})
                Telephones.Items.AddRange(phones)
                If Telephones.Items.Count <> 0 Then Telephones.SelectedIndex = 0
            End If
            Diagnostic.Text = myQL(6)
            DateAppel.Text = DateFormat.getTextDate(CDate(myQL(12)))
            If myQL(9) = "0" Or myQL(9) = "" Then
                DateRV.Text = "Aucun"
            Else
                Dim myDate As Date = CDate(myQL(17))
                DateRV.Text = DateFormat.getTextDate(myDate)
            End If
            If myQL(15) = "Évaluation" Or myQL(15) = "Traitement" Then
                Periode.Text = myQL(15)
            Else
                Dim periodes() As String = {"15 minutes", "30 minutes", "45 minutes", "1 heure", "1h15min", "1h30min", "1h45min", "2 heures"}
                Periode.Text = IIf(myQL(20) <> "" AndAlso myQL(20) = True, "Évaluation de ", "Traitement de ") & periodes(myQL(15) / 15 - 1)
            End If
            'REM_CODES
            codedossier.Text = FolderCodesManager.getInstance.getCodeNameByNoUnique(myQL(14))
            If myQL(10) = 0 Then
                TRP.Text = "* Tous les thérapeutes *"
            Else
                TRP.Text = UsersManager.getInstance.getUser(myQL(10)).toString()
            End If
            nam.Text = myQL(5)
            Remarques.Text = myQL(11).Replace("<br>", vbCrLf)
            curDispo = ""
            lockItems(False)
        End If

        formModified = False
    End Sub

    Private Sub dispoBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DispoBtn.Click
        If listeQueueList.selected < 0 Then MessageBox.Show("Veuillez sélectionner un client", "Information manquante") : Exit Sub

        Dim myQL() As String = CStr(listeQueueList.ItemValueA(listeQueueList.selected)).Split(New Char() {"¶"})
        Dim myDisponibilites As New Disponibilities()
        Dim myDispo As String = myQL(13)
        If curDispo <> "" Then myDispo = curDispo
        myDispo = myDisponibilites.GetDisponisibilites(myDispo, Not allowModification)

        If myDispo <> "" Then curDispo = myDispo : formModified = True
    End Sub

    Private Sub deleting(ByVal noQL As Integer)
        Dim myQL() As String = CStr(listeQueueList.ItemValueA(listeQueueList.selected)).Split(New Char() {"¶"})
        Dim rvText As String = ""

        DBLinker.getInstance.delDB("ListeAttente", "NoQL", noQL, False)
        If myQL(9) = "" OrElse myQL(9) = 0 Then
            rvText = "sans rendez-vous"
        Else
            rvText = "avec rendez-vous (" & Me.DateRV.Text & ")"
            DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite,Comments", "32," & myQL(7) & "," & myQL(8) & "," & myQL(9) & ",''")
        End If
        InternalUpdatesManager.getInstance.sendUpdate("QueueList()")

        myMainWin.StatusText = "Liste d'attente : Le client " & nom.Text & " (" & myQL(7) & ") " & rvText & " a été supprimé de la liste d'attente"
    End Sub

    Private Sub enlever_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Enlever.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir enlever ce client de la liste d'attente ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim noQL As Integer = listeQueueList.ItemValueB(listeQueueList.selected)
        deleting(noQL)

        lockSecteur("QueueList-" & noQL, False)
        Modifier.Image = imgModifSave.Images(0)
        ToolTip1.SetToolTip(Modifier, "Modifier")
        allowModification = False
        emptyFields()
        listeQueueList.selected = -1
    End Sub

    Private Sub modifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modifier.Click
        'Droit & Accès
        If currentDroitAcces(19) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier la liste d'attente." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If ToolTip1.GetToolTip(Modifier) = "Modifier" Then
            If lockSecteur("QueueList-" & listeQueueList.ItemValueB(listeQueueList.selected), True, "Liste d'attente") = False Then
                If PreferencesManager.getUserPreferences()("AffMSGModif") = True Then MessageBox.Show("Impossible de modifier. Cette sélection est déjà en cours de modification par un autre utilisateur.", "Liste d'attente : Impossible de modifier")
                Exit Sub
            End If

            Modifier.Image = imgModifSave.Images(1)
            ToolTip1.SetToolTip(Modifier, "Enregistrer")
            allowModification = True
        Else
            Dim myQL() As String = CStr(listeQueueList.ItemValueA(listeQueueList.selected)).Split(New Char() {"¶"})
            Dim savingDispo As String = ""

            Modifier.Image = imgModifSave.Images(0)
            ToolTip1.SetToolTip(Modifier, "Modifier")
            myQL(11) = Remarques.Text.Replace(vbCrLf, "<br>")
            If curDispo <> "" Then myQL(13) = curDispo : savingDispo = ",Disponibilites='" & curDispo & "'"
            listeQueueList.ItemValueA(listeQueueList.selected) = String.Join("¶", myQL)
            Dim curNoQL As Integer = listeQueueList.ItemValueB(listeQueueList.selected)
            DBLinker.getInstance.updateDB("ListeAttente", "Remarques='" & myQL(11).Replace("'", "''") & "'" & savingDispo, "NoQL", curNoQL, False)
            lockSecteur("QueueList-" & listeQueueList.ItemValueB(listeQueueList.selected), False)
            InternalUpdatesManager.getInstance.sendUpdate("QueueList()")

            Dim rvText As String = ""
            If myQL(9) = "" OrElse myQL(9) = 0 Then
                rvText = "sans rendez-vous"
            Else
                rvText = "avec rendez-vous (" & Me.DateRV.Text & ")"
            End If

            myMainWin.StatusText = "Liste d'attente : Le client " & nom.Text & " (" & myQL(7) & ") " & rvText & " a été modifié"
            formModified = False : allowModification = False
        End If
    End Sub

    Private Sub selectionner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Selectionner.Click
        'REM_CODES
        Dim MyQL(), myDispo() As String, Possibilities() As Date = Nothing, PossibilitiesStr() As String = Nothing
        Dim FirstTime, EndTime, transitiveDate As Date
        Dim n, vPeriode As Short
        n = 0
        lastFreeTime = lastFreeTime.AddMinutes(15)

        MyQL = CStr(listeQueueList.ItemValueA(listeQueueList.selected)).Split(New Char() {"¶"})
        Dim noQL As Integer = listeQueueList.ItemValueB(listeQueueList.selected)
        myDispo = CStr(MyQL(13)).Split(New Char() {";"})
        Dim noFolder, noVisite As Integer
        If MyQL(8) IsNot DBNull.Value Then Integer.TryParse(MyQL(8), noFolder)
        If MyQL(9) IsNot DBNull.Value Then Integer.TryParse(MyQL(9), noVisite)


        Dim myCode As Accounts.Clients.Folders.Codifications.FolderCode = Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getItemable(Integer.Parse(MyQL(14)), therapeute, visiteDate)

        If MyQL(15) = "Traitement" Then
            vPeriode = myCode.getDefaultPeriod(False)
        ElseIf MyQL(15) = "Évaluation" Then
            vPeriode = myCode.getDefaultPeriod(True)
        Else
            vPeriode = MyQL(15)
        End If

        Dim endingDate As Date = visiteDate.AddDays(1)

        FirstTime = firstFreeTime
        EndTime = FirstTime.AddMinutes(vPeriode)

        'REM Isn't checking supposed to be done already (on loading) ?
        Dim agendaEntries As Generic.List(Of AgendaEntry) = AgendaManager.getInstance.loadEntries(therapeute, FirstTime, EndTime, False)
        If myDispo(weekDayNo * 2) <> "--:--" Then transitiveDate = CDate(myDispo(weekDayNo * 2 + 1)).AddMinutes(15) : myDispo(weekDayNo * 2 + 1) = DateFormat.getTextDate(transitiveDate, DateFormat.TextDateOptions.ShortTime)
        Do Until time1Suptime2(EndTime, CDate(lastFreeTime)) = True
            If myDispo(weekDayNo * 2) = "--:--" Then
                If AgendaManager.getInstance.checkTimeConflict(FirstTime, vPeriode, therapeute, , MyQL(7), myCode.noUnique, agendaEntries, , noVisite, noFolder) = "" Then
                    ReDim Preserve Possibilities(n), PossibilitiesStr(n)
                    Possibilities(n) = FirstTime
                    PossibilitiesStr(n) = DateFormat.getTextDate(FirstTime, DateFormat.TextDateOptions.ShortTime)
                    n += 1
                End If
            Else
                If time1Suptime2(CDate(FirstTime), CDate(myDispo(weekDayNo * 2)), True) And time1Suptime2(CDate(myDispo(weekDayNo * 2 + 1)), CDate(EndTime), True) Then
                    If AgendaManager.getInstance.checkTimeConflict(FirstTime, vPeriode, therapeute, , MyQL(7), myCode.noUnique, agendaEntries, , noVisite, noFolder) = "" Then
                        ReDim Preserve Possibilities(n), PossibilitiesStr(n)
                        Possibilities(n) = FirstTime
                        PossibilitiesStr(n) = DateFormat.getTextDate(FirstTime, DateFormat.TextDateOptions.ShortTime)
                        n += 1
                    End If
                End If
            End If
            FirstTime = FirstTime.AddMinutes(15)
            EndTime = FirstTime.AddMinutes(vPeriode)
        Loop

        If Possibilities Is Nothing OrElse Possibilities.Length = 0 Then
            MessageBox.Show("Un rendez-vous a été ajouté dans la plage qui était disponible." & vbCrLf & "Il n'y a hélas plus de place disponible pour ce client", "Conflit")
            Exit Sub
        Else
            Dim removalDate As String = DateRV.Text
            Dim myMultiChoice As New multichoice()
            Dim myChoice As Short = myMultiChoice.GetChoice("Sélectionner une heure pour le rendez-vous", String.Join("§", PossibilitiesStr), "INDEX", "§", False, DateFormat.getTextDate(selectedTime, DateFormat.TextDateOptions.ShortTime))
            If myChoice < 0 Then Exit Sub

            Me.Close()
            If removalDate = "Aucun" Then
                Me.DialogResult = System.Windows.Forms.DialogResult.Yes
                openNewRV(MyQL(7), therapeute, , , Possibilities(myChoice), MyQL(0), Periode.Text, MyQL(14))
            Else
                Me.DialogResult = System.Windows.Forms.DialogResult.OK

                deleting(noQL)

                Dim rv As RendezVous = RendezVousManager.getInstance.loadRendezVous(CInt(MyQL(9)))
                rv.cut()
                rv.pasteTo(visiteDate, therapeute, rv.period)
            End If
        End If
    End Sub

    Private Sub remarques_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Remarques.TextChanged
        formModified = True
    End Sub

    Private Sub removeUsed()
        lockSecteur("QueueList-" & listeQueueList.ItemValueB(listeQueueList.selected), False)
    End Sub

    Private Sub listeQueueList_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles listeQueueList.willSelect
        If isLoading Then Exit Sub

        If allowModification = True Then
            removeUsed()

            If formModified = True AndAlso MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                modifier_Click(listeQueueList, EventArgs.Empty)
            End If
            Modifier.Image = imgModifSave.Images(0)
            ToolTip1.SetToolTip(Modifier, "Modifier")
            allowModification = False
        End If
        formModified = False
    End Sub

    Private Sub fCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FCode.SelectedIndexChanged
        If isLoading = False Then loading(, False)
    End Sub

    Private Sub nom_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles nom.MouseClick
        If listeQueueList.selected <> -1 AndAlso e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim myQL() As String = CStr(listeQueueList.ItemValueA(listeQueueList.selected)).Split(New Char() {"¶"})

            openAccount(myQL(7))
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf queueList_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function = "QueueList" Then
            reLoad()
        End If
    End Sub
End Class
