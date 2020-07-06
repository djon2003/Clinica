Public Interface ICommand
    Event enabilityChanged(ByVal enabled As Boolean)

    Sub load()
    Sub execute(ByVal obj As Object)
    Sub setEnability(ByVal enabled As Boolean)
End Interface
