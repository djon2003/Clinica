Public Class ReportBodyStats
    Inherits ReportBodyTable

    Private Const TOTAL_TYPE_AVERAGE As String = "AVG"

    Private myExtraColumnsSQL As String
    Private myExtraColumnsSQLWhere As String
    Private myTotalPosition As String = "COL"
    Private myShowInPourcent As Boolean = False
    Private _TotalsType As String

    Public Sub New(ByRef curReport As Report)
        MyBase.New(curReport)
        myExtraColumnsSQL = ""
        MyBase.isGrouped = False
    End Sub

#Region "Properties"
    Public Property totalsType() As String
        Get
            Return _TotalsType
        End Get
        Set(ByVal value As String)
            _TotalsType = value
        End Set
    End Property

    Public Property showInPourcent() As Boolean
        Get
            Return myShowInPourcent
        End Get
        Set(ByVal value As Boolean)
            myShowInPourcent = value
        End Set
    End Property

    Public Property totalPosition() As String
        Get
            Return myTotalPosition
        End Get
        Set(ByVal value As String)
            myTotalPosition = value
        End Set
    End Property

    Public Property extraColumnsSQL() As String
        Get
            Return myExtraColumnsSQL
        End Get
        Set(ByVal value As String)
            myExtraColumnsSQL = value
        End Set
    End Property

    Public Property extraColumnsSQLWhere() As String
        Get
            Return myExtraColumnsSQLWhere
        End Get
        Set(ByVal value As String)
            myExtraColumnsSQLWhere = value
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

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        Dim i, j As Integer
        Dim mySum As Double

        mySQLStatement = MyBase.transformSQLStatement(mySQLStatement)
        myExtraColumnsSQL = MyBase.transformSQLStatement(myExtraColumnsSQL)

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True
        Dim myDataSet As DataSet = DBLinker.getInstance.readDBForGrid(mySQLStatement, , , "Body1", , False)
        If myExtraColumnsSQL <> "" Then DBLinker.getInstance.readDBForGrid(myExtraColumnsSQL, , , "ExtraColumns", myDataSet, False)
        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        If myDataSet Is Nothing Then Exit Sub

        Dim myStatTable As New DataTable("Body")
        Dim myColumnsNames As New ArrayList
        Dim myFirstColumnData As New ArrayList
        Dim myOtherColumnsData As New Hashtable
        Dim myExtraColumnsData As New Hashtable
        Dim myTotalPerColumn As New Hashtable
        Dim MyNbColsByRow(), myNbRowsByCol() As Double

        Dim isTotalTypeAverage As Boolean = _TotalsType = TOTAL_TYPE_AVERAGE
        Dim mainExtraColumns As Integer = If(isTotalTypeAverage, 5, 3)
        Dim nbColumns As Integer = 0

        With myDataSet.Tables("Body1")
            'Ensure all columns needed by average exists in query
            If isTotalTypeAverage AndAlso .Columns.Count < 5 Then
                MessageBox.Show("Using 'TotalsType=" & TOTAL_TYPE_AVERAGE & "' shall have a minimum of 5 columns : first header column data, row header data, grid cell data calculated (average), numerator to calculate average, denominator to calculate average, [extra fields]", "Missing SQL fields", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'Add first column
            myStatTable.Columns.Add(.Columns(0).ColumnName)

            'Build others columns hashtable
            For i = 0 To .Rows.Count - 1
                If myColumnsNames.IndexOf(.Rows(i)(1)) < 0 Then myColumnsNames.Add(.Rows(i)(1)) : myTotalPerColumn.Add(.Rows(i)(1), 0)
                If myFirstColumnData.IndexOf(.Rows(i)(0)) < 0 Then myFirstColumnData.Add(.Rows(i)(0))

                'Manage extra columns that affects a whole row
                If myExtraColumnsData.ContainsKey(.Rows(i)(0) & "-" & mainExtraColumns) = False Then
                    For j = mainExtraColumns To .Columns.Count - 1
                        myExtraColumnsData.Add(.Rows(i)(0) & "-" & j, .Rows(i)(j))
                    Next
                End If

                Dim myCellData() As Object
                ReDim myCellData(If(isTotalTypeAverage, 2, 0))
                For j = 2 To mainExtraColumns - 1
                    myCellData(j - 2) = .Rows(i)(j)
                Next j

                myOtherColumnsData.Add(.Rows(i)(0) & "-" & .Rows(i)(1), myCellData)
            Next i
            myColumnsNames.Sort()

            'Build all columns based on second column of the query
            For i = 0 To myColumnsNames.Count - 1
                myStatTable.Columns.Add(myColumnsNames(i))
            Next i

            'Add Total Column (if required)
            If totalPosition.ToUpper = "COL" Or totalPosition.ToUpper = "BOTH" Then
                colsFormat.Add("Total", colsFormat(.Columns(2).ColumnName))
                myStatTable.Columns.Add("Total")
            End If

            'Add formats
            For i = 0 To myColumnsNames.Count - 1
                If colsFormat.ContainsKey(myColumnsNames(i)) = False Then colsFormat.Add(myColumnsNames(i), colsFormat(.Columns(2).ColumnName))
            Next i

            'Generate stats table
            Dim myRow() As Object
            ReDim myRow(myStatTable.Columns.Count - 1)
            ReDim MyNbColsByRow(myFirstColumnData.Count - 1)
            ReDim myNbRowsByCol(myStatTable.Columns.Count - 1)
            For i = 0 To myFirstColumnData.Count - 1
                myRow(0) = myFirstColumnData(i)
                mySum = 0
                'Data columns of a row
                For j = 0 To myColumnsNames.Count - 1
                    If myOtherColumnsData.ContainsKey(myFirstColumnData(i) & "-" & myColumnsNames(j)) Then
                        myRow(j + 1) = myOtherColumnsData(myFirstColumnData(i) & "-" & myColumnsNames(j))
                        mySum += myRow(j + 1)(If(isTotalTypeAverage, 1, 0))
                        'Data for denominator of averages totals
                        If isTotalTypeAverage AndAlso myRow(j + 1)(0) <> 0 Then
                            MyNbColsByRow(i) += myRow(j + 1)(2)
                            myNbRowsByCol(j + 1) += myRow(j + 1)(2)
                        End If
                        myTotalPerColumn(myColumnsNames(j)) += myRow(j + 1)(If(isTotalTypeAverage, 1, 0))
                    Else
                        myRow(j + 1) = 0
                    End If
                Next j

                'Change data as percentage
                'TODO : I don't think this works for totaltype average
                If myShowInPourcent And totalPosition.ToUpper = "COL" Then
                    Dim newCellText As String
                    For j = 1 To myRow.GetUpperBound(0) - 1
                        newCellText = If(TypeOf myRow(j) Is Array, myRow(j)(0), myRow(j))
                        newCellText = CType(newCellText, Double) / CType(mySum, Double) * 100
                        forceManaging(newCellText, True, "", False, True, False, True, ",§.", , , , , , , 2)
                        If TypeOf myRow(j) Is Array Then
                            myRow(j)(0) = newCellText
                        Else
                            myRow(j) = newCellText
                        End If
                    Next j

                    mySum = 100
                End If

                'Add total column if needed
                If totalPosition.ToUpper = "COL" Or totalPosition.ToUpper = "BOTH" Then
                    myTotalPerColumn("BIGBIGTOTAL") += mySum
                    If myShowInPourcent = False AndAlso isTotalTypeAverage AndAlso MyNbColsByRow(i) <> 0 Then mySum /= MyNbColsByRow(i)
                    myRow(myStatTable.Columns.Count - 1) = "CELLCSSCLASS:StatsTotalCell:" & mySum
                    myTotalPerColumn("BIGROWTOTAL") += mySum
                End If

                'Join all array cells
                For r As Integer = 0 To myRow.Length - 1
                    If (TypeOf myRow(r) Is Array) Then
                        Dim newCellValue As String = myRow(r)(0)
                        For rr As Integer = 1 To CType(myRow(r), Array).Length - 1
                            newCellValue &= EXTRA_CELL_VALUES & myRow(r)(rr)
                        Next
                        myRow(r) = newCellValue
                    End If
                Next r
                myStatTable.Rows.Add(myRow)
            Next i

            'Add total row if needed
            If totalPosition.ToUpper = "ROW" Or totalPosition.ToUpper = "BOTH" Then
                myRow(0) = "CELLCSSCLASS:StatsTotalCell:Total"
                For i = 0 To myColumnsNames.Count - 1
                    If myShowInPourcent Then
                        Dim newCellText As String
                        For j = 0 To myStatTable.Rows.Count - 1
                            newCellText = CType(myStatTable.Rows(j)(i + 1), Double) / CType(myTotalPerColumn(myColumnsNames(i)), Double) * 100
                            forceManaging(newCellText, True, "", False, True, False, True, ",§.", , , , , , , 2)
                            myStatTable.Rows(j)(i + 1) = newCellText
                        Next j
                        myRow(i + 1) = "CELLCSSCLASS:StatsTotalCell:100"
                    Else
                        If isTotalTypeAverage AndAlso myNbRowsByCol(i + 1) <> 0 Then myTotalPerColumn(myColumnsNames(i)) /= myNbRowsByCol(i + 1)
                        myRow(i + 1) = "CELLCSSCLASS:StatsTotalCell:" & myTotalPerColumn(myColumnsNames(i))
                        myTotalPerColumn("BIGCOLTOTAL") += myTotalPerColumn(myColumnsNames(i))
                    End If
                Next i

                If totalPosition.ToUpper = "BOTH" Then
                    Dim bigTotal As Double = myTotalPerColumn("BIGBIGTOTAL")
                    If isTotalTypeAverage Then
                        Dim nbTotal As Double = 0
                        For t As Integer = 0 To myNbRowsByCol.Length - 1
                            nbTotal += myNbRowsByCol(t)
                        Next t
                        bigTotal /= nbTotal
                    End If
                    myRow(myColumnsNames.Count + 1) = "CELLCSSCLASS:StatsBigTotalCell:" & bigTotal
                ElseIf totalPosition.ToUpper = "COL" Then
                    myRow(myColumnsNames.Count + 1) = "CELLCSSCLASS:StatsBigTotalCell:"
                End If

                myStatTable.Rows.Add(myRow)
            End If
            nbColumns = myStatTable.Columns.Count

            'Add Extra Columns
            If Not myDataSet.Tables("ExtraColumns") Is Nothing Then
                Dim myNbExtraColumns As Integer = 0
                With myDataSet.Tables("ExtraColumns").Columns
                    For i = 0 To .Count - 1
                        myStatTable.Columns.Add(.Item(i).ColumnName)
                    Next i

                    mySum = 0
                    Dim removeOneMoreRow As Byte = 0
                    If totalPosition.ToUpper = "ROW" Then removeOneMoreRow = 1
                    With myDataSet.Tables("ExtraColumns").Rows
                        For i = 0 To myStatTable.Rows.Count - 1 - removeOneMoreRow
                            If i < .Count Then
                                For j = nbColumns To myDataSet.Tables("ExtraColumns").Columns.Count - 1 + nbColumns
                                    myStatTable.Rows(i)(j) = .Item(i)(j - nbColumns)
                                    mySum += myStatTable.Rows(i)(j)
                                Next j
                            End If
                        Next i

                        If totalPosition.ToUpper = "ROW" Then myStatTable.Rows(myStatTable.Rows.Count - 1)(myStatTable.Columns.Count - 1) = mySum
                    End With
                End With

                nbColumns = myStatTable.Columns.Count
            End If

            'Add extra columns in first query (always hidden)
            If .Columns.Count > mainExtraColumns Then
                For i = mainExtraColumns To .Columns.Count - 1
                    myStatTable.Columns.Add(.Columns(i).ColumnName)
                Next i
                For i = 0 To myStatTable.Rows.Count - 1
                    For j = mainExtraColumns To .Columns.Count - 1
                        myStatTable.Rows(i)(nbColumns + j - mainExtraColumns) = myExtraColumnsData(myStatTable.Rows(i)(0) & "-" & j)
                    Next
                Next i
            End If
        End With

        Me.nbColumns = nbColumns

        Dim myTrueDataSet As New DataSet()
        myTrueDataSet.Tables.Add(myStatTable)
        
        generateHTMLTable(htmlBuilder, myTrueDataSet)

    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
        htmlBuilder.Append("<!-- ExtraColumnsSQL=").Append(myExtraColumnsSQL).AppendLine(" -->")
        htmlBuilder.Append("<!-- ExtraColumnsSQLWhere=").Append(myExtraColumnsSQLWhere).AppendLine(" -->")
        htmlBuilder.Append("<!-- TotalPosition=").Append(myTotalPosition).AppendLine(" -->")
        htmlBuilder.Append("<!-- TotalsType=").Append(_TotalsType).AppendLine(" -->")

        'SubTotalColumnsType
        htmlBuilder.AppendLine("<script language=javascript>var Stats_TotalsType = '" & _TotalsType & "';</script>")
    End Sub

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim curSQLWhere As String = ""

        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "ExtraColumnsSQL"
                    extraColumnsSQL = myKey.Value
                Case "ExtraColumnsSQLWhere"
                    extraColumnsSQLWhere = myKey.Value
                Case "TotalPosition"
                    totalPosition = myKey.Value
                Case "ShowInPourcent"
                    showInPourcent = myKey.Value
                Case "TotalsType"
                    _TotalsType = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)

        curSQLWhere = filtersWhereString
        If curSQLWhere <> "" And extraColumnsSQLWhere <> "" Then curSQLWhere &= " AND " & extraColumnsSQLWhere
        If curSQLWhere = "" Then curSQLWhere = extraColumnsSQLWhere
        If curSQLWhere <> "" Then If curSQLWhere.StartsWith("WHERE") = False Then curSQLWhere = "WHERE " & curSQLWhere

        extraColumnsSQL = Me.myReport.replaceCustomVariables(myExtraColumnsSQL)
        extraColumnsSQL = extraColumnsSQL.Replace("WHEREGEN", curSQLWhere)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        properties.Add("ExtraColumnsSQL", extraColumnsSQL)
        properties.Add("ExtraColumnsSQLWhere", extraColumnsSQLWhere)
        properties.Add("TotalPosition", totalPosition)
        properties.Add("ShowInPourcent", showInPourcent)
        properties.Add("TotalsType", _TotalsType)

        MyBase.saveProperties(properties)
    End Sub

    Public Overloads Shared Function findBody(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportBody
        Dim myRapportElement As New ReportBodyStats(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other bodys
        Return ReportBodyVisites.findBody(classElementName, leRapport, True, firstClass)
    End Function
End Class
