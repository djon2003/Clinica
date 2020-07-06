Public Class Software
    Inherits CI.Base.Software

    Public Shared keepAliveSoftware As String = "KeepAlive.exe"
    Public Const keepAliveProcessName As String = "KeepAlive"
    Public Const accept_multiple_instance As String = "/multiple"

    'TODO : This variable shall be private when the class is transfered to BaseLib
    Public plugins As New List(Of Plugin)
    Private updater As CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater
    Private isRunning As Boolean = False
    Private _hasToConfigure As Boolean = True

    Private keepAliveProcess As Process

    Protected Sub New()
        Dim softPath As String = Environment.GetCommandLineArgs(0)
        softPath = softPath.Substring(0, softPath.LastIndexOf("\"))
        keepAliveSoftware = softPath & "\" & keepAliveSoftware
    End Sub

    Public Overloads Shared Function getInstance() As Software
        setRealType(GetType(Software))

        Return CI.Base.Software.getInstance()
    End Function

#Region "Properties"

    Public ReadOnly Property externalUpdater() As CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater
        Get
            createUpdater()

            Return updater
        End Get
    End Property

    Public Shared ReadOnly Property clientUpdateVersion() As Integer
        Get
            If IO.File.Exists(Application.StartupPath & bar(Application.StartupPath) & "current.version") Then Return IO.File.ReadAllText(Application.StartupPath & bar(Application.StartupPath) & "current.version")

            Return 0
        End Get
    End Property

    Public ReadOnly Property config() As Config
        Get
            Return CType(ConfigurationsManager.getInstance.softwareConfig, Object)
        End Get
    End Property

    Public Property hasToConfigure() As Boolean
        Get
            Return _hasToConfigure
        End Get
        Set(ByVal value As Boolean)
            _hasToConfigure = value
        End Set
    End Property

    Public Property hasToConfigureOnlyMainOne() As Boolean
        Get
            Return ConfigurationsManager.getInstance().hasToConfigureOnlyMainOne
        End Get
        Set(ByVal value As Boolean)
            ConfigurationsManager.getInstance().hasToConfigureOnlyMainOne = value
        End Set
    End Property

#End Region

    Private configAnswer As TCPAnswers.Configs

    Private Sub updateConfigs()
        Loading.getInstance.forward("Charge les configurations") 'Avance de un le chargement
        ConfigurationsManager.getInstance().load()

        ConfigurationsManager.getInstance().hasToConfigureOnlyMainOne = False
        If _hasToConfigure Then
            ConfigurationsManager.getInstance().ensureConfigsUpToDate()
        ElseIf ConfigurationsManager.getInstance.isConfigurationNeeded Then
            Dim maximumText As String = "un maximum de " & config.waitingMinutesForRemoteConfig & " minute(s)"
            If config.waitingMinutesForRemoteConfig = config.WAITING_REMOTECONFIG_INFINITLY Then maximumText = "infiniment"

            Loading.getInstance.forward("Attend " & maximumText & " pour la configuration à distance (via Clinica). ") 'Avance de un le chargement
            Dim waitingTime As Integer = config.waitingMinutesForRemoteConfig
            If waitingTime <> config.WAITING_REMOTECONFIG_INFINITLY Then waitingTime = waitingTime * 60 * 1000

            Dim configs As TCPAnswers.Configs = TCPHost.getInstance.waitForData(GetType(TCPAnswers.Configs), waitingTime)
            If configs Is Nothing Then
                ConfigurationsManager.getInstance().ensureConfigsUpToDate()
            Else
                ConfigurationsManager.getInstance.saveConfigs()
            End If
        End If
    End Sub

    Private Sub load()
        Loading.getInstance.Show() 'Show loading

        checkExternalUpdates(False)

        startHost() 'First to be able to remote config

        loadPlugins()
        updateConfigs()

        connectToSQL()

        initializePlugins() 'SQL may be needed by init

        Loading.getInstance.Close() 'Close loading
    End Sub

    Private Sub startHost()
        Loading.getInstance.forward("Démarre le gestionnaire des connexions clientes") 'Avance de un le chargement

        TCPHost.getInstance().portNumbers = config.serverPort
        AddHandler TCPHost.getInstance().clientConnected, AddressOf nbClientsChanged
        AddHandler TCPHost.getInstance().clientDisconnected, AddressOf nbClientsChanged
        AddHandler TCPHost.getInstance().messageExchanged, AddressOf messageExchanged
        AddHandler TCPHost.getInstance().stoppedListenning, AddressOf stoppedListenning

        TCPHost.getInstance.startListening()
    End Sub

    Private Sub stoppedListenning()
        If mainWin IsNot Nothing AndAlso mainWin.IsDisposed = False Then mainWin.WindowState = FormWindowState.Normal
    End Sub

    Private Sub run()
        If isRunning Then Exit Sub

        startKeepAlive()

        isRunning = True
        load()

        mainWin.Show()
        
        Application.Run()
    End Sub

    Private Sub startKeepAlive()
        If IO.File.Exists(keepAliveSoftware) Then
            Dim currentKeepAlive() As Process = Process.GetProcessesByName(Software.keepAliveProcessName)
            If currentKeepAlive.Length = 0 Then
                keepAliveProcess = Process.Start(keepAliveSoftware, Environment.GetCommandLineArgs(0))
            Else
                keepAliveProcess = currentKeepAlive(0)
            End If
        End If
    End Sub

    Private Sub stopKeepAlive()
        If keepAliveProcess IsNot Nothing AndAlso keepAliveProcess.HasExited = False Then keepAliveProcess.Kill()
    End Sub

    Private Sub connectToSQL()
        Loading.getInstance.forward("Se connecte au serveur SQL") 'Avance de un le chargement

        Dim askConfig As Boolean
        If config.sqlServerPort = config.NO_SQL_PORT Then
            askConfig = Not DBLinker.getInstance.initConnection(config.sqlServerAddress, config.sqlDBName, "", "")
        Else
            askConfig = Not DBLinker.getInstance.initConnection(config.sqlServerAddress, config.sqlServerPort, config.sqlDBName, "", "")
        End If

        'Ouverture de la connexion à la base de données
        Try
            DBLinker.getInstance().dbConnected = True
        Catch ex As Exception
            askConfig = True
        End Try

        If askConfig Then
            If MessageBox.Show("Les configurations pour la connexion SQL ne permettent pas de se connecter au serveur SQL." & vbCrLf & "Veuillez vérifier les informations entrer ainsi que la disponibilité du serveur SQL." & vbCrLf & "Voulez-vous corriger les informations ? (Si non ferme le logiciel)", "Connexion au serveur SQL impossible", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
                doEndProcess()
                End
            End If
            Base.ConfigurationsManager.getInstance.showConfigs()
            connectToSQL()
            Exit Sub
        End If
    End Sub

    Private Sub loadPlugins()
        Loading.getInstance.forward("Charge les greffons") 'Avance de un le chargement

        Dim pluginsPath As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "Plugins"
        If IO.Directory.Exists(pluginsPath) = False Then Exit Sub

        Dim pluginsFiles() As String = IO.Directory.GetFiles(pluginsPath, "*.dll")
        For Each pluginFile As String In pluginsFiles
            Try
                Dim curPlugin As New Plugin(pluginFile)
                curPlugin.assignPlugin()
                Me.plugins.Add(curPlugin)
            Catch ex As Exception
                MessageBox.Show("Impossible de charger le greffon du fichier """ & pluginFile & """." & vbCrLf & "Veuillez tenter une mise à jour du logiciel ou une réinstallation.", "Chargement d'un greffon impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End Try
        Next

        setPluginsAsLoaded()
    End Sub

    Private Sub initializePlugins()
        Dim pluginsCrashed As New List(Of Plugin)
        For Each curPlugin As Plugin In plugins
            Try
                curPlugin.getPluginClass().initialize()
            Catch ex As Exception
                pluginsCrashed.Add(curPlugin)
                MessageBox.Show("Impossible d'initialiser le greffon """ & curPlugin.getPluginClass().name & """." & vbCrLf & "Veuillez tenter une mise à jour du logiciel ou une réinstallation.", "Initialisation d'un greffon impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End Try
        Next

        'Removed crashed plugin
        For Each curPlugin As Plugin In pluginsCrashed
            plugins.Remove(curPlugin)
        Next
    End Sub

    Private Sub createUpdater()
        If updater IsNot Nothing Then Exit Sub

        updater = New CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater(ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.BasicParts.CLIENT)
        updater.channel = config.updateChannel.ToString.ToLower()
        updater.softKey = config.updateKey
        updater.updateURL = config.updateUrl
        updater.username = config.updateUsername
        updater.password = config.updatePassword
        updater.userType = config.updateUserType
    End Sub

    Public Sub checkExternalUpdates()
        createUpdater()

        Try
            updater.update()
        Catch ex As CI.ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException
            Select Case ex.errorType
                Case ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.SQL_FILE_CONTAINS_ERROR, ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.CLIENT_CURRENTLY_COPIED_BY_ANOTHER, ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.UPDATE_SERVER_OFFLINE, ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.NO_INTERNET_CONNECTION
                    MessageBox.Show(ex.errorText, "Mise à jour impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Case Else
                    addErrorLog(ex)
            End Select
        End Try
    End Sub

    Private Sub checkExternalUpdates(ByVal showLoadingWindow As Boolean)
        createUpdater()

        Dim isAutoUpdatingExternally As Boolean = CMD_LINE_ARGS.getCmdLineArg(New String() {CMD_LINE_ARGS.auto_update, CMD_LINE_ARGS.auto_update2, CMD_LINE_ARGS.auto_update3}) <> ""
        Dim isAutoUpdatingPref As Boolean = False
        Dim isAutoUpdating As Boolean = isAutoUpdatingExternally Or isAutoUpdatingPref
        Dim askWhenOnlyClientUpdate As Boolean = False

        Dim updateAnswer As CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons
        Try
            updateAnswer = updater.launchStartingUpdateProcess(isAutoUpdating, askWhenOnlyClientUpdate)
        Catch ex As CI.ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException
            Select Case ex.errorType
                Case ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.NO_INTERNET_CONNECTION
                    MessageBox.Show("Impossible de tenter une mise à jour, car vous n'êtes pas connecter à l'Internet.", "Mise à jour impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Case ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.UPDATE_SERVER_OFFLINE
                    MessageBox.Show("Impossible de tenter une mise à jour, car le serveur de mise à jour est présentement fermé. Veuillez réessayer plus tard.", "Mise à jour impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Case ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.CLIENT_CURRENTLY_COPIED_BY_ANOTHER
                    MessageBox.Show("Impossible de tenter une mise à jour, car un poste copie actuellement votre version de Clinica.", "Mise à jour impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Case Else
                    Throw ex
            End Select
            Exit Sub
        End Try


        If isAutoUpdatingExternally AndAlso updateAnswer <> ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.NoUpdates Then 'Met à jour si nécessaire
            If updateAnswer = CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.AutoupdateEnded Then
                If showLoadingWindow Then Loading.getInstance.forward("Mise à jour terminée") 'Avance de un le chargement
            ElseIf updateAnswer = ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.LockedByServer Then
                If showLoadingWindow Then Loading.getInstance.forward("Mise à jour bloquée par le serveur") 'Avance de un le chargement
            Else
                If showLoadingWindow Then Loading.getInstance.forward("Aucune mise à jour") 'Avance de un le chargement
            End If
            doEndProcess()
            Threading.Thread.Sleep(10000)
            End
        End If
    End Sub

    Public Shared Sub start()
        Dim acceptMultipleInstances As Boolean = False
        For Each arg As String In Environment.GetCommandLineArgs
            If arg = accept_multiple_instance Then
                acceptMultipleInstances = True
                Exit For
            End If
        Next

        If Not acceptMultipleInstances Then activatePrevInstance("ClinicaServer")

        Software.getInstance().run()
    End Sub

    Private Sub messageExchanged(ByVal message As String, ByVal sender As CI.Base.TCPClient, ByVal isReceived As Boolean)
        Logger.logText(message, sender.portNumber & ":" & sender.name, isReceived)
    End Sub

    Private Sub nbClientsChanged(ByVal client As Base.TCPClient)
        Logger.logText("", "")
    End Sub

    Public Shared Sub doEndProcess()

    End Sub

    Public Shared Sub restart(Optional ByVal askingQuitQuestion As Boolean = True)
        Software.doEndProcess()

        Software.getInstance().stopKeepAlive()

        launchAProccess(Application.StartupPath & bar(Application.StartupPath) & "Updater.exe", , ProcessWindowStyle.Hidden, Environment.CommandLine)

        End
    End Sub

    Public Declare Function ShowWindowAsync Lib "user32" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Integer

    Private Shared Sub activatePrevInstance(ByVal argStrAppToFind As String, Optional ByVal endCurrentOne As Boolean = True)
        Dim prevHndl As IntPtr
        Dim prevProcess As Process = Nothing
        Dim result As Long

        For Each objProcess As Process In Process.GetProcesses()
            If objProcess.ProcessName.ToUpper = argStrAppToFind.ToUpper And objProcess.Id.Equals(Process.GetCurrentProcess.Id) = False Then
                prevHndl = objProcess.Handle
                prevProcess = objProcess
                If prevHndl.ToInt32 > 0 Then Exit For
            End If
        Next
        If prevHndl.ToInt32 = 0 Or prevProcess Is Nothing Then Exit Sub

        If prevProcess.MainWindowHandle.ToInt32 <> 0 Then
            result = ShowWindowAsync(prevProcess.MainWindowHandle, 2)
            result = ShowWindowAsync(prevProcess.MainWindowHandle, 9)
        Else
            MessageBox.Show("Le programme " & argStrAppToFind & " est déjà exécuté dans une autre session", "Programme en cours d'exécution")
        End If
        If endCurrentOne Then End
    End Sub

#Region "Command line arguments that are possible"

    Private Class CMD_LINE_ARGS
        Public Const auto_update As String = "/update"
        Public Const auto_update2 As String = "/autoupdate"
        Public Const auto_update3 As String = "/au"

        Public Shared Function getCmdLineArg(ByVal argToGet() As String) As String
            If argToGet Is Nothing Then Return ""

            Dim returning As String = ""
            For Each arg As String In argToGet
                returning = getCmdLineArg(arg)
                If returning <> "" Then Return returning
            Next

            Return ""
        End Function

        Public Shared Function getCmdLineArg(ByVal argToGet As String) As String
            Dim startsWith As Boolean = False

            For Each arg As String In Environment.GetCommandLineArgs
                If (Not startsWith AndAlso arg = argToGet) OrElse (startsWith AndAlso arg.StartsWith(argToGet)) Then
                    Return arg
                End If
            Next

            Return ""
        End Function
    End Class
#End Region
End Class
