Namespace TCPCommands


    Public Class GetTasksInfos
        Inherits TCPCommand

        Public Const NAME As String = "GETTASKS"

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
                Dim tasksInfos As New TCPAnswers.TasksInfos(Me, PluginTasksManager.getInstance.getTasks())
                tasksInfos.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME
        End Function
    End Class


End Namespace