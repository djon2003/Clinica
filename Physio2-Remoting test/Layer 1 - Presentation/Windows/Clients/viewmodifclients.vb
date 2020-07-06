Imports CI.Clinica.Accounts.Clients.Folders.RVs
Imports CI.Clinica.Accounts.Clients.Folders
Imports CI.Clinica.Accounts.Clients.Folders.FoldersStatus
Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Friend Class viewmodifclients
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        saveantecedent.Enabled = False
        maximise2.Enabled = False
        Me.reference.Tag = ""
        If CurrentDroitAcces(64) = True Then
            Me.menuDossierFlagged.Visible = True
            Me.menuVisiteFlagged.Visible = True
        End If

        Me.Top = 0
        Me.Left = 0
        Me.StartStopCommChanging.Visible = False
        Me.StartStopBillChanging.Visible = False

        'Settings du user pour la fenêtre
        Dim setting As String = UsersManager.currentUser.settings.accountEquipmentStyle
        If setting <> "" Then ThisSettings = setting.Split(New Char() {"§"})

        Me.MdiParent = MyMainWin
        Me.sexe = New BaseObjArray()
        Me.sexe.Add(Me._sexe_0)
        Me.sexe.Add(Me._sexe_1)
        Me.menudossierstatut = New BaseObjArray()
        Me.menudossierstatut.Add(Me.menustatutOuvert)
        Me.menudossierstatut.Add(Me.menustatutFerme)
        If CurrentUserName = "Administrateur" Then AdminPanel.Visible = True

        'LegendePanel
        Me.LegendePanel = New Clinica.AccountLegend()
        Me.LegendePanel.AbsenceMColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(192, Byte))
        Me.LegendePanel.AbsenceNMColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(128, Byte))
        Me.LegendePanel.Bordered = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LegendePanel.Caption = "Légende"
        Me.LegendePanel.EndingPosition = 0
        Me.LegendePanel.Location = New System.Drawing.Point(0, -40)
        Me.LegendePanel.MovingSpeed = 1
        Me.LegendePanel.Name = "LegendePanel"
        Me.LegendePanel.PresenceColor = System.Drawing.Color.White
        Me.LegendePanel.RVColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.LegendePanel.StartingPosition = -60
        Me.LegendePanel.TabIndex = 144
        LegendePanel.AbsenceMColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors7"))
        LegendePanel.AbsenceNMColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors5"))
        LegendePanel.PresenceColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors4"))
        LegendePanel.PresencePayeeColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorPresencePayee"))
        LegendePanel.RVColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors2"))
        Me._ongletsclient_TabPage2.Controls.Add(LegendePanel)
        Me.LegendePanel.BringToFront()

        'Ajout de la boîte de facturation
        CurFactureBox = New FacturationBox(False, False, False, FacturationBox.DedicatedType.Client)
        CurFactureBox.Locked = True
        '
        'CurFactureBox
        '
        Me.CurFactureBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.CurFactureBox.BackColor = System.Drawing.SystemColors.Control
        Me.CurFactureBox.Location = New System.Drawing.Point(13, 466)
        Me.CurFactureBox.MontantFacture = 0.0!
        Me.CurFactureBox.MontantRestant = 0.0!
        Me.CurFactureBox.Name = "CurFactureBox"
        Me.CurFactureBox.NegativePaiement = False
        Me.CurFactureBox.NoFacture = 0
        Me.CurFactureBox.SetNom = ""
        Me.CurFactureBox.Size = New System.Drawing.Size(536, 130)
        Me.CurFactureBox.TabIndex = 0
        Me.CurFactureBox.ValueA = Nothing
        Me.CurFactureBox.ValueB = Nothing
        Me._ongletsclient_TabPage3.Controls.Add(Me.CurFactureBox)


        'Préparation des boîtes de texte WebTextControl
        antecedants.EditorWidth = 552
        antecedants.EditorHeight = 468
        antecedants.EditorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")
        antecedants.AncreActif = True
        antecedants.Ancre = PreferencesManager.getGeneralPreferences()("Ancre")
        folderText.EditorHeight = 380
        folderText.EditorWidth = 535
        folderText.EditorURL = PreferencesManager.getGeneralPreferences()("HTMLEditorPath")
        folderText.AncreActif = True
        folderText.Ancre = PreferencesManager.getGeneralPreferences()("Ancre")

        'Chargement des images
        IMGModifSave = New ImageList()
        With DrawingManager.GetInstance
            Try
                IMGModifSave.Images.Add(.GetImage("modifier16.gif"))
                IMGModifSave.Images.Add(.GetImage("save.jpg"))
                IMGModifSave.Images.Add(.GetImage("stopmodif16.gif"))
            Catch
            End Try

            Dim delImage As Image = DrawingManager.IconToImage(.GetIcon("delete16.ico"), New Size(16, 16))
            Me.enleverphoto.Image = delImage
            Me.choosephoto.Image = DrawingManager.IconToImage(.GetIcon("modifphoto16.ico"), New Size(16, 16))
            Me.modifsaveTextes.Image = IMGModifSave.Images(0)
            Me.modifsaveDossierInfos.Image = IMGModifSave.Images(0)
            Me.modifsaveEquip.Image = IMGModifSave.Images(0)
            Me.maximise1.Image = .GetImage("FDehors.jpg")
            Me.modelebtn1.Image = .GetImage("modeles16.gif")
            Me.viderComm.Image = .GetImage("eraser.jpg")
            Me.importComm.Image = DrawingManager.IconToImage(.GetIcon("import16.ico"), New Size(16, 16))
            Me.listeCommunications.Icons.Add(.GetIcon("import16.ico"))
            Me.delComm.Image = DelImage
            Me.modifComm.Image = IMGModifSave.Images(1)
            Me.btnAddComm.Image = .GetImage("ajouter16.gif")
            Me.selectCommDate.Image = .GetImage("selection16.gif")
            Me.selectKeyPeople.Image = .GetImage("selection16.gif")
            Me.vider.Image = .GetImage("eraser.jpg")
            Me.delete.Image = DelImage
            Me.modif.Image = IMGModifSave.Images(1)
            Me.maximise2.Image = .GetImage("FDehors.jpg")
            Me.saveantecedent.Image = IMGModifSave.Images(1)
            Me.modelebtn2.Image = .GetImage("modeles16.gif")
            Me.StartStopBillChanging.Image = IMGModifSave.Images(0)
            Me.StartStopCommChanging.Image = IMGModifSave.Images(0)
            Me.FilterBills.Image = .GetImage("selection16.gif")
            Me.modifenable.Image = IMGModifSave.Images(0)
            Me.Photo.Image = .GetImage("NoPhoto.jpg")
            Me.paiements.Image = .GetImage("paiement16.gif")
            Me.DownTel.Image = .GetImage("DownArrow.jpg")
            Me.UpTel.Image = .GetImage("UpArrow.jpg")
            Me.AddTel.Image = .GetImage("ajouter16.gif")
            Me.DelTel.Image = DelImage
            Me.ModifTel.Image = IMGModifSave.Images(0)
            Me.selectionner.Image = .GetImage("selection16.gif")
            Me.btnAddAsKP.Image = DrawingManager.IconToImage(.GetIcon("NewKP16.ico"), New Size(16, 16))
            Me.btnAddAlert.Image = .GetImage("alarme16.gif")
            Me.createBill.Image = .GetImage("newBill16.jpg")
            Me.Icon = .GetIcon("client16.ico")
            Me.VisitesList.Icons.Add(DrawingManager.getInstance.GetIcon("Confirmation.ico"))
            Me.FermerDossierHisto.Image = .getImage("exitbutton7.gif")
            Me.btnSendEmail.Image = .getImage("send16.gif")
            Me.createEquipmentReceipt.Image = .getImage("createrecu.gif")
        End With
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If Disposing Then
            If Not folderText Is Nothing Then
                folderText = Nothing
            End If
            If Not components Is Nothing Then
                components.Dispose()
            End If
            If MyPhoto IsNot Nothing Then MyPhoto.Dispose()
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private WithEvents curFactureBox As Clinica.FacturationBox
    Public WithEvents enleverphoto As System.Windows.Forms.Button
    Public WithEvents choosephoto As System.Windows.Forms.Button
    Public WithEvents saveantecedent As System.Windows.Forms.Button
    Public WithEvents modelebtn2 As System.Windows.Forms.Button
    Public WithEvents _ongletsclient_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents dossiersVlist As System.Windows.Forms.ComboBox
    Public WithEvents _ongletsclient_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents ongletsclient As System.Windows.Forms.TabControl
    Public WithEvents modifenable As System.Windows.Forms.Button
    Public WithEvents photo As System.Windows.Forms.PictureBox
    Public WithEvents sexe As BaseObjArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents menuviewmodifclientsdossier As System.Windows.Forms.ContextMenu
    Public WithEvents menudelete As System.Windows.Forms.MenuItem
    Public WithEvents menustatutOuvert As System.Windows.Forms.MenuItem
    Public WithEvents menustatutFerme As System.Windows.Forms.MenuItem
    Public WithEvents menudossierstatut As BaseObjArray
    Friend WithEvents paiements As System.Windows.Forms.Button
    Public WithEvents lblCommDeA As System.Windows.Forms.Label
    Public WithEvents label23 As System.Windows.Forms.Label
    Public WithEvents label25 As System.Windows.Forms.Label
    Public WithEvents commDeA As ManagedCombo
    Public WithEvents commSujet As ManagedCombo
    Public WithEvents panel2 As System.Windows.Forms.Panel
    Public WithEvents commType2 As System.Windows.Forms.RadioButton
    Public WithEvents commType1 As System.Windows.Forms.RadioButton
    Public WithEvents label26 As System.Windows.Forms.Label
    Public WithEvents commDate As System.Windows.Forms.Label
    Public WithEvents label27 As System.Windows.Forms.Label
    Public WithEvents commUser As System.Windows.Forms.Label
    Public WithEvents label28 As System.Windows.Forms.Label
    Friend WithEvents CommRemarques As ManagedText
    Public WithEvents btnAddComm As System.Windows.Forms.Button
    Public WithEvents modifComm As System.Windows.Forms.Button
    Public WithEvents viderComm As System.Windows.Forms.Button
    Public WithEvents delComm As System.Windows.Forms.Button
    Public WithEvents importComm As System.Windows.Forms.Button
    Public WithEvents commEnvoie As System.Windows.Forms.CheckBox
    Public WithEvents commReception As System.Windows.Forms.CheckBox
    Public WithEvents label29 As System.Windows.Forms.Label
    Public WithEvents commFiltrage As System.Windows.Forms.ComboBox
    Public WithEvents selectKeyPeople As System.Windows.Forms.Button
    Public WithEvents selectCommDate As System.Windows.Forms.Button
    Friend WithEvents menuviewmodifclientscommunications As System.Windows.Forms.ContextMenu
    Friend WithEvents menuImportFromOutside As System.Windows.Forms.MenuItem
    Friend WithEvents menuImportFromDB As System.Windows.Forms.MenuItem
    Public WithEvents listeCommunications As CI.Controls.List
    Public WithEvents visitesList As CI.Controls.List
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RefMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents menuRefAutre As System.Windows.Forms.MenuItem
    Friend WithEvents menuRefCompte As System.Windows.Forms.MenuItem
    Friend WithEvents menuRefKP As System.Windows.Forms.MenuItem
    Public WithEvents maximise2 As System.Windows.Forms.Button
    Friend WithEvents btnAddAsKP As System.Windows.Forms.Button
    Friend WithEvents LegendeTimer As System.Windows.Forms.Timer
    Friend WithEvents AdminPanel As System.Windows.Forms.Panel
    Friend WithEvents AdminCloseBtn As System.Windows.Forms.Button
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents AdminInterval As System.Windows.Forms.TextBox
    Friend WithEvents LegendePanel As Clinica.AccountLegend
    Friend WithEvents menuclickRV As System.Windows.Forms.ContextMenu
    Friend WithEvents menuacopier As System.Windows.Forms.MenuItem
    Friend WithEvents menuaenlever As System.Windows.Forms.MenuItem
    Friend WithEvents menuSeparator As System.Windows.Forms.MenuItem
    Friend WithEvents menuUpQL As System.Windows.Forms.MenuItem
    Friend WithEvents menuAddToQueueList As System.Windows.Forms.MenuItem
    Friend WithEvents menuQueueList As System.Windows.Forms.MenuItem
    Friend WithEvents menumodifstatus As System.Windows.Forms.MenuItem
    Friend WithEvents menupresent As System.Windows.Forms.MenuItem
    Friend WithEvents menuabsentmotive As System.Windows.Forms.MenuItem
    Friend WithEvents menuabsentnonmotive As System.Windows.Forms.MenuItem
    Friend WithEvents menueffstatus As System.Windows.Forms.MenuItem
    Friend WithEvents AdminButton1 As System.Windows.Forms.Button
    Friend WithEvents AdminButton2 As System.Windows.Forms.Button
    Public WithEvents _Label1_16 As System.Windows.Forms.Label
    Friend WithEvents AddTel As System.Windows.Forms.Button
    Friend WithEvents ModifTel As System.Windows.Forms.Button
    Friend WithEvents DelTel As System.Windows.Forms.Button
    Friend WithEvents Telephones As Clinica.ManagedCombo
    Public WithEvents _Label1_4 As System.Windows.Forms.Label
    Public WithEvents ville As Clinica.ManagedCombo
    Public WithEvents selectionner As System.Windows.Forms.Button
    Friend WithEvents reference As System.Windows.Forms.TextBox
    Public WithEvents url As ManagedText
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents courriel As ManagedText
    Public WithEvents label3 As System.Windows.Forms.Label
    Public WithEvents prenom As ManagedText
    Public WithEvents remarques As ManagedText
    Public WithEvents nam As ManagedText
    Public WithEvents autrenom As ManagedText
    Public WithEvents nom As ManagedText
    Public WithEvents adresse As ManagedText
    Public WithEvents codepostal2 As ManagedText
    Public WithEvents codepostal1 As ManagedText
    Public WithEvents _Label1_17 As System.Windows.Forms.Label
    Public WithEvents _Label1_15 As System.Windows.Forms.Label
    Public WithEvents _Label1_14 As System.Windows.Forms.Label
    Public WithEvents _Label1_11 As System.Windows.Forms.Label
    Public WithEvents _Label1_8 As System.Windows.Forms.Label
    Public WithEvents _Label1_7 As System.Windows.Forms.Label
    Public WithEvents _Label1_6 As System.Windows.Forms.Label
    Public WithEvents _Label1_5 As System.Windows.Forms.Label
    Public WithEvents _Label1_0 As System.Windows.Forms.Label
    Public WithEvents _Label1_1 As System.Windows.Forms.Label
    Public WithEvents _Label1_2 As System.Windows.Forms.Label
    Public WithEvents _Label1_3 As System.Windows.Forms.Label
    Public WithEvents _Label2_2 As System.Windows.Forms.Label
    Public WithEvents metierslist As Clinica.ManagedCombo
    Public WithEvents employeurslist As Clinica.ManagedCombo
    Public WithEvents _sexe_1 As System.Windows.Forms.RadioButton
    Public WithEvents _sexe_0 As System.Windows.Forms.RadioButton
    Public WithEvents annee As Clinica.ManagedCombo
    Public WithEvents mois As Clinica.ManagedCombo
    Public WithEvents jour As Clinica.ManagedCombo
    Friend WithEvents DownTel As System.Windows.Forms.Button
    Friend WithEvents UpTel As System.Windows.Forms.Button
    Friend WithEvents dsVentePret As System.Data.DataSet
    Friend WithEvents MenuLine1 As System.Windows.Forms.MenuItem
    Friend WithEvents DataTable1 As System.Data.DataTable
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    Friend WithEvents DataColumn2 As System.Data.DataColumn
    Friend WithEvents DataColumn3 As System.Data.DataColumn
    Friend WithEvents DataColumn4 As System.Data.DataColumn
    Friend WithEvents DataColumn5 As System.Data.DataColumn
    Friend WithEvents DataColumn6 As System.Data.DataColumn
    Friend WithEvents DataColumn7 As System.Data.DataColumn
    Friend WithEvents DataColumn8 As System.Data.DataColumn
    Friend WithEvents DataColumn9 As System.Data.DataColumn
    Friend WithEvents DataColumn10 As System.Data.DataColumn
    Friend WithEvents DataColumn11 As System.Data.DataColumn
    Friend WithEvents DataColumn12 As System.Data.DataColumn
    Friend WithEvents DataColumn13 As System.Data.DataColumn
    Friend WithEvents menuPreRefList As System.Windows.Forms.MenuItem
    Public WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents publipostage As Clinica.ManagedCombo
    Friend WithEvents antecedants As Clinica.WebTextControl
    Friend WithEvents AdminTimer As System.Windows.Forms.Label
    Friend WithEvents _ongletsclient_TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents StartStopBillChanging As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents FilterBills As System.Windows.Forms.Button
    Friend WithEvents DateA As System.Windows.Forms.Label
    Friend WithEvents DateDe As System.Windows.Forms.Label
    Friend WithEvents ChoixA As System.Windows.Forms.Button
    Friend WithEvents DateAll As System.Windows.Forms.CheckBox
    Friend WithEvents ChoixDe As System.Windows.Forms.Button
    Friend WithEvents GroupComm As System.Windows.Forms.GroupBox
    Friend WithEvents StartStopCommChanging As System.Windows.Forms.Button
    Friend WithEvents _ongletsclient_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents dossiersClist As ManagedCombo
    Public WithEvents label22 As System.Windows.Forms.Label
    Friend WithEvents menuViewModifClient As System.Windows.Forms.MenuStrip
    Friend WithEvents menuComm As ContextMenuItem
    Friend WithEvents EnregistrerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImporterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OuvrirLeFichierJointToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SupprimerLeFichierJointToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SupprimerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAddAlert As System.Windows.Forms.Button
    Friend WithEvents addAlertMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CompteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RapportAuMédecinToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuDossierFlagged As System.Windows.Forms.MenuItem
    Friend WithEvents menuVisiteFlagged As System.Windows.Forms.MenuItem
    Friend WithEvents menuStatutDemande As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuDemandeNonTransmise As System.Windows.Forms.MenuItem
    Friend WithEvents menuDemandeEnvoyee As System.Windows.Forms.MenuItem
    Friend WithEvents menuDemandeAcceptee As System.Windows.Forms.MenuItem
    Friend WithEvents menuDemandeRefusee As System.Windows.Forms.MenuItem
    Friend WithEvents createBill As System.Windows.Forms.Button
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuSuiviDemande As System.Windows.Forms.MenuItem
    Public WithEvents facturesView As DataGridPlus
    Friend WithEvents DateF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoFacture As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents menuGenFolderRapport As System.Windows.Forms.MenuItem
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents visiteHistory As DataGridPlus
    Public WithEvents _ongletsclient_TabPage0 As System.Windows.Forms.TabPage
    Friend WithEvents folderTexts As Clinica.ManagedCombo
    Public WithEvents modifsaveTextes As System.Windows.Forms.Button
    Public WithEvents maximise1 As System.Windows.Forms.Button
    Public WithEvents modelebtn1 As System.Windows.Forms.Button
    Public WithEvents dossier As System.Windows.Forms.ListBox
    Public WithEvents ongletsdossier As System.Windows.Forms.TabControl
    Public WithEvents _ongletsdossier_TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents listEquipement As DataGridPlus
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Public WithEvents vider As System.Windows.Forms.Button
    Public WithEvents delete As System.Windows.Forms.Button
    Public WithEvents backPret As System.Windows.Forms.Button
    Public WithEvents submit As System.Windows.Forms.Button
    Public WithEvents modif As System.Windows.Forms.Button
    Public WithEvents frameE1 As System.Windows.Forms.GroupBox
    Public WithEvents eDescription As System.Windows.Forms.TextBox
    Public WithEvents panel1 As System.Windows.Forms.Panel
    Public WithEvents eType2 As System.Windows.Forms.RadioButton
    Public WithEvents eType1 As System.Windows.Forms.RadioButton
    Public WithEvents frameE2 As System.Windows.Forms.GroupBox
    Public WithEvents eVerified As System.Windows.Forms.CheckBox
    Public WithEvents eReturned As System.Windows.Forms.CheckBox
    Public WithEvents eDuree As Clinica.ManagedCombo
    Public WithEvents eSignature As System.Windows.Forms.PictureBox
    Public WithEvents eNotes As ManagedText
    Public WithEvents anretour As Clinica.ManagedCombo
    Public WithEvents moisretour As Clinica.ManagedCombo
    Public WithEvents jourretour As Clinica.ManagedCombo
    Public WithEvents eDepot As ManagedText
    Public WithEvents label20 As System.Windows.Forms.Label
    Public WithEvents ePret As ManagedText
    Public WithEvents label19 As System.Windows.Forms.Label
    Public WithEvents eRefund As ManagedText
    Public WithEvents label13 As System.Windows.Forms.Label
    Public WithEvents label11 As System.Windows.Forms.Label
    Public WithEvents eRefunded As System.Windows.Forms.CheckBox
    Public WithEvents label18 As System.Windows.Forms.Label
    Public WithEvents label12 As System.Windows.Forms.Label
    Public WithEvents refundlabel As System.Windows.Forms.Label
    Public WithEvents label10 As System.Windows.Forms.Label
    Public WithEvents label9 As System.Windows.Forms.Label
    Public WithEvents label8 As System.Windows.Forms.Label
    Public WithEvents eProfit As ManagedText
    Public WithEvents label16 As System.Windows.Forms.Label
    Public WithEvents eTotal As ManagedText
    Public WithEvents label17 As System.Windows.Forms.Label
    Public WithEvents label15 As System.Windows.Forms.Label
    Public WithEvents label14 As System.Windows.Forms.Label
    Public WithEvents etrp As Clinica.ManagedCombo
    Public WithEvents eNoItem As Clinica.ManagedCombo
    Public WithEvents eNom As Clinica.ManagedCombo
    Public WithEvents eDate As System.Windows.Forms.TextBox
    Public WithEvents label7 As System.Windows.Forms.Label
    Public WithEvents label6 As System.Windows.Forms.Label
    Public WithEvents label5 As System.Windows.Forms.Label
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents _ongletsdossier_TabPage0 As System.Windows.Forms.TabPage
    Friend WithEvents folderText As Clinica.WebTextControl
    Friend WithEvents StyleType As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StyleNumerodelitem As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StyleFacture As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StyleTherapeute As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StyleItem As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StyleDate As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents menuRVRemarques As System.Windows.Forms.MenuItem
    Friend WithEvents menuRVRemarkDel As System.Windows.Forms.MenuItem
    Friend WithEvents menuRVService As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents menuTransferFolder As System.Windows.Forms.MenuItem
    Friend WithEvents menuAnnonceClient As System.Windows.Forms.MenuItem
    Friend WithEvents menuConfirmRV As System.Windows.Forms.MenuItem
    Friend WithEvents menuPrintRecu As System.Windows.Forms.MenuItem
    Friend WithEvents menuRVtypes As System.Windows.Forms.MenuItem
    Friend WithEvents menuRVEval As System.Windows.Forms.MenuItem
    Friend WithEvents menuRVTraitement As System.Windows.Forms.MenuItem
    Friend WithEvents menuRefAucun As System.Windows.Forms.MenuItem
    Friend WithEvents folderHistoryFrame As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents folderHistory As DataGridPlus
    Friend WithEvents FermerDossierHisto As System.Windows.Forms.Button
    Friend WithEvents menuShowDossierHisto As System.Windows.Forms.MenuItem
    Public WithEvents label31 As System.Windows.Forms.Label
    Public WithEvents commCategorie As Clinica.ManagedCombo
    Public WithEvents commFiltrageCat As System.Windows.Forms.ComboBox
    Public WithEvents label32 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents modifsaveEquip As System.Windows.Forms.Button
    Public WithEvents modifsaveDossierInfos As System.Windows.Forms.Button
    Friend WithEvents _ongletsdossier_TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents ctlFolderInfos As Clinica.FolderInfos
    Friend WithEvents menuChangeRVPeriod As System.Windows.Forms.MenuItem
    Friend WithEvents btnSendEmail As System.Windows.Forms.Button
    Public WithEvents colorReceived As System.Windows.Forms.Label
    Public WithEvents colorSent As System.Windows.Forms.Label
    Public WithEvents createEquipmentReceipt As System.Windows.Forms.Button
    Friend WithEvents menuExportFolder As System.Windows.Forms.MenuItem
    Friend WithEvents menuTransferRV As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(viewmodifclients))
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.menustatutOuvert = New System.Windows.Forms.MenuItem
        Me.menustatutFerme = New System.Windows.Forms.MenuItem
        Me.enleverphoto = New System.Windows.Forms.Button
        Me.choosephoto = New System.Windows.Forms.Button
        Me.ongletsclient = New System.Windows.Forms.TabControl
        Me._ongletsclient_TabPage1 = New System.Windows.Forms.TabPage
        Me.antecedants = New CI.Clinica.WebTextControl
        Me.maximise2 = New System.Windows.Forms.Button
        Me.saveantecedent = New System.Windows.Forms.Button
        Me.modelebtn2 = New System.Windows.Forms.Button
        Me._ongletsclient_TabPage4 = New System.Windows.Forms.TabPage
        Me.GroupComm = New System.Windows.Forms.GroupBox
        Me.dossiersClist = New CI.Clinica.ManagedCombo
        Me.panel2 = New System.Windows.Forms.Panel
        Me.commType2 = New System.Windows.Forms.RadioButton
        Me.commType1 = New System.Windows.Forms.RadioButton
        Me.viderComm = New System.Windows.Forms.Button
        Me.modifComm = New System.Windows.Forms.Button
        Me.btnAddComm = New System.Windows.Forms.Button
        Me.listeCommunications = New CI.Controls.List
        Me.CommRemarques = New CI.Base.ManagedText
        Me.label28 = New System.Windows.Forms.Label
        Me.importComm = New System.Windows.Forms.Button
        Me.commFiltrageCat = New System.Windows.Forms.ComboBox
        Me.commFiltrage = New System.Windows.Forms.ComboBox
        Me.commUser = New System.Windows.Forms.Label
        Me.lblCommDeA = New System.Windows.Forms.Label
        Me.label27 = New System.Windows.Forms.Label
        Me.label31 = New System.Windows.Forms.Label
        Me.label23 = New System.Windows.Forms.Label
        Me.delComm = New System.Windows.Forms.Button
        Me.colorReceived = New System.Windows.Forms.Label
        Me.colorSent = New System.Windows.Forms.Label
        Me.label32 = New System.Windows.Forms.Label
        Me.label29 = New System.Windows.Forms.Label
        Me.commDate = New System.Windows.Forms.Label
        Me.label25 = New System.Windows.Forms.Label
        Me.selectCommDate = New System.Windows.Forms.Button
        Me.commDeA = New CI.Clinica.ManagedCombo
        Me.label22 = New System.Windows.Forms.Label
        Me.label26 = New System.Windows.Forms.Label
        Me.commReception = New System.Windows.Forms.CheckBox
        Me.commEnvoie = New System.Windows.Forms.CheckBox
        Me.commCategorie = New CI.Clinica.ManagedCombo
        Me.selectKeyPeople = New System.Windows.Forms.Button
        Me.commSujet = New CI.Clinica.ManagedCombo
        Me._ongletsclient_TabPage3 = New System.Windows.Forms.TabPage
        Me.facturesView = New CI.Base.Windows.Forms.DataGridPlus
        Me.DateF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PPO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PU = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoFacture = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.FilterBills = New System.Windows.Forms.Button
        Me.DateA = New System.Windows.Forms.Label
        Me.DateDe = New System.Windows.Forms.Label
        Me.ChoixA = New System.Windows.Forms.Button
        Me.DateAll = New System.Windows.Forms.CheckBox
        Me.ChoixDe = New System.Windows.Forms.Button
        Me._ongletsclient_TabPage0 = New System.Windows.Forms.TabPage
        Me.folderHistoryFrame = New System.Windows.Forms.Panel
        Me.FermerDossierHisto = New System.Windows.Forms.Button
        Me.Label30 = New System.Windows.Forms.Label
        Me.folderHistory = New CI.Base.Windows.Forms.DataGridPlus
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.modifsaveTextes = New System.Windows.Forms.Button
        Me.modifsaveEquip = New System.Windows.Forms.Button
        Me.modifsaveDossierInfos = New System.Windows.Forms.Button
        Me.maximise1 = New System.Windows.Forms.Button
        Me.modelebtn1 = New System.Windows.Forms.Button
        Me.dossier = New System.Windows.Forms.ListBox
        Me.folderTexts = New CI.Clinica.ManagedCombo
        Me.ongletsdossier = New System.Windows.Forms.TabControl
        Me._ongletsdossier_TabPage4 = New System.Windows.Forms.TabPage
        Me.listEquipement = New CI.Base.Windows.Forms.DataGridPlus
        Me.vider = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.backPret = New System.Windows.Forms.Button
        Me.submit = New System.Windows.Forms.Button
        Me.createEquipmentReceipt = New System.Windows.Forms.Button
        Me.modif = New System.Windows.Forms.Button
        Me.frameE1 = New System.Windows.Forms.GroupBox
        Me.eDescription = New System.Windows.Forms.TextBox
        Me.panel1 = New System.Windows.Forms.Panel
        Me.eType2 = New System.Windows.Forms.RadioButton
        Me.eType1 = New System.Windows.Forms.RadioButton
        Me.frameE2 = New System.Windows.Forms.GroupBox
        Me.eVerified = New System.Windows.Forms.CheckBox
        Me.eReturned = New System.Windows.Forms.CheckBox
        Me.eDuree = New CI.Clinica.ManagedCombo
        Me.eSignature = New System.Windows.Forms.PictureBox
        Me.eNotes = New CI.Base.ManagedText
        Me.anretour = New CI.Clinica.ManagedCombo
        Me.moisretour = New CI.Clinica.ManagedCombo
        Me.jourretour = New CI.Clinica.ManagedCombo
        Me.eDepot = New CI.Base.ManagedText
        Me.label20 = New System.Windows.Forms.Label
        Me.ePret = New CI.Base.ManagedText
        Me.label19 = New System.Windows.Forms.Label
        Me.eRefund = New CI.Base.ManagedText
        Me.label13 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me.eRefunded = New System.Windows.Forms.CheckBox
        Me.label18 = New System.Windows.Forms.Label
        Me.label12 = New System.Windows.Forms.Label
        Me.refundlabel = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.label9 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.eProfit = New CI.Base.ManagedText
        Me.label16 = New System.Windows.Forms.Label
        Me.eTotal = New CI.Base.ManagedText
        Me.label17 = New System.Windows.Forms.Label
        Me.label15 = New System.Windows.Forms.Label
        Me.label14 = New System.Windows.Forms.Label
        Me.etrp = New CI.Clinica.ManagedCombo
        Me.eNoItem = New CI.Clinica.ManagedCombo
        Me.eNom = New CI.Clinica.ManagedCombo
        Me.eDate = New System.Windows.Forms.TextBox
        Me.label7 = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me._ongletsdossier_TabPage1 = New System.Windows.Forms.TabPage
        Me.ctlFolderInfos = New CI.Clinica.FolderInfos
        Me._ongletsdossier_TabPage0 = New System.Windows.Forms.TabPage
        Me.folderText = New CI.Clinica.WebTextControl
        Me._ongletsclient_TabPage2 = New System.Windows.Forms.TabPage
        Me.Label24 = New System.Windows.Forms.Label
        Me.visiteHistory = New CI.Base.Windows.Forms.DataGridPlus
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.visitesList = New CI.Controls.List
        Me.dossiersVlist = New System.Windows.Forms.ComboBox
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.StartStopBillChanging = New System.Windows.Forms.Button
        Me.DataTable1 = New System.Data.DataTable
        Me.DataColumn1 = New System.Data.DataColumn
        Me.DataColumn2 = New System.Data.DataColumn
        Me.DataColumn3 = New System.Data.DataColumn
        Me.DataColumn4 = New System.Data.DataColumn
        Me.DataColumn5 = New System.Data.DataColumn
        Me.DataColumn6 = New System.Data.DataColumn
        Me.DataColumn7 = New System.Data.DataColumn
        Me.DataColumn8 = New System.Data.DataColumn
        Me.DataColumn9 = New System.Data.DataColumn
        Me.DataColumn10 = New System.Data.DataColumn
        Me.DataColumn11 = New System.Data.DataColumn
        Me.DataColumn12 = New System.Data.DataColumn
        Me.DataColumn13 = New System.Data.DataColumn
        Me.modifenable = New System.Windows.Forms.Button
        Me.photo = New System.Windows.Forms.PictureBox
        Me.menuviewmodifclientsdossier = New System.Windows.Forms.ContextMenu
        Me.MenuLine1 = New System.Windows.Forms.MenuItem
        Me.menuShowDossierHisto = New System.Windows.Forms.MenuItem
        Me.menuGenFolderRapport = New System.Windows.Forms.MenuItem
        Me.menudelete = New System.Windows.Forms.MenuItem
        Me.menuTransferFolder = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.menuStatutDemande = New System.Windows.Forms.MenuItem
        Me.menuDemandeNonTransmise = New System.Windows.Forms.MenuItem
        Me.menuDemandeEnvoyee = New System.Windows.Forms.MenuItem
        Me.menuDemandeAcceptee = New System.Windows.Forms.MenuItem
        Me.menuDemandeRefusee = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.menuSuiviDemande = New System.Windows.Forms.MenuItem
        Me.menuDossierFlagged = New System.Windows.Forms.MenuItem
        Me.paiements = New System.Windows.Forms.Button
        Me.menuviewmodifclientscommunications = New System.Windows.Forms.ContextMenu
        Me.menuImportFromOutside = New System.Windows.Forms.MenuItem
        Me.menuImportFromDB = New System.Windows.Forms.MenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAddAsKP = New System.Windows.Forms.Button
        Me.AddTel = New System.Windows.Forms.Button
        Me.ModifTel = New System.Windows.Forms.Button
        Me.DelTel = New System.Windows.Forms.Button
        Me.selectionner = New System.Windows.Forms.Button
        Me.DownTel = New System.Windows.Forms.Button
        Me.UpTel = New System.Windows.Forms.Button
        Me.StartStopCommChanging = New System.Windows.Forms.Button
        Me.annee = New CI.Clinica.ManagedCombo
        Me.mois = New CI.Clinica.ManagedCombo
        Me.jour = New CI.Clinica.ManagedCombo
        Me.btnAddAlert = New System.Windows.Forms.Button
        Me.createBill = New System.Windows.Forms.Button
        Me.btnSendEmail = New System.Windows.Forms.Button
        Me.RefMenu = New System.Windows.Forms.ContextMenu
        Me.menuRefAucun = New System.Windows.Forms.MenuItem
        Me.menuRefAutre = New System.Windows.Forms.MenuItem
        Me.menuRefCompte = New System.Windows.Forms.MenuItem
        Me.menuPreRefList = New System.Windows.Forms.MenuItem
        Me.menuRefKP = New System.Windows.Forms.MenuItem
        Me.LegendeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.AdminPanel = New System.Windows.Forms.Panel
        Me.AdminTimer = New System.Windows.Forms.Label
        Me.AdminButton2 = New System.Windows.Forms.Button
        Me.AdminButton1 = New System.Windows.Forms.Button
        Me.AdminInterval = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.AdminCloseBtn = New System.Windows.Forms.Button
        Me.menuclickRV = New System.Windows.Forms.ContextMenu
        Me.menuacopier = New System.Windows.Forms.MenuItem
        Me.menuaenlever = New System.Windows.Forms.MenuItem
        Me.menuSeparator = New System.Windows.Forms.MenuItem
        Me.menuAnnonceClient = New System.Windows.Forms.MenuItem
        Me.menuConfirmRV = New System.Windows.Forms.MenuItem
        Me.menuPrintRecu = New System.Windows.Forms.MenuItem
        Me.menuUpQL = New System.Windows.Forms.MenuItem
        Me.menuAddToQueueList = New System.Windows.Forms.MenuItem
        Me.menuQueueList = New System.Windows.Forms.MenuItem
        Me.menuChangeRVPeriod = New System.Windows.Forms.MenuItem
        Me.menuRVService = New System.Windows.Forms.MenuItem
        Me.menumodifstatus = New System.Windows.Forms.MenuItem
        Me.menupresent = New System.Windows.Forms.MenuItem
        Me.menuabsentmotive = New System.Windows.Forms.MenuItem
        Me.menuabsentnonmotive = New System.Windows.Forms.MenuItem
        Me.menueffstatus = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.menuRVRemarques = New System.Windows.Forms.MenuItem
        Me.menuRVRemarkDel = New System.Windows.Forms.MenuItem
        Me.menuTransferRV = New System.Windows.Forms.MenuItem
        Me.menuRVtypes = New System.Windows.Forms.MenuItem
        Me.menuRVEval = New System.Windows.Forms.MenuItem
        Me.menuRVTraitement = New System.Windows.Forms.MenuItem
        Me.menuVisiteFlagged = New System.Windows.Forms.MenuItem
        Me._Label1_16 = New System.Windows.Forms.Label
        Me._Label1_4 = New System.Windows.Forms.Label
        Me.reference = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me._Label1_17 = New System.Windows.Forms.Label
        Me._Label1_15 = New System.Windows.Forms.Label
        Me._Label1_14 = New System.Windows.Forms.Label
        Me._Label1_11 = New System.Windows.Forms.Label
        Me._Label1_8 = New System.Windows.Forms.Label
        Me._Label1_7 = New System.Windows.Forms.Label
        Me._Label1_6 = New System.Windows.Forms.Label
        Me._Label1_5 = New System.Windows.Forms.Label
        Me._Label1_0 = New System.Windows.Forms.Label
        Me._Label1_1 = New System.Windows.Forms.Label
        Me._Label1_2 = New System.Windows.Forms.Label
        Me._Label1_3 = New System.Windows.Forms.Label
        Me._Label2_2 = New System.Windows.Forms.Label
        Me._sexe_1 = New System.Windows.Forms.RadioButton
        Me._sexe_0 = New System.Windows.Forms.RadioButton
        Me.dsVentePret = New System.Data.DataSet
        Me.label21 = New System.Windows.Forms.Label
        Me.publipostage = New CI.Clinica.ManagedCombo
        Me.Telephones = New CI.Clinica.ManagedCombo
        Me.ville = New CI.Clinica.ManagedCombo
        Me.url = New CI.Base.ManagedText
        Me.courriel = New CI.Base.ManagedText
        Me.prenom = New CI.Base.ManagedText
        Me.remarques = New CI.Base.ManagedText
        Me.nam = New CI.Base.ManagedText
        Me.autrenom = New CI.Base.ManagedText
        Me.nom = New CI.Base.ManagedText
        Me.adresse = New CI.Base.ManagedText
        Me.codepostal2 = New CI.Base.ManagedText
        Me.codepostal1 = New CI.Base.ManagedText
        Me.metierslist = New CI.Clinica.ManagedCombo
        Me.employeurslist = New CI.Clinica.ManagedCombo
        Me.menuViewModifClient = New System.Windows.Forms.MenuStrip
        Me.menuComm = New CI.Clinica.ContextMenuItem
        Me.EnregistrerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImporterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OuvrirLeFichierJointToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SupprimerLeFichierJointToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SupprimerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.addAlertMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CompteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RapportAuMédecinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StyleType = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StyleNumerodelitem = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StyleFacture = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StyleTherapeute = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StyleItem = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StyleDate = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.menuExportFolder = New System.Windows.Forms.MenuItem
        Me.ongletsclient.SuspendLayout()
        Me._ongletsclient_TabPage1.SuspendLayout()
        Me._ongletsclient_TabPage4.SuspendLayout()
        Me.GroupComm.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me._ongletsclient_TabPage3.SuspendLayout()
        CType(Me.facturesView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me._ongletsclient_TabPage0.SuspendLayout()
        Me.folderHistoryFrame.SuspendLayout()
        CType(Me.folderHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ongletsdossier.SuspendLayout()
        Me._ongletsdossier_TabPage4.SuspendLayout()
        CType(Me.listEquipement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameE1.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.frameE2.SuspendLayout()
        CType(Me.eSignature, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._ongletsdossier_TabPage1.SuspendLayout()
        Me._ongletsdossier_TabPage0.SuspendLayout()
        Me._ongletsclient_TabPage2.SuspendLayout()
        CType(Me.visiteHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.photo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AdminPanel.SuspendLayout()
        CType(Me.dsVentePret, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuViewModifClient.SuspendLayout()
        Me.addAlertMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'menustatutOuvert
        '
        Me.menustatutOuvert.Index = 0
        Me.menustatutOuvert.RadioCheck = True
        Me.menustatutOuvert.Text = "Actif"
        '
        'menustatutFerme
        '
        Me.menustatutFerme.Index = 1
        Me.menustatutFerme.RadioCheck = True
        Me.menustatutFerme.Text = "Inactif"
        '
        'enleverphoto
        '
        Me.enleverphoto.BackColor = System.Drawing.SystemColors.Control
        Me.enleverphoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.enleverphoto.Enabled = False
        Me.enleverphoto.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.enleverphoto.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.enleverphoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.enleverphoto.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.enleverphoto.Location = New System.Drawing.Point(8, 462)
        Me.enleverphoto.Name = "enleverphoto"
        Me.enleverphoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.enleverphoto.Size = New System.Drawing.Size(24, 24)
        Me.enleverphoto.TabIndex = 33
        Me.enleverphoto.TabStop = False
        Me.ToolTip1.SetToolTip(Me.enleverphoto, "Enlever la photo")
        Me.enleverphoto.UseVisualStyleBackColor = False
        '
        'choosephoto
        '
        Me.choosephoto.BackColor = System.Drawing.SystemColors.Control
        Me.choosephoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.choosephoto.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.choosephoto.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.choosephoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.choosephoto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.choosephoto.Location = New System.Drawing.Point(8, 438)
        Me.choosephoto.Name = "choosephoto"
        Me.choosephoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.choosephoto.Size = New System.Drawing.Size(24, 24)
        Me.choosephoto.TabIndex = 32
        Me.choosephoto.TabStop = False
        Me.ToolTip1.SetToolTip(Me.choosephoto, "Choisir la photo")
        Me.choosephoto.UseVisualStyleBackColor = False
        '
        'ongletsclient
        '
        Me.ongletsclient.Controls.Add(Me._ongletsclient_TabPage1)
        Me.ongletsclient.Controls.Add(Me._ongletsclient_TabPage4)
        Me.ongletsclient.Controls.Add(Me._ongletsclient_TabPage3)
        Me.ongletsclient.Controls.Add(Me._ongletsclient_TabPage0)
        Me.ongletsclient.Controls.Add(Me._ongletsclient_TabPage2)
        Me.ongletsclient.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ongletsclient.ItemSize = New System.Drawing.Size(42, 18)
        Me.ongletsclient.Location = New System.Drawing.Point(367, 0)
        Me.ongletsclient.Name = "ongletsclient"
        Me.ongletsclient.SelectedIndex = 0
        Me.ongletsclient.Size = New System.Drawing.Size(569, 624)
        Me.ongletsclient.TabIndex = 35
        '
        '_ongletsclient_TabPage1
        '
        Me._ongletsclient_TabPage1.Controls.Add(Me.antecedants)
        Me._ongletsclient_TabPage1.Controls.Add(Me.maximise2)
        Me._ongletsclient_TabPage1.Controls.Add(Me.saveantecedent)
        Me._ongletsclient_TabPage1.Controls.Add(Me.modelebtn2)
        Me._ongletsclient_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._ongletsclient_TabPage1.Name = "_ongletsclient_TabPage1"
        Me._ongletsclient_TabPage1.Size = New System.Drawing.Size(561, 598)
        Me._ongletsclient_TabPage1.TabIndex = 1
        Me._ongletsclient_TabPage1.Text = "Bilan de santé"
        Me._ongletsclient_TabPage1.UseVisualStyleBackColor = True
        '
        'antecedants
        '
        Me.antecedants.activateLinksOnEdit = True
        Me.antecedants.allowContextMenu = True
        Me.antecedants.allowEditorContextMenu = True
        Me.antecedants.allowNavigation = False
        Me.antecedants.allowPopupWindows = True
        Me.antecedants.allowRefresh = False
        Me.antecedants.allowUndo = True
        Me.antecedants.ancre = Nothing
        Me.antecedants.ancreActif = False
        Me.antecedants.editorContextMenu = Nothing
        Me.antecedants.editorHeight = 350
        Me.antecedants.editorURL = ""
        Me.antecedants.editorWidth = 460
        Me.antecedants.htmlPageURL = Nothing
        Me.antecedants.Location = New System.Drawing.Point(0, 40)
        Me.antecedants.Name = "antecedants"
        Me.antecedants.Silent = False
        Me.antecedants.Size = New System.Drawing.Size(560, 560)
        Me.antecedants.startupPos = 0
        Me.antecedants.TabIndex = 105
        Me.antecedants.toolBarStyles = 1
        Me.antecedants.useNavigationCache = True
        Me.antecedants.viewDisableHtmlFields = False
        '
        'maximise2
        '
        Me.maximise2.BackColor = System.Drawing.SystemColors.Control
        Me.maximise2.Cursor = System.Windows.Forms.Cursors.Default
        Me.maximise2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.maximise2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maximise2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.maximise2.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.maximise2.Location = New System.Drawing.Point(268, 4)
        Me.maximise2.Name = "maximise2"
        Me.maximise2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.maximise2.Size = New System.Drawing.Size(24, 24)
        Me.maximise2.TabIndex = 104
        Me.maximise2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.maximise2, "Maximiser")
        Me.maximise2.UseVisualStyleBackColor = False
        '
        'saveantecedent
        '
        Me.saveantecedent.BackColor = System.Drawing.SystemColors.Control
        Me.saveantecedent.Cursor = System.Windows.Forms.Cursors.Default
        Me.saveantecedent.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.saveantecedent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveantecedent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.saveantecedent.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.saveantecedent.Location = New System.Drawing.Point(228, 4)
        Me.saveantecedent.Name = "saveantecedent"
        Me.saveantecedent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.saveantecedent.Size = New System.Drawing.Size(24, 24)
        Me.saveantecedent.TabIndex = 95
        Me.saveantecedent.TabStop = False
        Me.ToolTip1.SetToolTip(Me.saveantecedent, "Modifier")
        Me.saveantecedent.UseVisualStyleBackColor = False
        '
        'modelebtn2
        '
        Me.modelebtn2.BackColor = System.Drawing.SystemColors.Control
        Me.modelebtn2.Cursor = System.Windows.Forms.Cursors.Default
        Me.modelebtn2.Enabled = False
        Me.modelebtn2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modelebtn2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modelebtn2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modelebtn2.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modelebtn2.Location = New System.Drawing.Point(308, 4)
        Me.modelebtn2.Name = "modelebtn2"
        Me.modelebtn2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modelebtn2.Size = New System.Drawing.Size(24, 24)
        Me.modelebtn2.TabIndex = 103
        Me.modelebtn2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modelebtn2, "Modèles")
        Me.modelebtn2.UseVisualStyleBackColor = False
        '
        '_ongletsclient_TabPage4
        '
        Me._ongletsclient_TabPage4.Controls.Add(Me.GroupComm)
        Me._ongletsclient_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._ongletsclient_TabPage4.Name = "_ongletsclient_TabPage4"
        Me._ongletsclient_TabPage4.Size = New System.Drawing.Size(561, 598)
        Me._ongletsclient_TabPage4.TabIndex = 4
        Me._ongletsclient_TabPage4.Text = "Communications"
        Me._ongletsclient_TabPage4.UseVisualStyleBackColor = True
        '
        'GroupComm
        '
        Me.GroupComm.Controls.Add(Me.dossiersClist)
        Me.GroupComm.Controls.Add(Me.panel2)
        Me.GroupComm.Controls.Add(Me.viderComm)
        Me.GroupComm.Controls.Add(Me.modifComm)
        Me.GroupComm.Controls.Add(Me.btnAddComm)
        Me.GroupComm.Controls.Add(Me.listeCommunications)
        Me.GroupComm.Controls.Add(Me.CommRemarques)
        Me.GroupComm.Controls.Add(Me.label28)
        Me.GroupComm.Controls.Add(Me.importComm)
        Me.GroupComm.Controls.Add(Me.commFiltrageCat)
        Me.GroupComm.Controls.Add(Me.commFiltrage)
        Me.GroupComm.Controls.Add(Me.commUser)
        Me.GroupComm.Controls.Add(Me.lblCommDeA)
        Me.GroupComm.Controls.Add(Me.label27)
        Me.GroupComm.Controls.Add(Me.label31)
        Me.GroupComm.Controls.Add(Me.label23)
        Me.GroupComm.Controls.Add(Me.delComm)
        Me.GroupComm.Controls.Add(Me.colorReceived)
        Me.GroupComm.Controls.Add(Me.colorSent)
        Me.GroupComm.Controls.Add(Me.label32)
        Me.GroupComm.Controls.Add(Me.label29)
        Me.GroupComm.Controls.Add(Me.commDate)
        Me.GroupComm.Controls.Add(Me.label25)
        Me.GroupComm.Controls.Add(Me.selectCommDate)
        Me.GroupComm.Controls.Add(Me.commDeA)
        Me.GroupComm.Controls.Add(Me.label22)
        Me.GroupComm.Controls.Add(Me.label26)
        Me.GroupComm.Controls.Add(Me.commReception)
        Me.GroupComm.Controls.Add(Me.commEnvoie)
        Me.GroupComm.Controls.Add(Me.commCategorie)
        Me.GroupComm.Controls.Add(Me.selectKeyPeople)
        Me.GroupComm.Controls.Add(Me.commSujet)
        Me.GroupComm.Location = New System.Drawing.Point(8, 6)
        Me.GroupComm.Name = "GroupComm"
        Me.GroupComm.Size = New System.Drawing.Size(541, 584)
        Me.GroupComm.TabIndex = 134
        Me.GroupComm.TabStop = False
        Me.GroupComm.Text = "Communications"
        '
        'dossiersClist
        '
        Me.dossiersClist.acceptAlpha = True
        Me.dossiersClist.acceptedChars = Nothing
        Me.dossiersClist.acceptNumeric = True
        Me.dossiersClist.allCapital = False
        Me.dossiersClist.allLower = False
        Me.dossiersClist.autoComplete = True
        Me.dossiersClist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.dossiersClist.autoSizeDropDown = True
        Me.dossiersClist.BackColor = System.Drawing.Color.White
        Me.dossiersClist.blockOnMaximum = False
        Me.dossiersClist.blockOnMinimum = False
        Me.dossiersClist.cb_AcceptLeftZeros = False
        Me.dossiersClist.cb_AcceptNegative = False
        Me.dossiersClist.currencyBox = False
        Me.dossiersClist.Cursor = System.Windows.Forms.Cursors.Default
        Me.dossiersClist.dbField = Nothing
        Me.dossiersClist.doComboDelete = True
        Me.dossiersClist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dossiersClist.firstLetterCapital = False
        Me.dossiersClist.firstLettersCapital = False
        Me.dossiersClist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dossiersClist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dossiersClist.IntegralHeight = False
        Me.dossiersClist.itemsToolTipDuration = 10000
        Me.dossiersClist.Location = New System.Drawing.Point(6, 181)
        Me.dossiersClist.manageText = True
        Me.dossiersClist.matchExp = Nothing
        Me.dossiersClist.maximum = 0
        Me.dossiersClist.minimum = 0
        Me.dossiersClist.Name = "dossiersClist"
        Me.dossiersClist.nbDecimals = CType(-1, Short)
        Me.dossiersClist.onlyAlphabet = False
        Me.dossiersClist.pathOfList = ""
        Me.dossiersClist.ReadOnly = False
        Me.dossiersClist.refuseAccents = False
        Me.dossiersClist.refusedChars = Nothing
        Me.dossiersClist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dossiersClist.showItemsToolTip = False
        Me.dossiersClist.Size = New System.Drawing.Size(528, 22)
        Me.dossiersClist.TabIndex = 134
        Me.dossiersClist.trimText = False
        '
        'panel2
        '
        Me.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel2.Controls.Add(Me.commType2)
        Me.panel2.Controls.Add(Me.commType1)
        Me.panel2.Location = New System.Drawing.Point(6, 19)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(528, 16)
        Me.panel2.TabIndex = 112
        '
        'commType2
        '
        Me.commType2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commType2.Location = New System.Drawing.Point(259, 0)
        Me.commType2.Name = "commType2"
        Me.commType2.Size = New System.Drawing.Size(73, 16)
        Me.commType2.TabIndex = 1
        Me.commType2.Text = "Réception"
        '
        'commType1
        '
        Me.commType1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commType1.Location = New System.Drawing.Point(195, 0)
        Me.commType1.Name = "commType1"
        Me.commType1.Size = New System.Drawing.Size(57, 16)
        Me.commType1.TabIndex = 0
        Me.commType1.Text = "Envoi"
        '
        'viderComm
        '
        Me.viderComm.BackColor = System.Drawing.SystemColors.Control
        Me.viderComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.viderComm.Enabled = False
        Me.viderComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.viderComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viderComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.viderComm.Location = New System.Drawing.Point(510, 140)
        Me.viderComm.Name = "viderComm"
        Me.viderComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.viderComm.Size = New System.Drawing.Size(24, 24)
        Me.viderComm.TabIndex = 122
        Me.viderComm.TabStop = False
        Me.ToolTip1.SetToolTip(Me.viderComm, "Vider les champs")
        Me.viderComm.UseVisualStyleBackColor = False
        '
        'modifComm
        '
        Me.modifComm.BackColor = System.Drawing.SystemColors.Control
        Me.modifComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.modifComm.Enabled = False
        Me.modifComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modifComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifComm.Location = New System.Drawing.Point(508, 503)
        Me.modifComm.Name = "modifComm"
        Me.modifComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modifComm.Size = New System.Drawing.Size(24, 24)
        Me.modifComm.TabIndex = 121
        Me.modifComm.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modifComm, "Enregistrer la communation sélectionnée")
        Me.modifComm.UseVisualStyleBackColor = False
        '
        'btnAddComm
        '
        Me.btnAddComm.BackColor = System.Drawing.SystemColors.Control
        Me.btnAddComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAddComm.Enabled = False
        Me.btnAddComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAddComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnAddComm.Location = New System.Drawing.Point(508, 401)
        Me.btnAddComm.Name = "btnAddComm"
        Me.btnAddComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAddComm.Size = New System.Drawing.Size(24, 24)
        Me.btnAddComm.TabIndex = 120
        Me.btnAddComm.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnAddComm, "Ajouter une communication")
        Me.btnAddComm.UseVisualStyleBackColor = False
        '
        'listeCommunications
        '
        Me.listeCommunications.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.listeCommunications.autoAdjust = True
        Me.listeCommunications.autoKeyDownSelection = True
        Me.listeCommunications.autoSizeHorizontally = False
        Me.listeCommunications.autoSizeVertically = False
        Me.listeCommunications.BackColor = System.Drawing.Color.White
        Me.listeCommunications.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.listeCommunications.baseBackColor = System.Drawing.Color.White
        Me.listeCommunications.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.listeCommunications.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.listeCommunications.bgColor = System.Drawing.Color.White
        Me.listeCommunications.borderColor = System.Drawing.Color.Empty
        Me.listeCommunications.borderSelColor = System.Drawing.Color.Empty
        Me.listeCommunications.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.listeCommunications.CausesValidation = False
        Me.listeCommunications.clickEnabled = True
        Me.listeCommunications.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.listeCommunications.do3D = False
        Me.listeCommunications.draw = False
        Me.listeCommunications.extraWidth = 0
        Me.listeCommunications.hScrollColor = System.Drawing.SystemColors.Control
        Me.listeCommunications.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listeCommunications.hScrolling = True
        Me.listeCommunications.hsValue = 0
        Me.listeCommunications.icons = CType(resources.GetObject("listeCommunications.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.listeCommunications.itemBorder = 0
        Me.listeCommunications.itemMargin = 0
        Me.listeCommunications.items = CType(resources.GetObject("listeCommunications.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.listeCommunications.Location = New System.Drawing.Point(6, 401)
        Me.listeCommunications.mouseMove3D = False
        Me.listeCommunications.mouseSpeed = 0
        Me.listeCommunications.Name = "listeCommunications"
        Me.listeCommunications.objMaxHeight = 0.0!
        Me.listeCommunications.objMaxWidth = 0.0!
        Me.listeCommunications.objMinHeight = 0.0!
        Me.listeCommunications.objMinWidth = 0.0!
        Me.listeCommunications.reverseSorting = False
        Me.listeCommunications.selected = -1
        Me.listeCommunications.selectedClickAllowed = False
        Me.listeCommunications.selectMultiple = False
        Me.listeCommunications.Size = New System.Drawing.Size(496, 177)
        Me.listeCommunications.sorted = True
        Me.listeCommunications.TabIndex = 133
        Me.listeCommunications.toolTipText = Nothing
        Me.listeCommunications.vScrollColor = System.Drawing.SystemColors.Control
        Me.listeCommunications.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.listeCommunications.vScrolling = True
        Me.listeCommunications.vsValue = 0
        '
        'CommRemarques
        '
        Me.CommRemarques.acceptAlpha = True
        Me.CommRemarques.acceptedChars = ""
        Me.CommRemarques.acceptNumeric = True
        Me.CommRemarques.allCapital = False
        Me.CommRemarques.allLower = False
        Me.CommRemarques.blockOnMaximum = False
        Me.CommRemarques.blockOnMinimum = False
        Me.CommRemarques.cb_AcceptLeftZeros = False
        Me.CommRemarques.cb_AcceptNegative = False
        Me.CommRemarques.currencyBox = False
        Me.CommRemarques.firstLetterCapital = True
        Me.CommRemarques.firstLettersCapital = False
        Me.CommRemarques.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CommRemarques.Location = New System.Drawing.Point(6, 222)
        Me.CommRemarques.manageText = True
        Me.CommRemarques.matchExp = ""
        Me.CommRemarques.maximum = 0
        Me.CommRemarques.minimum = 0
        Me.CommRemarques.Multiline = True
        Me.CommRemarques.Name = "CommRemarques"
        Me.CommRemarques.nbDecimals = CType(-1, Short)
        Me.CommRemarques.onlyAlphabet = False
        Me.CommRemarques.refuseAccents = False
        Me.CommRemarques.refusedChars = ""
        Me.CommRemarques.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.CommRemarques.showInternalContextMenu = True
        Me.CommRemarques.Size = New System.Drawing.Size(528, 118)
        Me.CommRemarques.TabIndex = 119
        Me.CommRemarques.trimText = False
        '
        'label28
        '
        Me.label28.AutoSize = True
        Me.label28.BackColor = System.Drawing.Color.Transparent
        Me.label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.label28.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label28.Location = New System.Drawing.Point(6, 205)
        Me.label28.Name = "label28"
        Me.label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label28.Size = New System.Drawing.Size(70, 14)
        Me.label28.TabIndex = 118
        Me.label28.Text = "Remarques :"
        '
        'importComm
        '
        Me.importComm.BackColor = System.Drawing.SystemColors.Control
        Me.importComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.importComm.Enabled = False
        Me.importComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.importComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.importComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.importComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.importComm.Location = New System.Drawing.Point(508, 452)
        Me.importComm.Name = "importComm"
        Me.importComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.importComm.Size = New System.Drawing.Size(24, 24)
        Me.importComm.TabIndex = 128
        Me.importComm.TabStop = False
        Me.ToolTip1.SetToolTip(Me.importComm, "Importer un fichier pour la communication sélectionnée")
        Me.importComm.UseVisualStyleBackColor = False
        '
        'commFiltrageCat
        '
        Me.commFiltrageCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.commFiltrageCat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commFiltrageCat.Location = New System.Drawing.Point(292, 373)
        Me.commFiltrageCat.Name = "commFiltrageCat"
        Me.commFiltrageCat.Size = New System.Drawing.Size(207, 22)
        Me.commFiltrageCat.Sorted = True
        Me.commFiltrageCat.TabIndex = 132
        '
        'commFiltrage
        '
        Me.commFiltrage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.commFiltrage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commFiltrage.Location = New System.Drawing.Point(51, 373)
        Me.commFiltrage.Name = "commFiltrage"
        Me.commFiltrage.Size = New System.Drawing.Size(168, 22)
        Me.commFiltrage.Sorted = True
        Me.commFiltrage.TabIndex = 132
        '
        'commUser
        '
        Me.commUser.AutoSize = True
        Me.commUser.BackColor = System.Drawing.SystemColors.Control
        Me.commUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.commUser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.commUser.Location = New System.Drawing.Point(334, 140)
        Me.commUser.Name = "commUser"
        Me.commUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.commUser.Size = New System.Drawing.Size(0, 14)
        Me.commUser.TabIndex = 117
        '
        'lblCommDeA
        '
        Me.lblCommDeA.AutoSize = True
        Me.lblCommDeA.BackColor = System.Drawing.Color.Transparent
        Me.lblCommDeA.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCommDeA.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommDeA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCommDeA.Location = New System.Drawing.Point(6, 43)
        Me.lblCommDeA.Name = "lblCommDeA"
        Me.lblCommDeA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCommDeA.Size = New System.Drawing.Size(43, 14)
        Me.lblCommDeA.TabIndex = 65
        Me.lblCommDeA.Text = "À | De :"
        '
        'label27
        '
        Me.label27.AutoSize = True
        Me.label27.BackColor = System.Drawing.Color.Transparent
        Me.label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.label27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label27.Location = New System.Drawing.Point(270, 140)
        Me.label27.Name = "label27"
        Me.label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label27.Size = New System.Drawing.Size(63, 14)
        Me.label27.TabIndex = 116
        Me.label27.Text = "Utilisateur :"
        '
        'label31
        '
        Me.label31.AutoSize = True
        Me.label31.BackColor = System.Drawing.Color.Transparent
        Me.label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.label31.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label31.Location = New System.Drawing.Point(6, 76)
        Me.label31.Name = "label31"
        Me.label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label31.Size = New System.Drawing.Size(61, 14)
        Me.label31.TabIndex = 66
        Me.label31.Text = "Catégorie :"
        '
        'label23
        '
        Me.label23.AutoSize = True
        Me.label23.BackColor = System.Drawing.Color.Transparent
        Me.label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.label23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label23.Location = New System.Drawing.Point(6, 111)
        Me.label23.Name = "label23"
        Me.label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label23.Size = New System.Drawing.Size(41, 14)
        Me.label23.TabIndex = 66
        Me.label23.Text = "Objet :"
        '
        'delComm
        '
        Me.delComm.BackColor = System.Drawing.SystemColors.Control
        Me.delComm.Cursor = System.Windows.Forms.Cursors.Default
        Me.delComm.Enabled = False
        Me.delComm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delComm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delComm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.delComm.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delComm.Location = New System.Drawing.Point(508, 554)
        Me.delComm.Name = "delComm"
        Me.delComm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delComm.Size = New System.Drawing.Size(24, 24)
        Me.delComm.TabIndex = 127
        Me.delComm.TabStop = False
        Me.ToolTip1.SetToolTip(Me.delComm, "Supprimer la communication sélectionnée")
        Me.delComm.UseVisualStyleBackColor = False
        '
        'colorReceived
        '
        Me.colorReceived.BackColor = System.Drawing.Color.White
        Me.colorReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorReceived.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorReceived.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorReceived.ForeColor = System.Drawing.SystemColors.ControlText
        Me.colorReceived.Location = New System.Drawing.Point(284, 350)
        Me.colorReceived.Name = "colorReceived"
        Me.colorReceived.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorReceived.Size = New System.Drawing.Size(62, 18)
        Me.colorReceived.TabIndex = 131
        Me.colorReceived.Text = "Réception"
        Me.colorReceived.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'colorSent
        '
        Me.colorSent.BackColor = System.Drawing.Color.LightGray
        Me.colorSent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorSent.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorSent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorSent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.colorSent.Location = New System.Drawing.Point(200, 350)
        Me.colorSent.Name = "colorSent"
        Me.colorSent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorSent.Size = New System.Drawing.Size(42, 18)
        Me.colorSent.TabIndex = 131
        Me.colorSent.Text = "Envoi"
        Me.colorSent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label32
        '
        Me.label32.AutoSize = True
        Me.label32.BackColor = System.Drawing.Color.Transparent
        Me.label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.label32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label32.Location = New System.Drawing.Point(225, 376)
        Me.label32.Name = "label32"
        Me.label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label32.Size = New System.Drawing.Size(61, 14)
        Me.label32.TabIndex = 131
        Me.label32.Text = "Catégorie :"
        '
        'label29
        '
        Me.label29.AutoSize = True
        Me.label29.BackColor = System.Drawing.Color.Transparent
        Me.label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.label29.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label29.Location = New System.Drawing.Point(3, 376)
        Me.label29.Name = "label29"
        Me.label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label29.Size = New System.Drawing.Size(43, 14)
        Me.label29.TabIndex = 131
        Me.label29.Text = "À | De :"
        '
        'commDate
        '
        Me.commDate.AutoSize = True
        Me.commDate.BackColor = System.Drawing.SystemColors.Control
        Me.commDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.commDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.commDate.Location = New System.Drawing.Point(105, 141)
        Me.commDate.Name = "commDate"
        Me.commDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.commDate.Size = New System.Drawing.Size(0, 14)
        Me.commDate.TabIndex = 115
        '
        'label25
        '
        Me.label25.AutoSize = True
        Me.label25.BackColor = System.Drawing.Color.Transparent
        Me.label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.label25.Font = New System.Drawing.Font("Arial", 8.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label25.Location = New System.Drawing.Point(3, 352)
        Me.label25.Name = "label25"
        Me.label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label25.Size = New System.Drawing.Size(155, 13)
        Me.label25.TabIndex = 67
        Me.label25.Text = "Liste des communications :"
        '
        'selectCommDate
        '
        Me.selectCommDate.BackColor = System.Drawing.SystemColors.Control
        Me.selectCommDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectCommDate.Enabled = False
        Me.selectCommDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectCommDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectCommDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectCommDate.Location = New System.Drawing.Point(73, 135)
        Me.selectCommDate.Name = "selectCommDate"
        Me.selectCommDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectCommDate.Size = New System.Drawing.Size(24, 24)
        Me.selectCommDate.TabIndex = 114
        Me.ToolTip1.SetToolTip(Me.selectCommDate, "Choisir la date de la communication")
        Me.selectCommDate.UseVisualStyleBackColor = False
        '
        'commDeA
        '
        Me.commDeA.acceptAlpha = True
        Me.commDeA.acceptedChars = Nothing
        Me.commDeA.acceptNumeric = True
        Me.commDeA.allCapital = False
        Me.commDeA.allLower = False
        Me.commDeA.autoComplete = True
        Me.commDeA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.commDeA.autoSizeDropDown = True
        Me.commDeA.BackColor = System.Drawing.Color.White
        Me.commDeA.blockOnMaximum = False
        Me.commDeA.blockOnMinimum = False
        Me.commDeA.cb_AcceptLeftZeros = False
        Me.commDeA.cb_AcceptNegative = False
        Me.commDeA.currencyBox = False
        Me.commDeA.dbField = Nothing
        Me.commDeA.doComboDelete = True
        Me.commDeA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.commDeA.firstLetterCapital = False
        Me.commDeA.firstLettersCapital = False
        Me.commDeA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commDeA.IntegralHeight = False
        Me.commDeA.itemsToolTipDuration = 10000
        Me.commDeA.Location = New System.Drawing.Point(73, 43)
        Me.commDeA.manageText = True
        Me.commDeA.matchExp = Nothing
        Me.commDeA.maximum = 0
        Me.commDeA.minimum = 0
        Me.commDeA.Name = "commDeA"
        Me.commDeA.nbDecimals = CType(-1, Short)
        Me.commDeA.onlyAlphabet = False
        Me.commDeA.pathOfList = ""
        Me.commDeA.ReadOnly = False
        Me.commDeA.refuseAccents = False
        Me.commDeA.refusedChars = Nothing
        Me.commDeA.showItemsToolTip = False
        Me.commDeA.Size = New System.Drawing.Size(429, 22)
        Me.commDeA.Sorted = True
        Me.commDeA.TabIndex = 109
        Me.commDeA.trimText = False
        '
        'label22
        '
        Me.label22.AutoSize = True
        Me.label22.BackColor = System.Drawing.Color.Transparent
        Me.label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.label22.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label22.Location = New System.Drawing.Point(6, 164)
        Me.label22.Name = "label22"
        Me.label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label22.Size = New System.Drawing.Size(208, 14)
        Me.label22.TabIndex = 113
        Me.label22.Text = "Dossier associé à cette communication :"
        '
        'label26
        '
        Me.label26.AutoSize = True
        Me.label26.BackColor = System.Drawing.Color.Transparent
        Me.label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.label26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label26.Location = New System.Drawing.Point(6, 140)
        Me.label26.Name = "label26"
        Me.label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label26.Size = New System.Drawing.Size(36, 14)
        Me.label26.TabIndex = 113
        Me.label26.Text = "Date :"
        '
        'commReception
        '
        Me.commReception.AutoSize = True
        Me.commReception.Checked = True
        Me.commReception.CheckState = System.Windows.Forms.CheckState.Checked
        Me.commReception.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commReception.Location = New System.Drawing.Point(263, 353)
        Me.commReception.Name = "commReception"
        Me.commReception.Size = New System.Drawing.Size(15, 14)
        Me.commReception.TabIndex = 130
        '
        'commEnvoie
        '
        Me.commEnvoie.AutoSize = True
        Me.commEnvoie.Checked = True
        Me.commEnvoie.CheckState = System.Windows.Forms.CheckState.Checked
        Me.commEnvoie.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commEnvoie.Location = New System.Drawing.Point(179, 353)
        Me.commEnvoie.Name = "commEnvoie"
        Me.commEnvoie.Size = New System.Drawing.Size(15, 14)
        Me.commEnvoie.TabIndex = 129
        '
        'commCategorie
        '
        Me.commCategorie.acceptAlpha = True
        Me.commCategorie.acceptedChars = Nothing
        Me.commCategorie.acceptNumeric = True
        Me.commCategorie.allCapital = False
        Me.commCategorie.allLower = False
        Me.commCategorie.autoComplete = True
        Me.commCategorie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.commCategorie.autoSizeDropDown = True
        Me.commCategorie.BackColor = System.Drawing.Color.White
        Me.commCategorie.blockOnMaximum = False
        Me.commCategorie.blockOnMinimum = False
        Me.commCategorie.cb_AcceptLeftZeros = False
        Me.commCategorie.cb_AcceptNegative = False
        Me.commCategorie.currencyBox = False
        Me.commCategorie.dbField = "CommCategories.Categorie"
        Me.commCategorie.doComboDelete = True
        Me.commCategorie.firstLetterCapital = True
        Me.commCategorie.firstLettersCapital = False
        Me.commCategorie.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commCategorie.IntegralHeight = False
        Me.commCategorie.itemsToolTipDuration = 10000
        Me.commCategorie.Location = New System.Drawing.Point(73, 73)
        Me.commCategorie.manageText = True
        Me.commCategorie.matchExp = ""
        Me.commCategorie.maximum = 0
        Me.commCategorie.minimum = 0
        Me.commCategorie.Name = "commCategorie"
        Me.commCategorie.nbDecimals = CType(-1, Short)
        Me.commCategorie.onlyAlphabet = False
        Me.commCategorie.pathOfList = ""
        Me.commCategorie.ReadOnly = False
        Me.commCategorie.refuseAccents = False
        Me.commCategorie.refusedChars = ""
        Me.commCategorie.showItemsToolTip = False
        Me.commCategorie.Size = New System.Drawing.Size(461, 22)
        Me.commCategorie.Sorted = True
        Me.commCategorie.TabIndex = 111
        Me.commCategorie.trimText = False
        '
        'selectKeyPeople
        '
        Me.selectKeyPeople.BackColor = System.Drawing.SystemColors.Control
        Me.selectKeyPeople.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectKeyPeople.Enabled = False
        Me.selectKeyPeople.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectKeyPeople.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectKeyPeople.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectKeyPeople.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.selectKeyPeople.Location = New System.Drawing.Point(510, 42)
        Me.selectKeyPeople.Name = "selectKeyPeople"
        Me.selectKeyPeople.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectKeyPeople.Size = New System.Drawing.Size(24, 24)
        Me.selectKeyPeople.TabIndex = 110
        Me.ToolTip1.SetToolTip(Me.selectKeyPeople, "Sélectionner une personne / organisme clé")
        Me.selectKeyPeople.UseVisualStyleBackColor = False
        '
        'commSujet
        '
        Me.commSujet.acceptAlpha = True
        Me.commSujet.acceptedChars = Nothing
        Me.commSujet.acceptNumeric = True
        Me.commSujet.allCapital = False
        Me.commSujet.allLower = False
        Me.commSujet.autoComplete = True
        Me.commSujet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.commSujet.autoSizeDropDown = True
        Me.commSujet.BackColor = System.Drawing.Color.White
        Me.commSujet.blockOnMaximum = False
        Me.commSujet.blockOnMinimum = False
        Me.commSujet.cb_AcceptLeftZeros = False
        Me.commSujet.cb_AcceptNegative = False
        Me.commSujet.currencyBox = False
        Me.commSujet.dbField = "CommSubjects.CommSubject"
        Me.commSujet.doComboDelete = True
        Me.commSujet.firstLetterCapital = True
        Me.commSujet.firstLettersCapital = False
        Me.commSujet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commSujet.IntegralHeight = False
        Me.commSujet.itemsToolTipDuration = 10000
        Me.commSujet.Location = New System.Drawing.Point(73, 108)
        Me.commSujet.manageText = True
        Me.commSujet.matchExp = ""
        Me.commSujet.maximum = 0
        Me.commSujet.minimum = 0
        Me.commSujet.Name = "commSujet"
        Me.commSujet.nbDecimals = CType(-1, Short)
        Me.commSujet.onlyAlphabet = False
        Me.commSujet.pathOfList = ""
        Me.commSujet.ReadOnly = False
        Me.commSujet.refuseAccents = False
        Me.commSujet.refusedChars = ""
        Me.commSujet.showItemsToolTip = False
        Me.commSujet.Size = New System.Drawing.Size(461, 22)
        Me.commSujet.Sorted = True
        Me.commSujet.TabIndex = 111
        Me.commSujet.trimText = False
        '
        '_ongletsclient_TabPage3
        '
        Me._ongletsclient_TabPage3.Controls.Add(Me.facturesView)
        Me._ongletsclient_TabPage3.Controls.Add(Me.GroupBox1)
        Me._ongletsclient_TabPage3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ongletsclient_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._ongletsclient_TabPage3.Name = "_ongletsclient_TabPage3"
        Me._ongletsclient_TabPage3.Size = New System.Drawing.Size(561, 598)
        Me._ongletsclient_TabPage3.TabIndex = 3
        Me._ongletsclient_TabPage3.Text = "Comptabilité"
        Me._ongletsclient_TabPage3.UseVisualStyleBackColor = True
        '
        'facturesView
        '
        Me.facturesView.AllowUserToAddRows = False
        Me.facturesView.AllowUserToDeleteRows = False
        Me.facturesView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.facturesView.autoSelectOnDataSourceChanged = True
        Me.facturesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.facturesView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.facturesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.facturesView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DateF, Me.TF, Me.MF, Me.MP, Me.PC, Me.PPO, Me.PU, Me.EL, Me.NoFacture})
        Me.facturesView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.facturesView.isDoubleBuffered = False
        Me.facturesView.Location = New System.Drawing.Point(0, 47)
        Me.facturesView.Name = "facturesView"
        Me.facturesView.ReadOnly = True
        Me.facturesView.RowHeadersVisible = False
        Me.facturesView.RowHeadersWidth = 20
        Me.facturesView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.facturesView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.facturesView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.facturesView.Size = New System.Drawing.Size(560, 417)
        Me.facturesView.TabIndex = 283
        '
        'DateF
        '
        Me.DateF.DataPropertyName = "DF"
        DataGridViewCellStyle6.Format = "d"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.DateF.DefaultCellStyle = DataGridViewCellStyle6
        Me.DateF.HeaderText = "Date facture"
        Me.DateF.Name = "DateF"
        Me.DateF.ReadOnly = True
        '
        'TF
        '
        Me.TF.DataPropertyName = "TF"
        Me.TF.HeaderText = "Type facture"
        Me.TF.Name = "TF"
        Me.TF.ReadOnly = True
        '
        'MF
        '
        Me.MF.DataPropertyName = "MF"
        DataGridViewCellStyle7.Format = "C2"
        DataGridViewCellStyle7.NullValue = "0"
        Me.MF.DefaultCellStyle = DataGridViewCellStyle7
        Me.MF.HeaderText = "Montant facturé"
        Me.MF.Name = "MF"
        Me.MF.ReadOnly = True
        '
        'MP
        '
        Me.MP.DataPropertyName = "MP"
        DataGridViewCellStyle8.Format = "C2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.MP.DefaultCellStyle = DataGridViewCellStyle8
        Me.MP.HeaderText = "Montant payé"
        Me.MP.Name = "MP"
        Me.MP.ReadOnly = True
        '
        'PC
        '
        Me.PC.DataPropertyName = "PC"
        Me.PC.HeaderText = "Payeur client"
        Me.PC.Name = "PC"
        Me.PC.ReadOnly = True
        Me.PC.Visible = False
        '
        'PPO
        '
        Me.PPO.DataPropertyName = "PPO"
        Me.PPO.HeaderText = "Payeur P/O"
        Me.PPO.Name = "PPO"
        Me.PPO.ReadOnly = True
        '
        'PU
        '
        Me.PU.DataPropertyName = "PU"
        Me.PU.HeaderText = "Payeur utilisateur"
        Me.PU.Name = "PU"
        Me.PU.ReadOnly = True
        Me.PU.Visible = False
        '
        'EL
        '
        Me.EL.DataPropertyName = "EL"
        Me.EL.HeaderText = "Entité liée"
        Me.EL.Name = "EL"
        Me.EL.ReadOnly = True
        Me.EL.Visible = False
        '
        'NoFacture
        '
        Me.NoFacture.DataPropertyName = "NoFacture"
        Me.NoFacture.HeaderText = "# Facture"
        Me.NoFacture.Name = "NoFacture"
        Me.NoFacture.ReadOnly = True
        Me.NoFacture.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.FilterBills)
        Me.GroupBox1.Controls.Add(Me.DateA)
        Me.GroupBox1.Controls.Add(Me.DateDe)
        Me.GroupBox1.Controls.Add(Me.ChoixA)
        Me.GroupBox1.Controls.Add(Me.DateAll)
        Me.GroupBox1.Controls.Add(Me.ChoixDe)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(555, 41)
        Me.GroupBox1.TabIndex = 148
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtrage"
        '
        'FilterBills
        '
        Me.FilterBills.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.FilterBills.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FilterBills.Location = New System.Drawing.Point(352, 12)
        Me.FilterBills.Name = "FilterBills"
        Me.FilterBills.Size = New System.Drawing.Size(32, 21)
        Me.FilterBills.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.FilterBills, "Filtrer")
        '
        'DateA
        '
        Me.DateA.AutoSize = True
        Me.DateA.BackColor = System.Drawing.Color.Transparent
        Me.DateA.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateA.Location = New System.Drawing.Point(264, 16)
        Me.DateA.Name = "DateA"
        Me.DateA.Size = New System.Drawing.Size(0, 14)
        Me.DateA.TabIndex = 5
        '
        'DateDe
        '
        Me.DateDe.AutoSize = True
        Me.DateDe.BackColor = System.Drawing.Color.Transparent
        Me.DateDe.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateDe.Location = New System.Drawing.Point(148, 16)
        Me.DateDe.Name = "DateDe"
        Me.DateDe.Size = New System.Drawing.Size(0, 14)
        Me.DateDe.TabIndex = 4
        '
        'ChoixA
        '
        Me.ChoixA.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChoixA.Location = New System.Drawing.Point(224, 16)
        Me.ChoixA.Name = "ChoixA"
        Me.ChoixA.Size = New System.Drawing.Size(32, 16)
        Me.ChoixA.TabIndex = 3
        Me.ChoixA.Text = "A"
        '
        'DateAll
        '
        Me.DateAll.BackColor = System.Drawing.Color.Transparent
        Me.DateAll.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateAll.Location = New System.Drawing.Point(8, 16)
        Me.DateAll.Name = "DateAll"
        Me.DateAll.Size = New System.Drawing.Size(104, 16)
        Me.DateAll.TabIndex = 2
        Me.DateAll.Text = "Toutes les dates"
        Me.DateAll.UseVisualStyleBackColor = False
        '
        'ChoixDe
        '
        Me.ChoixDe.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChoixDe.Location = New System.Drawing.Point(112, 16)
        Me.ChoixDe.Name = "ChoixDe"
        Me.ChoixDe.Size = New System.Drawing.Size(32, 16)
        Me.ChoixDe.TabIndex = 1
        Me.ChoixDe.Text = "De"
        '
        '_ongletsclient_TabPage0
        '
        Me._ongletsclient_TabPage0.Controls.Add(Me.folderHistoryFrame)
        Me._ongletsclient_TabPage0.Controls.Add(Me.modifsaveTextes)
        Me._ongletsclient_TabPage0.Controls.Add(Me.modifsaveEquip)
        Me._ongletsclient_TabPage0.Controls.Add(Me.modifsaveDossierInfos)
        Me._ongletsclient_TabPage0.Controls.Add(Me.maximise1)
        Me._ongletsclient_TabPage0.Controls.Add(Me.modelebtn1)
        Me._ongletsclient_TabPage0.Controls.Add(Me.dossier)
        Me._ongletsclient_TabPage0.Controls.Add(Me.folderTexts)
        Me._ongletsclient_TabPage0.Controls.Add(Me.ongletsdossier)
        Me._ongletsclient_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._ongletsclient_TabPage0.Name = "_ongletsclient_TabPage0"
        Me._ongletsclient_TabPage0.Size = New System.Drawing.Size(561, 598)
        Me._ongletsclient_TabPage0.TabIndex = 0
        Me._ongletsclient_TabPage0.Text = "Dossiers"
        Me._ongletsclient_TabPage0.UseVisualStyleBackColor = True
        '
        'folderHistoryFrame
        '
        Me.folderHistoryFrame.Controls.Add(Me.FermerDossierHisto)
        Me.folderHistoryFrame.Controls.Add(Me.Label30)
        Me.folderHistoryFrame.Controls.Add(Me.folderHistory)
        Me.folderHistoryFrame.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.folderHistoryFrame.Location = New System.Drawing.Point(0, 73)
        Me.folderHistoryFrame.Name = "folderHistoryFrame"
        Me.folderHistoryFrame.Size = New System.Drawing.Size(561, 236)
        Me.folderHistoryFrame.TabIndex = 193
        Me.folderHistoryFrame.Visible = False
        '
        'FermerDossierHisto
        '
        Me.FermerDossierHisto.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.FermerDossierHisto.Location = New System.Drawing.Point(542, 3)
        Me.FermerDossierHisto.Name = "FermerDossierHisto"
        Me.FermerDossierHisto.Size = New System.Drawing.Size(16, 16)
        Me.FermerDossierHisto.TabIndex = 288
        Me.ToolTip1.SetToolTip(Me.FermerDossierHisto, "Cacher")
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(-1, 5)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(201, 14)
        Me.Label30.TabIndex = 287
        Me.Label30.Text = "Historique du dossier sélectionné :"
        '
        'folderHistory
        '
        Me.folderHistory.AllowUserToAddRows = False
        Me.folderHistory.AllowUserToDeleteRows = False
        Me.folderHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.folderHistory.autoSelectOnDataSourceChanged = True
        Me.folderHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.folderHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.folderHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8})
        Me.folderHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.folderHistory.isDoubleBuffered = False
        Me.folderHistory.Location = New System.Drawing.Point(2, 22)
        Me.folderHistory.Name = "folderHistory"
        Me.folderHistory.ReadOnly = True
        Me.folderHistory.RowHeadersVisible = False
        Me.folderHistory.RowHeadersWidth = 20
        Me.folderHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.folderHistory.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.folderHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.folderHistory.Size = New System.Drawing.Size(558, 212)
        Me.folderHistory.TabIndex = 286
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "DateHeureCreation"
        DataGridViewCellStyle9.Format = "g"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn4.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 54
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "NomAction"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Action"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 63
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "ByUser"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Fait par"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 68
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Raison"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Raison"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 65
        '
        'modifsaveTextes
        '
        Me.modifsaveTextes.BackColor = System.Drawing.SystemColors.Control
        Me.modifsaveTextes.Cursor = System.Windows.Forms.Cursors.Default
        Me.modifsaveTextes.Enabled = False
        Me.modifsaveTextes.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifsaveTextes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifsaveTextes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modifsaveTextes.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifsaveTextes.Location = New System.Drawing.Point(477, 74)
        Me.modifsaveTextes.Name = "modifsaveTextes"
        Me.modifsaveTextes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modifsaveTextes.Size = New System.Drawing.Size(24, 24)
        Me.modifsaveTextes.TabIndex = 86
        Me.modifsaveTextes.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modifsaveTextes, "Modifier les textes du dossier sélectionné")
        Me.modifsaveTextes.UseVisualStyleBackColor = False
        '
        'modifsaveEquip
        '
        Me.modifsaveEquip.BackColor = System.Drawing.SystemColors.Control
        Me.modifsaveEquip.Cursor = System.Windows.Forms.Cursors.Default
        Me.modifsaveEquip.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifsaveEquip.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifsaveEquip.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modifsaveEquip.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifsaveEquip.Location = New System.Drawing.Point(477, 74)
        Me.modifsaveEquip.Name = "modifsaveEquip"
        Me.modifsaveEquip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modifsaveEquip.Size = New System.Drawing.Size(24, 24)
        Me.modifsaveEquip.TabIndex = 86
        Me.modifsaveEquip.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modifsaveEquip, "Commencer la modification de l'équipement prêté/vendu du dossier sélectionné")
        Me.modifsaveEquip.UseVisualStyleBackColor = False
        '
        'modifsaveDossierInfos
        '
        Me.modifsaveDossierInfos.BackColor = System.Drawing.SystemColors.Control
        Me.modifsaveDossierInfos.Cursor = System.Windows.Forms.Cursors.Default
        Me.modifsaveDossierInfos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifsaveDossierInfos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifsaveDossierInfos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modifsaveDossierInfos.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifsaveDossierInfos.Location = New System.Drawing.Point(477, 74)
        Me.modifsaveDossierInfos.Name = "modifsaveDossierInfos"
        Me.modifsaveDossierInfos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modifsaveDossierInfos.Size = New System.Drawing.Size(24, 24)
        Me.modifsaveDossierInfos.TabIndex = 86
        Me.modifsaveDossierInfos.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modifsaveDossierInfos, "Modifier les informations de base du dossier sélectionné")
        Me.modifsaveDossierInfos.UseVisualStyleBackColor = False
        '
        'maximise1
        '
        Me.maximise1.BackColor = System.Drawing.SystemColors.Control
        Me.maximise1.Cursor = System.Windows.Forms.Cursors.Default
        Me.maximise1.Enabled = False
        Me.maximise1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.maximise1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maximise1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.maximise1.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.maximise1.Location = New System.Drawing.Point(501, 74)
        Me.maximise1.Name = "maximise1"
        Me.maximise1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.maximise1.Size = New System.Drawing.Size(24, 24)
        Me.maximise1.TabIndex = 87
        Me.maximise1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.maximise1, "Maximiser le texte en cours")
        Me.maximise1.UseVisualStyleBackColor = False
        '
        'modelebtn1
        '
        Me.modelebtn1.BackColor = System.Drawing.SystemColors.Control
        Me.modelebtn1.Cursor = System.Windows.Forms.Cursors.Default
        Me.modelebtn1.Enabled = False
        Me.modelebtn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modelebtn1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modelebtn1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modelebtn1.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modelebtn1.Location = New System.Drawing.Point(525, 74)
        Me.modelebtn1.Name = "modelebtn1"
        Me.modelebtn1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modelebtn1.Size = New System.Drawing.Size(24, 24)
        Me.modelebtn1.TabIndex = 101
        Me.modelebtn1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modelebtn1, "Modèles pour le texte en cours")
        Me.modelebtn1.UseVisualStyleBackColor = False
        '
        'dossier
        '
        Me.dossier.BackColor = System.Drawing.SystemColors.Window
        Me.dossier.Cursor = System.Windows.Forms.Cursors.Default
        Me.dossier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dossier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dossier.ItemHeight = 14
        Me.dossier.Location = New System.Drawing.Point(0, 0)
        Me.dossier.Name = "dossier"
        Me.dossier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dossier.Size = New System.Drawing.Size(560, 74)
        Me.dossier.TabIndex = 67
        '
        'folderTexts
        '
        Me.folderTexts.acceptAlpha = True
        Me.folderTexts.acceptedChars = Nothing
        Me.folderTexts.acceptNumeric = True
        Me.folderTexts.allCapital = False
        Me.folderTexts.allLower = False
        Me.folderTexts.autoComplete = True
        Me.folderTexts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.folderTexts.autoSizeDropDown = True
        Me.folderTexts.BackColor = System.Drawing.Color.White
        Me.folderTexts.blockOnMaximum = False
        Me.folderTexts.blockOnMinimum = False
        Me.folderTexts.cb_AcceptLeftZeros = False
        Me.folderTexts.cb_AcceptNegative = False
        Me.folderTexts.currencyBox = False
        Me.folderTexts.dbField = Nothing
        Me.folderTexts.doComboDelete = True
        Me.folderTexts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.folderTexts.firstLetterCapital = False
        Me.folderTexts.firstLettersCapital = False
        Me.folderTexts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.folderTexts.IntegralHeight = False
        Me.folderTexts.itemsToolTipDuration = 10000
        Me.folderTexts.Location = New System.Drawing.Point(209, 74)
        Me.folderTexts.manageText = True
        Me.folderTexts.matchExp = Nothing
        Me.folderTexts.maximum = 0
        Me.folderTexts.minimum = 0
        Me.folderTexts.Name = "folderTexts"
        Me.folderTexts.nbDecimals = CType(-1, Short)
        Me.folderTexts.onlyAlphabet = False
        Me.folderTexts.pathOfList = ""
        Me.folderTexts.ReadOnly = False
        Me.folderTexts.refuseAccents = False
        Me.folderTexts.refusedChars = Nothing
        Me.folderTexts.showItemsToolTip = False
        Me.folderTexts.Size = New System.Drawing.Size(260, 21)
        Me.folderTexts.TabIndex = 191
        Me.folderTexts.trimText = False
        '
        'ongletsdossier
        '
        Me.ongletsdossier.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.ongletsdossier.Controls.Add(Me._ongletsdossier_TabPage4)
        Me.ongletsdossier.Controls.Add(Me._ongletsdossier_TabPage1)
        Me.ongletsdossier.Controls.Add(Me._ongletsdossier_TabPage0)
        Me.ongletsdossier.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ongletsdossier.ItemSize = New System.Drawing.Size(42, 18)
        Me.ongletsdossier.Location = New System.Drawing.Point(0, 74)
        Me.ongletsdossier.Name = "ongletsdossier"
        Me.ongletsdossier.SelectedIndex = 0
        Me.ongletsdossier.Size = New System.Drawing.Size(560, 528)
        Me.ongletsdossier.TabIndex = 68
        '
        '_ongletsdossier_TabPage4
        '
        Me._ongletsdossier_TabPage4.Controls.Add(Me.listEquipement)
        Me._ongletsdossier_TabPage4.Controls.Add(Me.vider)
        Me._ongletsdossier_TabPage4.Controls.Add(Me.delete)
        Me._ongletsdossier_TabPage4.Controls.Add(Me.backPret)
        Me._ongletsdossier_TabPage4.Controls.Add(Me.submit)
        Me._ongletsdossier_TabPage4.Controls.Add(Me.createEquipmentReceipt)
        Me._ongletsdossier_TabPage4.Controls.Add(Me.modif)
        Me._ongletsdossier_TabPage4.Controls.Add(Me.frameE1)
        Me._ongletsdossier_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._ongletsdossier_TabPage4.Name = "_ongletsdossier_TabPage4"
        Me._ongletsdossier_TabPage4.Size = New System.Drawing.Size(552, 502)
        Me._ongletsdossier_TabPage4.TabIndex = 4
        Me._ongletsdossier_TabPage4.Text = "Équipement"
        Me._ongletsdossier_TabPage4.UseVisualStyleBackColor = True
        '
        'listEquipement
        '
        Me.listEquipement.AllowUserToAddRows = False
        Me.listEquipement.AllowUserToDeleteRows = False
        Me.listEquipement.AllowUserToResizeRows = False
        Me.listEquipement.autoSelectOnDataSourceChanged = False
        Me.listEquipement.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.listEquipement.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.listEquipement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.listEquipement.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.listEquipement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listEquipement.isDoubleBuffered = False
        Me.listEquipement.Location = New System.Drawing.Point(0, 349)
        Me.listEquipement.MultiSelect = False
        Me.listEquipement.Name = "listEquipement"
        Me.listEquipement.ReadOnly = True
        Me.listEquipement.RowHeadersVisible = False
        Me.listEquipement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.listEquipement.Size = New System.Drawing.Size(432, 152)
        Me.listEquipement.TabIndex = 116
        '
        'vider
        '
        Me.vider.BackColor = System.Drawing.SystemColors.Control
        Me.vider.Cursor = System.Windows.Forms.Cursors.Default
        Me.vider.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.vider.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vider.ForeColor = System.Drawing.SystemColors.ControlText
        Me.vider.Location = New System.Drawing.Point(522, 375)
        Me.vider.Name = "vider"
        Me.vider.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.vider.Size = New System.Drawing.Size(24, 24)
        Me.vider.TabIndex = 111
        Me.vider.TabStop = False
        Me.ToolTip1.SetToolTip(Me.vider, "Vider les champs")
        Me.vider.UseVisualStyleBackColor = False
        '
        'delete
        '
        Me.delete.BackColor = System.Drawing.SystemColors.Control
        Me.delete.Cursor = System.Windows.Forms.Cursors.Default
        Me.delete.Enabled = False
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.delete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delete.Location = New System.Drawing.Point(466, 375)
        Me.delete.Name = "delete"
        Me.delete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 110
        Me.delete.TabStop = False
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer")
        Me.delete.UseVisualStyleBackColor = False
        '
        'backPret
        '
        Me.backPret.BackColor = System.Drawing.SystemColors.Control
        Me.backPret.Cursor = System.Windows.Forms.Cursors.Default
        Me.backPret.Enabled = False
        Me.backPret.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.backPret.ForeColor = System.Drawing.SystemColors.ControlText
        Me.backPret.Location = New System.Drawing.Point(438, 453)
        Me.backPret.Name = "backPret"
        Me.backPret.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.backPret.Size = New System.Drawing.Size(104, 24)
        Me.backPret.TabIndex = 109
        Me.backPret.TabStop = False
        Me.backPret.Text = "Retour du prêt"
        Me.backPret.UseVisualStyleBackColor = False
        '
        'submit
        '
        Me.submit.BackColor = System.Drawing.SystemColors.Control
        Me.submit.Cursor = System.Windows.Forms.Cursors.Default
        Me.submit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.submit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.submit.Location = New System.Drawing.Point(438, 405)
        Me.submit.Name = "submit"
        Me.submit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.submit.Size = New System.Drawing.Size(104, 24)
        Me.submit.TabIndex = 108
        Me.submit.TabStop = False
        Me.submit.Text = "Vendre / Prêter"
        Me.submit.UseVisualStyleBackColor = False
        '
        'createEquipmentReceipt
        '
        Me.createEquipmentReceipt.BackColor = System.Drawing.SystemColors.Control
        Me.createEquipmentReceipt.Cursor = System.Windows.Forms.Cursors.Default
        Me.createEquipmentReceipt.Enabled = False
        Me.createEquipmentReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.createEquipmentReceipt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.createEquipmentReceipt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.createEquipmentReceipt.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.createEquipmentReceipt.Location = New System.Drawing.Point(494, 375)
        Me.createEquipmentReceipt.Name = "createEquipmentReceipt"
        Me.createEquipmentReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.createEquipmentReceipt.Size = New System.Drawing.Size(24, 24)
        Me.createEquipmentReceipt.TabIndex = 107
        Me.createEquipmentReceipt.TabStop = False
        Me.ToolTip1.SetToolTip(Me.createEquipmentReceipt, "Émettre le reçu pour l'équipement sélectionné")
        Me.createEquipmentReceipt.UseVisualStyleBackColor = False
        '
        'modif
        '
        Me.modif.BackColor = System.Drawing.SystemColors.Control
        Me.modif.Cursor = System.Windows.Forms.Cursors.Default
        Me.modif.Enabled = False
        Me.modif.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modif.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modif.Location = New System.Drawing.Point(438, 375)
        Me.modif.Name = "modif"
        Me.modif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modif.Size = New System.Drawing.Size(24, 24)
        Me.modif.TabIndex = 107
        Me.modif.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modif, "Enregistrer l'item sélectionné")
        Me.modif.UseVisualStyleBackColor = False
        '
        'frameE1
        '
        Me.frameE1.Controls.Add(Me.eDescription)
        Me.frameE1.Controls.Add(Me.panel1)
        Me.frameE1.Controls.Add(Me.frameE2)
        Me.frameE1.Controls.Add(Me.eProfit)
        Me.frameE1.Controls.Add(Me.label16)
        Me.frameE1.Controls.Add(Me.eTotal)
        Me.frameE1.Controls.Add(Me.label17)
        Me.frameE1.Controls.Add(Me.label15)
        Me.frameE1.Controls.Add(Me.label14)
        Me.frameE1.Controls.Add(Me.etrp)
        Me.frameE1.Controls.Add(Me.eNoItem)
        Me.frameE1.Controls.Add(Me.eNom)
        Me.frameE1.Controls.Add(Me.eDate)
        Me.frameE1.Controls.Add(Me.label7)
        Me.frameE1.Controls.Add(Me.label6)
        Me.frameE1.Controls.Add(Me.label5)
        Me.frameE1.Controls.Add(Me.label2)
        Me.frameE1.Controls.Add(Me.label1)
        Me.frameE1.Location = New System.Drawing.Point(0, 0)
        Me.frameE1.Name = "frameE1"
        Me.frameE1.Size = New System.Drawing.Size(552, 338)
        Me.frameE1.TabIndex = 65
        Me.frameE1.TabStop = False
        Me.frameE1.Text = "Informations générales"
        '
        'eDescription
        '
        Me.eDescription.BackColor = System.Drawing.SystemColors.Control
        Me.eDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.eDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eDescription.Location = New System.Drawing.Point(5, 122)
        Me.eDescription.Multiline = True
        Me.eDescription.Name = "eDescription"
        Me.eDescription.ReadOnly = True
        Me.eDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.eDescription.Size = New System.Drawing.Size(541, 31)
        Me.eDescription.TabIndex = 72
        '
        'panel1
        '
        Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel1.Controls.Add(Me.eType2)
        Me.panel1.Controls.Add(Me.eType1)
        Me.panel1.Location = New System.Drawing.Point(8, 16)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(528, 16)
        Me.panel1.TabIndex = 80
        '
        'eType2
        '
        Me.eType2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eType2.Location = New System.Drawing.Point(271, 0)
        Me.eType2.Name = "eType2"
        Me.eType2.Size = New System.Drawing.Size(48, 16)
        Me.eType2.TabIndex = 1
        Me.eType2.Text = "Prêt"
        '
        'eType1
        '
        Me.eType1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eType1.Location = New System.Drawing.Point(207, 0)
        Me.eType1.Name = "eType1"
        Me.eType1.Size = New System.Drawing.Size(56, 16)
        Me.eType1.TabIndex = 0
        Me.eType1.Text = "Vente"
        '
        'frameE2
        '
        Me.frameE2.BackColor = System.Drawing.Color.Transparent
        Me.frameE2.Controls.Add(Me.eVerified)
        Me.frameE2.Controls.Add(Me.eReturned)
        Me.frameE2.Controls.Add(Me.eDuree)
        Me.frameE2.Controls.Add(Me.eSignature)
        Me.frameE2.Controls.Add(Me.eNotes)
        Me.frameE2.Controls.Add(Me.anretour)
        Me.frameE2.Controls.Add(Me.moisretour)
        Me.frameE2.Controls.Add(Me.jourretour)
        Me.frameE2.Controls.Add(Me.eDepot)
        Me.frameE2.Controls.Add(Me.label20)
        Me.frameE2.Controls.Add(Me.ePret)
        Me.frameE2.Controls.Add(Me.label19)
        Me.frameE2.Controls.Add(Me.eRefund)
        Me.frameE2.Controls.Add(Me.label13)
        Me.frameE2.Controls.Add(Me.label11)
        Me.frameE2.Controls.Add(Me.eRefunded)
        Me.frameE2.Controls.Add(Me.label18)
        Me.frameE2.Controls.Add(Me.label12)
        Me.frameE2.Controls.Add(Me.refundlabel)
        Me.frameE2.Controls.Add(Me.label10)
        Me.frameE2.Controls.Add(Me.label9)
        Me.frameE2.Controls.Add(Me.label8)
        Me.frameE2.Location = New System.Drawing.Point(0, 160)
        Me.frameE2.Name = "frameE2"
        Me.frameE2.Size = New System.Drawing.Size(552, 179)
        Me.frameE2.TabIndex = 79
        Me.frameE2.TabStop = False
        Me.frameE2.Text = "Informations sur le prêt"
        '
        'eVerified
        '
        Me.eVerified.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.eVerified.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eVerified.Location = New System.Drawing.Point(8, 88)
        Me.eVerified.Name = "eVerified"
        Me.eVerified.Size = New System.Drawing.Size(152, 16)
        Me.eVerified.TabIndex = 107
        Me.eVerified.Text = "Vérifié par le thérapeute :"
        '
        'eReturned
        '
        Me.eReturned.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.eReturned.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eReturned.Location = New System.Drawing.Point(176, 104)
        Me.eReturned.Name = "eReturned"
        Me.eReturned.Size = New System.Drawing.Size(80, 16)
        Me.eReturned.TabIndex = 106
        Me.eReturned.Text = "Retourné :"
        '
        'eDuree
        '
        Me.eDuree.acceptAlpha = True
        Me.eDuree.acceptedChars = Nothing
        Me.eDuree.acceptNumeric = True
        Me.eDuree.allCapital = False
        Me.eDuree.allLower = False
        Me.eDuree.autoComplete = True
        Me.eDuree.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.eDuree.autoSizeDropDown = True
        Me.eDuree.BackColor = System.Drawing.Color.White
        Me.eDuree.blockOnMaximum = False
        Me.eDuree.blockOnMinimum = False
        Me.eDuree.cb_AcceptLeftZeros = False
        Me.eDuree.cb_AcceptNegative = False
        Me.eDuree.currencyBox = False
        Me.eDuree.dbField = Nothing
        Me.eDuree.doComboDelete = True
        Me.eDuree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.eDuree.firstLetterCapital = False
        Me.eDuree.firstLettersCapital = False
        Me.eDuree.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eDuree.IntegralHeight = False
        Me.eDuree.Items.AddRange(New Object() {"1 jour", "2 jours", "3 jours", "4 jours", "5 jours", "6 jours", "1 semaine", "1 semaine et 1 jour", "1 semaine et 2 jours", "1 semaine et 3 jours", "1 semaine et 4 jours", "1 semaine et 5 jours", "1 semaine et 6 jours", "2 semaines", "2 semaines et 1 jour", "2 semaines et 2 jours", "2 semaines et 3 jours", "2 semaines et 4 jours", "2 semaines et 5 jours", "2 semaines et 6 jours", "3 semaines", "3 semaines et 1 jour", "3 semaines et 2 jours", "3 semaines et 3 jours", "3 semaines et 4 jours", "3 semaines et 5 jours", "3 semaines et 6 jours", "4 semaines", "4 semaines et 1 jour", "4 semaines et 2 jours", "4 semaines et 3 jours", "4 semaines et 4 jours", "4 semaines et 5 jours", "4 semaines et 6 jours", "1 mois", "Plus d'un mois", "Date de retour invalide"})
        Me.eDuree.itemsToolTipDuration = 10000
        Me.eDuree.Location = New System.Drawing.Point(354, 16)
        Me.eDuree.manageText = True
        Me.eDuree.matchExp = Nothing
        Me.eDuree.maximum = 0
        Me.eDuree.minimum = 0
        Me.eDuree.Name = "eDuree"
        Me.eDuree.nbDecimals = CType(-1, Short)
        Me.eDuree.onlyAlphabet = False
        Me.eDuree.pathOfList = ""
        Me.eDuree.ReadOnly = False
        Me.eDuree.refuseAccents = False
        Me.eDuree.refusedChars = Nothing
        Me.eDuree.showItemsToolTip = False
        Me.eDuree.Size = New System.Drawing.Size(192, 22)
        Me.eDuree.TabIndex = 105
        Me.eDuree.trimText = False
        '
        'eSignature
        '
        Me.eSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.eSignature.Location = New System.Drawing.Point(72, 136)
        Me.eSignature.Name = "eSignature"
        Me.eSignature.Size = New System.Drawing.Size(208, 24)
        Me.eSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.eSignature.TabIndex = 104
        Me.eSignature.TabStop = False
        '
        'eNotes
        '
        Me.eNotes.acceptAlpha = True
        Me.eNotes.acceptedChars = ""
        Me.eNotes.acceptNumeric = True
        Me.eNotes.AcceptsReturn = True
        Me.eNotes.allCapital = False
        Me.eNotes.allLower = False
        Me.eNotes.BackColor = System.Drawing.SystemColors.Window
        Me.eNotes.blockOnMaximum = False
        Me.eNotes.blockOnMinimum = False
        Me.eNotes.cb_AcceptLeftZeros = False
        Me.eNotes.cb_AcceptNegative = False
        Me.eNotes.currencyBox = False
        Me.eNotes.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.eNotes.firstLetterCapital = True
        Me.eNotes.firstLettersCapital = False
        Me.eNotes.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eNotes.ForeColor = System.Drawing.SystemColors.WindowText
        Me.eNotes.Location = New System.Drawing.Point(286, 104)
        Me.eNotes.manageText = True
        Me.eNotes.matchExp = ""
        Me.eNotes.maximum = 0
        Me.eNotes.MaxLength = 200
        Me.eNotes.minimum = 0
        Me.eNotes.Multiline = True
        Me.eNotes.Name = "eNotes"
        Me.eNotes.nbDecimals = CType(-1, Short)
        Me.eNotes.onlyAlphabet = False
        Me.eNotes.refuseAccents = False
        Me.eNotes.refusedChars = ""
        Me.eNotes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.eNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.eNotes.showInternalContextMenu = True
        Me.eNotes.Size = New System.Drawing.Size(263, 73)
        Me.eNotes.TabIndex = 103
        Me.eNotes.trimText = False
        '
        'anretour
        '
        Me.anretour.acceptAlpha = False
        Me.anretour.acceptedChars = Nothing
        Me.anretour.acceptNumeric = True
        Me.anretour.allCapital = False
        Me.anretour.allLower = False
        Me.anretour.autoComplete = True
        Me.anretour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.anretour.autoSizeDropDown = True
        Me.anretour.BackColor = System.Drawing.Color.White
        Me.anretour.blockOnMaximum = False
        Me.anretour.blockOnMinimum = False
        Me.anretour.cb_AcceptLeftZeros = False
        Me.anretour.cb_AcceptNegative = False
        Me.anretour.currencyBox = False
        Me.anretour.Cursor = System.Windows.Forms.Cursors.Default
        Me.anretour.dbField = Nothing
        Me.anretour.doComboDelete = True
        Me.anretour.firstLetterCapital = False
        Me.anretour.firstLettersCapital = False
        Me.anretour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.anretour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.anretour.IntegralHeight = False
        Me.anretour.ItemHeight = 14
        Me.anretour.itemsToolTipDuration = 10000
        Me.anretour.Location = New System.Drawing.Point(96, 16)
        Me.anretour.manageText = True
        Me.anretour.matchExp = Nothing
        Me.anretour.maximum = 0
        Me.anretour.minimum = 0
        Me.anretour.Name = "anretour"
        Me.anretour.nbDecimals = CType(-1, Short)
        Me.anretour.onlyAlphabet = False
        Me.anretour.pathOfList = ""
        Me.anretour.ReadOnly = False
        Me.anretour.refuseAccents = False
        Me.anretour.refusedChars = ""
        Me.anretour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.anretour.showItemsToolTip = False
        Me.anretour.Size = New System.Drawing.Size(56, 22)
        Me.anretour.TabIndex = 100
        Me.anretour.trimText = False
        '
        'moisretour
        '
        Me.moisretour.acceptAlpha = False
        Me.moisretour.acceptedChars = Nothing
        Me.moisretour.acceptNumeric = True
        Me.moisretour.allCapital = False
        Me.moisretour.allLower = False
        Me.moisretour.autoComplete = True
        Me.moisretour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.moisretour.autoSizeDropDown = True
        Me.moisretour.BackColor = System.Drawing.Color.White
        Me.moisretour.blockOnMaximum = True
        Me.moisretour.blockOnMinimum = True
        Me.moisretour.cb_AcceptLeftZeros = True
        Me.moisretour.cb_AcceptNegative = False
        Me.moisretour.currencyBox = False
        Me.moisretour.Cursor = System.Windows.Forms.Cursors.Default
        Me.moisretour.dbField = Nothing
        Me.moisretour.doComboDelete = True
        Me.moisretour.firstLetterCapital = False
        Me.moisretour.firstLettersCapital = False
        Me.moisretour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.moisretour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.moisretour.IntegralHeight = False
        Me.moisretour.ItemHeight = 14
        Me.moisretour.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.moisretour.itemsToolTipDuration = 10000
        Me.moisretour.Location = New System.Drawing.Point(152, 16)
        Me.moisretour.manageText = True
        Me.moisretour.matchExp = Nothing
        Me.moisretour.maximum = 12
        Me.moisretour.MaxLength = 2
        Me.moisretour.minimum = 1
        Me.moisretour.Name = "moisretour"
        Me.moisretour.nbDecimals = CType(-1, Short)
        Me.moisretour.onlyAlphabet = False
        Me.moisretour.pathOfList = ""
        Me.moisretour.ReadOnly = False
        Me.moisretour.refuseAccents = False
        Me.moisretour.refusedChars = ""
        Me.moisretour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.moisretour.showItemsToolTip = False
        Me.moisretour.Size = New System.Drawing.Size(48, 22)
        Me.moisretour.TabIndex = 101
        Me.moisretour.trimText = False
        '
        'jourretour
        '
        Me.jourretour.acceptAlpha = False
        Me.jourretour.acceptedChars = Nothing
        Me.jourretour.acceptNumeric = True
        Me.jourretour.allCapital = False
        Me.jourretour.allLower = False
        Me.jourretour.autoComplete = True
        Me.jourretour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.jourretour.autoSizeDropDown = True
        Me.jourretour.BackColor = System.Drawing.Color.White
        Me.jourretour.blockOnMaximum = True
        Me.jourretour.blockOnMinimum = True
        Me.jourretour.cb_AcceptLeftZeros = True
        Me.jourretour.cb_AcceptNegative = False
        Me.jourretour.currencyBox = False
        Me.jourretour.Cursor = System.Windows.Forms.Cursors.Default
        Me.jourretour.dbField = Nothing
        Me.jourretour.doComboDelete = True
        Me.jourretour.firstLetterCapital = False
        Me.jourretour.firstLettersCapital = False
        Me.jourretour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.jourretour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.jourretour.IntegralHeight = False
        Me.jourretour.ItemHeight = 14
        Me.jourretour.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.jourretour.itemsToolTipDuration = 10000
        Me.jourretour.Location = New System.Drawing.Point(200, 16)
        Me.jourretour.manageText = True
        Me.jourretour.matchExp = Nothing
        Me.jourretour.maximum = 31
        Me.jourretour.MaxLength = 2
        Me.jourretour.minimum = 1
        Me.jourretour.Name = "jourretour"
        Me.jourretour.nbDecimals = CType(-1, Short)
        Me.jourretour.onlyAlphabet = False
        Me.jourretour.pathOfList = ""
        Me.jourretour.ReadOnly = False
        Me.jourretour.refuseAccents = False
        Me.jourretour.refusedChars = ""
        Me.jourretour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.jourretour.showItemsToolTip = False
        Me.jourretour.Size = New System.Drawing.Size(48, 22)
        Me.jourretour.TabIndex = 102
        Me.jourretour.trimText = False
        '
        'eDepot
        '
        Me.eDepot.acceptAlpha = False
        Me.eDepot.acceptedChars = ",§."
        Me.eDepot.acceptNumeric = True
        Me.eDepot.allCapital = False
        Me.eDepot.allLower = False
        Me.eDepot.BackColor = System.Drawing.Color.White
        Me.eDepot.blockOnMaximum = False
        Me.eDepot.blockOnMinimum = False
        Me.eDepot.cb_AcceptLeftZeros = False
        Me.eDepot.cb_AcceptNegative = False
        Me.eDepot.currencyBox = True
        Me.eDepot.firstLetterCapital = False
        Me.eDepot.firstLettersCapital = False
        Me.eDepot.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eDepot.Location = New System.Drawing.Point(104, 55)
        Me.eDepot.manageText = True
        Me.eDepot.matchExp = ""
        Me.eDepot.maximum = 0
        Me.eDepot.MaxLength = 6
        Me.eDepot.minimum = 0
        Me.eDepot.Multiline = True
        Me.eDepot.Name = "eDepot"
        Me.eDepot.nbDecimals = CType(2, Short)
        Me.eDepot.onlyAlphabet = False
        Me.eDepot.refuseAccents = False
        Me.eDepot.refusedChars = ""
        Me.eDepot.showInternalContextMenu = True
        Me.eDepot.Size = New System.Drawing.Size(40, 16)
        Me.eDepot.TabIndex = 98
        Me.eDepot.Text = "0"
        Me.eDepot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.eDepot.trimText = False
        '
        'label20
        '
        Me.label20.AutoSize = True
        Me.label20.BackColor = System.Drawing.SystemColors.Control
        Me.label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.label20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label20.Location = New System.Drawing.Point(144, 55)
        Me.label20.Name = "label20"
        Me.label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label20.Size = New System.Drawing.Size(13, 14)
        Me.label20.TabIndex = 99
        Me.label20.Text = "$"
        '
        'ePret
        '
        Me.ePret.acceptAlpha = False
        Me.ePret.acceptedChars = ",§."
        Me.ePret.acceptNumeric = True
        Me.ePret.allCapital = False
        Me.ePret.allLower = False
        Me.ePret.BackColor = System.Drawing.Color.White
        Me.ePret.blockOnMaximum = False
        Me.ePret.blockOnMinimum = False
        Me.ePret.cb_AcceptLeftZeros = False
        Me.ePret.cb_AcceptNegative = False
        Me.ePret.currencyBox = True
        Me.ePret.firstLetterCapital = False
        Me.ePret.firstLettersCapital = False
        Me.ePret.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ePret.Location = New System.Drawing.Point(282, 55)
        Me.ePret.manageText = True
        Me.ePret.matchExp = ""
        Me.ePret.maximum = 0
        Me.ePret.MaxLength = 6
        Me.ePret.minimum = 0
        Me.ePret.Multiline = True
        Me.ePret.Name = "ePret"
        Me.ePret.nbDecimals = CType(2, Short)
        Me.ePret.onlyAlphabet = False
        Me.ePret.refuseAccents = False
        Me.ePret.refusedChars = ""
        Me.ePret.showInternalContextMenu = True
        Me.ePret.Size = New System.Drawing.Size(40, 16)
        Me.ePret.TabIndex = 96
        Me.ePret.Text = "0"
        Me.ePret.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ePret.trimText = False
        '
        'label19
        '
        Me.label19.AutoSize = True
        Me.label19.BackColor = System.Drawing.SystemColors.Control
        Me.label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.label19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label19.Location = New System.Drawing.Point(322, 55)
        Me.label19.Name = "label19"
        Me.label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label19.Size = New System.Drawing.Size(13, 14)
        Me.label19.TabIndex = 97
        Me.label19.Text = "$"
        '
        'eRefund
        '
        Me.eRefund.acceptAlpha = False
        Me.eRefund.acceptedChars = ",§."
        Me.eRefund.acceptNumeric = True
        Me.eRefund.allCapital = False
        Me.eRefund.allLower = False
        Me.eRefund.BackColor = System.Drawing.SystemColors.ControlLight
        Me.eRefund.blockOnMaximum = True
        Me.eRefund.blockOnMinimum = True
        Me.eRefund.cb_AcceptLeftZeros = False
        Me.eRefund.cb_AcceptNegative = False
        Me.eRefund.currencyBox = True
        Me.eRefund.firstLetterCapital = False
        Me.eRefund.firstLettersCapital = False
        Me.eRefund.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eRefund.Location = New System.Drawing.Point(492, 55)
        Me.eRefund.manageText = True
        Me.eRefund.matchExp = ""
        Me.eRefund.maximum = 0
        Me.eRefund.minimum = 0
        Me.eRefund.Multiline = True
        Me.eRefund.Name = "eRefund"
        Me.eRefund.nbDecimals = CType(2, Short)
        Me.eRefund.onlyAlphabet = False
        Me.eRefund.ReadOnly = True
        Me.eRefund.refuseAccents = False
        Me.eRefund.refusedChars = ""
        Me.eRefund.showInternalContextMenu = True
        Me.eRefund.Size = New System.Drawing.Size(40, 16)
        Me.eRefund.TabIndex = 94
        Me.eRefund.Text = "0"
        Me.eRefund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.eRefund.trimText = False
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.BackColor = System.Drawing.SystemColors.Control
        Me.label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label13.Location = New System.Drawing.Point(532, 55)
        Me.label13.Name = "label13"
        Me.label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label13.Size = New System.Drawing.Size(13, 14)
        Me.label13.TabIndex = 95
        Me.label13.Text = "$"
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.BackColor = System.Drawing.SystemColors.Control
        Me.label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label11.Location = New System.Drawing.Point(8, 16)
        Me.label11.Name = "label11"
        Me.label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label11.Size = New System.Drawing.Size(83, 14)
        Me.label11.TabIndex = 93
        Me.label11.Text = "Date de retour :"
        '
        'eRefunded
        '
        Me.eRefunded.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.eRefunded.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eRefunded.Location = New System.Drawing.Point(168, 88)
        Me.eRefunded.Name = "eRefunded"
        Me.eRefunded.Size = New System.Drawing.Size(88, 16)
        Me.eRefunded.TabIndex = 92
        Me.eRefunded.Text = "Remboursé :"
        '
        'label18
        '
        Me.label18.AutoSize = True
        Me.label18.BackColor = System.Drawing.SystemColors.Control
        Me.label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.label18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label18.Location = New System.Drawing.Point(8, 140)
        Me.label18.Name = "label18"
        Me.label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label18.Size = New System.Drawing.Size(60, 14)
        Me.label18.TabIndex = 91
        Me.label18.Text = "Signature :"
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.BackColor = System.Drawing.SystemColors.Control
        Me.label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.label12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label12.Location = New System.Drawing.Point(286, 88)
        Me.label12.Name = "label12"
        Me.label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label12.Size = New System.Drawing.Size(42, 14)
        Me.label12.TabIndex = 90
        Me.label12.Text = "Notes :"
        '
        'refundlabel
        '
        Me.refundlabel.AutoSize = True
        Me.refundlabel.BackColor = System.Drawing.SystemColors.Control
        Me.refundlabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.refundlabel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.refundlabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.refundlabel.Location = New System.Drawing.Point(364, 55)
        Me.refundlabel.Name = "refundlabel"
        Me.refundlabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.refundlabel.Size = New System.Drawing.Size(122, 14)
        Me.refundlabel.TabIndex = 89
        Me.refundlabel.Text = "Montant à rembourser :"
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.BackColor = System.Drawing.SystemColors.Control
        Me.label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label10.Location = New System.Drawing.Point(202, 55)
        Me.label10.Name = "label10"
        Me.label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label10.Size = New System.Drawing.Size(73, 14)
        Me.label10.TabIndex = 88
        Me.label10.Text = "Coût du prêt :"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.SystemColors.Control
        Me.label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label9.Location = New System.Drawing.Point(266, 16)
        Me.label9.Name = "label9"
        Me.label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label9.Size = New System.Drawing.Size(80, 14)
        Me.label9.TabIndex = 87
        Me.label9.Text = "Durée du prêt :"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.SystemColors.Control
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(8, 55)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(97, 14)
        Me.label8.TabIndex = 86
        Me.label8.Text = "Montant du dépôt :"
        '
        'eProfit
        '
        Me.eProfit.acceptAlpha = False
        Me.eProfit.acceptedChars = ",§.§-"
        Me.eProfit.acceptNumeric = True
        Me.eProfit.allCapital = False
        Me.eProfit.allLower = False
        Me.eProfit.BackColor = System.Drawing.Color.White
        Me.eProfit.blockOnMaximum = False
        Me.eProfit.blockOnMinimum = False
        Me.eProfit.cb_AcceptLeftZeros = False
        Me.eProfit.cb_AcceptNegative = True
        Me.eProfit.currencyBox = True
        Me.eProfit.firstLetterCapital = False
        Me.eProfit.firstLettersCapital = False
        Me.eProfit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eProfit.Location = New System.Drawing.Point(283, 72)
        Me.eProfit.manageText = True
        Me.eProfit.matchExp = ""
        Me.eProfit.maximum = 0
        Me.eProfit.MaxLength = 6
        Me.eProfit.minimum = 0
        Me.eProfit.Multiline = True
        Me.eProfit.Name = "eProfit"
        Me.eProfit.nbDecimals = CType(2, Short)
        Me.eProfit.onlyAlphabet = False
        Me.eProfit.refuseAccents = False
        Me.eProfit.refusedChars = ""
        Me.eProfit.showInternalContextMenu = True
        Me.eProfit.Size = New System.Drawing.Size(40, 16)
        Me.eProfit.TabIndex = 74
        Me.eProfit.Text = "0"
        Me.eProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.eProfit.trimText = False
        '
        'label16
        '
        Me.label16.AutoSize = True
        Me.label16.BackColor = System.Drawing.SystemColors.Control
        Me.label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.label16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label16.Location = New System.Drawing.Point(529, 72)
        Me.label16.Name = "label16"
        Me.label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label16.Size = New System.Drawing.Size(13, 14)
        Me.label16.TabIndex = 78
        Me.label16.Text = "$"
        '
        'eTotal
        '
        Me.eTotal.acceptAlpha = False
        Me.eTotal.acceptedChars = ",§."
        Me.eTotal.acceptNumeric = True
        Me.eTotal.allCapital = False
        Me.eTotal.allLower = False
        Me.eTotal.BackColor = System.Drawing.SystemColors.ControlLight
        Me.eTotal.blockOnMaximum = False
        Me.eTotal.blockOnMinimum = False
        Me.eTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.eTotal.cb_AcceptLeftZeros = False
        Me.eTotal.cb_AcceptNegative = False
        Me.eTotal.currencyBox = True
        Me.eTotal.firstLetterCapital = False
        Me.eTotal.firstLettersCapital = False
        Me.eTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eTotal.Location = New System.Drawing.Point(489, 72)
        Me.eTotal.manageText = True
        Me.eTotal.matchExp = ""
        Me.eTotal.maximum = 0
        Me.eTotal.minimum = 0
        Me.eTotal.Name = "eTotal"
        Me.eTotal.nbDecimals = CType(2, Short)
        Me.eTotal.onlyAlphabet = False
        Me.eTotal.ReadOnly = True
        Me.eTotal.refuseAccents = False
        Me.eTotal.refusedChars = ""
        Me.eTotal.showInternalContextMenu = True
        Me.eTotal.Size = New System.Drawing.Size(40, 13)
        Me.eTotal.TabIndex = 77
        Me.eTotal.Text = "0"
        Me.eTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.eTotal.trimText = False
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.BackColor = System.Drawing.SystemColors.Control
        Me.label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label17.Location = New System.Drawing.Point(407, 72)
        Me.label17.Name = "label17"
        Me.label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label17.Size = New System.Drawing.Size(76, 14)
        Me.label17.TabIndex = 76
        Me.label17.Text = "Montant total :"
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.BackColor = System.Drawing.SystemColors.Control
        Me.label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.label15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label15.Location = New System.Drawing.Point(323, 72)
        Me.label15.Name = "label15"
        Me.label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label15.Size = New System.Drawing.Size(13, 14)
        Me.label15.TabIndex = 75
        Me.label15.Text = "$"
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.BackColor = System.Drawing.SystemColors.Control
        Me.label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.label14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label14.Location = New System.Drawing.Point(168, 72)
        Me.label14.Name = "label14"
        Me.label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label14.Size = New System.Drawing.Size(112, 14)
        Me.label14.TabIndex = 73
        Me.label14.Text = "Profit de transaction :"
        '
        'etrp
        '
        Me.etrp.acceptAlpha = True
        Me.etrp.acceptedChars = Nothing
        Me.etrp.acceptNumeric = True
        Me.etrp.allCapital = False
        Me.etrp.allLower = False
        Me.etrp.autoComplete = True
        Me.etrp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.etrp.autoSizeDropDown = True
        Me.etrp.BackColor = System.Drawing.Color.White
        Me.etrp.blockOnMaximum = False
        Me.etrp.blockOnMinimum = False
        Me.etrp.cb_AcceptLeftZeros = False
        Me.etrp.cb_AcceptNegative = False
        Me.etrp.currencyBox = False
        Me.etrp.dbField = Nothing
        Me.etrp.doComboDelete = True
        Me.etrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.etrp.firstLetterCapital = False
        Me.etrp.firstLettersCapital = False
        Me.etrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etrp.IntegralHeight = False
        Me.etrp.itemsToolTipDuration = 10000
        Me.etrp.Location = New System.Drawing.Point(342, 96)
        Me.etrp.manageText = True
        Me.etrp.matchExp = Nothing
        Me.etrp.maximum = 0
        Me.etrp.minimum = 0
        Me.etrp.Name = "etrp"
        Me.etrp.nbDecimals = CType(-1, Short)
        Me.etrp.onlyAlphabet = False
        Me.etrp.pathOfList = ""
        Me.etrp.ReadOnly = False
        Me.etrp.refuseAccents = False
        Me.etrp.refusedChars = Nothing
        Me.etrp.showItemsToolTip = False
        Me.etrp.Size = New System.Drawing.Size(203, 22)
        Me.etrp.Sorted = True
        Me.etrp.TabIndex = 71
        Me.etrp.trimText = False
        '
        'eNoItem
        '
        Me.eNoItem.acceptAlpha = True
        Me.eNoItem.acceptedChars = Nothing
        Me.eNoItem.acceptNumeric = True
        Me.eNoItem.allCapital = False
        Me.eNoItem.allLower = False
        Me.eNoItem.autoComplete = True
        Me.eNoItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.eNoItem.autoSizeDropDown = True
        Me.eNoItem.BackColor = System.Drawing.Color.White
        Me.eNoItem.blockOnMaximum = False
        Me.eNoItem.blockOnMinimum = False
        Me.eNoItem.cb_AcceptLeftZeros = False
        Me.eNoItem.cb_AcceptNegative = False
        Me.eNoItem.currencyBox = False
        Me.eNoItem.dbField = Nothing
        Me.eNoItem.doComboDelete = True
        Me.eNoItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.eNoItem.firstLetterCapital = False
        Me.eNoItem.firstLettersCapital = False
        Me.eNoItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eNoItem.IntegralHeight = False
        Me.eNoItem.itemsToolTipDuration = 10000
        Me.eNoItem.Location = New System.Drawing.Point(395, 42)
        Me.eNoItem.manageText = True
        Me.eNoItem.matchExp = Nothing
        Me.eNoItem.maximum = 0
        Me.eNoItem.minimum = 0
        Me.eNoItem.Name = "eNoItem"
        Me.eNoItem.nbDecimals = CType(-1, Short)
        Me.eNoItem.onlyAlphabet = False
        Me.eNoItem.pathOfList = ""
        Me.eNoItem.ReadOnly = False
        Me.eNoItem.refuseAccents = False
        Me.eNoItem.refusedChars = Nothing
        Me.eNoItem.showItemsToolTip = False
        Me.eNoItem.Size = New System.Drawing.Size(150, 22)
        Me.eNoItem.Sorted = True
        Me.eNoItem.TabIndex = 70
        Me.eNoItem.trimText = False
        '
        'eNom
        '
        Me.eNom.acceptAlpha = True
        Me.eNom.acceptedChars = Nothing
        Me.eNom.acceptNumeric = True
        Me.eNom.allCapital = False
        Me.eNom.allLower = False
        Me.eNom.autoComplete = True
        Me.eNom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.eNom.autoSizeDropDown = True
        Me.eNom.BackColor = System.Drawing.Color.White
        Me.eNom.blockOnMaximum = False
        Me.eNom.blockOnMinimum = False
        Me.eNom.cb_AcceptLeftZeros = False
        Me.eNom.cb_AcceptNegative = False
        Me.eNom.currencyBox = False
        Me.eNom.dbField = Nothing
        Me.eNom.doComboDelete = True
        Me.eNom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.eNom.firstLetterCapital = False
        Me.eNom.firstLettersCapital = False
        Me.eNom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eNom.IntegralHeight = False
        Me.eNom.itemsToolTipDuration = 10000
        Me.eNom.Location = New System.Drawing.Point(43, 42)
        Me.eNom.manageText = True
        Me.eNom.matchExp = Nothing
        Me.eNom.maximum = 0
        Me.eNom.minimum = 0
        Me.eNom.Name = "eNom"
        Me.eNom.nbDecimals = CType(-1, Short)
        Me.eNom.onlyAlphabet = False
        Me.eNom.pathOfList = ""
        Me.eNom.ReadOnly = False
        Me.eNom.refuseAccents = False
        Me.eNom.refusedChars = Nothing
        Me.eNom.showItemsToolTip = False
        Me.eNom.Size = New System.Drawing.Size(224, 22)
        Me.eNom.Sorted = True
        Me.eNom.TabIndex = 69
        Me.eNom.trimText = False
        '
        'eDate
        '
        Me.eDate.BackColor = System.Drawing.SystemColors.Control
        Me.eDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.eDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eDate.Location = New System.Drawing.Point(115, 72)
        Me.eDate.Name = "eDate"
        Me.eDate.ReadOnly = True
        Me.eDate.Size = New System.Drawing.Size(56, 13)
        Me.eDate.TabIndex = 67
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.SystemColors.Control
        Me.label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label7.Location = New System.Drawing.Point(272, 99)
        Me.label7.Name = "label7"
        Me.label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label7.Size = New System.Drawing.Size(69, 14)
        Me.label7.TabIndex = 64
        Me.label7.Text = "Thérapeute :"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.SystemColors.Control
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(5, 106)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(70, 14)
        Me.label6.TabIndex = 63
        Me.label6.Text = "Description :"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.SystemColors.Control
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(3, 45)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(37, 14)
        Me.label5.TabIndex = 62
        Me.label5.Text = "Item : "
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(298, 45)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(96, 14)
        Me.label2.TabIndex = 61
        Me.label2.Text = "Numéro de l'item :"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(3, 72)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(109, 14)
        Me.label1.TabIndex = 60
        Me.label1.Text = "Date de transaction :"
        '
        '_ongletsdossier_TabPage1
        '
        Me._ongletsdossier_TabPage1.Controls.Add(Me.ctlFolderInfos)
        Me._ongletsdossier_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._ongletsdossier_TabPage1.Name = "_ongletsdossier_TabPage1"
        Me._ongletsdossier_TabPage1.Size = New System.Drawing.Size(552, 502)
        Me._ongletsdossier_TabPage1.TabIndex = 5
        Me._ongletsdossier_TabPage1.Text = "Infos"
        Me._ongletsdossier_TabPage1.UseVisualStyleBackColor = True
        '
        'ctlFolderInfos
        '
        Me.ctlFolderInfos.dateAccident = ""
        Me.ctlFolderInfos.dateAppel = ""
        Me.ctlFolderInfos.dateDebut = ""
        Me.ctlFolderInfos.dateEval = ""
        Me.ctlFolderInfos.dateFin = ""
        Me.ctlFolderInfos.dateRechute = ""
        Me.ctlFolderInfos.dateReference = ""
        Me.ctlFolderInfos.dateReferenceReception = ""
        Me.ctlFolderInfos.diagnostic = ""
        Me.ctlFolderInfos.duree = -1
        Me.ctlFolderInfos.frequence = -1
        Me.ctlFolderInfos.isFolderModified = False
        Me.ctlFolderInfos.Location = New System.Drawing.Point(0, 0)
        Me.ctlFolderInfos.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlFolderInfos.medecin = ""
        Me.ctlFolderInfos.MinimumSize = New System.Drawing.Size(420, 426)
        Me.ctlFolderInfos.Name = "ctlFolderInfos"
        Me.ctlFolderInfos.nbAbsences = ""
        Me.ctlFolderInfos.nbPresences = ""
        Me.ctlFolderInfos.noClient = 0
        Me.ctlFolderInfos.noFolder = 0
        Me.ctlFolderInfos.noRef = ""
        Me.ctlFolderInfos.noRefMedecin = ""
        Me.ctlFolderInfos.remarques = ""
        Me.ctlFolderInfos.service = ""
        Me.ctlFolderInfos.siteLesion = ""
        Me.ctlFolderInfos.Size = New System.Drawing.Size(554, 502)
        Me.ctlFolderInfos.TabIndex = 0
        Me.ctlFolderInfos.trpDemande = 0
        Me.ctlFolderInfos.trpNoPermis = ""
        Me.ctlFolderInfos.trpToTransfer = 0
        Me.ctlFolderInfos.trpTraitant = ""
        '
        '_ongletsdossier_TabPage0
        '
        Me._ongletsdossier_TabPage0.Controls.Add(Me.folderText)
        Me._ongletsdossier_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._ongletsdossier_TabPage0.Name = "_ongletsdossier_TabPage0"
        Me._ongletsdossier_TabPage0.Size = New System.Drawing.Size(552, 502)
        Me._ongletsdossier_TabPage0.TabIndex = 0
        Me._ongletsdossier_TabPage0.Text = "Textes"
        Me._ongletsdossier_TabPage0.UseVisualStyleBackColor = True
        '
        'folderText
        '
        Me.folderText.activateLinksOnEdit = True
        Me.folderText.allowContextMenu = True
        Me.folderText.allowEditorContextMenu = True
        Me.folderText.allowNavigation = False
        Me.folderText.allowPopupWindows = True
        Me.folderText.allowRefresh = False
        Me.folderText.allowUndo = True
        Me.folderText.ancre = Nothing
        Me.folderText.ancreActif = False
        Me.folderText.editorContextMenu = Nothing
        Me.folderText.editorHeight = 350
        Me.folderText.editorURL = ""
        Me.folderText.editorWidth = 460
        Me.folderText.htmlPageURL = Nothing
        Me.folderText.Location = New System.Drawing.Point(0, 8)
        Me.folderText.Name = "folderText"
        Me.folderText.Silent = False
        Me.folderText.Size = New System.Drawing.Size(552, 491)
        Me.folderText.startupPos = 0
        Me.folderText.TabIndex = 0
        Me.folderText.toolBarStyles = 1
        Me.folderText.useNavigationCache = True
        Me.folderText.viewDisableHtmlFields = True
        '
        '_ongletsclient_TabPage2
        '
        Me._ongletsclient_TabPage2.Controls.Add(Me.Label24)
        Me._ongletsclient_TabPage2.Controls.Add(Me.visiteHistory)
        Me._ongletsclient_TabPage2.Controls.Add(Me.visitesList)
        Me._ongletsclient_TabPage2.Controls.Add(Me.dossiersVlist)
        Me._ongletsclient_TabPage2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ongletsclient_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._ongletsclient_TabPage2.Name = "_ongletsclient_TabPage2"
        Me._ongletsclient_TabPage2.Size = New System.Drawing.Size(561, 598)
        Me._ongletsclient_TabPage2.TabIndex = 2
        Me._ongletsclient_TabPage2.Text = "Rendez-vous"
        Me._ongletsclient_TabPage2.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(-1, 428)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(228, 14)
        Me.Label24.TabIndex = 285
        Me.Label24.Text = "Historique du rendez-vous sélectionné :"
        '
        'visiteHistory
        '
        Me.visiteHistory.AllowUserToAddRows = False
        Me.visiteHistory.AllowUserToDeleteRows = False
        Me.visiteHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.visiteHistory.autoSelectOnDataSourceChanged = True
        Me.visiteHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.visiteHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.visiteHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn5})
        Me.visiteHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.visiteHistory.isDoubleBuffered = False
        Me.visiteHistory.Location = New System.Drawing.Point(0, 445)
        Me.visiteHistory.Name = "visiteHistory"
        Me.visiteHistory.ReadOnly = True
        Me.visiteHistory.RowHeadersVisible = False
        Me.visiteHistory.RowHeadersWidth = 20
        Me.visiteHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.visiteHistory.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.visiteHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.visiteHistory.Size = New System.Drawing.Size(560, 153)
        Me.visiteHistory.TabIndex = 284
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "DateHeureCreation"
        DataGridViewCellStyle10.Format = "g"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn1.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 54
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "NomAction"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Action"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 63
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "ByUser"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Fait par"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 68
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Raison"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Raison"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 65
        '
        'visitesList
        '
        Me.visitesList.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.visitesList.autoAdjust = True
        Me.visitesList.autoKeyDownSelection = True
        Me.visitesList.autoSizeHorizontally = False
        Me.visitesList.autoSizeVertically = False
        Me.visitesList.BackColor = System.Drawing.Color.White
        Me.visitesList.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.visitesList.baseBackColor = System.Drawing.Color.White
        Me.visitesList.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.visitesList.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.visitesList.bgColor = System.Drawing.Color.White
        Me.visitesList.borderColor = System.Drawing.Color.Empty
        Me.visitesList.borderSelColor = System.Drawing.Color.Empty
        Me.visitesList.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.visitesList.CausesValidation = False
        Me.visitesList.clickEnabled = True
        Me.visitesList.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.visitesList.do3D = False
        Me.visitesList.draw = False
        Me.visitesList.extraWidth = 0
        Me.visitesList.hScrollColor = System.Drawing.SystemColors.Control
        Me.visitesList.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.visitesList.hScrolling = True
        Me.visitesList.hsValue = 0
        Me.visitesList.icons = CType(resources.GetObject("visitesList.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.visitesList.itemBorder = 0
        Me.visitesList.itemMargin = 0
        Me.visitesList.items = CType(resources.GetObject("visitesList.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.visitesList.Location = New System.Drawing.Point(0, 32)
        Me.visitesList.mouseMove3D = False
        Me.visitesList.mouseSpeed = 0
        Me.visitesList.Name = "visitesList"
        Me.visitesList.objMaxHeight = 0.0!
        Me.visitesList.objMaxWidth = 0.0!
        Me.visitesList.objMinHeight = 0.0!
        Me.visitesList.objMinWidth = 0.0!
        Me.visitesList.reverseSorting = True
        Me.visitesList.selected = -1
        Me.visitesList.selectedClickAllowed = True
        Me.visitesList.selectMultiple = False
        Me.visitesList.Size = New System.Drawing.Size(560, 393)
        Me.visitesList.sorted = True
        Me.visitesList.TabIndex = 108
        Me.visitesList.toolTipText = Nothing
        Me.visitesList.vScrollColor = System.Drawing.SystemColors.Control
        Me.visitesList.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.visitesList.vScrolling = True
        Me.visitesList.vsValue = 0
        '
        'dossiersVlist
        '
        Me.dossiersVlist.BackColor = System.Drawing.SystemColors.Window
        Me.dossiersVlist.Cursor = System.Windows.Forms.Cursors.Default
        Me.dossiersVlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dossiersVlist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dossiersVlist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dossiersVlist.Location = New System.Drawing.Point(0, 8)
        Me.dossiersVlist.Name = "dossiersVlist"
        Me.dossiersVlist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dossiersVlist.Size = New System.Drawing.Size(560, 22)
        Me.dossiersVlist.TabIndex = 104
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Nothing
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "DataTable1"
        '
        'StartStopBillChanging
        '
        Me.StartStopBillChanging.Enabled = False
        Me.StartStopBillChanging.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.StartStopBillChanging.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.StartStopBillChanging.Location = New System.Drawing.Point(899, 0)
        Me.StartStopBillChanging.Name = "StartStopBillChanging"
        Me.StartStopBillChanging.Size = New System.Drawing.Size(24, 24)
        Me.StartStopBillChanging.TabIndex = 149
        Me.StartStopBillChanging.TabStop = False
        Me.ToolTip1.SetToolTip(Me.StartStopBillChanging, "Commencer la modification des factures et paiements")
        '
        'DataTable1
        '
        Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4, Me.DataColumn5, Me.DataColumn6, Me.DataColumn7, Me.DataColumn8, Me.DataColumn9, Me.DataColumn10, Me.DataColumn11, Me.DataColumn12, Me.DataColumn13})
        Me.DataTable1.TableName = "DataTable1"
        '
        'DataColumn1
        '
        Me.DataColumn1.Caption = "Date"
        Me.DataColumn1.ColumnName = "Date"
        Me.DataColumn1.DataType = GetType(Date)
        '
        'DataColumn2
        '
        Me.DataColumn2.Caption = "Item"
        Me.DataColumn2.ColumnName = "Item"
        '
        'DataColumn3
        '
        Me.DataColumn3.Caption = "Thérapeute"
        Me.DataColumn3.ColumnName = "Thérapeute"
        '
        'DataColumn4
        '
        Me.DataColumn4.Caption = "Facture"
        Me.DataColumn4.ColumnName = "Facture"
        Me.DataColumn4.DataType = GetType(Short)
        '
        'DataColumn5
        '
        Me.DataColumn5.Caption = "Numéro de l'item"
        Me.DataColumn5.ColumnName = "Numéro de l'item"
        Me.DataColumn5.DataType = GetType(Short)
        '
        'DataColumn6
        '
        Me.DataColumn6.Caption = "Type"
        Me.DataColumn6.ColumnName = "Type"
        '
        'DataColumn7
        '
        Me.DataColumn7.ColumnMapping = System.Data.MappingType.Hidden
        Me.DataColumn7.ColumnName = "DateRetour"
        '
        'DataColumn8
        '
        Me.DataColumn8.ColumnMapping = System.Data.MappingType.Hidden
        Me.DataColumn8.ColumnName = "Depot"
        '
        'DataColumn9
        '
        Me.DataColumn9.ColumnMapping = System.Data.MappingType.Hidden
        Me.DataColumn9.ColumnName = "CoutPret"
        '
        'DataColumn10
        '
        Me.DataColumn10.ColumnMapping = System.Data.MappingType.Hidden
        Me.DataColumn10.ColumnName = "VerifiedByTRP"
        Me.DataColumn10.DataType = GetType(Boolean)
        '
        'DataColumn11
        '
        Me.DataColumn11.ColumnMapping = System.Data.MappingType.Hidden
        Me.DataColumn11.ColumnName = "Rembourse"
        Me.DataColumn11.DataType = GetType(Boolean)
        '
        'DataColumn12
        '
        Me.DataColumn12.ColumnMapping = System.Data.MappingType.Hidden
        Me.DataColumn12.ColumnName = "Retourne"
        Me.DataColumn12.DataType = GetType(Boolean)
        '
        'DataColumn13
        '
        Me.DataColumn13.ColumnMapping = System.Data.MappingType.Hidden
        Me.DataColumn13.ColumnName = "Remarques"
        '
        'modifenable
        '
        Me.modifenable.BackColor = System.Drawing.SystemColors.Control
        Me.modifenable.Cursor = System.Windows.Forms.Cursors.Default
        Me.modifenable.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifenable.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifenable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modifenable.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifenable.Location = New System.Drawing.Point(8, 408)
        Me.modifenable.Name = "modifenable"
        Me.modifenable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modifenable.Size = New System.Drawing.Size(24, 24)
        Me.modifenable.TabIndex = 31
        Me.modifenable.TabStop = False
        Me.ToolTip1.SetToolTip(Me.modifenable, "Modifier les informations de base du client")
        Me.modifenable.UseVisualStyleBackColor = False
        '
        'photo
        '
        Me.photo.BackColor = System.Drawing.SystemColors.Control
        Me.photo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.photo.Cursor = System.Windows.Forms.Cursors.Default
        Me.photo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.photo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.photo.Location = New System.Drawing.Point(40, 408)
        Me.photo.Name = "photo"
        Me.photo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.photo.Size = New System.Drawing.Size(320, 216)
        Me.photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.photo.TabIndex = 58
        Me.photo.TabStop = False
        '
        'menuviewmodifclientsdossier
        '
        Me.menuviewmodifclientsdossier.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menustatutOuvert, Me.menustatutFerme, Me.MenuLine1, Me.menuShowDossierHisto, Me.menuExportFolder, Me.menuGenFolderRapport, Me.menudelete, Me.menuTransferFolder, Me.MenuItem2, Me.menuStatutDemande, Me.menuDossierFlagged})
        '
        'MenuLine1
        '
        Me.MenuLine1.Index = 2
        Me.MenuLine1.Text = "-"
        '
        'menuShowDossierHisto
        '
        Me.menuShowDossierHisto.Index = 3
        Me.menuShowDossierHisto.Text = "Afficher l'historique"
        '
        'menuGenFolderRapport
        '
        Me.menuGenFolderRapport.Index = 5
        Me.menuGenFolderRapport.Text = "Générer le rapport détaillé"
        '
        'menudelete
        '
        Me.menudelete.Index = 6
        Me.menudelete.Text = "Supprimer"
        '
        'menuTransferFolder
        '
        Me.menuTransferFolder.Index = 7
        Me.menuTransferFolder.Text = "Transférer vers un autre compte client"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 8
        Me.MenuItem2.Text = "-"
        '
        'menuStatutDemande
        '
        Me.menuStatutDemande.Index = 9
        Me.menuStatutDemande.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuDemandeNonTransmise, Me.menuDemandeEnvoyee, Me.menuDemandeAcceptee, Me.menuDemandeRefusee, Me.MenuItem1, Me.menuSuiviDemande})
        Me.menuStatutDemande.Text = "Demande d'autorisation"
        '
        'menuDemandeNonTransmise
        '
        Me.menuDemandeNonTransmise.Checked = True
        Me.menuDemandeNonTransmise.Index = 0
        Me.menuDemandeNonTransmise.Text = "Non transmise"
        '
        'menuDemandeEnvoyee
        '
        Me.menuDemandeEnvoyee.Index = 1
        Me.menuDemandeEnvoyee.Text = "Envoyée"
        '
        'menuDemandeAcceptee
        '
        Me.menuDemandeAcceptee.Index = 2
        Me.menuDemandeAcceptee.Text = "Acceptée"
        '
        'menuDemandeRefusee
        '
        Me.menuDemandeRefusee.Index = 3
        Me.menuDemandeRefusee.Text = "Refusée"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 4
        Me.MenuItem1.Text = "-"
        '
        'menuSuiviDemande
        '
        Me.menuSuiviDemande.Index = 5
        Me.menuSuiviDemande.Text = "Voir le suivi de la demande"
        '
        'menuDossierFlagged
        '
        Me.menuDossierFlagged.Index = 10
        Me.menuDossierFlagged.Text = "Flaggé"
        Me.menuDossierFlagged.Visible = False
        '
        'paiements
        '
        Me.paiements.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.paiements.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.paiements.Location = New System.Drawing.Point(8, 516)
        Me.paiements.Name = "paiements"
        Me.paiements.Size = New System.Drawing.Size(24, 24)
        Me.paiements.TabIndex = 108
        Me.paiements.TabStop = False
        Me.ToolTip1.SetToolTip(Me.paiements, "Effectuer le(s) paiement(s)")
        '
        'menuviewmodifclientscommunications
        '
        Me.menuviewmodifclientscommunications.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuImportFromOutside, Me.menuImportFromDB})
        '
        'menuImportFromOutside
        '
        Me.menuImportFromOutside.Index = 0
        Me.menuImportFromOutside.Text = "De l'extérieur du logiciel"
        '
        'menuImportFromDB
        '
        Me.menuImportFromDB.Index = 1
        Me.menuImportFromDB.Text = "De la banque de données"
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'btnAddAsKP
        '
        Me.btnAddAsKP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddAsKP.Location = New System.Drawing.Point(8, 492)
        Me.btnAddAsKP.Name = "btnAddAsKP"
        Me.btnAddAsKP.Size = New System.Drawing.Size(24, 24)
        Me.btnAddAsKP.TabIndex = 144
        Me.btnAddAsKP.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnAddAsKP, "Ajouter comme personne clé")
        '
        'AddTel
        '
        Me.AddTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AddTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.AddTel.Location = New System.Drawing.Point(16, 240)
        Me.AddTel.Name = "AddTel"
        Me.AddTel.Size = New System.Drawing.Size(24, 24)
        Me.AddTel.TabIndex = 8
        Me.AddTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.AddTel, "Ajout d'un numéro de téléphone")
        '
        'ModifTel
        '
        Me.ModifTel.Enabled = False
        Me.ModifTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ModifTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ModifTel.Location = New System.Drawing.Point(48, 240)
        Me.ModifTel.Name = "ModifTel"
        Me.ModifTel.Size = New System.Drawing.Size(24, 24)
        Me.ModifTel.TabIndex = 155
        Me.ModifTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ModifTel, "Modifier un numéro de téléphone")
        '
        'DelTel
        '
        Me.DelTel.Enabled = False
        Me.DelTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DelTel.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.DelTel.Location = New System.Drawing.Point(80, 240)
        Me.DelTel.Name = "DelTel"
        Me.DelTel.Size = New System.Drawing.Size(24, 24)
        Me.DelTel.TabIndex = 156
        Me.DelTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DelTel, "Enlever un numéro de téléphone")
        '
        'selectionner
        '
        Me.selectionner.BackColor = System.Drawing.SystemColors.Control
        Me.selectionner.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectionner.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectionner.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectionner.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectionner.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.selectionner.Location = New System.Drawing.Point(8, 288)
        Me.selectionner.Name = "selectionner"
        Me.selectionner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectionner.Size = New System.Drawing.Size(24, 24)
        Me.selectionner.TabIndex = 8
        Me.selectionner.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.selectionner, "Sélectionner le référent")
        Me.selectionner.UseVisualStyleBackColor = False
        '
        'DownTel
        '
        Me.DownTel.Enabled = False
        Me.DownTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DownTel.Location = New System.Drawing.Point(144, 240)
        Me.DownTel.Name = "DownTel"
        Me.DownTel.Size = New System.Drawing.Size(24, 24)
        Me.DownTel.TabIndex = 189
        Me.DownTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DownTel, "Descendre un numéro de téléphone")
        '
        'UpTel
        '
        Me.UpTel.Enabled = False
        Me.UpTel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.UpTel.Location = New System.Drawing.Point(112, 240)
        Me.UpTel.Name = "UpTel"
        Me.UpTel.Size = New System.Drawing.Size(24, 24)
        Me.UpTel.TabIndex = 188
        Me.UpTel.TabStop = False
        Me.ToolTip1.SetToolTip(Me.UpTel, "Monter un numéro de téléphone")
        '
        'StartStopCommChanging
        '
        Me.StartStopCommChanging.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.StartStopCommChanging.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.StartStopCommChanging.Location = New System.Drawing.Point(899, 0)
        Me.StartStopCommChanging.Name = "StartStopCommChanging"
        Me.StartStopCommChanging.Size = New System.Drawing.Size(24, 24)
        Me.StartStopCommChanging.TabIndex = 192
        Me.StartStopCommChanging.TabStop = False
        Me.ToolTip1.SetToolTip(Me.StartStopCommChanging, "Commencer la modification les communications")
        '
        'annee
        '
        Me.annee.acceptAlpha = False
        Me.annee.acceptedChars = ",§."
        Me.annee.acceptNumeric = True
        Me.annee.allCapital = False
        Me.annee.allLower = False
        Me.annee.autoComplete = False
        Me.annee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.annee.autoSizeDropDown = True
        Me.annee.BackColor = System.Drawing.Color.White
        Me.annee.blockOnMaximum = False
        Me.annee.blockOnMinimum = False
        Me.annee.cb_AcceptLeftZeros = False
        Me.annee.cb_AcceptNegative = False
        Me.annee.currencyBox = True
        Me.annee.Cursor = System.Windows.Forms.Cursors.Default
        Me.annee.dbField = Nothing
        Me.annee.doComboDelete = True
        Me.annee.firstLetterCapital = False
        Me.annee.firstLettersCapital = False
        Me.annee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.annee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.annee.IntegralHeight = False
        Me.annee.itemsToolTipDuration = 10000
        Me.annee.Location = New System.Drawing.Point(192, 16)
        Me.annee.manageText = True
        Me.annee.matchExp = Nothing
        Me.annee.maximum = 0
        Me.annee.minimum = 0
        Me.annee.Name = "annee"
        Me.annee.nbDecimals = CType(-1, Short)
        Me.annee.onlyAlphabet = False
        Me.annee.pathOfList = ""
        Me.annee.ReadOnly = False
        Me.annee.refuseAccents = False
        Me.annee.refusedChars = ""
        Me.annee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.annee.showItemsToolTip = False
        Me.annee.Size = New System.Drawing.Size(56, 22)
        Me.annee.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.annee, "(AAAA/MM/JJ)")
        Me.annee.trimText = False
        '
        'mois
        '
        Me.mois.acceptAlpha = False
        Me.mois.acceptedChars = ",§."
        Me.mois.acceptNumeric = True
        Me.mois.allCapital = False
        Me.mois.allLower = False
        Me.mois.autoComplete = False
        Me.mois.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.mois.autoSizeDropDown = True
        Me.mois.BackColor = System.Drawing.Color.White
        Me.mois.blockOnMaximum = True
        Me.mois.blockOnMinimum = True
        Me.mois.cb_AcceptLeftZeros = True
        Me.mois.cb_AcceptNegative = False
        Me.mois.currencyBox = True
        Me.mois.Cursor = System.Windows.Forms.Cursors.Default
        Me.mois.dbField = Nothing
        Me.mois.doComboDelete = True
        Me.mois.firstLetterCapital = False
        Me.mois.firstLettersCapital = False
        Me.mois.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mois.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mois.IntegralHeight = False
        Me.mois.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.mois.itemsToolTipDuration = 10000
        Me.mois.Location = New System.Drawing.Point(248, 16)
        Me.mois.manageText = True
        Me.mois.matchExp = Nothing
        Me.mois.maximum = 12
        Me.mois.MaxLength = 2
        Me.mois.minimum = 0
        Me.mois.Name = "mois"
        Me.mois.nbDecimals = CType(-1, Short)
        Me.mois.onlyAlphabet = False
        Me.mois.pathOfList = ""
        Me.mois.ReadOnly = False
        Me.mois.refuseAccents = False
        Me.mois.refusedChars = ""
        Me.mois.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mois.showItemsToolTip = False
        Me.mois.Size = New System.Drawing.Size(48, 22)
        Me.mois.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.mois, "(AAAA/MM/JJ)")
        Me.mois.trimText = False
        '
        'jour
        '
        Me.jour.acceptAlpha = False
        Me.jour.acceptedChars = ",§."
        Me.jour.acceptNumeric = True
        Me.jour.allCapital = False
        Me.jour.allLower = False
        Me.jour.autoComplete = False
        Me.jour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.jour.autoSizeDropDown = True
        Me.jour.BackColor = System.Drawing.Color.White
        Me.jour.blockOnMaximum = True
        Me.jour.blockOnMinimum = True
        Me.jour.cb_AcceptLeftZeros = True
        Me.jour.cb_AcceptNegative = False
        Me.jour.currencyBox = True
        Me.jour.Cursor = System.Windows.Forms.Cursors.Default
        Me.jour.dbField = Nothing
        Me.jour.doComboDelete = True
        Me.jour.firstLetterCapital = False
        Me.jour.firstLettersCapital = False
        Me.jour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.jour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.jour.IntegralHeight = False
        Me.jour.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.jour.itemsToolTipDuration = 10000
        Me.jour.Location = New System.Drawing.Point(296, 16)
        Me.jour.manageText = True
        Me.jour.matchExp = Nothing
        Me.jour.maximum = 31
        Me.jour.MaxLength = 2
        Me.jour.minimum = 0
        Me.jour.Name = "jour"
        Me.jour.nbDecimals = CType(-1, Short)
        Me.jour.onlyAlphabet = False
        Me.jour.pathOfList = ""
        Me.jour.ReadOnly = False
        Me.jour.refuseAccents = False
        Me.jour.refusedChars = ""
        Me.jour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.jour.showItemsToolTip = False
        Me.jour.Size = New System.Drawing.Size(48, 22)
        Me.jour.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.jour, "(AAAA/MM/JJ)")
        Me.jour.trimText = False
        '
        'btnAddAlert
        '
        Me.btnAddAlert.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddAlert.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnAddAlert.Location = New System.Drawing.Point(8, 564)
        Me.btnAddAlert.Name = "btnAddAlert"
        Me.btnAddAlert.Size = New System.Drawing.Size(24, 24)
        Me.btnAddAlert.TabIndex = 108
        Me.btnAddAlert.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnAddAlert, "Ajouter une alarme sur...")
        '
        'createBill
        '
        Me.createBill.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.createBill.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.createBill.Location = New System.Drawing.Point(8, 540)
        Me.createBill.Name = "createBill"
        Me.createBill.Size = New System.Drawing.Size(24, 24)
        Me.createBill.TabIndex = 108
        Me.createBill.TabStop = False
        Me.ToolTip1.SetToolTip(Me.createBill, "Ajouter une nouvelle facture à un dossier")
        '
        'btnSendEmail
        '
        Me.btnSendEmail.Enabled = False
        Me.btnSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSendEmail.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnSendEmail.Location = New System.Drawing.Point(8, 588)
        Me.btnSendEmail.Name = "btnSendEmail"
        Me.btnSendEmail.Size = New System.Drawing.Size(24, 24)
        Me.btnSendEmail.TabIndex = 108
        Me.btnSendEmail.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnSendEmail, "Envoyer un message externe au client")
        '
        'RefMenu
        '
        Me.RefMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuRefAucun, Me.menuRefAutre, Me.menuRefCompte, Me.menuPreRefList, Me.menuRefKP})
        '
        'menuRefAucun
        '
        Me.menuRefAucun.Checked = True
        Me.menuRefAucun.Index = 0
        Me.menuRefAucun.Text = "Aucun"
        '
        'menuRefAutre
        '
        Me.menuRefAutre.Index = 1
        Me.menuRefAutre.Text = "Autre"
        '
        'menuRefCompte
        '
        Me.menuRefCompte.Index = 2
        Me.menuRefCompte.Text = "Compte client"
        '
        'menuPreRefList
        '
        Me.menuPreRefList.Index = 3
        Me.menuPreRefList.Text = "Liste prédéterminée"
        '
        'menuRefKP
        '
        Me.menuRefKP.Index = 4
        Me.menuRefKP.Text = "Personne ou organisme clé"
        '
        'AdminPanel
        '
        Me.AdminPanel.BackColor = System.Drawing.SystemColors.Control
        Me.AdminPanel.Controls.Add(Me.AdminTimer)
        Me.AdminPanel.Controls.Add(Me.AdminButton2)
        Me.AdminPanel.Controls.Add(Me.AdminButton1)
        Me.AdminPanel.Controls.Add(Me.AdminInterval)
        Me.AdminPanel.Controls.Add(Me.Label33)
        Me.AdminPanel.Controls.Add(Me.AdminCloseBtn)
        Me.AdminPanel.Location = New System.Drawing.Point(192, 6)
        Me.AdminPanel.Name = "AdminPanel"
        Me.AdminPanel.Size = New System.Drawing.Size(168, 88)
        Me.AdminPanel.TabIndex = 145
        Me.AdminPanel.Visible = False
        '
        'AdminTimer
        '
        Me.AdminTimer.AutoSize = True
        Me.AdminTimer.Location = New System.Drawing.Point(8, 48)
        Me.AdminTimer.Name = "AdminTimer"
        Me.AdminTimer.Size = New System.Drawing.Size(13, 14)
        Me.AdminTimer.TabIndex = 5
        Me.AdminTimer.Text = "0"
        '
        'AdminButton2
        '
        Me.AdminButton2.Location = New System.Drawing.Point(80, 32)
        Me.AdminButton2.Name = "AdminButton2"
        Me.AdminButton2.Size = New System.Drawing.Size(80, 16)
        Me.AdminButton2.TabIndex = 4
        Me.AdminButton2.Text = "Selected tab"
        '
        'AdminButton1
        '
        Me.AdminButton1.Location = New System.Drawing.Point(8, 32)
        Me.AdminButton1.Name = "AdminButton1"
        Me.AdminButton1.Size = New System.Drawing.Size(64, 16)
        Me.AdminButton1.TabIndex = 3
        Me.AdminButton1.Text = "Select tab"
        '
        'AdminInterval
        '
        Me.AdminInterval.Location = New System.Drawing.Point(96, 8)
        Me.AdminInterval.Name = "AdminInterval"
        Me.AdminInterval.Size = New System.Drawing.Size(40, 20)
        Me.AdminInterval.TabIndex = 2
        Me.AdminInterval.Text = "1"
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(8, 8)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(88, 16)
        Me.Label33.TabIndex = 1
        Me.Label33.Text = "Interval Legende"
        '
        'AdminCloseBtn
        '
        Me.AdminCloseBtn.Location = New System.Drawing.Point(144, 8)
        Me.AdminCloseBtn.Name = "AdminCloseBtn"
        Me.AdminCloseBtn.Size = New System.Drawing.Size(16, 16)
        Me.AdminCloseBtn.TabIndex = 0
        Me.AdminCloseBtn.Text = "X"
        '
        'menuclickRV
        '
        Me.menuclickRV.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuacopier, Me.menuaenlever, Me.menuSeparator, Me.menuAnnonceClient, Me.menuConfirmRV, Me.menuPrintRecu, Me.menuUpQL, Me.menuChangeRVPeriod, Me.menuRVService, Me.menumodifstatus, Me.MenuItem3, Me.menuTransferRV, Me.menuRVtypes, Me.menuVisiteFlagged})
        '
        'menuacopier
        '
        Me.menuacopier.Index = 0
        Me.menuacopier.Text = "Copier"
        '
        'menuaenlever
        '
        Me.menuaenlever.Index = 1
        Me.menuaenlever.Text = "Enlever"
        '
        'menuSeparator
        '
        Me.menuSeparator.Index = 2
        Me.menuSeparator.Text = "-"
        '
        'menuAnnonceClient
        '
        Me.menuAnnonceClient.Index = 3
        Me.menuAnnonceClient.Text = "Annoncer l'arrivée du client"
        '
        'menuConfirmRV
        '
        Me.menuConfirmRV.Index = 4
        Me.menuConfirmRV.Text = "Confirmer le rendez-vous"
        '
        'menuPrintRecu
        '
        Me.menuPrintRecu.Index = 5
        Me.menuPrintRecu.Text = "Imprimer un reçu"
        '
        'menuUpQL
        '
        Me.menuUpQL.Index = 6
        Me.menuUpQL.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAddToQueueList, Me.menuQueueList})
        Me.menuUpQL.Text = "Liste d'attente"
        '
        'menuAddToQueueList
        '
        Me.menuAddToQueueList.Index = 0
        Me.menuAddToQueueList.Text = "Ajouter à la liste d'attente"
        '
        'menuQueueList
        '
        Me.menuQueueList.Index = 1
        Me.menuQueueList.Text = "Voir la liste d'attente"
        '
        'menuChangeRVPeriod
        '
        Me.menuChangeRVPeriod.Index = 7
        Me.menuChangeRVPeriod.Text = "Modifier la période"
        '
        'menuRVService
        '
        Me.menuRVService.Index = 8
        Me.menuRVService.Text = "Modifier le service"
        '
        'menumodifstatus
        '
        Me.menumodifstatus.Index = 9
        Me.menumodifstatus.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menupresent, Me.menuabsentmotive, Me.menuabsentnonmotive, Me.menueffstatus})
        Me.menumodifstatus.Text = "Modifier le statut..."
        '
        'menupresent
        '
        Me.menupresent.Index = 0
        Me.menupresent.Text = "Présent"
        '
        'menuabsentmotive
        '
        Me.menuabsentmotive.Index = 1
        Me.menuabsentmotive.Text = "Absent - Motivé"
        '
        'menuabsentnonmotive
        '
        Me.menuabsentnonmotive.Index = 2
        Me.menuabsentnonmotive.Text = "Absent - Non-motivé"
        '
        'menueffstatus
        '
        Me.menueffstatus.Index = 3
        Me.menueffstatus.Text = "Effacer"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 10
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuRVRemarques, Me.menuRVRemarkDel})
        Me.MenuItem3.Text = "Remarque"
        '
        'menuRVRemarques
        '
        Me.menuRVRemarques.Index = 0
        Me.menuRVRemarques.Text = "Modifier la remarque"
        '
        'menuRVRemarkDel
        '
        Me.menuRVRemarkDel.Index = 1
        Me.menuRVRemarkDel.Text = "Supprimer la remarque"
        '
        'menuTransferRV
        '
        Me.menuTransferRV.Index = 11
        Me.menuTransferRV.Text = "Transférer vers un autre dossier"
        '
        'menuRVtypes
        '
        Me.menuRVtypes.Index = 12
        Me.menuRVtypes.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuRVEval, Me.menuRVTraitement})
        Me.menuRVtypes.Text = "Type de rendez-vous"
        '
        'menuRVEval
        '
        Me.menuRVEval.Index = 0
        Me.menuRVEval.Text = "Évaluation"
        '
        'menuRVTraitement
        '
        Me.menuRVTraitement.Index = 1
        Me.menuRVTraitement.Text = "Traitement"
        '
        'menuVisiteFlagged
        '
        Me.menuVisiteFlagged.Index = 13
        Me.menuVisiteFlagged.Text = "Flaggé"
        Me.menuVisiteFlagged.Visible = False
        '
        '_Label1_16
        '
        Me._Label1_16.AutoSize = True
        Me._Label1_16.BackColor = System.Drawing.Color.Transparent
        Me._Label1_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_16.Location = New System.Drawing.Point(8, 320)
        Me._Label1_16.Name = "_Label1_16"
        Me._Label1_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_16.Size = New System.Drawing.Size(77, 14)
        Me._Label1_16.TabIndex = 182
        Me._Label1_16.Text = "Remarques :"
        '
        '_Label1_4
        '
        Me._Label1_4.AutoSize = True
        Me._Label1_4.BackColor = System.Drawing.Color.Transparent
        Me._Label1_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_4.Location = New System.Drawing.Point(8, 200)
        Me._Label1_4.Name = "_Label1_4"
        Me._Label1_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_4.Size = New System.Drawing.Size(79, 14)
        Me._Label1_4.TabIndex = 187
        Me._Label1_4.Text = "Téléphones :"
        '
        'reference
        '
        Me.reference.BackColor = System.Drawing.SystemColors.Control
        Me.reference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.reference.Location = New System.Drawing.Point(40, 288)
        Me.reference.Multiline = True
        Me.reference.Name = "reference"
        Me.reference.ReadOnly = True
        Me.reference.Size = New System.Drawing.Size(144, 32)
        Me.reference.TabIndex = 186
        Me.reference.TabStop = False
        Me.reference.Tag = ""
        Me.reference.Text = "Nom du référent"
        Me.reference.WordWrap = False
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(192, 240)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(149, 14)
        Me.label4.TabIndex = 185
        Me.label4.Text = "Adresse du site internet :"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(192, 200)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(58, 14)
        Me.label3.TabIndex = 184
        Me.label3.Text = "Courriel :"
        '
        '_Label1_17
        '
        Me._Label1_17.AutoSize = True
        Me._Label1_17.BackColor = System.Drawing.Color.Transparent
        Me._Label1_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_17.Location = New System.Drawing.Point(96, 0)
        Me._Label1_17.Name = "_Label1_17"
        Me._Label1_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_17.Size = New System.Drawing.Size(57, 14)
        Me._Label1_17.TabIndex = 183
        Me._Label1_17.Text = "Prénom :"
        '
        '_Label1_15
        '
        Me._Label1_15.AutoSize = True
        Me._Label1_15.BackColor = System.Drawing.Color.Transparent
        Me._Label1_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_15.Location = New System.Drawing.Point(192, 160)
        Me._Label1_15.Name = "_Label1_15"
        Me._Label1_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_15.Size = New System.Drawing.Size(174, 14)
        Me._Label1_15.TabIndex = 181
        Me._Label1_15.Text = "Numéro d'assurance maladie :"
        '
        '_Label1_14
        '
        Me._Label1_14.AutoSize = True
        Me._Label1_14.BackColor = System.Drawing.Color.Transparent
        Me._Label1_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_14.Location = New System.Drawing.Point(8, 272)
        Me._Label1_14.Name = "_Label1_14"
        Me._Label1_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_14.Size = New System.Drawing.Size(61, 14)
        Me._Label1_14.TabIndex = 180
        Me._Label1_14.Text = "Référent :"
        '
        '_Label1_11
        '
        Me._Label1_11.AutoSize = True
        Me._Label1_11.BackColor = System.Drawing.Color.Transparent
        Me._Label1_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_11.Location = New System.Drawing.Point(192, 120)
        Me._Label1_11.Name = "_Label1_11"
        Me._Label1_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_11.Size = New System.Drawing.Size(49, 14)
        Me._Label1_11.TabIndex = 179
        Me._Label1_11.Text = "Métier :"
        '
        '_Label1_8
        '
        Me._Label1_8.AutoSize = True
        Me._Label1_8.BackColor = System.Drawing.Color.Transparent
        Me._Label1_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_8.Location = New System.Drawing.Point(192, 80)
        Me._Label1_8.Name = "_Label1_8"
        Me._Label1_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_8.Size = New System.Drawing.Size(72, 14)
        Me._Label1_8.TabIndex = 178
        Me._Label1_8.Text = "Employeur :"
        '
        '_Label1_7
        '
        Me._Label1_7.AutoSize = True
        Me._Label1_7.BackColor = System.Drawing.Color.Transparent
        Me._Label1_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_7.Location = New System.Drawing.Point(192, 40)
        Me._Label1_7.Name = "_Label1_7"
        Me._Label1_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_7.Size = New System.Drawing.Size(40, 14)
        Me._Label1_7.TabIndex = 177
        Me._Label1_7.Text = "Sexe :"
        '
        '_Label1_6
        '
        Me._Label1_6.AutoSize = True
        Me._Label1_6.BackColor = System.Drawing.Color.Transparent
        Me._Label1_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_6.Location = New System.Drawing.Point(192, 0)
        Me._Label1_6.Name = "_Label1_6"
        Me._Label1_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_6.Size = New System.Drawing.Size(113, 14)
        Me._Label1_6.TabIndex = 176
        Me._Label1_6.Text = "Date de naissance :"
        '
        '_Label1_5
        '
        Me._Label1_5.AutoSize = True
        Me._Label1_5.BackColor = System.Drawing.Color.Transparent
        Me._Label1_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_5.Location = New System.Drawing.Point(8, 160)
        Me._Label1_5.Name = "_Label1_5"
        Me._Label1_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_5.Size = New System.Drawing.Size(72, 14)
        Me._Label1_5.TabIndex = 175
        Me._Label1_5.Text = "Autre nom :"
        '
        '_Label1_0
        '
        Me._Label1_0.AutoSize = True
        Me._Label1_0.BackColor = System.Drawing.Color.Transparent
        Me._Label1_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_0.Location = New System.Drawing.Point(8, 0)
        Me._Label1_0.Name = "_Label1_0"
        Me._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_0.Size = New System.Drawing.Size(38, 14)
        Me._Label1_0.TabIndex = 174
        Me._Label1_0.Text = "Nom :"
        '
        '_Label1_1
        '
        Me._Label1_1.AutoSize = True
        Me._Label1_1.BackColor = System.Drawing.Color.Transparent
        Me._Label1_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_1.Location = New System.Drawing.Point(8, 40)
        Me._Label1_1.Name = "_Label1_1"
        Me._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_1.Size = New System.Drawing.Size(61, 14)
        Me._Label1_1.TabIndex = 173
        Me._Label1_1.Text = "Adresse :"
        '
        '_Label1_2
        '
        Me._Label1_2.AutoSize = True
        Me._Label1_2.BackColor = System.Drawing.Color.Transparent
        Me._Label1_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_2.Location = New System.Drawing.Point(8, 80)
        Me._Label1_2.Name = "_Label1_2"
        Me._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_2.Size = New System.Drawing.Size(37, 14)
        Me._Label1_2.TabIndex = 172
        Me._Label1_2.Text = "Ville :"
        '
        '_Label1_3
        '
        Me._Label1_3.AutoSize = True
        Me._Label1_3.BackColor = System.Drawing.Color.Transparent
        Me._Label1_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_3.Location = New System.Drawing.Point(8, 120)
        Me._Label1_3.Name = "_Label1_3"
        Me._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_3.Size = New System.Drawing.Size(79, 14)
        Me._Label1_3.TabIndex = 171
        Me._Label1_3.Text = "Code postal :"
        '
        '_Label2_2
        '
        Me._Label2_2.AutoSize = True
        Me._Label2_2.BackColor = System.Drawing.Color.Transparent
        Me._Label2_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_2.Location = New System.Drawing.Point(40, 136)
        Me._Label2_2.Name = "_Label2_2"
        Me._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_2.Size = New System.Drawing.Size(11, 14)
        Me._Label2_2.TabIndex = 170
        Me._Label2_2.Text = "-"
        '
        '_sexe_1
        '
        Me._sexe_1.BackColor = System.Drawing.SystemColors.Control
        Me._sexe_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._sexe_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._sexe_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._sexe_1.Location = New System.Drawing.Point(256, 56)
        Me._sexe_1.Name = "_sexe_1"
        Me._sexe_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._sexe_1.Size = New System.Drawing.Size(64, 17)
        Me._sexe_1.TabIndex = 13
        Me._sexe_1.TabStop = True
        Me._sexe_1.Text = "Femme"
        Me._sexe_1.UseVisualStyleBackColor = False
        '
        '_sexe_0
        '
        Me._sexe_0.BackColor = System.Drawing.SystemColors.Control
        Me._sexe_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._sexe_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._sexe_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._sexe_0.Location = New System.Drawing.Point(192, 56)
        Me._sexe_0.Name = "_sexe_0"
        Me._sexe_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._sexe_0.Size = New System.Drawing.Size(64, 17)
        Me._sexe_0.TabIndex = 12
        Me._sexe_0.TabStop = True
        Me._sexe_0.Text = "Homme"
        Me._sexe_0.UseVisualStyleBackColor = False
        '
        'dsVentePret
        '
        Me.dsVentePret.DataSetName = "dsVentePret"
        Me.dsVentePret.Locale = New System.Globalization.CultureInfo("fr-CA")
        Me.dsVentePret.Tables.AddRange(New System.Data.DataTable() {Me.DataTable1})
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label21.Location = New System.Drawing.Point(192, 280)
        Me.label21.Name = "label21"
        Me.label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label21.Size = New System.Drawing.Size(85, 14)
        Me.label21.TabIndex = 190
        Me.label21.Text = "Publipostage :"
        '
        'publipostage
        '
        Me.publipostage.acceptAlpha = True
        Me.publipostage.acceptedChars = Nothing
        Me.publipostage.acceptNumeric = True
        Me.publipostage.allCapital = False
        Me.publipostage.allLower = False
        Me.publipostage.autoComplete = True
        Me.publipostage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.publipostage.autoSizeDropDown = True
        Me.publipostage.BackColor = System.Drawing.Color.White
        Me.publipostage.blockOnMaximum = False
        Me.publipostage.blockOnMinimum = False
        Me.publipostage.cb_AcceptLeftZeros = False
        Me.publipostage.cb_AcceptNegative = False
        Me.publipostage.currencyBox = False
        Me.publipostage.dbField = Nothing
        Me.publipostage.doComboDelete = True
        Me.publipostage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.publipostage.firstLetterCapital = False
        Me.publipostage.firstLettersCapital = False
        Me.publipostage.IntegralHeight = False
        Me.publipostage.Items.AddRange(New Object() {"Ne pas recevoir d'envoi", "Recevoir l'envoi par la poste", "Recevoir l'envoi par courriel"})
        Me.publipostage.itemsToolTipDuration = 10000
        Me.publipostage.Location = New System.Drawing.Point(192, 296)
        Me.publipostage.manageText = True
        Me.publipostage.matchExp = Nothing
        Me.publipostage.maximum = 0
        Me.publipostage.minimum = 0
        Me.publipostage.Name = "publipostage"
        Me.publipostage.nbDecimals = CType(-1, Short)
        Me.publipostage.onlyAlphabet = False
        Me.publipostage.pathOfList = ""
        Me.publipostage.ReadOnly = False
        Me.publipostage.refuseAccents = False
        Me.publipostage.refusedChars = Nothing
        Me.publipostage.showItemsToolTip = False
        Me.publipostage.Size = New System.Drawing.Size(168, 22)
        Me.publipostage.TabIndex = 19
        Me.publipostage.trimText = False
        '
        'Telephones
        '
        Me.Telephones.acceptAlpha = True
        Me.Telephones.acceptedChars = Nothing
        Me.Telephones.acceptNumeric = True
        Me.Telephones.allCapital = False
        Me.Telephones.allLower = False
        Me.Telephones.autoComplete = True
        Me.Telephones.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Telephones.autoSizeDropDown = True
        Me.Telephones.BackColor = System.Drawing.Color.White
        Me.Telephones.blockOnMaximum = False
        Me.Telephones.blockOnMinimum = False
        Me.Telephones.cb_AcceptLeftZeros = False
        Me.Telephones.cb_AcceptNegative = False
        Me.Telephones.currencyBox = False
        Me.Telephones.dbField = Nothing
        Me.Telephones.doComboDelete = True
        Me.Telephones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Telephones.DropDownWidth = 250
        Me.Telephones.firstLetterCapital = False
        Me.Telephones.firstLettersCapital = False
        Me.Telephones.IntegralHeight = False
        Me.Telephones.itemsToolTipDuration = 10000
        Me.Telephones.Location = New System.Drawing.Point(8, 216)
        Me.Telephones.manageText = True
        Me.Telephones.matchExp = Nothing
        Me.Telephones.maximum = 0
        Me.Telephones.minimum = 0
        Me.Telephones.Name = "Telephones"
        Me.Telephones.nbDecimals = CType(-1, Short)
        Me.Telephones.onlyAlphabet = False
        Me.Telephones.pathOfList = ""
        Me.Telephones.ReadOnly = False
        Me.Telephones.refuseAccents = False
        Me.Telephones.refusedChars = ""
        Me.Telephones.showItemsToolTip = False
        Me.Telephones.Size = New System.Drawing.Size(168, 22)
        Me.Telephones.TabIndex = 7
        Me.Telephones.trimText = False
        '
        'ville
        '
        Me.ville.acceptAlpha = True
        Me.ville.acceptedChars = " §'§-§.§/§|§\§(§)"
        Me.ville.acceptNumeric = False
        Me.ville.allCapital = False
        Me.ville.allLower = False
        Me.ville.autoComplete = True
        Me.ville.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ville.autoSizeDropDown = True
        Me.ville.BackColor = System.Drawing.Color.White
        Me.ville.blockOnMaximum = False
        Me.ville.blockOnMinimum = False
        Me.ville.cb_AcceptLeftZeros = False
        Me.ville.cb_AcceptNegative = False
        Me.ville.currencyBox = False
        Me.ville.Cursor = System.Windows.Forms.Cursors.Default
        Me.ville.dbField = "Villes.NomVille"
        Me.ville.doComboDelete = True
        Me.ville.firstLetterCapital = True
        Me.ville.firstLettersCapital = True
        Me.ville.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ville.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ville.IntegralHeight = False
        Me.ville.itemsToolTipDuration = 10000
        Me.ville.Location = New System.Drawing.Point(8, 96)
        Me.ville.manageText = True
        Me.ville.matchExp = ""
        Me.ville.maximum = 0
        Me.ville.minimum = 0
        Me.ville.Name = "ville"
        Me.ville.nbDecimals = CType(-1, Short)
        Me.ville.onlyAlphabet = True
        Me.ville.pathOfList = ""
        Me.ville.ReadOnly = False
        Me.ville.refuseAccents = False
        Me.ville.refusedChars = ""
        Me.ville.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ville.showItemsToolTip = False
        Me.ville.Size = New System.Drawing.Size(169, 22)
        Me.ville.Sorted = True
        Me.ville.TabIndex = 3
        Me.ville.trimText = False
        '
        'url
        '
        Me.url.acceptAlpha = True
        Me.url.acceptedChars = ""
        Me.url.acceptNumeric = True
        Me.url.AcceptsReturn = True
        Me.url.allCapital = False
        Me.url.allLower = False
        Me.url.BackColor = System.Drawing.SystemColors.Window
        Me.url.blockOnMaximum = False
        Me.url.blockOnMinimum = False
        Me.url.cb_AcceptLeftZeros = False
        Me.url.cb_AcceptNegative = False
        Me.url.currencyBox = False
        Me.url.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.url.firstLetterCapital = False
        Me.url.firstLettersCapital = False
        Me.url.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.url.ForeColor = System.Drawing.SystemColors.WindowText
        Me.url.Location = New System.Drawing.Point(192, 256)
        Me.url.manageText = True
        Me.url.matchExp = ""
        Me.url.maximum = 0
        Me.url.MaxLength = 0
        Me.url.minimum = 0
        Me.url.Name = "url"
        Me.url.nbDecimals = CType(-1, Short)
        Me.url.onlyAlphabet = False
        Me.url.refuseAccents = False
        Me.url.refusedChars = ""
        Me.url.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.url.showInternalContextMenu = True
        Me.url.Size = New System.Drawing.Size(169, 20)
        Me.url.TabIndex = 18
        Me.url.trimText = False
        '
        'courriel
        '
        Me.courriel.acceptAlpha = True
        Me.courriel.acceptedChars = "!§#§|§$§%§?§&§*§-§@§.§_§=§+§£§¢§¤§¬§¦§²§³§¼§½§¾§^§¨§¸§`§'§«§»§°§~§¯§´"
        Me.courriel.acceptNumeric = True
        Me.courriel.AcceptsReturn = True
        Me.courriel.allCapital = False
        Me.courriel.allLower = True
        Me.courriel.BackColor = System.Drawing.SystemColors.Window
        Me.courriel.blockOnMaximum = False
        Me.courriel.blockOnMinimum = False
        Me.courriel.cb_AcceptLeftZeros = False
        Me.courriel.cb_AcceptNegative = False
        Me.courriel.currencyBox = False
        Me.courriel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.courriel.firstLetterCapital = False
        Me.courriel.firstLettersCapital = False
        Me.courriel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.courriel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.courriel.Location = New System.Drawing.Point(192, 216)
        Me.courriel.manageText = True
        Me.courriel.matchExp = ""
        Me.courriel.maximum = 0
        Me.courriel.MaxLength = 0
        Me.courriel.minimum = 0
        Me.courriel.Name = "courriel"
        Me.courriel.nbDecimals = CType(-1, Short)
        Me.courriel.onlyAlphabet = True
        Me.courriel.refuseAccents = True
        Me.courriel.refusedChars = ""
        Me.courriel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.courriel.showInternalContextMenu = True
        Me.courriel.Size = New System.Drawing.Size(169, 20)
        Me.courriel.TabIndex = 17
        Me.courriel.trimText = False
        '
        'prenom
        '
        Me.prenom.acceptAlpha = True
        Me.prenom.acceptedChars = " §'§-"
        Me.prenom.acceptNumeric = False
        Me.prenom.AcceptsReturn = True
        Me.prenom.allCapital = False
        Me.prenom.allLower = False
        Me.prenom.BackColor = System.Drawing.SystemColors.Window
        Me.prenom.blockOnMaximum = False
        Me.prenom.blockOnMinimum = False
        Me.prenom.cb_AcceptLeftZeros = False
        Me.prenom.cb_AcceptNegative = False
        Me.prenom.currencyBox = False
        Me.prenom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.prenom.firstLetterCapital = True
        Me.prenom.firstLettersCapital = True
        Me.prenom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prenom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.prenom.Location = New System.Drawing.Point(96, 16)
        Me.prenom.manageText = True
        Me.prenom.matchExp = ""
        Me.prenom.maximum = 0
        Me.prenom.MaxLength = 0
        Me.prenom.minimum = 0
        Me.prenom.Name = "prenom"
        Me.prenom.nbDecimals = CType(-1, Short)
        Me.prenom.onlyAlphabet = True
        Me.prenom.refuseAccents = False
        Me.prenom.refusedChars = "(§)"
        Me.prenom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.prenom.showInternalContextMenu = True
        Me.prenom.Size = New System.Drawing.Size(81, 20)
        Me.prenom.TabIndex = 1
        Me.prenom.trimText = False
        '
        'remarques
        '
        Me.remarques.acceptAlpha = True
        Me.remarques.acceptedChars = ""
        Me.remarques.acceptNumeric = True
        Me.remarques.AcceptsReturn = True
        Me.remarques.allCapital = False
        Me.remarques.allLower = False
        Me.remarques.BackColor = System.Drawing.SystemColors.Window
        Me.remarques.blockOnMaximum = False
        Me.remarques.blockOnMinimum = False
        Me.remarques.cb_AcceptLeftZeros = False
        Me.remarques.cb_AcceptNegative = False
        Me.remarques.currencyBox = False
        Me.remarques.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.remarques.firstLetterCapital = True
        Me.remarques.firstLettersCapital = False
        Me.remarques.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.remarques.ForeColor = System.Drawing.SystemColors.WindowText
        Me.remarques.Location = New System.Drawing.Point(8, 336)
        Me.remarques.manageText = True
        Me.remarques.matchExp = ""
        Me.remarques.maximum = 0
        Me.remarques.MaxLength = 0
        Me.remarques.minimum = 0
        Me.remarques.Multiline = True
        Me.remarques.Name = "remarques"
        Me.remarques.nbDecimals = CType(-1, Short)
        Me.remarques.onlyAlphabet = False
        Me.remarques.refuseAccents = False
        Me.remarques.refusedChars = ""
        Me.remarques.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.remarques.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.remarques.showInternalContextMenu = True
        Me.remarques.Size = New System.Drawing.Size(352, 59)
        Me.remarques.TabIndex = 20
        Me.remarques.trimText = False
        '
        'nam
        '
        Me.nam.acceptAlpha = True
        Me.nam.acceptedChars = ""
        Me.nam.acceptNumeric = True
        Me.nam.AcceptsReturn = True
        Me.nam.allCapital = True
        Me.nam.allLower = False
        Me.nam.BackColor = System.Drawing.SystemColors.Window
        Me.nam.blockOnMaximum = False
        Me.nam.blockOnMinimum = False
        Me.nam.cb_AcceptLeftZeros = False
        Me.nam.cb_AcceptNegative = False
        Me.nam.currencyBox = False
        Me.nam.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nam.firstLetterCapital = False
        Me.nam.firstLettersCapital = False
        Me.nam.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nam.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nam.Location = New System.Drawing.Point(192, 176)
        Me.nam.manageText = True
        Me.nam.matchExp = "AAAA111111X1"
        Me.nam.maximum = 0
        Me.nam.MaxLength = 12
        Me.nam.minimum = 0
        Me.nam.Name = "nam"
        Me.nam.nbDecimals = CType(-1, Short)
        Me.nam.onlyAlphabet = True
        Me.nam.refuseAccents = True
        Me.nam.refusedChars = ""
        Me.nam.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nam.showInternalContextMenu = True
        Me.nam.Size = New System.Drawing.Size(168, 20)
        Me.nam.TabIndex = 16
        Me.nam.trimText = False
        '
        'autrenom
        '
        Me.autrenom.acceptAlpha = True
        Me.autrenom.acceptedChars = " §'"
        Me.autrenom.acceptNumeric = False
        Me.autrenom.AcceptsReturn = True
        Me.autrenom.allCapital = False
        Me.autrenom.allLower = False
        Me.autrenom.BackColor = System.Drawing.SystemColors.Window
        Me.autrenom.blockOnMaximum = False
        Me.autrenom.blockOnMinimum = False
        Me.autrenom.cb_AcceptLeftZeros = False
        Me.autrenom.cb_AcceptNegative = False
        Me.autrenom.currencyBox = False
        Me.autrenom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.autrenom.firstLetterCapital = True
        Me.autrenom.firstLettersCapital = True
        Me.autrenom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autrenom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.autrenom.Location = New System.Drawing.Point(8, 176)
        Me.autrenom.manageText = True
        Me.autrenom.matchExp = ""
        Me.autrenom.maximum = 0
        Me.autrenom.MaxLength = 0
        Me.autrenom.minimum = 0
        Me.autrenom.Name = "autrenom"
        Me.autrenom.nbDecimals = CType(-1, Short)
        Me.autrenom.onlyAlphabet = True
        Me.autrenom.refuseAccents = False
        Me.autrenom.refusedChars = ""
        Me.autrenom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.autrenom.showInternalContextMenu = True
        Me.autrenom.Size = New System.Drawing.Size(169, 20)
        Me.autrenom.TabIndex = 6
        Me.autrenom.trimText = False
        '
        'nom
        '
        Me.nom.acceptAlpha = True
        Me.nom.acceptedChars = " §'§-"
        Me.nom.acceptNumeric = False
        Me.nom.AcceptsReturn = True
        Me.nom.allCapital = False
        Me.nom.allLower = False
        Me.nom.BackColor = System.Drawing.SystemColors.Window
        Me.nom.blockOnMaximum = False
        Me.nom.blockOnMinimum = False
        Me.nom.cb_AcceptLeftZeros = False
        Me.nom.cb_AcceptNegative = False
        Me.nom.currencyBox = False
        Me.nom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.nom.firstLetterCapital = True
        Me.nom.firstLettersCapital = True
        Me.nom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nom.Location = New System.Drawing.Point(8, 16)
        Me.nom.manageText = True
        Me.nom.matchExp = ""
        Me.nom.maximum = 0
        Me.nom.MaxLength = 0
        Me.nom.minimum = 0
        Me.nom.Name = "nom"
        Me.nom.nbDecimals = CType(-1, Short)
        Me.nom.onlyAlphabet = True
        Me.nom.refuseAccents = False
        Me.nom.refusedChars = "(§)"
        Me.nom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nom.showInternalContextMenu = True
        Me.nom.Size = New System.Drawing.Size(81, 20)
        Me.nom.TabIndex = 0
        Me.nom.trimText = False
        '
        'adresse
        '
        Me.adresse.acceptAlpha = True
        Me.adresse.acceptedChars = ""
        Me.adresse.acceptNumeric = True
        Me.adresse.AcceptsReturn = True
        Me.adresse.allCapital = False
        Me.adresse.allLower = False
        Me.adresse.BackColor = System.Drawing.SystemColors.Window
        Me.adresse.blockOnMaximum = False
        Me.adresse.blockOnMinimum = False
        Me.adresse.cb_AcceptLeftZeros = False
        Me.adresse.cb_AcceptNegative = False
        Me.adresse.currencyBox = False
        Me.adresse.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.adresse.firstLetterCapital = True
        Me.adresse.firstLettersCapital = True
        Me.adresse.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adresse.ForeColor = System.Drawing.SystemColors.WindowText
        Me.adresse.Location = New System.Drawing.Point(8, 56)
        Me.adresse.manageText = True
        Me.adresse.matchExp = ""
        Me.adresse.maximum = 0
        Me.adresse.MaxLength = 0
        Me.adresse.minimum = 0
        Me.adresse.Name = "adresse"
        Me.adresse.nbDecimals = CType(-1, Short)
        Me.adresse.onlyAlphabet = False
        Me.adresse.refuseAccents = False
        Me.adresse.refusedChars = ""
        Me.adresse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adresse.showInternalContextMenu = True
        Me.adresse.Size = New System.Drawing.Size(169, 20)
        Me.adresse.TabIndex = 2
        Me.adresse.trimText = False
        '
        'codepostal2
        '
        Me.codepostal2.acceptAlpha = True
        Me.codepostal2.acceptedChars = ""
        Me.codepostal2.acceptNumeric = True
        Me.codepostal2.AcceptsReturn = True
        Me.codepostal2.allCapital = True
        Me.codepostal2.allLower = False
        Me.codepostal2.BackColor = System.Drawing.SystemColors.Window
        Me.codepostal2.blockOnMaximum = False
        Me.codepostal2.blockOnMinimum = False
        Me.codepostal2.cb_AcceptLeftZeros = False
        Me.codepostal2.cb_AcceptNegative = False
        Me.codepostal2.currencyBox = False
        Me.codepostal2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.codepostal2.firstLetterCapital = False
        Me.codepostal2.firstLettersCapital = False
        Me.codepostal2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codepostal2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.codepostal2.Location = New System.Drawing.Point(48, 136)
        Me.codepostal2.manageText = True
        Me.codepostal2.matchExp = "1A1"
        Me.codepostal2.maximum = 0
        Me.codepostal2.MaxLength = 3
        Me.codepostal2.minimum = 0
        Me.codepostal2.Name = "codepostal2"
        Me.codepostal2.nbDecimals = CType(-1, Short)
        Me.codepostal2.onlyAlphabet = True
        Me.codepostal2.refuseAccents = True
        Me.codepostal2.refusedChars = ""
        Me.codepostal2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.codepostal2.showInternalContextMenu = True
        Me.codepostal2.Size = New System.Drawing.Size(33, 20)
        Me.codepostal2.TabIndex = 5
        Me.codepostal2.trimText = False
        '
        'codepostal1
        '
        Me.codepostal1.acceptAlpha = True
        Me.codepostal1.acceptedChars = ""
        Me.codepostal1.acceptNumeric = True
        Me.codepostal1.AcceptsReturn = True
        Me.codepostal1.allCapital = True
        Me.codepostal1.allLower = False
        Me.codepostal1.BackColor = System.Drawing.SystemColors.Window
        Me.codepostal1.blockOnMaximum = False
        Me.codepostal1.blockOnMinimum = False
        Me.codepostal1.cb_AcceptLeftZeros = False
        Me.codepostal1.cb_AcceptNegative = False
        Me.codepostal1.currencyBox = False
        Me.codepostal1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.codepostal1.firstLetterCapital = False
        Me.codepostal1.firstLettersCapital = False
        Me.codepostal1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.codepostal1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.codepostal1.Location = New System.Drawing.Point(8, 136)
        Me.codepostal1.manageText = True
        Me.codepostal1.matchExp = "A1A"
        Me.codepostal1.maximum = 0
        Me.codepostal1.MaxLength = 3
        Me.codepostal1.minimum = 0
        Me.codepostal1.Name = "codepostal1"
        Me.codepostal1.nbDecimals = CType(-1, Short)
        Me.codepostal1.onlyAlphabet = True
        Me.codepostal1.refuseAccents = True
        Me.codepostal1.refusedChars = ""
        Me.codepostal1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.codepostal1.showInternalContextMenu = True
        Me.codepostal1.Size = New System.Drawing.Size(33, 20)
        Me.codepostal1.TabIndex = 4
        Me.codepostal1.trimText = False
        '
        'metierslist
        '
        Me.metierslist.acceptAlpha = True
        Me.metierslist.acceptedChars = Nothing
        Me.metierslist.acceptNumeric = True
        Me.metierslist.allCapital = False
        Me.metierslist.allLower = False
        Me.metierslist.autoComplete = True
        Me.metierslist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.metierslist.autoSizeDropDown = True
        Me.metierslist.BackColor = System.Drawing.Color.White
        Me.metierslist.blockOnMaximum = False
        Me.metierslist.blockOnMinimum = False
        Me.metierslist.cb_AcceptLeftZeros = False
        Me.metierslist.cb_AcceptNegative = False
        Me.metierslist.currencyBox = False
        Me.metierslist.Cursor = System.Windows.Forms.Cursors.Default
        Me.metierslist.dbField = "Metiers.Metier"
        Me.metierslist.doComboDelete = True
        Me.metierslist.firstLetterCapital = True
        Me.metierslist.firstLettersCapital = False
        Me.metierslist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.metierslist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.metierslist.IntegralHeight = False
        Me.metierslist.itemsToolTipDuration = 10000
        Me.metierslist.Location = New System.Drawing.Point(192, 136)
        Me.metierslist.manageText = True
        Me.metierslist.matchExp = ""
        Me.metierslist.maximum = 0
        Me.metierslist.minimum = 0
        Me.metierslist.Name = "metierslist"
        Me.metierslist.nbDecimals = CType(-1, Short)
        Me.metierslist.onlyAlphabet = False
        Me.metierslist.pathOfList = ""
        Me.metierslist.ReadOnly = False
        Me.metierslist.refuseAccents = False
        Me.metierslist.refusedChars = ""
        Me.metierslist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.metierslist.showItemsToolTip = False
        Me.metierslist.Size = New System.Drawing.Size(169, 22)
        Me.metierslist.Sorted = True
        Me.metierslist.TabIndex = 15
        Me.metierslist.trimText = False
        '
        'employeurslist
        '
        Me.employeurslist.acceptAlpha = True
        Me.employeurslist.acceptedChars = Nothing
        Me.employeurslist.acceptNumeric = True
        Me.employeurslist.allCapital = False
        Me.employeurslist.allLower = False
        Me.employeurslist.autoComplete = True
        Me.employeurslist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.employeurslist.autoSizeDropDown = True
        Me.employeurslist.BackColor = System.Drawing.Color.White
        Me.employeurslist.blockOnMaximum = False
        Me.employeurslist.blockOnMinimum = False
        Me.employeurslist.cb_AcceptLeftZeros = False
        Me.employeurslist.cb_AcceptNegative = False
        Me.employeurslist.currencyBox = False
        Me.employeurslist.Cursor = System.Windows.Forms.Cursors.Default
        Me.employeurslist.dbField = "Employeurs.Employeur"
        Me.employeurslist.doComboDelete = True
        Me.employeurslist.firstLetterCapital = True
        Me.employeurslist.firstLettersCapital = False
        Me.employeurslist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.employeurslist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.employeurslist.IntegralHeight = False
        Me.employeurslist.itemsToolTipDuration = 10000
        Me.employeurslist.Location = New System.Drawing.Point(192, 96)
        Me.employeurslist.manageText = True
        Me.employeurslist.matchExp = ""
        Me.employeurslist.maximum = 0
        Me.employeurslist.minimum = 0
        Me.employeurslist.Name = "employeurslist"
        Me.employeurslist.nbDecimals = CType(-1, Short)
        Me.employeurslist.onlyAlphabet = False
        Me.employeurslist.pathOfList = ""
        Me.employeurslist.ReadOnly = False
        Me.employeurslist.refuseAccents = False
        Me.employeurslist.refusedChars = ""
        Me.employeurslist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.employeurslist.showItemsToolTip = False
        Me.employeurslist.Size = New System.Drawing.Size(169, 22)
        Me.employeurslist.Sorted = True
        Me.employeurslist.TabIndex = 14
        Me.employeurslist.trimText = False
        '
        'menuViewModifClient
        '
        Me.menuViewModifClient.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuComm})
        Me.menuViewModifClient.Location = New System.Drawing.Point(0, 0)
        Me.menuViewModifClient.Name = "menuViewModifClient"
        Me.menuViewModifClient.Size = New System.Drawing.Size(936, 24)
        Me.menuViewModifClient.TabIndex = 193
        Me.menuViewModifClient.Text = "MenuStrip1"
        Me.menuViewModifClient.Visible = False
        '
        'menuComm
        '
        Me.menuComm.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnregistrerToolStripMenuItem, Me.ImporterToolStripMenuItem, Me.OuvrirLeFichierJointToolStripMenuItem, Me.SupprimerLeFichierJointToolStripMenuItem, Me.SupprimerToolStripMenuItem})
        Me.menuComm.Name = "menuComm"
        Me.menuComm.Size = New System.Drawing.Size(122, 20)
        Me.menuComm.Text = "menuContextMenu"
        Me.menuComm.Visible = False
        '
        'EnregistrerToolStripMenuItem
        '
        Me.EnregistrerToolStripMenuItem.Name = "EnregistrerToolStripMenuItem"
        Me.EnregistrerToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.EnregistrerToolStripMenuItem.Text = "Enregistrer"
        '
        'ImporterToolStripMenuItem
        '
        Me.ImporterToolStripMenuItem.Name = "ImporterToolStripMenuItem"
        Me.ImporterToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.ImporterToolStripMenuItem.Text = "Importer"
        '
        'OuvrirLeFichierJointToolStripMenuItem
        '
        Me.OuvrirLeFichierJointToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.OuvrirLeFichierJointToolStripMenuItem.Name = "OuvrirLeFichierJointToolStripMenuItem"
        Me.OuvrirLeFichierJointToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.OuvrirLeFichierJointToolStripMenuItem.Text = "Ouvrir le fichier joint"
        '
        'SupprimerLeFichierJointToolStripMenuItem
        '
        Me.SupprimerLeFichierJointToolStripMenuItem.Name = "SupprimerLeFichierJointToolStripMenuItem"
        Me.SupprimerLeFichierJointToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.SupprimerLeFichierJointToolStripMenuItem.Text = "Supprimer le fichier joint"
        '
        'SupprimerToolStripMenuItem
        '
        Me.SupprimerToolStripMenuItem.Name = "SupprimerToolStripMenuItem"
        Me.SupprimerToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.SupprimerToolStripMenuItem.Text = "Supprimer"
        '
        'addAlertMenu
        '
        Me.addAlertMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompteToolStripMenuItem, Me.RapportAuMédecinToolStripMenuItem})
        Me.addAlertMenu.Name = "addAlertMenu"
        Me.addAlertMenu.Size = New System.Drawing.Size(182, 48)
        '
        'CompteToolStripMenuItem
        '
        Me.CompteToolStripMenuItem.Name = "CompteToolStripMenuItem"
        Me.CompteToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CompteToolStripMenuItem.Text = "Compte"
        '
        'RapportAuMédecinToolStripMenuItem
        '
        Me.RapportAuMédecinToolStripMenuItem.Name = "RapportAuMédecinToolStripMenuItem"
        Me.RapportAuMédecinToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.RapportAuMédecinToolStripMenuItem.Text = "Rapport au médecin"
        '
        'StyleType
        '
        Me.StyleType.Format = ""
        Me.StyleType.FormatInfo = Nothing
        Me.StyleType.HeaderText = "Type"
        Me.StyleType.MappingName = "Type"
        Me.StyleType.Width = 38
        '
        'StyleNumerodelitem
        '
        Me.StyleNumerodelitem.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StyleNumerodelitem.Format = ""
        Me.StyleNumerodelitem.FormatInfo = Nothing
        Me.StyleNumerodelitem.HeaderText = "Numéro de l'item"
        Me.StyleNumerodelitem.MappingName = "Numéro de l'item"
        Me.StyleNumerodelitem.Width = 53
        '
        'StyleFacture
        '
        Me.StyleFacture.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StyleFacture.Format = ""
        Me.StyleFacture.FormatInfo = Nothing
        Me.StyleFacture.HeaderText = "Facture"
        Me.StyleFacture.MappingName = "Facture"
        Me.StyleFacture.Width = 48
        '
        'StyleTherapeute
        '
        Me.StyleTherapeute.Format = ""
        Me.StyleTherapeute.FormatInfo = Nothing
        Me.StyleTherapeute.HeaderText = "Thérapeute"
        Me.StyleTherapeute.MappingName = "Thérapeute"
        Me.StyleTherapeute.Width = 119
        '
        'StyleItem
        '
        Me.StyleItem.Format = ""
        Me.StyleItem.FormatInfo = Nothing
        Me.StyleItem.HeaderText = "Item"
        Me.StyleItem.MappingName = "Item"
        Me.StyleItem.Width = 227
        '
        'StyleDate
        '
        Me.StyleDate.Format = ""
        Me.StyleDate.FormatInfo = Nothing
        Me.StyleDate.HeaderText = "Date"
        Me.StyleDate.MappingName = "Date"
        Me.StyleDate.Width = 65
        '
        'menuExportFolder
        '
        Me.menuExportFolder.Index = 4
        Me.menuExportFolder.Text = "Exporter"
        '
        'viewmodifclients
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(936, 624)
        Me.Controls.Add(Me.selectionner)
        Me.Controls.Add(Me.reference)
        Me.Controls.Add(Me.Telephones)
        Me.Controls.Add(Me.nam)
        Me.Controls.Add(Me.AdminPanel)
        Me.Controls.Add(Me.publipostage)
        Me.Controls.Add(Me.url)
        Me.Controls.Add(Me.courriel)
        Me.Controls.Add(Me.remarques)
        Me.Controls.Add(Me.prenom)
        Me.Controls.Add(Me.ville)
        Me.Controls.Add(Me.autrenom)
        Me.Controls.Add(Me.adresse)
        Me.Controls.Add(Me.nom)
        Me.Controls.Add(Me.codepostal2)
        Me.Controls.Add(Me.codepostal1)
        Me.Controls.Add(Me.metierslist)
        Me.Controls.Add(Me.employeurslist)
        Me.Controls.Add(Me.annee)
        Me.Controls.Add(Me.mois)
        Me.Controls.Add(Me.jour)
        Me.Controls.Add(Me.StartStopCommChanging)
        Me.Controls.Add(Me.StartStopBillChanging)
        Me.Controls.Add(Me.ongletsclient)
        Me.Controls.Add(Me.label21)
        Me.Controls.Add(Me._Label1_16)
        Me.Controls.Add(Me._Label1_4)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me._Label1_17)
        Me.Controls.Add(Me._Label1_15)
        Me.Controls.Add(Me._Label1_14)
        Me.Controls.Add(Me._Label1_11)
        Me.Controls.Add(Me._Label1_8)
        Me.Controls.Add(Me._Label1_7)
        Me.Controls.Add(Me._Label1_6)
        Me.Controls.Add(Me._Label1_5)
        Me.Controls.Add(Me._Label1_0)
        Me.Controls.Add(Me._Label1_1)
        Me.Controls.Add(Me._Label1_2)
        Me.Controls.Add(Me._Label1_3)
        Me.Controls.Add(Me._Label2_2)
        Me.Controls.Add(Me.DownTel)
        Me.Controls.Add(Me.UpTel)
        Me.Controls.Add(Me.AddTel)
        Me.Controls.Add(Me.ModifTel)
        Me.Controls.Add(Me.DelTel)
        Me.Controls.Add(Me._sexe_1)
        Me.Controls.Add(Me._sexe_0)
        Me.Controls.Add(Me.enleverphoto)
        Me.Controls.Add(Me.btnAddAsKP)
        Me.Controls.Add(Me.modifenable)
        Me.Controls.Add(Me.btnAddAlert)
        Me.Controls.Add(Me.btnSendEmail)
        Me.Controls.Add(Me.choosephoto)
        Me.Controls.Add(Me.paiements)
        Me.Controls.Add(Me.photo)
        Me.Controls.Add(Me.createBill)
        Me.Controls.Add(Me.menuViewModifClient)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(39, 65)
        Me.MainMenuStrip = Me.menuViewModifClient
        Me.MaximizeBox = False
        Me.Name = "viewmodifclients"
        Me.ShowInTaskbar = False
        Me.Text = "Client : XXXX00000000"
        Me.ongletsclient.ResumeLayout(False)
        Me._ongletsclient_TabPage1.ResumeLayout(False)
        Me._ongletsclient_TabPage4.ResumeLayout(False)
        Me.GroupComm.ResumeLayout(False)
        Me.GroupComm.PerformLayout()
        Me.panel2.ResumeLayout(False)
        Me._ongletsclient_TabPage3.ResumeLayout(False)
        CType(Me.facturesView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me._ongletsclient_TabPage0.ResumeLayout(False)
        Me.folderHistoryFrame.ResumeLayout(False)
        Me.folderHistoryFrame.PerformLayout()
        CType(Me.folderHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ongletsdossier.ResumeLayout(False)
        Me._ongletsdossier_TabPage4.ResumeLayout(False)
        CType(Me.listEquipement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameE1.ResumeLayout(False)
        Me.frameE1.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.frameE2.ResumeLayout(False)
        Me.frameE2.PerformLayout()
        CType(Me.eSignature, System.ComponentModel.ISupportInitialize).EndInit()
        Me._ongletsdossier_TabPage1.ResumeLayout(False)
        Me._ongletsdossier_TabPage0.ResumeLayout(False)
        Me._ongletsclient_TabPage2.ResumeLayout(False)
        Me._ongletsclient_TabPage2.PerformLayout()
        CType(Me.visiteHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.photo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AdminPanel.ResumeLayout(False)
        Me.AdminPanel.PerformLayout()
        CType(Me.dsVentePret, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuViewModifClient.ResumeLayout(False)
        Me.menuViewModifClient.PerformLayout()
        Me.addAlertMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region "Définitions"

    Private Const AND_CONJUNCTION As String = " et "

    Private isFormClosed As Boolean = False

    Private loadingDossiersDueToInfos As Boolean = False
    Private lastLoadedBill As Integer = 0
    Private newFolderNoClient As Integer = 0
    Private lastVisiteSelected As Short = -1
    Private isLoadOnlyList As Boolean = False
    Private _curDoctorNo, OldNAM, OldService, OldRegion, OldTherapeute, OldItemText, thisSettings() As String
    Private oldFolderCode As FolderCode
    Private acceptFolderCodeModification As Boolean = True
    Private DisplayEmployeur, displayMetier As Boolean
    Private updatePret As Boolean = True
    Private accountInfosModified As Boolean = False
    Private antecedentModified As Boolean = False
    Private commModified As Boolean = False
    Private folderEquipmentModified As Boolean = False
    Private folderTextsModified As Boolean = False
    Private loadingDossier As Boolean = True
    Private loadingCompte As Boolean = True
    Private myPhoto As Bitmap
    Private imgModifSave As ImageList
    Private _NoClient As Integer
    Private currentNoFolder As Integer = 0
    Private lastTextBoxSel As Clinica.WebTextControl
    Private _allowFolderEquipmentModification As Boolean = False
    Private _allowFolderInfosModification As Boolean = False
    Private _allowFolderTextsModification As Boolean = False

    Private curItem As Item
    Private lastNoFolderText As Integer = 0
#End Region

#Region "menudossierstatut Events"
    Private Sub menustatutFerme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menustatutFerme.Click
        menudossierstatut_Click(1, sender, e)
    End Sub

    Private Sub menustatutOuvert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menustatutOuvert.Click
        menudossierstatut_Click(0, sender, e)
    End Sub
#End Region

#Region "Sexe Events"
    Private Sub _sexe_0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _sexe_0.CheckedChanged
        sexe_CheckChanged(1, sender, e)
        accountInfosModified = True
    End Sub

    Private Sub _sexe_0_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _sexe_0.KeyDown
        sexe_KeyDown(0, sender, e)
    End Sub

    Private Sub _sexe_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _sexe_1.CheckedChanged
        sexe_CheckChanged(0, sender, e)
        accountInfosModified = True
    End Sub

    Private Sub _sexe_1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _sexe_1.KeyDown
        sexe_KeyDown(1, sender, e)
    End Sub
#End Region

#Region "Propriétés"
    Public Property noClient() As Integer
        Get
            Return _NoClient
        End Get
        Set(ByVal Value As Integer)
            _NoClient = Value
        End Set
    End Property

    Public WriteOnly Property setFolderModified() As Boolean
        Set(ByVal Value As Boolean)
            ctlFolderInfos.isFolderModified = Value
        End Set
    End Property

    Public Property curDoctorNo() As String
        Get
            Return _curDoctorNo
        End Get
        Set(ByVal Value As String)
            _curDoctorNo = Value
        End Set
    End Property

    Private Property allowFolderTextsModification() As Boolean
        Get
            Return _allowFolderTextsModification
        End Get
        Set(ByVal value As Boolean)
            _allowFolderTextsModification = value
        End Set
    End Property

    Private Property allowFolderInfosModification() As Boolean
        Get
            Return _allowFolderInfosModification
        End Get
        Set(ByVal value As Boolean)
            _allowFolderInfosModification = value
        End Set
    End Property

    Private Property allowFolderEquipmentModification() As Boolean
        Get
            Return _allowFolderEquipmentModification
        End Get
        Set(ByVal value As Boolean)
            _allowFolderEquipmentModification = value
        End Set
    End Property
#End Region

#Region "Admin"
    Private Sub adminInterval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminInterval.TextChanged
        LegendeTimer.Interval = AdminInterval.Text
    End Sub

    Private Sub adminCloseBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminCloseBtn.Click
        AdminPanel.Visible = False
    End Sub

    Private Sub adminButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminButton1.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        ongletsdossier.SelectedIndex = myInputBoxPlus("Index de l'onglet à sélectionner", "Index", "1")
    End Sub

    Private Sub adminButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminButton2.Click
        MessageBox.Show(ongletsdossier.SelectedIndex)
    End Sub
#End Region

    Private Enum Sectors As Integer
        Account = 0
        AccountInfos = 1
        AccountFolderInfos = 2
        AccountFolderTexts = 4
        AccountFolderEquipment = 8
        AccountCommunications = 16
        AccountHealthStatus = 32
    End Enum

    Private Function askSavingQuestion(ByVal sector As Sectors) As Boolean
        'TODO : Shall apply same question detail all over !!
        Dim sectorIsAccount As Boolean = sector = Sectors.Account
        Dim folderSector As Integer = Sectors.AccountFolderInfos Or Sectors.AccountFolderTexts Or Sectors.AccountFolderEquipment
        Dim saveMessage As String = String.Empty

        If accountInfosModified AndAlso (sectorIsAccount OrElse (sector And Sectors.AccountInfos) = Sectors.AccountInfos) Then
            saveMessage &= vbCrLf & "- Les informations de base du client ?"
        End If
        If commModified AndAlso (sectorIsAccount OrElse (sector And Sectors.AccountCommunications) = Sectors.AccountCommunications) Then
            saveMessage &= vbCrLf & "- Les communications du client ?"
        End If
        If antecedentModified AndAlso (sectorIsAccount OrElse (sector And Sectors.AccountHealthStatus) = Sectors.AccountHealthStatus) Then
            saveMessage &= vbCrLf & "- Le bilan de santé du client ?"
        End If

        If (sectorIsAccount OrElse (sector And folderSector) <> 0) Then
            Dim saveFolderWhat As String = If(folderEquipmentModified AndAlso (sectorIsAccount OrElse (sector And Sectors.AccountFolderEquipment) = Sectors.AccountFolderEquipment), "l'équipement", String.Empty)
            saveFolderWhat &= If(saveFolderWhat <> String.Empty, AND_CONJUNCTION, String.Empty)
            saveFolderWhat &= If(ctlFolderInfos.isFolderModified AndAlso (sectorIsAccount OrElse (sector And Sectors.AccountFolderInfos) = Sectors.AccountFolderInfos), "les informations de base", String.Empty)
            If saveFolderWhat <> String.Empty Then
                saveFolderWhat = saveFolderWhat.Replace(AND_CONJUNCTION, ", ")
                saveFolderWhat &= AND_CONJUNCTION
            End If
            saveFolderWhat &= If(folderTextsModified AndAlso (sectorIsAccount OrElse (sector And Sectors.AccountFolderTexts) = Sectors.AccountFolderTexts), "les textes", String.Empty)

            If saveFolderWhat <> String.Empty Then saveMessage &= vbCrLf & "- " & saveFolderWhat.Substring(0, 1).ToUpper & saveFolderWhat.Substring(1) & " du dossier #" & currentNoFolder & " ?"
        End If

        If saveMessage = String.Empty Then Return False

        saveMessage = "Désirez-vous enregistrer les modifications suivantes :" & saveMessage
        If MessageBox.Show(saveMessage, "Enregistrement pour le compte " & Me.Text.Substring(0, 1).ToLower & Me.Text.Substring(1).Replace(":", "-"), MessageBoxButtons.YesNo) = DialogResult.Yes Then Return True

        Return False
    End Function

#Region "Window Events"
    Private Sub viewmodifclients_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        If nom.ReadOnly = False Then lockSecteur("ClientGenInfo-" & noClient & "-", False)
        If allowFolderTextsModification Then lockSecteur("ClientFolderText-" & noClient & "-" & currentNoFolder & "-", False)
        If allowFolderInfosModification Then lockSecteur("ClientFolderInfos-" & noClient & "-" & currentNoFolder & "-", False)
        If allowFolderEquipmentModification Then lockSecteur("ClientFolderEquip-" & noClient & "-" & currentNoFolder & "-", False)
        If ToolTip1.GetToolTip(saveantecedent).StartsWith("Enregistrer") Then lockSecteur("ClientAntecedents-" & noClient & "-", False)
        For i As Integer = 0 To currentLocks.Count - 1
            lockSecteur(currentLocks(i), False)
        Next i
        If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("A") = True Then lockSecteur("ClientComm-" & noClient & "-", False)
    End Sub

    Private Sub viewmodifclients_FormClosing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.FormClosing
        If askSavingQuestion(Sectors.Account) AndAlso saveForm() <> "Saved" Then e.Cancel = True : Exit Sub

        Dim equipSettings As String = ""
        If dsVentePret.Tables("DataTable1") Is Nothing Then
            If thisSettings Is Nothing OrElse thisSettings.Length = 0 Then
                equipSettings = "65§227§119§48§53§38§Date 2"
            Else
                equipSettings = thisSettings(0) & "§" & thisSettings(1) & "§" & thisSettings(2) & "§" & thisSettings(3) & "§" & thisSettings(4) & "§" & thisSettings(5) & "§" & thisSettings(6)
            End If
        Else
            For Each curColumn As DataGridViewColumn In listEquipement.Columns
                equipSettings &= curColumn.Width & "§"
            Next

            If listEquipement.SortedColumn IsNot Nothing Then equipSettings &= listEquipement.SortedColumn.Name & " " & CInt(listEquipement.SortOrder).ToString
        End If

        Dim curUser As User = UsersManager.currentUser
        curUser.settings.accountEquipmentStyle = equipSettings
        curUser.settings.clientLastTabs = ongletsclient.SelectedTab.Text & "§" & ongletsdossier.SelectedTab.Text & "§" & folderTexts.Text
        curUser.settings.saveData()

        isFormClosed = True
    End Sub
#End Region

#Region "GeneralInfo Form"
    Private Const email_field_not_filled As String = "* Courriel non entré *"
    Private Const url_field_not_filled As String = "* Adresse non entrée *"

    Private Sub jour_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles jour.Validated
        jour.Text = addZeros(jour.Text, 2)
        If jour.Text = "00" Then jour.Text = "01"
    End Sub

    Private Sub mois_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles mois.Validated
        mois.Text = addZeros(mois.Text, 2)
        If mois.Text = "00" Then mois.Text = "01"
    End Sub

    Private Sub courriel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles courriel.KeyDown
        If Not courriel.ReadOnly AndAlso e.KeyCode <> Keys.Enter AndAlso e.KeyCode <> Keys.Tab AndAlso courriel.Text = email_field_not_filled Then
            courriel.Text = ""
            courriel.manageText = True
        End If
    End Sub

    Private Sub url_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles url.KeyDown
        If Not url.ReadOnly AndAlso e.KeyCode <> Keys.Enter AndAlso e.KeyCode <> Keys.Tab AndAlso url.Text = url_field_not_filled Then
            url.Text = ""
            url.manageText = True
        End If
    End Sub

    Private Sub objWithForwardBackwardFocus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nam.KeyDown, prenom.KeyDown, mois.KeyDown, metierslist.KeyDown, adresse.KeyDown, ville.KeyDown, autrenom.KeyDown, codepostal1.KeyDown, codepostal2.KeyDown, courriel.KeyDown, url.KeyDown, publipostage.KeyDown, Telephones.KeyDown
        If e.KeyCode = Keys.Back AndAlso ((TypeOf (sender) Is ComboBox AndAlso CType(sender, ComboBox).DropDownStyle = ComboBoxStyle.DropDown AndAlso sender.Text = "") Or sender.Text = "") Then Me.GetNextControl(sender, False).Focus() : e.Handled = True
        If e.KeyCode = Keys.Enter Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
    End Sub

    Private Sub selectionner_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles selectionner.KeyDown
        If e.KeyCode = Keys.Back Then Me.GetNextControl(sender, False).Focus()
    End Sub

    Private Sub jour_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles jour.KeyUp
        If jour.Text.Length = 2 Then
            If sexe(1).checked = True Then
                CType(sexe(1), RadioButton).Focus()
            Else
                CType(sexe(0), RadioButton).Focus()
            End If
        End If
    End Sub

    Private Sub jour_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles jour.KeyDown
        If e.KeyCode = Keys.Back And jour.Text = "" Then mois.Focus()
        If e.KeyCode = Keys.Enter Then
            If sexe(1).checked = True Then
                CType(sexe(1), RadioButton).Focus()
            Else
                CType(sexe(0), RadioButton).Focus()
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub annee_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles annee.KeyDown
        If e.KeyCode = Keys.Back And annee.Text = "" Then selectionner.Focus()
        If e.KeyCode = Keys.Enter Then mois.Focus() : e.Handled = True
    End Sub

    Private Sub addTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTel.Click
        Dim myInputBoxPlus As New InputBoxPlus(True, , "TelePhoneTitles.TelephoneTitle")
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ":"
        Dim newTitle As String = myInputBoxPlus("Veuillez entrer le titre du numéro de téléphone", "Titre")
        If newTitle = "" Then Exit Sub

        myInputBoxPlus = New InputBoxPlus()
        myInputBoxPlus.acceptAlpha = False
        myInputBoxPlus.acceptedChars = "-§#"
        myInputBoxPlus.refusedChars = ":"

        Dim newTel As String = myInputBoxPlus("Veuillez entrer le numéro de téléphone", "Numéro de téléphone")
        If newTel = "" Then Exit Sub

        DBHelper.addItemToADBList("TelephoneTitles", "TelephoneTitle", newTitle, "NoTelephoneTitle")

        Dim n As Integer = Telephones.Items.Add(newTitle & ":" & newTel)
        Telephones.SelectedIndex = n
        accountInfosModified = True
    End Sub

    Private Sub telephones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Telephones.SelectedIndexChanged
        If Telephones.SelectedIndex <> -1 And AddTel.Enabled = True Then
            ModifTel.Enabled = True
            DelTel.Enabled = True
            If Telephones.Items.Count > 1 Then
                If Telephones.SelectedIndex = 0 Then
                    UpTel.Enabled = False
                Else
                    UpTel.Enabled = True
                End If
                If Telephones.SelectedIndex = (Telephones.Items.Count - 1) Then
                    DownTel.Enabled = False
                Else
                    DownTel.Enabled = True
                End If
            Else
                UpTel.Enabled = False
                DownTel.Enabled = False
            End If
        Else
            ModifTel.Enabled = False
            DelTel.Enabled = False
            UpTel.Enabled = False
            DownTel.Enabled = False
        End If
    End Sub

    Private Sub modifTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifTel.Click
        If Telephones.SelectedIndex < 0 Then MessageBox.Show("Veuillez sélectionner un numéro de téléphone", "Impossible de modifier") : Exit Sub

        Dim myPhone() As String = Telephones.GetItemText(Telephones.SelectedItem).Split(New Char() {":"})

        Dim myInputBoxPlus As New InputBoxPlus(True, , "TelePhoneTitles.TelephoneTitle")
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ":"
        Dim newTitle As String = myInputBoxPlus("Veuillez entrer le titre du numéro de téléphone", "Titre", myPhone(0))
        If newTitle = "" Then Exit Sub

        myInputBoxPlus = New InputBoxPlus()
        myInputBoxPlus.acceptAlpha = False
        myInputBoxPlus.acceptedChars = "-§#"
        myInputBoxPlus.refusedChars = ":"

        Dim newTel As String = myInputBoxPlus("Veuillez entrer le numéro de téléphone", "Numéro de téléphone", myPhone(1))
        If newTel = "" Then Exit Sub

        DBHelper.addItemToADBList("TelephoneTitles", "TelephoneTitle", newTitle, "NoTelephoneTitle")

        Telephones.Items(Telephones.SelectedIndex) = newTitle & ":" & newTel
        accountInfosModified = True
    End Sub

    Private Sub delTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelTel.Click
        If Telephones.SelectedIndex < 0 Then MessageBox.Show("Veuillez sélectionner un numéro de téléphone", "Impossible de modifier") : Exit Sub

        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce numéro de téléphone ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Telephones.Items.RemoveAt(Telephones.SelectedIndex)
        DownTel.Enabled = False
        UpTel.Enabled = False
        ModifTel.Enabled = False
        DelTel.Enabled = False
        accountInfosModified = True
    End Sub

    Private Sub upTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpTel.Click
        Dim SPhones(), selItem As String
        Dim curIndex As Integer
        curIndex = Telephones.SelectedIndex
        ReDim SPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(SPhones, 0)

        selItem = SPhones(curIndex)
        SPhones(curIndex) = SPhones(curIndex - 1)
        SPhones(curIndex - 1) = selItem
        Telephones.Items.Clear()
        Telephones.Items.AddRange(SPhones)
        Telephones.SelectedIndex = curIndex - 1
        accountInfosModified = True
    End Sub

    Private Sub downTel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownTel.Click
        Dim SPhones(), selItem As String
        Dim curIndex As Integer
        curIndex = Telephones.SelectedIndex
        ReDim SPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(SPhones, 0)

        selItem = SPhones(curIndex)
        SPhones(curIndex) = SPhones(curIndex + 1)
        SPhones(curIndex + 1) = selItem
        Telephones.Items.Clear()
        Telephones.Items.AddRange(SPhones)
        Telephones.SelectedIndex = curIndex + 1
        accountInfosModified = True
    End Sub

    Private Sub publipostage_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles publipostage.SelectionChangeCommitted
        accountInfosModified = True
    End Sub

    Private Sub allGenForm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles reference.TextChanged, remarques.TextChanged, url.TextChanged, adresse.TextChanged, autrenom.TextChanged, courriel.TextChanged, ville.TextChanged, employeurslist.TextChanged, metierslist.TextChanged
        accountInfosModified = True
    End Sub

    Private Sub adjustNam(ByVal sender As Object)
        curAdjustNAM = True

        Dim NewNAM, currentNAM As String
        Dim maxLength As Byte

        Dim stripNom As String = Chaines.replaceAccents(nom.Text)
        Chaines.forceManaging(stripNom, Me.nam.currencyBox, "", nam.acceptAlpha, nam.acceptNumeric, nam.onlyAlphabet, nam.refuseAccents, nam.acceptedChars, nam.refusedChars, nam.matchExp, nam.firstLetterCapital, nam.firstLettersCapital, nam.allCapital, nam.allLower, nam.nbDecimals)
        maxLength = stripNom.Length
        If maxLength = 0 Then Exit Sub
        If maxLength > 3 Then maxLength = 3
        NewNAM = stripNom.Substring(0, maxLength)

        If maxLength >= 3 Then
            Dim stripPrenom As String = Chaines.replaceAccents(prenom.Text)
            Chaines.forceManaging(stripPrenom, Me.nam.currencyBox, "", nam.acceptAlpha, nam.acceptNumeric, nam.onlyAlphabet, nam.refuseAccents, nam.acceptedChars, nam.refusedChars, nam.matchExp, nam.firstLetterCapital, nam.firstLettersCapital, nam.allCapital, nam.allLower, nam.nbDecimals)
            maxLength = stripPrenom.Length
            If maxLength = 0 Then GoTo Skipping
            If maxLength > 1 Then maxLength = 1
            NewNAM &= stripPrenom.Substring(0, maxLength)

            If maxLength > 0 And annee.Text.Length = 4 Then
                NewNAM &= annee.Text.Substring(2)
                If mois.Text <> "" Then
                    Dim myMonth As String = mois.Text

                    myMonth = addZeros(myMonth, 2)
                    If sexe(1).checked = True Then myMonth += 50
                    NewNAM &= myMonth

                    If jour.Text <> "" Then
                        Dim myDay As String = jour.Text

                        myDay = addZeros(myDay, 2)
                        NewNAM &= myDay
                    End If
                End If
            End If
        End If

Skipping:
        'Ajustement du NAM
        currentNAM = nam.Text
        NewNAM = replaceAccents(NewNAM)
        If currentNAM.Length <= NewNAM.Length Then
            nam.Text = NewNAM
        Else
            nam.Text = NewNAM & currentNAM.Substring(NewNAM.Length, currentNAM.Length - NewNAM.Length)
        End If

        If TypeOf (sender) Is RadioButton Then
            If sender.checked = True Then
                sender.focus()
            End If
        Else
            'sender.focus()
        End If
        Try
            If TypeOf sender Is ManagedText Or TypeOf sender Is ManagedCombo Then sender.selectionstart = sender.text.length
        Catch ex As Exception
        End Try

        curAdjustNAM = False
    End Sub

    Private Sub annee_TextChanged(ByVal sender As System.Object, ByVal eventArgs As System.EventArgs) Handles annee.TextChanged
        Dim ss As Integer = annee.SelectionStart
        If annee.Text.Length > 4 Then annee.Text = annee.Text.Substring(0, 4)

        annee_SelectedIndexChanged(annee, New System.EventArgs())
        If annee.Text.Length < 4 Then
            'annee.Focus()
            annee.SelectionStart = ss
        End If
        'Me.GetNextControl(sender, True).Focus()
    End Sub

    Private Sub annee_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles annee.SelectedIndexChanged
        adjustNam(annee)

        If Not mois.Text = "" Then mois_SelectedIndexChanged(mois, New System.EventArgs())
        accountInfosModified = True
    End Sub

    Private Sub codepostal1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles codepostal1.TextChanged
        If codepostal1.Text.Length = 3 Then codepostal2.Focus()
        accountInfosModified = True
    End Sub

    Private Sub codepostal2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles codepostal2.TextChanged
        If codepostal2.Text.Length = 3 Then autrenom.Focus()
        accountInfosModified = True
    End Sub

    Private Sub employeurslist_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles employeurslist.KeyDown
        If e.KeyCode = Keys.Back And sender.Text = "" Then
            If sexe(1).Checked = True Then
                sexe(1).Focus()
            Else
                sexe(0).Focus()
            End If
        End If
        If e.KeyCode = Keys.Enter Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
    End Sub

    Private Sub jour_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles jour.TextChanged
        If jour.Text = "" Then Exit Sub
        If jour.Text.Length > 2 Then jour.Text = jour.Text.Substring(0, 2)

        jour_SelectedIndexChanged(jour, New System.EventArgs())
        If Integer.Parse(jour.Text) > Integer.Parse(jour.Items(jour.Items.Count - 1)) Then jour.Text = jour.GetItemText(jour.Items.Item(jour.Items.Count - 1))
    End Sub

    Private Sub jour_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles jour.SelectedIndexChanged
        adjustNam(jour)
        accountInfosModified = True
    End Sub

    Private Sub mois_TextChanged(ByVal sender As System.Object, ByVal eventArgs As System.EventArgs) Handles mois.TextChanged
        If mois.Text.Length > 2 Then mois.Text.Substring(0, 2)

        If mois.Text.Length > 0 Then
            If jour.Text > jour.GetItemText(jour.Items.Item(jour.Items.Count - 1)) Then jour.Text = jour.GetItemText(jour.Items.Item(jour.Items.Count - 1))
            If CDbl(mois.Text) > 12 Then mois.Text = CStr(12)
            If mois.Text.Length = 2 Then Me.GetNextControl(sender, True)
        End If
        mois_SelectedIndexChanged(mois, New System.EventArgs())
    End Sub

    Private Sub mois_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mois.SelectedIndexChanged
        Dim an3, an2, an1 As String
        Dim monthDays() As Byte = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        If mois.Text.Length > 0 Then
            adjustNam(mois)

            'Modification du nombre de jour dépendamment du mois
            If Not annee.Text = "" Then
                an1 = CDbl(annee.Text) / 400
                an2 = CDbl(annee.Text) / 100
                an3 = CDbl(annee.Text) / 4
                If an3.IndexOf(",") = -1 And an3.IndexOf(".") = -1 Then monthDays(1) = 29
                If an2.IndexOf(",") = -1 And an2.IndexOf(".") = -1 Then monthDays(1) = 28
                If an1.IndexOf(",") = -1 And an1.IndexOf(".") = -1 Then monthDays(1) = 29

                If CDbl(mois.Text) > 0 Then
                    If CDbl(mois.Text) > 12 Then mois.Text = 12
                    Dim curJour As Integer = -1
                    If jour.Text <> "" Then curJour = Integer.Parse(jour.Text)
                    Do
                        If monthDays(CDbl(mois.Text) - 1) > jour.Items.Count Then
                            jour.Items.Add(CStr(jour.Items.Count + 1))
                        ElseIf monthDays(CDbl(mois.Text) - 1) < jour.Items.Count Then
                            jour.Items.RemoveAt(jour.Items.Count - 1)
                        End If
                    Loop Until monthDays(CDbl(mois.Text) - 1) = jour.Items.Count
                    If curJour > -1 AndAlso curJour > Integer.Parse(jour.Items(jour.Items.Count - 1)) Then curJour = Integer.Parse(jour.Items(jour.Items.Count - 1))
                    If curJour > -1 Then jour.Text = curJour
                End If
            End If
        End If

        accountInfosModified = True
    End Sub

    Private Sub nom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles nom.KeyDown
        If e.KeyCode = Keys.Enter Then Me.GetNextControl(sender, True).Focus() : e.Handled = True
    End Sub

    Private Sub remarques_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles remarques.KeyDown
        If e.KeyCode = Keys.Back And sender.Text = "" Then Me.GetNextControl(sender, False).Focus()
    End Sub

    Private Sub selectionner_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles selectionner.Click
        RefMenu.Show(selectionner, New Point(0, 0))
    End Sub

    Private Sub nom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nom.TextChanged
        adjustNam(nom)
        accountInfosModified = True
    End Sub

    Private Sub prenom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles prenom.TextChanged
        adjustNam(prenom)
        accountInfosModified = True
    End Sub

    Private Sub nam_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles nam.TextChanged
        If nam.Text.Length = 12 And curAdjustNAM = False Then courriel.Focus()
        accountInfosModified = True
    End Sub

    Private curAdjustNAM As Boolean = False

    Private Sub sexe_CheckChanged(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles sexe.checkChanged
        If curAdjustNAM = False Then adjustNam(sexe(index))
    End Sub

    Private Sub sexe_KeyDown(ByRef index As Short, ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles sexe.keyDown
        If e.KeyCode = Keys.Back Then jour.Focus() : e.Handled = True
        If e.KeyCode = Keys.Enter Then employeurslist.Focus() : e.Handled = True
    End Sub

    Private Sub menuRefAucun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRefAucun.Click
        uncheckMenus()
        menuRefAucun.Checked = True
        reference.Text = "Nom du référent"
        reference.Tag = ""
    End Sub

    Private Sub menuRefAutre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRefAutre.Click
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        Dim myRef As String = myInputBoxPlus.Prompt("Veuillez entrer le nom du référent", "Référent")

        If myRef <> "" Then
            reference.Text = myRef : reference.Tag = "AUTRE"
            uncheckMenus()
            menuRefAutre.Checked = True
        End If
    End Sub

    Private Sub menuRefCompte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRefCompte.Click
        Dim myRecherche As New clientSearch()
        myRecherche.Visible = False
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()
    End Sub

    Private Sub menuRefKP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRefKP.Click
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.Visible = False
        myKeyPeople.selected = True
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            uncheckMenus()
            menuRefKP.Checked = True
            reference.Tag = "KP"
            reference.Text = kpChosen.noKP & vbCrLf & kpChosen.kpFullName
        End If
    End Sub

    Private Sub menuPreRefList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        With CType(sender, MenuItem)
            reference.Text = .Text : reference.Tag = "LIST"
            uncheckMenus()
            .Checked = True
        End With
    End Sub
#End Region

#Region "Communications Form"
    Private Sub commCategorie_DeletedItem(ByVal currentItem As String) Handles commCategorie.deletedItem
        commModified = False
        commFiltrageCat.Items.Remove(currentItem)
        loadCommunications(True)
    End Sub

    Private Sub commCategorie_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles commCategorie.TextChanged
        commModified = True
    End Sub

    Private Sub listeCommunications_ItemClick(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs) Handles listeCommunications.itemClick
        If Not (CType(sender, CI.Controls.List).clickEnabled = False Or listeCommunications.selected < 0) And e.button = 2 Then
            listeCommunications_WillSelect(sender, New CI.Controls.List.WillSelectEventArgs(e.selectedItem, 2, e.x, e.y, False))
            menuComm.showDropDown(True)
        End If
    End Sub

    Private Sub listeCommunications_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles listeCommunications.willSelect
        Try
            If (modifComm.Enabled = True Or btnAddComm.Enabled = True) AndAlso askSavingQuestion(Sectors.AccountCommunications) Then If saveCommunications() = "" Then Exit Sub
            commModified = False

            Dim trueFalse As Boolean = False
            If modifComm.Enabled Or btnAddComm.Enabled = True Then trueFalse = True

            EnregistrerToolStripMenuItem.Enabled = trueFalse
            ImporterToolStripMenuItem.Enabled = trueFalse
            SupprimerLeFichierJointToolStripMenuItem.Enabled = trueFalse
            SupprimerToolStripMenuItem.Enabled = trueFalse

            If e.selectedItem >= 0 Then
                Dim myComm() As String = listeCommunications.ItemValueB(e.selectedItem).Split(New Char() {"§"})
                If myComm(9) <> "" Then
                    Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
                    Dim pathFromApp As String = "", MyFileName As String = ""
                    Dim myType As OpeningType
                    Select Case sMyComm(0)
                        Case "DB"
                            myType = CommonProc.OpeningType.DB
                            MyFileName = getLastDir(sMyComm(1))
                            pathFromApp = sMyComm(1).Substring(0, sMyComm(1).Length - MyFileName.Length)
                        Case "FILE"
                            myType = CommonProc.OpeningType.FILE
                            MyFileName = sMyComm(1)
                            pathFromApp = "Clients\" & noClient & "\Comm\"

                        Case "EMAIL"
                            myType = CommonProc.OpeningType.EMAIL
                            MyFileName = sMyComm(1)
                            pathFromApp = "Clients\" & noClient & "\Comm\"
                        Case "REPORT"
                            myType = CommonProc.OpeningType.REPORT
                            MyFileName = sMyComm(1)
                            pathFromApp = "Clients\" & noClient & "\Comm\"
                    End Select

                    OuvrirLeFichierJointToolStripMenuItem.Enabled = True
                    If sMyComm(0) <> "DB" AndAlso (IO.File.Exists(appPath & bar(appPath) & pathFromApp & MyFileName) = False OrElse isCommunicationInUse()) Then
                        SupprimerLeFichierJointToolStripMenuItem.Enabled = False
                        OuvrirLeFichierJointToolStripMenuItem.Enabled = False
                    End If
                Else
                    SupprimerLeFichierJointToolStripMenuItem.Enabled = False
                    OuvrirLeFichierJointToolStripMenuItem.Enabled = False
                End If
            Else
                SupprimerLeFichierJointToolStripMenuItem.Enabled = False
                OuvrirLeFichierJointToolStripMenuItem.Enabled = False
            End If
        Catch ex As Exception
            listeCommunications.selected = -1
            addErrorLog(New Exception("bug testing" & vbCrLf & "NoClient=" & Me.noClient & "; e.selectedItem = " & e.selectedItem & "; listeCommunications.ItemText(e.SelectedItem)=" & listeCommunications.ItemText(e.selectedItem), ex))
        End Try
    End Sub

    Private Sub commFiltrages_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles commFiltrage.SelectionChangeCommitted, commFiltrageCat.SelectionChangeCommitted
        If (modifComm.Enabled = True Or btnAddComm.Enabled = True) AndAlso askSavingQuestion(Sectors.AccountCommunications) Then If saveCommunications() = "" Then Exit Sub

        loadCommunications(True)
    End Sub

    Private Sub commTypes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles commEnvoie.CheckedChanged, commReception.CheckedChanged, colorSent.Click, colorReceived.Click
        If (modifComm.Enabled = True Or btnAddComm.Enabled = True) AndAlso askSavingQuestion(Sectors.AccountCommunications) Then If saveCommunications() = "" Then Exit Sub
        If sender Is colorSent Then commEnvoie.Checked = Not commEnvoie.Checked
        If sender Is colorReceived Then commReception.Checked = Not commReception.Checked

        If commEnvoie.Checked = False And commReception.Checked = False Then
            If sender Is commEnvoie OrElse sender Is colorSent Then
                commReception.Checked = True
            Else
                commEnvoie.Checked = True
            End If
        End If

        If loadingCompte = False Then loadCommunications(True)
    End Sub

    Private Sub listeCommunications_SelectedChange() Handles listeCommunications.selectedChange
        If isLoadOnlyList Then Exit Sub

        If listeCommunications.selected = -1 Then
            viderComm_Click(listeCommunications, EventArgs.Empty)
        Else
            Dim i As Integer
            Dim myComm() As String = listeCommunications.ItemValueB(listeCommunications.selected).Split(New Char() {"§"})

            commType1.Checked = myComm(4)
            commType2.Checked = Not commType1.Checked

            If myComm(3) = 0 Then
                commDeA.SelectedIndex = 0
            Else
                Dim found As Integer
                found = -1
                For i = 0 To commDeA.Items.Count - 1
                    If CStr(commDeA.Items.Item(i)).IndexOf("(" & myComm(3) & ")") <> -1 Then found = i : Exit For
                Next i
                If found = -1 Then
                    commDeA.SelectedIndex = 0
                Else
                    commDeA.SelectedIndex = found
                End If
            End If

            commSujet.Text = myComm(5)
            commDate.Text = DateFormat.getTextDate(CDate(myComm(6)))
            commUser.Text = UsersManager.getInstance.getUser(myComm(7)).toString()
            CommRemarques.Text = myComm(8).Replace("<br>", vbCrLf)

            modifComm.Enabled = False

            If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("C") = False Then
                If myComm(9) = "" Then
                    importComm.Enabled = True
                Else
                    Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
                    If sMyComm(0) = "DB" Or sMyComm(0) = "FILE" Then
                        importComm.Enabled = True
                    Else
                        importComm.Enabled = False
                    End If
                End If
                modifComm.Enabled = True
                delComm.Enabled = True
            End If

            dossiersClist.SelectedIndex = 0
            For i = 0 To dossiersClist.Items.Count - 1
                If dossiersClist.Items(i).ToString.StartsWith(myComm(10) & " -") Then
                    dossiersClist.SelectedIndex = i
                    Exit For
                End If
            Next i

            commCategorie.Text = myComm(12)

            btnAddComm.Enabled = False
            commType1.Enabled = False
            commType2.Enabled = False
        End If
        commModified = False
    End Sub

    Private Sub selectCommDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectCommDate.Click
        Dim myDateChoice As New DateChoice()
        Dim MyDate As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year - 50, Date.Today.Year + 1, , , , , , , , , , , CDate(commDate.Text))
        If MyDate.Count <> 0 Then
            commDate.Text = DateFormat.getTextDate(MyDate(0))
            commModified = True
        End If
    End Sub

    Private Sub selectKeyPeople_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectKeyPeople.Click
        Dim oldKP As String = commDeA.SelectedText
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.Visible = False
        myKeyPeople.selected = True
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        Dim kpChosen As KPSelectorReturn = myKeyPeople.showDialog()
        If kpChosen.noKP <> 0 Then
            Dim Found, i As Integer
            Found = -1
            For i = 0 To commDeA.Items.Count - 1
                If commDeA.Items.Item(i).ToString.IndexOf(kpChosen.noKP) <> -1 Then Found = i : Exit For
            Next i
            If Found < 0 Then Found = commDeA.Items.Add(kpChosen.kpFullName & " (" & kpChosen.noKP & ")")
            commDeA.SelectedIndex = Found
        End If
        If oldKP <> commDeA.SelectedText Then commModified = True
    End Sub

    Private Sub commDeA_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles commDeA.SelectedIndexChanged
        If commDeA.SelectedIndex = -1 Then
            btnAddComm.Enabled = False
        Else
            If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("C") = False Then
                If listeCommunications.selected = -1 Then
                    If commSujet.Text = "" Or (commType1.Checked = False And commType2.Checked = False) Then
                        btnAddComm.Enabled = False
                    Else
                        btnAddComm.Enabled = True
                    End If
                Else
                    If commSujet.Text = "" Then
                        modifComm.Enabled = False
                    Else
                        modifComm.Enabled = True
                    End If
                End If
            End If
            commModified = True
        End If
    End Sub

    Private Sub commSujet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles commSujet.TextChanged
        If commSujet.Text = "" Then
            btnAddComm.Enabled = False
        Else
            If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("C") = False Then
                If listeCommunications.selected = -1 Then
                    If commDeA.SelectedIndex = -1 Or (commType1.Checked = False And commType2.Checked = False) Then
                        btnAddComm.Enabled = False
                    Else
                        btnAddComm.Enabled = True
                    End If
                Else
                    If commDeA.SelectedIndex = -1 Then
                        modifComm.Enabled = False
                    Else
                        modifComm.Enabled = True
                    End If
                End If
            End If
        End If
        commModified = True
    End Sub

    Private Sub commRemarques_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CommRemarques.TextChanged
        commModified = True
    End Sub

    Private Sub commType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles commType1.CheckedChanged, commType2.CheckedChanged
        commModified = True
        If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("C") = False Then
            If listeCommunications.selected = -1 Then
                If commSujet.Text = "" Or commDeA.SelectedIndex = -1 Then
                    btnAddComm.Enabled = False
                Else
                    btnAddComm.Enabled = True
                End If
            Else
                If commSujet.Text = "" Or commDeA.SelectedIndex = -1 Then
                    modifComm.Enabled = False
                Else
                    modifComm.Enabled = True
                End If
            End If
        End If

        If commType1.Checked Then
            lblCommDeA.Text = "À :"
        Else
            lblCommDeA.Text = "De :"
        End If
        commModified = True
    End Sub

    Private Sub dossiersClist_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dossiersClist.SelectedIndexChanged
        commModified = True
    End Sub
#End Region

#Region "Equipement Form"
    Private Sub listEquipement_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles listEquipement.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then selectingEquipment()
    End Sub

    Private Sub etrp_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles etrp.SelectionChangeCommitted
        folderEquipmentModified = True
    End Sub

    Private Sub eNoItem_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles eNoItem.SelectionChangeCommitted
        folderEquipmentModified = True
    End Sub

    Private Sub eVerified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eVerified.CheckedChanged
        folderEquipmentModified = True
    End Sub

    Private Sub eNotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eNotes.TextChanged
        folderEquipmentModified = True
    End Sub

    Private Sub eRefunded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eRefunded.CheckedChanged
        If eRefunded.Checked = True And eReturned.Checked = False Then MessageBox.Show("L'équipement doit être retourné avant d'être remboursé.", "Retour de l'équipement") : eRefunded.Checked = False : Exit Sub

        If eRefunded.Checked = True Then
            refundlabel.Text = "Montant remboursé :"
        Else
            refundlabel.Text = "Montant à rembourser :"
        End If
        eRefund_TextChanged(sender, e)
        folderEquipmentModified = True
    End Sub

    Private Sub eReturned_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles eReturned.Click
        If sender.Checked = True Then
            sender.Checked = False
        Else
            sender.Checked = True
        End If
    End Sub

    Private Sub loadFolderEquipmentNoItems()
        Dim curNoItem As String = eNoItem.Text

        eNoItem.Items.Clear()
        Dim curEquip As Equipment = eNom.SelectedItem

        Dim pretNoItems() As String = curEquip.noItemsBorrowed.ToArray
        For i As Integer = 0 To curEquip.noItems.Count - 1
            If searchArray(pretNoItems, curEquip.noItems(i), SearchType.ExactMatch) < 0 Then
                eNoItem.Items.Add(curEquip.noItems(i))

                'Reselect what was selected
                If curEquip.noItems(i) = curNoItem Then eNoItem.SelectedItem = curEquip.noItems(i)
            End If
        Next i
    End Sub


    Private Sub eNom_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles eNom.SelectionChangeCommitted
        Dim etype1Checked As Boolean = eType1.Checked
        Dim etype2Checked As Boolean = eType2.Checked

        resetFolderEquipmentFields(True, True)

        eType1.Checked = etype1Checked
        eType2.Checked = etype2Checked

        If eNom.SelectedIndex <> -1 Then
            Dim curEquip As Equipment = eNom.SelectedItem
            loadFolderEquipmentNoItems()

            Select Case curEquip.type
                Case Equipment.EquipmentType.Vente
                    eType1.Checked = True
                    If allowFolderEquipmentModification = True Then eType2.Enabled = False : eType1.Enabled = True
                Case Equipment.EquipmentType.Pret
                    eType2.Checked = True
                    If allowFolderEquipmentModification = True Then eType1.Enabled = False : eType2.Enabled = True
                Case Else
                    If allowFolderEquipmentModification = True Then eType2.Enabled = True : eType1.Enabled = True
                    eType_CheckedChanged(sender, e)
            End Select

            If curEquip.type <> Equipment.EquipmentType.Vente Then
                eDepot.Text = curEquip.depositAmount
                ePret.Text = curEquip.costAmountBy
                dateRetour_SelectedIndexChanged(sender, e)
            End If

            If eType1.Checked = True Then eProfit.Text = curEquip.itemSoldAmount : eProfit_TextChanged(sender, e)

            eDescription.Text = curEquip.description.Replace("<br>", vbCrLf)
            If curEquip.amountPaidByItem > 0 Then
                If eDescription.Text <> "" Then eDescription.Text &= vbCrLf
                eDescription.Text &= "Montant de l'achat : " & curEquip.amountPaidByItem & " $"
            End If

            eDate.Text = DateFormat.getTextDate(Date.Today)
            folderEquipmentModified = True
        End If
    End Sub

    Private Sub eType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles eType1.CheckedChanged, eType2.CheckedChanged
        If eNom.SelectedIndex = -1 Then loadFolderEquipmentInfos()
        If allowFolderEquipmentModification = True Then
            anretour.ReadOnly = Not eType2.Checked
            moisretour.ReadOnly = Not eType2.Checked
            jourretour.ReadOnly = Not eType2.Checked
            eDuree.ReadOnly = Not eType2.Checked
            eDepot.ReadOnly = Not eType2.Checked
            ePret.ReadOnly = Not eType2.Checked
            eVerified.Enabled = eType2.Checked
            eRefunded.Enabled = eType2.Checked
            eReturned.Enabled = eType2.Checked
            eNotes.ReadOnly = Not eType2.Checked
            If delete.Enabled = False Then
                If currentDroitAcces(63) Then
                    eProfit.ReadOnly = eType2.Checked
                Else
                    eProfit.ReadOnly = True
                End If
            End If
        Else
            anretour.ReadOnly = True
            moisretour.ReadOnly = True
            jourretour.ReadOnly = True
            eDuree.ReadOnly = True
            eDepot.ReadOnly = True
            ePret.ReadOnly = True
            eVerified.Enabled = False
            eRefunded.Enabled = False
            eReturned.Enabled = False
            eNotes.ReadOnly = True
            eProfit.ReadOnly = True
        End If

        If eType2.Checked = False Then
            eProfit.cb_AcceptNegative = False
            If eNom.SelectedItem Is Nothing Then
                eProfit.Text = 0
            Else
                eProfit.Text = CType(eNom.SelectedItem, Equipment).itemSoldAmount
            End If
            eProfit_TextChanged(sender, e)
        Else
            eProfit.cb_AcceptNegative = True
            eRefund_TextChanged(sender, e)
            updatePret = False
            dateRetour_SelectedIndexChanged(sender, e)
            updatePret = True
        End If
        folderEquipmentModified = True
    End Sub



    Private Sub dateRetour_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles anretour.TextChanged, jourretour.TextChanged, moisretour.TextChanged
        dateRetour_SelectedIndexChanged(sender, e)
        folderEquipmentModified = True
    End Sub

    Private Sub eDuree_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles eDuree.SelectedIndexChanged
        If eDuree.Text = "Date de retour invalide" Or eDuree.Text = "Plus d'un mois" Or eDuree.Text = "" Then Exit Sub
        Dim myDate As Date = Date.Today.AddMonths(1)
        Dim sDuree() As String = System.Text.RegularExpressions.Regex.Split(eDuree.Text, " et ")
        Dim nbDay As Short

        If eDuree.Text.IndexOf("mois") = -1 Then
            If sDuree.Length = 1 Then
                If sDuree(0).IndexOf("semaine") <> -1 Then
                    sDuree(0) = sDuree(0).Replace("semaine", "")
                    sDuree(0) = sDuree(0).Replace("s", "")
                    sDuree(0) = sDuree(0).Replace(" ", "")
                    nbDay = sDuree(0) * 7
                Else
                    sDuree(0) = sDuree(0).Replace("jour", "")
                    sDuree(0) = sDuree(0).Replace("s", "")
                    sDuree(0) = sDuree(0).Replace(" ", "")
                    nbDay = sDuree(0)
                End If
            Else
                sDuree(0) = sDuree(0).Replace("semaine", "")
                sDuree(0) = sDuree(0).Replace("s", "")
                sDuree(0) = sDuree(0).Replace(" ", "")
                sDuree(1) = sDuree(1).Replace("jour", "")
                sDuree(1) = sDuree(1).Replace("s", "")
                sDuree(1) = sDuree(1).Replace(" ", "")
                nbDay = sDuree(0) * 7 + sDuree(1)
            End If

            myDate = Date.Today
            myDate = myDate.AddDays(nbDay)
        End If

        anretour.SelectedIndex = anretour.FindStringExact(myDate.Year)
        moisretour.SelectedIndex = myDate.Month - 1
        jourretour.SelectedIndex = myDate.Day - 1
        folderEquipmentModified = True
    End Sub

    Private Sub dateRetour_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles anretour.SelectedIndexChanged, jourretour.SelectedIndexChanged, moisretour.SelectedIndexChanged
        If anretour.Text = "" Or moisretour.Text = "" Or jourretour.Text = "" Then eDuree.SelectedIndex = eDuree.Items.Count - 1 : Exit Sub

        Dim myDate As Date = Date.Today
        Dim retourDate As Date
        Try
            retourDate = CDate(anretour.Text & "/" & moisretour.Text & "/" & jourretour.Text)
        Catch
            Exit Sub
        End Try
        Dim DiffDate, NbWeek, nbDay As Short
        Dim dureeStr As String

        Try
            If date1Infdate2(retourDate, myDate, True) Then
                eDuree.SelectedIndex = eDuree.Items.Count - 1
            Else
                dureeStr = transformdiffDateInText(retourDate, myDate, DiffDate, NbWeek, nbDay)
                If dureeStr = "" Then
                    eDuree.SelectedIndex = eDuree.Items.Count - 1
                Else
                    If dureeStr <> "1 mois et plus" Then
                        eDuree.SelectedIndex = eDuree.FindStringExact(dureeStr)
                        If eDuree.SelectedIndex < 0 Then eDuree.SelectedIndex = eDuree.Items.Count - 1
                    Else
                        If Date.Equals(myDate.AddMonths(1), retourDate) Then
                            eDuree.SelectedIndex = eDuree.Items.Count - 3
                        Else
                            eDuree.SelectedIndex = eDuree.Items.Count - 2
                        End If
                    End If
                End If
            End If
        Catch
            eDuree.SelectedIndex = eDuree.Items.Count - 1
        End Try
Next1:
        If Not anretour.Text = "" And sender.name = "moisretour" Then
            adjustNbDays(anretour, moisretour, jourretour)
        End If

        If updatePret = False Then Exit Sub
        Try
            DiffDate = CDate(anretour.Text & "/" & moisretour.Text & "/" & jourretour.Text).Subtract(Date.Today).Days
            NbWeek = Math.Floor(DiffDate / 7)
            nbDay = DiffDate Mod 7
        Catch
            ePret.Text = 0
            eRefund_TextChanged(sender, e)
            Exit Sub
        End Try
        If eDuree.Text = "Date de retour invalide" Then
            ePret.Text = 0
        Else
            Dim curEquip As Equipment = eNom.SelectedItem
            If eType2.Checked Then 'If it's an item to borrow
                Dim curItemBorrowed As ItemBorrowed = curItem
                If curItemBorrowed IsNot Nothing Then
                    Select Case curItemBorrowed.costRepetitionBy
                        Case Equipment.costAmountFrequency.Day
                            ePret.Text = curItemBorrowed.costAmountBy * DiffDate
                        Case Equipment.costAmountFrequency.Week
                            If nbDay = 0 Then
                                ePret.Text = curItemBorrowed.costAmountBy * NbWeek
                            Else
                                ePret.Text = curItemBorrowed.costAmountBy * (NbWeek + 1)
                            End If
                        Case Else
                            ePret.Text = curItemBorrowed.costAmountBy
                    End Select
                ElseIf curEquip IsNot Nothing Then
                    Select Case curEquip.costRepetitionBy
                        Case Equipment.costAmountFrequency.Day
                            ePret.Text = curEquip.costAmountBy * DiffDate
                        Case Equipment.costAmountFrequency.Week
                            If nbDay = 0 Then
                                ePret.Text = curEquip.costAmountBy * NbWeek
                            Else
                                ePret.Text = curEquip.costAmountBy * (NbWeek + 1)
                            End If
                        Case Else
                            ePret.Text = curEquip.costAmountBy
                    End Select
                End If
            Else
                ePret.Text = 0
            End If
            
        End If
        eDepot_EPret_TextChanged(sender, e)
        eRefund_TextChanged(sender, e)
    End Sub

    Private Sub eProfit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles eProfit.TextChanged
        Dim curEquip As Equipment = eNom.SelectedItem
        If curEquip Is Nothing Then Exit Sub

        If eProfit.ReadOnly = False Then
            Dim amount As Double = eProfit.Text
            If curItem IsNot Nothing AndAlso curItem.curBill IsNot Nothing Then
                amount = calcAmountPlusTaxes(amount, curItem.curBill.taxe1, curItem.curBill.taxe2, curItem.curBill.taxesApplication)
            ElseIf curEquip.applyTax Then
                amount = calcAmountPlusTaxes(amount)
            End If

            eTotal.Text = roundAmount(amount)
        End If
        folderEquipmentModified = True
    End Sub

    Private Sub eRefund_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles eRefund.TextChanged
        Dim curEquip As Equipment = eNom.SelectedItem
        If eTotal.Text = "" OrElse eRefund.Text = "" OrElse curEquip Is Nothing Then Exit Sub

        eProfit.Text = CDbl(eDepot.Text) + CDbl(ePret.Text) - CDbl(eRefund.Text) : eProfit_TextChanged(sender, e)
        eProfit.forceManaging()

        Dim amount As Double = eProfit.Text
        If curEquip.applyTax Then
            If curItem IsNot Nothing AndAlso curItem.curBill IsNot Nothing Then
                amount = calcAmountPlusTaxes(amount, curItem.curBill.taxe1, curItem.curBill.taxe2, curItem.curBill.taxesApplication)
            Else
                amount = calcAmountPlusTaxes(amount)
            End If
        Else
            amount += If(eRefunded.Checked, 0, CDbl(eRefund.Text))
        End If

        eTotal.Text = roundAmount(amount)
        folderEquipmentModified = True
    End Sub

    Private Sub eDepot_EPret_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles eDepot.TextChanged, ePret.TextChanged
        Dim curEquip As Equipment = eNom.SelectedItem
        If curEquip Is Nothing Then Exit Sub

        Dim curRefund As Double = 0
        If curItem IsNot Nothing AndAlso TypeOf curItem Is ItemBorrowed Then
            Dim borrowedItem As ItemBorrowed = curItem
            curRefund = (CDbl(eDepot.Text) * borrowedItem.depositRefundPercentage + CDbl(ePret.Text) * borrowedItem.costRefundPercentage) / 100
        Else
            curRefund = (CDbl(eDepot.Text) * curEquip.depositRefundPercentage + CDbl(ePret.Text) * curEquip.costRefundPercentage) / 100
        End If
        curRefund = roundAmount(curRefund)

        eRefund.maximum = CDbl(ePret.Text) + CDbl(eDepot.Text)
        eRefund.Text = curRefund
        eRefund_TextChanged(Me, e)

        folderEquipmentModified = True
    End Sub

    Private Sub eNoItem_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles eNoItem.EnabledChanged
        If eNoItem.Enabled = False Then
            eNoItem.DropDownStyle = ComboBoxStyle.DropDown
            eNoItem.Text = OldItemText
        Else
            OldItemText = eNoItem.Text
            eNoItem.DropDownStyle = ComboBoxStyle.DropDownList
        End If
    End Sub
#End Region

#Region "External functions"
    Public Sub uncheckMenus()
        menuRefAucun.Checked = False
        menuRefAutre.Checked = False
        menuRefCompte.Checked = False
        menuRefKP.Checked = False
        Dim i As Short
        With menuPreRefList.MenuItems
            For i = 0 To .Count - 1
                .Item(i).Checked = False
            Next i
        End With
    End Sub

    Public Sub loadBills()
        'Droit & Accès
        If currentDroitAcces(14) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Comptabilité client." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim reactivateModif As Boolean = False
        Dim lastSelected As Integer = -1
        If facturesView.RowCount <> 0 AndAlso facturesView.currentRow IsNot Nothing Then lastSelected = facturesView.currentRow.Cells("NoFacture").Value
        If ToolTip1.GetToolTip(StartStopBillChanging).StartsWith("Commencer") = False Then
            startStopBillChanging_Click(Me, Nothing)
            reactivateModif = True
        End If

        Dim whereStr As String = "WHERE StatFactures.ParNoClient = " & Me.noClient

        'Date facture
        If DateDe.Text <> "" And DateA.Text <> "" And Me.DateAll.Checked = False Then
            whereStr &= " AND StatFactures.DateFacture >= '" & DateDe.Text & "' AND StatFactures.DateFacture <= '" & DateFormat.getTextDate(CDate(DateA.Text).AddDays(1)) & "'"
        End If

        Dim descending As String = "DESC"
        Dim sortingOrder As DBLinker.SortOrderType = DBLinker.SortOrderType.Descending
        If PreferencesManager.getGeneralPreferences()("TriFactures").StartsWith("A") Then descending = "" : sortingOrder = DBLinker.SortOrderType.Ascending

        If whereStr.StartsWith(" AND") Then whereStr = " WHERE " & whereStr.Substring(4)
        Dim factures As DataSet = DBLinker.getInstance.readDBForGrid("Statfactures LEFT OUTER JOIN                       InfoClients AS IC1 ON IC1.NoClient = Statfactures.NoClient LEFT OUTER JOIN                       KeyPeople AS KP1 ON KP1.NoKP = Statfactures.NoKp LEFT OUTER JOIN                       Utilisateurs AS U1 ON U1.NoUser = Statfactures.NoUser LEFT OUTER JOIN                       InfoClients AS IC2 ON IC2.NoClient = Statfactures.ParNoClient LEFT OUTER JOIN                       Utilisateurs AS U2 ON U2.NoUser = Statfactures.ParNoUser LEFT OUTER JOIN                       KeyPeople AS KP2 ON KP2.NoKP = Statfactures.ParNoKp", "DF, TF, MF , CASE WHEN MP IS NULL THEN 0 ELSE MP END AS MP,PC , PPO, PU, EL , NoFacture FROM (SELECT     MIN(Statfactures.DateFacture) AS DF, MIN(Statfactures.TypeFacture) AS TF, CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN SUM(Statfactures.MontantFacture) ELSE dbo.GetLinkedBillMF(Statfactures.NoFacture,0) END AS [MF], CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN (SELECT SUM(MontantPaiement) FROM StatPaiements WHERE StatPaiements.NoFacture = Statfactures.NoFacture) ELSE dbo.GetLinkedBillMP(Statfactures.NoFacture,0) END AS MP, CASE WHEN MIN(Statfactures.ParNoClient)=0 THEN 'Aucun' ELSE MIN(IC2.Nom) + ',' + MIN(IC2.Prenom) END AS PC, CASE WHEN MIN(Statfactures.ParNoKP)=0 THEN 'Aucun' ELSE MIN(KP2.Nom) END AS PPO, CASE WHEN MIN(Statfactures.ParNoUser)=0 THEN 'Aucun' ELSE MIN(U2.Nom) + ',' + MIN(U2.Prenom) END AS PU, CASE WHEN MIN(Statfactures.NoClient)=0 THEN CASE WHEN MIN(Statfactures.NoKP)=0 THEN CASE WHEN MIN(Statfactures.NoUserFacture)=0 THEN 'Aucun' ELSE MIN(U1.Nom) + ',' + MIN(U1.Prenom) END ELSE MIN(KP1.Nom) END ELSE MIN(IC1.Nom) + ',' + MIN(IC1.Prenom) END AS EL, Statfactures.NoFacture,MIN(Statfactures.NoFactureRef) AS FRef, (SELECT TOP 1 SF2.NoAction FROM Statfactures AS SF2 WHERE SF2.NoStat = MAX(Statfactures.NoStat)) AS Action, MIN(NoStat) as NoStat", whereStr & " GROUP BY Statfactures.NoFacture) AS Test WHERE Action<>20 ORDER BY DF DESC,NoStat DESC")
        facturesView.DataSource = factures.Tables(0)

        If factures.Tables(0).Rows.Count = 0 Then
            StartStopBillChanging.Enabled = False
        Else
            StartStopBillChanging.Enabled = True
        End If

        If reactivateModif = True And factures.Tables(0).Rows.Count > 0 Then startStopBillChanging_Click(Me, Nothing)

        If lastSelected <> -1 Then
            For i As Integer = 0 To facturesView.RowCount - 1
                If facturesView.Rows(i).Cells("NoFacture").Value = lastSelected Then
                    facturesView.currentCell = facturesView.Rows(i).Cells(0)
                    curFactureBox.loading(lastSelected)
                    Exit For
                End If
            Next i
        End If
    End Sub

    Public Sub loadFolderText(ByVal noFolder As Integer)
        'REM_CODES
        Dim lastTexteSel As Integer = folderTexts.SelectedIndex

        'Chargement des FolderTextes
        'Copy ceux courant, si en cours de modif
        Dim fTextes As New Generic.List(Of FolderText)
        If folderText.Editing Then
            For i As Integer = 0 To folderTexts.Items.Count - 1
                fTextes.Add(folderTexts.Items(i))
            Next i
        End If
        folderTexts.Items.Clear()
        Dim textesFolders As DataSet = DBLinker.getInstance.readDBForGrid("FolderTextes INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FolderTextes.NoFolderTexteType", "FolderTextes.*", "WHERE ((NoFolder)=" & noFolder & ") ORDER BY FTT.Position,DateStarted;")
        If textesFolders.Tables(0).Rows.Count <> 0 Then
            For i As Integer = 0 To textesFolders.Tables(0).Rows.Count - 1
                Dim curFT As New FolderText(textesFolders.Tables(0).Rows(i))
                Dim curFTT As FolderTextType = curFT.getFolderTexteType
                If curFT.dateStarted.Equals(LIMIT_DATE) OrElse date1Infdate2(curFT.dateStarted.AddDays(curFTT.nbDaysDiff * IIf(curFTT.isNbDaysDiffBefore, -1, 1)), Date.Today, True) = True Then
                    Dim added As Boolean = False
                    If folderText.Editing Then
                        For j As Integer = 0 To fTextes.Count - 1
                            If fTextes(j).noFolderTexte = curFT.noFolderTexte Then folderTexts.Items.Add(fTextes(j)) : added = True
                        Next j
                    End If

                    If added = False Then folderTexts.Items.Add(curFT)
                End If
            Next i
        End If

        'Sélectionne le bon texte
        If folderTexts.Items.Count <> 0 AndAlso (lastTexteSel < 0 OrElse lastTexteSel >= folderTexts.Items.Count) Then
            Dim texteToFind As String = PreferencesManager.getUserPreferences()("DossierTexteAutoSel")
            If texteToFind = "* Dernier accédé *" Then
                Dim setting As String = UsersManager.currentUser.settings.clientLastTabs
                Dim settings() As String = setting.Split(New Char() {"§"})
                If settings.Length >= 3 Then texteToFind = settings(2)
            End If
            folderTexts.SelectedIndex = folderTexts.findString(texteToFind, False, True)
            If folderTexts.SelectedIndex < 0 Then folderTexts.SelectedIndex = 0
        ElseIf folderTexts.Items.Count <> 0 Then
            folderTexts.SelectedIndex = lastTexteSel
        End If
        If Me.folderText.Editing = False Then
            If folderTexts.Items.Count = 0 Then folderText.setHtml("") 'Reset text when no foldertextes in list to ensure old text of another folder is not showed

            folderTextes_SelectedIndexChanged(Me, EventArgs.Empty)
            folderTextsModified = False
        End If
    End Sub

    Public Sub loadFolderInfos(ByVal noFolder As Integer)
        Dim myDate As Date

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim infoFolders(,) As String = DBLinker.getInstance.readDB("KeyPeople RIGHT OUTER JOIN Utilisateurs INNER JOIN FolderDates ON Utilisateurs.NoUser = FolderDates.NoTRPTraitant ON KeyPeople.NoKP = FolderDates.NoKP LEFT OUTER JOIN SiteLesion ON FolderDates.NoSiteLesion = SiteLesion.NoSiteLesion", _
                                                                   "FolderDates.Diagnostic, " & _
                                                                   "SiteLesion.SiteLesion, " & _
                                                                   "Utilisateurs.Nom + ',' + Utilisateurs.Prenom AS NomTRP, " & _
                                                                   "FolderDates.NoKP, " & _
                                                                   "FolderDates.NoCodeUnique, " & _
                                                                   "FolderDates.NoRef, " & _
                                                                   "FolderDates.DateRef, " & _
                                                                   "FolderDates.DateAccident, " & _
                                                                   "FolderDates.DateRechute, " & _
                                                                   "FolderDates.Duree, " & _
                                                                   "FolderDates.Frequence, " & _
                                                                   "FolderDates.NoTRPTraitant," & _
                                                                   "(SELECT     COUNT(NoVisite) AS Expr1 FROM InfoVisites WHERE      (NoFolder = FolderDates.NoFolder)) AS NbVisite, " & _
                                                                   "KeyPeople.Nom AS KPName, " & _
                                                                   "FolderDates.Flagged, " & _
                                                                   "FolderDates.Remarques, " & _
                                                                   "(SELECT     COUNT(NoVisite) AS Expr1 FROM          InfoVisites AS InfoVisites_2 WHERE      (NoFolder = FolderDates.NoFolder) AND (NoStatut = 4)) AS NbPresences, " & _
                                                                   "(SELECT     COUNT(NoVisite) AS Expr1 FROM          InfoVisites AS InfoVisites_1 WHERE      (NoFolder = FolderDates.NoFolder) AND (NoStatut < 3)) AS NbAbsences, " & _
                                                                   "FolderDates.FirstEval, " & _
                                                                   "FolderDates.FirstTraitement, " & _
                                                                   "FolderDates.CreationDate, " & _
                                                                   "FolderDates.ClosingDate, " & _
                                                                   "KeyPeople.NoRef AS Expr1, " & _
                                                                   "Utilisateurs.NoPermis, " & _
                                                                   "FolderDates.Service," & _
                                                                   "FolderDates.NoTRPDemande," & _
                                                                   "FolderDates.NoTRPToTransfer, " & _
                                                                   "FolderDates.NoCodeUser, " & _
                                                                   "FolderDates.NoCodeDate, " & _
                                                                   "FolderDates.DateReceptionRef", _
                                                                   "WHERE ((FolderDates.NoClient)=" & noClient & " AND (FolderDates.NoFolder)=" & noFolder & ");")
        If infoFolders Is Nothing OrElse infoFolders.Length = 0 Then Exit Sub

        ctlFolderInfos.diagnostic = infoFolders(0, 0)
        OldRegion = infoFolders(1, 0)
        ctlFolderInfos.siteLesion = infoFolders(1, 0)
        ctlFolderInfos.trpTraitant = infoFolders(2, 0) & " (" & infoFolders(11, 0) & ")"
        OldTherapeute = infoFolders(2, 0) & " (" & infoFolders(11, 0) & ")"
        Dim eTherapeute As Integer = etrp.FindStringExact(OldTherapeute)
        If eTherapeute <> -1 Then etrp.SelectedIndex = eTherapeute

        If infoFolders(3, 0) = "" OrElse infoFolders(3, 0) = 0 Then
            ctlFolderInfos.medecin = "Aucun médecin sélectionné"
            curDoctorNo = 0
        Else
            curDoctorNo = infoFolders(3, 0)
            ctlFolderInfos.medecin = infoFolders(13, 0)
        End If

        'Si le nombre de présence associé au dossier est égale à zéro, alors accepte de la possibilité de changer de code de dossier
        acceptFolderCodeModification = infoFolders(16, 0) = 0

        'REM_CODES
        Dim noCodeUser As Integer = If(infoFolders(27, 0) = "", 0, Integer.Parse(infoFolders(27, 0)))
        oldFolderCode = FolderCodesManager.getInstance.getItemable(Integer.Parse(infoFolders(4, 0)), noCodeUser, Date.Parse(infoFolders(28, 0)))
        ctlFolderInfos.codeDossier(noCodeUser) = oldFolderCode
        ctlFolderInfos.noRef = infoFolders(5, 0)
        If infoFolders(6, 0) = "" Then
            ctlFolderInfos.dateReference = ""
        Else
            myDate = infoFolders(6, 0)
            ctlFolderInfos.dateReference = myDate.Year & "/" & myDate.Month & "/" & myDate.Day
        End If

        If infoFolders(29, 0) = "" Then
            ctlFolderInfos.dateReferenceReception = ""
        Else
            myDate = infoFolders(29, 0)
            ctlFolderInfos.dateReferenceReception = myDate.Year & "/" & myDate.Month & "/" & myDate.Day
        End If

        If infoFolders(7, 0) = "" Then
            ctlFolderInfos.dateAccident = ""
        Else
            myDate = infoFolders(7, 0)
            ctlFolderInfos.dateAccident = myDate.Year & "/" & myDate.Month & "/" & myDate.Day
        End If

        If infoFolders(8, 0) = "" Then
            ctlFolderInfos.dateRechute = ""
        Else
            myDate = infoFolders(8, 0)
            ctlFolderInfos.dateRechute = myDate.Year & "/" & myDate.Month & "/" & myDate.Day
        End If
        ctlFolderInfos.duree = 0
        ctlFolderInfos.frequence = 0
        If infoFolders(9, 0) <> "" Then ctlFolderInfos.duree = infoFolders(9, 0)
        If infoFolders(10, 0) <> "" Then ctlFolderInfos.frequence = infoFolders(10, 0)
        If infoFolders(14, 0) <> "" Then
            menuDossierFlagged.Checked = infoFolders(14, 0)
        Else
            menuDossierFlagged.Checked = False
        End If
        ctlFolderInfos.remarques = infoFolders(15, 0).ToString.Replace("\n", vbCrLf)
        ctlFolderInfos.nbPresences = infoFolders(16, 0)
        ctlFolderInfos.nbAbsences = infoFolders(17, 0)
        If infoFolders(18, 0) <> "" Then ctlFolderInfos.dateEval = DateFormat.getTextDate(CDate(infoFolders(18, 0))) Else ctlFolderInfos.dateEval = ""
        If infoFolders(19, 0) <> "" Then ctlFolderInfos.dateDebut = DateFormat.getTextDate(CDate(infoFolders(19, 0))) Else ctlFolderInfos.dateDebut = ""
        If infoFolders(20, 0) <> "" Then ctlFolderInfos.dateAppel = DateFormat.getTextDate(CDate(infoFolders(20, 0))) Else ctlFolderInfos.dateAppel = ""
        If infoFolders(21, 0) <> "" Then ctlFolderInfos.dateFin = DateFormat.getTextDate(CDate(infoFolders(21, 0))) Else ctlFolderInfos.dateFin = ""
        ctlFolderInfos.noRefMedecin = infoFolders(22, 0)
        ctlFolderInfos.trpNoPermis = infoFolders(23, 0)
        ctlFolderInfos.service = infoFolders(24, 0)
        OldService = infoFolders(24, 0)
        ctlFolderInfos.trpDemande = IIf(infoFolders(25, 0) = "", 0, infoFolders(25, 0))
        ctlFolderInfos.trpToTransfer = IIf(infoFolders(26, 0) = "", 0, infoFolders(26, 0))
        ctlFolderInfos.noFolder = noFolder
        ctlFolderInfos.noClient = noClient

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        ctlFolderInfos.isFolderModified = False

        If ongletsdossier.SelectedIndex = 2 Then folderTexts.Enabled = True
    End Sub

    Public Sub loadFolder(ByVal noFolder As Integer, Optional ByVal loadOnlyTexts As Boolean = False)
        If ongletsdossier.SelectedIndex = 2 Then folderTexts.Enabled = False

        loadFolderInfos(noFolder)
        loadFolderText(noFolder)
        loadFolderEquipment()

        'Préf de sélection auto du même dossier ds l'onglet RV
        If PreferencesManager.getUserPreferences()("AutoSelectFolderInRV") = True Then dossiersVlist.SelectedIndex = dossier.SelectedIndex + 1

        If ongletsdossier.SelectedIndex = 2 Then folderTexts.Enabled = True
    End Sub

    Public Sub loadCommunications(Optional ByVal loadOnlyList As Boolean = False)
        isLoadOnlyList = loadOnlyList
        Dim CommDeAs(,), MyComms(,), Cats(), curCat As String
        Dim i, j, no As Integer
        Dim noCommSelected As Integer = 0
        If listeCommunications.selected <> -1 AndAlso listeCommunications.ItemValueA(listeCommunications.selected) IsNot Nothing Then noCommSelected = listeCommunications.ItemValueA(listeCommunications.selected)

        If loadOnlyList = False Then
            viderComm_Click(Me, EventArgs.Empty)

            'Load CommCategories
            commCategorie.Items.Clear()
            commFiltrageCat.Items.Clear()
            commFiltrageCat.Items.Add("* Toutes *")
            Dim categories(,) As String = DBLinker.getInstance.readDB("CommCategories", "Categorie,(SELECT TOP 1 NoClient FROM Communications WHERE CommCategories.NoCategorie=Communications.NoCategorie AND NoClient = " & Me.noClient & ")")
            If Not categories Is Nothing AndAlso categories.Length <> 0 Then
                For i = 0 To categories.GetUpperBound(1)
                    commCategorie.Items.Add(categories(0, i))
                    If categories(1, i) <> "" Then
                        Dim currentCategories() As String = categories(0, i).Split(":")
                        Dim currentCategorie As String = String.Empty
                        For Each currentSubCategorie As String In currentCategories
                            currentCategorie &= ":" & currentSubCategorie
                            If currentCategorie.StartsWith(":") Then currentCategorie = currentCategorie.Substring(1)
                            If commFiltrageCat.Items.IndexOf(currentCategorie) = -1 Then commFiltrageCat.Items.Add(currentCategorie)
                        Next
                    End If
                Next i
            End If
            commFiltrageCat.SelectedIndex = 0

            'Load CommDeA
            commFiltrage.Items.Clear()
            commDeA.Items.Clear()
            CommDeAs = DBLinker.getInstance.readDB("KPCategorie RIGHT JOIN (CommDeA LEFT JOIN KeyPeople ON CommDeA.NoKP = KeyPeople.NoKP) ON KPCategorie.NoCategorie = KeyPeople.NoCategorie", "Nom + '(' + CONVERT(nvarchar, CommDeA.NoKP) + ')', Categorie ", "WHERE (CommDeA.NoClient=" & noClient & ");")
            If Not CommDeAs Is Nothing AndAlso CommDeAs.Length <> 0 Then
                For i = 0 To CommDeAs.GetUpperBound(1)
                    commDeA.Items.Add(CommDeAs(0, i))

                    If CommDeAs(1, i) <> "" Then
                        Cats = CommDeAs(1, i).Split(New Char() {":"}) : curCat = ""
                        For j = 0 To Cats.Length - 1
                            curCat &= ":" & Cats(j)
                            If curCat.Substring(0, 1) = ":" Then curCat = curCat.Substring(1)
                            If commFiltrage.FindStringExact(curCat) < 0 Then commFiltrage.Items.Add(curCat)
                        Next j
                    End If
                Next i
            End If
            If commDeA.FindStringExact("* Client *") < 0 Then commDeA.Items.Add("* Client *")
            If commFiltrage.FindStringExact(" Tout afficher ") < 0 Then commFiltrage.Items.Add(" Tout afficher ")
            If commFiltrage.FindStringExact("* Client *") < 0 Then commFiltrage.Items.Add("* Client *")
            If commFiltrage.SelectedIndex < 0 Then commFiltrage.SelectedIndex = 0

            'Load subject
            commSujet.Items.Clear()
            Dim commObjects() As String = DBLinker.getInstance.readOneDBField("CommSubjects", "CommSubject")
            If commObjects IsNot Nothing Then commSujet.Items.AddRange(commObjects)
        End If

        listeCommunications.cls()

        MyComms = DBLinker.getInstance.readDB("KPCategorie RIGHT JOIN (((Communications LEFT JOIN CommSubjects ON Communications.NoCommSubject = CommSubjects.NoCommSubject) LEFT JOIN CommCategories ON CommCategories.NoCategorie=Communications.NoCategorie) LEFT JOIN KeyPeople ON Communications.NoKP = KeyPeople.NoKP) ON KPCategorie.NoCategorie = KeyPeople.NoCategorie", "KPCategorie.Categorie, Communications.NoCommunication,Communications.NoClient,Communications.NoKP,Communications.IsEnvoie,CommSubject,Communications.CommDate,Communications.NoUser,Communications.Remarques,Communications.NameOfFile,Communications.NoFolder,Communications.NoCategorie,CommCategories.Categorie", "WHERE (Communications.NoClient=" & noClient & ");")
        If Not MyComms Is Nothing AndAlso MyComms.Length <> 0 Then
            For i = 0 To MyComms.GetUpperBound(1)
                If MyComms(3, i) = "" Then MyComms(3, i) = 0 'Change null value to 0

                Dim acceptedComm As Boolean = False
                If commFiltrage.Text <> " Tout afficher " Then
                    If commFiltrage.Text.StartsWith("* Client *") Then
                        If MyComms(3, i) = 0 Then acceptedComm = True
                    Else
                        If MyComms(3, i) <> 0 And MyComms(0, i).StartsWith(commFiltrage.Text) = True Then acceptedComm = True
                    End If
                Else
                    acceptedComm = True
                End If
                If acceptedComm AndAlso commFiltrageCat.SelectedIndex <> 0 Then
                    If Not (MyComms(12, i) = commFiltrageCat.Text OrElse MyComms(12, i).StartsWith(commFiltrageCat.Text & ":")) Then acceptedComm = False
                End If

                If acceptedComm = True Then
                    If (MyComms(4, i) = True And commEnvoie.Checked = True) Or (MyComms(4, i) = False And commReception.Checked = True) Then
                        no = listeCommunications.add("(" & DateFormat.getTextDate(CDate(MyComms(6, i))) & ") " & IIf(MyComms(12, i) = "", "* Aucune catégorie *", MyComms(12, i)) & " : " & MyComms(5, i))
                        listeCommunications.ItemValueA(no) = MyComms(1, i)
                        For j = 0 To MyComms.GetUpperBound(0)
                            listeCommunications.ItemValueB(no) &= "§" & MyComms(j, i)
                        Next j
                        listeCommunications.ItemValueB(no) = CStr(listeCommunications.ItemValueB(no)).Substring(1)
                        If MyComms(9, i) <> "" Then listeCommunications.ItemIconsShowed(no, 0) = True
                        If MyComms(4, i) = True Then
                            listeCommunications.ItemBackColor(no) = colorSent.BackColor
                        Else
                            listeCommunications.ItemBackColor(no) = colorReceived.BackColor
                        End If
                    End If
                End If
            Next i
        End If
        listeCommunications.draw = True : listeCommunications.draw = False
        listeCommunications.selected = listeCommunications.findStringExact(noCommSelected, , CI.Controls.List.FindingType.ValueA)
        If listeCommunications.selected <> -1 AndAlso listeCommunications.itemVisible(listeCommunications.selected) <> CI.Controls.List.ItemVisibility.Fully Then listeCommunications.showItem(listeCommunications.selected)
        commModified = False

        isLoadOnlyList = False
    End Sub

    Public Sub loadFolderEquipment()
        Dim lastSelected As String = ""
        If listEquipement.currentRow IsNot Nothing AndAlso listEquipement.currentRow.DataGridView IsNot Nothing Then lastSelected = listEquipement.currentRow.Cells("Type").Value.ToString.Substring(0, 1) & listEquipement.currentRow.Cells("Numéro de l'item").Value

        resetFolderEquipmentFields()
        If allowFolderEquipmentModification = False Then lockEquipment(True)

        dsVentePret.Clear()

        loadFolderEquipmentList(currentNoFolder)

        listEquipement.Tag = Nothing

        'Resélectionne celui qui était sélectionné
        listEquipement.ClearSelection()
        If lastSelected <> "" AndAlso dsVentePret.Tables(0).Rows.Count <> 0 Then
            For Each curRow As DataGridViewRow In listEquipement.Rows
                If curRow.Cells("Numéro de l'item").Value = lastSelected.Substring(1) AndAlso curRow.Cells("Type").Value.ToString.Substring(0, 1) = lastSelected.Substring(0, 1) Then
                    curRow.Selected = True
                    listEquipement.currentCell = curRow.Cells(0)
                Else
                    curRow.Selected = False
                End If
            Next
            selectingEquipment()
        End If

        folderEquipmentModified = False
    End Sub

    Private foldersActive As New Generic.Dictionary(Of Integer, Boolean)

    Public Sub loadFolders()
        Dim NoDossiers, i, CurDossier, CurDossierV, curDossierC As Integer
        Dim statut As String
        CurDossier = -1 : CurDossierV = 0 : curDossierC = 0
        If dossier.Items.Count > 0 Then CurDossier = dossier.SelectedIndex
        If dossiersVlist.Items.Count > 0 Then CurDossierV = dossiersVlist.SelectedIndex
        If dossiersClist.Items.Count > 0 Then curDossierC = dossiersClist.SelectedIndex

        NoDossiers = 0 : dossier.Items.Clear() : dossiersVlist.Items.Clear() : dossiersClist.Items.Clear()
        Dim results(,) As String = DBLinker.getInstance.readDB("SiteLesion RIGHT OUTER JOIN                       InfoFolders ON SiteLesion.NoSiteLesion = InfoFolders.NoSiteLesion INNER JOIN Utilisateurs ON Utilisateurs.Nouser=InfoFolders.NoTRPTraitant", "SiteLesion.SiteLesion, InfoFolders.StatutOuvert, InfoFolders.NoCodeUnique, InfoFolders.NoFolder,Utilisateurs.Nom + ',' + Utilisateurs.Prenom + ' (' + CAST(Utilisateurs.NoUser AS VARCHAR(MAX)) + ')' AS TRP, InfoFolders.Service", "WHERE ((InfoFolders.NoClient)=" & noClient & ");")
        If Not results Is Nothing AndAlso results.Length <> 0 Then NoDossiers = results.GetUpperBound(1) + 1

        dossiersVlist.Items.Add("* Tous les dossiers *")
        dossiersClist.Items.Add("* Aucun dossier *")
        If NoDossiers = 0 Then
            resetFolder() 'Ensure last selected folder is cleaned out.

            Me.modifsaveTextes.Enabled = False
            Me.modifsaveDossierInfos.Enabled = False
            Me.modifsaveEquip.Enabled = False
            Exit Sub
        Else
            Me.modifsaveDossierInfos.Enabled = True
            Me.modifsaveEquip.Enabled = True
            If ongletsdossier.SelectedIndex = 2 Then Me.maximise1.Enabled = True
        End If

        'Creation of the lists + variable containing folders data
        'TODO: foldersActive should remove this variable to put Clients.Folder directly into the lists instead of strings
        foldersActive.Clear()
        For i = 0 To NoDossiers - 1
            foldersActive.Add(results(3, i), results(1, i)) 'Will be changed when folder will be changed in object
            If results(1, i) = True Then
                statut = "Actif"
            Else
                statut = "Inactif"
            End If
            'REM_CODES
            Dim myCodeName As String = FolderCodesManager.getInstance.getCodeNameByNoUnique(results(2, i))
            dossier.Items.Add(results(3, i) & " - " & results(0, i) & " - " & results(5, i) & " - " & results(4, i) & " (" & myCodeName & ") (" & statut & ")")
            dossiersVlist.Items.Add(results(3, i) & " - " & results(0, i) & " - " & results(5, i) & " - " & results(4, i) & " (" & myCodeName & ") (" & statut & ")")
            dossiersClist.Items.Add(results(3, i) & " - " & results(0, i) & " - " & results(5, i) & " - " & results(4, i) & " (" & myCodeName & ") (" & statut & ")")
        Next i

        'Reselection of the folder in rendez-vous section selected before loading
        If CurDossierV > 0 Then
            Try
                If CurDossierV < dossiersVlist.Items.Count Then dossiersVlist.SelectedIndex = CurDossierV Else dossiersVlist.SelectedIndex = dossiersVlist.Items.Count - 1
            Catch
                dossiersVlist.SelectedIndex = dossiersVlist.Items.Count - 1
            End Try
        Else
            dossiersVlist.SelectedIndex = dossiersVlist.Items.Count - 1
        End If

        'Reselection of the folder selected in communications section before loading
        Dim lastCommModified As Boolean = commModified
        If curDossierC > 0 Then
            Try
                dossiersClist.SelectedIndex = curDossierC
            Catch
                dossiersClist.SelectedIndex = 0
            End Try
        Else
            dossiersClist.SelectedIndex = 0
        End If
        commModified = lastCommModified

        'Reselection of the folder selected before loading
        If CurDossier >= 0 Then
            If ToolTip1.GetToolTip(modifsaveTextes).StartsWith("Modifier") = False Then loadingDossier = False
            If dossier.SelectedIndex <> CurDossier Then
                If CurDossier < dossier.Items.Count Then dossier.SelectedIndex = CurDossier Else dossier.SelectedIndex = dossier.Items.Count - 1
            Else
                dossier_SelectedIndexChanged(Me, EventArgs.Empty)
            End If
            loadingDossier = True
        Else
            dossier.SelectedIndex = dossier.Items.Count - 1
        End If
    End Sub

    Public Sub loadRendezVous()
        If dossier.Items.Count = 0 Then Exit Sub
        Dim selectedRV As RendezVous = visitesList.ItemValueA(visitesList.selected)
        Dim sNoFolder() As String
        lastVisiteSelected = -1

        visitesList.cls()
        Dim rvs As Generic.List(Of RendezVous)
        If dossiersVlist.SelectedIndex = 0 Then
            rvs = RendezVousManager.getInstance.loadRendezVous(Me.noClient, firstUsageDate)
        Else
            sNoFolder = dossiersVlist.Text.Split(New Char() {"-"})
            rvs = RendezVousManager.getInstance.loadRendezVous(Me.noClient, firstUsageDate, , , sNoFolder(0))
        End If

        loadRendezVous(rvs)

        visitesList.draw = True : visitesList.draw = False
        visitesList.selected = visitesList.findValue(selectedRV)
        If visitesList.selected <> -1 Then manageRendezVousContextMenu(visitesList.selected)
        visitesList.showItem(visitesList.selected)
    End Sub

    Public Sub loadGeneralInfo()
        Dim i As Short
        Dim referer() As String
        Dim birthDate As Date

        Dim loadingTime As Long = Date.Now.Ticks

        Dim genInfo(,) As String = DBLinker.getInstance.readDB("Villes RIGHT JOIN (Metiers RIGHT JOIN (Employeurs RIGHT JOIN InfoClients ON Employeurs.NoEmployeur = InfoClients.NoEmployeur) ON Metiers.NoMetier = InfoClients.NoMetier) ON Villes.NoVille = InfoClients.NoVille", "InfoClients.Nom, InfoClients.Prenom, InfoClients.Adresse, Villes.NomVille, InfoClients.CodePostal, InfoClients.Telephones, InfoClients.AutreNom, InfoClients.DateNaissance, InfoClients.SexeHomme, Employeurs.Employeur, Metiers.Metier, InfoClients.NAM, InfoClients.NomReferent, InfoClients.Courriel, InfoClients.URL, InfoClients.Photo, InfoClients.Description, InfoClients.Publipostage", "WHERE ((InfoClients.NoClient)=" & noClient & ");", , , True)
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading GenInfo - DB request : " & (Date.Now.Ticks - loadingTime) / 10000000 : loadingTime = Now.Ticks

        If genInfo Is Nothing OrElse genInfo.Length = 0 Then MessageBox.Show("Le compte n'existe plus. Veuillez réessayer de l'ouvrir.", "Compte inexistant") : Me.Close() : Exit Sub

        nom.Text = genInfo(0, 0)
        prenom.Text = genInfo(1, 0)
        adresse.Text = genInfo(2, 0)
        ville.Text = genInfo(3, 0)
        codepostal1.Text = Microsoft.VisualBasic.Left(genInfo(4, 0), 3)
        codepostal2.Text = Microsoft.VisualBasic.Right(genInfo(4, 0), 3)
        If genInfo(5, 0) <> "" Then
            Telephones.Items.AddRange(genInfo(5, 0).ToString.Split(New Char() {"§"}))
            Telephones.SelectedIndex = 0
        End If

        ' Speed problem between
        autrenom.Text = genInfo(6, 0)
        If genInfo(7, 0) <> String.Empty Then
            birthDate = genInfo(7, 0)
            annee.Text = birthDate.Year
            mois.Text = addZeros(birthDate.Month, 2)
            jour.Text = addZeros(birthDate.Day, 2)
        End If
        If genInfo(8, 0) = True Then
            sexe(0).checked = True
        Else
            sexe(1).checked = True
        End If
        employeurslist.Text = genInfo(9, 0)
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading GenInfo - infos half way : " & (Date.Now.Ticks - loadingTime) / 10000000 : loadingTime = Now.Ticks
        metierslist.Text = genInfo(10, 0)
        nam.Text = genInfo(11, 0)
        ' Speed problem between

        If genInfo(12, 0) <> "" Then
            referer = genInfo(12, 0).Split(New Char() {"§"})
            reference.Tag = referer(0)
            menuRefAucun.Checked = False
            Select Case referer(0).ToLower
                Case "autre"
                    menuRefAutre.Checked = True
                Case "compte"
                    menuRefCompte.Checked = True
                Case "kp"
                    menuRefKP.Checked = True
                Case "list"
                    With menuPreRefList.MenuItems
                        For i = 0 To .Count - 1
                            If .Item(i).Text = referer(1) Then .Item(i).Checked = True : Exit For
                        Next i
                    End With
            End Select
            reference.Text = referer(1).Replace("<br>", vbCrLf)
        End If
        courriel.manageText = genInfo(13, 0) IsNot Nothing
        courriel.Text = If(genInfo(13, 0) Is Nothing, email_field_not_filled, genInfo(13, 0))
        url.manageText = genInfo(14, 0) IsNot Nothing
        url.Text = If(genInfo(14, 0) Is Nothing, url_field_not_filled, genInfo(14, 0))
        'Photo.Image = GenInfo(15, 0)
        If genInfo(16, 0) IsNot Nothing Then remarques.Text = genInfo(16, 0).ToString.Replace("\n", vbCrLf)
        publipostage.SelectedIndex = genInfo(17, 0)

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading GenInfo - Set infos into form : " & (Date.Now.Ticks - loadingTime) / 10000000 : loadingTime = Now.Ticks

        'Photo du client
        If Dir(appPath & bar(appPath) & "Clients\" & noClient & "\photo") <> "" Then myPhoto = New Bitmap(appPath & bar(appPath) & "Clients\" & noClient & "\photo") : photo.Image = myPhoto

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading GenInfo - Set photo : " & (Date.Now.Ticks - loadingTime) / 10000000 : loadingTime = Now.Ticks

        Me.btnSendEmail.Enabled = Me.courriel.Text <> String.Empty
        accountInfosModified = False
    End Sub

    Public Sub loadClientHistory()
        Dim contenu() As String = DBLinker.getInstance.readOneDBField("ClientsAntecedents", "Antecedents", "WHERE NoClient=" & Me.noClient)
        If contenu IsNot Nothing AndAlso contenu.Length <> 0 Then antecedants.setHtml("<HTML><BODY>" & contenu(0) & "</BODY></HTML>")
    End Sub

    Public Sub loadFolderEquipmentInfos()
        Dim curSelected As Equipment = eNom.SelectedItem
        Dim curNoItem As String = eNoItem.Text
        'Load Equipement list
        eNom.Items.Clear()
        Dim equipements As Generic.List(Of Equipment) = EquipmentsManager.getInstance.getItemables()
        For i As Integer = 0 To equipements.Count - 1
            Dim isNotPret As Boolean = (eType1.Checked = True AndAlso equipements(i).type <> Equipment.EquipmentType.Pret)
            Dim isNotVente As Boolean = (eType2.Checked = True AndAlso equipements(i).type <> Equipment.EquipmentType.Vente)
            Dim nothingSelected As Boolean = eType2.Checked = False AndAlso eType1.Checked = False

            If (isNotPret OrElse isNotVente OrElse nothingSelected) AndAlso equipements(i).type <> Equipment.EquipmentType.Inventaire Then eNom.Items.Add(equipements(i))
        Next i

        If curSelected IsNot Nothing AndAlso eNom.Items.Contains(curSelected) Then
            eNom.SelectedItem = curSelected
            loadFolderEquipmentNoItems()
        End If
        If curNoItem <> "" Then eNoItem.SelectedIndex = eNoItem.FindStringExact(curNoItem)
    End Sub

    Private Sub loadFolderEquipmentList(ByVal noFolder As Integer)
        Dim sortingColumn As String = ""
        If listEquipement.SortedColumn IsNot Nothing Then sortingColumn = listEquipement.SortedColumn.Name
        Dim sortingOrder As SortOrder = listEquipement.SortOrder

        dsVentePret = DBLinker.getInstance.readDBForGrid("SELECT     Prets.DateHeure AS Date, Equipements.NomItem + ' (' + CONVERT(nvarchar, Prets.NoEquipement) + ') - ' + CONVERT(nvarchar, Prets.NoItem) AS Item,                        Utilisateurs.Nom + ',' + Utilisateurs.Prenom + ' (' + CONVERT(nvarchar, Prets.NoTRP) + ')' AS Thérapeute, Prets.NoFacture AS Facture,                        Prets.NoPret AS [Numéro de l'item], 'Prêt' AS Type, DateHeure,Prets.NoEquipement,Prets.NoClient,Prets.NoFolder,Prets.NoTRP,Prets.NoFacture,MontantProfit,NoItem, Prets.DateRetour, Prets.Depot, Prets.CoutPret, Prets.VerifiedByTRP, Prets.Rembourse,                        Prets.Retourne, Prets.Remarques,Prets.MontantRembourse,NoPret as NoItemPV, Prets.CostRefundPercentage, Prets.DepositRefundPercentage, Prets.CostAmountBy, Prets.CostRepetitionBy " & _
                                                         " FROM         Equipements INNER JOIN                       Utilisateurs INNER JOIN                       Prets ON Utilisateurs.NoUser = Prets.NoTRP ON Equipements.NoEquipement = Prets.NoEquipement WHERE     (Prets.NoFolder = " & noFolder & ") UNION ALL " & _
                                                         "SELECT     Ventes.DateHeure AS Date, Equipements.NomItem + ' (' + CONVERT(nvarchar, Ventes.NoEquipement) + ') - ' + Ventes.NoItem AS Item,                        Utilisateurs.Nom + ',' + Utilisateurs.Prenom + ' (' + CONVERT(nvarchar, Ventes.NoTRP) + ')' AS Thérapeute, Ventes.NoFacture AS Facture,                        Ventes.NoVente AS [Numéro de l'item], 'Vente' AS Type,DateHeure,Ventes.NoEquipement,Ventes.NoClient,Ventes.NoFolder,Ventes.NoTRP,Ventes.NoFacture,MontantProfit,NoItem, '' AS DateRetour, 0 AS Depot, 0 AS CoutPret, 0 AS VerifiedByTRP, 0 AS Rembourse,                        0 AS Retourne, '' AS Remarques,0,NoVente as NoItemPV, 0 AS CostRefundPercentage, 0 AS DepositRefundPercentage, 0 AS CostAmountBy, 1 AS CostRepetitionBy " & _
                                                         " FROM         Utilisateurs INNER JOIN                       Ventes INNER JOIN                       Equipements ON Ventes.NoEquipement = Equipements.NoEquipement ON Utilisateurs.NoUser = Ventes.NoTRP WHERE     (Ventes.NoFolder = " & noFolder & ")", , , "DataTable1")
        Dim primaryKeyCols(1) As DataColumn
        Dim i As Byte
        With dsVentePret.Tables("DataTable1")
            primaryKeyCols(0) = .Columns("Type")
            primaryKeyCols(1) = .Columns("Numéro de l'item")
            .PrimaryKey = primaryKeyCols
            For i = 6 To .Columns.Count - 1
                .Columns(i).ColumnMapping = MappingType.Hidden
            Next i
        End With
        listEquipement.DataSource = dsVentePret.Tables("DataTable1").DefaultView

        If sortingColumn <> "" Then listEquipement.Sort(listEquipement.Columns(sortingColumn), IIf(sortingOrder = SortOrder.Descending, 1, 0))
    End Sub

    Public Sub loading()
        Dim loadingTime As Double = DateTime.Now.Ticks

        '
        'Préparation de la fenêtre Compte Client
        '
        Dim lastOnglets() As String, SelCompteOnglet As String = "", SelDossierOnglet As String = "", SelFolderTextes As String = ""
        Dim i As Short
        saveantecedent.Image = imgModifSave.Images(0)
        Dim selfOpened As Boolean = False

        'Configuration des listes
        configList(visitesList)
        configList(listeCommunications)
        With visitesList
            .hScrollForeColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors2"))
            .vScrollForeColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors2"))
            .hScrollColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors3"))
            .vScrollColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors3"))
            .borderSelColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors4"))
            .borderColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors5"))
            .baseForeColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors6"))
        End With

        If PreferencesManager.getGeneralPreferences()("TriRVCompte").StartsWith("A") Then
            visitesList.reverseSorting = False
        Else
            visitesList.reverseSorting = True
        End If

        'Load Référents prédéterminés
        Dim referents As String = PreferencesManager.getGeneralPreferences()("PreReferents")
        If referents <> "" Then
            Dim refs() As String = referents.Split(New Char() {vbTab})
            With menuPreRefList.MenuItems
                For i = 0 To refs.Length - 1
                    Dim myMenuItem As New MenuItem(refs(i), New EventHandler(AddressOf menuPreRefList_Click))
                    .Add(myMenuItem)
                Next i
            End With
        Else
            menuPreRefList.Visible = False
        End If

        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        'Load Employeurs
        employeurslist.Items.Clear()
        Dim employeurs() As String = DBLinker.getInstance.readOneDBField("Employeurs", "Employeur", , True)
        If Not employeurs Is Nothing AndAlso employeurs.Length <> 0 Then employeurslist.Items.AddRange(employeurs)

        'Load Métiers
        metierslist.Items.Clear()
        Dim metiers() As String = DBLinker.getInstance.readOneDBField("Metiers", "Metier", , True)
        If Not metiers Is Nothing AndAlso metiers.Length <> 0 Then metierslist.Items.AddRange(metiers)

        'Load Villes
        ville.Items.Clear()
        Dim villes() As String = DBLinker.getInstance.readOneDBField("Villes", "NomVille", , True)
        If Not villes Is Nothing AndAlso villes.Length <> 0 Then ville.Items.AddRange(villes)

        'Load TRP
        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(, True)
        If users.Count <> 0 Then
            etrp.Items.AddRange(users.ToArray)
            etrp.SelectedIndex = 0
        End If

        loadFolderEquipment()
        applyFolderEquipmentListSettings()

        'Ensure everything in Folder is disabled
        eType_CheckedChanged(Me, EventArgs.Empty)
        lockCommunications(True)
        lockEquipment(True)
        lockFolderInfos(True)
        lockFolderText(True)
        lockGeneralInfos(True)

        'Ajustement des cases pour la date de retour d'un équipement
        For i = firstUsageDate.Year To Date.Today.Year
            anretour.Items.Add(i)
        Next i
        anretour.SelectedIndex = anretour.Items.Count - 1
        moisretour.SelectedIndex = Date.Today.Month - 1
        jourretour.SelectedIndex = Date.Today.Day - 1
        adjustNbDays(anretour, moisretour, jourretour)

        'Vérification de l'existance du compte client
        Dim isExists() As String = DBLinker.getInstance.readOneDBField("InfoClients", "NoClient", "WHERE ((NoClient)=" & noClient & ");")
        If isExists Is Nothing OrElse isExists.Length = 0 Then
            MessageBox.Show("Compte client inexistant", "Compte invalide")
            accountInfosModified = False
            folderEquipmentModified = False
            commModified = False
            antecedentModified = False
            Me.Close()
            If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            Exit Sub
        End If

        '
        'Chargement des informations de base du compte client + Définition du titre de la fenêtre
        '
        For i = 1900 To Date.Today.Year
            annee.Items.Add(i)
        Next i
        Dim loadingTime2 As Long = Date.Now.Ticks
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading upto Prepare Win : " & (Date.Now.Ticks - loadingTime) / 10000000
        loadGeneralInfo()
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading upto GenInfo : " & (Date.Now.Ticks - loadingTime) / 10000000 & " (Only GenInfo : " & (Now.Ticks - loadingTime2) / 10000000 & ")" : loadingTime2 = Now.Ticks

        Me.Text = "Client " & Me.noClient & " : " & nom.Text & "," & prenom.Text & " : " & nam.Text

        'Chargement des dossiers & des visites
        loadFolders()
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading upto Dossiers : " & (Date.Now.Ticks - loadingTime) / 10000000 & " (Only Dossiers : " & (Now.Ticks - loadingTime2) / 10000000 & ")" : loadingTime2 = Now.Ticks

        'Chargement du bilan de santé (anciennement antécédents)
        loadClientHistory()
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading upto Antécédents : " & (Date.Now.Ticks - loadingTime) / 10000000 & " (Only Antécédents : " & (Now.Ticks - loadingTime2) / 10000000 & ")" : loadingTime2 = Now.Ticks

        'Activation/Désactivation
        paiements.Enabled = Bill.isPaymentsToDoByClient(noClient)
        Dim isKP() As String = DBLinker.getInstance.readOneDBField("KeyPeople", "NoKP", "WHERE (Nom)='" & nom.Text.Replace("'", "''") & "," & prenom.Text.Replace("'", "''") & "';")
        If Not isKP Is Nothing AndAlso isKP.Length <> 0 Then btnAddAsKP.Enabled = False

        'Chargement des communications
        colorSent.BackColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorCommSent"))
        colorReceived.BackColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorCommReceived"))
        loadCommunications()
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "CC loading upto Comms : " & (Date.Now.Ticks - loadingTime) / 10000000 & " (Only Comms : " & (Now.Ticks - loadingTime2) / 10000000 & ")" : loadingTime2 = Now.Ticks

        'Sélection des onglets
        Dim setting As String = UsersManager.currentUser.settings.clientLastTabs
        If setting <> "" Then
            lastOnglets = setting.Split(New Char() {"§"})
            If PreferencesManager.getUserPreferences()("DossierTexteAutoSel").StartsWith("*") AndAlso lastOnglets.GetUpperBound(0) >= 2 Then
                SelFolderTextes = lastOnglets(2)
            Else
                SelFolderTextes = PreferencesManager.getUserPreferences()("DossierTexteAutoSel")
            End If
            If PreferencesManager.getUserPreferences()("DossierTabAutoSelect").StartsWith("*") AndAlso lastOnglets.GetUpperBound(0) >= 1 Then
                SelDossierOnglet = lastOnglets(1)
            Else
                SelDossierOnglet = PreferencesManager.getUserPreferences()("DossierTabAutoSelect")
            End If
            If PreferencesManager.getUserPreferences()("ClientTabAutoSelect").StartsWith("*") AndAlso lastOnglets.GetUpperBound(0) >= 0 Then
                SelCompteOnglet = lastOnglets(0)
            Else
                SelCompteOnglet = PreferencesManager.getUserPreferences()("ClientTabAutoSelect")
            End If
        Else
            SelCompteOnglet = PreferencesManager.getUserPreferences()("ClientTabAutoSelect")
            SelDossierOnglet = PreferencesManager.getUserPreferences()("DossierTabAutoSelect")
            SelFolderTextes = PreferencesManager.getUserPreferences()("DossierTexteAutoSel")
        End If

        'REM_CODES
        If SelFolderTextes <> "" Then
            For i = folderTexts.Items.Count - 1 To 0 Step -1
                If CType(folderTexts.Items(i), FolderText).getFolderTexteType().textTitle.StartsWith(SelFolderTextes) Then folderTexts.SelectedIndex = i : Exit For
            Next i
        End If
        If folderTexts.SelectedIndex = -1 AndAlso folderTexts.Items.Count <> 0 Then folderTexts.SelectedIndex = 0

        If SelDossierOnglet <> "" Then
            Select Case SelDossierOnglet.Substring(0, 2).ToUpper
                Case "TE"
                    ongletsdossier.SelectedIndex = 2
                    folderTexts.Enabled = True
                Case "IN"
                    ongletsdossier.SelectedIndex = 1
                Case "ÉQ"
                    ongletsdossier.SelectedIndex = 0
            End Select
        End If

        If SelCompteOnglet <> "" Then
            For i = 0 To ongletsclient.TabPages.Count - 1
                If ongletsclient.TabPages(i).Text = SelCompteOnglet Then ongletsclient.SelectedIndex = i : Exit For
            Next i
        End If

        ongletsclient_SelectedIndexChanged(Me, New EventArgs())
        ongletsdossier_SelectedIndexChanged(Me, New EventArgs())

        'S'assure que le boutton de modification des onglets Communications et Comptabilité sont visibles si un de ces onglets est sélectionnés
        If ongletsclient.SelectedIndex = 1 Then StartStopCommChanging.Visible = True
        If ongletsclient.SelectedIndex = 2 Then StartStopBillChanging.Visible = True


        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        accountInfosModified = False
        antecedentModified = False
        ctlFolderInfos.isFolderModified = False
        commModified = False
        folderEquipmentModified = False
        loadingCompte = False

        If currentUserName = "Administrateur" Then
            loadingTime = DateTime.Now.Ticks - loadingTime
            loadingTime /= 10000000 'To get in seconds
            AdminTimer.Text = "Time to load->" & loadingTime
        End If
    End Sub

    Private Function saveFolder() As String
        If folderEquipmentModified = True Then If saveEquipment() = "" Then Return "Not saved"
        If ctlFolderInfos.isFolderModified = True Then If saveFolderInfos() = "" Then Return "Not saved"
        If folderTextsModified Then If saveFolderTexts() = "" Then Return "Not saved"

        Return "Saved"
    End Function

    Public Function saveForm() As String
        If accountInfosModified = True Then If saveGeneralInfo() = "" Then Return "Not saved"
        If commModified = True And (modifComm.Enabled = True Or btnAddComm.Enabled = True) Then If saveCommunications() = "" Then Return "Not saved"
        If antecedentModified = True Then If saveClientHistory() = "" Then Return "Not saved"

        Return saveFolder()
    End Function

    Private Sub saveAll(ByVal sender As Object, ByVal e As EventArgs)
        saveForm()
    End Sub
#End Region

#Region "Internal functions"
    Private Sub applyFolderEquipmentListSettings()
        Dim setting As String = UsersManager.currentUser.settings.accountEquipmentStyle
        If setting <> "" AndAlso Not dsVentePret.Tables("DataTable1") Is Nothing Then
            Dim n As Byte = 0
            Dim sorting() As String = thisSettings(6).Split(New Char() {" "})
            Dim sortingColumn As DataGridViewColumn = Nothing
            For Each curColumn As DataGridViewColumn In listEquipement.Columns
                curColumn.Width = thisSettings(n)
                If curColumn.Name = sorting(0) Then sortingColumn = curColumn
                n += 1
            Next

            If thisSettings(6) <> "" AndAlso sortingColumn IsNot Nothing Then listEquipement.Sort(sortingColumn, IIf(sorting(1) = 2, 1, 0))
        End If
    End Sub

    Private Function isCommunicationInUse() As Boolean
        If dossier.SelectedIndex = -1 Then Return False
        If listeCommunications.selected = -1 Then Return False

        Dim myComm() As String = CStr(listeCommunications.ItemValueB(listeCommunications.selected)).Split(New Char() {"§"})
        If myComm(9) <> "" And myComm(9).StartsWith("FILE|") Then
            Dim oldFileName As String = myComm(9).Substring(5)
            Dim myPath As String = appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & oldFileName
            Return Fichiers.fileInUse(myPath)
        End If

        Return False
    End Function


    Private Sub importCommunication()
        If dossier.SelectedIndex = -1 Then Exit Sub

        Try
            Dim ImportFile, MyPath, NewFileName, OldFileName, MyComm(), sfn() As String
            Dim haveToDelete As Boolean = False

            MyPath = "Clients\" & noClient & "\Comm"
            If IO.Directory.Exists(appPath & bar(appPath) & MyPath) = False Then IO.Directory.CreateDirectory(appPath & bar(appPath) & MyPath)

            MyComm = CStr(listeCommunications.ItemValueB(listeCommunications.selected)).Split(New Char() {"§"})
            If MyComm(9) <> "" AndAlso MyComm(9).StartsWith("DB|") = False Then
                OldFileName = MyComm(9).Split(New Char() {"|"})(1)
                sfn = OldFileName.Split(New Char() {"."})
                NewFileName = sfn(0)
            Else
                haveToDelete = True
                NewFileName = Directory.getNewFileName(appPath & bar(appPath) & MyPath, listeCommunications.ItemValueA(listeCommunications.selected))
                OldFileName = ""
            End If
            ImportFile = importer(NewFileName, MyPath, 3, , OldFileName)
            If haveToDelete And IO.File.Exists(appPath & bar(appPath) & MyPath & "\" & NewFileName) Then IO.File.Delete(appPath & bar(appPath) & MyPath & "\" & NewFileName)

            If ImportFile = "" Then Exit Sub

            Dim noCommunication As Integer = 0
            If listeCommunications.selected <> -1 Then noCommunication = listeCommunications.ItemValueA(listeCommunications.selected)
            If noCommunication = 0 Then
                MessageBox.Show("Impossible de lier l'item de la banque de données. Veuillez fermer le compte client, le rouvrir et recommencer. Merci!", "Erreur de communication")
                Exit Sub
            End If

            MyComm(9) = "FILE|" & ImportFile
            listeCommunications.ItemValueB(listeCommunications.selected) = String.Join("§", MyComm)
            DBLinker.getInstance.updateDB("Communications", "NameOfFile='FILE|" & ImportFile.Replace("'", "''") & "'", "NoCommunication", noCommunication, False)
            InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunications(" & noClient & "," & True & ")")
            myMainWin.StatusText = Me.Text & " : Importation pour la communication """ & commSujet.Text & """ effectuée"
        Catch ex As Exception
            addErrorLog(New Exception("listeCommunications.Selected=" & listeCommunications.selected, ex))
            Throw New AlreadyLoggedException(ex)
        End Try
    End Sub

    Private Sub addSaveCommunication(Optional ByVal adding As Boolean = True, Optional ByVal loadList As Boolean = True)
        Dim noCommunication As Integer = 0
        If listeCommunications.selected <> -1 Then noCommunication = listeCommunications.ItemValueA(listeCommunications.selected)

        Dim SMyNoKP(), SMyNoUser(), sMyNoFolder() As String
        Dim MyNoKP, MyNoUser, myNoFolder As Integer

        If commDeA.Text.StartsWith("* Client *") = False Then
            SMyNoKP = commDeA.Text.Split(New Char() {"("}, 2)
            MyNoKP = SMyNoKP(1).Substring(0, SMyNoKP(1).Length - 1)
            DBHelper.addItemToADBList("CommDeA", "NoKP", MyNoKP, "NoDeA", , , , False, "NoClient", noClient)
        End If
        SMyNoUser = commUser.Text.Split(New Char() {"("})
        MyNoUser = SMyNoUser(1).Substring(0, SMyNoUser(1).Length - 1)

        If dossiersClist.SelectedIndex = 0 Then
            myNoFolder = 0
        Else
            sMyNoFolder = dossiersClist.Items(dossiersClist.SelectedIndex).ToString.Split(New Char() {"-"}, 2)
            myNoFolder = sMyNoFolder(0)
        End If

        commModified = False
        If adding = True Then
            Dim noComm As Integer = addingComm(noClient, MyNoKP, commType1.Checked, commCategorie.Text, commSujet.Text, CDate(commDate.Text), CommRemarques.Text, myNoFolder)
            listeCommunications.selected = listeCommunications.findStringExact(noComm, , CI.Controls.List.FindingType.ValueA)
            If listeCommunications.itemVisible(listeCommunications.selected) <> CI.Controls.List.ItemVisibility.Fully Then listeCommunications.showItem(listeCommunications.selected)
        Else
            DBLinker.getInstance.updateDB("Communications", "NoKp=" & MyNoKP & ",NoCommSubject=" & DBHelper.addItemToADBList("CommSubjects", "CommSubject", commSujet.Text, "NoCommSubject") & ",CommDate='" & commDate.Text & "',Remarques='" & CommRemarques.Text.Replace("'", "''").Replace(vbCrLf, "<br>") & "',NoFolder=" & myNoFolder & ",NoCategorie=" & DBHelper.addItemToADBList("CommCategories", "Categorie", commCategorie.Text, "NoCategorie"), "NoCommunication", noCommunication, False)
            myMainWin.StatusText = Me.Text & " : Communication """ & commSujet.Text & """ modifiée"
            InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunications(" & noClient & "," & False & ")")
        End If

        If commCategorie.Text <> "" AndAlso commCategorie.Items.IndexOf(commCategorie.Text) = -1 Then
            commCategorie.Items.Add(commCategorie.Text)
            commFiltrageCat.Items.Add(commCategorie.Text)
        End If
    End Sub

    Private Sub lockGeneralInfos(ByRef trueFalse As Boolean)
        'modifsave.Enabled = True
        Dim opposite As Boolean = Not trueFalse
        Dim backColor As Color = SystemColors.ControlLight
        If trueFalse = False Then backColor = Color.White

        'Informations générales
        nom.ReadOnly = trueFalse
        prenom.ReadOnly = trueFalse
        adresse.ReadOnly = trueFalse
        ville.ReadOnly = trueFalse
        codepostal1.ReadOnly = trueFalse
        codepostal2.ReadOnly = trueFalse

        AddTel.Enabled = opposite
        ModifTel.Enabled = opposite
        DelTel.Enabled = opposite
        UpTel.Enabled = opposite
        DownTel.Enabled = opposite

        autrenom.ReadOnly = trueFalse
        annee.ReadOnly = trueFalse
        mois.ReadOnly = trueFalse
        jour.ReadOnly = trueFalse
        sexe(0).Enabled = opposite
        sexe(1).Enabled = opposite

        Telephones.BackColor = backColor

        selectionner.Enabled = opposite

        employeurslist.ReadOnly = trueFalse
        metierslist.ReadOnly = trueFalse
        nam.ReadOnly = trueFalse
        remarques.ReadOnly = trueFalse
        courriel.ReadOnly = trueFalse
        url.ReadOnly = trueFalse
        publipostage.ReadOnly = trueFalse

        'Bouttons & Photo
        choosephoto.Enabled = opposite
        enleverphoto.Enabled = opposite
        If trueFalse = False And myPhoto Is Nothing Then enleverphoto.Enabled = False
        'Onglets
    End Sub

    Private Sub lockFolderText(ByRef trueFalse As Boolean)
        Try
            folderText.Editing(False) = Not trueFalse
        Catch ex As NullReferenceException
            addErrorLog(New Exception("folderText is Nothing=" & (folderText Is Nothing) & vbCrLf & "Me.IsHandleCreated=" & Me.IsHandleCreated & vbCrLf & "isFormClosed=" & isFormClosed, ex))
        End Try
    End Sub

    Private Sub lockFolderInfos(ByVal trueFalse As Boolean)
        ctlFolderInfos.lockFolderInfo(trueFalse)
    End Sub

    Private Sub lockEquipment(ByVal trueFalse As Boolean)
        eType1.Enabled = Not trueFalse
        eType2.Enabled = Not trueFalse
        etrp.ReadOnly = trueFalse

        eNoItem.ReadOnly = trueFalse
        eProfit.ReadOnly = trueFalse
        submit.Enabled = Not trueFalse
        vider.Enabled = Not trueFalse
        eNom.ReadOnly = trueFalse
        createEquipmentReceipt.Enabled = False
        modif.Enabled = Not trueFalse
        delete.Enabled = Not trueFalse

        eDepot.ReadOnly = trueFalse
        ePret.ReadOnly = trueFalse
        anretour.ReadOnly = trueFalse
        moisretour.ReadOnly = trueFalse
        jourretour.ReadOnly = trueFalse
        eDuree.ReadOnly = trueFalse
        eRefunded.Enabled = Not trueFalse
        eVerified.Enabled = Not trueFalse
        eReturned.Enabled = Not trueFalse
        eNotes.ReadOnly = trueFalse
    End Sub

    Private Function subLoadModel(ByVal menuItemToAddTo As MenuItem, ByVal pathOfModeles As String, Optional ByVal n As Integer = 0, Optional ByVal menuAucunWasPreadded As Boolean = True, Optional ByVal persoModel As Boolean = False) As Integer
        Dim i As Integer
        Dim curModeleName As String
        Dim myModeles As ArrayList

        If IO.Directory.Exists(pathOfModeles) = True Then
            myModeles = hddListing(pathOfModeles, False, True)
            For i = 0 To myModeles.Count - 1
                curModeleName = System.Text.RegularExpressions.Regex.Replace(CStr(myModeles(i)), "\.html", "")
                If n = 0 Then menuItemToAddTo.MenuItems.RemoveAt(0)

                If persoModel Then
                    menuItemToAddTo.MenuItems.Add(curModeleName, New EventHandler(AddressOf Me.menumodeleperso_Click))
                Else
                    menuItemToAddTo.MenuItems.Add(curModeleName, New EventHandler(AddressOf Me.menumodelegen_Click))
                End If
                n += 1
            Next i
        End If

        Return n
    End Function

    Private Function convertStringToInt(ByVal input As String) As Integer
        Return Integer.Parse(input)
    End Function

    Private Sub applyModelToFolderText(ByVal myMenuItem As MenuItem, ByVal noUser As Integer)
        Dim myParentMenuItem As MenuItem
        Dim cat As String

        myParentMenuItem = CType(myMenuItem.Parent, MenuItem)

        If myParentMenuItem.Text = myParentMenuItem.GetContextMenu.MenuItems(0).Text Or myParentMenuItem.Text = myParentMenuItem.GetContextMenu.MenuItems(1).Text Then
            cat = "* Tous *"
        Else
            cat = myParentMenuItem.Text
        End If

        Dim myModele As String = ""
        Dim modele() As String = DBLinker.getInstance.readOneDBField("Modeles INNER JOIN ModelesCategories ON ModelesCategories.NoCategorie=Modeles.NoCategorie", "Modele", "WHERE NoUser" & IIf(noUser = 0, " IS null", "=" & noUser) & " AND Nom='" & myMenuItem.Text.Replace("'", "''") & "' AND Categorie='" & cat.Replace("'", "''") & "'")
        If modele IsNot Nothing AndAlso modele.Length <> 0 Then myModele = modele(0)

        'Ajoute le modèle à la boîte de texte actuel
        lastTextBoxSel.insertHtml(myModele)
        lastTextBoxSel.focus()

        If lastTextBoxSel.Name = "antecedants" Then
            antecedentModified = True
        Else
            folderTextsModified = True
        End If
    End Sub

    Private Sub menumodelegen_Click(ByVal sender As Object, ByVal e As EventArgs)
        applyModelToFolderText(CType(sender, MenuItem), 0)
    End Sub

    Private Sub menumodeleperso_Click(ByVal sender As Object, ByVal e As EventArgs)
        applyModelToFolderText(CType(sender, MenuItem), ConnectionsManager.currentUser)
    End Sub

    Private Sub resetFolder()
        currentNoFolder = 0

        resetFolderEquipment()
        resetFolderInfos()
        resetFolderTexts()

        ctlFolderInfos.isFolderModified = False
        folderEquipmentModified = False
        folderTextsModified = False
    End Sub

    Private Sub resetFolderInfos()
        ctlFolderInfos.resetFields()
    End Sub

    Private Sub resetFolderTexts()
        folderTexts.Items.Clear()
        folderText.setHtml("")
    End Sub

    Private Sub resetFolderEquipment()
        resetFolderEquipmentFields()
        loadFolderEquipmentList(0) 'Set NoFolder to 0 so that no item are showed
        lockEquipment(True)
    End Sub

    Private Sub resetFolderEquipmentFields(Optional ByVal skipENom As Boolean = False, Optional ByVal skipList As Boolean = False)
        lockEquipment(False)
        submit.Enabled = True
        If skipENom = False Then eNom.SelectedIndex = -1
        eType1.Checked = False
        eType2.Checked = False
        eDate.Text = DateFormat.getTextDate(Date.Today)
        eNotes.Text = ""
        eDescription.Text = ""
        If skipList = False Then
            With listEquipement
                If .currentRow IsNot Nothing Then .currentRow.Selected = False
                .Tag = Nothing
            End With
        End If
        delete.Enabled = False
        modif.Enabled = False
        backPret.Enabled = False
        If allowFolderEquipmentModification Then
            eType1.Enabled = True
            eType2.Enabled = True
            anretour.ReadOnly = False
            moisretour.ReadOnly = False
            jourretour.ReadOnly = False
            eDuree.ReadOnly = False
            eDepot.ReadOnly = False
            ePret.ReadOnly = False
        End If

        eNoItem.Items.Clear()
        eDepot.Text = 0
        eDepot.forceManaging()
        ePret.Text = 0
        ePret.forceManaging()
        eRefund.minimum = 0
        eRefund.maximum = 0
        eProfit.Text = 0
        eProfit_TextChanged(Me, EventArgs.Empty)
        eTotal.Text = 0
        eDate.Text = ""
        eReturned.Checked = False
        eVerified.Checked = False
        eRefunded.Checked = False
        eDate.Text = DateFormat.getTextDate(Date.Now)
        eNoItem.DropDownStyle = ComboBoxStyle.DropDownList

        anretour.Text = Date.Today.Year
        moisretour.Text = addZeros(Date.Today.Month.ToString, 2)
        jourretour.Text = addZeros(Date.Today.Day.ToString, 2)


        folderEquipmentModified = False
    End Sub

    Private Sub adjustNbDays(ByVal anCtl As ComboBox, ByVal moisCtl As ComboBox, ByVal jourCtl As ComboBox)
        Dim monthDays() As Byte = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        Dim an1, an2, an3 As String

        an1 = CDbl(anCtl.Text) / 400
        an2 = CDbl(anCtl.Text) / 100
        an3 = CDbl(anCtl.Text) / 4
        If an3.IndexOf(",") = -1 And an3.IndexOf(".") = -1 Then monthDays(1) = 29
        If an2.IndexOf(",") = -1 And an2.IndexOf(".") = -1 Then monthDays(1) = 28
        If an1.IndexOf(",") = -1 And an1.IndexOf(".") = -1 Then monthDays(1) = 29

        If CDbl(moisCtl.Text) > 0 Then
            Do
                If monthDays(CDbl(moisCtl.Text) - 1) > jourCtl.Items.Count Then
                    jourCtl.Items.Add(CStr(jourCtl.Items.Count + 1))
                ElseIf monthDays(CDbl(moisCtl.Text) - 1) < jourCtl.Items.Count Then
                    jourCtl.Items.RemoveAt(jourCtl.Items.Count - 1)
                End If
            Loop Until monthDays(CDbl(moisCtl.Text) - 1) = jourCtl.Items.Count
        End If
    End Sub

    Private Sub lockCommunications(ByVal trueFalse As Boolean)
        commCategorie.ReadOnly = trueFalse
        commType1.Enabled = Not trueFalse
        commType2.Enabled = Not trueFalse
        commDeA.ReadOnly = trueFalse
        selectKeyPeople.Enabled = Not trueFalse
        commSujet.ReadOnly = trueFalse
        selectCommDate.Enabled = Not trueFalse
        CommRemarques.ReadOnly = trueFalse
        btnAddComm.Enabled = Not trueFalse
        importComm.Enabled = Not trueFalse
        modifComm.Enabled = Not trueFalse
        delComm.Enabled = Not trueFalse
        viderComm.Enabled = Not trueFalse
        dossiersClist.ReadOnly = trueFalse
        listeCommunications_WillSelect(listeCommunications, New CI.Controls.List.WillSelectEventArgs(listeCommunications.selected, 0, 0, 0, False))
        listeCommunications_SelectedChange()
    End Sub

    Private Function saveFolderInfos() As String
        Dim noTRP() As String
        noTRP = System.Text.RegularExpressions.Regex.Split(ctlFolderInfos.trpTraitant, " \(")
        noTRP(1) = noTRP(1).Substring(0, noTRP(1).Length - 1)

        If ctlFolderInfos.siteLesion = "" Then
            MessageBox.Show("Veuillez entrer un site de lésion", "Information manquante")
            ongletsdossier.SelectedIndex = 1
            ctlFolderInfos.ctlregion_Renamed.Focus()
            Exit Function
        End If
        If ctlFolderInfos.service = "" Then
            MessageBox.Show("Veuillez entrer un service", "Information manquante")
            ongletsdossier.SelectedIndex = 1
            ctlFolderInfos.ctlService.Focus()
            Exit Function
        End If

        'REM_CODES
        Dim myCode As FolderCode = ctlFolderInfos.codeDossier(0)
        If PreferencesManager.getUserPreferences()("checkForMissingInfos") = True AndAlso myCode.confirmReference = True AndAlso ctlFolderInfos.noRef = "" AndAlso MessageBox.Show("Avez-vous oublié d'entrer le numéro de référence ?", "Numéro de référence", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            ongletsdossier.SelectedIndex = 1
            ctlFolderInfos.ctlnoref.Focus()
            Exit Function
        End If
        If PreferencesManager.getUserPreferences()("checkForMissingInfos") = True AndAlso myCode.confirmDiagnostic = True AndAlso ctlFolderInfos.diagnostic = "" AndAlso MessageBox.Show("Avez-vous oublié d'entrer le diagnostic ?", "Diagnostic", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            ongletsdossier.SelectedIndex = 1
            ctlFolderInfos.ctlDiagnostic.Focus()
            Exit Function
        End If

        If PreferencesManager.getUserPreferences()("checkForMissingInfos") = True AndAlso PreferencesManager.getGeneralPreferences()("ConfirmDateAccident") = True AndAlso ctlFolderInfos.dateAccident = "" AndAlso myCode.accidentDate = True AndAlso MessageBox.Show("Avez-vous oublié d'entrer la date d'accident ?", "Date d'accident", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            ongletsdossier.SelectedIndex = 1
            Exit Function
        End If
        If PreferencesManager.getUserPreferences()("checkForMissingInfos") = True AndAlso PreferencesManager.getGeneralPreferences()("ConfirmDateRechute") = True AndAlso ctlFolderInfos.dateRechute = "" AndAlso myCode.relaspeDate = True AndAlso MessageBox.Show("Avez-vous oublié d'entrer la date de rechute ?", "Date de rechute", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            ongletsdossier.SelectedIndex = 1
            Exit Function
        End If

        Dim DateRef, DateAcc, DateRec, noMedStr, dateReceptionRef As String
        If ctlFolderInfos.dateReference = "" Then
            DateRef = "null"
        Else
            DateRef = "'" & ctlFolderInfos.dateReference & "'"
        End If
        If ctlFolderInfos.dateReferenceReception = "" Then
            dateReceptionRef = "null"
        Else
            dateReceptionRef = "'" & ctlFolderInfos.dateReferenceReception & "'"
        End If
        If ctlFolderInfos.dateAccident = "" Then
            DateAcc = "null"
        Else
            DateAcc = "'" & ctlFolderInfos.dateAccident & "'"
        End If
        If ctlFolderInfos.dateRechute = "" Then
            DateRec = "null"
        Else
            DateRec = "'" & ctlFolderInfos.dateRechute & "'"
        End If
        If curDoctorNo = "" OrElse curDoctorNo = 0 Then
            noMedStr = "null"
        Else
            noMedStr = curDoctorNo
        End If

        Dim codeChanged As Boolean = oldFolderCode IsNot Nothing AndAlso oldFolderCode.noCodification <> ctlFolderInfos.codeDossier(0).noCodification
        'REM_CODES 
        DBLinker.getInstance.updateDB("InfoFolders", _
        "Remarques='" & ctlFolderInfos.remarques.Replace("'", "''") & _
        "',Service='" & ctlFolderInfos.service.Replace("'", "''") & _
        "',NoTRPDemande=" & IIf(ctlFolderInfos.trpDemande = 0, "null", ctlFolderInfos.trpDemande) & _
        ",Diagnostic='" & ctlFolderInfos.diagnostic.Replace("'", "''") & _
        "',NoSiteLesion=" & DBHelper.addItemToADBList("SiteLesion", "SiteLesion", ctlFolderInfos.siteLesion, "NoSiteLesion") & _
        ",NoKP=" & noMedStr & _
        If(codeChanged, ",NoCodeUnique=" & myCode.noUnique, String.Empty) & _
        If(codeChanged, ",NoCodeUser=" & noTRP(1), String.Empty) & _
        If(codeChanged, ",NoCodeDate='" & DateFormat.getTextDate(Date.Today) & "'", String.Empty) & _
        ",NoRef='" & ctlFolderInfos.noRef _
        & "',DateRef=" & DateRef & _
        ",DateAccident=" & DateAcc & _
        ",DateRechute=" & DateRec & _
        ",DateReceptionRef=" & dateReceptionRef & _
        ",Duree=" & ctlFolderInfos.duree & _
        ",Frequence=" & ctlFolderInfos.frequence & _
        ",NoTRPTraitant=" & noTRP(1) & _
        ",NoTRPToTransfer=" & IIf(ctlFolderInfos.trpToTransfer = 0, "null", ctlFolderInfos.trpToTransfer), _
        "NoFolder", currentNoFolder, False)

        If OldTherapeute <> ctlFolderInfos.trpTraitant Then
            Dim existingAlerts() As String = DBLinker.getInstance.readOneDBField("SELECT LastNoUserAlert FROM FolderAlerts WHERE NoFolder=" & currentNoFolder & " UNION ALL SELECT NoUserAlert FROM FolderTexteAlerts FTA INNER JOIN FolderTextes FT ON FT.NoFolderTexte=FTA.NoFolderTexte WHERE FT.NoFolder=" & currentNoFolder)
            If existingAlerts IsNot Nothing AndAlso existingAlerts.Length <> 0 Then
                Dim oldTrp() As String = OldTherapeute.Split(New Char() {"("})
                oldTrp(1) = oldTrp(1).Substring(0, oldTrp(1).Length - 1)
                For a As Integer = 0 To existingAlerts.GetUpperBound(0)
                    AlertsManager.getInstance.transferAlertTo(existingAlerts(a), noTRP(1), oldTrp(1))
                Next a
            End If
            DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient, Comments", "21," & currentNoFolder & "," & noClient & ",'De " & OldTherapeute.Replace("'", "''") & " vers " & ctlFolderInfos.trpTraitant.Replace("'", "''") & "'")
        End If
        If codeChanged Then
            'Adjust external status of R-V to the startingStatus of the new code
            DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & myCode.startingExternalStatus, "NoFolder", currentNoFolder, False)
            'Log change
            DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient, Comments", "26," & currentNoFolder & "," & noClient & ",'De " & oldFolderCode.name.Replace("'", "''") & "(" & DateFormat.getTextDate(oldFolderCode.firstEffectiveTime) & ")" & " vers " & ctlFolderInfos.codeDossier(0).toString.Replace("'", "''") & "(" & DateFormat.getTextDate(ctlFolderInfos.codeDossier(0).firstEffectiveTime) & ")" & "'")
        End If
        If OldService <> "" AndAlso OldService <> ctlFolderInfos.service Then
            DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient, Comments", "31," & currentNoFolder & "," & noClient & ",'De " & OldService.Replace("'", "''") & " vers " & ctlFolderInfos.service.Replace("'", "''") & "'")
        End If

        If ctlFolderInfos.isFolderModified Then
            ctlFolderInfos.isFolderModified = False
            OldTherapeute = ctlFolderInfos.trpTraitant
            'REM_CODES 
            oldFolderCode = myCode
            OldRegion = ctlFolderInfos.siteLesion
            loadingDossiersDueToInfos = True
            dossier.Items(dossier.SelectedIndex) = currentNoFolder & " - " & OldRegion & " - " & OldTherapeute & " (" & oldFolderCode.name & ") (" & IIf(foldersActive(currentNoFolder.ToString), "Actif", "Inactif") & ")"
            dossiersVlist.Items(dossier.SelectedIndex + 1) = currentNoFolder & " - " & OldRegion & " - " & OldTherapeute & " (" & oldFolderCode.name & ") (" & IIf(foldersActive(currentNoFolder.ToString), "Actif", "Inactif") & ")"
            dossiersClist.Items(dossier.SelectedIndex + 1) = currentNoFolder & " - " & OldRegion & " - " & OldTherapeute & " (" & oldFolderCode.name & ") (" & IIf(foldersActive(currentNoFolder.ToString), "Actif", "Inactif") & ")"
            loadingDossiersDueToInfos = False
            InternalUpdatesManager.getInstance.sendUpdate("AccountsDossiers(" & noClient & ")")
            InternalUpdatesManager.getInstance.sendUpdate("AccountsDossierInfos(" & noClient & "," & currentNoFolder & ")")
        End If

        myMainWin.StatusText = Me.Text & " : Informations de base du dossier #" & currentNoFolder & " enregistré"

        desactivateFolderInfos()

        Return "DONE"
    End Function

    Private Sub desactivateFolderEquipment()
        folderEquipmentModified = False
        lockSecteur("ClientFacturation-" & noClient & "-", False, "Facturation du client " & nom.Text & "," & prenom.Text)
        currentLocks.Remove("ClientFacturation-" & noClient & "-")

        ToolTip1.SetToolTip(modifsaveEquip, "Commencer la modification de l'équipement prêté/vendu du dossier sélectionné")
        modifsaveEquip.Image = imgModifSave.Images(0)
        lockEquipment(True)
        lockSecteur("ClientFolderEquip-" & noClient & "-" & currentNoFolder & "-", False)
        allowFolderEquipmentModification = False
    End Sub

    Private Sub desactivateFolderInfos()
        ctlFolderInfos.isFolderModified = False
        ToolTip1.SetToolTip(modifsaveDossierInfos, "Modifier les informations de base du dossier sélectionné")
        modifsaveDossierInfos.Image = imgModifSave.Images(0)
        lockFolderInfos(True)
        lockSecteur("ClientFolderInfos-" & noClient & "-" & currentNoFolder & "-", False, "Dossier d'un client")
        allowFolderInfosModification = False
    End Sub

    Private Sub desactivateFolderTexts()
        modelebtn1.Enabled = False
        folderTextsModified = False
        ToolTip1.SetToolTip(modifsaveTextes, "Modifier les textes du dossier sélectionné")
        modifsaveTextes.Image = imgModifSave.Images(0)
        lockFolderText(True)
        lockSecteur("ClientFolderText-" & noClient & "-" & currentNoFolder & "-", False, "Dossier d'un client")
        allowFolderTextsModification = False
    End Sub

    Private Function saveFolderTexts() As String
        Dim noTRP() As String
        Dim i As Integer
        Dim currentTabIndex As Byte = ongletsdossier.SelectedIndex

        noTRP = System.Text.RegularExpressions.Regex.Split(ctlFolderInfos.trpTraitant, " \(")
        noTRP(1) = noTRP(1).Substring(0, noTRP(1).Length - 1)

        modelebtn1.Enabled = False

        'Boîte de textes
        Dim updateUserAlert As Boolean = False
        If folderTexts.SelectedIndex <> -1 Then 'Droit & Accès
            'REM_CODES
            Dim sbTextes As New System.Text.StringBuilder
            Dim myFT As FolderText = CType(folderTexts.SelectedItem, FolderText)
            myFT.text = folderText.getHTML()
            myFT.textPosition = folderText.getPos()
            Dim texteTitle As String = ""
            For i = 0 To folderTexts.Items.Count - 1
                Dim curFT As FolderText = CType(folderTexts.Items(i), FolderText)
                Dim curFTT As FolderTextType = curFT.getFolderTexteType
                Dim initialContent As String = curFTT.getFolderTextInitialContent()
                Dim curContent As String = ""
                If Not curFT.text Is Nothing Then curContent = curFT.text
                If curContent.ToLower = "<div>&nbsp;</div>" Then curContent = String.Empty
                Dim hasContentToSave As Boolean = curContent <> String.Empty AndAlso curContent <> initialContent

                curContent = curContent.Replace("'", "''")
                Dim inserting As String = ""
                Dim updating As String = ""

                If (curFTT.multiple Or curFTT.copyTextToOtherText <> 0 Or curFTT.startingExternalStatus <> 1) AndAlso hasContentToSave AndAlso curFT.dateFinished.Equals(LIMIT_DATE) Then
                    If MessageBox.Show("Avez-vous terminé '" & curFT.textTitle & "' ?", "Texte terminé", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        sbTextes.Append("UPDATE FolderTextes SET TextePos='").Append(curFT.textPosition).Append("',Texte='").Append(IIf(curFTT.resetTextOnCopy, "", curContent)).Append("',IsTexte='").Append(curContent <> "").Append("'")
                        folderTextsModified = True
                        If curFTT.multiple OrElse curFTT.resetTextOnCopy = False Then
                            sbTextes.Append(",DateFinished='").Append(DateFormat.getTextDate(Date.Today)).Append("'")
                            curFT.dateFinished = Date.Today
                        End If

                        'Copie le texte dans un autre boîte si nécessaire
                        If curFTT.copyTextToOtherText > 0 Then
                            Dim copyFTT As FolderTextType = FolderTextTypesManager.getInstance.getItemable(curFTT.copyTextToOtherText)
                            For j As Integer = 0 To folderTexts.Items.Count - 1
                                Dim targetFT As FolderText = folderTexts.Items(j)
                                If targetFT.noFolderTexte <> curFT.noFolderTexte AndAlso (targetFT.noFolderTexteType = curFTT.copyTextToOtherText OrElse targetFT.textTitle = copyFTT.textTitle) Then
                                    If targetFT.text Is Nothing Then targetFT.text = ""

                                    targetFT.text &= "<div style='border-top:solid 1px black;border-bottom:solid 1px black;'>"
                                    targetFT.text &= "<b>" & curFT.textTitle & "</b>"
                                    targetFT.text &= "<div style='text-align:right;'>Débuté le " & DateFormat.getTextDate(curFT.dateStarted) & "</div>"
                                    targetFT.text &= curFT.text
                                    targetFT.text &= "<div style='text-align:right;'>Terminé le " & DateFormat.getTextDate(Date.Today) & "</div>"
                                    targetFT.text &= "</div>"
                                    targetFT.text &= "<BR>"
                                    updating = "UPDATE FolderTextes SET Texte='" & targetFT.text.Replace("'", "''") & "' WHERE NoFolderTexte=" & targetFT.noFolderTexte & ";"
                                    Exit For
                                End If
                            Next j
                        End If

                        If curFTT.resetTextOnCopy Then 'Reset date
                            curFT.dateStarted = Date.Today
                            curFT.text = curFTT.getFolderTextInitialContent()
                            sbTextes.Append(",DateStarted='" & DateFormat.getTextDate(Date.Today) & "'")
                        End If

                        'Test pour les deux cas où le texte doit être arrêté (Si faux = doit être arrêté)
                        Dim wtbOnClosing As Boolean = (curFTT.whenToBeStopped = FolderTextType.WhenToBeStop.OnFolderClosing AndAlso dossier.Text.IndexOf("(Actif)") <> -1)
                        Dim wtbOnMax As Boolean = curFTT.whenToBeStopped = FolderTextType.WhenToBeStop.OnMaxReached AndAlso curFT.noMultiple < curFTT.nbMultipleEnding

                        If (wtbOnClosing Or wtbOnMax) AndAlso curFTT.multiple = True AndAlso curFTT.typeForMultiple = FolderTextType.TypeMultiple.OnTexteEnded Then
                            updateUserAlert = updateUserAlert OrElse curFTT.showAlert
                            inserting = Accounts.Clients.Folders.FolderText.add(curFTT.noFolderTexteType, Me.noClient, currentNoFolder, curFTT.textTitle & " " & curFT.noMultiple + 1, Date.Today, curFT.noMultiple + 1, noTRP(1), Me.nom.Text & "," & Me.prenom.Text, False)
                        End If

                        myMainWin.StatusText = Me.Text & " : Texte """ & curFT.textTitle & """ du dossier #" & currentNoFolder & " terminé"
                    Else
                        sbTextes.Append("UPDATE FolderTextes SET Texte='").Append(curContent).Append("',TextePos='").Append(curFT.textPosition).Append("',IsTexte='").Append(curContent <> "").Append("'")
                    End If
                Else
                    sbTextes.Append("UPDATE FolderTextes SET Texte='").Append(curContent).Append("',TextePos='").Append(curFT.textPosition).Append("',IsTexte='").Append(curContent <> "").Append("'")
                End If

                sbTextes.Append(" WHERE NoFolderTexte=").Append(curFT.noFolderTexte).Append(";").Append(inserting).Append(updating)
            Next i
            DBLinker.executeSQLScript(sbTextes.ToString())
            sbTextes = Nothing
        End If

        If folderTextsModified Then
            folderTextsModified = False
            If updateUserAlert Then AlertsManager.sendUpdate(noTRP(1))
            InternalUpdatesManager.getInstance.sendUpdate("AccountsDossierTextBoxes(" & noClient & "," & currentNoFolder & ")")
        End If

        desactivateFolderTexts()

        'Recharge la version visualisable du texte
        If folderTexts.SelectedIndex <> -1 Then
            'REM_CODES
            Dim myFT As FolderText = CType(folderTexts.SelectedItem, FolderText)
            folderText.setHtml(myFT.text)
        End If

        myMainWin.StatusText = Me.Text & " : Textes du dossier #" & currentNoFolder & " enregistré"

        Return "DONE"
    End Function

    Private Function ensureFolderActive() As String
        If foldersActive(currentNoFolder) = False Then
            Dim myMsgBox As New MsgBox1()
            Dim choice As Byte = myMsgBox("Voulez-vous réactiver ce dossier ?", "Activation de dossier", 3, "Activer", "Modifier sans activer", "Annuler")
            If choice = 3 Then Return ""
            If choice = 1 Then
                If Accounts.Clients.Folders.ClientFolder.changeStatus(FolderPossibleStatuses.Inactive, FolderPossibleStatuses.Active, noClient, currentNoFolder) = False Then
                    Return ""
                End If
            End If
        End If

        Return "DONE"
    End Function

    Private Function activateFolderEquipment() As String
        'Droit & Accès
        If currentDroitAcces(12) = False And currentDroitAcces(13) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Function
        End If

        Dim currentTabIndex As Byte = ongletsdossier.SelectedIndex
        If ensureFolderActive() = "" Then Return ""
        If lockSecteur("ClientFolderEquip-" & noClient & "-" & currentNoFolder & "-", True, "Équipement du dossier #" & currentNoFolder & " du client " & Me.nom.Text & "," & Me.prenom.Text) = False Then Return ""

        If lockSecteur("ClientFacturation-" & noClient & "-", True, "Facturation du client " & nom.Text & "," & prenom.Text, False) = False Then
            If PreferencesManager.getUserPreferences()("AffMSGModif") = True Then MessageBox.Show("Factures & Paiements en cours de modification." & vbCrLf & "Veuillez changer d'onglet pour pourvoir accéder à la modification de ce dossier ou attendre que l'autre utilisateur termine avec celui-ci.", "Impossible de modifier")
            lockSecteur("ClientFolderEquip-" & noClient & "-" & currentNoFolder & "-", False)

            Exit Function
        Else
            currentLocks.Add("ClientFacturation-" & noClient & "-")
        End If

        allowFolderEquipmentModification = True

        lockEquipment(False)
        If Not listEquipement.Tag Is Nothing Then
            Dim myFacture As New Bill(listEquipement.Rows.Item(listEquipement.currentRow.Index).Cells(3).Value)
            afterSelectEquipmentActivation(myFacture.getBillPaymentTotal)
        Else
            resetFolderEquipmentFields()
        End If

        ToolTip1.SetToolTip(modifsaveEquip, "Arrêter la modification de l'équipement prêté/vendu du dossier sélectionné")
        modifsaveEquip.Image = imgModifSave.Images(2)
    End Function

    Private Sub askTRPToTransfer()
        Dim sNoTRP() As String = ctlFolderInfos.trpTraitant.Split(New Char() {"("})
        Try
            If askForTRPToTransfer(noClient, currentNoFolder, sNoTRP(1).Substring(0, sNoTRP(1).Length - 1), ctlFolderInfos.trpToTransfer) = True Then
                'REM Should a pref to activate/desactivate?
                'ctlFolderInfos.ShowHiddenPart()
                'ctlFolderInfos.selectTRP.Focus()
            End If
        Catch ex As Exception
            addErrorLog(New Exception("ctlFolderInfos.TRPTraitant=" & ctlFolderInfos.trpTraitant & vbCrLf & "NoFolder=" & currentNoFolder, ex))
        End Try
    End Sub

    Private Function activateFolderInfos() As String
        'Droit & Accès
        If currentDroitAcces(13) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Function
        End If

        Dim currentTabIndex As Byte = ongletsdossier.SelectedIndex
        If ensureFolderActive() = "" Then Return ""
        If lockSecteur("ClientFolderInfos-" & noClient & "-" & currentNoFolder & "-", True, "Infos du dossier #" & currentNoFolder & " du client " & Me.nom.Text & "," & Me.prenom.Text) = False Then Return ""

        'Droit & Accès
        allowFolderInfosModification = currentDroitAcces(13)
        lockFolderInfos(False)
        askTRPToTransfer()
        ctlFolderInfos.lockCodeSelection(Not acceptFolderCodeModification) 'S'il existe des RV avec des présences, désactive

        ToolTip1.SetToolTip(modifsaveDossierInfos, "Enregistrer les informations de base du dossier sélectionné")
        modifsaveDossierInfos.Image = imgModifSave.Images(1)

        ctlFolderInfos.isFolderModified = False
    End Function

    Private Function activateFolderTexts() As String
        'Droit & Accès
        If currentDroitAcces(12) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Function
        End If
        If folderTexts.SelectedIndex = -1 Or dossier.SelectedIndex = -1 Then
            MessageBox.Show("Veuillez sélectionner un dossier et un texte avant de modifer. Merci!", "Aucun texte sélectionné")
            Exit Function
        End If

        Dim currentTabIndex As Byte = ongletsdossier.SelectedIndex
        If ensureFolderActive() = "" Then Return ""
        If lockSecteur("ClientFolderText-" & noClient & "-" & currentNoFolder & "-", True, "Textes du dossier #" & currentNoFolder & " du client " & Me.nom.Text & "," & Me.prenom.Text) = False Then Return ""

        askTRPToTransfer()
        ToolTip1.SetToolTip(modifsaveTextes, "Enregistrer les textes du dossier sélectionné")
        modifsaveTextes.Image = imgModifSave.Images(1)
        allowFolderTextsModification = True
        lockFolderText(False)
        If folderTexts.SelectedIndex <> -1 Then
            'REM_CODES 
            folderText.setHtml(CType(folderTexts.SelectedItem, FolderText).text)
            'REM_CODES 
            folderText.setPos(CType(folderTexts.SelectedItem, FolderText).textPosition)
            folderText.focus()
        End If
        modelebtn1.Enabled = True

        Return "DONE"
    End Function


    Private Function saveEquipment() As String
        If eNom.Text = "" Then Exit Function

        If eType2.Checked Then
            Try
                Dim testDate As Date = CDate(anretour.Text & "/" & moisretour.Text & "/" & jourretour.Text)
            Catch
                MessageBox.Show("Veuillez sélectionner une date de retour valide.", "Date de retour invalide")
                Return ""
            End Try
        End If

        Dim curItem As Item

        If submit.Enabled = False Then
            If dossier.SelectedIndex = -1 Then Return ""
            If listEquipement.Tag Is Nothing Then Return ""

            curItem = listEquipement.Tag

            If eTotal.Text < curItem.curBill.getBillPaymentTotal Then MessageBox.Show("Impossible d'enregistrer cet équipement. Le montant total est inférieur au montant déjà payé (" & Chaines.forceManaging(curItem.curBill.getBillPaymentTotal.ToString, True, "", False, True, False, False, ",§.", , , , , , , 2) & " $.", "Enregistrement") : Return ""
            If TypeOf curItem Is ItemBorrowed AndAlso eRefund.Text < curItem.curBill.getBillPaymentTotal() Then MessageBox.Show("Impossible d'enregistrer cet équipement. Le montant à rembourser est inférieur au montant déjà payé (" & Chaines.forceManaging(curItem.curBill.getBillPaymentTotal.ToString, True, "", False, True, False, False, ",§.", , , , , , , 2) & " $.", "Enregistrement") : Return ""

            curItem.profitAmount = eProfit.Text

            If TypeOf curItem Is ItemBorrowed Then
                With CType(curItem, ItemBorrowed)
                    .verifiedByTRP = eVerified.Checked
                    .isRefund = eRefunded.Checked
                    .description = eNotes.Text
                    .amountRefund = eRefund.Text
                    .cost = ePret.Text
                    .depositAmount = eDepot.Text
                    .returningDate = New Date(anretour.Text, moisretour.Text, jourretour.Text)
                End With
            End If

            curItem.saveData()

            myMainWin.StatusText = Me.Text & " : Équipement modifié (" & listEquipement.Rows(listEquipement.currentRow.Index).Cells(1).Value & ")"
        Else
            addEquipment()
        End If

        folderEquipmentModified = False

        Return "DONE"
    End Function

    Private Function saveClientHistory() As String
        If ToolTip1.GetToolTip(saveantecedent).StartsWith("Modifier") Then
            'Droit & Accès
            If currentDroitAcces(56) = False Then
                'Message & Exit
                MessageBox.Show("Vous n'avez pas le droit de modifier ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
                Exit Function
            End If

            If lockSecteur("ClientAntecedents-" & noClient & "-", True, "Bilan de santé du client") = True Then
                ToolTip1.SetToolTip(saveantecedent, "Enregistrer les modifications")
                saveantecedent.Image = imgModifSave.Images(1)
                antecedants.Editing = True
                antecedants.focus()
                modelebtn2.Enabled = True
            End If
        Else
            Dim contenu As String = antecedants.getHTML()
            DBLinker.getInstance.updateDB("ClientsAntecedents", "Antecedents='" & contenu.Replace("'", "''") & "'", "NoClient", Me.noClient, False)

            ToolTip1.SetToolTip(saveantecedent, "Modifier le bilan de santé")
            saveantecedent.Image = imgModifSave.Images(0)

            modelebtn2.Enabled = False
            antecedants.Editing = False

            lockSecteur("ClientAntecedents-" & noClient & "-", False, "Bilan de santé du client")

            InternalUpdatesManager.getInstance.sendUpdate("AccountsAntecedents(" & noClient & ")")
            myMainWin.StatusText = Me.Text & " : Bilan de santé enregistré"

            antecedants.setHtml(contenu)
            'REM If CurrentUserName = "Administrateur" Then If MessageBox.Show("Voulez-vous sauvegarder pour le bilan de santé du logiciel?", "Antécédents logiciels", MessageBoxButtons.YesNo) = DialogResult.Yes Then IO.File.Copy(AppPath & Bar(AppPath) & "Clients\" & NoClient & "\" & NoClient & "-antecedents.html", AppPath & Bar(AppPath) & "Data\antecedents.html", True)
        End If

        antecedentModified = False
        Return "DONE"
    End Function

    Private Function saveGeneralInfo() As String
        Dim Phones, Referer, sPhones() As String
        Referer = ""

        'Vérification des champs obligatoires
        If PreferencesManager.getGeneralPreferences()("COCC1") = True And nom.Text = "" Then MessageBox.Show("Le champ 'Nom' est obligatoire", "Information manquante") : nom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC2") = True And prenom.Text = "" Then MessageBox.Show("Le champ 'Prénom' est obligatoire", "Information manquante") : prenom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC3") = True And adresse.Text = "" Then MessageBox.Show("Le champ 'Adresse' est obligatoire", "Information manquante") : adresse.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC4") = True And ville.Text = "" Then MessageBox.Show("Le champ 'Ville' est obligatoire", "Information manquante") : ville.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC5") = True And (codepostal1.Text = "" Or codepostal2.Text = "" Or codepostal1.Text.Length < 3 Or codepostal2.Text.Length < 3) Then MessageBox.Show("Le champ 'Code postal' est obligatoire", "Information manquante") : codepostal1.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC6") = True And Telephones.Items.Count = 0 Then MessageBox.Show("Le champ 'Téléphones' est obligatoire", "Information manquante") : AddTel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC9") = True And autrenom.Text = "" Then MessageBox.Show("Le champ 'Autre nom' est obligatoire", "Information manquante") : autrenom.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC10") = True And (annee.Text = "" Or mois.Text = "" Or jour.Text = "") Then MessageBox.Show("Le champ 'Date de naissance' est obligatoire", "Information manquante") : annee.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC11") = True And sexe(0).Checked = False And sexe(1).Checked = False Then MessageBox.Show("Le champ 'Sexe' est obligatoire", "Information manquante") : sexe(0).Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC12") = True And employeurslist.Text = "" Then MessageBox.Show("Le champ 'Employeur' est obligatoire", "Information manquante") : employeurslist.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC13") = True And metierslist.Text = "" Then MessageBox.Show("Le champ 'Métier' est obligatoire", "Information manquante") : metierslist.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC15") = True And nam.Text = "" Then MessageBox.Show("Le champ 'Numéro d'assurance maladie' est obligatoire", "Information manquante") : nam.Focus() : Exit Function
        If nam.Text <> "" AndAlso nam.Text.Length < 12 Then MessageBox.Show("Le champ 'Numéro d'assurance maladie' n'est pas complet", "Information incomplète") : nam.Focus() : Exit Function
        If nam.Text <> "" AndAlso User.validateNAM(nam.Text) = False Then MessageBox.Show("Le champ 'Numéro d'assurance maladie' n'est pas valide", "Information invalide") : nam.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC16") = True And reference.Text = "" Then MessageBox.Show("Le champ 'Référence' est obligatoire", "Information manquante") : selectionner.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC17") = True And remarques.Text = "" Then MessageBox.Show("Le champ 'Remarques' est obligatoire", "Information manquante") : remarques.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC18") = True And courriel.Text = "" Then MessageBox.Show("Le champ 'Courriel' est obligatoire", "Information manquante") : courriel.Focus() : Exit Function
        If publipostage.SelectedIndex = 2 And courriel.Text = "" Then MessageBox.Show("Le champ 'Courriel' est obligatoire lorsque le publipostage est envoyé par courriel", "Information manquante") : courriel.Focus() : Exit Function
        If PreferencesManager.getGeneralPreferences()("COCC19") = True And url.Text = "" Then MessageBox.Show("Le champ 'Adresse du site internet' est obligatoire", "Information manquante") : url.Focus() : Exit Function

        Dim emailValidation As EmailValidator.ValidationLevels = EmailValidator.isEmailValid(MailsManager.mainFromEmailAddress, courriel.Text)
        If courriel.Text <> email_field_not_filled AndAlso courriel.Text <> "" And emailValidation <> EmailValidator.ValidationLevels.Valid Then
            Dim message As String = String.Empty
            Dim domain As String = courriel.Text.Substring(courriel.Text.IndexOf("@") + 1)
            Select Case emailValidation
                Case EmailValidator.ValidationLevels.WrongStructure
                    message = "Veuillez vous assurez que l'adresse de courriel soit valide :" & vbCrLf & "alias@domaine.extension" & vbCrLf & "Exemple : info@cints.net"

                Case EmailValidator.ValidationLevels.DomainNotExists
                    message = "Le nom de domaine """ & domain & """ n'existe pas ou n'a pas de serveur de courriel"

                Case EmailValidator.ValidationLevels.NotConfirmedByDomain
                    message = "L'adresse a été rejeté par le nom de domaine """ & domain & """"
            End Select

            MessageBox.Show(message, "Courriel invalide")
            courriel.Focus()
            Exit Function
        End If

        If nam.Text <> "" Then
            Dim existingNam() As String = DBLinker.getInstance.readOneDBField("InfoClients", "NoClient", "WHERE NAM='" & nam.Text.Replace("'", "''") & "' AND NoClient<>" & Me.noClient)
            If existingNam IsNot Nothing AndAlso existingNam.Length <> 0 Then
                If MessageBox.Show("Le numéro d'assurance maladie est déjà utilisé par un autre client. Il est impossible d'enregistrer les informations de base de ce compte avec ce NAM. Désirez-vous ouvrir l'autre compte ayant le même NAM ?", "NAM déjà utilisé", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then openAccount(existingNam(0))
                Exit Function
            End If
        End If

        Dim myTitle As String = "Client " & Me.noClient & " : " & nom.Text & "," & prenom.Text & " : " & nam.Text
        If Me.Text <> myTitle Then updateText(Me, myTitle)

        'Écriture des données dans la base de données
        ReDim sPhones(Telephones.Items.Count - 1)
        Telephones.Items.CopyTo(sPhones, 0)
        Phones = String.Join("§", sPhones)
        If Phones Is Nothing Then Phones = ""
        If Not reference.Text = "Nom du référent" Then Referer = reference.Tag & "§" & reference.Text.Replace(vbCrLf, "<br>")

        Dim dateNaissance As String = "null"
        If annee.Text <> String.Empty AndAlso mois.Text <> String.Empty AndAlso jour.Text <> String.Empty Then dateNaissance = "'" & annee.Text & "/" & mois.Text & "/" & jour.Text & "'"

        DBLinker.getInstance.updateDB("InfoClients", "Nom='" & nom.Text.Replace("'", "''") & "',Prenom='" & prenom.Text.Replace("'", "''") & "',Adresse='" & adresse.Text.Replace("'", "''") & "',NoVille=" & DBHelper.addItemToADBList("Villes", "NomVille", ville.Text, "NoVille") & ",CodePostal='" & codepostal1.Text & codepostal2.Text & "',Telephones='" & Phones.Replace("'", "''") & "',AutreNom='" & autrenom.Text.Replace("'", "''") & "',DateNaissance=" & dateNaissance & ",SexeHomme='" & sexe(0).checked & "',NoEmployeur=" & DBHelper.addItemToADBList("Employeurs", "Employeur", employeurslist.Text, "NoEmployeur") & ",NoMetier=" & DBHelper.addItemToADBList("Metiers", "Metier", metierslist.Text, "NoMetier") & ",NAM='" & nam.Text & "',NomReferent='" & Referer.Replace("'", "''") & "',Courriel=" & If(courriel.Text = email_field_not_filled, "null", "'" & courriel.Text & "'") & ",URL=" & If(url.Text = url_field_not_filled, "null", "'" & url.Text & "'") & ",Description='" & remarques.Text.Replace("'", "''") & "',Publipostage=" & publipostage.SelectedIndex, "NoClient", noClient, False)

        lockGeneralInfos(True)
        lockSecteur("ClientGenInfo-" & noClient & "-", False)
        myMainWin.StatusText = Me.Text & " : Informations de base enregistrées"

        InternalUpdatesManager.getInstance.sendUpdate("AccountsGenInfo(" & noClient & ")")

        accountInfosModified = False
        Return "DONE"
    End Function

    Private Function saveCommunications(Optional ByVal loadList As Boolean = True) As String
        If btnAddComm.Enabled = False And modifComm.Enabled = False Then Return "DONE"

        If commSujet.Text = "" Then MessageBox.Show("Veuillez entrer un objet", "Information manquante") : Exit Function
        If commDeA.SelectedIndex < 0 Then MessageBox.Show("Veuillez sélectionner une provenance ou une destination (Champ À | De)", "Information manquante") : Exit Function

        If btnAddComm.Enabled = True Then
            addSaveCommunication(, loadList)
        Else
            addSaveCommunication(False, loadList)
        End If

        Return "DONE"
    End Function

    Private Function addEquipment() As String
        If dossier.SelectedIndex = -1 Then Exit Function

        If eNom.Text = "" Then MessageBox.Show("Veuillez sélectionner un item", "Information manquante") : Exit Function
        If eNoItem.Text = "" Then MessageBox.Show("Veuillez sélectionner un numéro d'item", "Information manquante") : Exit Function
        Dim curEquip As Equipment = eNom.SelectedItem
        Dim SNoTRP(), typeFacturation As String
        SNoTRP = System.Text.RegularExpressions.Regex.Split(etrp.Text, " \(")
        Dim noTRP As Integer = SNoTRP(1).Substring(0, SNoTRP(1).Length - 1)
        Dim i As Short
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim newItem As Item

        If eType1.Checked = True Then
            If eProfit.Text = "0" And curEquip.itemSoldAmount <> 0 Then If MessageBox.Show("Êtes-vous certain de charger 0$ pour la vente de cet équipement ?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Function

            typeFacturation = "Vente"
            newItem = New ItemSold()
        ElseIf eType2.Checked = True Then
            If eDuree.Text = "Date de retour invalide" Then MessageBox.Show("Veuillez sélectionner une date de retour valide, donc supérieur à aujourd'hui", "Information manquante") : Exit Function

            typeFacturation = "Prêt"
            newItem = New ItemBorrowed
            With CType(newItem, ItemBorrowed)
                .depositAmount = Double.Parse(Me.eDepot.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                .cost = Double.Parse(Me.ePret.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                .amountRefund = Double.Parse(Me.eRefund.Text.Replace(",", Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                .returningDate = New Date(anretour.Text, moisretour.Text, jourretour.Text)
                .description = eNotes.Text
                .isRefund = eRefunded.Checked
                .isReturned = eReturned.Checked
                .verifiedByTRP = eVerified.Checked
            End With
        Else
            MessageBox.Show("Veuillez sélectionner Vente ou Prêt", "Information manquante")
            If selfOpened = True Then DBLinker.getInstance().dbConnected = False
            Exit Function
        End If

        newItem.dateTime = Date.Today
        newItem.profitAmount = eProfit.Text
        newItem.noClient = Me.noClient
        newItem.noEquipment = curEquip.noEquipment
        newItem.noFolder = currentNoFolder
        newItem.noItem = eNoItem.Text
        newItem.noTRP = noTRP

        newItem.saveData()

        Dim myKeys(1) As Object
        myKeys(0) = typeFacturation
        myKeys(1) = newItem.noItemBoth

        With listEquipement
            .ClearSelection()
            For i = 0 To dsVentePret.Tables("DataTable1").DefaultView.Count - 1
                If .Rows(i).Cells("Numéro de l'item").Value = myKeys(1) And .Rows(i).Cells("Type").Value = myKeys(0) Then
                    .Rows(i).Selected = True
                    .currentCell = .Rows(i).Cells(0)
                    selectingEquipment()
                    Exit For
                End If
            Next i
        End With

        If eType1.Checked = True Then
            myMainWin.StatusText = Me.Text & " : Équipement vendu (" & myKeys(1) & ")"
        ElseIf eType2.Checked = True Then
            myMainWin.StatusText = Me.Text & " : Équipement prêté (" & myKeys(1) & ")"
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        Return "DONE"
    End Function
#End Region

#Region "Window Actions"
    Private Sub choosephoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles choosephoto.Click
        photo.Image = Nothing
        If Not myPhoto Is Nothing Then myPhoto.Dispose()
        If importer("photo", "Clients\" & noClient, 7, "JPG;BMP;GIF", , True) = "" Then
            Dim photoPath As String = appPath & bar(appPath) & "Clients\" & noClient & "\photo"
            If IO.File.Exists(photoPath) Then
                myPhoto = New Bitmap(photoPath)
                photo.Image = myPhoto
            Else
                photo.Image = DrawingManager.getInstance.getImage("NoPhoto.jpg")
            End If
            Exit Sub
        End If

        If IO.File.Exists(appPath & bar(appPath) & "Clients\" & noClient & "\photo") = True Then
            DBLinker.getInstance.updateBinary("InfoClients", "Photo", readFile("Clients\" & noClient & "\photo", True), "NoClient", Me.noClient)
            myPhoto = New Bitmap(appPath & bar(appPath) & "Clients\" & noClient & "\photo")
            photo.Image = myPhoto
            enleverphoto.Enabled = True
        End If
    End Sub

    Private Sub loadPhoto()
        myPhoto = New Bitmap(appPath & bar(appPath) & "Clients\" & noClient & "\photo")
        photo.Image = myPhoto
    End Sub

    Private Sub enleverphoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles enleverphoto.Click
        Dim photoPath As String = appPath & bar(appPath) & "Clients\" & noClient & "\photo"
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer la photo du compte client ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        photo.Image = Nothing
        myPhoto.Dispose()

        If fileInUse(photoPath) Then
            loadPhoto()
            MessageBox.Show("Impossible de supprimer la photo de ce client, car un autre utilisateur visionne le compte et la photo du même coup", "Photo en cours d'utilisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If IO.File.Exists(photoPath) = True Then
            Try
                IO.File.Delete(photoPath)
            Catch ex As IO.IOException
                loadPhoto()
                MessageBox.Show("Une erreur est survenue.Veuillez fermer et rouvrir le compte, puis réessayez.", "Suppresion de la photo")
                choosephoto.Enabled = False
                enleverphoto.Enabled = False
                Exit Sub
            Catch ex As Exception
                loadPhoto()
                addErrorLog(ex)
                MessageBox.Show("Une erreur est survenue.Veuillez fermer et rouvrir le compte, puis réessayez.", "Suppresion de la photo")
                choosephoto.Enabled = False
                enleverphoto.Enabled = False
                Exit Sub
            End Try
        End If

        photo.Image = DrawingManager.getInstance.getImage("NoPhoto.jpg")
        DBLinker.getInstance.updateDB("InfoClients", "Photo=null", "NoClient", noClient, False)
        enleverphoto.Enabled = False
        myMainWin.StatusText = Me.Text & " : Photo supprimée"
    End Sub

    Private Sub btnAddAsKP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAsKP.Click
        Dim phones As String = ""
        If Telephones.Items.Count > 0 Then
            Dim myPhones() As String
            ReDim myPhones(Telephones.Items.Count - 1)
            Telephones.Items.CopyTo(myPhones, 0)
            phones = String.Join("§", myPhones)
        End If

        Comptes.addKP(nom.Text & "," & prenom.Text, adresse.Text, ville.Text, codepostal1.Text & codepostal2.Text, phones, courriel.Text, url.Text, nam.Text, "", "", "", Me, False)
    End Sub

    Private Sub reference_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles reference.MouseMove
        If reference.Tag.ToString.ToLower = "compte" Or reference.Tag.ToString.ToLower = "kp" Then
            If Not reference.Cursor Is Cursors.Hand Then reference.Cursor = Cursors.Hand
            If ToolTip1.GetToolTip(reference) <> "Simple-clique pour accéder" Then ToolTip1.SetToolTip(reference, "Simple-clique pour accéder")
        Else
            If Not reference.Cursor Is Cursors.Default Then reference.Cursor = Cursors.Default
            If ToolTip1.GetToolTip(reference) <> "" Then ToolTip1.SetToolTip(reference, "")
        End If
    End Sub

    Private Sub reference_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles reference.Click
        If reference.Cursor Is Cursors.Hand Then
            Dim isKP As Boolean = False
            If reference.Tag.ToString.ToUpper = "KP" Then isKP = True
            openAccount(reference.Lines(0), IIf(isKP, Comptes.CompteType.KP, Comptes.CompteType.Client))
        End If
    End Sub

    Private Sub modifenable_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles modifenable.Click
        If nom.ReadOnly Then
            'Droit & Accès
            If currentDroitAcces(11) = False Then
                'Message & Exit
                MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
                Exit Sub
            End If

            If lockSecteur("ClientGenInfo-" & noClient & "-", True, "Informations de base d'un client") = True Then
                lockGeneralInfos(False)
                telephones_SelectedIndexChanged(Events, eventArgs)
                OldNAM = nam.Text
            End If

            Me.modifenable.Image = imgModifSave.Images(1)
            ToolTip1.SetToolTip(Me.modifenable, "Enregistrer les informations de base du client")
        Else
            If saveGeneralInfo() = "DONE" Then
                Me.modifenable.Image = imgModifSave.Images(0)
                ToolTip1.SetToolTip(Me.modifenable, "Modifier les informations de base du client")
                Me.btnSendEmail.Enabled = Me.courriel.Text <> String.Empty
            End If
        End If
    End Sub

    Private Sub paiements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles paiements.Click
        Dim myPaiement As Payment = openUniqueWindow(New Payment(), "Effectuer le(s) paiement(s) de " & nom.Text & "," & prenom.Text)
        If myPaiement.billsLoaded = False Then myPaiement.loading(noClient, FacturationBox.DedicatedType.Client)
        myPaiement.Show()
    End Sub

    Private Sub createBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles createBill.Click
        'Droit & Accès
        If currentDroitAcces(76) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Nouvelle facture." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If dossier.Items.Count = 0 Then
            MessageBox.Show("Il doit exister au moins un dossier pour créer une nouvelle facture", "Impossible d'ajouter une nouvelle facture")
            Exit Sub
        End If

        Dim myMultiChoice As New multichoice
        Dim folders(dossier.Items.Count - 1) As String
        dossier.Items.CopyTo(folders, 0)
        Dim myChoice As String = myMultiChoice.GetChoice("Veuillez choisir le dossier", String.Join("§", folders), , "§")
        If myChoice.StartsWith("ERROR") Or myChoice = "" Then Exit Sub

        Dim sNoFolder() As String = myChoice.Split(New Char() {"-"})
        Dim noFolder As Integer = sNoFolder(0)

        Dim myAddBill As addBill = openUniqueWindow(New addBill())
        myAddBill.Show()
        myAddBill.mode = addBill.Modes.Client
        myAddBill.setClient(noClient, nom.Text & "," & prenom.Text, noFolder, ctlFolderInfos.siteLesion, ctlFolderInfos.service)
    End Sub
#End Region

#Region "General Dossier Actions"
    Private Sub folderTextes_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles folderTexts.SelectionChangeCommitted
        setFolderText()
    End Sub

    Private Sub ongletsdossier_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ongletsdossier.SelectedIndexChanged
        'Active / désactive les boutons nécessaire au bon sous-secteur du dossier
        Select Case ongletsdossier.SelectedIndex
            Case 1 'Infos
                modifsaveEquip.Visible = False
                modifsaveTextes.Visible = False
                modifsaveDossierInfos.Visible = True
                modelebtn1.Enabled = False
                maximise1.Enabled = False
                folderTexts.Enabled = False
            Case 2 'Textes
                folderText.focus()
                modifsaveEquip.Visible = False
                modifsaveTextes.Visible = True
                modifsaveDossierInfos.Visible = False
                modelebtn1.Enabled = allowFolderTextsModification AndAlso folderText.Editing
                maximise1.Enabled = modifsaveTextes.Enabled
                folderTexts.Enabled = True
            Case 0 'Équipement
                modifsaveEquip.Visible = True
                modifsaveTextes.Visible = False
                modifsaveDossierInfos.Visible = False
                modelebtn1.Enabled = False
                maximise1.Enabled = False
                folderTexts.Enabled = False
        End Select
    End Sub

    Private Sub dossier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dossier.SelectedIndexChanged
        If loadingDossiersDueToInfos Then Exit Sub

        Dim noFolder() As String = dossier.GetItemText(dossier.SelectedItem).Split(New Char() {"-"})
        If noFolder Is Nothing Then Exit Sub
        If noFolder(0) = "" Then Exit Sub
        If noFolder(0) = currentNoFolder Then Exit Sub

        'Vérifie si le dossier précédemment sélectionné a été modifié et demande pour enregistrer
        If currentNoFolder <> 0 AndAlso (folderEquipmentModified = True Or ctlFolderInfos.isFolderModified = True Or folderTextsModified) Then
            If askSavingQuestion(Sectors.AccountFolderEquipment Or Sectors.AccountFolderInfos Or Sectors.AccountFolderTexts) Then
                If saveFolder() <> "Saved" Then dossier.SelectedIndex = Me.dossier.FindString(currentNoFolder & "-") : Exit Sub
            End If
        End If
        If currentNoFolder <> 0 Then
            If allowFolderEquipmentModification Then desactivateFolderEquipment()
            If allowFolderInfosModification Then desactivateFolderInfos()
            If allowFolderTextsModification Then desactivateFolderTexts()
        End If

        currentNoFolder = noFolder(0)
        If loadingDossier = True Then loadFolder(noFolder(0))
        loadFolderHistory(noFolder(0))
    End Sub

    Private Sub dossier_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles dossier.MouseDown
        Dim button As Short = eventArgs.Button \ &H100000
        Dim shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Double = eventArgs.X
        Dim y As Double = eventArgs.Y
        Dim i As Integer
        Dim selfOpened As Boolean = False
        If dossier.SelectedIndex <> -1 And button = 2 Then
            If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True
            Dim dossierActif(,) As String = DBLinker.getInstance.readDB("InfoFolders", "StatutOuvert, dbo.GetDossierCurrentDemandeStatut(NoFolder)", "WHERE (NoFolder=" & currentNoFolder & ");")

            CType(menudossierstatut(0), MenuItem).Checked = CBool(dossierActif(0, 0))
            CType(menudossierstatut(1), MenuItem).Checked = Not CBool(dossierActif(0, 0))
            menuStatutDemande.Visible = True
            menuDemandeAcceptee.Checked = False
            menuDemandeRefusee.Checked = False
            menuDemandeNonTransmise.Checked = False
            menuDemandeEnvoyee.Checked = False
            Select Case dossierActif(1, 0)
                Case 22
                    menuDemandeNonTransmise.Checked = True
                Case 23
                    menuDemandeEnvoyee.Checked = True
                Case 24
                    menuDemandeRefusee.Checked = True
                Case 25
                    menuDemandeAcceptee.Checked = True
                Case Else
                    menuStatutDemande.Visible = False
            End Select

            menudelete.Enabled = True
            If currentUserName <> "Administrateur" Then
                Dim visiteCount() As String = DBLinker.getInstance.readOneDBField("InfoVisites", "NoStatut", "WHERE (NoFolder=" & currentNoFolder & ");")
                If visiteCount Is Nothing OrElse visiteCount.Length = 0 Then
                    menudelete.Enabled = True
                Else
                    For i = 0 To visiteCount.Length - 1
                        If visiteCount(i) <> 3 Then
                            menudelete.Enabled = False
                            Exit For
                        End If
                    Next i
                End If
            End If
            If selfOpened = True Then DBLinker.getInstance().dbConnected = False

            menuviewmodifclientsdossier.Show(dossier, New Point(eventArgs.X, eventArgs.Y))
        End If
    End Sub

    Private Sub menudelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menudelete.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce dossier ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim delReturn As String = deleteFolder(Me.noClient, currentNoFolder)
        If delReturn <> "" Then MessageBox.Show(delReturn, "Impossible de supprimer le dossier")
    End Sub

    Private Sub maximise_s_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles maximise2.Click, maximise1.Click
        Dim texteBoxTitle As String
        Dim contenu As String = ""
        If eventSender.name = "maximise2" Then
            contenu = antecedants.getHTML
            lastTextBoxSel = antecedants
            texteBoxTitle = "Bilan de santé"
        Else
            If folderTexts.SelectedIndex = -1 Then
                MessageBox.Show("Veuillez sélectionner un dossier et un texte avant de maximiser. Merci!", "Aucun texte sélectionné")
                Exit Sub
            End If
            contenu = folderText.getHTML
            lastTextBoxSel = folderText
            texteBoxTitle = folderTexts.Text
        End If

        'Ouvre la fenêtre maximiser avec le contenu de la boîte de texte web en cours
        Try
            TextWindow.getInstance.currentData = contenu
            TextWindow.getInstance.Text = "Visualisation : " & Me.Text & " : " & texteBoxTitle
            TextWindow.getInstance.isLocked = Not lastTextBoxSel.Editing
            TextWindow.getInstance.isHTML = True
            Dim mySel As String = TextWindow.getInstance.ShowTexteModif(lastTextBoxSel.getPos, True)

            'Recopie la modification du fichier temporaire à la boîte de texte
            If lastTextBoxSel.Editing = True And contenu <> TextWindow.getInstance.currentData Then 'ERROR HAPPENS HERE
                If eventSender.name = "maximise2" Then
                    antecedants.setHtml(TextWindow.getInstance.currentData)
                    antecedants.setPos(mySel)
                    antecedentModified = True
                Else
                    'REM_CODES
                    Dim curFT As FolderText = folderTexts.SelectedItem
                    If TextWindow.getInstance.currentData <> curFT.text Then
                        curFT.text = TextWindow.getInstance.currentData
                        folderText.setHtml(curFT.text)
                        folderTextsModified = True
                    End If
                    folderText.setPos(mySel)
                End If
            End If

            lastTextBoxSel.focus()
            lastTextBoxSel.setPos(mySel)
        Catch ex As Exception
            addErrorLog(New Exception("ctlFolderInfos.NoFolder=" = ctlFolderInfos.noFolder & vbCrLf & "Error2:LastTextBoxSel Is Nothing=" & (lastTextBoxSel Is Nothing) & vbCrLf & "textemodif.GetInstance.CurrentData Is Nothing=" & (TextWindow.getInstance.currentData Is Nothing) & vbCrLf & "Contenu Is Nothing=" & (contenu Is Nothing), ex))
        End Try
    End Sub

    Private Sub modelebtn1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles modelebtn1.Click
        Dim myContextMenu As New ContextMenu()
        lastTextBoxSel = folderText
        'REM_CODES
        Dim curFT As FolderText = folderTexts.SelectedItem
        myContextMenu = ModelsManager.getInstance.createModelsMenu(New Integer() {1, curFT.getFolderTexteType.noModelCategory}, New EventHandler(AddressOf menumodelegen_Click), New EventHandler(AddressOf menumodeleperso_Click))
        myContextMenu.Show(modelebtn1, New Point(0, 0))
    End Sub

    Private Sub modifsaveTextes_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles modifsaveTextes.Click
        If ToolTip1.GetToolTip(modifsaveTextes).StartsWith("Modifier") Then
            activateFolderTexts()
        Else
            saveFolderTexts()
        End If
    End Sub

    Private Sub modifsaveEquip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifsaveEquip.Click
        If ToolTip1.GetToolTip(modifsaveEquip).StartsWith("Commencer") Then
            activateFolderEquipment()
        Else
            If folderEquipmentModified AndAlso askSavingQuestion(Sectors.AccountFolderEquipment) Then saveEquipment()
            desactivateFolderEquipment()
        End If
    End Sub

    Private Sub modifsaveDossierInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifsaveDossierInfos.Click
        If ToolTip1.GetToolTip(modifsaveDossierInfos).StartsWith("Modifier") Then
            activateFolderInfos()
        Else
            saveFolderInfos()
        End If
    End Sub

    Private Sub menuDossierFlagged_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDossierFlagged.Click
        menuDossierFlagged.Checked = Not menuDossierFlagged.Checked
        DBLinker.getInstance.updateDB("InfoFolders", "Flagged='" & menuDossierFlagged.Checked & "'", "NoFolder", currentNoFolder, False)
    End Sub

    Private Sub menudossierstatut_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles menudossierstatut.click
        If menudossierstatut(index).Checked = True Then Exit Sub

        Dim newStatus As FolderPossibleStatuses = IIf(menudossierstatut(index).Text = "Inactif", FolderPossibleStatuses.Inactive, FolderPossibleStatuses.Active)
        Dim oldStatus As FolderPossibleStatuses = IIf(newStatus = FolderPossibleStatuses.Active, FolderPossibleStatuses.Inactive, FolderPossibleStatuses.Active)

        If newStatus = FolderPossibleStatuses.Inactive Then
            If MessageBox.Show("Êtes-vous certain de vouloir désactiver ce dossier ?" & vbCrLf & "Cette procédure éliminera tous les rendez-vous futurs.", "Confirmation de désactivation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
        Else
            If MessageBox.Show("Êtes-vous certain de vouloir activer ce dossier ?", "Confirmation d'activation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
        End If

        If Not Accounts.Clients.Folders.ClientFolder.changeStatus(oldStatus, newStatus, noClient, currentNoFolder) Then Exit Sub

        If newStatus = FolderPossibleStatuses.Inactive Then
            menustatutFerme.Checked = True
            menustatutOuvert.Checked = False
        Else
            menustatutFerme.Checked = False
            menustatutOuvert.Checked = True
        End If

        Dim dossierTitle() As String = dossier.GetItemText(dossier.SelectedItem).Split(New Char() {"("})
        dossier.Items(dossier.SelectedIndex) = dossierTitle(0) & "(" & dossierTitle(1) & "(" & dossierTitle(2) & "(" & menudossierstatut(index).Text & ")"
        loadFolderInfos(currentNoFolder)
    End Sub

    Private Sub ongletsclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ongletsclient.SelectedIndexChanged
        StartStopCommChanging.Visible = False
        StartStopBillChanging.Visible = False

        If ongletsclient.SelectedIndex = 1 Then
            StartStopCommChanging.Visible = True
        ElseIf ongletsclient.SelectedIndex = 2 Then
            StartStopBillChanging.Visible = True
        End If
    End Sub

    Private Sub menuGenFolderRapport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuGenFolderRapport.Click
        startClientReportGeneration("Dossier détaillé")
    End Sub

    Private Sub menuTransferFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuTransferFolder.Click
        Dim errMsg As String = transferFolder(Me.noClient, currentNoFolder)
        If errMsg <> "" Then MessageBox.Show(errMsg, "Impossible de transférer le dossier", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    End Sub

    Private Sub menuDemandeNonTransmise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDemandeNonTransmise.Click
        changeAuthorizationProcess(sender, 22)
    End Sub

    Private Sub menuDemandeEnvoyee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDemandeEnvoyee.Click
        changeAuthorizationProcess(sender, 23)
    End Sub

    Private Sub menuDemandeAcceptee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDemandeAcceptee.Click
        changeAuthorizationProcess(sender, 25)
    End Sub

    Private Sub menuDemandeRefusee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDemandeRefusee.Click
        changeAuthorizationProcess(sender, 24)
    End Sub

    Private Sub changeAuthorizationProcess(ByVal sender As MenuItem, ByVal noAction As Integer)
        If sender.Checked = True Then Exit Sub

        'Droit & Accès
        If currentDroitAcces(67) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à la modification d'une demande d'autorisation." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        'Demande pour le commentaires et le nom de l'agent
        Dim comments As String = ""
        Dim nomAgent As String = ""
        Dim myInputBoxPlus As New InputBoxPlus
        If noAction <> 22 Then nomAgent = myInputBoxPlus.Prompt("Veuillez entrer le nom de l'agent avec qui vous avez parlé", "Nom de l'agent")
        comments = myInputBoxPlus.Prompt("Veuillez entrer un commentaire pour la changement de statut de la demande d'autorisation", "Commentaire d'une demande d'autorisation")
        If PreferencesManager.getGeneralPreferences()("DemandeAuthorisationCommentsRequired") = True And comments = "" Then
            MessageBox.Show("Le commentaire est obligatoire. Veuillez recommencer s'il vous plait.", "Commentaire obligatoire")
            Exit Sub
        End If

        menuDemandeAcceptee.Checked = False
        menuDemandeEnvoyee.Checked = False
        menuDemandeNonTransmise.Checked = False
        menuDemandeRefusee.Checked = False

        sender.Checked = True

        DBLinker.getInstance.writeDB("DemandesAuthorisations", "NoClient,NoFolder,NoAction,NoUser,Commentaires,NomAgent", Me.noClient & "," & currentNoFolder & "," & noAction & "," & ConnectionsManager.currentUser & ",'" & comments.Replace("'", "''") & "','" & nomAgent.Replace("'", "''") & "'")
    End Sub

    Private Sub menuSuiviDemande_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSuiviDemande.Click
        startClientReportGeneration("Demande d'autorisation d'un dossier")
    End Sub

    Private Sub startClientReportGeneration(ByVal rapportTitle As String)
        Dim tpFilter As New FilteringComposite
        Dim noClientFilter As New FilteringNoClient
        noClientFilter.noClient = Me.noClient
        noClientFilter.noFolder = currentNoFolder
        noClientFilter.clientFullName = nom.Text & "," & prenom.Text
        tpFilter.add(noClientFilter)

        ReportGeneration.startRapportGeneration(rapportTitle, tpFilter)
    End Sub

    Private Sub menuShowDossierHisto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuShowDossierHisto.Click
        If menuShowDossierHisto.Checked Then
            folderHistoryFrame.Visible = False
            menuShowDossierHisto.Checked = False
        Else
            folderHistoryFrame.Visible = True
            menuShowDossierHisto.Checked = True

            loadFolderHistory(currentNoFolder)
        End If
    End Sub

    Private Sub fermerDossierHisto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FermerDossierHisto.Click
        folderHistoryFrame.Visible = False
        menuShowDossierHisto.Checked = False
    End Sub

    Private Sub loadFolderHistory(ByVal noFolder As Integer)
        If folderHistoryFrame.Visible = False Then Exit Sub

        folderHistory.DataSource = DBLinker.getInstance.readDBForGrid("SELECT DateHeureCreation, NomAction, Utilisateurs.Nom +','+Utilisateurs.Prenom + ' (' + CAST(Utilisateurs.NoUser AS VARCHAR(5)) + ')' AS ByUser, Comments AS Raison FROM StatFolders INNER JOIN ListeAction ON ListeAction.NoAction = StatFolders.NoAction LEFT JOIN Utilisateurs ON Utilisateurs.NoUser = StatFolders.NoUser WHERE NoFolder=" & noFolder & " ORDER BY DateHeureCreation").Tables(0)
    End Sub
#End Region

#Region "Bilan de santé Actions"

    Private Sub antecedants_PageLoaded() Handles antecedants.pageLoaded
        saveantecedent.Enabled = True
        maximise2.Enabled = True
    End Sub

    Private Sub antecedants_AddedImage_Link() Handles antecedants.addedImage, antecedants.addedLink
        antecedentModified = True
    End Sub

    Private Shadows Sub antecedants_TextChanged(ByVal theText As String) Handles antecedants.textChanged
        antecedentModified = True
    End Sub

    Private Sub antecedants_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles antecedants.MouseDown
        lastTextBoxSel = sender
    End Sub

    Private Sub modelebtn2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles modelebtn2.Click
        lastTextBoxSel = antecedants
        Dim myContextMenu As ContextMenu = ModelsManager.getInstance.createModelsMenu(New Integer() {1, 2}, New EventHandler(AddressOf menumodelegen_Click), New EventHandler(AddressOf menumodeleperso_Click))
        myContextMenu.Show(modelebtn2, New Point(modelebtn2.Width, 0))
    End Sub

    Private Sub saveantecedent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles saveantecedent.Click
        saveClientHistory()
    End Sub
#End Region

#Region "TexteBoxes excluding bilan de santé Actions (except for event AddingLink)"

    Private Sub folderTexte_AddedImage_Link() Handles folderText.addedImage, folderText.addedLink
        folderTextsModified = True
    End Sub

    Private Sub textes_AddingLink(ByRef sender As WebTextControl, ByRef handled As Boolean) Handles folderText.addingLink, antecedants.addingLink
        handled = True
        Dim myChoice As Byte
        Dim myMsgBox As New MsgBox1

        'Droit & Accès
        If currentDroitAcces(53) = False And currentDroitAcces(6) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'ajouter de liens." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        ElseIf currentDroitAcces(53) = False Then
            'Force l'utilisateur a utilisé les liens internes
            myChoice = myMsgBox("Voulez ajouter un lien depuis la banque de données ou depuis les communications ?", "Ajout d'un lien", 2, "Banque de données", "Communications")
        Else
            myChoice = myMsgBox("Voulez ajouter un lien depuis la banque de données ou depuis les communications ou externe ?", "Ajout d'un lien", 3, "Banque de données", "Communications", "Lien externe")
            If myChoice = 0 Then Exit Sub
        End If

        Dim myURL As String = ""
        Dim myURLEncoded As String = ""
        Dim myTitre As String = ""

        If myChoice = 1 Then myURLEncoded = InternalDBManager.getInstance.getURLFromDB(myTitre)

        'Choix à partir des communications
        If myChoice = 2 Then
            Dim myChoices As String = ""
            Dim mySelChoice As String = ""
            Dim myFiles() As String = {}
            Dim myCommNames() As String = {}
            Dim i, n As Integer
            For i = 0 To listeCommunications.listCount - 1
                Dim myComm() As String = listeCommunications.ItemValueB(i).ToString.Split(New Char() {"§"})
                If myComm(9) <> "" AndAlso (sender Is antecedants OrElse myComm(10) = 0 OrElse dossier.Items(dossier.SelectedIndex).ToString.StartsWith(myComm(10) & " -")) Then 'S'il y a un fichier joint à la communication et que celle-ci fait partie du dossier
                    myChoices &= "§" & listeCommunications.ItemText(i)
                    ReDim Preserve myFiles(n)
                    ReDim Preserve myCommNames(n)
                    myCommNames(n) = myComm(5)
                    myFiles(n) = myComm(9)
                    n += 1
                End If
            Next i

            If myChoices <> "" Then
                myChoices = myChoices.Substring(1)
                Dim myMultiChoice As New multichoice
                mySelChoice = myMultiChoice.GetChoice("Sélectionner la communication", myChoices, "INDEX", "§", False)
                If mySelChoice < 0 Then
                    mySelChoice = ""
                    Exit Sub
                Else
                    myTitre = myCommNames(mySelChoice)
                    mySelChoice = myFiles(mySelChoice)
                    If mySelChoice.StartsWith("DB|") = False Then mySelChoice = mySelChoice.Substring(0, mySelChoice.IndexOf("|")) & "|Clients\" & noClient & "\Comm\" & mySelChoice.Substring(mySelChoice.Substring(0, mySelChoice.IndexOf("|")).Length + 1)
                    myURLEncoded = WebTextControl.PROTOCOL_CLINICA & Web.HttpUtility.UrlEncode(mySelChoice)
                End If
            Else
                MessageBox.Show("Il n'existe aucune communication ayant une pièce jointe et étant lié à ce dossier ou lié à aucun dossier", "Ajout d'un lien")
            End If
        End If

        'Demander l'adresse directement du lien
        If myChoice = 3 Then
            myURL = folderText.getURLFromInput()
            If myURL <> "" Then myURLEncoded = "http://" & Web.HttpUtility.UrlEncode(myURL.Substring(7))
        End If

        CType(sender, WebTextControl).addLink(myURLEncoded, myURL, myTitre)
    End Sub
    Private Sub folderTexte_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles folderText.MouseDown
        lastTextBoxSel = sender
    End Sub

    Private Sub folderTexte_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles folderText.Enter
        lastTextBoxSel = sender
        If folderText.Editing Then modelebtn1.Enabled = True
    End Sub

    Private Sub folderTexte_PageLoaded() Handles folderText.pageLoaded
        If dossier.Items.Count > 0 Then modifsaveTextes.Enabled = True

        If ongletsdossier.SelectedIndex = 2 Then maximise1.Enabled = True
    End Sub

    ''' <summary>
    ''' Save current editing text and Set the folder text object text and position to the one currently selected
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setFolderText()
        If folderText.Editing = True And lastNoFolderText <> 0 Then
            Dim curTextFound As Boolean = False
            'Search if current text in modification still exists, if so keep the text and position
            For i As Integer = 0 To folderTexts.Items.Count - 1
                'REM_CODES
                Dim curFT As FolderText = folderTexts.Items(i)
                If curFT.noFolderTexte = lastNoFolderText Then
                    curFT.textPosition = folderText.getPos()
                    curFT.text = folderText.getHTML
                    curTextFound = True
                    Exit For
                End If
            Next i

            'If text not found, advise user that text is copied into the clipboard
            If curTextFound = False Then
                MessageBox.Show("Le texte que vous êtes en train de modifier a été supprimé par l'action d'un autre utilisateur." & vbCrLf & "Votre texte a été copié et vous pouvez donc le recoller autre part en attendant de vérifier la cause." & vbCrLf & "Si vous copiez d'autre texte, celui-ci sera probablement perdu.", "Texte supprimé", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Clipboard.SetData(System.Windows.Forms.DataFormats.Html, folderText.getHTML)
            End If
        End If

        If folderTexts.SelectedIndex = -1 Then
            folderText.setHtml("", True)
        Else
            'REM_CODES
            Dim curFT As FolderText = folderTexts.SelectedItem
            If folderText.Editing Then
                Try
                    folderText.setHtml(curFT.text.Replace(vbCrLf, ""), True)
                Catch
                    folderText.setHtml("", True)
                End Try

                folderText.setPos(curFT.textPosition)
                folderText.focus()
            Else
                Try
                    folderText.setHtml("<HTML><BODY>" & curFT.text & "</BODY></HTML>", True)
                    folderText.setPos(curFT.textPosition)
                    'folderText.focus()
                Catch ex As Exception
                    Throw ex
                End Try
            End If
        End If

        lastNoFolderText = CType(folderTexts.SelectedItem, FolderText).noFolderTexte
    End Sub

    Private Sub folderTextes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles folderTexts.SelectedIndexChanged
        If folderTexts.SelectedItem Is Nothing OrElse folderText Is Nothing Then Exit Sub
        If lastNoFolderText = CType(folderTexts.SelectedItem, FolderText).noFolderTexte AndAlso folderText.Editing = True Then
            Exit Sub
        End If

        setFolderText()
    End Sub

    Private Shadows Sub folderTexte_TextChanged(ByVal theText As String) Handles folderText.textChanged
        folderTextsModified = True
    End Sub
#End Region

#Region "Equipement Actions"
    Private Sub backPret_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles backPret.Click
        If listEquipement.currentRow.Index < 0 Then MessageBox.Show("Veuillez sélectionner un équipement", "Aucune sélection") : Exit Sub
        If folderEquipmentModified Then MessageBox.Show("Veuillez enregistrer avant de faire le retour de prêt. Merci!", "Retour du prêt", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

        Dim curPret As ItemBorrowed = listEquipement.Tag
        Dim myFacture As Bill = curPret.curBill
        If myFacture.getBillTotal <> myFacture.getBillPaymentTotal Then MessageBox.Show("Vous devez avoir pris le paiement en entier avant de retourner le prêt", "Prêt non payé", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

        If MessageBox.Show("Êtes-vous sûr de retourner cet équipement ?", "Retour d'équipement", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim createReceipt As Boolean = False
        If curPret.curBill.amountBilledToClient > 0 AndAlso MessageBox.Show("Désirez-vous émettre le reçu ?" & vbCrLf & "(Cette opération bloque la modification de la facture)", "Création du reçu", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            createReceipt = True
        End If

        If curPret.returnItem(createReceipt) = False Then Exit Sub

        myMainWin.StatusText = Me.Text & " : Équipement retourné (" & listEquipement.Rows(listEquipement.currentRow.Index).Cells(1).Value & ")"

        eReturned.Checked = True
        eRefunded.Checked = True
        backPret.Enabled = False
        folderEquipmentModified = False
    End Sub

    Private Sub vider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles vider.Click
        If askSavingQuestion(Sectors.AccountFolderEquipment) Then If saveEquipment() = "" Then Exit Sub

        resetFolderEquipmentFields()
        curItem = Nothing
    End Sub

    Private Sub submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles submit.Click
        saveEquipment()
    End Sub

    Private Sub modif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modif.Click
        saveEquipment()
    End Sub

    Private Sub createEquipmentReceipt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles createEquipmentReceipt.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir émettre le reçu ?" & vbCrLf & "(Cette opération bloque la modification de la facture)", "Création du reçu", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim curItem As Item = listEquipement.Tag
        curItem.curBill.generateReceipt("C")

        afterSelectEquipmentActivation(curItem.curBill.getBillPaymentTotal())
    End Sub

    Private Sub delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click
        If dossier.SelectedIndex = -1 Then Exit Sub
        If eNom.Text = "" Then Exit Sub
        If MessageBox.Show("Êtes-vous certain de vouloir supprimer cet équipement ?", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Dim curItem As Item = listEquipement.Tag
        curItem.delete()

        myMainWin.StatusText = Me.Text & " : Équipement supprimé (" & curItem.toString & ")"
        resetFolderEquipmentFields()
        curItem = Nothing
    End Sub

    Private Sub listEquipement_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles listEquipement.MouseUp
        If dossier.SelectedIndex = -1 Then Exit Sub

        If Not e.GetType Is EventArgs.Empty Then
            Select Case listEquipement.HitTest(e.X, e.Y).Type
                Case DataGrid.HitTestType.None
                    If allowFolderEquipmentModification = True Then vider_Click(Me, EventArgs.Empty)
                    Exit Sub

                Case DataGrid.HitTestType.Caption, DataGrid.HitTestType.ColumnResize, DataGrid.HitTestType.ParentRows, DataGrid.HitTestType.RowResize
                    Exit Sub
            End Select
        End If

        selectingEquipment()
    End Sub

    Private Sub selectingEquipment()
        Dim noFolder As Integer = currentNoFolder
        curItem = Nothing

        With listEquipement
            If .currentRow Is Nothing Then Exit Sub
            Dim rowIndex As Integer = .currentRow.Index
            If rowIndex < 0 Then Exit Sub

            Dim myKeys(1) As Object
            myKeys(0) = .Rows(rowIndex).Cells("Type").Value
            myKeys(1) = .Rows(rowIndex).Cells("Numéro de l'item").Value
            Dim curItemData As New DBItemableData(dsVentePret.Tables("DataTable1").Rows.Find(myKeys))
            If .Rows(rowIndex).Cells("Type").Value.ToString = "Prêt" Then
                curItem = New ItemBorrowed(curItemData)
            Else
                curItem = New ItemSold(curItemData)
            End If

            If Not .Tag Is Nothing AndAlso CType(.Tag, Item).noItemBoth = curItem.noItemBoth AndAlso (curItem.GetType.Equals(.Tag.GetType())) Then
                Exit Sub
            Else
                If askSavingQuestion(Sectors.AccountFolderEquipment) Then If saveEquipment() = "" Then Exit Sub
            End If
            .Tag = curItem
            .Rows(rowIndex).Selected = True
        End With

        resetFolderEquipmentFields(, True)

        updatePret = False

        eNom.SelectedItem = curItem.getEquipement() 'This has to be before the rest of loading, because it's resting equipement

        If TypeOf curItem Is ItemBorrowed Then
            With CType(curItem, ItemBorrowed)
                anretour.Text = .returningDate.Year
                moisretour.Text = addZeros(.returningDate.Month.ToString, 2)
                jourretour.Text = addZeros(.returningDate.Day.ToString, 2)
                eDepot.Text = .depositAmount
                eDepot.forceManaging()
                ePret.Text = .cost
                ePret.forceManaging()
                eRefund.Text = .amountRefund
                eRefund.forceManaging()
                eVerified.Checked = .verifiedByTRP
                eReturned.Checked = .isReturned
                eRefunded.Checked = .isRefund
                eNotes.Text = .description
                eType2.Checked = True
            End With
        Else
            eType1.Checked = True
        End If

        Dim eTherapeute As Integer = etrp.FindStringExact(UsersManager.getInstance.getUser(curItem.noTRP).toString())
        If eTherapeute <> -1 Then etrp.SelectedIndex = eTherapeute
        eDate.Text = DateFormat.getTextDate(curItem.dateTime)

        afterSelectEquipmentActivation(curItem.curBill.getBillPaymentTotal())

        updatePret = True

        eNoItem.Text = curItem.noItem 'After disabling the ENoItem object, because it's changing its dropdownstyle
        folderEquipmentModified = False
    End Sub

    Private Sub afterSelectEquipmentActivation(ByVal montantPaiementTotal As Double)
        lockEquipment(True)

        If allowFolderEquipmentModification = False Then
            delete.Enabled = False
            modif.Enabled = False
            createEquipmentReceipt.Enabled = False
            backPret.Enabled = False
            vider.Enabled = False
            eDepot.ReadOnly = True
        Else
            Try
                eType_CheckedChanged(Me, EventArgs.Empty)
                eType1.Enabled = False
                eType2.Enabled = False
                vider.Enabled = True
                modif.Enabled = True
                createEquipmentReceipt.Enabled = True
                If montantPaiementTotal = 0 Then
                    delete.Enabled = True
                    If eType2.Checked = False Then
                        eProfit.ReadOnly = False
                        eProfit_TextChanged(listEquipement, EventArgs.Empty)
                    Else
                        eDepot.ReadOnly = False
                        ePret.ReadOnly = False
                        anretour.ReadOnly = False
                        moisretour.ReadOnly = False
                        jourretour.ReadOnly = False
                        eDuree.ReadOnly = False
                    End If
                Else
                    delete.Enabled = False
                    eProfit.ReadOnly = True
                    eDepot.ReadOnly = True
                    ePret.ReadOnly = True

                    Dim blockReturnDate As Boolean = True
                    If TypeOf listEquipement.Tag Is ItemBorrowed Then
                        Dim borrowedItem As ItemBorrowed = listEquipement.Tag
                        Dim equipment As Equipment = borrowedItem.getEquipement()
                        blockReturnDate = borrowedItem.costRepetitionBy <> equipment.costAmountFrequency.Unique AndAlso (borrowedItem.cost <> 0 OrElse borrowedItem.costAmountBy > 0)
                    End If
                    anretour.ReadOnly = blockReturnDate
                    moisretour.ReadOnly = blockReturnDate
                    jourretour.ReadOnly = blockReturnDate
                    eDuree.ReadOnly = blockReturnDate
                End If
                With listEquipement
                    If TypeOf listEquipement.Tag Is ItemBorrowed Then
                        If eReturned.Checked = False Then
                            backPret.Enabled = True
                        Else
                            delete.Enabled = False
                            eProfit.ReadOnly = True
                            eDepot.ReadOnly = True
                            ePret.ReadOnly = True
                            anretour.ReadOnly = True
                            moisretour.ReadOnly = True
                            jourretour.ReadOnly = True
                            eDuree.ReadOnly = True
                        End If
                    Else
                        backPret.Enabled = False
                    End If
                    eProfit.Enabled = True
                End With
            Catch
            End Try
        End If

        If listEquipement.Tag IsNot Nothing Then
            With CType(listEquipement.Tag, Item)
                eProfit.Text = .profitAmount
                eProfit.forceManaging()
                eTotal.Text = .getTotal()

                If createEquipmentReceipt.Enabled Then createEquipmentReceipt.Enabled = .curBill.isReceiptToDo("C")
            End With
        End If

        eNoItem.DropDownStyle = If(eNoItem.ReadOnly, ComboBoxStyle.DropDown, ComboBoxStyle.DropDownList)

        folderEquipmentModified = False
    End Sub
#End Region

#Region "Communications Actions"
    Private Sub viderComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles viderComm.Click
        If (modifComm.Enabled = True Or btnAddComm.Enabled = True) AndAlso askSavingQuestion(Sectors.AccountCommunications) AndAlso saveCommunications() = "" Then Exit Sub

        commType1.Checked = False
        commType2.Checked = False
        lblCommDeA.Text = "À | De :"
        commDeA.SelectedIndex = -1
        commSujet.Text = ""
        commCategorie.Text = ""
        commDate.Text = DateFormat.getTextDate(Date.Today)
        CommRemarques.Text = ""
        commUser.Text = UsersManager.currentUser.toString()
        If dossiersClist.Items.Count > 0 Then dossiersClist.SelectedIndex = 0
        If listeCommunications.selected <> -1 Then commModified = False : listeCommunications.selected = -1

        'Activation/Désactivation
        If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("C") = False Then
            selectKeyPeople.Enabled = True
            selectCommDate.Enabled = True
            commType1.Enabled = True
            commType2.Enabled = True
        End If
        btnAddComm.Enabled = False
        importComm.Enabled = False
        modifComm.Enabled = False
        delComm.Enabled = False
        commModified = False
    End Sub

    Private Sub btnAddComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComm.Click
        If commType1.Checked = False And commType2.Checked = False Then MessageBox.Show("Veuillez sélectionner Envoi ou Réception", "Information manquante") : Exit Sub

        saveCommunications()
    End Sub

    Private Sub importComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles importComm.Click
        If isCommunicationInUse() Then MessageBox.Show("La communication est présentement en cours d'utilisation. Veuillez patienter un petit moment et réessayer.", "Communication en cours d'utilisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

        If commType1.Checked = True Then
            menuviewmodifclientscommunications.Show(importComm, New Point(0, 0))
        Else
            importCommunication()
        End If
    End Sub

    Private Sub modifComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifComm.Click
        saveCommunications()
    End Sub

    Private Sub delComm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delComm.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette communication ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub


        Dim myComm() As String = listeCommunications.ItemValueB(listeCommunications.selected).Split(New Char() {"§"})

        'Delete attached file or break if it is in use
        If myComm(9) <> "" Then
            Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)

            If sMyComm(0) = "FILE" AndAlso IO.File.Exists(appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & sMyComm(1)) Then
                If fileInUse(appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & sMyComm(1)) Then
                    MessageBox.Show("Impossible du supprimer la communication, car le fichier attaché est en cours d'utilisation", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Exit Sub
                End If
                IO.File.Delete(appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & sMyComm(1))
            End If
        End If

        DBLinker.getInstance.delDB("Communications", "NoCommunication", listeCommunications.ItemValueA(listeCommunications.selected), False)

        myMainWin.StatusText = Me.Text & " : Communication """ & commSujet.Text & """ supprimée"
        InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunications(" & noClient & "," & False & ")")
    End Sub

    Private Sub menuImportFromDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuImportFromDB.Click
        Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB())
        mySearchDB.from = commDate
        mySearchDB.Visible = False
        mySearchDB.selectedCat = "Généraux"
        mySearchDB.useWinAsSelection = True
        mySearchDB.MdiParent = Nothing
        mySearchDB.StartPosition = FormStartPosition.CenterScreen
        mySearchDB.ShowDialog()
    End Sub

    Private Sub menuImportFromOutside_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuImportFromOutside.Click
        importCommunication()
    End Sub

    Private Sub listeCommunications_DblClick(ByVal sender As System.Object, ByVal e As CI.Controls.List.DblClickEventArgs) Handles listeCommunications.dblClick
        If e.selectedItem < 0 Or listeCommunications.selected < 0 Or e.button <> 1 Then Exit Sub

        Dim lectureSeule As Boolean = False
        If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("C") Then lectureSeule = True
        Dim myComm() As String = listeCommunications.ItemValueB(e.selectedItem).Split(New Char() {"§"})
        If myComm(9) = "" Then Exit Sub

        Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
        Dim fullPath As String = ""
        Select Case sMyComm(0)
            Case "DB"
                fullPath = "db:\" & sMyComm(1)
            Case Else
                fullPath = appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & sMyComm(1)
                If IO.File.Exists(fullPath) = False Then
                    MessageBox.Show("Le fichier demandé n'existe plus. Veuillez importer de nouveau.", "Fichier importé inexistant", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
        End Select

        TypesFilesOpener.getInstance.open(fullPath, Nothing)
    End Sub

    Private Sub listeCommunications_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles listeCommunications.KeyUp
        If e.KeyCode = 13 Then e.SuppressKeyPress = True : listeCommunications_DblClick(sender, New CI.Controls.List.DblClickEventArgs(listeCommunications.selected, 1, 0, 0))
    End Sub

    Private Sub startStopCommChanging_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartStopCommChanging.Click
        If ToolTip1.GetToolTip(StartStopCommChanging).StartsWith("C") Then
            'Droit & Accès
            If currentDroitAcces(57) = False Then
                'Message & Exit
                MessageBox.Show("Vous n'avez pas le droit de modifier ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
                Exit Sub
            End If

            If lockSecteur("ClientComm-" & noClient & "-", True, "Communications du client " & nom.Text & "," & prenom.Text) = True Then
                StartStopCommChanging.Image = imgModifSave.Images(2)
                ToolTip1.SetToolTip(StartStopCommChanging, "Arrêter la modification des communications")

                lockCommunications(False)
                If listeCommunications.selected <> -1 Then listeCommunications_SelectedChange()
            End If
        Else
            If askSavingQuestion(Sectors.AccountCommunications) AndAlso saveCommunications(True) <> "" Then Exit Sub
            commModified = False

            StartStopCommChanging.Image = imgModifSave.Images(0)
            ToolTip1.SetToolTip(StartStopCommChanging, "Commencer la modification les communications")

            lockCommunications(True)
            lockSecteur("ClientComm-" & noClient & "-", False)
        End If
    End Sub

    Private Sub enregistrerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnregistrerToolStripMenuItem.Click
        saveCommunications()
    End Sub

    Private Sub importerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImporterToolStripMenuItem.Click
        importComm_Click(sender, e)
    End Sub

    Private Sub supprimerLeFichierJointToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerLeFichierJointToolStripMenuItem.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer le fichier joint à cette communication ?", "Suppression d'un fichier joint", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim myComm() As String = listeCommunications.ItemValueB(listeCommunications.selected).Split(New Char() {"§"})
        If myComm(9) = "" Then Exit Sub

        Dim sMyComm() As String = myComm(9).Split(New Char() {"|"}, 2)
        If sMyComm(0).ToUpper = "FILE" Then
            Dim myFileToDel As String = appPath & bar(appPath) & "Clients\" & noClient & "\Comm\" & sMyComm(1)
            If IO.File.Exists(myFileToDel) Then IO.File.Delete(myFileToDel)
        End If

        Dim noCommunication As Integer = myComm(1)
        DBLinker.getInstance.updateDB("Communications", "NameOfFile=''", "NoCommunication", noCommunication, False)
        InternalUpdatesManager.getInstance.sendUpdate("AccountsCommunications(" & noClient & "," & True & ")")
    End Sub

    Private Sub supprimerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupprimerToolStripMenuItem.Click
        delComm_Click(sender, e)
    End Sub

    Private Sub ouvrirLeFichierJointToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OuvrirLeFichierJointToolStripMenuItem.Click
        listeCommunications_DblClick(sender, New CI.Controls.List.DblClickEventArgs(listeCommunications.selected, 1, 0, 0))
    End Sub
#End Region

#Region "RV & Comptabilité Actions"

    Private Sub menuChangeRVPeriod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuChangeRVPeriod.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.changePeriod()
    End Sub

    Private Sub menuVisiteFlagged_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuVisiteFlagged.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        menuVisiteFlagged.Checked = Not menuVisiteFlagged.Checked
        rv.flagged = menuVisiteFlagged.Checked

        DBLinker.getInstance.updateDB("InfoVisites", "Flagged='" & menuVisiteFlagged.Checked & "'", "NoVisite", rv.noVisite, False)
    End Sub

    Private Sub createdBill(ByVal sender As Object)
        paiements.Enabled = True
    End Sub

    Private Sub dossiersVlist_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dossiersVlist.SelectedIndexChanged
        loadRendezVous()
    End Sub

    Private Sub loadRendezVous(ByVal rvs As Generic.List(Of RendezVous))
        'REM_CODES
        Dim myWeekDays() As String = {"dim.", "lun.", "mar.", "mer.", "jeu.", "ven.", "sam."}
        Dim periodeTranslation() As String = {"15 minutes", "30 minutes", "45 minutes", "1 heure", "1h15min", "1h30min", "1h45min", "2 heures"}
        Dim n As Integer
        Dim curFolderCode As FolderCode

        For Each curRV As RendezVous In rvs
            curFolderCode = curRV.getFolderCode
            If curFolderCode Is Nothing Then
                addErrorLog(New Exception("curFolderCode Is Nothing : It should never be !!"))
                MessageBox.Show("Le rendez-vous de " & DateFormat.getTextDate(curRV.dateHeure) & " du dossier #" & curRV.noFolder & " n'a pas de codification dossier. Le rendez-vous ne peut donc pas être affiché dans la liste.", "Codification dossier manquante", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Continue For
            End If

            Dim visiteLine As String = DateFormat.getTextDate(curRV.dateHeure) & " " & myWeekDays(curRV.dateHeure.DayOfWeek) & vbTab & DateFormat.getTextDate(curRV.dateHeure, DateFormat.TextDateOptions.ShortTime) & " (" & curRV.noFolder & "-" & curFolderCode.name & ") " & periodeTranslation(curRV.period / 15 - 1) & " de " & curRV.service & " par " & UsersManager.getInstance.getUser(curRV.noTRP).toString
            n = visitesList.add(visiteLine)
            Select Case curRV.noStatut
                Case 3
                    visitesList.ItemBackColor(n) = LegendePanel.rvColor
                Case 4
                    If curRV.isBillPaid Or curRV.isBillSouffrance Then
                        visitesList.ItemBackColor(n) = LegendePanel.presencePayeeColor
                    Else
                        visitesList.ItemBackColor(n) = LegendePanel.presenceColor
                    End If
                Case 2
                    visitesList.ItemBackColor(n) = LegendePanel.absenceMColor
                Case 1
                    visitesList.ItemBackColor(n) = LegendePanel.absenceNMColor
            End Select
            'RV Is to confirmed
            If curRV.confirmed = False AndAlso ((curFolderCode.confirmation = 2 And curRV.evaluation = False) Or (curFolderCode.confirmation = 1 And curRV.evaluation = True) Or curFolderCode.confirmation = 3) Then
                visitesList.ItemIconsShowed(n, 0) = True
            End If

            If curRV.evaluation Then
                Dim curFS As FontStyle = FontStyle.Regular
                If PreferencesManager.getGeneralPreferences()("RVEvalGras") Then curFS += FontStyle.Bold
                If PreferencesManager.getGeneralPreferences()("RVEvalItalique") Then curFS += FontStyle.Italic
                If PreferencesManager.getGeneralPreferences()("RVEvalSouligne") Then curFS += FontStyle.Underline
                If PreferencesManager.getGeneralPreferences()("RVEvalBarre") Then curFS += FontStyle.Strikeout
                visitesList.ItemFont(n) = New Font(PreferencesManager.getGeneralPreferences()("RVEvalFont").ToString, visitesList.baseFont.Size, curFS)
            End If

            visitesList.ItemValueA(n) = curRV
            If curRV.remarks <> "" Then visitesList.ItemToolTipText(n) = "Remarques :" & vbCrLf & curRV.remarks
        Next

        'Reset history so that if an RV is deleted, it erase the content
        If visiteHistory.DataSource IsNot Nothing Then
            With CType(visiteHistory.DataSource, DataTable).Rows
                For i As Integer = 0 To .Count - 1
                    .Item(i).Delete()
                Next i
            End With
        End If
        'visiteHistory.DataSource = Nothing
        visiteHistory.Refresh()
    End Sub

    Private Sub choixFiltrage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChoixA.Click, ChoixDe.Click
        Dim CurDate, scd() As String
        Dim myDate As Date = Nothing
        If sender.name.tolower = "choixde" Then
            CurDate = DateDe.Text
        Else
            CurDate = DateA.Text
        End If
        If CurDate <> "" Then
            scd = CurDate.Split(New Char() {"/"})
            myDate = New Date(scd(0), scd(1), scd(2))
        Else
            myDate = Date.Today
        End If
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year - 10, Date.Today.Year + 1, , , , , , True, , , , , myDate)
        If dateReturn.Count = 0 Then Exit Sub

        Dim dateReturnString As String = DateFormat.getTextDate(dateReturn(0))
        If sender.name.tolower = "choixde" Then
            DateDe.Text = dateReturnString
            If DateA.Text = "" Then
                DateA.Text = dateReturnString
            Else
                If date1Infdate2(CDate(DateA.Text), dateReturn(0)) Then DateA.Text = dateReturnString
            End If
        Else
            If DateDe.Text = "" Then
                DateDe.Text = dateReturnString
            Else
                If date1Infdate2(dateReturn(0), CDate(DateDe.Text)) Then DateDe.Text = dateReturnString
            End If
            DateA.Text = dateReturnString
        End If
    End Sub

    Private Sub filterBills_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FilterBills.Click
        loadBills()
    End Sub

    Private Sub visitesList_ItemClick(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs) Handles visitesList.itemClick
        If e.button = 2 Then
            If e.selectedItem <> visitesList.selected Then visitesList.selected = e.selectedItem
            menuclickRV.Show(visitesList, New Point(e.x, e.y - visitesList.vsValue))
        End If
    End Sub

    Public Sub menuacopier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuacopier.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.copy()
    End Sub


    Private Sub menuAnnonceClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAnnonceClient.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.annonce()
    End Sub

    Private Sub menuConfirmRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuConfirmRV.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.confirm()
    End Sub

    Private Sub menuPrintRecu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuPrintRecu.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.generateRecu()
    End Sub

    Private Sub menuTransferRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuTransferRV.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        Dim choices As String = ""
        For i As Integer = 0 To dossier.Items.Count - 1
            If dossier.Items(i).ToString.StartsWith(rv.noFolder & " -") = False Then choices &= "§" & dossier.Items(i).ToString
        Next i
        If choices = "" Then Exit Sub

        choices = choices.Substring(1)

        Dim myMultiChoice As New multichoice
        Dim myChoice As String = myMultiChoice.GetChoice("Sélectionner le dossier pour transférer", choices, , "§", False)
        If myChoice.StartsWith("ERROR") Then Exit Sub

        Dim newNoFolder As Integer = myChoice.Substring(0, myChoice.IndexOf("-")).Trim
        rv.transferToFolder(newNoFolder)
    End Sub


    Private Sub menuRVService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRVService.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.changeService()
    End Sub

    Private Sub menuRVRemarques_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRVRemarques.Click, menuRVRemarkDel.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        If CType(sender, MenuItem).Text = "Modifier la remarque" Then
            rv.changeRemarks()
        Else
            rv.deleteRemarks()
        End If
    End Sub

    Public Sub menuaenlever_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuaenlever.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.delete()
    End Sub

    Private Sub menuQueueList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuQueueList.Click
        openQL()
    End Sub

    Private Sub menuAddToQueueList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAddToQueueList.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        addToListeAttente(noClient, rv.noFolder, rv.noVisite, rv.noTRP, rv.period, rv.dateHeure, Me.nom.Text & "," & Me.prenom.Text)
    End Sub

    Public Sub menueffstatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menueffstatus.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        Dim errorMsg As String = rv.changeStatus(RVsStatus.RVPossibleStatuses.Normal)
        If errorMsg <> "" AndAlso errorMsg.Contains("annulé") = False Then MessageBox.Show(errorMsg, "Impossible de changer le statut")
    End Sub

    Public Sub menupresent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menupresent.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        Dim errorMsg As String = rv.changeStatus(RVsStatus.RVPossibleStatuses.Present)
        If errorMsg <> "" AndAlso errorMsg.Contains("annulé") = False Then MessageBox.Show(errorMsg, "Impossible de changer le statut")
    End Sub

    Private Sub menuabsentmotive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuabsentmotive.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        Dim errorMsg As String = rv.changeStatus(RVsStatus.RVPossibleStatuses.NotPresentMotivated)
        If errorMsg <> "" AndAlso errorMsg.Contains("annulé") = False Then MessageBox.Show(errorMsg, "Impossible de changer le statut")
    End Sub

    Private Sub menuabsentnonmotive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuabsentnonmotive.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)

        Dim errorMsg As String = rv.changeStatus(RVsStatus.RVPossibleStatuses.NotPresentNotMotivated)
        If errorMsg <> "" AndAlso errorMsg.Contains("annulé") = False Then MessageBox.Show(errorMsg, "Impossible de changer le statut")
    End Sub

    Private currentLocks As New ArrayList

    Private Sub startStopBillChanging_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartStopBillChanging.Click
        'Droit & Accès
        If currentDroitAcces(15) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier les factures." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim i As Integer

        If ToolTip1.GetToolTip(StartStopBillChanging).StartsWith("C") Then
            If lockSecteur("ClientFacturation-" & noClient & "-", True, "Facturation du client " & nom.Text & "," & prenom.Text) = True Then
                currentLocks.Add("ClientFacturation-" & noClient & "-")
                StartStopBillChanging.Image = imgModifSave.Images(2)
                ToolTip1.SetToolTip(StartStopBillChanging, "Arrêter la modification des factures et paiements")

                curFactureBox.locked = False
                If facturesView.SelectedRows.Count > 0 Then
                    If curFactureBox.lockSecteur("", currentLocks) = False Then
                        MessageBox.Show("Impossible de modifier la facture présentement sélectionnée, car soit la facturation de l'entité liée ou de l'un des payeurs est présentement en cours de modification", "Facturation du client " & nom.Text & "," & prenom.Text)
                        curFactureBox.locked = True
                    End If
                End If
            End If
        Else
            StartStopBillChanging.Image = imgModifSave.Images(0)
            ToolTip1.SetToolTip(StartStopBillChanging, "Commencer la modification des factures et paiements")

            curFactureBox.locked = True
            For i = 0 To currentLocks.Count - 1
                lockSecteur(currentLocks(i), False)
            Next i
            currentLocks.Clear()

            If facturesView.RowCount > 0 Then InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & noClient & ",-1)")
        End If
    End Sub

    Private Sub manageRendezVousContextMenu(ByVal noSelected As Integer)
        If visitesList.listCount <= 0 Then Exit Sub

        lastVisiteSelected = noSelected

        Dim rv As RendezVous = visitesList.ItemValueA(noSelected)

        visiteHistory.DataSource = DBLinker.getInstance.readDBForGrid("SELECT DateHeureCreation, NomAction, Utilisateurs.Nom +','+Utilisateurs.Prenom + ' (' + CAST(Utilisateurs.NoUser AS VARCHAR(5)) + ')' AS ByUser, CASE WHEN Raison IS NULL THEN Comments ELSE Raison END AS Raison FROM StatVisites INNER JOIN ListeAction ON ListeAction.NoAction = StatVisites.NoAction INNER JOIN Utilisateurs ON Utilisateurs.NoUser = StatVisites.NoUser LEFT JOIN AbsencesRaisons ON AbsencesRaisons.NoRaison = StatVisites.NoRaison WHERE NoVisite=" & rv.noVisite).Tables(0)

        With Me
            visitesList.selected = noSelected
            .menuAddToQueueList.Enabled = True
            .menupresent.Enabled = True
            .menuabsentmotive.Enabled = True
            .menuabsentnonmotive.Enabled = True
            .menueffstatus.Enabled = True
            .menuaenlever.Enabled = True
            .menuAnnonceClient.Enabled = True
            .menuConfirmRV.Enabled = Not rv.confirmed
            .menuPrintRecu.Enabled = True
            .menuChangeRVPeriod.Enabled = True
            .menuTransferRV.Enabled = IIf(dossier.Items.Count > 1, True, False)

            If date1Infdate2(rv.dateHeure, Date.Today) = True Then 'Past
                .menuaenlever.Enabled = False
                .menuAddToQueueList.Enabled = False
                .menuChangeRVPeriod.Enabled = False
            End If

            If date1Infdate2(rv.dateHeure, Date.Today, True) = False Then 'Today + Future
                .menupresent.Enabled = False
                .menuabsentnonmotive.Enabled = False
            End If

            If rv.dateHeure.Date.Equals(Date.Today) = False Then 'Not today
                .menuAnnonceClient.Enabled = False
            End If

            If ColorTranslator.ToOle(visitesList.ItemBackColor(noSelected)) = ColorTranslator.ToOle(LegendePanel.presenceColor) Then .menuaenlever.Enabled = False
            If ColorTranslator.ToOle(visitesList.ItemBackColor(noSelected)) <> ColorTranslator.ToOle(LegendePanel.rvColor) Then
                .menuAddToQueueList.Enabled = False
                .menuTransferRV.Enabled = False
            End If


            .menuRVEval.Enabled = rv.noStatut = 3 'Active only if Normal status
            .menuRVTraitement.Enabled = rv.noStatut = 3

            Dim myFacture As New Bill(rv.noFacture)
            Select Case ColorTranslator.ToOle(visitesList.ItemBackColor(noSelected))
                Case ColorTranslator.ToOle(LegendePanel.absenceMColor)
                    'Case Absent - Motivé
                    .menuabsentmotive.Enabled = False
                    .menuAnnonceClient.Enabled = False
                    .menuVisiteFlagged.Checked = rv.flagged

                Case ColorTranslator.ToOle(LegendePanel.absenceNMColor)
                    'Case Absent - Non-Motivé
                    .menuabsentnonmotive.Enabled = False
                    .menuAnnonceClient.Enabled = False
                    .menuRVEval.Enabled = False
                    .menuRVTraitement.Enabled = False
                    .menuVisiteFlagged.Checked = rv.flagged

                    If myFacture.noFacture > 0 Then If myFacture.getBillPaymentTotal > 0 Or myFacture.isSouffrance = True Then .menueffstatus.Enabled = False : .menuaenlever.Enabled = False

                Case Else
                    'Case Compte client
                    .menuVisiteFlagged.Checked = rv.flagged

                    If rv.noStatut = 3 Then
                        .menueffstatus.Enabled = False
                    Else
                        .menupresent.Enabled = False
                        .menuabsentmotive.Enabled = False
                        .menuabsentnonmotive.Enabled = False
                    End If

                    .menuRVEval.Checked = rv.evaluation
                    .menuRVTraitement.Checked = Not rv.evaluation

                    If myFacture.noFacture > 0 Then
                        If myFacture.getBillPaymentTotal > 0 Or myFacture.isSouffrance Then
                            .menuaenlever.Enabled = False
                            .menueffstatus.Enabled = False
                        End If
                    End If

                    Dim inQL() As String = DBLinker.getInstance.readOneDBField("ListeAttente", "NoQL", "WHERE ((NoVisite)=" & rv.noVisite & ");")
                    If Not inQL Is Nothing AndAlso inQL.Length <> 0 Then .menuAddToQueueList.Enabled = False
            End Select

            .menuPrintRecu.Enabled = myFacture.isReceiptToDo("C")
        End With
    End Sub

    Private Sub menuRVEval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRVEval.Click, menuRVTraitement.Click
        Dim rv As RendezVous = visitesList.ItemValueA(visitesList.selected)
        rv.switchType()
    End Sub
#End Region

#Region "Handling FolderText document events (Testing)"
    'Private Sub folderTexte_TextClick(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextClick
    '    MyMainWin.StatusText = "FolderTexte_TextClick"
    'End Sub

    'Private Sub folderTexte_TextDoubleClick(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextDoubleClick
    '    MyMainWin.StatusText = "FolderTexte_TextDoubleClick"
    'End Sub

    'Private Sub folderTexte_TextKeyDown(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextKeyDown
    '    MyMainWin.StatusText = "FolderTexte_TextKeyDown"
    'End Sub

    'Private Sub folderTexte_TextGotFocus(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextGotFocus
    '    MyMainWin.StatusText = "FolderTexte_TextGotFocus"
    'End Sub

    'Private Sub folderTexte_TextKeyPress(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextKeyPress
    '    MyMainWin.StatusText = "FolderTexte_TextKeyPress"
    'End Sub

    'Private Sub folderTexte_TextKeyUp(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextKeyUp
    '    MyMainWin.StatusText = "FolderTexte_TextKeyUp"
    'End Sub

    'Private Sub folderTexte_TextLostFocus(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextLostFocus
    '    MyMainWin.StatusText = "FolderTexte_TextLostFocus"
    'End Sub

    'Private Sub folderTexte_TextMouseDown(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextMouseDown
    '    MyMainWin.StatusText = "FolderTexte_TextMouseDown"
    'End Sub

    'Private Sub folderTexte_TextMouseMove(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextMouseMove
    '    MyMainWin.StatusText = "FolderTexte_TextMouseMove"
    'End Sub

    'Private Sub folderTexte_TextMouseOut(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextMouseOut
    '    MyMainWin.StatusText = "FolderTexte_TextMouseOut"
    'End Sub

    'Private Sub folderTexte_TextMouseOver(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextMouseOver
    '    MyMainWin.StatusText = "FolderTexte_TextMouseOver"
    'End Sub

    'Private Sub folderTexte_TextMouseUp(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextMouseUp
    '    MyMainWin.StatusText = "FolderTexte_TextMouseUp"
    'End Sub

    'Private Sub folderTexte_TextMouseWheel(ByVal e As mshtml.IHTMLEventObj) Handles FolderTexte.TextMouseWheel
    '    MyMainWin.StatusText = "FolderTexte_TextMouseWheel"
    'End Sub
#End Region

    Private Sub addAlert(ByVal alertMessage As String, Optional ByVal noFolder As Integer = 0, Optional ByVal alarmDate As Date = LIMIT_DATE)
        Dim alertDate As Date
        If alarmDate.Equals(LIMIT_DATE) = False Then
            alertDate = alarmDate
        Else
            Dim myDateChoice As New DateChoice
            Dim myAlarmDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year, Date.Now.Year + 1, , , True, , , True, Date.Now, , , , , , , , , 1)
            If myAlarmDate.Count = 0 Then Exit Sub

            alertDate = myAlarmDate(0)
        End If
        Dim createRapportEtape As Boolean = False
        If noFolder > 0 Then createRapportEtape = True

        AlertsManager.getInstance.addAlert(alertMessage, ConnectionsManager.currentUser, AlertsManager.AType.OpenClientAccount, Me.noClient, , New AlarmOfClientAccount(alertDate, Me.noClient, noFolder, createRapportEtape), True)
    End Sub

    Private Sub compteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompteToolStripMenuItem.Click
        Dim myInputBoxPlus As New InputBoxPlus(True, "Users\Lists\" & ConnectionsManager.currentUser & "\alertmsg.lst")
        Dim alertMessage As String = myInputBoxPlus("Quel message désirez-vous vous laisser ?", "Message au compte")
        If alertMessage = "" Then Exit Sub
        addItemToAList("Users\Lists\" & ConnectionsManager.currentUser & "\alertmsg.lst", alertMessage, , True, 15, False)

        addAlert(alertMessage)
    End Sub

    Private Sub rapportAuMédecinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RapportAuMédecinToolStripMenuItem.Click
        Dim myDateChoice As New DateChoice

        myDateChoice.texteEnHaut.Text = "Date d'échéance du rapport :"
        Dim rapportDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year, Date.Now.Year + 1, , , , , , True, Date.Now, , , , , , , , , 1)
        If rapportDate.Count = 0 Then Exit Sub

        myDateChoice = New DateChoice()
        myDateChoice.texteEnHaut.Text = "Date et heure pour l'alarme :"
        Dim alarmDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year, Date.Now.Year + 1, , , True, , , True, Date.Now, , , , , , , , , 1)
        If alarmDate.Count = 0 Then Exit Sub
        
        Dim dossiersStr As String = ""
        Dim i As Integer
        For i = 0 To dossier.Items.Count - 1
            If dossier.Items(i).ToString.IndexOf("Inactif") < 0 Then dossiersStr &= "§" & dossier.Items(i).ToString
        Next i
        If dossiersStr = "" Then Exit Sub
        dossiersStr = dossiersStr.Substring(1)
        Dim myMultiChoice As New multichoice()
        Dim rapportDossier As String = myMultiChoice.GetChoice("Choix du dossier du rapport", dossiersStr, , "§")
        If rapportDossier.StartsWith("ERROR") Then Exit Sub
        rapportDossier = rapportDossier.Substring(0, rapportDossier.IndexOf("-"))

        addAlert("La rapport au médecin pour le client " & nom.Text & "," & prenom.Text & " du dossier #" & rapportDossier & " est dû le " & rapportDate(0), 0, alarmDate(0))
    End Sub

    Private Sub btnAddAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAlert.Click
        addAlertMenu.Show(btnAddAlert, btnAddAlert.Width, 0)
    End Sub

    Protected Overrides Sub finalize()
        myPhoto = Nothing
        imgModifSave = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub facturesView_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles facturesView.RowStateChanged
        If e.StateChanged = DataGridViewElementStates.None Then
            curFactureBox.locked = True
        End If
    End Sub

    Private Sub facturesView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles facturesView.SelectionChanged
        If facturesView.currentRow Is Nothing Then Exit Sub

        Dim curLoadedBill As Integer = Integer.Parse(facturesView.currentRow.Cells("NoFacture").Value.ToString)
        If curLoadedBill = lastLoadedBill Then Exit Sub

        lastLoadedBill = curLoadedBill
        If ToolTip1.GetToolTip(StartStopBillChanging).StartsWith("Commencer") = False Then curFactureBox.locked = False
        curFactureBox.loading(lastLoadedBill)
        If curFactureBox.locked = False AndAlso curFactureBox.lockSecteur("", currentLocks) = False Then
            MessageBox.Show("Impossible de modifier la facture présentement sélectionné, car soit la facturation de l'entité liée ou de l'un des payeurs est présentement en cours de modification", "Facturation du client " & nom.Text & "," & prenom.Text)
            curFactureBox.locked = True
        End If
    End Sub

    Public WriteOnly Property transferFolderNoClient() As Integer
        Set(ByVal value As Integer)
            newFolderNoClient = value
        End Set
    End Property

    Private Sub visitesList_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs) Handles visitesList.willSelect
        If e.selectedItem < 0 Then Exit Sub
        If lastVisiteSelected = e.selectedItem Then Exit Sub

        manageRendezVousContextMenu(e.selectedItem)
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf saveAll)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        'Close the window
        If dataReceived.function = "Client-Close" AndAlso dataReceived.params(0) = Me.noClient Then
            Me.Close()
            Exit Sub
        End If

        'Active/désactive le bouton de paiement lors d'une MAJ des paiements
        If dataReceived.function = "Paiements" AndAlso dataReceived.params(0) = Me.noClient Then
            Dim paymentsToDo As Boolean = False
            If dataReceived.params(1) = -1 Then
                paymentsToDo = Bill.isPaymentsToDoByClient(noClient)
            ElseIf dataReceived.params(1) > 0 Then
                paymentsToDo = True
            End If
            paiements.Enabled = paymentsToDo
        End If

        'Met à jour la comptabilité et le bouton de paiement si nécessaire
        If dataReceived.function = "AccountsBills" AndAlso dataReceived.params(0) = noClient Then
            If Me.facturesView.RowCount <> 0 Then loadBills()
            paiements.Enabled = Bill.isPaymentsToDoByClient(noClient)
        End If

        'Met à jour les antécédents
        If dataReceived.fromExternal AndAlso dataReceived.function = "AccountsAntecedents" AndAlso dataReceived.params(0) = noClient Then
            loadClientHistory()
        End If

        'Met à jour les communications
        If dataReceived.function = "AccountsCommunications" AndAlso dataReceived.params(0) = noClient Then
            loadCommunications(dataReceived.params(1))
        End If

        'Met à jour les textes d'un dossier
        If dataReceived.function = "AccountsDossierTextBoxes" AndAlso dataReceived.params(0) = noClient AndAlso dataReceived.params(1) = currentNoFolder Then
            loadFolderText(currentNoFolder)
        End If

        'Met à jour les infos d'un dossier
        If dataReceived.fromExternal AndAlso dataReceived.function = "AccountsDossierInfos" AndAlso dataReceived.params(0) = noClient AndAlso dataReceived.params(1) = currentNoFolder Then
            loadFolderInfos(currentNoFolder)
        End If

        'Met à jour les équipements
        If dataReceived.function.StartsWith("Equipement") Then
            loadFolderEquipmentInfos()
            folderEquipmentModified = False
            listEquipement.Tag = Nothing
            selectingEquipment()
        End If

        'Met à jour les équipements prêtés/vendus
        If (dataReceived.function = "Paiements" AndAlso dataReceived.params(0) = Me.noClient) OrElse (dataReceived.function = "AccountsEquipements" AndAlso dataReceived.params(0) = noClient AndAlso dataReceived.params(1) = currentNoFolder) Then
            loadFolderEquipmentInfos()
            loadFolderEquipment()
        End If

        'Met à jour les infos de base du client
        If dataReceived.fromExternal AndAlso dataReceived.function = "AccountsGenInfo" AndAlso dataReceived.params(0) = noClient Then
            loadGeneralInfo()
        End If

        'Recharge les dossiers
        If dataReceived.function = "AccountsDossiers" AndAlso dataReceived.params(0) = noClient Then
            loadFolders()
        End If

        'Recharge les visites
        If dataReceived.function = "AccountsVisites" AndAlso dataReceived.params(0) = Me.noClient Then
            Dim sNoFolder() As String = dossiersVlist.GetItemText(dossiersVlist.SelectedItem).Split(New Char() {"-"})
            If (Val(sNoFolder(0)) = dataReceived.params(1) Or dossiersVlist.SelectedIndex = 0) Then loadRendezVous()
        End If
    End Sub

    Private Sub btnSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        sendEmailTo(courriel.Text)
    End Sub

    Private Sub GroupComm_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupComm.Enter

    End Sub

    Private Sub menuExportFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExportFolder.Click
        'Droit & Accès
        If currentDroitAcces(110) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Exportation d'un dossier." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myExportWindow As FolderExportation = openUniqueWindow(New FolderExportation())
        myExportWindow.setFolder(nom.Text & "," & prenom.Text, noClient, Me.currentNoFolder)
        myExportWindow.Show()
    End Sub
End Class
