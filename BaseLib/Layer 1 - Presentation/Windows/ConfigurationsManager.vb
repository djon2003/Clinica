Namespace Windows.Forms


    Public Class ConfigurationsManager

        Private nbConfigs As Integer = 0
        Private configs As New List(Of ConfigBase)
        Private hasToConfig As Boolean = False
        Private formModified As Boolean = False
        Private isSaved As Boolean = False

        Public Sub New()

            ' Cet appel est requis par le Concepteur Windows Form.
            InitializeComponent()

            ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End Sub

        Public Sub addConfig(ByVal config As ConfigBase)
            nbConfigs += 1

            Dim curPage As ConfigurationsPage = Me.TabConfig1
            If nbConfigs <> 1 Then
                curPage = New ConfigurationsPage()
                curPage.Name = "TabConfig" & nbConfigs
                TabConfigs.TabPages.Add(curPage)
            End If

            curPage.load(config)
            configs.Add(config)
            config.saveState()
            hasToConfig = hasToConfig OrElse config.hasToUpdate
        End Sub

        Public Sub addConfigs(ByVal configs As List(Of ConfigBase))
            If configs Is Nothing Then Exit Sub

            For Each curConfig As ConfigBase In configs
                addConfig(curConfig)
            Next
        End Sub

        Private Sub save()
            For Each curConfig As ConfigBase In configs
                curConfig.save()
            Next

            isSaved = True
        End Sub

        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Try
                save()
            Catch
                'TODO : Shall have a specific error
                'Save error...field incorrect 
                Exit Sub
            End Try

            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private Sub ConfigurationsManager_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            If isSaved = False AndAlso hasToConfig AndAlso MessageBox.Show("Êtes-vous sûr de vouloir fermer la gestion des configurations ?" & vbCrLf & "Les configurations doivent être enregistrer pour démarrer le logiciel.", "Confirmation de fermeture", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            End If

            For Each curConfig As ConfigBase In configs
                curConfig.reloadPreviousState()
            Next
        End Sub
    End Class


End Namespace