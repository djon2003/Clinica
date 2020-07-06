Imports System.Reflection

Public Class ConfigurationsManager
    Inherits ItemableManagerBase(Of ConfigurationsManager, ConfigBase)

    Private _mainConfig As ConfigBase
    Private _configureOnlyMainOne As Boolean = False
    Private loadedOnce As Boolean = False

    Protected Sub New()
        load()
        loadedOnce = True
    End Sub

#Region "Properties"
    Public ReadOnly Property isConfigurationNeeded() As Boolean
        Get
            For Each curConfig As ConfigBase In getItemables()
                If curConfig.hasToUpdate = True Then Return True
            Next

            Return False
        End Get
    End Property

    Public Property hasToConfigureOnlyMainOne() As Boolean
        Get
            Return _configureOnlyMainOne
        End Get
        Set(ByVal value As Boolean)
            _configureOnlyMainOne = value
        End Set
    End Property

    Public ReadOnly Property softwareConfig() As ConfigBase
        Get
            Return _mainConfig
        End Get
    End Property
#End Region

    Public Sub load()
        loadConfigsClasses()
        loadConfigsData()
    End Sub

    Public Sub saveConfigs()
        For Each curConfig As ConfigBase In getItemables()
            curConfig.save()
        Next
    End Sub

    Public Sub showConfigs()
        Dim configWindow As New Windows.Forms.ConfigurationsManager()
        configWindow.addConfigs(getItemables())
        configWindow.ShowDialog()
    End Sub

    Public Sub ensureConfigsUpToDate()
        Dim configsNeedingUpdate As New List(Of ConfigBase)
        For Each curConfig As ConfigBase In getItemables()
            Dim onlyMain As Boolean = (Not hasToConfigureOnlyMainOne OrElse (hasToConfigureOnlyMainOne AndAlso _mainConfig IsNot Nothing AndAlso curConfig.Equals(_mainConfig)))
            If onlyMain AndAlso curConfig.hasToUpdate Then configsNeedingUpdate.Add(curConfig)
        Next
        If configsNeedingUpdate.Count = 0 Then Exit Sub

        Dim configWindow As New Windows.Forms.ConfigurationsManager()
        configWindow.addConfigs(configsNeedingUpdate)
        Dim configResult As DialogResult = configWindow.ShowDialog()
        If configResult <> DialogResult.OK Then
            External.current.endSoftware()
        End If
    End Sub

    Private Sub loadConfigsData()
        For Each curConfig As ConfigBase In getItemables()
            curConfig.load()
        Next
    End Sub

    Private Sub loadConfigsClasses()
        'Skip all assemblies related to BaseLib
        Dim assembliesToSkip As New List(Of AssemblyName)
        assembliesToSkip.Add(Me.GetType.Assembly.GetName())
        assembliesToSkip.AddRange(Me.GetType.Assembly.GetReferencedAssemblies())

        For Each curAssembly As Assembly In AppDomain.CurrentDomain.GetAssemblies()
            Dim skipping As Boolean = False
            For Each skipped As AssemblyName In assembliesToSkip
                If skipped.FullName = curAssembly.GetName().FullName Then
                    skipping = True
                    Exit For
                End If
            Next
            If skipping Then Continue For

            searchAssembly(curAssembly)
        Next
    End Sub

    Public Overloads Function getItemable(ByVal [type] As Type) As ConfigBase
        For Each curItem As ConfigBase In getItemables()
            If curItem.GetType.FullName = [type].FullName Then Return curItem
        Next

        Return Nothing
    End Function

    Public Overloads Function getItemable(ByVal typeFullName As String) As ConfigBase
        For Each curItem As ConfigBase In getItemables()
            If curItem.GetType.FullName = typeFullName Then Return curItem
        Next

        Return Nothing
    End Function

    Private Sub searchAssembly(ByVal curAssembly As Assembly)
        For Each curType As Type In curAssembly.GetTypes()
            If curType.BaseType IsNot Nothing AndAlso curType.IsSubclassOf(GetType(ConfigBase)) Then
                Dim config As ConfigBase = Activator.CreateInstance(curType)
                If getItemable(config.GetType) IsNot Nothing Then Continue For

                If curType.Assembly.FullName = Assembly.GetEntryAssembly().FullName Then _mainConfig = config
                Me.addItemable(config)
            End If
        Next
    End Sub

End Class
