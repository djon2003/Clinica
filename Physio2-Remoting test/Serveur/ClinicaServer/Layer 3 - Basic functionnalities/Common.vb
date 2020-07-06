Module Common

    Public mainWin As New MainForm
    Private fileErrorLock As New System.Threading.Mutex()
    Private baseLibExternal As New BaseLibExternal()
    Private externalUpdatesExternal As New ExternalUpdatesExternal()


    Private Sub canRestartChanged(ByVal sender As Object, ByVal e As EventArgs)
        'Propagate canRestart property to other lib
        Select Case True
            Case TypeOf sender Is BaseLibExternal
                externalUpdatesExternal.canRestart = baseLibExternal.canRestart
            Case TypeOf sender Is ExternalUpdatesExternal
                baseLibExternal.canRestart = externalUpdatesExternal.canRestart
        End Select
    End Sub

    Public Sub main()
        AddHandler Application.ThreadException, AddressOf application_ThreadException
        AddHandler System.AppDomain.CurrentDomain.UnhandledException, AddressOf domain_UnhandledException
        If IO.File.Exists("dev.env") Then
            'If in development environment, than skip try catch so that error stop where it crashes
            mainSub()
        Else
            ' Add global error handling
            Try
                mainSub()
            Catch ex As Exception
                Loading.getInstance.Close()
                Software.doEndProcess()
                addErrorLog(ex)
            End Try
        End If
    End Sub

    Private Sub domain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        If e.ExceptionObject IsNot Nothing AndAlso TypeOf e.ExceptionObject Is Exception Then
            addErrorLog(New Exception("Domain level exception", e.ExceptionObject))
        ElseIf e.ExceptionObject IsNot Nothing Then
            addErrorLog(New Exception("Domain level exception. TypeOf e.ExceptionObject=" & e.ExceptionObject.GetType.ToString))
        Else
            addErrorLog(New Exception("Domain level exception."))
        End If
    End Sub

    Private Sub application_ThreadException(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
        If Not TypeOf e.Exception Is AlreadyLoggedException Then addErrorLog(New Exception("Application level exception", e.Exception))
    End Sub

    Private Sub mainSub()
        AddHandler externalUpdatesExternal.canRestartChanged, AddressOf canRestartChanged
        AddHandler baseLibExternal.canRestartChanged, AddressOf canRestartChanged
        Base.External.setCurrentExternal(baseLibExternal)
        CI.ProjectUpdates.ProjectUpdateLibrary.External.setCurrentExternal(externalUpdatesExternal)

        Software.getInstance().hasToConfigure = Software.getInstance().config.neverConfiged 'Configure will be remote with Clinica
        Software.getInstance().hasToConfigureOnlyMainOne = True
        Software.start()
    End Sub

    Public Function bar(ByVal chemin As String) As String
        If chemin = "" Then Return ""
        If Not Right(chemin, 1) = "\" Then Return "\"

        Return ""
    End Function

    Public Sub launchAProccess(ByVal filePath As String, Optional ByVal showErrMessage As Boolean = True, Optional ByVal windowStyle As Diagnostics.ProcessWindowStyle = ProcessWindowStyle.Normal, Optional ByVal commandLine As String = "")
        Try
            Dim myNewProcess As New System.Diagnostics.ProcessStartInfo()
            Dim myProcess As New Process()
            myNewProcess.WindowStyle = windowStyle
            myNewProcess.FileName = filePath
            myNewProcess.UseShellExecute = True
            myNewProcess.CreateNoWindow = False
            myNewProcess.Arguments = commandLine
            myProcess.StartInfo = myNewProcess
            myProcess.Start()
        Catch exp As Exception
            If showErrMessage Then MessageBox.Show(exp.Message, filePath)
        End Try
    End Sub

    Public Sub addErrorLog(ByVal errorMsg As Exception, Optional ByVal internalCount As Byte = 0)
        Dim sbError As New System.Text.StringBuilder()
        Dim baseErrorMsg As Exception = errorMsg
        Dim innerCount As Integer = 1
        Dim printingLineError As String = ""
        Dim printingLineSpacing As String = ""
        Dim writeStarted As Boolean = False
        Dim hasToRetry As Boolean = False
        Try
            sbError.AppendLine("Date :" & Date.Now.ToString)
            sbError.AppendLine("Version de Clinica : " & My.Application.Info.Version.ToString)
            sbError.AppendLine("Ordinateur : " & Environment.MachineName)
            sbError.AppendLine("Utilisateur Windows : " & Environment.UserDomainName & "/" & Environment.UserName)
            Dim strClients As String = ""
            For Each curClient As Base.TCPClient In TCPHost.getInstance.clients
                strClients &= "," & curClient.name
            Next curClient
            If strClients <> "" Then strClients = strClients.Substring(1)
            sbError.AppendLine("Utilisateurs Clinica : " & TCPHost.getInstance.countClients() & "={" & strClients & "}")
            Do
                Try
                    sbError.AppendLine(printingLineSpacing & "Error code : " & CType(errorMsg, Object).ErrorCode)
                Catch ex As Exception
                End Try
                If TypeOf (errorMsg) Is SqlClient.SqlException Then
                    sbError.AppendLine(printingLineSpacing & "Sql-Number : " & CType(errorMsg, SqlClient.SqlException).Number)
                    sbError.AppendLine(printingLineSpacing & "Sql-LineNumber : " & CType(errorMsg, SqlClient.SqlException).LineNumber)
                    sbError.AppendLine(printingLineSpacing & "Sql-Procedure : " & CType(errorMsg, SqlClient.SqlException).Procedure)
                End If

                sbError.AppendLine(printingLineSpacing & "Error type : " & errorMsg.GetType.ToString)

                Dim mysource As String = "" 'IIf(ErrorMsg.Source IsNot Nothing, vbCrLf & vbCrLf & "Source :" & vbCrLf & ErrorMsg.Source & IIf(ErrorMsg.TargetSite IsNot Nothing, " -- " & ErrorMsg.TargetSite.ToString, ""), "")
                If errorMsg.Source IsNot Nothing Then
                    mysource = vbCrLf & vbCrLf & "Source :" & vbCrLf & errorMsg.Source
                    If errorMsg.TargetSite IsNot Nothing Then mysource &= " -- " & errorMsg.TargetSite.ToString
                End If
                printingLineError = printingLineSpacing & "Message :" & vbCrLf & errorMsg.Message & mysource & vbCrLf & vbCrLf & "Exception Stack Trace :" & vbCrLf & IIf(errorMsg.StackTrace Is Nothing, "Trace not available", errorMsg.StackTrace) & vbCrLf & vbCrLf & "Environment stack trace :" & vbCrLf & System.Environment.StackTrace REM & vbCrLf & vbCrLf & "Other informations :" & vbCrLf & ErrorMsg.TargetSite.DeclaringType.ToString & vbCrLf & ErrorMsg.TargetSite.Attributes & vbCrLf & Join(ErrorMsg.TargetSite.GetCustomAttributes(True), "   ") & vbCrLf & ErrorMsg.TargetSite.Name & vbCrLf & ErrorMsg.TargetSite.GetCurrentMethod.Name
                printingLineError = printingLineError.Replace(vbCrLf, vbCrLf & printingLineSpacing)
                sbError.AppendLine(printingLineError)
                If errorMsg.Data IsNot Nothing AndAlso errorMsg.Data.Count <> 0 Then
                    sbError.AppendLine()
                    sbError.AppendLine("Data :")
                    For Each curKey As System.Collections.DictionaryEntry In errorMsg.Data
                        sbError.AppendLine("   " & curKey.Key.ToString & "=" & curKey.Value.ToString)
                    Next
                    sbError.AppendLine()
                End If
                sbError.AppendLine(printingLineSpacing & "InnerException " & innerCount & " --->")
                errorMsg = errorMsg.InnerException
                innerCount += 1
                printingLineSpacing &= "   "
            Loop While (Not errorMsg Is Nothing)
            sbError.AppendLine(printingLineSpacing & "Aucune")
            sbError.AppendLine("--------------------------------------------------------------------")

            'Writing error in errors.log file
            fileErrorLock.WaitOne()
            writeStarted = True
            Using errorFile As IO.FileStream = IO.File.Open(My.Application.Info.DirectoryPath & IIf(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & "erreurs.log", IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Read)
                Dim writeStream As New IO.StreamWriter(errorFile, System.Text.Encoding.Unicode)
                writeStream.Write(sbError.ToString)
                writeStream.Flush()
            End Using
            fileErrorLock.ReleaseMutex()
            writeStarted = False
        Catch ex As Exception
            If writeStarted Then fileErrorLock.ReleaseMutex()

            hasToRetry = True
        Finally
        End Try

        'Retry or log impossible to retry
        If hasToRetry Then
            If internalCount < 5 Then
                addErrorLog(errorMsg, internalCount + 1)
            Else
                Logger.logText("Impossible d'écrire le message d'erreur dans le fichier erreurs.log : " & errorMsg.Message, "SERVER")
            End If
        End If
    End Sub
End Module
