Public Class AlertOfPersoNote
    Inherits Alert

    Private myPersoNote As PersoNote
    Private controlCreated As Boolean = False

    Public Overrides ReadOnly Property alertType() As AlertsManager.AType
        Get
            Return AlertsManager.AType.OpenNote
        End Get
    End Property

    Public ReadOnly Property persoNote() As PersoNote
        Get
            Return myPersoNote
        End Get
    End Property

    Public Overrides Sub delete()
        myPersoNote.deleteControl()

        MyBase.delete()
    End Sub

    Protected Overrides Function isAlertType(ByVal data As DataRow) As Boolean
        If data Is Nothing Then Return False

        If data("AlertType").StartsWith("OpenNote") Then Return True
    End Function

    Public Overrides Sub loadData(ByVal data As DataRow, ByVal addAlarmToManager As Boolean)
        Dim myNotes As Generic.List(Of PersoNote) = AlertsManager.getInstance.getNotes()
        Dim found As Boolean = False
        For i As Integer = 0 To myNotes.Count - 1
            With myNotes(i)
                If .alertAssociated.noAlert = noAlert Then
                    .alertAssociated = Me
                    myPersoNote = myNotes(i)
                    found = True
                End If
            End With
        Next i
        If found = False Then
            myPersoNote = New PersoNote(Me)
            With myPersoNote
                .blockMove = False
                .Location = New Point(myMainWin.mdiChildRectangle.Location)
                .title = data("AlertData")
                .visible = False
            End With
            myMainWin.barMainObjects.addControl(myPersoNote)
            If Me.isNew Then myPersoNote.centerObject()
        End If

        MyBase.loadData(data, addAlarmToManager)

        If Not alertAlarm Is Nothing Then myPersoNote.setDateTime = alertAlarm.getDateTime
        myPersoNote.setMessage = MyBase.toString.Replace("<BR>", vbCrLf)
    End Sub

    Public Overrides Sub doAction()
        myPersoNote.visible = True
        myPersoNote.IsSwitchedToToolbar = myMainWin.AlertMessages.IsSwitchedToToolbar
        myPersoNote.BringToFront()
    End Sub

    Public Overrides Function toString() As String
        Return myPersoNote.title
    End Function

    Protected Overrides Sub loadAlarm(ByVal initString As String, Optional ByVal addAlarmToManager As Boolean = True)
        If initString = "" Then Exit Sub

        Dim initData() As String = initString.Split(New Char() {":"}, 2)

        alertAlarm = New AlarmOfPersoNote(Date.Parse(initData(0) & " " & initData(1)))
        alertAlarm.alertAssociated = Me

        If addAlarmToManager Then AlarmManager.getInstance.addAlarm(alertAlarm)
    End Sub

    Public Overrides Sub saveData()
        Dim alarmData As String = ""
        If alertAlarm IsNot Nothing Then alarmData = alertAlarm.toString().Replace("'", "''")

        If Me.noAlert = 0 Then
            DBLinker.getInstance.writeDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", _
            noUser & ",'" & Me.alertType.ToString.Replace("'", "''") & "','" & Me.myPersoNote.title.Replace("'", "''") & "'," & IIf(Me.showingDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.showingDate) & " " & Me.showingDate.Hour & ":" & Me.showingDate.Minute & "'") & "," & IIf(Me.expiryDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.expiryDate) & " " & Me.expiryDate.Hour & ":" & Me.expiryDate.Minute & "'") & _
            ",'" & alarmData & "','" & isHidden & "','" & isNew & "',''", , , , Me.noAlert)
        Else
            DBLinker.getInstance.updateDB("UsersAlerts", "AlertData='" & myPersoNote.title.Replace("'", "''") & "',AlarmData='" & alarmData & "',IsHidden='" & Me.isHidden & "',IsNew='" & Me.isNew & "',Message='" & System.Text.RegularExpressions.Regex.Replace(Me.persoNote.Note.Rtf, "(\r\n|\r|\n)", "<BR>").Replace("'", "''") & "'", "NoUserAlert", noAlert, False)
            onDataChanged()
        End If

        'TODO: use If autoSendUpdateOnSave Then
    End Sub

    Public Overrides ReadOnly Property alertData() As String
        Get
            Return myPersoNote.title
        End Get
    End Property

    Public Overrides ReadOnly Property soundFile() As String
        Get
            Return getSoundFile("sonAlertNote")
        End Get
    End Property
End Class
