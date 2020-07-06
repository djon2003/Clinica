Namespace ErrorTypes

    Public Class Presence_ErrorDueToInitialManuallyEntered
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10225E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            csstError.errorMessage = "Le rapport initial a été manuellement saisi par la CSST, mais la version que vous avez faxé n'est pas encore saisie. Les présences vont se renvoyer automatiquement."
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Presence_ErrorDueToInitialManuallyEntered(errorCode)
        End Function
    End Class

End Namespace
