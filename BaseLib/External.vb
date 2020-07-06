Public MustInherit Class External
    Private Shared _current As External = New DefaultExternal()

    Private _canRestart As Boolean = True

    Public Enum DoReconnect
        Reconnect = 0
        EndSoftware = 1
    End Enum

    Public Event canRestartChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public Property canRestart() As Boolean
        Get
            Return _canRestart
        End Get
        Set(ByVal value As Boolean)
            Dim hasChanged As Boolean = _canRestart <> value
            _canRestart = value

            If hasChanged Then onCanRestartChanged(EventArgs.Empty)
        End Set
    End Property

    Protected Overridable Sub onCanRestartChanged(ByVal e As EventArgs)
        RaiseEvent canRestartChanged(Me, e)
    End Sub

    Public MustOverride Sub refreshTasks()
    Public MustOverride Sub updateSoftware(ByVal autoReboot As Boolean)
    Public MustOverride Function isUpdateSoftwareToDo() As Boolean
    Public MustOverride Function DBLinker_askReconnect() As DoReconnect
    Public MustOverride Sub endSoftware()
    Public MustOverride Sub restartSoftware()
    Public MustOverride Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)
    Public MustOverride ReadOnly Property currentUser() As Integer
    Public MustOverride Function getUser_ToString(ByVal noUser As Integer) As String
    Public MustOverride Sub addErrorLog(ByVal ex As Exception)

    Friend Shared Property current() As External
        Get
            Return _current
        End Get
        Set(ByVal value As External)
            _current = value
        End Set
    End Property

    Public Shared Sub setCurrentExternal(ByVal current As External)
        _current = current
    End Sub

    Public Shared Sub propagateErrorLog(ByVal ex As Exception)
        If _current IsNot Nothing Then _current.addErrorLog(ex)
    End Sub
End Class