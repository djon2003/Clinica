Public Class InternalDBFolder
    Inherits FolderBase

    Private _IsHidden As Boolean

#Region "Properties"
    Public ReadOnly Property noDBFolder() As Integer
        Get
            Return noFolder
        End Get
    End Property

    Public Property dbFolder() As String
        Get
            Return folder
        End Get
        Set(ByVal value As String)
            folder = value
        End Set
    End Property

    Public Property isHidden() As Boolean
        Get
            Return _IsHidden
        End Get
        Set(ByVal value As Boolean)
            _IsHidden = value
        End Set
    End Property

#End Region

    Public Sub New()
        MyBase.New(FolderType.DB)
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        MyBase.New(FolderType.DB, data)
    End Sub

    Public Overrides Sub delete()
        Dim returning As String = DBHelper.deleteFolder(Me.noUser, folder, "DBFolders", "DBFolder", "", False)
        Dim noUser As Integer = Me.noUser
        Dim realPath As String = Me.folder
        Dim dbItemsToDel() As String = DBLinker.getInstance.readOneDBField("SELECT DBItemFile FROM DBItems WHERE NoDBFolder IN (SELECT NoDBFolder FROM DBFolders  WHERE NoUser" & IIf(noUser = 0, " IS NULL", "=" & noUser) & " AND (DBFolder LIKE '" & realPath.Replace("'", "''") & "\%' OR DBFolder = '" & realPath.Replace("'", "''") & "'));")
        If dbItemsToDel IsNot Nothing AndAlso dbItemsToDel.Length <> 0 Then
            For i As Integer = 0 To dbItemsToDel.GetUpperBound(0)
                IO.File.Delete(appPath & bar(appPath) & "DB\" & dbItemsToDel(i))
            Next i
        End If

        returning &= "DELETE FROM DBItems WHERE NoDBFolder NOT IN (SELECT NoDBFolder FROM DBFolders);DELETE FROM DBItemsDroitsAcces WHERE NoDBItem NOT IN (SELECT NoDBItem FROM DBItems);"

        DBLinker.executeSQLScript(returning)

        onDeleted()

        'TODO : use If autoSendUpdateOnDelete Then 
    End Sub

    Protected Overrides Function getFolderColumnName() As String
        Return "DBFolder"
    End Function

    Protected Overrides Function getNoFolderColumnName() As String
        Return "NoDBFolder"
    End Function

    Protected Overrides Function getTableName() As String
        Return "DBFolders"
    End Function

    Public Overloads Shared Function exists(ByVal folderPath As String) As Boolean
        Return FolderBase.exists(folderPath, "DBFolders", "DBFolder")
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        _IsHidden = properties("ishidden")
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        properties("ishidden") = _IsHidden
    End Sub
End Class
