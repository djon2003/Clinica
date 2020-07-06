'This object is Singleton
Public Class DBLinker
    Inherits ContextBoundObject

    Private waitingTriggerThread As Threading.Thread
    Private Shared mySelf As DBLinker
    Private Shared con As SqlClient.SqlConnection
    Private _dbConnected As Boolean = False
    Private _KeepAlive As Boolean = False
    Private lastSqlString As String = ""
    Private dbLockingObject As New Object
    Private lockingMutex As New System.Threading.Mutex(False, "Locking")
    Private _NbTransactions As Integer = 0
    Private lastSqlCommand As SqlClient.SqlCommand
    Private Shared lastWriteCon As SqlClient.SqlConnection
    Private spParameters As New Hashtable

    Private Sub new()

    End Sub

    Public Enum SortOrderType
        Ascending = 0
        Descending = 1
    End Enum

    Public Enum QueryTypes
        Add = 0
    End Enum

    Public Class StatType
        Public errorStr As String
        Public user As Integer
        Public date_Renamed As Date
        Public userFullName As String
        Public actionStr As String
        Public statDataSet As DataSet
    End Class

    Public Event message(ByVal source As String, ByVal message As String)

#Region "Propriétés publiques"
    Public Property dbConnected() As Boolean
        Get
            Return _DBConnected
        End Get
        Set(ByVal Value As Boolean)
            _DBConnected = Value
            If Value = True Then
                If DBLinker.con.State = ConnectionState.Open Then Exit Property

                Try
                    If DBLinker.con.State = ConnectionState.Closed Or DBLinker.con.State = ConnectionState.Broken Then DBLinker.con.Open()
                Catch ex As Exception
                    _DBConnected = False
                    DBLinker.con.Close()
                    DBLinker.con = Nothing
                    Throw ex
                End Try
            Else
                If DBLinker.con.State <> ConnectionState.Closed Then DBLinker.con.Close()
            End If
        End Set
    End Property

    Public Property keepAlive() As Boolean
        Get
            Return _KeepAlive
        End Get
        Set(ByVal Value As Boolean)
            _KeepAlive = Value
        End Set
    End Property
#End Region

#Region "Statistiques"
    Public Function readStats(ByVal tableName As String, ByVal fieldToSearch As String, ByVal strToSearch As String, ByVal sortingField As String, Optional ByVal quoted As Boolean = True, Optional ByRef sortOrder As sortOrderType = sortOrderType.Descending, Optional ByVal firstIndex As Integer = 0, Optional ByVal aliasForTable As String = "", Optional ByVal addOperatorForstrToSearch As Boolean = True, Optional ByVal extraTables(,) As String = Nothing, Optional ByVal useMainConnection As Boolean = True) As StatType
        Dim i As Integer
        Dim Quotation, SortString, WhereSortingField, tableNames As String
        Dim stat As New DataSet()
        Dim cStat As New StatType
        If SortOrder = SortOrderType.Ascending Then
            SortString = " ORDER BY " & SortingField
        Else
            SortString = " ORDER BY " & SortingField & " DESC"
        End If

        If AliasForTable = "" Then
            AliasForTable = TableName
        Else
            TableName &= " AS " & AliasForTable
        End If

        TableNames = TableName

        If Not ExtraTables Is Nothing AndAlso ExtraTables.Length <> 0 Then
            For i = 0 To ExtraTables.GetUpperBound(1)
                TableNames = TableNames.Replace(TableName, "(" & ExtraTables(0, i) & " RIGHT JOIN " & TableName & " ON " & ExtraTables(0, i) & "." & ExtraTables(1, i) & " = " & AliasForTable & "." & ExtraTables(1, i) & ")")
            Next i
        End If

        If Quoted = True Then Quotation = "'" Else Quotation = ""
        If FirstIndex > 0 Then WhereSortingField = " AND " & SortingField & ">=" & FirstIndex Else WhereSortingField = ""

        Dim whereStr As String = ""
        If StrToSearch <> "" And AddOperatorForStrToSearch = True Then
            WhereStr = FieldToSearch & "=" & Quotation & StrToSearch & Quotation
        Else
            If FieldToSearch <> "" Then WhereStr = FieldToSearch
            If StrToSearch <> "" Then WhereStr &= Quotation & StrToSearch & Quotation
        End If

        Stat = ReadDBForGrid("ListeAction INNER JOIN (Utilisateurs INNER JOIN " & TableNames & " ON Utilisateurs.NoUser = " & AliasForTable & ".NoUser) ON ListeAction.NoAction = " & AliasForTable & ".NoAction", "DISTINCT *,[Utilisateurs].[Nom]+','+[Utilisateurs].[Prenom] AS FullName, ListeAction.NomAction AS TheAction", "WHERE (" & WhereStr & WhereSortingField & ")" & SortString & ";", , , , , UseMainConnection)
        If Stat Is Nothing Then CStat.ErrorStr = "ERROR:NOTHINGFOUND" : Return CStat
        If Stat.Tables.Count = 0 Then CStat.ErrorStr = "ERROR:NOTHINGFOUND" : Return CStat
        If Stat.Tables("Table").Rows.Count = 0 Then CStat.ErrorStr = "ERROR:NOTHINGFOUND" : Return CStat

        CStat.StatDataSet = Stat
        CStat.User = Stat.Tables("Table").Rows(0).Item("NoUser")
        CStat.UserFullName = Stat.Tables("Table").Rows(0).Item("FullName")
        CStat.ActionStr = Stat.Tables("Table").Rows(0).Item("TheAction")
        CStat.Date_Renamed = Stat.Tables("Table").Rows(0).Item("DateHeureCreation")
        CStat.ErrorStr = ""
        Return CStat
    End Function
#End Region

    Private readDBError As System.Exception

    Private Class OneTimeReadError
        Inherits Exception

        Public Function getError() As Exception
            Dim myTempException As Exception = Me
            Return MyTempException
        End Function
    End Class

#Region "Private functions & properties"
    Private Function internalReadDB(ByVal sqlStatement As String, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True) As String(,)
        Dim myDataSet As DataSet = InternalReadDBForGrid(SQLStatement, , , , , , UseMainConnection)
        Dim results(,) As String = Nothing

        If myDataSet Is Nothing OrElse myDataSet.Tables.Count = 0 Then Return Results
        If myDataSet.Tables(0).Rows.Count = 0 Then Return Results

        With myDataSet.Tables(0)
            ReDim Results(.Columns.Count - 1, .Rows.Count - 1)

            For i As Integer = 0 To .Columns.Count - 1
                For j As Integer = 0 To .Rows.Count - 1
                    Results(i, j) = .Rows(j)(i).ToString
                Next j
            Next i
        End With

        Return Results
    End Function

    'This method is not synchronized
    Private Function internalReadDBForGrid2(ByVal sqlStatement As String, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal didOnce As Boolean = False, Optional ByVal sqlType As System.Data.CommandType = CommandType.Text, Optional ByVal curCon As SqlClient.SqlConnection = Nothing, Optional ByVal params() As String = Nothing) As DataSet
        If DA Is Nothing Then DA = New SqlClient.SqlDataAdapter
        If DS Is Nothing Then DS = New DataSet()
        Dim cmd As New SqlClient.SqlCommand()

        LastSqlCommand = cmd

        cmd.CommandText = SQLStatement
        cmd.CommandType = SQLType
        cmd.Connection = CurCon
        cmd.CommandTimeout = 10000

        If SQLType = CommandType.StoredProcedure AndAlso Params IsNot Nothing AndAlso Params.Length <> 0 Then
            If spParameters.Contains(SQLStatement) Then
                cmd.Parameters.AddRange(spParameters(SQLStatement))
            Else
                SqlClient.SqlCommandBuilder.DeriveParameters(cmd)
                Dim cmdParams() As SqlClient.SqlParameter
                ReDim cmdParams(cmd.Parameters.Count - 1)
                cmd.Parameters.CopyTo(cmdParams, 0)
                spParameters.Add(SQLStatement, cmdParams)
            End If
            For i As Integer = 0 To Math.Min(Params.Length, cmd.Parameters.Count) - 1
                cmd.Parameters.Item(i + 1).Value = Params(i)
            Next i
        End If

        Try
            DA.SelectCommand = cmd
            DA.Fill(DS, TableMappingName)

        Catch e As SqlClient.SqlException
            If DidOnce Then Throw e

            Return InternalReadDBForGrid2(SQLStatement, DA, TableMappingName, DS, True, SQLType, CurCon)
        Catch ex As Exception
            If DidOnce Then Throw ex

            Return InternalReadDBForGrid2(SQLStatement, DA, TableMappingName, DS, True, SQLType, CurCon)
        Finally
            cmd.Parameters.Clear()
            cmd.Dispose()
        End Try

        Return DS
    End Function

    Private Function internalReadDBForGrid(ByVal sqlStatement As String, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal didOnce As Boolean = False, Optional ByVal sqlType As System.Data.CommandType = CommandType.Text, Optional ByVal useMainConnection As Boolean = True, Optional ByVal params() As String = Nothing) As DataSet
        Dim curCon As SqlClient.SqlConnection = DBLinker.con
        Dim closeCon As Boolean = False
        If CurCon Is Nothing Then CurCon = DBLinker.con

        Dim selfOpenDB As Boolean = False
        If CurCon.Equals(DBLinker.con) AndAlso DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpenDB = True
        If CurCon.State <> ConnectionState.Open Then CurCon.Open() : CloseCon = True

        If UseMainConnection = False Then
            CurCon = MakeNewConnection()
            CurCon.Open()
            CloseCon = True
        Else
            If DidOnce = False Then LockingMutex.WaitOne()
        End If

        Dim returning As DataSet = InternalReadDBForGrid2(SQLStatement, DA, TableMappingName, DS, DidOnce, SQLType, CurCon, Params)

        If CurCon.Equals(DBLinker.con) AndAlso SelfOpenDB = True Then DBLinker.GetInstance().DBConnected = False
        If CloseCon Then
            CurCon.Close()
            CurCon.Dispose()
        End If
        If CurCon.Equals(DBLinker.con) AndAlso DidOnce = False Then LockingMutex.ReleaseMutex()

        Return returning

    End Function

    Private _WaitingTriggerNo As Integer = 0
    Private myUniqueLock As Object = New Object

    Private Property waitingNo() As Integer
        Get
            Return _WaitingTriggerNo
        End Get
        Set(ByVal value As Integer)
            _WaitingTriggerNo = value
        End Set
    End Property

    Public ReadOnly Property isBatching() As Boolean
        Get
            Return _Batching.Count <> 0
        End Get
    End Property
#End Region

    Public Sub cancelCommand()
        If LastSqlCommand IsNot Nothing Then
            Try
                LastSqlCommand.Cancel()
            Catch ex As Exception
                'Impossible d'annuler
            End Try
            DBConnected = False
            DBConnected = True
        End If
    End Sub

    Private _Batching As New System.Collections.Stack

    Public Sub beginBatching()
        _Batching.Push(New ArrayList)
    End Sub

    Public Function endBatching() As Boolean
        If IsBatching = False Then Return False

        Dim poped As Object = _Batching.Pop()
        Dim script As String = String.Join(vbCrLf, CType(poped, ArrayList).ToArray("".GetType))
        Try
            DBLinker.ExecuteSQLScript(script)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Sub clearBatching()
        _Batching.Clear()
    End Sub

    Public Sub beginTransaction()
        _NbTransactions += 1
        ExecuteSQLScript("BEGIN TRANSACTION T" & _NbTransactions & vbCrLf & "SAVE TRANSACTION T" & _NbTransactions, False)
    End Sub

    Public Sub commitTransaction()
        If _NbTransactions = 0 Then Exit Sub

        ExecuteSQLScript("COMMIT TRANSACTION T" & _NbTransactions, False)
        _NbTransactions -= 1
    End Sub

    Public Sub rollbackTransaction()
        If _NbTransactions = 0 Then Exit Sub

        ExecuteSQLScript("ROLLBACK TRANSACTION T" & _NbTransactions, False)
        _NbTransactions -= 1
    End Sub

    Public Sub rollbackAllTransactions()
        If _NbTransactions = 0 Then Exit Sub

        ExecuteSQLScript("ROLLBACK TRANSACTION", False)
        _NbTransactions = 0
    End Sub

    Public Function getNbTransactions(Optional ByVal fromServer As Boolean = False) As Integer
        If FromServer Then
            Dim curNb() As String = Me.ReadOneDBField("SELECT @@TRANCOUNT")
            If curNb IsNot Nothing AndAlso curNb.Length <> 0 Then Return curNb(0)

            Throw New Exception("Erreur de lecture du nombre de transactions depuis le serveur")
        End If

        Return _NbTransactions
    End Function

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

        If executeScript Then DBLinker.ExecuteSQLScript(script)
        Return script
    End Function

    Public Shared Function getInstance() As DBLinker
        If MySelf Is Nothing Then MySelf = New DBLinker
        Return MySelf
    End Function

    Public Function readDB(ByVal sqlScript As String, Optional ByVal useMainConnection As Boolean = True) As String(,)
        Return InternalReadDB(SQLScript, , UseMainConnection)
    End Function

    Public Function readDB(ByVal tableName As String, ByVal fieldsToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal showErrMsg As Boolean = True, Optional ByVal useMainConnection As Boolean = True) As String(,)
        'LockingMutex.WaitOne()
        'Console.WriteLine("ReadDB1 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Dim Results(,), sqlStatement As String

        If WhereSource <> "" And WhereSource.StartsWith(" ") = False Then WhereSource = " " & WhereSource
        SQLStatement = "SELECT " & FieldsToSelect & " FROM " & TableName & WhereSource
        SQLStatement = SQLStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        'Console.WriteLine("ReadDB2 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Try
            Results = InternalReadDB(SQLStatement, , UseMainConnection)
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            If ShowErrMsg Then MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & SQLStatement & vbCrLf & vbCrLf & e.Message)
            ReadDBError = e
            Results = Nothing
        Finally
            '   LockingMutex.ReleaseMutex()
        End Try

        Return Results
    End Function

    Public Function executestoredProcedure(ByVal storedProc As String, ByVal params() As String, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing) As DataSet
        'LockingMutex.WaitOne()
        'Console.WriteLine("ESPForGrid1 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Dim sqlStatement As String = StoredProc

        'Console.WriteLine("ESPForGrid2 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Try
            DS = InternalReadDBForGrid(SQLStatement, DA, TableMappingName, DS, , CommandType.StoredProcedure, , Params)
        Catch ex As Exception
            MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & SQLStatement & vbCrLf & vbCrLf & ex.Message)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return DS
    End Function

    Public Function readDBForGrid(ByVal tableName As String, ByVal fieldsToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal useMainConnection As Boolean = True) As DataSet
        'LockingMutex.WaitOne()
        'Console.WriteLine("ReadDBForGrid1 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Dim sqlStatement As String

        If WhereSource <> "" AndAlso WhereSource.Trim().StartsWith("WHERE") = False Then WhereSource = "WHERE " & WhereSource
        If WhereSource <> "" AndAlso WhereSource.StartsWith(" ") = False Then WhereSource = " " & WhereSource
        SQLStatement = "SELECT " & FieldsToSelect & " FROM " & TableName & WhereSource
        SQLStatement = SQLStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        'Console.WriteLine("ReadDBForGrid2 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Try
            DS = InternalReadDBForGrid(SQLStatement, DA, TableMappingName, DS, , , UseMainConnection)
        Catch ex As Threading.ThreadAbortException
            'Aborted by user
        Catch ex As Exception
            MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & SQLStatement & vbCrLf & vbCrLf & ex.Message)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return DS
    End Function

    Public Function readDBForGrid(ByVal sqlStatement As String, Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal useMainConnection As Boolean = True) As DataSet
        'LockingMutex.WaitOne()
        'Console.WriteLine("ReadDBForGrid1-1 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Try
            DS = InternalReadDBForGrid(SQLStatement, DA, TableMappingName, DS, , , UseMainConnection)
        Catch ex As Threading.ThreadAbortException
            'Aborted by user
        Catch ex As Exception
            MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & SQLStatement & vbCrLf & vbCrLf & ex.Message)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return DS
    End Function

    Public Function readOneDBField(ByVal tableName As String, ByVal fieldToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal sortedTable As Boolean = False, Optional ByVal invertSort As Boolean = False, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True) As String()
        'LockingMutex.WaitOne()
        'Console.WriteLine("ReadOneDBField1 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Dim i As Integer
        Dim results() As String = {}, TempResults(,), SQLStatement As String

        If WhereSource <> "" And WhereSource.StartsWith(" ") = False Then WhereSource = " " & WhereSource
        SQLStatement = "SELECT " & FieldToSelect
        If TableName <> "" Then SQLStatement &= " FROM " & TableName & WhereSource

        SQLStatement = SQLStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        'Console.WriteLine("ReadOneDBField2 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Try
            TempResults = InternalReadDB(SQLStatement, , UseMainConnection)
            If Not TempResults Is Nothing AndAlso TempResults.Length <> 0 Then
                ReDim Results(TempResults.GetUpperBound(1))
                For i = 0 To TempResults.GetUpperBound(1)
                    Results(i) = TempResults(0, i)
                Next i
            End If
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            If ShowErrMSG Then MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & SQLStatement & vbCrLf & vbCrLf & e.Message)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        If Not Results Is Nothing AndAlso Results.Length <> 0 Then
            If SortedTable = True Then Array.Sort(Results)
            If InvertSort = True Then Array.Reverse(Results)
        End If
        Return Results
    End Function

    Public Function readOneDBField(ByVal sqlStatement As String, Optional ByVal sortedTable As Boolean = False, Optional ByVal invertSort As Boolean = False, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True) As String()
        'LockingMutex.WaitOne()
        'Console.WriteLine("ReadOneDBField2 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Dim i As Integer
        Dim results() As String = {}, TempResults(,) As String = {}

        Try
            TempResults = InternalReadDB(SQLStatement, , UseMainConnection)
            If Not TempResults Is Nothing AndAlso TempResults.Length <> 0 Then
                ReDim Results(TempResults.GetUpperBound(1))
                For i = 0 To TempResults.GetUpperBound(1)
                    Results(i) = TempResults(0, i)
                Next i
            End If
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            If ShowErrMSG Then MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & SQLStatement & vbCrLf & vbCrLf & e.Message)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        If Not Results Is Nothing AndAlso Results.Length <> 0 Then
            If SortedTable = True Then Array.Sort(Results)
            If InvertSort = True Then Array.Reverse(Results)
        End If
        Return Results
    End Function

    Public Function addItemToADBList(ByVal tableName As String, ByVal fieldToWrite As String, ByVal dataToAdd As String, ByVal fieldOfAutoNum As String, Optional ByVal maximumItems As Integer = 0, Optional ByVal comparingMethod As Microsoft.VisualBasic.CompareMethod = CompareMethod.Text, Optional ByVal showErrMSG As Boolean = True, Optional ByVal quoted As Boolean = True, Optional ByVal extraWhereField As String = "", Optional ByVal extraWhereData As String = "") As Object
        If DataToAdd = "" Then Return "null"

        Dim i As Integer
        Dim quotationMark As String = ""
        If Quoted = True Then QuotationMark = "'"

        Dim myFieldToWrite As String = FieldToWrite
        If ExtraWhereField <> "" And ExtraWhereData <> "" Then MyFieldToWrite &= ", " & ExtraWhereField
        Dim myExtraWhere As String = ""
        Dim extraQuoted As Boolean = Not Double.TryParse(ExtraWhereData, 0)

        If ExtraWhereField <> "" And ExtraWhereData <> "" Then
            MyExtraWhere = " AND (" & ExtraWhereField & "=" & IIf(ExtraQuoted, QuotationMark, "") & ExtraWhereData & IIf(ExtraQuoted, QuotationMark, "") & ")"
        End If

        ReadDBError = Nothing
        Dim results(,) As String = ReadDB(TableName, FieldOfAutoNum & ", " & MyFieldToWrite, "WHERE 1=1 " & MyExtraWhere, False)

        If ReadDBError Is Nothing Then
            If Not Results Is Nothing AndAlso Results.Length <> 0 Then
                For i = 0 To Results.GetUpperBound(1)
                    If Results(1, i).ToUpper = DataToAdd.ToUpper Then
                        If ExtraWhereField = "" And ExtraWhereData = "" Then
                            Return Results(0, i)
                        ElseIf ExtraWhereField <> "" And ExtraWhereData <> "" Then
                            If ExtraWhereData = Results(2, i) Then Return Results(0, i)
                        End If
                    End If
                Next i

                If MaximumItems > 0 And (Results.GetUpperBound(1) + 1) >= MaximumItems Then DelDB(TableName, FieldOfAutoNum, "(SELECT TOP 1 " & TableName & "." & FieldOfAutoNum & " FROM " & TableName & " WHERE 1=1 " & MyExtraWhere & " ORDER BY " & TableName & "." & FieldOfAutoNum & ")", False)
            End If
        Else
            REM Ne Fonctionne pas !!!! DBLinker.GetInstance.AlterDB("ALTER TABLE " & TableName & " ADD COLUMN " & FieldToWrite & " TEXT (250);", False)
            REM If Not ReadDBError Is Nothing Then MsgBox("NOT")
            Return "null"
        End If

        DataToAdd = DataToAdd.Replace("'", "''")
        DataToAdd = QuotationMark & DataToAdd & QuotationMark

        Dim myDataToAdd As String = DataToAdd
        If MyExtraWhere <> "" Then
            If ExtraQuoted Then ExtraWhereData = QuotationMark & ExtraWhereData & QuotationMark
            MyDataToAdd &= "," & ExtraWhereData
        End If

        Dim noItem As Integer = 0
        If WriteDB(TableName, MyFieldToWrite, MyDataToAdd, ShowErrMSG, , , , True, NoItem) Then
            If NoItem = 0 Then Return "null"
            Return NoItem
        Else
            Return "null"
        End If
    End Function

    Public Function removeItemToADBList(ByVal tableName As String, ByVal fieldOfList As String, ByVal dataToDel As String, ByVal fieldOfAutoNum As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal quoted As Boolean = True, Optional ByVal noUser As Integer = 0, Optional ByVal noFolder As Integer = 0) As Boolean
        If DataToDel = "" Then Exit Function

        Dim quotationMark As String = ""
        Dim extraWhere As String = ""
        If Quoted = True Then QuotationMark = "'"

        If NoUser > 0 Then ExtraWhere &= " AND (NoUser = " & NoUser & ")"
        If NoFolder > 0 Then ExtraWhere &= " AND (NoFolder = " & NoFolder & ")"

        Return DelDB(TableName, FieldOfList, DataToDel, Quoted, , ShowErrMSG, ExtraWhere)
    End Function

    Public Function writeDB(ByVal sqlStatement As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal waitForDB As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = 0) As Boolean
        Return innerWriteDB(SQLStatement, ShowErrMSG, WaitForDB, DidOnce, UseMainConnection, ByPassBatching, GetScopeIdentity)
    End Function

    Public Function writeDB(ByVal tableName As String, ByVal fieldsToWrite As String, ByVal dataToWrite As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal waitForDB As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = -1) As Boolean
        Dim sqlStatement As String = "INSERT INTO " & TableName & " (" & FieldsToWrite.Replace(vbCrLf, "\n") & ") VALUES(" & DataToWrite.Replace(vbCrLf, "\n") & ")"
        Return innerWriteDB(SQLStatement, ShowErrMSG, WaitForDB, DidOnce, UseMainConnection, ByPassBatching, GetScopeIdentity)
    End Function

    Private Function innerWriteDB(ByVal sqlStatement As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal waitForDB As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = -1) As Boolean
        If ByPassBatching = False AndAlso IsBatching Then
            CType(_Batching.Peek, ArrayList).Add(SQLStatement)
            Return True
        End If

        Dim curCon As SqlClient.SqlConnection = DBLinker.con
        Dim closeCon As Boolean = False
        If UseMainConnection = False Then
            CurCon = MakeNewConnection()
            CurCon.Open()
            CloseCon = True
        Else
            LockingMutex.WaitOne()
        End If
        lastWriteCon = CurCon

        Dim returns As Boolean = False
        Dim selfOpenDB As Boolean = False
        If UseMainConnection AndAlso DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpenDB = True

        Dim cmd As IDbCommand = CurCon.CreateCommand()
        cmd.CommandTimeout = 60 '1 minute, au lieu de 30 secondes
        LastSqlCommand = cmd
        cmd.CommandText = SQLStatement

        Try
            cmd.ExecuteNonQuery()

            If GetScopeIdentity = 0 Then
                Dim noIdent As DataSet = Me.InternalReadDBForGrid2("SELECT SCOPE_IDENTITY()", , , , , , CurCon)
                If NoIdent Is Nothing OrElse NoIdent.Tables(0).Rows.Count = 0 Then
                    GetScopeIdentity = 0
                Else
                    GetScopeIdentity = NoIdent.Tables(0).Rows(0)(0)
                End If
            End If

            Returns = True
        Catch e As SqlClient.SqlException
            If DidOnce Then
                If _NbTransactions = 0 Then 'Priorité aux transactions
                    Throw New Exception("DBLinker-" & SQLStatement, e)
                Else
                    Exit Try
                End If
            Else
            End If

            Return innerWriteDB(SQLStatement, ShowErrMSG, WaitForDB, True, UseMainConnection, ByPassBatching, GetScopeIdentity)
        Catch e As Exception
             Returns = False
        Finally
            If UseMainConnection AndAlso SelfOpenDB = True Then DBLinker.GetInstance().DBConnected = False
            If CloseCon Then
                CurCon.Close()
                CurCon.Dispose()
                '                        Else
                LockingMutex.ReleaseMutex()
            End If
        End Try

        Return Returns
    End Function

    Public Function alterDB(ByVal sqlCommand As String, Optional ByVal showErrMSG As Boolean = True) As Boolean
        'LockingMutex.WaitOne()

        Dim selfOpenDB As Boolean = False
        If DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpenDB = True

        Dim cmd As IDbCommand = con.CreateCommand()
        LastSqlCommand = cmd
        cmd.CommandText = SQLCommand

        Try
            cmd.ExecuteNonQuery()
        Catch e As Exception
        Finally
            If SelfOpenDB = True Then DBLinker.GetInstance().DBConnected = False
            'LockingMutex.ReleaseMutex()
        End Try
    End Function

    Public Function delDB(ByVal tableName As String, Optional ByVal whereFieldName As String = "", Optional ByVal whereFieldCondition As String = "", Optional ByVal quoted As Boolean = True, Optional ByVal fieldToDel As String = "", Optional ByVal showErrMSG As Boolean = True, Optional ByVal extraWhereSource As String = "", Optional ByVal waitForSynch As Boolean = True, Optional ByVal whereOperator As String = "=", Optional ByVal didOnce As Boolean = False, Optional ByRef nbAffectedRows As Integer = -1) As Boolean
        Dim quotation As String = "'"
        Dim whereCmd As String = ""
        Dim sqlStatement As String = ""
        If Quoted = False Then Quotation = ""
        Dim doneBool As Boolean = False

        If ExtraWhereSource <> "" Then ExtraWhereSource = " " & ExtraWhereSource

        If WhereFieldName <> "" And WhereFieldCondition <> "" Then
            WhereCmd = " WHERE (((" & TableName & "." & WhereFieldName & ") " & WhereOperator & " " & Quotation & WhereFieldCondition & Quotation & ")" & ExtraWhereSource & ");"
            WhereCmd = WhereCmd.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        End If

        SQLStatement = "DELETE " & FieldToDel & " FROM " & TableName & WhereCmd

        If IsBatching Then 'Batch delete and return True
            CType(_Batching.Peek, ArrayList).Add(SQLStatement)
            Return True
        End If

        LockingMutex.WaitOne()

        Dim selfOpenDB As Boolean = False
        If DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpenDB = True

        Dim cmd As IDbCommand = con.CreateCommand()
        LastSqlCommand = cmd

        Try
            cmd.CommandText = SQLStatement
            NbAffectedRows = cmd.ExecuteNonQuery()
            DoneBool = True
        Catch e As SqlClient.SqlException
            If DidOnce Then
                If ShowErrMSG = True Then
                    If _NbTransactions = 0 Then
                        Throw New Exception("SqlClient.SqlException:DBLinker-" & SQLStatement, e)
                    Else
                    End If
                End If
                Exit Try
            End If

            Return DelDB(TableName, WhereFieldName, WhereFieldCondition, Quoted, FieldToDel, ShowErrMSG, ExtraWhereSource, WaitForSynch, WhereOperator, True)
        Catch e As Exception
        Finally
            If SelfOpenDB = True Then DBLinker.GetInstance().DBConnected = False
            'If WaitForSynch Then WaitingParallelThread.Join()
            LockingMutex.ReleaseMutex()
        End Try

        Return DoneBool
    End Function

    Public Function updateDB(ByVal tableName As String, ByVal setString As String, Optional ByVal whereFieldName As String = "", Optional ByVal whereFieldCondition As String = "", Optional ByVal quoted As Boolean = True, Optional ByVal whereOperator As String = "=", Optional ByVal showErrorMessage As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True) As Boolean
        Dim quotation As String = "'"
        Dim sqlStatement As String = ""
        If Quoted = False Then Quotation = ""
        Dim whereCmd As String = ""
        If WhereFieldName <> "" And WhereFieldCondition <> "" Then WhereCmd = " WHERE (((" & TableName & "." & WhereFieldName & ") " & WhereOperator & " " & Quotation & WhereFieldCondition & Quotation & "));" : WhereCmd = WhereCmd.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        SQLStatement = "UPDATE " & TableName & " SET " & SetString.Replace(vbCrLf, "\n") & WhereCmd

        If IsBatching Then
            CType(_Batching.Peek, ArrayList).Add(SQLStatement)
            Return True
        End If

        Dim done As Boolean = False
        Dim curCon As SqlClient.SqlConnection = DBLinker.con
        Dim closeCon As Boolean = False
        If UseMainConnection = False Then
            CurCon = MakeNewConnection()
            CurCon.Open()
            CloseCon = True
        Else
            LockingMutex.WaitOne()
        End If

        Dim selfOpenDB As Boolean = False
        If UseMainConnection AndAlso DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpenDB = True

        Dim cmd As IDbCommand = con.CreateCommand()
        LastSqlCommand = cmd
        cmd.CommandText = SQLStatement

        Try
            cmd.ExecuteNonQuery()
            done = True
        Catch e As SqlClient.SqlException
            If DidOnce Then
                If ShowErrorMessage = True Then MsgBox("Le logiciel n'a pas été capable de modifier les données à la base de données." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & e.Message)
                If _NbTransactions = 0 Then
                    Throw New Exception("SqlClient.SqlException:DBLinker-" & SQLStatement, e)
                Else
                    Exit Try
                End If
            End If

            UpdateDB(TableName, SetString, WhereFieldName, WhereFieldCondition, Quoted, WhereOperator, ShowErrorMessage, True)
        Catch e As Exception
            If ShowErrorMessage = True Then MsgBox("Le logiciel n'a pas été capable de modifier les données à la base de données." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & e.Message)
        Finally
            If UseMainConnection AndAlso SelfOpenDB = True Then DBLinker.GetInstance().DBConnected = False
            If CloseCon Then
                CurCon.Close()
                CurCon.Dispose()
                '                        Else
                LockingMutex.ReleaseMutex()
            End If
        End Try

        Return done
    End Function

    Private Sub beingDisconnected()
        _DBConnected = False

    End Sub

    Private Sub connectionStateChanged(ByVal sender As Object, ByVal e As Data.StateChangeEventArgs)
     
    End Sub

    Private Sub conInfoMessage(ByVal sender As Object, ByVal e As Data.SqlClient.SqlInfoMessageEventArgs)
        For Each err As System.Data.SqlClient.SqlError In e.Errors
            If err.State = 1 Then
                'not an error, an info message
                RaiseEvent Message(e.Source, e.Message)
            Else
                'State <> 1 means it really is an error
            End If
        Next
    End Sub

    Private Function connect(ByVal sqlString As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        Dim setGeneralCon As Boolean = False
        If con Is Nothing Then setGeneralCon = True

        LastSqlString = SqlString
        Dim passed As Boolean = False
        Try
            con = New SqlClient.SqlConnection(SqlString)
            If setGeneralCon Then
                DBLinker.con = con
                Passed = TestConnection(ShowErrMsg)
                AddHandler con.StateChange, AddressOf ConnectionStateChanged
            Else
                Passed = True
            End If

            REM Used to receive manual raiseerror.. so possibly Messages from server while running a StoredProcedure
            'con.FireInfoMessageEventOnUserErrors = True
            'AddHandler con.InfoMessage, AddressOf Me.conInfoMessage
        Catch ex As Exception
        Finally
        End Try

        Return Passed
    End Function

    Private Function makeNewConnection() As SqlClient.SqlConnection
        Dim newCon As New SqlClient.SqlConnection
        Connect(LastSqlString, , newCon)

        Return newCon
    End Function

    Public Function initConnection(ByVal server As String, ByVal port As Integer, ByVal dbName As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        If con Is Nothing Then con = DBLinker.con
        Return Connect("Data Source=" & Server & "," & Port & ";Network Library=DBMSSOCN;Initial Catalog=" & dbName & ";Integrated Security=True;Connect Timeout=30;", ShowErrMsg, con)
    End Function

    Public Function initConnection(ByVal server As String, ByVal dbName As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        If con Is Nothing Then con = DBLinker.con
        Return Connect("Data Source=" & Server & ";Initial Catalog=" & dbName & ";Integrated Security=True;Connect Timeout=30;", ShowErrMsg, con)
    End Function

    Private Function testConnection(Optional ByVal showErrMsg As Boolean = True) As Boolean
        If DBLinker.con Is Nothing Then Return False
        If DBLinker.con.State <> ConnectionState.Closed Or DBLinker.con.State <> ConnectionState.Broken Then Return True

        Dim passed As Boolean = False
        Try
            DBLinker.con.Open()
            Passed = True
        Catch ex As Exception
        Finally
            DBLinker.con.Close()
        End Try

        Return Passed
    End Function

    Public Shared Sub executeSQLScriptFile(ByVal fileName As String)
        Dim script As String = IO.File.ReadAllText(FileName)
        DBLinker.ExecuteSQLScript(script)
    End Sub

    Public Shared Sub executeSQLScript(ByVal script As String, Optional ByVal useTransaction As Boolean = True)
        Dim selfOpened As Boolean = False
        If DBLinker.GetInstance.DBConnected = False Then SelfOpened = True : DBLinker.GetInstance.DBConnected = True

        Dim scriptCmd As New SqlClient.SqlCommand()

        Try
            If UseTransaction Then DBLinker.GetInstance.BeginTransaction()
            scriptCmd.Connection = con
            scriptCmd.CommandTimeout = 10000
            Dim sql() As String = System.Text.RegularExpressions.Regex.Split(script, "(\r){0,1}\nGO\n(\r){0,1}")
            For i As Integer = 0 To SQL.GetUpperBound(0) 'Loop through array, executing each statement separately
                scriptCmd.CommandText = SQL(i)
                scriptCmd.ExecuteNonQuery()
            Next
            If UseTransaction Then DBLinker.GetInstance.CommitTransaction()
        Catch ex As Exception
            Try
                If UseTransaction Then DBLinker.GetInstance.RollbackTransaction()
            Catch newEx As Exception
            End Try

            Throw ex
        Finally
            If SelfOpened Then DBLinker.GetInstance.DBConnected = False
            scriptCmd.Dispose()
        End Try
    End Sub

    Public Sub updateBinary(ByVal tableName As String, ByVal fieldName As String, ByVal binaryData() As Byte, ByVal wherefieldName As String, ByVal whereFieldCondition As String, Optional ByVal quoted As Boolean = True)
        Dim sqlCom As New SqlClient.sqlCommand("UPDATE " & TableName & " SET " & FieldName & "=@Photo WHERE " & WhereFieldName & "=" & WhereFieldCondition, DBLinker.con)

        LastSqlCommand = SqlCom

        SqlCom.Parameters.Add(New SqlClient.SqlParameter("@Photo", BinaryData))
        SqlCom.ExecuteNonQuery()

        SqlCom.Dispose()
    End Sub
End Class
