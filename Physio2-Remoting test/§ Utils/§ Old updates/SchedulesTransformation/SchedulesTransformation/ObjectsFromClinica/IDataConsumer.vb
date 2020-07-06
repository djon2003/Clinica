Public Interface DataConsumer(Of T)
    Inherits IComparable(Of DataConsumer(Of T))

    ReadOnly Property priority() As Integer

    Sub dataConsume(ByVal dataReceived As T)
End Interface
