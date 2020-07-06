Public Class MailsImporter
    Inherits BasicErrorsImporter


    Public Overloads Overrides Sub import(ByVal folder As String, ByVal recursive As Boolean)
        Dim files() As String = System.IO.Directory.GetFiles(folder, "*.eml", IIf(recursive, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
        For Each curFile As String In files
            import(curFile)
        Next
    End Sub

    Public Overloads Overrides Sub import(ByVal filename As String)
        Dim fileContent As String = IO.File.ReadAllText(filename)

        Dim errors() As String = fileContent.Split(New String() {"--------------------------------------------------------------------"}, StringSplitOptions.RemoveEmptyEntries)
        For Each curError As String In errors
            importOneError(curError)
        Next
    End Sub
End Class
