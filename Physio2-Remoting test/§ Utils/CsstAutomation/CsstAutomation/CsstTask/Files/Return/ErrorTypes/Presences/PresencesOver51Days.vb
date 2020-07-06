Namespace ErrorTypes

    Public Class PresencesOver51Days
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10123E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            input.out.newMarking = Params.current.markedAsConfirmed

            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New PresencesOver51Days(errorCode)
        End Function
    End Class

End Namespace
