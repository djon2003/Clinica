Public Interface ReportElement
    Inherits IHTMLPageProducer, IPropertiesManagable

    ReadOnly Property name() As String
    Property styleFileName() As String()
    ReadOnly Property filtersWhereString() As String

End Interface
