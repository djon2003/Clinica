Namespace Accounts.Clients.Folders
    Partial Public Class RVs

        Public Class RendezVousManager
            Inherits ManagerBase(Of RendezVousManager)

            Private Const tableName As String = "InfoFolders INNER JOIN InfoVisites INNER JOIN InfoClients ON InfoClients.NoClient = InfoVisites.NoClient ON InfoFolders.NoFolder = InfoVisites.NoFolder"
            Private Const fieldsToSelect As String = "InfoVisites.IsOnAgenda,InfoVisites.Flagged,InfoClients.Telephones,InfoClients.Description as RemarquesClient,InfoFolders.Remarques AS RemarquesFolder,InfoFolders.Frequence as FolderFrequency,InfoFolders.NoCodeUnique as FolderNoCodeUnique,InfoFolders.NoCodeUser as FolderNoCodeUser,InfoFolders.NoCodeDate as FolderNoCodeDate,InfoVisites.Service,InfoVisites.Remarques AS RemarquesRV,InfoVisites.Evaluation,InfoVisites.IsAnnounced,InfoVisites.NoVisite,InfoVisites.Confirmed,InfoVisites.NoFacture,InfoClients.Nom + ',' + InfoClients.Prenom AS ItemText,InfoVisites.DateHeure, InfoVisites.NoFolder,InfoVisites.NoStatut, InfoVisites.NoClient, InfoVisites.NoTRP, InfoVisites.Periode, CASE WHEN (SELECT COUNT(Factures.NoFacture) FROM Factures WHERE Factures.NoFacture = InfoVisites.NoFacture AND IsSouffrance=0)=0 THEN 1 ELSE 0 END AS IsBillPaid, CASE WHEN InfoVisites.NoFacture=0 THEN 0 ELSE (SELECT IsSouffrance FROM Factures WHERE Factures.NoFacture=InfoVisites.NoFacture) END AS IsBillSouffrance"

            Protected Sub New()
                MyBase.New()
            End Sub

            Public Function addRendezVous(ByVal vDate As Date, ByVal vTime As Date, ByVal vPeriode As Short, ByVal vNoTRP As Integer, ByVal vNoClient As Integer, ByVal vNoFolder As Integer, ByVal vService As String, Optional ByVal vFrequence As Short = -1, Optional ByVal vEval As Boolean = False, Optional ByVal noTRPFrom As Integer = 0, Optional ByVal confirmed As Boolean = False) As Integer
                'REM_CODES
                'Droit & Accès
                If currentDroitAcces(16) = False Then
                    'Message & Exit
                    MessageBox.Show("Vous n'avez pas le droit d'ajouter un rendez-vous." & vbCrLf & "Merci!", "Droit & Accès")
                    Exit Function
                End If

                Dim touchQL As Boolean = False
                Dim selfOpened As Boolean = False
                If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

                'Ensure RV can be added upon some verifications
                Dim rvAccepted As Boolean = RendezVousAcceptor.isAccepted(vNoClient, vNoFolder, vDate, vNoTRP, vFrequence, noTRPFrom, 0)
                Dim myCode As Folders.Codifications.FolderCode = RendezVousAcceptor.lastFolderCode
                If Not rvAccepted Then
                    If selfOpened = True Then DBLinker.getInstance().dbConnected = False
                    Return 0
                End If

                Dim noVisite As Integer = 0
                Try
                    'Écrit la visite
                    vDate = New Date(vDate.Year, vDate.Month, vDate.Day, vTime.Hour, vTime.Minute, 0)
                    If DBLinker.getInstance.writeDB("InfoVisites", "NoClient, NoFolder, NoStatut, NoFacture, NoTRP, DateHeure, Periode, Service, Evaluation,Confirmed, ExternalStatus", vNoClient & "," & vNoFolder & ",3,null," & vNoTRP & ",'" & DateFormat.getTextDate(vDate, DateFormat.TextDateOptions.YYYYMMDD) & " " & DateFormat.getTextDate(vDate, DateFormat.TextDateOptions.ShortTime) & "'," & vPeriode & ",'" & vService & "','" & vEval & "','" & confirmed & "'," & myCode.startingExternalStatus, False, , , noVisite) = False Then
                        MessageBox.Show("Une erreur est survenu lors de l'ajout de la visite. Veuillez recommencer s'il vous plait.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return 0
                    End If
                    myMainWin.StatusText = "Ajout d'un rendez-vous le " & DateFormat.getTextDate(vDate, DateFormat.TextDateOptions.YYYYMMDD) & " à " & DateFormat.getTextDate(vTime, DateFormat.TextDateOptions.ShortTime) & " dans l'agenda de " & UsersManager.getInstance.getUser(vNoTRP).toString

                Catch ex As Exception
                    addErrorLog(ex)
                    MessageBox.Show("Une erreur est survenu lors de l'ajout de la visite. Veuillez recommencer s'il vous plait.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return 0
                End Try

                DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite", "6," & vNoFolder & "," & vNoClient & "," & noVisite)

                'Gestion de la liste d'attente
                If Integer.Parse(PreferencesManager.getGeneralPreferences()("NbJourForAutoQL")) > 0 AndAlso date1Infdate2(vDate, Date.Today.AddDays(PreferencesManager.getGeneralPreferences()("NbJourForAutoQL"))) = False Then
                    touchQL = ListeAttente.addToListeAttente(vNoClient, vNoFolder, noVisite, vNoTRP, vPeriode, vDate)
                End If

                If CType(PreferencesManager.getGeneralPreferences()("AutoDelQLAfterNewRV"), Boolean) = True Then
                    Dim infoFolder(,) As String = DBLinker.getInstance.readDB("InfoFolders", "NoCodeUnique,Service", "WHERE (NoFolder)=" & vNoFolder & ";")

                    DBLinker.getInstance.delDB("ListeAttente", "NoClient", vNoClient & " AND (NoFolder) IS NULL AND NoVisite IS NULL AND Service='" & infoFolder(1, 0) & "' AND NoCodeUnique = " & infoFolder(0, 0), False, , , , False)
                    touchQL = True
                End If
                If touchQL Then InternalUpdatesManager.getInstance.sendUpdate("QueueList()")
                If selfOpened = True Then DBLinker.getInstance().dbConnected = False

                Return noVisite
            End Function

            Public Function loadRendezVous(ByVal noVisite As Integer) As RendezVous
                Dim entries As Generic.List(Of RendezVous) = loadRendezVous("WHERE Infovisites.NoVisite=" & noVisite)
                If entries.Count = 0 Then Return Nothing

                Return entries(0)
            End Function

            Public Function loadRendezVous(ByVal noClient As Integer, ByVal [from] As Date, Optional ByVal [to] As Date = LIMIT_DATE, Optional ByVal getAbsences As Boolean = True, Optional ByVal noFolder As Integer = 0) As Generic.List(Of RendezVous)
                Dim folder As String = ""
                If noFolder <> 0 Then folder = " AND InfoVisites.NoFolder=" & noFolder
                Dim whereTo As String = ""
                If [to] <> LIMIT_DATE Then whereTo = " AND DateHeure<'" & DateFormat.getTextDate([to]) & "'"
                Dim absences As String = ""
                If getAbsences = False Then absences = "InfoVisites.NoStatut>2 AND "

                Return loadRendezVous("WHERE " & absences & "Infovisites.NoClient=" & noClient & " AND DateHeure>='" & DateFormat.getTextDate(from) & "'" & whereTo & folder & " ORDER BY DateHeure;")
            End Function

            Private Function loadRendezVous(ByVal whereAndOrder As String) As Generic.List(Of RendezVous)
                Dim visites As DataSet = DBLinker.getInstance.readDBForGrid(tableName, fieldsToSelect, whereAndOrder)
                Dim entries As New Generic.List(Of RendezVous)
                If visites Is Nothing OrElse visites.Tables.Count = 0 Then Return entries

                For i As Integer = 0 To visites.Tables(0).Rows.Count - 1
                    entries.Add(New RendezVous(New DBItemableData(visites.Tables(0).Rows(i))))
                Next i

                Return entries
            End Function
        End Class

    End Class
End Namespace
