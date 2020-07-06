Imports System.ComponentModel

Public Class ReportTypeFootersList
    Inherits StringConverter

    Public Overrides Function getStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function getStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(New String() {"RapportFooterTotal", "RapportFooterSimple", "RapportBasicFooter"})
    End Function

    Public Overrides Function getStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True 'False to be able to enter text
    End Function
End Class
