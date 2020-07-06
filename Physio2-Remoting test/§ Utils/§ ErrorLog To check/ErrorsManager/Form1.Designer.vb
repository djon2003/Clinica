<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FichierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImporterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TousToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.EmailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FichierLogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ErreursCourantesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EnvoyerParCourrielToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.propertiesOfErrors = New System.Windows.Forms.PropertyGrid
        Me.ÉcrireEnFichiersEMLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 27)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(671, 329)
        Me.DataGridView1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FichierToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(695, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FichierToolStripMenuItem
        '
        Me.FichierToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImporterToolStripMenuItem, Me.ErreursCourantesToolStripMenuItem})
        Me.FichierToolStripMenuItem.Name = "FichierToolStripMenuItem"
        Me.FichierToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.FichierToolStripMenuItem.Text = "Fichier"
        '
        'ImporterToolStripMenuItem
        '
        Me.ImporterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TousToolStripMenuItem, Me.ToolStripMenuItem1, Me.EmailToolStripMenuItem, Me.FichierLogsToolStripMenuItem})
        Me.ImporterToolStripMenuItem.Name = "ImporterToolStripMenuItem"
        Me.ImporterToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ImporterToolStripMenuItem.Text = "Importer..."
        '
        'TousToolStripMenuItem
        '
        Me.TousToolStripMenuItem.Name = "TousToolStripMenuItem"
        Me.TousToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.TousToolStripMenuItem.Text = "Tous"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(149, 6)
        '
        'EmailToolStripMenuItem
        '
        Me.EmailToolStripMenuItem.Name = "EmailToolStripMenuItem"
        Me.EmailToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EmailToolStripMenuItem.Text = "E-mail"
        '
        'FichierLogsToolStripMenuItem
        '
        Me.FichierLogsToolStripMenuItem.Name = "FichierLogsToolStripMenuItem"
        Me.FichierLogsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.FichierLogsToolStripMenuItem.Text = "Fichier logs"
        '
        'ErreursCourantesToolStripMenuItem
        '
        Me.ErreursCourantesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnvoyerParCourrielToolStripMenuItem, Me.ÉcrireEnFichiersEMLToolStripMenuItem})
        Me.ErreursCourantesToolStripMenuItem.Name = "ErreursCourantesToolStripMenuItem"
        Me.ErreursCourantesToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ErreursCourantesToolStripMenuItem.Text = "Erreurs courantes"
        '
        'EnvoyerParCourrielToolStripMenuItem
        '
        Me.EnvoyerParCourrielToolStripMenuItem.Name = "EnvoyerParCourrielToolStripMenuItem"
        Me.EnvoyerParCourrielToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.EnvoyerParCourrielToolStripMenuItem.Text = "Envoyer par courriel"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "Tous les fichiers|*.log;*.eml|Fichiers log|*.log|Fichiers eml|*.eml"
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.SelectedPath = "C:\Physio2-Remoting test\§ErrorLog To check"
        '
        'propertiesOfErrors
        '
        Me.propertiesOfErrors.Location = New System.Drawing.Point(424, 27)
        Me.propertiesOfErrors.Name = "propertiesOfErrors"
        Me.propertiesOfErrors.Size = New System.Drawing.Size(259, 341)
        Me.propertiesOfErrors.TabIndex = 2
        '
        'ÉcrireEnFichiersEMLToolStripMenuItem
        '
        Me.ÉcrireEnFichiersEMLToolStripMenuItem.Name = "ÉcrireEnFichiersEMLToolStripMenuItem"
        Me.ÉcrireEnFichiersEMLToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ÉcrireEnFichiersEMLToolStripMenuItem.Text = "Écrire en fichiers EML"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 368)
        Me.Controls.Add(Me.propertiesOfErrors)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FichierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImporterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmailToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FichierLogsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents TousToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents propertiesOfErrors As System.Windows.Forms.PropertyGrid
    Friend WithEvents ErreursCourantesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnvoyerParCourrielToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ÉcrireEnFichiersEMLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
