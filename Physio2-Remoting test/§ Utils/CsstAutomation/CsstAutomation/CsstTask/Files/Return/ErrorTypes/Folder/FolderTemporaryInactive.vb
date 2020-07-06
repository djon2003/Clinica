Namespace ErrorTypes

    Public Class FolderTemporaryInactive
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10246E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            csstError.errorMessage = "Le dossier à la CSST est temporairement inactif (probablement dû à un transfert de clinique). Le module va renvoyer automatiquement."
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New FolderTemporaryInactive(errorCode)
        End Function
    End Class

End Namespace
