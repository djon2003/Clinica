Imports CI.Clinica.DateFormat

Public Class ReportHeaderSimple
    Inherits ReportBasicHeader

    Private _sqlStatement As String = ""
    Private _HeaderFileName As String = ""
    Private _sqlWhere As String = ""
    Private _ExtraFieldQuestion As String = ""
    Private _ExtraFieldType As String = ""

#Region "Properties"
    Public Property sqlStatement() As String
        Get
            Return _sqlStatement
        End Get
        Set(ByVal value As String)
            _sqlStatement = value
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

    Public Property headerFileName() As String
        Get
            Return _HeaderFileName
        End Get
        Set(ByVal value As String)
            _HeaderFileName = value
        End Set
    End Property

    Public Property extraFieldType() As String
        Get
            Return _ExtraFieldType
        End Get
        Set(ByVal value As String)
            _ExtraFieldType = value
        End Set
    End Property

    Public Property extraFieldQuestion() As String
        Get
            Return _ExtraFieldQuestion
        End Get
        Set(ByVal value As String)
            _ExtraFieldQuestion = value
        End Set
    End Property
#End Region

    Public Sub New(ByRef curReport As Report)
        MyBase.New(curReport)
    End Sub

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        Dim myRapportModel() As String = readFile(headerFileName, , , False)
        Dim header As String = String.Join(vbCrLf, myRapportModel)

        If sqlStatement <> "" Then
            Dim myDataSet As New DataSet()
            myDataSet = DBLinker.getInstance.readDBForGrid(sqlStatement, , , "Header", myDataSet, False)
            If myDataSet.Tables("Header") Is Nothing Then Exit Sub

            Dim myColumn As DataColumn
            With myDataSet.Tables("Header")
                For Each myColumn In .Columns
                    Dim myRowStr As String
                    Try
                        myRowStr = .Rows(0)(myColumn.ColumnName)
                    Catch ex As Exception
                        myRowStr = ""
                    End Try
                    If colsFormat.ContainsKey(myColumn.Caption) Then myRowStr = myReport.formatCell(myRowStr, colsFormat(myColumn.Caption))
                    header = header.Replace("###" & myColumn.Caption & "###", myRowStr)
                Next
            End With
        End If

        If myReport.reportFilters = "<br><font size=1></font>" Then myReport.reportFilters = ""
        header = header.Replace("###RapportTitle###", myReport.reportTitle)
        header = header.Replace("###RapportFilters###", myReport.reportFilters.Replace("</table>", myReport.reportBodyOrder & "</table>"))
        header = header.Replace("###RapportDate###", DateFormat.getTextDate(Date.Now, TextDateOptions.YYYYMMDD))
        header = header.Replace("###RapportTime###", DateFormat.getTextDate(Date.Now, TextDateOptions.ShortTime))
        header = header.Replace("###RapportCreatorName###", currentUserName)

        'Apply Passif Filter
        If _ExtraFieldType = "ASK" And _ExtraFieldQuestion <> "" Then
            Dim myInputBoxPlus As New InputBoxPlus
            header = header.Replace("###ExtraField###", myInputBoxPlus(_ExtraFieldQuestion, "Champs en extra"))
        ElseIf _ExtraFieldType = "FILTER" Then
            With CType(myReport.reportFilter, FilterComposite)
                header = header.Replace("###ExtraField###", CType(.Item(.indexOf("FilterPassive")), FilterPassive).passiveValue)
            End With
        Else
            header = header.Replace("###ExtraField###", "")
        End If

        header = myReport.replaceCustomVariables(header)

        htmlBuilder.AppendLine(header)
    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
        htmlBuilder.AppendLine("<!-- HeaderFileName=" & _HeaderFileName & " -->")
        htmlBuilder.AppendLine("<!-- SQLStatement=" & _sqlStatement & " -->")
        htmlBuilder.AppendLine("<!-- SQLWhere=" & _sqlWhere & " -->")
        htmlBuilder.AppendLine("<!-- ExtraFieldQuestion=" & _ExtraFieldQuestion & " -->")
        htmlBuilder.AppendLine("<!-- ExtraFieldType=" & _ExtraFieldType & " -->")
    End Sub

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "HeaderFileName"
                    headerFileName = myKey.Value
                    headerFileName = headerFileName.Replace("###CLINICAPATH###", appPath & bar(appPath))
                Case "SQLStatement"
                    _sqlStatement = myKey.Value
                Case "SQLWhere"
                    _sqlWhere = myKey.Value
                Case "ExtraFieldType"
                    _ExtraFieldType = myKey.Value
                Case "ExtraFieldQuestion"
                    _ExtraFieldQuestion = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)

        Dim curSQLWhere As String = filtersWhereString
        If curSQLWhere <> "" And sqlWhere <> "" Then curSQLWhere &= " AND " & sqlWhere
        If curSQLWhere = "" Then curSQLWhere = sqlWhere
        If curSQLWhere <> "" And curSQLWhere.StartsWith("WHERE") = False Then curSQLWhere = "WHERE " & curSQLWhere

        _sqlStatement = Me.myReport.replaceCustomVariables(_sqlStatement)
        _sqlStatement = _sqlStatement.Replace("WHEREGEN", curSQLWhere)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        properties.Add("HeaderFileName", headerFileName)
        properties.Add("SQLStatement", sqlStatement)

        MyBase.saveProperties(properties)
    End Sub

    Public Overloads Shared Function findHeader(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportHeader
        Dim myRapportElement As New ReportHeaderSimple(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other footers
        Return ReportBasicHeader.findHeader(classElementName, leRapport, firstDone, firstClass)
    End Function
End Class
