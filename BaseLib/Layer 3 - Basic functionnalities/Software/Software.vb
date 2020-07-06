Public Class Software
    Inherits ManagerBase(Of Software)

    Private _arePluginsLoaded As Boolean = False

    Protected Sub New()

    End Sub

#Region "Properties"
    Public ReadOnly Property arePluginsLoaded() As Boolean
        Get
            Return _arePluginsLoaded
        End Get
    End Property
#End Region

    Protected Sub setPluginsAsLoaded()
        _arePluginsLoaded = True
    End Sub

End Class

'Friend Class Software
'    Friend Shared Function getInstance() As Software(Of Object)
'        Return CType(Software(Of Object).getInstance(), Software(Of Object))
'    End Function
'End Class
