Imports CI.Clinica.Accounts.Clients.Folders.Codifications


Namespace Accounts.Clients.Folders

    Public Class FolderAlert
        Public Shared Function add(ByVal noFolderAlertType As Integer, ByVal noClient As Integer, ByVal noFolder As Integer, ByVal affDate As Date, ByVal noUserToAlert As Integer, Optional ByVal executeSQL As Boolean = True) As String
            affDate = New Date(affDate.Year, affDate.Month, affDate.Day)

            Dim curFAT As Codifications.FolderAlertType = FolderAlertTypesManager.getInstance.getItemable(noFolderAlertType)
            Dim newAlert As New AlertOfClientAccount(noClient)
            newAlert.noUser = noUserToAlert
            newAlert.isHidden = True
            newAlert.showingDate = affDate
            newAlert.expiryDate = newAlert.showingDate.AddDays(curFAT.alertNbDaysForExpiry + 1)

            newAlert.message = replaceMessageVariables(curFAT.AlertMessage, noClient, noFolder, newAlert.expiryDate.AddDays(-1))

            Dim newAlarm As New AlarmOfClientAccount(newAlert.showingDate, noClient, noFolder, False)
            newAlert.alertAlarm = newAlarm

            Dim inserting As String = newAlert.getSqlQuery(DBLinker.QueryTypes.Add)
            inserting &= ";DELETE FROM FolderAlerts WHERE NoFolder=" & noFolder & " AND NoFolderAlertType=" & noFolderAlertType & ";INSERT INTO FolderAlerts (NoFolder,NoFolderAlertType,LastNoUserAlert,IsAlertDone) VALUES(" & noFolder & "," & noFolderAlertType & ",SCOPE_IDENTITY(),0);"

            If executeSQL Then
                DBLinker.executeSQLScript(inserting)
                myMainWin.AlertMessages.loadAlerts()
            End If

            Return inserting
        End Function

        Private Shared Function replaceMessageVariables(ByVal message As String, ByVal noClient As Integer, ByVal noFolder As Integer, ByVal endingDate As Date) As String
            message = message.Replace("###ClientName###", getClientName(noClient))
            message = message.Replace("###NoFolder###", noFolder)
            message = message.Replace("###EndDate###", DateFormat.getTextDate(endingDate))

            Return message
        End Function
    End Class

End Namespace
