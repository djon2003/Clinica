Public Class ContactsManager
    Inherits ManagerBase(Of ContactsManager)
    Implements IEmailsValidable

    Private foldersList As ContactFoldersList

    Protected Sub New()
        MyBase.New()
        EmailValidator.addValidable(Me)
        foldersList = ContactFoldersList.getInstance()
    End Sub

    Public ReadOnly Property folderType() As Type
        Get
            Return foldersList.managedType()
        End Get
    End Property

    Public Function addContactFolder(ByVal path As String, Optional ByRef noContactFolder As Integer = 0) As String
        Return foldersList.addItemable(path, noContactFolder)
    End Function

    Public Function getContactFolder(ByVal path As String) As ContactFolder
        Return foldersList.getItemable(path)
    End Function

    Public Function getContactFolder(ByVal noContactFolder As Integer) As ContactFolder
        Return foldersList.getItemable(noContactFolder)
    End Function

    Public Function getContactFolders() As Generic.List(Of ContactFolder)
        Return foldersList.getItemables()
    End Function

    Public Function validateEmails(Optional ByVal where As String = "", Optional ByVal reportOnly As Boolean = False) As System.Data.DataTable Implements IEmailsValidable.validateEmails
        Dim invalidTable As DataTable = EmailValidator.getBaseValidationTable()
        Dim emails As New Generic.List(Of String)
        Dim clients As DataSet = DBLinker.getInstance.readDBForGrid("Contacts", "*", where & IIf(where <> "", " AND ", "") & "Courriel<>'' ORDER BY Nom,Prenom")
        If clients Is Nothing OrElse clients.Tables.Count = 0 OrElse clients.Tables(0).Rows.Count = 0 Then Return invalidTable

        For Each curRow As DataRow In clients.Tables(0).Rows
            Dim courriels() As String = curRow("Courriels").ToString().Split(New Char() {"§"})
            For i As Integer = 0 To courriels.Length - 1
                If emails.Contains(courriels(i)) = False Then emails.Add(courriels(i))
            Next i
        Next

        Dim answer As Generic.Dictionary(Of String, Integer()) = EmailValidator.isEmailsValid(MailsManager.mainFromEmailAddress, emails)
        Dim sqlUpdate As New System.Text.StringBuilder()
        For Each curKey As String In answer.Keys
            If answer(curKey)(0) = False Then
                sqlUpdate.AppendLine("UPDATE Contacts SET Courriels=CASE WHEN Courriels = '" & curKey.Replace("'", "''") & "' THEN '' ELSE CASE WHEN Courriels LIKE '" & curKey.Replace("'", "''") & "§%' THEN SUBSTRING(Courriels," & (curKey.Length + 2) & ",LEN(Courriels)-" & (curKey.Length + 1) & ") ELSE CASE WHEN Courriels LIKE '%§" & curKey.Replace("'", "''") & "' THEN SUBSTRING(Courriels, 0, LEN(Courriels) - " & (curKey.Length) & ") ELSE REPLACE(Courriels, '§" & curKey.Replace("'", "''") & "§','§') END END END WHERE Courriels LIKE '%" & curKey.Replace("'", "''") & "%'")
                Dim rows() As DataRow = clients.Tables(0).Select("Courriels='" & curKey.Replace("'", "''") & "' OR Courriels LIKE '" & curKey.Replace("'", "''") & "§%' OR Courriels LIKE '%§" & curKey.Replace("'", "''") & "' OR Courriels LIKE '%§" & curKey.Replace("'", "''") & "§%'")
                For Each curRow As DataRow In rows
                    invalidTable.Rows.Add(New Object() {"Contact", curRow("Nom") & "," & curRow("Prenom"), curKey, answer(curKey)(1), "CONTACT|" & curRow("NoContactFolder") & "|" & curRow("NoContact")})
                Next
            End If
        Next
        Dim whereUpdate As String = sqlUpdate.ToString
        If reportOnly = False AndAlso whereUpdate <> "" Then
            DBLinker.executeSQLScript(whereUpdate, False)
        End If

        Return invalidTable
    End Function

    Public ReadOnly Property type() As String Implements IEmailsValidable.type
        Get
            Return "Contact"
        End Get
    End Property
End Class
