Public Class msgContact
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        Me.MdiParent = myMainWin

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        configList(Courriels)

        imgList = New ImageList
        Try
            With imgList.Images
                .Add(DrawingManager.getInstance.getImage("save.jpg"))
                .Add(DrawingManager.getInstance.getImage("ajouter16.gif"))
            End With
        Catch
        End Try

        'Chargement des images
        Me.SelectKeyPeople.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.SelectCompte.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.ModifEmail.Image = DrawingManager.getInstance.getImage("modifier16.gif")
        Me.ModifTel.Image = DrawingManager.getInstance.getImage("modifier16.gif")
        Me.DelTel.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.DelEmail.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.AddEmail.Image = imgList.Images(1)
        Me.submit.Image = imgList.Images(0)
        Me.AddTel.Image = imgList.Images(1)
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CodePostal1 As ManagedText
    Friend WithEvents Adresse As ManagedText
    Friend WithEvents Courriels As CI.Controls.List
    Friend WithEvents URL As ManagedText
    Friend WithEvents Afficher As Clinica.ManagedCombo
    Friend WithEvents Prenom As ManagedText
    Friend WithEvents Surnom As ManagedText
    Friend WithEvents Nom As ManagedText
    Friend WithEvents CodePostal2 As ManagedText
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents SendTextOnly As System.Windows.Forms.CheckBox
    Friend WithEvents ModifTel As System.Windows.Forms.Button
    Friend WithEvents DelTel As System.Windows.Forms.Button
    Friend WithEvents DelEmail As System.Windows.Forms.Button
    Friend WithEvents AddEmail As System.Windows.Forms.Button
    Friend WithEvents submit As System.Windows.Forms.Button
    Friend WithEvents AddTel As System.Windows.Forms.Button
    Friend WithEvents ModifEmail As System.Windows.Forms.Button
    Friend WithEvents SelectCompte As System.Windows.Forms.Button
    Friend WithEvents SelectKeyPeople As System.Windows.Forms.Button
    Friend WithEvents KeyPeopleRef As System.Windows.Forms.Label
    Friend WithEvents CompteRef As System.Windows.Forms.Label
    Friend WithEvents Telephones As Clinica.ManagedCombo
    Friend WithEvents Ville As Clinica.ManagedCombo
    Friend WithEvents Pays As Clinica.ManagedCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.SendTextOnly = New System.Windows.Forms.CheckBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ModifTel = New System.Windows.Forms.Button
        Me.DelTel = New System.Windows.Forms.Button
        Me.DelEmail = New System.Windows.Forms.Button
        Me.AddEmail = New System.Windows.Forms.Button
        Me.submit = New System.Windows.Forms.Button
        Me.AddTel = New System.Windows.Forms.Button
        Me.ModifEmail = New System.Windows.Forms.Button
        Me.SelectCompte = New System.Windows.Forms.Button
        Me.SelectKeyPeople = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CodePostal2 = New ManagedText
        Me.KeyPeopleRef = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.CompteRef = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Telephones = New Clinica.ManagedCombo
        Me.Pays = New Clinica.ManagedCombo
        Me.CodePostal1 = New ManagedText
        Me.Adresse = New ManagedText
        Me.Courriels = New CI.Controls.List
        Me.URL = New ManagedText
        Me.Afficher = New Clinica.ManagedCombo
        Me.Prenom = New ManagedText
        Me.Surnom = New ManagedText
        Me.Nom = New ManagedText
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Ville = New Clinica.ManagedCombo
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SendTextOnly
        '
        Me.SendTextOnly.Location = New System.Drawing.Point(8, 336)
        Me.SendTextOnly.Name = "SendTextOnly"
        Me.SendTextOnly.Size = New System.Drawing.Size(280, 16)
        Me.SendTextOnly.TabIndex = 14
        Me.SendTextOnly.Text = "Envoyer des messages en texte brute uniquement"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'ModifTel
        '
        Me.ModifTel.Enabled = False
        Me.ModifTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ModifTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ModifTel.Location = New System.Drawing.Point(336, 232)
        Me.ModifTel.Name = "ModifTel"
        Me.ModifTel.Size = New System.Drawing.Size(24, 24)
        Me.ModifTel.TabIndex = 56
        Me.ModifTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ModifTel, "Modifier un numéro de téléphone")
        '
        'DelTel
        '
        Me.DelTel.Enabled = False
        Me.DelTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelTel.Location = New System.Drawing.Point(368, 232)
        Me.DelTel.Name = "DelTel"
        Me.DelTel.Size = New System.Drawing.Size(24, 24)
        Me.DelTel.TabIndex = 55
        Me.DelTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DelTel, "Enlever un numéro de téléphone")
        '
        'DelEmail
        '
        Me.DelEmail.Enabled = False
        Me.DelEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelEmail.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelEmail.Location = New System.Drawing.Point(368, 148)
        Me.DelEmail.Name = "DelEmail"
        Me.DelEmail.Size = New System.Drawing.Size(24, 24)
        Me.DelEmail.TabIndex = 48
        Me.DelEmail.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DelEmail, "Enlever une adresse de courriel")
        '
        'AddEmail
        '
        Me.AddEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddEmail.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddEmail.Location = New System.Drawing.Point(368, 100)
        Me.AddEmail.Name = "AddEmail"
        Me.AddEmail.Size = New System.Drawing.Size(24, 24)
        Me.AddEmail.TabIndex = 47
        Me.AddEmail.TabStop = False
        Me.ToolTip1.SetToolTip(Me.AddEmail, "Ajout d'une adresse de courriel")
        '
        'submit
        '
        Me.submit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.submit.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.submit.Location = New System.Drawing.Point(376, 336)
        Me.submit.Name = "submit"
        Me.submit.Size = New System.Drawing.Size(24, 24)
        Me.submit.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.submit, "Enregistrer le contact")
        '
        'AddTel
        '
        Me.AddTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddTel.Location = New System.Drawing.Point(304, 232)
        Me.AddTel.Name = "AddTel"
        Me.AddTel.Size = New System.Drawing.Size(24, 24)
        Me.AddTel.TabIndex = 57
        Me.AddTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.AddTel, "Ajout d'un numéro de téléphone")
        '
        'ModifEmail
        '
        Me.ModifEmail.Enabled = False
        Me.ModifEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ModifEmail.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ModifEmail.Location = New System.Drawing.Point(368, 124)
        Me.ModifEmail.Name = "ModifEmail"
        Me.ModifEmail.Size = New System.Drawing.Size(24, 24)
        Me.ModifEmail.TabIndex = 58
        Me.ModifEmail.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ModifEmail, "Modifier une adresse de courriel")
        '
        'SelectCompte
        '
        Me.SelectCompte.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SelectCompte.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SelectCompte.Location = New System.Drawing.Point(152, 264)
        Me.SelectCompte.Name = "SelectCompte"
        Me.SelectCompte.Size = New System.Drawing.Size(24, 20)
        Me.SelectCompte.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.SelectCompte, "Sélectionner un compte client")
        '
        'SelectKeyPeople
        '
        Me.SelectKeyPeople.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SelectKeyPeople.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SelectKeyPeople.Location = New System.Drawing.Point(152, 288)
        Me.SelectKeyPeople.Name = "SelectKeyPeople"
        Me.SelectKeyPeople.Size = New System.Drawing.Size(24, 20)
        Me.SelectKeyPeople.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.SelectKeyPeople, "Sélectionner une personne / organisme clé")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CodePostal2)
        Me.GroupBox1.Controls.Add(Me.KeyPeopleRef)
        Me.GroupBox1.Controls.Add(Me.SelectKeyPeople)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.CompteRef)
        Me.GroupBox1.Controls.Add(Me.SelectCompte)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.ModifEmail)
        Me.GroupBox1.Controls.Add(Me.AddTel)
        Me.GroupBox1.Controls.Add(Me.ModifTel)
        Me.GroupBox1.Controls.Add(Me.DelTel)
        Me.GroupBox1.Controls.Add(Me.Telephones)
        Me.GroupBox1.Controls.Add(Me.Ville)
        Me.GroupBox1.Controls.Add(Me.Pays)
        Me.GroupBox1.Controls.Add(Me.CodePostal1)
        Me.GroupBox1.Controls.Add(Me.Adresse)
        Me.GroupBox1.Controls.Add(Me.DelEmail)
        Me.GroupBox1.Controls.Add(Me.AddEmail)
        Me.GroupBox1.Controls.Add(Me.Courriels)
        Me.GroupBox1.Controls.Add(Me.URL)
        Me.GroupBox1.Controls.Add(Me.Afficher)
        Me.GroupBox1.Controls.Add(Me.Prenom)
        Me.GroupBox1.Controls.Add(Me.Surnom)
        Me.GroupBox1.Controls.Add(Me.Nom)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(400, 320)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Informations sur le contact"
        '
        'CodePostal2
        '
        Me.CodePostal2.acceptAlpha = True
        Me.CodePostal2.acceptedChars = ""
        Me.CodePostal2.acceptNumeric = True
        Me.CodePostal2.allCapital = True
        Me.CodePostal2.allLower = False
        Me.CodePostal2.cb_AcceptNegative = False
        Me.CodePostal2.currencyBox = False
        Me.CodePostal2.firstLetterCapital = False
        Me.CodePostal2.firstLettersCapital = False
        Me.CodePostal2.Location = New System.Drawing.Point(120, 208)
        Me.CodePostal2.matchExp = "1A1"
        Me.CodePostal2.MaxLength = 3
        Me.CodePostal2.Name = "CodePostal2"
        Me.CodePostal2.nbDecimals = CType(-1, Short)
        Me.CodePostal2.onlyAlphabet = True
        Me.CodePostal2.refuseAccents = True
        Me.CodePostal2.refusedChars = ""
        Me.CodePostal2.Size = New System.Drawing.Size(32, 20)
        Me.CodePostal2.TabIndex = 9
        Me.CodePostal2.trimText = False
        '
        'KeyPeopleRef
        '
        Me.KeyPeopleRef.AutoSize = True
        Me.KeyPeopleRef.Location = New System.Drawing.Point(184, 288)
        Me.KeyPeopleRef.Name = "KeyPeopleRef"
        Me.KeyPeopleRef.Size = New System.Drawing.Size(92, 13)
        Me.KeyPeopleRef.TabIndex = 66
        Me.KeyPeopleRef.Text = "Aucune référence"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 288)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(136, 13)
        Me.Label16.TabIndex = 64
        Me.Label16.Text = "Personne / Organisme clé :"
        '
        'CompteRef
        '
        Me.CompteRef.AutoSize = True
        Me.CompteRef.Location = New System.Drawing.Point(184, 264)
        Me.CompteRef.Name = "CompteRef"
        Me.CompteRef.Size = New System.Drawing.Size(104, 13)
        Me.CompteRef.TabIndex = 63
        Me.CompteRef.Text = "Aucun compte client"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 264)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 13)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "Compte client :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(112, 210)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 13)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "-"
        '
        'Telephones
        '
        Me.Telephones.acceptAlpha = True
        Me.Telephones.acceptedChars = Nothing
        Me.Telephones.acceptNumeric = True
        Me.Telephones.allCapital = False
        Me.Telephones.allLower = False
        Me.Telephones.autoComplete = True
        Me.Telephones.cb_AcceptNegative = False
        Me.Telephones.currencyBox = False
        Me.Telephones.dbField = Nothing
        Me.Telephones.doComboDelete = True
        Me.Telephones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Telephones.firstLetterCapital = False
        Me.Telephones.firstLettersCapital = False
        Me.Telephones.Location = New System.Drawing.Point(80, 232)
        Me.Telephones.manageText = True
        Me.Telephones.matchExp = Nothing
        Me.Telephones.Name = "Telephones"
        Me.Telephones.nbDecimals = CType(-1, Short)
        Me.Telephones.onlyAlphabet = False
        Me.Telephones.pathOfList = Nothing
        Me.Telephones.refuseAccents = False
        Me.Telephones.refusedChars = ""
        Me.Telephones.Size = New System.Drawing.Size(216, 21)
        Me.Telephones.TabIndex = 11
        Me.Telephones.trimText = False
        '
        'Pays
        '
        Me.Pays.acceptAlpha = True
        Me.Pays.acceptedChars = " §'§-§.§/§|§\§(§)"
        Me.Pays.acceptNumeric = False
        Me.Pays.allCapital = False
        Me.Pays.allLower = False
        Me.Pays.autoComplete = True
        Me.Pays.cb_AcceptNegative = False
        Me.Pays.currencyBox = False
        Me.Pays.dbField = "Pays.Pays"
        Me.Pays.doComboDelete = True
        Me.Pays.firstLetterCapital = True
        Me.Pays.firstLettersCapital = True
        Me.Pays.Location = New System.Drawing.Point(256, 208)
        Me.Pays.manageText = True
        Me.Pays.matchExp = Nothing
        Me.Pays.Name = "Pays"
        Me.Pays.nbDecimals = CType(-1, Short)
        Me.Pays.onlyAlphabet = True
        Me.Pays.pathOfList = ""
        Me.Pays.refuseAccents = False
        Me.Pays.refusedChars = ""
        Me.Pays.Size = New System.Drawing.Size(136, 21)
        Me.Pays.TabIndex = 10
        Me.Pays.trimText = False
        '
        'CodePostal1
        '
        Me.CodePostal1.acceptAlpha = True
        Me.CodePostal1.acceptedChars = ""
        Me.CodePostal1.acceptNumeric = True
        Me.CodePostal1.allCapital = True
        Me.CodePostal1.allLower = False
        Me.CodePostal1.cb_AcceptNegative = False
        Me.CodePostal1.currencyBox = False
        Me.CodePostal1.firstLetterCapital = False
        Me.CodePostal1.firstLettersCapital = False
        Me.CodePostal1.Location = New System.Drawing.Point(80, 208)
        Me.CodePostal1.matchExp = "A1A"
        Me.CodePostal1.MaxLength = 3
        Me.CodePostal1.Name = "CodePostal1"
        Me.CodePostal1.nbDecimals = CType(-1, Short)
        Me.CodePostal1.onlyAlphabet = True
        Me.CodePostal1.refuseAccents = True
        Me.CodePostal1.refusedChars = ""
        Me.CodePostal1.Size = New System.Drawing.Size(32, 20)
        Me.CodePostal1.TabIndex = 8
        Me.CodePostal1.trimText = False
        '
        'Adresse
        '
        Me.Adresse.acceptAlpha = True
        Me.Adresse.acceptedChars = ""
        Me.Adresse.acceptNumeric = True
        Me.Adresse.allCapital = False
        Me.Adresse.allLower = False
        Me.Adresse.cb_AcceptNegative = False
        Me.Adresse.currencyBox = False
        Me.Adresse.firstLetterCapital = True
        Me.Adresse.firstLettersCapital = True
        Me.Adresse.Location = New System.Drawing.Point(64, 184)
        Me.Adresse.matchExp = ""
        Me.Adresse.Name = "Adresse"
        Me.Adresse.nbDecimals = CType(-1, Short)
        Me.Adresse.onlyAlphabet = False
        Me.Adresse.refuseAccents = False
        Me.Adresse.refusedChars = ""
        Me.Adresse.Size = New System.Drawing.Size(136, 20)
        Me.Adresse.TabIndex = 6
        Me.Adresse.trimText = False
        '
        'Courriels
        '
        Me.Courriels.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.Courriels.autoAdjust = True
        Me.Courriels.autoKeyDownSelection = True
        Me.Courriels.autoSizeHorizontally = False
        Me.Courriels.autoSizeVertically = False
        Me.Courriels.BackColor = System.Drawing.Color.White
        Me.Courriels.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.Courriels.baseBackColor = System.Drawing.Color.White
        Me.Courriels.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.Courriels.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.Courriels.bgColor = System.Drawing.Color.White
        Me.Courriels.borderColor = System.Drawing.Color.Empty
        Me.Courriels.borderSelColor = System.Drawing.Color.Empty
        Me.Courriels.borderStyle = CI.Controls.List.BSType.Fixed3D
        Me.Courriels.CausesValidation = False
        Me.Courriels.clickEnabled = True
        Me.Courriels.do3D = False
        Me.Courriels.draw = False
        Me.Courriels.hScrollColor = System.Drawing.SystemColors.Control
        Me.Courriels.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.Courriels.hScrolling = True
        Me.Courriels.hsValue = CType(0, Short)
        Me.Courriels.itemBorder = CType(0, Short)
        Me.Courriels.Location = New System.Drawing.Point(8, 112)
        Me.Courriels.itemMargin = CType(0, Short)
        Me.Courriels.mouseMove3D = False
        Me.Courriels.mouseSpeed = 0
        Me.Courriels.Name = "Courriels"
        Me.Courriels.objMaxHeight = 0.0!
        Me.Courriels.objMaxWidth = 0.0!
        Me.Courriels.objMinHeight = 0.0!
        Me.Courriels.objMinWidth = 0.0!
        Me.Courriels.reverseSorting = False
        Me.Courriels.selected = CType(-1, Short)
        Me.Courriels.selectedClickAllowed = False
        Me.Courriels.Size = New System.Drawing.Size(352, 56)
        Me.Courriels.sorted = True
        Me.Courriels.TabIndex = 5
        Me.Courriels.toolTipText = "Double-clique : Sélectionner l'adresse par défaut"
        Me.Courriels.vScrollColor = System.Drawing.SystemColors.Control
        Me.Courriels.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.Courriels.vScrolling = True
        Me.Courriels.vsValue = CType(0, Short)
        '
        'URL
        '
        Me.URL.acceptAlpha = True
        Me.URL.acceptedChars = ""
        Me.URL.acceptNumeric = True
        Me.URL.allCapital = False
        Me.URL.allLower = False
        Me.URL.cb_AcceptNegative = False
        Me.URL.currencyBox = False
        Me.URL.firstLetterCapital = False
        Me.URL.firstLettersCapital = False
        Me.URL.Location = New System.Drawing.Point(80, 72)
        Me.URL.matchExp = ""
        Me.URL.Name = "URL"
        Me.URL.nbDecimals = CType(-1, Short)
        Me.URL.onlyAlphabet = False
        Me.URL.refuseAccents = False
        Me.URL.refusedChars = ""
        Me.URL.Size = New System.Drawing.Size(312, 20)
        Me.URL.TabIndex = 4
        Me.URL.trimText = False
        '
        'Afficher
        '
        Me.Afficher.acceptAlpha = True
        Me.Afficher.acceptedChars = Nothing
        Me.Afficher.acceptNumeric = True
        Me.Afficher.allCapital = False
        Me.Afficher.allLower = False
        Me.Afficher.autoComplete = True
        Me.Afficher.cb_AcceptNegative = False
        Me.Afficher.currencyBox = False
        Me.Afficher.dbField = Nothing
        Me.Afficher.doComboDelete = True
        Me.Afficher.firstLetterCapital = False
        Me.Afficher.firstLettersCapital = False
        Me.Afficher.Location = New System.Drawing.Point(256, 40)
        Me.Afficher.manageText = True
        Me.Afficher.matchExp = Nothing
        Me.Afficher.Name = "Afficher"
        Me.Afficher.nbDecimals = CType(-1, Short)
        Me.Afficher.onlyAlphabet = False
        Me.Afficher.pathOfList = Nothing
        Me.Afficher.refuseAccents = False
        Me.Afficher.refusedChars = ""
        Me.Afficher.Size = New System.Drawing.Size(136, 21)
        Me.Afficher.TabIndex = 3
        Me.Afficher.trimText = False
        '
        'Prenom
        '
        Me.Prenom.acceptAlpha = True
        Me.Prenom.acceptedChars = " §'§-"
        Me.Prenom.acceptNumeric = False
        Me.Prenom.allCapital = False
        Me.Prenom.allLower = False
        Me.Prenom.cb_AcceptNegative = False
        Me.Prenom.currencyBox = False
        Me.Prenom.firstLetterCapital = True
        Me.Prenom.firstLettersCapital = True
        Me.Prenom.Location = New System.Drawing.Point(256, 16)
        Me.Prenom.matchExp = ""
        Me.Prenom.Name = "Prenom"
        Me.Prenom.nbDecimals = CType(-1, Short)
        Me.Prenom.onlyAlphabet = True
        Me.Prenom.refuseAccents = False
        Me.Prenom.refusedChars = ""
        Me.Prenom.Size = New System.Drawing.Size(136, 20)
        Me.Prenom.TabIndex = 1
        Me.Prenom.trimText = False
        '
        'Surnom
        '
        Me.Surnom.acceptAlpha = True
        Me.Surnom.acceptedChars = ""
        Me.Surnom.acceptNumeric = True
        Me.Surnom.allCapital = False
        Me.Surnom.allLower = False
        Me.Surnom.cb_AcceptNegative = False
        Me.Surnom.currencyBox = False
        Me.Surnom.firstLetterCapital = False
        Me.Surnom.firstLettersCapital = False
        Me.Surnom.Location = New System.Drawing.Point(56, 40)
        Me.Surnom.matchExp = ""
        Me.Surnom.Name = "Surnom"
        Me.Surnom.nbDecimals = CType(-1, Short)
        Me.Surnom.onlyAlphabet = False
        Me.Surnom.refuseAccents = False
        Me.Surnom.refusedChars = ""
        Me.Surnom.Size = New System.Drawing.Size(136, 20)
        Me.Surnom.TabIndex = 2
        Me.Surnom.trimText = False
        '
        'Nom
        '
        Me.Nom.acceptAlpha = True
        Me.Nom.acceptedChars = " §'§-"
        Me.Nom.acceptNumeric = False
        Me.Nom.allCapital = False
        Me.Nom.allLower = False
        Me.Nom.cb_AcceptNegative = False
        Me.Nom.currencyBox = False
        Me.Nom.firstLetterCapital = True
        Me.Nom.firstLettersCapital = True
        Me.Nom.Location = New System.Drawing.Point(56, 16)
        Me.Nom.matchExp = ""
        Me.Nom.Name = "Nom"
        Me.Nom.nbDecimals = CType(-1, Short)
        Me.Nom.onlyAlphabet = True
        Me.Nom.refuseAccents = False
        Me.Nom.refusedChars = ""
        Me.Nom.Size = New System.Drawing.Size(136, 20)
        Me.Nom.TabIndex = 0
        Me.Nom.trimText = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 72)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 13)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "Site internet :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 232)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "Téléphones :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(216, 208)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Pays :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 208)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 13)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Code postal :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(216, 184)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "Ville :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 184)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Adresse :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Adresses de courriel :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(208, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Afficher :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Surnom :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(208, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Prénom :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Nom :"
        '
        'Ville
        '
        Me.Ville.acceptAlpha = True
        Me.Ville.acceptedChars = " §'§-§.§/§|§\§(§)"
        Me.Ville.acceptNumeric = False
        Me.Ville.allCapital = False
        Me.Ville.allLower = False
        Me.Ville.autoComplete = True
        Me.Ville.cb_AcceptNegative = False
        Me.Ville.currencyBox = False
        Me.Ville.dbField = "Villes.NomVille"
        Me.Ville.doComboDelete = True
        Me.Ville.firstLetterCapital = True
        Me.Ville.firstLettersCapital = True
        Me.Ville.Location = New System.Drawing.Point(256, 181)
        Me.Ville.manageText = True
        Me.Ville.matchExp = Nothing
        Me.Ville.Name = "Ville"
        Me.Ville.nbDecimals = CType(-1, Short)
        Me.Ville.onlyAlphabet = True
        Me.Ville.pathOfList = ""
        Me.Ville.refuseAccents = False
        Me.Ville.refusedChars = ""
        Me.Ville.Size = New System.Drawing.Size(136, 21)
        Me.Ville.TabIndex = 10
        Me.Ville.trimText = False
        '
        'msgContact
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(416, 367)
        Me.Controls.Add(Me.submit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.SendTextOnly)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "msgContact"
        Me.ShowInTaskbar = False
        Me.Text = "Fenêtre d'un contact"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private imgList As ImageList
    Private noContactFolder As Integer = 0
    Private noContact As Integer = 0
    Private formModified As Boolean = False
    Private _AllowModification As Boolean = True

    Public WriteOnly Property setFormModified() As Boolean
        Set(ByVal value As Boolean)
            formModified = value
        End Set
    End Property

    Private Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal value As Boolean)
            _AllowModification = value

            lockItems(Not value)
        End Set
    End Property

    Private Sub lockItems(ByVal trueFalse As Boolean)
        Nom.Enabled = Not trueFalse
        Prenom.Enabled = Not trueFalse
        Afficher.Enabled = Not trueFalse
        URL.Enabled = Not trueFalse
        Surnom.Enabled = Not trueFalse
        Adresse.Enabled = Not trueFalse
        AddEmail.Enabled = Not trueFalse
        Ville.Enabled = Not trueFalse
        CodePostal1.Enabled = Not trueFalse
        CodePostal2.Enabled = Not trueFalse
        Pays.Enabled = Not trueFalse
        AddTel.Enabled = Not trueFalse
        SelectCompte.Enabled = Not trueFalse
        SelectKeyPeople.Enabled = Not trueFalse
        SendTextOnly.Enabled = Not trueFalse
        submit.Enabled = Not trueFalse
    End Sub

    Public Sub loading(ByVal noContactFolder As Integer, Optional ByVal noContact As Integer = 0)
        Me.noContactFolder = noContactFolder
        If noContact = 0 Then
            Me.Text = "Ajout d'un contact"
            submit.Image = imgList.Images(1)
            ToolTip1.SetToolTip(submit, "Ajouter le contact")
            Exit Sub
        End If

        Me.noContact = noContact

        ToolTip1.SetToolTip(submit, "Enregistrer le contact")
        Me.Text = "Modification d'un contact"
        submit.Image = imgList.Images(0)

        If lockSecteur("Contact-" & noContact & ".lock", True, "Modification d'un contact") = False Then
            allowModification = False
        Else
            allowModification = True
        End If

        Dim myContact(,) As String = DBLinker.getInstance.readDB("Contacts LEFT JOIN Villes ON Contacts.NoVille=Villes.NoVille LEFT JOIN Pays ON Pays.NoPays=Contacts.NoPays", "Nom,Prenom,Surnom,Afficher,Courriel,Courriels,TextMsgOnly,Adresse,NomVille,CodePostal,Pays,Telephones,URL,NoClient,NoKP", "WHERE NoContact=" & noContact)
        Dim i, n As Short
        Nom.Text = myContact(0, 0)
        Prenom.Text = myContact(1, 0)
        Surnom.Text = myContact(2, 0)
        Afficher.Text = myContact(3, 0)
        Dim myEmails() As String = myContact(5, 0).Split(New Char() {"§"})
        Courriels.cls()
        For i = 0 To myEmails.Length - 1
            n = Courriels.add(myEmails(i))
            If myEmails(i) = myContact(4, 0) Then Courriels.ItemFont(n) = New Font(Courriels.ItemFont(n), FontStyle.Bold)
        Next i
        Courriels.draw = True : Courriels.draw = False
        SendTextOnly.Checked = myContact(6, 0)
        Adresse.Text = myContact(7, 0)
        Ville.Text = myContact(8, 0)
        If myContact(9, 0).Length <= 3 Then
            CodePostal1.Text = myContact(9, 0)
        Else
            CodePostal1.Text = myContact(9, 0).Substring(0, 3)
            CodePostal2.Text = myContact(9, 0).Substring(3)
        End If
        Pays.Text = myContact(10, 0)
        If myContact(11, 0) <> "" Then
            Telephones.Items.AddRange(myContact(11, 0).Split(New Char() {"§"}))
            Telephones.SelectedIndex = 0
        End If
        URL.Text = myContact(12, 0)
        If myContact(13, 0) <> "" Then CompteRef.Text = getClientName(myContact(13, 0)) & " (" & myContact(13, 0) & ")"
        If myContact(14, 0) <> "" Then KeyPeopleRef.Text = getKPName(myContact(14, 0)) & " (" & myContact(14, 0) & ")"
        formModified = False
    End Sub

    Private Sub msgContact_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        'GotFocusWindow(Me)
    End Sub

    Private Sub msgContact_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub msgContact_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        'RemoveWinFromToolBar(Me)
    End Sub

    Private Sub msgContact_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myPays() As String = DBLinker.getInstance.readOneDBField("Pays", "Pays", , True)
        If Not myPays Is Nothing AndAlso myPays.Length <> 0 Then Pays.Items.AddRange(myPays)

        Dim villes() As String = DBLinker.getInstance.readOneDBField("Villes", "NomVille", , True)
        Ville.Items.Clear()
        If Not villes Is Nothing AndAlso villes.Length <> 0 Then Ville.Items.AddRange(villes)
    End Sub

    Private Sub afficherAdjust(ByVal sender As Object, ByVal e As System.EventArgs) Handles Nom.TextChanged, Prenom.TextChanged, Surnom.TextChanged
        Afficher.Items.Clear()

        If Surnom.Text <> "" Then Afficher.Items.Add(Surnom.Text)
        If Nom.Text <> "" And Prenom.Text <> "" Then
            Afficher.Items.Add(Nom.Text & "," & Prenom.Text)
            Afficher.Items.Add(Prenom.Text & " " & Nom.Text)
        End If
    End Sub

    Private Sub objects_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Nom.TextChanged, Prenom.TextChanged, Surnom.TextChanged, Afficher.TextChanged, URL.TextChanged, Adresse.TextChanged, CodePostal1.TextChanged, CodePostal2.TextChanged, Pays.TextChanged, Ville.TextChanged
        formModified = True
    End Sub

    Private Sub addEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddEmail.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.acceptedChars = "!§#§|§$§%§?§&§*§-§@§.§_§=§+§£§¢§¤§¬§¦§²§³§¼§½§¾§^§¨§¸§`§'§«§»§°§~§¯§´"
        myInputBoxPlus.allLower = True
        myInputBoxPlus.onlyAlphabet = True
        myInputBoxPlus.refuseAccents = False
        Dim newEmail As String = ""
        Dim emailValidation As EmailValidator.ValidationLevels = EmailValidator.ValidationLevels.Valid
        Do
            newEmail = myInputBoxPlus("Veuillez entrer l'adresse de courriel", "Adresse de courriel", newEmail)
            If newEmail = "" Then Exit Sub

            emailValidation = EmailValidator.isEmailValid(MailsManager.mainFromEmailAddress, newEmail)
            If emailValidation <> EmailValidator.ValidationLevels.Valid Then
                Dim message As String = String.Empty
                Dim domain As String = newEmail.Substring(newEmail.IndexOf("@") + 1)
                Select Case emailValidation
                    Case EmailValidator.ValidationLevels.WrongStructure
                        message = "Veuillez vous assurez que l'adresse de courriel soit valide :" & vbCrLf & "alias@domaine.extension" & vbCrLf & "Exemple : info@cints.net"

                    Case EmailValidator.ValidationLevels.DomainNotExists
                        message = "Le nom de domaine """ & domain & """ n'existe pas ou n'a pas de serveur de courriel"

                    Case EmailValidator.ValidationLevels.NotConfirmedByDomain
                        message = "L'adresse a été rejeté par le nom de domaine """ & domain & """"
                End Select

                MessageBox.Show(message, "Courriel invalide")
            End If
        Loop While emailValidation <> EmailValidator.ValidationLevels.Valid

        Dim exists As Short = Courriels.findStringExact(newEmail, False)
        If exists <> -1 Then
            MessageBox.Show("Adresse de courriel déjà présente dans ce contact", "Adresse déjà existante")
            Exit Sub
        End If

        Dim n As Short = Courriels.add(newEmail)
        If Courriels.listCount = 1 Then Courriels.ItemFont(n) = New Font(Courriels.ItemFont(n), FontStyle.Bold)
        Courriels.draw = True : Courriels.draw = False
        formModified = True
    End Sub

    Private Sub modifEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifEmail.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.acceptedChars = "!§#§|§$§%§?§&§*§-§@§.§_§=§+§£§¢§¤§¬§¦§²§³§¼§½§¾§^§¨§¸§`§'§«§»§°§~§¯§´"
        myInputBoxPlus.allLower = True
        myInputBoxPlus.onlyAlphabet = True
        myInputBoxPlus.refuseAccents = True
        Dim newEmail As String = Courriels.ItemText(Courriels.selected)
        Dim emailValidation As EmailValidator.ValidationLevels = EmailValidator.ValidationLevels.Valid
        Do
            newEmail = myInputBoxPlus("Veuillez entrer l'adresse de courriel", "Adresse de courriel", newEmail)
            If newEmail = "" Then Exit Sub

            emailValidation = EmailValidator.isEmailValid(MailsManager.mainFromEmailAddress, newEmail)
            If emailValidation <> EmailValidator.ValidationLevels.Valid Then
                Dim message As String = String.Empty
                Dim domain As String = newEmail.Substring(newEmail.IndexOf("@") + 1)
                Select Case emailValidation
                    Case EmailValidator.ValidationLevels.WrongStructure
                        message = "Veuillez vous assurez que l'adresse de courriel soit valide :" & vbCrLf & "alias@domaine.extension" & vbCrLf & "Exemple : info@cints.net"

                    Case EmailValidator.ValidationLevels.DomainNotExists
                        message = "Le nom de domaine """ & domain & """ n'existe pas ou n'a pas de serveur de courriel"

                    Case EmailValidator.ValidationLevels.NotConfirmedByDomain
                        message = "L'adresse a été rejeté par le nom de domaine """ & domain & """"
                End Select

                MessageBox.Show(message, "Courriel invalide")
            End If
        Loop While emailValidation <> EmailValidator.ValidationLevels.Valid

        Courriels.ItemText(Courriels.selected) = newEmail
        Courriels.draw = True : Courriels.draw = False
        formModified = True
    End Sub

    Private Sub delEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelEmail.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette adresse de couriel ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim found As Boolean = False
        Courriels.remove(Courriels.selected)
        Dim i As Short
        For i = 0 To Courriels.maximum
            If Courriels.ItemFont(i).Bold = True Then found = True
        Next i
        If found = False And Courriels.listCount > 0 Then Courriels.ItemFont(Courriels.findFirstItem) = New Font(Courriels.ItemFont(Courriels.findFirstItem), FontStyle.Bold)
        Courriels.draw = True : Courriels.draw = False

        DelEmail.Enabled = False
        ModifEmail.Enabled = False
        formModified = True
    End Sub

    Private Sub courriels_SelectedChange() Handles Courriels.selectedChange
        If Courriels.selected <> -1 And allowModification = True Then
            DelEmail.Enabled = True
            ModifEmail.Enabled = True
        Else
            DelEmail.Enabled = False
            ModifEmail.Enabled = False
        End If
    End Sub

    Private Sub courriels_DblClick(ByVal sender As Object, ByVal e As CI.Controls.List.DblClickEventArgs) Handles Courriels.dblClick
        If e.selectedItem < 0 Then Exit Sub
        If allowModification = False Then Exit Sub

        Courriels.all(CI.Controls.List.PType.FontBold1, False)
        Courriels.ItemFont(Courriels.selected) = New Font(Courriels.ItemFont(Courriels.selected), FontStyle.Bold)
        Courriels.sorted = True
        Courriels.draw = True : Courriels.draw = False
        formModified = True
    End Sub

    Private Sub addTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTel.Click
        Dim myInputBoxPlus As New InputBoxPlus(True, , "TelePhoneTitles.TelephoneTitle")
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ":"
        Dim newTitle As String = myInputBoxPlus("Veuillez entrer le titre du numéro de téléphone", "Titre")
        If newTitle = "" Then Exit Sub

        myInputBoxPlus = New InputBoxPlus()
        myInputBoxPlus.acceptAlpha = False
        myInputBoxPlus.acceptedChars = "-"
        myInputBoxPlus.maxLength = 14
        myInputBoxPlus.refusedChars = ":"

        Dim newTel As String = myInputBoxPlus("Veuillez entrer le numéro de téléphone", "Numéro de téléphone")
        If newTel = "" Then Exit Sub

        DBHelper.addItemToADBList("TelephoneTitles", "TelephoneTitle", newTitle, "NoTelephoneTitle")

        Dim n As Integer = Telephones.Items.Add(newTitle & ":" & newTel)
        Telephones.SelectedIndex = n
        formModified = True
    End Sub

    Private Sub telephones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Telephones.SelectedIndexChanged
        If Telephones.SelectedIndex <> -1 And allowModification = True Then
            ModifTel.Enabled = True
            DelTel.Enabled = True
        Else
            ModifTel.Enabled = False
            DelTel.Enabled = False
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
        myInputBoxPlus.acceptedChars = "-"
        myInputBoxPlus.maxLength = 14
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
        ModifTel.Enabled = False
        DelTel.Enabled = False
        formModified = True
    End Sub

    Private Sub selectCompte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectCompte.Click
        Dim myRecherche As New clientSearch()
        myRecherche.Visible = False
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()
    End Sub

    Private Sub selectKeyPeople_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectKeyPeople.Click
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.Visible = False
        myKeyPeople.selected = True
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            KeyPeopleRef.Text = kpChosen.kpFullName & " (" & kpChosen.noKP & ")"
            setFormModified = True
        End If
    End Sub

    Private Sub submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles submit.Click
        saving()

        Me.Close()
    End Sub

    Private Sub saving()
        Dim myContact() As String
        ReDim myContact(15)
        Dim i As Short
        myContact(0) = Nom.Text.Replace("'", "''")
        myContact(1) = Prenom.Text.Replace("'", "''")
        myContact(2) = Surnom.Text.Replace("'", "''")
        myContact(3) = Afficher.Text.Replace("'", "''")
        myContact(5) = ""
        For i = 0 To Courriels.maximum
            myContact(5) &= "§" & Courriels.ItemText(i)
            If Courriels.ItemFont(i).Bold Then myContact(4) = Courriels.ItemText(i)
        Next i
        If myContact(5) <> "" Then myContact(5) = myContact(5).Substring(1)
        myContact(5) = myContact(5).Replace("'", "''")
        myContact(6) = SendTextOnly.Checked
        myContact(7) = Adresse.Text.Replace("'", "''")
        myContact(8) = Ville.Text
        myContact(9) = CodePostal1.Text & CodePostal2.Text
        myContact(10) = Pays.Text
        If Telephones.Items.Count = 0 Then
            myContact(11) = ""
        Else
            Dim phones() As String
            ReDim phones(Telephones.Items.Count - 1)
            Telephones.Items.CopyTo(phones, 0)
            myContact(11) = String.Join("§", phones)
        End If
        If Not myContact(11) Is Nothing AndAlso myContact(11) <> "" Then myContact(11) = myContact(11).Substring(1)
        myContact(12) = URL.Text
        If CompteRef.Text.IndexOf("(") > 0 Then
            Dim sClient() As String = CompteRef.Text.Split(New Char() {"("})
            myContact(13) = sClient(1).Substring(0, sClient(1).Length - 1)
        Else
            myContact(13) = "null"
        End If
        If KeyPeopleRef.Text.IndexOf("(") > 0 Then
            Dim skp() As String = KeyPeopleRef.Text.Split(New Char() {"("})
            myContact(14) = skp(1).Substring(0, skp(1).Length - 1)
        Else
            myContact(14) = "null"
        End If

        If Me.Text.StartsWith("A") Then
            DBLinker.getInstance.writeDB("Contacts", "NoContactFolder,Nom,Prenom,Surnom,Afficher,Courriel,Courriels,TextMsgOnly,Adresse,NoVille,CodePostal,NoPays,Telephones,URL,NoClient,NoKP", _
            noContactFolder & ",'" & myContact(0) & "','" & myContact(1) & "','" & myContact(2) & "','" & myContact(3) & "','" & myContact(4) & "','" & myContact(5) & "','" & myContact(6) & "','" & myContact(7) & "'," & DBHelper.addItemToADBList("Villes", "NomVille", myContact(8), "NoVille") & ",'" & myContact(9) & "'," & DBHelper.addItemToADBList("Pays", "Pays", myContact(10), "NoPays") & ",'" & myContact(11) & "','" & myContact(12) & "'," & myContact(13) & "," & myContact(14))
            myMainWin.StatusText = "Ajout d'un contact"
        Else
            DBLinker.getInstance.updateDB("Contacts", "Nom='" & myContact(0) & "',Prenom='" & myContact(1) & "',Surnom='" & myContact(2) & "',Afficher='" & myContact(3) & "',Courriel='" & myContact(4) & "',Courriels='" & myContact(5) & "',TextMsgOnly='" & myContact(6) & "',Adresse='" & myContact(7) & "',NoVille=" & DBHelper.addItemToADBList("Villes", "NomVille", myContact(8), "NoVille") & ",CodePostal='" & myContact(9) & "',NoPays=" & DBHelper.addItemToADBList("Pays", "Pays", myContact(10), "NoPays") & ",Telephones='" & myContact(11) & "',URL='" & myContact(12) & "',NoClient=" & myContact(13) & ",NoKP=" & myContact(14), "NoContact", noContact, False)
            myMainWin.StatusText = "Modification d'un contact"
        End If

        InternalUpdatesManager.getInstance.sendUpdate("Contacts(" & Me.noContactFolder & ")")

        formModified = False
    End Sub

    Private Sub sendTextOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendTextOnly.CheckedChanged
        formModified = True
    End Sub

    Private Sub msgContact_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If allowModification = True Then
            lockSecteur("Contact-" & noContact & ".lock", False)
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then saving()
        End If
    End Sub

    Private Sub msgContact_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If allowModification = True Then
            lockSecteur("Contact-" & noContact & ".lock", False)
            If formModified = True Then saving()
        End If
    End Sub
End Class
