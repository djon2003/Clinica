<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.List1 = New CI.Controls.List
        Me.nbLinesToAdd = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.addLines = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.actionMessage = New System.Windows.Forms.Label
        CType(Me.nbLinesToAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'List1
        '
        Me.List1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.List1.autoAdjust = True
        Me.List1.autoKeyDownSelection = True
        Me.List1.autoSizeHorizontally = False
        Me.List1.autoSizeVertically = False
        Me.List1.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.List1.baseBackColor = System.Drawing.Color.White
        Me.List1.baseFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.List1.baseForeColor = System.Drawing.Color.Black
        Me.List1.bgColor = System.Drawing.SystemColors.Control
        Me.List1.borderColor = System.Drawing.Color.Blue
        Me.List1.borderSelColor = System.Drawing.Color.Black
        Me.List1.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.List1.CausesValidation = False
        Me.List1.clickEnabled = False
        Me.List1.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.List1.do3D = False
        Me.List1.draw = True
        Me.List1.extraWidth = 0
        Me.List1.hScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.List1.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.List1.hScrolling = False
        Me.List1.hsValue = 0
        Me.List1.icons = CType(resources.GetObject("List1.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.List1.itemBorder = 1
        Me.List1.itemMargin = 2
        Me.List1.items = CType(resources.GetObject("List1.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.List1.Location = New System.Drawing.Point(12, 12)
        Me.List1.mouseMove3D = False
        Me.List1.mouseSpeed = 0
        Me.List1.Name = "List1"
        Me.List1.objMaxHeight = 0.0!
        Me.List1.objMaxWidth = 0.0!
        Me.List1.objMinHeight = 0.0!
        Me.List1.objMinWidth = 0.0!
        Me.List1.reverseSorting = False
        Me.List1.selected = -1
        Me.List1.selectedClickAllowed = False
        Me.List1.selectMultiple = False
        Me.List1.Size = New System.Drawing.Size(189, 237)
        Me.List1.sorted = False
        Me.List1.TabIndex = 0
        Me.List1.toolTipText = Nothing
        Me.List1.vScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.List1.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.List1.vScrolling = True
        Me.List1.vsValue = 0
        '
        'nbLinesToAdd
        '
        Me.nbLinesToAdd.Location = New System.Drawing.Point(3, 20)
        Me.nbLinesToAdd.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.nbLinesToAdd.Name = "nbLinesToAdd"
        Me.nbLinesToAdd.Size = New System.Drawing.Size(115, 20)
        Me.nbLinesToAdd.TabIndex = 1
        Me.nbLinesToAdd.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Add lines"
        '
        'addLines
        '
        Me.addLines.Location = New System.Drawing.Point(2, 46)
        Me.addLines.Name = "addLines"
        Me.addLines.Size = New System.Drawing.Size(117, 23)
        Me.addLines.TabIndex = 3
        Me.addLines.Text = "Add lines"
        Me.addLines.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.addLines)
        Me.Panel1.Controls.Add(Me.nbLinesToAdd)
        Me.Panel1.Location = New System.Drawing.Point(207, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(121, 237)
        Me.Panel1.TabIndex = 4
        '
        'actionMessage
        '
        Me.actionMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.actionMessage.Location = New System.Drawing.Point(12, 252)
        Me.actionMessage.Name = "actionMessage"
        Me.actionMessage.Size = New System.Drawing.Size(313, 23)
        Me.actionMessage.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 274)
        Me.Controls.Add(Me.actionMessage)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.List1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.nbLinesToAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents List1 As CI.Controls.List
    Friend WithEvents nbLinesToAdd As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents addLines As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents actionMessage As System.Windows.Forms.Label

End Class
