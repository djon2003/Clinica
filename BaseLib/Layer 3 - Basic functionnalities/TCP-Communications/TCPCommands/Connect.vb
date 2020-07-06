Namespace TCPCommands


    Public Class Connect
        Inherits TCPCommand

        Public Const NAME As String = "CONNECTME"
        Private _identifier As String = String.Empty

#Region "Constructors"
        Public Sub New(ByVal client As TCPClient, ByVal identifier As String)
            MyBase.New(client)
            Me._identifier = identifier
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            Me._identifier = data.args(0)
        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property identifier() As String
            Get
                Return _identifier
            End Get
        End Property
#End Region

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim isJoined As Boolean = TCPHost.getInstance.addClient(data.args(0), getClient())

                Dim joinAnswer As New TCPAnswers.Join(Me, isJoined)
                joinAnswer.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _identifier
        End Function
    End Class


End Namespace