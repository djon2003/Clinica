Public Class DateInterval
    Implements IComparable(Of DateInterval), IComparable

    Private _from As Date
    Private _to As Date

    Public Sub New(ByVal from As Date, Optional ByVal [to] As Date = LIMIT_DATE)
        _from = from
        _to = [to]
    End Sub

#Region "Properties"
    Public Property from() As Date
        Get
            Return _from
        End Get
        Set(ByVal value As Date)
            _from = value
        End Set
    End Property

    Public Property [to]() As Date
        Get
            Return _to
        End Get
        Set(ByVal value As Date)
            _to = value
        End Set
    End Property
#End Region

    Public Overrides Function toString() As String
        Return "De " & DateFormat.getTextDate(_from) & If(_to = LIMIT_DATE, "", " à " & DateFormat.getTextDate(_to))
    End Function

    Public Function isDateBetween(ByVal testedDate As Date) As Boolean
        Return _from <= testedDate.Date AndAlso (_to = LIMIT_DATE OrElse testedDate <= _to)
    End Function

    Public Shared Function areIntervalsOverlaping(ByVal first As DateInterval, ByVal second As DateInterval) As Boolean
        Dim firstSecondOver As Boolean = Not date1Infdate2(first.to, second.from)
        Dim secondFirstOver As Boolean = Not date1Infdate2(second.to, first.to)

        Return firstSecondOver OrElse secondFirstOver
    End Function

    Public Shared Function combineIntervals(ByVal intervals As Generic.List(Of DateInterval)) As Generic.List(Of DateInterval)
        Dim combined As New Generic.List(Of DateInterval)(intervals)

        For Each baseInterval As DateInterval In intervals
            For Each comparedInterval As DateInterval In intervals
                If baseInterval.GetHashCode() <> comparedInterval.GetHashCode() AndAlso areIntervalsOverlaping(baseInterval, comparedInterval) Then
                    Dim from As Date = If(date1Infdate2(baseInterval._from, comparedInterval._from), baseInterval._from, comparedInterval._from)
                    Dim [to] As Date = If(date1Infdate2(baseInterval._to, comparedInterval._to), comparedInterval._to, baseInterval._to)

                    combined.Add(New DateInterval(from, [to]))
                    combined.Remove(baseInterval)
                    combined.Remove(comparedInterval)
                    Return combineIntervals(combined)
                End If
            Next
        Next

        Return intervals
    End Function

    Public Shared Function createTimeline(ByVal dates As Generic.List(Of Date)) As Generic.List(Of DateInterval)
        Dim timeLine As New Generic.List(Of DateInterval)()

        If dates Is Nothing Then Return timeLine

        'Remove duplicates
        dates.Sort()
        Dim current As Integer = 0
        While current <= dates.Count - 2
            If dates(current).Date = dates(current + 1).Date Then
                dates.RemoveAt(current + 1)
            Else
                current += 1
            End If
        End While

        'Create time line
        If dates.Count <> 0 Then
            Dim curDate As Date
            curDate = dates(0)

            For i As Integer = 1 To dates.Count - 2
                timeLine.Add(New DateInterval(curDate, dates(i).AddDays(-1)))
                curDate = dates(i)
            Next i

            timeLine.Add(New DateInterval(curDate, dates(dates.Count - 1)))
        End If


        Return timeLine
    End Function

    Public Shared Function createTimeline(ByVal intervals As Generic.List(Of DateInterval)) As Generic.List(Of DateInterval)
        Dim dates As New Generic.List(Of Date)

        For Each curInterval As DateInterval In intervals
            dates.Add(curInterval._from)
            dates.Add(curInterval._to)
        Next

        Return createTimeline(dates)
    End Function

    Public Function compareTo(ByVal other As DateInterval) As Integer Implements System.IComparable(Of DateInterval).CompareTo
        Dim returning As Integer = _from.CompareTo(other._from) * -1
        If returning = 0 Then returning = _to.CompareTo(other._to) * -1

        Return returning
    End Function

    Public Function compareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        If TypeOf obj Is DateInterval Then
            Return compareTo(CType(obj, DateInterval))
        End If

        Return 0
    End Function
End Class
