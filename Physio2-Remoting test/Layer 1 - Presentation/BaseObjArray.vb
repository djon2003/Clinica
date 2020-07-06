Public Class BaseObjArray
    Inherits System.Collections.CollectionBase

    Public Event checkChanged(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)
    Public Event click(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)
    Public Event doubleClick(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)
    Public Event enter(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)
    Public Event gotFocus(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)
    Public Event keyDown(ByRef index As Short, ByVal sender As Object, ByVal e As KeyEventArgs)
    Public Event keyPress(ByRef index As Short, ByVal sender As Object, ByVal e As KeyPressEventArgs)
    Public Event keyUp(ByRef index As Short, ByVal sender As Object, ByVal e As KeyEventArgs)
    Public Event leave(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)
    Public Event lostFocus(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)
    Public Event mouseDown(ByRef index As Short, ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event mouseEnter(ByRef index As Short, ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event mouseHover(ByRef index As Short, ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event mouseLeave(ByRef index As Short, ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event mouseMove(ByRef index As Short, ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event mouseUp(ByRef index As Short, ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event mouseWheel(ByRef index As Short, ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event textChanged(ByRef index As Short, ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal capacity As Integer)
        MyBase.New(capacity)
    End Sub

    Public Function add(ByVal anObj As Object) As Integer
        Return Me.InnerList.Add(anObj)
    End Function

    Public Function add(ByVal anObj As Object, ByVal index As Integer) As Integer
        myMainWin.StatusText = "Will add " & index
        If index > Me.InnerList.Count - 1 Then Return add(anObj)

        myMainWin.StatusText = "Adding to " & index
        Me.InnerList.Insert(index, anObj)
        Return index
    End Function

    Public Sub remove(ByVal index As Integer)
        If index > Count - 1 Or index < 0 Then
            System.Windows.Forms.MessageBox.Show("Index non valide")
        Else
            InnerList.RemoveAt(index)
        End If
    End Sub

    Default Public ReadOnly Property Item(ByVal Index As Integer) As Object
        Get
            Return InnerList.Item(Index)
        End Get
    End Property
End Class
