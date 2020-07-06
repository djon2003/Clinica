Public Class TransparentControlBox
    Inherits TransparentControl

    Public Event lockingControl(ByVal willBeLocked As Boolean)
    Public Event closingControl()
    Public Event switchingControl(ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean)

    Public Sub New()
        MyBase.New(New InvisibleControlBox())

        AddHandler myControlBox.closingControl, AddressOf myControlBox_ClosingControl
        AddHandler myControlBox.lockingControl, AddressOf myControlBox_LockingControl
        AddHandler myControlBox.switchingControl, AddressOf myControlBox_SwitchingControl
    End Sub


    Private ReadOnly Property myControlBox() As InvisibleControlBox
        Get
            Return CType(myControl, InvisibleControlBox)
        End Get
    End Property

    Private Sub transparentControl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Size = New System.Drawing.Size(myControl.Width, myControl.Height)
    End Sub

    Private Sub myControlBox_ClosingControl()
        RaiseEvent closingControl()
    End Sub

    Private Sub myControlBox_LockingControl(ByVal willBeLocked As Boolean)
        RaiseEvent lockingControl(willBeLocked)
    End Sub

    Private Sub myControlBox_SwitchingControl(ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean)
        RaiseEvent switchingControl(willBeSwitchedToToolBar, showPanel)
    End Sub

    Public Property isLocked() As Boolean
        Get
            Return myControlBox.isLocked
        End Get
        Set(ByVal Value As Boolean)
            If Me.DesignMode = False Then myControlBox.isLocked = Value
        End Set
    End Property

    Public Property IsSwitchedToToolbar(Optional ByVal showPanel As Boolean = True) As Boolean
        Get
            Return myControlBox.IsSwitchedToToolbar
        End Get
        Set(ByVal Value As Boolean)
            If Me.DesignMode = False Then myControlBox.IsSwitchedToToolbar(showPanel) = Value
        End Set
    End Property

    Public Overloads Sub dispose()
        RemoveHandler myControlBox.closingControl, AddressOf myControlBox_ClosingControl
        RemoveHandler myControlBox.lockingControl, AddressOf myControlBox_LockingControl
        RemoveHandler myControlBox.switchingControl, AddressOf myControlBox_SwitchingControl

        MyBase.Dispose()
    End Sub

    Public Sub setLockability(ByVal lockable As Boolean)
        myControlBox.setLockability(lockable)
    End Sub
End Class
