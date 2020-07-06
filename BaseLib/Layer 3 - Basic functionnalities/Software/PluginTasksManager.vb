Public Class PluginTasksManager
    Inherits PluginsManagerBase(Of PluginTaskBase)
    Implements IDataConsumer(Of DataTCP), ITcpWaiter

    Private _isRemote As Boolean = False
    Private _isSubscribed As Boolean = False
    Private _nbCallsToSubscribe, _nbCallsToListen As Integer
    Private _subscribers As New List(Of TCPClient)
    Private Shared subscribeCommand As New TCPCommands.SubscribeTasks(TCPClient.getInstance())
    Private Shared unsubscribeCommand As New TCPCommands.UnsubscribeTasks(TCPClient.getInstance())

    Protected Sub New()

    End Sub

#Region "Properties"
    Public ReadOnly Property isSubscribed() As Boolean
        Get
            Return _isSubscribed
        End Get
    End Property

    Public ReadOnly Property subscribers() As List(Of TCPClient)
        Get
            Return _subscribers
        End Get
    End Property

    Public Property isRemote() As Boolean
        Get
            Return _isRemote
        End Get
        Set(ByVal value As Boolean)
            _isRemote = value
        End Set
    End Property
#End Region

#Region "Client methods"
    Public Sub populateTasks()
        Dim tasksInfos As New TCPCommands.GetTasksInfos(TCPClient.getInstance())
        waitCommand(tasksInfos, GetType(TCPAnswers.TasksInfos))
    End Sub

    Public Sub createPluginClients()
        isRemote = True

        Dim taskTypesInfos As New TCPCommands.GetTasksTypesInfos(TCPClient.getInstance())
        waitCommand(taskTypesInfos, GetType(TCPAnswers.TasksTypesInfo))
    End Sub

    Private Sub waitCommand(ByVal task As TCPCommands.TCPCommand, ByVal tcpAnswerToReceive As Type)
        listenToTCPClient()

        Dim answer As TCPCommunicationBase = Nothing
        Dim innerException As Exception = Nothing
        Try
            answer = TCPHelper.getInstance().sendWaitCommunication(Me, task, tcpAnswerToReceive)
        Catch ex As Exception
            innerException = ex
        End Try

        stopListenningTCPClient()

        'If answer not received, quit software and inform user what he could try
        If answer Is Nothing Then
            Throw New SendingServerCmdException(task.ToString, tcpAnswerToReceive.Name, innerException)
        End If
    End Sub

    Private Sub listenToTCPClient()
        _nbCallsToListen += 1

        If _nbCallsToListen = 1 Then TCPClient.getInstance().addConsumer(Me)
    End Sub

    Private Sub stopListenningTCPClient()
        _nbCallsToListen -= 1

        If _nbCallsToListen = 0 Then TCPClient.getInstance().removeConsumer(Me)
    End Sub

    Public Sub subscribe()
        _nbCallsToSubscribe += 1

        If _isSubscribed Then Exit Sub

        listenToTCPClient()
        _isSubscribed = True
        subscribeCommand.execute()
    End Sub

    Public Sub unsubscribe()
        _nbCallsToSubscribe -= 1

        If _nbCallsToSubscribe <> 0 Then Exit Sub

        stopListenningTCPClient()
        _isSubscribed = False
        unsubscribeCommand.execute()
    End Sub
#End Region

#Region "Host methods"
    Public Sub addSubscriber(ByVal tcpClient As TCPClient)
        If _subscribers.Contains(tcpClient) Then Exit Sub

        _subscribers.Add(tcpClient)
    End Sub

    Public Sub removeSubscriber(ByVal tcpClient As TCPClient)
        If _subscribers.Contains(tcpClient) = False Then Exit Sub

        _subscribers.Remove(tcpClient)
    End Sub
#End Region

    Private Sub taskDeleted(ByVal sender As Object, ByVal task As TaskBase)
        'Propagate deletion to subscribers
        Dim subscribers() As CI.Base.TCPClient = Me.subscribers.ToArray()
        For Each curSubscriber As TCPClient In subscribers
            Dim delTask As New TCPCommands.DelTask(curSubscriber, CType(sender, PluginTaskBase).getIdentifier(), task.noItemable)
            delTask.execute()
        Next
    End Sub

    Public Overrides Function addItemable(ByVal newItem As PluginTaskBase) As String
        If getItemable(newItem.getIdentifier()) IsNot Nothing Then Return String.Empty

        newItem.isRemote = isRemote
        AddHandler newItem.taskDeleted, AddressOf taskDeleted
        Return MyBase.addItemable(newItem)
    End Function

    Public Overloads Function getItemable(ByVal identifier As String) As PluginTaskBase
        Dim item As PluginTaskBase = Nothing
        For Each curItem As PluginTaskBase In getItemables()
            If curItem.getIdentifier() = identifier Then
                item = curItem
                Exit For
            End If
        Next

        Return item
    End Function

    Public Sub clearTasks()
        For Each curPlugin As PluginTaskBase In getItemables()
            curPlugin.clearTasks()
        Next
    End Sub

    Friend Sub addTask(ByVal taskTypeIdentifier As String, ByVal task As TaskBase)
        Dim curPlugin As PluginTaskBase = getItemable(taskTypeIdentifier)
        curPlugin.addTask(task)
    End Sub

    Public Sub stopTask(ByVal taskTypeIdentifier As String, ByVal noTask As Integer)
        For Each curClient As TCPClient In _subscribers
            Dim taskInfo As New TCPCommands.StopTask(curClient, taskTypeIdentifier, noTask)
            taskInfo.execute()
        Next
    End Sub

    Friend Function getTask(ByVal taskTypeIdentifier As String, ByVal noItemable As Integer)
        For Each curTask As TaskBase In getTasks()
            If curTask.creator.getIdentifier() = taskTypeIdentifier AndAlso curTask.noItemable = noItemable Then Return curTask
        Next

        Return Nothing
    End Function

    Public Function getTasksRunning() As List(Of TaskBase)
        Dim tasks As New List(Of TaskBase)
        For Each curPlugin As PluginTaskBase In Me.getItemables()
            tasks.AddRange(curPlugin.getTasksRunning())
        Next

        Return tasks
    End Function

    Public Function getTasks() As List(Of TaskBase)
        Dim tasks As New List(Of TaskBase)
        For Each curPlugin As PluginTaskBase In Me.getItemables()
            tasks.AddRange(curPlugin.getTasks())
        Next

        Return tasks
    End Function

    Public Function getTasksTypesInfos() As Generic.List(Of TaskTypeInfos)
        Dim tasksTypesInfos As New Generic.List(Of TaskTypeInfos)
        For Each curTaskKey As Integer In _items.Keys
            Dim curTaskType As PluginTaskBase = _items(curTaskKey)
            tasksTypesInfos.Add(curTaskType.infos)
        Next

        Return tasksTypesInfos
    End Function

    Public Sub dataConsume(ByVal dataReceived As DataTCP) Implements IDataConsumer(Of DataTCP).dataConsume
        Dim tcpAnswer As TCPAnswers.TCPAnswer = TCPAnswers.TCPAnswer.createTCPAnswer(TCPClient.getInstance(), dataReceived)
        addRemoteTask(tcpAnswer)

        Dim tcpCommand As TCPCommands.TCPCommand = TCPCommands.TCPCommand.createTCPCommand(TCPClient.getInstance(), dataReceived)
        delRemoteTask(tcpCommand)
    End Sub

    Private Sub delRemoteTask(ByVal tcpCommand As TCPCommands.TCPCommand)
        If tcpCommand Is Nothing Then Exit Sub
        If Not TypeOf tcpCommand Is TCPCommands.DelTask Then Exit Sub

        Dim delTask As TCPCommands.DelTask = tcpCommand
        Me.getItemable(delTask.taskTypeIdentifier).deleteTask(delTask.taskIdentifier)
        External.current.refreshTasks()

        'Resend to others
        Dim subscribers() As CI.Base.TCPClient = Me.subscribers.ToArray()
        For Each subscriber As TCPClient In subscribers
            Dim newDelTask As New TCPCommands.DelTask(subscriber, delTask.taskTypeIdentifier, delTask.taskIdentifier)
            newDelTask.execute()
        Next
    End Sub

    Private Sub addRemoteTask(ByVal tcpAnswer As TCPAnswers.TCPAnswer)
        If tcpAnswer Is Nothing Then Exit Sub
        If Not TypeOf tcpAnswer Is TCPAnswers.TaskInfo Then Exit Sub

        'Add task created by others
        With CType(tcpAnswer, TCPAnswers.TaskInfo)
            Dim taskExists As TaskBase = getTask(.infos.taskTypeIdentifier, .infos.noItemable)
            If taskExists Is Nothing Then
                Dim task As TaskBase = New TaskClient(getItemable(.infos.taskTypeIdentifier), .infos)
                addTask(.infos.taskTypeIdentifier, task)
                External.current.refreshTasks()
            End If
        End With
    End Sub

    Public Sub onOneLoopDone(ByVal currentAttemptTime As Integer, ByVal nbTries As Integer, ByVal maxTries As Integer, ByVal isNewTry As Boolean) Implements Base.ITcpWaiter.onOneLoopDone
        If Loading.getInstance.Visible Then
            If isNewTry Then
                Loading.getInstance.forward("Charge les types de tâches du serveur : Tentative #" & nbTries & " / " & maxTries)
            Else
                Loading.getInstance.appendDetail(".")
            End If
        End If
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataTCP).priority
        Get
            Return 0
        End Get
    End Property

    Public Function CompareTo(ByVal other As IDataConsumer(Of DataTCP)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataTCP)).CompareTo
        Return other.priority.CompareTo(Me.priority)
    End Function
End Class
