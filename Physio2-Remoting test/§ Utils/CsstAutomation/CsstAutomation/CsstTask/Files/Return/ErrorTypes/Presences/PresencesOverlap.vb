Namespace ErrorTypes

    Public Class PresencesOverlap
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10275E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsConfirmed Then input.out.newMarking = Params.current.markedAsConfirmed

            If input.in.curType = FileResponse.reportTypes.Presence Then
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                csstError.errorMessage = "Le client a déjà un autre dossier à la CSST ayant une présence à la même journée que l'une de ce lot. Veuillez faxer " & input.in.rvsString & "."
            Else
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                input.out.isError = True
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New PresencesOverlap(errorCode)
        End Function
    End Class

End Namespace
