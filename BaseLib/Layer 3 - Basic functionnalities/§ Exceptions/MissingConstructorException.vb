Public Class MissingConstructorException
    Inherits ExceptionBase

    Private Const CONSTRUCTOR_TYPES_SEPARATOR As String = ", "

    Public Sub New(ByVal [type] As Type, ByVal scope As System.Reflection.MethodAttributes, ByVal constructorTypes As System.Type())
        MyBase.New(type.FullName & " has to have a constructor with a " & extractScopeName(scope) & " scope with those parameters : " & listConstructorParameters(constructorTypes))
    End Sub

    Private Shared Function extractScopeName(ByVal scope As System.Reflection.MethodAttributes) As String
        Dim scopeName As String = scope.ToString()
        Return scopeName.Substring(scopeName.IndexOf(",") + 1).ToLower()
    End Function

    Private Shared Function listConstructorParameters(ByVal constructorTypes As System.Type()) As String
        Dim types As String = String.Empty
        For Each curType As Type In constructorTypes
            types &= CONSTRUCTOR_TYPES_SEPARATOR & curType.FullName
        Next
        If types <> String.Empty Then types = types.Substring(CONSTRUCTOR_TYPES_SEPARATOR.Length)

        Return types
    End Function

End Class
