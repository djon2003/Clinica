Namespace TCPAnswers


    Public MustInherit Class TCPAnswer
        Inherits TCPCommunicationBase

        Public Sub New(ByVal client As TCPClient)
            MyBase.New(client)
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommunicationBase)
            MyBase.New(tcpMessage)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Shared Function createTCPAnswer(ByVal client As TCPClient, ByVal receivedData As DataTCP) As TCPAnswer
            Return createTCPCommunication(GetType(TCPAnswer), client, receivedData)
        End Function
    End Class


End Namespace