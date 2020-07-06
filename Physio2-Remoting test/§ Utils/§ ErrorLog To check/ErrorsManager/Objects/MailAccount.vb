Public Class MailAccount

    Private Const default_incoming_server_port As Integer = 110


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

    Private _smtpNeedAuthentication As Boolean
    Private _smtpSpecificCredential As Boolean
    Private _smtpAuthenUsername As String = ""
    Private _smtpPassword As String = ""
    Private _smtpPasswordKey As String = ""
    Private _smtpSavePassword As Boolean
    Private _IncomingProtocol As ManagedProtocols = ManagedProtocols.POP3
    Private _TimeoutInSeconds As Integer = 60

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


    Public Overloads Sub loadData(ByVal noDBItem As Integer)
    End Sub

    Public Overloads Sub loadData(ByVal name As String)
    End Sub


    Public Overrides Function toString() As String
        Return _AccountName & " (" & _SendingName & " <" & _Email & ">)"
    End Function


End Class
