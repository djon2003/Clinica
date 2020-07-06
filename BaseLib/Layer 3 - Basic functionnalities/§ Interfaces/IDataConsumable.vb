Public Interface IDataConsumable(Of T)

    Sub addConsumer(ByVal newConsumer As IDataConsumer(Of T))
    Sub removeConsumer(ByVal curConsumer As IDataConsumer(Of T))

End Interface
