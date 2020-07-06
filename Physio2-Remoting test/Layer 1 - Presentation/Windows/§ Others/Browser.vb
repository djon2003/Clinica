Friend Class Browser
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin
        Me.BrowsingPage.htmlPageURL = emptyHTMLPath
        AddHandler BrowsingPage.pageLoaded, AddressOf pageLoaded
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            RemoveHandler BrowsingPage.pageLoaded, AddressOf pageLoaded
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
    Friend WithEvents ToolBar As System.Windows.Forms.Panel
    Friend WithEvents Addresses As Clinica.ManagedCombo
    Friend WithEvents Submit As System.Windows.Forms.Button
    Friend WithEvents Forward As System.Windows.Forms.Button
    Friend WithEvents Back As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    'TODO : Change back to WebControl when problem had been fixed with Base.WebControl
    Friend WithEvents BrowsingPage As Base.Windows.Forms.WebControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolBar = New System.Windows.Forms.Panel
        Me.Addresses = New Clinica.ManagedCombo
        Me.Submit = New System.Windows.Forms.Button
        Me.Forward = New System.Windows.Forms.Button
        Me.Back = New System.Windows.Forms.Button
        Me.BrowsingPage = New Base.Windows.Forms.WebControl
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolBar.Controls.Add(Me.Addresses)
        Me.ToolBar.Controls.Add(Me.Submit)
        Me.ToolBar.Controls.Add(Me.Forward)
        Me.ToolBar.Controls.Add(Me.Back)
        Me.ToolBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.Size = New System.Drawing.Size(608, 32)
        Me.ToolBar.TabIndex = 9
        '
        'Addresses
        '
        Me.Addresses.acceptAlpha = True
        Me.Addresses.acceptedChars = Nothing
        Me.Addresses.acceptNumeric = True
        Me.Addresses.allCapital = False
        Me.Addresses.allLower = False
        Me.Addresses.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Addresses.autoComplete = True
        Me.Addresses.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Addresses.autoSizeDropDown = True
        Me.Addresses.blockOnMaximum = False
        Me.Addresses.blockOnMinimum = False
        Me.Addresses.cb_AcceptLeftZeros = False
        Me.Addresses.cb_AcceptNegative = False
        Me.Addresses.currencyBox = False
        Me.Addresses.dbField = Nothing
        Me.Addresses.doComboDelete = True
        Me.Addresses.firstLetterCapital = False
        Me.Addresses.firstLettersCapital = False
        Me.Addresses.itemsToolTipDuration = 10000
        Me.Addresses.Location = New System.Drawing.Point(152, 4)
        Me.Addresses.manageText = True
        Me.Addresses.matchExp = Nothing
        Me.Addresses.maximum = 0
        Me.Addresses.minimum = 0
        Me.Addresses.Name = "Addresses"
        Me.Addresses.nbDecimals = CType(-1, Short)
        Me.Addresses.onlyAlphabet = False
        Me.Addresses.pathOfList = Nothing
        Me.Addresses.ReadOnly = False
        Me.Addresses.refuseAccents = False
        Me.Addresses.refusedChars = ""
        Me.Addresses.showItemsToolTip = False
        Me.Addresses.Size = New System.Drawing.Size(376, 21)
        Me.Addresses.Sorted = True
        Me.Addresses.TabIndex = 16
        Me.Addresses.Text = "http://"
        Me.Addresses.trimText = False
        '
        'Submit
        '
        Me.Submit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Submit.Location = New System.Drawing.Point(536, 4)
        Me.Submit.Name = "Submit"
        Me.Submit.Size = New System.Drawing.Size(64, 24)
        Me.Submit.TabIndex = 15
        Me.Submit.Text = "Naviguer"
        '
        'Forward
        '
        Me.Forward.Enabled = False
        Me.Forward.Location = New System.Drawing.Point(80, 4)
        Me.Forward.Name = "Forward"
        Me.Forward.Size = New System.Drawing.Size(64, 24)
        Me.Forward.TabIndex = 14
        Me.Forward.Text = "Suivant"
        '
        'Back
        '
        Me.Back.Enabled = False
        Me.Back.Location = New System.Drawing.Point(8, 4)
        Me.Back.Name = "Back"
        Me.Back.Size = New System.Drawing.Size(64, 24)
        Me.Back.TabIndex = 13
        Me.Back.Text = "Précédent"
        '
        'BrowsingPage
        '
        Me.BrowsingPage.activateLinksOnEdit = True
        Me.BrowsingPage.allowContextMenu = True
        Me.BrowsingPage.allowEditorContextMenu = True
        Me.BrowsingPage.allowNavigation = False
        Me.BrowsingPage.allowRefresh = True
        Me.BrowsingPage.allowUndo = True
        Me.BrowsingPage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BrowsingPage.editorContextMenu = Nothing
        Me.BrowsingPage.editorHeight = 350
        Me.BrowsingPage.editorURL = ""
        Me.BrowsingPage.editorWidth = 460
        Me.BrowsingPage.htmlPageURL = Nothing
        Me.BrowsingPage.Location = New System.Drawing.Point(0, 32)
        Me.BrowsingPage.Name = "BrowsingPage"
        Me.BrowsingPage.Size = New System.Drawing.Size(608, 405)
        Me.BrowsingPage.startupPos = 0
        Me.BrowsingPage.TabIndex = 10
        Me.BrowsingPage.toolBarStyles = 1
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'Browser
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(608, 437)
        Me.Controls.Add(Me.BrowsingPage)
        Me.Controls.Add(Me.ToolBar)
        Me.Name = "Browser"
        Me.Opacity = 0.10000000149011612
        Me.ShowInTaskbar = False
        Me.Text = "Navigateur internet"
        Me.ToolBar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Propriétés"
    Public Property htmlPageUrl() As String
        Get
            Return Addresses.Text
        End Get
        Set(ByVal value As String)
            Addresses.Text = value
            BrowsingPage.htmlPageURL = value
        End Set
    End Property
#End Region

    Private pageIsLoaded As Boolean = False

    Private Sub pageLoaded()
        If BrowsingPage.htmlPageURL <> "" AndAlso BrowsingPage.htmlPageURL <> "about:blank" Then
            'BrowsingPage.ShowPage()
        End If

        pageIsLoaded = True
        DBHelper.addItemToADBList("BrowserUrls", "BrowserUrl", Addresses.Text, "NoBrowserUrl", , , , True, "NoUser", ConnectionsManager.currentUser)
        If Addresses.Items.IndexOf(Addresses.Text) = -1 Then Addresses.Items.Add(Addresses.Text)
    End Sub

    Private Sub browser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim urls() As String = DBLinker.getInstance.readOneDBField("BrowserUrls", "BrowserUrl", "WHERE NoUser=" & ConnectionsManager.currentUser, , True)
        If Not urls Is Nothing AndAlso urls.Length <> 0 Then Addresses.Items.AddRange(urls)
    End Sub

    Protected Sub enableNavIfHistory()
        'If htmlDocActiveX Is Nothing OrElse htmlDocActiveX.parentWindow.history.length = 0 Then
        '    Back.Enabled = False
        '    Forward.Enabled = False
        'Else
        '    Back.Enabled = True
        '    Forward.Enabled = True
        'End If
    End Sub

    Private Sub submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit.Click
        BrowsingPage.htmlPageURL = Addresses.Text
        BrowsingPage.showPage()
    End Sub

    Private Sub back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Back.Click
        BrowsingPage.goBack()
    End Sub

    Private Sub forward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Forward.Click
        BrowsingPage.goForward()
    End Sub

    Public Sub showPage()
        If BrowsingPage.htmlPageURL <> "" Then
            BrowsingPage.showPage()

        End If

    End Sub

    Private Sub addresses_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Addresses.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            submit_Click(sender, New EventArgs())
        End If
    End Sub

    Private Sub addresses_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Addresses.SelectedIndexChanged

    End Sub

    Private Sub browsingPage_HistoryChanged(ByRef sender As WebTextControl) Handles BrowsingPage.historyChanged
        Me.Back.Enabled = BrowsingPage.isBackPossible()
        Me.Forward.Enabled = BrowsingPage.isForwardPossible()
    End Sub

    Private Sub browsingPage_NavigateComplete(ByVal url As String) Handles BrowsingPage.navigateComplete
        Addresses.Text = url
        Dim oldTitle As String = Me.Text
        Dim separator As String = " : "
        If BrowsingPage.documentTitle.Length = 0 Then separator = ""
        Me.Text = "Navigateur internet" & separator & BrowsingPage.documentTitle
        If Me.Text <> oldTitle Then
            WindowsManager.getInstance.updateWindowText(oldTitle, Me.Text)
        End If
        pageLoaded()
    End Sub

    Private Sub addresses_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles Addresses.SelectionChangeCommitted
        Addresses.Text = Addresses.SelectedItem
        submit_Click(sender, e)
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
