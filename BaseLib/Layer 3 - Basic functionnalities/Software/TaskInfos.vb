Public Class TaskInfos

    Private Const INFO_SPLITTER As String = "§§"
    Public Const NO_RESULT_HTML_CHANGE As String = "  "
    Private _taskTypeIdentifier As String = ""
    Private _currentStep, _noItemable, _maximumSteps, _clientTaskIdentifier As Integer
    Private _currentStepName As String = "", _clientIdentifier As String = "", _resultHtml As String = ""
    Private Shared _lastResultHtml As New Generic.Dictionary(Of Integer, String)
    Private _isCancelled, _isTaskRunning As Boolean
    Private _progression As Double
    Private _nbErrors, _nbProcessed As Integer


#Region "Constructors"
    Public Sub New(ByVal task As TaskBase, Optional ByVal byPassResultCheck As Boolean = False)
        _taskTypeIdentifier = task.creator.getIdentifier()
        _noItemable = task.noItemable
        _maximumSteps = task.maximumSteps
        _currentStep = task.currentStep
        _currentStepName = task.currentStepName
        _isCancelled = task.isCancelled
        _isTaskRunning = task.isTaskRunning
        _progression = task.getTaskProgession()
        _nbErrors = task.nbErrors
        _nbProcessed = task.nbProcessed
        If byPassResultCheck Then
            _resultHtml = task.resultHtml
        Else
            If isTaskRunning = False OrElse _lastResultHtml.ContainsKey(task.noItemable) = False OrElse _lastResultHtml(task.noItemable) <> task.resultHtml Then
                If _lastResultHtml.ContainsKey(task.noItemable) = False Then _lastResultHtml.Add(task.noItemable, "")
                _resultHtml = task.resultHtml
                _lastResultHtml(task.noItemable) = _resultHtml
            ElseIf _lastResultHtml(task.noItemable) = task.resultHtml Then
                _resultHtml = NO_RESULT_HTML_CHANGE
            End If
        End If
    End Sub

    Public Sub New(ByVal task As TaskBase, ByVal clientIdentifier As String, ByVal clientTaskIdentifier As Integer)
        Me.New(task)
        _clientTaskIdentifier = clientTaskIdentifier
        _clientIdentifier = clientIdentifier
    End Sub

    Public Sub New(ByVal newData As String)
        Dim data() As String = newData.Split(New String() {INFO_SPLITTER}, StringSplitOptions.None)
        _taskTypeIdentifier = data(0)
        _noItemable = data(1)
        _maximumSteps = data(2)
        _currentStep = data(3)
        _currentStepName = data(4).Trim
        _isCancelled = data(5)
        _isTaskRunning = data(6)
        Try
            _progression = data(7)
        Catch ex As System.InvalidCastException
            _progression = data(7).Replace(".", ",")
        End Try
        _nbErrors = data(8)
        _nbProcessed = data(9)
        _resultHtml = data(10).Replace(vbLf, vbCrLf).Trim
        _clientTaskIdentifier = data(11)
        _clientIdentifier = data(12).Trim
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property progression() As Double
        Get
            Return _progression
        End Get
    End Property

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

    Public ReadOnly Property clientTaskIdentifier() As Integer
        Get
            Return _clientTaskIdentifier
        End Get
    End Property

    Public ReadOnly Property resultHtml() As String
        Get
            Return _resultHtml
        End Get
    End Property

    Public ReadOnly Property clientIdentifier() As String
        Get
            Return _clientIdentifier
        End Get
    End Property

    Public ReadOnly Property noItemable() As Integer
        Get
            Return _noItemable
        End Get
    End Property

    Public ReadOnly Property isCancelled() As Boolean
        Get
            Return _isCancelled
        End Get
    End Property

    Public ReadOnly Property isTaskRunning() As Boolean
        Get
            Return _isTaskRunning
        End Get
    End Property

    Public ReadOnly Property currentStepName() As String
        Get
            Return _currentStepName
        End Get
    End Property

    Public ReadOnly Property taskTypeIdentifier() As String
        Get
            Return _taskTypeIdentifier
        End Get
    End Property

    Public ReadOnly Property maximumSteps() As Integer
        Get
            Return _maximumSteps
        End Get
    End Property

    Public ReadOnly Property currentStep() As Integer
        Get
            Return _currentStep
        End Get
    End Property
#End Region

    Public Overrides Function ToString() As String
        Return _taskTypeIdentifier & _
                INFO_SPLITTER & _noItemable & _
                INFO_SPLITTER & _maximumSteps & _
                INFO_SPLITTER & _currentStep & _
                INFO_SPLITTER & If(_currentStepName = "", " ", _currentStepName) & _
                INFO_SPLITTER & _isCancelled & _
                INFO_SPLITTER & _isTaskRunning & _
                INFO_SPLITTER & _progression & _
                INFO_SPLITTER & _nbErrors & _
                INFO_SPLITTER & _nbProcessed & _
                INFO_SPLITTER & If(_resultHtml = "", " ", _resultHtml.Replace(vbCrLf, vbLf)) & _
                INFO_SPLITTER & _clientTaskIdentifier & _
                INFO_SPLITTER & If(_clientIdentifier = "", " ", _clientIdentifier)
    End Function
End Class
