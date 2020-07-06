Namespace Accounts.Clients.Folders
    Partial Public Class RVs

        Private Class RendezVousAcceptor

            Private Shared _lastFolderCode As Folders.Codifications.FolderCode

            Public Shared ReadOnly Property lastFolderCode() As Folders.Codifications.FolderCode
                Get
                    Return _lastFolderCode
                End Get
            End Property

            Public Shared Function isAccepted(ByVal vNoClient As Integer, ByVal vNoFolder As Integer, ByVal vDate As Date, ByVal vNoTRP As Integer, ByVal vFrequence As Integer, ByVal noTRPFrom As Integer, ByVal curNoVisite As Integer) As Boolean
                'Détermine les bornes de la fréquence pour les visites
                vFrequence += 1
                Dim FirstWeekDay, nextFirst As Date
                If vFrequence < 8 Then
                    FirstWeekDay = vDate.AddDays(vDate.DayOfWeek * -1)
                    nextFirst = FirstWeekDay.AddDays(7)
                ElseIf vFrequence = 8 Then
                    FirstWeekDay = vDate.AddDays(vDate.DayOfWeek * -1 - 7)
                    nextFirst = FirstWeekDay.AddDays(21)
                ElseIf vFrequence = 9 Then
                    FirstWeekDay = vDate.AddDays(vDate.Day * -1 + 1)
                    nextFirst = FirstWeekDay.AddMonths(1).AddDays(-1)
                End If

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''                                                                                                                                                                                                   0-                      1-                2-                3-                 4-         5-             6-                     7-
                Dim dataNeeded(,) As String = DBLinker.getInstance.readDB("WITH IF1 (NbVisiteHavingCAR,OldiestCAR) AS (SELECT SUM(NbVisiteHavingCAR) as NbVisiteHavingCAR, MIN(OldiestCAR) as OldiestCAR FROM InfoFolders  WHERE InfoFolders.NoClient=" & vNoClient & ") SELECT TOP 1 IF1.NbVisiteHavingCAR,IF1.OldiestCAR, IF2.[StatutOuvert],IF2.NoTRPTraitant,IF2.NoCodeUnique,IF2.NoCodeUser,IF2.NoCodeDate,(SELECT Count([NoVisite]) From InfoVisites WHERE InfoVisites.NoVisite<>" & curNoVisite & " AND (((InfoVisites.DateHeure)>='" & FirstWeekDay.Year & "/" & FirstWeekDay.Month & "/" & FirstWeekDay.Day & "' And (InfoVisites.DateHeure)<'" & nextFirst.Year & "/" & nextFirst.Month & "/" & nextFirst.Day & "') AND ((InfoVisites.NoFolder)=" & vNoFolder & "))) AS NbVisiteFreq  FROM InfoFolders AS IF2,IF1 WHERE IF2.NoFolder=" & vNoFolder & ";")
                If dataNeeded Is Nothing Then
                    MessageBox.Show("Des données nécessaires à l'ajout du rendez-vous sont manquantes. Le problème en cours de résolution. Veuillez réessayer s'il vous plait.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    addErrorLog(New Exception("VDate=" & DateFormat.getTextDate(vDate) & vbCrLf & "VNoFolder=" & vNoFolder))
                    Return False
                End If

                'Vérifie si la fréquence a été dépassé et pose une question si tel est le cas
                If PreferencesManager.getGeneralPreferences()("VerifyFrequenceForNewRV") = True AndAlso vFrequence <> 0 Then
                    If vFrequence < 8 Then
                        If dataNeeded(7, 0) >= vFrequence Then If MessageBox.Show("La fréquence de ce dossier indique " & vFrequence & " fois par semaine." & vbCrLf & "Doit-on ajouter un rendez-vous même si cela dépasse la fréquence ?", "Fréquence d'un dossier", MessageBoxButtons.YesNo) = DialogResult.No Then Return False
                    ElseIf vFrequence = 8 Then
                        If dataNeeded(7, 0) >= 1 Then If MessageBox.Show("La fréquence de ce dossier indique 1 fois par 2 semaines." & vbCrLf & "Doit-on ajouter un rendez-vous même si cela dépasse la fréquence ?", "Fréquence d'un dossier", MessageBoxButtons.YesNo) = DialogResult.No Then Return False
                    ElseIf vFrequence = 9 Then
                        If dataNeeded(7, 0) >= 1 Then If MessageBox.Show("La fréquence de ce dossier indique 1 fois par mois." & vbCrLf & "Doit-on ajouter un rendez-vous même si cela dépasse la fréquence ?", "Fréquence d'un dossier", MessageBoxButtons.YesNo) = DialogResult.No Then Return False
                    End If
                End If

                'Vérifie si le nombre de visite ayant un CAR a été dépassé
                If Not dataNeeded Is Nothing AndAlso dataNeeded.Length <> 0 AndAlso PreferencesManager.getGeneralPreferences()("NbVisiteCAR") <> "" AndAlso PreferencesManager.getGeneralPreferences()("NbDayCAR") <> "" Then
                    If dataNeeded(0, 0) >= PreferencesManager.getGeneralPreferences()("NbVisiteCAR") And PreferencesManager.getGeneralPreferences()("NbVisiteCAR") > -1 Then
                        'REM Vérifier le nombre NbForCar(0,0) : M'a affiché 0 alors qu'il devait être à 1
                        If MessageBox.Show("Le nombre de rendez-vous ayant un compte à recevoir a été atteint (" & dataNeeded(0, 0) & "/" & PreferencesManager.getGeneralPreferences()("NbVisiteCAR") & ") pour ce client." & vbCrLf & "Voulez-vous quand même ajouter ce rendez-vous ?", "Nombre de compte à recevoir atteint", MessageBoxButtons.YesNo) = DialogResult.No Then Return False
                    End If

                    If dataNeeded(1, 0) <> "" And PreferencesManager.getGeneralPreferences()("NbDayCAR") > -1 Then
                        If date1Infdate2(CDate(dataNeeded(1, 0)).AddDays(PreferencesManager.getGeneralPreferences()("NbDayCAR")), Date.Now, True) Then
                            If MessageBox.Show("Le compte à recevoir le plus ancien a dépassé le nombre de jour accepté pour un client (" & PreferencesManager.getGeneralPreferences()("NbDayCAR") & ")." & vbCrLf & "Voulez-vous quand même ajouter ce rendez-vous ?", "Nombre de jour maximal pour un compte client", MessageBoxButtons.YesNo) = DialogResult.No Then Return False
                        End If
                    End If
                End If

                'Vérifie le statut du dossier
                If CBool(dataNeeded(2, 0)) = False Then
                    If PreferencesManager.getGeneralPreferences()("ActiveFolderAutoOnRVStatusChange") = False AndAlso MessageBox.Show("Le dossier (#" & vNoFolder & ") du rendez-vous est inactif. Voulez-vous le réactiver ? (Sinon, impossible d'ajouter de ce rendez-vous)", "Dossier inactif", MessageBoxButtons.YesNo) = DialogResult.No Then
                        'REM tran DBLinker.GetInstance.RollbackTransaction()
                        Return False
                    Else
                        Accounts.Clients.Folders.ClientFolder.changeStatus(Accounts.Clients.Folders.FoldersStatus.FolderPossibleStatuses.Inactive, Accounts.Clients.Folders.FoldersStatus.FolderPossibleStatuses.Active, vNoClient, vNoFolder)
                    End If
                End If

                'Confirme que le rendez-vous sera collé au bon agenda
                Dim myCode As Accounts.Clients.Folders.Codifications.FolderCode = Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getItemable(Integer.Parse(dataNeeded(4, 0)), If(dataNeeded(5, 0) = "", 0, Integer.Parse(dataNeeded(5, 0))), Date.Parse(dataNeeded(6, 0)))
                _lastFolderCode = myCode
                If PreferencesManager.getGeneralPreferences()("ConfirmWhenPastingRVIntoAgendaWhichNotTrpTraitant") = True Then
                    Dim curTRP As User = UsersManager.getInstance.getUser(vNoTRP)
                    If noTRPFrom <> 0 AndAlso curTRP.notConfirmRVOnPasteOfDTRP = True AndAlso myCode.notConfirmRVOnPasteOfDTRP = True AndAlso vNoTRP <> noTRPFrom Then
                        If MessageBox.Show("Êtes-vous sûr de vouloir ajouter un rendez-vous dans l'agenda de " & UsersManager.getInstance.getUser(vNoTRP).toString & " provenant de l'agenda du thérapeute " & UsersManager.getInstance.getUser(noTRPFrom).toString & " ?", "Confirmation d'un nouveau rendez-vous", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                            Return False
                        End If
                    ElseIf dataNeeded(3, 0) <> vNoTRP AndAlso myCode.notConfirmRVOnPasteOfDTRP = False Then
                        If MessageBox.Show("Êtes-vous sûr de vouloir ajouter un rendez-vous dans l'agenda de " & UsersManager.getInstance.getUser(vNoTRP).toString & " d'un dossier ayant comme thérapeute traitant " & UsersManager.getInstance.getUser(dataNeeded(3, 0)).toString & " ?", "Confirmation d'un nouveau rendez-vous", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                            Return False
                        End If
                    End If
                End If

                Return True
            End Function
        End Class

    End Class
End Namespace
