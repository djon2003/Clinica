Public Class DateComparer
    Implements IComparer

    Private _InverseSorting As Boolean = False

    Public Sub New(Optional ByVal inverseSorting As Boolean = False)
        _InverseSorting = inverseSorting
    End Sub

    Public Function compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        If _InverseSorting Then Return CType(x, Date).CompareTo(CType(y, Date)) * -1

        Return CType(x, Date).CompareTo(CType(y, Date))
    End Function
End Class
