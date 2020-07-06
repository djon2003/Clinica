Public Class FilteringInputFieldData
    Inherits BasicFiltering

    Private _CurrentReturn As New PersoSelectorReturn

#Region "Properties"
    Public Property persoChoice() As String
        Get
            Return _CurrentReturn.persoChoice
        End Get
        Set(ByVal value As String)
            _CurrentReturn.persoChoice = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As PersoSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "PersoChoice"
                    persoChoice = myKey.Value
                Case Else
            End Select
        Next

        _CurrentReturn.persoChoice = persoChoice
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
