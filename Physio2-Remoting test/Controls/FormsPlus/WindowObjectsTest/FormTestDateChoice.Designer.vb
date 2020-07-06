<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTestDateChoice
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub dispose(ByVal disposing As Boolean)
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
    Private Sub initializeComponent()
        Me.showTime = New System.Windows.Forms.CheckBox
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.Button1 = New System.Windows.Forms.Button
        Me.results = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'showTime
        '
        Me.showTime.AutoSize = True
        Me.showTime.Location = New System.Drawing.Point(97, 2)
        Me.showTime.Name = "showTime"
        Me.showTime.Size = New System.Drawing.Size(75, 17)
        Me.showTime.TabIndex = 0
        Me.showTime.Text = "Show time"
        Me.showTime.UseVisualStyleBackColor = True
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(272, 360)
        Me.PropertyGrid1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(284, 138)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Test"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'results
        '
        Me.results.Location = New System.Drawing.Point(284, 187)
        Me.results.Multiline = True
        Me.results.Name = "results"
        Me.results.Size = New System.Drawing.Size(131, 86)
        Me.results.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(281, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Results"
        '
        'FormTestDateChoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 360)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.results)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.showTime)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Name = "FormTestDateChoice"
        Me.Text = "FormTestDateChoice"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents showTime As System.Windows.Forms.CheckBox
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents results As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
