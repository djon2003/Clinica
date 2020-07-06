Public Class SpecialDate
    Inherits DBItemableBase

    Public Enum SpecialDateMethod
        MonthDaySpecific = 0
        MonthSpecificOnDate = 1
        DateOnMonthDaySpecific = 2
        DateRelative = 3
        CodeVBNet = 4
    End Enum

    Public Enum SpecialDatePosition
        First = 0
        Second = 1
        Third = 2
        Fourth = 3
        Fifth = 4
        Last = 5
    End Enum

    Public Enum SpecialDateRelative
        Before = 0
        After = 1
    End Enum

    Private _NoSpecialDate As Integer
    Private _Nom As String = ""
    Private _MaxYear As Integer = 0
    Private _BaseDay4 As Integer = 0, _NbDays4 As Integer = 0
    Private _Method As SpecialDateMethod = SpecialDateMethod.MonthDaySpecific
    Private _month1, _month2, _month3, _Jour1, _Jour3, _Journee2, _Journee3 As Byte
    Private _Relative3 As SpecialDateRelative = SpecialDateRelative.Before
    Private _Relative4 As SpecialDateRelative = SpecialDateRelative.Before
    Private _Position2 As SpecialDatePosition
    Private _CodeVBNet As String = ""
    Private curMethodEval As MethodEvaluator

    Private dateCaching As New Hashtable


#Region "Properties"
    Public Property method() As SpecialDateMethod
        Get
            Return _Method
        End Get
        Set(ByVal value As SpecialDateMethod)
            _Method = value
        End Set
    End Property

    Public Property nom() As String
        Get
            Return _Nom
        End Get
        Set(ByVal value As String)
            _Nom = value
        End Set
    End Property

    Public Property codeVBNet() As String
        Get
            Return _CodeVBNet
        End Get
        Set(ByVal value As String)
            Me.curMethodEval = Nothing
            If value <> "" Then
                Dim curCodeProvider As New cVBEvalProvider
                Me.curMethodEval = curCodeProvider.getMethodEvaluator(value.Replace("\n", vbCrLf))
                If Me.curMethodEval Is Nothing Then
                    Dim errors As String = ""
                    For i As Integer = 0 To curCodeProvider.compilerErrors.Count - 1
                        errors &= vbCrLf & vbCrLf & "Erreur #" & (i + 1).ToString & vbCrLf & curCodeProvider.compilerErrors(i).ErrorNumber & "-" & curCodeProvider.compilerErrors(i).ErrorText & " à la ligne/colonne (" & (curCodeProvider.compilerErrors(i).Line - curCodeProvider.nbLinesAboveCode - 1).ToString & "/" & curCodeProvider.compilerErrors(i).Column & ")"
                    Next i
                    MessageBox.Show("Le code VB.NET contient des erreurs de compilation. Cette journée spéciale ne fonctionnera pas tant que ces erreurs existes." & errors, "Erreur de compilation")
                End If
            End If

            _CodeVBNet = value
        End Set
    End Property

    Public ReadOnly Property noSpecialDate() As Integer
        Get
            Return _NoSpecialDate
        End Get
    End Property

    Public Property position2() As SpecialDatePosition
        Get
            Return _Position2
        End Get
        Set(ByVal value As SpecialDatePosition)
            _Position2 = value
        End Set
    End Property

    Public Property relative3() As SpecialDateRelative
        Get
            Return _Relative3
        End Get
        Set(ByVal value As SpecialDateRelative)
            _Relative3 = value
        End Set
    End Property

    Public Property relative4() As SpecialDateRelative
        Get
            Return _Relative4
        End Get
        Set(ByVal value As SpecialDateRelative)
            _Relative4 = value
        End Set
    End Property

    Public Property month1() As Byte
        Get
            Return _month1
        End Get
        Set(ByVal value As Byte)
            _month1 = value
        End Set
    End Property

    Public Property month2() As Byte
        Get
            Return _month2
        End Get
        Set(ByVal value As Byte)
            _month2 = value
        End Set
    End Property

    Public Property month3() As Byte
        Get
            Return _month3
        End Get
        Set(ByVal value As Byte)
            _month3 = value
        End Set
    End Property

    Public Property journee2() As Byte
        Get
            Return _Journee2
        End Get
        Set(ByVal value As Byte)
            _Journee2 = value
        End Set
    End Property

    Public Property journee3() As Byte
        Get
            Return _Journee3
        End Get
        Set(ByVal value As Byte)
            _Journee3 = value
        End Set
    End Property

    Public Property jour1() As Byte
        Get
            Return _Jour1
        End Get
        Set(ByVal value As Byte)
            _Jour1 = value
        End Set
    End Property

    Public Property jour3() As Byte
        Get
            Return _Jour3
        End Get
        Set(ByVal value As Byte)
            _Jour3 = value
        End Set
    End Property

    Public Property maxYear() As Integer
        Get
            Return _MaxYear
        End Get
        Set(ByVal value As Integer)
            _MaxYear = value
        End Set
    End Property

    Public Property nbDays4() As Integer
        Get
            Return _NbDays4
        End Get
        Set(ByVal value As Integer)
            _NbDays4 = value
        End Set
    End Property

    Public Property baseDay4() As Integer
        Get
            Return _BaseDay4
        End Get
        Set(ByVal value As Integer)
            _BaseDay4 = value
        End Set
    End Property
#End Region

    Public Sub New()
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData
        _NoSpecialDate = curData("NoSpecialDate")
        _Nom = curData("Nom")
        _MaxYear = curData("MaxYear")
        _Method = curData("Method")
        _month1 = curData("Mois1")
        _month2 = curData("Mois2")
        _month3 = curData("Mois3")
        _Jour1 = curData("Jour1")
        _Jour3 = curData("Jour3")
        _Journee2 = curData("Journee2")
        _Journee3 = curData("Journee3")
        _Relative3 = curData("Relative3")
        _Relative4 = IIf(curData("Relative4") Is DBNull.Value, 0, curData("Relative4"))
        _BaseDay4 = IIf(curData("BaseDay4") Is DBNull.Value, 0, curData("BaseDay4"))
        _NbDays4 = IIf(curData("NbDays4") Is DBNull.Value, 0, curData("NbDays4"))
        _Position2 = curData("Position2")
        _CodeVBNet = curData("CodeVBNet")

        If _CodeVBNet <> "" Then
            Dim curCodeProvider As New cVBEvalProvider
            Me.curMethodEval = curCodeProvider.getMethodEvaluator(Me.codeVBNet.Replace("\n", vbCrLf))
        End If
    End Sub

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("SpecialDates", "NoSpecialDate", _NoSpecialDate, False)
        onDeleted()
        If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("SpecialDates()")
    End Sub

    Public Overrides Sub saveData()
        If _NoSpecialDate = 0 Then
            DBLinker.getInstance.writeDB("SpecialDates", "Method,Nom,MaxYear,Mois1,Mois2,Mois3,Journee2,Journee3,Jour1,Jour3,Relative3,Relative4,NbDays4,BaseDay4,Position2,CodeVBNet", CInt(_Method) & ",'" & _Nom.Replace("'", "''") & "'," & _MaxYear & "," & _month1 & "," & _month2 & "," & _month3 & "," & _Journee2 & "," & _Journee3 & "," & jour1 & "," & jour3 & "," & CInt(_Relative3) & "," & CInt(_Relative4) & "," & _BaseDay4 & "," & _NbDays4 & "," & CInt(_Position2) & ",'" & _CodeVBNet.Replace("'", "''") & "'", , , , _NoSpecialDate)
        Else
            DBLinker.getInstance.updateDB("SpecialDates", "Method=" & CInt(_Method) & ",Nom='" & _Nom.Replace("'", "''") & "',MaxYear=" & _MaxYear & ",Mois1=" & _month1 & ",Mois2=" & _month2 & ",Mois3=" & _month3 & ",Journee2=" & _Journee2 & ",Journee3=" & _Journee3 & ",Jour1=" & jour1 & ",Jour3=" & jour3 & ",Relative3=" & CInt(_Relative3) & ",Relative4=" & CInt(_Relative4) & ",BaseDay4=" & _BaseDay4 & ",NbDays4=" & _NbDays4 & ",Position2=" & CInt(_Position2) & ",CodeVBNet='" & _CodeVBNet.Replace("'", "''") & "'", "NoSpecialDate", _NoSpecialDate, False)
            onDataChanged()
            If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("SpecialDates()")
        End If
    End Sub

    Public Function getDate(ByVal year As Integer) As Date
        If Me.dateCaching.ContainsKey(year) Then Return Me.dateCaching(year)

        If _MaxYear <> 0 AndAlso year > _MaxYear Then Me.dateCaching.Add(year, LIMIT_DATE) : Return LIMIT_DATE

        Select Case method
            Case SpecialDateMethod.MonthDaySpecific
                Me.dateCaching.Add(year, New Date(year, month1, jour1))
                Return New Date(year, month1, jour1)

            Case SpecialDateMethod.MonthSpecificOnDate
                Dim curDate As Date = New Date(year, month2, 1)
                If curDate.DayOfWeek <> Me.journee2 Then
                    Do
                        curDate = curDate.AddDays(1)
                    Loop Until curDate.DayOfWeek = Me.journee2
                End If

                Dim n As Byte = 0
                Dim stopCondition As Date = curDate.AddMonths(1)
                Do
                    If position2 = n Then Me.dateCaching.Add(year, curDate) : Return curDate
                    curDate = curDate.AddDays(7)
                    n += 1
                Loop Until curDate.Month = stopCondition.Month And curDate.Year = stopCondition.Year
                curDate = curDate.AddDays(-7)
                If position2 = SpecialDatePosition.Last Then Me.dateCaching.Add(year, curDate) : Return curDate

            Case SpecialDateMethod.DateOnMonthDaySpecific
                Dim sdDate As Date = New Date(year, Me.month3, Me.jour3)
                Dim stepping As Integer = 1
                If Me.relative3 = SpecialDateRelative.Before Then stepping = -1
                Do
                    sdDate = sdDate.AddDays(stepping)
                Loop Until sdDate.DayOfWeek = Me.journee3

                Me.dateCaching.Add(year, sdDate)
                Return sdDate

            Case SpecialDateMethod.DateRelative
                Dim relSD As SpecialDate = SpecialDatesManager.getInstance.getItemable(Me.baseDay4)
                If relSD Is Nothing Then Me.dateCaching.Add(year, LIMIT_DATE) : Return LIMIT_DATE

                Dim multiplier As Integer = 1
                If Me.relative4 = SpecialDateRelative.Before Then multiplier = -1

                Dim sdDate As Date = relSD.getDate(year).AddDays(Me.nbDays4 * multiplier)
                Me.dateCaching.Add(year, sdDate)
                Return sdDate

            Case SpecialDateMethod.CodeVBNet
                If Me.curMethodEval Is Nothing Then Me.dateCaching.Add(year, LIMIT_DATE) : Return LIMIT_DATE

                Dim answer As Date = Me.curMethodEval.eval(New Object() {New Date(year, 1, 1)})
                Me.dateCaching.Add(year, answer)
                Return answer
        End Select

        Return LIMIT_DATE
    End Function

    Public Function isDateSpecial(ByVal dateToTest As Date) As Boolean
        Dim sdDate As Date = Me.getDate(dateToTest.Year)

        If sdDate.Equals(LIMIT_DATE) Then Return False
        If sdDate.Equals(dateToTest) Then Return True

        Return False
    End Function

    Public Overrides Function toString() As String
        Return _Nom
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noSpecialDate
        End Get
    End Property
End Class
