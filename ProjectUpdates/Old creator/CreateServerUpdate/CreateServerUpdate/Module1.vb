Module Module1

    Private Const test_ascii_http_url As String = "http://www.cints.net/clinica/updates/test.ascii"
    Private Const test_ascii_ftp_url As String = "ftp://ftp.cyberinternautes.net/www/clinica/updates/test.ascii"
    Private ftp_credential As New Net.NetworkCredential("cyber2", "AzertyMDP751486")

    Sub main()
        Dim action As String = ""
        If My.Application.CommandLineArgs.Contains("/clean") Then action = "clean"
        If My.Application.CommandLineArgs.Contains("/restore") Then action = "restore"

        Select Case action
            Case "clean"
                Clean()
            Case "restore"
                Restore()
            Case Else
                Create()
        End Select

        If My.Application.CommandLineArgs.Contains("/noend") = False Then Console.ReadLine()
    End Sub

    Private Sub restore()

    End Sub

    Private Sub clean()
        'Clean server new folder
        For Each curFile As String In IO.Directory.GetFiles("Updates\server2\new")
            If curFile.EndsWith("sqlupdates.sql") Then
                IO.File.WriteAllText(curFile, "", Text.Encoding.GetEncoding(1252))
            Else
                IO.File.Delete(curFile)
            End If
        Next
        For Each curDir As String In IO.Directory.GetDirectories("Updates\server2\new")
            My.Computer.FileSystem.DeleteDirectory(curDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
        Next
    End Sub

    Private Sub create()
        'Ajust current.version file
        Dim startingAddress As String = "http://clinica.cints.net/updates/"
        Dim wc As New System.Net.WebClient()
        Dim curUpdateVer As String = wc.DownloadString(StartingAddress & "current.version")
        curUpdateVer += 1

        Dim newDir As String = "Updates\server2\new"
        Dim updateDir As String = "Updates\server2\" & curUpdateVer

        Dim nbNewFiles As Integer = 0
        If IO.Directory.Exists(newDir) Then nbNewFiles = IO.Directory.GetFiles(newDir, "*.*", IO.SearchOption.AllDirectories).Length

        If nbNewFiles <= 1 Then
            Dim sqlUpdates As String = ""
            If IO.File.Exists(newDir & "\sqlupdates.sql") Then
                sqlUpdates = IO.File.ReadAllText(newDir & "\sqlupdates.sql")
            Else
                If nbNewFiles = 1 Then sqlUpdates = "fake"
            End If

            If sqlUpdates = "" Then
                Console.WriteLine("Aucune mise à jour côté serveur")
                If My.Application.CommandLineArgs.Count = 0 Then Console.ReadLine()
                wc.Dispose()
                Exit Sub
            End If
        End If

        If IO.Directory.Exists(updateDir) Then My.Computer.FileSystem.DeleteDirectory(updateDir, FileIO.DeleteDirectoryOption.DeleteAllContents)

        IO.File.WriteAllText("Updates\current.version", curUpdateVer)
        IO.Directory.CreateDirectory(updateDir)

        Dim filesList As String = ""
        For Each curFile As String In IO.Directory.GetFiles(newDir, "*.*", IO.SearchOption.AllDirectories)
            Dim fs As String = ""
            If curFile.EndsWith("sqlupdates.sql") Then
                Console.WriteLine("Testing file size of sqlupdates.sql over web")

                FtpUploadFile(TEST_ASCII_FTP_URL, curFile, FTP_CREDENTIAL, False)

                Dim asciiFileContent As String = wc.DownloadString(TEST_ASCII_HTTP_URL)
                IO.File.WriteAllText("Updates\test.ascii", asciiFileContent, System.Text.Encoding.GetEncoding(1252))
                Dim fileSize As New IO.FileInfo("Updates\test.ascii")
                fs = fileSize.Length

                FtpDeleteFile(TEST_ASCII_FTP_URL, FTP_CREDENTIAL)

                fileSize.Delete()
                fileSize = Nothing

                If fs = 0 Then Continue For
            Else
                Dim fileSize As New IO.FileInfo(curFile)
                fs = fileSize.Length
            End If

            Dim newfilePath As String = updateDir & curFile.Substring(newDir.Length)
            IO.Directory.CreateDirectory(newfilePath.Substring(0, newfilePath.LastIndexOf("\")))
            IO.File.Copy(curFile, newfilePath)

            Dim relFile As String = curFile.Substring(curFile.IndexOf(newDir) + newDir.Length + 1)
            filesList &= "§§" & relFile.Replace("\", "/") & "§" & relFile & "§" & fs & "§False§" & IIf(relFile = "sqlupdates.sql", "ASCII", "Binary")
        Next

        filesList = filesList.Substring(2).Replace("§§", vbCrLf)
        IO.File.WriteAllText(updateDir & "\files.list", filesList, System.Text.Encoding.GetEncoding(1252))

        Console.WriteLine("Server update #" & curUpdateVer & " has been generated")

        wc.Dispose()
    End Sub

    Private Sub ftpDeleteFile(ByVal ftpUrl As String, ByVal netCredential As Net.NetworkCredential)
        Try
            Console.Write("Deleting " & ftpUrl & " ... ")
            Dim clsRequest As System.Net.FtpWebRequest = _
                DirectCast(System.Net.WebRequest.Create(ftpUrl), System.Net.FtpWebRequest)
            clsRequest.Credentials = netCredential
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.DeleteFile
            clsRequest.KeepAlive = False

            clsRequest.GetResponse()

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


End Module
