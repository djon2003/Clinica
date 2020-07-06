Public Class FuturVisitesCommand
    Inherits System.Windows.Forms.ToolStripButton
    Implements Base.ICommand

    Public Sub New()

    End Sub

    Public Sub load() Implements Base.ICommand.load
        Me.Image = DrawingManager.getInstance.getImage("RV23.gif")
        Me.ImageScaling = ToolStripItemImageScaling.None
        Me.ToolTipText = "Activer/désactiver Rendez-vous futur(s)"
    End Sub

    Public Sub execute(ByVal obj As Object) Implements Base.ICommand.execute
        'Droit & Accès
        If currentDroitAcces(17) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        setEnability(False)
        With myMainWin
            .menuRVFutur.Checked = Not .menuRVFutur.Checked
            .RVMenu.visible = .menuRVFutur.Checked
            'If .RVMenu.Visible = False Then
            '    .RVMenu.Visible = True
            '    .menuRVFutur.Checked = True
            'Else
            '    .RVMenu.Visible = False
            '    .menuRVFutur.Checked = False
            'End If
        End With

        setEnability(True)
    End Sub

    Public Sub setEnability(ByVal enabled As Boolean) Implements Base.ICommand.setEnability
        If Me.Parent.Enabled Then Me.Enabled = enabled
        RaiseEvent enabilityChanged(enabled)
        Application.DoEvents()
    End Sub

    Public Overrides Function toString() As String
        Return "Activation Rendez-vous futur(s)"
    End Function

    Public Event enabilityChanged(ByVal enabled As Boolean) Implements Base.ICommand.enabilityChanged

    Private Sub futurVisitesCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.execute(Nothing)
    End Sub
End Class
