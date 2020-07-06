Public Class POP3DownloadingStatus
    Private _EtatChargement As String
    Private _NumMail As Int32
    Private _NumEtape As Integer
    Private _PourcentMail As Double
    Private _NbrNouveauMessage As Integer

    Public Sub New(ByVal etatChargement As String, ByVal numMail As Int32, ByVal numEtape As Integer, ByVal pourcentMail As Double, ByVal nbrNouveauMessage As Integer)
        _EtatChargement = etatChargement
        _NumMail = numMail
        _NumEtape = numEtape
        If pourcentMail > 100 Then pourcentMail = 100
        _PourcentMail = pourcentMail
        _NbrNouveauMessage = nbrNouveauMessage
    End Sub

#Region "Properties"
    Public ReadOnly Property numMail() As Int32
        Get
            Return _NumMail
        End Get
    End Property
    Public ReadOnly Property nbrNouveauMessage() As Integer
        Get
            Return _NbrNouveauMessage
        End Get
    End Property
    Public ReadOnly Property numEtape() As Integer
        Get
            Return _NumEtape
        End Get
    End Property
    Public ReadOnly Property pourcentMail() As Double
        Get
            Return _PourcentMail
        End Get
    End Property
    Public ReadOnly Property etatChargement() As String
        Get
            Return _EtatChargement
        End Get
    End Property
#End Region
End Class
