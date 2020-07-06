Public Class PresencePrices

    Private Shared prices As List(Of PresencePrice)

    Private Sub New()
    End Sub

    Private Shared Sub loadPrices()
        If prices IsNot Nothing Then Exit Sub

        prices = New List(Of PresencePrice)

        'First price shall not have a startDate and last price shall not have an endDate while middle ones shall have both
        prices.Add(New PresencePrice(32, , New Date(2007, 11, 21)))
        prices.Add(New PresencePrice(35, New Date(2007, 11, 22), New Date(2009, 4, 22)))
        prices.Add(New PresencePrice(36, New Date(2009, 4, 23)))
    End Sub

    Public Shared Function getPrice(ByVal rvDate As Date) As Double
        loadPrices()

        For Each curPrice As PresencePrice In prices
            If curPrice.isPriceInDate(rvDate) Then Return curPrice.price
        Next

        Return 0
    End Function

End Class
