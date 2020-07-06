Public Class ExternalUpdateException
    Inherits Exception

    Public Enum ErrorTypes As Integer
        CLIENT_CURRENTLY_COPIED_BY_ANOTHER = 0
        NO_INTERNET_CONNECTION = 1
        UPDATE_SERVER_OFFLINE = 2
        MISSING_CURRENT_VERSION_FILE = 3
        UPDATES_FILE_CONTAINS_WRONG_XML_ENTRY = 4
        SQL_FILE_CONTAINS_ERROR = 5
    End Enum

    Private Shared errorsText() As String = { _
        "Impossible de mettre à jour temporairement, car d'autres utilisateurs copient vos propres fichiers", _
        "Impossible de mettre à jour, car vous devez être connecté à Internet", _
        "Le serveur de mise à jour est temporairement hors service. Veuillez réessayer plus tard.", _
        "Un problème est survenu lors du téléchargement de la mise à jour. Veuillez vérifier votre connexion à l'internet et redémarrer Clinica. (Au besoin, contacter votre administrateur).", _
        "Les informations de connexion au serveur de mise à jour ne sont pas acceptés. Veuillez vérifier les configurations.", _
        "Impossible de mettre à jour, la mise à jour du serveur SQL a causé une erreur. Veuillez contacter CyberInternautes." _
        }

    Private _errorType As ErrorTypes

    Public ReadOnly Property errorType() As ErrorTypes
        Get
            Return _errorType
        End Get
    End Property

    Public ReadOnly Property errorText() As String
        Get
            Return errorsText(_errorType)
        End Get
    End Property


    Public Sub New(ByVal errorType As ErrorTypes)
        MyBase.New(errorsText(errorType))
        Me._errorType = errorType
    End Sub


    Public Sub New(ByVal errorType As ErrorTypes, ByVal innerException As Exception)
        MyBase.New(errorsText(errorType), innerException)
        Me._errorType = errorType
    End Sub

End Class
