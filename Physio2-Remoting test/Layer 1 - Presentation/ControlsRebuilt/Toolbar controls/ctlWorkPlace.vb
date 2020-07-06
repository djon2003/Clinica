Imports System.Windows.Forms

Public Class ctlWorkPlace
    Inherits ContainerControl

    Private _IsPanelBlocked As Boolean = False
    Private _IsMovingPanel As Boolean = False
    Private _TextShowedCharByCharVertically As Boolean = False
    Private WithEvents barMainObjects As System.Windows.Forms.ToolStrip
    Private WithEvents openSidePanel As System.Windows.Forms.ToolStripButton
    Private WithEvents openSidePanel2 As System.Windows.Forms.ToolStripButton
    Public WithEvents mainObjectsPanel As System.Windows.Forms.Panel
    Friend WithEvents objectMenu As System.Windows.Forms.ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents menuOutBar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuClose As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents tcSideToolBar As Clinica.TransparentControl

    Public Event barShowed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event barHidden(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        htControls.Clear()
        initSideBar()

        InitializeComponent()
    End Sub

    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        If disposing Then htControls.Clear()
    End Sub

    Public Property textShowedCharByCharVertically() As Boolean
        Get
            Return _TextShowedCharByCharVertically
        End Get
        Set(ByVal value As Boolean)
            _TextShowedCharByCharVertically = value
        End Set
    End Property

    Public Property isPanelBlocked() As Boolean
        Get
            Return _IsPanelBlocked
        End Get
        Set(ByVal value As Boolean)
            _IsPanelBlocked = value
        End Set
    End Property

    Public ReadOnly Property isPanelVisible() As Boolean
        Get
            Return mainObjectsPanel.Visible
        End Get
    End Property

    Public ReadOnly Property barWidth() As Integer
        Get
            Return Me.barMainObjects.Width
        End Get
    End Property

    Public ReadOnly Property isBarVisible() As Boolean
        Get
            Return Me.barMainObjects.Visible
        End Get
    End Property

    Public ReadOnly Property isBarVisibleForced() As Boolean
        Get
            If Me.barMainObjects.Visible = False Then Return False

            For Each curControllable As IControllable In htControls.Keys
                If curControllable.hasToBlink AndAlso curControllable.isSwitchedToToolbar AndAlso curControllable.isClosed = False Then Return True
            Next

            Return False
        End Get
    End Property

    Public Property isMovingPanel() As Boolean
        Get
            Return _IsMovingPanel
        End Get
        Set(ByVal value As Boolean)
            _IsMovingPanel = value
        End Set
    End Property

    Public Function getBarWidth() As Integer
        Return Me.barMainObjects.Width
    End Function

    Private Sub barTitleChanged(ByVal sender As IControllable)
        With CType(htControls(sender), ToolStripItem)
            .Text = sender.getBarTitle
            If sender.hasToBlink Then .BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))

            If sender.hasToBlink And sender.isClosed = False And sender.isSwitchedToToolbar = True Then
                Me.visible = True
                If sender.isSwitchedToToolbar Then .Visible = True
                For i As Byte = 1 To 4
                    Threading.Thread.Sleep(200)
                    If i Mod 2 = 0 Then
                        .BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
                    Else
                        .BackColor = System.Drawing.SystemColors.AppWorkspace
                    End If
                    Application.DoEvents()
                Next i
            End If
        End With
    End Sub

    Private htControls As New Hashtable

    Public Overloads Property visible() As Boolean
        Get
            Return barMainObjects.Visible
        End Get
        Set(ByVal value As Boolean)
            If value Then
                showBar()
            Else
                hideBar()
            End If
        End Set
    End Property

    Public Sub hideBar()
        If mainObjectsPanel.Visible Then hidePanel()
        For Each CurControl As IControllable In htControls.Keys
            If CurControl.hasToBlink = True AndAlso CurControl.isSwitchedToToolbar Then Exit Sub
            CType(htControls(CurControl), ToolStripItem).BackColor = SystemColors.AppWorkspace
        Next

        Me.barMainObjects.Visible = False
        Me.tcSideToolBar.Visible = True
        Me.Width = Me.tcSideToolBar.Width

        RaiseEvent barHidden(Me, EventArgs.Empty)
    End Sub

    Delegate Sub delAddControl(ByVal newControl As IControllable)
    Public myAddControl As delAddControl

    Public Sub addControl(ByVal newControl As IControllable)
        If htControls.ContainsKey(newControl) Then Exit Sub
        If Me.InvokeRequired Then
            Me.Invoke(Me.myAddControl, New Object() {newControl})
            Exit Sub
        End If

        Me.mainObjectsPanel.Controls.Add(newControl)
        Dim newTSI As ToolStripItem
        If _TextShowedCharByCharVertically Then
            newTSI = New ToolStripButtonPlus(newControl.getBarTitle, Nothing, New EventHandler(AddressOf openSidePanel_Click))
        Else
            newTSI = New ToolStripButton(newControl.getBarTitle, Nothing, New EventHandler(AddressOf openSidePanel_Click))
        End If
        newTSI.Text = newControl.getBarTitle()
        htControls.Add(newControl, Me.barMainObjects.Items(Me.barMainObjects.Items.Add(newTSI)))
        Dim tsi As ToolStripItem = CType(htControls(newControl), ToolStripItem)
        tsi.TextDirection = ToolStripTextDirection.Vertical90
        tsi.Visible = False
        tsi.Tag = newControl
        AddHandler tsi.MouseUp, AddressOf tsiMouseClick
        AddHandler newControl.barTitleChanged, AddressOf barTitleChanged
        Dim normalControl As Control = CType(newControl, Control)
        AddHandler normalControl.VisibleChanged, AddressOf controlVisibleChanged
        AddHandler newControl.switchingControl, AddressOf controlBox_SwitchingControl
        AddHandler newControl.closing, AddressOf controlClosing
        refreshBar()
    End Sub

    Private lastClickControlTSI As Control

    Private Sub tsiMouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            lastClickControlTSI = CType(sender, ToolStripItem).Tag
            menuLock.Checked = CType(lastClickControlTSI, IMovableObject).blockMove

            objectMenu.Show(Control.MousePosition.X, Control.MousePosition.Y)
        Else
            For Each curControl As IControllable In Me.mainObjectsPanel.Controls
                If htControls(curControl).Equals(sender) Then curControl.focus()
            Next
        End If
    End Sub

    Private Sub controlClosing(ByVal sender As IControllable)
        CType(htControls(sender), ToolStripItem).Visible = False
    End Sub

    Private Sub controlVisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
        'If Me.MainObjectsPanel.Visible = False Then Exit Sub
        If CType(sender, Control).Parent Is Nothing Then Exit Sub

        With CType(sender, IControllable)
            CType(htControls(sender), ToolStripItem).Visible = Not .isClosed AndAlso .isSwitchedToToolbar
        End With

        barTitleChanged(sender)
    End Sub

    Private Sub initSideBar()
        'Side bar
        Me.tcSideToolBar = New TransparentControl(Nothing)
        'Set SideToolBar visibility & the transparent control supportting his viewability
        Me.tcSideToolBar.Dock = DockStyle.Right
        Me.tcSideToolBar.Width = 2
        Me.Controls.Add(Me.tcSideToolBar)
    End Sub

    Private Sub tcSideToolBar_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcSideToolBar.MouseEnter
        showBar()
    End Sub

    Public Sub showBar()
        Me.barMainObjects.Visible = True
        Me.tcSideToolBar.Visible = False
        Me.Width = Me.barMainObjects.Width
        Me.refreshBar()

        RaiseEvent barShowed(Me, EventArgs.Empty)
    End Sub

    Public Sub showPanel()
        If Me.Dock = DockStyle.Fill Or Me._IsPanelBlocked Then Exit Sub 'Already opened

        _IsMovingPanel = True
        openSidePanel.Text = ">>"
        openSidePanel.ToolTipText = "Cacher"
        openSidePanel2.Text = ">>"
        openSidePanel2.ToolTipText = "Cacher"
        Me.BringToFront()
        Dim nbIterations As Integer = Math.Floor((Me.Parent.Width - Me.barMainObjects.Width) / 10)
        For i As Integer = 1 To nbIterations
            Me.Width = Me.barMainObjects.Width + i * 10
            Application.DoEvents()
        Next i
        Me.Dock = DockStyle.Fill
        mainObjectsPanel.Visible = True
        Me.Width = Me.barMainObjects.Width
        refreshBar()
        showBar()
        _IsMovingPanel = False
    End Sub

    Private Sub refreshBar()
        For Each CurControl As IControllable In htControls.Keys
            With CType(htControls(CurControl), ToolStripItem)
                If CurControl.isClosed = False AndAlso (CType(CurControl, Control).Parent IsNot Nothing AndAlso CType(CurControl, Control).Parent.Equals(mainObjectsPanel) = True) Then
                    .Visible = True
                Else
                    .Visible = False
                End If
                .TextDirection = ToolStripTextDirection.Vertical270
            End With
        Next
    End Sub

    Public Sub hidePanel()
        mainObjectsPanel.Visible = False
        openSidePanel.Text = "<<"
        openSidePanel.ToolTipText = "Afficher"
        openSidePanel2.Text = "<<"
        openSidePanel2.ToolTipText = "Afficher"
        Me.Dock = DockStyle.Right
        Me.refreshBar()
    End Sub

    Private Sub openSidePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles openSidePanel.Click, openSidePanel2.Click
        If Me.Dock <> DockStyle.Fill Then
            showPanel()
        Else
            hidePanel()
        End If
    End Sub

    Private Sub controlBox_SwitchingControl(ByVal sender As IControllable, ByVal willBeSwitchedToToolBar As Boolean, ByVal showPanel As Boolean)
        CType(htControls(sender), ToolStripItem).Visible = willBeSwitchedToToolBar
        If willBeSwitchedToToolBar Then
            CType(sender, Control).Parent = myMainWin.barMainObjects.mainObjectsPanel
            If showPanel And PreferencesManager.getUserPreferences()("AutoOpenSideBarOnTransfer") = True Then
                Me.showPanel()
            Else
                If sender.hasToBlink Then Me.visible = True
            End If
        Else
            CType(sender, Control).Parent = myMainWin
            If showPanel And PreferencesManager.getUserPreferences()("AutoCloseSideBarOnTransfer") = True Then Me.hidePanel()
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.objectMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuOutBar = New System.Windows.Forms.ToolStripMenuItem
        Me.menuLock = New System.Windows.Forms.ToolStripMenuItem
        Me.menuClose = New System.Windows.Forms.ToolStripMenuItem
        Me.objectMenu.SuspendLayout()
        Me.SuspendLayout()
        Me.barMainObjects = New System.Windows.Forms.ToolStrip
        Me.openSidePanel = New System.Windows.Forms.ToolStripButton
        Me.openSidePanel2 = New System.Windows.Forms.ToolStripButton
        Me.mainObjectsPanel = New System.Windows.Forms.Panel
        Me.barMainObjects.SuspendLayout()
        '
        'BarMainObjects
        '
        Me.barMainObjects.Dock = System.Windows.Forms.DockStyle.Left
        Me.barMainObjects.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.barMainObjects.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.openSidePanel, Me.openSidePanel2})
        Me.barMainObjects.Location = New System.Drawing.Point(728, 49)
        Me.barMainObjects.Name = "BarMainObjects"
        Me.barMainObjects.Size = New System.Drawing.Size(32, 298)
        Me.barMainObjects.TabIndex = 21
        Me.barMainObjects.Text = "ToolStrip1"
        '
        'OpenSidePanel
        '
        Me.openSidePanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.openSidePanel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.openSidePanel.Name = "OpenSidePanel"
        Me.openSidePanel.Size = New System.Drawing.Size(29, 17)
        Me.openSidePanel.Text = "<<"
        Me.openSidePanel.ToolTipText = "Afficher"
        '
        'OpenSidePanel
        '
        Me.openSidePanel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.openSidePanel2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.openSidePanel2.Name = "OpenSidePanel"
        Me.openSidePanel2.Alignment = ToolStripItemAlignment.Right
        Me.openSidePanel2.Overflow = ToolStripItemOverflow.Never
        Me.openSidePanel2.Size = New System.Drawing.Size(29, 17)
        Me.openSidePanel2.Text = "<<"
        Me.openSidePanel2.Alignment = ToolStripItemAlignment.Right
        Me.openSidePanel2.ToolTipText = "Afficher"
        '
        'MainObjectsPanel
        '
        Me.mainObjectsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainObjectsPanel.Location = New System.Drawing.Point(0, 0)
        Me.mainObjectsPanel.Name = "MainObjectsPanel"
        Me.mainObjectsPanel.Size = New System.Drawing.Size(760, 347)
        Me.mainObjectsPanel.TabIndex = 23
        Me.mainObjectsPanel.Visible = False
        Me.Controls.Add(Me.barMainObjects)
        Me.Controls.Add(Me.mainObjectsPanel)
        Me.barMainObjects.ResumeLayout(False)
        Me.barMainObjects.PerformLayout()
        '
        'objectMenu
        '
        Me.objectMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuOutBar, Me.menuLock, Me.menuClose})
        Me.objectMenu.Name = "objectMenu"
        Me.objectMenu.Size = New System.Drawing.Size(208, 70)
        '
        'menuOutBar
        '
        Me.menuOutBar.Name = "menuOutBar"
        Me.menuOutBar.Size = New System.Drawing.Size(207, 22)
        Me.menuOutBar.Text = "Sortir de la bar de côté"
        '
        'menuLock
        '
        Me.menuLock.Name = "menuLock"
        Me.menuLock.Size = New System.Drawing.Size(207, 22)
        Me.menuLock.Text = "Empêcher le déplacement"
        '
        'menuClose
        '
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Size = New System.Drawing.Size(207, 22)
        Me.menuClose.Text = "Cacher"
        Me.objectMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private Sub menuClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuClose.Click
        CType(lastClickControlTSI, IControllable).visible = False
    End Sub

    Private Sub menuLock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuLock.Click
        CType(lastClickControlTSI, IMovableObject).blockMove = Not CType(lastClickControlTSI, IMovableObject).blockMove
    End Sub

    Private Sub menuOutBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOutBar.Click
        CType(lastClickControlTSI, IControllable).isSwitchedToToolbar = False
    End Sub

    Private Sub mainObjectsPanel_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles mainObjectsPanel.ControlAdded
        refreshBar()
    End Sub

    Private Sub mainObjectsPanel_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles mainObjectsPanel.ControlRemoved
        refreshBar()
    End Sub

    Private Sub barMainObjects_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles barMainObjects.Layout
        'REM OverFlow elements showed horizontally 
        'For Each curObjectButton As ToolStripItem In BarMainObjects.Items
        '    If TypeOf curObjectButton Is ToolStripButtonPlus Then
        '        With CType(curObjectButton, ToolStripButtonPlus)
        '            .TextShowedCharByCharVertically = .TextShowedCharByCharVertically 'Refresh the changes applied with this property
        '            .Overflow = ToolStripItemOverflow.AsNeeded
        '        End With
        '    End If
        'Next
    End Sub

    Private Sub barMainObjects_LayoutCompleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles barMainObjects.LayoutCompleted
        'REM OverFlow elements showed horizontally 
        'BarMainObjects.SuspendLayout()

        'For Each curObjectButton As ToolStripItem In BarMainObjects.Items
        '    If TypeOf curObjectButton Is ToolStripButtonPlus Then
        '        With CType(curObjectButton, ToolStripButtonPlus)
        '            .Invalidate()
        '            If .IsOnOverflow Then
        '                .Overflow = ToolStripItemOverflow.Always
        '                .TextDirection = ToolStripTextDirection.Horizontal
        '                .AutoSize = True
        '                .ForeColor = Color.Black
        '            Else
        '                .AutoSize = Not _TextShowedCharByCharVertically
        '                .TextDirection = ToolStripTextDirection.Vertical270
        '                .ForeColor = .BackColor
        '            End If
        '        End With
        '    End If
        'Next
        'BarMainObjects.ResumeLayout(False)
    End Sub
End Class
