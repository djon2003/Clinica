Public Class FileResponse

    Private Const CANT_SELECT_FOLDER As String = "Le module n'a pas été capable de sélectionner le dossier relié à ce retour CSST. Veuillez communiquer avec CyberInternautes."

    Private _nbReports As Integer = 0
    Private lines() As String
    Private errorsLinks As ErrorsLinks
    Private isMessage As Boolean = False
    Private message As String = ""

    Public filename As String
    Public fileDate As Date

    Public Enum reportActions As Integer
        Updated = 2
        PutOnWait = 3
        RejectedLevel1 = 4
        RejectedLevel2 = 5
        CorrectedBySoftware = 9999
    End Enum

    Public Enum reportTypes As Integer
        Initial = 1
        [Step] = 2
        Final = 3
        Presence = 4
    End Enum

    Public Sub New(ByVal filename As String, ByVal data As String, ByVal errorsLinks As ErrorsLinks)
        Me.filename = filename
        Dim outputFile As String = Config.current.outputFolder & Base.addSlash(Config.current.outputFolder) & filename
        Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso8859-1")
        IO.File.WriteAllText(outputFile, data, encoding)

        data = IO.File.ReadAllText(outputFile, encoding)
        If Not data.StartsWith(CSSTBrowser.RETURN_MESSAGE_MARKING) Then
            lines = data.TrimEnd.Split(New String() {vbCrLf}, StringSplitOptions.None)
            _nbReports = Integer.Parse(lines(lines.Length - 1).Substring(12, 7))
        Else
            _nbReports = 1
            message = data
            isMessage = True
        End If
        Me.errorsLinks = errorsLinks
    End Sub

    Public Event reportAnalyzed(ByVal sender As FileResponse)
    Public results As New List(Of FileResult)
    Public nbProcessed, nbErrors As Integer

    Private Function getLineErrors(ByVal curNbLine As Integer) As CSSTResponseErrors
        Dim lineErrors As New CSSTResponseErrors()

        For i As Integer = curNbLine + 1 To lines.Length - 1
            If lines(i).StartsWith("08") = False Then Exit For

            Dim lineErrorMessage As String = lines(i).Trim.Replace("&gt;", ">").Replace("&lt;", "<")
            Dim lineErrorCode As String = lineErrorMessage.Substring(2, 8)
            lineErrorMessage = lineErrorMessage.Substring(10)

            lineErrors.Add(New CSSTResponseError(lineErrorCode, lineErrorMessage))
        Next i

        Return lineErrors
    End Function

    Public Function findResult(ByVal previousResults As List(Of FileResult), ByVal noClient As Integer, ByVal noFolder As Integer, ByVal objectSearched As String) As FileResult
        Dim prevResult As FileResult = Nothing
        For Each pResult As FileResult In previousResults
            If pResult.noClient = noClient AndAlso pResult.noFolder = noFolder AndAlso pResult.resultObject = objectSearched Then
                prevResult = pResult
                Exit For
            End If
        Next

        Return prevResult
    End Function

    Public Sub analyze(ByVal previousResults As List(Of FileResult))
        'Message (not a report return)
        If isMessage Then
            results.Add(New FileResult(False, 0, "", 0, "Message", message, "", "", "", "", Base.LIMIT_DATE, ""))
            Exit Sub
        End If

        'Normal report return
        Dim curNbRV As Integer = 0
        Dim curNbPresence As Integer = 0
        Dim curNoLine As Integer = 0
        Dim specificFilename As String = lines(0).Substring(156)
        specificFilename = specificFilename.Substring(0, specificFilename.IndexOf(" "))
        specificFilename = specificFilename.Substring(specificFilename.LastIndexOf("."))
        If specificFilename = ".cst" Then specificFilename = ""

        Dim year As Integer = lines(0).Substring(10, 4), month As Integer = lines(0).Substring(14, 2), day As Integer = lines(0).Substring(16, 2)
        Dim fileDate As New Date(year, month, day)
        Me.fileDate = fileDate
        fileDate = fileDate.AddDays(1)
        Dim filename As String = lines(1).Substring(2, 8) & specificFilename & ".ret"
        If filename <> Me.filename Then
            Dim outputFileNew As String = Config.current.outputFolder & Base.addSlash(Config.current.outputFolder) & filename
            Dim outputFileOld As String = Config.current.outputFolder & Base.addSlash(Config.current.outputFolder) & Me.filename
            If IO.File.Exists(outputFileNew) Then IO.File.Delete(outputFileNew)
            IO.File.Move(outputFileOld, outputFileNew)
            Me.filename = filename
        End If

        For Each curLine As String In lines
            If curLine.StartsWith("07") Then
                Dim lineErrors As CSSTResponseErrors = getLineErrors(curNoLine)
                Dim lineInput As New FileResponseLine(Me.filename, curLine, errorsLinks, lineErrors)
                Dim lineOutput As New FileResponseOutput()

                If lineInput.loadClientData() = False Then
                    results.Add(New FileResult(True, 0, lineInput.nam, 0, lineInput.subject, lineInput.returnMessage, filename, lineInput.nam, CsstTask.RESULT_ERROR_COLOR, "", lineInput.eventDate, lineInput.service))
                    RaiseEvent reportAnalyzed(Me)
                    curNoLine += 1
                    nbErrors += 1
                    Continue For
                End If

                'Adjust NoCsstFolder error when changed
                Dim goodClinicaFolderFromBadCsstNumber As Integer = 0
                If lineErrors.isErrorCodeExists(ErrorTypes.BadCsstNumber.CSST_ERROR_CODE) AndAlso errorsLinks.errorCSSTNumbersLinks.ContainsKey(lineInput.noCsstFolder) Then
                    goodClinicaFolderFromBadCsstNumber = errorsLinks.errorCSSTNumbersLinks(lineInput.noCsstFolder).noFolder
                End If

                'Select proper folder
                Dim verifs As FolderVerifications = lineInput.selectFolder(goodClinicaFolderFromBadCsstNumber)

                'If no folder selected because two fits the data, continue next
                If verifs Is Nothing Then
                    nbErrors += 1
                    lineOutput.isError = True
                    lineOutput.fontColor = CsstTask.RESULT_UNKNOWN_ERROR_COLOR
                    results.Add(New FileResult(lineOutput.isError, lineInput.clientFolderDS.Tables(0).Rows(0)("NoClient"), lineInput.clientFolderDS.Tables(0).Rows(0)("clientName"), 0, lineInput.subject, CANT_SELECT_FOLDER, filename, lineInput.nam, lineOutput.fontColor, "", lineInput.eventDate, lineInput.service))

                    Continue For
                End If

                'Adding a NAM links in case that we have two files (only for the incorrect one) : One with correct NAM other not correct, but in waiting status
                If lineInput.clientFolderData IsNot Nothing AndAlso File.validateNAM(lineInput.nam, lineInput.clientFolderData("clientLastName"), lineInput.clientFolderData("clientFirstName")) = False AndAlso errorsLinks.errorNAMsLinks.ContainsKey(lineInput.nam) = False Then
                    errorsLinks.errorNAMsLinks.Add(lineInput.nam, New ErrorsLinks.ErrorNAM(lineInput.clientFolderDS.Tables(0).Rows(0)("NoClient"), False))
                End If

                'Depends on if CSST passed an noCsstFolder or not
                If lineInput.ensureFolderSelection(lineOutput, verifs) = False Then
                    Dim noFolder As Integer = If(lineInput.clientFolderData IsNot Nothing, lineInput.clientFolderData("NoFolder"), 0)
                    results.Add(New FileResult(lineOutput.isError, lineInput.clientFolderDS.Tables(0).Rows(0)("NoClient"), lineInput.clientFolderDS.Tables(0).Rows(0)("clientName"), 0, lineInput.subject, lineInput.returnMessage, filename, lineInput.nam, lineOutput.fontColor, "", lineInput.eventDate, lineInput.service))
                    RaiseEvent reportAnalyzed(Me)
                    curNoLine += 1
                    nbErrors += If(lineOutput.isError, 1, 0)
                    Continue For
                End If

                'Set rv numbers & rv string
                If lineInput.curAction <> reportActions.Updated OrElse lines(curNoLine - 1).Substring(4, 9).Trim() <> lineInput.noCsstFolder Then
                    curNbRV = 0
                    curNbPresence = 0
                End If
                curNbRV += lineInput.nbRV
                curNbPresence += lineInput.nbPresences
                If lineInput.curType = reportTypes.Presence Then
                    lineInput.rvsString = If(curNbRV < 2, "le", "les " & curNbRV) & " rendez-vous avant le " & fileDate.ToString("yyyy-MM-dd")
                    lineInput.rvsString &= " dont " & If(curNbPresence = 0, "aucune", If(curNbPresence = 1, "une", curNbPresence.ToString())) & " présence" & If(curNbPresence > 1, "s", String.Empty)
                End If

                'Manage Action to apply 
                Dim addResult As Boolean = lineInput.manageAction(curNoLine, lines, lineOutput, previousResults, Me)
                If lineOutput.isError Then
                    nbErrors += 1
                Else
                    nbProcessed += 1
                End If

                'Update database + Add result for HTML report of task
                If lineInput.clientFolderData IsNot Nothing Then
                    If lineOutput.newMarking <> Params.current.markedAsUploaded Then
                        Select Case lineInput.curType
                            Case reportTypes.Presence
                                Base.DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & lineOutput.newMarking, _
                                                                   "NoVisite", "(SELECT TOP " & curNbRV & " NoVisite FROM InfoVisites WHERE NoFolder = " & lineInput.clientFolderData("NoFolder") & " AND NoStatut <> 3 AND ExternalStatus=" & Params.current.markedAsUploaded & " AND DateHeure < '" & fileDate.ToString("yyyy-MM-dd") & "' ORDER BY DateHeure)", False, "IN")
                            Case reportTypes.Initial
                                Base.DBLinker.getInstance.updateDB("FolderTextes", "ExternalStatus=" & lineOutput.newMarking, _
                                                                   "NoFolder", lineInput.clientFolderData("NoFolder") & " AND TexteTitle LIKE '" & lineInput.subject.Replace("'", "''") & "' AND ExternalStatus=" & Params.current.markedAsUploaded & " AND DateStarted < '" & fileDate.ToString("yyyy-MM-dd") & "'", False)
                            Case reportTypes.Step
                                Base.DBLinker.getInstance.updateDB("FolderTextes", "ExternalStatus=" & lineOutput.newMarking, _
                                                                   "NoFolder", lineInput.clientFolderData("NoFolder") & " AND TexteTitle LIKE '" & lineInput.subject.Replace("'", "''") & "' AND ExternalStatus=" & Params.current.markedAsUploaded & " AND DateStarted < '" & fileDate.ToString("yyyy-MM-dd") & "'", False)
                            Case reportTypes.Final
                                Base.DBLinker.getInstance.updateDB("FolderTextes", "ExternalStatus=" & lineOutput.newMarking, _
                                                                   "NoFolder", lineInput.clientFolderData("NoFolder") & " AND TexteTitle LIKE '" & lineInput.subject.Replace("'", "''") & "' AND ExternalStatus=" & Params.current.markedAsUploaded & " AND DateStarted < '" & fileDate.ToString("yyyy-MM-dd") & "'", False)
                        End Select
                    End If

                    If addResult Then results.Add(New FileResult(lineOutput.isError, lineInput.clientFolderData("NoClient"), lineInput.clientFolderData("clientName"), lineInput.clientFolderData("NoFolder"), lineInput.subject, lineInput.returnMessage, filename, lineInput.nam, lineOutput.fontColor, "", lineInput.eventDate, lineInput.service))
                ElseIf addResult Then
                    If lineInput.clientFolderDS.Tables(0).Rows.Count <> 0 Then
                        lineInput.clientFolderData = lineInput.clientFolderDS.Tables(0).Rows(0)
                        results.Add(New FileResult(lineOutput.isError, lineInput.clientFolderData("NoClient"), lineInput.clientFolderData("clientName"), 0, lineInput.subject, lineInput.returnMessage, filename, lineInput.nam, lineOutput.fontColor, "", lineInput.eventDate, lineInput.service))
                    Else
                        results.Add(New FileResult(lineOutput.isError, 0, "Client introuvable", 0, lineInput.subject, lineInput.returnMessage, filename, "", CsstTask.RESULT_ERROR_COLOR, "", lineInput.eventDate, lineInput.service))
                    End If
                End If
                RaiseEvent reportAnalyzed(Me)
            End If
            curNoLine += 1
        Next
    End Sub

    Public ReadOnly Property nbReports() As Integer
        Get
            Return _nbReports
        End Get
    End Property
End Class
