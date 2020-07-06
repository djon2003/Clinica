Friend Class msgSending
    Inherits SingleWindow

    Private _IsAnswering As Boolean = False
    Private HTMLPageURL, htmlFileName As String
    Private WithEvents attachMenu As System.Windows.Forms.ContextMenuStrip
    Private WithEvents externeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnApplyModele As System.Windows.Forms.Button
    Private WithEvents banqueDeDonnéesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Private Sub addControlsNotSupportedByDesigner()
        Me.InterneA = New Clinica.TreeViewComboPlus()
        '
        'InterneA
        '
        Me.InterneA.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
        Me.InterneA.downText = "Cliquer sur la flèche pour cacher la liste"
        Me.InterneA.dropDownHeight = 200
        Me.InterneA.droppedDown = False
        Me.InterneA.expandAllNodes = False
        Me.InterneA.tooltipTitle = "Utilisateur(s) sélectionné(s) :"
        Me.InterneA.imageList = Nothing
        Me.InterneA.Location = New System.Drawing.Point(40, 40)
        Me.InterneA.Name = "InterneA"
        Me.InterneA.pathSeparator = "\"
        Me.InterneA.Size = New System.Drawing.Size(416, 20)
        Me.InterneA.TabIndex = 1
        Me.InterneA.TabStop = True
        Me.InterneA.tree = Nothing
        Me.InterneA.upText = "Cliquer sur la flèche pour afficher la liste"
        Me.InterneA.Visible = True
        Me.Controls.Add(Me.InterneA)
        Me.InterneA.BringToFront()
        Message.Select() 'BUGFIX: This line fix a bug. Without, the control InterneA is not shown in front of the tabs when the window is opened when other windows are already opened.
    End Sub


    Private Sub loading()
        Dim MySettings(), Tree(), Sujets(), ExterneAs(), CCs(), bcCs() As String
        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(False)
        Dim i As Short

        'Chargement du champ À Interne
        If users.Count <> 0 Then
            ReDim Tree(users.Count - 1)
            For Each curUser As User In users
                If curUser.noType = 0 OrElse curUser.getUserType.name = "* Défaut *" Then
                    Tree(i) = curUser.toString()
                Else
                    If searchArray(Tree, curUser.getUserType().name, SearchType.ExactMatch) < 0 Then ReDim Preserve Tree(Tree.Length) : Tree(Tree.Length - 1) = curUser.getUserType().name
                    Tree(i) = curUser.getUserType().name & "\" & curUser.toString()
                End If
                i += 1
            Next

            InterneA.expandAllNodes = True
            InterneA.tree = Tree
            InterneA.refreshTree()
        End If

        'Chargement du champ De Externe
        For Each curMailAccount As MailAccount In MailsManager.getInstance.getMailAccounts()
            If curMailAccount.canSendEmail AndAlso (currentDroitAcces(68) = False OrElse curMailAccount.commonAccount OrElse curMailAccount.inboxFolderName = UsersManager.currentUser.toString()) Then De.Items.Add(curMailAccount)
        Next
        If De.Items.Count > 0 Then De.SelectedIndex = 0
        If De.Items.Count = 0 OrElse currentDroitAcces(38) = False Then
            Externe.Enabled = False
            Both.Enabled = False
            Interne.Checked = True
            TabTypeMail.TabPages.Remove(TabPage2)
        End If

        'Chargement des listes Combo selon l'utilisateur
        Sujets = readFile("Users\Lists\" & ConnectionsManager.currentUser & "\mailsujets.lst", , False)
        msgObject.pathOfList = "Users\Lists\" & ConnectionsManager.currentUser & "\mailsujets.lst"
        ExterneAs = readFile("Users\Lists\" & ConnectionsManager.currentUser & "\mailexterneas.lst", , False)
        ExterneA.pathOfList = "Users\Lists\" & ConnectionsManager.currentUser & "\mailexterneas.lst"
        CCs = readFile("Users\Lists\" & ConnectionsManager.currentUser & "\mailccs.lst", , False)
        CC.pathOfList = "Users\Lists\" & ConnectionsManager.currentUser & "\mailccs.lst"
        bcCs = readFile("Users\Lists\" & ConnectionsManager.currentUser & "\mailbccs.lst", , False)
        BCC.pathOfList = "Users\Lists\" & ConnectionsManager.currentUser & "\mailbccs.lst"

        If Sujets(0).ToUpper.StartsWith("ERROR") = False Then msgObject.Items.AddRange(Sujets)
        If ExterneAs(0).ToUpper.StartsWith("ERROR") = False Then ExterneA.Items.AddRange(ExterneAs)
        If CCs(0).ToUpper.StartsWith("ERROR") = False Then CC.Items.AddRange(CCs)
        If bcCs(0).ToUpper.StartsWith("ERROR") = False Then BCC.Items.AddRange(bcCs)

        Message.htmlPageURL = appPath & bar(appPath) & emptyHTMLPath
        Message.Editing = True
        Message.showPage()

        'Settings
        Dim setting As String = UsersManager.currentUser.settings.sendMessage
        If setting <> "" Then
            MySettings = setting.Split(New Char() {"§"})
            FileDialog.InitialDirectory = MySettings(0)
            InterneA.dropDownHeight = MySettings(1)
        End If
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        addControlsNotSupportedByDesigner()
        Interne.Checked = True

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin
        Me.ToolTip1.ShowAlways = True
        Me.FileDialog.Filter = TypesFilesManager.getGeneralFiltering
        Me.FileDialog.Title = "Choix du fichier à attacher"
        Me.FileDialog.ShowReadOnly = False
        Me.FileDialog.ReadOnlyChecked = True

        Message.editorWidth = 455
        Message.editorHeight = 100
        Message.editorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")
        Message.ancreActif = True
        Message.ancre = PreferencesManager.getGeneralPreferences()("Ancre")
        Message.toolBarStyles = 2
        Me.AlertUser.Checked = PreferencesManager.getUserPreferences()("AlertUsersByDefOnMsgSending")

        'Loading
        '#If DEBUG Then
        loading()
        '#End If

        'Chargement des images
        With DrawingManager.getInstance
            Me.DelAttachedCompte.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.SelectCompte.Image = .getImage("selection16.gif")
            Me.ImmediateSending.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.SelectAffDate.Image = .getImage("selection16.gif")
            Me.AddBCC.Image = .getImage("selection16.gif")
            Me.AddCC.Image = .getImage("selection16.gif")
            Me.AddA.Image = .getImage("selection16.gif")
            Me.Envoyer.Image = .getImage("send16.gif")
            Me.AddAttach.Image = .getImage("ajouter16.gif")
            Me.Vider.Image = .getImage("eraser.jpg")
            Me.DelAttach.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.btnApplyModele.Image = .getImage("modeles16.gif")
            Me.Icon = DrawingManager.imageToIcon(.getImage("send16.gif"))
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
    Friend WithEvents msgObject As Clinica.ManagedCombo
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Vider As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Envoyer As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents AddAttach As System.Windows.Forms.Button
    Friend WithEvents DelAttach As System.Windows.Forms.Button
    Friend WithEvents SelectAffDate As System.Windows.Forms.Button
    Friend WithEvents AffDate As System.Windows.Forms.Label
    Friend WithEvents ImmediateSending As System.Windows.Forms.Button
    Friend WithEvents AlertUser As System.Windows.Forms.CheckBox
    Friend WithEvents FileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AddBCC As System.Windows.Forms.Button
    Friend WithEvents AddCC As System.Windows.Forms.Button
    Friend WithEvents AddA As System.Windows.Forms.Button
    Friend WithEvents BCC As Clinica.ManagedCombo
    Friend WithEvents CC As Clinica.ManagedCombo
    Friend WithEvents ExterneA As Clinica.ManagedCombo
    Friend WithEvents De As Clinica.ManagedCombo
    Friend WithEvents Attachements As ManagedText
    Friend WithEvents AttachedCompte As System.Windows.Forms.Label
    Friend WithEvents TabTypeMail As System.Windows.Forms.TabControl
    Friend WithEvents Interne As System.Windows.Forms.RadioButton
    Friend WithEvents Externe As System.Windows.Forms.RadioButton
    Friend WithEvents Both As System.Windows.Forms.RadioButton
    Friend WithEvents SelectCompte As System.Windows.Forms.Button
    Friend WithEvents DelAttachedCompte As System.Windows.Forms.Button
    Friend WithEvents InterneA As Clinica.TreeViewComboPlus
    Friend WithEvents Message As WebTextControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TabTypeMail = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.DelAttachedCompte = New System.Windows.Forms.Button
        Me.SelectCompte = New System.Windows.Forms.Button
        Me.AttachedCompte = New System.Windows.Forms.Label
        Me.ImmediateSending = New System.Windows.Forms.Button
        Me.AffDate = New System.Windows.Forms.Label
        Me.SelectAffDate = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.AlertUser = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.BCC = New Clinica.ManagedCombo
        Me.AddBCC = New System.Windows.Forms.Button
        Me.AddCC = New System.Windows.Forms.Button
        Me.AddA = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.CC = New Clinica.ManagedCombo
        Me.ExterneA = New Clinica.ManagedCombo
        Me.De = New Clinica.ManagedCombo
        Me.Vider = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Envoyer = New System.Windows.Forms.Button
        Me.AddAttach = New System.Windows.Forms.Button
        Me.DelAttach = New System.Windows.Forms.Button
        Me.btnApplyModele = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.FileDialog = New System.Windows.Forms.OpenFileDialog
        Me.Interne = New System.Windows.Forms.RadioButton
        Me.Externe = New System.Windows.Forms.RadioButton
        Me.Both = New System.Windows.Forms.RadioButton
        Me.attachMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.externeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.banqueDeDonnéesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Message = New Clinica.WebTextControl
        Me.Attachements = New ManagedText
        Me.msgObject = New Clinica.ManagedCombo
        Me.TabTypeMail.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.attachMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 144)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Objet :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 200)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Message :"
        '
        'TabTypeMail
        '
        Me.TabTypeMail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabTypeMail.Controls.Add(Me.TabPage1)
        Me.TabTypeMail.Controls.Add(Me.TabPage2)
        Me.TabTypeMail.Location = New System.Drawing.Point(8, 8)
        Me.TabTypeMail.Name = "TabTypeMail"
        Me.TabTypeMail.SelectedIndex = 0
        Me.TabTypeMail.Size = New System.Drawing.Size(464, 128)
        Me.TabTypeMail.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DelAttachedCompte)
        Me.TabPage1.Controls.Add(Me.SelectCompte)
        Me.TabPage1.Controls.Add(Me.AttachedCompte)
        Me.TabPage1.Controls.Add(Me.ImmediateSending)
        Me.TabPage1.Controls.Add(Me.AffDate)
        Me.TabPage1.Controls.Add(Me.SelectAffDate)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.AlertUser)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(456, 102)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Interne"
        '
        'DelAttachedCompte
        '
        Me.DelAttachedCompte.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelAttachedCompte.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelAttachedCompte.Location = New System.Drawing.Point(104, 75)
        Me.DelAttachedCompte.Name = "DelAttachedCompte"
        Me.DelAttachedCompte.Size = New System.Drawing.Size(24, 24)
        Me.DelAttachedCompte.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.DelAttachedCompte, "Enlever le compte client lié")
        '
        'SelectCompte
        '
        Me.SelectCompte.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SelectCompte.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.SelectCompte.Location = New System.Drawing.Point(72, 75)
        Me.SelectCompte.Name = "SelectCompte"
        Me.SelectCompte.Size = New System.Drawing.Size(24, 24)
        Me.SelectCompte.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.SelectCompte, "Sélectionner le compte client à lier")
        '
        'AttachedCompte
        '
        Me.AttachedCompte.AutoSize = True
        Me.AttachedCompte.Location = New System.Drawing.Point(144, 79)
        Me.AttachedCompte.Name = "AttachedCompte"
        Me.AttachedCompte.Size = New System.Drawing.Size(89, 13)
        Me.AttachedCompte.TabIndex = 17
        Me.AttachedCompte.Text = "Aucun compte lié"
        '
        'ImmediateSending
        '
        Me.ImmediateSending.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ImmediateSending.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ImmediateSending.Location = New System.Drawing.Point(136, 42)
        Me.ImmediateSending.Name = "ImmediateSending"
        Me.ImmediateSending.Size = New System.Drawing.Size(24, 24)
        Me.ImmediateSending.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.ImmediateSending, "Envoyer le message immédiatement")
        '
        'AffDate
        '
        Me.AffDate.AutoSize = True
        Me.AffDate.Location = New System.Drawing.Point(168, 46)
        Me.AffDate.Name = "AffDate"
        Me.AffDate.Size = New System.Drawing.Size(78, 13)
        Me.AffDate.TabIndex = 15
        Me.AffDate.Text = "Immédiatement"
        '
        'SelectAffDate
        '
        Me.SelectAffDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SelectAffDate.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.SelectAffDate.Location = New System.Drawing.Point(104, 42)
        Me.SelectAffDate.Name = "SelectAffDate"
        Me.SelectAffDate.Size = New System.Drawing.Size(24, 24)
        Me.SelectAffDate.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.SelectAffDate, "Sélectionner la date d'affichage du message")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 79)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 13)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Compte lié :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(8, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "À :"
        '
        'AlertUser
        '
        Me.AlertUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertUser.BackColor = System.Drawing.Color.Transparent
        Me.AlertUser.Location = New System.Drawing.Point(320, 80)
        Me.AlertUser.Name = "AlertUser"
        Me.AlertUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AlertUser.Size = New System.Drawing.Size(120, 16)
        Me.AlertUser.TabIndex = 6
        Me.AlertUser.Text = "Alerter l'utilisateur"
        Me.AlertUser.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Date d'affichage :"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BCC)
        Me.TabPage2.Controls.Add(Me.AddBCC)
        Me.TabPage2.Controls.Add(Me.AddCC)
        Me.TabPage2.Controls.Add(Me.AddA)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.CC)
        Me.TabPage2.Controls.Add(Me.ExterneA)
        Me.TabPage2.Controls.Add(Me.De)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(456, 102)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Externe"
        '
        'BCC
        '
        Me.BCC.acceptAlpha = True
        Me.BCC.acceptedChars = "!§#§|§$§%§?§&§*§-§@§.§_§=§+§£§¢§¤§¬§¦§²§³§¼§½§¾§^§¨§¸§`§'§«§»§°§~§¯§´§;§<§>§ "
        Me.BCC.acceptNumeric = True
        Me.BCC.allCapital = False
        Me.BCC.allLower = False
        Me.BCC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BCC.autoComplete = True
        Me.BCC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BCC.autoSizeDropDown = True
        Me.BCC.BackColor = System.Drawing.Color.White
        Me.BCC.blockOnMaximum = False
        Me.BCC.blockOnMinimum = False
        Me.BCC.cb_AcceptLeftZeros = False
        Me.BCC.cb_AcceptNegative = False
        Me.BCC.currencyBox = False
        Me.BCC.dbField = Nothing
        Me.BCC.doComboDelete = True
        Me.BCC.firstLetterCapital = False
        Me.BCC.firstLettersCapital = False
        Me.BCC.itemsToolTipDuration = 10000
        Me.BCC.Location = New System.Drawing.Point(40, 80)
        Me.BCC.manageText = True
        Me.BCC.matchExp = Nothing
        Me.BCC.maximum = 0
        Me.BCC.minimum = 0
        Me.BCC.Name = "BCC"
        Me.BCC.nbDecimals = CType(-1, Short)
        Me.BCC.onlyAlphabet = True
        Me.BCC.pathOfList = Nothing
        Me.BCC.ReadOnly = False
        Me.BCC.refuseAccents = False
        Me.BCC.refusedChars = ""
        Me.BCC.showItemsToolTip = False
        Me.BCC.Size = New System.Drawing.Size(384, 21)
        Me.BCC.TabIndex = 12
        Me.BCC.trimText = False
        '
        'AddBCC
        '
        Me.AddBCC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddBCC.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddBCC.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddBCC.Location = New System.Drawing.Point(424, 80)
        Me.AddBCC.Name = "AddBCC"
        Me.AddBCC.Size = New System.Drawing.Size(24, 20)
        Me.AddBCC.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.AddBCC, "Ajouter une adresse de courriel au champ 'CCC' à partir du carnet d'adresse")
        '
        'AddCC
        '
        Me.AddCC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddCC.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddCC.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddCC.Location = New System.Drawing.Point(424, 56)
        Me.AddCC.Name = "AddCC"
        Me.AddCC.Size = New System.Drawing.Size(24, 20)
        Me.AddCC.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.AddCC, "Ajouter une adresse de courriel au champ 'CC' à partir du carnet d'adresse")
        '
        'AddA
        '
        Me.AddA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddA.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddA.Location = New System.Drawing.Point(424, 32)
        Me.AddA.Name = "AddA"
        Me.AddA.Size = New System.Drawing.Size(24, 20)
        Me.AddA.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.AddA, "Ajouter une adresse de courriel au champ 'À' à partir du carnet d'adresse")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(8, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "CCC :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(8, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "CC :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(8, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "À :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(8, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "De :"
        '
        'CC
        '
        Me.CC.acceptAlpha = True
        Me.CC.acceptedChars = "!§#§|§$§%§?§&§*§-§@§.§_§=§+§£§¢§¤§¬§¦§²§³§¼§½§¾§^§¨§¸§`§'§«§»§°§~§¯§´§;§<§>§ "
        Me.CC.acceptNumeric = True
        Me.CC.allCapital = False
        Me.CC.allLower = False
        Me.CC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CC.autoComplete = True
        Me.CC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CC.autoSizeDropDown = True
        Me.CC.BackColor = System.Drawing.Color.White
        Me.CC.blockOnMaximum = False
        Me.CC.blockOnMinimum = False
        Me.CC.cb_AcceptLeftZeros = False
        Me.CC.cb_AcceptNegative = False
        Me.CC.currencyBox = False
        Me.CC.dbField = Nothing
        Me.CC.doComboDelete = True
        Me.CC.firstLetterCapital = False
        Me.CC.firstLettersCapital = False
        Me.CC.itemsToolTipDuration = 10000
        Me.CC.Location = New System.Drawing.Point(40, 56)
        Me.CC.manageText = True
        Me.CC.matchExp = Nothing
        Me.CC.maximum = 0
        Me.CC.minimum = 0
        Me.CC.Name = "CC"
        Me.CC.nbDecimals = CType(-1, Short)
        Me.CC.onlyAlphabet = True
        Me.CC.pathOfList = Nothing
        Me.CC.ReadOnly = False
        Me.CC.refuseAccents = False
        Me.CC.refusedChars = ""
        Me.CC.showItemsToolTip = False
        Me.CC.Size = New System.Drawing.Size(384, 21)
        Me.CC.TabIndex = 10
        Me.CC.trimText = False
        '
        'ExterneA
        '
        Me.ExterneA.acceptAlpha = True
        Me.ExterneA.acceptedChars = "!§#§|§$§%§?§&§*§-§@§.§_§=§+§£§¢§¤§¬§¦§²§³§¼§½§¾§^§¨§¸§`§'§«§»§°§~§¯§´§;§<§>§ "
        Me.ExterneA.acceptNumeric = True
        Me.ExterneA.allCapital = False
        Me.ExterneA.allLower = False
        Me.ExterneA.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExterneA.autoComplete = True
        Me.ExterneA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ExterneA.autoSizeDropDown = True
        Me.ExterneA.BackColor = System.Drawing.Color.White
        Me.ExterneA.blockOnMaximum = False
        Me.ExterneA.blockOnMinimum = False
        Me.ExterneA.cb_AcceptLeftZeros = False
        Me.ExterneA.cb_AcceptNegative = False
        Me.ExterneA.currencyBox = False
        Me.ExterneA.dbField = Nothing
        Me.ExterneA.doComboDelete = True
        Me.ExterneA.firstLetterCapital = False
        Me.ExterneA.firstLettersCapital = False
        Me.ExterneA.itemsToolTipDuration = 10000
        Me.ExterneA.Location = New System.Drawing.Point(40, 32)
        Me.ExterneA.manageText = True
        Me.ExterneA.matchExp = Nothing
        Me.ExterneA.maximum = 0
        Me.ExterneA.minimum = 0
        Me.ExterneA.Name = "ExterneA"
        Me.ExterneA.nbDecimals = CType(-1, Short)
        Me.ExterneA.onlyAlphabet = True
        Me.ExterneA.pathOfList = Nothing
        Me.ExterneA.ReadOnly = False
        Me.ExterneA.refuseAccents = False
        Me.ExterneA.refusedChars = ""
        Me.ExterneA.showItemsToolTip = False
        Me.ExterneA.Size = New System.Drawing.Size(384, 21)
        Me.ExterneA.TabIndex = 8
        Me.ExterneA.trimText = False
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
        Me.De.Location = New System.Drawing.Point(40, 8)
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
        Me.De.Size = New System.Drawing.Size(408, 21)
        Me.De.TabIndex = 7
        Me.De.trimText = False
        '
        'Vider
        '
        Me.Vider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Vider.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Vider.Location = New System.Drawing.Point(416, 0)
        Me.Vider.Name = "Vider"
        Me.Vider.Size = New System.Drawing.Size(24, 24)
        Me.Vider.TabIndex = 5
        Me.Vider.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Vider, "Vider les champs")
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
        Me.Envoyer.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Envoyer, "Envoyer le message")
        '
        'AddAttach
        '
        Me.AddAttach.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddAttach.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddAttach.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddAttach.Location = New System.Drawing.Point(448, 168)
        Me.AddAttach.Name = "AddAttach"
        Me.AddAttach.Size = New System.Drawing.Size(24, 24)
        Me.AddAttach.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.AddAttach, "Ajouter un attachement au message")
        '
        'DelAttach
        '
        Me.DelAttach.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DelAttach.Enabled = False
        Me.DelAttach.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelAttach.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelAttach.Location = New System.Drawing.Point(448, 192)
        Me.DelAttach.Name = "DelAttach"
        Me.DelAttach.Size = New System.Drawing.Size(24, 24)
        Me.DelAttach.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.DelAttach, "Enlever un attachement déjà attaché")
        '
        'btnApplyModele
        '
        Me.btnApplyModele.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyModele.Enabled = False
        Me.btnApplyModele.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnApplyModele.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnApplyModele.Location = New System.Drawing.Point(384, 0)
        Me.btnApplyModele.Name = "btnApplyModele"
        Me.btnApplyModele.Size = New System.Drawing.Size(24, 24)
        Me.btnApplyModele.TabIndex = 5
        Me.btnApplyModele.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnApplyModele, "Appliquer un modèle au message")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 168)
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
        'Interne
        '
        Me.Interne.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Interne.Location = New System.Drawing.Point(168, 8)
        Me.Interne.Name = "Interne"
        Me.Interne.Size = New System.Drawing.Size(64, 16)
        Me.Interne.TabIndex = 11
        Me.Interne.Text = "Interne"
        '
        'Externe
        '
        Me.Externe.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Externe.Location = New System.Drawing.Point(224, 8)
        Me.Externe.Name = "Externe"
        Me.Externe.Size = New System.Drawing.Size(64, 16)
        Me.Externe.TabIndex = 12
        Me.Externe.Text = "Externe"
        '
        'Both
        '
        Me.Both.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Both.Location = New System.Drawing.Point(288, 8)
        Me.Both.Name = "Both"
        Me.Both.Size = New System.Drawing.Size(72, 16)
        Me.Both.TabIndex = 13
        Me.Both.Text = "Les deux"
        '
        'AttachMenu
        '
        Me.attachMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.externeToolStripMenuItem, Me.banqueDeDonnéesToolStripMenuItem})
        Me.attachMenu.Name = "ContextMenuStrip1"
        Me.attachMenu.ShowImageMargin = False
        Me.attachMenu.ShowItemToolTips = False
        Me.attachMenu.Size = New System.Drawing.Size(183, 48)
        '
        'ExterneToolStripMenuItem
        '
        Me.externeToolStripMenuItem.Name = "ExterneToolStripMenuItem"
        Me.externeToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.externeToolStripMenuItem.Text = "De l'extérieur du logiciel"
        '
        'BanqueDeDonnéesToolStripMenuItem
        '
        Me.banqueDeDonnéesToolStripMenuItem.Name = "BanqueDeDonnéesToolStripMenuItem"
        Me.banqueDeDonnéesToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.banqueDeDonnéesToolStripMenuItem.Text = "De la banque de données"
        '
        'Message
        '
        Me.Message.activateLinksOnEdit = True
        Me.Message.allowContextMenu = True
        Me.Message.allowEditorContextMenu = True
        Me.Message.allowNavigation = False
        Me.Message.allowRefresh = False
        Me.Message.allowUndo = True
        Me.Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.ancre = Nothing
        Me.Message.ancreActif = False
        Me.Message.editorContextMenu = Nothing
        Me.Message.editorHeight = 350
        Me.Message.editorURL = ""
        Me.Message.editorWidth = 460
        Me.Message.htmlPageURL = Nothing
        Me.Message.Location = New System.Drawing.Point(8, 220)
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(464, 352)
        Me.Message.startupPos = 0
        Me.Message.TabIndex = 17
        Me.Message.toolBarStyles = 1
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
        Me.Attachements.Location = New System.Drawing.Point(88, 168)
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
        Me.Attachements.TabStop = False
        Me.Attachements.trimText = False
        '
        'Sujet
        '
        Me.msgObject.acceptAlpha = True
        Me.msgObject.acceptedChars = Nothing
        Me.msgObject.acceptNumeric = True
        Me.msgObject.allCapital = False
        Me.msgObject.allLower = False
        Me.msgObject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.msgObject.autoComplete = True
        Me.msgObject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.msgObject.autoSizeDropDown = True
        Me.msgObject.BackColor = System.Drawing.Color.White
        Me.msgObject.blockOnMaximum = False
        Me.msgObject.blockOnMinimum = False
        Me.msgObject.cb_AcceptLeftZeros = False
        Me.msgObject.cb_AcceptNegative = False
        Me.msgObject.currencyBox = False
        Me.msgObject.dbField = Nothing
        Me.msgObject.doComboDelete = True
        Me.msgObject.firstLetterCapital = True
        Me.msgObject.firstLettersCapital = False
        Me.msgObject.itemsToolTipDuration = 10000
        Me.msgObject.Location = New System.Drawing.Point(48, 144)
        Me.msgObject.manageText = True
        Me.msgObject.matchExp = ""
        Me.msgObject.maximum = 0
        Me.msgObject.minimum = 0
        Me.msgObject.Name = "Sujet"
        Me.msgObject.nbDecimals = CType(-1, Short)
        Me.msgObject.onlyAlphabet = False
        Me.msgObject.pathOfList = Nothing
        Me.msgObject.ReadOnly = False
        Me.msgObject.refuseAccents = False
        Me.msgObject.refusedChars = ""
        Me.msgObject.showItemsToolTip = False
        Me.msgObject.Size = New System.Drawing.Size(424, 21)
        Me.msgObject.Sorted = True
        Me.msgObject.TabIndex = 14
        Me.msgObject.trimText = False
        '
        'msgSending
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(482, 579)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.Both)
        Me.Controls.Add(Me.Externe)
        Me.Controls.Add(Me.Interne)
        Me.Controls.Add(Me.DelAttach)
        Me.Controls.Add(Me.AddAttach)
        Me.Controls.Add(Me.Attachements)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Envoyer)
        Me.Controls.Add(Me.Vider)
        Me.Controls.Add(Me.btnApplyModele)
        Me.Controls.Add(Me.msgObject)
        Me.Controls.Add(Me.TabTypeMail)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(490, 613)
        Me.Name = "msgSending"
        Me.ShowInTaskbar = False
        Me.Text = "Envoi d'un message"
        Me.TabTypeMail.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.attachMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Property isAnswering() As Boolean
        Get
            Return _IsAnswering
        End Get
        Set(ByVal value As Boolean)
            _IsAnswering = value
        End Set
    End Property

    Private Sub msgSending_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub immediateSending_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImmediateSending.Click
        AffDate.Text = "Immédiatement"
        AffDate.Tag = Nothing
    End Sub

    Private Sub tabTypeMail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabTypeMail.SelectedIndexChanged
        changingTab(TabTypeMail.SelectedIndex)
    End Sub

    Private Sub changingTab(ByVal index As Byte)
        If index = 0 Then
            If Both.Checked = False Then Interne.Checked = True
            InterneA.Visible = True
        Else
            If Both.Checked = False Then Externe.Checked = True
            InterneA.Visible = False
        End If
        TabTypeMail.SelectedIndex = index
    End Sub

    Private Sub typeMail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Interne.CheckedChanged, Externe.CheckedChanged
        If Both.Checked = True Then Exit Sub

        If Interne.Checked = False Then
            changingTab(1)
        Else
            changingTab(0)
        End If
    End Sub

    Private Sub selectAffDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAffDate.Click
        Dim myDateChoice As New DateChoice()
        Dim myChosenDate As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year, Date.Today.Year + 10, , True, True, , "23:59", False, Date.Today, , , , Date.Today, , , , , 1)
        If myChosenDate.Count = 0 Then Exit Sub

        AffDate.Text = DateFormat.getTextDate(myChosenDate(0)) & " " & DateFormat.getTextDate(myChosenDate(0), DateFormat.TextDateOptions.ShortTime)
        AffDate.Tag = myChosenDate(0)
    End Sub

    Private Sub selectCompte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectCompte.Click
        Dim myRecherche As New clientSearch()
        myRecherche.Visible = False
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()
    End Sub

    Private Sub delAttachedCompte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelAttachedCompte.Click
        AttachedCompte.Text = "Aucun compte lié"
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

    Private Sub addingEmailAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddA.Click, AddBCC.Click, AddCC.Click
        Dim myMsgAdressBook As msgAddressBook = openUniqueWindow(New msgAddressBook())
        myMsgAdressBook.useWinAsSelection = True
        Select Case CType(sender, Button).Name.ToLower
            Case "adda"
                myMsgAdressBook.from = Me.ExterneA
            Case "addbcc"
                myMsgAdressBook.from = Me.BCC
            Case "addcc"
                myMsgAdressBook.from = Me.CC
        End Select
        myMsgAdressBook.Show()
    End Sub

    Private Sub msgSending_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim fdPath As String
        If FileDialog.FileNames.Length > 0 Then
            fdPath = FileDialog.FileNames(0).Substring(0, FileDialog.FileNames(0).Length - getLastDir(FileDialog.FileNames(0)).Length - 1)
        Else
            fdPath = FileDialog.InitialDirectory
        End If
        'Save Settings
        Dim curUser As User = UsersManager.currentUser
        curUser.settings.sendMessage = fdPath & "§" & InterneA.dropDownHeight
        curUser.settings.saveData()
    End Sub

    Private Sub vider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Vider.Click
        InterneA.checkAll(False)
        immediateSending_Click(sender, e)
        delAttachedCompte_Click(sender, e)
        Attachements.Text = ""
        Message.sethtml(startingHTML, True)
        ExterneA.Text = ""
        BCC.Text = ""
        CC.Text = ""
        Me.AlertUser.Checked = PreferencesManager.getUserPreferences()("AlertUsersByDefOnMsgSending")
        msgObject.Text = ""
        Message.setPos(0)
        Message.focus()
    End Sub

    Private Sub envoyer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Envoyer.Click
        If (Externe.Checked = True Or Both.Checked = True) Then
            Dim cancel As Boolean = False
            If ExterneA.Text = "" Then
                MessageBox.Show("Veuillez entrer au moins une adresse de courriel à qui envoyer ce message", "Adresse manquante")
                cancel = True
            Else
                Dim fromEmailAccount As MailAccount = De.SelectedItem

                'Verify emails
                Dim emails() As String = extractEmails(ExterneA.Text).Split(New Char() {";"})
                For i As Integer = 0 To emails.GetUpperBound(0)
                    Dim emailValidation As EmailValidator.ValidationLevels = EmailValidator.isEmailValid(fromEmailAccount.email, emails(i))
                    If emailValidation <> EmailValidator.ValidationLevels.Valid Then
                        Dim message As String = String.Empty
                        Dim domain As String = emails(i).Substring(emails(i).IndexOf("@") + 1)
                        Select Case emailValidation
                            Case EmailValidator.ValidationLevels.WrongStructure
                                message = "Veuillez vous assurez que l'adresse de courriel soit valide :" & vbCrLf & "alias@domaine.extension" & vbCrLf & "Exemple : info@cints.net"

                            Case EmailValidator.ValidationLevels.DomainNotExists
                                message = "Le nom de domaine """ & domain & """ n'existe pas ou n'a pas de serveur de courriel"

                            Case EmailValidator.ValidationLevels.NotConfirmedByDomain
                                message = "L'adresse a été rejeté par le nom de domaine """ & domain & """"
                        End Select

                        MessageBox.Show(message, "Courriel invalide")
                        cancel = True
                        Exit For
                    End If
                Next i
            End If
            If cancel = True Then
                TabTypeMail.SelectedIndex = 1
                ExterneA.Focus()
                Exit Sub
            End If
        End If

        Dim usersTo() As String = InterneA.getSelected
        If Interne.Checked Or Both.Checked Then
            If usersTo.Length = 0 Then
                MessageBox.Show("Veuillez sélectionner au moins un utilisateur à qui envoyer le message", "Aucun utilisateur sélectionné")
                TabTypeMail.SelectedIndex = 0
                InterneA.droppedDown = True
                Exit Sub
            End If
        End If

        sendingText = Message.getHTML

        If Me.msgObject.Text = "" Then
            If MessageBox.Show("Avez-vous oublié d'entrer l'objet du message ?", "Objet vide", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                msgObject.Focus()
                Exit Sub
            End If
        End If

        If sendingText = "" Then
            If MessageBox.Show("Avez-vous oublié d'entrer du texte pour votre message ?", "Message vide", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Message.focus()
                Exit Sub
            End If
        End If

        Envoyer.Enabled = False
        Dim shouldClose As Boolean = True
        If Externe.Checked = True Or Both.Checked = True Then shouldClose = externalSending()
        If Interne.Checked = True Or Both.Checked = True Then internalSending(usersTo)

        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\mailsujets.lst", trimSubject(msgObject.Text), , True, 50)
        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\mailexterneas.lst", ExterneA.Text, , True, 50)
        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\mailccs.lst", CC.Text, , True, 50)
        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\mailbccs.lst", BCC.Text, , True, 50)

        If shouldClose Then Me.Close() Else Envoyer.Enabled = True
    End Sub

    Private Function trimSubject(ByVal subject As String) As String
        If subject.StartsWith("Tr:") OrElse subject.StartsWith("Re:") Then Return trimSubject(subject.Substring(3).Trim)

        Return subject
    End Function

    Private Function externalSending() As Boolean
        Return sendEmail(ExterneA.Text, msgObject.Text, sendingText, CC.Text, BCC.Text, Attachements.Text)
    End Function

    Private Function sendEmail(ByVal emailTo As String, ByVal subject As String, ByVal emailMessage As String, Optional ByVal emailCC As String = "", Optional ByVal emailBCC As String = "", Optional ByVal emailAttachs As String = "", Optional ByVal emailFrom As String = "", Optional ByVal showErrMsg As Boolean = True, Optional ByVal showLog As Boolean = True) As Boolean
        Dim curAccount As MailAccount = De.SelectedItem

        Dim noKeepMailFolder As Integer = getNoMailFolder_MailSent()
        If noKeepMailFolder <> 0 Then 'Keep trace of msg sent
            Dim dateAff As String = String.Empty
            If AffDate.Tag IsNot Nothing Then
                dateAff = AffDate.Text
            Else
                dateAff = DateFormat.getTextDate(Date.Today) & " " & Date.Now.Hour & ":" & Date.Now.Minute
            End If

            Dim noClient As Integer = 0
            If AttachedCompte.Text.StartsWith("Aucun") = False Then
                Dim sUserName() As String = AttachedCompte.Text.Split(New Char() {"("})
                noClient = sUserName(1).Substring(0, sUserName(1).Length - 1)
            End If

            Dim noMailSent As Integer = 0
            DBLinker.getInstance.writeDB("Mails", "NoMailFolder,NoUserFrom,[To],[From],AffDate,NoClient,Subject,IsRead,Message,FilesAttached,HasSentFeedBack", noKeepMailFolder & "," & ConnectionsManager.currentUser & ",'" & emailTo.Replace("'", "''") & "','" & emailFrom.Replace("'", "''") & "','" & dateAff & "'," & IIf(noClient = 0, "null", noClient) & ",'" & msgObject.Text.Replace("'", "''") & "',0,'" & sendingText.Replace("'", "''").Replace(vbCrLf, "\n") & "','',1", , , , noMailSent)
            addMailAttach(noMailSent)
        End If

        Dim sent As Boolean = emailSending(curAccount, "", emailTo, emailCC, emailBCC, subject, True, emailMessage, emailAttachs, IIf(showLog, Nothing, ""), IIf(showLog, Nothing, ""), showErrMsg, PreferencesManager.getUserPreferences()("ReceiveFeedBackForMessages"))

        If sent Then
            'Detect if an email is linked to a client account and if so, add a communication
            Dim emails As String = extractEmails(emailTo)
            For Each curEmail As String In emails.Split(New Char() {";"})
                Dim accounts() As String = DBLinker.getInstance().readOneDBField("InfoClients", "NoClient", "WHERE Courriel='" & curEmail.Replace("'", "''") & "'")
                If accounts IsNot Nothing Then
                    For i As Integer = 0 To accounts.Length - 1
                        Comptes.addingComm(accounts(i), 0, True, "Courriel", "Envoi d'un courriel au client", Date.Today, "Sujet du courriel : " & subject)
                    Next i
                End If
            Next
        End If

        Return sent
    End Function

    Private Sub addMailAttach(ByVal noMail As Integer)
        Dim filesAttached As String = ""
        filesAttached = ""
        Dim dirPath As String = "Data\Mails\" & noMail & "\attach"
        Dim basePath As String = appPath & bar(appPath) & dirPath
        IO.Directory.CreateDirectory(basePath)
        If Attachements.Text <> "" Then
            Dim sAttachements() As String = Attachements.Text.Split(New Char() {";"})
            For j As Integer = 0 To sAttachements.Length - 1
                If sAttachements(j) <> "" Then
                    If sAttachements(j).StartsWith("DB:\") Then
                        filesAttached &= "§DB|" & sAttachements(j).Substring(4)
                    Else
                        Dim fileName As String = getLastDir(sAttachements(j))
                        Dim extension As String = fileName.Substring(fileName.LastIndexOf(".") + 1)
                        Dim fileNo As Integer = genUniqueNo()
                        If IO.File.Exists(sAttachements(j)) = False Then
                            MessageBox.Show("Le fichier attaché (" & sAttachements(j) & ") n'existe plus. Il ne sera pas joint au message.", "Fichier inexistant")
                        Else
                            FileCopy(sAttachements(j), basePath & "\" & fileNo & "." & extension)
                            filesAttached &= "§FILE|" & fileName & "|" & fileNo & "." & extension
                        End If
                    End If
                End If
            Next j
        End If

        If filesAttached <> "" Then
            filesAttached = filesAttached.Substring(1)
            DBLinker.getInstance.updateDB("Mails", "FilesAttached='" & filesAttached.Replace("'", "''") & "'", "NoMail", noMail, False)
        End If
    End Sub

    Private Function getNoMailFolder_MailSent() As Integer
        Dim noKeepMailFolder As Integer = 0
        If PreferencesManager.getUserPreferences()("KeepATraceOfSentMsg") = True AndAlso PreferencesManager.getUserPreferences()("MailFolderForSentMsg") <> "" Then 'Recherche du numéro de dossier pour enregistrer les messages envoyés
            Dim sentFolder As MailFolder = MailsManager.getInstance.getMailFolder(MailFolder.getPath(ConnectionsManager.currentUser, PreferencesManager.getUserPreferences()("MailFolderForSentMsg")))
            If sentFolder IsNot Nothing AndAlso sentFolder.isSendingFolder = True Then noKeepMailFolder = sentFolder.noMailFolder

            If sentFolder Is Nothing Then 'On le crée s'il n'existe pas
                MailsManager.getInstance.addMailFolder("Utilisateurs\" & UsersManager.currentUser.toString() & "\" & PreferencesManager.getUserPreferences()("MailFolderForSentMsg"), , True)
                sentFolder = MailsManager.getInstance.getMailFolder(MailFolder.getPath(ConnectionsManager.currentUser, PreferencesManager.getUserPreferences()("MailFolderForSentMsg")))
                If sentFolder IsNot Nothing AndAlso sentFolder.isSendingFolder = True Then noKeepMailFolder = sentFolder.noMailFolder
            End If
        End If

        Return noKeepMailFolder
    End Function

    Private Sub internalSending(ByVal UsersToSend() As String)
        Dim UserName, sUserName() As String
        Dim i As Short
        Dim User, emailSent As Integer
        UsersToSend = InterneA.getSelected
        emailSent = 0

        Dim noUsers() As String = {}
        Dim n As Integer
        'fill NoUsers
        For i = 0 To UsersToSend.Length - 1
            If UsersToSend(i).IndexOf(",") <> -1 Then
                If UsersToSend(i).IndexOf("\") <> -1 Then
                    Dim sUsersToSend() As String = UsersToSend(i).Split(New Char() {"\"})
                    UserName = sUsersToSend(sUsersToSend.Length - 1)
                Else
                    UserName = UsersToSend(i)
                End If

                sUserName = UserName.Split(New Char() {"("})
                User = sUserName(1).Substring(0, sUserName(1).Length - 1)

                ReDim Preserve noUsers(n)
                noUsers(n) = User
                n += 1
            End If
        Next i

        Dim noMailFolders(,) As String = DBLinker.getInstance().readDB("MailFolders INNER JOIN Utilisateurs ON MailFolders.NoUser=Utilisateurs.NoUser", "NoMailFolder,MailFolders.NoUser,Courriel", "WHERE MailFolders.NoUser IN (" & String.Join(",", noUsers) & ") AND MailFolder=''")
        Dim noKeepMailFolder As Integer = getNoMailFolder_MailSent()

        Dim dateAff As String = String.Empty
        If AffDate.Tag IsNot Nothing Then
            dateAff = AffDate.Text
        Else
            dateAff = DateFormat.getTextDate(Date.Today) & " " & Date.Now.Hour & ":" & Date.Now.Minute
        End If

        Dim noClient As Integer = 0
        If AttachedCompte.Text.StartsWith("Aucun") = False Then
            sUserName = AttachedCompte.Text.Split(New Char() {"("})
            noClient = sUserName(1).Substring(0, sUserName(1).Length - 1)
        End If

        'Envoie des messages
        For i = 0 To noMailFolders.GetUpperBound(1)
            'Redirection du message par courriel
            If noMailFolders(2, i) <> "" AndAlso PreferencesManager.getInstance.getPreferences(noMailFolders(1, i))("RedirectInternMailToEmail") <> "" AndAlso PreferencesManager.getInstance.getPreferences(noMailFolders(1, i))("RedirectInternMailToEmail") = True Then
                Dim redirectMsg As String = MailsManager.fillReplyTemplate(sendingText, UsersManager.currentUser.toString(), AttachedCompte.Text, noMailFolders(2, i), DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Now, DateFormat.TextDateOptions.ShortTime), msgObject.Text, "Message redirigé par Clinica")
                If sendEmail(noMailFolders(2, i), "[Clinica] " & msgObject.Text, redirectMsg, , , Attachements.Text, , False, False) Then
                    emailSent += 1
                    Continue For
                    'Si le msg n'est pas envoyé, alors l'ajout à l'interne
                End If
            End If

            'Keep trace of msg sent
            Dim noMailSent As Integer = 0
            If noKeepMailFolder <> 0 Then
                DBLinker.getInstance.writeDB("Mails", "NoMailFolder,NoUserFrom,NoUserTo,AffDate,NoClient,Subject,IsRead,Message,FilesAttached,HasSentFeedBack", noKeepMailFolder & "," & ConnectionsManager.currentUser & "," & noMailFolders(1, i) & ",'" & dateAff & "'," & IIf(noClient = 0, "null", noClient) & ",'" & msgObject.Text.Replace("'", "''") & "',0,'" & sendingText.Replace("'", "''").Replace(vbCrLf, "\n") & "','',1", , , , noMailSent)
            End If

            'Écriture ds la BD
            Dim noMail As Integer = 0
            DBLinker.getInstance.writeDB("Mails", "NoMailFolder,NoUserFrom,NoUserTo,AffDate,NoClient,Subject,IsRead,Message,FilesAttached,HasSentFeedBack", noMailFolders(0, i) & "," & ConnectionsManager.currentUser & "," & noMailFolders(1, i) & ",'" & dateAff & "'," & IIf(noClient = 0, "null", noClient) & ",'" & msgObject.Text.Replace("'", "''") & "',0,'" & sendingText.Replace("'", "''").Replace(vbCrLf, "\n") & "',''," & IIf(PreferencesManager.getUserPreferences()("ReceiveFeedBackForMessages") = True, "0", "1"), , , , noMail)

            addMailAttach(noMail)
            If noMailSent <> 0 Then addMailAttach(noMailSent)

            InternalUpdatesManager.getInstance.sendUpdate("MessagesList(" & noMailFolders(0, i) & ")")
            If noMailSent <> 0 AndAlso noKeepMailFolder <> 0 Then InternalUpdatesManager.getInstance.sendUpdate("MessagesList(" & noKeepMailFolder & ")")
            If AlertUser.Checked = True Then AlertsManager.getInstance.addAlert("Nouveau message de : " & UsersManager.currentUser.toString() & vbCrLf & msgObject.Text, noMailFolders(1, i), , "Utilisateurs\" & UsersManager.getInstance.getUser(noMailFolders(1, i)).toString() & "\" & noMail, CDate(dateAff).AddDays(1), , , True, dateAff)

            emailSent += 1
        Next i

        If emailSent > 0 Then myMainWin.StatusText = "Message(s) interne(s) envoyé(s)"
    End Sub

    Private sendingText As String = ""

    Public Sub setmessage(ByVal message As String)
        sendingText = message
    End Sub

    Public Sub setsujet(ByVal sujet As String)
        Me.msgObject.Text = sujet
    End Sub

    Public Sub setCompteLie(ByVal noClient As Integer)
        If noClient = 0 Then
            Me.AttachedCompte.Text = "Aucun compte lié"
        Else
            Me.AttachedCompte.Text = getClientName(noClient) & " (" & noClient & ")"
        End If
    End Sub

    Public Sub setAttachements(ByVal attachments As String)
        Me.Attachements.Text = attachments
    End Sub

    Public Sub setExternalTo(ByVal email As String)
        If ExterneA.Text.Contains(email) = False Then
            If email.EndsWith(";") = False Then email &= ";"
            ExterneA.Text &= email
        End If
        If Me.Externe.Enabled Then Me.Externe.Checked = True
        Me.Message.focus()
    End Sub

    Public Sub setInternalTo(ByVal noUser As Integer)
        Dim nodePath As String = Me.InterneA.searchANode(UsersManager.getInstance.getUser(noUser).toString())
        If nodePath = "" Then Exit Sub

        Me.InterneA.selectANode(nodePath)
        Me.InterneA.droppedDown = False
        Me.Interne.Checked = True
        Me.Message.focus()
    End Sub

    Private Sub message_PageLoaded() Handles Message.pageLoaded
        pageLoaded()
    End Sub

    Private startingHTML As String = ""

    Private Sub pageLoaded()
        Dim emailSignatureModel As String = PreferencesManager.getUserPreferences()("EmailSignatureModel")
        Dim emailPathSplitterPos As Integer = emailSignatureModel.IndexOf("\")
        If emailPathSplitterPos <> -1 AndAlso ((_IsAnswering And PreferencesManager.getUserPreferences()("AddEmailSignOnAnswer")) Or (_IsAnswering = False And PreferencesManager.getUserPreferences()("AddEmailSignOnSend"))) Then
            Dim emailModelInfos() As String = emailSignatureModel.Split(New Char() {"\"})
            Dim modele() As String = DBLinker.getInstance.readOneDBField("Modeles INNER JOIN ModelesCategories ON ModelesCategories.NoCategorie=Modeles.NoCategorie", "Modele", "WHERE NoUser" & IIf(emailModelInfos(0) = 0, " IS null", "=" & emailModelInfos(0)) & " AND Nom='" & emailModelInfos(2).Replace("'", "''") & "' AND Categorie='" & emailModelInfos(1).Replace("'", "''") & "'")
            If modele IsNot Nothing AndAlso modele.Length <> 0 Then sendingText = "<br>" & modele(0) & sendingText
        End If

        startingHTML = sendingText

        Me.Message.setHtml(sendingText)
        Me.Message.setPos(0)
        Me.Message.focus()

        Me.Envoyer.Enabled = True
        Me.btnApplyModele.Enabled = True
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

    Private Sub btnApplyModele_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyModele.Click
        Dim myContextMenu As ContextMenu = ModelsManager.getInstance.createModelsMenu(New Integer() {1, 6}, New EventHandler(AddressOf menumodelegen_Click), New EventHandler(AddressOf menumodeleperso_Click))
        myContextMenu.Show(btnApplyModele, New Point(btnApplyModele.Width, 0))
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

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub

    Private Sub msgSending_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub msgSending_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged

    End Sub
End Class
