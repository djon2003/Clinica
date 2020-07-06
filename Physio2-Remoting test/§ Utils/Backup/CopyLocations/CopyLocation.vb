Public MustInherit Class CopyLocation

    Private _LocationName As String = ""
    Private _LocationPath As String = ""
    Private _vpnLocation As String = ""
    Private _vpnUserName As String = ""
    Private _vpnPassword As String = ""
    Private _SourceDataPath As String = ""
    Private _KeepOnlyOneCopy As Boolean = False

#Region "Propriétés"
    Protected Property keepOnlyOneCopy() As String
        Get
            Return _KeepOnlyOneCopy
        End Get
        Set(ByVal value As String)
            _KeepOnlyOneCopy = value
        End Set
    End Property
    Protected Property locationName() As String
        Get
            Return _LocationName
        End Get
        Set(ByVal value As String)
            _LocationName = value
        End Set
    End Property
    Protected Property locationPath() As String
        Get
            Return _LocationPath
        End Get
        Set(ByVal value As String)
            _LocationPath = value
        End Set
    End Property
    Protected Property vpnLocation() As String
        Get
            Return _vpnLocation
        End Get
        Set(ByVal value As String)
            _vpnLocation = value
        End Set
    End Property
    Protected Property vpnUserName() As String
        Get
            Return _vpnUserName
        End Get
        Set(ByVal value As String)
            _vpnUserName = value
        End Set
    End Property
    Protected Property vpnPassword() As String
        Get
            Return _vpnPassword
        End Get
        Set(ByVal value As String)
            _vpnPassword = value
        End Set
    End Property
    Protected Property sourceDataPath() As String
        Get
            Return _SourceDataPath
        End Get
        Set(ByVal value As String)
            _SourceDataPath = value
        End Set
    End Property
#End Region

    Public Shared Function createNew(ByVal loadingData As DataRow, ByVal sourceDataPath As String) As CopyLocation
        Dim newCL As CopyLocation
        Select Case loadingData("CopyType")
            Case "DirectCopy"
                newCL = New DirectCopy
            Case "VPNCopy"
                newCL = New VPNCopy
            Case Else
                Throw New Exception("Location not valid")
        End Select
        newCL.load(loadingData, sourceDataPath)


        Return newCL
    End Function

    Public Sub load(ByVal loadingData As DataRow, ByVal sourceDataPath As String)
        _LocationName = loadingData("LocationName")
        _LocationPath = loadingData("LocationPath")
        _vpnLocation = loadingData("VPNLocation")
        _vpnUserName = loadingData("VPNUserName")
        _vpnPassword = loadingData("VPNPassword")
        _KeepOnlyOneCopy = loadingData("KeepOnlyOneCopy")
        _SourceDataPath = sourceDataPath
    End Sub

    Public MustOverride Sub copy()

End Class
