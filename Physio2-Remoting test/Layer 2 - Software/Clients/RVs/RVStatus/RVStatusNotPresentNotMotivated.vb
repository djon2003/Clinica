Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus


        Private Class RVStatusNotPresentNotMotivated
            Inherits RVStatus

            Private myNewRaison As String = ""
            Private lockedFolder As Boolean

            Public Overrides Sub changeToStatus(ByVal status As RVStatusChange)
                'REM_CODES
                Dim myAmount As Double
                Dim PourcentClient, pourcentKP As Double


                Dim curFolderCode As FolderCode = status.rv.getFolderCode()
                Dim countPresences As Integer = status.extraInfos.nbPresences
                Dim lastRVDate As Date = IIf(status.extraInfos.lastRVDate.Equals(LIMIT_DATE), New Date(1900, 1, 1), status.extraInfos.lastRVDate)
                If date1Infdate2(lastRVDate, status.rv.dateHeure) And status.[new] = 4 Then lastRVDate = status.rv.dateHeure

                Dim myFacture As New Bill
                myFacture.noFacture = 0
                If status.bill IsNot Nothing Then myFacture = status.bill


                Dim done As Boolean = True
                Try


                    'Modifications dues au nouveau statut
                    Dim parNoKP As Integer = 0

                    myAmount = curFolderCode.getPrice(status.rv.period, status.rv.evaluation)
                    myAmount *= curFolderCode.getAbsence(status.rv.period, status.rv.evaluation) / 100
                    parNoKP = curFolderCode.getNoKP(status.rv.period, status.rv.evaluation)

                    PourcentClient = 100
                    pourcentKP = 0
                    If myAmount > 0 Then
                        myFacture.noFacture = createFacturation(status.rv.noClient, myAmount, status.rv.service & ": Absence non motivée", status.rv.dateHeure, status.rv.noFolder, status.rv.noVisite, , , , , , , , , status.rv.noClient, parNoKP, , PourcentClient, pourcentKP)

                        If myFacture.noFacture = 0 Then
                            done = False
                            If status.extraInfos.noFTDefault <> 0 Then lockFolder(status.rv.noClient, status.rv.noFolder, False)
                            GoTo SkipModif
                        End If

                        myFacture.saveDescription()
                    End If

                    'Ajout de la raison d'absence dans les notes du dossier
                    If lockedFolder AndAlso status.extraInfos.noFTDefault <> 0 Then
                        Dim defaultText As String = status.extraInfos.textFTDefault.Replace(vbCrLf, "").Replace("'", "''") & _
                                                    IIf(status.extraInfos.textFTDefault = "", "", "<br><BR>") & _
                                                    "Inscription à " & DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.FullTime) & " d''une absence non-motivée datée le " & DateFormat.getTextDate(status.rv.dateHeure) & " à " & DateFormat.getTextDate(status.rv.dateHeure, DateFormat.TextDateOptions.ShortTime) & _
                                                    IIf(myNewRaison = "", "", " à cause de :<BR>" & myNewRaison.Replace("'", "''"))
                        DBLinker.getInstance.updateDB("FolderTextes", "Texte='" & defaultText & "'", "NoFolderTexte", status.extraInfos.noFTDefault, False)
                        lockFolder(status.rv.noClient, status.rv.noFolder, False)
                        InternalUpdatesManager.getInstance.sendUpdate("AccountsDossierTextBoxes(" & status.rv.noClient & "," & status.rv.noFolder & ")")
                    End If

SkipModif:

                    'M-A-J DB
                    done = done AndAlso DBLinker.getInstance.updateDB("InfoVisites", "NoStatut=" & status.[new] & ",NoFacture=" & IIf(myFacture.noFacture = 0, "null", myFacture.noFacture), "NoVisite", status.rv.noVisite, False)
                    done = done AndAlso DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite, NoRaison", status.[new] & "," & status.rv.noFolder & "," & status.rv.noClient & "," & status.rv.noVisite & "," & IIf(myNewRaison = "", "null", DBHelper.addItemToADBList("AbsencesRaisons", "Raison", myNewRaison, "NoRaison").ToString))

                Catch ex As Exception
                    addErrorLog(ex)
                    done = False
                End Try

                If done = False Then
                    MessageBox.Show("Une erreur est survenu lors de l'écriture à la base de données SQL. Veuillez réessayez de changer le statut.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    addErrorLog(New Exception("ChangeVisiteStatus(" & status.[new] & "," & status.rv.noClient & "," & status.rv.noFolder & "," & status.rv.noVisite & ") - OldStatus=" & status.old))

                    DBLinker.getInstance.dbConnected = False
                    DBLinker.getInstance.dbConnected = True

                    'REM tran DBLinker.GetInstance.RollbackTransaction()
                    Throw New RVStatusException(errorTxt)
                End If

                'REM tran         DBLinker.GetInstance.CommitTransaction()

                'REM_CODES
                If status.hasToEnsureFTDates Then DBLinker.getInstance.executestoredProcedure("dbo.ensureFTDatesOnPresence", New String() {status.rv.noFolder})

                If PreferencesManager.getGeneralPreferences("AlertTRPOnRVAbsence") = "" OrElse PreferencesManager.getGeneralPreferences("AlertTRPOnRVAbsence") = True Then
                    Dim newAlert As New AlertOfClientAccount(status.rv.noClient)
                    newAlert.message = "Le rendez-vous du " & DateFormat.getTextDate(status.rv.dateHeure) & " " & DateFormat.getTextDate(status.rv.dateHeure, DateFormat.TextDateOptions.ShortTime) & " au dossier #" & status.rv.noFolder & " du client " & status.rv.clientName & " est passé au statut absent non motivé"
                    newAlert.showingDate = Date.Today
                    newAlert.expiryDate = Date.Now.AddMinutes(15)
                    AlertsManager.getInstance.addAlert(status.rv.noTRP, newAlert)
                End If

                status.showPayment = status.rv.noFacture <> myFacture.noFacture
            End Sub

            Public Overrides Sub undoStatus(ByVal status As RVStatusChange)
                Dim tc As String = ""
                Dim done As Boolean = True
                Dim curFolderCode As FolderCode = status.rv.getFolderCode()
                Dim countPresences As Integer = status.extraInfos.nbPresences
                Dim lastRVDate As Date = IIf(status.extraInfos.lastRVDate.Equals(LIMIT_DATE), New Date(1900, 1, 1), status.extraInfos.lastRVDate)
                If date1Infdate2(lastRVDate, status.rv.dateHeure) And status.[new] = 4 Then lastRVDate = status.rv.dateHeure

                'REM tran  DBLinker.GetInstance.BeginTransaction()

                Dim myFacture As New Bill
                If status.bill IsNot Nothing Then myFacture = status.bill
                'Vérifications et modifications dues à l'ancien statut
                If status.[new] > 2 Then
                    tc = AgendaManager.getInstance.checkTimeConflict(status.rv.dateHeure, status.rv.period, status.rv.noTRP, , status.rv.noClient, curFolderCode.noUnique, , , , status.rv.noFolder)
                    If tc <> "" Then
                        'MessageBox.Show(TC, "Impossible de changer le statut")
                        'REM tran DBLinker.GetInstance.RollbackTransaction()
                        Throw New RVStatusException("Impossible de changer le statut : " & tc)
                    End If
                End If

                done = deleteFacturation(myFacture.noFacture)
                myFacture.noFacture = 0


                If done = False Then
                    'REM tran DBLinker.GetInstance.RollbackTransaction()
                    Throw New RVStatusException(errorTxt)
                End If
            End Sub

            Public Overrides Sub verifyChange(ByVal status As RVStatusChange)
                'Vérifications du nouveau status
                If status.extraInfos.noFTDefault <> 0 AndAlso lockFolder(status.rv.noClient, status.rv.noFolder, True) <> "" Then
                    Dim hasToAsk As Boolean = PreferencesManager.getGeneralPreferences()("AllowToSkipAbsenceReasonInsertionToText")
                    If hasToAsk AndAlso MessageBox.Show("Les textes du dossier sont présentement en cours d'utilisation. Désirez-vous quand même continuer ? (La raison ne sera pas ajoutée au texte par défaut)", "Impossible d'insérer la raison au texte par défaut", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        lockedFolder = False
                    Else
                        Throw New RVStatusException("Dossier client en cours d'utilisation")
                    End If
                Else
                    lockedFolder = True
                End If

                Dim myInputBoxPlus As New InputBoxPlus(True, , "AbsencesRaisons.Raison")
                myInputBoxPlus.firstLetterCapital = True
                myNewRaison = myInputBoxPlus.Prompt("Veuillez entrer la raison de l'absence :", "Absence - Non motivée")
                If myNewRaison = "" Then
                    If lockedFolder AndAlso status.extraInfos.noFTDefault <> 0 Then lockFolder(status.rv.noClient, status.rv.noFolder, False)
                    Throw New RVStatusException("Changement de statut annulé")
                End If
            End Sub

            Public Overrides Function toString() As String
                Return "Absent non-motivé"
            End Function
        End Class

    End Class
End Namespace

