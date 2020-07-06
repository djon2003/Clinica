Public Class FilteringUser
    Inherits BasicFiltering

    Private _CurrentReturn As New UserSelectorReturn

#Region "Properties"
    Public Property noUser() As Integer
        Get
            Return _CurrentReturn.noUser
        End Get
        Set(ByVal value As Integer)
            _CurrentReturn.noUser = value
        End Set
    End Property

    Public Property user() As String
        Get
            Return _CurrentReturn.user
        End Get
        Set(ByVal value As String)
            _CurrentReturn.user = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As UserSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "NoUser"
                    noUser = myKey.Value
                Case Else
            End Select
        Next
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
