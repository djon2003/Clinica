Imports System.ComponentModel

Public Class ReportType
    Implements System.Collections.Generic.IComparer(Of ReportType), Collections.IComparer, System.ICloneable, IPropertiesManagable

#Region "Definitions"
    Private _ReportWidth As Integer = 100, _FiltrageSize As Integer = 1, _NoReportType As Integer
    Private _ReportPrinter As String = "* Imprimante par défaut *", _ReportWidthType As String = "Pourcent", _ReportCategorie, _ReportFooterProperties, _ReportFooterName, _ReportBodyProperties, _RapportProperties, _ReportTitle, _ReportHeaderName, _ReportHeaderProperties, _ReportBodyName As String
    Private _ShowCatInName As Boolean = False
    Private _IsInternal As Boolean = False
#End Region

#Region "Properties"
    <CategoryAttribute("Behavior"), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DesignOnly(True), _
    DescriptionAttribute("Afficher la catégorie dans le nom du type de rapport")> _
    Public Property showCatInName() As Boolean
        Get
            Return _ShowCatInName
        End Get
        Set(ByVal value As Boolean)
            _ShowCatInName = value
        End Set
    End Property

    <CategoryAttribute(""), _
Browsable(False), _
[ReadOnly](False), _
BindableAttribute(False), _
DefaultValueAttribute(""), _
DesignOnly(True), _
DescriptionAttribute("")> _
Public ReadOnly Property noReportType() As Integer
        Get
            Return _NoReportType
        End Get
    End Property

    Public ReadOnly Property reportTitle() As String
        Get
            Return _ReportTitle
        End Get
    End Property

    Public Property reportCategorie() As String
        Get
            Return _ReportCategorie
        End Get
        Set(ByVal value As String)
            _ReportCategorie = value
        End Set
    End Property

    <TypeConverter(GetType(ReportTypePrinterConverter)), CategoryAttribute(""), _
Browsable(True), _
[ReadOnly](False), _
BindableAttribute(False), _
DefaultValueAttribute(""), _
DesignOnly(False), _
DescriptionAttribute("")> _
   Public Property reportPrinter() As String
        Get
            Return _ReportPrinter
        End Get
        Set(ByVal value As String)
            _ReportPrinter = value
        End Set
    End Property

    Public ReadOnly Property reportWidth() As Integer
        Get
            Return _ReportWidth
        End Get
    End Property

    Public ReadOnly Property reportWidthType() As String
        Get
            Return _ReportWidthType
        End Get
    End Property

    <TypeConverter(GetType(ReportTypeHeadersList)), CategoryAttribute(""), _
Browsable(True), _
[ReadOnly](False), _
BindableAttribute(False), _
DefaultValueAttribute(""), _
DesignOnly(False), _
DescriptionAttribute("")> _
    Public Property reportHeaderName() As String
        Get
            Return _ReportHeaderName
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    <TypeConverter(GetType(HeaderPropertiesConverter)), CategoryAttribute(""), _
Browsable(True), _
[ReadOnly](False), _
BindableAttribute(False), _
DefaultValueAttribute(""), _
DesignOnly(False), _
DescriptionAttribute("")> _
    Public ReadOnly Property headerProperties() As ReportHeader
        Get
            Dim rapportHeader As ReportHeader = ReportBasicHeader.findHeader(reportHeaderName, New Report)
            rapportHeader.loadproperties(PropertiesHelper.transformProperties(Me.reportHeaderProperties))
            Return rapportHeader
        End Get
    End Property


    Public ReadOnly Property reportHeaderProperties() As String
        Get
            Return _ReportHeaderProperties
        End Get
    End Property

    <TypeConverter(GetType(ReportTypeBodysList)), CategoryAttribute(""), _
Browsable(True), _
[ReadOnly](False), _
BindableAttribute(False), _
DefaultValueAttribute(""), _
DesignOnly(False), _
DescriptionAttribute(""), RefreshProperties(RefreshProperties.All)> _
    Public Property reportBodyName() As String
        Get
            Return _ReportBodyName
        End Get
        Set(ByVal value As String)
            reportBody = Nothing 'REM Doesn't work to view real bodyproperties in the propertygrid control
            _ReportBodyName = value
        End Set
    End Property

    Dim reportBody As ReportBody
    <TypeConverter(GetType(BodyPropertiesConverter)), CategoryAttribute(""), _
Browsable(True), _
[ReadOnly](False), _
BindableAttribute(False), _
DefaultValueAttribute(""), _
DesignOnly(False), _
DescriptionAttribute("")> _
Public ReadOnly Property bodyProperties() As ReportBody
        Get
            If reportBody Is Nothing AndAlso _ReportBodyProperties <> "" Then
                reportBody = ReportBasicBody.findBody(reportBodyName, New Report)
                reportBody.loadproperties(PropertiesHelper.transformProperties(_ReportBodyProperties))
            End If
            Return reportBody
        End Get
    End Property

    <Browsable(False)> _
    Public ReadOnly Property reportBodyProperties() As String
        Get
            If reportBody IsNot Nothing Then Return reportBody.propertiesToString

            Return _ReportBodyProperties
        End Get
    End Property

    <TypeConverter(GetType(ReportTypeFootersList)), CategoryAttribute(""), _
Browsable(True), _
[ReadOnly](False), _
BindableAttribute(False), _
DefaultValueAttribute(""), _
DesignOnly(False), _
DescriptionAttribute("")> _
    Public ReadOnly Property reportFooterName() As String
        Get
            Return _ReportFooterName
        End Get
    End Property

    Public ReadOnly Property reportFooterProperties() As String
        Get
            Return _ReportFooterProperties
        End Get
    End Property

    Public ReadOnly Property filtrageSize() As Integer
        Get
            Return _FiltrageSize
        End Get
    End Property

    Public ReadOnly Property isInternal() As Boolean
        Get
            Return _IsInternal
        End Get
    End Property
#End Region

    Public Sub New()

    End Sub

    Public Overrides Function toString() As String
        Dim myCat As String = ""
        If _ShowCatInName And reportCategorie <> "" Then myCat = reportCategorie & " \\ "

        Return myCat & _ReportTitle
    End Function

    Public Function isLeastOneRequiredFilter() As Boolean
        Return ReportsManager.getInstance.getFilter(Me).isRequired
    End Function

    Public Function isFilter(ByVal filterName As String) As Boolean
        Dim curFilter As FilterComposite = ReportsManager.getInstance.getFilter(Me)
        If curFilter.indexOf(filterName) = -1 Then Return False

        Return True
    End Function

    Public ReadOnly Property count() As Integer
        Get
            Dim curFilter As FilterComposite = ReportsManager.getInstance.getFilter(Me)
            Return curFilter.Count
        End Get
    End Property

    Public Sub New(ByVal data As DataRow) 'ByVal NoRapportType As Integer, ByVal RapportTitle As String, ByVal RapportWidth As Integer, ByVal RapportWidthType As String, ByVal RapportHeaderName As String, ByVal RapportHeaderProperties As String, ByVal RapportBodyName As String, ByVal RapportBodyProperties As String, ByVal RapportFooterName As String, ByVal RapportFooterProperties As String, ByVal FiltrageSize As Integer, ByVal RapportCategorie As String, ByVal RapportPrinter As String)
        _NoReportType = data("NoRapportType")
        '_FiltrageSize = FiltrageSize
        '_RapportWidth = RapportWidth
        _ReportFooterProperties = data("RapportFooterProperties").ToString
        _ReportFooterName = data("RapportFooterName").ToString
        _ReportBodyProperties = data("RapportBodyProperties").ToString
        _ReportTitle = data("RapportTitle").ToString
        '_RapportWidthType = RapportWidthType
        _ReportHeaderName = data("RapportHeaderName").ToString
        _ReportHeaderProperties = data("RapportHeaderProperties").ToString
        _ReportBodyName = data("RapportBodyName").ToString
        _ReportCategorie = data("RapportCategorie").ToString
        _RapportProperties = data("RapportProperties").ToString
        Me.loadproperties(PropertiesHelper.transformProperties(_RapportProperties))
    End Sub

    Public Function compare(ByVal x As ReportType, ByVal y As ReportType) As Integer Implements System.Collections.Generic.IComparer(Of ReportType).Compare
        Return x.reportTitle.CompareTo(y.reportTitle)
    End Function

    Public Function compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Return compare(CType(x, ReportType), CType(y, ReportType))
    End Function

    Public Function clone() As Object Implements System.ICloneable.Clone
        Dim newRT As New ReportType()

        newRT._NoReportType = noReportType
        newRT._ReportFooterProperties = Me.reportFooterProperties
        newRT._ReportFooterName = Me.reportFooterName
        newRT._ReportBodyProperties = Me.reportBodyProperties
        newRT._ReportTitle = Me.reportTitle
        newRT._ReportHeaderName = Me.reportHeaderName
        newRT._ReportHeaderProperties = Me.reportHeaderProperties
        newRT._ReportBodyName = Me.reportBodyName
        newRT._ReportCategorie = Me.reportCategorie
        newRT._RapportProperties = Me._RapportProperties
        newRT.loadproperties(PropertiesHelper.transformProperties(_RapportProperties))

        Return newRT
    End Function

    Public Sub loadproperties(ByVal properties As System.Collections.Hashtable) Implements IPropertiesManagable.loadproperties
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "IsInternal"
                    _IsInternal = myKey.Value
                Case "RapportWidth"
                    _ReportWidth = myKey.Value
                Case "RapportWidthType"
                    _ReportWidthType = myKey.Value
                Case "FiltrageSize"
                    _FiltrageSize = myKey.Value
                Case "RapportPrinter"
                    _ReportPrinter = myKey.Value
                Case Else
            End Select
        Next
    End Sub

    Public Sub saveproperties(ByRef properties As System.Collections.Hashtable) Implements IPropertiesManagable.saveProperties

    End Sub

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString

    End Function
End Class
