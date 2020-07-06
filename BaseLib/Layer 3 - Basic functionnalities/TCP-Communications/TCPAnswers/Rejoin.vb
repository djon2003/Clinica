Namespace TCPAnswers


    Public Class Rejoin
        Inherits TCPAnswer

        Public Const NAME As String = "REJOIN"
        Private pcWinName As String = String.Empty
        Private isJoined As Boolean

        Public Sub New(ByVal tcpMessage As TCPCommands.Reconnect, ByVal isJoined As Boolean)
            MyBase.New(tcpMessage)
            Me.pcWinName = tcpMessage.identifier
            Me.isJoined = isJoined
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            pcWinName = data.args(0)
            isJoined = data.args(1)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else
                Dim thankCommand As New TCPCommands.Thank(Me, pcWinName)
                thankCommand.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & pcWinName & DataTCP.PARAMS_SEPARATOR & If(isJoined, 1, 0)
        End Function
    End Class


End Namespace