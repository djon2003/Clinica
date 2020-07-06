Public Class Schedule
    Inherits DBItemableBase
    Implements ICloneable, IComparable(Of Schedule), IComparable


    Private Const nbMinutesInADay As Short = 1440
    Private monday(nbMinutesInADay) As Boolean
    Private mardi(nbMinutesInADay) As Boolean
    Private mercredi(nbMinutesInADay) As Boolean
    Private jeudi(nbMinutesInADay) As Boolean
    Private vendredi(nbMinutesInADay) As Boolean
    Private samedi(nbMinutesInADay) As Boolean
    Private dimanche(nbMinutesInADay) As Boolean
    Private _noSchedule As Integer
    Private _intervalMinutes As Integer = 15
    Private _noUser As Integer
    Private _scheduleDate As Date = limitDate

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
        If dateHoraire.Equals(limitDate) = False Then _scheduleDate = dateHoraire.AddDays(dateHoraire.DayOfWeek * -1)
    End Sub

    Public Sub New(ByVal baseHoraire As Schedule, ByVal dateHoraire As Date)
        Me._noUser = baseHoraire.noUser
        If dateHoraire.Equals(limitDate) = False Then _scheduleDate = dateHoraire.AddDays(dateHoraire.DayOfWeek * -1)
        Me.dimanche = baseHoraire.dimanche.Clone()
        Me.monday = baseHoraire.monday.Clone()
        Me.mardi = baseHoraire.mardi.Clone()
        Me.mercredi = baseHoraire.mercredi.Clone()
        Me.jeudi = baseHoraire.jeudi.Clone()
        Me.vendredi = baseHoraire.vendredi.Clone()
        Me.samedi = baseHoraire.samedi.Clone()
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
            Return _scheduleDate.Equals(limitDate)
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

    Public Function findTime(ByVal whichTime As WTType, ByVal noWeekDay As Byte) As String
        Dim checkDate As Date = Date.Today.AddDays(Date.Today.DayOfWeek * -1 + noWeekDay)
        Return findTime(whichTime, checkDate)
    End Function

    Public Function findTime(ByVal whichTime As WTType, ByVal checkDay As Date) As String
        Dim Stepping, i As Short
        Dim FMax, fMin As String
        fMin = 0
        FMax = nbMinutesInADay
        Stepping = _intervalMinutes
        checkDay = checkDay.Date
        If whichTime = WTType.Last Then
            fMin = FMax
            FMax = 0
            Stepping = _intervalMinutes * -1
            checkDay = checkDay.AddDays(1).AddMinutes(Stepping)
        End If
        For i = fMin To FMax Step Stepping
            If isOpened(checkDay) Then Return DateFormat.getTextDate(checkDay, DateFormat.TextDateOptions.ShortTime)

            checkDay = checkDay.AddMinutes(Stepping)
        Next i

        Return IIf(whichTime = WTType.Last, "00:00", "23:59")
    End Function

    Public Function totalMinutesByDay(ByVal myDay As DayOfWeek) As Integer
        Dim totalMinutes As Integer = 0

        For i As Integer = 0 To dimanche.GetUpperBound(0)
            Select Case myDay
                Case DayOfWeek.Sunday
                    If dimanche(i) = True Then totalMinutes += 1
                Case DayOfWeek.Monday
                    If monday(i) = True Then totalMinutes += 1
                Case DayOfWeek.Tuesday
                    If mardi(i) = True Then totalMinutes += 1
                Case DayOfWeek.Wednesday
                    If mercredi(i) = True Then totalMinutes += 1
                Case DayOfWeek.Thursday
                    If jeudi(i) = True Then totalMinutes += 1
                Case DayOfWeek.Friday
                    If vendredi(i) = True Then totalMinutes += 1
                Case DayOfWeek.Saturday
                    If samedi(i) = True Then totalMinutes += 1
            End Select
        Next i

        Return totalMinutes
    End Function

    Public Function isOpened(ByVal myDay As Date) As Boolean
        Select Case myDay.DayOfWeek
            Case DayOfWeek.Sunday
                Return dimanche(myDay.Hour * 60 + myDay.Minute)
            Case DayOfWeek.Monday
                Return monday(myDay.Hour * 60 + myDay.Minute)
            Case DayOfWeek.Tuesday
                Return mardi(myDay.Hour * 60 + myDay.Minute)
            Case DayOfWeek.Wednesday
                Return mercredi(myDay.Hour * 60 + myDay.Minute)
            Case DayOfWeek.Thursday
                Return jeudi(myDay.Hour * 60 + myDay.Minute)
            Case DayOfWeek.Friday
                Return vendredi(myDay.Hour * 60 + myDay.Minute)
            Case DayOfWeek.Saturday
                Return samedi(myDay.Hour * 60 + myDay.Minute)
        End Select

        Return False
    End Function

    Public Sub setOpened(ByVal myDay As Date, ByVal isOpened As Boolean)
        Dim index As Integer = Math.Floor((myDay.Hour * 60 + myDay.Minute) / _intervalMinutes) * _intervalMinutes

        For j As Integer = 1 To _intervalMinutes
            Select Case myDay.DayOfWeek
                Case DayOfWeek.Sunday
                    dimanche(index) = isOpened
                Case DayOfWeek.Monday
                    monday(index) = isOpened
                Case DayOfWeek.Tuesday
                    mardi(index) = isOpened
                Case DayOfWeek.Wednesday
                    mercredi(index) = isOpened
                Case DayOfWeek.Thursday
                    jeudi(index) = isOpened
                Case DayOfWeek.Friday
                    vendredi(index) = isOpened
                Case DayOfWeek.Saturday
                    samedi(index) = isOpened
            End Select
            index += 1
        Next j
    End Sub

    Public Function clone() As Object Implements System.ICloneable.Clone
        Dim newHoraire As New Schedule(_noUser, _scheduleDate)
        newHoraire.dimanche = Me.dimanche.Clone
        newHoraire.monday = Me.monday.Clone
        newHoraire.mardi = Me.mardi.Clone
        newHoraire.mercredi = Me.mercredi.Clone
        newHoraire.jeudi = Me.jeudi.Clone
        newHoraire.vendredi = Me.vendredi.Clone
        newHoraire.samedi = Me.samedi.Clone
        newHoraire._intervalMinutes = Me._intervalMinutes

        Return newHoraire
    End Function

    Public Overrides Sub delete()
        'DBLinker.getInstance.delDB("Horaires", "NoHoraire", noSchedule, False)
        'onDeleted()
        'If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendupdate("Horaire-Del(" & Me.noSchedule & "," & Me.noUser & "," & DateFormat.getTextDate(Me.scheduleDate) & ")")
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData
        _noSchedule = curData("NoHoraire")
        If curData("NoUser").Equals(DBNull.Value) = False Then _noUser = curData("NoUser")
        If curData("DateHoraire").Equals(DBNull.Value) = False Then _scheduleDate = curData("DateHoraire")
        copyArray(dimanche, curData("Dimanche"))
        copyArray(monday, curData("Lundi"))
        copyArray(mardi, curData("Mardi"))
        copyArray(mercredi, curData("Mercredi"))
        copyArray(jeudi, curData("Jeudi"))
        copyArray(vendredi, curData("Vendredi"))
        copyArray(samedi, curData("Samedi"))
        _intervalMinutes = curData("intervalMinutes")
    End Sub

    Public Function splitStr(ByVal chaine As String) As Char()
        Dim i As Integer
        Dim tempTab() As Char
        ReDim tempTab(chaine.Length - 1)

        For i = 0 To chaine.Length - 1
            tempTab(i) = chaine.Substring(i, 1)
        Next i

        Return tempTab
    End Function

    Public Sub copyArray(ByRef destinationArray() As Boolean, Optional ByVal stringToSplit As String = "", Optional ByVal originArray() As Boolean = Nothing)
        Dim i, arrayMax As Integer
        Dim MyTempArray(), myFalseChar As Char
        myFalseChar = "0"c

        If stringToSplit <> "" Then
            MyTempArray = splitStr(stringToSplit)
            arrayMax = UBound(MyTempArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                If MyTempArray(i) = myFalseChar Then
                    destinationArray(i) = False
                Else
                    destinationArray(i) = True
                End If
            Next i
        ElseIf Not originArray Is Nothing Then
            arrayMax = UBound(originArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                destinationArray(i) = originArray(i)
            Next i
        End If
    End Sub

    Public Function createStringFromArray(ByVal myDay() As Boolean) As String

        Dim myString As String = ""
        For i As Integer = 0 To dimanche.GetUpperBound(0) Step 30
            Dim newInteger As Integer = 0
            For j As Integer = i + 29 To i Step -1
                Dim bit As Integer = If(myDay(j), 1, 0)
                newInteger = (newInteger << 1) Or bit
            Next j
            myString &= "," & newInteger.ToString()
        Next

        myString = myString.Substring(1)

        Return myString
    End Function

    Public Overrides Sub saveData()
        If _noSchedule = 0 Then
            Dim lookForExistingHor() As String = DBLinker.getInstance.readOneDBField("Horaires", "NoHoraire", "WHERE (((NoUser)=" & IIf(_noUser = 0, "0 OR Horaires.NoUser IS NULL", _noUser) & ") AND (DateHoraire = " & IIf(_scheduleDate.Equals(limitDate), "null", "'" & DateFormat.getTextDate(_scheduleDate) & "'") & "));")
            If lookForExistingHor IsNot Nothing AndAlso lookForExistingHor.Length <> 0 Then _noSchedule = lookForExistingHor(0)
        End If

        Dim openedMinutes As String = createStringFromArray(dimanche) & "," & createStringFromArray(monday) & "," & createStringFromArray(mardi) & "," & createStringFromArray(mercredi) & "," & createStringFromArray(jeudi) & "," & createStringFromArray(vendredi) & "," & createStringFromArray(samedi)

        'Save/Insert Horaire
        If Me.noSchedule = 0 Then
            'DBLinker.getInstance.writeDB("Horaires", "NoUser,DateHoraire,Dimanche,Lundi,Mardi,Mercredi,Jeudi,Vendredi,Samedi,IntervalMinutes", _
            'IIf(_noUser = 0, "null", _noUser) & "," & IIf(_scheduleDate.Equals(limitDate), "null", "'" & DateFormat.getTextDate(_scheduleDate) & "'") & ",'" & copyArrayToString(dimanche).Substring(0, 1440) & "','" & copyArrayToString(monday).Substring(0, 1440) & "','" & copyArrayToString(mardi).Substring(0, 1440) & "','" & copyArrayToString(mercredi).Substring(0, 1440) & "','" & copyArrayToString(jeudi).Substring(0, 1440) & "','" & copyArrayToString(vendredi).Substring(0, 1440) & "','" & copyArrayToString(samedi).Substring(0, 1440) & "'," & _intervalMinutes, , , , , , _noSchedule)

            'If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendupdate("Horaire-Add(" & Me.noSchedule & "," & Me.noUser & "," & DateFormat.getTextDate(Me.scheduleDate) & ")")
        Else
            DBLinker.getInstance.updateDB("Horaires", "OpenedMinutes='" & openedMinutes & "'", "noHoraire", _noSchedule, False)
            'If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendupdate("Horaire-Modif(" & Me.noSchedule & "," & Me.noUser & "," & DateFormat.getTextDate(Me.scheduleDate) & ")")
        End If

        
    End Sub

    Public Function compareTo(ByVal other As Schedule) As Integer Implements System.IComparable(Of Schedule).CompareTo
        Return other.scheduleDate.CompareTo(Me.scheduleDate)
    End Function

    Public Function compareTo1(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        Return Me.compareTo(obj)
    End Function

    Public Overrides Function toString() As String
        Return IIf(_scheduleDate.Equals(limitDate), "* Défaut *", DateFormat.getTextDate(_scheduleDate))
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noSchedule
        End Get
    End Property
End Class