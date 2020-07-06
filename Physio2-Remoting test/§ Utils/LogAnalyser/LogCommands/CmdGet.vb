Namespace LogCommands
    Friend Class CmdGet
        Inherits LogCommand

        Private filePath As String = ""

        Public Sub new(ByVal commandLine As String, ByVal params() As Object)
            MyBase.New(commandLine, params)
            If params Is Nothing OrElse params.Length = 0 Then Exit Sub
            Me.filePath = params(0)
        End Sub

        Private Function lineHasFilter(ByVal line As String, ByVal filters As ArrayList) As Boolean
            For i As Integer = 0 To filters.Count - 1
                If line.ToUpper.Contains(filters(i).ToString.ToUpper) Then Return True
            Next i

            Return False
        End Function

        Private Sub fillFilter(ByVal filter As ArrayList, ByVal paramName As String)
            If commandLine.Contains(paramName) Then
                Dim curFound As Integer = commandLine.IndexOf(paramName, curFound)
                Dim curFilter As String = ""
                Do
                    curFilter = commandLine.Substring(CurFound + paramName.Length)
                    If curFilter.IndexOf("/") <> -1 Then curFilter = curFilter.Substring(0, curFilter.IndexOf("/"))
                    '                    If curFilter.IndexOf(" ") <> -1 Then curFilter = curFilter.Substring(0, curFilter.IndexOf(" "))
                    filter.Add(curFilter)

                    CurFound = commandLine.IndexOf(paramName, CurFound + 1)
                Loop While CurFound <> -1
            End If
        End Sub

        Public Overrides Sub execute()
            If commandLine.Contains("/?") Then 'Demande de l'aide
                OnExecuted("HELP GET")
                Exit Sub
            End If

            'Define condition
            Dim filterPlus As New ArrayList
            Dim filterMoins As New ArrayList
            fillFilter(filterPlus, "/+filter:")
            fillFilter(filterMoins, "/-filter:")
            Dim isParam_f As Boolean = commandLine.Contains("/f:")
            Dim isParam_op As Boolean = commandLine.EndsWith("/op") OrElse commandLine.Contains("/op ") OrElse commandLine.Contains("/op/")
            Dim isParam_c As Boolean = commandLine.EndsWith("/c") OrElse commandLine.Contains("/c ") OrElse commandLine.Contains("/c/")
            Dim isGeneralExtract As Boolean = commandLine.ToUpper = "EXTRACT" OrElse ((IsParam_op Or IsParam_f) AndAlso IsParam_c = False)

            Dim filePaths() As String = {filePath}
            Console.Clear()

            If IO.Directory.Exists(filePath) Then filePaths = IO.Directory.GetFiles(filePath, "*.log")

            Dim messages As New ArrayList
            Dim errorLines As New ArrayList
            For f As Integer = 0 To filePaths.GetUpperBound(0)
                Console.WriteLine("Lecture du fichier en cours (" & filePaths(f) & ") ...")

                Dim logFile() As String = IO.File.ReadAllLines(filePaths(f))
                'Extract data
                For i As Integer = 0 To logFile.GetUpperBound(0)
                    Try
                        Dim splittedLine() As String = logFile(i).Split(New Char() {"|"}, 2)
                        splittedLine(0) = splittedLine(0).Trim
                        Dim splittedTime() As String = splittedLine(0).Split(New Char() {" "})
                        splittedLine(1) = splittedLine(1).Trim
                        Dim splittedCommand() As String = splittedLine(1).Split(New Char() {"|"})

                        Dim compName As String = ""
                        If splittedLine(1).StartsWith(">>") Then
                            compName = splittedCommand(2)
                        ElseIf splittedCommand(0).StartsWith("<<") = False Then
                            compName = splittedCommand(0).Substring(0, splittedCommand(0).IndexOf(" "))
                        End If
                        If IsParam_op AndAlso compName.ToUpper.StartsWith("POSTE") = False Then Continue For

                        If IsGeneralExtract OrElse IsParam_c Then
                            If splittedLine(1).StartsWith(">>") Then messages.Add(filePaths(f) & " " & (i + 1).ToString & ":" & splittedLine(1) & "\C")
                            If splittedCommand(1) = "DISCONNECTME" Then messages.Add(filePaths(f) & " " & (i + 1).ToString & ":" & splittedLine(1) & "\D")
                        End If

                    Catch ex As Exception
                        errorLines.Add(filePaths(f) & " " & (i + 1).ToString & ":" & logFile(i))
                    End Try
                Next i
            Next f

            Console.Clear()
            'Show analysis
            If IsGeneralExtract OrElse IsParam_c Then
                Console.WriteLine("Lignes de connexion :")
                For Each curEntry As String In messages
                    If (curEntry.EndsWith("\C") = True Or curEntry.EndsWith("\D") = True) AndAlso (filterPlus.Count = 0 OrElse Me.LineHasFilter(curEntry, filterPlus) = True) AndAlso (filterMoins.Count = 0 OrElse LineHasFilter(curEntry, filterMoins) = False) Then
                        Console.WriteLine(curEntry.Substring(0, curEntry.Length - 2))
                    End If
                Next curEntry
                Console.WriteLine()
            End If

            If errorLines.Count <> 0 Then
                Console.WriteLine("Lignes en erreurs :")
                For i As Integer = 0 To errorLines.Count - 1
                    Console.WriteLine(errorLines(i))
                Next i
            End If

            OnExecuted(filePath)
        End Sub

        Public Overrides Function getAcceptedCommands() As String()
            Return New String() {"GET", "EXTRACT"}
        End Function

        Public Overrides Function getShortDescription() As String
            Return "Extrait des lignes spéciques des logs"
        End Function

        Public Overrides Function getHelp() As String
            Return "Extrait des lignes spéciques des logs:" & vbCrLf & "EXTRACT [/c][/op]([/+filter:FILTER1][/+filter:FILTER2]...)([/-filter:FILTER1][/-filter:FILTER2]...)" & vbCrLf & vbCrLf & _
            "Paramètres :" _
            & vbCrLf & "/c  :  Extrait les connexions/déconnexions" _
            & vbCrLf & "/op  :  Tient uniquement en compte les ordinateurs ayant un nom débutant par 'POSTE'" _
            & vbCrLf & "/+filter:FILTER  :  Affiche uniquement les lignes contenant FILTER" _
            & vbCrLf & "/-filter:FILTER  :  N'affiche pas les lignes contenant FILTER"
        End Function
    End Class
End Namespace
