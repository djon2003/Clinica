Public Class AgendaPlace

    Private _time As Date
    Private _user As User

#Region "Properties"
    Public ReadOnly Property noUser() As Integer
        Get
            Return _user.noUser
        End Get
    End Property

    Public ReadOnly Property time() As Date
        Get
            Return _time
        End Get
    End Property

#End Region

    Public Sub New(ByVal noUser As Integer, ByVal time As Date)
        _time = time
        _user = UsersManager.getInstance.getUser(noUser)
    End Sub

    Public Overrides Function toString() As String
        Return DateFormat.getTextDate(_time, DateFormat.TextDateOptions.YYYYMMDDShortDayName) & " " & DateFormat.getTextDate(_time, DateFormat.TextDateOptions.ShortTime) & " " & _user.toString
    End Function

End Class
