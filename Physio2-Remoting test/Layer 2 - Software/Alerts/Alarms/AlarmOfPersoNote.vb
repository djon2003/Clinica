Public Class AlarmOfPersoNote
    Inherits Alarm

    Public Sub New(ByVal dateTime As Date)
        MyBase.New(dateTime)
    End Sub

    Public Overrides Sub doAction()
        MyBase.doAction()

        CType(Me.alertAssociated, AlertOfPersoNote).persoNote.resetAlarmDate()
        CType(Me.alertAssociated, AlertOfPersoNote).persoNote.weakUpFromAlarm()

        alertAssociated.alertAlarm = Nothing
        alertAssociated.saveData()
    End Sub

    Public Overloads Overrides Function equals(ByVal alarmToCompare As Alarm) As Boolean
        If Me.getAttachObject Is Nothing Then Return False

        Return Me.alarmDone = alarmToCompare.alarmDone And Me.getAttachObject.Equals(alarmToCompare.getAttachObject)
    End Function

    Protected Friend Overrides Function getAttachObject() As Object
        Return CType(Me.alertAssociated, AlertOfPersoNote).persoNote
    End Function

    Public Overrides Function toString() As String
        Return DateFormat.getTextDate(MyBase.getDateTime) & ":" & DateFormat.getTextDate(MyBase.getDateTime, DateFormat.TextDateOptions.ShortTime)
    End Function
End Class
