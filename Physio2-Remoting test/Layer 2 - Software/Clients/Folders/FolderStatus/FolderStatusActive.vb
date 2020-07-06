Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Namespace Accounts.Clients.Folders


    Partial Public Class FoldersStatus
        Private Class FolderStatusActive
            Inherits FolderStatus

            Private Sub removeFolderTexts(ByVal noFolder As Integer, ByVal updateTRPAlerts As Generic.List(Of Integer))
                'Remove FolderTextes Where WhenToBeCreated Is OnFolderClosing
                Dim noFTTSelect As String = "(SELECT NoFolderTexte FROM FolderTextes INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FolderTextes.NoFolderTexteType Where FTT.WhenToBeCreated=" & FolderTextType.WhenToBeCreate.OnFolderClosing & " AND NoFolder=" & noFolder & ")"
                Dim noAlertsToDel(,) As String = DBLinker.getInstance.readDB("FolderTexteAlerts INNER JOIN UsersAlerts ON UsersAlerts.NoUserAlert = FolderTexteAlerts.NoUserAlert", "FolderTexteAlerts.NoUserAlert,NoUser", "WHERE NoFolderTexte IN " & noFTTSelect)
                DBLinker.getInstance.delDB("FolderTexteAlerts", "NoFolderTexte", noFTTSelect, False, , , , , " IN ")
                Dim NbUADeleted, nbFTDeleted As Integer
                If noAlertsToDel IsNot Nothing AndAlso noAlertsToDel.Length <> 0 Then
                    Dim noATD(noAlertsToDel.GetUpperBound(1)) As String
                    For i As Integer = 0 To noAlertsToDel.GetUpperBound(1)
                        noATD(i) = noAlertsToDel(0, i)
                        If updateTRPAlerts.IndexOf(noAlertsToDel(1, i)) = -1 Then updateTRPAlerts.Add(noAlertsToDel(1, i))
                    Next
                    DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", "(" & String.Join(",", noATD) & ")", False, , , , , " IN ", , NbUADeleted)
                End If
                DBLinker.getInstance.delDB("FolderTextes", "NoFolderTexte", noFTTSelect, False, , , , , " IN ", , nbFTDeleted)
            End Sub

            Private Sub recreateMultiplesForNbDays(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal updateTRPAlerts As Generic.List(Of Integer), ByRef noFTTAlreadyRecreated As Generic.List(Of Integer))
                Dim nbFTCreated As Integer = 0
                'Recreate multiple for TypeForMultiple is NbDays
                Dim noFTTSelect As String = "(SELECT MAX(NoFolderTexte) FROM FolderTextes INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FolderTextes.NoFolderTexteType Where FTT.Multiple=1 AND FTT.WhenToBeStopped=" & FolderTextType.WhenToBeStop.OnFolderClosing & " AND FTT.TypeForMultiple=" & FolderTextType.TypeMultiple.NbDaysX & " AND NoFolder=" & noFolder & " GROUP BY FTT.NoFolderTexteType)"
                Dim ftOfTFM_NbDays As DataSet = DBLinker.getInstance.readDBForGrid("SELECT FolderTextes.*,InfoFolders.NoTRPTraitant FROM FolderTextes INNER JOIN InfoFolders ON InfoFolders.NoFolder=FolderTextes.NoFolder INNER JOIN FolderTexteTypes AS FTT1 ON FTT1.NoFolderTexteType=FolderTextes.NoFolderTexteType WHERE NoFolderTexte IN " & noFTTSelect)
                If ftOfTFM_NbDays IsNot Nothing AndAlso ftOfTFM_NbDays.Tables(0).Rows.Count <> 0 Then
                    With ftOfTFM_NbDays.Tables(0).Rows
                        For i As Integer = 0 To .Count - 1
                            Dim curFT As New FolderText(.Item(i))
                            Dim curFTT As FolderTextType = curFT.getFolderTexteType
                            Dim noTRP As Integer = .Item(i)("noTRPTraitant")
                            FolderText.add(curFT.noFolderTexteType, noClient, noFolder, curFTT.textTitle & " " & (curFT.noMultiple + 1), curFT.dateStarted.AddDays(curFTT.nbDaysMultiple), curFT.noMultiple + 1, noTRP)
                            noFTTAlreadyRecreated.Add(curFTT.noFolderTexteType)
                            If curFTT.showAlert AndAlso updateTRPAlerts.IndexOf(noTRP) = -1 Then updateTRPAlerts.Add(noTRP)
                            nbFTCreated += 1
                        Next i
                    End With
                End If
            End Sub

            Private Sub recreateMultipleForTextEnded(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal updateTRPAlerts As Generic.List(Of Integer), ByRef noFTTAlreadyRecreated As Generic.List(Of Integer))

                'Recreate multiple for TypeForMultiple is OnTexteEnded
                Dim noFTTSelect As String = "(SELECT MAX(NoFolderTexte) FROM FolderTextes INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FolderTextes.NoFolderTexteType Where FTT.Multiple=1 AND FTT.WhenToBeStopped=" & FolderTextType.WhenToBeStop.OnFolderClosing & " AND FTT.TypeForMultiple=" & FolderTextType.TypeMultiple.OnTexteEnded & " AND NoFolder=" & noFolder & " GROUP BY FTT.NoFolderTexteType)"
                Dim ftOfTFM_OnEnded As DataSet = DBLinker.getInstance.readDBForGrid("SELECT FolderTextes.*,InfoFolders.NoTRPTraitant FROM FolderTextes INNER JOIN InfoFolders ON InfoFolders.NoFolder=FolderTextes.NoFolder INNER JOIN FolderTexteTypes AS FTT1 ON FTT1.NoFolderTexteType=FolderTextes.NoFolderTexteType WHERE NoFolderTexte IN " & noFTTSelect)
                If ftOfTFM_OnEnded IsNot Nothing AndAlso ftOfTFM_OnEnded.Tables(0).Rows.Count <> 0 Then
                    With ftOfTFM_OnEnded.Tables(0).Rows
                        For i As Integer = 0 To .Count - 1
                            Dim curFT As New FolderText(.Item(i))
                            If curFT.dateFinished.Equals(LIMIT_DATE) = False Then
                                Dim curFTT As FolderTextType = curFT.getFolderTexteType
                                Dim noTRP As Integer = .Item(i)("noTRPTraitant")
                                FolderText.add(curFT.noFolderTexteType, noClient, noFolder, curFTT.textTitle & " " & (curFT.noMultiple + 1), curFT.dateFinished, curFT.noMultiple + 1, noTRP)
                                If curFTT.showAlert AndAlso updateTRPAlerts.IndexOf(noTRP) = -1 Then updateTRPAlerts.Add(noTRP)
                            End If
                            noFTTAlreadyRecreated.Add(curFT.noFolderTexteType)
                        Next i
                    End With
                End If
            End Sub

            Private Sub recreateFirstText(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal updateTRPAlerts As Generic.List(Of Integer), ByVal noFTTAlreadyRecreated As Generic.List(Of Integer))
                'Recreate first text
                Dim dataRequired(,) As String = DBLinker.getInstance.readDB("InfoFolders INNER JOIN InfoClients ON InfoClients.NoClient=InfoFolders.NoClient", _
                                                                            "NoCodeUnique," & _
                                                                            "(SELECT DateHeureCreation FROM StatFolders WHERE StatFolders.NoFolder=InfoFolders.NoFolder AND NoAction=13) AS FolderCreation," & _
                                                                            "InfoClients.Nom + ',' + InfoClients.Prenom as ClientName," & _
                                                                            "NoTRPTraitant," & _
                                                                            "NoCodeUser," & _
                                                                            "NoCodeDate ", _
                                                                            "WHERE InfoFolders.NoFolder = " & noFolder)
                Dim curFTTs As Generic.List(Of FolderTextType) = FolderCodesManager.getInstance.getItemable(Integer.Parse(dataRequired(0, 0)), If(dataRequired(4, 0) = "", 0, Integer.Parse(dataRequired(4, 0))), Date.Parse(dataRequired(5, 0))).folderTexteTypes()
                For Each curFTT As FolderTextType In curFTTs
                    If noFTTAlreadyRecreated.IndexOf(curFTT.noFolderTexteType) = -1 AndAlso curFTT.multiple AndAlso curFTT.whenToBeStopped = FolderTextType.WhenToBeStop.OnFolderClosing Then
                        Dim multiple As String = " 1"

                        If curFTT.isActive AndAlso curFTT.whenToBeCreated = FolderTextType.WhenToBeCreate.OnDayX Or curFTT.whenToBeCreated = FolderTextType.WhenToBeCreate.OnFolderCreation Then
                            FolderText.add(curFTT.noFolderTexteType, noClient, noFolder, curFTT.textTitle & multiple, CDate(dataRequired(1, 0)).AddDays(curFTT.nbDaysX), 1, dataRequired(3, 0), dataRequired(2, 0))
                            If curFTT.showAlert AndAlso updateTRPAlerts.IndexOf(dataRequired(3, 0)) = -1 Then updateTRPAlerts.Add(dataRequired(3, 0))
                        ElseIf curFTT.isActive AndAlso curFTT.whenToBeCreated = FolderTextType.WhenToBeCreate.OnPresenceX Then
                            Dim rvDate(,) As String = DBLinker.getInstance.readDB("SELECT DateHeure,NoTRP, (SELECT TexteTitle FROM FolderTextes WHERE NoFolder=" & noFolder & " AND TexteTitle='" & curFTT.textTitle.Replace("'", "''") & multiple & "') AS AlreadyExists FROM (SELECT DateHeure,NoTRP,ROW_NUMBER() OVER(ORDER BY NoVisite) AS No FROM InfoVisites WHERE NoStatut=4 AND InfoVisites.NoFolder=" & noFolder & ") AS T WHERE T.No=" & curFTT.nbPresencesX)
                            If rvDate IsNot Nothing AndAlso rvDate.Length <> 0 AndAlso rvDate(2, 0) = "" Then
                                Dim noTRP As Integer = dataRequired(3, 0)
                                FolderText.add(curFTT.noFolderTexteType, noClient, noFolder, curFTT.textTitle & multiple, CDate(rvDate(0, 0)).AddDays(curFTT.nbDaysX), 1, noTRP, dataRequired(2, 0))
                                If curFTT.showAlert AndAlso updateTRPAlerts.IndexOf(noTRP) = -1 Then updateTRPAlerts.Add(noTRP)
                            End If
                        End If
                    End If
                Next
            End Sub

            Public Overrides Sub changeToStatus(ByVal status As FolderStatusChange)
                Dim selfOpened As Boolean = False
                If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

                Dim updateTRPAlerts As New Generic.List(Of Integer)
                Dim noFTTAlreadyRecreated As New Generic.List(Of Integer)

                removeFolderTexts(status.noFolder, updateTRPAlerts)
                recreateMultiplesForNbDays(status.noClient, status.noFolder, updateTRPAlerts, noFTTAlreadyRecreated)
                recreateMultipleForTextEnded(status.noClient, status.noFolder, updateTRPAlerts, noFTTAlreadyRecreated)
                recreateFirstText(status.noClient, status.noFolder, updateTRPAlerts, noFTTAlreadyRecreated)

                'Send updates of foldertextes modifications (only alerts, because textes are updated by the applier)
                For i As Integer = 0 To updateTRPAlerts.Count - 1
                    AlertsManager.sendUpdate(updateTRPAlerts(i))
                Next

                DBLinker.getInstance.updateDB("InfoFolders", "StatutOuvert=1", "NoFolder", status.noFolder, False)
                DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient, Comments", UserActions.Folder_Activate & "," & status.noFolder & "," & status.noClient & ",'" & status.comments.Replace("'", "''") & "'", Date.Now)
                removeFutureRVs(status.noClient, status.noFolder, Date.Today)

                If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            End Sub
        End Class
    End Class


End Namespace
