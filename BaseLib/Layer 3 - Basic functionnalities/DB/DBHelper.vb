Public Class DBHelper
    Private Sub New()
    End Sub

    Public Shared Function copyDataTable(ByVal ds As DataSet, ByVal tableName As String, ByVal where As String, ByVal sort As String) As DataTable
        Return copyDataTable(ds.Tables(tableName), where, sort)
    End Function

    Public Shared Function copyDataTable(ByVal table As DataTable, ByVal where As String, ByVal sort As String) As DataTable
        If table Is Nothing Then Return Nothing

        Dim newTable As DataTable = table.Clone()
        For Each curRow As DataRow In table.Select(where, sort)
            newTable.Rows.Add(curRow.ItemArray)
        Next

        Return newTable
    End Function

    Public Shared Function cloneDataColumns(ByVal columns As Generic.List(Of DataColumn)) As Generic.List(Of DataColumn)
        If columns Is Nothing Then Return Nothing

        Dim columnsCloned As New Generic.List(Of DataColumn)
        For Each curColumn As DataColumn In columns
            columnsCloned.Add(New DataColumn(curColumn.ColumnName, curColumn.DataType, curColumn.Expression, curColumn.ColumnMapping))
        Next

        Return columnsCloned
    End Function

    Public Shared Function getTableColumns(ByVal tableName As String) As Generic.List(Of DataColumn)
        Dim ds As DataSet = DBLinker.getInstance.readDBForGrid(tableName, "*", "WHERE 1=2")
        Dim columns As New Generic.List(Of DataColumn)

        If ds IsNot Nothing AndAlso ds.Tables.Count <> 0 Then
            For Each curColumn As DataColumn In ds.Tables(0).Columns
                columns.Add(New DataColumn(curColumn.ColumnName, curColumn.DataType, curColumn.Expression, curColumn.ColumnMapping))
            Next
        End If

        Return columns
    End Function

    Public Shared Function addItemToADBList(ByVal tableName As String, ByVal fieldToWrite As String, ByVal dataToAdd As String, ByVal fieldOfAutoNum As String, Optional ByVal maximumItems As Integer = 0, Optional ByVal comparingMethod As Microsoft.VisualBasic.CompareMethod = CompareMethod.Text, Optional ByVal showErrMSG As Boolean = True, Optional ByVal quoted As Boolean = True, Optional ByVal extraWhereField As String = "", Optional ByVal extraWhereData As String = "") As Object
        If dataToAdd = "" Then Return "null"

        Dim i As Integer
        Dim quotationMark As String = ""
        If quoted = True Then quotationMark = "'"

        Dim myFieldToWrite As String = fieldToWrite
        If extraWhereField <> "" And extraWhereData <> "" Then myFieldToWrite &= ", " & extraWhereField
        Dim myExtraWhere As String = ""
        Dim extraQuoted As Boolean = Not Double.TryParse(extraWhereData, 0)

        If extraWhereField <> "" And extraWhereData <> "" Then
            myExtraWhere = " AND (" & extraWhereField & "=" & IIf(extraQuoted, quotationMark, "") & extraWhereData & IIf(extraQuoted, quotationMark, "") & ")"
        End If

        Dim results(,) As String = DBLinker.getInstance.readDB(tableName, fieldOfAutoNum & ", " & myFieldToWrite, "WHERE 1=1 " & myExtraWhere, False)

        If Not results Is Nothing AndAlso results.Length <> 0 Then
            For i = 0 To results.GetUpperBound(1)
                If results(1, i).ToUpper = dataToAdd.ToUpper Then
                    If extraWhereField = "" And extraWhereData = "" Then
                        Return results(0, i)
                    ElseIf extraWhereField <> "" And extraWhereData <> "" Then
                        If extraWhereData = results(2, i) Then Return results(0, i)
                    End If
                End If
            Next i

            If maximumItems > 0 And (results.GetUpperBound(1) + 1) >= maximumItems Then DBLinker.getInstance.delDB(tableName, fieldOfAutoNum, "(SELECT TOP 1 " & tableName & "." & fieldOfAutoNum & " FROM " & tableName & " WHERE 1=1 " & myExtraWhere & " ORDER BY " & tableName & "." & fieldOfAutoNum & ")", False)
        End If

        dataToAdd = dataToAdd.Replace("'", "''")
        dataToAdd = quotationMark & dataToAdd & quotationMark

        Dim myDataToAdd As String = dataToAdd
        If myExtraWhere <> "" Then
            If extraQuoted Then extraWhereData = quotationMark & extraWhereData & quotationMark
            myDataToAdd &= "," & extraWhereData
        End If

        Dim noItem As Integer = 0
        If DBLinker.getInstance.writeDB(tableName, myFieldToWrite, myDataToAdd, showErrMSG, , True, noItem) Then
            If noItem = 0 Then Return "null"
            Return noItem
        Else
            Return "null"
        End If
    End Function

    Public Shared Function removeItemToADBList(ByVal tableName As String, ByVal fieldOfList As String, ByVal dataToDel As String, ByVal fieldOfAutoNum As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal quoted As Boolean = True, Optional ByVal noUser As Integer = 0, Optional ByVal noFolder As Integer = 0) As Boolean
        If dataToDel = "" Then Exit Function

        Dim quotationMark As String = ""
        Dim extraWhere As String = ""
        If quoted = True Then quotationMark = "'"

        If noUser > 0 Then extraWhere &= " AND (NoUser = " & noUser & ")"
        If noFolder > 0 Then extraWhere &= " AND (NoFolder = " & noFolder & ")"

        Return DBLinker.getInstance.delDB(tableName, fieldOfList, dataToDel, quoted, , showErrMSG, extraWhere)
    End Function



#Region "Statistiques"
    Public Class StatType
        Public errorStr As String
        Public user As Integer
        Public date_Renamed As Date
        Public userFullName As String
        Public actionStr As String
        Public statDataSet As DataSet
    End Class


    Public Shared Function writeStats(ByVal tableName As String, ByVal fieldsToWrite As String, ByVal dataToWrite As String, Optional ByVal statDate As Date = LIMIT_DATE) As Boolean
        If statDate.Equals(LIMIT_DATE) Then statDate = Date.Now
        Return DBLinker.getInstance.writeDB(tableName, fieldsToWrite & ",NoUser,DateHeureCreation", dataToWrite & "," & External.current.currentUser & ",'" & DateFormat.getTextDate(statDate) & " " & DateFormat.getTextDate(statDate, DateFormat.TextDateOptions.FullTime) & "'")
    End Function

    Public Shared Function readStats(ByVal tableName As String, ByVal fieldToSearch As String, ByVal strToSearch As String, ByVal sortingField As String, Optional ByVal quoted As Boolean = True, Optional ByRef sortOrder As DBLinker.SortOrderType = DBLinker.SortOrderType.Descending, Optional ByVal firstIndex As Integer = 0, Optional ByVal aliasForTable As String = "", Optional ByVal addOperatorForStrToSearch As Boolean = True, Optional ByVal extraTables(,) As String = Nothing, Optional ByVal useMainConnection As Boolean = True) As StatType
        Dim i As Integer
        Dim Quotation, SortString, WhereSortingField, tableNames As String
        Dim stat As New DataSet()
        Dim cStat As New StatType
        If sortOrder = DBLinker.SortOrderType.Ascending Then
            SortString = " ORDER BY " & sortingField
        Else
            SortString = " ORDER BY " & sortingField & " DESC"
        End If

        If aliasForTable = "" Then
            aliasForTable = tableName
        Else
            tableName &= " AS " & aliasForTable
        End If

        tableNames = tableName

        If Not extraTables Is Nothing AndAlso extraTables.Length <> 0 Then
            For i = 0 To extraTables.GetUpperBound(1)
                tableNames = tableNames.Replace(tableName, "(" & extraTables(0, i) & " RIGHT JOIN " & tableName & " ON " & extraTables(0, i) & "." & extraTables(1, i) & " = " & aliasForTable & "." & extraTables(1, i) & ")")
            Next i
        End If

        If quoted = True Then Quotation = "'" Else Quotation = ""
        If firstIndex > 0 Then WhereSortingField = " AND " & sortingField & ">=" & firstIndex Else WhereSortingField = ""

        Dim whereStr As String = ""
        If strToSearch <> "" And addOperatorForStrToSearch = True Then
            whereStr = fieldToSearch & "=" & Quotation & strToSearch & Quotation
        Else
            If fieldToSearch <> "" Then whereStr = fieldToSearch
            If strToSearch <> "" Then whereStr &= Quotation & strToSearch & Quotation
        End If

        stat = DBLinker.getInstance.readDBForGrid("ListeAction INNER JOIN (Utilisateurs INNER JOIN " & tableNames & " ON Utilisateurs.NoUser = " & aliasForTable & ".NoUser) ON ListeAction.NoAction = " & aliasForTable & ".NoAction", "DISTINCT *,[Utilisateurs].[Nom]+','+[Utilisateurs].[Prenom] AS FullName, ListeAction.NomAction AS TheAction", "WHERE (" & whereStr & WhereSortingField & ")" & SortString & ";", , , , , useMainConnection)
        If stat Is Nothing Then cStat.errorStr = "ERROR:NOTHINGFOUND" : Return cStat
        If stat.Tables.Count = 0 Then cStat.errorStr = "ERROR:NOTHINGFOUND" : Return cStat
        If stat.Tables("Table").Rows.Count = 0 Then cStat.errorStr = "ERROR:NOTHINGFOUND" : Return cStat

        cStat.statDataSet = stat
        cStat.user = stat.Tables("Table").Rows(0).Item("NoUser")
        cStat.userFullName = stat.Tables("Table").Rows(0).Item("FullName")
        cStat.actionStr = stat.Tables("Table").Rows(0).Item("TheAction")
        cStat.date_Renamed = stat.Tables("Table").Rows(0).Item("DateHeureCreation")
        cStat.errorStr = ""
        Return cStat
    End Function
#End Region

    Public Shared Function deleteFolder(ByVal noUser As Integer, ByVal realPath As String, ByVal folderTableName As String, ByVal pathColumnName As String, ByVal itemsDeletionScript As String, Optional ByVal executeScript As Boolean = True) As String
        Dim script As String = ""
        If noUser = 0 Then
            script = "DELETE FROM " & folderTableName & " WHERE NoUser IS NULL AND (" & pathColumnName & " LIKE '" & realPath.Replace("'", "''") & "\%' OR " & pathColumnName & " = '" & realPath.Replace("'", "''") & "')"
        Else
            Dim curPath As String = ""
            If realPath = "" Then
                curPath = "=''"
            Else
                curPath = " LIKE '" & realPath.Replace("'", "''") & "\%' OR " & pathColumnName & " = '" & realPath.Replace("'", "''") & "'"
            End If
            script = "DELETE FROM " & folderTableName & " WHERE NoUser=" & noUser & " AND (" & pathColumnName & curPath & ")"
        End If

        script &= ";" & itemsDeletionScript

        If executeScript Then DBLinker.executeSQLScript(script)
        Return script
    End Function

End Class
