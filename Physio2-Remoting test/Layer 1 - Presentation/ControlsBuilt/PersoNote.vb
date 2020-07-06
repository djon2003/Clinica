Imports CI.Base

Public Class PersoNote
    Inherits System.Windows.Forms.UserControl
    Implements IMovableObject, IControllable

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal alertAssociated As AlertOfPersoNote)
        MyBase.New()
        Me._AlertAssociated = alertAssociated

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        movingCursor = DrawingManager.getInstance.getCursor("MOVE4WAY.CUR")
        ctlTitle.Cursor = movingCursor
        DateHeureAlarmNote.Cursor = movingCursor
        DownMovingObject.Cursor = movingCursor
        Me.visible = False
        Me.selectAlarmDate.Image = DrawingManager.getInstance.getImage("selection16.gif")

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ctlTitle As System.Windows.Forms.Label
    Friend WithEvents selectAlarmDate As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents DateHeureAlarmNote As System.Windows.Forms.Label
    Friend WithEvents Note As Clinica.TextControl
    Friend WithEvents AlarmGroup As System.Windows.Forms.GroupBox
    Friend WithEvents DownMovingObject As System.Windows.Forms.Label
    Friend WithEvents NoteTitle As System.Windows.Forms.Label
    Friend WithEvents ControlBox As Clinica.TransparentControlBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ControlBox = New Clinica.TransparentControlBox
        Me.AlarmGroup = New System.Windows.Forms.GroupBox
        Me.selectAlarmDate = New System.Windows.Forms.Button
        Me.DateHeureAlarmNote = New System.Windows.Forms.Label
        Me.DownMovingObject = New System.Windows.Forms.Label
        Me.ctlTitle = New System.Windows.Forms.Label
        Me.Note = New Clinica.TextControl
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.NoteTitle = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.AlarmGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.ControlBox)
        Me.Panel1.Controls.Add(Me.AlarmGroup)
        Me.Panel1.Controls.Add(Me.NoteTitle)
        Me.Panel1.Controls.Add(Me.ctlTitle)
        Me.Panel1.Controls.Add(Me.Note)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(297, 323)
        Me.Panel1.TabIndex = 0
        '
        'ControlBox
        '
        Me.ControlBox.BackColor = System.Drawing.Color.Transparent
        Me.ControlBox.isLocked = True
        Me.ControlBox.IsSwitchedToToolbar = True
        Me.ControlBox.Location = New System.Drawing.Point(244, 2)
        Me.ControlBox.Name = "ControlBox"
        Me.ControlBox.Size = New System.Drawing.Size(47, 15)
        Me.ControlBox.TabIndex = 50
        '
        'AlarmGroup
        '
        Me.AlarmGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlarmGroup.Controls.Add(Me.selectAlarmDate)
        Me.AlarmGroup.Controls.Add(Me.DateHeureAlarmNote)
        Me.AlarmGroup.Controls.Add(Me.DownMovingObject)
        Me.AlarmGroup.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.AlarmGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AlarmGroup.ForeColor = System.Drawing.Color.White
        Me.AlarmGroup.Location = New System.Drawing.Point(-8, 275)
        Me.AlarmGroup.Name = "AlarmGroup"
        Me.AlarmGroup.Size = New System.Drawing.Size(305, 48)
        Me.AlarmGroup.TabIndex = 2
        Me.AlarmGroup.TabStop = False
        Me.AlarmGroup.Text = "Alarme"
        '
        'selectAlarmDate
        '
        Me.selectAlarmDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.selectAlarmDate.BackColor = System.Drawing.SystemColors.Control
        Me.selectAlarmDate.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.selectAlarmDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectAlarmDate.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectAlarmDate.Location = New System.Drawing.Point(16, 16)
        Me.selectAlarmDate.Name = "selectAlarmDate"
        Me.selectAlarmDate.Size = New System.Drawing.Size(24, 24)
        Me.selectAlarmDate.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.selectAlarmDate, "Sélectionner la date pour l'alarme de la note")
        Me.selectAlarmDate.UseVisualStyleBackColor = False
        '
        'DateHeureAlarmNote
        '
        Me.DateHeureAlarmNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DateHeureAlarmNote.ForeColor = System.Drawing.Color.White
        Me.DateHeureAlarmNote.Location = New System.Drawing.Point(48, 16)
        Me.DateHeureAlarmNote.Name = "DateHeureAlarmNote"
        Me.DateHeureAlarmNote.Size = New System.Drawing.Size(240, 24)
        Me.DateHeureAlarmNote.TabIndex = 3
        Me.DateHeureAlarmNote.Text = "Aucune date et heure sélectionnées"
        Me.DateHeureAlarmNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DownMovingObject
        '
        Me.DownMovingObject.BackColor = System.Drawing.Color.Transparent
        Me.DownMovingObject.Location = New System.Drawing.Point(8, 8)
        Me.DownMovingObject.Name = "DownMovingObject"
        Me.DownMovingObject.Size = New System.Drawing.Size(288, 40)
        Me.DownMovingObject.TabIndex = 4
        '
        'Title
        '
        Me.ctlTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlTitle.BackColor = System.Drawing.Color.Black
        Me.ctlTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlTitle.ForeColor = System.Drawing.Color.White
        Me.ctlTitle.Location = New System.Drawing.Point(0, 0)
        Me.ctlTitle.Name = "Title"
        Me.ctlTitle.Size = New System.Drawing.Size(297, 24)
        Me.ctlTitle.TabIndex = 1
        Me.ctlTitle.Text = "Note personnelle"
        Me.ctlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Note
        '
        Me.Note.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Note.ancre = Nothing
        Me.Note.ancreON = False
        Me.Note.ancreRemove = False
        Me.Note.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Note.Location = New System.Drawing.Point(0, 47)
        Me.Note.Name = "Note"
        Me.Note.showImgMenu = True
        Me.Note.showMenu = True
        Me.Note.Size = New System.Drawing.Size(297, 224)
        Me.Note.TabIndex = 0
        Me.Note.tabSpacing = CType(0, Short)
        Me.Note.Text = ""
        '
        'NoteTitle
        '
        Me.NoteTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NoteTitle.BackColor = System.Drawing.Color.Black
        Me.NoteTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoteTitle.ForeColor = System.Drawing.Color.White
        Me.NoteTitle.Location = New System.Drawing.Point(-1, 21)
        Me.NoteTitle.Name = "NoteTitle"
        Me.NoteTitle.Size = New System.Drawing.Size(297, 24)
        Me.NoteTitle.TabIndex = 1
        Me.NoteTitle.Text = "Titre"
        Me.NoteTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PersoNote
        '
        Me.Controls.Add(Me.Panel1)
        Me.Name = "PersoNote"
        Me.Size = New System.Drawing.Size(297, 323)
        Me.Location = New Point(-1, -1)
        Me.Panel1.ResumeLayout(False)
        Me.AlarmGroup.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _AlertAssociated As AlertOfPersoNote
    Private movingCursor As Cursor
    Private XObjects, YObjects, XSplitObjects, ySplitObjects As Integer
    Private ButtonDown, buttonDownForSplit As MouseButtons
    Private _BlockObjectInArea As Boolean = True
    Private noteLoaded As Boolean = False
    Private _JoinedToolBarItem As ToolStrip = Nothing
    Private formCreator As Form
    Private currentlyResizing As Boolean = False
    Private _NoAlert As Integer = 0
    Private myTitle As String

    Public Shadows Event move(ByVal sender As Object, ByVal e As System.EventArgs) Implements IMovableObject.move
    Public Event willMove(ByVal sender As Object, ByVal x As Integer, ByVal y As Integer, ByVal xObjects As Integer, ByVal yObjects As Integer) Implements IMovableObject.willMove

#Region "Propriétés"
    Public Property blockObjectInArea() As Boolean Implements IMovableObject.blockObjectInArea
        Get
            Return _BlockObjectInArea
        End Get
        Set(ByVal Value As Boolean)
            _BlockObjectInArea = Value
        End Set
    End Property

    Public Property alertAssociated() As AlertOfPersoNote
        Get
            Return _AlertAssociated
        End Get
        Set(ByVal value As AlertOfPersoNote)
            _AlertAssociated = value
            Me._NoAlert = value.noAlert
            noteLoaded = True
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

    Public Property title() As String
        Get
            Return myTitle
        End Get
        Set(ByVal value As String)
            myTitle = value
            NoteTitle.Text = value
        End Set
    End Property

    Public WriteOnly Property setMessage() As String
        Set(ByVal value As String)
            Note.Rtf = value
            noteLoaded = True
        End Set
    End Property

    Public ReadOnly Property noAlert() As Integer
        Get
            If _AlertAssociated Is Nothing Then Return 0

            Return _AlertAssociated.noAlert
        End Get
    End Property

    Public WriteOnly Property setDateTime() As Date
        Set(ByVal value As Date)
            DateHeureAlarmNote.Text = DateFormat.getTextDate(value, DateFormat.TextDateOptions.FullDayMonthNames) & " " & DateFormat.getTextDate(value, DateFormat.TextDateOptions.ShortTime)
            DateHeureAlarmNote.Tag = DateFormat.getTextDate(value, DateFormat.TextDateOptions.YYYYMMDD) & " " & DateFormat.getTextDate(value, DateFormat.TextDateOptions.ShortTime)
        End Set
    End Property
#End Region

    Public Sub deleteControl()
        If Me.Parent IsNot Nothing Then Me.Parent.Controls.Remove(Me)
        Me.visible = False
        Me.Dispose()
    End Sub

    Public Overloads Sub focus() Implements IControllable.focus
        MyBase.Focus()
        Me.BringToFront()
    End Sub

    Private Sub selectAlarmDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectAlarmDate.Click
        Dim myDateChoice As DateChoice = New DateChoice()
        Dim myDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year, Date.Now.Year + 1, True, True, True, "00:00", "23:59", True, Date.Now, , , , , , , , , 1)

        If myDate.Count <> 0 Then
            DateHeureAlarmNote.Text = DateFormat.getTextDate(myDate(0), DateFormat.TextDateOptions.FullDayMonthNames) & " " & DateFormat.getTextDate(myDate(0), DateFormat.TextDateOptions.ShortTime)
            'TODO : Shall use the first line instead of the second one
            DateHeureAlarmNote.Tag = myDate(0)
            DateHeureAlarmNote.Tag = DateFormat.getTextDate(myDate(0), DateFormat.TextDateOptions.YYYYMMDD_FullTime)
            _AlertAssociated.alertAlarm = New AlarmOfPersoNote(myDate(0))
            _AlertAssociated.alertAlarm.alertAssociated = Me._AlertAssociated

            AlarmManager.getInstance.addAlarm(_AlertAssociated.alertAlarm)
        End If
    End Sub

    Public Sub saveNote()
        If noteLoaded = False Then Exit Sub

        Me.alertAssociated.saveData()
    End Sub

    Public Sub resetAlarmDate()
        DateHeureAlarmNote.Text = "Aucune date et heure sélectionnées"
        DateHeureAlarmNote.Tag = Nothing
    End Sub

    Private Sub clickingControls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ControlBox.Click, DateHeureAlarmNote.Click, DownMovingObject.Click, Note.Click, NoteTitle.Click, Panel1.Click, selectAlarmDate.Click, ctlTitle.Click
        Me._HasToBlink = False
        Me.NoteTitle.ForeColor = Color.White
    End Sub

    Private Sub movingControls_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlTitle.MouseDown, DownMovingObject.MouseDown, DateHeureAlarmNote.MouseDown, NoteTitle.MouseDown
        ButtonDown = e.Button
        XObjects = e.X
        YObjects = e.Y
    End Sub

    Private Sub movingControls_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlTitle.MouseUp, DownMovingObject.MouseUp, DateHeureAlarmNote.MouseUp, NoteTitle.MouseUp
        ButtonDown = MouseButtons.None
    End Sub

    Private Sub movingControls_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ctlTitle.MouseMove, DownMovingObject.MouseMove, DateHeureAlarmNote.MouseMove, NoteTitle.MouseMove
        Me.BringToFront()

        If blockMove = True Then Exit Sub
        If currentlyResizing Then Exit Sub

        If blockObjectInArea = True And e.Button = System.Windows.Forms.MouseButtons.Left Then
            RaiseEvent willMove(Me, e.X, e.Y, XObjects, YObjects)
            Dim NewTop, newLeft As Integer
            NewTop = (Me.Top + (e.Y - YObjects))
            newLeft = (Me.Left + (e.X - XObjects))
            setCoord(ensureGoodCoord(NewTop, newLeft, Me.IsSwitchedToToolbar))
            RaiseEvent move(Me, EventArgs.Empty)
        End If
    End Sub

    Public Sub weakUpFromAlarm()
        Me._HasToBlink = True
        Me.NoteTitle.ForeColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
        Me.visible = True
        Me.BringToFront()
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

    Private Sub controlBox_ClosingControl() Handles ControlBox.closingControl
        Me.visible = False
        RaiseEvent closing(Me)
    End Sub

    Private Sub note_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Note.MouseMove
        Me.BringToFront()
    End Sub

    Private Sub alarmGroup_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AlarmGroup.MouseDown
        buttonDownForSplit = e.Button
        XSplitObjects = e.X
        ySplitObjects = e.Y
    End Sub

    Private Sub alarmGroup_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AlarmGroup.MouseUp
        buttonDownForSplit = MouseButtons.None
    End Sub

    Private Sub alarmGroup_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AlarmGroup.MouseMove
        Me.BringToFront()
        If blockMove = True Then Exit Sub

        If buttonDownForSplit = MouseButtons.Left Then
            currentlyResizing = True
            Dim newHeight As Integer = Me.Height + (e.Y - ySplitObjects)
            If newHeight < 120 Then newHeight = 120
            If newHeight + Me.Top > myMainWin.mdiChildRectangle.Height Then newHeight = myMainWin.mdiChildRectangle.Height - Me.Top
            Me.Height = newHeight
            currentlyResizing = False
        End If
    End Sub

    Private Sub persoNote_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.visible = False Then saveNote()
    End Sub

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

    Private Sub controlBox_LockingControl(ByVal willBeLocked As Boolean) Handles ControlBox.lockingControl
        If willBeLocked = True Then
            ctlTitle.Cursor = Cursors.Default
            DownMovingObject.Cursor = Cursors.Default
            DateHeureAlarmNote.Cursor = Cursors.Default
            AlarmGroup.Cursor = movingCursor
            NoteTitle.Cursor = Cursors.Default
        Else
            NoteTitle.Cursor = movingCursor
            ctlTitle.Cursor = movingCursor
            DownMovingObject.Cursor = movingCursor
            DateHeureAlarmNote.Cursor = movingCursor
            AlarmGroup.Cursor = Cursors.HSplit
        End If
    End Sub

    Public Event barTitleChanged(ByVal sender As IControllable) Implements IControllable.barTitleChanged

    Public Function getBarTitle() As String Implements IControllable.getBarTitle
        Return "Note - " & myTitle
    End Function

    Private _HasToBlink As Boolean = False

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

    Public Sub centerObject()
        setCoord(New Point((Me.ParentForm.ClientSize.Width - Me.Width) / 2, (Me.ParentForm.ClientSize.Height - Me.Height) / 2))
    End Sub
End Class
