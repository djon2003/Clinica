Namespace TCPAnswers


    Public Class TasksInfos
        Inherits TCPAnswer

        Private Const TASK_SPLITTER As String = "§§§"
        Private Const INFO_SPLITTER As String = "§§"

        Public Const NAME As String = "TASKS"
        Private tasks As List(Of TaskBase)

        Public Sub New(ByVal client As TCPClient, ByVal tasks As List(Of TaskBase))
            MyBase.New(client)
            Me.tasks = tasks
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.GetTasksInfos, ByVal tasks As List(Of TaskBase))
            MyBase.New(tcpMessage)
            Me.tasks = tasks
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Private Function createSendString() As String
            Dim tasksString As New List(Of String)

            For Each task As TaskBase In tasks
                tasksString.Add((New TaskInfos(task, True)).ToString())
            Next

            Dim sendString As String = ""
            If tasksString.Count <> 0 Then
                sendString = String.Join(TASK_SPLITTER, tasksString.ToArray())
            End If

            Return sendString
        End Function

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else
                If data.getArgsString() = "" Then Exit Sub

                For Each curData As String In data.getArgsString().Split(New String() {TASK_SPLITTER}, StringSplitOptions.None)
                    Dim taskInfos As New TaskInfos(curData)
                    Dim taskExists As TaskBase = PluginTasksManager.getInstance().getTask(taskInfos.taskTypeIdentifier, taskInfos.noItemable)

                    If taskExists Is Nothing Then
                        Dim task As TaskBase = New TaskClient(PluginTasksManager.getInstance().getItemable(taskInfos.taskTypeIdentifier), taskInfos)
                        PluginTasksManager.getInstance.addTask(taskInfos.taskTypeIdentifier, task)
                    End If
                Next
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & createSendString()
        End Function
    End Class


End Namespace