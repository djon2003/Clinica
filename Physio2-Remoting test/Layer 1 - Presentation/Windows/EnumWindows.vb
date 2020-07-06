Namespace vbAccelerator.Components.Win32

    ''' <summary>
    ''' Window Style Flags
    ''' </summary>
    <Flags()> _
    Public Enum windowStyleFlags As Integer
        WS_OVERLAPPED = &H0
        WS_POPUP = &H80000000
        WS_CHILD = &H40000000
        WS_MINIMIZE = &H20000000
        WS_VISIBLE = &H10000000
        WS_DISABLED = &H8000000
        WS_CLIPSIBLINGS = &H4000000
        WS_CLIPCHILDREN = &H2000000
        WS_MAXIMIZE = &H1000000
        WS_BORDER = &H800000
        WS_DLGFRAME = &H400000
        WS_VSCROLL = &H200000
        WS_HSCROLL = &H100000
        WS_SYSMENU = &H80000
        WS_THICKFRAME = &H40000
        WS_GROUP = &H20000
        WS_TABSTOP = &H10000
        WS_MINIMIZEBOX = &H20000
        WS_MAXIMIZEBOX = &H10000
    End Enum

    ''' <summary>
    ''' Extended Windows Style flags
    ''' </summary>
    <Flags()> _
    Public Enum extendedWindowStyleFlags As Integer
        WS_EX_DLGMODALFRAME = &H1
        WS_EX_NOPARENTNOTIFY = &H4
        WS_EX_TOPMOST = &H8
        WS_EX_ACCEPTFILES = &H10
        WS_EX_TRANSPARENT = &H20
        WS_EX_MDICHILD = &H40
        WS_EX_TOOLWINDOW = &H80
        WS_EX_WINDOWEDGE = &H100
        WS_EX_CLIENTEDGE = &H200
        WS_EX_CONTEXTHELP = &H400

        WS_EX_RIGHT = &H1000
        WS_EX_LEFT = &H0
        WS_EX_RTLREADING = &H2000
        WS_EX_LTRREADING = &H0
        WS_EX_LEFTSCROLLBAR = &H4000
        WS_EX_RIGHTSCROLLBAR = &H0

        WS_EX_CONTROLPARENT = &H10000
        WS_EX_STATICEDGE = &H20000
        WS_EX_APPWINDOW = &H40000

        WS_EX_LAYERED = &H80000

        WS_EX_NOINHERITLAYOUT = &H100000    ' Disable inheritence of mirroring by children
        WS_EX_LAYOUTRTL = &H400000          ' Right to left mirroring

        WS_EX_COMPOSITED = &H2000000
        WS_EX_NOACTIVATE = &H8000000
    End Enum


#Region "EnumWindows"
    ''' <summary>
    ''' EnumWindows wrapper for .NET
    ''' </summary>
    Public Class EnumWindows

#Region "Delegates"
        Private Delegate Function enumWindowsProc(ByVal hwnd As IntPtr, ByVal lParam As Integer) As Integer
#End Region

#Region "UnManagedMethods"
        Private Class UnManagedMethods

            Public Declare Function EnumWindows Lib "user32" ( _
                ByVal lpEnumFunc As enumWindowsProc, _
                ByVal lParam As Integer _
                ) As Integer

            Public Declare Function EnumChildWindows Lib "user32" ( _
                ByVal hWndParent As IntPtr, _
                ByVal lpEnumFunc As enumWindowsProc, _
                ByVal lParam As Integer _
            ) As Integer

        End Class
#End Region

#Region "Member Variables"
        Private m_items As EnumWindowsCollection = Nothing
#End Region

        ''' <summary>
        ''' Returns the collection of windows returned by
        ''' GetWindows
        ''' </summary>
        Public ReadOnly Property items() As EnumWindowsCollection
            Get
                Return m_items
            End Get
        End Property

        ''' <summary>
        ''' Gets all top level windows on the system.
        ''' </summary>
        Public Sub getWindows()
            m_items = New EnumWindowsCollection()
            UnManagedMethods.EnumWindows( _
             AddressOf Me.windowEnum, _
             0)
        End Sub

        ''' <summary>
        ''' Gets all child windows of the specified window
        ''' </summary>
        ''' <param name="hWndParent">Window Handle to get children for</param>
        Public Sub getWindows( _
             ByVal hWndParent As IntPtr _
             )
            m_items = New EnumWindowsCollection()
            UnManagedMethods.EnumChildWindows( _
             hWndParent, _
             AddressOf Me.windowEnum, _
             0)
        End Sub

#Region "EnumWindows callback"
        ''' <summary>
        ''' The enum Windows callback.
        ''' </summary>
        ''' <param name="hWnd">Window Handle</param>
        ''' <param name="lParam">Application defined value</param>
        ''' <returns>1 to continue enumeration, 0 to stop</returns>
        Private Function windowEnum( _
            ByVal hWnd As IntPtr, _
            ByVal lParam As Integer _
            ) As Integer

            If (Me.onWindowEnum(hWnd)) Then
                Return 1
            Else
                Return 0
            End If
        End Function
#End Region

        ''' <summary>
        ''' Called whenever a new window is about to be added
        ''' by the Window enumeration called from GetWindows.
        ''' If overriding this function, return true to continue
        ''' enumeration or false to stop.  If you do not call
        ''' the base implementation the Items collection will
        ''' be empty.
        ''' </summary>
        ''' <param name="hWnd">Window handle to add</param>
        ''' <returns>True to continue enumeration, False to stop</returns>
        Protected Overridable Function onWindowEnum( _
             ByVal hWnd As IntPtr _
            ) As Boolean
            m_items.add(hWnd)
            Return True
        End Function

#Region "Constructor, Dispose"
        Public Sub New()
            ' nothing to do
        End Sub
#End Region

    End Class
#End Region


#Region "EnumWindowsCollection"
    ''' <summary>
    ''' Holds a collection of Windows returned by GetWindows.
    ''' </summary>
    Public Class EnumWindowsCollection
        Inherits ReadOnlyCollectionBase

        ''' <summary>
        ''' Add a new Window to the collection.  Intended for
        ''' internal use by EnumWindows only.
        ''' </summary>
        ''' <param name="hWnd">Window handle to add</param>
        Public Sub add(ByVal hWnd As IntPtr)
            Dim item As EnumWindowsItem = New EnumWindowsItem(hWnd)
            MyBase.InnerList.Add(item)
        End Sub

        ''' <summary>
        ''' Gets the Window at the specified index
        ''' </summary>
        Default Public ReadOnly Property Item(ByVal index As Integer) As EnumWindowsItem
            Get
                Return Me.InnerList(index)
            End Get
        End Property

        ''' <summary>
        ''' Constructs a new EnumWindowsCollection object.
        ''' </summary>
        Public Sub New()
            ' nothing to do
        End Sub
    End Class
#End Region

#Region "EnumWindowsItem"
    ''' <summary>
    ''' Provides details about a Window returned by the 
    ''' enumeration
    ''' </summary>
    Public Class EnumWindowsItem

#Region "Structures"
        <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack:=4)> _
        Private Structure RECT
            Public left As Integer
            Public top As Integer
            Public right As Integer
            Public bottom As Integer
        End Structure

        <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack:=4)> _
         Private Structure FLASHWINFO
            Public cbSize As Integer
            Public hwnd As IntPtr
            Public dwFlags As Integer
            Public uCount As Integer
            Public dwTimeout As Integer
        End Structure
#End Region

#Region "UnManagedMethods"
        Private Class UnManagedMethods


            Public Declare Function IsWindowVisible Lib "user32" ( _
                ByVal hWnd As IntPtr) As Integer
            Public Declare Auto Function GetWindowText Lib "user32" ( _
                ByVal hWnd As IntPtr, _
                ByVal lpString As System.Text.StringBuilder, _
                ByVal cch As Integer) As Integer
            Public Declare Auto Function GetWindowTextLength Lib "user32" ( _
                 ByVal hWnd As IntPtr) As Integer
            Public Declare Function BringWindowToTop Lib "user32" (ByVal hWnd As IntPtr) As Integer
            Public Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer
            Public Declare Function IsIconic Lib "user32" (ByVal hWnd As IntPtr) As Integer
            Public Declare Function IsZoomed Lib "user32" (ByVal hwnd As IntPtr) As Integer
            Public Declare Auto Function GetClassName Lib "user32" ( _
                ByVal hWnd As IntPtr, _
                ByVal lpClassName As System.Text.StringBuilder, _
                ByVal nMaxCount As Integer _
             ) As Integer
            Public Declare Function FlashWindow Lib "user32" ( _
                ByVal hWnd As IntPtr, _
                ByRef pwfi As FLASHWINFO) As Integer
            Public Declare Function GetWindowRect Lib "user32" ( _
                ByVal hWnd As IntPtr, _
                ByRef lpRect As RECT) As Integer
            Public Declare Auto Function SendMessage Lib "user32" ( _
                ByVal hWnd As IntPtr, _
                ByVal wMsg As Integer, _
                ByVal wParam As IntPtr, _
                ByVal lParam As IntPtr _
             ) As Integer
            Public Declare Auto Function GetWindowLong Lib "user32" ( _
             ByVal hwnd As IntPtr, _
             ByVal nIndex As Integer) As Integer

            Public Const wm_command As Integer = &H111
            Public Const wm_syscommand As Integer = &H112

            Public Const sc_restore As Integer = &HF120&
            Public Const sc_close As Integer = &HF060&
            Public Const sc_maximize As Integer = &HF030&
            Public Const sc_minimize As Integer = &HF020&

            Public Const gwl_style As Integer = (-16)
            Public Const gwl_exstyle As Integer = (-20)

            ''' <summary>
            ''' Stop flashing. The system restores the window to its original state.
            ''' </summary>
            Public Const flashw_stop As Integer = 0
            ''' <summary>
            ''' Flash the window caption. 
            ''' </summary>
            Public Const flashw_caption As Integer = &H1
            ''' <summary>
            ''' Flash the taskbar button.
            ''' </summary>
            Public Const flashw_tray As Integer = &H2
            ''' <summary>
            ''' Flash both the window caption and taskbar button.
            ''' </summary>
            Public Const flashw_all As Integer = (flashw_caption Or flashw_tray)
            ''' <summary>
            ''' Flash continuously, until the FLASHW_STOP flag is set.
            ''' </summary>
            Public Const flashw_timer As Integer = &H4
            ''' <summary>
            ''' Flash continuously until the window comes to the foreground. 
            ''' </summary>
            Public Const flashw_timernofg As Integer = &HC
        End Class
#End Region

        ''' <summary>
        ''' The window handle.
        ''' </summary>
        Private hWnd As IntPtr = IntPtr.Zero

        ''' <summary>
        ''' To allow items to be compared, the hash code
        ''' is set to the Window handle, so two EnumWindowsItem
        ''' objects for the same Window will be equal.
        ''' </summary>
        ''' <returns>The Window Handle for this window</returns>
        Public Overrides Function getHashCode() As System.Int32
            Return Me.hWnd.ToInt32()
        End Function

        ''' <summary>
        ''' Gets the window's handle
        ''' </summary>
        Public ReadOnly Property handle() As IntPtr
            Get
                Return Me.hWnd
            End Get
        End Property

        ''' <summary>
        ''' Gets the window's title (caption)
        ''' </summary>
        Public ReadOnly Property text() As String
            Get
                Dim title As System.Text.StringBuilder = New System.Text.StringBuilder(260, 260)
                UnManagedMethods.GetWindowText(Me.hWnd, title, title.Capacity)
                Return title.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Gets the window's class name.
        ''' </summary>
        Public ReadOnly Property className() As String
            Get
                Dim theClassName As System.Text.StringBuilder = New System.Text.StringBuilder(260, 260)
                UnManagedMethods.GetClassName(Me.hWnd, theClassName, theClassName.Capacity)
                Return theClassName.ToString()
            End Get
        End Property

        ''' <summary>
        ''' Gets/Sets whether the window is iconic (mimimised) or not.
        ''' </summary>
        Public Property iconic() As Boolean
            Get
                Return IIf(UnManagedMethods.IsIconic(Me.hWnd) = 0, False, True)
            End Get
            Set(ByVal Value As Boolean)
                UnManagedMethods.SendMessage( _
                 Me.hWnd, _
                 UnManagedMethods.wm_syscommand, _
                 New IntPtr(UnManagedMethods.sc_minimize), _
                 IntPtr.Zero)
            End Set
        End Property

        ''' <summary>
        ''' Gets/Sets whether the window is maximised or not.
        ''' </summary>
        Public Property maximised() As Boolean
            Get
                Return IIf(UnManagedMethods.IsZoomed(Me.hWnd) = 0, False, True)
            End Get
            Set(ByVal Value As Boolean)
                UnManagedMethods.SendMessage( _
                 Me.hWnd, _
                 UnManagedMethods.wm_syscommand, _
                 New IntPtr(UnManagedMethods.sc_maximize), _
                 IntPtr.Zero)
            End Set
        End Property

        ''' <summary>
        ''' Gets whether the window is visible.
        ''' </summary>
        Public ReadOnly Property visible() As Boolean
            Get
                Return IIf(UnManagedMethods.IsWindowVisible(Me.hWnd) = 0, False, True)
            End Get
        End Property

        ''' <summary>
        ''' Gets the bounding rectangle of the window
        ''' </summary>
        Public ReadOnly Property rectangle() As System.Drawing.Rectangle
            Get
                Dim rc As RECT = New RECT()
                UnManagedMethods.GetWindowRect( _
                    Me.hWnd, _
                     rc)
                Dim rcRet As System.Drawing.Rectangle = New System.Drawing.Rectangle( _
                   rc.left, rc.top, _
                   rc.right - rc.left, rc.bottom - rc.top)
                Return rcRet
            End Get
        End Property

        ''' <summary>
        ''' Gets the location of the window relative to the screen.
        ''' </summary>
        Public ReadOnly Property location() As System.Drawing.Point
            Get
                Dim rc As Rectangle = Me.rectangle
                Dim pt As System.Drawing.Point = New System.Drawing.Point( _
                    rc.Left, _
                    rc.Top)
                Return pt
            End Get
        End Property

        ''' <summary>
        ''' Gets the size of the window.
        ''' </summary>
        Public ReadOnly Property size() As System.Drawing.Size
            Get
                Dim rc As System.Drawing.Rectangle = Me.rectangle
                Dim sz As System.Drawing.Size = New System.Drawing.Size( _
                 rc.Right - rc.Left, _
                 rc.Bottom - rc.Top)
                Return sz
            End Get
        End Property

        ''' <summary>
        ''' Restores and Brings the window to the front, 
        ''' assuming it is a visible application window.
        ''' </summary>
        Public Sub restore()
            If (iconic) Then
                UnManagedMethods.SendMessage( _
                 Me.hWnd, _
                 UnManagedMethods.wm_syscommand, _
                 New IntPtr(UnManagedMethods.sc_restore), _
                 IntPtr.Zero)
            End If
            UnManagedMethods.BringWindowToTop(Me.hWnd)
            UnManagedMethods.SetForegroundWindow(Me.hWnd)
        End Sub

        Public ReadOnly Property windowStyle() As windowStyleFlags
            Get
                Return UnManagedMethods.GetWindowLong( _
                 Me.hWnd, UnManagedMethods.gwl_style)
            End Get
        End Property

        Public ReadOnly Property extendedWindowStyle() As extendedWindowStyleFlags
            Get
                Return UnManagedMethods.GetWindowLong( _
                 Me.hWnd, UnManagedMethods.gwl_exstyle)
            End Get
        End Property

        ''' <summary>
        '''  Constructs a new instance of this class for
        '''  the specified Window Handle.
        ''' </summary>
        ''' <param name="hWnd">The Window Handle</param>
        Public Sub New(ByVal hWnd As IntPtr)
            Me.hWnd = hWnd
        End Sub
    End Class
#End Region

End Namespace
