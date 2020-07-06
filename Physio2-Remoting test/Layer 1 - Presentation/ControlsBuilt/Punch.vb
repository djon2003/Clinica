Public Class Punch
    Inherits System.Windows.Forms.UserControl
    Implements Base.IMovableObject, IControllable

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        movingCursor = DrawingManager.getInstance.getCursor("MOVE4WAY.CUR")
        Bordure.Cursor = movingCursor
    End Sub

#Region " Windows Form Designer generated code "

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents Bordure As System.Windows.Forms.Panel
    Friend WithEvents ControlBox As Clinica.TransparentControlBox
    Friend WithEvents LabelTitle As System.Windows.Forms.Label
    Friend WithEvents LabelDepart As System.Windows.Forms.Label
    Friend WithEvents HeureDepart As System.Windows.Forms.Label
    Friend WithEvents HeureArrivee As System.Windows.Forms.Label
    Friend WithEvents LabelArrivee As System.Windows.Forms.Label
    Friend WithEvents Punching As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Bordure = New System.Windows.Forms.Panel
        Me.Punching = New System.Windows.Forms.Button
        Me.LabelDepart = New System.Windows.Forms.Label
        Me.HeureDepart = New System.Windows.Forms.Label
        Me.HeureArrivee = New System.Windows.Forms.Label
        Me.LabelArrivee = New System.Windows.Forms.Label
        Me.ControlBox = New Clinica.TransparentControlBox
        Me.LabelTitle = New System.Windows.Forms.Label
        Me.Bordure.SuspendLayout()
        Me.SuspendLayout()
        '
        'Bordure
        '
        Me.Bordure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bordure.Controls.Add(Me.Punching)
        Me.Bordure.Controls.Add(Me.LabelDepart)
        Me.Bordure.Controls.Add(Me.HeureDepart)
        Me.Bordure.Controls.Add(Me.HeureArrivee)
        Me.Bordure.Controls.Add(Me.LabelArrivee)
        Me.Bordure.Controls.Add(Me.ControlBox)
        Me.Bordure.Controls.Add(Me.LabelTitle)
        Me.Bordure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Bordure.Location = New System.Drawing.Point(0, 0)
        Me.Bordure.Name = "Bordure"
        Me.Bordure.Size = New System.Drawing.Size(145, 105)
        Me.Bordure.TabIndex = 0
        '
        'Punching
        '
        Me.Punching.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Punching.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Punching.Location = New System.Drawing.Point(6, 70)
        Me.Punching.Name = "Punching"
        Me.Punching.Size = New System.Drawing.Size(132, 26)
        Me.Punching.TabIndex = 4
        Me.Punching.Text = "'Punch'"
        Me.Punching.UseVisualStyleBackColor = True
        '
        'LabelDepart
        '
        Me.LabelDepart.AutoSize = True
        Me.LabelDepart.Location = New System.Drawing.Point(3, 43)
        Me.LabelDepart.Name = "LabelDepart"
        Me.LabelDepart.Size = New System.Drawing.Size(90, 13)
        Me.LabelDepart.TabIndex = 3
        Me.LabelDepart.Text = "Heure de départ :"
        '
        'HeureDepart
        '
        Me.HeureDepart.AutoSize = True
        Me.HeureDepart.Location = New System.Drawing.Point(94, 43)
        Me.HeureDepart.Name = "HeureDepart"
        Me.HeureDepart.Size = New System.Drawing.Size(44, 13)
        Me.HeureDepart.TabIndex = 3
        Me.HeureDepart.Text = "Aucune"
        '
        'HeureArrivee
        '
        Me.HeureArrivee.AutoSize = True
        Me.HeureArrivee.Location = New System.Drawing.Point(94, 28)
        Me.HeureArrivee.Name = "HeureArrivee"
        Me.HeureArrivee.Size = New System.Drawing.Size(44, 13)
        Me.HeureArrivee.TabIndex = 3
        Me.HeureArrivee.Text = "Aucune"
        '
        'LabelArrivee
        '
        Me.LabelArrivee.AutoSize = True
        Me.LabelArrivee.Location = New System.Drawing.Point(3, 28)
        Me.LabelArrivee.Name = "LabelArrivee"
        Me.LabelArrivee.Size = New System.Drawing.Size(85, 13)
        Me.LabelArrivee.TabIndex = 3
        Me.LabelArrivee.Text = "Heure d'arrivée :"
        '
        'ControlBox
        '
        Me.ControlBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ControlBox.BackColor = System.Drawing.Color.Transparent
        Me.ControlBox.isLocked = False
        Me.ControlBox.Location = New System.Drawing.Point(61, 3)
        Me.ControlBox.Name = "ControlBox"
        Me.ControlBox.Size = New System.Drawing.Size(64, 16)
        Me.ControlBox.TabIndex = 1
        '
        'LabelTitle
        '
        Me.LabelTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabelTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTitle.Location = New System.Drawing.Point(-1, -1)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(145, 19)
        Me.LabelTitle.TabIndex = 2
        Me.LabelTitle.Text = "'Punch' virtuel"
        Me.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Punch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Bordure)
        Me.Name = "Punch"
        Me.Size = New System.Drawing.Size(145, 105)
        Me.Bordure.ResumeLayout(False)
        Me.Bordure.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private movingCursor As Cursor
    Private XObjects, yObjects As Integer
    Private buttonDown As MouseButtons
    Private _BlockObjectInArea As Boolean = True
    Private _JoinedToolBarItem As ToolStrip = Nothing
    Private formCreator As Form

    Public Event willMove(ByVal sender As Object, ByVal x As Integer, ByVal y As Integer, ByVal xObjects As Integer, ByVal yObjects As Integer) Implements IMovableObject.willMove
    Public Shadows Event move(ByVal sender As Object, ByVal e As EventArgs) Implements IMovableObject.move

#Region "Propriétés"
    Public Property blockObjectInArea() As Boolean Implements IMovableObject.blockObjectInArea
        Get
            Return _BlockObjectInArea
        End Get
        Set(ByVal Value As Boolean)
            _BlockObjectInArea = Value
        End Set
    End Property

    Public Property blockMove() As Boolean Implements IMovableObject.blockMove
        Get
            Return ControlBox.isLocked
        End Get
        Set(ByVal Value As Boolean)
            ControlBox.isLocked = Value
        End Set
    End Property
#End Region

    Private Sub punching_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Punching.Click
        Dim myWeek As Date = Date.Today.AddDays(Date.Today.DayOfWeek * -1)
        Dim MyTime, myDayField As String
        Dim myMinutes As Integer
        Dim calcul As Decimal

        If HeureArrivee.Text <> "Aucune" And HeureDepart.Text <> "Aucune" Then
            HeureArrivee.Text = "Aucune"
            HeureDepart.Text = "Aucune"
        End If

        Select Case Date.Today.DayOfWeek
            Case DayOfWeek.Sunday
                myDayField = "Di"
            Case DayOfWeek.Monday
                myDayField = "Lu"
            Case DayOfWeek.Tuesday
                myDayField = "Ma"
            Case DayOfWeek.Wednesday
                myDayField = "Me"
            Case DayOfWeek.Thursday
                myDayField = "Je"
            Case DayOfWeek.Friday
                myDayField = "Ve"
            Case DayOfWeek.Saturday
                myDayField = "Sa"
            Case Else
                Exit Sub
        End Select

        myMinutes = Date.Now.Hour * 60 + Date.Now.Minute

        calcul = CDec(myMinutes)
        calcul /= 15

        If HeureArrivee.Text = "Aucune" Then
            myDayField &= "De"
            Select Case PreferencesManager.getGeneralPreferences.getProperty("PunchModeArrival")
                Case "Arrondir aux 15 minutes d'avant ou d'après"
                    calcul = Math.Round(calcul)

                Case "Toujours arrondir aux 15 minutes d'avant"
                    calcul = Math.Floor(calcul)

                Case "Toujours arrondir aux 15 minutes d'après"
                    calcul = Math.Ceiling(calcul)
            End Select
        Else
            myDayField &= "A"
            Select Case PreferencesManager.getGeneralPreferences()("PunchModeDeparture")
                Case "Arrondir aux 15 minutes d'avant ou d'après"
                    calcul = Math.Round(calcul)

                Case "Toujours arrondir aux 15 minutes d'avant"
                    calcul = Math.Floor(calcul)

                Case "Toujours arrondir aux 15 minutes d'après"
                    calcul = Math.Ceiling(calcul)
            End Select
        End If

        calcul *= 15
        myMinutes = CInt(calcul)
        calcul = Math.Floor(myMinutes / 60)
        MyTime = DateFormat.getTextDate(CDate(calcul & ":" & myMinutes - calcul * 60), DateFormat.TextDateOptions.ShortTime)

        Dim isNew() As String = DBLinker.getInstance.readOneDBField("WorkHours", "Week", "WHERE (NoUser=" & ConnectionsManager.currentUser & ") AND Week='" & myWeek.Year & "/" & myWeek.Month & "/" & myWeek.Day & "'")

        If isNew Is Nothing OrElse isNew.Length = 0 Then
            DBLinker.getInstance.writeDB("WorkHours", myDayField & ",Approuved,NoUser,Week", "'" & MyTime & "','" & False & "'," & ConnectionsManager.currentUser & ",'" & myWeek.Year & "/" & myWeek.Month & "/" & myWeek.Day & "'")
        Else
            DBLinker.getInstance.updateDB("WorkHours", myDayField & "='" & MyTime & "',Approuved='" & False & "'", "NoUser", ConnectionsManager.currentUser & " AND Week='" & myWeek.Year & "/" & myWeek.Month & "/" & myWeek.Day & "'", False)
        End If

        If myDayField.EndsWith("De") Then
            HeureArrivee.Text = DateFormat.getTextDate(CDate(MyTime), DateFormat.TextDateOptions.ShortTime)
            myMainWin.StatusText = myDayField & "'Punch' virtuel - Heure d'arrivée : " & HeureArrivee.Text
        Else
            HeureDepart.Text = DateFormat.getTextDate(CDate(MyTime), DateFormat.TextDateOptions.ShortTime)
            myMainWin.StatusText = myDayField & "'Punch' virtuel - Heure de départ : " & HeureDepart.Text
        End If
    End Sub

    Private Sub controlBox_ClosingControl() Handles ControlBox.closingControl
        Me.visible = False
        myMainWin.menuPunch.Checked = False
        RaiseEvent closing(Me)
    End Sub

    Private Sub objects_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Bordure.MouseDown, LabelArrivee.MouseDown, LabelDepart.MouseDown, LabelTitle.MouseDown, HeureArrivee.MouseDown, HeureDepart.MouseDown
        buttonDown = e.Button
        XObjects = e.X
        yObjects = e.Y
    End Sub

    Private Sub objects_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Bordure.MouseUp, LabelArrivee.MouseUp, LabelDepart.MouseUp, LabelTitle.MouseUp, HeureArrivee.MouseUp, HeureDepart.MouseUp
        buttonDown = System.Windows.Forms.MouseButtons.None
    End Sub

    Private Sub objects_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Bordure.MouseMove, LabelArrivee.MouseMove, LabelDepart.MouseMove, LabelTitle.MouseMove, HeureArrivee.MouseMove, HeureDepart.MouseMove
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

    Private Function ensureGoodCoord(ByVal newTop As Integer, ByVal newLeft As Integer, ByVal isSwitchedToToolbar As Boolean) As Point Implements IMovableObject.ensureGoodCoord
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

    Public Sub loading()
        Dim myWeek As Date = Date.Today.AddDays(Date.Today.DayOfWeek * -1)
        Dim myDayField As String

        Select Case Date.Today.DayOfWeek
            Case DayOfWeek.Sunday
                myDayField = "Di"
            Case DayOfWeek.Monday
                myDayField = "Lu"
            Case DayOfWeek.Tuesday
                myDayField = "Ma"
            Case DayOfWeek.Wednesday
                myDayField = "Me"
            Case DayOfWeek.Thursday
                myDayField = "Je"
            Case DayOfWeek.Friday
                myDayField = "Ve"
            Case DayOfWeek.Saturday
                myDayField = "Sa"
            Case Else
                Exit Sub
        End Select

        Dim myTimes(,) As String = DBLinker.getInstance.readDB("WorkHours", myDayField & "De," & myDayField & "A", "WHERE (NoUser=" & ConnectionsManager.currentUser & ") AND Week='" & myWeek.Year & "/" & myWeek.Month & "/" & myWeek.Day & "'")
        If Not myTimes Is Nothing AndAlso myTimes.Length <> 0 Then
            If myTimes(0, 0) <> "" Then HeureArrivee.Text = DateFormat.getTextDate(CDate(myTimes(0, 0)), DateFormat.TextDateOptions.ShortTime)
            If myTimes(1, 0) <> "" Then HeureDepart.Text = DateFormat.getTextDate(CDate(myTimes(1, 0)), DateFormat.TextDateOptions.ShortTime)
        End If

        'REM Disabled widget
        Me.visible = False
    End Sub

    Public Function getCoord() As System.Drawing.Point Implements IMovableObject.getCoord
        Return Me.Location
    End Function

    Public Sub setCoord(ByVal newCoord As System.Drawing.Point) Implements IMovableObject.setCoord
        Me.Location = newCoord
    End Sub

    Public Sub setMovability(ByVal movable As Boolean) Implements IMovableObject.setMovability
        Me.Left = 0
        ControlBox.setLockability(movable)
    End Sub

    Private Sub controlBox_LockingControl(ByVal willBeLocked As Boolean) Handles ControlBox.lockingControl
        If willBeLocked = True Then
            Bordure.Cursor = Cursors.Default
        Else
            Bordure.Cursor = movingCursor
        End If
    End Sub

    Public Event barTitleChanged(ByVal sender As IControllable) Implements IControllable.barTitleChanged

    Public Function getBarTitle() As String Implements IControllable.getBarTitle
        Return "Punch virtuel"
    End Function

    Public ReadOnly Property hasToBlink() As Boolean Implements IControllable.hasToBlink
        Get
            Return False
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

    Private _IsClosed As Boolean = True

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

    Public Event closing(ByVal sender As IControllable) Implements IControllable.closing

    Public Overloads Sub focus() Implements IControllable.focus
        MyBase.Focus()
    End Sub
End Class
