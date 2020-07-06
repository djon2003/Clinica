Imports System.ComponentModel
Imports System.Reflection

<Serializable()> _
Public MustInherit Class ParametersBase
    Implements IItemable

    Private Const PARAMS_NAME As String = "params"

    Private _name As String = ""
    Private _hasToUpdate As Boolean = True
    <NonSerialized()> _
    Private Shared nbClasses As Integer = 0
    Private _noItemable As Integer
    <NonSerialized()> _
    Private _neverConfiged As Boolean
    <NonSerialized()> _
    Private properties As New List(Of PropertyInfo)
    <NonSerialized()> _
    Private propertiesData As DataTable
    Private _writeXmlOnSave As Boolean = True
    Private _filename As String = ""

    Public Event noItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer) Implements IItemable.noItemableChanged

#Region "Constructors"
    Public Sub New()
        nbClasses += 1
        _noItemable = nbClasses

        loadProperties()
    End Sub
#End Region

#Region "Properties"

#Region "Browsables"
    <CategoryAttribute(BasePropertiesCategories.INFORMATIONS), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Emplacement du fichier de configuration")> _
    Public Overridable ReadOnly Property fileName() As String
        Get
            'TODO : Shall ensure name doesn't contain illegal characters for files
            Return _filename
        End Get
    End Property

    <CategoryAttribute(BasePropertiesCategories.INFORMATIONS), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(""), _
    DescriptionAttribute("Emplacement du fichier de configuration")> _
    Public ReadOnly Property hasToUpdate() As Boolean
        Get
            Return _hasToUpdate
        End Get
    End Property
#End Region

#Region "Not browsables"
    <Browsable(False)> _
    Public Overridable Property writeXmlOnSave() As Boolean
        Get
            Return _writeXmlOnSave
        End Get
        Set(ByVal value As Boolean)
            _writeXmlOnSave = value
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property neverConfiged() As Boolean
        Get
            Return _neverConfiged
        End Get
    End Property

    <Browsable(False)> _
    Public Overridable ReadOnly Property name() As String
        Get
            Return If(_name = "", Me.GetType.Assembly.GetName().Name, _name)
        End Get
    End Property

    <Browsable(False)> _
    Public ReadOnly Property noItemable() As Integer Implements IItemable.noItemable
        Get
            Return _noItemable
        End Get
    End Property
#End Region

#Region "Class to define Base Properties Categories"
    Protected NotInheritable Class BasePropertiesCategories
        Public Const INFORMATIONS As String = "Informations"

        Private Sub New()
        End Sub
    End Class
#End Region

#End Region

    Protected MustOverride Function getTypeName() As String

    Private Function _getTypeName() As String
        Dim typeName As String = getTypeName()
        If String.IsNullOrEmpty(typeName) Then typeName = PARAMS_NAME

        Return typeName
    End Function

    Private Function isBrowsable(ByVal p As PropertyInfo) As Boolean
        Dim res As Boolean
        res = True
        For Each att_loopVariable As Object In p.GetCustomAttributes(True)
            If TypeOf att_loopVariable Is System.ComponentModel.BrowsableAttribute Then
                Dim att As System.ComponentModel.BrowsableAttribute
                att = DirectCast(att_loopVariable, System.ComponentModel.BrowsableAttribute)
                If Not TryCast(att, System.ComponentModel.BrowsableAttribute).Browsable Then
                    res = False
                End If
            End If
        Next
        Return res
    End Function

    Private Sub loadProperties()
        For Each pi As PropertyInfo In Me.GetType().GetProperties(BindingFlags.Public Or BindingFlags.Instance)
            If pi.CanWrite AndAlso isBrowsable(pi) Then
                properties.Add(pi)
            End If
        Next
    End Sub

    Private Sub createPropertiesDataTable()
        propertiesData = New DataTable(_getTypeName())
        'Add columns
        For Each pi As PropertyInfo In properties
            propertiesData.Columns.Add(New DataColumn(pi.Name, pi.PropertyType))
        Next
        'Create new row
        propertiesData.Rows.Add(propertiesData.NewRow())

        'Loaded default properties value
        transferPropertiesToFromDatatable(False)
    End Sub

    Private Sub transferPropertiesToFromDatatable(ByVal from As Boolean)
        For Each pi As PropertyInfo In properties
            If [from] Then
                pi.SetValue(Me, propertiesData.Rows(0)(pi.Name), Nothing)
            Else
                propertiesData.Rows(0)(pi.Name) = pi.GetValue(Me, Nothing)
            End If
        Next
    End Sub

    Private Sub loadNew()
        createPropertiesDataTable()
        _hasToUpdate = True
    End Sub

    Protected Function getProperty(ByVal propertyName As String) As PropertyInfo
        For Each curProperty As PropertyInfo In properties
            If curProperty.Name = propertyName Then Return curProperty
        Next

        Return Nothing
    End Function

    Private Sub loadFile()
        createPropertiesDataTable()

        Dim loadedData As New DataSet
        loadedData.ReadXml(fileName)

        'TODO : Future dev : To manage multiple configs, add a properties alternativeConfigs which is a list of Configs from row index 1 to N
        Dim nbLoaded As Integer = 0
        If loadedData.Tables.Contains(_getTypeName()) Then
            With loadedData.Tables(_getTypeName())
                If .Rows.Count <> 0 Then
                    For Each curColumn As DataColumn In .Columns
                        Dim curProperty As PropertyInfo = getProperty(curColumn.ColumnName)
                        If curProperty IsNot Nothing Then
                            propertiesData.Rows(0)(curProperty.Name) = .Rows(0)(curColumn.ColumnName)
                            curProperty.SetValue(Me, propertiesData.Rows(0)(curProperty.Name), Nothing)
                            nbLoaded += 1
                        End If
                    Next
                End If
            End With
        End If

        _hasToUpdate = nbLoaded < properties.Count
    End Sub

    Public Overloads Sub load(ByVal jsonString As String)
        createPropertiesDataTable()

        Dim js As New Web.Script.Serialization.JavaScriptSerializer()
        Dim data As Object = js.DeserializeObject(jsonString)

        Dim nbLoaded As Integer = 0
        For Each keyValue As KeyValuePair(Of String, Object) In data
            Dim p As PropertyInfo = getProperty(keyValue.Key)
            If p IsNot Nothing Then
                nbLoaded += 1
                p.SetValue(Me, keyValue.Value, Nothing)
            End If
        Next

        _hasToUpdate = data("hasToUpdate")
        _name = data("name")
        _neverConfiged = data("neverConfiged")
        If TCPClient.getInstance.isConnected Then _filename = "Fichier distant"
    End Sub

    Public Overridable Overloads Sub load()
        _filename = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & name & "." & _getTypeName() & ".xml"
        _neverConfiged = Not IO.File.Exists(_filename)

        If neverConfiged Then
            loadNew()
        Else
            loadFile()
        End If
    End Sub

    Protected Function isNoEmptyFields(ByVal noEmptyFields() As String) As Boolean
        For Each curField As String In noEmptyFields
            If getProperty(curField).GetValue(Me, Nothing) = "" Then
                MessageBox.Show("Veuillez remplir le champ """ & curField & """ de l'onglet """ & Me.GetType.Assembly.GetName.Name & """", "Champ vide", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return True
    End Function

    Protected MustOverride Function isFieldsCorrectlyFilled() As Boolean

    Public Sub saveState()
        transferPropertiesToFromDatatable(False)
    End Sub

    Public Sub reloadPreviousState()
        transferPropertiesToFromDatatable(True)
    End Sub

    Public Sub save()
        If isFieldsCorrectlyFilled() = False Then Throw New Exception("Field(s) inccorectly filled")

        transferPropertiesToFromDatatable(False)

        If _writeXmlOnSave Then propertiesData.WriteXml(fileName)

        _hasToUpdate = False
    End Sub
End Class
