Public Class FilterPassive
    Inherits BasicFilter

    Private _Descriptive As String = ""
    Private _PassiveValue As String = ""

    Public Sub New()
        MyBase.new()
    End Sub

    Public Sub New(ByVal value As String)
        _PassiveValue = value
    End Sub

#Region "Properties"
    Public Property descriptive() As String
        Get
            Return _Descriptive
        End Get
        Set(ByVal value As String)
            _Descriptive = value
        End Set
    End Property

    Public Property passiveValue() As String
        Get
            Return _PassiveValue
        End Get
        Set(ByVal value As String)
            _PassiveValue = value
        End Set
    End Property
#End Region

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        If Not filtered Is Nothing Then passiveValue = CType(filtered, FilteringPassive).passiveValue
        Return New FilterResult(Nothing, Me.filterOnReportParts, False)
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Return New FilterResult(Nothing, Me.filterOnReportParts, False)
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "Descriptive"
                    _Descriptive = myKey.Value
                Case "PassiveValue"
                    _PassiveValue = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
