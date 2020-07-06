Module Main

    Public Sub Main()
        Dim fileToWatch As String = String.Empty
        For i As Integer = 1 To Environment.GetCommandLineArgs.Length - 1
            fileToWatch &= " " & Environment.GetCommandLineArgs(i)
        Next i
        fileToWatch = fileToWatch.Trim()

        If fileToWatch = String.Empty OrElse Not IO.File.Exists(fileToWatch) Then
            MessageBox.Show("Execution of this software requires to pass it the filepath of the software to watch as parameter", "Starting impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName).Length > 1 Then
            MessageBox.Show("This software can only be executed once", "Starting impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim fileToStart As String = fileToWatch
        fileToWatch = fileToWatch.Substring(fileToWatch.LastIndexOf("\") + 1)
        fileToWatch = fileToWatch.Substring(0, fileToWatch.LastIndexOf("."))

        While True
            If Diagnostics.Process.GetProcessesByName(fileToWatch).Length = 0 Then
                Process.Start(fileToStart)
            End If
            Threading.Thread.Sleep(1000)
        End While
    End Sub

End Module
