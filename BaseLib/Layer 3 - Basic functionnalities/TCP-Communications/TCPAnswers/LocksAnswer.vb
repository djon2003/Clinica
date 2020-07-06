Namespace TCPAnswers


    Public Class LocksAnswer
        Inherits TCPAnswer

        Public Const NAME As String = "LOCKS"
        Private _locks As Generic.Dictionary(Of String, Lock)
        Private locksString As String = String.Empty

        Public Sub New(ByVal client As TCPClient, ByVal locks As Generic.Dictionary(Of String, Lock))
            MyBase.New(client)

            Me._locks = locks
            For Each sectorId As String In _locks.Keys
                locksString &= DataTCP.PARAMS_SEPARATOR & _locks(sectorId).ToString().Replace(DataTCP.PARAMS_SEPARATOR, "§§§")
            Next
            If locksString <> String.Empty Then locksString = locksString.Substring(1)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                'Nothing to do for server, answer processed within LocksManager.lockSector
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & locksString
        End Function
    End Class


End Namespace