Namespace ErrorTypes

    Public Class BadEventDate
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub


        Public Const CSST_ERROR_CODE As String = "MF10257E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            Dim newEventDate As Date = CDate(csstError.errorMessage.Substring(csstError.errorMessage.Length - 10))
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            If input.in.clientFolderData IsNot Nothing Then
                Dim changeDate As Boolean = False
                Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
                Dim curEventDate As Date = input.in.clientFolderData("DateAccident")
                Dim newErrorMessage As String = "La date d'accident du dossier a été modifié à : " & newEventDate.ToString("yyyy-MM-dd") & "."

                If input.in.errorsLinks.errorEventDatesLinks.ContainsKey(noFolder) Then
                    If input.in.errorsLinks.errorEventDatesLinks(noFolder).oldDate.Date = curEventDate.Date Then
                        changeDate = True
                    Else
                        newErrorMessage = "La date d'accident du dossier avait été modifié à : " & newEventDate.ToString("yyyy-MM-dd") & "."
                    End If
                Else
                    input.in.errorsLinks.errorEventDatesLinks.Add(noFolder, New ErrorsLinks.ErrorDate(curEventDate, False))
                    changeDate = True
                End If

                csstError.errorMessage = newErrorMessage
                input.shallAddResult = True

                If changeDate Then Base.DBLinker.getInstance.updateDB("InfoFolders", "DateAccident='" & newEventDate.ToString("yyyy-MM-dd") & "'", "NoFolder", input.in.clientFolderData("NoFolder"), False)
                
                If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed
            Else
                csstError.errorMessage = "Impossible de trouver le dossier ayant pour date d'événement (Date d'accident) le " & input.in.eventDate.ToString("yyyy/MM/dd") & " et pour service " & input.in.fullServiceName & ". Veuillez ajuster la date d'accident dans les infos du bon dossier pour celle indiquée ici (même si cette date est erronée). Puis, exécutez de nouveau le module CSST. Ensuite remettez la date d'accident valide et finalement exécutez une dernière fois le module CSST."
                input.out.isError = True
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR

                If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadEventDate(errorCode)
        End Function
    End Class

End Namespace
