Namespace TCPAnswers


    Public Class LockAnswer
        Inherits TCPAnswer

        Public Const NAME As String = "LOCK"
        Private _lock As Lock
        Private _didLocked As Boolean

        Public Sub New(ByVal client As TCPClient, ByVal lock As Lock, ByVal didLocked As Boolean)
            MyBase.New(client)

            Me._lock = lock
            Me._didLocked = didLocked
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.TCPCommand, ByVal lock As Lock, ByVal didLocked As Boolean)
            MyBase.New(tcpMessage)

            Me._lock = lock
            Me._didLocked = didLocked
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)

            _lock = New Lock(data.args(0), String.Empty, String.Empty)
            _didLocked = If(data.args(1) = "1", True, False)
        End Sub

        Public ReadOnly Property didLocked() As Boolean
            Get
                Return _didLocked
            End Get
        End Property

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else
                'Nothing to do for client, answer processed within LocksManager.lockSector
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _lock.sectorId & DataTCP.PARAMS_SEPARATOR & If(_didLocked, "1", "0")
        End Function
    End Class


End Namespace