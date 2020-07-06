Friend Class ExternalUpdateSoftwareInfos
    Private _version As Integer
    Private _softName As String

    Public ReadOnly Property version() As Integer
        Get
            Return _version
        End Get
    End Property

    Public ReadOnly Property softName() As String
        Get
            Return _softName
        End Get
    End Property

    Public Sub New(ByVal data As String)
        Dim curData() As String = data.Split(New Char() {"|"}, 2)
        _version = curData(0)
        _softName = curData(1)
    End Sub
End Class
