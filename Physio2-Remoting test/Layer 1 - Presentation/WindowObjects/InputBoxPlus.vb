Public Class InputBoxPlus
    Inherits CI.Base.InputBoxPlus

    Private isCombo As Boolean = False

    Public Sub New(Optional ByVal isComboBox As Boolean = False, Optional ByVal pathList As String = "", Optional ByVal dbField As String = "", Optional ByVal whereSource As String = "", Optional ByVal doComboDelete As Boolean = True)
        MyBase.New(isComboBox, ensureFullPathList(pathList), dbField, whereSource, doComboDelete)
    End Sub

    Private Shared Function ensureFullPathList(ByVal pathList As String) As String
        If pathList = String.Empty Then
            Return String.Empty
        ElseIf pathList.IndexOf(":") <> -1 Then
            Return pathList
        Else
            Return appPath & bar(appPath) & pathList
        End If
    End Function

End Class
