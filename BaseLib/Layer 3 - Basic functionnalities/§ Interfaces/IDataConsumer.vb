Public Interface IDataConsumer(Of T)
    Inherits IComparable(Of IDataConsumer(Of T))

    ReadOnly Property priority() As Integer

    Sub dataConsume(ByVal dataReceived As T)
End Interface
