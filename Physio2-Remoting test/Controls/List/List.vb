Option Strict Off
Option Explicit On
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Net.Mail
Imports System.Xml
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports System.Xml.Xsl
Imports System.IO.IsolatedStorage
Imports System.Data
Imports System.Data.SqlClient

<Serializable()> _
Public Class List
    Inherits System.Windows.Forms.UserControl

    Private Class InnerPannel
        Inherits Panel

        Public Sub New()
            MyBase.New()

            Me.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, True)
            Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, True)
        End Sub
    End Class

#Region "Windows Form Designer generated code "
    Private Shared sharedInitDone As Boolean = False

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        If sharedInitDone = False Then
            sharedInitDone = True
            List.toolTip1 = New System.Windows.Forms.ToolTip()
            List.toolTip2 = New System.Windows.Forms.ToolTip()
            List.toolTip1.UseAnimation = False
            List.toolTip2.UseAnimation = False
            List.toolTip1.UseFading = False
            List.toolTip2.UseFading = False
        End If

        Me.VScroll_Renamed = New ListScrollBar(Me)
        Me.VScroll_Renamed.orientation = ListScrollBar.ListScrollBarOrientation.Vertical
        Me.VScroll_Renamed.width = 10

        Me.HScroll_Renamed = New ListScrollBar(Me)
        Me.HScroll_Renamed.height = 10

        userControl_Initialize()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
            _Items.Clear()
            If Not b Is Nothing Then
                b.Dispose()
                b = Nothing
            End If
            If Not g Is Nothing Then
                g.Dispose()
                g = Nothing
            End If
            _BaseFont.Dispose()
            If Me.icons IsNot Nothing AndAlso Me.icons.Count <> 0 Then
                For Each curIcon As Icon In icons
                    If curIcon IsNot Nothing Then curIcon = Nothing
                Next
            End If
        End If

        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Private Shared WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents borderPanel As InnerPannel

    Private Shared WithEvents toolTip2 As System.Windows.Forms.ToolTip

    Friend WithEvents VScroll_Renamed As ListScrollBar
    Friend WithEvents TimerMouseDown As System.Windows.Forms.Timer 'System.Windows.Forms.VScrollBar
    Friend WithEvents HScroll_Renamed As ListScrollBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.borderPanel = New Controls.List.InnerPannel
        Me.TimerMouseDown = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'BorderPanel
        '
        Me.borderPanel.Location = New System.Drawing.Point(0, 0)
        Me.borderPanel.Name = "BorderPanel"
        Me.borderPanel.Size = New System.Drawing.Size(96, 128)
        Me.borderPanel.TabIndex = 5
        '
        'List
        '
        Me.CausesValidation = False
        Me.Controls.Add(Me.borderPanel)
        Me.Name = "List"
        Me.Size = New System.Drawing.Size(114, 171)
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Definitions"
#Region "Events"
    Public Event itemValueAChange(ByVal noItem As Integer, ByVal oldValue As Object, ByVal newValue As Object)
    Public Event itemValueBChange(ByVal noItem As Integer, ByVal oldValue As Object, ByVal newValue As Object)
    Public Event itemCaptionChange(ByVal noItem As Integer, ByVal oldCaption As String, ByVal newCaption As String)
    Public Event baseFontBoldChange()
    Public Event borderColorChange()
    Public Event baseFontSizeChange()
    Public Event baseBackColorChange()
    Public Event baseFontItalicChange()
    Public Event vScrollingChange()
    Public Event objMaxWidthChange()
    Public Event objMinWidthChange()
    Public Event borderSelColorChange()
    Public Event objMaxHeightChange()
    Public Event objMinHeightChange()
    Public Event bgColorChange()
    Public Event hScrollingChange()
    Public Event autoSizeChange()
    Public Event selectedChange()
    Public Event selectedChangeByUser()
    Public Event baseForeColorChange()
    Public Event baseFontChange()
    Public Event clickEnabledChange(ByVal sender As Object, ByVal e As EventArgs)
    Public Event baseAlignmentChange()
    Public Event autoAdjustChange()
    Public Event baseFontUnderlineChange()
    Public Event margeChange()
    Public Event drawChange()
    Public Event sortedChange()
    Public Event baseFontStrikeThruChange()
    Public Event itemBorderChange()
    'Événements
    Event hScroll_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
    Event vScroll_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
    Event reDraw(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Event willReDraw(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Event enterFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Event exitFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Event hide_Renamed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Event show_Renamed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Shadows Event resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
    <System.Runtime.InteropServices.ProgId("ClickEventArgs_NET.ClickEventArgs")> Public NotInheritable Class ClickEventArgs
        Inherits System.EventArgs
        Public selectedItem As Integer
        Public button As Integer
        Public x As Single
        Public y As Single
        Public Sub New(ByRef selectedItem As Integer, ByRef button As Integer, ByRef x As Single, ByRef y As Single)
            MyBase.New()
            Me.selectedItem = selectedItem
            Me.button = button
            Me.x = x
            Me.y = y
        End Sub
    End Class
    Event itemClick(ByVal sender As System.Object, ByVal e As ClickEventArgs)
    <System.Runtime.InteropServices.ProgId("DblClickEventArgs_NET.DblClickEventArgs")> Public NotInheritable Class DblClickEventArgs
        Inherits System.EventArgs
        Public selectedItem As Integer
        Public button As Integer
        Public x As Single
        Public y As Single
        Public Sub New(ByRef selectedItem As Integer, ByRef button As Integer, ByRef x As Single, ByRef y As Single)
            MyBase.New()
            Me.selectedItem = selectedItem
            Me.button = button
            Me.x = x
            Me.y = y
        End Sub
    End Class
    Event dblClick(ByVal sender As System.Object, ByVal e As DblClickEventArgs)
    <System.Runtime.InteropServices.ProgId("MouseDownEventArgs_NET.MouseDownEventArgs")> Public NotInheritable Class MouseDownEventArgs
        Inherits System.EventArgs
        Public selectedItem As Integer
        Public button As Integer
        Public shift As Integer
        Public x As Single
        Public y As Single
        Public Sub New(ByRef selectedItem As Integer, ByRef button As Integer, ByRef shift As Integer, ByRef x As Single, ByRef y As Single)
            MyBase.New()
            Me.selectedItem = selectedItem
            Me.button = button
            Me.shift = shift
            Me.x = x
            Me.y = y
        End Sub
    End Class
    Shadows Event mouseDown(ByVal sender As System.Object, ByVal e As MouseDownEventArgs)
    Shadows Event mouseLeave(ByVal sender As Object, ByVal e As EventArgs)
    Shadows Event mouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    <System.Runtime.InteropServices.ProgId("MouseUpEventArgs_NET.MouseUpEventArgs")> Public NotInheritable Class MouseUpEventArgs
        Inherits System.EventArgs
        Public selectedItem As Integer
        Public button As Integer
        Public shift As Integer
        Public x As Single
        Public y As Single
        Public Sub New(ByRef selectedItem As Integer, ByRef button As Integer, ByRef shift As Integer, ByRef x As Single, ByRef y As Single)
            MyBase.New()
            Me.selectedItem = selectedItem
            Me.button = button
            Me.shift = shift
            Me.x = x
            Me.y = y
        End Sub
    End Class
    Shadows Event mouseUp(ByVal sender As System.Object, ByVal e As MouseUpEventArgs)
    <System.Runtime.InteropServices.ProgId("WillSelectEventArgs_NET.WillSelectEventArgs")> Public NotInheritable Class WillSelectEventArgs
        Inherits System.EventArgs
        Public selectedItem As Integer
        Public button As Integer
        Public x As Single
        Public y As Single
        Public cancel As Boolean = False
        Public Sub New(ByRef selectedItem As Integer, ByRef button As Integer, ByRef x As Single, ByRef y As Single, ByRef cancel As Boolean)
            MyBase.New()
            Me.selectedItem = selectedItem
            Me.button = button
            Me.x = x
            Me.y = y
        End Sub
    End Class
    Event willSelect(ByVal sender As System.Object, ByVal e As WillSelectEventArgs)
#End Region
#Region "Structure & Enum"
    Public Enum PosType
        Top = 0
        Middle = 1
        Bottom = 2
        BetterGuess = 4
    End Enum
    Public Enum ItemVisibility
        Fully = 4
        Partial_BelowList = 3
        Partial_UpperList = 2
        None_BelowList = 1
        None_UpperList = 0
    End Enum
    Public Enum PType
        Alignment1 = 0
        BackColor1 = 1
        Caption1 = 2
        Font1 = 3
        FontSize1 = 4
        ForeColor1 = 5
        ToolTipText1 = 6
        FontBold1 = 7
        FontItalic1 = 8
        FontStrikeThru1 = 9
        FontUnderline1 = 10
    End Enum
    Public Enum OTType
        Transparent = 0
        Opaque = 1
    End Enum
    Public Enum BSType
        NoBorder = 0
        FixedSingle = 1
        Fixed3D = 2
    End Enum
    Public Enum FindingType
        Caption = 0
        ToolTipText = 1
        ValueA = 2
        ValueB = 3
    End Enum
#End Region



    Private _extraWidth As Integer = 0
    Private isPlacingObjects As Boolean = False
    Private _BaseFont As New System.Drawing.Font("Ms sans sherif", 8)
    Private _BaseForeColor As System.Drawing.Color = Color.Black
    Private _BaseBackColor As System.Drawing.Color = Color.White
    Private _BaseAlign As IListItem.AlignmentType = IListItem.AlignmentType.LeftA
    Private _Icons As New Generic.List(Of Icon)
    Private _Items As New Generic.List(Of IListItem)
    Private EmptyItem, ttt As String
    Private minSpace As Single
    Private oldMarge As Integer
    Private AddedWidth, addedHeight As Single
    Private AAProperty, HASProperty, VASProperty, aaDid As Boolean
    Private DoDraw, modifNeeded As Boolean
    Private _MouseMove3D, _Do3D As Boolean
    Private BColor, bsColor As System.Drawing.Color
    Private MaxHeight, MaxWidth, MinWidth, MinHeight, gmWidth As Single
    Private IBorder, oldBorder As Single
    Private itemSelect As Integer
    Private _SelectMultiple As Boolean = False
    Private ItemSorted, itemReversed As Boolean
    Private cEnabled As Boolean
    Private g As Graphics
    Private b As Bitmap
    Private firstTime As Boolean = False
    Private _AutoKeyDownSelection As Boolean = True

    Private bStyle As BSType

    Private _DefaultIconsPosition As Icons.IconPositions = CI.Controls.Icons.IconPositions.AfterText

    'Scrollbars
    Private hs As Boolean
    Private vs As Boolean

    'Position & Boutton de la souris
    Private currentX As Single
    Private currentY As Single
    Private currentButton As Integer
    Private mSpeed As Integer
    Private previousMouseMove As Integer = -1
    Private sca As Boolean = False

    'List height & width
    Private _ListHeight, _ListWidth As Integer
#End Region

    Protected Overrides Function isInputKey(ByVal keyData As Keys) As Boolean
        If Not keyData = Keys.Tab Then Return True
    End Function

#Region "Propriétés"

    Public Property extraWidth() As Integer
        Get
            Return _extraWidth
        End Get
        Set(ByVal value As Integer)
            _extraWidth = value
        End Set
    End Property

    Public Property defaultIconsPosition() As Icons.IconPositions
        Get
            Return _DefaultIconsPosition
        End Get
        Set(ByVal value As Icons.IconPositions)
            _DefaultIconsPosition = value
        End Set
    End Property

    Private Property listHeight() As Integer
        Get
            Return _ListHeight
        End Get
        Set(ByVal value As Integer)
            _ListHeight = value
        End Set
    End Property
    Private Property listWidth() As Integer
        Get
            Return _ListWidth
        End Get
        Set(ByVal value As Integer)
            _ListWidth = value
        End Set
    End Property
    Private ReadOnly Property listTop() As Integer
        Get
            If VScroll_Renamed.maximum <> 0 Then Return -VScroll_Renamed.value

            Return 0
        End Get
    End Property
    Private ReadOnly Property listLeft() As Integer
        Get
            If HScroll_Renamed.visible Then Return -HScroll_Renamed.value

            Return 0
        End Get
    End Property

    Public Property icons() As Generic.List(Of Icon)
        Get
            If _Icons Is Nothing Then _Icons = New Generic.List(Of System.Drawing.Icon)

            Return _Icons
        End Get
        Set(ByVal Value As Generic.List(Of Icon))
            _Icons = Value
        End Set
    End Property

    Public Property autoKeyDownSelection() As Boolean
        Get
            Return _AutoKeyDownSelection
        End Get
        Set(ByVal Value As Boolean)
            _AutoKeyDownSelection = Value
        End Set
    End Property

    Public Property selectedClickAllowed() As Boolean
        Get
            Return sca
        End Get
        Set(ByVal Value As Boolean)
            sca = Value
        End Set
    End Property

    Public Property toolTipText() As String
        Get
            Return ttt
        End Get
        Set(ByVal Value As String)
            ttt = Value
            toolTip2.SetToolTip(borderPanel, ttt)
        End Set
    End Property

    Public Property reverseSorting() As Boolean
        Get
            Return itemReversed
        End Get
        Set(ByVal Value As Boolean)
            itemReversed = Value
        End Set
    End Property

    Public Property mouseMove3D() As Boolean
        Get
            Return _MouseMove3D
        End Get
        Set(ByVal Value As Boolean)
            _MouseMove3D = Value
            Me.HScroll_Renamed.flatStyle = IIf(_Do3D, FlatStyle.Standard, IIf(_MouseMove3D, FlatStyle.Popup, FlatStyle.Flat))
            Me.VScroll_Renamed.flatStyle = IIf(_Do3D, FlatStyle.Standard, IIf(_MouseMove3D, FlatStyle.Popup, FlatStyle.Flat))
        End Set
    End Property
    Public Property do3D() As Boolean
        Get
            do3D = _Do3D
        End Get
        Set(ByVal Value As Boolean)
            _Do3D = Value
            Me.HScroll_Renamed.flatStyle = IIf(_Do3D, FlatStyle.Standard, IIf(_MouseMove3D, FlatStyle.Popup, FlatStyle.Flat))
            Me.VScroll_Renamed.flatStyle = IIf(_Do3D, FlatStyle.Standard, IIf(_MouseMove3D, FlatStyle.Popup, FlatStyle.Flat))
            placeObjects(0, _Items.Count - 1)
        End Set
    End Property
    Public Property hScrollColor() As System.Drawing.Color
        Get
            hScrollColor = HScroll_Renamed.backColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            HScroll_Renamed.backColor = Value
        End Set
    End Property
    Public Property vScrollColor() As System.Drawing.Color
        Get
            vScrollColor = VScroll_Renamed.backColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            VScroll_Renamed.backColor = Value
        End Set
    End Property
    Public Property hScrollForeColor() As System.Drawing.Color
        Get
            hScrollForeColor = HScroll_Renamed.buttonsForecolor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            HScroll_Renamed.buttonsForecolor = Value
        End Set
    End Property
    Public Property vScrollForeColor() As System.Drawing.Color
        Get
            vScrollForeColor = VScroll_Renamed.buttonsForecolor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            VScroll_Renamed.buttonsForecolor = Value
        End Set
    End Property
    Public Property mouseSpeed() As Integer
        Get
            mouseSpeed = mSpeed
        End Get
        Set(ByVal Value As Integer)
            mSpeed = CInt(Value)
        End Set
    End Property
    Public Property autoSizeHorizontally() As Boolean
        Get
            Return HASProperty
        End Get
        Set(ByVal Value As Boolean)
            Dim beforeW As Single
            HASProperty = Value

            If HASProperty = True Then
                beforeW = borderPanel.ClientRectangle.Width
                placeObjects(1, 0)
                If beforeW <> borderPanel.ClientRectangle.Width Then placeObjects(findFirstItem, findLastItem)
            End If

            RaiseEvent autoSizeChange()
        End Set
    End Property
    Public Property autoSizeVertically() As Boolean
        Get
            Return VASProperty
        End Get
        Set(ByVal Value As Boolean)
            Dim beforeW As Single
            VASProperty = Value

            If VASProperty = True Then
                beforeW = borderPanel.ClientRectangle.Width
                placeObjects(1, 0)
                If beforeW <> borderPanel.ClientRectangle.Width Then placeObjects(findFirstItem, findLastItem)
            End If

            RaiseEvent autoSizeChange()
        End Set
    End Property
    Public Property objMinWidth() As Single
        Get
            Return MinWidth
        End Get
        Set(ByVal Value As Single)
            MinWidth = Value
            RaiseEvent objMinWidthChange()
        End Set
    End Property
    Public Property objMinHeight() As Single
        Get
            Return MinHeight
        End Get
        Set(ByVal Value As Single)
            MinHeight = Value
            RaiseEvent objMinHeightChange()
        End Set
    End Property
    Public Property objMaxWidth() As Single
        Get
            objMaxWidth = MaxWidth
        End Get
        Set(ByVal Value As Single)
            MaxWidth = Value
            RaiseEvent objMaxWidthChange()
        End Set
    End Property
    Public Property objMaxHeight() As Single
        Get
            objMaxHeight = MaxHeight
        End Get
        Set(ByVal Value As Single)
            MaxHeight = Value
            RaiseEvent objMaxHeightChange()
        End Set
    End Property
    Public Property selectMultiple() As Boolean
        Get
            Return _SelectMultiple
        End Get
        Set(ByVal value As Boolean)
            _SelectMultiple = value
        End Set
    End Property
    Public Property selected() As Integer
        Get
            selected = itemSelect
        End Get
        Set(ByVal Value As Integer)
            If Value < -1 Or Value > (_Items.Count - 1) Or _Items.Count = 0 Then Exit Property

            Dim is2 As Integer = itemSelect
            Dim pmm As Integer = previousMouseMove
            If pmm > -1 Then pmm += 1
            Dim MinDraw, maxDraw As Integer
            MinDraw = 32000 : maxDraw = -1
            Dim curMousePos As Point = Me.PointToClient(Control.MousePosition)

            Dim force3D As Boolean = mouseMove3D AndAlso Me.mapItem(curMousePos.X + hsValue, curMousePos.Y + vsValue) = Value AndAlso curMousePos.X <= Me.Width AndAlso curMousePos.Y <= Me.Height
            If do3D = True Then force3D = False

            REM Software crashed totally with StackOverFlow error
            Dim wsEventArgs As New WillSelectEventArgs(Value, currentButton, currentX, currentY, False)
            RaiseEvent willSelect(Me, wsEventArgs)

            If wsEventArgs.cancel = True Then Exit Property 'Canceling modif

            Dim hasToRedraw As Boolean = False
            If selectMultiple = False OrElse (Control.ModifierKeys <> Keys.Control AndAlso (currentButton <> 2 OrElse (Value <> -1 AndAlso _Items(Value).IsSelected = False AndAlso Control.ModifierKeys <> Keys.ControlKey)) AndAlso itemSelect <> -1) Then hasToRedraw = innerClearSelection() > 1
            If Value <> -1 Then _Items(Value).IsSelected = True

            itemSelect = Value

            If hasToRedraw Then 'S'il y avait une sélection multiple, on redessine tout
                forceRedraw()
                RaiseEvent selectedChange()
                Exit Property
            End If

            g = Graphics.FromImage(b)

            If selectMultiple = False OrElse Control.ModifierKeys <> Keys.Control Then
                If is2 > -1 And is2 < _Items.Count Then
                    MinDraw = Math.Min(MinDraw, is2)
                    maxDraw = Math.Max(maxDraw, is2)
                    drawAll(is2)
                End If
                If pmm > -1 And pmm < _Items.Count Then
                    MinDraw = Math.Min(MinDraw, pmm)
                    maxDraw = Math.Max(maxDraw, pmm)
                    drawAll(pmm)
                End If
            End If
            If Value > -1 Then
                MinDraw = Math.Min(MinDraw, Value)
                maxDraw = Math.Max(maxDraw, Value)
                drawAll(Value, force3D)
            End If

            If maxDraw <> -1 And MinDraw <> 32000 Then
                'Me.Refresh()
                Me.Invalidate(getDrawingRectangle(MinDraw, maxDraw), True)
            Else
                'Me.Invalidate(True)
                Me.Refresh()
            End If

            g.Dispose()
            RaiseEvent selectedChange()
        End Set
    End Property


    Public Property itemMargin() As Integer
        Get
            Return minSpace
        End Get
        Set(ByVal Value As Integer)
            If Value = minSpace Then Exit Property

            minSpace = Value
            If findFirstItem() > -1 Then
                placeObjects(0, _Items.Count - 1)
                RaiseEvent margeChange()
            End If
        End Set
    End Property
    Public Property itemBorder() As Integer
        Get
            Return IBorder
        End Get
        Set(ByVal Value As Integer)
            If IBorder = Value Then Exit Property

            IBorder = Value
            If findFirstItem() > -1 Then
                placeObjects(0, _Items.Count - 1)
                RaiseEvent itemBorderChange()
            End If
        End Set
    End Property
    Public Property vScrolling() As Boolean
        Get
            vScrolling = vs
        End Get
        Set(ByVal Value As Boolean)
            If Value = vs Then Exit Property

            vs = Value

            placeObjects(0, _Items.Count - 1)
            RaiseEvent vScrollingChange()
        End Set
    End Property
    Public Property hScrolling() As Boolean
        Get
            hScrolling = hs
        End Get
        Set(ByVal Value As Boolean)
            If hs = Value Then Exit Property

            hs = Value

            placeObjects(1, 0)
            RaiseEvent hScrollingChange()
        End Set
    End Property
    Public Property baseAlignment() As IListItem.AlignmentType
        Get
            Return _BaseAlign
        End Get
        Set(ByVal Value As IListItem.AlignmentType)
            If _BaseAlign = Value Then Exit Property

            _BaseAlign = Value
            RaiseEvent baseAlignmentChange()
        End Set
    End Property
    Public Property baseBackColor() As System.Drawing.Color
        Get
            Return _BaseBackColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If _BaseBackColor = Value Then Exit Property

            _BaseBackColor = Value
            RaiseEvent baseBackColorChange()
        End Set
    End Property
    Public Property baseForeColor() As System.Drawing.Color
        Get
            Return _BaseForeColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If _BaseForeColor = Value Then Exit Property

            _BaseForeColor = Value
            RaiseEvent baseForeColorChange()
        End Set
    End Property
    Public Property baseFont() As System.Drawing.Font
        Get
            Return _BaseFont
        End Get
        Set(ByVal Value As System.Drawing.Font)
            If _BaseFont.Equals(Value) Then Exit Property

            _BaseFont = Value
            RaiseEvent baseFontChange()
        End Set
    End Property
    Public Property autoAdjust() As Boolean
        Get
            Return AAProperty
        End Get
        Set(ByVal Value As Boolean)
            If AAProperty = Value Then Exit Property

            AAProperty = Value
            RaiseEvent autoAdjustChange()
        End Set
    End Property
    Public Property borderColor() As System.Drawing.Color
        Get
            borderColor = BColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If BColor = Value Then Exit Property

            BColor = Value
            RaiseEvent borderColorChange()
        End Set
    End Property
    Public Property borderSelColor() As System.Drawing.Color
        Get
            borderSelColor = bsColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If bsColor = Value Then Exit Property

            bsColor = Value
            RaiseEvent borderSelColorChange()
        End Set
    End Property
    Public Property draw() As Boolean
        Get
            draw = DoDraw
        End Get
        Set(ByVal Value As Boolean)
            DoDraw = Value

            If DoDraw = True And modifNeeded = True Then placeObjects(0, _Items.Count - 1)
            RaiseEvent drawChange()
        End Set
    End Property
    Public Property items() As Generic.List(Of IListItem)
        Get
            Return _Items
        End Get
        Set(ByVal value As Generic.List(Of IListItem))
            If value Is Nothing Then
                _Items = New Generic.List(Of IListItem)
            Else
                _Items = value
            End If
        End Set
    End Property
    Private Function matchingSelected(ByVal obj As CI.Controls.IListItem) As Boolean
        Return obj.IsSelected
    End Function
    Public ReadOnly Property selectedItem() As IListItem
        Get
            If selected = -1 OrElse _Items.Count <= selected Then Return Nothing

            Return _Items(selected)
        End Get
    End Property
    Public ReadOnly Property selectedItems() As Generic.List(Of IListItem)
        Get
            Return _Items.FindAll(New Predicate(Of CI.Controls.IListItem)(AddressOf matchingSelected))
        End Get
    End Property
    Public Property vsValue() As Integer
        Get
            Return VScroll_Renamed.value
        End Get
        Set(ByVal Value As Integer)
            If VScroll_Renamed.visible = False OrElse VScroll_Renamed.value = Value Then Exit Property

            VScroll_Renamed.value = Value
        End Set
    End Property
    Public ReadOnly Property vsValueMax() As Integer
        Get
            Return VScroll_Renamed.maximum
        End Get
    End Property
    Public Property hsValue() As Integer
        Get
            hsValue = HScroll_Renamed.value
        End Get
        Set(ByVal Value As Integer)
            If HScroll_Renamed.visible = False OrElse HScroll_Renamed.value = Value Then Exit Property

            HScroll_Renamed.value = Value
        End Set
    End Property
    Public ReadOnly Property hsValueMax() As Integer
        Get
            Return VScroll_Renamed.maximum
        End Get
    End Property
    Public Shadows Property borderStyle() As BSType
        Get
            borderStyle = bStyle
        End Get
        Set(ByVal Value As BSType)
            bStyle = Value
            borderPanel.BorderStyle = Value
        End Set
    End Property
    Public Property bgColor() As System.Drawing.Color
        Get
            bgColor = borderPanel.BackColor
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If Value.Equals(Value) Then Exit Property

            borderPanel.BackColor = Value
            MyBase.BackColor = Value
            RaiseEvent bgColorChange()
        End Set
    End Property
    Public Property clickEnabled() As Boolean
        Get
            clickEnabled = cEnabled
        End Get
        Set(ByVal Value As Boolean)
            If cEnabled = Value Then Exit Property

            cEnabled = Value
            RaiseEvent clickEnabledChange(Me, New EventArgs())
        End Set
    End Property
    Public Property sorted() As Boolean
        Get
            sorted = ItemSorted
        End Get
        Set(ByVal Value As Boolean)
            If ItemSorted = Value Then Exit Property

            ItemSorted = Value
            If Value = True Then placeObjects(0, _Items.Count - 1)
            RaiseEvent sortedChange()
        End Set
    End Property
#End Region

#Region "Événements"

    Private lastSelectedFromKeyDownFind As Integer = -1
    Private lastWordFromKeyDownFind As String = ""
    Private lastTimeSelectedKDF As Long = 0

    Protected Overrides Sub onKeyDown(ByVal e As Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.Handled = True Then Exit Sub
        If e.KeyCode = Keys.ControlKey Then Exit Sub

        Dim keyCode As Integer = e.KeyCode
        Dim shift As Integer = e.Shift
        Dim noItem As Integer
        noItem = selected
        If noItem < 0 Then Exit Sub
        If _Items(noItem).Text Is Nothing Then Exit Sub

        Select Case keyCode
            Case 35 'END
                Me.lastWordFromKeyDownFind = ""
                Me.lastTimeSelectedKDF = 0
                Me.lastSelectedFromKeyDownFind = -1
                If autoKeyDownSelection = True Then
                    If findLastItem() > -1 Then
                        Me.selected = findLastItem()
                        RaiseEvent selectedChangeByUser()
                    End If
                End If

            Case 36 'HOME
                Me.lastWordFromKeyDownFind = ""
                Me.lastTimeSelectedKDF = 0
                Me.lastSelectedFromKeyDownFind = -1
                If autoKeyDownSelection = True Then
                    If findFirstItem() > -1 Then
                        Me.selected = findFirstItem()
                        RaiseEvent selectedChangeByUser()
                    End If
                End If

            Case 37 'LEFT
                Me.hsValue = hsValue - 1

            Case 38 'UP
                Me.lastWordFromKeyDownFind = ""
                Me.lastTimeSelectedKDF = 0
                Me.lastSelectedFromKeyDownFind = -1
                If autoKeyDownSelection = True Then
                    If findPreviousItem(noItem, True) > -1 Then
                        Me.selected = findPreviousItem(noItem, True)
                        RaiseEvent selectedChangeByUser()
                    End If
                End If

            Case 39 'RIGHT
                Me.hsValue = hsValue + 1

            Case 40 'DOWN
                Me.lastWordFromKeyDownFind = ""
                Me.lastTimeSelectedKDF = 0
                Me.lastSelectedFromKeyDownFind = -1
                If autoKeyDownSelection = True Then
                    If findNextItem(noItem, True) > -1 Then
                        Me.selected = findNextItem(noItem, True)
                        RaiseEvent selectedChangeByUser()
                    End If
                End If

            Case Else
                'Sélectionner tout
                If Me.selectMultiple AndAlso e.KeyCode = Keys.A AndAlso e.Control Then
                    For Each curItem As IListItem In _Items
                        curItem.IsSelected = True
                    Next

                    RaiseEvent selectedChangeByUser()
                    Exit Select
                End If

                Dim keyString As String = ""
                If e.KeyCode >= 48 And e.KeyCode <= 57 Then keyString = (e.KeyCode - 48).ToString
                If e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9 Then keyString = (e.KeyCode - 96).ToString
                If e.KeyCode = Keys.OemQuestion Then keyString = "e"
                If keyString = "" Then keyString = e.KeyCode.ToString


                REM Shall fix some problems before activating.. like accents, but more.. not necessarly finding first in the list (if the list has been sorted)
                'Finding item from keys typed
                If keyString.Length = 1 Then
                    If Me.lastTimeSelectedKDF <> 0 AndAlso Date.Now.Subtract((New Date(Me.lastTimeSelectedKDF))).Milliseconds > 250 Then
                        Me.lastWordFromKeyDownFind = ""
                        Me.lastTimeSelectedKDF = 0
                        Me.lastSelectedFromKeyDownFind = -1
                    End If
                    If Me.lastSelectedFromKeyDownFind = -1 Or Me.lastSelectedFromKeyDownFind <> Me.selected Then Me.lastWordFromKeyDownFind = ""

                    Dim found As Integer = -1
                    If Me.lastWordFromKeyDownFind <> "" AndAlso Me.ItemText(Me.selected).StartsWith(Me.lastWordFromKeyDownFind & keyString, StringComparison.OrdinalIgnoreCase) Then found = Me.selected

                    If found = -1 Then found = Me.findString(lastWordFromKeyDownFind & keyString, False, , True, IIf(Me.lastTimeSelectedKDF <> 0, Me.lastSelectedFromKeyDownFind, Me.selected + 1), True)
                    'OrElse (found = Me.Selected AndAlso found <> -1)
                    If lastSelectedFromKeyDownFind <> -1 AndAlso (found = -1) AndAlso Me.ItemText(Me.selected + 1).StartsWith(lastWordFromKeyDownFind & keyString, StringComparison.OrdinalIgnoreCase) Then
                        found = Me.selected + 1
                    Else
                        lastWordFromKeyDownFind &= keyString
                    End If

                    If found = -1 AndAlso Me.ItemText(Me.selected).StartsWith(lastWordFromKeyDownFind) = False Then
                        Me.lastWordFromKeyDownFind = keyString
                        found = Me.findString(lastWordFromKeyDownFind, False, , True)
                    End If

                    If found <> -1 Then
                        Me.lastTimeSelectedKDF = Date.Now.Ticks
                        Me.lastSelectedFromKeyDownFind = found
                        Me.selected = found
                        RaiseEvent selectedChangeByUser()
                    End If
                End If
        End Select

        If autoKeyDownSelection = True Then showItem(selected)
    End Sub

    Private Sub list_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick
        'Ne propage pas l'événement si le double s'est fait sur une des barres de défilement
        If isOverVScroll(lastMouseEvent.X, lastMouseEvent.Y) Or isOverHScroll(lastMouseEvent.X, lastMouseEvent.Y) Then Exit Sub

        RaiseEvent dblClick(Me, New DblClickEventArgs(selected, currentButton, currentX, currentY))
    End Sub

    Private Sub borderPanel_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles borderPanel.DoubleClick
        'Ne propage pas l'événement si le double s'est fait sur une des barres de défilement
        If isOverVScroll(lastMouseEvent.X, lastMouseEvent.Y) Or isOverHScroll(lastMouseEvent.X, lastMouseEvent.Y) Then Exit Sub

        RaiseEvent dblClick(Me, New DblClickEventArgs(selected, currentButton, currentX, currentY))
    End Sub

    Private Sub borderPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles borderPanel.Click
        If Me.Focused = False Then Me.Focus()

    End Sub

    Private Sub list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click
        borderPanel_Click(sender, e)
    End Sub

    Private Sub vScroll_Renamed_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScroll_Renamed.valueChanged
        Me.Invalidate(True)
        RaiseEvent vScroll_Scroll(sender, New ScrollEventArgs(ScrollEventType.EndScroll, VScroll_Renamed.value))
    End Sub

    Private Sub list_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        If CStr(eventSender.name).ToLower <> "borderpanel" And borderPanel.Size <> Me.Size Then borderPanel.Size = Me.Size
        If autoAdjust = True And autoSizeHorizontally = False And autoSizeVertically = False Then placeObjects(0, _Items.Count - 1)
        RaiseEvent resize(Me, Nothing)
    End Sub

    Private Sub userControl_Show()
        RaiseEvent show_Renamed(Me, Nothing)
    End Sub

    Private Function isOverVScroll(ByVal x As Integer, ByVal y As Integer) As Boolean
        Return x >= vScrollRectangle.X AndAlso x <= vScrollRectangle.Right AndAlso y >= vScrollRectangle.Top AndAlso y <= vScrollRectangle.Bottom
    End Function

    Private Function isOverHScroll(ByVal x As Integer, ByVal y As Integer) As Boolean
        Return x >= hScrollRectangle.X AndAlso x <= hScrollRectangle.Right AndAlso y >= hScrollRectangle.Top AndAlso y <= hScrollRectangle.Bottom
    End Function

    Private lastMouseEvent As MouseEventArgs
    Private isVScrollMouseDown As Boolean = False
    Private isHScrollMouseDown As Boolean = False

    Private Sub borderPanel_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles borderPanel.MouseDown
        lastMouseEvent = eventArgs
        If isOverVScroll(eventArgs.X, eventArgs.Y) Then
            isVScrollMouseDown = True
            VScroll_Renamed.onMouseDown(eventArgs)
            TimerMouseDown.Start()
            Exit Sub
        End If
        If isOverHScroll(eventArgs.X, eventArgs.Y) Then
            isHScrollMouseDown = True
            HScroll_Renamed.onMouseDown(eventArgs)
            TimerMouseDown.Start()
            Exit Sub
        End If

        Dim button As Integer = eventArgs.Button \ &H100000
        Dim shift As Integer = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = eventArgs.X
        Dim y As Single = eventArgs.Y
        currentButton = button
        RaiseEvent mouseDown(Me, New MouseDownEventArgs(mapItem(currentX, currentY), button, shift, x, y))
    End Sub

    Private Sub borderPanel_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles borderPanel.MouseUp
        lastMouseEvent = eventArgs

        VScroll_Renamed.onMouseUp(eventArgs)
        HScroll_Renamed.onMouseUp(eventArgs)

        If isOverVScroll(eventArgs.X, eventArgs.Y) Then
            isVScrollMouseDown = False
            TimerMouseDown.Stop()
            Exit Sub
        End If
        If isOverHScroll(eventArgs.X, eventArgs.Y) Then
            isHScrollMouseDown = False
            TimerMouseDown.Stop()
            Exit Sub
        End If

        Dim button As Integer = eventArgs.Button \ &H100000
        Dim shift As Integer = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = eventArgs.X
        Dim y As Single = eventArgs.Y
        currentButton = button
        RaiseEvent mouseUp(Me, New MouseUpEventArgs(mapItem(currentX, currentY), button, shift, x, y))

        Dim selectedItem As Integer = mapItem(currentX, currentY)
        If selectedItem = -1 Then Exit Sub
        If _Items(selectedItem).Clickable = False Then Exit Sub

        RaiseEvent itemClick(Me, New ClickEventArgs(selectedItem, currentButton, currentX, currentY))
        If clickEnabled = True AndAlso (_Items(selectedItem).IsSelected = False OrElse eventArgs.Button <> Windows.Forms.MouseButtons.Right) Then
            If selectedItem = selected And selectedClickAllowed = False Then Exit Sub

            Me.selected = selectedItem
            RaiseEvent selectedChangeByUser()
        End If

        currentButton = 0
    End Sub
    Private Sub list_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Enter
        RaiseEvent enterFocus(Me, Nothing)
    End Sub

    Private Sub list_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Leave
        RaiseEvent exitFocus(Me, Nothing)
    End Sub
    Private Sub userControl_Hide()
        RaiseEvent hide_Renamed(Me, Nothing)
    End Sub

#End Region


#Region "Internal functions"
    Public Shadows ReadOnly Property clientRectangle() As Rectangle
        Get
            Dim curRect As Rectangle = MyBase.ClientRectangle
            Select Case borderStyle
                Case BSType.Fixed3D
                    curRect.X += 2
                    curRect.Y += 2
                    curRect.Width -= 4
                    curRect.Height -= 4
                Case BSType.FixedSingle
                    curRect.X += 1
                    curRect.Y += 1
                    curRect.Width -= 2
                    curRect.Height -= 2
                Case Else
            End Select

            Return curRect
        End Get
    End Property

    Private Sub borderPanel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles borderPanel.Paint
        If Not IsNothing(b) Then e.Graphics.DrawImage(b, e.ClipRectangle, Math.Abs(listLeft) + e.ClipRectangle.Left, Math.Abs(listTop) + e.ClipRectangle.Top, e.ClipRectangle.Width, e.ClipRectangle.Height, Drawing.GraphicsUnit.Pixel)

        If VScroll_Renamed.visible Then VScroll_Renamed.onPaint(New PaintEventArgs(e.Graphics, vScrollRectangle))
        If HScroll_Renamed.visible Then HScroll_Renamed.onPaint(New PaintEventArgs(e.Graphics, hScrollRectangle))
    End Sub

    Private vscrollOver As Boolean = False
    Private hscrollOver As Boolean = False

    Private Sub borderPanel_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles borderPanel.MouseMove
        lastMouseEvent = e

        Dim Index1, PMMBefore, PMMAfter, MinDraw, maxDraw As Integer
        PMMBefore = -1 : PMMAfter = -1 : MinDraw = 32000 : maxDraw = -1
        currentX = e.X + Math.Abs(listLeft)
        currentY = e.Y + Math.Abs(listTop)

        VScroll_Renamed.onMouseMove(e)
        HScroll_Renamed.onMouseMove(e)

        If isOverVScroll(e.X, e.Y) Then
            vscrollOver = True
            If isVScrollMouseDown Then lastMouseEvent = e
            toolTip1.Active = False
            toolTip1.Active = False
            Exit Sub
        ElseIf vscrollOver Then
            VScroll_Renamed.onMouseLeave(e)
        End If
        If isOverHScroll(e.X, e.Y) Then
            hscrollOver = True
            If isHScrollMouseDown Then lastMouseEvent = e
            toolTip1.Active = False
            toolTip1.Active = False
            Exit Sub
        ElseIf hscrollOver Then
            HScroll_Renamed.onMouseLeave(e)
        End If

        toolTip1.Active = True
        toolTip1.Active = True

        Index1 = mapItem(currentX, currentY)
        If mouseMove3D = True And do3D = False And (previousMouseMove <> Index1) Then
            g = Graphics.FromImage(b)
            'Erase previous move
            If previousMouseMove > -1 Then
                If listCount() = 0 Then g.Clear(bgColor)

                PMMBefore = previousMouseMove - 1
                PMMAfter = previousMouseMove + 1
                If PMMBefore > -1 Then
                    MinDraw = Math.Min(MinDraw, PMMBefore)
                    maxDraw = Math.Max(maxDraw, PMMBefore)
                    drawAll(PMMBefore)
                End If
                drawAll(previousMouseMove)
                MinDraw = Math.Min(MinDraw, previousMouseMove)
                maxDraw = Math.Max(maxDraw, previousMouseMove)
                If PMMAfter > -1 Then
                    MinDraw = Math.Min(MinDraw, PMMAfter)
                    maxDraw = Math.Max(maxDraw, PMMAfter)
                    drawAll(PMMAfter)
                End If

                If selected > -1 Then
                    For i As Integer = 0 To _Items.Count - 1
                        If _Items(i).IsSelected Then drawAll(i)
                    Next i
                End If
            End If
            'Draw current move
            If Index1 > -1 Then
                MinDraw = Math.Min(MinDraw, Index1)
                MinDraw = Math.Min(MinDraw, findPreviousItem(Index1))
                maxDraw = Math.Max(maxDraw, Index1)
                Dim startingPt As Integer = 0
                If Index1 = Me.selected Then startingPt = Convert.ToInt16(System.Math.Ceiling(itemBorder / 2))
                drawAll(Index1, True)
            End If

            g.Dispose()
            Me.Invalidate(getDrawingRectangle(MinDraw, maxDraw), False)
            previousMouseMove = Index1
        End If
        If Index1 > -1 Then toolTip1.SetToolTip(borderPanel, _Items(Index1).ToolTipText)

        If ttt <> "" Then toolTip2.SetToolTip(borderPanel, ttt)

        RaiseEvent mouseMove(sender, e)
    End Sub

    Private Sub list_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseWheel
        If VScroll_Renamed.visible = False Then Exit Sub
        If e.Delta > 0 Then
            vsValue -= mouseSpeed
        Else
            vsValue += mouseSpeed
        End If
    End Sub

    Private Sub borderPanel_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles borderPanel.Resize
        MyBase.Size = borderPanel.Size
    End Sub

    Private Sub list_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave
        borderPanel_MouseMove(sender, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.None, 0, -1, -1, 0))
        RaiseEvent mouseLeave(sender, e)
    End Sub

    Private Sub borderPanel_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles borderPanel.MouseLeave
        borderPanel_MouseMove(sender, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.None, 0, -1, -1, 0))
        isVScrollMouseDown = False
        isHScrollMouseDown = False
        TimerMouseDown.Stop()
        RaiseEvent mouseLeave(sender, e)
    End Sub

    Private Sub list_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        If Me.Visible = False Then
            userControl_Hide()
        Else
            userControl_Show()
        End If
    End Sub

    Private Sub vScroll_Renamed_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VScroll_Renamed.visibleChanged
        If Me.Name = "" Or VScroll_Renamed.visible = False Then Exit Sub
        If HScroll_Renamed.visible = True Then showBars()
    End Sub

    Private Function getDrawingRectangle(ByVal firstNoItem As Integer, ByVal lastNoItem As Integer) As Rectangle
        Dim noLastItem As Integer = findLastItem()
        If firstNoItem = -1 Then firstNoItem = lastNoItem
        If lastNoItem = -1 Then lastNoItem = firstNoItem
        If firstNoItem = -1 And lastNoItem = -1 Then
            firstNoItem = findFirstItem()
            lastNoItem = findLastItem()
        End If
        If lastNoItem > noLastItem Then lastNoItem = noLastItem
        If firstNoItem > noLastItem Then firstNoItem = noLastItem
        If firstNoItem < 0 Then firstNoItem = 0

        Dim RedrawLeftPos, RedrawTopPos, RedrawHeight, redrawWidth As Integer
        'Find coord to draw
        RedrawTopPos = _Items(firstNoItem).Top
        If RedrawTopPos < 0 Then RedrawTopPos = 0
        RedrawTopPos += listTop

        RedrawLeftPos = _Items(firstNoItem).Left
        If RedrawLeftPos < 0 Then RedrawLeftPos = 0
        RedrawLeftPos += listLeft

        RedrawHeight = (_Items(lastNoItem).Top + _Items(lastNoItem).Height) - RedrawTopPos + 1
        If mouseMove3D Then RedrawHeight += 8
        RedrawHeight += itemBorder

        redrawWidth = borderPanel.ClientRectangle.Width + itemBorder
        
        Return New Rectangle(RedrawLeftPos, RedrawTopPos, redrawWidth, RedrawHeight)
    End Function

    Private Function getMaxWidth() As Integer
        If _Items.Count = 0 Then Return 0

        Dim MaxW1, maxW2 As Integer
        MaxW1 = 0 : aaDid = False

        For Each curItem As IListItem In _Items
            MaxW1 = Math.Max(curItem.Width, MaxW1)
        Next

        maxW2 = MaxW1

        If autoSizeHorizontally = False AndAlso autoAdjust = True AndAlso borderPanel.ClientRectangle.Width > MaxW1 Then
            MaxW1 = borderPanel.ClientRectangle.Width - 2

            If vScrolling = True Then
                If (_Items(_Items.Count - 1).Top + _Items(_Items.Count - 1).Height - itemBorder + 1) > borderPanel.ClientRectangle.Height Then MaxW1 -= VScroll_Renamed.width
            End If

            aaDid = True
        End If

        If MaxW1 < maxW2 Then MaxW1 = maxW2
        If MaxW1 < MinWidth Then MaxW1 = MinWidth

        If mouseMove3D = True And autoSizeHorizontally = False And borderPanel.ClientRectangle.Width < (MaxW1 + 1) Then MaxW1 += 4

        Return MaxW1 + extraWidth
    End Function

    Private Sub drawAll(ByVal noItem As Integer, Optional ByVal force3D As Boolean = False)
        If noItem < 0 Then Exit Sub

        Dim gCreated As Boolean = False
        If g Is Nothing Then
            g = Graphics.FromImage(b)
            gCreated = True
        End If

        drawBack(noItem, force3D)
        drawBorder(noItem, force3D)
        drawText(noItem, force3D)

        If gCreated Then g.Dispose()
    End Sub

    Private Sub drawBorder(ByVal noItem As Integer, Optional ByVal force3D As Boolean = False)
        If noItem >= _Items.Count Then Exit Sub

        Dim remove3D As Integer = 0
        Dim i As Integer
        Dim curRect As Rectangle = _Items(noItem).ItemRectangle
        curRect.Width = gmWidth

        If force3D = True Then
            curRect.Height += 4
            If noItem = findLastItem() And noItem <> findFirstItem() Then
                curRect.Y -= 4
            Else
            End If
        End If

        If do3D = True Or force3D = True Then
            Dim FirstColor, secondColor As Color
            FirstColor = SystemColors.ControlLightLight
            secondColor = SystemColors.ControlDarkDark
            Dim firstPen As New Pen(FirstColor)
            Dim secondPen As New Pen(secondColor)
            'curRect.Width += 4
            'curRect.Height += 4

            For i = 0 To 1
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
            Next i
            firstPen.Dispose()
            secondPen.Dispose()
            remove3D = 4
        End If

        _Items(noItem).drawBorder(New PaintEventArgs(g, curRect))
    End Sub
    'Private Sub drawBack(ByVal noItem As Integer, Optional ByVal force3D As Boolean = False)
    '    Dim curRect As Rectangle = _Items(NoItem).ItemRectangle
    '    curRect.Width = GMWidth
    '    If NoItem > 0 Then curRect.Height -= ItemBorder

    '    If Force3D = True Or Do3D = True Then
    '        If Force3D = True And NoItem = FindLastItem() And NoItem <> FindFirstItem() Then
    '            curRect.Y -= 4
    '        Else
    '            curRect.Y += 2
    '            curRect.Height += 2
    '        End If
    '    End If

    '    Dim CurPen As New Pen(_Items(NoItem).BackColor)
    '    Dim CurBrush As New SolidBrush(_Items(NoItem).BackColor)
    '    G.DrawRectangle(CurPen, curRect)
    '    G.FillRectangle(CurBrush, curRect)
    '    CurPen.Dispose()
    '    CurBrush.Dispose()
    'End Sub
    Private Sub drawBack(ByVal noItem As Integer, Optional ByVal force3D As Boolean = False)
        If noItem >= _Items.Count Then Exit Sub

        Dim curRect As Rectangle = _Items(noItem).ItemRectangle
        curRect.Width = gmWidth
        If noItem > 0 Then curRect.Height -= itemBorder

        If force3D = True And do3D = False Then
            If force3D = True And noItem = findLastItem() And noItem <> findFirstItem() Then
                curRect.Y -= 4
            Else
                curRect.Y += 2
                curRect.Height += 2
            End If
        End If

        _Items(noItem).drawBack(New PaintEventArgs(g, curRect))
    End Sub

    Private Sub drawText(ByVal noItem As Integer, Optional ByVal force3D As Boolean = False)
        If noItem >= _Items.Count Then Exit Sub

        Dim curItems As Generic.List(Of ListItem) = _Items(noItem).getItems
        Dim XPt, yPt As Integer
        For Each curItem As ListItem In curItems
            XPt = IIf(itemBorder = 0, 1, itemBorder) + itemMargin + curItem.left
            yPt = IIf(itemBorder = 0, 1, itemBorder) + itemMargin + curItem.top

            If force3D = True And noItem = findLastItem() And noItem <> findFirstItem() Then yPt -= 4
            If force3D = True Then yPt += 1

            If do3D = True Or force3D = True Then
                yPt += 1 : XPt += 1
            End If
            CType(curItem, IListItem).drawText(New PaintEventArgs(g, New Rectangle(XPt, yPt, gmWidth, curItem.height - itemBorder * 2 - itemMargin * 2)))
        Next
    End Sub

    Private Sub calculateTopLeft()
        Dim curTop As Integer = 0
        For Each curItem As IListItem In _Items
            curItem.Top = curTop
            curTop += curItem.Height
            If do3D Then
                curTop += 1
            Else
                curTop -= IIf(itemBorder = 0, 1, itemBorder) 'So all items border superposed
            End If
        Next
    End Sub

    Private Sub calculateHeightWidth(ByVal first2 As Integer, ByVal last2 As Integer)
        For Each curItem As IListItem In _Items
            curItem.calculateHeightWidth()

            If do3D = True Then
                curItem.Height += 4
                curItem.Width += 4
            ElseIf mouseMove3D = True And autoSizeHorizontally = True Then
                curItem.Width += 4
            End If
        Next
    End Sub

    Private Sub placeObjects(Optional ByRef firstPO As Integer = -1, Optional ByRef lastPO As Integer = -1, Optional ByRef allNextsFromItem As Integer = -1)
        If b Is Nothing Then Exit Sub
        If _Items.Count = 0 Then Exit Sub
        If isPlacingObjects = True Then Exit Sub

        Dim n, i As Integer

        If DoDraw = False Then
            modifNeeded = True
            Exit Sub
        Else
            modifNeeded = False
        End If

        isPlacingObjects = True

        RaiseEvent willReDraw(Me, Nothing)
        MyBase.SuspendLayout()

        Dim firstIndex As Integer = -1
        If sorted = True Then
            _Items.Sort()
            If Me.reverseSorting Then internalReverse()
        End If

        calculateHeightWidth(firstPO, lastPO)
        calculateTopLeft()
        gmWidth = getMaxWidth()
        Dim lastItemIndex As Integer = findLastItem()
        Dim myHeight As Integer = _Items(lastItemIndex).Top + _Items(lastItemIndex).Height + 1
        Dim myWidth As Integer = gmWidth + 1
        If Me.listCount = 0 And mouseMove3D = True Then myHeight += 4

        If autoSizeHorizontally = True Then
            myWidth += 2
            borderPanel.Width = myWidth
        End If
        If autoSizeVertically = True Then
            myHeight += 2
            If borderPanel.Height <> myHeight Then
                borderPanel.Height = myHeight
            End If
        End If
        
        'Create an image of the apprioriate size if the size changed
        If b.Width <> myWidth Or b.Height <> myHeight Then
            'Ensure that really big drawing isn't created... otherwise the "new Bitmap" line is causing a bug.
            'REM Ensure to keep this or not
            'REM If myWidth > 3000 Then myWidth = 3000
            'REM If myHeight > 3000 Then myHeight = 3000

            listWidth = myWidth
            listHeight = myHeight
            Dim b2 As Bitmap = b.Clone
            REM G.Clear(Me.BGColor)
            b = New Bitmap(myWidth, myHeight)
            g = Graphics.FromImage(b)
            g.DrawImage(b2, 0, 0)
            b2.Dispose()
        Else
            g = Graphics.FromImage(b)
        End If
        
        If allNextsFromItem = -1 Then
            If firstPO = -1 Then firstPO = 0
            If lastPO = -1 Then lastPO = _Items.Count - 1

            For i = firstPO To lastPO
                drawBack(i)
                drawText(i)
                drawBorder(i)
            Next i
        Else
            n = allNextsFromItem
            Do
                drawBack(n)
                drawText(n)
                drawBorder(n)
                n = findNextItem(n)
            Loop Until n < 0
        End If
        
        MyBase.ResumeLayout()
        isPlacingObjects = False

        showBars()
        
        If g IsNot Nothing Then g.Dispose()
        Me.Invalidate(True)
        
        gmWidth = getMaxWidth()
        Me.selected = itemSelect
        If itemSelect < 0 Then vsValue = 0
        oldBorder = itemBorder
        oldMarge = itemMargin
        RaiseEvent reDraw(Me, Nothing)
    End Sub

    Private vScrollRectangle As New Rectangle(0, 0, 0, 0)
    Private hScrollRectangle As New Rectangle(0, 0, 0, 0)

    Private Sub showBars()
        If findLastItem() < 0 Then VScroll_Renamed.visible = False : HScroll_Renamed.visible = False : Exit Sub

        If (autoSizeVertically = False And ((_Items(findLastItem).Top + _Items(findLastItem).Height - itemBorder + 1) > borderPanel.ClientRectangle.Height) Or (borderPanel.ClientRectangle.Height > objMaxHeight And autoSizeVertically = True And objMaxHeight > 0)) And vScrolling = True Then
            VScroll_Renamed.visible = True
            If autoSizeHorizontally = True Then borderPanel.Width = listWidth + VScroll_Renamed.width
            If autoSizeVertically = True And borderPanel.ClientRectangle.Height > objMaxHeight And objMaxHeight > 0 Then borderPanel.Height = objMaxHeight
            If autoSizeHorizontally = True And borderPanel.ClientRectangle.Width > objMaxWidth And objMaxWidth > 0 Then borderPanel.Width = objMaxWidth
            If autoSizeVertically = True And borderPanel.ClientRectangle.Height < objMinHeight And objMinHeight > 0 Then borderPanel.Height = objMinHeight
            If autoSizeHorizontally = True And borderPanel.ClientRectangle.Width < objMinWidth And objMinWidth > 0 Then borderPanel.Width = objMinWidth
            VScroll_Renamed.left = borderPanel.ClientRectangle.Width - VScroll_Renamed.width
            VScroll_Renamed.height = borderPanel.ClientRectangle.Height
        Else
            VScroll_Renamed.visible = False
        End If

        If (autoSizeHorizontally = False And ((listWidth > borderPanel.ClientRectangle.Width And VScroll_Renamed.visible = False)) Or (listWidth > VScroll_Renamed.left And VScroll_Renamed.visible = True)) And hScrolling = True Then
            HScroll_Renamed.visible = True
            If autoSizeVertically = True Then borderPanel.Height = borderPanel.Height + HScroll_Renamed.height

            HScroll_Renamed.top = borderPanel.ClientRectangle.Height - HScroll_Renamed.height
            If VScroll_Renamed.visible = True And VScroll_Renamed.height > HScroll_Renamed.top Then
                HScroll_Renamed.width = borderPanel.ClientRectangle.Width - VScroll_Renamed.width
            Else
                HScroll_Renamed.width = borderPanel.ClientRectangle.Width
            End If
        Else
            HScroll_Renamed.visible = False
        End If

        Dim rHeight As Single = borderPanel.ClientRectangle.Height
        Dim rWidth As Single = borderPanel.ClientRectangle.Width

        'Max & LargeChange Properties & Corrections
        If VScroll_Renamed.visible = True Then rWidth -= VScroll_Renamed.width
        If (listWidth - rWidth) >= 0 Then HScroll_Renamed.maximum = listWidth - rWidth
        REM If HScroll_Renamed.Maximum > 0 Then HScroll_Renamed.LargeChange = Int(HScroll_Renamed.Maximum / 10)
        REM If HScroll_Renamed.Maximum <= 0 Or HScroll_Renamed.LargeChange <= 0 And HScroll_Renamed.Visible = True Then HScroll_Renamed.Visible = False : If VScroll_Renamed.Visible = True Then VScroll_Renamed.Height = BorderPanel.ClientSize.Height
        If HScroll_Renamed.visible = True Then rHeight -= HScroll_Renamed.height
        If (listHeight - rHeight) >= 0 Then VScroll_Renamed.maximum = listHeight - rHeight
        REM If VScroll_Renamed.Maximum > 0 Then VScroll_Renamed.LargeChange = Int(VScroll_Renamed.Maximum / 10)
        REM If VScroll_Renamed.Maximum <= 0 Or VScroll_Renamed.LargeChange <= 0 And VScroll_Renamed.Visible = True Then VScroll_Renamed.Visible = False : If HScroll_Renamed.Visible = True Then HScroll_Renamed.Width = BorderPanel.ClientSize.Width

        'Added to correct problem with scrolling using the scrollbar
        REM VScroll_Renamed.Maximum += VScroll_Renamed.LargeChange
        REM HScroll_Renamed.Maximum += HScroll_Renamed.LargeChange

        If VScroll_Renamed.visible = True And autoSizeVertically = True Then VScroll_Renamed.maximum -= 2
        If HScroll_Renamed.visible = True And autoSizeHorizontally = True Then HScroll_Renamed.maximum -= 2
        VScroll_Renamed.minimum = 0
        VScroll_Renamed.top = 0
        HScroll_Renamed.minimum = 0
        HScroll_Renamed.left = 0
        vsValue = 0
        hsValue = 0
        HScroll_Renamed.maximum += 1
        VScroll_Renamed.maximum += 1

        Dim vScrollRemoval As Integer = IIf(VScroll_Renamed.visible, VScroll_Renamed.width, 0)
        If VScroll_Renamed.visible Then vScrollRectangle = New Rectangle(Me.clientRectangle.Width - VScroll_Renamed.width - 1, 0, VScroll_Renamed.width, Me.clientRectangle.Height - 1)
        If HScroll_Renamed.visible Then hScrollRectangle = New Rectangle(0, Me.clientRectangle.Height - HScroll_Renamed.height - 1, Me.clientRectangle.Width - vScrollRemoval - 1, HScroll_Renamed.height)
    End Sub

    Private Sub userControl_Initialize()
        EmptyItem = ""
        itemSelect = -1
        modifNeeded = False
        borderPanel.Size = MyBase.Size
        AddedWidth = MyBase.Width - borderPanel.ClientRectangle.Width
        addedHeight = MyBase.Height - borderPanel.ClientRectangle.Height
        oldMarge = itemMargin
        oldBorder = itemBorder
        _Items.Clear()

        Try
            b = New Bitmap(borderPanel.ClientRectangle.Width, borderPanel.ClientRectangle.Height, borderPanel.CreateGraphics())
            g = Graphics.FromImage(b)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'AddErrorLog(ex)
        End Try
        Me.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, True)
        firstTime = True
    End Sub

    Private Sub userControl_InitProperties()
        Me.do3D = False
        Me.autoSizeVertically = False
        Me.autoSizeHorizontally = False
        Me.autoAdjust = True
        Me.objMaxHeight = 200
        Me.objMaxWidth = 200
        Me.selected = -1
        Me.itemMargin = 0
        Me.itemBorder = 0
        Me.hScrolling = False
        Me.vScrolling = False
        Me.hsValue = 0
        Me.vsValue = 0
        Me.draw = True
        Me.borderColor = System.Drawing.ColorTranslator.FromOle(&H0)
        Me.borderSelColor = System.Drawing.ColorTranslator.FromOle(&H80000002)
        Me.baseAlignment = IListItem.AlignmentType.LeftA
        Me.baseBackColor = System.Drawing.ColorTranslator.FromOle(&H80000009)
        Me.baseForeColor = System.Drawing.ColorTranslator.FromOle(&H0)
        baseFont = New Font(MyBase.Font.Name, MyBase.Font.Size)
        Me.borderStyle = 0
        Me.bgColor = System.Drawing.ColorTranslator.FromOle(&H80000009)
        Me.mouseSpeed = 50
        Me.clickEnabled = True
        Me.sorted = False
    End Sub
#End Region

#Region "Item modification"
    Public Sub insertRange(ByVal startingIndex As Integer, ByVal items As Generic.List(Of ListItem))
        For Each curItem As IListItem In items
            _Items.Add(curItem)
        Next
    End Sub

    Public Sub untieItem(ByVal noItem As Integer)
        If Not TypeOf _Items(noItem) Is ListItemGroup Then Exit Sub

        Dim subItems As Generic.List(Of ListItem) = _Items(noItem).getItems
        _Items.RemoveAt(noItem)
        _Items.InsertRange(noItem, subItems.ToArray)
    End Sub

    Public Sub tieItem(ByVal nbItemToGroup As Integer, ByVal startingItem As Integer)
        If startingItem < 0 Or startingItem > (_Items.Count - 1) Then Exit Sub

        If nbItemToGroup = 1 Then
            untieItem(startingItem)
        Else
            Dim curNbToGroup As Integer = 0
            Dim ending As Integer = startingItem + nbItemToGroup - 1
            For a As Integer = startingItem To ending
                If _Items.Count <= a Then Exit For

                If TypeOf _Items(a) Is ListItemGroup Then
                    curNbToGroup += _Items(a).getItems.Count
                    ending += _Items(a).getItems.Count
                    untieItem(a)
                    If curNbToGroup >= nbItemToGroup Then curNbToGroup = nbItemToGroup : Exit For
                Else
                    If curNbToGroup >= nbItemToGroup Then curNbToGroup = nbItemToGroup : Exit For
                    curNbToGroup += 1
                End If
            Next a

            Dim newItemGroup As New ListItemGroup(Me)
            For a As Integer = startingItem To startingItem + curNbToGroup - 1 'Create grouped item
                newItemGroup.items.Add(_Items(a))
            Next a
            For a As Integer = 1 To curNbToGroup 'Remove items grouped
                _Items.RemoveAt(startingItem)
            Next a
            _Items.Insert(startingItem, newItemGroup) 'Add the GroupedItem
        End If

        placeObjects(0, _Items.Count - 1)
    End Sub

    Public ReadOnly Property ItemLeft(ByVal noItem As Integer) As Single
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return 0

            Return _Items(noItem).Left
        End Get
    End Property

    Public ReadOnly Property ItemTop(ByVal noItem As Integer) As Single
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return 0

            Return _Items(noItem).Top
        End Get
    End Property

    Public ReadOnly Property ItemRelativeLeft(ByVal noItem As Integer) As Single
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return 0

            Dim relative As Single = _Items(noItem).Left - Me.hsValue
            If relative < 0 Then relative = 0
            Return relative
        End Get
    End Property

    Public ReadOnly Property ItemRelativeTop(ByVal noItem As Integer) As Single
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return 0

            Dim relative As Single = _Items(noItem).Top - Me.vsValue
            If relative < 0 Then relative = 0
            Return relative
        End Get
    End Property

    Public Property ItemIconsShowed(ByVal noItem As Integer, ByVal NoIconIndex As Integer) As Boolean
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return False
            If _Items(noItem).IconsShowed.Count <= NoIconIndex Then Return False

            Return _Items(noItem).IconsShowed(NoIconIndex)
        End Get
        Set(ByVal Value As Boolean)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property
            If NoIconIndex < 0 Then Exit Property

            'Ensure there is enough space in the list to set data
            If _Items(noItem).IconsShowed.Count >= NoIconIndex Then
                For i As Integer = _Items(noItem).IconsShowed.Count To NoIconIndex
                    _Items(noItem).IconsShowed.Add(False)
                Next i
            End If

            'Set data
            _Items(noItem).IconsShowed(NoIconIndex) = Value

            'Redraw items
            placeObjects(noItem, noItem)
        End Set
    End Property

    Public Property ItemClickable(ByVal noItem As Integer) As Boolean
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Return _Items(noItem).Clickable
        End Get
        Set(ByVal Value As Boolean)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            _Items(noItem).Clickable = Value
        End Set
    End Property

    Public Property ItemBackColor(ByVal noItem As Integer) As Color
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Return _Items(noItem).BackColor
        End Get
        Set(ByVal Value As Color)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            _Items(noItem).BackColor = Value

            placeObjects(noItem, noItem)
        End Set
    End Property

    Public Property ItemForeColor(ByVal noItem As Integer) As Color
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Return _Items(noItem).ForeColor
        End Get
        Set(ByVal Value As Color)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            _Items(noItem).ForeColor = Value

            placeObjects(noItem, noItem)
        End Set
    End Property

    Public Property ItemText(ByVal noItem As Integer) As String
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return ""

            Return _Items(noItem).Text
        End Get
        Set(ByVal Value As String)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Dim beforeW As Single
            Dim oldCaption As String = _Items(noItem).Text
            beforeW = gmWidth
            If Value Is Nothing Then Value = ""
            _Items(noItem).Text = Value
            placeObjects(noItem, noItem)
            If beforeW <> getMaxWidth() Then placeObjects(0, _Items.Count - 1)
            RaiseEvent itemCaptionChange(noItem, oldCaption, Value)
        End Set
    End Property

    Public Property ItemAlignment(ByVal noItem As Integer) As IListItem.AlignmentType
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Return _Items(noItem).Alignment
        End Get
        Set(ByVal Value As IListItem.AlignmentType)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            _Items(noItem).Alignment = Value

            placeObjects(noItem, noItem)
        End Set
    End Property

    Public Property ItemFont(ByVal noItem As Integer) As Font
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return Me.baseFont

            Return _Items(noItem).Font
        End Get
        Set(ByVal Value As Font)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Dim beforeW As Integer = gmWidth

            _Items(noItem).Font = Value

            placeObjects(noItem, noItem)
            If beforeW <> getMaxWidth() Then placeObjects(0, _Items.Count - 1)
        End Set
    End Property

    Public Property ItemToolTipText(ByVal noItem As Integer) As String
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return ""

            Return _Items(noItem).ToolTipText
        End Get
        Set(ByVal Value As String)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            _Items(noItem).ToolTipText = Value
        End Set
    End Property

    Public Property ItemValueA(ByVal noItem As Integer) As Object
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return Nothing

            Return _Items(noItem).ValueA
        End Get
        Set(ByVal Value As Object)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Dim oldValue As Object = _Items(noItem).ValueA
            _Items(noItem).ValueA = Value
            RaiseEvent itemValueAChange(noItem, oldValue, Value)
        End Set
    End Property

    Public Property ItemValueB(ByVal noItem As Integer) As Object
        Get
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Return Nothing

            Return _Items(noItem).ValueB
        End Get
        Set(ByVal Value As Object)
            If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Property

            Dim oldValue As Object = _Items(noItem).ValueB
            _Items(noItem).ValueB = Value
            RaiseEvent itemValueBChange(noItem, oldValue, Value)
        End Set
    End Property

    Public Sub all(ByRef propertyToChange As PType, ByVal value As Object)
        Dim i As Integer
        For i = 0 To (_Items.Count - 1)
            Select Case propertyToChange
                Case 0
                    _Items(i).Alignment = value

                Case 1
                    _Items(i).BackColor = value

                Case 2
                    If IsNothing(value) Then value = "Item " & i
                    _Items(i).Text = value

                Case 3
                    If IsNothing(value) Then value = baseFont
                    _Items(i).Font = value

                Case 4
                    If IsNothing(value) Then value = 8
                    _Items(i).Font = New Font(_Items(i).Font.FontFamily, value, _Items(i).Font.Style)

                Case 5
                    _Items(i).ForeColor = value

                Case 6
                    _Items(i).ToolTipText = value

                Case 7
                    If (_Items(i).Font.Style And FontStyle.Bold) = FontStyle.Bold Then
                        If value = False Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style - FontStyle.Bold)
                    Else
                        If value = True Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style + FontStyle.Bold)
                    End If

                Case 8
                    If (_Items(i).Font.Style And FontStyle.Italic) = FontStyle.Italic Then
                        If value = False Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style - FontStyle.Italic)
                    Else
                        If value = True Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style + FontStyle.Italic)
                    End If

                Case 9
                    If (_Items(i).Font.Style And FontStyle.Strikeout) = FontStyle.Strikeout Then
                        If value = False Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style - FontStyle.Strikeout)
                    Else
                        If value = True Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style + FontStyle.Strikeout)
                    End If

                Case 10
                    If (_Items(i).Font.Style And FontStyle.Underline) = FontStyle.Underline Then
                        If value = False Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style - FontStyle.Underline)
                    Else
                        If value = True Then _Items(i).Font = New Font(_Items(i).Font, _Items(i).Font.Style + FontStyle.Underline)
                    End If
            End Select
        Next i

        If propertyToChange <> 6 Then placeObjects(0, _Items.Count - 1)
    End Sub
#End Region

#Region "Find an item"
    Public Function mapItem(ByVal x As Single, ByVal y As Single) As Integer
        Dim i As Integer
        For i = 0 To _Items.Count - 1
            If y >= _Items(i).Top And y < (_Items(i).ItemRectangle.Bottom + 1) And x > _Items(i).Left And x < (_Items(i).Left + gmWidth) Then
                mapItem = i : Exit Function
            End If
        Next i

        mapItem = -1
    End Function

    Public Function findString(ByVal strToSearch As String, Optional ByVal respectCase As Boolean = True, Optional ByVal searchIn As FindingType = FindingType.Caption, Optional ByVal startsWith As Boolean = False, Optional ByVal startingIndex As Integer = 0, Optional ByVal compareOnlyAlphaNum As Boolean = False) As Integer
        If startingIndex < 0 Then startingIndex = 0
        Dim i As Integer
        Dim myMethod As Microsoft.VisualBasic.CompareMethod = Microsoft.VisualBasic.CompareMethod.Binary
        If respectCase = False Then myMethod = Microsoft.VisualBasic.CompareMethod.Text
        Dim compareMethod As StringComparison = StringComparison.Ordinal
        If respectCase = False Then compareMethod = StringComparison.OrdinalIgnoreCase
        If compareOnlyAlphaNum Then
            strToSearch = Chaines.replaceAccents(strToSearch)
            onlyAlpha(strToSearch, , True, True)
        End If

        Dim curText As String = ""

        For i = startingIndex To _Items.Count - 1
            For Each curItem As ListItem In _Items(i).getItems()
                curText = ""
                Select Case searchIn
                    Case FindingType.Caption
                        curText = curItem.text
                    Case FindingType.ToolTipText
                        curText = curItem.toolTipText
                    Case FindingType.ValueA
                        If curItem.valueA IsNot Nothing Then curText = curItem.valueA.ToString
                    Case FindingType.ValueB
                        If curItem.valueB IsNot Nothing Then curText = curItem.valueB.ToString
                End Select

                If compareOnlyAlphaNum Then
                    curText = Chaines.replaceAccents(curText)
                    onlyAlpha(curText, , True, True)
                End If
                If (startsWith AndAlso curText.StartsWith(strToSearch, compareMethod)) OrElse (startsWith = False AndAlso InStr(curText, strToSearch, myMethod)) > 0 Then Return i
            Next
        Next i

        Return -1
    End Function

    Public Function findValue(ByVal valueToSearch As Object, Optional ByVal searchIn As FindingType = FindingType.ValueA) As Integer
        For i As Integer = 0 To _Items.Count - 1
            Select Case searchIn
                Case FindingType.ValueA
                    If _Items(i).ValueA.Equals(valueToSearch) Then Return i
                Case FindingType.ValueB
                    If _Items(i).ValueB.Equals(valueToSearch) Then Return i
                Case FindingType.Caption
                    If _Items(i).Text.Equals(valueToSearch) Then Return i
                Case FindingType.ToolTipText
                    If _Items(i).ToolTipText.Equals(valueToSearch) Then Return i
            End Select
        Next i

        Return -1
    End Function

    Public Function findStringExact(ByVal strToSearch As String, Optional ByVal respectCase As Boolean = True, Optional ByVal searchIn As FindingType = FindingType.Caption, Optional ByVal startingIndex As Integer = 0) As Integer
        If startingIndex < 0 Then startingIndex = 0
        Dim i As Integer
        For i = startingIndex To _Items.Count - 1
            Select Case searchIn
                Case FindingType.Caption
                    If respectCase = True Then
                        If _Items(i).Text = strToSearch Then Return i
                    Else
                        If _Items(i).Text.ToUpper = strToSearch.ToUpper Then Return i
                    End If
                Case FindingType.ToolTipText
                    If respectCase = True Then
                        If _Items(i).ToolTipText = strToSearch Then Return i
                    Else
                        If _Items(i).ToolTipText.ToUpper = strToSearch.ToUpper Then Return i
                    End If
                Case FindingType.ValueA
                    If respectCase = True Then
                        If CStr(_Items(i).ValueA) = strToSearch Then Return i
                    Else
                        If CStr(_Items(i).ValueA).ToUpper = strToSearch.ToUpper Then Return i
                    End If
                Case FindingType.ValueB
                    If respectCase = True Then
                        If CStr(_Items(i).ValueB) = strToSearch Then Return i
                    Else
                        If CStr(_Items(i).ValueB).ToUpper = strToSearch.ToUpper Then Return i
                    End If
            End Select
        Next i

        Return -1
    End Function

    Public Function findFirstItem() As Integer
        If _Items.Count = 0 Then Return -1

        Return 0
    End Function

    Public Function findNextItem(ByRef noItem As Integer, Optional ByVal respectClickabality As Boolean = False) As Integer
        If noItem < 0 OrElse noItem = (_Items.Count - 1) Then Return -1

        Return noItem + 1
    End Function

    Public Function findPreviousItem(ByRef noItem As Integer, Optional ByVal respectClickabality As Boolean = False) As Integer
        If noItem < 1 Then Return -1

        Return noItem - 1
    End Function

    Public Function findLastItem() As Integer
        If _Items Is Nothing Then Return -1
        If _Items.Count = 0 Then Return -1

        Return _Items.Count - 1
    End Function
#End Region

    Public Function add(Optional ByVal nomItem As String = "", Optional ByVal valueAItem As Object = Nothing, Optional ByVal valueBItem As Object = Nothing) As Integer
        Dim beforeMW As Single
        If DoDraw = True Then beforeMW = getMaxWidth()

        Dim newItem As New ListItem(Me)

        With newItem
            .clickable = True
            .alignment = _BaseAlign
            .backColor = _BaseBackColor
            .text = nomItem
            .font = _BaseFont
            .foreColor = _BaseForeColor
            .toolTipText = ""
            .valueA = valueAItem
            .valueB = valueBItem
            .calculateHeightWidth()

            If findLastItem() <> -1 Then
                .top = _Items(findLastItem).Top + _Items(findLastItem).Height - itemBorder
            Else
                .top = itemBorder
            End If
            .left = 0
        End With

        _Items.Add(newItem)

        If DoDraw = True Then
            If beforeMW <> getMaxWidth() Then
                placeObjects(0, _Items.Count - 1)
            Else
                placeObjects(_Items.Count - 1, _Items.Count - 1)
            End If
        Else
            placeObjects(0, 0)
        End If

        AddHandler newItem.shouldRedraw, AddressOf shouldRedraw

        Return _Items.IndexOf(newItem)
    End Function

    Private Sub shouldRedraw()
        placeObjects()
    End Sub

    Public Function maximum() As Integer
        Return (_Items.Count - 1)
    End Function

    Public Sub remove(ByVal noItem As Integer)
        If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Sub

        If itemSelect = noItem Then
            itemSelect = -1
            RaiseEvent selectedChange()
        End If
        RemoveHandler _Items(noItem).shouldRedraw, AddressOf shouldRedraw
        _Items.RemoveAt(noItem)
        If findFirstItem() = -1 Then cls() : Exit Sub

        placeObjects(0, _Items.Count - 1)
    End Sub

    Private Sub internalReverse()
        Dim theList2 As New Generic.List(Of IListItem)

        For i As Integer = _Items.Count - 1 To 0 Step -1
            theList2.Add(_Items(i))
        Next i

        _Items = theList2
    End Sub

    Public Sub reverse()
        internalReverse()

        placeObjects()
    End Sub

    Public Sub cls()
        If b IsNot Nothing Then
            g = Graphics.FromImage(b)
            g.Clear(Me.bgColor)
            g.Dispose()
        End If
        Me.Invalidate(True)

        For i As Integer = 0 To _Items.Count - 1
            _Items(i).ValueA = Nothing
            _Items(i).ValueB = Nothing
            _Items(i).ToolTipText = ""
        Next i
        List.toolTip1.RemoveAll()
        EmptyItem = ""
        _Items.Clear()
        itemSelect = -1
        Me.vsValue = 0
        Me.hsValue = 0
        VScroll_Renamed.visible = False
        HScroll_Renamed.visible = False
    End Sub

    Public Sub showItem(ByVal noItem As Integer)
        Select Case itemVisible(noItem)
            Case List.ItemVisibility.None_BelowList
                showItem(noItem, List.PosType.Bottom)
            Case List.ItemVisibility.Partial_BelowList
                showItem(noItem, List.PosType.Bottom)
            Case List.ItemVisibility.None_UpperList
                showItem(noItem, List.PosType.Top)
            Case List.ItemVisibility.Partial_UpperList
                showItem(noItem, List.PosType.Top)
            Case Else
        End Select
    End Sub

    Public Sub showItem(ByVal noItem As Integer, ByVal position As PosType)
        If position = PosType.BetterGuess Then
            showItem(noItem)
            Exit Sub
        End If

        Dim ajustement As Integer
        If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Sub
        Dim newListTop As Integer = 0

        Select Case position
            Case PosType.Top
                newListTop = -_Items(noItem).Top

            Case PosType.Middle
                newListTop = -_Items(noItem).Top + (borderPanel.ClientRectangle.Height - _Items(noItem).Height) / 2

            Case PosType.Bottom
                ajustement = 1
                If HScroll_Renamed.visible = True Then ajustement = HScroll_Renamed.height + 2
                newListTop = borderPanel.ClientRectangle.Height - _Items(noItem).ItemRectangle.Bottom - ajustement
        End Select

        If newListTop > 0 Then newListTop = 0
        If System.Math.Abs(newListTop) > VScroll_Renamed.maximum Then newListTop = -VScroll_Renamed.maximum
        vsValue = System.Math.Abs(newListTop)
    End Sub

    Public Function itemVisible(ByVal noItem As Integer) As ItemVisibility
        If noItem > (_Items.Count - 1) Or noItem < 0 Then Exit Function

        Dim ObjTop, ObjBottom, MinPos, maxPos As Integer
        Dim ajustement As Integer
        ajustement = 0
        If HScroll_Renamed.visible = True Then ajustement = HScroll_Renamed.height
        MinPos = Math.Abs(listTop)
        maxPos = MinPos + borderPanel.ClientRectangle.Height - ajustement
        ObjBottom = _Items(noItem).ItemRectangle.Bottom
        ObjTop = _Items(noItem).Top

        If ObjTop >= MinPos And ObjBottom <= maxPos Then
            Return ItemVisibility.Fully
        ElseIf (ObjTop >= MinPos And ObjBottom >= maxPos And ObjTop < maxPos) Then
            Return ItemVisibility.Partial_BelowList
        ElseIf (ObjTop <= MinPos And ObjBottom <= maxPos And ObjBottom > MinPos) Then
            Return ItemVisibility.Partial_UpperList
        ElseIf ObjTop < maxPos Then
            Return ItemVisibility.None_BelowList
        ElseIf ObjBottom > MinPos Then
            Return ItemVisibility.None_UpperList
        End If
    End Function

    Public Function itemGrouped(ByVal noItem As Integer) As Boolean
        Return TypeOf _Items(noItem) Is ListItemGroup
    End Function

    Public Function itemNbGroup(ByVal noItem As Integer) As Integer
        Return _Items(noItem).getItems.Count
    End Function

    Public Sub forceRedraw()
        Dim oldHSValue As Integer = hsValue
        Dim oldVSValue As Integer = vsValue
        Dim oldSelected As Integer = itemSelect
        Dim oldDrawState As Boolean = draw
        draw = True
        placeObjects()
        hsValue = oldHSValue
        vsValue = oldVSValue
        If oldSelected <> -1 AndAlso Me.itemVisible(oldSelected) <> ItemVisibility.Fully Then Me.showItem(oldSelected)
        draw = oldDrawState
    End Sub

    Public Function listCount() As Integer
        Return _Items.Count
    End Function

    'Public Function basePropertiesToXml() As String
    '    Dim xml As New System.Text.StringBuilder()
    '    xml.AppendLine("<BaseProperties>")
    '    xml.Append("<BaseAlignment>").Append(CInt(Me.BaseAlignment)).AppendLine("</BaseAlignment>")
    '    xml.Append("<BaseAlignment>").Append(ColorTranslator.ToHtml(Me.BaseBackColor)).AppendLine("</BaseAlignment>")
    '    xml.Append("<BaseAlignment>").Append(ColorTranslator.ToHtml(Me.BaseForeColor)).AppendLine("</BaseAlignment>")
    '    xml.Append("<BaseAlignment>").Append(Me.BaseFont.ToString).AppendLine("</BaseAlignment>")
    '    xml.Append("<BaseAlignment>").Append(Me.AutoAdjust).AppendLine("</BaseAlignment>")
    '    xml.Append("<BaseAlignment>").Append(CInt(Me.BaseAlignment)).AppendLine("</BaseAlignment>")
    '    xml.AppendLine("</BaseProperties>")

    '    Return xml.ToString
    'End Function

    Public Sub saveTo(ByVal filePath As String)
        Dim newFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Using fs As FileStream = IO.File.Open(filePath, FileMode.Create)

            Dim properties As New Hashtable()
            Dim p() As Reflection.PropertyInfo = Me.GetType.GetProperties
            For Each curP As Reflection.PropertyInfo In p
                If curP.GetIndexParameters.Length = 0 AndAlso curP.DeclaringType.Name = Me.GetType.Name Then
                    If properties.ContainsKey(curP.Name) Then
                        properties(curP.Name) = curP.GetValue(Me, Nothing)
                    Else
                        properties.Add(curP.Name, curP.GetValue(Me, Nothing))
                    End If
                End If
            Next
            newFormatter.Serialize(fs, properties)
        End Using
    End Sub

    Public Sub readFrom(ByVal filePath As String)
        If IO.File.Exists(filePath) = False Then Exit Sub

        Dim newFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Using fs As FileStream = IO.File.Open(filePath, FileMode.Open)
            Dim properties As Hashtable = newFormatter.Deserialize(fs)
            Dim p() As Reflection.PropertyInfo = Me.GetType.GetProperties
            For Each curP As Reflection.PropertyInfo In p
                If curP.GetSetMethod IsNot Nothing AndAlso curP.GetIndexParameters.Length = 0 AndAlso curP.DeclaringType.Name = Me.GetType.Name Then
                    If properties.ContainsKey(curP.Name) Then curP.SetValue(Me, properties(curP.Name), Nothing)
                End If
            Next
        End Using
    End Sub

    Private Function innerClearSelection() As Integer
        Dim nbChanged As Integer = 0
        For Each curItem As IListItem In _Items
            If curItem.IsSelected Then
                nbChanged += 1
                curItem.IsSelected = False
            End If
        Next
        itemSelect = -1

        Return nbChanged
    End Function

    Public Function clearSelection() As Integer
        Dim nbChanged As Integer = innerClearSelection()

        If nbChanged <> 0 Then placeObjects()

        Return nbChanged
    End Function

    Protected Overrides Sub onPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
    End Sub

    Private Sub list_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    End Sub

    Private Sub hScroll_Renamed_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HScroll_Renamed.valueChanged
        Me.Invalidate(True)
        RaiseEvent hScroll_Scroll(sender, New ScrollEventArgs(ScrollEventType.EndScroll, HScroll_Renamed.value))
    End Sub

    Private Sub timerMouseDown_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerMouseDown.Tick
        If Control.MouseButtons <> Windows.Forms.MouseButtons.None Then borderPanel_MouseDown(Me, lastMouseEvent)
    End Sub

    Private Sub list_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        If Me.ParentForm Is Nothing OrElse (Me.ParentForm.MdiParent IsNot Nothing AndAlso Me.ParentForm.MdiParent.ActiveControl IsNot Nothing AndAlso Me.ParentForm.MdiParent.ActiveControl.FindForm.Equals(Me.ParentForm) = False) Then
            toolTip1.Active = False
            toolTip2.Active = False
        End If
    End Sub

    Private Sub list_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        toolTip1.Active = True
        toolTip2.Active = True
    End Sub
End Class
