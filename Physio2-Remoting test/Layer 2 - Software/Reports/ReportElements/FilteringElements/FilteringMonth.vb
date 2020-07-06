Public Class FilteringMonth
    Inherits BasicFiltering

    Private _CurrentReturn As New MonthSelectorReturn

#Region "Properties"
    Public Property month() As Byte
        Get
            Return _CurrentReturn.month
        End Get
        Set(ByVal value As Byte)
            _CurrentReturn.month = value
        End Set
    End Property

    Public Property year() As Integer
        Get
            Return _CurrentReturn.year
        End Get
        Set(ByVal value As Integer)
            _CurrentReturn.year = value
        End Set
    End Property


    Public ReadOnly Property currentReturn() As MonthSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "Month"
                    month = myKey.Value
                Case "Year"
                    year = myKey.Value
                Case Else
            End Select
        Next
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
