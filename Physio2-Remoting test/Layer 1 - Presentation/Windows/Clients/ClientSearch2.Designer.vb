<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class ClientSearch2
    Inherits BaseSearch

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

    '


    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.myClientSearchResult = New ClientSearchResult
        Me.SuspendLayout()
        '
        'searchResultsTitle
        '
        Me.searchResultsTitle.Size = New System.Drawing.Size(196, 19)
        Me.searchResultsTitle.Text = "Compte(s) client(s) trouvé(s)"
        '
        'myClientSearchResult
        '
        Me.myClientSearchResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.myClientSearchResult.Location = New System.Drawing.Point(0, 17)
        Me.myClientSearchResult.MinimumSize = New System.Drawing.Size(368, 132)
        Me.myClientSearchResult.Name = "myClientSearchResult"
        Me.myClientSearchResult.Size = New System.Drawing.Size(459, 164)
        Me.myClientSearchResult.TabIndex = 0
        '
        'ClientSearch2
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(491, 383)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ClientSearch2"
        Me.Text = "Recherche d'un compte client"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents myClientSearchResult As ClientSearchResult
End Class
