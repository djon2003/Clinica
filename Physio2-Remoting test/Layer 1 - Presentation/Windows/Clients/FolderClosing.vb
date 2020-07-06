Imports CI.Clinica.Accounts.Clients.Folders.Codifications
Imports CI.Clinica.Accounts.Clients.Folders.ClientFolder
Imports CI.Clinica.Accounts.Clients.Folders.FoldersStatus

Friend Class FolderClosing

    Private changingChecks As Boolean = False
    Private isClosing As Boolean = False

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        Me.MdiParent = myMainWin
        Me.selectDate.Image = DrawingManager.getInstance.getImage("selection16.gif")

        loadForm()
    End Sub

    Private Sub closeFolders()
        Dim codesIds As New Generic.List(Of String)
        Dim trpsIds As New Generic.List(Of String)

        'Fill selected ids
        For i As Integer = 0 To therapists.Items.Count - 1
            If therapists.GetItemChecked(i) Then trpsIds.Add(CType(therapists.Items(i), User).noUser)
        Next i
        For i As Integer = 0 To codifications.Items.Count - 1
            If codifications.GetItemChecked(i) Then codesIds.Add(FolderCodesManager.getInstance().getNoUniqueByCodeName(codifications.Items(i)))
        Next i

        'Check if all mandatory fields
        If trpsIds.Count = 0 Then MessageBox.Show("Veuillez sélectionner au moins un thérapeute", "Thérapeute manquant") : Exit Sub
        If codesIds.Count = 0 Then MessageBox.Show("Veuillez sélectionner au moins une codification dossier", "Codification manquante") : Exit Sub
        If selectedDate.Tag Is Nothing Then MessageBox.Show("Veuillez sélectionner une date", "Date manquante") : Exit Sub
        If closingComments.Text = String.Empty Then MessageBox.Show("Veuillez saisir une raison pour la désactivation", "Raison manquante") : closingComments.Focus() : Exit Sub

        Dim closingComment As String = closingComments.Text
        Dim useLastRvDate As Boolean = closingDateType.SelectedIndex = 1

        progress.Value = 0
        lockForm(True)

        'Load data
        Dim where As String = "StatutOuvert = 1 AND LastDate <= '" & selectedDate.Text & "'"
        If Not allTRP.Checked Then where &= " AND NoTrpTraitant IN (" & String.Join(",", trpsIds.ToArray()) & ")"
        If Not allCodes.Checked Then where &= " AND NoCodeUnique IN (" & String.Join(",", codesIds.ToArray()) & ")"
        where &= " ORDER BY Client"

        Dim foldersToClose As DataSet = DBLinker.getInstance().readDBForGrid("FolderDates if1 INNER JOIN InfoClients ic ON ic.NoClient = if1.NoClient", "Nom + ',' + Prenom AS [Client], NoFolder AS [# dossier], 'Non traité' AS [Résultat], if1.NoClient, LastTraitement", where)

        'Close all folders demanded
        With foldersToClose.Tables(0)
            If .Rows.Count = 0 Then
                MessageBox.Show("Aucun dossier à désactiver", "Désactivation de dossiers", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                lockForm(False)
                Exit Sub
            End If

            If MessageBox.Show("Êtes-vous sûr de vouloir désactiver en lot les " & .Rows.Count & " dossiers correspondant aux options choisies ?", "Confirmation de désactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then
                lockForm(False)
                Exit Sub
            End If

            progress.Maximum = .Rows.Count
            For i As Integer = 0 To .Rows.Count - 1
                Dim noClient As Integer = .Rows(i)("NoClient")
                Dim noFolder As Integer = .Rows(i)("# dossier")
                Dim statusDate As Date = Date.Now
                If useLastRvDate Then statusDate = .Rows(i)("LastTraitement")

                Dim statusChange As New FolderStatusChange(FolderPossibleStatuses.Active, FolderPossibleStatuses.Inactive, noClient, noFolder, closingComment, statusDate)
                Try
                    changeStatus(statusChange, True)
                    .Rows(i)("Résultat") = "Désactivé"
                Catch ex As Exception
                    .Rows(i)("Résultat") = ex.Message
                End Try

                progress.Value = i
                ToolTip1.SetToolTip(progress, i & " / " & .Rows.Count)
                Application.DoEvents()
            Next i
        End With

        'Generate report
        ReportGeneration.startRapportGeneration("Désactivation de dossiers en lot", Nothing, foldersToClose.Tables(0))

        lockForm(False)
    End Sub

    Private Sub loadForm()
        'Load lists
        Dim codes As Generic.List(Of String) = FolderCodesManager.getInstance().getCodeNames()
        codes.Sort()
        codifications.Items.AddRange(codes.ToArray())
        therapists.Items.AddRange(UsersManager.getInstance().getUsers(, True, False).ToArray())

        closingDateType.SelectedIndex = 0
    End Sub

    Private Function lockForm(ByVal isLocked As Boolean) As Object
        allCodes.Enabled = Not isLocked
        allTRP.Enabled = Not isLocked
        therapists.Enabled = Not isLocked
        codifications.Enabled = Not isLocked
        selectDate.Enabled = Not isLocked
        selectedDate.Enabled = Not isLocked
        btnClose.Enabled = Not isLocked
        closingComments.ReadOnly = isLocked
        closingDateType.Enabled = Not isLocked
        progress.Visible = isLocked
        isClosing = isLocked

        Return Nothing
    End Function

    Private Sub selectAll(ByVal checksList As CheckedListBox, ByVal isChecked As Boolean)
        If changingChecks Then Exit Sub

        changingChecks = True
        For i As Integer = 0 To checksList.Items.Count - 1
            checksList.SetItemChecked(i, isChecked)
        Next i
        changingChecks = False
    End Sub

    Private Sub changeAll(ByVal checksList As CheckedListBox, ByVal checkBox As CheckBox, ByVal indexChecked As Integer, ByVal indexValue As Boolean)
        If changingChecks Then Exit Sub

        changingChecks = True

        Dim allChecked As Boolean = indexValue
        If indexValue Then
            For i As Integer = 0 To checksList.Items.Count - 1
                If indexChecked <> i AndAlso Not checksList.GetItemChecked(i) Then
                    allChecked = False
                    Exit For
                End If
            Next i
        End If

        checkBox.Checked = allChecked
        changingChecks = False
    End Sub

    Private Sub allCodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles allCodes.CheckedChanged
        selectAll(codifications, allCodes.Checked)
    End Sub

    Private Sub allTRP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles allTRP.CheckedChanged
        selectAll(therapists, allTRP.Checked)
    End Sub

    Private Sub therapists_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles therapists.ItemCheck
        changeAll(therapists, allTRP, e.Index, e.NewValue)
    End Sub

    Private Sub codifications_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles codifications.ItemCheck
        changeAll(codifications, allCodes, e.Index, e.NewValue)
    End Sub

    Private Sub selectDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectDate.Click
        Dim myDateChoice As New DateChoice()
        Dim chosenDate As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Now.Year)
        If chosenDate.Count <> 0 Then
            selectedDate.Tag = chosenDate(0)
            selectedDate.Text = DateFormat.getTextDate(chosenDate(0))
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeFolders()
    End Sub

    Private Sub FolderClosing_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If isClosing Then
            e.Cancel = True
            MessageBox.Show("Désactivation en lot en cours de processus. Veuillez attendre la fin.", "Impossible de fermer la fenêtre", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As Base.DataInternalUpdate)

    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property
End Class