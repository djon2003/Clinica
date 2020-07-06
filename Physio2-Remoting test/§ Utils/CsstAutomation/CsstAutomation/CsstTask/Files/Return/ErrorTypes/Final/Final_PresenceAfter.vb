Namespace ErrorTypes

    Public Class Final_PresenceAfter
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10237E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
            If FilesReturnator.finalErrorSetConfirmed.ContainsKey(noFolder) = False Then FilesReturnator.finalErrorSetConfirmed.Add(noFolder, True)

            input.out.newMarking = Params.current.markedAsConfirmed

            If input.in.curType = FileResponse.reportTypes.Final Then
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                csstError.errorMessage = "Un rendez-vous après la date d'inactivation du dossier avait été envoyé. Veuillez envoyer le rapport final manuellement à la CSST."
            Else
                input.out.isError = True
                input.out.fontColor = CsstTask.RESULT_UNKNOWN_ERROR_COLOR
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Final_PresenceAfter(errorCode)
        End Function
    End Class

End Namespace
