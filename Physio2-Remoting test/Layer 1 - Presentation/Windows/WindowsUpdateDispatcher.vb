Friend Class WindowsUpdateDispatcher
    Implements IDataConsumer(Of DataInternalUpdate)

    Private windows As Generic.Dictionary(Of Integer, IItemable)
    Private windowsLock As Threading.ReaderWriterLock
    Delegate Sub updateCallback(ByVal update As DataInternalUpdate, ByVal curWindow As SingleWindow)

    Public Sub New(ByRef windows As Generic.Dictionary(Of Integer, IItemable), ByVal windowsLock As Threading.ReaderWriterLock)
        Me.windows = windows
        Me.windowsLock = windowsLock
        InternalUpdatesManager.getInstance.addConsumer(Me)
    End Sub

    Public Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
        'Create array to ensure thread safe
        Dim curWindows() As IItemable
        windowsLock.AcquireReaderLock(Threading.Timeout.Infinite)
        ReDim curWindows(Me.windows.Count - 1)
        windows.Values.CopyTo(curWindows, 0)
        windowsLock.ReleaseReaderLock()

        'Loop through windows to pass the update received
        For Each CurWindow As SingleWindow In curWindows
            _DataConsume(dataReceived, CurWindow)
        Next
    End Sub

    Public Sub _DataConsume(ByVal dataReceived As DataInternalUpdate, ByVal curWindow As SingleWindow)
        If curWindow.InvokeRequired Then
            Dim d As New updateCallback(AddressOf _DataConsume)
            curWindow.BeginInvoke(d, New Object() {dataReceived, curWindow})
        Else
            curWindow.dataConsume(dataReceived)
        End If
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return -1
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function
End Class
