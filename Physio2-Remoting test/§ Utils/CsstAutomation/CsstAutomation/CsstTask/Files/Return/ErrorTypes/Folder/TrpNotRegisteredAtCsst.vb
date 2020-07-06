Namespace ErrorTypes

    Public Class TrpNotRegisteredAtCsst
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10178E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            If input.in.curType = FileResponse.reportTypes.Initial Then
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                csstError.errorMessage = "Le numéro de permis de " & input.in.clientFolderData("TRP") & " n'est probablement pas encore saisi à la CSST (appeler pour confirmer et le faire ajouter) ou son numéro de permis n'est pas correct (vérifier le numéro). Le rapport initial sera renvoyé automatiquement tant que l'erreur ne sera pas corrigée."
            Else
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                input.out.isError = True
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New TrpNotRegisteredAtCsst(errorCode)
        End Function
    End Class

End Namespace
