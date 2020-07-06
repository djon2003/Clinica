Friend Class SingleWindow
    Inherits Form
    Implements IComparer, IComparable, IDataConsumer(Of DataInternalUpdate), IItemable

    Private _Number As String = 0
    Private mySavingMethod As [Delegate]
    Private _Loaded As Boolean = False

    Public Sub New()
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen

        Try
            Me.Icon = DrawingManager.getInstance().getIcon("Clinica.ico")
        Catch ex As Exception
            'Ensure designer is functionnal
        End Try
    End Sub

    Public Event GlobalMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event GlobalMouseEnter(ByVal sender As Object, ByVal e As EventArgs)
    Public Event noItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer) Implements IItemable.noItemableChanged

    Protected Overridable Sub onNoItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer)
        RaiseEvent noItemableChanged(oldNoItemable, newNoItemable)
    End Sub

#Region "Propriétés"
    Public Overridable ReadOnly Property textExtra() As String
        Get
            Return ""
        End Get
    End Property

    Public Property loaded() As Boolean
        Get
            Return _Loaded
        End Get
        Set(ByVal value As Boolean)
            _Loaded = value
        End Set
    End Property

    Public Property number() As Integer
        Get
            Return _Number
        End Get
        Set(ByVal value As Integer)
            _Number = value
        End Set
    End Property

    Public Overrides Property [Text]() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            If Me.loaded AndAlso MyBase.Text <> value AndAlso Me.MdiParent IsNot Nothing Then WindowsManager.getInstance.updateWindowText(MyBase.Text, value)
            MyBase.Text = value
        End Set
    End Property

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property noItemable() As Integer Implements IItemable.noItemable
        Get
            Return _Number
        End Get
    End Property
#End Region

#Region "Own events"
    Private Sub SingleWindow_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Ensure that window is not maximised when closed so that the size saved is not the one maximized
        If Me.WindowState = FormWindowState.Maximized Then Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub SingleWindow_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        onGlobalMouseMove(sender, e)
    End Sub
#End Region

#Region "Common behaviors"
#Region "Overridden events"
    Protected Overrides Sub onControlAdded(ByVal e As System.Windows.Forms.ControlEventArgs)
        MyBase.OnControlAdded(e)

        onControlAdded(Me, e)
    End Sub

    Private Overloads Sub onControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs)
        onControlAdded(e.Control)
    End Sub

    Private Overloads Sub onControlAdded(ByVal myControl As Control)
        'Subscribing to events for special behavior
        AddHandler myControl.Click, AddressOf autoFocusFormOnClick
        AddHandler myControl.MouseMove, AddressOf onGlobalMouseMove
        AddHandler myControl.MouseEnter, AddressOf onGlobalMouseEnter

        'To manage inner controls of the control
        For Each curControl As Control In myControl.Controls
            onControlAdded(curControl)
        Next
        AddHandler myControl.ControlAdded, AddressOf onControlAdded
    End Sub


    Protected Sub onGlobalMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        RaiseEvent GlobalMouseMove(sender, e)
    End Sub

    Protected Sub onGlobalMouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent GlobalMouseEnter(sender, e)
    End Sub

    Private Sub autoFocusFormOnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs)
        AddHandler e.Control.Click, AddressOf autoFocusFormOnClick
        AddHandler e.Control.ControlAdded, AddressOf autoFocusFormOnClick
    End Sub

    Private Sub autoFocusFormOnClick(ByVal sender As Object, ByVal e As EventArgs)
        'REM Why is this code there ?
        'I disabled this code because when clicking on a button in a form which opens another form, it gives the focus back to the old form (so we loose the focus on the new form, which is BAD !)

        'Dim formToFocus As Form = CType(sender, Control).FindForm()
        'If formToFocus IsNot Nothing AndAlso formToFocus.Focused = False AndAlso CType(sender, Control).Focused = False Then formToFocus.Focus()
    End Sub

    Protected Overrides Sub onClick(ByVal e As System.EventArgs)
        MyBase.OnClick(e)
        Me.Focus()
    End Sub

    Protected Overrides Sub onEnter(ByVal e As System.EventArgs)
        MyBase.OnEnter(e)
        If Me.MdiParent Is Nothing Then Exit Sub

        Me.loaded = True

        If Me.WindowState <> FormWindowState.Minimized Then WindowsManager.getInstance.selected(Me)
    End Sub

    Protected Overrides Sub onKeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyUp(e)
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub centerForm()
        Dim parentSize As Size
        If Me.ParentForm IsNot Nothing Then
            parentSize = CType(Me.ParentForm, MainWin).ClientRectangle.Size
        Else
            parentSize = Screen.PrimaryScreen.WorkingArea.Size
        End If

        Me.Left = (parentSize.Width - Me.Width) / 2
        Me.Top = (parentSize.Height - Me.Height) / 2
    End Sub

    Protected Overrides Sub onLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        'Ajout ds la barre d'outils
        If Me.MdiParent IsNot Nothing Then WindowsManager.getInstance.addItemable(Me)

        'Force le centrage de la fenêtre
        If Me.StartPosition = FormStartPosition.CenterScreen Or Me.StartPosition = FormStartPosition.CenterParent Then
            centerForm()
        End If
    End Sub

    Protected Overrides Sub onFormClosed(ByVal e As System.Windows.Forms.FormClosedEventArgs)
        MyBase.OnFormClosed(e)

        If Me.MdiParent IsNot Nothing Then WindowsManager.getInstance.removeItemable(Me.Text)
    End Sub
#End Region

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function

    Public Function compareTo(ByVal x As Object) As Integer Implements IComparable.CompareTo
        Return Me.Text.CompareTo(CType(x, SingleWindow).Text)
    End Function

    Public Function compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Return String.Compare(CType(x, SingleWindow).Text, CType(y, SingleWindow).Text)
    End Function

    Public Overrides Function toString() As String
        Return Me.Text
    End Function

    Public Function saveWindow() As SingleWindowSaveEventArgs
        Dim eventReturn As New SingleWindowSaveEventArgs
        If Not mySavingMethod Is Nothing Then Me.Invoke(mySavingMethod, New Object() {Me, eventReturn})

        Return eventReturn
    End Function

    Public Shadows Sub bringToFront()
        MyBase.BringToFront()
        If Me.WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Normal
    End Sub
#End Region


#Region "HAS TO BE IMPLEMENTED IN DERIVED CLASS"
    'These shall be set has MustOverrides, but then class itself has to be set MustInherit (which is OK), but this brings the designer not to show the forms inheriting directly from SingleWindow
    'Tried to implement a solution of creating my own TypeDescriptor which shall fakes the type class so the designer would accept it.. but this code was transfered from C# and it was working on a beta version of VS.. so, did not figured out why

    Public Overridable Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
        Throw New NotImplementedException()
    End Sub

    Public Overridable ReadOnly Property savingMethod() As [Delegate]
        Get
            Throw New NotImplementedException()
        End Get
    End Property
#End Region

End Class
