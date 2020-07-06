Public Class FolderChangedEventArgs
    Inherits EventArgs

    Private _oldFolder As String = ""
    Private _oldUser As Integer = 0

    Public ReadOnly Property oldFolder() As String
        Get
            Return _oldFolder
        End Get
    End Property

    Public ReadOnly Property oldUser() As String
        Get
            Return _oldUser
        End Get
    End Property

    Public Sub New(ByVal oldFolder As String, ByVal oldUser As Integer)
        _oldFolder = oldFolder
        _oldUser = oldUser
    End Sub
End Class
