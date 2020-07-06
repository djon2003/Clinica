Public Class FieldValidationException
    Inherits Exception

    Public fontColor As String
    Public isError As Boolean = True

    Public Sub New(ByVal message As String, ByVal isError As Boolean, Optional ByVal fontColor As String = CsstTask.RESULT_ERROR_COLOR)
        MyBase.New(message)

        Me.isError = isError
        Me.fontColor = fontColor
    End Sub

End Class
