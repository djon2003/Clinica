<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FolderInfos
    Inherits UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.label36 = New System.Windows.Forms.Label
        Me.label35 = New System.Windows.Forms.Label
        Me.ctlmedecin = New System.Windows.Forms.TextBox
        Me.label34 = New System.Windows.Forms.Label
        Me.label32 = New System.Windows.Forms.Label
        Me.label30 = New System.Windows.Forms.Label
        Me.selectmedecin = New System.Windows.Forms.Button
        Me._Label1_19 = New System.Windows.Forms.Label
        Me._Label1_22 = New System.Windows.Forms.Label
        Me._Label1_23 = New System.Windows.Forms.Label
        Me._Label1_24 = New System.Windows.Forms.Label
        Me._Label1_25 = New System.Windows.Forms.Label
        Me._Label1_26 = New System.Windows.Forms.Label
        Me.selectcode = New System.Windows.Forms.Button
        Me.ctltherapeute = New System.Windows.Forms.TextBox
        Me.ctlcodedossier = New System.Windows.Forms.TextBox
        Me.selectTRP = New System.Windows.Forms.Button
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.label7 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.label9 = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me.ctlDateEval = New System.Windows.Forms.Label
        Me.ctlDateDebut = New System.Windows.Forms.Label
        Me.ctlDateFin = New System.Windows.Forms.Label
        Me.ctlNbAbsences = New System.Windows.Forms.Label
        Me.ctlNbPresences = New System.Windows.Forms.Label
        Me.ctlDateAppel = New System.Windows.Forms.Label
        Me.ctlNoRefMedecin = New System.Windows.Forms.Label
        Me.ctlNoPermis = New System.Windows.Forms.Label
        Me.ctlRemarques = New System.Windows.Forms.TextBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.label12 = New System.Windows.Forms.Label
        Me.ctlDateReference = New ManagedText
        Me.ctlDateRechute = New ManagedText
        Me.ctlService = New ManagedCombo
        Me.ctlTRPToTransfer = New ManagedCombo
        Me.ctlTRPdemande = New ManagedCombo
        Me.ctlregion_Renamed = New ManagedCombo
        Me.ctlDateAccident = New ManagedText
        Me.ctlFrequence = New ManagedCombo
        Me.ctlDuree = New ManagedCombo
        Me.ctlDiagnostic = New ManagedText
        Me.ctlnoref = New ManagedText
        Me.Label13 = New System.Windows.Forms.Label
        Me.ctlReceptionReferenceDate = New ManagedText
        Me.SuspendLayout()
        '
        'label36
        '
        Me.label36.AutoSize = True
        Me.label36.BackColor = System.Drawing.Color.Transparent
        Me.label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.label36.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label36.Location = New System.Drawing.Point(2, 64)
        Me.label36.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label36.Name = "label36"
        Me.label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label36.Size = New System.Drawing.Size(68, 13)
        Me.label36.TabIndex = 210
        Me.label36.Text = "Fréquence :"
        '
        'label35
        '
        Me.label35.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.label35.AutoSize = True
        Me.label35.BackColor = System.Drawing.SystemColors.Control
        Me.label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.label35.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label35.Location = New System.Drawing.Point(191, 64)
        Me.label35.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label35.Name = "label35"
        Me.label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label35.Size = New System.Drawing.Size(46, 13)
        Me.label35.TabIndex = 208
        Me.label35.Text = "Durée :"
        '
        'ctlmedecin
        '
        Me.ctlmedecin.AcceptsReturn = True
        Me.ctlmedecin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlmedecin.BackColor = System.Drawing.SystemColors.Control
        Me.ctlmedecin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ctlmedecin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlmedecin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlmedecin.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlmedecin.Location = New System.Drawing.Point(83, 108)
        Me.ctlmedecin.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlmedecin.MaxLength = 0
        Me.ctlmedecin.Name = "ctlmedecin"
        Me.ctlmedecin.ReadOnly = True
        Me.ctlmedecin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlmedecin.Size = New System.Drawing.Size(298, 13)
        Me.ctlmedecin.TabIndex = 207
        '
        'label34
        '
        Me.label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label34.AutoSize = True
        Me.label34.BackColor = System.Drawing.Color.Transparent
        Me.label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.label34.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label34.Location = New System.Drawing.Point(385, 158)
        Me.label34.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label34.Name = "label34"
        Me.label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label34.Size = New System.Drawing.Size(106, 13)
        Me.label34.TabIndex = 206
        Me.label34.Text = "Date de référence :"
        '
        'label32
        '
        Me.label32.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.label32.AutoSize = True
        Me.label32.BackColor = System.Drawing.Color.Transparent
        Me.label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.label32.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label32.Location = New System.Drawing.Point(191, 39)
        Me.label32.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label32.Name = "label32"
        Me.label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label32.Size = New System.Drawing.Size(96, 13)
        Me.label32.TabIndex = 205
        Me.label32.Text = "Date de rechute :"
        '
        'label30
        '
        Me.label30.AutoSize = True
        Me.label30.BackColor = System.Drawing.Color.Transparent
        Me.label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.label30.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label30.Location = New System.Drawing.Point(2, 39)
        Me.label30.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label30.Name = "label30"
        Me.label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label30.Size = New System.Drawing.Size(93, 13)
        Me.label30.TabIndex = 204
        Me.label30.Text = "Date d'accident :"
        '
        'selectmedecin
        '
        Me.selectmedecin.BackColor = System.Drawing.SystemColors.Control
        Me.selectmedecin.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectmedecin.Enabled = False
        Me.selectmedecin.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectmedecin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectmedecin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectmedecin.Location = New System.Drawing.Point(4, 103)
        Me.selectmedecin.Margin = New System.Windows.Forms.Padding(2)
        Me.selectmedecin.Name = "selectmedecin"
        Me.selectmedecin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectmedecin.Size = New System.Drawing.Size(18, 23)
        Me.selectmedecin.TabIndex = 192
        Me.selectmedecin.UseVisualStyleBackColor = False
        '
        '_Label1_19
        '
        Me._Label1_19.AutoSize = True
        Me._Label1_19.BackColor = System.Drawing.Color.Transparent
        Me._Label1_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_19.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_19.Location = New System.Drawing.Point(28, 107)
        Me._Label1_19.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me._Label1_19.Name = "_Label1_19"
        Me._Label1_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_19.Size = New System.Drawing.Size(58, 13)
        Me._Label1_19.TabIndex = 191
        Me._Label1_19.Text = "Médecin :"
        '
        '_Label1_22
        '
        Me._Label1_22.AutoSize = True
        Me._Label1_22.BackColor = System.Drawing.Color.Transparent
        Me._Label1_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_22.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_22.Location = New System.Drawing.Point(28, 323)
        Me._Label1_22.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me._Label1_22.Name = "_Label1_22"
        Me._Label1_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_22.Size = New System.Drawing.Size(97, 13)
        Me._Label1_22.TabIndex = 193
        Me._Label1_22.Text = "Code du dossier :"
        '
        '_Label1_23
        '
        Me._Label1_23.AutoSize = True
        Me._Label1_23.BackColor = System.Drawing.Color.Transparent
        Me._Label1_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_23.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_23.Location = New System.Drawing.Point(29, 130)
        Me._Label1_23.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me._Label1_23.Name = "_Label1_23"
        Me._Label1_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_23.Size = New System.Drawing.Size(69, 13)
        Me._Label1_23.TabIndex = 195
        Me._Label1_23.Text = "Diagnostic :"
        '
        '_Label1_24
        '
        Me._Label1_24.AutoSize = True
        Me._Label1_24.BackColor = System.Drawing.Color.Transparent
        Me._Label1_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_24.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_24.Location = New System.Drawing.Point(28, 229)
        Me._Label1_24.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me._Label1_24.Name = "_Label1_24"
        Me._Label1_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_24.Size = New System.Drawing.Size(98, 13)
        Me._Label1_24.TabIndex = 196
        Me._Label1_24.Text = "Site de la lésion :"
        '
        '_Label1_25
        '
        Me._Label1_25.AutoSize = True
        Me._Label1_25.BackColor = System.Drawing.Color.Transparent
        Me._Label1_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_25.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_25.Location = New System.Drawing.Point(28, 205)
        Me._Label1_25.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me._Label1_25.Name = "_Label1_25"
        Me._Label1_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_25.Size = New System.Drawing.Size(116, 13)
        Me._Label1_25.TabIndex = 197
        Me._Label1_25.Text = "Thérapeute traitant :"
        '
        '_Label1_26
        '
        Me._Label1_26.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._Label1_26.AutoSize = True
        Me._Label1_26.BackColor = System.Drawing.Color.Transparent
        Me._Label1_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_26.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_26.Location = New System.Drawing.Point(385, 39)
        Me._Label1_26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me._Label1_26.Name = "_Label1_26"
        Me._Label1_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_26.Size = New System.Drawing.Size(94, 13)
        Me._Label1_26.TabIndex = 202
        Me._Label1_26.Text = "N° de référence :"
        '
        'selectcode
        '
        Me.selectcode.BackColor = System.Drawing.SystemColors.Control
        Me.selectcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectcode.Enabled = False
        Me.selectcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectcode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectcode.Location = New System.Drawing.Point(4, 320)
        Me.selectcode.Margin = New System.Windows.Forms.Padding(2)
        Me.selectcode.Name = "selectcode"
        Me.selectcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectcode.Size = New System.Drawing.Size(18, 23)
        Me.selectcode.TabIndex = 194
        Me.selectcode.UseVisualStyleBackColor = False
        '
        'ctltherapeute
        '
        Me.ctltherapeute.AcceptsReturn = True
        Me.ctltherapeute.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctltherapeute.BackColor = System.Drawing.SystemColors.Control
        Me.ctltherapeute.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ctltherapeute.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctltherapeute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctltherapeute.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctltherapeute.Location = New System.Drawing.Point(167, 207)
        Me.ctltherapeute.Margin = New System.Windows.Forms.Padding(2)
        Me.ctltherapeute.MaxLength = 0
        Me.ctltherapeute.Name = "ctltherapeute"
        Me.ctltherapeute.ReadOnly = True
        Me.ctltherapeute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctltherapeute.Size = New System.Drawing.Size(214, 13)
        Me.ctltherapeute.TabIndex = 200
        '
        'ctlcodedossier
        '
        Me.ctlcodedossier.AcceptsReturn = True
        Me.ctlcodedossier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlcodedossier.BackColor = System.Drawing.SystemColors.Control
        Me.ctlcodedossier.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ctlcodedossier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlcodedossier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlcodedossier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlcodedossier.Location = New System.Drawing.Point(124, 324)
        Me.ctlcodedossier.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlcodedossier.MaxLength = 0
        Me.ctlcodedossier.Name = "ctlcodedossier"
        Me.ctlcodedossier.ReadOnly = True
        Me.ctlcodedossier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlcodedossier.Size = New System.Drawing.Size(145, 13)
        Me.ctlcodedossier.TabIndex = 201
        '
        'selectTRP
        '
        Me.selectTRP.BackColor = System.Drawing.SystemColors.Control
        Me.selectTRP.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectTRP.Enabled = False
        Me.selectTRP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectTRP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectTRP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectTRP.Location = New System.Drawing.Point(4, 201)
        Me.selectTRP.Margin = New System.Windows.Forms.Padding(2)
        Me.selectTRP.Name = "selectTRP"
        Me.selectTRP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectTRP.Size = New System.Drawing.Size(18, 23)
        Me.selectTRP.TabIndex = 215
        Me.selectTRP.UseVisualStyleBackColor = False
        '
        'label1
        '
        Me.label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(191, 21)
        Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(119, 13)
        Me.label1.TabIndex = 193
        Me.label1.Text = "Début de traitement :"
        '
        'label2
        '
        Me.label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(385, 21)
        Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label2.Name = "label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(105, 13)
        Me.label2.TabIndex = 193
        Me.label2.Text = "Fin de traitement :"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(2, 342)
        Me.label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label3.Name = "label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(73, 13)
        Me.label3.TabIndex = 193
        Me.label3.Text = "Remarques :"
        '
        'label4
        '
        Me.label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.label4.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label4.Location = New System.Drawing.Point(385, 2)
        Me.label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label4.Name = "label4"
        Me.label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label4.Size = New System.Drawing.Size(113, 13)
        Me.label4.TabIndex = 193
        Me.label4.Text = "Nombre d'absences :"
        '
        'label5
        '
        Me.label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.label5.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label5.Location = New System.Drawing.Point(191, 2)
        Me.label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label5.Name = "label5"
        Me.label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label5.Size = New System.Drawing.Size(124, 13)
        Me.label5.TabIndex = 193
        Me.label5.Text = "Nombre de présences :"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.Color.Transparent
        Me.label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.label6.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label6.Location = New System.Drawing.Point(29, 258)
        Me.label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label6.Name = "label6"
        Me.label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label6.Size = New System.Drawing.Size(123, 13)
        Me.label6.TabIndex = 193
        Me.label6.Text = "Thérapeute demandé :"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.Color.Transparent
        Me.label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.label7.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label7.Location = New System.Drawing.Point(2, 2)
        Me.label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label7.Name = "label7"
        Me.label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label7.Size = New System.Drawing.Size(78, 13)
        Me.label7.TabIndex = 193
        Me.label7.Text = "Date d'appel :"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.label8.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label8.Location = New System.Drawing.Point(2, 21)
        Me.label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label8.Name = "label8"
        Me.label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label8.Size = New System.Drawing.Size(105, 13)
        Me.label8.TabIndex = 193
        Me.label8.Text = "Date d'évaluation :"
        '
        'label9
        '
        Me.label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.Color.Transparent
        Me.label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.label9.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label9.Location = New System.Drawing.Point(385, 107)
        Me.label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label9.Name = "label9"
        Me.label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label9.Size = New System.Drawing.Size(80, 13)
        Me.label9.TabIndex = 193
        Me.label9.Text = "N° de permis :"
        '
        'label10
        '
        Me.label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label10.AutoSize = True
        Me.label10.BackColor = System.Drawing.Color.Transparent
        Me.label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.label10.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label10.Location = New System.Drawing.Point(385, 205)
        Me.label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label10.Name = "label10"
        Me.label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label10.Size = New System.Drawing.Size(80, 13)
        Me.label10.TabIndex = 193
        Me.label10.Text = "N° de permis :"
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.BackColor = System.Drawing.Color.Transparent
        Me.label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.label11.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label11.Location = New System.Drawing.Point(273, 323)
        Me.label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label11.Name = "label11"
        Me.label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label11.Size = New System.Drawing.Size(52, 13)
        Me.label11.TabIndex = 193
        Me.label11.Text = "Service :"
        '
        'ctlDateEval
        '
        Me.ctlDateEval.AutoSize = True
        Me.ctlDateEval.BackColor = System.Drawing.Color.Transparent
        Me.ctlDateEval.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlDateEval.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDateEval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlDateEval.Location = New System.Drawing.Point(107, 22)
        Me.ctlDateEval.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlDateEval.Name = "ctlDateEval"
        Me.ctlDateEval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDateEval.Size = New System.Drawing.Size(0, 14)
        Me.ctlDateEval.TabIndex = 193
        '
        'ctlDateDebut
        '
        Me.ctlDateDebut.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ctlDateDebut.AutoSize = True
        Me.ctlDateDebut.BackColor = System.Drawing.Color.Transparent
        Me.ctlDateDebut.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlDateDebut.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDateDebut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlDateDebut.Location = New System.Drawing.Point(310, 22)
        Me.ctlDateDebut.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlDateDebut.Name = "ctlDateDebut"
        Me.ctlDateDebut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDateDebut.Size = New System.Drawing.Size(0, 14)
        Me.ctlDateDebut.TabIndex = 193
        '
        'ctlDateFin
        '
        Me.ctlDateFin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlDateFin.AutoSize = True
        Me.ctlDateFin.BackColor = System.Drawing.Color.Transparent
        Me.ctlDateFin.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlDateFin.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDateFin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlDateFin.Location = New System.Drawing.Point(491, 22)
        Me.ctlDateFin.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlDateFin.Name = "ctlDateFin"
        Me.ctlDateFin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDateFin.Size = New System.Drawing.Size(0, 14)
        Me.ctlDateFin.TabIndex = 193
        '
        'ctlNbAbsences
        '
        Me.ctlNbAbsences.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlNbAbsences.AutoSize = True
        Me.ctlNbAbsences.BackColor = System.Drawing.Color.Transparent
        Me.ctlNbAbsences.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlNbAbsences.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlNbAbsences.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlNbAbsences.Location = New System.Drawing.Point(498, 2)
        Me.ctlNbAbsences.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlNbAbsences.Name = "ctlNbAbsences"
        Me.ctlNbAbsences.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlNbAbsences.Size = New System.Drawing.Size(0, 14)
        Me.ctlNbAbsences.TabIndex = 193
        '
        'ctlNbPresences
        '
        Me.ctlNbPresences.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ctlNbPresences.AutoSize = True
        Me.ctlNbPresences.BackColor = System.Drawing.Color.Transparent
        Me.ctlNbPresences.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlNbPresences.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlNbPresences.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlNbPresences.Location = New System.Drawing.Point(315, 2)
        Me.ctlNbPresences.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlNbPresences.Name = "ctlNbPresences"
        Me.ctlNbPresences.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlNbPresences.Size = New System.Drawing.Size(0, 14)
        Me.ctlNbPresences.TabIndex = 193
        '
        'ctlDateAppel
        '
        Me.ctlDateAppel.AutoSize = True
        Me.ctlDateAppel.BackColor = System.Drawing.Color.Transparent
        Me.ctlDateAppel.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlDateAppel.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDateAppel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlDateAppel.Location = New System.Drawing.Point(80, 2)
        Me.ctlDateAppel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlDateAppel.Name = "ctlDateAppel"
        Me.ctlDateAppel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDateAppel.Size = New System.Drawing.Size(0, 14)
        Me.ctlDateAppel.TabIndex = 193
        '
        'ctlNoRefMedecin
        '
        Me.ctlNoRefMedecin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlNoRefMedecin.AutoSize = True
        Me.ctlNoRefMedecin.BackColor = System.Drawing.Color.Transparent
        Me.ctlNoRefMedecin.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlNoRefMedecin.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlNoRefMedecin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlNoRefMedecin.Location = New System.Drawing.Point(469, 108)
        Me.ctlNoRefMedecin.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlNoRefMedecin.Name = "ctlNoRefMedecin"
        Me.ctlNoRefMedecin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlNoRefMedecin.Size = New System.Drawing.Size(0, 14)
        Me.ctlNoRefMedecin.TabIndex = 193
        '
        'ctlNoPermis
        '
        Me.ctlNoPermis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlNoPermis.AutoSize = True
        Me.ctlNoPermis.BackColor = System.Drawing.Color.Transparent
        Me.ctlNoPermis.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlNoPermis.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlNoPermis.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ctlNoPermis.Location = New System.Drawing.Point(465, 207)
        Me.ctlNoPermis.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ctlNoPermis.Name = "ctlNoPermis"
        Me.ctlNoPermis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlNoPermis.Size = New System.Drawing.Size(0, 14)
        Me.ctlNoPermis.TabIndex = 193
        '
        'ctlRemarques
        '
        Me.ctlRemarques.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlRemarques.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlRemarques.Location = New System.Drawing.Point(4, 357)
        Me.ctlRemarques.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlRemarques.Multiline = True
        Me.ctlRemarques.Name = "ctlRemarques"
        Me.ctlRemarques.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ctlRemarques.Size = New System.Drawing.Size(543, 67)
        Me.ctlRemarques.TabIndex = 216
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.BackColor = System.Drawing.Color.Transparent
        Me.label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.label12.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label12.Location = New System.Drawing.Point(29, 288)
        Me.label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label12.Name = "label12"
        Me.label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label12.Size = New System.Drawing.Size(138, 13)
        Me.label12.TabIndex = 193
        Me.label12.Text = "Thérapeute à transférer :"
        '
        'ctlDateReference
        '
        Me.ctlDateReference.acceptAlpha = True
        Me.ctlDateReference.acceptedChars = ""
        Me.ctlDateReference.acceptNumeric = True
        Me.ctlDateReference.AcceptsReturn = True
        Me.ctlDateReference.allCapital = False
        Me.ctlDateReference.allLower = False
        Me.ctlDateReference.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlDateReference.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ctlDateReference.blockOnMaximum = False
        Me.ctlDateReference.blockOnMinimum = False
        Me.ctlDateReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctlDateReference.cb_AcceptLeftZeros = False
        Me.ctlDateReference.cb_AcceptNegative = False
        Me.ctlDateReference.currencyBox = False
        Me.ctlDateReference.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlDateReference.firstLetterCapital = False
        Me.ctlDateReference.firstLettersCapital = False
        Me.ctlDateReference.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDateReference.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlDateReference.Location = New System.Drawing.Point(488, 158)
        Me.ctlDateReference.manageText = False
        Me.ctlDateReference.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlDateReference.matchExp = ""
        Me.ctlDateReference.maximum = 0
        Me.ctlDateReference.MaxLength = 0
        Me.ctlDateReference.minimum = 0
        Me.ctlDateReference.Name = "ctlDateReference"
        Me.ctlDateReference.nbDecimals = CType(-1, Short)
        Me.ctlDateReference.onlyAlphabet = False
        Me.ctlDateReference.ReadOnly = True
        Me.ctlDateReference.refuseAccents = False
        Me.ctlDateReference.refusedChars = ""
        Me.ctlDateReference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDateReference.showInternalContextMenu = False
        Me.ctlDateReference.Size = New System.Drawing.Size(59, 20)
        Me.ctlDateReference.TabIndex = 214
        Me.ctlDateReference.trimText = False
        '
        'ctlDateRechute
        '
        Me.ctlDateRechute.acceptAlpha = True
        Me.ctlDateRechute.acceptedChars = ""
        Me.ctlDateRechute.acceptNumeric = True
        Me.ctlDateRechute.AcceptsReturn = True
        Me.ctlDateRechute.allCapital = False
        Me.ctlDateRechute.allLower = False
        Me.ctlDateRechute.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ctlDateRechute.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ctlDateRechute.blockOnMaximum = False
        Me.ctlDateRechute.blockOnMinimum = False
        Me.ctlDateRechute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctlDateRechute.cb_AcceptLeftZeros = False
        Me.ctlDateRechute.cb_AcceptNegative = False
        Me.ctlDateRechute.currencyBox = False
        Me.ctlDateRechute.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlDateRechute.firstLetterCapital = False
        Me.ctlDateRechute.firstLettersCapital = False
        Me.ctlDateRechute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDateRechute.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlDateRechute.Location = New System.Drawing.Point(289, 37)
        Me.ctlDateRechute.manageText = False
        Me.ctlDateRechute.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlDateRechute.matchExp = ""
        Me.ctlDateRechute.maximum = 0
        Me.ctlDateRechute.MaxLength = 0
        Me.ctlDateRechute.minimum = 0
        Me.ctlDateRechute.Name = "ctlDateRechute"
        Me.ctlDateRechute.nbDecimals = CType(-1, Short)
        Me.ctlDateRechute.onlyAlphabet = False
        Me.ctlDateRechute.ReadOnly = True
        Me.ctlDateRechute.refuseAccents = False
        Me.ctlDateRechute.refusedChars = ""
        Me.ctlDateRechute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDateRechute.showInternalContextMenu = False
        Me.ctlDateRechute.Size = New System.Drawing.Size(59, 20)
        Me.ctlDateRechute.TabIndex = 213
        Me.ctlDateRechute.trimText = False
        '
        'ctlService
        '
        Me.ctlService.acceptAlpha = True
        Me.ctlService.acceptedChars = Nothing
        Me.ctlService.acceptNumeric = True
        Me.ctlService.allCapital = False
        Me.ctlService.allLower = False
        Me.ctlService.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlService.autoComplete = True
        Me.ctlService.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ctlService.autoSizeDropDown = True
        Me.ctlService.BackColor = System.Drawing.Color.White
        Me.ctlService.blockOnMaximum = False
        Me.ctlService.blockOnMinimum = False
        Me.ctlService.cb_AcceptLeftZeros = False
        Me.ctlService.cb_AcceptNegative = False
        Me.ctlService.currencyBox = False
        Me.ctlService.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlService.dbField = Nothing
        Me.ctlService.doComboDelete = True
        Me.ctlService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ctlService.firstLetterCapital = True
        Me.ctlService.firstLettersCapital = False
        Me.ctlService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlService.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlService.itemsToolTipDuration = 10000
        Me.ctlService.Location = New System.Drawing.Point(329, 321)
        Me.ctlService.manageText = True
        Me.ctlService.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlService.matchExp = ""
        Me.ctlService.maximum = 0
        Me.ctlService.minimum = 0
        Me.ctlService.Name = "ctlService"
        Me.ctlService.nbDecimals = CType(-1, Short)
        Me.ctlService.onlyAlphabet = False
        Me.ctlService.pathOfList = Nothing
        Me.ctlService.ReadOnly = False
        Me.ctlService.refuseAccents = False
        Me.ctlService.refusedChars = "-"
        Me.ctlService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlService.showItemsToolTip = False
        Me.ctlService.Size = New System.Drawing.Size(218, 22)
        Me.ctlService.TabIndex = 199
        Me.ctlService.trimText = False
        '
        'ctlTRPToTransfer
        '
        Me.ctlTRPToTransfer.acceptAlpha = True
        Me.ctlTRPToTransfer.acceptedChars = Nothing
        Me.ctlTRPToTransfer.acceptNumeric = True
        Me.ctlTRPToTransfer.allCapital = False
        Me.ctlTRPToTransfer.allLower = False
        Me.ctlTRPToTransfer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlTRPToTransfer.autoComplete = True
        Me.ctlTRPToTransfer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ctlTRPToTransfer.autoSizeDropDown = True
        Me.ctlTRPToTransfer.BackColor = System.Drawing.Color.White
        Me.ctlTRPToTransfer.blockOnMaximum = False
        Me.ctlTRPToTransfer.blockOnMinimum = False
        Me.ctlTRPToTransfer.cb_AcceptLeftZeros = False
        Me.ctlTRPToTransfer.cb_AcceptNegative = False
        Me.ctlTRPToTransfer.currencyBox = False
        Me.ctlTRPToTransfer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlTRPToTransfer.dbField = Nothing
        Me.ctlTRPToTransfer.doComboDelete = True
        Me.ctlTRPToTransfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ctlTRPToTransfer.firstLetterCapital = True
        Me.ctlTRPToTransfer.firstLettersCapital = False
        Me.ctlTRPToTransfer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlTRPToTransfer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlTRPToTransfer.itemsToolTipDuration = 10000
        Me.ctlTRPToTransfer.Location = New System.Drawing.Point(167, 285)
        Me.ctlTRPToTransfer.manageText = True
        Me.ctlTRPToTransfer.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlTRPToTransfer.matchExp = ""
        Me.ctlTRPToTransfer.maximum = 0
        Me.ctlTRPToTransfer.minimum = 0
        Me.ctlTRPToTransfer.Name = "ctlTRPToTransfer"
        Me.ctlTRPToTransfer.nbDecimals = CType(-1, Short)
        Me.ctlTRPToTransfer.onlyAlphabet = False
        Me.ctlTRPToTransfer.pathOfList = Nothing
        Me.ctlTRPToTransfer.ReadOnly = False
        Me.ctlTRPToTransfer.refuseAccents = False
        Me.ctlTRPToTransfer.refusedChars = "-"
        Me.ctlTRPToTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlTRPToTransfer.showItemsToolTip = False
        Me.ctlTRPToTransfer.Size = New System.Drawing.Size(380, 22)
        Me.ctlTRPToTransfer.Sorted = True
        Me.ctlTRPToTransfer.TabIndex = 199
        Me.ctlTRPToTransfer.trimText = False
        '
        'ctlTRPdemande
        '
        Me.ctlTRPdemande.acceptAlpha = True
        Me.ctlTRPdemande.acceptedChars = Nothing
        Me.ctlTRPdemande.acceptNumeric = True
        Me.ctlTRPdemande.allCapital = False
        Me.ctlTRPdemande.allLower = False
        Me.ctlTRPdemande.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlTRPdemande.autoComplete = True
        Me.ctlTRPdemande.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ctlTRPdemande.autoSizeDropDown = True
        Me.ctlTRPdemande.BackColor = System.Drawing.Color.White
        Me.ctlTRPdemande.blockOnMaximum = False
        Me.ctlTRPdemande.blockOnMinimum = False
        Me.ctlTRPdemande.cb_AcceptLeftZeros = False
        Me.ctlTRPdemande.cb_AcceptNegative = False
        Me.ctlTRPdemande.currencyBox = False
        Me.ctlTRPdemande.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlTRPdemande.dbField = Nothing
        Me.ctlTRPdemande.doComboDelete = True
        Me.ctlTRPdemande.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ctlTRPdemande.firstLetterCapital = True
        Me.ctlTRPdemande.firstLettersCapital = False
        Me.ctlTRPdemande.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlTRPdemande.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlTRPdemande.itemsToolTipDuration = 10000
        Me.ctlTRPdemande.Location = New System.Drawing.Point(167, 255)
        Me.ctlTRPdemande.manageText = True
        Me.ctlTRPdemande.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlTRPdemande.matchExp = ""
        Me.ctlTRPdemande.maximum = 0
        Me.ctlTRPdemande.minimum = 0
        Me.ctlTRPdemande.Name = "ctlTRPdemande"
        Me.ctlTRPdemande.nbDecimals = CType(-1, Short)
        Me.ctlTRPdemande.onlyAlphabet = False
        Me.ctlTRPdemande.pathOfList = Nothing
        Me.ctlTRPdemande.ReadOnly = False
        Me.ctlTRPdemande.refuseAccents = False
        Me.ctlTRPdemande.refusedChars = "-"
        Me.ctlTRPdemande.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlTRPdemande.showItemsToolTip = False
        Me.ctlTRPdemande.Size = New System.Drawing.Size(380, 22)
        Me.ctlTRPdemande.Sorted = True
        Me.ctlTRPdemande.TabIndex = 199
        Me.ctlTRPdemande.trimText = False
        '
        'ctlregion_Renamed
        '
        Me.ctlregion_Renamed.acceptAlpha = True
        Me.ctlregion_Renamed.acceptedChars = Nothing
        Me.ctlregion_Renamed.acceptNumeric = True
        Me.ctlregion_Renamed.allCapital = False
        Me.ctlregion_Renamed.allLower = False
        Me.ctlregion_Renamed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlregion_Renamed.autoComplete = True
        Me.ctlregion_Renamed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ctlregion_Renamed.autoSizeDropDown = True
        Me.ctlregion_Renamed.BackColor = System.Drawing.Color.White
        Me.ctlregion_Renamed.blockOnMaximum = False
        Me.ctlregion_Renamed.blockOnMinimum = False
        Me.ctlregion_Renamed.cb_AcceptLeftZeros = False
        Me.ctlregion_Renamed.cb_AcceptNegative = False
        Me.ctlregion_Renamed.currencyBox = False
        Me.ctlregion_Renamed.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlregion_Renamed.dbField = "SiteLesion.SiteLesion"
        Me.ctlregion_Renamed.doComboDelete = True
        Me.ctlregion_Renamed.firstLetterCapital = True
        Me.ctlregion_Renamed.firstLettersCapital = False
        Me.ctlregion_Renamed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlregion_Renamed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlregion_Renamed.itemsToolTipDuration = 10000
        Me.ctlregion_Renamed.Location = New System.Drawing.Point(167, 227)
        Me.ctlregion_Renamed.manageText = True
        Me.ctlregion_Renamed.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlregion_Renamed.matchExp = ""
        Me.ctlregion_Renamed.maximum = 0
        Me.ctlregion_Renamed.minimum = 0
        Me.ctlregion_Renamed.Name = "ctlregion_Renamed"
        Me.ctlregion_Renamed.nbDecimals = CType(-1, Short)
        Me.ctlregion_Renamed.onlyAlphabet = False
        Me.ctlregion_Renamed.pathOfList = Nothing
        Me.ctlregion_Renamed.ReadOnly = False
        Me.ctlregion_Renamed.refuseAccents = False
        Me.ctlregion_Renamed.refusedChars = "-"
        Me.ctlregion_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlregion_Renamed.showItemsToolTip = False
        Me.ctlregion_Renamed.Size = New System.Drawing.Size(380, 22)
        Me.ctlregion_Renamed.TabIndex = 199
        Me.ctlregion_Renamed.trimText = False
        '
        'ctlDateAccident
        '
        Me.ctlDateAccident.acceptAlpha = True
        Me.ctlDateAccident.acceptedChars = ""
        Me.ctlDateAccident.acceptNumeric = True
        Me.ctlDateAccident.AcceptsReturn = True
        Me.ctlDateAccident.allCapital = False
        Me.ctlDateAccident.allLower = False
        Me.ctlDateAccident.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ctlDateAccident.blockOnMaximum = False
        Me.ctlDateAccident.blockOnMinimum = False
        Me.ctlDateAccident.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctlDateAccident.cb_AcceptLeftZeros = False
        Me.ctlDateAccident.cb_AcceptNegative = False
        Me.ctlDateAccident.currencyBox = False
        Me.ctlDateAccident.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlDateAccident.firstLetterCapital = False
        Me.ctlDateAccident.firstLettersCapital = False
        Me.ctlDateAccident.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDateAccident.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlDateAccident.Location = New System.Drawing.Point(96, 37)
        Me.ctlDateAccident.manageText = False
        Me.ctlDateAccident.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlDateAccident.matchExp = ""
        Me.ctlDateAccident.maximum = 0
        Me.ctlDateAccident.MaxLength = 0
        Me.ctlDateAccident.minimum = 0
        Me.ctlDateAccident.Name = "ctlDateAccident"
        Me.ctlDateAccident.nbDecimals = CType(-1, Short)
        Me.ctlDateAccident.onlyAlphabet = False
        Me.ctlDateAccident.ReadOnly = True
        Me.ctlDateAccident.refuseAccents = False
        Me.ctlDateAccident.refusedChars = ""
        Me.ctlDateAccident.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDateAccident.showInternalContextMenu = False
        Me.ctlDateAccident.Size = New System.Drawing.Size(59, 20)
        Me.ctlDateAccident.TabIndex = 212
        Me.ctlDateAccident.trimText = False
        '
        'ctlFrequence
        '
        Me.ctlFrequence.acceptAlpha = True
        Me.ctlFrequence.acceptedChars = Nothing
        Me.ctlFrequence.acceptNumeric = True
        Me.ctlFrequence.allCapital = False
        Me.ctlFrequence.allLower = False
        Me.ctlFrequence.autoComplete = True
        Me.ctlFrequence.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ctlFrequence.autoSizeDropDown = True
        Me.ctlFrequence.BackColor = System.Drawing.Color.White
        Me.ctlFrequence.blockOnMaximum = False
        Me.ctlFrequence.blockOnMinimum = False
        Me.ctlFrequence.cb_AcceptLeftZeros = False
        Me.ctlFrequence.cb_AcceptNegative = False
        Me.ctlFrequence.currencyBox = False
        Me.ctlFrequence.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlFrequence.dbField = Nothing
        Me.ctlFrequence.doComboDelete = True
        Me.ctlFrequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ctlFrequence.firstLetterCapital = False
        Me.ctlFrequence.firstLettersCapital = False
        Me.ctlFrequence.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlFrequence.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlFrequence.itemsToolTipDuration = 10000
        Me.ctlFrequence.Location = New System.Drawing.Point(67, 62)
        Me.ctlFrequence.manageText = True
        Me.ctlFrequence.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlFrequence.matchExp = Nothing
        Me.ctlFrequence.maximum = 0
        Me.ctlFrequence.minimum = 0
        Me.ctlFrequence.Name = "ctlFrequence"
        Me.ctlFrequence.nbDecimals = CType(-1, Short)
        Me.ctlFrequence.onlyAlphabet = False
        Me.ctlFrequence.pathOfList = Nothing
        Me.ctlFrequence.ReadOnly = False
        Me.ctlFrequence.refuseAccents = False
        Me.ctlFrequence.refusedChars = Nothing
        Me.ctlFrequence.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlFrequence.showItemsToolTip = False
        Me.ctlFrequence.Size = New System.Drawing.Size(79, 22)
        Me.ctlFrequence.TabIndex = 211
        Me.ctlFrequence.trimText = False
        '
        'ctlDuree
        '
        Me.ctlDuree.acceptAlpha = True
        Me.ctlDuree.acceptedChars = Nothing
        Me.ctlDuree.acceptNumeric = True
        Me.ctlDuree.allCapital = False
        Me.ctlDuree.allLower = False
        Me.ctlDuree.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ctlDuree.autoComplete = True
        Me.ctlDuree.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ctlDuree.autoSizeDropDown = True
        Me.ctlDuree.BackColor = System.Drawing.Color.White
        Me.ctlDuree.blockOnMaximum = False
        Me.ctlDuree.blockOnMinimum = False
        Me.ctlDuree.cb_AcceptLeftZeros = False
        Me.ctlDuree.cb_AcceptNegative = False
        Me.ctlDuree.currencyBox = False
        Me.ctlDuree.Cursor = System.Windows.Forms.Cursors.Default
        Me.ctlDuree.dbField = Nothing
        Me.ctlDuree.doComboDelete = True
        Me.ctlDuree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ctlDuree.DropDownWidth = 62
        Me.ctlDuree.firstLetterCapital = False
        Me.ctlDuree.firstLettersCapital = False
        Me.ctlDuree.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDuree.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlDuree.Items.AddRange(New Object() {"???", "005 jours", "010 jours", "015 jours", "020 jours", "025 jours", "030 jours", "035 jours", "040 jours", "045 jours", "050 jours", "055 jours", "060 jours", "065 jours", "070 jours", "075 jours", "080 jours", "085 jours", "090 jours", "095 jours", "100 jours", "105 jours", "110 jours", "115 jours", "120 jours", "125 jours", "130 jours", "135 jours", "140 jours", "145 jours", "150 jours", "155 jours", "160 jours", "165 jours", "170 jours", "175 jours", "180 jours", "185 jours", "190 jours", "195 jours", "200 jours", "205 jours", "210 jours", "215 jours", "220 jours", "225 jours", "230 jours", "235 jours", "240 jours", "245 jours", "250 jours", "255 jours", "260 jours", "265 jours", "270 jours", "275 jours", "280 jours", "285 jours", "290 jours", "295 jours", "300 jours", "305 jours", "310 jours", "315 jours", "320 jours", "325 jours", "330 jours", "335 jours", "340 jours", "345 jours", "350 jours", "355 jours", "360 jours", "365 jours", "Plus de 365 jours"})
        Me.ctlDuree.itemsToolTipDuration = 10000
        Me.ctlDuree.Location = New System.Drawing.Point(241, 62)
        Me.ctlDuree.manageText = True
        Me.ctlDuree.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlDuree.matchExp = Nothing
        Me.ctlDuree.maximum = 0
        Me.ctlDuree.minimum = 0
        Me.ctlDuree.Name = "ctlDuree"
        Me.ctlDuree.nbDecimals = CType(-1, Short)
        Me.ctlDuree.onlyAlphabet = False
        Me.ctlDuree.pathOfList = Nothing
        Me.ctlDuree.ReadOnly = False
        Me.ctlDuree.refuseAccents = False
        Me.ctlDuree.refusedChars = Nothing
        Me.ctlDuree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDuree.showItemsToolTip = False
        Me.ctlDuree.Size = New System.Drawing.Size(60, 22)
        Me.ctlDuree.TabIndex = 209
        Me.ctlDuree.trimText = False
        '
        'ctlDiagnostic
        '
        Me.ctlDiagnostic.acceptAlpha = True
        Me.ctlDiagnostic.acceptedChars = ""
        Me.ctlDiagnostic.acceptNumeric = True
        Me.ctlDiagnostic.AcceptsReturn = True
        Me.ctlDiagnostic.allCapital = False
        Me.ctlDiagnostic.allLower = False
        Me.ctlDiagnostic.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlDiagnostic.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ctlDiagnostic.blockOnMaximum = False
        Me.ctlDiagnostic.blockOnMinimum = False
        Me.ctlDiagnostic.cb_AcceptLeftZeros = False
        Me.ctlDiagnostic.cb_AcceptNegative = False
        Me.ctlDiagnostic.currencyBox = False
        Me.ctlDiagnostic.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlDiagnostic.firstLetterCapital = True
        Me.ctlDiagnostic.firstLettersCapital = False
        Me.ctlDiagnostic.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlDiagnostic.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlDiagnostic.Location = New System.Drawing.Point(102, 128)
        Me.ctlDiagnostic.manageText = True
        Me.ctlDiagnostic.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlDiagnostic.matchExp = ""
        Me.ctlDiagnostic.maximum = 0
        Me.ctlDiagnostic.MaxLength = 0
        Me.ctlDiagnostic.minimum = 0
        Me.ctlDiagnostic.Name = "ctlDiagnostic"
        Me.ctlDiagnostic.nbDecimals = CType(-1, Short)
        Me.ctlDiagnostic.onlyAlphabet = False
        Me.ctlDiagnostic.ReadOnly = True
        Me.ctlDiagnostic.refuseAccents = False
        Me.ctlDiagnostic.refusedChars = ""
        Me.ctlDiagnostic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlDiagnostic.showInternalContextMenu = True
        Me.ctlDiagnostic.Size = New System.Drawing.Size(445, 20)
        Me.ctlDiagnostic.TabIndex = 198
        Me.ctlDiagnostic.trimText = False
        '
        'ctlnoref
        '
        Me.ctlnoref.acceptAlpha = True
        Me.ctlnoref.acceptedChars = ""
        Me.ctlnoref.acceptNumeric = True
        Me.ctlnoref.AcceptsReturn = True
        Me.ctlnoref.allCapital = True
        Me.ctlnoref.allLower = False
        Me.ctlnoref.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlnoref.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ctlnoref.blockOnMaximum = False
        Me.ctlnoref.blockOnMinimum = False
        Me.ctlnoref.cb_AcceptLeftZeros = False
        Me.ctlnoref.cb_AcceptNegative = False
        Me.ctlnoref.currencyBox = False
        Me.ctlnoref.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlnoref.firstLetterCapital = False
        Me.ctlnoref.firstLettersCapital = False
        Me.ctlnoref.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlnoref.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlnoref.Location = New System.Drawing.Point(483, 37)
        Me.ctlnoref.manageText = True
        Me.ctlnoref.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlnoref.matchExp = ""
        Me.ctlnoref.maximum = 0
        Me.ctlnoref.MaxLength = 0
        Me.ctlnoref.minimum = 0
        Me.ctlnoref.Name = "ctlnoref"
        Me.ctlnoref.nbDecimals = CType(-1, Short)
        Me.ctlnoref.onlyAlphabet = False
        Me.ctlnoref.refuseAccents = False
        Me.ctlnoref.refusedChars = ""
        Me.ctlnoref.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlnoref.showInternalContextMenu = True
        Me.ctlnoref.Size = New System.Drawing.Size(64, 20)
        Me.ctlnoref.TabIndex = 203
        Me.ctlnoref.trimText = False
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(28, 158)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(186, 13)
        Me.Label13.TabIndex = 217
        Me.Label13.Text = "Date de réception de la référence :"
        '
        'ctlReceptionReferenceDate
        '
        Me.ctlReceptionReferenceDate.acceptAlpha = True
        Me.ctlReceptionReferenceDate.acceptedChars = ""
        Me.ctlReceptionReferenceDate.acceptNumeric = True
        Me.ctlReceptionReferenceDate.AcceptsReturn = True
        Me.ctlReceptionReferenceDate.allCapital = False
        Me.ctlReceptionReferenceDate.allLower = False
        Me.ctlReceptionReferenceDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ctlReceptionReferenceDate.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ctlReceptionReferenceDate.blockOnMaximum = False
        Me.ctlReceptionReferenceDate.blockOnMinimum = False
        Me.ctlReceptionReferenceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctlReceptionReferenceDate.cb_AcceptLeftZeros = False
        Me.ctlReceptionReferenceDate.cb_AcceptNegative = False
        Me.ctlReceptionReferenceDate.currencyBox = False
        Me.ctlReceptionReferenceDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ctlReceptionReferenceDate.firstLetterCapital = False
        Me.ctlReceptionReferenceDate.firstLettersCapital = False
        Me.ctlReceptionReferenceDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctlReceptionReferenceDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ctlReceptionReferenceDate.Location = New System.Drawing.Point(218, 156)
        Me.ctlReceptionReferenceDate.manageText = False
        Me.ctlReceptionReferenceDate.Margin = New System.Windows.Forms.Padding(2)
        Me.ctlReceptionReferenceDate.matchExp = ""
        Me.ctlReceptionReferenceDate.maximum = 0
        Me.ctlReceptionReferenceDate.MaxLength = 0
        Me.ctlReceptionReferenceDate.minimum = 0
        Me.ctlReceptionReferenceDate.Name = "ctlReceptionReferenceDate"
        Me.ctlReceptionReferenceDate.nbDecimals = CType(-1, Short)
        Me.ctlReceptionReferenceDate.onlyAlphabet = False
        Me.ctlReceptionReferenceDate.ReadOnly = True
        Me.ctlReceptionReferenceDate.refuseAccents = False
        Me.ctlReceptionReferenceDate.refusedChars = ""
        Me.ctlReceptionReferenceDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ctlReceptionReferenceDate.showInternalContextMenu = False
        Me.ctlReceptionReferenceDate.Size = New System.Drawing.Size(59, 20)
        Me.ctlReceptionReferenceDate.TabIndex = 218
        Me.ctlReceptionReferenceDate.trimText = False
        '
        'FolderInfos
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.ctlnoref)
        Me.Controls.Add(Me.selectcode)
        Me.Controls.Add(Me.ctlReceptionReferenceDate)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.ctlDateReference)
        Me.Controls.Add(Me.ctlRemarques)
        Me.Controls.Add(Me.ctlDateRechute)
        Me.Controls.Add(Me.label32)
        Me.Controls.Add(Me.ctlService)
        Me.Controls.Add(Me.ctlTRPToTransfer)
        Me.Controls.Add(Me.ctlTRPdemande)
        Me.Controls.Add(Me.ctlregion_Renamed)
        Me.Controls.Add(Me.ctlDateAccident)
        Me.Controls.Add(Me.ctlFrequence)
        Me.Controls.Add(Me.label36)
        Me.Controls.Add(Me.ctlDuree)
        Me.Controls.Add(Me.label35)
        Me.Controls.Add(Me.ctlmedecin)
        Me.Controls.Add(Me.label34)
        Me.Controls.Add(Me.label30)
        Me.Controls.Add(Me.selectmedecin)
        Me.Controls.Add(Me._Label1_19)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label12)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label11)
        Me.Controls.Add(Me.label10)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.ctlNoPermis)
        Me.Controls.Add(Me.ctlNoRefMedecin)
        Me.Controls.Add(Me.ctlDateAppel)
        Me.Controls.Add(Me.ctlNbPresences)
        Me.Controls.Add(Me.ctlNbAbsences)
        Me.Controls.Add(Me.ctlDateFin)
        Me.Controls.Add(Me.ctlDateDebut)
        Me.Controls.Add(Me.ctlDateEval)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me._Label1_22)
        Me.Controls.Add(Me._Label1_23)
        Me.Controls.Add(Me._Label1_24)
        Me.Controls.Add(Me._Label1_25)
        Me.Controls.Add(Me._Label1_26)
        Me.Controls.Add(Me.ctlDiagnostic)
        Me.Controls.Add(Me.ctltherapeute)
        Me.Controls.Add(Me.ctlcodedossier)
        Me.Controls.Add(Me.selectTRP)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(482, 426)
        Me.Name = "FolderInfos"
        Me.Size = New System.Drawing.Size(550, 426)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents ctlregion_Renamed As ManagedCombo
    Public WithEvents ctlDateReference As ManagedText
    Public WithEvents ctlDateRechute As ManagedText
    Public WithEvents ctlDateAccident As ManagedText
    Public WithEvents ctlFrequence As ManagedCombo
    Public WithEvents label36 As System.Windows.Forms.Label
    Public WithEvents ctlDuree As ManagedCombo
    Public WithEvents label35 As System.Windows.Forms.Label
    Public WithEvents ctlmedecin As System.Windows.Forms.TextBox
    Public WithEvents label34 As System.Windows.Forms.Label
    Public WithEvents label32 As System.Windows.Forms.Label
    Public WithEvents label30 As System.Windows.Forms.Label
    Public WithEvents selectmedecin As System.Windows.Forms.Button
    Public WithEvents _Label1_19 As System.Windows.Forms.Label
    Public WithEvents _Label1_22 As System.Windows.Forms.Label
    Public WithEvents _Label1_23 As System.Windows.Forms.Label
    Public WithEvents _Label1_24 As System.Windows.Forms.Label
    Public WithEvents _Label1_25 As System.Windows.Forms.Label
    Public WithEvents _Label1_26 As System.Windows.Forms.Label
    Public WithEvents selectcode As System.Windows.Forms.Button
    Public WithEvents ctlDiagnostic As ManagedText
    Public WithEvents ctltherapeute As System.Windows.Forms.TextBox
    Public WithEvents ctlcodedossier As System.Windows.Forms.TextBox
    Public WithEvents ctlnoref As ManagedText
    Public WithEvents selectTRP As System.Windows.Forms.Button
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents label3 As System.Windows.Forms.Label
    Public WithEvents label4 As System.Windows.Forms.Label
    Public WithEvents label5 As System.Windows.Forms.Label
    Public WithEvents label6 As System.Windows.Forms.Label
    Public WithEvents label7 As System.Windows.Forms.Label
    Public WithEvents label8 As System.Windows.Forms.Label
    Public WithEvents label9 As System.Windows.Forms.Label
    Public WithEvents label10 As System.Windows.Forms.Label
    Public WithEvents label11 As System.Windows.Forms.Label
    Public WithEvents ctlDateEval As System.Windows.Forms.Label
    Public WithEvents ctlDateDebut As System.Windows.Forms.Label
    Public WithEvents ctlDateFin As System.Windows.Forms.Label
    Public WithEvents ctlTRPdemande As ManagedCombo
    Public WithEvents ctlNbAbsences As System.Windows.Forms.Label
    Public WithEvents ctlNbPresences As System.Windows.Forms.Label
    Public WithEvents ctlDateAppel As System.Windows.Forms.Label
    Public WithEvents ctlNoRefMedecin As System.Windows.Forms.Label
    Public WithEvents ctlNoPermis As System.Windows.Forms.Label
    Friend WithEvents ctlRemarques As System.Windows.Forms.TextBox
    Public WithEvents ctlService As ManagedCombo
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents label12 As System.Windows.Forms.Label
    Public WithEvents ctlTRPToTransfer As ManagedCombo
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents ctlReceptionReferenceDate As ManagedText

End Class
