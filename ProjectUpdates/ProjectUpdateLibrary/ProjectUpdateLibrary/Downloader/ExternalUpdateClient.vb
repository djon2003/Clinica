Namespace Downloader

    Friend Class ExternalUpdateClient
        Inherits ExternalUpdate


        Private _newVersion, _dataUpdateVersion As Integer
        Private _UpdateClientFromLocal As Boolean = False
        Private _UpdateLocalPath As String = ""

        Friend Sub New(ByVal actionner As ExternalUpdateActionner, ByVal curWebClient As Net.WebClient)
            MyBase.New(actionner, curWebClient)
        End Sub

#Region "Properties"
        Public Property updateClientFromLocal() As Boolean
            Get
                Return _UpdateClientFromLocal
            End Get
            Set(ByVal value As Boolean)
                _UpdateClientFromLocal = value
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

        Public Property updateLocalPath() As String
            Get
                Return _UpdateLocalPath
            End Get
            Set(ByVal value As String)
                _UpdateLocalPath = value

                setFiles()
            End Set
        End Property
#End Region

        Private localUpdateVersionFile As String = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "current.version"
        Private localUpdateUpdatingFile As String = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "updating.on"
        Private localUpdateCopyingFile As String = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "copying.on"


        Private Sub setFiles()
            localUpdateVersionFile = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "current.version"
            localUpdateUpdatingFile = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "updating.on"
            localUpdateCopyingFile = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "copying.on"
        End Sub

        Public Function checkIfUpdatingFromLocal() As Boolean
            Dim localFilesExists As Boolean = IO.File.Exists(localUpdateVersionFile) AndAlso IO.File.Exists(localUpdateUpdatingFile) = False
            Dim locationIsNotOwn As Boolean = Not Directory.isRemoteLocationLocal(Me.updateLocalPath, My.Application.Info.DirectoryPath)

            If Me.updateClientFromLocal AndAlso Me.updateLocalPath <> "" AndAlso localFilesExists AndAlso locationIsNotOwn Then
                Dim localUpdateVersionEqualsCurVersion As Boolean = False
                Dim localUpdateVersion As String = IO.File.ReadAllText(localUpdateVersionFile)
                localUpdateVersionEqualsCurVersion = localUpdateVersion <> "" AndAlso localUpdateVersion = dataUpdateVersion
                Return External.current.isSoftwareVersionEqualsServer() = False AndAlso Not localUpdateVersionEqualsCurVersion
            End If

            Return False
        End Function


        Public Overrides Sub update()
            update("")
        End Sub

        Private Overloads Sub update(ByVal clientTmpDir As String)
            Dim clientDir As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath)
            Dim strUpdaterList As String = ""
            If clientTmpDir = "" Then
                clientTmpDir = clientDir & "Temp\UpdatingClient\"
                My.Computer.FileSystem.MoveDirectory(clientTmpDir, clientDir, True)
            Else
                clientTmpDir &= addSlash(clientTmpDir)

                'TODO : Do not manage sub folders (Like Plugins when it will exists). Ensure that no extra folders are copied (Like Images and Temp could be skipped)
                Dim sbFiles As New System.Text.StringBuilder()
                For Each curFile As String In IO.Directory.GetFiles(clientTmpDir, "*.*", IO.SearchOption.AllDirectories)
                    If curFile.EndsWith("\copying.on") Then Continue For 'Skip flag file
                    Dim configFile As String = "\" & Process.GetCurrentProcess().ProcessName & ".config.xml"
                    If curFile.EndsWith(configFile) Then Continue For 'Skip config file
                    If curFile.EndsWith("\Updater.exe") Then 'Copy right now Updater
                        IO.File.Copy(curFile, curFile.Replace(clientTmpDir, clientDir), True)
                        Continue For
                    End If

                    Dim relFile As String = curFile.Replace(clientTmpDir, "")
                    IO.File.Copy(curFile, clientDir & relFile & ".new", True)
                    sbFiles.AppendLine(relFile & ".new|" & relFile)
                Next
                strUpdaterList = sbFiles.ToString
            End If

            If strUpdaterList <> "" Then IO.File.WriteAllText(clientDir & "updater.list", strUpdaterList)
            IO.File.WriteAllText(clientDir & "current.version", newVersion)
        End Sub

        Public Sub updateLocal()
            Dim code As String = Me.GetHashCode() & ":" & Date.Today.ToString("yyyy/MM/dd")
            IO.File.WriteAllText(localUpdateCopyingFile, code)

            update(Me.updateLocalPath & addSlash(Me.updateLocalPath))

            If IO.File.Exists(localUpdateCopyingFile) Then
                Dim content As String = IO.File.ReadAllText(localUpdateCopyingFile)
                If content = code Then IO.File.Delete(localUpdateCopyingFile)
            End If
        End Sub

        Protected Overrides ReadOnly Property curVersion() As String
            Get
                Dim appDir As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath)
                Dim retCurVersion As String = "0"
                If IO.File.Exists(appDir & "current.version") Then retCurVersion = IO.File.ReadAllText(appDir & "current.version")

                Return retCurVersion
            End Get
        End Property

        Protected Overrides Sub modifyFilesToDownloadList(ByRef filesToDownload As System.Collections.Generic.Dictionary(Of String, String))
            'Nothing to do
        End Sub

        Protected Overrides Sub doPreDownloadActions()
            'Nothing to do
        End Sub

        Protected Overrides ReadOnly Property tmpDir() As String
            Get
                Return My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "Temp\UpdatingClient\"
            End Get
        End Property

        Protected Overrides ReadOnly Property updateFolder() As String
            Get
                Return "client"
            End Get
        End Property
    End Class

End Namespace
