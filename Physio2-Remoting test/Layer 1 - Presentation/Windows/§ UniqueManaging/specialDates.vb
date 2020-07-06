Friend Class specialDates
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        Me.MdiParent = myMainWin
        lockItems(1)
        configList(Me.ListSpecialDates)

        vider_Click(Me, EventArgs.Empty)

        With DrawingManager.getInstance
            Me.Adding.Image = .getImage("ajouter16.gif")
            Me.delete.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.Save.Image = .getImage("save.jpg")
            Me.vider.Image = .getImage("eraser.jpg")
            Me.maximise.Image = .getImage("FDehors.jpg")
        End With
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ListSpecialDates As CI.Controls.List
    Friend WithEvents Mois1 As System.Windows.Forms.ComboBox
    Friend WithEvents Jour1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Nom As ManagedText
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Mois2 As System.Windows.Forms.ComboBox
    Friend WithEvents Semaine2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Journee2 As System.Windows.Forms.ComboBox
    Friend WithEvents Mois3 As System.Windows.Forms.ComboBox
    Friend WithEvents Jour3 As System.Windows.Forms.ComboBox
    Friend WithEvents Journee3 As System.Windows.Forms.ComboBox
    Friend WithEvents AfterBefore3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Select1 As System.Windows.Forms.RadioButton
    Friend WithEvents Select2 As System.Windows.Forms.RadioButton
    Friend WithEvents Select3 As System.Windows.Forms.RadioButton
    Friend WithEvents Select5 As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CodeVBNet As System.Windows.Forms.TextBox
    Friend WithEvents Adding As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents delete As System.Windows.Forms.Button
    Friend WithEvents vider As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents MaxYear As ManagedText
    Friend WithEvents maximise As System.Windows.Forms.Button
    Friend WithEvents Select4 As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents BaseDay4 As System.Windows.Forms.ComboBox
    Friend WithEvents Relative4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents NbJour4 As ManagedText

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Adding = New System.Windows.Forms.Button
        Me.Save = New System.Windows.Forms.Button
        Me.delete = New System.Windows.Forms.Button
        Me.vider = New System.Windows.Forms.Button
        Me.maximise = New System.Windows.Forms.Button
        Me.MaxYear = New ManagedText
        Me.Mois1 = New System.Windows.Forms.ComboBox
        Me.Jour1 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Mois2 = New System.Windows.Forms.ComboBox
        Me.Semaine2 = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Journee2 = New System.Windows.Forms.ComboBox
        Me.Mois3 = New System.Windows.Forms.ComboBox
        Me.Jour3 = New System.Windows.Forms.ComboBox
        Me.Journee3 = New System.Windows.Forms.ComboBox
        Me.AfterBefore3 = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Select1 = New System.Windows.Forms.RadioButton
        Me.Select2 = New System.Windows.Forms.RadioButton
        Me.Select3 = New System.Windows.Forms.RadioButton
        Me.Select5 = New System.Windows.Forms.RadioButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.CodeVBNet = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Select4 = New System.Windows.Forms.RadioButton
        Me.Label12 = New System.Windows.Forms.Label
        Me.BaseDay4 = New System.Windows.Forms.ComboBox
        Me.Relative4 = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.NbJour4 = New ManagedText
        Me.Nom = New ManagedText
        Me.ListSpecialDates = New CI.Controls.List
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'Adding
        '
        Me.Adding.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Adding.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Adding.Location = New System.Drawing.Point(100, 526)
        Me.Adding.Name = "Adding"
        Me.Adding.Size = New System.Drawing.Size(24, 24)
        Me.Adding.TabIndex = 6
        Me.Adding.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Adding, "Ajouter une journée spéciale avec les paramètres ci-haut")
        '
        'Save
        '
        Me.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Save.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Save.Location = New System.Drawing.Point(139, 526)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(24, 24)
        Me.Save.TabIndex = 6
        Me.Save.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Save, "Enregistrer la journée spéciale sélectionnée")
        '
        'delete
        '
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.delete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.delete.Location = New System.Drawing.Point(178, 526)
        Me.delete.Name = "delete"
        Me.delete.Size = New System.Drawing.Size(24, 24)
        Me.delete.TabIndex = 6
        Me.delete.TabStop = False
        Me.ToolTip1.SetToolTip(Me.delete, "Supprimer la journée spéciale sélectionnée")
        '
        'vider
        '
        Me.vider.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.vider.Location = New System.Drawing.Point(256, 526)
        Me.vider.Name = "vider"
        Me.vider.Size = New System.Drawing.Size(24, 24)
        Me.vider.TabIndex = 6
        Me.vider.TabStop = False
        Me.ToolTip1.SetToolTip(Me.vider, "Vider les champs")
        '
        'maximise
        '
        Me.maximise.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.maximise.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.maximise.Location = New System.Drawing.Point(216, 526)
        Me.maximise.Name = "maximise"
        Me.maximise.Size = New System.Drawing.Size(24, 24)
        Me.maximise.TabIndex = 6
        Me.maximise.TabStop = False
        Me.ToolTip1.SetToolTip(Me.maximise, "Maximiser le code VB.NET")
        '
        'MaxYear
        '
        Me.MaxYear.acceptAlpha = False
        Me.MaxYear.acceptedChars = ",§."
        Me.MaxYear.acceptNumeric = True
        Me.MaxYear.allCapital = False
        Me.MaxYear.allLower = False
        Me.MaxYear.cb_AcceptNegative = False
        Me.MaxYear.currencyBox = True
        Me.MaxYear.firstLetterCapital = True
        Me.MaxYear.firstLettersCapital = False
        Me.MaxYear.Location = New System.Drawing.Point(266, 124)
        Me.MaxYear.manageText = True
        Me.MaxYear.matchExp = ""
        Me.MaxYear.MaxLength = 4
        Me.MaxYear.Name = "MaxYear"
        Me.MaxYear.nbDecimals = CType(0, Short)
        Me.MaxYear.onlyAlphabet = False
        Me.MaxYear.refuseAccents = False
        Me.MaxYear.refusedChars = ""
        Me.MaxYear.showInternalContextMenu = True
        Me.MaxYear.Size = New System.Drawing.Size(81, 20)
        Me.MaxYear.TabIndex = 3
        Me.MaxYear.Text = "0"
        Me.MaxYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.MaxYear, "Entrer zéro pour aucune limite")
        Me.MaxYear.trimText = False
        '
        'Mois1
        '
        Me.Mois1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Mois1.FormattingEnabled = True
        Me.Mois1.Items.AddRange(New Object() {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"})
        Me.Mois1.Location = New System.Drawing.Point(32, 173)
        Me.Mois1.Name = "Mois1"
        Me.Mois1.Size = New System.Drawing.Size(78, 21)
        Me.Mois1.TabIndex = 1
        '
        'Jour1
        '
        Me.Jour1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Jour1.FormattingEnabled = True
        Me.Jour1.Location = New System.Drawing.Point(116, 173)
        Me.Jour1.Name = "Jour1"
        Me.Jour1.Size = New System.Drawing.Size(60, 21)
        Me.Jour1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 157)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Mois :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nom de la journée :"
        '
        'Mois2
        '
        Me.Mois2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Mois2.FormattingEnabled = True
        Me.Mois2.Items.AddRange(New Object() {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"})
        Me.Mois2.Location = New System.Drawing.Point(32, 213)
        Me.Mois2.Name = "Mois2"
        Me.Mois2.Size = New System.Drawing.Size(78, 21)
        Me.Mois2.TabIndex = 1
        '
        'Semaine2
        '
        Me.Semaine2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Semaine2.FormattingEnabled = True
        Me.Semaine2.Items.AddRange(New Object() {"1er", "2e", "3e", "4e", "5e", "Dernier"})
        Me.Semaine2.Location = New System.Drawing.Point(116, 213)
        Me.Semaine2.Name = "Semaine2"
        Me.Semaine2.Size = New System.Drawing.Size(60, 21)
        Me.Semaine2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 197)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Mois :"
        '
        'Journee2
        '
        Me.Journee2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Journee2.FormattingEnabled = True
        Me.Journee2.Items.AddRange(New Object() {"Dimanche", "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi"})
        Me.Journee2.Location = New System.Drawing.Point(182, 213)
        Me.Journee2.Name = "Journee2"
        Me.Journee2.Size = New System.Drawing.Size(78, 21)
        Me.Journee2.TabIndex = 1
        '
        'Mois3
        '
        Me.Mois3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Mois3.FormattingEnabled = True
        Me.Mois3.Items.AddRange(New Object() {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"})
        Me.Mois3.Location = New System.Drawing.Point(203, 252)
        Me.Mois3.Name = "Mois3"
        Me.Mois3.Size = New System.Drawing.Size(78, 21)
        Me.Mois3.TabIndex = 1
        '
        'Jour3
        '
        Me.Jour3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Jour3.FormattingEnabled = True
        Me.Jour3.Location = New System.Drawing.Point(287, 252)
        Me.Jour3.Name = "Jour3"
        Me.Jour3.Size = New System.Drawing.Size(60, 21)
        Me.Jour3.TabIndex = 1
        '
        'Journee3
        '
        Me.Journee3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Journee3.FormattingEnabled = True
        Me.Journee3.Items.AddRange(New Object() {"Dimanche", "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi"})
        Me.Journee3.Location = New System.Drawing.Point(32, 252)
        Me.Journee3.Name = "Journee3"
        Me.Journee3.Size = New System.Drawing.Size(78, 21)
        Me.Journee3.TabIndex = 1
        '
        'AfterBefore3
        '
        Me.AfterBefore3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AfterBefore3.FormattingEnabled = True
        Me.AfterBefore3.Items.AddRange(New Object() {"Avant", "Après"})
        Me.AfterBefore3.Location = New System.Drawing.Point(116, 252)
        Me.AfterBefore3.Name = "AfterBefore3"
        Me.AfterBefore3.Size = New System.Drawing.Size(60, 21)
        Me.AfterBefore3.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 237)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Journée :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(113, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Jour :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(179, 197)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Journée :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(200, 237)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Mois :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(284, 237)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(33, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Jour :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(181, 256)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(16, 13)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "->"
        '
        'Select1
        '
        Me.Select1.AutoSize = True
        Me.Select1.Checked = True
        Me.Select1.Location = New System.Drawing.Point(12, 176)
        Me.Select1.Name = "Select1"
        Me.Select1.Size = New System.Drawing.Size(14, 13)
        Me.Select1.TabIndex = 4
        Me.Select1.TabStop = True
        Me.Select1.UseVisualStyleBackColor = True
        '
        'Select2
        '
        Me.Select2.AutoSize = True
        Me.Select2.Location = New System.Drawing.Point(12, 216)
        Me.Select2.Name = "Select2"
        Me.Select2.Size = New System.Drawing.Size(14, 13)
        Me.Select2.TabIndex = 4
        Me.Select2.UseVisualStyleBackColor = True
        '
        'Select3
        '
        Me.Select3.AutoSize = True
        Me.Select3.Location = New System.Drawing.Point(12, 255)
        Me.Select3.Name = "Select3"
        Me.Select3.Size = New System.Drawing.Size(14, 13)
        Me.Select3.TabIndex = 4
        Me.Select3.UseVisualStyleBackColor = True
        '
        'Select5
        '
        Me.Select5.AutoSize = True
        Me.Select5.Location = New System.Drawing.Point(12, 322)
        Me.Select5.Name = "Select5"
        Me.Select5.Size = New System.Drawing.Size(14, 13)
        Me.Select5.TabIndex = 4
        Me.Select5.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(29, 322)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Code VB.NET :"
        '
        'CodeVBNet
        '
        Me.CodeVBNet.Location = New System.Drawing.Point(32, 338)
        Me.CodeVBNet.Multiline = True
        Me.CodeVBNet.Name = "CodeVBNet"
        Me.CodeVBNet.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.CodeVBNet.Size = New System.Drawing.Size(315, 178)
        Me.CodeVBNet.TabIndex = 5
        Me.CodeVBNet.WordWrap = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(263, 108)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Année maximal :"
        '
        'Select4
        '
        Me.Select4.AutoSize = True
        Me.Select4.Location = New System.Drawing.Point(12, 295)
        Me.Select4.Name = "Select4"
        Me.Select4.Size = New System.Drawing.Size(14, 13)
        Me.Select4.TabIndex = 4
        Me.Select4.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(29, 276)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(92, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Journée de base :"
        '
        'BaseDay4
        '
        Me.BaseDay4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BaseDay4.FormattingEnabled = True
        Me.BaseDay4.Items.AddRange(New Object() {"Août", "Avril", "Décembre", "Février", "Janvier", "Juillet", "Juin", "Mai", "Mars", "Novembre", "Octobre", "Septembre"})
        Me.BaseDay4.Location = New System.Drawing.Point(32, 292)
        Me.BaseDay4.Name = "BaseDay4"
        Me.BaseDay4.Size = New System.Drawing.Size(198, 21)
        Me.BaseDay4.Sorted = True
        Me.BaseDay4.TabIndex = 1
        '
        'Relative4
        '
        Me.Relative4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Relative4.FormattingEnabled = True
        Me.Relative4.Items.AddRange(New Object() {"Moins", "Plus"})
        Me.Relative4.Location = New System.Drawing.Point(236, 292)
        Me.Relative4.Name = "Relative4"
        Me.Relative4.Size = New System.Drawing.Size(61, 21)
        Me.Relative4.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(300, 276)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Nb jour :"
        '
        'NbJour4
        '
        Me.NbJour4.acceptAlpha = False
        Me.NbJour4.acceptedChars = ",§."
        Me.NbJour4.acceptNumeric = True
        Me.NbJour4.allCapital = False
        Me.NbJour4.allLower = False
        Me.NbJour4.cb_AcceptNegative = False
        Me.NbJour4.currencyBox = True
        Me.NbJour4.firstLetterCapital = True
        Me.NbJour4.firstLettersCapital = False
        Me.NbJour4.Location = New System.Drawing.Point(303, 292)
        Me.NbJour4.manageText = True
        Me.NbJour4.matchExp = ""
        Me.NbJour4.MaxLength = 4
        Me.NbJour4.Name = "NbJour4"
        Me.NbJour4.nbDecimals = CType(0, Short)
        Me.NbJour4.onlyAlphabet = False
        Me.NbJour4.refuseAccents = False
        Me.NbJour4.refusedChars = ""
        Me.NbJour4.showInternalContextMenu = True
        Me.NbJour4.Size = New System.Drawing.Size(44, 20)
        Me.NbJour4.TabIndex = 3
        Me.NbJour4.Text = "0"
        Me.NbJour4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NbJour4.trimText = False
        '
        'Nom
        '
        Me.Nom.acceptAlpha = True
        Me.Nom.acceptedChars = " §,§-§/§\§.§'"
        Me.Nom.acceptNumeric = True
        Me.Nom.allCapital = False
        Me.Nom.allLower = False
        Me.Nom.cb_AcceptNegative = False
        Me.Nom.currencyBox = False
        Me.Nom.firstLetterCapital = True
        Me.Nom.firstLettersCapital = False
        Me.Nom.Location = New System.Drawing.Point(32, 124)
        Me.Nom.manageText = True
        Me.Nom.matchExp = ""
        Me.Nom.Name = "Nom"
        Me.Nom.nbDecimals = CType(-1, Short)
        Me.Nom.onlyAlphabet = True
        Me.Nom.refuseAccents = False
        Me.Nom.refusedChars = ""
        Me.Nom.showInternalContextMenu = True
        Me.Nom.Size = New System.Drawing.Size(228, 20)
        Me.Nom.TabIndex = 3
        Me.Nom.trimText = False
        '
        'ListSpecialDates
        '
        Me.ListSpecialDates.autoAdjust = True
        Me.ListSpecialDates.autoKeyDownSelection = True
        Me.ListSpecialDates.autoSizeHorizontally = False
        Me.ListSpecialDates.autoSizeVertically = False
        Me.ListSpecialDates.BackColor = System.Drawing.Color.White
        Me.ListSpecialDates.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.ListSpecialDates.baseBackColor = System.Drawing.Color.White
        Me.ListSpecialDates.baseFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.ListSpecialDates.baseForeColor = System.Drawing.Color.Black
        Me.ListSpecialDates.bgColor = System.Drawing.Color.White
        Me.ListSpecialDates.borderColor = System.Drawing.Color.Empty
        Me.ListSpecialDates.borderSelColor = System.Drawing.Color.Empty
        Me.ListSpecialDates.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.ListSpecialDates.CausesValidation = False
        Me.ListSpecialDates.clickEnabled = False
        Me.ListSpecialDates.do3D = False
        Me.ListSpecialDates.draw = False
        Me.ListSpecialDates.hScrollColor = System.Drawing.Color.White
        Me.ListSpecialDates.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.ListSpecialDates.hScrolling = True
        Me.ListSpecialDates.hsValue = CType(0, Short)
        Me.ListSpecialDates.itemBorder = CType(0, Short)
        Me.ListSpecialDates.Location = New System.Drawing.Point(32, 12)
        Me.ListSpecialDates.itemMargin = CType(0, Short)
        Me.ListSpecialDates.mouseMove3D = False
        Me.ListSpecialDates.mouseSpeed = 0
        Me.ListSpecialDates.Name = "ListSpecialDates"
        Me.ListSpecialDates.objMaxHeight = 0.0!
        Me.ListSpecialDates.objMaxWidth = 0.0!
        Me.ListSpecialDates.objMinHeight = 0.0!
        Me.ListSpecialDates.objMinWidth = 0.0!
        Me.ListSpecialDates.reverseSorting = False
        Me.ListSpecialDates.selected = CType(-1, Short)
        Me.ListSpecialDates.selectedClickAllowed = True
        Me.ListSpecialDates.Size = New System.Drawing.Size(315, 84)
        Me.ListSpecialDates.sorted = True
        Me.ListSpecialDates.TabIndex = 0
        Me.ListSpecialDates.toolTipText = Nothing
        Me.ListSpecialDates.vScrollColor = System.Drawing.Color.White
        Me.ListSpecialDates.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.ListSpecialDates.vScrolling = True
        Me.ListSpecialDates.vsValue = CType(0, Short)
        '
        'specialDates
        '
        Me.ClientSize = New System.Drawing.Size(381, 560)
        Me.Controls.Add(Me.maximise)
        Me.Controls.Add(Me.vider)
        Me.Controls.Add(Me.delete)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.Adding)
        Me.Controls.Add(Me.Journee3)
        Me.Controls.Add(Me.Relative4)
        Me.Controls.Add(Me.AfterBefore3)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Jour3)
        Me.Controls.Add(Me.Mois3)
        Me.Controls.Add(Me.Journee2)
        Me.Controls.Add(Me.Semaine2)
        Me.Controls.Add(Me.Mois2)
        Me.Controls.Add(Me.Jour1)
        Me.Controls.Add(Me.BaseDay4)
        Me.Controls.Add(Me.Mois1)
        Me.Controls.Add(Me.CodeVBNet)
        Me.Controls.Add(Me.Select5)
        Me.Controls.Add(Me.Select4)
        Me.Controls.Add(Me.Select3)
        Me.Controls.Add(Me.Select2)
        Me.Controls.Add(Me.Select1)
        Me.Controls.Add(Me.NbJour4)
        Me.Controls.Add(Me.MaxYear)
        Me.Controls.Add(Me.Nom)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListSpecialDates)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "specialDates"
        Me.Text = "Gestion des journées spéciales"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _AllowModification As Boolean = True

    Public Property allowModification() As Boolean
        Get
            Return _AllowModification
        End Get
        Set(ByVal value As Boolean)
            _AllowModification = value
        End Set
    End Property

#Region "specialDates Events"
    Private Sub specialDates_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        If _AllowModification Then lockSecteur("SpecialDates.lock", False)
    End Sub

    Private Sub specialDates_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        loadSpecialDates()

        _AllowModification = lockSecteur("SpecialDates.lock", True, "Journées spéciales")
        If _AllowModification = False Then lockAll()
    End Sub
#End Region

    Private Sub lockAll()
        lockButtons(True)
        Adding.Enabled = False
        vider.Enabled = False
        lockItems(1)
        Select1.Enabled = False
        Select2.Enabled = False
        Select3.Enabled = False
        Select4.Enabled = False
        Select5.Enabled = False
        Mois1.Enabled = False
        Jour1.Enabled = False
        Nom.ReadOnly = True
        MaxYear.ReadOnly = True
    End Sub

    Private Sub lockButtons(ByVal trueFalse As Boolean)
        Save.Enabled = Not trueFalse
        delete.Enabled = Not trueFalse
    End Sub

    Private Sub lockItems(ByVal noSelect As Byte)
        Mois1.Enabled = False
        Jour1.Enabled = False
        Mois2.Enabled = False
        Semaine2.Enabled = False
        Journee2.Enabled = False
        Mois3.Enabled = False
        Journee3.Enabled = False
        AfterBefore3.Enabled = False
        Jour3.Enabled = False
        BaseDay4.Enabled = False
        Relative4.Enabled = False
        NbJour4.Enabled = False
        CodeVBNet.Enabled = False
        maximise.Enabled = False
        Select Case noSelect
            Case 1
                Mois1.Enabled = True
                Jour1.Enabled = True

            Case 2
                Mois2.Enabled = True
                Semaine2.Enabled = True
                Journee2.Enabled = True

            Case 3
                Mois3.Enabled = True
                Journee3.Enabled = True
                Jour3.Enabled = True
                AfterBefore3.Enabled = True

            Case 4
                BaseDay4.Enabled = True
                Relative4.Enabled = True
                NbJour4.Enabled = True

            Case 5
                CodeVBNet.Enabled = True
                maximise.Enabled = True
        End Select
    End Sub

    Private Sub selects_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Select1.CheckedChanged, Select2.CheckedChanged, Select3.CheckedChanged, Select5.CheckedChanged, Select4.CheckedChanged
        Dim name As String = CType(sender, RadioButton).Name
        If name <> "" And _AllowModification Then lockItems(name.Substring(name.Length - 1))
    End Sub

    Private Sub vider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles vider.Click
        Me.CodeVBNet.Text = ""
        Me.Mois1.SelectedIndex = 0
        Me.Mois2.SelectedIndex = 0
        Me.Mois3.SelectedIndex = 0
        Me.Jour1.SelectedIndex = 0
        Me.Jour3.SelectedIndex = 0
        Me.Journee2.SelectedIndex = 0
        Me.Journee3.SelectedIndex = 0
        Me.AfterBefore3.SelectedIndex = 0
        Me.Semaine2.SelectedIndex = 0
        Me.Relative4.SelectedIndex = 0
        Me.NbJour4.Text = "0"
        Me.BaseDay4.Items.Clear()
        For i As Integer = 0 To Me.ListSpecialDates.listCount - 1
            Me.BaseDay4.Items.Add(Me.ListSpecialDates.ItemValueA(i))
        Next i
        If Me.BaseDay4.Items.Count <> 0 Then Me.BaseDay4.SelectedIndex = 0

        Nom.Text = ""
        MaxYear.Text = "0"

        Me.ListSpecialDates.selected = -1
    End Sub

    Private Sub adjustDays(ByVal controlFrom As ComboBox, ByVal controlTo As ComboBox)
        Dim curSelected As Integer = controlTo.SelectedIndex
        controlTo.Items.Clear()
        If controlFrom.SelectedIndex = -1 Then Exit Sub

        Dim nbDays() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        For i As Byte = 1 To nbDays(controlFrom.SelectedIndex)
            controlTo.Items.Add(i)
        Next i
        If curSelected = -1 Or curSelected > (controlTo.Items.Count - 1) Then
            controlTo.SelectedIndex = 0
        Else
            controlTo.SelectedIndex = curSelected
        End If
    End Sub

    Private Sub mois1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mois1.SelectedIndexChanged, BaseDay4.SelectedIndexChanged
        adjustDays(Mois1, Jour1)
    End Sub

    Private Sub mois3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mois3.SelectedIndexChanged
        adjustDays(Mois3, Jour3)
    End Sub

    Private Sub maxYear_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MaxYear.KeyDown, NbJour4.KeyDown
        Dim curYear As Integer = 0
        If MaxYear.Text <> "" Then curYear = MaxYear.Text

        If e.KeyCode = Keys.Up Then
            MaxYear.Text = curYear + 1
            e.Handled = True
        ElseIf e.KeyCode = Keys.Down Then
            MaxYear.Text = curYear - 1
            e.Handled = True
        End If
    End Sub

    Public Sub loadSpecialDates()
        Dim lastScrollPos As Integer = ListSpecialDates.vsValue
        Dim lastIndex As Integer = ListSpecialDates.selected
        ListSpecialDates.cls()

        Dim curSD As Generic.List(Of SpecialDate) = SpecialDatesManager.getInstance.getItemables
        If curSD.Count = 0 Then Exit Sub

        Dim arrSD() As SpecialDate = curSD.ToArray()
        For i As Integer = 0 To arrSD.GetUpperBound(0)
            Dim n As Integer = ListSpecialDates.add(arrSD(i).toString)
            ListSpecialDates.ItemValueA(n) = arrSD(i)
        Next i

        ListSpecialDates.draw = True : ListSpecialDates.draw = False
        ListSpecialDates.vsValue = lastScrollPos
        ListSpecialDates.selected = lastIndex
        If ListSpecialDates.itemVisible(ListSpecialDates.selected) <> CI.Controls.List.ItemVisibility.Fully Then ListSpecialDates.showItem(ListSpecialDates.selected)
    End Sub

    Private Sub adding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Adding.Click
        Dim method As SpecialDate.SpecialDateMethod
        If Select1.Checked Then method = SpecialDate.SpecialDateMethod.MonthDaySpecific
        If Select2.Checked Then method = SpecialDate.SpecialDateMethod.MonthSpecificOnDate
        If Select3.Checked Then method = SpecialDate.SpecialDateMethod.DateOnMonthDaySpecific
        If Select4.Checked Then method = SpecialDate.SpecialDateMethod.DateRelative
        If Select5.Checked Then method = SpecialDate.SpecialDateMethod.CodeVBNet

        Dim noBaseDay As Integer = 0
        If BaseDay4.SelectedIndex <> -1 Then noBaseDay = CType(BaseDay4.Items(BaseDay4.SelectedIndex), SpecialDate).noSpecialDate

        SpecialDatesManager.getInstance.addSpecialDate(Nom.Text, MaxYear.Text, method, Mois1.SelectedIndex + 1, Jour1.Text, Mois2.SelectedIndex + 1, Semaine2.SelectedIndex, Journee2.SelectedIndex, Journee3.SelectedIndex, Me.AfterBefore3.SelectedIndex, Mois3.SelectedIndex + 1, Jour3.Text, noBaseDay, Me.Relative4.SelectedIndex, Me.NbJour4.Text, CodeVBNet.Text)
    End Sub

    Private Sub save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        Dim method As SpecialDate.SpecialDateMethod
        If Select1.Checked Then method = SpecialDate.SpecialDateMethod.MonthDaySpecific
        If Select2.Checked Then method = SpecialDate.SpecialDateMethod.MonthSpecificOnDate
        If Select3.Checked Then method = SpecialDate.SpecialDateMethod.DateOnMonthDaySpecific
        If Select4.Checked Then method = SpecialDate.SpecialDateMethod.DateRelative
        If Select5.Checked Then method = SpecialDate.SpecialDateMethod.CodeVBNet

        Dim curSD As SpecialDate = ListSpecialDates.ItemValueA(ListSpecialDates.selected)
        curSD.nom = Nom.Text
        curSD.maxYear = MaxYear.Text
        curSD.method = method
        curSD.month1 = Mois1.SelectedIndex + 1
        curSD.jour1 = Jour1.Text
        curSD.month2 = Mois2.SelectedIndex + 1
        curSD.position2 = Me.Semaine2.SelectedIndex
        curSD.journee2 = Journee2.SelectedIndex
        curSD.journee3 = Journee3.SelectedIndex
        curSD.relative3 = Me.AfterBefore3.SelectedIndex
        curSD.month3 = Me.Mois3.SelectedIndex + 1
        curSD.jour3 = Jour3.Text
        curSD.relative4 = Me.Relative4.SelectedIndex

        Dim noBaseDay As Integer = 0
        If BaseDay4.SelectedIndex <> -1 Then noBaseDay = CType(BaseDay4.Items(BaseDay4.SelectedIndex), SpecialDate).noSpecialDate
        curSD.baseDay4 = noBaseDay

        curSD.nbDays4 = Me.NbJour4.Text
        curSD.codeVBNet = CodeVBNet.Text

        curSD.saveData()
    End Sub

    Private Sub delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete.Click
        If MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette journée spéciale ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then Exit Sub

        Dim curSD As SpecialDate = ListSpecialDates.ItemValueA(ListSpecialDates.selected)
        curSD.delete()
    End Sub

    Private Sub listSpecialDates_SelectedChange() Handles ListSpecialDates.selectedChange
        If ListSpecialDates.selected <> -1 Then
            Dim curSD As SpecialDate = ListSpecialDates.ItemValueA(ListSpecialDates.selected)
            Nom.Text = curSD.nom
            MaxYear.Text = curSD.maxYear
            Select Case curSD.method
                Case SpecialDate.SpecialDateMethod.MonthDaySpecific
                    Select1.Checked = True
                Case SpecialDate.SpecialDateMethod.MonthSpecificOnDate
                    Select2.Checked = True
                Case SpecialDate.SpecialDateMethod.DateOnMonthDaySpecific
                    Select3.Checked = True
                Case SpecialDate.SpecialDateMethod.DateRelative
                    Select4.Checked = True
                Case SpecialDate.SpecialDateMethod.CodeVBNet
                    Select5.Checked = True
            End Select
            Mois1.SelectedIndex = curSD.month1 - 1
            Jour1.Text = curSD.jour1
            Mois2.SelectedIndex = curSD.month2 - 1
            Me.Semaine2.SelectedIndex = curSD.position2
            Journee2.SelectedIndex = curSD.journee2
            Journee3.SelectedIndex = curSD.journee3
            Me.AfterBefore3.SelectedIndex = curSD.relative3
            Me.Mois3.SelectedIndex = curSD.month3 - 1
            Jour3.Text = curSD.jour3
            Me.Relative4.SelectedIndex = curSD.relative4
            'Arrange BaseDay List
            Me.BaseDay4.Items.Clear()
            For i As Integer = 0 To Me.ListSpecialDates.listCount - 1
                If i <> Me.ListSpecialDates.selected Then Me.BaseDay4.Items.Add(Me.ListSpecialDates.ItemValueA(i))
                If CType(Me.ListSpecialDates.ItemValueA(i), SpecialDate).noSpecialDate = curSD.baseDay4 Then Me.BaseDay4.SelectedIndex = Me.BaseDay4.Items.Count - 1
            Next i
            If Me.BaseDay4.Items.Count <> 0 And Me.BaseDay4.SelectedIndex = -1 Then Me.BaseDay4.SelectedIndex = 0
            Me.NbJour4.Text = curSD.nbDays4
            CodeVBNet.Text = curSD.codeVBNet.Replace("\n", vbCrLf)

            If _AllowModification Then lockButtons(False)
        Else
            lockButtons(True)
        End If
    End Sub

    Private Sub maximise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maximise.Click
        TextWindow.getInstance.texteType = RichTextBoxStreamType.PlainText
        TextWindow.getInstance.currentData = CodeVBNet.Text
        TextWindow.getInstance.Text = "Visualisation : Code VB.NET"
        TextWindow.getInstance.isHTML = False
        'If LectureSeule = True Then textemodif.GetInstance.IsLocked = True
        Dim mySel As String = TextWindow.getInstance.ShowTexteModif(0)
        CodeVBNet.Text = TextWindow.getInstance.currentData
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf save_Click)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function = "SpecialDates" Then
            loadSpecialDates()
        End If
    End Sub
End Class
