Public Interface IItemableManager(Of Self, Managed)
    Inherits IManager(Of Self), IItemables(Of Managed), IItemableManager

End Interface

Public Interface IItemableManager
    Inherits IManager, IItemables

End Interface
