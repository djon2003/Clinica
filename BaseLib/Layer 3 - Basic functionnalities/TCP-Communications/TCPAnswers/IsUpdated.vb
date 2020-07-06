Namespace TCPAnswers


    Public Class IsUpdated
        Inherits TCPAnswer

        Public Const NAME As String = "SERVER-ISUPDATE"

        Private _isUpdated As Boolean

        Public Sub New(ByVal client As TCPClient)
            MyBase.New(client)
            _isUpdated = Not External.current.isUpdateSoftwareToDo()
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.IsUpdate)
            MyBase.New(tcpMessage)
            _isUpdated = Not External.current.isUpdateSoftwareToDo()
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            _isUpdated = data.args(0).Trim
        End Sub

        Public ReadOnly Property isUpdated() As Boolean
            Get
                Return _isUpdated
            End Get
        End Property

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else

            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _isUpdated
        End Function
    End Class


End Namespace