Namespace ErrorTypes

    Public Class NoPresenceForEvaluation
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10235E", "MF10182E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsUploaded Then input.out.newMarking = Params.current.markedAsUploaded

            If input.in.clientFolderData("Evaluation") Is DBNull.Value Then
                csstError.errorMessage = "Il n'existe pas d'évaluation pour ce dossier. Veuillez changer le type du premier rendez-vous pour ""Évaluation""."
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
            Else
                Dim evaluation As Date = input.in.clientFolderData("Evaluation")
                evaluation = evaluation.Date

                Base.DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & Params.current.markedAsNotProcessed, _
                                               "NoFolder", input.in.clientFolderData("NoFolder") & " AND DateHeure >= '" & evaluation.ToString("yyyy-MM-dd") & "'", False)

                csstError.errorMessage = "Les présences seront renvoyées depuis la première évaluation."
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New NoPresenceForEvaluation(errorCode)
        End Function
    End Class

End Namespace
