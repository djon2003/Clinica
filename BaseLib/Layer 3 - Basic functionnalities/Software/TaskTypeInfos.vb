Public Class TaskTypeInfos

    Private Const INFO_SPLITTER As String = "§§"
    Private base As PluginTaskBase

    Public Sub New(ByVal taskBase As PluginTaskBase)
        Me.base = taskBase
    End Sub

    Public Overrides Function ToString() As String
        Return base.getIdentifier() & _
                INFO_SPLITTER & base.hasToConfigure & _
                INFO_SPLITTER & base.maximumConcurrentTasks & _
                INFO_SPLITTER & base.name & _
                INFO_SPLITTER & base.description.Replace(vbCrLf, "§")
    End Function


End Class
