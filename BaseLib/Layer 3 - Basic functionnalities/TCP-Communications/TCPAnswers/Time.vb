Namespace TCPAnswers


    Public Class Time
        Inherits TCPAnswer

        Public Const NAME As String = "SERVER-TIME"
        Private _serverDate As Date

        Public Sub New(ByVal client As TCPClient)
            MyBase.New(client)
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.Time)
            MyBase.New(tcpMessage)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)

            _serverDate = data.args(0)
        End Sub

        Public ReadOnly Property serverDate() As Date
            Get
                Return _serverDate
            End Get
        End Property

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else

            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.FullTime)
        End Function
    End Class


End Namespace