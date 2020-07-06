Public Class UserDefault
    Inherits User

    Private Shared mySelf As UserDefault

    Private Sub New()
        Me.startingDate = firstUsageDate
        Me.endingDate = LIMIT_DATE
    End Sub

    Public Shared Function getInstance() As UserDefault
        If mySelf Is Nothing Then mySelf = New UserDefault()

        Return mySelf
    End Function

    Public Overrides Function toString() As String
        Return "* Défaut *"
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
