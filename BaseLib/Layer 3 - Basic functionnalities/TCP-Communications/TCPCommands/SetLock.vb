Imports System.Runtime.Serialization.Formatters.Binary

Namespace TCPCommands


    Public Class SetLock
        Inherits TCPCommand

        Public Const NAME As String = "SETLOCK"
        Private _lock As Lock
        Private _isLocked As Boolean

        Public Sub New(ByVal client As TCPClient, ByVal lock As Lock, ByVal isLocked As Boolean)
            MyBase.New(client)

            _lock = lock
            _isLocked = isLocked
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)

            _lock = New Lock(data.args(0), data.args(1).Replace("§§", DataTCP.PARAMS_SEPARATOR), data.args(2).Replace("§§", DataTCP.PARAMS_SEPARATOR))
            _isLocked = If(data.args(3) = "1", True, False)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                LocksManager.getInstance().lockSector(_lock, _isLocked, Me)
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _lock.ToString() & DataTCP.PARAMS_SEPARATOR & If(_isLocked, "1", "0")
        End Function
    End Class


End Namespace