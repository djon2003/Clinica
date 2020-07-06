Public Interface IDBItemableManager(Of Self, Managed)
    Inherits IItemableManager(Of Self, Managed), IDBItemableManager

End Interface


Public Interface IDBItemableManager
    Inherits IItemableManager

    Sub load()
    Sub sendUpdate()
End Interface
