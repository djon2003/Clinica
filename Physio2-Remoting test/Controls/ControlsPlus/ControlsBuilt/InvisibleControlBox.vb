Public Class InvisibleControlBox
    Inherits System.Windows.Forms.UserControl


#Region " Windows Form Designer generated code "



    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        lockedSize = New Size(Me.Width, Me.Height)
    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents LockWindow As System.Windows.Forms.Button
    Friend WithEvents SwitchTB_Main As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Fermer As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.LockWindow = New System.Windows.Forms.Button
        Me.Fermer = New System.Windows.Forms.Button
        Me.SwitchTB_Main = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'LockWindow
        '
        Me.LockWindow.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.LockWindow.Location = New System.Drawing.Point(16, 0)
        Me.LockWindow.Name = "LockWindow"
        Me.LockWindow.Size = New System.Drawing.Size(16, 16)
        Me.LockWindow.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.LockWindow, "Empêcher le déplacement")
        '
        'Fermer
        '
        Me.Fermer.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Fermer.Location = New System.Drawing.Point(32, 0)
        Me.Fermer.Name = "Fermer"
        Me.Fermer.Size = New System.Drawing.Size(16, 16)
        Me.Fermer.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.Fermer, "Cacher")
        '
        'SwitchTB_Main
        '
        Me.SwitchTB_Main.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.SwitchTB_Main.Location = New System.Drawing.Point(0, 0)
        Me.SwitchTB_Main.Name = "SwitchTB_Main"
        Me.SwitchTB_Main.Size = New System.Drawing.Size(16, 16)
        Me.SwitchTB_Main.TabIndex = 5
        Me.SwitchTB_Main.Text = "<"
        Me.ToolTip1.SetToolTip(Me.SwitchTB_Main, "Sortir de la barre de côté")
        '
        'InvisibleControlBox
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Controls.Add(Me.SwitchTB_Main)
        Me.Controls.Add(Me.LockWindow)
        Me.Controls.Add(Me.Fermer)
        Me.DoubleBuffered = True
        Me.Name = "InvisibleControlBox"
        Me.Size = New System.Drawing.Size(47, 15)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event leavingControl()
    Public Event lockingControl(ByVal willBeLocked As Boolean)
    Public Event closingControl()
    Public Event switchingControl(ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean)

    Private willLock As Boolean = True
    Private willSwitchToToolBar As Boolean = False
    Private lockedSize As Size
    Private _unlockedImage As Bitmap
    Private _lockedImage As Bitmap


    Protected Overrides Sub onResize(ByVal e As System.EventArgs)
        If lockedSize = Nothing Then
            MyBase.OnResize(e)
        Else
            MyBase.Size = lockedSize
        End If
    End Sub

    Private Sub controls_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles LockWindow.MouseLeave, Fermer.MouseLeave, SwitchTB_Main.MouseLeave
        Me.OnMouseLeave(e)
    End Sub

    Private Sub lockWindow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LockWindow.Click
        isLocked = Not isLocked
        If isLocked Then
            ToolTip1.SetToolTip(LockWindow, "Permettre le déplacement")
        Else
            ToolTip1.SetToolTip(LockWindow, "Empêcher le déplacement")
        End If
    End Sub

#Region "Propriétés"
    Public Property closeImage() As Bitmap
        Get
            Return Fermer.Image
        End Get
        Set(ByVal value As Bitmap)
            Fermer.Image = value
        End Set
    End Property
    Public Property lockedImage() As Bitmap
        Get
            Return _lockedImage
        End Get
        Set(ByVal value As Bitmap)
            _lockedImage = value
            If isLocked Then LockWindow.Image = value
        End Set
    End Property

    Public Property unlockedImage() As Bitmap
        Get
            Return _unlockedImage
        End Get
        Set(ByVal value As Bitmap)
            _unlockedImage = value
            If isLocked = False Then LockWindow.Image = value
        End Set
    End Property

    Public Property IsSwitchedToToolbar(Optional ByVal showPanel As Boolean = True) As Boolean
        Get
            Return Not willSwitchToToolBar
        End Get
        Set(ByVal value As Boolean)
            willSwitchToToolBar = value
            switchText()
            willSwitchToToolBar = Not value
            RaiseEvent switchingControl(value, showPanel)
        End Set
    End Property

    Public Property isLocked() As Boolean
        Get
            Return willLock
        End Get
        Set(ByVal Value As Boolean)
            RaiseEvent lockingControl(Value)

            willLock = Value

            If Value = True Then
                LockWindow.Image = lockedImage
            Else
                LockWindow.Image = unlockedImage
            End If
        End Set
    End Property
#End Region

    Private Sub fermer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Fermer.Click
        RaiseEvent closingControl()
    End Sub

    Private Sub switchText()
        If willSwitchToToolBar = False Then
            Me.SwitchTB_Main.Text = ">"
            ToolTip1.SetToolTip(SwitchTB_Main, "Transférer vers la barre de côté")
        Else
            Me.SwitchTB_Main.Text = "<"
            ToolTip1.SetToolTip(SwitchTB_Main, "Sortir de la barre de côté")
        End If
    End Sub

    Private Sub switchTB_Main_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchTB_Main.Click
        switchText()

        RaiseEvent switchingControl(willSwitchToToolBar, True)

        willSwitchToToolBar = Not willSwitchToToolBar
    End Sub

    Public Sub setLockability(ByVal lockable As Boolean)
        LockWindow.Enabled = lockable
        Me.isLocked = Not lockable
    End Sub
End Class
