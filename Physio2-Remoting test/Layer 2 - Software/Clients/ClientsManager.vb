Public Class ClientsManager
    Inherits ManagerBase(Of ClientsManager)
    Implements IEmailsValidable

    Protected Sub New()
        MyBase.New()
        EmailValidator.addValidable(Me)
    End Sub

    Public Function validateEmails(Optional ByVal where As String = "", Optional ByVal reportOnly As Boolean = False) As System.Data.DataTable Implements IEmailsValidable.validateEmails
        Dim invalidTable As DataTable = EmailValidator.getBaseValidationTable()
        Dim emails As New Generic.List(Of String)
        Dim clients As DataSet = DBLinker.getInstance.readDBForGrid("InfoClients", "*", where & IIf(where <> "", " AND ", "") & "Courriel<>'' ORDER BY Nom,Prenom")
        If clients Is Nothing OrElse clients.Tables.Count = 0 OrElse clients.Tables(0).Rows.Count = 0 Then Return invalidTable

        For Each curRow As DataRow In clients.Tables(0).Rows
            If emails.Contains(curRow("Courriel")) = False Then emails.Add(curRow("Courriel"))
        Next

        Dim answer As Generic.Dictionary(Of String, Integer()) = EmailValidator.isEmailsValid(MailsManager.mainFromEmailAddress, emails)
        Dim sqlUpdate As New System.Text.StringBuilder()
        For Each curKey As String In answer.Keys
            If answer(curKey)(0) = False Then
                sqlUpdate.Append(" OR Courriel='" & curKey.Replace("'", "''") & "'")
                Dim rows() As DataRow = clients.Tables(0).Select("Courriel='" & curKey.Replace("'", "''") & "'")
                For Each curRow As DataRow In rows
                    invalidTable.Rows.Add(New Object() {"Client", curRow("Nom") & "," & curRow("Prenom"), curKey, answer(curKey)(1), "CLIENT|" & curRow("NoClient")})
                Next
            End If
        Next
        Dim whereUpdate As String = sqlUpdate.ToString
        If reportOnly = False AndAlso whereUpdate <> "" Then
            whereUpdate = "UPDATE InfoClients SET Courriel='' WHERE " & whereUpdate.Substring(4)
            DBLinker.executeSQLScript(whereUpdate, False)
        End If

        Return invalidTable
    End Function

    Public ReadOnly Property type() As String Implements IEmailsValidable.type
        Get
            Return "Client"
        End Get
    End Property
End Class