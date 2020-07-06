Option Strict Off
Option Explicit On
Friend Class addmodifusers
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Chargement des images
        Me.DownTel.Image = DrawingManager.getInstance.getImage("DownArrow.jpg")
        Me.UpTel.Image = DrawingManager.getInstance.getImage("UpArrow.jpg")
        Me.downService.Image = DrawingManager.getInstance.getImage("DownArrow.jpg")
        Me.upService.Image = DrawingManager.getInstance.getImage("UpArrow.jpg")
        Me.AddTel.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.ModifTel.Image = DrawingManager.getInstance.getImage("modifier16.gif")
        Me.DelTel.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))

        Me.choixdate1.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.choixdate2.Image = DrawingManager.getInstance.getImage("selection16.gif")

        buttonsImgList.Images.Add(DrawingManager.getInstance.getImage("ajouter16.gif"))
        buttonsImgList.Images.Add(DrawingManager.getInstance.getImage("save.jpg"))
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents typetravailleur As ManagedCombo
    Public WithEvents choixdate2 As System.Windows.Forms.Button
    Public WithEvents choixdate1 As System.Windows.Forms.Button
    Public WithEvents nopermis As ManagedText
    Public WithEvents mdp2 As ManagedText
    Public WithEvents codepostal1 As ManagedText
    Public WithEvents codepostal2 As ManagedText
    Public WithEvents mdpTested As System.Windows.Forms.TextBox
    Public WithEvents ok As System.Windows.Forms.Button
    Public WithEvents mdp As ManagedText
    Public WithEvents typeuser As ManagedCombo
    Public WithEvents courriel As ManagedText
    Public WithEvents adresse As ManagedText
    Public WithEvents nom As ManagedText
    Public WithEvents _Label1_18 As System.Windows.Forms.Label
    Public WithEvents endjobdate As System.Windows.Forms.Label
    Public WithEvents _Label1_17 As System.Windows.Forms.Label
    Public WithEvents startjobdate As System.Windows.Forms.Label
    Public WithEvents _Label1_16 As System.Windows.Forms.Label
    Public WithEvents _Label1_12 As System.Windows.Forms.Label
    Public WithEvents _Label1_11 As System.Windows.Forms.Label
    Public WithEvents _Label1_8 As System.Windows.Forms.Label
    Public WithEvents nochar As System.Windows.Forms.Label
    Public WithEvents _Label2_2 As System.Windows.Forms.Label
    Public WithEvents _Label1_7 As System.Windows.Forms.Label
    Public WithEvents _Label1_6 As System.Windows.Forms.Label
    Public WithEvents _Label1_5 As System.Windows.Forms.Label
    Public WithEvents _Label1_4 As System.Windows.Forms.Label
    Public WithEvents _Label1_3 As System.Windows.Forms.Label
    Public WithEvents _Label1_2 As System.Windows.Forms.Label
    Public WithEvents _Label1_1 As System.Windows.Forms.Label
    Public WithEvents _Label1_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents url As ManagedText
    Friend WithEvents AdminLabel As System.Windows.Forms.Label
    Friend WithEvents Services As System.Windows.Forms.CheckedListBox
    Public WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents prenom As ManagedText
    Friend WithEvents AddTel As System.Windows.Forms.Button
    Friend WithEvents ModifTel As System.Windows.Forms.Button
    Friend WithEvents DelTel As System.Windows.Forms.Button
    Friend WithEvents Telephones As Clinica.ManagedCombo
    Friend WithEvents Ville As Clinica.ManagedCombo
    Friend WithEvents UpTel As System.Windows.Forms.Button
    Friend WithEvents DownTel As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EnleverToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotConfirmRVOnPasteOfDTRP As System.Windows.Forms.CheckBox
    Public WithEvents upService As System.Windows.Forms.Button
    Public WithEvents downService As System.Windows.Forms.Button
    Friend WithEvents titre As Clinica.ManagedCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.typetravailleur = New ManagedCombo
        Me.choixdate2 = New System.Windows.Forms.Button
        Me.choixdate1 = New System.Windows.Forms.Button
        Me.nopermis = New ManagedText
        Me.mdp2 = New ManagedText
        Me.codepostal1 = New ManagedText
        Me.codepostal2 = New ManagedText
        Me.mdpTested = New System.Windows.Forms.TextBox
        Me.ok = New System.Windows.Forms.Button
        Me.mdp = New ManagedText
        Me.typeuser = New ManagedCombo
        Me.courriel = New ManagedText
        Me.adresse = New ManagedText
        Me.nom = New ManagedText
        Me._Label1_18 = New System.Windows.Forms.Label
        Me.endjobdate = New System.Windows.Forms.Label
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EnleverToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me._Label1_17 = New System.Windows.Forms.Label
        Me.startjobdate = New System.Windows.Forms.Label
        Me._Label1_16 = New System.Windows.Forms.Label
        Me._Label1_12 = New System.Windows.Forms.Label
        Me._Label1_11 = New System.Windows.Forms.Label
        Me._Label1_8 = New System.Windows.Forms.Label
        Me.nochar = New System.Windows.Forms.Label
        Me._Label2_2 = New System.Windows.Forms.Label
        Me._Label1_7 = New System.Windows.Forms.Label
        Me._Label1_6 = New System.Windows.Forms.Label
        Me._Label1_5 = New System.Windows.Forms.Label
        Me._Label1_4 = New System.Windows.Forms.Label
        Me._Label1_3 = New System.Windows.Forms.Label
        Me._Label1_2 = New System.Windows.Forms.Label
        Me._Label1_1 = New System.Windows.Forms.Label
        Me._Label1_0 = New System.Windows.Forms.Label
        Me.url = New ManagedText
        Me.label4 = New System.Windows.Forms.Label
        Me.AdminLabel = New System.Windows.Forms.Label
        Me.Services = New System.Windows.Forms.CheckedListBox
        Me.label1 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AddTel = New System.Windows.Forms.Button
        Me.ModifTel = New System.Windows.Forms.Button
        Me.DelTel = New System.Windows.Forms.Button
        Me.UpTel = New System.Windows.Forms.Button
        Me.DownTel = New System.Windows.Forms.Button
        Me.upService = New System.Windows.Forms.Button
        Me.downService = New System.Windows.Forms.Button
        Me.label2 = New System.Windows.Forms.Label
        Me.prenom = New ManagedText
        Me.Telephones = New ManagedCombo
        Me.Ville = New ManagedCombo
        Me.titre = New ManagedCombo
        Me.NotConfirmRVOnPasteOfDTRP = New System.Windows.Forms.CheckBox
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'typetravailleur
        '
        Me.typetravailleur.acceptAlpha = True
        Me.typetravailleur.acceptedChars = Nothing
        Me.typetravailleur.acceptNumeric = True
        Me.typetravailleur.allCapital = False
        Me.typetravailleur.allLower = False
        Me.typetravailleur.autoComplete = True
        Me.typetravailleur.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.typetravailleur.autoSizeDropDown = True
        Me.typetravailleur.BackColor = System.Drawing.Color.White
        Me.typetravailleur.blockOnMaximum = False
        Me.typetravailleur.blockOnMinimum = False
        Me.typetravailleur.cb_AcceptLeftZeros = False
        Me.typetravailleur.cb_AcceptNegative = False
        Me.typetravailleur.currencyBox = False
        Me.typetravailleur.Cursor = System.Windows.Forms.Cursors.Default
        Me.typetravailleur.dbField = Nothing
        Me.typetravailleur.doComboDelete = True
        Me.typetravailleur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.typetravailleur.firstLetterCapital = False
        Me.typetravailleur.firstLettersCapital = False
        Me.typetravailleur.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.typetravailleur.ForeColor = System.Drawing.SystemColors.WindowText
        Me.typetravailleur.Items.AddRange(New Object() {"Employé (e)", "Travailleur(se) autonome"})
        Me.typetravailleur.itemsToolTipDuration = 10000
        Me.typetravailleur.Location = New System.Drawing.Point(192, 64)
        Me.typetravailleur.manageText = True
        Me.typetravailleur.matchExp = Nothing
        Me.typetravailleur.maximum = 0
        Me.typetravailleur.minimum = 0
        Me.typetravailleur.Name = "typetravailleur"
        Me.typetravailleur.nbDecimals = CType(-1, Short)
        Me.typetravailleur.onlyAlphabet = False
        Me.typetravailleur.pathOfList = Nothing
        Me.typetravailleur.ReadOnly = False
        Me.typetravailleur.refuseAccents = False
        Me.typetravailleur.refusedChars = Nothing
        Me.typetravailleur.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.typetravailleur.showItemsToolTip = False
        Me.typetravailleur.Size = New System.Drawing.Size(169, 22)
        Me.typetravailleur.TabIndex = 12
        Me.typetravailleur.trimText = False
        '
        'choixdate2
        '
        Me.choixdate2.BackColor = System.Drawing.SystemColors.Control
        Me.choixdate2.Cursor = System.Windows.Forms.Cursors.Default
        Me.choixdate2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.choixdate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.choixdate2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.choixdate2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.choixdate2.Location = New System.Drawing.Point(8, 372)
        Me.choixdate2.Name = "choixdate2"
        Me.choixdate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.choixdate2.Size = New System.Drawing.Size(25, 17)
        Me.choixdate2.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.choixdate2, "Choisir la date de fin de travail")
        Me.choixdate2.UseVisualStyleBackColor = False
        '
        'choixdate1
        '
        Me.choixdate1.BackColor = System.Drawing.SystemColors.Control
        Me.choixdate1.Cursor = System.Windows.Forms.Cursors.Default
        Me.choixdate1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.choixdate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.choixdate1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.choixdate1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.choixdate1.Location = New System.Drawing.Point(8, 336)
        Me.choixdate1.Name = "choixdate1"
        Me.choixdate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.choixdate1.Size = New System.Drawing.Size(25, 17)
        Me.choixdate1.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.choixdate1, "Choisir la date du début de travail")
        Me.choixdate1.UseVisualStyleBackColor = False
        '
        'nopermis
        '
        Me.nopermis.acceptAlpha = True
        Me.nopermis.acceptedChars = ""
        Me.nopermis.acceptNumeric = True
        Me.nopermis.AcceptsReturn = True
        Me.nopermis.allCapital = True
        Me.nopermis.allLower = False
        Me.nopermis.BackColor = System.Drawing.SystemColors.Window
        Me.nopermis.blockOnMaximum = False
        Me.nopermis.blockOnMinimum = False
        Me.nopermis.cb_AcceptLeftZeros = False
        Me.nopermis.cb_AcceptNegative = False
        Me.nopermis.currencyBox = False
        Me.nopermis.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nopermis.firstLetterCapital = False
        Me.nopermis.firstLettersCapital = False
        Me.nopermis.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nopermis.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nopermis.Location = New System.Drawing.Point(8, 296)
        Me.nopermis.manageText = True
        Me.nopermis.matchExp = ""
        Me.nopermis.maximum = 0
        Me.nopermis.MaxLength = 0
        Me.nopermis.minimum = 0
        Me.nopermis.Name = "nopermis"
        Me.nopermis.nbDecimals = CType(-1, Short)
        Me.nopermis.onlyAlphabet = False
        Me.nopermis.refuseAccents = False
        Me.nopermis.refusedChars = ""
        Me.nopermis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nopermis.showInternalContextMenu = True
        Me.nopermis.Size = New System.Drawing.Size(169, 20)
        Me.nopermis.TabIndex = 9
        Me.nopermis.trimText = False
        '
        'mdp2
        '
        Me.mdp2.acceptAlpha = True
        Me.mdp2.acceptedChars = ""
        Me.mdp2.acceptNumeric = True
        Me.mdp2.AcceptsReturn = True
        Me.mdp2.allCapital = False
        Me.mdp2.allLower = False
        Me.mdp2.BackColor = System.Drawing.SystemColors.Window
        Me.mdp2.blockOnMaximum = False
        Me.mdp2.blockOnMinimum = False
        Me.mdp2.cb_AcceptLeftZeros = False
        Me.mdp2.cb_AcceptNegative = False
        Me.mdp2.currencyBox = False
        Me.mdp2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mdp2.firstLetterCapital = False
        Me.mdp2.firstLettersCapital = False
        Me.mdp2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mdp2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mdp2.Location = New System.Drawing.Point(192, 144)
        Me.mdp2.manageText = True
        Me.mdp2.matchExp = ""
        Me.mdp2.maximum = 0
        Me.mdp2.MaxLength = 10
        Me.mdp2.minimum = 0
        Me.mdp2.Name = "mdp2"
        Me.mdp2.nbDecimals = CType(-1, Short)
        Me.mdp2.onlyAlphabet = False
        Me.mdp2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.mdp2.refuseAccents = False
        Me.mdp2.refusedChars = ""
        Me.mdp2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mdp2.showInternalContextMenu = True
        Me.mdp2.Size = New System.Drawing.Size(169, 20)
        Me.mdp2.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.mdp2, "10 caractères maximum")
        Me.mdp2.trimText = False
        '
        'codepostal1
        '
        Me.codepostal1.acceptAlpha = True
        Me.codepostal1.acceptedChars = ""
        Me.codepostal1.acceptNumeric = True
        Me.codepostal1.AcceptsReturn = True
        Me.codepostal1.allCapital = True
        Me.codepostal1.allLower = False
        Me.codepostal1.BackColor = System.Drawing.SystemColors.Window
        Me.codepostal1.blockOnMaximum = False
        Me.codepostal1.blockOnMinimum = False
        Me.codepostal1.cb_AcceptLeftZeros = False
        Me.codepostal1.cb_AcceptNegative = False
        Me.codepostal1.currencyBox = False
        Me.codepostal1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.codepostal1.firstLetterCapital = False
        Me.codepostal1.firstLettersCapital = False
        Me.codepostal1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codepostal1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.codepostal1.Location = New System.Drawing.Point(8, 144)
        Me.codepostal1.manageText = True
        Me.codepostal1.matchExp = "A1A"
        Me.codepostal1.maximum = 0
        Me.codepostal1.MaxLength = 3
        Me.codepostal1.minimum = 0
        Me.codepostal1.Name = "codepostal1"
        Me.codepostal1.nbDecimals = CType(-1, Short)
        Me.codepostal1.onlyAlphabet = True
        Me.codepostal1.refuseAccents = True
        Me.codepostal1.refusedChars = ""
        Me.codepostal1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.codepostal1.showInternalContextMenu = True
        Me.codepostal1.Size = New System.Drawing.Size(33, 20)
        Me.codepostal1.TabIndex = 5
        Me.codepostal1.trimText = False
        '
        'codepostal2
        '
        Me.codepostal2.acceptAlpha = True
        Me.codepostal2.acceptedChars = ""
        Me.codepostal2.acceptNumeric = True
        Me.codepostal2.AcceptsReturn = True
        Me.codepostal2.allCapital = True
        Me.codepostal2.allLower = False
        Me.codepostal2.BackColor = System.Drawing.SystemColors.Window
        Me.codepostal2.blockOnMaximum = False
        Me.codepostal2.blockOnMinimum = False
        Me.codepostal2.cb_AcceptLeftZeros = False
        Me.codepostal2.cb_AcceptNegative = False
        Me.codepostal2.currencyBox = False
        Me.codepostal2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.codepostal2.firstLetterCapital = False
        Me.codepostal2.firstLettersCapital = False
        Me.codepostal2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codepostal2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.codepostal2.Location = New System.Drawing.Point(54, 144)
        Me.codepostal2.manageText = True
        Me.codepostal2.matchExp = "1A1"
        Me.codepostal2.maximum = 0
        Me.codepostal2.MaxLength = 3
        Me.codepostal2.minimum = 0
        Me.codepostal2.Name = "codepostal2"
        Me.codepostal2.nbDecimals = CType(-1, Short)
        Me.codepostal2.onlyAlphabet = True
        Me.codepostal2.refuseAccents = True
        Me.codepostal2.refusedChars = ""
        Me.codepostal2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.codepostal2.showInternalContextMenu = True
        Me.codepostal2.Size = New System.Drawing.Size(33, 20)
        Me.codepostal2.TabIndex = 6
        Me.codepostal2.trimText = False
        '
        'mdpTested
        '
        Me.mdpTested.AcceptsReturn = True
        Me.mdpTested.BackColor = System.Drawing.SystemColors.Window
        Me.mdpTested.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mdpTested.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mdpTested.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mdpTested.Location = New System.Drawing.Point(376, 264)
        Me.mdpTested.MaxLength = 0
        Me.mdpTested.Multiline = True
        Me.mdpTested.Name = "mdpTested"
        Me.mdpTested.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mdpTested.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.mdpTested.Size = New System.Drawing.Size(217, 153)
        Me.mdpTested.TabIndex = 38
        Me.mdpTested.Visible = False
        Me.mdpTested.WordWrap = False
        '
        'ok
        '
        Me.ok.BackColor = System.Drawing.SystemColors.Control
        Me.ok.Cursor = System.Windows.Forms.Cursors.Default
        Me.ok.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ok.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ok.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ok.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ok.Location = New System.Drawing.Point(337, 419)
        Me.ok.Name = "ok"
        Me.ok.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ok.Size = New System.Drawing.Size(24, 24)
        Me.ok.TabIndex = 19
        Me.ok.UseVisualStyleBackColor = False
        '
        'mdp
        '
        Me.mdp.acceptAlpha = True
        Me.mdp.acceptedChars = ""
        Me.mdp.acceptNumeric = True
        Me.mdp.AcceptsReturn = True
        Me.mdp.allCapital = False
        Me.mdp.allLower = False
        Me.mdp.BackColor = System.Drawing.SystemColors.Window
        Me.mdp.blockOnMaximum = False
        Me.mdp.blockOnMinimum = False
        Me.mdp.cb_AcceptLeftZeros = False
        Me.mdp.cb_AcceptNegative = False
        Me.mdp.currencyBox = False
        Me.mdp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mdp.firstLetterCapital = False
        Me.mdp.firstLettersCapital = False
        Me.mdp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mdp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mdp.Location = New System.Drawing.Point(192, 104)
        Me.mdp.manageText = True
        Me.mdp.matchExp = ""
        Me.mdp.maximum = 0
        Me.mdp.MaxLength = 10
        Me.mdp.minimum = 0
        Me.mdp.Name = "mdp"
        Me.mdp.nbDecimals = CType(-1, Short)
        Me.mdp.onlyAlphabet = False
        Me.mdp.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.mdp.refuseAccents = False
        Me.mdp.refusedChars = ""
        Me.mdp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mdp.showInternalContextMenu = True
        Me.mdp.Size = New System.Drawing.Size(169, 20)
        Me.mdp.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.mdp, "10 caractères maximum")
        Me.mdp.trimText = False
        '
        'typeuser
        '
        Me.typeuser.acceptAlpha = True
        Me.typeuser.acceptedChars = Nothing
        Me.typeuser.acceptNumeric = True
        Me.typeuser.allCapital = False
        Me.typeuser.allLower = False
        Me.typeuser.autoComplete = True
        Me.typeuser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.typeuser.autoSizeDropDown = True
        Me.typeuser.BackColor = System.Drawing.Color.White
        Me.typeuser.blockOnMaximum = False
        Me.typeuser.blockOnMinimum = False
        Me.typeuser.cb_AcceptLeftZeros = False
        Me.typeuser.cb_AcceptNegative = False
        Me.typeuser.currencyBox = False
        Me.typeuser.Cursor = System.Windows.Forms.Cursors.Default
        Me.typeuser.dbField = Nothing
        Me.typeuser.doComboDelete = True
        Me.typeuser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.typeuser.firstLetterCapital = False
        Me.typeuser.firstLettersCapital = False
        Me.typeuser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.typeuser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.typeuser.itemsToolTipDuration = 10000
        Me.typeuser.Location = New System.Drawing.Point(192, 24)
        Me.typeuser.manageText = True
        Me.typeuser.matchExp = Nothing
        Me.typeuser.maximum = 0
        Me.typeuser.minimum = 0
        Me.typeuser.Name = "typeuser"
        Me.typeuser.nbDecimals = CType(-1, Short)
        Me.typeuser.onlyAlphabet = False
        Me.typeuser.pathOfList = Nothing
        Me.typeuser.ReadOnly = False
        Me.typeuser.refuseAccents = False
        Me.typeuser.refusedChars = Nothing
        Me.typeuser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.typeuser.showItemsToolTip = False
        Me.typeuser.Size = New System.Drawing.Size(169, 22)
        Me.typeuser.TabIndex = 11
        Me.typeuser.trimText = False
        '
        'courriel
        '
        Me.courriel.acceptAlpha = True
        Me.courriel.acceptedChars = "!§#§|§$§%§?§&§*§-§@§.§_§=§+§£§¢§¤§¬§¦§²§³§¼§½§¾§^§¨§¸§`§'§«§»§°§~§¯§´"
        Me.courriel.acceptNumeric = True
        Me.courriel.AcceptsReturn = True
        Me.courriel.allCapital = False
        Me.courriel.allLower = True
        Me.courriel.BackColor = System.Drawing.SystemColors.Window
        Me.courriel.blockOnMaximum = False
        Me.courriel.blockOnMinimum = False
        Me.courriel.cb_AcceptLeftZeros = False
        Me.courriel.cb_AcceptNegative = False
        Me.courriel.currencyBox = False
        Me.courriel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.courriel.firstLetterCapital = False
        Me.courriel.firstLettersCapital = False
        Me.courriel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.courriel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.courriel.Location = New System.Drawing.Point(192, 184)
        Me.courriel.manageText = True
        Me.courriel.matchExp = ""
        Me.courriel.maximum = 0
        Me.courriel.MaxLength = 0
        Me.courriel.minimum = 0
        Me.courriel.Name = "courriel"
        Me.courriel.nbDecimals = CType(-1, Short)
        Me.courriel.onlyAlphabet = True
        Me.courriel.refuseAccents = False
        Me.courriel.refusedChars = ""
        Me.courriel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.courriel.showInternalContextMenu = True
        Me.courriel.Size = New System.Drawing.Size(169, 20)
        Me.courriel.TabIndex = 15
        Me.courriel.trimText = False
        '
        'adresse
        '
        Me.adresse.acceptAlpha = True
        Me.adresse.acceptedChars = ""
        Me.adresse.acceptNumeric = True
        Me.adresse.AcceptsReturn = True
        Me.adresse.allCapital = False
        Me.adresse.allLower = False
        Me.adresse.BackColor = System.Drawing.SystemColors.Window
        Me.adresse.blockOnMaximum = False
        Me.adresse.blockOnMinimum = False
        Me.adresse.cb_AcceptLeftZeros = False
        Me.adresse.cb_AcceptNegative = False
        Me.adresse.currencyBox = False
        Me.adresse.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.adresse.firstLetterCapital = True
        Me.adresse.firstLettersCapital = True
        Me.adresse.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adresse.ForeColor = System.Drawing.SystemColors.WindowText
        Me.adresse.Location = New System.Drawing.Point(8, 64)
        Me.adresse.manageText = True
        Me.adresse.matchExp = ""
        Me.adresse.maximum = 0
        Me.adresse.MaxLength = 0
        Me.adresse.minimum = 0
        Me.adresse.Name = "adresse"
        Me.adresse.nbDecimals = CType(-1, Short)
        Me.adresse.onlyAlphabet = False
        Me.adresse.refuseAccents = False
        Me.adresse.refusedChars = ""
        Me.adresse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adresse.showInternalContextMenu = True
        Me.adresse.Size = New System.Drawing.Size(169, 20)
        Me.adresse.TabIndex = 3
        Me.adresse.trimText = False
        '
        'nom
        '
        Me.nom.acceptAlpha = True
        Me.nom.acceptedChars = " §'§-"
        Me.nom.acceptNumeric = False
        Me.nom.AcceptsReturn = True
        Me.nom.allCapital = False
        Me.nom.allLower = False
        Me.nom.BackColor = System.Drawing.SystemColors.Window
        Me.nom.blockOnMaximum = False
        Me.nom.blockOnMinimum = False
        Me.nom.cb_AcceptLeftZeros = False
        Me.nom.cb_AcceptNegative = False
        Me.nom.currencyBox = False
        Me.nom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nom.firstLetterCapital = True
        Me.nom.firstLettersCapital = True
        Me.nom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nom.Location = New System.Drawing.Point(8, 24)
        Me.nom.manageText = True
        Me.nom.matchExp = ""
        Me.nom.maximum = 0
        Me.nom.MaxLength = 0
        Me.nom.minimum = 0
        Me.nom.Name = "nom"
        Me.nom.nbDecimals = CType(-1, Short)
        Me.nom.onlyAlphabet = True
        Me.nom.refuseAccents = False
        Me.nom.refusedChars = "(§)"
        Me.nom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nom.showInternalContextMenu = True
        Me.nom.Size = New System.Drawing.Size(80, 20)
        Me.nom.TabIndex = 1
        Me.nom.trimText = False
        '
        '_Label1_18
        '
        Me._Label1_18.AutoSize = True
        Me._Label1_18.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_18.Location = New System.Drawing.Point(192, 48)
        Me._Label1_18.Name = "_Label1_18"
        Me._Label1_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_18.Size = New System.Drawing.Size(114, 14)
        Me._Label1_18.TabIndex = 57
        Me._Label1_18.Text = "Type de travailleur :"
        '
        'endjobdate
        '
        Me.endjobdate.AutoSize = True
        Me.endjobdate.BackColor = System.Drawing.SystemColors.Control
        Me.endjobdate.ContextMenuStrip = Me.ContextMenuStrip1
        Me.endjobdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.endjobdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endjobdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.endjobdate.Location = New System.Drawing.Point(39, 373)
        Me.endjobdate.Name = "endjobdate"
        Me.endjobdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.endjobdate.Size = New System.Drawing.Size(133, 14)
        Me.endjobdate.TabIndex = 0
        Me.endjobdate.Text = "Aucune date sélectionnée"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnleverToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(113, 26)
        '
        'EnleverToolStripMenuItem
        '
        Me.EnleverToolStripMenuItem.Name = "EnleverToolStripMenuItem"
        Me.EnleverToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.EnleverToolStripMenuItem.Text = "Enlever"
        '
        '_Label1_17
        '
        Me._Label1_17.AutoSize = True
        Me._Label1_17.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_17.Location = New System.Drawing.Point(8, 356)
        Me._Label1_17.Name = "_Label1_17"
        Me._Label1_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_17.Size = New System.Drawing.Size(82, 14)
        Me._Label1_17.TabIndex = 55
        Me._Label1_17.Text = "Fin de travail :"
        '
        'startjobdate
        '
        Me.startjobdate.AutoSize = True
        Me.startjobdate.BackColor = System.Drawing.SystemColors.Control
        Me.startjobdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.startjobdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startjobdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.startjobdate.Location = New System.Drawing.Point(35, 337)
        Me.startjobdate.Name = "startjobdate"
        Me.startjobdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.startjobdate.Size = New System.Drawing.Size(133, 14)
        Me.startjobdate.TabIndex = 54
        Me.startjobdate.Text = "Aucune date sélectionnée"
        '
        '_Label1_16
        '
        Me._Label1_16.AutoSize = True
        Me._Label1_16.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_16.Location = New System.Drawing.Point(8, 320)
        Me._Label1_16.Name = "_Label1_16"
        Me._Label1_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_16.Size = New System.Drawing.Size(97, 14)
        Me._Label1_16.TabIndex = 53
        Me._Label1_16.Text = "Début de travail :"
        '
        '_Label1_12
        '
        Me._Label1_12.AutoSize = True
        Me._Label1_12.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_12.Location = New System.Drawing.Point(8, 280)
        Me._Label1_12.Name = "_Label1_12"
        Me._Label1_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_12.Size = New System.Drawing.Size(116, 14)
        Me._Label1_12.TabIndex = 49
        Me._Label1_12.Text = "Numéro de permis :"
        '
        '_Label1_11
        '
        Me._Label1_11.AutoSize = True
        Me._Label1_11.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_11.Location = New System.Drawing.Point(8, 240)
        Me._Label1_11.Name = "_Label1_11"
        Me._Label1_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_11.Size = New System.Drawing.Size(39, 14)
        Me._Label1_11.TabIndex = 48
        Me._Label1_11.Text = "Titre :"
        '
        '_Label1_8
        '
        Me._Label1_8.AutoSize = True
        Me._Label1_8.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_8.Location = New System.Drawing.Point(192, 128)
        Me._Label1_8.Name = "_Label1_8"
        Me._Label1_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_8.Size = New System.Drawing.Size(181, 14)
        Me._Label1_8.TabIndex = 41
        Me._Label1_8.Text = "Confirmation du mot de passe :"
        '
        'nochar
        '
        Me.nochar.AutoSize = True
        Me.nochar.BackColor = System.Drawing.SystemColors.Control
        Me.nochar.Cursor = System.Windows.Forms.Cursors.Default
        Me.nochar.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nochar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.nochar.Location = New System.Drawing.Point(368, 104)
        Me.nochar.Name = "nochar"
        Me.nochar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nochar.Size = New System.Drawing.Size(0, 14)
        Me.nochar.TabIndex = 40
        '
        '_Label2_2
        '
        Me._Label2_2.AutoSize = True
        Me._Label2_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label2_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_2.Location = New System.Drawing.Point(43, 147)
        Me._Label2_2.Name = "_Label2_2"
        Me._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_2.Size = New System.Drawing.Size(11, 14)
        Me._Label2_2.TabIndex = 39
        Me._Label2_2.Text = "-"
        '
        '_Label1_7
        '
        Me._Label1_7.AutoSize = True
        Me._Label1_7.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_7.Location = New System.Drawing.Point(192, 88)
        Me._Label1_7.Name = "_Label1_7"
        Me._Label1_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_7.Size = New System.Drawing.Size(88, 14)
        Me._Label1_7.TabIndex = 36
        Me._Label1_7.Text = "Mot de passe :"
        '
        '_Label1_6
        '
        Me._Label1_6.AutoSize = True
        Me._Label1_6.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_6.Location = New System.Drawing.Point(192, 8)
        Me._Label1_6.Name = "_Label1_6"
        Me._Label1_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_6.Size = New System.Drawing.Size(108, 14)
        Me._Label1_6.TabIndex = 35
        Me._Label1_6.Text = "Type d'utilisateur :"
        '
        '_Label1_5
        '
        Me._Label1_5.AutoSize = True
        Me._Label1_5.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_5.Location = New System.Drawing.Point(192, 168)
        Me._Label1_5.Name = "_Label1_5"
        Me._Label1_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_5.Size = New System.Drawing.Size(58, 14)
        Me._Label1_5.TabIndex = 34
        Me._Label1_5.Text = "Courriel :"
        '
        '_Label1_4
        '
        Me._Label1_4.AutoSize = True
        Me._Label1_4.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_4.Location = New System.Drawing.Point(8, 168)
        Me._Label1_4.Name = "_Label1_4"
        Me._Label1_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_4.Size = New System.Drawing.Size(77, 14)
        Me._Label1_4.TabIndex = 31
        Me._Label1_4.Text = "Téléphones :"
        '
        '_Label1_3
        '
        Me._Label1_3.AutoSize = True
        Me._Label1_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_3.Location = New System.Drawing.Point(8, 128)
        Me._Label1_3.Name = "_Label1_3"
        Me._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_3.Size = New System.Drawing.Size(79, 14)
        Me._Label1_3.TabIndex = 30
        Me._Label1_3.Text = "Code postal :"
        '
        '_Label1_2
        '
        Me._Label1_2.AutoSize = True
        Me._Label1_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_2.Location = New System.Drawing.Point(8, 88)
        Me._Label1_2.Name = "_Label1_2"
        Me._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_2.Size = New System.Drawing.Size(37, 14)
        Me._Label1_2.TabIndex = 29
        Me._Label1_2.Text = "Ville :"
        '
        '_Label1_1
        '
        Me._Label1_1.AutoSize = True
        Me._Label1_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_1.Location = New System.Drawing.Point(8, 48)
        Me._Label1_1.Name = "_Label1_1"
        Me._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_1.Size = New System.Drawing.Size(61, 14)
        Me._Label1_1.TabIndex = 28
        Me._Label1_1.Text = "Adresse :"
        '
        '_Label1_0
        '
        Me._Label1_0.AutoSize = True
        Me._Label1_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_0.Location = New System.Drawing.Point(8, 8)
        Me._Label1_0.Name = "_Label1_0"
        Me._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_0.Size = New System.Drawing.Size(38, 14)
        Me._Label1_0.TabIndex = 26
        Me._Label1_0.Text = "Nom :"
        '
        'url
        '
        Me.url.acceptAlpha = True
        Me.url.acceptedChars = ""
        Me.url.acceptNumeric = True
        Me.url.AcceptsReturn = True
        Me.url.allCapital = False
        Me.url.allLower = False
        Me.url.BackColor = System.Drawing.SystemColors.Window
        Me.url.blockOnMaximum = False
        Me.url.blockOnMinimum = False
        Me.url.cb_AcceptLeftZeros = False
        Me.url.cb_AcceptNegative = False
        Me.url.currencyBox = False
        Me.url.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.url.firstLetterCapital = False
        Me.url.firstLettersCapital = False
        Me.url.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.url.ForeColor = System.Drawing.SystemColors.WindowText
        Me.url.Location = New System.Drawing.Point(192, 224)
        Me.url.manageText = True
        Me.url.matchExp = ""
        Me.url.maximum = 0
        Me.url.MaxLength = 0
        Me.url.minimum = 0
        Me.url.Name = "url"
        Me.url.nbDecimals = CType(-1, Short)
        Me.url.onlyAlphabet = False
        Me.url.refuseAccents = False
        Me.url.refusedChars = ""
        Me.url.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.url.showInternalContextMenu = True
        Me.url.Size = New System.Drawing.Size(169, 20)
        Me.url.TabIndex = 16
        Me.url.trimText = False
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.SystemColors.Control
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(192, 208)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(149, 14)
        Me.label4.TabIndex = 59
        Me.label4.Text = "Adresse du site internet :"
        '
        'AdminLabel
        '
        Me.AdminLabel.Location = New System.Drawing.Point(5, 440)
        Me.AdminLabel.Name = "AdminLabel"
        Me.AdminLabel.Size = New System.Drawing.Size(72, 16)
        Me.AdminLabel.TabIndex = 0
        Me.AdminLabel.Text = "FULL BOXES"
        Me.AdminLabel.Visible = False
        '
        'Services
        '
        Me.Services.HorizontalExtent = 10
        Me.Services.HorizontalScrollbar = True
        Me.Services.Location = New System.Drawing.Point(192, 264)
        Me.Services.Name = "Services"
        Me.Services.Size = New System.Drawing.Size(168, 94)
        Me.Services.TabIndex = 17
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(192, 248)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(102, 14)
        Me.label1.TabIndex = 63
        Me.label1.Text = "Services offerts :"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'AddTel
        '
        Me.AddTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddTel.Location = New System.Drawing.Point(16, 208)
        Me.AddTel.Name = "AddTel"
        Me.AddTel.Size = New System.Drawing.Size(24, 24)
        Me.AddTel.TabIndex = 69
        Me.AddTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.AddTel, "Ajout d'un numéro de téléphone")
        '
        'ModifTel
        '
        Me.ModifTel.Enabled = False
        Me.ModifTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ModifTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ModifTel.Location = New System.Drawing.Point(48, 208)
        Me.ModifTel.Name = "ModifTel"
        Me.ModifTel.Size = New System.Drawing.Size(24, 24)
        Me.ModifTel.TabIndex = 68
        Me.ModifTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ModifTel, "Modifier un numéro de téléphone")
        '
        'DelTel
        '
        Me.DelTel.Enabled = False
        Me.DelTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelTel.Location = New System.Drawing.Point(80, 208)
        Me.DelTel.Name = "DelTel"
        Me.DelTel.Size = New System.Drawing.Size(24, 24)
        Me.DelTel.TabIndex = 67
        Me.DelTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DelTel, "Enlever un numéro de téléphone")
        '
        'UpTel
        '
        Me.UpTel.Enabled = False
        Me.UpTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.UpTel.Location = New System.Drawing.Point(112, 208)
        Me.UpTel.Name = "UpTel"
        Me.UpTel.Size = New System.Drawing.Size(24, 24)
        Me.UpTel.TabIndex = 71
        Me.UpTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.UpTel, "Monter un numéro de téléphone")
        '
        'DownTel
        '
        Me.DownTel.Enabled = False
        Me.DownTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DownTel.Location = New System.Drawing.Point(144, 208)
        Me.DownTel.Name = "DownTel"
        Me.DownTel.Size = New System.Drawing.Size(24, 24)
        Me.DownTel.TabIndex = 72
        Me.DownTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DownTel, "Descendre un numéro de téléphone")
        '
        'upService
        '
        Me.upService.BackColor = System.Drawing.SystemColors.Control
        Me.upService.Cursor = System.Windows.Forms.Cursors.Default
        Me.upService.Enabled = False
        Me.upService.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.upService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.upService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.upService.Location = New System.Drawing.Point(239, 363)
        Me.upService.Name = "upService"
        Me.upService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.upService.Size = New System.Drawing.Size(24, 24)
        Me.upService.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.upService, "Monter le service sélectionné")
        Me.upService.UseVisualStyleBackColor = False
        '
        'downService
        '
        Me.downService.BackColor = System.Drawing.SystemColors.Control
        Me.downService.Cursor = System.Windows.Forms.Cursors.Default
        Me.downService.Enabled = False
        Me.downService.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.downService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.downService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.downService.Location = New System.Drawing.Point(286, 364)
        Me.downService.Name = "downService"
        Me.downService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.downService.Size = New System.Drawing.Size(24, 24)
        Me.downService.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.downService, "Descendre le service sélectionné")
        Me.downService.UseVisualStyleBackColor = False
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(96, 8)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(56, 14)
        Me.label2.TabIndex = 64
        Me.label2.Text = "Prénom :"
        '
        'prenom
        '
        Me.prenom.acceptAlpha = True
        Me.prenom.acceptedChars = " §'§-"
        Me.prenom.acceptNumeric = False
        Me.prenom.AcceptsReturn = True
        Me.prenom.allCapital = False
        Me.prenom.allLower = False
        Me.prenom.BackColor = System.Drawing.SystemColors.Window
        Me.prenom.blockOnMaximum = False
        Me.prenom.blockOnMinimum = False
        Me.prenom.cb_AcceptLeftZeros = False
        Me.prenom.cb_AcceptNegative = False
        Me.prenom.currencyBox = False
        Me.prenom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.prenom.firstLetterCapital = True
        Me.prenom.firstLettersCapital = True
        Me.prenom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prenom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.prenom.Location = New System.Drawing.Point(96, 24)
        Me.prenom.manageText = True
        Me.prenom.matchExp = ""
        Me.prenom.maximum = 0
        Me.prenom.MaxLength = 0
        Me.prenom.minimum = 0
        Me.prenom.Name = "prenom"
        Me.prenom.nbDecimals = CType(-1, Short)
        Me.prenom.onlyAlphabet = True
        Me.prenom.refuseAccents = False
        Me.prenom.refusedChars = "(§)"
        Me.prenom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.prenom.showInternalContextMenu = True
        Me.prenom.Size = New System.Drawing.Size(80, 20)
        Me.prenom.TabIndex = 2
        Me.prenom.trimText = False
        '
        'Telephones
        '
        Me.Telephones.acceptAlpha = True
        Me.Telephones.acceptedChars = Nothing
        Me.Telephones.acceptNumeric = True
        Me.Telephones.allCapital = False
        Me.Telephones.allLower = False
        Me.Telephones.autoComplete = True
        Me.Telephones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Telephones.autoSizeDropDown = True
        Me.Telephones.BackColor = System.Drawing.Color.White
        Me.Telephones.blockOnMaximum = False
        Me.Telephones.blockOnMinimum = False
        Me.Telephones.cb_AcceptLeftZeros = False
        Me.Telephones.cb_AcceptNegative = False
        Me.Telephones.currencyBox = False
        Me.Telephones.dbField = Nothing
        Me.Telephones.doComboDelete = True
        Me.Telephones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Telephones.firstLetterCapital = False
        Me.Telephones.firstLettersCapital = False
        Me.Telephones.itemsToolTipDuration = 10000
        Me.Telephones.Location = New System.Drawing.Point(8, 184)
        Me.Telephones.manageText = True
        Me.Telephones.matchExp = Nothing
        Me.Telephones.maximum = 0
        Me.Telephones.minimum = 0
        Me.Telephones.Name = "Telephones"
        Me.Telephones.nbDecimals = CType(-1, Short)
        Me.Telephones.onlyAlphabet = False
        Me.Telephones.pathOfList = Nothing
        Me.Telephones.ReadOnly = False
        Me.Telephones.refuseAccents = False
        Me.Telephones.refusedChars = ""
        Me.Telephones.showItemsToolTip = False
        Me.Telephones.Size = New System.Drawing.Size(168, 22)
        Me.Telephones.TabIndex = 7
        Me.Telephones.trimText = False
        '
        'Ville
        '
        Me.Ville.acceptAlpha = True
        Me.Ville.acceptedChars = " §'§-§.§/§|§\§(§)"
        Me.Ville.acceptNumeric = False
        Me.Ville.allCapital = False
        Me.Ville.allLower = False
        Me.Ville.autoComplete = True
        Me.Ville.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Ville.autoSizeDropDown = True
        Me.Ville.BackColor = System.Drawing.Color.White
        Me.Ville.blockOnMaximum = False
        Me.Ville.blockOnMinimum = False
        Me.Ville.cb_AcceptLeftZeros = False
        Me.Ville.cb_AcceptNegative = False
        Me.Ville.currencyBox = False
        Me.Ville.Cursor = System.Windows.Forms.Cursors.Default
        Me.Ville.dbField = "Villes.NomVille"
        Me.Ville.doComboDelete = True
        Me.Ville.firstLetterCapital = True
        Me.Ville.firstLettersCapital = True
        Me.Ville.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ville.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Ville.itemsToolTipDuration = 10000
        Me.Ville.Location = New System.Drawing.Point(8, 104)
        Me.Ville.manageText = True
        Me.Ville.matchExp = ""
        Me.Ville.maximum = 0
        Me.Ville.minimum = 0
        Me.Ville.Name = "Ville"
        Me.Ville.nbDecimals = CType(-1, Short)
        Me.Ville.onlyAlphabet = True
        Me.Ville.pathOfList = ""
        Me.Ville.ReadOnly = False
        Me.Ville.refuseAccents = False
        Me.Ville.refusedChars = ""
        Me.Ville.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Ville.showItemsToolTip = False
        Me.Ville.Size = New System.Drawing.Size(168, 22)
        Me.Ville.Sorted = True
        Me.Ville.TabIndex = 4
        Me.Ville.trimText = False
        '
        'titre
        '
        Me.titre.acceptAlpha = True
        Me.titre.acceptedChars = ""
        Me.titre.acceptNumeric = True
        Me.titre.allCapital = False
        Me.titre.allLower = False
        Me.titre.autoComplete = True
        Me.titre.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.titre.autoSizeDropDown = True
        Me.titre.BackColor = System.Drawing.Color.White
        Me.titre.blockOnMaximum = False
        Me.titre.blockOnMinimum = False
        Me.titre.cb_AcceptLeftZeros = False
        Me.titre.cb_AcceptNegative = False
        Me.titre.currencyBox = False
        Me.titre.dbField = "Titres.Titre"
        Me.titre.doComboDelete = True
        Me.titre.firstLetterCapital = True
        Me.titre.firstLettersCapital = False
        Me.titre.itemsToolTipDuration = 10000
        Me.titre.Location = New System.Drawing.Point(8, 256)
        Me.titre.manageText = True
        Me.titre.matchExp = Nothing
        Me.titre.maximum = 0
        Me.titre.minimum = 0
        Me.titre.Name = "titre"
        Me.titre.nbDecimals = CType(-1, Short)
        Me.titre.onlyAlphabet = False
        Me.titre.pathOfList = Nothing
        Me.titre.ReadOnly = False
        Me.titre.refuseAccents = False
        Me.titre.refusedChars = ""
        Me.titre.showItemsToolTip = False
        Me.titre.Size = New System.Drawing.Size(168, 22)
        Me.titre.Sorted = True
        Me.titre.TabIndex = 8
        Me.titre.trimText = False
        '
        'NotConfirmRVOnPasteOfDTRP
        '
        Me.NotConfirmRVOnPasteOfDTRP.Location = New System.Drawing.Point(8, 395)
        Me.NotConfirmRVOnPasteOfDTRP.Name = "NotConfirmRVOnPasteOfDTRP"
        Me.NotConfirmRVOnPasteOfDTRP.Size = New System.Drawing.Size(272, 61)
        Me.NotConfirmRVOnPasteOfDTRP.TabIndex = 73
        Me.NotConfirmRVOnPasteOfDTRP.Text = "Désactiver le questionnement lors d'une prise d'un rendez-vous d'un même agenda o" & _
            "ù le thérapeute réel n'est pas le thérapeute traitant"
        Me.NotConfirmRVOnPasteOfDTRP.UseVisualStyleBackColor = True
        '
        'addmodifusers
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(370, 455)
        Me.Controls.Add(Me.AdminLabel)
        Me.Controls.Add(Me.mdpTested)
        Me.Controls.Add(Me.titre)
        Me.Controls.Add(Me.choixdate2)
        Me.Controls.Add(Me.choixdate1)
        Me.Controls.Add(Me.Ville)
        Me.Controls.Add(Me.Telephones)
        Me.Controls.Add(Me.prenom)
        Me.Controls.Add(Me.url)
        Me.Controls.Add(Me.nopermis)
        Me.Controls.Add(Me.mdp2)
        Me.Controls.Add(Me.codepostal1)
        Me.Controls.Add(Me.mdp)
        Me.Controls.Add(Me.codepostal2)
        Me.Controls.Add(Me.courriel)
        Me.Controls.Add(Me.adresse)
        Me.Controls.Add(Me.nom)
        Me.Controls.Add(Me.Services)
        Me.Controls.Add(Me.typetravailleur)
        Me.Controls.Add(Me.typeuser)
        Me.Controls.Add(Me.DownTel)
        Me.Controls.Add(Me.UpTel)
        Me.Controls.Add(Me.AddTel)
        Me.Controls.Add(Me.ModifTel)
        Me.Controls.Add(Me.DelTel)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me._Label1_18)
        Me.Controls.Add(Me._Label1_16)
        Me.Controls.Add(Me.endjobdate)
        Me.Controls.Add(Me._Label1_17)
        Me.Controls.Add(Me.startjobdate)
        Me.Controls.Add(Me._Label1_12)
        Me.Controls.Add(Me._Label1_11)
        Me.Controls.Add(Me._Label1_8)
        Me.Controls.Add(Me._Label2_2)
        Me.Controls.Add(Me._Label1_7)
        Me.Controls.Add(Me._Label1_6)
        Me.Controls.Add(Me._Label1_5)
        Me.Controls.Add(Me._Label1_4)
        Me.Controls.Add(Me._Label1_3)
        Me.Controls.Add(Me._Label1_2)
        Me.Controls.Add(Me._Label1_1)
        Me.Controls.Add(Me._Label1_0)
        Me.Controls.Add(Me.nochar)
        Me.Controls.Add(Me.downService)
        Me.Controls.Add(Me.upService)
        Me.Controls.Add(Me.ok)
        Me.Controls.Add(Me.NotConfirmRVOnPasteOfDTRP)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.Name = "addmodifusers"
        Me.ShowInTaskbar = False
        Me.Text = "Titre Ajout ou Modif"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private ctrl_onoff As Boolean
    Private isLoaded As Boolean = False
    Private formModified As Boolean = False
    Private serviceCheckedBySoft As Boolean = False
    Private itemDatas() As String
    Private OldMDP, oldCle As String
    Private _UserID As Integer
    Private lockedUser As Boolean = False
    Private isLoading As Boolean = False
    Private oldType As String = ""
    Private oldIsTherapist As Boolean = False
    Private buttonsImgList As New ImageList
    Private curUser As New User()

#Region "Propriétés"
    Public Property userID() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal Value As Integer)
            _UserID = Value
        End Set
    End Property
#End Region

#Region "Formulaire"
    Private Sub upTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpTel.Click
        Dim SPhones(), selItem As String
        Dim curIndex As Integer
        curIndex = Telephones.SelectedIndex
        ReDim SPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(SPhones, 0)

        selItem = SPhones(curIndex)
        SPhones(curIndex) = SPhones(curIndex - 1)
        SPhones(curIndex - 1) = selItem
        Telephones.Items.Clear()
        Telephones.Items.AddRange(SPhones)
        Telephones.SelectedIndex = curIndex - 1
        formModified = True
    End Sub

    Private Sub downTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownTel.Click
        Dim SPhones(), selItem As String
        Dim curIndex As Integer
        curIndex = Telephones.SelectedIndex
        ReDim SPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(SPhones, 0)

        selItem = SPhones(curIndex)
        SPhones(curIndex) = SPhones(curIndex + 1)
        SPhones(curIndex + 1) = selItem
        Telephones.Items.Clear()
        Telephones.Items.AddRange(SPhones)
        Telephones.SelectedIndex = curIndex + 1
        formModified = True
    End Sub

    Private Sub chooseDate(ByVal curDateControl As Control)
        Dim myDateChoice As New DateChoice()
        Dim curDate() As String
        Dim myDate As Date = Nothing
        If curDateControl.Text <> "Aucune date sélectionnée" Then
            curDate = curDateControl.Text.Split(New Char() {"/"})
            myDate = New Date(curDate(2), curDate(1), curDate(0))
        Else
            myDate = Date.Now
        End If
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Today.Year + 1, , , , , , True, , , , , myDate)
        If dateReturn.Count = 0 Then Exit Sub

        curDateControl.Text = DateFormat.getTextDate(dateReturn(0), DateFormat.TextDateOptions.DDMMYYYY)
        formModified = True
    End Sub

    Private Sub choixdate1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles choixdate1.Click
        If startjobdate.Text <> "Aucune date sélectionnée" And Me.Text.StartsWith("Modif") Then If MessageBox.Show("Êtes-vous sûr de vouloir remodifier la date de début ?", "Confirmation de modification", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        chooseDate(startjobdate)
    End Sub

    Private Sub choixdate2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles choixdate2.Click
        chooseDate(endjobdate)
    End Sub

    Private Sub nom_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nom.KeyUp
        If e.KeyCode = 13 Then Me.GetNextControl(sender, True).Focus()
        e.Handled = True
    End Sub

    Private Sub typeuser_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles typeuser.SelectedIndexChanged
        If isLoading = True Then Exit Sub

        If typeuser.GetItemText(typeuser.SelectedItem).ToUpper = "PERSONNALISÉ" Then
            Dim no As Integer
            Dim daLine As String
            Dim myTypesUser As New typesuser(Me)
            'TODO : Test search array
            no = searchArray(itemDatas, typeuser.GetItemText(typeuser.SelectedItem) & "§", SearchType.StartsWith)
            daLine = itemDatas(no)
            daLine = Microsoft.VisualBasic.Right(daLine, daLine.Length - typeuser.GetItemText(typeuser.SelectedItem).Length - 1)

            Dim oldDALine As String = daLine

            daLine = myTypesUser.personnalize(daLine)
            itemDatas(no) = typeuser.GetItemText(typeuser.SelectedItem) & "§" & daLine

            If oldDALine <> daLine Then formModified = True
        Else
            formModified = True
        End If
    End Sub

    Private Sub codepostal1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles codepostal1.TextChanged
        If codepostal1.Text.Length = 3 Then Me.GetNextControl(sender, True).Focus()
        formModified = True
    End Sub

    Private Sub codepostal2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles codepostal2.TextChanged
        If codepostal2.Text.Length = 3 Then Me.GetNextControl(sender, True).Focus()
        formModified = True
    End Sub

    Private Sub choixdates_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles choixdate1.KeyUp, choixdate2.KeyUp
        If e.KeyCode = 8 Then Me.GetNextControl(sender, False).Focus()
    End Sub

    Private Sub addTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTel.Click
        Dim myInputBoxPlus As New InputBoxPlus(True, , "TelePhoneTitles.TelephoneTitle")
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ":"
        Dim newTitle As String = myInputBoxPlus("Veuillez entrer le titre du numéro de téléphone", "Titre")
        If newTitle = "" Then Exit Sub

        myInputBoxPlus = New InputBoxPlus()
        myInputBoxPlus.acceptAlpha = False
        myInputBoxPlus.acceptedChars = "-§#"
        myInputBoxPlus.refusedChars = ":"

        Dim newTel As String = myInputBoxPlus("Veuillez entrer le numéro de téléphone", "Numéro de téléphone")
        If newTel = "" Then Exit Sub

        DBHelper.addItemToADBList("TelephoneTitles", "TelephoneTitle", newTitle, "NoTelephoneTitle")

        Dim n As Integer = Telephones.Items.Add(newTitle & ":" & newTel)
        Telephones.SelectedIndex = n
        formModified = True
    End Sub

    Private Sub telephones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Telephones.SelectedIndexChanged
        If Telephones.SelectedIndex <> -1 And Me.AddTel.Enabled = True Then
            ModifTel.Enabled = True
            DelTel.Enabled = True
            If Telephones.Items.Count > 1 Then
                If Telephones.SelectedIndex = 0 Then
                    UpTel.Enabled = False
                Else
                    UpTel.Enabled = True
                End If
                If Telephones.SelectedIndex = (Telephones.Items.Count - 1) Then
                    DownTel.Enabled = False
                Else
                    DownTel.Enabled = True
                End If
            Else
                UpTel.Enabled = False
                DownTel.Enabled = False
            End If
        Else
            ModifTel.Enabled = False
            DelTel.Enabled = False
            UpTel.Enabled = False
            DownTel.Enabled = False
        End If
    End Sub

    Private Sub modifTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifTel.Click
        Dim myPhone() As String = Telephones.GetItemText(Telephones.SelectedItem).Split(New Char() {":"})

        Dim myInputBoxPlus As New InputBoxPlus(True, , "TelePhoneTitles.TelephoneTitle")
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ":"
        Dim newTitle As String = myInputBoxPlus("Veuillez entrer le titre du numéro de téléphone", "Titre", myPhone(0))
        If newTitle = "" Then Exit Sub

        myInputBoxPlus = New InputBoxPlus()
        myInputBoxPlus.acceptAlpha = False
        myInputBoxPlus.acceptedChars = "-§#"
        myInputBoxPlus.refusedChars = ":"

        Dim newTel As String = myInputBoxPlus("Veuillez entrer le numéro de téléphone", "Numéro de téléphone", myPhone(1))
        If newTel = "" Then Exit Sub

        DBHelper.addItemToADBList("TelephoneTitles", "TelephoneTitle", newTitle, "NoTelephoneTitle")

        Telephones.Items(Telephones.SelectedIndex) = newTitle & ":" & newTel
        formModified = True
    End Sub

    Private Sub delTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelTel.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce numéro de téléphone ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Telephones.Items.RemoveAt(Telephones.SelectedIndex)
        DownTel.Enabled = False
        UpTel.Enabled = False
        ModifTel.Enabled = False
        DelTel.Enabled = False
        formModified = True
    End Sub

    Private Sub changeFocusObject(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles codepostal1.KeyUp, codepostal2.KeyUp, mdp.KeyUp, mdp2.KeyUp, nopermis.KeyUp, Telephones.KeyUp, typetravailleur.KeyUp, typeuser.KeyUp, Ville.KeyUp, courriel.KeyUp, url.KeyUp, adresse.KeyUp, prenom.KeyUp, titre.KeyUp
        If e.KeyCode = 13 Then Me.GetNextControl(sender, True).Focus()
        If e.KeyCode = 8 Then
            Try
                If (TypeOf (sender) Is ComboBox AndAlso CType(sender, ComboBox).DropDownStyle = ComboBoxStyle.DropDown AndAlso sender.Text = "") Or sender.Text = "" Then Me.GetNextControl(sender, False).Focus()
            Catch
                Me.GetNextControl(sender, False).Focus()
            End Try
        End If
        e.Handled = True
    End Sub
#End Region

#Region "External functions"
    Public Sub loading()
        Services.Items.AddRange(PreferencesManager.getGeneralPreferences()("Services").Split(New Char() {vbTab}))

        Dim No, i As Integer
        typetravailleur.SelectedIndex = 0

        If currentUserName = "Administrateur" Then
            mdp.PasswordChar = CChar("")
            mdp2.PasswordChar = CChar("")
        Else
            mdp.PasswordChar = CChar("*")
            mdp2.PasswordChar = CChar("*")
        End If

        typeuser.Items.Clear()
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim results(,) As String = DBLinker.getInstance.readDB("TypeUtilisateur", "TypeUtilisateur.NoType, TypeUtilisateur.NomType, TypeUtilisateur.DroitAcces, TypeUtilisateur.IsTherapist")
        If Not results Is Nothing AndAlso results.Length <> 0 Then
            ReDim itemDatas(results.GetUpperBound(1) + 1)
            ReDim noTypes(results.GetUpperBound(1) + 1)
            For i = 0 To results.GetUpperBound(1)
                typeuser.Items.Add(results(1, i))
                itemDatas(i) = results(2, i)
                If results(3, i) = True And itemDatas(i).StartsWith("3") = False Then itemDatas(i) = "3" & itemDatas(i)
                itemDatas(i) = results(1, i) & "§" & itemDatas(i)
                noTypes(i) = results(1, i) & "§" & results(0, i)
            Next i
        End If

        Ville.Items.Clear()
        Dim villes() As String = DBLinker.getInstance.readOneDBField("Villes", "NomVille", , True)
        If Not villes Is Nothing AndAlso villes.Length <> 0 Then Ville.Items.AddRange(villes)

        titre.Items.Clear()
        Dim titres() As String = DBLinker.getInstance.readOneDBField("Titres", "Titre", , True)
        If Not titres Is Nothing AndAlso titres.Length <> 0 Then titre.Items.AddRange(titres)

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        isLoading = True
        No = typeuser.Items.Count
        typeuser.Items.Insert(No, "Personnalisé")
        If itemDatas Is Nothing OrElse itemDatas.Length = 0 Then
            ReDim itemDatas(0)
            ReDim noTypes(0)
        End If
        itemDatas(No) = "Personnalisé§20"
        noTypes(No) = "Personnalisé§0"
        typeuser.SelectedIndex = 0
        isLoading = False
    End Sub

    Public Sub addUsers()
        loading()
        Me.Text = "Ajout d'un utilisateur"
        ToolTip1.SetToolTip(ok, "Ajouter un utilisateur")
        ok.Image = buttonsImgList.Images(0)

        Me.startjobdate.Text = DateFormat.getTextDate(Date.Today, DateFormat.TextDateOptions.DDMMYYYY)
        nom.Enabled = True
        choixdate1.Enabled = True
        choixdate2.Enabled = False
        formModified = False
        isLoaded = True
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        nom.ReadOnly = trueFalse
        prenom.ReadOnly = trueFalse
        ok.Enabled = Not trueFalse
        adresse.ReadOnly = trueFalse
        Ville.ReadOnly = trueFalse
        codepostal1.ReadOnly = trueFalse
        codepostal2.ReadOnly = trueFalse
        Telephones.BackColor = IIf(trueFalse, SystemColors.ControlLight, Color.White)
        AddTel.Enabled = Not trueFalse
        ModifTel.Enabled = Not trueFalse
        DelTel.Enabled = Not trueFalse
        UpTel.Enabled = Not trueFalse
        DownTel.Enabled = Not trueFalse
        courriel.ReadOnly = trueFalse
        titre.ReadOnly = trueFalse
        nopermis.ReadOnly = trueFalse
        choixdate1.Enabled = Not trueFalse
        choixdate2.Enabled = Not trueFalse
        typeuser.ReadOnly = trueFalse
        mdp.ReadOnly = trueFalse
        mdp2.ReadOnly = trueFalse
        url.ReadOnly = trueFalse
        typetravailleur.ReadOnly = trueFalse
        Services.Enabled = Not trueFalse
        upService.Enabled = Not trueFalse
        downService.Enabled = Not trueFalse
    End Sub

    Public Sub modifUser(ByRef userToShow As Integer)
        loading()
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance.getImage("user.gif"))
        ToolTip1.SetToolTip(ok, "Modifier l'utilisateur")
        ok.Image = buttonsImgList.Images(1)
        userID = userToShow

        Dim i As Short
        Me.Text = "Modification d'un utilisateur"
        choixdate2.Enabled = True

        curUser = UsersManager.getInstance.getUser(userToShow)
        If curUser Is Nothing Then ok.Enabled = False : MessageBox.Show("L'utilisateur demandé n'est pas présent dans la base de données", "Erreur") : Exit Sub

        If lockSecteur("ModifUser-" & userToShow & ".lock", True, "Modification d'un utilisateur") = False Then
            lockItems(True)
        Else
            lockedUser = True
        End If

        oldCle = curUser.passwordKey
        OldMDP = curUser.password
        If OldMDP <> "00" And OldMDP <> "" Then
            mdp.PasswordChar = Nothing
            mdp.Text = "Déjà entré."
            mdp2.PasswordChar = Nothing
            mdp2.Text = "Déjà entré."
        End If
        nom.Text = curUser.lastName
        prenom.Text = curUser.firstName
        If curUser.noType = 0 Then
            isLoading = True
            typeuser.Text = "Personnalisé"
            itemDatas(itemDatas.GetUpperBound(0)) = "Personnalisé§" & curUser.rights
            isLoading = False
        Else
            typeuser.Text = curUser.getUserType().toString()
        End If

        oldType = typeuser.Text
        oldIsTherapist = curUser.isTherapist

        url.Text = curUser.url
        adresse.Text = curUser.address
        Ville.Text = curUser.city
        codepostal1.Text = Microsoft.VisualBasic.Left(curUser.postalCode, 3)
        codepostal2.Text = Microsoft.VisualBasic.Right(curUser.postalCode, 3)
        If curUser.telephones <> "" Then
            Telephones.Items.AddRange(curUser.telephones.Split(New Char() {"§"}))
            Telephones.SelectedIndex = 0
        End If
        courriel.Text = curUser.email
        titre.Text = curUser.title
        nopermis.Text = curUser.noPermit
        If curUser.startingDate.Equals(LIMIT_DATE) = False Then
            startjobdate.Text = curUser.startingDate.Day & "/" & curUser.startingDate.Month & "/" & curUser.startingDate.Year
        End If

        If curUser.endingDate.Equals(LIMIT_DATE) = False Then
            endjobdate.Text = curUser.endingDate.Day & "/" & curUser.endingDate.Month & "/" & curUser.endingDate.Year
        End If
        typetravailleur.SelectedIndex = curUser.noEmployeeType - 1

        serviceCheckedBySoft = True
        Try
            Dim lastChecked As Integer = 0
            Dim curServices() As String = curUser.services.Split(New Char() {"§"})
            For i = 0 To curServices.Length - 1
                Dim curIndex As Integer = Services.Items.IndexOf(curServices(i))
                If curIndex <> -1 Then
                    Services.Items.RemoveAt(curIndex)
                    Services.Items.Insert(lastChecked, curServices(i))
                    Services.SetItemChecked(lastChecked, True)
                    lastChecked += 1
                End If
            Next i
        Catch
        End Try
        serviceCheckedBySoft = False

        NotConfirmRVOnPasteOfDTRP.Checked = curUser.notConfirmRVOnPasteOfDTRP

        isLoaded = True
        formModified = False
    End Sub
#End Region

    Private Sub sortServices()
        serviceCheckedBySoft = True
        Services.Sorted = True
        Services.Sorted = False

        Dim lastChecked As Integer = 0
        For i As Integer = 0 To Services.Items.Count - 1
            If Services.GetItemChecked(i) = True Then
                If lastChecked <> i Then
                    Services.Items.Insert(lastChecked, Services.Items(i))
                    Services.SetItemChecked(lastChecked, True)
                    Services.Items.RemoveAt(i + 1)
                    'i -= 1
                End If
                lastChecked += 1
            End If
        Next i
        serviceCheckedBySoft = False
    End Sub

    Private Sub ok_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ok.Click
        If saving() <> "" Then Me.Close()
    End Sub

    Private Function saving() As String
        Dim NoType, SPhones(), Phones, ModifMDP, Cle, DALine, sDate() As String, MyServices As String = ""
        Dim i As Short
        Dim isTherapist As Boolean = False
        Dim StartDate, endDate As Date
        Cle = DateFormat.getTextDate(Date.Today) & Date.Now.ToString("HH:mm:ss")

        'Validation des champs
        Dim allowUserEmptyPassword As Boolean = PreferencesManager.getGeneralPreferences()("AllowUserEmptyPassword")
        If Not allowUserEmptyPassword AndAlso mdp.Text = "" Then mdp.Focus() : MessageBox.Show("Veuillez entrer un Mot de passe", "Champ vide") : Exit Function
        If nom.Text = "" Then nom.Focus() : MessageBox.Show("Veuillez entrer un Nom", "Champ vide") : Exit Function
        If prenom.Text = "" Then prenom.Focus() : MessageBox.Show("Veuillez entrer un Prénom", "Champ vide") : Exit Function
        If adresse.Text = "" Then adresse.Focus() : MessageBox.Show("Veuillez entrer un Adresse", "Champ vide") : Exit Function
        If Ville.Text = "" Then Ville.Focus() : MessageBox.Show("Veuillez entrer une Ville", "Champ vide") : Exit Function
        If titre.Text = "" Then titre.Focus() : MessageBox.Show("Veuillez entrer un Titre", "Champ vide") : Exit Function
        If codepostal1.Text = "" Or codepostal2.Text = "" Or codepostal1.Text.Length < 3 Or codepostal2.Text.Length < 3 Then codepostal1.Focus() : MessageBox.Show("Veuillez entrer un Code Postal", "Champ vide") : Exit Function
        If Telephones.Items.Count = 0 Then AddTel.Focus() : MessageBox.Show("Veuillez entrer un Numéro de téléphone dans Téléphones", "Champ vide") : Exit Function
        If Not mdp.Text = mdp2.Text Then mdp2.Focus() : MessageBox.Show("Les mots de passe ne correspondent pas", "Mot de passe invalide") : Exit Function
        Dim emailValidation As EmailValidator.ValidationLevels = EmailValidator.isEmailValid(MailsManager.mainFromEmailAddress, courriel.Text)
        If courriel.Text <> "" And emailValidation <> EmailValidator.ValidationLevels.Valid Then
            Dim message As String = String.Empty
            Dim domain As String = courriel.Text.Substring(courriel.Text.IndexOf("@") + 1)
            Select Case emailValidation
                Case EmailValidator.ValidationLevels.WrongStructure
                    message = "Veuillez vous assurez que l'adresse de courriel soit valide :" & vbCrLf & "alias@domaine.extension" & vbCrLf & "Exemple : info@cints.net"

                Case EmailValidator.ValidationLevels.DomainNotExists
                    message = "Le nom de domaine """ & domain & """ n'existe pas ou n'a pas de serveur de courriel"

                Case EmailValidator.ValidationLevels.NotConfirmedByDomain
                    message = "L'adresse a été rejeté par le nom de domaine """ & domain & """"
            End Select

            MessageBox.Show(message, "Courriel invalide")
            courriel.Focus()
            Exit Function
        End If

        'Droit&Acces + IsTherapist
        'TODO : Test search array
        DALine = itemDatas(searchArray(itemDatas, typeuser.GetItemText(typeuser.SelectedItem) & "§", SearchType.StartsWith))
        DALine = Microsoft.VisualBasic.Right(DALine, DALine.Length - typeuser.GetItemText(typeuser.SelectedItem).Length - 1)
        If DALine.Substring(0, 1) = "3" Then
            DALine.Substring(0, DALine.Length - 1)
            isTherapist = True
        End If

        'Vérification si l'utilisateur a des RV liés à lui et si son type est un thérapeute
        If oldType <> "" And oldType <> typeuser.Text And oldIsTherapist = True And isTherapist = False Then
            Dim rVs() As String = DBLinker.getInstance.readOneDBField("InfoVisites", "NoVisite", "WHERE NoTRP=" & userID)
            If Not rVs Is Nothing AndAlso rVs.Length <> 0 Then MessageBox.Show("L'utilisateur est lié à des rendez-vous. Le type d'utilisateur choisi doit prendre des clients." & vbCrLf & "Veuillez en sélectionner un autre.", "Type d'utilisateur") : Exit Function
        End If

        'Construit la chaîne des services
        For i = 0 To Services.Items.Count - 1
            If Services.GetItemChecked(i) = True Then MyServices &= "§" & Services.GetItemText(Services.Items.Item(i))
        Next i
        If MyServices Is Nothing Then MyServices = ""
        If Not MyServices = "" Then MyServices = MyServices.Substring(1)

        'Vérification si l'utilisateur est un TRP et qu'il offre au moins un service
        If isTherapist = True And MyServices = "" Then
            MessageBox.Show("Veuillez sélectionner au moins un service, car cet utilisateur prend des clients", "Service manquant")
            Exit Function
        End If

        'Adaptation pour le stockage de certaines données
        If Me.Text.StartsWith("Modif") And (mdp.Text = "Déjà entré.") Then
            ModifMDP = OldMDP
            Cle = oldCle
        Else
            Dim myMdp As String = mdp.Text
            If PreferencesManager.getGeneralPreferences()("UserMDPRespectCasse") = False Then myMdp = myMdp.ToUpper
            ModifMDP = mdpProcessToModif(Cle, myMdp)
        End If

        ReDim SPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(SPhones, 0)
        Phones = String.Join("§", SPhones)

        NoType = noTypes(searchArray(noTypes, typeuser.GetItemText(typeuser.SelectedItem) & "§", SearchType.StartsWith))
        NoType = Microsoft.VisualBasic.Right(NoType, NoType.Length - typeuser.GetItemText(typeuser.SelectedItem).Length - 1)

        If startjobdate.Text.StartsWith("A") = False Then
            sDate = startjobdate.Text.Split(New Char() {"/"})
            StartDate = New Date(sDate(2), sDate(1), sDate(0))
        Else
            StartDate = LIMIT_DATE
        End If
        If endjobdate.Text.StartsWith("A") = False Then
            sDate = endjobdate.Text.Split(New Char() {"/"})
            endDate = New Date(sDate(2), sDate(1), sDate(0))
        Else
            endDate = LIMIT_DATE
        End If

        'Modification de l'objet User
        curUser.isTherapist = isTherapist
        curUser.rights = DALine
        curUser.noEmployeeType = typetravailleur.SelectedIndex + 1
        curUser.passwordKey = Cle
        curUser.password = ModifMDP
        curUser.telephones = Phones
        curUser.noType = NoType
        curUser.startingDate = StartDate
        curUser.endingDate = endDate
        curUser.lastName = nom.Text
        curUser.firstName = prenom.Text
        curUser.address = adresse.Text
        curUser.postalCode = codepostal1.Text & codepostal2.Text
        curUser.email = courriel.Text
        curUser.noPermit = nopermis.Text
        curUser.notConfirmRVOnPasteOfDTRP = NotConfirmRVOnPasteOfDTRP.Checked
        curUser.services = MyServices
        curUser.title = titre.Text
        curUser.url = url.Text
        curUser.city = Ville.Text

        'Enregistre dans la base de données
        Try
            curUser.saveData()
        Catch ex As UserAlreadyUsingException
            MessageBox.Show(ex.Message, "Impossible d'enregistrer l'utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return String.Empty
        End Try

        formModified = False

        Return "DONE"
    End Function

    Private Sub addmodifusers_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Click
        If ctrl_onoff = True And ConnectionsManager.currentUser = 0 Then
            mdpTested.Location = mdp2.Location
            mdp.Text = "0"
            maxi = 2 : generateMDP(1, Me)
        End If
    End Sub

    Private Sub addmodifusers_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = 17 Then ctrl_onoff = True
    End Sub

    Private Sub addmodifusers_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = 17 Then ctrl_onoff = False
    End Sub

    Private Sub mdpTested_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles mdpTested.MouseDown
        Dim button As Short = eventArgs.Button \ &H100000
        Dim shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Double = (eventArgs.X)
        Dim y As Double = (eventArgs.Y)
        If button = 2 Then mdpTested.Visible = False : nochar.Text = ""
    End Sub

    Private Sub addmodifusers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If currentUserName = "Administrateur" Then AdminLabel.Visible = True

        If isLoaded = False Then loading()
    End Sub

    Private Sub addmodifusers_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If ok.Enabled = True Then
            If Me.Text.StartsWith("M") Then If lockedUser = True Then lockSecteur("ModifUser-" & _UserID & ".lock", False)
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If saving() = "" Then e.Cancel = True : Exit Sub
            updatingALLTRPMenu()
        End If
    End Sub

    Private Sub addmodifusers_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If ok.Enabled = True Then
            If Me.Text.StartsWith("M") Then If lockedUser = True Then lockSecteur("ModifUser-" & _UserID & ".lock", False)
            If formModified = True Then If saving() <> "" Then updatingALLTRPMenu()
        End If
    End Sub

    Private Sub adminLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminLabel.Click
        nom.Text = "testing"
        adresse.Text = "777 test rd"
        Ville.Text = "TestCity"
        codepostal1.Text = "a1a" : codepostal2.Text = "1a1"
        Telephones.Items.Add("Maison:555-555-5555")
        titre.Text = "Physio"
        nopermis.Text = "000"
        mdp.Text = "testing"
        mdp2.Text = "testing"
        courriel.Text = "test@test.com"
        url.Text = "http://www.test.com"
    End Sub

    Private Sub services_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles Services.ItemCheck
        If serviceCheckedBySoft = False Then
            serviceCheckedBySoft = True
            Services.SetItemCheckState(e.Index, e.NewValue)
            formModified = True
            sortServices()
            e.NewValue = Services.GetItemCheckState(e.Index)
            services_SelectedIndexChanged(sender, EventArgs.Empty)
        End If
    End Sub

    Private Sub services_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Services.SelectedIndexChanged
        If Services.SelectedIndex = -1 OrElse Services.GetItemChecked(Services.SelectedIndex) = False Then
            upService.Enabled = False
            downService.Enabled = False
            Exit Sub
        End If

        upService.Enabled = Services.SelectedIndex <> 0
        If Services.SelectedIndex <> (Services.Items.Count - 1) Then
            downService.Enabled = Services.GetItemChecked(Services.SelectedIndex + 1) = True
        Else
            downService.Enabled = False
        End If
    End Sub

    Private Sub typetravailleur_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles typetravailleur.SelectedIndexChanged
        formModified = True
    End Sub

    Private Sub mdp_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mdp.GotFocus, mdp2.GotFocus
        With CType(sender, TextBox)
            If .PasswordChar = Nothing Then
                .PasswordChar = "*"
                .Text = ""
            End If
        End With
    End Sub

    Private Sub mdp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mdp.LostFocus, mdp2.LostFocus
        With CType(sender, TextBox)
            If Me.Text.StartsWith("Modif") And .Text = "" And OldMDP <> "00" And OldMDP <> "" Then
                .PasswordChar = Nothing
                .Text = "Déjà entré."
            End If
        End With
    End Sub

    Private Sub textBoxes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nom.TextChanged, prenom.TextChanged, adresse.TextChanged, nopermis.TextChanged, mdp.TextChanged, mdp2.TextChanged, courriel.TextChanged, url.TextChanged, Ville.TextChanged, titre.TextChanged
        formModified = True
    End Sub

    Private Sub enleverToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnleverToolStripMenuItem.Click
        endjobdate.Text = "Aucune date sélectionnée"
    End Sub

    Private Sub upService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles upService.Click
        Me.serviceCheckedBySoft = True
        Dim newIndex As Integer = Me.Services.SelectedIndex - 1
        Me.Services.Items.Insert(newIndex, Me.Services.Items(Me.Services.SelectedIndex))
        Me.Services.SetItemCheckState(newIndex, CheckState.Checked)
        Me.Services.Items.RemoveAt(Me.Services.SelectedIndex)
        Me.Services.SelectedIndex = newIndex
        Me.serviceCheckedBySoft = False
    End Sub

    Private Sub downService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downService.Click
        Me.serviceCheckedBySoft = True
        Dim newIndex As Integer = Me.Services.SelectedIndex + 2
        Me.Services.Items.Insert(newIndex, Me.Services.Items(Me.Services.SelectedIndex))
        Me.Services.SetItemCheckState(newIndex, CheckState.Checked)
        Me.Services.Items.RemoveAt(Me.Services.SelectedIndex)
        Me.Services.SelectedIndex = newIndex - 1
        Me.serviceCheckedBySoft = False
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf addmodifusers_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
