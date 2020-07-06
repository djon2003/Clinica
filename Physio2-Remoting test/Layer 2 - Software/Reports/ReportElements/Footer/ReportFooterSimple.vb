Public Class ReportFooterSimple
    Inherits ReportBasicFooter

    Private myFooterFileName As String = ""
    Private mySQLStatement As String = ""
    Private mySQLWhere As String = ""
    Private myExtraFieldType As String = ""
    Private myExtraFieldQuestion As String = ""

#Region "Properties"
    Public Property sqlStatement() As String
        Get
            Return mySQLStatement
        End Get
        Set(ByVal value As String)
            mySQLStatement = value
        End Set
    End Property

    Public Property sqlWhere() As String
        Get
            Return mySQLWhere
        End Get
        Set(ByVal value As String)
            mySQLWhere = value
        End Set
    End Property

    Public Property footerFileName() As String
        Get
            Return myFooterFileName
        End Get
        Set(ByVal value As String)
            myFooterFileName = value
        End Set
    End Property

    Public Property extraFieldType() As String
        Get
            Return myExtraFieldType
        End Get
        Set(ByVal value As String)
            myExtraFieldType = value
        End Set
    End Property

    Public Property extraFieldQuestion() As String
        Get
            Return myExtraFieldQuestion
        End Get
        Set(ByVal value As String)
            myExtraFieldQuestion = value
        End Set
    End Property
#End Region

    Public Sub New(ByVal curReport As Report)
        MyBase.New(curReport)
    End Sub

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        Dim myRapportModel() As String = readFile(myFooterFileName, , , False)
        Dim footer As String = String.Join(vbCrLf, myRapportModel)

        If mySQLStatement <> "" Then
            Dim myDataSet As New DataSet()
            myDataSet = DBLinker.getInstance.readDBForGrid(mySQLStatement, , , "Footer", myDataSet, False)
            If myDataSet.Tables("Footer") Is Nothing Then Exit Sub

            Dim myColumn As DataColumn
            With myDataSet.Tables("Footer")
                For Each myColumn In .Columns
                    Dim myRowStr As String = ""
                    Try
                        If .Rows.Count <> 0 Then myRowStr = .Rows(0)(myColumn.ColumnName)
                    Catch ex As Exception
                        myRowStr = ""
                    End Try
                    If colsFormat.ContainsKey(myColumn.Caption) Then myRowStr = myReport.formatCell(myRowStr, colsFormat(myColumn.Caption))
                    footer = footer.Replace("###" & myColumn.Caption & "###", myRowStr)
                Next
            End With
        End If

        'Apply Passif Filter
        If myExtraFieldType = "ASK" And myExtraFieldQuestion <> "" Then
            Dim myInputBoxPlus As New InputBoxPlus
            footer = footer.Replace("###ExtraField###", myInputBoxPlus(myExtraFieldQuestion, "Champs en extra"))
        ElseIf myExtraFieldType = "FILTER" Then
            Try
                With CType(myReport.reportFilter, FilterComposite)
                    footer = footer.Replace("###ExtraField###", CType(.Item(.indexOf("FilterPassif")), FilterPassive).passiveValue)
                End With
            Catch
            End Try
        Else
            footer = footer.Replace("###ExtraField###", "")
        End If

        htmlBuilder.AppendLine(footer)
    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
        htmlBuilder.AppendLine("<!-- SQLStatement=" & mySQLStatement & " -->")
        htmlBuilder.AppendLine("<!-- SQLWhere=" & mySQLWhere & " -->")
        htmlBuilder.AppendLine("<!-- FooterFileName=" & myFooterFileName & " -->")
        htmlBuilder.AppendLine("<!-- ExtraFieldType=" & myExtraFieldType & " -->")
        htmlBuilder.AppendLine("<!-- ExtraFieldQuestion=" & myExtraFieldQuestion & " -->")
    End Sub

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "SQLStatement"
                    mySQLStatement = myKey.Value
                Case "SQLWhere"
                    mySQLWhere = myKey.Value
                Case "FooterFileName"
                    myFooterFileName = myKey.Value
                    myFooterFileName = myFooterFileName.Replace("###CLINICAPATH###", appPath & bar(appPath))
                Case "ExtraFieldType"
                    extraFieldType = myKey.Value
                Case "ExtraFieldQuestion"
                    extraFieldQuestion = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)

        Dim curSQLWhere As String = filtersWhereString
        If curSQLWhere <> "" And sqlWhere <> "" Then curSQLWhere &= " AND " & sqlWhere
        If curSQLWhere = "" Then curSQLWhere = sqlWhere
        If curSQLWhere <> "" And curSQLWhere.StartsWith("WHERE") = False Then curSQLWhere = "WHERE " & curSQLWhere

        mySQLStatement = Me.myReport.replaceCustomVariables(mySQLStatement)
        mySQLStatement = mySQLStatement.Replace("WHEREGEN", curSQLWhere)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        MyBase.saveproperties(properties)
    End Sub

    Public Overloads Shared Function findFooter(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportFooter
        Dim myRapportElement As New ReportFooterSimple(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other footers
        Return ReportBasicFooter.findFooter(classElementName, leRapport, firstDone, firstClass)
    End Function
End Class
