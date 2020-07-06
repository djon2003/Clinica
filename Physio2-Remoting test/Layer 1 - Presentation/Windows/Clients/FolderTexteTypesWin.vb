Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Public Class FolderTexteTypesWin
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Dim modelesCats As Generic.List(Of ModelCategory) = ModelsManager.getInstance.getModelsCategories(New Integer() {3, 4, 5})
        Me.NoModeleCategorie.Items.AddRange(modelesCats.ToArray)

        'Chargement des images
        With DrawingManager.getInstance
            Me.add.Image = .getImage("ajouter16.gif")
            Me.delete.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.modif.Image = .getImage("modifier16.gif")
            Me.listFolderTexteTypes.icons.Add(New Icon(.getIcon("default.ico"), New Size(8, 8)))
            Me.setDefault.Image = DrawingManager.iconToImage(.getIcon("default.ico"), New Size(16, 16))
            Me.Icon = DrawingManager.imageToIcon(.getImage("codedossier.gif"))
            Me.upTextType.Image = .getImage("UpArrow.jpg")
            Me.downTextType.Image = .getImage("DownArrow.jpg")
            Me.btnSelectModelAppliedOnCreation.Image = .getImage("selection16.gif")
        End With
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbFTT As System.Windows.Forms.GroupBox
    Friend WithEvents TexteTitle As ManagedText
    Friend WithEvents WhenToBeCreated As ManagedCombo
    Friend WithEvents CopyTextToOtherText As ManagedCombo
    Friend WithEvents NbPresencesX As ManagedText
    Friend WithEvents NbDaysX As ManagedText
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents IsNbDaysDiffBefore As ManagedCombo
    Friend WithEvents NbDaysDiff As ManagedText
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents gbMultiple As System.Windows.Forms.GroupBox
    Friend WithEvents Multiple As System.Windows.Forms.CheckBox
    Friend WithEvents gbAlert As System.Windows.Forms.GroupBox
    Friend WithEvents ShowAlert As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents AlertMessageArticle As ManagedText
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents AlertNbDaysForExpiry As ManagedText
    Friend WithEvents ShowAlarm As System.Windows.Forms.CheckBox
    Friend WithEvents WhenToBeStopped As ManagedCombo
    Friend WithEvents TypeForMultiple As ManagedCombo
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents NbDaysMultiple As ManagedText
    Friend WithEvents NbMultipleEnding As ManagedText
    Friend WithEvents NbPresencesMultiple As ManagedText
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents listFolderTexteTypes As CI.Controls.List
    Public WithEvents add As System.Windows.Forms.Button
    Public WithEvents modif As System.Windows.Forms.Button
    Public WithEvents delete As System.Windows.Forms.Button
    Friend WithEvents NoModeleCategorie As ManagedCombo
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents setDefault As System.Windows.Forms.Button
    Friend WithEvents ResetTextOnCopy As System.Windows.Forms.CheckBox
    Public WithEvents upTextType As System.Windows.Forms.Button
    Public WithEvents downTextType As System.Windows.Forms.Button
    Friend WithEvents IsActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents startingExternalStatus As ManagedCombo
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents btnSelectModelAppliedOnCreation As System.Windows.Forms.Button
    Friend WithEvents modelAppliedOnCreation As ManagedText
    Friend WithEvents TerminatedOnCreation As System.Windows.Forms.CheckBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FolderTexteTypesWin))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.add = New System.Windows.Forms.Button
        Me.modif = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.setDefault = New System.Windows.Forms.Button
        Me.upTextType = New System.Windows.Forms.Button
        Me.downTextType = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.gbFTT = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.startingExternalStatus = New CI.Clinica.ManagedCombo
        Me.Label7 = New System.Windows.Forms.Label
        Me.IsActive = New System.Windows.Forms.CheckBox
        Me.ResetTextOnCopy = New System.Windows.Forms.CheckBox
        Me.btnSelectModelAppliedOnCreation = New System.Windows.Forms.Button
        Me.Multiple = New System.Windows.Forms.CheckBox
        Me.ShowAlert = New System.Windows.Forms.CheckBox
        Me.gbMultiple = New System.Windows.Forms.GroupBox
        Me.NbDaysMultiple = New CI.Base.ManagedText
        Me.WhenToBeStopped = New CI.Clinica.ManagedCombo
        Me.TypeForMultiple = New CI.Clinica.ManagedCombo
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.NbMultipleEnding = New CI.Base.ManagedText
        Me.NbPresencesMultiple = New CI.Base.ManagedText
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.gbAlert = New System.Windows.Forms.GroupBox
        Me.ShowAlarm = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.AlertMessageArticle = New CI.Base.ManagedText
        Me.Label8 = New System.Windows.Forms.Label
        Me.AlertNbDaysForExpiry = New CI.Base.ManagedText
        Me.IsNbDaysDiffBefore = New CI.Clinica.ManagedCombo
        Me.WhenToBeCreated = New CI.Clinica.ManagedCombo
        Me.CopyTextToOtherText = New CI.Clinica.ManagedCombo
        Me.NoModeleCategorie = New CI.Clinica.ManagedCombo
        Me.NbPresencesX = New CI.Base.ManagedText
        Me.NbDaysDiff = New CI.Base.ManagedText
        Me.NbDaysX = New CI.Base.ManagedText
        Me.modelAppliedOnCreation = New CI.Base.ManagedText
        Me.TexteTitle = New CI.Base.ManagedText
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.listFolderTexteTypes = New CI.Controls.List
        Me.TerminatedOnCreation = New System.Windows.Forms.CheckBox
        Me.gbFTT.SuspendLayout()
        Me.gbMultiple.SuspendLayout()
        Me.gbAlert.SuspendLayout()
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
        Me.add.Location = New System.Drawing.Point(335, 473)
        Me.add.Name = "add"
        Me.add.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.add.Size = New System.Drawing.Size(24, 24)
        Me.add.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.add, "Ajouter un type de texte")
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
        Me.modif.Location = New System.Drawing.Point(368, 473)
        Me.modif.Name = "modif"
        Me.modif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modif.Size = New System.Drawing.Size(24, 24)
        Me.modif.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.modif, "Modifier le type de texte sélectionné")
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
        Me.delete.Location = New System.Drawing.Point(401, 473)
        Me.delete.Name = "delete"
        Me.delete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer le(s) type(s) de texte sélectionné(s)")
        Me.delete.UseVisualStyleBackColor = False
        '
        'setDefault
        '
        Me.setDefault.BackColor = System.Drawing.SystemColors.Control
        Me.setDefault.Cursor = System.Windows.Forms.Cursors.Default
        Me.setDefault.Enabled = False
        Me.setDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.setDefault.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.setDefault.ForeColor = System.Drawing.SystemColors.ControlText
        Me.setDefault.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.setDefault.Location = New System.Drawing.Point(434, 473)
        Me.setDefault.Name = "setDefault"
        Me.setDefault.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.setDefault.Size = New System.Drawing.Size(24, 24)
        Me.setDefault.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.setDefault, "Changer le type de texte sélectionné pour celui par défaut")
        Me.setDefault.UseVisualStyleBackColor = False
        '
        'upTextType
        '
        Me.upTextType.BackColor = System.Drawing.SystemColors.Control
        Me.upTextType.Cursor = System.Windows.Forms.Cursors.Default
        Me.upTextType.Enabled = False
        Me.upTextType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.upTextType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.upTextType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.upTextType.Location = New System.Drawing.Point(467, 473)
        Me.upTextType.Name = "upTextType"
        Me.upTextType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.upTextType.Size = New System.Drawing.Size(24, 24)
        Me.upTextType.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.upTextType, "Monter d'une position le type de texte sélectionné")
        Me.upTextType.UseVisualStyleBackColor = False
        '
        'downTextType
        '
        Me.downTextType.BackColor = System.Drawing.SystemColors.Control
        Me.downTextType.Cursor = System.Windows.Forms.Cursors.Default
        Me.downTextType.Enabled = False
        Me.downTextType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.downTextType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.downTextType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.downTextType.Location = New System.Drawing.Point(500, 473)
        Me.downTextType.Name = "downTextType"
        Me.downTextType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.downTextType.Size = New System.Drawing.Size(24, 24)
        Me.downTextType.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.downTextType, "Descendre d'une position le type de texte sélectionné")
        Me.downTextType.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Titre :"
        '
        'gbFTT
        '
        Me.gbFTT.Controls.Add(Me.TerminatedOnCreation)
        Me.gbFTT.Controls.Add(Me.Label15)
        Me.gbFTT.Controls.Add(Me.startingExternalStatus)
        Me.gbFTT.Controls.Add(Me.Label7)
        Me.gbFTT.Controls.Add(Me.IsActive)
        Me.gbFTT.Controls.Add(Me.ResetTextOnCopy)
        Me.gbFTT.Controls.Add(Me.btnSelectModelAppliedOnCreation)
        Me.gbFTT.Controls.Add(Me.Multiple)
        Me.gbFTT.Controls.Add(Me.ShowAlert)
        Me.gbFTT.Controls.Add(Me.gbMultiple)
        Me.gbFTT.Controls.Add(Me.gbAlert)
        Me.gbFTT.Controls.Add(Me.IsNbDaysDiffBefore)
        Me.gbFTT.Controls.Add(Me.WhenToBeCreated)
        Me.gbFTT.Controls.Add(Me.CopyTextToOtherText)
        Me.gbFTT.Controls.Add(Me.NoModeleCategorie)
        Me.gbFTT.Controls.Add(Me.NbPresencesX)
        Me.gbFTT.Controls.Add(Me.NbDaysDiff)
        Me.gbFTT.Controls.Add(Me.NbDaysX)
        Me.gbFTT.Controls.Add(Me.modelAppliedOnCreation)
        Me.gbFTT.Controls.Add(Me.TexteTitle)
        Me.gbFTT.Controls.Add(Me.Label2)
        Me.gbFTT.Controls.Add(Me.Label3)
        Me.gbFTT.Controls.Add(Me.Label16)
        Me.gbFTT.Controls.Add(Me.Label5)
        Me.gbFTT.Controls.Add(Me.Label4)
        Me.gbFTT.Controls.Add(Me.Label1)
        Me.gbFTT.Controls.Add(Me.Label6)
        Me.gbFTT.Enabled = False
        Me.gbFTT.Location = New System.Drawing.Point(12, 12)
        Me.gbFTT.Name = "gbFTT"
        Me.gbFTT.Size = New System.Drawing.Size(317, 507)
        Me.gbFTT.TabIndex = 2
        Me.gbFTT.TabStop = False
        Me.gbFTT.Text = "Type de texte sélectionné"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 271)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(91, 13)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Modèle appliqué :"
        '
        'startingExternalStatus
        '
        Me.startingExternalStatus.acceptAlpha = True
        Me.startingExternalStatus.acceptedChars = Nothing
        Me.startingExternalStatus.acceptNumeric = True
        Me.startingExternalStatus.allCapital = False
        Me.startingExternalStatus.allLower = False
        Me.startingExternalStatus.allowUserToAddItem = True
        Me.startingExternalStatus.allowUserToDeleteItem = True
        Me.startingExternalStatus.autoComplete = True
        Me.startingExternalStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.startingExternalStatus.autoSizeDropDown = True
        Me.startingExternalStatus.BackColor = System.Drawing.Color.White
        Me.startingExternalStatus.blockOnMaximum = False
        Me.startingExternalStatus.blockOnMinimum = False
        Me.startingExternalStatus.cb_AcceptLeftZeros = False
        Me.startingExternalStatus.cb_AcceptNegative = False
        Me.startingExternalStatus.currencyBox = False
        Me.startingExternalStatus.dbField = Nothing
        Me.startingExternalStatus.doComboDelete = True
        Me.startingExternalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.startingExternalStatus.firstLetterCapital = False
        Me.startingExternalStatus.firstLettersCapital = False
        Me.startingExternalStatus.FormattingEnabled = True
        Me.startingExternalStatus.IntegralHeight = False
        Me.startingExternalStatus.itemsToolTipDuration = 10000
        Me.startingExternalStatus.Location = New System.Drawing.Point(91, 243)
        Me.startingExternalStatus.manageText = True
        Me.startingExternalStatus.matchExp = Nothing
        Me.startingExternalStatus.maximum = 0
        Me.startingExternalStatus.minimum = 0
        Me.startingExternalStatus.Name = "startingExternalStatus"
        Me.startingExternalStatus.nbDecimals = CType(-1, Short)
        Me.startingExternalStatus.onlyAlphabet = False
        Me.startingExternalStatus.pathOfList = ""
        Me.startingExternalStatus.ReadOnly = False
        Me.startingExternalStatus.refuseAccents = False
        Me.startingExternalStatus.refusedChars = Nothing
        Me.startingExternalStatus.showItemsToolTip = False
        Me.startingExternalStatus.Size = New System.Drawing.Size(215, 21)
        Me.startingExternalStatus.TabIndex = 7
        Me.startingExternalStatus.trimText = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 246)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Statut externe :"
        '
        'IsActive
        '
        Me.IsActive.Location = New System.Drawing.Point(102, 19)
        Me.IsActive.Name = "IsActive"
        Me.IsActive.Size = New System.Drawing.Size(151, 17)
        Me.IsActive.TabIndex = 5
        Me.IsActive.Text = "Le type de texte est actif"
        Me.IsActive.UseVisualStyleBackColor = True
        '
        'ResetTextOnCopy
        '
        Me.ResetTextOnCopy.Location = New System.Drawing.Point(66, 119)
        Me.ResetTextOnCopy.Name = "ResetTextOnCopy"
        Me.ResetTextOnCopy.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ResetTextOnCopy.Size = New System.Drawing.Size(240, 17)
        Me.ResetTextOnCopy.TabIndex = 5
        Me.ResetTextOnCopy.Text = "Remettre à zéro le texte après l'avoir copié"
        Me.ResetTextOnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ResetTextOnCopy.UseVisualStyleBackColor = True
        '
        'btnSelectModelAppliedOnCreation
        '
        Me.btnSelectModelAppliedOnCreation.BackColor = System.Drawing.SystemColors.Control
        Me.btnSelectModelAppliedOnCreation.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSelectModelAppliedOnCreation.Enabled = False
        Me.btnSelectModelAppliedOnCreation.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSelectModelAppliedOnCreation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectModelAppliedOnCreation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSelectModelAppliedOnCreation.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnSelectModelAppliedOnCreation.Location = New System.Drawing.Point(102, 266)
        Me.btnSelectModelAppliedOnCreation.Name = "btnSelectModelAppliedOnCreation"
        Me.btnSelectModelAppliedOnCreation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSelectModelAppliedOnCreation.Size = New System.Drawing.Size(24, 24)
        Me.btnSelectModelAppliedOnCreation.TabIndex = 18
        Me.btnSelectModelAppliedOnCreation.UseVisualStyleBackColor = False
        '
        'Multiple
        '
        Me.Multiple.AutoSize = True
        Me.Multiple.Location = New System.Drawing.Point(6, 387)
        Me.Multiple.Name = "Multiple"
        Me.Multiple.Size = New System.Drawing.Size(15, 14)
        Me.Multiple.TabIndex = 5
        Me.Multiple.UseVisualStyleBackColor = True
        '
        'ShowAlert
        '
        Me.ShowAlert.AutoSize = True
        Me.ShowAlert.Location = New System.Drawing.Point(6, 294)
        Me.ShowAlert.Name = "ShowAlert"
        Me.ShowAlert.Size = New System.Drawing.Size(15, 14)
        Me.ShowAlert.TabIndex = 5
        Me.ShowAlert.UseVisualStyleBackColor = True
        '
        'gbMultiple
        '
        Me.gbMultiple.BackColor = System.Drawing.Color.Transparent
        Me.gbMultiple.Controls.Add(Me.NbDaysMultiple)
        Me.gbMultiple.Controls.Add(Me.WhenToBeStopped)
        Me.gbMultiple.Controls.Add(Me.TypeForMultiple)
        Me.gbMultiple.Controls.Add(Me.Label14)
        Me.gbMultiple.Controls.Add(Me.Label10)
        Me.gbMultiple.Controls.Add(Me.Label11)
        Me.gbMultiple.Controls.Add(Me.NbMultipleEnding)
        Me.gbMultiple.Controls.Add(Me.NbPresencesMultiple)
        Me.gbMultiple.Controls.Add(Me.Label13)
        Me.gbMultiple.Controls.Add(Me.Label12)
        Me.gbMultiple.Enabled = False
        Me.gbMultiple.Location = New System.Drawing.Point(0, 388)
        Me.gbMultiple.Name = "gbMultiple"
        Me.gbMultiple.Size = New System.Drawing.Size(317, 119)
        Me.gbMultiple.TabIndex = 4
        Me.gbMultiple.TabStop = False
        Me.gbMultiple.Text = "M Multiplier le texte"
        '
        'NbDaysMultiple
        '
        Me.NbDaysMultiple.acceptAlpha = False
        Me.NbDaysMultiple.acceptedChars = ",§."
        Me.NbDaysMultiple.acceptNumeric = True
        Me.NbDaysMultiple.allCapital = False
        Me.NbDaysMultiple.allLower = False
        Me.NbDaysMultiple.blockOnMaximum = False
        Me.NbDaysMultiple.blockOnMinimum = True
        Me.NbDaysMultiple.cb_AcceptLeftZeros = False
        Me.NbDaysMultiple.cb_AcceptNegative = False
        Me.NbDaysMultiple.currencyBox = True
        Me.NbDaysMultiple.firstLetterCapital = False
        Me.NbDaysMultiple.firstLettersCapital = False
        Me.NbDaysMultiple.Location = New System.Drawing.Point(103, 68)
        Me.NbDaysMultiple.manageText = True
        Me.NbDaysMultiple.matchExp = ""
        Me.NbDaysMultiple.maximum = 0
        Me.NbDaysMultiple.minimum = 1
        Me.NbDaysMultiple.Name = "NbDaysMultiple"
        Me.NbDaysMultiple.nbDecimals = CType(0, Short)
        Me.NbDaysMultiple.onlyAlphabet = False
        Me.NbDaysMultiple.refuseAccents = False
        Me.NbDaysMultiple.refusedChars = ""
        Me.NbDaysMultiple.showInternalContextMenu = True
        Me.NbDaysMultiple.Size = New System.Drawing.Size(28, 20)
        Me.NbDaysMultiple.TabIndex = 2
        Me.NbDaysMultiple.Text = "1"
        Me.NbDaysMultiple.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NbDaysMultiple.trimText = False
        '
        'WhenToBeStopped
        '
        Me.WhenToBeStopped.acceptAlpha = True
        Me.WhenToBeStopped.acceptedChars = Nothing
        Me.WhenToBeStopped.acceptNumeric = True
        Me.WhenToBeStopped.allCapital = False
        Me.WhenToBeStopped.allLower = False
        Me.WhenToBeStopped.allowUserToAddItem = True
        Me.WhenToBeStopped.allowUserToDeleteItem = True
        Me.WhenToBeStopped.autoComplete = True
        Me.WhenToBeStopped.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.WhenToBeStopped.autoSizeDropDown = True
        Me.WhenToBeStopped.BackColor = System.Drawing.Color.White
        Me.WhenToBeStopped.blockOnMaximum = False
        Me.WhenToBeStopped.blockOnMinimum = False
        Me.WhenToBeStopped.cb_AcceptLeftZeros = False
        Me.WhenToBeStopped.cb_AcceptNegative = False
        Me.WhenToBeStopped.currencyBox = False
        Me.WhenToBeStopped.dbField = Nothing
        Me.WhenToBeStopped.doComboDelete = True
        Me.WhenToBeStopped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WhenToBeStopped.firstLetterCapital = False
        Me.WhenToBeStopped.firstLettersCapital = False
        Me.WhenToBeStopped.FormattingEnabled = True
        Me.WhenToBeStopped.IntegralHeight = False
        Me.WhenToBeStopped.Items.AddRange(New Object() {"À la fermeture du dossier", "Au nombre de multiple maximal"})
        Me.WhenToBeStopped.itemsToolTipDuration = 10000
        Me.WhenToBeStopped.Location = New System.Drawing.Point(119, 43)
        Me.WhenToBeStopped.manageText = True
        Me.WhenToBeStopped.matchExp = Nothing
        Me.WhenToBeStopped.maximum = 0
        Me.WhenToBeStopped.minimum = 0
        Me.WhenToBeStopped.Name = "WhenToBeStopped"
        Me.WhenToBeStopped.nbDecimals = CType(-1, Short)
        Me.WhenToBeStopped.onlyAlphabet = False
        Me.WhenToBeStopped.pathOfList = ""
        Me.WhenToBeStopped.ReadOnly = False
        Me.WhenToBeStopped.refuseAccents = False
        Me.WhenToBeStopped.refusedChars = Nothing
        Me.WhenToBeStopped.showItemsToolTip = False
        Me.WhenToBeStopped.Size = New System.Drawing.Size(187, 21)
        Me.WhenToBeStopped.TabIndex = 3
        Me.WhenToBeStopped.trimText = False
        '
        'TypeForMultiple
        '
        Me.TypeForMultiple.acceptAlpha = True
        Me.TypeForMultiple.acceptedChars = Nothing
        Me.TypeForMultiple.acceptNumeric = True
        Me.TypeForMultiple.allCapital = False
        Me.TypeForMultiple.allLower = False
        Me.TypeForMultiple.allowUserToAddItem = True
        Me.TypeForMultiple.allowUserToDeleteItem = True
        Me.TypeForMultiple.autoComplete = True
        Me.TypeForMultiple.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TypeForMultiple.autoSizeDropDown = True
        Me.TypeForMultiple.BackColor = System.Drawing.Color.White
        Me.TypeForMultiple.blockOnMaximum = False
        Me.TypeForMultiple.blockOnMinimum = False
        Me.TypeForMultiple.cb_AcceptLeftZeros = False
        Me.TypeForMultiple.cb_AcceptNegative = False
        Me.TypeForMultiple.currencyBox = False
        Me.TypeForMultiple.dbField = Nothing
        Me.TypeForMultiple.doComboDelete = True
        Me.TypeForMultiple.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeForMultiple.firstLetterCapital = False
        Me.TypeForMultiple.firstLettersCapital = False
        Me.TypeForMultiple.FormattingEnabled = True
        Me.TypeForMultiple.IntegralHeight = False
        Me.TypeForMultiple.Items.AddRange(New Object() {"Nombre de jour X", "Nombre de présence X", "Lorsque le texte est terminé"})
        Me.TypeForMultiple.itemsToolTipDuration = 10000
        Me.TypeForMultiple.Location = New System.Drawing.Point(119, 20)
        Me.TypeForMultiple.manageText = True
        Me.TypeForMultiple.matchExp = Nothing
        Me.TypeForMultiple.maximum = 0
        Me.TypeForMultiple.minimum = 0
        Me.TypeForMultiple.Name = "TypeForMultiple"
        Me.TypeForMultiple.nbDecimals = CType(-1, Short)
        Me.TypeForMultiple.onlyAlphabet = False
        Me.TypeForMultiple.pathOfList = ""
        Me.TypeForMultiple.ReadOnly = False
        Me.TypeForMultiple.refuseAccents = False
        Me.TypeForMultiple.refusedChars = Nothing
        Me.TypeForMultiple.showItemsToolTip = False
        Me.TypeForMultiple.Size = New System.Drawing.Size(187, 21)
        Me.TypeForMultiple.TabIndex = 3
        Me.TypeForMultiple.trimText = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 46)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Moment de fin :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Moment de création :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Nombre de jour X :"
        '
        'NbMultipleEnding
        '
        Me.NbMultipleEnding.acceptAlpha = False
        Me.NbMultipleEnding.acceptedChars = ",§."
        Me.NbMultipleEnding.acceptNumeric = True
        Me.NbMultipleEnding.allCapital = False
        Me.NbMultipleEnding.allLower = False
        Me.NbMultipleEnding.blockOnMaximum = False
        Me.NbMultipleEnding.blockOnMinimum = True
        Me.NbMultipleEnding.cb_AcceptLeftZeros = False
        Me.NbMultipleEnding.cb_AcceptNegative = False
        Me.NbMultipleEnding.currencyBox = True
        Me.NbMultipleEnding.Enabled = False
        Me.NbMultipleEnding.firstLetterCapital = False
        Me.NbMultipleEnding.firstLettersCapital = False
        Me.NbMultipleEnding.Location = New System.Drawing.Point(155, 89)
        Me.NbMultipleEnding.manageText = True
        Me.NbMultipleEnding.matchExp = ""
        Me.NbMultipleEnding.maximum = 0
        Me.NbMultipleEnding.minimum = 2
        Me.NbMultipleEnding.Name = "NbMultipleEnding"
        Me.NbMultipleEnding.nbDecimals = CType(0, Short)
        Me.NbMultipleEnding.onlyAlphabet = False
        Me.NbMultipleEnding.refuseAccents = False
        Me.NbMultipleEnding.refusedChars = ""
        Me.NbMultipleEnding.showInternalContextMenu = True
        Me.NbMultipleEnding.Size = New System.Drawing.Size(28, 20)
        Me.NbMultipleEnding.TabIndex = 2
        Me.NbMultipleEnding.Text = "2"
        Me.NbMultipleEnding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NbMultipleEnding.trimText = False
        '
        'NbPresencesMultiple
        '
        Me.NbPresencesMultiple.acceptAlpha = False
        Me.NbPresencesMultiple.acceptedChars = ",§."
        Me.NbPresencesMultiple.acceptNumeric = True
        Me.NbPresencesMultiple.allCapital = False
        Me.NbPresencesMultiple.allLower = False
        Me.NbPresencesMultiple.blockOnMaximum = False
        Me.NbPresencesMultiple.blockOnMinimum = True
        Me.NbPresencesMultiple.cb_AcceptLeftZeros = False
        Me.NbPresencesMultiple.cb_AcceptNegative = False
        Me.NbPresencesMultiple.currencyBox = True
        Me.NbPresencesMultiple.Enabled = False
        Me.NbPresencesMultiple.firstLetterCapital = False
        Me.NbPresencesMultiple.firstLettersCapital = False
        Me.NbPresencesMultiple.Location = New System.Drawing.Point(278, 68)
        Me.NbPresencesMultiple.manageText = True
        Me.NbPresencesMultiple.matchExp = ""
        Me.NbPresencesMultiple.maximum = 0
        Me.NbPresencesMultiple.minimum = 1
        Me.NbPresencesMultiple.Name = "NbPresencesMultiple"
        Me.NbPresencesMultiple.nbDecimals = CType(0, Short)
        Me.NbPresencesMultiple.onlyAlphabet = False
        Me.NbPresencesMultiple.refuseAccents = False
        Me.NbPresencesMultiple.refusedChars = ""
        Me.NbPresencesMultiple.showInternalContextMenu = True
        Me.NbPresencesMultiple.Size = New System.Drawing.Size(28, 20)
        Me.NbPresencesMultiple.TabIndex = 2
        Me.NbPresencesMultiple.Text = "1"
        Me.NbPresencesMultiple.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NbPresencesMultiple.trimText = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 92)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(143, 13)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Nombre de multiple maximal :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(153, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(122, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Nombre de présence X :"
        '
        'gbAlert
        '
        Me.gbAlert.BackColor = System.Drawing.Color.Transparent
        Me.gbAlert.Controls.Add(Me.ShowAlarm)
        Me.gbAlert.Controls.Add(Me.Label9)
        Me.gbAlert.Controls.Add(Me.AlertMessageArticle)
        Me.gbAlert.Controls.Add(Me.Label8)
        Me.gbAlert.Controls.Add(Me.AlertNbDaysForExpiry)
        Me.gbAlert.Enabled = False
        Me.gbAlert.Location = New System.Drawing.Point(0, 295)
        Me.gbAlert.Name = "gbAlert"
        Me.gbAlert.Size = New System.Drawing.Size(317, 119)
        Me.gbAlert.TabIndex = 4
        Me.gbAlert.TabStop = False
        Me.gbAlert.Text = "M Message instantanné"
        '
        'ShowAlarm
        '
        Me.ShowAlarm.Location = New System.Drawing.Point(6, 19)
        Me.ShowAlarm.Name = "ShowAlarm"
        Me.ShowAlarm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ShowAlarm.Size = New System.Drawing.Size(300, 17)
        Me.ShowAlarm.TabIndex = 5
        Me.ShowAlarm.Text = "Utiliser l'alarme en plus du message instantanné"
        Me.ShowAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ShowAlarm.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 73)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Article devant le titre :"
        '
        'AlertMessageArticle
        '
        Me.AlertMessageArticle.acceptAlpha = True
        Me.AlertMessageArticle.acceptedChars = ""
        Me.AlertMessageArticle.acceptNumeric = True
        Me.AlertMessageArticle.allCapital = False
        Me.AlertMessageArticle.allLower = False
        Me.AlertMessageArticle.blockOnMaximum = False
        Me.AlertMessageArticle.blockOnMinimum = False
        Me.AlertMessageArticle.cb_AcceptLeftZeros = False
        Me.AlertMessageArticle.cb_AcceptNegative = False
        Me.AlertMessageArticle.currencyBox = False
        Me.AlertMessageArticle.firstLetterCapital = False
        Me.AlertMessageArticle.firstLettersCapital = False
        Me.AlertMessageArticle.Location = New System.Drawing.Point(278, 70)
        Me.AlertMessageArticle.manageText = True
        Me.AlertMessageArticle.matchExp = ""
        Me.AlertMessageArticle.maximum = 0
        Me.AlertMessageArticle.minimum = 0
        Me.AlertMessageArticle.Name = "AlertMessageArticle"
        Me.AlertMessageArticle.nbDecimals = CType(0, Short)
        Me.AlertMessageArticle.onlyAlphabet = True
        Me.AlertMessageArticle.refuseAccents = True
        Me.AlertMessageArticle.refusedChars = ""
        Me.AlertMessageArticle.showInternalContextMenu = True
        Me.AlertMessageArticle.Size = New System.Drawing.Size(28, 20)
        Me.AlertMessageArticle.TabIndex = 2
        Me.AlertMessageArticle.Text = "Le"
        Me.AlertMessageArticle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.AlertMessageArticle.trimText = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 45)
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
        Me.AlertNbDaysForExpiry.Location = New System.Drawing.Point(278, 42)
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
        'IsNbDaysDiffBefore
        '
        Me.IsNbDaysDiffBefore.acceptAlpha = True
        Me.IsNbDaysDiffBefore.acceptedChars = Nothing
        Me.IsNbDaysDiffBefore.acceptNumeric = True
        Me.IsNbDaysDiffBefore.allCapital = False
        Me.IsNbDaysDiffBefore.allLower = False
        Me.IsNbDaysDiffBefore.allowUserToAddItem = True
        Me.IsNbDaysDiffBefore.allowUserToDeleteItem = True
        Me.IsNbDaysDiffBefore.autoComplete = True
        Me.IsNbDaysDiffBefore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.IsNbDaysDiffBefore.autoSizeDropDown = True
        Me.IsNbDaysDiffBefore.BackColor = System.Drawing.Color.White
        Me.IsNbDaysDiffBefore.blockOnMaximum = False
        Me.IsNbDaysDiffBefore.blockOnMinimum = False
        Me.IsNbDaysDiffBefore.cb_AcceptLeftZeros = False
        Me.IsNbDaysDiffBefore.cb_AcceptNegative = False
        Me.IsNbDaysDiffBefore.currencyBox = False
        Me.IsNbDaysDiffBefore.dbField = Nothing
        Me.IsNbDaysDiffBefore.doComboDelete = True
        Me.IsNbDaysDiffBefore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IsNbDaysDiffBefore.firstLetterCapital = False
        Me.IsNbDaysDiffBefore.firstLettersCapital = False
        Me.IsNbDaysDiffBefore.FormattingEnabled = True
        Me.IsNbDaysDiffBefore.IntegralHeight = False
        Me.IsNbDaysDiffBefore.Items.AddRange(New Object() {"Avant", "Après"})
        Me.IsNbDaysDiffBefore.itemsToolTipDuration = 10000
        Me.IsNbDaysDiffBefore.Location = New System.Drawing.Point(102, 139)
        Me.IsNbDaysDiffBefore.manageText = True
        Me.IsNbDaysDiffBefore.matchExp = Nothing
        Me.IsNbDaysDiffBefore.maximum = 0
        Me.IsNbDaysDiffBefore.minimum = 0
        Me.IsNbDaysDiffBefore.Name = "IsNbDaysDiffBefore"
        Me.IsNbDaysDiffBefore.nbDecimals = CType(-1, Short)
        Me.IsNbDaysDiffBefore.onlyAlphabet = False
        Me.IsNbDaysDiffBefore.pathOfList = ""
        Me.IsNbDaysDiffBefore.ReadOnly = False
        Me.IsNbDaysDiffBefore.refuseAccents = False
        Me.IsNbDaysDiffBefore.refusedChars = Nothing
        Me.IsNbDaysDiffBefore.showItemsToolTip = False
        Me.IsNbDaysDiffBefore.Size = New System.Drawing.Size(58, 21)
        Me.IsNbDaysDiffBefore.TabIndex = 3
        Me.IsNbDaysDiffBefore.trimText = False
        '
        'WhenToBeCreated
        '
        Me.WhenToBeCreated.acceptAlpha = True
        Me.WhenToBeCreated.acceptedChars = Nothing
        Me.WhenToBeCreated.acceptNumeric = True
        Me.WhenToBeCreated.allCapital = False
        Me.WhenToBeCreated.allLower = False
        Me.WhenToBeCreated.allowUserToAddItem = True
        Me.WhenToBeCreated.allowUserToDeleteItem = True
        Me.WhenToBeCreated.autoComplete = True
        Me.WhenToBeCreated.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.WhenToBeCreated.autoSizeDropDown = True
        Me.WhenToBeCreated.BackColor = System.Drawing.Color.White
        Me.WhenToBeCreated.blockOnMaximum = False
        Me.WhenToBeCreated.blockOnMinimum = False
        Me.WhenToBeCreated.cb_AcceptLeftZeros = False
        Me.WhenToBeCreated.cb_AcceptNegative = False
        Me.WhenToBeCreated.currencyBox = False
        Me.WhenToBeCreated.dbField = Nothing
        Me.WhenToBeCreated.doComboDelete = True
        Me.WhenToBeCreated.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WhenToBeCreated.firstLetterCapital = False
        Me.WhenToBeCreated.firstLettersCapital = False
        Me.WhenToBeCreated.FormattingEnabled = True
        Me.WhenToBeCreated.IntegralHeight = False
        Me.WhenToBeCreated.Items.AddRange(New Object() {"À la création du dossier", "Au jour X après la création", "À la présence X", "À la fermeture du dossier"})
        Me.WhenToBeCreated.itemsToolTipDuration = 10000
        Me.WhenToBeCreated.Location = New System.Drawing.Point(119, 169)
        Me.WhenToBeCreated.manageText = True
        Me.WhenToBeCreated.matchExp = Nothing
        Me.WhenToBeCreated.maximum = 0
        Me.WhenToBeCreated.minimum = 0
        Me.WhenToBeCreated.Name = "WhenToBeCreated"
        Me.WhenToBeCreated.nbDecimals = CType(-1, Short)
        Me.WhenToBeCreated.onlyAlphabet = False
        Me.WhenToBeCreated.pathOfList = ""
        Me.WhenToBeCreated.ReadOnly = False
        Me.WhenToBeCreated.refuseAccents = False
        Me.WhenToBeCreated.refusedChars = Nothing
        Me.WhenToBeCreated.showItemsToolTip = False
        Me.WhenToBeCreated.Size = New System.Drawing.Size(187, 21)
        Me.WhenToBeCreated.TabIndex = 3
        Me.WhenToBeCreated.trimText = False
        '
        'CopyTextToOtherText
        '
        Me.CopyTextToOtherText.acceptAlpha = True
        Me.CopyTextToOtherText.acceptedChars = Nothing
        Me.CopyTextToOtherText.acceptNumeric = True
        Me.CopyTextToOtherText.allCapital = False
        Me.CopyTextToOtherText.allLower = False
        Me.CopyTextToOtherText.allowUserToAddItem = True
        Me.CopyTextToOtherText.allowUserToDeleteItem = True
        Me.CopyTextToOtherText.autoComplete = True
        Me.CopyTextToOtherText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CopyTextToOtherText.autoSizeDropDown = True
        Me.CopyTextToOtherText.BackColor = System.Drawing.Color.White
        Me.CopyTextToOtherText.blockOnMaximum = False
        Me.CopyTextToOtherText.blockOnMinimum = False
        Me.CopyTextToOtherText.cb_AcceptLeftZeros = False
        Me.CopyTextToOtherText.cb_AcceptNegative = False
        Me.CopyTextToOtherText.currencyBox = False
        Me.CopyTextToOtherText.dbField = Nothing
        Me.CopyTextToOtherText.doComboDelete = True
        Me.CopyTextToOtherText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CopyTextToOtherText.firstLetterCapital = False
        Me.CopyTextToOtherText.firstLettersCapital = False
        Me.CopyTextToOtherText.FormattingEnabled = True
        Me.CopyTextToOtherText.IntegralHeight = False
        Me.CopyTextToOtherText.itemsToolTipDuration = 10000
        Me.CopyTextToOtherText.Location = New System.Drawing.Point(70, 92)
        Me.CopyTextToOtherText.manageText = True
        Me.CopyTextToOtherText.matchExp = Nothing
        Me.CopyTextToOtherText.maximum = 0
        Me.CopyTextToOtherText.minimum = 0
        Me.CopyTextToOtherText.Name = "CopyTextToOtherText"
        Me.CopyTextToOtherText.nbDecimals = CType(-1, Short)
        Me.CopyTextToOtherText.onlyAlphabet = False
        Me.CopyTextToOtherText.pathOfList = ""
        Me.CopyTextToOtherText.ReadOnly = False
        Me.CopyTextToOtherText.refuseAccents = False
        Me.CopyTextToOtherText.refusedChars = Nothing
        Me.CopyTextToOtherText.showItemsToolTip = False
        Me.CopyTextToOtherText.Size = New System.Drawing.Size(236, 21)
        Me.CopyTextToOtherText.TabIndex = 3
        Me.CopyTextToOtherText.trimText = False
        '
        'NoModeleCategorie
        '
        Me.NoModeleCategorie.acceptAlpha = True
        Me.NoModeleCategorie.acceptedChars = Nothing
        Me.NoModeleCategorie.acceptNumeric = True
        Me.NoModeleCategorie.allCapital = False
        Me.NoModeleCategorie.allLower = False
        Me.NoModeleCategorie.allowUserToAddItem = True
        Me.NoModeleCategorie.allowUserToDeleteItem = True
        Me.NoModeleCategorie.autoComplete = True
        Me.NoModeleCategorie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.NoModeleCategorie.autoSizeDropDown = True
        Me.NoModeleCategorie.BackColor = System.Drawing.Color.White
        Me.NoModeleCategorie.blockOnMaximum = False
        Me.NoModeleCategorie.blockOnMinimum = False
        Me.NoModeleCategorie.cb_AcceptLeftZeros = False
        Me.NoModeleCategorie.cb_AcceptNegative = False
        Me.NoModeleCategorie.currencyBox = False
        Me.NoModeleCategorie.dbField = Nothing
        Me.NoModeleCategorie.doComboDelete = True
        Me.NoModeleCategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.NoModeleCategorie.firstLetterCapital = False
        Me.NoModeleCategorie.firstLettersCapital = False
        Me.NoModeleCategorie.FormattingEnabled = True
        Me.NoModeleCategorie.IntegralHeight = False
        Me.NoModeleCategorie.itemsToolTipDuration = 10000
        Me.NoModeleCategorie.Location = New System.Drawing.Point(70, 65)
        Me.NoModeleCategorie.manageText = True
        Me.NoModeleCategorie.matchExp = Nothing
        Me.NoModeleCategorie.maximum = 0
        Me.NoModeleCategorie.minimum = 0
        Me.NoModeleCategorie.Name = "NoModeleCategorie"
        Me.NoModeleCategorie.nbDecimals = CType(-1, Short)
        Me.NoModeleCategorie.onlyAlphabet = False
        Me.NoModeleCategorie.pathOfList = ""
        Me.NoModeleCategorie.ReadOnly = False
        Me.NoModeleCategorie.refuseAccents = False
        Me.NoModeleCategorie.refusedChars = Nothing
        Me.NoModeleCategorie.showItemsToolTip = False
        Me.NoModeleCategorie.Size = New System.Drawing.Size(236, 21)
        Me.NoModeleCategorie.TabIndex = 3
        Me.NoModeleCategorie.trimText = False
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
        Me.NbPresencesX.Enabled = False
        Me.NbPresencesX.firstLetterCapital = False
        Me.NbPresencesX.firstLettersCapital = False
        Me.NbPresencesX.Location = New System.Drawing.Point(278, 217)
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
        'NbDaysDiff
        '
        Me.NbDaysDiff.acceptAlpha = False
        Me.NbDaysDiff.acceptedChars = ",§."
        Me.NbDaysDiff.acceptNumeric = True
        Me.NbDaysDiff.allCapital = False
        Me.NbDaysDiff.allLower = False
        Me.NbDaysDiff.blockOnMaximum = False
        Me.NbDaysDiff.blockOnMinimum = False
        Me.NbDaysDiff.cb_AcceptLeftZeros = False
        Me.NbDaysDiff.cb_AcceptNegative = False
        Me.NbDaysDiff.currencyBox = True
        Me.NbDaysDiff.firstLetterCapital = False
        Me.NbDaysDiff.firstLettersCapital = False
        Me.NbDaysDiff.Location = New System.Drawing.Point(48, 139)
        Me.NbDaysDiff.manageText = True
        Me.NbDaysDiff.matchExp = ""
        Me.NbDaysDiff.maximum = 0
        Me.NbDaysDiff.minimum = 0
        Me.NbDaysDiff.Name = "NbDaysDiff"
        Me.NbDaysDiff.nbDecimals = CType(0, Short)
        Me.NbDaysDiff.onlyAlphabet = False
        Me.NbDaysDiff.refuseAccents = False
        Me.NbDaysDiff.refusedChars = ""
        Me.NbDaysDiff.showInternalContextMenu = True
        Me.NbDaysDiff.Size = New System.Drawing.Size(30, 20)
        Me.NbDaysDiff.TabIndex = 2
        Me.NbDaysDiff.Text = "0"
        Me.NbDaysDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NbDaysDiff.trimText = False
        '
        'NbDaysX
        '
        Me.NbDaysX.acceptAlpha = False
        Me.NbDaysX.acceptedChars = ",§."
        Me.NbDaysX.acceptNumeric = True
        Me.NbDaysX.allCapital = False
        Me.NbDaysX.allLower = False
        Me.NbDaysX.blockOnMaximum = False
        Me.NbDaysX.blockOnMinimum = False
        Me.NbDaysX.cb_AcceptLeftZeros = False
        Me.NbDaysX.cb_AcceptNegative = False
        Me.NbDaysX.currencyBox = True
        Me.NbDaysX.firstLetterCapital = False
        Me.NbDaysX.firstLettersCapital = False
        Me.NbDaysX.Location = New System.Drawing.Point(103, 215)
        Me.NbDaysX.manageText = True
        Me.NbDaysX.matchExp = ""
        Me.NbDaysX.maximum = 0
        Me.NbDaysX.minimum = 0
        Me.NbDaysX.Name = "NbDaysX"
        Me.NbDaysX.nbDecimals = CType(0, Short)
        Me.NbDaysX.onlyAlphabet = False
        Me.NbDaysX.refuseAccents = False
        Me.NbDaysX.refusedChars = ""
        Me.NbDaysX.showInternalContextMenu = True
        Me.NbDaysX.Size = New System.Drawing.Size(28, 20)
        Me.NbDaysX.TabIndex = 2
        Me.NbDaysX.Text = "0"
        Me.NbDaysX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NbDaysX.trimText = False
        '
        'modelAppliedOnCreation
        '
        Me.modelAppliedOnCreation.acceptAlpha = True
        Me.modelAppliedOnCreation.acceptedChars = ""
        Me.modelAppliedOnCreation.acceptNumeric = True
        Me.modelAppliedOnCreation.allCapital = False
        Me.modelAppliedOnCreation.allLower = False
        Me.modelAppliedOnCreation.BackColor = System.Drawing.SystemColors.ControlLight
        Me.modelAppliedOnCreation.blockOnMaximum = False
        Me.modelAppliedOnCreation.blockOnMinimum = False
        Me.modelAppliedOnCreation.cb_AcceptLeftZeros = False
        Me.modelAppliedOnCreation.cb_AcceptNegative = False
        Me.modelAppliedOnCreation.currencyBox = False
        Me.modelAppliedOnCreation.firstLetterCapital = False
        Me.modelAppliedOnCreation.firstLettersCapital = False
        Me.modelAppliedOnCreation.Location = New System.Drawing.Point(132, 268)
        Me.modelAppliedOnCreation.manageText = True
        Me.modelAppliedOnCreation.matchExp = ""
        Me.modelAppliedOnCreation.maximum = 0
        Me.modelAppliedOnCreation.minimum = 0
        Me.modelAppliedOnCreation.Name = "modelAppliedOnCreation"
        Me.modelAppliedOnCreation.nbDecimals = CType(-1, Short)
        Me.modelAppliedOnCreation.onlyAlphabet = False
        Me.modelAppliedOnCreation.ReadOnly = True
        Me.modelAppliedOnCreation.refuseAccents = False
        Me.modelAppliedOnCreation.refusedChars = ""
        Me.modelAppliedOnCreation.showInternalContextMenu = True
        Me.modelAppliedOnCreation.Size = New System.Drawing.Size(174, 20)
        Me.modelAppliedOnCreation.TabIndex = 2
        Me.modelAppliedOnCreation.trimText = False
        '
        'TexteTitle
        '
        Me.TexteTitle.acceptAlpha = True
        Me.TexteTitle.acceptedChars = ""
        Me.TexteTitle.acceptNumeric = True
        Me.TexteTitle.allCapital = False
        Me.TexteTitle.allLower = False
        Me.TexteTitle.blockOnMaximum = False
        Me.TexteTitle.blockOnMinimum = False
        Me.TexteTitle.cb_AcceptLeftZeros = False
        Me.TexteTitle.cb_AcceptNegative = False
        Me.TexteTitle.currencyBox = False
        Me.TexteTitle.firstLetterCapital = False
        Me.TexteTitle.firstLettersCapital = False
        Me.TexteTitle.Location = New System.Drawing.Point(46, 39)
        Me.TexteTitle.manageText = True
        Me.TexteTitle.matchExp = ""
        Me.TexteTitle.maximum = 0
        Me.TexteTitle.minimum = 0
        Me.TexteTitle.Name = "TexteTitle"
        Me.TexteTitle.nbDecimals = CType(-1, Short)
        Me.TexteTitle.onlyAlphabet = False
        Me.TexteTitle.refuseAccents = False
        Me.TexteTitle.refusedChars = ""
        Me.TexteTitle.showInternalContextMenu = True
        Me.TexteTitle.Size = New System.Drawing.Size(260, 20)
        Me.TexteTitle.TabIndex = 2
        Me.TexteTitle.trimText = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Copié vers :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Moment de création :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 68)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(58, 13)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "Catégorie :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(153, 218)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Nombre de présence X :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 218)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Nombre de jour X :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(6, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(294, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Afficher            jour                      la date de création du texte."
        '
        'listFolderTexteTypes
        '
        Me.listFolderTexteTypes.autoAdjust = True
        Me.listFolderTexteTypes.autoKeyDownSelection = True
        Me.listFolderTexteTypes.autoSizeHorizontally = False
        Me.listFolderTexteTypes.autoSizeVertically = False
        Me.listFolderTexteTypes.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.listFolderTexteTypes.baseBackColor = System.Drawing.Color.White
        Me.listFolderTexteTypes.baseFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.listFolderTexteTypes.baseForeColor = System.Drawing.Color.Black
        Me.listFolderTexteTypes.bgColor = System.Drawing.SystemColors.Control
        Me.listFolderTexteTypes.borderColor = System.Drawing.Color.Empty
        Me.listFolderTexteTypes.borderSelColor = System.Drawing.Color.Empty
        Me.listFolderTexteTypes.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.listFolderTexteTypes.CausesValidation = False
        Me.listFolderTexteTypes.clickEnabled = False
        Me.listFolderTexteTypes.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.listFolderTexteTypes.do3D = False
        Me.listFolderTexteTypes.draw = False
        Me.listFolderTexteTypes.extraWidth = 0
        Me.listFolderTexteTypes.hScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.listFolderTexteTypes.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listFolderTexteTypes.hScrolling = False
        Me.listFolderTexteTypes.hsValue = 0
        Me.listFolderTexteTypes.icons = CType(resources.GetObject("listFolderTexteTypes.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.listFolderTexteTypes.itemBorder = 0
        Me.listFolderTexteTypes.itemMargin = 0
        Me.listFolderTexteTypes.items = CType(resources.GetObject("listFolderTexteTypes.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.listFolderTexteTypes.Location = New System.Drawing.Point(335, 19)
        Me.listFolderTexteTypes.mouseMove3D = False
        Me.listFolderTexteTypes.mouseSpeed = 0
        Me.listFolderTexteTypes.Name = "listFolderTexteTypes"
        Me.listFolderTexteTypes.objMaxHeight = 0.0!
        Me.listFolderTexteTypes.objMaxWidth = 0.0!
        Me.listFolderTexteTypes.objMinHeight = 0.0!
        Me.listFolderTexteTypes.objMinWidth = 0.0!
        Me.listFolderTexteTypes.reverseSorting = False
        Me.listFolderTexteTypes.selected = -1
        Me.listFolderTexteTypes.selectedClickAllowed = False
        Me.listFolderTexteTypes.selectMultiple = True
        Me.listFolderTexteTypes.Size = New System.Drawing.Size(192, 448)
        Me.listFolderTexteTypes.sorted = False
        Me.listFolderTexteTypes.TabIndex = 3
        Me.listFolderTexteTypes.toolTipText = Nothing
        Me.listFolderTexteTypes.vScrollColor = System.Drawing.SystemColors.ScrollBar
        Me.listFolderTexteTypes.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listFolderTexteTypes.vScrolling = False
        Me.listFolderTexteTypes.vsValue = 0
        '
        'TerminatedOnCreation
        '
        Me.TerminatedOnCreation.AutoSize = True
        Me.TerminatedOnCreation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TerminatedOnCreation.Location = New System.Drawing.Point(6, 195)
        Me.TerminatedOnCreation.Name = "TerminatedOnCreation"
        Me.TerminatedOnCreation.Size = New System.Drawing.Size(211, 17)
        Me.TerminatedOnCreation.TabIndex = 19
        Me.TerminatedOnCreation.Text = "Marqué le texte terminé dès sa création"
        Me.TerminatedOnCreation.UseVisualStyleBackColor = True
        '
        'FolderTexteTypesWin
        '
        Me.ClientSize = New System.Drawing.Size(541, 520)
        Me.Controls.Add(Me.downTextType)
        Me.Controls.Add(Me.upTextType)
        Me.Controls.Add(Me.setDefault)
        Me.Controls.Add(Me.delete)
        Me.Controls.Add(Me.modif)
        Me.Controls.Add(Me.add)
        Me.Controls.Add(Me.listFolderTexteTypes)
        Me.Controls.Add(Me.gbFTT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FolderTexteTypesWin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gestion des types de texte des dossiers"
        Me.gbFTT.ResumeLayout(False)
        Me.gbFTT.PerformLayout()
        Me.gbMultiple.ResumeLayout(False)
        Me.gbMultiple.PerformLayout()
        Me.gbAlert.ResumeLayout(False)
        Me.gbAlert.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private curData As DataTable
    Private oldTypeForMultiple As Integer = 0
    Private loadingFTT As Boolean = False
    Private formModified As Boolean = False
    Private oneTexteModified As Boolean = False
    Private _allowModification As Boolean = False

    Private Property allowModification() As Boolean
        Get
            Return _allowModification
        End Get
        Set(ByVal value As Boolean)
            _allowModification = value

            lockItems(Not value, True)
        End Set
    End Property

#Region "FolderTexteTypes Events"
    Private Sub fenFolderTexteTypes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (formModified OrElse oneTexteModified) Then
            Select Case MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Case System.Windows.Forms.DialogResult.Yes
                    Me.modif_Click(sender, EventArgs.Empty)
                    FolderTextTypesManager.getInstance.saveAll()
                Case System.Windows.Forms.DialogResult.Cancel
                    e.Cancel = True
                Case System.Windows.Forms.DialogResult.No
                    FolderTextTypesManager.getInstance.load() 'Ensure all modif revert
            End Select
        End If

        If allowModification AndAlso e.Cancel = False Then lockSecteur("FTTs", False)
    End Sub
    Private Sub folderTexteTypes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub fenFolderTexteTypes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        configList(listFolderTexteTypes)
        loading()
    End Sub
#End Region

    'TODO: (In the document) Shall replace the .Enabled by .ReadOnly + Replace GROUP.Enabled by LockGROUP function to lock it with ReadOnly.

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal all As Boolean = False)
        gbFTT.Enabled = all OrElse Not trueFalse
        Me.modif.Enabled = Not trueFalse
        Me.delete.Enabled = Not trueFalse
        Me.setDefault.Enabled = Not trueFalse
        If Me.listFolderTexteTypes.selected = Me.listFolderTexteTypes.findFirstItem Then
            Me.upTextType.Enabled = False
        Else
            Me.upTextType.Enabled = Not trueFalse
        End If
        If Me.listFolderTexteTypes.selected = Me.listFolderTexteTypes.findLastItem Then
            Me.downTextType.Enabled = False
        Else
            Me.downTextType.Enabled = Not trueFalse
        End If

        'Use when locking form, when another user already modifying
        If all Then
            add.Enabled = False
            IsActive.Enabled = Not trueFalse
            TerminatedOnCreation.Enabled = Not trueFalse
            TexteTitle.ReadOnly = trueFalse
            NoModeleCategorie.ReadOnly = trueFalse
            CopyTextToOtherText.ReadOnly = trueFalse
            ResetTextOnCopy.Enabled = Not trueFalse
            NbDaysDiff.ReadOnly = trueFalse
            IsNbDaysDiffBefore.Enabled = Not trueFalse
            WhenToBeCreated.ReadOnly = trueFalse
            NbDaysX.ReadOnly = trueFalse
            NbPresencesX.ReadOnly = trueFalse
            ShowAlert.Enabled = Not trueFalse
            ShowAlarm.Enabled = Not trueFalse
            AlertNbDaysForExpiry.ReadOnly = trueFalse
            AlertMessageArticle.ReadOnly = trueFalse
            Multiple.Enabled = Not trueFalse
            TypeForMultiple.ReadOnly = trueFalse
            WhenToBeStopped.ReadOnly = trueFalse
            NbDaysMultiple.ReadOnly = trueFalse
            NbPresencesMultiple.ReadOnly = trueFalse
            NbMultipleEnding.ReadOnly = trueFalse
            btnSelectModelAppliedOnCreation.Enabled = Not trueFalse
            startingExternalStatus.ReadOnly = trueFalse
        End If
    End Sub

    Private Sub checks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowAlarm.CheckedChanged, ShowAlert.CheckedChanged, Multiple.CheckedChanged, ResetTextOnCopy.CheckedChanged, IsActive.CheckedChanged
        formModified = True
    End Sub

    Private Sub loading()
        'REM_CODES
        'If lockSecteur had already been done and allowed, skip verification (anyhow this sector is blocked in a whole)
        allowModification = allowModification OrElse lockSecteur("FTTs", True, Me.Text, PreferencesManager.getUserPreferences()("AffMSGModif"))

        Dim curFTTNo As Integer = 0
        If listFolderTexteTypes.selected <> -1 Then curFTTNo = CType(listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected), FolderTextType).noFolderTexteType
        listFolderTexteTypes.cls()
        For Each curFTT As FolderTextType In FolderTextTypesManager.getInstance.getItemables()
            Dim n As Integer = listFolderTexteTypes.add(curFTT.toString)
            listFolderTexteTypes.ItemValueA(n) = curFTT
            If curFTT.isDefault Then listFolderTexteTypes.ItemIconsShowed(n, 0) = True
            If curFTTNo = curFTT.noFolderTexteType Then listFolderTexteTypes.selected = n
        Next

        'Load external statuses
        startingExternalStatus.Items.AddRange(Accounts.Clients.Folders.Codifications.ExternalStatuses.getInstance.statuses.ToArray())
        If startingExternalStatus.Items.Count <> 0 Then startingExternalStatus.SelectedIndex = 0

        formModified = False 'Controls were modified by default text values

        listFolderTexteTypes.draw = True : listFolderTexteTypes.draw = False

        If allowModification Then Me.add.Enabled = True
    End Sub

    Private Sub showAlert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowAlert.CheckedChanged
        gbAlert.Enabled = ShowAlert.Checked
        If Not allowModification Then lockItems(True, True) 'Ensure controls are locked if modification not allowed
    End Sub

    Private Sub multiple_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Multiple.CheckedChanged
        'REM_CODES
        If loadingFTT = False Then
            Dim myFTT As FolderTextType = listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected)
            'Vérification si Multiple.Checked=true, si ce FTT n'est pas déjà lié en tant que récipient d'une copie pour un autre FTT
            If Multiple.Checked AndAlso myFTT.noFolderTexteType <> 0 Then
                For i As Integer = 0 To listFolderTexteTypes.listCount - 1
                    Dim curFTT As FolderTextType = listFolderTexteTypes.ItemValueA(i)
                    If curFTT.copyTextToOtherText = myFTT.noFolderTexteType Then
                        MessageBox.Show("Impossible de changer ce type de texte pour qu'il devienne multiple, car il est déjà utilisé comme récipient d'une copie par le type de texte " & curFTT.textTitle, "Impossible de multiplier le texte", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Multiple.Checked = False
                        Exit For
                    End If
                Next i
            End If
        End If

        gbMultiple.Enabled = Multiple.Checked
        typeForMultiple_SelectedIndexChanged(sender, e)
        If Not allowModification Then lockItems(True, True) 'Ensure controls are locked if modification not allowed
    End Sub

    Private Sub listFolderTexteTypes_SelectedChange() Handles listFolderTexteTypes.selectedChange
        'REM_CODES
        If listFolderTexteTypes.selected = -1 Then
            resetFields()
        Else
            lockItems(False)
            loadingFTT = True
            loadCopyTo()
            Dim curFTT As FolderTextType = listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected)
            IsActive.Checked = curFTT.isActive
            If curFTT.isDefault Then Me.setDefault.Enabled = False
            For Each curMC As ModelCategory In NoModeleCategorie.Items
                If curMC.noCategory = curFTT.noModelCategory Then NoModeleCategorie.SelectedItem = curMC : Exit For
            Next
            ResetTextOnCopy.Checked = curFTT.resetTextOnCopy
            IsNbDaysDiffBefore.SelectedIndex = IIf(curFTT.isNbDaysDiffBefore, 0, 1)
            WhenToBeCreated.SelectedIndex = curFTT.whenToBeCreated
            TerminatedOnCreation.Checked = curFTT.terminatedOnCreation
            Me.whenToBeCreated_SelectedIndexChanged(Me, EventArgs.Empty)
            WhenToBeStopped.SelectedIndex = curFTT.whenToBeStopped
            AlertMessageArticle.Text = curFTT.alertMessageArticle
            AlertNbDaysForExpiry.Text = curFTT.alertNbDaysForExpiry
            If curFTT.copyTextToOtherText = 0 Then
                CopyTextToOtherText.SelectedIndex = 0
            Else
                For i As Integer = 1 To CopyTextToOtherText.Items.Count - 1
                    If CType(CopyTextToOtherText.Items(i), FolderTextType).noFolderTexteType = curFTT.copyTextToOtherText Then CopyTextToOtherText.SelectedIndex = i : Exit For
                Next i
            End If
            Multiple.Checked = curFTT.multiple
            NbDaysMultiple.Text = curFTT.nbDaysMultiple
            NbDaysX.Text = curFTT.nbDaysX
            NbDaysDiff.Text = curFTT.nbDaysDiff
            NbMultipleEnding.Text = curFTT.nbMultipleEnding
            NbPresencesMultiple.Text = curFTT.nbPresencesMultiple
            NbPresencesX.Text = curFTT.nbPresencesX
            ShowAlarm.Checked = curFTT.showAlarm
            ShowAlert.Checked = curFTT.showAlert
            TexteTitle.Text = curFTT.textTitle
            TypeForMultiple.SelectedIndex = curFTT.typeForMultiple
            startingExternalStatus.SelectedItem = ExternalStatuses.getInstance.getStatus(curFTT.startingExternalStatus)
            If curFTT.modelAppliedOnCreation <> 0 Then
                Dim modelName() As String = DBLinker.getInstance.readOneDBField("Modeles", "Nom", "WHERE NoModele=" & curFTT.modelAppliedOnCreation)
                If modelName IsNot Nothing AndAlso modelName.Length <> 0 Then
                    modelAppliedOnCreation.Tag = curFTT.modelAppliedOnCreation
                    modelAppliedOnCreation.Text = modelName(0)
                Else
                    modelAppliedOnCreation.Tag = Nothing
                    modelAppliedOnCreation.Text = NO_MODEL_APPLIED
                End If
            Else
                modelAppliedOnCreation.Tag = Nothing
                modelAppliedOnCreation.Text = NO_MODEL_APPLIED
            End If

            'Disable parameters where modification can change number of foldertextes if foldertextes already present
            If curFTT.countFolderTexts() > 0 Then
                Multiple.Enabled = Not curFTT.multiple
                gbMultiple.Enabled = False
                WhenToBeCreated.Enabled = False
                NbDaysX.Enabled = False
                NbPresencesX.Enabled = False
            Else
                Multiple.Enabled = True
                WhenToBeCreated.Enabled = True
            End If

            loadingFTT = False
        End If

        If Not allowModification Then lockItems(True, True) 'Ensure controls are locked if modification not allowed

        formModified = False
    End Sub

    Private Sub loadCopyTo()
        'REM_CODES
        Dim oldObject As Object = CopyTextToOtherText.SelectedItem

        CopyTextToOtherText.Items.Clear()
        CopyTextToOtherText.Items.Add("* Aucun *")
        For Each curItem As CI.Controls.IListItem In listFolderTexteTypes.items
            Dim curFTT As FolderTextType = curItem.ValueA
            If curFTT.noFolderTexteType <> 0 AndAlso curItem.IsSelected = False AndAlso curFTT.multiple = False Then CopyTextToOtherText.Items.Add(curFTT)
        Next
        If listFolderTexteTypes.selected <> -1 Then CopyTextToOtherText.Items.Remove(listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected))
        If oldObject Is Nothing Then
            CopyTextToOtherText.SelectedIndex = 0
        Else
            CopyTextToOtherText.SelectedItem = oldObject
        End If
    End Sub

    Private Sub resetFields()
        lockItems(True)
        Me.TexteTitle.Text = ""
        Me.IsActive.Checked = True
        Me.CopyTextToOtherText.Items.Clear()
        Me.NbDaysDiff.Text = 0
        Me.NbDaysMultiple.Text = 0
        Me.NbDaysX.Text = 0
        Me.NbMultipleEnding.Text = 0
        Me.NbPresencesMultiple.Text = 0
        Me.NbPresencesX.Text = 0
        Me.Multiple.Checked = False
        'Me.IsNbDaysDiffBefore.SelectedIndex = 0
        'Me.AlertIsTrpTraitant.SelectedIndex = 0
        'Me.WhenToBeCreated.SelectedIndex = 0
        'Me.WhenToBeStopped.SelectedIndex = 0
        'Me.TypeForMultiple.SelectedIndex = 0
        Me.ShowAlarm.Checked = False
        Me.ShowAlert.Checked = False
        Me.AlertMessageArticle.Text = ""
        Me.AlertNbDaysForExpiry.Text = 0
        Me.startingExternalStatus.SelectedIndex = 0
        Me.modelAppliedOnCreation.Tag = Nothing
        Me.modelAppliedOnCreation.Text = NO_MODEL_APPLIED
        Me.TerminatedOnCreation.Checked = False
    End Sub

    Private Const NO_MODEL_APPLIED As String = "Aucun modèle appliqué à la création"

    Private Sub add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.onlyAlphabet = True
        myInputBoxPlus.acceptedChars = " §:§-§.§,"
        Dim newName As String = myInputBoxPlus("Veuillez entrer le titre du nouveau type de texte", "Titre du type de texte")
        If newName = "" Then Exit Sub

        'REM_CODES
        Dim returning As String = FolderTextTypesManager.getInstance.addItemable(newName)
        If returning <> "" Then MessageBox.Show(returning, "Impossible d'ajouter un type de texte", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        Dim n As Integer = listFolderTexteTypes.add(newName)
        listFolderTexteTypes.ItemValueA(n) = FolderTextTypesManager.getInstance.getItemable(newName)

        listFolderTexteTypes.draw = True : listFolderTexteTypes.draw = False
        Me.listFolderTexteTypes.selected = n
        oneTexteModified = True
    End Sub

    Private Sub modif_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles modif.Click
        If listFolderTexteTypes.selected = -1 Then Exit Sub

        'REM_CODES
        Dim sameNameItem As Integer = listFolderTexteTypes.findStringExact(TexteTitle.Text, False)
        If sameNameItem <> -1 AndAlso sameNameItem <> listFolderTexteTypes.selected Then MessageBox.Show("Le nom entré existe déjà pour un autre type de texte. Veuillez en choisir un autre.", "Impossible de modifier le type de texte", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

        Dim curFTT As FolderTextType = listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected)
        listFolderTexteTypes.ItemText(listFolderTexteTypes.selected) = TexteTitle.Text
        curFTT.isActive = IsActive.Checked
        curFTT.noModelCategory = CType(NoModeleCategorie.SelectedItem, ModelCategory).noCategory
        curFTT.alertMessageArticle = AlertMessageArticle.Text
        curFTT.alertNbDaysForExpiry = AlertNbDaysForExpiry.Text
        Dim noText As Integer = 0
        If CopyTextToOtherText.SelectedIndex <> 0 Then noText = CType(CopyTextToOtherText.SelectedItem, FolderTextType).noFolderTexteType
        curFTT.resetTextOnCopy = ResetTextOnCopy.Checked
        curFTT.copyTextToOtherText = noText
        curFTT.isNbDaysDiffBefore = IsNbDaysDiffBefore.SelectedIndex = 0
        curFTT.multiple = Multiple.Checked
        curFTT.nbDaysDiff = NbDaysDiff.Text
        curFTT.nbDaysMultiple = NbDaysMultiple.Text
        curFTT.nbDaysX = NbDaysX.Text
        curFTT.nbMultipleEnding = NbMultipleEnding.Text
        curFTT.nbPresencesMultiple = NbPresencesMultiple.Text
        curFTT.nbPresencesX = NbPresencesX.Text
        curFTT.showAlarm = ShowAlarm.Checked
        curFTT.showAlert = ShowAlert.Checked
        curFTT.textTitle = TexteTitle.Text
        curFTT.typeForMultiple = TypeForMultiple.SelectedIndex
        curFTT.whenToBeCreated = WhenToBeCreated.SelectedIndex
        curFTT.whenToBeStopped = WhenToBeStopped.SelectedIndex
        If startingExternalStatus.SelectedItem IsNot Nothing Then curFTT.startingExternalStatus = CType(startingExternalStatus.SelectedItem, ExternalStatus).noExternalStatus
        If modelAppliedOnCreation.Tag Is Nothing Then
            curFTT.modelAppliedOnCreation = 0
        Else
            curFTT.modelAppliedOnCreation = modelAppliedOnCreation.Tag
        End If
        curFTT.terminatedOnCreation = TerminatedOnCreation.Checked

        loadCopyTo()

        formModified = False
        Me.oneTexteModified = True
    End Sub

    Private Sub delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click
        'REM_CODES
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce(s) type(s) de texte ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim errors As New Generic.List(Of FolderTextType)
        Dim nbToDelete As Integer = 0
        For i As Integer = listFolderTexteTypes.listCount - 1 To 0 Step -1
            If listFolderTexteTypes.items(i).IsSelected Then
                Dim curFTT As FolderTextType = listFolderTexteTypes.ItemValueA(i)
                Try
                    curFTT.delete()

                    listFolderTexteTypes.remove(i)
                    nbToDelete += 1
                Catch ex As DBItemableUndeletable
                    errors.Add(ex.dbItemables(0))
                End Try
            End If
        Next

        If errors.Count <> 0 Then
            Dim plural As String = If(errors.Count = 1, "", "s")
            Dim msg As String = "Le" & plural & " type" & plural & " de textes ci-dessous n'" & If(errors.Count = 1, "a", "ont") & " pu être supprimé" & plural & ", car il" & plural & " " & If(errors.Count = 1, "est", "sont") & " en cours d'utilisation :"
            For Each curErr As FolderTextType In errors
                msg &= vbCrLf & curErr.textTitle
            Next

            MessageBox.Show(msg, "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        If nbToDelete <> 0 Then
            listFolderTexteTypes.draw = True : listFolderTexteTypes.draw = False
            oneTexteModified = True
        End If
    End Sub

    Private Sub whenToBeCreated_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhenToBeCreated.SelectedIndexChanged
        'REM_CODES
        NbPresencesX.Enabled = WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnPresenceX
        NbDaysX.Enabled = WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnPresenceX Or WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnDayX
        If WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnFolderCreation Then NbDaysX.Text = 0

        'Si la création ce fait lors de la fermeture du dossier, alors impossible de terminer un texte multiple également à la fermeture du dossier
        WhenToBeStopped.Enabled = WhenToBeCreated.SelectedIndex <> FolderTextType.WhenToBeCreate.OnFolderClosing
        If WhenToBeStopped.Enabled = False Then WhenToBeStopped.SelectedIndex = FolderTextType.WhenToBeStop.OnMaxReached

        If WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnFolderCreation Or WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnFolderClosing Then
            IsNbDaysDiffBefore.SelectedIndex = 1
            IsNbDaysDiffBefore.Enabled = False
        Else
            IsNbDaysDiffBefore.Enabled = True
        End If

        If WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnFolderClosing AndAlso TypeForMultiple.SelectedIndex = FolderTextType.TypeMultiple.NbPresencesX Then
            TypeForMultiple.SelectedIndex = FolderTextType.TypeMultiple.NbDaysX
        End If

        If NbPresencesX.Enabled Then
            NbPresencesX.minimum = 1
            NbPresencesX.Text = 1
        Else
            NbPresencesX.minimum = 0
            NbPresencesX.Text = 0
        End If

        If Not allowModification Then lockItems(True, True) 'Ensure controls are locked if modification not allowed
    End Sub

    Private Sub typeForMultiple_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeForMultiple.SelectedIndexChanged
        'REM_CODES
        If loadingFTT = False AndAlso WhenToBeCreated.SelectedIndex = FolderTextType.WhenToBeCreate.OnFolderClosing AndAlso TypeForMultiple.SelectedIndex = FolderTextType.TypeMultiple.NbPresencesX Then
            MessageBox.Show("Impossible de sélectionner un moment de création par nombre de présences pour les textes multiples si le moment de création du premier texte est à la fermeture du dossier.", "Choix invalide", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TypeForMultiple.SelectedIndex = oldTypeForMultiple
            Exit Sub
        End If

        NbPresencesMultiple.Enabled = TypeForMultiple.SelectedIndex = FolderTextType.TypeMultiple.NbPresencesX
        NbDaysMultiple.Enabled = TypeForMultiple.SelectedIndex = FolderTextType.TypeMultiple.NbDaysX
        If Multiple.Checked AndAlso TypeForMultiple.SelectedIndex = FolderTextType.TypeMultiple.NbDaysX Then
            ShowAlert.Checked = True
            ShowAlarm.Checked = True
            ShowAlarm.Enabled = False
            ShowAlert.Enabled = False
        Else
            ShowAlarm.Enabled = True
            ShowAlert.Enabled = True
        End If

        oldTypeForMultiple = TypeForMultiple.SelectedIndex

        If Not allowModification Then lockItems(True, True) 'Ensure controls are locked if modification not allowed
    End Sub

    Private Sub whenToBeStopped_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhenToBeStopped.SelectedIndexChanged
        'REM_CODES 
        NbMultipleEnding.Enabled = WhenToBeStopped.SelectedIndex = FolderTextType.WhenToBeStop.OnMaxReached
        If Not allowModification Then lockItems(True, True) 'Ensure controls are locked if modification not allowed
    End Sub

    Private Sub texts_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NbPresencesX.TextChanged, NbDaysX.TextChanged, NbDaysDiff.TextChanged, TexteTitle.TextChanged, AlertNbDaysForExpiry.TextChanged, AlertMessageArticle.TextChanged, NbDaysMultiple.TextChanged, NbPresencesMultiple.TextChanged, NbMultipleEnding.TextChanged, modelAppliedOnCreation.TextChanged
        formModified = True
    End Sub

    Private Sub lists_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhenToBeStopped.SelectedIndexChanged, IsNbDaysDiffBefore.SelectedIndexChanged, CopyTextToOtherText.SelectedIndexChanged, WhenToBeCreated.SelectedIndexChanged, TypeForMultiple.SelectedIndexChanged, NoModeleCategorie.SelectedIndexChanged, startingExternalStatus.SelectedIndexChanged
        If sender Is Me.IsNbDaysDiffBefore Then
            nbDaysX_TextChanged(sender, e)
        End If
        If sender Is Me.CopyTextToOtherText AndAlso Me.ResetTextOnCopy IsNot Nothing Then
            If Me.CopyTextToOtherText.SelectedIndex = 0 Then
                Me.ResetTextOnCopy.Checked = False
                Me.ResetTextOnCopy.Enabled = False
            Else
                Me.ResetTextOnCopy.Enabled = True
            End If
        End If

        formModified = True
        If Not allowModification Then lockItems(True, True) 'Ensure controls are locked if modification not allowed
    End Sub

    Private Sub listFolderTexteTypes_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles listFolderTexteTypes.willSelect
        If listFolderTexteTypes.selected <> -1 AndAlso formModified = True AndAlso MessageBox.Show("Désirez-vous accepter les modifications ?", "Modification", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.modif_Click(sender, EventArgs.Empty)
        End If
    End Sub

    Private Sub nbDaysX_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NbDaysX.TextChanged
        If IsNbDaysDiffBefore.SelectedIndex = 0 Then
            NbDaysDiff.blockOnMaximum = True
            NbDaysDiff.maximum = NbDaysX.Text
            If CInt(NbDaysDiff.Text) > CInt(NbDaysX.Text) Then NbDaysDiff.Text = NbDaysX.Text
        Else
            NbDaysDiff.blockOnMaximum = False
        End If
    End Sub

    Private Sub setDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles setDefault.Click
        'REM_CODES
        For i As Integer = 0 To listFolderTexteTypes.listCount - 1
            Dim thisFTT As FolderTextType = listFolderTexteTypes.ItemValueA(i)
            If listFolderTexteTypes.selected = i Then
                listFolderTexteTypes.ItemIconsShowed(i, 0) = True
                thisFTT.isDefault = True
            Else
                listFolderTexteTypes.ItemIconsShowed(i, 0) = False
                thisFTT.isDefault = False
            End If
        Next i

        oneTexteModified = True

        setDefault.Enabled = False
        listFolderTexteTypes.draw = True : listFolderTexteTypes.draw = False
    End Sub

    Private Sub upTextType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles upTextType.Click
        'REM_CODES
        Dim curFTT As FolderTextType = listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected)
        Dim toFTT As FolderTextType = listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected - 1)
        Dim oldPos As Integer = curFTT.position
        curFTT.position = toFTT.position
        toFTT.position = oldPos

        loading()
        oneTexteModified = True
    End Sub

    Private Sub downTextType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downTextType.Click
        'REM_CODES
        Dim curFTT As FolderTextType = listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected)
        Dim toFTT As FolderTextType = listFolderTexteTypes.ItemValueA(listFolderTexteTypes.selected + 1)
        Dim oldPos As Integer = curFTT.position
        curFTT.position = toFTT.position
        toFTT.position = oldPos

        loading()
        oneTexteModified = True
    End Sub

    Private Sub btnSelectModelAppliedOnCreation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectModelAppliedOnCreation.Click
        selectModelAppliedOnCreation()
    End Sub

    Private Sub selectModelAppliedOnCreation()
        Dim myContextMenu As New ContextMenu()
        myContextMenu = ModelsManager.getInstance.createModelsMenu(New Integer() {1, CType(Me.NoModeleCategorie.SelectedItem, ModelCategory).noCategory}, New EventHandler(AddressOf menumodele_Click), , True)
        myContextMenu.Show(btnSelectModelAppliedOnCreation, New Point(0, 0))
    End Sub

    Private Sub menumodele_Click(ByVal sender As Object, ByVal e As EventArgs)
        If CType(sender, MenuItem).Tag Is Nothing Then
            modelAppliedOnCreation.Tag = Nothing
            modelAppliedOnCreation.Text = NO_MODEL_APPLIED
        Else
            Dim noModele As Integer = CType(sender, MenuItem).Tag
            Dim nomModele As String = CType(sender, MenuItem).Text
            modelAppliedOnCreation.Tag = noModele
            modelAppliedOnCreation.Text = nomModele
        End If

        formModified = True
    End Sub
End Class
