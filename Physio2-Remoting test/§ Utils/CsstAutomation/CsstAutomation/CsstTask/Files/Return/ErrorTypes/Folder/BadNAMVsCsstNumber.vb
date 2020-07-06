Namespace ErrorTypes

    Public Class BadNAMVsCsstNumber
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Public Const CSST_ERROR_CODE As String = "MF10114E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}
        Private Shared CSST_ERROR_MESSAGE As String = "Le numéro de référence du dossier ne correspond pas au NAM. Veuillez appeler la CSST pour obtenir le bon numéro (numéro de dossier pour eux) et l'inscrire dans le dossier."

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            If input.in.curType = FileResponse.reportTypes.Initial Then
                Base.DBLinker.getInstance.updateDB("InfoFolders", "NoRef=''", "NoFolder", input.in.clientFolderData("NoFolder"), False)
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                csstError.errorMessage = "Le numéro de référence du dossier a été effacé, car il était probablement erroné. Le rapport CSST initial sera renvoyé automatiquement."
            Else
                If input.in.errorsLinks.errorCSSTNumbersLinks.ContainsKey(input.in.noCsstFolder) = False Then
                    input.in.errorsLinks.errorCSSTNumbersLinks.Add(input.in.noCsstFolder, New ErrorsLinks.ErrorCSSTNumber(input.in.clientFolderData("NoFolder"), False))
                    input.out.isError = True
                    input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                    csstError.errorMessage = CSST_ERROR_MESSAGE
                ElseIf input.in.clientFolderData("NoRef") = input.in.noCsstFolder Then
                    input.out.isError = True
                    input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                    csstError.errorMessage = CSST_ERROR_MESSAGE
                Else
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                    csstError.errorMessage = "Numéro de référence a été corrigé."
                End If
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadNAMVsCsstNumber(errorCode)
        End Function
    End Class

End Namespace