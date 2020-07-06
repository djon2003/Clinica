Public Class Schedule
    Inherits DBItemableBase
    Implements ICloneable, IComparable(Of Schedule), IComparable, IPrintable

    Private Const nbMinutesInADay As Short = 1440
    Private Const nb30MinutesInAWeek As Short = 336
    Private Const nbMinutesForOneNumber As Integer = 30
    Private openedMinutes(nb30MinutesInAWeek) As UInteger
    Private _noSchedule As Integer
    Private _intervalMinutes As Integer = 15
    Private _noUser As Integer
    Private _scheduleDate As Date = LIMIT_DATE
    Private curRapport As Report

    'Arrays to speed up functions
    Private Shared multiple48() As Integer = {0, 48, 96, 144, 192, 240, 288}
    Private Shared div30() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    Private Shared mod30() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29}

    Public Enum WTType
        First = 0
        Last = 1
    End Enum

#Region "Constructor"
    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub

    Public Sub New(ByVal noUser As Integer, ByVal dateHoraire As Date)
        Me._noUser = noUser
        If dateHoraire.Equals(LIMIT_DATE) = False Then _scheduleDate = dateHoraire.AddDays(dateHoraire.DayOfWeek * -1)
    End Sub

    Public Sub New(ByVal baseHoraire As Schedule, ByVal dateHoraire As Date)
        Me._noUser = baseHoraire.noUser
        If dateHoraire.Equals(LIMIT_DATE) = False Then _scheduleDate = dateHoraire.AddDays(dateHoraire.DayOfWeek * -1)
        Me.openedMinutes = baseHoraire.openedMinutes.Clone()
        Me.openedMinutes = baseHoraire.openedMinutes.Clone()
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property noSchedule() As Integer
        Get
            Return _noSchedule
        End Get
    End Property

    Public ReadOnly Property scheduleDate() As Date
        Get
            Return _scheduleDate
        End Get
    End Property

    Public ReadOnly Property isDefault() As Boolean
        Get
            Return _scheduleDate.Equals(LIMIT_DATE)
        End Get
    End Property

    Public ReadOnly Property noUser() As Integer
        Get
            Return _noUser
        End Get
    End Property

    Public Property intervalMinutes() As Integer
        Get
            Return _intervalMinutes
        End Get
        Set(ByVal value As Integer)
            If value <= 0 Then value = 1
            If value > 1440 Then value = 1440

            _intervalMinutes = value
        End Set
    End Property
#End Region

    Private Function reverseIntegerBits(ByVal curInteger As UInteger) As UInteger
        Dim newInteger As UInteger = 0
        Dim c As Integer = nbMinutesForOneNumber
        While c
            newInteger = (newInteger << 1)
            newInteger = newInteger Or (curInteger And 1)
            curInteger = (curInteger >> 1)

            c -= 1
        End While

        Return newInteger
    End Function

    Public Function findTime(ByVal whichTime As WTType, ByVal noWeekDay As Byte) As String
        Dim h As UInteger
        Dim position As Integer = multiple48(noWeekDay)
        Dim stepMultiplier As Integer = 1
        Dim start As Integer = 0
        If whichTime = WTType.Last Then
            position += 47
            stepMultiplier = -1
            start = _intervalMinutes - 1
        End If
        Dim nextI As Integer = 0
        Dim leftInterval As Integer = 0
        For i As Integer = start To 1439 Step _intervalMinutes
            If i >= nextI Then
                'Change data 30 minutes range
                h = openedMinutes(position + Math.DivRem(i, nbMinutesForOneNumber, leftInterval) * stepMultiplier)
                If whichTime = WTType.Last Then h = reverseIntegerBits(h)

                h = (h >> leftInterval)
                nextI += nbMinutesForOneNumber
            End If

            If (h And 1) = 1 Then
                If whichTime = WTType.Last Then i = 1439 - i
                Dim minute As Integer
                Dim hour As Integer = Math.DivRem(i, 60, minute)
                Return DateFormat.getTextDate(New Date(2000, 1, 1, hour, minute, 0), DateFormat.TextDateOptions.ShortTime)
            End If

            h = (h >> _intervalMinutes)
        Next

        Return String.Empty
    End Function

    Public Function findTime(ByVal whichTime As WTType, ByVal checkDay As Date) As String
        Return findTime(whichTime, checkDay.DayOfWeek)
    End Function

    Public Function isOpened(ByVal myDay As Date) As Boolean
        Dim position As Integer = multiple48(myDay.DayOfWeek) + (myDay.Hour << 1) + div30(myDay.Minute)

        Dim h As UInteger = openedMinutes(position)
        h = (h >> mod30(myDay.Minute))

        Return (h And 1) = 1
    End Function

    Public Function totalMinutesByDay(ByVal myDay As DayOfWeek) As Integer
        Dim totalMinutes As Integer = 0
        Dim position As Integer = multiple48(myDay)

        Dim h As UInteger
        For i As Integer = 0 To 1439 'Step 60
            If i Mod nbMinutesForOneNumber = 0 Then h = openedMinutes(position) : position += 1

            If (h And 1) = 1 Then totalMinutes += 1

            h = (h >> 1)
        Next

        Return totalMinutes
    End Function

    Public Sub setOpened(ByVal myDay As Date, ByVal isOpened As Boolean)
        Dim position As Integer = multiple48(myDay.DayOfWeek) + (myDay.Hour << 1) + div30(myDay.Minute)

        Dim h As UInteger = openedMinutes(position)
        Dim bit As Integer = If(isOpened, 1, 0)
        Dim rightPart As UInteger = 0
        Dim myMinuteMod30 As Integer = mod30(myDay.Minute)
        If myMinuteMod30 <> 0 Then rightPart = (h And (2 ^ Math.Ceiling(Math.Log(myMinuteMod30, 2)) - 1))

        h = ((((h >> (myMinuteMod30 + 1) << 1)) Or bit) << myMinuteMod30) Or rightPart
        openedMinutes(position) = h
    End Sub

    Public Function clone() As Object Implements System.ICloneable.Clone
        Dim newHoraire As New Schedule(_noUser, _scheduleDate)
        newHoraire.openedMinutes = Me.openedMinutes
        newHoraire.openedMinutes = Me.openedMinutes.Clone
        newHoraire._intervalMinutes = Me._intervalMinutes

        Return newHoraire
    End Function

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("Horaires", "NoHoraire", noSchedule, False)
        onDeleted()
        If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("Horaire-Del(" & Me.noSchedule & "," & Me.noUser & "," & DateFormat.getTextDate(Me.scheduleDate) & ")")
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData
        _noSchedule = curData("NoHoraire")
        If curData("NoUser").Equals(DBNull.Value) = False Then _noUser = curData("NoUser")
        If curData("DateHoraire").Equals(DBNull.Value) = False Then _scheduleDate = curData("DateHoraire")

        openedMinutes = Array.ConvertAll(Of String, UInteger)(curData("OpenedMinutes").ToString().Split(New Char() {","}), New System.Converter(Of String, UInteger)(AddressOf Convert.ToUInt32))

        _intervalMinutes = curData("intervalMinutes")
    End Sub

    Public Overrides Sub saveData()
        If _noSchedule = 0 Then
            Dim lookForExistingHor() As String = DBLinker.getInstance.readOneDBField("Horaires", "NoHoraire", "WHERE (((NoUser)=" & IIf(_noUser = 0, "0 OR Horaires.NoUser IS NULL", _noUser) & ") AND (DateHoraire = " & IIf(_scheduleDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(_scheduleDate) & "'") & "));")
            If lookForExistingHor IsNot Nothing AndAlso lookForExistingHor.Length <> 0 Then _noSchedule = lookForExistingHor(0)
        End If

        Dim openedMinutes As String = String.Join(",", Array.ConvertAll(Of UInteger, String)(Me.openedMinutes, New System.Converter(Of UInteger, String)(AddressOf Convert.ToString)))

        'Save/Insert Horaire
        If Me.noSchedule = 0 Then
            DBLinker.getInstance.writeDB("Horaires", "NoUser,DateHoraire,OpenedMinutes,IntervalMinutes", _
            IIf(_noUser = 0, "null", _noUser) & "," & IIf(_scheduleDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(_scheduleDate) & "'") & ",'" & openedMinutes & "'," & _intervalMinutes, , , , _noSchedule)

            If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("Horaire-Add(" & Me.noSchedule & "," & Me.noUser & "," & DateFormat.getTextDate(Me.scheduleDate) & ")")
        Else
            DBLinker.getInstance.updateDB("Horaires", "OpenedMinutes='" & openedMinutes & "'", "noHoraire", _noSchedule, False)
            onDataChanged()
            If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("Horaire-Modif(" & Me.noSchedule & "," & Me.noUser & "," & DateFormat.getTextDate(Me.scheduleDate) & ")")
        End If

        If myMainWin IsNot Nothing Then
            Dim semaine As String = IIf(isDefault, "par défaut", "du " & toString())
            If _noUser = 0 Then
                myMainWin.StatusText = "Horaire de la clinique de la semaine " & semaine & " enregistré"
            Else
                myMainWin.StatusText = "Horaire de " & UsersManager.getInstance.getUser(_noUser).toString() & " de la semaine " & semaine & " enregistré"
            End If
        End If
    End Sub

    Public Function compareTo(ByVal other As Schedule) As Integer Implements System.IComparable(Of Schedule).CompareTo
        Return other.scheduleDate.CompareTo(Me.scheduleDate)
    End Function

    Public Function compareTo1(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        Return Me.compareTo(obj)
    End Function

    Public Overrides Function toString() As String
        Return IIf(_scheduleDate.Equals(LIMIT_DATE), "* Défaut *", DateFormat.getTextDate(_scheduleDate))
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noSchedule
        End Get
    End Property

    Private Function createReport() As Report
        Dim tpFilter As New FilteringComposite
        Dim userFilter As New FilteringUser
        userFilter.noUser = Me.noUser
        userFilter.user = If(Me.noUser = 0, "Clinique", UsersManager.getInstance.getUser(Me.noUser).toString())
        tpFilter.add(userFilter)
        Dim dateFilter As New FilteringFromTo
        dateFilter.firstDate = Me.scheduleDate
        dateFilter.firstDate = dateFilter.firstDate.AddDays(dateFilter.firstDate.DayOfWeek * -1)
        dateFilter.secondDate = dateFilter.firstDate.AddDays(6)
        tpFilter.add(dateFilter)

        Dim myRapport As Report = ReportsManager.getInstance.createReport("Semaine d'un horaire", tpFilter)
        Return myRapport
    End Function

    Public Sub print(Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False) Implements IPrintable.print
        curRapport = createReport()

        curRapport.print(promptUser, waitForSpooling)
    End Sub

    Public Sub printOptions() Implements IPrintable.printOptions

    End Sub

    Public Sub printPreview() Implements IPrintable.printPreview

    End Sub
End Class
