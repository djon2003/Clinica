Namespace TCPAnswers


    Public Class CanUpdate
        Inherits TCPAnswer

        Public Const NAME As String = "CANUPDATE"
        Private _canUpdate As Boolean

#Region "Constructors"
        Public Sub New(ByVal client As TCPClient, ByVal canUpdate As Boolean)
            MyBase.New(client)
            _canUpdate = canUpdate
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.UpdateLock, ByVal canUpdate As Boolean)
            MyBase.New(tcpMessage)
            _canUpdate = canUpdate
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            _canUpdate = If(data.args(0) = 1, True, False)
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property canUpdate() As Boolean
            Get
                Return _canUpdate
            End Get
        End Property
#End Region

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else
                'Nothing general to do
            End If
        End Sub

        Public Overrides Function ToString() As String
            'TODO : Set back to comment line after ensuring all clients where updated
            Return NAME & DataTCP.PARAMS_SEPARATOR & "1"
            'Return NAME & DataTCP.PARAMS_SEPARATOR & If(_canUpdate, 1, 0)
        End Function
    End Class


End Namespace