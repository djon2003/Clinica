Public Class MailAccount
    Inherits DBItemableBase

    Private Const changeQuestion As String = "Désirez-vous tenter de changer les informations de connexion ?"
    Private Const suggestion As String = "Veuillez réessayer plus tard et/ou demander que l'on vérifie les informations du serveur entrant."

    Public Const DEFAULT_INCOMING_SERVER_PORT As Integer = 110
    Public Const DEFAULT_SECURED_INCOMING_SERVER_PORT As Integer = 995
    Public Const DEFAULT_OUTGOING_SERVER_PORT As Integer = 25

    Public Event pop3Message(ByVal message As String)
    Public Event pop3Downloading(ByVal status As POP3DownloadingStatus)
    Public Event pop3DownloadEnded(ByVal sender As MailAccount, ByVal pop As Protocols.POP3)
    Public Event pop3Cancelled(ByVal sender As MailAccount)

#Region "MailAccountServer class"
    Public Class MailAccountServer
        Private _Server As String = ""
        Private _Port As Integer = 0
        Private _IsSecured As Boolean = False

        Private Sub New()
        End Sub

        Public Sub New(ByVal server As String, ByVal port As Integer, Optional ByVal isSecured As Boolean = False)
            _Server = server
            _Port = port
            _IsSecured = isSecured
        End Sub

        Public ReadOnly Property server() As String
            Get
                Return _Server
            End Get
        End Property

        Public ReadOnly Property port() As Integer
            Get
                Return _Port
            End Get
        End Property

        Public ReadOnly Property isSecured() As Boolean
            Get
                Return _IsSecured
            End Get
        End Property
    End Class
#End Region

    Public Sub New()
    End Sub

    Public Sub New(ByVal name As String)
        loadData(name)
    End Sub

    Public Sub New(ByVal noMailAccount As Integer)
        loadData(noMailAccount)
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub

    Public Enum ManagedProtocols
        POP3 = 0
    End Enum


    Private noUserInbox As Integer = 0
    Private _NoMailAccount As Integer = 0
    Private _AccountName As String = ""
    Private _SendingName As String = ""
    Private _Email As String = ""
    Private _popServer As New MailAccountServer("", 0)
    Private _smtpServer As New MailAccountServer("", 0)
    Private _Username As String = ""
    Private _PasswordKey As String = ""
    Private _Password As String = ""
    Private _SavePassword, _KeepMSGOnServer, _IncludeInGeneralReception, _CommonAccount As Boolean
    Private _InboxFolderName As String = ""

    Private _smtpNeedAuthentication, _smtpSpecificCredential, _canSendEmail As Boolean
    Private _smtpAuthenUsername As String = ""
    Private _smtpPassword As String = ""
    Private _smtpPasswordKey As String = ""
    Private _smtpSavePassword As Boolean
    Private _IncomingProtocol As ManagedProtocols = ManagedProtocols.POP3
    Private _TimeoutInSeconds As Integer = 60
    Private nbEmailsDownloaded As Integer = 0

#Region "Propriétés"

    Public Property timeoutInSeconds() As Integer
        Get
            Return _TimeoutInSeconds
        End Get
        Set(ByVal value As Integer)
            _TimeoutInSeconds = value
        End Set
    End Property

    Public Property incomingProtocol() As ManagedProtocols
        Get
            Return _IncomingProtocol
        End Get
        Set(ByVal value As ManagedProtocols)
            _IncomingProtocol = value
        End Set
    End Property

    Public Property smtpPasswordKey() As String
        Get
            Return _smtpPasswordKey
        End Get
        Set(ByVal value As String)
            _smtpPasswordKey = value
        End Set
    End Property

    Public Property smtpPassword() As String
        Get
            Return _smtpPassword
        End Get
        Set(ByVal value As String)
            _smtpPassword = value
        End Set
    End Property

    Public Property smtpAuthenUsername() As String
        Get
            Return _smtpAuthenUsername
        End Get
        Set(ByVal value As String)
            _smtpAuthenUsername = value
        End Set
    End Property

    Public Property smtpNeedAuthentication() As Boolean
        Get
            Return _smtpNeedAuthentication
        End Get
        Set(ByVal value As Boolean)
            _smtpNeedAuthentication = value
        End Set
    End Property

    Public Property smtpSpecificCredential() As Boolean
        Get
            Return _smtpSpecificCredential
        End Get
        Set(ByVal value As Boolean)
            _smtpSpecificCredential = value
        End Set
    End Property

    Public Property canSendEmail() As Boolean
        Get
            Return _canSendEmail
        End Get
        Set(ByVal value As Boolean)
            _canSendEmail = value
        End Set
    End Property

    Public Property smtpSavePassword() As Boolean
        Get
            Return _smtpSavePassword
        End Get
        Set(ByVal value As Boolean)
            _smtpSavePassword = value
        End Set
    End Property

    Public ReadOnly Property noMailAccount() As Integer
        Get
            Return _NoMailAccount
        End Get
    End Property

    Public Property commonAccount() As Boolean
        Get
            Return _CommonAccount
        End Get
        Set(ByVal value As Boolean)
            _CommonAccount = value
        End Set
    End Property
    Public Property includeInGeneralReception() As Boolean
        Get
            Return _IncludeInGeneralReception
        End Get
        Set(ByVal value As Boolean)
            _IncludeInGeneralReception = value
        End Set
    End Property
    Public Property keepMSGOnServer() As Boolean
        Get
            Return _KeepMSGOnServer
        End Get
        Set(ByVal value As Boolean)
            _KeepMSGOnServer = value
        End Set
    End Property
    Public Property savePassword() As Boolean
        Get
            Return _SavePassword
        End Get
        Set(ByVal value As Boolean)
            _SavePassword = value
        End Set
    End Property

    Public Property smtpServer() As MailAccountServer
        Get
            Return _smtpServer
        End Get
        Set(ByVal value As MailAccountServer)
            _smtpServer = value
        End Set
    End Property
    Public Property popServer() As MailAccountServer
        Get
            Return _popServer
        End Get
        Set(ByVal value As MailAccountServer)
            _popServer = value
        End Set
    End Property

    Public Property inboxFolderName() As String
        Get
            Return _InboxFolderName
        End Get
        Set(ByVal value As String)
            _InboxFolderName = value
            If Me.inboxFolderName.IndexOf("(") <> -1 Then
                Dim tmpNUI As String = Me.inboxFolderName.Substring(Me.inboxFolderName.LastIndexOf("(") + 1)
                noUserInbox = tmpNUI.Substring(0, tmpNUI.Length - 1)
            Else
                noUserInbox = 0
            End If
        End Set
    End Property
    Public Property password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property passwordKey() As String
        Get
            Return _PasswordKey
        End Get
        Set(ByVal value As String)
            _PasswordKey = value
        End Set
    End Property
    Public Property username() As String
        Get
            Return _Username
        End Get
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
    Public Property sendingName() As String
        Get
            Return _SendingName
        End Get
        Set(ByVal value As String)
            _SendingName = value
        End Set
    End Property
    Public Property accountName() As String
        Get
            Return _AccountName
        End Get
        Set(ByVal value As String)
            _AccountName = value
        End Set
    End Property
#End Region

    Public Shared Function exists(ByVal noMailAccount As Integer) As Boolean
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("MailAccounts", "COUNT(*)", "WHERE NoMailAccount=" & noMailAccount)

        Return data.Tables(0).Rows(0)(0) > 0
    End Function

    Public Shared Function exists(ByVal name As String) As Boolean
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("MailAccounts", "COUNT(*)", "WHERE AccountName='" & name.Replace("'", "''") & "'")

        Return data.Tables(0).Rows(0)(0) > 0
    End Function

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("MailAccounts", "NoMailAccount", _NoMailAccount, False)
        If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("MailAccounts()")
        onDeleted()
    End Sub

    Public Overloads Sub loadData(ByVal noDBItem As Integer)
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("MailAccounts", "*", "WHERE NoMailAccount=" & _NoMailAccount)
        If data Is Nothing OrElse data.Tables(0).Rows.Count = 0 Then Exit Sub

        loadData(New DBItemableData(data.Tables(0).Rows(0)))
    End Sub

    Public Overloads Sub loadData(ByVal name As String)
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("MailAccounts", "*", "WHERE AccountName='" & name.Replace("'", "''") & "'")
        If data Is Nothing OrElse data.Tables(0).Rows.Count = 0 Then Exit Sub

        loadData(New DBItemableData(data.Tables(0).Rows(0)))
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _NoMailAccount = curData("NoMailAccount")
        _AccountName = curData("AccountName")
        _SendingName = curData("SendingName")
        _Email = curData("Email")
        _popServer = New MailAccountServer(curData("POPServer"), curData("POPPort"), curData("POPSecured"))
        _smtpServer = New MailAccountServer(curData("SMTPServer"), curData("SMTPPort"), curData("SMTPSecured"))
        _Username = curData("Username")
        _PasswordKey = curData("PasswordKey")
        _Password = curData("Password")
        _SavePassword = curData("SavePassword")
        _KeepMSGOnServer = curData("KeepMSGOnServer")
        _IncludeInGeneralReception = curData("IncludeInGeneralReception")
        _CommonAccount = curData("CommonAccount")
        _InboxFolderName = curData("InboxFolderName")
        If Me.inboxFolderName.IndexOf("(") <> -1 Then
            Dim tmpNUI As String = Me.inboxFolderName.Substring(Me.inboxFolderName.LastIndexOf("(") + 1)
            noUserInbox = tmpNUI.Substring(0, tmpNUI.Length - 1)
        Else
            noUserInbox = 0
        End If
        _smtpNeedAuthentication = curData("SMTPNeedAuthen")
        _smtpSpecificCredential = curData("SMTPSpecificCredential")
        _smtpAuthenUsername = curData("SMTPAuthenUsername")
        _smtpPassword = curData("SMTPPassword")
        _smtpPasswordKey = curData("SMTPPasswordKey")
        _smtpSavePassword = curData("SMTPSavePassword")
        _IncomingProtocol = curData("IncomingProtocol")
        _TimeoutInSeconds = curData("TimeoutInSeconds")
        If curData("CanSendEmail") IsNot DBNull.Value Then
            _canSendEmail = curData("CanSendEmail")
        Else
            _canSendEmail = True
        End If
    End Sub

    Public Overrides Sub saveData()
        If _NoMailAccount = 0 Then
            DBLinker.getInstance.writeDB("MailAccounts", "AccountName,SendingName,Email,POPServer,POPPort,POPSecured,SMTPServer,SMTPPort,SMTPSecured,Username, PasswordKey, Password, SavePassword, KeepMSGOnServer, IncludeInGeneralReception, CommonAccount,InboxFolderName," & _
            "SMTPNeedAuthen,SMTPSpecificCredential,SMTPAuthenUsername,SMTPPassword,SMTPPasswordKey,SMTPSavePassword,IncomingProtocol,TimeoutInSeconds, CanSendEmail", "'" & _AccountName.Replace("'", "''") & "','" & _SendingName.Replace("'", "''") & "','" & _Email.Replace("'", "''") & "','" & _popServer.server.Replace("'", "''") & "'," & _popServer.port & ",'" & _popServer.isSecured & "','" & _smtpServer.server.Replace("'", "''") & "'," & _smtpServer.port & ",'" & _smtpServer.isSecured & "','" & _Username.Replace("'", "''") & "','" & _PasswordKey & "','" & _Password & "','" & _SavePassword & "','" & _KeepMSGOnServer & "','" & _IncludeInGeneralReception & "','" & _CommonAccount & "','" & _InboxFolderName.Replace("'", "''") & "','" & _smtpNeedAuthentication & "','" & _smtpSpecificCredential & "','" & _smtpAuthenUsername & "','" & _smtpPassword & "','" & _smtpPasswordKey & "','" & _smtpSavePassword & "'," & _IncomingProtocol & "," & _TimeoutInSeconds & ",'" & _canSendEmail & "'", , , , _NoMailAccount)
        Else
            DBLinker.getInstance.updateDB("MailAccounts", "AccountName='" & _AccountName.Replace("'", "''") & "',SendingName='" & _SendingName.Replace("'", "''") & "',Email='" & _Email.Replace("'", "''") & "',POPServer='" & _popServer.server.Replace("'", "''") & "',POPPort=" & _popServer.port & ",POPSecured='" & _popServer.isSecured & "',SMTPServer='" & _smtpServer.server.Replace("'", "''") & "',SMTPPort=" & _smtpServer.port & ",SMTPSecured='" & _smtpServer.isSecured & "',Username='" & _Username.Replace("'", "''") & "',PasswordKey='" & _PasswordKey & "',Password='" & _Password & "',SavePassword='" & _SavePassword & "',KeepMSGOnServer='" & _KeepMSGOnServer & "',IncludeInGeneralReception='" & _IncludeInGeneralReception & "',CommonAccount='" & _CommonAccount & "',InboxFolderName='" & _InboxFolderName.Replace("'", "''") & "',SMTPNeedAuthen='" & _smtpNeedAuthentication & "',SMTPSpecificCredential='" & _smtpSpecificCredential & "',SMTPAuthenUsername='" & _smtpAuthenUsername & "',SMTPPassword='" & _smtpPassword & "',SMTPPasswordKey='" & _smtpPasswordKey & "',SMTPSavePassword='" & _smtpSavePassword & "',IncomingProtocol=" & _IncomingProtocol & ",TimeoutInSeconds=" & _TimeoutInSeconds & ",CanSendEmail='" & _canSendEmail & "'", "NoMailAccount", _NoMailAccount, False)
            onDataChanged()
        End If

        If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("MailAccounts()")
    End Sub

    Public Overrides Function toString() As String
        Return _AccountName & " (" & _SendingName & " <" & _Email & ">)"
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noMailAccount
        End Get
    End Property

    Public Sub gatherEmails()
        'Ensure internet connection
        If Not isConnectionAvailable() Then Throw New NoInternetException()

        ''Ensure unique usage by one user
        If lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, True, "Téléchargement des courriels du compte " & Me.toString, False) = False Then
            Throw New UserAlreadyUsingException("Le compte de courriel " & Me.toString & " est déjà en cours de téléchargement par un utilisateur")
        End If

        Dim gatherThread As New Threading.Thread(AddressOf internalGatherEmails)
        gatherThread.Start()
    End Sub

    Private Function askPopServer() As Boolean
        Dim oldServer As String = Me.popServer.server

        If Me.popServer.port <> DEFAULT_INCOMING_SERVER_PORT OrElse Me.popServer.isSecured Then
            oldServer &= ":" & Me.popServer.port & If(Me.popServer.isSecured, ":1", String.Empty)
        End If

        Dim myInputBoxPlus As New InputBoxPlus
        Dim newPopServer() As String = myInputBoxPlus("Veuillez entrer le serveur entrant du compte de courriel (" & Me.toString() & ") :" & vbCrLf & "(Doit être de la forme >> Serveur[:Port[:SSL]]) ([] = Optionel, SSL : 1 pour actif, 0 inactif)" & vbCrLf & "(Par défaut >> Serveur:" & MailAccount.DEFAULT_INCOMING_SERVER_PORT & ":0) (Ex : pop.videotron.ca:490)", "Serveur entrant", oldServer).Split(":")
        If newPopServer(0) = "" Then
            Return False
        End If

        Dim popServerPort As Integer = DEFAULT_INCOMING_SERVER_PORT
        Dim popSecured As Boolean = False

        If newPopServer.Length > 1 Then If Integer.TryParse(newPopServer(1), popServerPort) = False Then popServerPort = DEFAULT_INCOMING_SERVER_PORT
        If newPopServer.Length = 3 AndAlso newPopServer(2) = "1" Then popSecured = True

        Me.popServer = New MailAccountServer(newPopServer(0), popServerPort, popSecured)
        Return True
    End Function

    Private Sub internalGatherEmails()
        Try
            Dim changedAccount As Boolean = False

            If Me.popServer.server = "" Then
                If currentDroitAcces(44) AndAlso askPopServer() Then
                    changedAccount = True
                Else
                    If Not currentDroitAcces(44) Then MessageBox.Show("Le serveur de courriel entrant pour le compte de courriel (" & Me.toString() & ") est vide. Veuillez demander à un utilisateur habilité à modifier ces informations de remplir cette information.", "Server de courriel entrant requis")
                    lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, False)
                    RaiseEvent pop3Cancelled(Me)
                    Exit Sub
                End If
            End If

            Dim user As String = Me.username
            If user.Trim = "" Then
                Dim myInputBoxPlus As New InputBoxPlus
                myInputBoxPlus.refusedChars = ":"
                user = myInputBoxPlus("Veuillez entrer le nom d'usager du compte de courriel (" & Me.toString() & ") :", "Nom d'usager requis")
                If user = "" Then
                    lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, False)
                    RaiseEvent pop3Cancelled(Me)
                    Exit Sub
                End If

                Me.username = user
                Me.password = "" 'Efface le mot de passe pour le redemander
                changedAccount = True
            End If
            Dim password As String = ""
            If Me.savePassword = False OrElse Me.password = "" Then
                Dim myInputBoxPlus As New InputBoxPlus
                myInputBoxPlus.refusedChars = ":"
                myInputBoxPlus.passwordChar = "*"
                password = myInputBoxPlus("Veuillez entrer le mot de passe du compte de courriel (" & Me.toString() & ") :", "Mot de passe requis")
                If password = "" Then
                    lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, False)
                    RaiseEvent pop3Cancelled(Me)
                    Exit Sub
                End If

                If Me.savePassword = True Then
                    Me.passwordKey = Chaines.getPasswordKey
                    Me.password = encrypt(password, Me.passwordKey)
                    changedAccount = True
                End If
            Else
                password = decrypt(Me.password, Me.passwordKey)
            End If

            Dim pop3Server As New Protocols.POP3(Me.popServer.server, user, password, Me.popServer.port)
            If Not pop3Server.isConnected Then
                Dim errorMsg As String = "Impossible de se connecter au serveur de courriel. Il est possible que les informations de connexion (serveur entrant et/ou port) soient erronées (" & Me.toString() & ")."
                Select Case MessageBox.Show(errorMsg & vbCrLf & vbCrLf & If(currentDroitAcces(44), changeQuestion, suggestion), "Informations erronées", If(currentDroitAcces(44), MessageBoxButtons.YesNo, MessageBoxButtons.OK), MessageBoxIcon.Error, If(currentDroitAcces(44), MessageBoxDefaultButton.Button2, MessageBoxDefaultButton.Button1))
                    Case DialogResult.Yes
                        If askPopServer() Then
                            changedAccount = True
                        Else
                            lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, False)
                            RaiseEvent pop3Cancelled(Me)
                            Exit Sub
                        End If

                    Case Else
                        lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, False)
                        RaiseEvent pop3Cancelled(Me)
                        Exit Sub
                End Select
            End If

            AddHandler pop3Server.message, AddressOf popMessage
            'If identification fails, advise user
            If pop3Server.identification() = False Then
                Dim errorMsg As String = "Les informations de connexion (nom d'usager et/ou mot de passe) sont erronées (" & Me.toString() & ")"
                Select Case MessageBox.Show(errorMsg & vbCrLf & vbCrLf & If(currentDroitAcces(44), changeQuestion, suggestion), "Informations erronées", If(currentDroitAcces(44), MessageBoxButtons.YesNo, MessageBoxButtons.OK), MessageBoxIcon.Error, If(currentDroitAcces(44), MessageBoxDefaultButton.Button2, MessageBoxDefaultButton.Button1))
                    Case DialogResult.Yes
                        RemoveHandler pop3Server.message, AddressOf popMessage
                        Me.username = ""
                        Me.password = ""
                        Me.saveData()
                        internalGatherEmails()

                    Case Else
                        lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, False)
                        RaiseEvent pop3Cancelled(Me)

                End Select
                Exit Sub
            End If

            If changedAccount Then Me.saveData()

            Dim curMessagesIdent() As String = DBLinker.getInstance.readOneDBField("Mails", "ServerIdent", "WHERE POPServer='" & Me.popServer.server.Replace("'", "''") & "' AND ServerIdent<>'' AND POPUser='" & Me.username.Replace("'", "''") & "'")

            nbEmailsDownloaded = 0 'Reset number of downloaded mails

            AddHandler pop3Server.downloading, AddressOf popDownloading
            AddHandler pop3Server.messageDownloaded, AddressOf popMessageDownloaded
            AddHandler pop3Server.downloadEnded, AddressOf popDownloadEnded
            pop3Server.gatherMails(curMessagesIdent, Not Me.keepMSGOnServer)
            RemoveHandler pop3Server.downloading, AddressOf popDownloading
            RemoveHandler pop3Server.message, AddressOf popMessage
            RemoveHandler pop3Server.downloadEnded, AddressOf popDownloadEnded

            If nbEmailsDownloaded <> 0 Then
                Dim savingFolder As MailFolder = MailsManager.getInstance.getMailFolder(MailFolder.getPath(noUserInbox, ""))
                InternalUpdatesManager.getInstance.sendUpdate("MessagesList(" & savingFolder.noMailFolder & ")")
            End If

            If noUserInbox <> 0 AndAlso nbEmailsDownloaded <> 0 Then
                Dim alertText As String = "Vous avez reçu " & If(nbEmailsDownloaded <= 10, Chaines.convertirMontantEnMots(nbEmailsDownloaded).ToLower(), nbEmailsDownloaded.ToString()) & " nouveau" & If(nbEmailsDownloaded = 1, String.Empty, "x") & " message" & If(nbEmailsDownloaded = 1, String.Empty, "s") & " externe" & If(nbEmailsDownloaded = 1, String.Empty, "s") & " qui " & If(nbEmailsDownloaded = 1, "a", "ont") & " été ajouté" & If(nbEmailsDownloaded = 1, String.Empty, "s") & " à votre boîte de réception personnelle"
                Dim msgAlert As New AlertOfMailSystem(alertText, "Utilisateurs\" & UsersManager.getInstance.getUser(noUserInbox).toString(), 0)
                msgAlert.expiryDate = Date.Today.AddDays(1)
                AlertsManager.getInstance.addAlert(noUserInbox, msgAlert)
            End If
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Private Sub popMessageDownloaded(ByVal message As POP3MessageDownloaded)
        Dim newEmail As New Email(message.id, Me.popServer.server, Me.username)
        newEmail.extractFromSource(message.contentBytes)
        newEmail.save(noUserInbox)

        nbEmailsDownloaded += 1
    End Sub

    Private Sub popMessage(ByVal message As String)
        RaiseEvent pop3Message(message)
    End Sub

    Private Sub popDownloading(ByVal status As POP3DownloadingStatus)
        RaiseEvent pop3Downloading(status)
    End Sub

    Private Sub popDownloadEnded(ByVal sender As Protocols.POP3)
        lockSecteur("MailAccount-GatherEmails-" & Me.noMailAccount, False)

        RaiseEvent pop3DownloadEnded(Me, sender)
    End Sub
End Class
