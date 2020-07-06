Public Class Lock

    Private _lockedTime As Date = Date.Now
    Private _sectorId As String = String.Empty
    Private _sectorName As String = String.Empty
    Private _lockedBy As String = String.Empty

    Public Sub New(ByVal lockString As String)
        Dim data() As String = lockString.Split(DataTCP.PARAMS_SEPARATOR)
        _sectorId = data(0)
        _sectorName = data(1).Replace("§§", DataTCP.PARAMS_SEPARATOR)
        _lockedBy = data(2).Replace("§§", DataTCP.PARAMS_SEPARATOR)
    End Sub

    Public Sub New(ByVal sectorId As String, ByVal sectorName As String, ByVal lockedBy As String)
        Me._sectorId = sectorId
        Me._lockedBy = lockedBy
        Me._sectorName = sectorName
    End Sub

    Public Sub New(ByVal sectorId As String, ByVal sectorName As String, ByVal lockedBy As String, ByVal lockedTime As Date)
        Me._sectorId = sectorId
        Me._sectorName = sectorName
        Me._lockedBy = lockedBy
        Me._lockedTime = lockedTime
    End Sub

    Public ReadOnly Property sectorId() As String
        Get
            Return _sectorId
        End Get
    End Property

    Public ReadOnly Property sectorName() As String
        Get
            Return _sectorName
        End Get
    End Property

    Public ReadOnly Property lockedBy() As String
        Get
            Return _lockedBy
        End Get
    End Property

    Public ReadOnly Property lockedTime() As Date
        Get
            Return _lockedTime
        End Get
    End Property


    Public Overrides Function ToString() As String
        Return _sectorId & DataTCP.PARAMS_SEPARATOR & _sectorName.Replace(DataTCP.PARAMS_SEPARATOR, "§§") & DataTCP.PARAMS_SEPARATOR & _lockedBy.Replace(DataTCP.PARAMS_SEPARATOR, "§§")
    End Function

End Class
