Imports CI.Clinica.Accounts.Clients.Folders.Codifications
Imports CI.Clinica.Accounts.Clients.Folders
Imports CI.Clinica.Accounts.Clients.Folders.RVsStatus

Namespace Accounts.Clients.Folders
    Partial Public Class RVs

        Public Class RendezVous
            Inherits AgendaEntry 'Peut-être pas bon.. peut-être vaut mieux implements IAgendaEntry

            Private _confirmDeletion As Boolean = True
            Private _FolderNoCodeUnique As Integer = 0
            Private _FolderNoCodeUser As Integer = 0
            Private _FolderNoCodeDate As Date = LIMIT_DATE
            Private _Service As String = ""
            Private _status As RVsStatus.RVPossibleStatuses

            Protected Sub New()
            End Sub

            Public Sub New(ByVal loadingData As DBItemableData)
                MyBase.New()
                loadData(loadingData)
            End Sub

#Region "Propriétés"
            Public ReadOnly Property status() As RVPossibleStatuses
                Get
                    Return _status
                End Get
            End Property

            Public Property confirmDeletion() As Boolean
                Get
                    Return _confirmDeletion
                End Get
                Set(ByVal value As Boolean)
                    _confirmDeletion = value
                End Set
            End Property

            Public ReadOnly Property noVisite() As Integer
                Get
                    If data.Table.Columns.Contains("NoVisite") = False Then Return noAgendaEntry

                    Return data("NoVisite")
                End Get
            End Property

            Public Property noFacture() As Integer
                Get
                    If data("NoFacture") Is DBNull.Value Then Return 0

                    Return data("NoFacture")
                End Get
                Set(ByVal value As Integer)
                    data("NoFacture") = value
                End Set
            End Property

            Public Property confirmed() As Boolean
                Get
                    Return data("Confirmed")
                End Get
                Set(ByVal value As Boolean)
                    data("Confirmed") = value
                End Set
            End Property

            Public Property isAnnounced() As Boolean
                Get
                    Return data("IsAnnounced")
                End Get
                Set(ByVal value As Boolean)
                    data("IsAnnounced") = value
                End Set
            End Property


            Public Property evaluation() As Boolean
                Get
                    Return data("Evaluation")
                End Get
                Set(ByVal value As Boolean)
                    data("Evaluation") = value
                End Set
            End Property

            Public Property remarks() As String
                Get
                    If data("RemarquesRV") Is DBNull.Value Then Return ""

                    Return data("RemarquesRV")
                End Get
                Set(ByVal value As String)
                    data("RemarquesRV") = value
                End Set
            End Property

            Public Property service() As String
                Get
                    Return _Service
                End Get
                Set(ByVal value As String)
                    _Service = value
                End Set
            End Property

            Public Property flagged() As Boolean
                Get
                    Return data("Flagged")
                End Get
                Set(ByVal value As Boolean)
                    data("Flagged") = value
                End Set
            End Property
#End Region
#Region "Proriétés qui ne devraient pas être dans cette classe"
            Public Property remarksFolder() As String
                Get
                    If data("RemarquesFolder") Is DBNull.Value Then Return ""

                    Return data("RemarquesFolder")
                End Get
                Set(ByVal value As String)
                    data("RemarquesFolder") = value
                End Set
            End Property

            Public Property remarksClient() As String
                Get
                    If data("RemarquesClient") Is DBNull.Value Then Return ""

                    Return data("RemarquesClient")
                End Get
                Set(ByVal value As String)
                    data("RemarquesClient") = value
                End Set
            End Property

            Public ReadOnly Property amountPaid() As Integer
                Get
                    If data.Table.Columns.Contains("MontantPayé") = False Then Throw New NotSupportedException()
                    If data("MontantPayé") Is DBNull.Value Then Return 0

                    Return data("MontantPayé")
                End Get
            End Property

            Public ReadOnly Property isBillPaid() As Boolean
                Get
                    If data.Table.Columns.Contains("IsBillPaid") = False Then Throw New NotSupportedException()
                    If data("IsBillPaid") Is DBNull.Value Then Return False

                    Return data("IsBillPaid") <> 0
                End Get
            End Property

            Public ReadOnly Property isBillSouffrance() As Boolean
                Get
                    If data.Table.Columns.Contains("IsBillSouffrance") = False Then Throw New NotSupportedException()
                    If data("IsBillSouffrance") Is DBNull.Value Then Return False

                    Return data("IsBillSouffrance")
                End Get
            End Property

            Public Overridable ReadOnly Property clientName() As String 'This has to be removed to go in the Client class (which still doesn't exists)
                Get
                    Return data("ItemText")
                End Get
            End Property

            Public ReadOnly Property folderFrequency() As Integer
                Get
                    If data.Table.Columns.Contains("FolderFrequency") = False Then Throw New NotSupportedException()
                    If data("FolderFrequency") Is DBNull.Value Then Return 0

                    Return data("FolderFrequency")
                End Get
            End Property

            Public Property folderNoCodeUnique() As Integer 'This has to be removed to go in the Folder class (which still doesn't exists)
                Get
                    Return _FolderNoCodeUnique
                End Get
                Set(ByVal value As Integer)
                    _FolderNoCodeUnique = value
                End Set
            End Property

            Public Property folderNoCodeUser() As Integer 'This has to be removed to go in the Folder class (which still doesn't exists)
                Get
                    Return _FolderNoCodeUser
                End Get
                Set(ByVal value As Integer)
                    _FolderNoCodeUser = value
                End Set
            End Property

            Public Property folderNoCodeDate() As Date 'This has to be removed to go in the Folder class (which still doesn't exists)
                Get
                    Return _FolderNoCodeDate
                End Get
                Set(ByVal value As Date)
                    _FolderNoCodeDate = value
                End Set
            End Property

            Public ReadOnly Property telephones() As String 'This has to be removed to go in the Client class (which still doesn't exists)
                Get
                    Return data("Telephones")
                End Get
            End Property
#End Region

            Public Sub changePeriod()
                'REM Shall be gotten from somewhere else to support user periods
                Dim periods() As String = {"15 minutes", "30 minutes", "45 minutes", "1 heure", "1h 15 minutes", "1h 30 minutes", "1h 45 minutes", "2 heures"}
                Dim myMultiChoice As New multichoice
                Dim myChoice As Integer = myMultiChoice.GetChoice("Choix de la période", String.Join("§", periods), "INDEX", "§", False, periods(Me.period / 15 - 1))
                If myChoice = -1 Then Exit Sub

                Dim newPeriod As Integer = (myChoice + 1) * 15

                changePeriod(newPeriod)
            End Sub

            Private Sub changePeriod(ByVal newPeriod As Integer)
                If Me.period = newPeriod Then Exit Sub

                Dim conflictMessage As String = AgendaManager.getInstance.checkTimeConflict(dateHeure, newPeriod, noTRP, , noClient, folderNoCodeUnique, , , noVisite, noFolder)
                If conflictMessage <> "" Then
                    MessageBox.Show(conflictMessage, "Impossible de changer la période", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite,Comments", CInt(UserActions.RV_ChangePeriod) & "," & noFolder & "," & noClient & "," & noVisite & ",'De " & Me.period & " minutes vers " & newPeriod & " minutes'")
                Me.period = newPeriod 'This line has to be after writeStats
                DBLinker.getInstance.updateDB("InfoVisites", "Periode=" & newPeriod, "NoVisite", Me.noVisite, False)

                updateVisites(noClient, noFolder, dateHeure, , False, noTRP)
            End Sub

            Public Overrides Function equals(ByVal obj As Object) As Boolean
                If obj Is Nothing Then Return False

                Return Me.noVisite.Equals(CType(obj, RendezVous).noVisite)
            End Function

            Public Function isOnQueueList() As Boolean
                'REM shall be replaced when QueueList will exist in object form
                Dim read() As String = DBLinker.getInstance.readOneDBField("ListeAttente", "NoQL", "WHERE (NoVisite)=" & noVisite)
                Return read IsNot Nothing AndAlso read.Length <> 0 AndAlso read(0) <> ""
            End Function

            Public Sub transferToFolder(ByVal newNoFolder As Integer)
                'Droit & Accès
                If currentDroitAcces(89) = False Then
                    'Message & Exit
                    MessageBox.Show("Vous n'avez pas le droit de transférer un rendez-vous." & vbCrLf & "Merci!", "Droit & Accès")
                    Exit Sub
                End If

                Dim changingScript As String = "UPDATE InfoVisites SET NoFolder=" & newNoFolder & " WHERE NoVisite=" & noVisite & ";"
                changingScript &= "UPDATE StatVisites SET NoFolder=" & newNoFolder & " WHERE NoVisite=" & noVisite & ";"
                changingScript &= "UPDATE ListeAttente SET NoFolder=" & newNoFolder & " WHERE NoVisite=" & noVisite & ";"

                DBLinker.executeSQLScript(changingScript)
                DBHelper.writeStats("StatVisites", "NoAction, NoVisite, NoFolder, NoClient, Comments", "28," & noVisite & "," & newNoFolder & "," & noClient & ",'Du dossier #" & noFolder & " vers le dossier #" & newNoFolder & "'")

                InternalUpdatesManager.getInstance.sendUpdate("AccountsVisites(" & noClient & "," & newNoFolder & ")")
                InternalUpdatesManager.getInstance.sendUpdate("AccountsVisites(" & noClient & "," & noFolder & ")")
                InternalUpdatesManager.getInstance.sendUpdate("Agendas(" & DateFormat.getTextDate(Me.dateHeure, DateFormat.TextDateOptions.YYYYMMDD_FullTime) & ",False," & noTRP & ")")
            End Sub

            Public Sub switchType()
                DBLinker.getInstance.updateDB("InfoVisites", "Evaluation='" & (Not evaluation) & "'", "NoVisite", Me.noVisite, False)
                updateVisites(Me.noClient, Me.noFolder, Me.dateHeure, , False, Me.noTRP)
            End Sub

            Public Sub changeService()
                Dim oldService As String = service
                Dim curTRP As User = UsersManager.getInstance.getUser(noTRP)
                If curTRP Is Nothing Then Exit Sub
                If curTRP.services = "" Then
                    MessageBox.Show("Impossible de modifier le service pour ce rendez-vous, car le thérapeute " & curTRP.toString() & " n'offre présentement aucun service.", "Service manquant", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Exit Sub
                End If

                Dim myMultiChoice As New multichoice
                Dim newService As String = myMultiChoice.GetChoice("Veuillez choisir le nouveau service", curTRP.services, , "§", , oldService)
                If newService = "" Or newService.StartsWith("ERROR") Or newService = oldService Then Exit Sub

                DBLinker.getInstance.updateDB("InfoVisites", "Service='" & newService.Replace("'", "''") & "'", "NoVisite", noVisite, False)
                DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite,Comments", CInt(UserActions.RV_ChangeService) & "," & noFolder & "," & noClient & "," & noVisite & ",'De " & oldService.Replace("'", "''") & " vers " & newService.Replace("'", "''") & "'")

                updateVisites(noClient, noFolder, dateHeure, , False, noTRP)
            End Sub

            Public Sub confirm()
                If MessageBox.Show("Avez-vous réellement confirmé ce rendez-vous ?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

                DBLinker.getInstance.updateDB("InfoVisites", "Confirmed='True'", "NoVisite", noVisite, False)
                DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite,Comments", "29," & noFolder & "," & noClient & "," & noVisite & ",''")
                updateVisites(Me.noClient, Me.noFolder, Me.dateHeure, , False, Me.noTRP)
            End Sub

            Public Sub generateRecu()
                Dim curFacture As New Bill(Me.noFacture)
                Dim filter As String = ""
                If PreferencesManager.getGeneralPreferences()("printRecuForClientAuto") = True Then filter = "C"
                curFacture.generateReceipt(filter)
            End Sub

            Public Overloads Function getFolderCode() As Clinica.Accounts.Clients.Folders.Codifications.FolderCode
                Return Clinica.Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getItemable(folderNoCodeUnique, folderNoCodeUser, Me.dateHeure)
            End Function

            Public Overloads Function getFolderCode(ByVal applicationDate As Date) As Clinica.Accounts.Clients.Folders.Codifications.FolderCode
                Return Clinica.Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getItemable(folderNoCodeUnique, folderNoCodeUser, applicationDate)
            End Function

            Private Sub innerChangeRemarks(ByVal newRemarques As String)
                remarks = newRemarques
                DBLinker.getInstance.updateDB("InfoVisites", "Remarques='" & newRemarques.Replace("'", "''") & "'", "NoVisite", noVisite, False)
                updateVisites(noClient, noFolder, dateHeure, Me, False, noTRP)
            End Sub

            Public Sub deleteRemarks()
                innerChangeRemarks("")
            End Sub

            Public Function changeStatus(ByVal newStatus As RVPossibleStatuses) As String
                Dim changeError As String = ""
                Dim changed As Boolean = False
                Try
                    RVStatusApplier.getInstance.changeStatus(New RVStatusChange(_status, newStatus, Me))
                    MyBase.setIsCutting(False)
                    changed = True
                Catch ex As RVStatusException
                    changeError = ex.Message
                Catch ex As UserRightException
                    changeError = ex.Message
                Catch ex As UserAlreadyUsingException
                    changeError = ex.Message
                End Try

                Return changeError
            End Function

            Public Sub changeRemarks()
                Dim myInputBoxPlus As New InputBoxPlus
                myInputBoxPlus.firstLetterCapital = True
                myInputBoxPlus.refusedChars = "/"
                Dim newRemarks As String = myInputBoxPlus("Veuillez entrer/modifier le texte de la remarque", "Remarque d'un rendez-vous", remarks)
                If newRemarks = "" Then Exit Sub

                innerChangeRemarks(newRemarks)
            End Sub

            Public Overrides Sub copy()
                'Droit & Accès
                If currentDroitAcces(16) = False Then
                    'Message & Exit
                    MessageBox.Show("Vous n'avez pas le droit de copier des rendez-vous." & vbCrLf & "Merci!", "Droit & Accès")
                    Exit Sub
                End If
                myMainWin.copyBox.setClient(Me, period / 15 - 1)
                MyBase.copy()
            End Sub

            Public Overrides Sub cut()
                copy()
                MyBase.cut()
            End Sub

            Public Overrides Sub loadData(ByVal data As DBItemableData)
                Dim curData As DataRow = data.mainData

                MyBase.loadData(data)
                _FolderNoCodeUnique = curData("FolderNoCodeUnique")
                If curData("FolderNoCodeUser") Is DBNull.Value Then
                    _FolderNoCodeUser = 0
                Else
                    _FolderNoCodeUser = curData("FolderNoCodeUser")
                End If
                _FolderNoCodeDate = curData("FolderNoCodeDate")
                _Service = curData("Service")
                _status = curData("NoStatut")
            End Sub

            Public Overrides Sub delete()
                'Droit & Accès
                If currentDroitAcces(75) = False Then
                    'Message & Exit
                    MessageBox.Show("Vous n'avez pas le droit de supprimer les rendez-vous." & vbCrLf & "Merci!", "Droit & Accès")
                    Exit Sub
                End If

                If _confirmDeletion AndAlso MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce rendez-vous ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

                If Me.noFacture <> 0 Then deleteFacturation(Me.noFacture)
                delVisite(noClient, noFolder, noVisite, dateHeure, , noTRP, autoSendUpdateOnDelete)
                onDeleted()
                If _confirmDeletion Then myMainWin.StatusText = "Suppression d'une plage le " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.YYYYMMDD) & " à " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.ShortTime) & IIf(noTRP <> 0, " pour " & UsersManager.getInstance.getUser(noTRP).toString, "")

                If CType(PreferencesManager.getGeneralPreferences()("ShowQLOnAgendaRemove"), Boolean) = True And skipQueueList = False And noTRP <> 0 Then openRestraintQueueList(dateHeure, noTRP)
            End Sub

            Public Overrides Sub saveData()
                Throw New NotImplementedException("Devrait avoir la possibilité d'enregistrer")

                onDataChanged()
                'TODO: use If autoSendUpdateOnSave Then
            End Sub

            Protected Function chooseFolder(ByRef myFrequence As Integer, ByVal dossiers(,) As String) As Integer
                'REM_CODES
                Dim NewFolder, folderIndex As Integer
                Dim msgReturn As Integer
                Dim ODossiers, OIndex, SOIndex(), soDossiers() As String
                ODossiers = ""
                OIndex = ""
                If Not dossiers Is Nothing AndAlso dossiers.Length <> 0 Then
                    For i As Integer = dossiers.GetUpperBound(1) To 0 Step -1
                        If dossiers(3, i) = True Then
                            ODossiers = "§" & dossiers(0, i) & " - " & dossiers(1, i) & " (" & FolderCodesManager.getInstance.getCodeNameByNoUnique(dossiers(2, i)) & ")" & ODossiers
                            OIndex = "§" & i & OIndex
                        End If
                    Next i
                Else
                    MessageBox.Show("Il n'existe aucun dossier pour le client " & clientName & " (" & noClient & ") " & "." & vbCrLf & "Veuillez prendre un nouveau rendez-vous à la place de coller.", "Dossier inexistant")
                    Return -1
                End If

                If ODossiers = "" Then
                    Dim myMsgBox As New MsgBox1()
                    msgReturn = myMsgBox.Message("Voulez-vous réactiver le dernier dossier ou en créer un nouveau", "Choix du dossier : " & dossiers(0, dossiers.GetUpperBound(1)), 2, "Réactivation", "Nouveau")
                    If msgReturn = 0 Then Return 0

                    If msgReturn = 1 Then
                        ClientFolder.changeStatus(FoldersStatus.FolderPossibleStatuses.Inactive, FoldersStatus.FolderPossibleStatuses.Active, noClient, dossiers(0, dossiers.GetUpperBound(1)))
                        NewFolder = dossiers(0, dossiers.GetUpperBound(1))
                        myFrequence = dossiers(6, dossiers.GetUpperBound(1))
                    End If
                Else
                    ODossiers = ODossiers.Substring(1)
                    OIndex = OIndex.Substring(1)
                    soDossiers = ODossiers.Split(New Char() {"§"})
                    SOIndex = OIndex.Split(New Char() {"§"})

                    If soDossiers.Length = 1 Then
                        If PreferencesManager.getUserPreferences()("QuestionLastNewFolder") = True Then
                            NewFolder = dossiers(0, OIndex)
                            folderIndex = OIndex
                            If dossiers(6, OIndex) = "" Then
                                myFrequence = -1
                            Else
                                myFrequence = dossiers(6, OIndex)
                            End If
                            Return NewFolder
                        End If
                        Dim myMsgBox As New MsgBox1()
                        msgReturn = myMsgBox.Message("Voulez-vous sélectionner le dernier dossier actif ou en créer un nouveau", "Choix du dossier", 2, "Dernier", "Nouveau")
                        If msgReturn = 0 Then Return 0

                        If msgReturn = 1 Then NewFolder = dossiers(0, OIndex) : folderIndex = OIndex : myFrequence = IIf(dossiers(6, OIndex) <> "", dossiers(6, OIndex), -1)
                    Else
                        If PreferencesManager.getUserPreferences()("QuestionChoosingFolder") = False Or noFolder = 0 Then
                            ODossiers = "* Nouveau dossier *" & "§" & ODossiers
                            Dim myMultiChoice As New multichoice()
                            msgReturn = myMultiChoice.GetChoice("Choix du dossier", ODossiers, "INDEX", "§", False)

                            If msgReturn < 0 Then Return -1
                            If msgReturn = 0 Then
                                NewFolder = 0
                            Else
                                NewFolder = dossiers(0, SOIndex(msgReturn - 1))
                                folderIndex = SOIndex(msgReturn - 1)
                                If dossiers(6, SOIndex(msgReturn - 1)) <> "" Then myFrequence = dossiers(6, SOIndex(msgReturn - 1))
                            End If
                        Else
                            For i As Integer = 0 To soDossiers.GetUpperBound(0)
                                If soDossiers(i).StartsWith(noFolder & " -") Then
                                    msgReturn = i
                                End If
                            Next i
                            NewFolder = noFolder
                            folderIndex = SOIndex(msgReturn)
                            If dossiers(6, SOIndex(msgReturn)) <> "" Then myFrequence = dossiers(6, SOIndex(msgReturn))
                        End If
                    End If
                End If

                Return NewFolder
            End Function

            Private Function pasteFromCut(ByVal dateHeure As Date, ByVal noTRP As Integer, ByVal newPeriod As Integer) As Boolean
                If isCutting Then
                    Dim checkConflictOptions As AgendaManager.TimeConflictOptions = AgendaManager.TimeConflictOptions.AcceptMultipleCodes Or AgendaManager.TimeConflictOptions.VerifySchedule Or AgendaManager.TimeConflictOptions.VerifyAbsences Or AgendaManager.TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime
                    Dim tc As String = AgendaManager.getInstance.checkTimeConflict(dateHeure, newPeriod, noTRP, If(dateHeure.Date.Equals(Me.dateHeure.Date), AgendaManager.TimeConflictOptions.AcceptDoubleClient Or AgendaManager.TimeConflictOptions.VerifySchedule Or AgendaManager.TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime, checkConflictOptions), noClient, getFolderCode.noUnique, , , Me.noVisite, noFolder)
                    If Not tc = "" Then MessageBox.Show(tc, "Conflit") : Return True

                    If Not RendezVousAcceptor.isAccepted(noClient, noFolder, dateHeure, noTRP, folderFrequency, Me.noTRP, Me.noVisite) Then Return True

                    'First paste when cut
                    Dim oldTRP As Integer = Me.noTRP
                    Dim oldDateHeure As Date = Me.dateHeure
                    If dateHeure.Equals(Me.dateHeure) = False Then
                        DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite,Comments", "33," & noFolder & "," & noClient & "," & noVisite & ",'De " & DateFormat.getTextDate(Me.dateHeure) & " " & DateFormat.getTextDate(Me.dateHeure, DateFormat.TextDateOptions.FullTime) & " à " & DateFormat.getTextDate(dateHeure) & " " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.FullTime) & "'")
                        myMainWin.StatusText = "Le rendez-vous de " & DateFormat.getTextDate(Me.dateHeure) & " " & DateFormat.getTextDate(Me.dateHeure, DateFormat.TextDateOptions.FullTime) & " au dossier #" & noFolder & " du client " & Me.clientName & " a été déplacé au " & DateFormat.getTextDate(dateHeure) & " " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.FullTime)
                    End If
                    If noTRP.Equals(Me.noTRP) = False Then
                        DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite,Comments", "34," & noFolder & "," & noClient & "," & noVisite & ",'De " & UsersManager.getInstance.getUser(Me.noTRP).toString & " à " & UsersManager.getInstance.getUser(noTRP).toString & "'")
                        myMainWin.StatusText = "Le rendez-vous de " & DateFormat.getTextDate(Me.dateHeure) & " " & DateFormat.getTextDate(Me.dateHeure, DateFormat.TextDateOptions.FullTime) & " au dossier #" & noFolder & " du client " & Me.clientName & " a été déplacé du thérapeute " & UsersManager.getInstance.getUser(Me.noTRP).toString & " à " & UsersManager.getInstance.getUser(noTRP).toString
                    End If

                    Me.dateHeure = dateHeure
                    Me.noTRP = noTRP

                    Dim nbAffectedRows As Integer = 0
                    DBLinker.getInstance.updateDB("InfoVisites", "IsOnAgenda=1,Periode=" & newPeriod & ",DateHeure='" & DateFormat.getTextDate(dateHeure) & " " & DateFormat.getTextDate(dateHeure, DateFormat.TextDateOptions.FullTime) & "',noTRP=" & noTRP, "NoVisite", Me.noVisite, False, , , , , nbAffectedRows)

                    updateVisites(Me.noClient, noFolder, dateHeure, , False, noTRP, nbAffectedRows <> 0)

                    If nbAffectedRows <> 0 Then 'If R-V still exists, otherwise continue with other pasting method
                        If CType(PreferencesManager.getGeneralPreferences()("ShowQLOnAgendaRemove"), Boolean) = True And skipQueueList = False Then openRestraintQueueList(oldDateHeure, oldTRP)

                        'Condition is to ensure not sending same update, because if it's moved on same day
                        If oldTRP <> Me.noTRP OrElse dateHeure.Date.Equals(oldDateHeure.Date) = False Then updateVisites(Me.noClient, noFolder, oldDateHeure, , False, oldTRP)
                        Return True
                    End If
                End If

                Return False
            End Function

            Private Sub pasteFromCopy(ByVal dateHeure As Date, ByVal noTRP As Integer, ByVal newPeriod As Integer)
                Dim dossiers(,) As String = DBLinker.getInstance.readDB("SiteLesion RIGHT JOIN InfoFolders ON SiteLesion.NoSiteLesion = InfoFolders.NoSiteLesion", "InfoFolders.NoFolder, SiteLesion.SiteLesion, InfoFolders.NoCodeUnique, InfoFolders.StatutOuvert, InfoFolders.Service, InfoFolders.NoTRPTraitant, InfoFolders.Frequence", "WHERE (NoClient)=" & noClient & ";")

                Dim i, myFrequence As Short
                myFrequence = -1
                Dim NewFolder, folderIndex As Integer
                Dim myText As String
                If WindowsManager.getInstance.selected Is Nothing OrElse Not TypeOf WindowsManager.getInstance.selected Is Agenda Then Exit Sub

                myText = myMainWin.copyBox.clientName
                myText = myText.Replace(vbCrLf, "<br>")

                If isCutting Then
                    NewFolder = noFolder
                    For i = dossiers.GetUpperBound(1) To 0 Step -1
                        If dossiers(0, i) = NewFolder Then
                            If dossiers(6, i) = "" Then
                                myFrequence = -1
                            Else
                                myFrequence = dossiers(6, i)
                            End If
                            folderIndex = i
                        End If
                    Next i
                Else
                    NewFolder = chooseFolder(myFrequence, dossiers)
                    If NewFolder > 0 Then
                        For d As Integer = dossiers.GetUpperBound(1) To 0 Step -1 'Do it reversed because there is more chance to select one of the newest instead of the oldest
                            If dossiers(0, d) = NewFolder Then
                                folderIndex = d
                                Exit For
                            End If
                        Next d
                    End If
                End If

                If NewFolder = 0 Then
                    openNewRV(noClient, noTRP, , 0, dateHeure)
                ElseIf NewFolder > 0 Then
                    'Change folder no + codification
                    Dim curFolderNoCodeUnique As Integer = folderNoCodeUnique
                    Dim curService As String = service
                    Dim curNoTRP As Integer = Me.noTRP
                    If NewFolder <> noFolder Then
                        curFolderNoCodeUnique = dossiers(2, folderIndex)
                        curService = dossiers(4, folderIndex)
                        curNoTRP = dossiers(5, folderIndex)
                    End If

                    Dim checkConflictOptions As AgendaManager.TimeConflictOptions = AgendaManager.TimeConflictOptions.AcceptMultipleCodes Or AgendaManager.TimeConflictOptions.VerifySchedule Or AgendaManager.TimeConflictOptions.VerifyAbsences Or AgendaManager.TimeConflictOptions.EnsureClientHasNoOtherRVAtTheSameTime
                    Dim tc As String = AgendaManager.getInstance.checkTimeConflict(dateHeure, myMainWin.copyBox.periodeMinutes, noTRP, checkConflictOptions, noClient, curFolderNoCodeUnique, , , , noFolder)
                    If Not tc = "" Then MessageBox.Show(tc, "Conflit") : Exit Sub

                    Dim myService As String = curService
                    'Sélectionne le service si changement de TRP
                    If noTRP <> curNoTRP Then
                        Dim myMultiChoice As New multichoice()
                        Dim curTRP As User = UsersManager.getInstance.getUser(noTRP)
                        If curTRP.services.IndexOf("§") <> -1 Then
                            myService = myMultiChoice.GetChoice("Veuillez sélectionner le service", curTRP.services, , "§")
                            If myService.ToUpper.StartsWith("ERROR") Then Exit Sub
                        Else
                            myService = curTRP.services
                        End If
                    End If

                    If RendezVousManager.getInstance.addRendezVous(dateHeure, dateHeure, myMainWin.copyBox.periodeMinutes, noTRP, noClient, NewFolder, myService, myFrequence, isCutting AndAlso evaluation, curNoTRP, isCutting AndAlso confirmed) = 0 Then Exit Sub
                    updateVisites(noClient, NewFolder, dateHeure, , , noTRP)
                End If
            End Sub

            Public Overrides Sub pasteTo(ByVal dateHeure As Date, ByVal noTRP As Integer, ByVal newPeriod As Integer)
                'IsCutting is required, when the RV was cut, deleted and then paste
                If isCutting AndAlso Me.dateHeure = dateHeure AndAlso Me.noTRP = noTRP Then
                    changePeriod(newPeriod)

                    MyBase.pasteTo(dateHeure, noTRP, newPeriod)
                    Exit Sub
                End If

                If pasteFromCut(dateHeure, noTRP, newPeriod) Then
                    MyBase.pasteTo(dateHeure, noTRP, newPeriod)
                    Exit Sub
                End If

                pasteFromCopy(dateHeure, noTRP, newPeriod)
                MyBase.pasteTo(dateHeure, noTRP, newPeriod)
            End Sub

            Public Overrides Function toString() As String
                Return itemText
            End Function

            Public Sub annonce()
                DBLinker.getInstance.updateDB("InfoVisites", "IsAnnounced=1", "NoVisite", Me.noVisite, False)
                AlertsManager.getInstance.addAlert("Le/la client(e) " & Me.clientName & " de " & DateFormat.getTextDate(Me.dateHeure, DateFormat.TextDateOptions.ShortTime) & " est arrivé(e).", Me.noTRP, AlertsManager.AType.OpenClientAccount, Me.noClient, Date.Now.AddHours(1), , , True)
                myMainWin.StatusText = "Client(e) " & Me.clientName & " de " & DateFormat.getTextDate(Me.dateHeure, DateFormat.TextDateOptions.ShortTime) & " pour " & UsersManager.getInstance.getUser(Me.noTRP).getFullName() & " a été annoncé"
                updateVisites(Me.noClient, Me.noFolder, Me.dateHeure, , False, Me.noTRP)
            End Sub
        End Class


    End Class
End Namespace
