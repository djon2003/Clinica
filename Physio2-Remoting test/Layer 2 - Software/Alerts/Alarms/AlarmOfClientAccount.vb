Imports CI.Clinica.Accounts.Clients.Folders
Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Public Class AlarmOfClientAccount
    Inherits Alarm

    Private noClient As Integer
    Private noFolder As Integer
    Private createTexte As Boolean

    Public Sub New(ByVal dateTime As Date, ByVal noClient As Integer, ByVal noFolder As Integer, ByVal createTexte As Boolean)
        MyBase.New(dateTime)

        Me.noClient = noClient
        Me.noFolder = noFolder
        Me.createTexte = createTexte
    End Sub

    Public Overrides Sub doAction()
        MyBase.doAction()

        'REM_CODES
        Dim setOld As Boolean = False
        Dim myMsgBox As New MsgBox1

        'Manage rappel
        If myMsgBox(alertAssociated.toString & vbCrLf & "Voulez-vous ouvrir le compte maintenant ou avoir un rappel ?", "Rapport du", 2, "Ouvrir le compte", "Avoir un rappel", , False) = 2 Then
            Dim myDateChoice As New DateChoice()
            Dim chosenDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year, Date.Now.Year + 1, False, False, True, , , , Date.Today, , , , Date.Today.AddDays(1), , , , , 1)
            If chosenDate.Count <> 0 Then
                Try
                    alertAssociated.alertAlarm = New AlarmOfClientAccount(chosenDate(0), Me.noClient, Me.noFolder, False)
                    alertAssociated.alertAlarm.alertAssociated = alertAssociated
                    AlarmManager.getInstance.addAlarm(alertAssociated.alertAlarm)
                Catch ex As Exception
                    addErrorLog(New Exception("ChosenDate=" & chosenDate(0), ex))
                End Try
            End If
        Else
            alertAssociated.alertAlarm = Nothing
            openAccount(Me.noClient)
            setOld = True
        End If

        Dim wasARappel As Boolean = Not alertAssociated.isHidden
        alertAssociated.isHidden = False
        If setOld Then alertAssociated.setOld()
        alertAssociated.saveData()

        'Add FolderTexte s'il ne s'agit pas d'un rappel (car déjà ajouté une fois le texte suivant)
        If wasARappel = False AndAlso createTexte And Me.noFolder > 0 Then
            Dim curFT As FolderText = FolderText.getFromAlert(Me.alertAssociated.noAlert)
            If curFT IsNot Nothing Then
                Dim curFTT As FolderTextType = curFT.getFolderTexteType
                'Vérifie que le dossier soit ouvert et que le texte n'existe pas déjà
                Dim dataRequired(,) As String = DBLinker.getInstance.readDB("InfoFolders", "StatutOuvert,(SELECT COUNT(*) FROM FolderTextes AS FT WHERE FT.NoFolder=" & noFolder & " AND FT.NoFolderTexteType=" & curFT.noFolderTexteType & " AND FT.NoMultiple=" & (curFT.noMultiple + 1) & ") AS NextFT", "WHERE InfoFolders.NoFolder = " & Me.noFolder)
                If dataRequired(0, 0) = True AndAlso dataRequired(1, 0) = 0 AndAlso (curFTT.whenToBeStopped <> FolderTextType.WhenToBeStop.OnMaxReached OrElse curFT.noMultiple < curFTT.nbMultipleEnding) Then
                    FolderText.add(curFT.noFolderTexteType, Me.noClient, Me.noFolder, curFTT.textTitle & " " & (curFT.noMultiple + 1), curFT.dateStarted.AddDays(curFTT.nbDaysMultiple), curFT.noMultiple + 1, Me.alertAssociated.noUser)
                    InternalUpdatesManager.getInstance.sendUpdate("AccountsDossierTextBoxes(" & noClient & "," & noFolder & ")")
                    AlertsManager.sendUpdate(Me.alertAssociated.noUser)
                End If
            End If
        End If

        'Set This alert has been done
        DBLinker.getInstance.updateDB("FolderAlerts", "IsAlertDone=1", "LastNoUserAlert", Me.alertAssociated.noAlert, False)
    End Sub

    Public Overloads Overrides Function equals(ByVal alarmToCompare As Alarm) As Boolean
        Return Me.alarmDone = alarmToCompare.alarmDone And Me.getAttachObject.Equals(alarmToCompare.getAttachObject) And Me.alertAssociated.toString.Equals(alarmToCompare.alertAssociated.toString)
    End Function

    Protected Friend Overrides Function getAttachObject() As Object
        Return noClient
    End Function

    Public Overrides Function toString() As String
        Return DateFormat.getTextDate(MyBase.getDateTime) & ":" & DateFormat.getTextDate(MyBase.getDateTime, DateFormat.TextDateOptions.ShortTime) & ":" & noClient & ":" & noFolder & ":" & createTexte
    End Function
End Class
