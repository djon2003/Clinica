Namespace ErrorTypes

    Public Class FolderRefused
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub


        Private Const FIRST_CSST_ERROR_CODE As String = "MF10126E"
        Private Const SECOND_CSST_ERROR_CODE As String = "MF10127E"
        Private Const THIRD_CSST_ERROR_CODE As String = "MF10128E"
        Private Shared CSST_ERROR_CODES() As String = {FIRST_CSST_ERROR_CODE, SECOND_CSST_ERROR_CODE, THIRD_CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            input.out.newMarking = Params.current.markedAsRefused

            If input.in.curType = FileResponse.reportTypes.Initial Then
                Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsRefused, "NoFolder", input.in.clientFolderData("NoFolder"), False)
            End If
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            csstError.errorMessage &= "<br/>Ce dossier a été exclus du processus automatisé. Veuillez désormais poursuivre manuellement avec la CSST."

        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New FolderRefused(errorCode)
        End Function
    End Class

End Namespace
