<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportGenerationInGroup
    Inherits SingleWindow

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportGenerationInGroup))
        Me.doReportGenerationInGroup = New System.Windows.Forms.Button
        Me.typesRapports = New TreeViewPlus
        Me.label1 = New System.Windows.Forms.Label
        Me.save = New System.Windows.Forms.Button
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.selectDBPath = New System.Windows.Forms.Button
        Me.dbPath = New System.Windows.Forms.Label
        Me.groupOfLot = New System.Windows.Forms.GroupBox
        Me.temporalSize = New ManagedCombo
        Me.groupName = New ManagedText
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.listGroups = New CI.Controls.List
        Me.add = New System.Windows.Forms.Button
        Me.groupOfLot.SuspendLayout()
        Me.SuspendLayout()
        '
        'doReportGenerationInGroup
        '
        Me.doReportGenerationInGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.doReportGenerationInGroup.Location = New System.Drawing.Point(10, 346)
        Me.doReportGenerationInGroup.Name = "doReportGenerationInGroup"
        Me.doReportGenerationInGroup.Size = New System.Drawing.Size(139, 23)
        Me.doReportGenerationInGroup.TabIndex = 0
        Me.doReportGenerationInGroup.Text = "Effectuer la génération"
        Me.doReportGenerationInGroup.UseVisualStyleBackColor = True
        '
        'typesRapports
        '
        Me.typesRapports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.typesRapports.CheckBoxes = True
        Me.typesRapports.expandAllNodes = True
        Me.typesRapports.ImageIndex = 0
        Me.typesRapports.Location = New System.Drawing.Point(6, 140)
        Me.typesRapports.Name = "typesRapports"
        Me.typesRapports.PathSeparator = " \\ "
        Me.typesRapports.readOnly = False
        Me.typesRapports.SelectedImageIndex = 0
        Me.typesRapports.showImages = False
        Me.typesRapports.Size = New System.Drawing.Size(298, 182)
        Me.typesRapports.Sorted = True
        Me.typesRapports.TabIndex = 1
        Me.typesRapports.tree = Nothing
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(3, 124)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(141, 13)
        Me.label1.TabIndex = 2
        Me.label1.Text = "Types de rapport à générer :"
        '
        'save
        '
        Me.save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.save.Location = New System.Drawing.Point(484, 346)
        Me.save.Name = "save"
        Me.save.Size = New System.Drawing.Size(24, 24)
        Me.save.TabIndex = 3
        Me.toolTip1.SetToolTip(Me.save, "Enregistrer le lot sélectionné")
        Me.save.UseVisualStyleBackColor = True
        '
        'toolTip1
        '
        Me.toolTip1.ShowAlways = True
        '
        'selectDBPath
        '
        Me.selectDBPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectDBPath.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectDBPath.Location = New System.Drawing.Point(6, 56)
        Me.selectDBPath.Name = "selectDBPath"
        Me.selectDBPath.Size = New System.Drawing.Size(24, 24)
        Me.selectDBPath.TabIndex = 3
        Me.toolTip1.SetToolTip(Me.selectDBPath, "Sélectionner le dossier où enregistrer les rapports")
        Me.selectDBPath.UseVisualStyleBackColor = True
        '
        'dbPath
        '
        Me.dbPath.AutoSize = True
        Me.dbPath.Location = New System.Drawing.Point(36, 62)
        Me.dbPath.Name = "dbPath"
        Me.dbPath.Size = New System.Drawing.Size(132, 13)
        Me.dbPath.TabIndex = 4
        Me.dbPath.Text = "Aucun chemin sélectionné"
        '
        'groupOfLot
        '
        Me.groupOfLot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupOfLot.Controls.Add(Me.temporalSize)
        Me.groupOfLot.Controls.Add(Me.groupName)
        Me.groupOfLot.Controls.Add(Me.label3)
        Me.groupOfLot.Controls.Add(Me.label2)
        Me.groupOfLot.Controls.Add(Me.label1)
        Me.groupOfLot.Controls.Add(Me.dbPath)
        Me.groupOfLot.Controls.Add(Me.typesRapports)
        Me.groupOfLot.Controls.Add(Me.selectDBPath)
        Me.groupOfLot.Location = New System.Drawing.Point(198, 12)
        Me.groupOfLot.Name = "groupOfLot"
        Me.groupOfLot.Size = New System.Drawing.Size(310, 328)
        Me.groupOfLot.TabIndex = 5
        Me.groupOfLot.TabStop = False
        Me.groupOfLot.Text = "Lot"
        '
        'temporalSize
        '
        Me.temporalSize.acceptAlpha = True
        Me.temporalSize.acceptedChars = Nothing
        Me.temporalSize.acceptNumeric = True
        Me.temporalSize.allCapital = False
        Me.temporalSize.allLower = False
        Me.temporalSize.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.temporalSize.autoComplete = True
        Me.temporalSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.temporalSize.autoSizeDropDown = True
        Me.temporalSize.BackColor = System.Drawing.Color.White
        Me.temporalSize.blockOnMaximum = False
        Me.temporalSize.blockOnMinimum = False
        Me.temporalSize.cb_AcceptLeftZeros = False
        Me.temporalSize.cb_AcceptNegative = False
        Me.temporalSize.currencyBox = False
        Me.temporalSize.dbField = Nothing
        Me.temporalSize.doComboDelete = True
        Me.temporalSize.firstLetterCapital = False
        Me.temporalSize.firstLettersCapital = False
        Me.temporalSize.FormattingEnabled = True
        Me.temporalSize.Items.AddRange(New Object() {"Hebdomadaire", "Journalière", "Mensuelle", "Variable"})
        Me.temporalSize.itemsToolTipDuration = 10000
        Me.temporalSize.Location = New System.Drawing.Point(6, 100)
        Me.temporalSize.manageText = False
        Me.temporalSize.matchExp = Nothing
        Me.temporalSize.maximum = 0
        Me.temporalSize.minimum = 0
        Me.temporalSize.Name = "temporalSize"
        Me.temporalSize.nbDecimals = CType(-1, Short)
        Me.temporalSize.onlyAlphabet = False
        Me.temporalSize.pathOfList = Nothing
        Me.temporalSize.ReadOnly = False
        Me.temporalSize.refuseAccents = False
        Me.temporalSize.refusedChars = Nothing
        Me.temporalSize.showItemsToolTip = False
        Me.temporalSize.Size = New System.Drawing.Size(298, 21)
        Me.temporalSize.TabIndex = 6
        Me.temporalSize.trimText = False
        '
        'groupName
        '
        Me.groupName.acceptAlpha = True
        Me.groupName.acceptedChars = ""
        Me.groupName.acceptNumeric = True
        Me.groupName.allCapital = False
        Me.groupName.allLower = False
        Me.groupName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupName.blockOnMaximum = False
        Me.groupName.blockOnMinimum = False
        Me.groupName.cb_AcceptLeftZeros = False
        Me.groupName.cb_AcceptNegative = False
        Me.groupName.currencyBox = False
        Me.groupName.firstLetterCapital = False
        Me.groupName.firstLettersCapital = False
        Me.groupName.Location = New System.Drawing.Point(6, 30)
        Me.groupName.manageText = True
        Me.groupName.matchExp = ""
        Me.groupName.maximum = 0
        Me.groupName.minimum = 0
        Me.groupName.Name = "groupName"
        Me.groupName.nbDecimals = CType(-1, Short)
        Me.groupName.onlyAlphabet = False
        Me.groupName.refuseAccents = False
        Me.groupName.refusedChars = ""
        Me.groupName.showInternalContextMenu = True
        Me.groupName.Size = New System.Drawing.Size(298, 20)
        Me.groupName.TabIndex = 5
        Me.groupName.trimText = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(3, 83)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(137, 13)
        Me.label3.TabIndex = 2
        Me.label3.Text = "Grandeur du filtre temporel :"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(3, 16)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(64, 13)
        Me.label2.TabIndex = 2
        Me.label2.Text = "Nom du lot :"
        '
        'listGroups
        '
        Me.listGroups.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.listGroups.autoAdjust = False
        Me.listGroups.autoKeyDownSelection = True
        Me.listGroups.autoSizeHorizontally = False
        Me.listGroups.autoSizeVertically = False
        Me.listGroups.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.listGroups.baseBackColor = System.Drawing.Color.White
        Me.listGroups.baseFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.listGroups.baseForeColor = System.Drawing.Color.Black
        Me.listGroups.bgColor = System.Drawing.SystemColors.Control
        Me.listGroups.borderColor = System.Drawing.Color.Empty
        Me.listGroups.borderSelColor = System.Drawing.Color.Empty
        Me.listGroups.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.listGroups.CausesValidation = False
        Me.listGroups.clickEnabled = False
        Me.listGroups.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.listGroups.do3D = False
        Me.listGroups.draw = False
        Me.listGroups.extraWidth = 0
        Me.listGroups.hScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.listGroups.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listGroups.hScrolling = False
        Me.listGroups.hsValue = 0
        Me.listGroups.icons = CType(resources.GetObject("listGroups.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.listGroups.itemBorder = 0
        Me.listGroups.itemMargin = 0
        Me.listGroups.items = CType(resources.GetObject("listGroups.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.listGroups.Location = New System.Drawing.Point(10, 12)
        Me.listGroups.mouseMove3D = False
        Me.listGroups.mouseSpeed = 0
        Me.listGroups.Name = "listGroups"
        Me.listGroups.objMaxHeight = 0.0!
        Me.listGroups.objMaxWidth = 0.0!
        Me.listGroups.objMinHeight = 0.0!
        Me.listGroups.objMinWidth = 0.0!
        Me.listGroups.reverseSorting = False
        Me.listGroups.selected = -1
        Me.listGroups.selectedClickAllowed = False
        Me.listGroups.selectMultiple = False
        Me.listGroups.Size = New System.Drawing.Size(182, 328)
        Me.listGroups.sorted = False
        Me.listGroups.TabIndex = 6
        Me.listGroups.toolTipText = Nothing
        Me.listGroups.vScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.listGroups.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listGroups.vScrolling = False
        Me.listGroups.vsValue = 0
        '
        'add
        '
        Me.add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.add.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.add.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.add.Location = New System.Drawing.Point(454, 346)
        Me.add.Name = "add"
        Me.add.Size = New System.Drawing.Size(24, 24)
        Me.add.TabIndex = 3
        Me.add.UseVisualStyleBackColor = True
        '
        'ReportGenerationInGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 381)
        Me.Controls.Add(Me.listGroups)
        Me.Controls.Add(Me.groupOfLot)
        Me.Controls.Add(Me.doReportGenerationInGroup)
        Me.Controls.Add(Me.add)
        Me.Controls.Add(Me.save)
        Me.MinimumSize = New System.Drawing.Size(419, 339)
        Me.Name = "ReportGenerationInGroup"
        Me.Text = "Générateur de rapports en lot"
        Me.groupOfLot.ResumeLayout(False)
        Me.groupOfLot.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents doReportGenerationInGroup As System.Windows.Forms.Button
    Private WithEvents typesRapports As TreeViewPlus
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents save As System.Windows.Forms.Button
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents selectDBPath As System.Windows.Forms.Button
    Private WithEvents dbPath As System.Windows.Forms.Label

    Private WithEvents groupOfLot As System.Windows.Forms.GroupBox
    Private WithEvents groupName As ManagedText
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents listGroups As CI.Controls.List
    Private WithEvents add As System.Windows.Forms.Button
    Friend WithEvents temporalSize As ManagedCombo
    Private WithEvents label3 As System.Windows.Forms.Label
End Class
