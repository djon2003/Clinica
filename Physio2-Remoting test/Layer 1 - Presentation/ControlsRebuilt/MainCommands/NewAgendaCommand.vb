Public Class NewAgendaCommand
    Inherits System.Windows.Forms.ToolStripButton
    Implements Base.ICommand

    Private noTRP As Integer = 0

    Public Sub New()

    End Sub

    Public Sub load() Implements Base.ICommand.load
        Me.Image = DrawingManager.getInstance.getImage("agenda23.gif")
        Me.ImageScaling = ToolStripItemImageScaling.None
        Me.ToolTipText = "Nouvelle agenda"
    End Sub

    Public Sub execute(ByVal obj As Object) Implements Base.ICommand.execute
        Me.noTRP = Integer.Parse(obj.ToString)

        'Droit & Accès
        If currentDroitAcces(0) = False And noTRP <> ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        setEnability(False)
        Dim no As Integer
        Dim myTRPName As String = UsersManager.getInstance.getUser(noTRP).toString

        Dim newAgenda As New Agenda()
        Dim myNewAgenda As Agenda = openUniqueWindow(newAgenda, "Agenda : " & myTRPName)
        If newAgenda.GetHashCode.Equals(myNewAgenda.GetHashCode) Then
            myNewAgenda.Text = "Agenda : " & myTRPName
            myNewAgenda.SuspendLayout()

            no = Date.Today.DayOfWeek * -1
            Select Case PreferencesManager.getUserPreferences()("FirstDay")
                Case "Lundi"
                    no += 1
                Case "Mardi"
                    no += 2
                Case "Mercredi"
                    no += 3
                Case "Jeudi"
                    no += 4
                Case "Vendredi"
                    no += 5
                Case "Samedi"
                    no += 6
                Case Else
                    no = 0
            End Select
            myNewAgenda.DebutDate = Date.Today.AddDays(no)

            myNewAgenda.agendaLoaded = True
            myNewAgenda.updateStructure(noTRP)
            myNewAgenda.ResumeLayout()
        End If

        If myNewAgenda.IsDisposed = False Then myNewAgenda.Show()

        setEnability(True)
    End Sub

    Public Sub setEnability(ByVal enabled As Boolean) Implements Base.ICommand.setEnability
        If Me.Parent.Enabled Then Me.Enabled = enabled
        RaiseEvent enabilityChanged(enabled)
        Application.DoEvents()
    End Sub

    Public Overrides Function toString() As String
        Return "Nouvelle agenda"
    End Function

    Public Event enabilityChanged(ByVal enabled As Boolean) Implements Base.ICommand.enabilityChanged

    Private Sub newAgendaCommand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        myMainWin.newAgenda()
    End Sub
End Class
