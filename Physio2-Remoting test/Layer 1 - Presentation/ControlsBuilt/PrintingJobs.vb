Public Class PrintingJobs
    Implements IMovableObject, IControllable

    Private XObjects, yObjects As Integer
    Private buttonDown As MouseButtons
    Private movingCursor As Cursor
    Private _BlockObjectInArea As Boolean = True
    Private _IsClosed As Boolean = True
    Private _HasToBlink As Boolean = False

    Public Event barTitleChanged(ByVal sender As Base.IControllable) Implements Base.IControllable.barTitleChanged
    Public Event closing(ByVal sender As Base.IControllable) Implements Base.IControllable.closing
    Public Shadows Event move(ByVal sender As Object, ByVal e As System.EventArgs) Implements Base.IMovableObject.move
    Public Event willMove(ByVal sender As Object, ByVal x As Integer, ByVal y As Integer, ByVal xObjects As Integer, ByVal yObjects As Integer) Implements Base.IMovableObject.willMove

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        movingCursor = DrawingManager.getInstance.getCursor("MOVE4WAY.CUR")
        ctlTitle.Cursor = movingCursor
        ControlBox.isLocked = False

        AddHandler PrintingHelper.JobsChanged, AddressOf loading
    End Sub

    Private Sub loading()
        loading(Nothing, Nothing)
    End Sub


    Private Sub title_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlTitle.MouseDown
        buttonDown = e.Button
        XObjects = e.X
        yObjects = e.Y
    End Sub

    Private Sub title_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlTitle.MouseUp
        buttonDown = MouseButtons.None
    End Sub

    Private Sub title_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlTitle.MouseMove
        If blockMove = True Then Exit Sub

        If blockObjectInArea = True And e.Button = System.Windows.Forms.MouseButtons.Left Then
            RaiseEvent willMove(Me, e.X, e.Y, XObjects, yObjects)
            Dim NewTop, newLeft As Integer
            NewTop = (Me.Top + (e.Y - yObjects))
            newLeft = (Me.Left + (e.X - XObjects))

            setCoord(ensureGoodCoord(NewTop, newLeft, Me.IsSwitchedToToolbar))
            RaiseEvent move(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub loading(ByVal sender As Object, ByVal e As EventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandler(AddressOf loading))
            Exit Sub
        End If

        jobsList.cls()
        For Each curJob As PrintingJob In PrintingHelper.getJobs()
            Dim status As String = " en file d'attente"
            If curJob.jobStatus = PrintingHelper.JobStatuses.PresendingActions Then
                status = " en préparation pour l'envoi à l'imprimante"
            ElseIf curJob.jobStatus = PrintingHelper.JobStatuses.SendingToPrinter Then
                status = " en cours d'envoi vers l'imprimante"
            ElseIf curJob.jobStatus = PrintingHelper.JobStatuses.PostsendingActions Then
                status = " en terminisation de l'envoi à l'imprimante"
            End If
            jobsList.add(curJob.startedAt.ToString("HH:mm:ss") & " : " & curJob.jobTitle & status)
        Next

        jobsList.draw = True : jobsList.draw = False
    End Sub

    Private Sub ControlBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ControlBox.Click

    End Sub


    Private Sub controlBox_ClosingControl() Handles ControlBox.closingControl
        Me.Visible = False
    End Sub

    Private Sub controlBox_LockingControl(ByVal willBeLocked As Boolean) Handles ControlBox.lockingControl
        If willBeLocked = True Then
            ctlTitle.Cursor = Cursors.Default
        Else
            ctlTitle.Cursor = movingCursor
        End If
    End Sub


    Public Overloads Sub focus() Implements IControllable.focus
        MyBase.Focus()
        Me.BringToFront()
    End Sub


    Public Function getBarTitle() As String Implements Base.IControllable.getBarTitle
        Return ctlTitle.Text
    End Function


    Public ReadOnly Property hasToBlink() As Boolean Implements IControllable.hasToBlink
        Get
            Return _HasToBlink
        End Get
    End Property

    Public Property IsSwitchedToToolbar(Optional ByVal showPanel As Boolean = True) As Boolean Implements IControllable.isSwitchedToToolbar
        Get
            Return ControlBox.IsSwitchedToToolbar
        End Get
        Set(ByVal value As Boolean)
            ControlBox.IsSwitchedToToolbar(showPanel) = value
        End Set
    End Property

    Public Event switchingControl(ByVal sender As IControllable, ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean) Implements IControllable.switchingControl

    Private Sub controlBox_SwitchingControl(ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean) Handles ControlBox.switchingControl
        RaiseEvent switchingControl(Me, willBeSwitchedToToolBar, showPanel)
        setCoord(ensureGoodCoord(Me.Top, Me.Left, willBeSwitchedToToolBar))
    End Sub


    Public Overloads Property visible() As Boolean Implements IControllable.visible
        Get
            Return MyBase.Visible
        End Get
        Set(ByVal value As Boolean)
            Dim curValue As Boolean = MyBase.Visible
            _IsClosed = Not value
            MyBase.Visible = value
            If curValue = value Then MyBase.OnVisibleChanged(EventArgs.Empty)
        End Set
    End Property

    Public Property isClosed() As Boolean Implements IControllable.isClosed
        Get
            Return _IsClosed
        End Get
        Set(ByVal value As Boolean)
            _IsClosed = value
            MyBase.Visible = Not value
        End Set
    End Property

    Public Sub centerObject()
        setCoord(New Point((Me.ParentForm.ClientSize.Width - Me.Width) / 2, (Me.ParentForm.ClientSize.Height - Me.Height) / 2))
    End Sub

    Public Property blockMove() As Boolean Implements IMovableObject.blockMove
        Get
            Return ControlBox.isLocked
        End Get
        Set(ByVal Value As Boolean)
            ControlBox.isLocked = Value
        End Set
    End Property



    Public Property blockObjectInArea() As Boolean Implements IMovableObject.blockObjectInArea
        Get
            Return _BlockObjectInArea
        End Get
        Set(ByVal Value As Boolean)
            _BlockObjectInArea = Value
        End Set
    End Property


    Private Function ensureGoodCoord(ByVal newTop As Integer, ByVal newLeft As Integer, ByVal isSwitchedToToolbar As Boolean) As Point Implements IMovableObject.ensureGoodCoord
        If myMainWin Is Nothing Then Return New Point(newLeft, newTop)

        If isSwitchedToToolbar = False Then
            If Not (newTop > myMainWin.mdiChildRectangle.Top And (newTop + Me.Height) < myMainWin.mdiChildRectangle.Bottom) Then
                If newTop > myMainWin.mdiChildRectangle.Top Then
                    newTop = myMainWin.mdiChildRectangle.Bottom - Me.Height
                Else
                    newTop = myMainWin.mdiChildRectangle.Top
                End If
            End If
            If Not (newLeft > myMainWin.mdiChildRectangle.Left And (newLeft + Me.Width) < myMainWin.mdiChildRectangle.Right) Then
                If newLeft > myMainWin.mdiChildRectangle.Left Then
                    newLeft = myMainWin.mdiChildRectangle.Right - Me.Width
                Else
                    newLeft = myMainWin.mdiChildRectangle.Left
                End If
            End If
        Else
            If Not (newTop >= 0 And (newTop + Me.Height) < (myMainWin.mdiChildRectangle.Bottom - myMainWin.mdiChildRectangle.Top)) Then
                If newTop > 0 Then
                    newTop = myMainWin.mdiChildRectangle.Bottom - Me.Height - myMainWin.mdiChildRectangle.Top
                Else
                    newTop = 0
                End If
            End If
            If Not (newLeft > myMainWin.barMainObjects.getBarWidth And (newLeft + Me.Width) < myMainWin.mdiChildRectangle.Right) Then
                If newLeft > myMainWin.barMainObjects.getBarWidth Then
                    newLeft = myMainWin.mdiChildRectangle.Right - Me.Width
                Else
                    newLeft = myMainWin.barMainObjects.getBarWidth
                End If
            End If
        End If

        Return New Point(newLeft, newTop)
    End Function


    Public Function getCoord() As System.Drawing.Point Implements IMovableObject.getCoord
        Return Me.Location
    End Function

    Public Sub setCoord(ByVal newCoord As System.Drawing.Point) Implements IMovableObject.setCoord
        Console.WriteLine("Fifth")
        Me.Location = newCoord
    End Sub

    Public Sub setMovability(ByVal movable As Boolean) Implements IMovableObject.setMovability
        Me.Left = 0
        ControlBox.setLockability(movable)
    End Sub

    Private Sub PrintingJobs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loading()
    End Sub

    Private Sub jobsList_dblClick(ByVal sender As Object, ByVal e As Controls.List.DblClickEventArgs) Handles jobsList.dblClick
        PrintingHelper.focusOnPrintersWindow()
    End Sub
End Class
