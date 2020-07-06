Namespace Accounts.Clients.Folders.Codifications


    Public Class ExternalStatus

        Private _noExternalStatus As Integer
        Private _externalKey As String = String.Empty
        Private _externalStatus As String = String.Empty

        Public Sub New(ByVal data As DataRow)
            load(data)
        End Sub

#Region "Properties"
        Public ReadOnly Property noExternalStatus() As Integer
            Get
                Return _noExternalStatus
            End Get
        End Property

        Public ReadOnly Property externalKey() As String
            Get
                Return _externalKey
            End Get
        End Property

        Public ReadOnly Property externalStatus() As String
            Get
                Return _externalStatus
            End Get
        End Property
#End Region

        Private Sub load(ByVal data As DataRow)
            _noExternalStatus = data("noExternalStatus")
            _externalKey = data("externalKey")
            _externalStatus = data("externalStatus")
        End Sub

        Public Overrides Function ToString() As String
            Return _externalStatus
        End Function

    End Class


End Namespace