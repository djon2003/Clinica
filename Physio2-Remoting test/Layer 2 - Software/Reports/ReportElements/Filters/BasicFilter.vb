Public MustInherit Class BasicFilter
    Implements ReportFilter

    Private _TableDotField As String
    Private _IsRequired As Boolean
    Private _parent As ReportFilter
    Private _filterOnReportParts As Report.RapportParts = Report.RapportParts.All
    Private _filterResult As FilterResult

    Public ReadOnly Property filterResult() As FilterResult Implements ReportFilter.filterResult
        Get
            Return _filterResult
        End Get
    End Property

    Public Property filterOnReportParts() As Report.RapportParts
        Get
            Return _filterOnReportParts
        End Get
        Set(ByVal value As Report.RapportParts)
            _filterOnReportParts = value
        End Set
    End Property

    Public Property tableDotField() As String Implements ReportFilter.tableDotField
        Get
            Return _TableDotField
        End Get
        Set(ByVal value As String)
            _TableDotField = value
        End Set
    End Property

    Public Property isRequired() As Boolean Implements ReportFilter.isRequired
        Get
            Return _IsRequired
        End Get
        Set(ByVal value As Boolean)
            _IsRequired = value
        End Set
    End Property

    Public Property parent() As ReportFilter Implements ReportFilter.parent
        Get
            Return _parent
        End Get
        Set(ByVal value As ReportFilter)
            _parent = value
        End Set
    End Property

    Protected MustOverride Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
    Protected MustOverride Function internalFilter() As FilterResult

    Public Function filter(ByVal filtered As FilteringElement) As FilterResult Implements ReportFilter.filter
        _filterResult = internalFilter(filtered)
        Return _filterResult
    End Function
    Public Function filter() As FilterResult Implements ReportFilter.filter
        _filterResult = internalFilter()
        Return _filterResult
    End Function

    Public Overridable Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements IPropertiesManagable.loadProperties
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "FilterOnRapportParts"
                    Select Case myKey.Value
                        Case "HEADER"
                            filterOnReportParts = Report.RapportParts.Header
                        Case "BODY"
                            filterOnReportParts = Report.RapportParts.Body
                        Case "FOOTER"
                            filterOnReportParts = Report.RapportParts.Footer
                        Case "HEADER-BODY"
                            filterOnReportParts = Report.RapportParts.Header_Body
                        Case "HEADER-FOOTER"
                            filterOnReportParts = Report.RapportParts.Header_Footer
                        Case "BODY-FOOTER"
                            filterOnReportParts = Report.RapportParts.Body_Footer
                        Case Else
                            filterOnReportParts = Report.RapportParts.All
                    End Select
                Case Else
            End Select
        Next
    End Sub
    Public MustOverride Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements IPropertiesManagable.saveProperties

    Public ReadOnly Property name() As String Implements ReportFilter.name
        Get
            Return Me.GetType.Name
        End Get
    End Property

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString

    End Function
End Class
