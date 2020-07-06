<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectDBCat
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
        Me.components = New System.ComponentModel.Container
        Me.categories = New Clinica.DirectoryTreeView
        Me.ok = New System.Windows.Forms.Button
        Me.annuler = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'categories
        '
        Me.categories.dragging = False
        Me.categories.expandAllNodes = False
        Me.categories.ImageIndex = 0
        Me.categories.Location = New System.Drawing.Point(12, 12)
        Me.categories.Name = "categories"
        Me.categories.properDrag = False
        Me.categories.SelectedImageIndex = 0
        Me.categories.showHiddenFiles = True
        Me.categories.showMenu = True
        Me.categories.showMenuAdd = True
        Me.categories.showMenuDel = True
        Me.categories.showMenuRename = True
        Me.categories.showMenuSearch = True
        Me.categories.Size = New System.Drawing.Size(382, 320)
        Me.categories.TabIndex = 0
        Me.categories.Sorted = True
        '
        'ok
        '
        Me.ok.Location = New System.Drawing.Point(120, 338)
        Me.ok.Name = "ok"
        Me.ok.Size = New System.Drawing.Size(75, 23)
        Me.ok.TabIndex = 1
        Me.ok.Text = "OK"
        Me.ok.UseVisualStyleBackColor = True
        '
        'annuler
        '
        Me.annuler.Location = New System.Drawing.Point(211, 338)
        Me.annuler.Name = "annuler"
        Me.annuler.Size = New System.Drawing.Size(75, 23)
        Me.annuler.TabIndex = 1
        Me.annuler.Text = "Annuler"
        Me.annuler.UseVisualStyleBackColor = True
        '
        'SelectDBCat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 373)
        Me.Controls.Add(Me.annuler)
        Me.Controls.Add(Me.ok)
        Me.Controls.Add(Me.categories)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectDBCat"
        Me.ShowIcon = False
        Me.Text = "Sélection du dossier"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents categories As Clinica.DirectoryTreeView
    Friend WithEvents ok As System.Windows.Forms.Button
    Friend WithEvents annuler As System.Windows.Forms.Button
End Class
