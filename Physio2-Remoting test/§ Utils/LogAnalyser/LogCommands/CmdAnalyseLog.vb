Namespace LogCommands
    Friend Class CmdAnalyseLog
        Inherits LogCommand

        Private filePath As String = ""

        Public Sub new(ByVal commandLine As String, ByVal params() As Object)
            MyBase.New(commandLine, params)
            If params Is Nothing OrElse params.Length = 0 Then Exit Sub
            Me.filePath = params(0)
        End Sub

        Public Overrides Sub execute()
            If commandLine.Contains("/?") Then 'Demande de l'aide
                OnExecuted("HELP ANALYSE")
                Exit Sub
            End If

            'Define condition
            Dim filter As String = ""
            If commandLine.Contains("/-filter:") Then
                filter = commandLine.Substring(commandLine.IndexOf("/-filter:") + 9)
                If filter.IndexOf("/") <> -1 Then filter = filter.Substring(0, filter.IndexOf("/"))
                If filter.IndexOf(" ") <> -1 Then filter = filter.Substring(0, filter.IndexOf(" "))
            End If
            Dim isParam_sc2 As Boolean = commandLine.EndsWith("/sC") OrElse commandLine.Contains("/sC ") OrElse commandLine.Contains("/sC/")
            Dim isParam_f As Boolean = commandLine.Contains("/f:")
            Dim isParam_sr As Boolean = commandLine.EndsWith("/sr") OrElse commandLine.Contains("/sr ") OrElse commandLine.Contains("/sr/")
            Dim isParam_sc_cj As Boolean = commandLine.EndsWith("/sc-cj") OrElse commandLine.Contains("/sc-cj ") OrElse commandLine.Contains("/sc-cj/")
            Dim isParam_op As Boolean = commandLine.EndsWith("/op") OrElse commandLine.Contains("/op ") OrElse commandLine.Contains("/op/")
            Dim isParam_sc As Boolean = isParam_sc2 OrElse commandLine.EndsWith("/sc") OrElse commandLine.Contains("/sc ") OrElse commandLine.Contains("/sc/")
            Dim isParam_c As Boolean = commandLine.EndsWith("/c") OrElse commandLine.Contains("/c ") OrElse commandLine.Contains("/c/")
            Dim isParam_cj As Boolean = commandLine.EndsWith("/cj") OrElse commandLine.Contains("/cj ") OrElse commandLine.Contains("/cj/")
            Dim isParam_showrawcounters As Boolean = commandLine.EndsWith("/showrawcounters") OrElse commandLine.Contains("/showrawcounters ") OrElse commandLine.Contains("/showrawcounters/")
            Dim isGeneralAnalyse As Boolean = commandLine.ToUpper = "ANALYSE" OrElse ((IsParam_op Or IsParam_f) AndAlso IsParam_sr = False AndAlso IsParam_sc_cj = False AndAlso IsParam_sc = False AndAlso IsParam_c = False AndAlso IsParam_cj = False)

            Dim filePaths() As String = {filePath}
            Console.Clear()

            If IO.Directory.Exists(filePath) Then filePaths = IO.Directory.GetFiles(filePath, "*.log")

            Dim messageCounter As New SortedList
            messageCounter.Add("\R", 0)
            messageCounter.Add("\S", 0)
            Dim connectionCounter As New SortedList
            Dim pingers As New ArrayList
            Dim errorLines As New ArrayList
            For f As Integer = 0 To filePaths.GetUpperBound(0)
                pingers.Clear()
                Console.WriteLine("Lecture du fichier en cours (" & filePaths(f) & ") ...")

                Dim logFile() As String = IO.File.ReadAllLines(filePaths(f))
                'Analyse data
                Dim lastSentToAll As String = ""
                Dim lastSentToAllLine As Integer = 0
                Dim lastNbReplyToAll As Integer = 0
                Dim lastNbConnected As Integer = 0
                Dim nbCurrentlyConnected As Integer = 0
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
                        'Comptage connexions/déconnexions + pingers
                        If IsGeneralAnalyse OrElse IsParam_c OrElse IsParam_cj OrElse IsParam_showrawcounters OrElse IsParam_sc OrElse IsParam_sc_cj Then
                            If splittedLine(1).StartsWith(">>") Then
                                If IsParam_op AndAlso compName.ToUpper.StartsWith("POSTE") = False Then Continue For

                                If connectionCounter.ContainsKey(splittedCommand(2) & "\C") Then
                                    connectionCounter(splittedCommand(2) & "\C") += 1
                                Else
                                    connectionCounter.Add(splittedCommand(2) & "\C", 1)
                                End If
                                If connectionCounter.ContainsKey(splittedCommand(2) & "\C-" & splittedTime(0)) Then
                                    connectionCounter(splittedCommand(2) & "\C-" & splittedTime(0)) += 1
                                Else
                                    connectionCounter.Add(splittedCommand(2) & "\C-" & splittedTime(0), 1)
                                End If
                                If connectionCounter.ContainsKey("\C-" & splittedTime(0)) Then
                                    connectionCounter("\C-" & splittedTime(0)) += 1
                                Else
                                    connectionCounter.Add("\C-" & splittedTime(0), 1)
                                End If
                                If pingers.Contains(compName) = False Then pingers.Add(compName)
                            End If
                            If IsParam_op AndAlso compName.ToUpper.StartsWith("POSTE") = False Then Continue For
                            If splittedCommand(1) = "DISCONNECTME" Then
                                If connectionCounter.ContainsKey(compName & "\D") Then
                                    connectionCounter(compName & "\D") += 1
                                Else
                                    connectionCounter.Add(compName & "\D", 1)
                                End If
                                If connectionCounter.ContainsKey(compName & "\D-" & splittedTime(0)) Then
                                    connectionCounter(compName & "\D-" & splittedTime(0)) += 1
                                Else
                                    connectionCounter.Add(compName & "\D-" & splittedTime(0), 1)
                                End If
                                If connectionCounter.ContainsKey("\D-" & splittedTime(0)) Then
                                    connectionCounter("\D-" & splittedTime(0)) += 1
                                Else
                                    connectionCounter.Add("\D-" & splittedTime(0), 1)
                                End If
                                If pingers.Contains(compName) Then pingers.Remove(compName)
                            End If
                        End If

                        'Comptage pinger
                        If logFile(i).Contains("PING") AndAlso pingers.Contains(compName) = False Then pingers.Add(compName)

                        'Détermine le nombre d'ordi connectés
                        If (pingers.Count = 0 OrElse pingers.Count <> NbCurrentlyConnected) AndAlso connectionCounter.ContainsKey("\C-" & splittedTime(0)) Then
                            NbCurrentlyConnected = connectionCounter("\C-" & splittedTime(0))
                            NbCurrentlyConnected -= connectionCounter("\D-" & splittedTime(0))
                            If NbCurrentlyConnected <> pingers.Count Then
                                If IsParam_sc2 Then errorLines.Add("Le nombre de connexions présente à " & splittedLine(0) & " a été ajusté de " & NbCurrentlyConnected & " à " & pingers.Count & " (Ligne:" & (i + 1).ToString & ")")
                                NbCurrentlyConnected = pingers.Count
                            End If
                        End If

                        'Compte le nombre de réception et d'envoi
                        If ((IsParam_op AndAlso compName.ToUpper.StartsWith("POSTE") = True) OrElse IsParam_op = False) AndAlso (filter = "" OrElse logFile(i).Contains(filter) = False) Then
                            If splittedCommand(0).Contains(">>") Then
                                messageCounter("\R") += 1
                                If messageCounter.ContainsKey("\R-" & splittedTime(0)) Then
                                    messageCounter("\R-" & splittedTime(0)) += 1
                                Else
                                    messageCounter.Add("\R-" & splittedTime(0), 1)
                                End If
                            End If
                            If splittedCommand(0).Contains("<<") Then
                                messageCounter("\S") += 1
                                If messageCounter.ContainsKey("\S-" & splittedTime(0)) Then
                                    messageCounter("\S-" & splittedTime(0)) += 1
                                Else
                                    messageCounter.Add("\S-" & splittedTime(0), 1)
                                End If
                            End If
                        End If

                        'Compare le nombre de renvoi d'un message en fonction du nombre d'ordi connectés
                        If (IsParam_sc_cj OrElse IsParam_sc) AndAlso splittedLine(1).StartsWith(">>") = False AndAlso splittedCommand(1) = "TOALL" Then
                            If splittedCommand(0).Contains(">>") Then
                                If lastSentToAll <> "" AndAlso lastNbReplyToAll <> lastNbConnected Then
                                    errorLines.Add(filePaths(f) & " " & lastSentToAllLine.ToString & ":Le serveur n'a pas envoyé cette ligne correctement (Reply=" & lastNbReplyToAll & ",Connected=" & lastNbConnected & ")")

                                    'ElseIf lastSentToAll <> "" AndAlso lastNbReplyToAll > lastNbConnected Then
                                    REM Corrige le 1er du fichier 2009.01.09.log à LEG, mais empire les autres
                                    'Le problème vient du fait que le poste003 est resté connecté toute la nuit
                                    'connectionCounter("\C-" & splittedTime(0)) += 1
                                    'connectionCounter("\C") += 1
                                End If
                                lastNbReplyToAll = 0
                                lastSentToAll = logFile(i)
                                lastSentToAllLine = i + 1
                                lastNbConnected = NbCurrentlyConnected
                            ElseIf lastSentToAll <> "" Then
                                lastNbReplyToAll += 1
                            End If
                        ElseIf splittedCommand(0).Contains(">>") Then 'Considère le renvoi terminé
                            If lastSentToAll <> "" AndAlso lastNbReplyToAll <> lastNbConnected Then errorLines.Add(filePaths(f) & " " & lastSentToAllLine.ToString & ":Le serveur n'a pas envoyé cette ligne correctement (Reply=" & lastNbReplyToAll & ",Connected=" & lastNbConnected & ")")
                            lastNbReplyToAll = 0
                            lastSentToAll = ""
                            lastSentToAllLine = 0
                            lastNbConnected = 0
                        End If
                    Catch ex As Exception
                        errorLines.Add(filePaths(f) & " " & (i + 1).ToString & ":" & logFile(i))
                    End Try
                Next i
            Next f

            Console.Clear()
            'Show analysis
            If IsGeneralAnalyse OrElse IsParam_c Then
                Console.WriteLine("Compteurs de connexion :")
                For Each curEntry As DictionaryEntry In connectionCounter
                    If curEntry.Key.ToString.EndsWith("\C") = True Then
                        Console.WriteLine(curEntry.Key.ToString.Substring(0, curEntry.Key.ToString.Length - 2) & " (Connexion/Déconnexion) = " & curEntry.Value & " / " & connectionCounter(curEntry.Key.ToString.Substring(0, curEntry.Key.ToString.Length - 1) & "D"))
                    End If
                Next curEntry
                Console.WriteLine()
            End If
            If IsGeneralAnalyse OrElse IsParam_sr Then
                Console.WriteLine("Compteurs d'envoi/réception :")
                If messageCounter.ContainsKey("\S") Then Console.WriteLine("# Envois : " & messageCounter("\S").ToString)
                If messageCounter.ContainsKey("\R") Then Console.WriteLine("# Réception : " & messageCounter("\R").ToString)
                Console.WriteLine()
            End If
            If IsParam_cj Then
                Console.WriteLine("Journées en erreur sur le nombre de connexion :")
                Dim curPoste As String = ""
                For Each curEntry As DictionaryEntry In connectionCounter
                    If curEntry.Key.ToString.IndexOf("\C-") <> -1 Then
                        Dim firstVal As Integer = curEntry.Value
                        Dim secondVal As Integer = connectionCounter(curEntry.Key.ToString.Replace("\C-", "\D-"))
                        If CurPoste = "" Then CurPoste = curEntry.Key.ToString.Substring(0, curEntry.Key.ToString.IndexOf("\C-"))
                        If FirstVal <> SecondVal Then
                            If CurPoste <> curEntry.Key.ToString.Substring(0, curEntry.Key.ToString.IndexOf("\C-")) Then
                                CurPoste = curEntry.Key.ToString.Substring(0, curEntry.Key.ToString.IndexOf("\C-"))
                                Console.WriteLine()
                                Console.WriteLine(CurPoste & " :")
                            End If

                            Console.WriteLine(curEntry.Key.ToString.Substring(curEntry.Key.ToString.IndexOf("\C-") + 3) & " (Connexion=" & FirstVal & ",Déconnexion=" & SecondVal & ")")
                        End If

                    End If
                Next curEntry
                Console.WriteLine()
            End If
            If IsParam_showrawcounters Then
                Console.WriteLine("Compteurs - ConnectionCounter :")
                For Each curEntry As DictionaryEntry In connectionCounter
                    Console.WriteLine(curEntry.Key.ToString & " = " & curEntry.Value.ToString)
                Next curEntry
                Console.WriteLine()
                Console.WriteLine("Compteurs - MessageCounter :")
                For Each curEntry As DictionaryEntry In messageCounter
                    Console.WriteLine(curEntry.Key.ToString & " = " & curEntry.Value.ToString)
                Next curEntry
                Console.WriteLine()
            End If

            If errorLines.Count <> 0 Then
                Console.WriteLine("Lignes en erreurs :")
                For i As Integer = 0 To errorLines.Count - 1
                    Dim isInConnectionErroToo As String = ""
                    If IsParam_sc_cj AndAlso errorLines(i).ToString.Contains("(Reply=") Then
                        Dim filename As String = errorLines(i).ToString.Substring(0, errorLines(i).ToString.IndexOf(".log"))
                        filename = filename.Substring(filename.LastIndexOf("\") + 1).Replace(".", "-")
                        isInConnectionErroToo = "("
                        If connectionCounter.ContainsKey("\C-" & filename) Then isInConnectionErroToo &= "C:" & connectionCounter("\C-" & filename) & ","
                        If connectionCounter.ContainsKey("\D-" & filename) Then isInConnectionErroToo &= "D:" & connectionCounter("\D-" & filename)
                        isInConnectionErroToo &= ")"
                    End If
                    Console.WriteLine(errorLines(i) & isInConnectionErroToo)
                Next i

                Console.WriteLine()
                Console.WriteLine("Nombre d'erreurs : " & errorLines.Count)
            End If

            OnExecuted(filePath)
        End Sub

        Public Overrides Function getAcceptedCommands() As String()
            Return New String() {"ANALYSE"}
        End Function

        Public Overrides Function getShortDescription() As String
            Return "Analyse les logs (Voir les options)"
        End Function

        Public Overrides Function getHelp() As String
            Return "Analyse les logs :" & vbCrLf & "ANALYSE [/c][/cj][/sc][/op][/showrawcounters]" & vbCrLf & vbCrLf & _
            "Paramètres :" _
            & vbCrLf & "/c  :  Analyse les connexions/déconnexions" _
            & vbCrLf & "/cj  :  Analyse erreurs de connexions/déconnexions" _
            & vbCrLf & "/sc  :  Analyse erreurs de relais de message via le nombre de connexion" _
            & vbCrLf & "/sC  :  Analyse erreurs de relais de message via le nombre de connexion + Changement du nombre de connexion détecté via le nombre de pingers" _
            & vbCrLf & "/sc-cj  :  Analyse /sc en lien avec /cj" _
            & vbCrLf & "/sr  :  Affiche le nombre d'envoies et de réceptions" _
            & vbCrLf & "/op  :  Tient uniquement en compte les ordinateurs ayant un nom débutant par 'POSTE'" _
            & vbCrLf & "/showrawcounters  :  Affiche les compteurs brutes"
        End Function
    End Class
End Namespace
