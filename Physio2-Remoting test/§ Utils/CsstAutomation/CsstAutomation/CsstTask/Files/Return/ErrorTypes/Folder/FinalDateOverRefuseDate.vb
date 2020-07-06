
Namespace ErrorTypes

    Public Class FinalDateOverRefuseDate
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10272E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsConfirmed Then input.out.newMarking = Params.current.markedAsConfirmed

            Base.DBLinker.getInstance().updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsRefused, "NoFolder", input.in.clientFolderData("NoFolder"))
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New FinalDateOverRefuseDate(errorCode)
        End Function
    End Class

End Namespace
