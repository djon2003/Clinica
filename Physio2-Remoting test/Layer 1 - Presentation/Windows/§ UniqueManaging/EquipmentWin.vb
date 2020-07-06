''' <summary>
''' 
''' </summary>
''' <remarks>From Default namespace renaming, from then, the designer is no more able to open the window unless we delete or set to thing the line Me.ListEquipement.items = XXXX within InitializeComponent</remarks>
Friend Class EquipmentWin
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin

        'Chargement des images
        Me.additem.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.ajout.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.delete.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Dim modifSaveImg As New ImageList
        modifSaveImg.Images.Add(DrawingManager.getInstance.getImage("modifier16.gif"))
        modifSaveImg.Images.Add(DrawingManager.getInstance.getImage("save.jpg"))
        Me.modif.ImageList = modifSaveImg
        Me.modif.ImageIndex = 0
        Me.removeitem.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.vider.Image = DrawingManager.getInstance.getImage("eraser.jpg")
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Frame1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents removeitem As System.Windows.Forms.Button
    Friend WithEvents EType As ManagedCombo
    Friend WithEvents ENom As ManagedText
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents additem As System.Windows.Forms.Button
    Friend WithEvents Frame2 As System.Windows.Forms.GroupBox
    Friend WithEvents EDescription As ManagedText
    Friend WithEvents EVente As ManagedText
    Friend WithEvents ERefundDepot As ManagedCombo
    Friend WithEvents EPar As ManagedCombo
    Friend WithEvents EPret As ManagedText
    Friend WithEvents EDepot As ManagedText
    Friend WithEvents nbVente As System.Windows.Forms.Label
    Friend WithEvents nbPret As System.Windows.Forms.Label
    Friend WithEvents nbTotal As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ajout As System.Windows.Forms.Button
    Friend WithEvents modif As System.Windows.Forms.Button
    Friend WithEvents delete As System.Windows.Forms.Button
    Friend WithEvents ListEquipement As CI.Controls.List
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents EAchat As ManagedText
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents vider As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents nbRestant As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents categorie As Clinica.ManagedCombo
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ERefundCout As Clinica.ManagedCombo
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents fPrets As System.Windows.Forms.CheckBox
    Friend WithEvents fVentes As System.Windows.Forms.CheckBox
    Friend WithEvents fInventaire As System.Windows.Forms.CheckBox
    Friend WithEvents EItems As System.Windows.Forms.ListBox
    Friend WithEvents EApplyTax As System.Windows.Forms.CheckBox
    Public WithEvents fCategorie As Clinica.ManagedCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EquipmentWin))
        Me.ListEquipement = New CI.Controls.List
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.categorie = New ManagedCombo
        Me.EItems = New System.Windows.Forms.ListBox
        Me.EAchat = New ManagedText
        Me.Label17 = New System.Windows.Forms.Label
        Me.EDescription = New ManagedText
        Me.EVente = New ManagedText
        Me.ERefundCout = New ManagedCombo
        Me.ERefundDepot = New ManagedCombo
        Me.EPar = New ManagedCombo
        Me.EPret = New ManagedText
        Me.EDepot = New ManagedText
        Me.removeitem = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.EType = New ManagedCombo
        Me.ENom = New ManagedText
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.additem = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.EApplyTax = New System.Windows.Forms.CheckBox
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.nbRestant = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.nbVente = New System.Windows.Forms.Label
        Me.nbPret = New System.Windows.Forms.Label
        Me.nbTotal = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.ajout = New System.Windows.Forms.Button
        Me.modif = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.vider = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label21 = New System.Windows.Forms.Label
        Me.fCategorie = New ManagedCombo
        Me.Label18 = New System.Windows.Forms.Label
        Me.fPrets = New System.Windows.Forms.CheckBox
        Me.fVentes = New System.Windows.Forms.CheckBox
        Me.fInventaire = New System.Windows.Forms.CheckBox
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListEquipement
        '
        Me.ListEquipement.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.ListEquipement.autoAdjust = True
        Me.ListEquipement.autoKeyDownSelection = True
        Me.ListEquipement.autoSizeHorizontally = False
        Me.ListEquipement.autoSizeVertically = False
        Me.ListEquipement.BackColor = System.Drawing.Color.White
        Me.ListEquipement.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.ListEquipement.baseBackColor = System.Drawing.Color.White
        Me.ListEquipement.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.ListEquipement.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.ListEquipement.bgColor = System.Drawing.Color.White
        Me.ListEquipement.borderColor = System.Drawing.Color.Empty
        Me.ListEquipement.borderSelColor = System.Drawing.Color.Empty
        Me.ListEquipement.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.ListEquipement.CausesValidation = False
        Me.ListEquipement.clickEnabled = True
        Me.ListEquipement.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.ListEquipement.do3D = False
        Me.ListEquipement.draw = False
        Me.ListEquipement.extraWidth = 0
        Me.ListEquipement.hScrollColor = System.Drawing.SystemColors.Control
        Me.ListEquipement.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.ListEquipement.hScrolling = True
        Me.ListEquipement.hsValue = 0
        Me.ListEquipement.icons = Nothing
        Me.ListEquipement.itemBorder = 0
        Me.ListEquipement.itemMargin = 0
        Me.ListEquipement.items = CType(resources.GetObject("ListEquipement.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.ListEquipement.Location = New System.Drawing.Point(12, 12)
        Me.ListEquipement.mouseMove3D = False
        Me.ListEquipement.mouseSpeed = 0
        Me.ListEquipement.Name = "ListEquipement"
        Me.ListEquipement.objMaxHeight = 0.0!
        Me.ListEquipement.objMaxWidth = 0.0!
        Me.ListEquipement.objMinHeight = 0.0!
        Me.ListEquipement.objMinWidth = 0.0!
        Me.ListEquipement.reverseSorting = False
        Me.ListEquipement.selected = -1
        Me.ListEquipement.selectedClickAllowed = False
        Me.ListEquipement.selectMultiple = False
        Me.ListEquipement.Size = New System.Drawing.Size(339, 194)
        Me.ListEquipement.sorted = True
        Me.ListEquipement.TabIndex = 12
        Me.ListEquipement.TabStop = False
        Me.ListEquipement.toolTipText = Nothing
        Me.ListEquipement.vScrollColor = System.Drawing.SystemColors.Control
        Me.ListEquipement.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.ListEquipement.vScrolling = True
        Me.ListEquipement.vsValue = 0
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.Color.Transparent
        Me.Frame1.Controls.Add(Me.categorie)
        Me.Frame1.Controls.Add(Me.EItems)
        Me.Frame1.Controls.Add(Me.EAchat)
        Me.Frame1.Controls.Add(Me.Label17)
        Me.Frame1.Controls.Add(Me.EDescription)
        Me.Frame1.Controls.Add(Me.EVente)
        Me.Frame1.Controls.Add(Me.ERefundCout)
        Me.Frame1.Controls.Add(Me.ERefundDepot)
        Me.Frame1.Controls.Add(Me.EPar)
        Me.Frame1.Controls.Add(Me.EPret)
        Me.Frame1.Controls.Add(Me.EDepot)
        Me.Frame1.Controls.Add(Me.removeitem)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.EType)
        Me.Frame1.Controls.Add(Me.ENom)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.Label12)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Controls.Add(Me.Label10)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label8)
        Me.Frame1.Controls.Add(Me.Label6)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Controls.Add(Me.additem)
        Me.Frame1.Controls.Add(Me.Label20)
        Me.Frame1.Controls.Add(Me.Label14)
        Me.Frame1.Controls.Add(Me.Label23)
        Me.Frame1.Controls.Add(Me.Label22)
        Me.Frame1.Controls.Add(Me.Label13)
        Me.Frame1.Controls.Add(Me.Label16)
        Me.Frame1.Controls.Add(Me.Label15)
        Me.Frame1.Controls.Add(Me.EApplyTax)
        Me.Frame1.Location = New System.Drawing.Point(12, 212)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Size = New System.Drawing.Size(591, 250)
        Me.Frame1.TabIndex = 27
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Options pour l'équipement"
        '
        'categorie
        '
        Me.categorie.acceptAlpha = True
        Me.categorie.acceptedChars = Nothing
        Me.categorie.acceptNumeric = True
        Me.categorie.allCapital = False
        Me.categorie.allLower = False
        Me.categorie.autoComplete = True
        Me.categorie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.categorie.autoSizeDropDown = True
        Me.categorie.BackColor = System.Drawing.Color.White
        Me.categorie.blockOnMaximum = False
        Me.categorie.blockOnMinimum = False
        Me.categorie.cb_AcceptLeftZeros = False
        Me.categorie.cb_AcceptNegative = False
        Me.categorie.currencyBox = False
        Me.categorie.Cursor = System.Windows.Forms.Cursors.Default
        Me.categorie.dbField = "ECategorie.Categorie"
        Me.categorie.doComboDelete = True
        Me.categorie.firstLetterCapital = True
        Me.categorie.firstLettersCapital = False
        Me.categorie.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.categorie.ForeColor = System.Drawing.SystemColors.WindowText
        Me.categorie.itemsToolTipDuration = 10000
        Me.categorie.Location = New System.Drawing.Point(280, 113)
        Me.categorie.manageText = True
        Me.categorie.matchExp = ""
        Me.categorie.maximum = 0
        Me.categorie.minimum = 0
        Me.categorie.Name = "categorie"
        Me.categorie.nbDecimals = CType(-1, Short)
        Me.categorie.onlyAlphabet = False
        Me.categorie.pathOfList = ""
        Me.categorie.ReadOnly = False
        Me.categorie.refuseAccents = False
        Me.categorie.refusedChars = ""
        Me.categorie.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.categorie.showItemsToolTip = False
        Me.categorie.Size = New System.Drawing.Size(304, 22)
        Me.categorie.Sorted = True
        Me.categorie.TabIndex = 49
        Me.categorie.trimText = False
        '
        'EItems
        '
        Me.EItems.FormattingEnabled = True
        Me.EItems.Location = New System.Drawing.Point(11, 94)
        Me.EItems.Name = "EItems"
        Me.EItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.EItems.Size = New System.Drawing.Size(179, 147)
        Me.EItems.Sorted = True
        Me.EItems.TabIndex = 53
        '
        'EAchat
        '
        Me.EAchat.acceptAlpha = False
        Me.EAchat.acceptedChars = ",§."
        Me.EAchat.acceptNumeric = True
        Me.EAchat.allCapital = False
        Me.EAchat.allLower = False
        Me.EAchat.blockOnMaximum = False
        Me.EAchat.blockOnMinimum = False
        Me.EAchat.cb_AcceptLeftZeros = False
        Me.EAchat.cb_AcceptNegative = False
        Me.EAchat.currencyBox = True
        Me.EAchat.firstLetterCapital = False
        Me.EAchat.firstLettersCapital = False
        Me.EAchat.Location = New System.Drawing.Point(486, 88)
        Me.EAchat.manageText = True
        Me.EAchat.matchExp = ""
        Me.EAchat.maximum = 0
        Me.EAchat.minimum = 0
        Me.EAchat.Name = "EAchat"
        Me.EAchat.nbDecimals = CType(2, Short)
        Me.EAchat.onlyAlphabet = False
        Me.EAchat.refuseAccents = False
        Me.EAchat.refusedChars = ""
        Me.EAchat.showInternalContextMenu = True
        Me.EAchat.Size = New System.Drawing.Size(48, 20)
        Me.EAchat.TabIndex = 10
        Me.EAchat.Text = "0"
        Me.EAchat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.EAchat, "Montant avec taxes / numéro d'item acheter")
        Me.EAchat.trimText = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(483, 73)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(87, 13)
        Me.Label17.TabIndex = 50
        Me.Label17.Text = "Coût d'achat :"
        Me.ToolTip1.SetToolTip(Me.Label17, "Montant avec taxes")
        '
        'EDescription
        '
        Me.EDescription.acceptAlpha = True
        Me.EDescription.acceptedChars = ""
        Me.EDescription.acceptNumeric = True
        Me.EDescription.allCapital = False
        Me.EDescription.allLower = False
        Me.EDescription.blockOnMaximum = False
        Me.EDescription.blockOnMinimum = False
        Me.EDescription.cb_AcceptLeftZeros = False
        Me.EDescription.cb_AcceptNegative = False
        Me.EDescription.currencyBox = False
        Me.EDescription.firstLetterCapital = True
        Me.EDescription.firstLettersCapital = False
        Me.EDescription.Location = New System.Drawing.Point(209, 154)
        Me.EDescription.manageText = True
        Me.EDescription.matchExp = ""
        Me.EDescription.maximum = 0
        Me.EDescription.minimum = 0
        Me.EDescription.Multiline = True
        Me.EDescription.Name = "EDescription"
        Me.EDescription.nbDecimals = CType(-1, Short)
        Me.EDescription.onlyAlphabet = False
        Me.EDescription.refuseAccents = False
        Me.EDescription.refusedChars = ""
        Me.EDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.EDescription.showInternalContextMenu = True
        Me.EDescription.Size = New System.Drawing.Size(375, 87)
        Me.EDescription.TabIndex = 11
        Me.EDescription.trimText = False
        '
        'EVente
        '
        Me.EVente.acceptAlpha = False
        Me.EVente.acceptedChars = ",§."
        Me.EVente.acceptNumeric = True
        Me.EVente.allCapital = False
        Me.EVente.allLower = False
        Me.EVente.blockOnMaximum = False
        Me.EVente.blockOnMinimum = False
        Me.EVente.cb_AcceptLeftZeros = False
        Me.EVente.cb_AcceptNegative = False
        Me.EVente.currencyBox = True
        Me.EVente.firstLetterCapital = False
        Me.EVente.firstLettersCapital = False
        Me.EVente.Location = New System.Drawing.Point(486, 33)
        Me.EVente.manageText = True
        Me.EVente.matchExp = ""
        Me.EVente.maximum = 0
        Me.EVente.minimum = 0
        Me.EVente.Name = "EVente"
        Me.EVente.nbDecimals = CType(2, Short)
        Me.EVente.onlyAlphabet = False
        Me.EVente.refuseAccents = False
        Me.EVente.refusedChars = ""
        Me.EVente.showInternalContextMenu = True
        Me.EVente.Size = New System.Drawing.Size(48, 20)
        Me.EVente.TabIndex = 6
        Me.EVente.Text = "0"
        Me.EVente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.EVente, "Montant avant taxes / numéro d'item vendu")
        Me.EVente.trimText = False
        '
        'ERefundCout
        '
        Me.ERefundCout.acceptAlpha = True
        Me.ERefundCout.acceptedChars = Nothing
        Me.ERefundCout.acceptNumeric = True
        Me.ERefundCout.allCapital = False
        Me.ERefundCout.allLower = False
        Me.ERefundCout.autoComplete = True
        Me.ERefundCout.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ERefundCout.autoSizeDropDown = True
        Me.ERefundCout.BackColor = System.Drawing.Color.White
        Me.ERefundCout.blockOnMaximum = True
        Me.ERefundCout.blockOnMinimum = False
        Me.ERefundCout.cb_AcceptLeftZeros = False
        Me.ERefundCout.cb_AcceptNegative = False
        Me.ERefundCout.currencyBox = False
        Me.ERefundCout.dbField = Nothing
        Me.ERefundCout.doComboDelete = True
        Me.ERefundCout.firstLetterCapital = False
        Me.ERefundCout.firstLettersCapital = False
        Me.ERefundCout.Items.AddRange(New Object() {"0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100"})
        Me.ERefundCout.itemsToolTipDuration = 10000
        Me.ERefundCout.Location = New System.Drawing.Point(289, 87)
        Me.ERefundCout.manageText = True
        Me.ERefundCout.matchExp = Nothing
        Me.ERefundCout.maximum = 100
        Me.ERefundCout.minimum = 0
        Me.ERefundCout.Name = "ERefundCout"
        Me.ERefundCout.nbDecimals = CType(4, Short)
        Me.ERefundCout.onlyAlphabet = False
        Me.ERefundCout.pathOfList = Nothing
        Me.ERefundCout.ReadOnly = False
        Me.ERefundCout.refuseAccents = False
        Me.ERefundCout.refusedChars = Nothing
        Me.ERefundCout.showItemsToolTip = False
        Me.ERefundCout.Size = New System.Drawing.Size(66, 21)
        Me.ERefundCout.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ERefundCout, "Pourcentage remboursé du coût de prêt")
        Me.ERefundCout.trimText = False
        '
        'ERefundDepot
        '
        Me.ERefundDepot.acceptAlpha = True
        Me.ERefundDepot.acceptedChars = ",§."
        Me.ERefundDepot.acceptNumeric = True
        Me.ERefundDepot.allCapital = False
        Me.ERefundDepot.allLower = False
        Me.ERefundDepot.autoComplete = True
        Me.ERefundDepot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ERefundDepot.autoSizeDropDown = True
        Me.ERefundDepot.BackColor = System.Drawing.Color.White
        Me.ERefundDepot.blockOnMaximum = True
        Me.ERefundDepot.blockOnMinimum = False
        Me.ERefundDepot.cb_AcceptLeftZeros = False
        Me.ERefundDepot.cb_AcceptNegative = False
        Me.ERefundDepot.currencyBox = True
        Me.ERefundDepot.dbField = Nothing
        Me.ERefundDepot.doComboDelete = True
        Me.ERefundDepot.firstLetterCapital = False
        Me.ERefundDepot.firstLettersCapital = False
        Me.ERefundDepot.Items.AddRange(New Object() {"0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100"})
        Me.ERefundDepot.itemsToolTipDuration = 10000
        Me.ERefundDepot.Location = New System.Drawing.Point(208, 87)
        Me.ERefundDepot.manageText = True
        Me.ERefundDepot.matchExp = Nothing
        Me.ERefundDepot.maximum = 100
        Me.ERefundDepot.minimum = 0
        Me.ERefundDepot.Name = "ERefundDepot"
        Me.ERefundDepot.nbDecimals = CType(4, Short)
        Me.ERefundDepot.onlyAlphabet = False
        Me.ERefundDepot.pathOfList = Nothing
        Me.ERefundDepot.ReadOnly = False
        Me.ERefundDepot.refuseAccents = False
        Me.ERefundDepot.refusedChars = Nothing
        Me.ERefundDepot.showItemsToolTip = False
        Me.ERefundDepot.Size = New System.Drawing.Size(66, 21)
        Me.ERefundDepot.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ERefundDepot, "Pourcentage remboursé du dépôt")
        Me.ERefundDepot.trimText = False
        '
        'EPar
        '
        Me.EPar.acceptAlpha = True
        Me.EPar.acceptedChars = Nothing
        Me.EPar.acceptNumeric = True
        Me.EPar.allCapital = False
        Me.EPar.allLower = False
        Me.EPar.autoComplete = True
        Me.EPar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EPar.autoSizeDropDown = True
        Me.EPar.BackColor = System.Drawing.Color.White
        Me.EPar.blockOnMaximum = False
        Me.EPar.blockOnMinimum = False
        Me.EPar.cb_AcceptLeftZeros = False
        Me.EPar.cb_AcceptNegative = False
        Me.EPar.currencyBox = False
        Me.EPar.dbField = Nothing
        Me.EPar.doComboDelete = True
        Me.EPar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EPar.firstLetterCapital = False
        Me.EPar.firstLettersCapital = False
        Me.EPar.Items.AddRange(New Object() {"Une seule fois", "Jour", "Semaine"})
        Me.EPar.itemsToolTipDuration = 10000
        Me.EPar.Location = New System.Drawing.Point(381, 31)
        Me.EPar.manageText = True
        Me.EPar.matchExp = Nothing
        Me.EPar.maximum = 0
        Me.EPar.minimum = 0
        Me.EPar.Name = "EPar"
        Me.EPar.nbDecimals = CType(-1, Short)
        Me.EPar.onlyAlphabet = False
        Me.EPar.pathOfList = Nothing
        Me.EPar.ReadOnly = False
        Me.EPar.refuseAccents = False
        Me.EPar.refusedChars = Nothing
        Me.EPar.showItemsToolTip = False
        Me.EPar.Size = New System.Drawing.Size(89, 21)
        Me.EPar.TabIndex = 9
        Me.EPar.trimText = False
        '
        'EPret
        '
        Me.EPret.acceptAlpha = False
        Me.EPret.acceptedChars = ",§."
        Me.EPret.acceptNumeric = True
        Me.EPret.allCapital = False
        Me.EPret.allLower = False
        Me.EPret.blockOnMaximum = False
        Me.EPret.blockOnMinimum = False
        Me.EPret.cb_AcceptLeftZeros = False
        Me.EPret.cb_AcceptNegative = False
        Me.EPret.currencyBox = True
        Me.EPret.firstLetterCapital = False
        Me.EPret.firstLettersCapital = False
        Me.EPret.Location = New System.Drawing.Point(289, 32)
        Me.EPret.manageText = True
        Me.EPret.matchExp = ""
        Me.EPret.maximum = 0
        Me.EPret.minimum = 0
        Me.EPret.Name = "EPret"
        Me.EPret.nbDecimals = CType(2, Short)
        Me.EPret.onlyAlphabet = False
        Me.EPret.refuseAccents = False
        Me.EPret.refusedChars = ""
        Me.EPret.showInternalContextMenu = True
        Me.EPret.Size = New System.Drawing.Size(48, 20)
        Me.EPret.TabIndex = 8
        Me.EPret.Text = "0"
        Me.EPret.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.EPret, "Montant avant taxes / numéro d'item prêté")
        Me.EPret.trimText = False
        '
        'EDepot
        '
        Me.EDepot.acceptAlpha = False
        Me.EDepot.acceptedChars = ",§."
        Me.EDepot.acceptNumeric = True
        Me.EDepot.allCapital = False
        Me.EDepot.allLower = False
        Me.EDepot.blockOnMaximum = False
        Me.EDepot.blockOnMinimum = False
        Me.EDepot.cb_AcceptLeftZeros = False
        Me.EDepot.cb_AcceptNegative = False
        Me.EDepot.currencyBox = True
        Me.EDepot.firstLetterCapital = False
        Me.EDepot.firstLettersCapital = False
        Me.EDepot.Location = New System.Drawing.Point(208, 32)
        Me.EDepot.manageText = True
        Me.EDepot.matchExp = ""
        Me.EDepot.maximum = 0
        Me.EDepot.minimum = 0
        Me.EDepot.Name = "EDepot"
        Me.EDepot.nbDecimals = CType(2, Short)
        Me.EDepot.onlyAlphabet = False
        Me.EDepot.refuseAccents = False
        Me.EDepot.refusedChars = ""
        Me.EDepot.showInternalContextMenu = True
        Me.EDepot.Size = New System.Drawing.Size(48, 20)
        Me.EDepot.TabIndex = 7
        Me.EDepot.Text = "0"
        Me.EDepot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.EDepot, "Montant du dépôt" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Montant sans taxe")
        Me.EDepot.trimText = False
        '
        'removeitem
        '
        Me.removeitem.Enabled = False
        Me.removeitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.removeitem.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.removeitem.Location = New System.Drawing.Point(166, 70)
        Me.removeitem.Name = "removeitem"
        Me.removeitem.Size = New System.Drawing.Size(24, 24)
        Me.removeitem.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.removeitem, "Enlever un numéro d'item")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(205, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Description :"
        '
        'EType
        '
        Me.EType.acceptAlpha = True
        Me.EType.acceptedChars = Nothing
        Me.EType.acceptNumeric = True
        Me.EType.allCapital = False
        Me.EType.allLower = False
        Me.EType.autoComplete = True
        Me.EType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EType.autoSizeDropDown = True
        Me.EType.BackColor = System.Drawing.Color.White
        Me.EType.blockOnMaximum = False
        Me.EType.blockOnMinimum = False
        Me.EType.cb_AcceptLeftZeros = False
        Me.EType.cb_AcceptNegative = False
        Me.EType.currencyBox = False
        Me.EType.dbField = Nothing
        Me.EType.doComboDelete = True
        Me.EType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EType.firstLetterCapital = False
        Me.EType.firstLettersCapital = False
        Me.EType.Items.AddRange(New Object() {"Prêt", "Vente", "Prêt ou Vente", "Inventaire"})
        Me.EType.itemsToolTipDuration = 10000
        Me.EType.Location = New System.Drawing.Point(48, 40)
        Me.EType.manageText = True
        Me.EType.matchExp = Nothing
        Me.EType.maximum = 0
        Me.EType.minimum = 0
        Me.EType.Name = "EType"
        Me.EType.nbDecimals = CType(-1, Short)
        Me.EType.onlyAlphabet = False
        Me.EType.pathOfList = Nothing
        Me.EType.ReadOnly = False
        Me.EType.refuseAccents = False
        Me.EType.refusedChars = Nothing
        Me.EType.showItemsToolTip = False
        Me.EType.Size = New System.Drawing.Size(142, 21)
        Me.EType.TabIndex = 1
        Me.EType.trimText = False
        '
        'ENom
        '
        Me.ENom.acceptAlpha = True
        Me.ENom.acceptedChars = ""
        Me.ENom.acceptNumeric = True
        Me.ENom.allCapital = False
        Me.ENom.allLower = False
        Me.ENom.blockOnMaximum = False
        Me.ENom.blockOnMinimum = False
        Me.ENom.cb_AcceptLeftZeros = False
        Me.ENom.cb_AcceptNegative = False
        Me.ENom.currencyBox = False
        Me.ENom.firstLetterCapital = True
        Me.ENom.firstLettersCapital = True
        Me.ENom.Location = New System.Drawing.Point(48, 16)
        Me.ENom.manageText = True
        Me.ENom.matchExp = ""
        Me.ENom.maximum = 0
        Me.ENom.minimum = 0
        Me.ENom.Name = "ENom"
        Me.ENom.nbDecimals = CType(-1, Short)
        Me.ENom.onlyAlphabet = False
        Me.ENom.refuseAccents = False
        Me.ENom.refusedChars = "(§)"
        Me.ENom.showInternalContextMenu = True
        Me.ENom.Size = New System.Drawing.Size(142, 20)
        Me.ENom.TabIndex = 0
        Me.ENom.trimText = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(205, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(153, 26)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Pourcentages remboursés des montants ci-haut :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(115, 13)
        Me.Label12.TabIndex = 35
        Me.Label12.Text = "Numéro des items :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(483, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 13)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Coût de vente :"
        Me.ToolTip1.SetToolTip(Me.Label11, "Montant avant taxes")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 13)
        Me.Label10.TabIndex = 33
        Me.Label10.Text = "Type :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(378, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Par :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(286, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 13)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Coût de prêt :"
        Me.ToolTip1.SetToolTip(Me.Label8, "Montant avec taxes")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(205, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Dépôt :"
        Me.ToolTip1.SetToolTip(Me.Label6, "Montant sans taxes")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Item :"
        '
        'additem
        '
        Me.additem.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.additem.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.additem.Location = New System.Drawing.Point(136, 70)
        Me.additem.Name = "additem"
        Me.additem.Size = New System.Drawing.Size(24, 24)
        Me.additem.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.additem, "Ajout d'un numéro d'item")
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(205, 116)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(69, 13)
        Me.Label20.TabIndex = 34
        Me.Label20.Text = "Catégorie :"
        Me.ToolTip1.SetToolTip(Me.Label20, "Montant avant taxes")
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(335, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(13, 13)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "$"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(354, 90)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(15, 13)
        Me.Label23.TabIndex = 42
        Me.Label23.Text = "%"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(272, 91)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(15, 13)
        Me.Label22.TabIndex = 42
        Me.Label22.Text = "%"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(255, 35)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(13, 13)
        Me.Label13.TabIndex = 42
        Me.Label13.Text = "$"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(532, 91)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(13, 13)
        Me.Label16.TabIndex = 52
        Me.Label16.Text = "$"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(532, 36)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(13, 13)
        Me.Label15.TabIndex = 48
        Me.Label15.Text = "$"
        '
        'EApplyTax
        '
        Me.EApplyTax.Checked = True
        Me.EApplyTax.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EApplyTax.Location = New System.Drawing.Point(376, 58)
        Me.EApplyTax.Name = "EApplyTax"
        Me.EApplyTax.Size = New System.Drawing.Size(112, 56)
        Me.EApplyTax.TabIndex = 50
        Me.EApplyTax.Text = "Appliquer les taxes sur le profit de transaction"
        Me.EApplyTax.UseVisualStyleBackColor = True
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.Color.Transparent
        Me.Frame2.Controls.Add(Me.nbRestant)
        Me.Frame2.Controls.Add(Me.Label19)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.nbVente)
        Me.Frame2.Controls.Add(Me.nbPret)
        Me.Frame2.Controls.Add(Me.nbTotal)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Location = New System.Drawing.Point(363, 121)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Size = New System.Drawing.Size(240, 85)
        Me.Frame2.TabIndex = 28
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Informations"
        '
        'nbRestant
        '
        Me.nbRestant.BackColor = System.Drawing.Color.Transparent
        Me.nbRestant.Location = New System.Drawing.Point(66, 59)
        Me.nbRestant.Name = "nbRestant"
        Me.nbRestant.Size = New System.Drawing.Size(40, 13)
        Me.nbRestant.TabIndex = 39
        Me.nbRestant.Text = "0"
        Me.nbRestant.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(10, 59)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(59, 13)
        Me.Label19.TabIndex = 38
        Me.Label19.Text = "Restant :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(135, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Prêté :"
        '
        'nbVente
        '
        Me.nbVente.BackColor = System.Drawing.Color.Transparent
        Me.nbVente.Location = New System.Drawing.Point(191, 59)
        Me.nbVente.Name = "nbVente"
        Me.nbVente.Size = New System.Drawing.Size(40, 13)
        Me.nbVente.TabIndex = 37
        Me.nbVente.Text = "0"
        Me.nbVente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nbPret
        '
        Me.nbPret.BackColor = System.Drawing.Color.Transparent
        Me.nbPret.Location = New System.Drawing.Point(191, 22)
        Me.nbPret.Name = "nbPret"
        Me.nbPret.Size = New System.Drawing.Size(40, 13)
        Me.nbPret.TabIndex = 36
        Me.nbPret.Text = "0"
        Me.nbPret.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nbTotal
        '
        Me.nbTotal.BackColor = System.Drawing.Color.Transparent
        Me.nbTotal.Location = New System.Drawing.Point(66, 21)
        Me.nbTotal.Name = "nbTotal"
        Me.nbTotal.Size = New System.Drawing.Size(40, 13)
        Me.nbTotal.TabIndex = 35
        Me.nbTotal.Text = "0"
        Me.nbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(135, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Vendu :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Total :"
        '
        'ajout
        '
        Me.ajout.Enabled = False
        Me.ajout.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ajout.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ajout.Location = New System.Drawing.Point(270, 468)
        Me.ajout.Name = "ajout"
        Me.ajout.Size = New System.Drawing.Size(24, 24)
        Me.ajout.TabIndex = 40
        Me.ajout.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ajout, "Ajouter un équipement")
        '
        'modif
        '
        Me.modif.Enabled = False
        Me.modif.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modif.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modif.Location = New System.Drawing.Point(320, 468)
        Me.modif.Name = "modif"
        Me.modif.Size = New System.Drawing.Size(24, 24)
        Me.modif.TabIndex = 41
        Me.modif.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modif, "Modifier l'équipement sélectionné")
        '
        'delete
        '
        Me.delete.Enabled = False
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delete.Location = New System.Drawing.Point(370, 468)
        Me.delete.Name = "delete"
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 42
        Me.delete.TabStop = False
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer l'équipement sélectionné")
        '
        'vider
        '
        Me.vider.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.vider.Location = New System.Drawing.Point(220, 468)
        Me.vider.Name = "vider"
        Me.vider.Size = New System.Drawing.Size(24, 24)
        Me.vider.TabIndex = 44
        Me.vider.TabStop = False
        Me.ToolTip1.SetToolTip(Me.vider, "Vider les champs")
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(360, 53)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(122, 13)
        Me.Label21.TabIndex = 34
        Me.Label21.Text = "Filtre par catégorie :"
        Me.ToolTip1.SetToolTip(Me.Label21, "Montant avant taxes")
        '
        'fCategorie
        '
        Me.fCategorie.acceptAlpha = True
        Me.fCategorie.acceptedChars = Nothing
        Me.fCategorie.acceptNumeric = True
        Me.fCategorie.allCapital = False
        Me.fCategorie.allLower = False
        Me.fCategorie.autoComplete = True
        Me.fCategorie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.fCategorie.autoSizeDropDown = True
        Me.fCategorie.BackColor = System.Drawing.Color.White
        Me.fCategorie.blockOnMaximum = False
        Me.fCategorie.blockOnMinimum = False
        Me.fCategorie.cb_AcceptLeftZeros = False
        Me.fCategorie.cb_AcceptNegative = False
        Me.fCategorie.currencyBox = False
        Me.fCategorie.Cursor = System.Windows.Forms.Cursors.Default
        Me.fCategorie.dbField = "ECategorie.Categorie"
        Me.fCategorie.doComboDelete = False
        Me.fCategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.fCategorie.firstLetterCapital = True
        Me.fCategorie.firstLettersCapital = False
        Me.fCategorie.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fCategorie.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fCategorie.itemsToolTipDuration = 10000
        Me.fCategorie.Location = New System.Drawing.Point(363, 69)
        Me.fCategorie.manageText = True
        Me.fCategorie.matchExp = ""
        Me.fCategorie.maximum = 0
        Me.fCategorie.minimum = 0
        Me.fCategorie.Name = "fCategorie"
        Me.fCategorie.nbDecimals = CType(-1, Short)
        Me.fCategorie.onlyAlphabet = False
        Me.fCategorie.pathOfList = ""
        Me.fCategorie.ReadOnly = False
        Me.fCategorie.refuseAccents = False
        Me.fCategorie.refusedChars = ""
        Me.fCategorie.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fCategorie.showItemsToolTip = False
        Me.fCategorie.Size = New System.Drawing.Size(240, 22)
        Me.fCategorie.Sorted = True
        Me.fCategorie.TabIndex = 49
        Me.fCategorie.TabStop = False
        Me.fCategorie.trimText = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(360, 12)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(93, 13)
        Me.Label18.TabIndex = 34
        Me.Label18.Text = "Filtre par type :"
        '
        'fPrets
        '
        Me.fPrets.AutoSize = True
        Me.fPrets.Checked = True
        Me.fPrets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.fPrets.Location = New System.Drawing.Point(363, 28)
        Me.fPrets.Name = "fPrets"
        Me.fPrets.Size = New System.Drawing.Size(50, 17)
        Me.fPrets.TabIndex = 50
        Me.fPrets.Text = "Prêts"
        Me.fPrets.UseVisualStyleBackColor = True
        '
        'fVentes
        '
        Me.fVentes.AutoSize = True
        Me.fVentes.Checked = True
        Me.fVentes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.fVentes.Location = New System.Drawing.Point(444, 28)
        Me.fVentes.Name = "fVentes"
        Me.fVentes.Size = New System.Drawing.Size(59, 17)
        Me.fVentes.TabIndex = 50
        Me.fVentes.Text = "Ventes"
        Me.fVentes.UseVisualStyleBackColor = True
        '
        'fInventaire
        '
        Me.fInventaire.AutoSize = True
        Me.fInventaire.Checked = True
        Me.fInventaire.CheckState = System.Windows.Forms.CheckState.Checked
        Me.fInventaire.Location = New System.Drawing.Point(530, 28)
        Me.fInventaire.Name = "fInventaire"
        Me.fInventaire.Size = New System.Drawing.Size(73, 17)
        Me.fInventaire.TabIndex = 50
        Me.fInventaire.Text = "Inventaire"
        Me.fInventaire.UseVisualStyleBackColor = True
        '
        'EquipmentWin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(615, 502)
        Me.Controls.Add(Me.fInventaire)
        Me.Controls.Add(Me.fVentes)
        Me.Controls.Add(Me.fPrets)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.vider)
        Me.Controls.Add(Me.delete)
        Me.Controls.Add(Me.modif)
        Me.Controls.Add(Me.ajout)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.ListEquipement)
        Me.Controls.Add(Me.fCategorie)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label21)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "EquipmentWin"
        Me.ShowInTaskbar = False
        Me.Text = "Équipement"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private acceptedText As String = ""
    Private mySelectionStart As Integer = 0
    Private formModified As Boolean = False
    Private _AllowModification As Boolean = True


#Region "Propriétés"
    Public Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal Value As Boolean)
            _AllowModification = Value
            lockItems(Not Value, True, True)
        End Set
    End Property
#End Region

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal allItems As Boolean = False, Optional ByVal forDroitAcces As Boolean = False)
        ENom.ReadOnly = trueFalse
        EType.ReadOnly = trueFalse
        If allItems = True Then
            EAchat.ReadOnly = trueFalse
            EDepot.ReadOnly = trueFalse
            EDescription.ReadOnly = trueFalse
            EPar.ReadOnly = trueFalse
            EPret.ReadOnly = trueFalse
            ERefundDepot.ReadOnly = trueFalse
            ERefundCout.ReadOnly = trueFalse
            additem.Enabled = Not trueFalse
            removeitem.Enabled = Not trueFalse AndAlso EItems.SelectedItems.Count <> 0
            EVente.ReadOnly = trueFalse
            categorie.ReadOnly = trueFalse
            EApplyTax.Enabled = Not trueFalse

            If trueFalse = False Then
                Select Case EType.SelectedItem.ToString
                    Case "Vente"
                        ERefundDepot.ReadOnly = True
                        ERefundCout.ReadOnly = True
                        EDepot.ReadOnly = True
                        EPret.ReadOnly = True
                        EPar.ReadOnly = True

                    Case "Prêt"
                        EVente.ReadOnly = True

                    Case "Inventaire"
                        ERefundDepot.ReadOnly = True
                        ERefundCout.ReadOnly = True
                        EVente.ReadOnly = True
                        EDepot.ReadOnly = True
                        EPret.ReadOnly = True
                        EPar.ReadOnly = True
                        EApplyTax.Enabled = False
                End Select
            End If
        End If
        btnActivation()
        If forDroitAcces Then
            ajout.Enabled = Not trueFalse
            modif.Enabled = Not trueFalse
            vider.Enabled = Not trueFalse
            ENom.ReadOnly = trueFalse
            EType.ReadOnly = trueFalse
        End If
    End Sub

    Private Sub btnActivation()
        If allowModification = False Then Exit Sub

        If ListEquipement.selected = -1 And (ENom.Text = "" Or EItems.Items.Count = 0) Then
            ajout.Enabled = False
            modif.Enabled = False
        Else
            If ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then
                modif.Enabled = True
                delete.Enabled = True
                ajout.Enabled = False
            ElseIf ListEquipement.selected > -1 Then
                delete.Enabled = False
                modif.Enabled = True
                ajout.Enabled = False
            Else
                delete.Enabled = False
                modif.Enabled = False
                ajout.Enabled = True
            End If
        End If
    End Sub

    Private currentlyLoading As Boolean = False

    Private Sub loading()
        If allowModification = True And ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then
            verifyModified()
            'ListEquipement.selected = -1
        End If

        Dim lastSelected As Integer = -1
        If ListEquipement.selected <> -1 Then lastSelected = CType(ListEquipement.items(ListEquipement.selected).ValueA, Equipment).noEquipment
        Dim curCat As String = fCategorie.Text
        loadCategories()
        currentlyLoading = True
        fCategorie.SelectedIndex = fCategorie.FindStringExact(curCat)
        If fCategorie.SelectedIndex < 0 Then fCategorie.SelectedIndex = 0
        currentlyLoading = False

        ListEquipement.draw = False
        ListEquipement.cls()
        Dim S, addedNo As Integer
        S = -1
        Dim equipements As Generic.List(Of Equipment) = EquipmentsManager.getInstance.getItemables()
        Dim catFilter As Boolean
        Dim typeFilter As Boolean
        For Each curEquip As Equipment In equipements
            catFilter = fCategorie.SelectedIndex = 0 Or curEquip.category.StartsWith(fCategorie.Text & ":") Or curEquip.category = fCategorie.Text
            typeFilter = (fPrets.Checked AndAlso (curEquip.type = Equipment.EquipmentType.Pret Or curEquip.type = Equipment.EquipmentType.PretVente)) Or (fVentes.Checked AndAlso (curEquip.type = Equipment.EquipmentType.Vente Or curEquip.type = Equipment.EquipmentType.PretVente)) Or (fInventaire.Checked = True AndAlso curEquip.type = Equipment.EquipmentType.Inventaire)
            If catFilter AndAlso typeFilter Then
                addedNo = ListEquipement.add(curEquip.toString)
                ListEquipement.ItemValueA(addedNo) = curEquip
                If curEquip.noEquipment = lastSelected Then S = addedNo
            End If
        Next

        formModified = False
        ListEquipement.draw = True
        ListEquipement.selected = S
    End Sub

    Private Sub loadCategories()
        fCategorie.Items.Clear()
        categorie.Items.Clear()

        Dim categories() As String = DBLinker.getInstance.readOneDBField("ECategorie", "Categorie")
        fCategorie.Items.Add("* Toutes les catégories *")
        If categories IsNot Nothing AndAlso categories.Length <> 0 Then
            For i As Integer = 0 To categories.GetUpperBound(0)
                If categories(i) = "" Then Continue For

                fCategorie.Items.Add(categories(i))
                categorie.Items.Add(categories(i))
            Next i
        End If
    End Sub

    Private isLoading As Boolean = False

    Private Sub equipement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EPar.SelectedIndex = 0
        If PreferencesManager.getGeneralPreferences()("DefaultEquipementType") <> "" Then EType.SelectedIndex = EType.FindStringExact(PreferencesManager.getGeneralPreferences()("DefaultEquipementType"))
        If EType.SelectedIndex < 0 Then EType.SelectedIndex = 2
        ERefundDepot.SelectedIndex = ERefundDepot.Items.Count - 1
        ERefundCout.SelectedIndex = 0
        formModified = False 'Important, sinon demande d'enregistré inutilement
        configList(Me.ListEquipement)

        loadCategories()
        If categorie.Items.Count <> 0 Then categorie.SelectedIndex = 0
        allowModification = True
        fCategorie.SelectedIndex = 0
        loading()
        isLoading = False

        'Droit & Accès
        If currentDroitAcces(32) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les équipements." & vbCrLf & "Merci!", "Droit & Accès")
            allowModification = False
        End If
    End Sub

    Private Sub eNom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENom.TextChanged
        btnActivation()
        formModified = True
    End Sub

    Private Sub additem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles additem.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        Dim noItems As String = myInputBoxPlus.Prompt("Veuillez entrer le numéro de l'item", "Ajout d'un item", , True)
        If noItems = "" Then Exit Sub
        Dim NoItem, sNoItems() As String
        sNoItems = noItems.Split(New Char() {"§"})

        For Each NoItem In sNoItems
            If EItems.FindStringExact(NoItem) <> -1 Then
                MessageBox.Show("Le numéro d'item (" & NoItem & ") est déjà utilisé", "Ajout d'un item")
            Else
                EItems.SelectedIndex = EItems.Items.Add(NoItem)
                nbTotal.Text += 1
            End If
        Next NoItem

        btnActivation()
        formModified = True
    End Sub

    Private Sub eItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EItems.SelectedIndexChanged
        If EItems.SelectedIndex <> -1 AndAlso ToolTip1.GetToolTip(Me.modif).StartsWith("Enregistrer") Then
            If ListEquipement.selected < 0 Then
                removeitem.Enabled = True
            Else
                Dim isValid As Boolean = True
                Dim curPretes() As String = CType(ListEquipement.items(ListEquipement.selected).ValueA, Equipment).noItemsBorrowed.ToArray
                If curPretes.Length <> 0 Then
                    For Each curItem As String In Me.EItems.SelectedItems
                        If searchArray(curPretes, curItem, SearchType.ExactMatch) >= 0 Then
                            isValid = False
                            Exit For
                        End If
                    Next
                End If

                removeitem.Enabled = isValid
            End If
        Else
            removeitem.Enabled = False
        End If
    End Sub

    Private Sub removeitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removeitem.Click
        If Me.EItems.SelectedItems.Count = Me.EItems.Items.Count Then MessageBox.Show("Impossible de supprimer tous les numéros d'item. Veuillez en garder au minimum un.", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning) : Exit Sub

        If MessageBox.Show("Êtes-vous sûr de vouloir enlever cet/ces item(s) ?", "Suppression d'un numéro d'item", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim removingItems() As String
        ReDim removingItems(Me.EItems.SelectedItems.Count)
        Me.EItems.SelectedItems.CopyTo(removingItems, 0)
        For Each curItem As String In removingItems
            EItems.Items.Remove(curItem)
        Next

        nbTotal.Text = EItems.Items.Count

        removeitem.Enabled = False
        btnActivation()
        formModified = True
    End Sub

    Private Sub writeData(Optional ByVal updating As Boolean = False)
        Dim curEquip As New Equipment
        If updating Then curEquip = ListEquipement.items(ListEquipement.selected).ValueA

        curEquip.noItems.Clear()
        For i As Integer = 0 To EItems.Items.Count - 1
            curEquip.noItems.Add(EItems.Items(i).ToString)
        Next i

        curEquip.amountPaidByItem = EAchat.Text
        curEquip.applyTax = Me.EApplyTax.Checked
        curEquip.category = Me.categorie.Text
        curEquip.costAmountBy = Double.Parse(Me.EPret.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
        curEquip.costRepetitionBy = Me.EPar.SelectedIndex + 1
        curEquip.depositAmount = Double.Parse(Me.EDepot.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
        curEquip.description = Me.EDescription.Text
        curEquip.item = Me.ENom.Text
        curEquip.costRefundPercentage = Double.Parse(Me.ERefundCout.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
        curEquip.depositRefundPercentage = Double.Parse(Me.ERefundDepot.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
        curEquip.type = Me.EType.SelectedIndex + 1
        curEquip.itemSoldAmount = Double.Parse(Me.EVente.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))

        curEquip.saveData()

        If updating = False Then
            myMainWin.StatusText = "Ajout d'un équipement"
        Else
            myMainWin.StatusText = "Équipement : " & ENom.Text & " enregistré(e)"
        End If

        formModified = False

        loading()
        ListEquipement.selected = ListEquipement.findValue(curEquip)
        If ListEquipement.selected = -1 Then emptyFields()
    End Sub

    Private Sub ajout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ajout.Click
        If adding() = "" Then Exit Sub

        formModified = False
    End Sub

    Private Sub modif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modif.Click
        Dim curEquip As Equipment = ListEquipement.ItemValueA(ListEquipement.selected)

        If ToolTip1.GetToolTip(sender).StartsWith("Modifier") Then
            'Vérification si l'item est en cours de modification + Droit & Accès
            If lockSecteur("Equipement-" & curEquip.noEquipment, True, "Équipement") = True Then
                modif.ImageIndex = 1
                ToolTip1.SetToolTip(modif, "Enregistrer l'équipement en cours")
                lockItems(False, True)
            End If
        Else
            saving()
        End If
    End Sub

    Private Sub listEquipement_SelectedChange() Handles ListEquipement.selectedChange
        If ListEquipement.selected <> -1 Then
            Dim curEquip As Equipment = ListEquipement.ItemValueA(ListEquipement.selected)

            ENom.Text = curEquip.item
            EType.SelectedIndex = curEquip.type - 1
            EItems.Items.Clear()
            EItems.Items.AddRange(curEquip.noItems.ToArray)

            ERefundDepot.Text = curEquip.depositRefundPercentage
            ERefundCout.Text = curEquip.costRefundPercentage
            EVente.Text = curEquip.itemSoldAmount
            EVente.forceManaging()
            EDepot.Text = curEquip.depositAmount
            EDepot.forceManaging()
            EPret.Text = curEquip.costAmountBy
            EPret.forceManaging()
            EPar.SelectedIndex = CByte(curEquip.costRepetitionBy) - 1
            EDescription.Text = curEquip.description
            nbTotal.Text = curEquip.noItems.Count
            nbVente.Text = curEquip.nbSold
            nbPret.Text = curEquip.noItemsBorrowed.Count
            nbRestant.Text = nbTotal.Text - nbPret.Text
            EAchat.Text = curEquip.amountPaidByItem
            EAchat.forceManaging()
            categorie.Text = curEquip.category
            EApplyTax.Checked = curEquip.applyTax

            lockItems(True, True)
            removeitem.Enabled = False
        Else
            emptyFields()
        End If

        formModified = False
    End Sub

    Private Sub emptyFields()
        ENom.Text = ""
        If PreferencesManager.getGeneralPreferences()("DefaultEquipementType") <> "" Then EType.SelectedIndex = EType.FindStringExact(PreferencesManager.getGeneralPreferences()("DefaultEquipementType"))
        If EType.SelectedIndex < 0 Then EType.SelectedIndex = 2
        EItems.Items.Clear()
        ERefundDepot.Text = 100
        ERefundCout.Text = 0
        EVente.Text = "0"
        EDepot.Text = "0"
        EPret.Text = "0"
        EPar.SelectedIndex = 0
        EDescription.Text = ""
        nbTotal.Text = "0"
        nbVente.Text = "0"
        nbPret.Text = "0"
        nbRestant.Text = "0"
        EAchat.Text = "0"
        categorie.Text = ""
        lockItems(False, True)
        delete.Enabled = False
        removeitem.Enabled = False
    End Sub

    Private Sub vider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles vider.Click
        verifyModified()
        ListEquipement.selected = -1
    End Sub

    Private Sub delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet équipement ?", "Suppression d'un équipement", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True
        Dim noEquipement As Integer = CType(ListEquipement.ItemValueA(ListEquipement.selected), Equipment).noEquipment
        Dim myItemsSoldNBorrowed() As String = DBLinker.getInstance.readOneDBField("Ventes WHERE (NoEquipement=" & noEquipement & ") UNION SELECT NoPret FROM Prets", "NoVente", "WHERE (NoEquipement=" & noEquipement & ");")
        If Not myItemsSoldNBorrowed Is Nothing AndAlso myItemsSoldNBorrowed.Length <> 0 Then
            MessageBox.Show("Impossible de supprimer cet équipement. Il a déjà été vendu ou prêté au moins une fois.", "Suppression d'un équipement")
            If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            Exit Sub
        End If

        CType(ListEquipement.ItemValueA(ListEquipement.selected), Equipment).delete()

        lockSecteur("Equipement-" & noEquipement, False, "Équipement")
        modif.ImageIndex = 0
        delete.Enabled = False
        ToolTip1.SetToolTip(modif, "Modifier l'équipement sélectionné")
        formModified = False
        loading()
        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
    End Sub

    Private Sub eType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EType.SelectedIndexChanged
        If allowModification = False Then Exit Sub

        If ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Or ListEquipement.selected = -1 Then lockItems(False, True)

        formModified = True
    End Sub

    Private Sub nbTotal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nbTotal.TextChanged
        Dim MyTotal, myPrêté As Integer
        MyTotal = nbTotal.Text
        myPrêté = nbPret.Text
        nbRestant.Text = MyTotal - myPrêté
    End Sub

    Private Sub comboBoxes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ERefundDepot.SelectedIndexChanged, EPar.SelectedIndexChanged, ERefundCout.SelectedIndexChanged
        formModified = True
    End Sub

    Private Sub textBoxes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EVente.TextChanged, EDepot.TextChanged, EPret.TextChanged, EAchat.TextChanged, EDescription.TextChanged
        formModified = True
    End Sub

    Private Sub listEquipement_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles ListEquipement.willSelect
        verifyModified()
    End Sub

    Private Sub verifyModified()
        If allowModification = True Then
            If formModified = True Then
                If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then
                        modif_Click(ListEquipement, EventArgs.Empty)
                    Else
                        ajout_Click(ListEquipement, EventArgs.Empty)
                    End If
                End If
            End If

            If ListEquipement.selected > -1 And ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then lockSecteur("Equipement-" & CType(ListEquipement.ItemValueA(ListEquipement.selected), Equipment).noEquipment, False, "Équipement")
            ToolTip1.SetToolTip(modif, "Modifier l'équipement sélectionné")
            modif.ImageIndex = 0
        End If

        formModified = False
    End Sub

    Private Function adding() As String
        If ENom.Text.Trim = "" Then
            MessageBox.Show("Le nom de l'item est obligatoire", "Champ obligatoire")
            ENom.Focus()
            Exit Function
        End If
        Dim nomExists() As String = DBLinker.getInstance.readOneDBField("Equipements", "NoEquipement", "WHERE (NomItem='" & ENom.Text.Replace("'", "''") & "');")
        If Not nomExists Is Nothing AndAlso nomExists.Length <> 0 Then MessageBox.Show("Il y a déjà un équipement portant ce nom", "Nom déjà utilisé") : Exit Function

        writeData()

        Return "DONE"
    End Function

    Private Function saving() As String
        If ENom.Text.Trim = "" Then
            MessageBox.Show("Le nom de l'item est obligatoire", "Champ obligatoire")
            ENom.Focus()
            Exit Function
        End If
        Dim curEquip As Equipment = ListEquipement.ItemValueA(ListEquipement.selected)
        If Not ListEquipement.ItemText(ListEquipement.selected) = ENom.Text Then
            Dim nomExists() As String = DBLinker.getInstance.readOneDBField("Equipements", "NoEquipement", "WHERE (NomItem='" & ENom.Text.Replace("'", "''") & "' AND NoEquipement<>" & curEquip.noEquipment & ");")
            If Not nomExists Is Nothing AndAlso nomExists.Length <> 0 Then
                MessageBox.Show("Il y a déjà un équipement portant ce nom", "Nom déjà utilisé")
                ENom.Focus()
                Exit Function
            End If
        End If

        writeData(True)
        formModified = False

        Return "DONE"
    End Function

    Private Sub equipement_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If allowModification = True Then
            If ListEquipement.selected > -1 And ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then lockSecteur("Equipement-" & CType(ListEquipement.ItemValueA(ListEquipement.selected), Equipment).noEquipment, False, "Équipement")

            If formModified = True Then
                If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then
                        If saving() = "" Then e.Cancel = True
                    Else
                        If adding() = "" Then e.Cancel = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub equipement_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If allowModification = True Then
            If ListEquipement.selected > -1 And ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then lockSecteur("Equipement-" & ListEquipement.ItemValueA(ListEquipement.selected), False, "Équipement")

            If formModified = True And ToolTip1.GetToolTip(modif).StartsWith("Enregistrer") Then saving()
        End If
    End Sub

    Private Sub filtersChecks_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fVentes.CheckedChanged, fPrets.CheckedChanged, fInventaire.CheckedChanged
        If isLoading = False AndAlso currentlyLoading = False Then loading()
    End Sub

    Private Sub fCategorie_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fCategorie.SelectedIndexChanged
        If isLoading = False AndAlso currentlyLoading = False Then loading()
    End Sub

    Private Sub categorie_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles categorie.TextChanged
        formModified = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf equipement_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.fromExternal = False OrElse dataReceived.function.StartsWith("Equipement") = False Then Exit Sub

        loading()
    End Sub
End Class
