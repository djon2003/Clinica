Public Class AlarmOfKPAccount
    Inherits Alarm

    Private noKP As Integer

    Public Sub New(ByVal dateTime As Date, ByVal noKP As Integer)
        MyBase.New(dateTime)

        Me.noKP = noKP
    End Sub

    Public Overrides Sub doAction()
        MyBase.doAction()

        Dim myMsgBox As New MsgBox1
        If myMsgBox(alertAssociated.toString & vbCrLf & "Voulez-vous ouvrir le compte maintenant ou avoir un rappel ?", "Alarme", 2, "Ouvrir le compte", "Avoir un rappel", , False) = 1 Then
            openAccount(Me.noKP, CompteType.KP)
            alertAssociated.alertAlarm = Nothing
        Else
            Dim myDateChoice As New DateChoice()
            Dim chosenDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year, Date.Now.Year + 1, False, False, True, , , , Date.Today, , , , Date.Today.AddDays(1), , , , , 1)
            If chosenDate.Count <> 0 Then
                alertAssociated.alertAlarm = New AlarmOfKPAccount(chosenDate(0), Me.noKP)
                alertAssociated.alertAlarm.alertAssociated = alertAssociated
                AlarmManager.getInstance.addAlarm(alertAssociated.alertAlarm)
            End If
        End If

        alertAssociated.isHidden = False
        alertAssociated.saveData()
    End Sub

    Public Overloads Overrides Function equals(ByVal alarmToCompare As Alarm) As Boolean
        Return Me.alarmDone = alarmToCompare.alarmDone And Me.getAttachObject.Equals(alarmToCompare.getAttachObject) And Me.alertAssociated.toString.Equals(alarmToCompare.alertAssociated.toString)
    End Function

    Protected Friend Overrides Function getAttachObject() As Object
        Return noKP
    End Function

    Public Overrides Function toString() As String
        Return DateFormat.getTextDate(MyBase.getDateTime) & ":" & DateFormat.getTextDate(MyBase.getDateTime, DateFormat.TextDateOptions.ShortTime) & ":" & noKP
    End Function
End Class
