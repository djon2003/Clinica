Public Class Software

    Private Shared updater As CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater

    Private Shared newNewsSeen As Boolean = False
    Private Shared mySelf As Software
    Private isServerUpdated As Boolean = False
    Private isCheckingForServerUpdate As Boolean = False
    Private serverMessage As String = ""
    Private Shared _isStarted As Boolean = False
    Private Shared softwareUpdater As New SoftwareUpdater()

    Private Delegate Sub Refresh()

    Public Shared ReadOnly Property isStarted() As Boolean
        Get
            Return _isStarted
        End Get
    End Property

    Public Shared Sub refreshTasks()
        If myMainWin Is Nothing OrElse myMainWin.IsDisposed Then Exit Sub

        If myMainWin.InvokeRequired Then
            myMainWin.Invoke(New Refresh(AddressOf refreshTasks))
            Exit Sub
        End If

        Dim newRemoteTasks As New RemoteTasksManagerWin()
        Dim remoteTasks As RemoteTasksManagerWin = WindowsManager.getInstance().getItemable(newRemoteTasks.Text)

        If remoteTasks IsNot Nothing Then
            remoteTasks.loadRunningTasks()
        End If
    End Sub

    Public Shared Function isNewNewsSeen() As Boolean
        Return newNewsSeen
    End Function

    Public Shared Sub setNewsHasSeen()
        newNewsSeen = True
    End Sub

    Private Shared Sub setDataPath()
        Dim a As Byte = 0

        'TODO : Replace appath by dataPath ? or not and add the addSlash method here so it can be removed every where else
        If config.dataPath = "" Then Base.ConfigurationsManager.getInstance.showConfigs()

        'This is a protection against direct file modification
        If IO.Directory.Exists(config.dataPath) = False Then
            MessageBox.Show("Le chemin pour les données est invalide (Onglet ""Clinica""-->Configuration ""dataPath""). Veuillez en saisir un nouveau.", "Chemin invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Base.ConfigurationsManager.getInstance.showConfigs()
        End If

        appPath = config.dataPath
    End Sub

    Private Shared Sub connectSQLServer()
        Dim askConfig As Boolean = False

        If config.sqlServerPort = ConfigAdvancedBase.NO_SQL_PORT Then
            askConfig = Not DBLinker.getInstance.initConnection(config.sqlServerAddress, config.sqlDBName, config.sqlUsername, config.sqlPassword)
        Else
            askConfig = Not DBLinker.getInstance.initConnection(config.sqlServerAddress, config.sqlServerPort, config.sqlDBName, config.sqlUsername, config.sqlPassword)
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
            connectSQLServer()
            Exit Sub
        End If

        DBLinker.getInstance.keepAlive = True
    End Sub

    Private Shared Sub loadPreferences()
        Dim genPref As Preferences = PreferencesManager.getGeneralPreferences()
        'Exit if software doesn't equal data version, otherwise asking for all computers when a new pref is added
        If Software.isSoftwareVersionEqualsServer() = False Then Exit Sub

        Dim myPreferences As New preferencesWin()
        Dim winNbPref As Integer = myPreferences.countGeneralPrefs()

        If genPref.count <> winNbPref Then
            Loading.getInstance.Close() 'Ferme la fenêtre de chargement
            MessageBox.Show("Veuillez mettre à jour les préférences générales avant de continuer. Le logiciel redémarrera automatiquement.", "Préférences")
            myPreferences.updateGenPref()

            Software.restart(False)
        End If

        myPreferences.Dispose()
    End Sub

    Private Shared Sub connectTo()
        Loading.getInstance.Hide()

        'Chargement de la fenêtre pour se connecter au logiciel
        If InStr(Microsoft.VisualBasic.Command, "/admin", CompareMethod.Text) > 0 Then forceShowAdmin = True
        Dim myLogo As New logo(False)
        Dim myAcces As New Access(myLogo, InStr(Microsoft.VisualBasic.Command, "/nologobg", CompareMethod.Text) = 0)
        myLogo.Show()

        'Dim myAcces As New Access(Nothing, InStr(Microsoft.VisualBasic.Command, "/nologobg", CompareMethod.Text) = 0)

        myAcces.ShowDialog()

        Loading.getInstance.Show() 'Réouvre la fenêtre indiquant le chargement après l'authentification
        myAcces.Dispose()
        myAcces = Nothing

        'Enregistrement du dernier utilisateur connecté sur ce poste
        config.lastUserConnected = ConnectionsManager.currentUser
        Try
            config.save()
        Catch ex As Exception
            'Doesn't matter if it's not saved... though it's weird
            addErrorLog(ex)
        End Try

        myLogo.Dispose()
    End Sub

    Private Shared Sub loadUserPreferences()
        Try
            If currentDroitAcces(60) = False Then currentUserName = "Administrateur"
        Catch ex As Exception
        End Try

        Dim userPref As Preferences = PreferencesManager.getUserPreferences()
        If userPref Is Nothing Then Exit Sub
        Dim myPreferences As New preferencesWin()
        Dim winNbPref As Integer = myPreferences.countUserPrefs()

        'Ouvre les préférences si aucune ou si elles sont à mettre à jour
        If userPref.count <> winNbPref Then
            Loading.getInstance.Hide()
            MessageBox.Show("Veuillez mettre à jour vos préférences utilisateur", "Préférences")
            myPreferences.updateUserPref()
            Loading.getInstance.Show()
        End If

        If PreferencesManager.getUserPreferences()("ActivateNumLockOnStartup") <> "" AndAlso PreferencesManager.getUserPreferences()("ActivateNumLockOnStartup") = True Then Keyboard.numLockActivated = True
    End Sub

    Private Shared Sub saveFirstDate()
        'Enregistrement de la première date d'utilisation
        infoDivers = DBLinker.getInstance.readDB("InfoLogicielDivers", "*")
        If infoDivers Is Nothing OrElse infoDivers.Length = 0 Then
            If DBLinker.getInstance.writeDB("InfoLogicielDivers", "DateFirstUsage", "'" & Date.Now.Year & "/" & Now.Month & "/" & Now.Day & "'") = False Then
                Software.doEndProcess()
                End
            End If
            firstUsageDate = Date.Today
        Else
            If infoDivers(0, 0) = "" Then
                DBLinker.getInstance.updateDB("InfoLogicielDivers", "DateFirstUsage='" & Date.Now.Year & "/" & Now.Month & "/" & Now.Day & "'")
                firstUsageDate = Date.Today
            Else
                firstUsageDate = CDate(infoDivers(0, 0))
            End If
        End If
    End Sub

    Private Shared Sub startServer()
        'Check if command line option ensure that server is up and running
        Dim arg As String = CMD_LINE_ARGS.getCmdLineArg(CMD_LINE_ARGS.start_server)
        If arg <> "" Then
            arg = arg.Substring(CMD_LINE_ARGS.start_server.Length).ToLower
            Dim filename As String = ""
            Dim extPos As Integer = arg.IndexOf(".exe")
            If extPos <> -1 Then
                filename = arg.Substring(arg.LastIndexOf("\", extPos) + 1)
                filename = filename.Substring(0, filename.IndexOf(".exe"))
            End If
            If filename <> "" AndAlso IO.File.Exists(arg) AndAlso (Diagnostics.Process.GetProcessesByName(filename).Length = 0) Then
                launchAProccess(arg, False, ProcessWindowStyle.Minimized, , , , , True)
                System.Threading.Thread.Sleep(2000)
            End If
        End If
    End Sub

    Private Shared Sub configureServer()
        Try
            softwareUpdater.configureServer()
        Catch ex As Exception
            If ex.InnerException IsNot Nothing Then addErrorLog(ex)

            MessageBox.Show("Impossible de configurer le serveur de Clinica. Le logiciel va s'arrêter. Veuillez le redémarrer, et si le problème persiste, veuillez vérifier le serveur de Clinica et au besoin le redémarrer.", "Impossible de configurer le serveur de Clinica", MessageBoxButtons.OK, MessageBoxIcon.Error)
            doEndProcess()
            End
        End Try
    End Sub

    Private Shared Sub connectServer(ByVal appendThreadIdToName As Boolean)
        Dim curLines() As String = {""}
        Dim myName As String = Environment.MachineName & If(appendThreadIdToName = False AndAlso CMD_LINE_ARGS.getCmdLineArg(CMD_LINE_ARGS.accept_multiple_instance) = "", "", "-" & Threading.Thread.CurrentThread.ManagedThreadId)
        Dim counterFile As String = appPath & bar(appPath) & "Data\Serveur\" & myName
        If IO.File.Exists(counterFile) Then curLines = IO.File.ReadAllLines(counterFile)
        If curLines.Length <> 0 AndAlso curLines(curLines.GetUpperBound(0)) <> "" Then
            Dim sCurLines() As String = curLines(curLines.GetUpperBound(0)).Split(New Char() {"|"})
            If sCurLines(0) = DateFormat.getTextDate(Date.Today) Then
                TCPClient.getInstance.currentNbMessagesSent = sCurLines(1)
                TCPClient.getInstance.currentLastNoMessageReceived = sCurLines(2)
                TCPClient.getInstance.currentNbMessagesReveicedErrors = sCurLines(3)
            End If
        End If
        TCPClient.getInstance.acceptMultipleConnectionsByComputer = CMD_LINE_ARGS.getCmdLineArg(CMD_LINE_ARGS.accept_multiple_instance) <> ""
        TCPClient.getInstance.doReconnection = True
        TCPClient.getInstance.usePing = config.tcpClientUsePing
        TCPClient.getInstance.maxReconnectionTrials = config.maxReconnectionTrials
        Dim pingInterval As Integer
        Try
            Integer.TryParse(PreferencesManager.getGeneralPreferences()("NbSecondsForPing"), pingInterval)
        Catch
        End Try
        If pingInterval <> 0 Then TCPClient.getInstance.pingInterval = pingInterval

        Dim endSoftware As Boolean = False
        Dim tryAgain As Boolean = False
        Try
            TCPClient.getInstance.connectToHost(config.serverAddress, config.serverPort)
        Catch ex As TCPConnectionRefusedException
            MessageBox.Show("Le serveur de Clinica empêche la connexion actuellement. L'une des raisons possibles est une mise à jour en cours.", "Connexion au serveur de Clinica impossible", MessageBoxButtons.OK, MessageBoxIcon.Information)
            endSoftware = True
        Catch ex As Exception
            If MessageBox.Show("Les configurations pour la connexion au serveur de Clinica ne permettent pas de s'y connecter." & vbCrLf & "Veuillez vérifier les informations entrer ainsi que la disponibilité du serveur de Clinica." & vbCrLf & "Voulez-vous corriger les informations ? (Si non ferme le logiciel)", "Connexion au serveur de Clinica impossible", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
                endSoftware = True
            Else
                tryAgain = True
            End If
        End Try

        If endSoftware Then
            doEndProcess()
            End
        ElseIf tryAgain Then
            Base.ConfigurationsManager.getInstance.showConfigs()
            connectServer(appendThreadIdToName)
        End If
    End Sub

    Private Shared Function synchClientDate() As Boolean
        Dim ds As New DateSynchronizer
        Return ds.adviseSynch()
    End Function

    Private Shared Sub setCurrentClinic()
        'Set le numéro de la clinique en cours
        Dim noClinic(,) As String = DBLinker.getInstance.readDB("InfoClinique", "NoClinique, Nom, Courriel")
        If noClinic Is Nothing OrElse noClinic.Length = 0 Then
            Dim myClinic As New Clinic()
            myClinic.MdiParent = Nothing
            Loading.getInstance().Hide()
            MessageBox.Show("Il n'existe aucune informations de base pour la clinique. Veuillez les remplir pour continuer.", "Informations manquantes")
            myClinic.ShowDialog()
            noClinic = DBLinker.getInstance.readDB("InfoClinique", "NoClinique, Nom, Courriel")

            If noClinic Is Nothing OrElse noClinic.Length = 0 Then
                Software.doEndProcess()
                End
            End If
            Loading.getInstance().Show()
        End If
        currentClinic = noClinic(0, 0)
        currentClinicName = noClinic(1, 0)
        currentClinicEmail = noClinic(2, 0)
    End Sub

    Private Shared Sub systemEvents_PowerModeChanged(ByVal sender As Object, ByVal e As Microsoft.Win32.PowerModeChangedEventArgs)
        If e.Mode = Microsoft.Win32.PowerModes.Suspend Then
            MessageBox.Show("Clinica va se fermer, car Windows entre en veille ou en hibernation. Veuillez excuser ce désagrément qui devrait être mieux supporté dans les versions futures de Clinica", "Fermeture automatique", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            doEndProcess()
            End
        End If
    End Sub

    Public Shared Function disconnectRemoteComputer(ByVal noUser As Integer, ByVal computerName As String) As Boolean
        Loading.getInstance.Show()
        Loading.getInstance.forward("Tentative de déconnexion du poste """ & computerName & """")
        Loading.getInstance.BringToFront()
        Application.DoEvents()

        Dim myFile As String = appPath & bar(appPath) & "Users\Connected\" & noUser & "-" & computerName
        If noUser = 0 Then
            Dim files() As String = IO.Directory.GetFiles(appPath & bar(appPath) & "Users\Connected", "*-" & computerName)
            If files.Length = 0 Then Return False
            myFile = files(0)
        End If

        InternalUpdatesManager.getInstance.sendUpdate("Close(" & noUser & "," & computerName & ")")

        Dim timeOutLoop As Integer
        While IO.File.Exists(myFile) = True
            Threading.Thread.Sleep(500)
            timeOutLoop += 1
            If timeOutLoop > 100 Then Exit While
        End While

        If timeOutLoop >= 100 Then
            Loading.getInstance.Hide()
            MessageBox.Show("Impossible de déconnecter l'autre utilisateur." & vbCrLf & "Veuillez vérifier l'ordinateur nommé """ & computerName & """.", "Utilisateur déjà connecté")
            Return False
        End If

        Return True
    End Function

    Private Shared Sub configureEnvironment()
        'Used to fix the use of RSACryptoServiceProvider under a temporary windows profile
        Security.Cryptography.RSACryptoServiceProvider.UseMachineKeyStore = True
    End Sub

    Public Shared Sub startForTest()
        configureEnvironment()

        'Ensure Clinica configurations are update to date
        Base.ConfigurationsManager.getInstance().load()
        Base.ConfigurationsManager.getInstance().hasToConfigureOnlyMainOne = True
        Base.ConfigurationsManager.getInstance().ensureConfigsUpToDate()

        Software.setDataPath()
        If appPath = "" Then Exit Sub

        emptyHTMLPath = appPath & bar(appPath) & "Data\empty.html"

        Software.connectSQLServer()

        startServer()
        Software.connectServer(False)
        Software.loadPreferences()
    End Sub

    Private Shared Sub createUpdater()
        If updater IsNot Nothing Then Exit Sub

        updater = New CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater(ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.BasicParts.CLIENT_AND_DATA)
        updater.addExternalUpdate(New ExternalUpdateServer())
        updater.channel = config.updateChannel.ToString.ToLower()
        updater.softKey = config.updateKey
        updater.updateURL = config.updateUrl
        updater.username = config.updateUsername
        updater.password = config.updatePassword
        updater.userType = config.updateUserType

        updater.dataFolder = appPath
        updater.dataTempDownloadFolder = appPath & addSlash(appPath) & "Users\Temp\UpdatingData\"
        updater.dataUpdateVersion = Software.serverUpdateVersion
        updater.appPath = appPath

        updater.updateClientFromLocal = config.updateLocal
        updater.updateLocalPath = config.updateLocalPath
    End Sub

    Public Shared ReadOnly Property externalUpdater() As CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater
        Get
            createUpdater()

            Return updater
        End Get
    End Property

    Public Shared Sub checkExternalUpdates()
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


    Private Shared Sub checkExternalUpdates(ByVal showLoadingWindow As Boolean)
        Loading.getInstance.forward("Vérification des mise à jour")

        createUpdater()

        Dim isAutoUpdatingExternally As Boolean = CMD_LINE_ARGS.getCmdLineArg(New String() {CMD_LINE_ARGS.auto_update, CMD_LINE_ARGS.auto_update2, CMD_LINE_ARGS.auto_update3}) <> ""
        Dim isAutoUpdatingPref As Boolean = False
        Boolean.TryParse(PreferencesManager.getGeneralPreferences("AutoupdateOnStart"), isAutoUpdatingPref)
        Dim isAutoUpdating As Boolean = isAutoUpdatingExternally Or isAutoUpdatingPref
        Dim askWhenOnlyClientUpdate As Boolean = CMD_LINE_ARGS.getCmdLineArg(New String() {CMD_LINE_ARGS.ask_when_only_client_update, CMD_LINE_ARGS.ask_when_only_client_update2}) <> ""


        'Check for password
        If isAutoUpdatingExternally Then
            Dim auPassword As String = CMD_LINE_ARGS.getCmdLineArg(New String() {CMD_LINE_ARGS.auto_update_password})
            If auPassword <> String.Empty Then auPassword = auPassword.Substring(CMD_LINE_ARGS.auto_update_password.Length)

            Dim badPassword As Boolean = auPassword <> PreferencesManager.getGeneralPreferences("AdministratorPassword")
            If badPassword Then auPassword = String.Empty

            If auPassword = String.Empty Then
                Loading.getInstance.Hide()
                auPassword = InputBox("Veuillez entrer le mot de passe administrateur pour effectuer la mise à jour", "Mot de passe requis")
                Loading.getInstance.Show()
            End If

            'No password entered, so quitting
            badPassword = auPassword <> PreferencesManager.getGeneralPreferences("AdministratorPassword")
            If auPassword = String.Empty OrElse badPassword Then
                If auPassword <> String.Empty AndAlso badPassword Then
                    Loading.getInstance.Hide()
                    MessageBox.Show("Mot de passe administrateur incorrect. Veuillez réessayer.", "Mot passe incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                doEndProcess()
                End
            End If
        End If

        'Update if needed and/or asked
        Dim updateAnswer As CI.ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons
        Try
            updateAnswer = updater.launchStartingUpdateProcess(isAutoUpdating, askWhenOnlyClientUpdate)
        Catch ex As CI.ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException
            Select Case ex.errorType
                Case ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.SQL_FILE_CONTAINS_ERROR, ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.CLIENT_CURRENTLY_COPIED_BY_ANOTHER, ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.UPDATE_SERVER_OFFLINE, ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.NO_INTERNET_CONNECTION
                    If showLoadingWindow Then Loading.getInstance.Hide()
                    Dim extraText As String = String.Empty
                    If ex.errorType = ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException.ErrorTypes.SQL_FILE_CONTAINS_ERROR Then
                        extraText = " - Erreur dans un fichier SQL : " & vbCrLf & ex.InnerException.Message
                        addErrorLog(ex)
                    End If
                    MessageBox.Show(ex.errorText & extraText, "Mise à jour impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    If isAutoUpdatingExternally Then
                        updateAnswer = ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.ErrorHappened
                    Else
                        updateAnswer = ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.NoUpdates
                    End If
                Case Else
                    addErrorLog(ex)
                    updateAnswer = ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.ErrorHappened
            End Select
        End Try

        If updateAnswer = ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.ErrorHappened OrElse (isAutoUpdatingExternally AndAlso updateAnswer <> ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.NoUpdates) Then 'Met à jour si nécessaire
            Select Case updateAnswer
                Case ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.LockedByServer
                    If showLoadingWindow Then Loading.getInstance.forward("Mise à jour bloquée par le serveur") 'Avance de un le chargement
                Case ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.AutoupdateEnded
                    If showLoadingWindow Then Loading.getInstance.forward("Mise à jour terminée") 'Avance de un le chargement
                Case ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.NoUpdatesHaveToClose, ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.NoUpdates
                    If showLoadingWindow Then Loading.getInstance.forward("Aucune mise à jour") 'Avance de un le chargement
                Case ProjectUpdates.ProjectUpdateLibrary.Downloader.ExternalUpdater.notStartingReasons.ErrorHappened
                    If showLoadingWindow Then Loading.getInstance.forward("Erreur lors de la mise à jour") 'Avance de un le chargement
            End Select
            Threading.Thread.Sleep(10000)
            End
        End If
    End Sub

    Public Shared ReadOnly Property config() As Config
        Get
            Return Base.ConfigurationsManager.getInstance().softwareConfig
        End Get
    End Property

    Private Shared Function testWritingRights(ByVal pathToTest As String) As Boolean
        Dim filePath As String = "§test§test§test§test§." & Date.Now.ToString("mmss") & Process.GetCurrentProcess().Id & "." & Environment.MachineName
        filePath = pathToTest & bar(pathToTest) & Fichiers.replaceIllegalChars(filePath)

        Dim testPassed As Boolean = False
        Dim testStep As Byte = 0
        Dim exceptionToLog As Exception = Nothing

        For i As Byte = 1 To 10
            Try
                'Test file
                If testStep = 0 Then
                    IO.File.WriteAllText(filePath, "test")
                    testStep += 1
                End If
                If testStep = 1 Then
                    IO.File.Delete(filePath)
                    testStep += 1
                End If

                'Test folder
                If testStep = 2 Then
                    IO.Directory.CreateDirectory(filePath)
                    testStep += 1
                End If
                If testStep = 3 Then
                    IO.Directory.Delete(filePath)
                    testStep += 1
                End If
                testPassed = True
            Catch ex As System.IO.IOException
            Catch ex As UnauthorizedAccessException
            Catch ex As Exception
                exceptionToLog = ex
            End Try

            If testPassed Then
                Exit For
            Else
                Threading.Thread.Sleep(300)
            End If
        Next

        If Not testPassed Then MessageBox.Show("Clinica n'a pas un accès en écriture du dossier suivant :" & pathToTest & vbCrLf & vbCrLf & "Veuillez demander à votre administrateur d'autoriser votre utilisateur Windows à modifier ce dossier.", "Impossible de démarrer Clinica", MessageBoxButtons.OK, MessageBoxIcon.Error)
        If exceptionToLog IsNot Nothing Then addErrorLog(exceptionToLog)

        Return testPassed
    End Function


    Public Shared Sub startPreSoftwareConnection(ByVal showLoadingWindow As Boolean)
        configureEnvironment()

        'Test local writing right
        If Not testWritingRights(My.Application.Info.DirectoryPath) Then End

        Dim trySessionRemoteDisconnect As Boolean = False
        'Active la fenêtre de clinica déjà ouverte s'il y a lieu
        Try
            If CMD_LINE_ARGS.getCmdLineArg(CMD_LINE_ARGS.accept_multiple_instance) = "" AndAlso (Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName).Length > 0) Then
                trySessionRemoteDisconnect = activatePrevInstance("Clinica", False) = Activations.NotActivated
            End If
        Catch
        End Try

        AddHandler Microsoft.Win32.SystemEvents.PowerModeChanged, AddressOf systemEvents_PowerModeChanged

        'Ensure Clinica configurations are update to date
        Base.ConfigurationsManager.getInstance().load()
        'Configure only if never did, otherwise, will update either later in configureServer (at the same time if needed) or by copying the file with a local update
        If config.neverConfiged Then
            Base.ConfigurationsManager.getInstance().hasToConfigureOnlyMainOne = True
            Base.ConfigurationsManager.getInstance().ensureConfigsUpToDate()
        End If

        Software.setDataPath()
        If Not testWritingRights(appPath) Then End
        If appPath = "" Then End

        'TODO : To replace with commented code when new lockSector method will be back in function
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "\Data\LockSecteur")
        'If IO.Directory.Exists(appPath & bar(appPath) & "\Data\LockSecteur") Then
        '    My.Computer.FileSystem.DeleteDirectory(appPath & bar(appPath) & "\Data\LockSecteur", FileIO.DeleteDirectoryOption.DeleteAllContents)
        'End If


        If showLoadingWindow Then Loading.getInstance.Show() 'Affiche le loading

        If trySessionRemoteDisconnect = False Then Software.installUpdates(showLoadingWindow) 'Install any updates that required code

        emptyHTMLPath = appPath & bar(appPath) & "Data\empty.html"

        If showLoadingWindow Then Loading.getInstance.forward("Charge les images et icônes") 'Avance de un le chargement
        If firstLoading = True Then DrawingManager.getInstance.loadManager() 'Chargement du contenu du dossier "Data\Images"

        If showLoadingWindow Then Loading.getInstance.forward("Connexion à la base de données")
        Software.connectSQLServer()

        If showLoadingWindow Then Loading.getInstance.forward("Connexion au serveur de clinica")
        startServer()
        Software.connectServer(trySessionRemoteDisconnect)

        'Fait une tentative de déconnexion du Clinica dans une autre session locale
        If trySessionRemoteDisconnect Then
            If disconnectRemoteComputer(0, Environment.MachineName) = False Then
                End
            Else
                restart(False)
            End If
        End If

        If showLoadingWindow Then Loading.getInstance.forward("Charge les types de tâches du serveur") 'Avance de un le chargement
        loadRemotePlugins()

        If showLoadingWindow Then Loading.getInstance.forward("Charge les préférences générales") 'Avance de un le chargement
        Software.loadPreferences()

        'Updates
        checkExternalUpdates(showLoadingWindow)

        'After update so that copyLocal can override Clinica.config.xml (So only need to configure main one)
        configureServer()

        'Ensure same date than server
        If showLoadingWindow Then Loading.getInstance().forward("Vérification de la date avec le serveur")
        If synchClientDate() = False Then
            doEndProcess()
            End
        End If

        Software.setCurrentClinic() 'Choix de la clinique

        If showLoadingWindow Then Loading.getInstance.forward() 'Avance de un le chargement
        Software.saveFirstDate()

        If showLoadingWindow Then Loading.getInstance.forward("Charge les types de rapports") 'Avance de un le chargement
        ReportsManager.getInstance() 'Charge les types de rapports

        'Chargement de la fenêtre de maximisation
        If showLoadingWindow Then Loading.getInstance.forward("Précharge la fenêtre de maximisation et d'impression") 'Avance de un le chargement
        TextWindow.getInstance()
        PrintingForm.getInstance()
    End Sub

    Private Shared Sub loadRemotePlugins()
        Try
            PluginTasksManager.getInstance().createPluginClients()
        Catch ex As Base.SendingServerCmdException
            If ex.InnerException IsNot Nothing Then addErrorLog(ex)

            MessageBox.Show("Impossible de charger les types de tâches depuis le serveur de Clinica. Le logiciel va s'arrêter. Veuillez le redémarrer, et si le problème persiste, veuillez vérifier le serveur de Clinica et au besoin le redémarrer.", "Problème avec le serveur de Clinica", MessageBoxButtons.OK, MessageBoxIcon.Error)
            doEndProcess()
            End
        End Try
    End Sub

    Public Shared Sub start()
        LocksManager.getInstance() 'To ensure that lockMutex is created before any usage

        _isStarted = True

        startPreSoftwareConnection(True)

        Software.connectTo() 'Connexion au logiciel

        'Préchargement de la fenêtre principale & création du WindowsManager
        myMainWin = New MainWin()
        WindowsManager.create(myMainWin)
        WindowsManager.getInstance()

        Loading.getInstance.forward("Charge les préférences utilisateur") 'Avance de un le chargement
        Software.loadUserPreferences()

        PrintingHelper.defHeader = PreferencesManager.getGeneralPreferences()("PrintingHeader")
        PrintingHelper.defFooter = PreferencesManager.getGeneralPreferences()("PrintingFooter")

        Loading.getInstance.forward() 'Avance de un le chargement
        ReDim foundClient(0)
        justAppliedFocus = False

        preloadFolders()
        loadManagers()

        Loading.getInstance.forward("Charge la fenêtre principale") 'Avance de un le chargement
        myMainWin.show()

        Loading.getInstance.Close() 'Ferme le chargement
        System.Windows.Forms.Cursor.Current = Cursors.Arrow
        myMainWin.StatusText = "Bienvenue dans Clinica!"

        myMainWin.lockItems(False)

        If firstLoading = True Then
            firstLoading = False

            AddHandler Application.ApplicationExit, AddressOf isEnding
            Application.Run()
        Else
            hasRestarted = True
        End If

        newNewsSeen = False
    End Sub

    Private Shared Sub isEnding(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Shared Sub loadManagers()
        'Load EquipementManager
        Loading.getInstance.forward("Charge l'équipement")
        EquipmentsManager.getInstance()

        'Setting SMTP server for error sending
        Loading.getInstance.forward("Charge les comptes de courriel") 'Avance de un le chargement
        If MailsManager.getInstance.getMailAccounts.Count <> 0 Then
            For Each curMailAccount As MailAccount In MailsManager.getInstance().getMailAccounts()
                If curMailAccount.smtpServer.server <> String.Empty Then
                    defaultSMTPServer = curMailAccount
                End If
            Next
        End If

        'Load special dates
        Loading.getInstance.forward("Charge les journées spéciales") 'Avance de un le chargement
        SpecialDatesManager.getInstance().load()

        'Charge les alertes
        Loading.getInstance.forward("Charge les alertes") 'Avance de un le chargement
        AlertsManager.getInstance.cleanExpiredAlerts()

        'Ensure those has been created
        Loading.getInstance.forward("Charge le contrôleur de contacts") 'Avance de un le chargement
        ContactsManager.getInstance()
        Loading.getInstance.forward("Charge le contrôleur de clients") 'Avance de un le chargement
        ClientsManager.getInstance()
        Loading.getInstance.forward("Charge le contrôleur de P/O") 'Avance de un le chargement
        KeyPeopleManager.getInstance()
        Loading.getInstance.forward("Charge le contrôleur d'utilisateurs") 'Avance de un le chargement
        UsersManager.getInstance()
        Loading.getInstance.forward("Charge le contrôleur de modèles") 'Avance de un le chargement
        ModelsManager.getInstance()
        Loading.getInstance.forward("Charge les horaires") 'Avance de un le chargement
        SchedulesManager.getInstance()
        Loading.getInstance.forward("Charge les codifications dossiers") 'Avance de un le chargement
        Clinica.Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance()
    End Sub

    Private Shared Sub preloadFolders()
        Loading.getInstance.forward("Charge les dossiers") 'Avance de un le chargement
        'Load DB's objects
        TypesFilesManager.getInstance()
        InternalDBFoldersList.getInstance().currentNoUser = ConnectionsManager.currentUser
        InternalDBManager.getInstance().askForReplacing = PreferencesManager.getGeneralPreferences()("AskForReplacingDBItem")

        'Load MailManager & ContactManager
        MailFoldersList.getInstance().currentNoUser = ConnectionsManager.currentUser
        MailsManager.getInstance()
        ContactFoldersList.getInstance().currentNoUser = ConnectionsManager.currentUser
        ContactsManager.getInstance()
    End Sub

    Public Shared Sub restart(Optional ByVal askingQuitQuestion As Boolean = True)
        If myMainWin IsNot Nothing Then
            myMainWin.SetQuitToChange(askingQuitQuestion) = True
            If myMainWin.closingWin = True Then
                WindowsManager.getInstance.clear()
                myMainWin.Close()
                myMainWin = Nothing
            Else
                Exit Sub
            End If
        End If

        Software.doEndProcess()

        launchAProccess(Application.StartupPath & bar(Application.StartupPath) & "Updater.exe", , ProcessWindowStyle.Hidden, Environment.CommandLine, ProcessPriorityClass.BelowNormal)

        End
    End Sub

    Public Shared Sub doEndProcess()
        If appPath <> String.Empty Then
            'Save TCPClient counters
            Dim curLines() As String = {""}
            Dim counterFile As String = appPath & bar(appPath) & "Data\Serveur\" & Environment.MachineName
            If IO.File.Exists(counterFile) Then curLines = IO.File.ReadAllLines(counterFile)
            If curLines.Length <> 0 AndAlso (curLines(0) = "" OrElse curLines(curLines.GetUpperBound(0)).StartsWith(DateFormat.getTextDate(Date.Today) & "|")) Then
                curLines(curLines.GetUpperBound(0)) = DateFormat.getTextDate(Date.Today) & "|" & TCPClient.getInstance.currentNbMessagesSent & "|" & TCPClient.getInstance.currentLastNoMessageReceived & "|" & TCPClient.getInstance.currentNbMessagesReveicedErrors
            Else
                ReDim Preserve curLines(curLines.Length)
                curLines(curLines.GetUpperBound(0)) = DateFormat.getTextDate(Date.Today) & "|" & TCPClient.getInstance.currentNbMessagesSent & "|" & TCPClient.getInstance.currentLastNoMessageReceived & "|" & TCPClient.getInstance.currentNbMessagesReveicedErrors
            End If
            IO.Directory.CreateDirectory(appPath & bar(appPath) & "Data\Serveur")
            IO.File.WriteAllLines(counterFile, curLines)

            If currentUserName <> "" Then 'Si un utilisateur a été connecté, supprime son fichier de connexion
                Dim connectedFile As String = appPath & bar(appPath) & "Users\Connected" & "\" & ConnectionsManager.currentUser & "-" & Environment.MachineName
                If IO.File.Exists(connectedFile) = True Then IO.File.Delete(connectedFile)
                currentUserName = ""
            End If
        End If

        TCPClient.getInstance.doReconnection = False
        'Ferme la connexion SQL & la connexion avec le serveur de CHAT.
        If DBLinker.getInstance.dbConnected Then DBLinker.getInstance.dbConnected = False
        If TCPClient.getInstance.isConnected Then TCPClient.getInstance.disconnect()

        Threading.Thread.Sleep(1000) 'Wait for ClinicaServer disconnection

        'Ferme les EmailValidator.exe s'il y en d'ouvert
        killOpenedProcesses()

        RemoveHandler Microsoft.Win32.SystemEvents.PowerModeChanged, AddressOf systemEvents_PowerModeChanged
    End Sub

    Private Shared Sub installUpdates(ByVal showLoadingWindow As Boolean)
        If IO.Directory.Exists(appPath & bar(appPath) & "Updates") Then
            Dim updatesToDo() As String = IO.Directory.GetFiles(appPath & bar(appPath) & "Updates", "*.exe", IO.SearchOption.TopDirectoryOnly)
            For i As Integer = 0 To updatesToDo.Length - 1
                If showLoadingWindow Then Loading.getInstance.forward("Installation des mises à jour (" & (i + 1) & "/" & updatesToDo.Length & ")") 'Avance de un le chargement
                Dim curFile As New IO.FileInfo(updatesToDo(i))
                Dim copiedFile As String = My.Application.Info.DirectoryPath & bar(My.Application.Info.DirectoryPath) & curFile.Name
                curFile.CopyTo(copiedFile)
                Threading.Thread.Sleep(500)
                Shell(copiedFile, AppWinStyle.MinimizedNoFocus, True)
                Threading.Thread.Sleep(500)
                IO.File.Delete(copiedFile)
                curFile.Delete()
            Next i
        End If
    End Sub

    Public Shared ReadOnly Property clientUpdateVersion() As String
        Get
            If IO.File.Exists(Application.StartupPath & bar(Application.StartupPath) & "current.version") Then Return IO.File.ReadAllText(Application.StartupPath & bar(Application.StartupPath) & "current.version")

            Return ""
        End Get
    End Property

    Public Shared ReadOnly Property serverUpdateVersion() As String
        Get
            If IO.File.Exists(appPath & bar(appPath) & "Data\current.version") Then Return IO.File.ReadAllText(appPath & bar(appPath) & "Data\current.version")

            Return ""
        End Get
    End Property

    Public Shared Function isSoftwareVersionEqualsServer() As Boolean
        Return Software.clientUpdateVersion = Software.serverUpdateVersion
    End Function

    Public Shared Function getInstance() As Software
        If mySelf Is Nothing Then mySelf = New Software

        Return mySelf
    End Function

    Private Sub New()

    End Sub

#Region "Command line arguments that are possible"

    Private Class CMD_LINE_ARGS
        Public Const start_server As String = "/startserver:"
        Public Const auto_update_password As String = "/aupassword:"
        Public Const auto_update As String = "/update"
        Public Const auto_update2 As String = "/autoupdate"
        Public Const auto_update3 As String = "/au"
        Public Const accept_multiple_instance As String = "/multiple"
        Public Const ask_when_only_client_update As String = "/askwhenonlyclientupdate"
        Public Const ask_when_only_client_update2 As String = "/awocu"

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

            Select Case argToGet
                Case start_server
                    startsWith = True
                Case auto_update_password
                    startsWith = True
            End Select

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
