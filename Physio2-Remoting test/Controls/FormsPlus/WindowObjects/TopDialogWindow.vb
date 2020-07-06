Public Class TopDialogWindow
    Inherits System.Windows.Forms.Form

    Private WithEvents window As Form
    Private isClosing As Boolean = False

    Public Sub New(ByVal window As Form)
        Me.window = window
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Height = 0
        Me.Width = 0
        Me.WindowState = FormWindowState.Minimized
        Me.Text = window.Text
    End Sub


    Private Sub tempDialogWindow_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Minimized
        window.WindowState = FormWindowState.Maximized
        window.BringToFront()
        window.Focus()
    End Sub

    Private Sub topDialogWindow_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If isClosing = False Then window.Close()
        isClosing = True
    End Sub

    Private Sub tempDialogWindow_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Me.WindowState = FormWindowState.Minimized
        window.WindowState = FormWindowState.Maximized
        window.BringToFront()
        window.Focus()
    End Sub

    Private Sub tempDialogWindow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Minimized
        window.Show()
        window.BringToFront()
        window.Focus()
    End Sub

    Private Sub tempDialogWindow_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Minimized
        window.WindowState = FormWindowState.Maximized
        window.BringToFront()
        window.Focus()
    End Sub

    Private Sub window_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles window.FormClosing
        If isClosing = False Then Me.Close()
        isClosing = True
    End Sub
End Class
