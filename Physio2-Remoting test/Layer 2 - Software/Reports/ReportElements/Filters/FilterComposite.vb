Public Class FilterComposite
    Inherits System.Collections.Generic.List(Of ReportFilter)
    Implements ReportFilter

    Public Sub New()

    End Sub

    Private _filterResult As FilterResult

    Public ReadOnly Property filterResult() As FilterResult Implements ReportFilter.filterResult
        Get
            Return _filterResult
        End Get
    End Property

    Public Property isRequired() As Boolean Implements ReportFilter.isRequired
        Get
            For i As Integer = 0 To Me.Count - 1
                If CType(Me.Item(i), ReportFilter).isRequired = True Then Return True
            Next i

            Return False
        End Get
        Set(ByVal value As Boolean)
            'Nothing has been implemented because we don't know which filter to modify
        End Set
    End Property

    Public Function filter(ByVal filtered As FilteringElement) As FilterResult Implements ReportFilter.filter
        Dim curFilter As ReportFilter
        Dim cFilterResult As FilterResultComposite = New FilterResultComposite
        Dim curFilterResult As FilterResult = Nothing
        For Each curFilter In MyBase.ToArray
            Try
                Dim indexFilter As Integer = CType(filtered, FilteringComposite).indexOf(curFilter.name.Replace("Filter", ""))
                If indexFilter = -1 Then Continue For
                curFilterResult = curFilter.filter(CType(filtered, FilteringComposite).Item(indexFilter))
                If curFilterResult Is Nothing Then Continue For

                cFilterResult.addFilterResult(curFilterResult)
            Catch ex As InvalidCastException
                'Le type d'élément filtrant ne correspond pas
            End Try

            If Not curFilterResult Is Nothing Then If curFilterResult.errorOccured Then Exit For
        Next

        _filterResult = cFilterResult

        Return cFilterResult
    End Function

    Public Function filter() As FilterResult Implements ReportFilter.filter
        Dim curFilter As ReportFilter
        Dim cFilterResult As FilterResultComposite = New FilterResultComposite
        Dim curFilterResult As FilterResult = Nothing
        For Each curFilter In MyBase.ToArray
            curFilterResult = curFilter.filter()

            cFilterResult.addFilterResult(curFilterResult)

            If curFilterResult.errorOccured Then Exit For
        Next

        _filterResult = cFilterResult

        Return cFilterResult
    End Function

    Public Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements IPropertiesManagable.loadProperties
        Dim curFilter As ReportFilter
        For Each curFilter In MyBase.ToArray
            curFilter.loadProperties(properties)
        Next
    End Sub

    Public Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements IPropertiesManagable.saveProperties
        Dim curFilter As ReportFilter
        For Each curFilter In MyBase.ToArray
            curFilter.saveProperties(properties)
        Next
    End Sub

#Region "IList Implementation"
    'Public Overrides Function add(ByVal value As ReportFilter) As Integer Implements System.Collections.Generic.IList(Of ReportFilter).Add
    '    Return MyList.Add(value)
    'End Function

    'Public Function contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
    '    Return MyList.Contains(value)
    'End Function

    'Public Function indexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
    '    Return MyList.IndexOf(value)
    'End Function

    'Public Sub insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
    '    MyList.Insert(index, value)
    'End Sub

    'Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
    '    Get
    '        Return MyList.IsFixedSize
    '    End Get
    'End Property

    'Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
    '    Get
    '        Return MyList.IsReadOnly
    '    End Get
    'End Property

    'Default Public Property Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
    '    Get
    '        If index = -1 Then Return Nothing

    '        Return MyList.Item(index)
    '    End Get
    '    Set(ByVal value As Object)
    '        MyList.Item(index) = value
    '    End Set
    'End Property

    'Public Sub remove(ByVal value As Object) Implements System.Collections.IList.Remove
    '    MyList.Remove(value)
    'End Sub

    'Public Sub removeAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt
    '    MyList.RemoveAt(index)
    'End Sub
#End Region

    Public Overloads Function indexOf(ByVal value As String) As Integer
        Dim curFilter As BasicFilter
        Dim n As Integer = 0
        For Each curFilter In MyBase.ToArray
            If curFilter.name.EndsWith(value) Then Return n
            n += 1
        Next

        Return -1
    End Function

    Public Overloads Sub add(ByVal item As ReportFilter)
        item.parent = Me
        MyBase.Add(item)
    End Sub

    Public Overloads Sub addRange(ByVal collection As Generic.IEnumerable(Of ReportFilter))
        For Each filter As ReportFilter In collection
            filter.parent = Me
        Next
        MyBase.AddRange(collection)
    End Sub

    Public Overloads Sub remove(ByVal item As ReportFilter)
        item.parent = Nothing
        MyBase.Remove(item)
    End Sub

    Public Overloads Sub removeAt(ByVal index As Integer)
        MyBase.Item(index).parent = Nothing
        MyBase.RemoveAt(index)
    End Sub

    Public Overloads Sub removeAll(ByVal match As Predicate(Of ReportFilter))
        For Each filter As ReportFilter In MyBase.FindAll(match)
            filter.parent = Nothing
        Next
        MyBase.RemoveAll(match)
    End Sub

    Public ReadOnly Property nom() As String Implements ReportFilter.name
        Get
            Return Me.GetType.ToString
        End Get
    End Property

    Public Property tableDotField() As String Implements ReportFilter.tableDotField
        Get
            Return ""
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property parent() As ReportFilter Implements ReportFilter.parent
        Get
            Return Nothing
        End Get
        Set(ByVal value As ReportFilter)

        End Set
    End Property

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString

    End Function
End Class
