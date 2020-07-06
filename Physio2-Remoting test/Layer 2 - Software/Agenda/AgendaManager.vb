Imports CI.Clinica.Accounts.Clients.Folders.RVs

Public Class AgendaManager
    Inherits ManagerBase(Of AgendaManager)

    Protected Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Get all agenda entries for a specific user or all users within a specific date range
    ''' </summary>
    ''' <param name="noUser">User number or zero for all users</param>
    ''' <param name="from">Date starting the range</param>
    ''' <param name="to">Date ending the range</param>
    ''' <returns>A list of agenda entries that fits the specific parameters  (can be empty, but not null)</returns>
    ''' <remarks></remarks>
    Public Function loadEntries(ByVal noUser As Integer, ByVal from As Date, ByVal [to] As Date, ByVal includeAbsences As Boolean) As Generic.List(Of AgendaEntry)
        [to] = [to].AddDays(1)
        Dim daysData As DataSet = DBLinker.getInstance.readDBForGrid("SELECT 6 as FolderFrequency,0 as FolderNoCodeUser, null as FolderNoCodeDate, 'False' AS Flagged, Agenda.DateHeure AS DateHeure, Agenda.Periode as Periode, Agenda.NoTRP as NoTRP, Agenda.Reserve AS ItemText, 0 AS NoClient, 0 AS NoFolder, Agenda.NoAgenda AS NoVisiteAgenda, NoStatut as NoStatut,0 as FolderNoCodeUnique, '' AS Telephones, 0 AS Confirmed, 0 AS Evaluation, 0 AS IsBillPaid, 0 AS IsBillSouffrance,0 AS NoFacture, 0 AS MontantPayé, '' AS RemarquesRV,'' AS RemarquesFolder,'' AS RemarquesClient,'' AS Service,0 AS IsAnnounced FROM Agenda WHERE DateHeure>='" & DateFormat.getTextDate(from) & "' And DateHeure<'" & DateFormat.getTextDate([to]) & "'" & IIf(noUser = 0, "", " AND NoTRP=" & noUser) & " UNION ALL SELECT InfoFolders.Frequence as FolderFrequency,InfoFolders.NoCodeUser as FolderNoCodeUser, InfoFolders.NoCodeDate as FolderNoCodeDate, InfoVisites.Flagged,InfoVisites.DateHeure, InfoVisites.Periode, InfoVisites.NoTRP, InfoClients.Nom+','+InfoClients.Prenom AS IText, InfoVisites.NoClient, InfoVisites.NoFolder, InfoVisites.NoVisite AS NoVisiteAgenda, InfoVisites.NoStatut, InfoFolders.NoCodeUnique as FolderNoCodeUnique, InfoClients.Telephones, InfoVisites.Confirmed, InfoVisites.Evaluation, CASE WHEN (SELECT COUNT(Factures.NoFacture) FROM Factures WHERE Factures.NoFacture = InfoVisites.NoFacture AND IsSouffrance=0)=0 THEN 1 ELSE 0 END AS IsBillPaid, CASE WHEN InfoVisites.NoFacture=0 THEN 0 ELSE (SELECT IsSouffrance FROM Factures WHERE Factures.NoFacture=InfoVisites.NoFacture) END AS IsBillSouffrance, CASE WHEN InfoVisites.NoFacture IS NULL THEN 0 ELSE InfoVisites.NoFacture END, (SELECT SUM(MontantPaiement)+SUM(MontantPaiementUser)+SUM(MontantPaiementKP)+SUM(MontantPaiementClinique) FROM Factures WHERE Factures.NoFacture = InfoVisites.NoFacture) AS MontantPayé,InfoVisites.Remarques,InfoFolders.Remarques,InfoClients.Description,InfoVisites.Service,InfoVisites.IsAnnounced FROM InfoFolders INNER JOIN InfoClients INNER JOIN InfoVisites ON InfoClients.NoClient = InfoVisites.NoClient ON InfoFolders.NoFolder = InfoVisites.NoFolder WHERE " & If(includeAbsences, "", "InfoVisites.NoStatut>2 AND ") & "InfoVisites.DateHeure>='" & DateFormat.getTextDate(from) & "' And InfoVisites.DateHeure<'" & DateFormat.getTextDate([to]) & "'" & IIf(noUser = 0, "", " AND InfoVisites.NoTRP=" & noUser) & " ORDER BY DateHeure;")
        Dim entries As New Generic.List(Of AgendaEntry)
        If daysData Is Nothing OrElse daysData.Tables.Count = 0 Then Return entries

        Dim newAgendaEntry As AgendaEntry
        For i As Integer = 0 To daysData.Tables(0).Rows.Count - 1
            Dim dayData As New DBItemableData(daysData.Tables(0).Rows(i))

            Select Case daysData.Tables(0).Rows(i)("NoStatut")
                Case 6
                    newAgendaEntry = New AgendaEntryReserved(dayData)
                Case 7
                    newAgendaEntry = New AgendaEntryBlocked(dayData)
                Case Is <= 4
                    newAgendaEntry = New RendezVous(dayData)
            End Select

            entries.Add(newAgendaEntry)
        Next i

        Return entries
    End Function

    Public Function findPlaces(ByVal noUser As Integer, ByVal myDisponibilites As String, ByVal periode As Short, ByVal noCodeUnique As Integer, ByVal noClient As Integer, Optional ByVal nbWeeksUpTo As Short = 2, Optional ByRef externalData As Generic.List(Of AgendaEntry) = Nothing, Optional ByVal service As String = "", Optional ByVal noFolder As Integer = 0, Optional ByVal noVisite As Integer = 0) As Generic.List(Of AgendaPlace)
        Dim foundPlaces As New Generic.List(Of AgendaPlace)
        Dim myDispo() As String
        Dim myTime As Date
        Dim WeekDayNo, i As Byte
        Dim curIndex As Integer
        curIndex = 0
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        If myDisponibilites = "" Then Return foundPlaces

        Dim MyDate, endingDate As Date
        MyDate = Date.Today
        myDispo = myDisponibilites.Split(New Char() {";"})
        If myDispo.Length = 15 AndAlso myDispo(14) <> "" Then MyDate = CDate(myDispo(14))
        endingDate = MyDate.AddDays(nbWeeksUpTo * 7)

        Dim data As Generic.List(Of AgendaEntry)
        If externalData Is Nothing Then
            data = loadEntries(0, MyDate, endingDate, True)
        Else
            data = externalData
        End If

        Dim gotoNextDay As Boolean
        Dim usersToDo As New Generic.List(Of User)
        If noUser = 0 Then
            usersToDo = UsersManager.getInstance.getUsers(False, True, False)
        Else
            usersToDo.Add(UsersManager.getInstance.getUser(noUser))
        End If

        Dim checkConflictOptions As AgendaManager.TimeConflictOptions = AgendaManager.TimeConflictOptions.AcceptMultipleCodes Or AgendaManager.TimeConflictOptions.VerifySchedule Or AgendaManager.TimeConflictOptions.VerifyAbsences Or AgendaManager.TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime
        Do While date1Infdate2(MyDate, endingDate, True) = True
            gotoNextDay = False
            WeekDayNo = MyDate.DayOfWeek
            Dim first15 As Integer = 0
            Dim last15 As Integer = 71
            If myDispo(WeekDayNo * 2) <> "--:--" And myDispo(WeekDayNo * 2) <> "" Then first15 = CDate(myDispo(WeekDayNo * 2)).Subtract(Date.Parse("06:00")).Minutes / 15
            If myDispo(WeekDayNo * 2 + 1) <> "--:--" And myDispo(WeekDayNo * 2 + 1) <> "" Then last15 = CDate(myDispo(WeekDayNo * 2 + 1)).AddMinutes(-periode).Subtract(Date.Parse("06:00")).Minutes / 15 - 1
            If myDispo(WeekDayNo * 2) = "" Or myDispo(WeekDayNo * 2 + 1) = "" Then last15 = -1

            If last15 > -1 Then
                For i = first15 To last15
                    If i = 71 Then gotoNextDay = True
                    myTime = New Date(2000, 1, 1, 6, 0, 0).AddMinutes(i * 15)
                    MyDate = New Date(MyDate.Year, MyDate.Month, MyDate.Day, myTime.Hour, myTime.Minute, 0)

                    'For each therapist or only one the specified one
                    For Each curUser As User In usersToDo
                        If AgendaManager.getInstance.checkTimeConflict(MyDate, periode, curUser.noUser, checkConflictOptions, noClient, noCodeUnique, data, , noVisite, noFolder) = "" Then
                            foundPlaces.Add(New AgendaPlace(curUser.noUser, MyDate))
                        End If
                    Next curUser
                Next i
            End If

            MyDate = MyDate.AddDays(1)
        Loop

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        Return foundPlaces
    End Function

    Public Enum TimeConflictOptions
        VerifySchedule = 1
        AcceptDoubleClient = 2 'If not choosen, have to provide noClient parameter
        AcceptMultipleCodes = 4 'If choosen, have to provide noCodeUnique param
        EnsureClientHasNoOtherRVAtTheSameTime = 8
        VerifyAbsences = 16
    End Enum

    Public Function checkTimeConflict(ByVal entryDateTime As Date, ByVal entryPeriod As Short, ByVal noUser As Integer, Optional ByVal options As TimeConflictOptions = TimeConflictOptions.AcceptMultipleCodes Or TimeConflictOptions.VerifySchedule Or TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime, Optional ByVal noClient As Integer = 0, Optional ByVal noCodeUnique As Integer = 0, Optional ByRef externalData As Generic.List(Of AgendaEntry) = Nothing, Optional ByVal externalIndex As Integer = 0, Optional ByVal noVisite As Integer = 0, Optional ByVal noFolder As Integer = 0) As String
        If entryDateTime = Nothing Then Return "Impossible d'évaluer l'existence d'un conflict : La date et/ou l'heure n'ont pas été spécifiées"

        Dim FTime2, FTime1, VTime2, VTime1, myDate As Date
        Dim i, n As Integer
        Dim No, MyNoClient, myNoVisite, min_i As Integer
        Dim verifyAbsences As Boolean = (options And TimeConflictOptions.VerifyAbsences) = TimeConflictOptions.VerifyAbsences
        If noClient = 0 AndAlso (options And TimeConflictOptions.AcceptDoubleClient) = 0 Then options = options + TimeConflictOptions.AcceptDoubleClient
        If noCodeUnique = 0 AndAlso (options And TimeConflictOptions.AcceptMultipleCodes) = TimeConflictOptions.AcceptMultipleCodes Then options = options - TimeConflictOptions.AcceptMultipleCodes
        If noUser = 0 AndAlso (options And TimeConflictOptions.VerifySchedule) = TimeConflictOptions.VerifySchedule Then options -= TimeConflictOptions.VerifySchedule
        If noClient = 0 AndAlso (options And TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime) = TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime Then options -= TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime

        Dim curTRP As User = UsersManager.getInstance.getUser(noUser)
        If date1Infdate2(entryDateTime, curTRP.startingDate) Then Return "Date du rendez-vous incorrecte, car elle est avant la date de début du thérapeute (" & DateFormat.getTextDate(curTRP.startingDate) & ")"
        If curTRP.endingDate <> LIMIT_DATE AndAlso date1Infdate2(curTRP.endingDate, entryDateTime) Then Return "Date du rendez-vous incorrecte, car elle est après la date de fin du thérapeute (" & DateFormat.getTextDate(curTRP.endingDate) & ")"

        If (options And TimeConflictOptions.VerifySchedule) = TimeConflictOptions.VerifySchedule Then
            'Horaire spécifique
            No = entryDateTime.DayOfWeek * -1
            myDate = entryDateTime.AddDays(No)
            Dim curHoraire As Schedule = SchedulesManager.getInstance.getSchedule(noUser, myDate)
            If curHoraire Is Nothing Then Return "Il n'existe pas d'horaire pas défaut pour ce thérapeute"

            Dim verifDate As Date = New Date(entryDateTime.Ticks)
            For i = 1 To entryPeriod Step curHoraire.intervalMinutes
                If curHoraire.isOpened(verifDate) = False Then Return "Entrée incorrecte : Sélection en dehors de l'horaire du thérapeute"
                verifDate = verifDate.AddMinutes(curHoraire.intervalMinutes)
            Next i
        End If

        'Vérification s'il y a déjà une visite à cette heure & date
        n = 0
        VTime1 = entryDateTime
        VTime2 = VTime1.AddMinutes(entryPeriod)
        Dim data As Generic.List(Of AgendaEntry)
        If externalData Is Nothing Then
            data = AgendaManager.getInstance().loadEntries(0, entryDateTime, entryDateTime, verifyAbsences)
            min_i = 0
        Else
            data = externalData
            min_i = externalIndex
        End If

        'Check into all users if client already have an agendaentry at this time
        If (options And TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime) = TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime Then
            For i = 0 To data.Count - 1
                If TypeOf data(i) Is RendezVous Then
                    With CType(data(i), RendezVous)
                        If (noVisite = 0 OrElse noVisite <> .noVisite) AndAlso .noClient = noClient Then
                            FTime1 = data(i).dateHeure
                            FTime2 = FTime1.AddMinutes(data(i).period)
                            If DateFormat.isTimesOverlap(FTime1, FTime2, VTime1, VTime2) Then Return "Le client a déjà un rendez-vous entrant en conflit avec le nouveau : " & DateFormat.getTextDate(FTime1) & " de " & DateFormat.getTextDate(FTime1, DateFormat.TextDateOptions.ShortTime) & " à " & DateFormat.getTextDate(FTime2, DateFormat.TextDateOptions.ShortTime) & " avec " & UsersManager.getInstance().getUser(data(i).noTRP).toString()
                        End If
                    End With
                End If
            Next i
        End If


        'Check into day data for this user
        For i = min_i To data.Count - 1
            If data(i).noTRP <> noUser Then Continue For
            If data(i).dateHeure.Date.Equals(entryDateTime.Date) = False Then Continue For

            If TypeOf data(i) Is RendezVous Then
                MyNoClient = CType(data(i), RendezVous).noClient
                myNoVisite = CType(data(i), RendezVous).noVisite
            Else
                myNoVisite = 0
                MyNoClient = 0
            End If

            If (noVisite <> 0 AndAlso noVisite = myNoVisite) Then Continue For

            'Vérifie si le client est déjà inscrit à cette date avec ou non des codifications différentes
            If TypeOf data(i) Is RendezVous AndAlso (noFolder = 0 OrElse noFolder = CType(data(i), RendezVous).noFolder) AndAlso ((verifyAbsences = False AndAlso data(i).noStatut > 2) OrElse verifyAbsences) AndAlso MyNoClient = noClient AndAlso (options And TimeConflictOptions.AcceptDoubleClient) = 0 Then
                Dim folderText As String = ""
                If noFolder <> 0 Then folderText = " pour le dossier #" & noFolder

                If (options And TimeConflictOptions.AcceptMultipleCodes) = TimeConflictOptions.AcceptMultipleCodes Then
                    'REM_CODES
                    If CType(data(i), RendezVous).getFolderCode().noUnique = noCodeUnique Then
                        If data(i).noStatut > 2 Then
                            Return "Le client a déjà un rendez-vous ayant la même codification en cette date" & folderText & " : " & DateFormat.getTextDate(data(i).dateHeure) & " de " & DateFormat.getTextDate(data(i).dateHeure, DateFormat.TextDateOptions.ShortTime) & " à " & DateFormat.getTextDate(data(i).dateHeure.AddMinutes(data(i).period), DateFormat.TextDateOptions.ShortTime)
                        Else
                            Return "Le client a déjà une absence ayant la même codification en cette date" & folderText & " : " & DateFormat.getTextDate(data(i).dateHeure) & " de " & DateFormat.getTextDate(data(i).dateHeure, DateFormat.TextDateOptions.ShortTime) & " à " & DateFormat.getTextDate(data(i).dateHeure.AddMinutes(data(i).period), DateFormat.TextDateOptions.ShortTime)
                        End If
                    End If
                Else
                    If data(i).noStatut > 2 Then
                        Return "Le client a déjà un rendez-vous en cette date" & folderText & " : " & DateFormat.getTextDate(data(i).dateHeure) & " de " & DateFormat.getTextDate(data(i).dateHeure, DateFormat.TextDateOptions.ShortTime) & " à " & DateFormat.getTextDate(data(i).dateHeure.AddMinutes(data(i).period), DateFormat.TextDateOptions.ShortTime)
                    Else
                        Return "Le client a déjà une absence en cette date" & folderText & " : " & DateFormat.getTextDate(data(i).dateHeure) & " de " & DateFormat.getTextDate(data(i).dateHeure, DateFormat.TextDateOptions.ShortTime) & " à " & DateFormat.getTextDate(data(i).dateHeure.AddMinutes(data(i).period), DateFormat.TextDateOptions.ShortTime)
                    End If
                End If
            End If

            'Vérification d'un conflit avec les plages actives de l'agenda.
            FTime1 = data(i).dateHeure
            FTime2 = FTime1.AddMinutes(data(i).period)
            If data(i).noStatut > 2 AndAlso DateFormat.isTimesOverlap(FTime1, FTime2, VTime1, VTime2) Then Return "Entrée incorrecte : Sélection entre en conflit avec une inscription dans l'agenda : " & DateFormat.getTextDate(FTime1) & " de " & DateFormat.getTextDate(FTime1, DateFormat.TextDateOptions.ShortTime) & " à " & DateFormat.getTextDate(FTime2, DateFormat.TextDateOptions.ShortTime)
        Next i

        Return ""
    End Function
End Class
