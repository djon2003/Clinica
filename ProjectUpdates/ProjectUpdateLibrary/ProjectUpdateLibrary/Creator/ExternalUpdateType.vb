Namespace Creator


    Public MustInherit Class ExternalUpdateType

        Private data As DataTable
        Private actionners As New Dictionary(Of String, ExternalUpdateActionner)
        Private filesList As New Dictionary(Of String, Text.StringBuilder)
        Private filesIntoList As New Dictionary(Of String, List(Of String))
        Private isUpdatesExist As New Dictionary(Of String, Boolean)
        Public webVersions As New Generic.Dictionary(Of String, Integer)
        Private softNames As New Generic.Dictionary(Of String, String)
        Private softNamesToUpdate As New List(Of String)
        Private Shared softAlreadyUpgraded As New List(Of String)

        Private newFilesToSkip As New List(Of String)

        Public Shared Event stopCreation(ByVal reason As String)

        Public Sub New(ByVal data As DataTable)
            Me.data = data

            createActionners()
        End Sub

        Private Sub createActionners()
            For Each curUpdateRow As DataRow In data.Rows
                Dim softKey As String = curUpdateRow("SoftKey")
                If actionners.ContainsKey(softKey) Then
                    actionners(softKey) = New ExternalUpdateActionner(curUpdateRow)
                Else
                    actionners.Add(softKey, New ExternalUpdateActionner(curUpdateRow))
                End If
            Next
        End Sub

        Public Function getSoftwareNamesToUpdate() As List(Of String)
            Return softNamesToUpdate
        End Function

#Region "Update creation"
        Private Function getWebVersions() As Generic.Dictionary(Of String, Integer)
            Using wc As New System.Net.WebClient()
                Dim n As Integer = 1
                For Each curUpdateRow As DataRow In data.Rows
                    Dim softKey As String = curUpdateRow("SoftKey")

                    Dim curUpdateVer As String = ""
                    Try
                        Dim infos As ExternalUpdateSoftwareInfos = actionners(softKey).getVersion(wc, True)

                        If webVersions.ContainsKey(curUpdateRow("SoftKey")) = False Then
                            webVersions.Add(curUpdateRow("SoftKey"), infos.version)
                            softNames.Add(curUpdateRow("SoftKey"), infos.softName)

                            If isUpdatesExist(softKey) Then softNamesToUpdate.Add(infos.softName)
                        End If
                    Catch ex As Net.WebException
                        If ex.Status = Net.WebExceptionStatus.ProtocolError AndAlso ex.Message.IndexOf("401") >= 0 Then
                            onStopCreation("UpdateUsername or UpdatePassword or SoftKey is/are not accepted." & vbCrLf & "Please ensure good values in table """ & data.TableName & """ for row #" & n & ".")
                        Else
                            Console.WriteLine(ex.ToString)
                            Throw ex
                        End If
                    End Try

                    n += 1
                Next
            End Using

            Return webVersions
        End Function

        Protected MustOverride Sub doPreCopyActions(ByVal curUpdateRow As DataRow, ByVal webVersions As Generic.Dictionary(Of String, Integer))

        Private Sub doPreCopyActions()
            For Each curUpdateRow As DataRow In data.Rows
                Dim softKey As String = curUpdateRow("SoftKey")
                If Not isUpdatesExist(softKey) Then Continue For

                doPreCopyActions(curUpdateRow, webVersions)
            Next
        End Sub


        Private Sub cleanOldUpdateVersions()
            For Each curUpdateRow As DataRow In data.Rows
                Dim softKey As String = curUpdateRow("SoftKey")
                If Not isUpdatesExist(softKey) Then Continue For

                Dim keepOnlyLastUpdate As Boolean = curUpdateRow("KeepOnlyLastUpdate")
                Dim updatesFolder As String = getFolderOrFilePath(curUpdateRow, "UpdatesFolder")
                If IO.Directory.GetDirectories(updatesFolder).Length <> 0 Then
                    For Each curDir As String In IO.Directory.GetDirectories(updatesFolder)
                        Dim myDir As New IO.DirectoryInfo(curDir)
                        Dim lastDirInPath As String = curDir.Substring(IO.Directory.GetParent(curDir).FullName.Length + 1)

                        If System.Text.RegularExpressions.Regex.IsMatch(myDir.Name, "[0-9]+") Then
                            If keepOnlyLastUpdate OrElse myDir.Name = webVersions(softKey) Then
                                My.Computer.FileSystem.DeleteDirectory(curDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
                            End If
                        End If
                    Next
                End If
            Next
        End Sub

        Private Sub createNewUpdateVersionFolders()
            For Each curUpdateRow As DataRow In data.Rows
                Dim softKey As String = curUpdateRow("SoftKey")
                If Not isUpdatesExist(softKey) Then Continue For

                Dim newVersionFolder As String = getFolderOrFilePath(curUpdateRow, "UpdatesFolder")
                Dim newVersion As String = webVersions(softKey)
                newVersionFolder &= IIf(newVersionFolder.EndsWith("\"), "", "\") & newVersion

                IO.Directory.CreateDirectory(newVersionFolder)

                Dim updateVersionFile As String = getFolderOrFilePath(curUpdateRow, "UpdateVersionFile")
                IO.Directory.CreateDirectory(updateVersionFile.Substring(0, updateVersionFile.LastIndexOf("\")))
                IO.File.WriteAllText(updateVersionFile, newVersion)

                Dim dataVersionFile As String = getFolderOrFilePath(curUpdateRow, "DataVersionFile")
                If dataVersionFile <> "" Then
                    IO.Directory.CreateDirectory(dataVersionFile.Substring(0, dataVersionFile.LastIndexOf("\")))
                    IO.File.WriteAllText(dataVersionFile, newVersion)
                End If
            Next
        End Sub

        Protected Function addFileToList(ByVal action As FileActions, ByVal filePath As String, ByVal newVersionFolder As String, ByVal baseFolderPathOfFile As String, ByVal fileSize As Integer, ByVal isLinkedToUpdateSoftwareRunning As Boolean, ByVal fileType As String) As Boolean
            Dim relFile As String = filePath.Substring(filePath.IndexOf(baseFolderPathOfFile) + baseFolderPathOfFile.Length + If(baseFolderPathOfFile.EndsWith("\"), 0, 1))

            If filesIntoList.ContainsKey(newVersionFolder) AndAlso filesIntoList(newVersionFolder).Contains(relFile) Then Return False

            Dim curList As List(Of String)
            Dim curStringBuilder As Text.StringBuilder
            Dim first As Boolean = False
            newVersionFolder &= If(newVersionFolder.EndsWith("\"), "", "\")
            If filesIntoList.ContainsKey(newVersionFolder) = False Then
                curList = New List(Of String)
                curStringBuilder = New Text.StringBuilder()
                filesIntoList.Add(newVersionFolder, curList)
                filesList.Add(newVersionFolder, curStringBuilder)
                first = True
            Else
                curList = filesIntoList(newVersionFolder)
                curStringBuilder = filesList(newVersionFolder)
            End If

            curList.Add(relFile)

            curStringBuilder.Append(If(first, "", vbCrLf) & relFile.Replace("\", "/") & _
                                    "§" & relFile & _
                                    "§" & fileSize & _
                                    "§" & If(isLinkedToUpdateSoftwareRunning, "True", "False") & _
                                    "§" & fileType & _
                                    "§" & CInt(action))

            Return True
        End Function

        Private Sub copyNewFiles()
            For Each curUpdateRow As DataRow In data.Rows
                Dim softKey As String = curUpdateRow("softKey")
                If Not isUpdatesExist(softKey) Then Continue For

                Dim newDir As String = getFolderOrFilePath(curUpdateRow, "NewVersionFolder")
                Dim newVersionFolder As String = getFolderOrFilePath(curUpdateRow, "UpdatesFolder")
                newVersionFolder &= IIf(newVersionFolder.EndsWith("\"), "", "\") & webVersions(softKey)
                For Each curFile As String In IO.Directory.GetFiles(newDir, "*.*", IO.SearchOption.AllDirectories)
                    If newFilesToSkip.Contains(curFile) Then Continue For

                    Dim fileSize As New IO.FileInfo(curFile)

                    Dim newfilePath As String = newVersionFolder & curFile.Substring(newDir.Length)
                    IO.Directory.CreateDirectory(newfilePath.Substring(0, newfilePath.LastIndexOf("\")))
                    IO.File.Copy(curFile, newfilePath)

                    addFileToList(FileActions.Add, curFile, newVersionFolder, newDir, fileSize.Length, False, "Binary")
                Next

                'Remove new version folder if empty
                If IO.Directory.GetFiles(newVersionFolder).Length = 0 AndAlso IO.Directory.GetDirectories(newVersionFolder).Length = 0 Then IO.Directory.Delete(newVersionFolder)
            Next
        End Sub

        Private Sub writeFilesListFiles()
            For Each newVersionFolder As String In filesList.Keys
                Dim filesListFile As String = newVersionFolder & If(newVersionFolder.EndsWith("\"), "", "\") & "files.list"

                IO.File.WriteAllText(filesListFile, filesList(newVersionFolder).ToString)
            Next
        End Sub

        Public Sub create()
            'Get versions (+1 to get newUpdateVersion) for each software to update which ensure username/password are good
            webVersions = getWebVersions()

            'Remove all old versions or only new one (if a previous update had been started, but not uploaded)
            cleanOldUpdateVersions()

            'Create new version folders + update new version file in updates folder
            createNewUpdateVersionFolders()

            'Do any actions previous to copying new files
            doPreCopyActions()

            'Copy new files to new version folder
            copyNewFiles()

            'Write files.list file to new version folder
            writeFilesListFiles()
        End Sub

        Protected Sub addNewFilesToSkip(ByVal file As String)
            newFilesToSkip.Add(file)
        End Sub
#End Region

#Region "Update clean up + Turn update server on"
        Public Sub clean()
            For Each curUpdateRow As DataRow In data.Rows
                Dim softKey As String = curUpdateRow("softKey")
                actionners(softKey).setServerIsOff(False, Date.Today)

                'Recreate the new version folder
                Dim newDir As String = getFolderOrFilePath(curUpdateRow, "NewVersionFolder")
                My.Computer.FileSystem.DeleteDirectory(newDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
                IO.Directory.CreateDirectory(newDir)

                'Update data & client versions
                Dim clientVersionFile As String = getFolderOrFilePath(curUpdateRow, "ClientVersionFile")
                Dim dataVersionFile As String = getFolderOrFilePath(curUpdateRow, "DataVersionFile")
                If dataVersionFile <> "" Then IO.File.WriteAllText(dataVersionFile, webVersions(softKey))
                IO.File.WriteAllText(clientVersionFile, webVersions(softKey))

                'Call underlying type cleaning
                clean(curUpdateRow)
            Next
        End Sub

        Protected MustOverride Sub clean(ByVal curUpdateRow As DataRow)
#End Region

#Region "Update existance"
        Public Function isUpdateExists() As Boolean
            Dim haveToUpdate As Boolean = False
            'Do it for each row even if an update exists before end so that this function is called for all of them
            For Each curUpdateRow As DataRow In data.Rows
                Dim softKey As String = curUpdateRow("SoftKey")

                isUpdatesExist.Add(softKey, isUpdateExists(curUpdateRow))
                If isUpdatesExist(softKey) Then haveToUpdate = True
            Next

            Return haveToUpdate
        End Function

        Protected MustOverride Function isUpdateExists(ByVal curUpdateRow As DataRow) As Boolean
#End Region

#Region "Common usage"
        Protected Function getSoftwareName(ByVal softKey As String) As String
            If softNames.ContainsKey(softKey) = False Then Return ""

            Return softNames(softKey)
        End Function

        Protected Function getFolderOrFilePath(ByVal curUpdateRow As DataRow, ByVal columnName As String) As String
            If curUpdateRow.Table.Columns.Contains(columnName) = False Then Return ""

            Dim folderFile As String = curUpdateRow(columnName)
            If folderFile = "" Then Return ""


            'If folder/file is not located on a remote path
            If Not folderFile.StartsWith("\\") AndAlso Not folderFile.StartsWith("//") Then
                If folderFile.IndexOf(":") = -1 Then folderFile = My.Application.Info.DirectoryPath & If(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & folderFile
                folderFile = folderFile.Replace("\\", "\") 'This ensure to remove a (back)slash at the begin of the entry in the XML file
            End If
            folderFile = folderFile.Replace("/", "\")

            Return folderFile
        End Function
#End Region


#Region "Update upload"

        Private Shared softwaresToUpgrade As New Dictionary(Of String, DataRow)

        Public Sub upload()
            For Each curUpdateRow As DataRow In data.Rows
                'TODO : Shall call to see if server updates is busy by Software Clients. If so, what do we do ? (Because we have to consider that an update can be composed of more than one UpdateType, so one could pass, not the other.. both shouldn't pass or both should pass, but not only one passing the other not)

                'Ensure there is an update to upload
                Dim softKey As String = curUpdateRow("softKey")
                If isUpdatesExist(softKey) = False Then Continue For

                Dim newVersionFolder As String = getFolderOrFilePath(curUpdateRow, "UpdatesFolder")
                newVersionFolder &= IIf(newVersionFolder.EndsWith("\"), "", "\") & webVersions(softKey)
                If IO.Directory.Exists(newVersionFolder) = False Then Continue For

                'Turn off server updates to Software Clients (so they don't interfer)
                actionners(softKey).setServerIsOff(True, Date.Now.AddHours(1))

                Dim files() As String = IO.Directory.GetFiles(newVersionFolder, "*.*", IO.SearchOption.AllDirectories)
                Dim uploadTypeFolder As String = Me.uploadTypeFolder.Replace("\", "/")
                If uploadTypeFolder <> "" AndAlso uploadTypeFolder.EndsWith("/") = False Then uploadTypeFolder &= "/"
                Console.WriteLine("Uploading version #" & webVersions(softKey) & " of " & softNames(softKey))
                For i As Integer = 0 To files.Length - 1
                    Dim relativeFolder As String = files(i).Substring(newVersionFolder.Length + 1)
                    Console.Write("Uploading file : " & uploadTypeFolder & webVersions(softKey) & "/" & relativeFolder.Replace("\", "/") & "...")

                    If relativeFolder.LastIndexOf("\") = -1 Then
                        relativeFolder = ""
                    Else
                        relativeFolder = relativeFolder.Substring(0, relativeFolder.LastIndexOf("\"))
                    End If

                    actionners(softKey).uploadFile(files(i), relativeFolder, uploadTypeFolder)

                    Console.WriteLine("Done")
                Next i

                If softwaresToUpgrade.ContainsKey(softKey) = False Then
                    softwaresToUpgrade.Add(softKey, curUpdateRow)
                End If
            Next
        End Sub

        Public Sub upgradeVersion()
            'Upgrade the version number on the server for each software (Not included in first loop because of possibility to have multiple updates for the same software (as data & client)
            Console.WriteLine()
            For Each curKey As String In softwaresToUpgrade.Keys
                If softAlreadyUpgraded.Contains(curKey) Then Continue For

                softAlreadyUpgraded.Add(curKey)
                actionners(curKey).upgradeVersion()

                Console.WriteLine(softNames(curKey) & " updated to version #" & webVersions(curKey))
            Next
        End Sub

        Protected MustOverride ReadOnly Property uploadTypeFolder() As String
#End Region

        Protected Shared Sub onStopCreation(ByVal reason As String)
            RaiseEvent stopCreation(reason)
        End Sub


        Protected Shared Sub ensureRequiredColumnsExist(ByVal data As DataTable)
            Dim columnsToCheck() As String = New String() {"ClientVersionFile", "SoftKey", "NewVersionFolder", "UpdatesFolder", "UpdateVersionFile"}

            For Each curColumn As String In columnsToCheck
                If data.Columns.Contains(curColumn) = False Then onStopCreation("The column """ & curColumn & """ in the table """ & data.TableName & """ is required. Ensure is presence please.")
            Next
        End Sub

        Public Shared Function getUpdateClass(ByVal data As DataTable) As ExternalUpdateType
            Return ExternalUpdateClient.getUpdateClass(data)
        End Function

    End Class


End Namespace