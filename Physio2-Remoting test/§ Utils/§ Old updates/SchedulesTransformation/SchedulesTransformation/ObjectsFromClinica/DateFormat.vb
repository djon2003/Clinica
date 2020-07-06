Public Class DateFormat
    Private Sub New()
    End Sub

    Private Shared daysNames() As String = {"Dimanche", "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi"}
    Private Shared monthsNames() As String = {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"}


    Public Shared Function getDayName(ByVal dayIndex As Integer) As String
        Return daysNames(dayIndex)
    End Function

    Public Shared Function getMonthName(ByVal monthIndex As Integer) As String
        Return monthsNames(monthIndex)
    End Function

    Public Enum TextDateOptions
        YYYYMMDD = 0
        DDMMYYYY = 1
        MMDDYYYY = 2
        FullDayMonthNames = 3
        ShortDayMonthNames = 4
        ShortDayNameDDMMYY = 5
        ShortDayNameMMDDYY = 6
        ShortDayNameYYMMDD = 7
        FullTime = 8
        ShortTime = 9
        YYYYMMDDShortDayName = 10
        WeekDayName = 11
    End Enum


    Public Shared Function getSecondsInHours(ByVal seconds As Integer) As String
        Dim textHours As String = ""
        textHours = seconds Mod 60 & "s"
        seconds -= seconds Mod 60
        seconds /= 60
        textHours = seconds Mod 60 & "m " & textHours
        seconds -= seconds Mod 60
        seconds /= 60
        textHours = seconds & "h " & textHours

        Return textHours
    End Function

    Public Shared Function getTextDate(ByVal dateToConvert As Date, Optional ByVal options As DateFormat.TextDateOptions = DateFormat.TextDateOptions.YYYYMMDD, Optional ByVal shortSeparator As String = ".", Optional ByVal dateSeparator As String = "/") As String
        Dim MyYear, MyMonth, MyDay, MyHour, MyMinute, mySecond As String
        MyYear = dateToConvert.Year
        MyMonth = dateToConvert.Month
        MyDay = dateToConvert.Day
        MyHour = dateToConvert.Hour
        MyMinute = dateToConvert.Minute
        mySecond = dateToConvert.Second

        If MyMonth < 10 Then MyMonth = "0" & MyMonth
        If MyDay < 10 Then MyDay = "0" & MyDay
        If MyHour < 10 Then MyHour = "0" & MyHour
        If MyMinute < 10 Then MyMinute = "0" & MyMinute
        If mySecond < 10 Then mySecond = "0" & mySecond

        Select Case options
            Case DateFormat.TextDateOptions.YYYYMMDD
                Return MyYear & dateSeparator & MyMonth & dateSeparator & MyDay
            Case DateFormat.TextDateOptions.DDMMYYYY
                Return MyDay & dateSeparator & MyMonth & dateSeparator & MyYear
            Case DateFormat.TextDateOptions.MMDDYYYY
                Return MyMonth & dateSeparator & MyDay & dateSeparator & MyYear
            Case DateFormat.TextDateOptions.FullDayMonthNames
                If dateToConvert.Day = 1 Then MyDay = "1er"
                Return daysNames(dateToConvert.DayOfWeek) & " " & MyDay & " " & monthsNames(MyMonth - 1) & " " & MyYear
            Case DateFormat.TextDateOptions.ShortDayMonthNames
                Return daysNames(dateToConvert.DayOfWeek).Substring(0, 3) & shortSeparator & " " & MyDay & " " & monthsNames(MyMonth - 1).Substring(0, 3) & shortSeparator & " " & MyYear
            Case DateFormat.TextDateOptions.ShortDayNameYYMMDD
                Return daysNames(dateToConvert.DayOfWeek).Substring(0, 3) & shortSeparator & " " & MyYear.Substring(2, 2) & dateSeparator & MyMonth & dateSeparator & MyDay
            Case DateFormat.TextDateOptions.ShortDayNameDDMMYY
                Return daysNames(dateToConvert.DayOfWeek).Substring(0, 3) & shortSeparator & " " & MyDay & dateSeparator & MyMonth & dateSeparator & MyYear.Substring(2, 2)
            Case DateFormat.TextDateOptions.ShortDayNameMMDDYY
                Return daysNames(dateToConvert.DayOfWeek).Substring(0, 3) & shortSeparator & " " & MyMonth & dateSeparator & MyDay & dateSeparator & MyYear.Substring(2, 2)
            Case DateFormat.TextDateOptions.FullTime
                Return MyHour & ":" & MyMinute & ":" & mySecond
            Case DateFormat.TextDateOptions.ShortTime
                Return MyHour & ":" & MyMinute
            Case DateFormat.TextDateOptions.YYYYMMDDShortDayName
                Return MyYear & dateSeparator & MyMonth & dateSeparator & MyDay & " " & daysNames(dateToConvert.DayOfWeek).Substring(0, 3) & shortSeparator
            Case DateFormat.TextDateOptions.WeekDayName
                Return daysNames(dateToConvert.DayOfWeek)
        End Select

        'Return default
        Return MyYear & dateSeparator & MyMonth & dateSeparator & MyDay
    End Function
End Class
