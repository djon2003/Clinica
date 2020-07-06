Public Class FilteringNoClient
    Inherits BasicFiltering

    Private _CurrentReturn As New ClientSelectorReturn

#Region "Properties"
    Public Property clientFullName() As String
        Get
            Return _CurrentReturn.clientFullName
        End Get
        Set(ByVal value As String)
            _CurrentReturn.clientFullName = value
        End Set
    End Property

    Public Property noFolder() As Integer
        Get
            Return _CurrentReturn.noFolder
        End Get
        Set(ByVal value As Integer)
            _CurrentReturn.noFolder = value
        End Set
    End Property

    Public Property noClient() As Integer
        Get
            Return _CurrentReturn.noClient
        End Get
        Set(ByVal value As Integer)
            _CurrentReturn.noClient = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As ClientSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "NoFolder"
                    noFolder = myKey.Value
                Case "NoClient"
                    noClient = myKey.Value
                Case "ClientFullName"
                    clientFullName = myKey.Value
                Case Else
            End Select
        Next
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
