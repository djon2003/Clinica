Public Class NewClientCommand
    Inherits System.Windows.Forms.ToolStripButton
    Implements Base.ICommand

    Public Sub New()

    End Sub

    Public Sub load() Implements Base.ICommand.load
        Me.Image = DrawingManager.getInstance.getImage("newuser23.gif")
        Me.ImageScaling = ToolStripItemImageScaling.None
        Me.ToolTipText = "Nouveau compte client"
    End Sub

    Public Sub execute(ByVal obj As Object) Implements Base.ICommand.execute
        'Droit & Accès
        If currentDroitAcces(9) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        setEnability(False)
        If obj IsNot Nothing AndAlso obj IsNot myMainWin AndAlso CType(obj, Form).MdiParent Is Nothing Then
            Dim myAddClient As New addclient()
            myAddClient.MdiParent = Nothing
            myAddClient.ShowDialog(obj)
        Else
            Dim myAddClient As addclient = openUniqueWindow(New addclient())
            myAddClient.Show()
        End If

        setEnability(True)
    End Sub

    Public Sub setEnability(ByVal enabled As Boolean) Implements Base.ICommand.setEnability
        If Me.Parent IsNot Nothing AndAlso Me.Parent.Enabled Then Me.Enabled = enabled
        RaiseEvent enabilityChanged(enabled)
        Application.DoEvents()
    End Sub

    Public Overrides Function toString() As String
        Return "Nouveau compte client"
    End Function


    Private Sub newUserCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.execute(Nothing)
    End Sub

    Public Event enabilityChanged(ByVal enabled As Boolean) Implements Base.ICommand.enabilityChanged
End Class
