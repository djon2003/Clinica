Public Class Preferences
    Inherits DBItemableBase
    Implements IPropertiesManagable

    Private properties As Hashtable
    Private _noUser As Integer
    Private _noPref As Integer


#Region "Properties"
    Public ReadOnly Property noPref() As Integer
        Get
            Return _noPref
        End Get
    End Property

    Public ReadOnly Property noUser() As Integer
        Get
            Return _noUser
        End Get
    End Property
#End Region

    Public Sub clear()
        Me.properties.Clear()
    End Sub

    Public ReadOnly Property count() As Integer
        Get
            If properties Is Nothing Then Return 0

            Return properties.Count
        End Get
    End Property

    Public Function existsProperty(ByVal propertyName As String) As Boolean
        Return Not (properties Is Nothing OrElse properties.ContainsKey(propertyName.ToLower) = False)
    End Function

    Default Public ReadOnly Property getProperty(ByVal propertyName As String) As String
        Get
            If properties Is Nothing OrElse properties.ContainsKey(propertyName.ToLower) = False OrElse properties(propertyName.ToLower) Is Nothing Then Return ""

            Return properties(propertyName.ToLower).ToString.Replace("\n", vbCrLf)
        End Get
    End Property

    Public Sub setProperty(ByVal propertyName As String, ByVal value As String)
        If properties.ContainsKey(propertyName.ToLower) Then
            properties(propertyName.ToLower) = value
        Else
            properties.Add(propertyName.ToLower, value)
        End If
    End Sub

    Public Overrides Sub delete()
        Throw New NotSupportedException()
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _noPref = curData("NoPref")
        If curData("NoUser") IsNot DBNull.Value Then _noUser = curData("NoUser")
        If curData("Preferences") Is DBNull.Value OrElse curData("Preferences").ToString = "" Then
            properties = New Hashtable()
        Else
            properties = PropertiesHelper.transformProperties(curData("Preferences").ToString, True)
        End If
    End Sub

    Public Overrides Sub saveData()
        DBLinker.getInstance.updateDB("Preferences", "Preferences='" & propertiesToString().Replace("'", "''") & "'", "NoPref", _noPref, False)
        onDataChanged()

        If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("Preferences(" & _noPref & ")")
    End Sub

    Public Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements IPropertiesManagable.loadProperties
        Me.properties = properties
    End Sub

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString
        Return PropertiesHelper.transformProperties(Me.properties, False)
    End Function

    Public Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements IPropertiesManagable.saveProperties
        properties = Me.properties
    End Sub

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noPref
        End Get
    End Property
End Class
