
Option Strict Off
Option Explicit On
Friend Class viewmodifKeyPeople
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

        'Ajout de la boîte de facturation
        curFactureBox = New FacturationBox(False, False, False, FacturationBox.DedicatedType.KP)
        curFactureBox.locked = True
        '
        'CurFactureBox
        '
        Me.curFactureBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.curFactureBox.BackColor = System.Drawing.SystemColors.Control
        Me.curFactureBox.Location = New System.Drawing.Point(13, 332)
        Me.curFactureBox.montantFacture = 0.0!
        Me.curFactureBox.montantRestant = 0.0!
        Me.curFactureBox.Name = "CurFactureBox"
        Me.curFactureBox.negativePaiement = False
        Me.curFactureBox.noFacture = 0
        Me.curFactureBox.setNom = ""
        Me.curFactureBox.Size = New System.Drawing.Size(536, 130)
        Me.curFactureBox.TabIndex = 0
        Me.curFactureBox.valueA = Nothing
        Me.curFactureBox.valueB = Nothing
        Me._ongletskp_TabPage4.Controls.Add(Me.curFactureBox)

        'Chargement des images
        With DrawingManager.getInstance
            imgModifSave = New ImageList()
            Try
                imgModifSave.Images.Add(.getImage("modifier16.gif"))
                imgModifSave.Images.Add(.getImage("save.jpg"))
                imgModifSave.Images.Add(.getImage("stopmodif16.gif"))
            Catch
            End Try

            Me.submit.Image = imgModifSave.Images(0)
            Me.downTel.Image = .getImage("DownArrow.jpg")
            Me.upTel.Image = .getImage("UpArrow.jpg")
            Me.addTel.Image = .getImage("ajouter16.gif")
            Me.modifTel.Image = imgModifSave.Images(0)
            Me.delTel.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.getReference.Image = .getImage("selection16.gif")
            Me.selectWorkPlace.Image = .getImage("selection16.gif")
            Me.viderComm.Image = .getImage("eraser.jpg")
            Me.importComm.Image = DrawingManager.iconToImage(.getIcon("import16.ico"), New Size(16, 16))
            Me.listeCommunications.icons.Add(.getIcon("import16.ico"))
            Me.delComm.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.modifComm.Image = imgModifSave.Images(1)
            Me.addComm.Image = .getImage("ajouter16.gif")
            Me.selectKeyPeople.Image = .getImage("selection16.gif")
            Me.selectCommDate.Image = .getImage("selection16.gif")
            Me.startStopCommChanging.Image = imgModifSave.Images(0)
            Me.startStopBillChanging.Image = imgModifSave.Images(0)
            Me.filterBills.Image = .getImage("selection16.gif")
            Me.paiements.Image = .getImage("paiement16.gif")
            Me.addAlert.Image = .getImage("alarme16.gif")
            Me.createBill.Image = .getImage("newBill16.jpg")
            Me.Icon = .getIcon("KP16.ico")
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
    Private WithEvents noident As ManagedText
    Private WithEvents adminbutton As System.Windows.Forms.Button
    Private WithEvents submit As System.Windows.Forms.Button
    Private WithEvents ainfo As ManagedText
    Private WithEvents adresse As ManagedText
    Private WithEvents codepostal1 As ManagedText
    Private WithEvents codepostal2 As ManagedText
    Private WithEvents categorie As ManagedCombo
    Private WithEvents nom As ManagedText
    Private WithEvents _Labels_7 As System.Windows.Forms.Label
    Private WithEvents _Labels_6 As System.Windows.Forms.Label
    Private WithEvents _Labels_5 As System.Windows.Forms.Label
    Private WithEvents _Labels_4 As System.Windows.Forms.Label
    Private WithEvents _Labels_3 As System.Windows.Forms.Label
    Private WithEvents _Label2_2 As System.Windows.Forms.Label
    Private WithEvents _Labels_2 As System.Windows.Forms.Label
    Private WithEvents _Labels_1 As System.Windows.Forms.Label
    Private WithEvents _Labels_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents courriel As ManagedText
    Private WithEvents url As ManagedText
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents getReference As System.Windows.Forms.Button
    Private WithEvents reference As System.Windows.Forms.Label
    Private WithEvents infoNom As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents ville As Clinica.ManagedCombo
    Private WithEvents downTel As System.Windows.Forms.Button
    Private WithEvents upTel As System.Windows.Forms.Button
    Private WithEvents addTel As System.Windows.Forms.Button
    Private WithEvents modifTel As System.Windows.Forms.Button
    Private WithEvents delTel As System.Windows.Forms.Button
    Private WithEvents telephones As Clinica.ManagedCombo
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents employeur As Clinica.ManagedCombo
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents selectWorkPlace As System.Windows.Forms.Button
    Public WithEvents workPlace As System.Windows.Forms.Label
    Private WithEvents viderComm As System.Windows.Forms.Button
    Friend WithEvents listeCommunications As CI.Controls.List
    Private WithEvents commFiltrage As System.Windows.Forms.ComboBox
    Private WithEvents label29 As System.Windows.Forms.Label
    Private WithEvents importComm As System.Windows.Forms.Button
    Private WithEvents delComm As System.Windows.Forms.Button
    Private WithEvents modifComm As System.Windows.Forms.Button
    Private WithEvents addComm As System.Windows.Forms.Button
    Private WithEvents commRemarques As ManagedText
    Private WithEvents label28 As System.Windows.Forms.Label
    Private WithEvents commUser As System.Windows.Forms.Label
    Private WithEvents label27 As System.Windows.Forms.Label
    Private WithEvents commDate As System.Windows.Forms.Label
    Private WithEvents selectCommDate As System.Windows.Forms.Button
    Private WithEvents label26 As System.Windows.Forms.Label
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents commType2 As System.Windows.Forms.RadioButton
    Private WithEvents commType1 As System.Windows.Forms.RadioButton
    Private WithEvents commSujet As Clinica.ManagedCombo
    Private WithEvents selectKeyPeople As System.Windows.Forms.Button
    Public WithEvents commDeA As ManagedCombo
    Private WithEvents label25 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents lblCommDeA As System.Windows.Forms.Label
    Private WithEvents startStopBillChanging As System.Windows.Forms.Button
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents filterBills As System.Windows.Forms.Button
    Private WithEvents dateA As System.Windows.Forms.Label
    Private WithEvents dateDe As System.Windows.Forms.Label
    Private WithEvents choixA As System.Windows.Forms.Button
    Private WithEvents dateAll As System.Windows.Forms.CheckBox
    Private WithEvents choixDe As System.Windows.Forms.Button
    Private WithEvents label31 As System.Windows.Forms.Label
    Private WithEvents ongletskp As System.Windows.Forms.TabControl
    Private WithEvents _ongletskp_TabPage3 As System.Windows.Forms.TabPage
    Private WithEvents _ongletskp_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents paiements As System.Windows.Forms.Button
    Private WithEvents menuviewmodifkpcommunications As System.Windows.Forms.ContextMenu
    Private WithEvents menuImportFromOutside As System.Windows.Forms.MenuItem
    Private WithEvents menuImportFromDB As System.Windows.Forms.MenuItem
    Private WithEvents menuViewModifKP As System.Windows.Forms.MenuStrip
    Private WithEvents menuComm As Clinica.ContextMenuItem
    Private WithEvents enregistrerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents importerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ouvrirLeFichierJointToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents supprimerLeFichierJointToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents supprimerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents addAlert As System.Windows.Forms.Button
    Private WithEvents createBill As System.Windows.Forms.Button
    Public WithEvents facturesView As DataGridPlus
    Private WithEvents startStopCommChanging As System.Windows.Forms.Button
    Friend WithEvents DateF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoFacture As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents commCategorie As Clinica.ManagedCombo
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents commFiltrageCat As System.Windows.Forms.ComboBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents publipostage As Clinica.ManagedCombo
    Public WithEvents label21 As System.Windows.Forms.Label
    Public WithEvents colorReceived As System.Windows.Forms.Label
    Public WithEvents colorSent As System.Windows.Forms.Label
    Public WithEvents commReception As System.Windows.Forms.CheckBox
    Public WithEvents commEnvoie As System.Windows.Forms.CheckBox
    Private WithEvents curFactureBox As Clinica.FacturationBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(viewmodifKeyPeople))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.noident = New CI.Base.ManagedText
        Me.adminbutton = New System.Windows.Forms.Button
        Me.submit = New System.Windows.Forms.Button
        Me.ainfo = New CI.Base.ManagedText
        Me.adresse = New CI.Base.ManagedText
        Me.codepostal1 = New CI.Base.ManagedText
        Me.codepostal2 = New CI.Base.ManagedText
        Me.categorie = New CI.Clinica.ManagedCombo
        Me.nom = New CI.Base.ManagedText
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
        Me.courriel = New CI.Base.ManagedText
        Me.url = New CI.Base.ManagedText
        Me.label2 = New System.Windows.Forms.Label
        Me.getReference = New System.Windows.Forms.Button
        Me.reference = New System.Windows.Forms.Label
        Me.infoNom = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.downTel = New System.Windows.Forms.Button
        Me.upTel = New System.Windows.Forms.Button
        Me.addTel = New System.Windows.Forms.Button
        Me.modifTel = New System.Windows.Forms.Button
        Me.delTel = New System.Windows.Forms.Button
        Me.selectWorkPlace = New System.Windows.Forms.Button
        Me.viderComm = New System.Windows.Forms.Button
        Me.importComm = New System.Windows.Forms.Button
        Me.delComm = New System.Windows.Forms.Button
        Me.modifComm = New System.Windows.Forms.Button
        Me.addComm = New System.Windows.Forms.Button
        Me.selectCommDate = New System.Windows.Forms.Button
        Me.selectKeyPeople = New System.Windows.Forms.Button
        Me.startStopBillChanging = New System.Windows.Forms.Button
        Me.filterBills = New System.Windows.Forms.Button
        Me.startStopCommChanging = New System.Windows.Forms.Button
        Me.paiements = New System.Windows.Forms.Button
        Me.addAlert = New System.Windows.Forms.Button
        Me.createBill = New System.Windows.Forms.Button
        Me.ville = New CI.Clinica.ManagedCombo
        Me.telephones = New CI.Clinica.ManagedCombo
        Me.label4 = New System.Windows.Forms.Label
        Me.employeur = New CI.Clinica.ManagedCombo
        Me.workPlace = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.ongletskp = New System.Windows.Forms.TabControl
        Me._ongletskp_TabPage3 = New System.Windows.Forms.TabPage
        Me.colorReceived = New System.Windows.Forms.Label
        Me.colorSent = New System.Windows.Forms.Label
        Me.commReception = New System.Windows.Forms.CheckBox
        Me.commEnvoie = New System.Windows.Forms.CheckBox
        Me.listeCommunications = New CI.Controls.List
        Me.commFiltrageCat = New System.Windows.Forms.ComboBox
        Me.commFiltrage = New System.Windows.Forms.ComboBox
        Me.label6 = New System.Windows.Forms.Label
        Me.label29 = New System.Windows.Forms.Label
        Me.commRemarques = New CI.Base.ManagedText
        Me.label28 = New System.Windows.Forms.Label
        Me.commUser = New System.Windows.Forms.Label
        Me.label27 = New System.Windows.Forms.Label
        Me.commDate = New System.Windows.Forms.Label
        Me.label26 = New System.Windows.Forms.Label
        Me.panel2 = New System.Windows.Forms.Panel
        Me.commType2 = New System.Windows.Forms.RadioButton
        Me.commType1 = New System.Windows.Forms.RadioButton
        Me.commCategorie = New CI.Clinica.ManagedCombo
        Me.commSujet = New CI.Clinica.ManagedCombo
        Me.commDeA = New CI.Clinica.ManagedCombo
        Me.label25 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label23 = New System.Windows.Forms.Label
        Me.lblCommDeA = New System.Windows.Forms.Label
        Me._ongletskp_TabPage4 = New System.Windows.Forms.TabPage
        Me.facturesView = New CI.Base.Windows.Forms.DataGridPlus
        Me.DateF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PPO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PU = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoFacture = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.dateA = New System.Windows.Forms.Label
        Me.dateDe = New System.Windows.Forms.Label
        Me.choixA = New System.Windows.Forms.Button
        Me.dateAll = New System.Windows.Forms.CheckBox
        Me.choixDe = New System.Windows.Forms.Button
        Me.label31 = New System.Windows.Forms.Label
        Me.menuviewmodifkpcommunications = New System.Windows.Forms.ContextMenu
        Me.menuImportFromOutside = New System.Windows.Forms.MenuItem
        Me.menuImportFromDB = New System.Windows.Forms.MenuItem
        Me.menuViewModifKP = New System.Windows.Forms.MenuStrip
        Me.menuComm = New CI.Clinica.ContextMenuItem
        Me.enregistrerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.importerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ouvrirLeFichierJointToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.supprimerLeFichierJointToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.supprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.publipostage = New CI.Clinica.ManagedCombo
        Me.label21 = New System.Windows.Forms.Label
        Me.ongletskp.SuspendLayout()
        Me._ongletskp_TabPage3.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me._ongletskp_TabPage4.SuspendLayout()
        CType(Me.facturesView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox1.SuspendLayout()
        Me.menuViewModifKP.SuspendLayout()
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
        Me.adminbutton.Location = New System.Drawing.Point(0, 446)
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
        Me.submit.Location = New System.Drawing.Point(305, 455)
        Me.submit.Name = "submit"
        Me.submit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.submit.Size = New System.Drawing.Size(24, 24)
        Me.submit.TabIndex = 14
        Me.toolTip1.SetToolTip(Me.submit, "Modifier les informations de base de la personne/organisme clé")
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
        Me.ainfo.Location = New System.Drawing.Point(11, 351)
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
        Me.ainfo.Size = New System.Drawing.Size(318, 98)
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
        Me.categorie.IntegralHeight = False
        Me.categorie.itemsToolTipDuration = 10000
        Me.categorie.Location = New System.Drawing.Point(120, 32)
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
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(8, 208)
        Me.label1.Name = "label1"
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
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(8, 232)
        Me.label2.Name = "label2"
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
        Me.getReference.Location = New System.Drawing.Point(104, 455)
        Me.getReference.Name = "getReference"
        Me.getReference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.getReference.Size = New System.Drawing.Size(24, 24)
        Me.getReference.TabIndex = 13
        Me.toolTip1.SetToolTip(Me.getReference, "Sélectionner un compte client")
        Me.getReference.UseVisualStyleBackColor = False
        '
        'reference
        '
        Me.reference.AutoSize = True
        Me.reference.BackColor = System.Drawing.Color.Transparent
        Me.reference.Cursor = System.Windows.Forms.Cursors.Default
        Me.reference.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reference.ForeColor = System.Drawing.SystemColors.ControlText
        Me.reference.Location = New System.Drawing.Point(134, 460)
        Me.reference.Name = "reference"
        Me.reference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.reference.Size = New System.Drawing.Size(105, 14)
        Me.reference.TabIndex = 29
        Me.reference.Tag = "0"
        Me.reference.Text = "Aucun compte client"
        '
        'infoNom
        '
        Me.infoNom.BackColor = System.Drawing.SystemColors.Info
        Me.infoNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.infoNom.Cursor = System.Windows.Forms.Cursors.Default
        Me.infoNom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.infoNom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.infoNom.Location = New System.Drawing.Point(120, 28)
        Me.infoNom.Name = "infoNom"
        Me.infoNom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.infoNom.Size = New System.Drawing.Size(208, 48)
        Me.infoNom.TabIndex = 30
        Me.infoNom.Text = "S'il s'agit du nom d'une personne veuillez l'inscrire de la façon suivante : Nom," & _
            "Prénom"
        Me.infoNom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.toolTip1.SetToolTip(Me.infoNom, "Cliquez dessus pour faire disparaître")
        Me.infoNom.Visible = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(8, 460)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(90, 14)
        Me.label3.TabIndex = 31
        Me.label3.Text = "Compte client :"
        '
        'toolTip1
        '
        Me.toolTip1.ShowAlways = True
        '
        'downTel
        '
        Me.downTel.BackColor = System.Drawing.SystemColors.Control
        Me.downTel.Enabled = False
        Me.downTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.downTel.Location = New System.Drawing.Point(272, 152)
        Me.downTel.Name = "downTel"
        Me.downTel.Size = New System.Drawing.Size(24, 24)
        Me.downTel.TabIndex = 80
        Me.downTel.TabStop = False
        Me.toolTip1.SetToolTip(Me.downTel, "Descendre un numéro de téléphone")
        Me.downTel.UseVisualStyleBackColor = False
        '
        'upTel
        '
        Me.upTel.BackColor = System.Drawing.SystemColors.Control
        Me.upTel.Enabled = False
        Me.upTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.upTel.Location = New System.Drawing.Point(240, 152)
        Me.upTel.Name = "upTel"
        Me.upTel.Size = New System.Drawing.Size(24, 24)
        Me.upTel.TabIndex = 79
        Me.upTel.TabStop = False
        Me.toolTip1.SetToolTip(Me.upTel, "Monter un numéro de téléphone")
        Me.upTel.UseVisualStyleBackColor = False
        '
        'addTel
        '
        Me.addTel.BackColor = System.Drawing.SystemColors.Control
        Me.addTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.addTel.Location = New System.Drawing.Point(144, 152)
        Me.addTel.Name = "addTel"
        Me.addTel.Size = New System.Drawing.Size(24, 24)
        Me.addTel.TabIndex = 76
        Me.addTel.TabStop = False
        Me.toolTip1.SetToolTip(Me.addTel, "Ajout d'un numéro de téléphone")
        Me.addTel.UseVisualStyleBackColor = False
        '
        'modifTel
        '
        Me.modifTel.BackColor = System.Drawing.SystemColors.Control
        Me.modifTel.Enabled = False
        Me.modifTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifTel.Location = New System.Drawing.Point(176, 152)
        Me.modifTel.Name = "modifTel"
        Me.modifTel.Size = New System.Drawing.Size(24, 24)
        Me.modifTel.TabIndex = 77
        Me.modifTel.TabStop = False
        Me.toolTip1.SetToolTip(Me.modifTel, "Modifier un numéro de téléphone")
        Me.modifTel.UseVisualStyleBackColor = False
        '
        'delTel
        '
        Me.delTel.BackColor = System.Drawing.SystemColors.Control
        Me.delTel.Enabled = False
        Me.delTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delTel.Location = New System.Drawing.Point(208, 152)
        Me.delTel.Name = "delTel"
        Me.delTel.Size = New System.Drawing.Size(24, 24)
        Me.delTel.TabIndex = 78
        Me.delTel.TabStop = False
        Me.toolTip1.SetToolTip(Me.delTel, "Enlever un numéro de téléphone")
        Me.delTel.UseVisualStyleBackColor = False
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
        Me.toolTip1.SetToolTip(Me.selectWorkPlace, "Sélectionner le lieu de travail d'une personne clée")
        Me.selectWorkPlace.UseVisualStyleBackColor = False
        '
        'viderComm
        '
        Me.viderComm.BackColor = System.Drawing.SystemColors.Control
        Me.viderComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.viderComm.Enabled = False
        Me.viderComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.viderComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viderComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.viderComm.Location = New System.Drawing.Point(528, 120)
        Me.viderComm.Name = "viderComm"
        Me.viderComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.viderComm.Size = New System.Drawing.Size(24, 24)
        Me.viderComm.TabIndex = 122
        Me.toolTip1.SetToolTip(Me.viderComm, "Vider les champs")
        Me.viderComm.UseVisualStyleBackColor = False
        '
        'importComm
        '
        Me.importComm.BackColor = System.Drawing.SystemColors.Control
        Me.importComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.importComm.Enabled = False
        Me.importComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.importComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.importComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.importComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.importComm.Location = New System.Drawing.Point(528, 333)
        Me.importComm.Name = "importComm"
        Me.importComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.importComm.Size = New System.Drawing.Size(24, 24)
        Me.importComm.TabIndex = 128
        Me.toolTip1.SetToolTip(Me.importComm, "Importer un fichier pour la communication sélectionnée")
        Me.importComm.UseVisualStyleBackColor = False
        '
        'delComm
        '
        Me.delComm.BackColor = System.Drawing.SystemColors.Control
        Me.delComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.delComm.Enabled = False
        Me.delComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.delComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delComm.Location = New System.Drawing.Point(528, 433)
        Me.delComm.Name = "delComm"
        Me.delComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delComm.Size = New System.Drawing.Size(24, 24)
        Me.delComm.TabIndex = 127
        Me.toolTip1.SetToolTip(Me.delComm, "Supprimer la communication sélectionnée")
        Me.delComm.UseVisualStyleBackColor = False
        '
        'modifComm
        '
        Me.modifComm.BackColor = System.Drawing.SystemColors.Control
        Me.modifComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.modifComm.Enabled = False
        Me.modifComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modifComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifComm.Location = New System.Drawing.Point(528, 383)
        Me.modifComm.Name = "modifComm"
        Me.modifComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modifComm.Size = New System.Drawing.Size(24, 24)
        Me.modifComm.TabIndex = 121
        Me.toolTip1.SetToolTip(Me.modifComm, "Enregistrer la communation sélectionnée")
        Me.modifComm.UseVisualStyleBackColor = False
        '
        'addComm
        '
        Me.addComm.BackColor = System.Drawing.SystemColors.Control
        Me.addComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.addComm.Enabled = False
        Me.addComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.addComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.addComm.Location = New System.Drawing.Point(528, 283)
        Me.addComm.Name = "addComm"
        Me.addComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.addComm.Size = New System.Drawing.Size(24, 24)
        Me.addComm.TabIndex = 120
        Me.toolTip1.SetToolTip(Me.addComm, "Ajouter une communication")
        Me.addComm.UseVisualStyleBackColor = False
        '
        'selectCommDate
        '
        Me.selectCommDate.BackColor = System.Drawing.SystemColors.Control
        Me.selectCommDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectCommDate.Enabled = False
        Me.selectCommDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectCommDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectCommDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectCommDate.Location = New System.Drawing.Point(75, 115)
        Me.selectCommDate.Name = "selectCommDate"
        Me.selectCommDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectCommDate.Size = New System.Drawing.Size(24, 24)
        Me.selectCommDate.TabIndex = 114
        Me.toolTip1.SetToolTip(Me.selectCommDate, "Choisir la date de la communication")
        Me.selectCommDate.UseVisualStyleBackColor = False
        '
        'selectKeyPeople
        '
        Me.selectKeyPeople.BackColor = System.Drawing.SystemColors.Control
        Me.selectKeyPeople.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectKeyPeople.Enabled = False
        Me.selectKeyPeople.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectKeyPeople.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectKeyPeople.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectKeyPeople.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectKeyPeople.Location = New System.Drawing.Point(528, 31)
        Me.selectKeyPeople.Name = "selectKeyPeople"
        Me.selectKeyPeople.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectKeyPeople.Size = New System.Drawing.Size(24, 24)
        Me.selectKeyPeople.TabIndex = 110
        Me.toolTip1.SetToolTip(Me.selectKeyPeople, "Sélectionner une personne / organisme clé")
        Me.selectKeyPeople.UseVisualStyleBackColor = False
        '
        'startStopBillChanging
        '
        Me.startStopBillChanging.Enabled = False
        Me.startStopBillChanging.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.startStopBillChanging.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.startStopBillChanging.Location = New System.Drawing.Point(784, 0)
        Me.startStopBillChanging.Name = "startStopBillChanging"
        Me.startStopBillChanging.Size = New System.Drawing.Size(24, 24)
        Me.startStopBillChanging.TabIndex = 149
        Me.toolTip1.SetToolTip(Me.startStopBillChanging, "Commencer la modification des factures et paiements")
        Me.startStopBillChanging.Visible = False
        '
        'filterBills
        '
        Me.filterBills.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.filterBills.Location = New System.Drawing.Point(352, 12)
        Me.filterBills.Name = "filterBills"
        Me.filterBills.Size = New System.Drawing.Size(32, 21)
        Me.filterBills.TabIndex = 6
        Me.toolTip1.SetToolTip(Me.filterBills, "Filtrer")
        '
        'startStopCommChanging
        '
        Me.startStopCommChanging.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.startStopCommChanging.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.startStopCommChanging.Location = New System.Drawing.Point(784, 0)
        Me.startStopCommChanging.Name = "startStopCommChanging"
        Me.startStopCommChanging.Size = New System.Drawing.Size(24, 24)
        Me.startStopCommChanging.TabIndex = 149
        Me.toolTip1.SetToolTip(Me.startStopCommChanging, "Commencer la modification les communications")
        '
        'paiements
        '
        Me.paiements.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.paiements.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.paiements.Location = New System.Drawing.Point(844, 0)
        Me.paiements.Name = "paiements"
        Me.paiements.Size = New System.Drawing.Size(24, 24)
        Me.paiements.TabIndex = 150
        Me.toolTip1.SetToolTip(Me.paiements, "Effectuer les paiements")
        '
        'addAlert
        '
        Me.addAlert.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addAlert.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.addAlert.Location = New System.Drawing.Point(873, 0)
        Me.addAlert.Name = "addAlert"
        Me.addAlert.Size = New System.Drawing.Size(24, 24)
        Me.addAlert.TabIndex = 150
        Me.toolTip1.SetToolTip(Me.addAlert, "Ajouter une alarme sur le compte")
        '
        'createBill
        '
        Me.createBill.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.createBill.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.createBill.Location = New System.Drawing.Point(814, 0)
        Me.createBill.Name = "createBill"
        Me.createBill.Size = New System.Drawing.Size(24, 24)
        Me.createBill.TabIndex = 150
        Me.toolTip1.SetToolTip(Me.createBill, "Ajouter une nouvelle facture pour cette personne / organisme clé")
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
        Me.ville.firstLettersCapital = False
        Me.ville.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ville.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ville.IntegralHeight = False
        Me.ville.itemsToolTipDuration = 10000
        Me.ville.Location = New System.Drawing.Point(120, 80)
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
        Me.ville.Size = New System.Drawing.Size(208, 22)
        Me.ville.Sorted = True
        Me.ville.TabIndex = 3
        Me.ville.trimText = False
        '
        'telephones
        '
        Me.telephones.acceptAlpha = True
        Me.telephones.acceptedChars = Nothing
        Me.telephones.acceptNumeric = True
        Me.telephones.allCapital = False
        Me.telephones.allLower = False
        Me.telephones.autoComplete = True
        Me.telephones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.telephones.autoSizeDropDown = True
        Me.telephones.BackColor = System.Drawing.Color.White
        Me.telephones.blockOnMaximum = False
        Me.telephones.blockOnMinimum = False
        Me.telephones.cb_AcceptLeftZeros = False
        Me.telephones.cb_AcceptNegative = False
        Me.telephones.currencyBox = False
        Me.telephones.dbField = Nothing
        Me.telephones.doComboDelete = True
        Me.telephones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.telephones.firstLetterCapital = False
        Me.telephones.firstLettersCapital = False
        Me.telephones.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.telephones.IntegralHeight = False
        Me.telephones.itemsToolTipDuration = 10000
        Me.telephones.Location = New System.Drawing.Point(120, 128)
        Me.telephones.manageText = True
        Me.telephones.matchExp = Nothing
        Me.telephones.maximum = 0
        Me.telephones.minimum = 0
        Me.telephones.Name = "telephones"
        Me.telephones.nbDecimals = CType(-1, Short)
        Me.telephones.onlyAlphabet = False
        Me.telephones.pathOfList = ""
        Me.telephones.ReadOnly = False
        Me.telephones.refuseAccents = False
        Me.telephones.refusedChars = ""
        Me.telephones.showItemsToolTip = False
        Me.telephones.Size = New System.Drawing.Size(208, 22)
        Me.telephones.TabIndex = 6
        Me.telephones.trimText = False
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(8, 256)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(72, 14)
        Me.label4.TabIndex = 81
        Me.label4.Text = "Employeur :"
        '
        'employeur
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
        Me.employeur.IntegralHeight = False
        Me.employeur.itemsToolTipDuration = 10000
        Me.employeur.Location = New System.Drawing.Point(120, 256)
        Me.employeur.manageText = True
        Me.employeur.matchExp = ""
        Me.employeur.maximum = 0
        Me.employeur.minimum = 0
        Me.employeur.Name = "employeur"
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
        'workPlace
        '
        Me.workPlace.AutoSize = True
        Me.workPlace.BackColor = System.Drawing.Color.Transparent
        Me.workPlace.Cursor = System.Windows.Forms.Cursors.Default
        Me.workPlace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.workPlace.ForeColor = System.Drawing.SystemColors.ControlText
        Me.workPlace.Location = New System.Drawing.Point(152, 288)
        Me.workPlace.Name = "workPlace"
        Me.workPlace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.workPlace.Size = New System.Drawing.Size(109, 14)
        Me.workPlace.TabIndex = 29
        Me.workPlace.Tag = "0"
        Me.workPlace.Text = "Aucun organisme clé"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(8, 280)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(90, 14)
        Me.label8.TabIndex = 31
        Me.label8.Text = "Lieu de travail :"
        '
        'ongletskp
        '
        Me.ongletskp.Controls.Add(Me._ongletskp_TabPage3)
        Me.ongletskp.Controls.Add(Me._ongletskp_TabPage4)
        Me.ongletskp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ongletskp.ItemSize = New System.Drawing.Size(42, 18)
        Me.ongletskp.Location = New System.Drawing.Point(335, 0)
        Me.ongletskp.Name = "ongletskp"
        Me.ongletskp.SelectedIndex = 0
        Me.ongletskp.Size = New System.Drawing.Size(568, 488)
        Me.ongletskp.TabIndex = 83
        '
        '_ongletskp_TabPage3
        '
        Me._ongletskp_TabPage3.Controls.Add(Me.colorReceived)
        Me._ongletskp_TabPage3.Controls.Add(Me.colorSent)
        Me._ongletskp_TabPage3.Controls.Add(Me.commReception)
        Me._ongletskp_TabPage3.Controls.Add(Me.commEnvoie)
        Me._ongletskp_TabPage3.Controls.Add(Me.viderComm)
        Me._ongletskp_TabPage3.Controls.Add(Me.listeCommunications)
        Me._ongletskp_TabPage3.Controls.Add(Me.commFiltrageCat)
        Me._ongletskp_TabPage3.Controls.Add(Me.commFiltrage)
        Me._ongletskp_TabPage3.Controls.Add(Me.label6)
        Me._ongletskp_TabPage3.Controls.Add(Me.label29)
        Me._ongletskp_TabPage3.Controls.Add(Me.importComm)
        Me._ongletskp_TabPage3.Controls.Add(Me.delComm)
        Me._ongletskp_TabPage3.Controls.Add(Me.modifComm)
        Me._ongletskp_TabPage3.Controls.Add(Me.addComm)
        Me._ongletskp_TabPage3.Controls.Add(Me.commRemarques)
        Me._ongletskp_TabPage3.Controls.Add(Me.label28)
        Me._ongletskp_TabPage3.Controls.Add(Me.commUser)
        Me._ongletskp_TabPage3.Controls.Add(Me.label27)
        Me._ongletskp_TabPage3.Controls.Add(Me.commDate)
        Me._ongletskp_TabPage3.Controls.Add(Me.selectCommDate)
        Me._ongletskp_TabPage3.Controls.Add(Me.label26)
        Me._ongletskp_TabPage3.Controls.Add(Me.panel2)
        Me._ongletskp_TabPage3.Controls.Add(Me.commCategorie)
        Me._ongletskp_TabPage3.Controls.Add(Me.commSujet)
        Me._ongletskp_TabPage3.Controls.Add(Me.selectKeyPeople)
        Me._ongletskp_TabPage3.Controls.Add(Me.commDeA)
        Me._ongletskp_TabPage3.Controls.Add(Me.label25)
        Me._ongletskp_TabPage3.Controls.Add(Me.label5)
        Me._ongletskp_TabPage3.Controls.Add(Me.label23)
        Me._ongletskp_TabPage3.Controls.Add(Me.lblCommDeA)
        Me._ongletskp_TabPage3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ongletskp_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._ongletskp_TabPage3.Name = "_ongletskp_TabPage3"
        Me._ongletskp_TabPage3.Size = New System.Drawing.Size(560, 462)
        Me._ongletskp_TabPage3.TabIndex = 3
        Me._ongletskp_TabPage3.Text = "Communications"
        '
        'colorReceived
        '
        Me.colorReceived.BackColor = System.Drawing.Color.White
        Me.colorReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorReceived.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorReceived.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorReceived.ForeColor = System.Drawing.SystemColors.ControlText
        Me.colorReceived.Location = New System.Drawing.Point(283, 233)
        Me.colorReceived.Name = "colorReceived"
        Me.colorReceived.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorReceived.Size = New System.Drawing.Size(62, 18)
        Me.colorReceived.TabIndex = 136
        Me.colorReceived.Text = "Réception"
        Me.colorReceived.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'colorSent
        '
        Me.colorSent.BackColor = System.Drawing.Color.LightGray
        Me.colorSent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorSent.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorSent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorSent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.colorSent.Location = New System.Drawing.Point(199, 233)
        Me.colorSent.Name = "colorSent"
        Me.colorSent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorSent.Size = New System.Drawing.Size(42, 18)
        Me.colorSent.TabIndex = 137
        Me.colorSent.Text = "Envoi"
        Me.colorSent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'commReception
        '
        Me.commReception.AutoSize = True
        Me.commReception.Checked = True
        Me.commReception.CheckState = System.Windows.Forms.CheckState.Checked
        Me.commReception.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commReception.Location = New System.Drawing.Point(262, 236)
        Me.commReception.Name = "commReception"
        Me.commReception.Size = New System.Drawing.Size(15, 14)
        Me.commReception.TabIndex = 135
        '
        'commEnvoie
        '
        Me.commEnvoie.AutoSize = True
        Me.commEnvoie.Checked = True
        Me.commEnvoie.CheckState = System.Windows.Forms.CheckState.Checked
        Me.commEnvoie.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commEnvoie.Location = New System.Drawing.Point(178, 236)
        Me.commEnvoie.Name = "commEnvoie"
        Me.commEnvoie.Size = New System.Drawing.Size(15, 14)
        Me.commEnvoie.TabIndex = 134
        '
        'listeCommunications
        '
        Me.listeCommunications.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.listeCommunications.autoAdjust = True
        Me.listeCommunications.autoKeyDownSelection = True
        Me.listeCommunications.autoSizeHorizontally = False
        Me.listeCommunications.autoSizeVertically = False
        Me.listeCommunications.BackColor = System.Drawing.Color.White
        Me.listeCommunications.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.listeCommunications.baseBackColor = System.Drawing.Color.White
        Me.listeCommunications.baseFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.listeCommunications.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.listeCommunications.bgColor = System.Drawing.Color.White
        Me.listeCommunications.borderColor = System.Drawing.Color.Empty
        Me.listeCommunications.borderSelColor = System.Drawing.Color.Empty
        Me.listeCommunications.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.listeCommunications.CausesValidation = False
        Me.listeCommunications.clickEnabled = True
        Me.listeCommunications.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.listeCommunications.do3D = False
        Me.listeCommunications.draw = False
        Me.listeCommunications.extraWidth = 0
        Me.listeCommunications.hScrollColor = System.Drawing.SystemColors.Control
        Me.listeCommunications.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listeCommunications.hScrolling = True
        Me.listeCommunications.hsValue = 0
        Me.listeCommunications.icons = CType(resources.GetObject("listeCommunications.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.listeCommunications.itemBorder = 0
        Me.listeCommunications.itemMargin = 0
        Me.listeCommunications.items = CType(resources.GetObject("listeCommunications.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.listeCommunications.Location = New System.Drawing.Point(8, 283)
        Me.listeCommunications.mouseMove3D = False
        Me.listeCommunications.mouseSpeed = 0
        Me.listeCommunications.Name = "listeCommunications"
        Me.listeCommunications.objMaxHeight = 0.0!
        Me.listeCommunications.objMaxWidth = 0.0!
        Me.listeCommunications.objMinHeight = 0.0!
        Me.listeCommunications.objMinWidth = 0.0!
        Me.listeCommunications.reverseSorting = False
        Me.listeCommunications.selected = -1
        Me.listeCommunications.selectedClickAllowed = False
        Me.listeCommunications.selectMultiple = False
        Me.listeCommunications.Size = New System.Drawing.Size(512, 174)
        Me.listeCommunications.sorted = True
        Me.listeCommunications.TabIndex = 133
        Me.listeCommunications.toolTipText = Nothing
        Me.listeCommunications.vScrollColor = System.Drawing.SystemColors.Control
        Me.listeCommunications.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listeCommunications.vScrolling = True
        Me.listeCommunications.vsValue = 0
        '
        'commFiltrageCat
        '
        Me.commFiltrageCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.commFiltrageCat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commFiltrageCat.Location = New System.Drawing.Point(320, 255)
        Me.commFiltrageCat.Name = "commFiltrageCat"
        Me.commFiltrageCat.Size = New System.Drawing.Size(203, 22)
        Me.commFiltrageCat.Sorted = True
        Me.commFiltrageCat.TabIndex = 132
        '
        'commFiltrage
        '
        Me.commFiltrage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.commFiltrage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commFiltrage.Location = New System.Drawing.Point(56, 255)
        Me.commFiltrage.Name = "commFiltrage"
        Me.commFiltrage.Size = New System.Drawing.Size(192, 22)
        Me.commFiltrage.Sorted = True
        Me.commFiltrage.TabIndex = 132
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.SystemColors.Control
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(254, 258)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(61, 14)
        Me.label6.TabIndex = 131
        Me.label6.Text = "Catégorie :"
        '
        'label29
        '
        Me.label29.AutoSize = True
        Me.label29.BackColor = System.Drawing.SystemColors.Control
        Me.label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.label29.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label29.Location = New System.Drawing.Point(8, 258)
        Me.label29.Name = "label29"
        Me.label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label29.Size = New System.Drawing.Size(43, 14)
        Me.label29.TabIndex = 131
        Me.label29.Text = "À | De :"
        '
        'commRemarques
        '
        Me.commRemarques.acceptAlpha = True
        Me.commRemarques.acceptedChars = ""
        Me.commRemarques.acceptNumeric = True
        Me.commRemarques.allCapital = False
        Me.commRemarques.allLower = False
        Me.commRemarques.blockOnMaximum = False
        Me.commRemarques.blockOnMinimum = False
        Me.commRemarques.cb_AcceptLeftZeros = False
        Me.commRemarques.cb_AcceptNegative = False
        Me.commRemarques.currencyBox = False
        Me.commRemarques.firstLetterCapital = True
        Me.commRemarques.firstLettersCapital = False
        Me.commRemarques.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commRemarques.Location = New System.Drawing.Point(8, 160)
        Me.commRemarques.manageText = True
        Me.commRemarques.matchExp = ""
        Me.commRemarques.maximum = 0
        Me.commRemarques.minimum = 0
        Me.commRemarques.Multiline = True
        Me.commRemarques.Name = "commRemarques"
        Me.commRemarques.nbDecimals = CType(-1, Short)
        Me.commRemarques.onlyAlphabet = False
        Me.commRemarques.refuseAccents = False
        Me.commRemarques.refusedChars = ""
        Me.commRemarques.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.commRemarques.showInternalContextMenu = True
        Me.commRemarques.Size = New System.Drawing.Size(544, 64)
        Me.commRemarques.TabIndex = 119
        Me.commRemarques.trimText = False
        '
        'label28
        '
        Me.label28.AutoSize = True
        Me.label28.BackColor = System.Drawing.SystemColors.Control
        Me.label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.label28.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label28.Location = New System.Drawing.Point(8, 144)
        Me.label28.Name = "label28"
        Me.label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label28.Size = New System.Drawing.Size(70, 14)
        Me.label28.TabIndex = 118
        Me.label28.Text = "Remarques :"
        '
        'commUser
        '
        Me.commUser.AutoSize = True
        Me.commUser.BackColor = System.Drawing.SystemColors.Control
        Me.commUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.commUser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.commUser.Location = New System.Drawing.Point(336, 120)
        Me.commUser.Name = "commUser"
        Me.commUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.commUser.Size = New System.Drawing.Size(0, 14)
        Me.commUser.TabIndex = 117
        '
        'label27
        '
        Me.label27.AutoSize = True
        Me.label27.BackColor = System.Drawing.SystemColors.Control
        Me.label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.label27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label27.Location = New System.Drawing.Point(272, 120)
        Me.label27.Name = "label27"
        Me.label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label27.Size = New System.Drawing.Size(63, 14)
        Me.label27.TabIndex = 116
        Me.label27.Text = "Utilisateur :"
        '
        'commDate
        '
        Me.commDate.AutoSize = True
        Me.commDate.BackColor = System.Drawing.SystemColors.Control
        Me.commDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.commDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.commDate.Location = New System.Drawing.Point(107, 121)
        Me.commDate.Name = "commDate"
        Me.commDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.commDate.Size = New System.Drawing.Size(0, 14)
        Me.commDate.TabIndex = 115
        '
        'label26
        '
        Me.label26.AutoSize = True
        Me.label26.BackColor = System.Drawing.SystemColors.Control
        Me.label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.label26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label26.Location = New System.Drawing.Point(8, 120)
        Me.label26.Name = "label26"
        Me.label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label26.Size = New System.Drawing.Size(36, 14)
        Me.label26.TabIndex = 113
        Me.label26.Text = "Date :"
        '
        'panel2
        '
        Me.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel2.Controls.Add(Me.commType2)
        Me.panel2.Controls.Add(Me.commType1)
        Me.panel2.Location = New System.Drawing.Point(8, 8)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(528, 16)
        Me.panel2.TabIndex = 112
        '
        'commType2
        '
        Me.commType2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commType2.Location = New System.Drawing.Point(259, 0)
        Me.commType2.Name = "commType2"
        Me.commType2.Size = New System.Drawing.Size(73, 16)
        Me.commType2.TabIndex = 1
        Me.commType2.Text = "Réception"
        '
        'commType1
        '
        Me.commType1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commType1.Location = New System.Drawing.Point(195, 0)
        Me.commType1.Name = "commType1"
        Me.commType1.Size = New System.Drawing.Size(57, 16)
        Me.commType1.TabIndex = 0
        Me.commType1.Text = "Envoi"
        '
        'commCategorie
        '
        Me.commCategorie.acceptAlpha = True
        Me.commCategorie.acceptedChars = Nothing
        Me.commCategorie.acceptNumeric = True
        Me.commCategorie.allCapital = False
        Me.commCategorie.allLower = False
        Me.commCategorie.autoComplete = True
        Me.commCategorie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.commCategorie.autoSizeDropDown = True
        Me.commCategorie.BackColor = System.Drawing.Color.White
        Me.commCategorie.blockOnMaximum = False
        Me.commCategorie.blockOnMinimum = False
        Me.commCategorie.cb_AcceptLeftZeros = False
        Me.commCategorie.cb_AcceptNegative = False
        Me.commCategorie.currencyBox = False
        Me.commCategorie.dbField = "CommCategories.Categorie"
        Me.commCategorie.doComboDelete = True
        Me.commCategorie.firstLetterCapital = True
        Me.commCategorie.firstLettersCapital = False
        Me.commCategorie.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commCategorie.IntegralHeight = False
        Me.commCategorie.itemsToolTipDuration = 10000
        Me.commCategorie.Location = New System.Drawing.Point(75, 60)
        Me.commCategorie.manageText = True
        Me.commCategorie.matchExp = ""
        Me.commCategorie.maximum = 0
        Me.commCategorie.minimum = 0
        Me.commCategorie.Name = "commCategorie"
        Me.commCategorie.nbDecimals = CType(-1, Short)
        Me.commCategorie.onlyAlphabet = False
        Me.commCategorie.pathOfList = ""
        Me.commCategorie.ReadOnly = False
        Me.commCategorie.refuseAccents = False
        Me.commCategorie.refusedChars = ""
        Me.commCategorie.showItemsToolTip = False
        Me.commCategorie.Size = New System.Drawing.Size(477, 22)
        Me.commCategorie.Sorted = True
        Me.commCategorie.TabIndex = 111
        Me.commCategorie.trimText = False
        '
        'commSujet
        '
        Me.commSujet.acceptAlpha = True
        Me.commSujet.acceptedChars = Nothing
        Me.commSujet.acceptNumeric = True
        Me.commSujet.allCapital = False
        Me.commSujet.allLower = False
        Me.commSujet.autoComplete = True
        Me.commSujet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.commSujet.autoSizeDropDown = True
        Me.commSujet.BackColor = System.Drawing.Color.White
        Me.commSujet.blockOnMaximum = False
        Me.commSujet.blockOnMinimum = False
        Me.commSujet.cb_AcceptLeftZeros = False
        Me.commSujet.cb_AcceptNegative = False
        Me.commSujet.currencyBox = False
        Me.commSujet.dbField = "CommSubjects.CommSubject"
        Me.commSujet.doComboDelete = True
        Me.commSujet.firstLetterCapital = True
        Me.commSujet.firstLettersCapital = False
        Me.commSujet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commSujet.IntegralHeight = False
        Me.commSujet.itemsToolTipDuration = 10000
        Me.commSujet.Location = New System.Drawing.Point(75, 88)
        Me.commSujet.manageText = True
        Me.commSujet.matchExp = ""
        Me.commSujet.maximum = 0
        Me.commSujet.minimum = 0
        Me.commSujet.Name = "commSujet"
        Me.commSujet.nbDecimals = CType(-1, Short)
        Me.commSujet.onlyAlphabet = False
        Me.commSujet.pathOfList = ""
        Me.commSujet.ReadOnly = False
        Me.commSujet.refuseAccents = False
        Me.commSujet.refusedChars = ""
        Me.commSujet.showItemsToolTip = False
        Me.commSujet.Size = New System.Drawing.Size(477, 22)
        Me.commSujet.Sorted = True
        Me.commSujet.TabIndex = 111
        Me.commSujet.trimText = False
        '
        'commDeA
        '
        Me.commDeA.acceptAlpha = True
        Me.commDeA.acceptedChars = Nothing
        Me.commDeA.acceptNumeric = True
        Me.commDeA.allCapital = False
        Me.commDeA.allLower = False
        Me.commDeA.autoComplete = True
        Me.commDeA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.commDeA.autoSizeDropDown = True
        Me.commDeA.BackColor = System.Drawing.Color.White
        Me.commDeA.blockOnMaximum = False
        Me.commDeA.blockOnMinimum = False
        Me.commDeA.cb_AcceptLeftZeros = False
        Me.commDeA.cb_AcceptNegative = False
        Me.commDeA.currencyBox = False
        Me.commDeA.dbField = Nothing
        Me.commDeA.doComboDelete = True
        Me.commDeA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.commDeA.firstLetterCapital = False
        Me.commDeA.firstLettersCapital = False
        Me.commDeA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commDeA.IntegralHeight = False
        Me.commDeA.itemsToolTipDuration = 10000
        Me.commDeA.Location = New System.Drawing.Point(75, 32)
        Me.commDeA.manageText = True
        Me.commDeA.matchExp = Nothing
        Me.commDeA.maximum = 0
        Me.commDeA.minimum = 0
        Me.commDeA.Name = "commDeA"
        Me.commDeA.nbDecimals = CType(-1, Short)
        Me.commDeA.onlyAlphabet = False
        Me.commDeA.pathOfList = ""
        Me.commDeA.ReadOnly = False
        Me.commDeA.refuseAccents = False
        Me.commDeA.refusedChars = Nothing
        Me.commDeA.showItemsToolTip = False
        Me.commDeA.Size = New System.Drawing.Size(445, 22)
        Me.commDeA.Sorted = True
        Me.commDeA.TabIndex = 109
        Me.commDeA.trimText = False
        '
        'label25
        '
        Me.label25.AutoSize = True
        Me.label25.BackColor = System.Drawing.SystemColors.Control
        Me.label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.label25.Font = New System.Drawing.Font("Arial", 8.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label25.Location = New System.Drawing.Point(8, 235)
        Me.label25.Name = "label25"
        Me.label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label25.Size = New System.Drawing.Size(155, 13)
        Me.label25.TabIndex = 67
        Me.label25.Text = "Liste des communications :"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.SystemColors.Control
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(8, 63)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(61, 14)
        Me.label5.TabIndex = 66
        Me.label5.Text = "Catégorie :"
        '
        'label23
        '
        Me.label23.AutoSize = True
        Me.label23.BackColor = System.Drawing.SystemColors.Control
        Me.label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.label23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label23.Location = New System.Drawing.Point(8, 91)
        Me.label23.Name = "label23"
        Me.label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label23.Size = New System.Drawing.Size(41, 14)
        Me.label23.TabIndex = 66
        Me.label23.Text = "Objet :"
        '
        'lblCommDeA
        '
        Me.lblCommDeA.AutoSize = True
        Me.lblCommDeA.BackColor = System.Drawing.SystemColors.Control
        Me.lblCommDeA.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCommDeA.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommDeA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCommDeA.Location = New System.Drawing.Point(8, 32)
        Me.lblCommDeA.Name = "lblCommDeA"
        Me.lblCommDeA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCommDeA.Size = New System.Drawing.Size(43, 14)
        Me.lblCommDeA.TabIndex = 65
        Me.lblCommDeA.Text = "À | De :"
        '
        '_ongletskp_TabPage4
        '
        Me._ongletskp_TabPage4.Controls.Add(Me.facturesView)
        Me._ongletskp_TabPage4.Controls.Add(Me.groupBox1)
        Me._ongletskp_TabPage4.Controls.Add(Me.label31)
        Me._ongletskp_TabPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ongletskp_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._ongletskp_TabPage4.Name = "_ongletskp_TabPage4"
        Me._ongletskp_TabPage4.Size = New System.Drawing.Size(560, 462)
        Me._ongletskp_TabPage4.TabIndex = 4
        Me._ongletskp_TabPage4.Text = "Comptabilité"
        '
        'facturesView
        '
        Me.facturesView.AllowUserToAddRows = False
        Me.facturesView.AllowUserToDeleteRows = False
        Me.facturesView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.facturesView.autoSelectOnDataSourceChanged = True
        Me.facturesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.facturesView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.facturesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.facturesView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DateF, Me.TF, Me.MF, Me.MP, Me.PC, Me.PPO, Me.PU, Me.EL, Me.NoFacture})
        Me.facturesView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.facturesView.isDoubleBuffered = False
        Me.facturesView.Location = New System.Drawing.Point(0, 57)
        Me.facturesView.Name = "facturesView"
        Me.facturesView.ReadOnly = True
        Me.facturesView.RowHeadersVisible = False
        Me.facturesView.RowHeadersWidth = 20
        Me.facturesView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.facturesView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.facturesView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.facturesView.Size = New System.Drawing.Size(560, 273)
        Me.facturesView.TabIndex = 282
        '
        'DateF
        '
        Me.DateF.DataPropertyName = "DF"
        DataGridViewCellStyle4.Format = "d"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DateF.DefaultCellStyle = DataGridViewCellStyle4
        Me.DateF.HeaderText = "Date facture"
        Me.DateF.Name = "DateF"
        Me.DateF.ReadOnly = True
        '
        'TF
        '
        Me.TF.DataPropertyName = "TF"
        Me.TF.HeaderText = "Type facture"
        Me.TF.Name = "TF"
        Me.TF.ReadOnly = True
        '
        'MF
        '
        Me.MF.DataPropertyName = "MF"
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.MF.DefaultCellStyle = DataGridViewCellStyle5
        Me.MF.HeaderText = "Montant facturé"
        Me.MF.Name = "MF"
        Me.MF.ReadOnly = True
        '
        'MP
        '
        Me.MP.DataPropertyName = "MP"
        DataGridViewCellStyle6.Format = "C2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.MP.DefaultCellStyle = DataGridViewCellStyle6
        Me.MP.HeaderText = "Montant payé"
        Me.MP.Name = "MP"
        Me.MP.ReadOnly = True
        '
        'PC
        '
        Me.PC.DataPropertyName = "PC"
        Me.PC.HeaderText = "Payeur client"
        Me.PC.Name = "PC"
        Me.PC.ReadOnly = True
        '
        'PPO
        '
        Me.PPO.DataPropertyName = "PPO"
        Me.PPO.HeaderText = "Payeur P/O"
        Me.PPO.Name = "PPO"
        Me.PPO.ReadOnly = True
        Me.PPO.Visible = False
        '
        'PU
        '
        Me.PU.DataPropertyName = "PU"
        Me.PU.HeaderText = "Payeur utilisateur"
        Me.PU.Name = "PU"
        Me.PU.ReadOnly = True
        Me.PU.Visible = False
        '
        'EL
        '
        Me.EL.DataPropertyName = "EL"
        Me.EL.HeaderText = "Entité liée"
        Me.EL.Name = "EL"
        Me.EL.ReadOnly = True
        Me.EL.Visible = False
        '
        'NoFacture
        '
        Me.NoFacture.DataPropertyName = "NoFacture"
        Me.NoFacture.HeaderText = "# Facture"
        Me.NoFacture.Name = "NoFacture"
        Me.NoFacture.ReadOnly = True
        Me.NoFacture.Visible = False
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.filterBills)
        Me.groupBox1.Controls.Add(Me.dateA)
        Me.groupBox1.Controls.Add(Me.dateDe)
        Me.groupBox1.Controls.Add(Me.choixA)
        Me.groupBox1.Controls.Add(Me.dateAll)
        Me.groupBox1.Controls.Add(Me.choixDe)
        Me.groupBox1.Location = New System.Drawing.Point(0, 0)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(520, 40)
        Me.groupBox1.TabIndex = 148
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Filtrage"
        '
        'dateA
        '
        Me.dateA.AutoSize = True
        Me.dateA.BackColor = System.Drawing.Color.Transparent
        Me.dateA.Location = New System.Drawing.Point(264, 16)
        Me.dateA.Name = "dateA"
        Me.dateA.Size = New System.Drawing.Size(0, 14)
        Me.dateA.TabIndex = 5
        '
        'dateDe
        '
        Me.dateDe.AutoSize = True
        Me.dateDe.BackColor = System.Drawing.Color.Transparent
        Me.dateDe.Location = New System.Drawing.Point(151, 16)
        Me.dateDe.Name = "dateDe"
        Me.dateDe.Size = New System.Drawing.Size(0, 14)
        Me.dateDe.TabIndex = 4
        '
        'choixA
        '
        Me.choixA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.choixA.Location = New System.Drawing.Point(224, 12)
        Me.choixA.Name = "choixA"
        Me.choixA.Size = New System.Drawing.Size(32, 20)
        Me.choixA.TabIndex = 3
        Me.choixA.Text = "A"
        '
        'dateAll
        '
        Me.dateAll.AutoSize = True
        Me.dateAll.BackColor = System.Drawing.Color.Transparent
        Me.dateAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateAll.Location = New System.Drawing.Point(8, 16)
        Me.dateAll.Name = "dateAll"
        Me.dateAll.Size = New System.Drawing.Size(104, 17)
        Me.dateAll.TabIndex = 2
        Me.dateAll.Text = "Toutes les dates"
        Me.dateAll.UseVisualStyleBackColor = False
        '
        'choixDe
        '
        Me.choixDe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.choixDe.Location = New System.Drawing.Point(115, 12)
        Me.choixDe.Name = "choixDe"
        Me.choixDe.Size = New System.Drawing.Size(32, 20)
        Me.choixDe.TabIndex = 1
        Me.choixDe.Text = "De"
        '
        'label31
        '
        Me.label31.AutoSize = True
        Me.label31.BackColor = System.Drawing.SystemColors.Control
        Me.label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label31.Location = New System.Drawing.Point(0, 40)
        Me.label31.Name = "label31"
        Me.label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label31.Size = New System.Drawing.Size(229, 14)
        Me.label31.TabIndex = 147
        Me.label31.Text = "Modification des factures et paiements :"
        '
        'menuviewmodifkpcommunications
        '
        Me.menuviewmodifkpcommunications.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuImportFromOutside, Me.menuImportFromDB})
        '
        'menuImportFromOutside
        '
        Me.menuImportFromOutside.Index = 0
        Me.menuImportFromOutside.Text = "De l'extérieur du logiciel"
        '
        'menuImportFromDB
        '
        Me.menuImportFromDB.Index = 1
        Me.menuImportFromDB.Text = "De la banque de données"
        '
        'menuViewModifKP
        '
        Me.menuViewModifKP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuComm})
        Me.menuViewModifKP.Location = New System.Drawing.Point(0, 0)
        Me.menuViewModifKP.Name = "menuViewModifKP"
        Me.menuViewModifKP.Size = New System.Drawing.Size(904, 24)
        Me.menuViewModifKP.TabIndex = 194
        Me.menuViewModifKP.Text = "MenuStrip1"
        Me.menuViewModifKP.Visible = False
        '
        'menuComm
        '
        Me.menuComm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.enregistrerToolStripMenuItem, Me.importerToolStripMenuItem, Me.ouvrirLeFichierJointToolStripMenuItem, Me.supprimerLeFichierJointToolStripMenuItem, Me.supprimerToolStripMenuItem})
        Me.menuComm.Name = "menuComm"
        Me.menuComm.Size = New System.Drawing.Size(122, 20)
        Me.menuComm.Text = "menuContextMenu"
        Me.menuComm.Visible = False
        '
        'enregistrerToolStripMenuItem
        '
        Me.enregistrerToolStripMenuItem.Name = "enregistrerToolStripMenuItem"
        Me.enregistrerToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.enregistrerToolStripMenuItem.Text = "Enregistrer"
        '
        'importerToolStripMenuItem
        '
        Me.importerToolStripMenuItem.Name = "importerToolStripMenuItem"
        Me.importerToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.importerToolStripMenuItem.Text = "Importer"
        '
        'ouvrirLeFichierJointToolStripMenuItem
        '
        Me.ouvrirLeFichierJointToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ouvrirLeFichierJointToolStripMenuItem.Name = "ouvrirLeFichierJointToolStripMenuItem"
        Me.ouvrirLeFichierJointToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.ouvrirLeFichierJointToolStripMenuItem.Text = "Ouvrir le fichier joint"
        '
        'supprimerLeFichierJointToolStripMenuItem
        '
        Me.supprimerLeFichierJointToolStripMenuItem.Name = "supprimerLeFichierJointToolStripMenuItem"
        Me.supprimerLeFichierJointToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.supprimerLeFichierJointToolStripMenuItem.Text = "Supprimer le fichier joint"
        '
        'supprimerToolStripMenuItem
        '
        Me.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem"
        Me.supprimerToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.supprimerToolStripMenuItem.Text = "Supprimer"
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
        Me.publipostage.IntegralHeight = False
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
        Me.publipostage.pathOfList = ""
        Me.publipostage.ReadOnly = False
        Me.publipostage.refuseAccents = False
        Me.publipostage.refusedChars = Nothing
        Me.publipostage.showItemsToolTip = False
        Me.publipostage.Size = New System.Drawing.Size(208, 22)
        Me.publipostage.TabIndex = 195
        Me.publipostage.trimText = False
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label21.Location = New System.Drawing.Point(8, 313)
        Me.label21.Name = "label21"
        Me.label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label21.Size = New System.Drawing.Size(85, 14)
        Me.label21.TabIndex = 196
        Me.label21.Text = "Publipostage :"
        '
        'viewmodifKeyPeople
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 13)
        Me.ClientSize = New System.Drawing.Size(904, 487)
        Me.Controls.Add(Me.publipostage)
        Me.Controls.Add(Me.label21)
        Me.Controls.Add(Me.ainfo)
        Me.Controls.Add(Me.addAlert)
        Me.Controls.Add(Me.createBill)
        Me.Controls.Add(Me.paiements)
        Me.Controls.Add(Me.infoNom)
        Me.Controls.Add(Me.codepostal2)
        Me.Controls.Add(Me.noident)
        Me.Controls.Add(Me.startStopCommChanging)
        Me.Controls.Add(Me.startStopBillChanging)
        Me.Controls.Add(Me.ongletskp)
        Me.Controls.Add(Me.employeur)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.reference)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me._Labels_7)
        Me.Controls.Add(Me._Labels_6)
        Me.Controls.Add(Me._Labels_5)
        Me.Controls.Add(Me._Labels_4)
        Me.Controls.Add(Me._Labels_3)
        Me.Controls.Add(Me._Label2_2)
        Me.Controls.Add(Me._Labels_2)
        Me.Controls.Add(Me._Labels_1)
        Me.Controls.Add(Me._Labels_0)
        Me.Controls.Add(Me.workPlace)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.downTel)
        Me.Controls.Add(Me.upTel)
        Me.Controls.Add(Me.addTel)
        Me.Controls.Add(Me.modifTel)
        Me.Controls.Add(Me.delTel)
        Me.Controls.Add(Me.telephones)
        Me.Controls.Add(Me.adminbutton)
        Me.Controls.Add(Me.ville)
        Me.Controls.Add(Me.categorie)
        Me.Controls.Add(Me.url)
        Me.Controls.Add(Me.courriel)
        Me.Controls.Add(Me.adresse)
        Me.Controls.Add(Me.codepostal1)
        Me.Controls.Add(Me.nom)
        Me.Controls.Add(Me.getReference)
        Me.Controls.Add(Me.submit)
        Me.Controls.Add(Me.selectWorkPlace)
        Me.Controls.Add(Me.menuViewModifKP)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(420, 330)
        Me.MaximizeBox = False
        Me.Name = "viewmodifKeyPeople"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Ajout d'une personne/organisme clé"
        Me.ongletskp.ResumeLayout(False)
        Me._ongletskp_TabPage3.ResumeLayout(False)
        Me._ongletskp_TabPage3.PerformLayout()
        Me.panel2.ResumeLayout(False)
        Me._ongletskp_TabPage4.ResumeLayout(False)
        Me._ongletskp_TabPage4.PerformLayout()
        CType(Me.facturesView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.menuViewModifKP.ResumeLayout(False)
        Me.menuViewModifKP.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private lastLoadedBill As Integer = 0
    Private _NoKP As Integer
    Private formModified As Boolean = False
    Private commModified As Boolean = False
    Private loadingCompte As Boolean = True
    Private isLoadOnlyList As Boolean = False
    Private imgModifSave As ImageList

#Region "Propriétés"
    Property NoKP() As Integer
        Get
            Return _NoKP
        End Get
        Set(ByVal Value As Integer)
            _NoKP = Value
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

    Private Sub addTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addTel.Click
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

        Dim n As Integer = telephones.Items.Add(newTitle & ":" & newTel)
        telephones.SelectedIndex = n
        formModified = True
    End Sub

    Private Sub telephones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles telephones.SelectedIndexChanged
        If telephones.SelectedIndex <> -1 And addTel.Enabled = True Then
            modifTel.Enabled = True
            delTel.Enabled = True
            If telephones.Items.Count > 1 Then
                If telephones.SelectedIndex = 0 Then
                    upTel.Enabled = False
                Else
                    upTel.Enabled = True
                End If
                If telephones.SelectedIndex = (telephones.Items.Count - 1) Then
                    downTel.Enabled = False
                Else
                    downTel.Enabled = True
                End If
            Else
                upTel.Enabled = False
                downTel.Enabled = False
            End If
        Else
            modifTel.Enabled = False
            delTel.Enabled = False
            upTel.Enabled = False
            downTel.Enabled = False
        End If
    End Sub

    Private Sub modifTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifTel.Click
        Dim myPhone() As String = Split(telephones.GetItemText(telephones.SelectedItem), ":")

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
        telephones.Items(telephones.SelectedIndex) = newTitle & ":" & newTel
        formModified = True
    End Sub

    Private Sub delTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delTel.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce numéro de téléphone ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        telephones.Items.RemoveAt(telephones.SelectedIndex)
        downTel.Enabled = False
        upTel.Enabled = False
        modifTel.Enabled = False
        delTel.Enabled = False
        formModified = True
    End Sub

    Private Sub upTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles upTel.Click
        Dim SPhones(), selItem As String
        Dim curIndex As Integer
        curIndex = telephones.SelectedIndex
        ReDim SPhones(telephones.Items.Count - 1)
        telephones.Items.CopyTo(SPhones, 0)

        selItem = SPhones(curIndex)
        SPhones(curIndex) = SPhones(curIndex - 1)
        SPhones(curIndex - 1) = selItem
        telephones.Items.Clear()
        telephones.Items.AddRange(SPhones)
        telephones.SelectedIndex = curIndex - 1
        formModified = True
    End Sub

    Private Sub downTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles downTel.Click
        Dim SPhones(), selItem As String
        Dim curIndex As Integer
        curIndex = telephones.SelectedIndex
        ReDim SPhones(telephones.Items.Count - 1)
        telephones.Items.CopyTo(SPhones, 0)

        selItem = SPhones(curIndex)
        SPhones(curIndex) = SPhones(curIndex + 1)
        SPhones(curIndex + 1) = selItem
        telephones.Items.Clear()
        telephones.Items.AddRange(SPhones)
        telephones.SelectedIndex = curIndex + 1
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

    Private Sub changeFocusObject(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles codepostal1.KeyUp, codepostal2.KeyUp, telephones.KeyUp, ville.KeyUp, courriel.KeyUp, url.KeyUp, adresse.KeyUp, categorie.KeyUp, noident.KeyUp, employeur.KeyUp, selectWorkPlace.KeyUp, getReference.KeyUp, publipostage.KeyUp
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
        myRecherche.Visible = False
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()
    End Sub

    Private Sub selectWorkPlace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectWorkPlace.Click
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.Visible = False
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            workPlace.Tag = kpChosen.noKP
            workPlace.Text = kpChosen.kpFullName
        End If
    End Sub

    Private Sub nom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles nom.Enter
        If nom.ReadOnly = False Then infoNom.Visible = True
    End Sub

    Private Sub nom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles nom.Leave
        infoNom.Visible = False
    End Sub
#End Region

#Region "Communications Form"

    Private Sub listeCommunications_ItemClick(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs) Handles listeCommunications.itemClick
        If Not (CType(sender, CI.Controls.List).clickEnabled = False Or listeCommunications.selected < 0) And e.button = 2 Then
            listeCommunications_WillSelect(sender, New CI.Controls.List.WillSelectEventArgs(e.selectedItem, 2, e.x, e.y, False))
            menuComm.showDropDown(True)
        End If
    End Sub

    Private Sub listeCommunications_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles listeCommunications.willSelect
        If commModified = True And (modifComm.Enabled = True Or addComm.Enabled = True) Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If savingCommunications() = "" Then Exit Sub
        commModified = False

        Dim trueFalse As Boolean = False
        If modifComm.Enabled Or addComm.Enabled = True Then trueFalse = True

        enregistrerToolStripMenuItem.Enabled = trueFalse
        importerToolStripMenuItem.Enabled = trueFalse
        supprimerLeFichierJointToolStripMenuItem.Enabled = trueFalse
        supprimerToolStripMenuItem.Enabled = trueFalse

        If e.selectedItem >= 0 Then
            Dim myComm() As String = listeCommunications.ItemValueB(e.selectedItem).Split(New Char() {"§"})
            If myComm(9) <> "" Then
                Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
                Dim PathFromApp, myFileName As String
                Dim myType As OpeningType
                Select Case sMyComm(0)
                    Case "DB"
                        myType = CommonProc.OpeningType.DB
                        myFileName = getLastDir(sMyComm(1))
                        PathFromApp = sMyComm(1).Substring(0, sMyComm(1).Length - myFileName.Length - 1)
                    Case "EMAIL"
                        myType = CommonProc.OpeningType.EMAIL
                        myFileName = sMyComm(1)
                        PathFromApp = "KP\" & NoKP & "\Comm\"
                    Case "REPORT"
                        myType = CommonProc.OpeningType.REPORT
                        myFileName = sMyComm(1)
                        PathFromApp = "KP\" & NoKP & "\Comm\"
                    Case "FILE"
                        myType = CommonProc.OpeningType.FILE
                        myFileName = sMyComm(1)
                        PathFromApp = "KP\" & NoKP & "\Comm\"
                End Select

                ouvrirLeFichierJointToolStripMenuItem.Enabled = True
                If sMyComm(0) <> "DB" AndAlso IO.File.Exists(appPath & bar(appPath) & PathFromApp & myFileName) = False Then
                    supprimerLeFichierJointToolStripMenuItem.Enabled = False
                    ouvrirLeFichierJointToolStripMenuItem.Enabled = False
                End If
            Else
                supprimerLeFichierJointToolStripMenuItem.Enabled = False
                ouvrirLeFichierJointToolStripMenuItem.Enabled = False
            End If
        Else
            supprimerLeFichierJointToolStripMenuItem.Enabled = False
            ouvrirLeFichierJointToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub commFiltrages_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles commFiltrage.SelectionChangeCommitted, commFiltrageCat.SelectionChangeCommitted
        If commModified = True And (modifComm.Enabled = True Or addComm.Enabled = True) Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If savingCommunications(False) = "" Then Exit Sub

        loadCommunications(True)
    End Sub

    Private Sub commTypes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles commEnvoie.CheckedChanged, commReception.CheckedChanged, colorReceived.Click, colorSent.Click
        If commModified = True And (modifComm.Enabled = True Or addComm.Enabled = True) Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If savingCommunications(False) = "" Then Exit Sub
        If sender Is colorSent Then commEnvoie.Checked = Not commEnvoie.Checked
        If sender Is colorReceived Then commReception.Checked = Not commReception.Checked

        If commEnvoie.Checked = False And commReception.Checked = False Then
            If sender Is commEnvoie OrElse sender Is colorSent Then
                commReception.Checked = True
            Else
                commEnvoie.Checked = True
            End If
        End If

        If loadingCompte = False Then loadCommunications(True)
    End Sub

    Private Sub listeCommunications_SelectedChange() Handles listeCommunications.selectedChange
        If listeCommunications.selected = -1 Then
            viderComm_Click(listeCommunications, EventArgs.Empty)
        Else
            Dim myComm() As String = listeCommunications.ItemValueB(listeCommunications.selected).Split(New Char() {"§"})

            commType1.Checked = myComm(4)
            commType2.Checked = Not commType1.Checked

            If myComm(3) = 0 Then
                commDeA.SelectedIndex = 0
            Else
                Dim Found, i As Integer
                Found = -1
                For i = 0 To commDeA.Items.Count - 1
                    If CStr(commDeA.Items.Item(i)).IndexOf("(" & myComm(3) & ")") <> -1 Then Found = i : Exit For
                Next i
                If Found = -1 Then
                    commDeA.SelectedIndex = 0
                Else
                    commDeA.SelectedIndex = Found
                End If
            End If

            commSujet.Text = myComm(5)
            commDate.Text = DateFormat.getTextDate(CDate(myComm(6)))
            commUser.Text = UsersManager.getInstance.getUser(myComm(7)).toString()
            commRemarques.Text = myComm(8).Replace("<br>", vbCrLf)

            modifComm.Enabled = False

            If toolTip1.GetToolTip(startStopCommChanging).StartsWith("Commencer") = False Then
                If myComm(9) = "" Then
                    importComm.Enabled = True
                Else
                    Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
                    If sMyComm(0) = "DB" Or sMyComm(0) = "FILE" Then
                        importComm.Enabled = True
                    Else
                        importComm.Enabled = False
                    End If
                End If
                modifComm.Enabled = True
                delComm.Enabled = True
            End If

            commCategorie.Text = myComm(11)

            addComm.Enabled = False
            commType1.Enabled = False
            commType2.Enabled = False
        End If
        commModified = False
    End Sub

    Private Sub selectCommDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectCommDate.Click
        Dim myDateChoice As New DateChoice()
        Dim MyDate As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year - 50, Date.Today.Year + 1, , , , , , , , , , , CDate(commDate.Text))
        If MyDate.Count <> 0 Then
            commDate.Text = DateFormat.getTextDate(MyDate(0))
            commModified = True
        End If
    End Sub

    Private Sub selectKeyPeople_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectKeyPeople.Click
        Dim oldKP As String = commDeA.SelectedText
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.Visible = False
        myKeyPeople.selected = True
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            Dim Found, i As Integer
            Found = -1
            For i = 0 To commDeA.Items.Count - 1
                If commDeA.Items.Item(i).ToString.IndexOf(kpChosen.noKP) <> -1 Then Found = i : Exit For
            Next i
            If Found < 0 Then Found = commDeA.Items.Add(kpChosen.kpFullName & " (" & kpChosen.noKP & ")")
            commDeA.SelectedIndex = Found
        End If
        If oldKP <> commDeA.SelectedText Then commModified = True
    End Sub

    Private Sub commDeA_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles commDeA.SelectedIndexChanged
        If commDeA.SelectedIndex = -1 Then
            addComm.Enabled = False
        Else
            If toolTip1.GetToolTip(modifComm).StartsWith("Commencer") = False Then
                If listeCommunications.selected = -1 Then
                    If commSujet.Text = "" Or (commType1.Checked = False And commType2.Checked = False) Then
                        addComm.Enabled = False
                    Else
                        addComm.Enabled = True
                    End If
                Else
                    If commSujet.Text = "" Then
                        modifComm.Enabled = False
                    Else
                        modifComm.Enabled = True
                    End If
                End If
            End If
            commModified = True
        End If
    End Sub

    Private Sub commSujet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles commSujet.TextChanged
        If commSujet.Text = "" Then
            addComm.Enabled = False
        Else
            If toolTip1.GetToolTip(modifComm).StartsWith("Commencer") = False Then
                If listeCommunications.selected = -1 Then
                    If commDeA.SelectedIndex = -1 Or (commType1.Checked = False And commType2.Checked = False) Then
                        addComm.Enabled = False
                    Else
                        addComm.Enabled = True
                    End If
                Else
                    If commDeA.SelectedIndex = -1 Then
                        modifComm.Enabled = False
                    Else
                        modifComm.Enabled = True
                    End If
                End If
            End If
        End If
        commModified = True
    End Sub

    Private Sub commRemarques_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles commRemarques.TextChanged
        commModified = True
    End Sub

    Private Sub commType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles commType1.CheckedChanged, commType2.CheckedChanged
        commModified = True
        If toolTip1.GetToolTip(modifComm).StartsWith("Commencer") = False Then
            If listeCommunications.selected = -1 Then
                If commSujet.Text = "" Or commDeA.SelectedIndex = -1 Then
                    addComm.Enabled = False
                Else
                    addComm.Enabled = True
                End If
            Else
                If commSujet.Text = "" Or commDeA.SelectedIndex = -1 Then
                    modifComm.Enabled = False
                Else
                    modifComm.Enabled = True
                End If
            End If
        End If

        If commType1.Checked Then
            lblCommDeA.Text = "À :"
        Else
            lblCommDeA.Text = "De :"
        End If
        commModified = True
    End Sub
#End Region

#Region "Communications Actions"
    Private Sub viderComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles viderComm.Click
        If commModified = True And (modifComm.Enabled = True Or addComm.Enabled = True) Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If savingCommunications() = "" Then Exit Sub

        commType1.Checked = False
        commType2.Checked = False
        lblCommDeA.Text = "À | De :"
        commDeA.SelectedIndex = -1
        commSujet.Text = ""
        commDate.Text = DateFormat.getTextDate(Date.Today)
        commRemarques.Text = ""
        commCategorie.Text = ""
        commUser.Text = UsersManager.currentUser.toString()
        If listeCommunications.selected <> -1 Then commModified = False : listeCommunications.selected = -1

        'Activation/Désactivation
        If toolTip1.GetToolTip(startStopCommChanging).StartsWith("Commencer") = False Then
            selectKeyPeople.Enabled = True
            selectCommDate.Enabled = True
            commType1.Enabled = True
            commType2.Enabled = True
        End If
        addComm.Enabled = False
        importComm.Enabled = False
        modifComm.Enabled = False
        delComm.Enabled = False
        commModified = False
    End Sub

    Private Sub addComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addComm.Click
        If commType1.Checked = False And commType2.Checked = False Then MessageBox.Show("Veuillez sélectionner Envoi ou Réception", "Information manquante") : Exit Sub

        savingCommunications()
    End Sub

    Private Sub importComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles importComm.Click
        If commType1.Checked = True Then
            menuviewmodifkpcommunications.Show(importComm, New Point(0, 0))
        Else
            commImporter()
        End If
    End Sub

    Private Sub modifComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifComm.Click
        savingCommunications()
    End Sub

    Private Sub delComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delComm.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette communication ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim myComm() As String = listeCommunications.ItemValueB(listeCommunications.selected).Split(New Char() {"§"})

        'Delete attached file or break if it is in use
        If myComm(9) <> "" Then
            Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)

            If sMyComm(0) = "FILE" AndAlso IO.File.Exists(appPath & bar(appPath) & "KP\" & NoKP & "\Comm\" & sMyComm(1)) Then
                If fileInUse(appPath & bar(appPath) & "KP\" & NoKP & "\Comm\" & sMyComm(1)) Then
                    MessageBox.Show("Impossible du supprimer la communication, car le fichier attaché est en cours d'utilisation", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Exit Sub
                End If
                IO.File.Delete(appPath & bar(appPath) & "KP\" & NoKP & "\Comm\" & sMyComm(1))
            End If
        End If

        DBLinker.getInstance.delDB("CommunicationsKP", "NoCommunication", listeCommunications.ItemValueA(listeCommunications.selected), False)

        InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunicationsKP(" & NoKP & "," & False & ")")
        myMainWin.StatusText = "Compte personne / organisme clé " & NoKP & " : Communication supprimée"
    End Sub

    Private Sub menuImportFromDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuImportFromDB.Click
        Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB())
        mySearchDB.Visible = False
        mySearchDB.from = commDate
        mySearchDB.selectedCat = "Généraux"
        mySearchDB.useWinAsSelection = True
        mySearchDB.MdiParent = Nothing
        mySearchDB.StartPosition = FormStartPosition.CenterScreen
        mySearchDB.ShowDialog()
    End Sub

    Private Sub menuImportFromOutside_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuImportFromOutside.Click
        commImporter()
    End Sub

    Private Sub listeCommunications_DblClick(ByVal sender As System.Object, ByVal e As CI.Controls.List.DblClickEventArgs) Handles listeCommunications.dblClick
        If e.selectedItem < 0 Or listeCommunications.selected < 0 Or e.button <> 1 Then Exit Sub

        Dim lectureSeule As Boolean = False
        If toolTip1.GetToolTip(modifComm).StartsWith("C") Then lectureSeule = True
        Dim myComm() As String = listeCommunications.ItemValueB(e.selectedItem).Split(New Char() {"§"})
        If myComm(9) = "" Then Exit Sub

        Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
        Dim fullPath As String = ""
        Select Case sMyComm(0)
            Case "DB"
                fullPath = "db:\" & sMyComm(1)
            Case Else
                fullPath = appPath & bar(appPath) & "KP\" & NoKP & "\Comm\" & "\" & sMyComm(1)
                If IO.File.Exists(fullPath) = False Then
                    MessageBox.Show("Le fichier demandé n'existe plus. Veuillez importer de nouveau.", "Fichier importé inexistant", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
        End Select

        TypesFilesOpener.getInstance.open(fullPath, Nothing)
    End Sub

    Private Sub listeCommunications_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles listeCommunications.keyUp
        If e.KeyCode = 13 Then e.SuppressKeyPress = True : listeCommunications_DblClick(sender, New CI.Controls.List.DblClickEventArgs(listeCommunications.selected, 1, 0, 0))
    End Sub

    Private Sub enregistrerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles enregistrerToolStripMenuItem.Click
        savingCommunications()
    End Sub

    Private Sub importerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles importerToolStripMenuItem.Click
        importComm_Click(sender, e)
    End Sub

    Private Sub supprimerLeFichierJointToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles supprimerLeFichierJointToolStripMenuItem.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer le fichier joint à cette communication ?", "Suppression d'un fichier joint", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim myComm() As String = listeCommunications.ItemValueB(listeCommunications.selected).Split(New Char() {"§"})
        If myComm(9) = "" Then Exit Sub

        Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
        If sMyComm(0).ToUpper = "FILE" Then
            Dim myFileToDel As String = appPath & bar(appPath) & "KP\" & NoKP & "\Comm\" & sMyComm(1)
            If IO.File.Exists(myFileToDel) Then IO.File.Delete(myFileToDel)
        End If

        Dim noCommunication As Integer = myComm(1)
        DBLinker.getInstance.updateDB("CommunicationsKP", "NameOfFile=''", "NoCommunication", noCommunication, False)
        InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunicationsKP(" & NoKP & "," & True & ")")
    End Sub

    Private Sub supprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles supprimerToolStripMenuItem.Click
        delComm_Click(sender, e)
    End Sub

    Private Sub ouvrirLeFichierJointToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ouvrirLeFichierJointToolStripMenuItem.Click
        listeCommunications_DblClick(sender, New CI.Controls.List.DblClickEventArgs(listeCommunications.selected, 1, 0, 0))
    End Sub
#End Region

    Private Sub choixFiltrage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles choixA.Click, choixDe.Click
        Dim CurDate, scd() As String
        Dim myDate As Date = Nothing
        If sender.name.tolower = "choixde" Then
            CurDate = dateDe.Text
        Else
            CurDate = dateA.Text
        End If
        If CurDate <> "" Then
            scd = CurDate.Split(New Char() {"/"})
            myDate = New Date(scd(0), scd(1), scd(2))
        Else
            myDate = Date.Today
        End If
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year - 10, Date.Today.Year + 1, , , , , , True, , , , , myDate)
        If dateReturn.Count = 0 Then Exit Sub

        Dim dateReturnString As String = DateFormat.getTextDate(dateReturn(0))
        If sender.name.tolower = "choixde" Then
            dateDe.Text = dateReturnString
            If dateA.Text = "" Then
                dateA.Text = dateReturnString
            Else
                If date1Infdate2(CDate(dateA.Text), dateReturn(0)) Then dateA.Text = dateReturnString
            End If
        Else
            If dateDe.Text = "" Then
                dateDe.Text = dateReturnString
            Else
                If date1Infdate2(dateReturn(0), CDate(dateDe.Text)) Then dateDe.Text = dateReturnString
            End If
            dateA.Text = dateReturnString
        End If
    End Sub

    Private Sub filterBills_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles filterBills.Click
        loadBills()
    End Sub

    Private Sub commImporter()
        Dim ImportFile, MyPath, NewFileName, MyComm(), sfn() As String, OldFileName As String = ""
        Dim haveToDelete As Boolean = False

        MyPath = "KP\" & NoKP & "\Comm"
        If IO.Directory.Exists(appPath & bar(appPath) & MyPath) = False Then IO.Directory.CreateDirectory(appPath & bar(appPath) & MyPath)

        MyComm = CStr(listeCommunications.ItemValueB(listeCommunications.selected)).Split(New Char() {"§"})
        If MyComm(9) <> "" AndAlso MyComm(9).StartsWith("DB|") = False Then
            OldFileName = MyComm(9).Split(New Char() {"|"})(1)
            sfn = OldFileName.Split(New Char() {"."})
            NewFileName = sfn(0)
        Else
            haveToDelete = True
            NewFileName = Directory.getNewFileName(appPath & bar(appPath) & MyPath, listeCommunications.ItemValueA(listeCommunications.selected))
        End If
        ImportFile = importer(NewFileName, MyPath, 3, , OldFileName)
        If haveToDelete AndAlso IO.File.Exists(appPath & bar(appPath) & MyPath & "\" & NewFileName) Then IO.File.Delete(appPath & bar(appPath) & MyPath & "\" & NewFileName)

        If ImportFile = "" Then Exit Sub

        Dim noCommunication As Integer = 0
        If listeCommunications.selected <> -1 Then noCommunication = listeCommunications.ItemValueA(listeCommunications.selected)
        If noCommunication = 0 Then
            MessageBox.Show("Impossible de lier l'item de la banque de données. Veuillez fermer le compte P/O, le rouvrir et recommencer. Merci!", "Erreur de communication")
            Exit Sub
        End If

        MyComm(9) = "FILE|" & ImportFile
        listeCommunications.ItemValueB(listeCommunications.selected) = String.Join("§", MyComm)
        DBLinker.getInstance.updateDB("CommunicationsKP", "NameOfFile='FILE|" & ImportFile.Replace("'", "''") & "'", "NoCommunication", noCommunication, False)
        InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunicationsKP(" & NoKP & "," & True & ")")
        myMainWin.StatusText = "Compte personne / organisme clé " & NoKP & " : Importation pour la communication effectuée"
    End Sub

    Private Sub adminButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adminbutton.Click
        nom.Text = "Boivin,Jonathan"
        categorie.Text = "Personne:Programmeur"
        adresse.Text = "15 Archambault"
        ville.Text = "Charlemagne"
        codepostal1.Text = "J5Z"
        codepostal2.Text = "1Z9"
        telephones.Items.Add("Maison:555-5555")
        ainfo.Text = "Test de personne clé"
    End Sub

    Private Sub viewmodifKeyPeople_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub viewmodifKeyPeople_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        If toolTip1.GetToolTip(submit).StartsWith("Enregistrer") Then lockSecteur("KPGenInfo-" & NoKP & "-", False, "Personne / organisme clé")
        If toolTip1.GetToolTip(startStopBillChanging).StartsWith("Arrêter") Then
            For i As Integer = 0 To currentLocks.Count - 1
                lockSecteur(currentLocks(i), False, "Personne / organisme clé")
            Next i
        End If
        If toolTip1.GetToolTip(startStopCommChanging).StartsWith("Arrêter") Then lockSecteur("KPComm-" & NoKP & "-", False, "Personne / organisme clé")
    End Sub

    Private Sub submit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles submit.Click
        'Droit & Accès
        If currentDroitAcces(22) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier les informations de base." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If toolTip1.GetToolTip(submit).StartsWith("Modifier") Then
            If lockSecteur("KPGenInfo-" & NoKP & "-", True, "Personne / organisme clé") Then
                lockItems(False)
                telephones_SelectedIndexChanged(eventSender, eventArgs)

                Me.submit.Image = imgModifSave.Images(1)
                toolTip1.SetToolTip(submit, "Enregistrer les informations de base de la personne/organisme clé")
            End If
        Else
            If saving() <> "" Then
                lockItems(True)
                telephones_SelectedIndexChanged(eventSender, eventArgs)

                Me.submit.Image = imgModifSave.Images(0)
                toolTip1.SetToolTip(submit, "Modifier les informations de base de la personne/organisme clé")
                lockSecteur("KPGenInfo-" & NoKP & "-", False, "Personne / organisme clé")
            End If
        End If
    End Sub

    Private Function saving() As String
        Dim i As Integer
        Dim noCat As Object = "null"
        Dim CurCat, TCat(), Phones, sPhones() As String

        If PreferencesManager.getGeneralPreferences()("COPOC1") And nom.Text = "" Then MessageBox.Show("Veuillez entrer un nom", "Entrée manquante") : nom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC2") And categorie.Text = "" Then MessageBox.Show("Veuillez entrer une catégorie", "Entrée manquante") : categorie.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC3") And adresse.Text = "" Then MessageBox.Show("Veuillez entrer une adresse", "Entrée manquante") : adresse.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC4") And ville.Text = "" Then MessageBox.Show("Veuillez entrer une ville", "Entrée manquante") : ville.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC5") And (codepostal1.Text = "" Or codepostal2.Text = "" Or codepostal1.Text.Length < 3 Or codepostal2.Text.Length < 3) Then MessageBox.Show("Veuillez entrer un code postal complet", "Entrée manquante") : codepostal1.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC6") And telephones.Items.Count = 0 Then MessageBox.Show("Veuillez entrer au moins un numéro de téléphone", "Entrée manquante") : addTel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC7") And noident.Text = "" Then MessageBox.Show("Veuillez entrer un numéro identifiant", "Entrée manquante") : noident.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC10") And ainfo.Text = "" Then MessageBox.Show("Veuillez entrer d'autres informations", "Entrée manquante") : ainfo.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC8") And courriel.Text = "" Then MessageBox.Show("Veuillez entrer une adresse de courriel", "Entrée manquante") : courriel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC9") And url.Text = "" Then MessageBox.Show("Veuillez entrer une adresse de site internet", "Entrée manquante") : url.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COPOC11") And employeur.Text = "" Then MessageBox.Show("Veuillez entrer un employeur", "Entrée manquante") : employeur.Focus() : Exit Function
        If courriel.Text <> "" And courriel.Text.IndexOf("@") < 0 Then MessageBox.Show("Veuillez vous assurez que l'adresse de courriel soit entrée correctement :" & vbCrLf & "alias@domaine.extension" & vbCrLf & "Exemple : jonathan@cints.net", "Entrée erronée") : courriel.Focus() : Exit Function
        If publipostage.SelectedIndex = 2 And courriel.Text = "" Then MessageBox.Show("Le champ 'Courriel' est obligatoire lorsque le publipostage est envoyé par courriel", "Information manquante") : courriel.Focus() : Exit Function

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        'Save catégories
        TCat = categorie.Text.Split(New Char() {":"})

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

        ReDim sPhones(telephones.Items.Count - 1)
        telephones.Items.CopyTo(sPhones, 0)
        Phones = String.Join("§", sPhones)
        If Phones Is Nothing Then Phones = ""

        Dim workPlaceNo As String = ""
        If Not workPlace.Tag Is Nothing Then workPlaceNo = workPlace.Tag & "<br>" & workPlace.Text.Replace("'", "''")

        DBLinker.getInstance.updateDB("KeyPeople", " Nom='" & nom.Text.Replace("'", "''") & "', NoCategorie=" & noCat & ", Adresse='" & adresse.Text.Replace("'", "''") & "', NoVille=" & DBHelper.addItemToADBList("Villes", "NomVille", ville.Text, "NoVille") & ", CodePostal='" & codepostal1.Text & codepostal2.Text & "', Telephones='" & Phones.Replace("'", "''") & "', NoRef='" & noident.Text.Replace("'", "''") & "', Courriel='" & courriel.Text & "', URL='" & url.Text.Replace("'", "''") & "', NoClient=" & IIf(reference.Tag.ToString = "0" OrElse reference.Tag.ToString = "", "null", reference.Tag) & ", AutreInfos='" & ainfo.Text.Replace("'", "''") & "', NoEmployeur=" & DBHelper.addItemToADBList("Employeurs", "Employeur", employeur.Text, "NoEmployeur") & ", NoUser=" & ConnectionsManager.currentUser & ", DateHeureCreation='" & DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.ShortTime) & "', WorkPlace='" & workPlaceNo & "', Publipostage=" & publipostage.SelectedIndex, "NoKP", NoKP, False)

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        myMainWin.StatusText = "Modification de la personne/organisme clé : " & nom.Text

        InternalUpdatesManager.getInstance.sendUpdate("AccountsGenInfoKP(" & NoKP & ")")

        formModified = False
        Return "DONE"
    End Function

    Private Sub viewmodifKeyPeople_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If submit.Enabled = True Then
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If saving() = "" Then e.Cancel = True
        End If
    End Sub

    Private Sub viewmodifKeyPeople_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If submit.Enabled = True Then
            If formModified = True Then saving()
        End If
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        telephones.BackColor = IIf(trueFalse, SystemColors.ControlLight, Color.White)
        nom.ReadOnly = trueFalse
        categorie.ReadOnly = trueFalse
        adresse.ReadOnly = trueFalse
        ville.ReadOnly = trueFalse
        codepostal1.ReadOnly = trueFalse
        codepostal2.ReadOnly = trueFalse
        addTel.Enabled = Not trueFalse
        courriel.ReadOnly = trueFalse
        url.ReadOnly = trueFalse
        noident.ReadOnly = trueFalse
        getReference.Enabled = Not trueFalse
        ainfo.ReadOnly = trueFalse
        selectWorkPlace.Enabled = Not trueFalse
        employeur.ReadOnly = trueFalse
        publipostage.ReadOnly = trueFalse
    End Sub

    Private Sub lockCommunications(ByVal trueFalse As Boolean)
        commCategorie.ReadOnly = trueFalse
        commType1.Enabled = Not trueFalse
        commType2.Enabled = Not trueFalse
        commDeA.ReadOnly = trueFalse
        selectKeyPeople.Enabled = Not trueFalse
        commSujet.ReadOnly = trueFalse
        selectCommDate.Enabled = Not trueFalse
        commRemarques.ReadOnly = trueFalse
        addComm.Enabled = Not trueFalse
        importComm.Enabled = Not trueFalse
        modifComm.Enabled = Not trueFalse
        delComm.Enabled = Not trueFalse
        viderComm.Enabled = Not trueFalse
        listeCommunications_WillSelect(listeCommunications, New CI.Controls.List.WillSelectEventArgs(listeCommunications.selected, 0, 0, 0, False))
        listeCommunications_SelectedChange()
    End Sub

    Private Sub commSaveLoad(Optional ByVal adding As Boolean = True, Optional ByVal loadList As Boolean = True)
        commModified = False
        Dim SMyNoKP(), sMyNoUser() As String
        Dim MyNoKP, myNoUser As Integer

        If commDeA.Text.StartsWith("* Personne / Organisme *") = False Then
            SMyNoKP = commDeA.Text.Split(New Char() {"("}, 2)
            MyNoKP = SMyNoKP(1).Substring(0, SMyNoKP(1).Length - 1)
            DBHelper.addItemToADBList("CommDeAKP", "NoKPDeA", MyNoKP, "NoDeA", , , , False, "NoKP", NoKP)
        End If
        sMyNoUser = commUser.Text.Split(New Char() {"("})
        myNoUser = sMyNoUser(1).Substring(0, sMyNoUser(1).Length - 1)

        If adding = True Then
            Dim noComm As Integer = addingCommKP(NoKP, MyNoKP, commType1.Checked, commCategorie.Text, commSujet.Text, CDate(commDate.Text), commRemarques.Text)
            listeCommunications.selected = listeCommunications.findStringExact(noComm, , CI.Controls.List.FindingType.ValueA)
        Else
            Dim noCommunication As Integer = 0
            If listeCommunications.selected <> -1 Then noCommunication = listeCommunications.ItemValueA(listeCommunications.selected)
            If noCommunication = 0 Then
                MessageBox.Show("Impossible de lier l'item de la banque de données. Veuillez fermer le compte client, le rouvrir et recommencer. Merci!", "Erreur de communication")
                Exit Sub
            End If
            DBLinker.getInstance.updateDB("CommunicationsKP", "NoKPFrom=" & MyNoKP & ",NoCommSubject=" & DBHelper.addItemToADBList("CommSubjects", "CommSubject", commSujet.Text, "NoCommSubject") & ",CommDate='" & commDate.Text & "',Remarques='" & commRemarques.Text.Replace("'", "''").Replace(vbCrLf, "<br>") & "',NoCategorie=" & DBHelper.addItemToADBList("CommCategories", "Categorie", commCategorie.Text, "NoCategorie"), "NoCommunication", noCommunication, False)
            myMainWin.StatusText = "Compte personne / orgaisme clé " & NoKP & " : Communication modifiée"
            InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunicationsKP(" & NoKP & "," & loadList & ")")
        End If

        If commCategorie.Text <> "" AndAlso commCategorie.Items.IndexOf(commCategorie.Text) = -1 Then
            commCategorie.Items.Add(commCategorie.Text)
            commFiltrageCat.Items.Add(commCategorie.Text)
        End If
    End Sub

    Private Function savingCommunications(Optional ByVal loadList As Boolean = True) As String
        If addComm.Enabled = False And modifComm.Enabled = False Then Return "DONE"

        If commSujet.Text = "" Then MessageBox.Show("Veuillez entrer un objet", "Information manquante") : Exit Function
        If commDeA.SelectedIndex < 0 Then MessageBox.Show("Veuillez sélectionner une provenance ou une destination (Champ À | De)", "Information manquante") : Exit Function

        If addComm.Enabled = True Then
            commSaveLoad(, loadList)
        Else
            commSaveLoad(False, loadList)
        End If

        Return "DONE"
    End Function

    Public Sub loadBills()
        'Droit & Accès
        If currentDroitAcces(23) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Comptabilité personne / organisme clé." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim lastSelected As Integer = -1
        If facturesView.RowCount <> 0 AndAlso facturesView.currentRow IsNot Nothing Then lastSelected = facturesView.currentRow.Cells("NoFacture").Value

        Dim reactivateModif As Boolean = False
        If toolTip1.GetToolTip(startStopBillChanging).StartsWith("Commencer") = False Then
            startStopBillChanging_Click(Me, Nothing)
            reactivateModif = True
        End If

        Dim whereStr As String = "WHERE StatFactures.ParNoKP = " & Me.NoKP

        'Date facture
        If dateDe.Text <> "" And dateA.Text <> "" And Me.dateAll.Checked = False Then
            whereStr &= " AND StatFactures.DateFacture >= '" & dateDe.Text & "' AND StatFactures.DateFacture < '" & DateFormat.getTextDate(CDate(dateA.Text).AddDays(1)) & "'"
        End If

        Dim descending As String = "DESC"
        Dim sortingOrder As DBLinker.SortOrderType = DBLinker.SortOrderType.Descending
        If PreferencesManager.getGeneralPreferences()("TriFactures").StartsWith("A") Then descending = "" : sortingOrder = DBLinker.SortOrderType.Ascending

        If whereStr.StartsWith(" AND") Then whereStr = " WHERE " & whereStr.Substring(4)
        Dim factures As DataSet = DBLinker.getInstance.readDBForGrid("Statfactures LEFT OUTER JOIN                       InfoClients AS IC1 ON IC1.NoClient = Statfactures.NoClient LEFT OUTER JOIN                       KeyPeople AS KP1 ON KP1.NoKP = Statfactures.NoKp LEFT OUTER JOIN                       Utilisateurs AS U1 ON U1.NoUser = Statfactures.NoUser LEFT OUTER JOIN                       InfoClients AS IC2 ON IC2.NoClient = Statfactures.ParNoClient LEFT OUTER JOIN                       Utilisateurs AS U2 ON U2.NoUser = Statfactures.ParNoUser LEFT OUTER JOIN                       KeyPeople AS KP2 ON KP2.NoKP = Statfactures.ParNoKp", "DF, TF, MF , CASE WHEN MP IS NULL THEN 0 ELSE MP END AS MP,PC , PPO, PU, EL , NoFacture FROM (SELECT     MIN(Statfactures.DateFacture) AS DF, MIN(Statfactures.TypeFacture) AS TF, CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN SUM(Statfactures.MontantFacture) ELSE dbo.GetLinkedBillMF(Statfactures.NoFacture,0) END AS [MF], CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN (SELECT SUM(MontantPaiement) FROM StatPaiements WHERE StatPaiements.NoFacture = Statfactures.NoFacture) ELSE dbo.GetLinkedBillMP(Statfactures.NoFacture,0) END AS MP, CASE WHEN MIN(Statfactures.ParNoClient)=0 THEN 'Aucun' ELSE MIN(IC2.Nom) + ',' + MIN(IC2.Prenom) END AS PC, CASE WHEN MIN(Statfactures.ParNoKP)=0 THEN 'Aucun' ELSE MIN(KP2.Nom) END AS PPO, CASE WHEN MIN(Statfactures.ParNoUser)=0 THEN 'Aucun' ELSE MIN(U2.Nom) + ',' + MIN(U2.Prenom) END AS PU, CASE WHEN MIN(Statfactures.NoClient)=0 THEN CASE WHEN MIN(Statfactures.NoKP)=0 THEN CASE WHEN MIN(Statfactures.NoUserFacture)=0 THEN 'Aucun' ELSE MIN(U1.Nom) + ',' + MIN(U1.Prenom) END ELSE MIN(KP1.Nom) END ELSE MIN(IC1.Nom) + ',' + MIN(IC1.Prenom) END AS EL, Statfactures.NoFacture,MIN(Statfactures.NoFactureRef) AS FRef, (SELECT TOP 1 SF2.NoAction FROM Statfactures AS SF2 WHERE SF2.NoStat = MAX(Statfactures.NoStat)) AS Action, MIN(NoStat) AS NoStat", whereStr & " GROUP BY Statfactures.NoFacture) AS Test WHERE Action<>20 ORDER BY DF DESC, NoStat DESC")
        facturesView.DataSource = factures.Tables(0)

        If factures.Tables(0).Rows.Count = 0 Then
            startStopBillChanging.Enabled = False
        Else
            startStopBillChanging.Enabled = True
        End If

        If lastSelected <> -1 Then
            For i As Integer = 0 To facturesView.RowCount - 1
                If facturesView.Rows(i).Cells("NoFacture").Value = lastSelected Then
                    facturesView.currentCell = facturesView.Rows(i).Cells(0)
                    curFactureBox.loading(lastSelected)
                    Exit For
                End If
            Next i
        End If

        If reactivateModif = True And factures.Tables(0).Rows.Count > 0 Then startStopBillChanging_Click(Me, Nothing)
    End Sub

    Private Sub loadLists()
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

    Public Sub loading(ByVal keyPeopleToLoad As Integer)
        configList(listeCommunications)
        loadLists()

        Dim kPeople(,) As String
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        kPeople = DBLinker.getInstance.readDB("Villes RIGHT JOIN (KPCategorie RIGHT JOIN ((KeyPeople LEFT JOIN InfoClients ON KeyPeople.NoClient = InfoClients.NoClient) LEFT JOIN Employeurs ON KeyPeople.NoEmployeur = Employeurs.NoEmployeur) ON KPCategorie.NoCategorie = KeyPeople.NoCategorie) ON Villes.NoVille = KeyPeople.NoVille", "KeyPeople.Nom, KPCategorie.Categorie, KeyPeople.Adresse, Villes.NomVille,KeyPeople.CodePostal,KeyPeople.Telephones,KeyPeople.NoRef,KeyPeople.Courriel,KeyPeople.URL,KeyPeople.NoClient, InfoClients.Nom, InfoClients.Prenom,AutreInfos, Employeurs.Employeur,WorkPlace,KeyPeople.Publipostage", "WHERE (NoKP=" & keyPeopleToLoad & ")")
        If kPeople Is Nothing OrElse kPeople.Length = 0 Then
            MessageBox.Show("La personne / organisme clé demandé(e) n'existe plus.", "P / O manquant")
            Me.Close()
            Exit Sub
        End If

        lockItems(True)
        lockCommunications(True)

        NoKP = keyPeopleToLoad

        loadGenInfo(kPeople)

        'Load comm section
        colorSent.BackColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorCommSent"))
        colorReceived.BackColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorCommReceived"))
        loadCommunications()

        paiements.Enabled = Bill.isPaymentsToDoByKP(keyPeopleToLoad)

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        formModified = False
        loadingCompte = False
    End Sub

    Public Sub loadGenInfo(Optional ByVal kPeople(,) As String = Nothing)
        Dim tels() As String
        If kPeople Is Nothing OrElse kPeople.Length = 0 Then kPeople = DBLinker.getInstance.readDB("Villes RIGHT JOIN (KPCategorie RIGHT JOIN ((KeyPeople LEFT JOIN InfoClients ON KeyPeople.NoClient = InfoClients.NoClient) LEFT JOIN Employeurs ON KeyPeople.NoEmployeur = Employeurs.NoEmployeur) ON KPCategorie.NoCategorie = KeyPeople.NoCategorie) ON Villes.NoVille = KeyPeople.NoVille", "KeyPeople.Nom, KPCategorie.Categorie, KeyPeople.Adresse, Villes.NomVille,KeyPeople.CodePostal,KeyPeople.Telephones,KeyPeople.NoRef,KeyPeople.Courriel,KeyPeople.URL,KeyPeople.NoClient, InfoClients.Nom, InfoClients.Prenom,AutreInfos, Employeurs.Employeur,WorkPlace, KeyPeople.Publipostage", "WHERE (NoKP=" & NoKP & ")")

        nom.Text = kPeople(0, 0)
        Me.Text = "Personne / Organisme " & NoKP & " : " & nom.Text
        categorie.Text = kPeople(1, 0)
        adresse.Text = kPeople(2, 0)
        ville.Text = kPeople(3, 0)
        If kPeople(4, 0).Length = 6 Then
            codepostal1.Text = kPeople(4, 0).Substring(0, 3)
            codepostal2.Text = kPeople(4, 0).Substring(3, 3)
        End If
        If kPeople(5, 0) <> "" Then
            tels = kPeople(5, 0).Split(New Char() {"§"})
            telephones.Items.AddRange(tels)
            telephones.SelectedIndex = 0
        End If
        noident.Text = kPeople(6, 0)
        courriel.Text = kPeople(7, 0)
        url.Text = kPeople(8, 0)
        If kPeople(9, 0) <> "0" And kPeople(9, 0) <> "" Then
            reference.Tag = kPeople(9, 0)
            reference.Text = kPeople(10, 0) & "," & kPeople(11, 0)
        End If
        ainfo.Text = kPeople(12, 0)
        employeur.Text = kPeople(13, 0)
        If kPeople(14, 0) <> "" Then
            Dim sWorkPlace() As String = System.Text.RegularExpressions.Regex.Split(kPeople(14, 0), "<br>")
            workPlace.Text = sWorkPlace(1)
            workPlace.Tag = sWorkPlace(0)
        End If

        If kPeople(15, 0) <> String.Empty Then publipostage.SelectedIndex = kPeople(15, 0)
        formModified = False
    End Sub

    Public Sub loadCommunications(Optional ByVal loadOnlyList As Boolean = False)
        isLoadOnlyList = loadOnlyList
        Dim CommDeAs(,), MyComms(,), Cats(), curCat As String
        Dim i, j, no As Integer

        If loadOnlyList = False Then
            viderComm_Click(Me, EventArgs.Empty)
            'Load CommCategories
            commCategorie.Items.Clear()
            commFiltrageCat.Items.Clear()
            commFiltrageCat.Items.Add("* Toutes *")
            Dim categories(,) As String = DBLinker.getInstance.readDB("CommCategories", "Categorie,(SELECT TOP 1 NoKP FROM CommunicationsKP WHERE CommCategories.NoCategorie=CommunicationsKP.NoCategorie AND NoKP = " & Me.NoKP & ")")
            If Not categories Is Nothing AndAlso categories.Length <> 0 Then
                For i = 0 To categories.GetUpperBound(1)
                    commCategorie.Items.Add(categories(0, i))
                    If categories(1, i) <> "" Then commFiltrageCat.Items.Add(categories(0, i))
                Next i
            End If
            commFiltrageCat.SelectedIndex = 0

            'Load CommDeA
            commDeA.Items.Clear()
            commSujet.Items.Clear()
            commFiltrage.Items.Clear()
            CommDeAs = DBLinker.getInstance.readDB("KPCategorie RIGHT JOIN (CommDeAKP LEFT JOIN KeyPeople ON CommDeAKP.NoKPDeA = KeyPeople.NoKP) ON KPCategorie.NoCategorie = KeyPeople.NoCategorie", "Nom + '(' + CONVERT(varchar,CommDeAKP.NoKPDeA) + ')', Categorie ", "WHERE (CommDeAKP.NoKP=" & NoKP & ");")
            If Not CommDeAs Is Nothing AndAlso CommDeAs.Length <> 0 Then
                For i = 0 To CommDeAs.GetUpperBound(1)
                    commDeA.Items.Add(CommDeAs(0, i))

                    If CommDeAs(1, i) <> "" Then
                        Cats = CommDeAs(1, i).Split(New Char() {":"}) : curCat = ""
                        For j = 0 To Cats.Length - 1
                            curCat &= ":" & Cats(j)
                            If curCat.Substring(0, 1) = ":" Then curCat = curCat.Substring(1)
                            If commFiltrage.FindStringExact(curCat) < 0 Then commFiltrage.Items.Add(curCat)
                        Next j
                    End If
                Next i
            End If
            If commDeA.FindStringExact("* Personne / Organisme *") < 0 Then commDeA.Items.Add("* Personne / Organisme *")
            If commFiltrage.FindStringExact(" Tout afficher ") < 0 Then commFiltrage.Items.Add(" Tout afficher ")
            If commFiltrage.FindStringExact("* Personne / Organisme *") < 0 Then commFiltrage.Items.Add("* Personne / Organisme *")
            If commFiltrage.SelectedIndex < 0 Then commFiltrage.SelectedIndex = 0

            'Load subject
            commSujet.Items.Clear()
            Dim commObjects() As String = DBLinker.getInstance.readOneDBField("CommSubjects", "CommSubject")
            If commObjects IsNot Nothing Then commSujet.Items.AddRange(commObjects)
        End If

        Dim noCommSelected As Integer = 0
        If listeCommunications.selected <> -1 AndAlso listeCommunications.ItemValueA(listeCommunications.selected) IsNot Nothing Then noCommSelected = listeCommunications.ItemValueA(listeCommunications.selected)
        listeCommunications.cls()

        MyComms = DBLinker.getInstance.readDB("KPCategorie RIGHT JOIN (((CommunicationsKP LEFT JOIN CommSubjects ON CommunicationsKP.NoCommSubject = CommSubjects.NoCommSubject) LEFT JOIN CommCategories ON CommCategories.NoCategorie=CommunicationsKP.NoCategorie) LEFT JOIN KeyPeople ON CommunicationsKP.NoKPFrom = KeyPeople.NoKP) ON KPCategorie.NoCategorie = KeyPeople.NoCategorie", "KPCategorie.Categorie, CommunicationsKP.NoCommunication,CommunicationsKP.NoKP,CommunicationsKP.NoKPFrom,CommunicationsKP.IsEnvoie,CommSubject,CommunicationsKP.CommDate,CommunicationsKP.NoUser,CommunicationsKP.Remarques,CommunicationsKP.NameOfFile,CommunicationsKP.NoCategorie,CommCategories.Categorie", "WHERE (CommunicationsKP.NoKP=" & NoKP & ");")
        If Not MyComms Is Nothing AndAlso MyComms.Length <> 0 Then
            For i = 0 To MyComms.GetUpperBound(1)
                If MyComms(3, i) = "" Then MyComms(3, i) = 0

                Dim acceptedComm As Boolean = False
                If commFiltrage.Text <> " Tout afficher " Then
                    If commFiltrage.Text.StartsWith("* Personne / Organisme *") Then
                        If MyComms(3, i) = 0 Then acceptedComm = True
                    Else
                        If MyComms(3, i) <> 0 And MyComms(0, i).StartsWith(commFiltrage.Text) = True Then acceptedComm = True
                    End If
                Else
                    acceptedComm = True
                End If
                If acceptedComm AndAlso commFiltrageCat.SelectedIndex <> 0 Then
                    If Not (MyComms(11, i) = commFiltrageCat.Text OrElse MyComms(11, i).StartsWith(commFiltrageCat.Text & ":")) Then acceptedComm = False
                End If

                If acceptedComm = True Then
                    If (MyComms(4, i) = True And commEnvoie.Checked = True) Or (MyComms(4, i) = False And commReception.Checked = True) Then
                        no = listeCommunications.add("(" & DateFormat.getTextDate(CDate(MyComms(6, i))) & ") " & IIf(MyComms(11, i) = "", "* Aucune catégorie *", MyComms(11, i)) & " : " & MyComms(5, i))
                        listeCommunications.ItemValueA(no) = MyComms(1, i)
                        For j = 0 To MyComms.GetUpperBound(0)
                            listeCommunications.ItemValueB(no) &= "§" & MyComms(j, i)
                        Next j
                        listeCommunications.ItemValueB(no) = CStr(listeCommunications.ItemValueB(no)).Substring(1)
                        If MyComms(9, i) <> "" Then listeCommunications.ItemIconsShowed(no, 0) = True
                        If MyComms(4, i) = True Then
                            listeCommunications.ItemBackColor(no) = colorSent.BackColor
                        Else
                            listeCommunications.ItemBackColor(no) = colorReceived.BackColor
                        End If
                    End If
                End If
            Next i
        End If
        listeCommunications.draw = True : listeCommunications.draw = False
        listeCommunications.selected = listeCommunications.findStringExact(noCommSelected, , CI.Controls.List.FindingType.ValueA)
        commModified = False

        isLoadOnlyList = False
    End Sub

    Private Sub paiements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles paiements.Click
        Dim myPaiement As Payment = openUniqueWindow(New Payment(), "Effectuer le(s) paiement(s) de " & nom.Text)
        If myPaiement.billsLoaded = False Then myPaiement.loading(NoKP, FacturationBox.DedicatedType.KP)
        myPaiement.Show()
    End Sub

    Private Sub createBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles createBill.Click
        'Droit & Accès
        If currentDroitAcces(77) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Nouvelle facture." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myAddBill As addBill = openUniqueWindow(New addBill())
        myAddBill.Show()
        myAddBill.mode = addBill.Modes.KP
        myAddBill.setKP(NoKP, nom.Text)
    End Sub

    Private Sub ongletskp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ongletskp.SelectedIndexChanged
        If ongletskp.SelectedIndex = 0 Then
            startStopCommChanging.Visible = True
            startStopBillChanging.Visible = False
        Else
            startStopCommChanging.Visible = False
            startStopBillChanging.Visible = True
        End If
    End Sub

    Private Sub startStopCommChanging_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles startStopCommChanging.Click
        'Droit & Accès
        If currentDroitAcces(26) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier les communications." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If toolTip1.GetToolTip(startStopCommChanging).StartsWith("C") Then
            If lockSecteur("KPComm-" & NoKP & "-", True, "Communications de la personne / organisme clé " & nom.Text) = True Then
                startStopCommChanging.Image = imgModifSave.Images(2)
                toolTip1.SetToolTip(startStopCommChanging, "Arrêter la modification des communications")

                lockCommunications(False)
            End If
        Else
            If commModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then If savingCommunications(True) <> "" Then Exit Sub
            commModified = False

            startStopCommChanging.Image = imgModifSave.Images(0)
            toolTip1.SetToolTip(startStopCommChanging, "Commencer la modification les communications")

            lockCommunications(True)
            lockSecteur("KPComm-" & NoKP & "-", False)
        End If
    End Sub

    Private currentLocks As New ArrayList

    Private Sub startStopBillChanging_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles startStopBillChanging.Click
        'Droit & Accès
        If currentDroitAcces(24) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier les factures." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim i As Integer

        If toolTip1.GetToolTip(startStopBillChanging).StartsWith("C") Then
            If lockSecteur("KPFacturation-" & NoKP & "-", True, "Facturation de la personne / organisme clé " & nom.Text) = True Then
                currentLocks.Add("KPFacturation-" & NoKP & "-")
                startStopBillChanging.Image = imgModifSave.Images(2)
                toolTip1.SetToolTip(startStopBillChanging, "Arrêter la modification des factures et paiements")

                curFactureBox.locked = False
                currentLocks.Add("KPFacturation-" & Me.NoKP & "-")
                If facturesView.SelectedRows.Count > 0 Then
                    If curFactureBox.lockSecteur("", currentLocks) = False Then
                        MessageBox.Show("Impossible de modifier la facture présentement sélectionné, car soit la facturation de l'entité liée ou de l'un des payeurs est présentement en cours de modification", "Facturation de la personne / organisme clé " & nom.Text)
                        curFactureBox.locked = True
                    End If
                End If
            End If
        Else
            startStopBillChanging.Image = imgModifSave.Images(0)
            toolTip1.SetToolTip(startStopBillChanging, "Commencer la modification des factures et paiements")

            curFactureBox.locked = True
            For i = 0 To currentLocks.Count - 1
                lockSecteur(currentLocks(i), False)
            Next i
            currentLocks.Clear()

            If facturesView.RowCount > 0 Then InternalUpdatesManager.getInstance.sendUpdate("PaiementsKP(" & NoKP & ",-1)")
        End If
    End Sub

    Private Sub addingAlert(ByVal alertMessage As String)
        Dim alertDate As Date

        Dim myDateChoice As New DateChoice
        Dim myAlarmDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year, Date.Now.Year + 1, , , True, , , True, Date.Now, , , , , , , , , 1)
        If myAlarmDate.Count = 0 Then Exit Sub

        alertDate = myAlarmDate(0)

        AlertsManager.getInstance.addAlert(alertMessage, ConnectionsManager.currentUser, AlertsManager.AType.OpenKPAccount, Me.NoKP, , New AlarmOfKPAccount(alertDate, Me.NoKP), True)
    End Sub

    Private Sub addAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addAlert.Click
        Dim myInputBoxPlus As New InputBoxPlus(True, "Users\Lists\" & ConnectionsManager.currentUser & "\alertmsg.lst")
        Dim alertMessage As String = myInputBoxPlus("Quel message désirez-vous vous laisser ?", "Message au compte")
        If alertMessage = "" Then Exit Sub
        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\alertmsg.lst", alertMessage, , True, 15, False)

        addingAlert(alertMessage)
    End Sub

    Private Sub facturesView_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles facturesView.RowStateChanged
        If e.StateChanged = DataGridViewElementStates.None Then
            curFactureBox.locked = True
        End If
    End Sub

    Private Sub facturesView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles facturesView.SelectionChanged
        If facturesView.currentRow Is Nothing Then Exit Sub

        Dim curLoadedBill As Integer = Integer.Parse(facturesView.currentRow.Cells("NoFacture").Value.ToString)
        If curLoadedBill = lastLoadedBill Then Exit Sub

        lastLoadedBill = curLoadedBill
        If toolTip1.GetToolTip(startStopBillChanging).StartsWith("Commencer") = False Then curFactureBox.locked = False
        curFactureBox.loading(lastLoadedBill)
        If curFactureBox.locked = False AndAlso curFactureBox.lockSecteur("Facture de la personne / organisme clé " & nom.Text, currentLocks) = False Then
            MessageBox.Show("Impossible de modifier la facture présentement sélectionné, car soit la facturation de l'entité liée ou de l'un des payeurs est présentement en cours de modification", "Facturation de la personne / organisme clé " & nom.Text)
            curFactureBox.locked = True
        End If
    End Sub

    Private Sub commCategorie_DeletedItem(ByVal currentItem As String) Handles commCategorie.deletedItem
        commModified = False
        commFiltrageCat.Items.Remove(currentItem)
        loadCommunications(True)
    End Sub

    Private Sub commCategorie_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles commCategorie.TextChanged
        commModified = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf viewmodifKeyPeople_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function = "KP-Close" AndAlso dataReceived.params(0) = Me.NoKP Then
            Me.Close()
            Exit Sub
        End If

        If dataReceived.fromExternal AndAlso dataReceived.function = "PaiementsKP" AndAlso dataReceived.params(0) = Me.NoKP Then
            Dim paymentsToDo As Boolean = False
            If dataReceived.params(1) = -1 Then
                paymentsToDo = Bill.isPaymentsToDoByKP(NoKP)
            ElseIf dataReceived.params(1) > 0 Then
                paymentsToDo = True
            End If
            paiements.Enabled = paymentsToDo
        End If

        If dataReceived.function = "AccountsBillsKP" AndAlso dataReceived.params(0) = NoKP Then
            If Me.facturesView.RowCount <> 0 Then loadBills()
            paiements.Enabled = Bill.isPaymentsToDoByKP(NoKP)
        End If

        If dataReceived.fromExternal AndAlso dataReceived.function = "AccountsGenInfoKP" AndAlso dataReceived.params(0) = NoKP Then
            loadGenInfo()
        End If

        If dataReceived.function = "AccountsCommunicationsKP" AndAlso dataReceived.params(0) = NoKP Then
            loadCommunications(dataReceived.params(1))
        End If
    End Sub
End Class
