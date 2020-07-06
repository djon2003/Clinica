Option Strict Off
Option Explicit On
Friend Class addclient
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.MdiParent = myMainWin
        Me.sexe = New BaseObjArray()
        Me.sexe.add(Me._sexe_0)
        Me.sexe.add(Me._sexe_1)
        Me.reference.Tag = ""

        'Chargement des images
        Me.ajout.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.DownTel.Image = DrawingManager.getInstance.getImage("DownArrow.jpg")
        Me.UpTel.Image = DrawingManager.getInstance.getImage("UpArrow.jpg")
        Me.AddTel.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.ModifTel.Image = DrawingManager.getInstance.getImage("modifier16.gif")
        Me.DelTel.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.selectionner.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.Icon = DrawingManager.getInstance.getIcon("newclient16.ico")
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
    Public WithEvents ajout As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents sexe As BaseObjArray
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents menuRefCompte As System.Windows.Forms.MenuItem
    Friend WithEvents menuRefKP As System.Windows.Forms.MenuItem
    Friend WithEvents menuRefAutre As System.Windows.Forms.MenuItem
    Friend WithEvents RefMenu As System.Windows.Forms.ContextMenu
    Public WithEvents fullField As System.Windows.Forms.Label
    Public WithEvents addManyClients As System.Windows.Forms.Label
    Friend WithEvents AdminBox As System.Windows.Forms.Panel
    Public WithEvents hideAdmin As System.Windows.Forms.Button
    Friend WithEvents menuPreRefList As System.Windows.Forms.MenuItem
    Friend WithEvents publipostage As System.Windows.Forms.ComboBox
    Public WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents DownTel As System.Windows.Forms.Button
    Friend WithEvents UpTel As System.Windows.Forms.Button
    Public WithEvents _Label1_16 As System.Windows.Forms.Label
    Public WithEvents _Label1_4 As System.Windows.Forms.Label
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents label3 As System.Windows.Forms.Label
    Public WithEvents _Label1_17 As System.Windows.Forms.Label
    Public WithEvents _Label1_15 As System.Windows.Forms.Label
    Public WithEvents _Label1_14 As System.Windows.Forms.Label
    Public WithEvents _Label1_11 As System.Windows.Forms.Label
    Public WithEvents _Label1_8 As System.Windows.Forms.Label
    Public WithEvents _Label1_7 As System.Windows.Forms.Label
    Public WithEvents _Label1_6 As System.Windows.Forms.Label
    Public WithEvents _Label1_5 As System.Windows.Forms.Label
    Public WithEvents _Label1_0 As System.Windows.Forms.Label
    Public WithEvents _Label1_1 As System.Windows.Forms.Label
    Public WithEvents _Label1_2 As System.Windows.Forms.Label
    Public WithEvents _Label1_3 As System.Windows.Forms.Label
    Public WithEvents _Label2_2 As System.Windows.Forms.Label
    Friend WithEvents AddTel As System.Windows.Forms.Button
    Friend WithEvents ModifTel As System.Windows.Forms.Button
    Friend WithEvents DelTel As System.Windows.Forms.Button
    Friend WithEvents Telephones As Clinica.ManagedCombo
    Public WithEvents ville As Clinica.ManagedCombo
    Public WithEvents selectionner As System.Windows.Forms.Button
    Friend WithEvents reference As System.Windows.Forms.TextBox
    Public WithEvents url As ManagedText
    Public WithEvents courriel As ManagedText
    Public WithEvents prenom As ManagedText
    Public WithEvents remarques As ManagedText
    Public WithEvents nam As ManagedText
    Public WithEvents autrenom As ManagedText
    Public WithEvents nom As ManagedText
    Public WithEvents adresse As ManagedText
    Public WithEvents codepostal2 As ManagedText
    Public WithEvents codepostal1 As ManagedText
    Public WithEvents metierslist As ManagedCombo
    Public WithEvents employeurslist As ManagedCombo
    Public WithEvents _sexe_1 As System.Windows.Forms.RadioButton
    Public WithEvents _sexe_0 As System.Windows.Forms.RadioButton
    Public WithEvents annee As ManagedCombo
    Public WithEvents mois As ManagedCombo
    Friend WithEvents menuRefAucun As System.Windows.Forms.MenuItem
    Public WithEvents jour As ManagedCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ajout = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DownTel = New System.Windows.Forms.Button
        Me.UpTel = New System.Windows.Forms.Button
        Me.AddTel = New System.Windows.Forms.Button
        Me.ModifTel = New System.Windows.Forms.Button
        Me.DelTel = New System.Windows.Forms.Button
        Me.selectionner = New System.Windows.Forms.Button
        Me.annee = New ManagedCombo
        Me.mois = New ManagedCombo
        Me.jour = New ManagedCombo
        Me.RefMenu = New System.Windows.Forms.ContextMenu
        Me.menuRefAucun = New System.Windows.Forms.MenuItem
        Me.menuRefAutre = New System.Windows.Forms.MenuItem
        Me.menuRefCompte = New System.Windows.Forms.MenuItem
        Me.menuPreRefList = New System.Windows.Forms.MenuItem
        Me.menuRefKP = New System.Windows.Forms.MenuItem
        Me.AdminBox = New System.Windows.Forms.Panel
        Me.hideAdmin = New System.Windows.Forms.Button
        Me.addManyClients = New System.Windows.Forms.Label
        Me.fullField = New System.Windows.Forms.Label
        Me.publipostage = New System.Windows.Forms.ComboBox
        Me.label21 = New System.Windows.Forms.Label
        Me._Label1_16 = New System.Windows.Forms.Label
        Me._Label1_4 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me._Label1_17 = New System.Windows.Forms.Label
        Me._Label1_15 = New System.Windows.Forms.Label
        Me._Label1_14 = New System.Windows.Forms.Label
        Me._Label1_11 = New System.Windows.Forms.Label
        Me._Label1_8 = New System.Windows.Forms.Label
        Me._Label1_7 = New System.Windows.Forms.Label
        Me._Label1_6 = New System.Windows.Forms.Label
        Me._Label1_5 = New System.Windows.Forms.Label
        Me._Label1_0 = New System.Windows.Forms.Label
        Me._Label1_1 = New System.Windows.Forms.Label
        Me._Label1_2 = New System.Windows.Forms.Label
        Me._Label1_3 = New System.Windows.Forms.Label
        Me._Label2_2 = New System.Windows.Forms.Label
        Me.Telephones = New ManagedCombo
        Me.ville = New ManagedCombo
        Me.reference = New System.Windows.Forms.TextBox
        Me.url = New ManagedText
        Me.courriel = New ManagedText
        Me.prenom = New ManagedText
        Me.remarques = New ManagedText
        Me.nam = New ManagedText
        Me.autrenom = New ManagedText
        Me.nom = New ManagedText
        Me.adresse = New ManagedText
        Me.codepostal2 = New ManagedText
        Me.codepostal1 = New ManagedText
        Me.metierslist = New ManagedCombo
        Me.employeurslist = New ManagedCombo
        Me._sexe_1 = New System.Windows.Forms.RadioButton
        Me._sexe_0 = New System.Windows.Forms.RadioButton
        Me.AdminBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ajout
        '
        Me.ajout.BackColor = System.Drawing.SystemColors.Control
        Me.ajout.Cursor = System.Windows.Forms.Cursors.Default
        Me.ajout.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ajout.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ajout.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ajout.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ajout.Location = New System.Drawing.Point(336, 408)
        Me.ajout.Name = "ajout"
        Me.ajout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ajout.Size = New System.Drawing.Size(24, 24)
        Me.ajout.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.ajout, "Ajouter le compte client")
        Me.ajout.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'DownTel
        '
        Me.DownTel.Enabled = False
        Me.DownTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DownTel.Location = New System.Drawing.Point(144, 248)
        Me.DownTel.Name = "DownTel"
        Me.DownTel.Size = New System.Drawing.Size(24, 24)
        Me.DownTel.TabIndex = 234
        Me.DownTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DownTel, "Descendre un numéro de téléphone")
        '
        'UpTel
        '
        Me.UpTel.Enabled = False
        Me.UpTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.UpTel.Location = New System.Drawing.Point(112, 248)
        Me.UpTel.Name = "UpTel"
        Me.UpTel.Size = New System.Drawing.Size(24, 24)
        Me.UpTel.TabIndex = 233
        Me.UpTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.UpTel, "Monter un numéro de téléphone")
        '
        'AddTel
        '
        Me.AddTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddTel.Location = New System.Drawing.Point(16, 248)
        Me.AddTel.Name = "AddTel"
        Me.AddTel.Size = New System.Drawing.Size(24, 24)
        Me.AddTel.TabIndex = 200
        Me.AddTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.AddTel, "Ajout d'un numéro de téléphone")
        '
        'ModifTel
        '
        Me.ModifTel.Enabled = False
        Me.ModifTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ModifTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ModifTel.Location = New System.Drawing.Point(48, 248)
        Me.ModifTel.Name = "ModifTel"
        Me.ModifTel.Size = New System.Drawing.Size(24, 24)
        Me.ModifTel.TabIndex = 201
        Me.ModifTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ModifTel, "Modifier un numéro de téléphone")
        '
        'DelTel
        '
        Me.DelTel.Enabled = False
        Me.DelTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelTel.Location = New System.Drawing.Point(80, 248)
        Me.DelTel.Name = "DelTel"
        Me.DelTel.Size = New System.Drawing.Size(24, 24)
        Me.DelTel.TabIndex = 202
        Me.DelTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DelTel, "Enlever un numéro de téléphone")
        '
        'selectionner
        '
        Me.selectionner.BackColor = System.Drawing.SystemColors.Control
        Me.selectionner.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectionner.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectionner.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectionner.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectionner.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectionner.Location = New System.Drawing.Point(8, 296)
        Me.selectionner.Name = "selectionner"
        Me.selectionner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectionner.Size = New System.Drawing.Size(24, 24)
        Me.selectionner.TabIndex = 8
        Me.selectionner.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectionner, "Sélectionner le référent")
        Me.selectionner.UseVisualStyleBackColor = False
        '
        'annee
        '
        Me.annee.acceptAlpha = False
        Me.annee.acceptedChars = Nothing
        Me.annee.acceptNumeric = True
        Me.annee.allCapital = False
        Me.annee.allLower = False
        Me.annee.autoComplete = False
        Me.annee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.annee.autoSizeDropDown = True
        Me.annee.BackColor = System.Drawing.Color.White
        Me.annee.blockOnMaximum = False
        Me.annee.blockOnMinimum = False
        Me.annee.cb_AcceptLeftZeros = False
        Me.annee.cb_AcceptNegative = False
        Me.annee.currencyBox = False
        Me.annee.Cursor = System.Windows.Forms.Cursors.Default
        Me.annee.dbField = Nothing
        Me.annee.doComboDelete = True
        Me.annee.firstLetterCapital = False
        Me.annee.firstLettersCapital = False
        Me.annee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.annee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.annee.itemsToolTipDuration = 10000
        Me.annee.Location = New System.Drawing.Point(192, 24)
        Me.annee.manageText = True
        Me.annee.matchExp = Nothing
        Me.annee.maximum = 0
        Me.annee.minimum = 0
        Me.annee.Name = "annee"
        Me.annee.nbDecimals = CType(-1, Short)
        Me.annee.onlyAlphabet = False
        Me.annee.pathOfList = Nothing
        Me.annee.ReadOnly = False
        Me.annee.refuseAccents = False
        Me.annee.refusedChars = ""
        Me.annee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.annee.showItemsToolTip = False
        Me.annee.Size = New System.Drawing.Size(56, 22)
        Me.annee.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.annee, "(AAAA/MM/JJ)")
        Me.annee.trimText = False
        '
        'mois
        '
        Me.mois.acceptAlpha = False
        Me.mois.acceptedChars = Nothing
        Me.mois.acceptNumeric = True
        Me.mois.allCapital = False
        Me.mois.allLower = False
        Me.mois.autoComplete = False
        Me.mois.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.mois.autoSizeDropDown = True
        Me.mois.BackColor = System.Drawing.Color.White
        Me.mois.blockOnMaximum = True
        Me.mois.blockOnMinimum = True
        Me.mois.cb_AcceptLeftZeros = True
        Me.mois.cb_AcceptNegative = False
        Me.mois.currencyBox = False
        Me.mois.Cursor = System.Windows.Forms.Cursors.Default
        Me.mois.dbField = Nothing
        Me.mois.doComboDelete = True
        Me.mois.firstLetterCapital = False
        Me.mois.firstLettersCapital = False
        Me.mois.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mois.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mois.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        Me.mois.itemsToolTipDuration = 10000
        Me.mois.Location = New System.Drawing.Point(248, 24)
        Me.mois.manageText = True
        Me.mois.matchExp = Nothing
        Me.mois.maximum = 12
        Me.mois.minimum = 0
        Me.mois.Name = "mois"
        Me.mois.nbDecimals = CType(-1, Short)
        Me.mois.onlyAlphabet = False
        Me.mois.pathOfList = Nothing
        Me.mois.ReadOnly = False
        Me.mois.refuseAccents = False
        Me.mois.refusedChars = ""
        Me.mois.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mois.showItemsToolTip = False
        Me.mois.Size = New System.Drawing.Size(48, 22)
        Me.mois.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.mois, "(AAAA/MM/JJ)")
        Me.mois.trimText = False
        '
        'jour
        '
        Me.jour.acceptAlpha = False
        Me.jour.acceptedChars = Nothing
        Me.jour.acceptNumeric = True
        Me.jour.allCapital = False
        Me.jour.allLower = False
        Me.jour.autoComplete = False
        Me.jour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.jour.autoSizeDropDown = True
        Me.jour.BackColor = System.Drawing.Color.White
        Me.jour.blockOnMaximum = False
        Me.jour.blockOnMinimum = True
        Me.jour.cb_AcceptLeftZeros = True
        Me.jour.cb_AcceptNegative = False
        Me.jour.currencyBox = False
        Me.jour.Cursor = System.Windows.Forms.Cursors.Default
        Me.jour.dbField = Nothing
        Me.jour.doComboDelete = True
        Me.jour.firstLetterCapital = False
        Me.jour.firstLettersCapital = False
        Me.jour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.jour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.jour.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.jour.itemsToolTipDuration = 10000
        Me.jour.Location = New System.Drawing.Point(296, 24)
        Me.jour.manageText = True
        Me.jour.matchExp = Nothing
        Me.jour.maximum = 31
        Me.jour.minimum = 0
        Me.jour.Name = "jour"
        Me.jour.nbDecimals = CType(-1, Short)
        Me.jour.onlyAlphabet = False
        Me.jour.pathOfList = Nothing
        Me.jour.ReadOnly = False
        Me.jour.refuseAccents = False
        Me.jour.refusedChars = ""
        Me.jour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.jour.showItemsToolTip = False
        Me.jour.Size = New System.Drawing.Size(48, 22)
        Me.jour.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.jour, "(AAAA/MM/JJ)")
        Me.jour.trimText = False
        '
        'RefMenu
        '
        Me.RefMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuRefAucun, Me.menuRefAutre, Me.menuRefCompte, Me.menuPreRefList, Me.menuRefKP})
        '
        'menuRefAucun
        '
        Me.menuRefAucun.Checked = True
        Me.menuRefAucun.Index = 0
        Me.menuRefAucun.Text = "Aucun"
        '
        'menuRefAutre
        '
        Me.menuRefAutre.Index = 1
        Me.menuRefAutre.Text = "Autre"
        '
        'menuRefCompte
        '
        Me.menuRefCompte.Index = 2
        Me.menuRefCompte.Text = "Compte client"
        '
        'menuPreRefList
        '
        Me.menuPreRefList.Index = 3
        Me.menuPreRefList.Text = "Liste prédéterminée"
        '
        'menuRefKP
        '
        Me.menuRefKP.Index = 4
        Me.menuRefKP.Text = "Personne ou organisme clé"
        '
        'AdminBox
        '
        Me.AdminBox.Controls.Add(Me.hideAdmin)
        Me.AdminBox.Controls.Add(Me.addManyClients)
        Me.AdminBox.Controls.Add(Me.fullField)
        Me.AdminBox.Location = New System.Drawing.Point(198, 8)
        Me.AdminBox.Name = "AdminBox"
        Me.AdminBox.Size = New System.Drawing.Size(168, 80)
        Me.AdminBox.TabIndex = 64
        '
        'hideAdmin
        '
        Me.hideAdmin.BackColor = System.Drawing.SystemColors.Control
        Me.hideAdmin.Cursor = System.Windows.Forms.Cursors.Default
        Me.hideAdmin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hideAdmin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hideAdmin.Location = New System.Drawing.Point(152, 0)
        Me.hideAdmin.Name = "hideAdmin"
        Me.hideAdmin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hideAdmin.Size = New System.Drawing.Size(16, 24)
        Me.hideAdmin.TabIndex = 26
        Me.hideAdmin.Text = "X"
        Me.hideAdmin.UseVisualStyleBackColor = False
        '
        'addManyClients
        '
        Me.addManyClients.AutoSize = True
        Me.addManyClients.BackColor = System.Drawing.SystemColors.Control
        Me.addManyClients.Cursor = System.Windows.Forms.Cursors.Default
        Me.addManyClients.Font = New System.Drawing.Font("Arial", 13.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addManyClients.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addManyClients.Location = New System.Drawing.Point(0, 24)
        Me.addManyClients.Name = "addManyClients"
        Me.addManyClients.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.addManyClients.Size = New System.Drawing.Size(145, 21)
        Me.addManyClients.TabIndex = 25
        Me.addManyClients.Text = "Add many clients"
        '
        'fullField
        '
        Me.fullField.AutoSize = True
        Me.fullField.BackColor = System.Drawing.SystemColors.Control
        Me.fullField.Cursor = System.Windows.Forms.Cursors.Default
        Me.fullField.Font = New System.Drawing.Font("Arial", 13.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fullField.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fullField.Location = New System.Drawing.Point(0, 4)
        Me.fullField.Name = "fullField"
        Me.fullField.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fullField.Size = New System.Drawing.Size(92, 21)
        Me.fullField.TabIndex = 24
        Me.fullField.Text = "Full Fields"
        '
        'publipostage
        '
        Me.publipostage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.publipostage.Items.AddRange(New Object() {"Ne pas recevoir d'envoi", "Recevoir l'envoi par la poste", "Recevoir l'envoi par courriel"})
        Me.publipostage.Location = New System.Drawing.Point(192, 304)
        Me.publipostage.Name = "publipostage"
        Me.publipostage.Size = New System.Drawing.Size(168, 22)
        Me.publipostage.TabIndex = 19
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.BackColor = System.Drawing.SystemColors.Control
        Me.label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label21.Location = New System.Drawing.Point(192, 288)
        Me.label21.Name = "label21"
        Me.label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label21.Size = New System.Drawing.Size(85, 14)
        Me.label21.TabIndex = 235
        Me.label21.Text = "Publipostage :"
        '
        '_Label1_16
        '
        Me._Label1_16.AutoSize = True
        Me._Label1_16.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_16.Location = New System.Drawing.Point(8, 328)
        Me._Label1_16.Name = "_Label1_16"
        Me._Label1_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_16.Size = New System.Drawing.Size(77, 14)
        Me._Label1_16.TabIndex = 227
        Me._Label1_16.Text = "Remarques :"
        '
        '_Label1_4
        '
        Me._Label1_4.AutoSize = True
        Me._Label1_4.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_4.Location = New System.Drawing.Point(8, 208)
        Me._Label1_4.Name = "_Label1_4"
        Me._Label1_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_4.Size = New System.Drawing.Size(79, 14)
        Me._Label1_4.TabIndex = 232
        Me._Label1_4.Text = "Téléphones :"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.SystemColors.Control
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(192, 248)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(149, 14)
        Me.label4.TabIndex = 230
        Me.label4.Text = "Adresse du site internet :"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.SystemColors.Control
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(192, 208)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(58, 14)
        Me.label3.TabIndex = 229
        Me.label3.Text = "Courriel :"
        '
        '_Label1_17
        '
        Me._Label1_17.AutoSize = True
        Me._Label1_17.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_17.Location = New System.Drawing.Point(96, 8)
        Me._Label1_17.Name = "_Label1_17"
        Me._Label1_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_17.Size = New System.Drawing.Size(57, 14)
        Me._Label1_17.TabIndex = 228
        Me._Label1_17.Text = "Prénom :"
        '
        '_Label1_15
        '
        Me._Label1_15.AutoSize = True
        Me._Label1_15.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_15.Location = New System.Drawing.Point(192, 168)
        Me._Label1_15.Name = "_Label1_15"
        Me._Label1_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_15.Size = New System.Drawing.Size(174, 14)
        Me._Label1_15.TabIndex = 226
        Me._Label1_15.Text = "Numéro d'assurance maladie :"
        '
        '_Label1_14
        '
        Me._Label1_14.AutoSize = True
        Me._Label1_14.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_14.Location = New System.Drawing.Point(8, 280)
        Me._Label1_14.Name = "_Label1_14"
        Me._Label1_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_14.Size = New System.Drawing.Size(61, 14)
        Me._Label1_14.TabIndex = 225
        Me._Label1_14.Text = "Référent :"
        '
        '_Label1_11
        '
        Me._Label1_11.AutoSize = True
        Me._Label1_11.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_11.Location = New System.Drawing.Point(192, 128)
        Me._Label1_11.Name = "_Label1_11"
        Me._Label1_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_11.Size = New System.Drawing.Size(49, 14)
        Me._Label1_11.TabIndex = 224
        Me._Label1_11.Text = "Métier :"
        '
        '_Label1_8
        '
        Me._Label1_8.AutoSize = True
        Me._Label1_8.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_8.Location = New System.Drawing.Point(192, 88)
        Me._Label1_8.Name = "_Label1_8"
        Me._Label1_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_8.Size = New System.Drawing.Size(72, 14)
        Me._Label1_8.TabIndex = 223
        Me._Label1_8.Text = "Employeur :"
        '
        '_Label1_7
        '
        Me._Label1_7.AutoSize = True
        Me._Label1_7.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_7.Location = New System.Drawing.Point(192, 48)
        Me._Label1_7.Name = "_Label1_7"
        Me._Label1_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_7.Size = New System.Drawing.Size(40, 14)
        Me._Label1_7.TabIndex = 222
        Me._Label1_7.Text = "Sexe :"
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
        Me._Label1_6.Size = New System.Drawing.Size(113, 14)
        Me._Label1_6.TabIndex = 221
        Me._Label1_6.Text = "Date de naissance :"
        '
        '_Label1_5
        '
        Me._Label1_5.AutoSize = True
        Me._Label1_5.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_5.Location = New System.Drawing.Point(8, 168)
        Me._Label1_5.Name = "_Label1_5"
        Me._Label1_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_5.Size = New System.Drawing.Size(72, 14)
        Me._Label1_5.TabIndex = 220
        Me._Label1_5.Text = "Autre nom :"
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
        Me._Label1_0.TabIndex = 219
        Me._Label1_0.Text = "Nom :"
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
        Me._Label1_1.TabIndex = 218
        Me._Label1_1.Text = "Adresse :"
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
        Me._Label1_2.TabIndex = 217
        Me._Label1_2.Text = "Ville :"
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
        Me._Label1_3.TabIndex = 216
        Me._Label1_3.Text = "Code postal :"
        '
        '_Label2_2
        '
        Me._Label2_2.AutoSize = True
        Me._Label2_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label2_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_2.Location = New System.Drawing.Point(40, 144)
        Me._Label2_2.Name = "_Label2_2"
        Me._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_2.Size = New System.Drawing.Size(11, 14)
        Me._Label2_2.TabIndex = 215
        Me._Label2_2.Text = "-"
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
        Me.Telephones.Location = New System.Drawing.Point(8, 224)
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
        'ville
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
        Me.ville.firstLettersCapital = True
        Me.ville.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ville.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ville.itemsToolTipDuration = 10000
        Me.ville.Location = New System.Drawing.Point(8, 104)
        Me.ville.manageText = True
        Me.ville.matchExp = ""
        Me.ville.maximum = 0
        Me.ville.minimum = 0
        Me.ville.Name = "ville"
        Me.ville.nbDecimals = CType(-1, Short)
        Me.ville.onlyAlphabet = True
        Me.ville.pathOfList = ""
        Me.ville.ReadOnly = False
        Me.ville.refuseAccents = False
        Me.ville.refusedChars = ""
        Me.ville.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ville.showItemsToolTip = False
        Me.ville.Size = New System.Drawing.Size(169, 22)
        Me.ville.Sorted = True
        Me.ville.TabIndex = 3
        Me.ville.trimText = False
        '
        'reference
        '
        Me.reference.BackColor = System.Drawing.SystemColors.Control
        Me.reference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.reference.Location = New System.Drawing.Point(40, 296)
        Me.reference.Multiline = True
        Me.reference.Name = "reference"
        Me.reference.ReadOnly = True
        Me.reference.Size = New System.Drawing.Size(144, 32)
        Me.reference.TabIndex = 231
        Me.reference.TabStop = False
        Me.reference.Text = "Nom du référent"
        Me.reference.WordWrap = False
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
        Me.url.Location = New System.Drawing.Point(192, 264)
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
        Me.url.TabIndex = 18
        Me.url.trimText = False
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
        Me.courriel.Location = New System.Drawing.Point(192, 224)
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
        Me.courriel.Size = New System.Drawing.Size(169, 20)
        Me.courriel.TabIndex = 17
        Me.courriel.trimText = False
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
        Me.prenom.Size = New System.Drawing.Size(81, 20)
        Me.prenom.TabIndex = 1
        Me.prenom.trimText = False
        '
        'remarques
        '
        Me.remarques.acceptAlpha = True
        Me.remarques.acceptedChars = ""
        Me.remarques.acceptNumeric = True
        Me.remarques.AcceptsReturn = True
        Me.remarques.allCapital = False
        Me.remarques.allLower = False
        Me.remarques.BackColor = System.Drawing.SystemColors.Window
        Me.remarques.blockOnMaximum = False
        Me.remarques.blockOnMinimum = False
        Me.remarques.cb_AcceptLeftZeros = False
        Me.remarques.cb_AcceptNegative = False
        Me.remarques.currencyBox = False
        Me.remarques.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.remarques.firstLetterCapital = True
        Me.remarques.firstLettersCapital = False
        Me.remarques.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.remarques.ForeColor = System.Drawing.SystemColors.WindowText
        Me.remarques.Location = New System.Drawing.Point(8, 344)
        Me.remarques.manageText = True
        Me.remarques.matchExp = ""
        Me.remarques.maximum = 0
        Me.remarques.MaxLength = 200
        Me.remarques.minimum = 0
        Me.remarques.Multiline = True
        Me.remarques.Name = "remarques"
        Me.remarques.nbDecimals = CType(-1, Short)
        Me.remarques.onlyAlphabet = False
        Me.remarques.refuseAccents = False
        Me.remarques.refusedChars = ""
        Me.remarques.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.remarques.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.remarques.showInternalContextMenu = True
        Me.remarques.Size = New System.Drawing.Size(352, 59)
        Me.remarques.TabIndex = 20
        Me.remarques.trimText = False
        '
        'nam
        '
        Me.nam.acceptAlpha = True
        Me.nam.acceptedChars = ""
        Me.nam.acceptNumeric = True
        Me.nam.AcceptsReturn = True
        Me.nam.allCapital = True
        Me.nam.allLower = False
        Me.nam.BackColor = System.Drawing.SystemColors.Window
        Me.nam.blockOnMaximum = False
        Me.nam.blockOnMinimum = False
        Me.nam.cb_AcceptLeftZeros = False
        Me.nam.cb_AcceptNegative = False
        Me.nam.currencyBox = False
        Me.nam.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nam.firstLetterCapital = False
        Me.nam.firstLettersCapital = False
        Me.nam.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nam.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nam.Location = New System.Drawing.Point(192, 184)
        Me.nam.manageText = True
        Me.nam.matchExp = "AAAA111111X1"
        Me.nam.maximum = 0
        Me.nam.MaxLength = 12
        Me.nam.minimum = 0
        Me.nam.Name = "nam"
        Me.nam.nbDecimals = CType(-1, Short)
        Me.nam.onlyAlphabet = True
        Me.nam.refuseAccents = True
        Me.nam.refusedChars = ""
        Me.nam.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nam.showInternalContextMenu = True
        Me.nam.Size = New System.Drawing.Size(168, 20)
        Me.nam.TabIndex = 16
        Me.nam.trimText = False
        '
        'autrenom
        '
        Me.autrenom.acceptAlpha = True
        Me.autrenom.acceptedChars = " §'"
        Me.autrenom.acceptNumeric = False
        Me.autrenom.AcceptsReturn = True
        Me.autrenom.allCapital = False
        Me.autrenom.allLower = False
        Me.autrenom.BackColor = System.Drawing.SystemColors.Window
        Me.autrenom.blockOnMaximum = False
        Me.autrenom.blockOnMinimum = False
        Me.autrenom.cb_AcceptLeftZeros = False
        Me.autrenom.cb_AcceptNegative = False
        Me.autrenom.currencyBox = False
        Me.autrenom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.autrenom.firstLetterCapital = True
        Me.autrenom.firstLettersCapital = True
        Me.autrenom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autrenom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.autrenom.Location = New System.Drawing.Point(8, 184)
        Me.autrenom.manageText = True
        Me.autrenom.matchExp = ""
        Me.autrenom.maximum = 0
        Me.autrenom.MaxLength = 0
        Me.autrenom.minimum = 0
        Me.autrenom.Name = "autrenom"
        Me.autrenom.nbDecimals = CType(-1, Short)
        Me.autrenom.onlyAlphabet = True
        Me.autrenom.refuseAccents = False
        Me.autrenom.refusedChars = ""
        Me.autrenom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autrenom.showInternalContextMenu = True
        Me.autrenom.Size = New System.Drawing.Size(169, 20)
        Me.autrenom.TabIndex = 6
        Me.autrenom.trimText = False
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
        Me.nom.Size = New System.Drawing.Size(81, 20)
        Me.nom.TabIndex = 0
        Me.nom.trimText = False
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
        Me.adresse.TabIndex = 2
        Me.adresse.trimText = False
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
        Me.codepostal2.Location = New System.Drawing.Point(48, 144)
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
        Me.codepostal1.TabIndex = 4
        Me.codepostal1.trimText = False
        '
        'metierslist
        '
        Me.metierslist.acceptAlpha = True
        Me.metierslist.acceptedChars = Nothing
        Me.metierslist.acceptNumeric = True
        Me.metierslist.allCapital = False
        Me.metierslist.allLower = False
        Me.metierslist.autoComplete = True
        Me.metierslist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.metierslist.autoSizeDropDown = True
        Me.metierslist.BackColor = System.Drawing.Color.White
        Me.metierslist.blockOnMaximum = False
        Me.metierslist.blockOnMinimum = False
        Me.metierslist.cb_AcceptLeftZeros = False
        Me.metierslist.cb_AcceptNegative = False
        Me.metierslist.currencyBox = False
        Me.metierslist.Cursor = System.Windows.Forms.Cursors.Default
        Me.metierslist.dbField = "Metiers.Metier"
        Me.metierslist.doComboDelete = True
        Me.metierslist.firstLetterCapital = True
        Me.metierslist.firstLettersCapital = False
        Me.metierslist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.metierslist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.metierslist.itemsToolTipDuration = 10000
        Me.metierslist.Location = New System.Drawing.Point(192, 144)
        Me.metierslist.manageText = True
        Me.metierslist.matchExp = ""
        Me.metierslist.maximum = 0
        Me.metierslist.minimum = 0
        Me.metierslist.Name = "metierslist"
        Me.metierslist.nbDecimals = CType(-1, Short)
        Me.metierslist.onlyAlphabet = False
        Me.metierslist.pathOfList = ""
        Me.metierslist.ReadOnly = False
        Me.metierslist.refuseAccents = False
        Me.metierslist.refusedChars = ""
        Me.metierslist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.metierslist.showItemsToolTip = False
        Me.metierslist.Size = New System.Drawing.Size(169, 22)
        Me.metierslist.Sorted = True
        Me.metierslist.TabIndex = 15
        Me.metierslist.trimText = False
        '
        'employeurslist
        '
        Me.employeurslist.acceptAlpha = True
        Me.employeurslist.acceptedChars = Nothing
        Me.employeurslist.acceptNumeric = True
        Me.employeurslist.allCapital = False
        Me.employeurslist.allLower = False
        Me.employeurslist.autoComplete = True
        Me.employeurslist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.employeurslist.autoSizeDropDown = True
        Me.employeurslist.BackColor = System.Drawing.Color.White
        Me.employeurslist.blockOnMaximum = False
        Me.employeurslist.blockOnMinimum = False
        Me.employeurslist.cb_AcceptLeftZeros = False
        Me.employeurslist.cb_AcceptNegative = False
        Me.employeurslist.currencyBox = False
        Me.employeurslist.Cursor = System.Windows.Forms.Cursors.Default
        Me.employeurslist.dbField = "Employeurs.Employeur"
        Me.employeurslist.doComboDelete = True
        Me.employeurslist.firstLetterCapital = True
        Me.employeurslist.firstLettersCapital = False
        Me.employeurslist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.employeurslist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.employeurslist.itemsToolTipDuration = 10000
        Me.employeurslist.Location = New System.Drawing.Point(192, 104)
        Me.employeurslist.manageText = True
        Me.employeurslist.matchExp = ""
        Me.employeurslist.maximum = 0
        Me.employeurslist.minimum = 0
        Me.employeurslist.Name = "employeurslist"
        Me.employeurslist.nbDecimals = CType(-1, Short)
        Me.employeurslist.onlyAlphabet = False
        Me.employeurslist.pathOfList = ""
        Me.employeurslist.ReadOnly = False
        Me.employeurslist.refuseAccents = False
        Me.employeurslist.refusedChars = ""
        Me.employeurslist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.employeurslist.showItemsToolTip = False
        Me.employeurslist.Size = New System.Drawing.Size(169, 22)
        Me.employeurslist.Sorted = True
        Me.employeurslist.TabIndex = 14
        Me.employeurslist.trimText = False
        '
        '_sexe_1
        '
        Me._sexe_1.BackColor = System.Drawing.SystemColors.Control
        Me._sexe_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._sexe_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._sexe_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._sexe_1.Location = New System.Drawing.Point(256, 64)
        Me._sexe_1.Name = "_sexe_1"
        Me._sexe_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._sexe_1.Size = New System.Drawing.Size(64, 17)
        Me._sexe_1.TabIndex = 13
        Me._sexe_1.TabStop = True
        Me._sexe_1.Text = "Femme"
        Me._sexe_1.UseVisualStyleBackColor = False
        '
        '_sexe_0
        '
        Me._sexe_0.BackColor = System.Drawing.SystemColors.Control
        Me._sexe_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._sexe_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._sexe_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._sexe_0.Location = New System.Drawing.Point(192, 64)
        Me._sexe_0.Name = "_sexe_0"
        Me._sexe_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._sexe_0.Size = New System.Drawing.Size(64, 17)
        Me._sexe_0.TabIndex = 12
        Me._sexe_0.TabStop = True
        Me._sexe_0.Text = "Homme"
        Me._sexe_0.UseVisualStyleBackColor = False
        '
        'addclient
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(370, 440)
        Me.Controls.Add(Me.selectionner)
        Me.Controls.Add(Me.reference)
        Me.Controls.Add(Me.AdminBox)
        Me.Controls.Add(Me.annee)
        Me.Controls.Add(Me.mois)
        Me.Controls.Add(Me.jour)
        Me.Controls.Add(Me.remarques)
        Me.Controls.Add(Me.publipostage)
        Me.Controls.Add(Me.Telephones)
        Me.Controls.Add(Me.ville)
        Me.Controls.Add(Me.url)
        Me.Controls.Add(Me.courriel)
        Me.Controls.Add(Me.nam)
        Me.Controls.Add(Me.autrenom)
        Me.Controls.Add(Me.adresse)
        Me.Controls.Add(Me.codepostal2)
        Me.Controls.Add(Me.codepostal1)
        Me.Controls.Add(Me.metierslist)
        Me.Controls.Add(Me.employeurslist)
        Me.Controls.Add(Me.prenom)
        Me.Controls.Add(Me.nom)
        Me.Controls.Add(Me.label21)
        Me.Controls.Add(Me.DownTel)
        Me.Controls.Add(Me.UpTel)
        Me.Controls.Add(Me._Label1_16)
        Me.Controls.Add(Me._Label1_4)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me._Label1_17)
        Me.Controls.Add(Me._Label1_15)
        Me.Controls.Add(Me._Label1_14)
        Me.Controls.Add(Me._Label1_11)
        Me.Controls.Add(Me._Label1_8)
        Me.Controls.Add(Me._Label1_7)
        Me.Controls.Add(Me._Label1_6)
        Me.Controls.Add(Me._Label1_5)
        Me.Controls.Add(Me._Label1_0)
        Me.Controls.Add(Me._Label1_1)
        Me.Controls.Add(Me._Label1_2)
        Me.Controls.Add(Me._Label1_3)
        Me.Controls.Add(Me._Label2_2)
        Me.Controls.Add(Me.AddTel)
        Me.Controls.Add(Me.ModifTel)
        Me.Controls.Add(Me.DelTel)
        Me.Controls.Add(Me._sexe_1)
        Me.Controls.Add(Me._sexe_0)
        Me.Controls.Add(Me.ajout)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(277, 266)
        Me.MaximizeBox = False
        Me.Name = "addclient"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ajout d'un compte client"
        Me.AdminBox.ResumeLayout(False)
        Me.AdminBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private formModified As Boolean = False

#Region "Sexe Events"
    Private Sub _sexe_0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _sexe_0.CheckedChanged
        sexe_CheckChanged(1, sender, e)
        formModified = True
    End Sub

    Private Sub _sexe_0_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _sexe_0.KeyDown
        sexe_KeyDown(0, sender, e)
    End Sub

    Private Sub _sexe_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _sexe_1.CheckedChanged
        sexe_CheckChanged(0, sender, e)
        formModified = True
    End Sub

    Private Sub _sexe_1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _sexe_1.KeyDown
        sexe_KeyDown(1, sender, e)
    End Sub
#End Region

#Region "GeneralInfo Form"
    Private Const email_field_not_filled As String = "* Courriel non entré *"
    Private Const url_field_not_filled As String = "* Adresse non entrée *"

    Private Sub jour_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles jour.Validated
        jour.Text = addZeros(jour.Text, 2)
        If jour.Text = "00" Then jour.Text = "01"
    End Sub

    Private Sub mois_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles mois.Validated
        mois.Text = addZeros(mois.Text, 2)
        If mois.Text = "00" Then mois.Text = "01"
    End Sub

    Private Sub courriel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles courriel.KeyDown
        If Not courriel.ReadOnly AndAlso e.KeyCode <> Keys.Enter AndAlso e.KeyCode <> Keys.Tab AndAlso courriel.Text = email_field_not_filled Then
            courriel.Text = ""
            courriel.manageText = True
        End If
    End Sub

    Private Sub url_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles url.KeyDown
        If Not url.ReadOnly AndAlso e.KeyCode <> Keys.Enter AndAlso e.KeyCode <> Keys.Tab AndAlso url.Text = url_field_not_filled Then
            url.Text = ""
            url.manageText = True
        End If
    End Sub

    Private Sub objWithForwardBackwardFocus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nam.KeyDown, prenom.KeyDown, mois.KeyDown, metierslist.KeyDown, adresse.KeyDown, ville.KeyDown, autrenom.KeyDown, codepostal1.KeyDown, codepostal2.KeyDown, courriel.KeyDown, url.KeyDown, publipostage.KeyDown, Telephones.KeyDown
        If e.KeyCode = Keys.Back AndAlso ((TypeOf (sender) Is ComboBox AndAlso CType(sender, ComboBox).DropDownStyle = ComboBoxStyle.DropDown AndAlso sender.Text = "") Or sender.Text = "") Then Me.GetNextControl(sender, False).Focus() : e.Handled = True
        If e.KeyCode = Keys.Enter Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
    End Sub

    Private Sub selectionner_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles selectionner.KeyDown
        If e.KeyCode = Keys.Back Then Me.GetNextControl(sender, False).Focus()
    End Sub

    Private Sub jour_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles jour.KeyUp
        If jour.Text.Length = 2 Then
            If sexe(1).checked = True Then
                CType(sexe(1), RadioButton).Focus()
            Else
                CType(sexe(0), RadioButton).Focus()
            End If
        End If
    End Sub

    Private Sub jour_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles jour.KeyDown
        If e.KeyCode = Keys.Back And jour.Text = "" Then mois.Focus()
        If e.KeyCode = Keys.Enter Then
            If sexe(1).checked = True Then
                CType(sexe(1), RadioButton).Focus()
            Else
                CType(sexe(0), RadioButton).Focus()
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub annee_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles annee.KeyDown
        If e.KeyCode = Keys.Back And annee.Text = "" Then selectionner.Focus()
        If e.KeyCode = Keys.Enter Then mois.Focus() : e.Handled = True
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
        If Telephones.SelectedIndex < 0 Then MessageBox.Show("Veuillez sélectionner un numéro de téléphone", "Impossible de modifier") : Exit Sub

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

    End Sub

    Private Sub delTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelTel.Click
        If Telephones.SelectedIndex < 0 Then MessageBox.Show("Veuillez sélectionner un numéro de téléphone", "Impossible de modifier") : Exit Sub

        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce numéro de téléphone ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Telephones.Items.RemoveAt(Telephones.SelectedIndex)
        DownTel.Enabled = False
        UpTel.Enabled = False
        ModifTel.Enabled = False
        DelTel.Enabled = False

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

    End Sub

    Private Sub publipostage_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles publipostage.SelectionChangeCommitted

    End Sub

    Private Sub allGenForm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles reference.TextChanged, remarques.TextChanged, url.TextChanged, adresse.TextChanged, autrenom.TextChanged, courriel.TextChanged, ville.TextChanged, employeurslist.TextChanged, metierslist.TextChanged

    End Sub

    Private Sub adjustNam(ByVal sender As Object)
        curAdjustNAM = True

        Dim NewNAM, currentNAM As String
        Dim maxLength As Byte

        Dim stripNom As String = Chaines.replaceAccents(nom.Text)
        Chaines.forceManaging(stripNom, Me.nam.currencyBox, "", nam.acceptAlpha, nam.acceptNumeric, nam.onlyAlphabet, nam.refuseAccents, nam.acceptedChars, nam.refusedChars, nam.matchExp, nam.firstLetterCapital, nam.firstLettersCapital, nam.allCapital, nam.allLower, nam.nbDecimals)
        maxLength = stripNom.Length
        If maxLength = 0 Then Exit Sub
        If maxLength > 3 Then maxLength = 3
        NewNAM = stripNom.Substring(0, maxLength)

        If maxLength >= 3 Then
            Dim stripPrenom As String = Chaines.replaceAccents(prenom.Text)
            Chaines.forceManaging(stripPrenom, Me.nam.currencyBox, "", nam.acceptAlpha, nam.acceptNumeric, nam.onlyAlphabet, nam.refuseAccents, nam.acceptedChars, nam.refusedChars, nam.matchExp, nam.firstLetterCapital, nam.firstLettersCapital, nam.allCapital, nam.allLower, nam.nbDecimals)
            maxLength = stripPrenom.Length
            If maxLength = 0 Then GoTo Skipping
            If maxLength > 1 Then maxLength = 1
            NewNAM &= stripPrenom.Substring(0, maxLength)

            If maxLength > 0 And annee.Text.Length = 4 Then
                NewNAM &= annee.Text.Substring(2)
                If mois.Text <> "" Then
                    Dim myMonth As String = mois.Text

                    myMonth = addZeros(myMonth, 2)
                    If sexe(1).checked = True Then myMonth += 50
                    NewNAM &= myMonth

                    If jour.Text <> "" Then
                        Dim myDay As String = jour.Text

                        myDay = addZeros(myDay, 2)
                        NewNAM &= myDay
                    End If
                End If
            End If
        End If

Skipping:
        'Ajustement du NAM
        currentNAM = nam.Text
        NewNAM = replaceAccents(NewNAM)
        If currentNAM.Length <= NewNAM.Length Then
            nam.Text = NewNAM
        Else
            nam.Text = NewNAM & currentNAM.Substring(NewNAM.Length, currentNAM.Length - NewNAM.Length)
        End If

        If TypeOf (sender) Is RadioButton Then
            If sender.checked = True Then
                sender.focus()
            End If
        Else
            'sender.focus()
        End If
        Try
            If TypeOf sender Is ManagedText Or TypeOf sender Is ManagedCombo Then sender.selectionstart = sender.text.length
        Catch ex As Exception
        End Try

        curAdjustNAM = False
    End Sub

    Private Sub annee_TextChanged(ByVal sender As System.Object, ByVal eventArgs As System.EventArgs) Handles annee.TextChanged
        Dim ss As Integer = annee.SelectionStart
        If annee.Text.Length > 4 Then annee.Text = annee.Text.Substring(0, 4)

        annee_SelectedIndexChanged(annee, New System.EventArgs())
        If annee.Text.Length < 4 Then
            'annee.Focus()
            annee.SelectionStart = ss
        End If
        'Me.GetNextControl(sender, True).Focus()
    End Sub

    Private Sub annee_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles annee.SelectedIndexChanged
        adjustNam(annee)

        If Not mois.Text = "" Then mois_SelectedIndexChanged(mois, New System.EventArgs())

    End Sub

    Private Sub codepostal1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles codepostal1.TextChanged
        If codepostal1.Text.Length = 3 Then codepostal2.Focus()

    End Sub

    Private Sub codepostal2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles codepostal2.TextChanged
        If codepostal2.Text.Length = 3 Then autrenom.Focus()

    End Sub

    Private Sub employeurslist_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles employeurslist.KeyDown
        If e.KeyCode = Keys.Back And sender.Text = "" Then
            If sexe(1).Checked = True Then
                sexe(1).Focus()
            Else
                sexe(0).Focus()
            End If
        End If
        If e.KeyCode = Keys.Enter Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
    End Sub

    Private Sub jour_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles jour.TextChanged
        If jour.Text = "" Then Exit Sub
        If jour.Text.Length > 2 Then jour.Text = jour.Text.Substring(0, 2)

        jour_SelectedIndexChanged(jour, New System.EventArgs())
        If Integer.Parse(jour.Text) > Integer.Parse(jour.Items(jour.Items.Count - 1)) Then jour.Text = jour.GetItemText(jour.Items.Item(jour.Items.Count - 1))
    End Sub

    Private Sub jour_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles jour.SelectedIndexChanged
        adjustNam(jour)

    End Sub

    Private Sub mois_TextChanged(ByVal sender As System.Object, ByVal eventArgs As System.EventArgs) Handles mois.TextChanged
        If mois.Text.Length > 2 Then mois.Text.Substring(0, 2)

        If mois.Text.Length > 0 Then
            If jour.Text > jour.GetItemText(jour.Items.Item(jour.Items.Count - 1)) Then jour.Text = jour.GetItemText(jour.Items.Item(jour.Items.Count - 1))
            If CDbl(mois.Text) > 12 Then mois.Text = CStr(12)
            If mois.Text.Length = 2 Then Me.GetNextControl(sender, True)
        End If
        mois_SelectedIndexChanged(mois, New System.EventArgs())
    End Sub

    Private Sub mois_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mois.SelectedIndexChanged
        Dim an3, an2, an1 As String
        Dim monthDays() As Byte = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        If mois.Text.Length > 0 Then
            adjustNam(mois)

            'Modification du nombre de jour dépendamment du mois
            If Not annee.Text = "" Then
                an1 = CDbl(annee.Text) / 400
                an2 = CDbl(annee.Text) / 100
                an3 = CDbl(annee.Text) / 4
                If an3.IndexOf(",") = -1 And an3.IndexOf(".") = -1 Then monthDays(1) = 29
                If an2.IndexOf(",") = -1 And an2.IndexOf(".") = -1 Then monthDays(1) = 28
                If an1.IndexOf(",") = -1 And an1.IndexOf(".") = -1 Then monthDays(1) = 29

                If CDbl(mois.Text) > 0 Then
                    If CDbl(mois.Text) > 12 Then mois.Text = 12
                    Dim curJour As Integer = -1
                    If jour.Text <> "" Then curJour = Integer.Parse(jour.Text)
                    Do
                        If monthDays(CDbl(mois.Text) - 1) > jour.Items.Count Then
                            jour.Items.Add(CStr(jour.Items.Count + 1))
                        ElseIf monthDays(CDbl(mois.Text) - 1) < jour.Items.Count Then
                            jour.Items.RemoveAt(jour.Items.Count - 1)
                        End If
                    Loop Until monthDays(CDbl(mois.Text) - 1) = jour.Items.Count
                    If curJour > -1 AndAlso curJour > Integer.Parse(jour.Items(jour.Items.Count - 1)) Then curJour = Integer.Parse(jour.Items(jour.Items.Count - 1))
                    If curJour > -1 Then jour.Text = curJour
                End If
            End If
        End If


    End Sub

    Private Sub nom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nom.KeyDown
        If e.KeyCode = Keys.Enter Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
    End Sub

    Private Sub remarques_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles remarques.KeyDown
        If e.KeyCode = Keys.Back And sender.Text = "" Then Me.GetNextControl(sender, False).Focus()
    End Sub

    Private Sub selectionner_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectionner.Click
        RefMenu.Show(selectionner, New Point(0, 0))
    End Sub

    Private Sub nom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nom.TextChanged
        adjustNam(nom)

    End Sub

    Private Sub prenom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles prenom.TextChanged
        adjustNam(prenom)

    End Sub

    Private Sub nam_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles nam.TextChanged
        If nam.Text.Length = 12 And curAdjustNAM = False Then courriel.Focus()

    End Sub

    Private curAdjustNAM As Boolean = False

    Private Sub sexe_CheckChanged(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles sexe.checkChanged
        If curAdjustNAM = False Then adjustNam(sexe(index))
    End Sub

    Private Sub sexe_KeyDown(ByRef index As Short, ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles sexe.keyDown
        If e.KeyCode = Keys.Back Then jour.Focus() : e.Handled = True
        If e.KeyCode = Keys.Enter Then employeurslist.Focus() : e.Handled = True
    End Sub

    Private Sub menuRefAucun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRefAucun.Click
        uncheckMenus()
        menuRefAucun.Checked = True
        reference.Text = "Nom du référent"
        reference.Tag = ""
    End Sub

    Private Sub menuRefAutre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRefAutre.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        Dim myRef As String = myInputBoxPlus.Prompt("Veuillez entrer le nom du référent", "Référent")

        If myRef <> "" Then
            reference.Text = myRef : reference.Tag = "AUTRE"
            uncheckMenus()
            menuRefAutre.Checked = True
        End If
    End Sub

    Private Sub menuRefCompte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRefCompte.Click
        Dim myRecherche As New clientSearch()
        myRecherche.Visible = False
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()
    End Sub

    Private Sub menuRefKP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRefKP.Click
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.Visible = False
        myKeyPeople.selected = True
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            uncheckMenus()
            menuRefKP.Checked = True
            reference.Tag = "KP"
            reference.Text = kpChosen.noKP & vbCrLf & kpChosen.kpFullName
        End If
    End Sub

    Private Sub menuPreRefList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        With CType(sender, MenuItem)
            reference.Text = .Text : reference.Tag = "LIST"
            uncheckMenus()
            .Checked = True
        End With
    End Sub
#End Region

    Public Sub unCheckMenus()
        menuRefAucun.Checked = False
        menuRefAutre.Checked = False
        menuRefCompte.Checked = False
        menuRefKP.Checked = False
        Dim i As Short
        With menuPreRefList.MenuItems
            For i = 0 To .Count - 1
                .Item(i).Checked = False
            Next i
        End With
    End Sub

    Private Function adding(Optional ByVal forcedNAM As String = "") As String
        Dim Phones, Referer, N_A_M, sPhones() As String
        N_A_M = nam.Text
        Referer = ""
        If forcedNAM <> "" Then N_A_M = forcedNAM

        'Vérification des champs obligatoires
        If PreferencesManager.getGeneralPreferences()("COCC1") = True And nom.Text = "" Then MessageBox.Show("Le champ 'Nom' est obligatoire", "Information manquante") : nom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC2") = True And prenom.Text = "" Then MessageBox.Show("Le champ 'Prénom' est obligatoire", "Information manquante") : prenom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC3") = True And adresse.Text = "" Then MessageBox.Show("Le champ 'Adresse' est obligatoire", "Information manquante") : adresse.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC4") = True And ville.Text = "" Then MessageBox.Show("Le champ 'Ville' est obligatoire", "Information manquante") : ville.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC5") = True And (codepostal1.Text = "" Or codepostal2.Text = "") Then MessageBox.Show("Le champ 'Code postal' est obligatoire", "Information manquante") : codepostal1.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC6") = True And Telephones.Items.Count = 0 Then MessageBox.Show("Le champ 'Téléphones' est obligatoire", "Information manquante") : AddTel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC9") = True And autrenom.Text = "" Then MessageBox.Show("Le champ 'Autre nom' est obligatoire", "Information manquante") : autrenom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC10") = True And (annee.Text = "" Or mois.Text = "" Or jour.Text = "") Then MessageBox.Show("Le champ 'Date de naissance' est obligatoire", "Information manquante") : annee.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC11") = True And sexe(0).Checked = False And sexe(1).Checked = False Then MessageBox.Show("Le champ 'Sexe' est obligatoire", "Information manquante") : sexe(0).Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC12") = True And employeurslist.Text = "" Then MessageBox.Show("Le champ 'Employeur' est obligatoire", "Information manquante") : employeurslist.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC13") = True And metierslist.Text = "" Then MessageBox.Show("Le champ 'Métier' est obligatoire", "Information manquante") : metierslist.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC15") = True And N_A_M = "" Then MessageBox.Show("Le champ 'Numéro d'assurance maladie' est obligatoire", "Information manquante") : nam.Focus() : Exit Function
        If nam.Text <> "" AndAlso N_A_M.Length < 12 Then MessageBox.Show("Le champ 'Numéro d'assurance maladie' n'est pas complet", "Information incomplète") : nam.Focus() : Exit Function
        If nam.Text <> "" AndAlso User.validateNAM(nam.Text) = False Then MessageBox.Show("Le champ 'Numéro d'assurance maladie' n'est pas valide", "Information invalide") : nam.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC16") = True And reference.Text = "" Then MessageBox.Show("Le champ 'Référence' est obligatoire", "Information manquante") : selectionner.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC17") = True And remarques.Text = "" Then MessageBox.Show("Le champ 'Remarques' est obligatoire", "Information manquante") : remarques.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC18") = True And courriel.Text = "" Then MessageBox.Show("Le champ 'Courriel' est obligatoire", "Information manquante") : courriel.Focus() : Exit Function
        If publipostage.SelectedIndex = 2 And courriel.Text = "" Then MessageBox.Show("Le champ 'Courriel' est obligatoire lorsque le publipostage est envoyé par courriel", "Information manquante") : courriel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC19") = True And url.Text = "" Then MessageBox.Show("Le champ 'Adresse du site internet' est obligatoire", "Information manquante") : url.Focus() : Exit Function
        Dim emailValidation As EmailValidator.ValidationLevels = EmailValidator.isEmailValid(MailsManager.mainFromEmailAddress, courriel.Text)
        If courriel.Text <> email_field_not_filled AndAlso courriel.Text <> "" And emailValidation <> EmailValidator.ValidationLevels.Valid Then
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
        If nam.Text <> "" Then
            Dim verifyNAM() As String = DBLinker.getInstance.readOneDBField("InfoClients", "NAM", "WHERE ((NAM)='" & N_A_M & "');")
            If Not verifyNAM Is Nothing AndAlso verifyNAM.Length <> 0 Then MessageBox.Show("NAM déjà utilisé par un client", "Client déjà existant") : Exit Function
        End If

        'Écriture des données dans la base de données
        ReDim sPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(sPhones, 0)
        Phones = String.Join("§", sPhones)
        If Phones Is Nothing Then Phones = ""
        If Not reference.Tag = "" Then Referer = reference.Tag & "§" & reference.Text.Replace(vbCrLf, "<br>")
        
        Dim dateNaissance As String = "null"
        If annee.Text <> String.Empty AndAlso mois.Text <> String.Empty AndAlso jour.Text <> String.Empty Then dateNaissance = "'" & annee.Text & "/" & mois.Text & "/" & jour.Text & "'"

        Dim noClient As Integer = 0
        If DBLinker.getInstance.writeDB("InfoClients", "NoUser, DateHeureCreation, Nom, Prenom, Adresse, NoVille, CodePostal, Telephones, AutreNom, DateNaissance, SexeHomme, NoEmployeur, NoMetier, NAM, NomReferent, Courriel, URL, Description, Publipostage", ConnectionsManager.currentUser & ",'" & DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.ShortTime) & "','" & nom.Text.Replace("'", "''") & "','" & prenom.Text.Replace("'", "''") & "','" & adresse.Text.Replace("'", "''") & "'," & DBHelper.addItemToADBList("Villes", "NomVille", ville.Text, "NoVille") & ",'" & codepostal1.Text & codepostal2.Text & "','" & Phones.Replace("'", "''") & "','" & autrenom.Text.Replace("'", "''") & "'," & dateNaissance & ",'" & sexe(0).checked & "'," & DBHelper.addItemToADBList("Employeurs", "Employeur", employeurslist.Text, "NoEmployeur") & "," & DBHelper.addItemToADBList("Metiers", "Metier", metierslist.Text, "NoMetier") & ",'" & N_A_M & "','" & Referer.Replace("'", "''") & "'," & If(courriel.Text = email_field_not_filled, "null", "'" & courriel.Text & "'") & "," & If(url.Text = url_field_not_filled, "null", "'" & url.Text & "'") & ",'" & remarques.Text.Replace("'", "''") & "'," & publipostage.SelectedIndex, , , , noClient) = False Then Return ""

        formModified = False

        If forcedNAM = "" AndAlso PreferencesManager.getUserPreferences()("AutoNewRV") = True Then openNewRV(noClient)

        myMainWin.StatusText = "Ajout d'un compte : Client ajouté"
        Return "DONE"
    End Function

    Private Sub ajout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ajout.Click
        If adding() <> "" Then Me.Close()
    End Sub

    Private Sub addclient_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub addclient_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim i As Short
        For i = 1900 To Date.Today.Year
            annee.Items.Add(i)
        Next i

        'Set not filled text
        courriel.manageText = False
        courriel.Text = email_field_not_filled
        url.manageText = False
        url.Text = url_field_not_filled

        'Load Référents prédéterminés
        Dim referents As String = PreferencesManager.getGeneralPreferences()("PreReferents")
        If referents <> "" Then
            Dim refs() As String = referents.Split(New Char() {vbTab})
            With menuPreRefList.MenuItems
                For i = 0 To refs.Length - 1
                    Dim myMenuItem As New MenuItem(refs(i), New EventHandler(AddressOf menuPreRefList_Click))
                    .Add(myMenuItem)
                Next i
            End With
        Else
            menuPreRefList.Visible = False
        End If

        'Load Employeurs
        employeurslist.Items.Clear()
        Dim employeurs() As String = DBLinker.getInstance.readOneDBField("Employeurs", "Employeur", , True)
        If Not employeurs Is Nothing AndAlso employeurs.Length <> 0 Then employeurslist.Items.AddRange(employeurs)

        'Load Métiers
        metierslist.Items.Clear()
        Dim metiers() As String = DBLinker.getInstance.readOneDBField("Metiers", "Metier", , True)
        If Not metiers Is Nothing AndAlso metiers.Length <> 0 Then metierslist.Items.AddRange(metiers)

        'Load Villes
        ville.Items.Clear()
        Dim villes() As String = DBLinker.getInstance.readOneDBField("Villes", "NomVille", , True)
        If Not villes Is Nothing AndAlso villes.Length <> 0 Then ville.Items.AddRange(villes)

        If currentUserName = "Administrateur" Then
            AdminBox.Visible = True
        Else
            AdminBox.Visible = False
        End If

        'Ajout ds la barre d'outils
        formModified = False
    End Sub

    Private Sub fullField_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles fullField.Click
        nom.Text = "Boivin"
        prenom.Text = "Jonathan"
        adresse.Text = "15 Archambault"
        ville.Text = "Charlemagne"
        codepostal1.Text = "J5Z"
        codepostal2.Text = "1Z9"
        Telephones.Items.Add("Maison:555-5555")
        annee.Text = "1983"
        mois.Text = "7"
        jour.Text = "27"
        sexe(0).Checked = True
        employeurslist.Text = "Bell Sympatico"
        metierslist.Text = "Agent en soutien technique"
        nam.Text = nam.Text & "00"
        remarques.Text = "Testing !!!!!! !!"
        courriel.Text = "djon2003@hotmail.com"
        url.Text = "http://www.physiotech.ca"
    End Sub

    Private Sub addclient_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If adding() = "" Then e.Cancel = True
    End Sub

    Private Sub addManyClients_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addManyClients.Click
        Dim n As Integer = InputBox("Veuillez entrer le numéro de départ", "Numéro de départ", "2")
        Dim maxi As Integer = InputBox("Veuillez entrer le numéro de fin", "Numéro de fin", "10000")
        Dim i As Integer
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        For i = n To maxi
            prenom.Text = "Jonathan" & n
            adding(Microsoft.VisualBasic.Left(nom.Text, 3) & Microsoft.VisualBasic.Left(prenom.Text, 1) & addZeros(i, 8))
        Next i

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
    End Sub

    Private Sub hideAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hideAdmin.Click
        AdminBox.Visible = False
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf ajout_Click)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
