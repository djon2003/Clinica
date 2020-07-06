Public Class SpecialDatesManager
    Inherits DBItemableManagerBase(Of SpecialDatesManager, SpecialDate)

    Protected Sub New()
        MyBase.New()
    End Sub


    Public Function getSpecialDates(ByVal dateToTest As Date) As Generic.List(Of SpecialDate)
        Dim curList As New Generic.List(Of SpecialDate)

        For Each curSD As SpecialDate In getItemables()
            If curSD.isDateSpecial(dateToTest) Then curList.Add(curSD)
        Next

        Return curList
    End Function


    Public Overrides Sub load()
        Dim sdData As DataSet = DBLinker.getInstance.readDBForGrid("SpecialDates", "*")
        If sdData Is Nothing OrElse sdData.Tables.Count = 0 Then
            Me.clear()
            Exit Sub
        End If

        'The creation of a list is to minimize impact on lock by differing objects creation
        Dim specialDates As New Generic.List(Of SpecialDate)(sdData.Tables(0).Rows.Count)
        With sdData.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                specialDates.Add(New SpecialDate(New DBItemableData(.Item(i))))
            Next i
        End With

        'Change list of itembles
        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        Me.clear()
        For Each curSD As SpecialDate In specialDates
            addItemable(curSD)
        Next
        changingItemablesLock.ReleaseWriterLock()
    End Sub

    Public Sub addSpecialDate(ByVal nom As String, ByVal maxYear As Integer, ByVal method As SpecialDate.SpecialDateMethod, ByVal mois1 As Integer, ByVal jour1 As Integer, ByVal mois2 As Integer, ByVal position2 As SpecialDate.SpecialDatePosition, ByVal journee2 As Integer, ByVal journee3 As Integer, ByVal relative3 As SpecialDate.SpecialDateRelative, ByVal mois3 As Integer, ByVal jour3 As Integer, ByVal baseDay4 As Integer, ByVal relative4 As SpecialDate.SpecialDateRelative, ByVal nbDays4 As Integer, ByVal codeVBNet As String)
        Dim newSpecialDate As New SpecialDate
        newSpecialDate.nom = nom
        newSpecialDate.maxYear = maxYear
        newSpecialDate.method = method
        newSpecialDate.month1 = mois1
        newSpecialDate.jour1 = jour1
        newSpecialDate.month2 = mois2
        newSpecialDate.position2 = position2
        newSpecialDate.journee2 = journee2
        newSpecialDate.journee3 = journee3
        newSpecialDate.relative3 = relative3
        newSpecialDate.month3 = mois3
        newSpecialDate.jour3 = jour3
        newSpecialDate.relative4 = relative4
        newSpecialDate.nbDays4 = nbDays4
        newSpecialDate.baseDay4 = baseDay4
        newSpecialDate.codeVBNet = codeVBNet

        newSpecialDate.saveData()

        addItemable(newSpecialDate)

        InternalUpdatesManager.getInstance.sendUpdate("SpecialDates()")
    End Sub

    Public Function isDateSpecial(ByVal dateToTest As Date) As Boolean
        For Each curSD As SpecialDate In getItemables()
            If curSD.isDateSpecial(dateToTest) Then Return True
        Next

        Return False
    End Function

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function = "SpecialDates" Then
            load()
        End If
    End Sub

    Protected Overrides Sub sendUpdate()
        InternalUpdatesManager.getInstance.sendUpdate("SpecialDates()")
    End Sub
End Class
