Imports System.Runtime.Serialization.Formatters.Binary

Namespace TCPCommands


    Public Class IsLocked
        Inherits TCPCommand

        Public Const NAME As String = "ISLOCKED"
        Private _lock As Lock
        Private _sectorNameStartingWith As Boolean

        Public Sub New(ByVal client As TCPClient, ByVal lock As Lock, ByVal sectorNameStartingWith As Boolean)
            MyBase.New(client)

            _lock = lock
            _sectorNameStartingWith = sectorNameStartingWith
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)

            _lock = New Lock(data.args(0), data.args(1).Replace("§§", DataTCP.PARAMS_SEPARATOR), data.args(2).Replace("§§", DataTCP.PARAMS_SEPARATOR))
            _sectorNameStartingWith = If(data.args(3) = "1", True, False)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim isLocked As Boolean = LocksManager.getInstance().isSectorLocked(_lock, _sectorNameStartingWith)
                Dim lockAnswer As New TCPAnswers.LockAnswer(Me, _lock, isLocked)
                lockAnswer.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & _lock.ToString() & DataTCP.PARAMS_SEPARATOR & If(_sectorNameStartingWith, "1", "0")
        End Function
    End Class


End Namespace