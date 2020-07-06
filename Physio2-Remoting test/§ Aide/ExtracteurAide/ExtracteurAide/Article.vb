Public Class Article
    Private _title As String = ""
    Private _path As String = ""
    Private _extractedHTML As String = ""
    Private _level As Integer
    Private _uniqueNumber As Integer

    Private Shared uniqueNumbersLink As New Hashtable


#Region "propriétés"
    Public ReadOnly Property title() As String
        Get
            Return _title
        End Get
    End Property

    Public ReadOnly Property path() As String
        Get
            Return _path
        End Get
    End Property

    Public ReadOnly Property level() As Integer
        Get
            Return _level
        End Get
    End Property

    Public ReadOnly Property uniqueNumber() As Integer
        Get
            Return _uniqueNumber
        End Get
    End Property
#End Region

    Public Sub new(ByVal title As String, ByVal path As String, ByVal extractedHTML As String, ByVal level As Integer)
        Console.WriteLine("Creating article <" & title & ">")

        _title = title.Trim
        _path = path
        _extractedHTML = extractedHTML
        _level = level

        extractAnchors()
        getUniqueNumber()
        correctHtml()
        FilesHelper.transferLinkedFiles(_extractedHTML)
    End Sub

    Private Sub extractAnchors()
        If _extractedHTML = "" Then Exit Sub

        Dim tag As String = "<a name="
        Dim pos As Integer = _extractedHTML.IndexOf(tag)
        While pos <> -1
            pos += tag.Length
            Dim link As String = _extractedHTML.Substring(pos, _extractedHTML.IndexOf(">", pos) - pos)
            If link.StartsWith("""") Or link.StartsWith("""") Then link = link.Substring(1, link.Length - 2)

            link = link.ToLower
            'link = "#" & link
            If Not uniqueNumbersLink.Contains(link) Then uniqueNumbersLink.Add(link, _path)

            pos = _extractedHTML.IndexOf("<a name=", pos)
        End While
    End Sub

    Private Sub correctHtml()
        If _extractedHTML = "" Then Exit Sub

        'Change title tag to H1
        Dim lastTagPos As Integer = _extractedHTML.IndexOf(">")
        _extractedHTML = "<h1" & _extractedHTML.Substring(lastTagPos)
        Dim lastTitlePos As Integer = _extractedHTML.IndexOf("</h")
        lastTagPos = _extractedHTML.IndexOf(">", lastTitlePos)
        _extractedHTML = _extractedHTML.Substring(0, lastTitlePos) & "</h1" & _extractedHTML.Substring(lastTagPos)

        'Remove anchor tags (Not sure good idea. Is internal page link still working?)
        REM Should not do that, so that links still works ... _extractedHTML = System.Text.RegularExpressions.Regex.Replace(_extractedHTML, "\<a name([^<>]+)\>(?<middle>([^<>]+))\<\/a\>", "${middle}")

        If _title = "Facture en souffrance" Then
            Dim a As Byte = 0
        End If

        REM 'Fonctionne partiellement. EX : Glossaire->Facture en souffrance : Il en reste tjs.
        'Remove text contained in tag which has this style : "background:lime" + Tag itself
        _extractedHTML = System.Text.RegularExpressions.Regex.Replace(_extractedHTML, "\<span ([^<>]|\n|\r)+background\:lime([^<>]|\n)*\>(?<middle>([^<>]+(\<?[^s]?[^p]?[^a]?[^n]?[^<>]*\>?)?[^<>]*(\<?\/?[^s]?[^p]?[^a]?[^n]?[^<>]*\>)?)[^<>]*)\<\/span\>", "", Text.RegularExpressions.RegexOptions.Singleline Or Text.RegularExpressions.RegexOptions.IgnoreCase)
    End Sub

    Private Sub getUniqueNumber()
        Dim tmpHTML As String = _extractedHTML.Replace(vbCrLf, "").Replace(" ", "")
        Dim textToFind As String = "<spanstyle='display:none'>Numéro&nbsp;:"
        Dim firstPos As Integer = tmpHTML.IndexOf(textToFind) + textToFind.Length
        If firstPos = textToFind.Length - 1 Then Exit Sub

        Dim lastPos As Integer = tmpHTML.IndexOf("</", firstPos)

        _uniqueNumber = tmpHTML.Substring(firstPos, lastPos - firstPos)

        If _path = "2.html" Then
            Dim a As Byte = 0
        End If


        textToFind = "<aname="
        firstPos = tmpHTML.IndexOf(textToFind) + textToFind.Length
        If firstPos = textToFind.Length - 1 Then Exit Sub
        lastPos = tmpHTML.IndexOf(">", firstPos)


        Dim linkName As String = tmpHTML.Substring(firstPos, lastPos - firstPos).ToLower.Replace("""", "")
        If uniqueNumbersLink.ContainsKey(linkName) = False Then uniqueNumbersLink.Add(linkName, _path)
        'If uniqueNumbersLink.ContainsKey(_title.ToLower) = False Then uniqueNumbersLink.Add(_title.ToLower, _path)
    End Sub

    Public Sub write()
        changeLinks()

        Dim sb As New Text.StringBuilder()
        FilesHelper.addHtmlPart(sb, extractedParts & "def-entete.html")
        sb.Append(_extractedHTML)
        sb.Append("</body></html>")
        IO.File.WriteAllText(dirToExtract & "\" & Path, sb.ToString, fileEncoding)

        Console.WriteLine("Writing article <" & _title & "> to :" & vbCrLf & dirToExtract & "\" & Path)
    End Sub

    Public Sub changeLinks()
        If _path = "5.html" Then
            Dim a As Byte = 0
        End If

        _extractedHTML = FilesHelper.transferLinkedFilesByTag(_extractedHTML, "a", "href", uniqueNumbersLink, _path)
    End Sub
End Class
