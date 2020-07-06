Public Module Common
    Public Enum ExternalUpdateActions As Integer
        cleanClient = 1
        isServerOff = 2
        getVersion = 3
        getMajorUpdate = 4
        uploadFile = 5
        upgradeVersion = 6
        setServerOff = 7
        getVersionFiles = 8
        downloadFile = 9
    End Enum

    Public Enum ExternalUpdateUserTypes As Integer
        Administrator = 1
        User = 2
    End Enum

    Public Enum FileActions As Integer
        Add = 0
        Delete = 1
    End Enum

    Friend Function isConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("http://www.cints.net/")
        Dim objWebReq As System.Net.WebRequest
        'Dim objResp As System.Net.WebResponse = Nothing
        Try
            objWebReq = System.Net.WebRequest.Create(objUrl)
            ' Attempt to get response and return True
            Using objResp As System.Net.WebResponse = objWebReq.GetResponse
            End Using
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False
            objWebReq = Nothing
            Return False
        End Try
    End Function

    Friend Function addSlash(ByVal data As String, Optional ByVal useBack As Boolean = True) As String
        Dim charToTest As Char = If(useBack, "\", "/")

        Return If(data = "" OrElse data.EndsWith(charToTest), "", charToTest)
    End Function
End Module
