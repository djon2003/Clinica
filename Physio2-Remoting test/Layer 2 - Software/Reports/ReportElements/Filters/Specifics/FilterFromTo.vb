Public Class FilterFromTo
    Inherits BasicFilter

    Private _WholeWeek As Boolean = False
    Private _OnlyOneDay As Boolean = False
    Private _ShowTime As Boolean = False
    Private _AcceptNull As Boolean = False
    Private _SwitchTitles As Boolean = False
    Private _CurrentReturn As DateSelectorReturn
    Private Const dateDepuis As String = "Date depuis..."
    Private Const dateJusqua As String = "Date jusqu'à..."

#Region "Properties"
    Public Property wholeWeek() As Boolean
        Get
            Return _WholeWeek
        End Get
        Set(ByVal value As Boolean)
            _WholeWeek = value
        End Set
    End Property

    Public Property onlyOneDay() As Boolean
        Get
            Return _OnlyOneDay
        End Get
        Set(ByVal value As Boolean)
            _OnlyOneDay = value
        End Set
    End Property

    Public Property showTime() As Boolean
        Get
            Return _ShowTime
        End Get
        Set(ByVal value As Boolean)
            _ShowTime = value
        End Set
    End Property

    Public Property acceptNull() As Boolean
        Get
            Return _AcceptNull
        End Get
        Set(ByVal value As Boolean)
            _AcceptNull = value
        End Set
    End Property

    Public Property switchTitles() As Boolean
        Get
            Return _SwitchTitles
        End Get
        Set(ByVal value As Boolean)
            _SwitchTitles = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As DateSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Public Sub adjustReturn()
        'TODO : This method shall be private and called upon changing a property of _CurrentReturn (so properties shall be created)

        If Me.tableDotField = "" Then Exit Sub

        Dim mySecondDate As Date = _CurrentReturn.secondDate.AddDays(1)
        If Not _CurrentReturn.firstDate.Equals(LIMIT_DATE) AndAlso Not _CurrentReturn.secondDate.Equals(LIMIT_DATE) Then
            _CurrentReturn.filtrageTexte = "Du " & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD) & " au " & DateFormat.getTextDate(_CurrentReturn.secondDate, DateFormat.TextDateOptions.YYYYMMDD)

            _CurrentReturn.whereStr = "(" & Me.tableDotField & " >='" & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD) & "' AND " & Me.tableDotField & " <'" & DateFormat.getTextDate(mySecondDate, DateFormat.TextDateOptions.YYYYMMDD) & "')"
        ElseIf Not _CurrentReturn.firstDate.Equals(LIMIT_DATE) AndAlso _CurrentReturn.secondDate.Equals(LIMIT_DATE) Then
            If switchTitles Then
                _CurrentReturn.filtrageTexte = dateJusqua.Substring(0, dateJusqua.Length - 3)
            Else
                _CurrentReturn.filtrageTexte = dateDepuis.Substring(0, dateDepuis.Length - 3)
            End If
            _CurrentReturn.filtrageTexte &= " " & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD)

            _CurrentReturn.whereStr = "(" & Me.tableDotField & " >='" & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD) & "')"
        ElseIf _CurrentReturn.firstDate.Equals(LIMIT_DATE) AndAlso Not _CurrentReturn.secondDate.Equals(LIMIT_DATE) Then
            If switchTitles Then
                _CurrentReturn.filtrageTexte = dateDepuis.Substring(0, dateDepuis.Length - 3)
            Else
                _CurrentReturn.filtrageTexte = dateJusqua.Substring(0, dateJusqua.Length - 3)
            End If
            _CurrentReturn.filtrageTexte &= " " & DateFormat.getTextDate(_CurrentReturn.secondDate, DateFormat.TextDateOptions.YYYYMMDD)

            _CurrentReturn.whereStr = "(" & Me.tableDotField & " <'" & DateFormat.getTextDate(mySecondDate, DateFormat.TextDateOptions.YYYYMMDD) & "')"
        End If

        If acceptNull And _CurrentReturn.whereStr <> "" Then _CurrentReturn.whereStr = "(" & Me.tableDotField & " IS NULL OR " & _CurrentReturn.whereStr & ")"
        If _CurrentReturn.whereStr <> "" Then _CurrentReturn.whereStr = "(" & _CurrentReturn.whereStr & ")"
        If _CurrentReturn.filtrageTexte = "" Then _CurrentReturn.filtrageTexte = "Date : Toutes"

        _CurrentReturn.filtrageTexte = "<tr><td colspan=3>" & _CurrentReturn.filtrageTexte & "</td></tr>"
    End Sub

    Private Function chooseFromTo(ByVal whereDateField As String, Optional ByVal showTime As Boolean = False, Optional ByVal selectWholeWeek As Boolean = False) As DateSelectorReturn
        Dim myReturn As New DateSelectorReturn

        If whereDateField = "" Then Return myReturn

        Dim myDateChoice As New DateChoice
        'Dim FirstDate, SecondDate, TempDate(), FirstTime, secondTime As String
        'FirstTime = "" : secondTime = ""
        Dim MyFirstDate As Date = LIMIT_DATE, mySecondDate As Date = LIMIT_DATE

        If showTime And selectWholeWeek Then
            myDateChoice.Text = "Semaine et heure de départ"
        ElseIf showTime And onlyOneDay Then
            myDateChoice.Text = "Date et heure de départ"
        Else
            If switchTitles Then
                myDateChoice.Text = dateJusqua
            Else
                myDateChoice.Text = dateDepuis
            End If
        End If
        Dim dateChosen As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Now.Year + 1, , , showTime, "06:00", , , , , , , Date.Today.Date, selectWholeWeek)

        If dateChosen.Count = 0 AndAlso (selectWholeWeek OrElse onlyOneDay) Then
            myReturn.canceling = True
            Return myReturn
        End If

        If dateChosen.Count <> 0 Then
            MyFirstDate = dateChosen(0)
            myReturn.firstDate = MyFirstDate
        End If

        If (selectWholeWeek Or onlyOneDay) Then
            mySecondDate = MyFirstDate.Date
            If showTime Then
                Dim myMultiChoice As New multichoice
                Dim secondTime As String = myMultiChoice.GetChoice("Veuillez sélectionner l'heure de fin d'une journée", "6:00§6:15§6:30§6:45§7:00§7:15§7:30§7:45§8:00§8:15§8:30§8:45§9:00§9:15§9:30§9:45§10:00§10:15§10:30§10:45§11:00§11:15§11:30§11:45§12:00§12:15§12:30§12:45§13:00§13:15§13:30§13:45§14:00§14:15§14:30§14:45§15:00§15:15§15:30§15:45§16:00§16:15§16:30§16:45§17:00§17:15§17:30§17:45§18:00§18:15§18:30§18:45§19:00§19:15§19:30§19:45§20:00§20:15§20:30§20:45§21:00§21:15§21:30§21:45§22:00§22:15§22:30§22:45§23:00§23:15§23:30§23:45", , "§", False, "23:45")
                If secondTime = "" Or secondTime.ToUpper.StartsWith("ERROR") Then secondTime = "23:45"

                Dim sSecondTime() As String = secondTime.Split(New Char() {":"})
                mySecondDate = New Date(mySecondDate.Year, mySecondDate.Month, mySecondDate.Day, sSecondTime(0), sSecondTime(1), 0)
            End If

            If selectWholeWeek Then MyFirstDate = MyFirstDate.AddDays(-6)
            myReturn.firstDate = MyFirstDate
            If onlyOneDay Then myReturn.secondDate = mySecondDate
        Else
            myDateChoice = New DateChoice
            If switchTitles Then
                myDateChoice.Text = dateDepuis
            Else
                myDateChoice.Text = dateJusqua
            End If
            dateChosen = myDateChoice.choose(firstUsageDate.Year, Date.Now.Year + 1, , , showTime, "06:00", , True, MyFirstDate, , , , If(MyFirstDate.Equals(LIMIT_DATE), Date.Today, MyFirstDate), selectWholeWeek)

            If dateChosen.Count <> 0 Then mySecondDate = dateChosen(0)
        End If

        myReturn.secondDate = mySecondDate

        _CurrentReturn = myReturn
        adjustReturn()

        Return myReturn
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Dim myDates As DateSelectorReturn = Me.chooseFromTo(Me.tableDotField, Me.showTime, Me.wholeWeek)
        Return New FilterResult(myDates, Me.filterOnReportParts, myDates.canceling)
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        With CType(filtered, FilteringFromTo)
            _CurrentReturn = .currentReturn
            Dim filtrageTexteChanged As Boolean = False
            If _CurrentReturn.firstDate.Year = 9999 Then
                _CurrentReturn.filtrageTexte = "Date par défaut"
                _CurrentReturn.whereStr = "(" & tableDotField & " is null)"
                filtrageTexteChanged = True
            ElseIf Not _CurrentReturn.firstDate.Equals(LIMIT_DATE) And Not _CurrentReturn.secondDate.Equals(LIMIT_DATE) Then
                _CurrentReturn.filtrageTexte = "Du " & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD) & " au " & DateFormat.getTextDate(_CurrentReturn.secondDate, DateFormat.TextDateOptions.YYYYMMDD)

                _CurrentReturn.whereStr = "(" & Me.tableDotField & " >='" & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD) & "' AND " & tableDotField & " <'" & DateFormat.getTextDate(_CurrentReturn.secondDate.AddDays(1), DateFormat.TextDateOptions.YYYYMMDD) & "')"
                filtrageTexteChanged = True
            ElseIf Not _CurrentReturn.firstDate.Equals(LIMIT_DATE) And _CurrentReturn.secondDate.Equals(LIMIT_DATE) Then
                If switchTitles Then
                    _CurrentReturn.filtrageTexte = dateJusqua.Substring(0, dateJusqua.Length - 3)
                Else
                    _CurrentReturn.filtrageTexte = dateDepuis.Substring(0, dateDepuis.Length - 3)
                End If
                _CurrentReturn.filtrageTexte &= " " & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD)

                _CurrentReturn.whereStr = "(" & tableDotField & " >='" & DateFormat.getTextDate(_CurrentReturn.firstDate, DateFormat.TextDateOptions.YYYYMMDD) & "')"
                filtrageTexteChanged = True
            ElseIf _CurrentReturn.firstDate.Equals(LIMIT_DATE) And Not _CurrentReturn.secondDate.Equals(LIMIT_DATE) Then
                If switchTitles Then
                    _CurrentReturn.filtrageTexte = dateDepuis.Substring(0, dateDepuis.Length - 3)
                Else
                    _CurrentReturn.filtrageTexte = dateJusqua.Substring(0, dateJusqua.Length - 3)
                End If
                _CurrentReturn.filtrageTexte &= " " & DateFormat.getTextDate(_CurrentReturn.secondDate, DateFormat.TextDateOptions.YYYYMMDD)

                _CurrentReturn.whereStr = "(" & tableDotField & " <'" & DateFormat.getTextDate(_CurrentReturn.secondDate.AddDays(1), DateFormat.TextDateOptions.YYYYMMDD) & "')"
                filtrageTexteChanged = True
            End If

            If filtrageTexteChanged Then _CurrentReturn.filtrageTexte = "<tr><td colspan=3>" & _CurrentReturn.filtrageTexte & "</td></tr>"

            If acceptNull And _CurrentReturn.whereStr <> "" Then _CurrentReturn.whereStr = "(" & tableDotField & " IS NULL OR " & _CurrentReturn.whereStr & ")"
            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "WholeWeek"
                    wholeWeek = myKey.Value
                Case "OnlyOneDay"
                    onlyOneDay = myKey.Value
                Case "ShowTime"
                    showTime = myKey.Value
                Case "SwitchTitles"
                    switchTitles = myKey.Value
                Case "AcceptNull"
                    acceptNull = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
