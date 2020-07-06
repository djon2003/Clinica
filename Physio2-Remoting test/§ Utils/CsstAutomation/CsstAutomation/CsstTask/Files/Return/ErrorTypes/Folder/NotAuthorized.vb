Namespace ErrorTypes

    Public Class NotAuthorized
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub


        Public Const CSST_ERROR_CODE As String = "SE50"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            input.out.newMarking = Params.current.markedAsUploaded 'Marked this way so that no DB updates are done for presences

            Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsRefused, "NoFolder", input.in.clientFolderData("NoFolder"), False)
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            csstError.errorMessage = "La CSST indique que les envois via le guichet ne sont pas autorisés pour ce dossier.<br/>Ce dossier a été exclus du processus automatisé. Veuillez désormais poursuivre manuellement avec la CSST."

        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New NotAuthorized(errorCode)
        End Function
    End Class

End Namespace
