<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.messages = New System.Windows.Forms.TextBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.pageTasks = New System.Windows.Forms.TabPage
        Me.ctlTasksManager = New CI.Base.Windows.Forms.TasksManager
        Me.pageMessages = New System.Windows.Forms.TabPage
        Me.pagePlugins = New System.Windows.Forms.TabPage
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.EnvoyerUnMessage¿TousToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Mettre¿JourToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConfigurationsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.clientsConnected = New System.Windows.Forms.ToolStripLabel
        Me.lblclientsConnected = New System.Windows.Forms.ToolStripLabel
        Me.TabControl1.SuspendLayout()
        Me.pageTasks.SuspendLayout()
        Me.pageMessages.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'messages
        '
        Me.messages.BackColor = System.Drawing.Color.White
        Me.messages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.messages.Location = New System.Drawing.Point(3, 3)
        Me.messages.Multiline = True
        Me.messages.Name = "messages"
        Me.messages.ReadOnly = True
        Me.messages.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.messages.Size = New System.Drawing.Size(608, 303)
        Me.messages.TabIndex = 0
        Me.messages.WordWrap = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.pageTasks)
        Me.TabControl1.Controls.Add(Me.pageMessages)
        Me.TabControl1.Controls.Add(Me.pagePlugins)
        Me.TabControl1.Location = New System.Drawing.Point(1, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(622, 335)
        Me.TabControl1.TabIndex = 4
        '
        'pageTasks
        '
        Me.pageTasks.Controls.Add(Me.ctlTasksManager)
        Me.pageTasks.Location = New System.Drawing.Point(4, 22)
        Me.pageTasks.Name = "pageTasks"
        Me.pageTasks.Size = New System.Drawing.Size(614, 309)
        Me.pageTasks.TabIndex = 2
        Me.pageTasks.Text = "T‚ches"
        Me.pageTasks.UseVisualStyleBackColor = True
        '
        'ctlTasksManager
        '
        Me.ctlTasksManager.autoLoad = True
        Me.ctlTasksManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ctlTasksManager.Location = New System.Drawing.Point(0, 0)
        Me.ctlTasksManager.MinimumSize = New System.Drawing.Size(323, 141)
        Me.ctlTasksManager.Name = "ctlTasksManager"
        Me.ctlTasksManager.Size = New System.Drawing.Size(614, 309)
        Me.ctlTasksManager.splitterPosition = 210
        Me.ctlTasksManager.TabIndex = 0
        '
        'pageMessages
        '
        Me.pageMessages.Controls.Add(Me.messages)
        Me.pageMessages.Location = New System.Drawing.Point(4, 22)
        Me.pageMessages.Name = "pageMessages"
        Me.pageMessages.Padding = New System.Windows.Forms.Padding(3)
        Me.pageMessages.Size = New System.Drawing.Size(614, 309)
        Me.pageMessages.TabIndex = 0
        Me.pageMessages.Text = "Messages ÈchangÈs"
        Me.pageMessages.UseVisualStyleBackColor = True
        '
        'pagePlugins
        '
        Me.pagePlugins.Location = New System.Drawing.Point(4, 22)
        Me.pagePlugins.Name = "pagePlugins"
        Me.pagePlugins.Padding = New System.Windows.Forms.Padding(3)
        Me.pagePlugins.Size = New System.Drawing.Size(614, 309)
        Me.pagePlugins.TabIndex = 1
        Me.pagePlugins.Text = "Greffons"
        Me.pagePlugins.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.clientsConnected, Me.lblclientsConnected})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(624, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnvoyerUnMessage¿TousToolStripMenuItem, Me.Mettre¿JourToolStripMenuItem, Me.ConfigurationsToolStripMenuItem1})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.ShowDropDownArrow = False
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(51, 22)
        Me.ToolStripDropDownButton1.Text = "Gestion"
        '
        'EnvoyerUnMessage¿TousToolStripMenuItem
        '
        Me.EnvoyerUnMessage¿TousToolStripMenuItem.Name = "EnvoyerUnMessage¿TousToolStripMenuItem"
        Me.EnvoyerUnMessage¿TousToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.EnvoyerUnMessage¿TousToolStripMenuItem.Text = "Envoyer un message ‡ tous"
        '
        'Mettre¿JourToolStripMenuItem
        '
        Me.Mettre¿JourToolStripMenuItem.Name = "Mettre¿JourToolStripMenuItem"
        Me.Mettre¿JourToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.Mettre¿JourToolStripMenuItem.Text = "Mettre ‡ jour"
        '
        'ConfigurationsToolStripMenuItem1
        '
        Me.ConfigurationsToolStripMenuItem1.Name = "ConfigurationsToolStripMenuItem1"
        Me.ConfigurationsToolStripMenuItem1.Size = New System.Drawing.Size(217, 22)
        Me.ConfigurationsToolStripMenuItem1.Text = "Ouvrir les configurations"
        '
        'clientsConnected
        '
        Me.clientsConnected.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.clientsConnected.Name = "clientsConnected"
        Me.clientsConnected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.clientsConnected.Size = New System.Drawing.Size(13, 22)
        Me.clientsConnected.Text = "0"
        '
        'lblclientsConnected
        '
        Me.lblclientsConnected.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblclientsConnected.Name = "lblclientsConnected"
        Me.lblclientsConnected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblclientsConnected.Size = New System.Drawing.Size(106, 22)
        Me.lblclientsConnected.Text = "Clients connectÈs :"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 361)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.MinimumSize = New System.Drawing.Size(401, 332)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Serveur Clinica"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.TabControl1.ResumeLayout(False)
        Me.pageTasks.ResumeLayout(False)
        Me.pageMessages.ResumeLayout(False)
        Me.pageMessages.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents messages As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pageMessages As System.Windows.Forms.TabPage
    Friend WithEvents pagePlugins As System.Windows.Forms.TabPage
    Friend WithEvents pageTasks As System.Windows.Forms.TabPage
    Friend WithEvents ctlTasksManager As CI.Base.Windows.Forms.TasksManager
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ConfigurationsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents clientsConnected As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblclientsConnected As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Mettre¿JourToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnvoyerUnMessage¿TousToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
