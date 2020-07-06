Imports CI.Clinica.Accounts.Clients.Folders.RVs

Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus

        ''' <summary>
        ''' Change the status of a folder from inactive to active or vice-versa.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class RVStatusApplier
            Inherits ManagerBase(Of RVStatusApplier)

            Private statuses As New Generic.List(Of RVStatus)

            Protected Sub New()
            End Sub

            Private Sub executeExtraActions(ByVal status As RVStatusChange)
                If status.showPayment Then
                    Dim myPaiement As Payment = openUniqueWindow(New Payment(), "Effectuer le(s) paiement(s) de " & status.rv.clientName)
                    If myPaiement.billsLoaded = False Then myPaiement.loading(status.rv.noClient, FacturationBox.DedicatedType.Client)
                    myPaiement.Show()
                End If

                'If it's an absence -> Open queue list
                If status.[new] < 3 And date1Infdate2(status.rv.dateHeure, Date.Today) = False Then openRestraintQueueList(status.rv.dateHeure, status.rv.noTRP)

                'If it's not a Rendez-vous
                If status.[new] <> 3 Then
                    DBLinker.getInstance.delDB("ListeAttente", "NoVisite", status.rv.noVisite, False, , , , False)
                    InternalUpdatesManager.getInstance.sendUpdate("QueueList()")
                End If

                'Look for future visites
                Dim actionOnNoRV As Byte = 0
                'If no R-V ask vérifie l'Action à entreprendre
                If PreferencesManager.getGeneralPreferences()("ActionOnNoRV").ToString <> "Ne rien faire" Then
                    Dim futureVisites As Generic.List(Of RendezVous) = RendezVousManager.getInstance.loadRendezVous(status.rv.noClient, Date.Today.AddDays(1), , False, status.rv.noFolder)
                    If futureVisites.Count = 0 Then
                        If (PreferencesManager.getGeneralPreferences()("ActionOnNoRV").ToString.StartsWith("Toujours") Or (PreferencesManager.getGeneralPreferences()("ActionOnNoRV").ToString.EndsWith("en cours") And status.rv.dateHeure.Date.CompareTo(Date.Today.Date) = 0) Or (PreferencesManager.getGeneralPreferences()("ActionOnNoRV").ToString.EndsWith("futur") And date1Infdate2(status.rv.dateHeure, Date.Today, True) = True)) Then
                            Dim myMsgBox As New MsgBox1()
                            Select Case myMsgBox.Message("Il n'existe plus de rendez-vous futur." & vbCrLf & "Que désirez-vous faire ?", "Confirmation", 3, "Prendre un nouveau rendez-vous", "Fermer le dossier", "Ne rien faire", False)
                                Case 2
                                    actionOnNoRV = 2

                                Case 1
                                    actionOnNoRV = 1
                            End Select
                        End If
                        If PreferencesManager.getGeneralPreferences()("ActionOnNoRV").ToString.StartsWith("Prendre") Then
                            actionOnNoRV = 1
                        ElseIf PreferencesManager.getGeneralPreferences()("ActionOnNoRV").ToString.StartsWith("Fermer") Then
                            actionOnNoRV = 2
                        End If
                    End If
                End If

                If actionOnNoRV = 1 Then
                    openNewRV(status.rv.noClient, status.rv.noTRP)
                ElseIf actionOnNoRV = 2 Then
                    ClientFolder.changeStatus(FoldersStatus.FolderPossibleStatuses.Active, FoldersStatus.FolderPossibleStatuses.Inactive, status.rv.noClient, status.rv.noFolder)
                End If
            End Sub

            Public Sub changeStatus(ByVal status As RVStatusChange)
                'Droit & Accès
                If currentDroitAcces(58) = False Then
                    'Message & Exit
                    Throw New UserRightException("Vous n'avez pas le droit de modifier le statut d'un rendez-vous." & vbCrLf & "Merci!")
                End If

                'Vérifie et barre le dossier
                If lockSecteur("ClientRV-ChangeStatus-" & status.rv.noClient & "-" & status.rv.noFolder & "-" & status.rv.noVisite, True, "Modification du statut d'un rendez-vous", False) = False Then
                    Throw New UserAlreadyUsingException("Le rendez-vous #" & status.rv.noVisite & " est déjà en cours de modification par un utilisateur")
                End If

                Dim selfOpened As Boolean = False
                Dim locks As New Generic.List(Of String)
                locks.Add("ClientRV-ChangeStatus-" & status.rv.noClient & "-" & status.rv.noFolder & "-" & status.rv.noVisite)
                Try
                    If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

                    'Load data
                    status.extraInfos = New RVStatusChangeExtraInfos(status)

                    'Ensure status wasn't already change (this can happen on a long desynchronization)
                    If status.extraInfos.rendezVousStatus = status.[new] Then Throw New RVStatusException("Le changement de statut est impossible, car le rendez-vous est déjà à ce statut." & vbCrLf & vbCrLf & "Veuillez rafraîchir le secteur où se trouve le rendez-vous en le fermant et en le rouvrant (l'une des méthodes).")

                    'Ensure the payment = 0 on an existing bill
                    If status.rv.noFacture <> 0 Then
                        Dim myFacture As New Bill(status.rv.noFacture)
                        status.bill = myFacture
                        If myFacture.getBillPaymentTotal > 0 Then
                            Throw New RVStatusException("Impossible de changer le statut : Un paiement a déjà été pris")
                        End If
                    End If

                    'Bloque le secteur de facturation de la facture (client, et potentiellement KP)
                    If status.[new] = 4 OrElse status.[new] = 2 OrElse status.old = 4 OrElse status.old = 2 Then
                        If lockSecteur("ClientFacturation-" & status.rv.noClient & "-", True, "Facturation du client #" & status.rv.noClient, False) = False Then
                            Throw New RVStatusException("Facturation du client déjà en cours de modification")
                        End If
                        Dim billNoKP As Integer = status.rv.getFolderCode().getNoKP(status.rv.period, status.rv.evaluation)
                        If billNoKP > 0 Then
                            If lockSecteur("KPFacturation-" & billNoKP & "-", True, "Facturation de la P/O clé #" & billNoKP, False) = False Then
                                lockSecteur("ClientFacturation-" & status.rv.noClient & "-", False, "Facturation du client #" & status.rv.noClient)
                                Throw New RVStatusException("Facturation de la personne/organisme clé déjà en cours de modification")
                            Else
                                locks.Add("KPFacturation-" & billNoKP & "-")
                            End If
                        End If

                        locks.Add("ClientFacturation-" & status.rv.noClient & "-")
                    End If

                    'Get classes
                    Dim firstStatus As RVStatus = getStatusClass(status.old)
                    Dim secondStatus As RVStatus = getStatusClass(status.[new])

                    'Verify new status conditions
                    secondStatus.verifyChange(status)


                    askForTRPToTransfer(status.rv.noClient, status.rv.noFolder, status.extraInfos.noTRPTraitant, status.extraInfos.noTRPToTransfer, New String() {"Ouvrir le compte", "M'envoyer une alerte"})

                    'Change RV status
                    firstStatus.undoStatus(status)
                    secondStatus.changeToStatus(status)

                    'Add log to mainwin
                    myMainWin.StatusText = "Le rendez-vous du client " & status.rv.clientName & " (" & status.rv.noClient & ") au dossier #" & status.rv.noFolder & " le " & DateFormat.getTextDate(status.rv.dateHeure) & " " & DateFormat.getTextDate(status.rv.dateHeure, DateFormat.TextDateOptions.ShortTime) & " est passé du statut """ & firstStatus.ToString & """ à """ & secondStatus.ToString & """"

                    'Send update
                    updateVisites(status.rv.noClient, status.rv.noFolder, status.rv.dateHeure, Me, status.showPayment, status.rv.noTRP)

                    'Unlock locks
                    For Each curLock As String In locks
                        lockSecteur(curLock, False, , False)
                    Next

                    'Do extra actions (as showing payment window)
                    executeExtraActions(status)

                Catch ex As Exception
                    For Each curLock As String In locks
                        lockSecteur(curLock, False, , False)
                    Next

                    Throw ex
                Finally
                    If selfOpened = True Then DBLinker.getInstance().dbConnected = False

                End Try
            End Sub
        End Class
    End Class
End Namespace
