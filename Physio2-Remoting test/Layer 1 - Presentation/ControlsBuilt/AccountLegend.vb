Public Class AccountLegend
    Inherits Legend

    Private WithEvents _Labels_4 As System.Windows.Forms.Label
    Private WithEvents _Labels_1 As System.Windows.Forms.Label
    Private WithEvents _Labels_2 As System.Windows.Forms.Label
    Private WithEvents _Labels_3 As System.Windows.Forms.Label
    Private WithEvents plageAbsenceNM As System.Windows.Forms.Label
    Private WithEvents plageAbsenceM As System.Windows.Forms.Label
    Private WithEvents plagePresence As System.Windows.Forms.Label
    Private WithEvents plagePresencePaye As Label
    Private WithEvents plageRV As System.Windows.Forms.Label

#Region "Constructeur"
    Public Sub New()
        MyBase.New()

        InitializeComponent()

        MyBase.attachObjectsMouseEvents()

        'Position objects
        Dim pos As Integer = (Me.Width - Me.plageAbsenceM.Width - Me.plageAbsenceNM.Width - Me.plagePresence.Width - Me.plagePresencePaye.Width - Me.plageRV.Width)
        Dim espacement As Integer = pos / 6
        pos = espacement
        Me.plageRV.Left = pos
        pos += espacement + Me.plageRV.Width
        Me.plagePresence.Left = pos
        pos += espacement + Me.plagePresence.Width
        Me.plagePresencePaye.Left = pos
        pos += espacement + Me.plagePresencePaye.Width
        Me.plageAbsenceM.Left = pos
        pos += espacement + Me.plageAbsenceM.Width
        Me.plageAbsenceNM.Left = pos
    End Sub

    Public Sub InitializeComponent()
        Me.Size = New Size(560, 68)

        Me.plageAbsenceNM = New System.Windows.Forms.Label()
        Me.plageAbsenceM = New System.Windows.Forms.Label()
        Me.plagePresence = New System.Windows.Forms.Label()
        Me.plageRV = New System.Windows.Forms.Label()
        Me.plagePresencePaye = New Label

        '
        'PlageAbsenceNM
        '
        Me.plageAbsenceNM.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(128, Byte))
        Me.plageAbsenceNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageAbsenceNM.Location = New System.Drawing.Point(384, 26)
        Me.plageAbsenceNM.Name = "PlageAbsenceNM"
        Me.plageAbsenceNM.Size = New System.Drawing.Size(17, 9)
        Me.plageAbsenceNM.TabIndex = 70
        Me.plageAbsenceNM.AutoSize = True
        Me.plageAbsenceNM.Text = "Absence non motivée"
        Me.plageAbsenceNM.Font = New Font("MS sans sherif", 8)
        Me.plageAbsenceNM.TextAlign = ContentAlignment.MiddleCenter
        '
        'PlageAbsenceM
        '
        Me.plageAbsenceM.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.plageAbsenceM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageAbsenceM.Location = New System.Drawing.Point(248, 26)
        Me.plageAbsenceM.Name = "PlageAbsenceM"
        Me.plageAbsenceM.Size = New System.Drawing.Size(17, 9)
        Me.plageAbsenceM.Text = "Absence motivée"
        Me.plageAbsenceM.AutoSize = True
        Me.plageAbsenceM.TabIndex = 71
        Me.plageAbsenceM.Font = New Font("MS sans sherif", 8)
        Me.plageAbsenceM.TextAlign = ContentAlignment.MiddleCenter
        '
        'PlagePresence
        '
        Me.plagePresence.BackColor = System.Drawing.Color.White
        Me.plagePresence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plagePresence.Location = New System.Drawing.Point(152, 26)
        Me.plagePresence.Name = "PlagePresence"
        Me.plagePresence.Size = New System.Drawing.Size(17, 9)
        Me.plagePresence.TabIndex = 72
        Me.plagePresence.Text = "Présence"
        Me.plagePresence.AutoSize = True
        Me.plagePresence.Font = New Font("MS sans sherif", 8)
        Me.plagePresence.TextAlign = ContentAlignment.MiddleCenter
        '
        'PlagePresencePaye
        '
        Me.plagePresencePaye.BackColor = System.Drawing.Color.White
        Me.plagePresencePaye.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plagePresencePaye.Location = New System.Drawing.Point(162, 26)
        Me.plagePresencePaye.Name = "PlagePresence"
        Me.plagePresencePaye.Size = New System.Drawing.Size(17, 9)
        Me.plagePresencePaye.TabIndex = 72
        Me.plagePresencePaye.Text = "Présence payée"
        Me.plagePresencePaye.AutoSize = True
        Me.plagePresencePaye.Font = New Font("MS sans sherif", 8)
        Me.plagePresencePaye.TextAlign = ContentAlignment.MiddleCenter
        '
        'PlageRV
        '
        Me.plageRV.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.plageRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageRV.Location = New System.Drawing.Point(40, 26)
        Me.plageRV.Name = "PlageRV"
        Me.plageRV.Size = New System.Drawing.Size(17, 9)
        Me.plageRV.TabIndex = 73
        Me.plageRV.AutoSize = True
        Me.plageRV.Text = "Rendez-vous"
        Me.plageRV.Font = New Font("MS sans sherif", 8)
        Me.plageRV.TextAlign = ContentAlignment.MiddleCenter


        Me.Controls.Add(Me.plageRV)
        Me.Controls.Add(Me.plagePresence)
        Me.Controls.Add(Me.plageAbsenceM)
        Me.Controls.Add(Me.plageAbsenceNM)
        Me.Controls.Add(Me.plagePresencePaye)
    End Sub
#End Region

#Region "Propriétés"
    Public Property rvColor() As Color
        Get
            Return plageRV.BackColor
        End Get
        Set(ByVal Value As Color)
            plageRV.BackColor = Value
        End Set
    End Property

    Public Property presenceColor() As Color
        Get
            Return plagePresence.BackColor
        End Get
        Set(ByVal Value As Color)
            plagePresence.BackColor = Value
        End Set
    End Property

    Public Property presencePayeeColor() As Color
        Get
            Return plagePresencePaye.BackColor
        End Get
        Set(ByVal Value As Color)
            plagePresencePaye.BackColor = Value
        End Set
    End Property

    Public Property absenceMColor() As Color
        Get
            Return plageAbsenceM.BackColor
        End Get
        Set(ByVal Value As Color)
            plageAbsenceM.BackColor = Value
        End Set
    End Property

    Public Property absenceNMColor() As Color
        Get
            Return plageAbsenceNM.BackColor
        End Get
        Set(ByVal Value As Color)
            plageAbsenceNM.BackColor = Value
        End Set
    End Property
#End Region

End Class
