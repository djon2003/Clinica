Public MustInherit Class ItemablesBase(Of Managed)
    Implements IItemables(Of Managed)

    Private items As New Generic.Dictionary(Of Integer, IItemable)

    Protected Sub New()
        MyBase.New()
    End Sub

#Region "Overridable methods linked to the list capalities"
    Public Overridable Function addItemable(ByVal newItem As Managed) As String Implements IItemables(Of Managed).addItemable
        items.Add(CType(newItem, IItemable).noItemable, newItem)
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

    Public Overridable Sub removeItemable(ByVal noItem As Integer) Implements IItemables(Of Managed).removeItemable
        If items.ContainsKey(noItem) = False Then Exit Sub

        items.Remove(noItem)
    End Sub

    Public Overridable Sub removeItemable(ByVal delItem As Managed) Implements IItemables(Of Managed).removeItemable
        removeItemable(CType(delItem, IItemable).noItemable)
    End Sub

    Public Overridable Sub clear() Implements IItemables(Of Managed).clear
        items.Clear()
    End Sub
#End Region

    Public ReadOnly Property count() As Integer Implements IItemables(Of Managed).count
        Get
            Return items.Count
        End Get
    End Property

    Protected Function insertItemable(ByVal index As Integer, ByVal newItem As Managed) As String Implements IItemables(Of Managed).insertItemable
        Dim itemables() As IItemable
        ReDim itemables(items.Count)
        items.Values.CopyTo(itemables, 0)

        For i As Integer = items.Count To index + 1 Step -1
            itemables(i) = itemables(i - 1)
        Next i
        itemables(index) = newItem

        Me.items.Clear()
        For i As Integer = 0 To itemables.Length - 1
            Me.items.Add(itemables(i).noItemable, itemables(i))
        Next i

        Return ""
    End Function

    Private Function convertIItemable(ByVal input As IItemable) As Managed
        Return CType(input, Managed)
    End Function

    Public ReadOnly Property managedType() As System.Type Implements IItemables(Of Managed).managedType
        Get
            Return GetType(Managed)
        End Get
    End Property
End Class
