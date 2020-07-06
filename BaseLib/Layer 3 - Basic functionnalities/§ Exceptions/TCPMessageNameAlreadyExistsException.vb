Public Class TCPMessageNameAlreadyExistsException
    Inherits ExceptionBase

    Public Sub New(ByVal currentType As Type, ByVal conflictType As Type)
        MyBase.New(currentType.FullName & " and " & conflictType.FullName & " have the same NAME constant value")
    End Sub
End Class
