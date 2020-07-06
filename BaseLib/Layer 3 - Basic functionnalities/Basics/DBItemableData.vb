Public Class DBItemableData

    Private _mainData As DataRow
    Private _multipleData() As DataRow = {}
    Private _linkTables As Generic.Dictionary(Of String, DataTable)
    Private _extraData As Generic.Dictionary(Of String, Object)

    Public Sub New(ByVal mainData As DataRow, Optional ByVal linkTables As Generic.Dictionary(Of String, DataTable) = Nothing, Optional ByVal extraData As Generic.Dictionary(Of String, Object) = Nothing)
        If linkTables Is Nothing Then linkTables = New Generic.Dictionary(Of String, DataTable)
        If extraData Is Nothing Then extraData = New Generic.Dictionary(Of String, Object)

        _mainData = mainData
        _linkTables = linkTables
        _extraData = extraData
    End Sub

    Public Sub New(ByVal multipleData() As DataRow, Optional ByVal linkTables As Generic.Dictionary(Of String, DataTable) = Nothing, Optional ByVal extraData As Generic.Dictionary(Of String, Object) = Nothing)
        If linkTables Is Nothing Then linkTables = New Generic.Dictionary(Of String, DataTable)
        If extraData Is Nothing Then extraData = New Generic.Dictionary(Of String, Object)

        _multipleData = multipleData
        _linkTables = linkTables
        _extraData = extraData
    End Sub

#Region "Properties"
    Public ReadOnly Property multipleData() As DataRow()
        Get
            Return _multipleData
        End Get
    End Property

    Public ReadOnly Property mainData() As DataRow
        Get
            Return _mainData
        End Get
    End Property

    Public ReadOnly Property linkTables() As Generic.Dictionary(Of String, DataTable)
        Get
            Return _linkTables
        End Get
    End Property

    Public ReadOnly Property extraData() As Generic.Dictionary(Of String, Object)
        Get
            Return _extraData
        End Get
    End Property
#End Region

End Class
