Public Interface ReportFilter
    Inherits IPropertiesManagable

    ReadOnly Property name() As String
    Property isRequired() As Boolean
    Property tableDotField() As String
    Property parent() As ReportFilter
    ReadOnly Property filterResult() As FilterResult

    Function filter(ByVal filtered As FilteringElement) As FilterResult
    Function filter() As FilterResult
End Interface
