Namespace ErrorTypes

    Public Class Initial_Error
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10233E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            input.shallAddResult = False
            'Error not to show, because fixed by the initial report
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Initial_Error(errorCode)
        End Function
    End Class

End Namespace
