Public Class Form1

    Private Sub fichierLogsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FichierLogsToolStripMenuItem.Click
        import(ErrorsImporter.Jobs.Logs)
    End Sub

    Private Sub emailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailToolStripMenuItem.Click
        import(ErrorsImporter.Jobs.Mails)
    End Sub

    Private Sub import(ByVal jobToDo As ErrorsImporter.Jobs)
        Dim typeQuestion As DialogResult = MessageBox.Show("Désirez-vous choisir un dossier (OUI) ou un fichier (NON) ?", "Choix du type d'importation", MessageBoxButtons.YesNoCancel)
        If typeQuestion = Windows.Forms.DialogResult.Cancel Then Exit Sub

        'REM Check if clear should be there.. now, good for dev
        ErrorsManager.getInstance.clearErrors()

        Select Case typeQuestion
            Case Windows.Forms.DialogResult.Yes
                importFolder(jobToDo)
            Case Windows.Forms.DialogResult.No
                importFile(jobToDo)
        End Select
    End Sub

    Private Sub importFile(ByVal jobToDo As ErrorsImporter.Jobs)
        OpenFileDialog1.InitialDirectory = "C:\DropBox\CI\Projects\Physio2-Remoting test\§ Utils\§ ErrorLog To check"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ErrorsImporter.getInstance.jobsToDo = jobToDo
            ErrorsImporter.getInstance.import(OpenFileDialog1.FileName)
        End If
    End Sub


    Private Sub importFolder(ByVal jobToDo As ErrorsImporter.Jobs)
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.SelectedPath = "C:\DropBox\CI\Projects\Physio2-Remoting test\§ Utils\§ ErrorLog To check\Errors"
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ErrorsImporter.getInstance.jobsToDo = jobToDo
            ErrorsImporter.getInstance.import(FolderBrowserDialog1.SelectedPath, False)
        End If
    End Sub

    Private Sub tousToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TousToolStripMenuItem.Click
        import(ErrorsImporter.Jobs.All)
    End Sub

    Public Sub new()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        propertiesOfErrors.SelectedObject = ErrorsManager.getInstance()
    End Sub

    Private Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub EnvoyerParCourrielToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnvoyerParCourrielToolStripMenuItem.Click
        If MessageBox.Show("Êtes-vous certain de vouloir envoyer les erreurs par courriel ? (Génére un courriel par erreur)", "Confirmation", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then Exit Sub

        ErrorsManager.getInstance.sendErrorsByEmail()

        MessageBox.Show("Emails sent!")
    End Sub

    Private Sub ÉcrireEnFichiersEMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ÉcrireEnFichiersEMLToolStripMenuItem.Click
        Dim myDir As String = InputBox("Veuillez entrer le dossier de sortie", "Dossier de sortie", "C:\DropBox\CI\Projects\Physio2-Remoting test\§ Utils\§ ErrorLog To check\Errors-Out")
        If myDir = "" Then Exit Sub

        ErrorsManager.getInstance.writeErrorsToEmail(myDir)
        MessageBox.Show("Emails wrote!")
    End Sub
End Class
