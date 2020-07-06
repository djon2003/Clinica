Public Class ClientCopyBox
    Inherits System.Windows.Forms.Panel

    Private WithEvents copyBox As CI.Controls.List
    Private WithEvents periode As System.Windows.Forms.ComboBox

    Public Sub New()
        MyBase.New()

        'MySelf config
        Me.Visible = True

        '
        'CopyBox
        '
        Me.copyBox = New CI.Controls.List()
        Me.copyBox.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.copyBox.autoAdjust = False
        Me.copyBox.autoKeyDownSelection = True
        Me.copyBox.autoSizeHorizontally = False
        Me.copyBox.autoSizeVertically = False
        Me.copyBox.BackColor = System.Drawing.Color.White
        Me.copyBox.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.copyBox.baseBackColor = System.Drawing.Color.White
        Me.copyBox.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.copyBox.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.copyBox.bgColor = System.Drawing.Color.White
        Me.copyBox.borderColor = System.Drawing.Color.Empty
        Me.copyBox.borderSelColor = System.Drawing.Color.Empty
        Me.copyBox.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.copyBox.clickEnabled = False
        Me.copyBox.do3D = False
        Me.copyBox.draw = True
        Me.copyBox.hScrollColor = System.Drawing.SystemColors.Control
        Me.copyBox.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.copyBox.hScrolling = False
        Me.copyBox.hsValue = CType(0, Short)
        Me.copyBox.itemBorder = CType(0, Short)
        Me.copyBox.Location = New System.Drawing.Point(0, 0)
        Me.copyBox.itemMargin = CType(0, Short)
        Me.copyBox.mouseMove3D = False
        Me.copyBox.mouseSpeed = 0
        Me.copyBox.Name = "CopyBox"
        Me.copyBox.objMaxHeight = 0.0!
        Me.copyBox.objMaxWidth = 0.0!
        Me.copyBox.reverseSorting = False
        Me.copyBox.selected = CType(-1, Short)
        Me.copyBox.selectedClickAllowed = False
        Me.copyBox.Size = New System.Drawing.Size(168, 16)
        Me.copyBox.sorted = False
        Me.copyBox.TabIndex = 10
        Me.copyBox.toolTipText = Nothing
        Me.copyBox.vScrollColor = System.Drawing.SystemColors.Control
        Me.copyBox.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.copyBox.vScrolling = False
        Me.copyBox.vsValue = CType(0, Short)
        Me.copyBox.Margin = New Padding(3, 0, 0, 0)
        Me.copyBox.Dock = DockStyle.Right
        Me.copyBox.add("Aucun")

        '
        'Periode
        '
        Me.periode = New System.Windows.Forms.ComboBox
        Me.periode.BackColor = System.Drawing.SystemColors.Window
        Me.periode.Cursor = System.Windows.Forms.Cursors.Default
        Me.periode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.periode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.periode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.periode.Items.AddRange(New Object() {"15 minutes", "30 minutes", "45 minutes", "1 heure", "1h15min", "1h30min", "1h45min", "2 heures", "2h15min", "2h30min", "2h45min", "3 heures", "3h15min", "3h30min", "3h45min", "4 heures", "4h15min", "4h30min", "4h45min", "5 heures", "5h15min", "5h30min", "5h45min", "6 heures"})
        Me.periode.Location = New System.Drawing.Point(0, 0)
        Me.periode.Name = "Periode"
        Me.periode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.periode.Size = New System.Drawing.Size(81, 22)
        Me.periode.TabIndex = 6
        Me.periode.Dock = DockStyle.Left

        Me.Controls.AddRange(New Control() {Me.periode, Me.copyBox})
        Me.Height = Me.periode.Height
        Me.Width = Me.periode.Width + Me.copyBox.Width + 3
        Me.PerformLayout()
    End Sub

    Public ReadOnly Property clientName() As String
        Get
            If copyBox.ItemText(0).StartsWith("Aucun") Then Return ""

            Return copyBox.ItemText(0)
        End Get
    End Property

    Public ReadOnly Property itemValueA() As Object
        Get
            If copyBox.ItemText(0).StartsWith("Aucun") Then Return ""

            Return copyBox.ItemValueA(0)
        End Get
    End Property

    Public ReadOnly Property itemValueB() As String
        Get
            If copyBox.ItemText(0).StartsWith("Aucun") Then Return ""

            Return copyBox.ItemValueB(0)
        End Get
    End Property

    Public ReadOnly Property periodeIndex() As Integer
        Get
            Return periode.SelectedIndex
        End Get
    End Property

    Public ReadOnly Property periodeMinutes() As Integer
        Get
            Return (periode.SelectedIndex + 1) * 15
        End Get
    End Property

    Public Sub setClient(ByVal itemValueA As AgendaEntry, ByVal periodeIndex As Integer)
        copyBox.ItemText(0) = itemValueA.toString
        copyBox.ItemValueA(0) = itemValueA
        myMainWin.toolTip1.SetToolTip(copyBox, itemValueA.itemText)

        periode.SelectedIndex = periodeIndex
        If periode.SelectedIndex = -1 Then periode.SelectedIndex = 0
    End Sub

    Private Sub periode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles periode.SelectedIndexChanged
        Try
            If copyBox.ItemValueA(0).ToString().IndexOf("/") <> -1 And periode.SelectedIndex > 7 Then periode.SelectedIndex = 7
        Catch
            'No copied client
        End Try
    End Sub
End Class
