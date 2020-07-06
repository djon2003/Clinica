Namespace ErrorTypes

    Public Class Initial_ManuallyEntered
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10138E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.in.curType = FileResponse.reportTypes.Initial Then
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                Dim newErrorMessage As String = "Le rapport initial a été manuellement saisi par la CSST. Veuillez faxer ce document à la CSST."
                csstError.errorMessage = newErrorMessage

                If input.out.newMarking > Params.current.markedAsConfirmed Then input.out.newMarking = Params.current.markedAsConfirmed
            Else
                input.out.isError = True
                input.out.fontColor = CsstTask.RESULT_UNKNOWN_ERROR_COLOR
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Initial_ManuallyEntered(errorCode)
        End Function
    End Class

End Namespace
