
Module ListeAttente
    Public Function addToListeAttente(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal noVisite As Integer, ByVal noTRP As Integer, ByVal periode As Integer, ByVal dateVisite As Date, Optional ByVal clientName As String = "") As Boolean
        'Droit & Accès
        If currentDroitAcces(19) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'ajouter des clients sur la liste d'attente." & vbCrLf & "Merci!", "Droit & Accès")
            Return False
        End If

        Dim myDisponibilites As New Disponibilities()
        Dim myDispo As String = myDisponibilites.GetDisponisibilites
        If myDispo = "" Then Return False
        Dim myVisiteStat As DBHelper.StatType = DBHelper.readStats("StatVisites", "NoVisite", noVisite, "NoStat", False, DBLinker.SortOrderType.Ascending)
        Dim infos(,) As String = DBLinker.getInstance.readDB("InfoFolders IF1 INNER JOIN InfoVisites IV1 ON IV1.NoFolder=IF1.NoFolder ", "NoCodeUnique,IF1.Service,IV1.DateHeure,NoCodeUser", "WHERE (IV1.NoVisite)=" & noVisite)

        If DBLinker.getInstance.writeDB("ListeAttente", "NoClient, NoFolder, NoTRP, DateAppel, Disponibilites, Periode, NoCodeUnique, Service, NoVisite,NoCodeUser", noClient & "," & noFolder & "," & noTRP & ",'" & DateFormat.getTextDate(myVisiteStat.date_Renamed, DateFormat.TextDateOptions.YYYYMMDD) & "','" & myDispo & "'," & periode & "," & infos(0, 0) & ",'" & infos(1, 0).Replace("'", "''") & "'," & noVisite & "," & If(infos(3, 0) = "", "null", infos(3, 0))) = False Then
            Return False
        Else
            DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite,Comments", "30," & noFolder & "," & noClient & "," & noVisite & ",''")
        End If

        If clientName = "" Then clientName = getClientName(noClient)

        myMainWin.StatusText = "Liste d'attente : Ajout du client " & clientName & " (" & noClient & ") sur la liste d'attente avec rendez-vous (" & DateFormat.getTextDate(infos(2, 0)) & ")"
        InternalUpdatesManager.getInstance.sendUpdate("QueueList()")
        updateVisites(noClient, noFolder, dateVisite, , False, noTRP)

        Return True
    End Function

    Public Sub openQL()
        'Droit & Accès
        If currentDroitAcces(18) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Liste d'attente." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myQueueList As QueueList = openUniqueWindow(New QueueList())
        myQueueList.Show()
    End Sub

    Public Function openRestraintQueueList(ByVal dateToLook As Date, ByVal noUser As Integer) As Boolean
        Dim done As Boolean = False
        'Droit & Accès
        If currentDroitAcces(18) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Liste d'attente." & vbCrLf & "Merci!", "Droit & Accès")
            Return True
        End If

        Dim FirstFreeTime, LastFreeTime, myDate As Date
        FirstFreeTime = dateToLook : LastFreeTime = FirstFreeTime

        myDate = dateToLook

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim agendaEntries As Generic.List(Of AgendaEntry) = AgendaManager.getInstance.loadEntries(0, dateToLook, dateToLook, True)
        Dim upsizedrange As Boolean = False

        Do While upsizedrange = False
            If time1Suptime2("06:00", FirstFreeTime.AddMinutes(-15)) Then
                upsizedrange = True
            Else
                If AgendaManager.getInstance.checkTimeConflict(FirstFreeTime.AddMinutes(-15), 15, noUser, , , , agendaEntries) = "" Then
                    FirstFreeTime = FirstFreeTime.AddMinutes(-15)
                Else
                    upsizedrange = True
                End If
            End If
        Loop

        upsizedrange = False
        Do While upsizedrange = False
            If time1Suptime2("23:45", LastFreeTime.AddMinutes(15)) = False Then
                upsizedrange = True
            Else
                If AgendaManager.getInstance.checkTimeConflict(LastFreeTime.AddMinutes(15), 15, noUser, , , , agendaEntries) = "" Then
                    LastFreeTime = LastFreeTime.AddMinutes(15)
                Else
                    upsizedrange = True
                End If
            End If
        Loop

        Dim myQLClients() As String = getQueueListClientFitting(FirstFreeTime, LastFreeTime, noUser, dateToLook, agendaEntries)
        If myQLClients Is Nothing OrElse myQLClients.Length = 0 Then done = False : GoTo fin

        Dim myQueueList As QueueList = openUniqueWindow(New QueueList(), , , True, False)
        With myQueueList
            .selectedTime = DateFormat.getTextDate(dateToLook, DateFormat.TextDateOptions.ShortTime)
            .weekDayNo = dateToLook.DayOfWeek
            .visiteDate = dateToLook
            .therapeute = noUser
            .firstFreeTime = FirstFreeTime
            .lastFreeTime = LastFreeTime
            .filtering = myQLClients
            .useWinAsSelection = True
            .MdiParent = Nothing
            .StartPosition = FormStartPosition.CenterScreen
            Dim resultOfSelection As DialogResult = .ShowDialog()
            If resultOfSelection <> DialogResult.OK And resultOfSelection <> DialogResult.Yes Then
                Dim blockedPeriod As Integer = 0
                'REM FullBlock
                blockedPeriod = (LastFreeTime.Hour - FirstFreeTime.Hour) * 60 - FirstFreeTime.Minute + LastFreeTime.Minute + 15

                ''Block uppon the bigger period
                'REM Blocking this way need a revision.. where to we place the block period ? at TimeToLook (if yes, if period > LastFreeTime, then has to set Lower than TimeToLook) ?
                'For i As Integer = 0 To MyQLClients.GetUpperBound(0)
                '    Dim CurClient() As String = MyQLClients(i).Split(New Char() {"§"})
                '    BlockedPeriod = Math.Max(BlockedPeriod, Integer.Parse(CurClient(3)))
                'Next i

                If DBLinker.getInstance.writeDB("Agenda", "DateHeure,Periode,NoTRP,NoStatut", "'" & DateFormat.getTextDate(dateToLook) & " " & DateFormat.getTextDate(FirstFreeTime, DateFormat.TextDateOptions.ShortTime) & "'," & blockedPeriod & "," & noUser & ",7") = False Then GoTo fin
                If PreferencesManager.getUserPreferences()("ReceivePlageBloqueeAlert") = True Then AlertsManager.getInstance.addAlert("Plage bloquée le " & DateFormat.getTextDate(dateToLook) & " à " & DateFormat.getTextDate(FirstFreeTime, DateFormat.TextDateOptions.ShortTime) & " dans l'agenda de " & UsersManager.getInstance.getUser(noUser).toString, ConnectionsManager.currentUser, AlertsManager.AType.ContinueQueueListe, DateFormat.getTextDate(dateToLook) & ":" & DateFormat.getTextDate(FirstFreeTime, DateFormat.TextDateOptions.ShortTime) & ":" & noUser, Date.Now.AddDays(1), , , True)
                myMainWin.StatusText = "Ajout d'une plage bloquée le " & DateFormat.getTextDate(dateToLook) & " à " & DateFormat.getTextDate(FirstFreeTime, DateFormat.TextDateOptions.ShortTime) & " dans l'agenda de " & UsersManager.getInstance.getUser(noUser).toString
                InternalUpdatesManager.getInstance.sendUpdate("Agendas(" & DateFormat.getTextDate(dateToLook, DateFormat.TextDateOptions.YYYYMMDD_FullTime) & ",False," & noUser & ")")
            End If
            If resultOfSelection = DialogResult.Yes Then WindowsManager.getInstance.selected(WindowsManager.getInstance.getItemable("Ajout d'un rendez-vous"))
        End With
        done = True

fin:
        If selfOpened Then DBLinker.getInstance().dbConnected = False

        Return done
    End Function

    Public Function getQueueListClientFitting(ByVal firstFreeTime As Date, ByVal lastFreeTime As Date, ByVal noUser As Integer, ByVal visiteDate As Date, Optional ByVal externalData As Generic.List(Of AgendaEntry) = Nothing) As String()
        Dim GQLCF(), myDispo() As String
        Dim i, n As Integer
        Dim TimeDispo, vPeriode As Integer
        Dim FirstTime, EndTime, myDate As Date
        n = 0

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim ql(,) As String = DBLinker.getInstance.readDB("ListeAttente LEFT JOIN InfoVisites ON ListeAttente.NoVisite = InfoVisites.NoVisite", _
        "ListeAttente.NoQL, ListeAttente.NoClient, ListeAttente.NoFolder, ListeAttente.NoTRP, ListeAttente.Remarques, ListeAttente.DateAppel, ListeAttente.Disponibilites, ListeAttente.Periode, ListeAttente.NoCodeUnique, ListeAttente.Service, ListeAttente.NoVisite, InfoVisites.DateHeure, ListeAttente.NoCodeUser", "WHERE (((ListeAttente.NoTRP)=" & noUser & "  Or (ListeAttente.NoTRP)=0))")
        Dim curTRP As User = UsersManager.getInstance.getUser(noUser)
        If ql Is Nothing OrElse ql.Length = 0 Then
            If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            Return Nothing
        End If

        Dim data As Generic.List(Of AgendaEntry)
        If externalData Is Nothing Then
            data = AgendaManager.getInstance.loadEntries(0, myDate, myDate, True)
        Else
            data = externalData
        End If
        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        lastFreeTime = lastFreeTime.AddMinutes(15)
        TimeDispo = (lastFreeTime.Hour * 60 + lastFreeTime.Minute) - (firstFreeTime.Hour * 60 + firstFreeTime.Minute)

        'REM_CODES
        Dim weekDayNo As Byte = visiteDate.DayOfWeek
        Dim checkConflictOptions As AgendaManager.TimeConflictOptions = AgendaManager.TimeConflictOptions.AcceptMultipleCodes Or AgendaManager.TimeConflictOptions.VerifySchedule Or AgendaManager.TimeConflictOptions.VerifyAbsences Or AgendaManager.TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime

        For i = 0 To ql.GetUpperBound(1)
            Dim noVisite, noFolder As Integer
            Integer.TryParse(ql(10, i), noVisite)
            Integer.TryParse(ql(2, i), noFolder)

            If noVisite > 0 Then If date1Infdate2(CDate(ql(11, i)), visiteDate, True) = True Then Continue For
            If curTRP.services.IndexOf(ql(9, i)) < 0 Then Continue For
            If ql(3, i) <> "" AndAlso ql(3, i) <> 0 AndAlso ql(3, i) <> noUser Then Continue For

            Dim curCode As Accounts.Clients.Folders.Codifications.FolderCode = Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getItemable(Integer.Parse(ql(8, i)), noUser, visiteDate)
            myDispo = ql(6, i).Split(New Char() {";"})
            Dim curDispoDate As Date = Date.Today
            If myDispo.Length = 15 AndAlso myDispo(14) <> "" Then curDispoDate = CDate(myDispo(14))

            If ql(7, i) = "Traitement" Then
                vPeriode = curCode.getDefaultPeriod(False)
            ElseIf ql(7, i) = "Évaluation" Then
                vPeriode = curCode.getDefaultPeriod(True)
            Else
                vPeriode = ql(7, i)
            End If


            If vPeriode <= TimeDispo And myDispo(weekDayNo * 2) <> "" AndAlso date1Infdate2(visiteDate, curDispoDate) = False Then
                If myDispo(weekDayNo * 2) = "--:--" Then
                    If AgendaManager.getInstance.checkTimeConflict(firstFreeTime, vPeriode, noUser, checkConflictOptions, ql(1, i), curCode.noUnique, data, , noVisite, noFolder) = "" Then
                        ReDim Preserve GQLCF(n)
                        GQLCF(n) = ql(1, i) & "§" & noFolder & "§" & noVisite & "§" & vPeriode
                        n += 1
                    End If
                Else
                    FirstTime = firstFreeTime
                    EndTime = FirstTime.AddMinutes(vPeriode - 15)
                    Do Until time1Suptime2(EndTime, lastFreeTime.AddMinutes(15)) = True
                        If Not GQLCF Is Nothing AndAlso GQLCF.Length <> 0 Then If searchArray(GQLCF, ql(1, i) & "§" & noFolder & "§" & noVisite & "§", SearchType.StartsWith) >= 0 Then Exit Do
                        If time1Suptime2(FirstTime, CDate(myDispo(weekDayNo * 2)), True) And time1Suptime2(CDate(myDispo(weekDayNo * 2 + 1)), EndTime, True) Then
                            If AgendaManager.getInstance.checkTimeConflict(FirstTime, vPeriode, noUser, checkConflictOptions, ql(1, i), curCode.noUnique, data, , noVisite, noFolder) = "" Then
                                ReDim Preserve GQLCF(n)
                                GQLCF(n) = ql(1, i) & "§" & noFolder & "§" & noVisite & "§" & vPeriode
                                n += 1
                            End If
                        End If
                        FirstTime = FirstTime.AddMinutes(15)
                        EndTime = FirstTime.AddMinutes(vPeriode)
                    Loop
                End If
            End If
        Next i

        Return GQLCF
    End Function


End Module
