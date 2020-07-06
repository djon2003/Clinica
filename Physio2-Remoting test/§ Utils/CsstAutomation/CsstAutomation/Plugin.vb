Imports CI.Base

Public Class Plugin
    Inherits PluginTaskBase

    Private Const MAXIMUM_CONCURRENT_TASKS As Integer = 1 'Shouldn't be changed.. This plugin doesn't support concurrency

#Region "Properties"
    Public Overrides ReadOnly Property description() As String
        Get
            Return "Module permettant le transfert automatisé des dossiers pour la CSST"
        End Get
    End Property

    Public Overrides ReadOnly Property name() As String
        Get
            Return "Processus des dossiers pour la CSST"
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

    Public Shared Sub log(ByVal logText As String)
        Dim logFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "logs.txt"
        IO.File.AppendAllText(logFile, logText & vbCrLf)
    End Sub

    Public Overrides Sub configure()

    End Sub

    Public Overrides Sub initialize()
        'Ensure params installation and loading
        Params.current.ToString()
    End Sub

    Public Overrides ReadOnly Property taskType() As System.Type
        Get

            Return GetType(CsstTask)
        End Get
    End Property


End Class
