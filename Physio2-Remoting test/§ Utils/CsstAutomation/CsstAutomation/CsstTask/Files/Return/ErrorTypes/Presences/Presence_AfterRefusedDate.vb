Namespace ErrorTypes

    Public Class Presence_AfterRefusedDate
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10269E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            'Do not change marking because it have to be send manually
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            csstError.errorMessage = "Un ou plusieurs rendez-vous du lot envoyé est/sont après la date de refus du dossier. Veuillez envoyer ce(s) rendez-vous manuellement à la CSST."
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Presence_AfterRefusedDate(errorCode)
        End Function
    End Class

End Namespace
