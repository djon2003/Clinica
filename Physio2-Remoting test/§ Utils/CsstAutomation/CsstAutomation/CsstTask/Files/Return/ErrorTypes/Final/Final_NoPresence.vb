Namespace ErrorTypes

    Public Class Final_NoPresence
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10226E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
            If FilesReturnator.finalErrorSetConfirmed.ContainsKey(noFolder) = False Then FilesReturnator.finalErrorSetConfirmed.Add(noFolder, True)

            input.out.newMarking = Params.current.markedAsConfirmed
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            csstError.errorMessage = "Aucune présence n'a été confirmé jusqu'à présent. Ainsi, ce rapport final devra être envoyé manuellement à la CSST."
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Final_NoPresence(errorCode)
        End Function
    End Class

End Namespace
