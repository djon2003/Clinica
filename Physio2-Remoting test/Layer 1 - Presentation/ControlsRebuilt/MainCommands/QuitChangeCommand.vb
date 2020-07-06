Public Class QuitChangeCommand
    Inherits System.Windows.Forms.ToolStripButton
    Implements Base.ICommand

    Public Sub New()

    End Sub

    Public Sub load() Implements Base.ICommand.load
        Me.Image = DrawingManager.getInstance.getImage("quitchange23.gif")
        Me.ImageScaling = ToolStripItemImageScaling.None
        Me.ToolTipText = "Changer d'utilisateur"
    End Sub

    Public Sub execute(ByVal obj As Object) Implements Base.ICommand.execute
        Software.restart()
    End Sub

    Public Sub setEnability(ByVal enabled As Boolean) Implements Base.ICommand.setEnability
        If Me.Parent.Enabled Then Me.Enabled = enabled
        RaiseEvent enabilityChanged(enabled)
        Application.DoEvents()
    End Sub

    Public Overrides Function toString() As String
        Return "Changement d'utilisateur"
    End Function

    Public Event enabilityChanged(ByVal enabled As Boolean) Implements Base.ICommand.enabilityChanged

    Private Sub quitChangeCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.execute(Nothing)
    End Sub
End Class
