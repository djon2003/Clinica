<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Button1 = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Client = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.erreurs = New System.Windows.Forms.TextBox
        Me.medecins = New System.Windows.Forms.CheckBox
        Me.Clinique = New System.Windows.Forms.CheckBox
        Me.Users = New System.Windows.Forms.CheckBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Button2 = New System.Windows.Forms.Button
        Me.RVs = New System.Windows.Forms.CheckBox
        Me.scripting = New System.Windows.Forms.CheckBox
        Me.mapfromfile = New System.Windows.Forms.CheckBox
        Me.updateTRPNoPermis = New System.Windows.Forms.CheckBox
        Me.allParts = New System.Windows.Forms.CheckBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Extract"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(12, 41)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(618, 182)
        Me.DataGridView1.TabIndex = 1
        '
        'Client
        '
        Me.Client.AutoSize = True
        Me.Client.Location = New System.Drawing.Point(383, 6)
        Me.Client.Name = "Client"
        Me.Client.Size = New System.Drawing.Size(57, 17)
        Me.Client.TabIndex = 2
        Me.Client.Text = "Clients"
        Me.Client.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 226)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Sortie :"
        '
        'erreurs
        '
        Me.erreurs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.erreurs.Location = New System.Drawing.Point(12, 242)
        Me.erreurs.Multiline = True
        Me.erreurs.Name = "erreurs"
        Me.erreurs.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.erreurs.Size = New System.Drawing.Size(618, 71)
        Me.erreurs.TabIndex = 4
        '
        'medecins
        '
        Me.medecins.AutoSize = True
        Me.medecins.Location = New System.Drawing.Point(293, 6)
        Me.medecins.Name = "medecins"
        Me.medecins.Size = New System.Drawing.Size(72, 17)
        Me.medecins.TabIndex = 2
        Me.medecins.Text = "Médecins"
        Me.medecins.UseVisualStyleBackColor = True
        '
        'Clinique
        '
        Me.Clinique.AutoSize = True
        Me.Clinique.Location = New System.Drawing.Point(160, 6)
        Me.Clinique.Name = "Clinique"
        Me.Clinique.Size = New System.Drawing.Size(63, 17)
        Me.Clinique.TabIndex = 2
        Me.Clinique.Text = "Clinique"
        Me.Clinique.UseVisualStyleBackColor = True
        '
        'Users
        '
        Me.Users.AutoSize = True
        Me.Users.Location = New System.Drawing.Point(220, 6)
        Me.Users.Name = "Users"
        Me.Users.Size = New System.Drawing.Size(77, 17)
        Me.Users.TabIndex = 2
        Me.Users.Text = "Utilisateurs"
        Me.Users.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "Fichiers Access|*.mdb"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(573, -2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(57, 31)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Quit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RVs
        '
        Me.RVs.AutoSize = True
        Me.RVs.Location = New System.Drawing.Point(466, 6)
        Me.RVs.Name = "RVs"
        Me.RVs.Size = New System.Drawing.Size(46, 17)
        Me.RVs.TabIndex = 2
        Me.RVs.Text = "RVs"
        Me.RVs.UseVisualStyleBackColor = True
        '
        'scripting
        '
        Me.scripting.AutoSize = True
        Me.scripting.Location = New System.Drawing.Point(466, 22)
        Me.scripting.Name = "scripting"
        Me.scripting.Size = New System.Drawing.Size(105, 17)
        Me.scripting.TabIndex = 2
        Me.scripting.Text = "Scripting method"
        Me.scripting.UseVisualStyleBackColor = True
        '
        'mapfromfile
        '
        Me.mapfromfile.AutoSize = True
        Me.mapfromfile.Location = New System.Drawing.Point(383, 22)
        Me.mapfromfile.Name = "mapfromfile"
        Me.mapfromfile.Size = New System.Drawing.Size(86, 17)
        Me.mapfromfile.TabIndex = 2
        Me.mapfromfile.Text = "Map from file"
        Me.mapfromfile.UseVisualStyleBackColor = True
        '
        'updateTRPNoPermis
        '
        Me.updateTRPNoPermis.AutoSize = True
        Me.updateTRPNoPermis.Location = New System.Drawing.Point(220, 22)
        Me.updateTRPNoPermis.Name = "updateTRPNoPermis"
        Me.updateTRPNoPermis.Size = New System.Drawing.Size(143, 17)
        Me.updateTRPNoPermis.TabIndex = 2
        Me.updateTRPNoPermis.Text = "Update T.R.P. NoPermis"
        Me.updateTRPNoPermis.UseVisualStyleBackColor = True
        '
        'allParts
        '
        Me.allParts.AutoSize = True
        Me.allParts.Location = New System.Drawing.Point(91, 6)
        Me.allParts.Name = "allParts"
        Me.allParts.Size = New System.Drawing.Size(37, 17)
        Me.allParts.TabIndex = 2
        Me.allParts.Text = "All"
        Me.allParts.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 325)
        Me.Controls.Add(Me.scripting)
        Me.Controls.Add(Me.RVs)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.erreurs)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.medecins)
        Me.Controls.Add(Me.Users)
        Me.Controls.Add(Me.allParts)
        Me.Controls.Add(Me.Clinique)
        Me.Controls.Add(Me.updateTRPNoPermis)
        Me.Controls.Add(Me.mapfromfile)
        Me.Controls.Add(Me.Client)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Extraction de Physio vers Clinica"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Client As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents erreurs As System.Windows.Forms.TextBox
    Friend WithEvents medecins As System.Windows.Forms.CheckBox
    Friend WithEvents Clinique As System.Windows.Forms.CheckBox
    Friend WithEvents Users As System.Windows.Forms.CheckBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents RVs As System.Windows.Forms.CheckBox
    Friend WithEvents scripting As System.Windows.Forms.CheckBox
    Friend WithEvents mapfromfile As System.Windows.Forms.CheckBox
    Friend WithEvents updateTRPNoPermis As System.Windows.Forms.CheckBox
    Friend WithEvents allParts As System.Windows.Forms.CheckBox

End Class
