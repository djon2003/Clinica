Public Class MailFolder
    Inherits FolderBase

    Private _IsSendingFolder, _isDeletingFolder As Boolean
    Private nbNewMails As Integer = 0

#Region "Properties"
    Public Property isSendingFolder() As Boolean
        Get
            Return _IsSendingFolder
        End Get
        Set(ByVal value As Boolean)
            _IsSendingFolder = value
        End Set
    End Property

    Public Property isDeletingFolder() As Boolean
        Get
            Return _isDeletingFolder
        End Get
        Set(ByVal value As Boolean)
            _isDeletingFolder = value
        End Set
    End Property


    Public ReadOnly Property noMailFolder() As Integer
        Get
            Return noFolder
        End Get
    End Property

    Public Property mailFolder() As String
        Get
            Return folder
        End Get
        Set(ByVal value As String)
            folder = value
        End Set
    End Property
#End Region

    Public Sub New()
        MyBase.New(FolderType.Mail)
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        MyBase.New(FolderType.Mail, data)
        checkIfUnreadMails()
    End Sub

    Private Function isUnreadMails() As Boolean
        nbNewMails = 0
        Dim mails() As String = DBLinker.getInstance.readOneDBField("Mails", "NoMail", "WHERE NoMailFolder=" & Me.noMailFolder & " AND IsRead=0 AND (AffDate IS NULL OR AffDate <= GETDATE())")

        If mails IsNot Nothing AndAlso mails.Length <> 0 Then
            nbNewMails = mails.Length
            Return True
        End If

        Return False
    End Function

    Public Overrides Sub delete()
        Dim delPath As String = PreferencesManager.getUserPreferences()("mailFolderForDelMsg")
        Dim moveMailToDelFolder As Boolean = PreferencesManager.getUserPreferences()("sendDeleteMailToTrash") = True AndAlso delPath.Trim() <> String.Empty

        Dim isUserFolder As Boolean = Me.noUser <> 0 AndAlso mailFolder = ""

        If Not isUserFolder AndAlso moveMailToDelFolder AndAlso Me.isDeletingFolder = False AndAlso Not Me.mailFolder.StartsWith(delPath & "\") Then
            'Move folder to delete folder. Subfolders changes are taken in charge by FoldersListBase which will detected the change upon save
            Dim delFolder As MailFolder = MailsManager.getInstance.getMailFolder(Me.noUser, delPath)
            If delFolder Is Nothing Then
                MailsManager.getInstance.addMailFolder(FolderBase.getPath(Me.noUser, delPath))
                delFolder = MailsManager.getInstance.getMailFolder(Me.noUser, delPath)
                delFolder.isDeletingFolder = True
                delFolder.saveData()
            End If

            Dim mailFolder As String = String.Empty
            If Me.mailFolder <> String.Empty Then mailFolder = "\" & Me.mailFolder.Substring(Me.mailFolder.LastIndexOf("\") + 1)
            Me.mailFolder = delPath & mailFolder

            Me.saveData()
        Else
            'Real deletion of folder
            DBHelper.deleteFolder(noUser, folder, "MailFolders", "MailFolder", "DELETE FROM Mails WHERE NoMailFolder NOT IN (SELECT NoMailFolder FROM MailFolders);", True)
            'TODO: use If autoSendUpdateOnDelete Then 
            onDeleted()
        End If
    End Sub

    Public Sub checkIfUnreadMails()
        bold = isUnreadMails()
        setNbNewItems(nbNewMails)
    End Sub

    Protected Overrides Function getFolderColumnName() As String
        Return "MailFolder"
    End Function

    Protected Overrides Function getNoFolderColumnName() As String
        Return "NoMailFolder"
    End Function

    Protected Overrides Function getTableName() As String
        Return "MailFolders"
    End Function

    Public Overloads Shared Function folderExists(ByVal folderPath As String) As Boolean
        Return FolderBase.exists(folderPath, "MailFolders", "MailFolder")
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        If properties.ContainsKey("issendingfolder") Then _IsSendingFolder = properties("issendingfolder")
        If properties.ContainsKey("isdeletingfolder") Then _isDeletingFolder = properties("isdeletingfolder")
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        If _IsSendingFolder Then
            properties("issendingfolder") = _IsSendingFolder
        ElseIf properties.ContainsKey("issendingfolder") Then
            properties.Remove("issendingfolder")
        End If

        If _isDeletingFolder Then
            properties("isdeletingfolder") = _isDeletingFolder
        ElseIf properties.ContainsKey("isdeletingfolder") Then
            properties.Remove("isdeletingfolder")
        End If
    End Sub
End Class
