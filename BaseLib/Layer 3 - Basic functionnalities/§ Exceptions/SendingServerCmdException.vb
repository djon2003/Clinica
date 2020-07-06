Public Class SendingServerCmdException
    Inherits ExceptionBase

    Public Sub New(ByVal cmdSent As String, ByVal answerAwait As String)
        MyBase.New("cmdSent:" & cmdSent & vbCrLf & "answerAwait:" & answerAwait)
    End Sub

    Public Sub New(ByVal cmdSent As String, ByVal answerAwait As String, ByVal innerException As Exception)
        MyBase.New("cmdSent:" & cmdSent & vbCrLf & "answerAwait:" & answerAwait, innerException)
    End Sub
End Class
