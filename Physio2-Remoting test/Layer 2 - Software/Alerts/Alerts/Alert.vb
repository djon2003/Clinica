Public MustInherit Class Alert
    Inherits DBItemableBase

    Private _showingDate As Date = LIMIT_DATE
    Private myExpiryDate As Date = LIMIT_DATE
    Private myMessage As String = String.Empty
    Private _IsHidden As Boolean
    Private _Alarm As Alarm
    Private _NoAlert As String
    Private _IsNew As Boolean = True
    Private _NoUser As Integer = 0


#Region "Properties"
    Public Property message() As String
        Get
            Return myMessage
        End Get
        Set(ByVal value As String)
            myMessage = value
        End Set
    End Property

    Public Property noUser() As Integer
        Get
            Return _NoUser
        End Get
        Set(ByVal value As Integer)
            _NoUser = value
        End Set
    End Property

    Public Property expiryDate() As Date
        Get
            Return myExpiryDate
        End Get
        Set(ByVal value As Date)
            myExpiryDate = value
        End Set
    End Property

    Public Property showingDate() As Date
        Get
            Return _showingDate
        End Get
        Set(ByVal value As Date)
            _showingDate = value
        End Set
    End Property

    Public Property isHidden() As Boolean
        Get
            Return _IsHidden
        End Get
        Set(ByVal value As Boolean)
            _IsHidden = value
        End Set
    End Property

    Public Property alertAlarm() As Alarm
        Get
            Return _Alarm
        End Get
        Set(ByVal value As Alarm)
            _Alarm = value
        End Set
    End Property

    Public Property noAlert() As Integer
        Get
            Return _NoAlert
        End Get
        Set(ByVal value As Integer)
            _NoAlert = value
        End Set
    End Property

    Public ReadOnly Property isNew() As Boolean
        Get
            Return _IsNew
        End Get
    End Property
#End Region

    Protected Function getSoundFile(ByVal prefUser As String) As String
        Dim sf As String = ""
        Dim sound As String = PreferencesManager.getUserPreferences()(prefUser)
        If sound.StartsWith("DB:\") Then
            Dim curDBItem As InternalDBItem
            Try
                curDBItem = InternalDBItem.getItemFromLink(sound.Substring(4))
            Catch ex As Exception 'REM Exception not handle

            End Try
            If curDBItem Is Nothing Then
                sf = ""
            Else
                sf = appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile
            End If
        ElseIf sound = "* Par défaut *" Then
            sf = AlertsManager.getInstance.defaultSonPath
        End If

        If sf <> "" AndAlso IO.File.Exists(sf) = False Then sf = ""

        Return sf
    End Function

    Public MustOverride ReadOnly Property soundFile() As String

    Public Sub setOld()
        _IsNew = False
    End Sub

    Public Sub setNew()
        _IsNew = True
    End Sub

    Public Event dataSaved(ByVal sender As Alert)

    Public MustOverride ReadOnly Property alertType() As AlertsManager.AType

    Protected Sub New()

    End Sub

    Public MustOverride Sub doAction()

    Protected Sub setIsNew(ByVal value As Boolean)
        _IsNew = value
    End Sub

    Protected MustOverride Function isAlertType(ByVal data As DataRow) As Boolean
    Protected MustOverride Sub loadAlarm(ByVal initString As String, Optional ByVal addAlarmToManager As Boolean = True)

    Public MustOverride ReadOnly Property alertData() As String

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", Me.noAlert, False)

        'TODO: use         If autoSendUpdateOnDelete Then
        onDeleted()
    End Sub

    Public Overridable Overloads Function toString() As String
        Return myMessage
    End Function


    Public Overrides Sub loadData(ByVal data As DBItemableData)
        loadData(data.mainData, True)
    End Sub

    Public Overridable Overloads Sub loadData(ByVal data As DataRow, ByVal addAlarmToManager As Boolean)
        _IsNew = data("IsNew")
        myMessage = data("Message").ToString.Replace("\n", vbCrLf)

        _NoAlert = data("NoUserAlert")
        _NoUser = data("NoUser")
        If Not TypeOf data("AffDate") Is System.DBNull AndAlso data("AffDate").ToString <> "" Then _showingDate = Date.Parse(data("AffDate"))
        If Not TypeOf data("ExpDate") Is System.DBNull AndAlso data("ExpDate").ToString <> "" Then myExpiryDate = Date.Parse(data("ExpDate"))
        _IsHidden = data("IsHidden")
        If Not TypeOf data("AlarmData") Is System.DBNull AndAlso data("AlarmData").ToString <> "" Then loadAlarm(data("AlarmData"), addAlarmToManager)
    End Sub

    Public Shared Function getAlertType(ByVal data As DataRow) As Alert
        Dim newAlert As Alert
        newAlert = New AlertOfQueueList
        If newAlert.isAlertType(data) Then Return newAlert

        newAlert = New AlertOfReportGenerator
        If newAlert.isAlertType(data) Then Return newAlert

        newAlert = New AlertOfKPAccount
        If newAlert.isAlertType(data) Then Return newAlert

        newAlert = New AlertOfMailSystem
        If newAlert.isAlertType(data) Then Return newAlert

        newAlert = New AlertOfPersoNote
        If newAlert.isAlertType(data) Then Return newAlert

        newAlert = New AlertOfClientAccount
        If newAlert.isAlertType(data) Then Return newAlert

        Throw New Exception("Not valid alert data")
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noAlert
        End Get
    End Property
End Class
