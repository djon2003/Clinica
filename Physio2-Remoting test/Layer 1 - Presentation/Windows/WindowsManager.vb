Friend Class WindowsManager
    Inherits ItemableManagerBase(Of WindowsManager, SingleWindow)

    Private myWindows As New Generic.Dictionary(Of String, SingleWindow)
    Private Shared windowsAdded As Integer = 0
    Private _Selected As SingleWindow
    Private _mainWindow As Form
    Private myWindowsUpdateDispatcher As WindowsUpdateDispatcher
    Private Shared _isCreated As Boolean = False

    Public Event windowsMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event windowsMdiChildActivate(ByVal sender As Object, ByVal e As EventArgs)
    Public Event windowsMouseEnter(ByVal sender As Object, ByVal e As EventArgs)

#Region "Propriétés"
    Public Property mainWindow() As Form
        Get
            Return _mainWindow
        End Get
        Set(ByVal value As Form)
            _mainWindow = value
        End Set
    End Property

    Public ReadOnly Property addedCount() As Integer
        Get
            Return windowsAdded
        End Get
    End Property

    Public Function selected() As SingleWindow
        Return _Selected
    End Function

    Public Sub selected(ByRef value As SingleWindow)
        If value Is Nothing Then Exit Sub
        _Selected = value
        Dim n As Integer = myMainWin.formOuvertes.findValue(value)
        'If n = -1 Then n = MyMainWin.FormOuvertes.FindString(value.Text & vbCrLf, , True)
        myMainWin.formOuvertes.selected = n
        value.bringToFront()
    End Sub

    Public Shared ReadOnly Property isCreated() As Boolean
        Get
            Return _isCreated
        End Get
    End Property
#End Region

    Protected Sub New()
        MyBase.New()
        myWindowsUpdateDispatcher = New WindowsUpdateDispatcher(_items, Me.changingItemablesLock)
    End Sub

    Public Shared Sub create(ByVal mainWindow As Form)
        ManagerBase(Of WindowsManager).createInstance()
        _isCreated = True
        getInstance()._mainWindow = mainWindow
    End Sub

    Public Overloads Shared Function getInstance() As WindowsManager
        If _isCreated = False Then Throw New Exception("Shall execute create method first")

        Return ManagerBase(Of WindowsManager).getInstance()
    End Function

    Public Overrides Function addItemable(ByVal newWindow As SingleWindow) As String
        If newWindow.MdiParent Is Nothing OrElse newWindow.MdiParent.Equals(_mainWindow) = False Then Exit Function

        windowsAdded += 1

        newWindow.number = windowsAdded

        If containsWindow(newWindow.Text) Then removeItemable(newWindow.Text)

        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        myWindows.Add(newWindow.Text, newWindow)
        Dim returning As String = MyBase.addItemable(newWindow)
        changingItemablesLock.ReleaseWriterLock()

        If myMainWin Is Nothing Then Exit Function
        AddHandler newWindow.GlobalMouseMove, AddressOf windows_MouseMove
        AddHandler newWindow.GlobalMouseEnter, AddressOf windows_MouseEnter
        AddHandler newWindow.MdiChildActivate, AddressOf windows_MdiChildActivate

        addToWindowsListControl(newWindow)

        selected(newWindow)

        Return returning
    End Function

    Private Function containsWindow(ByVal windowTitle As String) As Boolean
        Dim containingWindow As Boolean = False
        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        containingWindow = myWindows.ContainsKey(windowTitle)
        changingItemablesLock.ReleaseReaderLock()

        Return containingWindow
    End Function

    Private Sub addToWindowsListControl(ByVal newWindow As SingleWindow)
        With myMainWin.formOuvertes
            Dim showExtraInfoPref As Boolean = PreferencesManager.getUserPreferences()("ShowExtraInfosInWindowsListBox") <> "" AndAlso PreferencesManager.getUserPreferences()("ShowExtraInfosInWindowsListBox") = True
            Dim caption As String = newWindow.Text & If(showExtraInfoPref = False OrElse newWindow.textExtra.Trim = "", "", vbCrLf & newWindow.textExtra)
            .add(caption, newWindow)
            Dim n As Integer = .findStringExact(caption)
            CType(.items(n), Controls.ListItem).icon = DrawingManager.imageToIcon(DrawingManager.resizeImage(newWindow.Icon.ToBitmap, 16, 16))
            .ItemToolTipText(n) = caption
            .items(n).IconType = Controls.Icons.IconType.Icon
            .items(n).IconsPosition = Controls.Icons.IconPositions.BeforeText
        End With
    End Sub

    Public Function findWindowsByName(ByVal windowName As String) As Generic.List(Of SingleWindow)
        Dim mySingleWindows As New Generic.List(Of SingleWindow)

        For Each CurWindow As SingleWindow In getItemables()
            If CurWindow.Name.ToLower = windowName.ToLower Then mySingleWindows.Add(CurWindow)
        Next

        Return mySingleWindows
    End Function

    Public Overloads Function getItemable(ByVal windowTitle As String) As SingleWindow
        If containsWindow(windowTitle) = False Then Return Nothing

        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        Dim myWindow As SingleWindow = myWindows(windowTitle)
        changingItemablesLock.ReleaseReaderLock()

        Return myWindow
    End Function

    Public Overloads Sub removeItemable(ByVal windowTitle As String)
        If myMainWin Is Nothing Then Exit Sub
        Dim curWindow As SingleWindow = getItemable(windowTitle)

        If myMainWin.formOuvertes.findValue(curWindow) = -1 Then Exit Sub

        removeItemable(curWindow.noItemable)
    End Sub

    Public Overrides Sub removeItemable(ByVal noItem As Integer)
        Dim curWindow As SingleWindow = getItemable(noItem)
        Try
            myMainWin.mdiClosed(curWindow)
            RemoveHandler curWindow.GlobalMouseMove, AddressOf windows_MouseMove
            RemoveHandler curWindow.GlobalMouseEnter, AddressOf windows_MouseEnter
            RemoveHandler curWindow.MdiChildActivate, AddressOf windows_MdiChildActivate
        Catch ex As Exception
            ex = ex
        End Try

        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        myWindows.Remove(curWindow.Text)
        MyBase.removeItemable(noItem)
        changingItemablesLock.ReleaseWriterLock()
    End Sub

    Public Overrides Sub clear()
        If Not myMainWin Is Nothing AndAlso Not myMainWin.formOuvertes Is Nothing Then
            myMainWin.formOuvertes.items.Clear()
        End If

        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        myWindows.Clear()
        MyBase.clear()
        changingItemablesLock.ReleaseWriterLock()
    End Sub

    Public Sub updateWindowText(ByVal oldWindowTitle As String, ByVal newWindowTitle As String)
        With myMainWin.formOuvertes
            Dim myWindow As SingleWindow = getItemable(oldWindowTitle)
            If myWindow Is Nothing Then Exit Sub

            Dim caption As String = newWindowTitle & If(myWindow.textExtra.Trim = "", "", vbCrLf & myWindow.textExtra)
            Dim oldCaption As String = oldWindowTitle & If(myWindow.textExtra.Trim = "", "", vbCrLf & myWindow.textExtra)
            .remove(.findValue(myWindow))

            changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
            myWindows.Remove(oldWindowTitle)
            myWindows.Add(newWindowTitle, myWindow)
            changingItemablesLock.ReleaseWriterLock()
            myWindow.Text = newWindowTitle

            addToWindowsListControl(myWindow)

            .selected = .findValue(myWindow)
        End With
    End Sub

    Public Function saveAllWindows() As Boolean
        Dim curWindow As SingleWindow
        For Each curWindow In getItemables()
            If curWindow.saveWindow().isCancelled Then Return False
        Next

        Return True
    End Function

    Public Shared Function countWindowHandles() As Integer
        Dim i As Integer
        Dim eW As New vbAccelerator.Components.Win32.EnumWindows
        eW.getWindows()
        For i = 0 To eW.items.Count - 1
            Try
                If eW.items(i).text.ToUpper.StartsWith("CLINICA") Then
                    Dim myPointer As IntPtr = eW.items(i).handle
                    Dim handlesCount As New vbAccelerator.Components.Win32.EnumWindows
                    handlesCount.getWindows(myPointer)
                    If handlesCount.items.Count > 0 Then
                        Return handlesCount.items.Count
                    End If
                End If
            Catch
            End Try
        Next

        Return 0
    End Function

#Region "Events globaux des fenêtres"
    Private Sub windows_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent windowsMouseMove(sender, e)
    End Sub

    Private Sub windows_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent windowsMouseEnter(sender, e)
    End Sub

    Private Sub windows_MdiChildActivate(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent windowsMdiChildActivate(sender, e)
    End Sub
#End Region
End Class
