Imports System.IO

Public Module Files


    Private openedProcesses As New Generic.List(Of Process)

    Public Sub killOpenedProcesses()
        For Each curProcess As Process In openedProcesses
            Try
                If curProcess.HasExited = False Then
                    If curProcess.CloseMainWindow() = False Then
                        curProcess.Kill()
                    End If
                End If
            Catch ex As System.InvalidOperationException
                'TODO: Find a better way than catching an error if it is possible

                'Try catch added to bypass the following error :

                'Message:
                'Aucun processus n'est associé à cet objet.

                'Exception Stack Trace :
                'à System.Diagnostics.Process.EnsureState(State state)
                'à(System.Diagnostics.Process.get_HasExited())
                'à Clinica.Fichiers.killOpenedProcesses() dans C:\Physio2-Remoting test\CommProc\Fichiers.vb:ligne 1041
            End Try
        Next
    End Sub

    Public Sub launchAProccess(ByVal filePath As String, Optional ByVal showErrMessage As Boolean = True, Optional ByVal windowStyle As Diagnostics.ProcessWindowStyle = ProcessWindowStyle.Normal, Optional ByVal commandLine As String = "", Optional ByVal priority As ProcessPriorityClass = ProcessPriorityClass.Normal, Optional ByVal wait As Boolean = False, Optional ByRef exitCode As Integer = 0, Optional ByVal closeWithSoftware As Boolean = False)
        _launchAProccess(filePath, showErrMessage, windowStyle, commandLine, priority, wait, exitCode, closeWithSoftware, False)
    End Sub

    Private Sub _launchAProccess(ByVal filePath As String, Optional ByVal showErrMessage As Boolean = True, Optional ByVal windowStyle As Diagnostics.ProcessWindowStyle = ProcessWindowStyle.Normal, Optional ByVal commandLine As String = "", Optional ByVal priority As ProcessPriorityClass = ProcessPriorityClass.Normal, Optional ByVal wait As Boolean = False, Optional ByRef exitCode As Integer = 0, Optional ByVal closeWithSoftware As Boolean = False, Optional ByVal didOnce As Boolean = False)
        Dim myProcess As New Process()
        Try
            Dim myNewProcess As New System.Diagnostics.ProcessStartInfo()
            myNewProcess.WindowStyle = windowStyle
            myNewProcess.FileName = filePath
            myNewProcess.UseShellExecute = True
            myNewProcess.CreateNoWindow = False
            myNewProcess.Arguments = commandLine
            myProcess.StartInfo = myNewProcess
            If closeWithSoftware Then openedProcesses.Add(myProcess)
            Dim started As Boolean = myProcess.Start()
            If started AndAlso myProcess.HasExited = False Then myProcess.PriorityClass = priority
            If wait Then
                While myProcess.HasExited = False
                    Threading.Thread.Sleep(100)
                End While
                exitCode = myProcess.ExitCode
            End If
        Catch exp As System.ComponentModel.Win32Exception
            If exp.NativeErrorCode = 1155 Then 'S'il s'agit d'une extension non associée à une application
                If showErrMessage Then MessageBox.Show("Le fichier à exécuter n'est pas associé à une application :" & vbCrLf & filePath, "Aucune association")
            ElseIf exp.NativeErrorCode = 2 OrElse exp.NativeErrorCode = 1726 Then 'File not found (maybe temporaly inaccessible)
                If didOnce Then
                    External.propagateErrorLog(New Exception(filePath, exp))
                    If showErrMessage Then MessageBox.Show("Impossible d'effectuer la fonction demandée, car le fichier suivant n'existe plus ou n'est pas accessible :" & vbCrLf & filePath, "Fichier inexistant", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Threading.Thread.Sleep(500) 'wait to give time to file if it exists
                    _launchAProccess(filePath, showErrMessage, windowStyle, commandLine, priority, wait, exitCode, closeWithSoftware, True)
                End If
            Else 'Si non.. erreur normale
                External.propagateErrorLog(New Exception(filePath, exp))
                If showErrMessage Then MessageBox.Show("Impossible d'exécuter le fichier. Le programme associé a retourné l'erreur suivante : " & vbCrLf & exp.Message, filePath)
            End If
        Catch exp As Exception
            External.propagateErrorLog(New Exception(filePath, exp))
            If showErrMessage Then MessageBox.Show(exp.Message, filePath)
        End Try
    End Sub

    Public Function removeItemToAList(ByVal fullPathOfList As String, ByVal entryToDel As String, Optional ByVal allOccurences As Boolean = True, Optional ByVal delFileIfEmpty As Boolean = False, Optional ByVal startsWith As Boolean = False, Optional ByVal keepWhiteLine As Boolean = True, Optional ByVal endsWith As Boolean = False) As Integer
        Dim path As String = fullPathOfList
        If IO.File.Exists(path) = False Then Return 0

        Dim newPath As String = path & ".newlist"
        Dim occ As Integer
        Dim file As New System.Text.StringBuilder()
        Dim fileStream As IO.FileStream
        Dim curLine As String = String.Empty
        Dim fileWriter As IO.StreamWriter

        Try
            fileWriter = New IO.StreamWriter(newPath)
            fileStream = IO.File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete)
            Dim fileReader As New IO.StreamReader(fileStream)

            curLine = fileReader.ReadLine()
            Dim firstLine As Boolean = True
            Dim hasContent As Boolean = False
            While curLine IsNot Nothing
                If ((Not curLine.ToUpper = entryToDel.ToUpper And startsWith = False And endsWith = False) Or (startsWith = True And endsWith = False And curLine.ToUpper.StartsWith(entryToDel.ToUpper) = False)) Or (startsWith = False And endsWith = True And curLine.ToUpper.EndsWith(entryToDel.ToUpper) = False) Or (startsWith = True And endsWith = True And curLine.ToUpper.IndexOf(entryToDel.ToUpper) < 0) Or (allOccurences = False And occ = 1) Then
                    file.Append(If(firstLine, String.Empty, vbCrLf))
                    file.Append(curLine)
                    hasContent = True
                    firstLine = False
                Else
                    If allOccurences = True Then
                        occ += 1
                    Else
                        occ = 1
                    End If
                End If

                curLine = fileReader.ReadLine()
            End While

            IO.File.Delete(path)

            If hasContent OrElse Not delFileIfEmpty Then
                fileWriter.Write(file.ToString())
                fileWriter.Flush()
            End If
        Catch ex As Exception
            External.propagateErrorLog(ex)
            Return 0
        Finally
            If fileStream IsNot Nothing Then
                fileStream.Close()
                fileStream = Nothing
            End If

            If fileWriter IsNot Nothing Then fileWriter.Close()
        End Try

        If IO.File.Exists(newPath) Then IO.File.Move(newPath, path)

        'lockFile(pathFromAppOfList)
        'File = readFile(pathFromAppOfList, , keepWhiteLine)
        'If File(0).ToUpper.StartsWith("ERROR") Then
        '    Return 0
        '    Exit Function
        'Else
        '    ReDim file2(File.GetUpperBound(0))
        '    occ = 0
        '    For i = 0 To File.GetUpperBound(0)
        '        If ((Not File(i).ToUpper = entryToDel.ToUpper And startsWith = False And endsWith = False) Or (startsWith = True And endsWith = False And File(i).ToUpper.StartsWith(entryToDel.ToUpper) = False)) Or (startsWith = False And endsWith = True And File(i).ToUpper.EndsWith(entryToDel.ToUpper) = False) Or (startsWith = True And endsWith = True And File(i).ToUpper.IndexOf(entryToDel.ToUpper) < 0) Or (allOccurences = False And occ = 1) Then
        '            file2(i - occ) = File(i)
        '        Else
        '            If allOccurences = True Then
        '                occ += 1
        '            Else
        '                occ = 1
        '            End If
        '        End If
        '    Next i

        '    If (file2.GetUpperBound(0) - occ) < 0 Then
        '        ReDim file2(0)
        '    Else
        '        ReDim Preserve file2(file2.GetUpperBound(0) - occ)
        '    End If
        'End If

        'If file2.GetUpperBound(0) = 0 And file2(0) Is Nothing And delFileIfEmpty = True Then
        '    IO.File.Delete(appPath & bar(appPath) & pathFromAppOfList)
        'Else
        '    writeFile(pathFromAppOfList, file2)
        'End If
        'unLockFile(pathFromAppOfList)

        Return occ
    End Function
End Module
