Public Class TMCreator
    Private tableMatiere As Generic.List(Of Article)

    Public Sub new(ByVal tableMatiere As Collections.Generic.List(Of Article))
        Me.tableMatiere = tableMatiere
    End Sub

    Private depth As Integer

    Private Function writeTableRow(ByRef sb As Text.StringBuilder, ByVal n As Integer) As Integer
        depth += 1
        Console.Write(depth & "-")
        Dim isOne As Boolean = False
        Dim isBack As Boolean = False
        Dim onclickScript As String = ""
        Dim leftMargin As Integer = 0

        Dim curArt As Article
        For i As Integer = n To tableMatiere.Count - 1
            If tableMatiere(n).title = "Banque de données" Then
                Dim a As Byte = 0
            End If
            curArt = tableMatiere(i)
            If curArt.level < tableMatiere(n).level Then Return i

            isOne = False
            If tableMatiere.Count <> i + 1 Then
                isOne = curArt.level < tableMatiere(i + 1).level
            End If

            If isOne Then
                onclickScript = " onclick=""javascript:if(this.innerHTML == '+') {this.innerHTML='-';} else {this.innerHTML='+';} ShowHidePart('" & curArt.Path & "', 0);"""
            End If

            sb.Append("<div style=""clear:both;"" " & IIf(isOne And 1 = 2, " onmouseover=""ShowHidePart('grow-" & curArt.Path & "', 1);"" onmouseout=""ShowHidePart('grow-" & curArt.Path & "', 1);""", "") & ">") _
            .Append("<span id=""grow-").Append(curArt.Path).Append("""").Append(onclickScript).Append(" style=""visibility:" & IIf(isOne, "", "hidden") & ";margin-right:5px;"">+</span> ") _
            .AppendLine("<a href=""" & curArt.Path & """ target=page>" & curArt.Title & "</a></div>")

            If isOne Then
                sb.AppendLine("<div name=""grouping"" class=""group"" id=""" & curArt.Path & """ style='display:none;clear:both;'>")
                i = writeTableRow(sb, i + 1) - 1
                depth -= 1
                sb.AppendLine("</div>")
                sb.AppendLine()
            End If
        Next i

        Return tableMatiere.Count
    End Function

    Public Sub writeTable()
        Dim sb As New Text.StringBuilder()
        FilesHelper.addHtmlPart(sb, extractedParts & "utf8-entete.html")

        sb.Append("<h1>Table des matières</h1>")
        writeTableRow(sb, 0)
        sb.Append("</body></html>")

        IO.File.WriteAllText(dirToExtract & "\0-table.html", sb.ToString)
    End Sub
End Class
