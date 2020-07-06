Public Class DBLinkerException
    Inherits ExceptionBase


    Public Sub New(ByVal message As String)
        MyBase.new(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.new(message, innerException)
    End Sub

End Class
