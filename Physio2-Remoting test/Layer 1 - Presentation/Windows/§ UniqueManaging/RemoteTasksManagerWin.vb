Friend Class RemoteTasksManagerWin

    Private lastTaskEnded As TaskBase
    Private lockingTaskEnded As New System.Threading.Mutex()
    Private isLoading As Boolean = True

    Public Sub New()
        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        Me.MdiParent = myMainWin
        AddHandler ctlTaskManager.TasksLoaded, AddressOf tasksLoaded
    End Sub

    Private Sub tasksLoaded(ByVal sender As Object)
        activateForm(sender, EventArgs.Empty)
    End Sub

    Private Sub RemoteTasksManagerWin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        PluginTasksManager.getInstance.unsubscribe()
    End Sub

    Private Sub RemoteTasksManagerWin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        For Each curPlugin As PluginTaskClient In PluginTasksManager.getInstance.getItemables()
            ctlTaskManager.addTaskType(curPlugin)
        Next

        Dim loadThread As New Threading.Thread(AddressOf loading)
        loadThread.Start()
    End Sub

    Private Sub loading()
        Dim exceptionHappened As Boolean = False
        Try
            PluginTasksManager.getInstance().populateTasks()
        Catch ex As SendingServerCmdException
            exceptionHappened = True
        Catch ex As Exception
            addErrorLog(ex)
            exceptionHappened = True
        End Try

        If exceptionHappened Then
            MessageBox.Show("Impossible de charger les tâches distantes depuis le serveur de Clinica. Tentez de fermer et de rouvrir cette fenêtre. Si le problème persiste, veuillez vérifier le serveur de Clinica et au besoin le redémarrer.", "Impossible de charger les tâches distantes", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        PluginTasksManager.getInstance.subscribe()

        isLoading = False
        Me.ctlTaskManager.loadControl()
        activateForm(Me, EventArgs.Empty)
    End Sub

    Private Sub activateForm(ByVal sender As Object, ByVal e As EventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandler(AddressOf activateForm), New Object() {Me, EventArgs.Empty})
            Exit Sub
        End If

        If lblLoading.Visible = False OrElse isLoading Then Exit Sub

        lblLoading.Visible = False
        ctlTaskManager.Enabled = True
    End Sub

    Public Sub loadRunningTasks()
        ctlTaskManager.loadRunningTasks()
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        'Nothing to do... everything is in control
    End Sub

    Public Overrides ReadOnly Property savingMethod() As [Delegate]
        Get
            Return Nothing
        End Get
    End Property

    Private Sub ctlTaskManager_ResultToShow(ByVal sender As Object, ByVal task As Base.TaskBase) Handles ctlTaskManager.ResultToShow
        TextWindow.getInstance.currentData = task.resultHtml
        TextWindow.getInstance.isLocked = True
        TextWindow.getInstance.isHTML = True
        TextWindow.getInstance.Text = "Résultat de la tâche : " & task.ToString()

        Dim sel As String = TextWindow.getInstance.ShowTexteModif(0)
    End Sub

    Private Sub ctlTaskManager_TaskEnded(ByVal sender As Object, ByVal task As Base.TaskBase) Handles ctlTaskManager.TaskEnded
        lockingTaskEnded.WaitOne()

        lastTaskEnded = task
        Dim dbPath As String = PreferencesManager.getGeneralPreferences()("RemoteTask-" & task.creator.getIdentifier() & "-AutoSaveResultDBPath")
        If dbPath <> "" AndAlso dbPath <> "Ne pas enregistrer" Then
            saveToDB("Résultat de la tâche le " & DateFormat.getTextDate(Date.Now) & " à " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.FullTime).Replace(":", "."), dbPath, New String() {task.creator.name}, New String() {task.creator.description})
        End If

        lockingTaskEnded.ReleaseMutex()
    End Sub

    Private Sub internalFileSaving(ByVal type As String, ByVal filePath As String)
        IO.File.WriteAllText(filePath, lastTaskEnded.resultHtml)
    End Sub

    Public Function saveToDB(ByVal myNom As String, ByVal myPath As String, ByVal myMotsCles() As String, ByVal myDescription() As String) As String
        AddHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
        If InternalDBManager.getInstance.getDBFolder(myPath) Is Nothing Then InternalDBManager.getInstance.addDBFolder(myPath)
        Dim folder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(myPath)
        Dim returning As String = InternalDBManager.getInstance.addItem(myNom, folder, "Rapport", False, myMotsCles, myDescription, False, True, myNom & ".HTMLRPT")
        RemoveHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
        If returning <> "" Then
            MessageBox.Show(returning, "Erreur")
            Return returning
        End If

        Return ""
    End Function
End Class