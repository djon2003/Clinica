Namespace ErrorTypes

    Public Class BadNAM
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Const ERROR_MESSAGE As String = "Le NAM est erroné. Veuillez modifier celui-ci pour continuer."

        Public Const MAIN_ERROR_CODE As String = "MF10184E"
        Public Const SECONDARY_ERROR_CODE As String = "MF10110E"
        Private Shared CSST_ERROR_CODES() As String = {MAIN_ERROR_CODE, SECONDARY_ERROR_CODE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            input.out.isError = True
            input.out.fontColor = CsstTask.RESULT_ERROR_COLOR

            With input.in.lineErrors
                If .isErrorCodeExists(MAIN_ERROR_CODE) Then
                    .setErrorMessage(MAIN_ERROR_CODE, ERROR_MESSAGE)
                Else
                    .setErrorMessage(SECONDARY_ERROR_CODE, ERROR_MESSAGE)
                End If
            End With

            If input.in.errorsLinks.errorNAMsLinks.ContainsKey(input.in.nam) = False Then
                input.in.errorsLinks.errorNAMsLinks.Add(input.in.nam, New ErrorsLinks.ErrorNAM(input.in.clientFolderDS.Tables(0).Rows(0)("NoClient"), False))
            End If
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadNAM(errorCode)
        End Function
    End Class

End Namespace
