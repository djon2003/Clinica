Module SoftwareUpdater

    Dim softwareToUpdate As String = String.Empty
    Dim extraCmds As String = String.Empty

    Private Sub parseCommandLineArgs()
        Dim skippedFirst As Boolean = False
        For Each curCmd As String In Environment.GetCommandLineArgs
            If Not skippedFirst Then
                skippedFirst = True
                Continue For
            End If

            Dim isOtherCommandToPass As Boolean = System.Text.RegularExpressions.Regex.IsMatch(curCmd, "^[^A-Z0-9].*$", Text.RegularExpressions.RegexOptions.IgnoreCase)
            If Not isOtherCommandToPass Then
                softwareToUpdate = curCmd
                softwareToUpdate = softwareToUpdate.Replace("""", "")
                softwareToUpdate = softwareToUpdate.Substring(softwareToUpdate.LastIndexOf("\") + 1)

                If softwareToUpdate.ToLower = "updater.exe" Then softwareToUpdate = ""
            Else
                extraCmds &= " """ & curCmd & """"
            End If
        Next
    End Sub

    Private Sub detectSoftwareToLaunchFromListFile()
        'Detect file to launch
        If softwareToUpdate = "" AndAlso IO.File.Exists(basePath & "updater.list") Then
            Dim errorCopying As Boolean = False
            Dim files() As String = IO.File.ReadAllLines(basePath & "updater.list")
            For i As Integer = 0 To files.Length - 1
                Dim file() As String = files(i).Split(New Char() {"|"}, 2)
                If IO.File.Exists(basePath & file(0)) AndAlso file(1).ToLower().EndsWith(".exe") Then
                    Dim launchFile As New IO.FileInfo(basePath & file(1))
                    softwareToUpdate = launchFile.Name
                    Exit For
                End If
            Next i
        End If
    End Sub

    Private Sub stopExistingProcessOfSoftwareToLaunch()
        Dim nbWaiting As Integer = 0

        Threading.Thread.Sleep(2000)

        'Kill software to start
        While UBound(Diagnostics.Process.GetProcessesByName(softwareToUpdate & ".exe")) > 0
            Threading.Thread.Sleep(500)
            nbWaiting += 1
            If nbWaiting > 10 Then
                Dim curProcesses() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcessesByName(softwareToUpdate & ".exe")
                If curProcesses IsNot Nothing AndAlso curProcesses.Length <> 0 Then curProcesses(0).Kill()
            ElseIf nbWaiting > 50 Then
                Exit Sub 'Too much buggy. Impossible to stop Clinica.exe process
            End If
        End While
    End Sub

    Private Function replaceFiles() As Boolean
        Dim errorCopying As Boolean = False
        'Look for files to copy
        If IO.File.Exists(basePath & "updater.list") Then
            Dim files() As String = IO.File.ReadAllLines(basePath & "updater.list")
            For i As Integer = 0 To files.Length - 1
                Dim file() As String = files(i).Split(New Char() {"|"}, 2)
                If IO.File.Exists(basePath & file(0)) Then
                    Try
                        IO.File.Copy(basePath & file(0), basePath & file(1), True)
                        IO.File.Delete(basePath & file(0))
                    Catch ex As Exception
                        errorCopying = True
                        addErrorLog(ex)
                    End Try
                End If
            Next i

            If errorCopying = False Then IO.File.Delete(basePath & "updater.list")
        End If

        Return errorCopying
    End Function

    Private Function getSoftwareToUpdateFullPath() As String
        Return basePath & softwareToUpdate & If(softwareToUpdate.ToLower.EndsWith(".exe"), "", ".exe")
    End Function

    Private Sub launchSoftwareUpdated()
        Dim myNewProcess As New System.Diagnostics.ProcessStartInfo()
        Dim myProcess As New Process()
        myNewProcess.Arguments = extraCmds
        myNewProcess.FileName = getSoftwareToUpdateFullPath()
        myNewProcess.UseShellExecute = True
        myNewProcess.CreateNoWindow = False
        myProcess.StartInfo = myNewProcess
        myProcess.Start()
    End Sub

    Private Sub ensureSoftwareToUpdateExists()
        'If still empty software to launch then use default Clinica
        If softwareToUpdate = "" Then softwareToUpdate = "Clinica"
        softwareToUpdate = softwareToUpdate.Trim

        If Not IO.File.Exists(basePath & softwareToUpdate) Then
            Console.WriteLine("Following software doesn't exist :")
            Console.WriteLine(getSoftwareToUpdateFullPath())
            Console.WriteLine()
            Console.WriteLine("Press any key to continue...")
            Console.ReadKey()
            End
        End If
    End Sub

    Sub main()
        Try
            'Set base path
            basePath = My.Application.Info.DirectoryPath
            If basePath.EndsWith("\") = False Then basePath &= "\"

            'Detect software to launch from command line args and stock others starting with "/" to repass the software launched
            parseCommandLineArgs()

            'Get software to launch from the list of files to replace
            detectSoftwareToLaunchFromListFile()

            'Ensure existance of software to update
            ensureSoftwareToUpdateExists()

            'Kill process of software to launch (if not already)
            stopExistingProcessOfSoftwareToLaunch()

            'Replace files from list
            replaceFiles()

            'Launch updated software
            launchSoftwareUpdated()
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

End Module
