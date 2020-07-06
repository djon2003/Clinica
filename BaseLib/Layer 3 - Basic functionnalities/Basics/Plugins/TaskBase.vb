Public MustInherit Class TaskBase
    Implements IItemable

    Private Const ENDED_STEP_NAME = "Terminé"
    Private Const CANCELED_STEP_NAME = "Annulé"

    Private _currentStepName As String = ""
    Private taskProgression As Double = -1
    Private oldTaskProgression As Integer = -1
    Private _currentStep As Integer = 0
    Private _isTaskRunning As Boolean = False
    Private runningThread As Threading.Thread
    Private Shared nbInstances As Integer = 0
    Private _noItemable As Integer
    Private _creator As PluginTaskBase
    Private _isCancelled As Boolean = False
    Private _resultHtml As String = ""
    Private _nbErrors, _nbProcessed As Integer

    Public Sub New(ByVal creator As PluginTaskBase)
        _creator = creator
        nbInstances += 1
        _noItemable = nbInstances
    End Sub

#Region "Properties"
    Public ReadOnly Property nbErrors() As Integer
        Get
            Return _nbErrors
        End Get
    End Property

    Public ReadOnly Property nbProcessed() As Integer
        Get
            Return _nbProcessed
        End Get
    End Property

    Public ReadOnly Property resultHtml() As String
        Get
            Return _resultHtml
        End Get
    End Property

    Public ReadOnly Property noItemable() As Integer Implements IItemable.noItemable
        Get
            Return _noItemable
        End Get
    End Property

    Public Overridable ReadOnly Property isCancelled() As Boolean
        Get
            Return _isCancelled
        End Get
    End Property

    Public Overridable ReadOnly Property isTaskRunning() As Boolean
        Get
            Return _isTaskRunning
        End Get
    End Property

    Public ReadOnly Property currentStep() As Integer
        Get
            Return _currentStep
        End Get
    End Property

    Public ReadOnly Property currentStepName() As String
        Get
            Return _currentStepName
        End Get
    End Property

    Public ReadOnly Property creator() As PluginTaskBase
        Get
            Return _creator
        End Get
    End Property

#End Region

    Protected Sub setNoItemable(ByVal newNoItemable As Integer)
        Dim oldNoItemable As Integer = _noItemable
        _noItemable = newNoItemable
        onNoItemableChanged(oldNoItemable, newNoItemable)
    End Sub

    Public Overrides Function ToString() As String
        Return _creator.name & " #" & _noItemable
    End Function

    Public Event stepChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event taskStepProgressed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event taskEnded(ByVal sender As Object, ByVal e As EventArgs)
    Public MustOverride ReadOnly Property maximumSteps() As Integer
    Protected MustOverride Sub doTask()

    Private Sub informClientOfChanges()
        If _creator.isRemote = False Then
            Dim subscribers() As CI.Base.TCPClient = PluginTasksManager.getInstance().subscribers.ToArray()

            For Each curClient As TCPClient In subscribers
                Dim taskInfo As New TCPAnswers.TaskInfo(curClient, Me)
                taskInfo.execute()
            Next
        End If
    End Sub

    Private Sub informClientOfEnd()
        If _creator.isRemote = False Then
            PluginTasksManager.getInstance.stopTask(_creator.getIdentifier(), noItemable)
        End If
    End Sub

    Protected Overridable Sub onTaskAborted(ByVal e As EventArgs)
        runningThread.Abort()
    End Sub

    Protected Overridable Sub onTaskEnded(ByVal e As EventArgs)
        _isTaskRunning = False

        setCurrentStepName(If(isCancelled, CANCELED_STEP_NAME, ENDED_STEP_NAME))
        onStepChanged(EventArgs.Empty)

        RaiseEvent taskEnded(Me, e)

        informClientOfEnd()
    End Sub

    Protected Overridable Sub onTaskStepProgressed(ByVal e As EventArgs)
        RaiseEvent taskStepProgressed(Me, e)

        If oldTaskProgression <> Convert.ToInt16(taskProgression) Then
            informClientOfChanges()
            oldTaskProgression = taskProgression
        End If
    End Sub

    Protected Overridable Sub onStepChanged(ByVal e As EventArgs)
        taskProgression = -1

        RaiseEvent stepChanged(Me, e)

        informClientOfChanges()
    End Sub

    Protected Overridable Sub gotoNextStep()
        If _currentStep = maximumSteps Then Exit Sub

        _currentStep += 1

        onStepChanged(EventArgs.Empty)
    End Sub

    Protected Sub setCurrentStep(ByVal currentStep As Integer)
        _currentStep = currentStep
        onStepChanged(EventArgs.Empty)
    End Sub

    Protected Overridable Sub resetSteps()
        Dim oldStep As Integer = _currentStep

        _currentStepName = ""
        _currentStep = 0
        taskProgression = 0

        If oldStep <> 0 Then onStepChanged(EventArgs.Empty)
    End Sub

    Protected Sub setCurrentStepName(ByVal stepName As String)
        _currentStepName = stepName
    End Sub

    Private Sub _doTask()
        Try
            doTask()
        Catch ex As Exception
            'TODO : Could be ameliorated by having a "isError"
            _isCancelled = True
            onTaskEnded(EventArgs.Empty)
            External.propagateErrorLog(New Exception("ident=" & Me.creator.getIdentifier(), ex))
        End Try
    End Sub

    Public Overridable Sub startTask()
        _isTaskRunning = True
        runningThread = New Threading.Thread(AddressOf _doTask)
        runningThread.Start()
    End Sub

    Public Overridable Sub stopTask()
        If _isTaskRunning Then _isCancelled = True
        If runningThread IsNot Nothing AndAlso runningThread.IsAlive Then onTaskAborted(EventArgs.Empty)

        onTaskEnded(EventArgs.Empty)
    End Sub

    Public Overridable Sub deleteTask()
        creator.deleteTask(Me)
        External.current.refreshTasks()
    End Sub

    Public Function getTaskProgession() As Double
        Return taskProgression
    End Function

    Protected Sub setTaskProgession(ByVal taskProgression As Double)
        Me.taskProgression = taskProgression
        onTaskStepProgressed(EventArgs.Empty)
    End Sub

    Protected Sub setIsTaskRunning(ByVal isRunning As Boolean)
        _isTaskRunning = isRunning
    End Sub

    Protected Sub setResultHtml(ByVal html As String)
        _resultHtml = html
    End Sub

    Protected Sub setNbErrors(ByVal nbErrors As Integer)
        _nbErrors = nbErrors
    End Sub

    Protected Sub setNbProcessed(ByVal nbProcessed As Integer)
        _nbProcessed = nbProcessed
    End Sub

    Public Event noItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer) Implements IItemable.noItemableChanged

    Protected Overridable Sub onNoItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer)
        If oldNoItemable <> newNoItemable Then RaiseEvent noItemableChanged(oldNoItemable, newNoItemable)
    End Sub
End Class
