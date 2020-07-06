Public Class CSSTResponseError

    Public errorCode As String = ""
    Public errorMessage As String = ""
    Public errorColor As String = String.Empty

    Public Sub New(ByVal errorCode As String, ByVal errorMessage As String)
        Me.errorCode = errorCode.Trim()
        Me.errorMessage = errorMessage
    End Sub

    Public Overrides Function ToString() As String
        Return errorCode
    End Function
End Class
