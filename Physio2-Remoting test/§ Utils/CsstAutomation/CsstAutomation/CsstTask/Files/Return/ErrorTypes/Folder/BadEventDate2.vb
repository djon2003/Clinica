Namespace ErrorTypes

    Public Class BadEventDate2
        Inherits ErrorType

        Private Const EVENT_DATE_ERROR_MESSAGE As String = "La date d'événement ne correspond pas avec celle de la CSST. Veuillez appeler à la CSST pour ajuster la date d'événement (Dossier->Infos->Date d'accident) pour la leur."

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE_MAIN As String = "MF10118E"
        Private Shared CSST_ERROR_CODES() As String = {"MF10188E", CSST_ERROR_CODE_MAIN}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            If input.in.curType = FileResponse.reportTypes.Presence Then
                input.shallAddResult = False
            Else
                Dim eventDateErrorMessage As String = EVENT_DATE_ERROR_MESSAGE

                If input.in.clientFolderData Is Nothing OrElse (input.in.clientFolderData("DateAccident") <> input.in.eventDate AndAlso Not input.in.errorsLinks.errorEventDatesLinks.ContainsKey(input.in.clientFolderData("NoFolder"))) Then
                    eventDateErrorMessage = "Impossible de trouver le dossier ayant pour date d'événement (Date d'accident) le " & input.in.eventDate.ToString("yyyy/MM/dd") & " et pour service " & input.in.fullServiceName & ". Veuillez ajuster la date d'accident dans les infos du bon dossier pour celle indiquée ici (même si cette date est erronée). Puis, exécutez de nouveau le module CSST. Ensuite remettez la date d'accident valide et finalement exécutez une dernière fois le module CSST."

                    input.out.isError = True
                    input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                Else

                    Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
                    Dim curEventDate As Date = input.in.clientFolderData("DateAccident")

                    If input.in.errorsLinks.errorEventDatesLinks.ContainsKey(noFolder) Then
                        If input.in.errorsLinks.errorEventDatesLinks(noFolder).oldDate.Date <> curEventDate.Date Then
                            eventDateErrorMessage = "La date d'événement (Date d'accident) a été corrigée."
                            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                        Else
                            input.out.isError = True
                            input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                        End If
                    Else
                        input.in.errorsLinks.errorEventDatesLinks.Add(noFolder, New ErrorsLinks.ErrorDate(curEventDate, False))
                        input.out.isError = True
                        input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                    End If
                End If

                csstError.errorMessage = eventDateErrorMessage
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadEventDate2(errorCode)
        End Function
    End Class

End Namespace
