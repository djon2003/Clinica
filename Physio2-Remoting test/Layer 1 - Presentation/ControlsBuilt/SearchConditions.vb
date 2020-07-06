Public Class SearchConditions
    Inherits Legend

#Region "Constructeur"

    Friend WithEvents Conditions As System.Windows.Forms.Label
    Public Sub New()
        MyBase.New()

        Me.caption = "Conditions de recherche en format SQL"
        'Add any initialization after the InitializeComponent() call
        Me.Conditions = New System.Windows.Forms.Label()
        '
        'Conditions
        '
        Me.Conditions.Location = New System.Drawing.Point(8, 16)
        Me.Conditions.Name = "Conditions"
        Me.Conditions.Size = New System.Drawing.Size(544, 272)
        Me.Conditions.Font = New Font("Arial", 10)
        Me.Conditions.TabIndex = 70
        Me.Conditions.Text = ""
        Me.Conditions.Dock = DockStyle.Fill


        Me.Controls.Add(Me.Conditions)
        AddHandler Conditions.MouseEnter, AddressOf objects_MouseEnter
        AddHandler Conditions.MouseMove, AddressOf objects_MouseMove
        AddHandler Conditions.MouseLeave, AddressOf objects_MouseLeave
    End Sub

    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            RemoveHandler Conditions.MouseEnter, AddressOf objects_MouseEnter
            RemoveHandler Conditions.MouseMove, AddressOf objects_MouseMove
            RemoveHandler Conditions.MouseLeave, AddressOf objects_MouseLeave
            Me.Conditions.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
#End Region

    Public Overrides Property text() As String
        Get
            Return Conditions.Text
        End Get
        Set(ByVal Value As String)
            Conditions.Text = Value
        End Set
    End Property
End Class
