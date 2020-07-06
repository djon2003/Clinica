Namespace Creator


    Friend Class ExternalUpdateData
        Inherits ExternalUpdateType

        Private Const TABLENAME As String = "updateData"

        Public Sub New(ByVal data As DataTable)
            MyBase.New(data)
        End Sub

        Protected Overloads Shared Sub ensureRequiredColumnsExist(ByVal data As DataTable)
            Dim columnsToCheck() As String = New String() {}

            For Each curColumn As String In columnsToCheck
                If data.Columns.Contains(curColumn) = False Then onStopCreation("The column """ & curColumn & """ in the table """ & data.TableName & """ is required. Ensure is presence please.")
            Next

            ExternalUpdateType.ensureRequiredColumnsExist(data)
        End Sub

        Public Overloads Shared Function getUpdateClass(ByVal data As DataTable) As ExternalUpdateType
            If data.TableName <> TABLENAME Then Return Nothing

            ensureRequiredColumnsExist(data)

            Return New ExternalUpdateData(data)
        End Function

        Protected Overloads Overrides Function isUpdateExists(ByVal curUpdateRow As System.Data.DataRow) As Boolean
            Dim newDir As String = getFolderOrFilePath(curUpdateRow, "NewVersionFolder")
            Dim sqlFile As String = getFolderOrFilePath(curUpdateRow, "SQLUpdateFile")

            Dim nbNewFiles As Integer = 0
            If IO.Directory.Exists(newDir) Then nbNewFiles = IO.Directory.GetFiles(newDir, "*.*", IO.SearchOption.AllDirectories).Length

            If nbNewFiles <= 1 Then
                Dim sqlUpdates As String = ""
                If sqlFile <> "" AndAlso IO.File.Exists(sqlFile) Then
                    sqlUpdates = IO.File.ReadAllText(sqlFile).Trim
                Else
                    If nbNewFiles = 1 Then sqlUpdates = "fake"
                End If

                If sqlUpdates = "" Then Return False
            End If

            Return True
        End Function

        Protected Overrides Sub doPreCopyActions(ByVal curUpdateRow As System.Data.DataRow, ByVal webVersions As System.Collections.Generic.Dictionary(Of String, Integer))
            Dim sqlFile As String = getFolderOrFilePath(curUpdateRow, "SQLUpdateFile")

            'Skip file if empty
            If sqlFile <> "" AndAlso IO.File.Exists(sqlFile) Then
                Dim sql As String = IO.File.ReadAllText(sqlFile).Trim
                If sql = "" Then addNewFilesToSkip(sqlFile)
            End If
        End Sub

        Protected Overloads Overrides Sub clean(ByVal curUpdateRow As System.Data.DataRow)
            Dim sqlFile As String = getFolderOrFilePath(curUpdateRow, "SQLUpdateFile")
            If sqlFile <> "" Then
                Dim sqlFileInfo As New IO.FileInfo(sqlFile)
                IO.Directory.CreateDirectory(sqlFileInfo.Directory.FullName)
                Try
                    Dim utf8 As New Text.UTF8Encoding(True)
                    Using f As IO.StreamWriter = New IO.StreamWriter(sqlFile, False, utf8)
                        f.Write("")
                        f.Flush()
                    End Using
                Catch
                    Console.WriteLine("Impossible to recreate SQL file : " & sqlFile)
                End Try
            End If
        End Sub

        Protected Overrides ReadOnly Property uploadTypeFolder() As String
            Get
                Return "data"
            End Get
        End Property
    End Class


End Namespace