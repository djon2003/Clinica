Imports CI.Base

Public Class Report
    Implements IHTMLGenerator, IPrintable

    Public Enum RapportParts
        All = 1
        Header = 2
        Body = 3
        Footer = 4
        Header_Body = 5
        Header_Footer = 6
        Body_Footer = 7
    End Enum

    Private myHeader As ReportHeader
    Private myFooter As ReportFooter
    Private myBody As ReportBody
    Private _FileName As String = ""
    Private _ReportTitle As String = ""
    Private _ReportFilters As String = ""
    Private myHTML As String = ""
    Private myReportWidth As RapportSizes
    Private myReportFilter As ReportFilter
    Private ownWorkingThread As Threading.Thread
    Private _ReportBodyOrder As String = ""
    Private _ReportType As ReportType
    Private _Name As String = Me.GetType().ToString()
    Private _CustomVariables As New Hashtable
    Private _Filtered As Boolean = False

    Public Event htmlGenerated(ByVal html As String, ByVal ishtmlGenerated As Boolean) Implements IHTMLGenerator.htmlGenerated

#Region "Class RapportSizes"
    Public Class RapportSizes
        Private mySizeType As String
        Private mySize As Double

        Public Sub New(ByVal pourcentage As Byte)
            mySize = pourcentage
            mySizeType = "%"
        End Sub

        Public Sub New(ByVal pouces As Double)
            mySize = pouces
            mySizeType = "in"
        End Sub

        Public Shadows Function toString() As String
            Return mySize.ToString & mySizeType
        End Function
    End Class
#End Region

#Region "Constructeurs"
    Public Sub New()
        myReportWidth = New RapportSizes(CByte(100))
    End Sub

    Public Sub New(ByVal fileName As String)
        myReportWidth = New RapportSizes(CByte(100))
    End Sub

    Public Sub New(ByVal header As ReportHeader, ByVal body As ReportBody, ByVal footer As ReportFooter)
        myReportWidth = New RapportSizes(CByte(100))

        myHeader = header
        myFooter = footer
        myBody = body
    End Sub
#End Region

#Region "Propriétés"
    Public Property filtered() As Boolean
        Get
            Return _Filtered
        End Get
        Set(ByVal value As Boolean)
            _Filtered = value
        End Set
    End Property

    Public Property name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property reportBodyOrder() As String
        Get
            Return _ReportBodyOrder
        End Get
        Set(ByVal value As String)
            _ReportBodyOrder = value
        End Set
    End Property

    Public ReadOnly Property getHTML() As String
        Get
            Return myHTML
        End Get
    End Property

    Public Property width() As RapportSizes
        Get
            Return myReportWidth
        End Get
        Set(ByVal value As RapportSizes)
            myReportWidth = value
        End Set
    End Property

    Public Property fileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    Public Property reportTitle() As String
        Get
            Return _ReportTitle
        End Get
        Set(ByVal value As String)
            _ReportTitle = value
        End Set
    End Property

    Public Property reportFilters() As String
        Get
            Return _ReportFilters
        End Get
        Set(ByVal value As String)
            _ReportFilters = value
        End Set
    End Property

    Public Property reportFilter() As ReportFilter
        Get
            Return myReportFilter
        End Get
        Set(ByVal value As ReportFilter)
            myReportFilter = value
        End Set
    End Property

    Public Property body() As ReportBody
        Get
            Return myBody
        End Get
        Set(ByVal value As ReportBody)
            myBody = value
        End Set
    End Property

    Public Property header() As ReportHeader
        Get
            Return myHeader
        End Get
        Set(ByVal value As ReportHeader)
            myHeader = value
        End Set
    End Property

    Public Property footer() As ReportFooter
        Get
            Return myFooter
        End Get
        Set(ByVal value As ReportFooter)
            myFooter = value
        End Set
    End Property

    Public Property reportType() As ReportType
        Get
            Return _ReportType
        End Get
        Set(ByVal value As ReportType)
            _ReportType = value
        End Set
    End Property
#End Region

#Region "Public functions"
    'KeyName must be in the form : ###KEYNAME###
    Public Sub addCustomVariable(ByVal keyName As String, ByVal value As String)
        If _CustomVariables.ContainsKey(keyName) Then
            _CustomVariables(keyName) = value
        Else
            _CustomVariables.Add(keyName, value)
        End If
    End Sub

    'KeyName must be in the form : ###KEYNAME###
    Public Function getCustomVariable(ByVal keyName As String) As String
        If _CustomVariables.ContainsKey(keyName) = False Then Return ""

        Return _CustomVariables(keyName)
    End Function

    Public Sub clearCustomVariables()
        _CustomVariables.Clear()
    End Sub

    Public Function replaceCustomVariables(ByVal input As String) As String
        Dim variablesMatches As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(input, "\#\#\#[0-9a-zA-Z-]+\#\#\#")
        For Each match As System.Text.RegularExpressions.Match In variablesMatches
            Dim var As String = getCustomVariable(match.Value)
            If var <> "" Then input = input.Replace(match.Value, var)
        Next

        Return input
    End Function

    Public Overrides Function toString() As String
        Return _ReportTitle
    End Function

    Public Function generateHTML() As String Implements IHTMLGenerator.generateHTML
        internalGeneration()

        Return myHTML
    End Function

    Public Function loadFromFile(Optional ByVal fileName As String = "") As String
        If fileName = "" Then fileName = _FileName
        If fileName = "" Then Return "Nom du fichier manquant"
        If IO.File.Exists(fileName) = False Then Return "Fichier inexistant"

        Dim myRapportFile() As String = readFile(fileName, , , False)



        Return ""
    End Function

    Public Function saveToFile(Optional ByVal fileName As String = "") As String
        If fileName = "" Then fileName = _FileName
        If fileName = "" Then Return "Nom du fichier manquant"

        If myHTML = "" Then generateHTML()

        writeFile(fileName, New String() {myHTML}, , , , False)
        Return ""
    End Function

    Private Sub internalFileSaving(ByVal type As String, ByVal filePath As String)
        saveToFile(filePath)

        'Dim noDBItem As String = filePath.Substring(filePath.LastIndexOf("\") + 1)
        'noDBItem = noDBItem.Substring(0, noDBItem.IndexOf("."))
        'DBLinker.getInstance().updateDB("DBItems", "SearchableContent=dbo.fnGetDBItemContent(NoDBItem)", "NoDBItem", noDBItem, False)
    End Sub

    Public Function saveToDB(ByVal myNom As String, ByVal myPath As String, ByVal myMotsCles() As String, ByVal myDescription() As String) As String
        AddHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
        If InternalDBManager.getInstance.getDBFolder(myPath) Is Nothing Then InternalDBManager.getInstance.addDBFolder(myPath)
        Dim folder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(myPath)
        Dim returning As String = InternalDBManager.getInstance.addItem(myNom, folder, "Rapport", False, myMotsCles, myDescription, False, True, myNom & ".HTMLRPT")
        RemoveHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
        If returning <> "" Then
            MessageBox.Show(returning, "Erreur")
            Return returning
        End If

        Return ""
    End Function

    Public Function saveToClient(ByVal noClient As Integer) As String
        Dim newCommNo As Integer = genUniqueNo()
        saveToFile(appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & newCommNo & ".HTMLRPT")
        addingComm(noClient, 0, True, "Rapport", reportTitle, Date.Today, "", , "REPORT|" & newCommNo & ".HTMLRPT")

        Return ""
    End Function

    Public Function saveToKP(ByVal noKP As Integer) As String
        Dim newCommNo As Integer = genUniqueNo()
        saveToFile(appPath & bar(appPath) & "KP\" & noKP & "\Comm\" & newCommNo & ".HTMLRPT")
        addingCommKP(noKP, 0, True, "Rapport", Me.reportTitle, Date.Today, "", "REPORT|" & newCommNo & ".HTMLRPT")

        Return ""
    End Function

    Public Function formatCell(ByVal data As String, ByVal format As String) As String
        If format Is Nothing Then Return data

        Try
            Select Case format.ToUpper
                Case "BOOLEANBOX"
                    Dim htmlBox As String = "<input type=checkbox disabled" & IIf(data = True, " checked", "") & ">"
                    Return htmlBox
                Case "STRING"
                    Return data
                Case "INT"
                    If data = "" Then data = 0
                    Return data
                Case "DECIMAL"
                    If data = "" Then data = 0
                    data = Math.Round(CDbl(data.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)), 2)
                    forceManaging(data, True, "", False, False, False, False, ",§.", , , , , , , 2, True)
                    Return data
                Case "CURRENCY"
                    If data = "" Then data = 0
                    data = roundAmount(data) 'Math.Round(CDbl(Data.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)), 2)
                    forceManaging(data, True, "", False, False, False, False, ",§.", , , , , , , 2, True)
                    Return data & " $"
                Case "CURRENCYNAME"
                    If data = "" Then data = 0
                    data = roundAmount(data) 'Math.Round(CDbl(Data.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)), 2)
                    forceManaging(data, True, "", False, False, False, False, ",§.", , , , , , , 2, True)
                    data = convertirMontantEnMots(data)
                    Return data.ToUpper
                Case "POURCENT"
                    If data = "" Then data = 0
                    data = Math.Round(CDbl(data.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)), 2)
                    forceManaging(data, True, "", False, False, False, False, ",§.", , , , , , , 2)
                    Return data & " %"
                Case "DATE"
                    If data = "" Then Return ""
                    Return DateFormat.getTextDate(CDate(data), DateFormat.TextDateOptions.YYYYMMDD)
                Case "DATETIME"
                    If data = "" Then Return ""
                    Return DateFormat.getTextDate(CDate(data), DateFormat.TextDateOptions.YYYYMMDD) & " " & DateFormat.getTextDate(CDate(data), DateFormat.TextDateOptions.ShortTime)
                Case "TIME"
                    If data = "" Then Return ""
                    Return DateFormat.getTextDate(CDate(data), DateFormat.TextDateOptions.ShortTime)
            End Select
        Catch
        End Try
        Return data
    End Function

    Public Function addUpCell(ByVal previousData As Double, ByVal addedData As Object, ByVal format As String) As Double
        If format Is Nothing Then Return -1

        Try
            Select Case format.ToUpper
                Case "STRING"
                    Return previousData + 1
                Case "INT"
                    If addedData.ToString = "" Then addedData = 0
                    Return previousData + addedData
                Case "DECIMAL"
                    If addedData.ToString = "" Then addedData = 0
                    Return previousData + addedData
                Case "CURRENCY"
                    If addedData.ToString = "" Then addedData = 0
                    Return previousData + addedData
                Case "CURRENCYMONEY"
                    If addedData.ToString = "" Then addedData = 0
                    Return previousData + addedData
                Case "POURCENT"
                    If addedData.ToString = "" Then addedData = 0
                    Return previousData + addedData
                Case "DATE"
                    Return -1
                Case "DATETIME"
                    Return -1
                Case "TIME"
                    Return -1
            End Select
        Catch
        End Try
        Return -1
    End Function

    Public Function alignCell(ByVal format As String) As String
        Try
            If format Is Nothing Then Return ""
            Select Case format.ToUpper
                Case "STRING"
                    Return ""
                Case "INT"
                    Return " align=right"
                Case "DECIMAL"
                    Return " align=right"
                Case "CURRENCY"
                    Return " align=right"
                Case "POURCENT"
                    Return " align=right"
                Case "DATE"
                    Return ""
                Case "DATETIME"
                    Return ""
                Case "TIME"
                    Return ""
            End Select
        Catch
        End Try
        Return ""
    End Function
#End Region

    Private Sub internalGeneration()
        Try
            Dim htmlBuilder As New System.Text.StringBuilder
            Dim htmlGenerated As String = ""
            Dim isHTMLGenerated As Boolean = False
            Dim hasToEndTD As Boolean = False

            htmlBuilder.AppendLine("<html><head><title>Rapport Clinica</title></head><body><report>")
            htmlBuilder.AppendLine("<table border=0 cellpadding=0 cellspacing=0 style=""width:" & myReportWidth.toString & """>")

            If Not myHeader Is Nothing Then
                htmlGenerated = myHeader.generateHTML
                If htmlGenerated <> "" Then
                    htmlBuilder.AppendLine("<tr><td>" & vbCrLf & htmlGenerated & vbCrLf & "</td></tr>")
                    isHTMLGenerated = True
                End If
            End If

            If Not myBody Is Nothing Then
                htmlGenerated = myBody.generateHTML
                If htmlGenerated IsNot Nothing AndAlso htmlGenerated <> "" Then
                    htmlBuilder.AppendLine("<tr><td>" & vbCrLf & htmlGenerated)
                    hasToEndTD = True
                    isHTMLGenerated = True
                End If
            End If

            If Not myFooter Is Nothing Then
                htmlGenerated = myFooter.generateHTML
                If htmlGenerated <> "" Then
                    If hasToEndTD = False Then htmlBuilder.AppendLine("<tr><td>")
                    htmlBuilder.AppendLine(vbCrLf & htmlGenerated)
                    hasToEndTD = True
                    isHTMLGenerated = True
                End If
            End If

            If hasToEndTD = True Then htmlBuilder.AppendLine("</td></tr>")

            If isHTMLGenerated = False Then htmlBuilder.AppendLine("<td width=100% height=100%>Aucun rapport à générer</td>")
            htmlBuilder.AppendLine("</table>")
            htmlBuilder.AppendLine("</report></body></html>")

            myHTML = htmlBuilder.ToString
            myHTML = replaceCustomVariables(myHTML)

            RaiseEvent htmlGenerated(myHTML, isHTMLGenerated)
        Catch ex As Threading.ThreadAbortException
            'Aborted by the user
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

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
        If ownWorkingThread IsNot Nothing Then
            ownWorkingThread.Abort()
            ownWorkingThread = Nothing
        End If
    End Sub

    Private Shared logNumber As Integer = 0

    Public Sub print(Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False) Implements IPrintable.print
        Dim rapportPrinter As String = ""
        If Me.reportType.reportPrinter.StartsWith("*") = False And Me.reportType.reportPrinter.EndsWith("*") = False Then
            rapportPrinter = Me.reportType.reportPrinter
        End If

        logNumber += 1
        logToLocal("Report  " & localLogNumber & " - " & logNumber & " : Before HTML")

        Dim html As String = Me.getHTML
        If html = "" Then html = Me.generateHTML()

        logToLocal("Report  " & localLogNumber & " - " & logNumber & " : After HTML")

        PrintingHelper.printHtml(html, "Rapport : " & Me.reportTitle, promptUser, waitForSpooling, rapportPrinter)

        logToLocal("Report  " & localLogNumber & " - " & logNumber & " : After printing")
    End Sub

    Public Sub printOptions() Implements IPrintable.printOptions
        Throw New Exception("Not implemented")
    End Sub

    Public Sub printPreview() Implements IPrintable.printPreview
        Throw New Exception("Not implemented")
    End Sub
End Class
