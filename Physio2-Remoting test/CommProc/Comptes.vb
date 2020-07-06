Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Module Comptes
    Private Function replaceKPAccount(ByVal noAccount As Integer, ByVal wontDelMessage As String, Optional ByVal skipConfirmation As Boolean = False) As Boolean
        Dim isReplaced As Boolean = False
        If skipConfirmation OrElse MessageBox.Show(wontDelMessage & vbCrLf & "Voulez-vous changer les éléments liés pour remplacer ce compte par un autre compte ?", "Suppression impossible", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
            Dim myKeypeopleSearch As New keypeopleSearch
            myKeypeopleSearch.MdiParent = Nothing
            myKeypeopleSearch.selected = True
            Dim kpChosen As KPSelectorReturn = myKeypeopleSearch.showDialog()
            If kpChosen.canceling OrElse kpChosen.noKP = 0 Then Return False
            If kpChosen.noKP = noAccount Then
                MessageBox.Show("Veuillez sélectionner un compte différent de celui à supprimer", "Compte identique", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return replaceKPAccount(noAccount, wontDelMessage, True)
            End If

            Dim updateScript As String = "UPDATE InfoFolders SET NoKP = " & kpChosen.noKP & " WHERE NoKP = " & noAccount & ";"
            updateScript &= "UPDATE CodesDossiersPeriodes SET NoKP = " & kpChosen.noKP & " WHERE NoKP = " & noAccount & ";"
            updateScript &= "UPDATE CommDeAKP SET NoKP = " & kpChosen.noKP & " WHERE NoKP = " & noAccount & ";"
            updateScript &= "UPDATE CommDeAKP SET NoKPDeA = " & kpChosen.noKP & " WHERE NoKPDeA = " & noAccount & ";"
            updateScript &= "UPDATE CommunicationsKP SET NoKP = " & kpChosen.noKP & " WHERE NoKP = " & noAccount & ";"
            updateScript &= "UPDATE CommunicationsKP SET NoKPFrom = " & kpChosen.noKP & " WHERE NoKPFrom = " & noAccount & ";"
            updateScript &= "UPDATE Factures SET NoKP = " & kpChosen.noKP & " WHERE NoKP = " & noAccount & ";"
            updateScript &= "UPDATE Factures SET ParNoKP = " & kpChosen.noKP & " WHERE ParNoKP = " & noAccount & ";"
            updateScript &= "UPDATE StatFactures SET NoKP = " & kpChosen.noKP & " WHERE NoKP = " & noAccount & ";"
            updateScript &= "UPDATE StatFactures SET ParNoKP = " & kpChosen.noKP & " WHERE ParNoKP = " & noAccount & ";"
            updateScript &= "UPDATE StatPaiements SET NoKP = " & kpChosen.noKP & " WHERE NoKP = " & noAccount & ";"
            updateScript &= "UPDATE StatPaiements SET ParNoKP = " & kpChosen.noKP & " WHERE ParNoKP = " & noAccount & ";"

            DBLinker.executeSQLScript(updateScript, False)

            isReplaced = True
        End If

        Return isReplaced
    End Function

    Public Function delAccount(ByVal noAccount As Integer, Optional ByVal isKP As Boolean = False) As String
        Dim wontDelMsg As String = "Vous ne pouvez pas supprimer ce compte, car il est déjà en cours d'utilisation"
        Dim billLinkedError As String = "'Vous ne pouvez pas supprimer ce compte, car il est lié à des factures existantes ou supprimées'"
        Dim myAccount As AccountBase

        Dim Table, Column, ensureDeleteScript As String
        Dim personName As String = ""
        If isKP Then
            myAccount = New Accounts.KeyPeople(noAccount)
            personName = getKPName(noAccount)
            Table = "KeyPeople"
            Column = "NoKP"
            ensureDeleteScript = "SELECT TOP 1 " & billLinkedError & " FROM Factures WHERE Factures.NoKP=" & noAccount & " OR Factures.ParNoKP=" & noAccount
            ensureDeleteScript &= " UNION SELECT TOP 1 " & billLinkedError & " FROM StatFactures WHERE StatFactures.NoKP=" & noAccount & " OR StatFactures.ParNoKP=" & noAccount
            ensureDeleteScript &= " UNION SELECT TOP 1 " & billLinkedError & " FROM StatPaiements WHERE StatPaiements.ParNoKP=" & noAccount
            ensureDeleteScript &= " UNION SELECT TOP 1 'Vous ne pouvez pas supprimer ce compte, car il est lié à des dossiers clients' FROM InfoFolders WHERE NoKP=" & noAccount
        Else
            myAccount = New Accounts.Client(noAccount)
            personName = getClientName(noAccount)
            Table = "InfoClients"
            Column = "NoClient"
            ensureDeleteScript = "SELECT TOP 1 " & billLinkedError & " FROM Factures WHERE Factures.NoClient=" & noAccount & " OR Factures.ParNoClient=" & noAccount
            ensureDeleteScript &= " UNION SELECT TOP 1 " & billLinkedError & " FROM StatFactures WHERE StatFactures.NoClient=" & noAccount & " OR StatFactures.ParNoClient=" & noAccount
            ensureDeleteScript &= " UNION SELECT TOP 1 " & billLinkedError & " FROM StatPaiements WHERE StatPaiements.ParNoClient=" & noAccount
        End If

        If myAccount.isSectorLocked() Then
            Return wontDelMsg
        End If

        wontDelMsg = "Vous ne pouvez pas supprimer ce compte, car il est déjà lié à d'autres élements du logiciel (Ex: Dossiers, Factures)"
        Dim isUsedData(,) As String = DBLinker.getInstance.readDB(ensureDeleteScript)
        Dim isUsed As Boolean = isUsedData IsNot Nothing AndAlso isUsedData.Length <> 0
        Dim hadDeleted As Boolean = isUsed = False

        Try
            hadDeleted = hadDeleted AndAlso DBLinker.getInstance.delDB(Table, Column, noAccount, False, , False) = True
        Catch ex As DBLinkerSQLException
            hadDeleted = False
        End Try

        'Code to Allow user to replace the KP links by another and then delete it
        If isKP AndAlso Not hadDeleted Then
            If replaceKPAccount(noAccount, If(isUsed, isUsedData(0, 0), wontDelMsg)) Then
                Return delAccount(noAccount, isKP)
            End If
        End If

        If hadDeleted Then
            If isKP Then
                deltree(appPath & bar(appPath) & "KP\" & noAccount)
                myMainWin.StatusText = "Compte personne / organisme clé : P/O clé " & personName & " supprimé(e)"

                InternalUpdatesManager.getInstance.sendUpdate("KP-Close(" & noAccount & ")")
            Else
                DBLinker.getInstance.updateDB("KeyPeople", "NoClient=null", "NoClient", noAccount, False)
                deltree(appPath & bar(appPath) & "Client\" & noAccount)
                myMainWin.StatusText = "Compte client : Client " & personName & " supprimé(e)"

                InternalUpdatesManager.getInstance.sendUpdate("Client-Close(" & noAccount & ")")
            End If

            wontDelMsg = ""
        ElseIf isUsed = True Then
            wontDelMsg = isUsedData(0, 0)
        End If

        Return wontDelMsg
    End Function

    Public Sub openTypesUser(ByVal sender As Form)
        'Droit & Accès
        If currentDroitAcces(51) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Types d'utilisateur." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myTypesUser As typesuser = openUniqueWindow(New typesuser(sender))
        myTypesUser.Show()
    End Sub

    Public Function addingComm(ByVal noClient As Integer, ByVal noKP As Integer, ByVal isEnvoie As Boolean, ByVal categorie As String, ByVal sujet As String, ByVal commDate As Date, ByVal remarques As String, Optional ByVal noFolder As Integer = 0, Optional ByVal importingFile As String = "", Optional ByVal useMainConnection As Boolean = True) As Integer
        Dim noComm As Integer = 0
        DBLinker.getInstance.writeDB("Communications", "NoClient,NoKP,IsEnvoie,NoCommSubject,CommDate,NoUser,Remarques,NameOfFile,NoFolder,NoCategorie", noClient & "," & IIf(noKP = 0, "null", noKP) & ",'" & isEnvoie & "'," & DBHelper.addItemToADBList("CommSubjects", "CommSubject", sujet, "NoCommSubject") & ",'" & DateFormat.getTextDate(commDate, DateFormat.TextDateOptions.YYYYMMDD) & "'," & ConnectionsManager.currentUser & ",'" & remarques.Replace("'", "''").Replace(vbCrLf, "<br>") & "','" & importingFile.Replace("'", "''") & "'," & noFolder & "," & DBHelper.addItemToADBList("CommCategories", "Categorie", categorie, "NoCategorie"), , useMainConnection, , noComm)
        myMainWin.StatusText = "Compte client " & noClient & " : Communication ajoutée"

        InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunications(" & noClient & "," & False & ")")

        Return noComm
    End Function

    Public Function addingCommKP(ByVal noKPAccount As Integer, ByVal noKPFrom As Integer, ByVal isEnvoie As Boolean, ByVal categorie As String, ByVal sujet As String, ByVal commDate As Date, ByVal remarques As String, Optional ByVal importingFile As String = "", Optional ByVal useMainConnection As Boolean = True) As Integer
        Dim noComm As Integer = 0
        DBLinker.getInstance.writeDB("CommunicationsKP", "NoKP,NoKPFrom,IsEnvoie,NoCommSubject,CommDate,NoUser,Remarques,NameOfFile,NoCategorie", noKPAccount & "," & IIf(noKPFrom = 0, "null", noKPFrom) & ",'" & isEnvoie & "'," & DBHelper.addItemToADBList("CommSubjects", "CommSubject", sujet, "NoCommSubject") & ",'" & DateFormat.getTextDate(commDate, DateFormat.TextDateOptions.YYYYMMDD) & "'," & ConnectionsManager.currentUser & ",'" & remarques.Replace("'", "''").Replace(vbCrLf, "<br>") & "','" & importingFile.Replace("'", "''") & "'," & DBHelper.addItemToADBList("CommCategories", "Categorie", categorie, "NoCategorie"), , useMainConnection, , noComm)
        myMainWin.StatusText = "Compte personne / organisme clé " & noKPAccount & " : Communication ajoutée"

        InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunicationsKP(" & noKPAccount & "," & False & ")")

        Return noComm
    End Function

    Public Sub addClient(ByVal from As Form)
        CommandsHolder.getInstance.newClient.execute(from)
    End Sub

    Public Sub addKP(ByVal nom As String, ByVal adresse As String, ByVal ville As String, ByVal codePostal As String, ByVal telephones As String, ByVal courriel As String, ByVal url As String, ByVal reference As String, ByVal categorie As String, ByVal noIdent As String, ByVal autresInfos As String, Optional ByVal from As Form = Nothing, Optional ByVal resetFields As Boolean = True)
        'Droit & Accès
        If currentDroitAcces(20) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'ajouter de nouvelles personnes / organismes clé." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myAddKeyPeople As addkeypeople
        If from IsNot Nothing AndAlso from.MdiParent Is Nothing AndAlso from IsNot myMainWin Then
            myAddKeyPeople = New addkeypeople()
        Else
            myAddKeyPeople = openUniqueWindow(New addkeypeople())
        End If
        myAddKeyPeople.nom.Text = nom
        myAddKeyPeople.adresse.Text = adresse
        myAddKeyPeople.ville.Text = ville
        If codePostal <> "" Then
            Try
                myAddKeyPeople.codepostal1.Text = codePostal.Substring(0, 3)
                myAddKeyPeople.codepostal2.Text = codePostal.Substring(3)
            Catch
            End Try
        End If
        Dim myPhones() As Object = telephones.Split(New Char() {"§"})
        myAddKeyPeople.Telephones.Items.AddRange(myPhones)
        myAddKeyPeople.courriel.Text = courriel
        myAddKeyPeople.url.Text = url
        myAddKeyPeople.reference.Text = reference
        myAddKeyPeople.categorie.Text = categorie
        myAddKeyPeople.noident.Text = noIdent
        myAddKeyPeople.ainfo.Text = autresInfos

        myAddKeyPeople.from = from

        myAddKeyPeople.loading(resetFields)
        If from IsNot Nothing AndAlso from.MdiParent Is Nothing And from IsNot myMainWin Then
            myAddKeyPeople.MdiParent = Nothing
            myAddKeyPeople.Visible = False
            myAddKeyPeople.ShowDialog()
        Else
            myAddKeyPeople.Show()
        End If
    End Sub

    Public Sub delVisite(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal noVisite As Integer, ByVal changedDate As Date, Optional ByVal skipFolderDelete As Boolean = False, Optional ByVal noTRP As Integer = 0, Optional ByVal autoSendUpdate As Boolean = True)
        DBLinker.getInstance.delDB("ListeAttente", "NoVisite", noVisite, False, , , , False)

        DBLinker.getInstance.delDB("InfoVisites", "NoVisite", noVisite, False)
        If autoSendUpdate Then InternalUpdatesManager.getInstance.sendUpdate("QueueList()")
        updateVisites(noClient, noFolder, changedDate, , False, noTRP, autoSendUpdate)

        Dim visiteRestant() As String = DBLinker.getInstance.readOneDBField("InfoVisites", "Count(*)", "WHERE ((NoFolder)=" & noFolder & ");")
        If visiteRestant(0) = 0 And skipFolderDelete = False Then
            If MessageBox.Show("Il ne reste aucun rendez-vous pour ce dossier. Désirez-vous le supprimer ?", "Suppression de dossier", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                deleteFolder(noClient, noFolder)
            End If
        End If
    End Sub

    Public Sub openNewRV(Optional ByVal noClient As Integer = 0, Optional ByVal noTRP As Integer = 0, Optional ByVal nam As String = "", Optional ByVal noFolder As Integer = -1, Optional ByVal currentDH As Date = LIMIT_DATE, Optional ByVal qlFrom As Integer = 0, Optional ByVal periode As String = "", Optional ByVal noCodeUnique As Integer = 0)
        'REM_CODES
        'Droit & Accès
        If currentDroitAcces(16) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Ajout d'un rendez-vous." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myAddVisite As addvisite = openUniqueWindow(New addvisite(), "Ajout d'un rendez-vous")
        If noFolder > 0 Then myAddVisite.currentFolder = noFolder

        If nam <> "" Then
            myAddVisite.currentNoClient(nam) = noClient
        Else
            If noClient <> 0 Then myAddVisite.currentNoClient = noClient
        End If
        If noTRP <> 0 Then myAddVisite.currentTRP = UsersManager.getInstance().getUser(noTRP).toString()
        If currentDH <> LIMIT_DATE Then myAddVisite.currentDH = currentDH
        If qlFrom <> 0 Then myAddVisite.qlFrom = qlFrom
        If periode <> "" Then myAddVisite.currentPeriode = periode

        If noCodeUnique <> 0 Then myAddVisite.codeNoUnique = noCodeUnique

        myAddVisite.Show()
        myAddVisite.Select()
    End Sub

    Public Function getNoClientFromnam(ByVal nam As String) As Integer
        Dim noClient() As String = DBLinker.getInstance.readOneDBField("InfoClients", "NoClient", "WHERE NAM='" & nam.Replace("'", "''") & "'")
        If noClient Is Nothing Then Return 0
        If noClient.Length = 0 Then Return 0

        Return noClient(0)
    End Function

    Public Enum CompteType
        Client = 0
        KP = 1
        User = 2
    End Enum

    Public Sub openAccount(ByVal noAccount As Integer, Optional ByVal accountType As CompteType = CompteType.Client)
        Select Case accountType
            Case CompteType.KP
                'Droit & Accès
                If currentDroitAcces(21) = False Then
                    'Message & Exit
                    MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Compte personne / organisme clé." & vbCrLf & "Merci!", "Droit & Accès")
                    Exit Sub
                End If

                Dim myViewModifKeypeople As viewmodifKeyPeople = openUniqueWindow(New viewmodifKeyPeople(), "Personne / Organisme " & noAccount & " :", True)

                If openedNewWindow = True Then myViewModifKeypeople.loading(noAccount)
                If myViewModifKeypeople.IsDisposed = False Then myViewModifKeypeople.Show()

            Case CompteType.User
                'Droit & Accès
                If currentDroitAcces(46) = False Then
                    'Message & Exit
                    MessageBox.Show("Vous n'avez pas le droit de gérer les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
                    Exit Sub
                End If

                Dim myaddmodifusers As New addmodifusers()
                myaddmodifusers.modifUser(noAccount)
                myaddmodifusers.ShowDialog()
            Case Else
                'Droit & Accès
                If currentDroitAcces(10) = False Then
                    'Message & Exit
                    MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Compte client." & vbCrLf & "Merci!", "Droit & Accès")
                    Exit Sub
                End If

                Dim myViewModifClients As viewmodifclients = openUniqueWindow(New viewmodifclients(), "Client " & noAccount & " :", True)
                If openedNewWindow = True Then
                    myViewModifClients.noClient = noAccount
                    myViewModifClients.loading()
                End If
                If myViewModifClients.IsDisposed = False Then myViewModifClients.Show()
        End Select
    End Sub

    Public Function getClientName(ByVal noClient As Integer) As String
        Dim clientName() As String = DBLinker.getInstance.readOneDBField("InfoClients", "Nom+','+Prenom", "WHERE (NoClient=" & noClient & ")")
        If clientName Is Nothing Then Return ""
        If clientName.Length = 0 Then Return ""

        Return clientName(0)
    End Function

    Public Function getKPName(ByVal noKP As Integer) As String
        Dim kpName() As String = DBLinker.getInstance.readOneDBField("KeyPeople", "Nom", "WHERE (NoKP=" & noKP & ")")
        If kpName Is Nothing Then Return ""
        If kpName.Length = 0 Then Return ""

        Return kpName(0)
    End Function

    Public Sub updateVisites(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal changedDate As Date, Optional ByVal sender As Object = Nothing, Optional ByVal updateAccountsComptabilite As Boolean = True, Optional ByVal noTRP As Integer = 0, Optional ByVal autoSendUpdate As Boolean = True)
        If noClient <> 0 Then
            InternalUpdatesManager.getInstance.sendUpdate("AccountsVisites(" & noClient & "," & noFolder & ")")
            If autoSendUpdate AndAlso updateAccountsComptabilite Then
                InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & noClient & ")")
                InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & noClient & ",-1)")
            End If
        End If
        If autoSendUpdate Then InternalUpdatesManager.getInstance.sendUpdate("Agendas(" & DateFormat.getTextDate(changedDate, DateFormat.TextDateOptions.YYYYMMDD_FullTime) & ",False," & noTRP & ")")
    End Sub

    Public Function getLastFolderTRP(ByVal noClient As Integer, Optional ByVal ensureOpened As Boolean = False, Optional ByVal specificFolder As Integer = 0) As String
        Dim i As Integer
        Dim foldersOpened(,), TRP, folder As String

        folder = "" : TRP = ""
        If specificFolder <> 0 Then folder = " AND (NoFolder=" & specificFolder & ")"

        foldersOpened = DBLinker.getInstance.readDB("InfoFolders INNER JOIN Utilisateurs ON Utilisateurs.NoUser = InfoFolders.NoTRPTraitant", "StatutOuvert,Nom + ',' + Prenom + ' (' + CONVERT(nvarchar,NoUser) + ')'", "WHERE (NoClient=" & noClient & ")" & folder & ";")
        If foldersOpened Is Nothing Then Return ""
        If foldersOpened.Length = 0 Then Return ""

        For i = foldersOpened.GetUpperBound(1) To 0 Step -1
            If TRP = "" Then TRP = foldersOpened(1, i)

            If ensureOpened = False Then
                Exit For
            Else
                If CBool(foldersOpened(0, i)) = True Then Exit For
            End If
        Next i

        Return TRP
    End Function

    Public Function randomClient() As String
        Dim alea As New Random
        Dim clientList() As String = DBLinker.getInstance.readOneDBField("InfoClients", "NAM")
        If clientList Is Nothing Then Return "ERROR:NO_CLIENT"
        If clientList.Length = 0 Then Return "ERROR:NO_CLIENT"

        randomClient = clientList(Math.Floor(clientList.GetUpperBound(0) * alea.NextDouble()))
    End Function

    Public Function transferFolder(ByVal noClient As Integer, ByVal noFolder As Integer) As String
        'Droit & Accès
        If currentDroitAcces(89) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de transférer un dossier." & vbCrLf & "Merci!", "Droit & Accès")
            Return "Vous n'avez pas le droit de transférer un dossier." & vbCrLf & "Merci!"
        End If

        Dim lockingAnswer As String = lockFolder(noClient, noFolder, True)
        If lockingAnswer <> "" Then Return lockingAnswer

        Dim lastNbFoundClient As Integer = foundClient.Length
        Dim myRecherche As New clientSearch
        myRecherche.MdiParent = Nothing
        myRecherche.from = Nothing
        myRecherche.Visible = False
        myRecherche.ShowDialog()

        If foundClient.Length = lastNbFoundClient OrElse foundClient(foundClient.GetUpperBound(0)).noClient = noClient Then
            lockFolder(noClient, noFolder, False)
            If foundClient.Length = lastNbFoundClient Then Return "L'utilisateur a annulé la recherche du compte client"
            Return "L'utilisateur a choisi le même compte client"
        End If

        Dim newFolderNoClient As Integer = foundClient(foundClient.GetUpperBound(0)).noClient

        Dim changingScript As String = "UPDATE InfoFolders SET NoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE InfoVisites SET NoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE StatVisites SET NoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE Factures SET NoClient=" & newFolderNoClient & ",ParNoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE StatFactures SET NoClient=" & newFolderNoClient & ",ParNoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE StatPaiements SET NoClient=" & newFolderNoClient & ",ParNoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE StatFolders SET NoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE Prets SET NoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE ListeAttente SET NoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"
        changingScript &= "UPDATE Ventes SET NoClient=" & newFolderNoClient & " WHERE NoFolder=" & noFolder & ";"

        DBLinker.executeSQLScript(changingScript)
        Dim statComments As String = "De " & getClientName(noClient) & " (" & noClient & ") vers " & getClientName(newFolderNoClient) & " (" & newFolderNoClient & ")"
        statComments = statComments.Replace("'", "''")
        DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient, Comments", "27," & noFolder & "," & newFolderNoClient & ",'" & statComments & "'")

        lockFolder(noClient, noFolder, False)

        InternalUpdatesManager.getInstance.sendUpdate("AccountsDossiers(" & noClient & ")")
        InternalUpdatesManager.getInstance.sendUpdate("AccountsDossiers(" & newFolderNoClient & ")")

        If PreferencesManager.getUserPreferences()("OpenClientAccountOnFolderTransfer") = True Then openAccount(newFolderNoClient)
    End Function

    Public Sub addUser()
        'Droit & Accès
        If currentDroitAcces(46) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myaddmodifusers As New addmodifusers()
        myaddmodifusers.addUsers()
        myaddmodifusers.ShowDialog()
    End Sub

    Public Function lockFolder(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal trueFalse As Boolean) As String
        'Vérifie et barre le dossier
        If lockSecteur("ClientFolderEquip-" & noClient & "-" & noFolder & "-", trueFalse, "Dossier d'un client", False) = False Then
            Return "Le dossier " & noFolder & " est déjà en cours de modification par un utilisateur"
        End If
        If lockSecteur("ClientFolderInfos-" & noClient & "-" & noFolder & "-", trueFalse, "Dossier d'un client", False) = False Then
            lockSecteur("ClientFolderEquip-" & noFolder, False)
            Return "Le dossier " & noFolder & " est déjà en cours de modification par un utilisateur"
        End If
        If lockSecteur("ClientFolderText-" & noClient & "-" & noFolder & "-", trueFalse, "Dossier d'un client", False) = False Then
            lockSecteur("ClientFolderEquip-" & noClient & "-" & noFolder & "-", False)
            lockSecteur("ClientFolderInfos-" & noClient & "-" & noFolder & "-", False)
            Return "Le dossier " & noFolder & " est déjà en cours de modification par un utilisateur"
        End If

        Return ""
    End Function

    Public Function deleteFolder(ByVal noClient As Integer, ByVal noFolder As Integer) As String
        Dim lockingAnswer As String = lockFolder(noClient, noFolder, True)
        If lockingAnswer <> "" Then Return lockingAnswer

        Dim skipQL As Boolean
        Dim i As Integer
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim visiteCount(,) As String = DBLinker.getInstance.readDB("InfoVisites", "NoVisite, DateHeure,NoTRP", "WHERE (NoFolder=" & noFolder & ");")

        If Not visiteCount Is Nothing AndAlso visiteCount.Length <> 0 Then
            For i = 0 To visiteCount.GetUpperBound(1)
                If date1Infdate2(CDate(visiteCount(1, i)), Date.Today) Then
                    skipQL = True
                Else
                    skipQL = False
                End If

                removingAgendaEntry(noClient, noFolder, visiteCount(0, i), CDate(visiteCount(1, i)), , True, skipQL, UsersManager.getInstance.getUser(visiteCount(2, i)).toString())
            Next i
        End If

        'Supprime les alertes liés au dossier
        Dim noAlertsToDel(,) As String = DBLinker.getInstance.readDB("SELECT FolderTexteAlerts.NoUserAlert,NoUser FROM FolderTexteAlerts INNER JOIN UsersAlerts ON UsersAlerts.NoUserAlert = FolderTexteAlerts.NoUserAlert WHERE NoFolderTexte IN (SELECT NoFolderTexte FROM FolderTextes Where NoFolder=" & noFolder & ") UNION ALL SELECT FolderAlerts.LastNoUserAlert,UsersAlerts.NoUser FROM FolderAlerts INNER JOIN UsersAlerts ON UsersAlerts.NoUserAlert=FolderAlerts.LastNoUserAlert WHERE NoFolder=" & noFolder)
        DBLinker.getInstance.delDB("FolderTexteAlerts", "NoFolderTexte", "(SELECT NoFolderTexte FROM FolderTextes Where NoFolder=" & noFolder & ")", False, , , , , " IN ")
        If noAlertsToDel IsNot Nothing AndAlso noAlertsToDel.Length <> 0 Then
            Dim noATD(noAlertsToDel.GetUpperBound(1)) As String
            For i = 0 To noAlertsToDel.GetUpperBound(1)
                noATD(i) = noAlertsToDel(0, i)
            Next
            DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", "(" & String.Join(",", noATD) & ")", False, , , , , " IN ")
        End If
        DBLinker.getInstance.delDB("InfoFolders", "NoFolder", noFolder, False)

        'Débarre le dossier
        lockFolder(noClient, noFolder, False)

        InternalUpdatesManager.getInstance.sendUpdate("AccountsDossiers(" & noClient & ")")
        If noAlertsToDel IsNot Nothing AndAlso noAlertsToDel.Length <> 0 Then
            Dim userAlertUpdated As New Generic.List(Of String)
            For i = 0 To noAlertsToDel.GetUpperBound(1)
                If userAlertUpdated.IndexOf(noAlertsToDel(1, i)) = -1 Then
                    AlertsManager.sendUpdate(noAlertsToDel(1, i))
                    userAlertUpdated.Add(noAlertsToDel(1, i))
                End If
            Next
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        Return ""
    End Function

    Public Function askForTRPToTransfer(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal noTRPTraitant As Integer, ByVal noTRPToTransfer As Integer, Optional ByVal choices() As String = Nothing) As Boolean
        If noTRPToTransfer = 0 Then Return False
        If noTRPToTransfer = noTRPTraitant Then Return False

        Dim myChoice1 As String = "", MyChoice2 As String = ""
        If choices Is Nothing Then
            myChoice1 = "OK"
        Else
            myChoice1 = choices(0)
            MyChoice2 = choices(1)
        End If

        Dim myMsgBox As New MsgBox1
        Dim myChoice As Byte = myMsgBox("Le dossier #" & noFolder & " doit être transféré à " & UsersManager.getInstance.getUser(noTRPToTransfer).toString() & " en changeant le thérapeute traitant dans le dossier." & IIf(choices Is Nothing, "", "Que désirez-vous faire ?"), "Thérapeute à transférer", IIf(choices Is Nothing, 1, 2), myChoice1, MyChoice2)

        If myChoice = 1 And choices IsNot Nothing Then openAccount(noClient)
        If myChoice = 2 Then
            AlertsManager.getInstance.addAlert("Le dossier #" & noFolder & " doit être transféré à " & UsersManager.getInstance.getUser(noTRPToTransfer).toString() & " en changeant le thérapeute traitant dans le dossier.", ConnectionsManager.currentUser, AlertsManager.AType.OpenClientAccount, noClient, , , , True)
        End If
        Return True
    End Function

End Module
