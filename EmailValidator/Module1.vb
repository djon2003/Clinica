Imports System.Management

Module Module1

    Private Function miGetParentProcessId(ByVal processId As Integer) As Integer

        Dim loQuery As SelectQuery = New SelectQuery(String.Format("select * from Win32_Process where ProcessId = {0}", processId))
        Dim loSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(loQuery)
        Dim loProcesses As ManagementObjectCollection = loSearcher.Get()

        If loProcesses.Count > 0 Then
            Return loProcesses(0)("ParentProcessId")
        End If
        Return 0
    End Function

    Public Function IsConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("http://www.google.com/")
        Dim objWebReq As System.Net.WebRequest
        Dim objResp As System.Net.WebResponse = Nothing
        Try
            objWebReq = System.Net.WebRequest.Create(objUrl)
            ' Attempt to get response and return True
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False
            If objResp IsNot Nothing Then objResp.Close()
            objWebReq = Nothing
            Return False
        End Try
    End Function

    Sub Main()
        Dim parentProcessId As Integer = miGetParentProcessId(Process.GetCurrentProcess().Id)
        Dim parentProcess As Process = Process.GetProcessById(parentProcessId)
        Dim isFromOwnSoftwareActivation As Boolean = parentProcess.MainModule.ModuleName.ToLower().IndexOf("clinica") <> -1 OrElse parentProcess.MainModule.FileVersionInfo.CompanyName.ToLower().IndexOf("cyberinternaut") <> -1

        If (System.Environment.CommandLine.IndexOf("/f:") = -1 AndAlso System.Environment.CommandLine.IndexOf("/e:") = -1) Then

            Console.WriteLine("Veuillez utiliser le logiciel de la façon suivante :")
            Console.WriteLine("EmailValidator.exe [from@from.com] /e:test@test.com")
            Console.WriteLine("EmailValidator.exe [from@from.com] /f:FILEPATH")
            Console.WriteLine()
            Console.WriteLine("[from@from.com] : Optionel. Addresse de courriel de provenance")
            Console.WriteLine("FILEPATH : Chemin d'un fichier qui contient une adresse de courriel par ligne")
            Console.WriteLine("FILEPATH : Si le chemin contient des espaces, veuillez ajouter des guillements comme ceci : ""/f:FILEPATH""")
            If Not isFromOwnSoftwareActivation Then
                Console.WriteLine()
                Console.WriteLine("Appuyez sur une touche pour continuer")
                Console.ReadKey()
            End If

            Exit Sub
        End If

        Try
            'Get parameters
            Dim fromAddress As String = String.Empty
            Dim toAddress As String = String.Empty
            Dim file As String = String.Empty
            For i As Integer = 0 To My.Application.CommandLineArgs.Count - 1
                If My.Application.CommandLineArgs(i).StartsWith("/e:", StringComparison.OrdinalIgnoreCase) Then
                    toAddress = My.Application.CommandLineArgs(i).Substring(3).Trim()
                ElseIf My.Application.CommandLineArgs(i).StartsWith("/f:", StringComparison.OrdinalIgnoreCase) Then
                    file = My.Application.CommandLineArgs(i).Substring(3).Trim()
                Else
                    fromAddress = My.Application.CommandLineArgs(i)
                End If
            Next i

            'Test email(s)
            Dim isValid As Boolean
            Dim hasInternet As Boolean = IsConnectionAvailable()
            Dim out As New Text.StringBuilder()
            out.AppendLine("EmailValidator :")
            Dim emailValid As New EmailValidator(fromAddress, hasInternet)

            If toAddress <> String.Empty Then
                isValid = emailValid.test(toAddress)
                out.AppendLine(toAddress & " >> " & IIf(isValid, "is valid" & IIf(hasInternet, "", " without internet validations"), "is not valid at level " & emailValid.lastLevelReached))
            ElseIf file <> String.Empty Then
                emailValid.testFile(file)
                out.AppendLine(file & " >> tested")
            End If

            'Console.Clear()
            Console.WriteLine()
            Console.WriteLine(out.ToString)
            If Not isFromOwnSoftwareActivation Then
                Console.WriteLine()
                Console.WriteLine("Appuyez sur une touche pour continuer")
                Console.ReadKey()
            End If

            Environment.ExitCode = If(isValid, 0, emailValid.lastLevelReached)
        Catch ex As Exception
            Environment.ExitCode = 0
        End Try
    End Sub

End Module
