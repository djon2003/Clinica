Public Class ListScrollBar

    Public Enum ListScrollBarOrientation
        Horizontal = 0
        Vertical = 1
    End Enum

    Private Enum ControlTypes
        ButtonFirst = 0
        ButtonSecond = 1
        Slider = 2
        Background = 3
    End Enum

    Private Sub New()
    End Sub

    Public Sub New(ByVal associatedList As List)
        _List = associatedList
    End Sub

    Public Event valueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event visibleChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event scroll(ByVal sender As Object, ByVal e As Windows.Forms.ScrollEventArgs)

#Region "Définitions"
    Private _Visible As Boolean = False
    Private _List As List
    Private _Value As Integer = 0
    Private _Minimum As Integer = 0
    Private _Maximum As Integer = 0
    Private _ButtonsBackcolor As Color = SystemColors.Control
    Private _ButtonsForecolor As Color = SystemColors.ControlText
    Private _FlatStyle As FlatStyle = Windows.Forms.FlatStyle.Popup
    Private _SliderColor As Color = SystemColors.Control
    Private _BackColor As Color = SystemColors.ScrollBar
    Private _Orientation As ListScrollBarOrientation = ListScrollBarOrientation.Horizontal
    Private _Height As Integer
    Private _Width As Integer
    Private _Left As Integer
    Private _Top As Integer

    Private buttonFirstMouseDown As Boolean = False
    Private buttonSecondMouseDown As Boolean = False
    Private buttonFirstMouseOver As Boolean = False
    Private buttonSecondMouseOver As Boolean = False
    Private sliderMouseDown As Boolean = False
    Private sliderMouseOver As Boolean = False
    Private backMouseDown As Boolean = False
    Private oldSliderLocation As Point
    Private oldValue As Integer
#End Region

#Region "Propriétés"

    Public Property visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            If value = _Visible Then Exit Property

            _Visible = value
            RaiseEvent visibleChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property height() As Integer
        Get
            Return _Height
        End Get
        Set(ByVal value As Integer)
            _Height = value
        End Set
    End Property

    Public Property width() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal value As Integer)
            _Width = value
        End Set
    End Property

    Public Property left() As Integer
        Get
            Return _Left
        End Get
        Set(ByVal value As Integer)
            _Left = value
        End Set
    End Property

    Public Property top() As Integer
        Get
            Return _Top
        End Get
        Set(ByVal value As Integer)
            _Top = value
        End Set
    End Property

    Public Property orientation() As ListScrollBarOrientation
        Get
            Return _Orientation
        End Get
        Set(ByVal value As ListScrollBarOrientation)
            _Orientation = value
        End Set
    End Property

    Public Property minimum() As Integer
        Get
            Return _Minimum
        End Get
        Set(ByVal value As Integer)
            _Minimum = value
        End Set
    End Property

    Public ReadOnly Property parent() As List
        Get
            Return _List
        End Get
    End Property


    Public Property maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            _Maximum = value
        End Set
    End Property

    Public Property value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            If value < _Minimum Then value = _Minimum
            If value > _Maximum Then value = _Maximum

            If value = _Value Then Exit Property

            If value < _Value Then RaiseEvent scroll(Me, New ScrollEventArgs(ScrollEventType.SmallDecrement, value))
            If value > _Value Then RaiseEvent scroll(Me, New ScrollEventArgs(ScrollEventType.SmallIncrement, value))

            _Value = value

            RaiseEvent valueChanged(Me, EventArgs.Empty)
        End Set
    End Property

    Public Property flatStyle() As FlatStyle
        Get
            Return _FlatStyle
        End Get
        Set(ByVal value As FlatStyle)
            _FlatStyle = value
        End Set
    End Property

    Public Property buttonsForecolor() As Color
        Get
            Return _ButtonsForecolor
        End Get
        Set(ByVal value As Color)
            _ButtonsForecolor = value
        End Set
    End Property
    Public Property sliderColor() As Color
        Get
            Return _SliderColor
        End Get
        Set(ByVal value As Color)
            _SliderColor = value
        End Set
    End Property
    Public Property backColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            _BackColor = value
        End Set
    End Property
    Public Property buttonsBackColor() As Color
        Get
            Return _ButtonsBackcolor
        End Get
        Set(ByVal value As Color)
            _ButtonsBackcolor = value
        End Set
    End Property
#End Region

    Private Sub paintBorder(ByVal g As Graphics, ByVal curRect As Rectangle, Optional ByVal isMouseDown As Boolean = False, Optional ByVal isMouseOver As Boolean = False)
        Select Case _FlatStyle
            Case Windows.Forms.FlatStyle.Flat
                g.DrawRectangle(IIf(isMouseDown, Pens.Black, Pens.DarkGray), curRect)
            Case Windows.Forms.FlatStyle.Standard
                paint3DBorder(g, curRect, isMouseDown)
            Case Windows.Forms.FlatStyle.Popup
                If isMouseDown Or isMouseOver Then
                    paint3DBorder(g, curRect, isMouseDown)
                Else
                    g.DrawRectangle(Pens.DarkGray, curRect)
                End If
        End Select
    End Sub

    Private Sub paint3DBorder(ByVal g As Graphics, ByVal curRect As Rectangle, Optional ByVal isMouseDown As Boolean = False)
        Dim FirstColor, secondColor As Color
        FirstColor = SystemColors.ControlLightLight
        secondColor = SystemColors.ControlDarkDark
        If isMouseDown Then FirstColor = secondColor : secondColor = SystemColors.ControlLightLight
        Dim firstPen As New Pen(FirstColor)
        Dim secondPen As New Pen(secondColor)

        For i As Byte = 0 To 1
            g.DrawLine(firstPen, curRect.X, curRect.Y, curRect.X, curRect.Y + curRect.Height)
            g.DrawLine(firstPen, curRect.X, curRect.Y, curRect.Right, curRect.Y)
            g.DrawLine(secondPen, curRect.Right, curRect.Y, curRect.Right, curRect.Bottom)
            g.DrawLine(secondPen, curRect.X, curRect.Bottom, curRect.Right, curRect.Bottom)
            curRect.Y += 1
            curRect.X += 1
            curRect.Width -= 2
            curRect.Height -= 2
            FirstColor = SystemColors.ControlLight
            secondColor = SystemColors.ControlDark
            If isMouseDown Then FirstColor = secondColor : secondColor = SystemColors.ControlLight
        Next i
        firstPen.Dispose()
        secondPen.Dispose()
    End Sub

    Private buttonFirstRect As New Rectangle(0, 0, 0, 0)
    Private buttonSecondRect As New Rectangle(0, 0, 0, 0)
    Private sliderRect As New Rectangle(0, 0, 0, 0)

    Public Sub onPaint(ByVal e As PaintEventArgs)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim backBrush As New Drawing2D.HatchBrush(Drawing2D.HatchStyle.Percent20, _ButtonsBackcolor, _BackColor)
        Dim borderPen As New Pen(_ButtonsBackcolor)
        e.Graphics.FillRectangle(backBrush, e.ClipRectangle)
        e.Graphics.DrawRectangle(borderPen, e.ClipRectangle)
        borderPen.Dispose()
        backBrush.Dispose()

        Dim buttonsSize As Size
        Dim barLength As Integer = 0
        Dim buttonBackBrush As New SolidBrush(_ButtonsBackcolor)
        Dim buttonForeBrush As New SolidBrush(_ButtonsForecolor)
        Dim firstTriangle() As Point
        Dim secondTriangle() As Point

        'Calcule les positions/grandeurs de chaque bouton selon l'orientation de la barre de défilement
        If _Orientation = ListScrollBarOrientation.Horizontal Then
            buttonsSize = New Size(IIf(e.ClipRectangle.Width < e.ClipRectangle.Height * 2, Math.Floor(e.ClipRectangle.Width / 2), e.ClipRectangle.Height), e.ClipRectangle.Height)
            barLength = e.ClipRectangle.Width - buttonsSize.Width * 2

            If barLength > 0 Then
                Dim sliderWidth As Integer = barLength
                If Me.maximum - Me.minimum > 0 Then
                    sliderWidth = Math.Ceiling(Math.Log10(Me.maximum - Me.minimum + 10) ^ -1 * barLength)
                End If

                sliderRect = New Rectangle(e.ClipRectangle.X + buttonsSize.Width + Math.Floor((Me.value * (barLength - sliderWidth) / IIf(Me.maximum = 0, 1, Me.maximum))), e.ClipRectangle.Y, sliderWidth, e.ClipRectangle.Height)
            End If
            buttonFirstRect = New Rectangle(New Point(e.ClipRectangle.X, e.ClipRectangle.Y), buttonsSize)
            firstTriangle = New Point() {New Point(e.ClipRectangle.X + buttonsSize.Width * 3 / 4 - 1, e.ClipRectangle.Y + buttonsSize.Height / 4), New Point(e.ClipRectangle.X + buttonsSize.Width / 4, e.ClipRectangle.Y + buttonsSize.Height / 2), New Point(e.ClipRectangle.X + buttonsSize.Width * 3 / 4 - 1, e.ClipRectangle.Y + buttonsSize.Height * 3 / 4)}
            If Me.height > 10 AndAlso buttonFirstMouseDown Then
                firstTriangle(0).Offset(1, 1)
                firstTriangle(1).Offset(1, 1)
                firstTriangle(2).Offset(1, 1)
            End If
            buttonSecondRect = New Rectangle(New Point(e.ClipRectangle.Width - buttonsSize.Width, e.ClipRectangle.Y), buttonsSize)
            secondTriangle = New Point() {New Point(e.ClipRectangle.X + (e.ClipRectangle.Width - buttonsSize.Width * 3 / 4), e.ClipRectangle.Y + buttonsSize.Height / 4), New Point(e.ClipRectangle.X + (e.ClipRectangle.Width - buttonsSize.Width / 4), e.ClipRectangle.Y + buttonsSize.Height / 2), New Point(e.ClipRectangle.X + (e.ClipRectangle.Width - buttonsSize.Width * 3 / 4), e.ClipRectangle.Y + buttonsSize.Height * 3 / 4)}
            If Me.height > 10 AndAlso buttonSecondMouseDown Then
                secondTriangle(0).Offset(1, 1)
                secondTriangle(1).Offset(1, 1)
                secondTriangle(2).Offset(1, 1)
            End If
        Else
            buttonsSize = New Size(e.ClipRectangle.Width, IIf(e.ClipRectangle.Height < e.ClipRectangle.Width * 2, Math.Floor(e.ClipRectangle.Height / 2), e.ClipRectangle.Width))
            barLength = e.ClipRectangle.Height - buttonsSize.Height * 2
            If barLength > 0 Then
                Dim sliderHeight As Integer = barLength
                If Me.maximum - Me.minimum > 0 Then
                    sliderHeight = Math.Ceiling(Math.Log10(Me.maximum - Me.minimum + 10) ^ -1 * barLength)
                End If

                sliderRect = New Rectangle(e.ClipRectangle.X, buttonsSize.Height + Math.Floor((Me.value * (barLength - sliderHeight) / IIf(Me.maximum = 0, 1, Me.maximum))), e.ClipRectangle.Width, sliderHeight)
            End If
            buttonFirstRect = New Rectangle(New Point(e.ClipRectangle.X, e.ClipRectangle.Y), buttonsSize)
            firstTriangle = New Point() {New Point(e.ClipRectangle.X + buttonsSize.Width / 4, e.ClipRectangle.Y + buttonsSize.Height * 3 / 4), New Point(e.ClipRectangle.X + buttonsSize.Width / 2, e.ClipRectangle.Y + buttonsSize.Height / 4), New Point(e.ClipRectangle.X + buttonsSize.Width * 3 / 4, e.ClipRectangle.Y + buttonsSize.Height * 3 / 4)}
            If Me.width > 10 AndAlso buttonFirstMouseDown Then
                firstTriangle(0).Offset(1, 1)
                firstTriangle(1).Offset(1, 1)
                firstTriangle(2).Offset(1, 1)
            End If
            buttonSecondRect = New Rectangle(New Point(e.ClipRectangle.X, e.ClipRectangle.Height - buttonsSize.Height), buttonsSize)
            secondTriangle = New Point() {New Point(e.ClipRectangle.X + buttonsSize.Width / 4, e.ClipRectangle.Y + (e.ClipRectangle.Height - buttonsSize.Height * 3 / 4)), New Point(e.ClipRectangle.X + buttonsSize.Width / 2, e.ClipRectangle.Y + (e.ClipRectangle.Height - buttonsSize.Height / 4)), New Point(e.ClipRectangle.X + buttonsSize.Width * 3 / 4, e.ClipRectangle.Y + (e.ClipRectangle.Height - buttonsSize.Height * 3 / 4))}
            If Me.width > 10 AndAlso buttonSecondMouseDown Then
                secondTriangle(0).Offset(1, 1)
                secondTriangle(1).Offset(1, 1)
                secondTriangle(2).Offset(1, 1)
            End If
        End If

        'Dessine les boutons
        e.Graphics.FillRectangle(buttonBackBrush, buttonFirstRect)
        paintBorder(e.Graphics, buttonFirstRect, buttonFirstMouseDown, buttonFirstMouseOver)
        e.Graphics.FillPolygon(buttonForeBrush, firstTriangle)

        e.Graphics.FillRectangle(buttonBackBrush, sliderRect)
        paintBorder(e.Graphics, sliderRect, sliderMouseDown, sliderMouseOver)

        e.Graphics.FillRectangle(buttonBackBrush, buttonSecondRect)
        paintBorder(e.Graphics, buttonSecondRect, buttonSecondMouseDown, buttonSecondMouseOver)
        e.Graphics.FillPolygon(buttonForeBrush, secondTriangle)

        'S'assure que la bordure de sélection soit au-dessus des autres
        If buttonFirstMouseDown Then
            paintBorder(e.Graphics, buttonFirstRect, buttonFirstMouseDown, buttonFirstMouseOver)
        ElseIf buttonSecondMouseDown Then
            paintBorder(e.Graphics, buttonSecondRect, buttonSecondMouseDown, buttonSecondMouseOver)
        ElseIf sliderMouseDown Then
            paintBorder(e.Graphics, sliderRect, sliderMouseDown, sliderMouseOver)
        End If

        buttonForeBrush.Dispose()
        buttonBackBrush.Dispose()
    End Sub

    Private Function isOverButtonFirst(ByVal x As Integer, ByVal y As Integer) As Boolean
        Return x >= buttonFirstRect.X AndAlso x <= buttonFirstRect.Right AndAlso y >= buttonFirstRect.Top AndAlso y <= buttonFirstRect.Bottom
    End Function

    Private Function isOverButtonSecond(ByVal x As Integer, ByVal y As Integer) As Boolean
        Return x >= buttonSecondRect.X AndAlso x <= buttonSecondRect.Right AndAlso y >= buttonSecondRect.Top AndAlso y <= buttonSecondRect.Bottom
    End Function

    Private Function isOverSlider(ByVal x As Integer, ByVal y As Integer) As Boolean
        Return x >= sliderRect.X AndAlso x <= sliderRect.Right AndAlso y >= sliderRect.Top AndAlso y <= sliderRect.Bottom
    End Function

    Private Function isMouseAlreadyDown(ByVal current As ControlTypes) As Boolean
        Select Case current
            Case ControlTypes.Background
                If backMouseDown Then Return False
            Case ControlTypes.ButtonFirst
                If buttonFirstMouseDown Then Return False
            Case ControlTypes.ButtonSecond
                If buttonSecondMouseDown Then Return False
            Case ControlTypes.Slider
                If sliderMouseDown Then Return False
        End Select

        Return backMouseDown Or buttonFirstMouseDown Or buttonSecondMouseDown Or sliderMouseDown
    End Function

    Private firstValueOnMouseDown As Integer = -1

    Public Sub onMouseDown(ByVal e As MouseEventArgs)
        If isMouseAlreadyDown(ControlTypes.ButtonFirst) = False AndAlso isOverButtonFirst(e.X, e.Y) Then
            If firstValueOnMouseDown = -1 Then firstValueOnMouseDown = value
            buttonFirstMouseDown = True
            value -= 2
        ElseIf isMouseAlreadyDown(ControlTypes.ButtonSecond) = False AndAlso isOverButtonSecond(e.X, e.Y) Then
            If firstValueOnMouseDown = -1 Then firstValueOnMouseDown = value
            buttonSecondMouseDown = True
            value += 2
        ElseIf isMouseAlreadyDown(ControlTypes.Slider) = False AndAlso isOverSlider(e.X, e.Y) Then
            If sliderMouseDown = False Then oldSliderLocation = e.Location : oldValue = value
            sliderMouseDown = True
        ElseIf isMouseAlreadyDown(ControlTypes.Background) = False Then
            If isOverSlider(e.X, e.Y) = True Then
                backMouseDown = False
            Else
                If firstValueOnMouseDown = -1 Then firstValueOnMouseDown = value
                backMouseDown = True
                If _Orientation = ListScrollBarOrientation.Horizontal Then
                    If e.X < sliderRect.Left Then
                        Me.value -= 10
                    Else
                        Me.value += 10
                    End If
                Else
                    If e.Y < sliderRect.Top Then
                        Me.value -= 10
                    Else
                        Me.value += 10
                    End If
                End If
            End If
        End If
        _List.Invalidate(True)
    End Sub
    Public Sub onMouseUp(ByVal e As MouseEventArgs)
        buttonFirstMouseDown = False
        buttonSecondMouseDown = False
        sliderMouseDown = False
        backMouseDown = False

        If firstValueOnMouseDown <> -1 AndAlso firstValueOnMouseDown <> value Then RaiseEvent scroll(Me, New ScrollEventArgs(ScrollEventType.EndScroll, value))
        firstValueOnMouseDown = -1

        _List.Invalidate(True)
    End Sub
    Public Sub onMouseMove(ByVal e As MouseEventArgs)
        buttonFirstMouseOver = False
        buttonSecondMouseOver = False
        sliderMouseOver = False

        If _FlatStyle = Windows.Forms.FlatStyle.Popup AndAlso isOverButtonFirst(e.X, e.Y) Then
            buttonFirstMouseOver = True
        ElseIf _FlatStyle = Windows.Forms.FlatStyle.Popup AndAlso isOverButtonSecond(e.X, e.Y) Then
            buttonSecondMouseOver = True
        ElseIf sliderMouseDown Then
            If _Orientation = ListScrollBarOrientation.Horizontal Then
                Me.value = oldValue + (e.X - oldSliderLocation.X) * (_Maximum - _Minimum) / (Me.width - Me.buttonFirstRect.Width - Me.buttonSecondRect.Width - sliderRect.Width)
            Else
                Me.value = oldValue + (e.Y - oldSliderLocation.Y) * (_Maximum - _Minimum) / (Me.height - Me.buttonSecondRect.Height - Me.buttonFirstRect.Height - sliderRect.Height)
            End If
        ElseIf isOverSlider(e.X, e.Y) Then
            sliderMouseOver = True
        ElseIf backMouseDown Then
        End If

        _List.Invalidate(True)
    End Sub
    Public Sub onMouseLeave(ByVal e As MouseEventArgs)
        buttonFirstMouseOver = False
        buttonSecondMouseOver = False
        sliderMouseOver = False
        _List.Invalidate(True)
    End Sub
End Class
