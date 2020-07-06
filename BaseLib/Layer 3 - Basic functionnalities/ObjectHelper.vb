Imports System.Reflection

Public Class ObjectHelper
    Private Sub New()
    End Sub

    'Taken from http://stackoverflow.com/a/24564897/214898
    Public Shared Function hasMethod(ByVal objectToCheck As Object, ByVal methodName As String) As Boolean
        Try
            Dim myType As Object = objectToCheck.GetType()
            Return myType.GetMethod(methodName) IsNot Nothing
        Catch ex As AmbiguousMatchException
            ' ambiguous means there is more than one result,
            ' which means: a method with that name does exist
            Return True
        End Try
    End Function

    Public Shared Function hasProperty(ByVal objectToCheck As Object, ByVal methodName As String) As Boolean
        Try
            Dim myType As Type = objectToCheck.GetType()
            Return myType.GetProperty(methodName) IsNot Nothing
        Catch ex As AmbiguousMatchException
            ' ambiguous means there is more than one result,
            ' which means: a method with that name does exist
            Return True
        End Try
    End Function
End Class
