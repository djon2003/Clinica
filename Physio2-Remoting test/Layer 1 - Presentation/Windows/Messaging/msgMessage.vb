Friend Class msgMessage
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Message.editorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin

        'Chargement des images
        With DrawingManager.getInstance
            Me.printMsg.Image = DrawingManager.iconToImage(.getIcon("print16.ico"), New Size(16, 16))
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents De As ManagedText
    Friend WithEvents CompteLie As System.Windows.Forms.LinkLabel
    Friend WithEvents printMsg As System.Windows.Forms.Button
    Friend WithEvents sujet As ManagedText
    Friend WithEvents attachs As System.Windows.Forms.TextBox
    Friend WithEvents A As ManagedText
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents menuAttachs As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExécuterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnregistrerSousToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemDeLaBanqueDeDonnéesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CommunicationDunCompteClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CommunicationDunComptePersonneorganismeCléToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msgDate As ManagedText
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Message As WebTextControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.printMsg = New System.Windows.Forms.Button
        Me.CompteLie = New System.Windows.Forms.LinkLabel
        Me.attachs = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.menuAttachs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExécuterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EnregistrerSousToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ItemDeLaBanqueDeDonnéesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CommunicationDunCompteClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CommunicationDunComptePersonneorganismeCléToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Message = New WebTextControl
        Me.A = New ManagedText
        Me.De = New ManagedText
        Me.sujet = New ManagedText
        Me.msgDate = New ManagedText
        Me.Label7 = New System.Windows.Forms.Label
        Me.menuAttachs.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Compte lié :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Sujet :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Message :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Attachements :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "De :"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'printMsg
        '
        Me.printMsg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.printMsg.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.printMsg.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.printMsg.Location = New System.Drawing.Point(527, 119)
        Me.printMsg.Name = "printMsg"
        Me.printMsg.Size = New System.Drawing.Size(24, 24)
        Me.printMsg.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.printMsg, "Imprimer le message")
        Me.printMsg.UseVisualStyleBackColor = True
        '
        'CompteLie
        '
        Me.CompteLie.AutoSize = True
        Me.CompteLie.Location = New System.Drawing.Point(89, 66)
        Me.CompteLie.Name = "CompteLie"
        Me.CompteLie.Size = New System.Drawing.Size(38, 13)
        Me.CompteLie.TabIndex = 10
        Me.CompteLie.TabStop = True
        Me.CompteLie.Text = "Aucun"
        '
        'attachs
        '
        Me.attachs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.attachs.BackColor = System.Drawing.SystemColors.Window
        Me.attachs.Location = New System.Drawing.Point(92, 109)
        Me.attachs.Multiline = True
        Me.attachs.Name = "attachs"
        Me.attachs.ReadOnly = True
        Me.attachs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.attachs.Size = New System.Drawing.Size(429, 34)
        Me.attachs.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 29)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "À :"
        '
        'menuAttachs
        '
        Me.menuAttachs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExécuterToolStripMenuItem, Me.EnregistrerSousToolStripMenuItem})
        Me.menuAttachs.Name = "ContextMenuStrip1"
        Me.menuAttachs.Size = New System.Drawing.Size(167, 48)
        '
        'ExécuterToolStripMenuItem
        '
        Me.ExécuterToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ExécuterToolStripMenuItem.Name = "ExécuterToolStripMenuItem"
        Me.ExécuterToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ExécuterToolStripMenuItem.Text = "Exécuter"
        '
        'EnregistrerSousToolStripMenuItem
        '
        Me.EnregistrerSousToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItemDeLaBanqueDeDonnéesToolStripMenuItem, Me.CommunicationDunCompteClientToolStripMenuItem, Me.CommunicationDunComptePersonneorganismeCléToolStripMenuItem})
        Me.EnregistrerSousToolStripMenuItem.Name = "EnregistrerSousToolStripMenuItem"
        Me.EnregistrerSousToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.EnregistrerSousToolStripMenuItem.Text = "Enregistrer sous..."
        '
        'ItemDeLaBanqueDeDonnéesToolStripMenuItem
        '
        Me.ItemDeLaBanqueDeDonnéesToolStripMenuItem.Name = "ItemDeLaBanqueDeDonnéesToolStripMenuItem"
        Me.ItemDeLaBanqueDeDonnéesToolStripMenuItem.Size = New System.Drawing.Size(363, 22)
        Me.ItemDeLaBanqueDeDonnéesToolStripMenuItem.Text = "Item de la banque de données"
        '
        'CommunicationDunCompteClientToolStripMenuItem
        '
        Me.CommunicationDunCompteClientToolStripMenuItem.Name = "CommunicationDunCompteClientToolStripMenuItem"
        Me.CommunicationDunCompteClientToolStripMenuItem.Size = New System.Drawing.Size(363, 22)
        Me.CommunicationDunCompteClientToolStripMenuItem.Text = "Communication d'un compte client"
        '
        'CommunicationDunComptePersonneorganismeCléToolStripMenuItem
        '
        Me.CommunicationDunComptePersonneorganismeCléToolStripMenuItem.Name = "CommunicationDunComptePersonneorganismeCléToolStripMenuItem"
        Me.CommunicationDunComptePersonneorganismeCléToolStripMenuItem.Size = New System.Drawing.Size(363, 22)
        Me.CommunicationDunComptePersonneorganismeCléToolStripMenuItem.Text = "Communication d'un compte personne/organisme clé"
        '
        'Message
        '
        Me.Message.activateLinksOnEdit = True
        Me.Message.allowContextMenu = True
        Me.Message.allowEditorContextMenu = True
        Me.Message.allowNavigation = False
        Me.Message.allowPopupWindows = True
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
        Me.Message.Location = New System.Drawing.Point(11, 149)
        Me.Message.Name = "Message"
        Me.Message.Silent = False
        Me.Message.Size = New System.Drawing.Size(540, 313)
        Me.Message.startupPos = 0
        Me.Message.TabIndex = 20
        Me.Message.toolBarStyles = 1
        Me.Message.viewDisableHtmlFields = False
        '
        'A
        '
        Me.A.acceptAlpha = True
        Me.A.acceptedChars = ""
        Me.A.acceptNumeric = True
        Me.A.allCapital = False
        Me.A.allLower = False
        Me.A.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.A.BackColor = System.Drawing.SystemColors.Control
        Me.A.blockOnMaximum = False
        Me.A.blockOnMinimum = False
        Me.A.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.A.cb_AcceptLeftZeros = False
        Me.A.cb_AcceptNegative = False
        Me.A.currencyBox = False
        Me.A.firstLetterCapital = False
        Me.A.firstLettersCapital = False
        Me.A.Location = New System.Drawing.Point(92, 29)
        Me.A.manageText = True
        Me.A.matchExp = ""
        Me.A.maximum = 0
        Me.A.minimum = 0
        Me.A.Name = "A"
        Me.A.nbDecimals = CType(-1, Short)
        Me.A.onlyAlphabet = False
        Me.A.refuseAccents = False
        Me.A.refusedChars = ""
        Me.A.showInternalContextMenu = True
        Me.A.Size = New System.Drawing.Size(459, 13)
        Me.A.TabIndex = 1
        Me.A.trimText = False
        '
        'De
        '
        Me.De.acceptAlpha = True
        Me.De.acceptedChars = ""
        Me.De.acceptNumeric = True
        Me.De.allCapital = False
        Me.De.allLower = False
        Me.De.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.De.BackColor = System.Drawing.SystemColors.Control
        Me.De.blockOnMaximum = False
        Me.De.blockOnMinimum = False
        Me.De.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.De.cb_AcceptLeftZeros = False
        Me.De.cb_AcceptNegative = False
        Me.De.currencyBox = False
        Me.De.firstLetterCapital = False
        Me.De.firstLettersCapital = False
        Me.De.Location = New System.Drawing.Point(92, 10)
        Me.De.manageText = True
        Me.De.matchExp = ""
        Me.De.maximum = 0
        Me.De.minimum = 0
        Me.De.Name = "De"
        Me.De.nbDecimals = CType(-1, Short)
        Me.De.onlyAlphabet = False
        Me.De.refuseAccents = False
        Me.De.refusedChars = ""
        Me.De.showInternalContextMenu = True
        Me.De.Size = New System.Drawing.Size(459, 13)
        Me.De.TabIndex = 1
        Me.De.trimText = False
        '
        'sujet
        '
        Me.sujet.acceptAlpha = True
        Me.sujet.acceptedChars = ""
        Me.sujet.acceptNumeric = True
        Me.sujet.allCapital = False
        Me.sujet.allLower = False
        Me.sujet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sujet.BackColor = System.Drawing.SystemColors.Control
        Me.sujet.blockOnMaximum = False
        Me.sujet.blockOnMinimum = False
        Me.sujet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.sujet.cb_AcceptLeftZeros = False
        Me.sujet.cb_AcceptNegative = False
        Me.sujet.currencyBox = False
        Me.sujet.firstLetterCapital = False
        Me.sujet.firstLettersCapital = False
        Me.sujet.Location = New System.Drawing.Point(92, 89)
        Me.sujet.manageText = True
        Me.sujet.matchExp = ""
        Me.sujet.maximum = 0
        Me.sujet.minimum = 0
        Me.sujet.Name = "sujet"
        Me.sujet.nbDecimals = CType(-1, Short)
        Me.sujet.onlyAlphabet = False
        Me.sujet.refuseAccents = False
        Me.sujet.refusedChars = ""
        Me.sujet.showInternalContextMenu = True
        Me.sujet.Size = New System.Drawing.Size(459, 13)
        Me.sujet.TabIndex = 2
        Me.sujet.trimText = False
        '
        'msgDate
        '
        Me.msgDate.acceptAlpha = True
        Me.msgDate.acceptedChars = ""
        Me.msgDate.acceptNumeric = True
        Me.msgDate.allCapital = False
        Me.msgDate.allLower = False
        Me.msgDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.msgDate.BackColor = System.Drawing.SystemColors.Control
        Me.msgDate.blockOnMaximum = False
        Me.msgDate.blockOnMinimum = False
        Me.msgDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.msgDate.cb_AcceptLeftZeros = False
        Me.msgDate.cb_AcceptNegative = False
        Me.msgDate.currencyBox = False
        Me.msgDate.firstLetterCapital = False
        Me.msgDate.firstLettersCapital = False
        Me.msgDate.Location = New System.Drawing.Point(92, 48)
        Me.msgDate.manageText = True
        Me.msgDate.matchExp = ""
        Me.msgDate.maximum = 0
        Me.msgDate.minimum = 0
        Me.msgDate.Name = "msgDate"
        Me.msgDate.nbDecimals = CType(-1, Short)
        Me.msgDate.onlyAlphabet = False
        Me.msgDate.refuseAccents = False
        Me.msgDate.refusedChars = ""
        Me.msgDate.showInternalContextMenu = True
        Me.msgDate.Size = New System.Drawing.Size(459, 13)
        Me.msgDate.TabIndex = 1
        Me.msgDate.trimText = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Date :"
        '
        'msgMessage
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(563, 474)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.attachs)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.De)
        Me.Controls.Add(Me.printMsg)
        Me.Controls.Add(Me.CompteLie)
        Me.Controls.Add(Me.A)
        Me.Controls.Add(Me.msgDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.sujet)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(571, 508)
        Me.Name = "msgMessage"
        Me.ShowInTaskbar = False
        Me.Text = "Visionnement d'un message"
        Me.menuAttachs.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Const ATTACHMENT_SPLITTER As String = " | "
    Private _NoMail As Integer = 0
    Private currentSelectionStart As Integer = 0
    Private currentSelectionLength As Integer = 0

    Public Property noMail() As Integer
        Get
            Return _NoMail
        End Get
        Set(ByVal value As Integer)
            _NoMail = value
        End Set
    End Property

    Private Sub msgMessage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loading()
    End Sub


    Public Sub loading()
        If _NoMail = 0 Then Exit Sub

        Dim myInfos(,) As String = DBLinker.getInstance.readDB("Mails", "*", "WHERE NoMail=" & _NoMail)
        If myInfos(5, 0) <> "" AndAlso myInfos(2, 0) = "" Then
            De.Text = UsersManager.getInstance.getUser(myInfos(5, 0)).toString()
        Else
            De.Text = myInfos(2, 0)
        End If
        If myInfos(6, 0) <> "" AndAlso myInfos(6, 0) <> "0" AndAlso myInfos(3, 0) = "" Then
            A.Text = UsersManager.getInstance.getUser(myInfos(6, 0)).toString()
        Else
            A.Text = myInfos(3, 0)
        End If
        msgDate.Text = DateFormat.getTextDate(CDate(myInfos(7, 0))) & " " & DateFormat.getTextDate(CDate(myInfos(7, 0)), DateFormat.TextDateOptions.FullTime)
        Message.sethtml(myInfos(11, 0).Replace("\n", vbCrLf))
        sujet.Text = myInfos(9, 0)

        If myInfos(8, 0) <> "" AndAlso myInfos(8, 0) <> 0 Then
            CompteLie.Text = getClientName(myInfos(8, 0)) & " (" & myInfos(8, 0) & ")"
        End If

        attachs.Tag = myInfos(12, 0)

        If myInfos(12, 0) <> "" Then
            Dim attachFiles() As String = myInfos(12, 0).Split(New Char() {"§"})
            Dim attached As String = ""
            For i As Integer = 0 To attachFiles.GetUpperBound(0)
                Dim file() As String = attachFiles(i).Split(New Char() {"|"})
                attached &= file(1) & ATTACHMENT_SPLITTER
            Next i
            'If Attached <> "" Then Attached = Attached.Substring(1)
            attachs.Text = attached
        End If
    End Sub

    Private Sub selectAttachement(Optional ByVal keyDowned As Boolean = False)
        Dim LeftPos, RightPos, i As Integer
        If (attachs.SelectionStart - ATTACHMENT_SPLITTER.Length) > 0 Then
            For i = attachs.SelectionStart - ATTACHMENT_SPLITTER.Length To 0 Step -1
                If attachs.Text.Substring(i, ATTACHMENT_SPLITTER.Length) = ATTACHMENT_SPLITTER Then LeftPos = i + 1 : Exit For
            Next i
        Else
            If currentSelectionStart <> 0 OrElse keyDowned = False Then
                LeftPos = 0
            Else
                LeftPos = attachs.Text.Length
            End If
        End If

        For i = attachs.SelectionStart To attachs.Text.Length - ATTACHMENT_SPLITTER.Length
            If attachs.Text.Substring(i, ATTACHMENT_SPLITTER.Length) = ATTACHMENT_SPLITTER Then RightPos = i + 1 : Exit For
        Next i

        If LeftPos <> 0 Then LeftPos += ATTACHMENT_SPLITTER.Length - 2
        attachs.SelectionStart = LeftPos
        If (RightPos - LeftPos) > 0 Then attachs.SelectionLength = RightPos - LeftPos

        currentSelectionStart = Me.attachs.SelectionStart
        currentSelectionLength = Me.attachs.SelectionLength
    End Sub

    Private Sub compteLie_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles CompteLie.LinkClicked
        If CompteLie.Text <> "Aucun" Then
            Dim sNoClient() As String = CompteLie.Text.Split(New Char() {"("})
            Dim noClient As String = sNoClient(1).Substring(0, sNoClient(1).Length - 1)
            openAccount(noClient)
        End If
    End Sub

    Private Sub printMsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles printMsg.Click
        Dim html As String = MailsManager.fillReplyTemplate(Message.getHTML(), De.Text, CompteLie.Text, A.Text, msgDate.Text, sujet.Text, "Message imprimé")
        PrintingHelper.printHtml(html, Me.Text & " : " & sujet.Text)
    End Sub

    Private Sub attachs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles attachs.Click
        selectAttachement()
    End Sub

    Private Function getAttachIndex() As Integer
        If Me.attachs.Text.Length = 0 Then Return -1

        Me.attachs.SelectionStart = currentSelectionStart
        Me.attachs.SelectionLength = currentSelectionLength

        Dim curAttachPos As String = Me.attachs.Text.Insert(Me.attachs.SelectionStart + Me.attachs.SelectionLength - 2, ATTACHMENT_SPLITTER & "CLIN$$$$ICA" & ATTACHMENT_SPLITTER)
        Dim attachs() As String = curAttachPos.Split(New String() {ATTACHMENT_SPLITTER}, StringSplitOptions.None)
        Dim curAttachIndex As Integer
        For i As Integer = 0 To attachs.GetUpperBound(0)
            If attachs(i) = "CLIN$$$$ICA" Then curAttachIndex = i - 1 : Exit For
        Next i

        Return curAttachIndex
    End Function

    Private Sub attachs_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles attachs.DoubleClick
        If Me.attachs.Text = "" Then Exit Sub
        If Me.attachs.SelectionStart = Me.attachs.Text.Length Then Exit Sub

        Dim curAttachIndex As Integer = getAttachIndex()

        Dim attachsData() As String = Me.attachs.Tag.ToString.Split(New Char() {"§"})
        Dim file() As String = attachsData(curAttachIndex).Split(New Char() {"|"})
        Dim fullPath As String = ""
        Dim options As IOpenableOptions
        If file(0) = "DB" Then
            fullPath = WebTextControl.PROTOCOL_CLINICA & attachsData(curAttachIndex)
        ElseIf file(0) = "FILE" Then
            options = New InternalType_Text.InternalType_Text_Options(file(1))
            fullPath = appPath & bar(appPath) & "Data\Mails\" & Me.noMail & "\attach\" & file(2)
        End If

        If fullPath <> "" Then TypesFilesOpener.getInstance.open(fullPath, options)
    End Sub

    Private Sub attachs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles attachs.KeyDown
        Dim i, selStartPos As Integer
        Dim doKeyDowned As Boolean = False

        Select Case e.KeyCode
            Case Keys.Enter
                e.SuppressKeyPress = True
                attachs_DoubleClick(sender, EventArgs.Empty)
            Case 37
                doKeyDowned = True
                For i = attachs.SelectionStart - ATTACHMENT_SPLITTER.Length To 0 Step -1
                    If attachs.Text.Substring(i, ATTACHMENT_SPLITTER.Length) = ATTACHMENT_SPLITTER Then selStartPos = i + ATTACHMENT_SPLITTER.Length : Exit For
                Next i

                If selStartPos < 0 Then selStartPos = 0
            Case 39
                For i = attachs.SelectionStart To attachs.Text.Length - ATTACHMENT_SPLITTER.Length
                    If attachs.Text.Substring(i, ATTACHMENT_SPLITTER.Length) = ATTACHMENT_SPLITTER Then selStartPos = i + ATTACHMENT_SPLITTER.Length + 1 : Exit For
                Next i

                If selStartPos > attachs.Text.Length Then selStartPos = attachs.Text.Length
        End Select

        attachs.SelectionStart = selStartPos
        attachs.ScrollToCaret()
        selectAttachement(doKeyDowned)
        e.Handled = True
    End Sub

    Private Sub exécuterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExécuterToolStripMenuItem.Click
        attachs_DoubleClick(sender, e)
    End Sub

    Private Sub itemDeLaBanqueDeDonnéesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemDeLaBanqueDeDonnéesToolStripMenuItem.Click
        saveToDB()
    End Sub

    Private Sub communicationDunCompteClientToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CommunicationDunCompteClientToolStripMenuItem.Click
        saveToClient()
    End Sub

    Private Sub communicationDunComptePersonneorganismeCléToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CommunicationDunComptePersonneorganismeCléToolStripMenuItem.Click
        saveToKP()
    End Sub

    Private fileAttachToSave As String = ""

    Private Sub saveToDB()
        Dim curAttachIndex As Integer = getAttachIndex()

        Dim attachsData() As String = Me.attachs.Tag.ToString.Split(New Char() {"§"})
        Dim file() As String = attachsData(curAttachIndex).Split(New Char() {"|"})
        Dim curPath As String = ""
        Dim curFilename As String = ""
        Dim realFileName As String = ""
        Dim fullPath As String = ""
        curPath = "Data\Mails\" & Me.noMail & "\attach"
        curFilename = file(2)
        realFileName = file(1)
        fullPath = appPath & bar(appPath) & curPath
        Dim curExt As String = curFilename.Substring(curFilename.LastIndexOf(".") + 1)
        Dim curType As TypeFile = TypesFilesManager.getInstance.getTypeFileFromExt(curExt)
        fileAttachToSave = fullPath & "\" & curFilename

        Dim myInputBoxPlus As New InputBoxPlus(True, "Users\Lists\" & ConnectionsManager.currentUser & "\attachtitles.lst")
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = "\§/§:§*§?§""§<§>§|§%"
        Dim myNom As String = myInputBoxPlus("Veuillez entrer le nom que vous voulez donner à l'item", "Nom de l'item", realFileName.Substring(0, realFileName.LastIndexOf(".")))
        If myNom = "" Then Exit Sub

        Dim mySelectDBCat As New SelectDBCat
        Dim myPath As String = mySelectDBCat("")
        If myPath = "" Then Exit Sub

        Dim myMotsCles() As String = {"Importé depuis un message"}
        Dim myDescription() As String = {""}

        AddHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
        Dim returning As String = InternalDBManager.getInstance.addItem(myNom, InternalDBManager.getInstance.getDBFolder(myPath), curType.fileType, False, myMotsCles, myDescription, False, True, myNom & "." & curExt)
        If returning <> "" Then
            MessageBox.Show(returning, "Erreur")
            RemoveHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
            Exit Sub
        End If
        If curType.isInternal = False Then
            Dim curDBItem As New InternalDBItem(myPath, myNom)
            IO.File.Copy(fileAttachToSave, appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile, True)
        End If
        RemoveHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving

        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\attachtitles.lst", myNom, , True, 15, False)
    End Sub

    Private Sub internalFileSaving(ByVal type As String, ByVal filePath As String)
        'REM Why this is raised multiple times ??
        IO.File.Copy(fileAttachToSave, filePath, True)
    End Sub

    Private Sub saveToClient()
        'Sélection de la catégorie
        Dim myInputBoxPlus As New InputBoxPlus(True, , "CommCategories.Categorie")
        Dim cat As String = myInputBoxPlus("Veuillez choisir une catégorie pour cette communication", "Catégorie de la communication")
        If cat = "" AndAlso MessageBox.Show("Êtes-vous sûr de ne pas vouloir entrer de catégorie ?", "Catégorie vide", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim curAttachIndex As Integer = getAttachIndex()

        Dim attachsData() As String = Me.attachs.Tag.ToString.Split(New Char() {"§"})
        Dim file() As String = attachsData(curAttachIndex).Split(New Char() {"|"})
        Dim curPath As String = ""
        Dim curFilename As String = ""
        Dim realFileName As String = ""
        Dim fullPath As String = ""
        Dim curExt As String = ""
        If file(0) = "DB" Then
            curFilename = getLastDir(file(1))
            curPath = file(1).Substring(0, file(1).Length - (curFilename.Length + 1))
            Dim curDBItem As New InternalDBItem(curPath, curFilename)
            realFileName = "DB|" & curDBItem.noDBItem & "\" & curPath & "\" & curFilename
        ElseIf file(0) = "FILE" Then
            curPath = "Data\Mails\" & Me.noMail & "\attach"
            curFilename = file(1)
            fullPath = appPath & bar(appPath) & curPath & "\" & file(2)
            Dim newCommNo As Integer = genUniqueNo()
            curExt = curFilename.Substring(realFileName.LastIndexOf(".") + 1)
            realFileName = "FILE|" & newCommNo & "." & curExt
        End If

        Dim myRecherche As New clientSearch
        myRecherche.from = Me
        myRecherche.Visible = False
        myRecherche.MdiParent = Nothing
        Dim nbFoundClient As Integer = foundClient.Length
        myRecherche.ShowDialog()
        If nbFoundClient < foundClient.Length Then
            If file(0) = "FILE" Then
                Dim copyPath As String = appPath & bar(appPath) & "Clients\" & foundClient(foundClient.Length - 1).noClient & "\Comm"
                IO.Directory.CreateDirectory(copyPath)
                IO.File.Copy(fullPath, copyPath & "\" & realFileName.Substring(5))
            End If
            addingComm(foundClient(foundClient.Length - 1).noClient, 0, True, cat, curFilename, Date.Today, "Importation depuis un message", , realFileName)
        End If
    End Sub

    Private Sub saveToKP()
        'Sélection de la catégorie
        Dim myInputBoxPlus As New InputBoxPlus(True, , "CommCategories.Categorie")
        Dim cat As String = myInputBoxPlus("Veuillez choisir une catégorie pour cette communication", "catégorie de la communication")
        If cat = "" AndAlso MessageBox.Show("Êtes-vous sûr de ne pas vouloir entrer de catégorie ?", "Catégorie vide", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim myKeypeople As New keypeopleSearch
        myKeypeople.MdiParent = Nothing
        myKeypeople.Visible = False
        Dim kpSelected As KPSelectorReturn = myKeypeople.showDialog()
        If kpSelected.noKP = 0 Then Exit Sub

        Dim curAttachIndex As Integer = getAttachIndex()

        Dim attachsData() As String = Me.attachs.Tag.ToString.Split(New Char() {"§"})
        Dim file() As String = attachsData(curAttachIndex).Split(New Char() {"|"})
        Dim curPath As String = ""
        Dim curFilename As String = ""
        Dim realFileName As String = ""
        Dim fullPath As String = ""
        Dim curExt As String = ""
        If file(0) = "DB" Then
            curFilename = getLastDir(file(1))
            curPath = file(1).Substring(0, file(1).Length - (curFilename.Length + 1))
            Dim curDBItem As New InternalDBItem(curPath, curFilename)
            realFileName = "DB|" & curDBItem.noDBItem & "\" & curPath & "\" & curFilename
        ElseIf file(0) = "FILE" Then
            curPath = "Data\Mails\" & Me.noMail & "\attach"
            curFilename = file(1)
            fullPath = appPath & bar(appPath) & curPath & "\" & file(2)
            Dim newCommNo As Integer = genUniqueNo()
            curExt = curFilename.Substring(realFileName.LastIndexOf(".") + 1)
            realFileName = "FILE|" & newCommNo & "." & curExt
            Dim copyPath As String = appPath & bar(appPath) & "KP\" & kpSelected.noKP & "\Comm"
            IO.Directory.CreateDirectory(copyPath)
            IO.File.Copy(fullPath, copyPath & "\" & realFileName.Substring(5))
        End If

        addingCommKP(kpSelected.noKP, 0, True, cat, curFilename, Date.Today, "Importation depuis un message", realFileName)
    End Sub

    Private Sub attachs_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles attachs.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Me.attachs.Focus()
            Me.attachs.SelectionStart = Me.attachs.GetCharIndexFromPosition(e.Location)

            selectAttachement()
            Dim curAttachIndex As Integer = getAttachIndex()
            If curAttachIndex = -1 Then
                Me.attachs.ContextMenuStrip = Nothing
                Exit Sub
            End If
            Me.attachs.ContextMenuStrip = Me.menuAttachs

            Dim attachsData() As String = Me.attachs.Tag.ToString.Split(New Char() {"§"})
            Dim file() As String = attachsData(curAttachIndex).Split(New Char() {"|"})
            Me.ItemDeLaBanqueDeDonnéesToolStripMenuItem.Enabled = Not (file(0) = "DB")
            'menuAttachs.Show(attachs, e.Location)
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
