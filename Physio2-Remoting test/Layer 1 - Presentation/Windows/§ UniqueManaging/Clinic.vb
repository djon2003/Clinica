Friend Class Clinic
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin
        Me.Save.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.payBills.Image = DrawingManager.getInstance().getImage("paiement16.gif")
        Me.modifBills.Image = DrawingManager.getInstance().getImage("viewBills16.jpg")
        Me.selectLogo.Image = DrawingManager.getInstance().getImage("selection16.gif")
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            If Not logoImg Is Nothing Then logoImg.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GenInfoBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Nom As ManagedText
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents adresse As ManagedText
    Public WithEvents ville As Clinica.ManagedCombo
    Public WithEvents codepostal2 As ManagedText
    Public WithEvents codepostal1 As ManagedText
    Public WithEvents _Label2_2 As System.Windows.Forms.Label
    Public WithEvents tel1 As ManagedText
    Public WithEvents tel2 As ManagedText
    Public WithEvents tel3 As ManagedText
    Public WithEvents _Label2_0 As System.Windows.Forms.Label
    Public WithEvents _Label2_1 As System.Windows.Forms.Label
    Public WithEvents label10 As System.Windows.Forms.Label
    Public WithEvents label11 As System.Windows.Forms.Label
    Friend WithEvents Save As System.Windows.Forms.Button
    Public WithEvents fax1 As ManagedText
    Public WithEvents fax2 As ManagedText
    Public WithEvents fax3 As ManagedText
    Friend WithEvents NoTaxe2 As ManagedText
    Friend WithEvents NoTaxe1 As ManagedText
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents NoEtablissement As ManagedText
    Friend WithEvents noDAS1 As ManagedText
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents noNEQ As ManagedText
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents courriel As ManagedText
    Friend WithEvents modifHoraire As System.Windows.Forms.Button
    Friend WithEvents payBills As System.Windows.Forms.Button
    Friend WithEvents modifBills As System.Windows.Forms.Button
    Friend WithEvents noDAS2 As ManagedText
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents logo As System.Windows.Forms.PictureBox
    Friend WithEvents selectLogo As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents url As ManagedText
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.GenInfoBox = New System.Windows.Forms.GroupBox
        Me.logo = New System.Windows.Forms.PictureBox
        Me.selectLogo = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me._Label2_0 = New System.Windows.Forms.Label
        Me._Label2_1 = New System.Windows.Forms.Label
        Me._Label2_2 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Save = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.payBills = New System.Windows.Forms.Button
        Me.modifBills = New System.Windows.Forms.Button
        Me.modifHoraire = New System.Windows.Forms.Button
        Me.url = New ManagedText
        Me.courriel = New ManagedText
        Me.noNEQ = New ManagedText
        Me.noDAS2 = New ManagedText
        Me.noDAS1 = New ManagedText
        Me.fax1 = New ManagedText
        Me.fax2 = New ManagedText
        Me.fax3 = New ManagedText
        Me.tel1 = New ManagedText
        Me.tel2 = New ManagedText
        Me.tel3 = New ManagedText
        Me.codepostal2 = New ManagedText
        Me.codepostal1 = New ManagedText
        Me.adresse = New ManagedText
        Me.ville = New Clinica.ManagedCombo
        Me.NoTaxe2 = New ManagedText
        Me.NoTaxe1 = New ManagedText
        Me.NoEtablissement = New ManagedText
        Me.Nom = New ManagedText
        Me.GenInfoBox.SuspendLayout()
        CType(Me.logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GenInfoBox
        '
        Me.GenInfoBox.Controls.Add(Me.logo)
        Me.GenInfoBox.Controls.Add(Me.url)
        Me.GenInfoBox.Controls.Add(Me.selectLogo)
        Me.GenInfoBox.Controls.Add(Me.courriel)
        Me.GenInfoBox.Controls.Add(Me.noNEQ)
        Me.GenInfoBox.Controls.Add(Me.Label15)
        Me.GenInfoBox.Controls.Add(Me.noDAS2)
        Me.GenInfoBox.Controls.Add(Me.noDAS1)
        Me.GenInfoBox.Controls.Add(Me.Label16)
        Me.GenInfoBox.Controls.Add(Me.Label14)
        Me.GenInfoBox.Controls.Add(Me.Label13)
        Me.GenInfoBox.Controls.Add(Me.Label12)
        Me.GenInfoBox.Controls.Add(Me.fax1)
        Me.GenInfoBox.Controls.Add(Me.fax2)
        Me.GenInfoBox.Controls.Add(Me.fax3)
        Me.GenInfoBox.Controls.Add(Me.label10)
        Me.GenInfoBox.Controls.Add(Me.label11)
        Me.GenInfoBox.Controls.Add(Me.tel1)
        Me.GenInfoBox.Controls.Add(Me.tel2)
        Me.GenInfoBox.Controls.Add(Me.tel3)
        Me.GenInfoBox.Controls.Add(Me._Label2_0)
        Me.GenInfoBox.Controls.Add(Me._Label2_1)
        Me.GenInfoBox.Controls.Add(Me.codepostal2)
        Me.GenInfoBox.Controls.Add(Me.codepostal1)
        Me.GenInfoBox.Controls.Add(Me._Label2_2)
        Me.GenInfoBox.Controls.Add(Me.adresse)
        Me.GenInfoBox.Controls.Add(Me.ville)
        Me.GenInfoBox.Controls.Add(Me.NoTaxe2)
        Me.GenInfoBox.Controls.Add(Me.Label17)
        Me.GenInfoBox.Controls.Add(Me.Label9)
        Me.GenInfoBox.Controls.Add(Me.NoTaxe1)
        Me.GenInfoBox.Controls.Add(Me.Label8)
        Me.GenInfoBox.Controls.Add(Me.NoEtablissement)
        Me.GenInfoBox.Controls.Add(Me.Label7)
        Me.GenInfoBox.Controls.Add(Me.Label6)
        Me.GenInfoBox.Controls.Add(Me.Label5)
        Me.GenInfoBox.Controls.Add(Me.Label4)
        Me.GenInfoBox.Controls.Add(Me.Label3)
        Me.GenInfoBox.Controls.Add(Me.Label2)
        Me.GenInfoBox.Controls.Add(Me.Nom)
        Me.GenInfoBox.Controls.Add(Me.Label1)
        Me.GenInfoBox.Location = New System.Drawing.Point(8, 8)
        Me.GenInfoBox.Name = "GenInfoBox"
        Me.GenInfoBox.Size = New System.Drawing.Size(296, 436)
        Me.GenInfoBox.TabIndex = 0
        Me.GenInfoBox.TabStop = False
        Me.GenInfoBox.Text = "Informations de base"
        '
        'logo
        '
        Me.logo.Location = New System.Drawing.Point(168, 354)
        Me.logo.Name = "logo"
        Me.logo.Size = New System.Drawing.Size(120, 76)
        Me.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.logo.TabIndex = 100
        Me.logo.TabStop = False
        '
        'selectLogo
        '
        Me.selectLogo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectLogo.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectLogo.Location = New System.Drawing.Point(142, 354)
        Me.selectLogo.Name = "selectLogo"
        Me.selectLogo.Size = New System.Drawing.Size(24, 24)
        Me.selectLogo.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.selectLogo, "Sélectionner le logo pour le publipostage")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 258)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(45, 13)
        Me.Label15.TabIndex = 99
        Me.Label15.Text = "N.E.Q. :"
        Me.ToolTip1.SetToolTip(Me.Label15, "Numéro d'établissement du Québec")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 232)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(131, 13)
        Me.Label16.TabIndex = 99
        Me.Label16.Text = "Numéro D.A.S.  (Fédéral) :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(8, 208)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(139, 13)
        Me.Label14.TabIndex = 99
        Me.Label14.Text = "Numéro D.A.S. (Provincial) :"
        Me.ToolTip1.SetToolTip(Me.Label14, "Numéro de déduction à la source")
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 184)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(69, 13)
        Me.Label13.TabIndex = 99
        Me.Label13.Text = "Site internet :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 159)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 13)
        Me.Label12.TabIndex = 99
        Me.Label12.Text = "Courriel :"
        '
        'Label10
        '
        Me.label10.AutoSize = True
        Me.label10.BackColor = System.Drawing.SystemColors.Control
        Me.label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label10.Location = New System.Drawing.Point(177, 137)
        Me.label10.Name = "Label10"
        Me.label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label10.Size = New System.Drawing.Size(11, 14)
        Me.label10.TabIndex = 0
        Me.label10.Text = "-"
        '
        'Label11
        '
        Me.label11.AutoSize = True
        Me.label11.BackColor = System.Drawing.SystemColors.Control
        Me.label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label11.Location = New System.Drawing.Point(137, 137)
        Me.label11.Name = "Label11"
        Me.label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label11.Size = New System.Drawing.Size(11, 14)
        Me.label11.TabIndex = 0
        Me.label11.Text = "-"
        '
        '_Label2_0
        '
        Me._Label2_0.AutoSize = True
        Me._Label2_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label2_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_0.Location = New System.Drawing.Point(177, 113)
        Me._Label2_0.Name = "_Label2_0"
        Me._Label2_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_0.Size = New System.Drawing.Size(11, 14)
        Me._Label2_0.TabIndex = 0
        Me._Label2_0.Text = "-"
        '
        '_Label2_1
        '
        Me._Label2_1.AutoSize = True
        Me._Label2_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label2_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_1.Location = New System.Drawing.Point(137, 113)
        Me._Label2_1.Name = "_Label2_1"
        Me._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_1.Size = New System.Drawing.Size(11, 14)
        Me._Label2_1.TabIndex = 0
        Me._Label2_1.Text = "-"
        '
        '_Label2_2
        '
        Me._Label2_2.AutoSize = True
        Me._Label2_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label2_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_2.Location = New System.Drawing.Point(137, 89)
        Me._Label2_2.Name = "_Label2_2"
        Me._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_2.Size = New System.Drawing.Size(11, 14)
        Me._Label2_2.TabIndex = 0
        Me._Label2_2.Text = "-"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(8, 360)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(135, 13)
        Me.Label17.TabIndex = 99
        Me.Label17.Text = "Logo pour le publipostage :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 330)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(147, 13)
        Me.Label9.TabIndex = 99
        Me.Label9.Text = "Numéro de taxe 2 (Fédérale) :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 306)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(158, 13)
        Me.Label8.TabIndex = 99
        Me.Label8.Text = "Numéro de taxe 1 (Provinciale) :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 282)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(125, 13)
        Me.Label7.TabIndex = 99
        Me.Label7.Text = "Numéro d'établissement :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 99
        Me.Label6.Text = "Télécopieur :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 99
        Me.Label5.Text = "Code postal :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 99
        Me.Label4.Text = "Téléphone :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 99
        Me.Label3.Text = "Ville :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 99
        Me.Label2.Text = "Adresse :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "Nom :"
        '
        'Save
        '
        Me.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Save.Location = New System.Drawing.Point(280, 465)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(24, 24)
        Me.Save.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.Save, "Enregistrer les informations de la clinique")
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'payBills
        '
        Me.payBills.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.payBills.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.payBills.Location = New System.Drawing.Point(250, 465)
        Me.payBills.Name = "payBills"
        Me.payBills.Size = New System.Drawing.Size(24, 24)
        Me.payBills.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.payBills, "Effectuer les paiements")
        '
        'modifBills
        '
        Me.modifBills.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifBills.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifBills.Location = New System.Drawing.Point(220, 465)
        Me.modifBills.Name = "modifBills"
        Me.modifBills.Size = New System.Drawing.Size(24, 24)
        Me.modifBills.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.modifBills, "Voir les factures")
        '
        'modifHoraire
        '
        Me.modifHoraire.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifHoraire.Location = New System.Drawing.Point(8, 465)
        Me.modifHoraire.Name = "modifHoraire"
        Me.modifHoraire.Size = New System.Drawing.Size(206, 24)
        Me.modifHoraire.TabIndex = 0
        Me.modifHoraire.TabStop = False
        Me.modifHoraire.Text = "Modification de l'horaire"
        '
        'url
        '
        Me.url.acceptAlpha = True
        Me.url.acceptedChars = ""
        Me.url.acceptNumeric = False
        Me.url.AcceptsReturn = True
        Me.url.allCapital = False
        Me.url.allLower = False
        Me.url.BackColor = System.Drawing.SystemColors.Window
        Me.url.blockOnMaximum = False
        Me.url.blockOnMinimum = False
        Me.url.cb_AcceptNegative = False
        Me.url.currencyBox = False
        Me.url.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.url.firstLetterCapital = False
        Me.url.firstLettersCapital = False
        Me.url.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.url.ForeColor = System.Drawing.SystemColors.WindowText
        Me.url.Location = New System.Drawing.Point(104, 184)
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
        Me.url.Size = New System.Drawing.Size(184, 20)
        Me.url.TabIndex = 13
        Me.url.trimText = False
        '
        'courriel
        '
        Me.courriel.acceptAlpha = True
        Me.courriel.acceptedChars = ".§@"
        Me.courriel.acceptNumeric = True
        Me.courriel.AcceptsReturn = True
        Me.courriel.allCapital = False
        Me.courriel.allLower = True
        Me.courriel.BackColor = System.Drawing.SystemColors.Window
        Me.courriel.blockOnMaximum = False
        Me.courriel.blockOnMinimum = False
        Me.courriel.cb_AcceptNegative = False
        Me.courriel.currencyBox = False
        Me.courriel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.courriel.firstLetterCapital = False
        Me.courriel.firstLettersCapital = False
        Me.courriel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.courriel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.courriel.Location = New System.Drawing.Point(104, 160)
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
        Me.courriel.Size = New System.Drawing.Size(184, 20)
        Me.courriel.TabIndex = 12
        Me.courriel.trimText = False
        '
        'noNEQ
        '
        Me.noNEQ.acceptAlpha = True
        Me.noNEQ.acceptedChars = "-"
        Me.noNEQ.acceptNumeric = True
        Me.noNEQ.allCapital = True
        Me.noNEQ.allLower = False
        Me.noNEQ.blockOnMaximum = False
        Me.noNEQ.blockOnMinimum = False
        Me.noNEQ.cb_AcceptNegative = False
        Me.noNEQ.currencyBox = False
        Me.noNEQ.firstLetterCapital = False
        Me.noNEQ.firstLettersCapital = False
        Me.noNEQ.Location = New System.Drawing.Point(168, 258)
        Me.noNEQ.manageText = True
        Me.noNEQ.matchExp = ""
        Me.noNEQ.maximum = 0
        Me.noNEQ.minimum = 0
        Me.noNEQ.Name = "noNEQ"
        Me.noNEQ.nbDecimals = CType(-1, Short)
        Me.noNEQ.onlyAlphabet = True
        Me.noNEQ.refuseAccents = True
        Me.noNEQ.refusedChars = "'"
        Me.noNEQ.showInternalContextMenu = True
        Me.noNEQ.Size = New System.Drawing.Size(120, 20)
        Me.noNEQ.TabIndex = 16
        Me.noNEQ.trimText = False
        '
        'noDAS2
        '
        Me.noDAS2.acceptAlpha = True
        Me.noDAS2.acceptedChars = "-"
        Me.noDAS2.acceptNumeric = True
        Me.noDAS2.allCapital = True
        Me.noDAS2.allLower = False
        Me.noDAS2.blockOnMaximum = False
        Me.noDAS2.blockOnMinimum = False
        Me.noDAS2.cb_AcceptNegative = False
        Me.noDAS2.currencyBox = False
        Me.noDAS2.firstLetterCapital = False
        Me.noDAS2.firstLettersCapital = False
        Me.noDAS2.Location = New System.Drawing.Point(168, 232)
        Me.noDAS2.manageText = True
        Me.noDAS2.matchExp = ""
        Me.noDAS2.maximum = 0
        Me.noDAS2.minimum = 0
        Me.noDAS2.Name = "noDAS2"
        Me.noDAS2.nbDecimals = CType(-1, Short)
        Me.noDAS2.onlyAlphabet = True
        Me.noDAS2.refuseAccents = True
        Me.noDAS2.refusedChars = "'"
        Me.noDAS2.showInternalContextMenu = True
        Me.noDAS2.Size = New System.Drawing.Size(120, 20)
        Me.noDAS2.TabIndex = 15
        Me.noDAS2.trimText = False
        '
        'noDAS1
        '
        Me.noDAS1.acceptAlpha = True
        Me.noDAS1.acceptedChars = "-"
        Me.noDAS1.acceptNumeric = True
        Me.noDAS1.allCapital = True
        Me.noDAS1.allLower = False
        Me.noDAS1.blockOnMaximum = False
        Me.noDAS1.blockOnMinimum = False
        Me.noDAS1.cb_AcceptNegative = False
        Me.noDAS1.currencyBox = False
        Me.noDAS1.firstLetterCapital = False
        Me.noDAS1.firstLettersCapital = False
        Me.noDAS1.Location = New System.Drawing.Point(168, 208)
        Me.noDAS1.manageText = True
        Me.noDAS1.matchExp = ""
        Me.noDAS1.maximum = 0
        Me.noDAS1.minimum = 0
        Me.noDAS1.Name = "noDAS1"
        Me.noDAS1.nbDecimals = CType(-1, Short)
        Me.noDAS1.onlyAlphabet = True
        Me.noDAS1.refuseAccents = True
        Me.noDAS1.refusedChars = "'"
        Me.noDAS1.showInternalContextMenu = True
        Me.noDAS1.Size = New System.Drawing.Size(120, 20)
        Me.noDAS1.TabIndex = 14
        Me.noDAS1.trimText = False
        '
        'Fax1
        '
        Me.fax1.acceptAlpha = False
        Me.fax1.acceptedChars = ""
        Me.fax1.acceptNumeric = True
        Me.fax1.AcceptsReturn = True
        Me.fax1.allCapital = False
        Me.fax1.allLower = False
        Me.fax1.BackColor = System.Drawing.SystemColors.Window
        Me.fax1.blockOnMaximum = False
        Me.fax1.blockOnMinimum = False
        Me.fax1.cb_AcceptNegative = False
        Me.fax1.currencyBox = False
        Me.fax1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.fax1.firstLetterCapital = False
        Me.fax1.firstLettersCapital = False
        Me.fax1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fax1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fax1.Location = New System.Drawing.Point(104, 136)
        Me.fax1.manageText = True
        Me.fax1.matchExp = ""
        Me.fax1.maximum = 0
        Me.fax1.MaxLength = 3
        Me.fax1.minimum = 0
        Me.fax1.Name = "Fax1"
        Me.fax1.nbDecimals = CType(-1, Short)
        Me.fax1.onlyAlphabet = False
        Me.fax1.refuseAccents = False
        Me.fax1.refusedChars = ""
        Me.fax1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fax1.showInternalContextMenu = True
        Me.fax1.Size = New System.Drawing.Size(33, 20)
        Me.fax1.TabIndex = 9
        Me.fax1.trimText = False
        '
        'Fax2
        '
        Me.fax2.acceptAlpha = False
        Me.fax2.acceptedChars = ""
        Me.fax2.acceptNumeric = True
        Me.fax2.AcceptsReturn = True
        Me.fax2.allCapital = False
        Me.fax2.allLower = False
        Me.fax2.BackColor = System.Drawing.SystemColors.Window
        Me.fax2.blockOnMaximum = False
        Me.fax2.blockOnMinimum = False
        Me.fax2.cb_AcceptNegative = False
        Me.fax2.currencyBox = False
        Me.fax2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.fax2.firstLetterCapital = False
        Me.fax2.firstLettersCapital = False
        Me.fax2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fax2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fax2.Location = New System.Drawing.Point(144, 136)
        Me.fax2.manageText = True
        Me.fax2.matchExp = ""
        Me.fax2.maximum = 0
        Me.fax2.MaxLength = 3
        Me.fax2.minimum = 0
        Me.fax2.Name = "Fax2"
        Me.fax2.nbDecimals = CType(-1, Short)
        Me.fax2.onlyAlphabet = False
        Me.fax2.refuseAccents = False
        Me.fax2.refusedChars = ""
        Me.fax2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fax2.showInternalContextMenu = True
        Me.fax2.Size = New System.Drawing.Size(33, 20)
        Me.fax2.TabIndex = 10
        Me.fax2.trimText = False
        '
        'Fax3
        '
        Me.fax3.acceptAlpha = False
        Me.fax3.acceptedChars = ""
        Me.fax3.acceptNumeric = True
        Me.fax3.AcceptsReturn = True
        Me.fax3.allCapital = False
        Me.fax3.allLower = False
        Me.fax3.BackColor = System.Drawing.SystemColors.Window
        Me.fax3.blockOnMaximum = False
        Me.fax3.blockOnMinimum = False
        Me.fax3.cb_AcceptNegative = False
        Me.fax3.currencyBox = False
        Me.fax3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.fax3.firstLetterCapital = False
        Me.fax3.firstLettersCapital = False
        Me.fax3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fax3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fax3.Location = New System.Drawing.Point(184, 136)
        Me.fax3.manageText = True
        Me.fax3.matchExp = ""
        Me.fax3.maximum = 0
        Me.fax3.MaxLength = 4
        Me.fax3.minimum = 0
        Me.fax3.Name = "Fax3"
        Me.fax3.nbDecimals = CType(-1, Short)
        Me.fax3.onlyAlphabet = False
        Me.fax3.refuseAccents = False
        Me.fax3.refusedChars = ""
        Me.fax3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fax3.showInternalContextMenu = True
        Me.fax3.Size = New System.Drawing.Size(41, 20)
        Me.fax3.TabIndex = 11
        Me.fax3.trimText = False
        '
        'tel1
        '
        Me.tel1.acceptAlpha = False
        Me.tel1.acceptedChars = ""
        Me.tel1.acceptNumeric = True
        Me.tel1.AcceptsReturn = True
        Me.tel1.allCapital = False
        Me.tel1.allLower = False
        Me.tel1.BackColor = System.Drawing.SystemColors.Window
        Me.tel1.blockOnMaximum = False
        Me.tel1.blockOnMinimum = False
        Me.tel1.cb_AcceptNegative = False
        Me.tel1.currencyBox = False
        Me.tel1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tel1.firstLetterCapital = False
        Me.tel1.firstLettersCapital = False
        Me.tel1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tel1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tel1.Location = New System.Drawing.Point(104, 112)
        Me.tel1.manageText = True
        Me.tel1.matchExp = ""
        Me.tel1.maximum = 0
        Me.tel1.MaxLength = 3
        Me.tel1.minimum = 0
        Me.tel1.Name = "tel1"
        Me.tel1.nbDecimals = CType(-1, Short)
        Me.tel1.onlyAlphabet = False
        Me.tel1.refuseAccents = False
        Me.tel1.refusedChars = ""
        Me.tel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tel1.showInternalContextMenu = True
        Me.tel1.Size = New System.Drawing.Size(33, 20)
        Me.tel1.TabIndex = 6
        Me.tel1.trimText = False
        '
        'tel2
        '
        Me.tel2.acceptAlpha = False
        Me.tel2.acceptedChars = ""
        Me.tel2.acceptNumeric = True
        Me.tel2.AcceptsReturn = True
        Me.tel2.allCapital = False
        Me.tel2.allLower = False
        Me.tel2.BackColor = System.Drawing.SystemColors.Window
        Me.tel2.blockOnMaximum = False
        Me.tel2.blockOnMinimum = False
        Me.tel2.cb_AcceptNegative = False
        Me.tel2.currencyBox = False
        Me.tel2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tel2.firstLetterCapital = False
        Me.tel2.firstLettersCapital = False
        Me.tel2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tel2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tel2.Location = New System.Drawing.Point(144, 112)
        Me.tel2.manageText = True
        Me.tel2.matchExp = ""
        Me.tel2.maximum = 0
        Me.tel2.MaxLength = 3
        Me.tel2.minimum = 0
        Me.tel2.Name = "tel2"
        Me.tel2.nbDecimals = CType(-1, Short)
        Me.tel2.onlyAlphabet = False
        Me.tel2.refuseAccents = False
        Me.tel2.refusedChars = ""
        Me.tel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tel2.showInternalContextMenu = True
        Me.tel2.Size = New System.Drawing.Size(33, 20)
        Me.tel2.TabIndex = 7
        Me.tel2.trimText = False
        '
        'tel3
        '
        Me.tel3.acceptAlpha = False
        Me.tel3.acceptedChars = ""
        Me.tel3.acceptNumeric = True
        Me.tel3.AcceptsReturn = True
        Me.tel3.allCapital = False
        Me.tel3.allLower = False
        Me.tel3.BackColor = System.Drawing.SystemColors.Window
        Me.tel3.blockOnMaximum = False
        Me.tel3.blockOnMinimum = False
        Me.tel3.cb_AcceptNegative = False
        Me.tel3.currencyBox = False
        Me.tel3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tel3.firstLetterCapital = False
        Me.tel3.firstLettersCapital = False
        Me.tel3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tel3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tel3.Location = New System.Drawing.Point(184, 112)
        Me.tel3.manageText = True
        Me.tel3.matchExp = ""
        Me.tel3.maximum = 0
        Me.tel3.MaxLength = 4
        Me.tel3.minimum = 0
        Me.tel3.Name = "tel3"
        Me.tel3.nbDecimals = CType(-1, Short)
        Me.tel3.onlyAlphabet = False
        Me.tel3.refuseAccents = False
        Me.tel3.refusedChars = ""
        Me.tel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tel3.showInternalContextMenu = True
        Me.tel3.Size = New System.Drawing.Size(41, 20)
        Me.tel3.TabIndex = 8
        Me.tel3.trimText = False
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
        Me.codepostal2.cb_AcceptNegative = False
        Me.codepostal2.currencyBox = False
        Me.codepostal2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.codepostal2.firstLetterCapital = False
        Me.codepostal2.firstLettersCapital = False
        Me.codepostal2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codepostal2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.codepostal2.Location = New System.Drawing.Point(144, 88)
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
        Me.codepostal1.cb_AcceptNegative = False
        Me.codepostal1.currencyBox = False
        Me.codepostal1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.codepostal1.firstLetterCapital = False
        Me.codepostal1.firstLettersCapital = False
        Me.codepostal1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codepostal1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.codepostal1.Location = New System.Drawing.Point(104, 88)
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
        'Adresse
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
        Me.adresse.cb_AcceptNegative = False
        Me.adresse.currencyBox = False
        Me.adresse.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.adresse.firstLetterCapital = True
        Me.adresse.firstLettersCapital = True
        Me.adresse.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adresse.ForeColor = System.Drawing.SystemColors.WindowText
        Me.adresse.Location = New System.Drawing.Point(104, 40)
        Me.adresse.manageText = True
        Me.adresse.matchExp = ""
        Me.adresse.maximum = 0
        Me.adresse.MaxLength = 0
        Me.adresse.minimum = 0
        Me.adresse.Name = "Adresse"
        Me.adresse.nbDecimals = CType(-1, Short)
        Me.adresse.onlyAlphabet = False
        Me.adresse.refuseAccents = False
        Me.adresse.refusedChars = ""
        Me.adresse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adresse.showInternalContextMenu = True
        Me.adresse.Size = New System.Drawing.Size(184, 20)
        Me.adresse.TabIndex = 2
        Me.adresse.trimText = False
        '
        'Ville
        '
        Me.ville.acceptAlpha = True
        Me.ville.acceptedChars = " §'§-§.§/§|§\§(§)"
        Me.ville.acceptNumeric = False
        Me.ville.allCapital = False
        Me.ville.allLower = False
        Me.ville.autoComplete = True
        Me.ville.autoSizeDropDown = True
        Me.ville.BackColor = System.Drawing.Color.White
        Me.ville.blockOnMaximum = False
        Me.ville.blockOnMinimum = False
        Me.ville.cb_AcceptNegative = False
        Me.ville.currencyBox = False
        Me.ville.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ville.dbField = "Villes.NomVille"
        Me.ville.doComboDelete = True
        Me.ville.firstLetterCapital = True
        Me.ville.firstLettersCapital = True
        Me.ville.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ville.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ville.itemsToolTipDuration = 10000
        Me.ville.Location = New System.Drawing.Point(104, 64)
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
        Me.ville.Size = New System.Drawing.Size(184, 22)
        Me.ville.TabIndex = 2
        Me.ville.trimText = False
        '
        'NoTaxe2
        '
        Me.NoTaxe2.acceptAlpha = True
        Me.NoTaxe2.acceptedChars = "-"
        Me.NoTaxe2.acceptNumeric = True
        Me.NoTaxe2.allCapital = False
        Me.NoTaxe2.allLower = False
        Me.NoTaxe2.blockOnMaximum = False
        Me.NoTaxe2.blockOnMinimum = False
        Me.NoTaxe2.cb_AcceptNegative = False
        Me.NoTaxe2.currencyBox = False
        Me.NoTaxe2.firstLetterCapital = True
        Me.NoTaxe2.firstLettersCapital = False
        Me.NoTaxe2.Location = New System.Drawing.Point(168, 330)
        Me.NoTaxe2.manageText = True
        Me.NoTaxe2.matchExp = ""
        Me.NoTaxe2.maximum = 0
        Me.NoTaxe2.minimum = 0
        Me.NoTaxe2.Name = "NoTaxe2"
        Me.NoTaxe2.nbDecimals = CType(-1, Short)
        Me.NoTaxe2.onlyAlphabet = True
        Me.NoTaxe2.refuseAccents = True
        Me.NoTaxe2.refusedChars = "'"
        Me.NoTaxe2.showInternalContextMenu = True
        Me.NoTaxe2.Size = New System.Drawing.Size(120, 20)
        Me.NoTaxe2.TabIndex = 19
        Me.NoTaxe2.trimText = False
        '
        'NoTaxe1
        '
        Me.NoTaxe1.acceptAlpha = True
        Me.NoTaxe1.acceptedChars = "-"
        Me.NoTaxe1.acceptNumeric = True
        Me.NoTaxe1.allCapital = False
        Me.NoTaxe1.allLower = False
        Me.NoTaxe1.blockOnMaximum = False
        Me.NoTaxe1.blockOnMinimum = False
        Me.NoTaxe1.cb_AcceptNegative = False
        Me.NoTaxe1.currencyBox = False
        Me.NoTaxe1.firstLetterCapital = True
        Me.NoTaxe1.firstLettersCapital = False
        Me.NoTaxe1.Location = New System.Drawing.Point(168, 306)
        Me.NoTaxe1.manageText = True
        Me.NoTaxe1.matchExp = ""
        Me.NoTaxe1.maximum = 0
        Me.NoTaxe1.minimum = 0
        Me.NoTaxe1.Name = "NoTaxe1"
        Me.NoTaxe1.nbDecimals = CType(-1, Short)
        Me.NoTaxe1.onlyAlphabet = True
        Me.NoTaxe1.refuseAccents = True
        Me.NoTaxe1.refusedChars = "'"
        Me.NoTaxe1.showInternalContextMenu = True
        Me.NoTaxe1.Size = New System.Drawing.Size(120, 20)
        Me.NoTaxe1.TabIndex = 18
        Me.NoTaxe1.trimText = False
        '
        'NoEtablissement
        '
        Me.NoEtablissement.acceptAlpha = True
        Me.NoEtablissement.acceptedChars = "-"
        Me.NoEtablissement.acceptNumeric = True
        Me.NoEtablissement.allCapital = False
        Me.NoEtablissement.allLower = False
        Me.NoEtablissement.blockOnMaximum = False
        Me.NoEtablissement.blockOnMinimum = False
        Me.NoEtablissement.cb_AcceptNegative = False
        Me.NoEtablissement.currencyBox = False
        Me.NoEtablissement.firstLetterCapital = True
        Me.NoEtablissement.firstLettersCapital = False
        Me.NoEtablissement.Location = New System.Drawing.Point(168, 282)
        Me.NoEtablissement.manageText = True
        Me.NoEtablissement.matchExp = ""
        Me.NoEtablissement.maximum = 0
        Me.NoEtablissement.minimum = 0
        Me.NoEtablissement.Name = "NoEtablissement"
        Me.NoEtablissement.nbDecimals = CType(-1, Short)
        Me.NoEtablissement.onlyAlphabet = True
        Me.NoEtablissement.refuseAccents = True
        Me.NoEtablissement.refusedChars = "'"
        Me.NoEtablissement.showInternalContextMenu = True
        Me.NoEtablissement.Size = New System.Drawing.Size(120, 20)
        Me.NoEtablissement.TabIndex = 17
        Me.NoEtablissement.trimText = False
        '
        'Nom
        '
        Me.Nom.acceptAlpha = True
        Me.Nom.acceptedChars = ""
        Me.Nom.acceptNumeric = True
        Me.Nom.allCapital = False
        Me.Nom.allLower = False
        Me.Nom.blockOnMaximum = False
        Me.Nom.blockOnMinimum = False
        Me.Nom.cb_AcceptNegative = False
        Me.Nom.currencyBox = False
        Me.Nom.firstLetterCapital = True
        Me.Nom.firstLettersCapital = False
        Me.Nom.Location = New System.Drawing.Point(104, 16)
        Me.Nom.manageText = True
        Me.Nom.matchExp = ""
        Me.Nom.maximum = 0
        Me.Nom.minimum = 0
        Me.Nom.Name = "Nom"
        Me.Nom.nbDecimals = CType(-1, Short)
        Me.Nom.onlyAlphabet = False
        Me.Nom.refuseAccents = False
        Me.Nom.refusedChars = ""
        Me.Nom.showInternalContextMenu = True
        Me.Nom.Size = New System.Drawing.Size(184, 20)
        Me.Nom.TabIndex = 1
        Me.Nom.trimText = False
        '
        'Clinique
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(313, 501)
        Me.Controls.Add(Me.modifHoraire)
        Me.Controls.Add(Me.modifBills)
        Me.Controls.Add(Me.payBills)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.GenInfoBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Clinique"
        Me.ShowInTaskbar = False
        Me.Text = "Gestion de la clinique"
        Me.GenInfoBox.ResumeLayout(False)
        Me.GenInfoBox.PerformLayout()
        CType(Me.logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private formModified As Boolean = False
    Private updating As Boolean = False
    Private myNoClinique As Integer = 0
    Private logoURL As String = ""
    Private logoImg As Bitmap

    Private Sub lockItems(ByVal trueFalse As Boolean)
        Nom.ReadOnly = trueFalse
        adresse.ReadOnly = trueFalse
        ville.ReadOnly = trueFalse
        codepostal1.ReadOnly = trueFalse
        codepostal2.ReadOnly = trueFalse
        fax1.ReadOnly = trueFalse
        fax2.ReadOnly = trueFalse
        fax3.ReadOnly = trueFalse
        NoTaxe1.ReadOnly = trueFalse
        NoTaxe2.ReadOnly = trueFalse
        tel1.ReadOnly = trueFalse
        tel2.ReadOnly = trueFalse
        tel3.ReadOnly = trueFalse
        NoEtablissement.ReadOnly = trueFalse
        noDAS1.ReadOnly = trueFalse
        noDAS2.ReadOnly = trueFalse
        noNEQ.ReadOnly = trueFalse
        courriel.ReadOnly = trueFalse
        url.ReadOnly = trueFalse
        Save.Enabled = Not trueFalse
    End Sub

    Public Sub loading()
        If myMainWin Is Nothing Then
            modifHoraire.Enabled = False
            Payment.Enabled = False
            payBills.Enabled = False
            modifBills.Enabled = False
        End If

        ville.Items.Clear()
        Dim villes() As String = DBLinker.getInstance.readOneDBField("Villes", "NomVille", , True)
        If Not villes Is Nothing AndAlso villes.Length <> 0 Then ville.Items.AddRange(villes)

        Dim bic(,) As String = DBLinker.getInstance.readDB("InfoClinique LEFT OUTER JOIN Villes ON Villes.NoVille = InfoClinique.NoVille", "Nom,Adresse,NomVille,CodePostal,Telephone,Fax,NoEtablissement,NoTaxe1,NoTaxe2,Courriel,URL,NoDAS,NEQ,NoClinique,NoDAS2, LogoURL", "WHERE NoClinique=" & currentClinic)
        If bic Is Nothing OrElse bic.Length = 0 Then Exit Sub

        updating = True
        Nom.Text = bic(0, 0)
        adresse.Text = bic(1, 0)
        ville.Text = bic(2, 0)
        Try
            codepostal1.Text = bic(3, 0).Substring(0, 3)
        Catch
        End Try
        Try
            codepostal2.Text = bic(3, 0).Substring(3, 3)
        Catch
        End Try
        If bic(4, 0).Trim <> String.Empty Then
            tel1.Text = bic(4, 0).Split(New Char() {"-"})(0)
            tel2.Text = bic(4, 0).Split(New Char() {"-"})(1)
            tel3.Text = bic(4, 0).Split(New Char() {"-"})(2)
        End If
        If bic(5, 0) <> "" Then
            fax1.Text = bic(5, 0).Split(New Char() {"-"})(0)
            fax2.Text = bic(5, 0).Split(New Char() {"-"})(1)
            fax3.Text = bic(5, 0).Split(New Char() {"-"})(2)
        End If
        NoEtablissement.Text = bic(6, 0)
        NoTaxe1.Text = bic(7, 0)
        NoTaxe2.Text = bic(8, 0)
        courriel.Text = bic(9, 0)
        url.Text = bic(10, 0)
        noDAS1.Text = bic(11, 0)
        noDAS2.Text = bic(14, 0)
        noNEQ.Text = bic(12, 0)
        myNoClinique = bic(13, 0)
        logoURL = bic(15, 0).Replace("file:///", "")
        If logoURL <> "" AndAlso IO.File.Exists(logoURL) Then
            logoImg = New Bitmap(logoURL)
            logo.Image = logoImg
        End If

        payBills.Enabled = Bill.isPaymentsToDoByClinic(myNoClinique)

        formModified = False
    End Sub

    Private Function saving() As String
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

        If updating = False Then
            DBLinker.getInstance.writeDB("InfoClinique", "Nom,Adresse,NoVille,CodePostal,Telephone,Fax,NoEtablissement,NoTaxe1,NoTaxe2,Courriel,URL,NoDAS,NEQ,NoDAS2, LogoURL", "'" & Nom.Text.Replace("'", "''") & "','" & adresse.Text.Replace("'", "''") & "'," & DBHelper.addItemToADBList("Villes", "NomVille", ville.Text, "NoVille") & ",'" & codepostal1.Text & codepostal2.Text & "','" & tel1.Text & "-" & tel2.Text & "-" & tel3.Text & "','" & fax1.Text & "-" & fax2.Text & "-" & fax3.Text & "','" & NoEtablissement.Text & "','" & NoTaxe1.Text & "','" & NoTaxe2.Text & "','" & courriel.Text.Replace("'", "''") & "','" & url.Text.Replace("'", "''") & "','" & noDAS1.Text & "','" & noNEQ.Text & "','" & noDAS2.Text.Replace("'", "''") & "','" & logoURL.Replace("'", "''") & "'")
        Else
            DBLinker.getInstance.updateDB("InfoClinique", "Nom='" & Nom.Text.Replace("'", "''") & "',Adresse='" & adresse.Text.Replace("'", "''") & "',NoVille=" & DBHelper.addItemToADBList("Villes", "NomVille", ville.Text, "NoVille") & ",CodePostal='" & codepostal1.Text & codepostal2.Text & "',Telephone='" & tel1.Text & "-" & tel2.Text & "-" & tel3.Text & "',Fax='" & fax1.Text & "-" & fax2.Text & "-" & fax3.Text & "',NoEtablissement='" & NoEtablissement.Text & "',NoTaxe1='" & NoTaxe1.Text & "',NoTaxe2='" & NoTaxe2.Text & "',Courriel='" & courriel.Text.Replace("'", "''") & "',URL='" & url.Text.Replace("'", "''") & "',NoDAS='" & noDAS1.Text & "',NEQ='" & noNEQ.Text & "',NoDAS2='" & noDAS2.Text.Replace("'", "''") & "',LogoURL='" & logoURL.Replace("'", "''") & "'", "NoClinique", myNoClinique, False)
        End If

        InternalUpdatesManager.getInstance.sendUpdate("Clinique()")

        If myMainWin IsNot Nothing Then myMainWin.StatusText = "Clinique : Informations enregistrées"
        formModified = False
        Return "DONE"
    End Function

    Private Sub clinique_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If lockSecteur("Clinique.modif", True, "Gestion de la clinique") = False Then lockItems(True)
        loading()
    End Sub

    Private Sub save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        saving()
        Me.Close()
    End Sub

    Private Sub all_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nom.TextChanged, adresse.TextChanged, ville.TextChanged, codepostal1.TextChanged, codepostal2.TextChanged, fax1.TextChanged, fax2.TextChanged, fax3.TextChanged, NoTaxe1.TextChanged, NoTaxe2.TextChanged, tel1.TextChanged, tel2.TextChanged, tel3.TextChanged, NoEtablissement.TextChanged, noDAS1.TextChanged, noNEQ.TextChanged, courriel.TextChanged, url.TextChanged, noDAS2.TextChanged
        formModified = True
    End Sub

    Private Sub clinique_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Nom.Enabled = True Then
            lockSecteur("Clinique.modif", False)
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If saving() = "" Then e.Cancel = True
        End If
    End Sub

    Private Sub clinique_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If Nom.Enabled = True Then
            lockSecteur("Clinique.modif", False)
            If formModified = True Then saving()
        End If
    End Sub

    Private Sub all_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Nom.KeyUp, adresse.KeyUp, ville.KeyUp, codepostal1.KeyUp, codepostal2.KeyUp, fax1.KeyUp, fax2.KeyUp, fax3.KeyUp, NoTaxe1.KeyUp, NoTaxe2.KeyUp, tel1.KeyUp, tel2.KeyUp, tel3.KeyUp, NoEtablissement.KeyUp, noDAS1.KeyUp, noNEQ.KeyUp, courriel.KeyUp, url.KeyUp, noDAS2.KeyUp
        If e.KeyCode = 8 And sender.text = "" Then
            If sender.tabindex = 1 Then Exit Sub
            GenInfoBox.GetNextControl(sender, False).Focus()
        End If
        If e.KeyCode = 13 Then
            Try
                GenInfoBox.GetNextControl(sender, True).Focus()
            Catch
                Save.Focus()
            End Try
            e.Handled = True
        End If
    End Sub

    Private Sub threeCharsBoxes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles codepostal1.TextChanged, codepostal2.TextChanged, tel1.TextChanged, tel2.TextChanged, fax1.TextChanged, fax2.TextChanged
        If CType(sender, ManagedText).Text.Length = 3 Then GenInfoBox.GetNextControl(sender, True).Focus()
    End Sub

    Private Sub fourCharsBoxes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tel3.TextChanged, fax3.TextChanged
        If CType(sender, ManagedText).Text.Length = 4 Then GenInfoBox.GetNextControl(sender, True).Focus()
    End Sub

    Private Sub modifHoraire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifHoraire.Click
        openModifHoraire(0)
    End Sub

    Private Sub modifBills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifBills.Click
        'Droit & Accès
        If currentDroitAcces(82) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Facturation de la clinique." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myModifFacturation As modiffacturation = openUniqueWindow(New modiffacturation(), "Facturation de la clinique")
        myModifFacturation.dedicatedType = FacturationBox.DedicatedType.Clinique
        myModifFacturation.noClinique = myNoClinique
        myModifFacturation.Show()
    End Sub

    Private Sub payBills_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles payBills.Click
        'Droit & Accès
        If currentDroitAcces(80) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Paiements de la clinique." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myPaiement As Payment = openUniqueWindow(New Payment(), "Effectuer le(s) paiement(s) de " & Nom.Text)
        If myPaiement.billsLoaded = False Then myPaiement.loading(myNoClinique, FacturationBox.DedicatedType.Clinique)
        myPaiement.Show()
    End Sub

    Private Sub selectLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectLogo.Click
        Dim myLogo As InternalDBItem = InternalDBManager.getInstance.getImageFromDB()
        If myLogo Is Nothing Then Exit Sub

        Me.logoURL = "file:///" & appPath & bar(appPath) & "DB\" & myLogo.dbItemFile
        logoImg = New Bitmap(Me.logoURL.Replace("file:///", ""))
        logo.Image = logoImg
        formModified = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf clinique_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.fromExternal AndAlso dataReceived.function = "Clinique" Then
            loading()
        End If
    End Sub
End Class
