<Serializable()> _
Public Class ListItemGroup
    Implements IListItem

    Private Sub New()
    End Sub

    Friend Sub New(ByVal associatedList As List)
        Me._List = associatedList
    End Sub

    Public Event shouldRedraw() Implements IListItem.shouldRedraw

#Region "Définitions"
    Private _IsSelected As Boolean = False
    Private _List As List
    Private _Items As New Generic.List(Of IListItem)
#End Region

#Region "Propriétés"

    Public Property iconsPosition() As Icons.IconPositions Implements IListItem.IconsPosition
        Get
            If _Items.Count = 0 Then Return Icons.IconPositions.DefinedByParent

            Return _Items(0).IconsPosition

            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As Icons.IconPositions)
            Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Set
    End Property

    Public Property iconsShowed() As System.Collections.Generic.List(Of Boolean) Implements IListItem.IconsShowed
        Get
            If _Items.Count = 0 Then Return New Generic.List(Of Boolean)

            Return _Items(0).IconsShowed
            '   Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Boolean))
            Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Set
    End Property

    Public Property iconType() As Icons.IconType Implements IListItem.IconType
        Get
            If _Items.Count = 0 Then Return Icons.IconType.InList

            Return _Items(0).IconType
        End Get
        Set(ByVal value As Icons.IconType)
            Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Set
    End Property

    Public Property items() As Generic.List(Of IListItem)
        Get
            Return _Items
        End Get
        Set(ByVal value As Generic.List(Of IListItem))
            _Items = value
        End Set
    End Property

    Public Property isSelected() As Boolean Implements IListItem.IsSelected
        Get
            Return _IsSelected
        End Get
        Set(ByVal value As Boolean)
            _IsSelected = value
        End Set
    End Property


#Region "Set all Items property at once"
    Public Property alignment() As IListItem.AlignmentType Implements IListItem.Alignment
        Get
            If _Items.Count = 0 Then Return IListItem.AlignmentType.LeftA

            Return _Items(0).Alignment
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As IListItem.AlignmentType)
            For Each curItem As IListItem In _Items
                curItem.Alignment = value
            Next
        End Set
    End Property

    Public Property font() As Font Implements IListItem.Font
        Get
            If _Items.Count = 0 Then Return New Font("Arial", 8)

            Return _Items(0).Font
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As Font)
            For Each curItem As IListItem In _Items
                curItem.Font = value
            Next
        End Set
    End Property

    Public Property backColor() As Drawing.Color Implements IListItem.BackColor
        Get
            If _Items.Count = 0 Then Return Color.White

            Return _Items(0).BackColor
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As Drawing.Color)
            For Each curItem As IListItem In _Items
                curItem.BackColor = value
            Next
        End Set
    End Property

    Public Property foreColor() As Drawing.Color Implements IListItem.ForeColor
        Get
            If _Items.Count = 0 Then Return Color.Black

            Return _Items(0).ForeColor
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As Drawing.Color)
            For Each curItem As IListItem In _Items
                curItem.ForeColor = value
            Next
        End Set
    End Property

    Public Property text() As String Implements IListItem.Text
        Get
            If _Items.Count = 0 Then Return ""

            Return _Items(0).Text
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As String)
            If _Items.Count = 0 Then Exit Property

            _Items(0).Text = value
            'For Each curItem As IListItem In _Items
            '    curItem.Text = value
            'Next
        End Set
    End Property

    Public Property toolTipText() As String Implements IListItem.ToolTipText
        Get
            If _Items.Count = 0 Then Return ""

            Return _Items(0).ToolTipText
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As String)
            For Each curItem As IListItem In _Items
                curItem.ToolTipText = value
            Next
        End Set
    End Property

    Public Property valueA() As Object Implements IListItem.ValueA
        Get
            If _Items.Count = 0 Then Return Nothing

            Return _Items(0).ValueA
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As Object)
            For Each curItem As IListItem In _Items
                curItem.ValueA = value
            Next
        End Set
    End Property

    Public Property valueB() As Object Implements IListItem.ValueB
        Get
            If _Items.Count = 0 Then Return Nothing

            Return _Items(0).ValueB
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As Object)
            For Each curItem As IListItem In _Items
                curItem.ValueB = value
            Next
        End Set
    End Property

    Public Property clickable() As Boolean Implements IListItem.Clickable
        Get
            If _Items.Count = 0 Then Return True

            Return _Items(0).Clickable
            'Throw New Exception("Not implemented - Acces via Items(Index).PropertyName")
        End Get
        Set(ByVal value As Boolean)
            For Each curItem As IListItem In _Items
                curItem.Clickable = value
            Next
        End Set
    End Property
#End Region

    Public Property itemRectangle() As Rectangle Implements IListItem.ItemRectangle
        Get
            Return New Rectangle(Me.left, Me.top, Me.width, Me.height)
        End Get
        Set(ByVal value As Rectangle)
            Me.top = value.Top
            Me.left = value.Left
            Me.width = value.Width
            Me.height = value.Height
        End Set
    End Property

    Public Property top() As Integer Implements IListItem.Top
        Get
            If _Items.Count = 0 Then Return 0

            Return _Items(0).Top
        End Get
        Set(ByVal value As Integer)
            For Each curItem As IListItem In _Items
                curItem.Top = value
                value += curItem.Height - IIf(_List.itemBorder = 0, 1, _List.itemBorder)
            Next
        End Set
    End Property

    Public Property left() As Integer Implements IListItem.Left
        Get
            Dim minLeft As Integer = Integer.MaxValue
            For Each curItem As IListItem In _Items
                minLeft = Math.Min(minLeft, curItem.Left)
            Next
            Return minLeft
        End Get
        Set(ByVal value As Integer)
            For Each curItem As IListItem In _Items
                curItem.Left = value
            Next
        End Set
    End Property

    Public Property width() As Integer Implements IListItem.Width
        Get
            Dim maxWidth As Integer
            For Each curItem As IListItem In _Items
                maxWidth = Math.Max(curItem.Width, maxWidth)
            Next
            Return maxWidth
        End Get
        Set(ByVal value As Integer)
            For Each curItem As IListItem In _Items
                curItem.Width = value
            Next
        End Set
    End Property

    Public Property height() As Integer Implements IListItem.Height
        Get
            Return (_Items(_Items.Count - 1).Top + _Items(_Items.Count - 1).Height) - _Items(0).Top
        End Get
        Set(ByVal value As Integer)
            For Each curItem As IListItem In _Items
                curItem.Height = value
            Next
        End Set
    End Property
#End Region

    Friend Sub calculateHeightWidth() Implements IListItem.calculateHeightWidth
        For Each curItem As IListItem In _Items
            curItem.calculateHeightWidth()
        Next
    End Sub

    Public Function compareTo(ByVal other As IListItem) As Integer Implements System.IComparable(Of IListItem).CompareTo
        If _Items.Count = 0 Then Return ("").CompareTo(other.Text)

        Return _Items(0).Text.CompareTo(other.Text)
    End Function

    Public Function getItems() As System.Collections.Generic.List(Of ListItem) Implements IListItem.getItems
        Dim t As New Generic.List(Of ListItem)

        For Each curItem As IListItem In _Items
            If TypeOf curItem Is ListItem Then
                t.Add(curItem)
            Else
                t.AddRange(curItem.getItems)
            End If
        Next

        Return t
    End Function

    Public Overrides Function toString() As String
        Return Me.text
    End Function

    Friend Sub drawBack(ByVal e As PaintEventArgs) Implements IListItem.drawBack
        'Draw background
        Dim curPen As New Pen(_Items(0).BackColor)
        Dim curBrush As New SolidBrush(_Items(0).BackColor)
        e.Graphics.DrawRectangle(curPen, e.ClipRectangle)
        e.Graphics.FillRectangle(curBrush, e.ClipRectangle)
        curPen.Dispose()
        curBrush.Dispose()

        For Each curItem As IListItem In _Items
            If curItem.BackColor <> _Items(0).BackColor Then
                curItem.drawBack(New PaintEventArgs(e.Graphics, curItem.ItemRectangle))
            End If
        Next
    End Sub

    Friend Sub drawBorder(ByVal e As PaintEventArgs) Implements IListItem.drawBorder
        Dim startingPt As Integer = 1
        Dim endingPt As Integer = _List.itemBorder
        If endingPt = 0 Then endingPt = 1
        If _IsSelected Then startingPt = Math.Ceiling(endingPt / 2)

        Dim curPen As New Pen(CType(IIf(_List.itemBorder = 0, Me.backColor, _List.borderColor), Color))
        For i As Integer = 0 To startingPt - 2
            e.Graphics.DrawRectangle(curPen, e.ClipRectangle.X + i, e.ClipRectangle.Y + i, e.ClipRectangle.Width - i * 2, e.ClipRectangle.Height - i * 2)
        Next i
        If _IsSelected Then
            curPen.Dispose() : curPen = New Pen(_List.borderSelColor)
        End If
        For i As Integer = startingPt - 1 To endingPt - 1
            e.Graphics.DrawRectangle(curPen, e.ClipRectangle.X + i, e.ClipRectangle.Y + i, e.ClipRectangle.Width - i * 2, e.ClipRectangle.Height - i * 2)
        Next i
        curPen.Dispose()
    End Sub

    Friend Sub drawText(ByVal e As PaintEventArgs) Implements IListItem.drawText
        For Each curItem As IListItem In _Items
            curItem.drawText(New PaintEventArgs(e.Graphics, New Rectangle(curItem.ItemRectangle.X + IIf(_List.itemBorder = 0, 1, _List.itemBorder) + _List.itemMargin, curItem.ItemRectangle.Y + IIf(_List.itemBorder = 0, 1, _List.itemBorder) + _List.itemMargin, e.ClipRectangle.Width, curItem.ItemRectangle.Height - IIf(_List.itemBorder = 0, 1, _List.itemBorder) * 2 + _List.itemMargin * 2)))
        Next
    End Sub

    Friend Sub onPaint(ByVal e As PaintEventArgs) Implements IListItem.onPaint
        drawBack(e)
        drawBorder(e)
        drawText(e)
    End Sub
End Class
