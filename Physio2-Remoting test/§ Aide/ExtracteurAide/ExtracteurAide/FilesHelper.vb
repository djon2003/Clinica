Public Class FilesHelper

    Private Shared errors As New Text.StringBuilder
    Private Shared nbErrors As Integer = 0
    Private Shared nbLinks As Integer = 0

    Private Sub new()
    End Sub

    Public Shared Sub addHtmlPart(ByRef sb As Text.StringBuilder, ByVal fileName As String)
        If IO.File.Exists(fileName) = False Then Exit Sub

        Dim html As String = IO.File.ReadAllText(fileName)
        transferLinkedFiles(html)
        sb.Append(html)
    End Sub

    Public Shared Sub transferLinkedFiles(ByVal html As String)
        transferLinkedFilesByTag(html, "script", "src")
        transferLinkedFilesByTag(html, "img", "src")
        transferLinkedFilesByTag(html, "link", "href")
    End Sub

    Public Shared Function transferLinkedFilesByTag(ByVal html As String, ByVal tag As String, ByVal attrib As String, Optional ByVal replaceHash As Hashtable = Nothing, Optional ByVal curFilePath As String = "") As String
        Dim foundPos As Integer = html.IndexOf("<" & tag)
        Dim endPos1, endPos2, endTagPos, firstTagPos As Integer
        Dim sepChar As Char
        Dim filename As String = ""
        Dim previousArticle As String = ""
        Dim linkTitle As String = ""
        firstTagPos = foundPos
        While foundPos <> -1
            endTagPos = html.IndexOf(">", foundPos + 1)
            foundPos = html.IndexOf(attrib, foundPos + 1) + attrib.Length + 1
            If foundPos <> (attrib.Length) AndAlso foundPos < endTagPos Then
                sepChar = html.Substring(foundPos, 1)
                If sepChar <> """" AndAlso sepChar <> "'" Then
                    sepChar = " "
                Else
                    foundPos += 1
                End If

                endPos1 = html.IndexOf(sepChar, foundPos)
                endPos2 = html.IndexOf(">", foundPos)
                endPos1 = Math.Min(endPos1, endPos2)
                filename = html.Substring(foundPos, endPos1 - foundPos)

                If replaceHash IsNot Nothing Then
                    endPos2 += 1
                    endTagPos = html.IndexOf("<", endPos2)
                    linkTitle = html.Substring(endPos2, endTagPos - endPos2).ToLower

                    'If filename.IndexOf("#") <> -1 Then
                    filename = filename.Substring(filename.IndexOf("#") + 1).ToLower
                    previousArticle = ""
                    If filename.IndexOf("_") <> -1 Then previousArticle = filename.Substring(0, filename.LastIndexOf("_"))
                    Select Case True
                        Case replaceHash.ContainsKey(filename) AndAlso replaceHash(filename) = curFilePath
                            'Keep internal links functional
                        Case replaceHash.ContainsKey(filename)
                            html = html.Substring(0, foundPos) & replaceHash(filename) & html.Substring(endPos1)
                        Case replaceHash.ContainsKey(previousArticle)
                            html = html.Substring(0, foundPos) & replaceHash(previousArticle) & "#" & filename & html.Substring(endPos1)
                        Case html.IndexOf("name=""") <> -1
                        Case True
                            nbErrors += 1
                            errors.AppendLine("<tr><td>" & Extractor.curArt.Title & "</td><td><a href=" & Extractor.curArt.Path & ">" & Extractor.curArt.Path & "</a></td><td>" & filename & "</td><td><textarea cols=20 rows=3>" & html & "</textarea></td></tr>")
                    End Select

                    nbLinks += 1
                    'End If
                Else
                    filename = filename.Replace("/", "\")
                    If IO.File.Exists(dirToExtract & "\" & filename) = False Then
                        If IO.File.Exists(extractedParts & filename) Then IO.File.Copy(extractedParts & filename, dirToExtract & "\" & filename)
                        If IO.File.Exists(extractedParts.Substring(0, extractedParts.LastIndexOf("\", extractedParts.Length - 2)) & "\" & filename) Then
                            If filename.IndexOf("\") <> -1 Then IO.Directory.CreateDirectory(dirToExtract & "\" & filename.Substring(0, filename.LastIndexOf("\")))
                            IO.File.Copy(extractedParts.Substring(0, extractedParts.LastIndexOf("\", extractedParts.Length - 2)) & "\" & filename, dirToExtract & "\" & filename)
                        End If
                    End If
                End If
            Else
                foundPos = firstTagPos
            End If

            foundPos = html.IndexOf("<" & tag, foundPos + 1)
            firstTagPos = foundPos
        End While

        Return html
    End Function

    Public Shared Sub writeErrors()
        Dim sb As New Text.StringBuilder
        addHtmlPart(sb, extractedParts & "files_errors-entete.html")
        sb.Append(errors.ToString)
        sb.Append("</table><b>Total errors : ").Append(nbErrors).Append("<br>Total links :").Append(nbLinks).Append("</b></body></html>")
        IO.File.WriteAllText(dirToExtract & "\§file-errors.html", sb.ToString, fileEncoding)
    End Sub
End Class
