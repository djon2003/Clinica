Public Class DateSelectorReturn
    Inherits BasicSelectorReturn

    Public Sub New()
        firstDate = LIMIT_DATE
        secondDate = LIMIT_DATE
    End Sub

    Public firstDate As Date
    Public secondDate As Date
End Class
