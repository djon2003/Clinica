Public Class PluginTaskClient
    Inherits PluginTaskBase

    Private _description As String = ""
    Private _name As String = ""
    Private _hasToConfigure As Boolean
    Private _maximumConcurrentTasks As Integer
    Private _identifier As String = ""

    Public Sub New(ByVal data() As String)
        MyBase.New()

        _identifier = data(0)
        _hasToConfigure = data(1)
        _maximumConcurrentTasks = data(2)
        _name = data(3)
        _description = data(4).Replace("§", vbCrLf)
    End Sub

    Public Overrides Function getIdentifier() As String
        Return _identifier
    End Function

    Public Overrides ReadOnly Property description() As String
        Get
            Return _description
        End Get
    End Property


    Public Overrides ReadOnly Property hasToConfigure() As Boolean
        Get
            Return _hasToConfigure
        End Get
    End Property

    Public Overrides Property maximumConcurrentTasks() As Integer
        Get
            Return _maximumConcurrentTasks
        End Get
        Set(ByVal value As Integer)
            _maximumConcurrentTasks = value
        End Set
    End Property

    Public Overrides ReadOnly Property name() As String
        Get
            Return _name
        End Get
    End Property

    Public Overrides ReadOnly Property taskType() As System.Type
        Get
            Return GetType(TaskClient)
        End Get
    End Property

    Public Overrides Sub initialize()
        'Nothing to do
    End Sub

    Public Overrides Sub configure()
        'Nothing to do
    End Sub
End Class
