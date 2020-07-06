Public MustInherit Class Alarm
    Implements IItemable

    Private myDateTime As Date

    Private _AlertAssociated As Alert
    Private _AlarmDone As Boolean = False

    Public Event noItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer) Implements IItemable.noItemableChanged

    Protected Overridable Sub onNoItemableChanged(ByVal oldNoItemable As Integer, ByVal newNoItemable As Integer)
        RaiseEvent noItemableChanged(oldNoItemable, newNoItemable)
    End Sub

    Public Property alertAssociated() As Alert
        Get
            Return _AlertAssociated
        End Get
        Set(ByVal value As Alert)
            _AlertAssociated = value
        End Set
    End Property

    Public ReadOnly Property alarmDone() As Boolean
        Get
            Return _AlarmDone
        End Get
    End Property

    Protected Sub alarmIsDone()
        _AlarmDone = True
    End Sub

    Protected Sub New(ByVal dateTime As Date)
        myDateTime = dateTime
    End Sub

    Public Function isNow() As Boolean
        If _AlarmDone Then Return False

        If myDateTime.Date < Date.Now.Date Or (myDateTime.Date = Date.Now.Date And ((myDateTime.TimeOfDay.Hours = Date.Now.TimeOfDay.Hours And myDateTime.TimeOfDay.Minutes <= Date.Now.TimeOfDay.Minutes) Or myDateTime.TimeOfDay.Hours < Date.Now.TimeOfDay.Hours)) Then Return True

        Return False
    End Function

    Public Overridable Sub doAction()
        _AlarmDone = True
        AlertsManager.getInstance.playAlertSound(Me.alertAssociated)
    End Sub

    Public MustOverride Overloads Function equals(ByVal alarmToCompare As Alarm) As Boolean

    Public MustOverride Overloads Function toString() As String

    Protected Friend MustOverride Function getAttachObject() As Object

    Public Function getDateTime() As Date
        Return myDateTime
    End Function

    Public Sub setDateTime(ByVal newDate As Date)
        myDateTime = New Date
    End Sub

    Public ReadOnly Property noItemable() As Integer Implements IItemable.noItemable
        Get
            If alertAssociated Is Nothing Then Return 0

            Return alertAssociated.noAlert
        End Get
    End Property
End Class
