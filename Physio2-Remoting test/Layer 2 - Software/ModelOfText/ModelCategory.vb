
Public Class ModelCategory
    Private _noCategory As Integer
    Private _category As String = ""

    Public Sub New(ByVal noCategory As Integer, ByVal category As String)
        _noCategory = noCategory
        _category = category
    End Sub

#Region "Properties"
    Public ReadOnly Property noCategory() As Integer
        Get
            Return _noCategory
        End Get
    End Property

    Public ReadOnly Property category() As String
        Get
            Return _category
        End Get
    End Property

#End Region

    Public Overrides Function toString() As String
        Return _category
    End Function
End Class
