Option Strict Off
Option Explicit On
Friend Class TextWindow
    Inherits System.Windows.Forms.Form

#Region "Windows Form Designer generated code "
    Private Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        With tmhtml
            .Text = ""
            .editorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")
            .ancreActif = True
            .ancre = PreferencesManager.getGeneralPreferences()("Ancre")
        End With

        isHTML = True

        Me.printDoc.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("print16.ico"), New Size(16, 16))

        If PreferencesManager.getGeneralPreferences()("Ancre") <> "" Then tm.ancre = PreferencesManager.getGeneralPreferences()("Ancre") : tm.ancreON = True
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
    Private WithEvents annuler As System.Windows.Forms.Button
    Private WithEvents accept As System.Windows.Forms.Button
    Private WithEvents tm As TextControl
    Private WithEvents printDoc As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Private Shared WithEvents tmhtml As Clinica.WebTextControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.annuler = New System.Windows.Forms.Button
        Me.accept = New System.Windows.Forms.Button
        Me.tm = New TextControl
        TextWindow.tmhtml = New WebTextControl
        Me.printDoc = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'annuler
        '
        Me.annuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.annuler.BackColor = System.Drawing.SystemColors.Control
        Me.annuler.Cursor = System.Windows.Forms.Cursors.Default
        Me.annuler.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.annuler.ForeColor = System.Drawing.SystemColors.ControlText
        Me.annuler.Location = New System.Drawing.Point(272, 459)
        Me.annuler.Name = "annuler"
        Me.annuler.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.annuler.Size = New System.Drawing.Size(75, 23)
        Me.annuler.TabIndex = 2
        Me.annuler.Text = "Annuler"
        Me.annuler.UseVisualStyleBackColor = False
        '
        'accept
        '
        Me.accept.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.accept.BackColor = System.Drawing.SystemColors.Control
        Me.accept.Cursor = System.Windows.Forms.Cursors.Default
        Me.accept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.accept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.accept.Location = New System.Drawing.Point(161, 459)
        Me.accept.Name = "accept"
        Me.accept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.accept.Size = New System.Drawing.Size(75, 23)
        Me.accept.TabIndex = 1
        Me.accept.Text = "Accepter"
        Me.accept.UseVisualStyleBackColor = False
        '
        'TM
        '
        Me.tm.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tm.ancre = Nothing
        Me.tm.ancreON = False
        Me.tm.ancreRemove = False
        Me.tm.Location = New System.Drawing.Point(0, 0)
        Me.tm.Name = "TM"
        Me.tm.showImgMenu = True
        Me.tm.showMenu = True
        Me.tm.Size = New System.Drawing.Size(509, 453)
        Me.tm.TabIndex = 0
        Me.tm.tabSpacing = CType(0, Short)
        Me.tm.Text = ""
        '
        'TMHTML
        '
        tmhtml.activateLinksOnEdit = True
        tmhtml.allowContextMenu = True
        tmhtml.allowEditorContextMenu = True
        tmhtml.allowNavigation = False
        tmhtml.allowRefresh = False
        tmhtml.allowUndo = True
        tmhtml.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        tmhtml.ancre = Nothing
        tmhtml.ancreActif = False
        tmhtml.editorContextMenu = Nothing
        tmhtml.editorHeight = 350
        tmhtml.editorURL = ""
        tmhtml.editorWidth = 460
        tmhtml.htmlPageURL = Nothing
        tmhtml.Location = New System.Drawing.Point(0, 0)
        tmhtml.Name = "TMHTML"
        tmhtml.Size = New System.Drawing.Size(509, 453)
        tmhtml.startupPos = 0
        tmhtml.TabIndex = 3
        tmhtml.toolBarStyles = 1
        '
        'printDoc
        '
        Me.printDoc.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.printDoc.BackColor = System.Drawing.SystemColors.Control
        Me.printDoc.Cursor = System.Windows.Forms.Cursors.Default
        Me.printDoc.Enabled = False
        Me.printDoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.printDoc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.printDoc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.printDoc.Location = New System.Drawing.Point(242, 458)
        Me.printDoc.Name = "printDoc"
        Me.printDoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.printDoc.Size = New System.Drawing.Size(24, 24)
        Me.printDoc.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.printDoc, "Imprimer le document")
        Me.printDoc.UseMnemonic = False
        Me.printDoc.UseVisualStyleBackColor = False
        '
        'textemodif
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(509, 494)
        Me.Controls.Add(Me.printDoc)
        Me.Controls.Add(TextWindow.tmhtml)
        Me.Controls.Add(Me.annuler)
        Me.Controls.Add(Me.accept)
        Me.Controls.Add(Me.tm)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MinimumSize = New System.Drawing.Size(476, 340)
        Me.Name = "textemodif"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modification de texte"
        Me.windowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private diffHeight As Integer = -1
    Private diffWidth As Integer = -1
    Private mySel As String
    Private formModified As Boolean = False
    Private tmhtmlModified As Boolean = False
    Private acceptedTM As Boolean = False
    Private _IsHTML As Boolean = True
    Private _IsLocked As Boolean = False
    Private myTMType As RichTextBoxStreamType = RichTextBoxStreamType.RichText
    Private _CurrentData As String = ""
    Private Shared mySelf As TextWindow

#Region "Propriétés"
    Public Property isHTML() As Boolean
        Get
            Return _IsHTML
        End Get
        Set(ByVal Value As Boolean)
            _IsHTML = Value
            If Value = True Then
                tmhtml.Visible = True
                tm.Visible = False
            Else
                tmhtml.Visible = False
                tm.Visible = True
            End If
        End Set
    End Property

    Public Overloads Property windowState() As FormWindowState
        Get
            Return MyBase.WindowState
        End Get
        Set(ByVal value As FormWindowState)
            MyBase.WindowState = value
        End Set
    End Property


    Public ReadOnly Property ShowTexteModif(ByVal mySelection As String, Optional ByVal viewDisableHtmlFields As Boolean = False) As String
        Get
            mySel = mySelection
            'Charge le contenu dans l'éditeur approprié
            If isHTML Then
                tmhtml.viewDisableHtmlFields = viewDisableHtmlFields
                tmhtml.setHtml(_CurrentData)
                tmhtml.setPos(mySel)
                'tmhtml.focus()
                Me.printDoc.Enabled = True
            Else
                If myTMType = RichTextBoxStreamType.PlainText Then
                    tm.ResetText()
                    tm.Text = _CurrentData
                Else
                    tm.Rtf = _CurrentData
                End If
                Dim intPos As Integer = 0
                Integer.TryParse(mySel, intPos)
                tm.SelectionStart = intPos
                'tm.Focus()
                'TODO: Support printing for text doc
                Me.printDoc.Enabled = False
            End If

            'Modifie le titre en conséquence d'une visualisation si l'utilisateur ne peut pas modifier
            If isLocked Then
                Me.Text = Me.Text.Replace("Modification", "Visualisation")
            Else
                Me.Text = Me.Text.Replace("Visualisation", "Modification")
            End If

            formModified = False
            tmModified = False

            'Me.ShowDialog()

            'This code is required to get always the same window of maximisation
            Dim tmpDialogWindow As New TopDialogWindow(getInstance)
            tmpDialogWindow.ShowDialog()
            tmpDialogWindow.Dispose()

            Return mySel
        End Get
    End Property

    Public Property texteType() As RichTextBoxStreamType
        Get
            Return myTMType
        End Get
        Set(ByVal Value As RichTextBoxStreamType)
            myTMType = Value
        End Set
    End Property

    Public Property currentData() As String
        Get
            Return _CurrentData
        End Get
        Set(ByVal Value As String)
            _CurrentData = Value
        End Set
    End Property

    Public Property isLocked() As Boolean
        Get
            Return _IsLocked
        End Get
        Set(ByVal value As Boolean)
            _IsLocked = value
            tmhtml.Editing = Not value
            tm.ReadOnly = value
            accept.Enabled = Not value
        End Set
    End Property
#End Region

    Public Shared Function getInstance() As TextWindow
        If mySelf Is Nothing Then mySelf = New TextWindow

        Return mySelf
    End Function

    Private Sub accept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles accept.Click
        acceptedTM = True
        tmModified = True

        If isHTML Then
            _CurrentData = tmhtml.getHTML()
            mySel = tmhtml.getPos()
        Else
            If myTMType = RichTextBoxStreamType.PlainText Then
                _CurrentData = tm.Text.Replace(vbLf, vbCrLf)
            Else
                _CurrentData = tm.Rtf
            End If
            mySel = tm.SelectionStart
        End If

        formModified = False
        Me.Close()
    End Sub

    Private Sub annuler_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles annuler.Click
        Me.Close()
    End Sub

    Private Sub TextWindow_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'TODO : This code supposed to put focus on text box, but doesn't !!
        setFocusOnText()
    End Sub

    Private Sub setFocusOnText()
        If isHTML Then
            tmhtml.focus()
        Else
            tm.Focus()
        End If
    End Sub

    Private Sub TextWindow_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If formModified = True Then
            If MessageBox.Show("Désirez-vous accepter les modifications apportées au texte ?", "Modification effectuée", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then accept_Click(sender, EventArgs.Empty)
        Else
            'Keep position only if not modified, otherwise the cursor can be miss placed (not same text, same position)
            If isHTML = False Then
                mySel = tm.SelectionStart
            Else
                mySel = tmhtml.getPos()
            End If
        End If

        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub TextWindow_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        setFocusOnText()
    End Sub

    Private Sub textemodif_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub textemodif_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        formModified = False
        tmModified = False
        If isHTML Then
            tmhtml.focus()
        Else
            tm.Focus()
        End If
    End Sub

    Private Sub tm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tm.TextChanged
        formModified = True
    End Sub

    Private Shared Sub tmhtml_PageLoaded() Handles tmhtml.pageLoaded
        mySelf.formModified = False
        tmModified = False
        mySelf.printDoc.Enabled = mySelf.isHTML
    End Sub

    Private Sub printDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles printDoc.Click
        PrintingHelper.printHtml(tmhtml, Me.Text, True, True)
    End Sub

    Private Shared Sub tmhtml_TextChanged(ByVal theText As String) Handles tmhtml.textChanged
        mySelf.formModified = True
    End Sub

    Private Sub TextWindow_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        setFocusOnText()
    End Sub

    Private Sub textemodif_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub textemodif_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible AndAlso isLocked = False Then
            If isHTML Then
                tmhtml.setPos(mySel)
                tmhtml.focus()
            Else
                tm.Focus()
            End If
        End If
    End Sub
End Class
