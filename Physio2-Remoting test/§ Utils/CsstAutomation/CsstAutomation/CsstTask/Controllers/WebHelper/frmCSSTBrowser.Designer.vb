<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSSTBrowser
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            If disposing AndAlso CsstBrowser1 IsNot Nothing Then
                CsstBrowser1.Dispose()
                CsstBrowser1 = Nothing
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
        Me.CsstBrowser1 = New CI.CsstAutomation.CSSTBrowser
        Me.SuspendLayout()
        '
        'CsstBrowser1
        '
        Me.CsstBrowser1.activateLinksOnEdit = True
        Me.CsstBrowser1.allowContextMenu = True
        Me.CsstBrowser1.allowEditorContextMenu = True
        Me.CsstBrowser1.allowNavigation = False
        Me.CsstBrowser1.allowRefresh = True
        Me.CsstBrowser1.allowUndo = True
        Me.CsstBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CsstBrowser1.editorContextMenu = Nothing
        Me.CsstBrowser1.editorHeight = 350
        Me.CsstBrowser1.editorURL = ""
        Me.CsstBrowser1.editorWidth = 460
        Me.CsstBrowser1.htmlPageURL = Nothing
        Me.CsstBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.CsstBrowser1.Name = "CsstBrowser1"
        Me.CsstBrowser1.Size = New System.Drawing.Size(292, 266)
        Me.CsstBrowser1.startupPos = 0
        Me.CsstBrowser1.TabIndex = 0
        Me.CsstBrowser1.toolBarStyles = 1
        '
        'frmCSSTBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.CsstBrowser1)
        Me.Name = "frmCSSTBrowser"
        Me.Text = "frmCSSTBrowser"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CsstBrowser1 As CI.CsstAutomation.CSSTBrowser

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
