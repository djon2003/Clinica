Public Class ReportsManager
    Inherits ManagerBase(Of ReportsManager)

#Region "Class ReportFilterType"
    Private Class ReportFilterType
#Region "Definitions"
        Private _NoRapportType, _NoReportFilterType As Integer
        Private _TableDotField, _Nom, _Properties As String
        Private _IsRequired As Boolean
#End Region

#Region "Properties"
        Public ReadOnly Property isRequired() As Boolean
            Get
                Return _IsRequired
            End Get
        End Property

        Public ReadOnly Property noReportFilterType() As Integer
            Get
                Return _NoReportFilterType
            End Get
        End Property

        Public ReadOnly Property tableDotField() As String
            Get
                Return _TableDotField
            End Get
        End Property

        Public ReadOnly Property noRapportType() As Integer
            Get
                Return _NoRapportType
            End Get
        End Property

        Public ReadOnly Property nom() As String
            Get
                Return _Nom
            End Get
        End Property

        Public ReadOnly Property properties() As String
            Get
                Return _Properties
            End Get
        End Property
#End Region

        Public Sub New(ByVal noReportFilterType As Integer, ByVal filterName As String, ByVal tableDotField As String, ByVal properties As String, ByVal noRapportType As Integer, ByVal isRequired As Boolean)
            _NoRapportType = noRapportType
            _NoReportFilterType = noReportFilterType
            _Nom = filterName
            _TableDotField = tableDotField
            _Properties = properties
            _IsRequired = isRequired
        End Sub
    End Class
#End Region

    Private myReportTypes As New ArrayList
    Private myReportFiltrages As New ArrayList
    Private myReportGenerator As ReportGenerator
    Private myFilterCreator As New FilterCreator

    Protected Sub New()
        loadReportTypes()
        loadReportFilterTypes()
    End Sub

#Region "Public functions"
    Public Function getReportTypes() As ArrayList
        Return myReportTypes.Clone
    End Function

    Public Function selectReportType(Optional ByRef height As Integer = 0, Optional ByRef width As Integer = 0) As String
        If myReportTypes Is Nothing Then Return ""

        Dim i As Integer
        Dim typesStr As String = ""

        Dim showCategories As Boolean = PreferencesManager.getUserPreferences()("AffRapportCatInSelection")
        For i = 0 To myReportTypes.Count - 1
            With CType(myReportTypes(i), ReportType)
                If .isInternal = False Then
                    Dim cat As String = .reportCategorie
                    If cat <> "" Then cat &= " " & bar(.reportCategorie) & bar(.reportCategorie) & " "
                    If showCategories = False Then cat = ""
                    typesStr &= "§" & cat & .reportTitle
                End If
            End With
        Next i
        typesStr = typesStr.Substring(1)

        Dim myMultiChoice As New multichoice
        If height <> 0 Then myMultiChoice.Height = height
        If width <> 0 Then myMultiChoice.Width = width
        Dim myChoice As String = myMultiChoice.GetChoice("Veuillez choisir le type de rapport", typesStr, , "§")
        width = myMultiChoice.Width
        height = myMultiChoice.Height

        If myChoice.IndexOf(" \\ ") > 0 Then myChoice = myChoice.Substring(myChoice.IndexOf(" \\ ") + 4)

        Return myChoice
    End Function

#Region "CreateRapport functions"
    Public Function createReport(ByVal reportTitle As String, Optional ByVal curFilteringElement As FilteringElement = Nothing, Optional ByVal customVariables As Hashtable = Nothing, Optional ByVal bodyTable As DataTable = Nothing) As Report
        If reportTitle = "" Then Return Nothing
        If myReportTypes Is Nothing Then Return Nothing
        Dim reportIndex As Integer = getReportIndex(reportTitle)
        If reportIndex = -1 Then Return Nothing

        Return createReport(reportIndex, curFilteringElement, customVariables, bodyTable)
    End Function

    Public Function createReport(ByVal reportIndex As Integer, Optional ByVal curFilteringElement As FilteringElement = Nothing, Optional ByVal customVariables As Hashtable = Nothing, Optional ByVal bodyTable As DataTable = Nothing) As Report
        If currentUserName = "Administrateur" Then
            myReportTypes.Clear() : myReportFiltrages.Clear()
            loadReportTypes() : loadReportFilterTypes()
        End If

        If reportIndex < 0 Then Return Nothing

        myReportGenerator = New ReportGenerator
        Dim myRapportType As ReportType = CType(myReportTypes(reportIndex), ReportType)

        prepareGenerator(myRapportType, customVariables)
        Dim newReport As Report = myReportGenerator.generateRapport(getFilter(myRapportType), curFilteringElement)
        If bodyTable IsNot Nothing AndAlso TypeOf newReport.body Is ReportBodyTable Then CType(newReport.body, ReportBodyTable).passedTable = bodyTable
        newReport.reportType = myRapportType
        Return newReport
    End Function
#End Region
#End Region

#Region "Private functions"
    Private Sub prepareGenerator(ByVal myRapportType As ReportType, Optional ByVal customVariables As Hashtable = Nothing)
        'Préparation du générator
        With myReportGenerator
            .customVariables = customVariables
            .reportTitle = myRapportType.reportTitle
            Select Case myRapportType.reportWidthType
                Case "Pourcent"
                    .reportWidth = New Report.RapportSizes(CByte(myRapportType.reportWidth))
                Case "Inches"
                    .reportWidth = New Report.RapportSizes(CDbl(myRapportType.reportWidth))
            End Select
            .reportHeaderName = myRapportType.reportHeaderName
            .reportHeaderProperties = myRapportType.reportHeaderProperties
            .reportBodyName = myRapportType.reportBodyName
            .reportBodyProperties = myRapportType.reportBodyProperties
            .reportFooterName = myRapportType.reportFooterName
            .reportFooterProperties = myRapportType.reportFooterProperties
            .filteringSize = myRapportType.filtrageSize
        End With
    End Sub

    Public Function getFilter(ByVal myRapportType As ReportType) As ReportFilter
        'Création des filtres appropriés
        Dim myFilterComposite As New FilterComposite
        Dim i As Integer
        For i = 0 To myReportFiltrages.Count - 1
            With CType(myReportFiltrages(i), ReportFilterType)
                If .noRapportType = myRapportType.noReportType Then
                    Dim newReportFilter As ReportFilter = myFilterCreator.createFilter(.nom)
                    newReportFilter.loadProperties(PropertiesHelper.transformProperties(.properties))
                    newReportFilter.tableDotField = .tableDotField
                    newReportFilter.isRequired = .isRequired
                    myFilterComposite.add(newReportFilter)
                End If
            End With
        Next i

        Return myFilterComposite
    End Function

    Private Function getReportIndex(ByVal reportTitle As String) As Integer
        Dim i As Integer

        For i = 0 To myReportTypes.Count - 1
            If CType(myReportTypes(i), ReportType).reportTitle = reportTitle Then Return i
        Next i

        Return -1
    End Function

    Private Sub loadReportTypes()
        Dim theRapportTypes As DataSet = DBLinker.getInstance.readDBForGrid("RapportTypes LEFT JOIN RapportCategories ON RapportCategories.NoRapportCategorie = RapportTypes.NoRapportCategorie", "RapportTypes.*,RapportCategorie")
        Dim i As Integer
        With theRapportTypes.Tables(0).Rows
            For i = 0 To .Count - 1
                myReportTypes.Add(New ReportType(.Item(i)))
            Next i
        End With
    End Sub

    Private Sub loadReportFilterTypes()
        Dim theRapportFiltrages(,) As String = DBLinker.getInstance.readDB("RapportFiltrages", "*")
        Dim i As Integer
        For i = 0 To theRapportFiltrages.GetUpperBound(1)
            myReportFiltrages.Add(New ReportFilterType(theRapportFiltrages(0, i), theRapportFiltrages(1, i), theRapportFiltrages(2, i), theRapportFiltrages(3, i), theRapportFiltrages(4, i), theRapportFiltrages(5, i)))
        Next i
    End Sub
#End Region
End Class
