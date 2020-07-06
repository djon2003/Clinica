Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus


        Private Class RVStatusPresent
            Inherits RVStatus

            Public Overrides Sub changeToStatus(ByVal status As RVStatusChange)
                'REM_CODES
                Dim myNewRaison As String = ""
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
                    PourcentClient = curFolderCode.getPourcentage(status.rv.period, status.rv.evaluation)
                    parNoKP = curFolderCode.getNoKP(status.rv.period, status.rv.evaluation)

                    pourcentKP = 100 - PourcentClient
                    myFacture.noFacture = createFacturation(status.rv.noClient, myAmount, status.rv.service, status.rv.dateHeure, status.rv.noFolder, status.rv.noVisite, , , , , , , , , status.rv.noClient, parNoKP, , PourcentClient, pourcentKP)

                    If myFacture.noFacture = 0 Then
                        done = False
                        GoTo SkipModif
                    End If

                    myFacture.saveDescription()
                    If curFolderCode.autoShowPayment = True And Bill.isPaymentsToDoByClient(status.rv.noClient) Then
                        status.showPayment = True
                    End If

                    'Création des FolderTextes si nécessaire
                    Dim curFTTs As Generic.List(Of FolderTextType) = curFolderCode.folderTexteTypes()
                    Dim countFTs As String(,) = FolderTextTypesManager.getInstance.countFolderTexts(status.rv.noFolder)
                    Dim nbAdded As Integer = 0
                    Dim updateTRPTraitantAlerts As Boolean = False
                    Dim ftScript As New System.Text.StringBuilder()
                    Dim didFTT As New Generic.List(Of Integer)
                    For Each curFTT As FolderTextType In curFTTs
                        Dim countFT As Integer = 0
                        For c As Integer = 0 To countFTs.GetUpperBound(1)
                            If countFTs(0, c) = curFTT.noFolderTexteType Then countFT = countFTs(1, c) : Exit For
                        Next
                        Dim wtbc As Boolean = curFTT.whenToBeCreated = FolderTextType.WhenToBeCreate.OnPresenceX AndAlso (curFTT.nbPresencesX - 1) = countPresences
                        Dim tfm As Boolean = curFTT.typeForMultiple = FolderTextType.TypeMultiple.NbPresencesX AndAlso curFTT.multiple AndAlso (countPresences - curFTT.nbPresencesX + 1) > 0 AndAlso ((countPresences - curFTT.nbPresencesX + 1) Mod curFTT.nbPresencesMultiple) = 0
                        Dim wtbs As Boolean = curFTT.whenToBeStopped <> FolderTextType.WhenToBeStop.OnMaxReached OrElse curFTT.nbMultipleEnding >= countFT 'This condition has to be true if NOT stopped
                        Dim multiple As String = ""
                        If curFTT.isActive AndAlso wtbc Then 'Création d'un FolderTexte selon WhenToBeCreated à OnPresenceX
                            nbAdded += 1
                            didFTT.Add(curFTT.noFolderTexteType)

                            If curFTT.multiple Then multiple = " 1"
                            ftScript.AppendLine(FolderText.add(curFTT.noFolderTexteType, status.rv.noClient, status.rv.noFolder, curFTT.textTitle & multiple, lastRVDate.AddDays(curFTT.nbDaysX), 1, status.extraInfos.noTRPTraitant, status.rv.clientName, False))

                            updateTRPTraitantAlerts = updateTRPTraitantAlerts OrElse curFTT.showAlert
                        End If
                        If tfm AndAlso wtbs Then 'Création d'un FolderTexte selon TypeForMultiple à NbPresencesX
                            nbAdded += 1
                            didFTT.Add(curFTT.noFolderTexteType)

                            Dim noMultiple As Integer = (countPresences - curFTT.nbPresencesX + 1) / curFTT.nbPresencesMultiple + 1 'Plus one because first is already created
                            multiple = " " & noMultiple
                            ftScript.AppendLine(FolderText.add(curFTT.noFolderTexteType, status.rv.noClient, status.rv.noFolder, curFTT.textTitle & multiple, lastRVDate, noMultiple, status.extraInfos.noTRPTraitant, status.rv.clientName, False))
                        End If
                    Next

                    'Créé les FolderTextes multiple dont le FTT n'est pas associé à la codification
                    If status.extraInfos.noFTTs <> "" Then
                        Dim noFTTs() As String = status.extraInfos.noFTTs.Split(New Char() {","})
                        For i As Integer = 0 To noFTTs.GetUpperBound(0)
                            Dim curFTT As FolderTextType = FolderTextTypesManager.getInstance.getItemable(Integer.Parse(noFTTs(i)))
                            Dim countFT As Integer = 0
                            For c As Integer = 0 To countFTs.GetUpperBound(1)
                                If countFTs(0, c) = curFTT.noFolderTexteType Then countFT = countFTs(1, c) : Exit For
                            Next
                            Dim tfm As Boolean = curFTT.typeForMultiple = FolderTextType.TypeMultiple.NbPresencesX AndAlso curFTT.multiple AndAlso ((countPresences - curFTT.nbPresencesX + 1) Mod curFTT.nbPresencesMultiple) = 0
                            Dim wtbs As Boolean = curFTT.whenToBeStopped <> FolderTextType.WhenToBeStop.OnMaxReached OrElse curFTT.nbMultipleEnding < countFT
                            If didFTT.Contains(curFTT.noFolderTexteType) = False AndAlso tfm AndAlso wtbs Then
                                nbAdded += 1
                                didFTT.Add(curFTT.noFolderTexteType)

                                Dim noMultiple As Integer = (countPresences - curFTT.nbPresencesX + 1) / curFTT.nbPresencesMultiple
                                ftScript.AppendLine(FolderText.add(curFTT.noFolderTexteType, status.rv.noClient, status.rv.noFolder, curFTT.textTitle & " " & noMultiple, lastRVDate, noMultiple, status.extraInfos.noTRPTraitant, status.rv.clientName, False))
                            End If
                        Next i
                    End If

                    'Création des alertes liés au dossier + Vérification des alertes par rapport au nombre de présences
                    Dim curFCAs As Generic.List(Of FolderAlertType) = curFolderCode.folderAlertTypes()
                    For Each curFCA As FolderAlertType In curFCAs
                        'Création
                        If curFCA.startingDate_Type = FolderAlertType.StartingDateTypes.OnPresenceX AndAlso countPresences = (curFCA.startingDate_NbPresencesX - 1) Then
                            FolderAlert.add(curFCA.noFolderAlertType, status.rv.noClient, status.rv.noFolder, status.rv.dateHeure.AddDays(curFCA.startingDate_NbDaysX - curFCA.alertNbDaysDiff), status.extraInfos.noTRPTraitant)
                            updateTRPTraitantAlerts = True
                        End If
                        'Vérification
                        If countPresences = (curFCA.startingDate_NbPresencesX + curFCA.nbPresencesX - curFCA.alertNbPresencesDiff - 1) AndAlso curFCA.isAlertDone(status.rv.noFolder) = False Then
                            DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", "(SELECT TOP 1 LastNoUserAlert FROM FolderAlerts WHERE NoFolder=" & status.rv.noFolder & " AND NoFolderAlertType=" & curFCA.noFolderAlertType & ")", False)
                            FolderAlert.add(curFCA.noFolderAlertType, status.rv.noClient, status.rv.noFolder, status.rv.dateHeure, status.extraInfos.noTRPTraitant)
                            updateTRPTraitantAlerts = True
                        End If
                    Next

                    'Send update for FolderTextes and their alert
                    If nbAdded <> 0 Then
                        DBLinker.executeSQLScript(ftScript.ToString)
                        InternalUpdatesManager.getInstance.sendUpdate("AccountsDossierTextBoxes(" & status.rv.noClient & "," & status.rv.noFolder & ")")
                    End If

                    If updateTRPTraitantAlerts Then AlertsManager.sendUpdate(status.extraInfos.noTRPTraitant)

                    If date1Infdate2(status.rv.dateHeure, lastRVDate) Then status.hasToEnsureFTDates = True


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

                    REM tran DBLinker.GetInstance.RollbackTransaction()
                    Throw New RVStatusException(errorTxt)
                End If

                'REM tran         DBLinker.GetInstance.CommitTransaction()

                'REM_CODES
                If status.hasToEnsureFTDates Then DBLinker.getInstance.executestoredProcedure("dbo.ensureFTDatesOnPresence", New String() {status.rv.noFolder})

                'TODO : Old line changed to ensure payment window appears whenever the client is owing something
                ' What was the noFacture condition useful for ??
                'status.showPayment = status.showPayment OrElse (myFacture.amountBilledToClient <> 0 AndAlso status.rv.noFacture <> myFacture.noFacture AndAlso status.rv.getFolderCode.autoShowPayment)
                status.showPayment = status.showPayment OrElse (Bill.isPaymentsToDoByClient(myFacture.parNoClient) AndAlso status.rv.getFolderCode.autoShowPayment)
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
                done = deleteFacturation(myFacture.noFacture)
                myFacture.noFacture = 0

                Dim noATD() As String
                Dim userAlertUpdated As New Generic.List(Of String)
                Dim updateFT As Boolean = False
                'Remove FolderTextes Where WhenToBeCreated Is OnPresencesX
                Dim noFTTSelect As String = "(SELECT NoFolderTexte FROM FolderTextes INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FolderTextes.NoFolderTexteType Where FTT.WhenToBeCreated=" & FolderTextType.WhenToBeCreate.OnPresenceX & " AND FTT.NbPresencesX=" & countPresences & " AND NoFolder=" & status.rv.noFolder & ")"
                Dim noAlertsToDel(,) As String = DBLinker.getInstance.readDB("FolderTexteAlerts INNER JOIN UsersAlerts ON UsersAlerts.NoUserAlert = FolderTexteAlerts.NoUserAlert", "FolderTexteAlerts.NoUserAlert,NoUser", "WHERE NoFolderTexte IN " & noFTTSelect)
                DBLinker.getInstance.delDB("FolderTexteAlerts", "NoFolderTexte", noFTTSelect, False, , , , , " IN ")
                Dim NbUADeleted, nbFTDeleted As Integer
                If noAlertsToDel IsNot Nothing AndAlso noAlertsToDel.Length <> 0 Then
                    ReDim noATD(noAlertsToDel.GetUpperBound(1))
                    For i As Integer = 0 To noAlertsToDel.GetUpperBound(1)
                        noATD(i) = noAlertsToDel(0, i)
                    Next
                    DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", "(" & String.Join(",", noATD) & ")", False, , , , , " IN ", , NbUADeleted)
                End If
                DBLinker.getInstance.delDB("FolderTextes", "NoFolderTexte", noFTTSelect, False, , , , , " IN ", , nbFTDeleted)
                If nbFTDeleted <> 0 Then updateFT = True
                If NbUADeleted <> 0 Then
                    For i As Integer = 0 To noAlertsToDel.GetUpperBound(1)
                        If userAlertUpdated.IndexOf(noAlertsToDel(1, i)) = -1 Then userAlertUpdated.Add(noAlertsToDel(1, i))
                    Next
                End If

                'Remove FolderTextes Where TypeForMultiple Is NbPresencesX
                noFTTSelect = "(SELECT NoFolderTexte FROM FolderTextes INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FolderTextes.NoFolderTexteType Where FTT.TypeForMultiple=" & FolderTextType.TypeMultiple.NbPresencesX & " AND CAST(FolderTextes.NoMultiple - 1 AS float)=(CAST((" & countPresences & "-FTT.NbPresencesX) AS float)/CAST(FTT.NbPresencesMultiple AS float)) AND NoFolder=" & status.rv.noFolder & ")"
                noAlertsToDel = DBLinker.getInstance.readDB("FolderTexteAlerts INNER JOIN UsersAlerts ON UsersAlerts.NoUserAlert = FolderTexteAlerts.NoUserAlert", "FolderTexteAlerts.NoUserAlert,NoUser", "WHERE NoFolderTexte IN " & noFTTSelect)
                DBLinker.getInstance.delDB("FolderTexteAlerts", "NoFolderTexte", noFTTSelect, False, , , , , " IN ")
                NbUADeleted = 0
                nbFTDeleted = 0
                If noAlertsToDel IsNot Nothing AndAlso noAlertsToDel.Length <> 0 Then
                    ReDim noATD(noAlertsToDel.GetUpperBound(1))
                    For i As Integer = 0 To noAlertsToDel.GetUpperBound(1)
                        noATD(i) = noAlertsToDel(0, i)
                    Next
                    DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", "(" & String.Join(",", noATD) & ")", False, , , , , " IN ", , NbUADeleted)
                End If
                DBLinker.getInstance.delDB("FolderTextes", "NoFolderTexte", noFTTSelect, False, , , , , " IN ", , nbFTDeleted)
                If nbFTDeleted <> 0 Then updateFT = True
                If NbUADeleted <> 0 Then
                    For i As Integer = 0 To noAlertsToDel.GetUpperBound(1)
                        If userAlertUpdated.IndexOf(noAlertsToDel(1, i)) = -1 Then
                            userAlertUpdated.Add(noAlertsToDel(1, i))
                        End If
                    Next
                End If

                'Remove alerts linked to folder
                noAlertsToDel = DBLinker.getInstance.readDB("FolderAlerts INNER JOIN UsersAlerts ON UsersAlerts.NoUserAlert = FolderAlerts.LastNoUserAlert", "FolderAlerts.LastNoUserAlert,NoUser,FolderAlerts.NoFolderAlertType", "WHERE NoFolder=" & status.rv.noFolder)
                If noAlertsToDel IsNot Nothing AndAlso noAlertsToDel.Length <> 0 Then
                    ReDim noATD(0)
                    Dim n As Integer = 0
                    For i As Integer = 0 To noAlertsToDel.GetUpperBound(1)
                        Dim curFCA As FolderAlertType = FolderAlertTypesManager.getInstance.getItemable(noAlertsToDel(2, i))
                        Dim delFirsts As Boolean = curFCA.startingDate_Type = FolderAlertType.StartingDateTypes.OnPresenceX AndAlso countPresences = curFCA.startingDate_NbPresencesX
                        Dim delSeconds As Boolean = countPresences = (curFCA.startingDate_NbPresencesX + curFCA.nbPresencesX - curFCA.alertNbPresencesDiff)
                        If delFirsts OrElse delSeconds Then
                            ReDim Preserve noATD(n)
                            noATD(n) = noAlertsToDel(0, i)
                            If userAlertUpdated.IndexOf(noAlertsToDel(1, i)) = -1 Then
                                userAlertUpdated.Add(noAlertsToDel(1, i))
                            End If
                            n += 1
                        End If
                    Next i
                    Dim strATD As String = String.Join(",", noATD)
                    If strATD <> "" Then DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", "(" & strATD & ")", False, , , , , " IN ", , NbUADeleted)
                End If
                'Recreate first alert if necessary
                Dim hasToRecreateFirst As Boolean = False
                Dim curFATs As Generic.List(Of FolderAlertType) = curFolderCode.folderAlertTypes()
                For Each curFAT As FolderAlertType In curFATs
                    Dim delSeconds As Boolean = countPresences = (curFAT.startingDate_NbPresencesX + curFAT.nbPresencesX - curFAT.alertNbPresencesDiff)
                    If delSeconds Then hasToRecreateFirst = True
                Next

                If hasToRecreateFirst Then
                    Dim folderDates(,) As String = DBLinker.getInstance.readDB("FolderDates", "CreationDate,DateRef,DateAccident,DateRechute", "WHERE NoFolder=" & status.rv.noFolder)
                    For Each curFAT As FolderAlertType In curFATs
                        Dim delSeconds As Boolean = countPresences = (curFAT.startingDate_NbPresencesX + curFAT.nbPresencesX - curFAT.alertNbPresencesDiff)
                        If delSeconds Then 'Recreate first
                            Dim affDate As Date = New Date(2000, 1, 1)
                            Select Case curFAT.startingDate_Type
                                Case FolderAlertType.StartingDateTypes.OnDateAccident
                                    If folderDates(2, 0) <> "" Then affDate = folderDates(2, 0)
                                Case FolderAlertType.StartingDateTypes.OnDateRechute
                                    If folderDates(3, 0) <> "" Then affDate = folderDates(3, 0)
                                Case FolderAlertType.StartingDateTypes.OnDateReferencce
                                    If folderDates(1, 0) <> "" Then affDate = folderDates(1, 0)
                                Case FolderAlertType.StartingDateTypes.OnFolderCreation
                                    If folderDates(0, 0) <> "" Then affDate = folderDates(0, 0)
                                Case FolderAlertType.StartingDateTypes.OnPresenceX
                                    Dim rvDate(,) As String = DBLinker.getInstance.readDB("SELECT DateHeure,NoTRP FROM (SELECT DateHeure,NoTRP,ROW_NUMBER() OVER(ORDER BY NoVisite) AS No FROM InfoVisites WHERE NoStatut=4 AND InfoVisites.NoFolder=" & status.rv.noFolder & ") AS T WHERE T.No=" & curFAT.startingDate_NbPresencesX)
                                    If rvDate IsNot Nothing AndAlso rvDate.Length <> 0 Then affDate = rvDate(0, 0)
                            End Select
                            affDate = affDate.AddDays(curFAT.startingDate_NbDaysX - curFAT.alertNbDaysDiff)
                            If date1Infdate2(affDate, Date.Today) = False Then
                                FolderAlert.add(curFAT.noFolderAlertType, status.rv.noClient, status.rv.noFolder, affDate, status.extraInfos.noTRPTraitant)
                                If userAlertUpdated.IndexOf(status.extraInfos.noTRPTraitant) = -1 Then userAlertUpdated.Add(status.extraInfos.noTRPTraitant)
                            End If
                        End If
                    Next
                End If


                'Send updates of foldertextes and useralerts modifications
                If updateFT Then InternalUpdatesManager.getInstance.sendUpdate("AccountsDossierTextBoxes(" & status.rv.noClient & "," & status.rv.noFolder & ")")
                For i As Integer = 0 To userAlertUpdated.Count - 1
                    AlertsManager.sendUpdate(userAlertUpdated(i))
                Next

                If date1Infdate2(status.rv.dateHeure, lastRVDate) Then status.hasToEnsureFTDates = True

                If done = False Then
                    'REM tran DBLinker.GetInstance.RollbackTransaction()
                    Throw New RVStatusException(errorTxt)
                End If
            End Sub

            Public Overrides Sub verifyChange(ByVal status As RVStatusChange)
                Dim myNewRaison As String = ""

                'Vérifications du nouveau status
                'Si le dossier est désactivée, demande de l'activé
                If status.extraInfos.folderStatus = FoldersStatus.FolderPossibleStatuses.Inactive And PreferencesManager.getGeneralPreferences()("ActiveFolderAutoOnRVStatusChange") = False Then
                    If MessageBox.Show("Le dossier (#" & status.rv.noFolder & ") du rendez-vous est inactif. Voulez-vous le réactiver ? (Sinon, impossible de changer le statut de ce rendez-vous)", "Dossier inactif", MessageBoxButtons.YesNo) = DialogResult.No Then
                        Throw New RVStatusException("Le dossier #" & status.rv.noFolder & " est inactif. Impossible d'ajouter le rendez-vous.")
                    Else
                        Try
                            ClientFolder.changeStatus(FoldersStatus.FolderPossibleStatuses.Inactive, FoldersStatus.FolderPossibleStatuses.Active, status.rv.noClient, status.rv.noFolder, True)
                        Catch ex As Exception
                            Throw ex
                        Finally
                        End Try

                    End If
                End If
            End Sub

            Public Overrides Function toString() As String
                Return "Présent"
            End Function
        End Class

    End Class
End Namespace

