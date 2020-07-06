Public Class ContactFolder
    Inherits FolderBase

#Region "Properties"
    Public ReadOnly Property noContactFolder() As Integer
        Get
            Return noFolder
        End Get
    End Property

    Public Property contactFolder() As String
        Get
            Return folder
        End Get
        Set(ByVal value As String)
            folder = value
        End Set
    End Property
#End Region

    Public Sub New()
        MyBase.New(FolderType.Contact)
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        MyBase.New(FolderType.Contact, data)
    End Sub

    Public Overrides Sub delete()
        Dim returning As String = DBHelper.deleteFolder(noUser, folder, "ContactFolders", "ContactFolder", "DELETE FROM Contacts WHERE NoContactFolder NOT IN (SELECT NoContactFolder FROM ContactFolders);", True)

        'TODO: use If autoSendUpdateOnDelete Then 
        onDeleted()
    End Sub

    Protected Overrides Function getFolderColumnName() As String
        Return "ContactFolder"
    End Function

    Protected Overrides Function getNoFolderColumnName() As String
        Return "NoContactFolder"
    End Function

    Protected Overrides Function getTableName() As String
        Return "ContactFolders"
    End Function

    Public Overloads Shared Function folderExists(ByVal folderPath As String) As Boolean
        Return FolderBase.exists(folderPath, "ContactFolders", "ContactFolder")
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)

    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
