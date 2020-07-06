Public Class MainForm

    Private launchedTime As Date = Date.Now
    Private WithEvents ChangingDateTimer As New System.Windows.Forms.Timer
    Private lastTimeReceivedLog As Date = New Date(2000, 1, 1)

    Private Delegate Sub loggingText(ByVal [text] As String)

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ChangingDateTimer.Interval = 3600000
        ChangingDateTimer.Start()
    End Sub

    Private Sub changingDateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChangingDateTimer.Tick
        If (launchedTime.Date = Date.Today) = False Then
            Logger.saveLog(launchedTime)
            'TODO: COUNTERS - Shall it be kept?: TCPHost.getInstance.saveCounters(launchedTime)
            launchedTime = Date.Today

            If Software.getInstance().config.restartEveryDay Then
                Software.restart(False)
            Else
                'TODO: COUNTERS - Shall it be kept?:  TCPHost.getInstance.loadCounters()
            End If
        End If
    End Sub

    Private Sub form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'TODO: COUNTERS - Shall it be kept?: TCPHost.getInstance.saveCounters(Date.Today)
        Logger.saveLog(Date.Today)
        End
    End Sub

    Private Sub mainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Software.getInstance().config.serverPassword <> String.Empty Then
            Dim inputBox As New InputBoxPlus()
            Dim password = inputBox("Veuillez entrer le mot de passe de sortie", "Mot de passe de sortie")
            If password = String.Empty Then
                e.Cancel = True
                Exit Sub
            End If

            If password <> Software.getInstance().config.serverPassword Then
                MessageBox.Show("Mot de passe incorrect", "Mot de passe invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Cancel = True
                Exit Sub
            End If
        End If

        If clientsConnected.Text > 0 AndAlso MessageBox.Show("Êtes-vous sûr de vouloir fermer le serveur de Clinica, car il y a des utilisateurs de connectés présentement ?", "Confirmation de fermeture", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        ElseIf clientsConnected.Text > 0 Then
            TCPHost.getInstance.closeAllClients()
        End If

        Dim keepAliveProc() As Process = Process.GetProcessesByName(Software.keepAliveProcessName)
        If keepAliveProc.Length <> 0 Then
            keepAliveProc(0).Kill()
        End If
    End Sub

    Private Sub callDelayedLog(ByVal delayedLog As DelayedLog)
        Try
            Threading.Thread.Sleep(MINIMUM_LOG_INTERVAL_MS)

            If delayedLog.delayedLogTime >= lastTimeReceivedLog Then logText(delayedLog.delayedLog)
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Private currentDelayedLogThread As Threading.Thread
    Private Const MINIMUM_LOG_INTERVAL_MS As Integer = 500

    Private Class DelayedLog
        Public delayedLogTime As Date
        Public delayedLog As String
    End Class

    ''' <summary>
    ''' Change logs
    ''' </summary>
    ''' <param name="text">full logs to show</param>
    ''' <remarks>It can suppress calls that are too near in time</remarks>
    Public Sub logText(ByVal [text] As String)
        'Console.WriteLine(">>>>" & Me.InvokeRequired)
        'Console.WriteLine([text].Substring([text].Length - 30))

        If Me.InvokeRequired Then
            If Date.Now.Subtract(lastTimeReceivedLog).TotalMilliseconds < MINIMUM_LOG_INTERVAL_MS Then
                If currentDelayedLogThread Is Nothing OrElse Not currentDelayedLogThread.IsAlive Then
                    Dim delayingLog As New DelayedLog
                    delayingLog.delayedLogTime = Date.Now
                    delayingLog.delayedLog = [text]
                    currentDelayedLogThread = New Threading.Thread(AddressOf callDelayedLog)
                    currentDelayedLogThread.Start(delayingLog)
                End If
                Exit Sub
            End If

            'Console.WriteLine("continue")

            Dim d As New loggingText(AddressOf logText)
            Me.BeginInvoke(d, New Object() {[text]})
        Else
            lastTimeReceivedLog = Date.Now

            Me.messages.SuspendLayout()
            Try
                Me.messages.Text = [text]
            Catch ex As System.OutOfMemoryException
                addErrorLog(New Exception("text.length=" & [text].Length, ex))
                Logger.saveLog(Date.Today)
            End Try
            Me.messages.SelectionStart = Me.messages.Text.Length
            Me.messages.ScrollToCaret()
            Me.messages.ResumeLayout()
            Me.clientsConnected.Text = TCPHost.getInstance.countClients()
        End If
    End Sub

    Private Sub ConfigurationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigurationsToolStripMenuItem1.Click
        ConfigurationsManager.getInstance.showConfigs()
    End Sub

    Private Sub MettreÀJourToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MettreÀJourToolStripMenuItem.Click
        If MessageBox.Show("Êtes-vous certain de vouloir mettre à jour ?" & vbCrLf & "Ceci peut causer des problèmes avec Clinica. Veuillez plutôt utiliser la mise à jour de Clinica.", "Confirmation de mise à jour", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = System.Windows.Forms.DialogResult.No Then Exit Sub

        TabControl1.SelectedTab = TabControl1.TabPages("pageMessages")
        Application.DoEvents()

        Software.getInstance().checkExternalUpdates()
    End Sub

    Private Sub EnvoyerUnMessageÀTousToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnvoyerUnMessageÀTousToolStripMenuItem.Click
        Dim toSend As String = ""
        If MessageBox.Show("Envoyer un message depuis un fichier ?" & vbCrLf & DataTCP.MESSAGE_DEFINITION, "Message type", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            toSend = InputBox("Nom du fichier", "Message type")
            If IO.File.Exists(toSend) = False Then
                toSend = ""
            Else
                toSend = IO.File.ReadAllText(toSend)
            End If
        Else
            toSend = InputBox("Message à envoyer à tous les ordinateurs connectés.", "Message à tous")
        End If
        If toSend = "" Then Exit Sub

        TCPHost.getInstance.sendToAllClients(0, toSend)
    End Sub

    Private Sub ctlTasksManager_ResultToShow(ByVal sender As Object, ByVal task As Base.TaskBase) Handles ctlTasksManager.ResultToShow
        Dim tempFile As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "csstresult.html"
        IO.File.WriteAllText(tempFile, task.resultHtml)
        launchAProccess(tempFile)
        MsgBox(task.resultHtml, , task.nbProcessed & "/" & task.nbErrors)
    End Sub
End Class
