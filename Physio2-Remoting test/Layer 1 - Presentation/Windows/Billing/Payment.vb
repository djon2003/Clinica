Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Friend Class Payment
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin
        Me.accepter.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance().getImage("paiement16.gif"))
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
    Public WithEvents _Labels_4 As System.Windows.Forms.Label
    Public WithEvents _infoclient_5 As System.Windows.Forms.Label
    Public WithEvents _infoclient_3 As System.Windows.Forms.Label
    Public WithEvents _infoclient_1 As System.Windows.Forms.Label
    Public WithEvents _infoclient_0 As System.Windows.Forms.Label
    Public WithEvents _Labels_3 As System.Windows.Forms.Label
    Public WithEvents _Labels_2 As System.Windows.Forms.Label
    Public WithEvents _Labels_1 As System.Windows.Forms.Label
    Public WithEvents _Labels_0 As System.Windows.Forms.Label
    Public WithEvents _Labels_6 As System.Windows.Forms.Label
    Public WithEvents _infoclient_4 As System.Windows.Forms.Label
    Public WithEvents _infoclient_2 As System.Windows.Forms.Label
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents nbFacture As System.Windows.Forms.Label
    Public WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents TypePaiement As System.Windows.Forms.ComboBox
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents pMontantTotal As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents Dossier As System.Windows.Forms.ComboBox
    Friend WithEvents accepter As System.Windows.Forms.Button
    Public WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents label5 As System.Windows.Forms.Label
    Public WithEvents montantTotal As System.Windows.Forms.Label
    Friend WithEvents BillEntityShowed As System.Windows.Forms.ComboBox
    Public WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents Comments As System.Windows.Forms.TextBox
    Public WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents BillsView As CI.Base.Windows.Forms.DataGridPlus
    Friend WithEvents BillSelected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DateF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MPReal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents EntityLinked As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtraInfos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoFacture As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsReadOnly As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents MFMin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MFPourcent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MPDone As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents rMontantTotal As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me._Labels_4 = New System.Windows.Forms.Label
        Me._infoclient_5 = New System.Windows.Forms.Label
        Me._infoclient_3 = New System.Windows.Forms.Label
        Me._infoclient_1 = New System.Windows.Forms.Label
        Me._infoclient_0 = New System.Windows.Forms.Label
        Me._Labels_3 = New System.Windows.Forms.Label
        Me._Labels_2 = New System.Windows.Forms.Label
        Me._Labels_1 = New System.Windows.Forms.Label
        Me._Labels_0 = New System.Windows.Forms.Label
        Me._Labels_6 = New System.Windows.Forms.Label
        Me._infoclient_4 = New System.Windows.Forms.Label
        Me._infoclient_2 = New System.Windows.Forms.Label
        Me.accepter = New System.Windows.Forms.Button
        Me.nbFacture = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.pMontantTotal = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.TypePaiement = New System.Windows.Forms.ComboBox
        Me.label4 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.Dossier = New System.Windows.Forms.ComboBox
        Me.rMontantTotal = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.label5 = New System.Windows.Forms.Label
        Me.montantTotal = New System.Windows.Forms.Label
        Me.BillEntityShowed = New System.Windows.Forms.ComboBox
        Me.label7 = New System.Windows.Forms.Label
        Me.Comments = New System.Windows.Forms.TextBox
        Me.label8 = New System.Windows.Forms.Label
        Me.BillsView = New CI.Base.Windows.Forms.DataGridPlus
        Me.BillSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DateF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MPReal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PaymentType = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.EntityLinked = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExtraInfos = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoFacture = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IsReadOnly = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.MFMin = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MFPourcent = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MPDone = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.BillsView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_Labels_4
        '
        Me._Labels_4.AutoSize = True
        Me._Labels_4.BackColor = System.Drawing.Color.Transparent
        Me._Labels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_4.Location = New System.Drawing.Point(8, 40)
        Me._Labels_4.Name = "_Labels_4"
        Me._Labels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_4.Size = New System.Drawing.Size(118, 14)
        Me._Labels_4.TabIndex = 26
        Me._Labels_4.Text = "Numéro de téléphone :"
        '
        '_infoclient_5
        '
        Me._infoclient_5.AutoSize = True
        Me._infoclient_5.BackColor = System.Drawing.Color.Transparent
        Me._infoclient_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._infoclient_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._infoclient_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._infoclient_5.Location = New System.Drawing.Point(416, 40)
        Me._infoclient_5.Name = "_infoclient_5"
        Me._infoclient_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._infoclient_5.Size = New System.Drawing.Size(0, 14)
        Me._infoclient_5.TabIndex = 25
        '
        '_infoclient_3
        '
        Me._infoclient_3.AutoSize = True
        Me._infoclient_3.BackColor = System.Drawing.SystemColors.Control
        Me._infoclient_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._infoclient_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._infoclient_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._infoclient_3.Location = New System.Drawing.Point(400, 8)
        Me._infoclient_3.Name = "_infoclient_3"
        Me._infoclient_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._infoclient_3.Size = New System.Drawing.Size(0, 14)
        Me._infoclient_3.TabIndex = 23
        '
        '_infoclient_1
        '
        Me._infoclient_1.AutoSize = True
        Me._infoclient_1.BackColor = System.Drawing.SystemColors.Control
        Me._infoclient_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._infoclient_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._infoclient_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._infoclient_1.Location = New System.Drawing.Point(48, 24)
        Me._infoclient_1.Name = "_infoclient_1"
        Me._infoclient_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._infoclient_1.Size = New System.Drawing.Size(0, 14)
        Me._infoclient_1.TabIndex = 22
        '
        '_infoclient_0
        '
        Me._infoclient_0.AutoSize = True
        Me._infoclient_0.BackColor = System.Drawing.SystemColors.Control
        Me._infoclient_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._infoclient_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._infoclient_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._infoclient_0.Location = New System.Drawing.Point(88, 8)
        Me._infoclient_0.Name = "_infoclient_0"
        Me._infoclient_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._infoclient_0.Size = New System.Drawing.Size(0, 14)
        Me._infoclient_0.TabIndex = 21
        '
        '_Labels_3
        '
        Me._Labels_3.AutoSize = True
        Me._Labels_3.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_3.Location = New System.Drawing.Point(344, 24)
        Me._Labels_3.Name = "_Labels_3"
        Me._Labels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_3.Size = New System.Drawing.Size(35, 14)
        Me._Labels_3.TabIndex = 19
        Me._Labels_3.Text = "Ville :"
        '
        '_Labels_2
        '
        Me._Labels_2.AutoSize = True
        Me._Labels_2.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_2.Location = New System.Drawing.Point(344, 8)
        Me._Labels_2.Name = "_Labels_2"
        Me._Labels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_2.Size = New System.Drawing.Size(54, 14)
        Me._Labels_2.TabIndex = 18
        Me._Labels_2.Text = "Adresse :"
        '
        '_Labels_1
        '
        Me._Labels_1.AutoSize = True
        Me._Labels_1.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_1.Location = New System.Drawing.Point(8, 24)
        Me._Labels_1.Name = "_Labels_1"
        Me._Labels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_1.Size = New System.Drawing.Size(37, 14)
        Me._Labels_1.TabIndex = 17
        Me._Labels_1.Text = "NAM :"
        '
        '_Labels_0
        '
        Me._Labels_0.AutoSize = True
        Me._Labels_0.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_0.Location = New System.Drawing.Point(8, 8)
        Me._Labels_0.Name = "_Labels_0"
        Me._Labels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_0.Size = New System.Drawing.Size(80, 14)
        Me._Labels_0.TabIndex = 16
        Me._Labels_0.Text = "Nom, Prénom :"
        '
        '_Labels_6
        '
        Me._Labels_6.AutoSize = True
        Me._Labels_6.BackColor = System.Drawing.Color.Transparent
        Me._Labels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_6.Location = New System.Drawing.Point(344, 40)
        Me._Labels_6.Name = "_Labels_6"
        Me._Labels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_6.Size = New System.Drawing.Size(73, 14)
        Me._Labels_6.TabIndex = 20
        Me._Labels_6.Text = "Code Postal :"
        '
        '_infoclient_4
        '
        Me._infoclient_4.AutoSize = True
        Me._infoclient_4.BackColor = System.Drawing.SystemColors.Control
        Me._infoclient_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._infoclient_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._infoclient_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._infoclient_4.Location = New System.Drawing.Point(384, 24)
        Me._infoclient_4.Name = "_infoclient_4"
        Me._infoclient_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._infoclient_4.Size = New System.Drawing.Size(0, 14)
        Me._infoclient_4.TabIndex = 24
        '
        '_infoclient_2
        '
        Me._infoclient_2.AutoSize = True
        Me._infoclient_2.BackColor = System.Drawing.SystemColors.Control
        Me._infoclient_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._infoclient_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._infoclient_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._infoclient_2.Location = New System.Drawing.Point(128, 40)
        Me._infoclient_2.Name = "_infoclient_2"
        Me._infoclient_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._infoclient_2.Size = New System.Drawing.Size(0, 14)
        Me._infoclient_2.TabIndex = 27
        '
        'accepter
        '
        Me.accepter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.accepter.Enabled = False
        Me.accepter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.accepter.Location = New System.Drawing.Point(728, 339)
        Me.accepter.Name = "accepter"
        Me.accepter.Size = New System.Drawing.Size(24, 24)
        Me.accepter.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.accepter, "Accepter le paiement")
        '
        'nbFacture
        '
        Me.nbFacture.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nbFacture.AutoSize = True
        Me.nbFacture.BackColor = System.Drawing.Color.Transparent
        Me.nbFacture.Cursor = System.Windows.Forms.Cursors.Default
        Me.nbFacture.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nbFacture.ForeColor = System.Drawing.SystemColors.ControlText
        Me.nbFacture.Location = New System.Drawing.Point(116, 305)
        Me.nbFacture.Name = "nbFacture"
        Me.nbFacture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nbFacture.Size = New System.Drawing.Size(13, 14)
        Me.nbFacture.TabIndex = 32
        Me.nbFacture.Text = "0"
        '
        'label2
        '
        Me.label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(12, 305)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(105, 14)
        Me.label2.TabIndex = 31
        Me.label2.Text = "Nombre de facture :"
        '
        'pMontantTotal
        '
        Me.pMontantTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pMontantTotal.AutoSize = True
        Me.pMontantTotal.BackColor = System.Drawing.Color.Transparent
        Me.pMontantTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.pMontantTotal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pMontantTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pMontantTotal.Location = New System.Drawing.Point(267, 328)
        Me.pMontantTotal.Name = "pMontantTotal"
        Me.pMontantTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pMontantTotal.Size = New System.Drawing.Size(22, 14)
        Me.pMontantTotal.TabIndex = 34
        Me.pMontantTotal.Text = "0 $"
        '
        'label3
        '
        Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(166, 328)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(100, 14)
        Me.label3.TabIndex = 33
        Me.label3.Text = "Total du paiement :"
        '
        'TypePaiement
        '
        Me.TypePaiement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypePaiement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypePaiement.Location = New System.Drawing.Point(514, 302)
        Me.TypePaiement.Name = "TypePaiement"
        Me.TypePaiement.Size = New System.Drawing.Size(192, 21)
        Me.TypePaiement.Sorted = True
        Me.TypePaiement.TabIndex = 35
        '
        'label4
        '
        Me.label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(368, 305)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(118, 14)
        Me.label4.TabIndex = 36
        Me.label4.Text = "Méthode de paiement :"
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(368, 329)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(140, 14)
        Me.label1.TabIndex = 38
        Me.label1.Text = "Dossier(s) sélectionné(s) :"
        '
        'Dossier
        '
        Me.Dossier.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Dossier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Dossier.DropDownWidth = 148
        Me.Dossier.Location = New System.Drawing.Point(514, 326)
        Me.Dossier.Name = "Dossier"
        Me.Dossier.Size = New System.Drawing.Size(192, 21)
        Me.Dossier.Sorted = True
        Me.Dossier.TabIndex = 37
        '
        'rMontantTotal
        '
        Me.rMontantTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rMontantTotal.AutoSize = True
        Me.rMontantTotal.BackColor = System.Drawing.Color.Transparent
        Me.rMontantTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.rMontantTotal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rMontantTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rMontantTotal.Location = New System.Drawing.Point(267, 305)
        Me.rMontantTotal.Name = "rMontantTotal"
        Me.rMontantTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rMontantTotal.Size = New System.Drawing.Size(22, 14)
        Me.rMontantTotal.TabIndex = 40
        Me.rMontantTotal.Text = "0 $"
        '
        'label6
        '
        Me.label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.Color.Transparent
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(12, 329)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(101, 14)
        Me.label6.TabIndex = 39
        Me.label6.Text = "Total des factures :"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'label5
        '
        Me.label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(165, 305)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(76, 14)
        Me.label5.TabIndex = 39
        Me.label5.Text = "Total à payer :"
        '
        'montantTotal
        '
        Me.montantTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.montantTotal.AutoSize = True
        Me.montantTotal.BackColor = System.Drawing.Color.Transparent
        Me.montantTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.montantTotal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.montantTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.montantTotal.Location = New System.Drawing.Point(116, 329)
        Me.montantTotal.Name = "montantTotal"
        Me.montantTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.montantTotal.Size = New System.Drawing.Size(22, 14)
        Me.montantTotal.TabIndex = 40
        Me.montantTotal.Text = "0 $"
        '
        'BillEntityShowed
        '
        Me.BillEntityShowed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BillEntityShowed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BillEntityShowed.DropDownWidth = 148
        Me.BillEntityShowed.Location = New System.Drawing.Point(514, 350)
        Me.BillEntityShowed.Name = "BillEntityShowed"
        Me.BillEntityShowed.Size = New System.Drawing.Size(192, 21)
        Me.BillEntityShowed.Sorted = True
        Me.BillEntityShowed.TabIndex = 37
        '
        'label7
        '
        Me.label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.Color.Transparent
        Me.label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label7.Location = New System.Drawing.Point(368, 353)
        Me.label7.Name = "label7"
        Me.label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label7.Size = New System.Drawing.Size(146, 14)
        Me.label7.TabIndex = 38
        Me.label7.Text = "Entité(s) liée(s) affichée(s) :"
        '
        'Comments
        '
        Me.Comments.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Comments.Location = New System.Drawing.Point(103, 350)
        Me.Comments.Name = "Comments"
        Me.Comments.Size = New System.Drawing.Size(248, 20)
        Me.Comments.TabIndex = 41
        '
        'label8
        '
        Me.label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(12, 352)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(85, 14)
        Me.label8.TabIndex = 39
        Me.label8.Text = "Commentaires :"
        '
        'BillsView
        '
        Me.BillsView.AllowUserToAddRows = False
        Me.BillsView.AllowUserToDeleteRows = False
        Me.BillsView.AllowUserToResizeRows = False
        Me.BillsView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BillsView.autoSelectOnDataSourceChanged = True
        Me.BillsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillsView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.BillsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillsView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BillSelected, Me.DateF, Me.TF, Me.MF, Me.MP, Me.MPReal, Me.PaymentType, Me.EntityLinked, Me.ExtraInfos, Me.NoFacture, Me.IsReadOnly, Me.MFMin, Me.MFPourcent, Me.MPDone})
        Me.BillsView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.BillsView.isDoubleBuffered = False
        Me.BillsView.Location = New System.Drawing.Point(11, 57)
        Me.BillsView.MultiSelect = False
        Me.BillsView.Name = "BillsView"
        Me.BillsView.RowHeadersVisible = False
        Me.BillsView.RowHeadersWidth = 20
        Me.BillsView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.BillsView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.BillsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillsView.ShowEditingIcon = False
        Me.BillsView.Size = New System.Drawing.Size(737, 239)
        Me.BillsView.TabIndex = 252
        '
        'BillSelected
        '
        Me.BillSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.BillSelected.DataPropertyName = "BillSelected"
        Me.BillSelected.Frozen = True
        Me.BillSelected.HeaderText = ""
        Me.BillSelected.Name = "BillSelected"
        Me.BillSelected.Width = 5
        '
        'DateF
        '
        Me.DateF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DateF.DataPropertyName = "DateF"
        DataGridViewCellStyle5.Format = "d"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.DateF.DefaultCellStyle = DataGridViewCellStyle5
        Me.DateF.Frozen = True
        Me.DateF.HeaderText = "Date facture"
        Me.DateF.MinimumWidth = 65
        Me.DateF.Name = "DateF"
        Me.DateF.ReadOnly = True
        Me.DateF.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DateF.Width = 65
        '
        'TF
        '
        Me.TF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.TF.DataPropertyName = "TF"
        Me.TF.Frozen = True
        Me.TF.HeaderText = "Type facture"
        Me.TF.Name = "TF"
        Me.TF.ReadOnly = True
        Me.TF.Width = 92
        '
        'MF
        '
        Me.MF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.MF.DataPropertyName = "MF"
        DataGridViewCellStyle6.Format = "C2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.MF.DefaultCellStyle = DataGridViewCellStyle6
        Me.MF.HeaderText = "Montant facturé"
        Me.MF.MinimumWidth = 71
        Me.MF.Name = "MF"
        Me.MF.ReadOnly = True
        Me.MF.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MF.Width = 71
        '
        'MP
        '
        Me.MP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.MP.DataPropertyName = "MP"
        DataGridViewCellStyle7.Format = "C2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.MP.DefaultCellStyle = DataGridViewCellStyle7
        Me.MP.HeaderText = "Montant à payer"
        Me.MP.MinimumWidth = 71
        Me.MP.Name = "MP"
        Me.MP.ReadOnly = True
        Me.MP.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MP.Width = 71
        '
        'MPReal
        '
        Me.MPReal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.MPReal.DataPropertyName = "MPReal"
        DataGridViewCellStyle8.Format = "C2"
        Me.MPReal.DefaultCellStyle = DataGridViewCellStyle8
        Me.MPReal.HeaderText = "Montant paiement"
        Me.MPReal.MinimumWidth = 75
        Me.MPReal.Name = "MPReal"
        Me.MPReal.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MPReal.Width = 75
        '
        'PaymentType
        '
        Me.PaymentType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.PaymentType.DataPropertyName = "PaymentType"
        Me.PaymentType.HeaderText = "Type paiement"
        Me.PaymentType.MinimumWidth = 83
        Me.PaymentType.Name = "PaymentType"
        Me.PaymentType.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PaymentType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.PaymentType.Width = 83
        '
        'EntityLinked
        '
        Me.EntityLinked.DataPropertyName = "EntityLinked"
        Me.EntityLinked.HeaderText = "Entitée liée"
        Me.EntityLinked.Name = "EntityLinked"
        Me.EntityLinked.ReadOnly = True
        '
        'ExtraInfos
        '
        Me.ExtraInfos.DataPropertyName = "ExtraInfos"
        Me.ExtraInfos.HeaderText = "Info"
        Me.ExtraInfos.Name = "ExtraInfos"
        Me.ExtraInfos.ReadOnly = True
        '
        'NoFacture
        '
        Me.NoFacture.DataPropertyName = "NoFacture"
        Me.NoFacture.HeaderText = "# Facture"
        Me.NoFacture.Name = "NoFacture"
        Me.NoFacture.ReadOnly = True
        Me.NoFacture.Visible = False
        '
        'IsReadOnly
        '
        Me.IsReadOnly.DataPropertyName = "IsReadOnly"
        Me.IsReadOnly.HeaderText = "IsReadOnly"
        Me.IsReadOnly.Name = "IsReadOnly"
        Me.IsReadOnly.Visible = False
        '
        'MFMin
        '
        Me.MFMin.DataPropertyName = "MFMin"
        Me.MFMin.HeaderText = "MFMin"
        Me.MFMin.Name = "MFMin"
        Me.MFMin.Visible = False
        '
        'MFPourcent
        '
        Me.MFPourcent.DataPropertyName = "MFPourcent"
        Me.MFPourcent.HeaderText = "MFPourcent"
        Me.MFPourcent.Name = "MFPourcent"
        Me.MFPourcent.Visible = False
        '
        'MPDone
        '
        Me.MPDone.DataPropertyName = "MPDone"
        Me.MPDone.HeaderText = "MPDone"
        Me.MPDone.Name = "MPDone"
        Me.MPDone.Visible = False
        '
        'Payment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(760, 375)
        Me.Controls.Add(Me.BillsView)
        Me.Controls.Add(Me.Comments)
        Me.Controls.Add(Me.montantTotal)
        Me.Controls.Add(Me.rMontantTotal)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.BillEntityShowed)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.Dossier)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.TypePaiement)
        Me.Controls.Add(Me.pMontantTotal)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.nbFacture)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.accepter)
        Me.Controls.Add(Me._infoclient_2)
        Me.Controls.Add(Me._Labels_4)
        Me.Controls.Add(Me._infoclient_5)
        Me.Controls.Add(Me._infoclient_4)
        Me.Controls.Add(Me._infoclient_3)
        Me.Controls.Add(Me._infoclient_1)
        Me.Controls.Add(Me._infoclient_0)
        Me.Controls.Add(Me._Labels_3)
        Me.Controls.Add(Me._Labels_2)
        Me.Controls.Add(Me._Labels_1)
        Me.Controls.Add(Me._Labels_0)
        Me.Controls.Add(Me._Labels_6)
        Me.MinimumSize = New System.Drawing.Size(768, 257)
        Me.Name = "Payment"
        Me.ShowInTaskbar = False
        Me.Text = "Effectuer le(s) paiement(s)"
        CType(Me.BillsView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _Loaded As Boolean = False
    Private _nam As String
    Private _NoClient As Integer
    Private _NoKP As Integer
    Private _NoClinique As Integer
    Private _NoUser As Integer
    Private _NoUserFacture As Integer
    Private _NoAccount As Integer = 0
    Private isLocked As Boolean = True
    Private _DedicatedType As FacturationBox.DedicatedType
    Private folderCodes As New Generic.Dictionary(Of Integer, FolderCode)
    Private folderNoTRPTraitant As New Generic.Dictionary(Of Integer, Integer)

#Region "Propriétés"
    Public Property noUser() As Integer
        Get
            Return _NoUser
        End Get
        Set(ByVal Value As Integer)
            _NoUser = Value
        End Set
    End Property

    Public Property noUserFacture() As Integer
        Get
            Return _NoUserFacture
        End Get
        Set(ByVal Value As Integer)
            _NoUserFacture = Value
        End Set
    End Property

    Public Property noClinique() As Integer
        Get
            Return _NoClinique
        End Get
        Set(ByVal Value As Integer)
            _NoClinique = Value
        End Set
    End Property

    Public ReadOnly Property dedicatedType() As FacturationBox.DedicatedType
        Get
            Return _DedicatedType
        End Get
    End Property

    Public Property noClient() As Integer
        Get
            Return _NoClient
        End Get
        Set(ByVal Value As Integer)
            _NoClient = Value
        End Set
    End Property

    Public Property noKP() As Integer
        Get
            Return _NoKP
        End Get
        Set(ByVal Value As Integer)
            _NoKP = Value
        End Set
    End Property

    Public Property billsLoaded() As Boolean
        Get
            Return _Loaded
        End Get
        Set(ByVal Value As Boolean)
            _Loaded = Value
        End Set
    End Property
#End Region

    Private nbBillsSelected As Integer = 0

    Private Sub calculateTotal()
        Dim totalAmountP As Double = 0
        Dim totalAmountF As Double = 0
        Dim totalAmountR As Double = 0
        Dim n As Integer = 0

        nbBillsSelected = 0

        For Each curRow As DataGridViewRow In BillsView.Rows
            If curRow.Cells("BillSelected").Value IsNot DBNull.Value AndAlso curRow.Cells("BillSelected").Value = 1 Then
                If curRow.Cells("MPReal").Value Is DBNull.Value Then
                    'Verifying error
                    addErrorLog(New Exception("MPReal is null. # bill = " & curRow.Cells("NoFacture").Value))
                Else
                    nbBillsSelected += 1
                    totalAmountP += curRow.Cells("MPReal").Value
                    totalAmountR += curRow.Cells("MP").Value
                End If
            End If

            If curRow.Cells("MF").Value Is DBNull.Value Then
                addErrorLog(New Exception("MF column is null!!... Client #" & Me.noClient & ", KP #" & Me.noKP))
            Else
                totalAmountF += curRow.Cells("MF").Value
            End If
            n += 1
        Next

        accepter.Enabled = nbBillsSelected <> 0
        nbFacture.Text = BillsView.RowCount
        rMontantTotal.Text = totalAmountR & " $"
        pMontantTotal.Text = totalAmountP & " $"
        montantTotal.Text = totalAmountF & " $"
    End Sub

    Private Sub mfChanged(ByVal sender As Object, ByVal oldAmount As Double, ByVal newAmount As Double)
        If oldAmount <> newAmount AndAlso CType(sender, PaymentBox).UsingPaymentBox = False Then CType(sender, PaymentBox).UsingPaymentBox = True
        calculateTotal()
    End Sub

    Private Sub mpChanged(ByVal sender As Object, ByVal oldAmount As Double, ByVal newAmount As Double)
        If oldAmount <> newAmount AndAlso CType(sender, PaymentBox).UsingPaymentBox = False Then CType(sender, PaymentBox).UsingPaymentBox = True
        calculateTotal()
    End Sub

    Private Sub upbChanged(ByVal sender As Object)
        calculateTotal()
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        accepter.Enabled = Not trueFalse AndAlso nbBillsSelected <> 0
        Dossier.Enabled = Not trueFalse
        TypePaiement.Enabled = Not trueFalse
        BillsView.ReadOnly = trueFalse
    End Sub

    Public Sub loading(ByVal noAccount As Integer, ByVal dedicatedType As FacturationBox.DedicatedType, Optional ByVal skipMsgBox As Boolean = False)
        'REM_CODES
        If noAccount = 0 Then MessageBox.Show("Compte inexistant", "Compte inexistant") : accepter.Enabled = False : Exit Sub

        Dim WhereStr2, FieldsStr, TableStr, WhereStr, GeneralInfo(,) As String
        Dim n As Short = 0
        Dim myCode As FolderCode
        Dim nbBills As Integer = 0

        If dedicatedType = -1 Then
            dedicatedType = _DedicatedType
        Else
            _DedicatedType = dedicatedType
        End If

        noClient = 0 : noKP = 0 : noClinique = 0 : noUserFacture = 0
        Select Case dedicatedType
            Case FacturationBox.DedicatedType.Clinique
                noClinique = noAccount
                If billsLoaded = False Then
                    If isLocked = True AndAlso lockSecteur("CliniqueFacturation-" & noClinique, True, "Facturation de la clinique", False) = False Then
                        If skipMsgBox = False AndAlso PreferencesManager.getUserPreferences()("AffMSGModif") = True Then MessageBox.Show("Factures & Paiements de cette clinique en cours de modification", "Impossible d'effectuer le(s) paiement(s)")
                        lockItems(True)
                        Me.isLocked = True
                    Else
                        lockItems(False)
                        isLocked = False
                    End If
                End If

                _Labels_0.Text = "Nom :"
                _Labels_1.Text = "NEQ :"
                WhereStr = "WHERE (NoClinique=" & noAccount & ");"
                TableStr = "Villes RIGHT JOIN InfoClinique ON Villes.NoVille = InfoClinique.NoVille"
                FieldsStr = "InfoClinique.Nom AS Fullname, InfoClinique.Telephone, InfoClinique.Adresse, Villes.NomVille, InfoClinique.CodePostal, InfoClinique.NEQ"
                WhereStr2 = "WHERE (Factures.ParNoClinique=" & noAccount & " AND IsSouffrance=0)"
            Case FacturationBox.DedicatedType.KP
                noKP = noAccount
                If billsLoaded = False Then
                    If isLocked = True AndAlso lockSecteur("KPFacturation-" & noKP & "-", True, "Facturation de la personne / organisme clé", False) = False Then
                        If skipMsgBox = False AndAlso PreferencesManager.getUserPreferences()("AffMSGModif") = True Then MessageBox.Show("Factures & Paiements de cet(te) personne / organisme clé en cours de modification", "Impossible d'effectuer le(s) paiement(s)")
                        lockItems(True)
                        isLocked = True
                    Else
                        lockItems(False)
                        isLocked = False
                    End If
                End If

                _Labels_0.Text = "Nom :"
                _Labels_1.Text = "N° identifiant :"
                WhereStr = "WHERE (NoKP=" & noAccount & ");"
                TableStr = "Villes RIGHT JOIN KeyPeople ON Villes.NoVille = KeyPeople.NoVille"
                FieldsStr = "KeyPeople.Nom AS Fullname, KeyPeople.Telephones, KeyPeople.Adresse, Villes.NomVille, KeyPeople.CodePostal, KeyPeople.NoRef"
                WhereStr2 = "WHERE (Factures.ParNoKP=" & noAccount & " AND IsSouffrance=0)"
            Case FacturationBox.DedicatedType.User
                noUserFacture = noAccount
                If billsLoaded = False Then
                    If isLocked = True AndAlso lockSecteur("UserFacturation-" & noUserFacture, True, "Facturation d'un travailleur autonome", False) = False Then
                        If skipMsgBox = False AndAlso PreferencesManager.getUserPreferences()("AffMSGModif") = True Then MessageBox.Show("Factures & Paiements de ce travailleur autonome en cours de modification", "Impossible d'effectuer le(s) paiement(s)")
                        lockItems(True)
                        isLocked = True
                    Else
                        lockItems(False)
                        isLocked = False
                    End If
                End If

                _Labels_0.Text = "Nom, prénom :"
                _Labels_1.Text = "N° du permis :"
                WhereStr = "WHERE (NoUser=" & noAccount & ");"
                TableStr = "Villes RIGHT JOIN Utilisateurs ON Villes.NoVille = Utilisateurs.NoVille"
                FieldsStr = "Utilisateurs.Nom + ',' + Utilisateurs.Prenom AS Fullname, Utilisateurs.Telephones, Utilisateurs.Adresse, Villes.NomVille, Utilisateurs.CodePostal, Utilisateurs.NoPermis"
                WhereStr2 = "WHERE (Factures.ParNoUser=" & noAccount & " AND IsSouffrance=0)"
            Case Else
                noClient = noAccount
                If billsLoaded = False Then
                    If isLocked = True AndAlso lockSecteur("ClientFacturation-" & noClient & "-", True, "Facturation du client", False) = False Then
                        If skipMsgBox = False AndAlso PreferencesManager.getUserPreferences()("AffMSGModif") = True Then MessageBox.Show("Factures & Paiements de ce client en cours de modification", "Impossible d'effectuer le(s) paiement(s)")
                        lockItems(True)
                        isLocked = True
                    Else
                        lockItems(False)
                        isLocked = False
                    End If
                End If

                _Labels_0.Text = "Nom, prénom :"
                _Labels_1.Text = "NAM :"
                WhereStr = "WHERE (NoClient=" & noClient & ");"
                TableStr = "Villes RIGHT JOIN InfoClients ON Villes.NoVille = InfoClients.NoVille"
                FieldsStr = "InfoClients.Nom + ',' + InfoClients.Prenom AS Fullname, InfoClients.Telephones, InfoClients.Adresse, Villes.NomVille, InfoClients.CodePostal, InfoClients.NAM"
                WhereStr2 = "WHERE (Factures.ParNoClient=" & noAccount & " AND IsSouffrance=0)"
        End Select
        Me._NoAccount = noAccount
        _infoclient_0.Left = _Labels_0.Left + _Labels_0.Width
        _infoclient_1.Left = _Labels_1.Left + _Labels_1.Width

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        GeneralInfo = DBLinker.getInstance.readDB(TableStr, FieldsStr, WhereStr)

        Dim dedicatedTypeSufix As String = If(dedicatedType = FacturationBox.DedicatedType.Client, "", If(dedicatedType = FacturationBox.DedicatedType.KP, "KP", If(dedicatedType = FacturationBox.DedicatedType.User, "User", "Clinique")))
        Dim bills As DataSet = DBLinker.getInstance.readDBForGrid("SELECT null AS BillSelected, '' AS PaymentType, ExtraInfos, NoTRPTraitant, NoCodeUser, NoFolder, NoFacture, NoCodeUnique, DateF, EntityLinked, TF, MF, MF AS OldMF, MP, MP AS MPReal, CASE WHEN (MFSingle*MPTotal/MF) < MF THEN (MFSingle*MPTotal/MF) ELSE MF END AS MFMin, (MFSingle/MF) AS MFPourcent, MPDone, IsReadOnly FROM (SELECT " & _
                                                      "Factures.TypeFacture AS TF, " & _
                                                      "CASE WHEN Factures.NoClient>0 THEN IC.Nom + ',' + IC.Prenom + ' (' + CAST(Factures.NoClient AS VARCHAR(MAX)) + ')' ELSE CASE WHEN Factures.NoKP>0 THEN KP.Nom + ' (' + CAST(Factures.NoKP AS VARCHAR(MAX)) + ')' ELSE U.Nom + ',' + U.Prenom + ' (' + CAST(Factures.NoUserFacture AS VARCHAR(MAX)) + ')' END END AS EntityLinked," & _
                                                      "(Factures.MontantFacture" & dedicatedTypeSufix & " - Factures.MontantPaiement" & dedicatedTypeSufix & ") AS MP," & _
                                                      "Factures.MontantFacture" & dedicatedTypeSufix & " AS MFSingle," & _
                                                      "Factures.MontantPaiement" & dedicatedTypeSufix & " AS MPDone," & _
                                                      "(Factures.MontantFacture + Factures.MontantFactureKP + Factures.MontantFactureUser + Factures.MontantFactureClinique) AS MF," & _
                                                      "(Factures.MontantPaiement + Factures.MontantPaiementKP + Factures.MontantPaiementUser + Factures.MontantPaiementClinique) AS MPTotal," & _
                                                      "'' AS ExtraInfos, " & _
                                                      "InfoFolders.NoTRPTraitant, NoCodeUser," & _
                                                      "CASE WHEN Factures.NoVente IS NULL THEN CASE WHEN Factures.NoPret IS NULL AND Factures.Taxe1 <> -1 THEN 0 ELSE 1 END ELSE 1 END AS IsReadOnly," & _
                                                      "Factures.DateFacture AS DateF, Factures.NoFolder, InfoFolders.NoCodeUnique, Factures.NoFacture " & _
                                                      "FROM Utilisateurs RIGHT OUTER JOIN Factures LEFT OUTER JOIN InfoClients AS IC ON IC.NoClient = Factures.NoClient LEFT OUTER JOIN KeyPeople AS KP ON KP.NoKP = Factures.NoKP LEFT OUTER JOIN Utilisateurs AS U ON U.NoUser = Factures.NoUserFacture LEFT OUTER JOIN InfoFolders ON Factures.NoFolder = InfoFolders.NoFolder ON Utilisateurs.NoUser = InfoFolders.NoTRPTraitant " & _
                                                      WhereStr2 & ") AS T WHERE MP > 0 ORDER BY EntityLinked, DateF")

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        Dim paymentTypes As Generic.List(Of String) = BillsManager.getInstance.getPaymentTypes()
        TypePaiement.Items.Clear()
        TypePaiement.Items.AddRange(paymentTypes.ToArray())
        TypePaiement.SelectedIndex = 0

        CType(BillsView.Columns("PaymentType"), DataGridViewComboBoxColumn).DataSource = paymentTypes

        BillEntityShowed.Items.Clear()
        BillEntityShowed.Items.Add("* Toutes les entités *")
        Dim entities As New Generic.List(Of String)
        Dim nEntities As Integer = -1

        If Me.Text.IndexOf("de") = -1 Then Me.Text = Me.Text & " de " & GeneralInfo(0, 0)
        _infoclient_0.Text = GeneralInfo(0, 0)
        _infoclient_1.Text = GeneralInfo(5, 0)
        If GeneralInfo(1, 0).IndexOf("§") > 0 Then
            _infoclient_2.Text = GeneralInfo(1, 0).Substring(0, GeneralInfo(1, 0).IndexOf("§"))
        Else
            _infoclient_2.Text = GeneralInfo(1, 0)
        End If
        _infoclient_3.Text = GeneralInfo(2, 0)
        _infoclient_4.Text = GeneralInfo(3, 0)
        If GeneralInfo(4, 0).Length > 3 Then GeneralInfo(4, 0) = GeneralInfo(4, 0).Insert(3, " ")
        _infoclient_5.Text = GeneralInfo(4, 0)

        Dim firstPaymentType As String = TypePaiement.Items(0)
        For Each curRow As DataRow In bills.Tables(0).Rows
            If curRow("NoFolder") Is DBNull.Value OrElse curRow("NoFolder") = 0 Then
                curRow("ExtraInfos") = "Aucun dossier associé"
                curRow("PaymentType") = firstPaymentType
            Else
                If folderCodes.ContainsKey(curRow("NoFolder")) = False Then
                    If curRow("NoCodeUnique") IsNot DBNull.Value Then
                        myCode = FolderCodesManager.getInstance.getItemable(Integer.Parse(curRow("NoCodeUnique")), If(curRow("NoCodeUser") Is DBNull.Value, 0, Integer.Parse(curRow("NoCodeUser"))), curRow("DateF"))
                    End If

                    folderNoTRPTraitant.Add(curRow("NoFolder"), curRow("NoTRPTraitant"))
                    folderCodes.Add(curRow("NoFolder"), myCode)
                Else
                    myCode = folderCodes(curRow("NoFolder"))
                End If

                curRow("ExtraInfos") = curRow("NoFolder") & "-" & UsersManager.getInstance.getUser(curRow("NoTRPTraitant")).toString() & ":" & myCode.name
                If myCode.autoSelectBillWhenPaying = True Then curRow("BillSelected") = True
                curRow("PaymentType") = If(myCode.defaultPaymentMethod = String.Empty, firstPaymentType, myCode.defaultPaymentMethod)
            End If

            If entities.Contains(curRow("EntityLinked")) = False Then
                entities.Add(curRow("EntityLinked"))
            End If
        Next

        BillsView.AutoGenerateColumns = False
        BillsView.DataSource = bills.Tables(0)

        calculateTotal()
        loadFolderList()

        If entities IsNot Nothing Then
            entities.Sort()
            BillEntityShowed.Items.AddRange(entities.ToArray())
        End If

        BillEntityShowed.SelectedIndex = 0

        billsLoaded = True
    End Sub

    Private Sub loadFolderList()
        Dossier.Items.Clear()
        Dossier.Items.Add("* Toutes les factures *")
        Dossier.Items.Add("* Aucune facture *")
        Dossier.Items.Add("* Tous les dossiers *")
        Dossier.Items.Add("* Aucun dossier associé *")

        For Each curRow As DataRow In CType(BillsView.DataSource, DataTable).Rows
            Dim noFolder As Integer = 0
            If curRow("NoFolder") IsNot DBNull.Value Then noFolder = curRow("NoFolder")
            If noFolder <> 0 AndAlso Dossier.FindStringExact(addZeros(noFolder, 6)) < 0 Then
                Dossier.Items.Add(addZeros(noFolder, 6))
            End If
        Next
    End Sub

    Private Sub typePaiement_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypePaiement.SelectedIndexChanged
        If BillsView.DataSource Is Nothing Then Exit Sub

        For Each curRow As DataRow In CType(BillsView.DataSource, DataTable).Rows
            curRow("PaymentType") = TypePaiement.SelectedItem
        Next
    End Sub

    Private Sub paiement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If isLocked = False And noClient <> 0 Then lockSecteur("ClientFacturation-" & Me.noClient & "-", False)
        If isLocked = False And noKP <> 0 Then lockSecteur("KPFacturation-" & Me.noKP & "-", False)
        If isLocked = False And noClinique <> 0 Then lockSecteur("CliniqueFacturation-" & Me.noClinique, False)
        If isLocked = False And noUserFacture <> 0 Then lockSecteur("UserFacturation-" & Me.noUserFacture, False)
    End Sub

    Private Sub dossier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Dossier.SelectedIndexChanged
        Dim oldCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        BillsView.SuspendLayout()

        Dim billSelected As Boolean
        For Each curRow As DataRow In CType(BillsView.DataSource, DataTable).Rows
            Select Case Dossier.SelectedItem.ToString
                Case "* Toutes les factures *"
                    billSelected = True

                Case "* Aucune facture *"
                    billSelected = False

                Case "* Tous les dossiers *"
                    billSelected = curRow("NoFolder") IsNot DBNull.Value

                Case "* Aucun dossier associé *"
                    billSelected = curRow("NoFolder") Is DBNull.Value

                Case Else
                    If curRow("NoFolder") IsNot DBNull.Value AndAlso addZeros(curRow("NoFolder"), 6) = Dossier.SelectedItem Then
                        billSelected = True
                    Else
                        billSelected = False
                    End If
            End Select
            curRow("BillSelected") = billSelected
        Next

        BillsView.ResumeLayout()
        Me.Cursor = oldCursor
    End Sub

    Private Sub accepter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles accepter.Click
        'REM_CODES
        accepter.Enabled = False 'S'assure que l'on ne peut cliquer qu'une seule fois

        Dim entitePayeur As String = "C"
        Dim montantFacture As Double
        Dim updatingEquipement As Boolean = False
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True
        If noClient > 0 Then entitePayeur = "C"
        If noKP > 0 Then entitePayeur = "K"
        If noUserFacture > 0 Then entitePayeur = "U"
        If noClinique > 0 Then entitePayeur = ""

        InternalUpdatesManager.getInstance.startBatchUpdate()

        For Each curRow As DataRow In CType(BillsView.DataSource, DataTable).Rows
            If curRow("BillSelected") Is DBNull.Value OrElse curRow("BillSelected") = False Then Continue For

            If curRow("MPReal") Is DBNull.Value Then
                'Verifying error
                addErrorLog(New Exception("MPReal is null. # bill = " & curRow("NoFacture")))
                MessageBox.Show("Impossible de prendre le paiement de la facture #" & curRow("NoFacture") & ". L'erreur devrait se corriger très bientôt! Si non appeler CyberInternautes Inc.", "Impossible de prendre un paiement", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Continue For
            End If

            montantFacture = -1
            Dim myComments As String = ""
            Dim curFacture As New Bill(curRow("NoFacture"))
            Dim billDate As String = DateFormat.getTextDate(CDate(curRow("DateF")))


            If CDbl(curRow("OldMF")) <> CDbl(curRow("MF")) Then
                If MessageBox.Show("Confirmez-vous le nouveau montant (" & curRow("MF") & "$) de cette facture :" & vbCrLf & curRow("TF") & " (" & billDate & ") ?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Continue For
                'Commentaires
                Dim myInputBoxPlus As New InputBoxPlus()
                myInputBoxPlus.firstLetterCapital = True
                myComments = myInputBoxPlus("Veuillez entrer le commentaire de l'ajustement sur le montant facturé", "Commentaire")
                If PreferencesManager.getGeneralPreferences()("AdjustingCommentsForced") = True And myComments = "" Then
                    MessageBox.Show("Le commentaire est obligatoire. Le paiement de cette facture ne sera pas effectué.", "Champ obligatoire")
                    Continue For
                End If
                montantFacture = curRow("MF")
            End If

            If adjustFacture(curRow("NoFacture"), montantFacture, CDbl(curRow("MPDone")) + CDbl(curRow("MPReal")), , , myComments, , curRow("PaymentType"), entitePayeur, , Comments.Text) = FactureAction.Deleted Then
                'Si la facture a été payé au complet

                'Et qu'il s'agit d'une visite --> Mettre à jour intra-inter logiciel
                If curFacture.noVisite > 0 Then
                    updateVisites(curFacture.noClient, curFacture.noFolder, curRow("DateF"), Me, , folderNoTRPTraitant(curFacture.noFolder))
                End If
            End If

            'Si la facture avait été définit selon une codification dossier, que la codification demande d'émettre un reçu automatiquement et qu'il y a un reçu a émettre et que le payeur a payé sa partie de la facture au complet
            If curFacture.noFolder > 0 AndAlso folderCodes(curFacture.noFolder).askReceipt Then
                curFacture.loadingData(curRow("NoFacture"))
                If curFacture.isPaymentsToDo(entitePayeur.ToCharArray()(0)) = False AndAlso curFacture.isReceiptToDo(entitePayeur) AndAlso MessageBox.Show("Voulez-vous générer un reçu pour la facture #" & curRow("NoFacture") & " (" & billDate & ")" & " : " & vbCrLf & curRow("TF"), "Génération d'un reçu", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    curFacture.generateReceipt(entitePayeur)
                End If
            End If
        Next

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        Dim myMontant As Double = CDbl(IIf(rMontantTotal.Text = "", 0, rMontantTotal.Text)) - CDbl(IIf(pMontantTotal.Text = "", 0, pMontantTotal.Text))
        Me.Close()
        If noClient > 0 Then
            InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & noClient & "," & myMontant & ")")
            myMainWin.StatusText = "Compte client " & noClient & " : Paiement effectué"
            InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & noClient & ")")
        End If
        If noKP > 0 Then
            InternalUpdatesManager.getInstance.sendUpdate("PaiementsKP(" & noKP & "," & myMontant & ")")
            myMainWin.StatusText = "Compte personne / organisme clé " & noKP & " : Paiement effectué"
            InternalUpdatesManager.getInstance.sendUpdate("AccountsBillsKP(" & noKP & ")")
        End If
        InternalUpdatesManager.getInstance.stopBatchUpdate()
        'REM Add Update for clinique & user
    End Sub

    Private Sub billEntityShowed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillEntityShowed.SelectedIndexChanged
        If billsLoaded = False Then Exit Sub

        If BillEntityShowed.SelectedIndex = 0 Then
            CType(BillsView.DataSource, DataTable).DefaultView.RowFilter = ""
        Else
            CType(BillsView.DataSource, DataTable).DefaultView.RowFilter = "EntityLinked = '" & BillEntityShowed.Text.Replace("'", "''") & "'"
        End If

        calculateTotal()
        loadFolderList()
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.fromExternal AndAlso dataReceived.function = "Paiements" AndAlso dataReceived.params(0) = Me.noClient Then
            loading(Me.noClient, FacturationBox.DedicatedType.Client, True)
        End If
        If dataReceived.fromExternal AndAlso dataReceived.function = "PaiementsKP" AndAlso dataReceived.params(0) = Me.noKP Then
            loading(Me.noKP, FacturationBox.DedicatedType.KP, True)
        End If
    End Sub

    Private Sub BillsView_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles BillsView.CellEndEdit
        calculateTotal()
    End Sub

    Private Sub BillsView_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles BillsView.CellValidating
        Dim columnName As String = BillsView.Columns(e.ColumnIndex).Name
        If columnName = "MPReal" OrElse columnName = "MF" Then
            Dim amount As String = e.FormattedValue
            If columnName = "MPReal" Then
                Chaines.forceManaging(amount, True, "", False, True, False, False, ",§.", , , , , , , 2, , , , , True, BillsView.Rows(e.RowIndex).Cells("MP").Value)
                If BillsView.Rows(e.RowIndex).Cells("MP").Value <> amount Then BillsView.Rows(e.RowIndex).Cells("BillSelected").Value = True
            ElseIf columnName = "MF" Then
                Chaines.forceManaging(amount, True, "", False, True, False, False, ",§.", , , , , , , 2, , , True, BillsView.Rows(e.RowIndex).Cells("MFMin").Value)
                Dim newPayment As Double = amount * BillsView.Rows(e.RowIndex).Cells("MFPourcent").Value - BillsView.Rows(e.RowIndex).Cells("MPDone").Value
                CType(BillsView.DataSource, DataTable).DefaultView(e.RowIndex).Row("MP") = newPayment
                CType(BillsView.DataSource, DataTable).DefaultView(e.RowIndex).Row("MPReal") = newPayment
            End If

            BillsView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = amount
            CType(BillsView.DataSource, DataTable).DefaultView(e.RowIndex).Row(columnName) = amount
        End If
    End Sub

    Private Sub BillsView_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles BillsView.CellValueChanged
        If e.ColumnIndex = 0 Then calculateTotal()
    End Sub

    Private Sub BillsView_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BillsView.CurrentCellDirtyStateChanged
        If BillsView.currentCell.ColumnIndex = 0 Then BillsView.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub Payment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BillsView.Focus()
        BillsView.Columns("MF").ReadOnly = Not currentDroitAcces(59)
    End Sub

    Private Sub BillsView_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles BillsView.DataBindingComplete
        For Each curRow As DataGridViewRow In BillsView.Rows
            curRow.Cells("MF").ReadOnly = BillsView.Columns("MF").ReadOnly OrElse curRow.Cells("IsReadOnly").Value
        Next
    End Sub

    Private Sub BillsView_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles BillsView.DataError
        e.Cancel = False
    End Sub

    Private Sub BillsView_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BillsView.KeyUp
        If e.KeyCode = Keys.Space Then
            If BillsView.currentRow.Cells("BillSelected").Value Is DBNull.Value OrElse BillsView.currentRow.Cells("BillSelected").Value = False Then
                BillsView.currentRow.Cells("BillSelected").Value = True
            Else
                BillsView.currentRow.Cells("BillSelected").Value = False
            End If
        End If
    End Sub
End Class
