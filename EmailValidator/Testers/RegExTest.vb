Public Class RegExTest
    Implements Testable(Of String)

    Private Const defRegex As String = "^[-a-zA-Z0-9]([-.a-zA-Z0-9]|_)*@([-.a-zA-Z0-9]|_)+(\.[-.a-zA-Z0-9]+)*\." & _
    "(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$"
    Private regexExpression As String

    Public Sub New()
        Me.New(defRegex)
    End Sub

    Public Sub New(ByVal regexExpression As String)
        Me.regexExpression = regexExpression
    End Sub

    Public Function test(ByVal objToTest As String) As Boolean Implements Testable(Of String).test
        Return Text.RegularExpressions.Regex.IsMatch(objToTest, regexExpression)
    End Function
End Class
