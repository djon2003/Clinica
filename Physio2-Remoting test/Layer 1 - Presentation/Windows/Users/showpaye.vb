Option Strict Off
Option Explicit On
Friend Class showpaye
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

        'Chargement des images
        selectSemaine.Image = DrawingManager.getInstance.getImage("selection16.gif")
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
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents _Label1_6 As System.Windows.Forms.Label
    Public WithEvents frame2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GenerateByHours As System.Windows.Forms.RadioButton
    Friend WithEvents GenerateByVisites As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Calculer As System.Windows.Forms.Button
    Friend WithEvents MeHe As ManagedText
    Friend WithEvents LuHe As ManagedText
    Friend WithEvents DiHe As ManagedText
    Friend WithEvents MaHe As ManagedText
    Friend WithEvents JeHe As ManagedText
    Friend WithEvents VeHe As ManagedText
    Friend WithEvents SaHe As ManagedText
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Generer As System.Windows.Forms.Button
    Friend WithEvents selectSemaine As System.Windows.Forms.Button
    Friend WithEvents Semaine As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PourcentVisite As ManagedText
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents SalaireHoraire As ManagedText
    Friend WithEvents DiVi As ManagedText
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents LuVi As ManagedText
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents MaVi As ManagedText
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents MeVi As ManagedText
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents JeVi As ManagedText
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents VeVi As ManagedText
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents SaVi As ManagedText
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents SaJo As ManagedText
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents VeJo As ManagedText
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents JeJo As ManagedText
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents MaJo As ManagedText
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents MeJo As ManagedText
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents LuJo As ManagedText
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents DiJo As ManagedText
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents dsHistoPaies As System.Data.DataSet
    Friend WithEvents Historique As System.Data.DataTable
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
    Friend WithEvents Totaux As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents HistoPaies As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.frame2 = New System.Windows.Forms.GroupBox
        Me.Totaux = New System.Windows.Forms.Label
        Me.HistoPaies = New System.Windows.Forms.DataGrid
        Me._Label1_6 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Generer = New System.Windows.Forms.Button
        Me.selectSemaine = New System.Windows.Forms.Button
        Me.Calculer = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.DiVi = New ManagedText
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.LuHe = New ManagedText
        Me.DiHe = New ManagedText
        Me.JeHe = New ManagedText
        Me.VeHe = New ManagedText
        Me.SaHe = New ManagedText
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.LuVi = New ManagedText
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.MaVi = New ManagedText
        Me.Label16 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.MeVi = New ManagedText
        Me.Label17 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.JeVi = New ManagedText
        Me.Label18 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.VeVi = New ManagedText
        Me.Label19 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.SaVi = New ManagedText
        Me.Label20 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.SaJo = New ManagedText
        Me.Label21 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.VeJo = New ManagedText
        Me.Label22 = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.JeJo = New ManagedText
        Me.Label23 = New System.Windows.Forms.Label
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.MaJo = New ManagedText
        Me.Label24 = New System.Windows.Forms.Label
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.MeJo = New ManagedText
        Me.Label25 = New System.Windows.Forms.Label
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.LuJo = New ManagedText
        Me.Label26 = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.DiJo = New ManagedText
        Me.Label27 = New System.Windows.Forms.Label
        Me.MaHe = New ManagedText
        Me.MeHe = New ManagedText
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Semaine = New System.Windows.Forms.Label
        Me.GenerateByHours = New System.Windows.Forms.RadioButton
        Me.GenerateByVisites = New System.Windows.Forms.RadioButton
        Me.SalaireHoraire = New ManagedText
        Me.PourcentVisite = New ManagedText
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dsHistoPaies = New System.Data.DataSet
        Me.Historique = New System.Data.DataTable
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
        Me.frame2.SuspendLayout()
        CType(Me.HistoPaies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        CType(Me.dsHistoPaies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Historique, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame2
        '
        Me.frame2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.frame2.BackColor = System.Drawing.Color.Transparent
        Me.frame2.Controls.Add(Me.Totaux)
        Me.frame2.Controls.Add(Me.HistoPaies)
        Me.frame2.Controls.Add(Me._Label1_6)
        Me.frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame2.Location = New System.Drawing.Point(0, 250)
        Me.frame2.MinimumSize = New System.Drawing.Size(236, 127)
        Me.frame2.Name = "Frame2"
        Me.frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame2.Size = New System.Drawing.Size(597, 205)
        Me.frame2.TabIndex = 1
        Me.frame2.TabStop = False
        Me.frame2.Text = "Historique des paies"
        '
        'Totaux
        '
        Me.Totaux.AutoSize = True
        Me.Totaux.Location = New System.Drawing.Point(64, 187)
        Me.Totaux.Name = "Totaux"
        Me.Totaux.Size = New System.Drawing.Size(0, 14)
        Me.Totaux.TabIndex = 30
        '
        'HistoPaies
        '
        Me.HistoPaies.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HistoPaies.CaptionVisible = False
        Me.HistoPaies.DataMember = ""
        Me.HistoPaies.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.HistoPaies.Location = New System.Drawing.Point(8, 19)
        Me.HistoPaies.Name = "HistoPaies"
        Me.HistoPaies.ReadOnly = True
        Me.HistoPaies.RowHeaderWidth = 15
        Me.HistoPaies.Size = New System.Drawing.Size(580, 168)
        Me.HistoPaies.TabIndex = 29
        '
        '_Label1_6
        '
        Me._Label1_6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me._Label1_6.AutoSize = True
        Me._Label1_6.BackColor = System.Drawing.Color.Transparent
        Me._Label1_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label1_6.Location = New System.Drawing.Point(8, 187)
        Me._Label1_6.Name = "_Label1_6"
        Me._Label1_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_6.Size = New System.Drawing.Size(50, 14)
        Me._Label1_6.TabIndex = 24
        Me._Label1_6.Text = "Totaux :"
        Me._Label1_6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Generer)
        Me.GroupBox1.Controls.Add(Me.selectSemaine)
        Me.GroupBox1.Controls.Add(Me.Calculer)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Semaine)
        Me.GroupBox1.Controls.Add(Me.GenerateByHours)
        Me.GroupBox1.Controls.Add(Me.GenerateByVisites)
        Me.GroupBox1.Controls.Add(Me.frame2)
        Me.GroupBox1.Controls.Add(Me.SalaireHoraire)
        Me.GroupBox1.Controls.Add(Me.PourcentVisite)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(597, 455)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Ajout d'une paie"
        '
        'Generer
        '
        Me.Generer.Enabled = False
        Me.Generer.Location = New System.Drawing.Point(524, 48)
        Me.Generer.Name = "Generer"
        Me.Generer.Size = New System.Drawing.Size(64, 28)
        Me.Generer.TabIndex = 4
        Me.Generer.Text = "Générer"
        Me.ToolTip1.SetToolTip(Me.Generer, "Générer la paie de la semaine sélectionnée")
        Me.Generer.UseVisualStyleBackColor = True
        '
        'selectSemaine
        '
        Me.selectSemaine.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectSemaine.Location = New System.Drawing.Point(6, 45)
        Me.selectSemaine.Name = "selectSemaine"
        Me.selectSemaine.Size = New System.Drawing.Size(24, 24)
        Me.selectSemaine.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.selectSemaine, "Sélectionner la semaine")
        Me.selectSemaine.UseVisualStyleBackColor = True
        '
        'Calculer
        '
        Me.Calculer.Enabled = False
        Me.Calculer.Location = New System.Drawing.Point(524, 14)
        Me.Calculer.Name = "Calculer"
        Me.Calculer.Size = New System.Drawing.Size(64, 28)
        Me.Calculer.TabIndex = 4
        Me.Calculer.Text = "Calculer"
        Me.ToolTip1.SetToolTip(Me.Calculer, "Calculer les heures et les montants pour la semaine sélectionnée")
        Me.Calculer.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LuHe, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DiHe, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.JeHe, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.VeHe, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.SaHe, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel3, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 5, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel6, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel7, 7, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel8, 7, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel9, 6, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel10, 5, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel11, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel12, 4, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel13, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel14, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.MaHe, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MeHe, 4, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(11, 82)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(577, 162)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.Controls.Add(Me.DiVi)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Location = New System.Drawing.Point(77, 76)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(62, 32)
        Me.Panel1.TabIndex = 5
        '
        'DiVi
        '
        Me.DiVi.acceptAlpha = False
        Me.DiVi.acceptedChars = ",§."
        Me.DiVi.acceptNumeric = True
        Me.DiVi.allCapital = False
        Me.DiVi.allLower = False
        Me.DiVi.BackColor = System.Drawing.SystemColors.ControlLight
        Me.DiVi.cb_AcceptNegative = False
        Me.DiVi.currencyBox = True
        Me.DiVi.Enabled = False
        Me.DiVi.firstLetterCapital = False
        Me.DiVi.firstLettersCapital = False
        Me.DiVi.Location = New System.Drawing.Point(11, 6)
        Me.DiVi.matchExp = ""
        Me.DiVi.Name = "DiVi"
        Me.DiVi.nbDecimals = CType(2, Short)
        Me.DiVi.onlyAlphabet = False
        Me.DiVi.ReadOnly = True
        Me.DiVi.refuseAccents = False
        Me.DiVi.refusedChars = ""
        Me.DiVi.Size = New System.Drawing.Size(39, 20)
        Me.DiVi.TabIndex = 1
        Me.DiVi.Text = "0"
        Me.DiVi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.DiVi.trimText = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(50, 10)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(13, 14)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "$"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(231, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Mardi"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(160, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Lundi"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(77, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Dimanche"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(292, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 14)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Mercredi"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(434, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Vendredi"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(513, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Samedi"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(373, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 14)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Jeudi"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 28)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Heures de travail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 42)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Montant total des visites"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 116)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 42)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Montant fixe par jour"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LuHe
        '
        Me.LuHe.acceptAlpha = False
        Me.LuHe.acceptedChars = ",§."
        Me.LuHe.acceptNumeric = True
        Me.LuHe.allCapital = False
        Me.LuHe.allLower = False
        Me.LuHe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LuHe.BackColor = System.Drawing.SystemColors.ControlLight
        Me.LuHe.cb_AcceptNegative = False
        Me.LuHe.currencyBox = True
        Me.LuHe.firstLetterCapital = False
        Me.LuHe.firstLettersCapital = False
        Me.LuHe.Location = New System.Drawing.Point(159, 37)
        Me.LuHe.matchExp = ""
        Me.LuHe.Name = "LuHe"
        Me.LuHe.nbDecimals = CType(2, Short)
        Me.LuHe.onlyAlphabet = False
        Me.LuHe.ReadOnly = True
        Me.LuHe.refuseAccents = False
        Me.LuHe.refusedChars = ""
        Me.LuHe.Size = New System.Drawing.Size(39, 20)
        Me.LuHe.TabIndex = 1
        Me.LuHe.Text = "0"
        Me.LuHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.LuHe.trimText = False
        '
        'DiHe
        '
        Me.DiHe.acceptAlpha = False
        Me.DiHe.acceptedChars = ",§."
        Me.DiHe.acceptNumeric = True
        Me.DiHe.allCapital = False
        Me.DiHe.allLower = False
        Me.DiHe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DiHe.BackColor = System.Drawing.SystemColors.ControlLight
        Me.DiHe.cb_AcceptNegative = False
        Me.DiHe.currencyBox = True
        Me.DiHe.firstLetterCapital = False
        Me.DiHe.firstLettersCapital = False
        Me.DiHe.Location = New System.Drawing.Point(88, 37)
        Me.DiHe.matchExp = ""
        Me.DiHe.Name = "DiHe"
        Me.DiHe.nbDecimals = CType(2, Short)
        Me.DiHe.onlyAlphabet = False
        Me.DiHe.ReadOnly = True
        Me.DiHe.refuseAccents = False
        Me.DiHe.refusedChars = ""
        Me.DiHe.Size = New System.Drawing.Size(39, 20)
        Me.DiHe.TabIndex = 1
        Me.DiHe.Text = "0"
        Me.DiHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.DiHe.trimText = False
        '
        'JeHe
        '
        Me.JeHe.acceptAlpha = False
        Me.JeHe.acceptedChars = ",§."
        Me.JeHe.acceptNumeric = True
        Me.JeHe.allCapital = False
        Me.JeHe.allLower = False
        Me.JeHe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.JeHe.BackColor = System.Drawing.SystemColors.ControlLight
        Me.JeHe.cb_AcceptNegative = False
        Me.JeHe.currencyBox = True
        Me.JeHe.firstLetterCapital = False
        Me.JeHe.firstLettersCapital = False
        Me.JeHe.Location = New System.Drawing.Point(372, 37)
        Me.JeHe.matchExp = ""
        Me.JeHe.Name = "JeHe"
        Me.JeHe.nbDecimals = CType(2, Short)
        Me.JeHe.onlyAlphabet = False
        Me.JeHe.ReadOnly = True
        Me.JeHe.refuseAccents = False
        Me.JeHe.refusedChars = ""
        Me.JeHe.Size = New System.Drawing.Size(39, 20)
        Me.JeHe.TabIndex = 1
        Me.JeHe.Text = "0"
        Me.JeHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.JeHe.trimText = False
        '
        'VeHe
        '
        Me.VeHe.acceptAlpha = False
        Me.VeHe.acceptedChars = ",§."
        Me.VeHe.acceptNumeric = True
        Me.VeHe.allCapital = False
        Me.VeHe.allLower = False
        Me.VeHe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.VeHe.BackColor = System.Drawing.SystemColors.ControlLight
        Me.VeHe.cb_AcceptNegative = False
        Me.VeHe.currencyBox = True
        Me.VeHe.firstLetterCapital = False
        Me.VeHe.firstLettersCapital = False
        Me.VeHe.Location = New System.Drawing.Point(443, 37)
        Me.VeHe.matchExp = ""
        Me.VeHe.Name = "VeHe"
        Me.VeHe.nbDecimals = CType(2, Short)
        Me.VeHe.onlyAlphabet = False
        Me.VeHe.ReadOnly = True
        Me.VeHe.refuseAccents = False
        Me.VeHe.refusedChars = ""
        Me.VeHe.Size = New System.Drawing.Size(39, 20)
        Me.VeHe.TabIndex = 1
        Me.VeHe.Text = "0"
        Me.VeHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.VeHe.trimText = False
        '
        'SaHe
        '
        Me.SaHe.acceptAlpha = False
        Me.SaHe.acceptedChars = ",§."
        Me.SaHe.acceptNumeric = True
        Me.SaHe.allCapital = False
        Me.SaHe.allLower = False
        Me.SaHe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SaHe.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SaHe.cb_AcceptNegative = False
        Me.SaHe.currencyBox = True
        Me.SaHe.firstLetterCapital = False
        Me.SaHe.firstLettersCapital = False
        Me.SaHe.Location = New System.Drawing.Point(517, 37)
        Me.SaHe.matchExp = ""
        Me.SaHe.Name = "SaHe"
        Me.SaHe.nbDecimals = CType(2, Short)
        Me.SaHe.onlyAlphabet = False
        Me.SaHe.ReadOnly = True
        Me.SaHe.refuseAccents = False
        Me.SaHe.refusedChars = ""
        Me.SaHe.Size = New System.Drawing.Size(39, 20)
        Me.SaHe.TabIndex = 1
        Me.SaHe.Text = "0"
        Me.SaHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.SaHe.trimText = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel2.Controls.Add(Me.LuVi)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Location = New System.Drawing.Point(148, 76)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(62, 32)
        Me.Panel2.TabIndex = 5
        '
        'LuVi
        '
        Me.LuVi.acceptAlpha = False
        Me.LuVi.acceptedChars = ",§."
        Me.LuVi.acceptNumeric = True
        Me.LuVi.allCapital = False
        Me.LuVi.allLower = False
        Me.LuVi.BackColor = System.Drawing.SystemColors.ControlLight
        Me.LuVi.cb_AcceptNegative = False
        Me.LuVi.currencyBox = True
        Me.LuVi.Enabled = False
        Me.LuVi.firstLetterCapital = False
        Me.LuVi.firstLettersCapital = False
        Me.LuVi.Location = New System.Drawing.Point(11, 6)
        Me.LuVi.matchExp = ""
        Me.LuVi.Name = "LuVi"
        Me.LuVi.nbDecimals = CType(2, Short)
        Me.LuVi.onlyAlphabet = False
        Me.LuVi.ReadOnly = True
        Me.LuVi.refuseAccents = False
        Me.LuVi.refusedChars = ""
        Me.LuVi.Size = New System.Drawing.Size(39, 20)
        Me.LuVi.TabIndex = 1
        Me.LuVi.Text = "0"
        Me.LuVi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.LuVi.trimText = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(50, 10)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(13, 14)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "$"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel3.Controls.Add(Me.MaVi)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Location = New System.Drawing.Point(219, 76)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(62, 32)
        Me.Panel3.TabIndex = 5
        '
        'MaVi
        '
        Me.MaVi.acceptAlpha = False
        Me.MaVi.acceptedChars = ",§."
        Me.MaVi.acceptNumeric = True
        Me.MaVi.allCapital = False
        Me.MaVi.allLower = False
        Me.MaVi.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MaVi.cb_AcceptNegative = False
        Me.MaVi.currencyBox = True
        Me.MaVi.Enabled = False
        Me.MaVi.firstLetterCapital = False
        Me.MaVi.firstLettersCapital = False
        Me.MaVi.Location = New System.Drawing.Point(11, 6)
        Me.MaVi.matchExp = ""
        Me.MaVi.Name = "MaVi"
        Me.MaVi.nbDecimals = CType(2, Short)
        Me.MaVi.onlyAlphabet = False
        Me.MaVi.ReadOnly = True
        Me.MaVi.refuseAccents = False
        Me.MaVi.refusedChars = ""
        Me.MaVi.Size = New System.Drawing.Size(39, 20)
        Me.MaVi.TabIndex = 1
        Me.MaVi.Text = "0"
        Me.MaVi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MaVi.trimText = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(50, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(13, 14)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "$"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel4.Controls.Add(Me.MeVi)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Location = New System.Drawing.Point(290, 76)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(62, 32)
        Me.Panel4.TabIndex = 5
        '
        'MeVi
        '
        Me.MeVi.acceptAlpha = False
        Me.MeVi.acceptedChars = ",§."
        Me.MeVi.acceptNumeric = True
        Me.MeVi.allCapital = False
        Me.MeVi.allLower = False
        Me.MeVi.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MeVi.cb_AcceptNegative = False
        Me.MeVi.currencyBox = True
        Me.MeVi.Enabled = False
        Me.MeVi.firstLetterCapital = False
        Me.MeVi.firstLettersCapital = False
        Me.MeVi.Location = New System.Drawing.Point(11, 6)
        Me.MeVi.matchExp = ""
        Me.MeVi.Name = "MeVi"
        Me.MeVi.nbDecimals = CType(2, Short)
        Me.MeVi.onlyAlphabet = False
        Me.MeVi.ReadOnly = True
        Me.MeVi.refuseAccents = False
        Me.MeVi.refusedChars = ""
        Me.MeVi.Size = New System.Drawing.Size(39, 20)
        Me.MeVi.TabIndex = 1
        Me.MeVi.Text = "0"
        Me.MeVi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MeVi.trimText = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(50, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(13, 14)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "$"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel5.Controls.Add(Me.JeVi)
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Location = New System.Drawing.Point(361, 76)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(62, 32)
        Me.Panel5.TabIndex = 5
        '
        'JeVi
        '
        Me.JeVi.acceptAlpha = False
        Me.JeVi.acceptedChars = ",§."
        Me.JeVi.acceptNumeric = True
        Me.JeVi.allCapital = False
        Me.JeVi.allLower = False
        Me.JeVi.BackColor = System.Drawing.SystemColors.ControlLight
        Me.JeVi.cb_AcceptNegative = False
        Me.JeVi.currencyBox = True
        Me.JeVi.Enabled = False
        Me.JeVi.firstLetterCapital = False
        Me.JeVi.firstLettersCapital = False
        Me.JeVi.Location = New System.Drawing.Point(11, 6)
        Me.JeVi.matchExp = ""
        Me.JeVi.Name = "JeVi"
        Me.JeVi.nbDecimals = CType(2, Short)
        Me.JeVi.onlyAlphabet = False
        Me.JeVi.ReadOnly = True
        Me.JeVi.refuseAccents = False
        Me.JeVi.refusedChars = ""
        Me.JeVi.Size = New System.Drawing.Size(39, 20)
        Me.JeVi.TabIndex = 1
        Me.JeVi.Text = "0"
        Me.JeVi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.JeVi.trimText = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(50, 10)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(13, 14)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "$"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel6.Controls.Add(Me.VeVi)
        Me.Panel6.Controls.Add(Me.Label19)
        Me.Panel6.Location = New System.Drawing.Point(432, 76)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(62, 32)
        Me.Panel6.TabIndex = 5
        '
        'VeVi
        '
        Me.VeVi.acceptAlpha = False
        Me.VeVi.acceptedChars = ",§."
        Me.VeVi.acceptNumeric = True
        Me.VeVi.allCapital = False
        Me.VeVi.allLower = False
        Me.VeVi.BackColor = System.Drawing.SystemColors.ControlLight
        Me.VeVi.cb_AcceptNegative = False
        Me.VeVi.currencyBox = True
        Me.VeVi.Enabled = False
        Me.VeVi.firstLetterCapital = False
        Me.VeVi.firstLettersCapital = False
        Me.VeVi.Location = New System.Drawing.Point(11, 6)
        Me.VeVi.matchExp = ""
        Me.VeVi.Name = "VeVi"
        Me.VeVi.nbDecimals = CType(2, Short)
        Me.VeVi.onlyAlphabet = False
        Me.VeVi.ReadOnly = True
        Me.VeVi.refuseAccents = False
        Me.VeVi.refusedChars = ""
        Me.VeVi.Size = New System.Drawing.Size(39, 20)
        Me.VeVi.TabIndex = 1
        Me.VeVi.Text = "0"
        Me.VeVi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.VeVi.trimText = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(50, 10)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(13, 14)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "$"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel7
        '
        Me.Panel7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel7.Controls.Add(Me.SaVi)
        Me.Panel7.Controls.Add(Me.Label20)
        Me.Panel7.Location = New System.Drawing.Point(506, 76)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(62, 32)
        Me.Panel7.TabIndex = 5
        '
        'SaVi
        '
        Me.SaVi.acceptAlpha = False
        Me.SaVi.acceptedChars = ",§."
        Me.SaVi.acceptNumeric = True
        Me.SaVi.allCapital = False
        Me.SaVi.allLower = False
        Me.SaVi.BackColor = System.Drawing.SystemColors.ControlLight
        Me.SaVi.cb_AcceptNegative = False
        Me.SaVi.currencyBox = True
        Me.SaVi.Enabled = False
        Me.SaVi.firstLetterCapital = False
        Me.SaVi.firstLettersCapital = False
        Me.SaVi.Location = New System.Drawing.Point(11, 6)
        Me.SaVi.matchExp = ""
        Me.SaVi.Name = "SaVi"
        Me.SaVi.nbDecimals = CType(2, Short)
        Me.SaVi.onlyAlphabet = False
        Me.SaVi.ReadOnly = True
        Me.SaVi.refuseAccents = False
        Me.SaVi.refusedChars = ""
        Me.SaVi.Size = New System.Drawing.Size(39, 20)
        Me.SaVi.TabIndex = 1
        Me.SaVi.Text = "0"
        Me.SaVi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.SaVi.trimText = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(50, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(13, 14)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "$"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel8
        '
        Me.Panel8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel8.Controls.Add(Me.SaJo)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Location = New System.Drawing.Point(506, 121)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(62, 32)
        Me.Panel8.TabIndex = 5
        '
        'SaJo
        '
        Me.SaJo.acceptAlpha = False
        Me.SaJo.acceptedChars = ",§."
        Me.SaJo.acceptNumeric = True
        Me.SaJo.allCapital = False
        Me.SaJo.allLower = False
        Me.SaJo.cb_AcceptNegative = False
        Me.SaJo.currencyBox = True
        Me.SaJo.firstLetterCapital = False
        Me.SaJo.firstLettersCapital = False
        Me.SaJo.Location = New System.Drawing.Point(11, 6)
        Me.SaJo.matchExp = ""
        Me.SaJo.Name = "SaJo"
        Me.SaJo.nbDecimals = CType(2, Short)
        Me.SaJo.onlyAlphabet = False
        Me.SaJo.refuseAccents = False
        Me.SaJo.refusedChars = ""
        Me.SaJo.Size = New System.Drawing.Size(39, 20)
        Me.SaJo.TabIndex = 1
        Me.SaJo.Text = "0"
        Me.SaJo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.SaJo.trimText = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(50, 10)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(13, 14)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "$"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel9
        '
        Me.Panel9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel9.Controls.Add(Me.VeJo)
        Me.Panel9.Controls.Add(Me.Label22)
        Me.Panel9.Location = New System.Drawing.Point(432, 121)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(62, 32)
        Me.Panel9.TabIndex = 5
        '
        'VeJo
        '
        Me.VeJo.acceptAlpha = False
        Me.VeJo.acceptedChars = ",§."
        Me.VeJo.acceptNumeric = True
        Me.VeJo.allCapital = False
        Me.VeJo.allLower = False
        Me.VeJo.cb_AcceptNegative = False
        Me.VeJo.currencyBox = True
        Me.VeJo.firstLetterCapital = False
        Me.VeJo.firstLettersCapital = False
        Me.VeJo.Location = New System.Drawing.Point(11, 6)
        Me.VeJo.matchExp = ""
        Me.VeJo.Name = "VeJo"
        Me.VeJo.nbDecimals = CType(2, Short)
        Me.VeJo.onlyAlphabet = False
        Me.VeJo.refuseAccents = False
        Me.VeJo.refusedChars = ""
        Me.VeJo.Size = New System.Drawing.Size(39, 20)
        Me.VeJo.TabIndex = 1
        Me.VeJo.Text = "0"
        Me.VeJo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.VeJo.trimText = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(50, 10)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(13, 14)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "$"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel10
        '
        Me.Panel10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel10.Controls.Add(Me.JeJo)
        Me.Panel10.Controls.Add(Me.Label23)
        Me.Panel10.Location = New System.Drawing.Point(361, 121)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(62, 32)
        Me.Panel10.TabIndex = 5
        '
        'JeJo
        '
        Me.JeJo.acceptAlpha = False
        Me.JeJo.acceptedChars = ",§."
        Me.JeJo.acceptNumeric = True
        Me.JeJo.allCapital = False
        Me.JeJo.allLower = False
        Me.JeJo.cb_AcceptNegative = False
        Me.JeJo.currencyBox = True
        Me.JeJo.firstLetterCapital = False
        Me.JeJo.firstLettersCapital = False
        Me.JeJo.Location = New System.Drawing.Point(11, 6)
        Me.JeJo.matchExp = ""
        Me.JeJo.Name = "JeJo"
        Me.JeJo.nbDecimals = CType(2, Short)
        Me.JeJo.onlyAlphabet = False
        Me.JeJo.refuseAccents = False
        Me.JeJo.refusedChars = ""
        Me.JeJo.Size = New System.Drawing.Size(39, 20)
        Me.JeJo.TabIndex = 1
        Me.JeJo.Text = "0"
        Me.JeJo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.JeJo.trimText = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(50, 10)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(13, 14)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "$"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel11
        '
        Me.Panel11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel11.Controls.Add(Me.MaJo)
        Me.Panel11.Controls.Add(Me.Label24)
        Me.Panel11.Location = New System.Drawing.Point(219, 121)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(62, 32)
        Me.Panel11.TabIndex = 5
        '
        'MaJo
        '
        Me.MaJo.acceptAlpha = False
        Me.MaJo.acceptedChars = ",§."
        Me.MaJo.acceptNumeric = True
        Me.MaJo.allCapital = False
        Me.MaJo.allLower = False
        Me.MaJo.cb_AcceptNegative = False
        Me.MaJo.currencyBox = True
        Me.MaJo.firstLetterCapital = False
        Me.MaJo.firstLettersCapital = False
        Me.MaJo.Location = New System.Drawing.Point(11, 6)
        Me.MaJo.matchExp = ""
        Me.MaJo.Name = "MaJo"
        Me.MaJo.nbDecimals = CType(2, Short)
        Me.MaJo.onlyAlphabet = False
        Me.MaJo.refuseAccents = False
        Me.MaJo.refusedChars = ""
        Me.MaJo.Size = New System.Drawing.Size(39, 20)
        Me.MaJo.TabIndex = 1
        Me.MaJo.Text = "0"
        Me.MaJo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MaJo.trimText = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(50, 10)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(13, 14)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "$"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel12
        '
        Me.Panel12.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel12.Controls.Add(Me.MeJo)
        Me.Panel12.Controls.Add(Me.Label25)
        Me.Panel12.Location = New System.Drawing.Point(290, 121)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(62, 32)
        Me.Panel12.TabIndex = 5
        '
        'MeJo
        '
        Me.MeJo.acceptAlpha = False
        Me.MeJo.acceptedChars = ",§."
        Me.MeJo.acceptNumeric = True
        Me.MeJo.allCapital = False
        Me.MeJo.allLower = False
        Me.MeJo.cb_AcceptNegative = False
        Me.MeJo.currencyBox = True
        Me.MeJo.firstLetterCapital = False
        Me.MeJo.firstLettersCapital = False
        Me.MeJo.Location = New System.Drawing.Point(11, 6)
        Me.MeJo.matchExp = ""
        Me.MeJo.Name = "MeJo"
        Me.MeJo.nbDecimals = CType(2, Short)
        Me.MeJo.onlyAlphabet = False
        Me.MeJo.refuseAccents = False
        Me.MeJo.refusedChars = ""
        Me.MeJo.Size = New System.Drawing.Size(39, 20)
        Me.MeJo.TabIndex = 1
        Me.MeJo.Text = "0"
        Me.MeJo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MeJo.trimText = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(50, 10)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(13, 14)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "$"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel13
        '
        Me.Panel13.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel13.Controls.Add(Me.LuJo)
        Me.Panel13.Controls.Add(Me.Label26)
        Me.Panel13.Location = New System.Drawing.Point(148, 121)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(62, 32)
        Me.Panel13.TabIndex = 5
        '
        'LuJo
        '
        Me.LuJo.acceptAlpha = False
        Me.LuJo.acceptedChars = ",§."
        Me.LuJo.acceptNumeric = True
        Me.LuJo.allCapital = False
        Me.LuJo.allLower = False
        Me.LuJo.cb_AcceptNegative = False
        Me.LuJo.currencyBox = True
        Me.LuJo.firstLetterCapital = False
        Me.LuJo.firstLettersCapital = False
        Me.LuJo.Location = New System.Drawing.Point(11, 6)
        Me.LuJo.matchExp = ""
        Me.LuJo.Name = "LuJo"
        Me.LuJo.nbDecimals = CType(2, Short)
        Me.LuJo.onlyAlphabet = False
        Me.LuJo.refuseAccents = False
        Me.LuJo.refusedChars = ""
        Me.LuJo.Size = New System.Drawing.Size(39, 20)
        Me.LuJo.TabIndex = 1
        Me.LuJo.Text = "0"
        Me.LuJo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.LuJo.trimText = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(50, 10)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(13, 14)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "$"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel14
        '
        Me.Panel14.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel14.Controls.Add(Me.DiJo)
        Me.Panel14.Controls.Add(Me.Label27)
        Me.Panel14.Location = New System.Drawing.Point(77, 121)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(62, 32)
        Me.Panel14.TabIndex = 5
        '
        'DiJo
        '
        Me.DiJo.acceptAlpha = False
        Me.DiJo.acceptedChars = ",§."
        Me.DiJo.acceptNumeric = True
        Me.DiJo.allCapital = False
        Me.DiJo.allLower = False
        Me.DiJo.cb_AcceptNegative = False
        Me.DiJo.currencyBox = True
        Me.DiJo.firstLetterCapital = False
        Me.DiJo.firstLettersCapital = False
        Me.DiJo.Location = New System.Drawing.Point(11, 6)
        Me.DiJo.matchExp = ""
        Me.DiJo.Name = "DiJo"
        Me.DiJo.nbDecimals = CType(2, Short)
        Me.DiJo.onlyAlphabet = False
        Me.DiJo.refuseAccents = False
        Me.DiJo.refusedChars = ""
        Me.DiJo.Size = New System.Drawing.Size(39, 20)
        Me.DiJo.TabIndex = 1
        Me.DiJo.Text = "0"
        Me.DiJo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.DiJo.trimText = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(50, 10)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(13, 14)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "$"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MaHe
        '
        Me.MaHe.acceptAlpha = False
        Me.MaHe.acceptedChars = ",§."
        Me.MaHe.acceptNumeric = True
        Me.MaHe.allCapital = False
        Me.MaHe.allLower = False
        Me.MaHe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.MaHe.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MaHe.cb_AcceptNegative = False
        Me.MaHe.currencyBox = True
        Me.MaHe.firstLetterCapital = False
        Me.MaHe.firstLettersCapital = False
        Me.MaHe.Location = New System.Drawing.Point(230, 37)
        Me.MaHe.matchExp = ""
        Me.MaHe.Name = "MaHe"
        Me.MaHe.nbDecimals = CType(2, Short)
        Me.MaHe.onlyAlphabet = False
        Me.MaHe.ReadOnly = True
        Me.MaHe.refuseAccents = False
        Me.MaHe.refusedChars = ""
        Me.MaHe.Size = New System.Drawing.Size(39, 20)
        Me.MaHe.TabIndex = 1
        Me.MaHe.Text = "0"
        Me.MaHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MaHe.trimText = False
        '
        'MeHe
        '
        Me.MeHe.acceptAlpha = False
        Me.MeHe.acceptedChars = ",§."
        Me.MeHe.acceptNumeric = True
        Me.MeHe.allCapital = False
        Me.MeHe.allLower = False
        Me.MeHe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.MeHe.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MeHe.cb_AcceptNegative = False
        Me.MeHe.currencyBox = True
        Me.MeHe.firstLetterCapital = False
        Me.MeHe.firstLettersCapital = False
        Me.MeHe.Location = New System.Drawing.Point(301, 37)
        Me.MeHe.matchExp = ""
        Me.MeHe.Name = "MeHe"
        Me.MeHe.nbDecimals = CType(2, Short)
        Me.MeHe.onlyAlphabet = False
        Me.MeHe.ReadOnly = True
        Me.MeHe.refuseAccents = False
        Me.MeHe.refusedChars = ""
        Me.MeHe.Size = New System.Drawing.Size(39, 20)
        Me.MeHe.TabIndex = 1
        Me.MeHe.Text = "0"
        Me.MeHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MeHe.trimText = False
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(384, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 14)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "% des visites :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(346, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(13, 14)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "$"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(215, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 14)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Salaire horaire :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Semaine
        '
        Me.Semaine.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Semaine.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Semaine.Location = New System.Drawing.Point(36, 48)
        Me.Semaine.Name = "Semaine"
        Me.Semaine.Size = New System.Drawing.Size(155, 19)
        Me.Semaine.TabIndex = 0
        Me.Semaine.Text = "Aucune semaine sélectionnée"
        Me.Semaine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GenerateByHours
        '
        Me.GenerateByHours.AutoSize = True
        Me.GenerateByHours.Checked = True
        Me.GenerateByHours.Location = New System.Drawing.Point(11, 19)
        Me.GenerateByHours.Name = "GenerateByHours"
        Me.GenerateByHours.Size = New System.Drawing.Size(243, 18)
        Me.GenerateByHours.TabIndex = 2
        Me.GenerateByHours.TabStop = True
        Me.GenerateByHours.Text = "Paie généré par le nombre d'heures de travail"
        Me.GenerateByHours.UseVisualStyleBackColor = True
        '
        'GenerateByVisites
        '
        Me.GenerateByVisites.AutoSize = True
        Me.GenerateByVisites.Location = New System.Drawing.Point(312, 19)
        Me.GenerateByVisites.Name = "GenerateByVisites"
        Me.GenerateByVisites.Size = New System.Drawing.Size(200, 18)
        Me.GenerateByVisites.TabIndex = 2
        Me.GenerateByVisites.Text = "Paie généré par le nombre de visites"
        Me.GenerateByVisites.UseVisualStyleBackColor = True
        '
        'SalaireHoraire
        '
        Me.SalaireHoraire.acceptAlpha = False
        Me.SalaireHoraire.acceptedChars = ",§."
        Me.SalaireHoraire.acceptNumeric = True
        Me.SalaireHoraire.allCapital = False
        Me.SalaireHoraire.allLower = False
        Me.SalaireHoraire.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SalaireHoraire.cb_AcceptNegative = False
        Me.SalaireHoraire.currencyBox = True
        Me.SalaireHoraire.firstLetterCapital = False
        Me.SalaireHoraire.firstLettersCapital = False
        Me.SalaireHoraire.Location = New System.Drawing.Point(304, 46)
        Me.SalaireHoraire.matchExp = ""
        Me.SalaireHoraire.Name = "SalaireHoraire"
        Me.SalaireHoraire.nbDecimals = CType(2, Short)
        Me.SalaireHoraire.onlyAlphabet = False
        Me.SalaireHoraire.refuseAccents = False
        Me.SalaireHoraire.refusedChars = ""
        Me.SalaireHoraire.Size = New System.Drawing.Size(39, 20)
        Me.SalaireHoraire.TabIndex = 1
        Me.SalaireHoraire.Text = "0"
        Me.SalaireHoraire.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.SalaireHoraire.trimText = False
        '
        'PourcentVisite
        '
        Me.PourcentVisite.acceptAlpha = False
        Me.PourcentVisite.acceptedChars = ",§."
        Me.PourcentVisite.acceptNumeric = True
        Me.PourcentVisite.allCapital = False
        Me.PourcentVisite.allLower = False
        Me.PourcentVisite.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PourcentVisite.cb_AcceptNegative = False
        Me.PourcentVisite.currencyBox = True
        Me.PourcentVisite.Enabled = False
        Me.PourcentVisite.firstLetterCapital = False
        Me.PourcentVisite.firstLettersCapital = False
        Me.PourcentVisite.Location = New System.Drawing.Point(468, 46)
        Me.PourcentVisite.matchExp = ""
        Me.PourcentVisite.Name = "PourcentVisite"
        Me.PourcentVisite.nbDecimals = CType(0, Short)
        Me.PourcentVisite.onlyAlphabet = False
        Me.PourcentVisite.refuseAccents = False
        Me.PourcentVisite.refusedChars = ""
        Me.PourcentVisite.Size = New System.Drawing.Size(39, 20)
        Me.PourcentVisite.TabIndex = 1
        Me.PourcentVisite.Text = "0"
        Me.PourcentVisite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.PourcentVisite.trimText = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'dsHistoPaies
        '
        Me.dsHistoPaies.DataSetName = "dsHistoPaies"
        Me.dsHistoPaies.Tables.AddRange(New System.Data.DataTable() {Me.Historique})
        '
        'Historique
        '
        Me.Historique.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4, Me.DataColumn5, Me.DataColumn6, Me.DataColumn7, Me.DataColumn8, Me.DataColumn9, Me.DataColumn10})
        Me.Historique.TableName = "Historique"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "Date"
        Me.DataColumn1.DataType = GetType(Date)
        '
        'DataColumn2
        '
        Me.DataColumn2.ColumnName = "Type"
        '
        'DataColumn3
        '
        Me.DataColumn3.Caption = "Montant"
        Me.DataColumn3.ColumnName = "Montant"
        Me.DataColumn3.DataType = GetType(System.Data.SqlTypes.SqlMoney)
        Me.DataColumn3.ReadOnly = True
        '
        'DataColumn4
        '
        Me.DataColumn4.ColumnName = "Dimanche"
        Me.DataColumn4.DefaultValue = "0"
        '
        'DataColumn5
        '
        Me.DataColumn5.ColumnName = "Lundi"
        Me.DataColumn5.DefaultValue = "0"
        '
        'DataColumn6
        '
        Me.DataColumn6.ColumnName = "Mardi"
        Me.DataColumn6.DefaultValue = "0"
        '
        'DataColumn7
        '
        Me.DataColumn7.ColumnName = "Mercredi"
        Me.DataColumn7.DefaultValue = "0"
        '
        'DataColumn8
        '
        Me.DataColumn8.ColumnName = "Jeudi"
        Me.DataColumn8.DefaultValue = "0"
        '
        'DataColumn9
        '
        Me.DataColumn9.ColumnName = "Vendredi"
        Me.DataColumn9.DefaultValue = "0"
        '
        'DataColumn10
        '
        Me.DataColumn10.ColumnName = "Samedi"
        Me.DataColumn10.DefaultValue = "0"
        '
        'showpaye
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(621, 479)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "showpaye"
        Me.ShowInTaskbar = False
        Me.Text = "Paye"
        Me.frame2.ResumeLayout(False)
        Me.frame2.PerformLayout()
        CType(Me.HistoPaies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.dsHistoPaies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Historique, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private oldWeek As String = ""
    Private curNoUser As Integer


    Public Sub loading(ByVal noUser As Integer)
        curNoUser = noUser
        Me.Text = "Gestion des paies de " & UsersManager.getInstance.getUser(noUser).toString()

        loadPaiesHistorique()
    End Sub

    Private Sub selectSemaine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectSemaine.Click
        Dim MySelDate, transitiveDate As Date

        If oldWeek <> "" Then
            MySelDate = CDate(oldWeek)
        Else
            MySelDate = Date.Today
        End If
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Today.Year + 1, , , , , , True, , , , , MySelDate, True)
        If dateReturn.Count = 0 Then Exit Sub

        transitiveDate = dateReturn(0)

        oldWeek = DateFormat.getTextDate(transitiveDate.AddDays(transitiveDate.DayOfWeek * -1))

        Semaine.Text = oldWeek & " au " & DateFormat.getTextDate(CDate(oldWeek).AddDays(6))

        Calculer.Enabled = True
        Generer.Enabled = True
    End Sub

    Private Sub calculer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Calculer.Click
        'Droit & Accès
        If currentDroitAcces(49) = False And ConnectionsManager.currentUser <> curNoUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les paies de tous les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If
        If currentDroitAcces(50) = False And ConnectionsManager.currentUser = curNoUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer vos paies." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim curHoraire As Schedule = SchedulesManager.getInstance.getSchedule(curNoUser, CDate(oldWeek))
        If curHoraire Is Nothing Then
            If MessageBox.Show("Il n'existe pas d'horaire par défaut pour cet utilisateur." & vbCrLf & "Voulez-vous en créer une ?", "Aucune horaire par défaut", MessageBoxButtons.YesNo) = DialogResult.No Then
                Exit Sub
            Else
                openModifHoraire(curNoUser)
                Exit Sub
            End If
        End If

        DiHe.Text = curHoraire.totalMinutesByDay(DayOfWeek.Sunday) / 60
        LuHe.Text = curHoraire.totalMinutesByDay(DayOfWeek.Monday) / 60
        MeHe.Text = curHoraire.totalMinutesByDay(DayOfWeek.Tuesday) / 60
        MaHe.Text = curHoraire.totalMinutesByDay(DayOfWeek.Wednesday) / 60
        JeHe.Text = curHoraire.totalMinutesByDay(DayOfWeek.Thursday) / 60
        VeHe.Text = curHoraire.totalMinutesByDay(DayOfWeek.Friday) / 60
        SaHe.Text = curHoraire.totalMinutesByDay(DayOfWeek.Saturday) / 60
        If currentDroitAcces(92) Then lockItems(False, True)

        'TODO : If possible, remove all CDate
        Dim curWeekDate As Date = CDate(oldWeek)
        Dim mySQLCommon As String = "(SELECT Sum([MontantFacture]) FROM InfoVisites INNER JOIN StatFactures ON InfoVisites.NoFacture = StatFactures.NoFacture WHERE (InfoVisites.NoTRP=" & curNoUser & " AND ((InfoVisites.NoStatut)=4) AND ((InfoVisites.DateHeure)>='DAYDATE') AND ((InfoVisites.DateHeure)<'NEXTDAYDATE')))"
        Dim mySQL As String = ""

        Dim i As Byte
        For i = 0 To 5
            mySQL &= "," & mySQLCommon
            mySQL = mySQL.Replace("NEXTDAYDATE", DateFormat.getTextDate(curWeekDate.AddDays(1)))
            mySQL = mySQL.Replace("DAYDATE", DateFormat.getTextDate(curWeekDate))
            curWeekDate = curWeekDate.AddDays(1)
        Next i

        Dim myNbVisites(,) As String = DBLinker.getInstance.readDB("InfoVisites INNER JOIN StatFactures ON InfoVisites.NoFacture = StatFactures.NoFacture", "Sum([MontantFacture])" & mySQL, "WHERE (((InfoVisites.NoStatut)=4) AND ((InfoVisites.DateHeure)>='" & DateFormat.getTextDate(curWeekDate) & "') AND ((InfoVisites.DateHeure)<'" & DateFormat.getTextDate(curWeekDate.AddDays(1)) & "'))")

        If Not myNbVisites Is Nothing AndAlso myNbVisites.Length <> 0 Then
            DiVi.Text = myNbVisites(1, 0)
            LuVi.Text = myNbVisites(2, 0)
            MaVi.Text = myNbVisites(3, 0)
            MeVi.Text = myNbVisites(4, 0)
            JeVi.Text = myNbVisites(5, 0)
            VeVi.Text = myNbVisites(6, 0)
            SaVi.Text = myNbVisites(0, 0)
        End If
    End Sub

    Private Sub generer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Generer.Click
        'Droit & Accès
        If currentDroitAcces(49) = False And ConnectionsManager.currentUser <> curNoUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les paies de tous les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If
        If currentDroitAcces(50) = False And ConnectionsManager.currentUser = curNoUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer vos paies." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim DiNb, LuNb, MaNb, MeNb, JeNb, VeNb, SaNb, PaieType, multiplicateur As String
        If GenerateByHours.Checked = True Then
            PaieType = "Par heure"
            multiplicateur = SalaireHoraire.Text
            DiNb = DiHe.Text
            LuNb = LuHe.Text
            MaNb = MeHe.Text
            MeNb = MaHe.Text
            JeNb = JeHe.Text
            VeNb = VeHe.Text
            SaNb = SaHe.Text
        Else
            PaieType = "Par visite"
            multiplicateur = PourcentVisite.Text / 100
            DiNb = DiVi.Text
            LuNb = LuVi.Text
            MaNb = MaVi.Text
            MeNb = MeVi.Text
            JeNb = JeVi.Text
            VeNb = VeVi.Text
            SaNb = SaVi.Text
        End If

        Dim isExists() As String = DBLinker.getInstance.readOneDBField("PayesUtilisateurs", "DatePaie", "WHERE (NoUser = " & curNoUser & " AND DatePaie='" & oldWeek & "')")
        If isExists Is Nothing OrElse isExists.Length = 0 Then
            Dim fieldsStringBuilder As New System.Text.StringBuilder()
            With fieldsStringBuilder
                .Append("DiNb")
                .Append(",LuNb")
                .Append(",MaNb")
                .Append(",MeNb")
                .Append(",JeNb")
                .Append(",VeNb")
                .Append(",SaNb")
                .Append(",DiMontantFixe")
                .Append(",LuMontantFixe")
                .Append(",MaMontantFixe")
                .Append(",MeMontantFixe")
                .Append(",JeMontantFixe")
                .Append(",VeMontantFixe")
                .Append(",SaMontantFixe")
                .Append(",NoUser")
                .Append(",DatePaie")
                .Append(",Multiplicateur")
                .Append(",Type")
            End With

            Dim valueStringBuilder As New System.Text.StringBuilder()
            With valueStringBuilder
                .Append(DiNb.Replace(",", "."))
                .Append(",").Append(LuNb.Replace(",", "."))
                .Append(",").Append(MaNb.Replace(",", "."))
                .Append(",").Append(MeNb.Replace(",", "."))
                .Append(",").Append(JeNb.Replace(",", "."))
                .Append(",").Append(VeNb.Replace(",", "."))
                .Append(",").Append(SaNb.Replace(",", "."))
                .Append(",").Append(DiJo.Text.Replace(",", "."))
                .Append(",").Append(LuJo.Text.Replace(",", "."))
                .Append(",").Append(MaJo.Text.Replace(",", "."))
                .Append(",").Append(MeJo.Text.Replace(",", "."))
                .Append(",").Append(JeJo.Text.Replace(",", "."))
                .Append(",").Append(VeJo.Text.Replace(",", "."))
                .Append(",").Append(SaJo.Text.Replace(",", "."))
                .Append(",").Append(curNoUser)
                .Append(",'").Append(oldWeek).Append("'")
                .Append(",").Append(multiplicateur.Replace(",", "."))
                .Append(",'").Append(PaieType).Append("'")
            End With

            DBLinker.getInstance.writeDB("PayesUtilisateurs", fieldsStringBuilder.ToString, valueStringBuilder.ToString)
        Else
            If MessageBox.Show("Voulez-vous remplacer la paie existante par celle-ci ?", "Remplacement de paie", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then Exit Sub

            Dim setStringBuilder As New System.Text.StringBuilder()
            With setStringBuilder
                .Append("DiNb=").Append(DiNb.Replace(",", "."))
                .Append(",LuNb=").Append(LuNb.Replace(",", "."))
                .Append(",MaNb=").Append(MaNb.Replace(",", "."))
                .Append(",MeNb=").Append(MeNb.Replace(",", "."))
                .Append(",JeNb=").Append(JeNb.Replace(",", "."))
                .Append(",VeNb=").Append(VeNb.Replace(",", "."))
                .Append(",SaNb=").Append(SaNb.Replace(",", "."))
                .Append(",DiMontantFixe=").Append(DiJo.Text.Replace(",", "."))
                .Append(",LuMontantFixe=").Append(LuJo.Text.Replace(",", "."))
                .Append(",MaMontantFixe=").Append(MaJo.Text.Replace(",", "."))
                .Append(",MeMontantFixe=").Append(MeJo.Text.Replace(",", "."))
                .Append(",JeMontantFixe=").Append(JeJo.Text.Replace(",", "."))
                .Append(",VeMontantFixe=").Append(VeJo.Text.Replace(",", "."))
                .Append(",SaMontantFixe=").Append(SaJo.Text.Replace(",", "."))
                .Append(",Multiplicateur=").Append(multiplicateur.Replace(",", "."))
                .Append(",Type='").Append(PaieType).Append("'")
            End With

            DBLinker.getInstance.updateDB("PayesUtilisateurs", setStringBuilder.ToString, "NoUser", curNoUser & " AND DatePaie='" & oldWeek & "'", False)
        End If

        loadPaiesHistorique()
    End Sub

    Public Sub loadPaiesHistorique()
        dsHistoPaies.Clear()

        dsHistoPaies = DBLinker.getInstance.readDBForGrid("PayesUtilisateurs", "PayesUtilisateurs.DatePaie, PayesUtilisateurs.Type, ((PayesUtilisateurs.DiNb+PayesUtilisateurs.LuNb+PayesUtilisateurs.MaNb+PayesUtilisateurs.MeNb+PayesUtilisateurs.JeNb+PayesUtilisateurs.VeNb+PayesUtilisateurs.SaNb)*PayesUtilisateurs.Multiplicateur+(PayesUtilisateurs.DiMontantFixe)+(PayesUtilisateurs.LuMontantFixe)+(PayesUtilisateurs.MaMontantFixe)+(PayesUtilisateurs.MeMontantFixe)+(PayesUtilisateurs.JeMontantFixe)+(PayesUtilisateurs.VeMontantFixe)+(PayesUtilisateurs.SaMontantFixe)) AS Montant, PayesUtilisateurs.DiNb as Dimanche, PayesUtilisateurs.LuNb as Lundi, PayesUtilisateurs.MaNb as Mardi, PayesUtilisateurs.MeNb as Mercredi, PayesUtilisateurs.JeNb as Jeudi, PayesUtilisateurs.VeNb as Vendredi, PayesUtilisateurs.SaNb as Samedi", "WHERE NoUser=" & curNoUser, , , "Historique")
        If Not dsHistoPaies.Tables("Historique") Is Nothing Then
            HistoPaies.DataSource = dsHistoPaies.Tables("Historique").DefaultView

            setStyleToDataGrid()

            HistoPaies.DataSource = dsHistoPaies.Tables("Historique")
        End If

        calculTotaux()
    End Sub

    Private Sub activateControlsOnType(ByVal byHours As Boolean)
        DiHe.Enabled = byHours
        DiVi.Enabled = Not byHours
        LuHe.Enabled = byHours
        LuVi.Enabled = Not byHours
        MeHe.Enabled = byHours
        MaVi.Enabled = Not byHours
        MaHe.Enabled = byHours
        MeVi.Enabled = Not byHours
        JeHe.Enabled = byHours
        JeVi.Enabled = Not byHours
        VeHe.Enabled = byHours
        VeVi.Enabled = Not byHours
        SaHe.Enabled = byHours
        SaVi.Enabled = Not byHours
        PourcentVisite.Enabled = Not byHours
        SalaireHoraire.Enabled = byHours
    End Sub

    Private Sub calculTotaux()
        Dim MontantTotal, HeuresTotal, visitesTotal As Double
        MontantTotal = 0 : HeuresTotal = 0 : visitesTotal = 0
        If Not dsHistoPaies.Tables("Historique") Is Nothing Then
            Dim i As Integer

            With dsHistoPaies.Tables("Historique")
                For i = 0 To .Rows.Count - 1
                    MontantTotal += .Rows(i).Item("Montant")
                    If .Rows(i).Item("Type").ToString.EndsWith("heure") Then
                        HeuresTotal += .Rows(i).Item("Dimanche")
                        HeuresTotal += .Rows(i).Item("Lundi")
                        HeuresTotal += .Rows(i).Item("Mardi")
                        HeuresTotal += .Rows(i).Item("Mercredi")
                        HeuresTotal += .Rows(i).Item("Jeudi")
                        HeuresTotal += .Rows(i).Item("Vendredi")
                        HeuresTotal += .Rows(i).Item("Samedi")
                    Else
                        visitesTotal += .Rows(i).Item("Dimanche")
                        visitesTotal += .Rows(i).Item("Lundi")
                        visitesTotal += .Rows(i).Item("Mardi")
                        visitesTotal += .Rows(i).Item("Mercredi")
                        visitesTotal += .Rows(i).Item("Jeudi")
                        visitesTotal += .Rows(i).Item("Vendredi")
                        visitesTotal += .Rows(i).Item("Samedi")
                    End If
                Next i
            End With
        End If

        Totaux.Text = "Montants : " & MontantTotal & "$                              Heures : " & HeuresTotal & "                              Visites : " & visitesTotal & "$"
    End Sub

    Private Sub setStyleToDataGrid()

        Dim ts As New DataGridTableStyle
        ts.MappingName = "Historique"

        Dim colDate As New DataGridTextBoxColumn
        With colDate
            .MappingName = "DatePaie"
            .HeaderText = "Date"
            .Width = 61
        End With

        Dim colPar As New DataGridTextBoxColumn
        With colPar
            .MappingName = "Type"
            .HeaderText = "Type"
            .Width = 54
        End With

        Dim colMontant As New HeaderAndDataAlignColumn
        With colMontant
            .MappingName = "Montant"
            .HeaderText = "Montant"
            .Format = "c"
            .dataAlignment = HorizontalAlignment.Right
            .Width = 59
        End With

        Dim colDi As New HeaderAndDataAlignColumn
        With colDi
            .MappingName = "Dimanche"
            .HeaderText = "Dimanche"
            .Format = "f"
            .Width = 55
            .dataAlignment = HorizontalAlignment.Right
        End With

        Dim colLu As New HeaderAndDataAlignColumn
        With colLu
            .MappingName = "Lundi"
            .HeaderText = "Lundi"
            .Format = "f"
            .Width = 55
            .dataAlignment = HorizontalAlignment.Right
        End With

        Dim colMa As New HeaderAndDataAlignColumn
        With colMa
            .MappingName = "Mardi"
            .HeaderText = "Mardi"
            .Format = "f"
            .Width = 55
            .dataAlignment = HorizontalAlignment.Right
        End With

        Dim colMe As New HeaderAndDataAlignColumn
        With colMe
            .MappingName = "Mercredi"
            .HeaderText = "Mercredi"
            .Format = "f"
            .Width = 55
            .dataAlignment = HorizontalAlignment.Right
        End With

        Dim colJe As New HeaderAndDataAlignColumn
        With colJe
            .MappingName = "Jeudi"
            .HeaderText = "Jeudi"
            .Format = "f"
            .Width = 55
            .dataAlignment = HorizontalAlignment.Right
        End With

        Dim colVe As New HeaderAndDataAlignColumn
        With colVe
            .MappingName = "Vendredi"
            .HeaderText = "Vendredi"
            .Format = "f"
            .Width = 50
            .dataAlignment = HorizontalAlignment.Right
        End With

        Dim colSa As New HeaderAndDataAlignColumn
        With colSa
            .MappingName = "Samedi"
            .HeaderText = "Samedi"
            .Format = "f"
            .Width = 45
            .dataAlignment = HorizontalAlignment.Right
        End With

        ts.GridColumnStyles.Add(colDate)
        ts.GridColumnStyles.Add(colPar)
        ts.GridColumnStyles.Add(colMontant)
        ts.GridColumnStyles.Add(colDi)
        ts.GridColumnStyles.Add(colLu)
        ts.GridColumnStyles.Add(colMa)
        ts.GridColumnStyles.Add(colMe)
        ts.GridColumnStyles.Add(colJe)
        ts.GridColumnStyles.Add(colVe)
        ts.GridColumnStyles.Add(colSa)

        HistoPaies.TableStyles.Clear()
        HistoPaies.TableStyles.Add(ts)
        ts = Nothing
        colDate = Nothing
        colPar = Nothing
        colMontant = Nothing
        colDi = Nothing
        colLu = Nothing
        colMa = Nothing
        colMe = Nothing
        colJe = Nothing
        colVe = Nothing
        colSa = Nothing
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean, Optional ByVal onlyHours As Boolean = False)
        DiHe.ReadOnly = trueFalse
        LuHe.ReadOnly = trueFalse
        MeHe.ReadOnly = trueFalse
        MaHe.ReadOnly = trueFalse
        JeHe.ReadOnly = trueFalse
        VeHe.ReadOnly = trueFalse
        SaHe.ReadOnly = trueFalse
        If onlyHours Then Exit Sub

        SalaireHoraire.Enabled = Not trueFalse
        PourcentVisite.Enabled = Not trueFalse
        DiVi.Enabled = Not trueFalse
        LuVi.Enabled = Not trueFalse
        MaVi.Enabled = Not trueFalse
        MeVi.Enabled = Not trueFalse
        JeVi.Enabled = Not trueFalse
        VeVi.Enabled = Not trueFalse
        SaVi.Enabled = Not trueFalse
        DiJo.Enabled = Not trueFalse
        LuJo.Enabled = Not trueFalse
        MaJo.Enabled = Not trueFalse
        MeJo.Enabled = Not trueFalse
        JeJo.Enabled = Not trueFalse
        VeJo.Enabled = Not trueFalse
        SaJo.Enabled = Not trueFalse
        selectSemaine.Enabled = Not trueFalse
        GenerateByHours.Enabled = Not trueFalse
        GenerateByVisites.Enabled = Not trueFalse

        If trueFalse = False Then
            Calculer.Enabled = False
            Generer.Enabled = False
        End If
    End Sub

    Private Sub generateByHours_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateByHours.CheckedChanged
        activateControlsOnType(True)
    End Sub

    Private Sub generateByVisites_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateByVisites.CheckedChanged
        activateControlsOnType(False)
    End Sub

    Private previousRow As Integer = -1

    Private Sub histoPaies_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HistoPaies.CurrentCellChanged
        If previousRow <> HistoPaies.CurrentRowIndex Then
            previousRow = HistoPaies.CurrentRowIndex

            Dim myCurrentHisto(,) As String = DBLinker.getInstance.readDB("PayesUtilisateurs", "PayesUtilisateurs.DatePaie, PayesUtilisateurs.Type, PayesUtilisateurs.DiNb,PayesUtilisateurs.LuNb,PayesUtilisateurs.MaNb,PayesUtilisateurs.MeNb,PayesUtilisateurs.JeNb,PayesUtilisateurs.VeNb,PayesUtilisateurs.SaNb,PayesUtilisateurs.Multiplicateur,PayesUtilisateurs.DiMontantFixe,PayesUtilisateurs.LuMontantFixe,PayesUtilisateurs.MaMontantFixe,PayesUtilisateurs.MeMontantFixe,PayesUtilisateurs.JeMontantFixe,PayesUtilisateurs.VeMontantFixe,PayesUtilisateurs.SaMontantFixe", "WHERE NoUser=" & curNoUser & " AND DatePaie = '" & DateFormat.getTextDate(CDate(CType(HistoPaies.DataSource, DataTable).Rows(HistoPaies.CurrentRowIndex)("DatePaie"))) & "'")
            If myCurrentHisto Is Nothing OrElse myCurrentHisto.Length = 0 Then Exit Sub

            Semaine.Text = DateFormat.getTextDate(CDate(myCurrentHisto(0, 0))) & " au " & DateFormat.getTextDate(CDate(myCurrentHisto(0, 0)).AddDays(6))
            Calculer.Enabled = True
            Generer.Enabled = True
            If myCurrentHisto(1, 0) = "Par heure" Then
                GenerateByHours.Checked = True
                DiHe.Text = myCurrentHisto(2, 0)
                LuHe.Text = myCurrentHisto(3, 0)
                MeHe.Text = myCurrentHisto(4, 0)
                MaHe.Text = myCurrentHisto(5, 0)
                JeHe.Text = myCurrentHisto(6, 0)
                VeHe.Text = myCurrentHisto(7, 0)
                SaHe.Text = myCurrentHisto(8, 0)
                SalaireHoraire.Text = myCurrentHisto(9, 0)
                If currentDroitAcces(92) Then lockItems(False, True)
            Else
                GenerateByVisites.Checked = True
                DiVi.Text = myCurrentHisto(2, 0)
                LuVi.Text = myCurrentHisto(3, 0)
                MaVi.Text = myCurrentHisto(4, 0)
                MeVi.Text = myCurrentHisto(5, 0)
                JeVi.Text = myCurrentHisto(6, 0)
                VeVi.Text = myCurrentHisto(7, 0)
                SaVi.Text = myCurrentHisto(8, 0)
                PourcentVisite.Text = myCurrentHisto(9, 0) * 100
            End If
            DiJo.Text = myCurrentHisto(10, 0)
            LuJo.Text = myCurrentHisto(11, 0)
            MaJo.Text = myCurrentHisto(12, 0)
            MeJo.Text = myCurrentHisto(13, 0)
            JeJo.Text = myCurrentHisto(14, 0)
            VeJo.Text = myCurrentHisto(15, 0)
            SaJo.Text = myCurrentHisto(16, 0)
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get

        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class

Public Class HeaderAndDataAlignColumn
    Inherits DataGridTextBoxColumn

    Private mTxtAlign As HorizontalAlignment = HorizontalAlignment.Left
    Private mDrawTxt As New StringFormat

    Protected Overloads Overrides Sub edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
        MyBase.Edit(source, rowNum, bounds, [readOnly], instantText, cellIsVisible)
        MyBase.TextBox.TextAlign = mTxtAlign
        MyBase.TextBox.CharacterCasing = CharacterCasing.Upper
    End Sub

    Protected Overloads Overrides Sub paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As System.Drawing.Brush, ByVal foreBrush As System.Drawing.Brush, ByVal alignToRight As Boolean)
        'clear the cell
        g.FillRectangle(backBrush, bounds)

        'draw the value
        Dim s As String
        If TypeOf Me.GetColumnValueAtRow([source], rowNum) Is Decimal Then
            s = CDec(Me.GetColumnValueAtRow([source], rowNum)).ToString(Me.Format)
        Else
            s = Me.GetColumnValueAtRow([source], rowNum).ToString
        End If
        Dim r As Rectangle = bounds
        r.Inflate(0, -1)
        g.DrawString(s, MyBase.TextBox.Font, foreBrush, RectangleF.op_Implicit(r), mDrawTxt)
    End Sub

    Public Property dataAlignment() As HorizontalAlignment
        Get
            Return mTxtAlign
        End Get
        Set(ByVal Value As HorizontalAlignment)
            mTxtAlign = Value
            If mTxtAlign = HorizontalAlignment.Center Then
                mDrawTxt.Alignment = StringAlignment.Center
            ElseIf mTxtAlign = HorizontalAlignment.Right Then
                mDrawTxt.Alignment = StringAlignment.Far
            Else
                mDrawTxt.Alignment = StringAlignment.Near
            End If
        End Set
    End Property

End Class
