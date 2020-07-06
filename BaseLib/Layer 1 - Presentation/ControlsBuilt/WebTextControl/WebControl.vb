Imports Win32 = Microsoft.Win32
Imports System.Runtime.InteropServices
Imports mshtml
Imports MsHtmHstInterop
Imports System.Drawing
Imports System.Security.Permissions

Namespace Windows.Forms



    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
    <System.Runtime.InteropServices.ComVisibleAttribute(True)> _
    Public Class WebControl
        Inherits System.Windows.Forms.UserControl
        Implements IOleDocumentSite, IDocHostShowUI, IPrintable

#Region "Class IEHoster"
        Private Class IEHoster
            Implements IDocHostUIHandler, IOleClientSite, IDisposable

            Private ctrl_alt_NumberCalledOnce As Boolean = False
            Private myWebInteraction As WebControlInteraction
            Private myWebControl As WebControl
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

            Public Sub New(ByVal curWebControl As WebControl, ByVal curWebInteraction As WebControlInteraction, ByVal allowRefresh As Boolean, ByVal allowScrolling As Boolean)
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

            Public Property allowRefresh() As Boolean
                Get
                    Return _AllowRefresh
                End Get
                Set(ByVal value As Boolean)
                    _AllowRefresh = value
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
                CType(myWebControl, Control).Select() ' Ensure window is focused when control is clicked
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
                'REM Const WM_KEYDOWN As Integer = &H100

                'Both param check up is necessary upon different IE/Windows version (?)
                Dim keyPressed As Integer = lpmsg.wParam And &HFF ' get the virtual keycode
                If keyPressed = 0 Then keyPressed = lpmsg.lParam And &HFF ' get the virtual keycode

                'Disable refresh F5
                If keyPressed = 116 And _AllowRefresh = False Then Throw New COMException("", KillAccelerator)
                'allow anything except when CTRL or ALT are down
                If (Control.ModifierKeys And Keys.Control) <> Keys.Control And (Control.ModifierKeys And Keys.Alt) <> Keys.Alt Then
                    Throw New COMException("", AllowAccelerator)
                End If

                'Allow CTRL-TAB
                'MsgBox(lpmsg.wParam)
                'Try to catch CTRL-TAB when focus on control (now, not switching window)
                'MyMainWin.StatusText = "T:" & lpmsg.wParam
                'If keyPressed= 9 Then
                '    myWebControl.OnKeyUp(New Windows.Forms.KeyEventArgs(Keys.Tab))

                '    'RaiseEvent  myWebControl.ParentForm.KeyPress(new Windows.Forms.KeyPressEventArgs(vbTab.ToCharArray()(0)))
                'End If

                ' allow, CTRL-Z,CTRL-A,CTRL-X,CTRL-C,CTRL-V
                If (keyPressed = 90 And allowUndo) Or keyPressed = 65 Or keyPressed = 35 Or keyPressed = 36 Or keyPressed = 37 Or keyPressed = 39 Or keyPressed = 88 Or keyPressed = 67 Or keyPressed = 86 Then
                    If keyPressed = 65 Then RaiseEvent acceleratorCTRL_A()
                    Throw New COMException("", AllowAccelerator)
                End If
                'allow CTRL+ATL+ something which gives a char
                If (keyPressed = 226 Or keyPressed = 190 Or keyPressed = 188 Or keyPressed = 220 Or keyPressed = 192 Or keyPressed = 186 Or keyPressed = 221 Or keyPressed = 219 Or keyPressed = 222 Or keyPressed = 189 Or keyPressed = 187 Or (keyPressed >= 48 And keyPressed <= 57)) Then
                    If ctrl_alt_NumberCalledOnce = False Then
                        ctrl_alt_NumberCalledOnce = True
                        Dim asciiCode As Integer = lpmsg.wParam
                        If keyPressed = 226 Then asciiCode = 176
                        If keyPressed = 190 Then asciiCode = 173
                        If keyPressed = 188 Then asciiCode = 175
                        If keyPressed = 220 Then asciiCode = 125
                        If keyPressed = 192 Then asciiCode = 123
                        If keyPressed = 186 Then asciiCode = 126
                        If keyPressed = 219 Then asciiCode = 91
                        If keyPressed = 221 Then asciiCode = 93
                        If keyPressed = 222 Then asciiCode = 92
                        If keyPressed = 50 Then asciiCode = 64
                        If keyPressed = 49 Then asciiCode = 177
                        If keyPressed = 54 Then asciiCode = 172
                        If keyPressed = 51 Then asciiCode = 163
                        If keyPressed = 52 Then asciiCode = keyPressed + 110
                        If keyPressed = 53 Then asciiCode = keyPressed + 111
                        If keyPressed = 55 Then asciiCode = 166
                        If (keyPressed = 56 Or keyPressed = 57) Then asciiCode = keyPressed + 122
                        If keyPressed = 48 Then asciiCode = 188
                        If keyPressed = 187 Then asciiCode = 190
                        RaiseEvent ctrl_alt_Number(asciiCode)
                    Else
                        ctrl_alt_NumberCalledOnce = False
                    End If
                    Throw New COMException("", KillAccelerator)
                End If
                ' disable everything else
                'Throw New COMException("", AllowAccelerator)
            End Sub

            Public Sub translateUrl(ByVal dwTranslate As UInteger, ByRef pchURLIn As UShort, ByVal ppchURLOut As System.IntPtr) Implements IDocHostUIHandler.TranslateUrl

            End Sub

            Public Sub updateUI() Implements IDocHostUIHandler.UpdateUI

            End Sub
#End Region

            Private disposedValue As Boolean = False        ' Pour détecter les appels redondants

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        If myWebControl IsNot Nothing AndAlso myWebControl.IsDisposed = False Then
                            myWebControl.Dispose()
                            myWebControl = Nothing
                        End If
                    End If

                    _ContextMenu = Nothing
                End If
                Me.disposedValue = True
            End Sub

#Region " IDisposable Support "
            ' Ce code a été ajouté par Visual Basic pour permettre l'implémentation correcte du modèle pouvant être supprimé.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Ne modifiez pas ce code. Ajoutez du code de nettoyage dans Dispose(ByVal disposing As Boolean) ci-dessus.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

        End Class
#End Region

#Region "Editor context menu"
        Private Sub menuAnnuler(ByVal sender As Object, ByVal e As EventArgs)
            Me.hasUndo()
            My.Computer.Keyboard.SendKeys("^z")
            If Me.hasUndo Then Me.onTextChanged(Me.getHTML)
            'Me.PageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_UNDO, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
        End Sub

        Private Sub menuColler(ByVal sender As Object, ByVal e As EventArgs)
            'Tried this other method to try to fix a Windows 7 pasting problem (removing front spaces of lines)
            'DirectCast(Me.pageForEdition.Document, IHTMLDocument2).execCommand("Paste")
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
            sender.Items(0).Enabled = Me.hasUndo
            sender.Items(2).Enabled = e.isOnSelection
            sender.Items(3).Enabled = e.isOnSelection
            sender.Items(4).Enabled = My.Computer.Clipboard.ContainsText OrElse Clipboard.ContainsData(System.Windows.Forms.DataFormats.Html)
        End Sub

        Private Sub editorSelectAll()
            Me.pageForEdition.ExecWB(SHDocVw.OLECMDID.OLECMDID_SELECTALL, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
        End Sub

        Private Sub editorCTRL_ALT_Number(ByVal numberKeyCode As Integer)
            Me.insertHtml(Chr(numberKeyCode))
        End Sub

        Protected Overrides Sub onKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            MyBase.OnKeyPress(e)
        End Sub

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call
            ieHosterText = New IEHoster(Me, myWebInteraction, _AllowRefresh, True)
            ieHosterEdition = New IEHoster(Me, myWebInteraction, True, False) 'AllowRefresh is set to True and is really important, otherwise the letter T doesn't work (though, refresh is not working, which is good)
            Me.allowUndo = True
            AddHandler ieHosterEdition.willContextMenu, AddressOf willEditorContextMenu
            AddHandler ieHosterEdition.acceleratorCTRL_A, AddressOf editorSelectAll
            AddHandler ieHosterEdition.ctrl_alt_Number, AddressOf editorCTRL_ALT_Number
            Dim obj As Object = pageForView.GetOcx()
            Dim oc As IOleObject = obj
            oc.setClientSite(ieHosterText)
            obj = pageForEdition.GetOcx()
            oc = obj
            oc.setClientSite(ieHosterEdition)

            pageForView.Navigate2("about:blank")
            pageForEdition.Navigate2("about:blank")

            'addhandler PageForText.MouseCaptureChanged,addressof
            AddHandler pageForView.PreviewKeyDown, AddressOf onTextPreviewKeyDown
            AddHandler pageForEdition.PreviewKeyDown, AddressOf onTextPreviewKeyDown
        End Sub

        'UserControl overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
            If disposing AndAlso Not _isDisposed Then
                _isDisposed = True

                If Not pageForView Is Nothing AndAlso pageForView.IsDisposed = False Then
                    RemoveHandler pageForView.PreviewKeyDown, AddressOf onTextPreviewKeyDown
                    pageForView.Dispose()
                    pageForView = Nothing
                End If
                If Not pageForEdition Is Nothing AndAlso pageForEdition.IsDisposed = False Then
                    RemoveHandler pageForEdition.PreviewKeyDown, AddressOf onTextPreviewKeyDown
                    pageForEdition.Dispose()
                    pageForEdition = Nothing
                End If
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                If Not ieHosterText Is Nothing Then
                    ieHosterText.Dispose()
                    ieHosterText = Nothing
                End If
                If Not ieHosterEdition Is Nothing Then
                    ieHosterEdition.Dispose()
                    ieHosterEdition = Nothing
                End If

                If Not editionContextMenu Is Nothing Then
                    editionContextMenu.Dispose()
                    editionContextMenu = Nothing
                End If

                If Not myWebInteraction Is Nothing Then
                    myWebInteraction.Dispose()
                    myWebInteraction = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        Public Class BrowserPage
            Inherits AxSHDocVw.AxWebBrowser

            <ComImport()> _
            <Guid("0000010D-0000-0000-C000-000000000046")> _
            <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
            Private Interface IViewObject
                Sub Draw(<MarshalAs(UnmanagedType.U4)> ByVal dwAspect As UInteger, ByVal lindex As Integer, ByVal pvAspect As IntPtr, <[In]()> ByVal ptd As IntPtr, ByVal hdcTargetDev As IntPtr, ByVal hdcDraw As IntPtr, _
                 <MarshalAs(UnmanagedType.Struct)> ByRef lprcBounds As RECT, <[In]()> ByVal lprcWBounds As IntPtr, ByVal pfnContinue As IntPtr, <MarshalAs(UnmanagedType.U4)> ByVal dwContinue As UInteger)
            End Interface

            <StructLayout(LayoutKind.Sequential, Pack:=4)> _
            Private Structure RECT
                Public Left As Integer
                Public Top As Integer
                Public Right As Integer
                Public Bottom As Integer
            End Structure

            Public Function drawToImage() As Image
                'TODO : Try to get webpage into image (so could be passed to any printer or else)

                Dim myDoc As mshtml.HTMLDocument = Me.Document
                Dim myWidth As Integer = myDoc.body.style.width
                Dim myHeight As Integer = myDoc.body.style.height

                Dim destination As New Bitmap(1000, 3000)

                Using graphics__1 As Graphics = Graphics.FromImage(destination)
                    Dim deviceContextHandle As IntPtr = IntPtr.Zero
                    Dim rectangle As New RECT()

                    rectangle.Right = destination.Width
                    rectangle.Bottom = destination.Height

                    graphics__1.Clear(Color.White)

                    Try
                        deviceContextHandle = graphics__1.GetHdc()

                        Dim viewObject As IViewObject = TryCast(Me.Application, IViewObject)
                        viewObject.Draw(1, -1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, deviceContextHandle, _
                         rectangle, IntPtr.Zero, IntPtr.Zero, 0)
                    Finally
                        If deviceContextHandle <> IntPtr.Zero Then
                            graphics__1.ReleaseHdc(deviceContextHandle)
                        End If
                    End Try
                End Using

                Return destination
            End Function

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

        Public Function drawToImage() As Image
            Return pageForView.drawToImage()
        End Function

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Private WithEvents pageForView As BrowserPage
        Private WithEvents pageForEdition As BrowserPage
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebControl))
            Me.pageForView = New WebControl.BrowserPage
            Me.pageForEdition = New WebControl.BrowserPage
            CType(Me.pageForView, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.pageForEdition, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'PageForText
            '
            Me.pageForView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pageForView.Enabled = True
            Me.pageForView.Location = New System.Drawing.Point(0, 0)
            Me.pageForView.OcxState = CType(resources.GetObject("PageForText.OcxState"), System.Windows.Forms.AxHost.State)
            Me.pageForView.Size = New System.Drawing.Size(336, 288)
            Me.pageForView.TabIndex = 0
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
            Me.Controls.Add(Me.pageForView)
            Me.Controls.Add(Me.pageForEdition)
            Me.Name = "WebControl"
            Me.Size = New System.Drawing.Size(336, 288)
            CType(Me.pageForView, System.ComponentModel.ISupportInitialize).EndInit()
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

        Private Const MAX_PRINT_WAIT_LOOP As Integer = 5000

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

        Private Shared syncLockBetweenLoadingAndPrint As New Object()

        Private editionContextMenu As New ContextMenuStrip()

        Private _isDisposed As Boolean = False
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
        Private _allowPopupWindows As Boolean = True
        Private _EditorContextMenu As ContextMenuStrip
        Private _EditorHeight As Integer = 350
        Private _EditorWidth As Integer = 460
        Private WithEvents _browserForEdition As SHDocVw.DWebBrowserEvents_Event
        Private WithEvents _browserForView As SHDocVw.DWebBrowserEvents_Event
        Private WithEvents htmlEvent As mshtml.HTMLDocumentEvents2_Event
        Protected myPage As String
        Private nbShowedPage As Integer = 0
        Private isFocused As Boolean = False
        Private _ToolBarStyles As Integer = 1
        Private myWebInteraction As New WebControlInteraction(Me)
        Private _StartupPos As Integer = 0
        Private _useNavigationCache As Boolean = True
        Private _IsPageLoaded As Boolean = False
        Private _ActivateLinksOnEdit As Boolean = True
        Private _viewDisableHtmlFields As Boolean = False

        Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
        Private Declare Function SetFocus Lib "user32" (ByVal hwnd As Long) As Long
        Public Declare Function apiFindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Int32, ByVal hWnd2 As Int32, ByVal lpsz1 As String, ByVal lpsz2 As String) As Int32
        Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByRef lParam As IntPtr) As Integer
        Declare Auto Function SendMessage Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wparam As Integer, ByVal lparam As System.Text.StringBuilder) As IntPtr

        Delegate Sub invokeSub()


        Public Event beforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean)
        Public Event navigateComplete(ByVal url As String)
        Public Event DocumentComplete(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent)
        Public Shadows Event textChanged(ByVal theText As String)
        Public Event textPreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
        Public Event textSaved(ByVal theText As String)
        Public Event pageSaved()
        Public Event requestNextAnchor()
        Public Event pageLoaded()
        Public Event addingLink(ByRef sender As WebControl, ByRef handled As Boolean)
        Public Event insertingDate(ByRef sender As WebControl, ByVal fieldId As String, ByVal selectedDate As String, ByRef handled As Boolean)
        Public Event addingImage(ByRef sender As WebControl, ByRef handled As Boolean)
        Public Event editorLinkClicked(ByVal sender As Object, ByVal e As LinkClickedEventArgs)
        Public Event historyChanged(ByRef sender As WebControl)

#Region "Propriétés"
        Protected ReadOnly Property browserForEdition() As BrowserPage
            Get
                Return pageForEdition
            End Get
        End Property

        Protected ReadOnly Property browserForView() As BrowserPage
            Get
                Return pageForView
            End Get
        End Property

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
                ieHosterText.allowRefresh = value
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
        Public Property allowPopupWindows() As Boolean
            Get
                Return _allowPopupWindows
            End Get
            Set(ByVal value As Boolean)
                _allowPopupWindows = value
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

        Public ReadOnly Property editorLocation() As String
            Get
                Dim myWin As mshtml.IHTMLWindow2 = pageForEdition.Document.parentWindow
                Return myWin.location.href
            End Get
        End Property

        Public ReadOnly Property viewLocation() As String
            Get
                Try
                    If pageForView.Document.parentWindow IsNot System.DBNull.Value Then
                        Dim myWin As mshtml.IHTMLWindow2 = pageForView.Document.parentWindow
                        Return myWin.location.href
                    End If
                Catch ex As Exception
                    'Not able to access parentWindow, will try with document itself
                End Try

                Dim myDoc As mshtml.IHTMLDocument2 = pageForView.Document
                Return myDoc.location.href
            End Get
        End Property

        Public Property viewDisableHtmlFields() As Boolean
            Get
                Return _viewDisableHtmlFields
            End Get
            Set(ByVal value As Boolean)
                _viewDisableHtmlFields = value
            End Set
        End Property

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

        Public Property Silent() As Boolean
            Get
                Return pageForView.Silent
            End Get
            Set(ByVal value As Boolean)
                Me.pageForEdition.Silent = value
                Me.pageForView.Silent = value
            End Set
        End Property

        Public Property Editing(Optional ByVal showThePage As Boolean = True) As Boolean
            Get
                Return _Editing
            End Get
            Set(ByVal Value As Boolean)
                If _Editing = Value Then Exit Property

                Dim html As String = ""
                If showThePage And _IsPageLoaded Then html = getHTML()
                If html Is Nothing Then html = ""
                _Editing = Value
                If Me.DesignMode = True Then Exit Property

                If Value = True Then
                    pageForEdition.BringToFront()
                    pageForEdition.Dock = DockStyle.Fill
                    pageForView.Dock = DockStyle.None
                Else
                    pageForView.BringToFront()
                    pageForEdition.Dock = DockStyle.None
                    pageForView.Dock = DockStyle.Fill
                End If

                If showThePage And _IsPageLoaded Then
                    setHtml(html)
                    If html <> "" Then
                        'TODO : WEBTEXTPAGE-POS Is this still useful ?
                        'Dim curPos As String = getPos() 'TO scroll cursor into view
                        'setPos(curPos) 'TO scroll cursor into view
                    End If
                End If
            End Set
        End Property

        Public Property useNavigationCache() As Boolean
            Get
                Return _useNavigationCache
            End Get
            Set(ByVal value As Boolean)
                _useNavigationCache = value
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
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub WaitForHTML()
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

        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub WaitForPos()
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

        Private Function disableFormFields(ByVal html As String) As String
            Dim newHtml As String = html
            For Each match As System.Text.RegularExpressions.Match In System.Text.RegularExpressions.Regex.Matches(html, "\<textarea[^>]*\>([^<>]*)\<\/textarea\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                Dim textarea As String = match.Groups(0).Value
                newHtml = newHtml.Replace(match.Groups(0).Value, "<span>" & match.Groups(1).Value.Replace(vbCr, "<BR>") & "</span>")
            Next
            html = newHtml

            html = System.Text.RegularExpressions.Regex.Replace(html, "\<select[^<>]+\>(<option[^<>]*\>[^<>]*\<\/option\>)*<option [^<>]*selected[^<>]*\>([^<>]*)\<\/option\>(<option[^<>]*\>[^<>]*\<\/option\>)*\<\/select\>", "$2", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            Dim tagsToDisable() As String = New String() {"input", "textarea", "select"}
            For Each curTag As String In tagsToDisable
                html = System.Text.RegularExpressions.Regex.Replace(html, "\<" & curTag, "<" & curTag & " disabled ", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            Next

            Return html
        End Function


        'To load document in PageForText not for edition
        Private Sub loadContent(ByVal html As String, ByVal page As WebControl.BrowserPage)
            System.Threading.Monitor.Enter(syncLockBetweenLoadingAndPrint)

            Try
                html = encodeSpecialCharacters(html)
                If _viewDisableHtmlFields Then html = disableFormFields(html)

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
            Catch ex As Exception
                External.propagateErrorLog(ex)
            Finally
                System.Threading.Monitor.Exit(syncLockBetweenLoadingAndPrint)
            End Try
        End Sub
#End Region

#Region "Fonctions publiques"
        Public Function documentTitle() As String
            Return CType(pageForView.Document, IHTMLDocument2).title
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

            CType(pageForView.Document, IHTMLDocument2).parentWindow.history.go(-1)
        End Sub

        Public Sub goForward()
            If _isForwardPossible = False Then Exit Sub

            CType(pageForView.Document, IHTMLDocument2).parentWindow.history.go(1)
        End Sub

        Public Sub goHome()
            pageForView.GoHome()
        End Sub

        Public Sub goSearch()
            pageForView.GoSearch()
        End Sub

        Public Function getPos() As String
            If _EditorURL = "" Or _Editing = False Or isGoodEditPage() = False Or isPageLoaded = False Then Return -1

            '
            Try
                Dim start As Date = Date.Now
                Dim myWaitingObject As New WaitingObject
                myWaitingObject.updateWaitingData(Nothing)
                Dim waitingNo As Integer = myWebInteraction.addObjectToQueueForTextReceiving(CType(myWaitingObject, Object))

                waitingThreadPos = New System.Threading.Thread(AddressOf WaitForPos)
                waitingThreadPos.Start()
                callEditionJavaScript("if (WebTextPage.savePos) WebTextPage.savePos(); else WebTextPage.getPos(" & waitingNo & ");")
                waitingThreadPos.Join()
                waitingThreadPos = Nothing

                Dim myPos As String = myWaitingObject.getWaitingData
                myWebInteraction.removeObjectFromQueueForTextReceiving(CType(myWaitingObject, Object))
                Dim m1 As Double = Date.Now.Subtract(start).TotalMilliseconds
                'MsgBox("m1=" & myPos & "=" & m1)
                start = Date.Now
                'callEditionJavaScript("savePos();")
                'Dim myDoc As mshtml.HTMLDocument = pageForEdition.Document()
                'Dim r As Object = myDoc.selection.createRange()
                'Dim p As Integer = Math.Abs(r.move("character", -100000000))
                If Me.FindForm().MdiParent.Text.IndexOf("Admin") <> -1 Then
                    MsgBox("m1=" & myPos & "=" & m1 & vbCrLf & "m2=0=" & Date.Now.Subtract(start).TotalMilliseconds)
                End If

                Return myPos
            Catch
                Return -1
            End Try
        End Function

        Public Sub setPos(ByVal pos As String)
            Dim intPos As Integer = -1
            Dim isNumber As Boolean = Integer.TryParse(pos, intPos)
            If isNumber AndAlso intPos < 0 Then Exit Sub

            If Editing = False OrElse isGoodEditPage() = False Then
                'TODO : setPos on view doesn't work.. though doesn't crash
                callViewJavaScript("var r = document.selection.createRange();r.moveStart('character', " & pos & ");r.moveEnd('character', " & pos & ");")
            Else
                If isNumber Then
                    callEditionJavaScript("WebTextPage.setPos(" & pos & ");")
                Else
                    callEditionJavaScript("WebTextPage.restorePos(""" & pos & """);")
                End If

            End If
        End Sub

        Private Function isGoodEditPage() As Boolean
            Dim testingURL As String = Me.pageForEdition.locationURL.Replace("file:///", "")
            Dim testingEditor As String = Me.editorURL.Replace("\", "/")

            Return testingURL.StartsWith(testingEditor)
        End Function

        Public Sub searchAndSelect(ByVal textToSearch As String, ByVal searchPos As Integer, ByVal moveCursorToEndOfFoundText As Boolean)
            If Editing = False OrElse isGoodEditPage() = False OrElse isPageLoaded = False Then Exit Sub

            callEditionJavaScript("WebTextPage.searchAndSelect('" & textToSearch & "'," & searchPos & "," & moveCursorToEndOfFoundText.ToString.ToLower & ");")
        End Sub

        Public Sub selectText(ByVal pos As Integer, ByVal length As Integer)
            If Editing = False Or isGoodEditPage() = False Or isPageLoaded = False Then Exit Sub

            callEditionJavaScript("WebTextPage.selectText(" & pos & "," & length & ");")
        End Sub

        Private Function getHTMLForEdition() As String
            Dim myWaitingObject As New WaitingObject
            myWaitingObject.updateWaitingData(Nothing)
            Dim waitingNo As Integer = myWebInteraction.addObjectToQueueForTextReceiving(CType(myWaitingObject, Object))

            waitingThreadHTML = New System.Threading.Thread(AddressOf WaitForHTML)
            waitingThreadHTML.Start()
            callEditionJavaScript("WebTextPage.getHTML(" & waitingNo & ");")
            waitingThreadHTML.Join()
            waitingThreadHTML = Nothing

            Dim myHTML As String = myWaitingObject.getWaitingData
            If myHTML Is Nothing Then myHTML = ""
            myWebInteraction.removeObjectFromQueueForTextReceiving(CType(myWaitingObject, Object))

            Return myHTML
        End Function

        Private Function convertHTMLEncoding(ByVal html As String, ByVal encoding As String)
            Dim html2 As String = html
            'Dim toEncoding As System.Text.Encoding = System.Text.Encoding.Unicode ' System.Text.Encoding.GetEncoding("windows-1252")
            'Dim originEncoding As System.Text.Encoding = System.Text.Encoding.UTF8 'System.Text.Encoding.GetEncoding(encoding)
            'html = IO.File.ReadAllText("C:\t.t", originEncoding)
            'Dim htmlBytes() As Byte = originEncoding.GetBytes(html)
            'Dim finalEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("windows-1252")

            'IO.File.WriteAllText("c:\t2.t", html, System.Text.Encoding.UTF8)
            'Dim newBytes() As Byte = System.Text.Encoding.Convert(toEncoding, originEncoding, toEncoding.GetBytes(html))
            'Dim newBytes1() As Byte = System.Text.Encoding.Convert(originEncoding, finalEncoding, newBytes)
            'Dim t As String = finalEncoding.GetString(newBytes1)

            'MsgBox(html, , "1")
            'Dim newBytes2() As Byte = System.Text.Encoding.Convert(originEncoding, finalEncoding, newBytes)
            'Dim newBytes3() As Byte = System.Text.Encoding.Convert(originEncoding, finalEncoding, toEncoding.GetBytes(html))
            'Dim newBytes4() As Byte = System.Text.Encoding.Convert(originEncoding, finalEncoding, htmlBytes)
            'MsgBox(finalEncoding.GetString(toEncoding.GetBytes(html)), , "2-1")
            'MsgBox(finalEncoding.GetString(htmlBytes), , "2-2")
            'MsgBox(originEncoding.GetString(newBytes), , "2-3")
            'MsgBox(finalEncoding.GetString(newBytes2), , "2-4")
            'MsgBox(finalEncoding.GetString(newBytes3), , "2-5")
            'MsgBox(finalEncoding.GetString(newBytes4), , "2-6")
            'MsgBox(toEncoding.GetString(toEncoding.GetBytes(html)), , "3-1")
            'MsgBox(toEncoding.GetString(htmlBytes), , "3-2")
            'MsgBox(toEncoding.GetString(newBytes), , "3-3")
            'MsgBox(toEncoding.GetString(newBytes2), , "3-4")
            'MsgBox(toEncoding.GetString(newBytes3), , "3-5")
            'MsgBox(toEncoding.GetString(newBytes4), , "3-6")

            Return html2
        End Function

        Private Function getHTMLForView(ByVal frameName As String) As String
            Dim myDoc As mshtml.HTMLDocument = pageForView.Document
            'TODO : Encoding is not always the proper one... tried to change defaultCharset, but is not working.. tried to convert bytes, still not working
            'MsgBox(myDoc.defaultCharset)
            'myDoc.defaultCharset = "windows-1252"
            'MsgBox(myDoc.body.outerHTML, , myDoc.defaultCharset)
            If frameName = "" Then Return convertHTMLEncoding(myDoc.body.outerHTML, myDoc.defaultCharset)

            Dim myFrame As mshtml.HTMLWindow2 = Nothing
            For i As Integer = 0 To myDoc.frames.length - 1
                'myFrame = myDoc.frames.item(i)
                myFrame = CrossFrameIE.GetDocumentFromWindow(myDoc.frames.item(i)).parentWindow
                If myFrame.name <> frameName Then
                    myFrame = Nothing
                Else
                    Exit For
                End If
            Next i

            If myFrame Is Nothing Then Return convertHTMLEncoding(myDoc.body.outerHTML, myDoc.defaultCharset)

            'myFrame.document.defaultCharset = "windows-1252"
            'MsgBox(myFrame.document.body.outerHTML, , myFrame.document.defaultCharset)
            Return convertHTMLEncoding(myFrame.document.body.outerHTML, myDoc.defaultCharset)
        End Function

        Public Function getHTML() As String
            Try

                If _EditorURL <> "" AndAlso _Editing AndAlso isGoodEditPage() AndAlso isPageLoaded Then
                    Return getHTMLForEdition()
                Else
                    Return getHTMLForView("")
                End If
            Catch
                Return ""
            End Try
        End Function

        Public Function getHTML(ByVal frameName As String) As String
            If Editing Then Return ""

            Return getHTMLForView(frameName)
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
        Public Sub insertHtml(ByVal html As String)
            If html Is Nothing Then html = ""
            If Editing = True AndAlso isGoodEditPage() AndAlso isPageLoaded Then
                Try
                    callEditionJavaScript("WebTextPage.insertHTML(""" & html.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "") & """);")
                Catch ex As System.Runtime.InteropServices.COMException
                    If ex.ErrorCode = -2147352319 Then
                        MessageBox.Show("Impossible d'insérer à cet endroit. Veuillez tenter de changer le curseur d'endroit.", "Insertion impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        Throw ex
                    End If
                End Try
            Else
                setHtml(html & getHTML())
            End If
        End Sub

        Protected Sub callEditionJavaScript(ByVal js As String)
            Dim myWin As mshtml.IHTMLWindow2 = pageForEdition.Document.parentWindow
            'REM If myWin.location.href.StartsWith("http") = False Then Exit Sub

            Try
                myWin.execScript(js, "javascript")
            Catch ex As Exception
                Throw New Exception("js:" & js, ex)
            End Try
        End Sub

        Protected Sub callViewJavaScript(ByVal js As String)
            Dim myDoc As mshtml.IHTMLDocument2 = pageForView.Document
            If myDoc.location.href.StartsWith("http") = False Then Exit Sub

            Try
                myDoc.parentWindow.execScript(js, "javascript")
            Catch ex As Exception
                Throw New Exception("js:" & js, ex)
            End Try
        End Sub

        '<System.Diagnostics.DebuggerStepThrough()> _
        Public Sub setHtml(ByVal html As String, Optional ByVal clearUndos As Boolean = False)
            If html Is Nothing Then html = ""
            If _Editing AndAlso isPageLoaded AndAlso isGoodEditPage() Then
                Dim htmlSet As Boolean = False
                Dim tries As Byte = 0
restartHTML:
                Try
                    html = html.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "\n").Replace(vbLf, "").Replace(vbCr, "")
                    callEditionJavaScript("WebTextPage.setHTML(""" & html & """);")
                    htmlSet = True
                    'TODO : Shall reactivate
                    ' Me.clearUndos()
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
                'REM IO.File.WriteAllText("C:\testing-big.html", HTML)

                'Trick to ensure internal links work when created from the internal editor
                If Me.editorURL <> "" Then html = System.Text.RegularExpressions.Regex.Replace(html, Me.editorURL.Replace("\", "\\") & "[^#]*#", "#")

                loadContent(html, pageForView)
            End If
        End Sub

        Private Function encodeSpecialCharacters(ByVal html As String) As String
            Dim sb As New System.Text.StringBuilder()
            Dim charInt As Integer
            Dim replaceChars As Boolean = True
            For Each c As Char In html
                charInt = Asc(c)
                If charInt = 60 Then replaceChars = False
                If charInt = 62 Then replaceChars = True

                If replaceChars AndAlso charInt > 127 Then
                    sb.Append(String.Format("&#{0};", charInt))
                Else
                    sb.Append(c)
                End If
            Next

            Return sb.ToString()
        End Function

        Public Sub clearUndos()
            callEditionJavaScript("WebTextPage.clearUndos();") 'Clear undos
        End Sub

        Public Function hasUndo() As Boolean
            callEditionJavaScript("WebTextPage.hasUndo();")

            Return _hasUndo
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
            If Editing = False OrElse isGoodEditPage() = False OrElse isPageLoaded = False Then Exit Sub

            callEditionJavaScript("WebTextPage.surroundHTML(""" & beforeHTML.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "") & """,""" & afterHTML.Replace("\", "\\").Replace("\\n", "\n").Replace("""", "\""").Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "") & """);")
        End Sub
#End Region
#End Region

#Region "Printing"
        Public Sub printSetup() Implements IPrintable.printOptions
            pageForView.ExecWB(SHDocVw.OLECMDID.OLECMDID_PAGESETUP, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
        End Sub

        Public Sub printPreview() Implements IPrintable.printPreview
            pageForView.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINTPREVIEW, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
        End Sub

        Public Sub print(Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False) Implements IPrintable.print
            If Editing Then
                'Copy current editing content to view and wait it's loaded into before printing
                Dim oldViewDisableHtmlFields As Boolean = _viewDisableHtmlFields
                _viewDisableHtmlFields = True
                Dim curViewContent As String = getHTMLForView("")
                loadContent(getHTMLForEdition(), pageForView)
                _viewDisableHtmlFields = oldViewDisableHtmlFields
                While curViewContent = getHTMLForView("")
                    Application.DoEvents()
                End While
                Threading.Thread.Sleep(500)
                Application.DoEvents()
            End If


            System.Threading.Monitor.Enter(syncLockBetweenLoadingAndPrint)

            _PrintJobSpooled = False
            Try
                'Wait until ready (Correcting error HRESULT : 0x80040100 (DRAGDROP_E_NOTREGISTERED))
                Dim maxLoopWait As Integer = 0
                While pageForView.QueryStatusWB(SHDocVw.OLECMDID.OLECMDID_PRINT) <> SHDocVw.OLECMDF.OLECMDF_SUPPORTED + SHDocVw.OLECMDF.OLECMDF_ENABLED
                    Application.DoEvents()
                    maxLoopWait += 1
                    If maxLoopWait = 10000 Then MessageBox.Show("Impossible d'imprimer. Veuillez réessayer.", "Erreur d'impression") : Exit Sub
                End While

                'Print
                If promptUser Then
                    pageForView.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
                Else
                    pageForView.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER)
                End If
                'Wait for end spooling
                If waitForSpooling = True Then
                    Dim nbPrintLoop As Integer = 0
                    'TODO : Shall second condition be added
                    While _PrintJobSpooled = False ' AndAlso nbPrintLoop < MAX_PRINT_WAIT_LOOP
                        Application.DoEvents()
                        nbPrintLoop += 1
                    End While
                End If
            Catch ex As Exception
                External.current.addErrorLog(ex)
                MessageBox.Show("Impossible d'imprimer. Veuillez réessayer.", "Erreur d'impression")
            Finally
                System.Threading.Monitor.Exit(syncLockBetweenLoadingAndPrint)
            End Try
        End Sub
#End Region

        Private Sub selectWindow()
            If Me.ParentForm.InvokeRequired Then
                Me.ParentForm.Invoke(New invokeSub(AddressOf selectWindow))
                Exit Sub
            Else
                Me.ParentForm.Focus()
            End If

            If Me.InvokeRequired Then
                Me.Invoke(New invokeSub(AddressOf selectWindow))
                Exit Sub
            Else
                Me.Select()
            End If
        End Sub

        Protected Function autoFillFileSelection(ByVal file As String) As Boolean
            ' Find program/dialog handle; 32770 je za dialog
            Dim hWnd As IntPtr
            Dim englishTitle As String = "Choose File to Upload"
            Dim frenchTitle As String = "Choisir un fichier à télécharger"
            Dim frenchTitle2 As String = "Choisir un fichier à charger"
            Dim englishButton As String = "&Open"
            Dim frenchButton As String = "&Ouvrir"
            Dim frenchButton2 As String = "Ou&vrir"

            Dim button As String = englishButton

            Dim windowClass As String = Nothing ' "#32770"
            hWnd = FindWindow(windowClass, englishTitle)
            If hWnd = 0 Then
                hWnd = FindWindow(windowClass, frenchTitle)
                If hWnd = 0 Then hWnd = FindWindow(windowClass, frenchTitle2)
                button = frenchButton
            End If

            If hWnd = 0 Then
                Return False
            End If

            'Ensure window has focus, outerwise, would wait for user to click on window (and potentially choosing another file, NO GOOD !)
            'CType(Me.ParentForm, Object).focus()
            selectWindow()
            SetFocus(hWnd)

            'Write file into dialog and submit it
            Dim hWndEdit As IntPtr
            hWndEdit = apiFindWindowEx(hWnd, IntPtr.Zero, "ComboBoxEx32", "")
            Dim hWndEdit1 As IntPtr
            hWndEdit1 = apiFindWindowEx(hWndEdit, IntPtr.Zero, "ComboBox", "")
            Dim hWndEdit2 As IntPtr
            hWndEdit2 = apiFindWindowEx(hWndEdit1, IntPtr.Zero, "Edit", "")
            Dim sb As New System.Text.StringBuilder(file)
            Dim WM_SETTEXT As Integer = 12 '&HC 'decimalno 12
            SendMessage(hWndEdit2, WM_SETTEXT, 0, sb)
            Dim hWndTextbox As IntPtr = apiFindWindowEx(hWnd, IntPtr.Zero, "Button", button)
            If hWndTextbox = 0 Then hWndTextbox = apiFindWindowEx(hWnd, IntPtr.Zero, "Button", frenchButton2)

            Dim BN_CLICKED As Integer = 245
            SendMessage(hWndTextbox, BN_CLICKED, 0, IntPtr.Zero)

            Return True
        End Function


        Public Sub waitForDoc()
            While pageForView.ReadyState <> SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE AndAlso _IsPageLoaded = False
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
                showingPage(pageForView, contenuHTML, _Editing)
            End If
        End Sub

        Private Sub showingPage(ByVal pageObject As AxSHDocVw.AxWebBrowser, Optional ByVal contenuHTML As String = "", Optional ByVal isEditing As Boolean = False)
            Dim myNull As DBNull = DBNull.Value

            Dim navConstants As Object = myNull
            If _useNavigationCache Then
                navConstants = BrowserNavConstants.navNoHistory
            Else
                navConstants = BrowserNavConstants.navNoHistory Or BrowserNavConstants.navNoReadFromCache Or BrowserNavConstants.navNoWriteToCache
            End If

            If isEditing = True Then
                If _EditorURL = "" Then Exit Sub

                'If ContenuHTML = "" And System.IO.File.Exists(HTMLPageURL) Then
                'MyPage = _EditorURL & "?toolbarstyle=" & _ToolBarStyles & "&height=" & _EditorHeight & "&width=" & _EditorWidth & "&" & NbShowedPage & "&page=" & System.Web.HttpUtility.UrlEncode(HTMLPageURL)
                'Else
                Dim randomFlag As Integer = nbShowedPage + Date.Now.Year + Date.Now.Month + Date.Now.Day + Date.Now.Hour + Date.Now.Minute + Date.Now.Second + Date.Now.Millisecond

                myPage = _EditorURL & "?toolbarstyle=" & _ToolBarStyles & "&height=" & _EditorHeight & "&width=" & _EditorWidth & "&r=" & randomFlag & "&contenu=" & System.Web.HttpUtility.UrlEncode(contenuHTML)
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
            If isPageLoaded = False Then Return False

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
            If Editing Then
                pageForEdition.Select()
                'Dim b As Boolean = PageForEdition.Focused
                pageForEdition.Focus()
                'Dim a As Boolean = PageForEdition.Focused
                'MsgBox(a & ":" & b)
                callEditionJavaScript("WebTextPage.setFocus();")
            Else
                pageForView.Select()
            End If
            'PageForText.Focus()
            'End If

            Return True
        End Function

        ''' <summary>
        ''' Get a bitmap from the current loaded page
        ''' </summary>
        ''' <param name="imgUrl">Image url to locate the IMG tag</param>
        ''' <returns></returns>
        ''' <remarks>Inspired from : http://stackoverflow.com/a/2568019/214898</remarks>
        Public Function getImageFromDocument(ByVal imgUrl As String) As Bitmap
            imgUrl = imgUrl.Replace("&amp;", "&")

            Dim myDoc As mshtml.HTMLDocument = pageForView.Document
            
            Dim bmp As Bitmap = Nothing
            For Each img As mshtml.IHTMLImgElement In myDoc.images
                If img.src = imgUrl Then
                    bmp = getImageFromDocument(img)
                    Exit For
                End If
            Next

            Return bmp
        End Function

        ''' <summary>
        ''' Get a bitmap from the current loaded page
        ''' </summary>
        ''' <param name="img">IMG tag to get image from</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function getImageFromDocument(ByVal img As IHTMLImgElement) As Bitmap
            Dim myDoc As mshtml.HTMLDocument = pageForView.Document
            Dim imgRange As mshtml.IHTMLControlRange = CType(myDoc.body, HTMLBody).createControlRange()

            ClipboardHelper.save()
            imgRange.add(CType(img, IHTMLControlElement))

            imgRange.execCommand("Copy", False, Nothing)

            Dim bmp As Bitmap = Clipboard.GetDataObject().GetData(DataFormats.Bitmap)
            ClipboardHelper.restore()

            Return bmp
        End Function

        Protected Function getParentNode(ByVal node As IHTMLDOMNode, ByVal tagName As String) As IHTMLDOMNode
            tagName = tagName.ToUpper()
            Dim parent As IHTMLDOMNode = Nothing

            While node.parentNode IsNot Nothing
                node = node.parentNode
                If node.nodeName.ToUpper() = tagName Then
                    parent = node
                    Exit While
                End If
            End While

            Return parent
        End Function

        Protected Function isNodeInParent(ByVal node As IHTMLDOMNode, ByVal parent As IHTMLDOMNode) As Boolean
            While node.parentNode IsNot Nothing
                node = node.parentNode
                If node Is parent Then Return True
            End While

            Return False
        End Function

        Protected Function getUrlFromElement(ByVal node As IHTMLElement, ByVal attributeName As String) As String
            Dim doc As HTMLDocument = browserForView.Document
            Dim relativeUrlBase As String = Me.viewLocation.Substring(0, Me.viewLocation.LastIndexOf("/") + 1)
            Dim relativeUrlBaseLength As Integer = relativeUrlBase.Length
            Dim absoluteUrlBase As String = doc.location.protocol & "//" & doc.location.host
            Dim absoluteUrlBaseLength As Integer = absoluteUrlBase.Length

            Dim html As String = node.outerHTML
            Dim url As String = node.getAttribute(attributeName)
            If html.IndexOf(attributeName & "=""" & url & """") <> -1 Then
                Return url
            End If

            If url.StartsWith(relativeUrlBase) Then
                url = url.Substring(relativeUrlBaseLength)
            End If
            If node.outerHTML.IndexOf(attributeName & "=""" & url & """") = -1 Then
                url = node.getAttribute(attributeName).Substring(absoluteUrlBaseLength)
            End If

            Return url
        End Function

#Region "Events subs"
        Private Sub doc_BeforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean) Handles _browserForEdition.BeforeNavigate
            onBeforeNavigate(url, flags, targetFrameName, postData, headers, cancel)
        End Sub

        Private Sub doc2_BeforeNavigate(ByVal url As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef postData As Object, ByVal headers As String, ByRef cancel As Boolean) Handles _browserForView.BeforeNavigate
            onBeforeNavigate(url, flags, targetFrameName, postData, headers, cancel)
        End Sub

        Private Sub doc_NavigateComplete(ByVal url As String) Handles _browserForEdition.NavigateComplete
            onNavigateComplete(url)
        End Sub

        Private Sub _browserForView_DownloadBegin() Handles _browserForView.DownloadBegin
            Dim a As Byte = 0
        End Sub

        Private Sub _browserForView_DownloadComplete() Handles _browserForView.DownloadComplete
            Dim a As Byte = 0
        End Sub

        Private Sub doc2_NavigateComplete(ByVal url As String) Handles _browserForView.NavigateComplete
            onNavigateComplete(url)
        End Sub

        Private Sub webControl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
            If Editing Then
                pageForEdition.Focus()
            Else
                pageForView.Focus()
            End If
        End Sub

        Private Sub webControl_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
            ctrlDown = e.Control
        End Sub

        Private Sub webTextControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            buildEditorContextMenu()

            'Using offline makes WebTextPage not showing on Windows 8 when no internet connection
            'pageForView.Offline = True
            'pageForEdition.Offline = True
            Dim b As Object = pageForEdition.Application
            _browserForEdition = DirectCast(b, SHDocVw.WebBrowser_V1)
            Dim c As Object = pageForView.Application
            _browserForView = DirectCast(c, SHDocVw.WebBrowser_V1)
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
                Dim imgDir As String = Application.StartupPath & addSlash(Application.StartupPath) & "Images\WBT\"
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
            'REM
            Dim ts As TimeSpan = Date.Now.Subtract(_TestingTime)
            'If currentUserName = "Administrateur" Then myMainWin.StatusText = "WebControl edition page loading time (ms) : " & ts.TotalMilliseconds

            'REM Create a bug when Accessing Générateur de rapport. Not really faster with this caching method
            'DownloadPageEditor()

            RaiseEvent pageLoaded()
        End Sub

        Protected Overridable Sub onAddLink(ByRef handled As Boolean)
            RaiseEvent addingLink(Me, handled)
        End Sub

        Protected Overridable Sub onInsertDate(ByVal fieldId As String, ByVal selectedDate As String, ByRef handled As Boolean)
            RaiseEvent insertingDate(Me, fieldId, selectedDate, handled)
        End Sub

        Protected Overridable Sub onAddImage(ByRef handled As Boolean)
            RaiseEvent addingImage(Me, handled)
        End Sub

        Protected Overridable Overloads Sub onTextChanged(ByVal [Text] As String)
            RaiseEvent textChanged([Text])
        End Sub

        Protected Overridable Sub onPasted()
            onTextChanged(getHTML())
        End Sub

        Protected Overridable Sub onCatchJSError(ByVal msg As String, ByVal url As String, ByVal line As Integer, ByVal html As String)
            External.current.addErrorLog(New Exception("JavaScript Error : " & msg & vbCrLf & "URL : " & url & " at line #" & line & vbCrLf & vbCrLf & "HTML :" & vbCrLf & html))
        End Sub

        Protected Overridable Sub onTextPreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
            RaiseEvent textPreviewKeyDown(sender, e)
        End Sub
#End Region

        Private _hasUndo As Boolean = False

        Protected Sub onHasUndo(ByVal hasUndo As Boolean)
            _hasUndo = hasUndo
        End Sub

#Region "Handling Browser Document events"
        Private Interface ICustomDoc
            Sub setUIHandler(ByVal pUIHandler As IDocHostUIHandler)
        End Interface

        Private Sub addDocEvents(ByRef curWindow As Object)
            'REM Handling HTML doc events, do it, but not for internal frames (Should fix it before activating)
            htmlEvent = DirectCast(curWindow.document, mshtml.HTMLDocumentEvents2_Event)

            Dim window As IHTMLWindow2 = CType(curWindow, IHTMLWindow2)

            If window IsNot Nothing Then
                Dim curDoc As HTMLDocument = curWindow.document
                Dim handler As DHTMLEventHandler = New DHTMLEventHandler(curDoc)
                handler.handler = New DHTMLEvent(AddressOf Me.clickEvent)
                curDoc.onclick = handler

                ' add the events in here 
                'CType(curHTMLDoc.document, IHTMLDocument3).attachEvent("onclick", handler) 'New HTMLDocumentEvents2_onclickEventHandler(AddressOf Me.ClickEvent)
                'AddHandler CType(CurHTMLDoc.document, HTMLDocumentEvents2_Event).onclick, Handler.Handler 'HandlerOnClickMenu
                'AddHandler htmlEvent.onclick, AddressOf clickEvent
                'AddHandler htmlEvent.ondblclick, AddressOf DoubleClickEvent
                'AddHandler htmlEvent.onfocusin, AddressOf GotFocusEvent
                'AddHandler htmlEvent.onfocusout, AddressOf LostFocusEvent
                'AddHandler htmlEvent.onkeydown, AddressOf KeyDownEvent
                'AddHandler htmlEvent.onkeypress, AddressOf KeyPressEvent
                'AddHandler htmlEvent.onkeyup, AddressOf KeyUpEvent
                'AddHandler htmlEvent.onmousedown, AddressOf MouseDownEvent
                'AddHandler htmlEvent.onmousemove, AddressOf mouseMoveEvent
                'AddHandler htmlEvent.onmouseout, AddressOf mouseOutEvent
                'AddHandler htmlEvent.onmouseover, AddressOf mouseOverEvent
                'AddHandler htmlEvent.onmouseup, AddressOf MouseUpEvent
                'AddHandler htmlEvent.onmousewheel, AddressOf MouseWheelEvent
            End If

            Dim i As Integer
            For i = 0 To window.frames.length - 1
                Dim curFrame As IHTMLWindow2
                curFrame = window.frames.item(i)
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
            'REM Handling HTML doc events, do it, but not for internal frames (Should fix it before activating)
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

        Public Delegate Function DHTMLEvent(ByVal e As IHTMLEventObj) As Boolean

        <Runtime.InteropServices.ComVisible(True)> _
        Public Class DHTMLEventHandler
            Public handler As DHTMLEvent
            Private document As HTMLDocument

            Public Sub New(ByVal doc As HTMLDocument)
                Me.document = doc
            End Sub

            <Runtime.InteropServices.DispId(0)> _
            Public Sub [Call]()
                handler(Me.document.parentWindow.event)
            End Sub
        End Class

        Private Sub pageForEdition_BeforeNavigate2(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event) Handles pageForEdition.BeforeNavigate2
            'RemoveDocEvents(PageForEdition.Document.parentWindow)
        End Sub

        Private Sub pageForEdition_DownloadBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles pageForEdition.DownloadBegin

        End Sub

        Private Sub pageForText_BeforeNavigate2(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event) Handles pageForView.BeforeNavigate2

        End Sub

        Private Sub pageForText_CommandStateChange(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent) Handles pageForView.CommandStateChange
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


        <DllImport("wininet.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Private Shared Function InternetGetCookieEx(ByVal pchURL As String, ByVal pchCookieName As String, ByVal pchCookieData As System.Text.StringBuilder, ByRef pcchCookieData As UInteger, ByVal dwFlags As Integer, ByVal lpReserved As IntPtr) As Boolean
        End Function
        Const INTERNET_COOKIE_HTTPONLY As Integer = &H2000

        ''' <summary>
        ''' Return the all cookies of the current page
        ''' </summary>
        ''' <param name="uri"></param>
        ''' <returns></returns>
        ''' <remarks>Taken from : https://www.codeproject.com/tips/659004/download-of-file-with-open-save-dialog-box</remarks>
        Protected Function getGlobalCookies(ByVal uri As String) As String
            Dim uiDataSize As UInteger = 2048
            Dim sbCookieData As New System.Text.StringBuilder(CInt(uiDataSize))
            If InternetGetCookieEx(uri, Nothing, sbCookieData, uiDataSize, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero) AndAlso sbCookieData.Length > 0 Then
                Return sbCookieData.ToString().Replace(";", ",")
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Download file using the same cookie as current page (so potentially credentials are preserved)
        ''' </summary>
        ''' <param name="url"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function downloadFile(ByVal url As String) As Byte()
            Dim myWebClient As New Net.WebClient()
            myWebClient.Headers.Add(Net.HttpRequestHeader.Cookie, Me.getGlobalCookies(Me.viewLocation))
            myWebClient.Headers.Add(Net.HttpRequestHeader.Referer, Me.viewLocation)
            Dim fileBytes() As Byte = myWebClient.DownloadData(url)
            myWebClient.Dispose()

            Return fileBytes
        End Function


        Private Sub pages_DocumentComplete(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent) Handles pageForEdition.DocumentComplete, pageForView.DocumentComplete
            If sender.ReadyState <> SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE Then Exit Sub

            RaiseEvent DocumentComplete(sender, e)

            Dim curBrowser As SHDocVw.IWebBrowser2 = e.pDisp
            Dim curDoc As HTMLDocument = curBrowser.Document
            'FIXME: Breaking mouse wheel
            addDocEvents(curDoc.parentWindow)

            'REM 'Ajoute le gestionnaire d'erreur
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

            'REM Essaies pour pouvoir cliquer sur un lien en mode édition
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
            'Dim CurBody As String = CType(curDoc.Document, IHTMLDocument2).body.innerHTML
            'If CurBody Is Nothing Then CurBody = ""

            'addDocEvents(CType(CType(sender, AxSHDocVw.AxWebBrowser).Document, IHTMLDocument2).parentWindow)

            '------------ ONLY GOOD LINE
            'addDocEvents(curDoc.Document.parentWindow)
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

            MyBase.OnMouseClick(New MouseEventArgs(e.button, 1, e.clientX, e.clientY, 0))
        End Sub

        Public Sub editorClick(ByVal url As String)
            If _ActivateLinksOnEdit And ctrlDown Then
                Me.onBeforeNavigate(url, 0, "", New Object, "", False)
                ctrlDown = False
            End If
        End Sub

        Private Function launchBase64Link(ByVal e As IHTMLEventObj) As Boolean
            Dim element As IHTMLElement = e.srcElement
            If Not TypeOf element Is IHTMLAnchorElement Then
                element = getParentNode(element, "A")
            End If
            If element Is Nothing Then Return False

            Dim anchor As IHTMLAnchorElement = element
            If anchor.href.StartsWith("data") Then
                Dim fileName As String = "fichier.txt"
                If element.getAttribute("download") IsNot DBNull.Value Then fileName = element.getAttribute("download")
                Dim fileExtension As String = fileName.Substring(fileName.LastIndexOf("."))

                Dim node As IHTMLDOMNode = element
                If node.childNodes.length = 1 Then
                    MessageBox.Show("Le lien en base64 doit contenir un sous élément qui contient la chaîne en base 64", "Élément manquant", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Dim base64 As String = node.childNodes(0).innerHTML

                    Dim filePath As String = System.IO.Path.GetTempFileName() & fileExtension
                    IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64))

                    launchAProccess(filePath, , , , , , , True)
                End If
                Return True
            End If

            Return False
        End Function

        Private Function clickEvent(ByVal e As IHTMLEventObj) As Boolean
            launchBase64Link(e)

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
            Implements IWebTextInteract, IDisposable

            Private myOwner As WebControl
            Private myTextQueue As New ArrayList()

            Public Sub New(ByVal ownerForm As WebControl)
                myOwner = ownerForm
            End Sub

#Region "Functions used by the HTML page"

            Public Sub catchError(ByVal msg As String, ByVal url As String, ByVal line As Integer, ByVal html As String) Implements IWebTextInteract.catchError
                myOwner.onCatchJSError(msg, url, line, html)
            End Sub

            Public Sub insertDate(ByVal fieldId As String, ByVal selectedDate As String) Implements IWebTextInteract.insertDate
                myOwner.onInsertDate(fieldId, selectedDate, False)
            End Sub

            Public Sub pasteHTMLFromClipboard() Implements IWebTextInteract.pasteHTMLFromClipboard
                myOwner.insertHtml(ClipboardHelper.getHTMLFromClipboard())
                myOwner.onPasted()
            End Sub

            Public Sub sendTextTo(ByVal [Text] As String, ByVal waitingNumber As Integer) Implements IWebTextInteract.sendTextTo
                CType(myTextQueue(waitingNumber), WaitingObject).updateWaitingData([Text])
                myOwner.stopWaitForHTML()
            End Sub

            Public Sub sendPosTo(ByVal pos As Integer, ByVal waitingNumber As Integer) Implements IWebTextInteract.sendPosTo
                CType(myTextQueue(waitingNumber), WaitingObject).updateWaitingData(pos)
                myOwner.stopWaitForPos()
            End Sub

            Public Sub savePos(ByVal pos As String, ByVal waitingNumber As Integer) Implements IWebTextInteract.savePos
                CType(myTextQueue(waitingNumber), WaitingObject).updateWaitingData(pos)
                myOwner.stopWaitForPos()
            End Sub

            Public Sub hasUndo(ByVal hasUndo As Boolean)
                myOwner.onHasUndo(hasUndo)
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

            Public Sub pasted() Implements IWebTextInteract.pasted
                myOwner.onPasted()
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

            Private disposedValue As Boolean = False        ' Pour détecter les appels redondants

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing AndAlso myOwner IsNot Nothing AndAlso myOwner.IsDisposed = False Then
                        myOwner.Dispose()
                        myOwner = Nothing
                    End If

                    If myTextQueue IsNot Nothing Then
                        myTextQueue.Clear()
                        myTextQueue = Nothing
                    End If
                End If
                Me.disposedValue = True
            End Sub

#Region " IDisposable Support "
            ' Ce code a été ajouté par Visual Basic pour permettre l'implémentation correcte du modèle pouvant être supprimé.
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Ne modifiez pas ce code. Ajoutez du code de nettoyage dans Dispose(ByVal disposing As Boolean) ci-dessus.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

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

        Private Sub pageForView_NavigateError(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_NavigateErrorEvent) Handles pageForView.NavigateError

        End Sub

        Private Sub pageForView_NewWindow2(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_NewWindow2Event) Handles pageForView.NewWindow2
            e.cancel = Not allowPopupWindows
        End Sub

        Private Sub pageForText_PrintTemplateInstantiation(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PrintTemplateInstantiationEvent) Handles pageForView.PrintTemplateInstantiation
            Console.WriteLine("WC-PrintTemplateInstantiation:" & Me.FindForm.Text)
        End Sub

        Private Sub pageForText_PrintTemplateTeardown(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PrintTemplateTeardownEvent) Handles pageForView.PrintTemplateTeardown
            _PrintJobSpooled = True

            Console.WriteLine("WC-PrintTemplateTeardown:" & Me.FindForm.Text)
        End Sub
    End Class


End Namespace