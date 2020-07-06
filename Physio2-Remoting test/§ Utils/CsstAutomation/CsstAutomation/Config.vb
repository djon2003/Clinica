Imports CI.Base
Imports System.ComponentModel

<Serializable()> _
Public Class Config
    Inherits ConfigBase

    Private _outputFolder As String = ""
    Private _csstWebSiteUsername As String = ""
    Private _csstWebSitePassword As String = ""
    Private _folderCodeNames As String = ""
    Private _noCSST As Integer = 0

    Private _doOnly_UploadTestFile As Boolean = False

    Public Shared ReadOnly Property current() As Config
        Get
            Return CType(ConfigurationsManager.getInstance().getItemable(GetType(Config)), Object)
        End Get
    End Property


    'TODO : Remove this config when testing of upload error is done !
    <CategoryAttribute("Test"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom des codifications dossiers représentant la CSST. Pour plusieurs noms, séparer les par des virgules.")> _
    Public Property doOnly_UploadTestFile() As Boolean
        Get
            Return _doOnly_UploadTestFile
        End Get
        Set(ByVal value As Boolean)
            _doOnly_UploadTestFile = value
        End Set
    End Property

    <CategoryAttribute("Création des fichiers"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Emplacement pour la création des fichiers")> _
    Public Property outputFolder() As String
        Get
            Return _outputFolder
        End Get
        Set(ByVal value As String)
            _outputFolder = value
        End Set
    End Property

    <CategoryAttribute("Création des fichiers"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom des codifications dossiers représentant la CSST. Pour plusieurs noms, séparer les par des virgules.")> _
    Public Property folderCodeNames() As String
        Get
            Return _folderCodeNames
        End Get
        Set(ByVal value As String)
            _folderCodeNames = value
        End Set
    End Property

    <CategoryAttribute("Création des fichiers"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Numéro d'enregistrement au service d'échange électronique de la CSST")> _
    Public Property noCSST() As Integer
        Get
            Return _noCSST
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then value = 0
            _noCSST = value
        End Set
    End Property

    <CategoryAttribute("Serveur web CSST"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom d'utilisateur pour se connecter au site de la CSST")> _
    Public Property csstWebSiteUsername() As String
        Get
            Return _csstWebSiteUsername
        End Get
        Set(ByVal value As String)
            _csstWebSiteUsername = value
        End Set
    End Property

    <CategoryAttribute("Serveur web CSST"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Nom d'utilisateur pour se connecter au site de la CSST")> _
    Public Property csstWebSitePassword() As String
        Get
            Return _csstWebSitePassword
        End Get
        Set(ByVal value As String)
            _csstWebSitePassword = value
        End Set
    End Property

    Protected Overrides Function isFieldsCorrectlyFilled() As Boolean
        Dim noEmptyFields() As String = {"outputFolder", "folderCodeNames", "csstWebSiteUsername", "csstWebSitePassword"}
        Dim hasNoEmptyFields As Boolean = isNoEmptyFields(noEmptyFields)
        If hasNoEmptyFields = False Then Return False

        'Ask for creation of output folder if it doesn't exist.
        If IO.Directory.Exists(outputFolder) = False AndAlso MessageBox.Show("Le dossier inscrit dans le champ ""outputFolder"" de l'onglet """ & Me.GetType.Assembly.GetName.Name & """ n'existe pas. Voulez-vous le créer ?", "Dossier inexistant", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return False
        End If
        IO.Directory.CreateDirectory(outputFolder) 'Ensure directory exists

        'Test csst user/pass
        If isConnectionAvailable() Then
            If FilesWebInteractor.testConnection() = False Then
                MessageBox.Show("Les informations de connexion au site web de la CSST ne permettent pas de s'y connecter. Veuillez vous assurer d'entrer des informations valides.", "Informations de connexion CSST invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Else
            Return MessageBox.Show("Les informations de connexion au site web de la CSST ne peuvent être confirmés, car la connexion à Internet ne fonctionne pas." & vbCrLf & "Désirez-vous poursuivre quand même ?", "Connexion Internet non fonctionnel", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes
        End If

        Return True
    End Function
End Class
