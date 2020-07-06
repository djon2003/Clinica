Public Class TCPMessageMissingNameException
    Inherits ExceptionBase

    Public Sub New(ByVal [type] As Type)
        MyBase.New(type.FullName & " is missing a public constant called NAME")
    End Sub

End Class
