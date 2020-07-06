Public Interface IPropertiesManagable
    Sub loadProperties(ByVal properties As Hashtable)
    Sub saveProperties(ByRef properties As Hashtable)
    Function propertiesToString() As String
End Interface
