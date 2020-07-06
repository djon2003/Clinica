Imports CI.Clinica.Accounts.Clients.Folders.RVs

Public Class ClientCopied
    Inherits RendezVous
    'TODO: This class inherits from RV ?? And it is called ClientCopied.. verify usage

    Private _noClient As Integer
    Private _clientName As String = ""

    Public Overrides Property noClient() As Integer
        Get
            Return _noClient
        End Get
        Set(ByVal value As Integer)
            _noClient = value
        End Set
    End Property

    Public Overrides ReadOnly Property clientName() As String
        Get
            Return _clientName
        End Get
    End Property

    Public Overrides Property itemText() As String
        Get
            Return _clientName
        End Get
        Set(ByVal value As String)
            _clientName = value
        End Set
    End Property

    Public Sub New(ByVal noClient As Integer, ByVal clientName As String)
        MyBase.New()

        _noClient = noClient
        _clientName = clientName
    End Sub

    Public Sub New(ByVal loadingData As DBItemableData)
        MyBase.New()
        loadData(loadingData)
    End Sub

    Public Overrides Sub copy()
        myMainWin.copyBox.setClient(Me, 0)
    End Sub

    Public Overrides Sub cut()
        Throw New NotSupportedException("Impossible de couper un client")
    End Sub

    Public Overrides Sub delete()
        Throw New NotSupportedException("Impossible de supprimer")
    End Sub

    Public Overrides Sub saveData()
        Throw New NotSupportedException("Impossible d'enregistrer")
    End Sub

    Public Overrides Function toString() As String
        Return _clientName
    End Function
End Class
