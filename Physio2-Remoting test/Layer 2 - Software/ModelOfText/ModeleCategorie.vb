
Public Class ModeleCategorie
    Private _noCategorie As Integer
    Private _categorie As String = ""

    Public Sub New(ByVal noCategorie As Integer, ByVal categorie As String)
        _noCategorie = noCategorie
        _categorie = categorie
    End Sub

#Region "Propriétés"
    Public ReadOnly Property noCategorie() As Integer
        Get
            Return _noCategorie
        End Get
    End Property

    Public ReadOnly Property categorie() As String
        Get
            Return _categorie
        End Get
    End Property

#End Region

    Public Overrides Function ToString() As String
        Return _categorie
    End Function
End Class
