Public Class AlarmManager
    Inherits ItemableManagerBase(Of AlarmManager, Alarm)
    Implements IDisposable

    Private _Started As Boolean = False
    Private WithEvents alarmTimer As System.Windows.Forms.Timer

    Protected Sub New()
        alarmTimer = New System.Windows.Forms.Timer()
        alarmTimer.Stop()
        alarmTimer.Interval = 1000
        AddHandler alarmTimer.Tick, AddressOf tick
    End Sub

    Public Sub cleanExpiredAlarms()
        Dim alerts As Generic.List(Of Alert) = AlertsManager.getInstance.getItemables
        Dim delAlarms As New Generic.List(Of Alarm)
        For Each curAlarm As Alarm In getItemables()
            If curAlarm.alertAssociated IsNot Nothing AndAlso alerts.IndexOf(curAlarm.alertAssociated) <> -1 Then delAlarms.Add(curAlarm)
        Next
        For Each delAlarm As Alarm In delAlarms
            removeItemable(delAlarm)
        Next

        If MyBase.count = 0 Then alarmTimer.Stop()
    End Sub

    Public Sub dispose() Implements System.IDisposable.Dispose
        alarmTimer.Stop()
        alarmTimer.Dispose()
        RemoveHandler alarmTimer.Tick, AddressOf tick
    End Sub

    Public Sub addAlarm(ByVal newAlarm As Alarm)
        If MyBase.count = 0 Then
            alarmTimer.Start()
            _Started = True
        Else
            'If alarm already exists, quit
            Dim existingAlarm As Alarm = getItemable(newAlarm.noItemable)
            If existingAlarm IsNot Nothing Then Exit Sub
        End If

        MyBase.addItemable(newAlarm)
    End Sub

    Private Sub tick(ByVal sender As Object, ByVal e As System.EventArgs)
        If MyBase.count = 0 Then Me.alarmTimer.Stop() : Exit Sub
        Dim thisAlarm As Alarm

        Dim curAlarms As New Generic.List(Of Alarm)
        curAlarms.AddRange(getItemables())

        For Each thisAlarm In curAlarms
            If AlertsManager.getInstance.getItemables.IndexOf(thisAlarm.alertAssociated) = -1 Then
                removeItemable(thisAlarm)
                Exit Sub 'Quit to update CurAlarms.. meaning that next Alarms will be taken by next Tick
            End If

            If thisAlarm.alarmDone = False AndAlso thisAlarm.isNow Then
                thisAlarm.doAction()
                removeItemable(thisAlarm)
                Exit Sub 'Quit to update CurAlarms.. meaning that next Alarms will be taken by next Tick
            End If
        Next
    End Sub


    Public Overrides Sub clear()
        MyBase.clear()
        alarmTimer.Stop()
        _Started = False
    End Sub

    Public ReadOnly Property started() As Boolean
        Get
            Return _Started
        End Get
    End Property
End Class
