Imports CI.Clinica.Accounts.Clients.Folders.Codifications
Imports CI.Clinica.Accounts.Clients.Folders
Imports CI.Clinica.Accounts.Clients.Folders.RVs

Friend Class addvisite
    Inherits SingleWindow


#Region "Windows Form Designer generated code "
    Public Sub New(Optional ByVal usageMode As UsageModes = UsageModes.AddingRV)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.
        Me.MdiParent = myMainWin
        codeNoUnique = -1
        currentFolder = -1
        periode.SelectedIndex = 0

        loadTRP(False)

        'Load Région
        Dim regions() As String = DBLinker.getInstance.readOneDBField("SiteLesion", "SiteLesion")
        If Not regions Is Nothing AndAlso regions.Length <> 0 Then region_Renamed.Items.AddRange(regions)



        'Chargement des images
        Me.selectDate.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.selectcode.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.selectDateAccident.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.selectDateRechute.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.selectmedecin.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.getTimeDate.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.adding.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.getNAM.Image = DrawingManager.getInstance.getImage("selection16.gif")


        'Set usage mode
        Me.usageMode = usageMode
        If usageMode = UsageModes.AddingFolder Then
            Me.Text = "Ajout d'un dossier client"
            Me.dossier.SelectedIndex = 0
            Me.dossier.Enabled = False
            _Labels_1.Visible = False
            _Labels_3.Visible = False
            getTimeDate.Visible = False
            dhVisite.Visible = False
            periode.Visible = False
            ToolTip1.SetToolTip(Me.adding, "Ajouter le dossier client")
        End If
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
    Private WithEvents adminButton As System.Windows.Forms.Button
    Private WithEvents periode As System.Windows.Forms.ComboBox
    Private WithEvents noref As ManagedText
    Private WithEvents selectcode As System.Windows.Forms.Button
    Private WithEvents selectmedecin As System.Windows.Forms.Button
    Private WithEvents therapeute As ManagedCombo
    Private WithEvents region_Renamed As ManagedCombo
    Private WithEvents dossier As System.Windows.Forms.ComboBox
    Private WithEvents _Labels_9 As System.Windows.Forms.Label
    Private WithEvents codedossier As System.Windows.Forms.Label
    Private WithEvents _Labels_8 As System.Windows.Forms.Label
    Private WithEvents medecin As System.Windows.Forms.Label
    Private WithEvents _Labels_7 As System.Windows.Forms.Label
    Private WithEvents _Labels_6 As System.Windows.Forms.Label
    Private WithEvents _Labels_5 As System.Windows.Forms.Label
    Private WithEvents _Labels_4 As System.Windows.Forms.Label
    Private WithEvents _Labels_2 As System.Windows.Forms.Label
    Private WithEvents dossierFrame As System.Windows.Forms.GroupBox
    Private WithEvents getTimeDate As System.Windows.Forms.Button
    Private WithEvents adding As System.Windows.Forms.Button
    Private WithEvents getNAM As System.Windows.Forms.Button
    Private WithEvents nam As ManagedText
    Private WithEvents dhVisite As System.Windows.Forms.TextBox
    Private WithEvents _Labels_3 As System.Windows.Forms.Label
    Private WithEvents _Labels_0 As System.Windows.Forms.Label
    Private WithEvents _Labels_1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Private WithEvents menuaddvisitesearch As System.Windows.Forms.ContextMenu
    Private WithEvents menusearchdispo As System.Windows.Forms.MenuItem
    Private WithEvents menuline As System.Windows.Forms.MenuItem
    Private WithEvents menusearchchoisir As System.Windows.Forms.MenuItem
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents nom As System.Windows.Forms.TextBox
    Private WithEvents adresse As System.Windows.Forms.TextBox
    Private WithEvents ville As System.Windows.Forms.TextBox
    Private WithEvents service As ManagedCombo
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents Diagnostic As ManagedText
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents selectDate As System.Windows.Forms.Button
    Private WithEvents dateReference As System.Windows.Forms.Label
    Private WithEvents askedTRP As ManagedCombo
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents tel As System.Windows.Forms.TextBox
    Private WithEvents remarques As ManagedText
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents selectDateRechute As System.Windows.Forms.Button
    Private WithEvents selectDateAccident As System.Windows.Forms.Button
    Private WithEvents dateRechute As System.Windows.Forms.Label
    Private WithEvents dateAccident As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents frequence As Clinica.ManagedCombo
    Private WithEvents duree As Clinica.ManagedCombo
    Private WithEvents trpToTransfer As Clinica.ManagedCombo
    Private WithEvents label13 As System.Windows.Forms.Label
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents label14 As System.Windows.Forms.Label
    Private WithEvents menuSelectAgenda As System.Windows.Forms.MenuItem
    Private WithEvents label7 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.adminButton = New System.Windows.Forms.Button
        Me.periode = New System.Windows.Forms.ComboBox
        Me.dossierFrame = New System.Windows.Forms.GroupBox
        Me.frequence = New ManagedCombo
        Me.duree = New ManagedCombo
        Me.trpToTransfer = New ManagedCombo
        Me.askedTRP = New ManagedCombo
        Me.label13 = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.selectDateRechute = New System.Windows.Forms.Button
        Me.selectDateAccident = New System.Windows.Forms.Button
        Me.selectDate = New System.Windows.Forms.Button
        Me.dateRechute = New System.Windows.Forms.Label
        Me.dateAccident = New System.Windows.Forms.Label
        Me.dateReference = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me.label9 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.Diagnostic = New ManagedText
        Me.service = New ManagedCombo
        Me.label4 = New System.Windows.Forms.Label
        Me.remarques = New ManagedText
        Me.noref = New ManagedText
        Me.selectcode = New System.Windows.Forms.Button
        Me.selectmedecin = New System.Windows.Forms.Button
        Me.therapeute = New ManagedCombo
        Me.region_Renamed = New ManagedCombo
        Me.dossier = New System.Windows.Forms.ComboBox
        Me.label15 = New System.Windows.Forms.Label
        Me.label14 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me._Labels_9 = New System.Windows.Forms.Label
        Me.codedossier = New System.Windows.Forms.Label
        Me._Labels_8 = New System.Windows.Forms.Label
        Me.medecin = New System.Windows.Forms.Label
        Me._Labels_7 = New System.Windows.Forms.Label
        Me._Labels_6 = New System.Windows.Forms.Label
        Me._Labels_5 = New System.Windows.Forms.Label
        Me._Labels_4 = New System.Windows.Forms.Label
        Me._Labels_2 = New System.Windows.Forms.Label
        Me.getTimeDate = New System.Windows.Forms.Button
        Me.adding = New System.Windows.Forms.Button
        Me.getNAM = New System.Windows.Forms.Button
        Me.nam = New ManagedText
        Me.dhVisite = New System.Windows.Forms.TextBox
        Me._Labels_3 = New System.Windows.Forms.Label
        Me._Labels_0 = New System.Windows.Forms.Label
        Me._Labels_1 = New System.Windows.Forms.Label
        Me.menuaddvisitesearch = New System.Windows.Forms.ContextMenu
        Me.menusearchdispo = New System.Windows.Forms.MenuItem
        Me.menuline = New System.Windows.Forms.MenuItem
        Me.menuSelectAgenda = New System.Windows.Forms.MenuItem
        Me.menusearchchoisir = New System.Windows.Forms.MenuItem
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.nom = New System.Windows.Forms.TextBox
        Me.adresse = New System.Windows.Forms.TextBox
        Me.ville = New System.Windows.Forms.TextBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tel = New System.Windows.Forms.TextBox
        Me.label7 = New System.Windows.Forms.Label
        Me.dossierFrame.SuspendLayout()
        Me.SuspendLayout()
        '
        'adminButton
        '
        Me.adminButton.BackColor = System.Drawing.SystemColors.Control
        Me.adminButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.adminButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adminButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adminButton.Location = New System.Drawing.Point(0, 40)
        Me.adminButton.Name = "adminButton"
        Me.adminButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adminButton.Size = New System.Drawing.Size(33, 25)
        Me.adminButton.TabIndex = 19
        Me.adminButton.Text = "Set"
        Me.adminButton.UseVisualStyleBackColor = False
        Me.adminButton.Visible = False
        '
        'periode
        '
        Me.periode.BackColor = System.Drawing.SystemColors.Window
        Me.periode.Cursor = System.Windows.Forms.Cursors.Default
        Me.periode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.periode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.periode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.periode.Items.AddRange(New Object() {"Traitement", "Évaluation", "15 minutes", "30 minutes", "45 minutes", "1 heure", "1h15min", "1h30min", "1h45min", "2 heures"})
        Me.periode.Location = New System.Drawing.Point(318, 467)
        Me.periode.Name = "periode"
        Me.periode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.periode.Size = New System.Drawing.Size(125, 22)
        Me.periode.TabIndex = 18
        '
        'dossierFrame
        '
        Me.dossierFrame.BackColor = System.Drawing.SystemColors.Control
        Me.dossierFrame.Controls.Add(Me.frequence)
        Me.dossierFrame.Controls.Add(Me.duree)
        Me.dossierFrame.Controls.Add(Me.trpToTransfer)
        Me.dossierFrame.Controls.Add(Me.askedTRP)
        Me.dossierFrame.Controls.Add(Me.label13)
        Me.dossierFrame.Controls.Add(Me.label6)
        Me.dossierFrame.Controls.Add(Me.selectDateRechute)
        Me.dossierFrame.Controls.Add(Me.selectDateAccident)
        Me.dossierFrame.Controls.Add(Me.selectDate)
        Me.dossierFrame.Controls.Add(Me.dateRechute)
        Me.dossierFrame.Controls.Add(Me.dateAccident)
        Me.dossierFrame.Controls.Add(Me.dateReference)
        Me.dossierFrame.Controls.Add(Me.label11)
        Me.dossierFrame.Controls.Add(Me.label9)
        Me.dossierFrame.Controls.Add(Me.label5)
        Me.dossierFrame.Controls.Add(Me.Diagnostic)
        Me.dossierFrame.Controls.Add(Me.service)
        Me.dossierFrame.Controls.Add(Me.label4)
        Me.dossierFrame.Controls.Add(Me.remarques)
        Me.dossierFrame.Controls.Add(Me.noref)
        Me.dossierFrame.Controls.Add(Me.selectcode)
        Me.dossierFrame.Controls.Add(Me.selectmedecin)
        Me.dossierFrame.Controls.Add(Me.therapeute)
        Me.dossierFrame.Controls.Add(Me.region_Renamed)
        Me.dossierFrame.Controls.Add(Me.dossier)
        Me.dossierFrame.Controls.Add(Me.label15)
        Me.dossierFrame.Controls.Add(Me.label14)
        Me.dossierFrame.Controls.Add(Me.label8)
        Me.dossierFrame.Controls.Add(Me._Labels_9)
        Me.dossierFrame.Controls.Add(Me.codedossier)
        Me.dossierFrame.Controls.Add(Me._Labels_8)
        Me.dossierFrame.Controls.Add(Me.medecin)
        Me.dossierFrame.Controls.Add(Me._Labels_7)
        Me.dossierFrame.Controls.Add(Me._Labels_6)
        Me.dossierFrame.Controls.Add(Me._Labels_5)
        Me.dossierFrame.Controls.Add(Me._Labels_4)
        Me.dossierFrame.Controls.Add(Me._Labels_2)
        Me.dossierFrame.Enabled = False
        Me.dossierFrame.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dossierFrame.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dossierFrame.Location = New System.Drawing.Point(8, 72)
        Me.dossierFrame.Name = "dossierFrame"
        Me.dossierFrame.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dossierFrame.Size = New System.Drawing.Size(465, 389)
        Me.dossierFrame.TabIndex = 6
        Me.dossierFrame.TabStop = False
        Me.dossierFrame.Text = "Dossier"
        '
        'frequence
        '
        Me.frequence.acceptAlpha = True
        Me.frequence.acceptedChars = Nothing
        Me.frequence.acceptNumeric = True
        Me.frequence.allCapital = False
        Me.frequence.allLower = False
        Me.frequence.autoComplete = True
        Me.frequence.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.frequence.autoSizeDropDown = True
        Me.frequence.BackColor = System.Drawing.SystemColors.ControlLight
        Me.frequence.blockOnMaximum = False
        Me.frequence.blockOnMinimum = False
        Me.frequence.cb_AcceptLeftZeros = False
        Me.frequence.cb_AcceptNegative = False
        Me.frequence.currencyBox = False
        Me.frequence.Cursor = System.Windows.Forms.Cursors.Default
        Me.frequence.dbField = Nothing
        Me.frequence.doComboDelete = True
        Me.frequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.frequence.firstLetterCapital = False
        Me.frequence.firstLettersCapital = False
        Me.frequence.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frequence.ForeColor = System.Drawing.SystemColors.WindowText
        Me.frequence.Items.AddRange(New Object() {"1xSemaine", "2xSemaine", "3xSemaine", "4xSemaine", "5xSemaine", "6xSemaine", "7xSemaine", "1x2 semaines", "1xMois"})
        Me.frequence.itemsToolTipDuration = 10000
        Me.frequence.Location = New System.Drawing.Point(340, 360)
        Me.frequence.manageText = True
        Me.frequence.matchExp = Nothing
        Me.frequence.maximum = 0
        Me.frequence.minimum = 0
        Me.frequence.Name = "frequence"
        Me.frequence.nbDecimals = CType(-1, Short)
        Me.frequence.onlyAlphabet = False
        Me.frequence.pathOfList = Nothing
        Me.frequence.ReadOnly = True
        Me.frequence.refuseAccents = False
        Me.frequence.refusedChars = Nothing
        Me.frequence.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frequence.showItemsToolTip = False
        Me.frequence.Size = New System.Drawing.Size(116, 22)
        Me.frequence.TabIndex = 16
        Me.frequence.trimText = False
        '
        'duree
        '
        Me.duree.acceptAlpha = True
        Me.duree.acceptedChars = Nothing
        Me.duree.acceptNumeric = True
        Me.duree.allCapital = False
        Me.duree.allLower = False
        Me.duree.autoComplete = True
        Me.duree.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.duree.autoSizeDropDown = True
        Me.duree.BackColor = System.Drawing.SystemColors.ControlLight
        Me.duree.blockOnMaximum = False
        Me.duree.blockOnMinimum = False
        Me.duree.cb_AcceptLeftZeros = False
        Me.duree.cb_AcceptNegative = False
        Me.duree.currencyBox = False
        Me.duree.Cursor = System.Windows.Forms.Cursors.Default
        Me.duree.dbField = Nothing
        Me.duree.doComboDelete = True
        Me.duree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.duree.firstLetterCapital = False
        Me.duree.firstLettersCapital = False
        Me.duree.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.duree.ForeColor = System.Drawing.SystemColors.WindowText
        Me.duree.Items.AddRange(New Object() {"???", "005 jours", "010 jours", "015 jours", "020 jours", "025 jours", "030 jours", "035 jours", "040 jours", "045 jours", "050 jours", "055 jours", "060 jours", "065 jours", "070 jours", "075 jours", "080 jours", "085 jours", "090 jours", "095 jours", "100 jours", "105 jours", "110 jours", "115 jours", "120 jours", "125 jours", "130 jours", "135 jours", "140 jours", "145 jours", "150 jours", "155 jours", "160 jours", "165 jours", "170 jours", "175 jours", "180 jours", "185 jours", "190 jours", "195 jours", "200 jours", "205 jours", "210 jours", "215 jours", "220 jours", "225 jours", "230 jours", "235 jours", "240 jours", "245 jours", "250 jours", "255 jours", "260 jours", "265 jours", "270 jours", "275 jours", "280 jours", "285 jours", "290 jours", "295 jours", "300 jours", "305 jours", "310 jours", "315 jours", "320 jours", "325 jours", "330 jours", "335 jours", "340 jours", "345 jours", "350 jours", "355 jours", "360 jours", "365 jours", "Plus de 365 jours"})
        Me.duree.itemsToolTipDuration = 10000
        Me.duree.Location = New System.Drawing.Point(143, 360)
        Me.duree.manageText = True
        Me.duree.matchExp = Nothing
        Me.duree.maximum = 0
        Me.duree.minimum = 0
        Me.duree.Name = "duree"
        Me.duree.nbDecimals = CType(-1, Short)
        Me.duree.onlyAlphabet = False
        Me.duree.pathOfList = Nothing
        Me.duree.ReadOnly = True
        Me.duree.refuseAccents = False
        Me.duree.refusedChars = Nothing
        Me.duree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.duree.showItemsToolTip = False
        Me.duree.Size = New System.Drawing.Size(120, 22)
        Me.duree.Sorted = True
        Me.duree.TabIndex = 15
        Me.duree.trimText = False
        '
        'trpToTransfer
        '
        Me.trpToTransfer.acceptAlpha = True
        Me.trpToTransfer.acceptedChars = Nothing
        Me.trpToTransfer.acceptNumeric = True
        Me.trpToTransfer.allCapital = False
        Me.trpToTransfer.allLower = False
        Me.trpToTransfer.autoComplete = True
        Me.trpToTransfer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.trpToTransfer.autoSizeDropDown = True
        Me.trpToTransfer.BackColor = System.Drawing.SystemColors.ControlLight
        Me.trpToTransfer.blockOnMaximum = False
        Me.trpToTransfer.blockOnMinimum = False
        Me.trpToTransfer.cb_AcceptLeftZeros = False
        Me.trpToTransfer.cb_AcceptNegative = False
        Me.trpToTransfer.currencyBox = False
        Me.trpToTransfer.Cursor = System.Windows.Forms.Cursors.Default
        Me.trpToTransfer.dbField = Nothing
        Me.trpToTransfer.doComboDelete = True
        Me.trpToTransfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.trpToTransfer.firstLetterCapital = False
        Me.trpToTransfer.firstLettersCapital = False
        Me.trpToTransfer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trpToTransfer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.trpToTransfer.itemsToolTipDuration = 10000
        Me.trpToTransfer.Location = New System.Drawing.Point(143, 109)
        Me.trpToTransfer.manageText = True
        Me.trpToTransfer.matchExp = Nothing
        Me.trpToTransfer.maximum = 0
        Me.trpToTransfer.minimum = 0
        Me.trpToTransfer.Name = "trpToTransfer"
        Me.trpToTransfer.nbDecimals = CType(-1, Short)
        Me.trpToTransfer.onlyAlphabet = False
        Me.trpToTransfer.pathOfList = Nothing
        Me.trpToTransfer.ReadOnly = True
        Me.trpToTransfer.refuseAccents = False
        Me.trpToTransfer.refusedChars = Nothing
        Me.trpToTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.trpToTransfer.showItemsToolTip = False
        Me.trpToTransfer.Size = New System.Drawing.Size(312, 22)
        Me.trpToTransfer.Sorted = True
        Me.trpToTransfer.TabIndex = 5
        Me.trpToTransfer.trimText = False
        '
        'askedTRP
        '
        Me.askedTRP.acceptAlpha = True
        Me.askedTRP.acceptedChars = Nothing
        Me.askedTRP.acceptNumeric = True
        Me.askedTRP.allCapital = False
        Me.askedTRP.allLower = False
        Me.askedTRP.autoComplete = True
        Me.askedTRP.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.askedTRP.autoSizeDropDown = True
        Me.askedTRP.BackColor = System.Drawing.SystemColors.ControlLight
        Me.askedTRP.blockOnMaximum = False
        Me.askedTRP.blockOnMinimum = False
        Me.askedTRP.cb_AcceptLeftZeros = False
        Me.askedTRP.cb_AcceptNegative = False
        Me.askedTRP.currencyBox = False
        Me.askedTRP.Cursor = System.Windows.Forms.Cursors.Default
        Me.askedTRP.dbField = Nothing
        Me.askedTRP.doComboDelete = True
        Me.askedTRP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.askedTRP.firstLetterCapital = False
        Me.askedTRP.firstLettersCapital = False
        Me.askedTRP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.askedTRP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.askedTRP.itemsToolTipDuration = 10000
        Me.askedTRP.Location = New System.Drawing.Point(143, 85)
        Me.askedTRP.manageText = True
        Me.askedTRP.matchExp = Nothing
        Me.askedTRP.maximum = 0
        Me.askedTRP.minimum = 0
        Me.askedTRP.Name = "askedTRP"
        Me.askedTRP.nbDecimals = CType(-1, Short)
        Me.askedTRP.onlyAlphabet = False
        Me.askedTRP.pathOfList = Nothing
        Me.askedTRP.ReadOnly = True
        Me.askedTRP.refuseAccents = False
        Me.askedTRP.refusedChars = Nothing
        Me.askedTRP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.askedTRP.showItemsToolTip = False
        Me.askedTRP.Size = New System.Drawing.Size(312, 22)
        Me.askedTRP.Sorted = True
        Me.askedTRP.TabIndex = 4
        Me.askedTRP.trimText = False
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.BackColor = System.Drawing.SystemColors.Control
        Me.label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label13.Location = New System.Drawing.Point(8, 112)
        Me.label13.Name = "label13"
        Me.label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label13.Size = New System.Drawing.Size(129, 14)
        Me.label13.TabIndex = 34
        Me.label13.Text = "Thérapeute à transférer :"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.SystemColors.Control
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(8, 88)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(115, 14)
        Me.label6.TabIndex = 34
        Me.label6.Text = "Thérapeute demandé :"
        '
        'selectDateRechute
        '
        Me.selectDateRechute.BackColor = System.Drawing.SystemColors.Control
        Me.selectDateRechute.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectDateRechute.Enabled = False
        Me.selectDateRechute.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectDateRechute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectDateRechute.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectDateRechute.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectDateRechute.Location = New System.Drawing.Point(143, 207)
        Me.selectDateRechute.Name = "selectDateRechute"
        Me.selectDateRechute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectDateRechute.Size = New System.Drawing.Size(24, 24)
        Me.selectDateRechute.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.selectDateRechute, "Choisir la date de rechute")
        Me.selectDateRechute.UseVisualStyleBackColor = False
        '
        'selectDateAccident
        '
        Me.selectDateAccident.BackColor = System.Drawing.SystemColors.Control
        Me.selectDateAccident.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectDateAccident.Enabled = False
        Me.selectDateAccident.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectDateAccident.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectDateAccident.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectDateAccident.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectDateAccident.Location = New System.Drawing.Point(143, 182)
        Me.selectDateAccident.Name = "selectDateAccident"
        Me.selectDateAccident.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectDateAccident.Size = New System.Drawing.Size(24, 24)
        Me.selectDateAccident.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.selectDateAccident, "Choisir la date d'accident")
        Me.selectDateAccident.UseVisualStyleBackColor = False
        '
        'selectDate
        '
        Me.selectDate.BackColor = System.Drawing.SystemColors.Control
        Me.selectDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectDate.Enabled = False
        Me.selectDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectDate.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectDate.Location = New System.Drawing.Point(143, 157)
        Me.selectDate.Name = "selectDate"
        Me.selectDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectDate.Size = New System.Drawing.Size(24, 24)
        Me.selectDate.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.selectDate, "Choisir la date de référence")
        Me.selectDate.UseVisualStyleBackColor = False
        '
        'dateRechute
        '
        Me.dateRechute.AutoSize = True
        Me.dateRechute.BackColor = System.Drawing.SystemColors.Control
        Me.dateRechute.Cursor = System.Windows.Forms.Cursors.Default
        Me.dateRechute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateRechute.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dateRechute.Location = New System.Drawing.Point(175, 212)
        Me.dateRechute.Name = "dateRechute"
        Me.dateRechute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dateRechute.Size = New System.Drawing.Size(133, 14)
        Me.dateRechute.TabIndex = 32
        Me.dateRechute.Text = "Aucune date sélectionnée"
        '
        'dateAccident
        '
        Me.dateAccident.AutoSize = True
        Me.dateAccident.BackColor = System.Drawing.SystemColors.Control
        Me.dateAccident.Cursor = System.Windows.Forms.Cursors.Default
        Me.dateAccident.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateAccident.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dateAccident.Location = New System.Drawing.Point(175, 187)
        Me.dateAccident.Name = "dateAccident"
        Me.dateAccident.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dateAccident.Size = New System.Drawing.Size(133, 14)
        Me.dateAccident.TabIndex = 32
        Me.dateAccident.Text = "Aucune date sélectionnée"
        '
        'dateReference
        '
        Me.dateReference.AutoSize = True
        Me.dateReference.BackColor = System.Drawing.SystemColors.Control
        Me.dateReference.Cursor = System.Windows.Forms.Cursors.Default
        Me.dateReference.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateReference.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dateReference.Location = New System.Drawing.Point(175, 162)
        Me.dateReference.Name = "dateReference"
        Me.dateReference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dateReference.Size = New System.Drawing.Size(133, 14)
        Me.dateReference.TabIndex = 32
        Me.dateReference.Text = "Aucune date sélectionnée"
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.BackColor = System.Drawing.SystemColors.Control
        Me.label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label11.Location = New System.Drawing.Point(8, 211)
        Me.label11.Name = "label11"
        Me.label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label11.Size = New System.Drawing.Size(90, 14)
        Me.label11.TabIndex = 31
        Me.label11.Text = "Date de rechute :"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.SystemColors.Control
        Me.label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label9.Location = New System.Drawing.Point(8, 186)
        Me.label9.Name = "label9"
        Me.label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label9.Size = New System.Drawing.Size(87, 14)
        Me.label9.TabIndex = 31
        Me.label9.Text = "Date d'accident :"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.SystemColors.Control
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(8, 161)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(101, 14)
        Me.label5.TabIndex = 31
        Me.label5.Text = "Date de référence :"
        '
        'Diagnostic
        '
        Me.Diagnostic.acceptAlpha = True
        Me.Diagnostic.acceptedChars = ""
        Me.Diagnostic.acceptNumeric = True
        Me.Diagnostic.allCapital = False
        Me.Diagnostic.allLower = False
        Me.Diagnostic.blockOnMaximum = False
        Me.Diagnostic.blockOnMinimum = False
        Me.Diagnostic.cb_AcceptLeftZeros = False
        Me.Diagnostic.cb_AcceptNegative = False
        Me.Diagnostic.currencyBox = False
        Me.Diagnostic.firstLetterCapital = True
        Me.Diagnostic.firstLettersCapital = False
        Me.Diagnostic.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Diagnostic.Location = New System.Drawing.Point(143, 134)
        Me.Diagnostic.manageText = True
        Me.Diagnostic.matchExp = ""
        Me.Diagnostic.maximum = 0
        Me.Diagnostic.minimum = 0
        Me.Diagnostic.Name = "Diagnostic"
        Me.Diagnostic.nbDecimals = CType(-1, Short)
        Me.Diagnostic.onlyAlphabet = False
        Me.Diagnostic.refuseAccents = False
        Me.Diagnostic.refusedChars = ""
        Me.Diagnostic.showInternalContextMenu = True
        Me.Diagnostic.Size = New System.Drawing.Size(312, 20)
        Me.Diagnostic.TabIndex = 6
        Me.Diagnostic.trimText = False
        '
        'service
        '
        Me.service.acceptAlpha = True
        Me.service.acceptedChars = Nothing
        Me.service.acceptNumeric = True
        Me.service.allCapital = False
        Me.service.allLower = False
        Me.service.autoComplete = True
        Me.service.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.service.autoSizeDropDown = True
        Me.service.BackColor = System.Drawing.SystemColors.ControlLight
        Me.service.blockOnMaximum = False
        Me.service.blockOnMinimum = False
        Me.service.cb_AcceptLeftZeros = False
        Me.service.cb_AcceptNegative = False
        Me.service.currencyBox = False
        Me.service.Cursor = System.Windows.Forms.Cursors.Default
        Me.service.dbField = Nothing
        Me.service.doComboDelete = True
        Me.service.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.service.firstLetterCapital = False
        Me.service.firstLettersCapital = False
        Me.service.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.service.ForeColor = System.Drawing.SystemColors.WindowText
        Me.service.itemsToolTipDuration = 10000
        Me.service.Location = New System.Drawing.Point(143, 307)
        Me.service.manageText = True
        Me.service.matchExp = Nothing
        Me.service.maximum = 0
        Me.service.minimum = 0
        Me.service.Name = "service"
        Me.service.nbDecimals = CType(-1, Short)
        Me.service.onlyAlphabet = False
        Me.service.pathOfList = Nothing
        Me.service.ReadOnly = True
        Me.service.refuseAccents = False
        Me.service.refusedChars = Nothing
        Me.service.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.service.showItemsToolTip = False
        Me.service.Size = New System.Drawing.Size(312, 22)
        Me.service.TabIndex = 13
        Me.service.trimText = False
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.SystemColors.Control
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(8, 309)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(50, 14)
        Me.label4.TabIndex = 28
        Me.label4.Text = "Service :"
        '
        'remarques
        '
        Me.remarques.acceptAlpha = True
        Me.remarques.acceptedChars = ""
        Me.remarques.acceptNumeric = True
        Me.remarques.AcceptsReturn = True
        Me.remarques.allCapital = False
        Me.remarques.allLower = False
        Me.remarques.BackColor = System.Drawing.SystemColors.ControlLight
        Me.remarques.blockOnMaximum = False
        Me.remarques.blockOnMinimum = False
        Me.remarques.cb_AcceptLeftZeros = False
        Me.remarques.cb_AcceptNegative = False
        Me.remarques.currencyBox = False
        Me.remarques.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.remarques.firstLetterCapital = False
        Me.remarques.firstLettersCapital = False
        Me.remarques.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.remarques.ForeColor = System.Drawing.SystemColors.WindowText
        Me.remarques.Location = New System.Drawing.Point(143, 334)
        Me.remarques.manageText = True
        Me.remarques.matchExp = ""
        Me.remarques.maximum = 0
        Me.remarques.MaxLength = 0
        Me.remarques.minimum = 0
        Me.remarques.Name = "remarques"
        Me.remarques.nbDecimals = CType(-1, Short)
        Me.remarques.onlyAlphabet = False
        Me.remarques.ReadOnly = True
        Me.remarques.refuseAccents = False
        Me.remarques.refusedChars = ""
        Me.remarques.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.remarques.showInternalContextMenu = True
        Me.remarques.Size = New System.Drawing.Size(313, 20)
        Me.remarques.TabIndex = 14
        Me.remarques.trimText = False
        '
        'noref
        '
        Me.noref.acceptAlpha = True
        Me.noref.acceptedChars = ""
        Me.noref.acceptNumeric = True
        Me.noref.AcceptsReturn = True
        Me.noref.allCapital = True
        Me.noref.allLower = False
        Me.noref.BackColor = System.Drawing.SystemColors.ControlLight
        Me.noref.blockOnMaximum = False
        Me.noref.blockOnMinimum = False
        Me.noref.cb_AcceptLeftZeros = False
        Me.noref.cb_AcceptNegative = False
        Me.noref.currencyBox = False
        Me.noref.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.noref.firstLetterCapital = False
        Me.noref.firstLettersCapital = False
        Me.noref.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.noref.ForeColor = System.Drawing.SystemColors.WindowText
        Me.noref.Location = New System.Drawing.Point(143, 283)
        Me.noref.manageText = True
        Me.noref.matchExp = ""
        Me.noref.maximum = 0
        Me.noref.MaxLength = 0
        Me.noref.minimum = 0
        Me.noref.Name = "noref"
        Me.noref.nbDecimals = CType(-1, Short)
        Me.noref.onlyAlphabet = False
        Me.noref.ReadOnly = True
        Me.noref.refuseAccents = False
        Me.noref.refusedChars = ""
        Me.noref.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.noref.showInternalContextMenu = True
        Me.noref.Size = New System.Drawing.Size(313, 20)
        Me.noref.TabIndex = 12
        Me.noref.trimText = False
        '
        'selectcode
        '
        Me.selectcode.BackColor = System.Drawing.SystemColors.Control
        Me.selectcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectcode.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.selectcode.Enabled = False
        Me.selectcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectcode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectcode.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectcode.Location = New System.Drawing.Point(143, 257)
        Me.selectcode.Name = "selectcode"
        Me.selectcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectcode.Size = New System.Drawing.Size(24, 24)
        Me.selectcode.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.selectcode, "Sélectionner le code du dossier")
        Me.selectcode.UseVisualStyleBackColor = False
        '
        'selectmedecin
        '
        Me.selectmedecin.BackColor = System.Drawing.SystemColors.Control
        Me.selectmedecin.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectmedecin.Enabled = False
        Me.selectmedecin.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectmedecin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectmedecin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectmedecin.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectmedecin.Location = New System.Drawing.Point(143, 232)
        Me.selectmedecin.Name = "selectmedecin"
        Me.selectmedecin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectmedecin.Size = New System.Drawing.Size(24, 24)
        Me.selectmedecin.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.selectmedecin, "Sélectionner le médecin")
        Me.selectmedecin.UseVisualStyleBackColor = False
        '
        'therapeute
        '
        Me.therapeute.acceptAlpha = True
        Me.therapeute.acceptedChars = Nothing
        Me.therapeute.acceptNumeric = True
        Me.therapeute.allCapital = False
        Me.therapeute.allLower = False
        Me.therapeute.autoComplete = True
        Me.therapeute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.therapeute.autoSizeDropDown = True
        Me.therapeute.BackColor = System.Drawing.SystemColors.ControlLight
        Me.therapeute.blockOnMaximum = False
        Me.therapeute.blockOnMinimum = False
        Me.therapeute.cb_AcceptLeftZeros = False
        Me.therapeute.cb_AcceptNegative = False
        Me.therapeute.currencyBox = False
        Me.therapeute.Cursor = System.Windows.Forms.Cursors.Default
        Me.therapeute.dbField = Nothing
        Me.therapeute.doComboDelete = True
        Me.therapeute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.therapeute.firstLetterCapital = False
        Me.therapeute.firstLettersCapital = False
        Me.therapeute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.therapeute.ForeColor = System.Drawing.SystemColors.WindowText
        Me.therapeute.itemsToolTipDuration = 10000
        Me.therapeute.Location = New System.Drawing.Point(143, 61)
        Me.therapeute.manageText = True
        Me.therapeute.matchExp = Nothing
        Me.therapeute.maximum = 0
        Me.therapeute.minimum = 0
        Me.therapeute.Name = "therapeute"
        Me.therapeute.nbDecimals = CType(-1, Short)
        Me.therapeute.onlyAlphabet = False
        Me.therapeute.pathOfList = Nothing
        Me.therapeute.ReadOnly = True
        Me.therapeute.refuseAccents = False
        Me.therapeute.refusedChars = Nothing
        Me.therapeute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.therapeute.showItemsToolTip = False
        Me.therapeute.Size = New System.Drawing.Size(312, 22)
        Me.therapeute.Sorted = True
        Me.therapeute.TabIndex = 3
        Me.therapeute.trimText = False
        '
        'region_Renamed
        '
        Me.region_Renamed.acceptAlpha = True
        Me.region_Renamed.acceptedChars = Nothing
        Me.region_Renamed.acceptNumeric = True
        Me.region_Renamed.allCapital = False
        Me.region_Renamed.allLower = False
        Me.region_Renamed.autoComplete = True
        Me.region_Renamed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.region_Renamed.autoSizeDropDown = True
        Me.region_Renamed.BackColor = System.Drawing.SystemColors.ControlLight
        Me.region_Renamed.blockOnMaximum = False
        Me.region_Renamed.blockOnMinimum = False
        Me.region_Renamed.cb_AcceptLeftZeros = False
        Me.region_Renamed.cb_AcceptNegative = False
        Me.region_Renamed.currencyBox = False
        Me.region_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.region_Renamed.dbField = "SiteLesion.SiteLesion"
        Me.region_Renamed.doComboDelete = True
        Me.region_Renamed.firstLetterCapital = True
        Me.region_Renamed.firstLettersCapital = False
        Me.region_Renamed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.region_Renamed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.region_Renamed.itemsToolTipDuration = 10000
        Me.region_Renamed.Location = New System.Drawing.Point(143, 37)
        Me.region_Renamed.manageText = True
        Me.region_Renamed.matchExp = ""
        Me.region_Renamed.maximum = 0
        Me.region_Renamed.minimum = 0
        Me.region_Renamed.Name = "region_Renamed"
        Me.region_Renamed.nbDecimals = CType(-1, Short)
        Me.region_Renamed.onlyAlphabet = False
        Me.region_Renamed.pathOfList = ""
        Me.region_Renamed.ReadOnly = True
        Me.region_Renamed.refuseAccents = False
        Me.region_Renamed.refusedChars = "-"
        Me.region_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.region_Renamed.showItemsToolTip = False
        Me.region_Renamed.Size = New System.Drawing.Size(312, 22)
        Me.region_Renamed.Sorted = True
        Me.region_Renamed.TabIndex = 2
        Me.region_Renamed.trimText = False
        '
        'dossier
        '
        Me.dossier.BackColor = System.Drawing.SystemColors.Window
        Me.dossier.Cursor = System.Windows.Forms.Cursors.Default
        Me.dossier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dossier.Enabled = True
        Me.dossier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dossier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dossier.Items.AddRange(New Object() {"* Nouveau dossier *"})
        Me.dossier.Location = New System.Drawing.Point(143, 13)
        Me.dossier.Name = "dossier"
        Me.dossier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dossier.Size = New System.Drawing.Size(312, 22)
        Me.dossier.TabIndex = 1
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.BackColor = System.Drawing.SystemColors.Control
        Me.label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.label15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label15.Location = New System.Drawing.Point(269, 363)
        Me.label15.Name = "label15"
        Me.label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label15.Size = New System.Drawing.Size(65, 14)
        Me.label15.TabIndex = 26
        Me.label15.Text = "Fréquence :"
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.BackColor = System.Drawing.SystemColors.Control
        Me.label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.label14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label14.Location = New System.Drawing.Point(8, 363)
        Me.label14.Name = "label14"
        Me.label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label14.Size = New System.Drawing.Size(42, 14)
        Me.label14.TabIndex = 26
        Me.label14.Text = "Durée :"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.SystemColors.Control
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(8, 336)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(68, 14)
        Me.label8.TabIndex = 26
        Me.label8.Text = "Remarques :"
        '
        '_Labels_9
        '
        Me._Labels_9.AutoSize = True
        Me._Labels_9.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_9.Location = New System.Drawing.Point(8, 285)
        Me._Labels_9.Name = "_Labels_9"
        Me._Labels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_9.Size = New System.Drawing.Size(116, 14)
        Me._Labels_9.TabIndex = 26
        Me._Labels_9.Text = "Numéro de référence :"
        '
        'codedossier
        '
        Me.codedossier.AutoSize = True
        Me.codedossier.BackColor = System.Drawing.SystemColors.Control
        Me.codedossier.Cursor = System.Windows.Forms.Cursors.Default
        Me.codedossier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codedossier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.codedossier.Location = New System.Drawing.Point(175, 262)
        Me.codedossier.Name = "codedossier"
        Me.codedossier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.codedossier.Size = New System.Drawing.Size(124, 14)
        Me.codedossier.TabIndex = 25
        Me.codedossier.Text = "Aucun code sélectionné"
        '
        '_Labels_8
        '
        Me._Labels_8.AutoSize = True
        Me._Labels_8.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_8.Location = New System.Drawing.Point(8, 261)
        Me._Labels_8.Name = "_Labels_8"
        Me._Labels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_8.Size = New System.Drawing.Size(92, 14)
        Me._Labels_8.TabIndex = 23
        Me._Labels_8.Text = "Code du dossier :"
        '
        'medecin
        '
        Me.medecin.AutoSize = True
        Me.medecin.BackColor = System.Drawing.SystemColors.Control
        Me.medecin.Cursor = System.Windows.Forms.Cursors.Default
        Me.medecin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.medecin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.medecin.Location = New System.Drawing.Point(175, 237)
        Me.medecin.Name = "medecin"
        Me.medecin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.medecin.Size = New System.Drawing.Size(140, 14)
        Me.medecin.TabIndex = 22
        Me.medecin.Text = "Aucun médecin sélectionné"
        '
        '_Labels_7
        '
        Me._Labels_7.AutoSize = True
        Me._Labels_7.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_7.Location = New System.Drawing.Point(8, 236)
        Me._Labels_7.Name = "_Labels_7"
        Me._Labels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_7.Size = New System.Drawing.Size(53, 14)
        Me._Labels_7.TabIndex = 20
        Me._Labels_7.Text = "Médecin :"
        '
        '_Labels_6
        '
        Me._Labels_6.AutoSize = True
        Me._Labels_6.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_6.Location = New System.Drawing.Point(8, 64)
        Me._Labels_6.Name = "_Labels_6"
        Me._Labels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_6.Size = New System.Drawing.Size(68, 14)
        Me._Labels_6.TabIndex = 16
        Me._Labels_6.Text = "Thérapeute :"
        '
        '_Labels_5
        '
        Me._Labels_5.AutoSize = True
        Me._Labels_5.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_5.Location = New System.Drawing.Point(8, 40)
        Me._Labels_5.Name = "_Labels_5"
        Me._Labels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_5.Size = New System.Drawing.Size(88, 14)
        Me._Labels_5.TabIndex = 13
        Me._Labels_5.Text = "Site de la lésion :"
        '
        '_Labels_4
        '
        Me._Labels_4.AutoSize = True
        Me._Labels_4.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_4.Location = New System.Drawing.Point(8, 136)
        Me._Labels_4.Name = "_Labels_4"
        Me._Labels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_4.Size = New System.Drawing.Size(63, 14)
        Me._Labels_4.TabIndex = 12
        Me._Labels_4.Text = "Diagnostic :"
        '
        '_Labels_2
        '
        Me._Labels_2.AutoSize = True
        Me._Labels_2.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_2.Location = New System.Drawing.Point(8, 16)
        Me._Labels_2.Name = "_Labels_2"
        Me._Labels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_2.Size = New System.Drawing.Size(94, 14)
        Me._Labels_2.TabIndex = 7
        Me._Labels_2.Text = "Dossier à joindre :"
        '
        'getTimeDate
        '
        Me.getTimeDate.BackColor = System.Drawing.SystemColors.Control
        Me.getTimeDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.getTimeDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.getTimeDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.getTimeDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.getTimeDate.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.getTimeDate.Location = New System.Drawing.Point(44, 467)
        Me.getTimeDate.Name = "getTimeDate"
        Me.getTimeDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.getTimeDate.Size = New System.Drawing.Size(24, 24)
        Me.getTimeDate.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.getTimeDate, "Sélectionner une date et une heure")
        Me.getTimeDate.UseVisualStyleBackColor = False
        '
        'adding
        '
        Me.adding.BackColor = System.Drawing.SystemColors.Control
        Me.adding.Cursor = System.Windows.Forms.Cursors.Default
        Me.adding.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.adding.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adding.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adding.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.adding.Location = New System.Drawing.Point(449, 467)
        Me.adding.Name = "adding"
        Me.adding.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adding.Size = New System.Drawing.Size(24, 24)
        Me.adding.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.adding, "Ajouter le rendez-vous")
        Me.adding.UseVisualStyleBackColor = False
        '
        'getNAM
        '
        Me.getNAM.BackColor = System.Drawing.SystemColors.Control
        Me.getNAM.Cursor = System.Windows.Forms.Cursors.Default
        Me.getNAM.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.getNAM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.getNAM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.getNAM.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.getNAM.Location = New System.Drawing.Point(65, 4)
        Me.getNAM.Name = "getNAM"
        Me.getNAM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.getNAM.Size = New System.Drawing.Size(24, 24)
        Me.getNAM.TabIndex = 1
        Me.getNAM.TabStop = False
        Me.ToolTip1.SetToolTip(Me.getNAM, "Rechercher un compte client")
        Me.getNAM.UseVisualStyleBackColor = False
        '
        'nam
        '
        Me.nam.acceptAlpha = True
        Me.nam.acceptedChars = ""
        Me.nam.acceptNumeric = True
        Me.nam.AcceptsReturn = True
        Me.nam.allCapital = True
        Me.nam.allLower = False
        Me.nam.BackColor = System.Drawing.SystemColors.Window
        Me.nam.blockOnMaximum = False
        Me.nam.blockOnMinimum = False
        Me.nam.cb_AcceptLeftZeros = False
        Me.nam.cb_AcceptNegative = False
        Me.nam.currencyBox = False
        Me.nam.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nam.firstLetterCapital = False
        Me.nam.firstLettersCapital = False
        Me.nam.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nam.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nam.Location = New System.Drawing.Point(273, 8)
        Me.nam.manageText = True
        Me.nam.matchExp = "AAAA111111X1"
        Me.nam.maximum = 0
        Me.nam.MaxLength = 12
        Me.nam.minimum = 0
        Me.nam.Name = "nam"
        Me.nam.nbDecimals = CType(-1, Short)
        Me.nam.onlyAlphabet = True
        Me.nam.refuseAccents = True
        Me.nam.refusedChars = ""
        Me.nam.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nam.showInternalContextMenu = True
        Me.nam.Size = New System.Drawing.Size(120, 20)
        Me.nam.TabIndex = 0
        Me.nam.trimText = False
        '
        'dhVisite
        '
        Me.dhVisite.AcceptsReturn = True
        Me.dhVisite.BackColor = System.Drawing.SystemColors.Window
        Me.dhVisite.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dhVisite.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dhVisite.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dhVisite.Location = New System.Drawing.Point(158, 467)
        Me.dhVisite.MaxLength = 0
        Me.dhVisite.Name = "dhVisite"
        Me.dhVisite.ReadOnly = True
        Me.dhVisite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dhVisite.Size = New System.Drawing.Size(96, 20)
        Me.dhVisite.TabIndex = 2
        Me.dhVisite.TabStop = False
        '
        '_Labels_3
        '
        Me._Labels_3.AutoSize = True
        Me._Labels_3.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_3.Location = New System.Drawing.Point(260, 470)
        Me._Labels_3.Name = "_Labels_3"
        Me._Labels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_3.Size = New System.Drawing.Size(56, 14)
        Me._Labels_3.TabIndex = 10
        Me._Labels_3.Text = "Période :"
        '
        '_Labels_0
        '
        Me._Labels_0.AutoSize = True
        Me._Labels_0.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_0.Location = New System.Drawing.Point(97, 8)
        Me._Labels_0.Name = "_Labels_0"
        Me._Labels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_0.Size = New System.Drawing.Size(174, 14)
        Me._Labels_0.TabIndex = 9
        Me._Labels_0.Text = "Numéro d'assurance maladie :"
        '
        '_Labels_1
        '
        Me._Labels_1.AutoSize = True
        Me._Labels_1.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_1.Location = New System.Drawing.Point(70, 470)
        Me._Labels_1.Name = "_Labels_1"
        Me._Labels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_1.Size = New System.Drawing.Size(87, 14)
        Me._Labels_1.TabIndex = 4
        Me._Labels_1.Text = "Date et heure :"
        '
        'menuaddvisitesearch
        '
        Me.menuaddvisitesearch.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menusearchdispo, Me.menuline, Me.menuSelectAgenda, Me.menusearchchoisir})
        '
        'menusearchdispo
        '
        Me.menusearchdispo.Index = 0
        Me.menusearchdispo.Text = "Disponibilités"
        '
        'menuline
        '
        Me.menuline.Index = 1
        Me.menuline.Text = "-"
        '
        'menuSelectAgenda
        '
        Me.menuSelectAgenda.Enabled = False
        Me.menuSelectAgenda.Index = 2
        Me.menuSelectAgenda.Text = "Agenda du thérapeute"
        '
        'menusearchchoisir
        '
        Me.menusearchchoisir.Index = 3
        Me.menusearchchoisir.Text = "Calendrier"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(8, 32)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(38, 14)
        Me.label1.TabIndex = 20
        Me.label1.Text = "Nom :"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(8, 48)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(61, 14)
        Me.label2.TabIndex = 21
        Me.label2.Text = "Adresse :"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.SystemColors.Control
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(240, 48)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(37, 14)
        Me.label3.TabIndex = 22
        Me.label3.Text = "Ville :"
        '
        'nom
        '
        Me.nom.AcceptsReturn = True
        Me.nom.BackColor = System.Drawing.SystemColors.Control
        Me.nom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.nom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nom.Location = New System.Drawing.Point(48, 32)
        Me.nom.MaxLength = 0
        Me.nom.Name = "nom"
        Me.nom.ReadOnly = True
        Me.nom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nom.Size = New System.Drawing.Size(192, 13)
        Me.nom.TabIndex = 23
        Me.nom.TabStop = False
        '
        'adresse
        '
        Me.adresse.AcceptsReturn = True
        Me.adresse.BackColor = System.Drawing.SystemColors.Control
        Me.adresse.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.adresse.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.adresse.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adresse.ForeColor = System.Drawing.SystemColors.WindowText
        Me.adresse.Location = New System.Drawing.Point(72, 48)
        Me.adresse.MaxLength = 0
        Me.adresse.Name = "adresse"
        Me.adresse.ReadOnly = True
        Me.adresse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adresse.Size = New System.Drawing.Size(160, 13)
        Me.adresse.TabIndex = 24
        Me.adresse.TabStop = False
        '
        'ville
        '
        Me.ville.AcceptsReturn = True
        Me.ville.BackColor = System.Drawing.SystemColors.Control
        Me.ville.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ville.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ville.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ville.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ville.Location = New System.Drawing.Point(280, 48)
        Me.ville.MaxLength = 0
        Me.ville.Name = "ville"
        Me.ville.ReadOnly = True
        Me.ville.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ville.Size = New System.Drawing.Size(168, 13)
        Me.ville.TabIndex = 25
        Me.ville.TabStop = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'tel
        '
        Me.tel.AcceptsReturn = True
        Me.tel.BackColor = System.Drawing.SystemColors.Control
        Me.tel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tel.Location = New System.Drawing.Point(312, 32)
        Me.tel.MaxLength = 0
        Me.tel.Name = "tel"
        Me.tel.ReadOnly = True
        Me.tel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tel.Size = New System.Drawing.Size(136, 13)
        Me.tel.TabIndex = 27
        Me.tel.TabStop = False
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.SystemColors.Control
        Me.label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label7.Location = New System.Drawing.Point(240, 32)
        Me.label7.Name = "label7"
        Me.label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label7.Size = New System.Drawing.Size(72, 14)
        Me.label7.TabIndex = 26
        Me.label7.Text = "Téléphone :"
        '
        'addvisite
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(482, 500)
        Me.Controls.Add(Me.ville)
        Me.Controls.Add(Me.tel)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me._Labels_3)
        Me.Controls.Add(Me._Labels_0)
        Me.Controls.Add(Me._Labels_1)
        Me.Controls.Add(Me.adminButton)
        Me.Controls.Add(Me.adresse)
        Me.Controls.Add(Me.nom)
        Me.Controls.Add(Me.nam)
        Me.Controls.Add(Me.dhVisite)
        Me.Controls.Add(Me.periode)
        Me.Controls.Add(Me.dossierFrame)
        Me.Controls.Add(Me.getTimeDate)
        Me.Controls.Add(Me.adding)
        Me.Controls.Add(Me.getNAM)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(208, 269)
        Me.MaximizeBox = False
        Me.Name = "addvisite"
        Me.ShowInTaskbar = False
        Me.Text = "Ajout d'un rendez-vous"
        Me.dossierFrame.ResumeLayout(False)
        Me.dossierFrame.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region "Definitions"

    Public Enum UsageModes
        AddingRV = 0
        AddingFolder = 1
    End Enum

    Private CTRP, _CurrentPeriode As String
    Private _CurrentDH As Date = LIMIT_DATE
    Private periodeArray() As Short = {15, 30, 15, 30, 45, 60, 75, 90, 105, 120}
    Private _LoadSearchWin As Boolean = False
    Private _CodeNoUnique, CFolder, _MedecinNo, _CurrentNoClient, _qlFrom As Integer
    Private namSelfOpened As Boolean = False
    Private dossiersFrequence() As Short
    Private _CodeNom As String = ""
    Private curCode As FolderCode
    Private usageMode As UsageModes = UsageModes.AddingRV

#End Region



#Region "Propriétés"
    Public Property loadSearchWin() As Boolean
        Get
            Return _LoadSearchWin
        End Get
        Set(ByVal Value As Boolean)
            _LoadSearchWin = Value
        End Set
    End Property

    Public Property qlFrom() As Integer
        Get
            Return _qlFrom
        End Get
        Set(ByVal Value As Integer)
            _qlFrom = Value
        End Set
    End Property

    Public Property currentNoClient(Optional ByVal userNAM As String = "") As Integer
        Get
            Return _CurrentNoClient
        End Get
        Set(ByVal Value As Integer)
            If userNAM <> "" And Value = 0 Then
                Dim noClient() As String = DBLinker.getInstance.readOneDBField("InfoClients", "NoClient", "WHERE ((NAM)='" & userNAM & "');")
                If noClient Is Nothing OrElse noClient.Length = 0 Then
                    _CurrentNoClient = 0
                    nam.Text = ""
                Else
                    _CurrentNoClient = (0)
                    nam.Text = userNAM
                End If
            Else
                _CurrentNoClient = Value
                If _CurrentNoClient > 0 Then
                    Dim theNAM() As String = DBLinker.getInstance.readOneDBField("InfoClients", "NAM", "WHERE ((NoClient)=" & Value & ");")
                    If Not theNAM Is Nothing AndAlso theNAM.Length <> 0 Then If theNAM(0) <> nam.Text Then nam.Text = theNAM(0)
                Else
                    nam.Text = ""
                End If
            End If

            checkNAMAndLoadFolders()
        End Set
    End Property

    Public Property currentDH() As Date
        Get
            Return _CurrentDH
        End Get
        Set(ByVal Value As Date)
            _CurrentDH = Value
            If _CurrentDH <> LIMIT_DATE Then dhVisite.Text = DateFormat.getTextDate(_CurrentDH, DateFormat.TextDateOptions.DDMMYYYY) & " " & DateFormat.getTextDate(_CurrentDH, DateFormat.TextDateOptions.ShortTime)
        End Set
    End Property

    Public Property currentPeriode() As String
        Get
            Return _CurrentPeriode
        End Get
        Set(ByVal Value As String)
            _CurrentPeriode = Value
            If currentPeriode <> "" Then periode.SelectedIndex = periode.FindStringExact(currentPeriode)
        End Set
    End Property

    Public Property currentTRP() As String
        Get
            Return CTRP
        End Get
        Set(ByVal Value As String)
            CTRP = Value
            If currentTRP = "" Then
                If therapeute.SelectedIndex < 0 Then therapeute.SelectedIndex = 0
            Else
                therapeute.SelectedIndex = therapeute.findString(currentTRP)
            End If
            If therapeute.SelectedIndex <> -1 Then menuSelectAgenda.Enabled = True
        End Set
    End Property

    Public Property currentFolder() As Integer
        Get
            Return CFolder
        End Get
        Set(ByVal Value As Integer)
            CFolder = Value
            If dossier.Items.Count > 1 Then
                For i As Integer = 1 To dossier.Items.Count - 1
                    If dossier.Items(i).ToString.StartsWith(Value & " - ") Then
                        dossier.SelectedIndex = i
                        Exit For
                    End If
                Next i
            End If
        End Set
    End Property

    Public Property medecinNo() As Integer
        Get
            Return _MedecinNo
        End Get
        Set(ByVal Value As Integer)
            _MedecinNo = Value
        End Set
    End Property

    Public Property codeNoUnique() As Integer
        Get
            Return _CodeNoUnique
        End Get
        Set(ByVal Value As Integer)
            _CodeNoUnique = Value
        End Set
    End Property

    Public Property codeNom() As String
        Get
            Return _CodeNom
        End Get
        Set(ByVal Value As String)
            _CodeNom = Value
        End Set
    End Property
#End Region

#Region "Context menu"
    Public Sub menusearchchoisir_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menusearchchoisir.Click
        Dim MyDT(), sDate() As String
        Dim cd As String = ""
        Dim adc As Boolean = True
        Dim myDate As Date = Nothing
        Dim curHoraire As Schedule = SchedulesManager.getInstance.getSchedule(0, LIMIT_DATE)
        If curHoraire Is Nothing Then
            If MessageBox.Show("L'horaire par défaut pour la clinique n'existe pas." & vbCrLf & "Voulez-vous en créer une ?", "Aucune horaire par défaut", MessageBoxButtons.YesNo) = DialogResult.No Then
                Exit Sub
            Else
                openModifHoraire(0)
                Exit Sub
            End If
        End If

        Dim myDateChoice As New DateChoice()
        If dhVisite.Text <> "" Then
            MyDT = dhVisite.Text.Split(New Char() {" "})
            sDate = MyDT(0).Split(New Char() {"/"})
            myDate = New Date(sDate(2), sDate(1), sDate(0))
        End If
        'REM_CODES
        If curCode IsNot Nothing Then cd = curCode.name : adc = False
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year, Date.Today.Year + 1, , , True, , , True, Date.Today, therapeute.GetItemText(therapeute.SelectedItem), periodeArray(periode.SelectedIndex), currentNoClient, myDate, , , adc, cd)

        If dateReturn.Count <> 0 Then dhVisite.Text = DateFormat.getTextDate(dateReturn(0))
    End Sub

    Private Sub menusearchdispo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menusearchdispo.Click
        Dim choices As String = "", MyPref As String = ""
        Dim myUpTo As Short
        Dim trPs As Generic.List(Of User) = UsersManager.getInstance.getUsers(False, True)
        For Each curUser As User In trPs
            If curUser.isOfferingService(service.Text) Then choices &= "§" & curUser.toString()
        Next
        choices = "* Tous les thérapeutes *" & choices

        Dim myMultiChoice As New multichoice()
        Dim myTRP As String = myMultiChoice.GetChoice("Veuillez choisir le thérapeute", choices, , "§", , therapeute.GetItemText(therapeute.SelectedItem))
        If myTRP.StartsWith("ERROR") Then Exit Sub
        Dim noTRP As Integer = 0
        If myTRP.StartsWith("*") = False Then
            myTRP = myTRP.Substring(myTRP.IndexOf("(") + 1)
            myTRP = myTRP.Substring(0, myTRP.Length - 1)
            noTRP = myTRP
        End If

        myMultiChoice = New multichoice()
        choices = "1 semaine§2 semaines§3 semaines§4 semaines"
        If PreferencesManager.getUserPreferences()("FindPlacesUpTo") <> "" Then MyPref = PreferencesManager.getUserPreferences()("FindPlacesUpTo")
        myUpTo = myMultiChoice.GetChoice("Rechercher sur...", choices, "INDEX", "§", , MyPref)
        If myUpTo < 0 Then Exit Sub
        myUpTo += 1

        Dim myDisponibilites As New Disponibilities()
        Dim myDispo As String = myDisponibilites.GetDisponisibilites
        If myDispo = "" Then Exit Sub

        Dim folderInfos() As String = dossier.Text.Split(New String() {" - "}, StringSplitOptions.None)
        Dim noFolder As Integer = If(dossier.SelectedIndex = 0, 0, CInt(folderInfos(0)))

        Dim myCodeUnique As Integer = 0
        If curCode IsNot Nothing Then myCodeUnique = curCode.noUnique
        Dim foundPlaces As Generic.List(Of AgendaPlace) = AgendaManager.getInstance.findPlaces(noTRP, myDispo, periodeArray(periode.SelectedIndex), myCodeUnique, currentNoClient, myUpTo, , service.Text, noFolder)
        If foundPlaces.Count = 0 Then MessageBox.Show("Aucune plage disponible", "Ajout d'un rendez-vous") : Exit Sub

        Dim askPlaces As String = ""
        For Each curPlace As AgendaPlace In foundPlaces
            If askPlaces <> "" Then askPlaces &= "§"
            askPlaces &= curPlace.toString()
        Next
        myMultiChoice = New multichoice()
        Dim myChoice As String = myMultiChoice.GetChoice("Veuillez choisir l'emplacement désiré", askPlaces, , "§", , foundPlaces(0).toString())
        If myChoice.StartsWith("ERROR") Then Exit Sub

        Dim sMyChoice() As String = Split(myChoice, " ")
        Dim myDate As Date = CDate(sMyChoice(0))
        dhVisite.Text = myDate.Day & "/" & myDate.Month & "/" & myDate.Year & " " & sMyChoice(2)

        If sMyChoice(3).StartsWith("*") = False Then therapeute.SelectedIndex = therapeute.FindStringExact(sMyChoice(3) & " " & sMyChoice(4))
    End Sub

    Private Sub menuSelectAgenda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSelectAgenda.Click
        myMainWin.newAgenda(CType(therapeute.SelectedItem, User).noUser)
    End Sub
#End Region

    Public Sub setNAM(ByVal nam As String)
        Me.nam.Text = nam
    End Sub

    Private Sub loadTRP(ByVal all As Boolean)
        therapeute.Items.Clear()
        askedTRP.Items.Clear()
        trpToTransfer.Items.Clear()

        'Load Thérapeute
        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(all, True)
        askedTRP.Items.Add(New UserGeneral("* Aucun thérapeute demandé *"))
        trpToTransfer.Items.Add(New UserGeneral("* Aucun thérapeute à transférer *"))
        therapeute.Items.AddRange(users.ToArray)
        askedTRP.Items.AddRange(users.ToArray)
        trpToTransfer.Items.AddRange(users.ToArray)

        askedTRP.SelectedIndex = 0
        trpToTransfer.SelectedIndex = 0
    End Sub

    Private Sub adding_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adding.Click
        'REM_CODES
        Dim folderInfos(), SVDate(), TC, DemandedTRP, TRPTransfer, DateRef, DateRec, dateAcc As String
        Dim myFrequence As Short
        Dim NoUser, noFolder As Integer
        Dim vDate As Date
        Dim reOpen As Boolean = False
        Dim vEval As Boolean = False
        Dim selfOpened As Boolean = False
        If dossierFrame.Enabled = False Then MessageBox.Show("Veuillez sélectionner un client", "Client non sélectionné") : Exit Sub
        If region_Renamed.Text = "" Then MessageBox.Show("Veuillez entrer un site de lésion", "Site de lésion non-entré") : Exit Sub
        If curCode Is Nothing Then MessageBox.Show("Veuillez sélectionner un code pour le nouveau dossier", "Code du dossier") : selectcode.Focus() : Exit Sub

        NoUser = User.extractNo(therapeute.Text)

        If dossier.SelectedIndex = 0 And curCode.confirmReference = True And noref.Text = "" Then If MessageBox.Show("Avez-vous oublié d'entrer le numéro de référence ?", "Numéro de référence", MessageBoxButtons.YesNo) = DialogResult.Yes Then noref.Focus() : Exit Sub
        If dossier.SelectedIndex = 0 And curCode.confirmDiagnostic = True And Diagnostic.Text = "" Then If MessageBox.Show("Avez-vous oublié d'entrer le diagnostic ?", "Diagnostic", MessageBoxButtons.YesNo) = DialogResult.Yes Then Diagnostic.Focus() : Exit Sub

        If usageMode = UsageModes.AddingRV Then
            If periode.SelectedIndex = -1 Then MessageBox.Show("Veuillez sélectionner une période pour le rendez-vous", "Période du rendez-vous") : periode.Focus() : Exit Sub
            If dhVisite.Text = "" Then MessageBox.Show("Veuillez sélectionner la date et l'heure de la visite", "Date et heure non-entrés") : Exit Sub

            SVDate = dhVisite.Text.Substring(0, dhVisite.Text.Length - 6).Split(New Char() {"/"})
            vDate = CDate(SVDate(2) & "/" & SVDate(1) & "/" & SVDate(0) & " " & dhVisite.Text.Substring(dhVisite.Text.Length - 5))
            folderInfos = dossier.Text.Split(New String() {" - "}, StringSplitOptions.None)
            noFolder = If(dossier.SelectedIndex = 0, 0, CInt(folderInfos(0)))
        End If

        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        If usageMode = UsageModes.AddingRV Then
            'Vérification s'il y a déjà une visite à cette heure & date
            Try
                Dim checkConflictOptions As AgendaManager.TimeConflictOptions = AgendaManager.TimeConflictOptions.AcceptMultipleCodes Or AgendaManager.TimeConflictOptions.VerifySchedule Or AgendaManager.TimeConflictOptions.VerifyAbsences Or AgendaManager.TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime
                TC = AgendaManager.getInstance.checkTimeConflict(vDate, periodeArray(periode.SelectedIndex), User.extractNo(therapeute.Text), checkConflictOptions, currentNoClient, curCode.noUnique, , , , noFolder)
            Catch ex As Exception
                TC = "Impossible d'ajouter le rendez-vous dû à une petite erreur logiciel en cours de correction. Veuillez fermer la fenêtre d'ajout de rendez-vous et recommencer. Merci de votre compréhension !"
                addErrorLog(New Exception("Added an if on Periode.SelectedIndex at beginning of function, shouldn't get error anymore..PeriodeArray.GetUpperBound(0)=" & periodeArray.GetUpperBound(0) & ",Periode.SelectedIndex=" & periode.SelectedIndex, ex))
            End Try
            If Not TC = "" Then MessageBox.Show(TC, "Impossible d'ajouter le rendez-vous") : Exit Sub

            'Réouverture du dossier
            If Microsoft.VisualBasic.Right(dossier.Text, 7) = "(Inactif)" Then
                If MessageBox.Show("Voulez-vous réactiver le dossier #" & noFolder, "Réouverture de dossier", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
                ClientFolder.changeStatus(FoldersStatus.FolderPossibleStatuses.Inactive, FoldersStatus.FolderPossibleStatuses.Active, currentNoClient, noFolder)
            End If
        End If

        adding.Enabled = False

        Dim updateUserAlerts As Boolean = False

        'Création, si nouveau dossier, de son répertoire spécifique.
        'Attribution de la variable MyNo en tant que numéro du dossier courant
        If dossier.SelectedIndex = 0 Then
            Dim dateAccidentOblig As Boolean = False
            Dim dateReferenceOblig As Boolean = False
            Dim dateRechuteOblig As Boolean = False

            'Vérifie si une date est requise
            Dim curFCAs As Generic.List(Of FolderAlertType) = curCode.folderAlertTypes
            For Each curFCA As FolderAlertType In curFCAs
                Select Case curFCA.startingDate_Type
                    Case FolderAlertType.StartingDateTypes.OnDateAccident
                        dateAccidentOblig = True
                    Case FolderAlertType.StartingDateTypes.OnDateRechute
                        dateRechuteOblig = True
                    Case FolderAlertType.StartingDateTypes.OnDateReferencce
                        dateReferenceOblig = True
                End Select
            Next

            If dateReference.Text.StartsWith("A") Then
                If dateReferenceOblig Then
                    MessageBox.Show("La date de référence est obligatoire.", "Date de référence", MessageBoxButtons.OK)
                    selectDate.Focus()
                    Exit Sub
                End If
                DateRef = "null"
            Else
                DateRef = "'" & dateReference.Text & "'"
            End If
            If dateAccident.Text.StartsWith("A") Then
                If dateReferenceOblig Then
                    MessageBox.Show("La date d'accident est obligatoire.", "Date d'accident", MessageBoxButtons.OK)
                    selectDateAccident.Focus()
                    Exit Sub
                End If
                dateAcc = "null"
            Else
                dateAcc = "'" & dateAccident.Text & "'"
            End If
            If dateRechute.Text.StartsWith("A") Then
                If dateRechuteOblig Then
                    MessageBox.Show("La date de rechute est obligatoire.", "Date de rechute", MessageBoxButtons.OK)
                    selectDateRechute.Focus()
                    Exit Sub
                End If
                DateRec = "null"
            Else
                DateRec = "'" & dateRechute.Text & "'"
            End If

            If askedTRP.SelectedIndex = 0 Then
                DemandedTRP = "null"
            Else
                DemandedTRP = User.extractNo(askedTRP.Text)
            End If

            If trpToTransfer.SelectedIndex = 0 Then
                TRPTransfer = "null"
            Else
                TRPTransfer = User.extractNo(trpToTransfer.Text)
            End If

            If DBLinker.getInstance.writeDB("InfoFolders", "NoClient, Diagnostic, NoSiteLesion, NoTRPTraitant, StatutOuvert, NoKP, NoRef, Service, DateRef, NoTRPDemande, Remarques,DateAccident,DateRechute,Frequence,Duree,NoTRPToTransfer, NoCodeDate,NoCodeUnique,NoCodeUser, ExternalStatus", currentNoClient & ",'" & Diagnostic.Text.Replace("'", "''") & "'," & DBHelper.addItemToADBList("SiteLesion", "SiteLesion", region_Renamed.Text, "NoSiteLesion") & "," & NoUser & ",'True'," & IIf(medecinNo = 0, "null", medecinNo) & ",'" & noref.Text.Replace("'", "''") & "','" & service.Text.Replace("'", "''") & "'," & DateRef & "," & DemandedTRP & ",'" & remarques.Text.Replace("'", "''") & "'," & dateAcc & "," & DateRec & "," & frequence.SelectedIndex & "," & duree.SelectedIndex & "," & TRPTransfer & ",'" & DateFormat.getTextDate(Date.Today) & "'," & curCode.noUnique & "," & NoUser & "," & curCode.startingExternalStatus, , , , noFolder) = False Then Exit Sub

            'Création des FolderTextes requis
            Dim curFTTs As Generic.List(Of FolderTextType) = curCode.folderTexteTypes()
            Dim multiple As String = ""
            For Each curFTT As FolderTextType In curFTTs
                If curFTT.multiple Then
                    multiple = " 1"
                Else
                    multiple = ""
                End If
                updateUserAlerts = updateUserAlerts OrElse curFTT.showAlert
                Dim acceptedText As Boolean = curFTT.whenToBeCreated = FolderTextType.WhenToBeCreate.OnDayX Or curFTT.whenToBeCreated = FolderTextType.WhenToBeCreate.OnFolderCreation
                If curFTT.isActive AndAlso acceptedText Then FolderText.add(curFTT.noFolderTexteType, currentNoClient, noFolder, curFTT.textTitle & multiple, Date.Today.AddDays(curFTT.nbDaysX), 1, NoUser, Me.nom.Text)
            Next

            'Création des Alertes requises
            For Each curFCA As FolderAlertType In curFCAs
                Dim acceptedAlert As Boolean = curFCA.startingDate_Type = FolderAlertType.StartingDateTypes.OnDateAccident OrElse curFCA.startingDate_Type = FolderAlertType.StartingDateTypes.OnDateRechute OrElse curFCA.startingDate_Type = FolderAlertType.StartingDateTypes.OnDateReferencce OrElse curFCA.startingDate_Type = FolderAlertType.StartingDateTypes.OnFolderCreation
                Dim affDate As Date = Date.Today
                Select Case curFCA.startingDate_Type
                    Case FolderAlertType.StartingDateTypes.OnDateAccident
                        affDate = CDate(dateAccident.Text)
                    Case FolderAlertType.StartingDateTypes.OnDateRechute
                        affDate = CDate(dateRechute.Text)
                    Case FolderAlertType.StartingDateTypes.OnDateReferencce
                        affDate = CDate(dateReference.Text)
                End Select
                If acceptedAlert Then FolderAlert.add(curFCA.noFolderAlertType, currentNoClient, noFolder, affDate.AddDays(curFCA.startingDate_NbDaysX - curFCA.alertNbDaysDiff), NoUser)
            Next

            DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient", "13," & noFolder & "," & currentNoClient)
            myFrequence = Me.frequence.SelectedIndex
            vEval = Not periode.Text.ToLower.StartsWith("trai")
        Else
            myFrequence = dossiersFrequence(dossier.SelectedIndex - 1)
            If periode.Text.ToLower.StartsWith("éval") Then vEval = True
        End If

        If usageMode = UsageModes.AddingRV Then
            Dim myNoVisite As Integer = RendezVousManager.getInstance.addRendezVous(vDate, CDate(Microsoft.VisualBasic.Right(dhVisite.Text, 5)), periodeArray(periode.SelectedIndex), NoUser, currentNoClient, noFolder, service.Text, myFrequence, vEval)
            If myNoVisite = 0 Then
                If selfOpened = True Then DBLinker.getInstance().dbConnected = False
                adding.Enabled = True
                Exit Sub
            End If
        End If

        If dossier.SelectedIndex = 0 Then InternalUpdatesManager.getInstance.sendUpdate("AccountsDossiers(" & currentNoClient & ")")
        If updateUserAlerts Then AlertsManager.sendUpdate(NoUser)

        If usageMode = UsageModes.AddingRV Then updateVisites(currentNoClient, noFolder, vDate, , , NoUser)

        If usageMode = UsageModes.AddingRV AndAlso qlFrom <> 0 Then DBLinker.getInstance.delDB("ListeAttente", "NoQL", qlFrom, False)
        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        Me.Close()

        If usageMode = UsageModes.AddingFolder AndAlso PreferencesManager.getUserPreferences()("openClientAccountOnNewFolderWORV") = True Then openAccount(_CurrentNoClient)
    End Sub

    Private Sub adminButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adminButton.Click
        nam.Text = randomClient()
    End Sub

    Private Sub dossier_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dossier.SelectedIndexChanged
        'REM_CODES
        Dim infoFolders(,) As String
        Dim selfOpened As Boolean = False

        If dossier.SelectedIndex = 0 Then
            medecinNo = 0
            noref.ReadOnly = False
            selectmedecin.Enabled = True
            selectDate.Enabled = True
            selectDateAccident.Enabled = True
            selectDateRechute.Enabled = True
            selectcode.Enabled = True
            region_Renamed.ReadOnly = False
            askedTRP.ReadOnly = False
            trpToTransfer.ReadOnly = False
            remarques.ReadOnly = False
            duree.ReadOnly = False
            frequence.ReadOnly = False
            region_Renamed.Text = ""
            Diagnostic.Text = ""
            remarques.Text = ""
            dateReference.Text = "Aucune date sélectionnée"
            dateAccident.Text = "Aucune date sélectionnée"
            dateRechute.Text = "Aucune date sélectionnée"
            medecin.Text = "Aucun médecin sélectionné"
            codedossier.Text = "Aucun code sélectionné"
            curCode = Nothing
            frequence.SelectedIndex = 0
            duree.SelectedIndex = 0
            noref.Text = ""
            changeDefaultPeriode(15, 30)
            periode.SelectedIndex = 1
            Diagnostic.ReadOnly = False
            askedTRP.SelectedIndex = 0
            loadTRP(False)
            If currentTRP <> "" Then therapeute.SelectedIndex = therapeute.FindStringExact(currentTRP)
        Else
            loadTRP(True)
            askedTRP.ReadOnly = True
            trpToTransfer.ReadOnly = True
            region_Renamed.ReadOnly = True
            Diagnostic.ReadOnly = True
            selectDate.Enabled = False
            selectDateAccident.Enabled = False
            selectDateRechute.Enabled = False
            selectmedecin.Enabled = False
            selectcode.Enabled = False
            noref.ReadOnly = True
            remarques.ReadOnly = True
            frequence.ReadOnly = True
            duree.ReadOnly = True

            If DBLinker.getInstance().dbConnected = False Then selfOpened = True : DBLinker.getInstance().dbConnected = True
            Dim noFolder() As String = dossier.GetItemText(dossier.SelectedItem).Split(New String() {" - "}, StringSplitOptions.None)
            infoFolders = DBLinker.getInstance.readDB("Utilisateurs INNER JOIN (SiteLesion RIGHT JOIN InfoFolders ON SiteLesion.NoSiteLesion = InfoFolders.NoSiteLesion) ON Utilisateurs.NoUser = InfoFolders.NoTRPTraitant", "InfoFolders.Diagnostic, SiteLesion.SiteLesion, Utilisateurs.Nom+','+Utilisateurs.Prenom + ' (' + CAST(Utilisateurs.NoUser AS VARCHAR(MAX)) + ')' AS TRP, InfoFolders.NoKP, InfoFolders.NoCodeUnique, InfoFolders.NoRef, InfoFolders.Service, InfoFolders.DateRef, InfoFolders.NoTRPDemande, InfoFolders.Remarques,InfoFolders.DateAccident,InfoFolders.DateRechute,InfoFolders.Frequence,InfoFolders.Duree,InfoFolders.NoTRPToTransfer,InfoFolders.NoTRPTraitant, NoCodeUser,NoCodeDate", "WHERE ((InfoFolders.NoFolder)=" & noFolder(0) & ")")
            If Not infoFolders Is Nothing AndAlso infoFolders.Length <> 0 Then
                Diagnostic.Text = infoFolders(0, 0)
                region_Renamed.Text = infoFolders(1, 0)
                If currentTRP <> "" Then
                    therapeute.SelectedIndex = therapeute.FindStringExact(currentTRP)
                Else
                    therapeute.SelectedIndex = therapeute.FindStringExact(infoFolders(2, 0))
                    If therapeute.SelectedIndex < 0 Then
                        If therapeute.Items.Count <> 0 Then therapeute.SelectedIndex = 0
                        MessageBox.Show("Le thérapeute du dossier n'existe plus dans la liste des thérapeutes. Le premier thérapeute dans la liste a été sélectionné.", "Thérapeute inexistant")
                    End If
                End If
                Try
                    Dim curMedecin As String = ""
                    If infoFolders(3, 0) <> "" AndAlso infoFolders(3, 0) > 0 Then
                        Dim medecinName() As String = DBLinker.getInstance.readOneDBField("KeyPeople", "Nom", "WHERE ((NoKP)=" & infoFolders(3, 0) & ");")
                        If medecinName IsNot Nothing AndAlso medecinName.Length <> 0 Then curMedecin = medecinName(0)
                        medecinNo = infoFolders(3, 0)
                    End If
                    If curMedecin = "" Then
                        medecin.Text = "Aucun médecin sélectionné"
                        medecinNo = 0
                    Else
                        medecin.Text = curMedecin
                    End If
                Catch ex As Exception
                    medecin.Text = "Aucun médecin sélectionné"
                    medecinNo = 0
                    addErrorLog(New Exception("InfoFolders(3, 0)=" & infoFolders(3, 0), ex))
                End Try
                curCode = FolderCodesManager.getInstance.getItemable(Integer.Parse(infoFolders(4, 0)), If(infoFolders(16, 0).Equals(""), 0, Integer.Parse(infoFolders(16, 0))), Date.Parse(infoFolders(17, 0)))
                _CodeNoUnique = curCode.noUnique
                _CodeNom = curCode.name

                Dim trp As User = UsersManager.getInstance.getUser(curCode.noUser)
                If trp Is Nothing Then trp = UserDefault.getInstance()

                codedossier.Text = trp.toString & ":" & curCode.name
                Try
                    changeDefaultPeriode(curCode)
                Catch ex As Exception
                    addErrorLog(New Exception("NoFolder=" & noFolder(0) & vbCrLf & "InfoFolders(15, 0)=" & infoFolders(15, 0) & vbCrLf & "InfoFolders(4, 0)=" & infoFolders(4, 0), ex))
                End Try
                noref.Text = infoFolders(5, 0)
                service.Text = infoFolders(6, 0)
                If service.SelectedIndex < 0 And service.Items.Count > 0 Then service.SelectedIndex = 0
                If infoFolders(7, 0) <> "" Then
                    dateReference.Text = DateFormat.getTextDate(infoFolders(7, 0))
                Else
                    dateReference.Text = "Aucune date sélectionnée"
                End If
                If infoFolders(8, 0) <> "" AndAlso infoFolders(8, 0) > 0 Then
                    Dim tosetTRP As User = UsersManager.getInstance.getUser(infoFolders(8, 0))
                    If tosetTRP IsNot Nothing Then
                        askedTRP.SelectedIndex = askedTRP.Items.IndexOf(tosetTRP)
                        If askedTRP.SelectedIndex = -1 Then askedTRP.SelectedIndex = 0
                    Else
                        askedTRP.SelectedIndex = 0
                    End If
                Else
                    askedTRP.SelectedIndex = 0
                End If

                remarques.Text = infoFolders(9, 0)

                If infoFolders(10, 0) <> "" Then
                    dateAccident.Text = DateFormat.getTextDate(infoFolders(10, 0))
                Else
                    dateAccident.Text = "Aucune date sélectionnée"
                End If
                If infoFolders(11, 0) <> "" Then
                    dateRechute.Text = DateFormat.getTextDate(infoFolders(11, 0))
                Else
                    dateRechute.Text = "Aucune date sélectionnée"
                End If

                Dim intTested As Integer
                If Integer.TryParse(infoFolders(12, 0), intTested) Then frequence.SelectedIndex = infoFolders(12, 0) Else frequence.SelectedIndex = -1
                If Integer.TryParse(infoFolders(13, 0), intTested) Then duree.SelectedIndex = infoFolders(13, 0) Else duree.SelectedIndex = 0

                If infoFolders(14, 0) <> "" AndAlso infoFolders(14, 0) > 0 Then
                    Dim tosetTRP As User = UsersManager.getInstance.getUser(infoFolders(14, 0))
                    If tosetTRP IsNot Nothing Then
                        trpToTransfer.SelectedIndex = askedTRP.Items.IndexOf(tosetTRP)
                        If trpToTransfer.SelectedIndex = -1 Then askedTRP.SelectedIndex = 0
                    Else
                        trpToTransfer.SelectedIndex = 0
                    End If
                Else
                    trpToTransfer.SelectedIndex = 0
                End If
            End If
            If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            Try
                periode.SelectedIndex = 0
            Catch ex As Exception
                addErrorLog(New Exception("Periode.Items.Count=" & periode.Items.Count, ex))
            End Try
        End If

        If therapeute.SelectedIndex < 0 Then therapeute.SelectedIndex = 0
        If therapeute.SelectedIndex <> -1 Then menuSelectAgenda.Enabled = True
    End Sub

    Private Sub addvisite_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub addvisite_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        If currentUserName = "Administrateur" Then dhVisite.ReadOnly = False : adminButton.Visible = True

        If PreferencesManager.getUserPreferences()("AutoOpenSearchIfNAMEmpty") = True And currentNoClient = 0 Then loadSearchWin = True ' : getNAM_Click(Me, eventArgs.Empty)
    End Sub

    Private Sub addvisite_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        currentTRP = ""
    End Sub

    Private Sub getNAM_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles getNAM.Click
        Dim myRecherche As clientSearch = New clientSearch()
        myRecherche.from = Me
        myRecherche.Visible = False
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()
    End Sub

    Private Sub getTimeDate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles getTimeDate.Click
        menusearchdispo.Enabled = Not (therapeute.Items.Count = 0 OrElse curCode Is Nothing OrElse dossierFrame.Enabled = False)

        menuaddvisitesearch.Show(getTimeDate, New Point(0, 0))
    End Sub

    Private previousNAM As String = ""

    Private Sub nam_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nam.KeyUp
        If previousNAM = String.Empty AndAlso nam.Text = String.Empty Then Exit Sub

        previousNAM = nam.Text
        _CurrentNoClient = 0
        checkNAMAndLoadFolders(False)
        If usageMode = UsageModes.AddingRV AndAlso dossierFrame.Enabled Then dossier.SelectedIndex = dossier.Items.Count - 1
    End Sub

    Private Sub checkNAMAndLoadFolders(Optional ByVal doSelection As Boolean = True)
        If nam.Text = "" AndAlso currentNoClient = 0 Then
            resetWindow()
            Exit Sub
        End If

        'REM_CODES
        periodeArray(0) = 15 : periodeArray(1) = 30

        Dim Phones(), ouverture As String
        Dim i As Integer
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim where As String = ""
        If nam.Text = "" Then
            where = "NoClient = " & currentNoClient
        Else
            where = "(NAM)='" & nam.Text & "'"
        End If
        Dim clientInfo(,) As String = DBLinker.getInstance.readDB("InfoClients LEFT JOIN Villes ON InfoClients.NoVille=Villes.NoVille", "Nom, Prenom, Adresse, NomVille, Telephones, NoClient", "WHERE " & where)

        If Not clientInfo Is Nothing AndAlso clientInfo.Length <> 0 Then
            nom.Text = clientInfo(0, 0) & "," & clientInfo(1, 0)
            adresse.Text = clientInfo(2, 0)
            ville.Text = clientInfo(3, 0)
            If clientInfo(4, 0) <> "" Then
                Phones = clientInfo(4, 0).Split(New Char() {"§"})
                tel.Text = Phones(0)
            End If
            Me._CurrentNoClient = clientInfo(5, 0)

            If dossierFrame.Enabled = False Then
                dossierFrame.Enabled = True
                'dossier.Enabled = True
                Diagnostic.ReadOnly = False
                region_Renamed.ReadOnly = False
                therapeute.ReadOnly = False
                selectmedecin.Enabled = True
                selectcode.Enabled = True
                selectDate.Enabled = True
                service.ReadOnly = False
                askedTRP.ReadOnly = False
                remarques.ReadOnly = False
            Else
                resetDossierField()
            End If

            Dim infoFolders(,) As String = DBLinker.getInstance.readDB("SiteLesion RIGHT JOIN InfoFolders ON SiteLesion.NoSiteLesion = InfoFolders.NoSiteLesion", "InfoFolders.NoFolder, SiteLesion.SiteLesion, InfoFolders.NoCodeUnique, InfoFolders.StatutOuvert, InfoFolders.Frequence, InfoFolders.NoCodeUser, InfoFolders.NoCodeDate, InfoFolders.Service", "WHERE ((InfoFolders.NoClient)=" & currentNoClient & ");")
            If Not infoFolders Is Nothing AndAlso infoFolders.Length <> 0 Then
                ReDim dossiersFrequence(infoFolders.GetUpperBound(1))
                For i = 0 To infoFolders.GetUpperBound(1)
                    Dim curCode As FolderCode = FolderCodesManager.getInstance.getItemable(Integer.Parse(infoFolders(2, i)), If(infoFolders(5, i) = "", 0, Integer.Parse(infoFolders(5, i))), Date.Parse(infoFolders(6, i)))
                    ouverture = "Inactif"
                    If infoFolders(3, i) = True Then ouverture = "Actif"
                    dossier.Items.Add(infoFolders(0, i) & " - " & infoFolders(1, i) & " - " & infoFolders(7, i) & " (" & curCode.name & ") (" & ouverture & ")")
                    If infoFolders(4, i) <> "" Then
                        dossiersFrequence(i) = infoFolders(4, i)
                    Else
                        dossiersFrequence(i) = 0
                    End If
                Next i
                If usageMode = UsageModes.AddingRV AndAlso doSelection Then dossier.SelectedIndex = dossier.Items.Count - 1

                If curCode IsNot Nothing Then changeDefaultPeriode(curCode)
            End If

            If usageMode = UsageModes.AddingRV AndAlso doSelection AndAlso currentFolder < 0 Then
                'Si aucun dossier de présélectionné, alors choisit le dernier si
                'le CodeNo = au CodeNo du dernier dossier, sinon choisit un Nouveau dossier
                'en affichant le code préselectionné
                dossier.SelectedIndex = dossier.Items.Count - 1
                Dim newCodeText As String = ""
                Dim myCode As FolderCode
                Dim noUser As Integer = CType(therapeute.SelectedItem, User).noUser
                If codeNoUnique > 0 Then
                    Try
                        myCode = FolderCodesManager.getInstance.getItemable(codeNoUnique, noUser, Date.Today)
                    Catch ex As Exception
                        'Testing null exception of myCode
                        Throw New Exception("codeNoUnique=" & codeNoUnique & ", noUser=" & noUser & ", Date.Today=" & DateFormat.getTextDate(Date.Today), ex)
                    End Try
                    If myCode Is Nothing Then
                        'Testing null exception of myCode
                        Throw New Exception("codeNoUnique=" & codeNoUnique & ", noUser=" & noUser & ", Date.Today=" & DateFormat.getTextDate(Date.Today))
                    End If

                    Dim trp As User = UsersManager.getInstance.getUser(myCode.noUser)
                    If trp Is Nothing Then trp = UserDefault.getInstance()
                    newCodeText = trp.toString & ":" & myCode.name
                End If

                If codeNom <> "" Then
                    myCode = FolderCodesManager.getInstance.getItemable(codeNom, noUser, Date.Today)
                    Dim trp As User = UsersManager.getInstance.getUser(myCode.noUser)
                    If trp Is Nothing Then trp = UserDefault.getInstance()
                    newCodeText = trp.toString() & ":" & myCode.name
                End If

                If newCodeText <> "" And newCodeText <> codedossier.Text Then
                    dossier.SelectedIndex = 0
                    codedossier.Text = newCodeText
                    curCode = myCode
                    changeDefaultPeriode(myCode)
                End If
            ElseIf usageMode = UsageModes.AddingRV AndAlso doSelection Then
                For i = 0 To dossier.Items.Count - 1
                    If dossier.Items(i).ToString.StartsWith(currentFolder & "-") Then dossier.SelectedIndex = i
                Next i
            ElseIf usageMode = UsageModes.AddingFolder Then
                dossier.SelectedIndex = 0
            End If
        Else
            resetWindow()
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
    End Sub

    Private Sub resetWindow()
        nom.Text = ""
        adresse.Text = ""
        ville.Text = ""
        tel.Text = ""
        resetDossierField()
        dossierFrame.Enabled = False
        'dossier.Enabled = False
        Diagnostic.ReadOnly = True
        region_Renamed.ReadOnly = True
        therapeute.ReadOnly = True
        selectmedecin.Enabled = False
        selectcode.Enabled = False
        selectDate.Enabled = False
        service.ReadOnly = True
        askedTRP.ReadOnly = True
        remarques.ReadOnly = True
    End Sub

    Private Sub resetDossierField()
        dossier.Items.Clear()
        dossier.Items.Add("* Nouveau dossier *")
        dossier.SelectedIndex = 0

        Diagnostic.Text = ""
        region_Renamed.Text = ""
        medecin.Text = "Aucun médecin sélectionné"
        codedossier.Text = "Aucun code sélectionné"
        curCode = Nothing
        noref.Text = ""
        remarques.Text = ""

        If currentTRP = "" Then
            therapeute.SelectedIndex = 0
        Else
            therapeute.SelectedIndex = therapeute.findString(currentTRP)
        End If
        _CodeNoUnique = 0
        _CodeNom = ""
    End Sub

    Private Sub selectcode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectcode.Click
        Dim selectingDate As Date = Date.Today
        If dhVisite.Text <> "" Then
            Dim vDate() As String = dhVisite.Text.Substring(0, dhVisite.Text.IndexOf(" ")).Split("/")
            selectingDate = New Date(vDate(2), vDate(1), vDate(0))
        End If

        Dim selectedCode As FolderCode = codifications.chooseCode(CType(therapeute.SelectedItem, User).noUser, selectingDate)
        If selectedCode Is Nothing Then Exit Sub

        Dim trp As User = UsersManager.getInstance.getUser(selectedCode.noUser)
        If trp Is Nothing Then trp = UserDefault.getInstance()

        curCode = selectedCode
        codedossier.Text = trp.toString & ":" & selectedCode.name
        changeDefaultPeriode(selectedCode)

        'REM_CODES
        codeNoUnique = selectedCode.noUnique
        selectDateAccident.Enabled = selectedCode.accidentDate
        selectDateRechute.Enabled = selectedCode.relaspeDate
        If selectDateAccident.Enabled = False Then dateAccident.Text = "Aucune date sélectionnée"
        If selectDateRechute.Enabled = False Then dateRechute.Text = "Aucune date sélectionnée"

        periodeChanged()
    End Sub

    Private Sub selectmedecin_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectmedecin.Click
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.selected = True
        myKeyPeople.specificCat = PreferencesManager.getGeneralPreferences()("MedecinCategorie")
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.Visible = False
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            medecinNo = kpChosen.noKP
            medecin.Text = kpChosen.kpFullName
        End If
    End Sub

    Private Sub periode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles periode.SelectedIndexChanged
        periodeChanged()
    End Sub

    Public Sub periodeChanged()
        Try
            If dhVisite.Text = "" Or periode.SelectedIndex < 0 Then Exit Sub

            Dim svDate() As String
            Dim myDate As Date
            Dim myCodeUnique As Integer = 0
            If curCode IsNot Nothing Then myCodeUnique = curCode.noUnique
            svDate = dhVisite.Text.Substring(0, dhVisite.Text.Length - 6).Split(New Char() {"/"})
            myDate = CDate(svDate(2) & "/" & svDate(1) & "/" & svDate(0) & " " & dhVisite.Text.Substring(dhVisite.Text.Length - 5, 5))

            Dim folderInfos() As String = dossier.Text.Split(New String() {" - "}, StringSplitOptions.None)
            Dim noFolder As Integer = If(dossier.SelectedIndex = 0, 0, CInt(folderInfos(0)))

            Dim checkConflictOptions As AgendaManager.TimeConflictOptions = AgendaManager.TimeConflictOptions.AcceptMultipleCodes Or AgendaManager.TimeConflictOptions.VerifySchedule Or AgendaManager.TimeConflictOptions.VerifyAbsences Or AgendaManager.TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime
            If Not AgendaManager.getInstance.checkTimeConflict(myDate, periodeArray(periode.SelectedIndex), User.extractNo(therapeute.Text), checkConflictOptions, currentNoClient, myCodeUnique, , , , noFolder) = "" Then dhVisite.Text = ""
        Catch 'REM Exception not handle
        End Try
    End Sub

    Public Sub changeDefaultPeriode(ByRef curCode As FolderCode) 'REM_CODES
        periode.Items.Clear()
        ReDim periodeArray(curCode.periods.Rows.Count + 7)
        Dim defEval As Short
        For i As Integer = 0 To curCode.periods.Rows.Count - 1
            periodeArray(i) = curCode.periods.Rows(i)("NoPeriode") * 15
            periode.Items.Add(IIf(curCode.periods.Rows(i)("IsEval"), "Évaluation", "Traitement") & " - " & periodeArray(i) & " min")
            If curCode.periods.Rows(i)("IsEval") AndAlso curCode.periods.Rows(i)("IsDefault") Then defEval = i
        Next i
        Dim curPeriode As Short = 15
        For i As Byte = curCode.periods.Rows.Count To periodeArray.GetUpperBound(0)
            periode.Items.Add(curPeriode & " min")
            periodeArray(i) = curPeriode
            curPeriode += 15
        Next i

        If dossier.SelectedIndex = 0 Then
            periode.SelectedIndex = defEval
        Else
            periode.SelectedIndex = 0
        End If
        periodeChanged()
    End Sub

    Public Sub changeDefaultPeriode(ByVal tempsTraitement As Short, ByVal tempsEvaluation As Short)
        periode.Items.Clear()
        ReDim periodeArray(9)
        periodeArray(0) = tempsTraitement
        periodeArray(1) = tempsEvaluation
        periode.Items.Add("Traitement - " & tempsTraitement & " min")
        periode.Items.Add("Évaluation - " & tempsEvaluation & " min")
        Dim curPeriode As Short = 15
        For i As Integer = 2 To periodeArray.GetUpperBound(0)
            periode.Items.Add(curPeriode & " min")
            periodeArray(i) = curPeriode
            curPeriode += 15
        Next i
        periodeChanged()
    End Sub

    Private Sub therapeute_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles therapeute.SelectedIndexChanged
        Dim noFolder() As String
        periodeChanged()
        If dossier.SelectedIndex = 0 Then
            codedossier.Text = "Aucun code sélectionné"
            curCode = Nothing
            'REM_CODES 
        End If

        service.Items.Clear()
        If therapeute.SelectedItem Is Nothing OrElse CType(therapeute.SelectedItem, User).services = "" Then
            'Désactive la sélection de l'heure et l'ajout d'un r-v
            adding.Enabled = False
            menusearchdispo.Enabled = False
            menusearchchoisir.Enabled = False
            Exit Sub
        End If

        adding.Enabled = True
        menusearchdispo.Enabled = True
        menusearchchoisir.Enabled = True

        service.Items.AddRange(CType(therapeute.SelectedItem, User).services.Split(New Char() {"§"}))

        If dossier.SelectedIndex > 0 Then
            noFolder = dossier.Text.Split(New String() {" - "}, StringSplitOptions.None)
            Dim myService() As String = DBLinker.getInstance.readOneDBField("InfoFolders", "Service", "WHERE ((NoTRPTraitant)=" & CType(therapeute.SelectedItem, User).noUser & " AND (NoFolder)=" & noFolder(0) & ");")
            If Not myService Is Nothing AndAlso myService.Length <> 0 Then service.Text = myService(0)
        End If

        If service.SelectedIndex < 0 And service.Items.Count > 0 Then service.SelectedIndex = 0
    End Sub

    Private Sub selectDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectDate.Click, selectDateAccident.Click, selectDateRechute.Click
        Dim myDateChoice As New DateChoice()
        Dim selectedDate As Date = Date.Now
        Dim dateLabel As Label
        Dim startingYear As Integer = firstUsageDate.Year - 100
        Select Case CType(sender, Button).Name
            Case "selectDate"
                dateLabel = dateReference
            Case "selectDateAccident"
                dateLabel = dateAccident
            Case "selectDateRechute"
                dateLabel = dateRechute
        End Select

        If dateLabel.Tag IsNot Nothing Then selectedDate = dateLabel.Tag

        Dim MyDate As Generic.List(Of Date) = myDateChoice.choose(startingYear, Date.Today.Year + 1, , , , , , True, , , , , selectedDate)
        If MyDate.Count <> 0 Then
            dateLabel.Text = DateFormat.getTextDate(MyDate(0))
            dateLabel.Tag = MyDate(0)
        End If
    End Sub

    Private Sub addvisite_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter

    End Sub

    Private Sub nam_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles nam.Enter
        If DBLinker.getInstance().dbConnected = False Then namSelfOpened = True : DBLinker.getInstance().dbConnected = True
    End Sub

    Private Sub nam_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles nam.Leave
        If namSelfOpened = True Then namSelfOpened = False : DBLinker.getInstance().dbConnected = False
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf adding_Click)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub

    Private Sub addvisite_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If loadSearchWin = True Then getNAM_Click(sender, EventArgs.Empty)
    End Sub
End Class
