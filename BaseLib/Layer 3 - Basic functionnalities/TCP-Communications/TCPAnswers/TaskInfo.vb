Namespace TCPAnswers


    Public Class TaskInfo
        Inherits TCPAnswer

        Public Const NAME As String = "TASKINFO"
        Private _infos As TaskInfos

#Region "Constructors"
        Public Sub New(ByVal client As TCPClient, ByVal task As TaskBase)
            MyBase.New(client)
            _infos = New TaskInfos(task)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal task As TaskBase, ByVal clientIdentifier As String, ByVal clientTaskIdentifier As Integer)
            MyBase.New(client)
            _infos = New TaskInfos(task, clientIdentifier, clientTaskIdentifier)
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.CreateTask, ByVal task As TaskBase, ByVal clientIdentifier As String, ByVal clientTaskIdentifier As Integer)
            MyBase.New(tcpMessage)
            _infos = New TaskInfos(task, clientIdentifier, clientTaskIdentifier)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            _infos = New TaskInfos(data.getArgsString())
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property infos() As TaskInfos
            Get
                Return _infos
            End Get
        End Property
#End Region

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else

            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _infos.ToString()
        End Function
    End Class


End Namespace