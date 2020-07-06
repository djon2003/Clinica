<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportGeneration
    Inherits SingleWindow

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing AndAlso MyReport IsNot Nothing Then
            RemoveHandler MyReport.HTMLGenerated, AddressOf RapportGenerated
        End If
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Generate = New System.Windows.Forms.Button
        Me.Save = New System.Windows.Forms.Button
        Me.menuSaving = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DansLaBanqueDeDonnéesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DansLesCommunicationsDunClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AlertUser = New System.Windows.Forms.CheckBox
        Me.print = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RapportDisplay = New WebTextControl
        Me.menuSaving.SuspendLayout()
        Me.SuspendLayout()
        '
        'Generate
        '
        Me.Generate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Generate.Location = New System.Drawing.Point(12, 378)
        Me.Generate.Name = "Generate"
        Me.Generate.Size = New System.Drawing.Size(63, 24)
        Me.Generate.TabIndex = 1
        Me.Generate.Text = "Générer"
        Me.Generate.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save.ContextMenuStrip = Me.menuSaving
        Me.Save.Enabled = False
        Me.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Save.Location = New System.Drawing.Point(551, 378)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(24, 24)
        Me.Save.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.Save, "Enregistrer le rapport en cours dans ...")
        Me.Save.UseVisualStyleBackColor = True
        '
        'menuSaving
        '
        Me.menuSaving.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DansLaBanqueDeDonnéesToolStripMenuItem, Me.DansLesCommunicationsDunClientToolStripMenuItem, Me.DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem})
        Me.menuSaving.Name = "menuSaving"
        Me.menuSaving.Size = New System.Drawing.Size(378, 70)
        '
        'DansLaBanqueDeDonnéesToolStripMenuItem
        '
        Me.DansLaBanqueDeDonnéesToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.DansLaBanqueDeDonnéesToolStripMenuItem.Name = "DansLaBanqueDeDonnéesToolStripMenuItem"
        Me.DansLaBanqueDeDonnéesToolStripMenuItem.Size = New System.Drawing.Size(377, 22)
        Me.DansLaBanqueDeDonnéesToolStripMenuItem.Text = "... dans la banque de données"
        '
        'DansLesCommunicationsDunClientToolStripMenuItem
        '
        Me.DansLesCommunicationsDunClientToolStripMenuItem.Name = "DansLesCommunicationsDunClientToolStripMenuItem"
        Me.DansLesCommunicationsDunClientToolStripMenuItem.Size = New System.Drawing.Size(377, 22)
        Me.DansLesCommunicationsDunClientToolStripMenuItem.Text = "... dans les communications d'un client"
        '
        'DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem
        '
        Me.DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem.Name = "DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem"
        Me.DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem.Size = New System.Drawing.Size(377, 22)
        Me.DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem.Text = "... dans les communications d'un(e) personne / organisme clé"
        '
        'AlertUser
        '
        Me.AlertUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AlertUser.AutoSize = True
        Me.AlertUser.Location = New System.Drawing.Point(81, 383)
        Me.AlertUser.Name = "AlertUser"
        Me.AlertUser.Size = New System.Drawing.Size(278, 17)
        Me.AlertUser.TabIndex = 2
        Me.AlertUser.Text = "M'alerter lorsque la génération du rapport est terminée"
        Me.AlertUser.UseVisualStyleBackColor = True
        '
        'print
        '
        Me.print.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.print.Enabled = False
        Me.print.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.print.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.print.Location = New System.Drawing.Point(519, 378)
        Me.print.Name = "print"
        Me.print.Size = New System.Drawing.Size(24, 24)
        Me.print.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.print, "Imprimer le rapport")
        Me.print.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'RapportDisplay
        '
        Me.RapportDisplay.activateLinksOnEdit = True
        Me.RapportDisplay.allowContextMenu = True
        Me.RapportDisplay.allowEditorContextMenu = True
        Me.RapportDisplay.allowNavigation = False
        Me.RapportDisplay.allowRefresh = False
        Me.RapportDisplay.allowUndo = True
        Me.RapportDisplay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RapportDisplay.ancre = Nothing
        Me.RapportDisplay.ancreActif = False
        Me.RapportDisplay.editorContextMenu = Nothing
        Me.RapportDisplay.editorHeight = 350
        Me.RapportDisplay.editorURL = ""
        Me.RapportDisplay.editorWidth = 460
        Me.RapportDisplay.htmlPageURL = Nothing
        Me.RapportDisplay.Location = New System.Drawing.Point(0, 0)
        Me.RapportDisplay.Name = "RapportDisplay"
        Me.RapportDisplay.Silent = False
        Me.RapportDisplay.Size = New System.Drawing.Size(587, 370)
        Me.RapportDisplay.startupPos = 0
        Me.RapportDisplay.TabIndex = 0
        Me.RapportDisplay.toolBarStyles = 1
        Me.RapportDisplay.viewDisableHtmlFields = True
        '
        'ReportGeneration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 412)
        Me.Controls.Add(Me.AlertUser)
        Me.Controls.Add(Me.print)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.Generate)
        Me.Controls.Add(Me.RapportDisplay)
        Me.MinimumSize = New System.Drawing.Size(472, 431)
        Me.Name = "ReportGeneration"
        Me.Text = "Générateur de rapport"
        Me.menuSaving.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RapportDisplay As Clinica.WebTextControl
    Friend WithEvents Generate As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents AlertUser As System.Windows.Forms.CheckBox
    Friend WithEvents print As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents menuSaving As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DansLaBanqueDeDonnéesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DansLesCommunicationsDunClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DansLesCommunicationsDunePersonneOrganismeCléeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
