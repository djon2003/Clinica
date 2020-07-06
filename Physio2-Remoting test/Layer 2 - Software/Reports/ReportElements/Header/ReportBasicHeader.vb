Imports CI.Clinica.DateFormat
Imports CI.Base

Public Class ReportBasicHeader
    Implements ReportHeader

    Protected myReport As Report
    Private _StyleFileName() As String
    Private _ColsFormat As New Hashtable
    Private ownWorkingThread As Threading.Thread

#Region "Properties"
    Public ReadOnly Property name() As String Implements ReportElement.name
        Get
            Return Me.GetType.Name
        End Get
    End Property

    Public Property colsFormat() As Hashtable
        Get
            Return _ColsFormat
        End Get
        Set(ByVal value As Hashtable)
            _ColsFormat = value
        End Set
    End Property

    Public ReadOnly Property filtersWhereString() As String Implements ReportElement.filtersWhereString
        Get
            If myReport Is Nothing OrElse myReport.reportFilter Is Nothing Then Return ""

            Return myReport.reportFilter.filterResult.filteringWhereHeader
        End Get
    End Property

    Public Property styleFileName() As String() Implements ReportElement.styleFileName
        Get
            Return _StyleFileName
        End Get
        Set(ByVal value As String())
            _StyleFileName = value
        End Set
    End Property
#End Region

    Public Sub New(ByRef curReport As Report)
        myReport = curReport
    End Sub

    Public Overridable Function generateHTML() As String Implements IHTMLGenerator.generateHTML
        Dim htmlBuilder As New System.Text.StringBuilder()

        generateHTMLFirstLine(htmlBuilder)
        generateHTMLProperties(htmlBuilder)
        generateHTMLMiddleLines(htmlBuilder)
        generateHTMLLastLine(htmlBuilder)

        RaiseEvent htmlGenerated(htmlBuilder.ToString, True)

        Return htmlBuilder.ToString
    End Function

    Private Sub internalGeneration()
        Try
            Dim htmlBuilder As New System.Text.StringBuilder()

            generateHTMLFirstLine(htmlBuilder)
            generateHTMLProperties(htmlBuilder)
            generateHTMLMiddleLines(htmlBuilder)
            generateHTMLLastLine(htmlBuilder)

            RaiseEvent htmlGenerated(htmlBuilder.ToString, True)
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Protected Overridable Sub generateHTMLFirstLine(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLFirstLine
        htmlBuilder.AppendLine("<!-- Rapport Header -->")
        htmlBuilder.AppendLine("<!-- Rapport Type=" & Me.GetType.Name & "-->")
    End Sub

    Protected Overridable Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLMiddleLines
    End Sub

    Protected Overridable Sub generateHTMLLastLine(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLLastLine
        htmlBuilder.AppendLine("<!-- End Rapport Header -->")
    End Sub

    Protected Overridable Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder) Implements ReportElement.generateHTMLProperties
        If _StyleFileName IsNot Nothing Then
            For i As Integer = 0 To _StyleFileName.GetLength(0) - 1
                Dim cssContent As String = IO.File.ReadAllText(_StyleFileName(i))
                htmlBuilder.Append("<style>").Append(cssContent).Append("</style>")
                If _StyleFileName(i) <> "" Then
                    Dim printStyleSheet As String = _StyleFileName(i).Replace(".css", "-print.css")
                    If IO.File.Exists(printStyleSheet) Then
                        htmlBuilder.Append("<link href=""").Append(printStyleSheet).AppendLine(""" rel=""stylesheet"" type=""text/css"" media=""print"">")
                    End If
                End If
            Next i
            htmlBuilder.Append("<!-- StyleFileName={").Append(String.Join("£", _StyleFileName)).AppendLine("} -->")
        End If
        htmlBuilder.AppendLine("<!-- FiltersWhereString=" & filtersWhereString & " -->")
    End Sub

    Public Overridable Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements ReportElement.loadProperties
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "StyleFileName"
                    If TypeOf myKey.Value Is Array Then
                        styleFileName = myKey.Value
                    Else
                        styleFileName = New String() {myKey.Value}
                    End If
                    For i As Integer = 0 To _StyleFileName.GetUpperBound(0)
                        _StyleFileName(i) = _StyleFileName(i).Replace("###CLINICAPATH###", appPath & bar(appPath))
                    Next i
                Case "ColsFormat"
                    Dim cols() As String = myKey.Value
                    Dim i As Integer
                    For i = 0 To cols.GetUpperBound(0)
                        Dim sCol() As String = cols(i).Split(New Char() {"="})
                        colsFormat.Add(sCol(0), sCol(1))
                    Next i
                Case Else
            End Select
        Next
    End Sub

    Public Overridable Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements ReportElement.saveProperties
        properties.Add("StyleFileName", styleFileName)
        properties.Add("FiltersWhereString", filtersWhereString)
        properties.Add("ColsFormat", colsFormat)
    End Sub

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString
        Dim prop As New Hashtable
        Me.saveProperties(prop)

        Return PropertiesHelper.transformProperties(prop)
    End Function

    Public Shared Function findHeader(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportHeader
        Dim myRapportElement As New ReportBasicHeader(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other footers
        Return ReportHeaderSimple.findHeader(classElementName, leRapport, firstDone, firstClass)
    End Function

    Public Event htmlGenerated(ByVal html As String, ByVal ishtmlGenerated As Boolean) Implements IHTMLGenerator.htmlGenerated

    Public Sub startHTMLGeneration() Implements IHTMLGenerator.startHTMLGeneration
        'Quitte si la génération est déjà en cours
        If Not ownWorkingThread Is Nothing Then
            If ownWorkingThread.IsAlive Then Exit Sub
        Else
            ownWorkingThread = New Threading.Thread(AddressOf internalGeneration)
        End If

        ownWorkingThread.IsBackground = True
        ownWorkingThread.Start()
    End Sub

    Public Sub stopHTMLGeneration() Implements IHTMLGenerator.stopHTMLGeneration
        ownWorkingThread = Nothing
    End Sub
End Class
