Namespace ErrorTypes

    Public Class PresenceEntryRemoved
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10241E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
            csstError.errorMessage = "Une entrée de présence a été effectuée manuellement et entre en conflit avec le présent envoi. Veuillez appeler la CSST pour vérifier " & input.in.rvsString & "."
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New PresenceEntryRemoved(errorCode)
        End Function
    End Class

End Namespace
