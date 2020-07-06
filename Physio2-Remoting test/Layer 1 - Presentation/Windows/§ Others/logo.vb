Option Strict Off
Option Explicit On
Friend Class logo
    Inherits System.Windows.Forms.Form


#Region "Windows Form Designer generated code "

    Public Sub New(ByVal showBackground As Boolean)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.dbNameStringWidth = Chaines.measureString(dbNameString, Me.Font).Width

        Me._showBackground = showBackground

        Try
            Me.Icon = DrawingManager.getInstance().getIcon("Clinica.ico")
        Catch ex As Exception
            'Ensure designer is functionnal
        End Try
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
    'Friend WithEvents LogoImage As System.Windows.Forms.Label
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'logo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 11)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(652, 445)
        Me.Font = New System.Drawing.Font("Lucida Console", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "logo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clinica"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private _showBackground As Boolean = False
    Private isScreen1024x768orMore As Boolean = Screen.PrimaryScreen.Bounds.Height >= 768
    Private dbNameString As String = "Connecté à la base de données """ & DBLinker.getInstance.dbName & """"
    Private dbNameStringWidth As Integer

    Public Property showBackground() As Boolean
        Get
            Return _showBackground
        End Get
        Set(ByVal value As Boolean)
            _showBackground = value
            Me.Invalidate()
        End Set
    End Property


    Private Sub logo_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        With DrawingManager.getInstance
            Dim hautGauche As Image = .getImage("haut-gauche.png")
            If hautGauche IsNot Nothing Then
                If _showBackground Then
                    e.Graphics.DrawImage(hautGauche, 0, 0)
                    Dim basDroit As Image = .getImage("bas-droit.png")
                    e.Graphics.DrawImage(basDroit, Screen.PrimaryScreen.Bounds.Width - basDroit.Width, Screen.PrimaryScreen.Bounds.Height - basDroit.Height)
                    Dim hautDroit As Image = .getImage("haut-droit.png")
                    If isScreen1024x768orMore Then e.Graphics.DrawImage(hautDroit, Screen.PrimaryScreen.Bounds.Width - hautDroit.Width, 0)
                    Dim basGauche As Image = .getImage("bas-gauche.png")
                    If isScreen1024x768orMore Then e.Graphics.DrawImage(basGauche, 0, Screen.PrimaryScreen.Bounds.Height - basGauche.Height)
                End If
                Dim milieu As Image = .getImage("milieu.png")
                e.Graphics.DrawImage(milieu, CType((Screen.PrimaryScreen.Bounds.Width - milieu.Width) / 2, Single), CType((Screen.PrimaryScreen.Bounds.Height - milieu.Height - 103) / 2, Single))

                'Draw SQL DBName connected on
                e.Graphics.DrawString(dbNameString, Me.Font, Brushes.Black, CType((Screen.PrimaryScreen.Bounds.Width - milieu.Width) / 2, Single) + milieu.Width - dbNameStringWidth, CType((Screen.PrimaryScreen.Bounds.Height - milieu.Height - 103) / 2, Single) + milieu.Height + 5)
            End If
        End With
    End Sub
End Class
