Public Class AgendaEntryReserved
    Inherits AgendaEntry

    Private cutting As Boolean = False

    Public Sub New(ByVal loadingData As DBItemableData)
        MyBase.New()
        loadData(loadingData)
    End Sub

    Public Overrides Sub copy()
        'Droit & Acc�s
        If currentDroitAcces(73) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de copier les plages r�serv�es." & vbCrLf & "Merci!", "Droit & Acc�s")
            Exit Sub
        End If
        myMainWin.copyBox.setClient(Me, period / 15 - 1)
        MyBase.copy()
    End Sub

    Public Overrides Sub cut()
        copy()
        cutting = True
        delete()
        cutting = False

        MyBase.cut()
    End Sub

    Public Overrides Sub delete()
        'Droit & Acc�s
        If currentDroitAcces(73) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de supprimer les plages r�serv�es." & vbCrLf & "Merci!", "Droit & Acc�s")
            Exit Sub
        End If
        If cutting = False AndAlso MessageBox.Show("�tes-vous s�r de vouloir supprimer cette plage r�serv�e ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        MyBase.delete()
    End Sub

    Public Overrides Sub pasteTo(ByVal dateHeure As Date, ByVal noTRP As Integer, ByVal newPeriod As Integer)
        Dim tc As String = AgendaManager.getInstance.checkTimeConflict(dateHeure, newPeriod, Me.noTRP)
        If Not tc = "" Then MessageBox.Show(tc, "Conflit") : Exit Sub

        'Ajout dans l'agenda
        DBLinker.getInstance.writeDB("Agenda", "DateHeure,Periode,NoTRP,Reserve", "'" & DateFormat.getTextDate(dateHeure) & " " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.FullTime) & "'," & newPeriod & "," & Me.noTRP & ",'" & itemText.Replace("'", "''") & "'")

        myMainWin.StatusText = "Ajout d'une plage r�serv�e le " & DateFormat.getTextDate(dateHeure) & " � " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.ShortTime)

        noClient = 0
        InternalUpdatesManager.getInstance.sendUpdate("Agendas(" & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.YYYYMMDD_FullTime) & ",False," & Me.noTRP & ")")

        MyBase.pasteTo(dateHeure, noTRP, newPeriod)
    End Sub

    Public Overrides Sub saveData()
        Throw New NotImplementedException()

        'TODO: use If autoSendUpdateOnSave Then
        onDataChanged()
    End Sub
End Class
