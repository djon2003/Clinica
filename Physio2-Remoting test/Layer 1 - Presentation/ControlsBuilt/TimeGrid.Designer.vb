<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimeGrid
    Inherits System.Windows.Forms.UserControl

    'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
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
    Private Sub InitializeComponent()
        Dim dataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim dataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.label10 = New System.Windows.Forms.Label
        Me.hoursDimanche = New System.Windows.Forms.Label
        Me.hoursSamedi = New System.Windows.Forms.Label
        Me.hoursVendredi = New System.Windows.Forms.Label
        Me.hoursJeudi = New System.Windows.Forms.Label
        Me.hoursMercredi = New System.Windows.Forms.Label
        Me.hoursMardi = New System.Windows.Forms.Label
        Me.hoursLundi = New System.Windows.Forms.Label
        Me.grdTimeGrid = New DataGridPlus
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDimanche = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLundi = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMardi = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colMercredi = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colJeudi = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colVendredi = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSamedi = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdTimeGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label10
        '
        Me.label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label10.AutoSize = True
        Me.label10.BackColor = System.Drawing.SystemColors.Control
        Me.label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label10.Location = New System.Drawing.Point(3, 111)
        Me.label10.Name = "Label10"
        Me.label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label10.Size = New System.Drawing.Size(84, 14)
        Me.label10.TabIndex = 301
        Me.label10.Text = "Heures / jour :"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursDimanche
        '
        Me.hoursDimanche.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.hoursDimanche.BackColor = System.Drawing.Color.Transparent
        Me.hoursDimanche.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursDimanche.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursDimanche.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursDimanche.Location = New System.Drawing.Point(66, 112)
        Me.hoursDimanche.Name = "hoursDimanche"
        Me.hoursDimanche.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursDimanche.Size = New System.Drawing.Size(56, 14)
        Me.hoursDimanche.TabIndex = 300
        Me.hoursDimanche.Text = "0 h"
        Me.hoursDimanche.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursSamedi
        '
        Me.hoursSamedi.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.hoursSamedi.BackColor = System.Drawing.SystemColors.Control
        Me.hoursSamedi.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursSamedi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursSamedi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursSamedi.Location = New System.Drawing.Point(536, 112)
        Me.hoursSamedi.Name = "hoursSamedi"
        Me.hoursSamedi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursSamedi.Size = New System.Drawing.Size(62, 14)
        Me.hoursSamedi.TabIndex = 303
        Me.hoursSamedi.Text = "0 h"
        Me.hoursSamedi.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursVendredi
        '
        Me.hoursVendredi.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.hoursVendredi.BackColor = System.Drawing.SystemColors.Control
        Me.hoursVendredi.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursVendredi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursVendredi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursVendredi.Location = New System.Drawing.Point(455, 112)
        Me.hoursVendredi.Name = "hoursVendredi"
        Me.hoursVendredi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursVendredi.Size = New System.Drawing.Size(67, 14)
        Me.hoursVendredi.TabIndex = 302
        Me.hoursVendredi.Text = "0 h"
        Me.hoursVendredi.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursJeudi
        '
        Me.hoursJeudi.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.hoursJeudi.BackColor = System.Drawing.SystemColors.Control
        Me.hoursJeudi.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursJeudi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursJeudi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursJeudi.Location = New System.Drawing.Point(375, 112)
        Me.hoursJeudi.Name = "hoursJeudi"
        Me.hoursJeudi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursJeudi.Size = New System.Drawing.Size(67, 14)
        Me.hoursJeudi.TabIndex = 297
        Me.hoursJeudi.Text = "0 h"
        Me.hoursJeudi.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursMercredi
        '
        Me.hoursMercredi.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.hoursMercredi.BackColor = System.Drawing.SystemColors.Control
        Me.hoursMercredi.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursMercredi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursMercredi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursMercredi.Location = New System.Drawing.Point(295, 112)
        Me.hoursMercredi.Name = "hoursMercredi"
        Me.hoursMercredi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursMercredi.Size = New System.Drawing.Size(67, 14)
        Me.hoursMercredi.TabIndex = 296
        Me.hoursMercredi.Text = "0 h"
        Me.hoursMercredi.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursMardi
        '
        Me.hoursMardi.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.hoursMardi.BackColor = System.Drawing.SystemColors.Control
        Me.hoursMardi.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursMardi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursMardi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursMardi.Location = New System.Drawing.Point(215, 112)
        Me.hoursMardi.Name = "hoursMardi"
        Me.hoursMardi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursMardi.Size = New System.Drawing.Size(67, 14)
        Me.hoursMardi.TabIndex = 299
        Me.hoursMardi.Text = "0 h"
        Me.hoursMardi.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursLundi
        '
        Me.hoursLundi.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.hoursLundi.BackColor = System.Drawing.SystemColors.Control
        Me.hoursLundi.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursLundi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursLundi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursLundi.Location = New System.Drawing.Point(135, 112)
        Me.hoursLundi.Name = "hoursLundi"
        Me.hoursLundi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursLundi.Size = New System.Drawing.Size(67, 14)
        Me.hoursLundi.TabIndex = 298
        Me.hoursLundi.Text = "0 h"
        Me.hoursLundi.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdTimeGrid
        '
        Me.grdTimeGrid.isDoubleBuffered = True
        Me.grdTimeGrid.AllowUserToAddRows = False
        Me.grdTimeGrid.AllowUserToDeleteRows = False
        Me.grdTimeGrid.AllowUserToResizeColumns = False
        Me.grdTimeGrid.AllowUserToResizeRows = False
        Me.grdTimeGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTimeGrid.autoSelectOnDataSourceChanged = False
        Me.grdTimeGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.grdTimeGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        dataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTimeGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
        Me.grdTimeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTimeGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTime, Me.colDimanche, Me.colLundi, Me.colMardi, Me.colMercredi, Me.colJeudi, Me.colVendredi, Me.colSamedi})
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        dataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdTimeGrid.DefaultCellStyle = dataGridViewCellStyle2
        Me.grdTimeGrid.Location = New System.Drawing.Point(0, 0)
        Me.grdTimeGrid.Name = "grdTimeGrid"
        Me.grdTimeGrid.ReadOnly = True
        Me.grdTimeGrid.RowHeadersVisible = False
        Me.grdTimeGrid.Size = New System.Drawing.Size(624, 108)
        Me.grdTimeGrid.TabIndex = 0
        '
        'colTime
        '
        Me.colTime.DataPropertyName = "colTime"
        Me.colTime.Frozen = True
        Me.colTime.HeaderText = ""
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = True
        Me.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colTime.Width = 40
        '
        'colDimanche
        '
        Me.colDimanche.DataPropertyName = "colDimanche"
        Me.colDimanche.HeaderText = "Dimanche"
        Me.colDimanche.MinimumWidth = 80
        Me.colDimanche.Name = "colDimanche"
        Me.colDimanche.ReadOnly = True
        Me.colDimanche.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDimanche.Width = 80
        '
        'colLundi
        '
        Me.colLundi.DataPropertyName = "colLundi"
        Me.colLundi.HeaderText = "Lundi"
        Me.colLundi.Name = "colLundi"
        Me.colLundi.ReadOnly = True
        Me.colLundi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colLundi.Width = 39
        '
        'colMardi
        '
        Me.colMardi.DataPropertyName = "colMardi"
        Me.colMardi.HeaderText = "Mardi"
        Me.colMardi.Name = "colMardi"
        Me.colMardi.ReadOnly = True
        Me.colMardi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMardi.Width = 39
        '
        'colMercredi
        '
        Me.colMercredi.DataPropertyName = "colMercredi"
        Me.colMercredi.HeaderText = "Mercredi"
        Me.colMercredi.Name = "colMercredi"
        Me.colMercredi.ReadOnly = True
        Me.colMercredi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMercredi.Width = 54
        '
        'colJeudi
        '
        Me.colJeudi.DataPropertyName = "colJeudi"
        Me.colJeudi.HeaderText = "Jeudi"
        Me.colJeudi.Name = "colJeudi"
        Me.colJeudi.ReadOnly = True
        Me.colJeudi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colJeudi.Width = 38
        '
        'colVendredi
        '
        Me.colVendredi.DataPropertyName = "colVendredi"
        Me.colVendredi.HeaderText = "Vendredi"
        Me.colVendredi.Name = "colVendredi"
        Me.colVendredi.ReadOnly = True
        Me.colVendredi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colVendredi.Width = 55
        '
        'colSamedi
        '
        Me.colSamedi.DataPropertyName = "colSamedi"
        Me.colSamedi.HeaderText = "Samedi"
        Me.colSamedi.Name = "colSamedi"
        Me.colSamedi.ReadOnly = True
        Me.colSamedi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSamedi.Width = 48
        '
        'TimeGrid
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.label10)
        Me.Controls.Add(Me.hoursDimanche)
        Me.Controls.Add(Me.hoursSamedi)
        Me.Controls.Add(Me.hoursVendredi)
        Me.Controls.Add(Me.hoursJeudi)
        Me.Controls.Add(Me.hoursMercredi)
        Me.Controls.Add(Me.hoursMardi)
        Me.Controls.Add(Me.hoursLundi)
        Me.Controls.Add(Me.grdTimeGrid)
        Me.MinimumSize = New System.Drawing.Size(624, 125)
        Me.Name = "TimeGrid"
        Me.Size = New System.Drawing.Size(624, 125)
        CType(Me.grdTimeGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents grdTimeGrid As DataGridPlus
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents hoursDimanche As System.Windows.Forms.Label
    Private WithEvents hoursSamedi As System.Windows.Forms.Label
    Private WithEvents hoursVendredi As System.Windows.Forms.Label
    Private WithEvents hoursJeudi As System.Windows.Forms.Label
    Private WithEvents hoursMercredi As System.Windows.Forms.Label
    Private WithEvents hoursMardi As System.Windows.Forms.Label
    Private WithEvents hoursLundi As System.Windows.Forms.Label
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDimanche As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLundi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMardi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMercredi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colJeudi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVendredi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSamedi As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
