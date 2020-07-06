<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FolderExportation
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
        Me.components = New System.ComponentModel.Container
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.exportPath = New System.Windows.Forms.TextBox
        Me.removeClientFolder = New System.Windows.Forms.Button
        Me.selectExportPath = New System.Windows.Forms.Button
        Me.selectClientFolder = New System.Windows.Forms.Button
        Me.selectedClientFolder = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.codifications = New System.Windows.Forms.CheckedListBox
        Me.allCodes = New System.Windows.Forms.CheckBox
        Me.therapists = New System.Windows.Forms.CheckedListBox
        Me.allTRP = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExport = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pathDialog = New System.Windows.Forms.FolderBrowserDialog
        Me.output = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.exportPath)
        Me.GroupBox1.Controls.Add(Me.removeClientFolder)
        Me.GroupBox1.Controls.Add(Me.selectExportPath)
        Me.GroupBox1.Controls.Add(Me.selectClientFolder)
        Me.GroupBox1.Controls.Add(Me.selectedClientFolder)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.codifications)
        Me.GroupBox1.Controls.Add(Me.allCodes)
        Me.GroupBox1.Controls.Add(Me.therapists)
        Me.GroupBox1.Controls.Add(Me.allTRP)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(425, 373)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'exportPath
        '
        Me.exportPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.exportPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.exportPath.Location = New System.Drawing.Point(36, 346)
        Me.exportPath.Name = "exportPath"
        Me.exportPath.Size = New System.Drawing.Size(383, 20)
        Me.exportPath.TabIndex = 8
        '
        'removeClientFolder
        '
        Me.removeClientFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.removeClientFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.removeClientFolder.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.removeClientFolder.Location = New System.Drawing.Point(36, 290)
        Me.removeClientFolder.Name = "removeClientFolder"
        Me.removeClientFolder.Size = New System.Drawing.Size(24, 24)
        Me.removeClientFolder.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.removeClientFolder, "Enlever le client (Pour prendre tous les clients)")
        '
        'selectExportPath
        '
        Me.selectExportPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectExportPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectExportPath.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectExportPath.Location = New System.Drawing.Point(6, 343)
        Me.selectExportPath.Name = "selectExportPath"
        Me.selectExportPath.Size = New System.Drawing.Size(24, 24)
        Me.selectExportPath.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.selectExportPath, "Sélectionner le chemin d'exportation")
        '
        'selectClientFolder
        '
        Me.selectClientFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectClientFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectClientFolder.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectClientFolder.Location = New System.Drawing.Point(6, 290)
        Me.selectClientFolder.Name = "selectClientFolder"
        Me.selectClientFolder.Size = New System.Drawing.Size(24, 24)
        Me.selectClientFolder.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.selectClientFolder, "Sélectionner un client ou l'un de ses dossiers")
        '
        'selectedClientFolder
        '
        Me.selectedClientFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectedClientFolder.AutoSize = True
        Me.selectedClientFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectedClientFolder.Location = New System.Drawing.Point(66, 296)
        Me.selectedClientFolder.Name = "selectedClientFolder"
        Me.selectedClientFolder.Size = New System.Drawing.Size(80, 13)
        Me.selectedClientFolder.TabIndex = 3
        Me.selectedClientFolder.Text = "Tous les clients"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 327)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(299, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Chemin sur l'ordinateur où enregistrer l'exportation :"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 274)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Client / dossier :"
        '
        'codifications
        '
        Me.codifications.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.codifications.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codifications.FormattingEnabled = True
        Me.codifications.Location = New System.Drawing.Point(6, 161)
        Me.codifications.Name = "codifications"
        Me.codifications.Size = New System.Drawing.Size(413, 94)
        Me.codifications.TabIndex = 4
        '
        'allCodes
        '
        Me.allCodes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.allCodes.AutoSize = True
        Me.allCodes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.allCodes.Location = New System.Drawing.Point(151, 145)
        Me.allCodes.Name = "allCodes"
        Me.allCodes.Size = New System.Drawing.Size(50, 17)
        Me.allCodes.TabIndex = 3
        Me.allCodes.Text = "Tous"
        Me.allCodes.UseVisualStyleBackColor = True
        '
        'therapists
        '
        Me.therapists.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.therapists.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.therapists.FormattingEnabled = True
        Me.therapists.Location = New System.Drawing.Point(6, 36)
        Me.therapists.Name = "therapists"
        Me.therapists.Size = New System.Drawing.Size(413, 94)
        Me.therapists.TabIndex = 1
        '
        'allTRP
        '
        Me.allTRP.AutoSize = True
        Me.allTRP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.allTRP.Location = New System.Drawing.Point(100, 20)
        Me.allTRP.Name = "allTRP"
        Me.allTRP.Size = New System.Drawing.Size(50, 17)
        Me.allTRP.TabIndex = 0
        Me.allTRP.Text = "Tous"
        Me.allTRP.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(138, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Codifications dossiers :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Thérapeutes :"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(362, 391)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(75, 23)
        Me.btnExport.TabIndex = 9
        Me.btnExport.Text = "Exporter"
        Me.ToolTip1.SetToolTip(Me.btnExport, "Exporter les dossiers client sous-tendant les choix des options")
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'output
        '
        Me.output.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.output.AutoSize = True
        Me.output.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.output.Location = New System.Drawing.Point(9, 396)
        Me.output.Name = "output"
        Me.output.Size = New System.Drawing.Size(0, 13)
        Me.output.TabIndex = 3
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(362, 391)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Annuler"
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        '
        'FolderExportation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 426)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.output)
        Me.MinimumSize = New System.Drawing.Size(465, 465)
        Me.Name = "FolderExportation"
        Me.Text = "Exportation de dossiers client"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents therapists As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents removeClientFolder As System.Windows.Forms.Button
    Friend WithEvents selectClientFolder As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents selectedClientFolder As System.Windows.Forms.Label
    Friend WithEvents exportPath As System.Windows.Forms.TextBox
    Friend WithEvents selectExportPath As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents allTRP As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents codifications As System.Windows.Forms.CheckedListBox
    Friend WithEvents allCodes As System.Windows.Forms.CheckBox
    Friend WithEvents pathDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents output As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
