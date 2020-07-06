Namespace TCPCommands


    Public Class CreateTask
        Inherits TCPCommand

        Public Const NAME As String = "CREATETASK"

        Private taskTypeIdentifier As String = ""
        Private newTask As TaskBase
        Private clientIdentifier As String = ""
        Private clientTaskIdentifier As Integer

        Public Sub New(ByVal client As TCPClient, ByVal taskTypeIdentifier As String, ByVal clientTaskIdentifier As Integer)
            MyBase.New(client)
            Me.taskTypeIdentifier = taskTypeIdentifier
            Me.clientTaskIdentifier = clientTaskIdentifier
            Me.clientIdentifier = client.name
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            taskTypeIdentifier = data.args(0)
            clientTaskIdentifier = data.args(1)
            clientIdentifier = data.args(2)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim curPlugin As PluginTaskBase = PluginTasksManager.getInstance.getItemable(taskTypeIdentifier)
                Dim task As TaskBase = curPlugin.createTask()
                'Send reply to creator so he gets the NoItemable of the task
                Dim taskInfo As New TCPAnswers.TaskInfo(Me, task, clientIdentifier, clientTaskIdentifier)
                taskInfo.execute()

                task.startTask()

                External.current.refreshTasks()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & taskTypeIdentifier & DataTCP.PARAMS_SEPARATOR & clientTaskIdentifier & DataTCP.PARAMS_SEPARATOR & clientIdentifier
        End Function
    End Class


End Namespace