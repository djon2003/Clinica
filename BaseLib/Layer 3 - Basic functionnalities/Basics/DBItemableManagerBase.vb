''' <summary>
''' Base to create a manager using IDBItemable object (Managed) providing basic functionnalities on lists. Automatically add the singleton pattern by the usage of Self. All child classes have to have a protected constructor.
''' </summary>
''' <typeparam name="Self">Same class name has the one inheriting the ManagerBase (for Singleton pattern)</typeparam>
''' <typeparam name="Managed">Any class implementing IItemable</typeparam>
''' <remarks></remarks>
Public MustInherit Class DBItemableManagerBase(Of Self, Managed)
    Inherits ItemableManagerBase(Of Self, Managed)
    Implements IDBItemableManager(Of Self, Managed), IDataConsumer(Of DataInternalUpdate)

    Private _autoSaveOnAdd As Boolean = True

    Protected Sub New()
        MyBase.New()
        If GetType(Managed).GetInterface("IDBItemable") Is Nothing Then Throw New Exception("Invalid type for the Managed type. Shall implements IDBItemable interface.")

        InternalUpdatesManager.getInstance.addConsumer(Me)
    End Sub

    Protected MustOverride Sub sendUpdate() Implements IDBItemableManager(Of Self, Managed).sendUpdate
    Public MustOverride Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
    Public MustOverride Sub load() Implements IDBItemableManager(Of Self, Managed).load

#Region "Properties"
    Public Overridable ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return 0
        End Get
    End Property

    Public Property autoSaveOnAdd() As Boolean
        Get
            Return _autoSaveOnAdd
        End Get
        Set(ByVal value As Boolean)
            _autoSaveOnAdd = value
        End Set
    End Property
#End Region

    Public Sub update()
        load()
        sendUpdate()
    End Sub

    Public Sub saveAll()
        Dim idManaged As Integer = 0
        Dim added As Boolean = False
        For Each curManaged As Managed In getItemables()
            With CType(curManaged, IDBItemable)
                Dim previous As Boolean = .autoSendUpdateOnSave
                .autoSendUpdateOnSave = False
                idManaged = .noItemable
                .saveData()

                'This condition adds support for deleting object into save method instead of adding
                'Because before it was idManaged <= 0, which was making a deleted item to be readded
                added = idManaged <> .noItemable
                'Ensure object is correctly keyed into MyBase.items when added
                If added Then
                    changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
                    removeItemable(idManaged)
                    addItemable(curManaged)
                    changingItemablesLock.ReleaseWriterLock()
                End If
                .autoSendUpdateOnSave = previous
            End With
        Next

        sendUpdate()
    End Sub

    Public Sub cleanNewNotSaved()
        For Each curTF As Managed In getItemables()
            If CType(curTF, IDBItemable).noItemable = 0 Then
                MyBase.removeItemable(curTF)
            End If
        Next
    End Sub

    Public Overrides Sub clear()
        For Each curTF As Managed In getItemables()
            RemoveHandler CType(curTF, IDBItemable).deleted, AddressOf managedDeleted
        Next
        MyBase.clear()
    End Sub

    Public Overrides Function addItemable(ByVal newItem As Managed) As String
        If _autoSaveOnAdd AndAlso CType(newItem, IDBItemable).noItemable = 0 Then CType(newItem, IDBItemable).saveData()

        Dim returning As String = MyBase.addItemable(newItem)

        AddHandler CType(newItem, IDBItemable).deleted, AddressOf managedDeleted
        AddHandler CType(newItem, IDBItemable).dataChanged, AddressOf managedChanged

        Return returning
    End Function

    Protected Overridable Sub managedDeleted(ByVal sender As IDBItemable)
        RemoveHandler sender.deleted, AddressOf managedDeleted
        RemoveHandler sender.dataChanged, AddressOf managedChanged
        MyBase.removeItemable(sender)
    End Sub

    Protected Overridable Sub managedChanged(ByVal sender As IDBItemable)
    End Sub

    Public Overrides Sub removeItemable(ByVal noItem As Integer)
        Dim delItem As Managed = MyBase.getItemable(noItem)
        If delItem Is Nothing Then Exit Sub

        RemoveHandler CType(delItem, IDBItemable).deleted, AddressOf managedDeleted

        MyBase.removeItemable(noItem)
    End Sub

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function

End Class
