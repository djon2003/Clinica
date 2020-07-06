Namespace TCPCommands


    Public Class Disconnect
        Inherits TCPCommand

        Public Const NAME As String = "DISCONNECTME"
        Private identifier As String = ""

        Public Sub New(ByVal client As TCPClient, ByVal identifier As String)
            MyBase.New(client)
            Me.identifier = identifier
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                TCPHost.getInstance.removeClient(Me.client)
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & identifier
        End Function
    End Class


End Namespace
