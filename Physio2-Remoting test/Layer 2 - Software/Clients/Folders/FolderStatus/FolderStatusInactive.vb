Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Namespace Accounts.Clients.Folders

    Partial Public Class FoldersStatus

        Private Class FolderStatusInactive
            Inherits FolderStatus

            Private Sub removeFolderTexts(ByVal noFolder As Integer, ByRef updateTRPAlerts As Boolean)
                'Remove FolderTextes Where WhenToBeStopped Is OnFolderClosing
                Dim noFTTSelect As String = "(SELECT NoFolderTexte FROM FolderTextes INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FolderTextes.NoFolderTexteType Where FTT.Multiple=1 AND FTT.WhenToBeStopped=" & FolderTextType.WhenToBeStop.OnFolderClosing & " AND NoFolder=" & noFolder & " AND ((FTT.TypeForMultiple=" & FolderTextType.TypeMultiple.OnTexteEnded & " AND DateFinished IS NULL AND IsTexte=0) OR (FTT.TypeForMultiple<>" & FolderTextType.TypeMultiple.OnTexteEnded & " AND DateStarted > '" & DateFormat.getTextDate(Date.Today) & "')))"
                Dim noAlertsToDel(,) As String = DBLinker.getInstance.readDB("FolderTexteAlerts INNER JOIN UsersAlerts ON UsersAlerts.NoUserAlert = FolderTexteAlerts.NoUserAlert", "FolderTexteAlerts.NoUserAlert,NoUser", "WHERE NoFolderTexte IN " & noFTTSelect)
                DBLinker.getInstance.delDB("FolderTexteAlerts", "NoFolderTexte", noFTTSelect, False, , , , , " IN ")
                Dim NbUADeleted, nbFTDeleted As Integer
                If noAlertsToDel IsNot Nothing AndAlso noAlertsToDel.Length <> 0 Then
                    Dim noATD(noAlertsToDel.GetUpperBound(1)) As String
                    For i As Integer = 0 To noAlertsToDel.GetUpperBound(1)
                        noATD(i) = noAlertsToDel(0, i)
                    Next
                    DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", "(" & String.Join(",", noATD) & ")", False, , , , , " IN ", , NbUADeleted)
                End If
                DBLinker.getInstance.delDB("FolderTextes", "NoFolderTexte", noFTTSelect, False, , , , , " IN ", , nbFTDeleted)
                updateTRPAlerts = updateTRPAlerts OrElse NbUADeleted <> 0
            End Sub


            Private Sub createFolderTexts(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal curCode As FolderCode, ByVal noMainTRP As Integer, ByVal clientName As String, ByRef updateTRPAlerts As Boolean, ByVal endingDate As Date)
                'Création des FolderTextes ayant WhenToBeCreated à OnFolderClosing
                Dim curFTTs As Generic.List(Of FolderTextType) = curCode.folderTexteTypes()
                Dim countFTs As String(,) = FolderTextTypesManager.getInstance.countFolderTexts(noFolder)
                Dim nbAdded As Integer = 0
                Dim ftScript As New System.Text.StringBuilder()


                For Each curFTT As FolderTextType In curFTTs
                    Dim wtbc As Boolean = curFTT.whenToBeCreated = FolderTextType.WhenToBeCreate.OnFolderClosing
                    Dim multiple As String = ""
                    If curFTT.isActive AndAlso wtbc Then 'Création d'un FolderTexte selon WhenToBeCreated à OnPresenceX
                        nbAdded += 1

                        If curFTT.multiple Then multiple = " 1"
                        ftScript.AppendLine(FolderText.add(curFTT.noFolderTexteType, noClient, noFolder, curFTT.textTitle & multiple, endingDate, 1, noMainTRP, clientName, False))

                        updateTRPAlerts = updateTRPAlerts OrElse curFTT.showAlert
                    End If
                Next
                If nbAdded <> 0 Then DBLinker.executeSQLScript(ftScript.ToString)
            End Sub

            Public Overrides Sub changeToStatus(ByVal status As FolderStatusChange)

                Dim selfOpened As Boolean = False
                If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

                'TODO : This could be bad !! Shall ask + add preference !!
                'Suppression des R-V étant à leur statut initial (Rendez-vous-3)
                Dim nextDay As Date = Date.Today.AddDays(1)
                Dim myOldVisites(,) As String = DBLinker.getInstance.readDB("InfoVisites", "NoVisite, DateHeure, NoTRP", "WHERE (NoStatut=3) AND (NoFolder=" & status.noFolder & ") AND (DateHeure<'" & nextDay.Year & "/" & nextDay.Month & "/" & nextDay.Day & "');")
                If Not myOldVisites Is Nothing AndAlso myOldVisites.Length <> 0 Then
                    If currentDroitAcces(75) = False Then
                        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
                        'Message & Exit
                        Throw New UserRightException("Vous n'avez pas le droit de supprimer les rendez-vous." & vbCrLf & "Merci!")
                    End If

                    Dim rvsToDelete As New Generic.List(Of String)
                    For i As Integer = 0 To myOldVisites.GetUpperBound(1)
                        rvsToDelete.Add(myOldVisites(0, i))
                    Next i
                    DBLinker.getInstance.delDB("InfoVisites", "NoVisite", "(" & String.Join(",", rvsToDelete.ToArray()) & ")", False, , , , , " IN")
                    For i As Integer = 0 To myOldVisites.GetUpperBound(1)
                        updateVisites(status.noClient, status.noFolder, myOldVisites(1, i), , False, myOldVisites(2, i))
                    Next i
                End If

                'Choix de la date de désactivation
                Dim closingDate As Date = status.date
                If closingDate = LIMIT_DATE Then
                    'Ask date if no one passed with new status
                    closingDate = Date.Now
                    Dim myDateChoice As New DateChoice()
                    Dim dateChosen As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Today.Year, , , , , , True, firstUsageDate, , , , , , , , , , Date.Today)
                    If dateChosen.Count <> 0 Then
                        Dim newDate As Date = dateChosen(0)
                        'Si ce n'est pas aujourd'hui (pour garder l'heure de l'action), changer la date
                        If newDate.Date.Equals(Date.Today.Date) = False Then closingDate = newDate
                    End If
                End If

                'Create/Delete FolderTexts
                Dim dataRequired(,) As String = DBLinker.getInstance.readDB("InfoFolders", "NoCodeUnique,NoTRPTraitant,(SELECT Nom + ',' + Prenom FROM InfoClients AS IC WHERE IC.NoClient=InfoFolders.NoClient) AS ClientName, NoCodeUser,NoCodeDate", "WHERE InfoFolders.NoFolder = " & status.noFolder)
                Dim curCode As FolderCode = FolderCodesManager.getInstance.getItemable(Integer.Parse(dataRequired(0, 0)), If(dataRequired(3, 0) = "", 0, Integer.Parse(dataRequired(3, 0))), Date.Parse(dataRequired(4, 0)))
                Dim updateTRPAlerts As Boolean = False
                createFolderTexts(status.noClient, status.noFolder, curCode, dataRequired(1, 0), dataRequired(2, 0), updateTRPAlerts, closingDate)
                removeFolderTexts(status.noFolder, updateTRPAlerts)

                'Send updates for FolderTextes modifications (only alerts, because Textes are updated by the applier)
                If updateTRPAlerts Then AlertsManager.sendUpdate(dataRequired(1, 0))

                DBLinker.getInstance.updateDB("InfoFolders", "StatutOuvert=0", "NoFolder", status.noFolder, False)
                DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient, Comments", UserActions.Folder_Desactivate & "," & status.noFolder & "," & status.noClient & ",'" & status.comments.Replace("'", "''") & "'", closingDate)

                removeFutureRVs(status.noClient, status.noFolder, closingDate)

                If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            End Sub
        End Class

    End Class


End Namespace
