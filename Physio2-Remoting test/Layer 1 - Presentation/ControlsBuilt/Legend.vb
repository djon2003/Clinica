Public Class Legend
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        headerSpacing = 22

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Dim i As Integer
        Me.Padding = New Padding(0, headerSpacing, 0, LockingLegende.Height)
        For i = 0 To Me.Controls.Count - 1
            With Me.Controls(i)
                AddHandler .MouseLeave, AddressOf objects_MouseLeave
                AddHandler .MouseMove, AddressOf objects_MouseMove
                Try
                    AddHandler .MouseEnter, AddressOf objects_MouseEnter
                Catch
                End Try
            End With
        Next i

        Dim curUserPref As Preferences = PreferencesManager.getUserPreferences()
        If Software.isStarted AndAlso curUserPref IsNot Nothing AndAlso CType(curUserPref("AutoScrollHiddenBox"), Boolean) = False Then lockingLegende_Click(Me, EventArgs.Empty)

        Me.Font = New Font(Me.Font.Name, 10)
        legendHeight = Me.Height '- LegendDiff
        legendWidth = Me.Width

        Me.scrollOrientation = LegendeOrientation.ScrollDown
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            'If CurrentArrowIMG IsNot Nothing Then CurrentArrowIMG.Dispose()
            For i As Integer = 0 To Me.Controls.Count - 1
                With Me.Controls(i)
                    RemoveHandler .MouseLeave, AddressOf objects_MouseLeave
                    RemoveHandler .MouseMove, AddressOf objects_MouseMove
                    Try
                        RemoveHandler .MouseEnter, AddressOf objects_MouseEnter
                    Catch
                    End Try
                End With
            Next i

            If Not Me.ParentForm Is Nothing Then RemoveHandler Me.ParentForm.LocationChanged, AddressOf legendes_LocationChanged
            'If Not CurrentArrowIMG Is Nothing Then
            '    CurrentArrowIMG.Dispose()
            '    CurrentArrowIMG = Nothing
            'End If
            'If Not OriginalArrowIMG Is Nothing Then
            '    'OriginalArrowIMG.Dispose()
            '    OriginalArrowIMG = Nothing
            'End If
            'If Not LockWindowImgs Is Nothing Then
            '    LockWindowImgs.Images.Clear()
            '    LockWindowImgs.Dispose()
            '    LockWindowImgs = Nothing
            'End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents Fleche As System.Windows.Forms.PictureBox
    Friend WithEvents LockingLegende As System.Windows.Forms.Button
    Friend WithEvents closeLegende As System.Windows.Forms.Button
    Friend WithEvents LegendeTimer As System.Windows.Forms.Timer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.LockingLegende = New System.Windows.Forms.Button
        Me.LegendeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.closeLegende = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LockingLegende
        '
        Me.LockingLegende.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LockingLegende.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.LockingLegende.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.LockingLegende.Location = New System.Drawing.Point(-1, 111)
        Me.LockingLegende.Name = "LockingLegende"
        Me.LockingLegende.Size = New System.Drawing.Size(16, 18)
        Me.LockingLegende.TabIndex = 58
        Me.LockingLegende.Tag = ""
        '
        'LegendeTimer
        '
        Me.LegendeTimer.Interval = 1
        '
        'closeLegende
        '
        Me.closeLegende.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.closeLegende.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.closeLegende.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.closeLegende.Location = New System.Drawing.Point(497, -1)
        Me.closeLegende.Margin = New System.Windows.Forms.Padding(0)
        Me.closeLegende.Name = "closeLegende"
        Me.closeLegende.Size = New System.Drawing.Size(20, 20)
        Me.closeLegende.TabIndex = 218
        Me.closeLegende.Text = "X"
        Me.closeLegende.UseVisualStyleBackColor = True
        '
        'Legend
        '
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.closeLegende)
        Me.Controls.Add(Me.LockingLegende)
        Me.Name = "Legend"
        Me.Size = New System.Drawing.Size(536, 128)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private headerSpacing As Integer = 15
    Private _Caption As String = "Légende"
    'Private LockWindowImgs As ImageList
    Private _Locked As Boolean = False
    Private _CancelingMove As Boolean = False
    Private _StartingPosition, _EndingPosition As Integer
    Private _MovingSpeed As Integer = 1
    Private _Orientation As LegendeOrientation = LegendeOrientation.ScrollDown
    Private curOrientation As LegendeOrientation = LegendeOrientation.ScrollDown
    Private curFlecheTop As Integer = 120
    Private curFlecheLeft As Integer = 264
    'Private CurrentArrowIMG As Bitmap
    'Private OriginalArrowIMG As Bitmap
    'Private _MovingOverActivated As Boolean = False

    Private updatingOrientation As Boolean = False

    Private legendWidth As Integer = 0
    Private legendHeight As Integer = 0

    Private coordX As Integer
    Private coordY As Integer

    Private Const flecheSpacing As Integer = 2

    Public Enum LegendeOrientation
        ScrollDown = 0
        ScrollUp = 1
        ScrollRight = 2
        ScrollLeft = 3
    End Enum

#Region "Propriétés"
    Public Property scrollOrientation() As LegendeOrientation
        Get
            Return _Orientation
        End Get
        Set(ByVal Value As LegendeOrientation)
            'If OriginalArrowIMG Is Nothing Then Exit Property

            _Orientation = Value
            reArrangeObjects()

            'CurrentArrowIMG = OriginalArrowIMG.Clone
            Me.Padding = New Padding(0, headerSpacing, 0, LockingLegende.Height)

            Select Case Value
                Case LegendeOrientation.ScrollUp
                    Me.Padding = New Padding(0, headerSpacing, 0, 0)
                    '       CurrentArrowIMG.RotateFlip(RotateFlipType.RotateNoneFlipY)

                Case LegendeOrientation.ScrollLeft
                    Me.Padding = New Padding(LockingLegende.Width, headerSpacing, 0, 0)
                    '      CurrentArrowIMG.RotateFlip(RotateFlipType.Rotate90FlipNone)

                Case LegendeOrientation.ScrollRight
                    Me.Padding = New Padding(0, headerSpacing, LockingLegende.Width, 0)
                    '     CurrentArrowIMG.RotateFlip(RotateFlipType.Rotate270FlipNone)
            End Select
        End Set
    End Property
    Public Property locked() As Boolean
        Get
            Return _Locked
        End Get
        Set(ByVal Value As Boolean)
            _Locked = Value
        End Set
    End Property

    Public Property caption() As String
        Get
            Return _Caption
        End Get
        Set(ByVal Value As String)
            _Caption = Value
        End Set
    End Property

    Public Property bordered() As BorderStyle
        Get
            Return Me.BorderStyle
        End Get
        Set(ByVal Value As BorderStyle)
            Me.BorderStyle = Value
        End Set
    End Property

    Public Property startingPosition() As Integer
        Get
            Return _StartingPosition
        End Get
        Set(ByVal Value As Integer)
            _StartingPosition = Value
            Me.Top = Value
        End Set
    End Property

    Public Property endingPosition() As Integer
        Get
            Return _EndingPosition
        End Get
        Set(ByVal Value As Integer)
            _EndingPosition = Value
        End Set
    End Property

    Public Property movingSpeed() As Integer
        Get
            Return _MovingSpeed
        End Get
        Set(ByVal Value As Integer)
            _MovingSpeed = Value
        End Set
    End Property

    Public WriteOnly Property cancelingMove() As Boolean
        Set(ByVal Value As Boolean)
            _CancelingMove = Value
        End Set
    End Property
#End Region

    Public Sub showHiddenPart(Optional ByVal useAnimation As Boolean = True)
        If useAnimation Then
            locked = False
            LegendeTimer.Enabled = True
        Else
            Select Case _Orientation
                Case LegendeOrientation.ScrollDown
                    Me.Top = _EndingPosition

                Case LegendeOrientation.ScrollLeft
                    Me.Left = _EndingPosition

                Case LegendeOrientation.ScrollRight
                    Me.Left = _EndingPosition

                Case LegendeOrientation.ScrollUp
                    Me.Top = _EndingPosition
            End Select
        End If
    End Sub

    Private Sub reArrangeObjects()
        If legendWidth = 0 Or legendHeight = 0 Then Exit Sub

        updatingOrientation = True
        Select Case _Orientation
            Case LegendeOrientation.ScrollDown
                closeLegende.Top = 0
                closeLegende.Left = Me.Width - LockingLegende.Width - closeLegende.Width
                LockingLegende.Top = Me.Height - LockingLegende.Height
                LockingLegende.Left = 0

                curFlecheTop = Me.Height - 7 - flecheSpacing
                curFlecheLeft = (Me.Width - 7) / 2

            Case LegendeOrientation.ScrollLeft
                LockingLegende.Top = Me.Height - LockingLegende.Height
                LockingLegende.Left = -1
                closeLegende.Top = Me.Height - LockingLegende.Height - closeLegende.Height
                closeLegende.Left = -1

                curFlecheTop = (Me.Height - 7) / 2
                curFlecheLeft = flecheSpacing

            Case LegendeOrientation.ScrollRight
                closeLegende.Top = Me.Height - LockingLegende.Height - closeLegende.Height
                closeLegende.Left = Me.Width - LockingLegende.Width - closeLegende.Width
                LockingLegende.Top = Me.Height - LockingLegende.Height
                LockingLegende.Left = Me.Width - LockingLegende.Width

                curFlecheTop = (Me.Height - 7) / 2
                curFlecheLeft = Me.Width - 7 - flecheSpacing

            Case LegendeOrientation.ScrollUp
                closeLegende.Top = 0
                closeLegende.Left = Me.Width - LockingLegende.Width - closeLegende.Width
                LockingLegende.Top = 0
                LockingLegende.Left = Me.Width - LockingLegende.Width

                curFlecheTop = flecheSpacing
                curFlecheLeft = (Me.Width - 7) / 2
        End Select
        curOrientation = _Orientation
        updatingOrientation = False
    End Sub

    Private Sub calculateScreenCoord()
        'Calculate reel position of control
        coordX = 0
        coordY = 0
        Dim curParent As Object = Me.Parent
        If curParent Is Nothing Then Exit Sub
        Try
            While Not TypeOf curParent Is Form And curParent IsNot Nothing
                coordX += curParent.Location.X
                coordY += curParent.Location.Y
                curParent = curParent.Parent
            End While

            If Me.ParentForm IsNot Nothing Then
                Dim newCoord As Point
                Select Case Me._Orientation
                    Case LegendeOrientation.ScrollLeft
                        newCoord = Me.ParentForm.PointToScreen(New Point(Me.Location.X, 0))
                    Case LegendeOrientation.ScrollRight
                        newCoord = Me.ParentForm.PointToScreen(Me.Location)
                    Case LegendeOrientation.ScrollUp
                        newCoord = Me.ParentForm.PointToScreen(Me.Location)
                    Case Else
                        newCoord = Me.ParentForm.PointToScreen(New Point(Me.Location.X, 0))
                End Select
                coordX += newCoord.X
                coordY += newCoord.Y
            End If
        Catch 'REM Exception not handle
        End Try
    End Sub

    Private Sub legendes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click

    End Sub

    Private Sub legendes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        calculateScreenCoord()
        If Not Me.ParentForm Is Nothing Then AddHandler Me.ParentForm.LocationChanged, AddressOf legendes_LocationChanged
    End Sub

    Private Sub legendes_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        calculateScreenCoord()
    End Sub

    Private Sub legendes_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawString(Me.caption, Me.Font, Brushes.Black, 2, 2)
        e.Graphics.DrawLine(Pens.Black, 0, headerSpacing - 3, Me.Width, headerSpacing - 3)
        e.Graphics.DrawLine(Pens.Black, 0, headerSpacing - 1, Me.Width, headerSpacing - 1)
        e.Graphics.DrawLine(Pens.Black, 0, Me.LockingLegende.Top, Me.Width, Me.LockingLegende.Top)
        If Me.DesignMode = False Then
            Dim flecheBmp As Bitmap = DrawingManager.getInstance.getImage("DownArrow.jpg")
            If flecheBmp Is Nothing Then Exit Sub
            flecheBmp = flecheBmp.Clone()
            Select Case Me.scrollOrientation
                Case LegendeOrientation.ScrollUp
                    flecheBmp.RotateFlip(RotateFlipType.Rotate180FlipNone)

                Case LegendeOrientation.ScrollLeft
                    flecheBmp.RotateFlip(RotateFlipType.Rotate90FlipNone)

                Case LegendeOrientation.ScrollRight
                    flecheBmp.RotateFlip(RotateFlipType.Rotate270FlipNone)
            End Select
            e.Graphics.DrawImage(flecheBmp, curFlecheLeft, curFlecheTop)
            flecheBmp.Dispose()
        End If
    End Sub


    Private Sub legende_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If legendWidth = 0 Or legendHeight = 0 Then Exit Sub
        Dim differenceSize As Size = New Size(Me.Width - legendWidth, Me.Height - legendHeight)

        If updatingOrientation = False Then
            legendHeight += differenceSize.Height
            legendWidth += differenceSize.Width
            curFlecheTop += differenceSize.Height
            curFlecheLeft = (Me.Width - 7) / 2

            If _Orientation <> LegendeOrientation.ScrollDown Then reArrangeObjects()
            Me.Refresh()
        End If
    End Sub

    Private Sub legendes_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        objects_MouseLeave(sender, e)
    End Sub

    Public Sub objects_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        _CancelingMove = False

        If _CancelingMove = False And ((Me.Top <> _EndingPosition And (_Orientation = LegendeOrientation.ScrollDown Or _Orientation = LegendeOrientation.ScrollUp)) Or (Me.Left <> _EndingPosition And (_Orientation = LegendeOrientation.ScrollRight Or _Orientation = LegendeOrientation.ScrollLeft))) Then
            LegendeTimer.Enabled = True
        End If
    End Sub

    Public Sub objects_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        Dim MouseX, MouseY, CurCoordX, curCoordY As Integer
        Select Case _Orientation
            Case LegendeOrientation.ScrollDown
                CurCoordX = coordX + Me.Location.X + MyBase.Width - 1
                curCoordY = coordY + Me.Location.Y + MyBase.Height - 1
            Case LegendeOrientation.ScrollLeft
                CurCoordX = coordX + Me.Location.X + MyBase.Width - 1
                curCoordY = coordY + Me.Location.Y + MyBase.Height - 1
            Case LegendeOrientation.ScrollRight
                CurCoordX = coordX + Me.Location.X + MyBase.Width - 1
                curCoordY = coordY + Me.Location.Y + MyBase.Height - 1
            Case LegendeOrientation.ScrollUp
                CurCoordX = coordX + Me.Location.X + MyBase.Width - 1
                curCoordY = coordY + Me.Location.Y + MyBase.Height - 1
        End Select

        MouseX = Control.MousePosition.X
        MouseY = Control.MousePosition.Y

        If Not (MouseX > coordX And MouseX < CurCoordX And MouseY > coordY And MouseY < curCoordY) Then
            If LockingLegende.Tag.ToString <> "" Then
                locked = True
            Else
                LegendeTimer.Enabled = False
                If _Orientation = LegendeOrientation.ScrollDown Or _Orientation = LegendeOrientation.ScrollUp Then
                    Me.Top = _StartingPosition
                Else
                    Me.Left = _StartingPosition
                End If
            End If
        End If
    End Sub

    Public Sub objects_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub attachObjectsMouseEvents()
        Dim i As Integer
        For i = 0 To Me.Controls.Count - 1
            With Me.Controls(i)
                AddHandler .MouseMove, AddressOf objects_MouseMove
                Try
                    AddHandler .MouseEnter, AddressOf objects_MouseEnter
                Catch
                End Try
            End With
        Next i
    End Sub

    Public Sub detachObjectsMouseEvents()
        Dim i As Integer
        For i = 0 To Me.Controls.Count - 1
            With Me.Controls(i)
                RemoveHandler .MouseMove, AddressOf objects_MouseMove
                Try
                    RemoveHandler .MouseEnter, AddressOf objects_MouseEnter
                Catch
                End Try
            End With
        Next i
    End Sub

    Private Sub legendeTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LegendeTimer.Tick
        If locked = True Then Exit Sub

        Select Case _Orientation
            Case LegendeOrientation.ScrollDown
                Me.Top += _MovingSpeed
                If Me.Top >= _EndingPosition Then LegendeTimer.Enabled = False : Me.Top = _EndingPosition

            Case LegendeOrientation.ScrollLeft
                Me.Left -= _MovingSpeed
                If Me.Left <= _EndingPosition Then LegendeTimer.Enabled = False : Me.Left = _EndingPosition

            Case LegendeOrientation.ScrollRight
                Me.Left += _MovingSpeed
                If Me.Left >= _EndingPosition Then LegendeTimer.Enabled = False : Me.Left = _EndingPosition

            Case LegendeOrientation.ScrollUp
                Me.Top -= _MovingSpeed
                If Me.Top <= _EndingPosition Then LegendeTimer.Enabled = False : Me.Top = _EndingPosition
        End Select
    End Sub

    Private Sub lockingLegende_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockingLegende.Click
        If LockingLegende.Tag.ToString = "" Then
            LockingLegende.Image = DrawingManager.getInstance.getImage("lockwin10.gif") 'LockWindowImgs.Images(1)
            LockingLegende.Tag = "LOCKED"
            locked = True
        Else
            LockingLegende.Image = DrawingManager.getInstance.getImage("unlockwin10.gif") 'LockWindowImgs.Images(0)
            LockingLegende.Tag = ""
            locked = False
        End If
    End Sub

    Private Sub legendes_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        'Quitte si ce n'est pas sur l'entête ou la barre
        Dim accepted As Boolean = False
        Select Case Me.scrollOrientation
            Case LegendeOrientation.ScrollDown
                If (e.Y > (Me.Height - Me.LockingLegende.Height) Or e.Y <= headerSpacing) Then accepted = True
            Case LegendeOrientation.ScrollLeft
                If (e.X < Me.LockingLegende.Width Or e.Y <= headerSpacing) Then accepted = True
            Case LegendeOrientation.ScrollRight
                If (e.X > (Me.Width - Me.LockingLegende.Width) Or e.Y <= headerSpacing) Then accepted = True
            Case LegendeOrientation.ScrollUp
                If (e.Y <= Math.Max(headerSpacing, Me.LockingLegende.Height)) Then accepted = True
        End Select
        If accepted = False Then Return

        If locked Then
            locked = False
        Else
            locked = True
            Select Case Me.scrollOrientation
                Case LegendeOrientation.ScrollDown, LegendeOrientation.ScrollUp
                    Me.Top = Me.startingPosition

                Case LegendeOrientation.ScrollLeft, LegendeOrientation.ScrollRight
                    Me.Left = Me.startingPosition
            End Select
        End If
    End Sub

    Private Sub closeLegende_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeLegende.Click
        If locked Then
            locked = False
        Else
            locked = True
            Select Case Me.scrollOrientation
                Case LegendeOrientation.ScrollDown, LegendeOrientation.ScrollUp
                    Me.Top = Me.startingPosition

                Case LegendeOrientation.ScrollLeft, LegendeOrientation.ScrollRight
                    Me.Left = Me.startingPosition
            End Select
        End If
    End Sub
End Class
