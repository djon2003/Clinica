<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintingForm
    Inherits System.Windows.Forms.Form

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
        Me.WebControl1 = New CI.Base.Windows.Forms.WebControl
        Me.SuspendLayout()
        '
        'WebControl1
        '
        Me.WebControl1.activateLinksOnEdit = True
        Me.WebControl1.allowContextMenu = True
        Me.WebControl1.allowEditorContextMenu = True
        Me.WebControl1.allowNavigation = False
        Me.WebControl1.allowPopupWindows = True
        Me.WebControl1.allowRefresh = True
        Me.WebControl1.allowUndo = True
        Me.WebControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebControl1.editorContextMenu = Nothing
        Me.WebControl1.editorHeight = 350
        Me.WebControl1.editorURL = ""
        Me.WebControl1.editorWidth = 460
        Me.WebControl1.htmlPageURL = Nothing
        Me.WebControl1.Location = New System.Drawing.Point(0, 0)
        Me.WebControl1.Name = "WebControl1"
        Me.WebControl1.Silent = False
        Me.WebControl1.Size = New System.Drawing.Size(284, 261)
        Me.WebControl1.startupPos = 0
        Me.WebControl1.TabIndex = 0
        Me.WebControl1.toolBarStyles = 1
        Me.WebControl1.viewDisableHtmlFields = False
        '
        'PrintingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.WebControl1)
        Me.Name = "PrintingForm"
        Me.Text = "PrintingForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WebControl1 As CI.Base.Windows.Forms.WebControl
End Class
