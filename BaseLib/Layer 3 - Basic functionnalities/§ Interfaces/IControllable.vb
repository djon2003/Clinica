Public Interface IControllable
    Inherits System.Windows.Forms.IContainerControl

    Event switchingControl(ByVal sender As IControllable, ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean)
    Property isSwitchedToToolbar(Optional ByVal showPanel As Boolean = True) As Boolean
    Property isClosed() As Boolean
    Overloads Property visible() As Boolean
    Event closing(ByVal sender As IControllable)
    Event barTitleChanged(ByVal sender As IControllable)
    ReadOnly Property hasToBlink() As Boolean
    Overloads Sub focus()

    Function getBarTitle() As String
End Interface
