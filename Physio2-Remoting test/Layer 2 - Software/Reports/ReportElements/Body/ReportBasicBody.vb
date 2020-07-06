Imports CI.Base

Public Class ReportBasicBody
    Implements Clinica.ReportBody

    Protected functionsToApply(,) As String = Nothing
    Private _StyleFileName() As String
    Protected myReport As Report
    Private _OrderDefaultColumn As String = ""
    Private _OrderColumnsName() As String
    Protected mySQLStatement As String = ""
    Protected _sqlStatement As String = ""
    Private _ColsFormat As New Hashtable
    Private ownWorkingThread As Threading.Thread
    Private curOrder As String = ""

#Region "Properties"
    Public ReadOnly Property sqlStatement() As String
        Get
            Return mySQLStatement
        End Get
    End Property

    Public ReadOnly Property name() As String Implements ReportElement.name
        Get
            Return Me.GetType.Name
        End Get
    End Property

    Protected Property currentOrder() As String Implements ReportBody.currentOrder
        Get
            Return curOrder
        End Get
        Set(ByVal value As String)
            curOrder = value
        End Set
    End Property

    Public Property orderColumnsName() As String()
        Get
            Return _OrderColumnsName
        End Get
        Set(ByVal value As String())
            _OrderColumnsName = value
        End Set
    End Property

    Public Property orderDefaultColumn() As String
        Get
            Return _OrderDefaultColumn
        End Get
        Set(ByVal value As String)
            _OrderDefaultColumn = value
        End Set
    End Property

    Public Property colsFormat() As Hashtable
        Get
            Return _ColsFormat
        End Get
        Set(ByVal value As Hashtable)
            _ColsFormat = value
        End Set
    End Property

    Public ReadOnly Property filtersWhereString() As String Implements ReportElement.filtersWhereString
        Get
            If myReport Is Nothing OrElse myReport.reportFilter Is Nothing Then Return ""

            Return myReport.reportFilter.filterResult.filteringWhereBody
        End Get
    End Property

    Public Property styleFileName() As String() Implements ReportElement.styleFileName
        Get
            Return _StyleFileName
        End Get
        Set(ByVal value As String())
            _StyleFileName = value
        End Set
    End Property
#End Region

    Public Sub New(ByRef curReport As Report)
        myReport = curReport
    End Sub

    Public Overridable Function generateHTML() As String Implements IHTMLGenerator.generateHTML
        Dim htmlBuilder As New System.Text.StringBuilder()

        generateHTMLFirstLine(htmlBuilder)
        generateHTMLMiddleLines(htmlBuilder)
        generateHTMLLastLine(htmlBuilder)
        generateHTMLProperties(htmlBuilder)

        RaiseEvent htmlGenerated(htmlBuilder.ToString, True)

        Return htmlBuilder.ToString
    End Function

    Private Sub internalGeneration()
        Try
            Dim htmlBuilder As New System.Text.StringBuilder()

            generateHTMLFirstLine(htmlBuilder)
            generateHTMLProperties(htmlBuilder)
            generateHTMLMiddleLines(htmlBuilder)
            generateHTMLLastLine(htmlBuilder)

            RaiseEvent htmlGenerated(htmlBuilder.ToString, True)
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Protected Overridable Sub generateHTMLFirstLine(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLFirstLine
        htmlBuilder.AppendLine("<!-- Rapport Body -->")
        htmlBuilder.Append("<!-- Rapport Type=").Append(Me.GetType.Name).AppendLine("-->")
    End Sub

    Protected Overridable Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLMiddleLines
        'No Body
    End Sub

    Protected Overridable Sub generateHTMLLastLine(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLLastLine
        htmlBuilder.AppendLine("<!-- End Rapport Body -->")
    End Sub

    Protected Overridable Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLProperties
        If _StyleFileName IsNot Nothing Then
            For i As Integer = 0 To _StyleFileName.GetLength(0) - 1
                Dim cssContent As String = IO.File.ReadAllText(_StyleFileName(i))
                htmlBuilder.Append("<style>").Append(cssContent).Append("</style>")
                If _StyleFileName(i) <> "" Then
                    Dim printStyleSheet As String = _StyleFileName(i).Replace(".css", "-print.css")
                    If IO.File.Exists(printStyleSheet) Then
                        htmlBuilder.Append("<link href=""").Append(printStyleSheet).AppendLine(""" rel=""stylesheet"" type=""text/css"" media=""print"">")
                    End If
                End If
            Next i
            htmlBuilder.Append("<!-- StyleFileName={").Append(String.Join("£", _StyleFileName)).AppendLine("} -->")
        End If
        htmlBuilder.Append("<!-- SQLStatement=").Append(mySQLStatement).AppendLine(" -->")
        htmlBuilder.Append("<!-- FiltersWhereString=").Append(filtersWhereString).AppendLine(" -->")
        htmlBuilder.Append("<!-- OrderDefaultColumn=").Append(_OrderDefaultColumn).AppendLine("-->")
    End Sub

    Public Overridable Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements ReportElement.loadProperties
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            If TypeOf myKey.Value Is Array Then
                For i As Integer = 0 To CType(myKey.Value, Array).Length - 1
                    CType(myKey.Value, Array)(i) = myReport.replaceCustomVariables(CType(myKey.Value, Array)(i))
                Next i
            Else
                myKey.Value = myReport.replaceCustomVariables(myKey.Value)
            End If
            Select Case myKey.Key.ToString
                Case "SQLStatement"
                    _sqlStatement = myKey.Value
                    mySQLStatement = myKey.Value
                Case "StyleFileName"
                    If TypeOf myKey.Value Is Array Then
                        styleFileName = myKey.Value
                    Else
                        styleFileName = New String() {myKey.Value}
                    End If
                    For i As Integer = 0 To _StyleFileName.GetUpperBound(0)
                        _StyleFileName(i) = _StyleFileName(i).Replace("###CLINICAPATH###", appPath & bar(appPath))
                    Next i
                Case "ColsFormat"
                    Dim cols() As String = myKey.Value
                    Dim i As Integer
                    For i = 0 To cols.GetUpperBound(0)
                        Dim sCol() As String = cols(i).Split(New Char() {"="})
                        _ColsFormat.Add(sCol(0), sCol(1))
                    Next i
                Case "OrderDefaultColumn"
                    orderDefaultColumn = myKey.Value
                Case "OrderColumnsName"
                    orderColumnsName = myKey.Value
                Case Else
            End Select
        Next

        Dim curSQLWhere As String = filtersWhereString
        If curSQLWhere.StartsWith("WHERE") = False And curSQLWhere <> "" Then curSQLWhere = "WHERE " & curSQLWhere
        mySQLStatement = Me.myReport.replaceCustomVariables(mySQLStatement)
        mySQLStatement = mySQLStatement.Replace("WHEREGEN", curSQLWhere)
        curOrder = chooseOrder()
        If curOrder <> "" Then
            If curOrder.EndsWith("DESC") Then
                curOrder = "[" & curOrder.Replace(" DESC", "") & "] DESC"
            Else
                curOrder = "[" & curOrder & "]"
            End If
        End If
        mySQLStatement = mySQLStatement.Replace("ORDERGEN", curOrder)
    End Sub

    Public Overridable Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements ReportElement.saveProperties
        properties.Add("StyleFileName", _StyleFileName)
        properties.Add("SQLStatement", _sqlStatement)
        properties.Add("FiltersWhereString", filtersWhereString)
        properties.Add("OrderColumnsName", _OrderColumnsName)
        properties.Add("OrderDefaultColumn", _OrderDefaultColumn)
        properties.Add("ColsFormat", _ColsFormat)
    End Sub

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString
        Dim prop As New Hashtable
        Me.saveProperties(prop)

        Return PropertiesHelper.transformProperties(prop)
    End Function

    Public Shared Function findBody(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportBody
        Dim myRapportElement As New ReportBasicBody(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other bodys
        Return ReportBodySimple.findBody(classElementName, leRapport, True, firstClass)
    End Function

    Public Event htmlGenerated(ByVal html As String, ByVal ishtmlGenerated As Boolean) Implements IHTMLGenerator.htmlGenerated

    Public Sub startHTMLGeneration() Implements IHTMLGenerator.startHTMLGeneration
        'Quitte si la génération est déjà en cours
        If Not ownWorkingThread Is Nothing Then
            If ownWorkingThread.IsAlive Then Exit Sub
        Else
            ownWorkingThread = New Threading.Thread(AddressOf internalGeneration)
        End If

        ownWorkingThread.IsBackground = True
        ownWorkingThread.Start()
    End Sub

    Public Sub stopHTMLGeneration() Implements IHTMLGenerator.stopHTMLGeneration
        ownWorkingThread = Nothing
    End Sub

    Private Function chooseOrder() As String
        Dim orderColumn As String = orderDefaultColumn
        If Not orderColumnsName Is Nothing Then
            If orderColumnsName.Length > 0 Then
                Dim tmpColumns() As String = orderColumnsName.Clone()
                Dim nbColumns As Integer = tmpColumns.GetUpperBound(0)
                For i As Integer = 0 To nbColumns
                    If tmpColumns(i).EndsWith(" ASK") Then
                        tmpColumns(i) = tmpColumns(i).Replace(" ASK", "").Replace(" DESC", "")
                        ReDim Preserve tmpColumns(tmpColumns.GetUpperBound(0) + 1)
                        tmpColumns(tmpColumns.GetUpperBound(0)) = tmpColumns(i) & " (Décroissant)"
                        tmpColumns(i) = tmpColumns(i) & " (Croissant)"
                    End If
                    If tmpColumns(i).EndsWith(" DESC") Then tmpColumns(i) = tmpColumns(i).Replace(" DESC", "") & " (Décroissant)"
                Next i
                Dim myMultiChoice As New multichoice
                Dim selColumn As String = orderDefaultColumn
                If selColumn.EndsWith(" DESC") Then
                    selColumn = selColumn.Replace(" DESC", "")
                    If Array.IndexOf(tmpColumns, selColumn) < 0 Then selColumn &= " (Décroissant)"
                Else
                    If Array.IndexOf(tmpColumns, selColumn) < 0 Then selColumn &= " (Croissant)"
                End If

                If myReport.filtered Then
                    orderColumn = selColumn
                Else
                    orderColumn = myMultiChoice.GetChoice("Veuillez choisir la colonne à trier", String.Join("§", tmpColumns), , "§", , selColumn)
                End If

                If orderColumn.StartsWith("ERROR") Then orderColumn = ""
                If orderColumn.EndsWith("(Croissant)") Then
                    orderColumn = orderColumn.Replace(" (Croissant)", "")
                ElseIf orderColumn.EndsWith("(Décroissant)") Then
                    orderColumn = orderColumn.Replace("(Décroissant)", "") & "DESC"
                End If
                If orderColumn = "" Then orderColumn = orderDefaultColumn
            End If
        End If

        Return orderColumn
    End Function

    Protected Function transformSQLStatement(ByVal mySQLStatement As String) As String
        Dim myReplaceSearch As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(mySQLStatement, "Replace\(((\[)?)(?<field>\b\w+\b)((\])?),'(?<search>[^']+)','(?<replace>[^']+)'\) AS ((\[)?)(?<alias>\b[^\]]+\b)((\])?)(?<end>[ ]*(,|FROM))", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Dim myMatch As System.Text.RegularExpressions.Match
        With myReplaceSearch
            If .Count > 0 Then
                ReDim functionsToApply(4, .Count - 1)
                mySQLStatement = System.Text.RegularExpressions.Regex.Replace(mySQLStatement, "Replace\((?<field>((\[)?)\b\w+\b((\])?)),'(?<search>[^']+)','(?<replace>[^']+)'\) AS (?<alias>((\[)?)\b[^\]]+\b((\])?))(?<end>[ ]*(,|FROM))", "${field} AS ${alias}${end}", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            End If
            Dim i As Integer = 0
            For Each myMatch In myReplaceSearch
                functionsToApply(0, i) = "Replace"
                functionsToApply(1, i) = myMatch.Groups("field").Value
                functionsToApply(2, i) = myMatch.Groups("search").Value
                functionsToApply(3, i) = myMatch.Groups("replace").Value
                If functionsToApply(3, i).ToLower = "vbCrLf" Then functionsToApply(3, i) = "<br>"
                functionsToApply(4, i) = myMatch.Groups("alias").Value
            Next
        End With

        Dim wherePartDate As String = ""
        Dim dateSelector As New DateSelectorReturn
        If Not myReport.reportFilter Is Nothing Then
            With CType(myReport.reportFilter, FilterComposite)
                If .indexOf("Month") <> -1 Then
                    If Not CType(.Item(.indexOf("Month")), FilterMonth).currentReturn Is Nothing Then
                        dateSelector = CType(.Item(.indexOf("Month")), FilterMonth).currentReturn
                        wherePartDate = CType(.Item(.indexOf("Month")), FilterMonth).currentReturn.whereStr
                    End If
                ElseIf .indexOf("FromTo") <> -1 Then
                    If Not CType(.Item(.indexOf("FromTo")), FilterFromTo).currentReturn Is Nothing Then
                        dateSelector = CType(.Item(.indexOf("FromTo")), FilterFromTo).currentReturn
                        wherePartDate = CType(.Item(.indexOf("FromTo")), FilterFromTo).currentReturn.whereStr
                    End If
                End If
            End With

            If wherePartDate <> "" Then wherePartDate = " AND " & wherePartDate
        End If
        mySQLStatement = mySQLStatement.Replace("WHEREPART:DATES", wherePartDate)
        Dim date1 As String = ""
        Dim date2 As String = ""
        If dateSelector IsNot Nothing Then
            date1 = IIf(dateSelector.firstDate.Equals(LIMIT_DATE), "NULL", "'" & DateFormat.getTextDate(dateSelector.firstDate) & "'")
            date2 = IIf(dateSelector.secondDate.Equals(LIMIT_DATE), "NULL", "'" & DateFormat.getTextDate(dateSelector.secondDate) & "'")
        End If
        mySQLStatement = mySQLStatement.Replace("WHEREPART:DATE1", date1)
        mySQLStatement = mySQLStatement.Replace("WHEREPART:DATE2", date2)

        Return mySQLStatement
    End Function

    Protected Function applyFunctions(ByVal input As String, ByVal functionsIndex As Integer) As String
        If functionsToApply Is Nothing OrElse functionsToApply.Length = 0 Then Return input

        Select Case functionsToApply(0, functionsIndex).ToUpper
            Case "REPLACE"
                input = input.Replace(functionsToApply(2, functionsIndex), functionsToApply(3, functionsIndex))
            Case Else

        End Select

        Return input
    End Function
End Class
