Namespace TCPCommands


    Public Class IsUpdate
        Inherits TCPCommand

        Public Const NAME As String = "ISUPDATE"

        Public Sub New(ByVal client As TCPClient)
            MyBase.New(client)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim isUpdated As New TCPAnswers.IsUpdated(Me)
                isUpdated.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & "SERVER"
        End Function
    End Class


End Namespace
