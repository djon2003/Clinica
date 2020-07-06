Public MustInherit Class AgendaEntry
    Inherits DBItemableBase
    Implements IAgendaEntry

    Protected data As DataRow
    Private _skipQueueList As Boolean = False
    Private _noFolder As Integer = 0
    Private _NoTRP As Integer = 0
    Private _isCutting As Boolean = False

#Region "Properties"
    Public ReadOnly Property isCutting() As Boolean
        Get
            Return _isCutting
        End Get
    End Property

    Public Property skipQueueList() As Boolean
        Get
            Return _skipQueueList
        End Get
        Set(ByVal value As Boolean)
            _skipQueueList = value
        End Set
    End Property

    Public Property dateHeure() As Date
        Get
            Return data("DateHeure")
        End Get
        Set(ByVal value As Date)
            data("DateHeure") = value
        End Set
    End Property

    Public Property noStatut() As Integer
        Get
            Return data("NoStatut")
        End Get
        Set(ByVal value As Integer)
            data("NoStatut") = value
        End Set
    End Property

    Public Overridable Property noClient() As Integer
        Get
            Return data("NoClient")
        End Get
        Set(ByVal value As Integer)
            data("NoClient") = value
        End Set
    End Property

    Public Property noAgendaEntry() As Integer
        Get
            If data.Table.Columns.Contains("NoVisiteAgenda") = False Then Return 0

            Return data("NoVisiteAgenda")
        End Get
        Set(ByVal value As Integer)
            data("NoVisiteAgenda") = value
        End Set
    End Property

    Public Property noFolder() As Integer
        Get
            Return _noFolder
        End Get
        Set(ByVal value As Integer)
            _noFolder = value
        End Set
    End Property

    Public Property period() As Integer
        Get
            Return data("Periode")
        End Get
        Set(ByVal value As Integer)
            data("Periode") = value
        End Set
    End Property

    Public Property noTRP() As Integer
        Get
            Return _NoTRP
        End Get
        Set(ByVal value As Integer)
            _NoTRP = value
        End Set
    End Property

    Public Overridable Property itemText() As String
        Get
            Return data("ItemText")
        End Get
        Set(ByVal value As String)
            data("ItemText") = value
        End Set
    End Property
#End Region

    Protected Sub New()
    End Sub

    Protected Sub setIsCutting(ByVal value As Boolean)
        _isCutting = value
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Me.data = data.mainData
        _noFolder = data.mainData("NoFolder")
        _NoTRP = data.mainData("NoTRP")
    End Sub

    Public Overridable Sub copy() Implements IAgendaEntry.copy
        _isCutting = False
    End Sub
    Public Overridable Sub cut() Implements IAgendaEntry.cut
        _isCutting = True
    End Sub
    Public Overridable Sub pasteTo(ByVal dateHeure As Date, ByVal noTRP As Integer, ByVal newPeriod As Integer) Implements IAgendaEntry.pasteTo
        _isCutting = False
    End Sub
    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("Agenda", "NoAgenda", noAgendaEntry, False)
        onDeleted()
        If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("Agendas(" & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.YYYYMMDD_FullTime) & ",False," & noTRP & ")")

        myMainWin.StatusText = "Suppression d'une plage le " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.YYYYMMDD) & " à " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.ShortTime) & IIf(noTRP <> 0, " pour " & UsersManager.getInstance.getUser(noTRP).toString, "")

        If CType(PreferencesManager.getGeneralPreferences()("ShowQLOnAgendaRemove"), Boolean) = True AndAlso skipQueueList = False And noTRP <> 0 Then
            openRestraintQueueList(dateHeure, noTRP)
        End If
    End Sub

    Protected Overrides Sub onDeleted()
        _isCutting = False

        MyBase.onDeleted()
    End Sub

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return noAgendaEntry
        End Get
    End Property

    Public Overrides Function toString() As String
        Return itemText.Replace("<br>", vbCrLf)
    End Function
End Class
