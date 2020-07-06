Public MustInherit Class PluginTaskBase
    Inherits PluginBase

    Private tasks As New List(Of TaskBase)
    Private _infos As TaskTypeInfos
    Private creationLock As New System.Threading.Mutex()

    Public Event taskDeleted(ByVal sender As Object, ByVal task As TaskBase)

    Public Sub New()
        MyBase.New()

        _infos = New TaskTypeInfos(Me)
    End Sub

#Region "Properties"
    Public ReadOnly Property infos() As TaskTypeInfos
        Get
            Return _infos
        End Get
    End Property
#End Region

    Public Function getTasksRunning() As List(Of TaskBase)
        Return getTasks(True)
    End Function

    Public Function getTasks() As List(Of TaskBase)
        Return getTasks(False)
    End Function

    Private Function getTasks(ByVal onlyRunningOnes As Boolean) As List(Of TaskBase)
        Dim tasks As New List(Of TaskBase)
        For Each curTask As TaskBase In Me.tasks
            If Not onlyRunningOnes OrElse CType(CType(curTask, Object), TaskBase).isTaskRunning Then tasks.Add(curTask)
        Next

        Return tasks
    End Function

    Public Sub stopTask(ByVal noTask As Integer)
        For Each curTask As TaskBase In getTasksRunning()
            If curTask.noItemable = noTask Then
                curTask.stopTask()
            End If
        Next
    End Sub

    Private Sub taskEnded(ByVal sender As Object, ByVal e As EventArgs)
        'TODO : Maybe not now...?
        'tasks.Remove(sender)
    End Sub

    ''' <summary>
    ''' Create a new task that will run in a separate thread
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This function returns nothing when the maximum concurrent tasks running have been reached</remarks>
    Public Function createTask() As TaskBase
        creationLock.WaitOne()

        If getTasksRunning().Count = maximumConcurrentTasks Then Return Nothing

        Dim task As TaskBase
        Try
            task = Activator.CreateInstance(taskType, New Object() {Me})
            AddHandler CType(CType(task, Object), TaskBase).taskEnded, AddressOf taskEnded
            If Not isRemote Then tasks.Add(task)

        Catch ex As Exception
            Throw ex
        Finally
            creationLock.ReleaseMutex()
        End Try

        Return task
    End Function

    ''' <summary>
    ''' Add already created task (by another computer)
    ''' </summary>
    ''' <param name="task"></param>
    ''' <remarks></remarks>
    Friend Sub addTask(ByVal task As TaskBase)
        tasks.Add(task)
    End Sub

    Public Sub deleteTask(ByVal noTask As Integer)
        For Each task In tasks
            If task.noItemable = noTask Then
                deleteTask(task)
                Exit For
            End If
        Next
    End Sub

    Public Sub deleteTask(ByVal task As TaskBase)
        tasks.Remove(task)

        RaiseEvent taskDeleted(Me, task)
    End Sub

    Public Sub clearTasks()
        tasks.Clear()
    End Sub

    Public MustOverride Property maximumConcurrentTasks() As Integer
    Public MustOverride ReadOnly Property taskType() As Type

    Public Overrides ReadOnly Property pluginType() As Plugin.PluginTypes
        Get
            Return Plugin.PluginTypes.Task
        End Get
    End Property
End Class
