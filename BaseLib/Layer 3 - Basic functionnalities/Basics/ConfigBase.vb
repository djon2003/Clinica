Imports System.ComponentModel
Imports System.Reflection

<Serializable()> _
Public MustInherit Class ConfigBase
    Inherits ParametersBase

    Private Const CONFIG_TYPE_NAME As String = "config"

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region


    Protected Overrides Function getTypeName() As String
        Return CONFIG_TYPE_NAME
    End Function
End Class
