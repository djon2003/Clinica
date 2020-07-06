Namespace Downloader

    Friend Class ExternalUpdateData
        Inherits ExternalUpdate

        Private _newVersion, _dataUpdateVersion As Integer
        Private _dataTempDownloadFolder As String = "", _dataFolder As String = ""
        Private nbSQLFilesFound As Integer = 0

        Friend Sub New(ByVal actionner As ExternalUpdateActionner, ByVal curWebClient As Net.WebClient)
            MyBase.New(actionner, curWebClient)
        End Sub

#Region "Properties"
        Public Property dataTempDownloadFolder() As String
            Get
                Return _dataTempDownloadFolder
            End Get
            Set(ByVal value As String)
                _dataTempDownloadFolder = value
            End Set
        End Property

        Public Property dataFolder() As String
            Get
                Return _dataFolder
            End Get
            Set(ByVal value As String)
                _dataFolder = value
            End Set
        End Property

        Public Property dataUpdateVersion() As Integer
            Get
                Return _dataUpdateVersion
            End Get
            Set(ByVal value As Integer)
                _dataUpdateVersion = value
            End Set
        End Property
#End Region

        Public Overrides Sub update()
            Dim tmpDir As String = dataTempDownloadFolder & addSlash(dataTempDownloadFolder) & "files\"
            Dim tmpSQLDir As String = dataTempDownloadFolder & addSlash(dataTempDownloadFolder) & "SQL\"

            'Update SQL Clinica database
            Dim sqlUpdatesFiles() As String = IO.Directory.GetFiles(tmpSQLDir, "*.sql")
            Dim sqlUpdates As New Generic.List(Of String)
            'Get SQL updates server files
            For Each curSQLFile As String In sqlUpdatesFiles
                sqlUpdates.Add(IO.File.ReadAllText(curSQLFile, System.Text.Encoding.UTF8))
            Next
            Dim sqlUpdate As String = String.Join(vbCrLf & "GO" & vbCrLf, sqlUpdates.ToArray())
            If sqlUpdate <> "" Then
                Try
                    External.current.executeSQL(sqlUpdate)
                Catch ex As Exception
                    Throw New ExternalUpdateException(ExternalUpdateException.ErrorTypes.SQL_FILE_CONTAINS_ERROR, ex)
                End Try
            End If

            'Move all files to be updated
            If IO.Directory.Exists(tmpDir) Then
                My.Computer.FileSystem.MoveDirectory(tmpDir, dataFolder, True)
            End If

            'Delete sql update files
            If IO.Directory.Exists(tmpSQLDir) Then
                My.Computer.FileSystem.DeleteDirectory(tmpSQLDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If

            'From now, the modification of the file is done externally
            dataUpdateVersion = Me.newVersion
            External.current.changeDataUpdateVersion(newVersion)
        End Sub

        Protected Overrides ReadOnly Property curVersion() As String
            Get
                Return dataUpdateVersion
            End Get
        End Property

        Protected Overrides Sub modifyFilesToDownloadList(ByRef filesToDownload As System.Collections.Generic.Dictionary(Of String, String))
            'If SQL update, rename it
            If filesToDownload.ContainsKey("sqlupdates.sql") Then
                nbSQLFilesFound += 1
                Dim line() As String = filesToDownload("sqlupdates.sql").Split(New Char() {"§"})
                line(1) = "..\SQL\sqlupdates" & nbSQLFilesFound & ".sql"
                filesToDownload.Add(line(1), String.Join("§", line))
                filesToDownload.Remove("sqlupdates.sql")
            End If
        End Sub

        Protected Overrides Sub doPreDownloadActions()
            Dim tmpSQLDir As String = dataTempDownloadFolder & addSlash(dataTempDownloadFolder) & "SQL/"
            Dim n As Integer = 0
            If IO.Directory.Exists(tmpSQLDir) Then My.Computer.FileSystem.DeleteDirectory(tmpSQLDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
            IO.Directory.CreateDirectory(tmpSQLDir)
        End Sub

        Protected Overrides ReadOnly Property tmpDir() As String
            Get
                Return dataTempDownloadFolder & addSlash(dataTempDownloadFolder) & "files/"
            End Get
        End Property

        Protected Overrides ReadOnly Property updateFolder() As String
            Get
                Return "data"
            End Get
        End Property
    End Class

End Namespace
