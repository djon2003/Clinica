'This object is Singleton
Public Class DBLinker
    Inherits ContextBoundObject

#Region "Definitions"
    Private Shared mySelf As DBLinker
    Private Shared con As SqlClient.SqlConnection

    Private waitingTriggerThread As Threading.Thread
    Private _dbConnected As Boolean = False
    Private _KeepAlive As Boolean = False
    Private lastSqlString As String = ""
    Private dbLockingObject As New Object
    Private lockingMutex As New System.Threading.Mutex(False, "Locking")
    Private _NbTransactions As Integer = 0
    Private lastSqlCommand As SqlClient.SqlCommand
    Private Shared lastWriteCon As SqlClient.SqlConnection
    Private spParameters As New Hashtable
    Private _Batching As New System.Collections.Stack
    Private readDBError As System.Exception
    Private _WaitingTriggerNo As Integer = 0
    Private myUniqueLock As Object = New Object
#End Region

    Private Sub New()

    End Sub

    Public Enum SortOrderType
        Ascending = 0
        Descending = 1
    End Enum

    Public Enum QueryTypes
        Add = 0
    End Enum

    Public Event message(ByVal source As String, ByVal message As String)

#Region "Propriétés publiques"
    Public ReadOnly Property dbName() As String
        Get
            If con Is Nothing Then Return ""

            Return con.Database
        End Get
    End Property


    Public Property dbConnected() As Boolean
        Get
            Return _dbConnected
        End Get
        Set(ByVal Value As Boolean)
            _dbConnected = Value
            If Value = True Then
                If DBLinker.con.State = ConnectionState.Open Then Exit Property

                Try
                    If DBLinker.con.State = ConnectionState.Closed Or DBLinker.con.State = ConnectionState.Broken Then DBLinker.con.Open()
                Catch ex As Exception
                    _dbConnected = False
                    DBLinker.con.Close()
                    DBLinker.con = Nothing
                    Throw New DBLinkerException("Impossible d'ouvrir la base de données. Veuillez vous assurer de la disponibilité de celle-ci.", ex)
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

    Public ReadOnly Property isBatching() As Boolean
        Get
            Return _Batching.Count <> 0
        End Get
    End Property

#End Region


    Private Class OneTimeReadError
        Inherits Exception

        Public Function getError() As Exception
            Dim myTempException As Exception = Me
            Return myTempException
        End Function
    End Class

#Region "Private functions & properties"
    Private Function internalReadDB(ByVal sqlStatement As String, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal keepNulls As Boolean = False) As String(,)
        Dim myDataSet As DataSet = internalReadDBForGrid(sqlStatement, , , , , , useMainConnection)
        Dim results(,) As String = Nothing

        If myDataSet Is Nothing OrElse myDataSet.Tables.Count = 0 Then Return results
        If myDataSet.Tables(0).Rows.Count = 0 Then Return results

        With myDataSet.Tables(0)
            ReDim results(.Columns.Count - 1, .Rows.Count - 1)

            For i As Integer = 0 To .Columns.Count - 1
                For j As Integer = 0 To .Rows.Count - 1
                    If keepNulls AndAlso .Rows(j)(i) Is DBNull.Value Then
                        results(i, j) = Nothing
                    Else
                        results(i, j) = .Rows(j)(i).ToString
                    End If
                Next j
            Next i
        End With

        Return results
    End Function

    'This method is not synchronized
    Private Function internalReadDBForGrid2(ByVal sqlStatement As String, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal didOnce As Boolean = False, Optional ByVal sqlType As System.Data.CommandType = CommandType.Text, Optional ByVal curCon As SqlClient.SqlConnection = Nothing, Optional ByVal params() As String = Nothing) As DataSet
        If da Is Nothing Then da = New SqlClient.SqlDataAdapter
        If ds Is Nothing Then ds = New DataSet()
        Dim cmd As New SqlClient.SqlCommand()

        lastSqlCommand = cmd

        cmd.CommandText = sqlStatement
        cmd.CommandType = sqlType
        cmd.Connection = curCon
        cmd.CommandTimeout = 10000

        If sqlType = CommandType.StoredProcedure AndAlso params IsNot Nothing AndAlso params.Length <> 0 Then
            If spParameters.Contains(sqlStatement) Then
                cmd.Parameters.AddRange(spParameters(sqlStatement))
            Else
                SqlClient.SqlCommandBuilder.DeriveParameters(cmd)
                Dim cmdParams() As SqlClient.SqlParameter
                ReDim cmdParams(cmd.Parameters.Count - 1)
                cmd.Parameters.CopyTo(cmdParams, 0)
                spParameters.Add(sqlStatement, cmdParams)
            End If
            For i As Integer = 0 To Math.Min(params.Length, cmd.Parameters.Count) - 1
                cmd.Parameters.Item(i + 1).Value = params(i)
            Next i
        End If

        Try
            da.SelectCommand = cmd
            da.Fill(ds, tableMappingName)

        Catch e As SqlClient.SqlException
            If didOnce Then Throw New DBLinkerSQLException("SQL:" & cmd.CommandText, e)

            Return internalReadDBForGrid2(sqlStatement, da, tableMappingName, ds, True, sqlType, curCon)
        Catch ex As Exception
            If didOnce Then Throw New DBLinkerException("SQL:" & cmd.CommandText, ex)

            Return internalReadDBForGrid2(sqlStatement, da, tableMappingName, ds, True, sqlType, curCon)
        Finally
            cmd.Parameters.Clear()
            cmd.Dispose()
        End Try

        Return ds
    End Function

    Private Function internalReadDBForGrid(ByVal sqlStatement As String, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal didOnce As Boolean = False, Optional ByVal sqlType As System.Data.CommandType = CommandType.Text, Optional ByVal useMainConnection As Boolean = True, Optional ByVal params() As String = Nothing) As DataSet
        Dim curCon As SqlClient.SqlConnection = DBLinker.con
        Dim closeCon As Boolean = False
        If curCon Is Nothing Then curCon = DBLinker.con

        Dim selfOpenDB As Boolean = False
        If curCon.Equals(DBLinker.con) AndAlso DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpenDB = True
        If curCon.State <> ConnectionState.Open Then curCon.Open() : closeCon = True

        If useMainConnection = False Then
            curCon = makeNewConnection()
            curCon.Open()
            closeCon = True
        Else
            If didOnce = False Then lockingMutex.WaitOne()
        End If

        Dim returning As DataSet = internalReadDBForGrid2(sqlStatement, da, tableMappingName, ds, didOnce, sqlType, curCon, params)

        If curCon.Equals(DBLinker.con) AndAlso selfOpenDB = True Then DBLinker.getInstance().dbConnected = False
        If closeCon Then
            curCon.Close()
            curCon.Dispose()
            '            AddErrorLog(New Exception("CurCon.GetHashCode=" & CurCon.GetHashCode() & vbCrLf & "DBLinker.con.GetHashCode=" & DBLinker.con.GetHashCode & vbCrLf & "DBLinker.con.State=" & DBLinker.con.State.ToString & vbCrLf & "UseMainConnection=" & UseMainConnection & vbCrLf & "SelfOpenDB=" & SelfOpenDB))
        End If
        If curCon.Equals(DBLinker.con) AndAlso didOnce = False Then lockingMutex.ReleaseMutex()

        Return returning
    End Function

    Private Property waitingNo() As Integer
        Get
            Return _WaitingTriggerNo
        End Get
        Set(ByVal value As Integer)
            _WaitingTriggerNo = value
        End Set
    End Property

    Private Function testConnection(Optional ByVal showErrMsg As Boolean = True) As Boolean
        If DBLinker.con Is Nothing Then Return False
        If DBLinker.con.State <> ConnectionState.Closed Or DBLinker.con.State <> ConnectionState.Broken Then Return True

        Dim passed As Boolean = False
        Try
            DBLinker.con.Open()
            passed = True
        Catch ex As Exception

        Finally
            DBLinker.con.Close()
        End Try

        Return passed
    End Function

    Private Function innerWriteDB(ByVal sqlStatement As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal waitForDB As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = -1) As Boolean
        If byPassBatching = False AndAlso isBatching Then
            CType(_Batching.Peek, ArrayList).Add(sqlStatement)
            Return True
        End If

        Dim curCon As SqlClient.SqlConnection = DBLinker.con
        Dim closeCon As Boolean = False
        If useMainConnection = False Then
            curCon = makeNewConnection()
            curCon.Open()
            closeCon = True
        Else
            lockingMutex.WaitOne()
        End If
        lastWriteCon = curCon

        Dim returns As Boolean = False
        Dim selfOpenDB As Boolean = False
        If useMainConnection AndAlso DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpenDB = True

        Dim cmd As IDbCommand = curCon.CreateCommand()
        cmd.CommandTimeout = 60 '1 minute, au lieu de 30 secondes
        lastSqlCommand = cmd
        cmd.CommandText = sqlStatement

        Try
            cmd.ExecuteNonQuery()

            If getScopeIdentity = 0 Then
                Dim noIdent As DataSet = Me.internalReadDBForGrid2("SELECT SCOPE_IDENTITY()", , , , , , curCon)
                If noIdent Is Nothing OrElse noIdent.Tables(0).Rows.Count = 0 Then
                    getScopeIdentity = 0
                Else
                    getScopeIdentity = noIdent.Tables(0).Rows(0)(0)
                End If
            End If

            returns = True
            'Catch e As SqlClient.SqlException
            '    If didOnce Then
            '        If _NbTransactions = 0 Then 'Priorité aux transactions
            '            Throw New DBLinkerSQLException("DBLinker-" & sqlStatement, e)
            '        Else
            '            addErrorLog(New DBLinkerSQLException("SqlClient.SqlException:DBLinker-" & sqlStatement, e))
            '            Exit Try
            '        End If
            '    Else
            '        addErrorLog(New DBLinkerSQLException("FirstTime-SqlClient.SqlException:DBLinker-" & sqlStatement, e))
            '    End If

            '    Return innerWriteDB(sqlStatement, showErrMSG, waitForDB, True, useMainConnection, byPassBatching, getScopeIdentity)
        Catch e As Exception
            Throw New DBLinkerSQLException("DBLinker-" & sqlStatement, e)
            returns = False
        Finally
            If useMainConnection AndAlso selfOpenDB = True Then DBLinker.getInstance().dbConnected = False
            If closeCon Then
                curCon.Close()
                curCon.Dispose()
                '            AddErrorLog(New Exception("CurCon.GetHashCode=" & CurCon.GetHashCode() & vbCrLf & "DBLinker.con.GetHashCode=" & DBLinker.con.GetHashCode & vbCrLf & "DBLinker.con.State=" & DBLinker.con.State.ToString & vbCrLf & "UseMainConnection=" & UseMainConnection & vbCrLf & "SelfOpenDB=" & SelfOpenDB))
            Else
                lockingMutex.ReleaseMutex()
            End If
        End Try

        Return returns
    End Function

    Private Sub beingDisconnected()
        _dbConnected = False

        'If keepAlive Then
        '    '    AddErrorLog(New Exception("BeingDisconnected():" & vbCrLf & "DBLinker.con.GetHashCode=" & DBLinker.con.GetHashCode & vbCrLf & "DBLinker.con.State=" & DBLinker.con.State.ToString))

        '    If MessageBox.Show("Vous venez d'être déconnecté du serveur SQL. Voulez-vous essayer de vous reconnectez ? (Sinon ferme le logiciel)", "Reconnexion", MessageBoxButtons.YesNo) = DialogResult.No Then
        '        Software.doEndProcess()
        '        End
        '    End If

        '    If connect(lastSqlString, False) = False Then
        '        beingDisconnected()
        '    Else
        '        dbConnected = True
        '    End If
        'End If
    End Sub

    Private Sub connectionStateChanged(ByVal sender As Object, ByVal e As Data.StateChangeEventArgs)
        'AddErrorLog(New Exception("ConnectionStateChanged(e.CurrentState=" & e.CurrentState.ToString & "):" & vbCrLf & "DBLinker.con.GetHashCode=" & DBLinker.con.GetHashCode & vbCrLf & "DBLinker.con.State=" & DBLinker.con.State.ToString))
        Select Case e.CurrentState
            Case ConnectionState.Broken
                If _dbConnected <> False Then beingDisconnected()
            Case ConnectionState.Closed
                If _dbConnected <> False Then beingDisconnected()
            Case Else
                _dbConnected = True
        End Select
    End Sub

    Private Sub conInfoMessage(ByVal sender As Object, ByVal e As Data.SqlClient.SqlInfoMessageEventArgs)
        For Each err As System.Data.SqlClient.SqlError In e.Errors
            If err.State = 1 Then
                'not an error, an info message
                RaiseEvent message(e.Source, e.Message)
            Else
                'State <> 1 means it really is an error
            End If
        Next
    End Sub

    Private Function connect(ByVal sqlString As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        Dim setGeneralCon As Boolean = False
        If con Is Nothing Then setGeneralCon = True

        lastSqlString = sqlString
        Dim passed As Boolean = False
        Try
            con = New SqlClient.SqlConnection(sqlString)
            If setGeneralCon Then
                DBLinker.con = con
                passed = testConnection(showErrMsg)
                AddHandler con.StateChange, AddressOf connectionStateChanged
            Else
                passed = True
            End If

            'REM Used to receive manual raiseerror.. so possibly Messages from server while running a StoredProcedure
            'con.FireInfoMessageEventOnUserErrors = True
            'AddHandler con.InfoMessage, AddressOf Me.conInfoMessage
        Catch ex As Exception
            'If showErrMsg Then MessageBox.Show(ex.Message)
        Finally
        End Try

        Return passed
    End Function

    Private Function makeNewConnection() As SqlClient.SqlConnection
        Dim newCon As New SqlClient.SqlConnection
        connect(lastSqlString, , newCon)

        Return newCon
    End Function

#End Region

    Public Sub cancelCommand()
        If lastSqlCommand IsNot Nothing Then
            Try
                lastSqlCommand.Cancel()
            Catch ex As Exception
                'Impossible d'annuler
            End Try
            dbConnected = False
            dbConnected = True
        End If
    End Sub

    Public Sub beginBatching()
        _Batching.Push(New ArrayList)
    End Sub

    Public Function endBatching() As Boolean
        If isBatching = False Then Return False

        Dim poped As Object = _Batching.Pop()
        Dim script As String = String.Join(vbCrLf, CType(poped, ArrayList).ToArray("".GetType))
        Try
            DBLinker.executeSQLScript(script)
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
        executeSQLScript("BEGIN TRANSACTION T" & _NbTransactions & vbCrLf & "SAVE TRANSACTION T" & _NbTransactions, False)
    End Sub

    Public Sub commitTransaction()
        If _NbTransactions = 0 Then Exit Sub

        executeSQLScript("COMMIT TRANSACTION T" & _NbTransactions, False)
        _NbTransactions -= 1
    End Sub

    Public Sub rollbackTransaction()
        If _NbTransactions = 0 Then Exit Sub

        executeSQLScript("ROLLBACK TRANSACTION T" & _NbTransactions, False)
        _NbTransactions -= 1
    End Sub

    Public Sub rollbackAllTransactions()
        If _NbTransactions = 0 Then Exit Sub

        executeSQLScript("ROLLBACK TRANSACTION", False)
        _NbTransactions = 0
    End Sub

    Public Function getNbTransactions(Optional ByVal fromServer As Boolean = False) As Integer
        If fromServer Then
            Dim curNb() As String = Me.readOneDBField("SELECT @@TRANCOUNT")
            If curNb IsNot Nothing AndAlso curNb.Length <> 0 Then Return curNb(0)

            Throw New DBLinkerException("Erreur de lecture du nombre de transactions depuis le serveur")
        End If

        Return _NbTransactions
    End Function

    Public Shared Function getInstance() As DBLinker
        If mySelf Is Nothing Then mySelf = New DBLinker
        Return mySelf
    End Function

    Public Function readDB(ByVal sqlScript As String, Optional ByVal useMainConnection As Boolean = True) As String(,)
        Return internalReadDB(sqlScript, , useMainConnection)
    End Function

    Public Function readDB(ByVal tableName As String, ByVal fieldsToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal showErrMsg As Boolean = True, Optional ByVal useMainConnection As Boolean = True, Optional ByVal keepNulls As Boolean = False) As String(,)
        'LockingMutex.WaitOne()
        Dim Results(,), sqlStatement As String

        If whereSource <> "" And whereSource.StartsWith(" ") = False Then whereSource = " " & whereSource
        sqlStatement = "SELECT " & fieldsToSelect & " FROM " & tableName & whereSource
        sqlStatement = sqlStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        Try
            Results = internalReadDB(sqlStatement, , useMainConnection, keepNulls)
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            'If showErrMsg Then MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & e.Message)
            Throw New Exception("DBLinker-" & sqlStatement, e)
            readDBError = e
            Results = Nothing
        Finally
            '   LockingMutex.ReleaseMutex()
        End Try

        Return Results
    End Function

    Public Function readDBForGrid(ByVal tableName As String, ByVal fieldsToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal useMainConnection As Boolean = True) As DataSet
        'LockingMutex.WaitOne()
        Dim sqlStatement As String

        If whereSource <> "" AndAlso whereSource.Trim().StartsWith("WHERE") = False Then whereSource = "WHERE " & whereSource
        If whereSource <> "" AndAlso whereSource.StartsWith(" ") = False Then whereSource = " " & whereSource
        sqlStatement = "SELECT " & fieldsToSelect & " FROM " & tableName & whereSource
        sqlStatement = sqlStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        Try
            ds = internalReadDBForGrid(sqlStatement, da, tableMappingName, ds, , , useMainConnection)
        Catch ex As Threading.ThreadAbortException
            'Aborted by user
        Catch ex As Exception
            'MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & ex.Message)
            Throw New Exception("DBLinker-" & sqlStatement, ex)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return ds
    End Function

    Public Function readDBForGrid(ByVal sqlStatement As String, Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal useMainConnection As Boolean = True) As DataSet
        'LockingMutex.WaitOne()
        Try
            ds = internalReadDBForGrid(sqlStatement, da, tableMappingName, ds, , , useMainConnection)
        Catch ex As Threading.ThreadAbortException
            'Aborted by user
        Catch ex As Exception
            'MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & ex.Message)
            Throw New Exception("DBLinker-" & sqlStatement, ex)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return ds
    End Function

    Public Function readOneDBField(ByVal tableName As String, ByVal fieldToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal sortedTable As Boolean = False, Optional ByVal invertSort As Boolean = False, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True) As String()
        'LockingMutex.WaitOne()
        Dim i As Integer
        Dim results() As String = {}, TempResults(,), SQLStatement As String

        If whereSource <> "" And whereSource.StartsWith(" ") = False Then whereSource = " " & whereSource
        SQLStatement = "SELECT " & fieldToSelect
        If tableName <> "" Then SQLStatement &= " FROM " & tableName & whereSource

        SQLStatement = SQLStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        Try
            TempResults = internalReadDB(SQLStatement, , useMainConnection)
            If Not TempResults Is Nothing AndAlso TempResults.Length <> 0 Then
                ReDim results(TempResults.GetUpperBound(1))
                For i = 0 To TempResults.GetUpperBound(1)
                    results(i) = TempResults(0, i)
                Next i
            End If
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            'If showErrMSG Then MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & SQLStatement & vbCrLf & vbCrLf & e.Message)
            Throw New Exception("DBLinker-" & SQLStatement, e)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        If Not results Is Nothing AndAlso results.Length <> 0 Then
            If sortedTable = True Then Array.Sort(results)
            If invertSort = True Then Array.Reverse(results)
        End If
        Return results
    End Function

    Public Function readOneDBField(ByVal sqlStatement As String, Optional ByVal sortedTable As Boolean = False, Optional ByVal invertSort As Boolean = False, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True) As String()
        'LockingMutex.WaitOne()
        Dim i As Integer
        Dim results() As String = {}, TempResults(,) As String = {}

        Try
            TempResults = internalReadDB(sqlStatement, , useMainConnection)
            If Not TempResults Is Nothing AndAlso TempResults.Length <> 0 Then
                ReDim results(TempResults.GetUpperBound(1))
                For i = 0 To TempResults.GetUpperBound(1)
                    results(i) = TempResults(0, i)
                Next i
            End If
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            'If showErrMSG Then MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & e.Message)
            Throw New Exception("DBLinker-" & sqlStatement, e)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        If Not results Is Nothing AndAlso results.Length <> 0 Then
            If sortedTable = True Then Array.Sort(results)
            If invertSort = True Then Array.Reverse(results)
        End If
        Return results
    End Function

    Public Function writeDB(ByVal sqlStatement As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal waitForDB As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = 0) As Boolean
        Return innerWriteDB(sqlStatement, showErrMSG, waitForDB, didOnce, useMainConnection, byPassBatching, getScopeIdentity)
    End Function

    Public Function writeDB(ByVal tableName As String, ByVal fieldsToWrite As String, ByVal dataToWrite As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal waitForDB As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = -1) As Boolean
        Dim sqlStatement As String = "INSERT INTO " & tableName & " (" & fieldsToWrite.Replace(vbCrLf, "\n") & ") VALUES(" & dataToWrite.Replace(vbCrLf, "\n") & ")"
        Return innerWriteDB(sqlStatement, showErrMSG, waitForDB, didOnce, useMainConnection, byPassBatching, getScopeIdentity)
    End Function

    Public Function alterDB(ByVal sqlCommand As String, Optional ByVal showErrMSG As Boolean = True) As Boolean
        'LockingMutex.WaitOne()

        Dim selfOpenDB As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpenDB = True

        Dim cmd As IDbCommand = con.CreateCommand()
        lastSqlCommand = cmd
        cmd.CommandText = sqlCommand

        Try
            cmd.ExecuteNonQuery()
        Catch e As Exception
            'If showErrMSG Then MsgBox("Le logiciel n'a pas été capable d'ajouter les données à la base de données." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & e.Message)
            Throw New Exception("DBLinker-" & sqlCommand, e)
        Finally
            If selfOpenDB = True Then DBLinker.getInstance().dbConnected = False
            'LockingMutex.ReleaseMutex()
        End Try
    End Function

    Public Function delDB(ByVal tableName As String, Optional ByVal whereFieldName As String = "", Optional ByVal whereFieldCondition As String = "", Optional ByVal quoted As Boolean = True, Optional ByVal fieldToDel As String = "", Optional ByVal showErrMSG As Boolean = True, Optional ByVal extraWhereSource As String = "", Optional ByVal waitForSynch As Boolean = True, Optional ByVal whereOperator As String = "=", Optional ByVal didOnce As Boolean = False, Optional ByRef nbAffectedRows As Integer = -1) As Boolean
        Dim quotation As String = "'"
        Dim whereCmd As String = ""
        Dim sqlStatement As String = ""
        If quoted = False Then quotation = ""
        Dim doneBool As Boolean = False

        If extraWhereSource <> "" Then extraWhereSource = " " & extraWhereSource

        If whereFieldName <> "" And whereFieldCondition <> "" Then
            whereCmd = " WHERE (((" & tableName & "." & whereFieldName & ") " & whereOperator & " " & quotation & whereFieldCondition & quotation & ")" & extraWhereSource & ");"
            whereCmd = whereCmd.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        End If

        sqlStatement = "DELETE " & fieldToDel & " FROM " & tableName & whereCmd

        If isBatching Then 'Batch delete and return True
            CType(_Batching.Peek, ArrayList).Add(sqlStatement)
            Return True
        End If

        lockingMutex.WaitOne()

        Dim selfOpenDB As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpenDB = True

        Dim cmd As IDbCommand = con.CreateCommand()
        lastSqlCommand = cmd


        Return doneBool
    End Function

    Public Function updateDB(ByVal tableName As String, ByVal setString As String, Optional ByVal whereFieldName As String = "", Optional ByVal whereFieldCondition As String = "", Optional ByVal quoted As Boolean = True, Optional ByVal whereOperator As String = "=", Optional ByVal showErrorMessage As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByRef nbAffectedRows As Integer = -1) As Boolean
        Dim quotation As String = "'"
        Dim sqlStatement As String = ""
        If quoted = False Then quotation = ""
        Dim whereCmd As String = ""
        If whereFieldName <> "" And whereFieldCondition <> "" Then whereCmd = " WHERE (((" & tableName & "." & whereFieldName & ") " & whereOperator & " " & quotation & whereFieldCondition & quotation & "));" : whereCmd = whereCmd.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        sqlStatement = "UPDATE " & tableName & " SET " & setString.Replace(vbCrLf, "\n") & whereCmd

        If isBatching Then
            CType(_Batching.Peek, ArrayList).Add(sqlStatement)
            Return True
        End If

        Dim done As Boolean = False
        Dim curCon As SqlClient.SqlConnection = DBLinker.con
        Dim closeCon As Boolean = False
        If useMainConnection = False Then
            curCon = makeNewConnection()
            curCon.Open()
            closeCon = True
        Else
            lockingMutex.WaitOne()
        End If

        Dim selfOpenDB As Boolean = False
        If useMainConnection AndAlso DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpenDB = True

        Dim cmd As IDbCommand = con.CreateCommand()
        lastSqlCommand = cmd
        cmd.CommandText = sqlStatement

        Try
            nbAffectedRows = cmd.ExecuteNonQuery()
            done = True
            'Catch e As SqlClient.SqlException
            '    If didOnce Then
            '        If showErrorMessage = True Then MsgBox("Le logiciel n'a pas été capable de modifier les données à la base de données." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & e.Message)
            '        If _NbTransactions = 0 Then
            '            Throw New DBLinkerSQLException("SqlClient.SqlException:DBLinker-" & sqlStatement, e)
            '        Else
            '            addErrorLog(New DBLinkerSQLException("SqlClient.SqlException:DBLinker-" & sqlStatement, e))
            '            Exit Try
            '        End If
            '    End If

            '    updateDB(tableName, setString, whereFieldName, whereFieldCondition, quoted, whereOperator, showErrorMessage, True)
        Catch e As Exception
            'If showErrorMessage = True Then MsgBox("Le logiciel n'a pas été capable de modifier les données à la base de données." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & e.Message)
            Throw New DBLinkerException("DBLinker-" & sqlStatement, e)
        Finally
            If useMainConnection AndAlso selfOpenDB = True Then DBLinker.getInstance().dbConnected = False
            If closeCon Then
                curCon.Close()
                curCon.Dispose()
                '            AddErrorLog(New Exception("CurCon.GetHashCode=" & CurCon.GetHashCode() & vbCrLf & "DBLinker.con.GetHashCode=" & DBLinker.con.GetHashCode & vbCrLf & "DBLinker.con.State=" & DBLinker.con.State.ToString & vbCrLf & "UseMainConnection=" & UseMainConnection & vbCrLf & "SelfOpenDB=" & SelfOpenDB))
            Else
                lockingMutex.ReleaseMutex()
            End If
        End Try

        Return done
    End Function

    Public Function initConnection(ByVal server As String, ByVal port As Integer, ByVal dbName As String, ByVal username As String, ByVal password As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        If con Is Nothing Then con = DBLinker.con

        Return connect("Data Source=" & server & "," & port & ";Network Library=DBMSSOCN;Initial Catalog=" & dbName & ";Integrated Security=True;Connect Timeout=30;Application Name=Clinica;" & If(username = "", "", "User ID=" & username & ";Password=" & password & ";"), showErrMsg, con)
    End Function

    Public Function initConnection(ByVal server As String, ByVal dbName As String, ByVal username As String, ByVal password As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        If con Is Nothing Then con = DBLinker.con
        Return connect("Data Source=" & server & ";Initial Catalog=" & dbName & ";Integrated Security=True;Connect Timeout=30;Application Name=Clinica;" & If(username = "", "", "User ID=" & username & ";Password=" & password & ";"), showErrMsg, con)
    End Function

    Public Shared Sub executeSQLScriptFile(ByVal fileName As String)
        Dim script As String = IO.File.ReadAllText(fileName)
        DBLinker.executeSQLScript(script)
    End Sub

    Public Shared Sub executeSQLScript(ByVal script As String, Optional ByVal useTransaction As Boolean = True)
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance.dbConnected = False Then selfOpened = True : DBLinker.getInstance.dbConnected = True

        Dim scriptCmd As New SqlClient.SqlCommand()
        Dim lastCmd As String = ""

        Try
            If useTransaction Then DBLinker.getInstance.beginTransaction()
            scriptCmd.Connection = con
            scriptCmd.CommandTimeout = 10000
            Dim sql() As String = System.Text.RegularExpressions.Regex.Split(script, "(\r){0,1}\nGO\n(\r){0,1}")
            For i As Integer = 0 To sql.GetUpperBound(0) 'Loop through array, executing each statement separately
                lastCmd = sql(i)
                scriptCmd.CommandText = sql(i)
                scriptCmd.ExecuteNonQuery()
            Next
            If useTransaction Then DBLinker.getInstance.commitTransaction()
        Catch ex As Exception
            Try
                If useTransaction Then DBLinker.getInstance.rollbackTransaction()
            Catch newEx As Exception
                'addErrorLog(New DBLinkerException("SQL:" & lastCmd, ex))
            End Try

            Throw New DBLinkerException("SQL:" & lastCmd, ex)
        Finally
            If selfOpened Then DBLinker.getInstance.dbConnected = False
            scriptCmd.Dispose()
        End Try
    End Sub

    Public Sub updateBinary(ByVal tableName As String, ByVal fieldName As String, ByVal binaryData() As Byte, ByVal wherefieldName As String, ByVal whereFieldCondition As String, Optional ByVal quoted As Boolean = True)
        Dim sqlCom As New SqlClient.SqlCommand("UPDATE " & tableName & " SET " & fieldName & "=@Photo WHERE " & wherefieldName & "=" & whereFieldCondition, DBLinker.con)

        lastSqlCommand = sqlCom

        sqlCom.Parameters.Add(New SqlClient.SqlParameter("@Photo", binaryData))
        sqlCom.ExecuteNonQuery()

        sqlCom.Dispose()
    End Sub
End Class
