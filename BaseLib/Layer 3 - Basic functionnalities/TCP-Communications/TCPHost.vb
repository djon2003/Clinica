Imports System.Net.Sockets

Public Class TCPHost
    Inherits ManagerBase(Of TCPHost)
    Implements IDataConsumable(Of DataTCP), IDataConsumer(Of DataTCP)

    Private Const MAX_SEND_ERROR_BEFORE_REMOVAL As Byte = 3
    Private Const DEFAULT_PORT_NUMBER As Integer = 20500

    Private notCompletedCommands As New Hashtable
    Private consumers As New System.Collections.Generic.List(Of IDataConsumer(Of DataTCP))
    Private nbMessagesSent As Integer = 0
    'TODO: COUNTERS - Shall it be kept?: Private nbMessagesReceived As New Dictionary(Of Integer, Dictionary(Of String, Integer))
    'TODO: COUNTERS - Shall it be kept?: Private nbMessagesReceivedErrors As New Dictionary(Of Integer, Dictionary(Of String, Integer))
    Private _clients As New Dictionary(Of Integer, Dictionary(Of String, TCPClient))
    Private _clientsNotSentCounter As New Dictionary(Of Integer, Dictionary(Of String, Integer))
    Private _isListening As Boolean = False
    Private _portNumbers As String = DEFAULT_PORT_NUMBER

    Private connectionDisallowed As Boolean = False
    Private clientsListChangeLock As New Object
    Private waitingDataLock As New Object
    Private waitingData As New Dictionary(Of Type, TCPCommunicationBase)


    Protected Sub New()

    End Sub

    Public Event stoppedListenning()
    Public Event clientConnected(ByVal client As TCPClient)
    Public Event clientDisconnected(ByVal client As TCPClient)
    Public Event messageExchanged(ByVal message As String, ByVal sender As TCPClient, ByVal isReceived As Boolean)

#Region "Properties"
    Public ReadOnly Property clients() As List(Of TCPClient)
        Get
            Dim curClients As New List(Of TCPClient)
            For Each port As Integer In _clients.Keys
                curClients.AddRange(_clients(port).Values)
            Next

            Return curClients
        End Get
    End Property


    Public Property portNumbers() As String
        Get
            Return _portNumbers
        End Get
        Set(ByVal value As String)
            _portNumbers = value
        End Set
    End Property

    Public ReadOnly Property isListening() As Boolean
        Get
            Return _isListening
        End Get
    End Property
#End Region

    Public Function countClients() As Integer
        Return Me.clients.Count
    End Function

    Public Sub closeAllClients()
        For Each portNumber As Integer In Me._clients.Keys
            For Each curEntry As String In Me._clients(portNumber).Keys
                _clients(portNumber)(curEntry).close()
            Next
        Next
    End Sub

    Public Sub startListening()
        'TODO: COUNTERS - Shall it be kept?: loadCounters()

        _clients = New Dictionary(Of Integer, Dictionary(Of String, TCPClient))
        _clientsNotSentCounter = New Dictionary(Of Integer, Dictionary(Of String, Integer))
        'TODO: COUNTERS - Shall it be kept?: nbMessagesReceived = New Dictionary(Of Integer, Dictionary(Of String, Integer))
        'TODO: COUNTERS - Shall it be kept?: nbMessagesReceivedErrors = New Dictionary(Of Integer, Dictionary(Of String, Integer))

        Dim ports() As String = _portNumbers.Split(New Char() {","})
        Me._isListening = True
        For i As Integer = 0 To ports.Length - 1
            _clients.Add(ports(i), New Dictionary(Of String, TCPClient))
            _clientsNotSentCounter.Add(ports(i), New Dictionary(Of String, Integer))
            'TODO: COUNTERS - Shall it be kept?: nbMessagesReceived.Add(ports(i), New Dictionary(Of String, Integer))
            'TODO: COUNTERS - Shall it be kept?: nbMessagesReceivedErrors.Add(ports(i), New Dictionary(Of String, Integer))

            Dim listenerThread As New Threading.Thread(AddressOf doListen)  'when a message is received goto to sub (subroutine) DoListen
            listenerThread.Start(ports(i))
        Next
    End Sub

    Public Sub stopListening()
        Me._isListening = False
    End Sub

    'TODO: COUNTERS - Shall it be kept?: 
    'Public Sub loadCounters()
    '    nbMessagesSent = 0
    '    Me.nbMessagesReceived.Clear()
    '    Me.nbMessagesReceivedErrors.Clear()

    '    Dim path As String = My.Application.Info.DirectoryPath & IIf(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & "Postes"
    '    IO.Directory.CreateDirectory(path)

    '    Dim curLines() As String = {""}
    '    Dim counterFile As String = path & "\serveur"
    '    If IO.File.Exists(counterFile) Then curLines = IO.File.ReadAllLines(counterFile)
    '    If curLines.Length <> 0 AndAlso curLines(curLines.GetUpperBound(0)) <> "" Then
    '        Dim sCurLines() As String = curLines(curLines.GetUpperBound(0)).Split(New Char() {"|"})
    '        If sCurLines(0) = DateFormat.getTextDate(Date.Today) Then
    '            nbMessagesSent = sCurLines(1)
    '        End If
    '    End If

    '    Dim postesFiles() As String = IO.Directory.GetFiles(path)
    '    For Each curFile As String In postesFiles
    '        curLines = New String() {""}
    '        If curFile.EndsWith("serveur") = False Then
    '            Dim file As New IO.FileInfo(curFile)
    '            curLines = IO.File.ReadAllLines(curFile)
    '            If curLines.Length <> 0 AndAlso curLines(curLines.GetUpperBound(0)) <> "" Then
    '                Dim sCurLines() As String = curLines(curLines.GetUpperBound(0)).Split(New Char() {"|"})
    '                If sCurLines(0) = DateFormat.getTextDate(Date.Today) Then
    '                    If Me.nbMessagesReceived.ContainsKey(file.Name) = False Then Me.nbMessagesReceived.Add(file.Name, 0)
    '                    If Me.nbMessagesReceivedErrors.ContainsKey(file.Name) = False Then Me.nbMessagesReceivedErrors.Add(file.Name, 0)

    '                    Me.nbMessagesReceived(file.Name) = sCurLines(1)
    '                    Me.nbMessagesReceivedErrors(file.Name) = sCurLines(2)
    '                End If
    '            End If
    '        End If
    '    Next
    'End Sub

    'Public Sub saveCounters(ByVal curDate As Date)
    '    Dim path As String = My.Application.Info.DirectoryPath & IIf(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & "Postes"
    '    IO.Directory.CreateDirectory(path)

    '    Dim curLines() As String = {""}
    '    Dim counterFile As String = path & "\serveur"
    '    If IO.File.Exists(counterFile) Then curLines = IO.File.ReadAllLines(counterFile)
    '    If curLines.Length <> 0 AndAlso (curLines(0) = "" OrElse curLines(curLines.GetUpperBound(0)).StartsWith(DateFormat.getTextDate(curDate) & "|")) Then
    '        curLines(curLines.GetUpperBound(0)) = DateFormat.getTextDate(curDate) & "|" & nbMessagesSent
    '    Else
    '        ReDim Preserve curLines(curLines.Length)
    '        curLines(curLines.GetUpperBound(0)) = DateFormat.getTextDate(curDate) & "|" & nbMessagesSent
    '    End If
    '    IO.File.WriteAllLines(counterFile, curLines)

    '    Dim curNbMessagesReceived As Hashtable = Me.nbMessagesReceived.Clone()
    '    Dim curNbMessagesReceivedErrors As Hashtable = Me.nbMessagesReceivedErrors.Clone()

    '    For Each curSender As String In curNbMessagesReceived.Keys
    '        Dim nbReceived As Integer = 0
    '        Dim nbErrors As Integer = 0
    '        If curNbMessagesReceived.ContainsKey(curSender) Then nbReceived = curNbMessagesReceived(curSender)
    '        If curNbMessagesReceivedErrors.ContainsKey(curSender) Then nbErrors = curNbMessagesReceivedErrors(curSender)

    '        curLines = New String() {""}
    '        counterFile = path & "\" & curSender
    '        If IO.File.Exists(counterFile) Then curLines = IO.File.ReadAllLines(counterFile)
    '        If curLines.Length <> 0 AndAlso (curLines(0) = "" OrElse curLines(curLines.GetUpperBound(0)).StartsWith(DateFormat.getTextDate(curDate) & "|")) Then
    '            curLines(curLines.GetUpperBound(0)) = DateFormat.getTextDate(curDate) & "|" & nbReceived & "|" & nbErrors
    '        Else
    '            ReDim Preserve curLines(curLines.Length)
    '            curLines(curLines.GetUpperBound(0)) = DateFormat.getTextDate(curDate) & "|" & nbReceived & "|" & nbErrors
    '        End If
    '        IO.File.WriteAllLines(counterFile, curLines)
    '    Next
    'End Sub


    Public Function addClient(ByVal pcWinName As String, ByVal sender As TCPClient) As Boolean
        Dim portNumber As Integer = sender.portNumber
        pcWinName = pcWinName.Replace(vbCrLf, "").Replace(vbCr, "").Replace(vbLf, "") REM & sender.GetHashCode
        Try
            SyncLock clientsListChangeLock
                If _clients(portNumber).ContainsKey(pcWinName) Then
                    sender.name = pcWinName
                    _clients(portNumber)(pcWinName) = sender
                    Return True
                End If

                sender.name = pcWinName
                'TODO: UPDATE - Reactivate when bugs fixed
                'If connectionDisallowed Then Return False

                _clients(portNumber).Add(pcWinName, sender)
                _clientsNotSentCounter(portNumber).Add(pcWinName, 0)
            End SyncLock

            'TODO: COUNTERS - Shall it be kept?: If Me.nbMessagesReceived(portNumber).ContainsKey(pcWinName) = False Then Me.nbMessagesReceived(portNumber).Add(pcWinName, 0)
            'TODO: COUNTERS - Shall it be kept?: If Me.nbMessagesReceivedErrors(portNumber).ContainsKey(pcWinName) = False Then Me.nbMessagesReceivedErrors(portNumber).Add(pcWinName, 0)

            RaiseEvent clientConnected(sender)

            'End If
        Catch ex As Exception
            External.current.addErrorLog(ex)
        End Try

        Return True
    End Function

    Public Sub allowConnection()
        SyncLock clientsListChangeLock
            connectionDisallowed = False
        End SyncLock
    End Sub

    Public Function disallowConnection() As Boolean
        SyncLock clientsListChangeLock
            If _clients.Count <> 1 Then Return False

            connectionDisallowed = True
        End SyncLock

        Return True
    End Function

    Friend Sub raiseMessageExchanged(ByVal message As String, ByVal sender As TCPClient, ByVal isReceived As Boolean)
        RaiseEvent messageExchanged(message, sender, isReceived)
    End Sub

    Private Function sendToClient(ByVal portNumber As Integer, ByVal pcName As String, ByVal strMessage As String, Optional ByVal didOnce As Boolean = False) As Boolean
        Try
            Dim client As TCPClient = _clients(portNumber)(pcName)
            client.sendData(strMessage)

            SyncLock clientsListChangeLock
                _clientsNotSentCounter(portNumber)(pcName) = 0
            End SyncLock
        Catch ex As Exception
            If didOnce Then
                External.current.addErrorLog(New Exception("Not able to send one", ex))
                Return False
            End If

            Return sendToClient(portNumber, pcName, strMessage, True)
        End Try

        Return True
    End Function

    ''' <summary>
    ''' This subroutine sends a message to all attached clients
    ''' </summary>
    ''' <param name="sendToAllCmd"></param>
    ''' <remarks></remarks>
    Public Function sendToAllClients(ByVal sendToAllCmd As TCPCommands.SendToAll) As Boolean
        Dim messageToSend As String = sendToAllCmd.getSendingData()
        Dim portNumber As Integer = sendToAllCmd.client.portNumber

        Return sendToAllClients(portNumber, messageToSend)
    End Function

    ''' <summary>
    ''' This subroutine sends a message to all attached clients
    ''' </summary>
    ''' <param name="messageToSend"></param>
    ''' <remarks>This method should not be used !!! Exists for sending raw message</remarks>
    Public Function sendToAllClients(ByVal portNumber As Integer, ByVal messageToSend As String) As Boolean
        If portNumber = 0 Then
            Dim isAllSent As Boolean = True
            For Each curPortNumber As Integer In _clients.Keys
                isAllSent = isAllSent AndAlso sendToAllClients(curPortNumber, messageToSend)
            Next
            Return isAllSent
        End If

        Try
            nbMessagesSent += 1
            ' All entries in the clients Hashtable are UserConnection so it is possible to assign it safely.
            Dim removingClients As New Generic.List(Of String)
            Dim clientsKeys() As String
            ReDim clientsKeys(_clients(portNumber).Keys.Count - 1)
            _clients(portNumber).Keys.CopyTo(clientsKeys, 0)
            For Each pcName As String In clientsKeys
                If _clients(portNumber).ContainsKey(pcName) = False Then Continue For

                If sendToClient(portNumber, pcName, messageToSend) = False Then removingClients.Add(pcName)
            Next
            If removingClients.Count <> 0 Then
                SyncLock clientsListChangeLock
                    For Each pcName As String In removingClients
                        _clientsNotSentCounter(portNumber)(pcName) += 1

                        If _clientsNotSentCounter(portNumber)(pcName) > MAX_SEND_ERROR_BEFORE_REMOVAL Then
                            If _clients(portNumber).ContainsKey(pcName) Then _clients(portNumber).Remove(pcName)
                            If _clientsNotSentCounter(portNumber).ContainsKey(pcName) Then _clientsNotSentCounter(portNumber).Remove(pcName)
                        End If
                    Next
                End SyncLock
            End If
        Catch ex As Exception
            External.current.addErrorLog(New Exception("Not able to send all : " & messageToSend, ex))
            Return False
        End Try
        Return True
    End Function

    Public Sub removeClient(ByVal sender As TCPClient)
        Dim pcWinName As String = sender.name
        Dim portNumber As Integer = sender.portNumber
        LocksManager.getInstance().clearLocks(pcWinName)

        pcWinName = pcWinName.Replace(vbCrLf, "").Replace(vbCr, "").Replace(vbLf, "")  REM & sender.GetHashCode
        Dim disconectedClient As TCPClient = Nothing

        SyncLock clientsListChangeLock
            If _clients(portNumber).ContainsKey(pcWinName) Then
                disconectedClient = _clients(portNumber)(pcWinName)
                disconectedClient.close()
                _clients(portNumber).Remove(pcWinName)
                _clientsNotSentCounter(portNumber).Remove(pcWinName)
                'TODO: COUNTERS - Shall it be kept?: nbMessagesReceived(portNumber).Remove(pcWinName)
                'TODO: COUNTERS - Shall it be kept?: nbMessagesReceivedErrors(portNumber).Remove(pcWinName)
            End If
        End SyncLock

        If disconectedClient IsNot Nothing Then RaiseEvent clientDisconnected(disconectedClient)

    End Sub

    ''' <summary>
    ''' This subroutine is used as a background listener thread to 
    ''' allow reading incoming messages without lagging the user interface.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doListen(ByVal portNumber As Integer)
        Try
            ' Listen for new connections.
            Dim listener As New TcpListener(System.Net.IPAddress.Any, portNumber)
            listener.Start()

            While _isListening
                Try
                    'Create a new user connection using TcpClient returned by TcpListener.AcceptTcpClient()
                    Dim client As New TCPClient(listener.AcceptTcpClient, portNumber)

                    'Create an event handler to allow the UserConnection to communicate with the window.
                    client.addConsumer(Me)
                Catch ex As Exception
                    External.current.addErrorLog(New Exception("IN1-DoListen()", ex))
                End Try
            End While

            listener.Stop()
        Catch ex As System.Net.Sockets.SocketException
            If ex.ErrorCode = 10048 Then 'Port already in use
                MessageBox.Show("Le port " & portNumber & " est déjà en cours d'utilisation. Une autre application l'utilise déjà ou une instance du Serveur de Clinica avec ce port est déjà ouvert.", "Erreur de démarrage", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                External.current.addErrorLog(New Exception("IN2-DoListen()", ex))
            End If
        Catch ex As Exception
            External.current.addErrorLog(New Exception("IN2-DoListen()", ex))
        End Try

        _isListening = False 'Ensure isListenning state is false (in case of bug)
        RaiseEvent stoppedListenning()
    End Sub

    Public Sub addConsumer(ByVal newConsumer As IDataConsumer(Of DataTCP)) Implements IDataConsumable(Of DataTCP).addConsumer
        consumers.Add(newConsumer)
    End Sub

    Public Sub removeConsumer(ByVal curConsumer As IDataConsumer(Of DataTCP)) Implements IDataConsumable(Of DataTCP).removeConsumer
        consumers.Remove(curConsumer)
    End Sub

    ''' <summary>
    ''' Wait for a TCPCommand or a TCPAnswer.
    ''' </summary>
    ''' <param name="commType">TCPCommand/TCPAnswer derived class type</param>
    ''' <param name="maximumMilliseconds">Maximum milliseconds to wait the data. If set to minus one, it means an infinite time.</param>
    ''' <returns>A TCPCommand or a TCPAnswer</returns>
    ''' <remarks></remarks>
    Public Function waitForData(ByVal commType As Type, Optional ByVal maximumMilliseconds As Integer = -1) As TCPCommunicationBase
        Dim waitingData As TCPCommunicationBase = Nothing
        SyncLock waitingDataLock
            If Me.waitingData.ContainsKey(commType) Then Throw New Exception("A commType of this Type is already waiting")
            Me.waitingData.Add(commType, Nothing)
        End SyncLock

        Dim nbTime As Integer = 0
        While waitingData Is Nothing AndAlso (maximumMilliseconds = -1 OrElse nbTime < maximumMilliseconds)
            Application.DoEvents()
            Threading.Thread.Sleep(100)
            nbTime += 100
            SyncLock waitingDataLock
                waitingData = Me.waitingData(commType)
            End SyncLock
        End While

        Return waitingData
    End Function

    Private Sub unlockWaitingData(ByVal comm As TCPCommunicationBase)
        For Each curType As Type In waitingData.Keys
            If comm.GetType.FullName = curType.FullName Then
                SyncLock waitingDataLock
                    waitingData(curType) = comm
                End SyncLock
                Exit For
            End If
        Next
    End Sub

    Public Sub dataConsume(ByVal dataReceived As DataTCP) Implements IDataConsumer(Of DataTCP).dataConsume
        Dim receivedCommand As TCPCommands.TCPCommand = TCPCommands.TCPCommand.createTCPCommand(dataReceived.client, dataReceived)
        Dim receivedAnswer As TCPAnswers.TCPAnswer = TCPAnswers.TCPAnswer.createTCPAnswer(dataReceived.client, dataReceived)
        If receivedCommand Is Nothing AndAlso receivedAnswer Is Nothing Then
            'Relay to other clients, if it is not an internal command
            Dim subCommand As String = dataReceived.args(0)
            Dim subCommandArgs() As String = {}
            If dataReceived.args.Length > 1 Then
                ReDim subCommandArgs(dataReceived.args.Length - 2)
                dataReceived.args.CopyTo(subCommandArgs, 1)
            End If
            Dim sendToAllCmd As New TCPCommands.SendToAll(Nothing, subCommand, subCommandArgs)
            sendToAllCmd.execute()
        ElseIf receivedCommand IsNot Nothing Then
            unlockWaitingData(receivedCommand)
        ElseIf receivedAnswer IsNot Nothing Then
            unlockWaitingData(receivedAnswer)
        End If

        For Each consumer As IDataConsumer(Of DataTCP) In consumers
            consumer.dataConsume(dataReceived)
        Next

        'TODO: COUNTERS - Shall it be kept?: 
        'Try
        '    If nbMessagesReceived.ContainsKey(dataReceived.client.name) = False Then nbMessagesReceived.Add(dataReceived.client.name, 0)
        'Catch ex As Exception
        '    External.current.addErrorLog(ex)
        'End Try
        'If dataReceived.noMessage <> 0 Then
        '    If (nbMessagesReceived(dataReceived.client.name) + 1) < dataReceived.noMessage Then
        '        If nbMessagesReceivedErrors.ContainsKey(dataReceived.client.name) = False Then nbMessagesReceivedErrors.Add(dataReceived.client.name, 0)
        '        nbMessagesReceivedErrors(dataReceived.client.name) += dataReceived.noMessage - (nbMessagesReceived(dataReceived.client.name) + 1)
        '    End If
        '    nbMessagesReceived(dataReceived.client.name) = dataReceived.noMessage
        'End If
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
