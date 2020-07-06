Public Class ReportCommand
    Inherits System.Windows.Forms.ToolStripButton
    Implements Base.ICommand

    Public Sub New()

    End Sub

    Public Sub load() Implements Base.ICommand.load
        Me.Image = DrawingManager.getInstance.getImage("rapports23.gif")
        Me.ImageScaling = ToolStripItemImageScaling.None
        Me.ToolTipText = "Rapports"
    End Sub

    Public Sub execute(ByVal obj As Object) Implements Base.ICommand.execute
        setEnability(False)
        openRapport()

        setEnability(True)
    End Sub

    Public Sub setEnability(ByVal enabled As Boolean) Implements Base.ICommand.setEnability
        If Me.Parent.Enabled Then Me.Enabled = enabled
        RaiseEvent enabilityChanged(enabled)
        Application.DoEvents()
    End Sub

    Public Overrides Function toString() As String
        Return "Rapports"
    End Function

    Public Event enabilityChanged(ByVal enabled As Boolean) Implements Base.ICommand.enabilityChanged

    Private Sub rapportCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.execute(Nothing)
    End Sub
End Class
