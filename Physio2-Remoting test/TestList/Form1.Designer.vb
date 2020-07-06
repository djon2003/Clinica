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
    Private Sub initializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Button2 = New System.Windows.Forms.Button
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(379, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(460, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "CLS"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.HelpVisible = False
        Me.PropertyGrid1.Location = New System.Drawing.Point(379, 70)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(238, 262)
        Me.PropertyGrid1.TabIndex = 5
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(542, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "Draw"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(460, 70)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "Save"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(542, 70)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 0
        Me.Button5.Text = "Load"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(379, 41)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 0
        Me.Button6.Text = "Remove"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(460, 41)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 23)
        Me.Button7.TabIndex = 0
        Me.Button7.Text = "Tie"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(542, 41)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(75, 23)
        Me.Button8.TabIndex = 0
        Me.Button8.Text = "GenRand"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 344)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Private WithEvents listBox1 As CyberInternautes.Controls.List

    Public Sub new()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ListBox1 = New CyberInternautes.Controls.List
        Me.ListBox1.AutoAdjust = True
        Me.ListBox1.AutoKeyDownSelection = True
        Me.ListBox1.AutoSizeHorizontally = False
        Me.ListBox1.AutoSizeVertically = False
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Window
        Me.ListBox1.BaseAlignment = CyberInternautes.Controls.IListItem.AlignmentType.LeftA
        Me.ListBox1.BaseBackColor = System.Drawing.Color.White
        Me.ListBox1.BaseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.ListBox1.BaseForeColor = System.Drawing.SystemColors.ControlText
        Me.ListBox1.BGColor = System.Drawing.SystemColors.Window
        Me.ListBox1.BorderColor = System.Drawing.SystemColors.Window
        Me.ListBox1.BorderSelColor = System.Drawing.Color.Blue
        Me.ListBox1.BorderStyle = CyberInternautes.Controls.List.BSType.FixedSingle
        Me.ListBox1.CausesValidation = False
        Me.ListBox1.ClickEnabled = True
        Me.ListBox1.Do3D = False
        Me.ListBox1.Draw = False
        Me.ListBox1.HScrollColor = System.Drawing.SystemColors.Window
        Me.ListBox1.HScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.ListBox1.HScrolling = True
        Me.ListBox1.HSValue = 0
        Me.ListBox1.Icon = Nothing
        Me.ListBox1.ItemBorder = 0
        Me.ListBox1.ItemMargin = 0
        Me.ListBox1.Location = New System.Drawing.Point(12, 12)
        Me.ListBox1.MouseMove3D = True
        Me.ListBox1.MouseSpeed = 30
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.ObjMaxHeight = 0.0!
        Me.ListBox1.ObjMaxWidth = 0.0!
        Me.ListBox1.ObjMinHeight = 0.0!
        Me.ListBox1.ObjMinWidth = 0.0!
        Me.ListBox1.ReverseSorting = False
        Me.ListBox1.Selected = -1
        Me.ListBox1.SelectedClickAllowed = False
        Me.ListBox1.Size = New System.Drawing.Size(214, 228)
        Me.ListBox1.Sorted = False
        Me.ListBox1.TabIndex = 1
        Me.ListBox1.ToolTipText = Nothing
        Me.ListBox1.VScrolling = True
        Me.ListBox1.VSValue = 0
        Me.Controls.Add(Me.ListBox1)

        Me.PropertyGrid1.SelectedObject = Me.ListBox1
    End Sub
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
End Class
