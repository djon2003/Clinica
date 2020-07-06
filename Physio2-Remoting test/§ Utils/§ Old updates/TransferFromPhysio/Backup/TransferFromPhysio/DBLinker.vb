'This object is Singleton
Public Class DBLinker
    Inherits ContextBoundObject

    Private enterChar As String = vbCrLf

    Private waitingTriggerThread As Threading.Thread
    Private Shared mySelf As DBLinker
    Private WithEvents checkNoTrigger As System.IO.FileSystemWatcher
    Private Shared con As System.Data.Common.DbConnection
    Private Shared con2 As System.Data.Common.DbConnection
    Private _dbConnected As Boolean = False
    Private _KeepAlive As Boolean = False
    Private lastSqlString As String = ""
    Private dbLockingObject As New Object
    Private lockingMutex As New System.Threading.Mutex(False, "Locking")
    Private Shared isSQL As Boolean = False

    Private Sub new(ByVal isSQL As Boolean)
        DBLinker.IsSQL = IsSQL
        '
        'CheckNoTrigger
        '
        'Me.CheckNoTrigger = New System.IO.FileSystemWatcher
        'Me.CheckNoTrigger.EnableRaisingEvents = False
        'Me.CheckNoTrigger.Filter = "notrigger.lst"
        'Me.CheckNoTrigger.NotifyFilter = CType((System.IO.NotifyFilters.FileName Or System.IO.NotifyFilters.LastWrite Or IO.NotifyFilters.CreationTime), System.IO.NotifyFilters)
        'CheckNoTrigger.Path = AppPath & Bar(AppPath) & "Data\Lists"
    End Sub

    Public Enum SortOrderType
        Ascending = 0
        Descending = 1
    End Enum

    Public Structure StatType
        Dim errorStr As String
        Dim user As Integer
        Dim date_Renamed As Date
        Dim userFullName As String
        Dim actionStr As String
        Dim statDataSet As DataSet
    End Structure

#Region "Propriétés publiques"
    Public Property dbConnected() As Boolean
        Get
            Return _DBConnected
        End Get
        Set(ByVal Value As Boolean)
            _DBConnected = Value
            If Value = True Then
                Try
                    If getCurCon.State = ConnectionState.Closed Or getCurCon.State = ConnectionState.Broken Then getCurCon.Open()
                Catch
                    _DBConnected = False
                    MessageBox.Show("Impossible d'ouvrir la base de données. Veuillez vous assurer de la disponibilité de celle-ci.", "Base de données")
                End Try
            Else
                If getCurCon.State <> ConnectionState.Closed Then getCurCon.Close()
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



    Private readDBError As System.Exception

    Private Class OneTimeReadError
        Inherits Exception

        Public Function getError() As Exception
            Dim myTempException As Exception = Me
            Return MyTempException
        End Function
    End Class

#Region "Private functions & properties"
    Private Sub waitForEndDB()
        Console.WriteLine("Entered WaitForEndDB for " & WaitingNo & " by " & Threading.Thread.CurrentThread.ManagedThreadId)
        'WaitingTriggerThread = New Threading.Thread(AddressOf WaitingTriggerThreadSpin)
        WaitingTriggerThread.Start()

        WaitingTriggerThread.Join()
        WaitingTriggerThread = Nothing
        Console.WriteLine("Quitting WaitForEndDB by " & Threading.Thread.CurrentThread.ManagedThreadId)
    End Sub

    Private Function internalReadDB(ByVal sqlStatement As String, Optional ByVal didOnce As Boolean = False) As String(,)
        LockingMutex.WaitOne()

        Dim i, n As Integer
        n = 0
        Dim selfOpenDB As Boolean = False
        If Me.DBConnected = False Then Me.DBConnected = True : SelfOpenDB = True
        Dim cmd As IDbCommand = getCurCon.CreateCommand()

        cmd.CommandText = SQLStatement

        Dim results(,) As String = Nothing
        Dim reader As IDataReader
        Try
            reader = cmd.ExecuteReader()

            While reader.Read()
                ReDim Preserve Results(reader.FieldCount - 1, n)
                For i = 0 To reader.FieldCount - 1
                    Results(i, n) = ""
                    Try
                        Results(i, n) = reader(i)
                    Catch ex As Exception
                    End Try

                    If Results(i, n) Is Nothing Then Results(i, n) = ""
                Next i
                n += 1
            End While
        Catch e As SqlClient.SqlException
            If DidOnce Then Throw e

            Return InternalReadDB(SQLStatement, True)
        Catch e As Exception
            Throw e
        Finally
            If Not reader Is Nothing Then reader.Close()
            If SelfOpenDB = True Then Me.DBConnected = False
            LockingMutex.ReleaseMutex()
        End Try

        Return Results
    End Function

    Private Function internalReadDBForGrid(ByVal sqlStatement As String, Optional ByRef da As System.Data.Common.DbDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing, Optional ByVal didOnce As Boolean = False, Optional ByVal sqlType As System.Data.CommandType = CommandType.Text) As DataSet
        Dim selfOpenDB As Boolean = False
        If Me.DBConnected = False Then Me.DBConnected = True : SelfOpenDB = True
        If DA Is Nothing Then
            If IsSQL Then
                DA = New SqlClient.SqlDataAdapter()
            Else
                DA = New OleDb.OleDbDataAdapter
            End If
        End If
        If DS Is Nothing Then DS = New DataSet()
        Dim cmd As System.Data.Common.DbCommand

        If IsSQL Then
            cmd = New SqlClient.SqlCommand()
        Else
            cmd = New OleDb.OleDbCommand()
        End If

        cmd.CommandText = SQLStatement
        cmd.CommandType = SQLType
        cmd.Connection = getCurCon()

        Try
            DA.SelectCommand = cmd
            DA.Fill(DS, TableMappingName)
        Catch e As SqlClient.SqlException
            If DidOnce Then Throw e

            Return InternalReadDBForGrid(SQLStatement, DA, TableMappingName, DS, True)
        Catch ex As Exception
            Throw ex
        Finally
            If SelfOpenDB = True Then Me.DBConnected = False
        End Try

        Return DS
    End Function

    Private Sub removeDBWaitingNumber(ByVal noTrigger As Integer, Optional ByVal showErrMSG As Boolean = True)
        Dim selfOpenDB As Boolean = False
        If Me.DBConnected = False Then Me.DBConnected = True : SelfOpenDB = True
        Dim cmd As IDbCommand = getCurCon.CreateCommand()
        cmd.CommandText = "DELETE TriggerReturns WHERE NoTrigger=" & NoTrigger

        Try
            cmd.ExecuteNonQuery()
        Catch e As Exception
            If ShowErrMSG Then MsgBox("Le logiciel n'a pas été capable d'enlever l'utilisateur dans la table TriggerReturns." & vbCrLf & EnterChar & cmd.CommandText & EnterChar & EnterChar & e.Message)
            'AddItemToAList("$DB Bug.txt", cmd.CommandText, True)
        Finally
            If SelfOpenDB = True Then Me.DBConnected = False
        End Try
    End Sub

    Private Sub dbSync()
        WaitForEndDB()
    End Sub

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
#End Region

    Public Function addItemToADBList(ByVal tableName As String, ByVal fieldToWrite As String, ByVal dataToAdd As String, ByVal fieldOfAutoNum As String, Optional ByVal maximumItems As Integer = 0, Optional ByVal comparingMethod As Microsoft.VisualBasic.CompareMethod = CompareMethod.Text, Optional ByVal showErrMSG As Boolean = True, Optional ByVal quoted As Boolean = True, Optional ByVal extraWhereField As String = "", Optional ByVal extraWhereData As String = "") As Object
        If DataToAdd = "" Then Return "null"

        Dim i As Integer
        Dim quotationMark As String = ""
        If Quoted = True Then QuotationMark = "'"

        Dim myFieldToWrite As String = FieldToWrite
        If ExtraWhereField <> "" And ExtraWhereData <> "" Then MyFieldToWrite &= ", " & ExtraWhereField

        ReadDBError = Nothing
        Dim results As DataSet = ReadDBForGrid(TableName, FieldOfAutoNum & ", " & MyFieldToWrite, , False)

        If ReadDBError Is Nothing Then
            If Not Results Is Nothing Then
                For i = 0 To Results.Tables(0).Rows.Count - 1
                    If Results.Tables(0).Rows(i)(1).ToUpper = DataToAdd.ToUpper Then
                        If ExtraWhereField = "" And ExtraWhereData = "" Then
                            Return Results.Tables(0).Rows(i)(0)
                        ElseIf ExtraWhereField <> "" And ExtraWhereData <> "" Then
                            If ExtraWhereData = Results.Tables(0).Rows(i)(2) Then Return Results.Tables(0).Rows(i)(0)
                        End If
                    End If
                Next i
            End If
        Else
            REM Ne Fonctionne pas !!!! DBLinker.GetInstance.AlterDB("ALTER TABLE " & TableName & " ADD COLUMN " & FieldToWrite & " TEXT (250);", False)
            REM If Not ReadDBError Is Nothing Then MsgBox("NOT")
            Return "null"
        End If

        DataToAdd = DataToAdd.Replace("'", "''")
        DataToAdd = QuotationMark & DataToAdd & QuotationMark

        Dim myDataToAdd As String = DataToAdd
        Dim myExtraWhere As String = ""
        Dim extraQuoted As Boolean = Not Double.TryParse(ExtraWhereData, 0)

        If ExtraWhereField <> "" And ExtraWhereData <> "" Then
            If ExtraQuoted Then ExtraWhereData = QuotationMark & ExtraWhereData & QuotationMark
            MyDataToAdd &= "," & ExtraWhereData
            MyExtraWhere = " AND (" & ExtraWhereField & "=" & ExtraWhereData & ")"
        End If

        If WriteDB(TableName, MyFieldToWrite, MyDataToAdd, ShowErrMSG) Then
            Return ReadDBForGrid(TableName, FieldOfAutoNum, " WHERE (((" & TableName & "." & FieldToWrite & ") = " & DataToAdd & ") " & MyExtraWhere & ");").Tables(0).Rows(0)(0)
        Else
            Return "null"
        End If
    End Function

    Public Sub enableNoTriggerListening()
        'CheckNoTrigger.EnableRaisingEvents = True
    End Sub

    Public Shared Function getInstance(ByVal isSQL As Boolean) As DBLinker
        If MySelf Is Nothing Then MySelf = New DBLinker(IsSQL)
        DBLinker.IsSQL = IsSQL
        Return MySelf
    End Function

    Public Function readDBForGrid(ByVal tableName As String, ByVal fieldsToSelect As String, Optional ByVal whereSource As String = "", Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As System.Data.Common.DbDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing) As DataSet
        'LockingMutex.WaitOne()
        Console.WriteLine("ReadDBForGrid1 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Dim sqlStatement As String

        If WhereSource <> "" And WhereSource.StartsWith(" ") = False Then WhereSource = " " & WhereSource
        SQLStatement = "SELECT " & FieldsToSelect & " FROM " & TableName & WhereSource
        SQLStatement = SQLStatement.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")
        Console.WriteLine("ReadDBForGrid2 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Try
            DS = InternalReadDBForGrid(SQLStatement, DA, TableMappingName, DS)
        Catch ex As Exception
            MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & EnterChar & EnterChar & SQLStatement & EnterChar & EnterChar & ex.Message)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return DS
    End Function

    Public Function readDBForGrid(ByVal sqlStatement As String, Optional ByVal logErrMSG As Boolean = False, Optional ByRef da As SqlClient.SqlDataAdapter = Nothing, Optional ByVal tableMappingName As String = "Table", Optional ByVal ds As DataSet = Nothing) As DataSet
        'LockingMutex.WaitOne()
        Console.WriteLine("ReadDBForGrid1-1 InternalThreadSync:Waiting " & Threading.Thread.CurrentThread.GetHashCode)
        Try
            DS = InternalReadDBForGrid(SQLStatement, DA, TableMappingName, DS)
        Catch ex As Exception
            MsgBox("Le logiciel n'a pas été capable de sélectionner dans la base de données." & EnterChar & EnterChar & SQLStatement & EnterChar & EnterChar & ex.Message)
        Finally
            'LockingMutex.ReleaseMutex()
        End Try

        Return DS
    End Function

    Public Function writeDB(ByVal tableName As String, ByVal fieldsToWrite As String, ByVal dataToWrite As String, Optional ByVal showErrMSG As Boolean = True, Optional ByVal waitForDB As Boolean = True, Optional ByVal didOnce As Boolean = False) As Boolean
        'LockingMutex.WaitOne()

        Dim returns As Boolean = True
        Dim selfOpenDB As Boolean = False
        Dim sqlStatement As String = ""
        If Me.DBConnected = False Then Me.DBConnected = True : SelfOpenDB = True

        'Dim WaitingParallelThread As Threading.Thread
        If WaitForDB Then
            REM LOOK IF THIS CAN BE REMOVED
            'WaitingParallelThread = New Threading.Thread(AddressOf DBSync)
            'WaitingParallelThread.Start()
        End If

        Dim cmd As IDbCommand = getCurCon.CreateCommand()
        SQLStatement = "INSERT INTO " & TableName & " (" & FieldsToWrite & ") VALUES(" & DataToWrite & ")"
        cmd.CommandText = SQLStatement

        Try
            'If WaitForDB Then SharedCode.Client.Instance.WaitForEndDB(WaitingNo)
            cmd.ExecuteNonQuery()
        Catch e As SqlClient.SqlException
            If DidOnce Then Throw New Exception(DataToWrite, e)

            Return WriteDB(TableName, FieldsToWrite, DataToWrite, ShowErrMSG, WaitForDB, True)
        Catch e As Exception
            If ShowErrMSG Then MsgBox("Le logiciel n'a pas été capable d'ajouter les données à la base de données." & EnterChar & EnterChar & cmd.CommandText & EnterChar & EnterChar & e.Message)
            Returns = False
        Finally
            If SelfOpenDB = True Then Me.DBConnected = False
            If WaitForDB Then
                'WaitingParallelThread.Join()
            End If
            'LockingMutex.ReleaseMutex()
        End Try

        Return Returns
    End Function

    Private Sub beingDisconnected()
        _DBConnected = False

        If KeepAlive Then
            If MessageBox.Show("Vous venez d'être déconnecté du serveur SQL. Voulez-vous essayer de vous reconnectez ? (Sinon ferme le logiciel)", "Reconnexion", MessageBoxButtons.YesNo) = DialogResult.No Then End

            If Connect(LastSqlString, False) = False Then BeingDisconnected()
        End If
    End Sub

    Private Sub connectionStateChanged(ByVal sender As Object, ByVal e As Data.StateChangeEventArgs)
        Select Case e.CurrentState
            Case ConnectionState.Broken
                If _DBConnected <> False Then BeingDisconnected()
            Case ConnectionState.Closed
                If _DBConnected <> False Then BeingDisconnected()
            Case Else
                _DBConnected = True
        End Select
    End Sub

    Private Function connect(ByVal sqlString As String, Optional ByVal showErrMsg As Boolean = True) As Boolean
        LastSqlString = SqlString
        Dim passed As Boolean = False
        Try
            If IsSQL Then
                con = New SqlClient.SqlConnection(SqlString)
            Else
                con2 = New OleDb.OleDbConnection(SqlString)
            End If
            Passed = TestConnection(ShowErrMsg)
            AddHandler getCurCon.StateChange, AddressOf ConnectionStateChanged
            _DBConnected = True
        Catch ex As Exception
            If ShowErrMsg Then MessageBox.Show(ex.Message)
        Finally
        End Try

        Return Passed
    End Function

    Public Function initConnection(ByVal server As String, ByVal port As Integer, Optional ByVal showErrMsg As Boolean = True, Optional ByVal db As String = "Clinica") As Boolean
        Return Connect("Data Source=" & Server & "," & Port & ";Network Library=DBMSSOCN;Initial Catalog=" & DB & ";Integrated Security=True;Connect Timeout=30;")
    End Function

    Public Function initConnection(ByVal server As String, Optional ByVal showErrMsg As Boolean = True, Optional ByVal db As String = "Clinica") As Boolean
        Dim connectString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" & Server
        If IsSQL Then ConnectString = "Data Source=" & Server & ";Initial Catalog=" & DB & ";Integrated Security=True;Connect Timeout=30;"
        Return Connect(ConnectString)
    End Function

    Private Shared Function getCurCon() As System.Data.Common.DbConnection
        If IsSQL Then Return con

        Return con2
    End Function

    Private Function testConnection(Optional ByVal showErrMsg As Boolean = True) As Boolean
        Dim passed As Boolean = False
        Try
            getCurCon.Open()
            Passed = True
        Catch ex As Exception
            If ShowErrMsg Then MessageBox.Show(ex.Message)
        Finally
            getCurCon.Close()
        End Try

        Return Passed
    End Function


    Public Sub updateDB(ByVal tableName As String, ByVal setString As String, Optional ByVal whereFieldName As String = "", Optional ByVal whereFieldCondition As String = "", Optional ByVal quoted As Boolean = True, Optional ByVal whereOperator As String = "=", Optional ByVal showErrorMessage As Boolean = True, Optional ByVal didOnce As Boolean = False)
        'LockingMutex.WaitOne()

        Dim quotation As String = "'"
        Dim sqlStatement As String = ""
        If Quoted = False Then Quotation = ""
        Dim whereCmd As String = ""
        If WhereFieldName <> "" And WhereFieldCondition <> "" Then WhereCmd = " WHERE (((" & TableName & "." & WhereFieldName & ") " & WhereOperator & " " & Quotation & WhereFieldCondition & Quotation & "));" : WhereCmd = WhereCmd.Replace("= null", "IS null").Replace("<> null", "IS NOT Null")

        Dim selfOpenDB As Boolean = False
        If Me.DBConnected = False Then Me.DBConnected = True : SelfOpenDB = True

        Dim cmd As IDbCommand = getCurCon.CreateCommand()
        SQLStatement = "UPDATE " & TableName & " SET " & SetString & WhereCmd
        cmd.CommandText = SQLStatement

        Try
            'SharedCode.Client.Instance.WaitForEndDB(WaitingNo)

            cmd.ExecuteNonQuery()
        Catch e As SqlClient.SqlException
            If DidOnce Then
                If ShowErrorMessage = True Then MsgBox("Le logiciel n'a pas été capable de modifier les données à la base de données." & EnterChar & EnterChar & cmd.CommandText & EnterChar & EnterChar & e.Message)
                Throw e
            End If

            UpdateDB(TableName, SetString, WhereFieldName, WhereFieldCondition, Quoted, WhereOperator, ShowErrorMessage, True)
        Catch e As Exception
            If ShowErrorMessage = True Then MsgBox("Le logiciel n'a pas été capable de modifier les données à la base de données." & EnterChar & EnterChar & cmd.CommandText & EnterChar & EnterChar & e.Message)
        Finally
            If SelfOpenDB = True Then Me.DBConnected = False
            'LockingMutex.ReleaseMutex()
        End Try
    End Sub

    Public Shared Sub executeSQLScript(ByVal script As String)
        Dim selfOpened As Boolean = False
        If DBLinker.GetInstance(True).DBConnected = False Then SelfOpened = True : DBLinker.GetInstance(True).DBConnected = True

        Dim scriptCmd As New SqlClient.SqlCommand()
        scriptCmd.Connection = getCurCon()
        Dim sql() As String = System.Text.RegularExpressions.Regex.Split(script, "\nGO\n")
        For i As Integer = 0 To SQL.GetUpperBound(0) 'Loop through array, executing each statement separately
            scriptCmd.CommandText = SQL(i)
            scriptCmd.ExecuteNonQuery()
        Next

        If SelfOpened Then DBLinker.GetInstance(True).DBConnected = False
        scriptCmd.Dispose()
    End Sub
End Class
