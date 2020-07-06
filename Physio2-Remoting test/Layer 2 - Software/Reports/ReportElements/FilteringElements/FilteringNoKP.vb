Public Class FilteringNoKP
    Inherits BasicFiltering

    Private _CurrentReturn As New KPSelectorReturn

#Region "Properties"
    Public Property kpFullName() As String
        Get
            Return _CurrentReturn.kpFullName
        End Get
        Set(ByVal value As String)
            _CurrentReturn.kpFullName = value
        End Set
    End Property

    Public Property noKP() As Integer
        Get
            Return _CurrentReturn.noKP
        End Get
        Set(ByVal value As Integer)
            _CurrentReturn.noKP = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As KPSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "NoKP"
                    noKP = myKey.Value
                Case "KPFullName"
                    kpFullName = myKey.Value
                Case Else
            End Select
        Next
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
