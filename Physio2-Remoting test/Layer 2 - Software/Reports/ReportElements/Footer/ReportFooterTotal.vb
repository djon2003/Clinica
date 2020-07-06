Public Class ReportFooterTotal
    Inherits ReportBasicFooter

    Private myFooterFileName As String = ""

    Public Sub New(ByRef rapport As Report)
        MyBase.New(rapport)
    End Sub

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        Dim htmlTemplate As String = String.Empty
        If IO.File.Exists(myFooterFileName) Then htmlTemplate = IO.File.ReadAllText(myFooterFileName)

        Dim sumFooterHtmlBuilder As New System.Text.StringBuilder()
        Dim linesFooterHtmlBuilder As New System.Text.StringBuilder()

        Dim i As Integer
        Dim myTotaux As Hashtable
        Dim myColsFormat As Hashtable
        Dim myColsName() As String
        Dim myGroupColsName() As String
        Dim myShowedCols() As String
        Dim myNbColumns As Integer = 0
        Dim showLine As Boolean
        Dim addUpCells As Boolean
        Dim nbGroupSubTotal As Integer = -1
        Dim subTotalColumnsType As Hashtable
        Try
            With CType(myReport.body, ReportBodyTable)
                myTotaux = .totals
                myColsFormat = .colsFormat
                myColsName = .columnsName
                myNbColumns = .nbColumns - .nbGroupColumns - 1
                myGroupColsName = .groupColumnsAlias
                myShowedCols = .subTotalColumnsName
                showLine = .showSubTotalLine
                addUpCells = .addUpSubTotalLine
                nbGroupSubTotal = .nbGroupColumnsSubTotal
                subTotalColumnsType = .subTotalColumnsType
            End With
        Catch
            Exit Sub
        End Try

        If Not myColsName Is Nothing AndAlso myColsName.Length <> 0 Then sumFooterHtmlBuilder.Append("<tr class=FooterFirstLine><td class=GrandTotalCell colspan=" & (myColsName.GetUpperBound(0) + 1) & ">Grand total :</td></tr>")

        Dim maxCells As Integer = 0
        If myColsName IsNot Nothing Then maxCells = myColsName.GetUpperBound(0)
        If maxCells > myNbColumns AndAlso myNbColumns >= 0 Then maxCells = myNbColumns
        If Not myColsName Is Nothing AndAlso myColsName.Length <> 0 And showLine Then
            sumFooterHtmlBuilder.Append("<tr>")
            Dim myColsSum As Double = 0
            Dim lastCol As Integer
            For i = 0 To maxCells
                Dim myCell As String = ""
                If searchArray(myShowedCols, myColsName(i), SearchType.ExactMatch) >= 0 Then
                    myCell = myTotaux(myColsName(i))
                    If subTotalColumnsType.ContainsKey(myColsName(i)) Then
                        Select Case subTotalColumnsType(myColsName(i))
                            Case "AVG"
                                myCell /= myTotaux("§TABLEROWCOUNT§")
                        End Select
                    End If
                    myCell = myReport.formatCell(myCell, myColsFormat(myColsName(i)))
                    myColsSum += myTotaux(myColsName(i))
                    lastCol = i
                End If
                sumFooterHtmlBuilder.Append("<td class=FooterTotalCell" & myReport.alignCell(myColsFormat(myColsName(i))) & ">" & myCell & "</td>")
            Next i
            sumFooterHtmlBuilder.Append("</tr>")
            If addUpCells Then
                sumFooterHtmlBuilder.Append("<tr><td class=FooterTotalLine colspan=" & lastCol + 1 & ">Total ")
                sumFooterHtmlBuilder.Append(myReport.formatCell(myColsSum, myColsFormat(myColsName(lastCol))))
                sumFooterHtmlBuilder.AppendLine("</td><td colspan=" & myColsName.GetUpperBound(0) - lastCol & ">&nbsp;</td></tr>")
            End If
        End If

        sumFooterHtmlBuilder.AppendLine("</table>")

        linesFooterHtmlBuilder.AppendLine("<table border=0 id=baseTotals>")
        linesFooterHtmlBuilder.Append("<tr class=FooterLastLines><td class=FooterTotalCell>Total des entrées</td><td width=5 align=center class=FooterTotalCell>:</td><td class=FooterTotalCell>" & myTotaux("§TABLEROWCOUNT§") & "</td></tr>")
        If Not myGroupColsName Is Nothing AndAlso myGroupColsName.Length <> 0 Then
            Dim sColName() As String
            If nbGroupSubTotal = -1 Then
                nbGroupSubTotal = myGroupColsName.GetUpperBound(0)
            ElseIf nbGroupSubTotal = 0 Then
                nbGroupSubTotal = -1
            Else
                nbGroupSubTotal = nbGroupSubTotal - 1
            End If

            For i = nbGroupSubTotal To 0 Step -1
                sColName = myGroupColsName(i).ToLower.Split(New String() {" "}, StringSplitOptions.None)
                sColName(0) = sColName(0) & "s"
                linesFooterHtmlBuilder.Append("<tr class=FooterLastLines><td class=FooterTotalCell>Total des " & String.Join(" ", sColName) & "</td><td width=5 align=center class=FooterTotalCell>:</td><td class=FooterTotalCell>" & myReport.formatCell(myTotaux(myGroupColsName(i)), colsFormat(myGroupColsName(i))) & "</td></tr>")
            Next i
        End If

        linesFooterHtmlBuilder.AppendLine("</table>")

        If htmlTemplate <> String.Empty Then
            htmlTemplate = htmlTemplate.Replace("###SUMTABLEFOOTER###", sumFooterHtmlBuilder.ToString())
            htmlTemplate = htmlTemplate.Replace("###SUMOFLINES###", linesFooterHtmlBuilder.ToString())
            htmlBuilder.Append(htmlTemplate)
        Else
            htmlBuilder.Append(sumFooterHtmlBuilder.ToString())
            htmlBuilder.Append(linesFooterHtmlBuilder.ToString())
        End If
    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
    End Sub

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "FooterFileName"
                    myFooterFileName = myKey.Value
                    myFooterFileName = myFooterFileName.Replace("###CLINICAPATH###", appPath & bar(appPath))
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        MyBase.saveproperties(properties)
    End Sub

    Public Overloads Shared Function findFooter(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportFooter
        Dim myRapportElement As New ReportFooterTotal(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other footers
        Return ReportFooterSimple.findFooter(classElementName, leRapport, True, firstClass)
    End Function
End Class
