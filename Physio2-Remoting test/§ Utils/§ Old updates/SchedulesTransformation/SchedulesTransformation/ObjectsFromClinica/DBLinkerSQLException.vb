Public Class DBLinkerSQLException
    Inherits ExceptionBase

    Public Sub New(ByVal message As String, ByVal innerException As SqlClient.SqlException)
        MyBase.new(message, innerException)
    End Sub

End Class
