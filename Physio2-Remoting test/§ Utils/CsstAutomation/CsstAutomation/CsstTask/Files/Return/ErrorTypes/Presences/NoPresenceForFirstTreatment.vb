Namespace ErrorTypes

    Public Class NoPresenceForFirstTreatment
        Inherits ErrorType

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Shared CSST_ERROR_CODES() As String = {"MF10133E"}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsUploaded Then input.out.newMarking = Params.current.markedAsUploaded

            Dim firstTreatment As Date = input.in.clientFolderData("FirstTreatment")
            firstTreatment = firstTreatment.Date

            Base.DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & Params.current.markedAsNotProcessed, _
                                           "NoFolder", input.in.clientFolderData("NoFolder") & " AND DateHeure >= '" & firstTreatment.ToString("yyyy-MM-dd") & "'", False)

            csstError.errorMessage = "Les présences seront renvoyées depuis le premier traitement."
            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New NoPresenceForFirstTreatment(errorCode)
        End Function
    End Class

End Namespace
