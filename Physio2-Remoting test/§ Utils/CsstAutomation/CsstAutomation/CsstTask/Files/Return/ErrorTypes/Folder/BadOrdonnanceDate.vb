Namespace ErrorTypes

    Public Class BadOrdonnanceDate
        Inherits ErrorType

        Private Const ORDONNANCE_DATE_ERROR_MESSAGE As String = "La date de l'ordonnance (Date de référence) entre en conflit avec un autre dossier de la CSST. Veuillez appeler à la CSST pour ajuster la date de l'ordonnance (Date de référence) pour une date acceptée."

        Public Sub New(ByVal csstError As CSSTResponseError)
            MyBase.New(csstError)
        End Sub

        Private Const ORDONNANCE_DATE_CONFLICT As String = "MF10103E"
        Private Const ORDONNANCE_DATE_INF_EVENT_DATE As String = "MF10120E"
        Private Shared CSST_ERROR_CODES() As String = {ORDONNANCE_DATE_CONFLICT, ORDONNANCE_DATE_INF_EVENT_DATE}

        Public Overrides Sub manageError(ByVal input As ErrorInput)
            If input.out.newMarking > Params.current.markedAsNotProcessed Then input.out.newMarking = Params.current.markedAsNotProcessed

            Dim ordonnanceDateErrorMessage As String = ORDONNANCE_DATE_ERROR_MESSAGE
            If input.in.clientFolderData("FolderOrdonnanceDate") Is DBNull.Value Then
                ordonnanceDateErrorMessage = "La date de l'ordonnance (Date de référence) est manquante."
                input.out.isError = True
                input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
            Else
                Dim ordonnanceDate As Date = input.in.clientFolderData("FolderOrdonnanceDate")
                Dim noFolder As Integer = input.in.clientFolderData("NoFolder")

                Dim shallChangeDates As Boolean = Me.shallChangeDates(input)

                If shallChangeDates Then
                    'Reference date and its reception date will be changed automatically
                    Dim eventDate As String = Base.DateFormat.getTextDate(input.in.clientFolderData("DateAccident"))
                    Base.DBLinker.getInstance().updateDB("InfoFolders", "DateRef='" & eventDate & "', DateReceptionRef='" & eventDate & "'", "NoFolder", noFolder, False)

                    ordonnanceDateErrorMessage = "Les dates suivantes ont été modifiées pour celle de la première évaluation : référence et réception de la référence. Ceci indique à la CSST que le dossier est continué depuis un dossier précédent."
                    input.out.fontColor = CsstTask.RESULT_INFO_COLOR

                Else
                    'Reference date has to be changed manually
                    If input.in.errorsLinks.errorOrdonnanceDatesLinks.ContainsKey(noFolder) Then
                        If input.in.errorsLinks.errorOrdonnanceDatesLinks(noFolder).oldDate.Date <> ordonnanceDate.Date Then
                            ordonnanceDateErrorMessage = "La date de l'ordonnance (Date de référence) a été corrigée."
                            input.out.fontColor = CsstTask.RESULT_INFO_COLOR
                        Else
                            input.out.isError = True
                            input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                        End If
                    Else
                        If Me.csstError.errorCode = ORDONNANCE_DATE_INF_EVENT_DATE Then ordonnanceDateErrorMessage = Me.csstError.errorMessage

                        input.in.errorsLinks.errorOrdonnanceDatesLinks.Add(noFolder, New ErrorsLinks.ErrorDate(ordonnanceDate, False))
                        input.out.isError = True
                        input.out.fontColor = CsstTask.RESULT_ERROR_COLOR
                    End If
                End If
            End If

            csstError.errorMessage = ordonnanceDateErrorMessage
        End Sub

        Public Overloads Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            If Array.IndexOf(Of String)(CSST_ERROR_CODES, errorCode.ToString()) = -1 Then Return Nothing

            Return New BadOrdonnanceDate(errorCode)
        End Function

        Private Function shallChangeDates(ByVal input As ErrorInput) As Boolean
            Dim noFolder As Integer = input.in.clientFolderData("NoFolder")
            Dim eventDate As Date = input.in.clientFolderData("DateAccident")

            Dim willChangeDates As Boolean = False
            Dim previousFolders As DataTable = Base.DBLinker.getInstance().readDBForGrid("FolderDates", "*", "NoClient = " & input.in.clientFolderData("NoClient")).Tables(0)

            For i As Integer = previousFolders.Rows.Count - 1 To 0 Step -1
                Dim curRow As DataRow = previousFolders.Rows(i)
                Dim sameServiceAndEventDate As Boolean = curRow("Service").ToString().StartsWith(input.in.service) AndAlso curRow("DateAccident") IsNot DBNull.Value AndAlso CDate(curRow("DateAccident")).Date.Equals(eventDate.Date)
                Dim noRefVerification As Boolean = sameServiceAndEventDate AndAlso curRow("NoFolder") <> noFolder AndAlso curRow("NoRef") IsNot DBNull.Value AndAlso curRow("NoRef") <> "" AndAlso curRow("NoRef") = input.in.noCsstFolder
                Dim initVerification As Boolean = sameServiceAndEventDate AndAlso curRow("NoFolder") <> noFolder AndAlso input.in.curType = FileResponse.reportTypes.Initial

                If noRefVerification OrElse initVerification Then
                    willChangeDates = True
                    Exit For
                End If
            Next

            Return willChangeDates
        End Function
    End Class

End Namespace
