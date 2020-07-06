Namespace TCPAnswers


    Public Class Join
        Inherits TCPAnswer

        Public Const NAME As String = "JOIN"

        Private pcWinName As String = String.Empty
        Private _isJoined As Boolean

#Region "Constructors"
        Public Sub New(ByVal tcpMessage As TCPCommands.Connect, ByVal isJoined As Boolean)
            MyBase.New(tcpMessage)
            Me.pcWinName = tcpMessage.identifier
            Me._isJoined = isJoined
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            Me.pcWinName = data.args(0)
            Me._isJoined = If(data.args(1) = 1, True, False)
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property isJoined() As Boolean
            Get
                Return _isJoined
            End Get
        End Property
#End Region

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else
                Dim thankCommand As New TCPCommands.Thank(Me, pcWinName)
                thankCommand.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & pcWinName & DataTCP.PARAMS_SEPARATOR & If(_isJoined, 1, 0)
        End Function
    End Class


End Namespace
