Namespace ErrorTypes

    Public Class TryAgain
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Const MAXIMUM_NB_TRIES As Integer = 3
        Public Const CSST_ERROR_CODE As String = "MF10267E"
        Private Shared CSST_ERROR_CODES() As String = {CSST_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            Dim nbTries As Integer = 1
            If input.in.errorsLinks.tryAgainCounters.ContainsKey(input.in.clientFolderData("NoFolder")) Then
                nbTries = input.in.errorsLinks.tryAgainCounters(input.in.clientFolderData("NoFolder"))
                input.in.errorsLinks.tryAgainCounters(input.in.clientFolderData("NoFolder")) += 1
            Else
                input.in.errorsLinks.tryAgainCounters.Add(input.in.clientFolderData("NoFolder"), 2)
            End If
            nbTries += 1

            If nbTries > MAXIMUM_NB_TRIES Then
                csstError.errorMessage = "Le nombre maximal de tentative (" & MAXIMUM_NB_TRIES & ") d'envoi a été atteinte.<br/>Ce dossier a été exclus du processus automatisé. Veuillez désormais poursuivre manuellement avec la CSST.<br/>Erreur d'origine : " & csstError.errorMessage
                Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsRefused, "NoFolder", input.in.clientFolderData("NoFolder"), False)
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                input.out.isError = True
            Else
                input.in.errorsLinks.tryAgainCounters.Remove(input.in.clientFolderData("NoFolder"))

                Base.DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & Params.current.markedAsNotProcessed, "NoFolder", input.in.clientFolderData("NoFolder") & " AND ExternalStatus=" & Params.current.markedAsUploaded, False)
                csstError.errorMessage = "Probablement une erreur d'ordre manuelle au sein du personnel de la CSST force le renvoi (Tentative #" & nbTries & " sur " & MAXIMUM_NB_TRIES & ") de certains rendez-vous.<br/>Erreur d'origine : " & csstError.errorMessage
                input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New TryAgain(errorCode)
        End Function
    End Class

End Namespace
