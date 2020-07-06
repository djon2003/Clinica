Public Class TCPConnectionFailedException
    Inherits ExceptionBase

    Public Sub New(ByVal innerException As Exception)
        MyBase.New("", innerException)
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

End Class
