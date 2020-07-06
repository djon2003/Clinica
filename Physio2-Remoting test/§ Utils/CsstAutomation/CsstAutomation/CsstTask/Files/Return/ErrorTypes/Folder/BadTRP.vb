Namespace ErrorTypes

    Public Class BadTRP
        Inherits ErrorType

        Private Const TRP_ERROR_MESSAGE As String = "Le numéro de permis du thérapeute ne correspond pas au service offert par le dossier."

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10277E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
            Dim trpErrorMessage As String = TRP_ERROR_MESSAGE
            If input.in.clientFolderData("TRPNo") Is DBNull.Value Then
                input.in.errorsLinks.errorTRPNoPermitLinks.Add(noFolder, New ErrorsLinks.ErrorTRPNoPermit(noFolder, 0, "", False))
                input.out.isError = True
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                trpErrorMessage = "Il n'existe pas de thérapeute dans les rendez-vous du dossier ayant pour titre Physiothérapeute ou Ergothérapeute."
            Else
                Dim trpNo As Integer = input.in.clientFolderData("TRPNo")
                Dim trpNoPermit As String = input.in.clientFolderData("TRPNoPermit")

                If input.in.errorsLinks.errorTRPNoPermitLinks.ContainsKey(noFolder) Then
                    If input.in.errorsLinks.errorTRPNoPermitLinks(noFolder).noUser <> trpNo OrElse input.in.errorsLinks.errorTRPNoPermitLinks(noFolder).oldNoPermit <> trpNoPermit Then
                        trpErrorMessage = "Le numéro de permis du thérapeute a été corrigé."
                        input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                    Else
                        input.out.isError = True
                        input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                    End If
                Else
                    input.in.errorsLinks.errorTRPNoPermitLinks.Add(noFolder, New ErrorsLinks.ErrorTRPNoPermit(noFolder, trpNo, trpNoPermit, False))
                    input.out.isError = True
                    input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                End If
            End If

            csstError.errorMessage = trpErrorMessage
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadTRP(errorCode)
        End Function
    End Class

End Namespace
