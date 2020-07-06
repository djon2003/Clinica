Public Class TCPHelper
    Inherits ManagerBase(Of TCPHelper)
    Implements IDataConsumer(Of DataTCP)

    Private Const NB_LOOP_FOR_NEXT_ATTEMPT As Integer = 50 ' 5 seconds for a 100ms loop
    Private Const NB_MS_FOR_A_LOOP As Integer = 100

    Public Const MAX_MESSAGE_ATTEMPTS As Integer = 10
    Public Const ATTEMPT_TIME_MS As Integer = NB_LOOP_FOR_NEXT_ATTEMPT * NB_MS_FOR_A_LOOP

    Private waitingDataLock As New Object
    Private waitingData As New Dictionary(Of String, Dictionary(Of Type, TCPCommunicationBase))
    Private guidChain As New Dictionary(Of String, String)

    Private producers As New Dictionary(Of IDataConsumable(Of DataTCP), Integer)

    Protected Sub New()
    End Sub

    ''' <summary>
    ''' Send and wait for a TCPCommand or a TCPAnswer.
    ''' </summary>
    ''' <param name="commType">TCPCommand/TCPAnswer derived class type</param>
    ''' <param name="maximumMilliseconds">Maximum milliseconds to wait the data. If set to minus one, it means a default timeout. If set to zero, it means an infinite timeout.</param>
    ''' <returns>A TCPCommand or a TCPAnswer</returns>
    ''' <remarks></remarks>
    Public Function sendWaitCommunication(ByVal tcpWaiter As ITcpWaiter, ByVal task As TCPCommands.TCPCommand, ByVal commTypeToWait As Type, Optional ByVal maximumMilliseconds As Integer = -1) As TCPCommunicationBase
        Dim receivedMessage As TCPCommunicationBase = Nothing
        If maximumMilliseconds < 0 Then maximumMilliseconds = NB_MS_FOR_A_LOOP * NB_LOOP_FOR_NEXT_ATTEMPT * MAX_MESSAGE_ATTEMPTS
        Dim nbTries As Integer = 1
        Dim maxTries As Integer = 0
        Dim lastLoopTime As Integer = 0
        If maximumMilliseconds > 0 Then
            maxTries = Math.DivRem(maximumMilliseconds, ATTEMPT_TIME_MS, lastLoopTime)
            If lastLoopTime <> 0 Then maxTries += 1
            If maxTries > MAX_MESSAGE_ATTEMPTS Then maxTries = MAX_MESSAGE_ATTEMPTS
        End If

        Dim producer As TCPClient = task.getClient()
        While receivedMessage Is Nothing AndAlso (maximumMilliseconds = 0 OrElse nbTries <= maxTries)
            Dim guid As String = task.guid

            If tcpWaiter IsNot Nothing Then tcpWaiter.onOneLoopDone(0, nbTries, maxTries, True)

            initWaiting(producer, guid, commTypeToWait)

            task.execute()

            receivedMessage = wait(tcpWaiter, producer, guid, nbTries, maxTries, commTypeToWait, If(nbTries = maxTries, lastLoopTime, ATTEMPT_TIME_MS))

            If receivedMessage Is Nothing Then
                nbTries += 1
                task.regenerateGuid()
                SyncLock waitingDataLock
                    guidChain.Add(guid, task.guid) 'Link old and new guid
                End SyncLock
            End If
        End While

        Return receivedMessage
    End Function

    Private Sub initWaiting(ByVal producer As IDataConsumable(Of DataTCP), ByVal guid As String, ByVal commTypeToWait As Type)
        Dim guidWaitingData As Dictionary(Of Type, TCPCommunicationBase)

        SyncLock waitingDataLock
            If Me.waitingData.ContainsKey(guid) Then
                guidWaitingData = Me.waitingData(guid)
            Else
                guidWaitingData = New Dictionary(Of Type, TCPCommunicationBase)()
                Me.waitingData.Add(guid, guidWaitingData)
            End If
            If guidWaitingData.ContainsKey(commTypeToWait) Then Throw New Exception("A commType of this Type is already waiting : " & commTypeToWait.FullName)

            guidWaitingData.Add(commTypeToWait, Nothing)
            If producers.ContainsKey(producer) = False Then
                producers.Add(producer, 1)
                producer.addConsumer(Me)
            Else
                producers(producer) += 1
            End If
        End SyncLock
    End Sub

    Private Function wait(ByRef tcpWaiter As ITcpWaiter, ByRef producer As IDataConsumable(Of DataTCP), ByRef guid As String, ByRef nbTries As Integer, ByRef maxTries As Integer, ByRef commTypeToWait As Type, Optional ByVal maximumMilliseconds As Integer = -1) As TCPCommunicationBase
        Dim waitingData As TCPCommunicationBase = Nothing

        Dim nbTime As Integer = 0
        While waitingData Is Nothing AndAlso (maximumMilliseconds = 0 OrElse nbTime < maximumMilliseconds)
            'REM TEMP Application.DoEvents()
            Threading.Thread.Sleep(NB_MS_FOR_A_LOOP)
            nbTime += NB_MS_FOR_A_LOOP
            SyncLock waitingDataLock
                waitingData = Me.waitingData(guid)(commTypeToWait)
            End SyncLock

            If tcpWaiter IsNot Nothing Then tcpWaiter.onOneLoopDone(nbTime, nbTries, maxTries, False)
        End While

        Dim guidWaitingData As Dictionary(Of Type, TCPCommunicationBase)
        SyncLock waitingDataLock
            guidWaitingData = Me.waitingData(guid)
            guidWaitingData.Remove(commTypeToWait)
            If guidWaitingData.Count = 0 Then Me.waitingData.Remove(guid)

            producers(producer) -= 1
            If producers(producer) = 0 Then producers.Remove(producer)
        End SyncLock

        Return waitingData
    End Function

    Private Sub unlockWaitingData(ByVal comm As TCPCommunicationBase)
        Dim guidWaitingData As Dictionary(Of Type, TCPCommunicationBase)
        SyncLock waitingDataLock
            Dim guidToUse As String = comm.guid
            If Not waitingData.ContainsKey(guidToUse) Then
                If Not guidChain.ContainsKey(guidToUse) Then
                    Exit Sub
                End If

                While guidChain.ContainsKey(guidToUse)
                    Dim guidToDelete As String = guidToUse
                    guidToUse = guidChain(guidToUse)
                    guidChain.Remove(guidToDelete)
                End While
            End If

            guidWaitingData = Me.waitingData(guidToUse)

            For Each curType As Type In guidWaitingData.Keys
                If comm.GetType.FullName = curType.FullName Then

                    guidWaitingData(curType) = comm

                    Exit For
                End If
            Next
        End SyncLock
    End Sub

    Public Sub dataConsume(ByVal dataReceived As DataTCP) Implements IDataConsumer(Of DataTCP).dataConsume
        Dim receivedCommand As TCPCommands.TCPCommand = TCPCommands.TCPCommand.createTCPCommand(dataReceived.client, dataReceived)
        Dim receivedAnswer As TCPAnswers.TCPAnswer = TCPAnswers.TCPAnswer.createTCPAnswer(dataReceived.client, dataReceived)

        If receivedCommand IsNot Nothing Then
            unlockWaitingData(receivedCommand)
        ElseIf receivedAnswer IsNot Nothing Then
            unlockWaitingData(receivedAnswer)
        End If
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataTCP).priority
        Get
            Return 0
        End Get
    End Property

    Public Function CompareTo(ByVal other As IDataConsumer(Of DataTCP)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataTCP)).CompareTo
        Return other.priority.CompareTo(Me.priority)
    End Function
End Class
