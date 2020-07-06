Public Class CommandsHolder
    Private Shared mySelf As CommandsHolder

    Private myNewAgendaCommand As New NewAgendaCommand()
    Private mySearchClientCommand As New SearchClientCommand()
    Private mySearchDBCommand As New SearchDBCommand()
    Private myFuturVisitesCommand As New FuturVisitesCommand()
    Private myQueueListCommand As New QueueListCommand()
    Private myRapportCommand As New ReportCommand()
    Private myQuitChangeCommand As New QuitChangeCommand()
    Private myNewClientCommand As New NewClientCommand()

    Private Sub New()

    End Sub

    Public Shared Function getInstance() As CommandsHolder
        If mySelf Is Nothing Then mySelf = New CommandsHolder

        Return mySelf
    End Function

    Public Function newAgenda() As NewAgendaCommand
        myNewAgendaCommand.load()
        Return myNewAgendaCommand
    End Function

    Public Function newClient() As NewClientCommand
        myNewClientCommand.load()
        Return myNewClientCommand
    End Function

    Public Function searchDB() As SearchDBCommand
        mySearchDBCommand.load()
        Return mySearchDBCommand
    End Function

    Public Function searchClient() As SearchClientCommand
        mySearchClientCommand.load()
        Return mySearchClientCommand
    End Function

    Public Function futurVisites() As FuturVisitesCommand
        myFuturVisitesCommand.load()
        Return myFuturVisitesCommand
    End Function

    Public Function queueList() As QueueListCommand
        myQueueListCommand.load()
        Return myQueueListCommand
    End Function

    Public Function rapport() As ReportCommand
        myRapportCommand.load()
        Return myRapportCommand
    End Function

    Public Function quitChange() As QuitChangeCommand
        myQuitChangeCommand.load()
        Return myQuitChangeCommand
    End Function
End Class
