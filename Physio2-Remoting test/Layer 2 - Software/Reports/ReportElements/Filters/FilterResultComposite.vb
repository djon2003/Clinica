Public Class FilterResultComposite
    Inherits FilterResult

    Public Sub New()
        MyBase.New(Nothing, Report.RapportParts.All)
    End Sub

    Private _Filters As New Generic.List(Of FilterResult)

    Public Sub addFilterResult(ByVal newFilterResult As FilterResult)
        If _Filters.Contains(newFilterResult) Then Exit Sub

        _Filters.Add(newFilterResult)
    End Sub

    Public Sub clear()
        _Filters.Clear()
    End Sub

    Public Overrides ReadOnly Property filteringText() As String
        Get
            Dim _filteringText As String = ""
            For i As Integer = 0 To _Filters.Count - 1
                If _Filters(i).filteringText <> "" Then _filteringText &= _Filters(i).filteringText
            Next i
            If _filteringText <> "" Then _filteringText = _filteringText.Substring(4)
            Return _filteringText
        End Get
    End Property

    Public Overrides ReadOnly Property filteringWhereHeader() As String
        Get
            Dim _filteringWhere As String = ""
            For i As Integer = 0 To _Filters.Count - 1
                If _Filters(i).filteringWhereHeader <> "" Then _filteringWhere &= " AND " & _Filters(i).filteringWhereHeader
            Next i
            If _filteringWhere <> "" Then _filteringWhere = _filteringWhere.Substring(5)
            Return _filteringWhere
        End Get
    End Property

    Public Overrides ReadOnly Property filteringWhereBody() As String
        Get
            Dim _filteringWhere As String = ""
            For i As Integer = 0 To _Filters.Count - 1
                If _Filters(i).filteringWhereBody <> "" Then _filteringWhere &= " AND " & _Filters(i).filteringWhereBody
            Next i
            If _filteringWhere <> "" Then _filteringWhere = _filteringWhere.Substring(5)
            Return _filteringWhere
        End Get
    End Property

    Public Overrides ReadOnly Property filteringWhereFooter() As String
        Get
            Dim _filteringWhere As String = ""
            For i As Integer = 0 To _Filters.Count - 1
                If _Filters(i).filteringWhereFooter <> "" Then _filteringWhere &= " AND " & _Filters(i).filteringWhereFooter
            Next i
            If _filteringWhere <> "" Then _filteringWhere = _filteringWhere.Substring(5)
            Return _filteringWhere
        End Get
    End Property

    Public Overrides ReadOnly Property errorOccured() As Boolean
        Get
            For i As Integer = 0 To _Filters.Count - 1
                If _Filters(i).errorOccured Then Return True
            Next i

            Return False
        End Get
    End Property
End Class
