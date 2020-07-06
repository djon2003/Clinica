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
        addItemable(CType(newItem, IItemable))
    End Function

    Public Overridable Function getItemable(ByVal noItem As Integer) As Managed Implements IItemables(Of Managed).getItemable
        If items.ContainsKey(noItem) = False Then Return Nothing

        Return items(noItem)
    End Function

    Public Overridable Function getItemables() As System.Collections.Generic.List(Of Managed) Implements IItemables(Of Managed).getItemables
        Dim itemables() As IItemable
        ReDim itemables(items.Values.Count - 1)
        items.Values.CopyTo(itemables, 0)
        Dim managables() As Managed = Array.ConvertAll(Of IItemable, Managed)(itemables, New Converter(Of IItemable, Managed)(AddressOf Me.convertIItemable))

        Return New Generic.List(Of Managed)(managables)
    End Function

    Public Overridable Sub removeItemable(ByVal noItem As Integer) Implements IItemables(Of Managed).removeItemable, IItemables.removeItemable
        If items.ContainsKey(noItem) = False Then Exit Sub

        items.Remove(noItem)
    End Sub

    Public Overridable Sub removeItemable(ByVal delItem As Managed) Implements IItemables(Of Managed).removeItemable
        removeItemable(CType(delItem, IItemable).noItemable)
    End Sub

    Public Overridable Sub clear() Implements IItemables(Of Managed).clear, IItemables.clear
        items.Clear()
    End Sub
#End Region

    ''' <summary>
    ''' Get the number of IItemable objects currently in the list
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>The number of items in the list</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property count() As Integer Implements IItemables(Of Managed).count, IItemables.count
        Get
            Return items.Count
        End Get
    End Property

    Protected Function insertItemable(ByVal index As Integer, ByVal newItem As Managed) As String Implements IItemables(Of Managed).insertItemable
        insertItemable(index, CType(newItem, IItemable))
    End Function

    Private Function convertIItemable(ByVal input As IItemable) As Managed
        Return CType(input, Managed)
    End Function

    Public ReadOnly Property managedType() As System.Type Implements IItemables(Of Managed).managedType, IItemables.managedType
        Get
            Return GetType(Managed)
        End Get
    End Property

    Public Function addItemable(ByVal newItem As IItemable) As String Implements IItemables.addItemable
        items.Add(newItem.noItemable, newItem)
    End Function

    Public Function getItemableAsIItemable(ByVal noItem As Integer) As IItemable Implements IItemables.getItemable
        Return getItemable(noItem)
    End Function

    Public Function getItemablesAsIItemable() As System.Collections.Generic.List(Of IItemable) Implements IItemables.getItemables
        Dim itemables() As IItemable
        ReDim itemables(items.Count - 1)
        items.Values.CopyTo(itemables, 0)

        Return New Generic.List(Of IItemable)(itemables)
    End Function

    Public Function insertItemable(ByVal index As Integer, ByVal newItem As IItemable) As String Implements IItemables.insertItemable
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

        Return ""
    End Function

    Public Sub removeItemable(ByVal delItem As IItemable) Implements IItemables.removeItemable
        removeItemable(delItem.noItemable)
    End Sub
End Class
