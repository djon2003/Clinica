Public Class ClientSearchResult

    Protected Overrides Sub loadImages()
        'Chargement des images
        Me.ajouter.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.delete.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.selectionner.Image = DrawingManager.getInstance.getImage("selection16.gif")
    End Sub

    Protected Overrides Sub doClosingActions()

    End Sub

    Private Sub clientsTrouves_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientsTrouves.DoubleClick
        If selectionner.Enabled = True Then selectionner_Click(sender, e)
    End Sub

    Private Sub clientsTrouves_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ClientsTrouves.KeyDown
        If e.KeyCode = Keys.Enter Then clientsTrouves_DoubleClick(sender, New EventArgs())
    End Sub

    Private Sub clientsTrouves_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ClientsTrouves.RowEnter
        delete.Enabled = True
        selectionner.Enabled = True
        viewFolders.Enabled = True
    End Sub

    Private Sub viewFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles viewFolders.Click
        Dim noClient As Integer = ClientsTrouves.Item(4, ClientsTrouves.currentRow.Index).Value
        'REM_CODES
        Dim results(,) As String = DBLinker.getInstance.readDB("SiteLesion RIGHT OUTER JOIN                       InfoFolders  ON SiteLesion.NoSiteLesion = InfoFolders.NoSiteLesion INNER JOIN Utilisateurs ON Utilisateurs.Nouser=InfoFolders.NoTRPTraitant", "SiteLesion.SiteLesion, InfoFolders.StatutOuvert, InfoFolders.NoCodeUnique, InfoFolders.NoFolder,Utilisateurs.Nom + ',' + Utilisateurs.Prenom + ' (' + CAST(Utilisateurs.NoUser AS VARCHAR(MAX)) + ')' AS TRP", "WHERE ((InfoFolders.NoClient)=" & noClient & ");")

        If results Is Nothing OrElse results.Length = 0 Then
            MessageBox.Show("Il n'existe aucun dossier pour ce compte client", "Aucun dossier")
            Exit Sub
        End If

        Dim myMultiChoice As New multichoice
        Dim statut As String = ""
        Dim dossiers As String = ""
        For i As Integer = 0 To results.GetUpperBound(1)
            If results(1, i) = True Then
                statut = "Actif"
            Else
                statut = "Inactif"
            End If
            dossiers &= "§" & results(3, i) & " - " & results(0, i) & " - " & results(4, i) & " (" & Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNameByNoUnique(results(2, i)) & ") (" & statut & ")"
        Next i
        dossiers = dossiers.Substring(1)
        Dim choice As String = myMultiChoice.GetChoice("Visualisation des dossiers", dossiers, , "§", False)
    End Sub


    Private Sub ajouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ajouter.Click
        Comptes.addClient(Me.ParentForm)
    End Sub

    Private Sub delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete.Click
        'Droit & Accès
        If currentDroitAcces(25) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de supprimer un client." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce compte client ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim delMsg As String = delAccount(ClientsTrouves.Item(4, ClientsTrouves.currentRow.Index).Value)
        If delMsg = "" Then
            Dim curDataRow As DataRow
            'For i As Integer = 0 To DataSetForGrid.Tables(0).Rows.Count - 1
            '    If DataSetForGrid.Tables(0).Rows(i)("# du compte") = ClientsTrouves.currentRow.Cells("# du compte").Value Then curDataRow = DataSetForGrid.Tables(0).Rows(i)
            'Next
            'DataSetForGrid.Tables(0).Rows.Remove(curDataRow)

            'If DataSetForGrid.Tables(0).Rows.Count = 0 Then
            '    delete.Enabled = False
            '    selectionner.Enabled = False
            '    viewFolders.Enabled = False
            'End If
        Else
            MessageBox.Show(delMsg, "Suppression impossible")
        End If
    End Sub

    Private Sub selectionner_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectionner.Click
        ReDim Preserve foundClient(foundClient.Length)
        With foundClient(foundClient.Length - 1)
            .nam = ClientsTrouves.Item(3, ClientsTrouves.currentRow.Index).Value
            .noClient = ClientsTrouves.Item(4, ClientsTrouves.currentRow.Index).Value
            .fullName = ClientsTrouves.Item(0, ClientsTrouves.currentRow.Index).Value
        End With
        Dim canceled As Boolean = False
        REM redirectSearch(from, canceled)
        If Not canceled Then
            If Me.ParentForm.MdiParent Is Nothing OrElse (PreferencesManager.getUserPreferences()("AutoCloseSearchClientOnOpenAccount") = True) Then
                Me.ParentForm.Close()
            End If
        End If
    End Sub

End Class
