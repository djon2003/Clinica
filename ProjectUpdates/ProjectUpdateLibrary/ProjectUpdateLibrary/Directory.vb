''' <summary>
''' Copied from base library so that this lib doesn't need it support
''' </summary>
''' <remarks></remarks>

Public Class Directory
    Private Sub New()
    End Sub

    Public Shared Function isRemoteLocationLocal(ByVal remotePath As String, ByVal localPath As String) As Boolean
        Dim tempFile As String = getNewFileName(localPath, "§TEMPTESTREMOTELOCATION§")
        remotePath = remotePath & addSlash(remotePath) & tempFile
        localPath = localPath & addSlash(localPath) & tempFile
        Dim isLocal As Boolean = IO.File.Exists(remotePath)
        IO.File.Delete(localPath)

        Return isLocal
    End Function

    Public Shared Function getNewFileName(ByVal path As String, Optional ByVal firstName As String = "FileName") As String
        Dim emptyFile() As String = {}
        Dim myName As String
        Dim done As Boolean = False
        Dim no As Integer = 1
        myName = firstName

        IO.Directory.CreateDirectory(path)
        Do
            If IO.Directory.GetFiles(path, myName & ".*").Length = 0 Then
                IO.File.WriteAllText(path & addSlash(path) & myName, "")
                Return myName
            End If

            myName = firstName & "-" & no
            no += 1
        Loop Until done = True

        Return ""
    End Function
End Class
