Namespace ErrorTypes

    Public Class PeriodToAnotherClinic
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10247E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            If input.in.curType = FileResponse.reportTypes.Initial Then
                If input.in.clientFolderData("Evaluation") Is DBNull.Value Then
                    csstError.errorMessage = "Il n'existe pas d'évaluation pour ce dossier. Veuillez changer le type du premier rendez-vous pour ""Évaluation""."
                    input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                Else
                    Dim dateRef As Date = input.in.clientFolderData("DateRef")
                    Dim dateReceptionRef As Date = input.in.clientFolderData("DateReceptionRef")
                    Dim evaluationDate As Date = input.in.clientFolderData("Evaluation")

                    If dateRef.Date.Equals(evaluationDate.Date) AndAlso dateReceptionRef.Date.Equals(evaluationDate.Date) AndAlso Not input.in.errorsLinks.errorPeriodToAnotherClinicsLinks.ContainsKey(input.in.nam) Then
                        csstError.errorMessage &= ". Veuillez contacter la CSST pour vérifier ce client et ensuite modifier la date de réception de la référence pour une autre date que " & dateReceptionRef.ToString("yyyy-MM-dd") & "."
                        input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                        input.out.isError = True
                    Else
                        If Not input.in.errorsLinks.errorPeriodToAnotherClinicsLinks.ContainsKey(input.in.nam) Then
                            input.in.errorsLinks.errorPeriodToAnotherClinicsLinks.Add(input.in.nam, New ErrorsLinks.ErrorPeriodToAnotherClinic(input.in.clientFolderData("NoFolder"), True))
                        End If

                        csstError.errorMessage = "Les dates suivantes ont été modifiées pour celle de la première évaluation : référence et réception de la référence. Ceci indique à la CSST que le dossier a été transféré depuis une autre clinique."
                        input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                        Base.DBLinker.getInstance.updateDB("InfoFolders", "DateRef='" & evaluationDate.Date.ToString("yyyy-MM-dd") & "', DateReceptionRef='" & evaluationDate.Date.ToString("yyyy-MM-dd") & "'", "NoFolder", input.in.clientFolderData("NoFolder"), False)
                    End If
                End If
            ElseIf input.in.curType = FileResponse.reportTypes.Presence Then
                If input.fileResponse.findResult(input.previousResults, input.in.clientFolderData("NoClient"), input.in.clientFolderData("NoFolder"), "Rapport CSST initial") Is Nothing Then
                    'Final case
                    input.out.newMarking = Params.current.markedAsConfirmed
                    csstError.errorMessage = "Les dernières présences ne peuvent être transmises électroniquement, car celles-ci entre en conflit avec une autre clinique. Veuillez envoyer manuellement ces présences à la CSST."
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                Else
                    'Initial case
                    csstError.errorMessage = "Le rapport CSST initial était en erreur et il a eu une correction automatique, alors les présences seront renvoyées."
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                End If
            ElseIf input.in.curType = FileResponse.reportTypes.Final Then
                input.out.newMarking = Params.current.markedAsConfirmed
                csstError.errorMessage = "Le rapport CSST final ne peut être transmis électroniquement, car des présences entre en conflit avec une autre clinique. Veuillez envoyer manuellement ce rapport à la CSST."
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            ElseIf input.in.curType = FileResponse.reportTypes.Step Then
                Dim stepData As DataSet = Base.DBLinker.getInstance.readDBForGrid("FolderTextes", "*", "NoFolder=" & input.in.clientFolderData("NoFolder") & " AND TexteTitle='" & input.in.subject.Replace("'", "''") & "'")

                If stepData.Tables.Count <> 0 AndAlso stepData.Tables(0).Rows.Count <> 0 Then
                    'FolderText found
                    Dim stepInfos As DataRow = stepData.Tables(0).Rows(0)
                    input.out.newMarking = Params.current.markedAsConfirmed
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                    If input.in.clientFolderData("FolderClosingDate") IsNot DBNull.Value AndAlso CDate(stepInfos("DateStarted")).Date > CDate(input.in.clientFolderData("FolderClosingDate")).Date Then
                        'DateStarted's FolderTexte is over FolderClosingDate (Invalid report)
                        csstError.errorMessage = "Le rapport CSST d'étape n'est pas valide et il devrait être supprimé via l'analyse des textes des dossiers."
                    Else
                        'DateStarted's FolderTexte is not over FolderClosingDate (Valid report, but has to be sent manually)
                        csstError.errorMessage = "Le rapport CSST d'étape ne peut être transmis électroniquement, car des présences entre en conflit avec une autre clinique. Veuillez envoyer manuellement ce rapport à la CSST."
                    End If
                Else
                    'FolderText not found
                    input.out.newMarking = Params.current.markedAsConfirmed
                    csstError.errorMessage = "Le rapport CSST d'étape a été supprimé, car il était probablement invalide."
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                End If
            Else
                input.out.fontColor = CsstTask.RESULT_UNKNOWN_ERROR_COLOR
                input.out.isError = True
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New PeriodToAnotherClinic(errorCode)
        End Function
    End Class

End Namespace
