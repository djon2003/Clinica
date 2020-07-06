Namespace TCPCommands


    Public Class Thank
        Inherits TCPCommand

        Public Const NAME As String = "THANK"
        Private pcWinName As String = ""

        Public Sub New(ByVal client As TCPClient, ByVal pcWinName As String)
            MyBase.New(client)
            Me.pcWinName = pcWinName
        End Sub

        Public Sub New(ByVal tcpMessage As TCPAnswers.Join, ByVal pcWinName As String)
            MyBase.New(tcpMessage)

            Me.pcWinName = pcWinName
        End Sub

        Public Sub New(ByVal tcpMessage As TCPAnswers.Rejoin, ByVal pcWinName As String)
            MyBase.New(tcpMessage)

            Me.pcWinName = pcWinName
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
            Return NAME & DataTCP.PARAMS_SEPARATOR & pcWinName
        End Function
    End Class


End Namespace
