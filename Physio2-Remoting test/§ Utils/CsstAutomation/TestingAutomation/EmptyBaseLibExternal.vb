Public Class EmptyBaseLibExternal
    Inherits CI.Base.External


    Public Overrides Sub addErrorLog(ByVal ex As System.Exception)

    End Sub

    Public Overrides ReadOnly Property currentUser() As Integer
        Get
            Return 0
        End Get
    End Property

    Public Overrides Function DBLinker_askReconnect() As CI.Base.External.DoReconnect
        Return DoReconnect.EndSoftware
    End Function

    Public Overrides Sub endSoftware()

    End Sub

    Public Overrides Function getUser_ToString(ByVal noUser As Integer) As String
        Return ""
    End Function

    Public Overrides Function isUpdateSoftwareToDo() As Boolean
        Return False
    End Function

    Public Overrides Sub refreshTasks()

    End Sub

    Public Overrides Sub restartSoftware()

    End Sub

    Public Overrides Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)

    End Sub

    Public Overrides Sub updateSoftware(ByVal autoReboot As Boolean)

    End Sub
End Class
