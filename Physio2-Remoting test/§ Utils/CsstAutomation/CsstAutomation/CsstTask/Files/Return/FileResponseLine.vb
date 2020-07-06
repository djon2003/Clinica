Public Class FileResponseLine

    Private Const SQL_FOLDERS_FROM As String = "FolderDates IF1 INNER JOIN InfoClients IC ON IC.NoClient = IF1.NoClient LEFT JOIN KeyPeople KP ON KP.NoKP = IF1.NoKP"
    Private Shared SQL_FOLDERS_FIELDS As String = "IC.Nom AS clientLastName, IC.Prenom AS clientFirstName, IC.Nom + ',' + IC.Prenom as clientName,IF1.Service,IF1.NoFolder,IC.NoClient, IF1.NoRef, IF1.DateAccident, IF1.NoKP as MedecinNo, KP.NoRef AS MedecinRef, IF1.DateRef, IF1.DateReceptionRef, " & _
                                                 "FirstTraitement AS FirstTreatment," & _
                                                 "FirstEval AS Evaluation," & _
                                                 "DateRef AS FolderOrdonnanceDate," & _
                                                 "ClosingDate AS FolderClosingDate," & _
                                                 "(SELECT TOP 1 U.Nom + ',' + U.Prenom + ' (' + CAST(U.NoUser AS VARCHAR(MAX)) + ') - ' + U.NoPermis FROM Utilisateurs U INNER JOIN Titres T ON T.NoTitre = U.NoTitre WHERE (T.Titre LIKE 'physio%' OR T.Titre LIKE 'ergo%') AND U.NoUser IN (SELECT NoTRP FROM InfoVisites IV WHERE IV.NoFolder = IF1.NoFolder)) AS TRP," & _
                                                 "(SELECT TOP 1 U.NoUser FROM Utilisateurs U INNER JOIN Titres T ON T.NoTitre = U.NoTitre WHERE (T.Titre LIKE 'physio%' OR T.Titre LIKE 'ergo%') AND U.NoUser IN (SELECT NoTRP FROM InfoVisites IV WHERE IV.NoFolder = IF1.NoFolder)) AS TRPNo," & _
                                                 "(SELECT TOP 1 U.NoPermis FROM Utilisateurs U INNER JOIN Titres T ON T.NoTitre = U.NoTitre WHERE (T.Titre LIKE 'physio%' OR T.Titre LIKE 'ergo%') AND U.NoUser IN (SELECT NoTRP FROM InfoVisites IV WHERE IV.NoFolder = IF1.NoFolder)) AS TRPNoPermit," & _
                                                 "(SELECT COUNT(*) FROM FolderTextes FT WHERE FT.NoFolder = IF1.NoFolder AND FT.ExternalStatus = " & Params.current.markedAsUploaded & ") AS NbTextSent," & _
                                                 "(SELECT COUNT(*) FROM InfoVisites IV WHERE IV.NoFolder = IF1.NoFolder AND IV.ExternalStatus = " & Params.current.markedAsUploaded & ") AS NbRVSent"

    Private Const SQL_ACCOUNT_FROM As String = "InfoClients"
    Private Const SQL_ACCOUNT_FIELDS As String = "NAM"


    Public curAction As FileResponse.reportActions
    Public curType As FileResponse.reportTypes
    Public noCsstFolder As String = ""
    Public eventDate As Date
    Public nam As String
    Public returnMessage As String = String.Empty
    Public subject As String = String.Empty
    Public service As String
    Public fullServiceName As String
    Public nbRV As Integer = 0
    Public nbPresences As Integer = 0
    Public rvsString As String = String.Empty


    Public clientFolderData As DataRow
    Public clientFolderDS As DataSet

    Private fileName As String
    Private fileClientLastName As String
    Private fileClientFirstName As String
    Public errorsLinks As ErrorsLinks
    Public lineErrors As CSSTResponseErrors


    Public Sub New(ByVal fileName As String, ByVal curLine As String, ByVal errorsLinks As ErrorsLinks, ByVal lineErrors As CSSTResponseErrors)
        Me.errorsLinks = errorsLinks
        Me.lineErrors = lineErrors
        Me.fileName = fileName

        curAction = curLine.Substring(2, 1)
        curType = curLine.Substring(3, 1)
        If curLine.Substring(4, 9).Trim <> "" Then noCsstFolder = curLine.Substring(4, 9)

        Dim Year As Integer = Integer.Parse(curLine.Substring(34, 4))
        Dim Month As Integer = Integer.Parse(curLine.Substring(38, 2))
        Dim Day As Integer = Integer.Parse(curLine.Substring(40, 2))
        eventDate = New Date(Year, Month, Day)
        nam = curLine.Substring(13, 12)
        service = curLine.Substring(82, 1)
        fullServiceName = If(service = "P", "Physiothérapie", "Ergothérapie")

        If curType = FileResponse.reportTypes.Presence AndAlso curLine.Substring(133, 2).Trim() <> String.Empty Then nbRV = curLine.Substring(133, 2).Trim()
        If curType = FileResponse.reportTypes.Presence AndAlso curLine.Substring(135, 2).Trim() <> String.Empty Then nbPresences = curLine.Substring(135, 2).Trim()

        fileClientLastName = curLine.Substring(83, 30).Trim
        fileClientFirstName = curLine.Substring(113, 20).Trim
        fileClientLastName = fileClientLastName.Substring(0, 1) & fileClientLastName.Substring(1).ToLower()
        fileClientFirstName = fileClientFirstName.Substring(0, 1) & fileClientFirstName.Substring(1).ToLower()


        'TODO : Changed hardcoded text title to be in params
        Select Case curType
            Case FileResponse.reportTypes.Initial
                subject = "Rapport CSST initial"
            Case FileResponse.reportTypes.Step
                subject = "Rapport CSST d'étape "
                If curLine.Length > 135 Then subject &= curLine.Substring(135).Trim()
            Case FileResponse.reportTypes.Final
                subject = "Rapport CSST final"
            Case FileResponse.reportTypes.Presence
                subject = "Présences"
        End Select
    End Sub


    Public Function loadClientData() As Boolean
        'Look for client folders
        clientFolderDS = Base.DBLinker.getInstance.readDBForGrid(SQL_FOLDERS_FROM, SQL_FOLDERS_FIELDS, "WHERE IC.NAM = '" & nam & "'")
        If clientFolderDS Is Nothing OrElse clientFolderDS.Tables.Count = 0 OrElse clientFolderDS.Tables(0).Rows.Count = 0 Then
            'TODO : Delete TODO ... Old condition : (lineErrors.isErrorCodeExists(ErrorTypes.BadNAM.MAIN_ERROR_CODE) OrElse lineErrors.isErrorCodeExists(ErrorTypes.BadNAM.SECONDARY_ERROR_CODE)) AndAlso 
            If errorsLinks.errorNAMsLinks.ContainsKey(nam) Then
                Dim currentDBNam As String = Base.DBLinker.getInstance().readScalar(SQL_ACCOUNT_FROM, SQL_ACCOUNT_FIELDS, "WHERE NoClient=" & errorsLinks.errorNAMsLinks(nam).noClient)

                If currentDBNam <> nam Then
                    'If the NAM was in error, and then corrected into software
                    errorsLinks.errorNAMsLinks(nam).isUsed = True
                    If lineErrors.isErrorCodeExists(ErrorTypes.BadNAM.MAIN_ERROR_CODE) Then
                        lineErrors.setErrorMessage(ErrorTypes.BadNAM.MAIN_ERROR_CODE, "NAM a été corrigé.")
                    Else
                        lineErrors.setErrorMessage(ErrorTypes.BadNAM.SECONDARY_ERROR_CODE, "NAM a été corrigé.")
                    End If
                    curAction = FileResponse.reportActions.CorrectedBySoftware
                End If

                clientFolderDS = Base.DBLinker.getInstance.readDBForGrid(SQL_FOLDERS_FROM, SQL_FOLDERS_FIELDS, "WHERE IC.NoClient = " & errorsLinks.errorNAMsLinks(nam).noClient)
            Else
                'If NAM wasn't linked previously
                returnMessage = "Impossible de trouver le compte client de " & fileClientLastName & "," & fileClientFirstName & " ayant pour NAM," & nam & ", et contenant le dossier ayant pour numéro de référence : " & noCsstFolder & ". Veuillez remettre le NAM erroné, exécuter de nouveau le module. Si le module indique que le NAM est erroné, là remettez le NAM valide et exécutez encore le module."
                If curAction = FileResponse.reportActions.RejectedLevel1 Or curAction = FileResponse.reportActions.RejectedLevel2 Then
                    returnMessage &= " :<br/>" & lineErrors.getReturnMessage()
                End If

                Return False
            End If
        End If

        Return True
    End Function

    Public Function selectFolder(ByVal goodClinicaFolderFromBadCsstNumber As Integer) As FolderVerifications
        Return selectFolder(goodClinicaFolderFromBadCsstNumber, 0)
    End Function

    Private Function selectFolder(ByVal goodClinicaFolderFromBadCsstNumber As Integer, ByVal goodFolderNumber As Integer) As FolderVerifications
        'Ensure folder exists or choose needed one (when initial)
        Dim initVerification, errorPeriodToAnotherClinicVerification, errorBadEventDateVerification As Boolean
        Dim foldersAndVerifs As New Generic.List(Of FolderVerifications)
        For i As Integer = clientFolderDS.Tables(0).Rows.Count - 1 To 0 Step -1
            Dim curRow As DataRow = clientFolderDS.Tables(0).Rows(i)

            Dim verif As New FolderVerifications
            verif.errorBadEventDateVerification = True
            verif.initVerification = curType = FileResponse.reportTypes.Initial
            verif.errorPeriodToAnotherClinicVerification = False
            verif.clientFolderData = curRow

            If goodFolderNumber <> 0 AndAlso curRow("NoFolder") = goodFolderNumber Then
                foldersAndVerifs.Add(verif)
                Continue For
            End If

            Dim sameServiceAndEventDate As Boolean = curRow("Service").ToString().StartsWith(service) AndAlso curRow("DateAccident") IsNot DBNull.Value AndAlso CDate(curRow("DateAccident")).Date.Equals(eventDate.Date)
            Dim noRefVerification As Boolean = sameServiceAndEventDate AndAlso curRow("NoRef") IsNot DBNull.Value AndAlso curRow("NoRef") <> "" AndAlso curRow("NoRef") = noCsstFolder
            initVerification = sameServiceAndEventDate AndAlso curType = FileResponse.reportTypes.Initial

            errorPeriodToAnotherClinicVerification = (curType = FileResponse.reportTypes.Initial OrElse curType = FileResponse.reportTypes.Presence) AndAlso lineErrors.isErrorCodeExists(ErrorTypes.PeriodToAnotherClinic.CSST_ERROR_CODE) AndAlso errorsLinks.errorPeriodToAnotherClinicsLinks.ContainsKey(nam) AndAlso errorsLinks.errorPeriodToAnotherClinicsLinks(nam).noFolder = curRow("NoFolder")
            errorBadEventDateVerification = (curType = FileResponse.reportTypes.Initial OrElse curType = FileResponse.reportTypes.Presence) AndAlso errorsLinks.errorEventDatesLinks.ContainsKey(curRow("NoFolder")) AndAlso errorsLinks.errorEventDatesLinks(curRow("NoFolder")).oldDate.Date = eventDate.Date

            verif.errorBadEventDateVerification = errorBadEventDateVerification
            verif.initVerification = initVerification
            verif.errorPeriodToAnotherClinicVerification = errorPeriodToAnotherClinicVerification
            verif.clientFolderData = curRow

            Dim curNoRef As String = System.Text.RegularExpressions.Regex.Replace(curRow("NoRef"), "[^0-9]", String.Empty)
            'This verification has to be before the next one
            If goodClinicaFolderFromBadCsstNumber <> 0 AndAlso goodClinicaFolderFromBadCsstNumber = curRow("NoFolder") Then
                If curNoRef <> noCsstFolder Then
                    lineErrors.setErrorMessage(ErrorTypes.BadCsstNumber.CSST_ERROR_CODE, "Numéro de référence du dossier a été corrigé.")
                    curAction = FileResponse.reportActions.CorrectedBySoftware
                End If

                'This line is apart, because the error message could be there twice and the second one appears as UNKNOWN otherwise
                foldersAndVerifs.Add(verif)
                Continue For
            End If

            If errorBadEventDateVerification OrElse errorPeriodToAnotherClinicVerification OrElse initVerification OrElse noRefVerification Then
                foldersAndVerifs.Add(verif)
            End If
        Next

        'Select proper folder (if there is many) : The one which has something sent
        Dim selectedVerif As FolderVerifications = Nothing
        If foldersAndVerifs.Count = 1 Then
            selectedVerif = foldersAndVerifs(0)
        ElseIf foldersAndVerifs.Count = 0 Then
            'Choose folder from save file
            Dim savedFile As New FileSave(Me.fileName)
            For Each curFR As FileResult In savedFile.fileResults
                If curFR.nam = nam AndAlso curFR.service.StartsWith(service) AndAlso curFR.refDate = eventDate Then
                    Return selectFolder(goodClinicaFolderFromBadCsstNumber, curFR.noFolder)
                End If
            Next
        Else
            For Each current As FolderVerifications In foldersAndVerifs
                Dim nbTextSent As Integer = current.clientFolderData("NbTextSent")
                Dim nbRVSent As Integer = current.clientFolderData("NbRVSent")
                If nbTextSent <> 0 Or nbRVSent <> 0 Then
                    selectedVerif = current
                    Exit For
                End If
            Next
        End If

        If selectedVerif IsNot Nothing Then clientFolderData = selectedVerif.clientFolderData

        Return selectedVerif
    End Function


    Public Function ensureFolderSelection(ByVal lineOutput As FileResponseOutput, ByVal verifs As FolderVerifications) As Boolean
        If noCsstFolder <> "" Then
            If clientFolderData Is Nothing Then
                returnMessage = "Impossible de trouver le dossier ayant pour numéro de référence et pour date d'événement (date d'accident) : " & noCsstFolder & " / " & eventDate.ToString("yyyy/MM/dd") & ". Veuillez inscrire ce numéro dans le dossier approprié de ce compte client et vous assurez que la date d'événement (date d'accident) est celle indiquée à la phrase précédente."
                lineOutput.fontColor = CsstTask.RESULT_ERROR_COLOR
                lineOutput.isError = True

                Return False
            End If
            Dim isWrongCsstNumber As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.BadCsstNumber.CSST_ERROR_CODE)
            If Not isWrongCsstNumber AndAlso verifs.initVerification AndAlso (clientFolderData("NoRef") Is DBNull.Value OrElse clientFolderData("NoRef") = "") Then
                'Set NoCsstFolder into database
                Base.DBLinker.getInstance.updateDB("InfoFolders", "NoRef='" & noCsstFolder & "'", "NoFolder", clientFolderData("NoFolder"), False)
            End If
        Else
            Dim notBadEventError As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.BadEventDate.CSST_ERROR_CODE) = False AndAlso lineErrors.isErrorCodeExists(ErrorTypes.BadEventDate2.CSST_ERROR_CODE_MAIN) = False
            Dim notPeriodToAnotherClinicError As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.PeriodToAnotherClinic.CSST_ERROR_CODE) = False
            Dim notNAMError As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.BadNAM.MAIN_ERROR_CODE) = False AndAlso lineErrors.isErrorCodeExists(ErrorTypes.BadNAM.SECONDARY_ERROR_CODE) = False
            Dim notRefusedError As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.ReclamationRefusedForLesion.CSST_ERROR_CODE) = False
            Dim notTreatmentBeforeEvalError As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.FirstTreatmentBeforeEval.CSST_ERROR_CODE) = False AndAlso clientFolderData IsNot Nothing
            Dim notInitialManuallyEnteredError As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.Initial_ManuallyEntered.CSST_ERROR_CODE) = False AndAlso clientFolderData IsNot Nothing
            Dim notFolderInactiveError As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.FolderInactive.MAIN_INACTIVE) = False AndAlso lineErrors.isErrorCodeExists(ErrorTypes.FolderInactive.SECOND_INACTIVE) = False AndAlso clientFolderData IsNot Nothing
            Dim notFinalAlreadyExists As Boolean = lineErrors.isErrorCodeExists(ErrorTypes.Final_AlreadyExists.CSST_ERROR_CODE) = False AndAlso clientFolderData IsNot Nothing

            Dim noManagedErrorsFound As Boolean = notFinalAlreadyExists AndAlso notFolderInactiveError AndAlso notInitialManuallyEnteredError AndAlso notTreatmentBeforeEvalError AndAlso notRefusedError AndAlso notNAMError AndAlso notPeriodToAnotherClinicError AndAlso notBadEventError

            If noManagedErrorsFound AndAlso curAction <> FileResponse.reportActions.CorrectedBySoftware Then
                If curAction = FileResponse.reportActions.PutOnWait Then
                    returnMessage = "En attente de traitement par la CSST"
                    lineOutput.fontColor = CsstTask.RESULT_INFO_COLOR
                Else
                    returnMessage = "Le numéro de référence du dossier dans le retour CSST est vide"
                    If curAction = FileResponse.reportActions.RejectedLevel1 Or curAction = FileResponse.reportActions.RejectedLevel2 Then
                        returnMessage &= " :<br/>" & lineErrors.getReturnMessage()
                    End If

                    lineOutput.isError = True
                    lineOutput.fontColor = CsstTask.RESULT_UNKNOWN_ERROR_COLOR
                End If

                Return False
            End If
        End If

        Return True
    End Function

    Public Function manageAction(ByRef curNoLine As Integer, ByRef lines() As String, ByVal lineOutput As FileResponseOutput, ByVal previousResults As List(Of FileResult), ByVal fileResponse As FileResponse) As Boolean
        Dim addResult As Boolean = True

        Select Case curAction
            Case FileResponse.reportActions.Updated
                'Ensure client wasn't split because period overlap more than one month, if yes, then sum RV and continue to next
                If curType = fileResponse.reportTypes.Presence AndAlso lines(curNoLine + 1).Substring(4, 9).Trim() = noCsstFolder Then
                    addResult = False
                    Exit Select
                End If

                returnMessage = "Confirmé(e) par la CSST"
                If curType = FileResponse.reportTypes.Presence Then returnMessage &= " pour " & rvsString

                If lineErrors.Count <> 0 Then
                    returnMessage &= " :<br/><font color=" & CsstTask.RESULT_INFO_COLOR & ">" & lineErrors.getReturnMessage() & "</font>"
                End If
                lineOutput.newMarking = Params.current.markedAsConfirmed

            Case FileResponse.reportActions.PutOnWait
                returnMessage = "En attente de traitement par la CSST"
                lineOutput.fontColor = CsstTask.RESULT_INFO_COLOR

                If curType = fileResponse.reportTypes.Initial Then
                    If clientFolderData Is Nothing Then
                        lineOutput.fontColor = CsstTask.RESULT_ERROR_COLOR
                        lineOutput.isError = True
                        lineOutput.newMarking = Params.current.markedAsUploaded 'Ensure no marking change
                        returnMessage = "Le dossier CSST ayant pour date d'accident le " & eventDate.ToString("yyyy/MM/dd") & " et ayant pour service """ & fullServiceName & """ n'existe pas. Veuillez vous assurer de l'existence d'un tel dossier."
                    Else
                        Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsWaiting, "NoFolder", clientFolderData("NoFolder"), False)
                    End If
                End If

            Case FileResponse.reportActions.CorrectedBySoftware
                returnMessage = lineErrors.getReturnMessage()
                lineOutput.newMarking = Params.current.markedAsNotProcessed
                lineOutput.fontColor = CsstTask.RESULT_INFO_COLOR

            Case FileResponse.reportActions.RejectedLevel1, FileResponse.reportActions.RejectedLevel2
                lineOutput.newMarking = Params.current.markedAsConfirmed

                Dim errorInput As New ErrorInput(Me, lineOutput, previousResults, fileResponse)

                'Manage all error types
                For Each curError As CSSTResponseError In lineErrors
                    Dim errorProcessor As ErrorTypes.ErrorType = ErrorTypes.ErrorType.createErrorType(curError)

                    If errorProcessor IsNot Nothing Then
                        errorProcessor.manageError(errorInput)
                    Else
                        'Error type not managed
                        If lineOutput.newMarking > Params.current.markedAsNotProcessed Then lineOutput.newMarking = Params.current.markedAsNotProcessed

                        lineOutput.isError = True
                        lineOutput.fontColor = CsstTask.RESULT_UNKNOWN_ERROR_COLOR
                        Base.External.propagateErrorLog(New Exception("CSST-UNKNOWN-ERROR : " & errorInput.fileResponse.filename & " : " & curError.errorCode & " : " & curError.errorMessage))
                    End If

                    curError.errorColor = lineOutput.fontColor
                Next

                returnMessage = lineErrors.getReturnMessage()
                addResult = errorInput.shallAddResult
        End Select

        If lineOutput.newMarking = Params.current.markedAsConfirmed AndAlso curType = FileResponse.reportTypes.Initial Then
            Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsConfirmed, "NoFolder", clientFolderData("NoFolder"), False)
        End If

        Return addResult
    End Function
End Class
