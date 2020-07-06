Public MustInherit Class BasicFiltering
    Implements FilteringElement

    Public ReadOnly Property name() As String
        Get
            Return Me.GetType.Name
        End Get
    End Property

    Public MustOverride Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements IPropertiesManagable.loadProperties
    Public MustOverride Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements IPropertiesManagable.saveProperties

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString

    End Function
End Class
