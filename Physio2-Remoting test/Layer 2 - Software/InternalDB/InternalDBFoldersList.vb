Public Class InternalDBFoldersList
    Inherits FoldersListBase(Of InternalDBFoldersList, InternalDBFolder)

    Private _isLoading As Boolean = False

    Protected Sub New()
        MyBase.new()
    End Sub

    Protected Overrides Sub load(ByVal noFolder As Integer)
        If noFolder = 0 Then MyBase.clear()
        If currentDroitAcces Is Nothing Then Exit Sub

        _isLoading = True

        Dim accesWhere As String = "DBFolders.NoUser=" & Me.currentNoUser
        If currentDroitAcces.GetUpperBound(0) >= 98 Then
            If currentDroitAcces(98) Then accesWhere = ""
            If currentDroitAcces(97) And accesWhere <> "" Then
                accesWhere &= " OR DBFolders.NoUser IS NULL"
            End If
        End If

        If currentDroitAcces(2) = False Then
            If accesWhere <> "" Then accesWhere = "(" & accesWhere & ") AND "
            accesWhere &= "FolderProperties NOT LIKE '%§IsHidden§=§True§%'"
        End If

        If PreferencesManager.getGeneralPreferences()("HideEndedUsersFolder") = True Then
            If accesWhere <> "" Then accesWhere = "(" & accesWhere & ") AND "
            accesWhere &= "DateFin IS NULL"
        End If

        Dim noFolderWhere As String = ""
        If noFolder <> 0 Then noFolderWhere = IIf(accesWhere <> "", " AND ", "") & "NoDBFolder=" & noFolder

        Dim curFolders As DataSet = DBLinker.getInstance.readDBForGrid("DBFolders LEFT JOIN Utilisateurs ON Utilisateurs.NoUser = DBFolders.NoUser", "DBFolders.*", accesWhere & noFolderWhere)
        If curFolders Is Nothing Then Exit Sub

        With curFolders.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim newFolder As New InternalDBFolder(New DBItemableData(.Item(i)))
                If noFolder <> 0 Then
                    Dim curFolder As InternalDBFolder = MyBase.getItemable(noFolder)
                    MyBase.removeItemable(curFolder)
                End If

                Me.addItemable(newFolder)
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
