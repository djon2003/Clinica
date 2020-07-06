Public Class Plugin

    Private pluginClass As IPlugin
    Private _isAssigned As Boolean = False

    Public Enum PluginTypes
        OwnlyManaged = 0
        Task = 1
    End Enum

    Public Sub New(ByVal pluginFile As String)
        Dim file As New IO.FileInfo(pluginFile)
        Dim loadedPlugin As Reflection.Assembly = Reflection.Assembly.LoadFile(pluginFile)

        Dim pluginType As Type = Nothing
        Try
            For Each curType As Type In loadedPlugin.GetTypes()
                If curType.GetInterface(Me.GetType.Namespace & ".IPlugin") IsNot Nothing Then
                    pluginType = curType
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw New Exception("Problem loading plugin : " & pluginFile, ex)
        End Try

        If pluginType Is Nothing Then Throw New Exception("Plugin class doesn't exist into the plugin file : " & pluginFile)

        Try
            pluginClass = Activator.CreateInstance(pluginType)
        Catch ex As Exception
            MessageBox.Show("Impossible de charger le greffon nommé """ & pluginClass.name & """. Veuillez tenter une mise à jour du logiciel ou une réinstallation.", "Chargement d'un greffon impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub

    Public ReadOnly Property isAssigned() As Boolean
        Get
            Return _isAssigned
        End Get
    End Property

    Public Sub assignPlugin()
        If _isAssigned Then Exit Sub

        _isAssigned = True
        Select Case pluginClass.getPluginType()
            Case PluginTypes.Task
                PluginTasksManager.getInstance.addItemable(CType(pluginClass, PluginTaskBase))
            Case Else
                _isAssigned = False
        End Select
    End Sub

    Public Function getPluginClass() As IPlugin
        Return pluginClass
    End Function
End Class
