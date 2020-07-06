Public Class FilteringFromTo
    Inherits BasicFiltering

    Private firstDateSet As Boolean = False
    Private secondDateSet As Boolean = False
    Private _CurrentReturn As New DateSelectorReturn

#Region "Properties"
    Public Property secondDate() As Date
        Get
            If secondDateSet Then Return _CurrentReturn.secondDate

            Return Nothing
        End Get
        Set(ByVal value As Date)
            secondDateSet = True
            _CurrentReturn.secondDate = value
        End Set
    End Property

    Public Property firstDate() As Date
        Get
            If firstDateSet Then Return _CurrentReturn.firstDate

            Return Nothing
        End Get
        Set(ByVal value As Date)
            firstDateSet = True
            _CurrentReturn.firstDate = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As DateSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "FirstDate"
                    firstDate = CDate(myKey.Value)
                Case "SecondDate"
                    secondDate = CDate(myKey.Value)
                Case Else
            End Select
        Next
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
