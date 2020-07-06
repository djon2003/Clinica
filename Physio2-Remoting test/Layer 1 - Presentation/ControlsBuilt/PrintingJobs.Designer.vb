<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintingJobs
    Inherits System.Windows.Forms.UserControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrintingJobs))
        Me.jobsList = New CI.Controls.List
        Me.ControlBox = New TransparentControlBox
        Me.ctlTitle = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'jobsList
        '
        Me.jobsList.autoAdjust = True
        Me.jobsList.autoKeyDownSelection = True
        Me.jobsList.autoSizeHorizontally = False
        Me.jobsList.autoSizeVertically = True
        Me.jobsList.BackColor = System.Drawing.Color.White
        Me.jobsList.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.jobsList.baseBackColor = System.Drawing.Color.White
        Me.jobsList.baseFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.jobsList.baseForeColor = System.Drawing.Color.Black
        Me.jobsList.bgColor = System.Drawing.Color.White
        Me.jobsList.borderColor = System.Drawing.Color.Empty
        Me.jobsList.borderSelColor = System.Drawing.Color.Empty
        Me.jobsList.borderStyle = CI.Controls.List.BSType.NoBorder
        Me.jobsList.CausesValidation = False
        Me.jobsList.clickEnabled = False
        Me.jobsList.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.jobsList.do3D = False
        Me.jobsList.draw = False
        Me.jobsList.extraWidth = 0
        Me.jobsList.hScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.jobsList.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.jobsList.hScrolling = False
        Me.jobsList.hsValue = 0
        Me.jobsList.icons = CType(resources.GetObject("jobsList.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.jobsList.itemBorder = 0
        Me.jobsList.itemMargin = 0
        Me.jobsList.items = CType(resources.GetObject("jobsList.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.jobsList.Location = New System.Drawing.Point(3, 24)
        Me.jobsList.mouseMove3D = False
        Me.jobsList.mouseSpeed = 0
        Me.jobsList.Name = "jobsList"
        Me.jobsList.objMaxHeight = 0.0!
        Me.jobsList.objMaxWidth = 0.0!
        Me.jobsList.objMinHeight = 0.0!
        Me.jobsList.objMinWidth = 0.0!
        Me.jobsList.reverseSorting = False
        Me.jobsList.selected = -1
        Me.jobsList.selectedClickAllowed = False
        Me.jobsList.selectMultiple = False
        Me.jobsList.Size = New System.Drawing.Size(396, 229)
        Me.jobsList.sorted = False
        Me.jobsList.TabIndex = 0
        Me.jobsList.toolTipText = Nothing
        Me.jobsList.vScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.jobsList.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.jobsList.vScrolling = False
        Me.jobsList.vsValue = 0
        '
        'ControlBox
        '
        Me.ControlBox.BackColor = System.Drawing.Color.Transparent
        Me.ControlBox.isLocked = True
        Me.ControlBox.Location = New System.Drawing.Point(352, 3)
        Me.ControlBox.Name = "ControlBox"
        Me.ControlBox.Size = New System.Drawing.Size(47, 15)
        Me.ControlBox.TabIndex = 51
        '
        'ctlTitle
        '
        Me.ctlTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlTitle.BackColor = System.Drawing.Color.Black
        Me.ctlTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlTitle.ForeColor = System.Drawing.Color.White
        Me.ctlTitle.Location = New System.Drawing.Point(3, 0)
        Me.ctlTitle.Name = "ctlTitle"
        Me.ctlTitle.Size = New System.Drawing.Size(396, 24)
        Me.ctlTitle.TabIndex = 52
        Me.ctlTitle.Text = "Documents en attente d'impression"
        Me.ctlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PrintingJobs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Black
        Me.Controls.Add(Me.ControlBox)
        Me.Controls.Add(Me.jobsList)
        Me.Controls.Add(Me.ctlTitle)
        Me.Name = "PrintingJobs"
        Me.Size = New System.Drawing.Size(402, 256)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents jobsList As CI.Controls.List
    Friend WithEvents ControlBox As TransparentControlBox
    Friend WithEvents ctlTitle As System.Windows.Forms.Label

End Class
