Friend Class addBill
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        Me.MdiParent = myMainWin
        Dim selectIMG As Image = DrawingManager.getInstance.getImage("selection16.gif")
        selectPrimeEntity.Image = selectIMG
        selectUser.Image = selectIMG
        selectClient.Image = selectIMG
        selectKP.Image = selectIMG
        createBill.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance().getImage("newBill16.jpg"))
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupPayeurs As System.Windows.Forms.GroupBox
    Friend WithEvents PourcentParNoClient As Clinica.ManagedCombo
    Friend WithEvents PayeurKP As System.Windows.Forms.TextBox
    Friend WithEvents PayeurUser As System.Windows.Forms.TextBox
    Friend WithEvents PayeurClient As System.Windows.Forms.TextBox
    Public WithEvents selectClient As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents selectKP As System.Windows.Forms.Button
    Public WithEvents selectUser As System.Windows.Forms.Button
    Friend WithEvents PourcentParNoUser As Clinica.ManagedCombo
    Friend WithEvents PourcentParNoKp As Clinica.ManagedCombo
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents selectPrimeEntity As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Labels As System.Windows.Forms.Label
    Friend WithEvents Entite As System.Windows.Forms.ComboBox
    Friend WithEvents EntitePrimaire As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TypeFacture As System.Windows.Forms.ComboBox
    Friend WithEvents MontantFacture As ManagedText
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents description As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ApplyTaxes As System.Windows.Forms.CheckBox
    Public WithEvents createBill As System.Windows.Forms.Button
    Friend WithEvents menuSelectEntity As System.Windows.Forms.ContextMenu
    Friend WithEvents menuSelect As System.Windows.Forms.MenuItem
    Friend WithEvents menuLine2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuAucune1 As System.Windows.Forms.MenuItem

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.selectClient = New System.Windows.Forms.Button
        Me.selectKP = New System.Windows.Forms.Button
        Me.selectUser = New System.Windows.Forms.Button
        Me.selectPrimeEntity = New System.Windows.Forms.Button
        Me.createBill = New System.Windows.Forms.Button
        Me.GroupPayeurs = New System.Windows.Forms.GroupBox
        Me.PourcentParNoUser = New Clinica.ManagedCombo
        Me.PourcentParNoKp = New Clinica.ManagedCombo
        Me.PourcentParNoClient = New Clinica.ManagedCombo
        Me.PayeurKP = New System.Windows.Forms.TextBox
        Me.PayeurUser = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.PayeurClient = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Labels = New System.Windows.Forms.Label
        Me.Entite = New System.Windows.Forms.ComboBox
        Me.EntitePrimaire = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.TypeFacture = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.description = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.ApplyTaxes = New System.Windows.Forms.CheckBox
        Me.menuSelectEntity = New System.Windows.Forms.ContextMenu
        Me.menuSelect = New System.Windows.Forms.MenuItem
        Me.menuLine2 = New System.Windows.Forms.MenuItem
        Me.menuAucune1 = New System.Windows.Forms.MenuItem
        Me.MontantFacture = New ManagedText
        Me.GroupPayeurs.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
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
        Me.ToolTip1.SetToolTip(Me.selectClient, "Sélectionner un client payeur")
        Me.selectClient.UseVisualStyleBackColor = False
        '
        'selectKP
        '
        Me.selectKP.BackColor = System.Drawing.SystemColors.Control
        Me.selectKP.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectKP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectKP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectKP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectKP.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectKP.Location = New System.Drawing.Point(186, 19)
        Me.selectKP.Name = "selectKP"
        Me.selectKP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectKP.Size = New System.Drawing.Size(24, 24)
        Me.selectKP.TabIndex = 224
        Me.selectKP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectKP, "Sélectionner un(e) personne / organisme clé payeur(euse)")
        Me.selectKP.UseVisualStyleBackColor = False
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
        Me.ToolTip1.SetToolTip(Me.selectUser, "Sélectionner un utilisateur payeur")
        Me.selectUser.UseVisualStyleBackColor = False
        '
        'selectPrimeEntity
        '
        Me.selectPrimeEntity.BackColor = System.Drawing.SystemColors.Control
        Me.selectPrimeEntity.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectPrimeEntity.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectPrimeEntity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectPrimeEntity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectPrimeEntity.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectPrimeEntity.Location = New System.Drawing.Point(12, 35)
        Me.selectPrimeEntity.Name = "selectPrimeEntity"
        Me.selectPrimeEntity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectPrimeEntity.Size = New System.Drawing.Size(24, 24)
        Me.selectPrimeEntity.TabIndex = 235
        Me.selectPrimeEntity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectPrimeEntity, "Sélectionner l'entité liée spécifique primaire")
        Me.selectPrimeEntity.UseVisualStyleBackColor = False
        '
        'createBill
        '
        Me.createBill.BackColor = System.Drawing.SystemColors.Control
        Me.createBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.createBill.Enabled = False
        Me.createBill.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.createBill.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.createBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.createBill.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.createBill.Location = New System.Drawing.Point(676, 211)
        Me.createBill.Name = "createBill"
        Me.createBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.createBill.Size = New System.Drawing.Size(24, 24)
        Me.createBill.TabIndex = 227
        Me.createBill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.createBill, "Créer une nouvelle facture")
        Me.createBill.UseVisualStyleBackColor = False
        '
        'GroupPayeurs
        '
        Me.GroupPayeurs.Controls.Add(Me.PourcentParNoUser)
        Me.GroupPayeurs.Controls.Add(Me.PourcentParNoKp)
        Me.GroupPayeurs.Controls.Add(Me.PourcentParNoClient)
        Me.GroupPayeurs.Controls.Add(Me.PayeurKP)
        Me.GroupPayeurs.Controls.Add(Me.PayeurUser)
        Me.GroupPayeurs.Controls.Add(Me.Label3)
        Me.GroupPayeurs.Controls.Add(Me.PayeurClient)
        Me.GroupPayeurs.Controls.Add(Me.Label2)
        Me.GroupPayeurs.Controls.Add(Me.selectClient)
        Me.GroupPayeurs.Controls.Add(Me.Label1)
        Me.GroupPayeurs.Controls.Add(Me.Label6)
        Me.GroupPayeurs.Controls.Add(Me.Label8)
        Me.GroupPayeurs.Controls.Add(Me.Label10)
        Me.GroupPayeurs.Controls.Add(Me.selectKP)
        Me.GroupPayeurs.Controls.Add(Me.selectUser)
        Me.GroupPayeurs.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPayeurs.Location = New System.Drawing.Point(12, 131)
        Me.GroupPayeurs.Name = "GroupPayeurs"
        Me.GroupPayeurs.Size = New System.Drawing.Size(688, 74)
        Me.GroupPayeurs.TabIndex = 229
        Me.GroupPayeurs.TabStop = False
        Me.GroupPayeurs.Text = "Entité(s) payeuse(s)"
        '
        'PourcentParNoUser
        '
        Me.PourcentParNoUser.acceptAlpha = True
        Me.PourcentParNoUser.acceptedChars = ",§."
        Me.PourcentParNoUser.acceptNumeric = True
        Me.PourcentParNoUser.allCapital = False
        Me.PourcentParNoUser.allLower = False
        Me.PourcentParNoUser.autoComplete = True
        Me.PourcentParNoUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PourcentParNoUser.autoSizeDropDown = True
        Me.PourcentParNoUser.BackColor = System.Drawing.Color.White
        Me.PourcentParNoUser.blockOnMaximum = False
        Me.PourcentParNoUser.blockOnMinimum = False
        Me.PourcentParNoUser.cb_AcceptLeftZeros = False
        Me.PourcentParNoUser.cb_AcceptNegative = False
        Me.PourcentParNoUser.currencyBox = True
        Me.PourcentParNoUser.dbField = Nothing
        Me.PourcentParNoUser.doComboDelete = True
        Me.PourcentParNoUser.firstLetterCapital = False
        Me.PourcentParNoUser.firstLettersCapital = False
        Me.PourcentParNoUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.PourcentParNoUser.FormattingEnabled = True
        Me.PourcentParNoUser.Items.AddRange(New Object() {"5", "10", "15", "20", "30", "35", "40", "45", "50", "55", "60", "65", "70", "75", "80", "85", "90", "95", "100"})
        Me.PourcentParNoUser.itemsToolTipDuration = 10000
        Me.PourcentParNoUser.Location = New System.Drawing.Point(584, 47)
        Me.PourcentParNoUser.manageText = True
        Me.PourcentParNoUser.matchExp = Nothing
        Me.PourcentParNoUser.maximum = 0
        Me.PourcentParNoUser.minimum = 0
        Me.PourcentParNoUser.Name = "PourcentParNoUser"
        Me.PourcentParNoUser.nbDecimals = CType(4, Short)
        Me.PourcentParNoUser.onlyAlphabet = False
        Me.PourcentParNoUser.pathOfList = Nothing
        Me.PourcentParNoUser.ReadOnly = False
        Me.PourcentParNoUser.refuseAccents = False
        Me.PourcentParNoUser.refusedChars = Nothing
        Me.PourcentParNoUser.showItemsToolTip = False
        Me.PourcentParNoUser.Size = New System.Drawing.Size(70, 21)
        Me.PourcentParNoUser.TabIndex = 234
        Me.PourcentParNoUser.trimText = False
        '
        'PourcentParNoKp
        '
        Me.PourcentParNoKp.acceptAlpha = True
        Me.PourcentParNoKp.acceptedChars = ",§."
        Me.PourcentParNoKp.acceptNumeric = True
        Me.PourcentParNoKp.allCapital = False
        Me.PourcentParNoKp.allLower = False
        Me.PourcentParNoKp.autoComplete = True
        Me.PourcentParNoKp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PourcentParNoKp.autoSizeDropDown = True
        Me.PourcentParNoKp.BackColor = System.Drawing.Color.White
        Me.PourcentParNoKp.blockOnMaximum = False
        Me.PourcentParNoKp.blockOnMinimum = False
        Me.PourcentParNoKp.cb_AcceptLeftZeros = False
        Me.PourcentParNoKp.cb_AcceptNegative = False
        Me.PourcentParNoKp.currencyBox = True
        Me.PourcentParNoKp.dbField = Nothing
        Me.PourcentParNoKp.doComboDelete = True
        Me.PourcentParNoKp.firstLetterCapital = False
        Me.PourcentParNoKp.firstLettersCapital = False
        Me.PourcentParNoKp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.PourcentParNoKp.FormattingEnabled = True
        Me.PourcentParNoKp.Items.AddRange(New Object() {"5", "10", "15", "20", "30", "35", "40", "45", "50", "55", "60", "65", "70", "75", "80", "85", "90", "95", "100"})
        Me.PourcentParNoKp.itemsToolTipDuration = 10000
        Me.PourcentParNoKp.Location = New System.Drawing.Point(384, 47)
        Me.PourcentParNoKp.manageText = True
        Me.PourcentParNoKp.matchExp = Nothing
        Me.PourcentParNoKp.maximum = 0
        Me.PourcentParNoKp.minimum = 0
        Me.PourcentParNoKp.Name = "PourcentParNoKp"
        Me.PourcentParNoKp.nbDecimals = CType(4, Short)
        Me.PourcentParNoKp.onlyAlphabet = False
        Me.PourcentParNoKp.pathOfList = Nothing
        Me.PourcentParNoKp.ReadOnly = False
        Me.PourcentParNoKp.refuseAccents = False
        Me.PourcentParNoKp.refusedChars = Nothing
        Me.PourcentParNoKp.showItemsToolTip = False
        Me.PourcentParNoKp.Size = New System.Drawing.Size(70, 21)
        Me.PourcentParNoKp.TabIndex = 234
        Me.PourcentParNoKp.trimText = False
        '
        'PourcentParNoClient
        '
        Me.PourcentParNoClient.acceptAlpha = True
        Me.PourcentParNoClient.acceptedChars = ",§."
        Me.PourcentParNoClient.acceptNumeric = True
        Me.PourcentParNoClient.allCapital = False
        Me.PourcentParNoClient.allLower = False
        Me.PourcentParNoClient.autoComplete = True
        Me.PourcentParNoClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PourcentParNoClient.autoSizeDropDown = True
        Me.PourcentParNoClient.BackColor = System.Drawing.Color.White
        Me.PourcentParNoClient.blockOnMaximum = False
        Me.PourcentParNoClient.blockOnMinimum = False
        Me.PourcentParNoClient.cb_AcceptLeftZeros = False
        Me.PourcentParNoClient.cb_AcceptNegative = False
        Me.PourcentParNoClient.currencyBox = True
        Me.PourcentParNoClient.dbField = Nothing
        Me.PourcentParNoClient.doComboDelete = True
        Me.PourcentParNoClient.firstLetterCapital = False
        Me.PourcentParNoClient.firstLettersCapital = False
        Me.PourcentParNoClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.PourcentParNoClient.FormattingEnabled = True
        Me.PourcentParNoClient.Items.AddRange(New Object() {"5", "10", "15", "20", "30", "35", "40", "45", "50", "55", "60", "65", "70", "75", "80", "85", "90", "95", "100"})
        Me.PourcentParNoClient.itemsToolTipDuration = 10000
        Me.PourcentParNoClient.Location = New System.Drawing.Point(80, 47)
        Me.PourcentParNoClient.manageText = True
        Me.PourcentParNoClient.matchExp = Nothing
        Me.PourcentParNoClient.maximum = 0
        Me.PourcentParNoClient.minimum = 0
        Me.PourcentParNoClient.Name = "PourcentParNoClient"
        Me.PourcentParNoClient.nbDecimals = CType(4, Short)
        Me.PourcentParNoClient.onlyAlphabet = False
        Me.PourcentParNoClient.pathOfList = Nothing
        Me.PourcentParNoClient.ReadOnly = False
        Me.PourcentParNoClient.refuseAccents = False
        Me.PourcentParNoClient.refusedChars = Nothing
        Me.PourcentParNoClient.showItemsToolTip = False
        Me.PourcentParNoClient.Size = New System.Drawing.Size(70, 21)
        Me.PourcentParNoClient.TabIndex = 234
        Me.PourcentParNoClient.trimText = False
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(654, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 13)
        Me.Label3.TabIndex = 219
        Me.Label3.Text = "%"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(454, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 219
        Me.Label2.Text = "%"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(151, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 13)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "%"
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(42, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(182, 13)
        Me.Label4.TabIndex = 234
        Me.Label4.Text = "Entité liée spécifique primaire :"
        '
        'Labels
        '
        Me.Labels.AutoSize = True
        Me.Labels.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Labels.Location = New System.Drawing.Point(9, 9)
        Me.Labels.Name = "Labels"
        Me.Labels.Size = New System.Drawing.Size(72, 13)
        Me.Labels.TabIndex = 233
        Me.Labels.Text = "Entité liée :"
        '
        'Entite
        '
        Me.Entite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Entite.Items.AddRange(New Object() {"Client", "Personne / organisme clé", "Utilisateur"})
        Me.Entite.Location = New System.Drawing.Point(87, 6)
        Me.Entite.Name = "Entite"
        Me.Entite.Size = New System.Drawing.Size(260, 21)
        Me.Entite.TabIndex = 232
        '
        'EntitePrimaire
        '
        Me.EntitePrimaire.BackColor = System.Drawing.SystemColors.Control
        Me.EntitePrimaire.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EntitePrimaire.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EntitePrimaire.Location = New System.Drawing.Point(230, 40)
        Me.EntitePrimaire.Name = "EntitePrimaire"
        Me.EntitePrimaire.Size = New System.Drawing.Size(120, 13)
        Me.EntitePrimaire.TabIndex = 236
        Me.EntitePrimaire.Text = "Aucune"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(9, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(105, 13)
        Me.Label13.TabIndex = 241
        Me.Label13.Text = "Type de facture :"
        '
        'TypeFacture
        '
        Me.TypeFacture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeFacture.Location = New System.Drawing.Point(120, 70)
        Me.TypeFacture.Name = "TypeFacture"
        Me.TypeFacture.Size = New System.Drawing.Size(230, 21)
        Me.TypeFacture.Sorted = True
        Me.TypeFacture.TabIndex = 240
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 13)
        Me.Label5.TabIndex = 219
        Me.Label5.Text = "Montant facturé :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(188, 108)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(13, 13)
        Me.Label7.TabIndex = 219
        Me.Label7.Text = "$"
        '
        'description
        '
        Me.description.Location = New System.Drawing.Point(356, 25)
        Me.description.Multiline = True
        Me.description.Name = "description"
        Me.description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.description.Size = New System.Drawing.Size(344, 100)
        Me.description.TabIndex = 243
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(353, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 233
        Me.Label9.Text = "Description :"
        '
        'ApplyTaxes
        '
        Me.ApplyTaxes.AutoSize = True
        Me.ApplyTaxes.Location = New System.Drawing.Point(236, 107)
        Me.ApplyTaxes.Name = "ApplyTaxes"
        Me.ApplyTaxes.Size = New System.Drawing.Size(114, 17)
        Me.ApplyTaxes.TabIndex = 244
        Me.ApplyTaxes.Text = "Appliquer les taxes"
        Me.ApplyTaxes.UseVisualStyleBackColor = True
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
        'MontantFacture
        '
        Me.MontantFacture.acceptAlpha = False
        Me.MontantFacture.acceptedChars = ",§."
        Me.MontantFacture.acceptNumeric = True
        Me.MontantFacture.allCapital = False
        Me.MontantFacture.allLower = False
        Me.MontantFacture.blockOnMaximum = False
        Me.MontantFacture.blockOnMinimum = False
        Me.MontantFacture.cb_AcceptLeftZeros = False
        Me.MontantFacture.cb_AcceptNegative = False
        Me.MontantFacture.currencyBox = True
        Me.MontantFacture.firstLetterCapital = False
        Me.MontantFacture.firstLettersCapital = False
        Me.MontantFacture.Location = New System.Drawing.Point(120, 105)
        Me.MontantFacture.manageText = True
        Me.MontantFacture.matchExp = ""
        Me.MontantFacture.maximum = 0
        Me.MontantFacture.minimum = 0
        Me.MontantFacture.Name = "MontantFacture"
        Me.MontantFacture.nbDecimals = CType(2, Short)
        Me.MontantFacture.onlyAlphabet = False
        Me.MontantFacture.refuseAccents = False
        Me.MontantFacture.refusedChars = ""
        Me.MontantFacture.showInternalContextMenu = True
        Me.MontantFacture.Size = New System.Drawing.Size(68, 20)
        Me.MontantFacture.TabIndex = 242
        Me.MontantFacture.Text = "0"
        Me.MontantFacture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MontantFacture.trimText = False
        '
        'addBill
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(712, 245)
        Me.Controls.Add(Me.ApplyTaxes)
        Me.Controls.Add(Me.description)
        Me.Controls.Add(Me.MontantFacture)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TypeFacture)
        Me.Controls.Add(Me.selectPrimeEntity)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Labels)
        Me.Controls.Add(Me.Entite)
        Me.Controls.Add(Me.EntitePrimaire)
        Me.Controls.Add(Me.GroupPayeurs)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.createBill)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "addBill"
        Me.Text = "Nouvelle facture"
        Me.GroupPayeurs.ResumeLayout(False)
        Me.GroupPayeurs.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "addBill Events"
    Private Sub addBill_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Entite.SelectedIndex = 0
        entite_SelectedIndexChanged(eventSender, eventArgs)

        If Not PreferencesManager.getGeneralPreferences()("AutresTypesBills") Is Nothing Then If PreferencesManager.getGeneralPreferences()("AutresTypesBills") <> "" Then TypeFacture.Items.AddRange(PreferencesManager.getGeneralPreferences()("AutresTypesBills").Split(New Char() {vbTab}))
        If TypeFacture.Items.Count > 0 Then TypeFacture.SelectedIndex = 0
    End Sub
#End Region

    Public Enum Modes
        All = 0
        Client = 1
        KP = 2
        User = 3
    End Enum
    Private _Mode As Modes

#Region "Propriétés"
    Public Property mode() As Modes
        Get
            Return _Mode
        End Get
        Set(ByVal value As Modes)
            _Mode = value
            Select Case _Mode
                Case Modes.All
                    Entite.Enabled = True
                    selectPrimeEntity.Enabled = True
                Case Else
                    Entite.Enabled = False
                    selectPrimeEntity.Enabled = False
                    Select Case _Mode
                        Case Modes.Client
                            Entite.SelectedIndex = 0
                        Case Modes.KP
                            Entite.SelectedIndex = 1
                        Case Modes.User
                            Entite.SelectedIndex = 2
                    End Select
                    entite_SelectedIndexChanged(Me, Nothing)
            End Select
        End Set
    End Property
#End Region

#Region "Private"
    Private Sub selectPrimeEntity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectPrimeEntity.Click
        Select Case Entite.SelectedIndex
            Case 0 'Client
                Dim myRecherche As New clientSearch()
                myRecherche.from = EntitePrimaire
                myRecherche.MdiParent = Nothing
                myRecherche.Visible = False
                myRecherche.ShowDialog()
                If EntitePrimaire.Text <> "Aucune" Then
                    PayeurClient.Text = EntitePrimaire.Text.Substring(0, EntitePrimaire.Text.LastIndexOf("("))
                    PayeurClient.Text = PayeurClient.Text.Substring(0, PayeurClient.Text.LastIndexOf("("))
                End If

            Case 1 'KP
                Dim myKeyPeople As New keypeopleSearch()
                myKeyPeople.selected = False
                myKeyPeople.MdiParent = Nothing
                myKeyPeople.Visible = False
                Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
                If kpChosen.noKP <> 0 Then
                    EntitePrimaire.Text = kpChosen.kpFullName & " (" & kpChosen.noKP & ")"
                End If
                If EntitePrimaire.Text <> "Aucune" Then PayeurKP.Text = EntitePrimaire.Text

            Case 2 'User
                selectAUser(EntitePrimaire)
        End Select

        If EntitePrimaire.Text <> "Aucune" Then createBill.Enabled = True
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

    Private Sub menuSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSelect.Click
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
        End Select
    End Sub

    Private Sub menuAucune1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAucune1.Click
        Select Case menuSelectEntity.SourceControl.Name
            Case "selectUser"
                PayeurUser.Text = "Aucun"

            Case "selectKP"
                PayeurKP.Text = "Aucun(e)"

            Case "selectClient"
                PayeurClient.Text = "Aucun"
        End Select
    End Sub


    Private Sub selectAUser(ByVal selectingControl As Control)
        Dim employeeType As Byte = 0
        If selectingControl.Name = "EntitePrimaire" Then employeeType = 2
        Dim myUser As User = UsersManager.getInstance.chooseUser(False, False, employeeType)

        If myUser Is Nothing Then
            If selectingControl.Name = "EntitePrimaire" Then
                EntitePrimaire.Text = "Aucune"
            Else
                Select Case menuSelectEntity.SourceControl.Name
                    Case "selectUser"
                        PayeurUser.Text = "Aucun"

                    Case "selectKP"
                        PayeurKP.Text = "Aucun(e)"

                    Case "selectClient"
                        PayeurClient.Text = "Aucun"
                End Select
            End If
        Else
            selectingControl.Text = myUser.toString
        End If
    End Sub

    Private Sub entite_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Entite.SelectedIndexChanged
        EntitePrimaire.Text = "Aucune"
        createBill.Enabled = False

        PourcentParNoClient.Text = 0
        PourcentParNoKp.Text = 0
        PourcentParNoUser.Text = 0
        PayeurClient.Text = "Aucun"
        PayeurKP.Text = "Aucun(e)"
        PayeurUser.Text = "Aucun"
        Select Case Entite.SelectedIndex
            Case 0
                selectClient.Enabled = False
                selectKP.Enabled = True
                PourcentParNoClient.Text = 100
                GroupPayeurs.Enabled = True
                TypeFacture.Enabled = True
                If TypeFacture.Items.IndexOf("Travailleur autonome") <> -1 Then TypeFacture.Items.Remove("Travailleur autonome")
                If TypeFacture.Items.Count > 0 Then TypeFacture.SelectedIndex = 0
                ApplyTaxes.Checked = True
            Case 1
                selectClient.Enabled = True
                selectKP.Enabled = False
                PourcentParNoKp.Text = 100
                GroupPayeurs.Enabled = True
                TypeFacture.Enabled = True
                If TypeFacture.Items.IndexOf("Travailleur autonome") <> -1 Then TypeFacture.Items.Remove("Travailleur autonome")
                If TypeFacture.Items.Count > 0 Then TypeFacture.SelectedIndex = 0
                ApplyTaxes.Checked = True
            Case 2
                GroupPayeurs.Enabled = False
                TypeFacture.Enabled = False
                TypeFacture.Items.Add("Travailleur autonome")
                TypeFacture.SelectedIndex = TypeFacture.Items.IndexOf("Travailleur autonome")
                ApplyTaxes.Checked = False
        End Select
    End Sub

    Private Sub createBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles createBill.Click
        If Me.MontantFacture.Text = 0 Then
            MessageBox.Show("Le montant facturé doit être supérieur à zéro.", "Impossible d'ajouter la facture")
            Me.MontantFacture.Focus()
            Exit Sub
        End If
        If TypeFacture.Items.Count = 0 Then
            MessageBox.Show("Il n'existe pas d'autres types de facturation. Veuillez en ajouter dans les préférences.", "Impossible d'ajouter la facture")
            Exit Sub
        End If
        Dim sumPayeurs As Double = (Double.Parse(PourcentParNoClient.Text.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) + Double.Parse(PourcentParNoKp.Text.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) + Double.Parse(PourcentParNoUser.Text.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)))
        If GroupPayeurs.Enabled = True And sumPayeurs <> 100 Then
            MessageBox.Show("La somme des pourcentages des payeurs doit être égal à 100 %", "Impossible d'ajouter la facture")
            Exit Sub
        End If
        If PourcentParNoClient.Text <> 0 And PayeurClient.Text = "Aucun" Then
            MessageBox.Show("Si le pourcentage du client n'est pas égal à zéro, vous devez sélectionner un payeur client", "Impossible d'ajouter la facture")
            selectClient.Focus()
            Exit Sub
        End If
        If PourcentParNoKp.Text <> 0 And PayeurKP.Text = "Aucun(e)" Then
            MessageBox.Show("Si le pourcentage de la personne / organisme clé n'est pas égal à zéro, vous devez sélectionner un payeur personne / organisme clé(e)", "Impossible d'ajouter la facture")
            selectKP.Focus()
            Exit Sub
        End If
        If PourcentParNoUser.Text <> 0 And PayeurUser.Text = "Aucun" Then
            MessageBox.Show("Si le pourcentage de l'utilisateur n'est pas égal à zéro, vous devez sélectionner un payeur utilisateur", "Impossible d'ajouter la facture")
            selectUser.Focus()
            Exit Sub
        End If

        Dim regExParsingNo As New System.Text.RegularExpressions.Regex("[^()]+ \((?<No>[0-9]+)\).*", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim noClient As Integer = 0
        Dim noKP As Integer = 0
        Dim noUser As Integer = 0
        If regExParsingNo.Match(PayeurClient.Text).Groups("No").Value <> "" Then noClient = regExParsingNo.Match(PayeurClient.Text).Groups("No").Value
        If regExParsingNo.Match(PayeurKP.Text).Groups("No").Value <> "" Then noKP = regExParsingNo.Match(PayeurKP.Text).Groups("No").Value
        If regExParsingNo.Match(PayeurUser.Text).Groups("No").Value <> "" Then noUser = regExParsingNo.Match(PayeurUser.Text).Groups("No").Value

        Dim montantFacture As Double = Double.Parse(Me.MontantFacture.Text.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))

        Select Case Entite.SelectedIndex
            Case 0
                Dim regExParsing As New System.Text.RegularExpressions.Regex("([^(]+) \((?<NoClient>[0-9]+)\) \((?<NoFolder>[0-9]+)\).*", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                createFacturation(regExParsing.Match(EntitePrimaire.Text).Groups("NoClient").Value, montantFacture, TypeFacture.Text, Date.Today, regExParsing.Match(EntitePrimaire.Text).Groups("NoFolder").Value, , , , , , , , , , noClient, noKP, noUser, PourcentParNoClient.Text, PourcentParNoKp.Text, PourcentParNoUser.Text, , , , , ApplyTaxes.Checked, description.Text)
            Case 1
                createFacturation(0, montantFacture, TypeFacture.Text, Date.Today, , , , , , , , , regExParsingNo.Match(EntitePrimaire.Text).Groups("No").Value, , noClient, noKP, noUser, PourcentParNoClient.Text, PourcentParNoKp.Text, PourcentParNoUser.Text, , , , , ApplyTaxes.Checked, description.Text)
            Case 2
                createFacturation(0, montantFacture, TypeFacture.Text, Date.Today, , , , , , , , , , regExParsingNo.Match(EntitePrimaire.Text).Groups("No").Value, , , , 0, , , , , , currentClinic, ApplyTaxes.Checked, description.Text)
        End Select

        If noClient > 0 Then InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & noClient & ")")
        If noKP > 0 Then InternalUpdatesManager.getInstance.sendUpdate("AccountsBillsKP(" & noKP & ")")
        'REM Missing updates for User & Clinique

        myMainWin.StatusText = "Une facture a été créé pour " & EntitePrimaire.Text
        Me.Close()
    End Sub
#End Region

    Public Sub setClient(ByVal noClient As Integer, ByVal clientName As String, ByVal noFolder As Integer, ByVal siteLesion As String, ByVal codeName As String)
        EntitePrimaire.Text = clientName & " (" & noClient & ") (" & noFolder & ") " & siteLesion & " (" & codeName & ")"
        PayeurClient.Text = clientName & " (" & noClient & ")"
        createBill.Enabled = True
    End Sub

    Public Sub setKP(ByVal noKP As Integer, ByVal kpName As String)
        EntitePrimaire.Text = kpName & " (" & noKP & ")"
        PayeurKP.Text = kpName & " (" & noKP & ")"
        createBill.Enabled = True
    End Sub

    Public Sub setUser(ByVal noUser As Integer, ByVal userName As String)
        EntitePrimaire.Text = userName & " (" & noUser & ")"
        createBill.Enabled = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
