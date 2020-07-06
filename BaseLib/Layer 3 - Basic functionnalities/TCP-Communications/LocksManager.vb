Public Class LocksManager
    Inherits ManagerBase(Of LocksManager)
    Implements IDataConsumer(Of DataTCP)

#Region "Constructor"
    Protected Sub New()
        isHost = TCPHost.getInstance().isListening

        'Those are required when Server receives an answer OR Client receives a command
        If Not isHost Then
            TCPClient.getInstance().addConsumer(Me)
        Else
            TCPHost.getInstance().addConsumer(Me)
        End If
    End Sub
#End Region

    Private currentLocks As New Generic.Dictionary(Of String, Lock)
    Private isHost As Boolean = True
    Private Shared lockSynch As New System.Threading.Mutex()

    Public Sub dataConsume(ByVal dataReceived As DataTCP) Implements IDataConsumer(Of DataTCP).dataConsume
        If isHost Then
            If dataReceived.command <> "LOCKS" Then Exit Sub

            'Receive locks from client
            loadLocks(dataReceived)
        Else
            If dataReceived.command <> "GETLOCKS" Then Exit Sub

            'Send current locks to server
            sendLocks()
        End If
    End Sub

    Public Function isSectorLocked(ByVal lock As Lock, Optional ByVal sectorNameStartingWith As Boolean = False)
        If isHost Then
            Dim isLocked As Boolean = False

            lockSynch.WaitOne()

            If sectorNameStartingWith Then
                For Each curKey As String In currentLocks.Keys
                    If curKey.StartsWith(lock.sectorId) Then
                        isLocked = True
                        Exit For
                    End If
                Next
            Else
                isLocked = currentLocks.ContainsKey(lock.sectorId)
            End If

            lockSynch.ReleaseMutex()

            Return isLocked
        Else
            Dim isSectorLockedCmd As New TCPCommands.IsLocked(TCPClient.getInstance(), lock, sectorNameStartingWith)

            'TODO: REACTIVATE !!!
            'lockSynch.WaitOne()
            'Dim answerReturn As TCPCommunicationBase = TCPHelper.getInstance().sendWaitCommunication(isSectorLockedCmd, GetType(TCPAnswers.LockAnswer), 0)
            'lockSynch.ReleaseMutex()

            'Dim lockAnswer As TCPAnswers.LockAnswer = answerReturn

            'Return lockAnswer.didLocked
        End If
    End Function

    Public Function lockSector(ByVal lock As Lock, ByVal isLocked As Boolean) As Boolean
        Return lockSector(lock, isLocked, Nothing)
    End Function

    Private Function lockSectorForClient(ByVal lock As Lock, ByVal isLocked As Boolean) As Boolean
        Dim setLock As New TCPCommands.SetLock(TCPClient.getInstance(), lock, isLocked)

        'TODO: REACTIVATE !!!
        'lockSynch.WaitOne()
        'Dim answerReturn As TCPCommunicationBase = TCPHelper.getInstance().sendWaitCommunication(setLock, GetType(TCPAnswers.LockAnswer), 0)
        'lockSynch.ReleaseMutex()

        'Dim lockAnswer As TCPAnswers.LockAnswer = answerReturn

        'If lockAnswer.didLocked AndAlso isLocked Then
        '    currentLocks.Add(lock.sectorId, lock)
        'ElseIf Not isLocked AndAlso currentLocks.ContainsKey(lock.sectorId) Then
        '    currentLocks.Remove(lock.sectorId)
        'End If

        'Return lockAnswer.didLocked
    End Function


    Private Function lockSectorForHost(ByVal lock As Lock, ByVal isLocked As Boolean, ByVal lockCommand As TCPCommands.TCPCommand) As Boolean
        Dim didLocked As Boolean = False

        lockSynch.WaitOne()

        If isLocked Then
            If Not currentLocks.ContainsKey(lock.sectorId) Then
                currentLocks.Add(lock.sectorId, lock)
                didLocked = True
            End If
        Else
            If currentLocks.ContainsKey(lock.sectorId) AndAlso currentLocks(lock.sectorId).lockedBy = lock.lockedBy Then
                currentLocks.Remove(lock.sectorId)
                didLocked = True
            End If
        End If

        lockSynch.ReleaseMutex()

        Dim lockAnswer As New TCPAnswers.LockAnswer(lockCommand, lock, didLocked)
        lockAnswer.execute()

        Return didLocked
    End Function

    Public Function lockSector(ByVal lock As Lock, ByVal isLocked As Boolean, ByVal lockCommand As TCPCommands.TCPCommand) As Boolean
        If isHost Then
            Return lockSectorForHost(lock, isLocked, lockCommand)
        Else
            Return lockSectorForClient(lock, isLocked)
        End If
    End Function

    Private Sub loadLocks(ByVal dataReceived As DataTCP)
        lockSynch.WaitOne()

        For Each lockString As String In dataReceived.args
            If lockString = String.Empty Then Exit For

            Dim newLock As New Lock(lockString.Replace("§§§", DataTCP.PARAMS_SEPARATOR))
            currentLocks.Add(newLock.sectorId, newLock)
        Next

        lockSynch.ReleaseMutex()
    End Sub

    Private Sub sendLocks()
        Dim locksAnswer As New TCPAnswers.LocksAnswer(TCPClient.getInstance(), currentLocks)
        locksAnswer.execute()
    End Sub

    Public Sub getLocks(ByVal client As TCPClient)
        'TODO : Shall have a blocking method so that a current lock is not taken by someone else meanwhile waiting for answer

        Dim getLocks As New TCPCommands.GetLocks(client)
        getLocks.execute()
    End Sub

    Public Sub clearLocks(ByVal clientId As String)
        lockSynch.WaitOne()

        Dim currentKeys() As String
        ReDim currentKeys(currentLocks.Count - 1)
        currentLocks.Keys.CopyTo(currentKeys, 0)

        For Each curSectorId As String In currentKeys
            If currentLocks(curSectorId).lockedBy = clientId Then currentLocks.Remove(curSectorId)
        Next

        lockSynch.ReleaseMutex()
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataTCP).priority
        Get
            Return 100
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataTCP)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataTCP)).CompareTo
        other.priority.CompareTo(priority)
    End Function
End Class
