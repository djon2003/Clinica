Public Class ReportGenerator

#Region "Private functions"
    Private Function getClinicName() As String
        Dim myClinic() As String = DBLinker.getInstance.readOneDBField("InfoClinique", "Nom")

        If myClinic Is Nothing OrElse myClinic.Length = 0 Then
            Return ""
        Else
            Return myClinic(0)
        End If
    End Function
#End Region

    Private _ReportHeaderName As String = ""
    Private _ReportHeaderProperties As String = ""
    Private _RapportHeaderStyle As String = ""
    Private _filteringSize As Integer = 1
    Private _reportTitle As String = ""
    Private _RapportWidth As Report.RapportSizes
    Private _ReportBodyName As String = ""
    Private _ReportBodyProperties As String = ""
    Private _ReportFooterName As String = ""
    Private _ReportFooterProperties As String = ""
    Private _CustomVariables As Hashtable

#Region "Propriétés"
    Public Property reportFooterName() As String
        Get
            Return _ReportFooterName
        End Get
        Set(ByVal value As String)
            _ReportFooterName = value
        End Set
    End Property

    Public Property reportFooterProperties() As String
        Get
            Return _ReportFooterProperties
        End Get
        Set(ByVal value As String)
            _ReportFooterProperties = value
        End Set
    End Property

    Public Property reportBodyName() As String
        Get
            Return _ReportBodyName
        End Get
        Set(ByVal value As String)
            _ReportBodyName = value
        End Set
    End Property

    Public Property reportBodyProperties() As String
        Get
            Return _ReportBodyProperties
        End Get
        Set(ByVal value As String)
            _ReportBodyProperties = value
        End Set
    End Property

    Public Property reportHeaderName() As String
        Get
            Return _ReportHeaderName
        End Get
        Set(ByVal value As String)
            _ReportHeaderName = value
        End Set
    End Property

    Public Property reportHeaderProperties() As String
        Get
            Return _ReportHeaderProperties
        End Get
        Set(ByVal value As String)
            _ReportHeaderProperties = value
        End Set
    End Property

    Public Property filteringSize() As Integer
        Get
            Return _filteringSize
        End Get
        Set(ByVal value As Integer)
            _filteringSize = value
        End Set
    End Property

    Public Property reportTitle() As String
        Get
            Return _reportTitle
        End Get
        Set(ByVal value As String)
            _reportTitle = value
        End Set
    End Property

    Public Property reportWidth() As Report.RapportSizes
        Get
            Return _RapportWidth
        End Get
        Set(ByVal value As Report.RapportSizes)
            _RapportWidth = value
        End Set
    End Property

    Public Property customVariables() As Hashtable
        Get
            Return _CustomVariables
        End Get
        Set(ByVal value As Hashtable)
            _CustomVariables = value
        End Set
    End Property
#End Region

    Public Function generateRapport(ByVal curFilter As ReportFilter, Optional ByVal curFilteringElement As FilteringElement = Nothing) As Report
       Dim myReport As New Report()

        If _CustomVariables IsNot Nothing Then
            For Each varKey As String In _CustomVariables.Keys
                myReport.addCustomVariable(varKey, _CustomVariables(varKey))
            Next
        End If

        myReport.filtered = curFilteringElement IsNot Nothing
        myReport.reportTitle = reportTitle
        myReport.reportFilter = curFilter

        'Génération du Filtrage
        Dim myFilterResult As FilterResult
        If curFilteringElement Is Nothing Then
            myFilterResult = curFilter.filter()
        Else
            myFilterResult = curFilter.filter(curFilteringElement)
        End If
        If Not myFilterResult Is Nothing Then If myFilterResult.errorOccured Then Return myReport
        'FORM is used because the WebControl can't gather <TABLE name="filterList"> with 'document.getElementsByName()'
        myReport.reportFilters = "<br><FORM name=""filterList""><table class=""filterList""><tr><td></td><td></td><td></td></tr>" & myFilterResult.filteringText & "</table></FORM>"
        
        subGenerateRapport(myReport, myFilterResult)

        If Not myReport.body Is Nothing Then
            If myReport.body.currentOrder <> "" Then
                Dim triage As String = myReport.body.currentOrder.Replace(" DESC", "")
                triage = triage.Substring(1, triage.Length - 2)
                myReport.reportBodyOrder = "<tr><td>Trié par</td><td> : </td><td>" & triage & "</td></tr>"
            End If
        End If

        Return myReport
    End Function

    Private Sub subGenerateRapport(ByRef myReport As Report, ByVal myFilterResult As FilterResult)
        Dim myHeader As ReportHeader = ReportBasicHeader.findHeader(reportHeaderName, myReport)
        Dim myHeaderProperties As Hashtable = PropertiesHelper.transformProperties(reportHeaderProperties)
        myHeader.loadProperties(myHeaderProperties)

        Dim myFooter As ReportFooter = ReportBasicFooter.findFooter(reportFooterName, myReport)
        Dim myFooterProperties As Hashtable = PropertiesHelper.transformProperties(reportFooterProperties)
        myFooter.loadProperties(myFooterProperties)

        Dim myBody As ReportBody = ReportBasicBody.findBody(reportBodyName, myReport)
        Dim myBodyProperties As Hashtable = PropertiesHelper.transformProperties(reportBodyProperties)
        myBody.loadProperties(myBodyProperties)

        myReport.header = myHeader
        myReport.body = myBody
        myReport.footer = myFooter
    End Sub
End Class
