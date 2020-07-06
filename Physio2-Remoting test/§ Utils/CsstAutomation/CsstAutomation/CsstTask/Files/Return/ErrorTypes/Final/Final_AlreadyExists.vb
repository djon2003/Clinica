Namespace ErrorTypes

    Public Class Final_AlreadyExists
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10228E"
        Public Const FINAL_EXISTS As String = "MF10155E" 'Ce rapport de fin d'intervention (ENF) a déjà été saisi.

        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE, FINAL_EXISTS}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            input.out.newMarking = Params.current.markedAsConfirmed

            input.out.fontColor = CsstTask.RESULT_ERROR_COLOR

            'TODO: Still not sure how to manage for initial & linked presences
            If input.in.curType = FileResponse.reportTypes.Initial OrElse (input.in.curType = FileResponse.reportTypes.Presence AndAlso input.fileResponse.findResult(input.previousResults, input.in.clientFolderData("NoClient"), input.in.clientFolderData("NoFolder"), "Rapport CSST initial") IsNot Nothing) Then
                input.out.isError = False
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Final_AlreadyExists(errorCode)
        End Function
    End Class

End Namespace
