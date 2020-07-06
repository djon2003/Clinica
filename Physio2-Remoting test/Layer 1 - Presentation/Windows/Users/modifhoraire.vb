Option Strict Off
Option Explicit On
Friend Class modifhoraire
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.
        Me.MdiParent = myMainWin

        boldFont = New Font(colorActif.Font, FontStyle.Bold)
        normalFont = New Font(colorPassif.Font, FontStyle.Regular)
        winLoaded = False

        colorPassif.BackColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorHoraireClose"))
        colorActif.BackColor = System.Drawing.ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorHoraireOpen"))
        grid.scheduleOff = colorPassif.BackColor
        grid.scheduleOn = colorActif.BackColor
        colorPassif.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        colorActif.BorderStyle = System.Windows.Forms.BorderStyle.None
        colorActif.ForeColor = colorPassif.BackColor
        colorPassif.ForeColor = colorActif.BackColor

        'Chargement des images
        Me.enlever.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.encours.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("default.ico"), New Size(16, 16))
        Me.enregistrer.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.saveas.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("saveas.ico"), New Size(16, 16))
        Me.print.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("print16.ico"), New Size(16, 16))
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            boldFont.Dispose()
            normalFont.Dispose()

            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public toolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents label2 As BaseObjArray
    Public WithEvents enlever As System.Windows.Forms.Button
    Public WithEvents encours As System.Windows.Forms.Button
    Public WithEvents saveas As System.Windows.Forms.Button
    Public WithEvents horairelist As System.Windows.Forms.ComboBox
    Public WithEvents enregistrer As System.Windows.Forms.Button
    Public WithEvents colorPassif As System.Windows.Forms.Label
    Public WithEvents colorActif As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents grid As Clinica.TimeGrid
    Public WithEvents print As System.Windows.Forms.Button
    Public WithEvents hoursTotal As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    '       <System.Diagnostics.DebuggerStepThrough()> 
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.enlever = New System.Windows.Forms.Button
        Me.encours = New System.Windows.Forms.Button
        Me.saveas = New System.Windows.Forms.Button
        Me.enregistrer = New System.Windows.Forms.Button
        Me.print = New System.Windows.Forms.Button
        Me.grid = New TimeGrid
        Me.horairelist = New System.Windows.Forms.ComboBox
        Me.colorPassif = New System.Windows.Forms.Label
        Me.colorActif = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.hoursTotal = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'toolTip1
        '
        Me.toolTip1.ShowAlways = True
        '
        'enlever
        '
        Me.enlever.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.enlever.BackColor = System.Drawing.SystemColors.Control
        Me.enlever.Cursor = System.Windows.Forms.Cursors.Default
        Me.enlever.Enabled = False
        Me.enlever.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.enlever.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.enlever.ForeColor = System.Drawing.SystemColors.ControlText
        Me.enlever.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.enlever.Location = New System.Drawing.Point(595, 481)
        Me.enlever.Name = "enlever"
        Me.enlever.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.enlever.Size = New System.Drawing.Size(24, 24)
        Me.enlever.TabIndex = 601
        Me.toolTip1.SetToolTip(Me.enlever, "Supprimer l'horaire en cours")
        Me.enlever.UseVisualStyleBackColor = False
        '
        'encours
        '
        Me.encours.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.encours.BackColor = System.Drawing.SystemColors.Control
        Me.encours.Cursor = System.Windows.Forms.Cursors.Default
        Me.encours.Enabled = False
        Me.encours.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.encours.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.encours.ForeColor = System.Drawing.SystemColors.ControlText
        Me.encours.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.encours.Location = New System.Drawing.Point(563, 481)
        Me.encours.Name = "encours"
        Me.encours.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.encours.Size = New System.Drawing.Size(24, 24)
        Me.encours.TabIndex = 603
        Me.toolTip1.SetToolTip(Me.encours, "Enregistrer l'horaire en cours sur l'horaire par défaut")
        Me.encours.UseVisualStyleBackColor = False
        '
        'saveas
        '
        Me.saveas.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.saveas.BackColor = System.Drawing.SystemColors.Control
        Me.saveas.Cursor = System.Windows.Forms.Cursors.Default
        Me.saveas.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.saveas.Font = New System.Drawing.Font("Arial", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveas.ForeColor = System.Drawing.SystemColors.ControlText
        Me.saveas.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.saveas.Location = New System.Drawing.Point(531, 481)
        Me.saveas.Name = "saveas"
        Me.saveas.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.saveas.Size = New System.Drawing.Size(24, 24)
        Me.saveas.TabIndex = 600
        Me.toolTip1.SetToolTip(Me.saveas, "Enregistrer sous une autre semaine l'horaire en cours")
        Me.saveas.UseVisualStyleBackColor = False
        '
        'enregistrer
        '
        Me.enregistrer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.enregistrer.BackColor = System.Drawing.SystemColors.Control
        Me.enregistrer.Cursor = System.Windows.Forms.Cursors.Default
        Me.enregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.enregistrer.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.enregistrer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.enregistrer.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.enregistrer.Location = New System.Drawing.Point(499, 481)
        Me.enregistrer.Name = "enregistrer"
        Me.enregistrer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.enregistrer.Size = New System.Drawing.Size(24, 24)
        Me.enregistrer.TabIndex = 4
        Me.toolTip1.SetToolTip(Me.enregistrer, "Enregistrer l'horaire en cours")
        Me.enregistrer.UseVisualStyleBackColor = False
        '
        'print
        '
        Me.print.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.print.BackColor = System.Drawing.SystemColors.Control
        Me.print.Cursor = System.Windows.Forms.Cursors.Default
        Me.print.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.print.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.print.ForeColor = System.Drawing.SystemColors.ControlText
        Me.print.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.print.Location = New System.Drawing.Point(625, 481)
        Me.print.Name = "print"
        Me.print.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.print.Size = New System.Drawing.Size(24, 24)
        Me.print.TabIndex = 601
        Me.toolTip1.SetToolTip(Me.print, "Imprimer l'horaire en cours")
        Me.print.UseVisualStyleBackColor = False
        '
        'grid
        '
        Me.grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.grid.isDrawingScheduleOn = False
        Me.grid.Location = New System.Drawing.Point(1, 1)
        Me.grid.MinimumSize = New System.Drawing.Size(624, 135)
        Me.grid.Name = "grid"
        Me.grid.scheduleBlocked = System.Drawing.Color.Black
        Me.grid.scheduleOff = System.Drawing.Color.LightGray
        Me.grid.scheduleOn = System.Drawing.Color.Blue
        Me.grid.Size = New System.Drawing.Size(656, 474)
        Me.grid.startingTime = New Date(2000, 1, 1, 6, 0, 0, 0)
        Me.grid.TabIndex = 604
        '
        'horairelist
        '
        Me.horairelist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.horairelist.BackColor = System.Drawing.SystemColors.Window
        Me.horairelist.Cursor = System.Windows.Forms.Cursors.Default
        Me.horairelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.horairelist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.horairelist.ForeColor = System.Drawing.SystemColors.WindowText
        Me.horairelist.Location = New System.Drawing.Point(387, 481)
        Me.horairelist.Name = "horairelist"
        Me.horairelist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.horairelist.Size = New System.Drawing.Size(105, 22)
        Me.horairelist.TabIndex = 599
        '
        'colorPassif
        '
        Me.colorPassif.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.colorPassif.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.colorPassif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.colorPassif.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorPassif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorPassif.ForeColor = System.Drawing.Color.Blue
        Me.colorPassif.Location = New System.Drawing.Point(147, 478)
        Me.colorPassif.Name = "colorPassif"
        Me.colorPassif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorPassif.Size = New System.Drawing.Size(140, 25)
        Me.colorPassif.TabIndex = 2
        Me.colorPassif.Text = "Plage d'horaire fermée"
        Me.colorPassif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'colorActif
        '
        Me.colorActif.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.colorActif.BackColor = System.Drawing.Color.Blue
        Me.colorActif.Cursor = System.Windows.Forms.Cursors.Default
        Me.colorActif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colorActif.ForeColor = System.Drawing.Color.White
        Me.colorActif.Location = New System.Drawing.Point(4, 478)
        Me.colorActif.Name = "colorActif"
        Me.colorActif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.colorActif.Size = New System.Drawing.Size(140, 25)
        Me.colorActif.TabIndex = 0
        Me.colorActif.Text = "Plage d'horaire ouverte"
        Me.colorActif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(296, 478)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(94, 14)
        Me.label1.TabIndex = 295
        Me.label1.Text = "Heures totales :"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'hoursTotal
        '
        Me.hoursTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.hoursTotal.BackColor = System.Drawing.SystemColors.Control
        Me.hoursTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.hoursTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hoursTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.hoursTotal.Location = New System.Drawing.Point(296, 492)
        Me.hoursTotal.Name = "hoursTotal"
        Me.hoursTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hoursTotal.Size = New System.Drawing.Size(80, 14)
        Me.hoursTotal.TabIndex = 295
        Me.hoursTotal.Text = "0 h"
        Me.hoursTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'modifhoraire
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(658, 513)
        Me.Controls.Add(Me.grid)
        Me.Controls.Add(Me.hoursTotal)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.colorPassif)
        Me.Controls.Add(Me.print)
        Me.Controls.Add(Me.enlever)
        Me.Controls.Add(Me.encours)
        Me.Controls.Add(Me.horairelist)
        Me.Controls.Add(Me.colorActif)
        Me.Controls.Add(Me.saveas)
        Me.Controls.Add(Me.enregistrer)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(184, 89)
        Me.MinimumSize = New System.Drawing.Size(666, 250)
        Me.Name = "modifhoraire"
        Me.ShowInTaskbar = False
        Me.Text = "Horaire"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private ModifPermission, MovePermission, DirectionUp, winLoaded As Boolean
    Private curHoraireUser As Integer
    Private oldHoraire As Schedule
    Private lastUser As Integer = -1
    Private formModified As Boolean = False
    Private _AllowModification As Boolean = True
    Private lockedGlobalUser As Boolean = False
    Private lockingChangeNotAllowed As Boolean = False
    Private _Loaded As Boolean = False
    Private boldFont As Font
    Private normalFont As Font

#Region "Propriétés"
    Public Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal Value As Boolean)
            _AllowModification = Value
        End Set
    End Property

    Public ReadOnly Property noTRP() As Integer
        Get
            Return curHoraireUser
        End Get
    End Property
#End Region


    Public Sub loadHoraire(ByVal userToLoad As Integer)
        If lastUser <> -1 AndAlso oldHoraire IsNot Nothing Then
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications de l'horaire " & horairelist.Text & "?", "Enregistrement de l'horaire", MessageBoxButtons.YesNo) = DialogResult.Yes Then saving(Nothing)
            lockSecteur("Horaires-" & lastUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", False)
        End If
        lastUser = userToLoad
        curHoraireUser = userToLoad

        If curHoraireUser > 0 Then
            Dim userName As String = UsersManager.getInstance.getUser(userToLoad).getFullName()
            If _Loaded Then
                updateText(Me, "Horaire de " & userName)
            Else
                Me.Text = "Horaire de " & userName
                _Loaded = True
            End If
        Else
            If _Loaded Then
                updateText(Me, "Horaire de la clinique")
            Else
                Me.Text = "Horaire de la clinique"
                _Loaded = True
            End If
            MessageBox.Show("La modification des heures d'ouverture et de fermeture peut influencer l'affichage des rendez-vous déjà pris", "Modification de l'horaire de la clinique", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        oldHoraire = SchedulesManager.getInstance.getDefaultSchedule(curHoraireUser)

        encours.Enabled = False
        reLoadHoraireList()
    End Sub

    Public Sub reLoadHoraireList(Optional ByVal noChangeOnLocking As Boolean = False)
        lockingChangeNotAllowed = noChangeOnLocking

        horairelist.Items.Clear()
        Dim horaires As Generic.List(Of Schedule) = SchedulesManager.getInstance.getSchedules(curHoraireUser)
        If horaires.Count <> 0 Then
            horaires.Sort()
            horairelist.Items.AddRange(horaires.ToArray)
        Else
            oldHoraire = New Schedule(curHoraireUser, LIMIT_DATE)
            horairelist.Items.Add(oldHoraire)
        End If

        If oldHoraire IsNot Nothing AndAlso horairelist.Items.Contains(oldHoraire) Then
            horairelist.SelectedItem = oldHoraire
        Else
            horairelist.SelectedIndex = 0
            oldHoraire = horairelist.Items(0)
        End If
        reLoadGraph()

        lockingChangeNotAllowed = False
    End Sub

    Private testingBugHappened As Boolean = False

    Public Sub reLoadGraph(Optional ByVal fromUpdate As Boolean = False)
        If fromUpdate Then lockingChangeNotAllowed = True
        Dim selectedHoraire As Schedule = horairelist.SelectedItem

        If lockingChangeNotAllowed = False Then
            If allowModification = True Then lockSecteur("Horaires-" & curHoraireUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", False)

            oldHoraire = selectedHoraire

            If lockSecteur("Horaires-" & curHoraireUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", True, "Modification d'un horaire") = True Then
                allowModification = True
            Else
                allowModification = False
            End If

            If oldHoraire.isDefault = False AndAlso allowModification = True AndAlso date1Infdate2(CDate(oldHoraire.scheduleDate).AddDays(6), Date.Today) Then
                allowModification = False
                lockSecteur("Horaires-" & curHoraireUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", False)
            End If

        End If

        If selectedHoraire.isDefault = False Then
            If allowModification = True Then
                enlever.Enabled = True
            Else
                enlever.Enabled = False
            End If

            If lockedVerification("Horaires-" & curHoraireUser & "-null.lock") = True Then
                encours.Enabled = False
            Else
                encours.Enabled = True
            End If
        Else
            encours.Enabled = False
            enlever.Enabled = False
        End If

        enregistrer.Enabled = allowModification

        grid.loadSchedule(selectedHoraire, IIf(curHoraireUser = 0, Nothing, SchedulesManager.getInstance.getSchedule(0, selectedHoraire.scheduleDate)))

        formModified = False
        If fromUpdate Then lockingChangeNotAllowed = False
    End Sub

    Private Sub colorActif_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles colorActif.Click
        If grid.isDrawingScheduleOn Then Exit Sub

        changeToActiveColor()
        grid.isDrawingScheduleOn = True
    End Sub

    Private Sub changeToActiveColor()
        colorPassif.BorderStyle = System.Windows.Forms.BorderStyle.None
        colorActif.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        colorActif.Font = boldFont
        colorPassif.Font = normalFont
    End Sub

    Private Sub colorActif_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles colorActif.DoubleClick
        colorActif_Click(colorActif, New System.EventArgs())
    End Sub

    Private Sub colorPassif_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles colorPassif.Click
        If grid.isDrawingScheduleOn = False Then Exit Sub

        changeToPassiveColor()
        grid.isDrawingScheduleOn = False
    End Sub

    Private Sub changeToPassiveColor()
        colorPassif.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        colorActif.BorderStyle = System.Windows.Forms.BorderStyle.None
        colorActif.Font = normalFont
        colorPassif.Font = boldFont
    End Sub

    Private Sub colorPassif_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles colorPassif.DoubleClick
        colorPassif_Click(colorPassif, New System.EventArgs())
    End Sub

    Private Sub encours_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles encours.Click
        If MessageBox.Show("Voulez-vous modifier l'horaire par défaut ?", "Horaire par défaut", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
        If lockSecteur("Horaires-" & curHoraireUser & "-null.lock", True, "Modification d'un horaire") = False Then Exit Sub

        Dim previousHoraire As Schedule = oldHoraire
        Dim previousFM As Boolean = formModified
        oldHoraire = horairelist.Items(0)
        saving(oldHoraire)
        formModified = previousFM

        oldHoraire = previousHoraire
        lockSecteur("Horaires-" & curHoraireUser & "-null.lock", False)
    End Sub

    Private Sub enlever_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles enlever.Click
        If MessageBox.Show("Êtes-vous certain de vouloir supprimer cet horaire ?", "Suppression d'horaire", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        lockSecteur("Horaires-" & curHoraireUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", False)
        oldHoraire.delete()
        horairelist.Items.RemoveAt(horairelist.SelectedIndex)
        horairelist.SelectedIndex = 0

        reLoadGraph()
    End Sub

    Private Sub enregistrer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles enregistrer.Click
        saving(oldHoraire)
    End Sub

    Private Function saving(ByVal savingHoraire As Schedule) As String
        If savingHoraire Is Nothing Then savingHoraire = oldHoraire
        grid.saveScheduleTo(savingHoraire)
        savingHoraire.saveData()

        formModified = False

        Return "DONE"
    End Function

    Private Sub modifhoraire_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        ModifPermission = False

        reLoadGraph()
        winLoaded = True
    End Sub

    Private Sub modifhoraire_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim button As Short = eventArgs.Button \ &H100000
        Dim shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Double = (eventArgs.X)
        Dim y As Double = (eventArgs.Y)
        MovePermission = False
    End Sub

    Private Sub saveas_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles saveas.Click
        Dim lastSavingHoraire As Schedule = oldHoraire
        Dim MySelDate, transitiveDate As Date
        Dim addedHoraire As Boolean = False

        If horairelist.SelectedIndex > 0 Then MySelDate = oldHoraire.scheduleDate
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year, Date.Today.Year + 1, , , , , , True, Date.Today, , , , MySelDate, True, True)
        If dateReturn.Count = 0 Then Exit Sub

        lockSecteur("Horaires-" & curHoraireUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", False)
        Dim newHoraire As Schedule
        For Each myDate As Date In dateReturn
            transitiveDate = myDate.AddDays(transitiveDate.DayOfWeek * -1)

            If horairelist.FindStringExact(DateFormat.getTextDate(transitiveDate)) >= 0 Then
                newHoraire = horairelist.Items(horairelist.FindStringExact(DateFormat.getTextDate(transitiveDate)))
                If MessageBox.Show("Voulez-vous remplacer l'horaire spécifique du (" & DateFormat.getTextDate(transitiveDate) & ") par celui-ci", "Horaire existant en conflit", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
            Else
                addedHoraire = True
                newHoraire = New Schedule(curHoraireUser, transitiveDate)
            End If

            If lockSecteur("Horaires-" & curHoraireUser & "-" & newHoraire.toString.Replace("/", "-") & ".lock", True, , False) Then
                lastSavingHoraire = newHoraire
                saving(newHoraire)
                lockSecteur("Horaires-" & curHoraireUser & "-" & newHoraire.toString.Replace("/", "-") & ".lock", False)
            Else
                MessageBox.Show("Impossible d'enregistrer l'horaire sous la semaine " & DateFormat.getTextDate(transitiveDate) & ". Cet horaire est présentement en cours de modification par un autre utilisateur.", "Horaire en cours de modification")
            End If
        Next myDate

        If addedHoraire Then reLoadHoraireList(True)
        If lastSavingHoraire IsNot Nothing Then horairelist.SelectedIndex = horairelist.FindStringExact(lastSavingHoraire.toString)

        reLoadGraph()
        formModified = False
    End Sub

    Private Sub modifhoraire_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If allowModification = True Then
            lockSecteur("Horaires-" & curHoraireUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", False)

            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then If saving(Nothing) = "" Then e.Cancel = True
        End If
    End Sub

    Private Sub modifhoraire_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If allowModification = True Then
            lockSecteur("Horaires-" & curHoraireUser & "-" & IIf(oldHoraire.isDefault, "null", oldHoraire.toString.Replace("/", "-")) & ".lock", False)

            If formModified = True Then saving(Nothing)
        End If
    End Sub

    Private Sub grid_isDrawingScheduleOnOnChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid.isDrawingScheduleOnOnChanged
        If grid.isDrawingScheduleOn = False Then
            changeToActiveColor()
        Else
            changeToPassiveColor()
        End If
    End Sub

    Private Sub grid_TimeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid.timeChanged
        formModified = True
    End Sub

    Private Sub grid_TotalChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid.totalChanged
        hoursTotal.Text = Math.Round(grid.getNbMinutes(-1) / 60, 2) & " h"
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf modifhoraire_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.fromExternal AndAlso dataReceived.function.StartsWith("Horaire-") AndAlso dataReceived.params(1) = curHoraireUser Then
            If dataReceived.function = "Horaire-Modif" AndAlso oldHoraire.scheduleDate = dataReceived.params(2) Then
                reLoadGraph(True)
            End If
            If dataReceived.function = "Horaire-Add" OrElse dataReceived.function = "Horaire-Del" Then reLoadHoraireList(True)
        End If
    End Sub

    Private Sub print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print.Click
        Dim selectedHoraire As Schedule = horairelist.SelectedItem
        selectedHoraire.print()
    End Sub

    Private Sub horairelist_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles horairelist.SelectionChangeCommitted
        If lockingChangeNotAllowed = False Then If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then saving(oldHoraire)

        reLoadGraph()
    End Sub
End Class
