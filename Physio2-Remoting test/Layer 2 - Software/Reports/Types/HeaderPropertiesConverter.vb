Imports System.ComponentModel

Friend Class HeaderPropertiesConverter : Inherits ExpandableObjectConverter
    Public Overloads Overrides Function canConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(ReportHeader)) Then
            Return True
        End If
        Return MyBase.CanConvertTo(context, destinationType)
    End Function

    Public Overloads Overrides Function canConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function convertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim rapportHeader As Clinica.ReportHeader = ReportBasicHeader.findHeader(CType(context.Instance, ReportType).reportHeaderName, New Report())
                rapportHeader.loadProperties(PropertiesHelper.transformProperties(value))
                Return rapportHeader
            Catch ex As Exception
                Throw New ArgumentException("Can not convert '" & value & "' to type RapportHeader")
            End Try
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function convertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class
