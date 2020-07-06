Public Class ReportBodySimple
    Inherits ReportBasicBody

    Private myBodyFileName As String = ""
    Private myExtraFieldType As String = ""
    Private myExtraFieldQuestion As String = ""

#Region "Properties"
    Public Property bodyFileName() As String
        Get
            Return myBodyFileName
        End Get
        Set(ByVal value As String)
            myBodyFileName = value
        End Set
    End Property

    Public Property extraFieldType() As String
        Get
            Return myExtraFieldType
        End Get
        Set(ByVal value As String)
            myExtraFieldType = value
        End Set
    End Property

    Public Property extraFieldQuestion() As String
        Get
            Return myExtraFieldQuestion
        End Get
        Set(ByVal value As String)
            myExtraFieldQuestion = value
        End Set
    End Property
#End Region

    Public Sub New(ByRef report As Report)
        MyBase.New(report)
    End Sub

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        Dim myRapportModel() As String = readFile(myBodyFileName, , , False)
        Dim bodyFile As String = String.Join(vbCrLf, myRapportModel)
        Dim body As String = ""

        If mySQLStatement <> "" Then
            mySQLStatement = MyBase.transformSQLStatement(mySQLStatement)

            Dim myDataSet As New DataSet()
            myDataSet = DBLinker.getInstance.readDBForGrid(mySQLStatement, , , "Body", myDataSet, False)
            If myDataSet Is Nothing OrElse myDataSet.Tables("Body") Is Nothing Then Exit Sub

            Dim myColumn As DataColumn
            With myDataSet.Tables("Body")
                For i As Integer = 0 To .Rows.Count - 1
                    body = bodyFile
                    For Each myColumn In .Columns
                        Dim myRowStr As String
                        Try
                            myRowStr = .Rows(i)(myColumn.ColumnName)
                        Catch ex As Exception
                            myRowStr = ""
                        End Try
                        If colsFormat.ContainsKey(myColumn.Caption) Then myRowStr = myReport.formatCell(myRowStr, colsFormat(myColumn.Caption))
                        body = body.Replace("###" & myColumn.Caption & "###", myRowStr)
                    Next
                    htmlBuilder.Append(body)
                Next i
            End With
        End If

        'Apply Passif Filter
        If myExtraFieldType = "ASK" And myExtraFieldQuestion <> "" Then
            Dim myInputBoxPlus As New InputBoxPlus
            htmlBuilder.Replace("###ExtraField###", myInputBoxPlus(myExtraFieldQuestion, "Champs en extra"))
        ElseIf myExtraFieldType = "FILTER" Then
            Try
                With CType(myReport.reportFilter, FilterComposite)
                    htmlBuilder.Replace("###ExtraField###", CType(.Item(.indexOf("FilterPassif")), FilterPassive).passiveValue)
                End With
            Catch
            End Try
        Else
            htmlBuilder.Replace("###ExtraField###", "")
        End If
    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
        htmlBuilder.Append("<!-- BodyFileName=").Append(myBodyFileName).AppendLine(" -->")
        htmlBuilder.Append("<!-- ExtraFieldType=").Append(myExtraFieldType).AppendLine(" -->")
        htmlBuilder.Append("<!-- ExtraFieldQuestion=").Append(myExtraFieldQuestion).AppendLine(" -->")
    End Sub

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "BodyFileName"
                    bodyFileName = myKey.Value
                    bodyFileName = bodyFileName.Replace("###CLINICAPATH###", appPath & bar(appPath))
                Case "ExtraFieldType"
                    extraFieldType = myKey.Value
                Case "ExtraFieldQuestion"
                    extraFieldQuestion = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)
        properties.Add("BodyFileName", bodyFileName)
        properties.Add("ExtraFieldType", extraFieldType)
        properties.Add("ExtraFieldQuestion", extraFieldQuestion)

        MyBase.saveProperties(properties)
    End Sub

    Public Overloads Shared Function findBody(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportBody
        Dim myRapportElement As New ReportBodySimple(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other bodys
        Return ReportBodyTable.findBody(classElementName, leRapport, True, firstClass)
    End Function
End Class
