Namespace TCPCommands


    Public Class UpdateLock
        Inherits TCPCommand

        Public Const NAME As String = "UPDATELOCK"

        Private isLocked As Boolean

        Public Sub New(ByVal client As TCPClient, ByVal isLocked As Boolean)
            MyBase.New(client)
            Me.isLocked = isLocked
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            Me.isLocked = If(data.args(0) = 1, True, False)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim canUpdate As Boolean = False
                If isLocked Then
                    canUpdate = TCPHost.getInstance().disallowConnection()
                Else
                    TCPHost.getInstance().allowConnection()
                End If

                Dim canUpdateAnswer As New TCPAnswers.CanUpdate(Me, canUpdate)
                canUpdateAnswer.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & If(isLocked, 1, 0)
        End Function
    End Class


End Namespace