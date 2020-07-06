Public Class ErrorsImporter
    Implements IErrorImporter

    Private Shared mySelf As ErrorsImporter

    Private Sub new()
    End Sub

    Public Shared Function getInstance() As ErrorsImporter
        If mySelf Is Nothing Then mySelf = New ErrorsImporter

        Return mySelf
    End Function

    Private mailsErrors As New MailsImporter
    Private logsErrors As New LogsImporter
    Public Enum jobs As Byte
        All = 0
        Mails = 1
        Logs = 2
    End Enum
    Private _jobsToDo As Jobs = Jobs.All

    Public Property jobsToDo() As Jobs
        Get
            Return _jobsToDo
        End Get
        Set(ByVal value As Jobs)
            _jobsToDo = value
        End Set
    End Property

    Public Sub import(ByVal filename As String) Implements IErrorImporter.import
        If jobsToDo = Jobs.All OrElse jobsToDo = Jobs.Mails Then mailsErrors.import(filename)
        If jobsToDo = Jobs.All OrElse jobsToDo = Jobs.Logs Then logsErrors.import(filename)

        MsgBox("Done")
    End Sub

    Public Sub import(ByVal folder As String, ByVal recursive As Boolean) Implements IErrorImporter.import
        If jobsToDo = Jobs.All OrElse jobsToDo = Jobs.Mails Then mailsErrors.import(folder, recursive)
        If jobsToDo = Jobs.All OrElse jobsToDo = Jobs.Logs Then logsErrors.import(folder, recursive)

        MsgBox("Done")
    End Sub
End Class
