Public Class AlertOfMailSystem
    Inherits Alert

    Private messagePath As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal message As String, ByVal messagePath As String, ByVal noMail As Integer)
        Me.message = message
        Me.messagePath = messagePath & "\" & noMail
    End Sub

    Public Overrides ReadOnly Property alertType() As AlertsManager.AType
        Get
            Return AlertsManager.AType.OpenMailSystem
        End Get
    End Property

    Protected Overrides Function isAlertType(ByVal data As DataRow) As Boolean
        If data Is Nothing Then Return False

        If data("AlertType").StartsWith("OpenMailSystem") Then Return True

        Return False
    End Function

    Public Overrides Sub loadData(ByVal data As DataRow, ByVal addAlarmToManager As Boolean)
        MyBase.loadData(data, addAlarmToManager)

        messagePath = data("AlertData")
    End Sub

    Public Overrides Sub doAction()
        Dim myMSGSystem As msgSystem = openUniqueWindow(New msgSystem())
        myMSGSystem.selectedMessage = messagePath
        myMSGSystem.Show()
    End Sub

    Protected Overrides Sub loadAlarm(ByVal initString As String, Optional ByVal addAlarmToManager As Boolean = True)
        'No alarm can be set on this object
    End Sub

    Public Overrides Sub saveData()
        Dim alarmData As String = ""
        If alertAlarm IsNot Nothing Then alarmData = alertAlarm.toString().Replace("'", "''")

        If Me.noAlert = 0 Then
            DBLinker.getInstance.writeDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", _
            noUser & ",'" & Me.alertType.ToString.Replace("'", "''") & "','" & Me.messagePath.Replace("'", "''") & "'," & IIf(Me.showingDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.showingDate) & " " & Me.showingDate.Hour & ":" & Me.showingDate.Minute & "'") & "," & IIf(Me.expiryDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.expiryDate) & " " & Me.showingDate.Hour & ":" & Me.showingDate.Minute & "'") & _
            ",'" & alarmData & "','" & isHidden & "','" & isNew & "','" & Me.toString.Replace("'", "''") & "'", , , , Me.noAlert)
        Else
            DBLinker.getInstance.updateDB("UsersAlerts", "AlarmData='" & alarmData & "',IsHidden='" & Me.isHidden & "',IsNew='" & Me.isNew & "',Message='" & Me.toString.Replace("'", "''") & "'", "NoUserAlert", noAlert, False)
            onDataChanged()
        End If

        'TODO: use If autoSendUpdateOnSave Then
    End Sub

    Public Overrides ReadOnly Property alertData() As String
        Get
            Return Me.messagePath
        End Get
    End Property

    Public Overrides ReadOnly Property soundFile() As String
        Get
            Return getSoundFile("sonAlertMSG")
        End Get
    End Property
End Class
