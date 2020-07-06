Public MustInherit Class External
    Private Shared _current As External '= New DefaultExternal()

    Private _canRestart As Boolean = True

    Public Enum DoReconnect
        Reconnect = 0
        EndSoftware = 0
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

    Public MustOverride Sub executeSQL(ByVal sql As String)
    Public MustOverride Sub changeDataUpdateVersion(ByVal newVersion As Integer)
    Public MustOverride Sub alertUserOfRestart()
    Public MustOverride Function askUpdateForClientOnly() As Boolean
    Public MustOverride Function isSoftwareVersionEqualsServer() As Boolean
    Public MustOverride Function canSoftwareBeUpdated(ByVal informUser As Boolean) As Boolean
    Public MustOverride Sub onEndSoftwareUpdate()
    Public MustOverride Sub restartSoftware()
    Public MustOverride Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)
    Public MustOverride Sub setLoadingForward(ByVal forwardText As String)
    Public MustOverride Sub setLoadingTopMost(ByVal isTopMost As Boolean)
    Public MustOverride ReadOnly Property clientUpdateVersion() As String
    Public MustOverride ReadOnly Property dataUpdateVersion() As String
    
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
End Class