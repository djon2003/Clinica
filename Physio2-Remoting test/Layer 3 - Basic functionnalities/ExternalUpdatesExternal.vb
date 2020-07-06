Public Class ExternalUpdatesExternal
    Inherits CI.ProjectUpdates.ProjectUpdateLibrary.External

    Public Overrides Sub alertUserOfRestart()
        MessageBox.Show("Le logiciel va être redémarré pour terminer la mise à jour. Merci")
    End Sub

    Public Overrides Function askUpdateForClientOnly() As Boolean
        Return MessageBox.Show("Le logiciel sur le poste doit être mis à jour pour démarrer." & vbCrLf & "Voulez-vous mettre à jour ?", "Mise à jour", MessageBoxButtons.YesNo) = DialogResult.Yes
    End Function

    Public Overrides ReadOnly Property clientUpdateVersion() As String
        Get
            Return Software.clientUpdateVersion
        End Get
    End Property

    Public Overrides Function isSoftwareVersionEqualsServer() As Boolean
        Return Software.isSoftwareVersionEqualsServer
    End Function

    Public Overrides Sub restartSoftware()
        If canRestart Then Software.restart(False)
    End Sub

    Public Overrides ReadOnly Property dataUpdateVersion() As String
        Get
            Return Software.serverUpdateVersion
        End Get
    End Property

    Public Overrides Sub setLoadingForward(ByVal forwardText As String)
        Loading.getInstance.forward(forwardText)
    End Sub

    Public Overrides Sub setLoadingTopMost(ByVal isTopMost As Boolean)
        Loading.getInstance.TopMost = isTopMost
    End Sub

    Public Overrides Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)
        If myMainWin IsNot Nothing Then
            myMainWin.StatusText(asMsgBox) = statusText
            Application.DoEvents()
        End If
    End Sub

    Public Overrides Sub changeDataUpdateVersion(ByVal newVersion As Integer)
        IO.File.WriteAllText(appPath & addSlash(appPath) & "Data\current.version", newVersion)
    End Sub

    Public Overrides Sub executeSQL(ByVal sql As String)
        DBLinker.executeSQLScript(sql)
    End Sub
End Class
