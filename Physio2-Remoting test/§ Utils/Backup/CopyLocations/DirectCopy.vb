Public Class DirectCopy
    Inherits CopyLocation

    Public Overrides Sub copy()
        If Me.sourceDataPath = "" OrElse IO.File.Exists(Me.sourceDataPath) = False Then Throw New Exception("SourceDataPath is invalid")
        'If Me.LocationPath = "" OrElse IO.Directory.Exists(Me.LocationPath) = False Then Throw New Exception("LocationPath is invalid")

        Dim targetPath As String = Me.locationPath & IIf(Me.locationPath.EndsWith("\"), "", "\") & "Backup-Clinica"

        Dim sourceFile As New IO.FileInfo(Me.sourceDataPath)
        If Me.keepOnlyOneCopy AndAlso IO.Directory.Exists(targetPath) Then
            For Each curFile As String In IO.Directory.GetFiles(targetPath, "*.zip")
                If curFile.EndsWith("\" & sourceFile.Name) = False Then IO.File.Delete(curFile)
            Next
        End If
        IO.Directory.CreateDirectory(targetPath)

        Try
            'SourceFile.CopyTo(targetPath & "\" & SourceFile.Name, True)
            copyFile(sourceFile.FullName, targetPath & "\" & sourceFile.Name)
        Catch ex As Exception
            Throw New Exception("Impossible to copy on target path : " & targetPath, ex)
        End Try

        Dim compared As Boolean = compareFile(Me.sourceDataPath, targetPath & "\" & sourceFile.Name)
        If compared = False Then Throw New Exception("Comparaison is not valid for target path : " & targetPath)
    End Sub
End Class
