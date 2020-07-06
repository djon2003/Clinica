Imports System.Reflection


Public Class MethodEvaluator

    Private oExecInstance As Object
    Private methodName As String = ""

    Public Sub New(ByVal oExecInstance As Object, ByVal methodName As String)
        Me.oExecInstance = oExecInstance
        Me.methodName = methodName
    End Sub

    Public Function eval(ByVal params() As Object) As Object
        If oExecInstance Is Nothing Then Return Nothing

        Dim oRetObj As Object = Nothing
        Dim oMethodInfo As MethodInfo
        Dim oType As Type = oExecInstance.GetType

        Try
            oMethodInfo = oType.GetMethod(methodName)
            oRetObj = oMethodInfo.Invoke(oExecInstance, New Object() {params})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

        Return oRetObj
    End Function

End Class
