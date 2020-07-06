Public Class MXVerificator
    Implements Testable(Of String)

    Private Const MxSearching As String = "mail exchanger = "
    Private from As String = "validatetest@advancedintellect.com" '"info@google.ca"

    Public nsResult As String = ""
    Private triedMxNsLookup As Boolean = False

    Public Sub New(ByVal from As String)
        If from <> "" Then Me.from = [from]
    End Sub

    Public Function test(ByVal objToTest As String) As Boolean Implements Testable(Of String).test
        Dim domain As String = objToTest.Substring(objToTest.IndexOf("@") + 1)
        Dim nsResult As String = Me.nsResult
        If nsResult = "" Then
            triedMxNsLookup = True
            nsResult = AskNsLookup("-q=MX " & domain)
        End If

        Dim server As String = ""
        Dim mailExchangerPos As Integer = nsResult.IndexOf(MxSearching)
        If mailExchangerPos = -1 AndAlso Not triedMxNsLookup Then 'In case DNS lookup failed getting the MX
            Me.nsResult = String.Empty
            Return test(objToTest)
        End If

        While mailExchangerPos <> -1
            Try
                mailExchangerPos += MxSearching.Length
                server = nsResult.Substring(mailExchangerPos, nsResult.IndexOf(vbCrLf, mailExchangerPos) - mailExchangerPos)
                Dim isValid As Boolean = askMxServer(server, objToTest)
                If isValid = False Then Return False
                If isValid AndAlso MVP.notAccessibleServers.Contains(server) = False Then Return True
            Catch
                If MVP.notAccessibleServers.Contains(server) = False Then MVP.notAccessibleServers.Add(server)
            End Try

            mailExchangerPos = nsResult.IndexOf(MxSearching, mailExchangerPos + 1)
        End While

        Return True
    End Function

    Private Function askMxServer(ByVal server As String, ByVal email As String) As Boolean
        Dim myMVP As New MVP(server, 25)
        Dim validation As Boolean = myMVP.validateEmail(email, from)

        myMVP.Deconnection()

        Return validation
    End Function
End Class
