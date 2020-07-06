Imports Win32 = Microsoft.Win32
Imports System.Runtime.InteropServices
Imports mshtml
Imports MsHtmHstInterop

Public Class WebControl
    Inherits System.Windows.Forms.UserControl
    Implements IOleDocumentSite, IDocHostShowUI, IPrintable

#Region "Class IEHoster"
    Private Class IEHoster
        Implements IDocHostUIHandler, IOleClientSite

        Private ctrl_alt_NumberCalledOnce As Boolean = False
        Private myWebInteraction As WebControlInteraction
        Private _AllowRefresh As Boolean = False
        Private _AllowScrolling As Boolean = True
        Private _AllowContextMenuOnSelection As Boolean = True
        Private _AllowContextMenu As Boolean = False
        Private _AllowUndo As Boolean = False
        Private _ContextMenu As ContextMenuStrip

        Public Class WillContextMenuEventArgs
            Private _IsOnSelection As Boolean
            Private _Location As Point
            Public Sub New(ByVal isOnSelection As Boolean, ByVal location As Point)
                _IsOnSelection = isOnSelection
                _Location = location
            End Sub
            Public ReadOnly Property isOnSelection() As Boolean
                Get
                    Return _IsOnSelection
                End Get
            End Property
            Public ReadOnly Property location() As Point
                Get
                    Return _Location
                End Get
            End Property
        End Class

        Public Event willContextMenu(ByVal contextMenu As ContextMenuStrip, ByVal e As WillContextMenuEventArgs)
        Public Event acceleratorCTRL_A()
        Public Event ctrl_alt_Number(ByVal numberKeyCode As Integer)

        Public Sub New(ByVal curWebInteraction As WebControlInteraction, ByVal allowRefresh As Boolean, ByVal allowScrolling As Boolean)
            myWebInteraction = curWebInteraction
            _AllowRefresh = allowRefresh
            _AllowScrolling = allowScrolling
        End Sub

#Region "Propriétés"
        Public Property contextMenu() As ContextMenuStrip
            Get
                Return _ContextMenu
            End Get
            Set(ByVal value As ContextMenuStrip)
                _ContextMenu = value
            End Set
        End Property

        Public Property allowContextMenu() As Boolean
            Get
                Return _AllowContextMenu
            End Get
            Set(ByVal value As Boolean)
                _AllowContextMenu = value
            End Set
        End Property

        Public Property allowUndo() As Boolean
            Get
                Return _AllowUndo
            End Get
            Set(ByVal value As Boolean)
                _AllowUndo = value
            End Set
        End Property

        Public Property allowContextMenuOnSelection() As Boolean
            Get
                Return _AllowContextMenuOnSelection
            End Get
            Set(ByVal value As Boolean)
                _AllowContextMenuOnSelection = value
            End Set
        End Property
#End Region

#Region "IOleClientSite methods"

        Sub saveObject() Implements IOleClientSite.saveObject
        End Sub

        Sub getMoniker(ByVal dwAssign As Integer, ByVal dwWhichMoniker As Integer, ByRef ppmk As Object) Implements IOleClientSite.getMoniker
        End Sub

        Sub getContainer(ByRef ppContainer As Object) Implements IOleClientSite.getContainer
            ppContainer = Me
        End Sub

        Sub showObject() Implements IOleClientSite.showObject
        End Sub

        Sub onShowWindow(ByVal fShow As Boolean) Implements IOleClientSite.onShowWindow
        End Sub

        Sub requestNewObjectLayout() Implements IOleClientSite.requestNewObjectLayout
        End Sub

#End Region


#Region "IDocHostUIHandler"
        Public Sub enableModeless(ByVal fEnable As Integer) Implements IDocHostUIHandler.EnableModeless

        End Sub

        Public Sub filterDataObject(ByVal pDO As MsHtmHstInterop.IDataObject, ByRef ppDORet As MsHtmHstInterop.IDataObject) Implements IDocHostUIHandler.FilterDataObject
            ppDORet = Nothing
        End Sub

        Public Sub getDropTarget(ByVal pDropTarget As MsHtmHstInterop.IDropTarget, ByRef ppDropTarget As MsHtmHstInterop.IDropTarget) Implements IDocHostUIHandler.GetDropTarget
            ppDropTarget = Nothing
        End Sub

        Public Sub getExternal(ByRef ppDispatch As Object) Implements IDocHostUIHandler.GetExternal
            ppDispatch = myWebInteraction
        End Sub

        Public Sub getHostInfo(ByRef pInfo As MsHtmHstInterop._DOCHOSTUIINFO) Implements IDocHostUIHandler.GetHostInfo
            If _AllowScrolling = False Then pInfo.dwFlags = pInfo.dwFlags Or tagDOCHOSTUIFLAG.DOCHOSTUIFLAG_SCROLL_NO Or tagDOCHOSTUIFLAG.DOCHOSTUIFLAG_DIALOG Or tagDOCHOSTUIFLAG.DOCHOSTUIFLAG_DIV_BLOCKDEFAULT
        End Sub

        Public Sub getOptionKeyPath(ByRef pchKey As String, ByVal dw As UInteger) Implements IDocHostUIHandler.GetOptionKeyPath
            pchKey = Nothing
        End Sub

        Public Sub hideUI() Implements IDocHostUIHandler.HideUI

        End Sub

        Public Sub onDocWindowActivate(ByVal fActivate As Integer) Implements IDocHostUIHandler.OnDocWindowActivate

        End Sub

        Public Sub onFrameWindowActivate(ByVal fActivate As Integer) Implements IDocHostUIHandler.OnFrameWindowActivate

        End Sub

        Public Sub resizeBorder(ByRef prcBorder As MsHtmHstInterop.tagRECT, ByVal pUIWindow As MsHtmHstInterop.IOleInPlaceUIWindow, ByVal fRameWindow As Integer) Implements IDocHostUIHandler.ResizeBorder

        End Sub

        <System.Diagnostics.DebuggerStepThrough()> _
        Public Sub showContextMenu(ByVal dwID As UInteger, ByRef ppt As MsHtmHstInterop.tagPOINT, ByVal pcmdtReserved As Object, ByVal pdispReserved As Object) Implements IDocHostUIHandler.ShowContextMenu
            Dim curPage As mshtml.HTMLDocument = pdispReserved.document
            Dim isOnSelection As Boolean = False
            Try
                isOnSelection = curPage.selection.type <> "None" OrElse curPage.selection.type = "Text"
                If isOnSelection = False AndAlso curPage.frames.length <> 0 Then isOnSelection = CType(curPage.frames.item(0), mshtml.HTMLWindow2).document.selection.type = "Text"
            Catch ex As Exception
            End Try
            '        ' use this code to show a custom menu
            Const MenuHandled As Integer = 0

            '        ' use this code to show your own context menu
            Const MenuNotHandled As Integer = 1

            If _ContextMenu IsNot Nothing AndAlso Me.allowContextMenu Then
                If TypeOf pdispReserved Is mshtml.HTMLBody Then
                    If pdispReserved.TagName IsNot Nothing AndAlso pdispReserved.document.parentWindow.frameElement IsNot Nothing Then
                        Dim p As New Point(ppt.x, ppt.y)
                        'p = Control.PointToClient(p)
                        RaiseEvent willContextMenu(_ContextMenu, New WillContextMenuEventArgs(isOnSelection, p))
                        _ContextMenu.Show(p)
                    End If
                End If
                Throw New COMException("", MenuHandled)
            End If

            If (_AllowContextMenuOnSelection And (curPage.selection.type = "Text")) Or (_AllowContextMenu And curPage.selection.type = "None") Then Throw New COMException("", MenuNotHandled)
        End Sub

        Public Sub showUI(ByVal dwID As UInteger, ByVal pActiveObject As MsHtmHstInterop.IOleInPlaceActiveObject, ByVal pCommandTarget As MsHtmHstInterop.IOleCommandTarget, ByVal pFrame As MsHtmHstInterop.IOleInPlaceFrame, ByVal pDoc As MsHtmHstInterop.IOleInPlaceUIWindow) Implements IDocHostUIHandler.ShowUI

        End Sub

        <System.Diagnostics.DebuggerStepThrough()> _
        Public Sub translateAccelerator(ByRef lpmsg As MsHtmHstInterop.tagMSG, ByRef pguidCmdGroup As System.Guid, ByVal nCmdID As UInteger) Implements IDocHostUIHandler.TranslateAccelerator
            Const KillAccelerator As Integer = 0
            Const AllowAccelerator As Integer = 1
            REM Const WM_KEYDOWN As Integer = &H100
            lpmsg.wParam = lpmsg.wParam And &HFF ' get the virtual keycode

            'Disable refresh F5
            If lpmsg.wParam = 116 And _AllowRefresh = False Then Throw New COMException("", KillAccelerator)
            'allow anything except when CTRL or ALT are down
            If (Control.ModifierKeys And Keys.Control) <> Keys.Control And (Control.ModifierKeys And Keys.Alt) <> Keys.Alt Then
                Throw New COMException("", AllowAccelerator)
            End If

            ' allow, CTRL-Z,CTRL-A,CTRL-X,CTRL-C,CTRL-V
            If (lpmsg.wParam = 90 And allowUndo) Or lpmsg.wParam = 65 Or lpmsg.wParam = 35 Or lpmsg.wParam = 36 Or lpmsg.wParam = 37 Or lpmsg.wParam = 39 Or lpmsg.wParam = 88 Or lpmsg.wParam = 67 Or lpmsg.wParam = 86 Then
                If lpmsg.wParam = 65 Then RaiseEvent acceleratorCTRL_A()
                Throw New COMException("", AllowAccelerator)
            End If
            'allow CTRL+ATL+ something which gives a char
            If (lpmsg.wParam = 226 Or lpmsg.wParam = 190 Or lpmsg.wParam = 188 Or lpmsg.wParam = 220 Or lpmsg.wParam = 192 Or lpmsg.wParam = 186 Or lpmsg.wParam = 221 Or lpmsg.wParam = 219 Or lpmsg.wParam = 222 Or lpmsg.wParam = 189 Or lpmsg.wParam = 187 Or (lpmsg.wParam >= 48 And lpmsg.wParam <= 57)) Then
                If ctrl_alt_NumberCalledOnce = False Then
                    ctrl_alt_NumberCalledOnce = True
                    Dim asciiCode As Integer = lpmsg.wParam
                    If lpmsg.wParam = 226 Then asciiCode = 176
                    If lpmsg.wParam = 190 Then asciiCode = 173
                    If lpmsg.wParam = 188 Then asciiCode = 175
                    If lpmsg.wParam = 220 Then asciiCode = 125
                    If lpmsg.wParam = 192 Then asciiCode = 123
                    If lpmsg.wParam = 186 Then asciiCode = 126
                    If lpmsg.wParam = 219 Then asciiCode = 91
                    If lpmsg.wParam = 221 Then asciiCode = 93
                    If lpmsg.wParam = 222 Then asciiCode = 92
                    If lpmsg.wParam = 50 Then asciiCode = 64
                    If lpmsg.wParam = 49 Then asciiCode = 177
                    If lpmsg.wParam = 54 Then asciiCode = 172
                    If lpmsg.wParam = 51 Then asciiCode = 163
                    If lpmsg.wParam = 52 Then asciiCode = lpmsg.wParam + 110
                    If lpmsg.wParam = 53 Then asciiCode = lpmsg.wParam + 111
                    If lpmsg.wParam = 55 Then asciiCode = 166
                    If (lpmsg.wParam = 56 Or lpmsg.wParam = 57) Then asciiCode = lpmsg.wParam + 122
                    If lpmsg.wParam = 48 Then asciiCode = 188
                    If lpmsg.wParam = 187 Then asciiCode = 190
                    RaiseEvent ctrl_alt_Number(asciiCode)
                Else
                    ctrl_alt_NumberCalledOnce = False
                End If
                Throw New COMException("", KillAccelerator)
            End If
            ' disable everything else
            'Throw New COMException("", KillAccelerator)
        End Sub

        Public Sub translateUrl(ByVal dwTranslate As UInteger, ByRef pchURLIn As UShort, ByVal ppchURLOut As System.IntPtr) Implements IDocHostUIHandler.TranslateUrl

        End Sub

        Public Sub updateUI() Implements IDocHostUIHandler.UpdateUI

        End Sub
#End Region

    End Class
#End Region

#Region "Editor context menu"
    Private Sub menuAnnuler(ByVal sender As Object, ByVal e As EventArgs)
        Me.isUndo()
        My.Computer.Keyboard.SendKeys("^z")
        If Me.isUndo Then Me.onTextChanged(Me.getHTML)
        'Me.PageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_UNDO, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
    End Sub

    Private Sub menuColler(ByVal sender As Object, ByVal e As EventArgs)
        Me.pageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_PASTE, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
        Me.onTextChanged(Me.getHTML)
    End Sub

    Private Sub menuInsertLink(ByVal sender As Object, ByVal e As EventArgs)
        Me.onAddLink(False)
        Me.onTextChanged(Me.getHTML)
    End Sub

    Private Sub menuInsertImg(ByVal sender As Object, ByVal e As EventArgs)
        Me.onAddImage(False)
        Me.onTextChanged(Me.getHTML)
    End Sub

    Private Sub menuCopier(ByVal sender As Object, ByVal e As EventArgs)
        Me.pageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_COPY, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
    End Sub

    Private Sub menuCouper(ByVal sender As Object, ByVal e As EventArgs)
        Me.pageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_CUT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
        Me.onTextChanged(Me.getHTML)
    End Sub

    Private Sub menuSelectAll(ByVal sender As Object, ByVal e As EventArgs)
        Me.pageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_SELECTALL, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
    End Sub

    Private Sub menuSearch(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.pageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_FIND, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
        Catch ex As Exception
            'Not able to use search
        End Try
    End Sub


    Private Sub menuAnchor(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent requestNextAnchor()
    End Sub
#End Region

    Protected Overridable Function isAnchorActif() As Boolean
        Return False
    End Function

    Protected Sub anchorChanged()
        If _EditorContextMenu Is Nothing AndAlso ieHosterEdition.contextMenu IsNot Nothing Then ieHosterEdition.contextMenu.Items(7).Visible = isAnchorActif()
    End Sub

    Private Sub buildEditorContextMenu()
        If ieHosterEdition Is Nothing Then Exit Sub

        ieHosterEdition.allowContextMenuOnSelection = True
        ieHosterEdition.allowContextMenu = True
        Dim editionContextMenu As New ContextMenuStrip()
        editionContextMenu.Items.Add("Annuler")
        AddHandler editionContextMenu.Items(0).Click, AddressOf menuAnnuler
        editionContextMenu.Items(0).Enabled = Me.allowUndo
        CType(editionContextMenu.Items(0), System.Windows.Forms.ToolStripMenuItem).ShortcutKeys = Keys.Control Or Keys.Z
        editionContextMenu.Items.Add("-")
        editionContextMenu.Items.Add("Couper")
        AddHandler editionContextMenu.Items(2).Click, AddressOf menuCouper
        CType(editionContextMenu.Items(2), System.Windows.Forms.ToolStripMenuItem).ShortcutKeys = Keys.Control Or Keys.X
        editionContextMenu.Items.Add("Copier")
        AddHandler editionContextMenu.Items(3).Click, AddressOf menuCopier
        CType(editionContextMenu.Items(3), System.Windows.Forms.ToolStripMenuItem).ShortcutKeys = Keys.Control Or Keys.C
        editionContextMenu.Items.Add("Coller")
        AddHandler editionContextMenu.Items(4).Click, AddressOf menuColler
        CType(editionContextMenu.Items(4), System.Windows.Forms.ToolStripMenuItem).ShortcutKeys = Keys.Control Or Keys.V
        editionContextMenu.Items.Add("Sélectionner tout")
        AddHandler editionContextMenu.Items(5).Click, AddressOf menuSelectAll
        CType(editionContextMenu.Items(5), System.Windows.Forms.ToolStripMenuItem).ShortcutKeys = Keys.Control Or Keys.A
        editionContextMenu.Items.Add("-")
        editionContextMenu.Items.Add("Atteindre le prochain ancre   CTRL+MAJ")
        editionContextMenu.Items(7).Visible = isAnchorActif()
        AddHandler editionContextMenu.Items(7).Click, AddressOf menuAnchor
        editionContextMenu.Items.Add("Insérer un lien")
        AddHandler editionContextMenu.Items(8).Click, AddressOf menuInsertLink
        editionContextMenu.Items.Add("Insérer une image")
        AddHandler editionContextMenu.Items(9).Click, AddressOf menuInsertImg
        editionContextMenu.Items.Add("Rechercher dans le texte")
        AddHandler editionContextMenu.Items(10).Click, AddressOf menuSearch
        ieHosterEdition.contextMenu = editionContextMenu
    End Sub

    Private Sub willEditorContextMenu(ByVal sender As ContextMenuStrip, ByVal e As IEHoster.WillContextMenuEventArgs)
        sender.Items(0).Enabled = Me.isUndo
        sender.Items(2).Enabled = e.isOnSelection
        sender.Items(3).Enabled = e.isOnSelection
        sender.Items(4).Enabled = My.Computer.Clipboard.ContainsText OrElse Clipboard.ContainsData(Windows.Forms.DataFormats.Html)
    End Sub

    Private Sub editorSelectAll()
        Me.pageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_SELECTALL, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
    End Sub

    Private Sub editorCTRL_ALT_Number(ByVal numberKeyCode As Integer)
        Me.inserthtml(Chr(numberKeyCode))
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ieHosterText = New IEHoster(myWebInteraction, _AllowRefresh, True)
        ieHosterEdition = New IEHoster(myWebInteraction, _AllowRefresh, False)
        Me.allowUndo = True
        AddHandler ieHosterEdition.willContextMenu, AddressOf willEditorContextMenu
        AddHandler ieHosterEdition.acceleratorCTRL_A, AddressOf editorSelectAll
        AddHandler ieHosterEdition.ctrl_alt_Number, AddressOf editorCTRL_ALT_Number
        Dim obj As Object = pageForText.GetOcx()
        Dim oc As IOleObject = obj
        oc.setClientSite(ieHosterText)
        obj = pageForEdition.GetOcx()
        oc = obj
        oc.setClientSite(ieHosterEdition)

        pageForText.Navigate2("about:blank")
        pageForEdition.Navigate2("about:blank")

        'addhandler PageForText.MouseCaptureChanged,addressof
        AddHandler pageForText.PreviewKeyDown, AddressOf onTextPreviewKeyDown
        AddHandler pageForEdition.PreviewKeyDown, AddressOf onTextPreviewKeyDown
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not pageForText Is Nothing Then
                RemoveHandler pageForText.PreviewKeyDown, AddressOf onTextPreviewKeyDown
                pageForText.Dispose()
            End If
            If Not pageForEdition Is Nothing Then
                RemoveHandler pageForEdition.PreviewKeyDown, AddressOf onTextPreviewKeyDown
                pageForEdition.Dispose()
            End If
            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            If ieHosterText IsNot Nothing Then ieHosterText = Nothing
            If ieHosterEdition IsNot Nothing Then ieHosterEdition = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Public Class BrowserPage
        Inherits AxSHDocVw.AxWebBrowser

        Public Overrides ReadOnly Property locationURL() As String
            Get
                Return MyBase.LocationURL
            End Get
        End Property

        Public Overrides ReadOnly Property path() As String
            Get
                Return MyBase.Path
            End Get
        End Property
    End Class

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents pageForText As BrowserPage
    Private WithEvents pageForEdition As BrowserPage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebControl))
        Me.pageForText = New WebControl.BrowserPage
        Me.pageForEdition = New WebControl.BrowserPage
        CType(Me.pageForText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pageForEdition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PageForText
        '
        Me.pageForText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pageForText.Enabled = True
        Me.pageForText.Location = New System.Drawing.Point(0, 0)
        Me.pageForText.OcxState = CType(resources.GetObject("PageForText.OcxState"), System.Windows.Forms.AxHost.State)
        Me.pageForText.Size = New System.Drawing.Size(336, 288)
        Me.pageForText.TabIndex = 0
        '
        'PageForEdition
        '
        Me.pageForEdition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pageForEdition.Enabled = True
        Me.pageForEdition.Location = New System.Drawing.Point(0, 0)
        Me.pageForEdition.OcxState = CType(resources.GetObject("PageForEdition.OcxState"), System.Windows.Forms.AxHost.State)
        Me.pageForEdition.Size = New System.Drawing.Size(336, 288)
        Me.pageForEdition.TabIndex = 0
        '
        'WebControl
        '
        Me.Controls.Add(Me.pageForText)
        Me.Controls.Add(Me.pageForEdition)
        Me.Name = "WebControl"
        Me.Size = New System.Drawing.Size(336, 288)
        CType(Me.pageForText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pageForEdition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Interop"
    Public Enum HRESULT
        S_OK = 0
        S_FALSE = 1
        E_NOTIMPL = &H80004001
        E_INVALIDARG = &H80070057
        E_NOINTERFACE = &H80004002
        E_FAIL = &H80004005
        E_UNEXPECTED = &H8000FFFF
    End Enum

    <ComVisible(True), ComImport(), Guid("7FD52380-4E07-101B-AE2D-08002B2EC713"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface IPersistStreamInit : Inherits IPersist
        Shadows Sub getClassID(ByRef pClassID As Guid)
        <PreserveSig()> Function IsDirty() As Integer
        <PreserveSig()> Function Load(ByVal pstm As UCOMIStream) As HRESULT
        <PreserveSig()> Function Save(ByVal pstm As UCOMIStream, <MarshalAs(UnmanagedType.Bool)> ByVal fClearDirty As Boolean) As HRESULT
        <PreserveSig()> Function GetSizeMax(<InAttribute(), Out(), MarshalAs(UnmanagedType.U8)> ByRef pcbSize As Long) As HRESULT
        <PreserveSig()> Function InitNew() As HRESULT
    End Interface

    <ComVisible(True), ComImport(), Guid("0000010c-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Public Interface IPersist
        Sub getClassID(ByRef pClassID As Guid)
    End Interface

    Declare Function CreateStreamOnHGlobal Lib "ole32" (ByVal hGlobal As IntPtr, ByVal fDeleteOnRelease As Boolean, ByRef ppstm As UCOMIStream) As Long
#End Region

    Private ieHosterText As IEHoster
    Private ieHosterEdition As IEHoster

    Private Enum BrowserNavConstants
        navOpenInNewWindow = 1
        navNoHistory = 2
        navNoReadFromCache = 4
        navNoWriteToCache = 8
        navAllowAutosearch = 10
        navBrowserBar = 20
        navHyperlink = 4
    End Enum

    Private _PrintJobSpooled As Boolean = False
    Private editorLinkJustClicked As Boolean = False
    Private ctrlDown As Boolean = False
    Private _HTMLPageURL, _EditorURL As String
    Private _Editing As Boolean = False
    Private _AllowRefresh As Boolean = True
    Private _AllowNavigation As Boolean = False
    Private _AllowUndo As Boolean = False
    Private _AllowEditorContextMenu As Boolean = True
    Private _AllowContextMenu As Boolean = True
    Private _EditorContextMenu As ContextMenuStrip
    Private _EditorHeight As Integer = 350
    Private _EditorWidth As Integer = 460
    Private WithEvents doc As SHDocVw.DWebBrowserEvents_Event
    Private WithEvents doc2 As SHDocVw.DWebBrowserEvents_Event
    Private WithEvents htmlEvent As mshtml.HTMLDocumentEvents2_Event
    Protected myPage As String
    Private nbShowedPage As Integer = 0
    Private isFocused As Boolean = False
    Private _ToolBarStyles As Integer = 1
    Private myWebInteraction As New WebControlInteraction(Me)
    Private _StartupPos As Integer = 0
    Private _IsPageLoaded As Boolean
    Private _ActivateLinksOnEdit As Boolean = True

    Public Event beforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean)
    Public Event navigateComplete(ByVal url As String)
    Public Shadows Event textChanged(ByVal theText As String)
    Public Event textPreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
    Public Event textSaved(ByVal theText As String)
    Public Event pageSaved()
    Public Event requestNextAnchor()
    Public Event pageLoaded()
    Public Event addingLink(ByRef sender As WebControl, ByRef handled As Boolean)
    Public Event addingImage(ByRef sender As WebControl, ByRef handled As Boolean)
    Public Event editorLinkClicked(ByVal sender As Object, ByVal e As LinkClickedEventArgs)
    Public Event historyChanged(ByRef sender As WebControl)

#Region "Propriétés"
    Public ReadOnly Property printJobSpooled() As Boolean
        Get
            Return _PrintJobSpooled
        End Get
    End Property

    Public Property activateLinksOnEdit() As Boolean
        Get
            Return _ActivateLinksOnEdit
        End Get
        Set(ByVal value As Boolean)
            _ActivateLinksOnEdit = value
        End Set
    End Property

    Public Property allowRefresh() As Boolean
        Get
            Return _AllowRefresh
        End Get
        Set(ByVal value As Boolean)
            _AllowRefresh = value
        End Set
    End Property
    Public Property allowNavigation() As Boolean
        Get
            Return _AllowNavigation
        End Get
        Set(ByVal value As Boolean)
            _AllowNavigation = value
        End Set
    End Property
    Public Property allowEditorContextMenu() As Boolean
        Get
            Return _AllowEditorContextMenu
        End Get
        Set(ByVal value As Boolean)
            _AllowEditorContextMenu = value
            ieHosterEdition.allowContextMenu = value
            ieHosterEdition.allowContextMenuOnSelection = value
        End Set
    End Property
    Public Property allowContextMenu() As Boolean
        Get
            Return _AllowContextMenu
        End Get
        Set(ByVal value As Boolean)
            _AllowContextMenu = value
            ieHosterText.allowContextMenuOnSelection = value
        End Set
    End Property

    Public Property allowUndo() As Boolean
        Get
            Return _AllowUndo
        End Get
        Set(ByVal value As Boolean)
            _AllowUndo = value

            If ieHosterEdition IsNot Nothing Then ieHosterEdition.allowUndo = value
        End Set
    End Property

    Public Property toolBarStyles() As Integer
        Get
            Return _ToolBarStyles
        End Get
        Set(ByVal Value As Integer)
            _ToolBarStyles = Value
            showingPage(pageForEdition, , True)
        End Set
    End Property

    Public Property editorContextMenu() As ContextMenuStrip
        Get
            Return _EditorContextMenu
        End Get
        Set(ByVal value As ContextMenuStrip)
            _EditorContextMenu = value
            If value Is Nothing Then
                buildEditorContextMenu()
            Else
                ieHosterEdition.contextMenu = _EditorContextMenu
            End If
        End Set
    End Property

    Public Property editorHeight() As Integer
        Get
            Return _EditorHeight
        End Get
        Set(ByVal Value As Integer)
            _EditorHeight = Value
        End Set
    End Property

    Public Property editorWidth() As Integer
        Get
            Return _EditorWidth
        End Get
        Set(ByVal Value As Integer)
            _EditorWidth = Value
        End Set
    End Property

    Public Property htmlPageURL() As String
        Get
            Return _HTMLPageURL
        End Get
        Set(ByVal Value As String)
            _HTMLPageURL = Value
        End Set
    End Property

    Private _TestingTime As Date

    Private Shared pfeHtml As String = ""

    Public Property editorURL() As String
        Get
            If _EditorURL Is Nothing Then Return ""

            Return _EditorURL
        End Get
        Set(ByVal Value As String)
            _EditorURL = Value
            _TestingTime = Date.Now
            If pfeHtml = "" Then
                showingPage(pageForEdition, , True)
            Else
                loadContent(pfeHtml, pageForEdition)
            End If
        End Set
    End Property

    Public Property Editing(Optional ByVal showThePage As Boolean = True) As Boolean
        Get
            Return _Editing
        End Get
        Set(ByVal Value As Boolean)
            Dim html As String = ""
            If showThePage And _IsPageLoaded Then html = getHTML()
            If html Is Nothing Then html = ""
            _Editing = Value
            If Me.DesignMode = True Then Exit Property

            If Value = True Then
                pageForEdition.BringToFront()
                pageForEdition.Dock = DockStyle.Fill
                pageForText.Dock = DockStyle.None
            Else
                pageForText.BringToFront()
                pageForEdition.Dock = DockStyle.None
                pageForText.Dock = DockStyle.Fill
            End If

            If showThePage And _IsPageLoaded Then
                sethtml(html)
                If html <> "" Then
                    Dim curPos As Integer = getPos() 'TO scroll cursor into view
                    setPos(curPos) 'TO scroll cursor into view
                End If
            End If
        End Set
    End Property

    Public Property startupPos() As Integer
        Get
            Return _StartupPos
        End Get
        Set(ByVal Value As Integer)
            _StartupPos = Value
        End Set
    End Property

    Public ReadOnly Property isPageLoaded() As Boolean
        Get
            Return _IsPageLoaded
        End Get
    End Property

#End Region

#Region "Changing & getting current html in page"
#Region "Subs required by GetHTML"
    <System.Diagnostics.DebuggerStepThrough()> Private Sub WaitForHTML()
        Try
            While (1 = 1)
                Threading.Thread.Sleep(100)
            End While
        Catch ex As Threading.ThreadInterruptedException
            'Thread was interrupted
        End Try
    End Sub

    Private Sub stopWaitForHTML()
        waitingThreadHTML.Interrupt()
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> Private Sub WaitForPos()
        Try
            Dim n As Integer = 1000
            While (n > 0)
                Threading.Thread.Sleep(100)
                n -= 1
            End While
        Catch ex As Threading.ThreadInterruptedException
            'Thread was interrupted
        End Try
    End Sub

    Private Sub stopWaitForPos()
        waitingThreadPos.Interrupt()
    End Sub

    Private waitingThreadHTML As System.Threading.Thread
    Private waitingThreadPos As System.Threading.Thread

    'To load document in PageForText not for edition
    Private Sub loadContent(ByVal html As String, ByVal page As WebControl.BrowserPage)
        ' ensure that the document is loaded, otherwise the next line fails... 
        If page.Document Is Nothing Then page.Navigate2("about:blank")
        ' get a reference to the document... 
        Dim clsDocument As mshtml.HTMLDocument = CType(page.Document, mshtml.HTMLDocument)
        ' initiailize the document using the IPersistStreamInit COM interface... 
        DirectCast(clsDocument, IPersistStreamInit).InitNew()
        ' Marshal the content into a interop stream... 
        Dim ptrValue As IntPtr = System.Runtime.InteropServices.Marshal.StringToHGlobalAuto(html)
        Dim clsStream As System.Runtime.InteropServices.UCOMIStream = Nothing
        CreateStreamOnHGlobal(ptrValue, True, clsStream)
        ' load the content into the browser.. 
        DirectCast(clsDocument, IPersistStreamInit).Load(clsStream)
    End Sub
#End Region

#Region "Fonctions publiques"
    Public Function documentTitle() As String
        Return CType(pageForText.Document, IHTMLDocument2).title
    End Function

    Private _isBackPossible As Boolean = False
    Private _isForwardPossible As Boolean = False

    Public Function isBackPossible() As Boolean
        Return _isBackPossible
    End Function

    Public Function isForwardPossible() As Boolean
        Return _isForwardPossible
    End Function

    Public Sub goBack()
        If isBackPossible() = False Then Exit Sub

        CType(pageForText.Document, IHTMLDocument2).parentWindow.history.go(-1)
    End Sub

    Public Sub goForward()
        If _isForwardPossible = False Then Exit Sub

        CType(pageForText.Document, IHTMLDocument2).parentWindow.history.go(1)
    End Sub

    Public Sub goHome()
        pageForText.GoHome()
    End Sub

    Public Sub goSearch()
        pageForText.GoSearch()
    End Sub

    Public Function getPos() As Integer
        Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
        If _EditorURL = "" Or _Editing = False Or isGoodEditPage = False Or isPageLoaded = False Then Return -1

        Try
            Dim myWaitingObject As New WaitingObject
            myWaitingObject.updateWaitingData(Nothing)
            Dim waitingNo As Integer = myWebInteraction.addObjectToQueueForTextReceiving(CType(myWaitingObject, Object))

            waitingThreadPos = New System.Threading.Thread(AddressOf WaitForPos)
            waitingThreadPos.Start()
            Dim myWin As mshtml.IHTMLWindow2 = pageForEdition.Document.parentWindow
            myWin.execScript("GetPos(" & waitingNo & ");", "javascript")
            waitingThreadPos.Join()
            waitingThreadPos = Nothing

            Dim myPos As Integer = myWaitingObject.getWaitingData
            myWebInteraction.removeObjectFromQueueForTextReceiving(CType(myWaitingObject, Object))
            Return myPos
        Catch
            Return -1
        End Try
    End Function

    Public Sub setPos(ByVal pos As Integer)
        Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
        If Editing = False Or isGoodEditPage = False Or isPageLoaded = False Then Exit Sub

        Dim myWin As IHTMLWindow2 = pageForEdition.Document.parentWindow
        myWin.execScript("SetPos(" & pos & ");", "javascript")
    End Sub

    Public Sub searchAndSelect(ByVal textToSearch As String, ByVal searchPos As Integer)
        Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
        If Editing = False Or isGoodEditPage = False Or isPageLoaded = False Then Exit Sub

        Dim myWin As IHTMLWindow2 = pageForEdition.Document.parentWindow
        myWin.execScript("SearchAndSelect('" & textToSearch & "'," & searchPos & ");", "javascript")
    End Sub

    Public Sub selectText(ByVal pos As Integer, ByVal length As Integer)
        Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
        If Editing = False Or isGoodEditPage = False Or isPageLoaded = False Then Exit Sub

        Dim myWin As IHTMLWindow2 = pageForEdition.Document.parentWindow
        myWin.execScript("SelectText(" & pos & "," & length & ");", "javascript")
    End Sub

    Public Function getHTML() As String
        If _EditorURL = "" Then Return ""

        Try

            Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
            If _Editing AndAlso isGoodEditPage AndAlso isPageLoaded Then
                Dim myWaitingObject As New WaitingObject
                myWaitingObject.updateWaitingData(Nothing)
                Dim waitingNo As Integer = myWebInteraction.addObjectToQueueForTextReceiving(CType(myWaitingObject, Object))

                waitingThreadHTML = New System.Threading.Thread(AddressOf WaitForHTML)
                waitingThreadHTML.Start()
                Dim myWin As mshtml.IHTMLWindow2 = pageForEdition.Document.parentWindow
                myWin.execScript("GetHTML(" & waitingNo & ");", "javascript")
                waitingThreadHTML.Join()
                waitingThreadHTML = Nothing

                Dim myHTML As String = myWaitingObject.getWaitingData
                If myHTML Is Nothing Then myHTML = ""
                myWebInteraction.removeObjectFromQueueForTextReceiving(CType(myWaitingObject, Object))
                Return myHTML
            Else
                Dim myDoc As mshtml.HTMLDocument = pageForText.Document
                Return myDoc.body.outerHTML
            End If
        Catch
            Return ""
        End Try
    End Function

    Public Function getText() As String
        Dim htmlText As String = getHTML()
        If htmlText Is Nothing Then htmlText = ""
        Dim bodyIndex As Integer = htmlText.IndexOf("<BODY", StringComparison.OrdinalIgnoreCase)
        If bodyIndex >= 0 Then
            htmlText = htmlText.Substring(htmlText.IndexOf(">", bodyIndex) + 1)
            htmlText = htmlText.Substring(0, htmlText.IndexOf("</BODY>", StringComparison.OrdinalIgnoreCase))
        End If

        htmlText = removeCommentsTag(removeStyleTag(htmlText))
        htmlText = htmlText.Replace("<BR>", vbCrLf).Replace("<bR>", vbCrLf).Replace("<Br>", vbCrLf).Replace("<br>", vbCrLf).Replace("&nbsp;", " ")
        htmlText = System.Text.RegularExpressions.Regex.Replace(htmlText, "\<(\/)?[^>]+\>", "")
        Return htmlText.Trim
    End Function

    Private Function removeCommentsTag(ByVal html As String) As String
        Dim styleIndex As Integer = html.IndexOf("<!--", StringComparison.OrdinalIgnoreCase)
        If styleIndex < 0 Then Return html

        html = html.Substring(0, styleIndex) & html.Substring(html.IndexOf("-->", styleIndex, StringComparison.OrdinalIgnoreCase) + 3)

        Return removeCommentsTag(html)
    End Function

    Private Function removeStyleTag(ByVal html As String) As String
        Dim styleIndex As Integer = html.IndexOf("<STYLE", StringComparison.OrdinalIgnoreCase)
        If styleIndex < 0 Then Return html

        html = html.Substring(0, styleIndex) & html.Substring(html.IndexOf("</STYLE>", styleIndex, StringComparison.OrdinalIgnoreCase) + 8)

        Return removeStyleTag(html)
    End Function

    '<summary>Insert html by replacing the current selection by the HTML string passed.
    'Only functionnal in editing mode.</summary>
    Public Sub inserthtml(ByVal html As String)
        If html Is Nothing Then html = ""
        Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
        If Editing = True AndAlso isGoodEditPage AndAlso isPageLoaded Then
            Dim myWin As mshtml.IHTMLWindow2 = pageForEdition.Document.parentWindow
            myWin.execScript("InsertHTML(""" & html.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "") & """);", "javascript")
        Else
            sethtml(html & getHTML())
        End If
    End Sub

    '<System.Diagnostics.DebuggerStepThrough()> _
    Public Sub sethtml(ByVal html As String, Optional ByVal clearUndos As Boolean = False)
        If html Is Nothing Then html = ""
        Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
        If _Editing AndAlso isPageLoaded Then
            Dim htmlSet As Boolean = False
            Dim tries As Byte = 0
restartHTML:
            Dim myWin As IHTMLWindow2 = pageForEdition.Document.parentWindow
            Try
                myWin.execScript("SetHTML(""" & html.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "") & """);", "javascript")
                htmlSet = True
                Me.clearUndos()
            Catch ex As Exception
                tries += 1
                If tries >= 10 Then
                    'bug happens when the page wasn't finished loading
                    MessageBox.Show(ex.Message, "Erreur de chargement du HTML")
                    Throw ex
                End If
            End Try
            If htmlSet = False Then Threading.Thread.Sleep(100) : GoTo restartHTML
        Else
            REM IO.File.WriteAllText("C:\testing-big.html", HTML)
            loadContent(html.Replace("’", "&rsquo;"), pageForText)
        End If
    End Sub

    Public Sub clearUndos()
        Dim myWin As IHTMLWindow2 = pageForEdition.Document.parentWindow
        myWin.execScript("FTB_API['FreeTextBox1'].undoArray = [];", "javascript") 'Clear undos
    End Sub

    Public Function isUndo() As Boolean
        Dim myWin As IHTMLWindow2 = pageForEdition.Document.parentWindow
        myWin.execScript("var isUndo;isUndo=FTB_API['FreeTextBox1'].undoArrayPos<1 || FTB_API['FreeTextBox1'].undoArray.length==0?false:true;external.IsUndo(isUndo);", "javascript")

        Return _IsUndo
    End Function

    'Public Overrides Sub refresh()
    '    Try
    '        If _Editing Then
    '            _IsPageLoaded = False
    '            PageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_REFRESH, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER)
    '        Else
    '            PageForText.ExecWB(SHDocVw.OLECMDID.OLECMDID_REFRESH, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER)
    '        End If
    '    Catch ex As System.Runtime.InteropServices.COMException
    '    End Try
    'End Sub

    Public Sub surroundHTML(ByVal beforeHTML As String, ByVal afterHTML As String)
        Dim isGoodEditPage As Boolean = Me.pageForEdition.locationURL.StartsWith(Me.editorURL)
        If Editing = False Or isGoodEditPage = False Or isPageLoaded = False Then Exit Sub

        Dim myWin As IHTMLWindow2 = pageForEdition.Document.parentWindow
        myWin.execScript("SurroundHTML(""" & beforeHTML.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "") & """,""" & afterHTML.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "") & """);", "javascript")
    End Sub
#End Region
#End Region

#Region "Printing"
    Public Sub printSetup() Implements IPrintable.printOptions
        pageForText.ExecWB(SHDocVw.OLECMDID.OLECMDID_PAGESETUP, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
    End Sub

    Public Sub printPreview() Implements IPrintable.printPreview
        pageForText.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINTPREVIEW, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
    End Sub

    Public Sub print(Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False) Implements IPrintable.print
        _PrintJobSpooled = False
        If promptUser Then
            pageForText.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
        Else
            pageForText.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER)
        End If
        If waitForSpooling = True Then
            While _PrintJobSpooled = False
                Application.DoEvents()
            End While
        End If
    End Sub
#End Region

    Public Sub waitForDoc()
        While pageForText.ReadyState <> SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE AndAlso _IsPageLoaded = False
            Application.DoEvents()
        End While
    End Sub

    '<System.Diagnostics.DebuggerHidden()> 
    Public Sub showPage(Optional ByVal contenuHTML As String = "")
        nbShowedPage += 1
        isFocused = False

        If _Editing Then
            _IsPageLoaded = False
            If contenuHTML = "" And IO.File.Exists(Me.htmlPageURL) Then contenuHTML = IO.File.ReadAllText(Me.htmlPageURL)
            'Me.SetHTML(ContenuHTML)
            showingPage(pageForEdition, contenuHTML, _Editing)
        Else
            showingPage(pageForText, contenuHTML, _Editing)
        End If
    End Sub

    Private Sub showingPage(ByVal pageObject As AxSHDocVw.AxWebBrowser, Optional ByVal contenuHTML As String = "", Optional ByVal isEditing As Boolean = False)
        Dim myNull As DBNull = DBNull.Value

        If isEditing = True Then
            If _EditorURL = "" Then Exit Sub

            'If ContenuHTML = "" And System.IO.File.Exists(HTMLPageURL) Then
            'MyPage = _EditorURL & "?toolbarstyle=" & _ToolBarStyles & "&height=" & _EditorHeight & "&width=" & _EditorWidth & "&" & NbShowedPage & "&page=" & System.Web.HttpUtility.UrlEncode(HTMLPageURL)
            'Else
            myPage = _EditorURL & "?toolbarstyle=" & _ToolBarStyles & "&height=" & _EditorHeight & "&width=" & _EditorWidth & "&" & nbShowedPage & "&contenu=" & System.Web.HttpUtility.UrlEncode(contenuHTML)
            'End If

            myPage &= "&initpos=" & startupPos

            Try
                pageObject.Navigate2(CType(myPage, Object), BrowserNavConstants.navNoHistory, myNull, myNull, myNull)
            Catch
                Try
                    pageObject.Navigate(myPage, BrowserNavConstants.navNoHistory, myNull, myNull, myNull)
                Catch
                    'Not able to display the page
                End Try
            End Try
        Else
            myPage = _HTMLPageURL
            Try
                pageObject.Navigate2(CType(myPage, Object), BrowserNavConstants.navNoHistory, myNull, myNull, myNull)
            Catch
                Try
                    pageObject.Navigate(myPage, BrowserNavConstants.navNoHistory, myNull, myNull, myNull)
                Catch
                    'Not able to display the page
                End Try
            End Try
        End If
    End Sub


    Public Overloads Function focus() As Boolean
        'If Editing AndAlso Me.PageForEdition.LocationURL.StartsWith(Me.EditorURL) AndAlso IsPageLoaded Then
        '    PageForEdition.Select()
        '    Try
        '        Dim MyWin As IHTMLWindow2 = PageForEdition.Document.parentWindow
        '        MyWin.execScript("SetFocus();", "javascript")
        '    Catch
        '        'Not able to apply focus
        '    End Try
        'Else
        'MyBase.Focus()
        pageForText.Select()
        'PageForText.Focus()
        'End If

        Return True
    End Function

#Region "Events subs"
    Private Sub doc_BeforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean) Handles doc.BeforeNavigate
        onBeforeNavigate(url, flags, targetFrameName, postData, headers, cancel)
    End Sub

    Private Sub doc2_BeforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean) Handles doc2.BeforeNavigate
        onBeforeNavigate(url, flags, targetFrameName, postData, headers, cancel)
    End Sub

    Private Sub doc_NavigateComplete(ByVal url As String) Handles doc.NavigateComplete
        onNavigateComplete(url)
    End Sub

    Private Sub doc2_NavigateComplete(ByVal url As String) Handles doc2.NavigateComplete
        onNavigateComplete(url)
    End Sub

    Private Sub webControl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        If Editing Then
            pageForEdition.Focus()
        Else
            pageForText.Focus()
        End If
    End Sub

    Private Sub webControl_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        ctrlDown = e.Control
    End Sub

    Private Sub webTextControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        buildEditorContextMenu()

        pageForText.Offline = True
        pageForEdition.Offline = True
        Dim b As Object = pageForEdition.Application
        doc = DirectCast(b, SHDocVw.WebBrowser_V1)
        Dim c As Object = pageForText.Application
        doc2 = DirectCast(c, SHDocVw.WebBrowser_V1)
    End Sub
#End Region

#Region "IOleDocumentSite methods"

    Public Sub activateMe(ByRef pViewToActivate As Object) Implements IOleDocumentSite.activateMe
    End Sub

#End Region

#Region "IDocHostShowUI methods"

    Public Function showMessage(ByVal hwnd As IntPtr, ByVal msgText As String, ByVal msgCaption As String, _
                                ByVal dwType As Integer, ByVal lpstrHelpFile As String, _
                                ByVal dwHelpContext As Integer, ByRef lpResult As Integer) _
                                As Integer Implements IDocHostShowUI.showMessage
        ' msgText is the contents of the MessageBox
        ' msgCaption is the text on the MessageBox caption bar
        lpResult = 0

        ' return one of these values
        Const MessageHandled As Integer = 0    ' MsHtml won't display its MessageBox
        Const MessageNotHandled As Integer = 1 ' MsHtml will display its MessageBox
        Return MessageNotHandled
    End Function

    Public Function showHelp(ByVal hwnd As IntPtr, ByVal pszHelpFile As String, ByVal uCommand As Integer, _
                             ByVal dwData As Integer, ByVal ptMouse As mshtml.tagPOINT, ByVal pDispatchObjectHit As Object) _
                             As Integer Implements IDocHostShowUI.showHelp

        ' pDispatchObject will reference an object of
        ' type mshtml.HTMLDocumentClass, or other class
        ' representing something on the HTML page.

        ' return one of these values
        Const HelpHandled As Integer = 0    ' MsHtml won't display its Help window
        Const HelpNotHandled As Integer = 1 ' MsHtml will display its Help window
        Return HelpNotHandled
    End Function


#End Region

#Region "Protected Overridable Subs used with the WebTextInteractionObject or for events"
    Protected Overridable Sub onBeforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean)
        RaiseEvent beforeNavigate(url, flags, targetFrameName, postData, headers, cancel)
    End Sub

    Protected Overridable Sub onNavigateComplete(ByVal url As String)
        RaiseEvent navigateComplete(url)
    End Sub

    Private Sub downloadPageEditor()
        If pfeHtml = "" Then
            Dim myDoc As mshtml.HTMLDocument = pageForEdition.Document
            pfeHtml = myDoc.body.outerHTML
            Dim baseURL As String = _EditorURL.Substring(0, _EditorURL.IndexOf("/", _EditorURL.IndexOf("//") + 2))
            Dim imgDir As String = Application.StartupPath & IIf(Application.StartupPath.EndsWith("\"), "", "\") & "Images\WBT\"
            If IO.Directory.Exists(imgDir) Then My.Computer.FileSystem.DeleteDirectory(imgDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
            IO.Directory.CreateDirectory(imgDir)

            Dim wc As New Net.WebClient
            Dim imageIndex As Integer = pfeHtml.IndexOf("<IMG", 0, StringComparison.OrdinalIgnoreCase)
            Dim noImage As Integer = 0
            Dim newPFE As New System.Text.StringBuilder(pfeHtml)
            While imageIndex <> -1
                Dim startIndex As Integer = pfeHtml.IndexOf("src=""/", imageIndex, StringComparison.OrdinalIgnoreCase)
                If startIndex <> -1 Then
                    startIndex += 5

                    Dim endIndex As Integer = pfeHtml.IndexOf("""", startIndex)
                    Dim src As String = pfeHtml.Substring(startIndex, endIndex - startIndex)
                    Try
                        wc.DownloadFile(baseURL & src, imgDir & noImage & ".gif")
                        newPFE.Replace(src, imgDir & noImage & ".gif")
                    Catch ex As Exception
                    End Try
                End If

                imageIndex = pfeHtml.IndexOf("<IMG", imageIndex + 1, StringComparison.OrdinalIgnoreCase)
                noImage += 1
            End While

            newPFE.Replace("src=""/", "src=""" & baseURL & "/")

            wc.Dispose()
            pfeHtml = newPFE.ToString
        End If
    End Sub

    Protected Overridable Sub onPageLoaded()
        _IsPageLoaded = True

        REM Create a bug when Accessing Générateur de rapport. Not really faster with this caching method
        'DownloadPageEditor()

        RaiseEvent pageLoaded()
    End Sub

    Protected Overridable Sub onAddLink(ByRef handled As Boolean)
        RaiseEvent addingLink(Me, handled)
    End Sub

    Protected Overridable Sub onAddImage(ByRef handled As Boolean)
        RaiseEvent addingImage(Me, handled)
    End Sub

    Protected Overridable Overloads Sub onTextChanged(ByVal [Text] As String)
        RaiseEvent textChanged([Text])
    End Sub

    Protected Overridable Sub onTextPreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
        RaiseEvent textPreviewKeyDown(sender, e)
    End Sub

    Protected Overridable Sub onError(ByVal sMsg As String, ByVal sUrl As String, ByVal sLine As String)
        Throw New Exception("sMsg=" & sMsg & vbCrLf & "sUrl=" & sUrl & "sLine=" & sLine)
    End Sub

#End Region

    Private _IsUndo As Boolean = False

    Protected Sub onIsUndo(ByVal isUndo As Boolean)
        _IsUndo = isUndo
    End Sub

#Region "Handling Browser Document events"
    Private Interface ICustomDoc
        Sub setUIHandler(ByVal pUIHandler As IDocHostUIHandler)
    End Interface

    Private Sub addDocEvents(ByRef curDoc As Object)
        REM Handling HTML doc events, do it, but not for internal frames (Should fix it before activating)
        htmlEvent = DirectCast(curDoc.document, mshtml.HTMLDocumentEvents2_Event)

        Dim curHTMLDoc As IHTMLWindow2 = CType(curDoc, IHTMLWindow2)

        If curHTMLDoc IsNot Nothing Then
            Dim handler As DHTMLEventHandler = New DHTMLEventHandler(curHTMLDoc)
            handler.handler = AddressOf Me.clickEvent

            ' add the events in here 
            CType(curHTMLDoc.document, IHTMLDocument3).attachEvent("onclick", handler) 'New HTMLDocumentEvents2_onclickEventHandler(AddressOf Me.ClickEvent)
            'AddHandler CType(CurHTMLDoc.document, HTMLDocumentEvents2_Event).onclick, Handler.Handler 'HandlerOnClickMenu
            AddHandler htmlEvent.onclick, AddressOf clickEvent
            'AddHandler htmlEvent.ondblclick, AddressOf DoubleClickEvent
            'AddHandler htmlEvent.onfocusin, AddressOf GotFocusEvent
            'AddHandler htmlEvent.onfocusout, AddressOf LostFocusEvent
            'AddHandler htmlEvent.onkeydown, AddressOf KeyDownEvent
            'AddHandler htmlEvent.onkeypress, AddressOf KeyPressEvent
            'AddHandler htmlEvent.onkeyup, AddressOf KeyUpEvent
            'AddHandler htmlEvent.onmousedown, AddressOf MouseDownEvent
            AddHandler htmlEvent.onmousemove, AddressOf mouseMoveEvent
            AddHandler htmlEvent.onmouseout, AddressOf mouseOutEvent
            AddHandler htmlEvent.onmouseover, AddressOf mouseOverEvent
            'AddHandler htmlEvent.onmouseup, AddressOf MouseUpEvent
            'AddHandler htmlEvent.onmousewheel, AddressOf MouseWheelEvent
        End If

        Dim i As Integer
        For i = 0 To curHTMLDoc.frames.length - 1
            Dim curFrame As IHTMLWindow2
            curFrame = curHTMLDoc.frames.item(i)
            'Dim Handler As New DHTMLEventHandler(curFrame)
            'Handler.Handler = AddressOf Me.FrameLoadedEvent
            'curFrame.document.onreadystatechange = Handler

            'Dim Handler As DHTMLEventHandler = New DHTMLEventHandler(curFrame)
            'Handler.Handler = AddressOf Me.ClickEvent

            ' add the events in here 
            'curFrame.document.body.onclick = Handler
            'curFrame.document.onclick = Handler

            addDocEvents(curFrame)
            'AddDocEvents(curFrame.document)
            'AddDocEvents(curFrame.document.window)
            'AddDocEvents(curFrame.contentWindow)
            'AddDocEvents(curFrame.document.parentWindow.document)
            'AddDocEvents(CurHTMLDoc.frames.item(i).parentWindow.document)
        Next i
    End Sub

    Private Sub removeDocEvents(ByRef curDoc As Object)
        REM Handling HTML doc events, do it, but not for internal frames (Should fix it before activating)
        htmlEvent = DirectCast(curDoc.document, mshtml.HTMLDocumentEvents2_Event)

        Dim curHTMLDoc As IHTMLWindow2 = CType(curDoc, IHTMLWindow2)

        RemoveHandler htmlEvent.onclick, AddressOf clickEvent
        RemoveHandler htmlEvent.onmousemove, AddressOf mouseMoveEvent
        RemoveHandler htmlEvent.onmouseout, AddressOf mouseOutEvent
        RemoveHandler htmlEvent.onmouseover, AddressOf mouseOverEvent

        Dim i As Integer
        For i = 0 To curHTMLDoc.frames.length - 1
            Dim curFrame As IHTMLWindow2
            curFrame = curHTMLDoc.frames.item(i)
            'Dim Handler As New DHTMLEventHandler(curFrame)
            'Handler.Handler = AddressOf Me.FrameLoadedEvent
            'curFrame.document.onreadystatechange = Handler

            'Dim Handler As DHTMLEventHandler = New DHTMLEventHandler(curFrame)
            'Handler.Handler = AddressOf Me.ClickEvent

            ' add the events in here 
            'curFrame.document.body.onclick = Handler
            'curFrame.document.onclick = Handler

            removeDocEvents(curFrame)
            'AddDocEvents(curFrame.document)
            'AddDocEvents(curFrame.document.window)
            'AddDocEvents(curFrame.contentWindow)
            'AddDocEvents(curFrame.document.parentWindow.document)
            'AddDocEvents(CurHTMLDoc.frames.item(i).parentWindow.document)
        Next i
    End Sub

    Public Delegate Function dhtmlEvent(ByVal e As IHTMLEventObj) As Boolean

    <Runtime.InteropServices.ComVisible(True)> _
    Public Class DHTMLEventHandler
        Public handler As dhtmlEvent
        Private document As IHTMLWindow2

        Public Sub New(ByVal doc As IHTMLWindow2)
            Me.document = doc
        End Sub

        <Runtime.InteropServices.DispId(0)> _
        Public Sub [Call]()
            handler(Me.document.event)
        End Sub
    End Class

    Private Sub pageForEdition_BeforeNavigate2(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event) Handles pageForEdition.BeforeNavigate2
        'RemoveDocEvents(PageForEdition.Document.parentWindow)
    End Sub

    Private Sub pageForEdition_DownloadBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles pageForEdition.DownloadBegin

    End Sub

    Private Sub pageForText_CommandStateChange(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent) Handles pageForText.CommandStateChange
        Dim oldValue As Boolean
        Dim historyChange As Boolean = False
        Select Case e.command
            Case SHDocVw.CommandStateChangeConstants.CSC_NAVIGATEBACK
                oldValue = _isBackPossible
                historyChange = True
                _isBackPossible = e.enable
            Case SHDocVw.CommandStateChangeConstants.CSC_NAVIGATEFORWARD
                oldValue = _isForwardPossible
                historyChange = True
                _isForwardPossible = e.enable
        End Select

        If oldValue <> e.enable Then RaiseEvent historyChanged(Me)
    End Sub

    Private Sub pageForEdition_DocumentComplete(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent) Handles pageForEdition.DocumentComplete, pageForText.DocumentComplete
        If pageForEdition.ReadyState <> SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE Then Exit Sub

        Dim curDoc As SHDocVw.IWebBrowser2 = e.pDisp
        REM 'Ajoute le gestionnaire d'erreur
        'Dim prevBody As String = ""
        'If sender.Equals(PageForEdition) AndAlso CType(CurDoc.Document, mshtml.IHTMLDocument2).body.outerHTML IsNot Nothing Then
        '    'prevBody = CType(CurDoc.Document, mshtml.IHTMLDocument2).body.outerHTML.ToString
        '    'if (window.external) {window.external.OnError(sMsg,sUrl,sLine);}return false;
        '    With CType(CurDoc.Document, mshtml.IHTMLDocument2)
        '        'Dim scriptElement As mshtml.IHTMLElement
        '        'scriptElement = .createElement("script")

        '        Dim scriptTag As String = "window.onerror=fnErrorTrap;function fnErrorTrap(sMsg,sUrl,sLine){alert(2);}function fnThrow() {alert(1);eval(""someObject.someProperty=true;"");}"
        '        'scriptElement.innerHTML = scriptTag
        '        '.body.insertAdjacentHTML("afterBegin", )
        '        .body.innerHTML = "<script id=errorScript DEFER>" & scriptTag & "</script><input type=button value=Throw onclick=alert(document.getElementById('errorScript'));alert(document.body.innerHTML);>" & .body.innerHTML
        '    End With
        'End If

        REM Essaies pour pouvoir cliquer sur un lien en mode édition
        'Déctection du clique en utilisant AddDocEvents, mais pas dans la IFRAME (donc, pas ds la boîte d'édition)
        'Détection du clique OK en utilisant AddHandler en bas de la méthode, mais bug en chargeant (on ne peut plus cliquer, sauf en gossant en changeant de fenêtre)

        'CType(e.pDisp, IWebBridge)
        'If PageForEdition.Document.parentWindow.frames.length > 0 Then
        'CType(PageForEdition.Document.parentWindow.frames(0).document, IHTMLDocument2).onreadystatechange = AddressOf PageForEdition_DocumentComplete
        'addhandler 
        'End If
        ''If CurDoc Is CType(sender, AxSHDocVw.AxWebBrowser).GetOcx() Then
        ''    Dim t As Integer = 0
        ''End If
        ''if (curdoc <>(sender as AxSHDocVw.AxWebBrowser).GetOcx()) the

        'Dim docHTML As mshtml.HTMLDocument
        'docHTML = PageForEdition.Document

        'AddHandler CType(docHTML, mshtml.HTMLDocumentEvents2_Event).onclick, AddressOf ClickEvent
        'Dim CurBody As String = CType(CurDoc.Document, IHTMLDocument2).body.innerHTML
        'If CurBody Is Nothing Then CurBody = ""

        '    AddDocEvents(CType(CType(sender, AxSHDocVw.AxWebBrowser).Document, IHTMLDocument2).parentWindow)
        'AddDocEvents(CurDoc.Document.parentWindow)
    End Sub

    Private Sub frameLoadedEvent(ByVal e As IHTMLEventObj)
        addDocEvents(pageForEdition.Document.parentWindow)
    End Sub

    Public Event textClick(ByVal e As IHTMLEventObj)

    Private Sub body_Click(ByVal sender As Object, ByVal e As IHTMLEventObj)
        Dim h As IHTMLElement = e.srcElement
        If ((h IsNot Nothing) And (h.tagName = "A")) And _ActivateLinksOnEdit And ctrlDown Then
            ctrlDown = False
            editorLinkJustClicked = True
            RaiseEvent editorLinkClicked(Me, New LinkClickedEventArgs(h.getAttribute("href")))
        End If
    End Sub

    Public Sub editorClick(ByVal url As String)
        If _ActivateLinksOnEdit And ctrlDown Then
            Me.onBeforeNavigate(url, 0, "", New Object, "", False)
            ctrlDown = False
        End If
    End Sub

    Private Function clickEvent(ByVal e As IHTMLEventObj) As Boolean
        Me.body_Click(Me, e)
        RaiseEvent textClick(e)
        Return True
    End Function

    Public Event textDoubleClick(ByVal e As IHTMLEventObj)

    Private Function doubleClickEvent(ByVal e As IHTMLEventObj) As Boolean
        RaiseEvent textDoubleClick(e)
    End Function

    Public Event textGotFocus(ByVal e As IHTMLEventObj)

    Private Sub gotFocusEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textGotFocus(e)
    End Sub

    Public Event textLostFocus(ByVal e As IHTMLEventObj)

    Private Sub lostFocusEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textLostFocus(e)
    End Sub

    Public Event textKeyPress(ByVal e As IHTMLEventObj)

    Private Function keyPressEvent(ByVal e As IHTMLEventObj) As Boolean
        RaiseEvent textKeyPress(e)
        Return False
    End Function

    Public Event textKeyUp(ByVal e As IHTMLEventObj)

    Private Sub keyUpEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textKeyUp(e)
    End Sub

    Public Event textKeyDown(ByVal e As IHTMLEventObj)

    Private Sub keyDownEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textKeyDown(e)
    End Sub

    Public Event textMouseDown(ByVal e As IHTMLEventObj)

    Private Sub mouseDownEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textMouseDown(e)
    End Sub

    Public Event textMouseMove(ByVal e As IHTMLEventObj)

    Private Sub mouseMoveEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textMouseMove(e)
    End Sub

    Public Event textMouseOut(ByVal e As IHTMLEventObj)

    Private Sub mouseOutEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textMouseOut(e)
    End Sub

    Public Event textMouseOver(ByVal e As IHTMLEventObj)

    Private Sub mouseOverEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textMouseOver(e)
    End Sub

    Public Event textMouseUp(ByVal e As IHTMLEventObj)

    Private Sub mouseUpEvent(ByVal e As IHTMLEventObj)
        RaiseEvent textMouseUp(e)
    End Sub

    Public Event textMouseWheel(ByVal e As IHTMLEventObj)

    Private Function mouseWheelEvent(ByVal e As IHTMLEventObj) As Boolean
        RaiseEvent textMouseWheel(e)
    End Function
#End Region

#Region "Public Class WebControlInteraction - Used as javascript external object"
    Public Class WebControlInteraction
        Implements IWebTextInteract

        Private myOwner As WebControl
        Private myTextQueue As New ArrayList()

        Public Sub New(ByVal ownerForm As WebControl)
            myOwner = ownerForm
        End Sub

#Region "Functions used by the HTML page"
        Public Sub sendTextTo(ByVal [Text] As String, ByVal waitingNumber As Integer) Implements IWebTextInteract.sendTextTo
            CType(myTextQueue(waitingNumber), WaitingObject).updateWaitingData([Text])
            myOwner.stopWaitForHTML()
        End Sub

        Public Sub sendposTo(ByVal pos As Integer, ByVal waitingNumber As Integer) Implements IWebTextInteract.sendposTo
            CType(myTextQueue(waitingNumber), WaitingObject).updateWaitingData(pos)
            myOwner.stopWaitForPos()
        End Sub

        Public Sub isUndo(ByVal isUndo As Boolean)
            myOwner.onIsUndo(isUndo)
        End Sub

        Public Sub pageLoaded() Implements IWebTextInteract.pageLoaded
            myOwner.onPageLoaded()
        End Sub

        Public Sub addLink() Implements IWebTextInteract.addLink
            myOwner.onAddLink(False)
        End Sub

        Public Sub addImage() Implements IWebTextInteract.addImage
            myOwner.onAddImage(False)
        End Sub

        Public Sub editorClick(ByVal url As String) Implements IWebTextInteract.editorClick
            myOwner.editorClick(url)
        End Sub

        Public Sub textChanged(ByVal [Text] As String) Implements IWebTextInteract.textChanged
            myOwner.onTextChanged([Text])
        End Sub

        Public Sub onError(ByVal sMsg As String, ByVal sUrl As String, ByVal sLine As String) Implements IWebTextInteract.onError
            myOwner.onError(sMsg, sUrl, sLine)
        End Sub
#End Region

        Public Function addObjectToQueueForTextReceiving(ByRef waitingObj As Object) As Integer
            Return ArrayList.Synchronized(myTextQueue).Add(waitingObj)
        End Function

        Public Function getObjectFromQueueForTextReceiving(ByRef waitingObjIndex As Integer) As WaitingObject
            Return ArrayList.Synchronized(myTextQueue).Item(waitingObjIndex)
        End Function

        Public Sub removeObjectFromQueueForTextReceiving(ByRef waitingObj As Object)
            ArrayList.Synchronized(myTextQueue).Remove(waitingObj)
        End Sub
    End Class
#End Region

#Region "Public Class WaitingObject - Used to send a function to the page and wait for its return"
    Public Class WaitingObject
        Private myWaitingData As New Object

        Public Sub New()

        End Sub

        Public Sub updateWaitingData(ByVal data As Object)
            myWaitingData = data
        End Sub

        Public Function getWaitingData() As Object
            Return myWaitingData
        End Function
    End Class
#End Region

    Private Sub pageForEdition_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles pageForEdition.PreviewKeyDown
        If editorLinkJustClicked Then
            editorLinkJustClicked = False
            Exit Sub
        End If
        ctrlDown = e.Control
    End Sub

    Private Sub webControl_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
        ctrlDown = e.Control
    End Sub

    Private Sub pageForText_PrintTemplateInstantiation(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PrintTemplateInstantiationEvent) Handles pageForText.PrintTemplateInstantiation

    End Sub

    Private Sub pageForText_PrintTemplateTeardown(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PrintTemplateTeardownEvent) Handles pageForText.PrintTemplateTeardown
        _PrintJobSpooled = True
    End Sub
End Class
