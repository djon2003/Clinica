Public Class PreferencesManager
    Inherits DBItemableManagerBase(Of PreferencesManager, Preferences)
    Implements IDataConsumer(Of DataInternalUpdate)

    Public Const ADMINISTRATOR_DEFAULT_PASSWORD As String = "AdminUser147963Power"

    Private preferences As New Generic.List(Of Preferences)
    Private loadLock As Object
    Private loadMutex As New Threading.Mutex()

    Protected Sub New()
        load()
        InternalUpdatesManager.getInstance.addConsumer(Me)
    End Sub

    Public Shared Function getUserPreferences() As Preferences
        Return PreferencesManager.getInstance.getPreferences(ConnectionsManager.currentUser)
    End Function

    Public Shared Function getGeneralPreferences() As Preferences
        Return PreferencesManager.getInstance.getPreferences(0)
    End Function

    Public Function getPreferences(ByVal noUser As Integer) As Preferences
        loadMutex.WaitOne()

        For Each curPref As Preferences In preferences
            If curPref.noUser = noUser Then
                loadMutex.ReleaseMutex()
                Return curPref
            End If
        Next

        loadMutex.ReleaseMutex()

        Throw New Exception("Preferences not found. noUser=" & noUser)
    End Function

    Public Overrides Sub load()
        Dim prefs As DataSet = DBLinker.getInstance.readDBForGrid("Preferences", "*")
        If prefs Is Nothing OrElse prefs.Tables.Count = 0 Then Exit Sub

        loadMutex.WaitOne()

        preferences.Clear()
        For Each curRow As DataRow In prefs.Tables(0).Rows
            Dim newPref As New Preferences
            newPref.loadData(New DBItemableData(curRow))
            preferences.Add(newPref)
        Next

        loadMutex.ReleaseMutex()
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function <> "Preferences" OrElse dataReceived.fromExternal = False Then Exit Sub

        load()
    End Sub

    Protected Overrides Sub sendUpdate()
        InternalUpdatesManager.getInstance.sendUpdate("Preferences()")
    End Sub
End Class
