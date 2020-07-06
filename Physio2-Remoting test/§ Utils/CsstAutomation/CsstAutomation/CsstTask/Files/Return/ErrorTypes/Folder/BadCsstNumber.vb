Namespace ErrorTypes

    Public Class BadCsstNumber
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10183E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}
        
        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            input.out.isError = True
            input.out.fontColor = CsstTask.RESULT_ERROR_COLOR

            If input.in.errorsLinks.errorCSSTNumbersLinks.ContainsKey(input.in.noCsstFolder) = False Then
                input.in.errorsLinks.errorCSSTNumbersLinks.Add(input.in.noCsstFolder, New ErrorsLinks.ErrorCSSTNumber(input.in.clientFolderData("NoFolder"), False))
            Else
                input.shallAddResult = False
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadCsstNumber(errorCode)
        End Function
    End Class

End Namespace
