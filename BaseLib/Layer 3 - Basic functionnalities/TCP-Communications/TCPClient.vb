Imports System.Text
Imports CI.Base.TCPCommands
Imports CI.Base.TCPAnswers

' The UserConnection class encapsulates the functionality of a TcpClient connection with streaming for a single user.
Public Class TCPClient
    Inherits ManagerBase(Of TCPClient)
    Implements IDataConsumable(Of DataTCP)

#Region "Definitions"
    Private Const READ_BUFFER_SIZE As Integer = 255
    Private Const SEND_BUFFER_SIZE As Integer = 255
    Private Const DEFAULT_MAX_RECONNECTION_TRIALS As Integer = 100

    Private Shared mySelf As TCPClient

    Private isClientForHost As Boolean = False
    Private notCompletedCommands As New ArrayList
    Private completedCommands As New Generic.List(Of String)
    Private takeFirstCommingNumber As Boolean = True
    Private connected As Boolean = False
    Private isConnecting As Boolean = False
    Private lostConnectionRunning As Boolean = False
    Private pingingTimer As Timers.Timer
    Private pingerTimer As Timers.Timer
    Private pingingTime As Long = 0
    Private lostConnectionSync As New Object
    Private nbMessagesSent As Integer = 0
    Private lastNoMessageReceived As Integer = 0
    Private nbMessagesReceivedErrors As Integer = 0
    Private _DoReconnection As Boolean = True
    Private _pingInterval As Integer = 30
    Private _maxReconnectionTrials As Integer = DEFAULT_MAX_RECONNECTION_TRIALS

    Private reconnectTrials As Integer = 0
    Private connectionTrials As Integer = 0
    Private disconnectionTrials As Integer = 0
    Private sendDataTrials As Integer = 0
    Private consumers As New System.Collections.Generic.List(Of IDataConsumer(Of DataTCP))
    Private Shared consumersLock As New Object
    Private client As Net.Sockets.TcpClient
    Private readBuffer(READ_BUFFER_SIZE) As Byte
    Private tempReadBuffer As New List(Of Byte)

    Private host_ip As String = "localhost" 'your Server IP 
    '//you could also use your server pc name (DNS server is required to ensure stability)
    Private _portNumber As Integer = 20500
    Private _acceptMultipleConnectionsByComputer As Boolean = False

    Private _usePing As Boolean = True

    Private isStreamOpened As Boolean = True
    Private strName As String
    Private readString As String = ""
#End Region

#Region "Constructors"
    ''' <summary>
    ''' Constructor used when using the class as a Singleton (For the client part)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub New()
        pingingTimer = New Timers.Timer(_pingInterval * 3000) 'Triple le temps pour la réponse (pong)
        pingerTimer = New Timers.Timer(_pingInterval * 1000) 'Temps pour l'envoi du ping
        pingingTimer.AutoReset = False
        pingerTimer.AutoReset = True
        AddHandler pingingTimer.Elapsed, AddressOf pingingTimeout
        AddHandler pingerTimer.Elapsed, AddressOf pinger

        'Set default name
        name = ""
    End Sub

    ''' <summary>
    ''' Constructor used by TCPHost to manage connected clients (For the server part)
    ''' </summary>
    ''' <param name="client"></param>
    ''' <remarks>Set usePing property to false by default</remarks>
    Public Sub New(ByVal client As Net.Sockets.TcpClient, ByVal portNumber As Integer)
        _usePing = False
        _DoReconnection = False
        connected = True
        isClientForHost = True
        _portNumber = portNumber

        Me.client = client

        startReading()
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property portNumber() As Integer
        Get
            Return _portNumber
        End Get
    End Property

    ' The Name property uniquely identifies the user connection.
    Public Property name() As String
        Get
            Return strName
        End Get
        Set(ByVal Value As String)
            If String.IsNullOrEmpty(Value) Then
                Value = Environment.MachineName & If(_acceptMultipleConnectionsByComputer = False, "", "-" & Process.GetCurrentProcess.Id)
            End If

            strName = Value
        End Set
    End Property

    Public Property usePing() As Boolean
        Get
            Return _usePing
        End Get
        Set(ByVal value As Boolean)
            _usePing = value
        End Set
    End Property

    Public Property acceptMultipleConnectionsByComputer() As Boolean
        Get
            Return _acceptMultipleConnectionsByComputer
        End Get
        Set(ByVal value As Boolean)
            _acceptMultipleConnectionsByComputer = value
            If isClientForHost = False Then name = ""
        End Set
    End Property

    Public Property pingInterval() As Integer
        Get
            Return _pingInterval
        End Get
        Set(ByVal value As Integer)
            _pingInterval = value
            pingingTimer.Interval = _pingInterval * 3000
            pingerTimer.Interval = _pingInterval * 1000
        End Set
    End Property

    Public Property maxReconnectionTrials() As Integer
        Get
            Return _maxReconnectionTrials
        End Get
        Set(ByVal value As Integer)
            _maxReconnectionTrials = value
        End Set
    End Property

    Public ReadOnly Property isConnected() As Boolean
        Get
            Return connected AndAlso (client IsNot Nothing AndAlso client.Connected)
        End Get
    End Property

    Public Property doReconnection() As Boolean
        Get
            Return _DoReconnection
        End Get
        Set(ByVal value As Boolean)
            _DoReconnection = value
        End Set
    End Property

    Public Property currentNbMessagesSent() As Integer
        Get
            Return Me.nbMessagesSent
        End Get
        Set(ByVal value As Integer)
            Me.nbMessagesSent = value
        End Set
    End Property

    Public Property currentNbMessagesReveicedErrors() As Integer
        Get
            Return Me.nbMessagesReceivedErrors
        End Get
        Set(ByVal value As Integer)
            Me.nbMessagesReceivedErrors = value
        End Set
    End Property

    Public Property currentLastNoMessageReceived() As Integer
        Get
            Return Me.lastNoMessageReceived
        End Get
        Set(ByVal value As Integer)
            Me.lastNoMessageReceived = value
        End Set
    End Property
#End Region

    Private Sub connectToHost()
        connectToHost(host_ip, _portNumber)
    End Sub

    Private Sub startReading()
        client.SendBufferSize = READ_BUFFER_SIZE
        client.ReceiveBufferSize = READ_BUFFER_SIZE

        ' This starts the asynchronous read thread.  The data will be saved into readBuffer.
        Me.client.GetStream.BeginRead(readBuffer, 0, READ_BUFFER_SIZE, AddressOf streamReceiver, Nothing)

        connectionTrials = 0
        If usePing Then
            pingerTimer.Start()
            pingingTimer.Start()
            pingingTime = Date.Now.AddMilliseconds(pingingTimer.Interval).Ticks
        End If
    End Sub

    Public Sub connectToHost(ByVal server As String, ByVal port As Integer)
        host_ip = server
        _portNumber = port

        connected = False
        isConnecting = True
        Dim exception As Exception = Nothing
        Try
            client = New Net.Sockets.TcpClient(host_ip, _portNumber)

            startReading()

            Dim connectCommand As New TCPCommands.Connect(Me, name)
            Dim isJoined As TCPAnswers.Join = TCPHelper.getInstance().sendWaitCommunication(Nothing, connectCommand, GetType(TCPAnswers.Join))

            If isJoined Is Nothing Then
                exception = New TCPConnectionFailedException("JOIN answer not received")
                External.current.addErrorLog(exception)
            ElseIf Not isJoined.isJoined Then
                exception = New TCPConnectionRefusedException()
            Else
                connected = True
            End If
        Catch ex As Exception
            External.current.addErrorLog(exception)
            connectionTrials += 1
            exception = New TCPConnectionFailedException(ex)
        End Try

        isConnecting = False

        If exception IsNot Nothing Then
            Throw exception
        End If
    End Sub

    Private Sub reconnect(Optional ByVal skipLostConnectionVerifs As Boolean = False)
        If isClientForHost Then
            External.current.setStatusText("La connexion avec " & Me.name & " a été perdue", False)
            TCPHost.getInstance.removeClient(Me)
            Exit Sub
        End If

        If _DoReconnection = False Then Exit Sub
        If skipLostConnectionVerifs = False AndAlso lostConnectionRunning Then Exit Sub
        If skipLostConnectionVerifs = False AndAlso System.Threading.Monitor.TryEnter(lostConnectionSync) = False Then Exit Sub

        lostConnectionRunning = True

        'Wait a little before trying to reconnect
        Threading.Thread.Sleep(1500)

        Try
            Dim shallRetryAgain As Boolean = Me.disconnect() = False
            If Not shallRetryAgain Then
                Try
                    Me.connectToHost()
                Catch ex As Exception
                    shallRetryAgain = True
                End Try
            End If

            If shallRetryAgain Then 'Was not able to reconnect to server
                reconnectTrials += 1
                External.current.setStatusText("Connexion au serveur de Clinica perdue. Tentative de reconnexion #" & reconnectTrials & " en cours...", False)
                If reconnectTrials > _maxReconnectionTrials Then
                    Dim message As String = "La connexion avec le serveur de Clinica a été perdue et plus de " & _maxReconnectionTrials & " tentatives de reconnexion ont échouées. Veuillez enregistrer vos données, fermer le logiciel et contactez votre administrateur (ou redémarrer votre serveur)."
                    External.current.setStatusText(message, True)
                Else
                    reconnect(True)
                End If
            Else
                If connected Then sendReconnectCommand()
                Threading.Thread.Sleep(100)
                connected = True
                Me.disconnect()
                Threading.Thread.Sleep(100)
                Me.connectToHost()

                sendReconnectCommand()

                'Successful reconnection
                reconnectTrials = 0
                PluginTasksManager.getInstance.clearTasks()
                External.current.refreshTasks()
                If PluginTasksManager.getInstance.isSubscribed Then
                    Dim subscribeCommand As New TCPCommands.SubscribeTasks(Me)
                    subscribeCommand.execute()
                    PluginTasksManager.getInstance.populateTasks()
                    External.current.refreshTasks()
                End If
                External.current.setStatusText("Reconnexion au serveur de Clinica réussie", False)
            End If
        Catch ex As Exception
            'Not able to reconnect
        Finally
            lostConnectionRunning = False
            If skipLostConnectionVerifs = False Then
                System.Threading.Monitor.Exit(lostConnectionSync)
            End If
        End Try
    End Sub

    Private Sub sendReconnectCommand()
        Dim reconnectCommand As New TCPCommands.Reconnect(Me, name)
        reconnectCommand.execute()
    End Sub

    Public Function disconnect() As Boolean
        If isConnected = False Then Return True

        Dim sent As Boolean = False
        Try
            Dim disconnectCommand As New TCPCommands.Disconnect(Me, name)
            disconnectCommand.execute()

            'REM With this line.. reconnection are looping, but without, the connection is not properly resetup.
            'Not sure this is still true, because the line is active.. shall test out.
            client.GetStream.Close(100)
            sent = True
            If usePing Then
                pingingTime = Date.Now.AddDays(1).Ticks
                disconnectionTrials = 0
                pingerTimer.Stop()
                pingingTimer.Stop()
            End If

            connected = False
        Catch ex As Exception
            disconnectionTrials += 1
            reconnect()

            External.current.addErrorLog(ex)
        End Try

        Return sent
    End Function

    ' This subroutine uses a StreamWriter to send a message to the user.
    Public Sub sendData(ByVal data As String, Optional ByVal sendMsgNo As Boolean = True)
        If client Is Nothing Then Exit Sub

        Dim noToSend As Integer = 0

        'Transform data to fit format
        If data.EndsWith(vbCrLf) = True Then data = data.Substring(0, data.Length - vbCrLf.Length)
        data = noToSend & DataTCP.PARAMS_SEPARATOR & data

        'Compress data if too big
        Dim encoding As New System.Text.UnicodeEncoding()
        If data.Length > 1024 Then
            data = compressString(data)
        End If

        'Add ending char
        data &= vbCrLf

        Dim encodedString() As Byte = encoding.GetBytes(data)

        Try
            ' Synclock ensure that no other threads try to use the stream at the same time.
            SyncLock client.GetStream
                If sendMsgNo Then
                    nbMessagesSent += 1
                    noToSend = nbMessagesSent
                End If

                For i As Integer = 0 To encodedString.GetUpperBound(0) Step SEND_BUFFER_SIZE
                    Dim max As Integer = SEND_BUFFER_SIZE
                    If (i + SEND_BUFFER_SIZE) > encodedString.GetUpperBound(0) Then max = encodedString.Length - i
                    client.GetStream.Write(encodedString, i, max)
                    client.GetStream.Flush()
                Next
            End SyncLock
        Catch ex As Exception
            sendDataTrials += 1
            reconnect()

            External.current.addErrorLog(ex)
        End Try

        If isClientForHost Then TCPHost.getInstance.raiseMessageExchanged(data, Me, False)
    End Sub

#Region "Handling received data from stream"
    Private Sub manageReceivedData(ByVal bytesRead As Integer)
        Dim tempBuffer() As Byte
        ReDim tempBuffer(bytesRead - 1)
        Array.Copy(readBuffer, tempBuffer, bytesRead)
        tempReadBuffer.AddRange(tempBuffer)

        Dim strMessage As String

        readString = Encoding.Unicode.GetString(tempReadBuffer.ToArray)

        Dim enterPos As Integer = readString.IndexOf(vbCrLf)
        Dim originalReadString As String = readString
        While enterPos <> -1
            originalReadString = readString 'TO REMOVE WHEN ERROR OF PARTIAL MESSAGE IS FIXED

            strMessage = readString.Substring(0, enterPos)
            Dim nbBytesRead As Integer = Encoding.Unicode.GetByteCount(strMessage & vbCrLf)
            tempReadBuffer.RemoveRange(0, nbBytesRead) 'Remove from buffer used bytes
            readString = readString.Substring(enterPos + 2) '+2 to remove the enter char

            Try
                strMessage = decompressString(strMessage)

                'Manage line
                onLineReceived(strMessage)
            Catch ex As Exception
                External.current.addErrorLog(New Exception("Error with received message : [" & strMessage & "]" & vbCrLf & vbCrLf & vbCrLf & "originalReadString=[" & originalReadString & "]" & vbCrLf & "enterPos=" & enterPos, ex))
            End Try

            enterPos = readString.IndexOf(vbCrLf)
        End While
    End Sub

    Private Sub processHostMessage(ByVal arg As Object)
        Try
            Dim receivedCommand As TCPCommand = arg
            receivedCommand.execute()
        Catch ex As Exception
            External.propagateErrorLog(ex)
        End Try
    End Sub

    Private Sub onLineReceived(ByVal strMessage As String)
        'Try to process command/answer via existing ones.. 
        Dim receivedData As New DataTCP(Me, strMessage)
        If isClientForHost Then
            TCPHost.getInstance.raiseMessageExchanged(strMessage, Me, True)

            Dim receivedCommand As TCPCommand = TCPCommand.createTCPCommand(Me, receivedData)
            If receivedCommand IsNot Nothing Then
                Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf processHostMessage), receivedCommand)
            End If
        Else
            Dim receivedAnswer As TCPAnswer = TCPAnswer.createTCPAnswer(Me, receivedData)
            If receivedAnswer IsNot Nothing Then
                receivedAnswer.execute()
            End If
        End If

        'Send to consumers
        For Each curConsumer As IDataConsumer(Of DataTCP) In consumers.ToArray()
            curConsumer.dataConsume(New DataTCP(Me, strMessage))
        Next
    End Sub

    ' This is the callback function for TcpClient.GetStream.Begin. It begins an 
    ' asynchronous read from a stream.
    Private Sub streamReceiver(ByVal ar As IAsyncResult)
        If Me.isConnected = False AndAlso isConnecting = False Then Exit Sub

        Dim bytesRead As Integer

        Try
            ' Ensure that no other threads try to use the stream at the same time.
            SyncLock client.GetStream
                ' Finish asynchronous read into readBuffer and get number of bytes read.
                bytesRead = client.GetStream.EndRead(ar)
            End SyncLock

            If bytesRead < 1 Then
                reconnect()
                Exit Sub    'If no bytes were read server has close.
            Else
                If usePing Then
                    pingingTimer.Stop()
                    pingingTimer.Start()
                    pingingTime = Date.Now.AddMilliseconds(pingingTimer.Interval).Ticks
                End If

                ' Convert the byte array the message was saved into, minus two for the Chr(13) and Chr(10)
                manageReceivedData(bytesRead)

                If isStreamOpened Then
                    ' Ensure that no other threads try to use the stream at the same time.
                    SyncLock client.GetStream
                        ' Start a new asynchronous read into readBuffer.
                        client.GetStream.BeginRead(readBuffer, 0, READ_BUFFER_SIZE, AddressOf streamReceiver, Nothing)
                    End SyncLock
                End If
            End If

        Catch ex As Exception
            reconnect()

            External.current.addErrorLog(ex)
        End Try
    End Sub
#End Region

    Public Sub close()
        If Me.client.Connected Then
            isStreamOpened = False
            Me.client.GetStream.Close()
            Me.client.Close()
        End If
    End Sub

    Protected Overrides Sub finalize()
        MyBase.Finalize()
    End Sub

#Region "Ping methods"
    Private Sub pingingTimeout(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs)
        If pingingTime <= e.SignalTime.Ticks Then
            reconnect()
        End If
    End Sub

    Private Sub pinger(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs)
        Try
            Dim ping As New TCPCommands.Ping(Me)
            ping.execute()

            If pingingTimer.Enabled = False Then pingingTimer.Start()
        Catch ex As Exception
            'Impossible de repartir le pingingTimer, Next time
        End Try
    End Sub
#End Region

    Public Sub addConsumer(ByVal newConsumer As IDataConsumer(Of DataTCP)) Implements IDataConsumable(Of DataTCP).addConsumer
        consumers.Add(newConsumer)
        consumers.Sort()
    End Sub

    Public Sub removeConsumer(ByVal curConsumer As IDataConsumer(Of DataTCP)) Implements IDataConsumable(Of DataTCP).removeConsumer
        consumers.Remove(curConsumer)
    End Sub
End Class
