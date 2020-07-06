Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Public Class FolderAlertTypesWin
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        'Chargement des images
        With DrawingManager.getInstance
            Me.add.Image = .getImage("ajouter16.gif")
            Me.delete.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.modif.Image = .getImage("modifier16.gif")
            Me.listFolderAlerts.icons.Add(New Icon(.getIcon("default.ico"), New Size(8, 8)))
            Me.Icon = DrawingManager.imageToIcon(.getImage("codedossier.gif"))
        End With
    End Sub

    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents gbFTT As System.Windows.Forms.GroupBox
    Friend WithEvents TypeDateDebut As ManagedCombo
    Friend WithEvents NbPresencesX As ManagedText
    Friend WithEvents DateDebutNbDaysX As ManagedText
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents AlertNbDaysDiff As ManagedText
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents AlertNbDaysForExpiry As ManagedText
    Friend WithEvents listFolderAlerts As CI.Controls.List
    Public WithEvents add As System.Windows.Forms.Button
    Public WithEvents modif As System.Windows.Forms.Button
    Public WithEvents delete As System.Windows.Forms.Button
#End Region

    Private curData As DataTable
    Private oldTypeForMultiple As Integer = 0
    Private loadingFTT As Boolean = False
    Private formModified As Boolean = False
    Private oneTexteModified As Boolean = False
    Private _allowModification As Boolean = False

    Private Property allowModification() As Boolean
        Get
            Return _allowModification
        End Get
        Set(ByVal value As Boolean)
            _allowModification = value

            lockItems(Not value, True)
        End Set
    End Property


#Region "FolderTexteTypes Events"
    Private Sub fenFolderTexteTypes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (formModified OrElse oneTexteModified) Then
            Select Case MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Case System.Windows.Forms.DialogResult.Yes
                    Me.modif_Click(sender, EventArgs.Empty)
                    FolderAlertTypesManager.getInstance.saveAll()
                Case System.Windows.Forms.DialogResult.Cancel
                    e.Cancel = True
                Case System.Windows.Forms.DialogResult.No
                    FolderAlertTypesManager.getInstance.load() 'Ensure all modif revert
            End Select
        End If

        If allowModification AndAlso e.Cancel = False Then lockSecteur("FATs", False)
    End Sub
    Private Sub folderTexteTypes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub fenFolderTexteTypes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        configList(listFolderAlerts)
        loading()
    End Sub
#End Region

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal all As Boolean = False)
        gbFTT.Enabled = Not trueFalse
        Me.modif.Enabled = Not trueFalse
        Me.delete.Enabled = Not trueFalse

        'Use when locking form, when another user already modifying
        If all Then
            add.Enabled = Not trueFalse
            AlertMessage.ReadOnly = trueFalse
            AlertNbDaysDiff.ReadOnly = trueFalse
            AlertNbDaysForExpiry.ReadOnly = trueFalse
            AlertNbPresencesDiff.ReadOnly = trueFalse
            AlertTitle.ReadOnly = trueFalse
            DateDebutNbDaysX.ReadOnly = trueFalse
            DateDebutNbPresencesX.ReadOnly = trueFalse
            NbPresencesX.ReadOnly = True
            TypeDateDebut.ReadOnly = trueFalse
        End If
    End Sub

    Private Sub loading()
        'If lockSecteur had already been done and allowed, skip verification (anyhow this sector is blocked in a whole)
        allowModification = allowModification OrElse lockSecteur("FATs", True, Me.Text, PreferencesManager.getUserPreferences()("AffMSGModif"))

        'REM_CODES
        Dim curFCANo As Integer = 0
        If listFolderAlerts.selected <> -1 Then curFCANo = CType(listFolderAlerts.ItemValueA(listFolderAlerts.selected), FolderAlertType).noFolderAlertType
        listFolderAlerts.cls()
        For Each curFAT As FolderAlertType In FolderAlertTypesManager.getInstance.getItemables()
            Dim n As Integer = listFolderAlerts.add(curFAT.toString)
            listFolderAlerts.ItemValueA(n) = curFAT
            If curFCANo = curFAT.noFolderAlertType Then listFolderAlerts.selected = n
        Next

        formModified = False 'Controls were modified by default text values
        listFolderAlerts.draw = True : listFolderAlerts.draw = False

        If allowModification Then Me.add.Enabled = True
    End Sub

    Private Sub listFolderAlerts_SelectedChange() Handles listFolderAlerts.selectedChange
        'REM_CODES
        If listFolderAlerts.selected = -1 Then
            resetFields()
        Else
            lockItems(False)
            loadingFTT = True

            Dim curFCA As FolderAlertType = listFolderAlerts.ItemValueA(listFolderAlerts.selected)
            AlertTitle.Text = curFCA.alertTitle
            TypeDateDebut.SelectedIndex = curFCA.startingDate_Type
            DateDebutNbPresencesX.Text = curFCA.startingDate_NbPresencesX
            DateDebutNbDaysX.Text = curFCA.startingDate_NbDaysX
            NbPresencesX.Text = curFCA.nbPresencesX
            AlertNbDaysDiff.Text = curFCA.alertNbDaysDiff
            AlertNbPresencesDiff.Text = curFCA.alertNbPresencesDiff
            AlertNbDaysForExpiry.Text = curFCA.alertNbDaysForExpiry
            AlertMessage.Text = curFCA.alertMessage

            loadingFTT = False
        End If

        formModified = False
    End Sub

    Private Sub resetFields()
        lockItems(True)
        Me.AlertTitle.Text = ""
        Me.AlertNbDaysDiff.Text = 0
        Me.DateDebutNbDaysX.Text = 0
        Me.NbPresencesX.Text = 0
        Me.AlertNbDaysForExpiry.Text = 0
    End Sub

    Private Sub add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.onlyAlphabet = True
        myInputBoxPlus.acceptedChars = " §:§-§.§,"
        Dim newName As String = myInputBoxPlus("Veuillez entrer le titre du nouveau message instantanné", "Titre du message instantanné")
        If newName = "" Then Exit Sub

        'REM_CODES
        Dim returning As String = FolderAlertTypesManager.getInstance.addItemable(newName)
        If returning <> "" Then MessageBox.Show(returning, "Impossible d'ajouter un type de message instantanné", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        Dim n As Integer = listFolderAlerts.add(newName)
        listFolderAlerts.ItemValueA(n) = FolderAlertTypesManager.getInstance.getItemable(newName)

        listFolderAlerts.draw = True : listFolderAlerts.draw = False
        Me.listFolderAlerts.selected = n
        oneTexteModified = True
    End Sub

    Private Sub modif_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles modif.Click
        If listFolderAlerts.selected = -1 Then Exit Sub

        'REM_CODES
        Dim sameNameItem As Integer = listFolderAlerts.findStringExact(AlertTitle.Text, False)
        If sameNameItem <> -1 AndAlso sameNameItem <> listFolderAlerts.selected Then MessageBox.Show("Le nom entré existe déjà pour un autre type de texte. Veuillez en choisir un autre.", "Impossible de modifier le type de texte", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

        Dim curFAT As FolderAlertType = listFolderAlerts.ItemValueA(listFolderAlerts.selected)
        listFolderAlerts.ItemText(listFolderAlerts.selected) = AlertTitle.Text
        curFAT.alertTitle = AlertTitle.Text
        curFAT.startingDate_Type = TypeDateDebut.SelectedIndex
        curFAT.startingDate_NbPresencesX = DateDebutNbPresencesX.Text
        curFAT.startingDate_NbDaysX = DateDebutNbDaysX.Text
        curFAT.nbPresencesX = NbPresencesX.Text
        curFAT.alertNbDaysDiff = AlertNbDaysDiff.Text
        curFAT.alertNbPresencesDiff = AlertNbPresencesDiff.Text
        curFAT.alertNbDaysForExpiry = AlertNbDaysForExpiry.Text
        curFAT.alertMessage = AlertMessage.Text

        formModified = False
        Me.oneTexteModified = True
    End Sub

    Private Sub delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click
        If listFolderAlerts.selected = -1 Then Exit Sub
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce(s) type(s) de message instantanné ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        'REM_CODES
        Dim errors As New Generic.List(Of FolderAlertType)
        Dim nbToDelete As Integer = 0
        For i As Integer = listFolderAlerts.listCount - 1 To 0 Step -1
            If listFolderAlerts.items(i).IsSelected Then
                Dim curFAT As FolderAlertType = listFolderAlerts.ItemValueA(i)
                Try
                    curFAT.delete()

                    listFolderAlerts.remove(i)
                    nbToDelete += 1
                Catch ex As DBItemableUndeletable
                    errors.Add(ex.dbItemables(0))
                End Try
            End If
        Next

        If errors.Count <> 0 Then
            Dim plural As String = If(errors.Count = 1, "", "s")
            Dim msg As String = "Le" & plural & " type" & plural & " de message instantanné ci-dessous n'" & If(errors.Count = 1, "a", "ont") & " pu être supprimé" & plural & ", car il" & plural & " " & If(errors.Count = 1, "est", "sont") & " en cours d'utilisation :"
            For Each curErr As FolderAlertType In errors
                msg &= vbCrLf & curErr.alertTitle
            Next

            MessageBox.Show(msg, "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        If nbToDelete <> 0 Then
            listFolderAlerts.draw = True : listFolderAlerts.draw = False
            oneTexteModified = True
        End If
    End Sub

    Private Sub typeDateDebut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeDateDebut.SelectedIndexChanged
        'REM_CODES
        If TypeDateDebut.SelectedIndex = FolderAlertType.StartingDateTypes.OnPresenceX Then
            DateDebutNbPresencesX.Enabled = True
            DateDebutNbPresencesX.blockOnMinimum = True
            DateDebutNbPresencesX.Text = 1
        Else
            DateDebutNbPresencesX.Enabled = False
            DateDebutNbPresencesX.blockOnMinimum = False
            DateDebutNbPresencesX.Text = 0
        End If

    End Sub

    Private Sub texts_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NbPresencesX.TextChanged, DateDebutNbDaysX.TextChanged, AlertNbDaysDiff.TextChanged, AlertNbDaysForExpiry.TextChanged, DateDebutNbPresencesX.TextChanged, AlertTitle.TextChanged, AlertNbPresencesDiff.TextChanged, AlertMessage.TextChanged
        formModified = True
    End Sub

    Private Sub lists_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeDateDebut.SelectedIndexChanged
        formModified = True
    End Sub

    Private Sub listFolderTexteTypes_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles listFolderAlerts.willSelect
        If listFolderAlerts.selected <> -1 AndAlso formModified = True AndAlso MessageBox.Show("Désirez-vous accepter les modifications ?", "Modification", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.modif_Click(sender, EventArgs.Empty)
        End If
    End Sub

    Private Sub showAlarm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        formModified = True
    End Sub
End Class
