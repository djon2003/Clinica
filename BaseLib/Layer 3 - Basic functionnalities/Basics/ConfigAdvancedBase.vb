Imports System.ComponentModel

<Serializable()> _
Public MustInherit Class ConfigAdvancedBase
    Inherits ConfigBase

    Public Const NO_SQL_PORT As Integer = -1
    Public Const UPDATE_KEY_LENGTH As Integer = 100

    Public Enum updateChannels As Integer
        STABLE = 0
        BETA = 1
        DEV = 2
    End Enum

    Private _sqlDBName As String = "", _sqlServerAddress As String = ""
    Private _sqlServerPort As Integer = NO_SQL_PORT
    Private _sqlUsername As String = ""
    Private _sqlPassword As String = ""

    Private _updateKey As String = "", _updateUrl As String = "", _updateUsername As String = "", _updatePassword As String = ""
    Private _updateUserType As ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateUserTypes = ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateUserTypes.User
    Private _updateChannel As updateChannels = updateChannels.STABLE
    Private _updateLocal As Boolean = False, _updateLocalPath As String = ""

#Region "Properties"

#Region "Updates"
    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom d'utilisateur pour accéder aux mises à jour du logiciel. Nécessite un redémarrage du logiciel.")> _
    Public Property updateUserType() As ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateUserTypes
        Get
            Return _updateUserType
        End Get
        Set(ByVal value As ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateUserTypes)
            _updateUserType = value
        End Set
    End Property

    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Canal de mise à jour du logiciel. Nécessite un redémarrage du logiciel.")> _
    Public Property updateChannel() As updateChannels
        Get
            Return _updateChannel
        End Get
        Set(ByVal value As updateChannels)
            _updateChannel = value
        End Set
    End Property

    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Adresse URL de mise à jour fournie par CyberInternautes. Nécessite un redémarrage du logiciel.")> _
    Public Property updateUrl() As String
        Get
            Return _updateUrl
        End Get
        Set(ByVal value As String)
            _updateUrl = value
        End Set
    End Property

    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom d'utilisateur pour accéder aux mises à jour du logiciel. Nécessite un redémarrage du logiciel.")> _
    Public Property updateUsername() As String
        Get
            Return _updateUsername
        End Get
        Set(ByVal value As String)
            _updateUsername = value
        End Set
    End Property

    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Mot de passe pour accéder aux mises à jour du logiciel. Nécessite un redémarrage du logiciel.")> _
    Public Property updatePassword() As String
        Get
            Return _updatePassword
        End Get
        Set(ByVal value As String)
            _updatePassword = value
        End Set
    End Property

    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Clé de mise à jour du logiciel fournie par CyberInternautes. Nécessite un redémarrage du logiciel.")> _
    Public Property updateKey() As String
        Get
            Return _updateKey
        End Get
        Set(ByVal value As String)
            _updateKey = value
        End Set
    End Property

    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Activer ou non la mise à jour via un chemin réseau. Nécessite un redémarrage du logiciel.")> _
    Public Property updateLocal() As Boolean
        Get
            Return _updateLocal
        End Get
        Set(ByVal value As Boolean)
            _updateLocal = value
        End Set
    End Property

    <CategoryAttribute("Mise à jour"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Chemin réseau pour une mise à jour locale. Nécessite un redémarrage du logiciel.")> _
    Public Property updateLocalPath() As String
        Get
            Return _updateLocalPath
        End Get
        Set(ByVal value As String)
            _updateLocalPath = value
        End Set
    End Property
#End Region

    <CategoryAttribute("Connexion SQL"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom de la base de données sur le serveur SQL. Nécessite un redémarrage du logiciel.")> _
    Public Property sqlDBName() As String
        Get
            Return _sqlDBName
        End Get
        Set(ByVal value As String)
            _sqlDBName = value
        End Set
    End Property

    <CategoryAttribute("Connexion SQL"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Adresse IP ou nom de l'hôte du serveur SQL. Nécessite un redémarrage du logiciel.")> _
    Public Property sqlServerAddress() As String
        Get
            Return _sqlServerAddress
        End Get
        Set(ByVal value As String)
            _sqlServerAddress = value
        End Set
    End Property

    <CategoryAttribute("Connexion SQL"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Port du serveur SQL. Utiliser une valeur inférieure à 1 pour désactiver l'utilisation du port. Nécessite un redémarrage du logiciel.")> _
    Public Property sqlServerPort() As Integer
        Get
            Return _sqlServerPort
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then value = NO_SQL_PORT
            _sqlServerPort = value
        End Set
    End Property

    <CategoryAttribute("Connexion SQL"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom d'utilisateur du serveur SQL. Nécessite un redémarrage du logiciel.")> _
    Public Property sqlUsername() As String
        Get
            Return _sqlUsername
        End Get
        Set(ByVal value As String)
            _sqlUsername = value
        End Set
    End Property

    <CategoryAttribute("Connexion SQL"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Mot de passe du serveur SQL. Nécessite un redémarrage du logiciel.")> _
    Public Property sqlPassword() As String
        Get
            Return _sqlPassword
        End Get
        Set(ByVal value As String)
            _sqlPassword = value
        End Set
    End Property
#End Region


    Protected Overrides Function isFieldsCorrectlyFilled() As Boolean
        Dim noEmptyFields() As String = {"sqlDBName", "sqlServerAddress", "updateUrl", "updateUsername", "updatePassword", "updateKey"}
        If Not isNoEmptyFields(noEmptyFields) Then Return False

        If updateKey.Length <> UPDATE_KEY_LENGTH Then
            MessageBox.Show("Veuillez vous assurez que la longueur du champ ""updateKey"" de l'onglet """ & Me.GetType.Assembly.GetName.Name & """ soit de " & UPDATE_KEY_LENGTH & " caractères", "Champ incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If updateLocal AndAlso (updateLocalPath = "" OrElse Not IO.Directory.Exists(updateLocalPath)) Then
            MessageBox.Show("Veuillez vous assurez de l'existance du chemin réseau du champ ""updateLocalPath"" de l'onglet """ & Me.GetType.Assembly.GetName.Name & """ lorsque la mise à jour locale est activée.", "Chemin inexistant", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return True
    End Function

End Class
