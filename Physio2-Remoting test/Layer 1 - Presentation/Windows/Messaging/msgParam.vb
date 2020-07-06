Friend Class msgParam
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin

        'TODO : msgParam : Remove when implemented
        newImplementedControls.Add(Me.TimeoutInSeconds)
        newImplementedControls.Add(Me.POPSecurized)
        newImplementedControls.Add(Me.SMTPSecurized)

        'Chargement des images
        With DrawingManager.getInstance
            Me.PictureBox2.Image = DrawingManager.iconToImage(.getIcon("securised16.ico"), New Size(16, 16))
            Me.PictureBox1.Image = DrawingManager.iconToImage(.getIcon("securised16.ico"), New Size(16, 16))
            Me.ajout.Image = .getImage("ajouter16.gif")
            Me.modif.Image = .getImage("save.jpg")
            Me.enlever.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.renommer.Image = DrawingManager.iconToImage(.getIcon("rename16.ico"), New Size(16, 16))
            Me.Icon = DrawingManager.imageToIcon(.getImage("mailaccount.gif"))
        End With
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
    Friend WithEvents ajout As System.Windows.Forms.Button
    Friend WithEvents modif As System.Windows.Forms.Button
    Friend WithEvents enlever As System.Windows.Forms.Button
    Friend WithEvents POP As ManagedText
    Friend WithEvents SMTP As ManagedText
    Friend WithEvents renommer As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Parametres As System.Windows.Forms.GroupBox
    Friend WithEvents comptes As Clinica.ManagedCombo
    Friend WithEvents Nom As ManagedText
    Friend WithEvents courriel As ManagedText
    Friend WithEvents POPPort As ManagedText
    Friend WithEvents SMTPPort As ManagedText
    Friend WithEvents POPSecurized As System.Windows.Forms.CheckBox
    Friend WithEvents SMTPSecurized As System.Windows.Forms.CheckBox
    Friend WithEvents UserName As ManagedText
    Friend WithEvents Password As ManagedText
    Friend WithEvents RetainMDP As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeAccount As System.Windows.Forms.CheckBox
    Friend WithEvents CommonAccount As System.Windows.Forms.CheckBox
    Friend WithEvents LeaveMSGOnServer As System.Windows.Forms.CheckBox
    Friend WithEvents SMTPNeedAuthentication As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SMTPAuthFrame As System.Windows.Forms.GroupBox
    Friend WithEvents SMTPSpecificCredential As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SMTPAuthenUsername As ManagedText
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents SMTPPassword As ManagedText
    Friend WithEvents SMTPSavePassword As System.Windows.Forms.CheckBox
    Friend WithEvents TimeoutInSeconds As System.Windows.Forms.HScrollBar
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents showPassword As System.Windows.Forms.CheckBox
    Friend WithEvents CanSendEmail As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents showPassword2 As System.Windows.Forms.CheckBox
    Friend WithEvents TransferToFolder As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Parametres = New System.Windows.Forms.GroupBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TimeoutInSeconds = New System.Windows.Forms.HScrollBar
        Me.Label4 = New System.Windows.Forms.Label
        Me.CanSendEmail = New System.Windows.Forms.CheckBox
        Me.Nom = New ManagedText
        Me.courriel = New ManagedText
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.CommonAccount = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.IncludeAccount = New System.Windows.Forms.CheckBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Label2 = New System.Windows.Forms.Label
        Me.TransferToFolder = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.POP = New ManagedText
        Me.UserName = New ManagedText
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.POPPort = New ManagedText
        Me.Password = New ManagedText
        Me.showPassword = New System.Windows.Forms.CheckBox
        Me.RetainMDP = New System.Windows.Forms.CheckBox
        Me.POPSecurized = New System.Windows.Forms.CheckBox
        Me.LeaveMSGOnServer = New System.Windows.Forms.CheckBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.SMTPAuthFrame = New System.Windows.Forms.GroupBox
        Me.SMTPSpecificCredential = New System.Windows.Forms.CheckBox
        Me.showPassword2 = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SMTPAuthenUsername = New ManagedText
        Me.Label6 = New System.Windows.Forms.Label
        Me.SMTPPassword = New ManagedText
        Me.SMTPSavePassword = New System.Windows.Forms.CheckBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.SMTPNeedAuthentication = New System.Windows.Forms.CheckBox
        Me.SMTP = New ManagedText
        Me.SMTPPort = New ManagedText
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.SMTPSecurized = New System.Windows.Forms.CheckBox
        Me.comptes = New ManagedCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.ajout = New System.Windows.Forms.Button
        Me.modif = New System.Windows.Forms.Button
        Me.enlever = New System.Windows.Forms.Button
        Me.renommer = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Parametres.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SMTPAuthFrame.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Parametres
        '
        Me.Parametres.Controls.Add(Me.TabControl1)
        Me.Parametres.Location = New System.Drawing.Point(8, 56)
        Me.Parametres.Name = "Parametres"
        Me.Parametres.Size = New System.Drawing.Size(375, 244)
        Me.Parametres.TabIndex = 0
        Me.Parametres.TabStop = False
        Me.Parametres.Text = "Paramètres de ce compte"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(3, 16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(369, 225)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TimeoutInSeconds)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.CanSendEmail)
        Me.TabPage1.Controls.Add(Me.Nom)
        Me.TabPage1.Controls.Add(Me.courriel)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.CommonAccount)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.IncludeAccount)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(361, 199)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Infos de base"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TimeoutInSeconds
        '
        Me.TimeoutInSeconds.Enabled = False
        Me.TimeoutInSeconds.LargeChange = 15
        Me.TimeoutInSeconds.Location = New System.Drawing.Point(159, 59)
        Me.TimeoutInSeconds.Maximum = 120
        Me.TimeoutInSeconds.Minimum = 15
        Me.TimeoutInSeconds.Name = "TimeoutInSeconds"
        Me.TimeoutInSeconds.Size = New System.Drawing.Size(126, 17)
        Me.TimeoutInSeconds.SmallChange = 15
        Me.TimeoutInSeconds.TabIndex = 30
        Me.TimeoutInSeconds.Value = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Nom lors de l'envoi :"
        '
        'CanSendEmail
        '
        Me.CanSendEmail.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CanSendEmail.Checked = True
        Me.CanSendEmail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CanSendEmail.Location = New System.Drawing.Point(3, 112)
        Me.CanSendEmail.Name = "CanSendEmail"
        Me.CanSendEmail.Size = New System.Drawing.Size(282, 16)
        Me.CanSendEmail.TabIndex = 13
        Me.CanSendEmail.Text = "Inclure ce compte lors de l'envoi d'un message"
        '
        'Nom
        '
        Me.Nom.acceptAlpha = True
        Me.Nom.acceptedChars = ""
        Me.Nom.acceptNumeric = True
        Me.Nom.allCapital = False
        Me.Nom.allLower = False
        Me.Nom.blockOnMaximum = False
        Me.Nom.blockOnMinimum = False
        Me.Nom.cb_AcceptLeftZeros = False
        Me.Nom.cb_AcceptNegative = False
        Me.Nom.currencyBox = False
        Me.Nom.firstLetterCapital = True
        Me.Nom.firstLettersCapital = True
        Me.Nom.Location = New System.Drawing.Point(123, 7)
        Me.Nom.manageText = True
        Me.Nom.matchExp = ""
        Me.Nom.maximum = 0
        Me.Nom.minimum = 0
        Me.Nom.Name = "Nom"
        Me.Nom.nbDecimals = CType(-1, Short)
        Me.Nom.onlyAlphabet = False
        Me.Nom.refuseAccents = False
        Me.Nom.refusedChars = ""
        Me.Nom.showInternalContextMenu = True
        Me.Nom.Size = New System.Drawing.Size(224, 20)
        Me.Nom.TabIndex = 1
        Me.Nom.trimText = False
        '
        'courriel
        '
        Me.courriel.acceptAlpha = True
        Me.courriel.acceptedChars = ".§@§-§_"
        Me.courriel.acceptNumeric = True
        Me.courriel.allCapital = False
        Me.courriel.allLower = True
        Me.courriel.blockOnMaximum = False
        Me.courriel.blockOnMinimum = False
        Me.courriel.cb_AcceptLeftZeros = False
        Me.courriel.cb_AcceptNegative = False
        Me.courriel.currencyBox = False
        Me.courriel.firstLetterCapital = False
        Me.courriel.firstLettersCapital = False
        Me.courriel.Location = New System.Drawing.Point(123, 31)
        Me.courriel.manageText = True
        Me.courriel.matchExp = ""
        Me.courriel.maximum = 0
        Me.courriel.minimum = 0
        Me.courriel.Name = "courriel"
        Me.courriel.nbDecimals = CType(-1, Short)
        Me.courriel.onlyAlphabet = True
        Me.courriel.refuseAccents = True
        Me.courriel.refusedChars = ""
        Me.courriel.showInternalContextMenu = True
        Me.courriel.Size = New System.Drawing.Size(224, 20)
        Me.courriel.TabIndex = 2
        Me.courriel.trimText = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Adresse de courriel :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(285, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Long (2min)"
        '
        'CommonAccount
        '
        Me.CommonAccount.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CommonAccount.Location = New System.Drawing.Point(3, 134)
        Me.CommonAccount.Name = "CommonAccount"
        Me.CommonAccount.Size = New System.Drawing.Size(282, 16)
        Me.CommonAccount.TabIndex = 14
        Me.CommonAccount.Text = "Compte commun"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(156, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Délai d'expiration :    Court (15s)"
        '
        'IncludeAccount
        '
        Me.IncludeAccount.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IncludeAccount.Checked = True
        Me.IncludeAccount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IncludeAccount.Location = New System.Drawing.Point(3, 88)
        Me.IncludeAccount.Name = "IncludeAccount"
        Me.IncludeAccount.Size = New System.Drawing.Size(282, 16)
        Me.IncludeAccount.TabIndex = 13
        Me.IncludeAccount.Text = "Inclure ce compte lors de la réception globale"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.TransferToFolder)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Controls.Add(Me.POP)
        Me.TabPage3.Controls.Add(Me.UserName)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.Label10)
        Me.TabPage3.Controls.Add(Me.PictureBox1)
        Me.TabPage3.Controls.Add(Me.POPPort)
        Me.TabPage3.Controls.Add(Me.Password)
        Me.TabPage3.Controls.Add(Me.showPassword)
        Me.TabPage3.Controls.Add(Me.RetainMDP)
        Me.TabPage3.Controls.Add(Me.POPSecurized)
        Me.TabPage3.Controls.Add(Me.LeaveMSGOnServer)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(361, 195)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Serveur entrant"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Serveur / Port :"
        '
        'TransferToFolder
        '
        Me.TransferToFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TransferToFolder.Location = New System.Drawing.Point(6, 118)
        Me.TransferToFolder.Name = "TransferToFolder"
        Me.TransferToFolder.Size = New System.Drawing.Size(341, 21)
        Me.TransferToFolder.Sorted = True
        Me.TransferToFolder.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 36)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 13)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Nom d'usager :"
        '
        'POP
        '
        Me.POP.acceptAlpha = True
        Me.POP.acceptedChars = ".§-§_"
        Me.POP.acceptNumeric = True
        Me.POP.allCapital = False
        Me.POP.allLower = True
        Me.POP.blockOnMaximum = False
        Me.POP.blockOnMinimum = False
        Me.POP.cb_AcceptLeftZeros = False
        Me.POP.cb_AcceptNegative = False
        Me.POP.currencyBox = False
        Me.POP.firstLetterCapital = False
        Me.POP.firstLettersCapital = False
        Me.POP.Location = New System.Drawing.Point(93, 7)
        Me.POP.manageText = True
        Me.POP.matchExp = ""
        Me.POP.maximum = 0
        Me.POP.minimum = 0
        Me.POP.Name = "POP"
        Me.POP.nbDecimals = CType(-1, Short)
        Me.POP.onlyAlphabet = True
        Me.POP.refuseAccents = True
        Me.POP.refusedChars = ""
        Me.POP.showInternalContextMenu = True
        Me.POP.Size = New System.Drawing.Size(168, 20)
        Me.POP.TabIndex = 3
        Me.POP.trimText = False
        '
        'UserName
        '
        Me.UserName.acceptAlpha = True
        Me.UserName.acceptedChars = ""
        Me.UserName.acceptNumeric = True
        Me.UserName.allCapital = False
        Me.UserName.allLower = False
        Me.UserName.blockOnMaximum = False
        Me.UserName.blockOnMinimum = False
        Me.UserName.cb_AcceptLeftZeros = False
        Me.UserName.cb_AcceptNegative = False
        Me.UserName.currencyBox = False
        Me.UserName.firstLetterCapital = False
        Me.UserName.firstLettersCapital = False
        Me.UserName.Location = New System.Drawing.Point(93, 33)
        Me.UserName.manageText = True
        Me.UserName.matchExp = ""
        Me.UserName.maximum = 0
        Me.UserName.minimum = 0
        Me.UserName.Name = "UserName"
        Me.UserName.nbDecimals = CType(-1, Short)
        Me.UserName.onlyAlphabet = False
        Me.UserName.refuseAccents = False
        Me.UserName.refusedChars = ""
        Me.UserName.showInternalContextMenu = True
        Me.UserName.Size = New System.Drawing.Size(256, 20)
        Me.UserName.TabIndex = 9
        Me.UserName.trimText = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 102)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(209, 13)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Télécharger les messages dans le dossier :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 60)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Mot de passe :"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(325, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 18
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Effectuer une connexion sécurisée SSL")
        '
        'POPPort
        '
        Me.POPPort.acceptAlpha = False
        Me.POPPort.acceptedChars = ""
        Me.POPPort.acceptNumeric = True
        Me.POPPort.allCapital = False
        Me.POPPort.allLower = True
        Me.POPPort.blockOnMaximum = False
        Me.POPPort.blockOnMinimum = False
        Me.POPPort.cb_AcceptLeftZeros = False
        Me.POPPort.cb_AcceptNegative = False
        Me.POPPort.currencyBox = False
        Me.POPPort.firstLetterCapital = False
        Me.POPPort.firstLettersCapital = False
        Me.POPPort.Location = New System.Drawing.Point(261, 7)
        Me.POPPort.manageText = True
        Me.POPPort.matchExp = ""
        Me.POPPort.maximum = 0
        Me.POPPort.minimum = 0
        Me.POPPort.Name = "POPPort"
        Me.POPPort.nbDecimals = CType(-1, Short)
        Me.POPPort.onlyAlphabet = True
        Me.POPPort.refuseAccents = True
        Me.POPPort.refusedChars = ""
        Me.POPPort.showInternalContextMenu = True
        Me.POPPort.Size = New System.Drawing.Size(32, 20)
        Me.POPPort.TabIndex = 4
        Me.POPPort.Text = "110"
        Me.ToolTip1.SetToolTip(Me.POPPort, "Port par défaut : 110 (Non sécurisé) 995 (Sécurisé)")
        Me.POPPort.trimText = False
        '
        'Password
        '
        Me.Password.acceptAlpha = True
        Me.Password.acceptedChars = ""
        Me.Password.acceptNumeric = True
        Me.Password.allCapital = False
        Me.Password.allLower = False
        Me.Password.blockOnMaximum = False
        Me.Password.blockOnMinimum = False
        Me.Password.cb_AcceptLeftZeros = False
        Me.Password.cb_AcceptNegative = False
        Me.Password.currencyBox = False
        Me.Password.firstLetterCapital = False
        Me.Password.firstLettersCapital = False
        Me.Password.Location = New System.Drawing.Point(93, 57)
        Me.Password.manageText = True
        Me.Password.matchExp = ""
        Me.Password.maximum = 0
        Me.Password.minimum = 0
        Me.Password.Name = "Password"
        Me.Password.nbDecimals = CType(-1, Short)
        Me.Password.onlyAlphabet = False
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Password.refuseAccents = False
        Me.Password.refusedChars = ""
        Me.Password.showInternalContextMenu = True
        Me.Password.Size = New System.Drawing.Size(256, 20)
        Me.Password.TabIndex = 10
        Me.Password.trimText = False
        '
        'showPassword
        '
        Me.showPassword.Location = New System.Drawing.Point(285, 83)
        Me.showPassword.Name = "showPassword"
        Me.showPassword.Size = New System.Drawing.Size(66, 16)
        Me.showPassword.TabIndex = 11
        Me.showPassword.Text = "Afficher"
        '
        'RetainMDP
        '
        Me.RetainMDP.Checked = True
        Me.RetainMDP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RetainMDP.Location = New System.Drawing.Point(93, 83)
        Me.RetainMDP.Name = "RetainMDP"
        Me.RetainMDP.Size = New System.Drawing.Size(160, 16)
        Me.RetainMDP.TabIndex = 11
        Me.RetainMDP.Text = "Mémoriser le mot de passe"
        '
        'POPSecurized
        '
        Me.POPSecurized.Enabled = False
        Me.POPSecurized.Location = New System.Drawing.Point(309, 7)
        Me.POPSecurized.Name = "POPSecurized"
        Me.POPSecurized.Size = New System.Drawing.Size(16, 16)
        Me.POPSecurized.TabIndex = 5
        '
        'LeaveMSGOnServer
        '
        Me.LeaveMSGOnServer.BackColor = System.Drawing.Color.Transparent
        Me.LeaveMSGOnServer.Location = New System.Drawing.Point(6, 145)
        Me.LeaveMSGOnServer.Name = "LeaveMSGOnServer"
        Me.LeaveMSGOnServer.Size = New System.Drawing.Size(208, 16)
        Me.LeaveMSGOnServer.TabIndex = 12
        Me.LeaveMSGOnServer.Text = "Laisser les messages sur le serveur"
        Me.LeaveMSGOnServer.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SMTPAuthFrame)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.SMTPNeedAuthentication)
        Me.TabPage2.Controls.Add(Me.SMTP)
        Me.TabPage2.Controls.Add(Me.SMTPPort)
        Me.TabPage2.Controls.Add(Me.PictureBox2)
        Me.TabPage2.Controls.Add(Me.SMTPSecurized)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(361, 195)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Serveur sortant"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SMTPAuthFrame
        '
        Me.SMTPAuthFrame.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SMTPAuthFrame.Controls.Add(Me.SMTPSpecificCredential)
        Me.SMTPAuthFrame.Controls.Add(Me.showPassword2)
        Me.SMTPAuthFrame.Controls.Add(Me.Label3)
        Me.SMTPAuthFrame.Controls.Add(Me.SMTPAuthenUsername)
        Me.SMTPAuthFrame.Controls.Add(Me.Label6)
        Me.SMTPAuthFrame.Controls.Add(Me.SMTPPassword)
        Me.SMTPAuthFrame.Controls.Add(Me.SMTPSavePassword)
        Me.SMTPAuthFrame.Location = New System.Drawing.Point(3, 54)
        Me.SMTPAuthFrame.Name = "SMTPAuthFrame"
        Me.SMTPAuthFrame.Size = New System.Drawing.Size(354, 90)
        Me.SMTPAuthFrame.TabIndex = 20
        Me.SMTPAuthFrame.TabStop = False
        '
        'SMTPSpecificCredential
        '
        Me.SMTPSpecificCredential.BackColor = System.Drawing.SystemColors.Control
        Me.SMTPSpecificCredential.Enabled = False
        Me.SMTPSpecificCredential.Location = New System.Drawing.Point(6, -1)
        Me.SMTPSpecificCredential.Name = "SMTPSpecificCredential"
        Me.SMTPSpecificCredential.Size = New System.Drawing.Size(235, 16)
        Me.SMTPSpecificCredential.TabIndex = 11
        Me.SMTPSpecificCredential.Text = "Authentification différente de la réception"
        Me.SMTPSpecificCredential.UseVisualStyleBackColor = False
        '
        'showPassword2
        '
        Me.showPassword2.Location = New System.Drawing.Point(282, 71)
        Me.showPassword2.Name = "showPassword2"
        Me.showPassword2.Size = New System.Drawing.Size(66, 16)
        Me.showPassword2.TabIndex = 11
        Me.showPassword2.Text = "Afficher"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Nom d'usager :"
        '
        'SMTPAuthenUsername
        '
        Me.SMTPAuthenUsername.acceptAlpha = True
        Me.SMTPAuthenUsername.acceptedChars = ""
        Me.SMTPAuthenUsername.acceptNumeric = True
        Me.SMTPAuthenUsername.allCapital = False
        Me.SMTPAuthenUsername.allLower = False
        Me.SMTPAuthenUsername.blockOnMaximum = False
        Me.SMTPAuthenUsername.blockOnMinimum = False
        Me.SMTPAuthenUsername.cb_AcceptLeftZeros = False
        Me.SMTPAuthenUsername.cb_AcceptNegative = False
        Me.SMTPAuthenUsername.currencyBox = False
        Me.SMTPAuthenUsername.Enabled = False
        Me.SMTPAuthenUsername.firstLetterCapital = False
        Me.SMTPAuthenUsername.firstLettersCapital = False
        Me.SMTPAuthenUsername.Location = New System.Drawing.Point(93, 22)
        Me.SMTPAuthenUsername.manageText = True
        Me.SMTPAuthenUsername.matchExp = ""
        Me.SMTPAuthenUsername.maximum = 0
        Me.SMTPAuthenUsername.minimum = 0
        Me.SMTPAuthenUsername.Name = "SMTPAuthenUsername"
        Me.SMTPAuthenUsername.nbDecimals = CType(-1, Short)
        Me.SMTPAuthenUsername.onlyAlphabet = False
        Me.SMTPAuthenUsername.refuseAccents = False
        Me.SMTPAuthenUsername.refusedChars = ""
        Me.SMTPAuthenUsername.showInternalContextMenu = True
        Me.SMTPAuthenUsername.Size = New System.Drawing.Size(256, 20)
        Me.SMTPAuthenUsername.TabIndex = 9
        Me.SMTPAuthenUsername.trimText = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Mot de passe :"
        '
        'SMTPPassword
        '
        Me.SMTPPassword.acceptAlpha = True
        Me.SMTPPassword.acceptedChars = ""
        Me.SMTPPassword.acceptNumeric = True
        Me.SMTPPassword.allCapital = False
        Me.SMTPPassword.allLower = False
        Me.SMTPPassword.blockOnMaximum = False
        Me.SMTPPassword.blockOnMinimum = False
        Me.SMTPPassword.cb_AcceptLeftZeros = False
        Me.SMTPPassword.cb_AcceptNegative = False
        Me.SMTPPassword.currencyBox = False
        Me.SMTPPassword.Enabled = False
        Me.SMTPPassword.firstLetterCapital = False
        Me.SMTPPassword.firstLettersCapital = False
        Me.SMTPPassword.Location = New System.Drawing.Point(93, 46)
        Me.SMTPPassword.manageText = True
        Me.SMTPPassword.matchExp = ""
        Me.SMTPPassword.maximum = 0
        Me.SMTPPassword.minimum = 0
        Me.SMTPPassword.Name = "SMTPPassword"
        Me.SMTPPassword.nbDecimals = CType(-1, Short)
        Me.SMTPPassword.onlyAlphabet = False
        Me.SMTPPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.SMTPPassword.refuseAccents = False
        Me.SMTPPassword.refusedChars = ""
        Me.SMTPPassword.showInternalContextMenu = True
        Me.SMTPPassword.Size = New System.Drawing.Size(256, 20)
        Me.SMTPPassword.TabIndex = 10
        Me.SMTPPassword.trimText = False
        '
        'SMTPSavePassword
        '
        Me.SMTPSavePassword.Checked = True
        Me.SMTPSavePassword.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SMTPSavePassword.Enabled = False
        Me.SMTPSavePassword.Location = New System.Drawing.Point(95, 71)
        Me.SMTPSavePassword.Name = "SMTPSavePassword"
        Me.SMTPSavePassword.Size = New System.Drawing.Size(160, 16)
        Me.SMTPSavePassword.TabIndex = 11
        Me.SMTPSavePassword.Text = "Mémoriser le mot de passe"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Serveur / Port :"
        '
        'SMTPNeedAuthentication
        '
        Me.SMTPNeedAuthentication.Location = New System.Drawing.Point(9, 31)
        Me.SMTPNeedAuthentication.Name = "SMTPNeedAuthentication"
        Me.SMTPNeedAuthentication.Size = New System.Drawing.Size(176, 16)
        Me.SMTPNeedAuthentication.TabIndex = 11
        Me.SMTPNeedAuthentication.Text = "Nécessite une authentification"
        '
        'SMTP
        '
        Me.SMTP.acceptAlpha = True
        Me.SMTP.acceptedChars = ".§-§_"
        Me.SMTP.acceptNumeric = True
        Me.SMTP.allCapital = False
        Me.SMTP.allLower = True
        Me.SMTP.blockOnMaximum = False
        Me.SMTP.blockOnMinimum = False
        Me.SMTP.cb_AcceptLeftZeros = False
        Me.SMTP.cb_AcceptNegative = False
        Me.SMTP.currencyBox = False
        Me.SMTP.firstLetterCapital = False
        Me.SMTP.firstLettersCapital = False
        Me.SMTP.Location = New System.Drawing.Point(96, 6)
        Me.SMTP.manageText = True
        Me.SMTP.matchExp = ""
        Me.SMTP.maximum = 0
        Me.SMTP.minimum = 0
        Me.SMTP.Name = "SMTP"
        Me.SMTP.nbDecimals = CType(-1, Short)
        Me.SMTP.onlyAlphabet = True
        Me.SMTP.refuseAccents = True
        Me.SMTP.refusedChars = ""
        Me.SMTP.showInternalContextMenu = True
        Me.SMTP.Size = New System.Drawing.Size(168, 20)
        Me.SMTP.TabIndex = 6
        Me.SMTP.trimText = False
        '
        'SMTPPort
        '
        Me.SMTPPort.acceptAlpha = False
        Me.SMTPPort.acceptedChars = ""
        Me.SMTPPort.acceptNumeric = True
        Me.SMTPPort.allCapital = False
        Me.SMTPPort.allLower = True
        Me.SMTPPort.blockOnMaximum = False
        Me.SMTPPort.blockOnMinimum = False
        Me.SMTPPort.cb_AcceptLeftZeros = False
        Me.SMTPPort.cb_AcceptNegative = False
        Me.SMTPPort.currencyBox = False
        Me.SMTPPort.firstLetterCapital = False
        Me.SMTPPort.firstLettersCapital = False
        Me.SMTPPort.Location = New System.Drawing.Point(264, 6)
        Me.SMTPPort.manageText = True
        Me.SMTPPort.matchExp = ""
        Me.SMTPPort.maximum = 0
        Me.SMTPPort.minimum = 0
        Me.SMTPPort.Name = "SMTPPort"
        Me.SMTPPort.nbDecimals = CType(-1, Short)
        Me.SMTPPort.onlyAlphabet = True
        Me.SMTPPort.refuseAccents = True
        Me.SMTPPort.refusedChars = ""
        Me.SMTPPort.showInternalContextMenu = True
        Me.SMTPPort.Size = New System.Drawing.Size(32, 20)
        Me.SMTPPort.TabIndex = 7
        Me.SMTPPort.Text = "25"
        Me.ToolTip1.SetToolTip(Me.SMTPPort, "Port par défaut : 25")
        Me.SMTPPort.trimText = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(328, 8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 19
        Me.PictureBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox2, "Effectuer une connexion sécurisée SSL")
        '
        'SMTPSecurized
        '
        Me.SMTPSecurized.Enabled = False
        Me.SMTPSecurized.Location = New System.Drawing.Point(312, 8)
        Me.SMTPSecurized.Name = "SMTPSecurized"
        Me.SMTPSecurized.Size = New System.Drawing.Size(16, 16)
        Me.SMTPSecurized.TabIndex = 8
        '
        'comptes
        '
        Me.comptes.acceptAlpha = True
        Me.comptes.acceptedChars = Nothing
        Me.comptes.acceptNumeric = True
        Me.comptes.allCapital = False
        Me.comptes.allLower = False
        Me.comptes.autoComplete = True
        Me.comptes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.comptes.autoSizeDropDown = True
        Me.comptes.BackColor = System.Drawing.Color.White
        Me.comptes.blockOnMaximum = False
        Me.comptes.blockOnMinimum = False
        Me.comptes.cb_AcceptLeftZeros = False
        Me.comptes.cb_AcceptNegative = False
        Me.comptes.currencyBox = False
        Me.comptes.dbField = Nothing
        Me.comptes.doComboDelete = True
        Me.comptes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comptes.firstLetterCapital = False
        Me.comptes.firstLettersCapital = False
        Me.comptes.itemsToolTipDuration = 10000
        Me.comptes.Location = New System.Drawing.Point(8, 24)
        Me.comptes.manageText = True
        Me.comptes.matchExp = Nothing
        Me.comptes.maximum = 0
        Me.comptes.minimum = 0
        Me.comptes.Name = "comptes"
        Me.comptes.nbDecimals = CType(-1, Short)
        Me.comptes.onlyAlphabet = False
        Me.comptes.pathOfList = Nothing
        Me.comptes.ReadOnly = False
        Me.comptes.refuseAccents = False
        Me.comptes.refusedChars = Nothing
        Me.comptes.showItemsToolTip = False
        Me.comptes.Size = New System.Drawing.Size(270, 21)
        Me.comptes.TabIndex = 0
        Me.comptes.trimText = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Comptes existants :"
        '
        'ajout
        '
        Me.ajout.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ajout.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ajout.Location = New System.Drawing.Point(284, 24)
        Me.ajout.Name = "ajout"
        Me.ajout.Size = New System.Drawing.Size(24, 24)
        Me.ajout.TabIndex = 3
        Me.ajout.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ajout, "Ajout")
        '
        'modif
        '
        Me.modif.Enabled = False
        Me.modif.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modif.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modif.Location = New System.Drawing.Point(308, 24)
        Me.modif.Name = "modif"
        Me.modif.Size = New System.Drawing.Size(24, 24)
        Me.modif.TabIndex = 4
        Me.modif.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modif, "Modifier")
        '
        'enlever
        '
        Me.enlever.Enabled = False
        Me.enlever.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.enlever.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.enlever.Location = New System.Drawing.Point(356, 24)
        Me.enlever.Name = "enlever"
        Me.enlever.Size = New System.Drawing.Size(24, 24)
        Me.enlever.TabIndex = 5
        Me.enlever.TabStop = False
        Me.ToolTip1.SetToolTip(Me.enlever, "Enlever")
        '
        'renommer
        '
        Me.renommer.Enabled = False
        Me.renommer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.renommer.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.renommer.Location = New System.Drawing.Point(332, 24)
        Me.renommer.Name = "renommer"
        Me.renommer.Size = New System.Drawing.Size(24, 24)
        Me.renommer.TabIndex = 6
        Me.renommer.TabStop = False
        Me.ToolTip1.SetToolTip(Me.renommer, "Renommer")
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'msgParam
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(392, 307)
        Me.Controls.Add(Me.comptes)
        Me.Controls.Add(Me.renommer)
        Me.Controls.Add(Me.enlever)
        Me.Controls.Add(Me.modif)
        Me.Controls.Add(Me.ajout)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Parametres)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "msgParam"
        Me.ShowInTaskbar = False
        Me.Text = "Paramètres des comptes de courriel"
        Me.Parametres.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.SMTPAuthFrame.ResumeLayout(False)
        Me.SMTPAuthFrame.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private loadingData() As String
    Private formModified As Boolean = False
    Private oldCompte As MailAccount

    Private newImplementedControls As New Generic.List(Of Control)

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal ajoutNeverLocked As Boolean = False)
        For Each curTab As TabPage In TabControl1.TabPages
            For Each curControl As Object In curTab.Controls
                If newImplementedControls.Contains(curControl) Then Continue For

                If ObjectHelper.hasProperty(curControl, "Enabled") Then
                    CType(curControl, Control).Enabled = Not trueFalse
                ElseIf ObjectHelper.hasProperty(curControl, "ReadOnly") Then
                    curControl.ReadOnly = trueFalse
                End If
            Next
        Next
        ajout.Enabled = ajoutNeverLocked OrElse Not trueFalse
        modif.Enabled = Not trueFalse
        enlever.Enabled = Not trueFalse
        renommer.Enabled = Not trueFalse
    End Sub

    Private Sub msgParam_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TransferToFolder.Items.Add("* Boîte de réception *")
        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(True)
        TransferToFolder.Items.AddRange(users.ToArray)

        loading()

        If lockSecteur("msgParam.modif", True, "Paramètres des comptes courriels") = False Then lockItems(True, False)
    End Sub

    Private Sub popSecurized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POPSecurized.CheckedChanged
        If POPSecurized.Checked = True Then
            POPPort.Text = MailAccount.DEFAULT_SECURED_INCOMING_SERVER_PORT
        Else
            POPPort.Text = MailAccount.DEFAULT_INCOMING_SERVER_PORT
        End If
    End Sub

    Private Sub comptes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comptes.SelectedIndexChanged
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then saving()

        oldCompte = Nothing
        If comptes.SelectedIndex <> -1 Then
            'Sélection d'un compte courriel
            If ajout.Enabled = True Then lockItems(False, True)

            oldCompte = comptes.SelectedItem
            Nom.Text = oldCompte.sendingName
            courriel.Text = oldCompte.email
            POP.Text = oldCompte.popServer.server
            POPSecurized.Checked = oldCompte.popServer.isSecured
            POPPort.Text = oldCompte.popServer.port
            SMTP.Text = oldCompte.smtpServer.server
            SMTPSecurized.Checked = oldCompte.smtpServer.isSecured
            SMTPPort.Text = oldCompte.smtpServer.port
            UserName.Text = oldCompte.username
            If oldCompte.passwordKey <> "" Then
                Password.Text = decrypt(oldCompte.password, oldCompte.passwordKey)
            Else
                Password.Text = ""
            End If
            RetainMDP.Checked = oldCompte.savePassword
            LeaveMSGOnServer.Checked = oldCompte.keepMSGOnServer
            IncludeAccount.Checked = oldCompte.includeInGeneralReception
            CanSendEmail.Checked = oldCompte.canSendEmail
            CommonAccount.Checked = oldCompte.commonAccount
            TransferToFolder.SelectedIndex = TransferToFolder.FindStringExact(oldCompte.inboxFolderName)
            If TransferToFolder.SelectedIndex < 0 Then TransferToFolder.SelectedIndex = 0

            TimeoutInSeconds.Value = oldCompte.timeoutInSeconds
            SMTPAuthenUsername.Text = oldCompte.smtpAuthenUsername
            SMTPNeedAuthentication.Checked = oldCompte.smtpNeedAuthentication
            If oldCompte.smtpPasswordKey <> "" Then
                SMTPPassword.Text = decrypt(oldCompte.smtpPassword, oldCompte.smtpPasswordKey)
            Else
                SMTPPassword.Text = ""
            End If
            SMTPSavePassword.Checked = oldCompte.smtpSavePassword
            SMTPSpecificCredential.Checked = oldCompte.smtpSpecificCredential

            formModified = False
        Else
            'La sélection est nulle (Désactivation des objets)
            lockItems(True, ajout.Enabled)

            resetFields()
        End If
    End Sub

    Private Sub resetFields()
        Nom.Text = ""
        courriel.Text = ""
        POP.Text = ""
        POPPort.Text = MailAccount.DEFAULT_INCOMING_SERVER_PORT
        POPSecurized.Checked = False
        SMTP.Text = ""
        SMTPPort.Text = MailAccount.DEFAULT_OUTGOING_SERVER_PORT
        SMTPSecurized.Checked = False
        UserName.Text = ""
        Password.Text = ""
        RetainMDP.Checked = True
        LeaveMSGOnServer.Checked = False
        IncludeAccount.Checked = True
        CommonAccount.Checked = False
        TransferToFolder.SelectedIndex = 0
    End Sub

    Private Sub msgParam_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If ajout.Enabled = True Then
            lockSecteur("msgParam.modif", False)
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then saving()
        End If
    End Sub

    Private Sub msgParam_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If ajout.Enabled = True Then
            lockSecteur("msgParam.modif", False)
            If formModified = True Then saving()
        End If
    End Sub

    Private Sub ajout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ajout.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        Dim newAccountName As String = myInputBoxPlus.Prompt("Veuillez entrer le nom pour ce nouveau compte", "Nom du compte")
        If newAccountName = "" Then Exit Sub

        Dim returnMsg As String = MailsManager.getInstance.addMailAccount(newAccountName)

        If returnMsg <> "" Then
            MessageBox.Show(returnMsg, "Impossible d'ajouter le compte")
            Exit Sub
        End If

        Dim newCompte As MailAccount = MailsManager.getInstance.getMailAccount(newAccountName)
        Me.comptes.Items.Add(newCompte)
        Me.comptes.SelectedItem = newCompte

        myMainWin.StatusText = "Ajout d'un compte de courriel"
    End Sub

    Public Sub loading()
        Dim si As Integer = comptes.SelectedIndex

        resetFields()
        comptes.Items.Clear()

        comptes.Items.AddRange(MailsManager.getInstance.getMailAccounts.ToArray)

        formModified = False

        If (comptes.Items.Count - 1) >= si Then comptes.SelectedIndex = si
        If comptes.SelectedIndex < 0 And comptes.Items.Count > 0 Then comptes.SelectedIndex = 0

        If comptes.Items.Count = 0 Then lockItems(True, True)

        formModified = False
    End Sub

    Private Sub modif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modif.Click
        saving()
    End Sub

    Private Sub saving()
        Dim myMDP As String = ""
        Dim mySMTPMDP As String = ""

        If oldCompte.passwordKey = "" Then oldCompte.passwordKey = Chaines.getPasswordKey
        If oldCompte.smtpPasswordKey = "" Then oldCompte.smtpPasswordKey = Chaines.getPasswordKey

        If RetainMDP.Checked = False Then
            Password.Text = ""
        Else
            myMDP = encrypt(Password.Text, oldCompte.passwordKey)
        End If

        If SMTPSavePassword.Checked = False Then
            SMTPPassword.Text = ""
        Else
            mySMTPMDP = encrypt(SMTPPassword.Text, oldCompte.smtpPasswordKey)
        End If

        oldCompte.sendingName = Nom.Text
        oldCompte.email = courriel.Text
        oldCompte.popServer = New MailAccount.MailAccountServer(POP.Text, POPPort.Text, POPSecurized.Checked)
        oldCompte.smtpServer = New MailAccount.MailAccountServer(SMTP.Text, SMTPPort.Text, SMTPSecurized.Checked)
        oldCompte.username = UserName.Text
        oldCompte.password = myMDP
        oldCompte.savePassword = RetainMDP.Checked
        oldCompte.keepMSGOnServer = LeaveMSGOnServer.Checked
        oldCompte.includeInGeneralReception = IncludeAccount.Checked
        oldCompte.commonAccount = CommonAccount.Checked
        oldCompte.inboxFolderName = TransferToFolder.Text
        oldCompte.timeoutInSeconds = TimeoutInSeconds.Value
        oldCompte.smtpAuthenUsername = SMTPAuthenUsername.Text
        oldCompte.smtpNeedAuthentication = SMTPNeedAuthentication.Checked
        oldCompte.smtpPassword = mySMTPMDP
        oldCompte.smtpSavePassword = SMTPSavePassword.Checked
        oldCompte.smtpSpecificCredential = SMTPSpecificCredential.Checked
        oldCompte.canSendEmail = CanSendEmail.Checked

        oldCompte.saveData()

        formModified = False

        'Refresh items by a workaround without reloading
        Dim curCompte As MailAccount = oldCompte
        comptes.SuspendLayout()
        comptes.Sorted = True : comptes.Sorted = False
        formModified = False
        comptes.SelectedItem = curCompte
        comptes.ResumeLayout()

        myMainWin.StatusText = "Modification du compte de courriel " & curCompte.accountName
    End Sub

    Private Sub renommer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles renommer.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        Dim newAccountName As String = myInputBoxPlus.Prompt("Veuillez entrer le nouveau nom pour ce compte", "Nom du compte", CType(comptes.SelectedItem, MailAccount).accountName)
        If newAccountName = "" Then Exit Sub

        Dim accountWithName As MailAccount = MailsManager.getInstance.getMailAccount(newAccountName)
        If accountWithName IsNot Nothing AndAlso accountWithName.noMailAccount <> oldCompte.noMailAccount Then
            MessageBox.Show("Un autre compte de courriel porte déjà ce nom", "Impossible de renommer")
            Exit Sub
        End If



        oldCompte.accountName = newAccountName
        oldCompte.saveData()

        Me.comptes.Items.Remove(oldCompte)
        Me.comptes.Items.Add(oldCompte)
        Me.comptes.SelectedItem = oldCompte
    End Sub

    Private Sub enlever_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles enlever.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce compte de courriel ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        oldCompte.delete()
        comptes.Items.Remove(oldCompte)
        If Me.comptes.Items.Count = 0 Then
            Me.enlever.Enabled = False
            Me.modif.Enabled = False
            Me.renommer.Enabled = False
        Else
            Me.comptes.SelectedIndex = 0
        End If
    End Sub

    Private Sub objects_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nom.TextChanged, courriel.TextChanged, POP.TextChanged, POPPort.TextChanged, SMTP.TextChanged, SMTPPort.TextChanged, UserName.TextChanged, Password.TextChanged, POPSecurized.CheckedChanged, SMTPSecurized.CheckedChanged, CommonAccount.CheckedChanged, IncludeAccount.CheckedChanged, RetainMDP.CheckedChanged, LeaveMSGOnServer.CheckedChanged, TransferToFolder.SelectedIndexChanged, SMTPNeedAuthentication.CheckedChanged, SMTPSpecificCredential.CheckedChanged, SMTPAuthenUsername.TextChanged, SMTPPassword.TextChanged, SMTPSavePassword.CheckedChanged, TimeoutInSeconds.ValueChanged, CanSendEmail.CheckedChanged
        formModified = True
    End Sub


    Private Sub smtpPort_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SMTPPort.TextChanged
        'REM This limitation could be removed by using something else than SMTPClient to send an email because this port is an internal exchange port
        If SMTPPort.Text = "465" Then
            MessageBox.Show("Impossible d'utiliser ce port, car il n'est pas supporté par Clinica présentement. Veuillez utiliser un autre port.", "Port non supporté", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SMTPPort.Text = MailAccount.DEFAULT_OUTGOING_SERVER_PORT
        End If
    End Sub

    Private Sub smtpNeedAuthentication_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SMTPNeedAuthentication.CheckedChanged
        SMTPSpecificCredential.Enabled = SMTPNeedAuthentication.Checked
        SMTPAuthenUsername.Enabled = SMTPNeedAuthentication.Checked AndAlso SMTPSpecificCredential.Checked
        SMTPPassword.Enabled = SMTPNeedAuthentication.Checked AndAlso SMTPSpecificCredential.Checked
        SMTPSavePassword.Enabled = SMTPNeedAuthentication.Checked AndAlso SMTPSpecificCredential.Checked
    End Sub

    Private Sub smtpSpecificCredential_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SMTPSpecificCredential.CheckedChanged
        SMTPAuthenUsername.Enabled = SMTPNeedAuthentication.Checked AndAlso SMTPSpecificCredential.Checked
        SMTPPassword.Enabled = SMTPNeedAuthentication.Checked AndAlso SMTPSpecificCredential.Checked
        SMTPSavePassword.Enabled = SMTPNeedAuthentication.Checked AndAlso SMTPSpecificCredential.Checked
        showPassword2.Enabled = SMTPNeedAuthentication.Checked AndAlso SMTPSpecificCredential.Checked
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf msgParam_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.fromExternal AndAlso dataReceived.function = "MailAccounts" Then
            loading()
        End If
    End Sub

    Private Sub showPassword_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles showPassword.CheckedChanged
        Password.PasswordChar = If(showPassword.Checked, "", "*")
    End Sub

    Private Sub showPassword2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showPassword2.CheckedChanged
        SMTPPassword.PasswordChar = If(showPassword2.Checked, "", "*")
    End Sub
End Class
