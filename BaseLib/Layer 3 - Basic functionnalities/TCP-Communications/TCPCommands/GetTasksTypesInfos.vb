Namespace TCPCommands


    Public Class GetTasksTypesInfos
        Inherits TCPCommand

        Public Const NAME As String = "GETTASKSTYPES"

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
                'Ensure plugins are loaded before sending the list
                While Not Software.getInstance().arePluginsLoaded
                    Threading.Thread.Sleep(100)
                End While

                Dim tasksTypesInfo As New TCPAnswers.TasksTypesInfo(Me, PluginTasksManager.getInstance.getTasksTypesInfos())
                tasksTypesInfo.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME
        End Function
    End Class


End Namespace