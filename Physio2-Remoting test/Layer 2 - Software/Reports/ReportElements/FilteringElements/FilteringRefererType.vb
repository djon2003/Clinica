Public Class FilteringRefererType
    Inherits BasicFiltering

    Private _CurrentReturn As New RefererSelectorReturn

#Region "Properties"
    Public Property refererName() As String
        Get
            Return _CurrentReturn.refererName
        End Get
        Set(ByVal value As String)
            _CurrentReturn.refererName = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As RefererSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "RefererName"
                    refererName = myKey.Value
                Case Else
            End Select
        Next
    End Sub

    Public Overrides Sub saveproperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
