Namespace Windows.Forms


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ConfigurationsPage
        'UserControl kept to modify easily the control view
        'Inherits System.Windows.Forms.UserControl
        Inherits System.Windows.Forms.TabPage

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
            Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
            Me.SuspendLayout()
            '
            'PropertyGrid1
            '
            Me.PropertyGrid1.Dock = DockStyle.Fill
            Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
            Me.PropertyGrid1.Name = "PropertyGrid1"
            Me.PropertyGrid1.Size = New System.Drawing.Size(267, 318)
            Me.PropertyGrid1.TabIndex = 0
            '
            'ConfigurationsManager
            '
            Me.Controls.Add(Me.PropertyGrid1)
            Me.Name = "ConfigurationsManager"
            Me.Size = New System.Drawing.Size(270, 318)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid

    End Class


End Namespace