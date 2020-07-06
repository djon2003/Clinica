Option Strict Off
Option Explicit On
Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Friend Class codifications
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
        configList(listcode)

        'Load default periodes table
        defaultPeriodes.Columns.Add("NoCDPeriode", (3).GetType)
        defaultPeriodes.Columns.Add("NoCodification", (3).GetType)
        defaultPeriodes.Columns.Add("IsEval", (True).GetType)
        defaultPeriodes.Columns.Add("IsDefault", (True).GetType)
        defaultPeriodes.Columns.Add("NoPeriode", (3).GetType)
        defaultPeriodes.Columns.Add("Montant", (New Decimal(1)).GetType)
        defaultPeriodes.Columns.Add("PourcentAbsence", (New Decimal(1)).GetType)
        defaultPeriodes.Columns.Add("PourcentClient", (New Decimal(1)).GetType)
        defaultPeriodes.Columns.Add("Button", ("").GetType)
        defaultPeriodes.Columns.Add("KPName", ("").GetType)
        defaultPeriodes.Columns.Add("NoKP", (3).GetType)
        defaultPeriodes.Rows.Add(New Object() {0, 0, 1, 1, 3, 40, 0.5, 1, "", "Aucun(e)", 0})
        defaultPeriodes.Rows.Add(New Object() {0, 0, 0, 1, 1, 40, 0.5, 1, "", "Aucun(e)", 0})

        'Chargement des images
        With DrawingManager.getInstance
            Me.add_Renamed.Image = .getImage("ajouter16.gif")
            Me.delete.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.selectCode.Image = .getImage("selection16.gif")
            Me.save.Image = .getImage("modifier16.gif")
            Dim defaultIcon As Image = DrawingManager.iconToImage(.getIcon("default.ico"), New Size(16, 16))
            Me.setDefaultAll.Image = defaultIcon
            Me.setDefault.Image = defaultIcon
            Me.listcode.icons = New Generic.List(Of Icon)
            Me.listcode.icons.Add(New Icon(.getIcon("default.ico"), New Size(8, 8)))
            Me.save.Image = .getImage("save.jpg")
            Me.copyAll.Image = .getImage("copy16.jpg")
            Me.copy.Image = .getImage("copy16.jpg")
            Me.selectFirstDate.Image = .getImage("selection16.gif")
            Me.Icon = DrawingManager.imageToIcon(.getImage("codedossier.gif"))
        End With
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
    Public WithEvents selectCode As System.Windows.Forms.Button
    Public WithEvents autoSelectBillWhenPaying As System.Windows.Forms.CheckBox
    Public WithEvents recu As System.Windows.Forms.CheckBox
    Public WithEvents setDefaultAll As System.Windows.Forms.Button
    Public WithEvents copyAll As System.Windows.Forms.Button
    Public WithEvents therapeute As System.Windows.Forms.ComboBox
    Public WithEvents frame2 As System.Windows.Forms.GroupBox
    Public WithEvents delete As System.Windows.Forms.Button
    Public WithEvents add_Renamed As System.Windows.Forms.Button
    Public WithEvents save As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents msgnoref As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents msgDiagnostic As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents dateAccident As System.Windows.Forms.CheckBox
    Public WithEvents dateRechute As System.Windows.Forms.CheckBox
    Friend WithEvents listcode As CI.Controls.List
    Public WithEvents autoShowPayment As System.Windows.Forms.CheckBox
    Friend WithEvents FrameOptions As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents frameCode As System.Windows.Forms.GroupBox
    Public WithEvents affPourcentAllTimes As System.Windows.Forms.CheckBox
    Public WithEvents label16 As System.Windows.Forms.Label
    Public WithEvents label17 As System.Windows.Forms.Label
    Public WithEvents ponderationEval As ManagedText
    Public WithEvents label18 As System.Windows.Forms.Label
    Public WithEvents ponderationVisite As ManagedText
    Public WithEvents defaultPaymentMethod As System.Windows.Forms.ComboBox
    Public WithEvents label19 As System.Windows.Forms.Label
    Public WithEvents demandeAuthorisation As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents periodes As DataGridPlus
    Public WithEvents notConfirmRVOnPasteOfDTRP As System.Windows.Forms.CheckBox
    Friend WithEvents menuKPSelector As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AucunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NoCDPeriode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoCodification As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsEval As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents IsDefault As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NoPeriode As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Montant As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PourcentAbsence As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PourcentClient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KPSelectorColumn As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents KPColumn As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents NoKP As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents dates As System.Windows.Forms.ComboBox
    Public WithEvents setDefault As System.Windows.Forms.Button
    Public WithEvents copy As System.Windows.Forms.Button
    Friend WithEvents lastEffectiveTime As System.Windows.Forms.Label
    Friend WithEvents firstEffectiveTime As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents selectFirstDate As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents folderTextTypes As TreeViewComboPlus
    Friend WithEvents folderAlertTypes As TreeViewComboPlus
    Public WithEvents startingExternalStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabFolder As System.Windows.Forms.TabPage
    Friend WithEvents tabRV As System.Windows.Forms.TabPage
    Friend WithEvents tabBill As System.Windows.Forms.TabPage
    Public WithEvents confirmation As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(codifications))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.selectCode = New System.Windows.Forms.Button
        Me.autoSelectBillWhenPaying = New System.Windows.Forms.CheckBox
        Me.recu = New System.Windows.Forms.CheckBox
        Me.FrameOptions = New System.Windows.Forms.GroupBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabFolder = New System.Windows.Forms.TabPage
        Me.msgnoref = New System.Windows.Forms.CheckBox
        Me.msgDiagnostic = New System.Windows.Forms.CheckBox
        Me.dateAccident = New System.Windows.Forms.CheckBox
        Me.dateRechute = New System.Windows.Forms.CheckBox
        Me.demandeAuthorisation = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.tabRV = New System.Windows.Forms.TabPage
        Me.label16 = New System.Windows.Forms.Label
        Me.confirmation = New System.Windows.Forms.ComboBox
        Me.notConfirmRVOnPasteOfDTRP = New System.Windows.Forms.CheckBox
        Me.startingExternalStatus = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tabBill = New System.Windows.Forms.TabPage
        Me.autoShowPayment = New System.Windows.Forms.CheckBox
        Me.affPourcentAllTimes = New System.Windows.Forms.CheckBox
        Me.label19 = New System.Windows.Forms.Label
        Me.defaultPaymentMethod = New System.Windows.Forms.ComboBox
        Me.lastEffectiveTime = New System.Windows.Forms.Label
        Me.firstEffectiveTime = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.selectFirstDate = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.label17 = New System.Windows.Forms.Label
        Me.ponderationEval = New ManagedText
        Me.label18 = New System.Windows.Forms.Label
        Me.ponderationVisite = New ManagedText
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.frame2 = New System.Windows.Forms.GroupBox
        Me.setDefaultAll = New System.Windows.Forms.Button
        Me.copyAll = New System.Windows.Forms.Button
        Me.dates = New System.Windows.Forms.ComboBox
        Me.therapeute = New System.Windows.Forms.ComboBox
        Me.frameCode = New System.Windows.Forms.GroupBox
        Me.add_Renamed = New System.Windows.Forms.Button
        Me.save = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.copy = New System.Windows.Forms.Button
        Me.setDefault = New System.Windows.Forms.Button
        Me.listcode = New CI.Controls.List
        Me.periodes = New CI.Base.Windows.Forms.DataGridPlus
        Me.NoCDPeriode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoCodification = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IsEval = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.IsDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.NoPeriode = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.Montant = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PourcentAbsence = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PourcentClient = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.KPSelectorColumn = New System.Windows.Forms.DataGridViewButtonColumn
        Me.KPColumn = New System.Windows.Forms.DataGridViewLinkColumn
        Me.NoKP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.menuKPSelector = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AucunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.folderAlertTypes = New TreeViewComboPlus
        Me.folderTextTypes = New TreeViewComboPlus
        Me.FrameOptions.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabFolder.SuspendLayout()
        Me.tabRV.SuspendLayout()
        Me.tabBill.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.frame2.SuspendLayout()
        Me.frameCode.SuspendLayout()
        CType(Me.periodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuKPSelector.SuspendLayout()
        Me.SuspendLayout()
        '
        'selectCode
        '
        Me.selectCode.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.selectCode.BackColor = System.Drawing.SystemColors.Control
        Me.selectCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectCode.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectCode.Location = New System.Drawing.Point(490, 457)
        Me.selectCode.Name = "selectCode"
        Me.selectCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectCode.Size = New System.Drawing.Size(24, 24)
        Me.selectCode.TabIndex = 22
        Me.selectCode.TabStop = False
        Me.ToolTip1.SetToolTip(Me.selectCode, "Choisir la codification sélectionnée")
        Me.selectCode.UseVisualStyleBackColor = False
        '
        'autoSelectBillWhenPaying
        '
        Me.autoSelectBillWhenPaying.BackColor = System.Drawing.Color.Transparent
        Me.autoSelectBillWhenPaying.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoSelectBillWhenPaying.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoSelectBillWhenPaying.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoSelectBillWhenPaying.Location = New System.Drawing.Point(3, 29)
        Me.autoSelectBillWhenPaying.Name = "autoSelectBillWhenPaying"
        Me.autoSelectBillWhenPaying.Size = New System.Drawing.Size(232, 19)
        Me.autoSelectBillWhenPaying.TabIndex = 15
        Me.autoSelectBillWhenPaying.Text = "Sélectionner le paiement automatiquement"
        Me.autoSelectBillWhenPaying.UseVisualStyleBackColor = False
        '
        'recu
        '
        Me.recu.BackColor = System.Drawing.Color.Transparent
        Me.recu.Cursor = System.Windows.Forms.Cursors.Default
        Me.recu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.recu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.recu.Location = New System.Drawing.Point(3, 54)
        Me.recu.Name = "recu"
        Me.recu.Size = New System.Drawing.Size(216, 18)
        Me.recu.TabIndex = 16
        Me.recu.Text = "Émettre un reçu automatiquement"
        Me.recu.UseVisualStyleBackColor = False
        '
        'FrameOptions
        '
        Me.FrameOptions.BackColor = System.Drawing.Color.Transparent
        Me.FrameOptions.Controls.Add(Me.TabControl1)
        Me.FrameOptions.Controls.Add(Me.lastEffectiveTime)
        Me.FrameOptions.Controls.Add(Me.firstEffectiveTime)
        Me.FrameOptions.Controls.Add(Me.Label3)
        Me.FrameOptions.Controls.Add(Me.Label1)
        Me.FrameOptions.Controls.Add(Me.selectFirstDate)
        Me.FrameOptions.Controls.Add(Me.GroupBox2)
        Me.FrameOptions.Location = New System.Drawing.Point(0, 40)
        Me.FrameOptions.Name = "FrameOptions"
        Me.FrameOptions.Size = New System.Drawing.Size(708, 274)
        Me.FrameOptions.TabIndex = 24
        Me.FrameOptions.TabStop = False
        Me.FrameOptions.Text = "Options"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabFolder)
        Me.TabControl1.Controls.Add(Me.tabRV)
        Me.TabControl1.Controls.Add(Me.tabBill)
        Me.TabControl1.Location = New System.Drawing.Point(6, 49)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(696, 180)
        Me.TabControl1.TabIndex = 103
        '
        'tabFolder
        '
        Me.tabFolder.Controls.Add(Me.msgnoref)
        Me.tabFolder.Controls.Add(Me.msgDiagnostic)
        Me.tabFolder.Controls.Add(Me.dateAccident)
        Me.tabFolder.Controls.Add(Me.dateRechute)
        Me.tabFolder.Controls.Add(Me.demandeAuthorisation)
        Me.tabFolder.Controls.Add(Me.Label9)
        Me.tabFolder.Controls.Add(Me.Label10)
        Me.tabFolder.Controls.Add(Me.Label11)
        Me.tabFolder.Location = New System.Drawing.Point(4, 23)
        Me.tabFolder.Name = "tabFolder"
        Me.tabFolder.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFolder.Size = New System.Drawing.Size(688, 153)
        Me.tabFolder.TabIndex = 0
        Me.tabFolder.Text = "Dossier"
        Me.tabFolder.UseVisualStyleBackColor = True
        '
        'msgnoref
        '
        Me.msgnoref.Cursor = System.Windows.Forms.Cursors.Default
        Me.msgnoref.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.msgnoref.ForeColor = System.Drawing.SystemColors.ControlText
        Me.msgnoref.Location = New System.Drawing.Point(364, 26)
        Me.msgnoref.Name = "msgnoref"
        Me.msgnoref.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.msgnoref.Size = New System.Drawing.Size(144, 17)
        Me.msgnoref.TabIndex = 8
        Me.msgnoref.Text = "Le numéro de référence"
        Me.msgnoref.UseVisualStyleBackColor = False
        '
        'msgDiagnostic
        '
        Me.msgDiagnostic.Cursor = System.Windows.Forms.Cursors.Default
        Me.msgDiagnostic.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.msgDiagnostic.ForeColor = System.Drawing.SystemColors.ControlText
        Me.msgDiagnostic.Location = New System.Drawing.Point(364, 49)
        Me.msgDiagnostic.Name = "msgDiagnostic"
        Me.msgDiagnostic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.msgDiagnostic.Size = New System.Drawing.Size(96, 17)
        Me.msgDiagnostic.TabIndex = 9
        Me.msgDiagnostic.Text = "Le diagnostic"
        Me.msgDiagnostic.UseVisualStyleBackColor = False
        '
        'dateAccident
        '
        Me.dateAccident.Cursor = System.Windows.Forms.Cursors.Default
        Me.dateAccident.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateAccident.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dateAccident.Location = New System.Drawing.Point(3, 7)
        Me.dateAccident.Name = "dateAccident"
        Me.dateAccident.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dateAccident.Size = New System.Drawing.Size(152, 17)
        Me.dateAccident.TabIndex = 10
        Me.dateAccident.Text = "Activer la date d'accident"
        Me.dateAccident.UseVisualStyleBackColor = False
        '
        'dateRechute
        '
        Me.dateRechute.Cursor = System.Windows.Forms.Cursors.Default
        Me.dateRechute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateRechute.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dateRechute.Location = New System.Drawing.Point(3, 30)
        Me.dateRechute.Name = "dateRechute"
        Me.dateRechute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dateRechute.Size = New System.Drawing.Size(152, 17)
        Me.dateRechute.TabIndex = 11
        Me.dateRechute.Text = "Activer la date de rechute"
        Me.dateRechute.UseVisualStyleBackColor = False
        '
        'demandeAuthorisation
        '
        Me.demandeAuthorisation.Cursor = System.Windows.Forms.Cursors.Default
        Me.demandeAuthorisation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.demandeAuthorisation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.demandeAuthorisation.Location = New System.Drawing.Point(3, 53)
        Me.demandeAuthorisation.Name = "demandeAuthorisation"
        Me.demandeAuthorisation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.demandeAuthorisation.Size = New System.Drawing.Size(246, 19)
        Me.demandeAuthorisation.TabIndex = 13
        Me.demandeAuthorisation.Text = "Activer le suivi d'une demande d'autorisation"
        Me.demandeAuthorisation.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(348, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(141, 14)
        Me.Label9.TabIndex = 57
        Me.Label9.Text = "Message indicateur pour ..."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(356, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(11, 14)
        Me.Label10.TabIndex = 59
        Me.Label10.Text = "-"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(356, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(11, 14)
        Me.Label11.TabIndex = 60
        Me.Label11.Text = "-"
        '
        'tabRV
        '
        Me.tabRV.Controls.Add(Me.confirmation)
        Me.tabRV.Controls.Add(Me.label16)
        Me.tabRV.Controls.Add(Me.notConfirmRVOnPasteOfDTRP)
        Me.tabRV.Controls.Add(Me.startingExternalStatus)
        Me.tabRV.Controls.Add(Me.Label2)
        Me.tabRV.Location = New System.Drawing.Point(4, 23)
        Me.tabRV.Name = "tabRV"
        Me.tabRV.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRV.Size = New System.Drawing.Size(688, 153)
        Me.tabRV.TabIndex = 1
        Me.tabRV.Text = "Rendez-vous"
        Me.tabRV.UseVisualStyleBackColor = True
        '
        'label16
        '
        Me.label16.AutoSize = True
        Me.label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label16.Location = New System.Drawing.Point(3, 3)
        Me.label16.Name = "label16"
        Me.label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label16.Size = New System.Drawing.Size(142, 14)
        Me.label16.TabIndex = 65
        Me.label16.Text = "Confirmer les rendez-vous :"
        '
        'confirmation
        '
        Me.confirmation.BackColor = System.Drawing.SystemColors.Window
        Me.confirmation.Cursor = System.Windows.Forms.Cursors.Default
        Me.confirmation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.confirmation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.confirmation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.confirmation.Items.AddRange(New Object() {"Aucun", "Évaluation", "Traitements", "Évaluation et traitements"})
        Me.confirmation.Location = New System.Drawing.Point(6, 19)
        Me.confirmation.Name = "confirmation"
        Me.confirmation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.confirmation.Size = New System.Drawing.Size(251, 22)
        Me.confirmation.TabIndex = 20
        '
        'notConfirmRVOnPasteOfDTRP
        '
        Me.notConfirmRVOnPasteOfDTRP.Cursor = System.Windows.Forms.Cursors.Default
        Me.notConfirmRVOnPasteOfDTRP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notConfirmRVOnPasteOfDTRP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.notConfirmRVOnPasteOfDTRP.Location = New System.Drawing.Point(6, 101)
        Me.notConfirmRVOnPasteOfDTRP.Name = "notConfirmRVOnPasteOfDTRP"
        Me.notConfirmRVOnPasteOfDTRP.Size = New System.Drawing.Size(416, 52)
        Me.notConfirmRVOnPasteOfDTRP.TabIndex = 17
        Me.notConfirmRVOnPasteOfDTRP.Text = "Désactiver le questionnement lors d'une prise d'un rendez-vous d'un même agenda o" & _
            "ù le thérapeute réel n'est pas le thérapeute traitant si l'utilisateur a ce ques" & _
            "tionnement désactivé"
        Me.notConfirmRVOnPasteOfDTRP.UseVisualStyleBackColor = False
        '
        'startingExternalStatus
        '
        Me.startingExternalStatus.BackColor = System.Drawing.SystemColors.Window
        Me.startingExternalStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.startingExternalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.startingExternalStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startingExternalStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.startingExternalStatus.Location = New System.Drawing.Point(6, 65)
        Me.startingExternalStatus.Name = "startingExternalStatus"
        Me.startingExternalStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.startingExternalStatus.Size = New System.Drawing.Size(251, 22)
        Me.startingExternalStatus.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(3, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(214, 14)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "Statut externe d'un nouveau rendez-vous :"
        '
        'tabBill
        '
        Me.tabBill.Controls.Add(Me.defaultPaymentMethod)
        Me.tabBill.Controls.Add(Me.autoShowPayment)
        Me.tabBill.Controls.Add(Me.affPourcentAllTimes)
        Me.tabBill.Controls.Add(Me.recu)
        Me.tabBill.Controls.Add(Me.autoSelectBillWhenPaying)
        Me.tabBill.Controls.Add(Me.label19)
        Me.tabBill.Location = New System.Drawing.Point(4, 23)
        Me.tabBill.Name = "tabBill"
        Me.tabBill.Size = New System.Drawing.Size(688, 153)
        Me.tabBill.TabIndex = 2
        Me.tabBill.Text = "Facturation"
        Me.tabBill.UseVisualStyleBackColor = True
        '
        'autoShowPayment
        '
        Me.autoShowPayment.AutoSize = True
        Me.autoShowPayment.Cursor = System.Windows.Forms.Cursors.Default
        Me.autoShowPayment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoShowPayment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.autoShowPayment.Location = New System.Drawing.Point(3, 3)
        Me.autoShowPayment.Name = "autoShowPayment"
        Me.autoShowPayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autoShowPayment.Size = New System.Drawing.Size(346, 18)
        Me.autoShowPayment.TabIndex = 14
        Me.autoShowPayment.Text = "Ouverture automatique du paiement lors de la prise de la présence"
        Me.autoShowPayment.UseVisualStyleBackColor = False
        '
        'affPourcentAllTimes
        '
        Me.affPourcentAllTimes.Cursor = System.Windows.Forms.Cursors.Default
        Me.affPourcentAllTimes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.affPourcentAllTimes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.affPourcentAllTimes.Location = New System.Drawing.Point(3, 78)
        Me.affPourcentAllTimes.Name = "affPourcentAllTimes"
        Me.affPourcentAllTimes.Size = New System.Drawing.Size(370, 20)
        Me.affPourcentAllTimes.TabIndex = 17
        Me.affPourcentAllTimes.Text = "Choisir le pourcentage payé par le client à chaque prise de présence"
        Me.affPourcentAllTimes.UseVisualStyleBackColor = False
        '
        'label19
        '
        Me.label19.AutoSize = True
        Me.label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label19.Location = New System.Drawing.Point(0, 113)
        Me.label19.Name = "label19"
        Me.label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label19.Size = New System.Drawing.Size(232, 14)
        Me.label19.TabIndex = 65
        Me.label19.Text = "Méthode de paiement sélectionnée par défaut :"
        '
        'defaultPaymentMethod
        '
        Me.defaultPaymentMethod.BackColor = System.Drawing.SystemColors.Window
        Me.defaultPaymentMethod.Cursor = System.Windows.Forms.Cursors.Default
        Me.defaultPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.defaultPaymentMethod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.defaultPaymentMethod.ForeColor = System.Drawing.SystemColors.WindowText
        Me.defaultPaymentMethod.Location = New System.Drawing.Point(3, 128)
        Me.defaultPaymentMethod.Name = "defaultPaymentMethod"
        Me.defaultPaymentMethod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.defaultPaymentMethod.Size = New System.Drawing.Size(251, 22)
        Me.defaultPaymentMethod.TabIndex = 21
        '
        'lastEffectiveTime
        '
        Me.lastEffectiveTime.AutoSize = True
        Me.lastEffectiveTime.Location = New System.Drawing.Point(428, 24)
        Me.lastEffectiveTime.Name = "lastEffectiveTime"
        Me.lastEffectiveTime.Size = New System.Drawing.Size(45, 14)
        Me.lastEffectiveTime.TabIndex = 102
        Me.lastEffectiveTime.Text = "Aucune"
        '
        'firstEffectiveTime
        '
        Me.firstEffectiveTime.AutoSize = True
        Me.firstEffectiveTime.Location = New System.Drawing.Point(191, 24)
        Me.firstEffectiveTime.Name = "firstEffectiveTime"
        Me.firstEffectiveTime.Size = New System.Drawing.Size(45, 14)
        Me.firstEffectiveTime.TabIndex = 102
        Me.firstEffectiveTime.Text = "Aucune"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(295, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 14)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "Date de fin d'application :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(43, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 14)
        Me.Label1.TabIndex = 102
        Me.Label1.Text = "Date de début d'application :"
        '
        'selectFirstDate
        '
        Me.selectFirstDate.BackColor = System.Drawing.SystemColors.Control
        Me.selectFirstDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectFirstDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectFirstDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectFirstDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectFirstDate.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectFirstDate.Location = New System.Drawing.Point(13, 19)
        Me.selectFirstDate.Name = "selectFirstDate"
        Me.selectFirstDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectFirstDate.Size = New System.Drawing.Size(24, 24)
        Me.selectFirstDate.TabIndex = 12
        Me.selectFirstDate.TabStop = False
        Me.ToolTip1.SetToolTip(Me.selectFirstDate, "Sélectionner la date de début d'application de la codification sélectionnée")
        Me.selectFirstDate.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.label17)
        Me.GroupBox2.Controls.Add(Me.ponderationEval)
        Me.GroupBox2.Controls.Add(Me.label18)
        Me.GroupBox2.Controls.Add(Me.ponderationVisite)
        Me.GroupBox2.Location = New System.Drawing.Point(525, 100)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(183, 66)
        Me.GroupBox2.TabIndex = 100
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Rapports de statistiques"
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.BackColor = System.Drawing.SystemColors.Control
        Me.label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label17.Location = New System.Drawing.Point(2, 16)
        Me.label17.Name = "label17"
        Me.label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label17.Size = New System.Drawing.Size(141, 14)
        Me.label17.TabIndex = 7
        Me.label17.Text = "Pondération de l'évaluation :"
        '
        'ponderationEval
        '
        Me.ponderationEval.acceptAlpha = False
        Me.ponderationEval.acceptedChars = ",§."
        Me.ponderationEval.acceptNumeric = True
        Me.ponderationEval.AcceptsReturn = True
        Me.ponderationEval.allCapital = False
        Me.ponderationEval.allLower = False
        Me.ponderationEval.BackColor = System.Drawing.SystemColors.Window
        Me.ponderationEval.blockOnMaximum = False
        Me.ponderationEval.blockOnMinimum = False
        Me.ponderationEval.cb_AcceptLeftZeros = False
        Me.ponderationEval.cb_AcceptNegative = False
        Me.ponderationEval.currencyBox = True
        Me.ponderationEval.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ponderationEval.firstLetterCapital = False
        Me.ponderationEval.firstLettersCapital = False
        Me.ponderationEval.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ponderationEval.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ponderationEval.Location = New System.Drawing.Point(149, 13)
        Me.ponderationEval.manageText = True
        Me.ponderationEval.matchExp = ""
        Me.ponderationEval.maximum = 0
        Me.ponderationEval.MaxLength = 0
        Me.ponderationEval.minimum = 0
        Me.ponderationEval.Name = "ponderationEval"
        Me.ponderationEval.nbDecimals = CType(2, Short)
        Me.ponderationEval.onlyAlphabet = False
        Me.ponderationEval.refuseAccents = False
        Me.ponderationEval.refusedChars = ""
        Me.ponderationEval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ponderationEval.showInternalContextMenu = True
        Me.ponderationEval.Size = New System.Drawing.Size(24, 20)
        Me.ponderationEval.TabIndex = 6
        Me.ponderationEval.Text = "0"
        Me.ponderationEval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ponderationEval.trimText = False
        '
        'label18
        '
        Me.label18.AutoSize = True
        Me.label18.BackColor = System.Drawing.SystemColors.Control
        Me.label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.label18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label18.Location = New System.Drawing.Point(2, 42)
        Me.label18.Name = "label18"
        Me.label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label18.Size = New System.Drawing.Size(143, 14)
        Me.label18.TabIndex = 7
        Me.label18.Text = "Pondération d'un traitement :"
        '
        'ponderationVisite
        '
        Me.ponderationVisite.acceptAlpha = False
        Me.ponderationVisite.acceptedChars = ",§."
        Me.ponderationVisite.acceptNumeric = True
        Me.ponderationVisite.AcceptsReturn = True
        Me.ponderationVisite.allCapital = False
        Me.ponderationVisite.allLower = False
        Me.ponderationVisite.BackColor = System.Drawing.SystemColors.Window
        Me.ponderationVisite.blockOnMaximum = False
        Me.ponderationVisite.blockOnMinimum = False
        Me.ponderationVisite.cb_AcceptLeftZeros = False
        Me.ponderationVisite.cb_AcceptNegative = False
        Me.ponderationVisite.currencyBox = True
        Me.ponderationVisite.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ponderationVisite.firstLetterCapital = False
        Me.ponderationVisite.firstLettersCapital = False
        Me.ponderationVisite.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ponderationVisite.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ponderationVisite.Location = New System.Drawing.Point(149, 39)
        Me.ponderationVisite.manageText = True
        Me.ponderationVisite.matchExp = ""
        Me.ponderationVisite.maximum = 0
        Me.ponderationVisite.MaxLength = 0
        Me.ponderationVisite.minimum = 0
        Me.ponderationVisite.Name = "ponderationVisite"
        Me.ponderationVisite.nbDecimals = CType(2, Short)
        Me.ponderationVisite.onlyAlphabet = False
        Me.ponderationVisite.refuseAccents = False
        Me.ponderationVisite.refusedChars = ""
        Me.ponderationVisite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ponderationVisite.showInternalContextMenu = True
        Me.ponderationVisite.Size = New System.Drawing.Size(24, 20)
        Me.ponderationVisite.TabIndex = 7
        Me.ponderationVisite.Text = "0"
        Me.ponderationVisite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ponderationVisite.trimText = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.FrameOptions)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 176)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(708, 275)
        Me.GroupBox3.TabIndex = 102
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Éléments ne prenant pas en compte les dates d'application"
        '
        'frame2
        '
        Me.frame2.BackColor = System.Drawing.SystemColors.Control
        Me.frame2.Controls.Add(Me.setDefaultAll)
        Me.frame2.Controls.Add(Me.copyAll)
        Me.frame2.Controls.Add(Me.dates)
        Me.frame2.Controls.Add(Me.therapeute)
        Me.frame2.Controls.Add(Me.frameCode)
        Me.frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame2.Location = New System.Drawing.Point(8, 0)
        Me.frame2.Name = "frame2"
        Me.frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame2.Size = New System.Drawing.Size(731, 527)
        Me.frame2.TabIndex = 17
        Me.frame2.TabStop = False
        Me.frame2.Text = "Thérapeute && Plage"
        '
        'setDefaultAll
        '
        Me.setDefaultAll.BackColor = System.Drawing.SystemColors.Control
        Me.setDefaultAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.setDefaultAll.Enabled = False
        Me.setDefaultAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.setDefaultAll.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.setDefaultAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.setDefaultAll.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.setDefaultAll.Location = New System.Drawing.Point(700, 14)
        Me.setDefaultAll.Name = "setDefaultAll"
        Me.setDefaultAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.setDefaultAll.Size = New System.Drawing.Size(24, 24)
        Me.setDefaultAll.TabIndex = 20
        Me.setDefaultAll.TabStop = False
        Me.ToolTip1.SetToolTip(Me.setDefaultAll, "Remettre toutes les codifications spécifiques du thérapeute par défaut")
        Me.setDefaultAll.UseVisualStyleBackColor = False
        '
        'copyAll
        '
        Me.copyAll.BackColor = System.Drawing.SystemColors.Control
        Me.copyAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.copyAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.copyAll.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copyAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copyAll.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.copyAll.Location = New System.Drawing.Point(670, 14)
        Me.copyAll.Name = "copyAll"
        Me.copyAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copyAll.Size = New System.Drawing.Size(24, 24)
        Me.copyAll.TabIndex = 19
        Me.copyAll.TabStop = False
        Me.ToolTip1.SetToolTip(Me.copyAll, "Copier les codifications spécifiques en cours vers un thérapeute")
        Me.copyAll.UseVisualStyleBackColor = False
        '
        'dates
        '
        Me.dates.BackColor = System.Drawing.SystemColors.Window
        Me.dates.Cursor = System.Windows.Forms.Cursors.Default
        Me.dates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dates.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dates.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dates.Location = New System.Drawing.Point(469, 16)
        Me.dates.Name = "dates"
        Me.dates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dates.Size = New System.Drawing.Size(195, 22)
        Me.dates.TabIndex = 18
        Me.dates.TabStop = False
        '
        'therapeute
        '
        Me.therapeute.BackColor = System.Drawing.SystemColors.Window
        Me.therapeute.Cursor = System.Windows.Forms.Cursors.Default
        Me.therapeute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.therapeute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.therapeute.ForeColor = System.Drawing.SystemColors.WindowText
        Me.therapeute.Location = New System.Drawing.Point(8, 16)
        Me.therapeute.Name = "therapeute"
        Me.therapeute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.therapeute.Size = New System.Drawing.Size(455, 22)
        Me.therapeute.Sorted = True
        Me.therapeute.TabIndex = 0
        Me.therapeute.TabStop = False
        '
        'frameCode
        '
        Me.frameCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.frameCode.BackColor = System.Drawing.Color.Transparent
        Me.frameCode.Controls.Add(Me.GroupBox3)
        Me.frameCode.Controls.Add(Me.add_Renamed)
        Me.frameCode.Controls.Add(Me.save)
        Me.frameCode.Controls.Add(Me.delete)
        Me.frameCode.Controls.Add(Me.copy)
        Me.frameCode.Controls.Add(Me.setDefault)
        Me.frameCode.Controls.Add(Me.selectCode)
        Me.frameCode.Controls.Add(Me.listcode)
        Me.frameCode.Controls.Add(Me.periodes)
        Me.frameCode.Location = New System.Drawing.Point(3, 37)
        Me.frameCode.Name = "frameCode"
        Me.frameCode.Size = New System.Drawing.Size(725, 487)
        Me.frameCode.TabIndex = 23
        Me.frameCode.TabStop = False
        Me.frameCode.Text = "Codification(s)"
        '
        'add_Renamed
        '
        Me.add_Renamed.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.add_Renamed.BackColor = System.Drawing.SystemColors.Control
        Me.add_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.add_Renamed.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.add_Renamed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.add_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.add_Renamed.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.add_Renamed.Location = New System.Drawing.Point(210, 457)
        Me.add_Renamed.Name = "add_Renamed"
        Me.add_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.add_Renamed.Size = New System.Drawing.Size(24, 24)
        Me.add_Renamed.TabIndex = 12
        Me.add_Renamed.TabStop = False
        Me.ToolTip1.SetToolTip(Me.add_Renamed, "Ajouter une codification")
        Me.add_Renamed.UseVisualStyleBackColor = False
        '
        'save
        '
        Me.save.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.save.BackColor = System.Drawing.SystemColors.Control
        Me.save.Cursor = System.Windows.Forms.Cursors.Default
        Me.save.Enabled = False
        Me.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.save.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save.ForeColor = System.Drawing.SystemColors.ControlText
        Me.save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.save.Location = New System.Drawing.Point(266, 457)
        Me.save.Name = "save"
        Me.save.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.save.Size = New System.Drawing.Size(24, 24)
        Me.save.TabIndex = 10
        Me.save.TabStop = False
        Me.ToolTip1.SetToolTip(Me.save, "Enregistrer la codification sélectionnée")
        Me.save.UseVisualStyleBackColor = False
        '
        'delete
        '
        Me.delete.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.delete.BackColor = System.Drawing.SystemColors.Control
        Me.delete.Cursor = System.Windows.Forms.Cursors.Default
        Me.delete.Enabled = False
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.delete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delete.Location = New System.Drawing.Point(322, 457)
        Me.delete.Name = "delete"
        Me.delete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 13
        Me.delete.TabStop = False
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer la codification sélectionnée")
        Me.delete.UseVisualStyleBackColor = False
        '
        'copy
        '
        Me.copy.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.copy.BackColor = System.Drawing.SystemColors.Control
        Me.copy.Cursor = System.Windows.Forms.Cursors.Default
        Me.copy.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.copy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.copy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.copy.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.copy.Location = New System.Drawing.Point(378, 457)
        Me.copy.Name = "copy"
        Me.copy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.copy.Size = New System.Drawing.Size(24, 24)
        Me.copy.TabIndex = 22
        Me.copy.TabStop = False
        Me.ToolTip1.SetToolTip(Me.copy, "Copier la codification sélectionnée vers un autre thérapeute")
        Me.copy.UseVisualStyleBackColor = False
        '
        'setDefault
        '
        Me.setDefault.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.setDefault.BackColor = System.Drawing.SystemColors.Control
        Me.setDefault.Cursor = System.Windows.Forms.Cursors.Default
        Me.setDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.setDefault.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.setDefault.ForeColor = System.Drawing.SystemColors.ControlText
        Me.setDefault.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.setDefault.Location = New System.Drawing.Point(434, 457)
        Me.setDefault.Name = "setDefault"
        Me.setDefault.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.setDefault.Size = New System.Drawing.Size(24, 24)
        Me.setDefault.TabIndex = 22
        Me.setDefault.TabStop = False
        Me.ToolTip1.SetToolTip(Me.setDefault, "Remettre par défaut la codification sélectionnée")
        Me.setDefault.UseVisualStyleBackColor = False
        '
        'listcode
        '
        Me.listcode.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.listcode.autoAdjust = True
        Me.listcode.autoKeyDownSelection = True
        Me.listcode.autoSizeHorizontally = False
        Me.listcode.autoSizeVertically = False
        Me.listcode.BackColor = System.Drawing.Color.White
        Me.listcode.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.listcode.baseBackColor = System.Drawing.Color.White
        Me.listcode.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.listcode.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.listcode.bgColor = System.Drawing.Color.White
        Me.listcode.borderColor = System.Drawing.Color.Empty
        Me.listcode.borderSelColor = System.Drawing.Color.Empty
        Me.listcode.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.listcode.CausesValidation = False
        Me.listcode.clickEnabled = True
        Me.listcode.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.listcode.do3D = False
        Me.listcode.draw = False
        Me.listcode.extraWidth = 0
        Me.listcode.hScrollColor = System.Drawing.SystemColors.Control
        Me.listcode.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listcode.hScrolling = True
        Me.listcode.hsValue = 0
        Me.listcode.icons = CType(resources.GetObject("listcode.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.listcode.itemBorder = 0
        Me.listcode.itemMargin = 0
        Me.listcode.items = CType(resources.GetObject("listcode.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.listcode.Location = New System.Drawing.Point(8, 16)
        Me.listcode.mouseMove3D = False
        Me.listcode.mouseSpeed = 0
        Me.listcode.Name = "listcode"
        Me.listcode.objMaxHeight = 0.0!
        Me.listcode.objMaxWidth = 0.0!
        Me.listcode.objMinHeight = 0.0!
        Me.listcode.objMinWidth = 0.0!
        Me.listcode.reverseSorting = False
        Me.listcode.selected = -1
        Me.listcode.selectedClickAllowed = False
        Me.listcode.selectMultiple = False
        Me.listcode.Size = New System.Drawing.Size(165, 158)
        Me.listcode.sorted = True
        Me.listcode.TabIndex = 99
        Me.listcode.TabStop = False
        Me.listcode.toolTipText = Nothing
        Me.listcode.vScrollColor = System.Drawing.SystemColors.Control
        Me.listcode.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listcode.vScrolling = True
        Me.listcode.vsValue = 0
        '
        'periodes
        '
        Me.periodes.AllowUserToResizeColumns = False
        Me.periodes.AllowUserToResizeRows = False
        Me.periodes.autoSelectOnDataSourceChanged = True
        Me.periodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.periodes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.periodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.periodes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NoCDPeriode, Me.NoCodification, Me.IsEval, Me.IsDefault, Me.NoPeriode, Me.Montant, Me.PourcentAbsence, Me.PourcentClient, Me.KPSelectorColumn, Me.KPColumn, Me.NoKP})
        Me.periodes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.periodes.Location = New System.Drawing.Point(179, 16)
        Me.periodes.MultiSelect = False
        Me.periodes.Name = "periodes"
        Me.periodes.RowHeadersWidth = 21
        Me.periodes.Size = New System.Drawing.Size(537, 158)
        Me.periodes.TabIndex = 101
        Me.periodes.VirtualMode = True
        '
        'NoCDPeriode
        '
        Me.NoCDPeriode.DataPropertyName = "NoCDPeriode"
        DataGridViewCellStyle1.NullValue = "0"
        Me.NoCDPeriode.DefaultCellStyle = DataGridViewCellStyle1
        Me.NoCDPeriode.HeaderText = "NoCDPeriode"
        Me.NoCDPeriode.Name = "NoCDPeriode"
        Me.NoCDPeriode.Visible = False
        '
        'NoCodification
        '
        Me.NoCodification.DataPropertyName = "NoCodification"
        DataGridViewCellStyle2.NullValue = "0"
        Me.NoCodification.DefaultCellStyle = DataGridViewCellStyle2
        Me.NoCodification.HeaderText = "NoCode"
        Me.NoCodification.Name = "NoCodification"
        Me.NoCodification.Visible = False
        '
        'IsEval
        '
        Me.IsEval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        Me.IsEval.DataPropertyName = "IsEval"
        Me.IsEval.HeaderText = "Éval"
        Me.IsEval.MinimumWidth = 31
        Me.IsEval.Name = "IsEval"
        Me.IsEval.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IsEval.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.IsEval.ToolTipText = "Évaluation"
        Me.IsEval.Width = 31
        '
        'IsDefault
        '
        Me.IsDefault.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.IsDefault.DataPropertyName = "IsDefault"
        Me.IsDefault.FillWeight = 40.0!
        Me.IsDefault.HeaderText = "Défaut"
        Me.IsDefault.MinimumWidth = 43
        Me.IsDefault.Name = "IsDefault"
        Me.IsDefault.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IsDefault.Width = 43
        '
        'NoPeriode
        '
        Me.NoPeriode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.NoPeriode.DataPropertyName = "NoPeriode"
        Me.NoPeriode.FillWeight = 81.04578!
        Me.NoPeriode.HeaderText = "Période"
        Me.NoPeriode.Name = "NoPeriode"
        Me.NoPeriode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'Montant
        '
        Me.Montant.DataPropertyName = "Montant"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        Me.Montant.DefaultCellStyle = DataGridViewCellStyle3
        Me.Montant.FillWeight = 39.53453!
        Me.Montant.HeaderText = "Montant"
        Me.Montant.MinimumWidth = 30
        Me.Montant.Name = "Montant"
        Me.Montant.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Montant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PourcentAbsence
        '
        Me.PourcentAbsence.DataPropertyName = "PourcentAbsence"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "P4"
        Me.PourcentAbsence.DefaultCellStyle = DataGridViewCellStyle4
        Me.PourcentAbsence.FillWeight = 60.29016!
        Me.PourcentAbsence.HeaderText = "% Abs. non motivée"
        Me.PourcentAbsence.Name = "PourcentAbsence"
        Me.PourcentAbsence.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PourcentAbsence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PourcentClient
        '
        Me.PourcentClient.DataPropertyName = "PourcentClient"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "P4"
        Me.PourcentClient.DefaultCellStyle = DataGridViewCellStyle5
        Me.PourcentClient.FillWeight = 60.29016!
        Me.PourcentClient.HeaderText = "% du client"
        Me.PourcentClient.Name = "PourcentClient"
        Me.PourcentClient.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PourcentClient.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'KPSelectorColumn
        '
        Me.KPSelectorColumn.DataPropertyName = "Button"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.NullValue = "..."
        Me.KPSelectorColumn.DefaultCellStyle = DataGridViewCellStyle6
        Me.KPSelectorColumn.FillWeight = 20.0!
        Me.KPSelectorColumn.HeaderText = ""
        Me.KPSelectorColumn.Name = "KPSelectorColumn"
        Me.KPSelectorColumn.ReadOnly = True
        Me.KPSelectorColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.KPSelectorColumn.Text = "..."
        Me.KPSelectorColumn.ToolTipText = "Sélectionner"
        Me.KPSelectorColumn.UseColumnTextForButtonValue = True
        Me.KPSelectorColumn.Visible = False
        '
        'KPColumn
        '
        Me.KPColumn.DataPropertyName = "KPName"
        Me.KPColumn.FillWeight = 60.29016!
        Me.KPColumn.HeaderText = "P / O payeur(euse) "
        Me.KPColumn.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline
        Me.KPColumn.LinkColor = System.Drawing.Color.Red
        Me.KPColumn.Name = "KPColumn"
        Me.KPColumn.ReadOnly = True
        Me.KPColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.KPColumn.TrackVisitedState = False
        '
        'NoKP
        '
        Me.NoKP.DataPropertyName = "NoKP"
        Me.NoKP.HeaderText = "NoKP"
        Me.NoKP.Name = "NoKP"
        Me.NoKP.Visible = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'menuKPSelector
        '
        Me.menuKPSelector.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem, Me.AucunToolStripMenuItem})
        Me.menuKPSelector.Name = "menuKPSelector"
        Me.menuKPSelector.ShowImageMargin = False
        Me.menuKPSelector.Size = New System.Drawing.Size(332, 48)
        '
        'SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem
        '
        Me.SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem.Name = "SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem"
        Me.SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem.Size = New System.Drawing.Size(331, 22)
        Me.SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem.Text = "Sélectionner le(la) personne/organisme clé payeur(euse)"
        '
        'AucunToolStripMenuItem
        '
        Me.AucunToolStripMenuItem.Name = "AucunToolStripMenuItem"
        Me.AucunToolStripMenuItem.Size = New System.Drawing.Size(331, 22)
        Me.AucunToolStripMenuItem.Text = "Aucun"
        '
        'folderAlertTypes
        '
        Me.folderAlertTypes.downText = "Cliquer sur la flèche pour cacher la liste"
        Me.folderAlertTypes.dropDownHeight = 220
        Me.folderAlertTypes.droppedDown = False
        Me.folderAlertTypes.expandAllNodes = False
        Me.folderAlertTypes.imageList = Nothing
        Me.folderAlertTypes.Location = New System.Drawing.Point(25, 232)
        Me.folderAlertTypes.Name = "folderAlertTypes"
        Me.folderAlertTypes.pathSeparator = "§"
        Me.folderAlertTypes.readOnly = False
        Me.folderAlertTypes.showLines = False
        Me.folderAlertTypes.showRootLines = False
        Me.folderAlertTypes.Size = New System.Drawing.Size(341, 20)
        Me.folderAlertTypes.sorted = False
        Me.folderAlertTypes.TabIndex = 90
        Me.folderAlertTypes.TabStop = False
        Me.folderAlertTypes.tooltipTitle = "Type(s) de message sélectionné(s) :"
        Me.folderAlertTypes.tree = Nothing
        Me.folderAlertTypes.upText = "Cliquer sur la flèche pour sélectionner des types de message"
        '
        'folderTextTypes
        '
        Me.folderTextTypes.downText = "Cliquer sur la flèche pour cacher la liste"
        Me.folderTextTypes.dropDownHeight = 220
        Me.folderTextTypes.droppedDown = False
        Me.folderTextTypes.expandAllNodes = False
        Me.folderTextTypes.imageList = Nothing
        Me.folderTextTypes.Location = New System.Drawing.Point(380, 232)
        Me.folderTextTypes.Name = "folderTextTypes"
        Me.folderTextTypes.pathSeparator = "§"
        Me.folderTextTypes.readOnly = False
        Me.folderTextTypes.showLines = False
        Me.folderTextTypes.showRootLines = False
        Me.folderTextTypes.Size = New System.Drawing.Size(341, 20)
        Me.folderTextTypes.sorted = False
        Me.folderTextTypes.TabIndex = 91
        Me.folderTextTypes.TabStop = False
        Me.folderTextTypes.tooltipTitle = "Type(s) de texte sélectionné(s) :"
        Me.folderTextTypes.tree = Nothing
        Me.folderTextTypes.upText = "Cliquer sur la flèche pour sélectionner des types de texte"
        '
        'codifications
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(748, 536)
        Me.Controls.Add(Me.folderTextTypes)
        Me.Controls.Add(Me.folderAlertTypes)
        Me.Controls.Add(Me.frame2)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(443, 342)
        Me.MaximizeBox = False
        Me.Name = "codifications"
        Me.ShowInTaskbar = False
        Me.Text = "Codification dossier"
        Me.FrameOptions.ResumeLayout(False)
        Me.FrameOptions.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tabFolder.ResumeLayout(False)
        Me.tabFolder.PerformLayout()
        Me.tabRV.ResumeLayout(False)
        Me.tabRV.PerformLayout()
        Me.tabBill.ResumeLayout(False)
        Me.tabBill.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.frame2.ResumeLayout(False)
        Me.frameCode.ResumeLayout(False)
        CType(Me.periodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuKPSelector.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private _kpChosen As KPSelectorReturn = Nothing
    Private lastKPSelectorButtonIndex As Integer = -1
    Private defaultPeriodes As New DataTable
    Private myCodes As Generic.List(Of FolderCode) = FolderCodesManager.getInstance.getItemables(0, LIMIT_DATE)
    Private Sel, loadingPeriodes As Boolean
    Private curTRP As User = UserDefault.getInstance()
    Private _FormModified As Boolean = False
    Private isChanging As Boolean = False
    Private _allowModification As Boolean = True
    Private hasJustCanceled As Boolean = False
    Private isSelecting As Boolean = False
    Private selectedCode As FolderCode
    Private selectedDate As Date = Date.Today

    Private Property allowModification() As Boolean
        Get
            Return _allowModification
        End Get
        Set(ByVal value As Boolean)
            _allowModification = value
            lockItems(Not value)
        End Set
    End Property

    Private Property formModified() As Boolean
        Get
            Return _FormModified
        End Get
        Set(ByVal value As Boolean)
            If value Then
                value = value
            End If
            _FormModified = value
        End Set
    End Property

#Region "Événements principaux"

    Private Sub codifications_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If allowModification = True Then
            If Not askWhenModified() Then
                e.Cancel = True
                Exit Sub
            End If

            lockSecteur("CodificationsDossiers.lock", False)
        End If
    End Sub

    Private Sub codifications_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim a As Integer = 0
    End Sub

    Protected Overrides Sub onKeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        'REM Reactivate when I'll be able to handle the Canceling a cell by ESC doesn't close the window
        'If hasJustCanceled = False AndAlso isChanging = False AndAlso periodes.IsCurrentRowDirty = False AndAlso e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
        'hasJustCanceled = False
    End Sub

    Private Sub codifications_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Droit & Accès
        If Not isSelecting AndAlso currentDroitAcces(29) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier les codifications dossier." & vbCrLf & "Merci!", "Droit & Accès")
            allowModification = False
        End If

        If allowModification AndAlso lockSecteur("CodificationsDossiers.lock", True, "Codification dossier") = False Then allowModification = False
        confirmation.SelectedIndex = 0
        selectCode.Enabled = isSelecting

        'Ensure firstEffectiveDate is at least tomorrow
        firstEffectiveTime.Text = DateFormat.getTextDate(Date.Today.AddDays(1))

        'Load périodes list
        With CType(periodes.Columns("NoPeriode"), DataGridViewComboBoxColumn)
            .DataSource = DBLinker.getInstance.readDBForGrid("ListePeriode", "*", "WHERE TotalMinutes<=120").Tables(0)
            .DisplayMember = "Periode"
            .ValueMember = "NoPeriode"
        End With

        periodes.Rows(0).Cells("NoPeriode").Value = 1

        'Load FTT
        folderTextTypes.tree = FolderTextTypesManager.getInstance.getItemables().ToArray()
        folderTextTypes.refreshTree()

        'Load FAT
        folderAlertTypes.tree = FolderAlertTypesManager.getInstance.getItemables().ToArray()
        folderAlertTypes.refreshTree()

        'Load payment method
        defaultPaymentMethod.Items.AddRange(BillsManager.getInstance.getPaymentTypes().ToArray())
        defaultPaymentMethod.Items.Insert(0, "* Aucune méthode de paiement par défaut *")
        defaultPaymentMethod.SelectedIndex = 0

        'Load Thérapeute
        therapeute.Items.Add(UserDefault.getInstance())
        therapeute.Items.AddRange(UsersManager.getInstance.getUsers(False, True).ToArray())

        'Load external statuses
        startingExternalStatus.Items.AddRange(Accounts.Clients.Folders.Codifications.ExternalStatuses.getInstance.statuses.ToArray())
        If startingExternalStatus.Items.Count <> 0 Then startingExternalStatus.SelectedIndex = 0

        formModified = False 'This have to be there otherwise a saving question could be asked even if not necessary
        If curTRP.noUser <> 0 Then therapeute.SelectedItem = curTRP
        If therapeute.SelectedIndex < 0 Then therapeute.SelectedIndex = 0

        myCodes = FolderCodesManager.getInstance.getItemables(curTRP.noUser, LIMIT_DATE)

        loadDates()
    End Sub


    Private Sub codifications_Saving(ByVal sender As Object, ByVal e As SingleWindowSaveEventArgs)
        If allowModification = True Then
            If formModified = True Then
                Dim reason As String = saveCode(False)
                If reason <> "" Then
                    e.cancel(reason)
                    Exit Sub
                End If
            End If
            lockSecteur("CodificationsDossiers.lock", False)
        End If
    End Sub

    Private Sub codifications_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        'REM Is this really useful ?
        formModified = False
    End Sub
#End Region

    Private Function setValuesToCode(ByVal curCode As FolderCode, Optional ByVal showMessages As Boolean = True) As String
        If curCode Is Nothing Then Return ""

        With periodes
            For i As Integer = 0 To .Rows.Count - 2
                Dim isAnEval As Boolean = Not TypeOf .Rows(i).Cells("IsEval").Value Is DBNull AndAlso .Rows(i).Cells("IsEval").Value
                Dim nomPeriode As String = CType(CType(Me.periodes.Columns("NoPeriode"), DataGridViewComboBoxColumn).DataSource, DataTable).Select("NoPeriode=" & .Rows(i).Cells("NoPeriode").Value)(0)("Periode")

                If .Rows(i).Cells("Montant").Value Is DBNull.Value Then .Rows(i).Cells("Montant").Value = 0
                If .Rows(i).Cells("Montant").Value = 0 Then
                    If showMessages AndAlso MessageBox.Show("Êtes-vous sûr de vouloir le montant " & IIf(isAnEval, "de l'évaluation", "du traitement") & " de " & nomPeriode & " de cette codification à zéro ?", "Montant évaluation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                        .ClearSelection()
                        .Rows(i).Cells("Montant").Selected = True
                        .BeginEdit(True)
                        Return "Montant d'une période à 0$"
                    ElseIf showMessages = False Then
                        Return "Montant d'une période à 0$"
                    End If
                End If
                If .Rows(i).Cells("PourcentClient").Value < 1 AndAlso .Rows(i).Cells("NoKP").Value = 0 Then
                    .ClearSelection()
                    .Rows(i).Cells("KPColumn").Selected = True
                    If showMessages Then MessageBox.Show("Vous devez nécessairement choisir un(e) personne / organisme clé pour sélectionner" & vbCrLf & "un pourcentage de paiement différent de 100 % pour " & IIf(isAnEval, "l'évaluation", "le traitement") & " de " & nomPeriode, "Personne / organisme clé(e) manquant(e)")
                    Return "Période, dont le pourcentage payé par le client n'est pas 100%, qui n'a pas de personne / organisme clé associé"
                End If
            Next i
        End With

        Dim oldDate As Date = curCode.firstEffectiveTime
        curCode.periods = Me.periodes.DataSource
        curCode.askReceipt = recu.Checked
        curCode.autoSelectBillWhenPaying = autoSelectBillWhenPaying.Checked
        curCode.confirmReference = msgnoref.Checked
        curCode.confirmDiagnostic = msgDiagnostic.Checked
        curCode.accidentDate = dateAccident.Checked
        curCode.relaspeDate = dateRechute.Checked
        curCode.autoShowPayment = autoShowPayment.Checked
        curCode.askPourcentage = affPourcentAllTimes.Checked
        curCode.confirmation = confirmation.SelectedIndex
        curCode.treatmentPonderation = ponderationVisite.Text
        curCode.evaluationPonderation = ponderationEval.Text
        If defaultPaymentMethod.SelectedIndex = 0 Then
            curCode.defaultPaymentMethod = ""
        Else
            curCode.defaultPaymentMethod = defaultPaymentMethod.Text
        End If
        curCode.authorizationProcessActivated = demandeAuthorisation.Checked
        curCode.notConfirmRVOnPasteOfDTRP = notConfirmRVOnPasteOfDTRP.Checked
        If date1Infdate2(Date.Today, Date.Parse(Me.firstEffectiveTime.Text)) Then curCode.firstEffectiveTime = Date.Parse(Me.firstEffectiveTime.Text)

        If curCode.do_NotShared_HaveChanged AndAlso date1Infdate2(oldDate, Date.Today, True) Then
            If date1Infdate2(curCode.firstEffectiveTime, Date.Today, True) Then curCode.firstEffectiveTime = Date.Today.AddDays(1)
            If showMessages = False OrElse (curCode.noCodification <> 0 AndAlso Not curCode.isTherapistDifferent() AndAlso MessageBox.Show("L'enregistrement de cette codification créera une nouvelle copie de celle-ci qui sera effective à partir du " & DateFormat.getTextDate(curCode.firstEffectiveTime) & "." & vbCrLf & "Êtes-vous sûr de vouloir procéder (réversible tant que la date d'application n'est pas atteinte) ?", "Confirmation d'enregistrement", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = System.Windows.Forms.DialogResult.No) Then
                curCode.revertChanges()
                Return "Enregistrement à confirmer"
            End If
        End If

        curCode.folderTexteTypes = folderTextTypes.getSelected(Of FolderTextType)(False)
        curCode.folderAlertTypes = folderAlertTypes.getSelected(Of FolderAlertType)(False)

        If startingExternalStatus.SelectedItem IsNot Nothing Then curCode.startingExternalStatus = CType(startingExternalStatus.SelectedItem, ExternalStatus).noExternalStatus


        Return ""
    End Function

    Private Function saveCode(Optional ByVal showMessages As Boolean = True) As String
        Dim curCode As FolderCode = listcode.ItemValueA(listcode.selected)
        If listcode.selected < 0 OrElse curCode Is Nothing Then Return ""

        Dim returning As String = setValuesToCode(curCode, showMessages)
        If returning <> "" Then Return returning

        curCode.saveData()

        loadDates()

        formModified = False

        Return ""
    End Function

    Public Shared Function chooseCode(ByVal noTRP As Integer, ByVal selectedDate As Date) As FolderCode
        Dim myCodifications As New codifications()
        With myCodifications
            .isSelecting = True
            .curTRP = UsersManager.getInstance.getUser(noTRP)
            If .curTRP Is Nothing Then .curTRP = UserDefault.getInstance
            .MdiParent = Nothing
            .StartPosition = FormStartPosition.CenterScreen
            .allowModification = False
            .selectCode.Enabled = True
            .selectedDate = selectedDate
            .dates.Enabled = False
            .therapeute.Enabled = False
            .ShowDialog()
        End With

        Return myCodifications.selectedCode
    End Function

    <DebuggerStepThrough()> _
    Private Sub lockItem(ByVal trueFalse As Boolean, ByVal curControl As Object)
        Try
            curControl.ReadOnly = trueFalse
        Catch ex As MissingMemberException
            'Property doesn't exist, then use other instead
            CType(curControl, Control).Enabled = Not trueFalse
        End Try
    End Sub

    <DebuggerHidden()> _
    Private Sub lockItem2(ByVal trueFalse As Boolean, ByVal curControl As Object)
        Try
            curControl.ReadOnly = trueFalse
        Catch ex As MissingMemberException
            'Property doesn't exist, then use other instead
            CType(curControl, Control).Enabled = Not trueFalse
        End Try
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        For Each curTab As TabPage In TabControl1.TabPages
            For Each curControl As Object In curTab.Controls
                lockItem2(trueFalse, curControl)
            Next
        Next
        periodes.Enabled = Not trueFalse
        folderTextTypes.[readOnly] = trueFalse
        folderAlertTypes.readOnly = trueFalse
        selectFirstDate.Enabled = Not trueFalse
        add_Renamed.Enabled = Not trueFalse
        save.Enabled = Not trueFalse
        delete.Enabled = Not trueFalse
        save.Enabled = Not trueFalse
        copyAll.Enabled = Not trueFalse
        setDefaultAll.Enabled = Not trueFalse
        copy.Enabled = Not trueFalse
        setDefault.Enabled = curTRP.noUser <> 0 AndAlso Not trueFalse
        ponderationEval.ReadOnly = trueFalse
        ponderationVisite.ReadOnly = trueFalse
    End Sub

    Private Sub add_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles add_Renamed.Click
        Dim curUser As User = CType(therapeute.SelectedItem, User)
        Dim curCode As FolderCode = listcode.ItemValueA(listcode.selected)
        Dim newName As String = "", defName As String = ""
        If curCode Is Nothing Then
            curCode = New FolderCode()
        Else
            If curCode.noUser <> curUser.noUser Then
                newName = curCode.name
                defName = newName
            End If
            curCode = curCode.clone(curUser.noUser)
        End If
        If setValuesToCode(curCode) <> "" Then Exit Sub

        'Ask for new name
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = "-"
        myInputBoxPlus.maxLength = 200
        newName = myInputBoxPlus.Prompt("Entrez un nom pour la nouvelle codification", "Codification", newName)
        If newName = "" Then Exit Sub

        If defName <> newName Then
            For i As Integer = 0 To listcode.listCount
                If listcode.ItemText(i).ToUpper = newName.ToUpper Then
                    MessageBox.Show("Une codification porte déjà ce nom. Veuillez en sélectionner un autre.", "Impossible d'ajouter la codification")
                    Exit Sub
                End If
            Next i
        End If

        curCode.name = newName
        'defName <> "" AndAlso 
        Dim resetNoUnique As Boolean = defName <> newName
        curCode.saveData(resetNoUnique)

        selectedDate = curCode.firstEffectiveTime

        loadDates()
        listcode.selected = listcode.findStringExact(newName)
        listcode.showItem(listcode.selected)
        formModified = False
    End Sub

    Private Function askWhenModified() As Boolean
        If formModified = True Then
            Select Case MessageBox.Show("Désirez-vous enregistrer les modifications de la codification en cours ?", "Enregistrement", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Case System.Windows.Forms.DialogResult.Yes
                    If saveCode() <> "" Then Return False
                Case System.Windows.Forms.DialogResult.No
                    If listcode.ItemValueA(listcode.selected) IsNot Nothing Then CType(listcode.ItemValueA(listcode.selected), FolderCode).periods.RejectChanges()
                Case System.Windows.Forms.DialogResult.Cancel
                    Return False
            End Select
        End If

        Return True
    End Function

    Private Function selectCopyingTRP() As Integer
        Dim msgReturn As String, Choices As String = ""
        Dim i As Integer
        For i = 1 To therapeute.Items.Count - 1
            If Not therapeute.SelectedIndex = i Then Choices &= "§" & therapeute.GetItemText(therapeute.Items.Item(i))
        Next i
        Choices = Choices.Substring(1)

        Dim myMultiChoice As New multichoice()
        msgReturn = myMultiChoice.GetChoice("Sélectionner le thérapeute de destination", Choices, , "§", True)

        Return If(msgReturn.StartsWith("ERROR"), -1, User.extractNo(msgReturn))
    End Function

    Private Sub copyAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles copyAll.Click
        If Not askWhenModified() Then Exit Sub

        Dim noTRP As Integer = selectCopyingTRP()
        If noTRP = -1 Then Exit Sub

        Dim codesNamesInError As String = ""
        Dim codesNamesCopied As String = ""
        Dim codesToCopy As New Generic.List(Of FolderCode)
        For Each curListItem As CI.Controls.IListItem In listcode.items
            Dim curCode As FolderCode = curListItem.ValueA
            If curCode.noUser = curTRP.noUser Then codesToCopy.Add(curCode)
        Next


        InternalUpdatesManager.getInstance.startBatchUpdate()
        For Each curCode As FolderCode In codesToCopy
            Try
                curCode.copyTo(noTRP)
                codesNamesCopied &= "§" & curCode.name
            Catch ex As DBItemableUncopiable
                codesNamesInError &= "§" & curCode.name
            End Try
        Next
        InternalUpdatesManager.getInstance.stopBatchUpdate()

        If codesNamesInError <> "" Then
            codesNamesInError = codesNamesInError.Substring(1)
            Dim isPlural As Boolean = codesNamesInError.IndexOf("§") <> -1
            codesNamesInError = codesNamesInError.Replace("§", ", ")
            If isPlural Then codesNamesInError = codesNamesInError.Substring(0, codesNamesInError.LastIndexOf(",")) & " et" & codesNamesInError.Substring(codesNamesInError.LastIndexOf(",") + 1)
            MessageBox.Show("Impossible de copier l" & If(isPlural, "es", "a") & " codification" & If(isPlural, "s", "") & " " & codesNamesInError & " pour le thérapeute demandé, car il y en a déjà de même nom et de même date de début d'application", "Impossible de copier", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        If codesNamesCopied <> "" Then
            codesNamesCopied = codesNamesCopied.Substring(1)
            Dim isPlural As Boolean = codesNamesCopied.IndexOf("§") <> -1
            codesNamesCopied = codesNamesCopied.Replace("§", ", ")
            If isPlural Then codesNamesCopied = codesNamesCopied.Substring(0, codesNamesCopied.LastIndexOf(",")) & " et" & codesNamesCopied.Substring(codesNamesCopied.LastIndexOf(",") + 1)

            myMainWin.StatusText = "Codifications dossiers : L" & If(isPlural, "es", "a") & " codification" & If(isPlural, "s", "") & " " & codesNamesCopied & " " & If(isPlural, "ont", "a") & " été copié" & If(isPlural, "s", "") & " depuis le thérapeute " & curTRP.toString & " vers le thérapeute " & UsersManager.getInstance.getUser(noTRP).toString
        End If
    End Sub

    Private Sub setDefaultAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles setDefaultAll.Click
        __setDefault(0, "Attention ! Cette procédure efface toutes les codifications futures du thérapeute sélectionné et inscrit les autres comme terminés." & vbCrLf & "Êtes-vous sûr de vouloir remettre par défaut toutes les codifications du thérapeute sélectionné ?", "Codification dossier  : Le thérapeute " & curTRP.toString & " a été remis par défaut")
    End Sub

    Private Sub delete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles delete.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir enlever cette codification ?", "Codification client", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        'REM_CODES
        CType(listcode.ItemValueA(listcode.selected), FolderCode).delete()

        Me.periodes.DataSource = CType(periodes.DataSource, DataTable).Clone

        loadDates()
        formModified = False
    End Sub

    Public Sub loadingData()
        myCodes = FolderCodesManager.getInstance.getItemables(curTRP.noUser, CType(dates.SelectedItem, DateInterval).from)

        Dim lastCodeName As String = listcode.ItemText(listcode.selected)
        periodes.DataSource = defaultPeriodes
        listcode.cls()
        save.Enabled = False
        delete.Enabled = False

        For Each curCode As FolderCode In myCodes
            Dim myNo As Short = listcode.add(curCode.toString)
            listcode.ItemValueA(myNo) = curCode
            If curCode.noUser = 0 And therapeute.SelectedIndex <> 0 Then listcode.ItemIconsShowed(myNo, 0) = True
        Next

        'Reset has to be done before redrawing, because it is calling WillSelect event
        formModified = False

        listcode.draw = True : listcode.draw = False
        If listcode.listCount > 0 Then
            listcode.selected = listcode.findStringExact(lastCodeName)
            If listcode.selected = -1 Then listcode.selected = listcode.findFirstItem()
            listcode.showItem(listcode.selected)
        End If
    End Sub

    Private Sub save_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles save.Click
        saveCode()
    End Sub

    Private Sub selectionner_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectCode.Click
        If listcode.selected < 0 Then MessageBox.Show("Veuillez sélectionner une codification dossier", "Information manquante")
        selectedCode = listcode.ItemValueA(listcode.selected)
        Me.Close()
    End Sub

    Private Sub therapeute_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles therapeute.SelectedIndexChanged
        If therapeute.SelectedItem.Equals(curTRP) Then Exit Sub 'Reselection due to cancel upon saving

        If Not askWhenModified() Then
            therapeute.SelectedItem = curTRP
            Exit Sub
        End If

        curTRP = therapeute.SelectedItem

        loadDates()
    End Sub

    Private Sub selectDate(ByVal selectedDate As Date)
        For Each curInterval As DateInterval In dates.Items
            If curInterval.isDateBetween(selectedDate) Then
                dates.SelectedItem = curInterval
                Exit For
            End If
        Next

        If dates.SelectedIndex = -1 Then dates.SelectedIndex = 0
    End Sub

    Private Sub loadDates()
        Dim curSelectedCode As Integer = listcode.selected

        dates.Items.Clear()
        dates.Items.AddRange(FolderCodesManager.getInstance.getAllDatesOfUser(curTRP.noUser).ToArray)
        selectDate(selectedDate)

        listcode.selected = If(curSelectedCode = -1 AndAlso listcode.listCount <> 0, listcode.findFirstItem(), curSelectedCode)
        listcode_SelectedChange()
    End Sub

    Private Sub listcode_DblClick(ByVal sender As Object, ByVal e As CI.Controls.List.DblClickEventArgs) Handles listcode.dblClick
        If isSelecting Then Me.selectionner_Click(sender, e)
    End Sub

    Private Sub listcode_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles listcode.KeyUp
        Select Case e.keyCode
            Case 37 'Gauche
                If Me.therapeute.Enabled AndAlso Me.therapeute.SelectedIndex > 0 Then Me.therapeute.SelectedIndex -= 1
            Case 39 'Droite
                If Me.therapeute.Enabled AndAlso Me.therapeute.SelectedIndex < (Me.therapeute.Items.Count - 1) Then Me.therapeute.SelectedIndex += 1
            Case 13
                If Me.selectCode.Enabled Then Me.selectionner_Click(Me, EventArgs.Empty)
        End Select
    End Sub

    Private Sub setCheckBoxCombo(ByVal list As Generic.List(Of IItemable), ByVal control As TreeViewComboPlus)
        Dim selected As New Generic.List(Of String)

        For Each curElement As IItemable In list
            selected.Add(curElement.ToString)
        Next

        control.setSelected(selected.ToArray)
    End Sub

    Private Sub listcode_SelectedChange() Handles listcode.selectedChange
        If listcode.selected = -1 Then
            Me.firstEffectiveTime.Text = DateFormat.getTextDate(Date.Today.AddDays(1))
            save.Enabled = False
            delete.Enabled = False
            copy.Enabled = False
            setDefault.Enabled = False
            Exit Sub
        End If

        loadingPeriodes = True
        Dim curCode As FolderCode = listcode.ItemValueA(listcode.selected)
        periodes.RefreshEdit()
        periodes.DataSource = curCode.periods
        periodes.Sort(periodes.Columns(2), System.ComponentModel.ListSortDirection.Descending)

        Dim firstDate As Date = curCode.firstEffectiveTime
        Dim lastDate As Date = curCode.lastEffectiveTime

        'Ensure default codification showed in specific TRP shows appropriate dates
        If curCode.noUser <> curTRP.noUser Then
            Dim prevCode As FolderCode = FolderCodesManager.getInstance.getPreviousCode(curCode.noUnique, curTRP.noUser, selectedDate)
            If prevCode IsNot Nothing Then firstDate = prevCode.lastEffectiveTime.AddDays(1)

            Dim nextCode As FolderCode = FolderCodesManager.getInstance.getNextCode(curCode.noUnique, curTRP.noUser, selectedDate)
            If nextCode IsNot Nothing Then lastDate = nextCode.firstEffectiveTime.AddDays(-1)
        End If

        firstEffectiveTime.Text = DateFormat.getTextDate(firstDate)
        lastEffectiveTime.Text = If(lastDate = LIMIT_DATE, "Aucune", DateFormat.getTextDate(lastDate))
        recu.Checked = curCode.askReceipt
        autoSelectBillWhenPaying.Checked = curCode.autoSelectBillWhenPaying
        msgnoref.Checked = curCode.confirmReference
        msgDiagnostic.Checked = curCode.confirmDiagnostic
        dateAccident.Checked = curCode.accidentDate
        dateRechute.Checked = curCode.relaspeDate
        autoShowPayment.Checked = curCode.autoShowPayment
        affPourcentAllTimes.Checked = curCode.askPourcentage
        confirmation.SelectedIndex = curCode.confirmation
        ponderationEval.Text = curCode.evaluationPonderation
        ponderationVisite.Text = curCode.treatmentPonderation
        If curCode.defaultPaymentMethod = "" Then
            defaultPaymentMethod.SelectedIndex = 0
        Else
            If defaultPaymentMethod.Items.IndexOf(curCode.defaultPaymentMethod) = -1 Then
                defaultPaymentMethod.SelectedIndex = 0
            Else
                defaultPaymentMethod.SelectedIndex = defaultPaymentMethod.Items.IndexOf(curCode.defaultPaymentMethod)
            End If
        End If
        demandeAuthorisation.Checked = curCode.authorizationProcessActivated
        notConfirmRVOnPasteOfDTRP.Checked = curCode.notConfirmRVOnPasteOfDTRP

        setCheckBoxCombo(curCode.folderTexteTypesAsIItemable, folderTextTypes)
        setCheckBoxCombo(curCode.folderAlertTypesAsIItemable, folderAlertTypes)

        startingExternalStatus.SelectedItem = ExternalStatuses.getInstance.getStatus(curCode.startingExternalStatus)

        lockItems(Not (allowModification = True AndAlso curCode.noUser = curTRP.noUser AndAlso CType(dates.SelectedItem, Clinica.DateInterval).to.Date > Date.Today))
        add_Renamed.Enabled = allowModification AndAlso CType(dates.SelectedItem, Clinica.DateInterval).to.Date > Date.Today
        setDefaultAll.Enabled = allowModification AndAlso curTRP.noUser <> 0 AndAlso Not FolderCodesManager.getInstance.isUserDefault(curTRP.noItemable, CType(dates.SelectedItem, DateInterval).from)

        formModified = False
        loadingPeriodes = False
    End Sub

    Private Sub checkBoxes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles recu.CheckedChanged, autoSelectBillWhenPaying.CheckedChanged, msgnoref.CheckedChanged, msgDiagnostic.CheckedChanged, dateAccident.CheckedChanged, dateRechute.CheckedChanged, autoShowPayment.CheckedChanged, affPourcentAllTimes.CheckedChanged, demandeAuthorisation.CheckedChanged, notConfirmRVOnPasteOfDTRP.CheckedChanged
        formModified = True
    End Sub

    Private Sub textBoxes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ponderationEval.TextChanged, ponderationVisite.TextChanged
        formModified = True
    End Sub

    Private Sub lists_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles confirmation.SelectedIndexChanged, defaultPaymentMethod.SelectedIndexChanged, startingExternalStatus.SelectedIndexChanged
        formModified = True
    End Sub

    Private Sub listcode_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles listcode.willSelect
        If Me.allowModification = False OrElse Me.periodes.currentCell Is Nothing Then Exit Sub

        If Me.periodes.currentCell.IsInEditMode Then e.cancel = Not Validate()
        If e.cancel = True Then Exit Sub

        If Not askWhenModified() Then
            e.cancel = True
            Exit Sub
        End If

        formModified = False
    End Sub

    Private Sub periodes_CancelRowEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.QuestionEventArgs) Handles periodes.CancelRowEdit
        hasJustCanceled = True
    End Sub

    Private Sub periodes_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles periodes.CellBeginEdit
        If periodes.Rows(e.RowIndex).IsNewRow Then Exit Sub

        isChanging = True
    End Sub

    Private Sub periodes_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles periodes.CellClick
        lastKPSelectorButtonIndex = -1
        If e.ColumnIndex < 8 Then periodes.Columns("KPSelectorColumn").Visible = False

        If e.ColumnIndex = 8 And e.RowIndex <> -1 Then
            lastKPSelectorButtonIndex = e.RowIndex
            menuKPSelector.Show(Control.MousePosition)
        End If
        If e.ColumnIndex = 9 Then
            periodes.Columns("KPSelectorColumn").Visible = True
        End If
    End Sub

    Private Sub periodes_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles periodes.CellContentClick
        If e.ColumnIndex = 9 AndAlso e.RowIndex <> -1 AndAlso periodes.Rows(e.RowIndex).Cells(10).Value IsNot DBNull.Value AndAlso periodes.Rows(e.RowIndex).Cells(10).Value <> 0 Then openAccount(periodes.Rows(e.RowIndex).Cells(10).Value, CompteType.KP)
        If curCell.RowIndex = e.RowIndex And curCell.ColumnIndex = e.ColumnIndex Then
            periodes.BeginEdit(True)
        End If
    End Sub

    Private Sub periodes_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles periodes.CellEndEdit
        If periodes.Rows(e.RowIndex).IsNewRow Then Exit Sub

        isChanging = False
    End Sub

    Private curCell As DataGridViewCell

    Private Sub periodes_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles periodes.CellEnter
        curCell = periodes.Rows(e.RowIndex).Cells(e.ColumnIndex)
    End Sub

    Private Sub periodes_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles periodes.CellMouseClick
        If curCell.RowIndex = e.RowIndex And curCell.ColumnIndex = e.ColumnIndex Then
            periodes.BeginEdit(True)
        End If
    End Sub

    Private Sub periodes_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles periodes.CellValidating
        If isChanging = False Then Exit Sub

        e.Cancel = validateCells(e.RowIndex, e.ColumnIndex, e.FormattedValue)

        isChanging = e.Cancel

        Try
            If Me.loadingPeriodes = False AndAlso e.Cancel = False AndAlso e.ColumnIndex = 2 Then periodes.Sort(periodes.Columns(2), System.ComponentModel.ListSortDirection.Descending)
        Catch
        End Try

        If e.Cancel = False And periodes.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue <> e.FormattedValue Then formModified = True
    End Sub

    Private Function validateCells(ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal formattedValue As Object) As Boolean
        'REM CODE
        'Return False

        'This code shouldn't happen because protections shall ensure that it always exists two entries, though.. sometimes I had
        'Actually, its three because it's counting the Add row.
        If periodes.Rows.Count < 3 Then Return False

        Dim canceling As Boolean = False
        Dim total As Integer = periodes.Rows.Count - 2
        'If periodes.Rows(RowIndex).IsNewRow = False Then Total -= 1

        If columnIndex = 5 AndAlso formattedValue = "" Then periodes.Rows(rowIndex).Cells(columnIndex).Value = 0
        If columnIndex = 5 AndAlso formattedValue <> "" Then
            Try
                formattedValue = CDbl(formattedValue)
            Catch ex As InvalidCastException
                MessageBox.Show("Veuillez entrer un montant valide", "Montant invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
                formattedValue = periodes.Rows(rowIndex).Cells(columnIndex).Value
            End Try
            periodes.EndEdit()
            periodes.Rows(rowIndex).Cells(columnIndex).Value = formattedValue
            If formattedValue < 0 Then periodes.Rows(rowIndex).Cells(columnIndex).Value = 0
        End If

        If columnIndex = 3 Or columnIndex = 2 Then
            'quitte si IsDefault est nulle
            '            If periodes.Rows(RowIndex).Cells("IsDefault").Value Is DBNull.Value Then Exit Sub

            'If ColumnIndex = 5 AndAlso periodes.Rows(RowIndex).Cells("IsDefault").FormattedValue = False Then
            '    MessageBox.Show("Vous devez toujours avoir au moins un" & IIf(periodes.Rows(RowIndex).Cells("IsEval").FormattedValue = True, "e évaluation", " traitement") & " par défaut", "Défaut")
            '    periodes.Rows(RowIndex).Cells("IsDefault").Value = True
            '    Exit Sub
            'End If

            'Décoche le 2ième par défaut d'une éval ou d'un traitement
            If (columnIndex = 4 And formattedValue = True) Or (columnIndex = 2 And periodes.Rows(rowIndex).Cells("IsDefault").FormattedValue = True) Then
                For i As Integer = 0 To periodes.Rows.Count - 1
                    If i <> rowIndex AndAlso periodes.Rows(rowIndex).Cells("IsEval").FormattedValue = periodes.Rows(i).Cells("IsEval").FormattedValue And periodes.Rows(i).Cells("IsDefault").FormattedValue = True Then
                        periodes.Rows(i).Cells("IsDefault").Value = False
                        Exit For
                    End If
                Next i
            End If

            'S'assure qu'il existe au moins un par défaut pour le type de rv en cours (éval ou traitement)
            If (columnIndex = 2 And periodes.Rows(rowIndex).Cells("IsDefault").FormattedValue = False) Or (columnIndex = 3 And formattedValue = False) Then
                Dim hasDef As Boolean = False
                For i As Integer = 0 To periodes.Rows.Count - 1
                    If i <> rowIndex AndAlso periodes.Rows(rowIndex).Cells("IsEval").FormattedValue = periodes.Rows(i).Cells("IsEval").FormattedValue And periodes.Rows(i).Cells("IsDefault").FormattedValue = True Then
                        hasDef = True
                        Exit For
                    End If
                Next i
                If hasDef = False And columnIndex <> 3 Then periodes.Rows(rowIndex).Cells("IsDefault").Value = True
                If hasDef = False And columnIndex = 3 Then
                    MessageBox.Show("Vous devez toujours avoir au moins un" & IIf(periodes.Rows(rowIndex).Cells("IsEval").FormattedValue = True, "e évaluation", " traitement") & " par défaut", "Défaut")
                    canceling = True
                End If
            End If

            'S'assure qu'il existe au moins un par défaut pour l'autre type de rv en cours (éval ou traitement)
            If (columnIndex = 2 Or columnIndex = 3) Then
                Dim hasDef As Boolean = False
                Dim first As Integer = -1
                For i As Integer = 0 To periodes.Rows.Count - 1
                    If i <> rowIndex AndAlso periodes.Rows(rowIndex).Cells("IsEval").FormattedValue <> periodes.Rows(i).Cells("IsEval").FormattedValue Then
                        If first = -1 Then first = i
                        If periodes.Rows(i).Cells("IsDefault").FormattedValue = True Then
                            hasDef = True
                            Exit For
                        End If
                    End If
                Next i
                If hasDef = False And first <> -1 Then periodes.Rows(first).Cells("IsDefault").Value = True
            End If
        End If

        'Vérifie qu'il n'existe pas d'éval ou de traitement du même période
        If columnIndex = 4 Then
            For i As Integer = 0 To total
                If i <> rowIndex AndAlso formattedValue = periodes.Rows(i).Cells("NoPeriode").FormattedValue And periodes.Rows(i).Cells("IsEval").FormattedValue = periodes.Rows(rowIndex).Cells("IsEval").FormattedValue Then
                    MessageBox.Show("Vous ne pouvez avoir qu'un" & IIf(periodes.Rows(rowIndex).Cells("IsEval").FormattedValue = True, "e évaluation", " traitement") & " par période", "Période identique")
                    canceling = True
                    Exit For
                End If
            Next i
        End If

        If columnIndex = 2 Then
            For i As Integer = 0 To total
                If i <> rowIndex AndAlso periodes.Rows(rowIndex).Cells("NoPeriode").FormattedValue = periodes.Rows(i).Cells("NoPeriode").FormattedValue And periodes.Rows(i).Cells("IsEval").FormattedValue = formattedValue Then
                    MessageBox.Show("Vous ne pouvez avoir qu'un" & IIf(formattedValue = True, "e évaluation", " traitement") & " par période", "Période identique")
                    canceling = True
                    Exit For
                End If
            Next i
        End If

        'Vérifie qu'il existe au moins une évaluation et un traitement
        If columnIndex = 2 Then
            Dim oneEval, oneTrait As Boolean
            For i As Integer = 0 To total
                If i <> rowIndex And periodes.Rows(i).Cells("IsEval").FormattedValue = True Then oneEval = True
                If i <> rowIndex And periodes.Rows(i).Cells("IsEval").FormattedValue = False Then oneTrait = True
            Next i
            If formattedValue = True Then
                oneEval = True
            Else
                oneTrait = True
            End If

            If oneEval = False Then
                MessageBox.Show("Vous devez avoir au moins une évaluation", "Période d'évaluation manquante")
                canceling = True
            End If
            If oneTrait = False Then
                MessageBox.Show("Vous devez avoir au moins un traitement", "Période de traitement manquant")
                canceling = True
            End If
        End If

        'Ensure good pourcentage modification
        If columnIndex = 6 Or columnIndex = 7 Then
            Try
                formattedValue = CDbl(formattedValue.ToString().Replace("%", ""))
            Catch ex As InvalidCastException
                MessageBox.Show("Veuillez entrer un pourcentage valide", "Pourcentage invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
                formattedValue = periodes.Rows(rowIndex).Cells(columnIndex).Value * 100
            End Try
            'If formattedValue Is Nothing Then formattedValue = 0
            'formattedValue = onlyNumeric(formattedValue.ToString.Replace(".", ","), ",")

            'If formattedValue = "" Then formattedValue = 0
            formattedValue /= 100
            formattedValue = Math.Round(formattedValue, 6)
            If formattedValue > 1 Then formattedValue = 1
            periodes.EndEdit()
            periodes.Rows(rowIndex).Cells(columnIndex).Value = formattedValue
        End If

        Return canceling
    End Function

    Private Sub periodes_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles periodes.CellValuePushed
        isChanging = True
    End Sub

    Private Sub periodes_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles periodes.DataError
        e.Cancel = False
    End Sub

    Private Sub periodes_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles periodes.EditingControlShowing
        AddHandler e.Control.VisibleChanged, AddressOf ec_Visible
        hasJustCanceled = True
    End Sub

    Private Sub ec_Visible(ByVal sender As Object, ByVal e As EventArgs)
        If periodes.EditingControl IsNot Nothing AndAlso periodes.EditingControl.Visible = False Then hasJustCanceled = False
    End Sub

    Private Sub periodes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles periodes.KeyDown
        If e.KeyCode = Keys.Enter Then
            periodes.BeginEdit(True)
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub periodes_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles periodes.RowsAdded
        If periodes.Rows.Count = 1 And e.RowCount = 1 Then Exit Sub 'Les périodes ne sont pas chargés

        Dim rowIndex As Integer = e.RowIndex
        If periodes.Rows(rowIndex).IsNewRow Then
            rowIndex -= 1
        End If
        rowAdded(rowIndex)
    End Sub

    Private Sub rowAdded(ByVal rowIndex As Integer)
        With periodes.Rows
            'If .Item(e.RowIndex).IsNewRow Then Exit Sub
            If .Item(rowIndex).Cells("IsEval").Value Is Nothing Or .Item(rowIndex).Cells("IsEval").Value Is DBNull.Value Then .Item(rowIndex).Cells("IsEval").Value = 0
            If .Item(rowIndex).Cells("NoPeriode").Value Is Nothing Or .Item(rowIndex).Cells("NoPeriode").Value Is DBNull.Value Then
                With CType(.Item(rowIndex).Cells("NoPeriode"), DataGridViewComboBoxCell)
                    .Value = CType(CType(periodes.Rows.Item(rowIndex).Cells("NoPeriode"), DataGridViewComboBoxCell).Items(0), DataRowView).Item("NoPeriode")
                    '.ValueMember = .ValueMember
                End With
            End If
            If .Item(rowIndex).Cells("IsDefault").Value Is Nothing Or .Item(rowIndex).Cells("IsDefault").Value Is DBNull.Value Then .Item(rowIndex).Cells("IsDefault").Value = 0
            If .Item(rowIndex).Cells("Montant").Value Is Nothing Or .Item(rowIndex).Cells("Montant").Value Is DBNull.Value Then .Item(rowIndex).Cells("Montant").Value = 0
            If .Item(rowIndex).Cells("PourcentAbsence").Value Is Nothing Or .Item(rowIndex).Cells("PourcentAbsence").Value Is DBNull.Value Then .Item(rowIndex).Cells("PourcentAbsence").Value = 0.5
            If .Item(rowIndex).Cells("PourcentClient").Value Is Nothing Or .Item(rowIndex).Cells("PourcentClient").Value Is DBNull.Value Then .Item(rowIndex).Cells("PourcentClient").Value = 1
            If .Item(rowIndex).Cells("NoKP").Value Is Nothing Or .Item(rowIndex).Cells("NoKP").Value Is DBNull.Value Then
                .Item(rowIndex).Cells("NoKP").Value = 0
                .Item(rowIndex).Cells("KPColumn").Value = "Aucun(e)"
            End If
        End With
        If CType(periodes.DataSource, DataTable).GetChanges IsNot Nothing Then formModified = True
    End Sub

    Private Sub codifications_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        formModified = False ' Is this really useful ? YES ! (Tested when no codes - cleaned database -)
    End Sub

    Private Sub periodes_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles periodes.RowStateChanged

    End Sub

    Private Sub periodes_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles periodes.RowValidating
        If periodes.Rows(e.RowIndex).IsNewRow Then rowAdded(e.RowIndex)

        Try
            For i As Byte = 2 To 4
                Dim canceling As Boolean = validateCells(e.RowIndex, i, periodes.Rows(e.RowIndex).Cells(i).FormattedValue)
                If canceling = True Then
                    e.Cancel = True
                    If periodes.Rows(e.RowIndex).IsNewRow = False Then periodes.currentCell = periodes.Rows(e.RowIndex).Cells(i + 1)
                    periodes.BeginEdit(True)
                    Exit For
                End If
            Next i
        Catch ex As Exception 'Catching problems when number of rows < 2
            addErrorLog(New Exception("Catching problems when number of rows < 2", ex))
        End Try
    End Sub

    Private Sub periodes_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles periodes.UserDeletingRow
        Dim hasOne As Boolean = False
        For i As Integer = 0 To periodes.Rows.Count - 1
            If periodes.Rows(i).IsNewRow = False AndAlso e.Row.Index <> i AndAlso e.Row.Cells(2).FormattedValue = periodes.Rows(i).Cells(2).FormattedValue Then hasOne = True : Exit For
        Next i

        If hasOne = False Then
            MessageBox.Show("Vous devez avoir au minimum un" & IIf(e.Row.Cells(2).FormattedValue = True, "e évaluation", " traitement"), "Période minimale")
            e.Cancel = True
        Else
            formModified = True
        End If
    End Sub

    Private Sub sélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SélectionnerLelaPersonneorganismeCléPayeureuseToolStripMenuItem.Click
        Dim myKeyPeople As New keypeopleSearch
        myKeyPeople.MdiParent = Nothing
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            periodes.Rows(lastKPSelectorButtonIndex).Cells("NoKP").Value = kpChosen.noKP
            periodes.Rows(lastKPSelectorButtonIndex).Cells("KPColumn").Value = kpChosen.kpFullName
        End If
    End Sub

    Private Sub aucunToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AucunToolStripMenuItem.Click
        periodes.Rows(lastKPSelectorButtonIndex).Cells("NoKP").Value = 0
        periodes.Rows(lastKPSelectorButtonIndex).Cells("KPColumn").Value = "Aucun(e)"
    End Sub

    Private Sub selectionner_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles selectCode.GotFocus
        If isSelecting Then Me.ActiveControl = Me.listcode
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(Of SingleWindowSaveEventArgs)(AddressOf codifications_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function.StartsWith("CodesDossiers") Then
            loadDates()
        End If
    End Sub

    Private Sub dates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dates.SelectedIndexChanged
        loadingData()

        If allowModification Then
            'Si rien n'est bloqué, alors Enabling/Desabling buttons
            Dim allDef As Boolean = True
            For Each curCode As FolderCode In myCodes
                If curCode.noUser <> 0 Then
                    allDef = False
                    Exit For
                End If
            Next curCode

            setDefaultAll.Enabled = Not allDef
            copyAll.Enabled = Not allDef AndAlso therapeute.Items.Count > 1
            If date1Infdate2(Date.Today, CType(dates.SelectedItem, DateInterval).to) Then add_Renamed.Enabled = True
        End If

        formModified = False
    End Sub

    Private Sub setDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles setDefault.Click
        Dim curCode As FolderCode = listcode.selectedItems(0).ValueA
        __setDefault(curCode.noUnique, "Attention ! Cette procédure efface toutes les versions futures de la codification sélectionnée et inscrit, si elle existe, la version d'aujourd'hui comme terminé." & vbCrLf & "Êtes-vous sûr de vouloir remettre par défaut la codification sélectionnée ?", "Codification dossier  : " & curTRP.toString & " - " & curCode.toString & " remis par défaut")
    End Sub

    Private Sub __setDefault(ByVal noUnique As Integer, ByVal confirmQuestion As String, ByVal status As String)
        If Not askWhenModified() Then Exit Sub

        If MessageBox.Show(confirmQuestion, "Codification client : Confirmation de remise à défaut", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then Exit Sub

        If FolderCodesManager.getInstance.setTherapistCodesToDefault(curTRP.noUser, noUnique) = False Then Exit Sub

        myMainWin.StatusText = status
        loadDates()
    End Sub


    Private Sub copy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles copy.Click
        If Not askWhenModified() Then Exit Sub

        Dim noTRP As Integer = selectCopyingTRP()
        If noTRP <> -1 Then
            Dim curCode As FolderCode = listcode.selectedItems(0).ValueA
            Try
                curCode.copyTo(noTRP)
            Catch ex As DBItemableUncopiable
                MessageBox.Show("Impossible de copier la codification " & curCode.name & " pour le thérapeute demandé, car il existe déjà une codification du même nom et de même date de début d'application", "Impossible de copier", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub selectFirstDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectFirstDate.Click
        Dim minDate As Date = Date.Today.AddDays(1)
        Dim maxDate As Date = LIMIT_DATE
        Dim selectedDate As Date = Date.Today.AddDays(1)
        If firstEffectiveTime.Text <> "" AndAlso firstEffectiveTime.Text <> "Aucune" Then selectedDate = Date.Parse(firstEffectiveTime.Text)
        If date1Infdate2(selectedDate, Date.Today, True) Then selectedDate = Date.Today.AddDays(1)

        If listcode.selectedItems.Count <> 0 Then
            Dim curCode As FolderCode = listcode.selectedItems(0).ValueA
            Dim prevCode As FolderCode = curCode.getPreviousUnique()
            If prevCode IsNot Nothing AndAlso date1Infdate2(minDate, prevCode.lastEffectiveTime) AndAlso date1Infdate2(Date.Today, prevCode.firstEffectiveTime) Then minDate = prevCode.lastEffectiveTime.AddDays(1)
            maxDate = curCode.lastEffectiveTime
        End If

        Dim myDateChoice As New DateChoice()
        Dim myDate As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year, Date.Today.Year + 1, , , , , , , minDate, , , , selectedDate, , , , , , maxDate)
        If myDate.Count <> 0 Then
            firstEffectiveTime.Text = DateFormat.getTextDate(myDate(0))
            formModified = True
        End If
    End Sub

    Private Sub folderTypes_CheckBoxChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles folderAlertTypes.checkBoxChanged, folderTextTypes.checkBoxChanged
        formModified = True
    End Sub

    Private Sub folderTypes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles folderAlertTypes.textChanged, folderTextTypes.textChanged
        With CType(sender, TreeViewComboPlus)
            If Not .droppedDown AndAlso .text <> .upText AndAlso .text.StartsWith("[") = False Then
                .text = "[" & .getSelected().Length & " sélectionné(s)] " & .text
            End If
        End With
    End Sub

    Private Sub dates_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dates.SelectionChangeCommitted
        selectedDate = CType(dates.SelectedItem, DateInterval).to
    End Sub

End Class
