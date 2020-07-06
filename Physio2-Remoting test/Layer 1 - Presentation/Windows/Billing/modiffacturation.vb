Option Strict Off
Option Explicit On
Friend Class modiffacturation
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
        curFactureBox = New FacturationBox(False, False, False, FacturationBox.DedicatedType.All)
        curFactureBox.locked = True
        '
        'CurFactureBox
        '
        Me.curFactureBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.curFactureBox.BackColor = System.Drawing.SystemColors.Control
        Me.curFactureBox.Location = New System.Drawing.Point(31, 268)
        Me.curFactureBox.montantFacture = 0.0!
        Me.curFactureBox.montantRestant = 0.0!
        Me.curFactureBox.Name = "CurFactureBox"
        Me.curFactureBox.negativePaiement = False
        Me.curFactureBox.noFacture = 0
        Me.curFactureBox.setNom = ""
        Me.curFactureBox.Size = New System.Drawing.Size(536, 130)
        Me.curFactureBox.TabIndex = 0
        Me.curFactureBox.valueA = Nothing
        Me.curFactureBox.valueB = Nothing
        Me.Controls.Add(Me.curFactureBox)

        With DrawingManager.getInstance
            imgModifSave.Images.Add(.getImage("modifier16.gif"))
            imgModifSave.Images.Add(.getImage("stopmodif16.gif"))
            startStopBillModif.Image = imgModifSave.Images(0)
            filter.Image = .getImage("selection16.gif")
            createBill.Image = .getImage("newBill16.jpg")
            Me.Icon = DrawingManager.imageToIcon(.getImage("viewBills16.jpg"))
        End With
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Private WithEvents curFactureBox As Clinica.FacturationBox
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents filter As System.Windows.Forms.Button
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents choixDe As System.Windows.Forms.Button
    Private WithEvents dateAll As System.Windows.Forms.CheckBox
    Private WithEvents dateA As System.Windows.Forms.Label
    Private WithEvents dateDe As System.Windows.Forms.Label
    Private WithEvents choixA As System.Windows.Forms.Button
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents noFactureA As ManagedText
    Private WithEvents noFactureDe As ManagedText
    Private WithEvents createBill As System.Windows.Forms.Button
    Private WithEvents startStopBillModif As System.Windows.Forms.Button
    Private WithEvents facturesView As DataGridPlus
    Friend WithEvents DateF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoFacture As System.Windows.Forms.DataGridViewTextBoxColumn
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim dataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim dataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim dataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.filter = New System.Windows.Forms.Button
        Me.createBill = New System.Windows.Forms.Button
        Me.startStopBillModif = New System.Windows.Forms.Button
        Me.label15 = New System.Windows.Forms.Label
        Me.choixDe = New System.Windows.Forms.Button
        Me.dateAll = New System.Windows.Forms.CheckBox
        Me.dateA = New System.Windows.Forms.Label
        Me.dateDe = New System.Windows.Forms.Label
        Me.choixA = New System.Windows.Forms.Button
        Me.label7 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.facturesView = New DataGridPlus
        Me.noFactureA = New ManagedText
        Me.noFactureDe = New ManagedText
        Me.DateF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MF = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PPO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PU = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NoFacture = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.facturesView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.toolTip1.ShowAlways = True
        '
        'filter
        '
        Me.filter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.filter.BackColor = System.Drawing.SystemColors.Control
        Me.filter.Cursor = System.Windows.Forms.Cursors.Default
        Me.filter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.filter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.filter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.filter.Location = New System.Drawing.Point(486, 9)
        Me.filter.Name = "filter"
        Me.filter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.filter.Size = New System.Drawing.Size(29, 32)
        Me.filter.TabIndex = 280
        Me.filter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.toolTip1.SetToolTip(Me.filter, "Filtrer les factures avec les paramètres ci-contre")
        Me.filter.UseVisualStyleBackColor = False
        '
        'createBill
        '
        Me.createBill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.createBill.BackColor = System.Drawing.SystemColors.Control
        Me.createBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.createBill.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.createBill.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.createBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.createBill.Location = New System.Drawing.Point(556, 9)
        Me.createBill.Name = "createBill"
        Me.createBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.createBill.Size = New System.Drawing.Size(29, 32)
        Me.createBill.TabIndex = 280
        Me.createBill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.toolTip1.SetToolTip(Me.createBill, "Créer une nouvelle facture pour cet utilisateur")
        Me.createBill.UseVisualStyleBackColor = False
        '
        'StartStopBillModif
        '
        Me.startStopBillModif.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.startStopBillModif.BackColor = System.Drawing.SystemColors.Control
        Me.startStopBillModif.Cursor = System.Windows.Forms.Cursors.Default
        Me.startStopBillModif.Enabled = False
        Me.startStopBillModif.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.startStopBillModif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startStopBillModif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.startStopBillModif.Location = New System.Drawing.Point(521, 9)
        Me.startStopBillModif.Name = "StartStopBillModif"
        Me.startStopBillModif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.startStopBillModif.Size = New System.Drawing.Size(29, 32)
        Me.startStopBillModif.TabIndex = 280
        Me.startStopBillModif.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.toolTip1.SetToolTip(Me.startStopBillModif, "Commencer la modification les factures")
        Me.startStopBillModif.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.label15.AutoSize = True
        Me.label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.Location = New System.Drawing.Point(12, 9)
        Me.label15.Name = "Label15"
        Me.label15.Size = New System.Drawing.Size(125, 13)
        Me.label15.TabIndex = 279
        Me.label15.Text = "Date de facturation :"
        '
        'ChoixDe
        '
        Me.choixDe.Location = New System.Drawing.Point(116, 25)
        Me.choixDe.Name = "ChoixDe"
        Me.choixDe.Size = New System.Drawing.Size(32, 18)
        Me.choixDe.TabIndex = 274
        Me.choixDe.Text = "De"
        '
        'DateAll
        '
        Me.dateAll.BackColor = System.Drawing.Color.Transparent
        Me.dateAll.Location = New System.Drawing.Point(12, 25)
        Me.dateAll.Name = "DateAll"
        Me.dateAll.Size = New System.Drawing.Size(112, 16)
        Me.dateAll.TabIndex = 275
        Me.dateAll.Text = "Toutes les dates"
        Me.dateAll.UseVisualStyleBackColor = False
        '
        'DateA
        '
        Me.dateA.AutoSize = True
        Me.dateA.BackColor = System.Drawing.Color.Transparent
        Me.dateA.Location = New System.Drawing.Point(268, 25)
        Me.dateA.Name = "DateA"
        Me.dateA.Size = New System.Drawing.Size(0, 14)
        Me.dateA.TabIndex = 278
        '
        'DateDe
        '
        Me.dateDe.AutoSize = True
        Me.dateDe.BackColor = System.Drawing.Color.Transparent
        Me.dateDe.Location = New System.Drawing.Point(156, 25)
        Me.dateDe.Name = "DateDe"
        Me.dateDe.Size = New System.Drawing.Size(0, 14)
        Me.dateDe.TabIndex = 277
        '
        'ChoixA
        '
        Me.choixA.Location = New System.Drawing.Point(228, 25)
        Me.choixA.Name = "ChoixA"
        Me.choixA.Size = New System.Drawing.Size(32, 18)
        Me.choixA.TabIndex = 276
        Me.choixA.Text = "A"
        '
        'Label7
        '
        Me.label7.AutoSize = True
        Me.label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.Location = New System.Drawing.Point(384, 28)
        Me.label7.Name = "Label7"
        Me.label7.Size = New System.Drawing.Size(14, 13)
        Me.label7.TabIndex = 263
        Me.label7.Text = "à"
        '
        'Label5
        '
        Me.label5.AutoSize = True
        Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(346, 9)
        Me.label5.Name = "Label5"
        Me.label5.Size = New System.Drawing.Size(134, 13)
        Me.label5.TabIndex = 261
        Me.label5.Text = "Numéro de la facture :"
        '
        'FacturesView
        '
        Me.facturesView.AllowUserToAddRows = False
        Me.facturesView.AllowUserToDeleteRows = False
        Me.facturesView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.facturesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.facturesView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.facturesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.facturesView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DateF, Me.TF, Me.MF, Me.MP, Me.PC, Me.PPO, Me.PU, Me.EL, Me.NoFacture})
        Me.facturesView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.facturesView.Location = New System.Drawing.Point(12, 51)
        Me.facturesView.Name = "FacturesView"
        Me.facturesView.ReadOnly = True
        Me.facturesView.RowHeadersVisible = False
        Me.facturesView.RowHeadersWidth = 20
        Me.facturesView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.facturesView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.facturesView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.facturesView.Size = New System.Drawing.Size(573, 209)
        Me.facturesView.TabIndex = 281
        '
        'NoFactureA
        '
        Me.noFactureA.acceptAlpha = False
        Me.noFactureA.acceptedChars = ",§."
        Me.noFactureA.acceptNumeric = True
        Me.noFactureA.allCapital = False
        Me.noFactureA.allLower = False
        Me.noFactureA.cb_AcceptNegative = False
        Me.noFactureA.currencyBox = True
        Me.noFactureA.firstLetterCapital = False
        Me.noFactureA.firstLettersCapital = False
        Me.noFactureA.Location = New System.Drawing.Point(402, 25)
        Me.noFactureA.matchExp = ""
        Me.noFactureA.Name = "NoFactureA"
        Me.noFactureA.nbDecimals = CType(0, Short)
        Me.noFactureA.onlyAlphabet = False
        Me.noFactureA.refuseAccents = False
        Me.noFactureA.refusedChars = ""
        Me.noFactureA.Size = New System.Drawing.Size(32, 20)
        Me.noFactureA.TabIndex = 260
        Me.noFactureA.Text = "0"
        Me.noFactureA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.noFactureA.trimText = False
        '
        'NoFactureDe
        '
        Me.noFactureDe.acceptAlpha = False
        Me.noFactureDe.acceptedChars = ",§."
        Me.noFactureDe.acceptNumeric = True
        Me.noFactureDe.allCapital = False
        Me.noFactureDe.allLower = False
        Me.noFactureDe.cb_AcceptNegative = False
        Me.noFactureDe.currencyBox = True
        Me.noFactureDe.firstLetterCapital = False
        Me.noFactureDe.firstLettersCapital = False
        Me.noFactureDe.Location = New System.Drawing.Point(346, 25)
        Me.noFactureDe.matchExp = ""
        Me.noFactureDe.Name = "NoFactureDe"
        Me.noFactureDe.nbDecimals = CType(0, Short)
        Me.noFactureDe.onlyAlphabet = False
        Me.noFactureDe.refuseAccents = False
        Me.noFactureDe.refusedChars = ""
        Me.noFactureDe.Size = New System.Drawing.Size(32, 20)
        Me.noFactureDe.TabIndex = 259
        Me.noFactureDe.Text = "0"
        Me.noFactureDe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.noFactureDe.trimText = False
        '
        'DateF
        '
        Me.DateF.DataPropertyName = "DF"
        dataGridViewCellStyle1.Format = "d"
        dataGridViewCellStyle1.NullValue = Nothing
        Me.DateF.DefaultCellStyle = dataGridViewCellStyle1
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
        dataGridViewCellStyle2.Format = "C2"
        dataGridViewCellStyle2.NullValue = "0"
        Me.MF.DefaultCellStyle = dataGridViewCellStyle2
        Me.MF.HeaderText = "Montant facturé"
        Me.MF.Name = "MF"
        Me.MF.ReadOnly = True
        '
        'MP
        '
        Me.MP.DataPropertyName = "MP"
        dataGridViewCellStyle3.Format = "C2"
        dataGridViewCellStyle3.NullValue = Nothing
        Me.MP.DefaultCellStyle = dataGridViewCellStyle3
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
        Me.PPO.Visible = False
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
        '
        'NoFacture
        '
        Me.NoFacture.DataPropertyName = "NoFacture"
        Me.NoFacture.HeaderText = "# Facture"
        Me.NoFacture.Name = "NoFacture"
        Me.NoFacture.ReadOnly = True
        Me.NoFacture.Visible = False
        '
        'modiffacturation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(596, 406)
        Me.Controls.Add(Me.facturesView)
        Me.Controls.Add(Me.createBill)
        Me.Controls.Add(Me.startStopBillModif)
        Me.Controls.Add(Me.filter)
        Me.Controls.Add(Me.label15)
        Me.Controls.Add(Me.choixDe)
        Me.Controls.Add(Me.dateAll)
        Me.Controls.Add(Me.dateA)
        Me.Controls.Add(Me.dateDe)
        Me.Controls.Add(Me.choixA)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.noFactureA)
        Me.Controls.Add(Me.noFactureDe)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(604, 768)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(604, 263)
        Me.Name = "modiffacturation"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.Text = "Facturation - Modif/View"
        CType(Me.facturesView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private lastLoadedBill As Integer = 0
    Private imgModifSave As New ImageList
    Private _NoUser, _NoClinique As Integer
    Private _Count As Integer = 0
    Private currentLocks As New ArrayList
    Private _DedicatedType As FacturationBox.DedicatedType

#Region "Propriétés"
    Public Property noClinique() As Integer
        Get
            Return _NoClinique
        End Get
        Set(ByVal value As Integer)
            _NoClinique = value
        End Set
    End Property

    Public Property noUser() As Integer
        Get
            Return _NoUser
        End Get
        Set(ByVal value As Integer)
            _NoUser = value
        End Set
    End Property

    Public Property dedicatedType() As FacturationBox.DedicatedType
        Get
            Return _DedicatedType
        End Get
        Set(ByVal value As FacturationBox.DedicatedType)
            _DedicatedType = value
            curFactureBox.currentType = value
        End Set
    End Property
#End Region

    Private Property count() As Integer
        Get
            Return _Count
        End Get
        Set(ByVal value As Integer)
            _Count = value
        End Set
    End Property

#Region "modiffacturation events"

    Private Sub modiffacturation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If currentLocks.Count > 0 Then
            For i As Integer = 0 To currentLocks.Count - 1
                lockSecteur(currentLocks(i), False)
            Next i
        End If
    End Sub

    Private Sub modiffacturation_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        If dedicatedType = FacturationBox.DedicatedType.Clinique Then
            Me.Text = "Facturation de la clinique"
        ElseIf dedicatedType = FacturationBox.DedicatedType.User Then
            Me.Text = "Facturation d'un travailleur autonome"
        End If
    End Sub
#End Region

    Private Sub filter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles filter.Click
        filter.Enabled = False
        loadFactures()
        filter.Enabled = True
    End Sub

    Private Sub choixDeA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles choixDe.Click, choixA.Click
        Dim CurDate, scd() As String
        Dim myDate As Date = Nothing
        If sender.name.tolower = "choixde" Then
            CurDate = dateDe.Text
        Else
            CurDate = dateA.Text
        End If
        If CurDate <> "" Then
            scd = CurDate.Split(New Char() {"/"})
            myDate = CDate(scd(0) & "/" & scd(1) & "/" & scd(2))
        Else
            myDate = Date.Today
        End If
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year - 10, Date.Today.Year + 1, , , , , , True, , , , , myDate)
        If dateReturn.Count = 0 Then Exit Sub

        Dim dateReturnString As String = DateFormat.getTextDate(dateReturn(0))
        If sender.name.tolower = "choixde" Then
            dateDe.Text = dateReturnString
            If dateA.Text = "" Then dateA.Text = dateReturnString
        Else
            dateA.Text = dateReturnString
        End If
    End Sub

    Private Sub cleanCurrentLocks()
        Dim i As Integer
        For i = 0 To currentLocks.Count - 1
            lockSecteur(currentLocks(i), False, "Facturation")
        Next i

        currentLocks.Clear()
    End Sub

    Public Sub loadFactures()
        Dim reactivateModif As Boolean = False
        If toolTip1.GetToolTip(startStopBillModif).StartsWith("Commencer") = False Then
            startStopBillModif_Click(Me, Nothing)
            reactivateModif = True
        End If

        Dim whereStr As String = ""
        If noFactureA.Text > 0 Then whereStr &= " AND StatFactures.NoFacture>=" & noFactureDe.Text & " AND StatFactures.NoFacture<=" & noFactureA.Text

        Select Case dedicatedType
            Case FacturationBox.DedicatedType.Clinique
                whereStr &= " AND StatFactures.ParNoClinique=" & noClinique
            Case FacturationBox.DedicatedType.User
                whereStr &= " AND ((StatFactures.ParNoUser) = " & noUser & " Or StatFactures.NoUserFacture = " & noUser & ")"
        End Select

        'Date facture
        If dateDe.Text <> "" And dateA.Text <> "" And Me.dateAll.Checked = False Then
            whereStr &= " AND StatFactures.DateFacture >= '" & dateDe.Text & "' AND StatFactures.DateFacture <= '" & dateA.Text & "'"
        End If

        Dim descending As String = "DESC"
        Dim sortingOrder As DBLinker.SortOrderType = DBLinker.SortOrderType.Descending
        If PreferencesManager.getGeneralPreferences()("TriFactures").StartsWith("A") Then descending = "" : sortingOrder = DBLinker.SortOrderType.Ascending

        If whereStr.StartsWith(" AND") Then whereStr = " WHERE " & whereStr.Substring(4)
        Dim factures As DataSet = DBLinker.getInstance.readDBForGrid("Statfactures LEFT OUTER JOIN                       InfoClients AS IC1 ON IC1.NoClient = Statfactures.NoClient LEFT OUTER JOIN                       KeyPeople AS KP1 ON KP1.NoKP = Statfactures.NoKp LEFT OUTER JOIN                       Utilisateurs AS U1 ON U1.NoUser = Statfactures.NoUser LEFT OUTER JOIN                       InfoClients AS IC2 ON IC2.NoClient = Statfactures.ParNoClient LEFT OUTER JOIN                       Utilisateurs AS U2 ON U2.NoUser = Statfactures.ParNoUser LEFT OUTER JOIN                       KeyPeople AS KP2 ON KP2.NoKP = Statfactures.ParNoKp", "DF, TF, MF , CASE WHEN MP IS NULL THEN 0 ELSE MP END AS MP,PC , PPO, PU, EL , NoFacture FROM (SELECT     MIN(Statfactures.DateFacture) AS DF, MIN(Statfactures.TypeFacture) AS TF, CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN SUM(Statfactures.MontantFacture) ELSE dbo.GetLinkedBillMF(Statfactures.NoFacture,0) END AS [MF], CASE WHEN MIN(Statfactures.NoFactureRef) = '' THEN (SELECT SUM(MontantPaiement) FROM StatPaiements WHERE StatPaiements.NoFacture = Statfactures.NoFacture) ELSE dbo.GetLinkedBillMP(Statfactures.NoFacture,0) END AS MP, CASE WHEN MIN(Statfactures.ParNoClient)=0 THEN 'Aucun' ELSE MIN(IC2.Nom) + ',' + MIN(IC2.Prenom) END AS PC, CASE WHEN MIN(Statfactures.ParNoKP)=0 THEN 'Aucun' ELSE MIN(KP2.Nom) END AS PPO, CASE WHEN MIN(Statfactures.ParNoUser)=0 THEN 'Aucun' ELSE MIN(U2.Nom) + ',' + MIN(U2.Prenom) END AS PU, CASE WHEN MIN(Statfactures.NoClient)=0 THEN CASE WHEN MIN(Statfactures.NoKP)=0 THEN CASE WHEN MIN(Statfactures.NoUserFacture)=0 THEN 'Aucun' ELSE MIN(U1.Nom) + ',' + MIN(U1.Prenom) END ELSE MIN(KP1.Nom) END ELSE MIN(IC1.Nom) + ',' + MIN(IC1.Prenom) END AS EL, Statfactures.NoFacture,MIN(Statfactures.NoFactureRef) AS FRef, (SELECT TOP 1 SF2.NoAction FROM Statfactures AS SF2 WHERE SF2.NoStat = MAX(Statfactures.NoStat)) AS Action", whereStr & " GROUP BY Statfactures.NoFacture) AS Test WHERE Action<>20 ORDER BY DF DESC")
        facturesView.DataSource = factures.Tables(0)

        If factures.Tables(0).Rows.Count = 0 Then
            startStopBillModif.Enabled = False
        Else
            startStopBillModif.Enabled = True
        End If

        If reactivateModif = True And factures.Tables(0).Rows.Count > 0 Then startStopBillModif_Click(Me, Nothing)
    End Sub

    Private Sub noFactureDe_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles noFactureDe.TextChanged
        If noFactureA.Text = "" Or noFactureDe.Text = "" Then Exit Sub
        If CType(noFactureA.Text, Integer) < CType(noFactureDe.Text, Integer) Then noFactureA.Text = noFactureDe.Text
    End Sub

    Private Sub noFactureA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles noFactureA.TextChanged
        If noFactureA.Text = "" Or noFactureDe.Text = "" Then Exit Sub
        If CType(noFactureA.Text, Integer) < CType(noFactureDe.Text, Integer) Then noFactureDe.Text = noFactureA.Text
    End Sub

    Private Sub startStopBillModif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startStopBillModif.Click
        'Droit & Accès
        If currentDroitAcces(83) = False And noClinique > 0 Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les factures de la clinique." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If
        If currentDroitAcces(49) = False And noUser > 0 And noUser <> ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les factures de tous les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If
        If currentDroitAcces(50) = False And noUser > 0 And noUser = ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer vos factures." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim currentFacturation As String = "UserFacturation"
        Dim currentNo As Integer = noUser
        Dim otherFacturation As String = "CliniqueFacturation"
        Dim otherNo As Integer = noClinique
        If dedicatedType = FacturationBox.DedicatedType.Clinique Then
            currentFacturation = otherFacturation
            currentNo = noClinique
            otherFacturation = "UserFacturation"
            otherNo = noUser
        End If

        Dim i As Integer
        If toolTip1.GetToolTip(startStopBillModif).StartsWith("C") Then
            If lockSecteur(currentFacturation & "-" & currentNo, True, Me.Text) = True Then
                currentLocks.Add(currentFacturation & "-" & currentNo)
                startStopBillModif.Image = imgModifSave.Images(1)
                toolTip1.SetToolTip(startStopBillModif, "Arrêter la modification des factures et paiements")

                curFactureBox.locked = False
            End If
        Else
            startStopBillModif.Image = imgModifSave.Images(0)
            toolTip1.SetToolTip(startStopBillModif, "Commencer la modification les factures et paiements")

            curFactureBox.locked = True
            For i = 0 To currentLocks.Count - 1
                lockSecteur(currentLocks(i), False)
            Next i
            currentLocks.Clear()
            'REM If FacturesPanel.Controls.Count > 0 Then UpdatePaiements(NoClient)
        End If
    End Sub

    Private Sub createBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles createBill.Click
        'Droit & Accès
        If (currentDroitAcces(85) = False And noClinique > 0) Or (currentDroitAcces(78) = False And noUser > 0 And noUser <> ConnectionsManager.currentUser) Or (currentDroitAcces(79) = False And noUser > 0 And noUser = ConnectionsManager.currentUser) Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de créer une nouvelle facture." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myAddBill As addBill = openUniqueWindow(New addBill())
        myAddBill.Show()
        Select Case dedicatedType
            Case FacturationBox.DedicatedType.Clinique
                myAddBill.mode = addBill.Modes.All
            Case FacturationBox.DedicatedType.User
                myAddBill.mode = addBill.Modes.User
                myAddBill.setUser(noUser, UsersManager.getInstance.getUser(noUser).getFullName())
        End Select
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

        If toolTip1.GetToolTip(startStopBillModif).StartsWith("Commencer") = False Then curFactureBox.locked = False
        curFactureBox.loading(lastLoadedBill)
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
