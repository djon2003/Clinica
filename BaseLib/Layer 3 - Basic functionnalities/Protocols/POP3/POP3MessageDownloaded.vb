Public Class POP3MessageDownloaded
    Private _id As String
    Private _contentBytes() As Byte
    Private _size As Integer

    Public Sub New(ByVal id As String, ByVal contentBytes() As Byte, ByVal size As Integer)
        _id = id
        _contentBytes = contentBytes
        _size = size
    End Sub

#Region "Properties"
    Public ReadOnly Property size() As String
        Get
            Return _size
        End Get
    End Property

    Public ReadOnly Property id() As String
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property contentBytes() As Byte()
        Get
            Return _contentBytes
        End Get
    End Property
#End Region
End Class
