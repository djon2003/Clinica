Imports CI.Clinica.Accounts.Clients.Folders.RVs

Friend Class Agenda
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.Height = myMainWin.minimumMdiChildRectangle.Height
        Me.Width = myMainWin.minimumMdiChildRectangle.Width
        maxBox.Width = Me.Width / 3
        Me.Top = 0
        Me.Left = 0
        Me.DaysRectangle = New Rectangle(MSpace.Text, YDephaseBox.Text, Me.ClientRectangle.Width - MSpace.Text * 2, Me.ClientRectangle.Height - YDephaseBox.Text - MSpace.Text)

        Me.MdiParent = MyMainWin
        LinkDayListArray()
        LinkLabelsNbDaysArray()
        LinkMaxMinArray()
        LinkLignesArray()
        LinkmenujourArray()
        LinkmenusemaineArray()
        LinkFeuxArray()
        BuildMenuRapport()

        'Légende
        PlageClinique.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors1"))
        PlageLibre.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors6"))
        PlageRV.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors2"))
        PlagePresence.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors4"))
        PlageReservee.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("Colors3"))
        PlageBloquee.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorBloquee"))
        PlagePresencePayee.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorPresencePayee"))

        Dim i As Byte
        For i = 0 To 42
            With CType(Feux(i), LightSignals)
                .ColorLibre = PlageLibre.BackColor
                .ColorPresence = PlagePresence.BackColor
                .ColorRV = PlageRV.BackColor
                .ColorClinique = PlageClinique.BackColor
                .ColorPresencePaye = PlagePresencePayee.BackColor
            End With
        Next i

        'Chargement des images
        Me.choosedate.Image = DrawingManager.IconToImage(DrawingManager.GetInstance.GetIcon("calendrier32.ico"), New Size(32, 32))
        Dim myConfirmationIcon As Icon = DrawingManager.getInstance.GetIcon("Confirmation.ico")
        Dim myQLIcon As Icon = DrawingManager.ImageToIcon(DrawingManager.IconToImage(DrawingManager.getInstance.GetIcon("ql24.ico"), New Size(12, 12)))
        Dim myClientRemarkIcon As Icon = DrawingManager.ImageToIcon(DrawingManager.IconToImage(DrawingManager.getInstance.GetIcon("client-remark16.ico"), New Size(12, 12)))
        Dim myFolderRemarkIcon As Icon = DrawingManager.ImageToIcon(DrawingManager.IconToImage(DrawingManager.getInstance.GetIcon("client16-folder-remark.ico"), New Size(12, 12)))
        Dim myRVRemarkIcon As Icon = DrawingManager.ImageToIcon(DrawingManager.IconToImage(DrawingManager.getInstance.GetIcon("rv16-remark.ico"), New Size(12, 12)))

        Dim maxIcon As Image = DrawingManager.getInstance.getImage("FDehors.jpg")
        For Each curMaxMin As Button In maxMin
            'curMaxMin.Image = maxIcon
            curMaxMin.BackgroundImage = maxIcon
            curMaxMin.BackgroundImageLayout = ImageLayout.Stretch
            curMaxMin.FlatStyle = FlatStyle.Flat
            curMaxMin.FlatAppearance.BorderSize = 0
        Next
        Me._MaxMin_42.BackgroundImage = DrawingManager.getInstance.getImage("FDedans.jpg")
        Me._MaxMin_42.BackgroundImageLayout = ImageLayout.Stretch

        For Each curDay As Controls.List In dayList
            curDay.icons = New Generic.List(Of Icon)
            curDay.icons.Add(myConfirmationIcon)
            curDay.icons.Add(myQLIcon)
            curDay.icons.Add(myClientRemarkIcon)
            curDay.icons.Add(myFolderRemarkIcon)
            curDay.icons.Add(myRVRemarkIcon)
            curDay.defaultIconsPosition = CI.Controls.Icons.IconPositions.OverText
        Next

        Me.Icon = DrawingManager.ImageToIcon(DrawingManager.GetInstance.GetImage("agenda.gif"))
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If Disposing Then
            LastAgendaListSel = Nothing
            For i As Integer = 0 To Me.menuRapports.DropDownItems.Count - 1
                RemoveHandler Me.menuRapports.DropDownItems(i).Click, AddressOf menuRapportWithFolder_Click
                RemoveHandler Me.menuRapports.DropDownItems(i).Click, AddressOf menuRapportWOFolder_Click
            Next i


            For i As Integer = 0 To DayList.Count - 1
                RemoveHandler DayList(i).ClickEnabledChange, AddressOf DayList_ClickEnabledChange
                RemoveHandler DayList(i).DblClick, AddressOf DayList_DblClick
                RemoveHandler DayList(i).ItemClick, AddressOf DayList_Click
                RemoveHandler DayList(i).KeyDown, AddressOf DayList_KeyDown
                RemoveHandler DayList(i).MouseDown, AddressOf DayList_MouseDown
                RemoveHandler DayList(i).MouseUp, AddressOf DayList_MouseUp
                RemoveHandler DayList(i).MouseWheel, AddressOf DayList_MouseWheel
                RemoveHandler DayList(i).Show_Renamed, AddressOf DayList_Show_Renamed
                RemoveHandler DayList(i).WillSelect, AddressOf DayList_WillSelect
            Next i
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public feux As BaseObjArray
    Public WithEvents menusemaine As BaseObjArray
    Public WithEvents menujour As BaseObjArray
    Public WithEvents _MaxMin_42 As System.Windows.Forms.Button
    Public WithEvents _LabelsNbDays_42 As System.Windows.Forms.Label
    Public WithEvents maxBox As System.Windows.Forms.Panel
    Public WithEvents adateweek As System.Windows.Forms.Button
    Public WithEvents adateday As System.Windows.Forms.Button
    Public WithEvents pdateday As System.Windows.Forms.Button
    Public WithEvents pdateweek As System.Windows.Forms.Button
    Public WithEvents choosedate As System.Windows.Forms.Button
    Public WithEvents command2 As System.Windows.Forms.Button
    Public WithEvents command1 As System.Windows.Forms.Button
    Public WithEvents mSpace As System.Windows.Forms.TextBox
    Public WithEvents yDephaseBox As System.Windows.Forms.TextBox
    Public WithEvents sDate As System.Windows.Forms.TextBox
    Public WithEvents lWidth As System.Windows.Forms.TextBox
    Public WithEvents _AdminLabels_0 As System.Windows.Forms.Label
    Public WithEvents _AdminLabels_1 As System.Windows.Forms.Label
    Public WithEvents _AdminLabels_2 As System.Windows.Forms.Label
    Public WithEvents _AdminLabels_3 As System.Windows.Forms.Label
    Public WithEvents adminBox As System.Windows.Forms.Panel
    Public WithEvents _MaxMin_40 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_39 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_38 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_37 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_36 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_35 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_34 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_33 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_32 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_31 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_30 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_29 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_28 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_27 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_26 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_25 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_24 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_23 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_22 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_21 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_20 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_19 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_18 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_17 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_16 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_15 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_14 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_13 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_12 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_11 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_10 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_9 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_8 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_7 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_6 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_5 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_4 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_3 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_2 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_1 As System.Windows.Forms.Button
    Public WithEvents _MaxMin_0 As System.Windows.Forms.Button
    Public WithEvents line4 As System.Windows.Forms.Label
    Public WithEvents line3 As System.Windows.Forms.Label
    Public WithEvents line2 As System.Windows.Forms.Label
    Public WithEvents line1 As System.Windows.Forms.Label
    Public WithEvents legende As System.Windows.Forms.GroupBox
    Public WithEvents _MaxMin_41 As System.Windows.Forms.Button
    Public WithEvents _Labels_5 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_21 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_41 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_40 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_39 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_38 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_37 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_36 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_35 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_34 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_33 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_32 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_31 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_30 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_29 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_28 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_27 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_26 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_25 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_24 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_23 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_22 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_20 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_19 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_18 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_17 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_16 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_15 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_14 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_13 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_12 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_11 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_10 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_9 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_8 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_7 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_6 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_5 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_4 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_3 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_2 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_1 As System.Windows.Forms.Label
    Public WithEvents _LabelsNbDays_0 As System.Windows.Forms.Label
    Public WithEvents _Lignes_14 As System.Windows.Forms.Label
    Public WithEvents _Lignes_13 As System.Windows.Forms.Label
    Public WithEvents _Lignes_12 As System.Windows.Forms.Label
    Public WithEvents _Lignes_11 As System.Windows.Forms.Label
    Public WithEvents _Lignes_10 As System.Windows.Forms.Label
    Public WithEvents _Lignes_9 As System.Windows.Forms.Label
    Public WithEvents _Lignes_8 As System.Windows.Forms.Label
    Public WithEvents _Lignes_7 As System.Windows.Forms.Label
    Public WithEvents _Lignes_6 As System.Windows.Forms.Label
    Public WithEvents _Lignes_5 As System.Windows.Forms.Label
    Public WithEvents _Lignes_4 As System.Windows.Forms.Label
    Public WithEvents _Lignes_3 As System.Windows.Forms.Label
    Public WithEvents _Lignes_2 As System.Windows.Forms.Label
    Public WithEvents _Lignes_1 As System.Windows.Forms.Label
    Public WithEvents _Lignes_0 As System.Windows.Forms.Label
    Public WithEvents dayList As dayListArray
    Public WithEvents labelsNbDays As BaseObjArray
    Public WithEvents lignes As BaseObjArray
    Public WithEvents maxMin As BaseObjArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents DayList0 As CI.Controls.List
    Friend WithEvents DayList42 As CI.Controls.List
    Friend WithEvents DayList2 As CI.Controls.List
    Friend WithEvents DayList3 As CI.Controls.List
    Friend WithEvents DayList4 As CI.Controls.List
    Friend WithEvents DayList5 As CI.Controls.List
    Friend WithEvents DayList6 As CI.Controls.List
    Friend WithEvents DayList7 As CI.Controls.List
    Friend WithEvents DayList8 As CI.Controls.List
    Friend WithEvents DayList9 As CI.Controls.List
    Friend WithEvents DayList10 As CI.Controls.List
    Friend WithEvents DayList11 As CI.Controls.List
    Friend WithEvents DayList12 As CI.Controls.List
    Friend WithEvents DayList13 As CI.Controls.List
    Friend WithEvents DayList14 As CI.Controls.List
    Friend WithEvents DayList15 As CI.Controls.List
    Friend WithEvents DayList16 As CI.Controls.List
    Friend WithEvents DayList17 As CI.Controls.List
    Friend WithEvents DayList18 As CI.Controls.List
    Friend WithEvents DayList19 As CI.Controls.List
    Friend WithEvents DayList20 As CI.Controls.List
    Friend WithEvents DayList21 As CI.Controls.List
    Friend WithEvents DayList22 As CI.Controls.List
    Friend WithEvents DayList23 As CI.Controls.List
    Friend WithEvents DayList24 As CI.Controls.List
    Friend WithEvents DayList25 As CI.Controls.List
    Friend WithEvents DayList26 As CI.Controls.List
    Friend WithEvents DayList27 As CI.Controls.List
    Friend WithEvents DayList28 As CI.Controls.List
    Friend WithEvents DayList29 As CI.Controls.List
    Friend WithEvents DayList30 As CI.Controls.List
    Friend WithEvents DayList31 As CI.Controls.List
    Friend WithEvents DayList32 As CI.Controls.List
    Friend WithEvents DayList33 As CI.Controls.List
    Friend WithEvents DayList34 As CI.Controls.List
    Friend WithEvents DayList35 As CI.Controls.List
    Friend WithEvents DayList36 As CI.Controls.List
    Friend WithEvents DayList37 As CI.Controls.List
    Friend WithEvents DayList38 As CI.Controls.List
    Friend WithEvents DayList39 As CI.Controls.List
    Friend WithEvents DayList40 As CI.Controls.List
    Friend WithEvents DayList41 As CI.Controls.List
    Friend WithEvents DayList1 As CI.Controls.List
    Friend WithEvents menuclickagenda As System.Windows.Forms.MenuStrip
    Friend WithEvents menuacopier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuacoller As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuaenlever As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menunewrv As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuopenaccount As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menumodifstatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menupresent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuabsentmotive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuabsentnonmotive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menueffstatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuafonction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menumodifhorairetrp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menureserved As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents button1 As System.Windows.Forms.Button
    Friend WithEvents menupaiement As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents tl As System.Windows.Forms.Label
    Friend WithEvents menuaCouper As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menujourup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menusemaineup As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents menumois As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menujour_1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menujour_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menujour_3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menujour_4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menujour_5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menujour_6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menusemaine_1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menusemaine_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menusemaine_3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _menusemaine_4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuperiode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuchangedate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menupdateday As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuadateday As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menupdateweek As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuadateweek As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menupdatemonth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuadatemonth As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents pdatemonth As System.Windows.Forms.Button
    Public WithEvents adatemonth As System.Windows.Forms.Button
    Friend WithEvents menuModifHoraireClinique As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuQueueList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPreferences As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOpenHoraire As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOpenHoraireUpto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCloseHoraire As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuScan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuSendEmail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRapports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuAddToKeyPeople As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPrintAgenda As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuQL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuAddToQueueList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuModifyReserved As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCloseFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRVFutur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Feux42 As Clinica.LightSignals
    Friend WithEvents Feux1 As Clinica.LightSignals
    Friend WithEvents Feux2 As Clinica.LightSignals
    Friend WithEvents Feux3 As Clinica.LightSignals
    Friend WithEvents Feux4 As Clinica.LightSignals
    Friend WithEvents Feux5 As Clinica.LightSignals
    Friend WithEvents Feux6 As Clinica.LightSignals
    Friend WithEvents Feux7 As Clinica.LightSignals
    Friend WithEvents Feux8 As Clinica.LightSignals
    Friend WithEvents Feux9 As Clinica.LightSignals
    Friend WithEvents Feux10 As Clinica.LightSignals
    Friend WithEvents Feux11 As Clinica.LightSignals
    Friend WithEvents Feux12 As Clinica.LightSignals
    Friend WithEvents Feux13 As Clinica.LightSignals
    Friend WithEvents Feux14 As Clinica.LightSignals
    Friend WithEvents Feux15 As Clinica.LightSignals
    Friend WithEvents Feux16 As Clinica.LightSignals
    Friend WithEvents Feux17 As Clinica.LightSignals
    Friend WithEvents Feux18 As Clinica.LightSignals
    Friend WithEvents Feux19 As Clinica.LightSignals
    Friend WithEvents Feux20 As Clinica.LightSignals
    Friend WithEvents Feux21 As Clinica.LightSignals
    Friend WithEvents Feux22 As Clinica.LightSignals
    Friend WithEvents Feux23 As Clinica.LightSignals
    Friend WithEvents Feux24 As Clinica.LightSignals
    Friend WithEvents Feux25 As Clinica.LightSignals
    Friend WithEvents Feux26 As Clinica.LightSignals
    Friend WithEvents Feux27 As Clinica.LightSignals
    Friend WithEvents Feux28 As Clinica.LightSignals
    Friend WithEvents Feux29 As Clinica.LightSignals
    Friend WithEvents Feux30 As Clinica.LightSignals
    Friend WithEvents Feux31 As Clinica.LightSignals
    Friend WithEvents Feux32 As Clinica.LightSignals
    Friend WithEvents Feux33 As Clinica.LightSignals
    Friend WithEvents Feux34 As Clinica.LightSignals
    Friend WithEvents Feux35 As Clinica.LightSignals
    Friend WithEvents Feux36 As Clinica.LightSignals
    Friend WithEvents Feux37 As Clinica.LightSignals
    Friend WithEvents Feux38 As Clinica.LightSignals
    Friend WithEvents Feux39 As Clinica.LightSignals
    Friend WithEvents Feux40 As Clinica.LightSignals
    Friend WithEvents Feux41 As Clinica.LightSignals
    Friend WithEvents Feux0 As Clinica.LightSignals
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Chargement As System.Windows.Forms.Label
    Public WithEvents button2 As System.Windows.Forms.Button
    Friend WithEvents menuOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents plageReservee As System.Windows.Forms.Label
    Public WithEvents plageClinique As System.Windows.Forms.Label
    Public WithEvents plageLibre As System.Windows.Forms.Label
    Public WithEvents plageRV As System.Windows.Forms.Label
    Public WithEvents plagePresence As System.Windows.Forms.Label
    Friend WithEvents menuConfirmation As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents nextAgenda As System.Windows.Forms.Button
    Public WithEvents previousAgenda As System.Windows.Forms.Button
    Friend WithEvents menuSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuContextMenu As ContextMenuItem
    Public WithEvents plagePresencePayee As System.Windows.Forms.Label
    Public WithEvents plageBloquee As System.Windows.Forms.Label
    Friend WithEvents menuContinuerLaListeDattente As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menugeneraterecu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRVRemarques As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStartQL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuChangeRVRemarks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuDeleteRVRemarks As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuAnnonceClient As System.Windows.Forms.ToolStripMenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Agenda))
        Me.adateweek = New System.Windows.Forms.Button
        Me.adateday = New System.Windows.Forms.Button
        Me.pdateday = New System.Windows.Forms.Button
        Me.pdateweek = New System.Windows.Forms.Button
        Me.pdatemonth = New System.Windows.Forms.Button
        Me.adatemonth = New System.Windows.Forms.Button
        Me.maxBox = New System.Windows.Forms.Panel
        Me.Feux42 = New LightSignals
        Me.DayList42 = New CI.Controls.List
        Me._MaxMin_42 = New System.Windows.Forms.Button
        Me._LabelsNbDays_42 = New System.Windows.Forms.Label
        Me.choosedate = New System.Windows.Forms.Button
        Me.adminBox = New System.Windows.Forms.Panel
        Me.button2 = New System.Windows.Forms.Button
        Me.button1 = New System.Windows.Forms.Button
        Me.command2 = New System.Windows.Forms.Button
        Me.command1 = New System.Windows.Forms.Button
        Me.mSpace = New System.Windows.Forms.TextBox
        Me.yDephaseBox = New System.Windows.Forms.TextBox
        Me.sDate = New System.Windows.Forms.TextBox
        Me.lWidth = New System.Windows.Forms.TextBox
        Me._AdminLabels_0 = New System.Windows.Forms.Label
        Me._AdminLabels_1 = New System.Windows.Forms.Label
        Me._AdminLabels_2 = New System.Windows.Forms.Label
        Me._AdminLabels_3 = New System.Windows.Forms.Label
        Me._MaxMin_40 = New System.Windows.Forms.Button
        Me._MaxMin_39 = New System.Windows.Forms.Button
        Me._MaxMin_38 = New System.Windows.Forms.Button
        Me._MaxMin_37 = New System.Windows.Forms.Button
        Me._MaxMin_36 = New System.Windows.Forms.Button
        Me._MaxMin_35 = New System.Windows.Forms.Button
        Me._MaxMin_34 = New System.Windows.Forms.Button
        Me._MaxMin_33 = New System.Windows.Forms.Button
        Me._MaxMin_32 = New System.Windows.Forms.Button
        Me._MaxMin_31 = New System.Windows.Forms.Button
        Me._MaxMin_30 = New System.Windows.Forms.Button
        Me._MaxMin_29 = New System.Windows.Forms.Button
        Me._MaxMin_28 = New System.Windows.Forms.Button
        Me._MaxMin_27 = New System.Windows.Forms.Button
        Me._MaxMin_26 = New System.Windows.Forms.Button
        Me._MaxMin_25 = New System.Windows.Forms.Button
        Me._MaxMin_24 = New System.Windows.Forms.Button
        Me._MaxMin_23 = New System.Windows.Forms.Button
        Me._MaxMin_22 = New System.Windows.Forms.Button
        Me._MaxMin_21 = New System.Windows.Forms.Button
        Me._MaxMin_20 = New System.Windows.Forms.Button
        Me._MaxMin_19 = New System.Windows.Forms.Button
        Me._MaxMin_18 = New System.Windows.Forms.Button
        Me._MaxMin_17 = New System.Windows.Forms.Button
        Me._MaxMin_16 = New System.Windows.Forms.Button
        Me._MaxMin_15 = New System.Windows.Forms.Button
        Me._MaxMin_14 = New System.Windows.Forms.Button
        Me._MaxMin_13 = New System.Windows.Forms.Button
        Me._MaxMin_12 = New System.Windows.Forms.Button
        Me._MaxMin_11 = New System.Windows.Forms.Button
        Me._MaxMin_10 = New System.Windows.Forms.Button
        Me._MaxMin_9 = New System.Windows.Forms.Button
        Me._MaxMin_8 = New System.Windows.Forms.Button
        Me._MaxMin_7 = New System.Windows.Forms.Button
        Me._MaxMin_6 = New System.Windows.Forms.Button
        Me._MaxMin_5 = New System.Windows.Forms.Button
        Me._MaxMin_4 = New System.Windows.Forms.Button
        Me._MaxMin_3 = New System.Windows.Forms.Button
        Me._MaxMin_2 = New System.Windows.Forms.Button
        Me._MaxMin_1 = New System.Windows.Forms.Button
        Me._MaxMin_0 = New System.Windows.Forms.Button
        Me.legende = New System.Windows.Forms.GroupBox
        Me.plagePresencePayee = New System.Windows.Forms.Label
        Me.plageBloquee = New System.Windows.Forms.Label
        Me.plageReservee = New System.Windows.Forms.Label
        Me.plageClinique = New System.Windows.Forms.Label
        Me.plageLibre = New System.Windows.Forms.Label
        Me.plageRV = New System.Windows.Forms.Label
        Me.plagePresence = New System.Windows.Forms.Label
        Me.line4 = New System.Windows.Forms.Label
        Me.line3 = New System.Windows.Forms.Label
        Me.line2 = New System.Windows.Forms.Label
        Me.line1 = New System.Windows.Forms.Label
        Me._MaxMin_41 = New System.Windows.Forms.Button
        Me._Labels_5 = New System.Windows.Forms.Label
        Me._LabelsNbDays_21 = New System.Windows.Forms.Label
        Me._LabelsNbDays_41 = New System.Windows.Forms.Label
        Me._LabelsNbDays_40 = New System.Windows.Forms.Label
        Me._LabelsNbDays_39 = New System.Windows.Forms.Label
        Me._LabelsNbDays_38 = New System.Windows.Forms.Label
        Me._LabelsNbDays_37 = New System.Windows.Forms.Label
        Me._LabelsNbDays_36 = New System.Windows.Forms.Label
        Me._LabelsNbDays_35 = New System.Windows.Forms.Label
        Me._LabelsNbDays_34 = New System.Windows.Forms.Label
        Me._LabelsNbDays_33 = New System.Windows.Forms.Label
        Me._LabelsNbDays_32 = New System.Windows.Forms.Label
        Me._LabelsNbDays_31 = New System.Windows.Forms.Label
        Me._LabelsNbDays_30 = New System.Windows.Forms.Label
        Me._LabelsNbDays_29 = New System.Windows.Forms.Label
        Me._LabelsNbDays_28 = New System.Windows.Forms.Label
        Me._LabelsNbDays_27 = New System.Windows.Forms.Label
        Me._LabelsNbDays_26 = New System.Windows.Forms.Label
        Me._LabelsNbDays_25 = New System.Windows.Forms.Label
        Me._LabelsNbDays_24 = New System.Windows.Forms.Label
        Me._LabelsNbDays_23 = New System.Windows.Forms.Label
        Me._LabelsNbDays_22 = New System.Windows.Forms.Label
        Me._LabelsNbDays_20 = New System.Windows.Forms.Label
        Me._LabelsNbDays_19 = New System.Windows.Forms.Label
        Me._LabelsNbDays_18 = New System.Windows.Forms.Label
        Me._LabelsNbDays_17 = New System.Windows.Forms.Label
        Me._LabelsNbDays_16 = New System.Windows.Forms.Label
        Me._LabelsNbDays_15 = New System.Windows.Forms.Label
        Me._LabelsNbDays_14 = New System.Windows.Forms.Label
        Me._LabelsNbDays_13 = New System.Windows.Forms.Label
        Me._LabelsNbDays_12 = New System.Windows.Forms.Label
        Me._LabelsNbDays_11 = New System.Windows.Forms.Label
        Me._LabelsNbDays_10 = New System.Windows.Forms.Label
        Me._LabelsNbDays_9 = New System.Windows.Forms.Label
        Me._LabelsNbDays_8 = New System.Windows.Forms.Label
        Me._LabelsNbDays_7 = New System.Windows.Forms.Label
        Me._LabelsNbDays_6 = New System.Windows.Forms.Label
        Me._LabelsNbDays_5 = New System.Windows.Forms.Label
        Me._LabelsNbDays_4 = New System.Windows.Forms.Label
        Me._LabelsNbDays_3 = New System.Windows.Forms.Label
        Me._LabelsNbDays_2 = New System.Windows.Forms.Label
        Me._LabelsNbDays_1 = New System.Windows.Forms.Label
        Me._LabelsNbDays_0 = New System.Windows.Forms.Label
        Me._Lignes_14 = New System.Windows.Forms.Label
        Me._Lignes_13 = New System.Windows.Forms.Label
        Me._Lignes_12 = New System.Windows.Forms.Label
        Me._Lignes_11 = New System.Windows.Forms.Label
        Me._Lignes_10 = New System.Windows.Forms.Label
        Me._Lignes_9 = New System.Windows.Forms.Label
        Me._Lignes_8 = New System.Windows.Forms.Label
        Me._Lignes_7 = New System.Windows.Forms.Label
        Me._Lignes_6 = New System.Windows.Forms.Label
        Me._Lignes_5 = New System.Windows.Forms.Label
        Me._Lignes_4 = New System.Windows.Forms.Label
        Me._Lignes_3 = New System.Windows.Forms.Label
        Me._Lignes_2 = New System.Windows.Forms.Label
        Me._Lignes_1 = New System.Windows.Forms.Label
        Me._Lignes_0 = New System.Windows.Forms.Label
        Me.DayList0 = New CI.Controls.List
        Me.DayList2 = New CI.Controls.List
        Me.DayList3 = New CI.Controls.List
        Me.DayList4 = New CI.Controls.List
        Me.DayList5 = New CI.Controls.List
        Me.DayList6 = New CI.Controls.List
        Me.DayList7 = New CI.Controls.List
        Me.DayList8 = New CI.Controls.List
        Me.DayList9 = New CI.Controls.List
        Me.DayList10 = New CI.Controls.List
        Me.DayList11 = New CI.Controls.List
        Me.DayList12 = New CI.Controls.List
        Me.DayList13 = New CI.Controls.List
        Me.DayList14 = New CI.Controls.List
        Me.DayList15 = New CI.Controls.List
        Me.DayList16 = New CI.Controls.List
        Me.DayList17 = New CI.Controls.List
        Me.DayList18 = New CI.Controls.List
        Me.DayList19 = New CI.Controls.List
        Me.DayList20 = New CI.Controls.List
        Me.DayList21 = New CI.Controls.List
        Me.DayList22 = New CI.Controls.List
        Me.DayList23 = New CI.Controls.List
        Me.DayList24 = New CI.Controls.List
        Me.DayList25 = New CI.Controls.List
        Me.DayList26 = New CI.Controls.List
        Me.DayList27 = New CI.Controls.List
        Me.DayList28 = New CI.Controls.List
        Me.DayList29 = New CI.Controls.List
        Me.DayList30 = New CI.Controls.List
        Me.DayList31 = New CI.Controls.List
        Me.DayList32 = New CI.Controls.List
        Me.DayList33 = New CI.Controls.List
        Me.DayList34 = New CI.Controls.List
        Me.DayList35 = New CI.Controls.List
        Me.DayList36 = New CI.Controls.List
        Me.DayList37 = New CI.Controls.List
        Me.DayList38 = New CI.Controls.List
        Me.DayList39 = New CI.Controls.List
        Me.DayList40 = New CI.Controls.List
        Me.DayList41 = New CI.Controls.List
        Me.DayList1 = New CI.Controls.List
        Me.menuclickagenda = New System.Windows.Forms.MenuStrip
        Me.menuContextMenu = New ContextMenuItem
        Me.menuaCouper = New System.Windows.Forms.ToolStripMenuItem
        Me.menuacopier = New System.Windows.Forms.ToolStripMenuItem
        Me.menuacoller = New System.Windows.Forms.ToolStripMenuItem
        Me.menuaenlever = New System.Windows.Forms.ToolStripMenuItem
        Me.menuModifyReserved = New System.Windows.Forms.ToolStripMenuItem
        Me.menuSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.menuAnnonceClient = New System.Windows.Forms.ToolStripMenuItem
        Me.menuConfirmation = New System.Windows.Forms.ToolStripMenuItem
        Me.menuSendEmail = New System.Windows.Forms.ToolStripMenuItem
        Me.menuCloseHoraire = New System.Windows.Forms.ToolStripMenuItem
        Me.menuQL = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAddToQueueList = New System.Windows.Forms.ToolStripMenuItem
        Me.menuContinuerLaListeDattente = New System.Windows.Forms.ToolStripMenuItem
        Me.menuStartQL = New System.Windows.Forms.ToolStripMenuItem
        Me.menuQueueList = New System.Windows.Forms.ToolStripMenuItem
        Me.menumodifstatus = New System.Windows.Forms.ToolStripMenuItem
        Me.menupresent = New System.Windows.Forms.ToolStripMenuItem
        Me.menuabsentmotive = New System.Windows.Forms.ToolStripMenuItem
        Me.menuabsentnonmotive = New System.Windows.Forms.ToolStripMenuItem
        Me.menueffstatus = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem4 = New System.Windows.Forms.ToolStripSeparator
        Me.menuOpenFolder = New System.Windows.Forms.ToolStripMenuItem
        Me.menuCloseFolder = New System.Windows.Forms.ToolStripMenuItem
        Me.menunewrv = New System.Windows.Forms.ToolStripMenuItem
        Me.menuScan = New System.Windows.Forms.ToolStripMenuItem
        Me.menuopenaccount = New System.Windows.Forms.ToolStripMenuItem
        Me.menupaiement = New System.Windows.Forms.ToolStripMenuItem
        Me.menugeneraterecu = New System.Windows.Forms.ToolStripMenuItem
        Me.menureserved = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRapports = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRVRemarques = New System.Windows.Forms.ToolStripMenuItem
        Me.menuChangeRVRemarks = New System.Windows.Forms.ToolStripMenuItem
        Me.menuDeleteRVRemarks = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRVFutur = New System.Windows.Forms.ToolStripMenuItem
        Me.menuOpenHoraire = New System.Windows.Forms.ToolStripMenuItem
        Me.menuOpenHoraireUpto = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.menuafonction = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAddToKeyPeople = New System.Windows.Forms.ToolStripMenuItem
        Me.menuPrintAgenda = New System.Windows.Forms.ToolStripMenuItem
        Me.menumodifhorairetrp = New System.Windows.Forms.ToolStripMenuItem
        Me.menuModifHoraireClinique = New System.Windows.Forms.ToolStripMenuItem
        Me.menuPreferences = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.menuchangedate = New System.Windows.Forms.ToolStripMenuItem
        Me.menupdateday = New System.Windows.Forms.ToolStripMenuItem
        Me.menuadateday = New System.Windows.Forms.ToolStripMenuItem
        Me.menupdateweek = New System.Windows.Forms.ToolStripMenuItem
        Me.menuadateweek = New System.Windows.Forms.ToolStripMenuItem
        Me.menupdatemonth = New System.Windows.Forms.ToolStripMenuItem
        Me.menuadatemonth = New System.Windows.Forms.ToolStripMenuItem
        Me.menuperiode = New System.Windows.Forms.ToolStripMenuItem
        Me.menujourup = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_1 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_2 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_3 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_4 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_5 = New System.Windows.Forms.ToolStripMenuItem
        Me._menujour_6 = New System.Windows.Forms.ToolStripMenuItem
        Me.menusemaineup = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_1 = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_2 = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_3 = New System.Windows.Forms.ToolStripMenuItem
        Me._menusemaine_4 = New System.Windows.Forms.ToolStripMenuItem
        Me.menumois = New System.Windows.Forms.ToolStripMenuItem
        Me.tl = New System.Windows.Forms.Label
        Me.Feux1 = New LightSignals
        Me.Feux2 = New LightSignals
        Me.Feux3 = New LightSignals
        Me.Feux4 = New LightSignals
        Me.Feux5 = New LightSignals
        Me.Feux6 = New LightSignals
        Me.Feux7 = New LightSignals
        Me.Feux8 = New LightSignals
        Me.Feux9 = New LightSignals
        Me.Feux10 = New LightSignals
        Me.Feux11 = New LightSignals
        Me.Feux12 = New LightSignals
        Me.Feux13 = New LightSignals
        Me.Feux14 = New LightSignals
        Me.Feux15 = New LightSignals
        Me.Feux16 = New LightSignals
        Me.Feux17 = New LightSignals
        Me.Feux18 = New LightSignals
        Me.Feux19 = New LightSignals
        Me.Feux20 = New LightSignals
        Me.Feux21 = New LightSignals
        Me.Feux22 = New LightSignals
        Me.Feux23 = New LightSignals
        Me.Feux24 = New LightSignals
        Me.Feux25 = New LightSignals
        Me.Feux26 = New LightSignals
        Me.Feux27 = New LightSignals
        Me.Feux28 = New LightSignals
        Me.Feux29 = New LightSignals
        Me.Feux30 = New LightSignals
        Me.Feux31 = New LightSignals
        Me.Feux32 = New LightSignals
        Me.Feux33 = New LightSignals
        Me.Feux34 = New LightSignals
        Me.Feux35 = New LightSignals
        Me.Feux36 = New LightSignals
        Me.Feux37 = New LightSignals
        Me.Feux38 = New LightSignals
        Me.Feux39 = New LightSignals
        Me.Feux40 = New LightSignals
        Me.Feux41 = New LightSignals
        Me.Feux0 = New LightSignals
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.nextAgenda = New System.Windows.Forms.Button
        Me.previousAgenda = New System.Windows.Forms.Button
        Me.Chargement = New System.Windows.Forms.Label
        Me.maxBox.SuspendLayout()
        Me.adminBox.SuspendLayout()
        Me.legende.SuspendLayout()
        Me.menuclickagenda.SuspendLayout()
        Me.SuspendLayout()
        '
        'adateweek
        '
        Me.adateweek.BackColor = System.Drawing.SystemColors.Control
        Me.adateweek.Cursor = System.Windows.Forms.Cursors.Default
        Me.adateweek.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adateweek.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adateweek.Location = New System.Drawing.Point(635, 4)
        Me.adateweek.Name = "adateweek"
        Me.adateweek.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adateweek.Size = New System.Drawing.Size(32, 32)
        Me.adateweek.TabIndex = 112
        Me.adateweek.Tag = "NOHIDE"
        Me.adateweek.Text = ">>"
        Me.ToolTip1.SetToolTip(Me.adateweek, "Semaine suivante")
        Me.adateweek.UseVisualStyleBackColor = False
        '
        'adateday
        '
        Me.adateday.BackColor = System.Drawing.SystemColors.Control
        Me.adateday.Cursor = System.Windows.Forms.Cursors.Default
        Me.adateday.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adateday.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adateday.Location = New System.Drawing.Point(603, 4)
        Me.adateday.Name = "adateday"
        Me.adateday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adateday.Size = New System.Drawing.Size(32, 32)
        Me.adateday.TabIndex = 111
        Me.adateday.Tag = "NOHIDE"
        Me.adateday.Text = ">"
        Me.ToolTip1.SetToolTip(Me.adateday, "Jour suivant")
        Me.adateday.UseVisualStyleBackColor = False
        '
        'pdateday
        '
        Me.pdateday.BackColor = System.Drawing.SystemColors.Control
        Me.pdateday.Cursor = System.Windows.Forms.Cursors.Default
        Me.pdateday.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pdateday.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pdateday.Location = New System.Drawing.Point(571, 4)
        Me.pdateday.Name = "pdateday"
        Me.pdateday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pdateday.Size = New System.Drawing.Size(32, 32)
        Me.pdateday.TabIndex = 110
        Me.pdateday.Tag = "NOHIDE"
        Me.pdateday.Text = "<"
        Me.ToolTip1.SetToolTip(Me.pdateday, "Jour précédent")
        Me.pdateday.UseVisualStyleBackColor = False
        '
        'pdateweek
        '
        Me.pdateweek.BackColor = System.Drawing.SystemColors.Control
        Me.pdateweek.Cursor = System.Windows.Forms.Cursors.Default
        Me.pdateweek.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pdateweek.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pdateweek.Location = New System.Drawing.Point(539, 4)
        Me.pdateweek.Name = "pdateweek"
        Me.pdateweek.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pdateweek.Size = New System.Drawing.Size(32, 32)
        Me.pdateweek.TabIndex = 109
        Me.pdateweek.Tag = "NOHIDE"
        Me.pdateweek.Text = "<<"
        Me.ToolTip1.SetToolTip(Me.pdateweek, "Semaine précédente")
        Me.pdateweek.UseVisualStyleBackColor = False
        '
        'pdatemonth
        '
        Me.pdatemonth.BackColor = System.Drawing.SystemColors.Control
        Me.pdatemonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.pdatemonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pdatemonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pdatemonth.Location = New System.Drawing.Point(507, 4)
        Me.pdatemonth.Name = "pdatemonth"
        Me.pdatemonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pdatemonth.Size = New System.Drawing.Size(32, 32)
        Me.pdatemonth.TabIndex = 175
        Me.pdatemonth.Tag = "NOHIDE"
        Me.pdatemonth.Text = "<<<"
        Me.ToolTip1.SetToolTip(Me.pdatemonth, "Mois précédent")
        Me.pdatemonth.UseVisualStyleBackColor = False
        '
        'adatemonth
        '
        Me.adatemonth.BackColor = System.Drawing.SystemColors.Control
        Me.adatemonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.adatemonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adatemonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adatemonth.Location = New System.Drawing.Point(667, 4)
        Me.adatemonth.Name = "adatemonth"
        Me.adatemonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adatemonth.Size = New System.Drawing.Size(32, 32)
        Me.adatemonth.TabIndex = 176
        Me.adatemonth.Tag = "NOHIDE"
        Me.adatemonth.Text = ">>>"
        Me.ToolTip1.SetToolTip(Me.adatemonth, "Mois suivant")
        Me.adatemonth.UseVisualStyleBackColor = False
        '
        'maxBox
        '
        Me.maxBox.BackColor = System.Drawing.SystemColors.Control
        Me.maxBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.maxBox.Controls.Add(Me.Feux42)
        Me.maxBox.Controls.Add(Me.DayList42)
        Me.maxBox.Controls.Add(Me._MaxMin_42)
        Me.maxBox.Controls.Add(Me._LabelsNbDays_42)
        Me.maxBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.maxBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maxBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.maxBox.Location = New System.Drawing.Point(320, 80)
        Me.maxBox.Name = "maxBox"
        Me.maxBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.maxBox.Size = New System.Drawing.Size(235, 240)
        Me.maxBox.TabIndex = 113
        Me.maxBox.TabStop = True
        Me.maxBox.Tag = "NOHIDE"
        Me.maxBox.Visible = False
        '
        'Feux42
        '
        Me.Feux42.colorClinique = System.Drawing.Color.Empty
        Me.Feux42.colorLibre = System.Drawing.Color.Empty
        Me.Feux42.colorPresence = System.Drawing.Color.Empty
        Me.Feux42.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux42.colorRV = System.Drawing.Color.Empty
        Me.Feux42.Location = New System.Drawing.Point(152, 0)
        Me.Feux42.Name = "Feux42"
        Me.Feux42.Size = New System.Drawing.Size(25, 16)
        Me.Feux42.TabIndex = 134
        Me.Feux42.Tag = ""
        '
        'DayList42
        '
        Me.DayList42.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList42.autoAdjust = True
        Me.DayList42.autoKeyDownSelection = True
        Me.DayList42.autoSizeHorizontally = False
        Me.DayList42.autoSizeVertically = False
        Me.DayList42.BackColor = System.Drawing.Color.White
        Me.DayList42.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList42.baseBackColor = System.Drawing.Color.White
        Me.DayList42.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList42.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList42.bgColor = System.Drawing.Color.White
        Me.DayList42.borderColor = System.Drawing.Color.Empty
        Me.DayList42.borderSelColor = System.Drawing.Color.Empty
        Me.DayList42.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList42.CausesValidation = False
        Me.DayList42.clickEnabled = True
        Me.DayList42.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList42.do3D = False
        Me.DayList42.draw = False
        Me.DayList42.extraWidth = 0
        Me.DayList42.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList42.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList42.hScrolling = True
        Me.DayList42.hsValue = 0
        Me.DayList42.icons = CType(resources.GetObject("DayList42.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList42.itemBorder = 0
        Me.DayList42.itemMargin = 0
        Me.DayList42.items = CType(resources.GetObject("DayList42.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList42.Location = New System.Drawing.Point(0, 16)
        Me.DayList42.mouseMove3D = False
        Me.DayList42.mouseSpeed = 0
        Me.DayList42.Name = "DayList42"
        Me.DayList42.objMaxHeight = 0.0!
        Me.DayList42.objMaxWidth = 0.0!
        Me.DayList42.objMinHeight = 0.0!
        Me.DayList42.objMinWidth = 0.0!
        Me.DayList42.reverseSorting = False
        Me.DayList42.selected = -1
        Me.DayList42.selectedClickAllowed = True
        Me.DayList42.selectMultiple = False
        Me.DayList42.Size = New System.Drawing.Size(48, 72)
        Me.DayList42.sorted = False
        Me.DayList42.TabIndex = 133
        Me.DayList42.Tag = ""
        Me.DayList42.toolTipText = Nothing
        Me.DayList42.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList42.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList42.vScrolling = True
        Me.DayList42.vsValue = 0
        '
        '_MaxMin_42
        '
        Me._MaxMin_42.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_42.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_42.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_42.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_42.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_42.Location = New System.Drawing.Point(208, 0)
        Me._MaxMin_42.Name = "_MaxMin_42"
        Me._MaxMin_42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_42.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_42.TabIndex = 114
        Me._MaxMin_42.TabStop = False
        Me._MaxMin_42.Tag = ""
        Me._MaxMin_42.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_42, "Cacher la maximisation de la journée")
        Me._MaxMin_42.UseVisualStyleBackColor = False
        '
        '_LabelsNbDays_42
        '
        Me._LabelsNbDays_42.AutoSize = True
        Me._LabelsNbDays_42.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_42.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_42.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_42.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_42.Location = New System.Drawing.Point(0, 0)
        Me._LabelsNbDays_42.Name = "_LabelsNbDays_42"
        Me._LabelsNbDays_42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_42.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_42.TabIndex = 115
        Me._LabelsNbDays_42.Tag = ""
        Me._LabelsNbDays_42.Text = "2"
        '
        'choosedate
        '
        Me.choosedate.BackColor = System.Drawing.SystemColors.Control
        Me.choosedate.Cursor = System.Windows.Forms.Cursors.Default
        Me.choosedate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.choosedate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.choosedate.Location = New System.Drawing.Point(451, 0)
        Me.choosedate.Name = "choosedate"
        Me.choosedate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.choosedate.Size = New System.Drawing.Size(48, 44)
        Me.choosedate.TabIndex = 108
        Me.choosedate.Tag = "NOHIDE"
        Me.ToolTip1.SetToolTip(Me.choosedate, "Choisir la date de début de cet agenda")
        Me.choosedate.UseVisualStyleBackColor = False
        '
        'adminBox
        '
        Me.adminBox.BackColor = System.Drawing.SystemColors.Control
        Me.adminBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.adminBox.Controls.Add(Me.button2)
        Me.adminBox.Controls.Add(Me.button1)
        Me.adminBox.Controls.Add(Me.command2)
        Me.adminBox.Controls.Add(Me.command1)
        Me.adminBox.Controls.Add(Me.mSpace)
        Me.adminBox.Controls.Add(Me.yDephaseBox)
        Me.adminBox.Controls.Add(Me.sDate)
        Me.adminBox.Controls.Add(Me.lWidth)
        Me.adminBox.Controls.Add(Me._AdminLabels_0)
        Me.adminBox.Controls.Add(Me._AdminLabels_1)
        Me.adminBox.Controls.Add(Me._AdminLabels_2)
        Me.adminBox.Controls.Add(Me._AdminLabels_3)
        Me.adminBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.adminBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adminBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adminBox.Location = New System.Drawing.Point(0, 240)
        Me.adminBox.Name = "adminBox"
        Me.adminBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adminBox.Size = New System.Drawing.Size(161, 144)
        Me.adminBox.TabIndex = 42
        Me.adminBox.TabStop = True
        Me.adminBox.Tag = "NOHIDE"
        Me.adminBox.Visible = False
        '
        'button2
        '
        Me.button2.BackColor = System.Drawing.SystemColors.Control
        Me.button2.Cursor = System.Windows.Forms.Cursors.Default
        Me.button2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.button2.Location = New System.Drawing.Point(140, 0)
        Me.button2.Name = "button2"
        Me.button2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.button2.Size = New System.Drawing.Size(16, 16)
        Me.button2.TabIndex = 54
        Me.button2.Text = "X"
        Me.button2.UseVisualStyleBackColor = False
        '
        'button1
        '
        Me.button1.BackColor = System.Drawing.SystemColors.Control
        Me.button1.Cursor = System.Windows.Forms.Cursors.Default
        Me.button1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.button1.Location = New System.Drawing.Point(24, 120)
        Me.button1.Name = "button1"
        Me.button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.button1.Size = New System.Drawing.Size(49, 17)
        Me.button1.TabIndex = 53
        Me.button1.Text = "Border"
        Me.button1.UseVisualStyleBackColor = False
        '
        'command2
        '
        Me.command2.BackColor = System.Drawing.SystemColors.Control
        Me.command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.command2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.command2.Location = New System.Drawing.Point(80, 96)
        Me.command2.Name = "command2"
        Me.command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.command2.Size = New System.Drawing.Size(49, 17)
        Me.command2.TabIndex = 52
        Me.command2.Text = "CLS"
        Me.command2.UseVisualStyleBackColor = False
        '
        'command1
        '
        Me.command1.BackColor = System.Drawing.SystemColors.Control
        Me.command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.command1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.command1.Location = New System.Drawing.Point(24, 96)
        Me.command1.Name = "command1"
        Me.command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.command1.Size = New System.Drawing.Size(49, 17)
        Me.command1.TabIndex = 51
        Me.command1.Text = "Add"
        Me.command1.UseVisualStyleBackColor = False
        '
        'mSpace
        '
        Me.mSpace.AcceptsReturn = True
        Me.mSpace.BackColor = System.Drawing.SystemColors.Window
        Me.mSpace.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mSpace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mSpace.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mSpace.Location = New System.Drawing.Point(80, 0)
        Me.mSpace.MaxLength = 0
        Me.mSpace.Name = "mSpace"
        Me.mSpace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mSpace.Size = New System.Drawing.Size(57, 20)
        Me.mSpace.TabIndex = 46
        Me.mSpace.Text = "5"
        '
        'yDephaseBox
        '
        Me.yDephaseBox.AcceptsReturn = True
        Me.yDephaseBox.BackColor = System.Drawing.SystemColors.Window
        Me.yDephaseBox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.yDephaseBox.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.yDephaseBox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.yDephaseBox.Location = New System.Drawing.Point(80, 24)
        Me.yDephaseBox.MaxLength = 0
        Me.yDephaseBox.Name = "yDephaseBox"
        Me.yDephaseBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.yDephaseBox.Size = New System.Drawing.Size(57, 20)
        Me.yDephaseBox.TabIndex = 45
        Me.yDephaseBox.Text = "45"
        '
        'sDate
        '
        Me.sDate.AcceptsReturn = True
        Me.sDate.BackColor = System.Drawing.SystemColors.Window
        Me.sDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.sDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.sDate.Location = New System.Drawing.Point(80, 48)
        Me.sDate.MaxLength = 0
        Me.sDate.Name = "sDate"
        Me.sDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.sDate.Size = New System.Drawing.Size(57, 20)
        Me.sDate.TabIndex = 44
        Me.sDate.Text = "4"
        '
        'lWidth
        '
        Me.lWidth.AcceptsReturn = True
        Me.lWidth.BackColor = System.Drawing.SystemColors.Window
        Me.lWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lWidth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lWidth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lWidth.Location = New System.Drawing.Point(80, 72)
        Me.lWidth.MaxLength = 0
        Me.lWidth.Name = "lWidth"
        Me.lWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lWidth.Size = New System.Drawing.Size(57, 20)
        Me.lWidth.TabIndex = 43
        Me.lWidth.Text = "0"
        '
        '_AdminLabels_0
        '
        Me._AdminLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._AdminLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._AdminLabels_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._AdminLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._AdminLabels_0.Location = New System.Drawing.Point(0, 0)
        Me._AdminLabels_0.Name = "_AdminLabels_0"
        Me._AdminLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._AdminLabels_0.Size = New System.Drawing.Size(81, 17)
        Me._AdminLabels_0.TabIndex = 50
        Me._AdminLabels_0.Text = "MinimumSpace"
        '
        '_AdminLabels_1
        '
        Me._AdminLabels_1.AutoSize = True
        Me._AdminLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._AdminLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._AdminLabels_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._AdminLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._AdminLabels_1.Location = New System.Drawing.Point(24, 24)
        Me._AdminLabels_1.Name = "_AdminLabels_1"
        Me._AdminLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._AdminLabels_1.Size = New System.Drawing.Size(58, 14)
        Me._AdminLabels_1.TabIndex = 49
        Me._AdminLabels_1.Text = "YDephase"
        '
        '_AdminLabels_2
        '
        Me._AdminLabels_2.AutoSize = True
        Me._AdminLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._AdminLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._AdminLabels_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._AdminLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._AdminLabels_2.Location = New System.Drawing.Point(24, 48)
        Me._AdminLabels_2.Name = "_AdminLabels_2"
        Me._AdminLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._AdminLabels_2.Size = New System.Drawing.Size(60, 14)
        Me._AdminLabels_2.TabIndex = 48
        Me._AdminLabels_2.Text = "DateSpace"
        '
        '_AdminLabels_3
        '
        Me._AdminLabels_3.AutoSize = True
        Me._AdminLabels_3.BackColor = System.Drawing.SystemColors.Control
        Me._AdminLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._AdminLabels_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._AdminLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._AdminLabels_3.Location = New System.Drawing.Point(32, 72)
        Me._AdminLabels_3.Name = "_AdminLabels_3"
        Me._AdminLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._AdminLabels_3.Size = New System.Drawing.Size(54, 14)
        Me._AdminLabels_3.TabIndex = 47
        Me._AdminLabels_3.Text = "LineWidth"
        '
        '_MaxMin_40
        '
        Me._MaxMin_40.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_40.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_40.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_40.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_40.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_40.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_40.Name = "_MaxMin_40"
        Me._MaxMin_40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_40.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_40.TabIndex = 106
        Me._MaxMin_40.TabStop = False
        Me._MaxMin_40.Tag = "0"
        Me._MaxMin_40.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_40, "Maximiser la journée")
        Me._MaxMin_40.UseVisualStyleBackColor = False
        Me._MaxMin_40.Visible = False
        '
        '_MaxMin_39
        '
        Me._MaxMin_39.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_39.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_39.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_39.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_39.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_39.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_39.Name = "_MaxMin_39"
        Me._MaxMin_39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_39.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_39.TabIndex = 105
        Me._MaxMin_39.TabStop = False
        Me._MaxMin_39.Tag = "0"
        Me._MaxMin_39.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_39, "Maximiser la journée")
        Me._MaxMin_39.UseVisualStyleBackColor = False
        Me._MaxMin_39.Visible = False
        '
        '_MaxMin_38
        '
        Me._MaxMin_38.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_38.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_38.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_38.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_38.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_38.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_38.Name = "_MaxMin_38"
        Me._MaxMin_38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_38.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_38.TabIndex = 104
        Me._MaxMin_38.TabStop = False
        Me._MaxMin_38.Tag = "0"
        Me._MaxMin_38.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_38, "Maximiser la journée")
        Me._MaxMin_38.UseVisualStyleBackColor = False
        Me._MaxMin_38.Visible = False
        '
        '_MaxMin_37
        '
        Me._MaxMin_37.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_37.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_37.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_37.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_37.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_37.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_37.Name = "_MaxMin_37"
        Me._MaxMin_37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_37.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_37.TabIndex = 103
        Me._MaxMin_37.TabStop = False
        Me._MaxMin_37.Tag = "0"
        Me._MaxMin_37.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_37, "Maximiser la journée")
        Me._MaxMin_37.UseVisualStyleBackColor = False
        Me._MaxMin_37.Visible = False
        '
        '_MaxMin_36
        '
        Me._MaxMin_36.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_36.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_36.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_36.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_36.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_36.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_36.Name = "_MaxMin_36"
        Me._MaxMin_36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_36.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_36.TabIndex = 102
        Me._MaxMin_36.TabStop = False
        Me._MaxMin_36.Tag = "0"
        Me._MaxMin_36.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_36, "Maximiser la journée")
        Me._MaxMin_36.UseVisualStyleBackColor = False
        Me._MaxMin_36.Visible = False
        '
        '_MaxMin_35
        '
        Me._MaxMin_35.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_35.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_35.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_35.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_35.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_35.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_35.Name = "_MaxMin_35"
        Me._MaxMin_35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_35.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_35.TabIndex = 101
        Me._MaxMin_35.TabStop = False
        Me._MaxMin_35.Tag = "0"
        Me._MaxMin_35.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_35, "Maximiser la journée")
        Me._MaxMin_35.UseVisualStyleBackColor = False
        Me._MaxMin_35.Visible = False
        '
        '_MaxMin_34
        '
        Me._MaxMin_34.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_34.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_34.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_34.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_34.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_34.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_34.Name = "_MaxMin_34"
        Me._MaxMin_34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_34.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_34.TabIndex = 100
        Me._MaxMin_34.TabStop = False
        Me._MaxMin_34.Tag = "0"
        Me._MaxMin_34.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_34, "Maximiser la journée")
        Me._MaxMin_34.UseVisualStyleBackColor = False
        Me._MaxMin_34.Visible = False
        '
        '_MaxMin_33
        '
        Me._MaxMin_33.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_33.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_33.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_33.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_33.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_33.Name = "_MaxMin_33"
        Me._MaxMin_33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_33.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_33.TabIndex = 99
        Me._MaxMin_33.TabStop = False
        Me._MaxMin_33.Tag = "0"
        Me._MaxMin_33.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_33, "Maximiser la journée")
        Me._MaxMin_33.UseVisualStyleBackColor = False
        Me._MaxMin_33.Visible = False
        '
        '_MaxMin_32
        '
        Me._MaxMin_32.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_32.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_32.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_32.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_32.Name = "_MaxMin_32"
        Me._MaxMin_32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_32.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_32.TabIndex = 98
        Me._MaxMin_32.TabStop = False
        Me._MaxMin_32.Tag = "0"
        Me._MaxMin_32.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_32, "Maximiser la journée")
        Me._MaxMin_32.UseVisualStyleBackColor = False
        Me._MaxMin_32.Visible = False
        '
        '_MaxMin_31
        '
        Me._MaxMin_31.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_31.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_31.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_31.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_31.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_31.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_31.Name = "_MaxMin_31"
        Me._MaxMin_31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_31.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_31.TabIndex = 97
        Me._MaxMin_31.TabStop = False
        Me._MaxMin_31.Tag = "0"
        Me._MaxMin_31.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_31, "Maximiser la journée")
        Me._MaxMin_31.UseVisualStyleBackColor = False
        Me._MaxMin_31.Visible = False
        '
        '_MaxMin_30
        '
        Me._MaxMin_30.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_30.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_30.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_30.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_30.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_30.Name = "_MaxMin_30"
        Me._MaxMin_30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_30.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_30.TabIndex = 96
        Me._MaxMin_30.TabStop = False
        Me._MaxMin_30.Tag = "0"
        Me._MaxMin_30.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_30, "Maximiser la journée")
        Me._MaxMin_30.UseVisualStyleBackColor = False
        Me._MaxMin_30.Visible = False
        '
        '_MaxMin_29
        '
        Me._MaxMin_29.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_29.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_29.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_29.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_29.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_29.Name = "_MaxMin_29"
        Me._MaxMin_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_29.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_29.TabIndex = 95
        Me._MaxMin_29.TabStop = False
        Me._MaxMin_29.Tag = "0"
        Me._MaxMin_29.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_29, "Maximiser la journée")
        Me._MaxMin_29.UseVisualStyleBackColor = False
        Me._MaxMin_29.Visible = False
        '
        '_MaxMin_28
        '
        Me._MaxMin_28.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_28.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_28.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_28.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_28.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_28.Name = "_MaxMin_28"
        Me._MaxMin_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_28.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_28.TabIndex = 94
        Me._MaxMin_28.TabStop = False
        Me._MaxMin_28.Tag = "0"
        Me._MaxMin_28.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_28, "Maximiser la journée")
        Me._MaxMin_28.UseVisualStyleBackColor = False
        Me._MaxMin_28.Visible = False
        '
        '_MaxMin_27
        '
        Me._MaxMin_27.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_27.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_27.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_27.Name = "_MaxMin_27"
        Me._MaxMin_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_27.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_27.TabIndex = 93
        Me._MaxMin_27.TabStop = False
        Me._MaxMin_27.Tag = "0"
        Me._MaxMin_27.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_27, "Maximiser la journée")
        Me._MaxMin_27.UseVisualStyleBackColor = False
        Me._MaxMin_27.Visible = False
        '
        '_MaxMin_26
        '
        Me._MaxMin_26.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_26.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_26.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_26.Name = "_MaxMin_26"
        Me._MaxMin_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_26.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_26.TabIndex = 92
        Me._MaxMin_26.TabStop = False
        Me._MaxMin_26.Tag = "0"
        Me._MaxMin_26.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_26, "Maximiser la journée")
        Me._MaxMin_26.UseVisualStyleBackColor = False
        Me._MaxMin_26.Visible = False
        '
        '_MaxMin_25
        '
        Me._MaxMin_25.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_25.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_25.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_25.Name = "_MaxMin_25"
        Me._MaxMin_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_25.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_25.TabIndex = 91
        Me._MaxMin_25.TabStop = False
        Me._MaxMin_25.Tag = "0"
        Me._MaxMin_25.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_25, "Maximiser la journée")
        Me._MaxMin_25.UseVisualStyleBackColor = False
        Me._MaxMin_25.Visible = False
        '
        '_MaxMin_24
        '
        Me._MaxMin_24.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_24.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_24.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_24.Name = "_MaxMin_24"
        Me._MaxMin_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_24.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_24.TabIndex = 90
        Me._MaxMin_24.TabStop = False
        Me._MaxMin_24.Tag = "0"
        Me._MaxMin_24.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_24, "Maximiser la journée")
        Me._MaxMin_24.UseVisualStyleBackColor = False
        Me._MaxMin_24.Visible = False
        '
        '_MaxMin_23
        '
        Me._MaxMin_23.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_23.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_23.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_23.Name = "_MaxMin_23"
        Me._MaxMin_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_23.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_23.TabIndex = 89
        Me._MaxMin_23.TabStop = False
        Me._MaxMin_23.Tag = "0"
        Me._MaxMin_23.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_23, "Maximiser la journée")
        Me._MaxMin_23.UseVisualStyleBackColor = False
        Me._MaxMin_23.Visible = False
        '
        '_MaxMin_22
        '
        Me._MaxMin_22.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_22.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_22.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_22.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_22.Name = "_MaxMin_22"
        Me._MaxMin_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_22.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_22.TabIndex = 88
        Me._MaxMin_22.TabStop = False
        Me._MaxMin_22.Tag = "0"
        Me._MaxMin_22.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_22, "Maximiser la journée")
        Me._MaxMin_22.UseVisualStyleBackColor = False
        Me._MaxMin_22.Visible = False
        '
        '_MaxMin_21
        '
        Me._MaxMin_21.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_21.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_21.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_21.Name = "_MaxMin_21"
        Me._MaxMin_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_21.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_21.TabIndex = 87
        Me._MaxMin_21.TabStop = False
        Me._MaxMin_21.Tag = "0"
        Me._MaxMin_21.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_21, "Maximiser la journée")
        Me._MaxMin_21.UseVisualStyleBackColor = False
        Me._MaxMin_21.Visible = False
        '
        '_MaxMin_20
        '
        Me._MaxMin_20.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_20.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_20.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_20.Name = "_MaxMin_20"
        Me._MaxMin_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_20.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_20.TabIndex = 86
        Me._MaxMin_20.TabStop = False
        Me._MaxMin_20.Tag = "0"
        Me._MaxMin_20.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_20, "Maximiser la journée")
        Me._MaxMin_20.UseVisualStyleBackColor = False
        Me._MaxMin_20.Visible = False
        '
        '_MaxMin_19
        '
        Me._MaxMin_19.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_19.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_19.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_19.Name = "_MaxMin_19"
        Me._MaxMin_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_19.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_19.TabIndex = 85
        Me._MaxMin_19.TabStop = False
        Me._MaxMin_19.Tag = "0"
        Me._MaxMin_19.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_19, "Maximiser la journée")
        Me._MaxMin_19.UseVisualStyleBackColor = False
        Me._MaxMin_19.Visible = False
        '
        '_MaxMin_18
        '
        Me._MaxMin_18.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_18.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_18.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_18.Name = "_MaxMin_18"
        Me._MaxMin_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_18.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_18.TabIndex = 84
        Me._MaxMin_18.TabStop = False
        Me._MaxMin_18.Tag = "0"
        Me._MaxMin_18.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_18, "Maximiser la journée")
        Me._MaxMin_18.UseVisualStyleBackColor = False
        Me._MaxMin_18.Visible = False
        '
        '_MaxMin_17
        '
        Me._MaxMin_17.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_17.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_17.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_17.Name = "_MaxMin_17"
        Me._MaxMin_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_17.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_17.TabIndex = 83
        Me._MaxMin_17.TabStop = False
        Me._MaxMin_17.Tag = "0"
        Me._MaxMin_17.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_17, "Maximiser la journée")
        Me._MaxMin_17.UseVisualStyleBackColor = False
        Me._MaxMin_17.Visible = False
        '
        '_MaxMin_16
        '
        Me._MaxMin_16.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_16.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_16.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_16.Name = "_MaxMin_16"
        Me._MaxMin_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_16.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_16.TabIndex = 82
        Me._MaxMin_16.TabStop = False
        Me._MaxMin_16.Tag = "0"
        Me._MaxMin_16.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_16, "Maximiser la journée")
        Me._MaxMin_16.UseVisualStyleBackColor = False
        Me._MaxMin_16.Visible = False
        '
        '_MaxMin_15
        '
        Me._MaxMin_15.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_15.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_15.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_15.Name = "_MaxMin_15"
        Me._MaxMin_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_15.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_15.TabIndex = 81
        Me._MaxMin_15.TabStop = False
        Me._MaxMin_15.Tag = "0"
        Me._MaxMin_15.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_15, "Maximiser la journée")
        Me._MaxMin_15.UseVisualStyleBackColor = False
        Me._MaxMin_15.Visible = False
        '
        '_MaxMin_14
        '
        Me._MaxMin_14.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_14.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_14.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_14.Name = "_MaxMin_14"
        Me._MaxMin_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_14.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_14.TabIndex = 80
        Me._MaxMin_14.TabStop = False
        Me._MaxMin_14.Tag = "0"
        Me._MaxMin_14.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_14, "Maximiser la journée")
        Me._MaxMin_14.UseVisualStyleBackColor = False
        Me._MaxMin_14.Visible = False
        '
        '_MaxMin_13
        '
        Me._MaxMin_13.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_13.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_13.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_13.Name = "_MaxMin_13"
        Me._MaxMin_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_13.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_13.TabIndex = 79
        Me._MaxMin_13.TabStop = False
        Me._MaxMin_13.Tag = "0"
        Me._MaxMin_13.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_13, "Maximiser la journée")
        Me._MaxMin_13.UseVisualStyleBackColor = False
        Me._MaxMin_13.Visible = False
        '
        '_MaxMin_12
        '
        Me._MaxMin_12.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_12.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_12.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_12.Name = "_MaxMin_12"
        Me._MaxMin_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_12.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_12.TabIndex = 78
        Me._MaxMin_12.TabStop = False
        Me._MaxMin_12.Tag = "0"
        Me._MaxMin_12.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_12, "Maximiser la journée")
        Me._MaxMin_12.UseVisualStyleBackColor = False
        Me._MaxMin_12.Visible = False
        '
        '_MaxMin_11
        '
        Me._MaxMin_11.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_11.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_11.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_11.Name = "_MaxMin_11"
        Me._MaxMin_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_11.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_11.TabIndex = 77
        Me._MaxMin_11.TabStop = False
        Me._MaxMin_11.Tag = "0"
        Me._MaxMin_11.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_11, "Maximiser la journée")
        Me._MaxMin_11.UseVisualStyleBackColor = False
        Me._MaxMin_11.Visible = False
        '
        '_MaxMin_10
        '
        Me._MaxMin_10.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_10.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_10.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_10.Name = "_MaxMin_10"
        Me._MaxMin_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_10.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_10.TabIndex = 76
        Me._MaxMin_10.TabStop = False
        Me._MaxMin_10.Tag = "0"
        Me._MaxMin_10.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_10, "Maximiser la journée")
        Me._MaxMin_10.UseVisualStyleBackColor = False
        Me._MaxMin_10.Visible = False
        '
        '_MaxMin_9
        '
        Me._MaxMin_9.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_9.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_9.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_9.Name = "_MaxMin_9"
        Me._MaxMin_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_9.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_9.TabIndex = 75
        Me._MaxMin_9.TabStop = False
        Me._MaxMin_9.Tag = "0"
        Me._MaxMin_9.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_9, "Maximiser la journée")
        Me._MaxMin_9.UseVisualStyleBackColor = False
        Me._MaxMin_9.Visible = False
        '
        '_MaxMin_8
        '
        Me._MaxMin_8.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_8.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_8.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_8.Name = "_MaxMin_8"
        Me._MaxMin_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_8.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_8.TabIndex = 74
        Me._MaxMin_8.TabStop = False
        Me._MaxMin_8.Tag = "0"
        Me._MaxMin_8.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_8, "Maximiser la journée")
        Me._MaxMin_8.UseVisualStyleBackColor = False
        Me._MaxMin_8.Visible = False
        '
        '_MaxMin_7
        '
        Me._MaxMin_7.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_7.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_7.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_7.Name = "_MaxMin_7"
        Me._MaxMin_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_7.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_7.TabIndex = 73
        Me._MaxMin_7.TabStop = False
        Me._MaxMin_7.Tag = "0"
        Me._MaxMin_7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_7, "Maximiser la journée")
        Me._MaxMin_7.UseVisualStyleBackColor = False
        Me._MaxMin_7.Visible = False
        '
        '_MaxMin_6
        '
        Me._MaxMin_6.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_6.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_6.Name = "_MaxMin_6"
        Me._MaxMin_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_6.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_6.TabIndex = 72
        Me._MaxMin_6.TabStop = False
        Me._MaxMin_6.Tag = "0"
        Me._MaxMin_6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_6, "Maximiser la journée")
        Me._MaxMin_6.UseVisualStyleBackColor = False
        Me._MaxMin_6.Visible = False
        '
        '_MaxMin_5
        '
        Me._MaxMin_5.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_5.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_5.Name = "_MaxMin_5"
        Me._MaxMin_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_5.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_5.TabIndex = 71
        Me._MaxMin_5.TabStop = False
        Me._MaxMin_5.Tag = "0"
        Me._MaxMin_5.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_5, "Maximiser la journée")
        Me._MaxMin_5.UseVisualStyleBackColor = False
        Me._MaxMin_5.Visible = False
        '
        '_MaxMin_4
        '
        Me._MaxMin_4.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_4.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_4.Name = "_MaxMin_4"
        Me._MaxMin_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_4.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_4.TabIndex = 70
        Me._MaxMin_4.TabStop = False
        Me._MaxMin_4.Tag = "0"
        Me._MaxMin_4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_4, "Maximiser la journée")
        Me._MaxMin_4.UseVisualStyleBackColor = False
        Me._MaxMin_4.Visible = False
        '
        '_MaxMin_3
        '
        Me._MaxMin_3.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_3.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_3.Name = "_MaxMin_3"
        Me._MaxMin_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_3.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_3.TabIndex = 69
        Me._MaxMin_3.TabStop = False
        Me._MaxMin_3.Tag = "0"
        Me._MaxMin_3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_3, "Maximiser la journée")
        Me._MaxMin_3.UseVisualStyleBackColor = False
        Me._MaxMin_3.Visible = False
        '
        '_MaxMin_2
        '
        Me._MaxMin_2.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_2.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_2.Name = "_MaxMin_2"
        Me._MaxMin_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_2.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_2.TabIndex = 68
        Me._MaxMin_2.TabStop = False
        Me._MaxMin_2.Tag = "0"
        Me._MaxMin_2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_2, "Maximiser la journée")
        Me._MaxMin_2.UseVisualStyleBackColor = False
        Me._MaxMin_2.Visible = False
        '
        '_MaxMin_1
        '
        Me._MaxMin_1.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_1.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_1.Name = "_MaxMin_1"
        Me._MaxMin_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_1.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_1.TabIndex = 67
        Me._MaxMin_1.TabStop = False
        Me._MaxMin_1.Tag = "0"
        Me._MaxMin_1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_1, "Maximiser la journée")
        Me._MaxMin_1.UseVisualStyleBackColor = False
        Me._MaxMin_1.Visible = False
        '
        '_MaxMin_0
        '
        Me._MaxMin_0.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_0.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_0.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_0.Name = "_MaxMin_0"
        Me._MaxMin_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_0.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_0.TabIndex = 66
        Me._MaxMin_0.TabStop = False
        Me._MaxMin_0.Tag = "0"
        Me._MaxMin_0.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_0, "Maximiser la journée")
        Me._MaxMin_0.UseVisualStyleBackColor = False
        Me._MaxMin_0.Visible = False
        '
        'legende
        '
        Me.legende.BackColor = System.Drawing.SystemColors.Control
        Me.legende.Controls.Add(Me.plagePresencePayee)
        Me.legende.Controls.Add(Me.plageBloquee)
        Me.legende.Controls.Add(Me.plageReservee)
        Me.legende.Controls.Add(Me.plageClinique)
        Me.legende.Controls.Add(Me.plageLibre)
        Me.legende.Controls.Add(Me.plageRV)
        Me.legende.Controls.Add(Me.plagePresence)
        Me.legende.Controls.Add(Me.line4)
        Me.legende.Controls.Add(Me.line3)
        Me.legende.Controls.Add(Me.line2)
        Me.legende.Controls.Add(Me.line1)
        Me.legende.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.legende.ForeColor = System.Drawing.SystemColors.ControlText
        Me.legende.Location = New System.Drawing.Point(8, 0)
        Me.legende.Name = "legende"
        Me.legende.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.legende.Size = New System.Drawing.Size(407, 44)
        Me.legende.TabIndex = 55
        Me.legende.TabStop = False
        Me.legende.Tag = "NOHIDE"
        Me.legende.Text = "Légende"
        '
        'plagePresencePayee
        '
        Me.plagePresencePayee.AutoSize = True
        Me.plagePresencePayee.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.plagePresencePayee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plagePresencePayee.Cursor = System.Windows.Forms.Cursors.Default
        Me.plagePresencePayee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plagePresencePayee.ForeColor = System.Drawing.SystemColors.ControlText
        Me.plagePresencePayee.Location = New System.Drawing.Point(194, 18)
        Me.plagePresencePayee.Name = "plagePresencePayee"
        Me.plagePresencePayee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.plagePresencePayee.Size = New System.Drawing.Size(88, 16)
        Me.plagePresencePayee.TabIndex = 65
        Me.plagePresencePayee.Text = "Présence payée"
        Me.plagePresencePayee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plageBloquee
        '
        Me.plageBloquee.AutoSize = True
        Me.plageBloquee.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.plageBloquee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageBloquee.Cursor = System.Windows.Forms.Cursors.Default
        Me.plageBloquee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plageBloquee.ForeColor = System.Drawing.SystemColors.ControlText
        Me.plageBloquee.Location = New System.Drawing.Point(350, 18)
        Me.plageBloquee.Name = "plageBloquee"
        Me.plageBloquee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.plageBloquee.Size = New System.Drawing.Size(48, 16)
        Me.plageBloquee.TabIndex = 65
        Me.plageBloquee.Text = "Bloquée"
        Me.plageBloquee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plageReservee
        '
        Me.plageReservee.AutoSize = True
        Me.plageReservee.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.plageReservee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageReservee.Cursor = System.Windows.Forms.Cursors.Default
        Me.plageReservee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plageReservee.ForeColor = System.Drawing.SystemColors.ControlText
        Me.plageReservee.Location = New System.Drawing.Point(288, 18)
        Me.plageReservee.Name = "plageReservee"
        Me.plageReservee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.plageReservee.Size = New System.Drawing.Size(56, 16)
        Me.plageReservee.TabIndex = 65
        Me.plageReservee.Text = "Réservée"
        Me.plageReservee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plageClinique
        '
        Me.plageClinique.AutoSize = True
        Me.plageClinique.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageClinique.Cursor = System.Windows.Forms.Cursors.Default
        Me.plageClinique.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plageClinique.ForeColor = System.Drawing.SystemColors.ControlText
        Me.plageClinique.Location = New System.Drawing.Point(8, 18)
        Me.plageClinique.Name = "plageClinique"
        Me.plageClinique.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.plageClinique.Size = New System.Drawing.Size(46, 16)
        Me.plageClinique.TabIndex = 64
        Me.plageClinique.Text = "Clinique"
        Me.plageClinique.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plageLibre
        '
        Me.plageLibre.AutoSize = True
        Me.plageLibre.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plageLibre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageLibre.Cursor = System.Windows.Forms.Cursors.Default
        Me.plageLibre.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plageLibre.ForeColor = System.Drawing.SystemColors.ControlText
        Me.plageLibre.Location = New System.Drawing.Point(60, 18)
        Me.plageLibre.Name = "plageLibre"
        Me.plageLibre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.plageLibre.Size = New System.Drawing.Size(33, 16)
        Me.plageLibre.TabIndex = 63
        Me.plageLibre.Text = "Libre"
        Me.plageLibre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plageRV
        '
        Me.plageRV.AutoSize = True
        Me.plageRV.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.plageRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plageRV.Cursor = System.Windows.Forms.Cursors.Default
        Me.plageRV.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plageRV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.plageRV.Location = New System.Drawing.Point(99, 18)
        Me.plageRV.Name = "plageRV"
        Me.plageRV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.plageRV.Size = New System.Drawing.Size(28, 16)
        Me.plageRV.TabIndex = 62
        Me.plageRV.Text = "R-V"
        Me.plageRV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plagePresence
        '
        Me.plagePresence.AutoSize = True
        Me.plagePresence.BackColor = System.Drawing.Color.White
        Me.plagePresence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plagePresence.Cursor = System.Windows.Forms.Cursors.Default
        Me.plagePresence.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plagePresence.ForeColor = System.Drawing.SystemColors.ControlText
        Me.plagePresence.Location = New System.Drawing.Point(133, 18)
        Me.plagePresence.Name = "plagePresence"
        Me.plagePresence.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.plagePresence.Size = New System.Drawing.Size(55, 16)
        Me.plagePresence.TabIndex = 61
        Me.plagePresence.Text = "Présence"
        Me.plagePresence.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'line4
        '
        Me.line4.BackColor = System.Drawing.SystemColors.WindowText
        Me.line4.Location = New System.Drawing.Point(3789, 120)
        Me.line4.Name = "line4"
        Me.line4.Size = New System.Drawing.Size(1, 360)
        Me.line4.TabIndex = 66
        '
        'line3
        '
        Me.line3.BackColor = System.Drawing.SystemColors.WindowText
        Me.line3.Location = New System.Drawing.Point(2655, 120)
        Me.line3.Name = "line3"
        Me.line3.Size = New System.Drawing.Size(1, 360)
        Me.line3.TabIndex = 67
        '
        'line2
        '
        Me.line2.BackColor = System.Drawing.SystemColors.WindowText
        Me.line2.Location = New System.Drawing.Point(1890, 120)
        Me.line2.Name = "line2"
        Me.line2.Size = New System.Drawing.Size(1, 360)
        Me.line2.TabIndex = 68
        '
        'line1
        '
        Me.line1.BackColor = System.Drawing.SystemColors.WindowText
        Me.line1.Location = New System.Drawing.Point(1065, 120)
        Me.line1.Name = "line1"
        Me.line1.Size = New System.Drawing.Size(1, 360)
        Me.line1.TabIndex = 69
        '
        '_MaxMin_41
        '
        Me._MaxMin_41.BackColor = System.Drawing.SystemColors.Control
        Me._MaxMin_41.Cursor = System.Windows.Forms.Cursors.Default
        Me._MaxMin_41.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me._MaxMin_41.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._MaxMin_41.ForeColor = System.Drawing.SystemColors.ControlText
        Me._MaxMin_41.Location = New System.Drawing.Point(616, 152)
        Me._MaxMin_41.Name = "_MaxMin_41"
        Me._MaxMin_41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._MaxMin_41.Size = New System.Drawing.Size(16, 16)
        Me._MaxMin_41.TabIndex = 54
        Me._MaxMin_41.TabStop = False
        Me._MaxMin_41.Tag = "0"
        Me._MaxMin_41.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._MaxMin_41, "Maximiser la journée")
        Me._MaxMin_41.UseVisualStyleBackColor = False
        Me._MaxMin_41.Visible = False
        '
        '_Labels_5
        '
        Me._Labels_5.AutoSize = True
        Me._Labels_5.BackColor = System.Drawing.SystemColors.Control
        Me._Labels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Labels_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Labels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Labels_5.Location = New System.Drawing.Point(414, 15)
        Me._Labels_5.Name = "_Labels_5"
        Me._Labels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Labels_5.Size = New System.Drawing.Size(37, 14)
        Me._Labels_5.TabIndex = 107
        Me._Labels_5.Tag = "NOHIDE"
        Me._Labels_5.Text = "Date :"
        '
        '_LabelsNbDays_21
        '
        Me._LabelsNbDays_21.AutoSize = True
        Me._LabelsNbDays_21.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_21.Location = New System.Drawing.Point(39, 347)
        Me._LabelsNbDays_21.Name = "_LabelsNbDays_21"
        Me._LabelsNbDays_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_21.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_21.TabIndex = 21
        Me._LabelsNbDays_21.Text = "2"
        Me._LabelsNbDays_21.Visible = False
        '
        '_LabelsNbDays_41
        '
        Me._LabelsNbDays_41.AutoSize = True
        Me._LabelsNbDays_41.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_41.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_41.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_41.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_41.Location = New System.Drawing.Point(10, 355)
        Me._LabelsNbDays_41.Name = "_LabelsNbDays_41"
        Me._LabelsNbDays_41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_41.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_41.TabIndex = 41
        Me._LabelsNbDays_41.Text = "2"
        Me._LabelsNbDays_41.Visible = False
        '
        '_LabelsNbDays_40
        '
        Me._LabelsNbDays_40.AutoSize = True
        Me._LabelsNbDays_40.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_40.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_40.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_40.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_40.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_40.Name = "_LabelsNbDays_40"
        Me._LabelsNbDays_40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_40.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_40.TabIndex = 40
        Me._LabelsNbDays_40.Text = "2"
        Me._LabelsNbDays_40.Visible = False
        '
        '_LabelsNbDays_39
        '
        Me._LabelsNbDays_39.AutoSize = True
        Me._LabelsNbDays_39.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_39.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_39.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_39.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_39.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_39.Name = "_LabelsNbDays_39"
        Me._LabelsNbDays_39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_39.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_39.TabIndex = 39
        Me._LabelsNbDays_39.Text = "2"
        Me._LabelsNbDays_39.Visible = False
        '
        '_LabelsNbDays_38
        '
        Me._LabelsNbDays_38.AutoSize = True
        Me._LabelsNbDays_38.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_38.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_38.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_38.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_38.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_38.Name = "_LabelsNbDays_38"
        Me._LabelsNbDays_38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_38.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_38.TabIndex = 38
        Me._LabelsNbDays_38.Text = "2"
        Me._LabelsNbDays_38.Visible = False
        '
        '_LabelsNbDays_37
        '
        Me._LabelsNbDays_37.AutoSize = True
        Me._LabelsNbDays_37.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_37.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_37.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_37.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_37.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_37.Name = "_LabelsNbDays_37"
        Me._LabelsNbDays_37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_37.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_37.TabIndex = 37
        Me._LabelsNbDays_37.Text = "2"
        Me._LabelsNbDays_37.Visible = False
        '
        '_LabelsNbDays_36
        '
        Me._LabelsNbDays_36.AutoSize = True
        Me._LabelsNbDays_36.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_36.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_36.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_36.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_36.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_36.Name = "_LabelsNbDays_36"
        Me._LabelsNbDays_36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_36.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_36.TabIndex = 36
        Me._LabelsNbDays_36.Text = "2"
        Me._LabelsNbDays_36.Visible = False
        '
        '_LabelsNbDays_35
        '
        Me._LabelsNbDays_35.AutoSize = True
        Me._LabelsNbDays_35.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_35.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_35.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_35.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_35.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_35.Name = "_LabelsNbDays_35"
        Me._LabelsNbDays_35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_35.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_35.TabIndex = 35
        Me._LabelsNbDays_35.Text = "2"
        Me._LabelsNbDays_35.Visible = False
        '
        '_LabelsNbDays_34
        '
        Me._LabelsNbDays_34.AutoSize = True
        Me._LabelsNbDays_34.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_34.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_34.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_34.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_34.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_34.Name = "_LabelsNbDays_34"
        Me._LabelsNbDays_34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_34.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_34.TabIndex = 34
        Me._LabelsNbDays_34.Text = "2"
        Me._LabelsNbDays_34.Visible = False
        '
        '_LabelsNbDays_33
        '
        Me._LabelsNbDays_33.AutoSize = True
        Me._LabelsNbDays_33.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_33.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_33.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_33.Name = "_LabelsNbDays_33"
        Me._LabelsNbDays_33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_33.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_33.TabIndex = 33
        Me._LabelsNbDays_33.Text = "2"
        Me._LabelsNbDays_33.Visible = False
        '
        '_LabelsNbDays_32
        '
        Me._LabelsNbDays_32.AutoSize = True
        Me._LabelsNbDays_32.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_32.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_32.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_32.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_32.Name = "_LabelsNbDays_32"
        Me._LabelsNbDays_32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_32.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_32.TabIndex = 32
        Me._LabelsNbDays_32.Text = "2"
        Me._LabelsNbDays_32.Visible = False
        '
        '_LabelsNbDays_31
        '
        Me._LabelsNbDays_31.AutoSize = True
        Me._LabelsNbDays_31.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_31.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_31.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_31.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_31.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_31.Name = "_LabelsNbDays_31"
        Me._LabelsNbDays_31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_31.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_31.TabIndex = 31
        Me._LabelsNbDays_31.Text = "2"
        Me._LabelsNbDays_31.Visible = False
        '
        '_LabelsNbDays_30
        '
        Me._LabelsNbDays_30.AutoSize = True
        Me._LabelsNbDays_30.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_30.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_30.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_30.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_30.Name = "_LabelsNbDays_30"
        Me._LabelsNbDays_30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_30.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_30.TabIndex = 30
        Me._LabelsNbDays_30.Text = "2"
        Me._LabelsNbDays_30.Visible = False
        '
        '_LabelsNbDays_29
        '
        Me._LabelsNbDays_29.AutoSize = True
        Me._LabelsNbDays_29.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_29.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_29.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_29.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_29.Name = "_LabelsNbDays_29"
        Me._LabelsNbDays_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_29.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_29.TabIndex = 29
        Me._LabelsNbDays_29.Text = "2"
        Me._LabelsNbDays_29.Visible = False
        '
        '_LabelsNbDays_28
        '
        Me._LabelsNbDays_28.AutoSize = True
        Me._LabelsNbDays_28.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_28.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_28.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_28.Name = "_LabelsNbDays_28"
        Me._LabelsNbDays_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_28.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_28.TabIndex = 28
        Me._LabelsNbDays_28.Text = "2"
        Me._LabelsNbDays_28.Visible = False
        '
        '_LabelsNbDays_27
        '
        Me._LabelsNbDays_27.AutoSize = True
        Me._LabelsNbDays_27.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_27.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_27.Name = "_LabelsNbDays_27"
        Me._LabelsNbDays_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_27.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_27.TabIndex = 27
        Me._LabelsNbDays_27.Text = "2"
        Me._LabelsNbDays_27.Visible = False
        '
        '_LabelsNbDays_26
        '
        Me._LabelsNbDays_26.AutoSize = True
        Me._LabelsNbDays_26.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_26.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_26.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_26.Name = "_LabelsNbDays_26"
        Me._LabelsNbDays_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_26.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_26.TabIndex = 26
        Me._LabelsNbDays_26.Text = "2"
        Me._LabelsNbDays_26.Visible = False
        '
        '_LabelsNbDays_25
        '
        Me._LabelsNbDays_25.AutoSize = True
        Me._LabelsNbDays_25.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_25.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_25.Name = "_LabelsNbDays_25"
        Me._LabelsNbDays_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_25.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_25.TabIndex = 25
        Me._LabelsNbDays_25.Text = "2"
        Me._LabelsNbDays_25.Visible = False
        '
        '_LabelsNbDays_24
        '
        Me._LabelsNbDays_24.AutoSize = True
        Me._LabelsNbDays_24.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_24.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_24.Name = "_LabelsNbDays_24"
        Me._LabelsNbDays_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_24.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_24.TabIndex = 24
        Me._LabelsNbDays_24.Text = "2"
        Me._LabelsNbDays_24.Visible = False
        '
        '_LabelsNbDays_23
        '
        Me._LabelsNbDays_23.AutoSize = True
        Me._LabelsNbDays_23.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_23.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_23.Name = "_LabelsNbDays_23"
        Me._LabelsNbDays_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_23.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_23.TabIndex = 23
        Me._LabelsNbDays_23.Text = "2"
        Me._LabelsNbDays_23.Visible = False
        '
        '_LabelsNbDays_22
        '
        Me._LabelsNbDays_22.AutoSize = True
        Me._LabelsNbDays_22.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_22.Location = New System.Drawing.Point(34, 355)
        Me._LabelsNbDays_22.Name = "_LabelsNbDays_22"
        Me._LabelsNbDays_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_22.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_22.TabIndex = 22
        Me._LabelsNbDays_22.Text = "2"
        Me._LabelsNbDays_22.Visible = False
        '
        '_LabelsNbDays_20
        '
        Me._LabelsNbDays_20.AutoSize = True
        Me._LabelsNbDays_20.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_20.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_20.Name = "_LabelsNbDays_20"
        Me._LabelsNbDays_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_20.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_20.TabIndex = 20
        Me._LabelsNbDays_20.Text = "2"
        Me._LabelsNbDays_20.Visible = False
        '
        '_LabelsNbDays_19
        '
        Me._LabelsNbDays_19.AutoSize = True
        Me._LabelsNbDays_19.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_19.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_19.Name = "_LabelsNbDays_19"
        Me._LabelsNbDays_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_19.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_19.TabIndex = 19
        Me._LabelsNbDays_19.Text = "2"
        Me._LabelsNbDays_19.Visible = False
        '
        '_LabelsNbDays_18
        '
        Me._LabelsNbDays_18.AutoSize = True
        Me._LabelsNbDays_18.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_18.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_18.Name = "_LabelsNbDays_18"
        Me._LabelsNbDays_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_18.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_18.TabIndex = 18
        Me._LabelsNbDays_18.Text = "2"
        Me._LabelsNbDays_18.Visible = False
        '
        '_LabelsNbDays_17
        '
        Me._LabelsNbDays_17.AutoSize = True
        Me._LabelsNbDays_17.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_17.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_17.Name = "_LabelsNbDays_17"
        Me._LabelsNbDays_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_17.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_17.TabIndex = 17
        Me._LabelsNbDays_17.Text = "2"
        Me._LabelsNbDays_17.Visible = False
        '
        '_LabelsNbDays_16
        '
        Me._LabelsNbDays_16.AutoSize = True
        Me._LabelsNbDays_16.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_16.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_16.Name = "_LabelsNbDays_16"
        Me._LabelsNbDays_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_16.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_16.TabIndex = 16
        Me._LabelsNbDays_16.Text = "2"
        Me._LabelsNbDays_16.Visible = False
        '
        '_LabelsNbDays_15
        '
        Me._LabelsNbDays_15.AutoSize = True
        Me._LabelsNbDays_15.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_15.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_15.Name = "_LabelsNbDays_15"
        Me._LabelsNbDays_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_15.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_15.TabIndex = 15
        Me._LabelsNbDays_15.Text = "2"
        Me._LabelsNbDays_15.Visible = False
        '
        '_LabelsNbDays_14
        '
        Me._LabelsNbDays_14.AutoSize = True
        Me._LabelsNbDays_14.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_14.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_14.Name = "_LabelsNbDays_14"
        Me._LabelsNbDays_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_14.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_14.TabIndex = 14
        Me._LabelsNbDays_14.Text = "2"
        Me._LabelsNbDays_14.Visible = False
        '
        '_LabelsNbDays_13
        '
        Me._LabelsNbDays_13.AutoSize = True
        Me._LabelsNbDays_13.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_13.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_13.Name = "_LabelsNbDays_13"
        Me._LabelsNbDays_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_13.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_13.TabIndex = 13
        Me._LabelsNbDays_13.Text = "2"
        Me._LabelsNbDays_13.Visible = False
        '
        '_LabelsNbDays_12
        '
        Me._LabelsNbDays_12.AutoSize = True
        Me._LabelsNbDays_12.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_12.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_12.Name = "_LabelsNbDays_12"
        Me._LabelsNbDays_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_12.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_12.TabIndex = 12
        Me._LabelsNbDays_12.Text = "2"
        Me._LabelsNbDays_12.Visible = False
        '
        '_LabelsNbDays_11
        '
        Me._LabelsNbDays_11.AutoSize = True
        Me._LabelsNbDays_11.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_11.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_11.Name = "_LabelsNbDays_11"
        Me._LabelsNbDays_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_11.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_11.TabIndex = 11
        Me._LabelsNbDays_11.Text = "2"
        Me._LabelsNbDays_11.Visible = False
        '
        '_LabelsNbDays_10
        '
        Me._LabelsNbDays_10.AutoSize = True
        Me._LabelsNbDays_10.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_10.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_10.Name = "_LabelsNbDays_10"
        Me._LabelsNbDays_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_10.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_10.TabIndex = 10
        Me._LabelsNbDays_10.Text = "2"
        Me._LabelsNbDays_10.Visible = False
        '
        '_LabelsNbDays_9
        '
        Me._LabelsNbDays_9.AutoSize = True
        Me._LabelsNbDays_9.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_9.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_9.Name = "_LabelsNbDays_9"
        Me._LabelsNbDays_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_9.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_9.TabIndex = 9
        Me._LabelsNbDays_9.Text = "2"
        Me._LabelsNbDays_9.Visible = False
        '
        '_LabelsNbDays_8
        '
        Me._LabelsNbDays_8.AutoSize = True
        Me._LabelsNbDays_8.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_8.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_8.Name = "_LabelsNbDays_8"
        Me._LabelsNbDays_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_8.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_8.TabIndex = 8
        Me._LabelsNbDays_8.Text = "2"
        Me._LabelsNbDays_8.Visible = False
        '
        '_LabelsNbDays_7
        '
        Me._LabelsNbDays_7.AutoSize = True
        Me._LabelsNbDays_7.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_7.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_7.Name = "_LabelsNbDays_7"
        Me._LabelsNbDays_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_7.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_7.TabIndex = 7
        Me._LabelsNbDays_7.Text = "2"
        Me._LabelsNbDays_7.Visible = False
        '
        '_LabelsNbDays_6
        '
        Me._LabelsNbDays_6.AutoSize = True
        Me._LabelsNbDays_6.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_6.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_6.Name = "_LabelsNbDays_6"
        Me._LabelsNbDays_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_6.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_6.TabIndex = 6
        Me._LabelsNbDays_6.Text = "2"
        Me._LabelsNbDays_6.Visible = False
        '
        '_LabelsNbDays_5
        '
        Me._LabelsNbDays_5.AutoSize = True
        Me._LabelsNbDays_5.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_5.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_5.Name = "_LabelsNbDays_5"
        Me._LabelsNbDays_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_5.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_5.TabIndex = 5
        Me._LabelsNbDays_5.Text = "2"
        Me._LabelsNbDays_5.Visible = False
        '
        '_LabelsNbDays_4
        '
        Me._LabelsNbDays_4.AutoSize = True
        Me._LabelsNbDays_4.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_4.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_4.Name = "_LabelsNbDays_4"
        Me._LabelsNbDays_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_4.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_4.TabIndex = 4
        Me._LabelsNbDays_4.Text = "2"
        Me._LabelsNbDays_4.Visible = False
        '
        '_LabelsNbDays_3
        '
        Me._LabelsNbDays_3.AutoSize = True
        Me._LabelsNbDays_3.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_3.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_3.Name = "_LabelsNbDays_3"
        Me._LabelsNbDays_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_3.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_3.TabIndex = 3
        Me._LabelsNbDays_3.Text = "2"
        Me._LabelsNbDays_3.Visible = False
        '
        '_LabelsNbDays_2
        '
        Me._LabelsNbDays_2.AutoSize = True
        Me._LabelsNbDays_2.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_2.Location = New System.Drawing.Point(10, 371)
        Me._LabelsNbDays_2.Name = "_LabelsNbDays_2"
        Me._LabelsNbDays_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_2.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_2.TabIndex = 2
        Me._LabelsNbDays_2.Text = "2"
        Me._LabelsNbDays_2.Visible = False
        '
        '_LabelsNbDays_1
        '
        Me._LabelsNbDays_1.AutoSize = True
        Me._LabelsNbDays_1.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_1.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_1.Name = "_LabelsNbDays_1"
        Me._LabelsNbDays_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_1.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_1.TabIndex = 1
        Me._LabelsNbDays_1.Text = "2"
        Me._LabelsNbDays_1.Visible = False
        '
        '_LabelsNbDays_0
        '
        Me._LabelsNbDays_0.AutoSize = True
        Me._LabelsNbDays_0.BackColor = System.Drawing.Color.Transparent
        Me._LabelsNbDays_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabelsNbDays_0.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabelsNbDays_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LabelsNbDays_0.Location = New System.Drawing.Point(10, 363)
        Me._LabelsNbDays_0.Name = "_LabelsNbDays_0"
        Me._LabelsNbDays_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabelsNbDays_0.Size = New System.Drawing.Size(15, 16)
        Me._LabelsNbDays_0.TabIndex = 0
        Me._LabelsNbDays_0.Text = "2"
        Me._LabelsNbDays_0.Visible = False
        '
        '_Lignes_14
        '
        Me._Lignes_14.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_14.Location = New System.Drawing.Point(138, 291)
        Me._Lignes_14.Name = "_Lignes_14"
        Me._Lignes_14.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_14.TabIndex = 117
        '
        '_Lignes_13
        '
        Me._Lignes_13.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_13.Location = New System.Drawing.Point(154, 235)
        Me._Lignes_13.Name = "_Lignes_13"
        Me._Lignes_13.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_13.TabIndex = 118
        '
        '_Lignes_12
        '
        Me._Lignes_12.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_12.Location = New System.Drawing.Point(114, 283)
        Me._Lignes_12.Name = "_Lignes_12"
        Me._Lignes_12.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_12.TabIndex = 119
        '
        '_Lignes_11
        '
        Me._Lignes_11.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_11.Location = New System.Drawing.Point(90, 275)
        Me._Lignes_11.Name = "_Lignes_11"
        Me._Lignes_11.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_11.TabIndex = 120
        '
        '_Lignes_10
        '
        Me._Lignes_10.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_10.Location = New System.Drawing.Point(74, 267)
        Me._Lignes_10.Name = "_Lignes_10"
        Me._Lignes_10.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_10.TabIndex = 121
        '
        '_Lignes_9
        '
        Me._Lignes_9.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_9.Location = New System.Drawing.Point(162, 219)
        Me._Lignes_9.Name = "_Lignes_9"
        Me._Lignes_9.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_9.TabIndex = 122
        '
        '_Lignes_8
        '
        Me._Lignes_8.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_8.Location = New System.Drawing.Point(138, 203)
        Me._Lignes_8.Name = "_Lignes_8"
        Me._Lignes_8.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_8.TabIndex = 123
        '
        '_Lignes_7
        '
        Me._Lignes_7.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_7.Location = New System.Drawing.Point(114, 195)
        Me._Lignes_7.Name = "_Lignes_7"
        Me._Lignes_7.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_7.TabIndex = 124
        '
        '_Lignes_6
        '
        Me._Lignes_6.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_6.Location = New System.Drawing.Point(98, 195)
        Me._Lignes_6.Name = "_Lignes_6"
        Me._Lignes_6.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_6.TabIndex = 125
        '
        '_Lignes_5
        '
        Me._Lignes_5.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_5.Location = New System.Drawing.Point(82, 187)
        Me._Lignes_5.Name = "_Lignes_5"
        Me._Lignes_5.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_5.TabIndex = 126
        '
        '_Lignes_4
        '
        Me._Lignes_4.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_4.Location = New System.Drawing.Point(175, 147)
        Me._Lignes_4.Name = "_Lignes_4"
        Me._Lignes_4.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_4.TabIndex = 127
        '
        '_Lignes_3
        '
        Me._Lignes_3.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_3.Location = New System.Drawing.Point(146, 147)
        Me._Lignes_3.Name = "_Lignes_3"
        Me._Lignes_3.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_3.TabIndex = 128
        '
        '_Lignes_2
        '
        Me._Lignes_2.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_2.Location = New System.Drawing.Point(66, 347)
        Me._Lignes_2.Name = "_Lignes_2"
        Me._Lignes_2.Size = New System.Drawing.Size(1, 48)
        Me._Lignes_2.TabIndex = 129
        '
        '_Lignes_1
        '
        Me._Lignes_1.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_1.Location = New System.Drawing.Point(82, 331)
        Me._Lignes_1.Name = "_Lignes_1"
        Me._Lignes_1.Size = New System.Drawing.Size(1, 64)
        Me._Lignes_1.TabIndex = 130
        '
        '_Lignes_0
        '
        Me._Lignes_0.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lignes_0.Location = New System.Drawing.Point(42, 363)
        Me._Lignes_0.Name = "_Lignes_0"
        Me._Lignes_0.Size = New System.Drawing.Size(80, 1)
        Me._Lignes_0.TabIndex = 131
        '
        'DayList0
        '
        Me.DayList0.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList0.autoAdjust = True
        Me.DayList0.autoKeyDownSelection = True
        Me.DayList0.autoSizeHorizontally = False
        Me.DayList0.autoSizeVertically = False
        Me.DayList0.BackColor = System.Drawing.Color.White
        Me.DayList0.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList0.baseBackColor = System.Drawing.Color.White
        Me.DayList0.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList0.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList0.bgColor = System.Drawing.Color.White
        Me.DayList0.borderColor = System.Drawing.Color.Empty
        Me.DayList0.borderSelColor = System.Drawing.Color.Empty
        Me.DayList0.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList0.CausesValidation = False
        Me.DayList0.clickEnabled = True
        Me.DayList0.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList0.do3D = False
        Me.DayList0.draw = False
        Me.DayList0.extraWidth = 0
        Me.DayList0.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList0.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList0.hScrolling = True
        Me.DayList0.hsValue = 0
        Me.DayList0.icons = CType(resources.GetObject("DayList0.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList0.itemBorder = 0
        Me.DayList0.itemMargin = 0
        Me.DayList0.items = CType(resources.GetObject("DayList0.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList0.Location = New System.Drawing.Point(202, 299)
        Me.DayList0.mouseMove3D = False
        Me.DayList0.mouseSpeed = 0
        Me.DayList0.Name = "DayList0"
        Me.DayList0.objMaxHeight = 0.0!
        Me.DayList0.objMaxWidth = 0.0!
        Me.DayList0.objMinHeight = 0.0!
        Me.DayList0.objMinWidth = 0.0!
        Me.DayList0.reverseSorting = False
        Me.DayList0.selected = -1
        Me.DayList0.selectedClickAllowed = True
        Me.DayList0.selectMultiple = False
        Me.DayList0.Size = New System.Drawing.Size(80, 72)
        Me.DayList0.sorted = False
        Me.DayList0.TabIndex = 132
        Me.DayList0.Tag = "NOHIDE"
        Me.DayList0.toolTipText = Nothing
        Me.DayList0.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList0.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList0.vScrolling = True
        Me.DayList0.vsValue = 0
        '
        'DayList2
        '
        Me.DayList2.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList2.autoAdjust = True
        Me.DayList2.autoKeyDownSelection = True
        Me.DayList2.autoSizeHorizontally = False
        Me.DayList2.autoSizeVertically = False
        Me.DayList2.BackColor = System.Drawing.Color.White
        Me.DayList2.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList2.baseBackColor = System.Drawing.Color.White
        Me.DayList2.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList2.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList2.bgColor = System.Drawing.Color.White
        Me.DayList2.borderColor = System.Drawing.Color.Empty
        Me.DayList2.borderSelColor = System.Drawing.Color.Empty
        Me.DayList2.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList2.CausesValidation = False
        Me.DayList2.clickEnabled = True
        Me.DayList2.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList2.do3D = False
        Me.DayList2.draw = False
        Me.DayList2.extraWidth = 0
        Me.DayList2.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList2.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList2.hScrolling = True
        Me.DayList2.hsValue = 0
        Me.DayList2.icons = CType(resources.GetObject("DayList2.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList2.itemBorder = 0
        Me.DayList2.itemMargin = 0
        Me.DayList2.items = CType(resources.GetObject("DayList2.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList2.Location = New System.Drawing.Point(210, 299)
        Me.DayList2.mouseMove3D = False
        Me.DayList2.mouseSpeed = 0
        Me.DayList2.Name = "DayList2"
        Me.DayList2.objMaxHeight = 0.0!
        Me.DayList2.objMaxWidth = 0.0!
        Me.DayList2.objMinHeight = 0.0!
        Me.DayList2.objMinWidth = 0.0!
        Me.DayList2.reverseSorting = False
        Me.DayList2.selected = -1
        Me.DayList2.selectedClickAllowed = True
        Me.DayList2.selectMultiple = False
        Me.DayList2.Size = New System.Drawing.Size(80, 72)
        Me.DayList2.sorted = False
        Me.DayList2.TabIndex = 133
        Me.DayList2.Tag = "NOHIDE"
        Me.DayList2.toolTipText = Nothing
        Me.DayList2.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList2.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList2.vScrolling = True
        Me.DayList2.vsValue = 0
        '
        'DayList3
        '
        Me.DayList3.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList3.autoAdjust = True
        Me.DayList3.autoKeyDownSelection = True
        Me.DayList3.autoSizeHorizontally = False
        Me.DayList3.autoSizeVertically = False
        Me.DayList3.BackColor = System.Drawing.Color.White
        Me.DayList3.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList3.baseBackColor = System.Drawing.Color.White
        Me.DayList3.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList3.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList3.bgColor = System.Drawing.Color.White
        Me.DayList3.borderColor = System.Drawing.Color.Empty
        Me.DayList3.borderSelColor = System.Drawing.Color.Empty
        Me.DayList3.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList3.CausesValidation = False
        Me.DayList3.clickEnabled = True
        Me.DayList3.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList3.do3D = False
        Me.DayList3.draw = False
        Me.DayList3.extraWidth = 0
        Me.DayList3.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList3.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList3.hScrolling = True
        Me.DayList3.hsValue = 0
        Me.DayList3.icons = CType(resources.GetObject("DayList3.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList3.itemBorder = 0
        Me.DayList3.itemMargin = 0
        Me.DayList3.items = CType(resources.GetObject("DayList3.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList3.Location = New System.Drawing.Point(178, 283)
        Me.DayList3.mouseMove3D = False
        Me.DayList3.mouseSpeed = 0
        Me.DayList3.Name = "DayList3"
        Me.DayList3.objMaxHeight = 0.0!
        Me.DayList3.objMaxWidth = 0.0!
        Me.DayList3.objMinHeight = 0.0!
        Me.DayList3.objMinWidth = 0.0!
        Me.DayList3.reverseSorting = False
        Me.DayList3.selected = -1
        Me.DayList3.selectedClickAllowed = True
        Me.DayList3.selectMultiple = False
        Me.DayList3.Size = New System.Drawing.Size(80, 72)
        Me.DayList3.sorted = False
        Me.DayList3.TabIndex = 134
        Me.DayList3.Tag = "NOHIDE"
        Me.DayList3.toolTipText = Nothing
        Me.DayList3.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList3.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList3.vScrolling = True
        Me.DayList3.vsValue = 0
        '
        'DayList4
        '
        Me.DayList4.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList4.autoAdjust = True
        Me.DayList4.autoKeyDownSelection = True
        Me.DayList4.autoSizeHorizontally = False
        Me.DayList4.autoSizeVertically = False
        Me.DayList4.BackColor = System.Drawing.Color.White
        Me.DayList4.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList4.baseBackColor = System.Drawing.Color.White
        Me.DayList4.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList4.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList4.bgColor = System.Drawing.Color.White
        Me.DayList4.borderColor = System.Drawing.Color.Empty
        Me.DayList4.borderSelColor = System.Drawing.Color.Empty
        Me.DayList4.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList4.CausesValidation = False
        Me.DayList4.clickEnabled = True
        Me.DayList4.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList4.do3D = False
        Me.DayList4.draw = False
        Me.DayList4.extraWidth = 0
        Me.DayList4.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList4.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList4.hScrolling = True
        Me.DayList4.hsValue = 0
        Me.DayList4.icons = CType(resources.GetObject("DayList4.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList4.itemBorder = 0
        Me.DayList4.itemMargin = 0
        Me.DayList4.items = CType(resources.GetObject("DayList4.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList4.Location = New System.Drawing.Point(186, 291)
        Me.DayList4.mouseMove3D = False
        Me.DayList4.mouseSpeed = 0
        Me.DayList4.Name = "DayList4"
        Me.DayList4.objMaxHeight = 0.0!
        Me.DayList4.objMaxWidth = 0.0!
        Me.DayList4.objMinHeight = 0.0!
        Me.DayList4.objMinWidth = 0.0!
        Me.DayList4.reverseSorting = False
        Me.DayList4.selected = -1
        Me.DayList4.selectedClickAllowed = True
        Me.DayList4.selectMultiple = False
        Me.DayList4.Size = New System.Drawing.Size(80, 72)
        Me.DayList4.sorted = False
        Me.DayList4.TabIndex = 135
        Me.DayList4.Tag = "NOHIDE"
        Me.DayList4.toolTipText = Nothing
        Me.DayList4.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList4.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList4.vScrolling = True
        Me.DayList4.vsValue = 0
        '
        'DayList5
        '
        Me.DayList5.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList5.autoAdjust = True
        Me.DayList5.autoKeyDownSelection = True
        Me.DayList5.autoSizeHorizontally = False
        Me.DayList5.autoSizeVertically = False
        Me.DayList5.BackColor = System.Drawing.Color.White
        Me.DayList5.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList5.baseBackColor = System.Drawing.Color.White
        Me.DayList5.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList5.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList5.bgColor = System.Drawing.Color.White
        Me.DayList5.borderColor = System.Drawing.Color.Empty
        Me.DayList5.borderSelColor = System.Drawing.Color.Empty
        Me.DayList5.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList5.CausesValidation = False
        Me.DayList5.clickEnabled = True
        Me.DayList5.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList5.do3D = False
        Me.DayList5.draw = False
        Me.DayList5.extraWidth = 0
        Me.DayList5.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList5.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList5.hScrolling = True
        Me.DayList5.hsValue = 0
        Me.DayList5.icons = CType(resources.GetObject("DayList5.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList5.itemBorder = 0
        Me.DayList5.itemMargin = 0
        Me.DayList5.items = CType(resources.GetObject("DayList5.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList5.Location = New System.Drawing.Point(194, 299)
        Me.DayList5.mouseMove3D = False
        Me.DayList5.mouseSpeed = 0
        Me.DayList5.Name = "DayList5"
        Me.DayList5.objMaxHeight = 0.0!
        Me.DayList5.objMaxWidth = 0.0!
        Me.DayList5.objMinHeight = 0.0!
        Me.DayList5.objMinWidth = 0.0!
        Me.DayList5.reverseSorting = False
        Me.DayList5.selected = -1
        Me.DayList5.selectedClickAllowed = True
        Me.DayList5.selectMultiple = False
        Me.DayList5.Size = New System.Drawing.Size(80, 72)
        Me.DayList5.sorted = False
        Me.DayList5.TabIndex = 136
        Me.DayList5.Tag = "NOHIDE"
        Me.DayList5.toolTipText = Nothing
        Me.DayList5.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList5.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList5.vScrolling = True
        Me.DayList5.vsValue = 0
        '
        'DayList6
        '
        Me.DayList6.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList6.autoAdjust = True
        Me.DayList6.autoKeyDownSelection = True
        Me.DayList6.autoSizeHorizontally = False
        Me.DayList6.autoSizeVertically = False
        Me.DayList6.BackColor = System.Drawing.Color.White
        Me.DayList6.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList6.baseBackColor = System.Drawing.Color.White
        Me.DayList6.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList6.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList6.bgColor = System.Drawing.Color.White
        Me.DayList6.borderColor = System.Drawing.Color.Empty
        Me.DayList6.borderSelColor = System.Drawing.Color.Empty
        Me.DayList6.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList6.CausesValidation = False
        Me.DayList6.clickEnabled = True
        Me.DayList6.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList6.do3D = False
        Me.DayList6.draw = False
        Me.DayList6.extraWidth = 0
        Me.DayList6.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList6.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList6.hScrolling = True
        Me.DayList6.hsValue = 0
        Me.DayList6.icons = CType(resources.GetObject("DayList6.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList6.itemBorder = 0
        Me.DayList6.itemMargin = 0
        Me.DayList6.items = CType(resources.GetObject("DayList6.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList6.Location = New System.Drawing.Point(202, 307)
        Me.DayList6.mouseMove3D = False
        Me.DayList6.mouseSpeed = 0
        Me.DayList6.Name = "DayList6"
        Me.DayList6.objMaxHeight = 0.0!
        Me.DayList6.objMaxWidth = 0.0!
        Me.DayList6.objMinHeight = 0.0!
        Me.DayList6.objMinWidth = 0.0!
        Me.DayList6.reverseSorting = False
        Me.DayList6.selected = -1
        Me.DayList6.selectedClickAllowed = True
        Me.DayList6.selectMultiple = False
        Me.DayList6.Size = New System.Drawing.Size(80, 72)
        Me.DayList6.sorted = False
        Me.DayList6.TabIndex = 137
        Me.DayList6.Tag = "NOHIDE"
        Me.DayList6.toolTipText = Nothing
        Me.DayList6.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList6.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList6.vScrolling = True
        Me.DayList6.vsValue = 0
        '
        'DayList7
        '
        Me.DayList7.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList7.autoAdjust = True
        Me.DayList7.autoKeyDownSelection = True
        Me.DayList7.autoSizeHorizontally = False
        Me.DayList7.autoSizeVertically = False
        Me.DayList7.BackColor = System.Drawing.Color.White
        Me.DayList7.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList7.baseBackColor = System.Drawing.Color.White
        Me.DayList7.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList7.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList7.bgColor = System.Drawing.Color.White
        Me.DayList7.borderColor = System.Drawing.Color.Empty
        Me.DayList7.borderSelColor = System.Drawing.Color.Empty
        Me.DayList7.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList7.CausesValidation = False
        Me.DayList7.clickEnabled = True
        Me.DayList7.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList7.do3D = False
        Me.DayList7.draw = False
        Me.DayList7.extraWidth = 0
        Me.DayList7.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList7.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList7.hScrolling = True
        Me.DayList7.hsValue = 0
        Me.DayList7.icons = CType(resources.GetObject("DayList7.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList7.itemBorder = 0
        Me.DayList7.itemMargin = 0
        Me.DayList7.items = CType(resources.GetObject("DayList7.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList7.Location = New System.Drawing.Point(210, 315)
        Me.DayList7.mouseMove3D = False
        Me.DayList7.mouseSpeed = 0
        Me.DayList7.Name = "DayList7"
        Me.DayList7.objMaxHeight = 0.0!
        Me.DayList7.objMaxWidth = 0.0!
        Me.DayList7.objMinHeight = 0.0!
        Me.DayList7.objMinWidth = 0.0!
        Me.DayList7.reverseSorting = False
        Me.DayList7.selected = -1
        Me.DayList7.selectedClickAllowed = True
        Me.DayList7.selectMultiple = False
        Me.DayList7.Size = New System.Drawing.Size(80, 72)
        Me.DayList7.sorted = False
        Me.DayList7.TabIndex = 138
        Me.DayList7.Tag = "NOHIDE"
        Me.DayList7.toolTipText = Nothing
        Me.DayList7.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList7.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList7.vScrolling = True
        Me.DayList7.vsValue = 0
        '
        'DayList8
        '
        Me.DayList8.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList8.autoAdjust = True
        Me.DayList8.autoKeyDownSelection = True
        Me.DayList8.autoSizeHorizontally = False
        Me.DayList8.autoSizeVertically = False
        Me.DayList8.BackColor = System.Drawing.Color.White
        Me.DayList8.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList8.baseBackColor = System.Drawing.Color.White
        Me.DayList8.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList8.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList8.bgColor = System.Drawing.Color.White
        Me.DayList8.borderColor = System.Drawing.Color.Empty
        Me.DayList8.borderSelColor = System.Drawing.Color.Empty
        Me.DayList8.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList8.CausesValidation = False
        Me.DayList8.clickEnabled = True
        Me.DayList8.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList8.do3D = False
        Me.DayList8.draw = False
        Me.DayList8.extraWidth = 0
        Me.DayList8.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList8.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList8.hScrolling = True
        Me.DayList8.hsValue = 0
        Me.DayList8.icons = CType(resources.GetObject("DayList8.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList8.itemBorder = 0
        Me.DayList8.itemMargin = 0
        Me.DayList8.items = CType(resources.GetObject("DayList8.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList8.Location = New System.Drawing.Point(178, 315)
        Me.DayList8.mouseMove3D = False
        Me.DayList8.mouseSpeed = 0
        Me.DayList8.Name = "DayList8"
        Me.DayList8.objMaxHeight = 0.0!
        Me.DayList8.objMaxWidth = 0.0!
        Me.DayList8.objMinHeight = 0.0!
        Me.DayList8.objMinWidth = 0.0!
        Me.DayList8.reverseSorting = False
        Me.DayList8.selected = -1
        Me.DayList8.selectedClickAllowed = True
        Me.DayList8.selectMultiple = False
        Me.DayList8.Size = New System.Drawing.Size(80, 72)
        Me.DayList8.sorted = False
        Me.DayList8.TabIndex = 139
        Me.DayList8.Tag = "NOHIDE"
        Me.DayList8.toolTipText = Nothing
        Me.DayList8.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList8.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList8.vScrolling = True
        Me.DayList8.vsValue = 0
        '
        'DayList9
        '
        Me.DayList9.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList9.autoAdjust = True
        Me.DayList9.autoKeyDownSelection = True
        Me.DayList9.autoSizeHorizontally = False
        Me.DayList9.autoSizeVertically = False
        Me.DayList9.BackColor = System.Drawing.Color.White
        Me.DayList9.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList9.baseBackColor = System.Drawing.Color.White
        Me.DayList9.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList9.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList9.bgColor = System.Drawing.Color.White
        Me.DayList9.borderColor = System.Drawing.Color.Empty
        Me.DayList9.borderSelColor = System.Drawing.Color.Empty
        Me.DayList9.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList9.CausesValidation = False
        Me.DayList9.clickEnabled = True
        Me.DayList9.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList9.do3D = False
        Me.DayList9.draw = False
        Me.DayList9.extraWidth = 0
        Me.DayList9.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList9.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList9.hScrolling = True
        Me.DayList9.hsValue = 0
        Me.DayList9.icons = CType(resources.GetObject("DayList9.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList9.itemBorder = 0
        Me.DayList9.itemMargin = 0
        Me.DayList9.items = CType(resources.GetObject("DayList9.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList9.Location = New System.Drawing.Point(186, 323)
        Me.DayList9.mouseMove3D = False
        Me.DayList9.mouseSpeed = 0
        Me.DayList9.Name = "DayList9"
        Me.DayList9.objMaxHeight = 0.0!
        Me.DayList9.objMaxWidth = 0.0!
        Me.DayList9.objMinHeight = 0.0!
        Me.DayList9.objMinWidth = 0.0!
        Me.DayList9.reverseSorting = False
        Me.DayList9.selected = -1
        Me.DayList9.selectedClickAllowed = True
        Me.DayList9.selectMultiple = False
        Me.DayList9.Size = New System.Drawing.Size(80, 64)
        Me.DayList9.sorted = False
        Me.DayList9.TabIndex = 140
        Me.DayList9.Tag = "NOHIDE"
        Me.DayList9.toolTipText = Nothing
        Me.DayList9.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList9.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList9.vScrolling = True
        Me.DayList9.vsValue = 0
        '
        'DayList10
        '
        Me.DayList10.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList10.autoAdjust = True
        Me.DayList10.autoKeyDownSelection = True
        Me.DayList10.autoSizeHorizontally = False
        Me.DayList10.autoSizeVertically = False
        Me.DayList10.BackColor = System.Drawing.Color.White
        Me.DayList10.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList10.baseBackColor = System.Drawing.Color.White
        Me.DayList10.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList10.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList10.bgColor = System.Drawing.Color.White
        Me.DayList10.borderColor = System.Drawing.Color.Empty
        Me.DayList10.borderSelColor = System.Drawing.Color.Empty
        Me.DayList10.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList10.CausesValidation = False
        Me.DayList10.clickEnabled = True
        Me.DayList10.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList10.do3D = False
        Me.DayList10.draw = False
        Me.DayList10.extraWidth = 0
        Me.DayList10.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList10.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList10.hScrolling = True
        Me.DayList10.hsValue = 0
        Me.DayList10.icons = CType(resources.GetObject("DayList10.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList10.itemBorder = 0
        Me.DayList10.itemMargin = 0
        Me.DayList10.items = CType(resources.GetObject("DayList10.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList10.Location = New System.Drawing.Point(186, 291)
        Me.DayList10.mouseMove3D = False
        Me.DayList10.mouseSpeed = 0
        Me.DayList10.Name = "DayList10"
        Me.DayList10.objMaxHeight = 0.0!
        Me.DayList10.objMaxWidth = 0.0!
        Me.DayList10.objMinHeight = 0.0!
        Me.DayList10.objMinWidth = 0.0!
        Me.DayList10.reverseSorting = False
        Me.DayList10.selected = -1
        Me.DayList10.selectedClickAllowed = True
        Me.DayList10.selectMultiple = False
        Me.DayList10.Size = New System.Drawing.Size(80, 72)
        Me.DayList10.sorted = False
        Me.DayList10.TabIndex = 141
        Me.DayList10.Tag = "NOHIDE"
        Me.DayList10.toolTipText = Nothing
        Me.DayList10.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList10.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList10.vScrolling = True
        Me.DayList10.vsValue = 0
        '
        'DayList11
        '
        Me.DayList11.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList11.autoAdjust = True
        Me.DayList11.autoKeyDownSelection = True
        Me.DayList11.autoSizeHorizontally = False
        Me.DayList11.autoSizeVertically = False
        Me.DayList11.BackColor = System.Drawing.Color.White
        Me.DayList11.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList11.baseBackColor = System.Drawing.Color.White
        Me.DayList11.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList11.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList11.bgColor = System.Drawing.Color.White
        Me.DayList11.borderColor = System.Drawing.Color.Empty
        Me.DayList11.borderSelColor = System.Drawing.Color.Empty
        Me.DayList11.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList11.CausesValidation = False
        Me.DayList11.clickEnabled = True
        Me.DayList11.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList11.do3D = False
        Me.DayList11.draw = False
        Me.DayList11.extraWidth = 0
        Me.DayList11.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList11.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList11.hScrolling = True
        Me.DayList11.hsValue = 0
        Me.DayList11.icons = CType(resources.GetObject("DayList11.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList11.itemBorder = 0
        Me.DayList11.itemMargin = 0
        Me.DayList11.items = CType(resources.GetObject("DayList11.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList11.Location = New System.Drawing.Point(194, 291)
        Me.DayList11.mouseMove3D = False
        Me.DayList11.mouseSpeed = 0
        Me.DayList11.Name = "DayList11"
        Me.DayList11.objMaxHeight = 0.0!
        Me.DayList11.objMaxWidth = 0.0!
        Me.DayList11.objMinHeight = 0.0!
        Me.DayList11.objMinWidth = 0.0!
        Me.DayList11.reverseSorting = False
        Me.DayList11.selected = -1
        Me.DayList11.selectedClickAllowed = True
        Me.DayList11.selectMultiple = False
        Me.DayList11.Size = New System.Drawing.Size(80, 72)
        Me.DayList11.sorted = False
        Me.DayList11.TabIndex = 142
        Me.DayList11.Tag = "NOHIDE"
        Me.DayList11.toolTipText = Nothing
        Me.DayList11.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList11.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList11.vScrolling = True
        Me.DayList11.vsValue = 0
        '
        'DayList12
        '
        Me.DayList12.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList12.autoAdjust = True
        Me.DayList12.autoKeyDownSelection = True
        Me.DayList12.autoSizeHorizontally = False
        Me.DayList12.autoSizeVertically = False
        Me.DayList12.BackColor = System.Drawing.Color.White
        Me.DayList12.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList12.baseBackColor = System.Drawing.Color.White
        Me.DayList12.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList12.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList12.bgColor = System.Drawing.Color.White
        Me.DayList12.borderColor = System.Drawing.Color.Empty
        Me.DayList12.borderSelColor = System.Drawing.Color.Empty
        Me.DayList12.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList12.CausesValidation = False
        Me.DayList12.clickEnabled = True
        Me.DayList12.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList12.do3D = False
        Me.DayList12.draw = False
        Me.DayList12.extraWidth = 0
        Me.DayList12.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList12.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList12.hScrolling = True
        Me.DayList12.hsValue = 0
        Me.DayList12.icons = CType(resources.GetObject("DayList12.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList12.itemBorder = 0
        Me.DayList12.itemMargin = 0
        Me.DayList12.items = CType(resources.GetObject("DayList12.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList12.Location = New System.Drawing.Point(186, 299)
        Me.DayList12.mouseMove3D = False
        Me.DayList12.mouseSpeed = 0
        Me.DayList12.Name = "DayList12"
        Me.DayList12.objMaxHeight = 0.0!
        Me.DayList12.objMaxWidth = 0.0!
        Me.DayList12.objMinHeight = 0.0!
        Me.DayList12.objMinWidth = 0.0!
        Me.DayList12.reverseSorting = False
        Me.DayList12.selected = -1
        Me.DayList12.selectedClickAllowed = True
        Me.DayList12.selectMultiple = False
        Me.DayList12.Size = New System.Drawing.Size(80, 72)
        Me.DayList12.sorted = False
        Me.DayList12.TabIndex = 143
        Me.DayList12.Tag = "NOHIDE"
        Me.DayList12.toolTipText = Nothing
        Me.DayList12.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList12.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList12.vScrolling = True
        Me.DayList12.vsValue = 0
        '
        'DayList13
        '
        Me.DayList13.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList13.autoAdjust = True
        Me.DayList13.autoKeyDownSelection = True
        Me.DayList13.autoSizeHorizontally = False
        Me.DayList13.autoSizeVertically = False
        Me.DayList13.BackColor = System.Drawing.Color.White
        Me.DayList13.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList13.baseBackColor = System.Drawing.Color.White
        Me.DayList13.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList13.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList13.bgColor = System.Drawing.Color.White
        Me.DayList13.borderColor = System.Drawing.Color.Empty
        Me.DayList13.borderSelColor = System.Drawing.Color.Empty
        Me.DayList13.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList13.CausesValidation = False
        Me.DayList13.clickEnabled = True
        Me.DayList13.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList13.do3D = False
        Me.DayList13.draw = False
        Me.DayList13.extraWidth = 0
        Me.DayList13.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList13.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList13.hScrolling = True
        Me.DayList13.hsValue = 0
        Me.DayList13.icons = CType(resources.GetObject("DayList13.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList13.itemBorder = 0
        Me.DayList13.itemMargin = 0
        Me.DayList13.items = CType(resources.GetObject("DayList13.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList13.Location = New System.Drawing.Point(202, 323)
        Me.DayList13.mouseMove3D = False
        Me.DayList13.mouseSpeed = 0
        Me.DayList13.Name = "DayList13"
        Me.DayList13.objMaxHeight = 0.0!
        Me.DayList13.objMaxWidth = 0.0!
        Me.DayList13.objMinHeight = 0.0!
        Me.DayList13.objMinWidth = 0.0!
        Me.DayList13.reverseSorting = False
        Me.DayList13.selected = -1
        Me.DayList13.selectedClickAllowed = True
        Me.DayList13.selectMultiple = False
        Me.DayList13.Size = New System.Drawing.Size(80, 64)
        Me.DayList13.sorted = False
        Me.DayList13.TabIndex = 144
        Me.DayList13.Tag = "NOHIDE"
        Me.DayList13.toolTipText = Nothing
        Me.DayList13.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList13.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList13.vScrolling = True
        Me.DayList13.vsValue = 0
        '
        'DayList14
        '
        Me.DayList14.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList14.autoAdjust = True
        Me.DayList14.autoKeyDownSelection = True
        Me.DayList14.autoSizeHorizontally = False
        Me.DayList14.autoSizeVertically = False
        Me.DayList14.BackColor = System.Drawing.Color.White
        Me.DayList14.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList14.baseBackColor = System.Drawing.Color.White
        Me.DayList14.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList14.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList14.bgColor = System.Drawing.Color.White
        Me.DayList14.borderColor = System.Drawing.Color.Empty
        Me.DayList14.borderSelColor = System.Drawing.Color.Empty
        Me.DayList14.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList14.CausesValidation = False
        Me.DayList14.clickEnabled = True
        Me.DayList14.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList14.do3D = False
        Me.DayList14.draw = False
        Me.DayList14.extraWidth = 0
        Me.DayList14.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList14.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList14.hScrolling = True
        Me.DayList14.hsValue = 0
        Me.DayList14.icons = CType(resources.GetObject("DayList14.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList14.itemBorder = 0
        Me.DayList14.itemMargin = 0
        Me.DayList14.items = CType(resources.GetObject("DayList14.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList14.Location = New System.Drawing.Point(210, 331)
        Me.DayList14.mouseMove3D = False
        Me.DayList14.mouseSpeed = 0
        Me.DayList14.Name = "DayList14"
        Me.DayList14.objMaxHeight = 0.0!
        Me.DayList14.objMaxWidth = 0.0!
        Me.DayList14.objMinHeight = 0.0!
        Me.DayList14.objMinWidth = 0.0!
        Me.DayList14.reverseSorting = False
        Me.DayList14.selected = -1
        Me.DayList14.selectedClickAllowed = True
        Me.DayList14.selectMultiple = False
        Me.DayList14.Size = New System.Drawing.Size(80, 64)
        Me.DayList14.sorted = False
        Me.DayList14.TabIndex = 145
        Me.DayList14.Tag = "NOHIDE"
        Me.DayList14.toolTipText = Nothing
        Me.DayList14.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList14.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList14.vScrolling = True
        Me.DayList14.vsValue = 0
        '
        'DayList15
        '
        Me.DayList15.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList15.autoAdjust = True
        Me.DayList15.autoKeyDownSelection = True
        Me.DayList15.autoSizeHorizontally = False
        Me.DayList15.autoSizeVertically = False
        Me.DayList15.BackColor = System.Drawing.Color.White
        Me.DayList15.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList15.baseBackColor = System.Drawing.Color.White
        Me.DayList15.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList15.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList15.bgColor = System.Drawing.Color.White
        Me.DayList15.borderColor = System.Drawing.Color.Empty
        Me.DayList15.borderSelColor = System.Drawing.Color.Empty
        Me.DayList15.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList15.CausesValidation = False
        Me.DayList15.clickEnabled = True
        Me.DayList15.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList15.do3D = False
        Me.DayList15.draw = False
        Me.DayList15.extraWidth = 0
        Me.DayList15.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList15.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList15.hScrolling = True
        Me.DayList15.hsValue = 0
        Me.DayList15.icons = CType(resources.GetObject("DayList15.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList15.itemBorder = 0
        Me.DayList15.itemMargin = 0
        Me.DayList15.items = CType(resources.GetObject("DayList15.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList15.Location = New System.Drawing.Point(210, 299)
        Me.DayList15.mouseMove3D = False
        Me.DayList15.mouseSpeed = 0
        Me.DayList15.Name = "DayList15"
        Me.DayList15.objMaxHeight = 0.0!
        Me.DayList15.objMaxWidth = 0.0!
        Me.DayList15.objMinHeight = 0.0!
        Me.DayList15.objMinWidth = 0.0!
        Me.DayList15.reverseSorting = False
        Me.DayList15.selected = -1
        Me.DayList15.selectedClickAllowed = True
        Me.DayList15.selectMultiple = False
        Me.DayList15.Size = New System.Drawing.Size(80, 72)
        Me.DayList15.sorted = False
        Me.DayList15.TabIndex = 146
        Me.DayList15.Tag = "NOHIDE"
        Me.DayList15.toolTipText = Nothing
        Me.DayList15.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList15.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList15.vScrolling = True
        Me.DayList15.vsValue = 0
        '
        'DayList16
        '
        Me.DayList16.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList16.autoAdjust = True
        Me.DayList16.autoKeyDownSelection = True
        Me.DayList16.autoSizeHorizontally = False
        Me.DayList16.autoSizeVertically = False
        Me.DayList16.BackColor = System.Drawing.Color.White
        Me.DayList16.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList16.baseBackColor = System.Drawing.Color.White
        Me.DayList16.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList16.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList16.bgColor = System.Drawing.Color.White
        Me.DayList16.borderColor = System.Drawing.Color.Empty
        Me.DayList16.borderSelColor = System.Drawing.Color.Empty
        Me.DayList16.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList16.CausesValidation = False
        Me.DayList16.clickEnabled = True
        Me.DayList16.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList16.do3D = False
        Me.DayList16.draw = False
        Me.DayList16.extraWidth = 0
        Me.DayList16.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList16.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList16.hScrolling = True
        Me.DayList16.hsValue = 0
        Me.DayList16.icons = CType(resources.GetObject("DayList16.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList16.itemBorder = 0
        Me.DayList16.itemMargin = 0
        Me.DayList16.items = CType(resources.GetObject("DayList16.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList16.Location = New System.Drawing.Point(218, 307)
        Me.DayList16.mouseMove3D = False
        Me.DayList16.mouseSpeed = 0
        Me.DayList16.Name = "DayList16"
        Me.DayList16.objMaxHeight = 0.0!
        Me.DayList16.objMaxWidth = 0.0!
        Me.DayList16.objMinHeight = 0.0!
        Me.DayList16.objMinWidth = 0.0!
        Me.DayList16.reverseSorting = False
        Me.DayList16.selected = -1
        Me.DayList16.selectedClickAllowed = True
        Me.DayList16.selectMultiple = False
        Me.DayList16.Size = New System.Drawing.Size(72, 72)
        Me.DayList16.sorted = False
        Me.DayList16.TabIndex = 147
        Me.DayList16.Tag = "NOHIDE"
        Me.DayList16.toolTipText = Nothing
        Me.DayList16.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList16.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList16.vScrolling = True
        Me.DayList16.vsValue = 0
        '
        'DayList17
        '
        Me.DayList17.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList17.autoAdjust = True
        Me.DayList17.autoKeyDownSelection = True
        Me.DayList17.autoSizeHorizontally = False
        Me.DayList17.autoSizeVertically = False
        Me.DayList17.BackColor = System.Drawing.Color.White
        Me.DayList17.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList17.baseBackColor = System.Drawing.Color.White
        Me.DayList17.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList17.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList17.bgColor = System.Drawing.Color.White
        Me.DayList17.borderColor = System.Drawing.Color.Empty
        Me.DayList17.borderSelColor = System.Drawing.Color.Empty
        Me.DayList17.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList17.CausesValidation = False
        Me.DayList17.clickEnabled = True
        Me.DayList17.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList17.do3D = False
        Me.DayList17.draw = False
        Me.DayList17.extraWidth = 0
        Me.DayList17.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList17.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList17.hScrolling = True
        Me.DayList17.hsValue = 0
        Me.DayList17.icons = CType(resources.GetObject("DayList17.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList17.itemBorder = 0
        Me.DayList17.itemMargin = 0
        Me.DayList17.items = CType(resources.GetObject("DayList17.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList17.Location = New System.Drawing.Point(194, 299)
        Me.DayList17.mouseMove3D = False
        Me.DayList17.mouseSpeed = 0
        Me.DayList17.Name = "DayList17"
        Me.DayList17.objMaxHeight = 0.0!
        Me.DayList17.objMaxWidth = 0.0!
        Me.DayList17.objMinHeight = 0.0!
        Me.DayList17.objMinWidth = 0.0!
        Me.DayList17.reverseSorting = False
        Me.DayList17.selected = -1
        Me.DayList17.selectedClickAllowed = True
        Me.DayList17.selectMultiple = False
        Me.DayList17.Size = New System.Drawing.Size(72, 72)
        Me.DayList17.sorted = False
        Me.DayList17.TabIndex = 148
        Me.DayList17.Tag = "NOHIDE"
        Me.DayList17.toolTipText = Nothing
        Me.DayList17.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList17.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList17.vScrolling = True
        Me.DayList17.vsValue = 0
        '
        'DayList18
        '
        Me.DayList18.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList18.autoAdjust = True
        Me.DayList18.autoKeyDownSelection = True
        Me.DayList18.autoSizeHorizontally = False
        Me.DayList18.autoSizeVertically = False
        Me.DayList18.BackColor = System.Drawing.Color.White
        Me.DayList18.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList18.baseBackColor = System.Drawing.Color.White
        Me.DayList18.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList18.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList18.bgColor = System.Drawing.Color.White
        Me.DayList18.borderColor = System.Drawing.Color.Empty
        Me.DayList18.borderSelColor = System.Drawing.Color.Empty
        Me.DayList18.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList18.CausesValidation = False
        Me.DayList18.clickEnabled = True
        Me.DayList18.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList18.do3D = False
        Me.DayList18.draw = False
        Me.DayList18.extraWidth = 0
        Me.DayList18.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList18.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList18.hScrolling = True
        Me.DayList18.hsValue = 0
        Me.DayList18.icons = CType(resources.GetObject("DayList18.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList18.itemBorder = 0
        Me.DayList18.itemMargin = 0
        Me.DayList18.items = CType(resources.GetObject("DayList18.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList18.Location = New System.Drawing.Point(202, 307)
        Me.DayList18.mouseMove3D = False
        Me.DayList18.mouseSpeed = 0
        Me.DayList18.Name = "DayList18"
        Me.DayList18.objMaxHeight = 0.0!
        Me.DayList18.objMaxWidth = 0.0!
        Me.DayList18.objMinHeight = 0.0!
        Me.DayList18.objMinWidth = 0.0!
        Me.DayList18.reverseSorting = False
        Me.DayList18.selected = -1
        Me.DayList18.selectedClickAllowed = True
        Me.DayList18.selectMultiple = False
        Me.DayList18.Size = New System.Drawing.Size(72, 72)
        Me.DayList18.sorted = False
        Me.DayList18.TabIndex = 149
        Me.DayList18.Tag = "NOHIDE"
        Me.DayList18.toolTipText = Nothing
        Me.DayList18.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList18.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList18.vScrolling = True
        Me.DayList18.vsValue = 0
        '
        'DayList19
        '
        Me.DayList19.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList19.autoAdjust = True
        Me.DayList19.autoKeyDownSelection = True
        Me.DayList19.autoSizeHorizontally = False
        Me.DayList19.autoSizeVertically = False
        Me.DayList19.BackColor = System.Drawing.Color.White
        Me.DayList19.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList19.baseBackColor = System.Drawing.Color.White
        Me.DayList19.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList19.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList19.bgColor = System.Drawing.Color.White
        Me.DayList19.borderColor = System.Drawing.Color.Empty
        Me.DayList19.borderSelColor = System.Drawing.Color.Empty
        Me.DayList19.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList19.CausesValidation = False
        Me.DayList19.clickEnabled = True
        Me.DayList19.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList19.do3D = False
        Me.DayList19.draw = False
        Me.DayList19.extraWidth = 0
        Me.DayList19.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList19.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList19.hScrolling = True
        Me.DayList19.hsValue = 0
        Me.DayList19.icons = CType(resources.GetObject("DayList19.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList19.itemBorder = 0
        Me.DayList19.itemMargin = 0
        Me.DayList19.items = CType(resources.GetObject("DayList19.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList19.Location = New System.Drawing.Point(210, 315)
        Me.DayList19.mouseMove3D = False
        Me.DayList19.mouseSpeed = 0
        Me.DayList19.Name = "DayList19"
        Me.DayList19.objMaxHeight = 0.0!
        Me.DayList19.objMaxWidth = 0.0!
        Me.DayList19.objMinHeight = 0.0!
        Me.DayList19.objMinWidth = 0.0!
        Me.DayList19.reverseSorting = False
        Me.DayList19.selected = -1
        Me.DayList19.selectedClickAllowed = True
        Me.DayList19.selectMultiple = False
        Me.DayList19.Size = New System.Drawing.Size(72, 72)
        Me.DayList19.sorted = False
        Me.DayList19.TabIndex = 150
        Me.DayList19.Tag = "NOHIDE"
        Me.DayList19.toolTipText = Nothing
        Me.DayList19.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList19.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList19.vScrolling = True
        Me.DayList19.vsValue = 0
        '
        'DayList20
        '
        Me.DayList20.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList20.autoAdjust = True
        Me.DayList20.autoKeyDownSelection = True
        Me.DayList20.autoSizeHorizontally = False
        Me.DayList20.autoSizeVertically = False
        Me.DayList20.BackColor = System.Drawing.Color.White
        Me.DayList20.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList20.baseBackColor = System.Drawing.Color.White
        Me.DayList20.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList20.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList20.bgColor = System.Drawing.Color.White
        Me.DayList20.borderColor = System.Drawing.Color.Empty
        Me.DayList20.borderSelColor = System.Drawing.Color.Empty
        Me.DayList20.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList20.CausesValidation = False
        Me.DayList20.clickEnabled = True
        Me.DayList20.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList20.do3D = False
        Me.DayList20.draw = False
        Me.DayList20.extraWidth = 0
        Me.DayList20.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList20.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList20.hScrolling = True
        Me.DayList20.hsValue = 0
        Me.DayList20.icons = CType(resources.GetObject("DayList20.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList20.itemBorder = 0
        Me.DayList20.itemMargin = 0
        Me.DayList20.items = CType(resources.GetObject("DayList20.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList20.Location = New System.Drawing.Point(218, 323)
        Me.DayList20.mouseMove3D = False
        Me.DayList20.mouseSpeed = 0
        Me.DayList20.Name = "DayList20"
        Me.DayList20.objMaxHeight = 0.0!
        Me.DayList20.objMaxWidth = 0.0!
        Me.DayList20.objMinHeight = 0.0!
        Me.DayList20.objMinWidth = 0.0!
        Me.DayList20.reverseSorting = False
        Me.DayList20.selected = -1
        Me.DayList20.selectedClickAllowed = True
        Me.DayList20.selectMultiple = False
        Me.DayList20.Size = New System.Drawing.Size(72, 64)
        Me.DayList20.sorted = False
        Me.DayList20.TabIndex = 151
        Me.DayList20.Tag = "NOHIDE"
        Me.DayList20.toolTipText = Nothing
        Me.DayList20.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList20.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList20.vScrolling = True
        Me.DayList20.vsValue = 0
        '
        'DayList21
        '
        Me.DayList21.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList21.autoAdjust = True
        Me.DayList21.autoKeyDownSelection = True
        Me.DayList21.autoSizeHorizontally = False
        Me.DayList21.autoSizeVertically = False
        Me.DayList21.BackColor = System.Drawing.Color.White
        Me.DayList21.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList21.baseBackColor = System.Drawing.Color.White
        Me.DayList21.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList21.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList21.bgColor = System.Drawing.Color.White
        Me.DayList21.borderColor = System.Drawing.Color.Empty
        Me.DayList21.borderSelColor = System.Drawing.Color.Empty
        Me.DayList21.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList21.CausesValidation = False
        Me.DayList21.clickEnabled = True
        Me.DayList21.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList21.do3D = False
        Me.DayList21.draw = False
        Me.DayList21.extraWidth = 0
        Me.DayList21.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList21.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList21.hScrolling = True
        Me.DayList21.hsValue = 0
        Me.DayList21.icons = CType(resources.GetObject("DayList21.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList21.itemBorder = 0
        Me.DayList21.itemMargin = 0
        Me.DayList21.items = CType(resources.GetObject("DayList21.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList21.Location = New System.Drawing.Point(186, 323)
        Me.DayList21.mouseMove3D = False
        Me.DayList21.mouseSpeed = 0
        Me.DayList21.Name = "DayList21"
        Me.DayList21.objMaxHeight = 0.0!
        Me.DayList21.objMaxWidth = 0.0!
        Me.DayList21.objMinHeight = 0.0!
        Me.DayList21.objMinWidth = 0.0!
        Me.DayList21.reverseSorting = False
        Me.DayList21.selected = -1
        Me.DayList21.selectedClickAllowed = True
        Me.DayList21.selectMultiple = False
        Me.DayList21.Size = New System.Drawing.Size(72, 64)
        Me.DayList21.sorted = False
        Me.DayList21.TabIndex = 152
        Me.DayList21.Tag = "NOHIDE"
        Me.DayList21.toolTipText = Nothing
        Me.DayList21.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList21.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList21.vScrolling = True
        Me.DayList21.vsValue = 0
        '
        'DayList22
        '
        Me.DayList22.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList22.autoAdjust = True
        Me.DayList22.autoKeyDownSelection = True
        Me.DayList22.autoSizeHorizontally = False
        Me.DayList22.autoSizeVertically = False
        Me.DayList22.BackColor = System.Drawing.Color.White
        Me.DayList22.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList22.baseBackColor = System.Drawing.Color.White
        Me.DayList22.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList22.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList22.bgColor = System.Drawing.Color.White
        Me.DayList22.borderColor = System.Drawing.Color.Empty
        Me.DayList22.borderSelColor = System.Drawing.Color.Empty
        Me.DayList22.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList22.CausesValidation = False
        Me.DayList22.clickEnabled = True
        Me.DayList22.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList22.do3D = False
        Me.DayList22.draw = False
        Me.DayList22.extraWidth = 0
        Me.DayList22.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList22.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList22.hScrolling = True
        Me.DayList22.hsValue = 0
        Me.DayList22.icons = CType(resources.GetObject("DayList22.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList22.itemBorder = 0
        Me.DayList22.itemMargin = 0
        Me.DayList22.items = CType(resources.GetObject("DayList22.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList22.Location = New System.Drawing.Point(210, 315)
        Me.DayList22.mouseMove3D = False
        Me.DayList22.mouseSpeed = 0
        Me.DayList22.Name = "DayList22"
        Me.DayList22.objMaxHeight = 0.0!
        Me.DayList22.objMaxWidth = 0.0!
        Me.DayList22.objMinHeight = 0.0!
        Me.DayList22.objMinWidth = 0.0!
        Me.DayList22.reverseSorting = False
        Me.DayList22.selected = -1
        Me.DayList22.selectedClickAllowed = True
        Me.DayList22.selectMultiple = False
        Me.DayList22.Size = New System.Drawing.Size(72, 72)
        Me.DayList22.sorted = False
        Me.DayList22.TabIndex = 153
        Me.DayList22.Tag = "NOHIDE"
        Me.DayList22.toolTipText = Nothing
        Me.DayList22.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList22.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList22.vScrolling = True
        Me.DayList22.vsValue = 0
        '
        'DayList23
        '
        Me.DayList23.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList23.autoAdjust = True
        Me.DayList23.autoKeyDownSelection = True
        Me.DayList23.autoSizeHorizontally = False
        Me.DayList23.autoSizeVertically = False
        Me.DayList23.BackColor = System.Drawing.Color.White
        Me.DayList23.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList23.baseBackColor = System.Drawing.Color.White
        Me.DayList23.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList23.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList23.bgColor = System.Drawing.Color.White
        Me.DayList23.borderColor = System.Drawing.Color.Empty
        Me.DayList23.borderSelColor = System.Drawing.Color.Empty
        Me.DayList23.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList23.CausesValidation = False
        Me.DayList23.clickEnabled = True
        Me.DayList23.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList23.do3D = False
        Me.DayList23.draw = False
        Me.DayList23.extraWidth = 0
        Me.DayList23.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList23.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList23.hScrolling = True
        Me.DayList23.hsValue = 0
        Me.DayList23.icons = CType(resources.GetObject("DayList23.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList23.itemBorder = 0
        Me.DayList23.itemMargin = 0
        Me.DayList23.items = CType(resources.GetObject("DayList23.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList23.Location = New System.Drawing.Point(218, 323)
        Me.DayList23.mouseMove3D = False
        Me.DayList23.mouseSpeed = 0
        Me.DayList23.Name = "DayList23"
        Me.DayList23.objMaxHeight = 0.0!
        Me.DayList23.objMaxWidth = 0.0!
        Me.DayList23.objMinHeight = 0.0!
        Me.DayList23.objMinWidth = 0.0!
        Me.DayList23.reverseSorting = False
        Me.DayList23.selected = -1
        Me.DayList23.selectedClickAllowed = True
        Me.DayList23.selectMultiple = False
        Me.DayList23.Size = New System.Drawing.Size(72, 64)
        Me.DayList23.sorted = False
        Me.DayList23.TabIndex = 154
        Me.DayList23.Tag = "NOHIDE"
        Me.DayList23.toolTipText = Nothing
        Me.DayList23.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList23.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList23.vScrolling = True
        Me.DayList23.vsValue = 0
        '
        'DayList24
        '
        Me.DayList24.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList24.autoAdjust = True
        Me.DayList24.autoKeyDownSelection = True
        Me.DayList24.autoSizeHorizontally = False
        Me.DayList24.autoSizeVertically = False
        Me.DayList24.BackColor = System.Drawing.Color.White
        Me.DayList24.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList24.baseBackColor = System.Drawing.Color.White
        Me.DayList24.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList24.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList24.bgColor = System.Drawing.Color.White
        Me.DayList24.borderColor = System.Drawing.Color.Empty
        Me.DayList24.borderSelColor = System.Drawing.Color.Empty
        Me.DayList24.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList24.CausesValidation = False
        Me.DayList24.clickEnabled = True
        Me.DayList24.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList24.do3D = False
        Me.DayList24.draw = False
        Me.DayList24.extraWidth = 0
        Me.DayList24.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList24.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList24.hScrolling = True
        Me.DayList24.hsValue = 0
        Me.DayList24.icons = CType(resources.GetObject("DayList24.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList24.itemBorder = 0
        Me.DayList24.itemMargin = 0
        Me.DayList24.items = CType(resources.GetObject("DayList24.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList24.Location = New System.Drawing.Point(194, 315)
        Me.DayList24.mouseMove3D = False
        Me.DayList24.mouseSpeed = 0
        Me.DayList24.Name = "DayList24"
        Me.DayList24.objMaxHeight = 0.0!
        Me.DayList24.objMaxWidth = 0.0!
        Me.DayList24.objMinHeight = 0.0!
        Me.DayList24.objMinWidth = 0.0!
        Me.DayList24.reverseSorting = False
        Me.DayList24.selected = -1
        Me.DayList24.selectedClickAllowed = True
        Me.DayList24.selectMultiple = False
        Me.DayList24.Size = New System.Drawing.Size(72, 72)
        Me.DayList24.sorted = False
        Me.DayList24.TabIndex = 155
        Me.DayList24.Tag = "NOHIDE"
        Me.DayList24.toolTipText = Nothing
        Me.DayList24.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList24.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList24.vScrolling = True
        Me.DayList24.vsValue = 0
        '
        'DayList25
        '
        Me.DayList25.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList25.autoAdjust = True
        Me.DayList25.autoKeyDownSelection = True
        Me.DayList25.autoSizeHorizontally = False
        Me.DayList25.autoSizeVertically = False
        Me.DayList25.BackColor = System.Drawing.Color.White
        Me.DayList25.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList25.baseBackColor = System.Drawing.Color.White
        Me.DayList25.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList25.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList25.bgColor = System.Drawing.Color.White
        Me.DayList25.borderColor = System.Drawing.Color.Empty
        Me.DayList25.borderSelColor = System.Drawing.Color.Empty
        Me.DayList25.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList25.CausesValidation = False
        Me.DayList25.clickEnabled = True
        Me.DayList25.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList25.do3D = False
        Me.DayList25.draw = False
        Me.DayList25.extraWidth = 0
        Me.DayList25.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList25.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList25.hScrolling = True
        Me.DayList25.hsValue = 0
        Me.DayList25.icons = CType(resources.GetObject("DayList25.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList25.itemBorder = 0
        Me.DayList25.itemMargin = 0
        Me.DayList25.items = CType(resources.GetObject("DayList25.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList25.Location = New System.Drawing.Point(202, 323)
        Me.DayList25.mouseMove3D = False
        Me.DayList25.mouseSpeed = 0
        Me.DayList25.Name = "DayList25"
        Me.DayList25.objMaxHeight = 0.0!
        Me.DayList25.objMaxWidth = 0.0!
        Me.DayList25.objMinHeight = 0.0!
        Me.DayList25.objMinWidth = 0.0!
        Me.DayList25.reverseSorting = False
        Me.DayList25.selected = -1
        Me.DayList25.selectedClickAllowed = True
        Me.DayList25.selectMultiple = False
        Me.DayList25.Size = New System.Drawing.Size(72, 64)
        Me.DayList25.sorted = False
        Me.DayList25.TabIndex = 156
        Me.DayList25.Tag = "NOHIDE"
        Me.DayList25.toolTipText = Nothing
        Me.DayList25.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList25.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList25.vScrolling = True
        Me.DayList25.vsValue = 0
        '
        'DayList26
        '
        Me.DayList26.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList26.autoAdjust = True
        Me.DayList26.autoKeyDownSelection = True
        Me.DayList26.autoSizeHorizontally = False
        Me.DayList26.autoSizeVertically = False
        Me.DayList26.BackColor = System.Drawing.Color.White
        Me.DayList26.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList26.baseBackColor = System.Drawing.Color.White
        Me.DayList26.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList26.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList26.bgColor = System.Drawing.Color.White
        Me.DayList26.borderColor = System.Drawing.Color.Empty
        Me.DayList26.borderSelColor = System.Drawing.Color.Empty
        Me.DayList26.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList26.CausesValidation = False
        Me.DayList26.clickEnabled = True
        Me.DayList26.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList26.do3D = False
        Me.DayList26.draw = False
        Me.DayList26.extraWidth = 0
        Me.DayList26.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList26.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList26.hScrolling = True
        Me.DayList26.hsValue = 0
        Me.DayList26.icons = CType(resources.GetObject("DayList26.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList26.itemBorder = 0
        Me.DayList26.itemMargin = 0
        Me.DayList26.items = CType(resources.GetObject("DayList26.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList26.Location = New System.Drawing.Point(210, 331)
        Me.DayList26.mouseMove3D = False
        Me.DayList26.mouseSpeed = 0
        Me.DayList26.Name = "DayList26"
        Me.DayList26.objMaxHeight = 0.0!
        Me.DayList26.objMaxWidth = 0.0!
        Me.DayList26.objMinHeight = 0.0!
        Me.DayList26.objMinWidth = 0.0!
        Me.DayList26.reverseSorting = False
        Me.DayList26.selected = -1
        Me.DayList26.selectedClickAllowed = True
        Me.DayList26.selectMultiple = False
        Me.DayList26.Size = New System.Drawing.Size(72, 64)
        Me.DayList26.sorted = False
        Me.DayList26.TabIndex = 157
        Me.DayList26.Tag = "NOHIDE"
        Me.DayList26.toolTipText = Nothing
        Me.DayList26.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList26.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList26.vScrolling = True
        Me.DayList26.vsValue = 0
        '
        'DayList27
        '
        Me.DayList27.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList27.autoAdjust = True
        Me.DayList27.autoKeyDownSelection = True
        Me.DayList27.autoSizeHorizontally = False
        Me.DayList27.autoSizeVertically = False
        Me.DayList27.BackColor = System.Drawing.Color.White
        Me.DayList27.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList27.baseBackColor = System.Drawing.Color.White
        Me.DayList27.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList27.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList27.bgColor = System.Drawing.Color.White
        Me.DayList27.borderColor = System.Drawing.Color.Empty
        Me.DayList27.borderSelColor = System.Drawing.Color.Empty
        Me.DayList27.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList27.CausesValidation = False
        Me.DayList27.clickEnabled = True
        Me.DayList27.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList27.do3D = False
        Me.DayList27.draw = False
        Me.DayList27.extraWidth = 0
        Me.DayList27.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList27.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList27.hScrolling = True
        Me.DayList27.hsValue = 0
        Me.DayList27.icons = CType(resources.GetObject("DayList27.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList27.itemBorder = 0
        Me.DayList27.itemMargin = 0
        Me.DayList27.items = CType(resources.GetObject("DayList27.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList27.Location = New System.Drawing.Point(210, 299)
        Me.DayList27.mouseMove3D = False
        Me.DayList27.mouseSpeed = 0
        Me.DayList27.Name = "DayList27"
        Me.DayList27.objMaxHeight = 0.0!
        Me.DayList27.objMaxWidth = 0.0!
        Me.DayList27.objMinHeight = 0.0!
        Me.DayList27.objMinWidth = 0.0!
        Me.DayList27.reverseSorting = False
        Me.DayList27.selected = -1
        Me.DayList27.selectedClickAllowed = True
        Me.DayList27.selectMultiple = False
        Me.DayList27.Size = New System.Drawing.Size(72, 72)
        Me.DayList27.sorted = False
        Me.DayList27.TabIndex = 158
        Me.DayList27.Tag = "NOHIDE"
        Me.DayList27.toolTipText = Nothing
        Me.DayList27.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList27.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList27.vScrolling = True
        Me.DayList27.vsValue = 0
        '
        'DayList28
        '
        Me.DayList28.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList28.autoAdjust = True
        Me.DayList28.autoKeyDownSelection = True
        Me.DayList28.autoSizeHorizontally = False
        Me.DayList28.autoSizeVertically = False
        Me.DayList28.BackColor = System.Drawing.Color.White
        Me.DayList28.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList28.baseBackColor = System.Drawing.Color.White
        Me.DayList28.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList28.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList28.bgColor = System.Drawing.Color.White
        Me.DayList28.borderColor = System.Drawing.Color.Empty
        Me.DayList28.borderSelColor = System.Drawing.Color.Empty
        Me.DayList28.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList28.CausesValidation = False
        Me.DayList28.clickEnabled = True
        Me.DayList28.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList28.do3D = False
        Me.DayList28.draw = False
        Me.DayList28.extraWidth = 0
        Me.DayList28.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList28.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList28.hScrolling = True
        Me.DayList28.hsValue = 0
        Me.DayList28.icons = CType(resources.GetObject("DayList28.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList28.itemBorder = 0
        Me.DayList28.itemMargin = 0
        Me.DayList28.items = CType(resources.GetObject("DayList28.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList28.Location = New System.Drawing.Point(218, 307)
        Me.DayList28.mouseMove3D = False
        Me.DayList28.mouseSpeed = 0
        Me.DayList28.Name = "DayList28"
        Me.DayList28.objMaxHeight = 0.0!
        Me.DayList28.objMaxWidth = 0.0!
        Me.DayList28.objMinHeight = 0.0!
        Me.DayList28.objMinWidth = 0.0!
        Me.DayList28.reverseSorting = False
        Me.DayList28.selected = -1
        Me.DayList28.selectedClickAllowed = True
        Me.DayList28.selectMultiple = False
        Me.DayList28.Size = New System.Drawing.Size(72, 72)
        Me.DayList28.sorted = False
        Me.DayList28.TabIndex = 159
        Me.DayList28.Tag = "NOHIDE"
        Me.DayList28.toolTipText = Nothing
        Me.DayList28.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList28.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList28.vScrolling = True
        Me.DayList28.vsValue = 0
        '
        'DayList29
        '
        Me.DayList29.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList29.autoAdjust = True
        Me.DayList29.autoKeyDownSelection = True
        Me.DayList29.autoSizeHorizontally = False
        Me.DayList29.autoSizeVertically = False
        Me.DayList29.BackColor = System.Drawing.Color.White
        Me.DayList29.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList29.baseBackColor = System.Drawing.Color.White
        Me.DayList29.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList29.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList29.bgColor = System.Drawing.Color.White
        Me.DayList29.borderColor = System.Drawing.Color.Empty
        Me.DayList29.borderSelColor = System.Drawing.Color.Empty
        Me.DayList29.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList29.CausesValidation = False
        Me.DayList29.clickEnabled = True
        Me.DayList29.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList29.do3D = False
        Me.DayList29.draw = False
        Me.DayList29.extraWidth = 0
        Me.DayList29.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList29.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList29.hScrolling = True
        Me.DayList29.hsValue = 0
        Me.DayList29.icons = CType(resources.GetObject("DayList29.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList29.itemBorder = 0
        Me.DayList29.itemMargin = 0
        Me.DayList29.items = CType(resources.GetObject("DayList29.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList29.Location = New System.Drawing.Point(186, 307)
        Me.DayList29.mouseMove3D = False
        Me.DayList29.mouseSpeed = 0
        Me.DayList29.Name = "DayList29"
        Me.DayList29.objMaxHeight = 0.0!
        Me.DayList29.objMaxWidth = 0.0!
        Me.DayList29.objMinHeight = 0.0!
        Me.DayList29.objMinWidth = 0.0!
        Me.DayList29.reverseSorting = False
        Me.DayList29.selected = -1
        Me.DayList29.selectedClickAllowed = True
        Me.DayList29.selectMultiple = False
        Me.DayList29.Size = New System.Drawing.Size(72, 72)
        Me.DayList29.sorted = False
        Me.DayList29.TabIndex = 160
        Me.DayList29.Tag = "NOHIDE"
        Me.DayList29.toolTipText = Nothing
        Me.DayList29.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList29.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList29.vScrolling = True
        Me.DayList29.vsValue = 0
        '
        'DayList30
        '
        Me.DayList30.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList30.autoAdjust = True
        Me.DayList30.autoKeyDownSelection = True
        Me.DayList30.autoSizeHorizontally = False
        Me.DayList30.autoSizeVertically = False
        Me.DayList30.BackColor = System.Drawing.Color.White
        Me.DayList30.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList30.baseBackColor = System.Drawing.Color.White
        Me.DayList30.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList30.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList30.bgColor = System.Drawing.Color.White
        Me.DayList30.borderColor = System.Drawing.Color.Empty
        Me.DayList30.borderSelColor = System.Drawing.Color.Empty
        Me.DayList30.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList30.CausesValidation = False
        Me.DayList30.clickEnabled = True
        Me.DayList30.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList30.do3D = False
        Me.DayList30.draw = False
        Me.DayList30.extraWidth = 0
        Me.DayList30.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList30.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList30.hScrolling = True
        Me.DayList30.hsValue = 0
        Me.DayList30.icons = CType(resources.GetObject("DayList30.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList30.itemBorder = 0
        Me.DayList30.itemMargin = 0
        Me.DayList30.items = CType(resources.GetObject("DayList30.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList30.Location = New System.Drawing.Point(202, 315)
        Me.DayList30.mouseMove3D = False
        Me.DayList30.mouseSpeed = 0
        Me.DayList30.Name = "DayList30"
        Me.DayList30.objMaxHeight = 0.0!
        Me.DayList30.objMaxWidth = 0.0!
        Me.DayList30.objMinHeight = 0.0!
        Me.DayList30.objMinWidth = 0.0!
        Me.DayList30.reverseSorting = False
        Me.DayList30.selected = -1
        Me.DayList30.selectedClickAllowed = True
        Me.DayList30.selectMultiple = False
        Me.DayList30.Size = New System.Drawing.Size(80, 72)
        Me.DayList30.sorted = False
        Me.DayList30.TabIndex = 161
        Me.DayList30.Tag = "NOHIDE"
        Me.DayList30.toolTipText = Nothing
        Me.DayList30.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList30.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList30.vScrolling = True
        Me.DayList30.vsValue = 0
        '
        'DayList31
        '
        Me.DayList31.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList31.autoAdjust = True
        Me.DayList31.autoKeyDownSelection = True
        Me.DayList31.autoSizeHorizontally = False
        Me.DayList31.autoSizeVertically = False
        Me.DayList31.BackColor = System.Drawing.Color.White
        Me.DayList31.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList31.baseBackColor = System.Drawing.Color.White
        Me.DayList31.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList31.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList31.bgColor = System.Drawing.Color.White
        Me.DayList31.borderColor = System.Drawing.Color.Empty
        Me.DayList31.borderSelColor = System.Drawing.Color.Empty
        Me.DayList31.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList31.CausesValidation = False
        Me.DayList31.clickEnabled = True
        Me.DayList31.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList31.do3D = False
        Me.DayList31.draw = False
        Me.DayList31.extraWidth = 0
        Me.DayList31.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList31.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList31.hScrolling = True
        Me.DayList31.hsValue = 0
        Me.DayList31.icons = CType(resources.GetObject("DayList31.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList31.itemBorder = 0
        Me.DayList31.itemMargin = 0
        Me.DayList31.items = CType(resources.GetObject("DayList31.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList31.Location = New System.Drawing.Point(194, 331)
        Me.DayList31.mouseMove3D = False
        Me.DayList31.mouseSpeed = 0
        Me.DayList31.Name = "DayList31"
        Me.DayList31.objMaxHeight = 0.0!
        Me.DayList31.objMaxWidth = 0.0!
        Me.DayList31.objMinHeight = 0.0!
        Me.DayList31.objMinWidth = 0.0!
        Me.DayList31.reverseSorting = False
        Me.DayList31.selected = -1
        Me.DayList31.selectedClickAllowed = True
        Me.DayList31.selectMultiple = False
        Me.DayList31.Size = New System.Drawing.Size(80, 64)
        Me.DayList31.sorted = False
        Me.DayList31.TabIndex = 162
        Me.DayList31.Tag = "NOHIDE"
        Me.DayList31.toolTipText = Nothing
        Me.DayList31.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList31.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList31.vScrolling = True
        Me.DayList31.vsValue = 0
        '
        'DayList32
        '
        Me.DayList32.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList32.autoAdjust = True
        Me.DayList32.autoKeyDownSelection = True
        Me.DayList32.autoSizeHorizontally = False
        Me.DayList32.autoSizeVertically = False
        Me.DayList32.BackColor = System.Drawing.Color.White
        Me.DayList32.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList32.baseBackColor = System.Drawing.Color.White
        Me.DayList32.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList32.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList32.bgColor = System.Drawing.Color.White
        Me.DayList32.borderColor = System.Drawing.Color.Empty
        Me.DayList32.borderSelColor = System.Drawing.Color.Empty
        Me.DayList32.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList32.CausesValidation = False
        Me.DayList32.clickEnabled = True
        Me.DayList32.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList32.do3D = False
        Me.DayList32.draw = False
        Me.DayList32.extraWidth = 0
        Me.DayList32.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList32.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList32.hScrolling = True
        Me.DayList32.hsValue = 0
        Me.DayList32.icons = CType(resources.GetObject("DayList32.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList32.itemBorder = 0
        Me.DayList32.itemMargin = 0
        Me.DayList32.items = CType(resources.GetObject("DayList32.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList32.Location = New System.Drawing.Point(210, 307)
        Me.DayList32.mouseMove3D = False
        Me.DayList32.mouseSpeed = 0
        Me.DayList32.Name = "DayList32"
        Me.DayList32.objMaxHeight = 0.0!
        Me.DayList32.objMaxWidth = 0.0!
        Me.DayList32.objMinHeight = 0.0!
        Me.DayList32.objMinWidth = 0.0!
        Me.DayList32.reverseSorting = False
        Me.DayList32.selected = -1
        Me.DayList32.selectedClickAllowed = True
        Me.DayList32.selectMultiple = False
        Me.DayList32.Size = New System.Drawing.Size(80, 72)
        Me.DayList32.sorted = False
        Me.DayList32.TabIndex = 163
        Me.DayList32.Tag = "NOHIDE"
        Me.DayList32.toolTipText = Nothing
        Me.DayList32.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList32.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList32.vScrolling = True
        Me.DayList32.vsValue = 0
        '
        'DayList33
        '
        Me.DayList33.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList33.autoAdjust = True
        Me.DayList33.autoKeyDownSelection = True
        Me.DayList33.autoSizeHorizontally = False
        Me.DayList33.autoSizeVertically = False
        Me.DayList33.BackColor = System.Drawing.Color.White
        Me.DayList33.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList33.baseBackColor = System.Drawing.Color.White
        Me.DayList33.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList33.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList33.bgColor = System.Drawing.Color.White
        Me.DayList33.borderColor = System.Drawing.Color.Empty
        Me.DayList33.borderSelColor = System.Drawing.Color.Empty
        Me.DayList33.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList33.CausesValidation = False
        Me.DayList33.clickEnabled = True
        Me.DayList33.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList33.do3D = False
        Me.DayList33.draw = False
        Me.DayList33.extraWidth = 0
        Me.DayList33.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList33.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList33.hScrolling = True
        Me.DayList33.hsValue = 0
        Me.DayList33.icons = CType(resources.GetObject("DayList33.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList33.itemBorder = 0
        Me.DayList33.itemMargin = 0
        Me.DayList33.items = CType(resources.GetObject("DayList33.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList33.Location = New System.Drawing.Point(178, 307)
        Me.DayList33.mouseMove3D = False
        Me.DayList33.mouseSpeed = 0
        Me.DayList33.Name = "DayList33"
        Me.DayList33.objMaxHeight = 0.0!
        Me.DayList33.objMaxWidth = 0.0!
        Me.DayList33.objMinHeight = 0.0!
        Me.DayList33.objMinWidth = 0.0!
        Me.DayList33.reverseSorting = False
        Me.DayList33.selected = -1
        Me.DayList33.selectedClickAllowed = True
        Me.DayList33.selectMultiple = False
        Me.DayList33.Size = New System.Drawing.Size(80, 72)
        Me.DayList33.sorted = False
        Me.DayList33.TabIndex = 164
        Me.DayList33.Tag = "NOHIDE"
        Me.DayList33.toolTipText = Nothing
        Me.DayList33.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList33.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList33.vScrolling = True
        Me.DayList33.vsValue = 0
        '
        'DayList34
        '
        Me.DayList34.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList34.autoAdjust = True
        Me.DayList34.autoKeyDownSelection = True
        Me.DayList34.autoSizeHorizontally = False
        Me.DayList34.autoSizeVertically = False
        Me.DayList34.BackColor = System.Drawing.Color.White
        Me.DayList34.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList34.baseBackColor = System.Drawing.Color.White
        Me.DayList34.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList34.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList34.bgColor = System.Drawing.Color.White
        Me.DayList34.borderColor = System.Drawing.Color.Empty
        Me.DayList34.borderSelColor = System.Drawing.Color.Empty
        Me.DayList34.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList34.CausesValidation = False
        Me.DayList34.clickEnabled = True
        Me.DayList34.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList34.do3D = False
        Me.DayList34.draw = False
        Me.DayList34.extraWidth = 0
        Me.DayList34.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList34.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList34.hScrolling = True
        Me.DayList34.hsValue = 0
        Me.DayList34.icons = CType(resources.GetObject("DayList34.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList34.itemBorder = 0
        Me.DayList34.itemMargin = 0
        Me.DayList34.items = CType(resources.GetObject("DayList34.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList34.Location = New System.Drawing.Point(186, 315)
        Me.DayList34.mouseMove3D = False
        Me.DayList34.mouseSpeed = 0
        Me.DayList34.Name = "DayList34"
        Me.DayList34.objMaxHeight = 0.0!
        Me.DayList34.objMaxWidth = 0.0!
        Me.DayList34.objMinHeight = 0.0!
        Me.DayList34.objMinWidth = 0.0!
        Me.DayList34.reverseSorting = False
        Me.DayList34.selected = -1
        Me.DayList34.selectedClickAllowed = True
        Me.DayList34.selectMultiple = False
        Me.DayList34.Size = New System.Drawing.Size(80, 72)
        Me.DayList34.sorted = False
        Me.DayList34.TabIndex = 165
        Me.DayList34.Tag = "NOHIDE"
        Me.DayList34.toolTipText = Nothing
        Me.DayList34.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList34.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList34.vScrolling = True
        Me.DayList34.vsValue = 0
        '
        'DayList35
        '
        Me.DayList35.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList35.autoAdjust = True
        Me.DayList35.autoKeyDownSelection = True
        Me.DayList35.autoSizeHorizontally = False
        Me.DayList35.autoSizeVertically = False
        Me.DayList35.BackColor = System.Drawing.Color.White
        Me.DayList35.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList35.baseBackColor = System.Drawing.Color.White
        Me.DayList35.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList35.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList35.bgColor = System.Drawing.Color.White
        Me.DayList35.borderColor = System.Drawing.Color.Empty
        Me.DayList35.borderSelColor = System.Drawing.Color.Empty
        Me.DayList35.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList35.CausesValidation = False
        Me.DayList35.clickEnabled = True
        Me.DayList35.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList35.do3D = False
        Me.DayList35.draw = False
        Me.DayList35.extraWidth = 0
        Me.DayList35.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList35.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList35.hScrolling = True
        Me.DayList35.hsValue = 0
        Me.DayList35.icons = CType(resources.GetObject("DayList35.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList35.itemBorder = 0
        Me.DayList35.itemMargin = 0
        Me.DayList35.items = CType(resources.GetObject("DayList35.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList35.Location = New System.Drawing.Point(194, 323)
        Me.DayList35.mouseMove3D = False
        Me.DayList35.mouseSpeed = 0
        Me.DayList35.Name = "DayList35"
        Me.DayList35.objMaxHeight = 0.0!
        Me.DayList35.objMaxWidth = 0.0!
        Me.DayList35.objMinHeight = 0.0!
        Me.DayList35.objMinWidth = 0.0!
        Me.DayList35.reverseSorting = False
        Me.DayList35.selected = -1
        Me.DayList35.selectedClickAllowed = True
        Me.DayList35.selectMultiple = False
        Me.DayList35.Size = New System.Drawing.Size(80, 64)
        Me.DayList35.sorted = False
        Me.DayList35.TabIndex = 166
        Me.DayList35.Tag = "NOHIDE"
        Me.DayList35.toolTipText = Nothing
        Me.DayList35.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList35.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList35.vScrolling = True
        Me.DayList35.vsValue = 0
        '
        'DayList36
        '
        Me.DayList36.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList36.autoAdjust = True
        Me.DayList36.autoKeyDownSelection = True
        Me.DayList36.autoSizeHorizontally = False
        Me.DayList36.autoSizeVertically = False
        Me.DayList36.BackColor = System.Drawing.Color.White
        Me.DayList36.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList36.baseBackColor = System.Drawing.Color.White
        Me.DayList36.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList36.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList36.bgColor = System.Drawing.Color.White
        Me.DayList36.borderColor = System.Drawing.Color.Empty
        Me.DayList36.borderSelColor = System.Drawing.Color.Empty
        Me.DayList36.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList36.CausesValidation = False
        Me.DayList36.clickEnabled = True
        Me.DayList36.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList36.do3D = False
        Me.DayList36.draw = False
        Me.DayList36.extraWidth = 0
        Me.DayList36.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList36.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList36.hScrolling = True
        Me.DayList36.hsValue = 0
        Me.DayList36.icons = CType(resources.GetObject("DayList36.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList36.itemBorder = 0
        Me.DayList36.itemMargin = 0
        Me.DayList36.items = CType(resources.GetObject("DayList36.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList36.Location = New System.Drawing.Point(186, 331)
        Me.DayList36.mouseMove3D = False
        Me.DayList36.mouseSpeed = 0
        Me.DayList36.Name = "DayList36"
        Me.DayList36.objMaxHeight = 0.0!
        Me.DayList36.objMaxWidth = 0.0!
        Me.DayList36.objMinHeight = 0.0!
        Me.DayList36.objMinWidth = 0.0!
        Me.DayList36.reverseSorting = False
        Me.DayList36.selected = -1
        Me.DayList36.selectedClickAllowed = True
        Me.DayList36.selectMultiple = False
        Me.DayList36.Size = New System.Drawing.Size(80, 64)
        Me.DayList36.sorted = False
        Me.DayList36.TabIndex = 167
        Me.DayList36.Tag = "NOHIDE"
        Me.DayList36.toolTipText = Nothing
        Me.DayList36.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList36.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList36.vScrolling = True
        Me.DayList36.vsValue = 0
        '
        'DayList37
        '
        Me.DayList37.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList37.autoAdjust = True
        Me.DayList37.autoKeyDownSelection = True
        Me.DayList37.autoSizeHorizontally = False
        Me.DayList37.autoSizeVertically = False
        Me.DayList37.BackColor = System.Drawing.Color.White
        Me.DayList37.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList37.baseBackColor = System.Drawing.Color.White
        Me.DayList37.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList37.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList37.bgColor = System.Drawing.Color.White
        Me.DayList37.borderColor = System.Drawing.Color.Empty
        Me.DayList37.borderSelColor = System.Drawing.Color.Empty
        Me.DayList37.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList37.CausesValidation = False
        Me.DayList37.clickEnabled = True
        Me.DayList37.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList37.do3D = False
        Me.DayList37.draw = False
        Me.DayList37.extraWidth = 0
        Me.DayList37.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList37.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList37.hScrolling = True
        Me.DayList37.hsValue = 0
        Me.DayList37.icons = CType(resources.GetObject("DayList37.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList37.itemBorder = 0
        Me.DayList37.itemMargin = 0
        Me.DayList37.items = CType(resources.GetObject("DayList37.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList37.Location = New System.Drawing.Point(186, 307)
        Me.DayList37.mouseMove3D = False
        Me.DayList37.mouseSpeed = 0
        Me.DayList37.Name = "DayList37"
        Me.DayList37.objMaxHeight = 0.0!
        Me.DayList37.objMaxWidth = 0.0!
        Me.DayList37.objMinHeight = 0.0!
        Me.DayList37.objMinWidth = 0.0!
        Me.DayList37.reverseSorting = False
        Me.DayList37.selected = -1
        Me.DayList37.selectedClickAllowed = True
        Me.DayList37.selectMultiple = False
        Me.DayList37.Size = New System.Drawing.Size(80, 72)
        Me.DayList37.sorted = False
        Me.DayList37.TabIndex = 168
        Me.DayList37.Tag = "NOHIDE"
        Me.DayList37.toolTipText = Nothing
        Me.DayList37.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList37.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList37.vScrolling = True
        Me.DayList37.vsValue = 0
        '
        'DayList38
        '
        Me.DayList38.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList38.autoAdjust = True
        Me.DayList38.autoKeyDownSelection = True
        Me.DayList38.autoSizeHorizontally = False
        Me.DayList38.autoSizeVertically = False
        Me.DayList38.BackColor = System.Drawing.Color.White
        Me.DayList38.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList38.baseBackColor = System.Drawing.Color.White
        Me.DayList38.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList38.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList38.bgColor = System.Drawing.Color.White
        Me.DayList38.borderColor = System.Drawing.Color.Empty
        Me.DayList38.borderSelColor = System.Drawing.Color.Empty
        Me.DayList38.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList38.CausesValidation = False
        Me.DayList38.clickEnabled = True
        Me.DayList38.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList38.do3D = False
        Me.DayList38.draw = False
        Me.DayList38.extraWidth = 0
        Me.DayList38.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList38.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList38.hScrolling = True
        Me.DayList38.hsValue = 0
        Me.DayList38.icons = CType(resources.GetObject("DayList38.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList38.itemBorder = 0
        Me.DayList38.itemMargin = 0
        Me.DayList38.items = CType(resources.GetObject("DayList38.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList38.Location = New System.Drawing.Point(194, 315)
        Me.DayList38.mouseMove3D = False
        Me.DayList38.mouseSpeed = 0
        Me.DayList38.Name = "DayList38"
        Me.DayList38.objMaxHeight = 0.0!
        Me.DayList38.objMaxWidth = 0.0!
        Me.DayList38.objMinHeight = 0.0!
        Me.DayList38.objMinWidth = 0.0!
        Me.DayList38.reverseSorting = False
        Me.DayList38.selected = -1
        Me.DayList38.selectedClickAllowed = True
        Me.DayList38.selectMultiple = False
        Me.DayList38.Size = New System.Drawing.Size(80, 72)
        Me.DayList38.sorted = False
        Me.DayList38.TabIndex = 169
        Me.DayList38.Tag = "NOHIDE"
        Me.DayList38.toolTipText = Nothing
        Me.DayList38.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList38.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList38.vScrolling = True
        Me.DayList38.vsValue = 0
        '
        'DayList39
        '
        Me.DayList39.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList39.autoAdjust = True
        Me.DayList39.autoKeyDownSelection = True
        Me.DayList39.autoSizeHorizontally = False
        Me.DayList39.autoSizeVertically = False
        Me.DayList39.BackColor = System.Drawing.Color.White
        Me.DayList39.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList39.baseBackColor = System.Drawing.Color.White
        Me.DayList39.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList39.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList39.bgColor = System.Drawing.Color.White
        Me.DayList39.borderColor = System.Drawing.Color.Empty
        Me.DayList39.borderSelColor = System.Drawing.Color.Empty
        Me.DayList39.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList39.CausesValidation = False
        Me.DayList39.clickEnabled = True
        Me.DayList39.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList39.do3D = False
        Me.DayList39.draw = False
        Me.DayList39.extraWidth = 0
        Me.DayList39.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList39.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList39.hScrolling = True
        Me.DayList39.hsValue = 0
        Me.DayList39.icons = CType(resources.GetObject("DayList39.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList39.itemBorder = 0
        Me.DayList39.itemMargin = 0
        Me.DayList39.items = CType(resources.GetObject("DayList39.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList39.Location = New System.Drawing.Point(202, 323)
        Me.DayList39.mouseMove3D = False
        Me.DayList39.mouseSpeed = 0
        Me.DayList39.Name = "DayList39"
        Me.DayList39.objMaxHeight = 0.0!
        Me.DayList39.objMaxWidth = 0.0!
        Me.DayList39.objMinHeight = 0.0!
        Me.DayList39.objMinWidth = 0.0!
        Me.DayList39.reverseSorting = False
        Me.DayList39.selected = -1
        Me.DayList39.selectedClickAllowed = True
        Me.DayList39.selectMultiple = False
        Me.DayList39.Size = New System.Drawing.Size(80, 64)
        Me.DayList39.sorted = False
        Me.DayList39.TabIndex = 170
        Me.DayList39.Tag = "NOHIDE"
        Me.DayList39.toolTipText = Nothing
        Me.DayList39.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList39.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList39.vScrolling = True
        Me.DayList39.vsValue = 0
        '
        'DayList40
        '
        Me.DayList40.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList40.autoAdjust = True
        Me.DayList40.autoKeyDownSelection = True
        Me.DayList40.autoSizeHorizontally = False
        Me.DayList40.autoSizeVertically = False
        Me.DayList40.BackColor = System.Drawing.Color.White
        Me.DayList40.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList40.baseBackColor = System.Drawing.Color.White
        Me.DayList40.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList40.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList40.bgColor = System.Drawing.Color.White
        Me.DayList40.borderColor = System.Drawing.Color.Empty
        Me.DayList40.borderSelColor = System.Drawing.Color.Empty
        Me.DayList40.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList40.CausesValidation = False
        Me.DayList40.clickEnabled = True
        Me.DayList40.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList40.do3D = False
        Me.DayList40.draw = False
        Me.DayList40.extraWidth = 0
        Me.DayList40.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList40.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList40.hScrolling = True
        Me.DayList40.hsValue = 0
        Me.DayList40.icons = CType(resources.GetObject("DayList40.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList40.itemBorder = 0
        Me.DayList40.itemMargin = 0
        Me.DayList40.items = CType(resources.GetObject("DayList40.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList40.Location = New System.Drawing.Point(186, 307)
        Me.DayList40.mouseMove3D = False
        Me.DayList40.mouseSpeed = 0
        Me.DayList40.Name = "DayList40"
        Me.DayList40.objMaxHeight = 0.0!
        Me.DayList40.objMaxWidth = 0.0!
        Me.DayList40.objMinHeight = 0.0!
        Me.DayList40.objMinWidth = 0.0!
        Me.DayList40.reverseSorting = False
        Me.DayList40.selected = -1
        Me.DayList40.selectedClickAllowed = True
        Me.DayList40.selectMultiple = False
        Me.DayList40.Size = New System.Drawing.Size(80, 72)
        Me.DayList40.sorted = False
        Me.DayList40.TabIndex = 171
        Me.DayList40.Tag = "NOHIDE"
        Me.DayList40.toolTipText = Nothing
        Me.DayList40.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList40.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList40.vScrolling = True
        Me.DayList40.vsValue = 0
        '
        'DayList41
        '
        Me.DayList41.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList41.autoAdjust = True
        Me.DayList41.autoKeyDownSelection = True
        Me.DayList41.autoSizeHorizontally = False
        Me.DayList41.autoSizeVertically = False
        Me.DayList41.BackColor = System.Drawing.Color.White
        Me.DayList41.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList41.baseBackColor = System.Drawing.Color.White
        Me.DayList41.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList41.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList41.bgColor = System.Drawing.Color.White
        Me.DayList41.borderColor = System.Drawing.Color.Empty
        Me.DayList41.borderSelColor = System.Drawing.Color.Empty
        Me.DayList41.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList41.CausesValidation = False
        Me.DayList41.clickEnabled = True
        Me.DayList41.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList41.do3D = False
        Me.DayList41.draw = False
        Me.DayList41.extraWidth = 0
        Me.DayList41.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList41.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList41.hScrolling = True
        Me.DayList41.hsValue = 0
        Me.DayList41.icons = CType(resources.GetObject("DayList41.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList41.itemBorder = 0
        Me.DayList41.itemMargin = 0
        Me.DayList41.items = CType(resources.GetObject("DayList41.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList41.Location = New System.Drawing.Point(194, 315)
        Me.DayList41.mouseMove3D = False
        Me.DayList41.mouseSpeed = 0
        Me.DayList41.Name = "DayList41"
        Me.DayList41.objMaxHeight = 0.0!
        Me.DayList41.objMaxWidth = 0.0!
        Me.DayList41.objMinHeight = 0.0!
        Me.DayList41.objMinWidth = 0.0!
        Me.DayList41.reverseSorting = False
        Me.DayList41.selected = -1
        Me.DayList41.selectedClickAllowed = True
        Me.DayList41.selectMultiple = False
        Me.DayList41.Size = New System.Drawing.Size(80, 72)
        Me.DayList41.sorted = False
        Me.DayList41.TabIndex = 172
        Me.DayList41.Tag = "NOHIDE"
        Me.DayList41.toolTipText = Nothing
        Me.DayList41.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList41.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList41.vScrolling = True
        Me.DayList41.vsValue = 0
        '
        'DayList1
        '
        Me.DayList1.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.DayList1.autoAdjust = True
        Me.DayList1.autoKeyDownSelection = True
        Me.DayList1.autoSizeHorizontally = False
        Me.DayList1.autoSizeVertically = False
        Me.DayList1.BackColor = System.Drawing.Color.White
        Me.DayList1.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.DayList1.baseBackColor = System.Drawing.Color.White
        Me.DayList1.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.DayList1.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList1.bgColor = System.Drawing.Color.White
        Me.DayList1.borderColor = System.Drawing.Color.Empty
        Me.DayList1.borderSelColor = System.Drawing.Color.Empty
        Me.DayList1.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.DayList1.CausesValidation = False
        Me.DayList1.clickEnabled = True
        Me.DayList1.defaultIconsPosition = CI.Controls.Icons.IconPositions.AfterText
        Me.DayList1.do3D = False
        Me.DayList1.draw = False
        Me.DayList1.extraWidth = 0
        Me.DayList1.hScrollColor = System.Drawing.SystemColors.Control
        Me.DayList1.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList1.hScrolling = True
        Me.DayList1.hsValue = 0
        Me.DayList1.icons = CType(resources.GetObject("DayList1.icons"), System.Collections.Generic.List(Of System.Drawing.Icon))
        Me.DayList1.itemBorder = 0
        Me.DayList1.itemMargin = 0
        Me.DayList1.items = CType(resources.GetObject("DayList1.items"), System.Collections.Generic.List(Of CI.Controls.IListItem))
        Me.DayList1.Location = New System.Drawing.Point(210, 315)
        Me.DayList1.mouseMove3D = False
        Me.DayList1.mouseSpeed = 0
        Me.DayList1.Name = "DayList1"
        Me.DayList1.objMaxHeight = 0.0!
        Me.DayList1.objMaxWidth = 0.0!
        Me.DayList1.objMinHeight = 0.0!
        Me.DayList1.objMinWidth = 0.0!
        Me.DayList1.reverseSorting = False
        Me.DayList1.selected = -1
        Me.DayList1.selectedClickAllowed = True
        Me.DayList1.selectMultiple = False
        Me.DayList1.Size = New System.Drawing.Size(80, 64)
        Me.DayList1.sorted = False
        Me.DayList1.TabIndex = 173
        Me.DayList1.Tag = "NOHIDE"
        Me.DayList1.toolTipText = Nothing
        Me.DayList1.vScrollColor = System.Drawing.SystemColors.Control
        Me.DayList1.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.DayList1.vScrolling = True
        Me.DayList1.vsValue = 0
        '
        'menuclickagenda
        '
        Me.menuclickagenda.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuContextMenu})
        Me.menuclickagenda.Location = New System.Drawing.Point(0, 0)
        Me.menuclickagenda.Name = "menuclickagenda"
        Me.menuclickagenda.Size = New System.Drawing.Size(242, 478)
        Me.menuclickagenda.TabIndex = 0
        '
        'menuContextMenu
        '
        Me.menuContextMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuaCouper, Me.menuacopier, Me.menuacoller, Me.menuaenlever, Me.menuModifyReserved, Me.menuSeparator, Me.menuAnnonceClient, Me.menuConfirmation, Me.menuSendEmail, Me.menuCloseHoraire, Me.menuQL, Me.menumodifstatus, Me.menunewrv, Me.menuScan, Me.menuopenaccount, Me.menupaiement, Me.menugeneraterecu, Me.menureserved, Me.menuRapports, Me.menuRVRemarques, Me.menuRVFutur, Me.menuOpenHoraire, Me.menuOpenHoraireUpto, Me.MenuItem2, Me.menuafonction})
        Me.menuContextMenu.Name = "menuContextMenu"
        Me.menuContextMenu.Size = New System.Drawing.Size(132, 474)
        Me.menuContextMenu.Text = "AgendaContextMenu"
        Me.menuContextMenu.Visible = False
        '
        'menuaCouper
        '
        Me.menuaCouper.MergeIndex = 0
        Me.menuaCouper.Name = "menuaCouper"
        Me.menuaCouper.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.menuaCouper.Size = New System.Drawing.Size(241, 22)
        Me.menuaCouper.Text = "Couper"
        '
        'menuacopier
        '
        Me.menuacopier.MergeIndex = 1
        Me.menuacopier.Name = "menuacopier"
        Me.menuacopier.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.menuacopier.Size = New System.Drawing.Size(241, 22)
        Me.menuacopier.Text = "Copier"
        '
        'menuacoller
        '
        Me.menuacoller.Enabled = False
        Me.menuacoller.MergeIndex = 2
        Me.menuacoller.Name = "menuacoller"
        Me.menuacoller.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.menuacoller.Size = New System.Drawing.Size(241, 22)
        Me.menuacoller.Text = "Coller"
        '
        'menuaenlever
        '
        Me.menuaenlever.MergeIndex = 3
        Me.menuaenlever.Name = "menuaenlever"
        Me.menuaenlever.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.menuaenlever.Size = New System.Drawing.Size(241, 22)
        Me.menuaenlever.Text = "Enlever"
        '
        'menuModifyReserved
        '
        Me.menuModifyReserved.MergeIndex = 4
        Me.menuModifyReserved.Name = "menuModifyReserved"
        Me.menuModifyReserved.Size = New System.Drawing.Size(241, 22)
        Me.menuModifyReserved.Text = "Modifier"
        '
        'menuSeparator
        '
        Me.menuSeparator.MergeIndex = 5
        Me.menuSeparator.Name = "menuSeparator"
        Me.menuSeparator.Size = New System.Drawing.Size(238, 6)
        '
        'menuAnnonceClient
        '
        Me.menuAnnonceClient.MergeIndex = 6
        Me.menuAnnonceClient.Name = "menuAnnonceClient"
        Me.menuAnnonceClient.Size = New System.Drawing.Size(241, 22)
        Me.menuAnnonceClient.Text = "Annoncer l'arrivée du client"
        '
        'menuConfirmation
        '
        Me.menuConfirmation.MergeIndex = 7
        Me.menuConfirmation.Name = "menuConfirmation"
        Me.menuConfirmation.Size = New System.Drawing.Size(241, 22)
        Me.menuConfirmation.Text = "Confirmer le rendez-vous"
        '
        'menuSendEmail
        '
        Me.menuSendEmail.MergeIndex = 8
        Me.menuSendEmail.Name = "menuSendEmail"
        Me.menuSendEmail.Size = New System.Drawing.Size(241, 22)
        Me.menuSendEmail.Text = "Envoyer un courriel"
        '
        'menuCloseHoraire
        '
        Me.menuCloseHoraire.MergeIndex = 9
        Me.menuCloseHoraire.Name = "menuCloseHoraire"
        Me.menuCloseHoraire.Size = New System.Drawing.Size(241, 22)
        Me.menuCloseHoraire.Text = "Fermer la plage horaire"
        '
        'menuQL
        '
        Me.menuQL.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuAddToQueueList, Me.menuContinuerLaListeDattente, Me.menuStartQL, Me.menuQueueList})
        Me.menuQL.MergeIndex = 10
        Me.menuQL.Name = "menuQL"
        Me.menuQL.Size = New System.Drawing.Size(241, 22)
        Me.menuQL.Text = "Liste d'attente"
        '
        'menuAddToQueueList
        '
        Me.menuAddToQueueList.MergeIndex = 0
        Me.menuAddToQueueList.Name = "menuAddToQueueList"
        Me.menuAddToQueueList.Size = New System.Drawing.Size(348, 22)
        Me.menuAddToQueueList.Text = "Ajouter à la liste d'attente"
        '
        'menuContinuerLaListeDattente
        '
        Me.menuContinuerLaListeDattente.Name = "menuContinuerLaListeDattente"
        Me.menuContinuerLaListeDattente.Size = New System.Drawing.Size(348, 22)
        Me.menuContinuerLaListeDattente.Text = "Sélectionner un rendez-vous depuis la liste d'attente"
        '
        'menuStartQL
        '
        Me.menuStartQL.Name = "menuStartQL"
        Me.menuStartQL.Size = New System.Drawing.Size(348, 22)
        Me.menuStartQL.Text = "Sélectionner un rendez-vous depuis la liste d'attente"
        '
        'menuQueueList
        '
        Me.menuQueueList.MergeIndex = 1
        Me.menuQueueList.Name = "menuQueueList"
        Me.menuQueueList.Size = New System.Drawing.Size(348, 22)
        Me.menuQueueList.Text = "Voir la liste d'attente"
        '
        'menumodifstatus
        '
        Me.menumodifstatus.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menupresent, Me.menuabsentmotive, Me.menuabsentnonmotive, Me.menueffstatus, Me.MenuItem4, Me.menuOpenFolder, Me.menuCloseFolder})
        Me.menumodifstatus.MergeIndex = 11
        Me.menumodifstatus.Name = "menumodifstatus"
        Me.menumodifstatus.Size = New System.Drawing.Size(241, 22)
        Me.menumodifstatus.Text = "Modifier le statut..."
        '
        'menupresent
        '
        Me.menupresent.MergeIndex = 0
        Me.menupresent.Name = "menupresent"
        Me.menupresent.Size = New System.Drawing.Size(187, 22)
        Me.menupresent.Text = "Présent"
        '
        'menuabsentmotive
        '
        Me.menuabsentmotive.MergeIndex = 1
        Me.menuabsentmotive.Name = "menuabsentmotive"
        Me.menuabsentmotive.Size = New System.Drawing.Size(187, 22)
        Me.menuabsentmotive.Text = "Absent - Motivé"
        '
        'menuabsentnonmotive
        '
        Me.menuabsentnonmotive.MergeIndex = 2
        Me.menuabsentnonmotive.Name = "menuabsentnonmotive"
        Me.menuabsentnonmotive.Size = New System.Drawing.Size(187, 22)
        Me.menuabsentnonmotive.Text = "Absent - Non-motivé"
        '
        'menueffstatus
        '
        Me.menueffstatus.MergeIndex = 3
        Me.menueffstatus.Name = "menueffstatus"
        Me.menueffstatus.Size = New System.Drawing.Size(187, 22)
        Me.menueffstatus.Text = "Effacer"
        '
        'MenuItem4
        '
        Me.MenuItem4.MergeIndex = 4
        Me.MenuItem4.Name = "MenuItem4"
        Me.MenuItem4.Size = New System.Drawing.Size(184, 6)
        '
        'menuOpenFolder
        '
        Me.menuOpenFolder.MergeIndex = 5
        Me.menuOpenFolder.Name = "menuOpenFolder"
        Me.menuOpenFolder.Size = New System.Drawing.Size(187, 22)
        Me.menuOpenFolder.Text = "Activer le dossier"
        '
        'menuCloseFolder
        '
        Me.menuCloseFolder.MergeIndex = 6
        Me.menuCloseFolder.Name = "menuCloseFolder"
        Me.menuCloseFolder.Size = New System.Drawing.Size(187, 22)
        Me.menuCloseFolder.Text = "Désactiver le dossier"
        '
        'menunewrv
        '
        Me.menunewrv.MergeIndex = 12
        Me.menunewrv.Name = "menunewrv"
        Me.menunewrv.Size = New System.Drawing.Size(241, 22)
        Me.menunewrv.Text = "Nouveau rendez-vous"
        '
        'menuScan
        '
        Me.menuScan.MergeIndex = 13
        Me.menuScan.Name = "menuScan"
        Me.menuScan.Size = New System.Drawing.Size(241, 22)
        Me.menuScan.Text = "Numériser une photo"
        Me.menuScan.Visible = False
        '
        'menuopenaccount
        '
        Me.menuopenaccount.MergeIndex = 14
        Me.menuopenaccount.Name = "menuopenaccount"
        Me.menuopenaccount.Size = New System.Drawing.Size(241, 22)
        Me.menuopenaccount.Text = "Ouvrir le compte"
        '
        'menupaiement
        '
        Me.menupaiement.MergeIndex = 15
        Me.menupaiement.Name = "menupaiement"
        Me.menupaiement.Size = New System.Drawing.Size(241, 22)
        Me.menupaiement.Text = "Paiement"
        '
        'menugeneraterecu
        '
        Me.menugeneraterecu.Name = "menugeneraterecu"
        Me.menugeneraterecu.Size = New System.Drawing.Size(241, 22)
        Me.menugeneraterecu.Text = "Imprimer un reçu"
        '
        'menureserved
        '
        Me.menureserved.MergeIndex = 16
        Me.menureserved.Name = "menureserved"
        Me.menureserved.Size = New System.Drawing.Size(241, 22)
        Me.menureserved.Text = "Plage réservée"
        '
        'menuRapports
        '
        Me.menuRapports.MergeIndex = 17
        Me.menuRapports.Name = "menuRapports"
        Me.menuRapports.Size = New System.Drawing.Size(241, 22)
        Me.menuRapports.Text = "Rapports personnalisés"
        '
        'menuRVRemarques
        '
        Me.menuRVRemarques.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuChangeRVRemarks, Me.menuDeleteRVRemarks})
        Me.menuRVRemarques.Name = "menuRVRemarques"
        Me.menuRVRemarques.Size = New System.Drawing.Size(241, 22)
        Me.menuRVRemarques.Text = "Remarque du rendez-vous"
        '
        'menuChangeRVRemarks
        '
        Me.menuChangeRVRemarks.Name = "menuChangeRVRemarks"
        Me.menuChangeRVRemarks.Size = New System.Drawing.Size(195, 22)
        Me.menuChangeRVRemarks.Text = "Modifier la remarque"
        '
        'menuDeleteRVRemarks
        '
        Me.menuDeleteRVRemarks.Name = "menuDeleteRVRemarks"
        Me.menuDeleteRVRemarks.Size = New System.Drawing.Size(195, 22)
        Me.menuDeleteRVRemarks.Text = "Supprimer la remarque"
        '
        'menuRVFutur
        '
        Me.menuRVFutur.MergeIndex = 18
        Me.menuRVFutur.Name = "menuRVFutur"
        Me.menuRVFutur.Size = New System.Drawing.Size(241, 22)
        Me.menuRVFutur.Text = "Rendez-vous futur(s)"
        '
        'menuOpenHoraire
        '
        Me.menuOpenHoraire.MergeIndex = 19
        Me.menuOpenHoraire.Name = "menuOpenHoraire"
        Me.menuOpenHoraire.Size = New System.Drawing.Size(241, 22)
        Me.menuOpenHoraire.Text = "Ouvrir la plage horaire"
        '
        'menuOpenHoraireUpto
        '
        Me.menuOpenHoraireUpto.MergeIndex = 20
        Me.menuOpenHoraireUpto.Name = "menuOpenHoraireUpto"
        Me.menuOpenHoraireUpto.Size = New System.Drawing.Size(241, 22)
        Me.menuOpenHoraireUpto.Text = "Ouvrir la plage horaire jusqu'à..."
        '
        'MenuItem2
        '
        Me.MenuItem2.MergeIndex = 21
        Me.MenuItem2.Name = "MenuItem2"
        Me.MenuItem2.Size = New System.Drawing.Size(238, 6)
        '
        'menuafonction
        '
        Me.menuafonction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuAddToKeyPeople, Me.menuPrintAgenda, Me.menumodifhorairetrp, Me.menuModifHoraireClinique, Me.menuPreferences, Me.MenuItem1, Me.menuchangedate, Me.menuperiode})
        Me.menuafonction.MergeIndex = 22
        Me.menuafonction.Name = "menuafonction"
        Me.menuafonction.Size = New System.Drawing.Size(241, 22)
        Me.menuafonction.Text = "Autres fonctions"
        '
        'menuAddToKeyPeople
        '
        Me.menuAddToKeyPeople.MergeIndex = 0
        Me.menuAddToKeyPeople.Name = "menuAddToKeyPeople"
        Me.menuAddToKeyPeople.Size = New System.Drawing.Size(295, 22)
        Me.menuAddToKeyPeople.Text = "Ajouter comme personne clé"
        '
        'menuPrintAgenda
        '
        Me.menuPrintAgenda.MergeIndex = 1
        Me.menuPrintAgenda.Name = "menuPrintAgenda"
        Me.menuPrintAgenda.Size = New System.Drawing.Size(295, 22)
        Me.menuPrintAgenda.Text = "Générer le rapport de la semaine en cours"
        '
        'menumodifhorairetrp
        '
        Me.menumodifhorairetrp.MergeIndex = 2
        Me.menumodifhorairetrp.Name = "menumodifhorairetrp"
        Me.menumodifhorairetrp.Size = New System.Drawing.Size(295, 22)
        Me.menumodifhorairetrp.Text = "Modification de l'horaire de ce thérapeute"
        '
        'menuModifHoraireClinique
        '
        Me.menuModifHoraireClinique.MergeIndex = 3
        Me.menuModifHoraireClinique.Name = "menuModifHoraireClinique"
        Me.menuModifHoraireClinique.Size = New System.Drawing.Size(295, 22)
        Me.menuModifHoraireClinique.Text = "Modification de l'horaire de la clinique"
        '
        'menuPreferences
        '
        Me.menuPreferences.MergeIndex = 4
        Me.menuPreferences.Name = "menuPreferences"
        Me.menuPreferences.Size = New System.Drawing.Size(295, 22)
        Me.menuPreferences.Text = "Préférences"
        '
        'MenuItem1
        '
        Me.MenuItem1.MergeIndex = 5
        Me.MenuItem1.Name = "MenuItem1"
        Me.MenuItem1.Size = New System.Drawing.Size(292, 6)
        '
        'menuchangedate
        '
        Me.menuchangedate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menupdateday, Me.menuadateday, Me.menupdateweek, Me.menuadateweek, Me.menupdatemonth, Me.menuadatemonth})
        Me.menuchangedate.MergeIndex = 6
        Me.menuchangedate.Name = "menuchangedate"
        Me.menuchangedate.Size = New System.Drawing.Size(295, 22)
        Me.menuchangedate.Text = "Changer la date"
        '
        'menupdateday
        '
        Me.menupdateday.MergeIndex = 0
        Me.menupdateday.Name = "menupdateday"
        Me.menupdateday.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.menupdateday.Size = New System.Drawing.Size(223, 22)
        Me.menupdateday.Text = "Une journée précédente"
        '
        'menuadateday
        '
        Me.menuadateday.MergeIndex = 1
        Me.menuadateday.Name = "menuadateday"
        Me.menuadateday.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.menuadateday.Size = New System.Drawing.Size(223, 22)
        Me.menuadateday.Text = "Une journée suivante"
        '
        'menupdateweek
        '
        Me.menupdateweek.MergeIndex = 2
        Me.menupdateweek.Name = "menupdateweek"
        Me.menupdateweek.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.menupdateweek.Size = New System.Drawing.Size(223, 22)
        Me.menupdateweek.Text = "Une semaine précédente"
        '
        'menuadateweek
        '
        Me.menuadateweek.MergeIndex = 3
        Me.menuadateweek.Name = "menuadateweek"
        Me.menuadateweek.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.menuadateweek.Size = New System.Drawing.Size(223, 22)
        Me.menuadateweek.Text = "Une semaine suivante"
        '
        'menupdatemonth
        '
        Me.menupdatemonth.MergeIndex = 4
        Me.menupdatemonth.Name = "menupdatemonth"
        Me.menupdatemonth.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.menupdatemonth.Size = New System.Drawing.Size(223, 22)
        Me.menupdatemonth.Text = "Un mois précédent"
        '
        'menuadatemonth
        '
        Me.menuadatemonth.MergeIndex = 5
        Me.menuadatemonth.Name = "menuadatemonth"
        Me.menuadatemonth.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.menuadatemonth.Size = New System.Drawing.Size(223, 22)
        Me.menuadatemonth.Text = "Un mois suivant"
        '
        'menuperiode
        '
        Me.menuperiode.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menujourup, Me.menusemaineup, Me.menumois})
        Me.menuperiode.MergeIndex = 7
        Me.menuperiode.Name = "menuperiode"
        Me.menuperiode.Size = New System.Drawing.Size(295, 22)
        Me.menuperiode.Text = "Période"
        '
        'menujourup
        '
        Me.menujourup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._menujour_1, Me._menujour_2, Me._menujour_3, Me._menujour_4, Me._menujour_5, Me._menujour_6})
        Me.menujourup.MergeIndex = 0
        Me.menujourup.Name = "menujourup"
        Me.menujourup.Size = New System.Drawing.Size(119, 22)
        Me.menujourup.Text = "Jour"
        '
        '_menujour_1
        '
        Me._menujour_1.MergeIndex = 0
        Me._menujour_1.Name = "_menujour_1"
        Me._menujour_1.Size = New System.Drawing.Size(128, 22)
        Me._menujour_1.Text = "1 journée"
        '
        '_menujour_2
        '
        Me._menujour_2.MergeIndex = 1
        Me._menujour_2.Name = "_menujour_2"
        Me._menujour_2.Size = New System.Drawing.Size(128, 22)
        Me._menujour_2.Text = "2 journées"
        '
        '_menujour_3
        '
        Me._menujour_3.MergeIndex = 2
        Me._menujour_3.Name = "_menujour_3"
        Me._menujour_3.Size = New System.Drawing.Size(128, 22)
        Me._menujour_3.Text = "3 journées"
        '
        '_menujour_4
        '
        Me._menujour_4.MergeIndex = 3
        Me._menujour_4.Name = "_menujour_4"
        Me._menujour_4.Size = New System.Drawing.Size(128, 22)
        Me._menujour_4.Text = "4 journées"
        '
        '_menujour_5
        '
        Me._menujour_5.Checked = True
        Me._menujour_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._menujour_5.MergeIndex = 4
        Me._menujour_5.Name = "_menujour_5"
        Me._menujour_5.Size = New System.Drawing.Size(128, 22)
        Me._menujour_5.Text = "5 journées"
        '
        '_menujour_6
        '
        Me._menujour_6.MergeIndex = 5
        Me._menujour_6.Name = "_menujour_6"
        Me._menujour_6.Size = New System.Drawing.Size(128, 22)
        Me._menujour_6.Text = "6 journées"
        '
        'menusemaineup
        '
        Me.menusemaineup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._menusemaine_1, Me._menusemaine_2, Me._menusemaine_3, Me._menusemaine_4})
        Me.menusemaineup.MergeIndex = 1
        Me.menusemaineup.Name = "menusemaineup"
        Me.menusemaineup.Size = New System.Drawing.Size(119, 22)
        Me.menusemaineup.Text = "Semaine"
        '
        '_menusemaine_1
        '
        Me._menusemaine_1.MergeIndex = 0
        Me._menusemaine_1.Name = "_menusemaine_1"
        Me._menusemaine_1.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_1.Text = "1 semaine"
        '
        '_menusemaine_2
        '
        Me._menusemaine_2.MergeIndex = 1
        Me._menusemaine_2.Name = "_menusemaine_2"
        Me._menusemaine_2.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_2.Text = "2 semaines"
        '
        '_menusemaine_3
        '
        Me._menusemaine_3.MergeIndex = 2
        Me._menusemaine_3.Name = "_menusemaine_3"
        Me._menusemaine_3.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_3.Text = "3 semaines"
        '
        '_menusemaine_4
        '
        Me._menusemaine_4.MergeIndex = 3
        Me._menusemaine_4.Name = "_menusemaine_4"
        Me._menusemaine_4.Size = New System.Drawing.Size(132, 22)
        Me._menusemaine_4.Text = "4 semaines"
        '
        'menumois
        '
        Me.menumois.MergeIndex = 2
        Me.menumois.Name = "menumois"
        Me.menumois.Size = New System.Drawing.Size(119, 22)
        Me.menumois.Text = "Mois"
        '
        'tl
        '
        Me.tl.AutoSize = True
        Me.tl.BackColor = System.Drawing.SystemColors.Control
        Me.tl.Cursor = System.Windows.Forms.Cursors.Default
        Me.tl.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tl.Location = New System.Drawing.Point(0, 0)
        Me.tl.Name = "tl"
        Me.tl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tl.Size = New System.Drawing.Size(0, 14)
        Me.tl.TabIndex = 174
        Me.tl.Visible = False
        '
        'Feux1
        '
        Me.Feux1.colorClinique = System.Drawing.Color.Empty
        Me.Feux1.colorLibre = System.Drawing.Color.Empty
        Me.Feux1.colorPresence = System.Drawing.Color.Empty
        Me.Feux1.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux1.colorRV = System.Drawing.Color.Empty
        Me.Feux1.Location = New System.Drawing.Point(40, 96)
        Me.Feux1.Name = "Feux1"
        Me.Feux1.Size = New System.Drawing.Size(25, 16)
        Me.Feux1.TabIndex = 177
        Me.Feux1.Visible = False
        '
        'Feux2
        '
        Me.Feux2.colorClinique = System.Drawing.Color.Empty
        Me.Feux2.colorLibre = System.Drawing.Color.Empty
        Me.Feux2.colorPresence = System.Drawing.Color.Empty
        Me.Feux2.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux2.colorRV = System.Drawing.Color.Empty
        Me.Feux2.Location = New System.Drawing.Point(24, 56)
        Me.Feux2.Name = "Feux2"
        Me.Feux2.Size = New System.Drawing.Size(25, 16)
        Me.Feux2.TabIndex = 178
        Me.Feux2.Visible = False
        '
        'Feux3
        '
        Me.Feux3.colorClinique = System.Drawing.Color.Empty
        Me.Feux3.colorLibre = System.Drawing.Color.Empty
        Me.Feux3.colorPresence = System.Drawing.Color.Empty
        Me.Feux3.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux3.colorRV = System.Drawing.Color.Empty
        Me.Feux3.Location = New System.Drawing.Point(32, 64)
        Me.Feux3.Name = "Feux3"
        Me.Feux3.Size = New System.Drawing.Size(25, 16)
        Me.Feux3.TabIndex = 179
        Me.Feux3.Visible = False
        '
        'Feux4
        '
        Me.Feux4.colorClinique = System.Drawing.Color.Empty
        Me.Feux4.colorLibre = System.Drawing.Color.Empty
        Me.Feux4.colorPresence = System.Drawing.Color.Empty
        Me.Feux4.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux4.colorRV = System.Drawing.Color.Empty
        Me.Feux4.Location = New System.Drawing.Point(40, 72)
        Me.Feux4.Name = "Feux4"
        Me.Feux4.Size = New System.Drawing.Size(25, 16)
        Me.Feux4.TabIndex = 180
        Me.Feux4.Visible = False
        '
        'Feux5
        '
        Me.Feux5.colorClinique = System.Drawing.Color.Empty
        Me.Feux5.colorLibre = System.Drawing.Color.Empty
        Me.Feux5.colorPresence = System.Drawing.Color.Empty
        Me.Feux5.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux5.colorRV = System.Drawing.Color.Empty
        Me.Feux5.Location = New System.Drawing.Point(48, 80)
        Me.Feux5.Name = "Feux5"
        Me.Feux5.Size = New System.Drawing.Size(25, 16)
        Me.Feux5.TabIndex = 181
        Me.Feux5.Visible = False
        '
        'Feux6
        '
        Me.Feux6.colorClinique = System.Drawing.Color.Empty
        Me.Feux6.colorLibre = System.Drawing.Color.Empty
        Me.Feux6.colorPresence = System.Drawing.Color.Empty
        Me.Feux6.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux6.colorRV = System.Drawing.Color.Empty
        Me.Feux6.Location = New System.Drawing.Point(56, 88)
        Me.Feux6.Name = "Feux6"
        Me.Feux6.Size = New System.Drawing.Size(25, 16)
        Me.Feux6.TabIndex = 182
        Me.Feux6.Visible = False
        '
        'Feux7
        '
        Me.Feux7.colorClinique = System.Drawing.Color.Empty
        Me.Feux7.colorLibre = System.Drawing.Color.Empty
        Me.Feux7.colorPresence = System.Drawing.Color.Empty
        Me.Feux7.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux7.colorRV = System.Drawing.Color.Empty
        Me.Feux7.Location = New System.Drawing.Point(16, 56)
        Me.Feux7.Name = "Feux7"
        Me.Feux7.Size = New System.Drawing.Size(25, 16)
        Me.Feux7.TabIndex = 183
        Me.Feux7.Visible = False
        '
        'Feux8
        '
        Me.Feux8.colorClinique = System.Drawing.Color.Empty
        Me.Feux8.colorLibre = System.Drawing.Color.Empty
        Me.Feux8.colorPresence = System.Drawing.Color.Empty
        Me.Feux8.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux8.colorRV = System.Drawing.Color.Empty
        Me.Feux8.Location = New System.Drawing.Point(24, 64)
        Me.Feux8.Name = "Feux8"
        Me.Feux8.Size = New System.Drawing.Size(25, 16)
        Me.Feux8.TabIndex = 184
        Me.Feux8.Visible = False
        '
        'Feux9
        '
        Me.Feux9.colorClinique = System.Drawing.Color.Empty
        Me.Feux9.colorLibre = System.Drawing.Color.Empty
        Me.Feux9.colorPresence = System.Drawing.Color.Empty
        Me.Feux9.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux9.colorRV = System.Drawing.Color.Empty
        Me.Feux9.Location = New System.Drawing.Point(32, 72)
        Me.Feux9.Name = "Feux9"
        Me.Feux9.Size = New System.Drawing.Size(25, 16)
        Me.Feux9.TabIndex = 185
        Me.Feux9.Visible = False
        '
        'Feux10
        '
        Me.Feux10.colorClinique = System.Drawing.Color.Empty
        Me.Feux10.colorLibre = System.Drawing.Color.Empty
        Me.Feux10.colorPresence = System.Drawing.Color.Empty
        Me.Feux10.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux10.colorRV = System.Drawing.Color.Empty
        Me.Feux10.Location = New System.Drawing.Point(40, 80)
        Me.Feux10.Name = "Feux10"
        Me.Feux10.Size = New System.Drawing.Size(25, 16)
        Me.Feux10.TabIndex = 186
        Me.Feux10.Visible = False
        '
        'Feux11
        '
        Me.Feux11.colorClinique = System.Drawing.Color.Empty
        Me.Feux11.colorLibre = System.Drawing.Color.Empty
        Me.Feux11.colorPresence = System.Drawing.Color.Empty
        Me.Feux11.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux11.colorRV = System.Drawing.Color.Empty
        Me.Feux11.Location = New System.Drawing.Point(48, 88)
        Me.Feux11.Name = "Feux11"
        Me.Feux11.Size = New System.Drawing.Size(25, 16)
        Me.Feux11.TabIndex = 187
        Me.Feux11.Visible = False
        '
        'Feux12
        '
        Me.Feux12.colorClinique = System.Drawing.Color.Empty
        Me.Feux12.colorLibre = System.Drawing.Color.Empty
        Me.Feux12.colorPresence = System.Drawing.Color.Empty
        Me.Feux12.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux12.colorRV = System.Drawing.Color.Empty
        Me.Feux12.Location = New System.Drawing.Point(56, 96)
        Me.Feux12.Name = "Feux12"
        Me.Feux12.Size = New System.Drawing.Size(25, 16)
        Me.Feux12.TabIndex = 188
        Me.Feux12.Visible = False
        '
        'Feux13
        '
        Me.Feux13.colorClinique = System.Drawing.Color.Empty
        Me.Feux13.colorLibre = System.Drawing.Color.Empty
        Me.Feux13.colorPresence = System.Drawing.Color.Empty
        Me.Feux13.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux13.colorRV = System.Drawing.Color.Empty
        Me.Feux13.Location = New System.Drawing.Point(64, 104)
        Me.Feux13.Name = "Feux13"
        Me.Feux13.Size = New System.Drawing.Size(25, 16)
        Me.Feux13.TabIndex = 189
        Me.Feux13.Visible = False
        '
        'Feux14
        '
        Me.Feux14.colorClinique = System.Drawing.Color.Empty
        Me.Feux14.colorLibre = System.Drawing.Color.Empty
        Me.Feux14.colorPresence = System.Drawing.Color.Empty
        Me.Feux14.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux14.colorRV = System.Drawing.Color.Empty
        Me.Feux14.Location = New System.Drawing.Point(72, 112)
        Me.Feux14.Name = "Feux14"
        Me.Feux14.Size = New System.Drawing.Size(25, 16)
        Me.Feux14.TabIndex = 190
        Me.Feux14.Visible = False
        '
        'Feux15
        '
        Me.Feux15.colorClinique = System.Drawing.Color.Empty
        Me.Feux15.colorLibre = System.Drawing.Color.Empty
        Me.Feux15.colorPresence = System.Drawing.Color.Empty
        Me.Feux15.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux15.colorRV = System.Drawing.Color.Empty
        Me.Feux15.Location = New System.Drawing.Point(80, 120)
        Me.Feux15.Name = "Feux15"
        Me.Feux15.Size = New System.Drawing.Size(25, 16)
        Me.Feux15.TabIndex = 191
        Me.Feux15.Visible = False
        '
        'Feux16
        '
        Me.Feux16.colorClinique = System.Drawing.Color.Empty
        Me.Feux16.colorLibre = System.Drawing.Color.Empty
        Me.Feux16.colorPresence = System.Drawing.Color.Empty
        Me.Feux16.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux16.colorRV = System.Drawing.Color.Empty
        Me.Feux16.Location = New System.Drawing.Point(88, 128)
        Me.Feux16.Name = "Feux16"
        Me.Feux16.Size = New System.Drawing.Size(25, 16)
        Me.Feux16.TabIndex = 192
        Me.Feux16.Visible = False
        '
        'Feux17
        '
        Me.Feux17.colorClinique = System.Drawing.Color.Empty
        Me.Feux17.colorLibre = System.Drawing.Color.Empty
        Me.Feux17.colorPresence = System.Drawing.Color.Empty
        Me.Feux17.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux17.colorRV = System.Drawing.Color.Empty
        Me.Feux17.Location = New System.Drawing.Point(96, 136)
        Me.Feux17.Name = "Feux17"
        Me.Feux17.Size = New System.Drawing.Size(25, 16)
        Me.Feux17.TabIndex = 193
        Me.Feux17.Visible = False
        '
        'Feux18
        '
        Me.Feux18.colorClinique = System.Drawing.Color.Empty
        Me.Feux18.colorLibre = System.Drawing.Color.Empty
        Me.Feux18.colorPresence = System.Drawing.Color.Empty
        Me.Feux18.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux18.colorRV = System.Drawing.Color.Empty
        Me.Feux18.Location = New System.Drawing.Point(24, 56)
        Me.Feux18.Name = "Feux18"
        Me.Feux18.Size = New System.Drawing.Size(25, 16)
        Me.Feux18.TabIndex = 194
        Me.Feux18.Visible = False
        '
        'Feux19
        '
        Me.Feux19.colorClinique = System.Drawing.Color.Empty
        Me.Feux19.colorLibre = System.Drawing.Color.Empty
        Me.Feux19.colorPresence = System.Drawing.Color.Empty
        Me.Feux19.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux19.colorRV = System.Drawing.Color.Empty
        Me.Feux19.Location = New System.Drawing.Point(32, 64)
        Me.Feux19.Name = "Feux19"
        Me.Feux19.Size = New System.Drawing.Size(25, 16)
        Me.Feux19.TabIndex = 195
        Me.Feux19.Visible = False
        '
        'Feux20
        '
        Me.Feux20.colorClinique = System.Drawing.Color.Empty
        Me.Feux20.colorLibre = System.Drawing.Color.Empty
        Me.Feux20.colorPresence = System.Drawing.Color.Empty
        Me.Feux20.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux20.colorRV = System.Drawing.Color.Empty
        Me.Feux20.Location = New System.Drawing.Point(40, 72)
        Me.Feux20.Name = "Feux20"
        Me.Feux20.Size = New System.Drawing.Size(25, 16)
        Me.Feux20.TabIndex = 196
        Me.Feux20.Visible = False
        '
        'Feux21
        '
        Me.Feux21.colorClinique = System.Drawing.Color.Empty
        Me.Feux21.colorLibre = System.Drawing.Color.Empty
        Me.Feux21.colorPresence = System.Drawing.Color.Empty
        Me.Feux21.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux21.colorRV = System.Drawing.Color.Empty
        Me.Feux21.Location = New System.Drawing.Point(48, 80)
        Me.Feux21.Name = "Feux21"
        Me.Feux21.Size = New System.Drawing.Size(25, 16)
        Me.Feux21.TabIndex = 197
        Me.Feux21.Visible = False
        '
        'Feux22
        '
        Me.Feux22.colorClinique = System.Drawing.Color.Empty
        Me.Feux22.colorLibre = System.Drawing.Color.Empty
        Me.Feux22.colorPresence = System.Drawing.Color.Empty
        Me.Feux22.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux22.colorRV = System.Drawing.Color.Empty
        Me.Feux22.Location = New System.Drawing.Point(56, 88)
        Me.Feux22.Name = "Feux22"
        Me.Feux22.Size = New System.Drawing.Size(25, 16)
        Me.Feux22.TabIndex = 198
        Me.Feux22.Visible = False
        '
        'Feux23
        '
        Me.Feux23.colorClinique = System.Drawing.Color.Empty
        Me.Feux23.colorLibre = System.Drawing.Color.Empty
        Me.Feux23.colorPresence = System.Drawing.Color.Empty
        Me.Feux23.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux23.colorRV = System.Drawing.Color.Empty
        Me.Feux23.Location = New System.Drawing.Point(16, 56)
        Me.Feux23.Name = "Feux23"
        Me.Feux23.Size = New System.Drawing.Size(25, 16)
        Me.Feux23.TabIndex = 199
        Me.Feux23.Visible = False
        '
        'Feux24
        '
        Me.Feux24.colorClinique = System.Drawing.Color.Empty
        Me.Feux24.colorLibre = System.Drawing.Color.Empty
        Me.Feux24.colorPresence = System.Drawing.Color.Empty
        Me.Feux24.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux24.colorRV = System.Drawing.Color.Empty
        Me.Feux24.Location = New System.Drawing.Point(24, 64)
        Me.Feux24.Name = "Feux24"
        Me.Feux24.Size = New System.Drawing.Size(25, 16)
        Me.Feux24.TabIndex = 200
        Me.Feux24.Visible = False
        '
        'Feux25
        '
        Me.Feux25.colorClinique = System.Drawing.Color.Empty
        Me.Feux25.colorLibre = System.Drawing.Color.Empty
        Me.Feux25.colorPresence = System.Drawing.Color.Empty
        Me.Feux25.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux25.colorRV = System.Drawing.Color.Empty
        Me.Feux25.Location = New System.Drawing.Point(32, 72)
        Me.Feux25.Name = "Feux25"
        Me.Feux25.Size = New System.Drawing.Size(25, 16)
        Me.Feux25.TabIndex = 201
        Me.Feux25.Visible = False
        '
        'Feux26
        '
        Me.Feux26.colorClinique = System.Drawing.Color.Empty
        Me.Feux26.colorLibre = System.Drawing.Color.Empty
        Me.Feux26.colorPresence = System.Drawing.Color.Empty
        Me.Feux26.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux26.colorRV = System.Drawing.Color.Empty
        Me.Feux26.Location = New System.Drawing.Point(40, 80)
        Me.Feux26.Name = "Feux26"
        Me.Feux26.Size = New System.Drawing.Size(25, 16)
        Me.Feux26.TabIndex = 202
        Me.Feux26.Visible = False
        '
        'Feux27
        '
        Me.Feux27.colorClinique = System.Drawing.Color.Empty
        Me.Feux27.colorLibre = System.Drawing.Color.Empty
        Me.Feux27.colorPresence = System.Drawing.Color.Empty
        Me.Feux27.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux27.colorRV = System.Drawing.Color.Empty
        Me.Feux27.Location = New System.Drawing.Point(48, 88)
        Me.Feux27.Name = "Feux27"
        Me.Feux27.Size = New System.Drawing.Size(25, 16)
        Me.Feux27.TabIndex = 203
        Me.Feux27.Visible = False
        '
        'Feux28
        '
        Me.Feux28.colorClinique = System.Drawing.Color.Empty
        Me.Feux28.colorLibre = System.Drawing.Color.Empty
        Me.Feux28.colorPresence = System.Drawing.Color.Empty
        Me.Feux28.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux28.colorRV = System.Drawing.Color.Empty
        Me.Feux28.Location = New System.Drawing.Point(56, 96)
        Me.Feux28.Name = "Feux28"
        Me.Feux28.Size = New System.Drawing.Size(25, 16)
        Me.Feux28.TabIndex = 204
        Me.Feux28.Visible = False
        '
        'Feux29
        '
        Me.Feux29.colorClinique = System.Drawing.Color.Empty
        Me.Feux29.colorLibre = System.Drawing.Color.Empty
        Me.Feux29.colorPresence = System.Drawing.Color.Empty
        Me.Feux29.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux29.colorRV = System.Drawing.Color.Empty
        Me.Feux29.Location = New System.Drawing.Point(64, 104)
        Me.Feux29.Name = "Feux29"
        Me.Feux29.Size = New System.Drawing.Size(25, 16)
        Me.Feux29.TabIndex = 205
        Me.Feux29.Visible = False
        '
        'Feux30
        '
        Me.Feux30.colorClinique = System.Drawing.Color.Empty
        Me.Feux30.colorLibre = System.Drawing.Color.Empty
        Me.Feux30.colorPresence = System.Drawing.Color.Empty
        Me.Feux30.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux30.colorRV = System.Drawing.Color.Empty
        Me.Feux30.Location = New System.Drawing.Point(72, 112)
        Me.Feux30.Name = "Feux30"
        Me.Feux30.Size = New System.Drawing.Size(25, 16)
        Me.Feux30.TabIndex = 206
        Me.Feux30.Visible = False
        '
        'Feux31
        '
        Me.Feux31.colorClinique = System.Drawing.Color.Empty
        Me.Feux31.colorLibre = System.Drawing.Color.Empty
        Me.Feux31.colorPresence = System.Drawing.Color.Empty
        Me.Feux31.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux31.colorRV = System.Drawing.Color.Empty
        Me.Feux31.Location = New System.Drawing.Point(80, 120)
        Me.Feux31.Name = "Feux31"
        Me.Feux31.Size = New System.Drawing.Size(25, 16)
        Me.Feux31.TabIndex = 207
        Me.Feux31.Visible = False
        '
        'Feux32
        '
        Me.Feux32.colorClinique = System.Drawing.Color.Empty
        Me.Feux32.colorLibre = System.Drawing.Color.Empty
        Me.Feux32.colorPresence = System.Drawing.Color.Empty
        Me.Feux32.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux32.colorRV = System.Drawing.Color.Empty
        Me.Feux32.Location = New System.Drawing.Point(48, 104)
        Me.Feux32.Name = "Feux32"
        Me.Feux32.Size = New System.Drawing.Size(25, 16)
        Me.Feux32.TabIndex = 208
        Me.Feux32.Visible = False
        '
        'Feux33
        '
        Me.Feux33.colorClinique = System.Drawing.Color.Empty
        Me.Feux33.colorLibre = System.Drawing.Color.Empty
        Me.Feux33.colorPresence = System.Drawing.Color.Empty
        Me.Feux33.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux33.colorRV = System.Drawing.Color.Empty
        Me.Feux33.Location = New System.Drawing.Point(56, 112)
        Me.Feux33.Name = "Feux33"
        Me.Feux33.Size = New System.Drawing.Size(25, 16)
        Me.Feux33.TabIndex = 209
        Me.Feux33.Visible = False
        '
        'Feux34
        '
        Me.Feux34.colorClinique = System.Drawing.Color.Empty
        Me.Feux34.colorLibre = System.Drawing.Color.Empty
        Me.Feux34.colorPresence = System.Drawing.Color.Empty
        Me.Feux34.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux34.colorRV = System.Drawing.Color.Empty
        Me.Feux34.Location = New System.Drawing.Point(40, 96)
        Me.Feux34.Name = "Feux34"
        Me.Feux34.Size = New System.Drawing.Size(25, 16)
        Me.Feux34.TabIndex = 210
        Me.Feux34.Visible = False
        '
        'Feux35
        '
        Me.Feux35.colorClinique = System.Drawing.Color.Empty
        Me.Feux35.colorLibre = System.Drawing.Color.Empty
        Me.Feux35.colorPresence = System.Drawing.Color.Empty
        Me.Feux35.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux35.colorRV = System.Drawing.Color.Empty
        Me.Feux35.Location = New System.Drawing.Point(48, 104)
        Me.Feux35.Name = "Feux35"
        Me.Feux35.Size = New System.Drawing.Size(25, 16)
        Me.Feux35.TabIndex = 211
        Me.Feux35.Visible = False
        '
        'Feux36
        '
        Me.Feux36.colorClinique = System.Drawing.Color.Empty
        Me.Feux36.colorLibre = System.Drawing.Color.Empty
        Me.Feux36.colorPresence = System.Drawing.Color.Empty
        Me.Feux36.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux36.colorRV = System.Drawing.Color.Empty
        Me.Feux36.Location = New System.Drawing.Point(56, 112)
        Me.Feux36.Name = "Feux36"
        Me.Feux36.Size = New System.Drawing.Size(25, 16)
        Me.Feux36.TabIndex = 212
        Me.Feux36.Visible = False
        '
        'Feux37
        '
        Me.Feux37.colorClinique = System.Drawing.Color.Empty
        Me.Feux37.colorLibre = System.Drawing.Color.Empty
        Me.Feux37.colorPresence = System.Drawing.Color.Empty
        Me.Feux37.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux37.colorRV = System.Drawing.Color.Empty
        Me.Feux37.Location = New System.Drawing.Point(40, 96)
        Me.Feux37.Name = "Feux37"
        Me.Feux37.Size = New System.Drawing.Size(25, 16)
        Me.Feux37.TabIndex = 213
        Me.Feux37.Visible = False
        '
        'Feux38
        '
        Me.Feux38.colorClinique = System.Drawing.Color.Empty
        Me.Feux38.colorLibre = System.Drawing.Color.Empty
        Me.Feux38.colorPresence = System.Drawing.Color.Empty
        Me.Feux38.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux38.colorRV = System.Drawing.Color.Empty
        Me.Feux38.Location = New System.Drawing.Point(48, 104)
        Me.Feux38.Name = "Feux38"
        Me.Feux38.Size = New System.Drawing.Size(25, 16)
        Me.Feux38.TabIndex = 214
        Me.Feux38.Visible = False
        '
        'Feux39
        '
        Me.Feux39.colorClinique = System.Drawing.Color.Empty
        Me.Feux39.colorLibre = System.Drawing.Color.Empty
        Me.Feux39.colorPresence = System.Drawing.Color.Empty
        Me.Feux39.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux39.colorRV = System.Drawing.Color.Empty
        Me.Feux39.Location = New System.Drawing.Point(56, 112)
        Me.Feux39.Name = "Feux39"
        Me.Feux39.Size = New System.Drawing.Size(25, 16)
        Me.Feux39.TabIndex = 215
        Me.Feux39.Visible = False
        '
        'Feux40
        '
        Me.Feux40.colorClinique = System.Drawing.Color.Empty
        Me.Feux40.colorLibre = System.Drawing.Color.Empty
        Me.Feux40.colorPresence = System.Drawing.Color.Empty
        Me.Feux40.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux40.colorRV = System.Drawing.Color.Empty
        Me.Feux40.Location = New System.Drawing.Point(16, 80)
        Me.Feux40.Name = "Feux40"
        Me.Feux40.Size = New System.Drawing.Size(25, 16)
        Me.Feux40.TabIndex = 216
        Me.Feux40.Visible = False
        '
        'Feux41
        '
        Me.Feux41.colorClinique = System.Drawing.Color.Empty
        Me.Feux41.colorLibre = System.Drawing.Color.Empty
        Me.Feux41.colorPresence = System.Drawing.Color.Empty
        Me.Feux41.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux41.colorRV = System.Drawing.Color.Empty
        Me.Feux41.Location = New System.Drawing.Point(24, 88)
        Me.Feux41.Name = "Feux41"
        Me.Feux41.Size = New System.Drawing.Size(25, 16)
        Me.Feux41.TabIndex = 217
        Me.Feux41.Visible = False
        '
        'Feux0
        '
        Me.Feux0.colorClinique = System.Drawing.Color.Empty
        Me.Feux0.colorLibre = System.Drawing.Color.Empty
        Me.Feux0.colorPresence = System.Drawing.Color.Empty
        Me.Feux0.colorPresencePaye = System.Drawing.Color.Empty
        Me.Feux0.colorRV = System.Drawing.Color.Empty
        Me.Feux0.Location = New System.Drawing.Point(96, 136)
        Me.Feux0.Name = "Feux0"
        Me.Feux0.Size = New System.Drawing.Size(25, 16)
        Me.Feux0.TabIndex = 218
        Me.Feux0.Visible = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'nextAgenda
        '
        Me.nextAgenda.BackColor = System.Drawing.SystemColors.Control
        Me.nextAgenda.Cursor = System.Windows.Forms.Cursors.Default
        Me.nextAgenda.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nextAgenda.ForeColor = System.Drawing.SystemColors.ControlText
        Me.nextAgenda.Location = New System.Drawing.Point(796, 4)
        Me.nextAgenda.Name = "nextAgenda"
        Me.nextAgenda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.nextAgenda.Size = New System.Drawing.Size(82, 32)
        Me.nextAgenda.TabIndex = 176
        Me.nextAgenda.Tag = "NOHIDE"
        Me.nextAgenda.Text = "Agenda >>>"
        Me.ToolTip1.SetToolTip(Me.nextAgenda, "Agenda ouverte suivante")
        Me.nextAgenda.UseVisualStyleBackColor = False
        '
        'previousAgenda
        '
        Me.previousAgenda.BackColor = System.Drawing.SystemColors.Control
        Me.previousAgenda.Cursor = System.Windows.Forms.Cursors.Default
        Me.previousAgenda.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.previousAgenda.ForeColor = System.Drawing.SystemColors.ControlText
        Me.previousAgenda.Location = New System.Drawing.Point(714, 4)
        Me.previousAgenda.Name = "previousAgenda"
        Me.previousAgenda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.previousAgenda.Size = New System.Drawing.Size(82, 32)
        Me.previousAgenda.TabIndex = 176
        Me.previousAgenda.Tag = "NOHIDE"
        Me.previousAgenda.Text = "<<< Agenda"
        Me.ToolTip1.SetToolTip(Me.previousAgenda, "Agenda ouverte précédente")
        Me.previousAgenda.UseVisualStyleBackColor = False
        '
        'Chargement
        '
        Me.Chargement.AutoSize = True
        Me.Chargement.Font = New System.Drawing.Font("Arial", 18.0!)
        Me.Chargement.Location = New System.Drawing.Point(24, 160)
        Me.Chargement.Name = "Chargement"
        Me.Chargement.Size = New System.Drawing.Size(273, 27)
        Me.Chargement.TabIndex = 219
        Me.Chargement.Text = "Chargement en cours ..."
        Me.Chargement.Visible = False
        '
        'Agenda
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(921, 473)
        Me.Controls.Add(Me.maxBox)
        Me.Controls.Add(Me.Feux0)
        Me.Controls.Add(Me.Feux41)
        Me.Controls.Add(Me.Feux40)
        Me.Controls.Add(Me.Feux39)
        Me.Controls.Add(Me.Feux38)
        Me.Controls.Add(Me.Feux37)
        Me.Controls.Add(Me.Feux36)
        Me.Controls.Add(Me.Feux35)
        Me.Controls.Add(Me.Feux34)
        Me.Controls.Add(Me.Feux33)
        Me.Controls.Add(Me.Feux32)
        Me.Controls.Add(Me.Feux31)
        Me.Controls.Add(Me.Feux30)
        Me.Controls.Add(Me.Feux29)
        Me.Controls.Add(Me.Feux28)
        Me.Controls.Add(Me.Feux27)
        Me.Controls.Add(Me.Feux26)
        Me.Controls.Add(Me.Feux25)
        Me.Controls.Add(Me.Feux24)
        Me.Controls.Add(Me.Feux23)
        Me.Controls.Add(Me.Feux22)
        Me.Controls.Add(Me.Feux21)
        Me.Controls.Add(Me.Feux20)
        Me.Controls.Add(Me.Feux19)
        Me.Controls.Add(Me.Feux18)
        Me.Controls.Add(Me.Feux17)
        Me.Controls.Add(Me.Feux16)
        Me.Controls.Add(Me.Feux15)
        Me.Controls.Add(Me.Feux14)
        Me.Controls.Add(Me.Feux13)
        Me.Controls.Add(Me.Feux12)
        Me.Controls.Add(Me.Feux11)
        Me.Controls.Add(Me.Feux10)
        Me.Controls.Add(Me.Feux9)
        Me.Controls.Add(Me.Feux8)
        Me.Controls.Add(Me.Feux7)
        Me.Controls.Add(Me.Feux6)
        Me.Controls.Add(Me.Feux5)
        Me.Controls.Add(Me.Feux4)
        Me.Controls.Add(Me.Feux3)
        Me.Controls.Add(Me.Feux2)
        Me.Controls.Add(Me.Feux1)
        Me.Controls.Add(Me.previousAgenda)
        Me.Controls.Add(Me.nextAgenda)
        Me.Controls.Add(Me.adatemonth)
        Me.Controls.Add(Me.pdatemonth)
        Me.Controls.Add(Me.adminBox)
        Me.Controls.Add(Me.tl)
        Me.Controls.Add(Me._LabelsNbDays_21)
        Me.Controls.Add(Me._Labels_5)
        Me.Controls.Add(Me._LabelsNbDays_41)
        Me.Controls.Add(Me._LabelsNbDays_40)
        Me.Controls.Add(Me._LabelsNbDays_39)
        Me.Controls.Add(Me._LabelsNbDays_38)
        Me.Controls.Add(Me._LabelsNbDays_37)
        Me.Controls.Add(Me._LabelsNbDays_36)
        Me.Controls.Add(Me._LabelsNbDays_35)
        Me.Controls.Add(Me._LabelsNbDays_34)
        Me.Controls.Add(Me._LabelsNbDays_33)
        Me.Controls.Add(Me._LabelsNbDays_32)
        Me.Controls.Add(Me._LabelsNbDays_31)
        Me.Controls.Add(Me._LabelsNbDays_30)
        Me.Controls.Add(Me._LabelsNbDays_29)
        Me.Controls.Add(Me._LabelsNbDays_28)
        Me.Controls.Add(Me._LabelsNbDays_27)
        Me.Controls.Add(Me._LabelsNbDays_26)
        Me.Controls.Add(Me._LabelsNbDays_25)
        Me.Controls.Add(Me._LabelsNbDays_24)
        Me.Controls.Add(Me._LabelsNbDays_23)
        Me.Controls.Add(Me._LabelsNbDays_22)
        Me.Controls.Add(Me._LabelsNbDays_20)
        Me.Controls.Add(Me._LabelsNbDays_19)
        Me.Controls.Add(Me._LabelsNbDays_18)
        Me.Controls.Add(Me._LabelsNbDays_17)
        Me.Controls.Add(Me._LabelsNbDays_16)
        Me.Controls.Add(Me._LabelsNbDays_15)
        Me.Controls.Add(Me._LabelsNbDays_14)
        Me.Controls.Add(Me._LabelsNbDays_13)
        Me.Controls.Add(Me._LabelsNbDays_12)
        Me.Controls.Add(Me._LabelsNbDays_11)
        Me.Controls.Add(Me._LabelsNbDays_10)
        Me.Controls.Add(Me._LabelsNbDays_9)
        Me.Controls.Add(Me._LabelsNbDays_8)
        Me.Controls.Add(Me._LabelsNbDays_7)
        Me.Controls.Add(Me._LabelsNbDays_6)
        Me.Controls.Add(Me._LabelsNbDays_5)
        Me.Controls.Add(Me._LabelsNbDays_4)
        Me.Controls.Add(Me._LabelsNbDays_3)
        Me.Controls.Add(Me._LabelsNbDays_2)
        Me.Controls.Add(Me._LabelsNbDays_1)
        Me.Controls.Add(Me._LabelsNbDays_0)
        Me.Controls.Add(Me.Chargement)
        Me.Controls.Add(Me.DayList1)
        Me.Controls.Add(Me.DayList41)
        Me.Controls.Add(Me.DayList40)
        Me.Controls.Add(Me.DayList39)
        Me.Controls.Add(Me.DayList38)
        Me.Controls.Add(Me.DayList37)
        Me.Controls.Add(Me.DayList36)
        Me.Controls.Add(Me.DayList35)
        Me.Controls.Add(Me.DayList34)
        Me.Controls.Add(Me.DayList33)
        Me.Controls.Add(Me.DayList32)
        Me.Controls.Add(Me.DayList31)
        Me.Controls.Add(Me.DayList30)
        Me.Controls.Add(Me.DayList29)
        Me.Controls.Add(Me.DayList28)
        Me.Controls.Add(Me.DayList27)
        Me.Controls.Add(Me.DayList26)
        Me.Controls.Add(Me.DayList25)
        Me.Controls.Add(Me.DayList24)
        Me.Controls.Add(Me.DayList23)
        Me.Controls.Add(Me.DayList22)
        Me.Controls.Add(Me.DayList21)
        Me.Controls.Add(Me.DayList20)
        Me.Controls.Add(Me.DayList19)
        Me.Controls.Add(Me.DayList18)
        Me.Controls.Add(Me.DayList17)
        Me.Controls.Add(Me.DayList16)
        Me.Controls.Add(Me.DayList15)
        Me.Controls.Add(Me.DayList14)
        Me.Controls.Add(Me.DayList13)
        Me.Controls.Add(Me.DayList12)
        Me.Controls.Add(Me.DayList11)
        Me.Controls.Add(Me.DayList10)
        Me.Controls.Add(Me.DayList9)
        Me.Controls.Add(Me.DayList8)
        Me.Controls.Add(Me.DayList7)
        Me.Controls.Add(Me.DayList6)
        Me.Controls.Add(Me.DayList5)
        Me.Controls.Add(Me.DayList4)
        Me.Controls.Add(Me.DayList3)
        Me.Controls.Add(Me.DayList2)
        Me.Controls.Add(Me.DayList0)
        Me.Controls.Add(Me.adateweek)
        Me.Controls.Add(Me.adateday)
        Me.Controls.Add(Me.pdateday)
        Me.Controls.Add(Me.pdateweek)
        Me.Controls.Add(Me.choosedate)
        Me.Controls.Add(Me._MaxMin_40)
        Me.Controls.Add(Me._MaxMin_39)
        Me.Controls.Add(Me._MaxMin_38)
        Me.Controls.Add(Me._MaxMin_37)
        Me.Controls.Add(Me._MaxMin_36)
        Me.Controls.Add(Me._MaxMin_35)
        Me.Controls.Add(Me._MaxMin_34)
        Me.Controls.Add(Me._MaxMin_33)
        Me.Controls.Add(Me._MaxMin_32)
        Me.Controls.Add(Me._MaxMin_31)
        Me.Controls.Add(Me._MaxMin_30)
        Me.Controls.Add(Me._MaxMin_29)
        Me.Controls.Add(Me._MaxMin_28)
        Me.Controls.Add(Me._MaxMin_27)
        Me.Controls.Add(Me._MaxMin_26)
        Me.Controls.Add(Me._MaxMin_25)
        Me.Controls.Add(Me._MaxMin_24)
        Me.Controls.Add(Me._MaxMin_23)
        Me.Controls.Add(Me._MaxMin_22)
        Me.Controls.Add(Me._MaxMin_21)
        Me.Controls.Add(Me._MaxMin_20)
        Me.Controls.Add(Me._MaxMin_19)
        Me.Controls.Add(Me._MaxMin_18)
        Me.Controls.Add(Me._MaxMin_17)
        Me.Controls.Add(Me._MaxMin_16)
        Me.Controls.Add(Me._MaxMin_15)
        Me.Controls.Add(Me._MaxMin_14)
        Me.Controls.Add(Me._MaxMin_13)
        Me.Controls.Add(Me._MaxMin_12)
        Me.Controls.Add(Me._MaxMin_11)
        Me.Controls.Add(Me._MaxMin_10)
        Me.Controls.Add(Me._MaxMin_9)
        Me.Controls.Add(Me._MaxMin_8)
        Me.Controls.Add(Me._MaxMin_7)
        Me.Controls.Add(Me._MaxMin_6)
        Me.Controls.Add(Me._MaxMin_5)
        Me.Controls.Add(Me._MaxMin_4)
        Me.Controls.Add(Me._MaxMin_3)
        Me.Controls.Add(Me._MaxMin_2)
        Me.Controls.Add(Me._MaxMin_1)
        Me.Controls.Add(Me._MaxMin_0)
        Me.Controls.Add(Me.legende)
        Me.Controls.Add(Me._MaxMin_41)
        Me.Controls.Add(Me._Lignes_14)
        Me.Controls.Add(Me._Lignes_13)
        Me.Controls.Add(Me._Lignes_12)
        Me.Controls.Add(Me._Lignes_11)
        Me.Controls.Add(Me._Lignes_10)
        Me.Controls.Add(Me._Lignes_9)
        Me.Controls.Add(Me._Lignes_8)
        Me.Controls.Add(Me._Lignes_7)
        Me.Controls.Add(Me._Lignes_6)
        Me.Controls.Add(Me._Lignes_5)
        Me.Controls.Add(Me._Lignes_4)
        Me.Controls.Add(Me._Lignes_3)
        Me.Controls.Add(Me._Lignes_2)
        Me.Controls.Add(Me._Lignes_1)
        Me.Controls.Add(Me._Lignes_0)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(148, 197)
        Me.MaximizeBox = False
        Me.Name = "Agenda"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Tag = "0"
        Me.Text = "Agenda"
        Me.maxBox.ResumeLayout(False)
        Me.maxBox.PerformLayout()
        Me.adminBox.ResumeLayout(False)
        Me.adminBox.PerformLayout()
        Me.legende.ResumeLayout(False)
        Me.legende.PerformLayout()
        Me.menuclickagenda.ResumeLayout(False)
        Me.menuclickagenda.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private Sub linkDayListArray()
        Me.DayList = New DayListArray()
        Me.DayList.Add(Me.DayList0)
        Me.DayList.Add(Me.DayList1)
        Me.DayList.Add(Me.DayList2)
        Me.DayList.Add(Me.DayList3)
        Me.DayList.Add(Me.DayList4)
        Me.DayList.Add(Me.DayList5)
        Me.DayList.Add(Me.DayList6)
        Me.DayList.Add(Me.DayList7)
        Me.DayList.Add(Me.DayList8)
        Me.DayList.Add(Me.DayList9)
        Me.DayList.Add(Me.DayList10)
        Me.DayList.Add(Me.DayList11)
        Me.DayList.Add(Me.DayList12)
        Me.DayList.Add(Me.DayList13)
        Me.DayList.Add(Me.DayList14)
        Me.DayList.Add(Me.DayList15)
        Me.DayList.Add(Me.DayList16)
        Me.DayList.Add(Me.DayList17)
        Me.DayList.Add(Me.DayList18)
        Me.DayList.Add(Me.DayList19)
        Me.DayList.Add(Me.DayList20)
        Me.DayList.Add(Me.DayList21)
        Me.DayList.Add(Me.DayList22)
        Me.DayList.Add(Me.DayList23)
        Me.DayList.Add(Me.DayList24)
        Me.DayList.Add(Me.DayList25)
        Me.DayList.Add(Me.DayList26)
        Me.DayList.Add(Me.DayList27)
        Me.DayList.Add(Me.DayList28)
        Me.DayList.Add(Me.DayList29)
        Me.DayList.Add(Me.DayList30)
        Me.DayList.Add(Me.DayList31)
        Me.DayList.Add(Me.DayList32)
        Me.DayList.Add(Me.DayList33)
        Me.DayList.Add(Me.DayList34)
        Me.DayList.Add(Me.DayList35)
        Me.DayList.Add(Me.DayList36)
        Me.DayList.Add(Me.DayList37)
        Me.DayList.Add(Me.DayList38)
        Me.DayList.Add(Me.DayList39)
        Me.DayList.Add(Me.DayList40)
        Me.DayList.Add(Me.DayList41)
        Me.DayList.Add(Me.DayList42)

        Dim i As Integer
        For i = 0 To DayList.Count - 1
            AddHandler dayList(i).clickEnabledChange, AddressOf dayList_ClickEnabledChange
            AddHandler dayList(i).dblClick, AddressOf dayList_DblClick
            AddHandler dayList(i).itemClick, AddressOf dayList_Click
            AddHandler dayList(i).KeyDown, AddressOf dayList_KeyDown
            AddHandler dayList(i).keyUp, AddressOf dayList_KeyUp
            AddHandler dayList(i).mouseMove, AddressOf dayList_MouseMove
            AddHandler dayList(i).mouseDown, AddressOf dayList_MouseDown
            AddHandler dayList(i).mouseUp, AddressOf dayList_MouseUp
            AddHandler dayList(i).MouseWheel, AddressOf dayList_MouseWheel
            AddHandler dayList(i).show_Renamed, AddressOf dayList_Show_Renamed
            AddHandler dayList(i).willSelect, AddressOf dayList_WillSelect
            AddHandler dayList(i).hScroll_Scroll, AddressOf dayList_HScrollScroll
            AddHandler dayList(i).vScroll_Scroll, AddressOf dayList_VScrollScroll
        Next i
    End Sub
    Private Sub linkFeuxArray()
        Me.Feux = New BaseObjArray()
        Me.Feux.Add(Me.Feux0)
        Me.Feux.Add(Me.Feux1)
        Me.Feux.Add(Me.Feux2)
        Me.Feux.Add(Me.Feux3)
        Me.Feux.Add(Me.Feux4)
        Me.Feux.Add(Me.Feux5)
        Me.Feux.Add(Me.Feux6)
        Me.Feux.Add(Me.Feux7)
        Me.Feux.Add(Me.Feux8)
        Me.Feux.Add(Me.Feux9)
        Me.Feux.Add(Me.Feux10)
        Me.Feux.Add(Me.Feux11)
        Me.Feux.Add(Me.Feux12)
        Me.Feux.Add(Me.Feux13)
        Me.Feux.Add(Me.Feux14)
        Me.Feux.Add(Me.Feux15)
        Me.Feux.Add(Me.Feux16)
        Me.Feux.Add(Me.Feux17)
        Me.Feux.Add(Me.Feux18)
        Me.Feux.Add(Me.Feux19)
        Me.Feux.Add(Me.Feux20)
        Me.Feux.Add(Me.Feux21)
        Me.Feux.Add(Me.Feux22)
        Me.Feux.Add(Me.Feux23)
        Me.Feux.Add(Me.Feux24)
        Me.Feux.Add(Me.Feux25)
        Me.Feux.Add(Me.Feux26)
        Me.Feux.Add(Me.Feux27)
        Me.Feux.Add(Me.Feux28)
        Me.Feux.Add(Me.Feux29)
        Me.Feux.Add(Me.Feux30)
        Me.Feux.Add(Me.Feux31)
        Me.Feux.Add(Me.Feux32)
        Me.Feux.Add(Me.Feux33)
        Me.Feux.Add(Me.Feux34)
        Me.Feux.Add(Me.Feux35)
        Me.Feux.Add(Me.Feux36)
        Me.Feux.Add(Me.Feux37)
        Me.Feux.Add(Me.Feux38)
        Me.Feux.Add(Me.Feux39)
        Me.Feux.Add(Me.Feux40)
        Me.Feux.Add(Me.Feux41)
        Me.Feux.Add(Me.Feux42)
    End Sub
    Private Sub linkLabelsNbDaysArray()
        Me.LabelsNbDays = New BaseObjArray()
        Me.LabelsNbDays.Add(Me._LabelsNbDays_0)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_1)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_2)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_3)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_4)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_5)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_6)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_7)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_8)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_9)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_10)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_11)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_12)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_13)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_14)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_15)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_16)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_17)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_18)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_19)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_20)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_21)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_22)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_23)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_24)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_25)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_26)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_27)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_28)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_29)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_30)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_31)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_32)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_33)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_34)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_35)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_36)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_37)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_38)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_39)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_40)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_41)
        Me.LabelsNbDays.Add(Me._LabelsNbDays_42)
    End Sub
    Private Sub linkMaxMinArray()
        Me.MaxMin = New BaseObjArray()
        Me.MaxMin.Add(Me._MaxMin_0)
        Me.MaxMin.Add(Me._MaxMin_1)
        Me.MaxMin.Add(Me._MaxMin_2)
        Me.MaxMin.Add(Me._MaxMin_3)
        Me.MaxMin.Add(Me._MaxMin_4)
        Me.MaxMin.Add(Me._MaxMin_5)
        Me.MaxMin.Add(Me._MaxMin_6)
        Me.MaxMin.Add(Me._MaxMin_7)
        Me.MaxMin.Add(Me._MaxMin_8)
        Me.MaxMin.Add(Me._MaxMin_9)
        Me.MaxMin.Add(Me._MaxMin_10)
        Me.MaxMin.Add(Me._MaxMin_11)
        Me.MaxMin.Add(Me._MaxMin_12)
        Me.MaxMin.Add(Me._MaxMin_13)
        Me.MaxMin.Add(Me._MaxMin_14)
        Me.MaxMin.Add(Me._MaxMin_15)
        Me.MaxMin.Add(Me._MaxMin_16)
        Me.MaxMin.Add(Me._MaxMin_17)
        Me.MaxMin.Add(Me._MaxMin_18)
        Me.MaxMin.Add(Me._MaxMin_19)
        Me.MaxMin.Add(Me._MaxMin_20)
        Me.MaxMin.Add(Me._MaxMin_21)
        Me.MaxMin.Add(Me._MaxMin_22)
        Me.MaxMin.Add(Me._MaxMin_23)
        Me.MaxMin.Add(Me._MaxMin_24)
        Me.MaxMin.Add(Me._MaxMin_25)
        Me.MaxMin.Add(Me._MaxMin_26)
        Me.MaxMin.Add(Me._MaxMin_27)
        Me.MaxMin.Add(Me._MaxMin_28)
        Me.MaxMin.Add(Me._MaxMin_29)
        Me.MaxMin.Add(Me._MaxMin_30)
        Me.MaxMin.Add(Me._MaxMin_31)
        Me.MaxMin.Add(Me._MaxMin_32)
        Me.MaxMin.Add(Me._MaxMin_33)
        Me.MaxMin.Add(Me._MaxMin_34)
        Me.MaxMin.Add(Me._MaxMin_35)
        Me.MaxMin.Add(Me._MaxMin_36)
        Me.MaxMin.Add(Me._MaxMin_37)
        Me.MaxMin.Add(Me._MaxMin_38)
        Me.MaxMin.Add(Me._MaxMin_39)
        Me.MaxMin.Add(Me._MaxMin_40)
        Me.MaxMin.Add(Me._MaxMin_41)
        Me.MaxMin.Add(Me._MaxMin_42)
    End Sub
    Private Sub linkLignesArray()
        Me.Lignes = New BaseObjArray()
        Me.Lignes.Add(Me._Lignes_0)
        Me.Lignes.Add(Me._Lignes_1)
        Me.Lignes.Add(Me._Lignes_2)
        Me.Lignes.Add(Me._Lignes_3)
        Me.Lignes.Add(Me._Lignes_4)
        Me.Lignes.Add(Me._Lignes_5)
        Me.Lignes.Add(Me._Lignes_6)
        Me.Lignes.Add(Me._Lignes_7)
        Me.Lignes.Add(Me._Lignes_8)
        Me.Lignes.Add(Me._Lignes_9)
        Me.Lignes.Add(Me._Lignes_10)
        Me.Lignes.Add(Me._Lignes_11)
        Me.Lignes.Add(Me._Lignes_12)
        Me.Lignes.Add(Me._Lignes_13)
        Me.Lignes.Add(Me._Lignes_14)
    End Sub
    Private Sub linkmenujourArray()
        Me.menujour = New BaseObjArray()
        menujour.Add(Me._menujour_1)
        menujour.Add(Me._menujour_1)
        menujour.Add(Me._menujour_2)
        menujour.Add(Me._menujour_3)
        menujour.Add(Me._menujour_4)
        menujour.Add(Me._menujour_5)
        menujour.Add(Me._menujour_6)
    End Sub
    Private Sub linkmenusemaineArray()
        Me.menusemaine = New BaseObjArray()
        menusemaine.Add(Me._menusemaine_1)
        menusemaine.Add(Me._menusemaine_1)
        menusemaine.Add(Me._menusemaine_2)
        menusemaine.Add(Me._menusemaine_3)
        menusemaine.Add(Me._menusemaine_4)
    End Sub
#End Region

#Region "menujour Events"
    Private Sub _menujour_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_1.Click
        menujour_Click(1, sender, e)
    End Sub
    Private Sub _menujour_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_2.Click
        menujour_Click(2, sender, e)
    End Sub
    Private Sub _menujour_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_3.Click
        menujour_Click(3, sender, e)
    End Sub
    Private Sub _menujour_4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_4.Click
        menujour_Click(4, sender, e)
    End Sub
    Private Sub _menujour_5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_5.Click
        menujour_Click(5, sender, e)
    End Sub
    Private Sub _menujour_6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menujour_6.Click
        menujour_Click(6, sender, e)
    End Sub
#End Region

#Region "menusemaine Events"
    Private Sub _menusemaine_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_1.Click
        menusemaine_Click(1, sender, e)
    End Sub
    Private Sub _menusemaine_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_2.Click
        menusemaine_Click(2, sender, e)
    End Sub
    Private Sub _menusemaine_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_3.Click
        menusemaine_Click(3, sender, e)
    End Sub
    Private Sub _menusemaine_4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _menusemaine_4.Click
        menusemaine_Click(4, sender, e)
    End Sub
#End Region

#Region "MaxMin Events"
    Private Sub _MaxMin_0_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_0.Click
        MaxMin_Click(0, Sender, e)
    End Sub
    Private Sub _MaxMin_1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_1.Click
        MaxMin_Click(1, Sender, e)
    End Sub
    Private Sub _MaxMin_2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_2.Click
        MaxMin_Click(2, Sender, e)
    End Sub
    Private Sub _MaxMin_3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_3.Click
        MaxMin_Click(3, Sender, e)
    End Sub
    Private Sub _MaxMin_4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_4.Click
        MaxMin_Click(4, Sender, e)
    End Sub
    Private Sub _MaxMin_5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_5.Click
        MaxMin_Click(5, Sender, e)
    End Sub
    Private Sub _MaxMin_6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_6.Click
        MaxMin_Click(6, Sender, e)
    End Sub
    Private Sub _MaxMin_7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_7.Click
        MaxMin_Click(7, Sender, e)
    End Sub
    Private Sub _MaxMin_8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_8.Click
        MaxMin_Click(8, Sender, e)
    End Sub
    Private Sub _MaxMin_9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_9.Click
        MaxMin_Click(9, Sender, e)
    End Sub
    Private Sub _MaxMin_10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_10.Click
        MaxMin_Click(10, Sender, e)
    End Sub
    Private Sub _MaxMin_11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_11.Click
        MaxMin_Click(11, Sender, e)
    End Sub
    Private Sub _MaxMin_12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_12.Click
        MaxMin_Click(12, Sender, e)
    End Sub
    Private Sub _MaxMin_13_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_13.Click
        MaxMin_Click(13, Sender, e)
    End Sub
    Private Sub _MaxMin_14_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_14.Click
        MaxMin_Click(14, Sender, e)
    End Sub
    Private Sub _MaxMin_15_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_15.Click
        MaxMin_Click(15, Sender, e)
    End Sub
    Private Sub _MaxMin_16_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_16.Click
        MaxMin_Click(16, Sender, e)
    End Sub
    Private Sub _MaxMin_17_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_17.Click
        MaxMin_Click(17, Sender, e)
    End Sub
    Private Sub _MaxMin_18_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_18.Click
        MaxMin_Click(18, Sender, e)
    End Sub
    Private Sub _MaxMin_19_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_19.Click
        MaxMin_Click(19, Sender, e)
    End Sub
    Private Sub _MaxMin_20_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_20.Click
        MaxMin_Click(20, Sender, e)
    End Sub
    Private Sub _MaxMin_21_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_21.Click
        MaxMin_Click(21, Sender, e)
    End Sub
    Private Sub _MaxMin_22_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_22.Click
        MaxMin_Click(22, Sender, e)
    End Sub
    Private Sub _MaxMin_23_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_23.Click
        MaxMin_Click(23, Sender, e)
    End Sub
    Private Sub _MaxMin_24_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_24.Click
        MaxMin_Click(24, Sender, e)
    End Sub
    Private Sub _MaxMin_25_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_25.Click
        MaxMin_Click(25, Sender, e)
    End Sub
    Private Sub _MaxMin_26_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_26.Click
        MaxMin_Click(26, Sender, e)
    End Sub
    Private Sub _MaxMin_27_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_27.Click
        MaxMin_Click(27, Sender, e)
    End Sub
    Private Sub _MaxMin_28_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_28.Click
        MaxMin_Click(28, Sender, e)
    End Sub
    Private Sub _MaxMin_29_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_29.Click
        MaxMin_Click(29, Sender, e)
    End Sub
    Private Sub _MaxMin_30_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_30.Click
        MaxMin_Click(30, Sender, e)
    End Sub
    Private Sub _MaxMin_31_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_31.Click
        MaxMin_Click(31, Sender, e)
    End Sub
    Private Sub _MaxMin_32_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_32.Click
        MaxMin_Click(32, Sender, e)
    End Sub
    Private Sub _MaxMin_33_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_33.Click
        MaxMin_Click(33, Sender, e)
    End Sub
    Private Sub _MaxMin_34_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_34.Click
        MaxMin_Click(34, Sender, e)
    End Sub
    Private Sub _MaxMin_35_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_35.Click
        MaxMin_Click(35, Sender, e)
    End Sub
    Private Sub _MaxMin_36_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_36.Click
        MaxMin_Click(36, Sender, e)
    End Sub
    Private Sub _MaxMin_37_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_37.Click
        MaxMin_Click(37, Sender, e)
    End Sub
    Private Sub _MaxMin_38_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_38.Click
        MaxMin_Click(38, Sender, e)
    End Sub
    Private Sub _MaxMin_39_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_39.Click
        MaxMin_Click(39, Sender, e)
    End Sub
    Private Sub _MaxMin_40_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_40.Click
        MaxMin_Click(40, Sender, e)
    End Sub
    Private Sub _MaxMin_41_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_41.Click
        MaxMin_Click(41, Sender, e)
    End Sub
    Private Sub _MaxMin_42_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MaxMin_42.Click
        MaxMin_Click(42, Sender, e)
    End Sub
#End Region

    Private scrollAllLists As Boolean = False
    Private maximiseIndex As Integer = 0
    Private prematureEnding As Boolean = False
    Private prematureClosing As Boolean = False
    Private mouseButton As Byte
    Dim adjustByDay(42) As Byte
    Private _NoHorizontal As Short
    Private _NoVertical As Short
    Private _DebutDate As Date
    Private _AgendaLoaded As Boolean = False
    Private skipQueueList As Boolean = True
    Private dragging As Boolean = False
    Private myWeek As Date
    Private _NoTRP As Integer
    Private daysRectangle As Rectangle
    Private lastAgendaItemClicked As Integer = -1
    Private lastAgendaListSel As CI.Controls.List
    Private lastItemValueA As Object = Nothing


#Region "Admin Commands"
    Private Sub mSpace_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MSpace.TextChanged
        If Not MSpace.Text = "" Then UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub yDephaseBox_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles YDephaseBox.TextChanged
        If Not YDephaseBox.Text = "" Then UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Byte
        For i = 0 To 41
            If DayList(i).Visible = True Then DayList(i).ItemBorder = 3 : DayList(i).Draw = True
        Next i
    End Sub

    Private Sub button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        AdminBox.Visible = False
    End Sub

    Private Sub command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Dim He, mi As String
        Dim i, n, j As Byte
        Dim loadingTime As Double = DateTime.Now.Ticks
        Dim myInputBoxPlus As New InputBoxPlus()
        n = MyInputBoxPlus("Nombre de case", "# de case", "72")
        For i = 0 To 41
            DayList(i).Draw = False
            If DayList(i).Visible = True Then
                For j = 1 To n
                    He = (6 + (j / 4)) - 0.25
                    Mi = ((He - CInt(He)) * 60)
                    He = CInt(He)
                    DayList(i).Add(He & ":" & Mi & " Boivin, Jonathan")
                Next j
            End If
            DayList(i).Draw = True
        Next i

        LoadingTime = DateTime.Now.Ticks - LoadingTime
        LoadingTime /= 10000000 'To get in seconds
        Me.TL.Text = "Time to load->" & LoadingTime : Me.TL.Visible = True
    End Sub

    Private Sub command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
        Dim i As Byte
        For i = 0 To 41
            If DayList(i).Visible = True Then DayList(i).Cls()
        Next i
    End Sub
#End Region

#Region "Properties"
    Public Property noTRP() As Integer
        Get
            Return _NoTRP
        End Get
        Set(ByVal Value As Integer)
            _NoTRP = Value
        End Set
    End Property

    Public Property agendaLoaded() As Boolean
        Get
            AgendaLoaded = _AgendaLoaded
        End Get
        Set(ByVal Value As Boolean)
            _AgendaLoaded = Value
        End Set
    End Property
    Public Property DebutDate(Optional ByVal updating As Boolean = False) As Date
        Get
            DebutDate = _DebutDate
        End Get
        Set(ByVal Value As Date)
            _DebutDate = Value
            If Updating = True Then Me.UpdateStructure(Me.NoTRP)
        End Set
    End Property
    'Public Property mnuperiode(Optional ByVal Updating As Boolean = False) As ToolStripItem
    '    Get
    '        mnuperiode = _mnuperiode
    '    End Get
    '    Set(ByVal Value As ToolStripItem)
    '        _mnuperiode = Value
    '        If Updating = True Then Me.UpdateStructure()
    '    End Set
    'End Property
    'Public Property nomTherapeute(Optional ByVal Updating As Boolean = False) As String
    '    Get
    '        nomTherapeute = _nomTherapeute
    '    End Get
    '    Set(ByVal Value As String)
    '        _nomTherapeute = Value
    '        If Updating = True Then Me.UpdateStructure()
    '    End Set
    'End Property
    'Public Property mnutherapeute(Optional ByVal Updating As Boolean = False) As Short
    '    Get
    '        mnutherapeute = _mnutherapeute
    '    End Get
    '    Set(ByVal Value As Short)
    '        _mnutherapeute = Value
    '        If Updating = True Then Me.UpdateStructure()
    '    End Set
    'End Property
    Public Property NoHorizontal(Optional ByVal updating As Boolean = False) As Short
        Get
            NoHorizontal = _NoHorizontal
        End Get
        Set(ByVal Value As Short)
            _NoHorizontal = Value
            If Updating = True Then Me.UpdateStructure(Me.NoTRP)
        End Set
    End Property
    Public Property NoVertical(Optional ByVal updating As Boolean = False) As Short
        Get
            NoVertical = _NoVertical
        End Get
        Set(ByVal Value As Short)
            _NoVertical = Value
            If Updating = True Then Me.UpdateStructure(Me.NoTRP)
        End Set
    End Property
#End Region

#Region "Context Menu"
    Private Sub menuChangeRVRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuChangeRVRemarks.Click
        ensureItemValuesGood()
        Dim myNo As Short = lastAgendaListSel.selected
        If Not TypeOf lastAgendaListSel.ItemValueA(myNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = lastAgendaListSel.ItemValueA(myNo)

        rv.changeRemarks()
    End Sub

    Private Sub menuDeleteRVRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDeleteRVRemarks.Click
        ensureItemValuesGood()
        Dim myNo As Short = lastAgendaListSel.selected
        If Not TypeOf lastAgendaListSel.ItemValueA(myNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = lastAgendaListSel.ItemValueA(myNo)

        rv.deleteRemarks()
    End Sub

    Private Sub menuContinuerLaListeDattente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuContinuerLaListeDattente.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is AgendaEntryBlocked Then Exit Sub
        Dim blocked As AgendaEntryBlocked = LastAgendaListSel.ItemValueA(MyNo)

        blocked.skipQueueList = False
        blocked.delete()
    End Sub

    Private Sub menuStartQL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles menuStartQL.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        If LastAgendaListSel.ItemValueA(MyNo).ToString <> "" Then Exit Sub

        If openRestraintQueueList(CDate(LastAgendaListSel.Tag & " " & LastAgendaListSel.ItemText(MyNo).Substring(0, 5)), NoTRP) = False Then MessageBox.Show("Il n'existe aucun client sur la liste d'attente pouvant être placé à cette plage", "Liste d'attente")
    End Sub

    Private Sub menuRapportWOFolder_Click(ByVal sender As Object, ByVal e As EventArgs)
        EnsureItemValuesGood()

        StartClientRapportGen(CType(sender, ToolStripMenuItem).Text, False)
    End Sub

    Private Sub menuRapportWithFolder_Click(ByVal sender As Object, ByVal e As EventArgs)
        EnsureItemValuesGood()

        StartClientRapportGen(CType(sender, ToolStripMenuItem).Text)
    End Sub

    Private Sub menuSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSendEmail.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        Dim clientEmail() As String = DBLinker.GetInstance.ReadOneDBField("InfoClients", "Courriel", "WHERE NoClient=" & rv.NoClient & ";")
        If ClientEmail Is Nothing OrElse ClientEmail.Length = 0 Then MessageBox.Show("Le client n'a pas de courriel", "Impossible d'envoyer") : Exit Sub
        If ClientEmail(0) = "" Then MessageBox.Show("Le client n'a pas de courriel", "Impossible d'envoyer") : Exit Sub

        SendEmailTo(ClientEmail(0))
    End Sub

    Private Sub menuAnnonceClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAnnonceClient.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        rv.annonce()
    End Sub

    Private Sub menuConfirmation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuConfirmation.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)
        rv.confirm()
    End Sub

    Private Sub menuOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuOpenFolder.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        If MessageBox.Show("Êtes-vous sûr de vouloir activer ce dossier ?", "Confirmation d'activation", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        Accounts.Clients.Folders.ClientFolder.changeStatus(Accounts.Clients.Folders.FoldersStatus.FolderPossibleStatuses.Inactive, Accounts.Clients.Folders.FoldersStatus.FolderPossibleStatuses.Active, curRV.NoClient, curRV.NoFolder)
    End Sub

    Private Sub menuCloseFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCloseFolder.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        If MessageBox.Show("Êtes-vous sûr de vouloir désactiver ce dossier ?" & vbCrLf & "Cette procédure éliminera tous les rendez-vous futurs.", "Confirmation de désactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then Exit Sub

        Accounts.Clients.Folders.ClientFolder.changeStatus(Accounts.Clients.Folders.FoldersStatus.FolderPossibleStatuses.Active, Accounts.Clients.Folders.FoldersStatus.FolderPossibleStatuses.Inactive, curRV.NoClient, curRV.NoFolder)
    End Sub

    Private Sub menuRVFutur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRVFutur.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        MyMainWin.menuRVFutur.Checked = True
        With MyMainWin.RVMenu
            .setRVClient(curRV.clientName, curRV.noClient)
            .setTRP(Me.noTRP)
            .visible = True
            If .IsSwitchedToToolbar Then myMainWin.barMainObjects.showPanel()
        End With
    End Sub

    Private Sub menuModifyReserved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuModifyReserved.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is AgendaEntryReserved Then Exit Sub
        Dim reserved As AgendaEntryReserved = LastAgendaListSel.ItemValueA(MyNo)

        'Droit & Accès
        If CurrentDroitAcces(73) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier les plages réservées." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myReservedAsk As New ReservedAsk()
        MyReservedAsk.CurrentTRP = UsersManager.getInstance.getUser(Me.NoTRP).ToString()
        MyReservedAsk.DayDate = LastAgendaListSel.Tag
        MyReservedAsk.LineTime = Microsoft.VisualBasic.Left(LastAgendaListSel.ItemText(MyNo), 5)
        MyReservedAsk.ListFrom = LastAgendaListSel
        MyReservedAsk.CurrentPeriode = reserved.period
        MyReservedAsk.LoadingData = LastAgendaListSel.ItemText(MyNo).Substring(6).Replace("<br>", vbCrLf)
        MyReservedAsk.NoAgenda = reserved.NoAgendaEntry
        MyReservedAsk.ShowDialog()
    End Sub

    Private Sub menujour_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles menujour.Click
        UncheckedPeriode(MyMainWin)

        MyMainWin.menujour(Index).Checked = True
        UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub menusemaine_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles menusemaine.Click
        UncheckedPeriode(MyMainWin)

        MyMainWin.menusemaine(Index).Checked = True
        UpdateStructure(Me.NoTRP)
    End Sub

    Public Sub menumois_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menumois.Click
        UncheckedPeriode(MyMainWin)
        MyMainWin.menumois.Checked = True
        UpdateStructure(Me.NoTRP)
    End Sub

    Public Sub menupdateday_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupdateday.Click
        DebutDate = DebutDate.AddDays(-1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Public Sub menupdateweek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupdateweek.Click
        DebutDate = DebutDate.AddDays(-7)
        UpdateStructure(Me.NoTRP)
    End Sub

    Public Sub menupdatemonth_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupdatemonth.Click
        DebutDate = DebutDate.AddMonths(-1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Public Sub menuadateday_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuadateday.Click
        DebutDate = DebutDate.AddDays(1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Public Sub menuadateweek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuadateweek.Click
        DebutDate = DebutDate.AddDays(7)
        UpdateStructure(Me.NoTRP)
    End Sub

    Public Sub menuadatemonth_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuadatemonth.Click
        DebutDate = DebutDate.AddMonths(1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub menuQueueList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuQueueList.Click
        OpenQL()
    End Sub

    Private Sub menuAddToQueueList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAddToQueueList.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        AddToListeAttente(rv.NoClient, rv.NoFolder, rv.NoVisite, rv.NoTRP, rv.period, rv.DateHeure)
    End Sub

    Private Sub menuaCouper_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuaCouper.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        Dim rv As AgendaEntry = LastAgendaListSel.ItemValueA(MyNo)

        rv.cut()
    End Sub

    Private Sub menuOpenHoraire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpenHoraire.Click
        EnsureItemValuesGood()

        openCloseHoraire(lastAgendaListSel.selected, lastAgendaListSel.selected)
    End Sub

    Private Sub menuOpenHoraireUpto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpenHoraireUpto.Click
        EnsureItemValuesGood()

        Dim index As Byte = lastAgendaListSel.Name.Substring(7)
        Dim firstLine As Integer = lastAgendaListSel.selected
        Dim secondLine As Integer = findFirstTime(index) - 1

        'If use below opened schedule
        If firstLine > secondLine Then
            secondLine = firstLine
            firstLine = findLastTime(index) + 1
        End If

        openCloseHoraire(firstLine, secondLine)
    End Sub

    Private Sub menuCloseHoraire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuCloseHoraire.Click
        EnsureItemValuesGood()

        openCloseHoraire(lastAgendaListSel.selected, lastAgendaListSel.selected, False)
    End Sub

    Private Sub menumodifhorairetrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menumodifhorairetrp.Click
        OpenModifHoraire(Me.NoTRP)
    End Sub

    Private Sub menuModifHoraireClinique_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuModifHoraireClinique.Click
        OpenModifHoraire(0)
    End Sub

    Private Sub menuPreferences_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuPreferences.Click
        Dim myPreferences As preferencesWin = OpenUniqueWindow(New preferencesWin())
        MyPreferences.Show()
    End Sub

    Private Sub menuAddToKeyPeople_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddToKeyPeople.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        Dim generalInfo(,) As String = DBLinker.GetInstance.ReadDB(" Villes RIGHT JOIN InfoClients ON Villes.NoVille = InfoClients.NoVille", "Nom+','+Prenom,Adresse,NomVille,CodePostal,Telephones,Courriel,Url,NAM", "WHERE (NoClient)=" & curRV.NoClient & ";")

        Comptes.AddKP(GeneralInfo(0, 0), GeneralInfo(1, 0), GeneralInfo(2, 0), GeneralInfo(3, 0), GeneralInfo(4, 0), GeneralInfo(5, 0), GeneralInfo(6, 0), GeneralInfo(7, 0), "", "", "", , False)
    End Sub

    Private Sub menupaiement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menupaiement.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        Dim myPaiement As Payment = OpenUniqueWindow(New Payment(), "Effectuer le(s) paiement(s) de " & curRV.ClientName)
        If MyPaiement.BillsLoaded = False Then MyPaiement.Loading(curRV.NoClient, FacturationBox.DedicatedType.Client)
        MyPaiement.Show()
    End Sub

    Public Sub menuacopier_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuacopier.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        Dim rv As AgendaEntry = LastAgendaListSel.ItemValueA(MyNo)

        rv.copy()
    End Sub

    Public Sub menuaenlever_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuaenlever.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        Dim entry As AgendaEntry = LastAgendaListSel.ItemValueA(MyNo)

        entry.delete()
    End Sub

    Private Sub ensureItemValuesGood()
        Try
            Dim myNo As Short = lastAgendaListSel.selected
            If lastAgendaListSel.ItemText(myNo) = "" Then
                lastAgendaListSel.selected = lastAgendaListSel.findFirstItem()
                Throw New Exception("000 ( lastItemValueA is Nothing)=" & (lastItemValueA Is Nothing) & vbCrLf & "Chargement.Visible=" & Chargement.Visible)
            End If

            If lastAgendaListSel.ItemValueA(myNo) Is Nothing Then
                lastAgendaListSel.selected = lastAgendaListSel.findFirstItem()
                Throw New Exception("111 ( lastItemValueA is Nothing)=" & (lastItemValueA Is Nothing) & vbCrLf & "Chargement.Visible=" & Chargement.Visible)
            End If

            If lastAgendaListSel.ItemValueA(myNo) IsNot Nothing AndAlso lastItemValueA IsNot Nothing AndAlso lastAgendaListSel.ItemValueA(myNo).GetHashCode <> lastItemValueA.GetHashCode Then
                lastAgendaListSel.selected = lastAgendaListSel.findFirstItem()
                Throw New Exception("222 LastAgendaListSel.ItemValueA(MyNo)=" & lastAgendaListSel.ItemValueA(myNo) & vbCrLf & "LastItemValueA=" & lastItemValueA & vbCrLf & "Chargement.Visible=" & Chargement.Visible)
            End If
        Catch ex As Exception
            MessageBox.Show("Une erreur minime de données est survenue ; probablement due au rechargement de la journée." & vbCrLf & "Veuillez réessayer et merci de votre compréhension.", "Erreur")
            Throw New Exception("LastAgendaListSel Is Nothing=" & (lastAgendaListSel Is Nothing) & vbCrLf & "Chargement.Visible=" & Chargement.Visible, ex)
        End Try
    End Sub

    Public Sub menuabsentmotive_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuabsentmotive.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        LastAgendaListSel.ClickEnabled = False
        Dim errorMsg As String = rv.changeStatus(Accounts.Clients.Folders.RVsStatus.RVPossibleStatuses.NotPresentMotivated)
        If ErrorMsg <> "" AndAlso ErrorMsg.Contains("annulé") = False Then MessageBox.Show(ErrorMsg, "Impossible de changer le statut")
        LastAgendaListSel.ClickEnabled = True
    End Sub

    Private Sub menuPrintAgenda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuPrintAgenda.Click
        Dim tpFilter As New FilteringComposite
        Dim userFilter As New FilteringUser
        UserFilter.NoUser = Me.NoTRP
        UserFilter.User = UsersManager.getInstance.getUser(Me.NoTRP).ToString()
        TPFilter.Add(UserFilter)
        Dim dateFilter As New FilteringFromTo
        DateFilter.FirstDate = CDate(LastAgendaListSel.Tag)
        DateFilter.FirstDate = DateFilter.FirstDate.AddDays(DateFilter.FirstDate.DayOfWeek * -1)
        DateFilter.SecondDate = LIMIT_DATE
        TPFilter.Add(DateFilter)

        ReportGeneration.StartRapportGeneration("Agenda", TPFilter)
    End Sub

    Private Sub menugeneraterecu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menugeneraterecu.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub

        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)
        rv.generateRecu()
    End Sub

    Public Sub menuabsentnonmotive_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuabsentnonmotive.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        LastAgendaListSel.ClickEnabled = False
        Dim errorMsg As String = rv.changeStatus(Accounts.Clients.Folders.RVsStatus.RVPossibleStatuses.NotPresentNotMotivated)
        If ErrorMsg <> "" AndAlso ErrorMsg.Contains("annulé") = False Then MessageBox.Show(ErrorMsg, "Impossible de changer le statut")
        LastAgendaListSel.ClickEnabled = True
    End Sub

    Public Sub menuacoller_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuacoller.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        Dim rv As AgendaEntry = myMainWin.copyBox.itemValueA
        If lastAgendaListSel.ItemValueA(myNo).ToString <> "" AndAlso Not TypeOf lastAgendaListSel.ItemValueA(myNo) Is RendezVous AndAlso CType(lastAgendaListSel.ItemValueA(myNo), RendezVous).noVisite <> rv.noItemable Then Exit Sub

        Dim curDate As Date = LastAgendaListSel.Tag
        Dim curTime As String = LastAgendaListSel.ItemText(MyNo).Substring(0, 5)
        curDate = New Date(curDate.Year, curDate.Month, curDate.Day, curTime.Substring(0, 2), curTime.Substring(3), 0)

        rv.pasteTo(curDate, Me.noTRP, myMainWin.copyBox.periodeMinutes)
    End Sub

    Public Sub menueffstatus_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menueffstatus.Click
        EnsureItemValuesGood()
        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        LastAgendaListSel.ClickEnabled = False
        Dim errorMsg As String = rv.changeStatus(Accounts.Clients.Folders.RVsStatus.RVPossibleStatuses.Normal)
        If ErrorMsg <> "" AndAlso ErrorMsg.Contains("annulé") = False Then MessageBox.Show(ErrorMsg, "Impossible de changer le statut")
        LastAgendaListSel.ClickEnabled = True
    End Sub

    Public Sub menunewrv_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menunewrv.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected

        If ColorTranslator.ToOle(LastAgendaListSel.ItemBackColor(MyNo)) = ColorTranslator.ToOle(PlageLibre.BackColor) Then
            'Plage libre
            Dim myDate As Date = lastAgendaListSel.Tag
            myDate = New Date(myDate.Year, myDate.Month, myDate.Day, lastAgendaListSel.ItemText(myNo).Substring(0, 2), lastAgendaListSel.ItemText(myNo).Substring(3, 2), 0)
            openNewRV(, Me.noTRP, , , myDate)
        Else
            If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
            Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

            'Plage avec un client
            OpenNewRV(curRV.NoClient, Me.NoTRP, , curRV.NoFolder)
        End If
    End Sub

    Public Sub menuopenaccount_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuopenaccount.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        OpenAccount(curRV.NoClient)
    End Sub

    Public Sub menureserved_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menureserved.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected

        'Droit & Accès
        If CurrentDroitAcces(73) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'ajouter de plages réservées." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myReservedAsk As New ReservedAsk()
        MyReservedAsk.CurrentTRP = UsersManager.getInstance.getUser(Me.NoTRP).ToString()
        MyReservedAsk.DayDate = LastAgendaListSel.Tag
        MyReservedAsk.LineTime = LastAgendaListSel.ItemText(MyNo).Substring(0, 5)
        MyReservedAsk.ListFrom = LastAgendaListSel
        MyReservedAsk.ShowDialog()
    End Sub

    Public Sub menupresent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menupresent.Click
        EnsureItemValuesGood()

        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim rv As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        LastAgendaListSel.ClickEnabled = False
        Dim errorMsg As String = rv.changeStatus(Accounts.Clients.Folders.RVsStatus.RVPossibleStatuses.Present)
        If errorMsg <> "" AndAlso errorMsg.Contains("annulé") = False Then MessageBox.Show(errorMsg, "Impossible de changer le statut", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        LastAgendaListSel.ClickEnabled = True
    End Sub
#End Region

    Private Sub loadTRPHoraire(ByVal curHoraire As Schedule, ByVal indexDay As Integer, ByVal caseDate As Date)
        With DayList(indexDay)
            CaseDate = New Date(CaseDate.Year, CaseDate.Month, CaseDate.Day, 6, 0, 0)
            For H As Integer = 0 To 71
                If curHoraire.IsOpened(CaseDate) Then .ItemBackColor(H - CShort(AdjustByDay(indexDay))) = PlageLibre.BackColor
                CaseDate = CaseDate.AddMinutes(15)
            Next H
        End With
    End Sub

#Region "External functions"
    Public Sub loadAgenda(Optional ByVal loadOnlyH As Byte = 0, Optional ByVal loadOnlyV As Byte = 0, Optional ByRef indexToChange As Byte = 255)
        'REM_CODES
        Dim No, NbDays, indexInDays As Integer
        Dim i, j, Max_j, Max_i, Min_j, Min_i, n, H, Saut, noLine As Short
        Dim myTTT As String
        Dim MyDate, caseDateInv As Date
        Dim AL, ARV, AP, APP, selfOpened As Boolean
        SelfOpened = False

        If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
            Min_i = 0
            Min_j = 0
            Max_i = Me.NoVertical - 1
            Max_j = Me.NoHorizontal - 1
            n = 0
            NbDays = Me.NoVertical * Me.NoHorizontal
        Else
            Min_i = LoadOnlyH - 1
            Max_i = LoadOnlyH - 1
            Min_j = LoadOnlyV - 1
            Max_j = LoadOnlyV - 1
            If IndexToChange = 255 Then
                n = Min_i * (Me.NoVertical) + Max_j
            Else
                n = IndexToChange
            End If
            NbDays = 1
        End If

        Dim itemText As String

        If DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpened = True

        MyWeek = LIMIT_DATE
        Dim curHoraire As Schedule = SchedulesManager.getInstance.getDefaultSchedule(Me.noTRP)
        If curHoraire Is Nothing Then
            If MessageBox.Show("Il n'existe pas d'horaire par défaut pour ce thérapeute." & vbCrLf & "Voulez-vous en créer une ?", "Aucune horaire par défaut", MessageBoxButtons.YesNo) = DialogResult.No Then
                Me.Chargement.Text = "Chargement annulé."
                PrematureEnding = True
                Exit Sub
            Else
                Me.Close()
                OpenModifHoraire(Me.NoTRP)
                PrematureEnding = True
                Exit Sub
            End If
        End If

        'Loading RV
        Dim FirstDate, lastDate As Date
        FirstDate = CDate(DayList(n).Tag)
        LastDate = FirstDate.AddDays(NbDays - 1)

        Dim curTRP As User = UsersManager.getInstance.getUser(NoTRP)
        Dim trpStartingDate As Date = curTRP.startingDate
        Dim trpEndingDate As Date = curTRP.endingDate
        Dim entries As Generic.List(Of AgendaEntry) = AgendaManager.getInstance.loadEntries(noTRP, FirstDate, lastDate, False)
        Dim curFolderCode As Accounts.Clients.Folders.Codifications.FolderCode

        'Load Therapist Data
        For i = Min_i To Max_i
            For j = Min_j To Max_j
                AL = False : AP = False : ARV = False : APP = False

                DayList.ValueA(n) = CShort(AdjustByDay(n))
                CaseDateInv = CDate(DayList(n).Tag)

                'Horaire spécifique utilisateur
                No = CaseDateInv.DayOfWeek * -1
                MyDate = CaseDateInv.AddDays(No)
                If Not MyWeek = MyDate Then
                    curHoraire = SchedulesManager.getInstance.getSchedule(Me.NoTRP, MyDate)
                    MyWeek = curHoraire.scheduleDate
                End If

                With DayList(n)
                    If Date1InfDate2(trpStartingDate, CaseDateInv, True) AndAlso (trpEndingDate = LIMIT_DATE OrElse Date1InfDate2(CaseDateInv, trpEndingDate, True)) Then LoadTRPHoraire(curHoraire, n, CaseDateInv)
                    MyDate = CaseDateInv
                    If entries.Count = 0 Then GoTo PassLoading

                    Dim newAgendaEntry As AgendaEntry

                    For H = IndexInDays To entries.Count - 1
                        Saut = 0

                        newAgendaEntry = entries(H)

                        If newAgendaEntry.DateHeure.Date <> MyDate.Date Then Exit For
                        If newAgendaEntry.NoTRP = Me.NoTRP Then
                            noLine = dayList(n).findString(DateFormat.getTextDate(newAgendaEntry.dateHeure, DateFormat.TextDateOptions.ShortTime), , , True)
                            itemText = DateFormat.getTextDate(newAgendaEntry.dateHeure, DateFormat.TextDateOptions.ShortTime) & " "
                            .tieItem(newAgendaEntry.period / 15, noLine)

                            If TypeOf newAgendaEntry Is AgendaEntryBlocked Then
                                .ItemBackColor(noLine) = plageBloquee.BackColor
                            ElseIf TypeOf newAgendaEntry Is AgendaEntryReserved Then
                                .ItemBackColor(noLine) = plageReservee.BackColor
                            Else
                                Dim rv As RendezVous = newAgendaEntry
                                curFolderCode = rv.getFolderCode()

                                'Testing bug
                                If curFolderCode Is Nothing Then
                                    addErrorLog(New Exception("curFolderCode Is Nothing=" & (curFolderCode Is Nothing) & ",FolderCodesManager.getInstance.count=" & Clinica.Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.count))
                                    .ItemText(noLine) = "Problème de chargement en cours de correction." & vbCrLf & "Veuillez ne pas utiliser cette case. Recharger l'agenda peut aider."
                                    .ItemClickable(noLine) = False
                                    Continue For
                                Else
                                    .ItemClickable(noLine) = True
                                End If

                                'Manage icons to show
                                Dim iconsShowed As New Generic.List(Of Boolean)(.icons.Count)
                                For iIcon As Integer = 0 To .icons.Count - 1
                                    iconsShowed.Add(False)
                                Next iIcon
                                'Modifie la case selon les settings des préférences si le RV n'est pas confirmé et qu'il requiert de l'être dépendamment de la codification et si le RV est une évaluation ou non
                                If rv.confirmed = False AndAlso ((curFolderCode.confirmation = 2 And rv.evaluation = False) Or (curFolderCode.confirmation = 1 And rv.evaluation = True) Or curFolderCode.confirmation = 3) Then
                                    iconsShowed(0) = True
                                End If

                                iconsShowed(1) = rv.isOnQueueList()
                                iconsShowed(2) = rv.remarksClient.Trim <> ""
                                iconsShowed(3) = rv.remarksFolder.Trim <> ""
                                iconsShowed(4) = rv.remarks.Trim <> ""

                                Try
                                    'Trying to understand the bug : "Index was out of range. Must be non-negative and less than the size of the collection."
                                    .items(noLine).getItems(0).iconsShowed = iconsShowed
                                Catch ex As Exception
                                    Dim testString As String = "noLine=" & noLine & ",.items.Count=" & .items.Count
                                    Try
                                        testString &= ",.items(noLine).getItems.Count=" & .items(noLine).getItems.Count
                                    Catch ex2 As Exception
                                    End Try

                                    addErrorLog(New Exception(testString, ex))
                                    'don't throw back error as it's not severe, otherwise, will break a few lines below
                                End Try

                                'Change Eval text appearance
                                If rv.evaluation Then
                                    Dim curFS As FontStyle = FontStyle.Regular
                                    If PreferencesManager.getGeneralPreferences()("RVEvalGras") Then curFS += FontStyle.Bold
                                    If PreferencesManager.getGeneralPreferences()("RVEvalItalique") Then curFS += FontStyle.Italic
                                    If PreferencesManager.getGeneralPreferences()("RVEvalSouligne") Then curFS += FontStyle.Underline
                                    If PreferencesManager.getGeneralPreferences()("RVEvalBarre") Then curFS += FontStyle.Strikeout

                                    .ItemFont(noLine) = New Font(PreferencesManager.getGeneralPreferences()("RVEvalFont").ToString, .baseFont.Size, curFS)
                                End If


                                'Manage Indicators
                                If rv.noStatut = 4 Then
                                    If rv.isBillPaid = False Then
                                        .ItemBackColor(noLine) = plagePresence.BackColor
                                        AP = True
                                    Else
                                        .ItemBackColor(noLine) = plagePresencePayee.BackColor
                                        APP = True
                                    End If
                                Else
                                    .ItemBackColor(noLine) = plageRV.BackColor
                                    ARV = True
                                End If
                            End If

                            .ItemValueA(NoLine) = newAgendaEntry

                            Dim specialDatesTT As String = ""
                            If PreferencesManager.getGeneralPreferences()("AffSpecialDatesInAgenda") = True Then
                                Dim sd As Generic.List(Of SpecialDate) = SpecialDatesManager.getInstance.GetSpecialDates(MyDate)
                                If sd.Count > 0 Then
                                    .ForeColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorSpecialDates"))
                                    SpecialDatesTT = " " & sd(0).Nom
                                    For k As Integer = 1 To sd.Count - 1
                                        SpecialDatesTT &= " - " & sd(k).Nom
                                    Next k
                                End If
                            End If
                            MyTTT = newAgendaEntry.ItemText
                            If Not TypeOf newAgendaEntry Is RendezVous Then
                                myTTT = DateFormat.getTextDate(caseDateInv, DateFormat.TextDateOptions.FullDayMonthNames) & specialDatesTT & vbCrLf & myTTT
                            Else
                                Dim evalTrait As String = "Évaluation"
                                Dim rv As RendezVous = newAgendaEntry
                                If rv.evaluation = False Then evalTrait = "Traitement"
                                Dim frequency As String = Accounts.Clients.Folders.ClientFolder.frequencies(rv.folderFrequency)
                                myTTT = DateFormat.getTextDate(caseDateInv, DateFormat.TextDateOptions.FullDayMonthNames) & specialDatesTT & vbCrLf & myTTT & vbCrLf & evalTrait & " de " & rv.service & " du dossier (" & rv.noFolder & "-" & curFolderCode.name & ")" & vbCrLf & "Fréquence : " & frequency & vbCrLf & rv.telephones.Replace("§", vbCrLf)
                                If rv.remarksClient.Trim <> "" Then MyTTT &= vbCrLf & vbCrLf & "Remarques du compte client :" & vbCrLf & rv.remarksClient.Replace("\n", vbCrLf)
                                If rv.remarksFolder.Trim <> "" Then MyTTT &= vbCrLf & vbCrLf & "Remarques du dossier :" & vbCrLf & rv.remarksFolder.Replace("\n", vbCrLf)
                                If rv.remarks.Trim <> "" Then myTTT &= vbCrLf & vbCrLf & "Remarques du rendez-vous :" & vbCrLf & rv.remarks
                            End If
                            MyTTT = (ItemText & MyTTT).Replace("<br>", vbCrLf)
                            ItemText = (ItemText & newAgendaEntry.ItemText).Replace("<br>", vbCrLf)
                            .ItemText(NoLine) = ItemText

                            If PreferencesManager.getUserPreferences()("InfoBulleClient") = False Then
                                .ItemToolTipText(NoLine) = MyTTT
                            Else
                                .ItemToolTipText(NoLine) = ""
                            End If
                        End If

                        IndexInDays += 1
                    Next H

PassLoading:
                    For H = 0 To .ListCount - 1
                        If ColorTranslator.ToOle(.ItemBackColor(H)) = ColorTranslator.ToOle(PlageLibre.BackColor) Then AL = True : Exit For
                    Next H

                    If Date1InfDate2(CaseDateInv, Date.Today) = False Then
                        CType(Feux(n), LightSignals).ActiveLibre = AL
                    Else
                        If ARV = False And AP = False And APP = False Then
                            CType(Feux(n), LightSignals).ActiveLibre = AL
                        Else
                            CType(Feux(n), LightSignals).ActiveLibre = False
                        End If
                    End If
                    CType(Feux(n), LightSignals).ActivePresence = AP
                    CType(Feux(n), LightSignals).ActiveRV = ARV
                    CType(Feux(n), LightSignals).ActivePresencePaye = APP
                    CType(Feux(n), LightSignals).Coloring()
                    .Draw = True
                    .draw = False
                    .showItem(findFirstTime(n), CI.Controls.List.PosType.Top)
                End With

                If LoadOnlyH = 0 Or LoadOnlyV = 0 Then n = n + 1 + Saut : Chargement.Text &= "." : Application.DoEvents()
            Next j
        Next i

        If SelfOpened = True Then DBLinker.GetInstance().DBConnected = False

        If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
            SetVisibility(Me)
            For i = 0 To (Me.NoVertical * Me.NoHorizontal) - 1
                DayList(i).Visible = True
                DayList(i).ClickEnabled = True
            Next i

            Chargement.Visible = False

            pdateday.Enabled = True
            adateday.Enabled = True
            pdateweek.Enabled = True
            adateweek.Enabled = True
            pdatemonth.Enabled = True
            adatemonth.Enabled = True
            choosedate.Enabled = True
        Else
            DayList(n).ClickEnabled = True

            If lastAgendaItemClicked <> -1 AndAlso lastAgendaListSel Is dayList(n) Then
                dayList(n).selected = lastAgendaItemClicked
                dayList(n).showItem(lastAgendaItemClicked)
            End If
            If LastAgendaListSel IsNot Nothing Then LastAgendaListSel.ClickEnabled = True
        End If
    End Sub

    Public Function getNbDays() As Integer
        Return (Me.NoHorizontal - 1) * 7 + Me.NoVertical
    End Function

    Public Sub updateStructure(ByVal trp As Integer)
        Dim smenutherapeute() As String

        If AgendaLoaded = False Then Exit Sub
        Dim i As Short
        CommandsHolder.GetInstance.NewAgenda.SetEnability(False)

        For i = 0 To MyMainWin.menutherapeute.DropDownItems.Count - 1
            Smenutherapeute = System.Text.RegularExpressions.Regex.Split(MyMainWin.menutherapeute.DropDownItems(i).Text, " \(")
            If Smenutherapeute(1).Substring(0, Smenutherapeute(1).Length - 1) = TRP Then
                CType(MyMainWin.menutherapeute.DropDownItems(i), ToolStripMenuItem).Checked = True
                MyMainWin.menutherapeute.DropDownItems(i).Enabled = False
            Else
                CType(MyMainWin.menutherapeute.DropDownItems(i), ToolStripMenuItem).Checked = False
            End If
        Next i
        Me.NoTRP = TRP

        UncheckedPeriode(Me)
        If MyMainWin.menumois.Checked = True Then
            Me.NoHorizontal = 6
            Me.NoVertical = 7
            menumois.Checked = True
        Else
            For i = 1 To 4
                If MyMainWin.menusemaine(i).Checked = True Then
                    menusemaine(i).Checked = True
                    Me.NoHorizontal = i
                    Me.NoVertical = 7
                    GoTo Jump
                End If
            Next i

            For i = 1 To 6
                If MyMainWin.menujour(i).Checked = True Then
                    Me.NoHorizontal = 1
                    menujour(i).Checked = True
                    Me.NoVertical = i
                    Exit For
                End If
            Next i
        End If

Jump:

        Dim loadingTime As Double = DateTime.Now.Ticks
        Dim selfOpened As Boolean = False

        With Me
            .SuspendLayout()
            If DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpened = True
            .BuildStructure(.NoVertical, .NoHorizontal, .DebutDate)
            If PrematureEnding = True Then GoTo EndNow
            .LoadAgenda()
            If PrematureEnding = True Then GoTo EndNow
            If CurrentUserName = "Administrateur" Then
                LoadingTime = DateTime.Now.Ticks - LoadingTime
                LoadingTime /= 10000000 'To get in seconds
                .TL.Text = "Time to load->" & LoadingTime : .TL.Visible = True
            End If
EndNow:
            PrematureEnding = False
            If SelfOpened = True Then DBLinker.GetInstance().DBConnected = False
            .ResumeLayout()
        End With
        CommandsHolder.GetInstance.NewAgenda.SetEnability(True)
    End Sub

    Public Sub buildStructure(ByRef horizontal As Byte, ByRef vertical As Byte, Optional ByVal todaysDate As Date = LIMIT_DATE, Optional ByRef bgColorOnOff As String = "", Optional ByVal loadOnlyH As Byte = 0, Optional ByVal loadOnlyV As Byte = 0, Optional ByRef indexToChange As Byte = 255)
        Me.VerticalScroll.Value = 0
        Me.HorizontalScroll.Value = 0

        Dim i, j, n, H, Max_i, Max_j, Min_j, Min_i, n2, tMax As Short
        Dim SFTime(), ETime, FTime, MyTT, curNoLine As String
        Dim spaceForDate As Byte
        Dim SquareHeight, SquareWidth, lineWidth As Integer
        Dim myDate As Date
        Dim selfOpened As Boolean = False

        If TodaysDate.Year = 9999 Or TodaysDate.Year.ToString = "1" Then TodaysDate = Date.Today
        If Vertical = 0 Or Horizontal = 0 Then Exit Sub

        If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
            Min_i = 0
            Min_j = 0
            Max_i = Vertical - 1
            Max_j = Horizontal - 1
            n = 0
        Else
            Min_i = LoadOnlyH - 1
            Max_i = LoadOnlyH - 1
            Min_j = LoadOnlyV - 1
            Max_j = LoadOnlyV - 1
            If IndexToChange = 255 Then
                n = Min_i * (Vertical) + Max_j
            Else
                n = IndexToChange
            End If
        End If

        'MinimumSpace : Espace minimale entre l'agenda et le bord de la fenêtre
        'YDephase : Déphase de l'agenda (Espace du haut)
        SpaceForDate = SDate.Text
        LineWidth = CInt(LWidth.Text)

        If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
            MaxBox.Height = DaysRectangle.Height
            MaxBox.Top = DaysRectangle.Top
        End If

        If Vertical = 1 Then MaxBox.Visible = False

        If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
            'Lignes
            n = 0
            For i = 1 To Horizontal + 1
                With CType(Lignes(n), Label)
                    .Left = DaysRectangle.Left + ((i - 1) * DaysRectangle.Width / Horizontal)
                    .Width = 1
                    .Top = DaysRectangle.Top
                    .Height = DaysRectangle.Height
                    .Tag = ""
                    n = n + 1
                End With
            Next i

            For i = 1 To Vertical + 1
                With CType(Lignes(n), Label)
                    .Left = DaysRectangle.Left
                    .Width = DaysRectangle.Width
                    .Top = DaysRectangle.Top + ((i - 1) * DaysRectangle.Height / Vertical)
                    .Height = 1
                    .Tag = ""
                    n = n + 1
                End With
            Next i

            For i = n To 14
                Lignes(i).Visible = False
                Lignes(i).Tag = "NOHIDE"
            Next i
            n = 0

            'Set to invisible all DayList
            For i = 0 To 41
                DayList(i).Visible = False
                DayList(i).Tag = "NOHIDE"
                LabelsNbDays(i).visible = False
                LabelsNbDays(i).tag = "NOHIDE"
                MaxMin(i).visible = False
                MaxMin(i).tag = "NOHIDE"
                Feux(i).visible = False
                Feux(i).tag = "NOHIDE"
            Next i
            SetVisibility(Me, False)

            pdateday.Enabled = False
            adateday.Enabled = False
            pdateweek.Enabled = False
            adateweek.Enabled = False
            pdatemonth.Enabled = False
            adatemonth.Enabled = False
            choosedate.Enabled = False
            Me.MaxBox.Visible = False
            With Chargement
                .Text = "Chargement en cours ..."
                .Left = (DaysRectangle.Width - .Width) / 2 + DaysRectangle.Left
                .Top = (DaysRectangle.Height - .Height) / 2 + DaysRectangle.Top
                .Visible = True
            End With
        End If

        If DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpened = True

        'Date & DayList Charging
        MyWeek = LIMIT_DATE
        Dim curHoraire As Schedule = SchedulesManager.getInstance.getDefaultSchedule(0)
        If curHoraire Is Nothing Then
            If MessageBox.Show("Il n'existe pas d'horaire par défaut pour la clinique." & vbCrLf & "Voulez-vous en créer une ?", "Aucune horaire par défaut", MessageBoxButtons.YesNo) = DialogResult.No Then
                Me.Chargement.Text = "Chargement annulé."
                PrematureEnding = True
                Exit Sub
            Else
                PrematureClosing = True
                OpenModifHoraire(0)
                PrematureEnding = True
                Exit Sub
            End If
        End If

        Dim myPos As PositionType
        For i = Min_i To Max_i
            For j = Min_j To Max_j
                MyTT = ""
                If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
                    MyPos.Y = CType(Lignes(Horizontal + 1 + i), Label).Top
                    MyPos.X = CType(Lignes(j), Label).Left

                    SquareWidth = CType(Lignes(j + 1), Label).Left - MyPos.X + 1
                    SquareHeight = CType(Lignes(Horizontal + 2 + i), Label).Top - MyPos.Y + 1
                    n2 = n
                Else
                    n2 = 0
                End If

                'Date
                MyDate = TodaysDate.AddDays(n2)
                With CType(LabelsNbDays(n), Label)
                    .Tag = ""
                    .Text = DateFormat.getTextDate(myDate, DateFormat.TextDateOptions.ShortDayNameDDMMYY, "")
                    If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
                        .Left = MyPos.X + SpaceForDate
                        .Top = MyPos.Y + SpaceForDate
                    End If
                    .ForeColor = Color.Black
                    If PreferencesManager.getGeneralPreferences()("AffSpecialDatesInAgenda") = True Then
                        Dim sd As Generic.List(Of SpecialDate) = SpecialDatesManager.GetInstance.GetSpecialDates(MyDate)
                        If sd.Count > 0 Then
                            .ForeColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorSpecialDates"))
                            MyTT = " " & sd(0).Nom
                            For k As Integer = 1 To sd.Count - 1
                                MyTT &= " - " & sd(k).Nom
                            Next k
                        End If
                    End If
                End With

                'Horaire spécifique clinique
                If Not MyWeek = MyDate.AddDays(MyDate.DayOfWeek * -1).Date Then
                    curHoraire = SchedulesManager.GetInstance.getSchedule(0, MyDate.AddDays(MyDate.DayOfWeek * -1).Date)
                    MyWeek = curHoraire.scheduleDate
                End If

                'Maximiser
                If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
                    With MaxMin(n)
                        If Me.NoHorizontal > 1 OrElse Me.NoVertical > 3 Then .tag = ""
                        .Left = (MyPos.X + SquareWidth - (MaxMin(n).Width) - SpaceForDate + 1)
                        .Top = (MyPos.Y + SpaceForDate - 1)
                    End With
                End If

                'Feux indicateur
                If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
                    With CType(Feux(n), LightSignals)
                        .Left = (MyPos.X + SquareWidth - (MaxMin(n).Width) - SpaceForDate * 2 - .Width + 3)
                        .Top = (MyPos.Y + SpaceForDate - 1)
                        .Tag = ""
                    End With
                End If

                'Case de la journée
                With DayList(n)
                    .Cls()
                    If DayList.Configed(n) = False Then
                        ConfigList(DayList(n))
                        DayList.Configed(n) = True

                        .BaseBackColor = PlageClinique.BackColor
                        .BGColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors1"))

                        .BorderStyle = CI.Controls.List.BSType.FixedSingle
                        .HScrollForeColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors2"))
                        .VScrollForeColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors2"))
                        .HScrollColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors3"))
                        .VScrollColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors3"))
                        .BorderSelColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors4"))
                        .BorderColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors5"))
                        .BaseForeColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("AListColors6"))
                        .BaseFont = New Font(.BaseFont.FontFamily, CShort(PreferencesManager.getUserPreferences()("AgendaFontSize")), .BaseFont.Style)
                    End If

                    FTime = curHoraire.findTime(Schedule.WTType.First, myDate)
                    ETime = curHoraire.findTime(Schedule.WTType.Last, myDate)
                    If FTime = String.Empty Then
                        tMax = -1
                    Else
                        tMax = (CDbl(ETime.Substring(0, 2)) - CDbl(FTime.Substring(0, 2))) * 60 + ETime.Substring(3, 2) - FTime.Substring(3, 2)
                    End If

                    'Ajustement pour les données
                    If TMax > -1 Then
                        SFTime = FTime.Split(":")
                        AdjustByDay(n) = (SFTime(0) - 6) * 4 + SFTime(1) / 15
                    End If

                    If TMax = -1 And n < 42 Then MaxMin(n).Tag = "NOHIDE" : MaxMin(n).Visible = False ': .BorderStyle = NoBorder
                    MyTT = DateFormat.getTextDate(todaysDate.AddDays(n2), DateFormat.TextDateOptions.FullDayMonthNames) & MyTT

                    If TMax <> -1 AndAlso .Maximum = -1 Then
                        For H = 0 To TMax Step 15
                            curNoLine = .add(CDate(FTime).AddMinutes(H).ToString("HH:mm"))
                            .ItemValueA(CurNoLine) = ""
                            .ItemValueB(CurNoLine) = ""
                            If PreferencesManager.getUserPreferences()("InfoBulleDate") = False Then .ItemToolTipText(CurNoLine) = .ItemText(CurNoLine) & " " & MyTT
                        Next H
                        .ToolTipText = ""
                    Else
                        If PreferencesManager.getUserPreferences()("InfoBulleDate") = False Then
                            .ToolTipText = MyTT
                        Else
                            .ToolTipText = ""
                        End If
                    End If

                    If LoadOnlyH = 0 Or LoadOnlyV = 0 Then
                        .Left = (MyPos.X + CDbl(LWidth.Text))
                        .Top = (MyPos.Y + SpaceForDate + (LabelsNbDays(n).Height))
                        .Height = SquareHeight + MyPos.Y - .Top
                        .Width = SquareWidth
                        .Tag = TodaysDate.AddDays(n)
                    End If
                End With
                n += 1
                If LoadOnlyH = 0 Or LoadOnlyV = 0 Then Chargement.Text &= "." : Chargement.Left -= 3 : Application.DoEvents()
            Next j
        Next i

        If SelfOpened = True Then DBLinker.GetInstance().DBConnected = False
    End Sub
#End Region

#Region "DayList Events"
    Private Sub dayList_Show_Renamed(ByVal sender As Object, ByVal e As EventArgs)
        'Auto adjust scroll bar position to first therapist time
        DayList(DayList.IndexOf(sender)).ShowItem(FindFirstTime(DayList.IndexOf(sender)), CI.Controls.List.PosType.Top)
    End Sub

    Private Sub dayList_Click(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs)
        If Not (CType(sender, CI.Controls.List).ClickEnabled = False Or e.SelectedItem < 0) And e.Button = 2 Then
            LastAgendaListSel = sender
            If e.SelectedItem = LastAgendaListSel.Selected Then ManageContextMenu(e.SelectedItem)
            menuContextMenu.showDropDown(True)
        End If
        LastAgendaItemClicked = e.SelectedItem
    End Sub

    Private Sub dayList_ClickEnabledChange(ByVal sender As Object, ByVal e As EventArgs)
        Dim curList As CI.Controls.List = CType(sender, CI.Controls.List)
        Dim i As Integer
        For i = 0 To menuContextMenu.DropDownItems.Count - 1
            menuContextMenu.DropDownItems(i).Enabled = CurList.ClickEnabled
        Next i
    End Sub

    Private Sub dayList_DblClick(ByVal sender As Object, ByVal e As CI.Controls.List.DblClickEventArgs)
        Dim curList As CI.Controls.List = CType(sender, CI.Controls.List)
        If CurList.ClickEnabled = False Or e.SelectedItem < 0 Or e.Button <> 1 Then Exit Sub
        Select Case ColorTranslator.ToOle(CurList.ItemBackColor(e.SelectedItem))
            Case ColorTranslator.ToOle(PlageClinique.BackColor)
                'Case Clinique
                If Date1InfDate2(CurList.Tag, Date.Today) = False Then
                    If e.SelectedItem > FindFirstTime(DayList.IndexOf(CurList)) And e.SelectedItem < FindLastTime(DayList.IndexOf(CurList)) Then
                        menuOpenHoraire_Click(sender, EventArgs.Empty)
                    Else
                        menuOpenHoraireUpto_Click(sender, EventArgs.Empty)
                    End If
                End If

            Case ColorTranslator.ToOle(PlageLibre.BackColor)
                'Case Libre
                If Date1InfDate2(CurList.Tag, Date.Today) = False Then menunewrv_Click(sender, EventArgs.Empty)

            Case ColorTranslator.ToOle(PlageBloquee.BackColor)
                'Case Bloquée
                If Date1InfDate2(CurList.Tag, Date.Today) = False Then
                    menuContinuerLaListeDattente_Click(sender, EventArgs.Empty)
                Else
                    menuaenlever_Click(sender, EventArgs.Empty)
                End If

            Case ColorTranslator.ToOle(PlageReservee.BackColor)
                'Case Réservée
                If Date1InfDate2(CurList.Tag, Date.Today) = False Then menuModifyReserved_Click(sender, EventArgs.Empty)

            Case Else
                'Case Compte client
                menuopenaccount_Click(sender, EventArgs.Empty)
        End Select
    End Sub

    Private Sub dayList_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        scrollAllLists = e.Control
    End Sub

    Private Sub dayList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim curIndex As Integer = DayList.IndexOf(sender)
        If CurIndex < 0 Then Exit Sub
        Dim oldIndex As Integer = CurIndex
        Dim curList As CI.Controls.List = CType(sender, CI.Controls.List)
        If CurList.Selected < 0 Then Exit Sub
        Dim doDayNLineSelection As Boolean = False
        Dim itemPosition As CI.Controls.List.PosType = CI.Controls.List.PosType.BetterGuess

        scrollAllLists = e.Control

        Select Case e.KeyCode
            Case Keys.Down  'BAS
                If e.Shift Then
                    If CurIndex = 42 And (MaximiseIndex + Me.NoVertical) < 42 Then
                        If DayList(MaximiseIndex + Me.NoVertical).ListCount > 0 And DayList(MaximiseIndex + Me.NoVertical).Visible = True Then MaximiseIndex += Me.NoVertical : MaximiseDay(MaximiseIndex)
                    ElseIf (CurIndex + Me.NoVertical) < 42 Then
                        If DayList(CurIndex + Me.NoVertical).ListCount > 0 Then CurIndex += Me.NoVertical
                        DoDayNLineSelection = True
                    End If
                ElseIf e.Control Then
                    curList.selected = findLastTime(curIndex)
                    itemPosition = CI.Controls.List.PosType.Bottom
                    DoDayNLineSelection = True
                Else
                    CurList.Selected = FindNextTime(CurIndex, CurList.Selected)
                    DoDayNLineSelection = True
                End If
            Case Keys.Right  'DROIT
                If CurIndex = 42 And (MaximiseIndex + 1) < 42 Then
                    If DayList(MaximiseIndex + 1).ListCount > 0 And DayList(MaximiseIndex + 1).Visible = True Then MaximiseIndex += 1 : MaximiseDay(MaximiseIndex)
                ElseIf CurIndex < 41 Then
                    If DayList(CurIndex + 1).ListCount > 0 Then CurIndex += 1
                End If
                DoDayNLineSelection = True
            Case Keys.Up   'HAUT
                If e.Shift Then
                    If CurIndex = 42 And (MaximiseIndex - Me.NoVertical) >= 0 Then
                        If DayList(MaximiseIndex - Me.NoVertical).ListCount > 0 And DayList(MaximiseIndex - Me.NoVertical).Visible = True Then MaximiseIndex -= Me.NoVertical : MaximiseDay(MaximiseIndex)
                    ElseIf (CurIndex - Me.NoVertical) >= 0 And CurIndex < 42 Then
                        If DayList(CurIndex - Me.NoVertical).ListCount > 0 Then CurIndex -= Me.NoVertical
                        DoDayNLineSelection = True
                    End If
                ElseIf e.Control Then
                    curList.selected = findFirstTime(curIndex)
                    itemPosition = CI.Controls.List.PosType.Top
                    DoDayNLineSelection = True
                Else
                    CurList.Selected = FindPreviousTime(CurIndex, CurList.Selected)
                    DoDayNLineSelection = True
                End If
            Case Keys.Left  'GAUCHE
                If CurIndex = 42 And (MaximiseIndex - 1) >= 0 Then
                    If DayList(MaximiseIndex - 1).ListCount > 0 And DayList(MaximiseIndex - 1).Visible = True Then MaximiseIndex -= 1 : MaximiseDay(MaximiseIndex)
                ElseIf CurIndex > 0 Then
                    If DayList(CurIndex - 1).ListCount > 0 Then CurIndex -= 1
                    DoDayNLineSelection = True
                End If
            Case Keys.Enter
                DayList_DblClick(sender, New CI.Controls.List.DblClickEventArgs(CurList.Selected, 1, 0, 0))
                e.Handled = True
        End Select

        If doDayNLineSelection Then
            scrollAllLists = False

            e.Handled = True
            If curIndex <> oldIndex Then
                If dayList(curIndex).Visible Then
                    Dim oldSelected As Integer = dayList(oldIndex).selected
                    dayList(curIndex).Focus()
                    dayList(curIndex).selected = dayList(curIndex).findString(dayList(oldIndex).ItemText(oldSelected).Substring(0, 5), , , True)
                    If dayList(curIndex).selected = -1 Then
                        Dim minIndexDiff, maxIndexDiff As Integer
                        minIndexDiff = Math.Abs(dayList(curIndex).findFirstItem - oldSelected)
                        maxIndexDiff = Math.Abs(dayList(curIndex).findLastItem - oldSelected)
                        If minIndexDiff < maxIndexDiff Then
                            dayList(curIndex).selected = dayList(curIndex).findFirstItem
                        Else
                            dayList(curIndex).selected = dayList(curIndex).findLastItem
                        End If
                    End If
                End If
            End If
            If dayList(curIndex).selected > -1 Then dayList(curIndex).showItem(dayList(curIndex).selected, itemPosition)
        End If
    End Sub

    Private Sub dayList_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If MyMainWin.BarMainObjects.IsMovingPanel = False Then MyMainWin.BarMainObjects.HideBar()
    End Sub

    Private Sub dayList_MouseDown(ByVal sender As Object, ByVal e As CI.Controls.List.MouseDownEventArgs)
        MouseButton = e.Button
    End Sub

    Private Sub dayList_MouseUp(ByVal sender As Object, ByVal e As CI.Controls.List.MouseUpEventArgs)
        MouseButton = 0
    End Sub

    Private Sub dayList_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If MouseButton = 4 OrElse Control.ModifierKeys = Keys.Control Then
            Dim i As Short
            Dim curVSValue As Short = CType(sender, CI.Controls.List).VSValue

            For i = 0 To Me.NoHorizontal * Me.NoVertical - 1
                If sender.Equals(DayList(i)) = False Then DayList(i).VSValue = CurVSValue
            Next i
        End If
    End Sub

    Private Sub dayList_HScrollScroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
    End Sub

    Private Sub dayList_VScrollScroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
        If scrollAllLists Then
            'TODO : What the hell was this line for ? Quite sure causing problem !!
            'If LastAgendaListSel IsNot Nothing AndAlso CType(Sender, CI.Controls.ListScrollBar).Parent.Equals(LastAgendaListSel) = True AndAlso LastAgendaListSel.Selected <> -1 Then LastAgendaListSel.Selected = -1
            Dim i As Short
            Dim curVSValue As Short = CType(sender, CI.Controls.ListScrollBar).value

            For i = 0 To Me.NoHorizontal * Me.NoVertical - 1
                If sender.Equals(dayList(i)) = False Then dayList(i).vsValue = curVSValue
            Next i
        End If
    End Sub

    Private Sub dayList_WillSelect(ByVal sender As Object, ByVal e As CI.Controls.List.WillSelectEventArgs)
        Dim index As Integer = DayList.indexOf(sender)

        'REM Should be to adjust scrollbar of Agenda window when changing DayList Item
        Dim diffHeight As Integer = (myMainWin.minimumMdiChildRectangle.Height) - Me.Height
        If DayList(Index).Selected <> -1 Then
            If Me.NoHorizontal = 1 OrElse Index = 42 Then
                Dim pourcent As Double = (DayList(Index).ItemRelativeTop(DayList(Index).Selected) / DayList(Index).Height)
                If Pourcent > 1 Then Pourcent = 1
                If Pourcent < 0 Then Pourcent = 0
                Me.VerticalScroll.Value = Pourcent * Me.VerticalScroll.Maximum ' - DiffHeight) '+ DayList(Index).Top ' (DayList(Index).VSValue / (DayList(Index).VSValueMax)) * Me.VerticalScroll.Maximum - DayList(Index).Top
            Else
                Me.ScrollControlIntoView(Me.LabelsNbDays.Item(Index))
            End If
        End If
        '(DayList(Index).Selected / (DayList(Index).ListCount + 1)) * Me.VerticalScroll.Maximum - DayList(Index).Top

        If Index < 42 Then
            Dim i As Integer
            For i = 0 To 41
                If DayList(i).Visible = True And Not Index = i And DayList(i).Selected > -1 Then DayList(i).Selected = -1
            Next i
        End If
        If e.SelectedItem <> -1 Then LastAgendaListSel = DayList(Index)

        ManageContextMenu(e.SelectedItem)
    End Sub
#End Region

#Region "Internal functions"
    Private Sub startClientRapportGen(ByVal rapportTitle As String, Optional ByVal useNoFolder As Boolean = True)
        Dim myNo As Short = LastAgendaListSel.Selected
        If Not TypeOf LastAgendaListSel.ItemValueA(MyNo) Is RendezVous Then Exit Sub
        Dim curRV As RendezVous = LastAgendaListSel.ItemValueA(MyNo)

        Dim tpFilter As New FilteringComposite
        Dim noClientFilter As New FilteringNoClient
        NoClientFilter.NoClient = curRV.NoClient
        If UseNoFolder Then NoClientFilter.NoFolder = curRV.NoFolder
        NoClientFilter.ClientFullName = curRV.ClientName
        TPFilter.Add(NoClientFilter)

        ReportGeneration.StartRapportGeneration(RapportTitle, TPFilter)
    End Sub

    Private Sub buildMenuRapport()
        AddHandler Me.menuRapports.DropDownItems.Add("Compte client détaillé").Click, AddressOf menuRapportWOFolder_Click
        AddHandler Me.menuRapports.DropDownItems.Add("Demande d'autorisation d'un dossier").Click, AddressOf menuRapportWithFolder_Click
        AddHandler Me.menuRapports.DropDownItems.Add("Dossier détaillé").Click, AddressOf menuRapportWithFolder_Click
        AddHandler Me.menuRapports.DropDownItems.Add("État de compte : compte client").Click, AddressOf menuRapportWOFolder_Click
        AddHandler Me.menuRapports.DropDownItems.Add("État de compte : dossier client").Click, AddressOf menuRapportWithFolder_Click
        AddHandler Me.menuRapports.DropDownItems.Add("Liste de reçus déjà générés").Click, AddressOf menuRapportWithFolder_Click
        AddHandler Me.menuRapports.DropDownItems.Add("Liste des rendez-vous d'un dossier").Click, AddressOf menuRapportWithFolder_Click
        AddHandler Me.menuRapports.DropDownItems.Add("Rendez-vous futurs d'un client").Click, AddressOf menuRapportWOFolder_Click
    End Sub

    Private Sub resetContextMenu()
        _ResetContextMenu(menuContextMenu)
    End Sub

    Private Sub _ResetContextMenu(ByVal obj As ToolStripMenuItem)
        'Reset all the menu
        For i As Integer = 0 To obj.DropDownItems.Count - 1
            obj.DropDownItems(i).Visible = True
            obj.DropDownItems(i).Enabled = True
            If TypeOf obj.DropDownItems(i) Is ToolStripMenuItem AndAlso CType(obj.DropDownItems(i), ToolStripMenuItem).DropDownItems.Count <> 0 Then _ResetContextMenu(obj.DropDownItems(i))
        Next i
    End Sub

    Private Sub manageContextMenu(ByVal selectedIndex As Integer)
        If SelectedIndex = -1 Then Exit Sub
        If LastAgendaListSel Is Nothing Then Exit Sub
        If LastAgendaListSel.Tag.ToString = "NOHIDE" Then Exit Sub

        LastItemValueA = LastAgendaListSel.ItemValueA(SelectedIndex)

        Dim tmp As Long = Date.Now.Ticks

        Dim firstDayOfWeek As Date = CDate(LastAgendaListSel.Tag)
        FirstDayOfWeek = FirstDayOfWeek.AddDays(FirstDayOfWeek.DayOfWeek * -1)

        With Me
            .menuSeparator.Owner.SuspendLayout()
            .menuaenlever.Font = New Font(.menuaenlever.Font, FontStyle.Regular)
            .menuAnnonceClient.Checked = False
            ResetContextMenu()

            .menuScan.Visible = False
            .menupresent.Enabled = False
            .menuabsentmotive.Enabled = False
            .menuabsentnonmotive.Enabled = False
            .menueffstatus.Enabled = False
            .menuCloseFolder.Enabled = False
            .menuOpenFolder.Enabled = False

            If Date1InfDate2(LastAgendaListSel.Tag, Date.Today) = True Then 'passé à aujourd'hui
                .menunewrv.Enabled = False
                .menuaenlever.Enabled = False
                .menuacoller.Enabled = False
                .menuaCouper.Enabled = False
                .menureserved.Enabled = False
                .menuOpenHoraire.Enabled = False
                .menuOpenHoraireUpto.Enabled = False
                .menuCloseHoraire.Enabled = False
                .menuAddToQueueList.Enabled = False
                .menuModifyReserved.Enabled = False
                .menuCloseFolder.Enabled = False
                .menuAnnonceClient.Enabled = False
                .menuStartQL.Enabled = False
            End If

            If Date1InfDate2(LastAgendaListSel.Tag, Date.Today, True) = False Then 'futur à aujourd'hui
                .menupresent.Enabled = False
                .menuabsentnonmotive.Enabled = False
                .menuOpenFolder.Enabled = False
                .menuAnnonceClient.Enabled = False
            End If

            Select Case ColorTranslator.ToOle(LastAgendaListSel.ItemBackColor(SelectedIndex))
                Case ColorTranslator.ToOle(PlageLibre.BackColor)
                    'Case Libre
                    If MyMainWin.CopyBox.ItemValueA Is Nothing OrElse MyMainWin.CopyBox.ItemValueA.ToString = "" Then
                        .menuacoller.Enabled = False
                    Else
                        Dim myNoClient As Integer
                        If TypeOf MyMainWin.CopyBox.ItemValueA Is AgendaEntryReserved Then
                            MyNoClient = 0
                        ElseIf TypeOf MyMainWin.CopyBox.ItemValueA Is RendezVous Then
                            MyNoClient = CType(MyMainWin.CopyBox.ItemValueA, RendezVous).NoClient
                        End If
                    End If
                    .menuacopier.Visible = False
                    .menuaenlever.Visible = False
                    .menuopenaccount.Visible = False
                    .menumodifstatus.Visible = False
                    .menupaiement.Visible = False
                    .menuaCouper.Visible = False
                    .menuAddToQueueList.Visible = False
                    .menuAddToKeyPeople.Visible = False
                    .menuSendEmail.Visible = False
                    .menuRapports.Visible = False
                    .menuOpenHoraire.Visible = False
                    .menuOpenHoraireUpto.Visible = False
                    .menuModifyReserved.Visible = False
                    .menuRVFutur.Visible = False
                    .menuConfirmation.Visible = False
                    .menuAnnonceClient.Visible = False
                    .menuContinuerLaListeDattente.Visible = False
                    .menugeneraterecu.Visible = False
                    .menuRVRemarques.Visible = False

                    .menuacopier.Enabled = False
                    .menuaenlever.Enabled = False
                    .menuopenaccount.Enabled = False
                    .menumodifstatus.Enabled = False
                    .menupaiement.Enabled = False
                    .menuaCouper.Enabled = False
                    .menuAddToQueueList.Enabled = False
                    .menuAddToKeyPeople.Enabled = False
                    .menuSendEmail.Enabled = False
                    .menuRapports.Enabled = False
                    .menuOpenHoraire.Enabled = False
                    .menuOpenHoraireUpto.Enabled = False
                    .menuModifyReserved.Enabled = False
                    .menuRVFutur.Enabled = False
                    .menuConfirmation.Enabled = False
                    .menuAnnonceClient.Enabled = False

                    'Horaire spécifique utilisateur
                    If LockedVerification("Horaires-" & NoTRP & "-#" & FirstDayOfWeek.Date & "#.lock") = True Then .menuCloseHoraire.Enabled = False
                    .menunewrv.Font = New Font(.menunewrv.Font, FontStyle.Bold)

                Case ColorTranslator.ToOle(PlageReservee.BackColor)
                    'Case Plage réservée
                    .menuacoller.Visible = False
                    .menunewrv.Visible = False
                    .menuopenaccount.Visible = False
                    .menureserved.Visible = False
                    .menumodifstatus.Visible = False
                    .menupaiement.Visible = False
                    .menuAddToQueueList.Visible = False
                    .menuAddToKeyPeople.Visible = False
                    .menuSendEmail.Visible = False
                    .menuRapports.Visible = False
                    .menuOpenHoraire.Visible = False
                    .menuOpenHoraireUpto.Visible = False
                    .menuCloseHoraire.Visible = False
                    .menuStartQL.Visible = False
                    .menuRVFutur.Visible = False
                    .menuConfirmation.Visible = False
                    .menuAnnonceClient.Visible = False
                    .menuContinuerLaListeDattente.Visible = False
                    .menugeneraterecu.Visible = False
                    .menuRVRemarques.Visible = False

                    .menuacoller.Enabled = False
                    .menunewrv.Enabled = False
                    .menuopenaccount.Enabled = False
                    .menureserved.Enabled = False
                    .menumodifstatus.Enabled = False
                    .menupaiement.Enabled = False
                    .menuAddToQueueList.Enabled = False
                    .menuAddToKeyPeople.Enabled = False
                    .menuSendEmail.Enabled = False
                    .menuRapports.Enabled = False
                    .menuOpenHoraire.Enabled = False
                    .menuOpenHoraireUpto.Enabled = False
                    .menuCloseHoraire.Enabled = False
                    .menuRVFutur.Enabled = False
                    .menuConfirmation.Enabled = False
                    .menuAnnonceClient.Enabled = False

                    .menuModifyReserved.Font = New Font(.menuModifyReserved.Font, FontStyle.Bold)
                Case ColorTranslator.ToOle(PlageBloquee.BackColor)
                    'Case Plage bloquée
                    .menuaCouper.Visible = False
                    .menuacopier.Visible = False
                    .menuacoller.Visible = False
                    .menunewrv.Visible = False
                    .menuopenaccount.Visible = False
                    .menureserved.Visible = False
                    .menuModifyReserved.Visible = False
                    .menumodifstatus.Visible = False
                    .menupaiement.Visible = False
                    .menuAddToQueueList.Visible = False
                    .menuStartQL.Visible = False
                    .menuAddToKeyPeople.Visible = False
                    .menuSendEmail.Visible = False
                    .menuRapports.Visible = False
                    .menuOpenHoraire.Visible = False
                    .menuOpenHoraireUpto.Visible = False
                    .menuCloseHoraire.Visible = False
                    .menuRVFutur.Visible = False
                    .menuConfirmation.Visible = False
                    .menuAnnonceClient.Visible = False
                    .menugeneraterecu.Visible = False
                    .menuRVRemarques.Visible = False

                    If Date1InfDate2(LastAgendaListSel.Tag, Date.Today) = True Then
                        .menuContinuerLaListeDattente.Visible = False
                        .menuaenlever.Font = New Font(.menuaenlever.Font, FontStyle.Bold)
                    End If

                    .menuaenlever.Enabled = True
                    .menuacoller.Enabled = False
                    .menunewrv.Enabled = False
                    .menuopenaccount.Enabled = False
                    .menureserved.Enabled = False
                    .menumodifstatus.Enabled = False
                    .menupaiement.Enabled = False
                    .menuAddToQueueList.Enabled = False
                    .menuAddToKeyPeople.Enabled = False
                    .menuSendEmail.Enabled = False
                    .menuRapports.Enabled = False
                    .menuOpenHoraire.Enabled = False
                    .menuOpenHoraireUpto.Enabled = False
                    .menuCloseHoraire.Enabled = False
                    .menuRVFutur.Enabled = False
                    .menuConfirmation.Enabled = False
                    .menuAnnonceClient.Enabled = False

                    .menuContinuerLaListeDattente.Font = New Font(.menuContinuerLaListeDattente.Font, FontStyle.Bold)
                Case ColorTranslator.ToOle(PlageClinique.BackColor)
                    'Case Clinique
                    .menuaCouper.Visible = False
                    .menuacopier.Visible = False
                    .menuacoller.Visible = False
                    .menuSeparator.Visible = False
                    .menuaenlever.Visible = False
                    .menunewrv.Visible = False
                    .menuopenaccount.Visible = False
                    .menumodifstatus.Visible = False
                    .menureserved.Visible = False
                    .menupaiement.Visible = False
                    .menuSendEmail.Visible = False
                    .menuCloseHoraire.Visible = False
                    .menuRapports.Visible = False
                    .menuAddToKeyPeople.Visible = False
                    .menuAddToQueueList.Visible = False
                    .menuStartQL.Visible = False
                    .menuModifyReserved.Visible = False
                    .menuRVFutur.Visible = False
                    .menuConfirmation.Visible = False
                    .menuAnnonceClient.Visible = False
                    .menuContinuerLaListeDattente.Visible = False
                    .menugeneraterecu.Visible = False
                    .menuRVRemarques.Visible = False

                    .menuaCouper.Enabled = False
                    .menuacopier.Enabled = False
                    .menuacoller.Enabled = False
                    .menuSeparator.Enabled = False
                    .menuaenlever.Enabled = False
                    .menunewrv.Enabled = False
                    .menuopenaccount.Enabled = False
                    .menumodifstatus.Enabled = False
                    .menureserved.Enabled = False
                    .menupaiement.Enabled = False
                    .menuSendEmail.Enabled = False
                    .menuCloseHoraire.Enabled = False
                    .menuRapports.Enabled = False
                    .menuAddToKeyPeople.Enabled = False
                    .menuAddToQueueList.Enabled = False
                    .menuModifyReserved.Enabled = False
                    .menuRVFutur.Enabled = False
                    .menuConfirmation.Enabled = False
                    .menuAnnonceClient.Enabled = False

                    If SelectedIndex > FindFirstTime(DayList.IndexOf(LastAgendaListSel)) And SelectedIndex < FindLastTime(DayList.IndexOf(LastAgendaListSel)) Then
                        .menuOpenHoraireUpto.Visible = False
                        .menuOpenHoraireUpto.Enabled = False
                        .menuOpenHoraire.Font = New Font(.menuOpenHoraire.Font, FontStyle.Bold)
                        .menuOpenHoraireUpto.Font = New Font(.menuOpenHoraireUpto.Font, FontStyle.Regular)
                    Else
                        .menuOpenHoraire.Font = New Font(.menuOpenHoraire.Font, FontStyle.Regular)
                        .menuOpenHoraireUpto.Font = New Font(.menuOpenHoraireUpto.Font, FontStyle.Bold)
                    End If

                    'Horaire spécifique utilisateur
                    If LockedVerification("Horaires-" & NoTRP & "-#" & FirstDayOfWeek.Date & "#.lock") = True Then
                        .menuOpenHoraire.Enabled = False
                        .menuOpenHoraireUpto.Enabled = False
                    End If
                Case Else
                    'Case Compte client
                    .menuacoller.Visible = False
                    .menureserved.Visible = False
                    .menuCloseHoraire.Visible = False
                    .menuOpenHoraire.Visible = False
                    .menuOpenHoraireUpto.Visible = False
                    .menuModifyReserved.Visible = False
                    .menuContinuerLaListeDattente.Visible = False
                    .menuStartQL.Visible = False

                    .menuacoller.Enabled = False
                    .menureserved.Enabled = False
                    .menuCloseHoraire.Enabled = False
                    .menuOpenHoraire.Enabled = False
                    .menuOpenHoraireUpto.Enabled = False
                    .menuModifyReserved.Enabled = False

                    .menunewrv.Enabled = True

                    Dim rv As RendezVous = lastAgendaListSel.ItemValueA(selectedIndex)
                    If TypeOf myMainWin.copyBox.itemValueA Is AgendaEntry Then
                        Dim rvCopied As AgendaEntry = myMainWin.copyBox.itemValueA
                        Dim activatedPasteMenu As Boolean = rvCopied IsNot Nothing AndAlso rvCopied.isCutting AndAlso rv.GetType.Equals(rvCopied.GetType) AndAlso rv.noItemable = rvCopied.noItemable
                        .menuacoller.Visible = activatedPasteMenu
                        .menuacoller.Enabled = activatedPasteMenu
                    End If

                    If ColorTranslator.ToOle(lastAgendaListSel.ItemBackColor(selectedIndex)) = ColorTranslator.ToOle(plageRV.BackColor) Then
                        If Not date1Infdate2(lastAgendaListSel.Tag, Date.Today, True) = False Then
                            .menupresent.Enabled = True
                            .menuabsentnonmotive.Enabled = True
                        End If
                        .menuabsentmotive.Enabled = True
                        .menugeneraterecu.Visible = False
                    Else
                        .menuaCouper.Enabled = False
                        .menuAddToQueueList.Enabled = False
                        .menueffstatus.Enabled = True
                        .menuaenlever.Enabled = False
                    End If

                    Dim infos(,) As String = Nothing
                    infos = DBLinker.getInstance.readDB("WITH subBills (NoFacture) AS (SELECT NoFacture FROM dbo.fn_getAllNoFactures(" & IIf(rv.noFacture > 0, rv.noFacture, "null") & ")) SELECT TOP 1 InfoClients.Courriel, InfoFolders.StatutOuvert, (SELECT Count(*) FROM KeyPeople WHERE (Nom)=InfoClients.Nom + ',' + InfoClients.Prenom ) AS InKP, ListeAttente.NoVisite,(SELECT     COUNT(*) FROM          FacturesRecusLeft INNER JOIN subBills ON FacturesRecusLeft.NoFacture = subBills.NoFacture " & IIf(PreferencesManager.getGeneralPreferences()("printRecuForClientAuto") = True, " WHERE NoEntitePayeur = 1", "") & ")" & _
                        " FROM (((InfoClients INNER JOIN InfoFolders ON InfoClients.NoClient = InfoFolders.NoClient) INNER JOIN InfoVisites ON InfoFolders.NoFolder = InfoVisites.NoFolder) LEFT JOIN ListeAttente ON InfoVisites.NoVisite = ListeAttente.NoVisite) WHERE (InfoVisites.NoVisite=" & rv.noVisite & ");")

                    If infos Is Nothing Then
                        .menuSendEmail.Enabled = False
                        .menuOpenFolder.Visible = False
                        .menuOpenFolder.Enabled = False
                        .menuCloseFolder.Visible = False
                        .menuAddToKeyPeople.Enabled = False
                        .menuAddToQueueList.Enabled = False
                        .menugeneraterecu.Enabled = False
                    Else
                        If infos(0, 0).IndexOf("@") = -1 Then .menuSendEmail.Enabled = False
                        If infos(1, 0) = False Then
                            .menuOpenFolder.Visible = True
                            .menuOpenFolder.Enabled = True
                            .menuCloseFolder.Visible = False
                        Else
                            .menuCloseFolder.Visible = True
                            .menuCloseFolder.Enabled = True
                            .menuOpenFolder.Visible = False
                        End If
                        If infos(2, 0) > 0 Then .menuAddToKeyPeople.Enabled = False
                        If infos(3, 0) = rv.noVisite.ToString Then .menuAddToQueueList.Enabled = False
                        If infos(4, 0) <> "0" Then .menugeneraterecu.Enabled = True Else .menugeneraterecu.Enabled = False
                    End If

                    .menuAnnonceClient.Checked = rv.isAnnounced

                    'Is an Évaluation IVB(2) / IsBillPaid IVB(3) / IsBillSouffrance IVB(4) / NoFacture IVB(5) / MontantPayé IVB(6)
                    If rv.amountPaid > 0 Or rv.isBillSouffrance Or rv.isBillPaid Then 'MontantPaiementTotal
                        If (rv.amountPaid > 0) Or rv.isBillSouffrance Then .menuaenlever.Enabled = False
                        .menueffstatus.Enabled = False
                    End If
                    .menupaiement.Enabled = Bill.isPaymentsToDoByClient(rv.noClient)
                    If lastAgendaListSel.ItemIconsShowed(selectedIndex, 0) = False Then .menuConfirmation.Enabled = False
                    .menuopenaccount.Font = New Font(.menuopenaccount.Font, FontStyle.Bold)
                    .menunewrv.Font = New Font(.menunewrv.Font, FontStyle.Regular)
            End Select
            .menuSeparator.Owner.ResumeLayout()
        End With

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Time:" & lastAgendaListSel.ItemBackColor(selectedIndex).ToString & ":" & (Date.Now.Ticks - tmp) / 10000000

        MyMainWin.menuAgendaContextuel.Enabled = MyMainWin.ActiveMdiChild Is Me
        If MyMainWin.ActiveMdiChild Is Me Then ToolStripManager.Merge(menuclickagenda, MyMainWin.MainWinMenu)
        menuSeparator.Owner.ResumeLayout()

    End Sub

    Private Sub maximiseDay(ByVal index As Byte)
        If DayList(Index).ListCount <= 0 Or DayList(Index).Visible = False Then Exit Sub

        Dim curSelected As Integer = DayList(42).Selected
        If CurSelected < 0 Then CurSelected = DayList(Index).Selected
        Dim ml As Integer
        Dim PosH, posV As Byte

        MaxBox.Visible = False
        MaxBox.Top = DaysRectangle.Top + Me.AutoScrollPosition.Y
        'Maximiser
        PosV = Math.Floor(Index / Me.NoVertical)
        PosH = Index - PosV * Me.NoVertical
        DayList(42).Tag = DayList(Index).Tag
        BuildStructure(Me.NoHorizontal, Me.NoVertical, DayList(Index).Tag, , PosH + 1, PosV + 1, 42)
        LoadAgenda(PosH + 1, PosV + 1, 42)
        DayList(42).Selected = CurSelected
        ml = dayList(index).Left ' (maxMin(index).Left) + (maxMin(index).Width) + 2
        Dim newML As Integer = ml - maxBox.Width + (dayList(index).Width)
        If newML > 0 AndAlso (ml + maxBox.Width) > Me.ClientRectangle.Width Then
            maxBox.Left = newML
        Else
            maxBox.Left = ml
        End If
        MaxBox.Visible = True
        DayList(42).Focus()
    End Sub


    Private Function topLeftCornerPos(ByRef posH As Byte, ByRef posV As Byte, ByRef horizontalMax As Byte, ByRef verticalMax As Byte) As PositionType
        Dim yofSquare As Double
        Dim xofSquare As Double
        XofSquare = (DaysRectangle.Width / HorizontalMax) * (PosH - 1) + DaysRectangle.Left
        YofSquare = (DaysRectangle.Height / VerticalMax) * (PosV - 1) + DaysRectangle.Top

        TopLeftCornerPos.X = XofSquare
        TopLeftCornerPos.Y = YofSquare
    End Function

    Private Sub openCloseHoraire(ByVal first As Short, ByVal last As Short, Optional ByVal opening As Boolean = True)
        'Droit & Accès
        If CurrentDroitAcces(87) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier l'horaire du thérapeute." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim index As Byte = LastAgendaListSel.Name.Substring(7)
        Dim i, NoItem, oldVSValue As Short
        Dim myDate As Date = LastAgendaListSel.Tag
        Dim newHoraire As Boolean = False
        OldVSValue = LastAgendaListSel.VSValue
        NoItem = LastAgendaListSel.Selected

        MyDate = MyDate.AddDays(MyDate.DayOfWeek * -1)

        'Horaire spécifique utilisateur
        Dim curHoraire As Schedule = SchedulesManager.GetInstance.getSchedule(NoTRP, MyDate)
        If curHoraire.isDefault Then
            curHoraire = New Schedule(curHoraire, MyDate)
            NewHoraire = True
        End If

        MyDate = LastAgendaListSel.Tag
        Dim curTime As String = lastAgendaListSel.ItemText(last).Substring(0, 5)
        MyDate = MyDate.Date.AddMinutes(curTime.Substring(0, 2) * 60 + curTime.Substring(3, 2))
        For i = Last To First Step -1
            curHoraire.SetOpened(MyDate, Opening)
            MyDate = MyDate.AddMinutes(curHoraire.intervalMinutes * -1)
        Next i

        curHoraire.saveData()

        LastAgendaListSel.VSValue = OldVSValue
        LastAgendaListSel.Selected = NoItem
    End Sub

    Private Function findNextTime(ByRef index As Byte, ByRef noItem As Short) As Short
        Dim fNo As Short
        Dim fpt As Boolean
        FPT = False
        FNo = NoItem

        Do
            NoItem = DayList(Index).FindNextItem(NoItem)
            If Not System.Drawing.ColorTranslator.ToOle(DayList(Index).ItemBackColor(NoItem)) = System.Drawing.ColorTranslator.ToOle(PlageClinique.BackColor) Then FPT = True
        Loop Until FPT = True Or NoItem = -1

        If NoItem = -1 Then Return FNo

        Return NoItem
    End Function

    Private Function findPreviousTime(ByRef index As Byte, ByRef noItem As Short) As Short
        Dim fNo As Short
        Dim fpt As Boolean
        FPT = False
        FNo = NoItem

        Do
            NoItem = DayList(Index).FindPreviousItem(NoItem)
            If Not System.Drawing.ColorTranslator.ToOle(DayList(Index).ItemBackColor(NoItem)) = System.Drawing.ColorTranslator.ToOle(PlageClinique.BackColor) Then FPT = True
        Loop Until FPT = True Or NoItem = -1

        If NoItem = -1 Then Return FNo

        Return NoItem
    End Function

    Private Function findFirstTime(ByRef index As Byte) As Short
        If DayList(Index).ListCount <= 0 Then Exit Function
        Dim i As Short

        For i = 0 To DayList(Index).Maximum
            If ColorTranslator.ToOle(dayList(index).ItemBackColor(i)) <> ColorTranslator.ToOle(plageClinique.BackColor) Then
                Return i
            End If
        Next i
    End Function

    Private Function findLastTime(ByRef index As Byte) As Short
        If DayList(Index).ListCount <= 0 Then Exit Function
        Dim i As Short

        For i = DayList(Index).Maximum To 0 Step -1
            If ColorTranslator.ToOle(DayList(Index).ItemBackColor(i)) <> ColorTranslator.ToOle(PlageClinique.BackColor) Then Return i
        Next i
    End Function
#End Region


    Private Sub adateday_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adateday.Click
        Me.DebutDate = Me.DebutDate.AddDays(1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub adateweek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adateweek.Click
        Me.DebutDate = Me.DebutDate.AddDays(7)
        UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub choosedate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles choosedate.Click
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Today.Year + 1, , , , , , True)
        If dateReturn.Count <> 0 Then
            Me.DebutDate = dateReturn(0)
            updateStructure(Me.noTRP)
        End If
    End Sub

    Private Sub agenda_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        For i As Integer = 0 To MyMainWin.menutherapeute.DropDownItems.Count - 1
            If MyMainWin.menutherapeute.DropDownItems(i).Text.EndsWith("(" & Me.NoTRP & ")") Then MyMainWin.menutherapeute.DropDownItems(i).Enabled = True
        Next i
    End Sub

    Private Sub agenda_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        If CurrentUserName = "Administrateur" Then AdminBox.Visible = True
        If Me.Text.IndexOf(":") = -1 Then Me.Text = "Agenda " & " : " & UsersManager.getInstance.getUser(Me.NoTRP).ToString()

        Me.Height = myMainWin.minimumMdiChildRectangle.Height
        Me.Width = myMainWin.minimumMdiChildRectangle.Width
        Me.MinimumSize = New Size(Me.Width, Me.Height / 2)
        Me.Top = 0
        Me.Left = 0

        AgendaLoaded = True
    End Sub

    Private Sub maxBox_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MaxBox.LocationChanged
        Dim a As Byte = 0
    End Sub

    Private Sub maxBox_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MaxBox.Resize
        If DayList Is Nothing Then Exit Sub

        Try
            DayList(42).Width = MaxBox.ClientRectangle.Width
            DayList(42).Top = MaxMin(42).Height
            DayList(42).Height = MaxBox.ClientRectangle.Height - DayList(42).Top
            LabelsNbDays(42).Left = CDbl(SDate.Text)
            maxMin(42).Left = maxBox.ClientRectangle.Width - maxMin(42).Width - CDbl(sDate.Text)
            feux(42).Left = maxMin(42).left - feux(42).Width - CDbl(sDate.Text)
        Catch
        End Try
    End Sub

    Private Sub pdateday_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles pdateday.Click
        Me.DebutDate = Me.DebutDate.AddDays(-1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub pdateweek_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles pdateweek.Click
        Me.DebutDate = Me.DebutDate.AddDays(-7)
        UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub pdatemonth_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles pdatemonth.Click
        Me.DebutDate = Me.DebutDate.AddMonths(-1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Private Sub adatemonth_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adatemonth.Click
        Me.DebutDate = Me.DebutDate.AddMonths(1)
        UpdateStructure(Me.NoTRP)
    End Sub

    Private firstLoading As Boolean = True

    Private Sub agenda_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        APIs.ShowScrollBar(Me.Handle, 0, False)

        Me.MinimumSize = New Size(myMainWin.minimumMdiChildRectangle.Width, Me.MinimumSize.Height)
        Me.Width = myMainWin.minimumMdiChildRectangle.Width
        If firstLoading Then
            Me.Height = myMainWin.minimumMdiChildRectangle.Height
            firstLoading = False
        End If

        Me.daysRectangle = New Rectangle(Me.daysRectangle.X, Me.daysRectangle.Y, Me.ClientRectangle.Width - mSpace.Text * 2, Me.daysRectangle.Height)
    End Sub

    Private Sub agenda_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If firstLoading Then Me.Top = 0
        Me.Left = 0
    End Sub

    Private Sub maxMin_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles MaxMin.Click
        If Index < 42 Then
            MaximiseIndex = Index
            MaximiseDay(Index)
        Else
            'Minimiser
            MaxBox.Visible = False
        End If
    End Sub

    Private Sub chargement_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chargement.TextChanged
        Chargement.Left = CInt((Me.Width - Chargement.Width) / 2)
    End Sub

    Private Sub agenda_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        If PrematureClosing = True Then Me.Close()
    End Sub

    Private Function findNextPreviousAgenda(Optional ByVal forward As Boolean = True) As SingleWindow
        Dim i, Min, Max, stepping As Integer
        With MyMainWin.FormOuvertes
            If Forward Then
                Max = .Items.Count - 1
                Min = .Selected + 1
                Stepping = 1
            Else
                Min = .Selected - 1
                Max = 0
                Stepping = -1
            End If
            With .Items
                For i = Min To Max Step Stepping
                    If TypeOf .Item(i).ValueA Is Agenda Then Return CType(.Item(i).ValueA, SingleWindow)
                Next i
            End With
        End With

        Return Nothing
    End Function

    Private Sub previous_Next_Agenda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviousAgenda.Click, NextAgenda.Click
        Dim myPNAgenda As SingleWindow = Nothing

        If CType(sender, Button).Name.ToString.ToLower.StartsWith("previous") Then
            MyPNAgenda = FindNextPreviousAgenda(False)
        Else
            MyPNAgenda = FindNextPreviousAgenda(True)
        End If

        If Not MyPNAgenda Is Nothing Then MyPNAgenda.Select() : MyPNAgenda.WindowState = FormWindowState.Normal
    End Sub

    Private Sub agenda_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        If Me.LastAgendaListSel IsNot Nothing Then Me.ActiveControl = Me.LastAgendaListSel
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        Dim borneSup As Date = LIMIT_DATE
        Dim borneInf As Date = LIMIT_DATE
        Dim doUpdate As Boolean = False

        'Update d'un horaire
        If dataReceived.function.StartsWith("Horaire-") AndAlso (dataReceived.params(1) = Me.NoTRP OrElse dataReceived.params(1) = 0) Then
            'Vérifie les dates
            borneSup = CDate(dataReceived.params(2)).Date.AddDays(6)
            borneInf = CDate(dataReceived.params(2)).Date
            doUpdate = True
        End If

        'Update d'une entrée de l'agenda
        'dataReceived.params(2) : NoTRP
        If dataReceived.function = "Agendas" Then
            Select Case dataReceived.params.Length
                Case 0 'All
                    doUpdate = True

                Case 1 'For a specific date
                    borneSup = CDate(dataReceived.params(0)).Date
                    borneInf = borneSup

                    doUpdate = True
                Case 2, 3 'For a specific date and/or a specific therapist
                    If dataReceived.params(1) = True Then
                        borneInf = LIMIT_DATE
                    Else
                        borneSup = CDate(dataReceived.params(0)).Date
                        borneInf = borneSup
                    End If

                    doUpdate = dataReceived.params.Length <> 3 OrElse dataReceived.params(2) = noTRP
            End Select
        End If


        'Quitte si aucun update à faire
        If doUpdate = False Then Exit Sub

        'Mets à jour les journées nécessaires
        Dim dateFin As Date = Me.DebutDate.AddDays(Me.NoVertical * Me.NoHorizontal - 1)
        Dim verifBorneSup As Boolean = Date1InfDate2(Me.DebutDate, borneSup, True) AndAlso Date1InfDate2(borneInf, dateFin, True)
        Dim verifBorneInf As Boolean = Date1InfDate2(Me.DebutDate, borneInf, True) AndAlso Date1InfDate2(borneSup, dateFin, True)
        If borneInf = LIMIT_DATE Then
            Me.buildStructure(Me.NoVertical, Me.NoHorizontal, DebutDate)
            Me.loadAgenda()
        ElseIf verifBorneSup OrElse verifBorneInf Then
            Dim n As Integer = 0
            For i As Integer = 1 To Me.NoHorizontal
                For j As Integer = 1 To Me.NoVertical
                    If Date.TryParse(DayList(n).Tag, Nothing) AndAlso Date1InfDate2(borneInf, DayList(n).Tag, True) AndAlso Date1InfDate2(DayList(n).Tag, borneSup, True) Then
                        Me.buildStructure(Me.NoVertical, Me.NoHorizontal, dayList(n).Tag, , i, j)
                        Me.LoadAgenda(i, j)
                    End If
                    n += 1
                Next j
            Next i
        End If
    End Sub

    Public Overrides ReadOnly Property textExtra() As String
        Get
            Dim curUser As User = UsersManager.getInstance.getUser(Me.NoTRP)
            Return curUser.title & " offrant :" & vbCrLf & curUser.services.Replace("§", ", ")
        End Get
    End Property
End Class
