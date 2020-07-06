Namespace Windows.Forms


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TasksManager
        Inherits System.Windows.Forms.UserControl

        'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Requise par le Concepteur Windows Form
        Private components As System.ComponentModel.IContainer

        'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
        'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
        'Ne la modifiez pas à l'aide de l'éditeur de code.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.tasksTypesList = New System.Windows.Forms.ListBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.createTask = New System.Windows.Forms.Button
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
            Me.runningTasksList = New CI.Base.Windows.Forms.DataGridPlus
            Me.Label2 = New System.Windows.Forms.Label
            Me.taskType = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.taskStep = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.taskProgression = New CI.Base.Windows.Forms.DataGridViewProgressColumn
            Me.taskAction = New System.Windows.Forms.DataGridViewButtonColumn
            Me.taskResult = New System.Windows.Forms.DataGridViewLinkColumn
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            CType(Me.runningTasksList, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'tasksTypesList
            '
            Me.tasksTypesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tasksTypesList.FormattingEnabled = True
            Me.tasksTypesList.Location = New System.Drawing.Point(0, 16)
            Me.tasksTypesList.Name = "tasksTypesList"
            Me.tasksTypesList.Size = New System.Drawing.Size(179, 238)
            Me.tasksTypesList.TabIndex = 0
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(-3, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(109, 13)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Types de tâches :"
            '
            'createTask
            '
            Me.createTask.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.createTask.Enabled = False
            Me.createTask.Location = New System.Drawing.Point(0, 269)
            Me.createTask.Name = "createTask"
            Me.createTask.Size = New System.Drawing.Size(106, 23)
            Me.createTask.TabIndex = 2
            Me.createTask.Text = "Créer une tâche"
            Me.createTask.UseVisualStyleBackColor = True
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.tasksTypesList)
            Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
            Me.SplitContainer1.Panel1.Controls.Add(Me.createTask)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.runningTasksList)
            Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
            Me.SplitContainer1.Size = New System.Drawing.Size(531, 295)
            Me.SplitContainer1.SplitterDistance = 182
            Me.SplitContainer1.TabIndex = 3
            '
            'runningTasksList
            '
            Me.runningTasksList.AllowUserToAddRows = False
            Me.runningTasksList.AllowUserToDeleteRows = False
            Me.runningTasksList.AllowUserToResizeRows = False
            Me.runningTasksList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.runningTasksList.autoSelectOnDataSourceChanged = True
            Me.runningTasksList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            Me.runningTasksList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.runningTasksList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.taskType, Me.taskStep, Me.taskProgression, Me.taskAction, Me.taskResult})
            Me.runningTasksList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
            Me.runningTasksList.Location = New System.Drawing.Point(2, 16)
            Me.runningTasksList.Name = "runningTasksList"
            Me.runningTasksList.RowHeadersVisible = False
            Me.runningTasksList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.runningTasksList.Size = New System.Drawing.Size(340, 276)
            Me.runningTasksList.TabIndex = 2
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(-1, 0)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(110, 13)
            Me.Label2.TabIndex = 1
            Me.Label2.Text = "Tâches en cours :"
            '
            'taskType
            '
            Me.taskType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.taskType.DataPropertyName = "taskType"
            Me.taskType.Frozen = True
            Me.taskType.HeaderText = "Type"
            Me.taskType.Name = "taskType"
            Me.taskType.Width = 56
            '
            'taskStep
            '
            Me.taskStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.taskStep.DataPropertyName = "taskStep"
            Me.taskStep.HeaderText = "Étape"
            Me.taskStep.Name = "taskStep"
            Me.taskStep.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.taskStep.Width = 60
            '
            'taskProgression
            '
            Me.taskProgression.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.taskProgression.DataPropertyName = "taskProgression"
            Me.taskProgression.HeaderText = "Progression"
            Me.taskProgression.Name = "taskProgression"
            Me.taskProgression.ProgressBarColor = System.Drawing.Color.Blue
            Me.taskProgression.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.taskProgression.Width = 68
            '
            'taskAction
            '
            Me.taskAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.taskAction.DataPropertyName = "taskStop"
            Me.taskAction.HeaderText = "Action"
            Me.taskAction.Name = "taskAction"
            Me.taskAction.Width = 43
            '
            'taskResult
            '
            Me.taskResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.taskResult.DataPropertyName = "taskResult"
            Me.taskResult.HeaderText = "Résultat"
            Me.taskResult.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.taskResult.Name = "taskResult"
            Me.taskResult.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
            Me.taskResult.Width = 52
            '
            'TasksManager
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.SplitContainer1)
            Me.MinimumSize = New System.Drawing.Size(323, 141)
            Me.Name = "TasksManager"
            Me.Size = New System.Drawing.Size(531, 295)
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel1.PerformLayout()
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.Panel2.PerformLayout()
            Me.SplitContainer1.ResumeLayout(False)
            CType(Me.runningTasksList, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tasksTypesList As System.Windows.Forms.ListBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents createTask As System.Windows.Forms.Button
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents runningTasksList As CI.Base.Windows.Forms.DataGridPlus
        Friend WithEvents taskType As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents taskStep As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents taskProgression As CI.Base.Windows.Forms.DataGridViewProgressColumn
        Friend WithEvents taskAction As System.Windows.Forms.DataGridViewButtonColumn
        Friend WithEvents taskResult As System.Windows.Forms.DataGridViewLinkColumn

    End Class


End Namespace