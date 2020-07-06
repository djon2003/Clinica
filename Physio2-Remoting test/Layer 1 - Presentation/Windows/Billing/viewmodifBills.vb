Friend Class viewmodifBills
    Inherits SingleWindow

    Private curFactureBox As FacturationBox

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        Me.MdiParent = myMainWin

        curFactureBox = New FacturationBox(False, False, False, FacturationBox.DedicatedType.All)
        curFactureBox.Left = (Me.Width - curFactureBox.Width) / 2
        curFactureBox.Top = 251
        curFactureBox.Anchor = AnchorStyles.Bottom
        curFactureBox.locked = True
        Me.Controls.Add(curFactureBox)

        'Chargement des images
        With DrawingManager.getInstance
            Me.selectPrimeEntity.Image = .getImage("selection16.gif")
            Me.selectSecondEntity.Image = .getImage("selection16.gif")
            Me.selectClient.Image = .getImage("selection16.gif")
            Me.selectKP.Image = .getImage("selection16.gif")
            Me.selectUser.Image = .getImage("selection16.gif")
            Me.filter.Image = .getImage("selection16.gif")
            Me.btnModif.ImageList = New ImageList()
            Me.btnModif.ImageList.Images.Add(.getImage("modifier16.gif"))
            Me.btnModif.ImageList.Images.Add(.getImage("stopmodif16.gif"))
            Me.btnModif.ImageIndex = 0
            Me.btnUnify.Image = .getImage("UnifyBill.jpg")
            Me.btnDesunify.Image = .getImage("DesunifyBill.jpg")
            Me.btnRecu.Image = .getImage("createrecu.gif")
            Me.Icon = DrawingManager.imageToIcon(.getImage("viewBills16.jpg"))
        End With

        If currentUserName <> "Administrateur" Then AdminBox.Visible = False
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            'For i As Integer = FacturesPanel.Controls.Count - 1 To 0 Step -1
            '    If TypeOf (FacturesPanel.Controls(i)) Is CheckBox Then RemoveHandler CType(FacturesPanel.Controls(i), CheckBox).CheckedChanged, AddressOf FacturationBoxCheck_CheckedChanged
            '    If FacturesPanel.Controls(i).Name.StartsWith("PBButton") Then RemoveHandler CType(FacturesPanel.Controls(i), Button).Click, AddressOf FacturationBoxEnableStop_Click
            '    If FacturesPanel.Controls(i).Name.StartsWith("UnifyButton") Then RemoveHandler CType(FacturesPanel.Controls(i), Button).Click, AddressOf FacturationBoxUnify_Click
            '    If FacturesPanel.Controls(i).Name.StartsWith("DUButton") Then RemoveHandler CType(FacturesPanel.Controls(i), Button).Click, AddressOf FacturationBoxDesunify_Click
            '    If FacturesPanel.Controls(i).Name.StartsWith("CRButton") Then RemoveHandler CType(FacturesPanel.Controls(i), Button).Click, AddressOf FacturationBoxCreateRecu_Click
            '    FacturesPanel.Controls(i).Dispose()
            'Next i
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
    Friend WithEvents Entite As System.Windows.Forms.ComboBox
    Friend WithEvents Labels As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupPayeurs As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DateA As System.Windows.Forms.Label
    Friend WithEvents DateDe As System.Windows.Forms.Label
    Friend WithEvents ChoixA As System.Windows.Forms.Button
    Friend WithEvents DateAll As System.Windows.Forms.CheckBox
    Friend WithEvents ChoixDe As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents selectPrimeEntity As System.Windows.Forms.Button
    Public WithEvents selectSecondEntity As System.Windows.Forms.Button
    Public WithEvents selectClient As System.Windows.Forms.Button
    Public WithEvents selectKP As System.Windows.Forms.Button
    Public WithEvents selectUser As System.Windows.Forms.Button
    Friend WithEvents PayeurKP As System.Windows.Forms.TextBox
    Friend WithEvents PayeurUser As System.Windows.Forms.TextBox
    Friend WithEvents PayeurClient As System.Windows.Forms.TextBox
    Friend WithEvents NoFactureDe As ManagedText
    Friend WithEvents NoFactureA As ManagedText
    Friend WithEvents MontantDuA As ManagedText
    Friend WithEvents MontantDuDe As ManagedText
    Friend WithEvents TypeFacture As System.Windows.Forms.ComboBox
    Friend WithEvents Createur As System.Windows.Forms.ComboBox
    Public WithEvents filter As System.Windows.Forms.Button
    Friend WithEvents menuSecondEntity As System.Windows.Forms.ContextMenu
    Friend WithEvents menuPret As System.Windows.Forms.MenuItem
    Friend WithEvents menuVente As System.Windows.Forms.MenuItem
    Friend WithEvents menuVisite As System.Windows.Forms.MenuItem
    Friend WithEvents menuLine1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuSelect As System.Windows.Forms.MenuItem
    Friend WithEvents menuAucune2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuSelectEntity As System.Windows.Forms.ContextMenu
    Friend WithEvents menuAucune1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuLine2 As System.Windows.Forms.MenuItem
    Friend WithEvents EntitePrimaire As System.Windows.Forms.TextBox
    Friend WithEvents EntiteSecondaire As System.Windows.Forms.TextBox
    Friend WithEvents AdminBox As System.Windows.Forms.GroupBox
    Friend WithEvents AdminClose As System.Windows.Forms.Button
    Friend WithEvents FacturesView As DataGridPlus
    Public WithEvents btnModif As System.Windows.Forms.Button
    Public WithEvents btnUnify As System.Windows.Forms.Button
    Public WithEvents btnDesunify As System.Windows.Forms.Button
    Public WithEvents btnRecu As System.Windows.Forms.Button
    Friend WithEvents DateF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoFacture As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents btnToggleSouffrance As System.Windows.Forms.Button
    Friend WithEvents lblListStatus As System.Windows.Forms.Label
    Friend WithEvents Spacing As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Entite = New System.Windows.Forms.ComboBox
        Me.Labels = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.selectPrimeEntity = New System.Windows.Forms.Button
        Me.selectSecondEntity = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.selectClient = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.selectKP = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.selectUser = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupPayeurs = New System.Windows.Forms.GroupBox
        Me.PayeurKP = New System.Windows.Forms.TextBox
        Me.PayeurUser = New System.Windows.Forms.TextBox
        Me.PayeurClient = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TypeFacture = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Createur = New System.Windows.Forms.ComboBox
        Me.DateA = New System.Windows.Forms.Label
        Me.DateDe = New System.Windows.Forms.Label
        Me.ChoixA = New System.Windows.Forms.Button
        Me.DateAll = New System.Windows.Forms.CheckBox
        Me.ChoixDe = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.filter = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnToggleSouffrance = New System.Windows.Forms.Button
        Me.btnRecu = New System.Windows.Forms.Button
        Me.menuSecondEntity = New System.Windows.Forms.ContextMenu
        Me.menuPret = New System.Windows.Forms.MenuItem
        Me.menuVente = New System.Windows.Forms.MenuItem
        Me.menuVisite = New System.Windows.Forms.MenuItem
        Me.menuLine1 = New System.Windows.Forms.MenuItem
        Me.menuAucune2 = New System.Windows.Forms.MenuItem
        Me.menuSelectEntity = New System.Windows.Forms.ContextMenu
        Me.menuSelect = New System.Windows.Forms.MenuItem
        Me.menuLine2 = New System.Windows.Forms.MenuItem
        Me.menuAucune1 = New System.Windows.Forms.MenuItem
        Me.EntitePrimaire = New System.Windows.Forms.TextBox
        Me.EntiteSecondaire = New System.Windows.Forms.TextBox
        Me.AdminBox = New System.Windows.Forms.GroupBox
        Me.Spacing = New System.Windows.Forms.TextBox
        Me.AdminClose = New System.Windows.Forms.Button
        Me.FacturesView = New CI.Base.Windows.Forms.DataGridPlus
        Me.DateF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PPO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PU = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoFacture = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnModif = New System.Windows.Forms.Button
        Me.btnUnify = New System.Windows.Forms.Button
        Me.btnDesunify = New System.Windows.Forms.Button
        Me.NoFactureDe = New ManagedText
        Me.MontantDuA = New ManagedText
        Me.MontantDuDe = New ManagedText
        Me.NoFactureA = New ManagedText
        Me.lblListStatus = New System.Windows.Forms.Label
        Me.GroupPayeurs.SuspendLayout()
        Me.AdminBox.SuspendLayout()
        CType(Me.FacturesView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Entite
        '
        Me.Entite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Entite.Items.AddRange(New Object() {"  Toutes  ", "Clients", "Personnes / organismes clé", "Utilisateurs"})
        Me.Entite.Location = New System.Drawing.Point(88, 8)
        Me.Entite.Name = "Entite"
        Me.Entite.Size = New System.Drawing.Size(144, 21)
        Me.Entite.TabIndex = 1
        '
        'Labels
        '
        Me.Labels.AutoSize = True
        Me.Labels.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Labels.Location = New System.Drawing.Point(8, 8)
        Me.Labels.Name = "Labels"
        Me.Labels.Size = New System.Drawing.Size(72, 13)
        Me.Labels.TabIndex = 2
        Me.Labels.Text = "Entité liée :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(40, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Entité liée spécifique primaire :"
        '
        'selectPrimeEntity
        '
        Me.selectPrimeEntity.BackColor = System.Drawing.SystemColors.Control
        Me.selectPrimeEntity.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectPrimeEntity.Enabled = False
        Me.selectPrimeEntity.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectPrimeEntity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectPrimeEntity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectPrimeEntity.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectPrimeEntity.Location = New System.Drawing.Point(8, 32)
        Me.selectPrimeEntity.Name = "selectPrimeEntity"
        Me.selectPrimeEntity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectPrimeEntity.Size = New System.Drawing.Size(24, 24)
        Me.selectPrimeEntity.TabIndex = 215
        Me.selectPrimeEntity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.selectPrimeEntity.UseVisualStyleBackColor = False
        '
        'selectSecondEntity
        '
        Me.selectSecondEntity.BackColor = System.Drawing.SystemColors.Control
        Me.selectSecondEntity.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectSecondEntity.Enabled = False
        Me.selectSecondEntity.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSecondEntity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectSecondEntity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectSecondEntity.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectSecondEntity.Location = New System.Drawing.Point(344, 32)
        Me.selectSecondEntity.Name = "selectSecondEntity"
        Me.selectSecondEntity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectSecondEntity.Size = New System.Drawing.Size(24, 24)
        Me.selectSecondEntity.TabIndex = 218
        Me.selectSecondEntity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectSecondEntity, "Sélectionner une visite, un prêt ou une vente spécifique")
        Me.selectSecondEntity.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(376, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(200, 13)
        Me.Label4.TabIndex = 216
        Me.Label4.Text = "Entité liée spécifique secondaire :"
        '
        'selectClient
        '
        Me.selectClient.BackColor = System.Drawing.SystemColors.Control
        Me.selectClient.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectClient.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectClient.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectClient.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectClient.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectClient.Location = New System.Drawing.Point(6, 19)
        Me.selectClient.Name = "selectClient"
        Me.selectClient.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectClient.Size = New System.Drawing.Size(24, 24)
        Me.selectClient.TabIndex = 221
        Me.selectClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectClient, "Sélectionner un client qui paie")
        Me.selectClient.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(32, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 219
        Me.Label6.Text = "Client :"
        '
        'selectKP
        '
        Me.selectKP.BackColor = System.Drawing.SystemColors.Control
        Me.selectKP.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectKP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectKP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectKP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectKP.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectKP.Location = New System.Drawing.Point(192, 19)
        Me.selectKP.Name = "selectKP"
        Me.selectKP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectKP.Size = New System.Drawing.Size(24, 24)
        Me.selectKP.TabIndex = 224
        Me.selectKP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectKP, "Sélectionner une personne / organisme clé qui paie")
        Me.selectKP.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(216, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(160, 13)
        Me.Label8.TabIndex = 222
        Me.Label8.Text = "Personne / organisme clé :"
        '
        'selectUser
        '
        Me.selectUser.BackColor = System.Drawing.SystemColors.Control
        Me.selectUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectUser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectUser.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectUser.Location = New System.Drawing.Point(486, 19)
        Me.selectUser.Name = "selectUser"
        Me.selectUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectUser.Size = New System.Drawing.Size(24, 24)
        Me.selectUser.TabIndex = 227
        Me.selectUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectUser, "Sélectionner un utilisateur qui paie")
        Me.selectUser.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(512, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 225
        Me.Label10.Text = "Utilisateur :"
        '
        'GroupPayeurs
        '
        Me.GroupPayeurs.Controls.Add(Me.PayeurKP)
        Me.GroupPayeurs.Controls.Add(Me.PayeurUser)
        Me.GroupPayeurs.Controls.Add(Me.PayeurClient)
        Me.GroupPayeurs.Controls.Add(Me.selectClient)
        Me.GroupPayeurs.Controls.Add(Me.Label6)
        Me.GroupPayeurs.Controls.Add(Me.Label8)
        Me.GroupPayeurs.Controls.Add(Me.Label10)
        Me.GroupPayeurs.Controls.Add(Me.selectKP)
        Me.GroupPayeurs.Controls.Add(Me.selectUser)
        Me.GroupPayeurs.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPayeurs.Location = New System.Drawing.Point(8, 56)
        Me.GroupPayeurs.Name = "GroupPayeurs"
        Me.GroupPayeurs.Size = New System.Drawing.Size(688, 48)
        Me.GroupPayeurs.TabIndex = 228
        Me.GroupPayeurs.TabStop = False
        Me.GroupPayeurs.Text = "Entité(s) payeuse(s)"
        '
        'PayeurKP
        '
        Me.PayeurKP.BackColor = System.Drawing.SystemColors.Control
        Me.PayeurKP.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PayeurKP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PayeurKP.Location = New System.Drawing.Point(384, 24)
        Me.PayeurKP.Name = "PayeurKP"
        Me.PayeurKP.Size = New System.Drawing.Size(96, 13)
        Me.PayeurKP.TabIndex = 233
        Me.PayeurKP.Text = "Aucun(e)"
        '
        'PayeurUser
        '
        Me.PayeurUser.BackColor = System.Drawing.SystemColors.Control
        Me.PayeurUser.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PayeurUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PayeurUser.Location = New System.Drawing.Point(584, 24)
        Me.PayeurUser.Name = "PayeurUser"
        Me.PayeurUser.Size = New System.Drawing.Size(96, 13)
        Me.PayeurUser.TabIndex = 232
        Me.PayeurUser.Text = "Aucun"
        '
        'PayeurClient
        '
        Me.PayeurClient.BackColor = System.Drawing.SystemColors.Control
        Me.PayeurClient.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PayeurClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PayeurClient.Location = New System.Drawing.Point(80, 24)
        Me.PayeurClient.Name = "PayeurClient"
        Me.PayeurClient.Size = New System.Drawing.Size(96, 13)
        Me.PayeurClient.TabIndex = 231
        Me.PayeurClient.Text = "Aucun"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(328, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 13)
        Me.Label5.TabIndex = 231
        Me.Label5.Text = "Numéro de la facture :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(385, 131)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 13)
        Me.Label7.TabIndex = 232
        Me.Label7.Text = "à"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(496, 128)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 236
        Me.Label9.Text = "jour(s) à"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(464, 112)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 13)
        Me.Label11.TabIndex = 235
        Me.Label11.Text = "Montant dû depuis :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(584, 128)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 13)
        Me.Label12.TabIndex = 237
        Me.Label12.Text = "jour(s)"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(456, 8)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(105, 13)
        Me.Label13.TabIndex = 239
        Me.Label13.Text = "Type de facture :"
        '
        'TypeFacture
        '
        Me.TypeFacture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeFacture.Location = New System.Drawing.Point(552, 8)
        Me.TypeFacture.Name = "TypeFacture"
        Me.TypeFacture.Size = New System.Drawing.Size(144, 21)
        Me.TypeFacture.Sorted = True
        Me.TypeFacture.TabIndex = 238
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(240, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(63, 13)
        Me.Label14.TabIndex = 241
        Me.Label14.Text = "Créateur :"
        '
        'Createur
        '
        Me.Createur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Createur.Location = New System.Drawing.Point(304, 8)
        Me.Createur.Name = "Createur"
        Me.Createur.Size = New System.Drawing.Size(144, 21)
        Me.Createur.Sorted = True
        Me.Createur.TabIndex = 240
        '
        'DateA
        '
        Me.DateA.AutoSize = True
        Me.DateA.BackColor = System.Drawing.Color.Transparent
        Me.DateA.Location = New System.Drawing.Point(264, 128)
        Me.DateA.Name = "DateA"
        Me.DateA.Size = New System.Drawing.Size(0, 13)
        Me.DateA.TabIndex = 247
        '
        'DateDe
        '
        Me.DateDe.AutoSize = True
        Me.DateDe.BackColor = System.Drawing.Color.Transparent
        Me.DateDe.Location = New System.Drawing.Point(152, 128)
        Me.DateDe.Name = "DateDe"
        Me.DateDe.Size = New System.Drawing.Size(0, 13)
        Me.DateDe.TabIndex = 246
        '
        'ChoixA
        '
        Me.ChoixA.Location = New System.Drawing.Point(224, 128)
        Me.ChoixA.Name = "ChoixA"
        Me.ChoixA.Size = New System.Drawing.Size(32, 18)
        Me.ChoixA.TabIndex = 245
        Me.ChoixA.Text = "A"
        '
        'DateAll
        '
        Me.DateAll.BackColor = System.Drawing.Color.Transparent
        Me.DateAll.Location = New System.Drawing.Point(8, 128)
        Me.DateAll.Name = "DateAll"
        Me.DateAll.Size = New System.Drawing.Size(112, 16)
        Me.DateAll.TabIndex = 244
        Me.DateAll.Text = "Toutes les dates"
        Me.DateAll.UseVisualStyleBackColor = False
        '
        'ChoixDe
        '
        Me.ChoixDe.Location = New System.Drawing.Point(112, 128)
        Me.ChoixDe.Name = "ChoixDe"
        Me.ChoixDe.Size = New System.Drawing.Size(32, 18)
        Me.ChoixDe.TabIndex = 243
        Me.ChoixDe.Text = "De"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 112)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(125, 13)
        Me.Label15.TabIndex = 248
        Me.Label15.Text = "Date de facturation :"
        '
        'filter
        '
        Me.filter.BackColor = System.Drawing.SystemColors.Control
        Me.filter.Cursor = System.Windows.Forms.Cursors.Default
        Me.filter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.filter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.filter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.filter.Location = New System.Drawing.Point(632, 112)
        Me.filter.Name = "filter"
        Me.filter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.filter.Size = New System.Drawing.Size(56, 32)
        Me.filter.TabIndex = 249
        Me.filter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.filter, "Filtrer les factures avec les paramètres ci-contre")
        Me.filter.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'btnToggleSouffrance
        '
        Me.btnToggleSouffrance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnToggleSouffrance.BackColor = System.Drawing.SystemColors.Control
        Me.btnToggleSouffrance.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnToggleSouffrance.Enabled = False
        Me.btnToggleSouffrance.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggleSouffrance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnToggleSouffrance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnToggleSouffrance.Location = New System.Drawing.Point(664, 358)
        Me.btnToggleSouffrance.Name = "btnToggleSouffrance"
        Me.btnToggleSouffrance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnToggleSouffrance.Size = New System.Drawing.Size(24, 24)
        Me.btnToggleSouffrance.TabIndex = 227
        Me.btnToggleSouffrance.Text = "S"
        Me.ToolTip1.SetToolTip(Me.btnToggleSouffrance, "Signaler que la facture est en souffrance")
        Me.btnToggleSouffrance.UseVisualStyleBackColor = False
        '
        'btnRecu
        '
        Me.btnRecu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRecu.BackColor = System.Drawing.SystemColors.Control
        Me.btnRecu.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnRecu.Enabled = False
        Me.btnRecu.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRecu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnRecu.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnRecu.Location = New System.Drawing.Point(664, 332)
        Me.btnRecu.Name = "btnRecu"
        Me.btnRecu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnRecu.Size = New System.Drawing.Size(24, 24)
        Me.btnRecu.TabIndex = 227
        Me.btnRecu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnRecu, "Émettre un reçu pour cette facture")
        Me.btnRecu.UseVisualStyleBackColor = False
        '
        'menuSecondEntity
        '
        Me.menuSecondEntity.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuPret, Me.menuVente, Me.menuVisite, Me.menuLine1, Me.menuAucune2})
        Me.menuSecondEntity.Tag = 2
        '
        'menuPret
        '
        Me.menuPret.Index = 0
        Me.menuPret.Text = "Prêt"
        '
        'menuVente
        '
        Me.menuVente.Index = 1
        Me.menuVente.Text = "Vente"
        '
        'menuVisite
        '
        Me.menuVisite.Index = 2
        Me.menuVisite.Text = "Visite"
        '
        'menuLine1
        '
        Me.menuLine1.Index = 3
        Me.menuLine1.Text = "-"
        '
        'menuAucune2
        '
        Me.menuAucune2.Index = 4
        Me.menuAucune2.Text = "Aucune"
        '
        'menuSelectEntity
        '
        Me.menuSelectEntity.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuSelect, Me.menuLine2, Me.menuAucune1})
        Me.menuSelectEntity.Tag = 1
        '
        'menuSelect
        '
        Me.menuSelect.Index = 0
        Me.menuSelect.Text = "Sélectionner"
        '
        'menuLine2
        '
        Me.menuLine2.Index = 1
        Me.menuLine2.Text = "-"
        '
        'menuAucune1
        '
        Me.menuAucune1.Index = 2
        Me.menuAucune1.Text = "Aucun(e)"
        '
        'EntitePrimaire
        '
        Me.EntitePrimaire.BackColor = System.Drawing.SystemColors.Control
        Me.EntitePrimaire.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EntitePrimaire.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EntitePrimaire.Location = New System.Drawing.Point(216, 40)
        Me.EntitePrimaire.Name = "EntitePrimaire"
        Me.EntitePrimaire.Size = New System.Drawing.Size(120, 13)
        Me.EntitePrimaire.TabIndex = 231
        Me.EntitePrimaire.Text = "Aucune"
        '
        'EntiteSecondaire
        '
        Me.EntiteSecondaire.BackColor = System.Drawing.SystemColors.Control
        Me.EntiteSecondaire.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EntiteSecondaire.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EntiteSecondaire.Location = New System.Drawing.Point(560, 40)
        Me.EntiteSecondaire.Name = "EntiteSecondaire"
        Me.EntiteSecondaire.Size = New System.Drawing.Size(136, 13)
        Me.EntiteSecondaire.TabIndex = 232
        Me.EntiteSecondaire.Text = "Aucune"
        '
        'AdminBox
        '
        Me.AdminBox.Controls.Add(Me.Spacing)
        Me.AdminBox.Controls.Add(Me.AdminClose)
        Me.AdminBox.Location = New System.Drawing.Point(176, 120)
        Me.AdminBox.Name = "AdminBox"
        Me.AdminBox.Size = New System.Drawing.Size(120, 40)
        Me.AdminBox.TabIndex = 250
        Me.AdminBox.TabStop = False
        Me.AdminBox.Text = "Admin"
        '
        'Spacing
        '
        Me.Spacing.Location = New System.Drawing.Point(8, 16)
        Me.Spacing.Name = "Spacing"
        Me.Spacing.Size = New System.Drawing.Size(40, 20)
        Me.Spacing.TabIndex = 1
        Me.Spacing.Text = "45"
        '
        'AdminClose
        '
        Me.AdminClose.Location = New System.Drawing.Point(104, 0)
        Me.AdminClose.Name = "AdminClose"
        Me.AdminClose.Size = New System.Drawing.Size(16, 16)
        Me.AdminClose.TabIndex = 0
        Me.AdminClose.Text = "X"
        '
        'FacturesView
        '
        Me.FacturesView.AllowUserToAddRows = False
        Me.FacturesView.AllowUserToDeleteRows = False
        Me.FacturesView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FacturesView.autoSelectOnDataSourceChanged = True
        Me.FacturesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.FacturesView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.FacturesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FacturesView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DateF, Me.TF, Me.MF, Me.MP, Me.PC, Me.PPO, Me.PU, Me.EL, Me.NoFacture})
        Me.FacturesView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.FacturesView.Location = New System.Drawing.Point(8, 154)
        Me.FacturesView.Name = "FacturesView"
        Me.FacturesView.ReadOnly = True
        Me.FacturesView.RowHeadersVisible = False
        Me.FacturesView.RowHeadersWidth = 20
        Me.FacturesView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.FacturesView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.FacturesView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FacturesView.Size = New System.Drawing.Size(688, 94)
        Me.FacturesView.TabIndex = 251
        '
        'DateF
        '
        Me.DateF.DataPropertyName = "DF"
        DataGridViewCellStyle10.Format = "d"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.DateF.DefaultCellStyle = DataGridViewCellStyle10
        Me.DateF.HeaderText = "Date facture"
        Me.DateF.Name = "DateF"
        Me.DateF.ReadOnly = True
        '
        'TF
        '
        Me.TF.DataPropertyName = "TF"
        Me.TF.HeaderText = "Type facture"
        Me.TF.Name = "TF"
        Me.TF.ReadOnly = True
        '
        'MF
        '
        Me.MF.DataPropertyName = "MF"
        DataGridViewCellStyle11.Format = "C2"
        DataGridViewCellStyle11.NullValue = "0"
        Me.MF.DefaultCellStyle = DataGridViewCellStyle11
        Me.MF.HeaderText = "Montant facturé"
        Me.MF.Name = "MF"
        Me.MF.ReadOnly = True
        '
        'MP
        '
        Me.MP.DataPropertyName = "MP"
        DataGridViewCellStyle12.Format = "C2"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.MP.DefaultCellStyle = DataGridViewCellStyle12
        Me.MP.HeaderText = "Montant payé"
        Me.MP.Name = "MP"
        Me.MP.ReadOnly = True
        '
        'PC
        '
        Me.PC.DataPropertyName = "PC"
        Me.PC.HeaderText = "Payeur client"
        Me.PC.Name = "PC"
        Me.PC.ReadOnly = True
        '
        'PPO
        '
        Me.PPO.DataPropertyName = "PPO"
        Me.PPO.HeaderText = "Payeur P/O"
        Me.PPO.Name = "PPO"
        Me.PPO.ReadOnly = True
        '
        'PU
        '
        Me.PU.DataPropertyName = "PU"
        Me.PU.HeaderText = "Payeur utilisateur"
        Me.PU.Name = "PU"
        Me.PU.ReadOnly = True
        '
        'EL
        '
        Me.EL.DataPropertyName = "EL"
        Me.EL.HeaderText = "Entité liée"
        Me.EL.Name = "EL"
        Me.EL.ReadOnly = True
        '
        'NoFacture
        '
        Me.NoFacture.DataPropertyName = "NoFacture"
        Me.NoFacture.HeaderText = "# Facture"
        Me.NoFacture.Name = "NoFacture"
        Me.NoFacture.ReadOnly = True
        Me.NoFacture.Visible = False
        '
        'btnModif
        '
        Me.btnModif.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModif.BackColor = System.Drawing.SystemColors.Control
        Me.btnModif.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnModif.Enabled = False
        Me.btnModif.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnModif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnModif.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnModif.Location = New System.Drawing.Point(664, 254)
        Me.btnModif.Name = "btnModif"
        Me.btnModif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnModif.Size = New System.Drawing.Size(24, 24)
        Me.btnModif.TabIndex = 227
        Me.btnModif.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModif.UseVisualStyleBackColor = False
        '
        'btnUnify
        '
        Me.btnUnify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUnify.BackColor = System.Drawing.SystemColors.Control
        Me.btnUnify.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnUnify.Enabled = False
        Me.btnUnify.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnUnify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnUnify.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnUnify.Location = New System.Drawing.Point(664, 280)
        Me.btnUnify.Name = "btnUnify"
        Me.btnUnify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnUnify.Size = New System.Drawing.Size(24, 24)
        Me.btnUnify.TabIndex = 227
        Me.btnUnify.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUnify.UseVisualStyleBackColor = False
        '
        'btnDesunify
        '
        Me.btnDesunify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDesunify.BackColor = System.Drawing.SystemColors.Control
        Me.btnDesunify.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnDesunify.Enabled = False
        Me.btnDesunify.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDesunify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDesunify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDesunify.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnDesunify.Location = New System.Drawing.Point(664, 306)
        Me.btnDesunify.Name = "btnDesunify"
        Me.btnDesunify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnDesunify.Size = New System.Drawing.Size(24, 24)
        Me.btnDesunify.TabIndex = 227
        Me.btnDesunify.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDesunify.UseVisualStyleBackColor = False
        '
        'NoFactureDe
        '
        Me.NoFactureDe.acceptAlpha = False
        Me.NoFactureDe.acceptedChars = ",§."
        Me.NoFactureDe.acceptNumeric = True
        Me.NoFactureDe.allCapital = False
        Me.NoFactureDe.allLower = False
        Me.NoFactureDe.blockOnMaximum = False
        Me.NoFactureDe.blockOnMinimum = False
        Me.NoFactureDe.cb_AcceptLeftZeros = False
        Me.NoFactureDe.cb_AcceptNegative = False
        Me.NoFactureDe.currencyBox = True
        Me.NoFactureDe.firstLetterCapital = False
        Me.NoFactureDe.firstLettersCapital = False
        Me.NoFactureDe.Location = New System.Drawing.Point(328, 128)
        Me.NoFactureDe.manageText = True
        Me.NoFactureDe.matchExp = ""
        Me.NoFactureDe.maximum = 0
        Me.NoFactureDe.minimum = 0
        Me.NoFactureDe.Name = "NoFactureDe"
        Me.NoFactureDe.nbDecimals = CType(0, Short)
        Me.NoFactureDe.onlyAlphabet = False
        Me.NoFactureDe.refuseAccents = False
        Me.NoFactureDe.refusedChars = ""
        Me.NoFactureDe.showInternalContextMenu = True
        Me.NoFactureDe.Size = New System.Drawing.Size(56, 20)
        Me.NoFactureDe.TabIndex = 229
        Me.NoFactureDe.Text = "0"
        Me.NoFactureDe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NoFactureDe.trimText = False
        '
        'MontantDuA
        '
        Me.MontantDuA.acceptAlpha = False
        Me.MontantDuA.acceptedChars = ",§."
        Me.MontantDuA.acceptNumeric = True
        Me.MontantDuA.allCapital = False
        Me.MontantDuA.allLower = False
        Me.MontantDuA.blockOnMaximum = False
        Me.MontantDuA.blockOnMinimum = False
        Me.MontantDuA.cb_AcceptLeftZeros = False
        Me.MontantDuA.cb_AcceptNegative = False
        Me.MontantDuA.currencyBox = True
        Me.MontantDuA.firstLetterCapital = False
        Me.MontantDuA.firstLettersCapital = False
        Me.MontantDuA.Location = New System.Drawing.Point(552, 128)
        Me.MontantDuA.manageText = True
        Me.MontantDuA.matchExp = ""
        Me.MontantDuA.maximum = 0
        Me.MontantDuA.minimum = 0
        Me.MontantDuA.Name = "MontantDuA"
        Me.MontantDuA.nbDecimals = CType(0, Short)
        Me.MontantDuA.onlyAlphabet = False
        Me.MontantDuA.refuseAccents = False
        Me.MontantDuA.refusedChars = ""
        Me.MontantDuA.showInternalContextMenu = True
        Me.MontantDuA.Size = New System.Drawing.Size(32, 20)
        Me.MontantDuA.TabIndex = 234
        Me.MontantDuA.Text = "0"
        Me.MontantDuA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MontantDuA.trimText = False
        '
        'MontantDuDe
        '
        Me.MontantDuDe.acceptAlpha = False
        Me.MontantDuDe.acceptedChars = ",§."
        Me.MontantDuDe.acceptNumeric = True
        Me.MontantDuDe.allCapital = False
        Me.MontantDuDe.allLower = False
        Me.MontantDuDe.blockOnMaximum = False
        Me.MontantDuDe.blockOnMinimum = False
        Me.MontantDuDe.cb_AcceptLeftZeros = False
        Me.MontantDuDe.cb_AcceptNegative = False
        Me.MontantDuDe.currencyBox = True
        Me.MontantDuDe.firstLetterCapital = False
        Me.MontantDuDe.firstLettersCapital = False
        Me.MontantDuDe.Location = New System.Drawing.Point(464, 128)
        Me.MontantDuDe.manageText = True
        Me.MontantDuDe.matchExp = ""
        Me.MontantDuDe.maximum = 0
        Me.MontantDuDe.minimum = 0
        Me.MontantDuDe.Name = "MontantDuDe"
        Me.MontantDuDe.nbDecimals = CType(0, Short)
        Me.MontantDuDe.onlyAlphabet = False
        Me.MontantDuDe.refuseAccents = False
        Me.MontantDuDe.refusedChars = ""
        Me.MontantDuDe.showInternalContextMenu = True
        Me.MontantDuDe.Size = New System.Drawing.Size(32, 20)
        Me.MontantDuDe.TabIndex = 233
        Me.MontantDuDe.Text = "0"
        Me.MontantDuDe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MontantDuDe.trimText = False
        '
        'NoFactureA
        '
        Me.NoFactureA.acceptAlpha = False
        Me.NoFactureA.acceptedChars = ",§."
        Me.NoFactureA.acceptNumeric = True
        Me.NoFactureA.allCapital = False
        Me.NoFactureA.allLower = False
        Me.NoFactureA.blockOnMaximum = False
        Me.NoFactureA.blockOnMinimum = False
        Me.NoFactureA.cb_AcceptLeftZeros = False
        Me.NoFactureA.cb_AcceptNegative = False
        Me.NoFactureA.currencyBox = True
        Me.NoFactureA.firstLetterCapital = False
        Me.NoFactureA.firstLettersCapital = False
        Me.NoFactureA.Location = New System.Drawing.Point(399, 128)
        Me.NoFactureA.manageText = True
        Me.NoFactureA.matchExp = ""
        Me.NoFactureA.maximum = 0
        Me.NoFactureA.minimum = 0
        Me.NoFactureA.Name = "NoFactureA"
        Me.NoFactureA.nbDecimals = CType(0, Short)
        Me.NoFactureA.onlyAlphabet = False
        Me.NoFactureA.refuseAccents = False
        Me.NoFactureA.refusedChars = ""
        Me.NoFactureA.showInternalContextMenu = True
        Me.NoFactureA.Size = New System.Drawing.Size(56, 20)
        Me.NoFactureA.TabIndex = 230
        Me.NoFactureA.Text = "0"
        Me.NoFactureA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NoFactureA.trimText = False
        '
        'lblListStatus
        '
        Me.lblListStatus.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblListStatus.AutoSize = True
        Me.lblListStatus.BackColor = Color.Transparent
        Me.lblListStatus.BorderStyle = BorderStyle.FixedSingle
        Me.lblListStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListStatus.Location = New System.Drawing.Point(288, 208)
        Me.lblListStatus.Name = "lblListStatus"
        Me.lblListStatus.Size = New System.Drawing.Size(129, 24)
        Me.lblListStatus.TabIndex = 252
        Me.lblListStatus.Text = "Chargement..."
        Me.lblListStatus.Visible = False
        '
        'viewmodifBills
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(704, 390)
        Me.Controls.Add(Me.lblListStatus)
        Me.Controls.Add(Me.NoFactureDe)
        Me.Controls.Add(Me.MontantDuA)
        Me.Controls.Add(Me.MontantDuDe)
        Me.Controls.Add(Me.NoFactureA)
        Me.Controls.Add(Me.FacturesView)
        Me.Controls.Add(Me.AdminBox)
        Me.Controls.Add(Me.filter)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.ChoixDe)
        Me.Controls.Add(Me.DateAll)
        Me.Controls.Add(Me.DateA)
        Me.Controls.Add(Me.DateDe)
        Me.Controls.Add(Me.btnRecu)
        Me.Controls.Add(Me.btnDesunify)
        Me.Controls.Add(Me.btnUnify)
        Me.Controls.Add(Me.btnToggleSouffrance)
        Me.Controls.Add(Me.btnModif)
        Me.Controls.Add(Me.ChoixA)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Createur)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TypeFacture)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupPayeurs)
        Me.Controls.Add(Me.selectSecondEntity)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.selectPrimeEntity)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Labels)
        Me.Controls.Add(Me.Entite)
        Me.Controls.Add(Me.EntitePrimaire)
        Me.Controls.Add(Me.EntiteSecondaire)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(712, 424)
        Me.Name = "viewmodifBills"
        Me.ShowInTaskbar = False
        Me.Text = "Gestion des factures"
        Me.GroupPayeurs.ResumeLayout(False)
        Me.GroupPayeurs.PerformLayout()
        Me.AdminBox.ResumeLayout(False)
        Me.AdminBox.PerformLayout()
        CType(Me.FacturesView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private lastLoadedBill As Integer = 0
    Private _Count As Integer = 0
    Private currentLocks As New ArrayList
    Private currentLocksCounting As New Hashtable

#Region "viewmodifBills Events"
    Private Sub viewmodifBills_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'Enregistrement de l'affichage personnalisée de cette fenêtre
        Dim curUser As User = UsersManager.currentUser
        curUser.settings.billsManaging = Me.Height
        curUser.settings.saveData()
    End Sub

    Private Sub viewmodifBills_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        cleanCurrentLocks()
    End Sub

    Private Sub viewmodifBills_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Me.Height = mymainwin.MdiChildRectangle.ri

        Entite.SelectedIndex = 0

        'Loading Users
        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(False)
        Createur.Items.AddRange(users.ToArray)
        Createur.Items.Add("  Tous  ")
        Createur.SelectedIndex = 0

        'Loading type facturation
        TypeFacture.Items.Add("  Tous  ")
        TypeFacture.Items.Add("* Prêts *")
        TypeFacture.Items.Add("* Travailleurs autonomes *")
        TypeFacture.Items.Add("* Ventes *")
        TypeFacture.Items.Add("* Services *")
        TypeFacture.Items.Add("* Autres *")
        TypeFacture.Items.Add("* Factures unifiées *")
        If Not PreferencesManager.getGeneralPreferences()("Services") Is Nothing Then If PreferencesManager.getGeneralPreferences()("Services") <> "" Then TypeFacture.Items.AddRange(System.Text.RegularExpressions.Regex.Split(PreferencesManager.getGeneralPreferences()("Services"), vbTab))
        If Not PreferencesManager.getGeneralPreferences()("AutresTypesBills") Is Nothing Then If PreferencesManager.getGeneralPreferences()("AutresTypesBills") <> "" Then TypeFacture.Items.AddRange(System.Text.RegularExpressions.Regex.Split(PreferencesManager.getGeneralPreferences()("AutresTypesBills"), vbTab))
        TypeFacture.SelectedIndex = 0

        Dim setting As String = UsersManager.currentUser.settings.billsManaging
        If setting <> "" Then
            Dim thisSettings() As String = setting.Split(New Char() {"§"})
            Me.Height = thisSettings(0)
        End If
    End Sub

    Private Sub viewmodifBills_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.Width <> 712 Then Me.Width = 712
        If Me.Height < 424 Then Me.Height = 424
    End Sub
#End Region

    Private Property count() As Integer
        Get
            Return _Count
        End Get
        Set(ByVal value As Integer)
            _Count = value
        End Set
    End Property

    Private Sub entite_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Entite.SelectedIndexChanged
        EntitePrimaire.Text = "Aucune"
        EntiteSecondaire.Text = "Aucune"

        Select Case Entite.SelectedIndex
            Case 0 'Tous
                selectPrimeEntity.Enabled = False
                selectSecondEntity.Enabled = False

            Case 1 'Client
                selectPrimeEntity.Enabled = True
                selectSecondEntity.Enabled = False
                ToolTip1.SetToolTip(selectPrimeEntity, "Sélectionner un client et un dossier spécifiques")

            Case 2 'KP
                selectPrimeEntity.Enabled = True
                selectSecondEntity.Enabled = False
                ToolTip1.SetToolTip(selectPrimeEntity, "Sélectionner une personne / organisme clé spécifique")

            Case 3 'User
                selectPrimeEntity.Enabled = True
                selectSecondEntity.Enabled = False
                ToolTip1.SetToolTip(selectPrimeEntity, "Sélectionner un utilisateur spécifique")

        End Select
    End Sub

    Private Sub selectSecondEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectSecondEntity.Click
        menuSecondEntity.Show(selectSecondEntity, New Point(selectSecondEntity.Width, 0))
    End Sub

    Private Sub selectPrimeEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectPrimeEntity.Click
        menuSelectEntity.Show(selectPrimeEntity, New Point(selectPrimeEntity.Width, 0))
    End Sub

    Private Sub selectClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectClient.Click
        menuSelectEntity.Show(selectClient, New Point(selectClient.Width, 0))
    End Sub

    Private Sub selectKP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectKP.Click
        menuSelectEntity.Show(selectKP, New Point(selectKP.Width, 0))
    End Sub

    Private Sub selectUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectUser.Click
        menuSelectEntity.Show(selectUser, New Point(selectUser.Width, 0))
    End Sub

    Private Sub menuSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSelect.Click
        Select Case menuSelectEntity.SourceControl.Name
            Case "selectUser"
                selectAUser(PayeurUser)

            Case "selectKP"
                Dim myKeyPeople As New keypeopleSearch()
                myKeyPeople.selected = False
                myKeyPeople.MdiParent = Nothing
                myKeyPeople.Visible = False
                Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
                If kpChosen.noKP <> 0 Then
                    PayeurKP.Text = kpChosen.kpFullName & " (" & kpChosen.noKP & ")"
                End If

            Case "selectClient"
                Dim myRecherche As New clientSearch()
                myRecherche.from = PayeurClient
                myRecherche.MdiParent = Nothing
                myRecherche.Visible = False
                myRecherche.ShowDialog()

            Case "selectPrimeEntity"
                Select Case Entite.SelectedIndex
                    Case 1 'Client
                        Dim myRecherche As New clientSearch()
                        myRecherche.from = EntitePrimaire
                        myRecherche.MdiParent = Nothing
                        myRecherche.Visible = False
                        myRecherche.ShowDialog()
                        If Me.EntitePrimaire.Text.IndexOf(",") > 0 Then
                            Me.selectSecondEntity.Enabled = True
                            Me.EntiteSecondaire.Text = "Aucune"
                            Me.menuPret.Checked = False
                            Me.menuVisite.Checked = False
                            Me.menuVente.Checked = False
                        End If

                    Case 2 'KP
                        Dim myKeyPeople As New keypeopleSearch()
                        myKeyPeople.selected = False
                        myKeyPeople.Visible = False
                        myKeyPeople.MdiParent = Nothing
                        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
                        If kpChosen.noKP <> 0 Then
                            EntitePrimaire.Text = kpChosen.kpFullName & " (" & kpChosen.noKP & ")"
                        End If

                    Case 3 'User
                        selectAUser(EntitePrimaire)
                End Select
        End Select
    End Sub

    Private Sub selectAUser(ByVal selectingControl As Control)
        Dim myUser As User = UsersManager.getInstance.chooseUser(False, False)

        If myUser Is Nothing Then
            Select Case menuSelectEntity.SourceControl.Name
                Case "selectUser"
                    PayeurUser.Text = "Aucun"

                Case "selectKP"
                    PayeurKP.Text = "Aucun(e)"

                Case "selectClient"
                    PayeurClient.Text = "Aucun"
            End Select
        Else
            selectingControl.Text = myUser.toString()
        End If
    End Sub

    Private Sub menuAucune_s_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAucune1.Click, menuAucune2.Click
        Select Case menuSelectEntity.SourceControl.Name
            Case "selectUser"
                PayeurUser.Text = "Aucun"

            Case "selectKP"
                PayeurKP.Text = "Aucun(e)"

            Case "selectClient"
                PayeurClient.Text = "Aucun"

            Case "selectPrimeEntity"
                If CType(sender, MenuItem).GetContextMenu().Tag = 1 Then
                    EntitePrimaire.Text = "Aucune"
                    selectSecondEntity.Enabled = False
                End If

                EntiteSecondaire.Text = "Aucune"
                menuPret.Checked = False
                menuVisite.Checked = False
                menuVente.Checked = False
        End Select
    End Sub

    Private Sub choixDeA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChoixDe.Click, ChoixA.Click
        Dim CurDate, scd() As String
        Dim myDate As Date = Nothing
        If sender.name.tolower = "choixde" Then
            CurDate = DateDe.Text
        Else
            CurDate = DateA.Text
        End If
        If CurDate <> "" Then
            scd = CurDate.Split(New Char() {"/"})
            myDate = New Date(scd(0), scd(1), scd(2))
        Else
            myDate = Date.Today
        End If
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year - 10, Date.Today.Year + 1, , , , , , True, , , , , myDate)
        If dateReturn.Count = 0 Then Exit Sub

        Dim dateReturnString As String = DateFormat.getTextDate(dateReturn(0))
        If sender.name.tolower = "choixde" Then
            DateDe.Text = dateReturnString
            If DateA.Text = "" Then DateA.Text = dateReturnString
        Else
            DateA.Text = dateReturnString
        End If
    End Sub

    Private Sub cleanCurrentLocks()
        Dim i As Integer
        For i = 0 To currentLocks.Count - 1
            lockSecteur(currentLocks(i), False, "Facturation")
        Next i

        currentLocks.Clear()
        currentLocksCounting.Clear()
    End Sub

    Public Sub loadFactures()
        cleanCurrentLocks()
        resetFacture()

        Dim whereStr As String = ""
        'Entité primaire
        Select Case Entite.SelectedIndex
            Case 1 'Client
                If EntitePrimaire.Text <> "Aucune" Then
                    Dim sEntite() As String = EntitePrimaire.Text.Split(New String() {" "}, System.StringSplitOptions.None)
                    whereStr = " AND StatFactures.NoFolder=" & sEntite(2).Substring(1)
                Else
                    whereStr = " AND StatFactures.NoClient > 0"
                End If
            Case 2 'KP
                If EntitePrimaire.Text <> "Aucune" Then
                    Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntitePrimaire.Text, " \(")
                    whereStr = " AND StatFactures.NoKP=" & sEntite(1).Substring(0, sEntite(1).Length - 1)
                Else
                    whereStr = " AND StatFactures.NoKP > 0"
                End If
            Case 3 'User
                If EntitePrimaire.Text <> "Aucune" Then
                    Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntitePrimaire.Text, " \(")
                    whereStr = " AND StatFactures.NoUserFacture=" & sEntite(1).Substring(0, sEntite(1).Length - 1)
                Else
                    whereStr = " AND StatFactures.NoUserFacture > 0"
                End If
        End Select

        'Entité secondaire s'il s'agit d'un client
        If Entite.SelectedIndex = 1 Then
            If menuVente.Checked = True Then
                Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntiteSecondaire.Text, "-")
                whereStr = " AND StatFactures.NoVente=" & sEntite(0)
            ElseIf menuPret.Checked = True Then
                Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntiteSecondaire.Text, "-")
                whereStr = " AND StatFactures.NoPret=" & sEntite(0)
            ElseIf menuVisite.Checked = True Then
                Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntiteSecondaire.Text, "-")
                whereStr = " AND StatFactures.NoVisite=" & sEntite(0)
            End If
        End If

        'Créateur de la facture
        If Createur.Text <> "  Tous  " Then
            Dim sCreateur() As String = System.Text.RegularExpressions.Regex.Split(Createur.Text, " \(")
            whereStr &= " AND StatFactures.NoUser=" & sCreateur(1).Substring(0, sCreateur(1).Length - 1)
        End If

        'Type de facture
        If TypeFacture.Text <> "  Tous  " Then
            Select Case TypeFacture.Text
                Case "* Travailleurs autonomes *"
                    whereStr &= " AND StatFactures.TypeFacture LIKE 'Travailleur autonome%'"
                Case "* Factures unifiées *"
                    whereStr &= " AND StatFactures.TypeFacture LIKE 'Facture unifiée%'"
                Case "* Prêts *"
                    whereStr &= " AND StatFactures.TypeFacture LIKE 'Prêt%'"
                Case "* Ventes *"
                    whereStr &= " AND StatFactures.TypeFacture LIKE 'Vente%'"
                Case "* Services *"
                    whereStr &= " AND (StatFactures.TypeFacture LIKE '" & System.Text.RegularExpressions.Regex.Replace(PreferencesManager.getGeneralPreferences()("Services").ToString.Replace("'", "''"), "\n", "%' OR StatFactures.TypeFacture LIKE '") & "%')"
                Case "* Autres *"
                    whereStr &= " AND StatFactures.TypeFacture IN ('" & System.Text.RegularExpressions.Regex.Replace(PreferencesManager.getGeneralPreferences()("AutresTypesBills").ToString.Replace("'", "''"), "\n", "','") & "')"
                Case Else
                    whereStr &= " AND StatFactures.TypeFacture LIKE '" & TypeFacture.Text.Replace("'", "''") & "%'"
            End Select
        End If

        'Payeurs
        If PayeurClient.Text <> "Aucun" Then
            Dim sPayeur() As String = System.Text.RegularExpressions.Regex.Split(PayeurClient.Text, " \(")
            whereStr &= " AND StatFactures.ParNoClient=" & sPayeur(1).Substring(0, sPayeur(1).Length - 1)
        End If
        If PayeurKP.Text <> "Aucun(e)" Then
            Dim sPayeur() As String = System.Text.RegularExpressions.Regex.Split(PayeurKP.Text, " \(")
            whereStr &= " AND StatFactures.ParNoKP=" & sPayeur(1).Substring(0, sPayeur(1).Length - 1)
        End If
        If PayeurUser.Text <> "Aucun" Then
            Dim sPayeur() As String = System.Text.RegularExpressions.Regex.Split(PayeurUser.Text, " \(")
            whereStr &= " AND StatFactures.ParNoUser=" & sPayeur(1).Substring(0, sPayeur(1).Length - 1)
        End If

        If NoFactureA.Text <> 0 Then whereStr &= " AND StatFactures.NoFacture >=" & NoFactureDe.Text & " AND StatFactures.NoFacture<=" & NoFactureA.Text

        'Date facture
        If DateDe.Text <> "" And DateA.Text <> "" And Me.DateAll.Checked = False Then
            whereStr &= " AND StatFactures.DateFacture >= '" & DateDe.Text & "' AND StatFactures.DateFacture <= '" & DateA.Text & "'"
        End If

        'Montant du depuis
        If MontantDuA.Text > 0 Then
            whereStr &= " AND StatFactures.DateFacture <= '" & DateFormat.getTextDate(Date.Now.AddDays(MontantDuDe.Text * -1)) & "' AND StatFactures.DateFacture >='" & DateFormat.getTextDate(Date.Now.AddDays(-1 * MontantDuA.Text)) & "' AND (SELECT TOP 1 COUNT(*) FROM Factures WHERE Factures.NoFacture = StatFactures.NoFacture)<>0"
        End If

        Dim descending As String = "DESC"
        Dim sortingOrder As DBLinker.SortOrderType = DBLinker.SortOrderType.Descending
        If PreferencesManager.getGeneralPreferences()("TriFactures").StartsWith("A") Then descending = "" : sortingOrder = DBLinker.SortOrderType.Ascending

        If whereStr.StartsWith(" AND") Then whereStr = " WHERE " & whereStr.Substring(4)
        If whereStr = "" Then whereStr = "WHERE 1=1 "
        Dim factures As DataSet = DBLinker.getInstance.readDBForGrid("Statfactures LEFT OUTER JOIN                       InfoClients AS IC1 ON IC1.NoClient = Statfactures.NoClient LEFT OUTER JOIN                       KeyPeople AS KP1 ON KP1.NoKP = Statfactures.NoKp LEFT OUTER JOIN                       Utilisateurs AS U1 ON U1.NoUser = Statfactures.NoUser LEFT OUTER JOIN                       InfoClients AS IC2 ON IC2.NoClient = Statfactures.ParNoClient LEFT OUTER JOIN                       Utilisateurs AS U2 ON U2.NoUser = Statfactures.ParNoUser LEFT OUTER JOIN                       KeyPeople AS KP2 ON KP2.NoKP = Statfactures.ParNoKp", "TOP 10000 DF, TF, MF , CASE WHEN MP IS NULL THEN 0 ELSE MP END AS MP,PC , PPO, PU, EL , NoFacture FROM (SELECT     MIN(Statfactures.DateFacture) AS DF, MIN(Statfactures.TypeFacture) AS TF, CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN SUM(Statfactures.MontantFacture) ELSE dbo.GetLinkedBillMF(Statfactures.NoFacture,0) END AS [MF], CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN (SELECT SUM(MontantPaiement) FROM StatPaiements WHERE StatPaiements.NoFacture = Statfactures.NoFacture) ELSE dbo.GetLinkedBillMP(Statfactures.NoFacture,0) END AS MP, CASE WHEN MIN(Statfactures.ParNoClient)=0 THEN 'Aucun' ELSE MIN(IC2.Nom) + ',' + MIN(IC2.Prenom) END AS PC, CASE WHEN MIN(Statfactures.ParNoKP)=0 THEN 'Aucun' ELSE MIN(KP2.Nom) END AS PPO, CASE WHEN MIN(Statfactures.ParNoUser)=0 THEN 'Aucun' ELSE MIN(U2.Nom) + ',' + MIN(U2.Prenom) END AS PU, CASE WHEN MIN(Statfactures.NoClient)=0 THEN CASE WHEN MIN(Statfactures.NoKP)=0 THEN CASE WHEN MIN(Statfactures.NoUserFacture)=0 THEN 'Aucun' ELSE MIN(U1.Nom) + ',' + MIN(U1.Prenom) END ELSE MIN(KP1.Nom) END ELSE MIN(IC1.Nom) + ',' + MIN(IC1.Prenom) END AS EL, Statfactures.NoFacture,MIN(Statfactures.NoFactureRef) AS FRef, (SELECT TOP 1 SF2.NoAction FROM Statfactures AS SF2 WHERE SF2.NoStat = MAX(Statfactures.NoStat)) AS Action", whereStr & " GROUP BY Statfactures.NoFacture) AS Test WHERE Action<>20 ORDER BY DF DESC")
        FacturesView.DataSource = factures.Tables(0)
        If factures.Tables(0).Rows.Count = 10000 Then MessageBox.Show("Vous avez atteint la limite de 10000 factures chargés en même temps. Veuillez sélectionner plus de filtre pour réduire le nombre de factures trouvés.", "Nombre de factures maximal")
    End Sub

    Private Sub noFactureDe_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoFactureDe.TextChanged
        If NoFactureA.Text = "" Or NoFactureDe.Text = "" Then Exit Sub
        If CType(NoFactureA.Text, Integer) < CType(NoFactureDe.Text, Integer) Then NoFactureA.Text = NoFactureDe.Text
    End Sub

    Private Sub noFactureA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoFactureA.TextChanged
        If NoFactureA.Text = "" Or NoFactureDe.Text = "" Then Exit Sub
        If CType(NoFactureA.Text, Integer) < CType(NoFactureDe.Text, Integer) Then NoFactureDe.Text = NoFactureA.Text
    End Sub

    Private Sub montantDuDe_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MontantDuDe.TextChanged
        If MontantDuA.Text = "" Or MontantDuDe.Text = "" Then Exit Sub
        If CType(MontantDuA.Text, Integer) < CType(MontantDuDe.Text, Integer) Then MontantDuDe.Text = MontantDuA.Text
    End Sub

    Private Sub montantDuA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MontantDuA.TextChanged
        If MontantDuA.Text = "" Or MontantDuDe.Text = "" Then Exit Sub
        If CType(MontantDuA.Text, Integer) < CType(MontantDuDe.Text, Integer) Then MontantDuA.Text = MontantDuDe.Text
    End Sub

    Private Sub filter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles filter.Click
        filter.Enabled = False
        lblListStatus.Text = "Chargement..."
        lblListStatus.Left = (Me.Width - lblListStatus.Width) / 2
        lblListStatus.Visible = True
        Application.DoEvents()
        Try
            loadFactures()
            filter.Enabled = True
        Catch ex As Exception
            addErrorLog(ex)
        End Try

        If FacturesView.Rows.Count = 0 Then
            lblListStatus.Text = "Aucune facture trouvée"
            lblListStatus.Left = (Me.Width - lblListStatus.Width) / 2
        Else
            lblListStatus.Visible = False
        End If
    End Sub

    Private Sub adminClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminClose.Click
        AdminBox.Visible = False
    End Sub

    Private Sub toggleSouffrance()
        If curFactureBox.curFacture.isSouffrance = False AndAlso curFactureBox.curFacture.isPaymentsToDo = False Then
            MessageBox.Show("Une facture ne peut être en souffrance que si elle a un montant dû.", "Facture en souffrance")
            Exit Sub
        End If

        Dim message As String = "Êtes-vous sûr de vouloir mettre en souffrance cette facture ?"
        If curFactureBox.curFacture.isSouffrance Then message = "Êtes-vous sûr de vouloir enlever le status de souffrance de cette facture ?"
        If MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        curFactureBox.curFacture.toggleSouffrance()
        curFactureBox.loading(curFactureBox.noFacture)
        If curFactureBox.curFacture.isSouffrance Then
            ToolTip1.SetToolTip(btnToggleSouffrance, "Enlever le statut de souffrance sur la facture")
        Else
            ToolTip1.SetToolTip(btnToggleSouffrance, "Signaler que la facture est en souffrance")
        End If
    End Sub

    Private Sub enableStop()
        With curFactureBox
            If .locked Then
                .lockSecteur("Facturation", currentLocks)

                If .locked Then
                    MessageBox.Show("La facturation pour ce client, cette personne/organisme clé, cet utilisateur et/ou cette clinique est déjà en cours de modification par un autre utilisateur.", "Secteur en cours d'utilisation")
                Else
                    FacturesView.Enabled = False
                    btnModif.ImageIndex = 1
                    ToolTip1.SetToolTip(btnModif, "Arrêter de modifier cette facture")
                    btnRecu.Enabled = curFactureBox.curFacture.isReceiptToDo()
                    btnToggleSouffrance.Enabled = True
                    If .curFacture.isSouffrance Then
                        ToolTip1.SetToolTip(btnToggleSouffrance, "Enlever le statut de souffrance sur la facture")
                    Else
                        ToolTip1.SetToolTip(btnToggleSouffrance, "Signaler la facture en souffrance")
                    End If
                    If .curFacture.noClient > 0 Then
                        If currentLocksCounting.ContainsKey("ClientFacturation-" & .curFacture.noClient & "-") Then
                            currentLocksCounting("ClientFacturation-" & .curFacture.noClient & "-") += 1
                        Else
                            currentLocksCounting.Add("ClientFacturation-" & .curFacture.noClient & "-", 1)
                        End If
                    End If
                    If .curFacture.parNoClient > 0 And .curFacture.parNoClient <> .curFacture.noClient Then
                        If currentLocksCounting.ContainsKey("ClientFacturation-" & .curFacture.parNoClient & "-") Then
                            currentLocksCounting("ClientFacturation-" & .curFacture.parNoClient & "-") += 1
                        Else
                            currentLocksCounting.Add("ClientFacturation-" & .curFacture.parNoClient & "-", 1)
                        End If
                    End If
                    If .curFacture.noKP > 0 Then
                        If currentLocksCounting.ContainsKey("KPFacturation-" & .curFacture.noKP & "-") Then
                            currentLocksCounting("KPFacturation-" & .curFacture.noKP & "-") += 1
                        Else
                            currentLocksCounting.Add("KPFacturation-" & .curFacture.noKP & "-", 1)
                        End If
                    End If
                    If .curFacture.parNoKP > 0 And .curFacture.parNoKP <> .curFacture.noKP Then
                        If currentLocksCounting.ContainsKey("KPFacturation-" & .curFacture.parNoKP & "-") Then
                            currentLocksCounting("KPFacturation-" & .curFacture.parNoKP & "-") += 1
                        Else
                            currentLocksCounting.Add("KPFacturation-" & .curFacture.parNoKP & "-", 1)
                        End If
                    End If
                    If .curFacture.noUserFacture > 0 Then
                        If currentLocksCounting.ContainsKey("UserFacturation-" & .curFacture.noUserFacture) Then
                            currentLocksCounting("UserFacturation-" & .curFacture.noUserFacture) += 1
                        Else
                            currentLocksCounting.Add("UserFacturation-" & .curFacture.noUserFacture, 1)
                        End If
                    End If
                    If .curFacture.parNoUser > 0 And .curFacture.parNoUser <> .curFacture.noUserFacture Then
                        If currentLocksCounting.ContainsKey("UserFacturation-" & .curFacture.parNoUser) Then
                            currentLocksCounting("UserFacturation-" & .curFacture.parNoUser) += 1
                        Else
                            currentLocksCounting.Add("UserFacturation-" & .curFacture.parNoUser, 1)
                        End If
                    End If
                    If .curFacture.parNoClinique > 0 Then
                        If currentLocksCounting.ContainsKey("CliniqueFacturation-" & .curFacture.parNoClinique) Then
                            currentLocksCounting("CliniqueFacturation-" & .curFacture.parNoClinique) += 1
                        Else
                            currentLocksCounting.Add("CliniqueFacturation-" & .curFacture.parNoClinique, 1)
                        End If
                    End If
                End If
            Else
                FacturesView.Enabled = True
                .locked = True
                btnModif.ImageIndex = 0
                btnToggleSouffrance.Enabled = False
                ToolTip1.SetToolTip(btnModif, "Commencer à modifier cette facture")
                If .curFacture.noClient > 0 Then
                    If currentLocksCounting("ClientFacturation-" & .curFacture.noClient & "-") = 1 Then
                        lockSecteur("ClientFacturation-" & .curFacture.noClient & "-", False, "Facturation")
                        currentLocks.Remove("ClientFacturation-" & .curFacture.noClient & "-")
                        currentLocksCounting.Remove("ClientFacturation-" & .curFacture.noClient & "-")
                    Else
                        currentLocksCounting("ClientFacturation-" & .curFacture.noClient & "-") -= 1
                    End If
                End If
                If .curFacture.parNoClient > 0 And .curFacture.parNoClient <> .curFacture.noClient Then
                    If currentLocksCounting("ClientFacturation-" & .curFacture.parNoClient & "-") = 1 Then
                        lockSecteur("ClientFacturation-" & .curFacture.parNoClient & "-", False, "Facturation")
                        currentLocks.Remove("ClientFacturation-" & .curFacture.parNoClient & "-")
                        currentLocksCounting.Remove("ClientFacturation-" & .curFacture.parNoClient & "-")
                    Else
                        currentLocksCounting("ClientFacturation-" & .curFacture.parNoClient & "-") -= 1
                    End If
                End If
                If .curFacture.noKP > 0 Then
                    If currentLocksCounting("KPFacturation-" & .curFacture.noKP & "-") = 1 Then
                        lockSecteur("KPFacturation-" & .curFacture.noKP & "-", False, "Facturation")
                        currentLocks.Remove("KPFacturation-" & .curFacture.noKP & "-")
                        currentLocksCounting.Remove("KPFacturation-" & .curFacture.noKP & "-")
                    Else
                        currentLocksCounting("KPFacturation-" & .curFacture.noKP & "-") -= 1
                    End If
                End If
                If .curFacture.parNoKP > 0 And .curFacture.parNoKP <> .curFacture.noKP Then
                    If currentLocksCounting("KPFacturation-" & .curFacture.parNoKP & "-") = 1 Then
                        lockSecteur("KPFacturation-" & .curFacture.parNoKP & "-", False, "Facturation")
                        currentLocks.Remove("KPFacturation-" & .curFacture.parNoKP & "-")
                        currentLocksCounting.Remove("KPFacturation-" & .curFacture.parNoKP & "-")
                    Else
                        currentLocksCounting("KPFacturation-" & .curFacture.parNoKP & "-") -= 1
                    End If
                End If
                If .curFacture.noUserFacture > 0 Then
                    If currentLocksCounting("UserFacturation-" & .curFacture.noUserFacture) = 1 Then
                        lockSecteur("UserFacturation-" & .curFacture.noUserFacture, False, "Facturation")
                        currentLocks.Remove("UserFacturation-" & .curFacture.noUserFacture)
                        currentLocksCounting.Remove("UserFacturation-" & .curFacture.noUserFacture)
                    Else
                        currentLocksCounting("UserFacturation-" & .curFacture.noUserFacture) -= 1
                    End If
                End If
                If .curFacture.parNoUser > 0 And .curFacture.parNoUser <> .curFacture.noUserFacture Then
                    If currentLocksCounting("UserFacturation-" & .curFacture.parNoUser) = 1 Then
                        lockSecteur("UserFacturation-" & .curFacture.parNoUser, False, "Facturation")
                        currentLocks.Remove("UserFacturation-" & .curFacture.parNoUser)
                        currentLocksCounting.Remove("UserFacturation-" & .curFacture.parNoUser)
                    Else
                        currentLocksCounting("UserFacturation-" & .curFacture.parNoUser) -= 1
                    End If
                End If
                If .curFacture.parNoClinique > 0 Then
                    If currentLocksCounting("CliniqueFacturation-" & .curFacture.parNoClinique) = 1 Then
                        lockSecteur("CliniqueFacturation-" & .curFacture.parNoClinique, False, "Facturation")
                        currentLocks.Remove("CliniqueFacturation-" & .curFacture.parNoClinique)
                        currentLocksCounting.Remove("CliniqueFacturation-" & .curFacture.parNoClinique)
                    Else
                        currentLocksCounting("CliniqueFacturation-" & .curFacture.parNoClinique) -= 1
                    End If
                End If
                btnRecu.Enabled = False
            End If
        End With
    End Sub

    Private Sub unify()
        Dim bills As New Collections.Generic.List(Of Bill)
        With FacturesView
            For i As Integer = 0 To .SelectedRows.Count - 1
                bills.Add(New Bill(.SelectedRows(i).Cells("NoFacture").Value))
            Next i
        End With

        Dim acceptedMSG As String = Bill.joinBills(bills)
        If acceptedMSG <> "" Then MessageBox.Show(acceptedMSG, "Impossible d'unifier")
    End Sub

    Private Sub desunify()
        If curFactureBox.isLinked Then MessageBox.Show("Impossible du supprimer cette facture unifiée, car elle est déjà liée à une facture unifiée", "Suppression impossible") : Exit Sub

        Dim unifiedBill As Bill = curFactureBox.curFacture

        With unifiedBill
            DBLinker.getInstance.delDB("FacturesLinked", "NoFacture", .noFacture, False)
            'TODO : Shall adjust unlink to be as linking... So it shall add a line by entity billed + Add missing fields
            DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere", "20," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & .noFacture & "," & .amountBilledToClient.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & .taxe1.ToString.Replace(",", ".") & "," & .taxe2.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered))
        End With

        ''Modifications des sous-factures
        Dim subFactures() As String = unifiedBill.noBillRef.Split(New Char() {"§"})
        Dim i As Integer
        'Build where STRING
        Dim whereStr As String = ""
        For i = 1 To subFactures.GetUpperBound(0)
            whereStr &= " OR (NoFacture = " & subFactures(i) & ")"
        Next i

        Dim noFacture As Integer = subFactures(0)
        DBLinker.getInstance.updateDB("Factures", "NoFactureTransfere = null", "NoFacture", noFacture & whereStr, False)
        DBLinker.getInstance.updateDB("StatFactures", "NoFactureTransfere = null", "NoFacture", noFacture & whereStr, False)

        loadFactures()
    End Sub

    Private Sub createRecu()
        If MessageBox.Show("Êtes-vous sûr de vouloir émettre le reçu ?" & vbCrLf & "(Cette opération bloque la modification de la facture)", "Création du reçu", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim myFacture As Bill = curFactureBox.curFacture
        myFacture.generateReceipt()
    End Sub

    Public Sub askSecondEntity(ByVal sender As MenuItem, ByVal tableName As String, ByVal fieldName As String, ByVal whereStr As String, ByVal nom As String)
        Dim myMultiChoice As New multichoice
        Dim choices() As String = DBLinker.getInstance.readOneDBField(tableName, fieldName, whereStr)
        If choices Is Nothing OrElse choices.Length = 0 Then Exit Sub
        Dim choice As String = myMultiChoice.GetChoice("Veuillez sélectionner " & nom, String.Join("§", choices), , "§", False)
        If choice.ToUpper.StartsWith("ERROR") Then Exit Sub

        menuVisite.Checked = False
        menuPret.Checked = False
        menuVente.Checked = False

        sender.Checked = True
        EntiteSecondaire.Text = choice
    End Sub

    Private Sub menuVisite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuVisite.Click
        Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntitePrimaire.Text, "\) ")
        askSecondEntity(sender, "InfoVisites INNER JOIN InfoFolders ON InfoFolders.NoFolder = InfoVisites.NoFolder INNER JOIN Utilisateurs ON Utilisateurs.NoUser = InfoVisites.NoTRP", "CONVERT(varchar(40),NoVisite) + ' - ' + CONVERT(varchar(MAX),InfoVisites.DateHeure) + '  ' + InfoVisites.Service + ' avec ' + Utilisateurs.Nom + ',' + Utilisateurs.Prenom + ' (Dossier:' + CONVERT(varchar(40),InfoFolders.NoFolder) + ')'", "WHERE InfoVisites.NoFacture > 0 AND InfoVisites.NoFolder = " & sEntite(1).Substring(1) & " ORDER BY InfoVisites.DateHeure DESC", "un rendez-vous")
    End Sub

    Private Sub menuVente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuVente.Click
        Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntitePrimaire.Text, "\) ")
        askSecondEntity(sender, "Ventes INNER JOIN InfoFolders ON InfoFolders.NoFolder = Ventes.NoFolder INNER JOIN Equipements ON Equipements.NoEquipement = Ventes.NoEquipement", "CONVERT(varchar(40),NoVente) + ' - ' + Equipements.NomItem + '-' + Ventes.NoItem + ' (Dossier:' + CONVERT(varchar(40),InfoFolders.NoFolder) + ')'", "WHERE Ventes.NoFacture > 0 AND Ventes.NoFolder=" & sEntite(1).Substring(1) & " ORDER BY Ventes.NoVente DESC", "une vente")
    End Sub

    Private Sub menuPret_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuPret.Click
        Dim sEntite() As String = System.Text.RegularExpressions.Regex.Split(EntitePrimaire.Text, "\) ")
        askSecondEntity(sender, "Prets INNER JOIN InfoFolders ON InfoFolders.NoFolder = Prets.NoFolder INNER JOIN Equipements ON Equipements.NoEquipement = Prets.NoEquipement", "CONVERT(varchar(40),NoPret) + ' - ' + Equipements.NomItem + '-' + Prets.NoItem + ' (Dossier:' + CONVERT(varchar(40),InfoFolders.NoFolder) + ')'", "WHERE Prets.NoFacture > 0 AND Prets.NoFolder=" & sEntite(1).Substring(1) & " ORDER BY Prets.NoPret DESC", "un prêt")
    End Sub

    Private Sub facturesView_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles FacturesView.RowStateChanged
        If e.StateChanged = DataGridViewElementStates.None Then
            resetFacture()
        End If
    End Sub

    Private Sub facturesView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FacturesView.SelectionChanged
        If FacturesView.currentRow Is Nothing Then Exit Sub

        Dim curLoadedBill As Integer = Integer.Parse(FacturesView.currentRow.Cells("NoFacture").Value.ToString)
        If curLoadedBill = lastLoadedBill Then Exit Sub

        lastLoadedBill = curLoadedBill

        curFactureBox.loading(lastLoadedBill)
        btnModif.Enabled = True
        If FacturesView.SelectedRows.Count > 1 Then
            btnUnify.Enabled = True
        Else
            btnUnify.Enabled = False
        End If
        If curFactureBox.curFacture.noBillRef <> "" And curFactureBox.curFacture.getNoReceiptsString() = "" Then
            btnDesunify.Enabled = True
        Else
            btnDesunify.Enabled = False
        End If
    End Sub

    Private Sub resetFacture()
        FacturesView.Enabled = True
        btnModif.Enabled = False
        btnUnify.Enabled = False
        btnDesunify.Enabled = False
        btnRecu.Enabled = False
        Me.curFactureBox.locked = True
        btnModif.ImageIndex = 0
    End Sub

    Private Sub btnModif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModif.Click
        enableStop()
    End Sub

    Private Sub btnUnify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnify.Click
        unify()
    End Sub

    Private Sub btnDesunify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDesunify.Click
        desunify()
    End Sub

    Private Sub btnRecu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecu.Click
        createRecu()
    End Sub

    Private Sub btnToggleSouffrance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggleSouffrance.Click
        toggleSouffrance()
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
