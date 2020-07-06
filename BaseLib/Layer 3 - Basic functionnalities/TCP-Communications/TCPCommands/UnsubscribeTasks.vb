Namespace TCPCommands


    Friend Class UnsubscribeTasks
        Inherits TCPCommand

        Public Const NAME As String = "RTASKS"

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
                PluginTasksManager.getInstance.removeSubscriber(getClient())
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME
        End Function
    End Class


End Namespace