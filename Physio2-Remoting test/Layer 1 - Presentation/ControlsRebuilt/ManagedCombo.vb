Public Class ManagedCombo
    Inherits CI.Base.ManagedCombo

    Public Overrides Property allowUserToDeleteItem() As Boolean
        Get
            If currentDroitAcces Is Nothing Then Return MyBase.allowUserToDeleteItem

            Return currentDroitAcces(54)
        End Get
        Set(ByVal value As Boolean)
            If currentDroitAcces Is Nothing Then
                MyBase.allowUserToDeleteItem = value
                Exit Property
            End If

            MyBase.allowUserToDeleteItem = currentDroitAcces(54)
        End Set
    End Property

    Public Overrides Property allowUserToAddItem() As Boolean
        Get
            If currentDroitAcces Is Nothing Then Return MyBase.allowUserToAddItem

            Return currentDroitAcces(61)
        End Get
        Set(ByVal value As Boolean)
            If currentDroitAcces Is Nothing Then
                MyBase.allowUserToAddItem = value
                Exit Property
            End If

            MyBase.allowUserToAddItem = currentDroitAcces(61)
        End Set
    End Property

    Public Overrides Property pathOfList() As String
        Get
            Return MyBase.pathOfList
        End Get
        Set(ByVal value As String)
            If value = String.Empty Then
                MyBase.pathOfList = String.Empty
            ElseIf value.IndexOf(":") <> -1 Then
                MyBase.pathOfList = value
            Else
                MyBase.pathOfList = appPath & bar(appPath) & value
            End If
        End Set
    End Property

End Class

