Public Class FilteringComposite
    Implements System.Collections.IList, FilteringElement

    Private myList As New ArrayList

    Public Sub New()

    End Sub

    Public Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements IPropertiesManagable.loadProperties
        Dim curFiltering As FilteringElement
        For Each curFiltering In myList
            curFiltering.loadProperties(properties)
        Next
    End Sub

    Public Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements IPropertiesManagable.saveProperties
        Dim curFilter As ReportFilter
        For Each curFilter In myList
            curFilter.saveProperties(properties)
        Next
    End Sub

#Region "IList Implementation"
    Public Sub copyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
        myList.CopyTo(array, index)
    End Sub

    Public ReadOnly Property count() As Integer Implements System.Collections.ICollection.Count
        Get
            Return myList.Count
        End Get
    End Property

    Public ReadOnly Property isSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
        Get
            Return myList.IsSynchronized
        End Get
    End Property

    Public ReadOnly Property syncRoot() As Object Implements System.Collections.ICollection.SyncRoot
        Get
            Return myList.SyncRoot
        End Get
    End Property

    Public Function getEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return myList.GetEnumerator
    End Function

    Public Function add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
        Return myList.Add(value)
    End Function

    Public Sub clear() Implements System.Collections.IList.Clear
        myList.Clear()
    End Sub

    Public Function contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
        Return myList.Contains(value)
    End Function

    Public Function indexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
        Return myList.IndexOf(value)
    End Function

    Public Sub insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
        myList.Insert(index, value)
    End Sub

    Public ReadOnly Property isFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
        Get
            Return myList.IsFixedSize
        End Get
    End Property

    Public ReadOnly Property isReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
        Get
            Return myList.IsReadOnly
        End Get
    End Property

    Default Public Property Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
        Get
            If index < 0 OrElse index >= myList.Count Then Return Nothing

            Return myList.Item(index)
        End Get
        Set(ByVal value As Object)
            myList.Item(index) = value
        End Set
    End Property

    Public Sub remove(ByVal value As Object) Implements System.Collections.IList.Remove
        myList.Remove(value)
    End Sub

    Public Sub removeAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
        myList.RemoveAt(index)
    End Sub
#End Region

    Public Function indexOf(ByVal value As String) As Integer
        Dim curFiltering As BasicFiltering
        Dim n As Integer = 0
        For Each curFiltering In myList
            If curFiltering.name.EndsWith(value) Then Return n
            n += 1
        Next

        Return -1
    End Function

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString

    End Function
End Class
