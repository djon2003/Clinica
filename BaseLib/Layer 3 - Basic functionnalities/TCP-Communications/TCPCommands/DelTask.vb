Namespace TCPCommands


    Public Class DelTask
        Inherits TCPCommand

        Public Const NAME As String = "DELTASK"
        Private _taskTypeIdentifier As String = ""
        Private _taskIdentifier As Integer = 0

        Public Sub New(ByVal client As TCPClient, ByVal taskTypeIdentifier As String, ByVal taskIdentifier As Integer)
            MyBase.New(client)
            Me._taskTypeIdentifier = taskTypeIdentifier
            Me._taskIdentifier = taskIdentifier
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            _taskTypeIdentifier = data.args(0)
            _taskIdentifier = data.args(1)
        End Sub

#Region "Properties"
        Public ReadOnly Property taskTypeIdentifier() As String
            Get
                Return _taskTypeIdentifier
            End Get
        End Property

        Public ReadOnly Property taskIdentifier() As Integer
            Get
                Return _taskIdentifier
            End Get
        End Property
#End Region

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim curPlugin As PluginTaskBase = PluginTasksManager.getInstance.getItemable(_taskTypeIdentifier)
                curPlugin.deleteTask(_taskIdentifier)

                External.current.refreshTasks()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _taskTypeIdentifier & DataTCP.PARAMS_SEPARATOR & _taskIdentifier
        End Function
    End Class


End Namespace