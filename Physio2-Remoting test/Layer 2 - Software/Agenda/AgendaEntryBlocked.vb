Public Class AgendaEntryBlocked
    Inherits AgendaEntry

    Public Sub New(ByVal loadingData As DBItemableData)
        MyBase.New()
        MyBase.skipQueueList = True
        loadData(loadingData)
    End Sub

    Public Overrides Property itemText() As String
        Get
            Return PreferencesManager.getGeneralPreferences()("TextForPlageBloquee")
        End Get
        Set(ByVal value As String)
            Throw New NotSupportedException("Cette fonction n'est pas d�fini, car il s'agit d'une param�tre des pr�f�rences..")
        End Set
    End Property

    Public Overrides Sub copy()
        Throw New NotSupportedException("Impossible de copier une plage bloqu�e")
    End Sub

    Public Overrides Sub cut()
        Throw New NotSupportedException("Impossible de couper une plage bloqu�e")
    End Sub

    Public Overrides Sub delete()
        'Droit & Acc�s
        If currentDroitAcces(74) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de g�rer les plages bloqu�es." & vbCrLf & "Merci!", "Droit & Acc�s")
            Exit Sub
        End If

        MyBase.delete()
    End Sub

    Public Overrides Sub pasteTo(ByVal dateHeure As Date, ByVal noTRP As Integer, ByVal newPeriod As Integer)
        Throw New NotSupportedException("Impossible de coller une plage bloqu�e")
    End Sub

    Public Overrides Sub saveData()
        Throw New NotImplementedException()

        'TODO: use If autoSendUpdateOnSave Then
        onDataChanged()
    End Sub
End Class
