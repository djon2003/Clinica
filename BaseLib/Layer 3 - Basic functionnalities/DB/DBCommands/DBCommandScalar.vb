Public Class DBCommandScalar
    Inherits DBCommand

    Private _return As Object

    Public ReadOnly Property [return]() As Object
        Get
            Return _return
        End Get
    End Property

    Public Overrides Sub execute(ByVal cmd As System.Data.SqlClient.SqlCommand)
        _return = cmd.ExecuteScalar()
    End Sub
End Class
