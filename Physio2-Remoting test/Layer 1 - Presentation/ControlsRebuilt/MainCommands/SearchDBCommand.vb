Public Class SearchDBCommand
    Inherits System.Windows.Forms.ToolStripButton
    Implements Base.ICommand

    Public Sub New()

    End Sub

    Public Sub load() Implements Base.ICommand.load
        Me.Image = DrawingManager.getInstance.getImage("SearchDB23.gif")
        Me.ImageScaling = ToolStripItemImageScaling.None
        Me.ToolTipText = "Rechercher dans la banque de données"
    End Sub

    Public Sub execute(ByVal obj As Object) Implements Base.ICommand.execute
        'Droit & Accès
        If currentDroitAcces(6) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        setEnability(False)
        Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB())
        mySearchDB.Show()
        setEnability(True)
    End Sub

    Public Sub setEnability(ByVal enabled As Boolean) Implements Base.ICommand.setEnability
        If Me.Parent.Enabled Then Me.Enabled = enabled
        RaiseEvent enabilityChanged(enabled)
        Application.DoEvents()
    End Sub

    Public Overrides Function toString() As String
        Return "Recherche dans la banque de données"
    End Function

    Public Event enabilityChanged(ByVal enabled As Boolean) Implements Base.ICommand.enabilityChanged

    Private Sub searchDBCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.execute(Nothing)
    End Sub
End Class
