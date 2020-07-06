<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EndOfMonth
    Inherits SingleWindow

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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.doFinMois = New System.Windows.Forms.Button
        Me.typesReports = New System.Windows.Forms.CheckedListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.save = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.selectDBPath = New System.Windows.Forms.Button
        Me.DBPath = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'doFinMois
        '
        Me.doFinMois.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.doFinMois.Location = New System.Drawing.Point(12, 264)
        Me.doFinMois.Name = "doFinMois"
        Me.doFinMois.Size = New System.Drawing.Size(139, 23)
        Me.doFinMois.TabIndex = 0
        Me.doFinMois.Text = "Effectuer la fin de mois"
        Me.doFinMois.UseVisualStyleBackColor = True
        '
        'typesRapports
        '
        Me.typesReports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.typesReports.FormattingEnabled = True
        Me.typesReports.Location = New System.Drawing.Point(12, 24)
        Me.typesReports.Name = "typesRapports"
        Me.typesReports.Size = New System.Drawing.Size(332, 199)
        Me.typesReports.Sorted = True
        Me.typesReports.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Types de rapport à générer :"
        '
        'save
        '
        Me.save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.save.Location = New System.Drawing.Point(320, 264)
        Me.save.Name = "save"
        Me.save.Size = New System.Drawing.Size(24, 24)
        Me.save.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.save, "Enregistrer les options de la fin de mois")
        Me.save.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'selectDBPath
        '
        Me.selectDBPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectDBPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectDBPath.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectDBPath.Location = New System.Drawing.Point(12, 229)
        Me.selectDBPath.Name = "selectDBPath"
        Me.selectDBPath.Size = New System.Drawing.Size(24, 24)
        Me.selectDBPath.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.selectDBPath, "Sélectionner le dossier où enregistrer les rapports")
        Me.selectDBPath.UseVisualStyleBackColor = True
        '
        'DBPath
        '
        Me.DBPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DBPath.AutoSize = True
        Me.DBPath.Location = New System.Drawing.Point(42, 235)
        Me.DBPath.Name = "DBPath"
        Me.DBPath.Size = New System.Drawing.Size(132, 13)
        Me.DBPath.TabIndex = 4
        Me.DBPath.Text = "Aucun chemin sélectionné"
        '
        'FinMois
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(356, 299)
        Me.Controls.Add(Me.DBPath)
        Me.Controls.Add(Me.selectDBPath)
        Me.Controls.Add(Me.save)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.typesReports)
        Me.Controls.Add(Me.doFinMois)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(364, 333)
        Me.Name = "FinMois"
        Me.Text = "Fin de mois"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents doFinMois As System.Windows.Forms.Button
    Friend WithEvents typesReports As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents save As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents selectDBPath As System.Windows.Forms.Button
    Friend WithEvents DBPath As System.Windows.Forms.Label

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
