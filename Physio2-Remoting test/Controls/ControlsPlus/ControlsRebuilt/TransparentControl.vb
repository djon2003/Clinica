Public Class TransparentControl
    Inherits Control

    Protected WithEvents myControl As Control
    Private Const ws_ex_transparent As Int32 = &H20
    Private myControlAdded As Boolean = False
    Private baseCP As CreateParams = Nothing
    Private myPaintEventArgs As PaintEventArgs = Nothing

    Public Sub New(ByRef newControl As Control)
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.UpdateStyles()
        Me.BackColor = Color.Transparent
        If Not newControl Is Nothing Then
            myControl = newControl
            myControl.Visible = True
            myControl.Location = New Point(0, 0)
        End If
    End Sub

    Protected Overrides ReadOnly Property createParams() As CreateParams
        Get
            If baseCP Is Nothing Then baseCP = MyBase.CreateParams
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or ws_ex_transparent
            Return cp
        End Get
    End Property

    Protected Overrides Sub onPaintBackground(ByVal e As PaintEventArgs)
        'Important - DO NOT DELETE
    End Sub

    Private Sub transparentControl_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter
        If myControlAdded = False And Not myControl Is Nothing Then
            myControlAdded = True
            Me.Controls.Add(myControl)
            Me.Invalidate()
        End If
    End Sub

    Private Sub myControlBox_LeavingControl(ByVal sender As Object, ByVal e As EventArgs) Handles myControl.MouseLeave
        If myControlAdded = True And Not myControl Is Nothing Then
            myControlAdded = False
            Me.Controls.Remove(myControl)
            Me.Invalidate()
            Dim controlleur As Control = Me.GetContainerControl
            controlleur.Refresh()
        End If
    End Sub
End Class
