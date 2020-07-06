Public Class SearchClientCommand
    Inherits System.Windows.Forms.ToolStripButton
    Implements Base.ICommand

    Public Sub New()

    End Sub

    Public Sub load() Implements Base.ICommand.load
        Me.Image = DrawingManager.resizeImage(DrawingManager.getInstance.getImage("searchClient16.gif"), 23, 23)
        Me.ImageScaling = ToolStripItemImageScaling.None
        Me.ImageAlign = ContentAlignment.MiddleCenter
        Me.Width = 23
        Me.ToolTipText = "Ouvrir un compte client"
    End Sub

    Public Sub execute(ByVal obj As Object) Implements Base.ICommand.execute
        Dim myRecherche As clientSearch = openUniqueWindow(New clientSearch())
        myRecherche.from = myMainWin.menuopen
        myRecherche.Show()
    End Sub

    Public Sub setEnability(ByVal enabled As Boolean) Implements Base.ICommand.setEnability
        If Me.Parent.Enabled Then Me.Enabled = enabled
        RaiseEvent enabilityChanged(enabled)
        Application.DoEvents()
    End Sub

    Public Overrides Function toString() As String
        Return "Ouvrir un compte client"
    End Function

    Public Event enabilityChanged(ByVal enabled As Boolean) Implements Base.ICommand.enabilityChanged

    Private Sub searchClientCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.execute(Nothing)
    End Sub
End Class
