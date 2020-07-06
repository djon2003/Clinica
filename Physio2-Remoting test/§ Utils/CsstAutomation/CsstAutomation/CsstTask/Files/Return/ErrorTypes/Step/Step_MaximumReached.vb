Namespace ErrorTypes

    Public Class Step_MaximumReached
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10172E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            'Setting to markedAsUploaded, so that DB.update is not done, because pursuitFolderWithNew changes it and the folderText is not in current folder anymore anyway
            If input.out.newMarking > Params.current.markedAsUploaded Then input.out.newMarking = Params.current.markedAsUploaded

            Dim newNoFolder As Integer = File.pursuitFolderWithNew(input.in.clientFolderData("NoFolder"))
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
            csstError.errorMessage = File.PURSUIT_REASON.Replace("OLD_FOLDER", input.in.clientFolderData("NoFolder")).Replace("NEW_FOLDER", newNoFolder)
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New Step_MaximumReached(errorCode)
        End Function
    End Class

End Namespace
