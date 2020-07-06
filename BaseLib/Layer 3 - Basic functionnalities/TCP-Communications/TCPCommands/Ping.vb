Namespace TCPCommands


    Public Class Ping
        Inherits TCPCommand

        Public Const NAME As String = "PING"

        Public Sub New(ByVal client As TCPClient)
            MyBase.New(client)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim pong As New TCPAnswers.Pong(Me)
                pong.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & "ME"
        End Function
    End Class


End Namespace