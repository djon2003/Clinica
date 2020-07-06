Namespace Downloader

    Public NotInheritable Class ExternalUpdater

#Region "Definitions"
        Private updating As Boolean = False
        Private actionner As ExternalUpdateActionner

        Private _newVersion As Integer
        Private webVersion As Integer = -1
        Private wc As New Net.WebClient
        Private updaterList As New Generic.List(Of String)
        Private _UpdateClientFromLocal As Boolean = False
        Private _UpdateLocalPath As String = ""
        Private _appPath As String = ""
        Private hasMajorUpdateMadeRegressVersion As Boolean = False
        Private newVersionNotRegressed As Integer

        'Files used by updater
        Private clientUpdaterListFile As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "updater.list"
        Private clientCopyingFile As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "copying.on"
        Private clientUpdatingFile As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "updating.on"
        Private localUpdateVersionFile As String = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "current.version"
        Private localUpdateUpdatingFile As String = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "updating.on"
        Private localUpdateCopyingFile As String = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "copying.on"
        Private clientMajorVersionFile As String = clientUpdatingFile & ".version"
        Private clientAutoupdateFile As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "autoupdate.on"


        Private clientPart As ExternalUpdateClient
        Private dataPart As ExternalUpdateData
        Private partsToUpdate As New Generic.List(Of IExternalUpdate)
        Private basicPartsToUpdate As BasicParts

        Public Enum BasicParts As Integer
            CLIENT = 0
            CLIENT_AND_DATA = 1
        End Enum
#End Region

        Public Sub New(ByVal basicPartsToUpdate As BasicParts)
            actionner = New ExternalUpdateActionner()
            clientPart = New ExternalUpdateClient(actionner, wc)
            dataPart = New ExternalUpdateData(actionner, wc)

            Select Case basicPartsToUpdate
                Case BasicParts.CLIENT
                Case BasicParts.CLIENT_AND_DATA
                    partsToUpdate.Add(dataPart)
            End Select

            'Ensure data is updated before client
            partsToUpdate.Add(clientPart)

            Me.basicPartsToUpdate = basicPartsToUpdate
        End Sub

        Protected Overrides Sub finalize()
            MyBase.Finalize()

            Me.wc.Dispose()
        End Sub

#Region "Properties"
        Private Property newVersion() As Integer
            Get
                Return _newVersion
            End Get
            Set(ByVal value As Integer)
                _newVersion = value
                For Each curPart As IExternalUpdate In partsToUpdate
                    curPart.newVersion = value
                Next
            End Set
        End Property

        Public Property softKey() As String
            Get
                Return actionner.softKey
            End Get
            Set(ByVal value As String)
                actionner.softKey = value
            End Set
        End Property

        Public Property userType() As ExternalUpdateUserTypes
            Get
                Return actionner.userType
            End Get
            Set(ByVal value As ExternalUpdateUserTypes)
                actionner.userType = value
            End Set
        End Property

        Public Property username() As String
            Get
                Return actionner.username
            End Get
            Set(ByVal value As String)
                actionner.username = value
            End Set
        End Property

        Public Property password() As String
            Get
                Return actionner.password
            End Get
            Set(ByVal value As String)
                actionner.password = value
            End Set
        End Property

        Public Property channel() As String
            Get
                Return actionner.channel
            End Get
            Set(ByVal value As String)
                actionner.channel = value
            End Set
        End Property

        Public Property dataTempDownloadFolder() As String
            Get
                Return dataPart.dataTempDownloadFolder
            End Get
            Set(ByVal value As String)
                dataPart.dataTempDownloadFolder = value
            End Set
        End Property

        Public Property dataFolder() As String
            Get
                Return dataPart.dataFolder
            End Get
            Set(ByVal value As String)
                dataPart.dataFolder = value
            End Set
        End Property

        Public Property dataUpdateVersion() As Integer
            Get
                Return dataPart.dataUpdateVersion
            End Get
            Set(ByVal value As Integer)
                dataPart.dataUpdateVersion = value
            End Set
        End Property

        Public Property updateURL() As String
            Get
                Return actionner.updateURL
            End Get
            Set(ByVal value As String)
                actionner.updateURL = value
            End Set
        End Property

        Public Property appPath() As String
            Get
                Return _appPath
            End Get
            Set(ByVal value As String)
                _appPath = value
            End Set
        End Property

        Public Property updateClientFromLocal() As Boolean
            Get
                Return _UpdateClientFromLocal
            End Get
            Set(ByVal value As Boolean)
                _UpdateClientFromLocal = value
                clientPart.updateClientFromLocal = value
            End Set
        End Property

        Public Property updateLocalPath() As String
            Get
                Return _UpdateLocalPath
            End Get
            Set(ByVal value As String)
                _UpdateLocalPath = value
                clientPart.updateLocalPath = value

                setFiles()
            End Set
        End Property
#End Region

        Public Sub addExternalUpdate(ByVal newExternalUpdate As IExternalUpdate)
            partsToUpdate.Add(newExternalUpdate)
            newExternalUpdate.newVersion = Me.newVersion
        End Sub

        Private Sub setFiles()
            localUpdateVersionFile = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "current.version"
            localUpdateUpdatingFile = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "updating.on"
            localUpdateCopyingFile = Me.updateLocalPath & addSlash(Me.updateLocalPath) & "copying.on"
        End Sub

        Private Sub ensureUpdateServerAccessible()
            If isConnectionAvailable() = False Then Throw New ExternalUpdateException(ExternalUpdateException.ErrorTypes.NO_INTERNET_CONNECTION)

            Dim isServerOff As Boolean = actionner.getServerIsOff()
            If isServerOff Then
                Throw New ExternalUpdateException(ExternalUpdateException.ErrorTypes.UPDATE_SERVER_OFFLINE)
            End If
        End Sub

        Private Sub setNewVersion(ByVal newVersion As Integer)
            If newVersion = 0 Then newVersion = actionner.getVersion().version

            newVersionNotRegressed = newVersion

            hasMajorUpdateMadeRegressVersion = False
            Dim major As Integer = actionner.checkForMajorUpdate(External.current.clientUpdateVersion + 1, newVersion, wc)
            If major <> 0 AndAlso newVersion > major Then
                hasMajorUpdateMadeRegressVersion = True
                newVersion = major
            End If

            Me.newVersion = newVersion
        End Sub

        Private Sub doUpdate()
            IO.File.WriteAllText(clientUpdatingFile, Date.Now)
            If External.current.isSoftwareVersionEqualsServer() Then
                For Each curPart As IExternalUpdate In partsToUpdate
                    curPart.download()
                Next
                For Each curPart As IExternalUpdate In partsToUpdate
                    curPart.update()
                Next
            Else
                If Me.newVersion = 0 Then Me.newVersion = dataUpdateVersion
                clientPart.download()
                clientPart.update()
            End If
        End Sub


        Public Function isUpdateSoftwareToDo() As Boolean
            Dim curVersionData As String = External.current.dataUpdateVersion
            Dim curVersionClient As String = External.current.clientUpdateVersion
            Dim isClientOutDated As Boolean = (basicPartsToUpdate = BasicParts.CLIENT_AND_DATA AndAlso curVersionData <> curVersionClient)
            If isClientOutDated Then Return True 'This verification is done before check newVersion to ensure local updates w/o internet connection works

            If webVersion = -1 Then webVersion = actionner.getVersion().version
            newVersion = webVersion

            Dim isDataOutDated As Boolean = basicPartsToUpdate = BasicParts.CLIENT_AND_DATA AndAlso curVersionData <> newVersion
            isClientOutDated = newVersion <> curVersionClient

            Return isDataOutDated Or isClientOutDated
        End Function

        Public Sub update(Optional ByVal autoReboot As Boolean = False)
            'Quit if software not allowing update
            If ensureCanUpdate(True) = False Then Exit Sub

            update(autoReboot, 0)
        End Sub

        Private Sub update(ByVal autoReboot As Boolean, ByVal newVersion As Integer)
            webVersion = -1 'Ensure that webVersion of client is taken back from server when a new update checking is asked. So, if update asked and none is to be done, and then another update asked after a new one had been posted, the new version will be detected

            If IO.File.Exists(clientCopyingFile) Then Throw New ExternalUpdateException(ExternalUpdateException.ErrorTypes.CLIENT_CURRENTLY_COPIED_BY_ANOTHER)
            If isUpdateSoftwareToDo() = False Then
                External.current.setStatusText("Le logiciel est présentement à jour", False)
                Exit Sub
            End If

            Dim isClientOnlyUpdate As Boolean = Not External.current.isSoftwareVersionEqualsServer()

            Dim updateFromLocal As Boolean = clientPart.checkIfUpdatingFromLocal()
            If updateFromLocal = False Then
                ensureUpdateServerAccessible()
                setNewVersion(newVersion)
            Else
                Me.newVersion = External.current.dataUpdateVersion
            End If

            'Write a file to continue after major update reboots (this is useful only when server & client are updated, otherwise, updateToDo is detected via different version both those)
            If hasMajorUpdateMadeRegressVersion Then
                IO.File.WriteAllText(clientMajorVersionFile, newVersionNotRegressed)
            ElseIf IO.File.Exists(clientMajorVersionFile) Then
                IO.File.Delete(clientMajorVersionFile)
            End If

            External.current.setStatusText("Mise à jour du logiciel... (Ceci peut prendre plusieurs minutes)", False)

            If updateFromLocal Then
                clientPart.updateLocal()
            Else
                doUpdate()
            End If

            If Not isClientOnlyUpdate Then callOnEndSoftwareUpdate()

            'Reboot if needed
            If IO.File.Exists(clientUpdaterListFile) Then
                If autoReboot = False Then External.current.alertUserOfRestart()
                External.current.restartSoftware()
            End If

            deleteUpdatingFile()

            External.current.setStatusText("Le logiciel a été mis à jour", False)
        End Sub

        Private Function ensureCanUpdate(ByVal informUser As Boolean) As Boolean
            If updating Then Return True
            If Not External.current.canSoftwareBeUpdated(informUser) Then Return False

            updating = True

            Return True
        End Function

        Private Sub callOnEndSoftwareUpdate()
            If Not updating Then Exit Sub

            External.current.onEndSoftwareUpdate()
            updating = False
        End Sub

        Public Shared Sub deleteUpdatingFile()
            Dim updatingFile As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "updating.on"

            If IO.File.Exists(updatingFile) Then IO.File.Delete(updatingFile)
        End Sub

        Public Enum notStartingReasons As Integer
            ErrorHappened = 0
            AbortedByClient = 1
            AutoupdateEnded = 2
            NoUpdates = 3
            NoUpdatesHaveToClose = 4
            LockedByServer = 5
        End Enum

        Public Function launchStartingUpdateProcess(ByVal autoUpdate As Boolean, ByVal askWhenOnlyClientUpdate As Boolean) As notStartingReasons
            'Si la version du logiciel sur le poste n'est pas égal à celle sur le serveur, doit mettre à jour
            If External.current.isSoftwareVersionEqualsServer() = False Then
                External.current.setLoadingForward("Mise à jour du poste") 'Avance de un le chargement
                External.current.setLoadingTopMost(False)
                If askWhenOnlyClientUpdate = False OrElse External.current.askUpdateForClientOnly() Then
                    update(True, External.current.dataUpdateVersion)
                Else
                    External.current.setLoadingTopMost(True)
                    Return notStartingReasons.AbortedByClient
                End If
                External.current.setLoadingTopMost(True)
            End If

            'TODO : This line shall be called first, but with another param that tells if update is only the client
            '       If so, the server will have a counter which could block updating Clinica as a whole
            'Quit if software not allowing update
            If Not ensureCanUpdate(False) Then Return notStartingReasons.LockedByServer


            'Termine le processus de MAJ si updater.exe a planté
            If IO.File.Exists("updater.list") Then
                External.current.restartSoftware()
                Return notStartingReasons.ErrorHappened
            End If

            deleteUpdatingFile() ' S'assure que le fichier indiquant que le logiciel se met à jour n'existe pas

            'Continue update to version asked (due to a major update restart)
            If IO.File.Exists(clientMajorVersionFile) Then
                External.current.setLoadingForward("Mise à jour automatique en cours")
                update(True, IO.File.ReadAllLines(clientMajorVersionFile)(0))
            End If

            'If detected autoupdate file, then stop software
            If IO.File.Exists(clientAutoupdateFile) AndAlso isUpdateSoftwareToDo() = False Then 'Quitte le logiciel s'il a été redémarré du à une mise à jour parti via /update
                callOnEndSoftwareUpdate()
                IO.File.Delete(clientAutoupdateFile)
                Return notStartingReasons.AutoupdateEnded
            End If

            'Launch autoupdate
            If autoUpdate Then
                Dim clientVersion As String = External.current.clientUpdateVersion
                Dim serverVersion As String = External.current.dataUpdateVersion

                External.current.setLoadingForward("Mise à jour automatique en cours")
                IO.File.WriteAllText(clientAutoupdateFile, "")
                update(True)
                'Si Clinica n'a pas été redémarrée
                IO.File.Delete(clientAutoupdateFile)

                callOnEndSoftwareUpdate()

                If clientVersion <> External.current.clientUpdateVersion OrElse serverVersion <> External.current.dataUpdateVersion Then
                    Return notStartingReasons.AutoupdateEnded
                Else
                    Return notStartingReasons.NoUpdatesHaveToClose
                End If
            End If

            callOnEndSoftwareUpdate()

            Return notStartingReasons.NoUpdates
        End Function

    End Class

End Namespace