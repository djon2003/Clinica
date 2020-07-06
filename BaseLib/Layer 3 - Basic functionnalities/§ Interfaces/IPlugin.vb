Public Interface IPlugin
    Inherits IItemable

    ReadOnly Property name() As String
    ReadOnly Property description() As String
    ReadOnly Property getPluginType() As Plugin.PluginTypes
    ReadOnly Property hasToConfigure() As Boolean

    Sub configure()
    Sub initialize()

End Interface
