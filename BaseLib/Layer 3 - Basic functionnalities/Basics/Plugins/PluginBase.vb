Public MustInherit Class PluginBase
    Implements IPlugin


    Private Shared nbInstances As Integer = 0
    Private _noItemable As Integer = 0
    Private _isRemote As Boolean = False

    Public Sub New()
        nbInstances += 1
        _noItemable = nbInstances
    End Sub

    Public MustOverride ReadOnly Property description() As String Implements IPlugin.description
    Public MustOverride ReadOnly Property name() As String Implements IPlugin.name
    Public MustOverride ReadOnly Property pluginType() As Plugin.PluginTypes Implements IPlugin.getPluginType
    Public MustOverride ReadOnly Property hasToConfigure() As Boolean Implements IPlugin.hasToConfigure
    
    Public MustOverride Sub configure() Implements IPlugin.configure
    Public MustOverride Sub initialize() Implements IPlugin.initialize

    Public Overridable Function getIdentifier() As String
        Return Me.GetType().Assembly.GetName().Name
    End Function

    Public Property isRemote() As Boolean
        Get
            Return _isRemote
        End Get
        Set(ByVal value As Boolean)
            _isRemote = value
        End Set
    End Property


    Public ReadOnly Property noItemable() As Integer Implements IItemable.noItemable
        Get
            Return _noItemable
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.name
    End Function

    Public Event noItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer) Implements IItemable.noItemableChanged

    Protected Overridable Sub onNoItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer)
        RaiseEvent noItemableChanged(oldNoItemable, newNoItemable)
    End Sub
End Class
