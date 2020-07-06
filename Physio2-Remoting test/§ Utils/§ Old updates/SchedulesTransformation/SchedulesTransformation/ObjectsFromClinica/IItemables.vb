Public Interface IItemables(Of Managed)
    Function addItemable(ByVal newItem As Managed) As String
    Function insertItemable(ByVal index As Integer, ByVal newItem As Managed) As String
    Sub removeItemable(ByVal delItem As Managed)
    Sub removeItemable(ByVal noItem As Integer)
    Function getItemable(ByVal noItem As Integer) As Managed
    Function getItemables() As Generic.List(Of Managed)
    Sub clear()

    ReadOnly Property count() As Integer
    ReadOnly Property managedType() As Type
End Interface


Public Interface IItemables
    Function addItemable(ByVal newItem As IItemable) As String
    Function insertItemable(ByVal index As Integer, ByVal newItem As IItemable) As String
    Sub removeItemable(ByVal delItem As IItemable)
    Sub removeItemable(ByVal noItem As Integer)
    Function getItemable(ByVal noItem As Integer) As IItemable
    Function getItemables() As Generic.List(Of IItemable)
    Sub clear()

    ReadOnly Property count() As Integer
    ReadOnly Property managedType() As Type
End Interface
