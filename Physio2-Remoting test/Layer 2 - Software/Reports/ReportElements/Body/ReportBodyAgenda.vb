Public NotInheritable Class ReportBodyAgenda
    Inherits ReportBodyTable

    Private MyStartingDateTime, myEndingDateTime As Date
    Private myClinicSchedule As Schedule = Nothing
    Private myTRPSchedule As Schedule = Nothing
    Private myNoTRP As Integer = 0
    Private myShowRVs As Boolean = True
    Private myLoadHoraires As Boolean = False
    Private mySelectNbDays As Boolean = False

#Region "Properties"
    Public Property selectNbDays() As Boolean
        Get
            Return mySelectNbDays
        End Get
        Set(ByVal value As Boolean)
            mySelectNbDays = value
        End Set
    End Property

    Public WriteOnly Property showRVs() As Boolean
        Set(ByVal value As Boolean)
            myShowRVs = value
        End Set
    End Property

    Public WriteOnly Property noTRP() As Integer
        Set(ByVal value As Integer)
            myNoTRP = value
        End Set
    End Property

    Public WriteOnly Property startingDateTime() As Date
        Set(ByVal value As Date)
            MyStartingDateTime = value
        End Set
    End Property

    Public WriteOnly Property endingDateTime() As Date
        Set(ByVal value As Date)
            myEndingDateTime = value
        End Set
    End Property

    Public WriteOnly Property clinicSchedule() As Schedule
        Set(ByVal value As Schedule)
            myClinicSchedule = value.clone
        End Set
    End Property

    Public WriteOnly Property trpSchedule() As Schedule
        Set(ByVal value As Schedule)
            If value IsNot Nothing Then
                myTRPSchedule = value.clone
            Else
                myTRPSchedule = Nothing
            End If
        End Set
    End Property

    Public Overrides Property isGrouped() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
            MyBase.isGrouped = False
        End Set
    End Property
#End Region

    Public Sub New(ByRef rapport As Report)
        MyBase.New(rapport)

        MyBase.isGrouped = False
    End Sub

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        Dim nbDaysChoosen As Integer = myEndingDateTime.Subtract(MyStartingDateTime).Days
        Dim startJour As Byte = MyStartingDateTime.DayOfWeek
        Dim endJour As Byte = MyStartingDateTime.DayOfWeek + nbDaysChoosen - 1


        Dim nbColumns As Integer = myEndingDateTime.Subtract(MyStartingDateTime).Days
        If nbColumns < 1 Then Exit Sub

        'Sélection de l'heure minimal et maximal pour l'horaire
        Dim maxIndex As Integer = 0
        Dim minIndex As Integer = 1440
        If Not myTRPSchedule Is Nothing Then
            Dim minTime, maxTime As String
            For CurJour As Integer = startJour To endJour
                minTime = myTRPSchedule.findTime(Schedule.WTType.First, CurJour Mod 7)
                If minTime = String.Empty Then Continue For
                maxTime = myTRPSchedule.findTime(Schedule.WTType.Last, CurJour Mod 7)
                minIndex = Math.Min(minIndex, minTime.Substring(0, 2) * 60 + minTime.Substring(3, 2))
                maxIndex = Math.Max(maxIndex, maxTime.Substring(0, 2) * 60 + maxTime.Substring(3, 2))
            Next CurJour
        End If

        mySQLStatement = MyBase.transformSQLStatement(mySQLStatement)
        Dim myDataSet As DataSet = DBLinker.getInstance.readDBForGrid(mySQLStatement, False, , "Visites", , False)
        If myDataSet Is Nothing Then Exit Sub
        If myDataSet.Tables("Visites") Is Nothing Then Exit Sub

        Dim i, j, Periode As Integer

        If myShowRVs Then
            'Grow min & max upon AgendaEntries
            With myDataSet.Tables("Visites").Rows
                Dim minAgendaEntryTime, maxAgendaEntryTime As Date
                For i = 0 To .Count - 1
                    minAgendaEntryTime = .Item(i)("DateHeure")
                    maxAgendaEntryTime = .Item(i)("DateHeure")
                    minIndex = Math.Min(minIndex, minAgendaEntryTime.Hour * 60 + minAgendaEntryTime.Minute)
                    maxIndex = Math.Max(maxIndex, maxAgendaEntryTime.Hour * 60 + maxAgendaEntryTime.Minute)
                Next i
            End With
        End If

        'Set Starting & Ending time
        If minIndex = 1440 Then minIndex = 0
        If maxIndex < minIndex Then maxIndex = minIndex
        MyStartingDateTime = New Date(MyStartingDateTime.Year, MyStartingDateTime.Month, MyStartingDateTime.Day, Math.Floor(minIndex / 60), minIndex Mod 60, 0)
        myEndingDateTime = New Date(myEndingDateTime.Year, myEndingDateTime.Month, myEndingDateTime.Day, Math.Floor(maxIndex / 60), maxIndex Mod 60, 0)

        Dim myAgendaTable As New DataTable("Body")
        Dim columnName As String = ""
        Dim cellColor As String = ""
        Dim columnWidth As String = 100 / nbColumns
        forceManaging(columnWidth, True, "", False, False, False, False, ",§.", , , , , , , 2)
        columnWidth = columnWidth.Replace(",", ".")
        Dim CurTime, curDate As Date
        CurTime = MyStartingDateTime

        htmlBuilder.AppendLine("<table class=BodyTable>")

        With myAgendaTable
            htmlBuilder.Append("<tr width=").Append(columnWidth).Append("%>")
            With .Columns
                For i = 1 To nbColumns
                    columnName = DateFormat.getTextDate(CurTime, DateFormat.TextDateOptions.WeekDayName) & If(CurTime.Year = 9999, "", "<br>" & DateFormat.getTextDate(CurTime))
                    .Add(columnName)
                    htmlBuilder.Append("<td class=BodyCellTitle>").Append(columnName).Append("</td>")
                    CurTime = CurTime.AddDays(1)
                Next i
            End With
            htmlBuilder.AppendLine("</tr>")

            Dim rowsByColumnSkipping() As Integer
            ReDim rowsByColumnSkipping(myAgendaTable.Columns.Count - 1)
            For i = 0 To myAgendaTable.Columns.Count - 1
                rowsByColumnSkipping(i) = 0
            Next i
            For i = (MyStartingDateTime.Hour * 60 + MyStartingDateTime.Minute) To (myEndingDateTime.Hour * 60 + myEndingDateTime.Minute) Step 15
                Dim myDayTimeString As String = ""
                curDate = MyStartingDateTime

                htmlBuilder.Append("<tr>")
                For j = 0 To myAgendaTable.Columns.Count - 1
                    If rowsByColumnSkipping(j) = 0 Then
                        cellColor = ""
                        CurTime = New Date(curDate.Year, curDate.Month, curDate.Day, Math.Floor(i / 60), (i - Math.Floor(i / 60) * 60), 0)
                        If myShowRVs Then
                            With myDataSet.Tables("Visites").DefaultView
                                .RowFilter = "DateHeure=#" & DateFormat.getTextDate(curDate, DateFormat.TextDateOptions.YYYYMMDD) & " " & DateFormat.getTextDate(CurTime, DateFormat.TextDateOptions.ShortTime) & "#"
                                If .Count > 0 Then
                                    myDayTimeString = " " & addLink(.Item(0).Item("ITEXT").ToString, "IText", myDataSet.Tables("Visites"), 0)
                                    Periode = Integer.Parse(.Item(0)("Periode"))
                                    Select Case CType(.Item(0)("NoStatut"), Integer)
                                        Case 0
                                            cellColor = " PlageReservee"
                                        Case 3
                                            cellColor = " PlageRV"
                                        Case 4
                                            cellColor = " PlagePresence"
                                    End Select
                                Else
                                    myDayTimeString = ""
                                    cellColor = ""
                                    Periode = 15
                                End If
                            End With
                        End If

                        If cellColor = "" Then
                            If Not myClinicSchedule Is Nothing Then
                                If myClinicSchedule.isOpened(CurTime) Then
                                    cellColor = " PlageClinique"
                                Else
                                    cellColor = " PlageFerme"
                                End If
                            End If
                            If Not myTRPSchedule Is Nothing Then
                                If myTRPSchedule.isOpened(CurTime) Then cellColor = " PlageLibre"
                            End If
                        End If

                        If myShowRVs Then rowsByColumnSkipping(j) = Periode / 15

                        htmlBuilder.Append("<td class=""BodyCell").Append(cellColor).Append(""" rowspan=").Append(rowsByColumnSkipping(j)).Append(">").Append(DateFormat.getTextDate(CurTime, DateFormat.TextDateOptions.ShortTime)).Append(myDayTimeString).Append("</td>")
                        If myShowRVs Then rowsByColumnSkipping(j) -= 1
                    Else
                        rowsByColumnSkipping(j) -= 1
                    End If
                    curDate = curDate.AddDays(1)
                Next j
                htmlBuilder.AppendLine("</tr>")
            Next i
        End With

        If endTable Then htmlBuilder.AppendFormat("</table>")
    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
        htmlBuilder.Append("<!-- StartingDateTime=").Append(MyStartingDateTime).AppendLine(" -->")
        htmlBuilder.Append("<!-- EndingDateTime=").Append(myEndingDateTime).AppendLine(" -->")
    End Sub

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "StartingDateTime"
                    MyStartingDateTime = myKey.Value
                Case "EndingDateTime"
                    myEndingDateTime = myKey.Value
                Case "LoadHoraires"
                    myLoadHoraires = myKey.Value
                Case "NoTRP"
                    myNoTRP = myKey.Value
                Case "ShowRVs"
                    myShowRVs = myKey.Value
                Case "SelectNbDays"
                    mySelectNbDays = myKey.Value
                Case Else
            End Select
        Next

        'Get filter values
        Try
            With Me
                With CType(.myReport.reportFilter, FilterComposite)
                    With CType(.Item(.indexOf("FilterFromTo")), FilterFromTo).currentReturn
                        startingDateTime = .firstDate
                    End With
                    With CType(.Item(.indexOf("FilterUser")), FilterUser).currentReturn
                        myNoTRP = .noUser
                    End With
                End With
            End With
        Catch ex As Exception
            addErrorLog(ex)
        End Try

        'Select number of days to show and change filter FromTo
        Dim nbDaysChoosen As Integer = 7
        If mySelectNbDays Then
            Dim choices As String = "1 journée;2 jours;3 jours;4 jours;5 jours;6 jours;1 semaine"
            Dim myMultiChoice As New multichoice
            nbDaysChoosen = myMultiChoice.GetChoice("Veuillez choisir le nombre de jour à afficher", choices, "INDEX", ";", False, "1 semaine")
            If nbDaysChoosen < 0 Then Exit Sub

            nbDaysChoosen += 1
            myEndingDateTime = MyStartingDateTime.AddDays(nbDaysChoosen)
        Else
            myEndingDateTime = MyStartingDateTime.AddDays(nbDaysChoosen)
        End If

        Try
            With Me
                With CType(.myReport.reportFilter, FilterComposite)
                    With CType(.Item(.indexOf("FilterFromTo")), FilterFromTo)
                        .currentReturn.secondDate = myEndingDateTime.AddDays(-1)
                        .adjustReturn()
                    End With
                End With
            End With
        Catch ex As Exception
            addErrorLog(ex)
        End Try

        'Load schedules
        If myLoadHoraires Then
            Try
                With Me
                    .trpSchedule = SchedulesManager.getInstance.getSchedule(myNoTRP, MyStartingDateTime)
                    .clinicSchedule = SchedulesManager.getInstance.getSchedule(0, MyStartingDateTime)
                End With
            Catch ex As Exception
                addErrorLog(ex)
            End Try
        End If

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        properties.Add("StartingDateTime", MyStartingDateTime)
        properties.Add("EndingDateTime", myEndingDateTime)
        properties.Add("LoadHoraires", myLoadHoraires)
        properties.Add("NoTRP", myNoTRP)
        properties.Add("ShowRVs", myShowRVs)

        MyBase.saveProperties(properties)
    End Sub

    Public Overloads Shared Function findBody(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportBody
        Dim myRapportElement As New ReportBodyAgenda(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other bodys
        Return ReportBodyStats.findBody(classElementName, leRapport, True, firstClass)
    End Function
End Class
