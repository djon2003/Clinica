Public Class EmailValidator

    Private Shared validables As New Generic.List(Of IEmailsValidable)

    Public Enum ValidationLevels As Integer
        Valid = 0
        WrongStructure = 1
        DomainNotExists = 2
        NotConfirmedByDomain = 3
    End Enum

    Public Shared Sub addValidable(ByVal newEmailsValidable As IEmailsValidable)
        validables.Add(newEmailsValidable)
    End Sub

    Public Shared Sub removeValidable(ByVal removedEmailsValidable As IEmailsValidable)
        If validables.Contains(removedEmailsValidable) Then validables.Remove(removedEmailsValidable)
    End Sub

    Public Shared Function isEmailValid(ByVal from As String, ByVal email As String) As ValidationLevels
        Dim returnCode As Integer
        launchAProccess(My.Application.Info.DirectoryPath & bar(My.Application.Info.DirectoryPath) & "EmailValidator.exe", True, ProcessWindowStyle.Hidden, """" & from & """ ""/e:" & email & """", , True, returnCode, True)
        Return returnCode
    End Function

    Public Shared Function isEmailsValid(ByVal from As String, ByVal emails As Generic.List(Of String)) As Generic.Dictionary(Of String, Integer())
        Dim tmpFile As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser
        IO.Directory.CreateDirectory(tmpFile)
        tmpFile &= "\§EmailVerif§Clinica.dat"
        IO.File.WriteAllText(tmpFile, String.Join(vbCrLf, emails.ToArray))
        launchAProccess(My.Application.Info.DirectoryPath & bar(My.Application.Info.DirectoryPath) & "EmailValidator.exe", True, ProcessWindowStyle.Hidden, from & " ""/f:" & tmpFile & """", , True, , True)

        Dim validationAnswer() As String = IO.File.ReadAllLines(tmpFile)
        Dim result As New Generic.Dictionary(Of String, Integer())
        If validationAnswer(0).IndexOf(":") <> 1 Then Return result
        Dim isValid As Boolean
        Dim level As Integer
        For i As Integer = 0 To validationAnswer.Length - 1
            Dim email() As String = validationAnswer(i).Split(New Char() {":"}, 3)
            isValid = IIf(email(1) = "0", False, True)
            level = email(0)
            result.Add(email(2), New Integer() {isValid, level})
        Next i

        IO.File.Delete(tmpFile)

        Return result
    End Function

    Private Class ValidationParams
        Public validable As IEmailsValidable
        Public where As String
        Public reportOnly As Boolean
    End Class

    Private Shared Sub startValidation(ByVal params As Object)
        Try
            Dim param As ValidationParams = params
            Dim invalid As DataTable = getBaseValidationTable()
            If param.validable IsNot Nothing Then
                validateValidable(param.validable, param.where, invalid, param.reportOnly)
            Else
                For Each curValidable As IEmailsValidable In validables
                    validateValidable(curValidable, param.where, invalid, param.reportOnly)
                Next
            End If

            If nbEmailErased <> 0 Then
                'Add URL protocol
                For Each curRow As DataRow In invalid.Rows
                    curRow("NomUrl") = WebTextControl.PROTOCOL_CLINICA & curRow("NomUrl")
                Next

                'Generate rapport of erased emails
                If param.reportOnly = True Then
                    ReportGeneration.startRapportGeneration("Courriels invalides", Nothing, invalid)
                Else
                    Dim myRapport As Report = ReportsManager.getInstance.createReport("Courriels invalides", , , invalid)
                    myRapport.saveToDB("Courriels invalides - " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.YYYYMMDD, , "."), "Généraux\Courriels invalides", New String() {"Rapport"}, New String() {"Liste des courriels supprimés car ils n'étaient plus valide"})
                End If
            End If

            myMainWin.StatusText = "Fin de la validation de courriels (" & nbEmailErased & " adresses de courriel " & IIf(param.reportOnly = True, "à supprimer)", "supprimées)")
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Private Shared Sub validateValidable(ByVal validable As IEmailsValidable, ByVal where As String, ByVal invalidTable As DataTable, Optional ByVal reportOnly As Boolean = False)
        Dim invalid As DataTable = validable.validateEmails(where, reportOnly)
        If invalid.Rows.Count = 0 Then Exit Sub

        nbEmailErased += invalid.Rows.Count
        For Each curRow As DataRow In invalid.Rows
            invalidTable.ImportRow(curRow)
        Next
    End Sub

    Private Shared nbEmailErased As Integer = 0

    Public Shared Sub validate(ByVal validable As IEmailsValidable, Optional ByVal where As String = "", Optional ByVal reportOnly As Boolean = False)
        If reportOnly = False AndAlso MessageBox.Show("Êtes-vous sûr de vouloir lancer la validation, car cela supprimera les adresses de courriel invalide ?", "Validation de courriels", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        myMainWin.StatusText = "Début de la validation de courriels"

        nbEmailErased = 0

        Dim newThread As New Threading.Thread(AddressOf startValidation)
        Dim params As New ValidationParams
        params.validable = validable
        params.where = where
        params.reportOnly = reportOnly
        newThread.Start(params)
    End Sub

    Public Shared Function getBaseValidationTable() As DataTable
        Dim validTable As New DataTable("Validation")
        validTable.Columns.Add("Type")
        validTable.Columns.Add("Nom")
        validTable.Columns.Add("Courriel")
        validTable.Columns.Add("Niveau")
        validTable.Columns.Add("NomURL")

        Return validTable
    End Function
End Class
