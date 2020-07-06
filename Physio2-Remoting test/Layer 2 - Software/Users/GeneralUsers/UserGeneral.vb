Public Class UserGeneral
    Inherits User

    Private description As String = ""

    Public Sub New(ByVal description As String)
        If description Is Nothing OrElse description = "" Then Throw New InvalidOperationException("Description parameter can't not be null or an empty string")

        Me.description = description
    End Sub

    Public Overrides Function toString() As String
        Return description
    End Function


    Public Overrides Sub delete()
        'Not able to delete this user
    End Sub

    Public Overrides Sub loadData(ByVal data As Base.DBItemableData)
        'Nothing to load for this user
    End Sub

    Protected Overrides Sub onDataChanged()
        'Nothing to do
    End Sub

    Protected Overrides Sub onDeleted()
        'Nothing to do
    End Sub

    Protected Overrides Sub onNoItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer)
        'Nothing to do
    End Sub

    Public Overrides Sub saveData()
        'Nothing to do
    End Sub

End Class
