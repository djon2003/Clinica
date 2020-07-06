Public Class Mail
    Inherits DBItemableBase

    Private _NoMail, _NoUserFrom, _NoMailFolder, _NoUserTo, _NoClient As Integer
    Private _IsRead, _HasSentFeedBack As Boolean
    Private _AffDate As Date
    Private _From As String = "", _To As String = "", _CC As String = "", _Subject As String = "", _Message As String = ""
    Private _FilesAttached As String = "", _Source As String = "", _POPServer As String = "", _POPUser As String = "", _ServerIdent As String = ""
    Private isSourceGotten As Boolean = False

    Public Shared lastCopyPath As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\MailCopy"
    Private curAccount As MailAccount


#Region "Constructors"
    Public Sub New()
        If MailsManager.getInstance.getMailAccounts.Count <> 0 Then curAccount = MailsManager.getInstance.getMailAccounts(0)
    End Sub

    Public Sub New(ByVal loadingData As DBItemableData)
        MyBase.New()
        If MailsManager.getInstance.getMailAccounts.Count <> 0 Then curAccount = MailsManager.getInstance.getMailAccounts(0)

        loadData(loadingData)
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property hasSource() As Boolean
        Get
            Return _POPServer <> String.Empty
        End Get
    End Property

    Public Property serverIdent() As String
        Get
            Return _ServerIdent
        End Get
        Set(ByVal value As String)
            _ServerIdent = value
        End Set
    End Property

    Public Property popUser() As String
        Get
            Return _POPUser
        End Get
        Set(ByVal value As String)
            _POPUser = value
        End Set
    End Property

    Public Property popServer() As String
        Get
            Return _POPServer
        End Get
        Set(ByVal value As String)
            _POPServer = value
        End Set
    End Property

    Public Property source() As String
        Get
            If Not isSourceGotten Then loadSource()

            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
        End Set
    End Property

    Public Property filesAttached() As String
        Get
            Return _FilesAttached
        End Get
        Set(ByVal value As String)
            _FilesAttached = value
        End Set
    End Property

    Public Property message() As String
        Get
            Return _Message
        End Get
        Set(ByVal value As String)
            _Message = value
        End Set
    End Property

    Public Property subject() As String
        Get
            Return _Subject
        End Get
        Set(ByVal value As String)
            _Subject = value
        End Set
    End Property

    Public Property cc() As String
        Get
            Return _CC
        End Get
        Set(ByVal value As String)
            _CC = value
        End Set
    End Property

    Public Property [to]() As String
        Get
            Return _To
        End Get
        Set(ByVal value As String)
            _To = value
        End Set
    End Property

    Public Property from() As String
        Get
            Return _From
        End Get
        Set(ByVal value As String)
            _From = value
        End Set
    End Property

    Public Property affDate() As Date
        Get
            Return _AffDate
        End Get
        Set(ByVal value As Date)
            _AffDate = value
        End Set
    End Property

    Public Property hasSentFeedBack() As Boolean
        Get
            Return _HasSentFeedBack
        End Get
        Set(ByVal value As Boolean)
            _HasSentFeedBack = value
        End Set
    End Property

    Public Property isRead() As Boolean
        Get
            Return _IsRead
        End Get
        Set(ByVal value As Boolean)
            _IsRead = value
        End Set
    End Property

    Public Property noClient() As Integer
        Get
            Return _NoClient
        End Get
        Set(ByVal value As Integer)
            _NoClient = value
        End Set
    End Property

    Public Property noUserTo() As Integer
        Get
            Return _NoUserTo
        End Get
        Set(ByVal value As Integer)
            _NoUserTo = value
        End Set
    End Property

    Public Property noMailFolder() As Integer
        Get
            Return _NoMailFolder
        End Get
        Set(ByVal value As Integer)
            _NoMailFolder = value
        End Set
    End Property

    Public Property noUserFrom() As Integer
        Get
            Return _NoUserFrom
        End Get
        Set(ByVal value As Integer)
            _NoUserFrom = value
        End Set
    End Property

    Public ReadOnly Property noMail() As Integer
        Get
            Return _NoMail
        End Get
    End Property

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return _NoMail
        End Get
    End Property

#End Region

    Private Sub loadSource()
        Dim source As Object = DBLinker.getInstance().readScalar("Mails", "Source", "NoMail = " & _NoMail)
        If source IsNot Nothing Then
            If Not source.Equals(DBNull.Value) Then _Source = source
            isSourceGotten = True
        End If
    End Sub

    Public Overrides Sub delete()
        Dim delPath As String = PreferencesManager.getUserPreferences()("mailFolderForDelMsg")
        Dim moveMailToDelFolder As Boolean = PreferencesManager.getUserPreferences()("sendDeleteMailToTrash") = True AndAlso delPath.Trim() <> String.Empty
        Dim curFolder As MailFolder = getMailFolder()
        If moveMailToDelFolder AndAlso curFolder.isDeletingFolder = False Then
            Dim delFolder As MailFolder = MailsManager.getInstance.getMailFolder(curFolder.noUser, delPath)
            If delFolder Is Nothing Then
                MailsManager.getInstance.addMailFolder(FolderBase.getPath(curFolder.noUser, delPath))
                delFolder = MailsManager.getInstance.getMailFolder(curFolder.noUser, delPath)
                delFolder.isDeletingFolder = True
                delFolder.saveData()
            End If

            Me.noMailFolder = delFolder.noMailFolder

            Me.saveData()
        Else
            'Delete attached files
            Try
                Fichiers.deltree(appPath & bar(appPath) & "Data\Mails\" & noMail)
            Catch ex As System.IO.IOException
                Dim list As New Generic.List(Of IDBItemable)
                list.Add(Me)
                Throw New DBItemableUndeletable(list)
            End Try

            'Delete in SQL DB
            DBLinker.getInstance.delDB("Mails", "NoMail", noMail, False)
        End If


        onDeleted()

        'TODO : use If autoSendUpdateOnDelete Then 
    End Sub


    Public Function getMailFolder() As MailFolder
        Return MailsManager.getInstance.getMailFolder(_NoMailFolder)
    End Function

    Private Function sendEmail(ByVal emailTo As String, ByVal subject As String, ByVal emailMessage As String, Optional ByVal emailCC As String = "", Optional ByVal emailBCC As String = "", Optional ByVal emailAttachs As String = "", Optional ByVal emailFrom As String = "", Optional ByVal showErrMsg As Boolean = True, Optional ByVal showLog As Boolean = True) As Boolean
        If curAccount Is Nothing AndAlso MailsManager.getInstance.getMailAccounts.Count <> 0 Then curAccount = MailsManager.getInstance.getMailAccounts(0)
        If curAccount Is Nothing Then Return False

        Return emailSending(curAccount, "", emailTo, emailCC, emailBCC, subject, False, emailMessage, emailAttachs, IIf(showLog, Nothing, ""), IIf(showLog, Nothing, ""), showErrMsg)
    End Function

    Public Shared Sub clearTempCopyFolder()
        deltree(lastCopyPath)
        ensureGoodPath(lastCopyPath)
    End Sub

    Public Shared Sub changeLastCopyPath()
        lastCopyPath = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\MailCopy" & (New Random).Next()
    End Sub

    Public Sub copy()
        Dim curPath As String = "Data\Mails\" & _NoMail

        Dim copyBasePath As String = lastCopyPath & bar(lastCopyPath) & _NoMail

        xCopy(appPath & bar(appPath) & curPath, copyBasePath)
        Dim mycontent As New System.Text.StringBuilder
        mycontent.AppendLine(_NoMail).AppendLine(_NoUserFrom).AppendLine(_NoMailFolder).AppendLine(_NoUserTo).AppendLine(_NoClient)
        mycontent.AppendLine(_IsRead).AppendLine(_HasSentFeedBack)
        mycontent.AppendLine(DateFormat.getTextDate(_AffDate) & " " & DateFormat.getTextDate(_AffDate, DateFormat.TextDateOptions.FullTime))
        mycontent.AppendLine(_From).AppendLine(_To).AppendLine(_CC).AppendLine(_Subject).AppendLine(_POPServer).AppendLine(_ServerIdent).AppendLine(_POPUser)

        IO.Directory.CreateDirectory(copyBasePath)
        IO.File.WriteAllText(copyBasePath & "\info", mycontent.ToString)
        If source <> "" Then
            IO.File.WriteAllText(copyBasePath & "\source", _Source)
        End If
        If _FilesAttached <> "" Then
            IO.File.WriteAllText(copyBasePath & "\files", _FilesAttached)
        End If
        If _Message <> "" Then
            IO.File.WriteAllText(lastCopyPath & bar(lastCopyPath) & _NoMail & "\message", _Message)
        End If
    End Sub

    Public Shared Sub paste(ByVal noMailToCopy As Integer, ByVal destFolder As MailFolder)
        Dim copyBasePath As String = lastCopyPath & bar(lastCopyPath) & noMailToCopy

        Dim infos() As String = IO.File.ReadAllLines(copyBasePath & "\info")
        Dim source As String = ""
        If IO.File.Exists(copyBasePath & "\source") Then source = IO.File.ReadAllText(copyBasePath & "\source")
        Dim myMessage As String = ""
        If IO.File.Exists(copyBasePath & "\message") Then myMessage = IO.File.ReadAllText(copyBasePath & "\message")
        Dim files As String = ""
        If IO.File.Exists(copyBasePath & "\files") Then files = IO.File.ReadAllText(copyBasePath & "\files")

        Dim newMail As New Mail
        newMail.source = source
        newMail.filesAttached = files
        newMail.message = myMessage
        newMail.noUserFrom = infos(1)
        newMail.noMailFolder = destFolder.noMailFolder
        newMail.noUserTo = infos(3)
        newMail.noClient = infos(4)
        newMail.isRead = infos(5)
        newMail.hasSentFeedBack = infos(6)
        newMail.affDate = infos(7)
        newMail.from = infos(8)
        newMail.[to] = infos(9)
        newMail.cc = infos(10)
        newMail.subject = infos(11)
        newMail.popServer = infos(12)
        newMail.serverIdent = infos(13)
        newMail.popUser = infos(14)
        newMail.isSourceGotten = Not source = String.Empty

        newMail.saveData()

        If IO.Directory.Exists(copyBasePath & "\attach") Then
            xCopy(copyBasePath & "\attach", appPath & bar(appPath) & "Data\Mails\" & newMail.noMail & "\attach")
        End If
    End Sub

    Public Sub sendFeedBack()
        Dim nom As String = "lu"
        Dim verbe As String = "a été lu"
        If isRead = False Then verbe = "a été supprimé sans avoir été lu" : nom = "non lu"

        If noUserFrom > 0 Then
            AlertsManager.getInstance.addAlert("Votre message """ & subject & """ " & verbe & " par " & UsersManager.currentUser.toString(), noUserFrom, AlertsManager.AType.OpenMailSystem, getMailFolder().toString() & "\" & noMail, Date.Now.AddHours(1), , , True)
        Else
            Dim curUser As User = UsersManager.currentUser
            If sendEmail(from, "[Clinica] Message " & nom & " : " & subject, "Votre message """ & subject & """ " & verbe & " par " & curUser.firstName & " " & curUser.lastName, , , , , False, False) = False Then
                Dim errorMsg As String = "Impossible d'envoyer la notification de lecture du message """ & subject & """." & vbCrLf & vbCrLf & "Raison :" & vbCrLf
                If curAccount Is Nothing Then
                    errorMsg &= "Il n'existe aucun compte de courriel."
                ElseIf curAccount.smtpServer.server.Trim = "" Then
                    errorMsg &= "Le compte de courriel """ & curAccount.toString & """ n'a pas de serveur de courriel sortant (SMTP)."
                Else
                    errorMsg &= "Une erreur est survenue lors de l'envoi de la confirmation. Veuillez vérifier le serveur d'envoi du compte de courriel """ & curAccount.toString & """"
                End If
                MessageBox.Show(errorMsg, "Confirmation de lecture non envoyée", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        DBLinker.getInstance.updateDB("Mails", "HasSentFeedBack=1", "NoMail", noMail, False)
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _NoMail = curData("NoMail")
        _NoMailFolder = curData("NoMailFolder")
        If curData("IsRead") IsNot DBNull.Value Then _IsRead = curData("IsRead")
        If curData("HasSentFeedBack") IsNot DBNull.Value Then _HasSentFeedBack = curData("HasSentFeedBack")
        If curData("AffDate") IsNot DBNull.Value Then _AffDate = curData("AffDate")
        If curData("From") IsNot DBNull.Value Then _From = curData("From")
        If curData("To") IsNot DBNull.Value Then _To = curData("To")
        If curData("CC") IsNot DBNull.Value Then _CC = curData("CC")
        If curData("Subject") IsNot DBNull.Value Then _Subject = curData("Subject")
        If curData("Message") IsNot DBNull.Value Then _Message = curData("Message")
        If curData("FilesAttached") IsNot DBNull.Value Then _FilesAttached = curData("FilesAttached")
        If curData("POPServer") IsNot DBNull.Value Then _POPServer = curData("POPServer")
        If curData("POPUser") IsNot DBNull.Value Then _POPUser = curData("POPUser")
        If curData("ServerIdent") IsNot DBNull.Value Then _ServerIdent = curData("ServerIdent")
        If curData("NoUserTo") IsNot DBNull.Value Then _NoUserTo = curData("NoUserTo")
        If curData("NoUserFrom") IsNot DBNull.Value Then _NoUserFrom = curData("NoUserFrom")
        If curData("NoClient") IsNot DBNull.Value Then _NoClient = curData("NoClient")
    End Sub

    Public Overrides Sub saveData()
        If _NoMail = 0 Then
            DBLinker.getInstance.writeDB("Mails", "NoClient, NoUserFrom,NoMailFolder,[From],CC,[To],NoUserTo,AffDate,Subject,IsRead,Message,FilesAttached,HasSentFeedBack,Source,POPServer,ServerIdent, POPUser", IIf(_NoClient = 0, "null", _NoClient) & "," & IIf(_NoUserFrom = 0, "null", _NoUserFrom) & "," & noMailFolder & ",'" & _From.Replace("'", "''") & "','" & _CC.Replace("'", "''") & "','" & _To.Replace("'", "''") & "'," & IIf(noUserTo = 0, "null", noUserTo) & ",'" & DateFormat.getTextDate(Me.affDate) & " " & DateFormat.getTextDate(Me.affDate, DateFormat.TextDateOptions.ShortTime) & "','" & subject.Replace("'", "''") & "','" & isRead & "','" & message.Replace("'", "''") & "','" & _FilesAttached.Replace("'", "''") & "','" & Me.hasSentFeedBack & "','" & Me.source.Replace("'", "''") & "','" & Me.popServer.Replace("'", "''") & "','" & Me.serverIdent.Replace("'", "''") & "','" & Me.popUser.Replace("'", "''") & "'", , , , _NoMail)
        Else
            DBLinker.getInstance.updateDB("Mails", "NoMailFolder=" & _NoMailFolder, "NoMail", _NoMail, False)
        End If

        'TODO: If autoSendUpdateOnSave Then 
    End Sub
End Class
