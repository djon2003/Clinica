Public Class AlertsManager
    Inherits DBItemableManagerBase(Of AlertsManager, Alert)

    Public defaultSonPath As String

    Public lastNoUpdate As Integer = 0
    Public lastNoUpdateThreadId As Integer = 0
    Public lastNoUpdateDone As Integer = 0
    Private alreadyLoadedAlerts As New Collections.Generic.List(Of Integer)
    Delegate Sub updateCallback()

    Public Enum AType
        OpenMailSystem = 0
        OpenClientAccount = 1
        OpenNote = 2
        OpenRapportGenerator = 3
        ContinueQueueListe = 4
        OpenKPAccount = 5
    End Enum

    Protected Sub New()
        Me.defaultSonPath = appPath & bar(appPath) & "Data\Sons\alerts.wav"
        'CleanExpiredAlerts()
    End Sub

    Public Event internalDataReloading(ByVal sender As Alert)

    Public Sub setAllAlertsAsOld()
        For Each curAlert As Alert In getItemables()
            curAlert.setOld()
        Next
    End Sub

    Private Function matchNewAlert(ByVal obj As Alert) As Boolean
        Return obj.isNew
    End Function

    Public Function getNewAlerts() As Generic.List(Of Alert)
        Return getItemables().FindAll(New Predicate(Of Alert)(AddressOf matchNewAlert))
    End Function

    Public Function getNotes() As Generic.List(Of PersoNote)
        Dim myNotes As New Generic.List(Of PersoNote)
        For Each curAlert As Alert In getItemables()
            If TypeOf curAlert Is AlertOfPersoNote Then myNotes.Add(CType(curAlert, AlertOfPersoNote).persoNote)
        Next

        Return myNotes
    End Function

    Public Function addAlert(ByVal message As String, ByVal userToAlert As Integer, Optional ByVal alertType As AType = AType.OpenMailSystem, Optional ByVal alertOpeningExtra As String = "", Optional ByVal expiryDate As Date = LIMIT_DATE, Optional ByVal alertAlarm As Alarm = Nothing, Optional ByVal isHidden As Boolean = False, Optional ByVal ensureUnique As Boolean = False, Optional ByVal affDate As Date = LIMIT_DATE, Optional ByVal playSound As Boolean = True) As Alert
        Dim alertDatas As New DataTable()
        alertDatas.Columns.Add("NoUserAlert")
        alertDatas.Columns.Add("NoUser")
        alertDatas.Columns.Add("AlertType")
        alertDatas.Columns.Add("AlertData")
        alertDatas.Columns.Add("AffDate")
        alertDatas.Columns.Add("ExpDate")
        alertDatas.Columns.Add("AlarmData")
        alertDatas.Columns.Add("IsHidden")
        alertDatas.Columns.Add("IsNew")
        alertDatas.Columns.Add("Message")

        Dim newAlert As Alert
        Dim newAlertData() As String
        ReDim newAlertData(alertDatas.Columns.Count - 1)
        Dim strExpiryDate As String = ""
        If expiryDate <> LIMIT_DATE Then strExpiryDate = DateFormat.getTextDate(expiryDate) & " " & DateFormat.getTextDate(expiryDate, DateFormat.TextDateOptions.ShortTime)

        newAlertData(0) = 0
        newAlertData(1) = userToAlert
        newAlertData(2) = alertType.ToString
        newAlertData(3) = alertOpeningExtra
        If affDate.Equals(LIMIT_DATE) Then
            newAlertData(4) = ""
        Else
            newAlertData(4) = DateFormat.getTextDate(affDate) & " " & DateFormat.getTextDate(affDate, DateFormat.TextDateOptions.ShortTime)
        End If
        newAlertData(5) = strExpiryDate
        If alertAlarm Is Nothing Then
            newAlertData(6) = ""
        Else
            newAlertData(6) = alertAlarm.toString()
        End If
        newAlertData(7) = isHidden
        newAlertData(8) = True
        newAlertData(9) = message

        alertDatas.Rows.Add(newAlertData)

        newAlert = Alert.getAlertType(alertDatas.Rows(0))
        newAlert.loadData(alertDatas.Rows(0), False)

        Return addAlert(userToAlert, newAlert, playSound, ensureUnique)
    End Function

    Public Sub transferAlertTo(ByVal noAlert As Integer, ByVal noUserTo As Integer, Optional ByVal noUserFrom As Integer = 0)
        If noUserFrom = 0 Then
            Dim readFromDB() As String = DBLinker.getInstance.readOneDBField("UsersAlerts", "NoUser", "WHERE NoAlert=" & noAlert)
            If readFromDB IsNot Nothing AndAlso readFromDB.Length <> 0 Then noUserFrom = readFromDB(0)
        End If
        If noUserFrom = 0 Then Exit Sub

        Dim transferedAlert As Alert = getItemable(noAlert)
        'Not an alert of this user
        If transferedAlert IsNot Nothing Then transferedAlert.saveData() 'Ensure notes are saved before transfer

        DBLinker.getInstance.updateDB("UsersAlerts", "IsNew=1,NoUser=" & noUserTo, "NoUserAlert", noAlert, False)
        InternalUpdatesManager.getInstance.sendUpdate("Alerts(" & noUserTo & "," & noAlert & ")")
        InternalUpdatesManager.getInstance.sendUpdate("Alerts(" & noUserFrom & ")")
        load()
        RaiseEvent internalDataReloading(Nothing)
    End Sub

    Private Sub alertDeleted(ByVal sender As IDBItemable)
        Me.removeItemable(sender)
        RaiseEvent internalDataReloading(sender)
    End Sub

    Public Function addAlert(ByVal userToAlert As Integer, ByVal newAlert As Alert, Optional ByVal playSound As Boolean = True, Optional ByVal ensureUnique As Boolean = False) As Alert
        newAlert.noUser = userToAlert
        If ensureUnique Then
            Dim nbAlert As String = DBLinker.getInstance.readOneDBField("UsersAlerts", "MAX(NoUserAlert)", "WHERE NoUser=" & userToAlert & " AND AlertData='" & newAlert.alertData.Replace("'", "''") & "' AND AlertType='" & newAlert.alertType.ToString.Replace("'", "''") & "' AND [Message]='" & newAlert.toString().Replace("'", "''") & "'")(0)
            If nbAlert IsNot Nothing AndAlso nbAlert <> "" AndAlso Integer.TryParse(nbAlert, 0) Then Return getItemable(nbAlert)
        End If

        AddHandler newAlert.deleted, AddressOf alertDeleted
        newAlert.saveData()

        If userToAlert = ConnectionsManager.currentUser Then
            If PreferencesManager.getUserPreferences()("SortingInstantMsgAndNotes").ToString.StartsWith("Plus") Then
                MyBase.insertItemable(0, newAlert)
            Else
                MyBase.addItemable(newAlert)
            End If
            If newAlert.alertAlarm IsNot Nothing Then AlarmManager.getInstance.addAlarm(newAlert.alertAlarm)
            AddHandler newAlert.dataSaved, AddressOf Me.alertHasBeenChanged

            If (newAlert.showingDate.Equals(LIMIT_DATE) Or date1Infdate2(newAlert.showingDate, Date.Today, True) = True) And newAlert.isHidden = False Then
                If playSound Then playAlertSound(newAlert)
                RaiseEvent internalDataReloading(newAlert)
            End If
        Else
            InternalUpdatesManager.getInstance.sendUpdate("Alerts(" & userToAlert & "," & newAlert.noAlert & ")")
        End If

        cleanExpiredAlerts()

        Return newAlert
    End Function

    Public Overrides Sub clear()
        For Each curAlert As Alert In getItemables()
            RemoveHandler CType(curAlert, Alert).dataSaved, AddressOf alertHasBeenChanged
            If TypeOf curAlert Is AlertOfPersoNote Then
                With CType(curAlert, AlertOfPersoNote)
                    .persoNote.saveNote()
                    If .persoNote.Parent IsNot Nothing Then .persoNote.Parent.Controls.Remove(.persoNote)
                End With
            End If
        Next

        MyBase.clear()
    End Sub

    Delegate Sub loading()

    Public Overrides Sub load()
        If myMainWin.InvokeRequired Then
            myMainWin.Invoke(New loading(AddressOf load))
            Exit Sub
        End If

        cleanExpiredAlerts()
        AlarmManager.getInstance.cleanExpiredAlarms()

        Dim i As Integer
        Dim curUser As User = UsersManager.currentUser
        If getNotes().Count <> 0 Then curUser.settings.persoNotes = myMainWin.AlertMessages.getNotesSettings()

        Dim sorting As String = " DESC"
        Dim sortPref As String = PreferencesManager.getUserPreferences()("SortingInstantMsgAndNotes")
        If sortPref IsNot Nothing AndAlso sortPref.StartsWith("Moins") Then sorting = ""
        Dim alerts As DataSet = DBLinker.getInstance.readDBForGrid("Usersalerts", "*", "WHERE NoUser=" & ConnectionsManager.currentUser & " ORDER BY NoUserAlert" & sorting)
        If alerts Is Nothing OrElse alerts.Tables.Count = 0 Then
            clear()
            Exit Sub
        End If

        Dim newAlert As Alert

        'Ensure that these objects are showed on top
        myMainWin.barMainObjects.addControl(myMainWin.AlertMessages)
        myMainWin.barMainObjects.addControl(myMainWin.RVMenu)
        myMainWin.barMainObjects.addControl(myMainWin.PunchClock)
        myMainWin.barMainObjects.addControl(myMainWin.printingJobs)


        Dim curAlerts As New Generic.List(Of Alert)
        With alerts.Tables(0).Rows
            For i = 0 To .Count - 1
                newAlert = Alert.getAlertType(.Item(i)) 'Constructor

                AddHandler newAlert.dataSaved, AddressOf alertHasBeenChanged
                newAlert.loadData(New DBItemableData(.Item(i)))
                curAlerts.Add(newAlert)
            Next i
        End With

        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        clear()
        For Each curAlert As Alert In curAlerts
            addItemable(curAlert)
        Next
        changingItemablesLock.ReleaseWriterLock()

        'Reshow opened notes
        myMainWin.AlertMessages.applyNotesSettings()
    End Sub

    Public Sub playAlertSound(ByVal curAlert As Alert)
        If curAlert.isNew Then
            Dim soundFile As String = curAlert.soundFile
            If soundFile <> "" Then My.Computer.Audio.Play(soundFile, AudioPlayMode.Background)
        End If
    End Sub

    Private Sub alertHasBeenChanged(ByVal sender As Alert)
        RaiseEvent internalDataReloading(sender)
    End Sub

    Public Sub cleanExpiredAlerts()
        If ConnectionsManager.currentUser <= 0 Then Exit Sub

        DBLinker.getInstance.delDB("UsersAlerts", "NoUser", ConnectionsManager.currentUser & " AND ExpDate < GETDATE() AND IsNew=0", False)
    End Sub

    Public Overloads Shared Sub sendUpdate(ByVal noUser As Integer, Optional ByVal noUserAlert As Integer = 0)
        If noUser = ConnectionsManager.currentUser Then
            AlertsManager.getInstance.load()
            AlertsManager.getInstance.alertHasBeenChanged(Nothing)
        End If
        InternalUpdatesManager.getInstance.sendUpdate("Alerts(" & noUser & IIf(noUserAlert = 0, "", "," & noUserAlert) & ")")
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function <> "Alerts" Then Exit Sub
        If dataReceived.params(0) <> ConnectionsManager.currentUser Then Exit Sub

        lastNoUpdate = dataReceived.noMessage
        lastNoUpdateThreadId = Threading.Thread.CurrentThread.ManagedThreadId
        If dataReceived.params.Length = 2 AndAlso dataReceived.params(1) <> 0 Then
            Dim myAlertDS As DataSet = DBLinker.getInstance.readDBForGrid("UsersAlerts", "*", "WHERE NoUserAlert=" & dataReceived.params(1))
            If myAlertDS Is Nothing OrElse myAlertDS.Tables.Count = 0 OrElse myAlertDS.Tables(0).Rows.Count = 0 Then Exit Sub 'Alert was deleted before receiving this update
        End If

        Me.load() 'Reloading instead of adding the new one.. because some may have been removed too
        lastNoUpdateDone = dataReceived.noMessage
    End Sub

    Protected Overloads Overrides Sub sendUpdate()
        Throw New NotImplementedException()
    End Sub
End Class
