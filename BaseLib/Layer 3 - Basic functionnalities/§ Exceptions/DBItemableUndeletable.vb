Public Class DBItemableUndeletable
    Inherits ExceptionBase

    Private _dbItemables As Generic.List(Of IDBItemable)

#Region "Properties"
    Public ReadOnly Property dbItemables() As Generic.List(Of IDBItemable)
        Get
            Return _dbItemables
        End Get
    End Property
#End Region

    Public Sub New(ByVal dbItemables As Generic.List(Of IDBItemable))
        _dbItemables = dbItemables
    End Sub

    Public Function getItemablesNames() As String
        If _dbItemables Is Nothing Then Return ""

        Dim curSB As New System.Text.StringBuilder()
        For Each curItemable As IDBItemable In _dbItemables
            curSB.AppendLine(CType(curItemable, Object).ToString)
        Next

        Return curSB.ToString
    End Function
End Class
