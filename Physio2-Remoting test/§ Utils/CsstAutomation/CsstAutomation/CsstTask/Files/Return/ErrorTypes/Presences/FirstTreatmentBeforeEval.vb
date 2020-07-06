Namespace ErrorTypes

    Public Class FirstTreatmentBeforeEval
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10260E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            If input.in.clientFolderData("Evaluation") Is DBNull.Value Then
                csstError.errorMessage = "Il n'existe pas d'évaluation pour ce dossier. Veuillez changer le type du premier rendez-vous pour ""Évaluation""."
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                input.out.isError = True
            Else
                Dim firstTreatment As Date = input.in.clientFolderData("FirstTreatment")
                Dim evaluationDate As Date = input.in.clientFolderData("Evaluation")
                If firstTreatment.Date > evaluationDate.Date Then
                    csstError.errorMessage = """Date d'évaluation versus date du premier traitement"" a été corrigée."
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                Else
                    input.out.isError = True
                    input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                End If
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New FirstTreatmentBeforeEval(errorCode)
        End Function
    End Class

End Namespace
