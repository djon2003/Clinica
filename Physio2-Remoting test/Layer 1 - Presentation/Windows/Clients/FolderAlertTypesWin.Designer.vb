<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FolderAlertTypesWin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FolderAlertTypesWin))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.add = New System.Windows.Forms.Button
        Me.modif = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.AlertMessage = New ManagedText
        Me.gbFTT = New System.Windows.Forms.GroupBox
        Me.AlertTitle = New ManagedText
        Me.Label8 = New System.Windows.Forms.Label
        Me.AlertNbDaysForExpiry = New ManagedText
        Me.TypeDateDebut = New ManagedCombo
        Me.Label2 = New System.Windows.Forms.Label
        Me.DateDebutNbPresencesX = New ManagedText
        Me.NbPresencesX = New ManagedText
        Me.AlertNbPresencesDiff = New ManagedText
        Me.AlertNbDaysDiff = New ManagedText
        Me.DateDebutNbDaysX = New ManagedText
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.listFolderAlerts = New CI.Controls.List
        Me.gbFTT.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'add
        '
        Me.add.BackColor = System.Drawing.SystemColors.Control
        Me.add.Cursor = System.Windows.Forms.Cursors.Default
        Me.add.Enabled = False
        Me.add.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.add.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.add.ForeColor = System.Drawing.SystemColors.ControlText
        Me.add.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.add.Location = New System.Drawing.Point(335, 245)
        Me.add.Name = "add"
        Me.add.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.add.Size = New System.Drawing.Size(24, 24)
        Me.add.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.add, "Ajouter un type de message instantanné")
        Me.add.UseVisualStyleBackColor = False
        '
        'modif
        '
        Me.modif.BackColor = System.Drawing.SystemColors.Control
        Me.modif.Cursor = System.Windows.Forms.Cursors.Default
        Me.modif.Enabled = False
        Me.modif.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modif.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modif.Location = New System.Drawing.Point(419, 245)
        Me.modif.Name = "modif"
        Me.modif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modif.Size = New System.Drawing.Size(24, 24)
        Me.modif.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.modif, "Modifier le type de message instantanné sélectionné")
        Me.modif.UseVisualStyleBackColor = False
        '
        'delete
        '
        Me.delete.BackColor = System.Drawing.SystemColors.Control
        Me.delete.Cursor = System.Windows.Forms.Cursors.Default
        Me.delete.Enabled = False
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.delete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delete.Location = New System.Drawing.Point(503, 245)
        Me.delete.Name = "delete"
        Me.delete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer le(s) type(s) de message(s) instantanné(s) sélectionné(s)")
        Me.delete.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 213)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(223, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Message (Variables possibles - voir Infobulle) :"
        Me.ToolTip1.SetToolTip(Me.Label7, "(Variables : ###ClientName###, ###NoFolder###, ###EndDate###)")
        '
        'AlertMessage
        '
        Me.AlertMessage.acceptAlpha = True
        Me.AlertMessage.acceptedChars = ""
        Me.AlertMessage.acceptNumeric = True
        Me.AlertMessage.allCapital = False
        Me.AlertMessage.allLower = False
        Me.AlertMessage.blockOnMaximum = False
        Me.AlertMessage.blockOnMinimum = False
        Me.AlertMessage.cb_AcceptLeftZeros = False
        Me.AlertMessage.cb_AcceptNegative = False
        Me.AlertMessage.currencyBox = False
        Me.AlertMessage.firstLetterCapital = False
        Me.AlertMessage.firstLettersCapital = False
        Me.AlertMessage.Location = New System.Drawing.Point(9, 228)
        Me.AlertMessage.manageText = True
        Me.AlertMessage.matchExp = ""
        Me.AlertMessage.maximum = 0
        Me.AlertMessage.minimum = 1
        Me.AlertMessage.Name = "AlertMessage"
        Me.AlertMessage.nbDecimals = CType(0, Short)
        Me.AlertMessage.onlyAlphabet = False
        Me.AlertMessage.refuseAccents = False
        Me.AlertMessage.refusedChars = ""
        Me.AlertMessage.showInternalContextMenu = True
        Me.AlertMessage.Size = New System.Drawing.Size(297, 20)
        Me.AlertMessage.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.AlertMessage, "(Variables : ###ClientName###, ###NoFolder###, ###EndDate###)")
        Me.AlertMessage.trimText = False
        '
        'gbFTT
        '
        Me.gbFTT.Controls.Add(Me.AlertMessage)
        Me.gbFTT.Controls.Add(Me.AlertTitle)
        Me.gbFTT.Controls.Add(Me.Label8)
        Me.gbFTT.Controls.Add(Me.AlertNbDaysForExpiry)
        Me.gbFTT.Controls.Add(Me.TypeDateDebut)
        Me.gbFTT.Controls.Add(Me.Label7)
        Me.gbFTT.Controls.Add(Me.Label2)
        Me.gbFTT.Controls.Add(Me.DateDebutNbPresencesX)
        Me.gbFTT.Controls.Add(Me.NbPresencesX)
        Me.gbFTT.Controls.Add(Me.AlertNbPresencesDiff)
        Me.gbFTT.Controls.Add(Me.AlertNbDaysDiff)
        Me.gbFTT.Controls.Add(Me.DateDebutNbDaysX)
        Me.gbFTT.Controls.Add(Me.Label3)
        Me.gbFTT.Controls.Add(Me.Label1)
        Me.gbFTT.Controls.Add(Me.Label5)
        Me.gbFTT.Controls.Add(Me.Label4)
        Me.gbFTT.Controls.Add(Me.Label15)
        Me.gbFTT.Controls.Add(Me.Label6)
        Me.gbFTT.Enabled = False
        Me.gbFTT.Location = New System.Drawing.Point(12, 12)
        Me.gbFTT.Name = "gbFTT"
        Me.gbFTT.Size = New System.Drawing.Size(317, 257)
        Me.gbFTT.TabIndex = 2
        Me.gbFTT.TabStop = False
        Me.gbFTT.Text = "Message instantanné sélectionné"
        '
        'AlertTitle
        '
        Me.AlertTitle.acceptAlpha = True
        Me.AlertTitle.acceptedChars = ""
        Me.AlertTitle.acceptNumeric = True
        Me.AlertTitle.allCapital = False
        Me.AlertTitle.allLower = False
        Me.AlertTitle.blockOnMaximum = False
        Me.AlertTitle.blockOnMinimum = False
        Me.AlertTitle.cb_AcceptLeftZeros = False
        Me.AlertTitle.cb_AcceptNegative = False
        Me.AlertTitle.currencyBox = False
        Me.AlertTitle.firstLetterCapital = False
        Me.AlertTitle.firstLettersCapital = False
        Me.AlertTitle.Location = New System.Drawing.Point(46, 24)
        Me.AlertTitle.manageText = True
        Me.AlertTitle.matchExp = ""
        Me.AlertTitle.maximum = 0
        Me.AlertTitle.minimum = 1
        Me.AlertTitle.Name = "AlertTitle"
        Me.AlertTitle.nbDecimals = CType(0, Short)
        Me.AlertTitle.onlyAlphabet = False
        Me.AlertTitle.refuseAccents = False
        Me.AlertTitle.refusedChars = ""
        Me.AlertTitle.showInternalContextMenu = True
        Me.AlertTitle.Size = New System.Drawing.Size(260, 20)
        Me.AlertTitle.TabIndex = 2
        Me.AlertTitle.trimText = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 190)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(167, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Nombre de jour avant l'expiration :"
        '
        'AlertNbDaysForExpiry
        '
        Me.AlertNbDaysForExpiry.acceptAlpha = False
        Me.AlertNbDaysForExpiry.acceptedChars = ",§."
        Me.AlertNbDaysForExpiry.acceptNumeric = True
        Me.AlertNbDaysForExpiry.allCapital = False
        Me.AlertNbDaysForExpiry.allLower = False
        Me.AlertNbDaysForExpiry.blockOnMaximum = False
        Me.AlertNbDaysForExpiry.blockOnMinimum = False
        Me.AlertNbDaysForExpiry.cb_AcceptLeftZeros = False
        Me.AlertNbDaysForExpiry.cb_AcceptNegative = False
        Me.AlertNbDaysForExpiry.currencyBox = True
        Me.AlertNbDaysForExpiry.firstLetterCapital = False
        Me.AlertNbDaysForExpiry.firstLettersCapital = False
        Me.AlertNbDaysForExpiry.Location = New System.Drawing.Point(278, 187)
        Me.AlertNbDaysForExpiry.manageText = True
        Me.AlertNbDaysForExpiry.matchExp = ""
        Me.AlertNbDaysForExpiry.maximum = 0
        Me.AlertNbDaysForExpiry.minimum = 0
        Me.AlertNbDaysForExpiry.Name = "AlertNbDaysForExpiry"
        Me.AlertNbDaysForExpiry.nbDecimals = CType(0, Short)
        Me.AlertNbDaysForExpiry.onlyAlphabet = False
        Me.AlertNbDaysForExpiry.refuseAccents = False
        Me.AlertNbDaysForExpiry.refusedChars = ""
        Me.AlertNbDaysForExpiry.showInternalContextMenu = True
        Me.AlertNbDaysForExpiry.Size = New System.Drawing.Size(28, 20)
        Me.AlertNbDaysForExpiry.TabIndex = 2
        Me.AlertNbDaysForExpiry.Text = "0"
        Me.AlertNbDaysForExpiry.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.AlertNbDaysForExpiry.trimText = False
        '
        'TypeDateDebut
        '
        Me.TypeDateDebut.acceptAlpha = True
        Me.TypeDateDebut.acceptedChars = Nothing
        Me.TypeDateDebut.acceptNumeric = True
        Me.TypeDateDebut.allCapital = False
        Me.TypeDateDebut.allLower = False
        Me.TypeDateDebut.autoComplete = True
        Me.TypeDateDebut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TypeDateDebut.autoSizeDropDown = True
        Me.TypeDateDebut.blockOnMaximum = False
        Me.TypeDateDebut.blockOnMinimum = False
        Me.TypeDateDebut.cb_AcceptLeftZeros = False
        Me.TypeDateDebut.cb_AcceptNegative = False
        Me.TypeDateDebut.currencyBox = False
        Me.TypeDateDebut.dbField = Nothing
        Me.TypeDateDebut.doComboDelete = True
        Me.TypeDateDebut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeDateDebut.firstLetterCapital = False
        Me.TypeDateDebut.firstLettersCapital = False
        Me.TypeDateDebut.FormattingEnabled = True
        Me.TypeDateDebut.Items.AddRange(New Object() {"À la création du dossier", "À partir de la date d'accident", "À partir de la date de rechute", "À partir de la date de référence", "À la présence X"})
        Me.TypeDateDebut.itemsToolTipDuration = 10000
        Me.TypeDateDebut.Location = New System.Drawing.Point(119, 54)
        Me.TypeDateDebut.manageText = True
        Me.TypeDateDebut.matchExp = Nothing
        Me.TypeDateDebut.maximum = 0
        Me.TypeDateDebut.minimum = 0
        Me.TypeDateDebut.Name = "TypeDateDebut"
        Me.TypeDateDebut.nbDecimals = CType(-1, Short)
        Me.TypeDateDebut.onlyAlphabet = False
        Me.TypeDateDebut.pathOfList = Nothing
        Me.TypeDateDebut.ReadOnly = False
        Me.TypeDateDebut.refuseAccents = False
        Me.TypeDateDebut.refusedChars = Nothing
        Me.TypeDateDebut.showItemsToolTip = False
        Me.TypeDateDebut.Size = New System.Drawing.Size(187, 21)
        Me.TypeDateDebut.TabIndex = 3
        Me.TypeDateDebut.trimText = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Titre :"
        '
        'DateDebutNbPresencesX
        '
        Me.DateDebutNbPresencesX.acceptAlpha = False
        Me.DateDebutNbPresencesX.acceptedChars = ",§."
        Me.DateDebutNbPresencesX.acceptNumeric = True
        Me.DateDebutNbPresencesX.allCapital = False
        Me.DateDebutNbPresencesX.allLower = False
        Me.DateDebutNbPresencesX.blockOnMaximum = False
        Me.DateDebutNbPresencesX.blockOnMinimum = True
        Me.DateDebutNbPresencesX.cb_AcceptLeftZeros = False
        Me.DateDebutNbPresencesX.cb_AcceptNegative = False
        Me.DateDebutNbPresencesX.currencyBox = True
        Me.DateDebutNbPresencesX.Enabled = False
        Me.DateDebutNbPresencesX.firstLetterCapital = False
        Me.DateDebutNbPresencesX.firstLettersCapital = False
        Me.DateDebutNbPresencesX.Location = New System.Drawing.Point(265, 81)
        Me.DateDebutNbPresencesX.manageText = True
        Me.DateDebutNbPresencesX.matchExp = ""
        Me.DateDebutNbPresencesX.maximum = 0
        Me.DateDebutNbPresencesX.minimum = 1
        Me.DateDebutNbPresencesX.Name = "DateDebutNbPresencesX"
        Me.DateDebutNbPresencesX.nbDecimals = CType(0, Short)
        Me.DateDebutNbPresencesX.onlyAlphabet = False
        Me.DateDebutNbPresencesX.refuseAccents = False
        Me.DateDebutNbPresencesX.refusedChars = ""
        Me.DateDebutNbPresencesX.showInternalContextMenu = True
        Me.DateDebutNbPresencesX.Size = New System.Drawing.Size(41, 20)
        Me.DateDebutNbPresencesX.TabIndex = 2
        Me.DateDebutNbPresencesX.Text = "1"
        Me.DateDebutNbPresencesX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.DateDebutNbPresencesX.trimText = False
        '
        'NbPresencesX
        '
        Me.NbPresencesX.acceptAlpha = False
        Me.NbPresencesX.acceptedChars = ",§."
        Me.NbPresencesX.acceptNumeric = True
        Me.NbPresencesX.allCapital = False
        Me.NbPresencesX.allLower = False
        Me.NbPresencesX.blockOnMaximum = False
        Me.NbPresencesX.blockOnMinimum = True
        Me.NbPresencesX.cb_AcceptLeftZeros = False
        Me.NbPresencesX.cb_AcceptNegative = False
        Me.NbPresencesX.currencyBox = True
        Me.NbPresencesX.firstLetterCapital = False
        Me.NbPresencesX.firstLettersCapital = False
        Me.NbPresencesX.Location = New System.Drawing.Point(278, 110)
        Me.NbPresencesX.manageText = True
        Me.NbPresencesX.matchExp = ""
        Me.NbPresencesX.maximum = 0
        Me.NbPresencesX.minimum = 1
        Me.NbPresencesX.Name = "NbPresencesX"
        Me.NbPresencesX.nbDecimals = CType(0, Short)
        Me.NbPresencesX.onlyAlphabet = False
        Me.NbPresencesX.refuseAccents = False
        Me.NbPresencesX.refusedChars = ""
        Me.NbPresencesX.showInternalContextMenu = True
        Me.NbPresencesX.Size = New System.Drawing.Size(28, 20)
        Me.NbPresencesX.TabIndex = 2
        Me.NbPresencesX.Text = "1"
        Me.NbPresencesX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NbPresencesX.trimText = False
        '
        'AlertNbPresencesDiff
        '
        Me.AlertNbPresencesDiff.acceptAlpha = False
        Me.AlertNbPresencesDiff.acceptedChars = ",§."
        Me.AlertNbPresencesDiff.acceptNumeric = True
        Me.AlertNbPresencesDiff.allCapital = False
        Me.AlertNbPresencesDiff.allLower = False
        Me.AlertNbPresencesDiff.blockOnMaximum = False
        Me.AlertNbPresencesDiff.blockOnMinimum = False
        Me.AlertNbPresencesDiff.cb_AcceptLeftZeros = False
        Me.AlertNbPresencesDiff.cb_AcceptNegative = False
        Me.AlertNbPresencesDiff.currencyBox = True
        Me.AlertNbPresencesDiff.firstLetterCapital = False
        Me.AlertNbPresencesDiff.firstLettersCapital = False
        Me.AlertNbPresencesDiff.Location = New System.Drawing.Point(48, 165)
        Me.AlertNbPresencesDiff.manageText = True
        Me.AlertNbPresencesDiff.matchExp = ""
        Me.AlertNbPresencesDiff.maximum = 0
        Me.AlertNbPresencesDiff.minimum = 0
        Me.AlertNbPresencesDiff.Name = "AlertNbPresencesDiff"
        Me.AlertNbPresencesDiff.nbDecimals = CType(0, Short)
        Me.AlertNbPresencesDiff.onlyAlphabet = False
        Me.AlertNbPresencesDiff.refuseAccents = False
        Me.AlertNbPresencesDiff.refusedChars = ""
        Me.AlertNbPresencesDiff.showInternalContextMenu = True
        Me.AlertNbPresencesDiff.Size = New System.Drawing.Size(30, 20)
        Me.AlertNbPresencesDiff.TabIndex = 2
        Me.AlertNbPresencesDiff.Text = "0"
        Me.AlertNbPresencesDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.AlertNbPresencesDiff.trimText = False
        '
        'AlertNbDaysDiff
        '
        Me.AlertNbDaysDiff.acceptAlpha = False
        Me.AlertNbDaysDiff.acceptedChars = ",§."
        Me.AlertNbDaysDiff.acceptNumeric = True
        Me.AlertNbDaysDiff.allCapital = False
        Me.AlertNbDaysDiff.allLower = False
        Me.AlertNbDaysDiff.blockOnMaximum = False
        Me.AlertNbDaysDiff.blockOnMinimum = False
        Me.AlertNbDaysDiff.cb_AcceptLeftZeros = False
        Me.AlertNbDaysDiff.cb_AcceptNegative = False
        Me.AlertNbDaysDiff.currencyBox = True
        Me.AlertNbDaysDiff.firstLetterCapital = False
        Me.AlertNbDaysDiff.firstLettersCapital = False
        Me.AlertNbDaysDiff.Location = New System.Drawing.Point(48, 139)
        Me.AlertNbDaysDiff.manageText = True
        Me.AlertNbDaysDiff.matchExp = ""
        Me.AlertNbDaysDiff.maximum = 0
        Me.AlertNbDaysDiff.minimum = 0
        Me.AlertNbDaysDiff.Name = "AlertNbDaysDiff"
        Me.AlertNbDaysDiff.nbDecimals = CType(0, Short)
        Me.AlertNbDaysDiff.onlyAlphabet = False
        Me.AlertNbDaysDiff.refuseAccents = False
        Me.AlertNbDaysDiff.refusedChars = ""
        Me.AlertNbDaysDiff.showInternalContextMenu = True
        Me.AlertNbDaysDiff.Size = New System.Drawing.Size(30, 20)
        Me.AlertNbDaysDiff.TabIndex = 2
        Me.AlertNbDaysDiff.Text = "0"
        Me.AlertNbDaysDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.AlertNbDaysDiff.trimText = False
        '
        'DateDebutNbDaysX
        '
        Me.DateDebutNbDaysX.acceptAlpha = False
        Me.DateDebutNbDaysX.acceptedChars = ",§."
        Me.DateDebutNbDaysX.acceptNumeric = True
        Me.DateDebutNbDaysX.allCapital = False
        Me.DateDebutNbDaysX.allLower = False
        Me.DateDebutNbDaysX.blockOnMaximum = False
        Me.DateDebutNbDaysX.blockOnMinimum = False
        Me.DateDebutNbDaysX.cb_AcceptLeftZeros = False
        Me.DateDebutNbDaysX.cb_AcceptNegative = False
        Me.DateDebutNbDaysX.currencyBox = True
        Me.DateDebutNbDaysX.firstLetterCapital = False
        Me.DateDebutNbDaysX.firstLettersCapital = False
        Me.DateDebutNbDaysX.Location = New System.Drawing.Point(103, 108)
        Me.DateDebutNbDaysX.manageText = True
        Me.DateDebutNbDaysX.matchExp = ""
        Me.DateDebutNbDaysX.maximum = 0
        Me.DateDebutNbDaysX.minimum = 0
        Me.DateDebutNbDaysX.Name = "DateDebutNbDaysX"
        Me.DateDebutNbDaysX.nbDecimals = CType(0, Short)
        Me.DateDebutNbDaysX.onlyAlphabet = False
        Me.DateDebutNbDaysX.refuseAccents = False
        Me.DateDebutNbDaysX.refusedChars = ""
        Me.DateDebutNbDaysX.showInternalContextMenu = True
        Me.DateDebutNbDaysX.Size = New System.Drawing.Size(28, 20)
        Me.DateDebutNbDaysX.TabIndex = 2
        Me.DateDebutNbDaysX.Text = "0"
        Me.DateDebutNbDaysX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.DateDebutNbDaysX.trimText = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Moment de création :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(253, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Nombre de présence X pour le moment de création :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(153, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Nombre de présence X :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Nombre de jour X :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(6, 168)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(288, 13)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Afficher            présence avant la date de création du texte."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(6, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(261, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Afficher            jour avant la date de création du texte."
        '
        'listFolderAlerts
        '
        Me.listFolderAlerts.autoAdjust = True
        Me.listFolderAlerts.autoKeyDownSelection = True
        Me.listFolderAlerts.autoSizeHorizontally = False
        Me.listFolderAlerts.autoSizeVertically = False
        Me.listFolderAlerts.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.listFolderAlerts.baseBackColor = System.Drawing.Color.White
        Me.listFolderAlerts.baseFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.listFolderAlerts.baseForeColor = System.Drawing.Color.Black
        Me.listFolderAlerts.bgColor = System.Drawing.SystemColors.Control
        Me.listFolderAlerts.borderColor = System.Drawing.Color.Empty
        Me.listFolderAlerts.borderSelColor = System.Drawing.Color.Empty
        Me.listFolderAlerts.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.listFolderAlerts.CausesValidation = False
        Me.listFolderAlerts.clickEnabled = False
        Me.listFolderAlerts.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.listFolderAlerts.do3D = False
        Me.listFolderAlerts.draw = False
        Me.listFolderAlerts.extraWidth = 0
        Me.listFolderAlerts.hScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.listFolderAlerts.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listFolderAlerts.hScrolling = False
        Me.listFolderAlerts.hsValue = 0
        Me.listFolderAlerts.icons = CType(resources.GetObject("listFolderAlerts.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.listFolderAlerts.itemBorder = 0
        Me.listFolderAlerts.itemMargin = 0
        Me.listFolderAlerts.items = CType(resources.GetObject("listFolderAlerts.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.listFolderAlerts.Location = New System.Drawing.Point(335, 19)
        Me.listFolderAlerts.mouseMove3D = False
        Me.listFolderAlerts.mouseSpeed = 0
        Me.listFolderAlerts.Name = "listFolderAlerts"
        Me.listFolderAlerts.objMaxHeight = 0.0!
        Me.listFolderAlerts.objMaxWidth = 0.0!
        Me.listFolderAlerts.objMinHeight = 0.0!
        Me.listFolderAlerts.objMinWidth = 0.0!
        Me.listFolderAlerts.reverseSorting = False
        Me.listFolderAlerts.selected = -1
        Me.listFolderAlerts.selectedClickAllowed = False
        Me.listFolderAlerts.selectMultiple = True
        Me.listFolderAlerts.Size = New System.Drawing.Size(192, 220)
        Me.listFolderAlerts.sorted = False
        Me.listFolderAlerts.TabIndex = 3
        Me.listFolderAlerts.toolTipText = Nothing
        Me.listFolderAlerts.vScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.listFolderAlerts.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listFolderAlerts.vScrolling = False
        Me.listFolderAlerts.vsValue = 0
        '
        'FolderAlertTypesWin
        '
        Me.ClientSize = New System.Drawing.Size(542, 282)
        Me.Controls.Add(Me.delete)
        Me.Controls.Add(Me.modif)
        Me.Controls.Add(Me.add)
        Me.Controls.Add(Me.listFolderAlerts)
        Me.Controls.Add(Me.gbFTT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FolderAlertTypesWin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gestion des types de messages instantannés des dossiers"
        Me.gbFTT.ResumeLayout(False)
        Me.gbFTT.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AlertTitle As ManagedText
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateDebutNbPresencesX As ManagedText
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AlertNbPresencesDiff As ManagedText
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents AlertMessage As ManagedText
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
