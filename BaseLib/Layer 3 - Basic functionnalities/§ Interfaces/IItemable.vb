Public Interface IItemable

    ReadOnly Property noItemable() As Integer
    Event noItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer)
End Interface
