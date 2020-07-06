Public Class InternalUpdatesManager
    Inherits ManagerBase(Of InternalUpdatesManager)
    Implements IDataConsumer(Of DataTCP), IDataConsumable(Of DataInternalUpdate)

#Region "Constructor"
    Protected Sub New()
        isHost = TCPHost.getInstance().isListening

        If Not isHost Then
            TCPClient.getInstance.addConsumer(Me)
        End If
    End Sub
#End Region

    Private isHost As Boolean = True
    Private consumers As New Generic.List(Of IDataConsumer(Of DataInternalUpdate))
    Private batchUpdates As Boolean = False
    Private sendUpdateSynch As New System.Threading.Mutex()
    Private updatesBatched As New ArrayList

    Public Sub dataConsume(ByVal dataReceived As DataTCP) Implements IDataConsumer(Of DataTCP).dataConsume
        If dataReceived.command <> "TOALL" OrElse dataReceived.args.Length = 0 OrElse dataReceived.args(0) <> "UPDATE" Then Exit Sub

        applyUpdate(dataReceived)
    End Sub

    Private Sub applyUpdate(ByVal update As DataTCP)
        Dim myUserNo As Integer
        If update.args.Length <> 4 Then Exit Sub
        If Integer.TryParse(update.args(1), myUserNo) = False Then Exit Sub

        Dim myEnv As String = update.args(2)

        Dim sameEnvSameUser As Boolean = (myEnv = Environment.MachineName AndAlso myUserNo = External.current.currentUser)
        If Not sameEnvSameUser Then sendInternalupdate(update.noMessage, update.args(3), True)
    End Sub

    Public Sub addConsumer(ByVal newConsumer As IDataConsumer(Of DataInternalUpdate)) Implements IDataConsumable(Of DataInternalUpdate).addConsumer
        If consumers.Contains(newConsumer) = False Then
            consumers.Add(newConsumer)
            consumers.Sort()
        End If
    End Sub

    Public Sub removeConsumer(ByVal curConsumer As IDataConsumer(Of DataInternalUpdate)) Implements IDataConsumable(Of DataInternalUpdate).removeConsumer
        If consumers.Contains(curConsumer) Then consumers.Remove(curConsumer)
    End Sub

    Public Sub startBatchUpdate()
        batchUpdates = True
    End Sub

    Public Sub stopBatchUpdate()
        batchUpdates = False
        sendUpdate("")
    End Sub

    Private Sub sendInternalUpdate(ByVal noMessage As Integer, ByVal update As String, ByVal fromExternal As Boolean)
        If Me.consumers.Count = 0 Then Exit Sub

        update = update.Replace(vbCrLf, "")


        '        Date :2012-04-11 16:43:48
        'Version de Clinica : 1.2.1204.240
        'Ordinateur: CP03()
        '        Utilisateur(Windows) : PHYSIOTECH(-LG / administrateur)
        'Utilisateur Clinica : 1/35/Deneau,Louise/111111111111111111111111111111111111111111111111111111111111111101111111111111111111110111111111111011111000
        'HasRestarted : False
        '        Error Type : System.Exception()
        'Message:
        'strMessage=1588|TOALL|UPDATE|59|POSTE002|AccountsBills(12553)


        'Exception Stack Trace :
        '        Trace(Not available)

        'Environment stack trace :
        '   à System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
        '        à(System.Environment.get_StackTrace())
        '   à CyberInternautes.Clinica.CommonProc.External.current.addErrorLog(Exception ErrorMsg, Byte InternalCount) dans C:\DropBox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:ligne 467
        '   à CyberInternautes.Clinica.TCPClient.ProcessCommands(String strMessage) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\TCPClient.vb:ligne 333
        '   à CyberInternautes.Clinica.TCPClient.DoRead(IAsyncResult ar) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\TCPClient.vb:ligne 233
        '   à System.Net.LazyAsyncResult.Complete(IntPtr userToken)
        '   à System.Net.ContextAwareResult.CompleteCallback(Object state)
        '   à System.Threading.ExecutionContext.runTryCode(Object userData)
        '   à System.Runtime.CompilerServices.RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(TryCode code, CleanupCode backoutCode, Object userData)
        '   à System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
        '   à System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
        '   à System.Net.ContextAwareResult.Complete(IntPtr userToken)
        '   à System.Net.LazyAsyncResult.ProtectedInvokeCallback(Object result, IntPtr userToken)
        '   à System.Net.Sockets.BaseOverlappedAsyncResult.CompletionPortCallback(UInt32 errorCode, UInt32 numBytes, NativeOverlapped* nativeOverlapped)
        '   à System.Threading._IOCompletionCallback.PerformIOCompletionCallback(UInt32 errorCode, UInt32 numBytes, NativeOverlapped* pOVERLAP)
        'InnerException 1 --->
        '        Error Type : System.InvalidOperationException()
        'Message:
        '   La collection a été modifiée ; l'opération d'énumération peut ne pas s'exécuter.

        'Source:
        '   mscorlib -- Void ThrowInvalidOperationException(System.ExceptionResource)

        '   Exception Stack Trace :
        '      à System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)
        '      à System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator.MoveNext()
        '      à CyberInternautes.Clinica.WindowsUpdateDispatcher.DataConsume(DataInternalUpdate dataReceived) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 1 - Presentation\Windows\WindowsUpdateDispatcher.vb:ligne 15
        '      à CyberInternautes.Clinica.InternalUpdatesManager.SendInternalUpdate(Int32 noMessage, String Update, Boolean fromExternal) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\InternalUpdatesManager.vb:ligne 64
        '      à CyberInternautes.Clinica.InternalUpdatesManager.ApplyUpdate(DataTCP update) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\InternalUpdatesManager.vb:ligne 30
        '      à CyberInternautes.Clinica.InternalUpdatesManager.DataConsume(DataTCP dataReceived) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\InternalUpdatesManager.vb:ligne 19
        '      à CyberInternautes.Clinica.TCPClient.SendToConsumers(String data) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\TCPClient.vb:ligne 339
        '      à CyberInternautes.Clinica.TCPClient.ProcessCommands(String strMessage) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\TCPClient.vb:ligne 328

        '   Environment stack trace :
        '      à System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
        '        à(System.Environment.get_StackTrace())
        '      à CyberInternautes.Clinica.CommonProc.External.current.addErrorLog(Exception ErrorMsg, Byte InternalCount) dans C:\DropBox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:ligne 467
        '      à CyberInternautes.Clinica.TCPClient.ProcessCommands(String strMessage) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\TCPClient.vb:ligne 333
        '      à CyberInternautes.Clinica.TCPClient.DoRead(IAsyncResult ar) dans C:\DropBox\CI\Projects\Physio2-Remoting test\Layer 3 - Basic functionnalities\TCP-Communications\TCPClient.vb:ligne 233
        '      à System.Net.LazyAsyncResult.Complete(IntPtr userToken)
        '      à System.Net.ContextAwareResult.CompleteCallback(Object state)
        '      à System.Threading.ExecutionContext.runTryCode(Object userData)
        '      à System.Runtime.CompilerServices.RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(TryCode code, CleanupCode backoutCode, Object userData)
        '      à System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
        '      à System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
        '      à System.Net.ContextAwareResult.Complete(IntPtr userToken)
        '      à System.Net.LazyAsyncResult.ProtectedInvokeCallback(Object result, IntPtr userToken)
        '      à System.Net.Sockets.BaseOverlappedAsyncResult.CompletionPortCallback(UInt32 errorCode, UInt32 numBytes, NativeOverlapped* nativeOverlapped)
        '      à System.Threading._IOCompletionCallback.PerformIOCompletionCallback(UInt32 errorCode, UInt32 numBytes, NativeOverlapped* pOVERLAP)
        '   InnerException 2 --->
        '        Aucune()
        '--------------------------------------------------------------------




        '''''The modifications below are an attempt to fix to bug above
        Dim diUpdate As New DataInternalUpdate(noMessage, update, fromExternal)
        'Copying to array to be safe when me.consumers is modified meanwhile looping
        Dim consumers() As IDataConsumer(Of DataInternalUpdate)
        ReDim consumers(Me.consumers.Count - 1)
        Me.consumers.CopyTo(consumers)
        'Tried to use a integer loop instead of a for each, plus a verification of null. 
        For c As Integer = 0 To consumers.Length - 1
            Dim curConsumer As IDataConsumer(Of DataInternalUpdate) = consumers(c)
            If curConsumer IsNot Nothing Then curConsumer.dataConsume(diUpdate)
        Next
    End Sub

    ''' <summary>
    ''' Send an update to all clients. This method can be used either from the client or the host. In the first the case it will be send to the host and internally and in the second one it will be send directly to all clients.
    ''' </summary>
    ''' <param name="update">Update string to send. It has to be : UpdateFunctionName([Param1[,Param2]])</param>
    ''' <remarks></remarks>
    Public Sub sendUpdate(ByVal update As String)
        If Not isHost AndAlso update <> "" Then sendInternalupdate(0, update, False) 'Propage update within software

        'Next lines are to propagate updates through network
        sendUpdateSynch.WaitOne()

        Try
            If batchUpdates Then
                If updatesBatched.IndexOf(update) < 0 Then updatesBatched.Add(update)
            Else
                sendData(update)
            End If
        Catch ex As Exception
            Throw New Exception("", ex)
        Finally
            sendUpdateSynch.ReleaseMutex()
        End Try
    End Sub

    Private Sub sendData(ByVal update As String)
        If updatesBatched.Count > 0 Then
            For i As Integer = 0 To updatesBatched.Count - 1
                Dim sendToAllCmd As New TCPCommands.SendToAll(TCPClient.getInstance(), "UPDATE", New String() {updatesBatched(i)})
                sendToAllCmd.execute()
            Next i
            updatesBatched.Clear()
        End If
        If update <> "" Then
            Dim sendToAllCmd As New TCPCommands.SendToAll(TCPClient.getInstance(), "UPDATE", New String() {update})
            sendToAllCmd.execute()
        End If
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataTCP).priority
        Get
            Return 0
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataTCP)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataTCP)).CompareTo
        other.priority.CompareTo(priority)
    End Function
End Class
