Public Class AlertManager
    Inherits DBItemableManagerBase(Of AlertManager, Alert)

    Public DefaultSonPath As String

    Public lastNoUpdate As Integer = 0
    Public lastNoUpdateDone As Integer = 0
    Private AlreadyLoadedAlerts As New Collections.Generic.List(Of Integer)
    Private MyNotes As New Generic.List(Of PersoNote)
    Private showedNotes As New Generic.List(Of Integer)
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
        Me.DefaultSonPath = AppPath & Bar(AppPath) & "Data\Sons\alerts.wav"
        'CleanExpiredAlerts()
    End Sub

    Public Event InternalDataReloading(ByVal sender As Alert)

#Region "Propriétés"

#End Region

    Public Sub SetAllAlertsAsOld()
        For Each curAlert As Alert In getItemables()
            curAlert.setOld()
        Next
    End Sub

    Private Function matchNewAlert(ByVal obj As Alert) As Boolean
        Return obj.IsNew
    End Function

    Public Function getNewAlerts() As Generic.List(Of Alert)
        Return getItemables().FindAll(New Predicate(Of Alert)(AddressOf matchNewAlert))
    End Function

    Public Function GetNotes() As Generic.List(Of PersoNote)
        Return MyNotes
    End Function

    Public Function AddAlert(ByVal Message As String, ByVal UserToAlert As Integer, Optional ByVal AlertType As AType = AType.OpenMailSystem, Optional ByVal AlertOpeningExtra As String = "", Optional ByVal ExpiryDate As Date = LimitDate, Optional ByVal AlertAlarm As Alarm = Nothing, Optional ByVal IsHidden As Boolean = False, Optional ByVal EnsureUnique As Boolean = False, Optional ByVal AffDate As Date = LimitDate, Optional ByVal PlaySound As Boolean = True) As Alert
        Dim AlertDatas As New DataTable()
        AlertDatas.Columns.Add("NoUserAlert")
        AlertDatas.Columns.Add("NoUser")
        AlertDatas.Columns.Add("AlertType")
        AlertDatas.Columns.Add("AlertData")
        AlertDatas.Columns.Add("AffDate")
        AlertDatas.Columns.Add("ExpDate")
        AlertDatas.Columns.Add("AlarmData")
        AlertDatas.Columns.Add("IsHidden")
        AlertDatas.Columns.Add("IsNew")
        AlertDatas.Columns.Add("Message")

        Dim NewAlert As Alert
        Dim NewAlertData() As String
        ReDim NewAlertData(AlertDatas.Columns.Count - 1)
        Dim strExpiryDate As String = ""
        If ExpiryDate <> LimitDate Then strExpiryDate = DateFormat.AffTextDate(ExpiryDate) & " " & DateFormat.AffTextDate(ExpiryDate, DateFormat.TextDateOptions.ShortTime)

        NewAlertData(0) = 0
        NewAlertData(1) = UserToAlert
        NewAlertData(2) = AlertType.ToString
        NewAlertData(3) = AlertOpeningExtra
        If AffDate.Equals(LimitDate) Then
            NewAlertData(4) = ""
        Else
            NewAlertData(4) = DateFormat.AffTextDate(AffDate) & " " & DateFormat.AffTextDate(AffDate, DateFormat.TextDateOptions.ShortTime)
        End If
        NewAlertData(5) = strExpiryDate
        If AlertAlarm Is Nothing Then
            NewAlertData(6) = ""
        Else
            NewAlertData(6) = AlertAlarm.ToString()
        End If
        NewAlertData(7) = IsHidden
        NewAlertData(8) = True
        NewAlertData(9) = Message

        AlertDatas.Rows.Add(NewAlertData)

        NewAlert = Alert.getAlertType(AlertDatas.Rows(0))
        NewAlert.loadData(AlertDatas.Rows(0), False)

        Return AddAlert(UserToAlert, NewAlert, PlaySound, EnsureUnique)
    End Function

    Public Sub ClearNotes()
        MyNotes.Clear()
    End Sub

    Public Sub TransferAlertTo(ByVal NoAlert As Integer, ByVal NoUserTo As Integer, Optional ByVal NoUserFrom As Integer = 0)
        If NoUserFrom = 0 Then
            Dim readFromDB() As String = DBLinker.GetInstance.ReadOneDBField("UsersAlerts", "NoUser", "WHERE NoAlert=" & NoAlert)
            If readFromDB IsNot Nothing AndAlso readFromDB.Length <> 0 Then NoUserFrom = readFromDB(0)
        End If
        If NoUserFrom = 0 Then Exit Sub

        DBLinker.GetInstance.UpdateDB("UsersAlerts", "IsNew=1,NoUser=" & NoUserTo, "NoUserAlert", NoAlert, False)
        Clinica.InternalUpdatesManager.getInstance.SendUpdate("Alerts(" & NoUserTo & "," & NoAlert & ")")
        Clinica.InternalUpdatesManager.getInstance.SendUpdate("Alerts(" & NoUserFrom & ")")
        load()
        RaiseEvent InternalDataReloading(Nothing)
    End Sub

    Public Function AddAlert(ByVal UserToAlert As Integer, ByVal NewAlert As Alert, Optional ByVal PlaySound As Boolean = True, Optional ByVal EnsureUnique As Boolean = False) As Alert
        NewAlert.NoUser = UserToAlert
        If EnsureUnique Then
            Dim NbAlert As String = DBLinker.GetInstance.ReadOneDBField("UsersAlerts", "MAX(NoUserAlert)", "WHERE NoUser=" & UserToAlert & " AND AlertData='" & NewAlert.AlertData & "' AND AlertType='" & NewAlert.AlertType.ToString & "' AND [Message]='" & NewAlert.ToString().Replace("'", "''") & "'")(0)
            If NbAlert IsNot Nothing AndAlso NbAlert <> "" AndAlso Integer.TryParse(NbAlert, 0) Then Return getItemable(NbAlert)
        End If

        NewAlert.saveData()

        If UserToAlert = CurrentUser Then
            If PreferencesManager.getUserPreferences()("SortingInstantMsgAndNotes").ToString.StartsWith("Plus") Then
                MyBase.insertItemable(0, NewAlert)
            Else
                MyBase.addItemable(NewAlert)
            End If
            If NewAlert.AlertAlarm IsNot Nothing Then AlarmManager.getInstance.addAlarm(NewAlert.AlertAlarm)
            AddHandler NewAlert.DataSaved, AddressOf Me.AlertHasBeenChanged

            If (NewAlert.AffDate.Equals(LimitDate) Or Date1InfDate2(NewAlert.AffDate, Date.Today, True) = True) And NewAlert.IsHidden = False Then
                If PlaySound Then PlayAlertSound(NewAlert)
                RaiseEvent InternalDataReloading(NewAlert)
            End If
        Else
            Clinica.InternalUpdatesManager.getInstance.SendUpdate("Alerts(" & UserToAlert & "," & NewAlert.NoAlert & ")")
        End If

        CleanExpiredAlerts()

        Return NewAlert
    End Function

    Public Sub AddNote(ByVal NewNote As PersoNote)
        MyNotes.Add(NewNote)
    End Sub

    Public Sub DelAlert(ByVal deletingAlert As Alert, Optional ByVal Reload As Boolean = True)
        If deletingAlert Is Nothing Then 'Just in case of..
            load()
            RaiseEvent InternalDataReloading(Nothing)
            Exit Sub
        End If

        DBLinker.GetInstance.DelDB("UsersAlerts", "NoUserAlert", deletingAlert.NoAlert, False)
        If TypeOf deletingAlert Is AlertOfPersoNote Then
            With CType(deletingAlert, AlertOfPersoNote)
                MyNotes.Remove(.PersoNote)
                .PersoNote.DeleteControl()
            End With
        End If

        If Reload Then
            load()
            RaiseEvent InternalDataReloading(Nothing)
        End If
    End Sub

    Public Overrides Sub clear()
        showedNotes.Clear()

        For Each curAlert As Alert In getItemables()
            RemoveHandler CType(curAlert, Alert).DataSaved, AddressOf AlertHasBeenChanged
            If TypeOf curAlert Is AlertOfPersoNote Then
                With CType(curAlert, AlertOfPersoNote)
                    If .PersoNote.IsClosed = False Then showedNotes.Add(curAlert.NoAlert)
                    .PersoNote.SaveNote()
                    If .PersoNote.Parent IsNot Nothing Then .PersoNote.Parent.Controls.Remove(.PersoNote)
                End With
            End If
        Next

        MyNotes.Clear()

        MyBase.clear()
    End Sub

    Public Overrides Sub load()
        If MyNotes.Count <> 0 AndAlso MyNotes(0).InvokeRequired Then
            Dim d As New updateCallback(AddressOf load)
            MyNotes(0).BeginInvoke(d)
            Exit Sub
        End If

        CleanExpiredAlerts()
        Dim i As Integer
        Dim curUser As User = UsersManager.getInstance.getUser(CurrentUser)
        If MyNotes.Count <> 0 Then curUser.settings.persoNotes = MyMainWin.AlertMessages.getNotesSettings()
        clear()
        AlarmManager.getInstance.cleanExpiredAlarms()

        Dim sorting As String = " DESC"
        Dim sortPref As String = PreferencesManager.getUserPreferences()("SortingInstantMsgAndNotes")
        If sortPref IsNot Nothing AndAlso sortPref.StartsWith("Moins") Then sorting = ""
        Dim Alerts As DataSet = DBLinker.GetInstance.ReadDBForGrid("UsersAlerts", "*", "WHERE NoUser=" & CurrentUser & " ORDER BY NoUserAlert" & sorting)
        If Alerts Is Nothing OrElse Alerts.Tables.Count = 0 Then Exit Sub

        Dim NewAlert As Alert

        'Ensure that these objects are showed on top
        MyMainWin.BarMainObjects.AddControl(MyMainWin.AlertMessages)
        MyMainWin.BarMainObjects.AddControl(MyMainWin.RVMenu)
        MyMainWin.BarMainObjects.AddControl(MyMainWin.PunchClock)

        With Alerts.Tables(0).Rows
            For i = 0 To .Count - 1
                NewAlert = Alert.getAlertType(.Item(i))

                AddHandler NewAlert.DataSaved, AddressOf AlertHasBeenChanged
                NewAlert.loadData(New DBItemableData(.Item(i)))
                addItemable(NewAlert)
            Next i
        End With

        'Reshow opened notes
        MyMainWin.AlertMessages.applyNotesSettings()
    End Sub

    Public Sub PlayAlertSound(ByVal curAlert As Alert)
        If curAlert.IsNew Then
            Dim soundFile As String = curAlert.SoundFile
            If soundFile <> "" Then My.Computer.Audio.Play(soundFile, AudioPlayMode.Background)
        End If
    End Sub

    Private Sub AlertHasBeenChanged(ByVal sender As Alert)
        RaiseEvent InternalDataReloading(sender)
    End Sub

    Public Sub CleanExpiredAlerts()
        If CurrentUser <= 0 Then Exit Sub

        DBLinker.GetInstance.DelDB("UsersAlerts", "NoUser", CurrentUser & " AND ExpDate < GETDATE() AND IsNew=0", False)
    End Sub

    Public Overloads Shared Sub SendUpdate(ByVal NoUser As Integer, Optional ByVal NoUserAlert As Integer = 0)
        If NoUser = CurrentUser Then
            AlertManager.getInstance.load()
            AlertManager.getInstance.AlertHasBeenChanged(Nothing)
        End If
        InternalUpdatesManager.getInstance.SendUpdate("Alerts(" & NoUser & IIf(NoUserAlert = 0, "", "," & NoUserAlert) & ")")
    End Sub

    Public Overrides Sub DataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function <> "Alerts" Then Exit Sub
        If dataReceived.params(0) <> CurrentUser Then Exit Sub

        lastNoUpdate = dataReceived.noMessage
        If dataReceived.params.Length = 2 AndAlso dataReceived.params(1) <> 0 Then
            Dim MyAlertDS As DataSet = DBLinker.GetInstance.ReadDBForGrid("UsersAlerts", "*", "WHERE NoUserAlert=" & dataReceived.params(1))
            If MyAlertDS Is Nothing OrElse MyAlertDS.Tables.Count = 0 OrElse MyAlertDS.Tables(0).Rows.Count = 0 Then Exit Sub 'Alert was deleted before receiving this update
        End If

        Me.load() 'Reloading instead of adding the new one.. because some may have been removed too
        lastNoUpdateDone = dataReceived.noMessage
    End Sub

    Protected Overloads Overrides Sub sendUpdate()
        Throw New NotImplementedException()
    End Sub
End Class
