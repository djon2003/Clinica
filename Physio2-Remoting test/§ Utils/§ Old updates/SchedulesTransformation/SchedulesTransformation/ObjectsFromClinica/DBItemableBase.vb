Public MustInherit Class DBItemableBase
    Implements IDBItemable

    Private _autoSendUpdateOnDelete As Boolean = True
    Private _autoSendUpdateOnSave As Boolean = True

#Region "Properties"
    Public Property autoSendUpdateOnDelete() As Boolean Implements IDBItemable.autoSendUpdateOnDelete
        Get
            Return _autoSendUpdateOnDelete
        End Get
        Set(ByVal value As Boolean)
            _autoSendUpdateOnDelete = value
        End Set
    End Property

    Public Property autoSendUpdateOnSave() As Boolean Implements IDBItemable.autoSendUpdateOnSave
        Get
            Return _autoSendUpdateOnSave
        End Get
        Set(ByVal value As Boolean)
            _autoSendUpdateOnSave = value
        End Set
    End Property
#End Region

#Region "Events"
    Public Event dataChanged(ByVal sender As IDBItemable) Implements IDBItemable.dataChanged
    Public Event deleted(ByVal sender As IDBItemable) Implements IDBItemable.deleted

    Protected Overridable Sub onDataChanged()
        RaiseEvent dataChanged(Me)
    End Sub

    Protected Overridable Sub onDeleted()
        RaiseEvent deleted(Me)
    End Sub
#End Region

    Public MustOverride Sub delete() Implements IDBItemable.delete
    Public MustOverride Sub loadData(ByVal data As DBItemableData) Implements IDBItemable.loadData
    Public MustOverride Sub saveData() Implements IDBItemable.saveData

    Public MustOverride ReadOnly Property noItemable() As Integer Implements IItemable.noItemable


End Class
