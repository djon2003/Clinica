Namespace Windows.Forms


    Public Class TasksManager

        Private form As System.Windows.Forms.Form

        Private curTask As TaskBase
        Private _splitterPosition As Integer
        Private isLoaded As Boolean = False
        Private isResizing As Boolean = False

        Public Event ResultToShow(ByVal sender As Object, ByVal task As TaskBase)
        Public Event TaskEnded(ByVal sender As Object, ByVal task As TaskBase)
        Public Event TasksLoaded(ByVal sender As Object)

        Public Sub New()

            ' Cet appel est requis par le Concepteur Windows Form.
            InitializeComponent()

            ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        End Sub

        Private _autoLoad As Boolean = True

        Public Property autoLoad() As Boolean
            Get
                Return _autoLoad
            End Get
            Set(ByVal value As Boolean)
                _autoLoad = value
            End Set
        End Property

        Public Property splitterPosition() As Integer
            Get
                Return _splitterPosition
            End Get
            Set(ByVal value As Integer)
                _splitterPosition = value
                SplitContainer1.SplitterDistance = value
            End Set
        End Property

        Private Sub MyFormsResizeBegin(ByVal sender As Object, ByVal e As EventArgs)
            _splitterPosition = SplitContainer1.SplitterDistance
            isResizing = True
        End Sub
        Private Sub MyFormsResizeEnd(ByVal sender As Object, ByVal e As EventArgs)
            If isLoaded Then SplitContainer1.SplitterDistance = _splitterPosition
            isResizing = False
        End Sub

        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            MyBase.OnResize(e)
            If isLoaded Then SplitContainer1.SplitterDistance = _splitterPosition
        End Sub

        Public Sub addTaskType(ByVal taskType As CI.Base.PluginTaskBase)
            tasksTypesList.Items.Add(taskType)
        End Sub

        Public Sub addTaskType(ByVal taskType As List(Of CI.Base.PluginTaskBase))
            tasksTypesList.Items.AddRange(taskType.ToArray)
        End Sub

        Private Sub createTask_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles createTask.Click
            Dim newTask As TaskBase = CType(tasksTypesList.SelectedItem, PluginTaskBase).createTask()
            If newTask Is Nothing Then
                MessageBox.Show("Impossible de créer une tâche de ce type, car le nombre de tâches simultannées pour ce type a été atteint", "Création de tâche impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If

            newTask.startTask()
            loadRunningTasks()
        End Sub

        Private Sub changeRunningTasksData(ByVal sender As Object, ByVal columnIndex As Integer, ByVal data As Object)
            Dim i As Integer = 0
            Dim curData As DataTable = CType(runningTasksList.DataSource, DataTable)
            For Each curRow As DataRow In curData.Rows
                If curRow(0).Equals(sender) Then
                    curData.Rows(i)(columnIndex) = data
                    Exit For
                End If
                i += 1
            Next

            If i < runningTasksList.Rows.Count Then
                'Force refresh of runningTasksList
                runningTasksList.AutoResizeColumns()
            End If
        End Sub

        Private Sub taskStepChanged(ByVal sender As Object, ByVal e As EventArgs)
            If form.InvokeRequired Then
                Me.Invoke(New EventHandler(AddressOf taskStepChanged), New Object() {sender, e})
                Exit Sub
            End If

            Dim task As TaskBase = sender
            Dim stepText As String = task.currentStep & "/" & task.maximumSteps
            If task.currentStepName <> "" Then
                stepText &= " : " & task.currentStepName
            Else
                stepText &= " : Démarrage"
                changeRunningTasksData(sender, 3, "Arrêter")
            End If
            changeRunningTasksData(sender, 1, stepText)

            changeRunningTasksData(sender, 3, If(task.isTaskRunning, "Arrêter", "Supprimer"))
            changeRunningTasksData(sender, 4, getResultText(task))

            'Do corrections
            'changeRunningTasksData(sender, 0, sender)
            taskProgressionChanged(sender, e)
        End Sub

        Private Sub taskProgressionChanged(ByVal sender As Object, ByVal e As EventArgs)
            If form.InvokeRequired Then
                Me.Invoke(New EventHandler(AddressOf taskProgressionChanged), New Object() {sender, e})
                Exit Sub
            End If

            changeRunningTasksData(sender, 2, CType(sender, TaskBase).getTaskProgession())
        End Sub

        Private Sub taskHasEnded(ByVal sender As Object, ByVal e As EventArgs)
            If form.InvokeRequired Then
                Me.Invoke(New EventHandler(AddressOf taskHasEnded), New Object() {sender, e})
                Exit Sub
            End If

            changeRunningTasksData(sender, 4, getResultText(sender))
            changeRunningTasksData(sender, 3, "Supprimer")

            RaiseEvent TaskEnded(Me, sender)
        End Sub

        Private Function getResultText(ByVal task As TaskBase) As String
            If task.isTaskRunning OrElse task.currentStep = 0 Then Return "En attente"

            Dim resultText As String = "Aucune erreur"
            With task
                If .nbErrors = 0 AndAlso .nbProcessed = 0 Then
                    resultText = "Rien à produire"
                ElseIf .nbErrors <> 0 Then
                    resultText = .nbErrors & " erreur(s)"
                End If
            End With

            Return resultText
        End Function

        Delegate Sub Loading()

        Public Sub loadRunningTasks()
            If form.InvokeRequired Then
                Me.Invoke(New Loading(AddressOf loadRunningTasks))
                Exit Sub
            End If

            Dim tasks As List(Of TaskBase) = PluginTasksManager.getInstance.getTasks()
            Dim dsTasks As New DataTable
            For Each curColumn As DataGridViewColumn In runningTasksList.Columns
                Dim curType As Type = curColumn.ValueType
                If curType Is Nothing Then curType = GetType(Object)
                dsTasks.Columns.Add(New DataColumn(curColumn.DataPropertyName, curType))
            Next
            For Each task As TaskBase In tasks
                AddHandler task.stepChanged, AddressOf taskStepChanged
                AddHandler task.taskStepProgressed, AddressOf taskProgressionChanged
                AddHandler task.taskEnded, AddressOf taskHasEnded

                Dim row() As Object
                ReDim row(dsTasks.Columns.Count - 1)
                row(0) = task
                row(1) = task.currentStep & "/" & task.maximumSteps
                If task.currentStepName <> "" Then
                    row(1) &= " : " & task.currentStepName
                Else
                    row(1) &= " : Démarrage"
                End If
                row(2) = task.getTaskProgession()
                row(3) = If(task.isTaskRunning, "Arrêter", "Supprimer")
                row(4) = getResultText(task)

                dsTasks.Rows.Add(row)
            Next

            runningTasksList.DataSource = dsTasks

            RaiseEvent TasksLoaded(Me)
        End Sub

        Private Sub loadTaskTypes()
            tasksTypesList.Items.Clear()

            Dim tasksPlugins As List(Of PluginTaskBase) = PluginTasksManager.getInstance.getItemables()

            If tasksPlugins.Count <> 0 Then addTaskType(tasksPlugins)
        End Sub

        Private Sub tasksTypesList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tasksTypesList.SelectedIndexChanged
            createTask.Enabled = isLoaded AndAlso tasksTypesList.SelectedIndex <> -1
        End Sub

        Private Sub TasksManager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        End Sub

        Private Sub SplitContainer1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
            If Not isResizing Then _splitterPosition = SplitContainer1.SplitterDistance
        End Sub

        Private Sub TasksManager_ParentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ParentChanged
            'Registering to Activated event of the form
            Dim curParent As Control = Me.Parent
            While curParent IsNot Nothing
                AddHandler curParent.ParentChanged, AddressOf TasksManager_ParentChanged
                curParent = curParent.Parent
            End While
            If Me.FindForm IsNot Nothing Then
                Me.form = Me.FindForm()
                If autoLoad Then AddHandler Me.FindForm().Activated, AddressOf formActivated
            End If
        End Sub

        Public Sub loadControl()
            If Me.FindForm() Is Nothing Then Exit Sub
            If Me.FindForm().InvokeRequired Then
                Me.Invoke(New Loading(AddressOf loadControl))
                Exit Sub
            End If

            loadTaskTypes()
            loadRunningTasks()

            AddHandler Me.FindForm.ResizeBegin, AddressOf MyFormsResizeBegin
            AddHandler Me.FindForm.ResizeEnd, AddressOf MyFormsResizeEnd

            isLoaded = True
            createTask.Enabled = tasksTypesList.SelectedIndex <> -1

            If autoLoad Then RemoveHandler Me.FindForm().Activated, AddressOf formActivated
        End Sub

        Private Sub formActivated(ByVal sender As Object, ByVal e As EventArgs)
            loadControl()
        End Sub

        Private Sub runningTasksList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles runningTasksList.CellClick
            If e.RowIndex = -1 Then Exit Sub

            'Action column
            If runningTasksList.Columns(e.ColumnIndex).Name = taskAction.Name Then
                Dim task As TaskBase = CType(runningTasksList.DataSource, DataTable).Rows(e.RowIndex)(0)
                If task.isTaskRunning Then
                    If MessageBox.Show("Êtes-vous certain de vouloir arrêter cette tâche ?", "Confirmation d'arrêt d'une tâche", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
                    task.stopTask()
                Else
                    If MessageBox.Show("Êtes-vous certain de vouloir supprimer cette tâche ?", "Confirmation de suppression d'une tâche", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

                    task.deleteTask()
                End If
            End If

            'Result column
            If runningTasksList.Columns(e.ColumnIndex).Name = taskResult.Name Then
                Dim task As TaskBase = CType(runningTasksList.DataSource, DataTable).Rows(e.RowIndex)(0)
                If task.isTaskRunning OrElse task.resultHtml = "" Then Exit Sub

                RaiseEvent ResultToShow(Me, task)
            End If
        End Sub
    End Class


End Namespace