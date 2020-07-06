'This object is Singleton
Public Class DBLinker
    Inherits ContextBoundObject

#Region "Definitions"
    Private Const TRANSACTION_DEADLOCKED As Integer = 1205

    Private setLang As String = "SET LANGUAGE us_english"

    Private Shared mySelf As DBLinker
    Private Shared con As SqlClient.SqlConnection

    Private waitingTriggerThread As Threading.Thread
    Private _dbConnected As Boolean = False
    Private _KeepAlive As Boolean = False
    Private lastSqlString As String = ""
    Private dbLockingObject As New Object
    Private Shared lockingMutex As New System.Threading.Mutex()
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
                    If DBLinker.con.State = ConnectionState.Closed Or DBLinker.con.State = ConnectionState.Broken Then
                        DBLinker.con.Open()
                        executeSQLScript(setLang, False, con)
                    End If
                Catch ex As Exception
                    _dbConnected = False
                    DBLinker.con.Close()
                    DBLinker.con = Nothing
                    MessageBox.Show("Impossible d'ouvrir la base de données. Veuillez vous assurer de la disponibilité de celle-ci.", "Base de données")
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

    Private Function internalReadDB(ByVal sqlStatement As String, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByVal keepNulls As Boolean = False, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As String(,)
        Dim myDataSet As DataSet = internalReadDBForGrid(sqlStatement, , , , , , useMainConnection, , sqlConnection)
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

    Public Function readScalar(ByVal tableName As String, ByVal fieldToSelect As String, ByVal whereSource As String, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Object
        If whereSource.Trim.StartsWith("WHERE") = False Then whereSource = "WHERE " & whereSource
        Dim sqlStatement As String = "SELECT " & fieldToSelect & " FROM " & tableName & " " & whereSource

        Return readScalar(sqlStatement, useMainConnection, sqlConnection)
    End Function

    Public Function readScalar(ByVal sqlStatement As String, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Object
        Dim dbCommand As New DBCommandScalar()
        Dim done As Boolean = processCommand(dbCommand, sqlStatement, useMainConnection, sqlConnection, True, False)

        Return dbCommand.return
    End Function

    Private Function internalReadDBForGrid(ByVal sqlStatement As String, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal didOnce As Boolean = False, Optional ByVal sqlType As System.Data.CommandType = CommandType.Text, Optional ByVal useMainConnection As Boolean = True, Optional ByVal params() As String = Nothing, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As DataSet
        Dim dbCommand As New DBCommandFill(da, ds, tableMappingName, sqlType, params, spParameters)
        Dim done As Boolean = processCommand(dbCommand, sqlStatement, useMainConnection, sqlConnection, True, False)

        Return ds
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
            If showErrMsg Then MessageBox.Show(ex.Message)
        Finally
            DBLinker.con.Close()
        End Try

        Return passed
    End Function

    Private Sub beingDisconnected()
        _dbConnected = False

        If keepAlive Then
            If External.current.DBLinker_askReconnect = External.DoReconnect.EndSoftware Then
                External.current.endSoftware()
            End If

            If connect(lastSqlString, False) = False Then
                beingDisconnected()
            Else
                dbConnected = True
            End If
        End If
    End Sub

    Private Sub connectionStateChanged(ByVal sender As Object, ByVal e As Data.StateChangeEventArgs)
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
            If showErrMsg Then MessageBox.Show(ex.Message)
        Finally
        End Try

        Return passed
    End Function

    Public Function makeNewConnection() As SqlClient.SqlConnection
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

    Public Function endBatching(Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Boolean
        If isBatching = False Then Return False

        Dim poped As Object = _Batching.Pop()
        Dim script As String = String.Join(vbCrLf, CType(poped, ArrayList).ToArray("".GetType))
        Try
            DBLinker.executeSQLScript(script, , sqlConnection)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Sub clearBatching()
        _Batching.Clear()
    End Sub

    Public Sub beginTransaction(Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing)
        executeSQLScript("BEGIN TRANSACTION T" & _NbTransactions & vbCrLf & "SAVE TRANSACTION T" & _NbTransactions, False, sqlConnection)
        _NbTransactions += 1
    End Sub

    Public Sub commitTransaction(Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing)
        If _NbTransactions = 0 Then Exit Sub

        executeSQLScript("COMMIT TRANSACTION T" & _NbTransactions, False, sqlConnection)
        _NbTransactions -= 1
    End Sub

    Public Sub rollbackTransaction(Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing)
        If _NbTransactions = 0 Then Exit Sub

        executeSQLScript("ROLLBACK TRANSACTION T" & _NbTransactions, False, sqlConnection)
        _NbTransactions -= 1
    End Sub

    Public Sub rollbackAllTransactions(Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing)
        If _NbTransactions = 0 Then Exit Sub

        executeSQLScript("ROLLBACK TRANSACTION", False, sqlConnection)
        _NbTransactions = 0
    End Sub

    Public Function getNbTransactions(Optional ByVal fromServer As Boolean = False, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Integer
        If fromServer Then
            Dim curNb() As String = Me.readOneDBField("SELECT @@TRANCOUNT", , , , sqlConnection Is Nothing, sqlConnection)
            If curNb IsNot Nothing AndAlso curNb.Length <> 0 Then Return curNb(0)

            Throw New DBLinkerException("Erreur de lecture du nombre de transactions depuis le serveur")
        End If

        Return _NbTransactions
    End Function

    Public Shared Function getInstance() As DBLinker
        If mySelf Is Nothing Then mySelf = New DBLinker
        Return mySelf
    End Function


    Public Function readDB(ByVal sqlScript As String, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As String(,)
        Return internalReadDB(sqlScript, , useMainConnection, , sqlConnection)
    End Function

    Public Function readDB(ByVal tableName As String, ByVal fieldsToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal showErrMsg As Boolean = True, Optional ByVal useMainConnection As Boolean = True, Optional ByVal keepNulls As Boolean = False, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As String(,)
        Dim Results(,) As String = Nothing, sqlStatement As String

        If whereSource <> "" And whereSource.StartsWith(" ") = False Then whereSource = " " & whereSource
        sqlStatement = "SELECT " & fieldsToSelect & " FROM " & tableName & whereSource
        sqlStatement = sqlStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        Try
            Results = internalReadDB(sqlStatement, , useMainConnection, keepNulls, sqlConnection)
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            If showErrMsg Then MessageBox.Show("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & e.Message)
            External.current.addErrorLog(New Exception("DBLinker-" & sqlStatement, e))
            readDBError = e
            Results = Nothing
        End Try

        Return Results
    End Function

    Public Function executeStoredProcedure(ByVal storedProc As String, ByVal params() As String, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As DataSet
        Try
            ds = internalReadDBForGrid(storedProc, da, tableMappingName, ds, , CommandType.StoredProcedure, useMainConnection, params, sqlConnection)
        Catch ex As Exception
            MessageBox.Show("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & storedProc & vbCrLf & vbCrLf & ex.Message)
            External.current.addErrorLog(New Exception("DBLinker-" & storedProc, ex))
        End Try

        Return ds
    End Function

    Public Function readDBForGrid(ByVal tableName As String, ByVal fieldsToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As DataSet
        'LockingMutex.WaitOne()
        Dim sqlStatement As String

        If whereSource <> "" AndAlso whereSource.Trim().StartsWith("WHERE") = False Then whereSource = "WHERE " & whereSource
        If whereSource <> "" AndAlso whereSource.StartsWith(" ") = False Then whereSource = " " & whereSource
        sqlStatement = "SELECT " & fieldsToSelect & " FROM " & tableName & whereSource
        sqlStatement = sqlStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        Try
            ds = internalReadDBForGrid(sqlStatement, da, tableMappingName, ds, , , useMainConnection, , sqlConnection)
        Catch ex As Threading.ThreadAbortException
            'Aborted by user
        Catch ex As Exception
            MessageBox.Show("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & ex.Message)
            External.current.addErrorLog(New Exception("DBLinker-" & sqlStatement, ex))
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        If ds IsNot Nothing AndAlso ds.Tables.Count = 0 Then
            '--> Probably server disconnected
            'Throwing an exception to try to find why sometimes it happens
            Throw New Exception("No tables ?? SQL:" & vbCrLf & sqlStatement)
        End If

        Return ds
    End Function

    Public Function readDBForGrid(ByVal sqlStatement As String, Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As DataSet
        'LockingMutex.WaitOne()
        Try
            ds = internalReadDBForGrid(sqlStatement, da, tableMappingName, ds, , , useMainConnection, , sqlConnection)
        Catch ex As Threading.ThreadAbortException
            'Aborted by user
        Catch ex As Exception
            MessageBox.Show("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & ex.Message)
            External.current.addErrorLog(New Exception("DBLinker-" & sqlStatement, ex))
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return ds
    End Function

    Public Function readOneDBField(ByVal tableName As String, ByVal fieldToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal sortedTable As Boolean = False, Optional ByVal invertSort As Boolean = False, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As String()
        Dim SQLStatement As String

        If whereSource.Trim <> String.Empty AndAlso whereSource.Trim.ToUpper.StartsWith("WHERE") = False Then whereSource = " WHERE " & whereSource
        If whereSource <> "" And whereSource.StartsWith(" ") = False Then whereSource = " " & whereSource
        SQLStatement = "SELECT " & fieldToSelect
        If tableName <> "" Then SQLStatement &= " FROM " & tableName & whereSource

        SQLStatement = SQLStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        Return readOneDBField(SQLStatement, sortedTable, invertSort, showErrMSG, useMainConnection, sqlConnection)
    End Function

    Public Function readOneDBField(ByVal sqlStatement As String, Optional ByVal sortedTable As Boolean = False, Optional ByVal invertSort As Boolean = False, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As String()
        Dim i As Integer
        Dim results() As String = {}, TempResults(,) As String = {}

        Try
            TempResults = internalReadDB(sqlStatement, , useMainConnection, , sqlConnection)
            If Not TempResults Is Nothing AndAlso TempResults.Length <> 0 Then
                ReDim results(TempResults.GetUpperBound(1))
                For i = 0 To TempResults.GetUpperBound(1)
                    results(i) = TempResults(0, i)
                Next i
            End If
        Catch e As Threading.ThreadAbortException
            'Aborted by user
        Catch e As Exception
            If showErrMSG Then MessageBox.Show("Le logiciel n'a pas été capable de sélectionner dans la base de données." & vbCrLf & vbCrLf & sqlStatement & vbCrLf & vbCrLf & e.Message)
            External.current.addErrorLog(New Exception("DBLinker-" & sqlStatement, e))
        End Try

        If Not results Is Nothing AndAlso results.Length <> 0 Then
            If sortedTable = True Then Array.Sort(results)
            If invertSort = True Then Array.Reverse(results)
        End If
        Return results
    End Function

    Public Function writeDB(ByVal sqlStatement As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = -1, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Boolean
        Dim dbCommand As New DBCommandNonQuery(getScopeIdentity <> -1)
        Dim done As Boolean = processCommand(dbCommand, sqlStatement, useMainConnection, sqlConnection, showErrMSG, False)
        getScopeIdentity = dbCommand.scopeIdentity

        Return done
    End Function

    Public Function writeDB(ByVal tableName As String, ByVal fieldsToWrite As String, ByVal dataToWrite As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal useMainConnection As Boolean = True, Optional ByVal byPassBatching As Boolean = False, Optional ByRef getScopeIdentity As Integer = -1, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Boolean
        Dim sqlStatement As String = "INSERT INTO " & tableName & " (" & fieldsToWrite.Replace(vbCrLf, "\n") & ") VALUES(" & dataToWrite.Replace(vbCrLf, "\n") & ")"
        Return writeDB(sqlStatement, showErrMSG, useMainConnection, byPassBatching, getScopeIdentity, sqlConnection)
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
            If showErrMSG Then MessageBox.Show("Le logiciel n'a pas été capable d'ajouter les données à la base de données." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & e.Message)
            External.current.addErrorLog(New Exception("DBLinker-" & sqlCommand, e))
        Finally
            If selfOpenDB = True Then DBLinker.getInstance().dbConnected = False
            'LockingMutex.ReleaseMutex()
        End Try
    End Function

    Public Function delDB(ByVal tableName As String, Optional ByVal whereFieldName As String = "", Optional ByVal whereFieldCondition As String = "", Optional ByVal quoted As Boolean = True, Optional ByVal fieldToDel As String = "", Optional ByVal showErrMSG As Boolean = True, Optional ByVal extraWhereSource As String = "", Optional ByVal waitForSynch As Boolean = True, Optional ByVal whereOperator As String = "=", Optional ByVal didOnce As Boolean = False, Optional ByRef nbAffectedRows As Integer = -1, Optional ByVal useMainConnection As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Boolean
        Dim quotation As String = "'"
        Dim whereCmd As String = ""
        Dim sqlStatement As String = ""
        If quoted = False Then quotation = ""
        If extraWhereSource <> "" Then extraWhereSource = " " & extraWhereSource

        If (whereFieldName <> "" AndAlso whereFieldCondition = "") OrElse (whereFieldName = "" AndAlso whereFieldCondition <> "") Then Throw New Exception("Where part is either missing the fieldname or the condition")
        If whereFieldName <> "" And whereFieldCondition <> "" Then
            whereCmd = " WHERE (((" & tableName & "." & whereFieldName & ") " & whereOperator & " " & quotation & whereFieldCondition & quotation & ")" & extraWhereSource & ");"
            whereCmd = whereCmd.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        End If

        sqlStatement = "DELETE " & fieldToDel & " FROM " & tableName & whereCmd

        Dim dbCommand As New DBCommandNonQuery()
        Dim done As Boolean = processCommand(dbCommand, sqlStatement, useMainConnection, sqlConnection, showErrMSG, False)
        nbAffectedRows = dbCommand.nbAffectedRows

        Return done
    End Function

    Private Function processCommand(ByVal dbCommand As DBCommand, ByVal sqlStatement As String, ByVal useMainConnection As Boolean, ByVal sqlConnection As SqlClient.SqlConnection, ByVal showErrorMessage As Boolean, ByVal skipBatching As Boolean, Optional ByVal didOnce As Boolean = False) As Boolean
        If dbCommand.GetType().Equals(GetType(DBCommandNonQuery)) AndAlso Not skipBatching AndAlso isBatching Then
            CType(_Batching.Peek, ArrayList).Add(sqlStatement)
            Return True
        End If

        Dim retry As Boolean = False
        Dim done As Boolean = False
        Dim curCon As SqlClient.SqlConnection = DBLinker.con
        Dim closeCon As Boolean = False
        Dim selfOpenDB As Boolean = False
        If useMainConnection = False OrElse sqlConnection IsNot Nothing Then
            If sqlConnection Is Nothing Then
                curCon = makeNewConnection()
            Else
                curCon = sqlConnection
            End If

            If curCon.State = ConnectionState.Closed OrElse curCon.State = ConnectionState.Broken Then
                curCon.Open()
                closeCon = True
            End If
        Else
            If didOnce = False Then lockingMutex.WaitOne()
            If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpenDB = True
        End If

        Dim cmd As SqlClient.SqlCommand = con.CreateCommand()
        lastSqlCommand = cmd

        cmd.CommandText = sqlStatement
        cmd.Connection = curCon
        cmd.CommandTimeout = 10000

        Try
            dbCommand.execute(cmd)

            done = True
        Catch e As SqlClient.SqlException
            If didOnce Then
                If showErrorMessage = True Then MessageBox.Show("Le logiciel n'a pas été capable d'exécuter une commande sur la base de données SQL." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & "Erreur # : " & e.Number & vbCrLf & e.Message)

                If _NbTransactions = 0 Then
                    Throw New DBLinkerSQLException("SqlClient.SqlException:DBLinker-" & sqlStatement, e)
                Else
                    Exit Try
                End If
            Else
                retry = True
            End If

        Catch ex As Threading.ThreadAbortException
            'Nothing to do, except not sending the error
        Catch ex As Threading.AbandonedMutexException
            retry = True
        Catch e As Exception
            If showErrorMessage = True Then MessageBox.Show("Le logiciel n'a pas été capable d'exécuter une commande sur la base de données SQL." & vbCrLf & vbCrLf & cmd.CommandText & vbCrLf & vbCrLf & e.Message)
            Throw New DBLinkerException("DBLinker-" & sqlStatement, e)
        Finally
            If selfOpenDB = True Then DBLinker.getInstance().dbConnected = False
            If closeCon Then
                If curCon.State <> ConnectionState.Closed AndAlso curCon.State <> ConnectionState.Broken Then curCon.Close()
                If sqlConnection Is Nothing Then curCon.Dispose()
            End If
            If sqlConnection Is Nothing AndAlso curCon.Equals(DBLinker.con) AndAlso didOnce = False Then lockingMutex.ReleaseMutex()

            cmd.Parameters.Clear()
            cmd.Dispose()
        End Try

        If Not didOnce AndAlso retry Then
            Return processCommand(dbCommand, sqlStatement, useMainConnection, sqlConnection, showErrorMessage, skipBatching, True)
        End If

        Return done
    End Function

    Public Function updateDB(ByVal tableName As String, ByVal setString As String, Optional ByVal whereFieldName As String = "", Optional ByVal whereFieldCondition As String = "", Optional ByVal quoted As Boolean = True, Optional ByVal whereOperator As String = "=", Optional ByVal showErrorMessage As Boolean = True, Optional ByVal didOnce As Boolean = False, Optional ByVal useMainConnection As Boolean = True, Optional ByRef nbAffectedRows As Integer = -1, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing) As Boolean
        Dim quotation As String = "'"
        Dim sqlStatement As String = ""
        If quoted = False Then quotation = ""
        Dim whereCmd As String = ""
        If (whereFieldName <> "" AndAlso whereFieldCondition = "") OrElse (whereFieldName = "" AndAlso whereFieldCondition <> "") Then Throw New Exception("Where part is either missing the fieldname or the condition")
        If whereFieldName <> "" AndAlso whereFieldCondition <> "" Then whereCmd = " WHERE (((" & tableName & "." & whereFieldName & ") " & whereOperator & " " & quotation & whereFieldCondition & quotation & "));" : whereCmd = whereCmd.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        sqlStatement = "UPDATE " & tableName & " SET " & setString.Replace(vbCrLf, "\n") & whereCmd

        Dim dbCommand As New DBCommandNonQuery()
        Dim done As Boolean = processCommand(dbCommand, sqlStatement, useMainConnection, sqlConnection, showErrorMessage, False)
        nbAffectedRows = dbCommand.nbAffectedRows

        Return done
    End Function

    Public Function initConnection(ByVal server As String, ByVal port As Integer, ByVal dbName As String, ByVal username As String, ByVal password As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        If con Is Nothing Then con = DBLinker.con

        Return connect("Data Source=" & server & "," & port & ";Network Library=DBMSSOCN;Initial Catalog=" & dbName & ";Integrated Security=" & If(username = "", "True", "False") & ";Connect Timeout=30;Application Name=Clinica;" & If(username = "", "", "User ID=" & username & ";Password=" & password & ";"), showErrMsg, con)
    End Function

    Public Function initConnection(ByVal server As String, ByVal dbName As String, ByVal username As String, ByVal password As String, Optional ByVal showErrMsg As Boolean = True, Optional ByRef con As SqlClient.SqlConnection = Nothing) As Boolean
        If con Is Nothing Then con = DBLinker.con

        Return connect("Data Source=" & server & ";Initial Catalog=" & dbName & ";Integrated Security=" & If(username = "", "True", "False") & ";Connect Timeout=30;Application Name=Clinica;" & If(username = "", "", "User ID=" & username & ";Password=" & password & ";"), showErrMsg, con)
    End Function

    ''' <summary>
    ''' Read the file and use the executeScript method
    ''' </summary>
    ''' <param name="fileName">SQL filename to execute</param>
    ''' <remarks>See the executeScript remarks</remarks>
    Public Shared Sub executeSQLScriptFile(ByVal fileName As String, Optional ByVal useTransaction As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing)
        Dim script As String = IO.File.ReadAllText(fileName)
        DBLinker.executeSQLScript(script, useTransaction, sqlConnection)
    End Sub

    ''' <summary> 
    ''' Execute a SQL script on the main connection
    ''' </summary>
    ''' <param name="script">Script to execute</param>
    ''' <param name="useTransaction">Use a global transaction for the whole script</param>
    ''' <remarks>- Support RECONFIGURE commands<br/>- Remove USE commands<br/>- Split the script on GO command to be executed independantly</remarks>
    Public Shared Sub executeSQLScript(ByVal script As String, Optional ByVal useTransaction As Boolean = True, Optional ByVal sqlConnection As SqlClient.SqlConnection = Nothing, Optional ByVal didOnce As Boolean = False)
        'TODO : Transpose using processCommand (maybe a new DBCommand class)
        'TODO : Can reconfigure be rollbacked ? Because it's not secure like that for updates. Even though, "reconfigure" is really rare.

        Dim selfOpened As Boolean = False
        Dim closeCon As Boolean = False
        Dim usedMainConnection As Boolean = False
        If sqlConnection Is Nothing Then
            If didOnce = False Then lockingMutex.WaitOne()
            usedMainConnection = True
            sqlConnection = DBLinker.con
            If DBLinker.getInstance.dbConnected = False Then selfOpened = True : DBLinker.getInstance.dbConnected = True
        Else
            If sqlConnection.State = ConnectionState.Closed OrElse sqlConnection.State = ConnectionState.Broken Then
                sqlConnection.Open()
                closeCon = True
            End If
        End If

        Dim retry As Boolean = False
        Dim scriptCmd As New SqlClient.SqlCommand()
        Dim lastCmd As String = ""
        script = System.Text.RegularExpressions.Regex.Replace(script, "\r{0,1}\n", vbCrLf)
        Dim nextCutIndex As Integer = script.ToLower().IndexOf(vbCrLf & "reconfigure")
        Dim runReconfigure As Boolean = useTransaction AndAlso nextCutIndex <> -1
        Dim curScript As String = String.Empty
        If Not runReconfigure Then
            nextCutIndex = script.Length 'Let it pass once to execute script that doesn't contain reconfigure
            curScript = script.Substring(0, nextCutIndex).Trim()
            script = String.Empty
        Else
            curScript = script.Substring(0, nextCutIndex).Trim()
            nextCutIndex = script.IndexOf(vbCrLf, nextCutIndex + 1)
            If nextCutIndex = -1 Then nextCutIndex = script.Length
            script = script.Substring(nextCutIndex)
        End If

        Try
            scriptCmd.Connection = sqlConnection
            scriptCmd.CommandTimeout = 10000

            'Execute each script part between RECONFIGURE commands (if none, the whole is executed)
            While curScript <> String.Empty

                'Execute each script part between GO commands
                If useTransaction Then DBLinker.getInstance.beginTransaction(sqlConnection)
                Dim sql() As String = System.Text.RegularExpressions.Regex.Split(curScript, "^GO.?$", Text.RegularExpressions.RegexOptions.Multiline Or Text.RegularExpressions.RegexOptions.IgnoreCase)
                For i As Integer = 0 To sql.GetUpperBound(0) 'Loop through array, executing each statement separately
                    'Remove USE command
                    sql(i) = System.Text.RegularExpressions.Regex.Replace(sql(i), "^USE [^ ]+$", "", Text.RegularExpressions.RegexOptions.IgnoreCase Or Text.RegularExpressions.RegexOptions.Multiline)
                    If sql(i).Trim = "" Then Continue For

                    lastCmd = sql(i)
                    scriptCmd.CommandText = sql(i)
                    scriptCmd.ExecuteNonQuery()
                Next
                If useTransaction Then DBLinker.getInstance.commitTransaction(sqlConnection)
                If runReconfigure Then DBLinker.executeSQLScript("reconfigure;", False, sqlConnection)

                'Find next RECONFIGURE command + Set next loop variables
                nextCutIndex = script.ToLower().IndexOf(vbCrLf & "reconfigure")
                If nextCutIndex = -1 Then
                    nextCutIndex = script.Length 'Let it pass once to execute script that doesn't contain reconfigure
                    curScript = script.Substring(0, nextCutIndex).Trim()
                    script = String.Empty
                    runReconfigure = False
                Else
                    curScript = script.Substring(0, nextCutIndex).Trim()
                    nextCutIndex = script.IndexOf(vbCrLf, nextCutIndex + 1)
                    If nextCutIndex = -1 Then nextCutIndex = script.Length
                    script = script.Substring(nextCutIndex)
                End If
            End While
        Catch ex As SqlClient.SqlException
            If didOnce Then
                Throw New DBLinkerSQLException("SqlClient.SqlException:DBLinker-" & lastCmd, ex)
            Else
                retry = True
            End If
        Catch ex As Threading.ThreadAbortException
            'Nothing to do, except not sending the error
        Catch ex As Threading.AbandonedMutexException
            retry = True
        Catch ex As Exception
            Try
                If useTransaction Then DBLinker.getInstance.rollbackTransaction(sqlConnection)
            Catch newEx As Exception
                External.current.addErrorLog(New DBLinkerException("SQL:" & lastCmd, ex))
            End Try

            Throw New DBLinkerException("SQL:" & lastCmd, ex)
        Finally
            If selfOpened Then DBLinker.getInstance.dbConnected = False
            If closeCon AndAlso sqlConnection.State <> ConnectionState.Closed AndAlso sqlConnection.State <> ConnectionState.Broken Then sqlConnection.Close()
            If usedMainConnection Then lockingMutex.ReleaseMutex()

            scriptCmd.Dispose()
        End Try

        If retry And Not didOnce Then
            executeSQLScript(curScript & vbCrLf & script, useTransaction, sqlConnection, True)
        End If
    End Sub

    Public Sub updateBinary(ByVal tableName As String, ByVal fieldName As String, ByVal binaryData() As Byte, ByVal wherefieldName As String, ByVal whereFieldCondition As String, Optional ByVal quoted As Boolean = True)
        'TODO : Use processCommand instead + Add useMainConnection & sqlConnection params to the method

        Dim sqlCom As New SqlClient.SqlCommand("UPDATE " & tableName & " SET " & fieldName & "=@Photo WHERE " & wherefieldName & "=" & whereFieldCondition, DBLinker.con)

        lastSqlCommand = sqlCom

        sqlCom.Parameters.Add(New SqlClient.SqlParameter("@Photo", binaryData))
        sqlCom.ExecuteNonQuery()

        sqlCom.Dispose()
    End Sub
End Class
