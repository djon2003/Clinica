Public Class AlreadyLoggedException
    Inherits ExceptionBase

    Public Sub New(ByVal innerException As Exception)
        MyBase.New("Already logged exception", innerException)
    End Sub

End Class
