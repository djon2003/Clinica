Public MustInherit Class PluginsManagerBase(Of PluginType)
    Inherits ItemableManagerBase(Of PluginTasksManager, PluginType)

    Public Overloads Function getItemable(ByVal fullName As String) As PluginType
        For Each curTaskKey As Integer In _items.Keys
            Dim curTask As PluginType = _items(curTaskKey)
            If curTask.GetType.FullName = fullName Then Return curTask
        Next

        Return Nothing
    End Function
End Class
