Public Class AlertOfClientAccount
    Inherits Alert

    Private myNoClient As Integer

    Public Sub New()
    End Sub

    Public Sub New(ByVal noClient As Integer)
        myNoClient = noClient
    End Sub

    Public Overrides ReadOnly Property alertType() As AlertsManager.AType
        Get
            Return AlertsManager.AType.OpenClientAccount
        End Get
    End Property

    Protected Overrides Function isAlertType(ByVal data As DataRow) As Boolean
        If data Is Nothing Then Return False

        If data("AlertType").StartsWith("OpenClientAccount") Then Return True
    End Function

    Public Overrides Sub loadData(ByVal data As DataRow, ByVal addAlarmToManager As Boolean)
        MyBase.loadData(data, addAlarmToManager)

        myNoClient = data("AlertData")
    End Sub

    Public Overrides Sub doAction()
        openAccount(myNoClient)
    End Sub

    Protected Overrides Sub loadAlarm(ByVal initString As String, Optional ByVal addAlarmToManager As Boolean = True)
        If initString = "" Then Exit Sub

        Dim initData() As String = initString.Split(New Char() {":"})
        alertAlarm = New AlarmOfClientAccount(Date.Parse(initData(0) & " " & initData(1) & ":" & initData(2)), initData(3), initData(4), initData(5))
        alertAlarm.alertAssociated = Me

        If addAlarmToManager Then AlarmManager.getInstance.addAlarm(alertAlarm)
    End Sub

    Public Overrides Sub saveData()
        Dim alarmData As String = ""
        If alertAlarm IsNot Nothing Then alarmData = alertAlarm.toString().Replace("'", "''")

        If Me.noAlert = 0 Then
            DBLinker.getInstance.writeDB(getSqlQuery(DBLinker.QueryTypes.Add), , , , Me.noAlert)
        Else
            DBLinker.getInstance.updateDB("UsersAlerts", "AlarmData='" & alarmData & "',IsHidden='" & Me.isHidden & "',IsNew='" & Me.isNew & "',Message='" & Me.toString.Replace("'", "''") & "'", "NoUserAlert", noAlert, False)
            onDataChanged()
        End If
    End Sub

    Public Function getSqlQuery(ByVal queryType As DBLinker.QueryTypes) As String
        Dim sql As String
        Dim alarmData As String = ""
        If alertAlarm IsNot Nothing Then alarmData = alertAlarm.toString().Replace("'", "''")

        Select Case queryType
            Case DBLinker.QueryTypes.Add
                sql = "INSERT INTO UsersAlerts(NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message) VALUES(" & _
                noUser & ",'" & Me.alertType.ToString.Replace("'", "''") & "','" & Me.myNoClient & "'," & IIf(Me.showingDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.showingDate) & " " & Me.showingDate.Hour & ":" & Me.showingDate.Minute & "'") & "," & IIf(Me.expiryDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.expiryDate) & " " & Me.expiryDate.Hour & ":" & Me.expiryDate.Minute & "'") & _
                ",'" & alarmData & "','" & isHidden & "','" & isNew & "','" & Me.toString.Replace("'", "''") & "')"
        End Select

        Return sql
    End Function

    Public Overrides ReadOnly Property alertData() As String
        Get
            Return myNoClient
        End Get
    End Property

    Public Overrides ReadOnly Property soundFile() As String
        Get
            Return getSoundFile("sonAlertClient")
        End Get
    End Property
End Class
