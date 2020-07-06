''' <summary>
''' Base to create a manager using IItemable object (Managed) providing basic functionnalities on lists. Automatically add the singleton pattern by the usage of Self. All child classes have to have a protected constructor.
''' If the object are IDBItemable types, you should use DBItemableManagerBase instead.
''' </summary>
''' <typeparam name="Self">Same class name has the one inheriting the ManagerBase (for Singleton pattern)</typeparam>
''' <typeparam name="Managed">Any class implementing IItemable</typeparam>
''' <remarks></remarks>
Public MustInherit Class ItemableManagerBase(Of Self, Managed)
    Inherits ManagerBase(Of Self)
    Implements IItemableManager(Of Self, Managed)


    Private items As New Generic.Dictionary(Of Integer, IItemable)
    Protected changingItemablesLock As New System.Threading.ReaderWriterLock()

    ''' <summary>
    ''' Return the reference of the items list.
    ''' 
    ''' When used, shall ensure locking through changingItemablesLock variable.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>To use only in last resort.</remarks>
    Protected ReadOnly Property _items() As Generic.Dictionary(Of Integer, IItemable)
        Get
            Return items
        End Get
    End Property

    Protected Sub New()
        MyBase.New()
        If GetType(Managed).GetInterface("IItemable") Is Nothing Then Throw New Exception("Invalid type for the Managed type. Shall implements IItemable interface.")
    End Sub

#Region "Overridable methods linked to the list capalities"
    Public Overridable Function addItemable(ByVal newItem As Managed) As String Implements IItemables(Of Managed).addItemable
        Return addItemable(CType(newItem, IItemable))
    End Function

    Public Overridable Function getItemable(ByVal noItem As Integer) As Managed Implements IItemables(Of Managed).getItemable
        If containsItemable(noItem) = False Then Return Nothing

        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        Dim myItem As Managed = items(noItem)
        changingItemablesLock.ReleaseReaderLock()

        Return myItem
    End Function

    Public Overridable Function getItemables() As System.Collections.Generic.List(Of Managed) Implements IItemables(Of Managed).getItemables
        Dim itemables() As IItemable
        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        ReDim itemables(items.Values.Count - 1)
        items.Values.CopyTo(itemables, 0)
        changingItemablesLock.ReleaseReaderLock()

        Dim managables() As Managed = Array.ConvertAll(Of IItemable, Managed)(itemables, New Converter(Of IItemable, Managed)(AddressOf Me.convertIItemable))

        Return New Generic.List(Of Managed)(managables)
    End Function

    Public Overridable Sub removeItemable(ByVal noItem As Integer) Implements IItemables(Of Managed).removeItemable, IItemables.removeItemable
        If containsItemable(noItem) = False Then Exit Sub

        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        items.Remove(noItem)
        changingItemablesLock.ReleaseWriterLock()
    End Sub

    Public Overridable Sub removeItemable(ByVal delItem As Managed) Implements IItemables(Of Managed).removeItemable
        removeItemable(CType(delItem, IItemable).noItemable)
    End Sub

    Public Overridable Sub clear() Implements IItemables(Of Managed).clear, IItemables.clear
        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        items.Clear()
        changingItemablesLock.ReleaseWriterLock()
    End Sub
#End Region

    Private Function containsItemable(ByVal noItem As Integer) As Boolean
        Dim containingItemable As Boolean = False
        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        containingItemable = Me.items.ContainsKey(noItem)
        changingItemablesLock.ReleaseReaderLock()

        Return containingItemable
    End Function

    ''' <summary>
    ''' Get the number of IItemable objects currently in the list
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>The number of items in the list</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property count() As Integer Implements IItemables(Of Managed).count, IItemables.count
        Get
            Dim itemablesCount As Integer = 0
            changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
            itemablesCount = Me.items.Count
            changingItemablesLock.ReleaseReaderLock()

            Return itemablesCount
        End Get
    End Property

    Protected Function insertItemable(ByVal index As Integer, ByVal newItem As Managed) As String Implements IItemables(Of Managed).insertItemable
        Return insertItemable(index, CType(newItem, IItemable))
    End Function

    Private Function convertIItemable(ByVal input As IItemable) As Managed
        Return CType(input, Managed)
    End Function

    Public ReadOnly Property managedType() As System.Type Implements IItemables(Of Managed).managedType, IItemables.managedType
        Get
            Return GetType(Managed)
        End Get
    End Property

    Private Sub item_NoItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer)
        items.Add(newNoItemable, items(oldNoItemable))
        items.Remove(oldNoItemable)
    End Sub

    Public Function addItemable(ByVal newItem As IItemable) As String Implements IItemables.addItemable
        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        items.Add(newItem.noItemable, newItem)
        AddHandler newItem.noItemableChanged, AddressOf item_NoItemableChanged

        changingItemablesLock.ReleaseWriterLock()

        Return ""
    End Function

    Public Function getItemableAsIItemable(ByVal noItem As Integer) As IItemable Implements IItemables.getItemable
        Return getItemable(noItem)
    End Function

    Public Function getItemablesAsIItemable() As System.Collections.Generic.List(Of IItemable) Implements IItemables.getItemables
        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        Dim itemables() As IItemable
        ReDim itemables(items.Count - 1)
        items.Values.CopyTo(itemables, 0)
        changingItemablesLock.ReleaseReaderLock()

        Return New Generic.List(Of IItemable)(itemables)
    End Function

    Public Function insertItemable(ByVal index As Integer, ByVal newItem As IItemable) As String Implements IItemables.insertItemable
        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        Dim itemables() As IItemable
        ReDim itemables(items.Count)
        items.Values.CopyTo(itemables, 0)

        'Move up previous items
        For i As Integer = items.Count To index + 1 Step -1
            itemables(i) = itemables(i - 1)
        Next i
        'Set inserted item
        itemables(index) = newItem

        'Apply modif to internal variables
        Me.items.Clear()
        For i As Integer = 0 To itemables.Length - 1
            Me.items.Add(itemables(i).noItemable, itemables(i))
        Next i

        changingItemablesLock.ReleaseWriterLock()

        Return ""
    End Function

    Public Sub removeItemable(ByVal delItem As IItemable) Implements IItemables.removeItemable
        removeItemable(delItem.noItemable)
    End Sub
End Class
