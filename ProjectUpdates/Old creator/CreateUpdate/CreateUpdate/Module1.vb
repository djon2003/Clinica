Module Module1

    Sub main()
        If My.Application.CommandLineArgs.Contains("/onlyupload") = False Then
            'Create client & server updates
            Console.WriteLine("Creating client update...")
            Shell("$$$ CreateClientUpdate.exe /noend", AppWinStyle.NormalFocus, True)

            Console.Clear()
            Console.WriteLine("Creating server update...")
            Shell("$$$ CreateServerUpdate.exe /noend", AppWinStyle.NormalFocus, True)
        End If

        'Look if update is necessary
        Dim startingAddress As String = "http://clinica.cints.net/updates/"
        Dim wc As New System.Net.WebClient()
        Dim curUpdateVer As String = wc.DownloadString(StartingAddress & "current.version")
        wc.Dispose()
        Dim curVer As String = IO.File.ReadAllText("Updates\current.version")
        If curVer = curUpdateVer Then
            Console.Clear()
            Console.WriteLine("Il n'est pas nécessaire de créer une nouvelle mise à jour.")
            Console.ReadLine()
            Exit Sub
        End If

        'Ask if update should be upload
        Console.Clear()
        Console.WriteLine("Mise à jour prête à être téléverser")

        Console.WriteLine()
        Console.Write("Désirez-vous téléverser la mise à jour (O/N) ? ")
        Dim uploadAnswer As String = Console.ReadLine()
        If uploadAnswer = "" OrElse (uploadAnswer.ToLower.Substring(0, 1) <> "o" AndAlso uploadAnswer.ToLower.Substring(0, 1) <> "y") Then Exit Sub

        Dim curUploadVer As String = uploading() 'Upload both

        'Cleaning client & server updates
        Console.WriteLine("Cleaning client update...")
        Shell("$$$ CreateClientUpdate.exe /noend /clean", AppWinStyle.NormalFocus, True)

        Console.WriteLine("Cleaning server update...")
        Shell("$$$ CreateServerUpdate.exe /noend /clean", AppWinStyle.NormalFocus, True)

        IO.File.WriteAllText("current.version", curUploadVer)
        IO.File.WriteAllText("C:\ClinicaData\Data\current.version", curUploadVer)

        Console.WriteLine()
        Console.WriteLine("Update #" & curUploadVer & " uploaded")
        Console.ReadLine()
    End Sub

    Private Function uploading() As String
        Dim estimatedOffTime As Date = Date.Now.AddHours(2)
        IO.File.WriteAllText("Updates\current.off", estimatedOffTime.ToString("yyyy/MM/dd hh:mm"))

        Dim baseUpdateUrl As String = "ftp://ftp.cyberinternautes.net/www/ci.ca/clinica/updates/"
        Dim netCredential As New Net.NetworkCredential("cyber2", "AzertyMDP751486")
        FtpUploadFile(baseUpdateUrl & "current.off", "Updates\current.off", netCredential, False)

        Dim curUploadVer As String = IO.File.ReadAllText("Updates\current.version")

        FtpCreateDirectory(baseUpdateUrl & "client2/", netCredential)
        FtpCreateDirectory(baseUpdateUrl & "server2/", netCredential)

        uploadOneSide(baseUpdateUrl & "client2/" & curUploadVer & "/", "Updates\client2\" & curUploadVer, netCredential)
        uploadOneSide(baseUpdateUrl & "server2/" & curUploadVer & "/", "Updates\server2\" & curUploadVer, netCredential)

        FtpUploadFile(baseUpdateUrl & "current.version", "Updates\current.version", netCredential, False)

        IO.File.WriteAllText("Updates\current.off", "")
        FtpUploadFile(baseUpdateUrl & "current.off", "Updates\current.off", netCredential, False)

        Return curUploadVer
    End Function

    Private Sub uploadOneSide(ByVal baseUpdateUrl As String, ByVal updatePath As String, ByVal netCredential As Net.NetworkCredential)
        baseUpdateUrl &= IIf(baseUpdateUrl.EndsWith("/"), "", "/")
        updatePath &= IIf(updatePath.EndsWith("\"), "", "\")

        If IO.File.Exists(updatePath & "files.list") = False Then Exit Sub

        FtpCreateDirectory(baseUpdateUrl, netCredential)
        Dim filesList() As String = IO.File.ReadAllLines(updatePath & "files.list", System.Text.Encoding.Default)
        For i As Integer = 0 To filesList.Length - 1
            Dim curFile() As String = filesList(i).Split(New Char() {"§"})
            If curFile(0).IndexOf("/") <> -1 Then 'Create upper dirs
                Dim upperDirs() As String = curFile(0).Split(New Char() {"/"})
                Dim curDir As String = ""
                For j As Integer = 0 To upperDirs.GetUpperBound(0) - 1
                    curDir &= upperDirs(j) & "/"
                    FtpCreateDirectory(baseUpdateUrl & curDir, netCredential)
                Next j
            End If
            FtpUploadFile(baseUpdateUrl & curFile(0), updatePath & curFile(1), netCredential, curFile(4).ToUpper = "BINARY")
        Next

        FtpUploadFile(baseUpdateUrl & "files.list", updatePath & "files.list", netCredential, False)
    End Sub

    Private Sub ftpDeleteFile(ByVal ftpUrl As String, ByVal netCredential As Net.NetworkCredential)
        Try
            Console.Write("Deleting " & ftpUrl & " ... ")
            Dim clsRequest As System.Net.FtpWebRequest = _
                DirectCast(System.Net.WebRequest.Create(ftpUrl), System.Net.FtpWebRequest)
            clsRequest.Credentials = netCredential
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.DeleteFile
            clsRequest.KeepAlive = False

            Console.WriteLine("Done.")
        Catch ex As Net.WebException
            Console.WriteLine("Failed.")
            ex = ex
        End Try
    End Sub

    Private Sub ftpUploadFile(ByVal ftpUrl As String, ByVal filePath As String, ByVal netCredential As Net.NetworkCredential, Optional ByVal useBinary As Boolean = True)
        Try
            Console.Write("Uploading " & ftpUrl & " ... ")
            Dim clsRequest As System.Net.FtpWebRequest = _
                DirectCast(System.Net.WebRequest.Create(ftpUrl), System.Net.FtpWebRequest)
            clsRequest.Credentials = netCredential
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            clsRequest.UseBinary = UseBinary
            clsRequest.KeepAlive = True

            ' read in file...
            Dim bFile() As Byte
            If UseBinary Then
                bFile = System.IO.File.ReadAllBytes(filePath)
            Else
                Dim fileContent As String = IO.File.ReadAllText(filePath, Text.Encoding.Default)
                bFile = Text.Encoding.Default.GetBytes(fileContent)
            End If

            clsRequest.ContentLength = bFile.Length

            ' upload file...
            Dim clsStream As System.IO.Stream = _
                clsRequest.GetRequestStream()
            clsStream.WriteTimeout = 600000
            Dim bLength As Integer
            For i As Integer = 0 To bFile.Length Step 1000
                bLength = bFile.Length - i
                If bLength > 1000 Then bLength = 1000
                clsStream.Write(bFile, i, bLength)
                clsStream.Flush()
            Next
            'clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()
            Console.WriteLine("Done.")
        Catch ex As Net.WebException
            Console.WriteLine("Failed.")
            ex = ex
        End Try
    End Sub

    Private Sub ftpCreateDirectory(ByVal ftpUrl As String, ByVal netCredential As Net.NetworkCredential)
        Dim req As System.Net.FtpWebRequest = Nothing
        Dim resp As System.Net.FtpWebResponse = Nothing
        Dim directoryName As String = ftpUrl
        Dim createDirectory As Boolean = False

        ' Create the FTPWebRequest to create the remote directory
        req = DirectCast(System.Net.FtpWebRequest.Create(directoryName), System.Net.FtpWebRequest)
        req.Method = System.Net.WebRequestMethods.Ftp.ListDirectory

        ' UserName & Password
        req.Credentials = netCredential

        Try
            ' Get the Response
            resp = DirectCast(req.GetResponse(), System.Net.FtpWebResponse)
        Catch ex As System.Net.WebException
            ' An error number of 550 means the directory doesn't exist
            If ex.Message.Contains("550") Then
                createDirectory = True
            Else
                ' This exception was expected, so throw it
                Throw ex
            End If
        Finally
            ' Make sure all streams are closed
            If resp IsNot Nothing Then
                resp.GetResponseStream().Close()
                resp.Close()
            End If
        End Try


        If createDirectory Then
            ' Create the FTPWebRequest to create the remote directory
            req = DirectCast(System.Net.FtpWebRequest.Create(directoryName), System.Net.FtpWebRequest)
            req.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory

            ' UserName & Password
            req.Credentials = netCredential

            ' Get the Response, and make sure all streams are closed
            Try
                resp = DirectCast(req.GetResponse(), System.Net.FtpWebResponse)
            Finally
                ' Make sure all streams are closed
                If resp IsNot Nothing Then
                    resp.GetResponseStream().Close()
                    resp.Close()
                End If
            End Try
        End If
    End Sub

End Module
