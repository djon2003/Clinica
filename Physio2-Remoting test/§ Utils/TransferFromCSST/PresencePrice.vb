Public Class PresencePrice

    Public Const LIMIT_DATE As Date = #1/27/9999#
    Private _price As Double = 0
    Private _startDate, _endDate As Date

    Public ReadOnly Property price() As Double
        Get
            Return _price
        End Get
    End Property

    Public Sub New(ByVal price As Double, Optional ByVal startDate As Date = LIMIT_DATE, Optional ByVal endDate As Date = LIMIT_DATE)
        _price = price
        _startDate = startDate
        _endDate = endDate
    End Sub

    Public Function isPriceInDate(ByVal rvDate As Date) As Boolean
        If _startDate.Equals(LIMIT_DATE) AndAlso _endDate.Equals(LIMIT_DATE) Then Return True

        If _startDate.Equals(LIMIT_DATE) Then
            Return rvDate.Date <= _endDate.Date
        ElseIf _endDate.Equals(LIMIT_DATE) Then
            Return rvDate.Date >= _startDate.Date
        End If

        Return rvDate.Date >= _startDate.Date AndAlso rvDate.Date <= _endDate.Date
    End Function
End Class
