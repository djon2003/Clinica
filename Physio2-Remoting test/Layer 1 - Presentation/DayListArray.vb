Public Class DayListArray
    Inherits Generic.List(Of Controls.List)

    Public Event click(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs)
    Public Event keyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event dblClick(ByVal sender As Object, ByVal e As CI.Controls.List.DblClickEventArgs)
    Public Event mouseDown(ByVal sender As Object, ByVal e As CI.Controls.List.MouseDownEventArgs)
    Public Event mouseUp(ByVal sender As Object, ByVal e As CI.Controls.List.MouseUpEventArgs)
    Public Event mouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event willSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs)
    Public Event show_Renamed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event dragDrop(ByRef sender As Object, ByRef e As DragEventArgs)
    Public Event dragEnter(ByRef sender As Object, ByRef e As DragEventArgs)
    Public Event dragLeave(ByRef sender As Object, ByRef e As EventArgs)
    Public Event dragOver(ByRef sender As Object, ByRef e As DragEventArgs)
    Private _Configed As New Generic.List(Of Boolean)
    Private _ValueA As New Generic.List(Of Object)
    Private _ValueB As New Generic.List(Of Object)

    Public Property Configed(ByVal index As Short) As Boolean
        Get
            If index >= _Configed.Count Then Return False

            Return _Configed(index)
        End Get
        Set(ByVal Value As Boolean)
            If index >= _Configed.Count Then
                For i As Integer = _Configed.Count To index
                    _Configed.Add(False)
                Next i
            End If

            _Configed(index) = Value
        End Set
    End Property

    Public Property ValueA(ByVal index As Short) As Object
        Get
            If index >= _ValueA.Count Then Return Nothing

            Return _ValueA(index)
        End Get
        Set(ByVal Value As Object)
            If index >= _ValueA.Count Then
                For i As Integer = _ValueA.Count To index
                    _ValueA.Add(Nothing)
                Next i
            End If

            _ValueA(index) = Value
        End Set
    End Property

    Public Property ValueB(ByVal index As Short) As Object
        Get
            If index >= _ValueB.Count Then Return Nothing

            Return _ValueB(index)
        End Get
        Set(ByVal Value As Object)
            If index >= _ValueB.Count Then
                For i As Integer = _ValueB.Count To index
                    _ValueB.Add(Nothing)
                Next i
            End If

            _ValueB(index) = Value
        End Set
    End Property
End Class

