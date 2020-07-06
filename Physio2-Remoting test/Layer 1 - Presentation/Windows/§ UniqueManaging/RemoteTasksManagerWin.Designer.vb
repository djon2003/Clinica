<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class RemoteTasksManagerWin
    Inherits SingleWindow

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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
        Me.ctlTaskManager = New CI.Base.Windows.Forms.TasksManager
        Me.lblLoading = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ctlTaskManager
        '
        Me.ctlTaskManager.autoLoad = False
        Me.ctlTaskManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ctlTaskManager.Enabled = False
        Me.ctlTaskManager.Location = New System.Drawing.Point(0, 0)
        Me.ctlTaskManager.MinimumSize = New System.Drawing.Size(323, 141)
        Me.ctlTaskManager.Name = "ctlTaskManager"
        Me.ctlTaskManager.Size = New System.Drawing.Size(715, 334)
        Me.ctlTaskManager.splitterPosition = 245
        Me.ctlTaskManager.TabIndex = 0
        '
        'lblLoading
        '
        Me.lblLoading.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblLoading.AutoSize = True
        Me.lblLoading.BackColor = System.Drawing.SystemColors.Control
        Me.lblLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoading.Location = New System.Drawing.Point(248, 143)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(263, 28)
        Me.lblLoading.TabIndex = 1
        Me.lblLoading.Text = "Chargement des tâches..."
        '
        'RemoteTasksManagerWin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(715, 334)
        Me.Controls.Add(Me.lblLoading)
        Me.Controls.Add(Me.ctlTaskManager)
        Me.MinimumSize = New System.Drawing.Size(504, 229)
        Me.Name = "RemoteTasksManagerWin"
        Me.Text = "Gestion des tâches du serveur"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ctlTaskManager As CI.Base.Windows.Forms.TasksManager
    Friend WithEvents lblLoading As System.Windows.Forms.Label
End Class
