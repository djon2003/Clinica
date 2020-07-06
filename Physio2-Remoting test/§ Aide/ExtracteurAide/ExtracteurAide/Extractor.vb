Public Class Extractor

    Private filename As String
    Private curPos As Integer
    Private tableMatiere As New System.Collections.Generic.List(Of Article)
    Public Shared curArt As Article

    Public Sub new(ByVal filename As String)
        Me.filename = filename
    End Sub

    Public Sub extract()
        If IO.Directory.Exists(dirToExtract) Then IO.Directory.Delete(dirToExtract, True)

        IO.Directory.CreateDirectory(dirToExtract)

        copyBasesFiles()

        Dim text As String = IO.File.ReadAllText(filename, fileEncoding)  ', 
        writeBaseCss(text)

        'Extract pages from the HTML file
        curPos = text.IndexOf("<h1>", StringComparison.OrdinalIgnoreCase) + 1
        extractPages(text, 1, "")
        ensureLinks()

        'Create the table of content
        Dim tmArt As New Article("Table des matières", "§table.html", "", 1)
        curArt = tmArt
        Dim myTMCreator As New TMCreator(Me.tableMatiere)
        myTMCreator.writeTable()

        'Write pages and errors
        writePages()
        FilesHelper.writeErrors()

        createXMLDatafile()
    End Sub

    Private Sub ensureLinks()
        For Each curArt As Article In Me.tableMatiere
            If curArt.Path = "3.html" Then
                Dim a As Integer = 0
            End If
            curArt.changeLinks()
        Next
    End Sub

    Private Sub writePages()
        For i As Integer = 0 To tableMatiere.Count - 1
            curArt = tableMatiere(i)
            tableMatiere(i).write()
        Next i
    End Sub

    Private Sub copyBasesFiles()
        Dim files() As String = IO.Directory.GetFiles(extractedParts, "0-*.*")
        For i As Integer = 0 To files.Length - 1
            IO.File.Copy(files(i), dirToExtract & files(i).Substring(files(i).LastIndexOf("\")))
        Next i
    End Sub

    Private Sub writeBaseCss(ByRef text As String)
        'Écrit le fichier css provenant de word
        Dim endPos As Integer
        curPos = text.IndexOf("<style") + 13
        endPos = text.IndexOf("</style>")
        IO.File.WriteAllText(dirToExtract & "\base.css", text.Substring(curPos, endPos))
    End Sub

    Private Function findLastPagePos(ByRef text As String, ByVal startPagePos As Integer) As Integer
        'Trouve la position de fin d'un article
        Dim endPagePos As Integer
        Dim found As Boolean = False
        While found = False
            endPagePos = text.IndexOf("<h", startPagePos, StringComparison.OrdinalIgnoreCase)
            If endPagePos = -1 Then
                endPagePos = text.Length
                found = True
            Else
                If Integer.TryParse(text.Substring(endPagePos + 2, 1), 0) = False Then
                    startPagePos += 1
                    Continue While
                End If
                found = True
            End If
        End While

        Return endPagePos
    End Function

    Private Sub createXMLDatafile()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("<helpArticles>")
        For Each curArt As Article In tableMatiere
            sb.AppendLine("<helpArticle>")
            sb.AppendLine("<UniqueNumber>" & curArt.UniqueNumber & "</UniqueNumber>")
            sb.AppendLine("<Title>" & curArt.Title.Replace("&nbsp;", " ") & "</Title>")
            sb.AppendLine("<Level>" & curArt.Level & "</Level>")
            sb.AppendLine("<Path>" & curArt.Path & "</Path>")
            sb.AppendLine("</helpArticle>")
        Next
        sb.AppendLine("</helpArticles>")

        IO.File.WriteAllText(dirToExtract & "\§data.xml", sb.ToString)
    End Sub

    Private Sub extractPages(ByRef text As String, ByVal level As Byte, ByVal previousPath As String)
        Dim firstTitlePos, prevTitlePos As Integer
        Dim endTitlePos, endPagePos As Integer
        Dim n As Integer = 1

        firstTitlePos = text.IndexOf("<h" & level & ">", curPos, StringComparison.OrdinalIgnoreCase)
        prevTitlePos = text.IndexOf("<h" & (level - 1) & ">", curPos, StringComparison.OrdinalIgnoreCase)
        If prevTitlePos <> -1 AndAlso prevTitlePos < firstTitlePos Then firstTitlePos = -1

        While firstTitlePos <> -1
            endTitlePos = text.IndexOf("</h" & level & ">", firstTitlePos, StringComparison.OrdinalIgnoreCase) + 5
            endPagePos = findLastPagePos(text, endTitlePos)

            'Crée l'article
            Dim title As String = System.Text.RegularExpressions.Regex.Replace(text.Substring(firstTitlePos + 4, endTitlePos - firstTitlePos - 9), "\<\\?[^<>]+\>", "").Replace(vbCrLf, " ")
            Dim newArt As New Article(title, previousPath & n & ".html", text.Substring(firstTitlePos, endPagePos - firstTitlePos), level)
            curArt = newArt
            tableMatiere.Add(newArt)
            curPos = endPagePos

            'Vérifie les sous-sections
            If level < 7 Then extractPages(text, level + 1, previousPath & n & "-")

            'Trouve la prochaine position
            firstTitlePos = text.IndexOf("<h" & level & ">", curPos, StringComparison.OrdinalIgnoreCase)
            prevTitlePos = text.IndexOf("<h" & (level - 1) & ">", curPos, StringComparison.OrdinalIgnoreCase)
            If prevTitlePos <> -1 AndAlso prevTitlePos < firstTitlePos Then firstTitlePos = -1

            n += 1
        End While
    End Sub
End Class
