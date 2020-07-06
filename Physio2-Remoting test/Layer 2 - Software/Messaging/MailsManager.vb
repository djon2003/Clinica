Public Class MailsManager
    Inherits ManagerBase(Of MailsManager)
    Implements IDataConsumer(Of DataInternalUpdate)

    Private foldersList As MailFoldersList
    Private _MailAccounts As New Generic.List(Of MailAccount)
    Private _mainFromEmailAddress As String = String.Empty

    Protected Sub New()
        MyBase.New()
        InternalUpdatesManager.getInstance.addConsumer(Me)
        foldersList = MailFoldersList.getInstance()
        loadMailAccounts()
    End Sub

    Public Shared ReadOnly Property mainFromEmailAddress() As String
        Get
            Dim mySelf As MailsManager = getInstance()
            Return mySelf._mainFromEmailAddress
        End Get
    End Property

    Public ReadOnly Property folderType() As Type
        Get
            Return foldersList.managedType()
        End Get
    End Property

#Region "Mail methods"
    Public Function loadMails(ByVal noMailFolder As Integer) As Generic.List(Of Mail)
        Dim mails As New Generic.List(Of Mail)
        Dim mailsData As DataSet = DBLinker.getInstance.readDBForGrid("Mails", "[NoMail] ,[NoMailFolder] ,[From] ,[To] ,[CC] ,[NoUserFrom] ,[NoUserTo] ,[AffDate] ,[NoClient] ,[Subject] ,[IsRead] ,[Message] ,[FilesAttached] ,[HasSentFeedBack] ,[POPServer] ,[ServerIdent] ,[POPUser]", "(AffDate IS NULL OR AffDate <= GETDATE()) AND NoMailFolder=" & noMailFolder)
        If mailsData Is Nothing OrElse mailsData.Tables.Count = 0 OrElse mailsData.Tables(0).Rows.Count = 0 Then Return mails

        For Each curRow As DataRow In mailsData.Tables(0).Rows
            mails.Add(New Mail(New DBItemableData(curRow)))
        Next

        Return mails
    End Function
#End Region

#Region "MailFolder methods"
    Public Function addMailFolder(ByVal path As String, Optional ByRef noMailFolder As Integer = 0, Optional ByVal isSendingFolder As Boolean = False) As String
        Dim returning As String = foldersList.addItemable(path, noMailFolder)

        If isSendingFolder Then
            Dim newFolder As MailFolder = getMailFolder(noMailFolder)
            newFolder.isSendingFolder = True
            newFolder.saveData()
        End If

        Return returning
    End Function

    Public Function getMailFolder(ByVal path As String) As MailFolder
        Return foldersList.getItemable(path)
    End Function

    Public Function getMailFolder(ByVal noUser As Integer, ByVal realPath As String) As MailFolder
        Return foldersList.getItemable(FolderBase.getPath(noUser, realPath))
    End Function

    Public Function getMailFolder(ByVal noMailFolder As Integer) As MailFolder
        Return foldersList.getItemable(noMailFolder)
    End Function

    Public Function getMailFolders() As Generic.List(Of MailFolder)
        Return foldersList.getItemables()
    End Function
#End Region

#Region "MailAccount methods"

    Public Function addMailAccount(ByVal name As String) As String
        If MailAccount.exists(name) Then
            Return "Le nom du compte de courriel est déjà en utilisation"
        End If

        Dim cle As String = Chaines.getPasswordKey
        Dim newMailAccount As New MailAccount
        newMailAccount.accountName = name
        newMailAccount.passwordKey = cle
        newMailAccount.popServer = New MailAccount.MailAccountServer("", MailAccount.DEFAULT_INCOMING_SERVER_PORT)
        newMailAccount.smtpServer = New MailAccount.MailAccountServer("", MailAccount.DEFAULT_OUTGOING_SERVER_PORT)
        newMailAccount.saveData()
        _MailAccounts.Add(newMailAccount)
        AddHandler newMailAccount.deleted, AddressOf mailAccount_Deleted
        InternalUpdatesManager.getInstance.sendUpdate("MailAccounts()")

        Return ""
    End Function

    Private Sub mailAccount_Deleted(ByVal sender As IDBItemable)
        _MailAccounts.Remove(sender)
    End Sub



    Public Function getMailAccount(ByVal name As String) As MailAccount
        For i As Integer = 0 To _MailAccounts.Count - 1
            If _MailAccounts(i).accountName = name Then Return _MailAccounts(i)
        Next i

        Return Nothing
    End Function

    Public Function getMailAccount(ByVal noMailAccount As Integer) As MailAccount
        For i As Integer = 0 To _MailAccounts.Count - 1
            If _MailAccounts(i).noMailAccount = noMailAccount Then Return _MailAccounts(i)
        Next i

        Return Nothing
    End Function

    Public Function getMailAccounts() As Generic.List(Of MailAccount)
        Return _MailAccounts
    End Function

    Private Sub loadMailAccounts()
        _MailAccounts.Clear()

        Dim curAccounts As DataSet = DBLinker.getInstance.readDBForGrid("MailAccounts", "MailAccounts.*")
        If curAccounts Is Nothing Then Exit Sub

        'TODO : Detect main address like this is not perfect. Shall have a new account param which tells the main one
        Dim _mainFromEmailAddress As String = String.Empty
        With curAccounts.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                _MailAccounts.Add(New MailAccount(New DBItemableData(.Item(i))))
                If _mainFromEmailAddress = String.Empty AndAlso _MailAccounts(i).commonAccount Then _mainFromEmailAddress = _MailAccounts(i).email
                AddHandler _MailAccounts(i).deleted, AddressOf mailAccount_Deleted
            Next i

            If .Count <> 0 AndAlso _mainFromEmailAddress = String.Empty Then _mainFromEmailAddress = _MailAccounts(0).email
        End With

        Me._mainFromEmailAddress = _mainFromEmailAddress
    End Sub

#End Region

#Region "DataConsumer implementation"
    Public Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
        If dataReceived.fromExternal AndAlso dataReceived.function = "MailAccounts" Then
            loadMailAccounts()
        End If
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return 0
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function
#End Region

    Public Shared Function fillReplyTemplate(ByVal message As String, ByVal from As String, ByVal accountLinked As String, ByVal [To] As String, ByVal [Date] As String, ByVal subject As String, Optional ByVal title As String = "message d'origine") As String
        Dim replyTemplate As String = String.Join(vbCrLf, readFile("Data\reply.html"))
        replyTemplate = replyTemplate.Replace("###Title###", title)
        replyTemplate = replyTemplate.Replace("###Message###", message)
        replyTemplate = replyTemplate.Replace("###From###", from.Replace("<", "&lt;").Replace(">", "&gt;") & vbCrLf)
        replyTemplate = replyTemplate.Replace("###AccountLinked###", accountLinked)

        replyTemplate = replyTemplate.Replace("###To###", [To].Replace("<", "&lt;").Replace(">", "&gt;"))
        replyTemplate = replyTemplate.Replace("###Date###", [Date])
        replyTemplate = replyTemplate.Replace("###Sujet###", subject.Replace("<", "&lt;").Replace(">", "&gt;") & vbCrLf)

        Return replyTemplate
    End Function

End Class
