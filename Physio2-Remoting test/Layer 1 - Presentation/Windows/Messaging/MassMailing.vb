Imports System.Web
Imports System.Web.Mail

Friend Class MassMailing
    Inherits SingleWindow

    Private HTMLPageURL, htmlFileName As String
    Private WithEvents attachMenu As System.Windows.Forms.ContextMenuStrip
    Private WithEvents externeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents banqueDeDonnéesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents testPrint As System.Windows.Forms.Button
    Friend WithEvents BothAccounts As System.Windows.Forms.RadioButton
    Friend WithEvents KeyPeople As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Clients As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private messageSent As Boolean = False
    Friend WithEvents btnApplyModel As System.Windows.Forms.Button
    Private isLoaded As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Both.Checked = True
        Me.FileDialog.Filter = TypesFilesManager.getGeneralFiltering
        Me.FileDialog.Title = "Choix du fichier à attacher"
        Me.FileDialog.ShowReadOnly = False
        Me.FileDialog.ReadOnlyChecked = True

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin

        '
        'Message
        '
        Me.Message = New WebTextControl()
        With Message
            .Location = New System.Drawing.Point(8, 156)
            .Name = "Message"
            .Size = New System.Drawing.Size(464, 444)
            .TabIndex = 20
            .Text = ""
            .editorWidth = 455
            .editorHeight = 352
            .editorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")
            .toolBarStyles = 2
            .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
            .allowContextMenu = True
            .allowEditorContextMenu = True
            .activateLinksOnEdit = True
            .allowNavigation = False
            .allowUndo = True
            .editorContextMenu = Nothing
        End With
        Me.Controls.Add(Me.Message)

        'Chargement des images
        With DrawingManager.getInstance
            Me.Envoyer.Image = .getImage("send16.gif")
            Me.AddAttach.Image = .getImage("ajouter16.gif")
            Me.Vider.Image = .getImage("eraser.jpg")
            Me.DelAttach.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.btnApplyModel.Image = .getImage("modeles16.gif")
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Sujet As Clinica.ManagedCombo
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Vider As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Envoyer As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents AddAttach As System.Windows.Forms.Button
    Friend WithEvents DelAttach As System.Windows.Forms.Button
    Friend WithEvents FileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents De As Clinica.ManagedCombo
    Friend WithEvents Attachements As ManagedText
    Friend WithEvents Message As WebTextControl
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Courrier As System.Windows.Forms.RadioButton
    Friend WithEvents Courriel As System.Windows.Forms.RadioButton
    Friend WithEvents Both As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Vider = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Envoyer = New System.Windows.Forms.Button
        Me.AddAttach = New System.Windows.Forms.Button
        Me.DelAttach = New System.Windows.Forms.Button
        Me.testPrint = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.FileDialog = New System.Windows.Forms.OpenFileDialog
        Me.Label3 = New System.Windows.Forms.Label
        Me.Courrier = New System.Windows.Forms.RadioButton
        Me.Courriel = New System.Windows.Forms.RadioButton
        Me.Both = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.attachMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.externeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.banqueDeDonnéesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BothAccounts = New System.Windows.Forms.RadioButton
        Me.KeyPeople = New System.Windows.Forms.RadioButton
        Me.Label6 = New System.Windows.Forms.Label
        Me.Clients = New System.Windows.Forms.RadioButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Attachements = New ManagedText
        Me.Sujet = New ManagedCombo
        Me.De = New ManagedCombo
        Me.btnApplyModel = New System.Windows.Forms.Button
        Me.attachMenu.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sujet :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Message :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(12, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "De :"
        '
        'Vider
        '
        Me.Vider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Vider.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Vider.Location = New System.Drawing.Point(388, 0)
        Me.Vider.Name = "Vider"
        Me.Vider.Size = New System.Drawing.Size(24, 24)
        Me.Vider.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.Vider, "Vider les champs")
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'Envoyer
        '
        Me.Envoyer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Envoyer.Enabled = False
        Me.Envoyer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Envoyer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Envoyer.Location = New System.Drawing.Point(448, 0)
        Me.Envoyer.Name = "Envoyer"
        Me.Envoyer.Size = New System.Drawing.Size(24, 24)
        Me.Envoyer.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.Envoyer, "Envoyer le message")
        '
        'AddAttach
        '
        Me.AddAttach.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddAttach.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddAttach.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddAttach.Location = New System.Drawing.Point(452, 104)
        Me.AddAttach.Name = "AddAttach"
        Me.AddAttach.Size = New System.Drawing.Size(24, 24)
        Me.AddAttach.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.AddAttach, "Ajouter un attachement au message")
        '
        'DelAttach
        '
        Me.DelAttach.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DelAttach.Enabled = False
        Me.DelAttach.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelAttach.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelAttach.Location = New System.Drawing.Point(452, 128)
        Me.DelAttach.Name = "DelAttach"
        Me.DelAttach.Size = New System.Drawing.Size(24, 24)
        Me.DelAttach.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.DelAttach, "Enlever un attachement déjà attaché")
        '
        'testPrint
        '
        Me.testPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.testPrint.Enabled = False
        Me.testPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.testPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.testPrint.Location = New System.Drawing.Point(418, 0)
        Me.testPrint.Name = "testPrint"
        Me.testPrint.Size = New System.Drawing.Size(24, 24)
        Me.testPrint.TabIndex = 6
        Me.testPrint.Text = "T"
        Me.ToolTip1.SetToolTip(Me.testPrint, "Imprimer un courrier de test")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 104)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Attachement(s) :"
        '
        'FileDialog
        '
        Me.FileDialog.Multiselect = True
        Me.FileDialog.ReadOnlyChecked = True
        Me.FileDialog.RestoreDirectory = True
        Me.FileDialog.ShowReadOnly = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(12, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Envoyé par :"
        '
        'Courrier
        '
        Me.Courrier.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Courrier.Location = New System.Drawing.Point(173, 0)
        Me.Courrier.Name = "Courrier"
        Me.Courrier.Size = New System.Drawing.Size(64, 16)
        Me.Courrier.TabIndex = 21
        Me.Courrier.Text = "Courrier"
        '
        'Courriel
        '
        Me.Courriel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Courriel.Location = New System.Drawing.Point(87, 0)
        Me.Courriel.Name = "Courriel"
        Me.Courriel.Size = New System.Drawing.Size(64, 16)
        Me.Courriel.TabIndex = 21
        Me.Courriel.Text = "Courriel"
        '
        'Both
        '
        Me.Both.Checked = True
        Me.Both.Location = New System.Drawing.Point(0, 0)
        Me.Both.Name = "Both"
        Me.Both.Size = New System.Drawing.Size(72, 16)
        Me.Both.TabIndex = 21
        Me.Both.TabStop = True
        Me.Both.Text = "Les deux"
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(-4, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(504, 1)
        Me.Label4.TabIndex = 22
        '
        'attachMenu
        '
        Me.attachMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.externeToolStripMenuItem, Me.banqueDeDonnéesToolStripMenuItem})
        Me.attachMenu.Name = "ContextMenuStrip1"
        Me.attachMenu.ShowImageMargin = False
        Me.attachMenu.ShowItemToolTips = False
        Me.attachMenu.Size = New System.Drawing.Size(183, 48)
        '
        'externeToolStripMenuItem
        '
        Me.externeToolStripMenuItem.Name = "externeToolStripMenuItem"
        Me.externeToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.externeToolStripMenuItem.Text = "De l'extérieur du logiciel"
        '
        'banqueDeDonnéesToolStripMenuItem
        '
        Me.banqueDeDonnéesToolStripMenuItem.Name = "banqueDeDonnéesToolStripMenuItem"
        Me.banqueDeDonnéesToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.banqueDeDonnéesToolStripMenuItem.Text = "De la banque de données"
        '
        'BothAccounts
        '
        Me.BothAccounts.Checked = True
        Me.BothAccounts.Location = New System.Drawing.Point(0, 0)
        Me.BothAccounts.Name = "BothAccounts"
        Me.BothAccounts.Size = New System.Drawing.Size(72, 16)
        Me.BothAccounts.TabIndex = 21
        Me.BothAccounts.TabStop = True
        Me.BothAccounts.Text = "Les deux"
        '
        'KeyPeople
        '
        Me.KeyPeople.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.KeyPeople.Location = New System.Drawing.Point(173, 0)
        Me.KeyPeople.Name = "KeyPeople"
        Me.KeyPeople.Size = New System.Drawing.Size(185, 16)
        Me.KeyPeople.TabIndex = 21
        Me.KeyPeople.Text = "Personnes / organismes clés"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(12, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Envoyé à :"
        '
        'Clients
        '
        Me.Clients.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Clients.Location = New System.Drawing.Point(87, 0)
        Me.Clients.Name = "Clients"
        Me.Clients.Size = New System.Drawing.Size(64, 16)
        Me.Clients.TabIndex = 21
        Me.Clients.Text = "Clients"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Clients)
        Me.Panel1.Controls.Add(Me.KeyPeople)
        Me.Panel1.Controls.Add(Me.BothAccounts)
        Me.Panel1.Location = New System.Drawing.Point(92, 29)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(352, 16)
        Me.Panel1.TabIndex = 23
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Both)
        Me.Panel2.Controls.Add(Me.Courriel)
        Me.Panel2.Controls.Add(Me.Courrier)
        Me.Panel2.Location = New System.Drawing.Point(92, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(244, 16)
        Me.Panel2.TabIndex = 23
        '
        'Attachements
        '
        Me.Attachements.acceptAlpha = True
        Me.Attachements.acceptedChars = ""
        Me.Attachements.acceptNumeric = True
        Me.Attachements.allCapital = False
        Me.Attachements.allLower = False
        Me.Attachements.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Attachements.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Attachements.blockOnMaximum = False
        Me.Attachements.blockOnMinimum = False
        Me.Attachements.cb_AcceptLeftZeros = False
        Me.Attachements.cb_AcceptNegative = False
        Me.Attachements.currencyBox = False
        Me.Attachements.firstLetterCapital = False
        Me.Attachements.firstLettersCapital = False
        Me.Attachements.Location = New System.Drawing.Point(92, 104)
        Me.Attachements.manageText = True
        Me.Attachements.matchExp = ""
        Me.Attachements.maximum = 0
        Me.Attachements.minimum = 0
        Me.Attachements.Multiline = True
        Me.Attachements.Name = "Attachements"
        Me.Attachements.nbDecimals = CType(-1, Short)
        Me.Attachements.onlyAlphabet = False
        Me.Attachements.ReadOnly = True
        Me.Attachements.refuseAccents = False
        Me.Attachements.refusedChars = ""
        Me.Attachements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Attachements.showInternalContextMenu = True
        Me.Attachements.Size = New System.Drawing.Size(360, 48)
        Me.Attachements.TabIndex = 8
        Me.Attachements.trimText = False
        '
        'Sujet
        '
        Me.Sujet.acceptAlpha = True
        Me.Sujet.acceptedChars = Nothing
        Me.Sujet.acceptNumeric = True
        Me.Sujet.allCapital = False
        Me.Sujet.allLower = False
        Me.Sujet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Sujet.autoComplete = True
        Me.Sujet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Sujet.autoSizeDropDown = True
        Me.Sujet.BackColor = System.Drawing.Color.White
        Me.Sujet.blockOnMaximum = False
        Me.Sujet.blockOnMinimum = False
        Me.Sujet.cb_AcceptLeftZeros = False
        Me.Sujet.cb_AcceptNegative = False
        Me.Sujet.currencyBox = False
        Me.Sujet.dbField = Nothing
        Me.Sujet.doComboDelete = True
        Me.Sujet.firstLetterCapital = True
        Me.Sujet.firstLettersCapital = False
        Me.Sujet.itemsToolTipDuration = 10000
        Me.Sujet.Location = New System.Drawing.Point(52, 80)
        Me.Sujet.manageText = True
        Me.Sujet.matchExp = ""
        Me.Sujet.maximum = 0
        Me.Sujet.minimum = 0
        Me.Sujet.Name = "Sujet"
        Me.Sujet.nbDecimals = CType(-1, Short)
        Me.Sujet.onlyAlphabet = False
        Me.Sujet.pathOfList = Nothing
        Me.Sujet.ReadOnly = False
        Me.Sujet.refuseAccents = False
        Me.Sujet.refusedChars = ""
        Me.Sujet.showItemsToolTip = False
        Me.Sujet.Size = New System.Drawing.Size(424, 21)
        Me.Sujet.Sorted = True
        Me.Sujet.TabIndex = 2
        Me.Sujet.trimText = False
        '
        'De
        '
        Me.De.acceptAlpha = True
        Me.De.acceptedChars = Nothing
        Me.De.acceptNumeric = True
        Me.De.allCapital = False
        Me.De.allLower = False
        Me.De.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.De.autoComplete = True
        Me.De.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.De.autoSizeDropDown = True
        Me.De.BackColor = System.Drawing.Color.White
        Me.De.blockOnMaximum = False
        Me.De.blockOnMinimum = False
        Me.De.cb_AcceptLeftZeros = False
        Me.De.cb_AcceptNegative = False
        Me.De.currencyBox = False
        Me.De.dbField = Nothing
        Me.De.doComboDelete = True
        Me.De.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.De.firstLetterCapital = False
        Me.De.firstLettersCapital = False
        Me.De.itemsToolTipDuration = 10000
        Me.De.Location = New System.Drawing.Point(52, 56)
        Me.De.manageText = True
        Me.De.matchExp = Nothing
        Me.De.maximum = 0
        Me.De.minimum = 0
        Me.De.Name = "De"
        Me.De.nbDecimals = CType(-1, Short)
        Me.De.onlyAlphabet = False
        Me.De.pathOfList = Nothing
        Me.De.ReadOnly = False
        Me.De.refuseAccents = False
        Me.De.refusedChars = ""
        Me.De.showItemsToolTip = False
        Me.De.Size = New System.Drawing.Size(424, 21)
        Me.De.TabIndex = 11
        Me.De.trimText = False
        '
        'btnApplyModel
        '
        Me.btnApplyModel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyModel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnApplyModel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnApplyModel.Enabled = False
        Me.btnApplyModel.Location = New System.Drawing.Point(358, 0)
        Me.btnApplyModel.Name = "btnApplyModel"
        Me.btnApplyModel.Size = New System.Drawing.Size(24, 24)
        Me.btnApplyModel.TabIndex = 5
        '
        'MassMailing
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(482, 608)
        Me.Controls.Add(Me.Attachements)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DelAttach)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.AddAttach)
        Me.Controls.Add(Me.Envoyer)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.testPrint)
        Me.Controls.Add(Me.Vider)
        Me.Controls.Add(Me.btnApplyModel)
        Me.Controls.Add(Me.Sujet)
        Me.Controls.Add(Me.De)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(490, 642)
        Me.Name = "MassMailing"
        Me.ShowInTaskbar = False
        Me.Text = "Publipostage"
        Me.attachMenu.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub msgPublipostage_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If MassMailing.sendingThread Is Nothing Then lockSecteur("Publipostage.lock", False)
    End Sub

    Private Sub msgPublipostage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim MySettings(), sujets() As String

        'Chargement du champ De Externe
        De.Items.AddRange(MailsManager.getInstance.getMailAccounts.ToArray)

        If De.Items.Count = 0 Then
            Me.Courriel.Enabled = False
            Me.Both.Enabled = False
            Me.Courrier.Checked = True
        Else
            De.SelectedIndex = 0
        End If

        'Chargement des listes Combo selon l'utilisateur
        sujets = readFile("Users\Lists\" & ConnectionsManager.currentUser & "\mailsujets.lst", , False)
        Sujet.pathOfList = "Users\Lists\" & ConnectionsManager.currentUser & "\mailsujets.lst"
        If sujets(0).ToUpper.StartsWith("ERROR") = False Then Sujet.Items.AddRange(sujets)

        Message.htmlPageURL = emptyHTMLPath
        Message.Editing = True
        Message.showPage()

        'Settings
        Dim setting As String = UsersManager.currentUser.settings.massMailing
        If setting <> "" Then
            MySettings = setting.Split(New Char() {"§"})
            FileDialog.InitialDirectory = MySettings(0)
        End If
    End Sub

    Public Shared Sub restartSending()
        Dim infos As New SendingInfos()
        infos.loadFromFile()
        MassMailing.sendingThread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf sending))
        sendingThread.IsBackground = True
        'Nécessaire pour le WebControl
        If sendingThread.TrySetApartmentState(Threading.ApartmentState.STA) = False Then
            MessageBox.Show("Une erreur est survenue pour l'envoi du publipostage.Veuillez réessayer s'il vous plait", "Erreur")
            sendingThread = Nothing
            Exit Sub
        End If
        sendingThread.Start(infos)
    End Sub

    Private Sub addAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddAttach.Click
        If currentDroitAcces(6) = False Then
            externeToolStripMenuItem_Click(sender, e)
        Else
            attachMenu.Show(Me.AddAttach, New Point(0, 0), ToolStripDropDownDirection.Left)
        End If
    End Sub

    Private Sub externeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles externeToolStripMenuItem.Click
        FileDialog.ShowDialog()
        Dim files() As String = FileDialog.FileNames
        Dim i As Integer
        For i = 0 To files.Length - 1
            If Attachements.Text.IndexOf(files(i) & ";") = -1 Then Attachements.Text &= files(i) & ";"
        Next i
    End Sub

    Private Sub banqueDeDonnéesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles banqueDeDonnéesToolStripMenuItem.Click
        Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB())
        mySearchDB.Visible = False
        mySearchDB.from = AddAttach
        mySearchDB.selectedCat = "Généraux"
        mySearchDB.useWinAsSelection = True
        mySearchDB.MdiParent = Nothing
        mySearchDB.StartPosition = FormStartPosition.CenterScreen
        mySearchDB.ShowDialog()
    End Sub

    Private Sub attachements_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Attachements.Click
        selectAttachement()
    End Sub

    Private Sub selectAttachement(Optional ByVal keyDowned As Boolean = False)
        Dim LeftPos, RightPos, i As Integer
        If (Attachements.SelectionStart - 1) > 0 Then
            For i = Attachements.SelectionStart - 1 To 0 Step -1
                If Attachements.Text.Substring(i, 1) = ";" Then LeftPos = i + 1 : Exit For
            Next i
        Else
            If keyDowned = False Then
                LeftPos = 0
            Else
                LeftPos = Attachements.Text.Length
            End If
        End If

        For i = Attachements.SelectionStart To Attachements.Text.Length - 1
            If Attachements.Text.Substring(i, 1) = ";" Then RightPos = i + 1 : Exit For
        Next i

        Attachements.SelectionStart = LeftPos
        If (RightPos - LeftPos) > 0 Then Attachements.SelectionLength = RightPos - LeftPos
        If Attachements.SelectedText = "" Then
            DelAttach.Enabled = False
        Else
            DelAttach.Enabled = True
        End If
    End Sub

    Private Sub delAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelAttach.Click
        If Attachements.SelectedText = "" Then
            MessageBox.Show("Veuillez sélectionner un attachement", "Aucune sélection")
            Exit Sub
        End If
        If MessageBox.Show("Êtes-vous sûr de vouloir enlever cet attachement ?", "Confirmation d'effacement", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Attachements.SelectedText = ""
        DelAttach.Enabled = False
    End Sub

    Private Sub attachements_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Attachements.KeyDown
        Dim i, selStartPos As Integer
        Dim doKeyDowned As Boolean = False

        Select Case e.KeyCode
            Case 37
                doKeyDowned = True
                For i = Attachements.SelectionStart - 1 To 0 Step -1
                    If Attachements.Text.Substring(i, 1) = ";" Then selStartPos = i : Exit For
                Next i

                If selStartPos < 0 Then selStartPos = 0
            Case 39
                For i = Attachements.SelectionStart To Attachements.Text.Length - 1
                    If Attachements.Text.Substring(i, 1) = ";" Then selStartPos = i + 1 : Exit For
                Next i

                If selStartPos > Attachements.Text.Length Then selStartPos = Attachements.Text.Length
        End Select

        Attachements.SelectionStart = selStartPos
        selectAttachement(doKeyDowned)
        e.Handled = True
    End Sub

    Private Sub msgPublipostage_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MassMailing.sendingThread IsNot Nothing Then Exit Sub

        saveSettingsUser()
    End Sub

    Private Sub saveSettingsUser()
        Dim fdPath As String
        If FileDialog.FileNames.Length > 0 Then
            fdPath = FileDialog.FileNames(0).Substring(0, FileDialog.FileNames(0).Length - getLastDir(FileDialog.FileNames(0)).Length - 1)
        Else
            fdPath = FileDialog.InitialDirectory
        End If
        'Save Settings
        Dim curUser As User = UsersManager.currentUser
        curUser.settings.massMailing = fdPath
        curUser.settings.saveData()
    End Sub

    Private Sub vider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Vider.Click
        Attachements.Text = ""
        Message.setHtml("", True)
        Sujet.Text = ""
        Message.setPos(0)
        Message.focus()
    End Sub

    Public Shared sendingThread As Threading.Thread

    Private Sub envoyer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Envoyer.Click
        Dim curAccount As MailAccount = De.SelectedItem

        Dim curInfos As New SendingInfos
        curInfos.attachements = Attachements.Text
        curInfos.des = De.GetItemText(De.SelectedItem)
        curInfos.messageText = Message.getHTML
        curInfos.account = curAccount
        curInfos.sujet = Sujet.Text
        curInfos.publipostageType = IIf(Both.Checked, 3, IIf(Courriel.Checked, 2, 1))
        curInfos.toType = IIf(BothAccounts.Checked, FacturationBox.DedicatedType.All, IIf(Clients.Checked, FacturationBox.DedicatedType.Client, FacturationBox.DedicatedType.KP))

        If Me.Sujet.Text = "" Then
            If MessageBox.Show("Avez-vous oublié d'entrer le sujet du message ?", "Sujet vide", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Sujet.Focus()
                Exit Sub
            End If
        End If

        If curInfos.messageText = "" Then
            If MessageBox.Show("Avez-vous oublié d'entrer du texte pour votre message ?", "Message vide", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Message.focus()
                Exit Sub
            End If
        End If

        MassMailing.sendingThread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf sending))
        sendingThread.IsBackground = True
        'Nécessaire pour le WebControl
        If sendingThread.TrySetApartmentState(Threading.ApartmentState.STA) = False Then
            MessageBox.Show("Une erreur est survenue pour l'envoi du publipostage.Veuillez réessayer s'il vous plait", "Erreur")
            sendingThread = Nothing
            Exit Sub
        End If
        saveSettingsUser()
        sendingThread.Start(curInfos)

        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\mailsujets.lst", Sujet.Text, , True, 50)
        Me.Close()
    End Sub

    Private Sub testPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles testPrint.Click
        Dim curAccount As MailAccount = De.SelectedItem

        Dim curInfos As New SendingInfos
        curInfos.attachements = ""
        curInfos.des = De.GetItemText(De.SelectedItem)
        curInfos.messageText = Message.getHTML
        curInfos.account = curAccount
        curInfos.sujet = Sujet.Text
        curInfos.publipostageType = 1
        curInfos.maximumPrint = 1

        MassMailing.sendingThread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf sending))
        sendingThread.IsBackground = True
        'Nécessaire pour le WebControl
        If sendingThread.TrySetApartmentState(Threading.ApartmentState.STA) = False Then
            MessageBox.Show("Une erreur est survenue pour l'envoi du publipostage.Veuillez réessayer s'il vous plait", "Erreur")
            sendingThread = Nothing
            Exit Sub
        End If
        saveSettingsUser()
        sendingThread.Start(curInfos)

        sendingThread.Join()
    End Sub

    Private Class SendingInfos
        Public loadedFromFile As Boolean = False
        Public sujet As String
        Public messageText As String
        Public des As String
        Public account As MailAccount
        Public attachements As String
        Public publipostageType As Integer
        Public toType As FacturationBox.DedicatedType
        Public maximumPrint As Integer = 0

        Public Sub saveToFile()
            System.IO.File.WriteAllText(appPath & bar(appPath) & "Data\publipostage.infos", _
            Me.sujet & vbCrLf & _
            Me.account.noMailAccount & ":" & Me.publipostageType & ":" & Me.maximumPrint & ":" & Me.toType & vbCrLf & _
            "BEGIN" & vbCrLf & Me.des & vbCrLf & "END" & vbCrLf & _
            "BEGIN" & vbCrLf & Me.attachements & vbCrLf & "END" & vbCrLf & _
            Me.messageText)
        End Sub

        Public Sub loadFromFile()
            loadedFromFile = True

            Dim data() As String = System.IO.File.ReadAllLines(appPath & bar(appPath) & "Data\publipostage.infos")
            Me.sujet = data(0)
            Dim options() As String = data(1).Split(":")
            Me.account = MailsManager.getInstance.getMailAccount(Integer.Parse(options(0)))
            Me.publipostageType = options(1)
            Me.maximumPrint = options(2)
            Me.toType = options(3)
            Dim curStep As Byte = 0
            For i As Integer = 2 To data.GetUpperBound(0)
                Select Case data(i)
                    Case "BEGIN"
                    Case "END"
                        curStep += 1
                    Case Else
                        Select Case curStep
                            Case 0
                                Me.des &= data(i)
                            Case 1
                                Me.attachements &= data(i)
                            Case 2
                                Me.messageText &= data(i)
                        End Select
                End Select
            Next i
        End Sub
    End Class

    Private Shared Sub sending(ByVal input As Object)
        Try
            Dim curInfos As SendingInfos = input

            Dim logoHeight As Double = 0.82
            Dim pref135 As Double = PreferencesManager.getGeneralPreferences()("PublipostageTopMargin").ToString.Replace(".", ",").Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
            logoHeight -= pref135
            PrintingHelper.printingInfos = New PrintingInfos(PrintingHelper.defHeader, PrintingHelper.defFooter, -1, pref135, -1, -1)

            Dim internalSendingMsg As String = ""
            Dim template As String = ""
            If IO.File.Exists(appPath & bar(appPath) & "Data\publipostage.html") Then template = IO.File.ReadAllText(appPath & bar(appPath) & "Data\publipostage.html")
            Dim myClinique(,) As String = DBLinker.getInstance.readDB("InfoClinique LEFT JOIN Villes ON Villes.NoVille = InfoClinique.NoVille", "Nom, Adresse, NomVille, CodePostal,LogoURL", "WHERE NoClinique=" & currentClinic)
            If template <> "" Then
                template = template.Replace("###LogoHeight###", logoHeight.ToString.Replace(",", "."))
                template = template.Replace("###Logo###", IIf(myClinique(4, 0) = "", "", "<img src=""" & myClinique(4, 0) & """>"))
                template = template.Replace("###Sujet###", curInfos.sujet)
                template = template.Replace("###Message###", curInfos.messageText)
                template = template.Replace("###spacing###", PreferencesManager.getGeneralPreferences()("PublipostageSpacing").ToString.Replace(",", "."))
                template = template.Replace("###Clinique###", myClinique(0, 0) & "<BR>" & myClinique(1, 0) & "<BR>" & myClinique(2, 0) & "&nbsp;&nbsp;" & myClinique(3, 0))
                internalSendingMsg = template
            Else
                internalSendingMsg = curInfos.messageText
            End If

            'Load from database the clients who wants the publipostage
            Dim whereStr As String = "WHERE Publipostage>0 AND Courriel<>''"
            If curInfos.publipostageType <> 3 Then whereStr = "WHERE Courriel<>'' AND Publipostage=" & curInfos.publipostageType

            Dim i, j As Integer
            Dim myPublipostageChoices(,) As String
            'Chargement des données
            If IO.File.Exists(appPath & bar(appPath) & "Data\publipostage.sav") Then
                Dim recoveryData() As String = IO.File.ReadAllLines(appPath & bar(appPath) & "Data\publipostage.sav")
                ReDim myPublipostageChoices(9, recoveryData.Length - 1)
                For i = 0 To myPublipostageChoices.GetUpperBound(1)
                    Dim lineData() As String = recoveryData(i).Split(New String() {vbTab}, StringSplitOptions.None)
                    For j = 0 To myPublipostageChoices.GetUpperBound(0)
                        myPublipostageChoices(j, i) = lineData(j)
                    Next j
                Next i
            Else
                Dim sqlStatement As String
                Select Case CType(input, SendingInfos).toType
                    Case FacturationBox.DedicatedType.Client
                        sqlStatement = "SELECT InfoClients.Nom, InfoClients.Prenom, InfoClients.Adresse, Villes.NomVille, InfoClients.CodePostal, InfoClients.NAM, InfoClients.Courriel, InfoClients.Publipostage, InfoClients.NoClient, 'C' FROM Villes RIGHT JOIN InfoClients ON Villes.NoVille = InfoClients.NoVille " & whereStr
                    Case FacturationBox.DedicatedType.KP
                        sqlStatement = "SELECT KeyPeople.Nom, '', KeyPeople.Adresse, Villes.NomVille, KeyPeople.CodePostal, KeyPeople.NoRef, KeyPeople.Courriel, KeyPeople.Publipostage, KeyPeople.NoKP, 'K' FROM Villes RIGHT JOIN KeyPeople ON Villes.NoVille = KeyPeople.NoVille " & whereStr
                    Case Else
                        sqlStatement = "SELECT InfoClients.Nom, InfoClients.Prenom, InfoClients.Adresse, Villes.NomVille, InfoClients.CodePostal, InfoClients.NAM, InfoClients.Courriel, InfoClients.Publipostage, InfoClients.NoClient, 'C' FROM Villes RIGHT JOIN InfoClients ON Villes.NoVille = InfoClients.NoVille " & whereStr & _
                                    "UNION ALL SELECT KeyPeople.Nom, '', KeyPeople.Adresse, Villes.NomVille, KeyPeople.CodePostal, KeyPeople.NoRef, KeyPeople.Courriel, KeyPeople.Publipostage, KeyPeople.NoKP, 'K' FROM Villes RIGHT JOIN KeyPeople ON Villes.NoVille = KeyPeople.NoVille " & whereStr
                End Select
                myPublipostageChoices = DBLinker.getInstance.readDB(sqlStatement)
            End If
            Dim selfOpened As Boolean = False
            Dim erronus As Integer = 0

            If Not myPublipostageChoices Is Nothing AndAlso myPublipostageChoices.Length <> 0 Then
                If IO.File.Exists(appPath & bar(appPath) & "Data\publipostage.sav") = False Then
                    'Enregistre les données en cas de plantage
                    Dim sb As New System.Text.StringBuilder()
                    For i = 0 To myPublipostageChoices.GetUpperBound(1)
                        For j = 0 To myPublipostageChoices.GetUpperBound(0)
                            sb.Append(myPublipostageChoices(j, i) & vbTab)
                        Next j
                        sb.AppendLine()
                    Next i
                    System.IO.File.WriteAllText(appPath & bar(appPath) & "Data\publipostage.sav", sb.ToString)
                    curInfos.saveToFile()
                End If

                'Début des envoies
                Dim prefNbSending As Integer = 0
                Dim interval As Integer = 0
                Integer.TryParse(PreferencesManager.getGeneralPreferences("PublipostageSendingInterval"), interval)
                Integer.TryParse(PreferencesManager.getGeneralPreferences("PublipostageNbSending"), prefNbSending)
                Dim intervalInMs As Integer = interval * 60000

                Dim printingWC As New WebTextControl()
                If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

                myMainWin.StatusText = "Début des envois pour le publipostage..."
                Dim sentEmails As New ArrayList
                Dim Printed, sent As Integer
                Dim min As Integer = 0
                If IO.File.Exists(appPath & bar(appPath) & "Data\publipostage.done") Then
                    Dim lines() As String = IO.File.ReadAllLines(appPath & bar(appPath) & "Data\publipostage.done")
                    min = lines.Length - 1
                    myMainWin.StatusText = "Le processus d'envoi pour le publipostage reprend et commence par entrer en pause pour une durée de " & interval & " minute(s)"
                    Threading.Thread.Sleep(intervalInMs)
                End If

                Dim nbSentByLot As Integer = 0

                Dim doneFile As IO.StreamWriter = System.IO.File.AppendText(appPath & bar(appPath) & "Data\publipostage.done")
                For i = min To myPublipostageChoices.GetUpperBound(1)
                    If myPublipostageChoices(7, i) = 2 Then 'Envoi par courriel
                        'Bloque l'envoi multiple si plusieurs comptes auraient la même adresse de courriel
                        If sentEmails.Contains(myPublipostageChoices(6, i)) = False Then
                            If emailSending(curInfos.account, "", myPublipostageChoices(6, i), "", "", curInfos.sujet, True, curInfos.messageText, curInfos.attachements, "", "Message envoyé " & IIf(myPublipostageChoices(9, i) = "C", "au client", "à la personne / organisme clé") & " ayant l'adresse de courriel " & myPublipostageChoices(6, i), False) Then
                                sentEmails.Add(myPublipostageChoices(6, i))
                                sent += 1
                                nbSentByLot += 1

                                If (myPublipostageChoices(9, i) = "C") Then
                                    addingComm(CInt(myPublipostageChoices(8, i)), 0, True, "Publipostage", "Envoi d'un courriel pour le publipostage", Date.Today, "Sujet du courriel : " & curInfos.sujet, , , False)
                                Else 'KP
                                    addingCommKP(CInt(myPublipostageChoices(8, i)), 0, True, "Publipostage", "Envoi d'un courriel pour le publipostage", Date.Today, "Sujet du courriel : " & curInfos.sujet, , False)
                                End If
                            Else
                                erronus += 1
                            End If
                        Else
                            If (myPublipostageChoices(9, i) = "C") Then
                                addingComm(CInt(myPublipostageChoices(8, i)), 0, True, "Publipostage", "Envoi d'un courriel pour le publipostage", Date.Today, "Sujet du courriel : " & curInfos.sujet, , , False)
                            Else 'KP
                                addingCommKP(CInt(myPublipostageChoices(8, i)), 0, True, "Publipostage", "Envoi d'un courriel pour le publipostage", Date.Today, "Sujet du courriel : " & curInfos.sujet, , False)
                            End If
                        End If
                    ElseIf myPublipostageChoices(7, i) = 1 Then 'Envoi par courrier (Impression)
                        Printed += 1
                        Dim printingHtml As String = internalSendingMsg
                        printingHtml = printingHtml.Replace("###To###", IIf(myPublipostageChoices(9, i) = "C", myPublipostageChoices(0, i) & "," & myPublipostageChoices(1, i), myPublipostageChoices(0, i)) & "<BR>" & myPublipostageChoices(2, i) & "<BR>" & myPublipostageChoices(3, i) & "&nbsp;&nbsp;" & myPublipostageChoices(4, i))

                        internalSending(printingHtml, IIf(myPublipostageChoices(9, i) = "C", myPublipostageChoices(0, i) & "," & myPublipostageChoices(1, i), myPublipostageChoices(0, i)), printingWC)
                        If (myPublipostageChoices(9, i) = "C") Then
                            addingComm(CInt(myPublipostageChoices(8, i)), 0, True, "Publipostage", "Envoi d'un courrier pour le publipostage", Date.Today, "Sujet du courrier : " & curInfos.sujet, , , False)
                        Else 'KP
                            addingCommKP(CInt(myPublipostageChoices(8, i)), 0, True, "Publipostage", "Envoi d'un courrier pour le publipostage", Date.Today, "Sujet du courrier : " & curInfos.sujet, , False)
                        End If

                        If Printed = curInfos.maximumPrint Then Exit For
                    End If

                    For j = 0 To myPublipostageChoices.GetUpperBound(0)
                        doneFile.Write(myPublipostageChoices(j, i) & vbTab)
                    Next j
                    doneFile.WriteLine()
                    doneFile.Flush()

                    If interval <> 0 AndAlso prefNbSending <> 0 AndAlso prefNbSending = nbSentByLot Then
                        'Maximum email sent by lot have been reached, so pausing time asked
                        nbSentByLot = 0
                        myMainWin.StatusText = "Le processus d'envoi pour le publipostage va entrer en pause pour une durée de " & interval & " minute(s)"
                        Threading.Thread.Sleep(intervalInMs)
                    Else
                        Threading.Thread.Sleep(500)
                    End If
                Next i

                doneFile.Close()
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.sav")
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.done")
                IO.File.Delete(appPath & bar(appPath) & "Data\publipostage.infos")

                PrintingHelper.setDefaultSystemPrinter(PrintingHelper.systemDefautPrinter)
                printingWC.Dispose()
                myMainWin.StatusText = "Fin des envois pour le publipostage (" & sent & " courriels envoyés (" & erronus & " erreurs) et " & Printed & " courriers imprimés" & ")"

                If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            End If

            PrintingHelper.printingInfos = Nothing
        Catch ex As Exception
            MessageBox.Show("Il y a eu une erreur lors de l'envoi du publipostage. Veuillez redémarrer le processus.", "Erreur publipostage", MessageBoxButtons.OK, MessageBoxIcon.Error)
            addErrorLog(ex)
        Finally
            MassMailing.sendingThread = Nothing
            lockSecteur("Publipostage.lock", False)
        End Try
        
    End Sub

    Private Shared Sub internalSending(ByVal html As String, ByVal title As String, ByRef printingWC As WebTextControl)
        PrintingHelper.printHtml(html, "Publipostage : " & title, False, , , False)
    End Sub

    Private Sub message_PageLoaded() Handles Message.pageLoaded
        If Not isLoaded Then
            'Vérifie si le secteur est déjà en cours d'utilisation
            If lockSecteur("Publipostage.lock", True, "Publipostage") = False Then
                Me.Close()
                Exit Sub
            End If

            If sendingThread IsNot Nothing Then
                MessageBox.Show("Impossible d'ouvrir la fenêtre du publipostage, car celui-ci est déjà en cours d'envoi. Veuillez attendre la fin de l'envoi en cours.", "Impossible d'ouvrir le publipostage", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If

            isLoaded = True
        End If

        Me.testPrint.Enabled = True
        Me.Envoyer.Enabled = True
        Me.btnApplyModel.Enabled = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub

    Private Sub btnApplyModel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApplyModel.Click
        Dim myContextMenu As ContextMenu = ModelsManager.getInstance.createModelsMenu(New Integer() {1, 6}, New EventHandler(AddressOf menumodelegen_Click), New EventHandler(AddressOf menumodeleperso_Click))
        myContextMenu.Show(btnApplyModel, New Point(btnApplyModel.Width, 0))
    End Sub


    Private Sub menumodelegen_Click(ByVal sender As Object, ByVal e As EventArgs)
        applyModele(CType(sender, MenuItem), 0)
    End Sub

    Private Sub menumodeleperso_Click(ByVal sender As Object, ByVal e As EventArgs)
        applyModele(CType(sender, MenuItem), ConnectionsManager.currentUser)
    End Sub

    Private Sub applyModele(ByVal myMenuItem As MenuItem, ByVal noUser As Integer)
        Dim myParentMenuItem As MenuItem
        Dim cat As String

        myParentMenuItem = CType(myMenuItem.Parent, MenuItem)

        If myParentMenuItem.Text = myParentMenuItem.GetContextMenu.MenuItems(0).Text Or myParentMenuItem.Text = myParentMenuItem.GetContextMenu.MenuItems(1).Text Then
            cat = "* Tous *"
        Else
            cat = myParentMenuItem.Text
        End If

        Dim myModele As String = ""
        Dim modele() As String = DBLinker.getInstance.readOneDBField("Modeles INNER JOIN ModelesCategories ON ModelesCategories.NoCategorie=Modeles.NoCategorie", "Modele", "WHERE NoUser" & IIf(noUser = 0, " IS null", "=" & noUser) & " AND Nom='" & myMenuItem.Text.Replace("'", "''") & "' AND Categorie='" & cat.Replace("'", "''") & "'")
        If modele IsNot Nothing AndAlso modele.Length <> 0 Then myModele = modele(0)

        'Ajoute le modèle à la boîte de texte actuel
        Message.insertHtml(myModele)
        Message.focus()
    End Sub

End Class
