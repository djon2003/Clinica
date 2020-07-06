Public Class DomainValidator
    Implements Testable(Of String)

    Public lastResult As String = ""
    Private Shared results As New Generic.Dictionary(Of String, String)

    Public Function test(ByVal objToTest As String) As Boolean Implements Testable(Of String).test
        Dim domain As String = objToTest.Substring(objToTest.IndexOf("@") + 1)
        Dim nsResult As String = ""

        If results.ContainsKey(domain) Then
            nsResult = results(domain)
        Else
            nsResult = AskNsLookup("-q=ANY " & domain)
            results.Add(domain, nsResult)
        End If

        lastResult = nsResult

        Return nsResult.Split(New String() {vbCrLf}, StringSplitOptions.None).Length > 4
    End Function
End Class
