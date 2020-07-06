Namespace ErrorTypes

    Public Class EvalAlreadySubmitted
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10245E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            input.out.newMarking = Params.current.markedAsUploaded 'Ensure no status modification is made upward

            Dim fileDate As Date = input.fileResponse.fileDate.AddDays(1)
            'Set other RVs as not processed
            Base.DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & Params.current.markedAsNotProcessed, _
                                   "NoVisite", "(SELECT TOP " & input.in.nbRV & " NoVisite FROM InfoVisites WHERE NoFolder = " & input.in.clientFolderData("NoFolder") & " AND NoStatut <> 3 AND ExternalStatus=" & Params.current.markedAsUploaded & " AND DateHeure < '" & fileDate.ToString("yyyy-MM-dd") & "' ORDER BY DateHeure)", False, "IN")

            'Set eval as confirmed
            Base.DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & Params.current.markedAsConfirmed, _
                       "NoVisite", "(SELECT TOP 1 NoVisite FROM InfoVisites WHERE Evaluation=1 AND NoFolder = " & input.in.clientFolderData("NoFolder") & " AND NoStatut <> 3 AND ExternalStatus=" & Params.current.markedAsNotProcessed & " AND DateHeure < '" & fileDate.ToString("yyyy-MM-dd") & "' ORDER BY DateHeure)", False, "IN")
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New EvalAlreadySubmitted(errorCode)
        End Function
    End Class

End Namespace
