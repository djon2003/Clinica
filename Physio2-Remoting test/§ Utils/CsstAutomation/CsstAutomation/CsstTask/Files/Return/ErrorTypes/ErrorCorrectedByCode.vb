Namespace ErrorTypes

    Public Class ErrorCorrectedByCode
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Const SIGNATURE_DATE_INFERIOR_TREATMENT_DATE As String = "MF10139E"
        Private Shared CSST_ERROR_CODES() As String = {SIGNATURE_DATE_INFERIOR_TREATMENT_DATE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            input.shallAddResult = False
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New ErrorCorrectedByCode(errorCode)
        End Function
    End Class

End Namespace
