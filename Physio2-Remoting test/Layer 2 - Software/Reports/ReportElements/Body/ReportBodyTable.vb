Imports System.ComponentModel

Public Class ReportBodyTable
    Inherits ReportBasicBody

    Protected Const EXTRA_CELL_VALUES As String = "§XCellV§"

    Private _EndTable As Boolean = True
    Private _IsGrouped As Boolean = False
    Private _ShowSubTotalLine As Boolean = True
    Private _ShowOnlySubTotals As Boolean = False
    Private _AddUpSubTotalLine As Boolean = False
    Private _NbGroupColumns As Integer = 0
    Private _NbGroupColumnsSubTotal As Integer = -1
    Private _GroupColumnsAlias() As String = Nothing
    Protected _ColumnsName() As String = Nothing
    Private _sqlWhere As String = ""
    Private _NbColumns As Integer = 0
    Protected Const groupJoiner As String = "£"
    Private _SubTotalColumnsName() As String = Nothing
    Private _SubTotalColumnsType As New Hashtable
    Private _totals As New Hashtable
    Private _AskShowOnlySubTotals As Boolean = False
    Private _ColumnsHidingEqualNextData() As String = Nothing
    Protected mySQLStatementW_OData As String = ""
    Private _PassedTable As DataSet
    Private _ColumnsCSS As New Hashtable
    Private _ColumnsLink As New Hashtable

    Public Sub New(ByRef curReport As Report)
        MyBase.New(curReport)
    End Sub

#Region "Properties"
    Public Property columnsLink() As Hashtable
        Get
            Return _ColumnsLink
        End Get
        Set(ByVal value As Hashtable)
            _ColumnsLink = value
        End Set
    End Property

    Public Property columnsCSS() As Hashtable
        Get
            Return _ColumnsCSS
        End Get
        Set(ByVal value As Hashtable)
            _ColumnsCSS = value
        End Set
    End Property

    Public Property passedTable() As DataTable
        Get
            If _PassedTable Is Nothing Then Return Nothing

            Return _PassedTable.Tables("Body").Copy
        End Get
        Set(ByVal value As DataTable)
            If value Is Nothing Then
                _PassedTable = Nothing
                Exit Property
            End If

            Dim ds As New DataSet()
            value.TableName = "Body"
            ds.Tables.Add(value.Copy)
            _PassedTable = ds
        End Set
    End Property

    Public Property askShowOnlySubTotals() As Boolean
        Get
            Return _AskShowOnlySubTotals
        End Get
        Set(ByVal value As Boolean)
            _AskShowOnlySubTotals = value
        End Set
    End Property

    Public Property subTotalColumnsType() As Hashtable
        Get
            Return _SubTotalColumnsType
        End Get
        Set(ByVal value As Hashtable)
            _SubTotalColumnsType = value
        End Set
    End Property

    Public Property totals() As Hashtable
        Get
            Return _totals
        End Get
        Set(ByVal value As Hashtable)
            _totals = value
        End Set
    End Property

    Public Property endTable() As Boolean
        Get
            Return _EndTable
        End Get
        Set(ByVal value As Boolean)
            _EndTable = value
        End Set
    End Property

    Public Property showSubTotalLine() As Boolean
        Get
            Return _ShowSubTotalLine
        End Get
        Set(ByVal value As Boolean)
            _ShowSubTotalLine = value
        End Set
    End Property

    Public Property addUpSubTotalLine() As Boolean
        Get
            Return _AddUpSubTotalLine
        End Get
        Set(ByVal value As Boolean)
            _AddUpSubTotalLine = value
        End Set
    End Property

    Public Property showOnlySubTotals() As Boolean
        Get
            Return _ShowOnlySubTotals
        End Get
        Set(ByVal value As Boolean)
            _ShowOnlySubTotals = value
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

    Public Overridable Property isGrouped() As Boolean
        Get
            Return _IsGrouped
        End Get
        Set(ByVal value As Boolean)
            _IsGrouped = value
        End Set
    End Property

    <DescriptionAttribute("Nombre de colonnes qui seront groupées, c-à-d les colonnes servant à regrouper les informations subséquentes."), RefreshProperties(RefreshProperties.All)> _
    Public Overridable Property nbGroupColumns() As Integer
        Get
            Return _NbGroupColumns
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then value = 0
            _NbGroupColumns = value
            If sqlStatement <> "" Then populateColumnsName()
        End Set
    End Property

    Public Overridable Property nbGroupColumnsSubTotal() As Integer
        Get
            Return _NbGroupColumnsSubTotal
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then value = 0
            _NbGroupColumnsSubTotal = value
        End Set
    End Property

    Public Overridable Property nbColumns() As Integer
        Get
            Return _NbColumns
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then value = 0
            _NbColumns = value
        End Set
    End Property

    Public Overridable Property groupColumnsAlias() As String()
        Get
            Return _GroupColumnsAlias
        End Get
        Set(ByVal value() As String)
            _GroupColumnsAlias = value
        End Set
    End Property

    Public ReadOnly Property columnsName() As String()
        Get
            Return _ColumnsName
        End Get
    End Property

    Public Property subTotalColumnsName() As String()
        Get
            Return _SubTotalColumnsName
        End Get
        Set(ByVal value() As String)
            _SubTotalColumnsName = value
        End Set
    End Property

    Public Property columnsHidingEqualNextData() As String()
        Get
            Return _ColumnsHidingEqualNextData
        End Get
        Set(ByVal value() As String)
            _ColumnsHidingEqualNextData = value
        End Set
    End Property
#End Region

#Region "Private subs"
    Protected Overridable Sub populateColumnsName()
        If mySQLStatementW_OData = "" Then Exit Sub

        Dim nbColumns As Integer = nbColumns
        Dim ds As DataSet = DBLinker.getInstance.readDBForGrid(mySQLStatementW_OData)
        If ds Is Nothing OrElse ds.Tables.Count = 0 Then Exit Sub
        nbColumns = IIf(nbColumns <= 0, ds.Tables(0).Columns.Count, IIf(ds.Tables(0).Columns.Count < nbColumns, ds.Tables(0).Columns.Count, nbColumns))

        Dim minColumn As Integer = 0
        If _IsGrouped AndAlso _NbGroupColumns <> 0 AndAlso Not _GroupColumnsAlias Is Nothing AndAlso _GroupColumnsAlias.Length <> 0 Then minColumn = _NbGroupColumns
        'Creates ColumnsName property
        ReDim _ColumnsName(nbColumns - 1 - minColumn)
        For i As Integer = minColumn To nbColumns - 1
            _ColumnsName(i - minColumn) = ds.Tables(0).Columns(i).Caption
        Next i
    End Sub
#End Region

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        mySQLStatement = transformSQLStatement(mySQLStatement)

        Dim myDataSet As DataSet
        If _PassedTable IsNot Nothing Then
            myDataSet = _PassedTable
        Else
            myDataSet = DBLinker.getInstance.readDBForGrid(mySQLStatement, , , "Body", , False)
        End If

        If myDataSet Is Nothing Then Exit Sub

        'Demande si on affiche les détails
        If askShowOnlySubTotals And myReport.filtered = False Then
            If MessageBox.Show("Désirez-vous afficher les détails du rapport " & myReport.reportTitle & " ?", "Affichage des détails", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                showOnlySubTotals = False
            Else
                showOnlySubTotals = True
            End If
        Else
            showOnlySubTotals = False
        End If

        generateHTMLTable(htmlBuilder, myDataSet)
    End Sub

    Protected Function addLink(ByVal myCell As String, ByVal columnName As String, ByVal myTable As DataTable, ByVal rowIndex As Integer) As String
        'Add link if needed
        If _ColumnsLink.ContainsKey(columnName) Then
            Dim link As String = _ColumnsLink(columnName)
            Dim varPos As Integer = link.IndexOf("###")
            Dim endPos As Integer = -1
            Dim replacing As New Generic.List(Of String)
            While varPos <> -1
                endPos = link.IndexOf("###", varPos + 3)
                If endPos = -1 Then Exit While

                replacing.Add(link.Substring(varPos, endPos - varPos + 3))
                varPos = link.IndexOf("###", endPos + 3)
            End While
            Dim curReplacingColumn As String = ""
            With myTable
                For Each replace As String In replacing
                    curReplacingColumn = replace.Substring(3, replace.Length - 6)
                    If .Columns.Contains(curReplacingColumn) AndAlso .Rows(rowIndex)(curReplacingColumn) IsNot DBNull.Value Then
                        link = link.Replace(replace, .Rows(rowIndex)(curReplacingColumn))
                    ElseIf .Columns.Contains(curReplacingColumn) AndAlso .Rows(rowIndex)(curReplacingColumn) Is DBNull.Value Then
                        'If the value of the replacement is null, then don't link (This was to correct the BodyStats Total line which was surrounded by a link)
                        Return myCell
                    End If
                Next
            End With
            myCell = "<a href=""" & link & """>" & myCell & "</a>"
        End If

        Return myCell
    End Function

    Protected Sub generateHTMLTable(ByRef htmlBuilder As System.Text.StringBuilder, ByVal myDataSet As DataSet)
        Dim i, j As Integer
        Dim alignement As String = ""

        If myDataSet.Tables("Body") Is Nothing Then Exit Sub

        'Add DIV to underline lines
        htmlBuilder.AppendLine("<div class=""underliner""></div>")

        'Add refresh tooltip
        htmlBuilder.AppendLine("<div class=""loading"">En cours de rafraîchissement du rapport...</div>")


        'Add input to filter a column
        htmlBuilder.AppendLine("<a class=""filterButton"" onmouseout=""showHidePart(curTD);"" onclick=""showFilter(this);"">Filtrer</a><div class=""filterInput""><select class=""filterInputOperator""></select><input type=text class=""filterInputData"" width=10 onkeyup=""if (window.event.keyCode == 27) this.value=''; if (window.event.keyCode==13 || window.event.keyCode == 27) addFilter();"" onblur=""""></div>")

        '

        'Add Javascript
        Dim jsFile As String = appPath & bar(appPath) & "Data\Rapports\basicbody.js"
        If IO.File.Exists(jsFile) Then htmlBuilder.AppendLine("<script language=javascript>").AppendLine(IO.File.ReadAllText(jsFile)).AppendLine("</script>")

        With myDataSet.Tables("Body")
            If nbColumns = 0 OrElse nbColumns > .Columns.Count Then nbColumns = .Columns.Count
            If nbColumns < 1 Then Exit Sub

            'REM For hidding grouping HTMLBuilder.AppendLine("<script language=javascript>function SH(ObjID) {if (document.getElementById(ObjID).style.display =='none') {document.getElementById(ObjID).style.display='block';} else {document.getElementById(ObjID).style.display = 'none';}}</script>")
            htmlBuilder.AppendLine("<table class=BodyTable onmouseout='changeRowOver(this);'>")

            totals.Clear()
            Dim columnWidth As String = 100 / nbColumns
            forceManaging(columnWidth, True, "", False, False, False, False, ",§.", , , , , , , 2)
            columnWidth = columnWidth.Replace(",", ".")

            Dim myColumn As DataColumn
            Dim showHeaders As Boolean = True
            Dim myRow As DataRow
            Dim minColumn As Integer = 0
            Dim lastGroupedColumn As Integer = -1
            Dim firstGroupedColumn As Integer = 0
            Dim maxGroupColumns As Integer = _NbGroupColumns - 1
            Dim grouping As Boolean = True
            Dim lastGroupRow() As String
            Dim lastRow() As String
            Dim totauxTitre As String = ""
            'Load group alias
            If _IsGrouped AndAlso _NbGroupColumns <> 0 AndAlso Not _GroupColumnsAlias Is Nothing AndAlso _GroupColumnsAlias.Length <> 0 Then
                ReDim lastGroupRow(maxGroupColumns)
                lastGroupedColumn = 0
                minColumn = _NbGroupColumns
            Else
                grouping = False
            End If

            ReDim lastRow(nbColumns - minColumn - 1)

            'Create Table
            Dim RowIndex, totalRows As Integer
            Dim firstGroup As String = "FirstBodyGroup "
            Dim primeGroup As String = "PrimeBodyGroup "
            Dim columnCSS As String = ""
            totalRows = .Rows.Count - 1
            totals.Add("§TABLEROWCOUNT§", totalRows + 1)
            For RowIndex = 0 To totalRows
                myRow = .Rows(RowIndex)
                Dim cellValue As String = String.Empty
                Dim extraValuesIndex As Integer = -1

                'Show group if required
                If _IsGrouped AndAlso grouping AndAlso Not _GroupColumnsAlias Is Nothing AndAlso _GroupColumnsAlias.Length <> 0 Then
                    For i = firstGroupedColumn To maxGroupColumns
                        cellValue = myRow(i).ToString()
                        extraValuesIndex = cellValue.IndexOf(EXTRA_CELL_VALUES)
                        If extraValuesIndex <> -1 Then cellValue = cellValue.Substring(0, extraValuesIndex)

                        If i = 0 Then
                            primeGroup = "PrimeBodyGroup "
                        Else
                            primeGroup = ""
                        End If

                        If cellValue <> "" Then
                            htmlBuilder.Append("<tr><td class=""BodyGroup ").Append(primeGroup).Append(firstGroup).Append(""" colspan=").Append(CInt(.Columns.Count - _NbGroupColumns)).Append("><label class=BodyGroupText>") 'REM For hidding grouping (replace label by anchor) : <a href=javascript:SH('BG" & RowIndex & "');
                            firstGroup = ""
                            For j = 1 To i
                                htmlBuilder.Append("&nbsp;")
                                htmlBuilder.Append("&nbsp;")
                            Next j
                            lastGroupRow(i) = cellValue
                            htmlBuilder.Append(_GroupColumnsAlias(i)).Append(" - ")
                            htmlBuilder.Append(addLink(myReport.formatCell(lastGroupRow(i), colsFormat(.Columns(i).ColumnName)), .Columns(i).ColumnName, myDataSet.Tables("Body"), RowIndex))
                            htmlBuilder.AppendLine("</label></td></tr>")
                            If totals.ContainsKey(_GroupColumnsAlias(i)) = False Then
                                totals.Add(_GroupColumnsAlias(i), "1")
                            Else
                                totals(_GroupColumnsAlias(i)) += 1
                            End If
                            lastGroupedColumn = i
                        End If
                    Next i

                    For i = 0 To lastRow.GetUpperBound(0)
                        lastRow(i) = ""
                    Next i
                    grouping = False
                    showHeaders = True
                    htmlBuilder.Append("<tbody id=BG" & RowIndex & " style='display:block'>")
                End If

                'CellHeaders
                If showHeaders Then
                    'Headers HTML
                    htmlBuilder.Append("<tr>")
                    For i = minColumn To nbColumns - 1
                        myColumn = .Columns(i)
                        htmlBuilder.Append("<td onmouseover=""showHidePart(this,false);"" onmouseout=""showHidePart(this,true);"" class=BodyCellTitle").Append(myReport.alignCell(colsFormat(myColumn.ColumnName))).Append(" width=").Append(columnWidth).Append("%>").Append(myColumn.Caption).Append("</td>")
                    Next i
                    htmlBuilder.AppendLine("</tr>")

                    showHeaders = False
                End If

                'Normal cells
                If _ShowOnlySubTotals = False Then htmlBuilder.Append("<tr nr style='background:' onmouseover=javascript:changeRowOver(this);>")
                Dim myCell As String = ""
                Dim curGroup As String = ""
                Dim previousGroup As String
                Dim cellCssClass As String = "BodyCell"
                For i = minColumn To nbColumns - 1
                    If myRow(i) Is DBNull.Value Then
                        cellValue = String.Empty
                    Else
                        cellValue = myRow(i).ToString()
                    End If
                    extraValuesIndex = cellValue.IndexOf(EXTRA_CELL_VALUES)
                    If extraValuesIndex <> -1 Then cellValue = cellValue.Substring(0, extraValuesIndex)
                    If cellValue.ToUpper().StartsWith("CELLCSSCLASS:") Then
                        Dim sCell() As String = cellValue.Split(New Char() {":"}, 3)
                        cellCssClass = sCell(1)
                        cellValue = sCell(2)
                    Else
                        cellCssClass = "BodyCell"
                    End If
                    myColumn = .Columns(i)
                    If Me._ColumnsCSS.Contains(myColumn.ColumnName) Then cellCssClass = Me._ColumnsCSS(myColumn.ColumnName) & " " & cellCssClass 'CSS Choisi par l'utilisateur

                    'Create sub-totals
                    previousGroup = ""
                    For j = 0 To maxGroupColumns
                        Dim cellValueJ As String = myRow(j).ToString()
                        Dim extraValuesJIndex As Integer = cellValueJ.IndexOf(EXTRA_CELL_VALUES)
                        If extraValuesJIndex <> -1 Then cellValueJ = cellValueJ.Substring(0, extraValuesJIndex)

                        If j > 0 Then previousGroup &= totals(_GroupColumnsAlias(j - 1)) & ";"
                        Try
                            curGroup = previousGroup & _GroupColumnsAlias(j) & " - " & cellValueJ
                        Catch ex As Exception
                            'TODO : Empty exception !
                        End Try
                        If totals.ContainsKey(curGroup) = False Then totals.Add(curGroup, 0)

                        If curGroup <> "" Then
                            If i = minColumn Then totals(curGroup) += 1
                            If totals.ContainsKey(curGroup & " - " & myColumn.ColumnName) = False Then totals.Add(curGroup & " - " & myColumn.ColumnName, 0)
                            totals(curGroup & " - " & myColumn.ColumnName) = myReport.addUpCell(totals(curGroup & " - " & myColumn.ColumnName), cellValue, colsFormat(myColumn.ColumnName))
                        End If
                    Next j
                    If totals.ContainsKey(myColumn.ColumnName) = False Then totals.Add(myColumn.ColumnName, 0)
                    totals(myColumn.ColumnName) = myReport.addUpCell(totals(myColumn.ColumnName), cellValue, colsFormat(myColumn.ColumnName))

                    'Show cells if required
                    If _ShowOnlySubTotals = False Then
                        myCell = cellValue
                        If Not functionsToApply Is Nothing AndAlso functionsToApply.Length <> 0 Then
                            For j = 0 To functionsToApply.GetUpperBound(1)
                                If functionsToApply(4, j) = myColumn.Caption Then myCell = applyFunctions(myCell, j)
                            Next j
                        End If
                        myCell = myReport.formatCell(myCell, colsFormat(myColumn.ColumnName))
                        'Add link if needed
                        myCell = addLink(myCell, myColumn.ColumnName, myDataSet.Tables("Body"), RowIndex)

                        'Build final cell HTML
                        If columnsHidingEqualNextData IsNot Nothing Then If lastRow(i - minColumn) = myCell And Array.IndexOf(columnsHidingEqualNextData, myColumn.ColumnName) >= 0 Then myCell = ""
                        htmlBuilder.Append("<td")
                        'Add extra attributes of cell
                        'TODO : Set cellTitle by using extraCellValues + extraColumnNames
                        Dim cellTitle As String = String.Empty
                        If extraValuesIndex <> -1 Then
                            Dim extraValues() As String = myRow(i).ToString().Split(New String() {EXTRA_CELL_VALUES}, StringSplitOptions.None)
                            For j = 1 To extraValues.Length - 1
                                htmlBuilder.Append(" at").Append(j).Append("=""").Append(extraValues(j)).Append("""")
                            Next j
                        End If
                        'Add TD class
                        If cellCssClass <> "BodyCell" Then htmlBuilder.Append(" class=""").Append(cellCssClass).Append("""")
                        'Add cell data
                        htmlBuilder.Append(myReport.alignCell(colsFormat(myColumn.ColumnName))).Append(">").Append(myCell).Append("</td>")
                        lastRow(i - minColumn) = myCell
                    End If
                Next i
                If _ShowOnlySubTotals = False Then htmlBuilder.AppendLine("</tr>")

                'Look if grouping will be required
                firstGroupedColumn = lastGroupedColumn
                If _IsGrouped And Not _GroupColumnsAlias Is Nothing AndAlso _GroupColumnsAlias.Length <> 0 And RowIndex <> totalRows Then
                    Try
                        Dim oldGroup As String = String.Empty
                        For i = maxGroupColumns To 0 Step -1
                            Dim nextCellValue As String = .Rows(RowIndex + 1)(i)
                            Dim nextExtraValuesIndex As Integer = nextCellValue.IndexOf(EXTRA_CELL_VALUES)
                            If nextExtraValuesIndex <> -1 Then nextCellValue = nextCellValue.Substring(0, nextExtraValuesIndex)

                            If lastGroupRow(i) <> nextCellValue Then
                                grouping = True
                                firstGroupedColumn = i
                            End If
                        Next
                    Catch
                    End Try
                End If

                'Show subtotals of group if required
                If _IsGrouped And (grouping Or RowIndex = totalRows) And Not _GroupColumnsAlias Is Nothing AndAlso _GroupColumnsAlias.Length <> 0 Then
                    If RowIndex = totalRows Then firstGroupedColumn = 0
                    For i = lastGroupedColumn To firstGroupedColumn Step -1
                        If nbGroupColumnsSubTotal = -1 Or nbGroupColumnsSubTotal >= (i + 1) Then 'Si l'on doit afficher le sous-total
                            cellValue = myRow(i).ToString()
                            extraValuesIndex = cellValue.IndexOf(EXTRA_CELL_VALUES)
                            If extraValuesIndex <> -1 Then cellValue = cellValue.Substring(0, extraValuesIndex)

                            htmlBuilder.Append("<tr><td class=BodySubTotal colspan=").Append(CInt(.Columns.Count - _NbGroupColumns)).Append(">")
                            For j = 1 To i
                                htmlBuilder.Append("&nbsp;")
                                htmlBuilder.Append("&nbsp;")
                            Next j
                            If i = 0 Then
                                totauxTitre = "Totaux"
                            Else
                                totauxTitre = "Sous-totaux"
                            End If

                            Dim totauxName As String = " de " & _GroupColumnsAlias(i)
                            If cellValue = String.Empty Then totauxName = String.Empty
                            htmlBuilder.Append(totauxTitre).Append(totauxName).Append(" - ").Append(myReport.formatCell(cellValue, colsFormat(.Columns(i).ColumnName)))
                            curGroup = _GroupColumnsAlias(i) & " - " & cellValue
                            If i > 0 Then
                                For j = i To 1
                                    curGroup = totals(_GroupColumnsAlias(j - 1)) & ";" & curGroup
                                Next j
                            End If
                            htmlBuilder.Append("(").Append(totals(curGroup)).AppendLine(")</td></tr>")
                            Dim subTotalLine As String = ""
                            Dim subTotal As Double = 0
                            Dim lastCol As Integer = 0
                            Dim showSubLine As Boolean = False
                            subTotalLine = "<tr>"
                            For j = minColumn To nbColumns - 1
                                myColumn = .Columns(j)
                                If totals.ContainsKey(curGroup & " - " & myColumn.ColumnName) = False Then
                                    myCell = "&nbsp;"
                                Else
                                    If (totals(curGroup & " - " & myColumn.ColumnName) < 0 And colsFormat(myColumn.ColumnName) <> "INT" And colsFormat(myColumn.ColumnName) <> "CURRENCY") Or searchArray(_SubTotalColumnsName, myColumn.ColumnName, SearchType.ExactMatch) < 0 Then
                                        myCell = "&nbsp;"
                                    Else
                                        subTotal += totals(curGroup & " - " & myColumn.ColumnName)
                                        myCell = totals(curGroup & " - " & myColumn.ColumnName)
                                        If _SubTotalColumnsType.ContainsKey(myColumn.ColumnName) Then
                                            Select Case _SubTotalColumnsType(myColumn.ColumnName)
                                                Case "AVG"
                                                    myCell /= totals(curGroup)
                                            End Select
                                        End If
                                        myCell = myReport.formatCell(myCell, colsFormat(myColumn.ColumnName))
                                        showSubLine = True
                                        lastCol = j
                                    End If
                                End If

                                subTotalLine &= "<td class=BodySubTotalCell" & myReport.alignCell(colsFormat(myColumn.ColumnName)) & ">" & myCell & "</td>"
                            Next j
                            subTotalLine &= "</tr>"
                            If showSubLine And _ShowSubTotalLine Then htmlBuilder.AppendLine(subTotalLine)
                            If _AddUpSubTotalLine Then
                                htmlBuilder.Append("<tr><td class=BodySubTotalLine colspan=").Append(lastCol - minColumn + 1).Append(">Total par ").Append(_GroupColumnsAlias(i)).Append(" ")
                                htmlBuilder.Append(myReport.formatCell(subTotal, colsFormat(.Columns(lastCol).ColumnName)))
                                htmlBuilder.Append("</td><td colspan=").Append(CInt(.Columns.Count - _NbGroupColumns) - lastCol - minColumn + 1).AppendLine(">&nbsp;</td></tr>")
                            End If
                            htmlBuilder.Append("</tbody>")
                        End If
                    Next i
                End If
            Next RowIndex

            If _EndTable Then htmlBuilder.AppendLine("</table>")
        End With
    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
        htmlBuilder.Append("<!-- NbGroupColumns=").Append(_NbGroupColumns).AppendLine(" -->")
        htmlBuilder.Append("<!-- EndTable=").Append(_EndTable).AppendLine(" -->")
        htmlBuilder.Append("<!-- IsGrouped=").Append(_IsGrouped).AppendLine(" -->")
        Dim myGroupAlias As String = ""
        If Not _GroupColumnsAlias Is Nothing AndAlso _GroupColumnsAlias.Length <> 0 Then myGroupAlias = String.Join(groupJoiner, _GroupColumnsAlias)
        htmlBuilder.Append("<!-- GroupColumnsAlias={").Append(myGroupAlias).AppendLine("} -->")
        htmlBuilder.Append("<!-- NbColumns=").Append(_NbColumns).AppendLine(" -->")
        htmlBuilder.Append("<!-- AddUpSubTotalLine=").Append(_AddUpSubTotalLine).AppendLine(" -->")
        htmlBuilder.Append("<!-- SQLWhere=").Append(_sqlWhere).AppendLine(" -->")
        htmlBuilder.Append("<!-- ShowSubTotalLine=").Append(_ShowSubTotalLine).AppendLine(" -->")
        htmlBuilder.Append("<!-- ShowOnlySubTotals=").Append(_ShowOnlySubTotals).AppendLine(" -->")
        Dim mySubTotalColumns As String = ""
        If Not _SubTotalColumnsName Is Nothing AndAlso _SubTotalColumnsName.Length <> 0 Then mySubTotalColumns = String.Join(groupJoiner, _SubTotalColumnsName)
        htmlBuilder.Append("<!-- SubTotalColumnsName={").Append(mySubTotalColumns).AppendLine("} -->")
        htmlBuilder.Append("<!-- AskShowOnlySubTotals=").Append(_AskShowOnlySubTotals).AppendLine(" -->")

        'SubTotalColumnsType
        htmlBuilder.Append("<script language=javascript>var SubTotalColumnsType = {};")
        For Each curKey As String In subTotalColumnsType.Keys
            htmlBuilder.Append("SubTotalColumnsType[""").Append(curKey).Append("""] = """ & subTotalColumnsType(curKey) & """;")
        Next
        htmlBuilder.AppendLine("</script>")
    End Sub

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim curSQLStatement As String = ""
        Dim curSQLWhere As String = ""

        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "NbGroupColumnsSubTotal"
                    nbGroupColumnsSubTotal = myKey.Value
                Case "NbGroupColumns"
                    nbGroupColumns = myKey.Value
                Case "GroupColumnsAlias"
                    groupColumnsAlias = myKey.Value
                Case "EndTable"
                    endTable = myKey.Value
                Case "IsGrouped"
                    isGrouped = myKey.Value
                Case "NbColumns"
                    nbColumns = myKey.Value
                Case "SQLWhere"
                    sqlWhere = myKey.Value
                Case "SQLStatement" 'Overwritten from BasicBody to be able to apply WHEREGEN replacement with the SQLWhere added
                    curSQLStatement = myKey.Value
                Case "ShowOnlySubTotals"
                    _ShowOnlySubTotals = myKey.Value
                Case "ShowSubTotalLine"
                    _ShowSubTotalLine = myKey.Value
                Case "AddUpSubTotalLine"
                    _AddUpSubTotalLine = myKey.Value
                Case "SubTotalColumnsName"
                    _SubTotalColumnsName = myKey.Value
                Case "SubTotalColumnsType"
                    Dim cols() As String = myKey.Value
                    Dim i As Integer
                    For i = 0 To cols.GetUpperBound(0)
                        Dim sCol() As String = cols(i).Split(New Char() {"="})
                        _SubTotalColumnsType.Add(sCol(0), sCol(1))
                    Next i
                Case "ColumnsHidingEqualNextData"
                    _ColumnsHidingEqualNextData = myKey.Value
                Case "AskShowOnlySubTotals"
                    _AskShowOnlySubTotals = myKey.Value
                Case "ColumnsCSS"
                    Dim cols() As String = myKey.Value
                    Dim i As Integer
                    For i = 0 To cols.GetUpperBound(0)
                        Dim sCol() As String = cols(i).Split(New Char() {"="})
                        _ColumnsCSS.Add(sCol(0), sCol(1))
                    Next i
                Case "ColumnsLink"
                    Dim cols() As String = myKey.Value
                    Dim i As Integer
                    For i = 0 To cols.GetUpperBound(0)
                        Dim sCol() As String = cols(i).Split(New Char() {"="})
                        _ColumnsLink.Add(sCol(0), sCol(1))
                    Next i
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)

        curSQLWhere = filtersWhereString
        If curSQLWhere <> "" And sqlWhere <> "" Then curSQLWhere &= " AND " & sqlWhere
        If curSQLWhere = "" Then curSQLWhere = sqlWhere
        If curSQLWhere <> "" And curSQLWhere.StartsWith("WHERE") = False Then curSQLWhere = "WHERE " & curSQLWhere

        mySQLStatement = Me.myReport.replaceCustomVariables(curSQLStatement)
        mySQLStatement = mySQLStatement.Replace("ORDERGEN", currentOrder)
        mySQLStatementW_OData = mySQLStatement
        mySQLStatement = mySQLStatement.Replace("WHEREGEN", curSQLWhere)

        'Adjust the SQL statement to remove all data loading
        If curSQLWhere = "" Then
            curSQLWhere = "WHERE 2=1"
        Else
            curSQLWhere &= " AND 2=1"
        End If
        mySQLStatementW_OData = transformSQLStatement(mySQLStatementW_OData.Replace("WHEREGEN", curSQLWhere))
        populateColumnsName()
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        properties.Add("IsGrouped", isGrouped)
        properties.Add("EndTable", endTable)
        properties.Add("NbGroupColumns", nbGroupColumns)
        properties.Add("GroupColumnsAlias", groupColumnsAlias)
        properties.Add("NbColumns", nbColumns)
        properties.Add("SQLWhere", sqlWhere)
        properties.Add("NbGroupColumnsSubTotal", nbGroupColumnsSubTotal)
        properties.Add("ShowOnlySubTotals", showOnlySubTotals)
        properties.Add("ShowSubTotalLine", showSubTotalLine)
        properties.Add("AddUpSubTotalLine", addUpSubTotalLine)
        properties.Add("SubTotalColumnsName", subTotalColumnsName)
        properties.Add("ColumnsHidingEqualNextData", columnsHidingEqualNextData)
        properties.Add("AskShowOnlySubTotals", askShowOnlySubTotals)
        properties.Add("SubTotalColumnsType", _SubTotalColumnsType)
        properties.Add("ColumnsCSS", _ColumnsCSS)
        properties.Add("ColumnsLink", _ColumnsLink)

        MyBase.saveProperties(properties)
    End Sub

    Public Overloads Shared Function findBody(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportBody
        Dim myRapportElement As New ReportBodyTable(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other bodys
        Return ReportBodyAgenda.findBody(classElementName, leRapport, True, firstClass)
    End Function
End Class
