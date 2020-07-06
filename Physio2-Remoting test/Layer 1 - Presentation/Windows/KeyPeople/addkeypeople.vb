Option Strict Off
Option Explicit On
Friend Class addkeypeople
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.
        Me.MdiParent = myMainWin
        Me.publipostage.SelectedIndex = 0

        'Chargement des images
        With DrawingManager.getInstance
            Me.submit.Image = .getImage("ajouter16.gif")
            Me.DownTel.Image = .getImage("DownArrow.jpg")
            Me.UpTel.Image = .getImage("UpArrow.jpg")
            Me.AddTel.Image = .getImage("ajouter16.gif")
            Me.ModifTel.Image = .getImage("modifier16.gif")
            Me.DelTel.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.getReference.Image = .getImage("selection16.gif")
            Me.selectWorkPlace.Image = .getImage("selection16.gif")
            Me.Icon = .getIcon("NewKP16.ico")
        End With
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
    Public WithEvents noident As ManagedText
    Public WithEvents adminbutton As System.Windows.Forms.Button
    Public WithEvents submit As System.Windows.Forms.Button
    Public WithEvents ainfo As ManagedText
    Public WithEvents adresse As ManagedText
    Public WithEvents codepostal1 As ManagedText
    Public WithEvents codepostal2 As ManagedText
    Public WithEvents categorie As ManagedCombo
    Public WithEvents nom As ManagedText
    Public WithEvents _Labels_7 As System.Windows.Forms.Label
    Public WithEvents _Labels_6 As System.Windows.Forms.Label
    Public WithEvents _Labels_5 As System.Windows.Forms.Label
    Public WithEvents _Labels_4 As System.Windows.Forms.Label
    Public WithEvents _Labels_3 As System.Windows.Forms.Label
    Public WithEvents _Label2_2 As System.Windows.Forms.Label
    Public WithEvents _Labels_2 As System.Windows.Forms.Label
    Public WithEvents _Labels_1 As System.Windows.Forms.Label
    Public WithEvents _Labels_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents courriel As ManagedText
    Public WithEvents url As ManagedText
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents getReference As System.Windows.Forms.Button
    Public WithEvents reference As System.Windows.Forms.Label
    Public WithEvents infoNom As System.Windows.Forms.Label
    Public WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents ville As Clinica.ManagedCombo
    Friend WithEvents DownTel As System.Windows.Forms.Button
    Friend WithEvents UpTel As System.Windows.Forms.Button
    Friend WithEvents AddTel As System.Windows.Forms.Button
    Friend WithEvents ModifTel As System.Windows.Forms.Button
    Friend WithEvents DelTel As System.Windows.Forms.Button
    Friend WithEvents Telephones As Clinica.ManagedCombo
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents employeur As Clinica.ManagedCombo
    Public WithEvents label8 As System.Windows.Forms.Label
    Public WithEvents selectWorkPlace As System.Windows.Forms.Button
    Friend WithEvents publipostage As Clinica.ManagedCombo
    Public WithEvents label21 As System.Windows.Forms.Label
    Public WithEvents workPlace As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.noident = New ManagedText
        Me.adminbutton = New System.Windows.Forms.Button
        Me.submit = New System.Windows.Forms.Button
        Me.ainfo = New ManagedText
        Me.adresse = New ManagedText
        Me.codepostal1 = New ManagedText
        Me.codepostal2 = New ManagedText
        Me.categorie = New Clinica.ManagedCombo
        Me.nom = New ManagedText
        Me._Labels_7 = New System.Windows.Forms.Label
        Me._Labels_6 = New System.Windows.Forms.Label
        Me._Labels_5 = New System.Windows.Forms.Label
        Me._Labels_4 = New System.Windows.Forms.Label
        Me._Labels_3 = New System.Windows.Forms.Label
        Me._Label2_2 = New System.Windows.Forms.Label
        Me._Labels_2 = New System.Windows.Forms.Label
        Me._Labels_1 = New System.Windows.Forms.Label
        Me._Labels_0 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.courriel = New ManagedText
        Me.url = New ManagedText
        Me.label2 = New System.Windows.Forms.Label
        Me.getReference = New System.Windows.Forms.Button
        Me.reference = New System.Windows.Forms.Label
        Me.infoNom = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DownTel = New System.Windows.Forms.Button
        Me.UpTel = New System.Windows.Forms.Button
        Me.AddTel = New System.Windows.Forms.Button
        Me.ModifTel = New System.Windows.Forms.Button
        Me.DelTel = New System.Windows.Forms.Button
        Me.selectWorkPlace = New System.Windows.Forms.Button
        Me.ville = New Clinica.ManagedCombo
        Me.Telephones = New Clinica.ManagedCombo
        Me.label4 = New System.Windows.Forms.Label
        Me.employeur = New Clinica.ManagedCombo
        Me.workPlace = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.publipostage = New Clinica.ManagedCombo
        Me.label21 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'noident
        '
        Me.noident.acceptAlpha = True
        Me.noident.acceptedChars = ""
        Me.noident.acceptNumeric = True
        Me.noident.AcceptsReturn = True
        Me.noident.allCapital = True
        Me.noident.allLower = False
        Me.noident.BackColor = System.Drawing.SystemColors.Window
        Me.noident.blockOnMaximum = False
        Me.noident.blockOnMinimum = False
        Me.noident.cb_AcceptLeftZeros = False
        Me.noident.cb_AcceptNegative = False
        Me.noident.currencyBox = False
        Me.noident.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.noident.firstLetterCapital = False
        Me.noident.firstLettersCapital = False
        Me.noident.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.noident.ForeColor = System.Drawing.SystemColors.WindowText
        Me.noident.Location = New System.Drawing.Point(120, 184)
        Me.noident.manageText = True
        Me.noident.matchExp = ""
        Me.noident.maximum = 0
        Me.noident.MaxLength = 0
        Me.noident.minimum = 0
        Me.noident.Name = "noident"
        Me.noident.nbDecimals = CType(-1, Short)
        Me.noident.onlyAlphabet = False
        Me.noident.refuseAccents = False
        Me.noident.refusedChars = ""
        Me.noident.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.noident.showInternalContextMenu = True
        Me.noident.Size = New System.Drawing.Size(208, 20)
        Me.noident.TabIndex = 7
        Me.noident.trimText = False
        '
        'adminbutton
        '
        Me.adminbutton.BackColor = System.Drawing.SystemColors.Control
        Me.adminbutton.Cursor = System.Windows.Forms.Cursors.Default
        Me.adminbutton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adminbutton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adminbutton.Location = New System.Drawing.Point(0, 392)
        Me.adminbutton.Name = "adminbutton"
        Me.adminbutton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adminbutton.Size = New System.Drawing.Size(33, 25)
        Me.adminbutton.TabIndex = 22
        Me.adminbutton.Text = "Set"
        Me.adminbutton.UseVisualStyleBackColor = False
        Me.adminbutton.Visible = False
        '
        'submit
        '
        Me.submit.BackColor = System.Drawing.SystemColors.Control
        Me.submit.Cursor = System.Windows.Forms.Cursors.Default
        Me.submit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.submit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.submit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.submit.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.submit.Location = New System.Drawing.Point(304, 424)
        Me.submit.Name = "submit"
        Me.submit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.submit.Size = New System.Drawing.Size(24, 24)
        Me.submit.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.submit, "Ajouter la personne ou l'organisme clé")
        Me.submit.UseVisualStyleBackColor = False
        '
        'ainfo
        '
        Me.ainfo.acceptAlpha = True
        Me.ainfo.acceptedChars = ""
        Me.ainfo.acceptNumeric = True
        Me.ainfo.AcceptsReturn = True
        Me.ainfo.allCapital = False
        Me.ainfo.allLower = False
        Me.ainfo.BackColor = System.Drawing.SystemColors.Window
        Me.ainfo.blockOnMaximum = False
        Me.ainfo.blockOnMinimum = False
        Me.ainfo.cb_AcceptLeftZeros = False
        Me.ainfo.cb_AcceptNegative = False
        Me.ainfo.currencyBox = False
        Me.ainfo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ainfo.firstLetterCapital = True
        Me.ainfo.firstLettersCapital = False
        Me.ainfo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ainfo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ainfo.Location = New System.Drawing.Point(11, 352)
        Me.ainfo.manageText = True
        Me.ainfo.matchExp = ""
        Me.ainfo.maximum = 0
        Me.ainfo.MaxLength = 0
        Me.ainfo.minimum = 0
        Me.ainfo.Multiline = True
        Me.ainfo.Name = "ainfo"
        Me.ainfo.nbDecimals = CType(-1, Short)
        Me.ainfo.onlyAlphabet = False
        Me.ainfo.refuseAccents = False
        Me.ainfo.refusedChars = ""
        Me.ainfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ainfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ainfo.showInternalContextMenu = True
        Me.ainfo.Size = New System.Drawing.Size(318, 65)
        Me.ainfo.TabIndex = 12
        Me.ainfo.trimText = False
        Me.ainfo.WordWrap = False
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
        Me.adresse.Location = New System.Drawing.Point(120, 56)
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
        Me.adresse.Size = New System.Drawing.Size(208, 20)
        Me.adresse.TabIndex = 2
        Me.adresse.trimText = False
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
        Me.codepostal1.Location = New System.Drawing.Point(120, 104)
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
        Me.codepostal1.TabIndex = 4
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
        Me.codepostal2.Location = New System.Drawing.Point(160, 104)
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
        Me.codepostal2.TabIndex = 5
        Me.codepostal2.trimText = False
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
        Me.categorie.dbField = "KPCategorie.Categorie"
        Me.categorie.doComboDelete = True
        Me.categorie.firstLetterCapital = True
        Me.categorie.firstLettersCapital = False
        Me.categorie.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.categorie.ForeColor = System.Drawing.SystemColors.WindowText
        Me.categorie.itemsToolTipDuration = 10000
        Me.categorie.Location = New System.Drawing.Point(120, 32)
        Me.categorie.manageText = True
        Me.categorie.matchExp = ""
        Me.categorie.maximum = 0
        Me.categorie.minimum = 0
        Me.categorie.Name = "categorie"
        Me.categorie.nbDecimals = CType(-1, Short)
        Me.categorie.onlyAlphabet = False
        Me.categorie.pathOfList = Nothing
        Me.categorie.ReadOnly = False
        Me.categorie.refuseAccents = False
        Me.categorie.refusedChars = ""
        Me.categorie.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.categorie.showItemsToolTip = False
        Me.categorie.Size = New System.Drawing.Size(208, 22)
        Me.categorie.Sorted = True
        Me.categorie.TabIndex = 1
        Me.categorie.trimText = False
        '
        'nom
        '
        Me.nom.acceptAlpha = True
        Me.nom.acceptedChars = " §'§-§#§,§."
        Me.nom.acceptNumeric = True
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
        Me.nom.Location = New System.Drawing.Point(120, 8)
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
        Me.nom.Size = New System.Drawing.Size(208, 20)
        Me.nom.TabIndex = 0
        Me.nom.trimText = False
        '
        '_Labels_7
        '
        Me._Labels_7.AutoSize = True
        Me._Labels_7.BackColor = System.Drawing.Color.Transparent
        Me._Labels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_7.Location = New System.Drawing.Point(8, 184)
        Me._Labels_7.Name = "_Labels_7"
        Me._Labels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_7.Size = New System.Drawing.Size(115, 14)
        Me._Labels_7.TabIndex = 23
        Me._Labels_7.Text = "Numéro identifiant :"
        '
        '_Labels_6
        '
        Me._Labels_6.AutoSize = True
        Me._Labels_6.BackColor = System.Drawing.Color.Transparent
        Me._Labels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_6.Location = New System.Drawing.Point(8, 335)
        Me._Labels_6.Name = "_Labels_6"
        Me._Labels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_6.Size = New System.Drawing.Size(125, 14)
        Me._Labels_6.TabIndex = 21
        Me._Labels_6.Text = "Autres informations :"
        '
        '_Labels_5
        '
        Me._Labels_5.AutoSize = True
        Me._Labels_5.BackColor = System.Drawing.Color.Transparent
        Me._Labels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_5.Location = New System.Drawing.Point(8, 128)
        Me._Labels_5.Name = "_Labels_5"
        Me._Labels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_5.Size = New System.Drawing.Size(79, 14)
        Me._Labels_5.TabIndex = 20
        Me._Labels_5.Text = "Téléphones :"
        '
        '_Labels_4
        '
        Me._Labels_4.AutoSize = True
        Me._Labels_4.BackColor = System.Drawing.Color.Transparent
        Me._Labels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_4.Location = New System.Drawing.Point(8, 104)
        Me._Labels_4.Name = "_Labels_4"
        Me._Labels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_4.Size = New System.Drawing.Size(79, 14)
        Me._Labels_4.TabIndex = 19
        Me._Labels_4.Text = "Code postal :"
        '
        '_Labels_3
        '
        Me._Labels_3.AutoSize = True
        Me._Labels_3.BackColor = System.Drawing.Color.Transparent
        Me._Labels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_3.Location = New System.Drawing.Point(8, 80)
        Me._Labels_3.Name = "_Labels_3"
        Me._Labels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_3.Size = New System.Drawing.Size(37, 14)
        Me._Labels_3.TabIndex = 18
        Me._Labels_3.Text = "Ville :"
        '
        '_Label2_2
        '
        Me._Label2_2.AutoSize = True
        Me._Label2_2.BackColor = System.Drawing.Color.Transparent
        Me._Label2_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_2.Location = New System.Drawing.Point(153, 106)
        Me._Label2_2.Name = "_Label2_2"
        Me._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_2.Size = New System.Drawing.Size(11, 14)
        Me._Label2_2.TabIndex = 17
        Me._Label2_2.Text = "-"
        '
        '_Labels_2
        '
        Me._Labels_2.AutoSize = True
        Me._Labels_2.BackColor = System.Drawing.Color.Transparent
        Me._Labels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_2.Location = New System.Drawing.Point(8, 56)
        Me._Labels_2.Name = "_Labels_2"
        Me._Labels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_2.Size = New System.Drawing.Size(61, 14)
        Me._Labels_2.TabIndex = 14
        Me._Labels_2.Text = "Adresse :"
        '
        '_Labels_1
        '
        Me._Labels_1.AutoSize = True
        Me._Labels_1.BackColor = System.Drawing.Color.Transparent
        Me._Labels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_1.Location = New System.Drawing.Point(8, 32)
        Me._Labels_1.Name = "_Labels_1"
        Me._Labels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_1.Size = New System.Drawing.Size(67, 14)
        Me._Labels_1.TabIndex = 13
        Me._Labels_1.Text = "Catégorie :"
        '
        '_Labels_0
        '
        Me._Labels_0.AutoSize = True
        Me._Labels_0.BackColor = System.Drawing.Color.Transparent
        Me._Labels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_0.Location = New System.Drawing.Point(8, 8)
        Me._Labels_0.Name = "_Labels_0"
        Me._Labels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_0.Size = New System.Drawing.Size(38, 14)
        Me._Labels_0.TabIndex = 12
        Me._Labels_0.Text = "Nom :"
        '
        'Label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(8, 208)
        Me.label1.Name = "Label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(58, 14)
        Me.label1.TabIndex = 25
        Me.label1.Text = "Courriel :"
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
        Me.courriel.Location = New System.Drawing.Point(120, 208)
        Me.courriel.manageText = True
        Me.courriel.matchExp = ""
        Me.courriel.maximum = 0
        Me.courriel.MaxLength = 0
        Me.courriel.minimum = 0
        Me.courriel.Name = "courriel"
        Me.courriel.nbDecimals = CType(-1, Short)
        Me.courriel.onlyAlphabet = True
        Me.courriel.refuseAccents = True
        Me.courriel.refusedChars = ""
        Me.courriel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.courriel.showInternalContextMenu = True
        Me.courriel.Size = New System.Drawing.Size(208, 20)
        Me.courriel.TabIndex = 8
        Me.courriel.trimText = False
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
        Me.url.Location = New System.Drawing.Point(120, 232)
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
        Me.url.Size = New System.Drawing.Size(208, 20)
        Me.url.TabIndex = 9
        Me.url.trimText = False
        '
        'Label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(8, 232)
        Me.label2.Name = "Label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(81, 14)
        Me.label2.TabIndex = 27
        Me.label2.Text = "Site internet :"
        '
        'getReference
        '
        Me.getReference.BackColor = System.Drawing.SystemColors.Control
        Me.getReference.Cursor = System.Windows.Forms.Cursors.Default
        Me.getReference.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.getReference.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.getReference.ForeColor = System.Drawing.SystemColors.ControlText
        Me.getReference.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.getReference.Location = New System.Drawing.Point(104, 424)
        Me.getReference.Name = "getReference"
        Me.getReference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.getReference.Size = New System.Drawing.Size(24, 24)
        Me.getReference.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.getReference, "Sélectionner un compte client")
        Me.getReference.UseVisualStyleBackColor = False
        '
        'Reference
        '
        Me.reference.AutoSize = True
        Me.reference.BackColor = System.Drawing.Color.Transparent
        Me.reference.Cursor = System.Windows.Forms.Cursors.Default
        Me.reference.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reference.ForeColor = System.Drawing.SystemColors.ControlText
        Me.reference.Location = New System.Drawing.Point(134, 429)
        Me.reference.Name = "Reference"
        Me.reference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.reference.Size = New System.Drawing.Size(105, 14)
        Me.reference.TabIndex = 29
        Me.reference.Tag = "0"
        Me.reference.Text = "Aucun compte client"
        '
        'InfoNom
        '
        Me.infoNom.BackColor = System.Drawing.SystemColors.Info
        Me.infoNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.infoNom.Cursor = System.Windows.Forms.Cursors.Default
        Me.infoNom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.infoNom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoNom.Location = New System.Drawing.Point(120, 28)
        Me.infoNom.Name = "InfoNom"
        Me.infoNom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.infoNom.Size = New System.Drawing.Size(208, 40)
        Me.infoNom.TabIndex = 30
        Me.infoNom.Text = "S'il s'agit du nom d'une personne veuillez l'inscrire de la façon suivante : Nom," & _
            "Prénom"
        Me.infoNom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.infoNom, "Cliquez dessus pour faire disparaître")
        Me.infoNom.Visible = False
        '
        'Label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(8, 429)
        Me.label3.Name = "Label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(90, 14)
        Me.label3.TabIndex = 31
        Me.label3.Text = "Compte client :"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'DownTel
        '
        Me.DownTel.BackColor = System.Drawing.SystemColors.Control
        Me.DownTel.Enabled = False
        Me.DownTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DownTel.Location = New System.Drawing.Point(272, 152)
        Me.DownTel.Name = "DownTel"
        Me.DownTel.Size = New System.Drawing.Size(24, 24)
        Me.DownTel.TabIndex = 80
        Me.DownTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DownTel, "Descendre un numéro de téléphone")
        Me.DownTel.UseVisualStyleBackColor = False
        '
        'UpTel
        '
        Me.UpTel.BackColor = System.Drawing.SystemColors.Control
        Me.UpTel.Enabled = False
        Me.UpTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.UpTel.Location = New System.Drawing.Point(240, 152)
        Me.UpTel.Name = "UpTel"
        Me.UpTel.Size = New System.Drawing.Size(24, 24)
        Me.UpTel.TabIndex = 79
        Me.UpTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.UpTel, "Monter un numéro de téléphone")
        Me.UpTel.UseVisualStyleBackColor = False
        '
        'AddTel
        '
        Me.AddTel.BackColor = System.Drawing.SystemColors.Control
        Me.AddTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddTel.Location = New System.Drawing.Point(144, 152)
        Me.AddTel.Name = "AddTel"
        Me.AddTel.Size = New System.Drawing.Size(24, 24)
        Me.AddTel.TabIndex = 76
        Me.AddTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.AddTel, "Ajout d'un numéro de téléphone")
        Me.AddTel.UseVisualStyleBackColor = False
        '
        'ModifTel
        '
        Me.ModifTel.BackColor = System.Drawing.SystemColors.Control
        Me.ModifTel.Enabled = False
        Me.ModifTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ModifTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ModifTel.Location = New System.Drawing.Point(176, 152)
        Me.ModifTel.Name = "ModifTel"
        Me.ModifTel.Size = New System.Drawing.Size(24, 24)
        Me.ModifTel.TabIndex = 77
        Me.ModifTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ModifTel, "Modifier un numéro de téléphone")
        Me.ModifTel.UseVisualStyleBackColor = False
        '
        'DelTel
        '
        Me.DelTel.BackColor = System.Drawing.SystemColors.Control
        Me.DelTel.Enabled = False
        Me.DelTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelTel.Location = New System.Drawing.Point(208, 152)
        Me.DelTel.Name = "DelTel"
        Me.DelTel.Size = New System.Drawing.Size(24, 24)
        Me.DelTel.TabIndex = 78
        Me.DelTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DelTel, "Enlever un numéro de téléphone")
        Me.DelTel.UseVisualStyleBackColor = False
        '
        'selectWorkPlace
        '
        Me.selectWorkPlace.BackColor = System.Drawing.SystemColors.Control
        Me.selectWorkPlace.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectWorkPlace.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectWorkPlace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectWorkPlace.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectWorkPlace.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectWorkPlace.Location = New System.Drawing.Point(120, 280)
        Me.selectWorkPlace.Name = "selectWorkPlace"
        Me.selectWorkPlace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectWorkPlace.Size = New System.Drawing.Size(24, 24)
        Me.selectWorkPlace.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.selectWorkPlace, "Sélectionner le lieu de travail d'une personne clée")
        Me.selectWorkPlace.UseVisualStyleBackColor = False
        '
        'Ville
        '
        Me.ville.acceptAlpha = True
        Me.ville.acceptedChars = " §'§-§.§/§|§\§(§)"
        Me.ville.acceptNumeric = False
        Me.ville.allCapital = False
        Me.ville.allLower = False
        Me.ville.autoComplete = True
        Me.ville.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ville.autoSizeDropDown = True
        Me.ville.BackColor = System.Drawing.Color.White
        Me.ville.blockOnMaximum = False
        Me.ville.blockOnMinimum = False
        Me.ville.cb_AcceptLeftZeros = False
        Me.ville.cb_AcceptNegative = False
        Me.ville.currencyBox = False
        Me.ville.Cursor = System.Windows.Forms.Cursors.Default
        Me.ville.dbField = "Villes.NomVille"
        Me.ville.doComboDelete = True
        Me.ville.firstLetterCapital = True
        Me.ville.firstLettersCapital = False
        Me.ville.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ville.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ville.itemsToolTipDuration = 10000
        Me.ville.Location = New System.Drawing.Point(120, 80)
        Me.ville.manageText = True
        Me.ville.matchExp = ""
        Me.ville.maximum = 0
        Me.ville.minimum = 0
        Me.ville.Name = "Ville"
        Me.ville.nbDecimals = CType(-1, Short)
        Me.ville.onlyAlphabet = True
        Me.ville.pathOfList = Nothing
        Me.ville.ReadOnly = False
        Me.ville.refuseAccents = False
        Me.ville.refusedChars = ""
        Me.ville.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ville.showItemsToolTip = False
        Me.ville.Size = New System.Drawing.Size(208, 22)
        Me.ville.Sorted = True
        Me.ville.TabIndex = 3
        Me.ville.trimText = False
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
        Me.Telephones.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Telephones.itemsToolTipDuration = 10000
        Me.Telephones.Location = New System.Drawing.Point(120, 128)
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
        Me.Telephones.Size = New System.Drawing.Size(208, 22)
        Me.Telephones.TabIndex = 6
        Me.Telephones.trimText = False
        '
        'Label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(8, 256)
        Me.label4.Name = "Label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(72, 14)
        Me.label4.TabIndex = 81
        Me.label4.Text = "Employeur :"
        '
        'Employeur
        '
        Me.employeur.acceptAlpha = True
        Me.employeur.acceptedChars = Nothing
        Me.employeur.acceptNumeric = True
        Me.employeur.allCapital = False
        Me.employeur.allLower = False
        Me.employeur.autoComplete = True
        Me.employeur.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.employeur.autoSizeDropDown = True
        Me.employeur.BackColor = System.Drawing.Color.White
        Me.employeur.blockOnMaximum = False
        Me.employeur.blockOnMinimum = False
        Me.employeur.cb_AcceptLeftZeros = False
        Me.employeur.cb_AcceptNegative = False
        Me.employeur.currencyBox = False
        Me.employeur.Cursor = System.Windows.Forms.Cursors.Default
        Me.employeur.dbField = "Employeurs.Employeur"
        Me.employeur.doComboDelete = True
        Me.employeur.firstLetterCapital = True
        Me.employeur.firstLettersCapital = False
        Me.employeur.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.employeur.ForeColor = System.Drawing.SystemColors.WindowText
        Me.employeur.itemsToolTipDuration = 10000
        Me.employeur.Location = New System.Drawing.Point(120, 256)
        Me.employeur.manageText = True
        Me.employeur.matchExp = ""
        Me.employeur.maximum = 0
        Me.employeur.minimum = 0
        Me.employeur.Name = "Employeur"
        Me.employeur.nbDecimals = CType(-1, Short)
        Me.employeur.onlyAlphabet = False
        Me.employeur.pathOfList = ""
        Me.employeur.ReadOnly = False
        Me.employeur.refuseAccents = False
        Me.employeur.refusedChars = ""
        Me.employeur.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.employeur.showItemsToolTip = False
        Me.employeur.Size = New System.Drawing.Size(208, 22)
        Me.employeur.Sorted = True
        Me.employeur.TabIndex = 10
        Me.employeur.trimText = False
        '
        'WorkPlace
        '
        Me.workPlace.AutoSize = True
        Me.workPlace.BackColor = System.Drawing.Color.Transparent
        Me.workPlace.Cursor = System.Windows.Forms.Cursors.Default
        Me.workPlace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.workPlace.ForeColor = System.Drawing.SystemColors.ControlText
        Me.workPlace.Location = New System.Drawing.Point(152, 288)
        Me.workPlace.Name = "WorkPlace"
        Me.workPlace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.workPlace.Size = New System.Drawing.Size(109, 14)
        Me.workPlace.TabIndex = 29
        Me.workPlace.Tag = "0"
        Me.workPlace.Text = "Aucun organisme clé"
        '
        'Label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(8, 280)
        Me.label8.Name = "Label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(90, 14)
        Me.label8.TabIndex = 31
        Me.label8.Text = "Lieu de travail :"
        '
        'publipostage
        '
        Me.publipostage.acceptAlpha = True
        Me.publipostage.acceptedChars = Nothing
        Me.publipostage.acceptNumeric = True
        Me.publipostage.allCapital = False
        Me.publipostage.allLower = False
        Me.publipostage.autoComplete = True
        Me.publipostage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.publipostage.autoSizeDropDown = True
        Me.publipostage.BackColor = System.Drawing.Color.White
        Me.publipostage.blockOnMaximum = False
        Me.publipostage.blockOnMinimum = False
        Me.publipostage.cb_AcceptLeftZeros = False
        Me.publipostage.cb_AcceptNegative = False
        Me.publipostage.currencyBox = False
        Me.publipostage.dbField = Nothing
        Me.publipostage.doComboDelete = True
        Me.publipostage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.publipostage.firstLetterCapital = False
        Me.publipostage.firstLettersCapital = False
        Me.publipostage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.publipostage.Items.AddRange(New Object() {"Ne pas recevoir d'envoi", "Recevoir l'envoi par la poste", "Recevoir l'envoi par courriel"})
        Me.publipostage.itemsToolTipDuration = 10000
        Me.publipostage.Location = New System.Drawing.Point(120, 310)
        Me.publipostage.manageText = True
        Me.publipostage.matchExp = Nothing
        Me.publipostage.maximum = 0
        Me.publipostage.minimum = 0
        Me.publipostage.Name = "publipostage"
        Me.publipostage.nbDecimals = CType(-1, Short)
        Me.publipostage.onlyAlphabet = False
        Me.publipostage.pathOfList = Nothing
        Me.publipostage.ReadOnly = False
        Me.publipostage.refuseAccents = False
        Me.publipostage.refusedChars = Nothing
        Me.publipostage.showItemsToolTip = False
        Me.publipostage.Size = New System.Drawing.Size(208, 22)
        Me.publipostage.TabIndex = 197
        Me.publipostage.trimText = False
        '
        'Label21
        '
        Me.label21.AutoSize = True
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label21.Location = New System.Drawing.Point(8, 313)
        Me.label21.Name = "Label21"
        Me.label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label21.Size = New System.Drawing.Size(85, 14)
        Me.label21.TabIndex = 198
        Me.label21.Text = "Publipostage :"
        '
        'addkeypeople
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 13)
        Me.ClientSize = New System.Drawing.Size(338, 457)
        Me.Controls.Add(Me.publipostage)
        Me.Controls.Add(Me.label21)
        Me.Controls.Add(Me.employeur)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.DownTel)
        Me.Controls.Add(Me.UpTel)
        Me.Controls.Add(Me.AddTel)
        Me.Controls.Add(Me.ModifTel)
        Me.Controls.Add(Me.DelTel)
        Me.Controls.Add(Me.Telephones)
        Me.Controls.Add(Me.infoNom)
        Me.Controls.Add(Me.adminbutton)
        Me.Controls.Add(Me.ville)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.categorie)
        Me.Controls.Add(Me.reference)
        Me.Controls.Add(Me.url)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.courriel)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.noident)
        Me.Controls.Add(Me.ainfo)
        Me.Controls.Add(Me.adresse)
        Me.Controls.Add(Me.codepostal1)
        Me.Controls.Add(Me.codepostal2)
        Me.Controls.Add(Me.nom)
        Me.Controls.Add(Me._Labels_7)
        Me.Controls.Add(Me._Labels_6)
        Me.Controls.Add(Me._Labels_5)
        Me.Controls.Add(Me._Labels_4)
        Me.Controls.Add(Me._Labels_3)
        Me.Controls.Add(Me._Label2_2)
        Me.Controls.Add(Me._Labels_2)
        Me.Controls.Add(Me._Labels_1)
        Me.Controls.Add(Me._Labels_0)
        Me.Controls.Add(Me.getReference)
        Me.Controls.Add(Me.submit)
        Me.Controls.Add(Me.selectWorkPlace)
        Me.Controls.Add(Me.workPlace)
        Me.Controls.Add(Me.label8)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(420, 330)
        Me.MaximizeBox = False
        Me.Name = "addkeypeople"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Ajout d'un(e) personne/organisme clé"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
    Private _From As Form
    Private formModified As Boolean = False

#Region "Propriétés"
    Public Property from() As Form
        Get
            from = _From
        End Get
        Set(ByVal Value As Form)
            _From = Value
        End Set
    End Property

    Public WriteOnly Property setFormModified() As Boolean
        Set(ByVal Value As Boolean)
            formModified = Value
        End Set
    End Property
#End Region

#Region "GeneralInfo Form"
    Private Sub infoNom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles infoNom.Click
        infoNom.Visible = False
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
        If Telephones.SelectedIndex <> -1 And AddTel.Enabled = True Then
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
        Dim myPhone() As String = Split(Telephones.GetItemText(Telephones.SelectedItem), ":")

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

    Private Sub textBoxes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nom.TextChanged, categorie.TextChanged, adresse.TextChanged, courriel.TextChanged, url.TextChanged, ainfo.TextChanged, noident.TextChanged
        formModified = True
    End Sub

    Private Sub ainfo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ainfo.KeyUp
        If e.KeyCode = 8 And ainfo.Text = "" Then Me.GetNextControl(sender, False).Focus()
        e.Handled = True
    End Sub

    Private Sub nom_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nom.KeyUp
        If e.KeyCode = 13 Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
    End Sub

    Private Sub changeFocusObject(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles codepostal1.KeyUp, codepostal2.KeyUp, Telephones.KeyUp, ville.KeyUp, courriel.KeyUp, url.KeyUp, adresse.KeyUp, categorie.KeyUp, noident.KeyUp, employeur.KeyUp, selectWorkPlace.KeyUp, getReference.KeyUp
        If e.KeyCode = 13 Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
        If e.KeyCode = 8 Then
            Try
                If sender.Text = "" Then Me.GetNextControl(CType(sender, Control), False).Focus()
            Catch
                Me.GetNextControl(sender, False).Focus()
            End Try
        End If
    End Sub

    Private Sub codepostal1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles codepostal1.TextChanged
        If codepostal1.Text.Length = 3 Then Me.GetNextControl(sender, True).Focus()
        formModified = True
    End Sub

    Private Sub codepostal2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles codepostal2.TextChanged
        If codepostal2.Text.Length = 3 Then Me.GetNextControl(sender, True).Focus()
        formModified = True
    End Sub

    Private Sub getReference_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles getReference.Click
        Dim myRecherche As New clientSearch()
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.Visible = False
        myRecherche.ShowDialog()
    End Sub

    Private Sub selectWorkPlace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectWorkPlace.Click
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        myKeyPeople.Visible = False
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            workPlace.Tag = kpChosen.noKP
            workPlace.Text = kpChosen.kpFullName
        End If
    End Sub

    Private Sub nom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles nom.Enter
        infoNom.Visible = True
    End Sub

    Private Sub nom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles nom.Leave
        infoNom.Visible = False
    End Sub
#End Region

    Private Sub adminButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adminbutton.Click
        nom.Text = "Boivin,Jonathan"
        categorie.Text = "Personne:Programmeur"
        adresse.Text = "15 Archambault"
        ville.Text = "Charlemagne"
        codepostal1.Text = "J5Z"
        codepostal2.Text = "1Z9"
        Telephones.Items.Add("Maison:555-5555")
        ainfo.Text = "Test de personne clé"
    End Sub

    Private Sub addkeypeople_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim dbList() As String
        If currentUserName = "Administrateur" Then adminbutton.Visible = True

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        'Chargement des catégories
        dbList = DBLinker.getInstance.readOneDBField("KPCategorie", "Categorie", , True)
        If Not dbList Is Nothing AndAlso dbList.Length <> 0 Then categorie.Items.AddRange(dbList)

        'Chargement des employeurs
        dbList = DBLinker.getInstance.readOneDBField("Employeurs", "Employeur", , True)
        If Not dbList Is Nothing AndAlso dbList.Length <> 0 Then employeur.Items.AddRange(dbList)

        'Chargement des villes
        dbList = DBLinker.getInstance.readOneDBField("Villes", "NomVille", , True)
        If Not dbList Is Nothing AndAlso dbList.Length <> 0 Then ville.Items.AddRange(dbList)

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
    End Sub

    Private Sub submit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles submit.Click
        If adding() <> "" Then
            If Not from Is Nothing Then
                Try
                    Select Case from.Name.ToLower
                        Case "viewmodifclients"
                            CType(from, viewmodifclients).btnAddAsKP.Enabled = False
                    End Select
                Catch
                End Try
            End If
            Me.Close()
        End If
    End Sub

    Private Function adding() As String
        Dim i As Integer
        Dim noCat As Object = "null"
        Dim CurCat, TCat(), Phones, sPhones() As String

        If PreferencesManager.getGeneralPreferences()("COPOC1") And nom.Text = "" Then MessageBox.Show("Veuillez entrer un nom", "Entrée manquante") : nom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC2") And categorie.Text = "" Then MessageBox.Show("Veuillez entrer une catégorie", "Entrée manquante") : categorie.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC3") And adresse.Text = "" Then MessageBox.Show("Veuillez entrer une adresse", "Entrée manquante") : adresse.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC4") And ville.Text = "" Then MessageBox.Show("Veuillez entrer une ville", "Entrée manquante") : ville.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC5") And (codepostal1.Text = "" Or codepostal2.Text = "") Then MessageBox.Show("Veuillez entrer un code postal complet", "Entrée manquante") : codepostal1.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC6") And Telephones.Items.Count = 0 Then MessageBox.Show("Veuillez entrer au moins un numéro de téléphone", "Entrée manquante") : AddTel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC7") And noident.Text = "" Then MessageBox.Show("Veuillez entrer un numéro identifiant", "Entrée manquante") : noident.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC10") And ainfo.Text = "" Then MessageBox.Show("Veuillez entrer d'autres informations", "Entrée manquante") : ainfo.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC8") And courriel.Text = "" Then MessageBox.Show("Veuillez entrer une adresse de courriel", "Entrée manquante") : courriel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC9") And url.Text = "" Then MessageBox.Show("Veuillez entrer une adresse de site internet", "Entrée manquante") : url.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC11") And employeur.Text = "" Then MessageBox.Show("Veuillez entrer un employeur", "Entrée manquante") : employeur.Focus() : Exit Function
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
        If publipostage.SelectedIndex = 2 And courriel.Text = "" Then MessageBox.Show("Le champ 'Courriel' est obligatoire lorsque le publipostage est envoyé par courriel", "Information manquante") : courriel.Focus() : Exit Function

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        'Save catégories
        TCat = Split(categorie.Text, ":")

        CurCat = ""
        For i = 0 To TCat.Length - 1
            If CurCat = "" Then
                CurCat = TCat(i)
            Else
                CurCat = CurCat & ":" & TCat(i)
            End If

            Dim myNoCat As Object = DBHelper.addItemToADBList("KPCategorie", "Categorie", CurCat, "NoCategorie")
            If CurCat = categorie.Text Then noCat = myNoCat
        Next i

        ReDim sPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(sPhones, 0)
        Phones = Join(sPhones, "§")
        If Phones Is Nothing Then Phones = ""

        Dim workPlaceNo As String = ""
        If Not workPlace.Tag Is Nothing Then workPlaceNo = workPlace.Tag & "<br>" & workPlace.Text.Replace("'", "''")
        Dim noClient As String = reference.Tag
        If noClient = "" Then noClient = "null"

        If DBLinker.getInstance.writeDB("KeyPeople", " Nom, NoCategorie, Adresse, NoVille, CodePostal, Telephones, NoRef, Courriel, URL, NoClient, AutreInfos, NoEmployeur, NoUser, DateHeureCreation, WorkPlace, Publipostage", "'" & nom.Text.Replace("'", "''") & "'," & noCat & ",'" & adresse.Text.Replace("'", "''") & "'," & DBHelper.addItemToADBList("Villes", "NomVille", ville.Text, "NoVille") & ",'" & codepostal1.Text & codepostal2.Text & "','" & Phones.Replace("'", "''") & "','" & noident.Text.Replace("'", "''") & "','" & courriel.Text & "','" & url.Text.Replace("'", "''") & "'," & noClient & ",'" & ainfo.Text.Replace("'", "''") & "'," & DBHelper.addItemToADBList("Employeurs", "Employeur", employeur.Text, "NoEmployeur") & "," & ConnectionsManager.currentUser & ",'" & DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Now, DateFormat.TextDateOptions.ShortTime) & "','" & workPlaceNo & "'," & publipostage.SelectedIndex) = False Then
            If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            Return ""
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        myMainWin.StatusText = "Ajout d'une personne/organisme clé"

        formModified = False
        Return "DONE"
    End Function

    Private Sub addkeypeople_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If submit.Enabled = True Then
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If adding() = "" Then e.Cancel = True
        End If
    End Sub

    Public Sub loading(Optional ByVal resetFields As Boolean = True)
        Me.Text = "Ajout d'une personne / organisme clé"
        If resetFields Then
            nom.Text = ""
            categorie.Text = ""
            adresse.Text = ""
            ville.Text = ""
            codepostal1.Text = ""
            codepostal2.Text = ""
            Telephones.Items.Clear()
            courriel.Text = ""
            url.Text = ""
            ainfo.Text = ""
            reference.Text = "Aucun compte référent"
            reference.Tag = Nothing
            workPlace.Text = "Aucun organisme clé"
            workPlace.Tag = ""
        End If

        formModified = False
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf Me.submit_Click)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
