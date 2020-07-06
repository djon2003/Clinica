Namespace Windows.Forms


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ConfigurationsManager
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
            Me.TabConfigs = New System.Windows.Forms.TabControl
            Me.TabConfig1 = New CI.Base.Windows.Forms.ConfigurationsPage
            Me.btnSave = New System.Windows.Forms.Button
            Me.TabConfigs.SuspendLayout()
            Me.SuspendLayout()
            '
            'TabConfigs
            '
            Me.TabConfigs.Controls.Add(Me.TabConfig1)
            Me.TabConfigs.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TabConfigs.Location = New System.Drawing.Point(0, 0)
            Me.TabConfigs.Name = "TabConfigs"
            Me.TabConfigs.SelectedIndex = 0
            Me.TabConfigs.Size = New System.Drawing.Size(364, 387)
            Me.TabConfigs.TabIndex = 1
            '
            'TabConfig1
            '
            Me.TabConfig1.Location = New System.Drawing.Point(4, 22)
            Me.TabConfig1.Name = "TabConfig1"
            Me.TabConfig1.Size = New System.Drawing.Size(356, 361)
            Me.TabConfig1.TabIndex = 0
            Me.TabConfig1.Text = "TabConfig1"
            Me.TabConfig1.UseVisualStyleBackColor = True
            '
            'btnSave
            '
            Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnSave.Location = New System.Drawing.Point(288, 22)
            Me.btnSave.Name = "btnSave"
            Me.btnSave.Size = New System.Drawing.Size(70, 24)
            Me.btnSave.TabIndex = 2
            Me.btnSave.Text = "Enregistrer"
            Me.btnSave.UseVisualStyleBackColor = True
            '
            'ConfigurationsManager
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(364, 387)
            Me.Controls.Add(Me.btnSave)
            Me.Controls.Add(Me.TabConfigs)
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(299, 337)
            Me.Name = "ConfigurationsManager"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Gestion des configurations"
            Me.TopMost = True
            Me.TabConfigs.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TabConfigs As System.Windows.Forms.TabControl
        Friend WithEvents TabConfig1 As CI.Base.Windows.Forms.ConfigurationsPage
        Friend WithEvents btnSave As System.Windows.Forms.Button
    End Class


End Namespace
