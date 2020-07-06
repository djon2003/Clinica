Public Interface IDBItemable
    Inherits IItemable

    Event dataChanged(ByVal sender As IDBItemable)
    'Useless, because we have to subscribe to an item which will be created.. Event added(ByVal sender As IDBItemable)
    Event deleted(ByVal sender As IDBItemable)

    Property autoSendUpdateOnSave() As Boolean
    Property autoSendUpdateOnDelete() As Boolean

    Sub delete()
    Sub loadData(ByVal data As DBItemableData)
    Sub saveData()
End Interface
