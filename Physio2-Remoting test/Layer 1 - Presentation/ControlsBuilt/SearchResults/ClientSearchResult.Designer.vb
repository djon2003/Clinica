<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClientSearchResult
    Inherits BaseSearchResult

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ajouter = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.noClientFound = New System.Windows.Forms.Label
        Me.viewFolders = New System.Windows.Forms.Button
        Me.selectionner = New System.Windows.Forms.Button
        Me.ClientsTrouves = New CI.Base.Windows.Forms.DataGridPlus
        CType(Me.ClientsTrouves, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ajouter
        '
        Me.ajouter.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ajouter.BackColor = System.Drawing.SystemColors.Control
        Me.ajouter.Cursor = System.Windows.Forms.Cursors.Default
        Me.ajouter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ajouter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ajouter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ajouter.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ajouter.Location = New System.Drawing.Point(424, 19)
        Me.ajouter.Name = "ajouter"
        Me.ajouter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ajouter.Size = New System.Drawing.Size(24, 24)
        Me.ajouter.TabIndex = 30
        Me.ajouter.UseVisualStyleBackColor = False
        '
        'delete
        '
        Me.delete.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.delete.BackColor = System.Drawing.SystemColors.Control
        Me.delete.Cursor = System.Windows.Forms.Cursors.Default
        Me.delete.Enabled = False
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.delete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delete.Location = New System.Drawing.Point(424, 49)
        Me.delete.Name = "delete"
        Me.delete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 31
        Me.delete.UseVisualStyleBackColor = False
        '
        'noClientFound
        '
        Me.noClientFound.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.noClientFound.AutoSize = True
        Me.noClientFound.BackColor = System.Drawing.SystemColors.Control
        Me.noClientFound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.noClientFound.Cursor = System.Windows.Forms.Cursors.Default
        Me.noClientFound.Font = New System.Drawing.Font("Arial", 14.0!)
        Me.noClientFound.ForeColor = System.Drawing.SystemColors.ControlText
        Me.noClientFound.Location = New System.Drawing.Point(88, 63)
        Me.noClientFound.Name = "noClientFound"
        Me.noClientFound.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.noClientFound.Size = New System.Drawing.Size(240, 24)
        Me.noClientFound.TabIndex = 34
        Me.noClientFound.Text = "Aucun compte client trouvé"
        '
        'viewFolders
        '
        Me.viewFolders.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.viewFolders.BackColor = System.Drawing.SystemColors.Control
        Me.viewFolders.Cursor = System.Windows.Forms.Cursors.Default
        Me.viewFolders.Enabled = False
        Me.viewFolders.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.viewFolders.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.viewFolders.ForeColor = System.Drawing.Color.Red
        Me.viewFolders.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.viewFolders.Location = New System.Drawing.Point(424, 79)
        Me.viewFolders.Name = "viewFolders"
        Me.viewFolders.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.viewFolders.Size = New System.Drawing.Size(24, 25)
        Me.viewFolders.TabIndex = 32
        Me.viewFolders.Text = "D"
        Me.viewFolders.UseVisualStyleBackColor = False
        '
        'selectionner
        '
        Me.selectionner.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.selectionner.BackColor = System.Drawing.SystemColors.Control
        Me.selectionner.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectionner.Enabled = False
        Me.selectionner.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectionner.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        Me.selectionner.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectionner.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectionner.Location = New System.Drawing.Point(424, 110)
        Me.selectionner.Name = "selectionner"
        Me.selectionner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectionner.Size = New System.Drawing.Size(24, 25)
        Me.selectionner.TabIndex = 33
        Me.selectionner.UseVisualStyleBackColor = False
        '
        'ClientsTrouves
        '
        Me.ClientsTrouves.AllowUserToAddRows = False
        Me.ClientsTrouves.AllowUserToDeleteRows = False
        Me.ClientsTrouves.AllowUserToResizeRows = False
        Me.ClientsTrouves.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClientsTrouves.autoSelectOnDataSourceChanged = True
        Me.ClientsTrouves.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ClientsTrouves.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.ClientsTrouves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ClientsTrouves.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.ClientsTrouves.Location = New System.Drawing.Point(0, 0)
        Me.ClientsTrouves.MultiSelect = False
        Me.ClientsTrouves.Name = "ClientsTrouves"
        Me.ClientsTrouves.ReadOnly = True
        Me.ClientsTrouves.RowHeadersVisible = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientsTrouves.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.ClientsTrouves.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ClientsTrouves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ClientsTrouves.ShowCellErrors = False
        Me.ClientsTrouves.ShowEditingIcon = False
        Me.ClientsTrouves.Size = New System.Drawing.Size(418, 151)
        Me.ClientsTrouves.StandardTab = True
        Me.ClientsTrouves.TabIndex = 29
        Me.ClientsTrouves.VirtualMode = True
        '
        'ClientSearchResult
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.ajouter)
        Me.Controls.Add(Me.delete)
        Me.Controls.Add(Me.noClientFound)
        Me.Controls.Add(Me.viewFolders)
        Me.Controls.Add(Me.selectionner)
        Me.Controls.Add(Me.ClientsTrouves)
        Me.MinimumSize = New System.Drawing.Size(368, 132)
        Me.Name = "ClientSearchResult"
        Me.Size = New System.Drawing.Size(448, 153)
        CType(Me.ClientsTrouves, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents ajouter As System.Windows.Forms.Button
    Public WithEvents delete As System.Windows.Forms.Button
    Public WithEvents noClientFound As System.Windows.Forms.Label
    Public WithEvents viewFolders As System.Windows.Forms.Button
    Public WithEvents selectionner As System.Windows.Forms.Button
    Friend WithEvents ClientsTrouves As CI.Base.Windows.Forms.DataGridPlus

End Class
