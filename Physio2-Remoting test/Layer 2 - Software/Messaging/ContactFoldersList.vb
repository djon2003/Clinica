Public Class ContactFoldersList
    Inherits FoldersListBase(Of ContactFoldersList, ContactFolder)

    Private _isLoading As Boolean = False

    Protected Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub load(ByVal noFolder As Integer)
        If noFolder = 0 Then clear()
        If currentDroitAcces Is Nothing Then Exit Sub
        _isLoading = True

        Dim accesWhere As String = "ContactFolders.NoUser=" & ConnectionsManager.currentUser
        If currentDroitAcces.GetUpperBound(0) >= 98 Then
            If currentDroitAcces(36) Then accesWhere = ""
            If currentDroitAcces(34) And accesWhere <> "" Then
                accesWhere &= " OR ContactFolders.NoUser IS NULL"
            ElseIf currentDroitAcces(34) = False AndAlso accesWhere = String.Empty Then
                accesWhere = "ContactFolders.NoUser IS NOT NULL"
            End If
        End If

        If PreferencesManager.getGeneralPreferences()("HideEndedUsersFolder") = True Then
            If accesWhere <> "" Then accesWhere = "(" & accesWhere & ") AND "
            accesWhere &= "DateFin IS NULL"
        End If

        Dim noFolderWhere As String = ""
        If noFolder <> 0 Then noFolderWhere = IIf(accesWhere <> "", " AND ", "") & "NoContactFolder=" & noFolder

        Dim curFolders As DataSet = DBLinker.getInstance.readDBForGrid("ContactFolders LEFT JOIN Utilisateurs ON Utilisateurs.NoUser = ContactFolders.NoUser", "ContactFolders.*", accesWhere & noFolderWhere)
        If curFolders Is Nothing Then Exit Sub

        With curFolders.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim newFolder As New ContactFolder(New DBItemableData(.Item(i)))
                If noFolder <> 0 Then
                    Dim curFolder As ContactFolder = getItemable(noFolder)
                    removeItemable(curFolder)
                End If
                addItemable(newFolder)
            Next i
        End With

        _isLoading = False
    End Sub

    Protected Overrides ReadOnly Property isLoading() As Boolean
        Get
            Return _isLoading
        End Get
    End Property
End Class
