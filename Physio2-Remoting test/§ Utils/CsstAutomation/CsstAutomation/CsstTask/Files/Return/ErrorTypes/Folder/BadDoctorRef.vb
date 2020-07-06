Namespace ErrorTypes

    Public Class BadDoctorRef
        Inherits ErrorType

        Private Const MEDECIN_ERROR_MESSAGE As String = "Le numéro de médecin n'existe pas à la CSST. Veuillez vous assurer qu'il s'agit du bon numéro de référence dans le compte personne/organisme clé du médecin de ce dossier."
        Private Const NO_MEDECIN_ERROR_MESSAGE As String = "Il n'y a pas de médecin sélectionné dans les informations du dossier."

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10119E", "MF10232E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
            Dim medecinNo As Integer = 0
            Dim medecinRef As String = String.Empty

            If input.in.clientFolderData("MedecinNo") IsNot DBNull.Value Then
                medecinNo = input.in.clientFolderData("MedecinNo")
                medecinRef = input.in.clientFolderData("MedecinRef")
            End If

            Dim medecinErrorMessage As String = MEDECIN_ERROR_MESSAGE

            If input.in.errorsLinks.errorMedecinRefsLinks.ContainsKey(noFolder) Then
                If medecinNo <> 0 AndAlso (input.in.errorsLinks.errorMedecinRefsLinks(noFolder).noKP <> medecinNo OrElse input.in.errorsLinks.errorMedecinRefsLinks(noFolder).oldNoRef <> medecinRef) Then
                    medecinErrorMessage = "Le numéro de référence du médecin a été corrigé."
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                Else
                    input.out.isError = True
                    input.out.fontColor = CsstTask.RESULT_ERROR_COLOR

                    If medecinNo = 0 Then medecinErrorMessage = NO_MEDECIN_ERROR_MESSAGE
                End If
            Else
                input.out.isError = True
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                input.in.errorsLinks.errorMedecinRefsLinks.Add(noFolder, New ErrorsLinks.ErrorMedecinRef(noFolder, medecinNo, medecinRef, False))

                If medecinNo = 0 Then medecinErrorMessage = NO_MEDECIN_ERROR_MESSAGE
            End If

            csstError.errorMessage = medecinErrorMessage
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadDoctorRef(errorCode)
        End Function
    End Class

End Namespace
