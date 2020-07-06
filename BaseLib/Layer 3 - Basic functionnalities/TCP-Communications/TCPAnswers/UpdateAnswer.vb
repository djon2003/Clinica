Namespace TCPAnswers


    Public Class UpdateAnswer
        Inherits TCPAnswer

        Public Const NAME As String = "SERVER-UPDATED"
        Private _answerError As String = ""

        Public Sub New(ByVal client As TCPClient, ByVal answerError As String)
            MyBase.New(client)

            Me._answerError = answerError
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.Update, ByVal answerError As String)
            MyBase.New(tcpMessage)

            Me._answerError = answerError
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            _answerError = data.getArgsString()
        End Sub

        Public ReadOnly Property answerError() As String
            Get
                Return _answerError
            End Get
        End Property

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else

            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _answerError
        End Function
    End Class


End Namespace
