Public Class AlertOfQueueList
    Inherits Alert

    Private myDateToLook As Date
    Private myNoUser As Integer

    Public Overrides ReadOnly Property alertType() As AlertsManager.AType
        Get
            Return AlertsManager.AType.ContinueQueueListe
        End Get
    End Property

    Public ReadOnly Property dateToLook() As Date
        Get
            Return myDateToLook
        End Get
    End Property

    Public ReadOnly Property noTRP() As Integer
        Get
            Return myNoUser
        End Get
    End Property

    Protected Overrides Function isAlertType(ByVal data As DataRow) As Boolean
        If data Is Nothing Then Return False

        If data("AlertType").StartsWith("ContinueQueueListe") Then Return True
    End Function

    Public Overrides Sub loadData(ByVal data As DataRow, ByVal addAlarmToManager As Boolean)
        MyBase.loadData(data, addAlarmToManager)

        Dim myAlertOptions() As String = System.Text.RegularExpressions.Regex.Split(data("AlertData"), "\:")

        myDateToLook = Date.Parse(myAlertOptions(0) & " " & myAlertOptions(1) & ":" & myAlertOptions(2))
        myNoUser = Integer.Parse(myAlertOptions(3))
    End Sub

    Public Overrides Sub doAction()
        'Droit & Accès
        If currentDroitAcces(74) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les plages bloquées." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myNoAgenda() As String = DBLinker.getInstance.readOneDBField("Agenda", "NoAgenda", "WHERE DateHeure='" & DateFormat.getTextDate(myDateToLook, DateFormat.TextDateOptions.YYYYMMDD) & " " & DateFormat.getTextDate(myDateToLook, DateFormat.TextDateOptions.ShortTime) & "' AND NoStatut=7 AND NoTRP = " & myNoUser)
        If myNoAgenda Is Nothing OrElse myNoAgenda.Length = 0 Then Exit Sub

        removingAgendaEntry(, , , myDateToLook, myNoAgenda(0), , , UsersManager.getInstance.getUser(myNoUser).toString)
        openRestraintQueueList(myDateToLook, myNoUser)
    End Sub

    Protected Overrides Sub loadAlarm(ByVal initString As String, Optional ByVal addAlarmToManager As Boolean = True)
        'No alarm can be set on this object
    End Sub

    Public Overrides Sub saveData()
        Dim alarmData As String = ""
        If alertAlarm IsNot Nothing Then alarmData = alertAlarm.toString().Replace("'", "''")

        If Me.noAlert = 0 Then
            DBLinker.getInstance.writeDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", _
            noUser & ",'" & Me.alertType.ToString.Replace("'", "''") & "','" & Me.alertData & "'," & IIf(Me.showingDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.showingDate) & " " & Me.showingDate.Hour & ":" & Me.showingDate.Minute & "'") & "," & IIf(Me.expiryDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.expiryDate) & " " & Me.expiryDate.Hour & ":" & Me.expiryDate.Minute & "'") & _
            ",'" & alarmData & "','" & isHidden & "','" & isNew & "','" & Me.toString.Replace("'", "''") & "'", , , , Me.noAlert)
        Else
            DBLinker.getInstance.updateDB("UsersAlerts", "AlarmData='" & alarmData & "',IsHidden='" & Me.isHidden & "',IsNew='" & Me.isNew & "',Message='" & Me.toString.Replace("'", "''") & "'", "NoUserAlert", noAlert, False)
            onDataChanged()
        End If
    End Sub

    Public Overrides ReadOnly Property alertData() As String
        Get
            Return DateFormat.getTextDate(dateToLook) & ":" & dateToLook.Hour & ":" & dateToLook.Minute & ":" & myNoUser
        End Get
    End Property

    Public Overrides ReadOnly Property soundFile() As String
        Get
            Return getSoundFile("sonAlertQL")
        End Get
    End Property
End Class
