Public Class MailFoldersList
    Inherits FoldersListBase(Of MailFoldersList, MailFolder)

    Private _isLoading As Boolean = False

    Protected Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function getItemables() As System.Collections.Generic.List(Of MailFolder)
        Dim items As New Generic.List(Of MailFolder)

        Dim allUsers As Boolean = currentDroitAcces(41)
        Dim generalFolders As Boolean = currentDroitAcces(39)
        Dim hideEndedUsers As Boolean = PreferencesManager.getGeneralPreferences()("HideEndedUsersFolder")

        For Each curFolder As MailFolder In MyBase.getItemables()
            Dim canUser As Boolean = curFolder.noUser <> 0 AndAlso (curFolder.noUser = ConnectionsManager.currentUser OrElse allUsers)
            Dim canGeneral As Boolean = (curFolder.noUser = 0 AndAlso generalFolders)
            Dim canEnded As Boolean = curFolder.noUser <> 0 AndAlso (Not hideEndedUsers OrElse UsersManager.getInstance().getUser(curFolder.noUser).endingDate >= Date.Today)
            If canGeneral OrElse (canUser AndAlso canEnded) Then
                items.Add(curFolder)
            End If
        Next

        Return items
    End Function

    Protected Overrides Sub load(ByVal noFolder As Integer)
        If noFolder = 0 Then clear()
        If currentDroitAcces Is Nothing Then Exit Sub

        _isLoading = True

        Dim noFolderWhere As String = ""
        If noFolder <> 0 Then noFolderWhere = "NoMailFolder=" & noFolder

        Dim curFolders As DataSet = DBLinker.getInstance.readDBForGrid("MailFolders LEFT JOIN Utilisateurs ON Utilisateurs.NoUser = MailFolders.NoUser", "MailFolders.*", noFolderWhere)
        If curFolders Is Nothing Then Exit Sub

        With curFolders.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim newFolder As New MailFolder(New DBItemableData(.Item(i)))
                If noFolder <> 0 Then
                    Dim curFolder As MailFolder = MyBase.getItemable(noFolder)
                    MyBase.removeItemable(curFolder)
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
