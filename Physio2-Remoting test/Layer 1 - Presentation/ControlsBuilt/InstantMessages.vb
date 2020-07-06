Public Class InstantMessages
    Inherits BaseUpdatedControl
    Implements IMovableObject, IControllable

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        movingCursor = DrawingManager.getInstance.getCursor("MOVE4WAY.CUR")
        Title.Cursor = movingCursor
        configList(AlertsList)

        Me._NewFont = AlertsList.baseFont
        AlertsList.borderColor = Color.Black

        AddHandler AlertsManager.getInstance.internalDataReloading, AddressOf alertHasBeenChanged

        formCreator = Me.ParentForm
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            RemoveHandler AlertsManager.getInstance.internalDataReloading, AddressOf alertHasBeenChanged
            InternalUpdatesManager.getInstance.removeConsumer(Me)
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents AlertsList As CI.Controls.List
    Friend WithEvents Title As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents AlertMenu As ContextMenuItem
    Friend WithEvents menuDelMSG As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOpenMailSys As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOpenClientAccount As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuAddNote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRenameNote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOpenNote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuOpenReportGen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuContinueQueueList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menusetOld As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuLigne1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuAllOld As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuLigne2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuLigne3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuTransferAllNotes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuHideAllNotes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuLigne4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuTransferNoteToSomeone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ControlBox As Clinica.TransparentControlBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Title = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AlertMenu = New ContextMenuItem
        Me.menuAddNote = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuLigne1 = New System.Windows.Forms.ToolStripSeparator
        Me.menusetOld = New System.Windows.Forms.ToolStripMenuItem
        Me.menuAllOld = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuLigne2 = New System.Windows.Forms.ToolStripSeparator
        Me.menuOpenMailSys = New System.Windows.Forms.ToolStripMenuItem
        Me.menuOpenNote = New System.Windows.Forms.ToolStripMenuItem
        Me.menuOpenClientAccount = New System.Windows.Forms.ToolStripMenuItem
        Me.menuOpenReportGen = New System.Windows.Forms.ToolStripMenuItem
        Me.menuContinueQueueList = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuLigne3 = New System.Windows.Forms.ToolStripSeparator
        Me.menuRenameNote = New System.Windows.Forms.ToolStripMenuItem
        Me.menuDelMSG = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuLigne4 = New System.Windows.Forms.ToolStripSeparator
        Me.MenuHideAllNotes = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuTransferAllNotes = New System.Windows.Forms.ToolStripMenuItem
        Me.ControlBox = New Clinica.TransparentControlBox
        Me.AlertsList = New CI.Controls.List
        Me.menuTransferNoteToSomeone = New System.Windows.Forms.ToolStripMenuItem
        Me.SuspendLayout()
        '
        'Title
        '
        Me.Title.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title.BackColor = System.Drawing.Color.Black
        Me.Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title.ForeColor = System.Drawing.Color.White
        Me.Title.Location = New System.Drawing.Point(0, 0)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(297, 240)
        Me.Title.TabIndex = 1
        Me.Title.Text = "Message(s) instantané(s) && Note(s)"
        Me.Title.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'AlertMenu
        '
        Me.AlertMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuAddNote, Me.MenuLigne1, Me.menusetOld, Me.menuAllOld, Me.MenuLigne2, Me.menuOpenMailSys, Me.menuOpenNote, Me.menuOpenClientAccount, Me.menuOpenReportGen, Me.menuContinueQueueList, Me.MenuLigne3, Me.menuRenameNote, Me.menuDelMSG, Me.menuTransferNoteToSomeone, Me.MenuLigne4, Me.MenuHideAllNotes, Me.MenuTransferAllNotes})
        Me.AlertMenu.Name = "AlertMenu"
        Me.AlertMenu.Size = New System.Drawing.Size(121, 474)
        Me.AlertMenu.Text = "AlertsContextMenu"
        Me.AlertMenu.Visible = False

        '
        'menuAddNote
        '
        Me.menuAddNote.Text = "Ajouter une note personnelle"
        '
        'MenuLigne1
        '
        Me.MenuLigne1.Text = "-"
        '
        'menusetOld
        '
        Me.menusetOld.Text = "Marquer commu étant fait"
        '
        'menuAllOld
        '
        Me.menuAllOld.Text = "Marquer tous comme étant fait"
        '
        'MenuLigne2
        '
        Me.MenuLigne2.Text = "-"
        '
        'menuOpenMailSys
        '
        Me.menuOpenMailSys.Font = New Font(menuOpenMailSys.Font, FontStyle.Bold)
        Me.menuOpenMailSys.Text = "Ouvrir la réception des messages"
        '
        'menuOpenNote
        '
        Me.menuOpenNote.Font = New Font(menuOpenNote.Font, FontStyle.Bold)
        Me.menuOpenNote.Text = "Ouvrir la note personnelle"
        '
        'menuOpenClientAccount
        '
        Me.menuOpenClientAccount.Font = New Font(menuOpenClientAccount.Font, FontStyle.Bold)
        Me.menuOpenClientAccount.Text = "Ouvrir le compte du client"
        '
        'menuOpenReportGen
        '
        Me.menuOpenReportGen.Font = New Font(menuOpenReportGen.Font, FontStyle.Bold)
        Me.menuOpenReportGen.Text = "Ouvrir le générateur de rapport"

        '
        'menuContinueQueueList
        '
        Me.menuContinueQueueList.Font = New Font(menuContinueQueueList.Font, FontStyle.Bold)
        Me.menuContinueQueueList.Text = "Reprendre la liste d'attente"
        '
        'MenuLigne3
        '
        Me.MenuLigne3.Text = "-"
        '
        'menuRenameNote
        '
        Me.menuRenameNote.Text = "Renommer la note"
        '
        'menuDelMSG
        '
        Me.menuDelMSG.Text = "Supprimer le message/note"
        '
        'MenuLigne4
        '
        Me.MenuLigne4.Text = "-"
        '
        'MenuHideAllNotes
        '
        Me.MenuHideAllNotes.Text = "Cacher toutes les notes"
        '
        'MenuTransferAllNotes
        '
        Me.MenuTransferAllNotes.Text = "Transférer toutes les notes ouvertes"
        '
        'ControlBox
        '
        Me.ControlBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ControlBox.BackColor = System.Drawing.Color.Transparent
        Me.ControlBox.isLocked = True
        Me.ControlBox.Location = New System.Drawing.Point(246, 1)
        Me.ControlBox.Name = "ControlBox"
        Me.ControlBox.Size = New System.Drawing.Size(47, 15)
        Me.ControlBox.TabIndex = 4
        '
        'AlertsList
        '
        Me.AlertsList.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.AlertsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlertsList.autoAdjust = True
        Me.AlertsList.autoKeyDownSelection = True
        Me.AlertsList.autoSizeHorizontally = True
        Me.AlertsList.autoSizeVertically = True
        Me.AlertsList.BackColor = System.Drawing.Color.White
        Me.AlertsList.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.AlertsList.baseBackColor = System.Drawing.Color.White
        Me.AlertsList.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.AlertsList.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.AlertsList.bgColor = System.Drawing.Color.White
        Me.AlertsList.borderColor = System.Drawing.Color.Empty
        Me.AlertsList.borderSelColor = System.Drawing.Color.Empty
        Me.AlertsList.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.AlertsList.CausesValidation = False
        Me.AlertsList.clickEnabled = False
        Me.AlertsList.do3D = False
        Me.AlertsList.draw = False
        Me.AlertsList.hScrollColor = System.Drawing.SystemColors.Control
        Me.AlertsList.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.AlertsList.hScrolling = True
        Me.AlertsList.hsValue = 0
        Me.AlertsList.itemBorder = 0
        Me.AlertsList.itemMargin = 0
        Me.AlertsList.Location = New System.Drawing.Point(0, 18)
        Me.AlertsList.mouseMove3D = False
        Me.AlertsList.mouseSpeed = 0
        Me.AlertsList.Name = "AlertsList"
        Me.AlertsList.objMaxHeight = 214.0!
        Me.AlertsList.objMaxWidth = 0.0!
        Me.AlertsList.objMinHeight = 0.0!
        Me.AlertsList.objMinWidth = 297.0!
        Me.AlertsList.reverseSorting = False
        Me.AlertsList.selected = -1
        Me.AlertsList.selectedClickAllowed = True
        Me.AlertsList.selectMultiple = True
        Me.AlertsList.Size = New System.Drawing.Size(297, 214)
        Me.AlertsList.sorted = False
        Me.AlertsList.TabIndex = 0
        Me.AlertsList.toolTipText = Nothing
        Me.AlertsList.vScrollColor = System.Drawing.SystemColors.Control
        Me.AlertsList.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.AlertsList.vScrolling = True
        Me.AlertsList.vsValue = 0
        '
        'menuTransferNoteToSomeone
        '
        Me.menuTransferNoteToSomeone.Text = "Transférer la note à un autre utilisateur"
        '
        'InstantMessages
        '
        Me.Controls.Add(Me.ControlBox)
        Me.Controls.Add(Me.AlertsList)
        Me.Controls.Add(Me.Title)
        Me.Name = "InstantMessages"
        Me.Size = New System.Drawing.Size(297, 240)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private nbNew As Integer = 0
    Private nbCurrent As Integer = 0
    Private movingCursor As Cursor
    Private XObjects, yObjects As Integer
    Private buttonDown As MouseButtons
    Private _BlockObjectInArea As Boolean = True
    Private _JoinedToolBarItem As ToolStrip = Nothing
    Private formCreator As Form
    Private mynewNoteName As Integer = 0
    Private _ActivatedColor As Color = Color.White
    Private _NewFont As Font

    Private curAlert As Alert = Nothing
    Private curSelectedItems As New Generic.List(Of CI.Controls.IListItem)

    Public Event willMove(ByVal sender As Object, ByVal x As Integer, ByVal y As Integer, ByVal xObjects As Integer, ByVal yObjects As Integer) Implements IMovableObject.willMove
    Public Shadows Event move(ByVal sender As Object, ByVal e As EventArgs) Implements IMovableObject.move

#Region "Propriétés"
    Public Property newAlertFont() As Font
        Get
            Return _NewFont
        End Get
        Set(ByVal value As Font)
            _NewFont = value
        End Set
    End Property


    Public Property activatedColor() As Color
        Get
            Return _ActivatedColor
        End Get
        Set(ByVal value As Color)
            _ActivatedColor = value
        End Set
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

    Public ReadOnly Property isAlarmActive() As Boolean
        Get
            Return AlarmManager.getInstance.started
        End Get
    End Property
#End Region

    Private Delegate Sub loadingAlerts(ByVal reloadAlerts As Boolean)

    Private Sub alertHasBeenChanged(ByVal sender As Alert)
        If Me.InvokeRequired Then
            Me.Invoke(New loadingAlerts(AddressOf loadAlerts), New Object() {False})
            Exit Sub
        End If
        loadAlerts(False)
    End Sub

    Public Sub loadAlerts(Optional ByVal reloadAlerts As Boolean = True)
        AlertMenu.HideDropDown()

        configList(AlertsList)
        AlertsList.autoSizeHorizontally = True
        If reloadAlerts Then AlertsManager.getInstance.load()
        
        Dim alerts As Generic.List(Of Alert) = AlertsManager.getInstance.getItemables
        Dim oldNbNew As Integer = nbNew
        nbNew = 0
        Dim curAlert As Alert
        Dim curIndexAlert As Short

        AlertsList.cls()

        For Each curAlert In alerts
            If curAlert.isHidden = False And (curAlert.showingDate.Equals(LIMIT_DATE) = True OrElse date1Infdate2(curAlert.showingDate, Date.Now, True)) Then
                curIndexAlert = AlertsList.add(curAlert.toString)
                If curAlert.isNew Then
                    nbNew += 1
                    AlertsList.ItemBackColor(curIndexAlert) = Me.activatedColor
                    AlertsList.ItemFont(curIndexAlert) = _NewFont
                End If
                AlertsList.ItemValueA(curIndexAlert) = curAlert
                If curAlert.noAlert = mynewNoteName Then
                    curAlert.doAction()
                    mynewNoteName = 0
                End If
            End If
        Next

        If nbNew <> oldNbNew Then 'Active l'objet s'il y a eu des nouveaux
            Me.visible = True
            '    RaiseEvent BarTitleChanged(Me)
        End If

        AlertsList.draw = True : AlertsList.draw = False
        Me.BringToFront()

    End Sub

    Private Sub alertsList_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles AlertsList.KeyUp
        If e.keyCode = 13 Then
            If AlertsList.selected = -1 Then Exit Sub

            doAlert(curAlert)
        End If
    End Sub

    Private Sub alertsList_MouseUp(ByVal sender As Object, ByVal e As CI.Controls.List.MouseUpEventArgs) Handles AlertsList.mouseUp
        If AlertsList.listCount <= 0 Then alertsList_ItemClick(Me, New CI.Controls.List.ClickEventArgs(-1, e.button, e.x, e.y))
    End Sub

    Private Sub title_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Title.MouseDown
        buttonDown = e.Button
        XObjects = e.X
        yObjects = e.Y
    End Sub

    Private Sub title_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Title.MouseUp
        buttonDown = MouseButtons.None
    End Sub

    Private Sub title_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Title.MouseMove
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

    Private Sub objects_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Title.MouseMove, AlertsList.mouseMove
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

    Private Sub alertsList_ItemClick(ByVal sender As Object, ByVal e As CI.Controls.List.ClickEventArgs) Handles AlertsList.itemClick
        If e.button = 2 Then
            'If AlertsList.Items(e.SelectedItem).IsSelected = False Then 
            AlertsList.selected = e.selectedItem
            manageContextMenu()
            AlertMenu.showDropDown(True)
        End If
    End Sub

    Private Function matchNewSelected(ByVal obj As CI.Controls.IListItem) As Boolean
        Return obj.IsSelected AndAlso CType(obj.ValueA, Alert).isNew
    End Function


    Private Sub manageContextMenu()
        menuOpenMailSys.Visible = False
        menuOpenClientAccount.Visible = False
        menuOpenNote.Visible = False
        menuRenameNote.Visible = False
        menuOpenReportGen.Visible = False
        menuContinueQueueList.Visible = False
        menuTransferNoteToSomeone.Visible = False

        If AlertsList.selected <> -1 Then
            Dim curSelectedItems As Generic.List(Of CI.Controls.IListItem) = AlertsList.selectedItems
            Dim delMessage As String = "Supprimer ce(s) message(s) et/ou cette(ces) note(s) ?"
            If curSelectedItems.Count = 1 Then
                If TypeOf curAlert Is AlertOfPersoNote Then
                    delMessage = "Supprimer la note personnelle"
                Else
                    delMessage = "Supprimer le message instantané"
                End If
            End If
            menuDelMSG.Text = delMessage
            menusetOld.Visible = True
            menusetOld.Enabled = AlertsList.items.FindAll(New Predicate(Of CI.Controls.IListItem)(AddressOf matchNewSelected)).Count <> 0
            Select Case CType(AlertsList.ItemValueA(AlertsList.selected), Alert).alertType
                Case AlertsManager.AType.OpenMailSystem
                    menuOpenMailSys.Visible = True
                Case AlertsManager.AType.OpenClientAccount
                    menuOpenClientAccount.Visible = True
                Case AlertsManager.AType.OpenNote
                    menuOpenNote.Visible = True
                    menuRenameNote.Visible = True
                    menuTransferNoteToSomeone.Visible = True
                Case AlertsManager.AType.OpenRapportGenerator
                    menuOpenReportGen.Visible = True
                Case AlertsManager.AType.ContinueQueueListe
                    menuContinueQueueList.Visible = True
            End Select
            menuDelMSG.Visible = True
            MenuLigne2.Visible = True
            MenuLigne3.Visible = True
        Else
            MenuLigne2.Visible = False
            MenuLigne3.Visible = False
            menuDelMSG.Visible = False
            menusetOld.Visible = False
        End If
    End Sub

    Private Sub menuDelMSG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDelMSG.Click
        Dim alertsNothing As Boolean = True
        Try
            Dim deletingAlerts As New Generic.List(Of Alert)
            For i As Integer = 0 To curSelectedItems.Count - 1
                deletingAlerts.Add(curSelectedItems(i).ValueA)
            Next i
            Dim delMessage As String = "Êtes-vous sûr de vouloir supprimer ce(s) message(s) instantané(s) et/ou cette(ces) note(s) personnelle(s) ?"
            If curSelectedItems.Count = 1 Then
                If TypeOf curAlert Is AlertOfPersoNote Then
                    delMessage = "Êtes-vous sûr de vouloir supprimer cette note personnelle ?"
                Else
                    delMessage = "Êtes-vous sûr de vouloir supprimer ce message instantané ?"
                End If
            End If

            If MessageBox.Show(delMessage, "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

            alertsNothing = True

            For i As Integer = 0 To deletingAlerts.Count - 1
                deletingAlerts(i).delete()
            Next i
            Me.loadAlerts()
        Catch ex As Exception 'REM Exception not handle
            addErrorLog(New Exception("alertsNothing=" & alertsNothing & vbCrLf & "AlertsList.Selected=" & AlertsList.selected & vbCrLf & "AlertsList.ItemText(AlertsList.Selected)=" & AlertsList.ItemText(AlertsList.selected), ex))
        End Try
    End Sub

    Private Sub doAlert(ByVal curAlert As Alert)
        If curAlert Is Nothing Then Exit Sub

        If curAlert.isNew Then
            AlertsList.ItemBackColor(AlertsList.selected) = AlertsList.baseBackColor
            AlertsList.ItemFont(AlertsList.selected) = New Font(AlertsList.ItemFont(AlertsList.selected), FontStyle.Regular)
            AlertsList.draw = True : AlertsList.draw = False
            Dim oldNew As Integer = nbNew
            nbNew -= 1
            curAlert.setOld()
            curAlert.saveData()

            If oldNew <> nbNew AndAlso Me.Parent IsNot Nothing AndAlso Me.Parent.Visible = True Then
                RaiseEvent barTitleChanged(Me)
            End If
        End If

        curAlert.doAction()
    End Sub

    Private Sub menuAlertsAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuOpenNote.Click, menuOpenClientAccount.Click, menuOpenMailSys.Click, menuOpenReportGen.Click
        doAlert(curAlert)
    End Sub

    Private Sub menuAddNote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAddNote.Click
        Dim noteTitleInput As InputBoxPlus = New InputBoxPlus(True, , "NotesTitles.Titles", "WHERE NotesTitles.NoUser=" & ConnectionsManager.currentUser, False)
        AddHandler noteTitleInput.comboDelete, AddressOf notesTitlesComboDelete
        noteTitleInput.firstLetterCapital = True
        noteTitleInput.refusedChars = ":"
        Dim noteTitle As String = noteTitleInput.Prompt("Veuillez choisir le titre de cette note personnelle", "Titre d'une note personnelle")
        RemoveHandler noteTitleInput.comboDelete, AddressOf notesTitlesComboDelete
        If noteTitle = "" Then Exit Sub

        DBHelper.addItemToADBList("NotesTitles", "Titles", noteTitle, "NoNotesTitles", 20, , , , "NoUser", ConnectionsManager.currentUser)
        Dim newAlert As AlertOfPersoNote = AlertsManager.getInstance.addAlert("", ConnectionsManager.currentUser, AlertsManager.AType.OpenNote, noteTitle, , , , , , False)
        With newAlert
            .setOld()
            .saveData()
            myMainWin.barMainObjects.addControl(.persoNote)
            If IsSwitchedToToolbar = False Then
                .persoNote.IsSwitchedToToolbar = False
                myMainWin.barMainObjects.mainObjectsPanel.Controls.Remove(.persoNote)
                myMainWin.Controls.Add(.persoNote)
            End If
            .persoNote.centerObject()
            .persoNote.isClosed = False
            .persoNote.focus()
        End With
    End Sub

    Private Sub menuRenameNote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuRenameNote.Click
        Dim noteTitleInput As InputBoxPlus = New InputBoxPlus(True, , "NotesTitles.Titles", "WHERE NotesTitles.NoUser=" & ConnectionsManager.currentUser, False)

        noteTitleInput.firstLetterCapital = True
        noteTitleInput.refusedChars = ":"
        Dim noteTitle As String = noteTitleInput.Prompt("Veuillez choisir le titre de cette note personnelle", "Titre d'une note personnelle", AlertsList.ItemText(AlertsList.selected))
        If noteTitle = "" Then Exit Sub

        DBHelper.addItemToADBList("NotesTitles", "Titles", noteTitle, "NoNotesTitles", 20, , , , "NoUser", ConnectionsManager.currentUser)

        Dim curAlert As AlertOfPersoNote = Me.curAlert

        curAlert.persoNote.title = noteTitle
        curAlert.persoNote.saveNote()

        AlertsList.ItemText(AlertsList.selected) = noteTitle
        AlertsList.draw = True : AlertsList.draw = False
    End Sub

    Private Sub notesTitlesComboDelete(ByVal currentItem As String)
        DBHelper.removeItemToADBList("NotesTitles", "Titles", currentItem, "NoNotesTitles", , , ConnectionsManager.currentUser)
    End Sub

    Private Sub alertsList_DblClick(ByVal sender As Object, ByVal e As CI.Controls.List.DblClickEventArgs) Handles AlertsList.dblClick
        If AlertsList.selected = -1 Then Exit Sub

        doAlert(curAlert)
    End Sub

    Private Sub instantMessages_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        If myMainWin IsNot Nothing Then myMainWin.menuAlert.Checked = Not Me.isClosed
    End Sub

    Private Sub controlBox_ClosingControl() Handles ControlBox.closingControl
        Me.visible = False
        myMainWin.menuAlert.Checked = False
        RaiseEvent closing(Me)
    End Sub

    Private Sub controlBox_LockingControl(ByVal willBeLocked As Boolean) Handles ControlBox.lockingControl
        If willBeLocked = True Then
            Title.Cursor = Cursors.Default
        Else
            Title.Cursor = movingCursor
        End If
    End Sub

    Public Function getCoord() As System.Drawing.Point Implements IMovableObject.getCoord
        Return Me.Location
    End Function

    Public Sub setCoord(ByVal newCoord As System.Drawing.Point) Implements IMovableObject.setCoord
        Me.Location = newCoord
    End Sub

    Public Sub setMovability(ByVal movable As Boolean) Implements IMovableObject.setMovability
        'Me.Left = 0
        ControlBox.setLockability(movable)
    End Sub

    Private Sub menuContinueQueueList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuContinueQueueList.Click
        Dim curAlert As AlertOfQueueList = Me.curAlert
        'Get NoAgenda & continue QL
        Dim myNoAgenda() As String = DBLinker.getInstance.readOneDBField("Agenda", "NoAgenda", "WHERE DateHeure='" & DateFormat.getTextDate(curAlert.dateToLook, DateFormat.TextDateOptions.YYYYMMDD) & " " & DateFormat.getTextDate(curAlert.dateToLook, DateFormat.TextDateOptions.ShortTime) & "' AND NoStatut=7 AND NoTRP = " & curAlert.noUser)
        If myNoAgenda Is Nothing OrElse myNoAgenda.Length = 0 Then
            DBLinker.getInstance.delDB("UsersAlerts", "NoUserAlert", curAlert.noAlert, False)
            loadAlerts()
        Else
            doAlert(curAlert)
        End If
    End Sub

    Private Sub alertsList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles AlertsList.resize
        Me.SuspendLayout()
        'Dim OldPos As Point = GetCoord()
        Me.Height = AlertsList.Height + 26
        Me.Width = AlertsList.Width
        Title.Height = Me.Height
        'SetCoord(OldPos)
        Me.ResumeLayout()
    End Sub

    Public Function getBarTitle() As String Implements IControllable.getBarTitle
        If nbNew = 0 Then Return "Message(s) instantané(s)"

        Return "Message(s) instantané(s) (" & nbNew & ")"
    End Function

    Public Event barTitleChanged(ByVal sender As IControllable) Implements IControllable.barTitleChanged

    Public ReadOnly Property hasToBlink() As Boolean Implements IControllable.hasToBlink
        Get
            Return nbNew > 0
        End Get
    End Property

    Public Property IsSwitchedToToolbar(Optional ByVal showPanel As Boolean = True) As Boolean Implements IControllable.isSwitchedToToolbar
        Get
            Return ControlBox.IsSwitchedToToolbar
        End Get
        Set(ByVal value As Boolean)
            ControlBox.IsSwitchedToToolbar(showPanel) = value
        End Set
    End Property

    Public Event switchingControl(ByVal sender As IControllable, ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean) Implements IControllable.switchingControl

    Private Sub controlBox_SwitchingControl(ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean) Handles ControlBox.switchingControl
        RaiseEvent switchingControl(Me, willBeSwitchedToToolBar, showPanel)
        setCoord(ensureGoodCoord(Me.Top, Me.Left, willBeSwitchedToToolBar))
    End Sub

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

    Private Sub menusetOld_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menusetOld.Click
        Try
            For Each curItem As CI.Controls.IListItem In AlertsList.selectedItems
                With CType(curItem.ValueA, Alert)
                    .setOld()
                    .saveData()
                End With
            Next
        Catch ex As Exception
            addErrorLog(New Exception("bug testing", ex))
        End Try

        Me.loadAlerts()
    End Sub

    Private Sub menuAllOld_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAllOld.Click
        AlertsManager.getInstance.setAllAlertsAsOld()
        For i As Integer = 0 To AlertsList.listCount - 1
            CType(AlertsList.ItemValueA(i), Alert).saveData()
        Next i
        Me.loadAlerts()
    End Sub

    Public Overloads Sub focus() Implements IControllable.focus
        MyBase.Focus()
    End Sub

    Private Sub menuTransferAllNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuTransferAllNotes.Click
        For Each curNotes As PersoNote In AlertsManager.getInstance.getNotes
            If curNotes.isClosed = False AndAlso curNotes.IsSwitchedToToolbar = False Then curNotes.IsSwitchedToToolbar = True
        Next
    End Sub

    Private Sub menuHideAllNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuHideAllNotes.Click
        For Each curNotes As PersoNote In AlertsManager.getInstance.getNotes
            If curNotes.isClosed = False Then curNotes.isClosed = True
        Next
    End Sub

    Private Sub menuTransferNoteToSomeone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuTransferNoteToSomeone.Click
        Dim choosedUser As User = UsersManager.getInstance.chooseUser(False, False, , , True, False, False)
        If choosedUser Is Nothing Then Exit Sub

        AlertsManager.getInstance.transferAlertTo(curAlert.noAlert, choosedUser.noUser, curAlert.noUser)
    End Sub


    Public Function getNotesSettings() As String
        Dim persoSettings As String = ""
        For Each curPersoNote As PersoNote In AlertsManager.getInstance.getNotes
            persoSettings &= "§" & curPersoNote.alertAssociated.noAlert & ":" & (Not curPersoNote.isClosed) & ":" & curPersoNote.Height & ":" & curPersoNote.Top & ":" & curPersoNote.Left & ":" & curPersoNote.blockMove & ":" & curPersoNote.IsSwitchedToToolbar
        Next
        If persoSettings <> "" Then persoSettings = persoSettings.Substring(1)

        Return persoSettings
    End Function

    Public Sub applyNotesSettings()
        If UsersManager.currentUser Is Nothing Then Exit Sub

        'PersoNotes Settings
        Dim setting As String = UsersManager.currentUser.settings.persoNotes
        If setting <> "" Then
            Dim lineSplit() As String = Split(setting, "§")
            Dim persoSettings() As String
            For i As Integer = 0 To lineSplit.Length - 1
                persoSettings = lineSplit(i).Split(New Char() {":"})
                For Each curPersoNote As PersoNote In AlertsManager.getInstance.getNotes
                    If curPersoNote.noAlert = persoSettings(0) Then
                        curPersoNote.visible = persoSettings(1)
                        curPersoNote.Height = persoSettings(2)
                        curPersoNote.blockMove = persoSettings(5)
                        If curPersoNote.IsSwitchedToToolbar = True Then
                            myMainWin.barMainObjects.addControl(curPersoNote)
                        Else
                            myMainWin.Controls.Add(curPersoNote)
                        End If
                        curPersoNote.IsSwitchedToToolbar(False) = persoSettings(6)
                        curPersoNote.Top = persoSettings(3)
                        curPersoNote.Left = persoSettings(4)
                        Exit For
                    End If
                Next
            Next i
        End If
    End Sub

    Public Overrides Sub dataConsuming(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function <> "Alerts" OrElse dataReceived.params(0) <> ConnectionsManager.currentUser OrElse dataReceived.params.Length < 2 Then Exit Sub

        Dim showWin As Boolean = True
        Dim myAlert As Alert = AlertsManager.getInstance.getItemable(dataReceived.params(1))
        'Condition added to test the bug below
        '        Date :2012-01-19 16:32:31
        'Version de Clinica : 1.2.1201.219
        'Ordinateur: POSTE001()
        '        Utilisateur(Windows) : PHYSIOTECH(-LG / sylvielessard)
        'Utilisateur Clinica : 1/55/Allard,Véronique/110110110111111011111111011010110010001101010101101001111111110000010111011111000000000101001000010011110000
        'HasRestarted : False
        '        Error Type : System.Exception()
        'Message:
        'Application level exception

        'Exception Stack Trace :
        '        Trace(Not available)

        'Environment stack trace :
        '   at System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
        '        at(System.Environment.get_StackTrace())
        '   at CommonProc.AddErrorLog(Exception ErrorMsg, Byte InternalCount) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 497
        '   at CommonProc.Application_ThreadException(Object sender, ThreadExceptionEventArgs e) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 392
        '   at System.Windows.Forms.Application.ThreadContext.OnThreadException(Exception t)
        '        at(System.Windows.Forms.Control.InvokeMarshaledCallbacks())
        '   at System.Windows.Forms.Control.WndProc(Message& m)
        '   at System.Windows.Forms.ScrollableControl.WndProc(Message& m)
        '   at System.Windows.Forms.ContainerControl.WndProc(Message& m)
        '   at System.Windows.Forms.UserControl.WndProc(Message& m)
        '   at System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
        '   at System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
        '   at System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
        '   at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG& msg)
        '   at System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(Int32 dwComponentID, Int32 reason, Int32 pvLoopData)
        '   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
        '   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
        '        at(System.Windows.Forms.Application.Run())
        '   at Software.Start() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Layer 2 - Software\§ Main\Software.vb:line 471
        '   at CommonProc.Main() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 372
        'InnerException 1 --->
        '        Error Type : System.NullReferenceException()
        'Message:
        '   Object reference not set to an instance of an object.

        'Source:
        '   Clinica -- Void DataConsuming(DataInternalUpdate)

        '   Exception Stack Trace :
        '      at InstantMessages.DataConsuming(DataInternalUpdate dataReceived) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Layer 1 - Presentation\ControlsBuilt\InstantMessages.vb:line 841

        '   Environment stack trace :
        '      at System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
        '        at(System.Environment.get_StackTrace())
        '      at CommonProc.AddErrorLog(Exception ErrorMsg, Byte InternalCount) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 497
        '      at CommonProc.Application_ThreadException(Object sender, ThreadExceptionEventArgs e) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 392
        '      at System.Windows.Forms.Application.ThreadContext.OnThreadException(Exception t)
        '        at(System.Windows.Forms.Control.InvokeMarshaledCallbacks())
        '      at System.Windows.Forms.Control.WndProc(Message& m)
        '      at System.Windows.Forms.ScrollableControl.WndProc(Message& m)
        '      at System.Windows.Forms.ContainerControl.WndProc(Message& m)
        '      at System.Windows.Forms.UserControl.WndProc(Message& m)
        '      at System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
        '      at System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
        '      at System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
        '      at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG& msg)
        '      at System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(Int32 dwComponentID, Int32 reason, Int32 pvLoopData)
        '      at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
        '      at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
        '        at(System.Windows.Forms.Application.Run())
        '      at Software.Start() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Layer 2 - Software\§ Main\Software.vb:line 471
        '      at CommonProc.Main() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 372
        '   InnerException 2 --->
        '        Aucune()
        '--------------------------------------------------------------------
        If myAlert Is Nothing Then
            Dim testData() As String = DBLinker.getInstance.readOneDBField("UsersAlerts", "NoUserAlert", "WHERE NoUserAlert=" & dataReceived.params(1))
            Dim dataExists As Boolean = (testData IsNot Nothing AndAlso testData.Length <> 0 AndAlso testData(0) = dataReceived.params(1))
            addErrorLog(New Exception("Testing null alert on receiving on update of this alert. Present in DB : " & dataExists & ", dataReceived.noMessage=" & dataReceived.noMessage & ", AlertManager.getInstance.lastNoUpdate=" & AlertsManager.getInstance.lastNoUpdate & ", AlertManager.getInstance.lastNoUpdateDone=" & AlertsManager.getInstance.lastNoUpdateDone & ", Same Thread:" & (AlertsManager.getInstance.lastNoUpdateThreadId = Threading.Thread.CurrentThread.ManagedThreadId)))

            If Not dataExists Then Exit Sub

            AlertsManager.getInstance.load()
            myAlert = AlertsManager.getInstance.getItemable(dataReceived.params(1))
        End If

        'Determine if the object has to be shown or not
        If myAlert.showingDate.Equals(LIMIT_DATE) = False AndAlso date1Infdate2(myAlert.showingDate, Date.Now, True) = False Then showWin = False
        If myAlert.isHidden Then showWin = False
        If visible = False And showWin Then visible = True

        Me.loadAlerts(False)
    End Sub

    Private Sub AlertsList_willSelect(ByVal sender As Object, ByVal e As Controls.List.WillSelectEventArgs) Handles AlertsList.willSelect
        
    End Sub

    Private Sub AlertsList_selectedChange() Handles AlertsList.selectedChange
        curAlert = If(AlertsList.selected = -1, Nothing, AlertsList.items(AlertsList.selected).ValueA)
        curSelectedItems = AlertsList.selectedItems
    End Sub
End Class
