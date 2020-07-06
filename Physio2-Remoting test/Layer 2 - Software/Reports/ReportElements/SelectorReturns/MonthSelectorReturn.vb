Public Class MonthSelectorReturn
    Inherits DateSelectorReturn

    Private _Month As Byte
    Public monthName As String = ""
    Private _Year As Integer

    Public Property month() As Byte
        Get
            Return _Month
        End Get
        Set(ByVal value As Byte)
            _Month = value
            adjustDates()
        End Set
    End Property

    Public Property year() As Integer
        Get
            Return _Year
        End Get
        Set(ByVal value As Integer)
            _Year = value
            adjustDates()
        End Set
    End Property

    Private Sub adjustDates()
        If _Month = 0 Or _Year = 0 Then Exit Sub

        Me.firstDate = New Date(_Year, _Month, 1)
        Me.secondDate = Me.firstDate.AddMonths(1).AddDays(-1)
    End Sub
End Class
