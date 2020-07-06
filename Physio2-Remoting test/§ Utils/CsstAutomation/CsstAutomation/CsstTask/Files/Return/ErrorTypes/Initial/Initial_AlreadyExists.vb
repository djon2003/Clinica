Namespace ErrorTypes

    Public Class Initial_AlreadyExists
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10150E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
            If FilesReturnator.finalErrorSetConfirmed.ContainsKey(noFolder) Then
                input.out.newMarking = Params.current.markedAsNotProcessed
                csstError.errorMessage = "Le rapport CSST final d'un autre dossier ayant le même numéro de référence a été défini comme devant être envoyé manuellement à la CSST. Ainsi, ce rapport CSST initial sera renvoyé."
            Else
                input.out.newMarking = Params.current.markedAsConfirmed
            End If


            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Initial_AlreadyExists(errorCode)
        End Function
    End Class

End Namespace
