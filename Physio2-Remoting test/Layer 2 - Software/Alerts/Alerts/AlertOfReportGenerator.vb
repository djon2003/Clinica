Public Class AlertOfReportGenerator
    Inherits Alert

    Public Overrides ReadOnly Property alertType() As AlertsManager.AType
        Get
            Return AlertsManager.AType.OpenRapportGenerator
        End Get
    End Property

    Protected Overrides Function isAlertType(ByVal data As DataRow) As Boolean
        If data Is Nothing Then Return False

        If data("AlertType").StartsWith("OpenRapportGenerator") Then Return True
    End Function

    Public Overrides Sub loadData(ByVal data As DataRow, ByVal addAlarmToManager As Boolean)
        MyBase.loadData(data, addAlarmToManager)
    End Sub

    Public Overrides Sub doAction()
        Dim myRapportGeneration As ReportGeneration = openUniqueWindow(New ReportGeneration)
        myRapportGeneration.Show()
    End Sub

    Protected Overrides Sub loadAlarm(ByVal initString As String, Optional ByVal addAlarmToManager As Boolean = True)
        'No alarm can be set on this object
    End Sub

    Public Overrides Sub saveData()
        Dim alarmData As String = ""
        If alertAlarm IsNot Nothing Then alarmData = alertAlarm.toString().Replace("'", "''")

        If Me.noAlert = 0 Then
            DBLinker.getInstance.writeDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", _
            noUser & ",'" & Me.alertType.ToString.Replace("'", "''") & "',''," & IIf(Me.showingDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.showingDate) & " " & Me.showingDate.Hour & ":" & Me.showingDate.Minute & "'") & "," & IIf(Me.expiryDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.expiryDate) & " " & Me.expiryDate.Hour & ":" & Me.expiryDate.Minute & "'") & _
            ",'" & alarmData & "','" & isHidden & "','" & isNew & "','" & Me.toString.Replace("'", "''") & "'", , , , Me.noAlert)
        Else
            DBLinker.getInstance.updateDB("UsersAlerts", "AlarmData='" & alarmData & "',IsHidden='" & Me.isHidden & "',IsNew='" & Me.isNew & "',Message='" & Me.toString.Replace("'", "''") & "'", "NoUserAlert", noAlert, False)
            onDataChanged()
        End If

        'TODO: use If autoSendUpdateOnSave Then
    End Sub

    Public Overrides ReadOnly Property alertData() As String
        Get
            Return ""
        End Get
    End Property

    Public Overrides ReadOnly Property soundFile() As String
        Get
            Return getSoundFile("sonAlertRapportGen")
        End Get
    End Property
End Class
