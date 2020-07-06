Public Class TaskClient
    Inherits TaskBase
    Implements IDataConsumer(Of DataTCP)

    Private _maximumSteps As Integer
    Private _isCancelled As Boolean
    Private _isTaskRunning = True
    Private isStopped As Boolean = False

    Public Sub New(ByVal creator As PluginTaskBase)
        MyBase.New(creator)

        TCPClient.getInstance().addConsumer(Me)
    End Sub

    Public Sub New(ByVal creator As PluginTaskBase, ByVal data As TaskInfos)
        Me.New(creator)

        fillProperties(data, data.noItemable)
        setIsTaskRunning(_isTaskRunning)
    End Sub

#Region "Properties"
    Public Overrides ReadOnly Property isCancelled() As Boolean
        Get
            Return _isCancelled
        End Get
    End Property

    Public Overrides ReadOnly Property isTaskRunning() As Boolean
        Get
            Return _isTaskRunning
        End Get
    End Property
#End Region

    Public Overrides Sub stopTask()
        Dim stopTask As New TCPCommands.StopTask(TCPClient.getInstance(), Me.creator.getIdentifier(), Me.noItemable)
        stopTask.execute()

        MyBase.stopTask()

        _isTaskRunning = False
        isStopped = True
    End Sub

    Public Overrides Sub deleteTask()
        Dim delTask As New TCPCommands.DelTask(TCPClient.getInstance(), Me.creator.getIdentifier(), Me.noItemable)
        delTask.execute()

        MyBase.deleteTask()
    End Sub

    Protected Overrides Sub doTask()
        Dim createTask As New TCPCommands.CreateTask(TCPClient.getInstance(), Me.creator.getIdentifier(), Me.noItemable)
        createTask.execute()
    End Sub

    Public Overrides ReadOnly Property maximumSteps() As Integer
        Get
            Return _maximumSteps
        End Get
    End Property

    Private Sub manageEnds(ByVal tcpCommand As TCPCommands.TCPCommand)
        If TypeOf tcpCommand Is TCPCommands.StopTask Then
            Dim stopTask As TCPCommands.StopTask = tcpCommand
            If stopTask.taskIdentifier = Me.noItemable AndAlso stopTask.taskTypeIdentifier = Me.creator.getIdentifier() Then
                isStopped = True
                _isTaskRunning = False
                Me.stopTask()
            End If
        End If
    End Sub

    Private Sub fillProperties(ByVal infos As TaskInfos, Optional ByVal noItemable As Integer = 0)
        With infos
            'Change noItemable when this client created the task
            If noItemable <> 0 Then Me.setNoItemable(noItemable)

            If Me.noItemable = .noItemable Then
                _maximumSteps = .maximumSteps
                Dim hasStepChanged As Boolean = currentStep <> .currentStep OrElse currentStepName <> .currentStepName
                Dim hasProgressChanged As Boolean = getTaskProgession() <> .progression
                setCurrentStep(.currentStep)
                setCurrentStepName(.currentStepName)
                setTaskProgession(.progression)
                _isCancelled = .isCancelled
                _isTaskRunning = .isTaskRunning

                If hasStepChanged Then onStepChanged(EventArgs.Empty)
                If hasProgressChanged Then onTaskStepProgressed(EventArgs.Empty)

                If .resultHtml <> TaskInfos.NO_RESULT_HTML_CHANGE Then setResultHtml(.resultHtml)
                setNbErrors(.nbErrors)
                setNbProcessed(.nbProcessed)
            End If
        End With
    End Sub

    Private Sub manageChanges(ByVal tcpAnswer As TCPAnswers.TCPAnswer)
        'Manage changes
        If TypeOf tcpAnswer Is TCPAnswers.TaskInfo Then
            Dim infos As TaskInfos = CType(tcpAnswer, TCPAnswers.TaskInfo).infos
            With infos
                Dim isNeededPlugin As Boolean = Me.creator.getIdentifier() = .taskTypeIdentifier
                If Not isNeededPlugin Then Exit Sub

                Dim newNoItemable As Integer = 0

                fillProperties(infos, newNoItemable)
            End With
        End If
    End Sub

    Public Sub dataConsume(ByVal dataReceived As DataTCP) Implements IDataConsumer(Of DataTCP).dataConsume
        'Ensure shall still listen
        Dim mySelf As TaskBase = PluginTasksManager.getInstance.getTask(Me.creator.getIdentifier(), Me.noItemable)
        If mySelf Is Nothing OrElse mySelf.Equals(Me) = False Then
            TCPClient.getInstance.removeConsumer(Me)
            Exit Sub
        End If

        'Create TCPComm
        Dim tcpComm As TCPCommunicationBase = TCPAnswers.TCPAnswer.createTCPAnswer(TCPClient.getInstance(), dataReceived)
        If tcpComm Is Nothing Then tcpComm = TCPCommands.TCPCommand.createTCPCommand(TCPClient.getInstance(), dataReceived)

        'Manage TCPComm
        If TypeOf tcpComm Is TCPAnswers.TCPAnswer Then manageChanges(tcpComm)
        If TypeOf tcpComm Is TCPCommands.TCPCommand Then manageEnds(tcpComm)
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataTCP).priority
        Get
            Return -1
        End Get
    End Property

    Public Function CompareTo(ByVal other As IDataConsumer(Of DataTCP)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataTCP)).CompareTo

    End Function

    Protected Overrides Sub Finalize()
        TCPClient.getInstance().removeConsumer(Me)

        MyBase.Finalize()
    End Sub
End Class
