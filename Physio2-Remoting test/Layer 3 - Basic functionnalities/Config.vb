Imports System.ComponentModel

Public Class Config
    Inherits Base.ConfigAdvancedBase

    Public Const DEFAULT_MAX_RECONNECTION_TRIALS As Integer = 100
    Public Const DEFAULT_SERVER_PORT As Integer = 20500

    Private _tcpClientUsePing As Boolean = True
    Private _maxReconnectionTrials As Integer = DEFAULT_MAX_RECONNECTION_TRIALS
    Private _serverPort As Integer = DEFAULT_SERVER_PORT
    Private _serverAddress As String = String.Empty
    Private _printer As String = String.Empty
    Private _dataPath As String = String.Empty
    Private _lastUserConnected As Integer

#Region "Properties"
    <CategoryAttribute("Connexion au serveur de Clinica"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Utilisation d'une commande Ping pour s'assurer que la connexion est toujours active. Nécessite un redémarrage du logiciel.")> _
    Public Property tcpClientUsePing() As Boolean
        Get
            Return _tcpClientUsePing
        End Get
        Set(ByVal value As Boolean)
            _tcpClientUsePing = value
        End Set
    End Property

    <CategoryAttribute("Connexion au serveur de Clinica"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Lors d'une perte de connexion du serveur de Clinica, nombre maximal de tentatives de reconnexion avant la fermeture automatique du logiciel.")> _
    Public Property maxReconnectionTrials() As Integer
        Get
            Return _maxReconnectionTrials
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then value = 0
            _maxReconnectionTrials = value
        End Set
    End Property

    <CategoryAttribute("Connexion au serveur de Clinica"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Adresse IP ou nom de l'hôte du serveur de Clinica. Nécessite un redémarrage du logiciel.")> _
    Public Property serverAddress() As String
        Get
            Return _serverAddress
        End Get
        Set(ByVal value As String)
            _serverAddress = value
        End Set
    End Property

    <CategoryAttribute("Connexion au serveur de Clinica"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Port du serveur de Clinica. Utiliser une valeur inférieure à 1 pour remettre au port par défaut. Nécessite un redémarrage du logiciel.")> _
    Public Property serverPort() As Integer
        Get
            Return _serverPort
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then value = DEFAULT_SERVER_PORT
            _serverPort = value
        End Set
    End Property

    <CategoryAttribute("Clinica"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Chemin des données de Clinica. Nécessite un redémarrage du logiciel.")> _
    Public Property dataPath() As String
        Get
            Return _dataPath
        End Get
        Set(ByVal value As String)
            _dataPath = value
        End Set
    End Property

    <CategoryAttribute("Clinica"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Dernier utilisateur à s'être connecté au logiciel. Donnée modifiée par le logiciel.")> _
    Public Property lastUserConnected() As Integer
        Get
            Return _lastUserConnected
        End Get
        Set(ByVal value As Integer)
            _lastUserConnected = value
        End Set
    End Property

    <TypeConverter(GetType(ConfigPrinters)), _
    CategoryAttribute("Clinica"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Imprimante par à utiliser par défaut.")> _
    Public Property defaultPrinter() As String
        Get
            Return _printer
        End Get
        Set(ByVal value As String)
            _printer = value
        End Set
    End Property
#End Region

    Protected Overrides Function isFieldsCorrectlyFilled() As Boolean
        Dim noEmptyFields() As String = {"serverAddress", "dataPath"}
        Dim isFieldsOk As Boolean = isNoEmptyFields(noEmptyFields)
        If Not isFieldsOk Then Return False

        'Ensure path exists
        If IO.Directory.Exists(_dataPath) = False Then
            MessageBox.Show("Le chemin pour les données est invalide (Onglet ""Clinica""-->Configuration ""dataPath""). Veuillez en saisir un nouveau.", "Chemin invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return MyBase.isFieldsCorrectlyFilled()
    End Function
End Class
