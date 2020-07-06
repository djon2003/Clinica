Imports CI.Base

Public Class Plugin
    Inherits PluginTaskBase

    Private Const MAXIMUM_CONCURRENT_TASKS As Integer = 5

#Region "Properties"
    Public Overrides ReadOnly Property description() As String
        Get
            Return "Module pour tester le système de tâche"
        End Get
    End Property

    Public Overrides ReadOnly Property name() As String
        Get
            Return "Dummy Task"
        End Get
    End Property

    Public Overrides Property maximumConcurrentTasks() As Integer
        Get
            Return MAXIMUM_CONCURRENT_TASKS
        End Get
        Set(ByVal value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Overrides ReadOnly Property hasToConfigure() As Boolean
        Get
            Return Config.current.hasToUpdate
        End Get
    End Property
#End Region

    Public Overrides Sub configure()

    End Sub

    Public Overrides Sub initialize()
        
    End Sub

    Public Overrides ReadOnly Property taskType() As System.Type
        Get

            Return GetType(DummyTask)
        End Get
    End Property


End Class
