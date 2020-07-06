Public Class FilterMonth
    Inherits BasicFilter

    Private _All As Boolean
    Private _Subquery As String = ""
    Private _sqlWhere As String = ""
    Private _CurrentReturn As MonthSelectorReturn

#Region "Properties"
    Public Property all() As Boolean
        Get
            Return _All
        End Get
        Set(ByVal value As Boolean)
            _All = value
        End Set
    End Property

    Public Property subquery() As String
        Get
            Return _Subquery
        End Get
        Set(ByVal value As String)
            _Subquery = value
        End Set
    End Property

    Public Property sqlWhere() As String
        Get
            Return _sqlWhere
        End Get
        Set(ByVal value As String)
            _sqlWhere = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As MonthSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Private Function chooseMonth(ByVal whereDateField As String) As MonthSelectorReturn
        Dim myReturn As New MonthSelectorReturn

        If whereDateField = "" Then Return myReturn

        Dim myMultiChoice As New multichoice
        Dim monthsLine As String = "Janvier§Février§Mars§Avril§Mai§Juin§Juillet§Août§Septembre§Octobre§Novembre§Décembre"
        Dim adjustForMonth As Byte = 0
        Dim adjustForYear As Byte = 0
        If all Then monthsLine = "* Tous *§" & monthsLine : adjustForMonth = 1
        Dim sMonthsLine() As String = monthsLine.Split(New Char() {"§"})
        Dim myMonthChoosed As Integer = myMultiChoice.GetChoice("Sélectionner un mois", monthsLine, "INDEX", "§", False, sMonthsLine(Date.Now.Month - 1 + adjustForMonth))
        If myMonthChoosed < 0 Then myReturn.canceling = True : Return myReturn

        myMultiChoice = New multichoice()
        Dim yearsLine As String = ""
        Dim i As Integer
        For i = firstUsageDate.Year To Date.Now.Year
            yearsLine &= "§" & i
        Next i
        yearsLine = yearsLine.Substring(1)
        If all Then yearsLine = "* Toutes *§" & yearsLine : adjustForYear = 1
        Dim sYearsLine() As String = yearsLine.Split(New Char() {"§"})
        Dim myYearChoosed As String = myMultiChoice.GetChoice("Sélectionner une année", yearsLine, , "§", False, Date.Now.Year.ToString)
        If myYearChoosed = "" Or myYearChoosed.StartsWith("ERROR") Then myReturn.canceling = True : Return myReturn

        If myYearChoosed.StartsWith("*") Then
            myReturn.filtrageTexte = "<tr><td>Date</td><td> : </td><td>Toutes</td></tr>"
        Else
            Dim curMonth As Byte = 1
            If Not (all And myMonthChoosed = 0) Then
                myReturn.month = myMonthChoosed + 1 - adjustForMonth
                myReturn.monthName = sMonthsLine(myMonthChoosed)
                curMonth = myReturn.month
            End If
            myReturn.year = myYearChoosed

            Dim MyFirstDate, mySecondDate As Date
            MyFirstDate = myReturn.year & "/" & curMonth & "/01"
            If Not (all And myMonthChoosed = 0) Then
                mySecondDate = MyFirstDate.AddMonths(1)
            Else
                mySecondDate = MyFirstDate.AddYears(1)
            End If
            myReturn.filtrageTexte = "<tr><td colspan=3>Du " & DateFormat.getTextDate(MyFirstDate, DateFormat.TextDateOptions.YYYYMMDD) & " au " & DateFormat.getTextDate(mySecondDate.AddDays(-1), DateFormat.TextDateOptions.YYYYMMDD) & "</td></tr>"
            myReturn.whereStr = "(" & whereDateField & " >='" & DateFormat.getTextDate(MyFirstDate, DateFormat.TextDateOptions.YYYYMMDD) & "' AND " & whereDateField & " <'" & DateFormat.getTextDate(mySecondDate, DateFormat.TextDateOptions.YYYYMMDD) & "')"
            If subquery <> "" Then
                If sqlWhere <> "" Then myReturn.whereStr &= " AND " & sqlWhere
                myReturn.whereStr = subquery.Replace("WHEREGEN", " WHERE " & myReturn.whereStr & " ")
            End If
        End If

        Return myReturn
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Dim myMonth As MonthSelectorReturn = Me.chooseMonth(Me.tableDotField)
        _CurrentReturn = myMonth
        Return New FilterResult(myMonth, Me.filterOnReportParts, myMonth.canceling)
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        With CType(filtered, FilteringMonth)
            _CurrentReturn = .currentReturn
            Dim MyFirstDate, mySecondDate As Date
            MyFirstDate = _CurrentReturn.year & "/" & _CurrentReturn.month & "/01"
            mySecondDate = MyFirstDate.AddMonths(1)
            _CurrentReturn.filtrageTexte = "<tr><td colspan=3>Du " & DateFormat.getTextDate(MyFirstDate, DateFormat.TextDateOptions.YYYYMMDD) & " au " & DateFormat.getTextDate(mySecondDate.AddDays(-1), DateFormat.TextDateOptions.YYYYMMDD) & "</td></tr>"
            _CurrentReturn.whereStr = "(" & tableDotField & " >='" & DateFormat.getTextDate(MyFirstDate, DateFormat.TextDateOptions.YYYYMMDD) & "' AND " & tableDotField & " <'" & DateFormat.getTextDate(mySecondDate, DateFormat.TextDateOptions.YYYYMMDD) & "')"
            If subquery <> "" Then
                If sqlWhere <> "" Then _CurrentReturn.whereStr &= " AND " & sqlWhere
                _CurrentReturn.whereStr = subquery.Replace("WHEREGEN", " WHERE " & _CurrentReturn.whereStr & " ")
            End If

            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "All"
                    all = myKey.Value
                Case "Subquery"
                    subquery = myKey.Value
                Case "SQLWhere"
                    sqlWhere = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
