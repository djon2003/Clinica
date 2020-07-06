Module Common

    Public basePath As String = String.Empty

    Private Sub fillErrorLog(ByVal sbError As System.Text.StringBuilder, ByVal errorMsg As Exception, ByRef lastErrorMessage As String, Optional ByVal printingLineSpacing As String = "")
        Dim innerCount As Integer = 1
        Dim printingLineError As String = ""

        sbError.AppendLine("Date :" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        sbError.AppendLine("Version d'Updater : " & My.Application.Info.Version.ToString)
        sbError.AppendLine("Ordinateur : " & Environment.MachineName)
        sbError.AppendLine("Utilisateur Windows : " & Environment.UserDomainName & "/" & Environment.UserName)
        sbError.AppendLine("Version OS :" & Environment.OSVersion.VersionString)
        Do
            Try
                sbError.AppendLine(printingLineSpacing & "Error code : " & CType(errorMsg, Object).ErrorCode)
            Catch ex As Exception
            End Try
            If TypeOf (errorMsg) Is System.ComponentModel.Win32Exception Then
                sbError.AppendLine(printingLineSpacing & "NativeErrorCode : " & CType(errorMsg, ComponentModel.Win32Exception).NativeErrorCode)
            End If

            sbError.AppendLine(printingLineSpacing & "Error type : " & errorMsg.GetType.ToString)
            lastErrorMessage = errorMsg.Message
            Dim multiLineSubject As Integer = lastErrorMessage.IndexOf(vbCrLf)
            If multiLineSubject <> -1 Then lastErrorMessage = lastErrorMessage.Substring(0, multiLineSubject) 'Keep only first line of message
            If lastErrorMessage.Length > 500 Then lastErrorMessage = lastErrorMessage.Substring(0, 500) & "... (cropped)"

            Dim mysource As String = ""
            If errorMsg.Source IsNot Nothing Then
                mysource = vbCrLf & vbCrLf & "Source :" & vbCrLf & errorMsg.Source
                If errorMsg.TargetSite IsNot Nothing Then mysource &= " -- " & errorMsg.TargetSite.ToString
            End If

            printingLineError = printingLineSpacing & "Message :" & vbCrLf & errorMsg.Message & mysource & vbCrLf & vbCrLf & "Exception Stack Trace :" & vbCrLf & IIf(errorMsg.StackTrace Is Nothing, "Trace not available", errorMsg.StackTrace) & vbCrLf & vbCrLf & "Environment stack trace :" & vbCrLf & System.Environment.StackTrace
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

            If TypeOf (errorMsg) Is Reflection.ReflectionTypeLoadException Then
                sbError.AppendLine(printingLineSpacing & "LoaderExceptions --->")
                For Each curEx As Exception In CType(errorMsg, Reflection.ReflectionTypeLoadException).LoaderExceptions
                    fillErrorLog(sbError, curEx, lastErrorMessage, printingLineSpacing)
                Next
                sbError.AppendLine(printingLineSpacing & "LoaderExceptions <---")
            End If

            sbError.AppendLine(printingLineSpacing & "InnerException " & innerCount & " --->")
            errorMsg = errorMsg.InnerException
            innerCount += 1
            printingLineSpacing &= "   "
        Loop While (Not errorMsg Is Nothing)
        sbError.AppendLine(printingLineSpacing & "Aucune")
        sbError.AppendLine("--------------------------------------------------------------------")

    End Sub

    Public Sub addErrorLog(ByVal errorMsg As Exception, Optional ByVal internalCount As Byte = 0)
        'Start error log
        Dim writeStarted As Boolean = False
        Dim hasToRetry As Boolean = False
        Dim sbError As New Text.StringBuilder()
        Dim baseErrorMsg As Exception = errorMsg
        Dim fileNum As Integer = FreeFile()
        Dim subject As String = ""

        Try
backToTesting:
            fillErrorLog(sbError, errorMsg, subject)

            Console.WriteLine("Error : " & subject)

            'Writing error in errors.log file
            writeStarted = True
            Using errorFile As IO.FileStream = IO.File.Open(basePath & "Updater.errors.log", IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Read)
                Dim writeStream As New IO.StreamWriter(errorFile, System.Text.Encoding.Unicode)
                writeStream.Write(sbError.ToString)
                writeStream.Flush()
            End Using
            writeStarted = False
        Catch ex As Exception
            hasToRetry = True
        Finally
        End Try

        'Retry or log impossible to retry
        If hasToRetry Then
            If internalCount < 5 Then
                addErrorLog(errorMsg, internalCount + 1)
            Else
                Console.WriteLine("Impossible d'écrire le message d'erreur dans le fichier erreurs.log : " & errorMsg.Message, "Erreur")
            End If
        End If
    End Sub

End Module
