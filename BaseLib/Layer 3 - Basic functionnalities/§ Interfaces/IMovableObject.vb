Imports System.Drawing

Public Interface IMovableObject
    Property blockObjectInArea() As Boolean
    Property blockMove() As Boolean

    Function getCoord() As Point
    Sub setCoord(ByVal newCoord As Point)
    Sub setMovability(ByVal movable As Boolean)
    Function ensureGoodCoord(ByVal newLeft As Integer, ByVal newTop As Integer, ByVal isSwitchedToolBar As Boolean) As Point

    Event willMove(ByVal sender As Object, ByVal x As Integer, ByVal y As Integer, ByVal xObjects As Integer, ByVal yObjects As Integer)
    Event move(ByVal sender As Object, ByVal e As EventArgs)
End Interface
