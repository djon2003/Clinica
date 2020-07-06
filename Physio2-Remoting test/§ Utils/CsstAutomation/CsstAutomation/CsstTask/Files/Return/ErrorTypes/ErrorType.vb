Namespace ErrorTypes

    Public MustInherit Class ErrorType

        Private Const CREATION_METHOD_NAME As String = "createErrorType"

        Private Shared types As New List(Of Type)

        Protected csstError As CSSTResponseError

        Public Sub New(ByVal csstError As CSSTResponseError)
            Me.csstError = csstError
        End Sub

        Public MustOverride Sub manageError(ByVal input As ErrorInput)

        Private Shared Sub loadTypes()
            Dim baseClassType As Type = GetType(ErrorType)
            Dim curNamespace As String = baseClassType.Namespace

            If types.Count = 0 Then
                Try
                    Dim types() As Type = baseClassType.Assembly.GetTypes()
                    For Each curType As Type In types
                        If curType.Equals(baseClassType) = False AndAlso curType.Namespace = curNamespace Then
                            ErrorTypes.ErrorType.types.Add(curType)
                        End If
                    Next
                Catch ex As System.Reflection.ReflectionTypeLoadException
                    Base.External.propagateErrorLog(ex)
                    Throw ex
                End Try
            End If
        End Sub

        Public Shared Function createErrorType(ByVal errorCode As CSSTResponseError) As ErrorType
            loadTypes()

            Dim foundError As ErrorType = Nothing

            For Each curType As Type In types
                foundError = curType.InvokeMember(CREATION_METHOD_NAME, Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, New Object() {errorCode})
                If foundError IsNot Nothing Then Exit For
            Next

            Return foundError
        End Function
    End Class

End Namespace