Public Class BaseLibExternal
    Inherits Base.External

    Public Overrides Function DBLinker_askReconnect() As External.DoReconnect
        If MessageBox.Show("Vous venez d'être déconnecté du serveur SQL. Voulez-vous essayer de vous reconnectez ? (Sinon ferme le logiciel)", "Reconnexion", MessageBoxButtons.YesNo) = DialogResult.No Then
            Return DoReconnect.EndSoftware
        End If

        Return DoReconnect.Reconnect
    End Function

    Public Overrides Sub endSoftware()
        Software.doEndProcess()
        End
    End Sub

    Public Overrides ReadOnly Property currentUser() As Integer
        Get
            Return ConnectionsManager.currentUser
        End Get
    End Property


    Public Overrides Function getUser_ToString(ByVal noUser As Integer) As String
        Return UsersManager.getInstance.getUser(noUser).toString()
    End Function

    Public Overrides Sub addErrorLog(ByVal ex As System.Exception)
        Clinica.addErrorLog(ex)
    End Sub

    Public Overloads Overrides Sub setStatusText(ByVal statusText As String, ByVal asMsgBox As Boolean)
        If myMainWin IsNot Nothing Then
            myMainWin.StatusText(asMsgBox) = statusText
            Application.DoEvents()
        End If
    End Sub

    Public Overrides Function isUpdateSoftwareToDo() As Boolean
        Return Software.externalUpdater.isUpdateSoftwareToDo()
    End Function

    Public Overrides Sub restartSoftware()
        If canRestart Then Software.restart(False)
    End Sub

    Public Overloads Overrides Sub updateSoftware(ByVal autoReboot As Boolean)
        Software.externalUpdater.update(autoReboot)
    End Sub

    Public Overrides Sub refreshTasks()
        Software.refreshTasks()
    End Sub
End Class
