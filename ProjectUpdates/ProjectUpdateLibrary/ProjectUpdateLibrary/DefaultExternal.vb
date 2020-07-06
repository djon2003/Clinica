Friend Class DefaultExternal
    Inherits External

    Public Overloads Overrides Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)
        Throw New NotSupportedException()
    End Sub

    Public Overrides Sub setLoadingForward(ByVal forwardText As String)
        Throw New NotSupportedException()
    End Sub

    Public Overrides Sub setLoadingTopMost(ByVal isTopMost As Boolean)
        Throw New NotSupportedException()
    End Sub

    Public Overrides ReadOnly Property clientUpdateVersion() As String
        Get
            Throw New NotSupportedException()
        End Get
    End Property

    Public Overrides ReadOnly Property dataUpdateVersion() As String
        Get
            Throw New NotSupportedException()
        End Get
    End Property

    Public Overrides Function isSoftwareVersionEqualsServer() As Boolean
        Throw New NotSupportedException()
    End Function

    Public Overrides Sub restartSoftware()
        Throw New NotSupportedException()
    End Sub

    Public Overrides Function askUpdateForClientOnly() As Boolean
        Throw New NotSupportedException()
    End Function

    Public Overrides Sub alertUserOfRestart()
        Throw New NotSupportedException()
    End Sub

    Public Overrides Sub changeDataUpdateVersion(ByVal newVersion As Integer)
        Throw New NotSupportedException()
    End Sub

    Public Overrides Sub executeSQL(ByVal sql As String)
        Throw New NotSupportedException()
    End Sub

    Public Overrides Function canSoftwareBeUpdated(ByVal informUser As Boolean) As Boolean
        Throw New NotSupportedException()
    End Function

    Public Overrides Sub onEndSoftwareUpdate()
        Throw New NotSupportedException()
    End Sub
End Class

