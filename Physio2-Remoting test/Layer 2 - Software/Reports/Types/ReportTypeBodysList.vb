Imports System.ComponentModel

Public Class ReportTypeBodysList
    Inherits StringConverter

    Public Overrides Function getStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function getStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(New String() {"RapportBodyVisites", "RapportBodyStats", "RapportBodyTable", "RapportBodyAgenda", "RapportBodySimple", "RapportBasicBody"})
    End Function

    Public Overrides Function getStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True 'False to be able to enter text
    End Function
End Class
