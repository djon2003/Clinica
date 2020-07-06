Public Class BaseLibExternal
    Inherits Base.External

    Public Overrides Sub addErrorLog(ByVal ex As System.Exception)
        Common.addErrorLog(ex)
    End Sub

    Public Overrides ReadOnly Property currentUser() As Integer
        Get
            Return 0
        End Get
    End Property

    Public Overrides Function DBLinker_askReconnect() As CI.Base.External.DoReconnect
        Return DoReconnect.Reconnect
    End Function

    Public Overrides Sub endSoftware()
        Software.doEndProcess()
        End
    End Sub

    Public Overrides Function getUser_ToString(ByVal noUser As Integer) As String
        Return ""
    End Function

    Public Overrides Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)
        Logger.logText(statusText, "Serveur", False)
    End Sub

    Public Overrides Function isUpdateSoftwareToDo() As Boolean
        Return Software.getInstance().externalUpdater.isUpdateSoftwareToDo()
    End Function

    Public Overrides Sub updateSoftware(ByVal autoReboot As Boolean)
        Software.getInstance().externalUpdater.update(autoReboot)
    End Sub

    Public Overrides Sub restartSoftware()
        If canRestart Then Software.restart(False)
    End Sub

    Public Overrides Sub refreshTasks()
        mainWin.ctlTasksManager.loadRunningTasks()
    End Sub
End Class
