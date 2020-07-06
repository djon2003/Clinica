Public Interface IListItem
    Inherits IComparable(Of IListItem)

    Property Alignment() As AlignmentType
    Property Font() As Font
    Property BackColor() As Drawing.Color
    Property ForeColor() As Drawing.Color
    Property Text() As String
    Property ToolTipText() As String
    Property ValueA() As Object
    Property ValueB() As Object
    Property Clickable() As Boolean
    Property Width() As Integer
    Property Height() As Integer
    Property Left() As Integer
    Property Top() As Integer
    Property ItemRectangle() As Rectangle
    Property IsSelected() As Boolean
    Property IconsPosition() As Icons.IconPositions
    Property IconsShowed() As Generic.List(Of Boolean)
    Property IconType() As Icons.IconType

    Event shouldRedraw()

    Sub calculateHeightWidth()
    Sub onPaint(ByVal e As PaintEventArgs)
    Sub drawBack(ByVal e As PaintEventArgs)
    Sub drawBorder(ByVal e As PaintEventArgs)
    Sub drawText(ByVal e As PaintEventArgs)
    Function getItems() As Generic.List(Of ListItem)

    Enum AlignmentType
        LeftA = 1
        RightA = 4
        CenterA = 2
    End Enum
End Interface
