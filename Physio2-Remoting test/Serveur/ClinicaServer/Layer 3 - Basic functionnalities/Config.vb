Imports System.ComponentModel

<Serializable()> _
Public Class Config
    Inherits ConfigAdvancedBase

    Public Const DEFAULT_SERVER_PORT As Integer = 20500
    Public Const WAITING_REMOTECONFIG_INFINITLY As Integer = -1
    Public Const DEFAULT_WAITING_REMOTECONFIG_MINUTES As Integer = 20

    Private _serverPort As String = DEFAULT_SERVER_PORT
    Private _waitingMinutesForRemoteConfig As Integer = DEFAULT_WAITING_REMOTECONFIG_MINUTES
    Private _restartEveryDay As Boolean = True
    Private _exitPassword As String = String.Empty

#Region "Properties"
    <CategoryAttribute("Serveur"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Port(s) du serveur de Clinica. Pour utiliser plusieurs ports, veuillez les séparer par des virgules. Utiliser une valeur vide pour remettre au port par défaut. Nécessite un redémarrage du logiciel.")> _
    Public Property serverPort() As String
        Get
            Return _serverPort
        End Get
        Set(ByVal value As String)
            If value = "" Then value = DEFAULT_SERVER_PORT
            value = value.Replace(";", ",")
            _serverPort = value
        End Set
    End Property

    <CategoryAttribute("Serveur"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nombre de minutes d'attente maximales avant de demander la configuration sur le poste du serveur. Utiliser une valeur inférieure à 1 pour attendre indéfiniment.")> _
    Public Property waitingMinutesForRemoteConfig() As Integer
        Get
            Return _waitingMinutesForRemoteConfig
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then value = WAITING_REMOTECONFIG_INFINITLY
            _waitingMinutesForRemoteConfig = value
        End Set
    End Property

    <CategoryAttribute("Serveur"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nombre de minutes d'attente maximales avant de demander la configuration sur le poste du serveur. Utiliser une valeur inférieure à 1 pour attendre indéfiniment.")> _
    Public Property restartEveryDay() As Boolean
        Get
            Return _restartEveryDay
        End Get
        Set(ByVal value As Boolean)
            _restartEveryDay = value
        End Set
    End Property

    <CategoryAttribute("Serveur"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Mot de passe pour quitter le logiciel. Aucun mot de passe désactive cette fonctionnalité.")> _
    Public Property serverPassword() As String
        Get
            Return _exitPassword
        End Get
        Set(ByVal value As String)
            _exitPassword = value
        End Set
    End Property
#End Region
End Class
