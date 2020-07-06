Friend Class DefaultExternal
    Inherits External

    Public Overrides Function DBLinker_askReconnect() As External.DoReconnect
        Throw New NotSupportedException()
    End Function

    Public Overrides Sub endSoftware()
        Throw New NotSupportedException()
    End Sub

    Public Overrides ReadOnly Property currentUser() As Integer
        Get
            Throw New NotSupportedException()
        End Get
    End Property

    Public Overrides Function getUser_ToString(ByVal noUser As Integer) As String
        Throw New NotSupportedException()
    End Function

    Public Overrides Sub addErrorLog(ByVal ex As System.Exception)
        Throw New NotSupportedException()
    End Sub

    Public Overloads Overrides Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)
        Throw New NotSupportedException()
    End Sub

    Public Overrides Function isUpdateSoftwareToDo() As Boolean
        Throw New NotSupportedException()
    End Function

    Public Overrides Sub updateSoftware(ByVal autoReboot As Boolean)
        Throw New NotSupportedException()
    End Sub

    Public Overrides Sub restartSoftware()
        Throw New NotSupportedException()
    End Sub

    Public Overrides Sub refreshTasks()
        Throw New NotSupportedException()
    End Sub
End Class

