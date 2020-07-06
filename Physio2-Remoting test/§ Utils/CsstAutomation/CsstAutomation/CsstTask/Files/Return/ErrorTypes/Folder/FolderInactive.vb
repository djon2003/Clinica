Namespace ErrorTypes

    Public Class FolderInactive
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const MAIN_INACTIVE As String = "MF10244E"
        Public Const SECOND_INACTIVE As String = "MF10258E"
        Public Const THIRD_INACTIVE As String = "MF10246E"
        Private Shared CSST_ERROR_CODES() As String = {THIRD_INACTIVE, SECOND_INACTIVE, MAIN_INACTIVE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            'FAKE_INACTIVE
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            Dim namErrorLinked As Boolean = input.in.lineErrors.getErrorMessage(BadNAMVsCsstNumber.CSST_ERROR_CODE) <> String.Empty
            If csstError.errorCode = SECOND_INACTIVE AndAlso namErrorLinked Then
                csstError.errorMessage = String.Empty 'Remove error message
            ElseIf Array.IndexOf(Of String)(CSST_ERROR_CODES, csstError.errorCode) <> -1 Then
                input.out.newMarking = Params.current.markedAsRefused

                Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsRefused, "NoFolder", input.in.clientFolderData("NoFolder"), False)

                csstError.errorMessage = "Le dossier a été inactivé par la CSST. Veuillez envoyer manuellement par fax le dossier en entier à la CSST, car il a été retiré du processus automatisé."
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            Else
                input.out.isError = True
                input.out.fontColor = CsstTask.RESULT_UNKNOWN_ERROR_COLOR
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New FolderInactive(errorCode)
        End Function
    End Class

End Namespace
