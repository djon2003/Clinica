<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

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
        Me.Button1 = New System.Windows.Forms.Button
        Me.taskStep = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Button2 = New System.Windows.Forms.Button
        Me.pat = New System.Windows.Forms.RadioButton
        Me.leg = New System.Windows.Forms.RadioButton
        Me.local = New System.Windows.Forms.RadioButton
        Me.taskPourcent = New System.Windows.Forms.Label
        Me.physiolanaud = New System.Windows.Forms.RadioButton
        Me.cmlocal = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(31, 80)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(143, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Démarrer la tâche"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'taskStep
        '
        Me.taskStep.AutoSize = True
        Me.taskStep.Location = New System.Drawing.Point(28, 54)
        Me.taskStep.Name = "taskStep"
        Me.taskStep.Size = New System.Drawing.Size(35, 13)
        Me.taskStep.TabIndex = 1
        Me.taskStep.Text = "Étape"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(31, 109)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(143, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(31, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(149, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Test NAM validation"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'pat
        '
        Me.pat.AutoSize = True
        Me.pat.Location = New System.Drawing.Point(79, 237)
        Me.pat.Name = "pat"
        Me.pat.Size = New System.Drawing.Size(46, 17)
        Me.pat.TabIndex = 4
        Me.pat.Text = "PAT"
        Me.pat.UseVisualStyleBackColor = True
        '
        'leg
        '
        Me.leg.AutoSize = True
        Me.leg.Location = New System.Drawing.Point(144, 237)
        Me.leg.Name = "leg"
        Me.leg.Size = New System.Drawing.Size(46, 17)
        Me.leg.TabIndex = 4
        Me.leg.Text = "LEG"
        Me.leg.UseVisualStyleBackColor = True
        '
        'local
        '
        Me.local.AutoSize = True
        Me.local.Location = New System.Drawing.Point(12, 237)
        Me.local.Name = "local"
        Me.local.Size = New System.Drawing.Size(51, 17)
        Me.local.TabIndex = 4
        Me.local.Text = "Local"
        Me.local.UseVisualStyleBackColor = True
        '
        'taskPourcent
        '
        Me.taskPourcent.AutoSize = True
        Me.taskPourcent.Location = New System.Drawing.Point(78, 135)
        Me.taskPourcent.Name = "taskPourcent"
        Me.taskPourcent.Size = New System.Drawing.Size(15, 13)
        Me.taskPourcent.TabIndex = 1
        Me.taskPourcent.Text = "%"
        '
        'physiolanaud
        '
        Me.physiolanaud.AutoSize = True
        Me.physiolanaud.Location = New System.Drawing.Point(196, 237)
        Me.physiolanaud.Name = "physiolanaud"
        Me.physiolanaud.Size = New System.Drawing.Size(112, 17)
        Me.physiolanaud.TabIndex = 4
        Me.physiolanaud.Text = "Physio Lanaudière"
        Me.physiolanaud.UseVisualStyleBackColor = True
        '
        'cmlocal
        '
        Me.cmlocal.AutoSize = True
        Me.cmlocal.Location = New System.Drawing.Point(12, 260)
        Me.cmlocal.Name = "cmlocal"
        Me.cmlocal.Size = New System.Drawing.Size(84, 17)
        Me.cmlocal.TabIndex = 4
        Me.cmlocal.Text = "CMSA Local"
        Me.cmlocal.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 298)
        Me.Controls.Add(Me.physiolanaud)
        Me.Controls.Add(Me.leg)
        Me.Controls.Add(Me.cmlocal)
        Me.Controls.Add(Me.local)
        Me.Controls.Add(Me.pat)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.taskPourcent)
        Me.Controls.Add(Me.taskStep)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents taskStep As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents pat As System.Windows.Forms.RadioButton
    Friend WithEvents leg As System.Windows.Forms.RadioButton
    Friend WithEvents local As System.Windows.Forms.RadioButton
    Friend WithEvents taskPourcent As System.Windows.Forms.Label
    Friend WithEvents physiolanaud As System.Windows.Forms.RadioButton
    Friend WithEvents cmlocal As System.Windows.Forms.RadioButton

End Class
