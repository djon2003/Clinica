Public Class SchedulesManager
    Inherits DBItemableManagerBase(Of SchedulesManager, Schedule)

#Region "Constructor"
    Protected Sub New()
        load()
    End Sub
#End Region

    Private fasterAccessor As New Generic.Dictionary(Of String, Schedule)

    Public Overrides Function addItemable(ByVal newItem As Schedule) As String
        Dim returning As String = MyBase.addItemable(newItem)
        fasterAccessor.Add(newItem.noUser & "_" & DateFormat.getTextDate(newItem.scheduleDate), newItem)

        Return returning
    End Function

    Public Overrides Sub removeItemable(ByVal noItem As Integer)
        Dim curHoraire As Schedule = getItemable(noItem)
        fasterAccessor.Remove(curHoraire.noUser & "_" & DateFormat.getTextDate(curHoraire.scheduleDate))
        MyBase.removeItemable(noItem)
    End Sub

    Public Overrides Sub clear()
        MyBase.clear()
        fasterAccessor.Clear()
    End Sub

    Public Overrides Sub load()
        load(0)
    End Sub


    Private Overloads Sub load(ByVal noSchedule As Integer)
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("Horaires", "*", IIf(noSchedule = 0, "", "NoHoraire=" & noSchedule))

        If noSchedule = 0 Then Me.clear()
        If noSchedule <> 0 AndAlso (data Is Nothing OrElse data.Tables.Count = 0 OrElse data.Tables(0).Rows.Count = 0) Then
            Dim horaire As Schedule = getItemable(noSchedule)
            If horaire IsNot Nothing Then
                fasterAccessor.Remove(horaire.noUser & "_" & DateFormat.getTextDate(horaire.scheduleDate))
                MyBase.removeItemable(horaire.noItemable)
            End If
            Exit Sub
        End If
        If (data Is Nothing OrElse data.Tables.Count = 0 OrElse data.Tables(0).Rows.Count = 0) Then Exit Sub

        If noSchedule <> 0 Then
            Dim horaire As Schedule = getItemable(noSchedule)
            Dim horaireData As New DBItemableData(data.Tables(0).Rows(0))
            If horaire Is Nothing Then
                horaire = New Schedule(horaireData)
                addItemable(horaire)
                Exit Sub
            End If
            horaire.loadData(horaireData)
        Else
            For Each curRow As DataRow In data.Tables(0).Rows
                Dim horaire As New Schedule(New DBItemableData(curRow))
                addItemable(horaire)
            Next
        End If
    End Sub

    Public Function getSchedule(ByVal noUser As Integer, ByVal scheduleDate As Date) As Schedule
        Dim key As String = noUser & "_" & DateFormat.getTextDate(scheduleDate)
        If fasterAccessor.ContainsKey(key) = False AndAlso scheduleDate = limitDate Then Return Nothing
        If fasterAccessor.ContainsKey(key) = False Then Return getDefaultSchedule(noUser)

        Return fasterAccessor(key)
    End Function

    Public Function getDefaultSchedule(ByVal noUser As Integer) As Schedule
        Return getSchedule(noUser, limitDate)
    End Function

    Public Function getSchedules(ByVal noUser As Integer, Optional ByVal excludeDefault As Boolean = False) As Generic.List(Of Schedule)
        Dim schedules As New Generic.List(Of Schedule)
        For Each curSchedule As Schedule In Me.fasterAccessor.Values
            If curSchedule.noUser = noUser AndAlso (excludeDefault = False OrElse curSchedule.isDefault = False) Then schedules.Add(curSchedule)
        Next

        Return schedules
    End Function

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function.StartsWith("Horaire") = False OrElse (dataReceived.function = "Horaire-Modif" AndAlso dataReceived.fromExternal = False) Then Exit Sub

        Select Case dataReceived.function
            Case "Horaire-Modif", "Horaire-Add"
                load(dataReceived.params(0))
            Case "Horaire-Del"
                Dim horaire As Schedule = getItemable(dataReceived.params(0))
                If horaire IsNot Nothing Then removeItemable(horaire.noSchedule)
            Case "Horaires"
                load()
        End Select
    End Sub

    Protected Overrides Sub sendUpdate()
        'InternalUpdatesManager.getInstance.sendupdate("Horaires()")
    End Sub
End Class
