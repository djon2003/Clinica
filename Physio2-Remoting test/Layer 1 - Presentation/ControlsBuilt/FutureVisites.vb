Imports CI.Clinica.Accounts.Clients.Folders.RVs

Public Class FutureVisites
    Inherits BaseUpdatedControl
    Implements IMovableObject, IControllable

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        movingCursor = DrawingManager.getInstance.getCursor("MOVE4WAY.CUR")
        Bordure.Cursor = movingCursor
        Me.rvList.icons.Add(DrawingManager.getInstance.getIcon("Confirmation.ico"))
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            InternalUpdatesManager.getInstance.removeConsumer(Me)
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Bordure As System.Windows.Forms.Panel
    Friend WithEvents Bordure2 As System.Windows.Forms.Panel
    Friend WithEvents printRV As System.Windows.Forms.Button
    Friend WithEvents menuclickRV As System.Windows.Forms.ContextMenu
    Friend WithEvents menuacopier As System.Windows.Forms.MenuItem
    Friend WithEvents menuaenlever As System.Windows.Forms.MenuItem
    Friend WithEvents menuSeparator As System.Windows.Forms.MenuItem
    Friend WithEvents menuSendEmail As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuAddToQueueList As System.Windows.Forms.MenuItem
    Friend WithEvents menuQueueList As System.Windows.Forms.MenuItem
    Friend WithEvents menumodifstatus As System.Windows.Forms.MenuItem
    Friend WithEvents menuabsentmotive As System.Windows.Forms.MenuItem
    Friend WithEvents menunewrv As System.Windows.Forms.MenuItem
    Friend WithEvents menuScan As System.Windows.Forms.MenuItem
    Friend WithEvents menuopenaccount As System.Windows.Forms.MenuItem
    Friend WithEvents menupaiement As System.Windows.Forms.MenuItem
    Friend WithEvents menuReports As System.Windows.Forms.MenuItem
    Friend WithEvents ClientBox As CI.Controls.List
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents menuCopier As System.Windows.Forms.MenuItem
    Friend WithEvents Titre As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ControlBox As Clinica.TransparentControlBox
    Friend WithEvents menuDemandeAuthorisation As System.Windows.Forms.MenuItem
    Friend WithEvents menuEtatCompte As System.Windows.Forms.MenuItem
    Friend WithEvents menuRecusGenerated As System.Windows.Forms.MenuItem
    Friend WithEvents menuRVFuturs As System.Windows.Forms.MenuItem
    Friend WithEvents menuReportClientDetails As System.Windows.Forms.MenuItem
    Friend WithEvents menuReportDossierDetails As System.Windows.Forms.MenuItem
    Friend WithEvents menuRapEtatClient As System.Windows.Forms.MenuItem
    Friend WithEvents menuRapRVDossier As System.Windows.Forms.MenuItem
    Friend WithEvents AskExtraQuestion As System.Windows.Forms.CheckBox
    Friend WithEvents menuConfirmRV As System.Windows.Forms.MenuItem
    Friend WithEvents menuModifRemarqueRV As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FutureVisites))
        Me.Bordure = New System.Windows.Forms.Panel
        Me.therapeute = New System.Windows.Forms.ComboBox
        Me.ClientBox = New CI.Controls.List
        Me.ControlBox = New TransparentControlBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Titre = New System.Windows.Forms.Label
        Me.tel = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.ville = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.adresse = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Bordure2 = New System.Windows.Forms.Panel
        Me.AskExtraQuestion = New System.Windows.Forms.CheckBox
        Me.printRV = New System.Windows.Forms.Button
        Me.rvList = New CI.Controls.List
        Me.clientMenu = New System.Windows.Forms.ContextMenu
        Me.menuCopier = New System.Windows.Forms.MenuItem
        Me.menuColler = New System.Windows.Forms.MenuItem
        Me.menuItem3 = New System.Windows.Forms.MenuItem
        Me.menuSearch = New System.Windows.Forms.MenuItem
        Me.menuclickRV = New System.Windows.Forms.ContextMenu
        Me.menuacopier = New System.Windows.Forms.MenuItem
        Me.menuaenlever = New System.Windows.Forms.MenuItem
        Me.menuSeparator = New System.Windows.Forms.MenuItem
        Me.menuConfirmRV = New System.Windows.Forms.MenuItem
        Me.menuSendEmail = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.menuAddToQueueList = New System.Windows.Forms.MenuItem
        Me.menuQueueList = New System.Windows.Forms.MenuItem
        Me.menuModifRemarqueRV = New System.Windows.Forms.MenuItem
        Me.menumodifstatus = New System.Windows.Forms.MenuItem
        Me.menuabsentmotive = New System.Windows.Forms.MenuItem
        Me.menunewrv = New System.Windows.Forms.MenuItem
        Me.menuScan = New System.Windows.Forms.MenuItem
        Me.menuopenaccount = New System.Windows.Forms.MenuItem
        Me.menupaiement = New System.Windows.Forms.MenuItem
        Me.menuReports = New System.Windows.Forms.MenuItem
        Me.menuReportClientDetails = New System.Windows.Forms.MenuItem
        Me.menuReportDossierDetails = New System.Windows.Forms.MenuItem
        Me.menuDemandeAuthorisation = New System.Windows.Forms.MenuItem
        Me.menuRapEtatClient = New System.Windows.Forms.MenuItem
        Me.menuEtatCompte = New System.Windows.Forms.MenuItem
        Me.menuRecusGenerated = New System.Windows.Forms.MenuItem
        Me.menuRapRVDossier = New System.Windows.Forms.MenuItem
        Me.menuRVFuturs = New System.Windows.Forms.MenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Bordure.SuspendLayout()
        Me.Bordure2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Bordure
        '
        Me.Bordure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bordure.Controls.Add(Me.therapeute)
        Me.Bordure.Controls.Add(Me.ClientBox)
        Me.Bordure.Controls.Add(Me.ControlBox)
        Me.Bordure.Controls.Add(Me.Label4)
        Me.Bordure.Controls.Add(Me.Titre)
        Me.Bordure.Controls.Add(Me.tel)
        Me.Bordure.Controls.Add(Me.Label3)
        Me.Bordure.Controls.Add(Me.ville)
        Me.Bordure.Controls.Add(Me.Label2)
        Me.Bordure.Controls.Add(Me.adresse)
        Me.Bordure.Controls.Add(Me.Label1)
        Me.Bordure.Controls.Add(Me.Bordure2)
        Me.Bordure.Controls.Add(Me.rvList)
        Me.Bordure.Location = New System.Drawing.Point(0, 0)
        Me.Bordure.Name = "Bordure"
        Me.Bordure.Size = New System.Drawing.Size(297, 402)
        Me.Bordure.TabIndex = 0
        '
        'therapeute
        '
        Me.therapeute.Cursor = System.Windows.Forms.Cursors.Default
        Me.therapeute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.therapeute.Location = New System.Drawing.Point(7, 120)
        Me.therapeute.Name = "therapeute"
        Me.therapeute.Size = New System.Drawing.Size(280, 21)
        Me.therapeute.TabIndex = 20
        '
        'ClientBox
        '
        Me.ClientBox.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.ClientBox.autoAdjust = True
        Me.ClientBox.autoKeyDownSelection = True
        Me.ClientBox.autoSizeHorizontally = False
        Me.ClientBox.autoSizeVertically = False
        Me.ClientBox.BackColor = System.Drawing.Color.White
        Me.ClientBox.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.ClientBox.baseBackColor = System.Drawing.Color.White
        Me.ClientBox.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.ClientBox.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.ClientBox.bgColor = System.Drawing.Color.White
        Me.ClientBox.borderColor = System.Drawing.Color.Empty
        Me.ClientBox.borderSelColor = System.Drawing.Color.Empty
        Me.ClientBox.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.ClientBox.CausesValidation = False
        Me.ClientBox.clickEnabled = False
        Me.ClientBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.ClientBox.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.ClientBox.do3D = False
        Me.ClientBox.draw = True
        Me.ClientBox.extraWidth = 0
        Me.ClientBox.hScrollColor = System.Drawing.SystemColors.Control
        Me.ClientBox.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.ClientBox.hScrolling = False
        Me.ClientBox.hsValue = 0
        Me.ClientBox.icons = CType(resources.GetObject("ClientBox.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.ClientBox.itemBorder = 0
        Me.ClientBox.itemMargin = 0
        Me.ClientBox.items = CType(resources.GetObject("ClientBox.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.ClientBox.Location = New System.Drawing.Point(7, 48)
        Me.ClientBox.mouseMove3D = False
        Me.ClientBox.mouseSpeed = 0
        Me.ClientBox.Name = "ClientBox"
        Me.ClientBox.objMaxHeight = 0.0!
        Me.ClientBox.objMaxWidth = 0.0!
        Me.ClientBox.objMinHeight = 0.0!
        Me.ClientBox.objMinWidth = 0.0!
        Me.ClientBox.reverseSorting = False
        Me.ClientBox.selected = -1
        Me.ClientBox.selectedClickAllowed = True
        Me.ClientBox.selectMultiple = False
        Me.ClientBox.Size = New System.Drawing.Size(280, 16)
        Me.ClientBox.sorted = False
        Me.ClientBox.TabIndex = 11
        Me.ClientBox.toolTipText = Nothing
        Me.ClientBox.vScrollColor = System.Drawing.SystemColors.Control
        Me.ClientBox.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.ClientBox.vScrolling = False
        Me.ClientBox.vsValue = 0
        '
        'ControlBox
        '
        Me.ControlBox.BackColor = System.Drawing.Color.Transparent
        Me.ControlBox.isLocked = True
        Me.ControlBox.Location = New System.Drawing.Point(244, 3)
        Me.ControlBox.Name = "ControlBox"
        Me.ControlBox.Size = New System.Drawing.Size(47, 15)
        Me.ControlBox.TabIndex = 26
        Me.ControlBox.Text = "TransparentControl1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Nom du client :"
        '
        'Titre
        '
        Me.Titre.AutoSize = True
        Me.Titre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Titre.Location = New System.Drawing.Point(90, 8)
        Me.Titre.Name = "Titre"
        Me.Titre.Size = New System.Drawing.Size(125, 13)
        Me.Titre.TabIndex = 24
        Me.Titre.Text = "Rendez-vous futur(s)"
        '
        'tel
        '
        Me.tel.AutoSize = True
        Me.tel.Location = New System.Drawing.Point(35, 104)
        Me.tel.Name = "tel"
        Me.tel.Size = New System.Drawing.Size(0, 13)
        Me.tel.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Tél - "
        '
        'ville
        '
        Me.ville.AutoSize = True
        Me.ville.Location = New System.Drawing.Point(40, 88)
        Me.ville.Name = "ville"
        Me.ville.Size = New System.Drawing.Size(0, 13)
        Me.ville.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Ville :"
        '
        'adresse
        '
        Me.adresse.AutoSize = True
        Me.adresse.Location = New System.Drawing.Point(56, 72)
        Me.adresse.Name = "adresse"
        Me.adresse.Size = New System.Drawing.Size(0, 13)
        Me.adresse.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Adresse :"
        '
        'Bordure2
        '
        Me.Bordure2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bordure2.Controls.Add(Me.AskExtraQuestion)
        Me.Bordure2.Controls.Add(Me.printRV)
        Me.Bordure2.Location = New System.Drawing.Point(-1, 360)
        Me.Bordure2.Name = "Bordure2"
        Me.Bordure2.Size = New System.Drawing.Size(297, 41)
        Me.Bordure2.TabIndex = 13
        '
        'AskExtraQuestion
        '
        Me.AskExtraQuestion.AutoSize = True
        Me.AskExtraQuestion.Checked = True
        Me.AskExtraQuestion.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AskExtraQuestion.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.AskExtraQuestion.Location = New System.Drawing.Point(191, 13)
        Me.AskExtraQuestion.Name = "AskExtraQuestion"
        Me.AskExtraQuestion.Size = New System.Drawing.Size(96, 17)
        Me.AskExtraQuestion.TabIndex = 2
        Me.AskExtraQuestion.Text = "Avec message"
        Me.AskExtraQuestion.UseVisualStyleBackColor = True
        '
        'printRV
        '
        Me.printRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.printRV.Location = New System.Drawing.Point(8, 8)
        Me.printRV.Name = "printRV"
        Me.printRV.Size = New System.Drawing.Size(177, 24)
        Me.printRV.TabIndex = 1
        Me.printRV.Text = "Imprimer les rendez-vous"
        '
        'rvList
        '
        Me.rvList.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.rvList.autoAdjust = True
        Me.rvList.autoKeyDownSelection = True
        Me.rvList.autoSizeHorizontally = False
        Me.rvList.autoSizeVertically = False
        Me.rvList.BackColor = System.Drawing.Color.White
        Me.rvList.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.rvList.baseBackColor = System.Drawing.Color.White
        Me.rvList.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.rvList.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.rvList.bgColor = System.Drawing.Color.White
        Me.rvList.borderColor = System.Drawing.Color.Empty
        Me.rvList.borderSelColor = System.Drawing.Color.Empty
        Me.rvList.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.rvList.CausesValidation = False
        Me.rvList.clickEnabled = True
        Me.rvList.Cursor = System.Windows.Forms.Cursors.Default
        Me.rvList.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.rvList.do3D = False
        Me.rvList.draw = False
        Me.rvList.extraWidth = 0
        Me.rvList.hScrollColor = System.Drawing.SystemColors.Control
        Me.rvList.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.rvList.hScrolling = True
        Me.rvList.hsValue = 0
        Me.rvList.icons = CType(resources.GetObject("rvList.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.rvList.itemBorder = 0
        Me.rvList.itemMargin = 0
        Me.rvList.items = CType(resources.GetObject("rvList.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.rvList.Location = New System.Drawing.Point(7, 144)
        Me.rvList.mouseMove3D = False
        Me.rvList.mouseSpeed = 0
        Me.rvList.Name = "rvList"
        Me.rvList.objMaxHeight = 0.0!
        Me.rvList.objMaxWidth = 0.0!
        Me.rvList.objMinHeight = 0.0!
        Me.rvList.objMinWidth = 0.0!
        Me.rvList.reverseSorting = False
        Me.rvList.selected = -1
        Me.rvList.selectedClickAllowed = True
        Me.rvList.selectMultiple = False
        Me.rvList.Size = New System.Drawing.Size(280, 208)
        Me.rvList.sorted = True
        Me.rvList.TabIndex = 12
        Me.rvList.toolTipText = Nothing
        Me.rvList.vScrollColor = System.Drawing.SystemColors.Control
        Me.rvList.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.rvList.vScrolling = True
        Me.rvList.vsValue = 0
        '
        'clientMenu
        '
        Me.clientMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuCopier, Me.menuColler, Me.menuItem3, Me.menuSearch})
        '
        'menuCopier
        '
        Me.menuCopier.Index = 0
        Me.menuCopier.Text = "Copier"
        '
        'menuColler
        '
        Me.menuColler.Index = 1
        Me.menuColler.Text = "Coller"
        '
        'menuItem3
        '
        Me.menuItem3.Index = 2
        Me.menuItem3.Text = "-"
        '
        'menuSearch
        '
        Me.menuSearch.DefaultItem = True
        Me.menuSearch.Index = 3
        Me.menuSearch.Text = "Rechercher un compte"
        '
        'menuclickRV
        '
        Me.menuclickRV.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuacopier, Me.menuaenlever, Me.menuSeparator, Me.menuConfirmRV, Me.menuSendEmail, Me.MenuItem1, Me.menuModifRemarqueRV, Me.menumodifstatus, Me.menunewrv, Me.menuScan, Me.menuopenaccount, Me.menupaiement, Me.menuReports})
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
        'menuConfirmRV
        '
        Me.menuConfirmRV.Index = 3
        Me.menuConfirmRV.Text = "Confirmer le rendez-vous"
        '
        'menuSendEmail
        '
        Me.menuSendEmail.Index = 4
        Me.menuSendEmail.Text = "Envoyer un courriel"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 5
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuAddToQueueList, Me.menuQueueList})
        Me.MenuItem1.Text = "Liste d'attente"
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
        'menuModifRemarqueRV
        '
        Me.menuModifRemarqueRV.Index = 6
        Me.menuModifRemarqueRV.Text = "Modifier la remarque du rendez-vous"
        '
        'menumodifstatus
        '
        Me.menumodifstatus.Index = 7
        Me.menumodifstatus.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuabsentmotive})
        Me.menumodifstatus.Text = "Modifier le statut..."
        '
        'menuabsentmotive
        '
        Me.menuabsentmotive.Index = 0
        Me.menuabsentmotive.Text = "Absent - Motivé"
        '
        'menunewrv
        '
        Me.menunewrv.Index = 8
        Me.menunewrv.Text = "Nouveau rendez-vous"
        '
        'menuScan
        '
        Me.menuScan.Index = 9
        Me.menuScan.Text = "Numériser une photo"
        Me.menuScan.Visible = False
        '
        'menuopenaccount
        '
        Me.menuopenaccount.Index = 10
        Me.menuopenaccount.Text = "Ouvrir le compte"
        '
        'menupaiement
        '
        Me.menupaiement.Index = 11
        Me.menupaiement.Text = "Paiement"
        '
        'menuReports
        '
        Me.menuReports.Index = 12
        Me.menuReports.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuReportClientDetails, Me.menuReportDossierDetails, Me.menuDemandeAuthorisation, Me.menuRapEtatClient, Me.menuEtatCompte, Me.menuRecusGenerated, Me.menuRapRVDossier, Me.menuRVFuturs})
        Me.menuReports.Text = "Rapports personnalisés"
        '
        'menuReportClientDetails
        '
        Me.menuReportClientDetails.Index = 0
        Me.menuReportClientDetails.Text = "Compte client détaillé"
        '
        'menuReportDossierDetails
        '
        Me.menuReportDossierDetails.Index = 1
        Me.menuReportDossierDetails.Text = "Dossier détaillé"
        '
        'menuDemandeAuthorisation
        '
        Me.menuDemandeAuthorisation.Index = 2
        Me.menuDemandeAuthorisation.Text = "Demande d'autorisation d'un dossier"
        '
        'menuRapEtatClient
        '
        Me.menuRapEtatClient.Index = 3
        Me.menuRapEtatClient.Text = "État de compte : compte client"
        '
        'menuEtatCompte
        '
        Me.menuEtatCompte.Index = 4
        Me.menuEtatCompte.Text = "État de compte : dossier client"
        '
        'menuRecusGenerated
        '
        Me.menuRecusGenerated.Index = 5
        Me.menuRecusGenerated.Text = "Liste détaillée de reçus déjà générés"
        '
        'menuRapRVDossier
        '
        Me.menuRapRVDossier.Index = 6
        Me.menuRapRVDossier.Text = "Liste des rendez-vous d'un dossier"
        '
        'menuRVFuturs
        '
        Me.menuRVFuturs.Index = 7
        Me.menuRVFuturs.Text = "Rendez-vous futurs d'un client"
        '
        'FutureVisites
        '
        Me.Controls.Add(Me.Bordure)
        Me.Name = "FutureVisites"
        Me.Size = New System.Drawing.Size(297, 402)
        Me.Bordure.ResumeLayout(False)
        Me.Bordure.PerformLayout()
        Me.Bordure2.ResumeLayout(False)
        Me.Bordure2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents menuSearch As System.Windows.Forms.MenuItem
    Private WithEvents menuItem3 As System.Windows.Forms.MenuItem
    Private WithEvents menuColler As System.Windows.Forms.MenuItem
    Private WithEvents clientMenu As System.Windows.Forms.ContextMenu
    Private WithEvents therapeute As System.Windows.Forms.ComboBox
    Private WithEvents tel As System.Windows.Forms.Label
    Private WithEvents ville As System.Windows.Forms.Label
    Private WithEvents adresse As System.Windows.Forms.Label
    Private WithEvents rvList As CI.Controls.List

    Private movingCursor As Cursor
    Private XObjects, yObjects As Integer
    Private buttonDown As MouseButtons
    Private firstLoading As Boolean = True
    Private _BlockObjectInArea As Boolean = True
    Private _JoinedToolBarItem As ToolStrip = Nothing
    Private formCreator As Form

    Public Event willMove(ByVal sender As Object, ByVal x As Integer, ByVal y As Integer, ByVal xObjects As Integer, ByVal yObjects As Integer) Implements IMovableObject.willMove
    Public Shadows Event move(ByVal sender As Object, ByVal e As EventArgs) Implements IMovableObject.move

#Region "Propriétés"
    Public ReadOnly Property noClient() As Integer
        Get
            If Not TypeOf ClientBox.ItemValueA(0) Is ClientCopied Then Return 0

            Return CType(ClientBox.ItemValueA(0), ClientCopied).noClient
        End Get
    End Property

    Public Property blockObjectInArea() As Boolean Implements IMovableObject.blockObjectInArea
        Get
            Return _BlockObjectInArea
        End Get
        Set(ByVal Value As Boolean)
            _BlockObjectInArea = Value
        End Set
    End Property

    Public Property blockMove() As Boolean Implements IMovableObject.blockMove
        Get
            Return ControlBox.isLocked
        End Get
        Set(ByVal Value As Boolean)
            ControlBox.isLocked = Value
        End Set
    End Property
#End Region

    Public Sub loading()
        'Load Thérapeute
        therapeute.Items.Add("* Tous les thérapeutes *")
        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(False, True)
        For Each curUser As User In users
            therapeute.Items.Add(curUser.toString)
        Next

        Try
            configList(rvList)
        Catch 'REM Exception not handle
        End Try
    End Sub

    Private Sub clientBox_MouseUp(ByVal sender As Object, ByVal e As CI.Controls.List.MouseUpEventArgs) Handles ClientBox.mouseUp
        If ClientBox.listCount <= 0 Then clientBox_ItemClick(Me, New CI.Controls.List.ClickEventArgs(-1, e.button, e.x, e.y))
    End Sub

    Private Sub clientBox_ItemClick(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs) Handles ClientBox.itemClick
        If e.button = 2 Then
            menuCopier.Enabled = True
            menuColler.Enabled = True
            If ClientBox.ItemText(0) = "" Then menuCopier.Enabled = False
            If myMainWin.copyBox.clientName = "" Then menuColler.Enabled = False

            clientMenu.Show(ClientBox, New Point(e.x, e.y))
        End If
    End Sub

    Private Sub futureVisites_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Size = New Size(297, 402)
    End Sub

    Private Sub objects_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Bordure.MouseDown, Bordure2.MouseDown, adresse.MouseDown, Label1.MouseDown, Label2.MouseDown, Label3.MouseDown, tel.MouseDown, ville.MouseDown, Titre.MouseDown, Label4.MouseDown
        buttonDown = e.Button
        XObjects = e.X
        yObjects = e.Y
    End Sub

    Private Sub objects_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Bordure.MouseUp, Bordure2.MouseUp, adresse.MouseUp, Label1.MouseUp, Label2.MouseUp, Label3.MouseUp, tel.MouseUp, ville.MouseUp, Titre.MouseUp, Label4.MouseUp
        buttonDown = MouseButtons.None
    End Sub

    Private Sub objects_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Bordure.MouseMove, Bordure2.MouseMove, adresse.MouseMove, Label1.MouseMove, Label2.MouseMove, Label3.MouseMove, tel.MouseMove, ville.MouseMove, Titre.MouseMove, Label4.MouseMove
        If blockMove = True Then Exit Sub

        If blockObjectInArea = True And e.Button = System.Windows.Forms.MouseButtons.Left Then
            RaiseEvent willMove(Me, e.X, e.Y, XObjects, yObjects)
            Dim NewTop, newLeft As Integer
            NewTop = (Me.Top + (e.Y - yObjects))
            newLeft = (Me.Left + (e.X - XObjects))
            setCoord(ensureGoodCoord(NewTop, newLeft, Me.IsSwitchedToToolbar))
            RaiseEvent move(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub objects_MouseMove2(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Bordure.MouseMove, Bordure2.MouseMove, adresse.MouseMove, Label1.MouseMove, Label2.MouseMove, Label3.MouseMove, tel.MouseMove, ville.MouseMove, Titre.MouseMove, Label4.MouseMove, ClientBox.mouseMove, ControlBox.MouseMove, printRV.MouseMove, rvList.mouseMove
        Me.BringToFront()
    End Sub

    Private Function ensureGoodCoord(ByVal newTop As Integer, ByVal newLeft As Integer, ByVal isSwitchedToToolbar As Boolean) As Point Implements IMovableObject.ensureGoodCoord
        If isSwitchedToToolbar = False Then
            If Not (newTop > myMainWin.mdiChildRectangle.Top And (newTop + Me.Height) < myMainWin.mdiChildRectangle.Bottom) Then
                If newTop > myMainWin.mdiChildRectangle.Top Then
                    newTop = myMainWin.mdiChildRectangle.Bottom - Me.Height
                Else
                    newTop = myMainWin.mdiChildRectangle.Top
                End If
            End If
            If Not (newLeft > myMainWin.mdiChildRectangle.Left And (newLeft + Me.Width) < myMainWin.mdiChildRectangle.Right) Then
                If newLeft > myMainWin.mdiChildRectangle.Left Then
                    newLeft = myMainWin.mdiChildRectangle.Right - Me.Width
                Else
                    newLeft = myMainWin.mdiChildRectangle.Left
                End If
            End If
        Else
            If Not (newTop >= 0 And (newTop + Me.Height) < (myMainWin.mdiChildRectangle.Bottom - myMainWin.mdiChildRectangle.Top)) Then
                If newTop > 0 Then
                    newTop = myMainWin.mdiChildRectangle.Bottom - Me.Height - myMainWin.mdiChildRectangle.Top
                Else
                    newTop = 0
                End If
            End If
            If Not (newLeft > myMainWin.barMainObjects.getBarWidth And (newLeft + Me.Width) < myMainWin.mdiChildRectangle.Right) Then
                If newLeft > myMainWin.barMainObjects.getBarWidth Then
                    newLeft = myMainWin.mdiChildRectangle.Right - Me.Width
                Else
                    newLeft = myMainWin.barMainObjects.getBarWidth
                End If
            End If
        End If

        Return New Point(newLeft, newTop)
    End Function

    Private Sub printRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles printRV.Click
        If Me.noClient = 0 Then
            MessageBox.Show("Veuillez sélectionner un client", "Client manquant")
            Exit Sub
        End If

        localLogNumber += 1
        logToLocal("Futur RV " & localLogNumber & " : Before creating report")
        Dim newRVRapport As Report = createRapport(Me.noClient, Me.AskExtraQuestion.Checked)
        logToLocal("Futur RV " & localLogNumber & " : After creating report")
        newRVRapport.print(True, True)
        logToLocal("Futur RV " & localLogNumber & " : After printing report")
    End Sub

    Private Function createRapport(ByVal noClient As Integer, Optional ByVal askExtraQuestion As Boolean = True) As Report
        Dim rapFiltering As New FilteringComposite
        Dim fnc As New FilteringNoClient
        fnc.noClient = noClient
        fnc.currentReturn.clientFullName = Me.ClientBox.ItemText(0)
        rapFiltering.add(fnc)

        Dim myRapport As Report = ReportsManager.getInstance.createReport("Rendez-vous futurs d'un client", rapFiltering)
        If askExtraQuestion = False Then CType(myRapport.footer, ReportFooterSimple).extraFieldType = "NONE"
        Return myRapport
    End Function

    Public Sub loadVisites()
        'REM_CODES
        rvList.cls()
        Dim caption As String
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True
        Dim myTRP As String
        Dim visites As Generic.List(Of RendezVous) = RendezVousManager.getInstance.loadRendezVous(noClient, Date.Today.AddDays(1), , False)

        Dim i, n As Integer
        n = 0
        Dim periodeTranslation() As String = {"15 minutes", "30 minutes", "45 minutes", "1 heure", "1h15min", "1h30min", "1h45min", "2 heures"}
        Dim myBackColor As Color = Nothing
        Dim curFolderCode As Accounts.Clients.Folders.Codifications.FolderCode
        Dim rv As RendezVous
        Dim myTTT As String
        Dim iconsShowed As New Generic.List(Of Boolean)
        iconsShowed.Add(True)
        myBackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors2"))
        For i = 0 To visites.Count - 1
            rv = visites(i)
            myTRP = UsersManager.getInstance.getUser(rv.noTRP).toString()
            If myTRP = therapeute.Text Or therapeute.SelectedIndex = 0 Or therapeute.SelectedIndex = -1 Then
                caption = DateFormat.getTextDate(rv.dateHeure, DateFormat.TextDateOptions.YYYYMMDDShortDayName) & " " & DateFormat.getTextDate(rv.dateHeure, DateFormat.TextDateOptions.ShortTime) & " (" & rv.noFolder & "-" & rv.getFolderCode().name & ")" & vbCrLf & periodeTranslation(rv.period / 15 - 1) & " de " & rv.service.ToLower
                If therapeute.GetItemText(therapeute.SelectedItem).StartsWith("*") Then caption &= " par " & myTRP
                n = rvList.add(caption)
                rvList.ItemBackColor(n) = myBackColor
                rvList.ItemValueA(n) = rv

                curFolderCode = rv.getFolderCode

                'Modifie la case selon les settings des préférences si le RV n'est pas confirmé et qu'il requiert de l'être dépendamment de la codification et si le RV est une évaluation ou non
                If rv.confirmed = False AndAlso ((curFolderCode.confirmation = 2 And rv.evaluation = False) Or (curFolderCode.confirmation = 1 And rv.evaluation = True) Or curFolderCode.confirmation = 3) Then
                    rvList.items(n).getItems(0).iconsShowed = iconsShowed
                End If

                If rv.evaluation Then
                    Dim curFS As FontStyle = FontStyle.Regular
                    If PreferencesManager.getGeneralPreferences()("RVEvalGras") Then curFS += FontStyle.Bold
                    If PreferencesManager.getGeneralPreferences()("RVEvalItalique") Then curFS += FontStyle.Italic
                    If PreferencesManager.getGeneralPreferences()("RVEvalSouligne") Then curFS += FontStyle.Underline
                    If PreferencesManager.getGeneralPreferences()("RVEvalBarre") Then curFS += FontStyle.Strikeout

                    rvList.ItemFont(n) = New Font(PreferencesManager.getGeneralPreferences()("RVEvalFont").ToString, rvList.baseFont.Size, curFS)
                End If

                Dim specialDatesTT As String = ""
                If PreferencesManager.getGeneralPreferences()("AffSpecialDatesInAgenda") = True Then
                    Dim sd As Generic.List(Of SpecialDate) = SpecialDatesManager.getInstance.getSpecialDates(rv.dateHeure)
                    If sd.Count > 0 Then
                        specialDatesTT = " " & sd(0).nom
                        For k As Integer = 1 To sd.Count - 1
                            specialDatesTT &= " - " & sd(k).nom
                        Next k
                    End If
                End If

                Dim evalTrait As String = "Évaluation"
                If rv.evaluation = False Then evalTrait = "Traitement"
                myTTT = DateFormat.getTextDate(rv.dateHeure, DateFormat.TextDateOptions.FullDayMonthNames) & specialDatesTT & vbCrLf & rv.itemText & vbCrLf & evalTrait & " de " & rv.service & " du dossier (" & rv.noFolder & "-" & curFolderCode.name & ")" & vbCrLf & rv.telephones.Replace("§", vbCrLf)
                If rv.remarksClient.Trim <> "" Then myTTT &= vbCrLf & vbCrLf & "Remarques du compte client :" & vbCrLf & rv.remarksClient.Replace("\n", vbCrLf)
                If rv.remarksFolder.Trim <> "" Then myTTT &= vbCrLf & vbCrLf & "Remarques du dossier :" & vbCrLf & rv.remarksFolder.Replace("\n", vbCrLf)
                If rv.remarks.Trim <> "" Then myTTT &= vbCrLf & vbCrLf & "Remarques du rendez-vous :" & vbCrLf & rv.remarks

                myTTT = (DateFormat.getTextDate(rv.dateHeure, DateFormat.TextDateOptions.ShortTime) & " " & myTTT).Replace("<br>", vbCrLf)
                rvList.ItemToolTipText(n) = myTTT

                n += 1
            End If
        Next i

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        rvList.draw = True : rvList.draw = False
    End Sub

    Public Sub setRVClient(ByVal clientFullName As String, ByVal noClient As Integer)
        With ClientBox
            .cls()
            .ItemValueA(.add(clientFullName)) = New ClientCopied(noClient, clientFullName)
        End With

        Dim results(,) As String = DBLinker.getInstance.readDB("Villes RIGHT JOIN InfoClients ON Villes.NoVille = InfoClients.NoVille", "InfoClients.Adresse, Villes.NomVille, InfoClients.Telephones", "WHERE ((NoClient)=" & noClient & ");")
        Dim sTel() As String = Split(results(2, 0), "§")
        Try
            adresse.Text = results(0, 0)
            ville.Text = results(1, 0)
            If IsNothing(sTel) = False Then tel.Text = sTel(0).Replace(":", " : ")

            therapeute.SelectedIndex = 0
        Catch
            'Aucun téléphone ou aucun thérapeute
        End Try
    End Sub

    Public Sub setTRP(ByVal noTRP As Integer)
        If noTRP = 0 Then
            Me.therapeute.SelectedIndex = 0
            Exit Sub
        End If

        Me.therapeute.SelectedIndex = Me.therapeute.FindStringExact(UsersManager.getInstance.getUser(noTRP).toString())
        If Me.therapeute.SelectedIndex = -1 Then Me.therapeute.SelectedIndex = 0
    End Sub

    Private Sub therapeute_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles therapeute.SelectedIndexChanged
        If firstLoading = False Then loadVisites()
    End Sub

    Private Sub clientBox_ItemValueAChange(ByVal noItem As Integer, ByVal oldValue As Object, ByVal newValue As Object) Handles ClientBox.itemValueAChange
        firstLoading = True
        If therapeute.Items.Count > 0 Then
            If therapeute.SelectedIndex = -1 Then therapeute.SelectedIndex = 0
        Else
            therapeute.SelectedIndex = -1
        End If
        firstLoading = False
        loadVisites()
    End Sub

#Region "Context Menu on ClientBox"
    Private Sub menuSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSearch.Click
        Dim myRecherche As New clientSearch()
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.StartPosition = FormStartPosition.CenterScreen
        myRecherche.ShowDialog()
    End Sub

    Private Sub menuCopier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCopier.Click
        Dim cClient As ClientCopied = ClientBox.ItemValueA(0)
        cClient.copy()
    End Sub

    Private Sub menuColler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuColler.Click
        Dim curCopy As AgendaEntry = myMainWin.copyBox.itemValueA()
        If Not TypeOf curCopy Is RendezVous AndAlso Not TypeOf curCopy Is ClientCopied Then Exit Sub
        Dim curClient As RendezVous = curCopy

        setRVClient(curClient.clientName, curClient.noClient)
    End Sub
#End Region

    Private Sub rvList_ItemClick(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs) Handles rvList.itemClick
        If e.selectedItem < 0 Then Exit Sub

        If e.button = 2 Then
            rvList.selected = e.selectedItem
            Dim rv As RendezVous = rvList.ItemValueA(e.selectedItem)
            menuAddToQueueList.Enabled = True

            menuAddToQueueList.Enabled = Not rv.isOnQueueList()
            menupaiement.Enabled = Bill.isPaymentsToDoByClient(Me.noClient)
            menuConfirmRV.Enabled = Not rv.confirmed

            menunewrv.DefaultItem = True
            If PreferencesManager.getUserPreferences()("DblClickOnFutureRV") = "Ouvrir le compte" Then menunewrv.DefaultItem = False : menuopenaccount.DefaultItem = True

            menuclickRV.Show(rvList, New Point(e.x, e.y - rvList.vsValue))
        End If
    End Sub

#Region "Context Menu on RVList"
    Public Sub menuacopier_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuacopier.Click
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)
        rv.copy()
    End Sub

    Public Sub menuaenlever_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuaenlever.Click
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)
        rv.delete()
    End Sub

    Public Sub menuabsentmotive_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuabsentmotive.Click
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)

        Dim errorMsg As String = rv.changeStatus(Accounts.Clients.Folders.RVsStatus.RVPossibleStatuses.NotPresentMotivated)
        If errorMsg <> "" AndAlso errorMsg.Contains("annulé") = False Then MessageBox.Show(errorMsg, "Impossible de changer le statut")
    End Sub

    Private Sub menuQueueList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuQueueList.Click
        openQL()
    End Sub

    Private Sub menuAddToQueueList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAddToQueueList.Click
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)

        addToListeAttente(rv.noClient, rv.noFolder, rv.noVisite, rv.noTRP, rv.period, rv.dateHeure, ClientBox.ItemText(0))
    End Sub

    Public Sub menunewrv_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menunewrv.Click
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)

        Try
            openNewRV(rv.noClient, rv.noTRP, , rv.noFolder)
        Catch ex As Exception
            addErrorLog(New Exception("bug testing - rv.NoFolder=" & rv.noFolder, ex))
        End Try
    End Sub

    Public Sub menuopenaccount_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuopenaccount.Click
        openAccount(noClient)
    End Sub

    Private Sub menupaiement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menupaiement.Click
        Dim myPaiement As Payment = openUniqueWindow(New Payment(), "Effectuer le(s) paiement(s) de " & ClientBox.ItemText(0))
        If myPaiement.billsLoaded = False Then myPaiement.loading(noClient, FacturationBox.DedicatedType.Client)
        myPaiement.Show()
    End Sub

    Private Sub menuSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSendEmail.Click
        Dim clientEmail() As String = DBLinker.getInstance.readOneDBField("InfoClients", "Courriel", "WHERE NoClient=" & Me.noClient & ";")
        If clientEmail Is Nothing OrElse clientEmail.Length = 0 Then MessageBox.Show("Le client n'a pas de courriel", "Impossible d'envoyer") : Exit Sub
        If clientEmail(0) = "" Then MessageBox.Show("Le client n'a pas de courriel", "Impossible d'envoyer") : Exit Sub

        sendemailTo(clientEmail(0))
    End Sub

    Private Sub menuDemandeAuthorisation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDemandeAuthorisation.Click
        startClientReportGen(CType(sender, MenuItem).Text)
    End Sub

    Private Sub menuEtatCompte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuEtatCompte.Click
        startClientReportGen(CType(sender, MenuItem).Text)
    End Sub

    Private Sub menuRecusGenerated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRecusGenerated.Click
        startClientReportGen(CType(sender, MenuItem).Text)
    End Sub

    Private Sub menuRVFuturs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRVFuturs.Click
        startClientReportGen(CType(sender, MenuItem).Text, False)
    End Sub

    Private Sub menuRapClientDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuReportClientDetails.Click
        startClientReportGen(CType(sender, MenuItem).Text, False)
    End Sub

    Private Sub menuRapDossierDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuReportDossierDetails.Click
        startClientReportGen(CType(sender, MenuItem).Text)
    End Sub

    Private Sub menuRapEtatClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRapEtatClient.Click
        startClientReportGen(CType(sender, MenuItem).Text, False)
    End Sub

    Private Sub menuRapRVDossier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRapRVDossier.Click
        startClientReportGen(CType(sender, MenuItem).Text)
    End Sub

    Private Sub menuModifRemarqueRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuModifRemarqueRV.Click
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)
        rv.changeRemarks()
    End Sub

    Private Sub menuConfirmRV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuConfirmRV.Click
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)
        rv.confirm()
    End Sub
#End Region

    Private Sub clientBox_DblClick(ByVal sender As Object, ByVal e As CI.Controls.List.DblClickEventArgs) Handles ClientBox.dblClick
        menuSearch_Click(sender, e)
    End Sub

    Private Sub futureVisites_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        'Droit & Accès
        If Not currentDroitAcces Is Nothing AndAlso currentDroitAcces.Length <> 0 Then
            If currentDroitAcces(17) = False And Me.visible = True Then
                'Message & Exit
                Me.visible = False
                myMainWin.menuRVFutur.Checked = False
                MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel." & vbCrLf & "Merci!", "Droit & Accès")
                Exit Sub
            End If
        End If

        Dim changed As Boolean = rvList.reverseSorting
        configList(rvList)
        If PreferencesManager.getGeneralPreferences()("TriRVFuturs").StartsWith("A") Then
            rvList.reverseSorting = False
        Else
            rvList.reverseSorting = True
        End If

        If rvList.reverseSorting <> changed Then loadVisites()
        If myMainWin IsNot Nothing Then myMainWin.menuRVFutur.Checked = Not Me.isClosed
    End Sub

    Private Sub rvList_DblClick(ByVal sender As Object, ByVal e As CI.Controls.List.DblClickEventArgs) Handles rvList.dblClick
        If rvList.listCount < 0 Then Exit Sub
        If menunewrv.DefaultItem = True Then
            menunewrv_Click(sender, EventArgs.Empty)
        Else
            menuopenaccount_Click(sender, EventArgs.Empty)
        End If
    End Sub

    Private Sub controlBox_ClosingControl() Handles ControlBox.closingControl
        Me.visible = False
        myMainWin.menuRVFutur.Checked = False
        RaiseEvent closing(Me)
    End Sub

    Private Sub controlBox_LockingControl(ByVal willBeLocked As Boolean) Handles ControlBox.lockingControl
        If willBeLocked = True Then
            Bordure.Cursor = Cursors.Default
        Else
            Bordure.Cursor = movingCursor
        End If
    End Sub

    Public Function getCoord() As System.Drawing.Point Implements IMovableObject.getCoord
        Return Me.Location
    End Function

    Public Sub setCoord(ByVal newCoord As System.Drawing.Point) Implements IMovableObject.setCoord
        Me.Location = newCoord
    End Sub

    Public Sub setMovability(ByVal movable As Boolean) Implements IMovableObject.setMovability
        Me.Left = 0
        ControlBox.setLockability(movable)
    End Sub

    Private Sub startClientReportGen(ByVal rapportTitle As String, Optional ByVal useNoFolder As Boolean = True)
        Dim rv As RendezVous = rvList.ItemValueA(rvList.selected)
        Dim tpFilter As New FilteringComposite
        Dim noClientFilter As New FilteringNoClient
        noClientFilter.noClient = noClient
        If useNoFolder Then noClientFilter.noFolder = rv.noFolder
        noClientFilter.clientFullName = ClientBox.ItemText(0)
        tpFilter.add(noClientFilter)

        ReportGeneration.startRapportGeneration(rapportTitle, tpFilter)
    End Sub

    Public Function getBarTitle() As String Implements IControllable.getBarTitle
        Return "Rendez-vous futur"
    End Function

    Public Event barTitleChanged(ByVal sender As IControllable) Implements IControllable.barTitleChanged

    Public ReadOnly Property hasToBlink() As Boolean Implements IControllable.hasToBlink
        Get
            Return False
        End Get
    End Property

    Private Sub controlBox_SwitchingControl(ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean) Handles ControlBox.switchingControl
        RaiseEvent switchingControl(Me, willBeSwitchedToToolBar, showPanel)
        setCoord(ensureGoodCoord(Me.Top, Me.Left, willBeSwitchedToToolBar))
    End Sub

    Public Property IsSwitchedToToolbar(Optional ByVal showPanel As Boolean = True) As Boolean Implements IControllable.isSwitchedToToolbar
        Get
            Return ControlBox.IsSwitchedToToolbar
        End Get
        Set(ByVal value As Boolean)
            ControlBox.IsSwitchedToToolbar(showPanel) = value
        End Set
    End Property

    Public Event switchingControl(ByVal sender As IControllable, ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean) Implements IControllable.switchingControl

    Private _IsClosed As Boolean = True

    Public Overloads Property visible() As Boolean Implements IControllable.visible
        Get
            Return MyBase.Visible
        End Get
        Set(ByVal value As Boolean)
            Dim curValue As Boolean = MyBase.Visible
            _IsClosed = Not value
            MyBase.Visible = value
            If curValue = value Then MyBase.OnVisibleChanged(EventArgs.Empty)
        End Set
    End Property

    Public Property isClosed() As Boolean Implements IControllable.isClosed
        Get
            Return _IsClosed
        End Get
        Set(ByVal value As Boolean)
            _IsClosed = value
            MyBase.Visible = Not value
        End Set
    End Property

    Public Event closing(ByVal sender As IControllable) Implements IControllable.closing

    Public Overloads Sub focus() Implements IControllable.focus
        MyBase.Focus()
    End Sub

    Public Overrides Sub dataConsuming(ByVal dataReceived As DataInternalUpdate)
        If Me.Disposing OrElse Me.IsDisposed OrElse Me.IsHandleCreated = False Then Exit Sub

        If (dataReceived.function = "AccountsDossiers" OrElse dataReceived.function = "AccountsVisites") AndAlso Me.noClient = dataReceived.params(0) Then
            loadVisites()
        End If
    End Sub
End Class
