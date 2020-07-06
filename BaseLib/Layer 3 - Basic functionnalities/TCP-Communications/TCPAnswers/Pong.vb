Namespace TCPAnswers


    Public Class Pong
        Inherits TCPAnswer

        Public Const NAME As String = "PONG"

        Public Sub New(ByVal client As TCPClient)
            MyBase.New(client)
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.Ping)
            MyBase.New(tcpMessage)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else

            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & "ME"
        End Function
    End Class


End Namespace
