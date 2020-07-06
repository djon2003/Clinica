Option Strict Off
Option Explicit On
Friend Class workHours
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.MdiParent = myMainWin

        'Chargement des images
        imgModifSave = New ImageList()
        With DrawingManager.getInstance
            Try
                imgModifSave.Images.Add(.getImage("modifier16.gif"))
                imgModifSave.Images.Add(.getImage("save.jpg"))
                imgModifSave.Images.Add(.getImage("stopmodif16.gif"))
            Catch
            End Try
            Me.modifWeek.Image = imgModifSave.Images(0)
            Me.Save.Image = imgModifSave.Images(1)
            Me.SaveAs.Image = DrawingManager.iconToImage(.getIcon("saveas.ico"), New Size(16, 16))
            Me.Icon = .getIcon("GC.ico")
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
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents sexe As BaseObjArray
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents menuRefCompte As System.Windows.Forms.MenuItem
    Friend WithEvents menuRefKP As System.Windows.Forms.MenuItem
    Friend WithEvents menuRefAutre As System.Windows.Forms.MenuItem
    Friend WithEvents RefMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Users As System.Windows.Forms.ComboBox
    Friend WithEvents CurSemaine As System.Windows.Forms.Label
    Friend WithEvents Weeks As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DiDe As System.Windows.Forms.ComboBox
    Friend WithEvents DiA As System.Windows.Forms.ComboBox
    Friend WithEvents LuDe As System.Windows.Forms.ComboBox
    Friend WithEvents LuA As System.Windows.Forms.ComboBox
    Friend WithEvents MaDe As System.Windows.Forms.ComboBox
    Friend WithEvents MaA As System.Windows.Forms.ComboBox
    Friend WithEvents MeDe As System.Windows.Forms.ComboBox
    Friend WithEvents MeA As System.Windows.Forms.ComboBox
    Friend WithEvents JeDe As System.Windows.Forms.ComboBox
    Friend WithEvents JeA As System.Windows.Forms.ComboBox
    Friend WithEvents VeDe As System.Windows.Forms.ComboBox
    Friend WithEvents VeA As System.Windows.Forms.ComboBox
    Friend WithEvents SaDe As System.Windows.Forms.ComboBox
    Friend WithEvents SaA As System.Windows.Forms.ComboBox
    Friend WithEvents modifWeek As System.Windows.Forms.Button
    Friend WithEvents SaveAs As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Approuved As System.Windows.Forms.CheckBox
    Friend WithEvents CopyForward1 As System.Windows.Forms.Button
    Friend WithEvents CopyForward2 As System.Windows.Forms.Button
    Friend WithEvents CopyBackward1 As System.Windows.Forms.Button
    Friend WithEvents CopyForward3 As System.Windows.Forms.Button
    Friend WithEvents CopyBackward2 As System.Windows.Forms.Button
    Friend WithEvents CopyForward4 As System.Windows.Forms.Button
    Friend WithEvents CopyBackward3 As System.Windows.Forms.Button
    Friend WithEvents CopyForward5 As System.Windows.Forms.Button
    Friend WithEvents CopyBackward4 As System.Windows.Forms.Button
    Friend WithEvents CopyForward6 As System.Windows.Forms.Button
    Friend WithEvents CopyBackward5 As System.Windows.Forms.Button
    Friend WithEvents CopyBackward6 As System.Windows.Forms.Button
    Friend WithEvents menuPreRefList As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Dim panel1 As System.Windows.Forms.Panel
        Dim label11 As System.Windows.Forms.Label
        Dim label3 As System.Windows.Forms.Label
        Dim label4 As System.Windows.Forms.Label
        Dim label5 As System.Windows.Forms.Label
        Dim label6 As System.Windows.Forms.Label
        Dim label7 As System.Windows.Forms.Label
        Dim label8 As System.Windows.Forms.Label
        Dim label9 As System.Windows.Forms.Label
        Dim label12 As System.Windows.Forms.Label
        Dim panel2 As System.Windows.Forms.Panel
        Dim panel3 As System.Windows.Forms.Panel
        Dim panel4 As System.Windows.Forms.Panel
        Dim panel5 As System.Windows.Forms.Panel
        Dim panel6 As System.Windows.Forms.Panel
        Dim panel7 As System.Windows.Forms.Panel
        Me.DiDe = New System.Windows.Forms.ComboBox
        Me.DiA = New System.Windows.Forms.ComboBox
        Me.LuDe = New System.Windows.Forms.ComboBox
        Me.LuA = New System.Windows.Forms.ComboBox
        Me.MaDe = New System.Windows.Forms.ComboBox
        Me.MaA = New System.Windows.Forms.ComboBox
        Me.MeDe = New System.Windows.Forms.ComboBox
        Me.MeA = New System.Windows.Forms.ComboBox
        Me.JeDe = New System.Windows.Forms.ComboBox
        Me.JeA = New System.Windows.Forms.ComboBox
        Me.VeDe = New System.Windows.Forms.ComboBox
        Me.VeA = New System.Windows.Forms.ComboBox
        Me.SaDe = New System.Windows.Forms.ComboBox
        Me.SaA = New System.Windows.Forms.ComboBox
        Me.CopyForward1 = New System.Windows.Forms.Button
        Me.CurSemaine = New System.Windows.Forms.Label
        Me.CopyForward2 = New System.Windows.Forms.Button
        Me.CopyBackward1 = New System.Windows.Forms.Button
        Me.CopyForward3 = New System.Windows.Forms.Button
        Me.CopyBackward2 = New System.Windows.Forms.Button
        Me.CopyForward4 = New System.Windows.Forms.Button
        Me.CopyBackward3 = New System.Windows.Forms.Button
        Me.CopyForward5 = New System.Windows.Forms.Button
        Me.CopyBackward4 = New System.Windows.Forms.Button
        Me.CopyForward6 = New System.Windows.Forms.Button
        Me.CopyBackward5 = New System.Windows.Forms.Button
        Me.CopyBackward6 = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.modifWeek = New System.Windows.Forms.Button
        Me.SaveAs = New System.Windows.Forms.Button
        Me.Save = New System.Windows.Forms.Button
        Me.RefMenu = New System.Windows.Forms.ContextMenu
        Me.menuRefAutre = New System.Windows.Forms.MenuItem
        Me.menuRefCompte = New System.Windows.Forms.MenuItem
        Me.menuPreRefList = New System.Windows.Forms.MenuItem
        Me.menuRefKP = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.Users = New System.Windows.Forms.ComboBox
        Me.Weeks = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Approuved = New System.Windows.Forms.CheckBox
        tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        panel1 = New System.Windows.Forms.Panel
        label11 = New System.Windows.Forms.Label
        label3 = New System.Windows.Forms.Label
        label4 = New System.Windows.Forms.Label
        label5 = New System.Windows.Forms.Label
        label6 = New System.Windows.Forms.Label
        label7 = New System.Windows.Forms.Label
        label8 = New System.Windows.Forms.Label
        label9 = New System.Windows.Forms.Label
        label12 = New System.Windows.Forms.Label
        panel2 = New System.Windows.Forms.Panel
        panel3 = New System.Windows.Forms.Panel
        panel4 = New System.Windows.Forms.Panel
        panel5 = New System.Windows.Forms.Panel
        panel6 = New System.Windows.Forms.Panel
        panel7 = New System.Windows.Forms.Panel
        tableLayoutPanel1.SuspendLayout()
        panel1.SuspendLayout()
        panel2.SuspendLayout()
        panel3.SuspendLayout()
        panel4.SuspendLayout()
        panel5.SuspendLayout()
        panel6.SuspendLayout()
        panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble
        tableLayoutPanel1.ColumnCount = 8
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        tableLayoutPanel1.Controls.Add(Me.DiDe, 1, 2)
        tableLayoutPanel1.Controls.Add(Me.DiA, 1, 3)
        tableLayoutPanel1.Controls.Add(Me.LuDe, 2, 2)
        tableLayoutPanel1.Controls.Add(Me.LuA, 2, 3)
        tableLayoutPanel1.Controls.Add(Me.MaDe, 3, 2)
        tableLayoutPanel1.Controls.Add(Me.MaA, 3, 3)
        tableLayoutPanel1.Controls.Add(Me.MeDe, 4, 2)
        tableLayoutPanel1.Controls.Add(Me.MeA, 4, 3)
        tableLayoutPanel1.Controls.Add(Me.JeDe, 5, 2)
        tableLayoutPanel1.Controls.Add(Me.JeA, 5, 3)
        tableLayoutPanel1.Controls.Add(Me.VeDe, 6, 2)
        tableLayoutPanel1.Controls.Add(Me.VeA, 6, 3)
        tableLayoutPanel1.Controls.Add(Me.SaDe, 7, 2)
        tableLayoutPanel1.Controls.Add(Me.SaA, 7, 3)
        tableLayoutPanel1.Controls.Add(panel1, 1, 4)
        tableLayoutPanel1.Controls.Add(label11, 0, 3)
        tableLayoutPanel1.Controls.Add(Me.CurSemaine, 0, 0)
        tableLayoutPanel1.Controls.Add(label3, 1, 1)
        tableLayoutPanel1.Controls.Add(label4, 2, 1)
        tableLayoutPanel1.Controls.Add(label5, 3, 1)
        tableLayoutPanel1.Controls.Add(label6, 4, 1)
        tableLayoutPanel1.Controls.Add(label7, 5, 1)
        tableLayoutPanel1.Controls.Add(label8, 6, 1)
        tableLayoutPanel1.Controls.Add(label9, 7, 1)
        tableLayoutPanel1.Controls.Add(label12, 0, 2)
        tableLayoutPanel1.Controls.Add(panel2, 2, 4)
        tableLayoutPanel1.Controls.Add(panel3, 3, 4)
        tableLayoutPanel1.Controls.Add(panel4, 4, 4)
        tableLayoutPanel1.Controls.Add(panel5, 5, 4)
        tableLayoutPanel1.Controls.Add(panel6, 6, 4)
        tableLayoutPanel1.Controls.Add(panel7, 7, 4)
        tableLayoutPanel1.Location = New System.Drawing.Point(15, 34)
        tableLayoutPanel1.Name = "TableLayoutPanel1"
        tableLayoutPanel1.RowCount = 5
        tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        tableLayoutPanel1.Size = New System.Drawing.Size(492, 142)
        tableLayoutPanel1.TabIndex = 2
        '
        'DiDe
        '
        Me.DiDe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DiDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DiDe.FormattingEnabled = True
        Me.DiDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.DiDe.Location = New System.Drawing.Point(49, 46)
        Me.DiDe.Name = "DiDe"
        Me.DiDe.Size = New System.Drawing.Size(54, 22)
        Me.DiDe.TabIndex = 3
        '
        'DiA
        '
        Me.DiA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DiA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DiA.FormattingEnabled = True
        Me.DiA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.DiA.Location = New System.Drawing.Point(49, 80)
        Me.DiA.Name = "DiA"
        Me.DiA.Size = New System.Drawing.Size(54, 22)
        Me.DiA.TabIndex = 4
        '
        'LuDe
        '
        Me.LuDe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LuDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LuDe.FormattingEnabled = True
        Me.LuDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.LuDe.Location = New System.Drawing.Point(112, 46)
        Me.LuDe.Name = "LuDe"
        Me.LuDe.Size = New System.Drawing.Size(54, 22)
        Me.LuDe.TabIndex = 5
        '
        'LuA
        '
        Me.LuA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LuA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LuA.FormattingEnabled = True
        Me.LuA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.LuA.Location = New System.Drawing.Point(112, 80)
        Me.LuA.Name = "LuA"
        Me.LuA.Size = New System.Drawing.Size(54, 22)
        Me.LuA.TabIndex = 6
        '
        'MaDe
        '
        Me.MaDe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.MaDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MaDe.FormattingEnabled = True
        Me.MaDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.MaDe.Location = New System.Drawing.Point(175, 46)
        Me.MaDe.Name = "MaDe"
        Me.MaDe.Size = New System.Drawing.Size(54, 22)
        Me.MaDe.TabIndex = 7
        '
        'MaA
        '
        Me.MaA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.MaA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MaA.FormattingEnabled = True
        Me.MaA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.MaA.Location = New System.Drawing.Point(175, 80)
        Me.MaA.Name = "MaA"
        Me.MaA.Size = New System.Drawing.Size(54, 22)
        Me.MaA.TabIndex = 8
        '
        'MeDe
        '
        Me.MeDe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.MeDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MeDe.FormattingEnabled = True
        Me.MeDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.MeDe.Location = New System.Drawing.Point(238, 46)
        Me.MeDe.Name = "MeDe"
        Me.MeDe.Size = New System.Drawing.Size(54, 22)
        Me.MeDe.TabIndex = 9
        '
        'MeA
        '
        Me.MeA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.MeA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MeA.FormattingEnabled = True
        Me.MeA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.MeA.Location = New System.Drawing.Point(238, 80)
        Me.MeA.Name = "MeA"
        Me.MeA.Size = New System.Drawing.Size(54, 22)
        Me.MeA.TabIndex = 10
        '
        'JeDe
        '
        Me.JeDe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.JeDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.JeDe.FormattingEnabled = True
        Me.JeDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.JeDe.Location = New System.Drawing.Point(301, 46)
        Me.JeDe.Name = "JeDe"
        Me.JeDe.Size = New System.Drawing.Size(54, 22)
        Me.JeDe.TabIndex = 11
        '
        'JeA
        '
        Me.JeA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.JeA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.JeA.FormattingEnabled = True
        Me.JeA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.JeA.Location = New System.Drawing.Point(301, 80)
        Me.JeA.Name = "JeA"
        Me.JeA.Size = New System.Drawing.Size(54, 22)
        Me.JeA.TabIndex = 12
        '
        'VeDe
        '
        Me.VeDe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.VeDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VeDe.FormattingEnabled = True
        Me.VeDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.VeDe.Location = New System.Drawing.Point(364, 46)
        Me.VeDe.Name = "VeDe"
        Me.VeDe.Size = New System.Drawing.Size(54, 22)
        Me.VeDe.TabIndex = 13
        '
        'VeA
        '
        Me.VeA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.VeA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VeA.FormattingEnabled = True
        Me.VeA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.VeA.Location = New System.Drawing.Point(364, 80)
        Me.VeA.Name = "VeA"
        Me.VeA.Size = New System.Drawing.Size(54, 22)
        Me.VeA.TabIndex = 14
        '
        'SaDe
        '
        Me.SaDe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SaDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SaDe.FormattingEnabled = True
        Me.SaDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.SaDe.Location = New System.Drawing.Point(429, 46)
        Me.SaDe.Name = "SaDe"
        Me.SaDe.Size = New System.Drawing.Size(54, 22)
        Me.SaDe.TabIndex = 15
        '
        'SaA
        '
        Me.SaA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SaA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SaA.FormattingEnabled = True
        Me.SaA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.SaA.Location = New System.Drawing.Point(429, 80)
        Me.SaA.Name = "SaA"
        Me.SaA.Size = New System.Drawing.Size(54, 22)
        Me.SaA.TabIndex = 16
        '
        'Panel1
        '
        panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        panel1.Controls.Add(Me.CopyForward1)
        panel1.Location = New System.Drawing.Point(58, 112)
        panel1.Name = "Panel1"
        panel1.Size = New System.Drawing.Size(36, 24)
        panel1.TabIndex = 22
        '
        'CopyForward1
        '
        Me.CopyForward1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyForward1.Location = New System.Drawing.Point(21, 1)
        Me.CopyForward1.Name = "CopyForward1"
        Me.CopyForward1.Size = New System.Drawing.Size(15, 23)
        Me.CopyForward1.TabIndex = 23
        Me.CopyForward1.TabStop = False
        Me.CopyForward1.Text = ">"
        Me.CopyForward1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyForward1, "Copier vers lundi")
        Me.CopyForward1.UseVisualStyleBackColor = True
        '
        'Label11
        '
        label11.Anchor = System.Windows.Forms.AnchorStyles.None
        label11.AutoSize = True
        label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        label11.Location = New System.Drawing.Point(12, 83)
        label11.Name = "Label11"
        label11.Size = New System.Drawing.Size(21, 14)
        label11.TabIndex = 2
        label11.Text = "À :"
        '
        'CurSemaine
        '
        Me.CurSemaine.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CurSemaine.AutoSize = True
        tableLayoutPanel1.SetColumnSpan(Me.CurSemaine, 8)
        Me.CurSemaine.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurSemaine.Location = New System.Drawing.Point(193, 3)
        Me.CurSemaine.Name = "CurSemaine"
        Me.CurSemaine.Size = New System.Drawing.Size(106, 14)
        Me.CurSemaine.TabIndex = 2
        Me.CurSemaine.Text = "Semaine : Aucune"
        '
        'Label3
        '
        label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        label3.AutoSize = True
        label3.Location = New System.Drawing.Point(49, 22)
        label3.Name = "Label3"
        label3.Size = New System.Drawing.Size(54, 14)
        label3.TabIndex = 2
        label3.Text = "Dimanche"
        label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        label4.Anchor = System.Windows.Forms.AnchorStyles.Top
        label4.AutoSize = True
        label4.Location = New System.Drawing.Point(122, 22)
        label4.Name = "Label4"
        label4.Size = New System.Drawing.Size(33, 14)
        label4.TabIndex = 2
        label4.Text = "Lundi"
        label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label5
        '
        label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        label5.AutoSize = True
        label5.Location = New System.Drawing.Point(185, 22)
        label5.Name = "Label5"
        label5.Size = New System.Drawing.Size(33, 14)
        label5.TabIndex = 2
        label5.Text = "Mardi"
        label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label6
        '
        label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        label6.AutoSize = True
        label6.Location = New System.Drawing.Point(240, 22)
        label6.Name = "Label6"
        label6.Size = New System.Drawing.Size(49, 14)
        label6.TabIndex = 2
        label6.Text = "Mercredi"
        label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label7
        '
        label7.Anchor = System.Windows.Forms.AnchorStyles.Top
        label7.AutoSize = True
        label7.Location = New System.Drawing.Point(312, 22)
        label7.Name = "Label7"
        label7.Size = New System.Drawing.Size(32, 14)
        label7.TabIndex = 2
        label7.Text = "Jeudi"
        label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label8
        '
        label8.Anchor = System.Windows.Forms.AnchorStyles.Top
        label8.AutoSize = True
        label8.Location = New System.Drawing.Point(365, 22)
        label8.Name = "Label8"
        label8.Size = New System.Drawing.Size(51, 14)
        label8.TabIndex = 2
        label8.Text = "Vendredi"
        label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label9
        '
        label9.Anchor = System.Windows.Forms.AnchorStyles.Top
        label9.AutoSize = True
        label9.Location = New System.Drawing.Point(435, 22)
        label9.Name = "Label9"
        label9.Size = New System.Drawing.Size(42, 14)
        label9.TabIndex = 2
        label9.Text = "Samedi"
        label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label12
        '
        label12.Anchor = System.Windows.Forms.AnchorStyles.None
        label12.AutoSize = True
        label12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        label12.Location = New System.Drawing.Point(9, 49)
        label12.Name = "Label12"
        label12.Size = New System.Drawing.Size(27, 14)
        label12.TabIndex = 2
        label12.Text = "De :"
        '
        'Panel2
        '
        panel2.Anchor = System.Windows.Forms.AnchorStyles.None
        panel2.Controls.Add(Me.CopyForward2)
        panel2.Controls.Add(Me.CopyBackward1)
        panel2.Location = New System.Drawing.Point(121, 112)
        panel2.Name = "Panel2"
        panel2.Size = New System.Drawing.Size(36, 24)
        panel2.TabIndex = 23
        '
        'CopyForward2
        '
        Me.CopyForward2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyForward2.Location = New System.Drawing.Point(21, 1)
        Me.CopyForward2.Name = "CopyForward2"
        Me.CopyForward2.Size = New System.Drawing.Size(15, 23)
        Me.CopyForward2.TabIndex = 25
        Me.CopyForward2.TabStop = False
        Me.CopyForward2.Text = ">"
        Me.CopyForward2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyForward2, "Copier vers mardi")
        Me.CopyForward2.UseVisualStyleBackColor = True
        '
        'CopyBackward1
        '
        Me.CopyBackward1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyBackward1.Location = New System.Drawing.Point(0, 1)
        Me.CopyBackward1.Name = "CopyBackward1"
        Me.CopyBackward1.Size = New System.Drawing.Size(15, 23)
        Me.CopyBackward1.TabIndex = 24
        Me.CopyBackward1.TabStop = False
        Me.CopyBackward1.Text = "<"
        Me.CopyBackward1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyBackward1, "Copier vers dimanche")
        Me.CopyBackward1.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        panel3.Anchor = System.Windows.Forms.AnchorStyles.None
        panel3.Controls.Add(Me.CopyForward3)
        panel3.Controls.Add(Me.CopyBackward2)
        panel3.Location = New System.Drawing.Point(184, 112)
        panel3.Name = "Panel3"
        panel3.Size = New System.Drawing.Size(36, 24)
        panel3.TabIndex = 24
        '
        'CopyForward3
        '
        Me.CopyForward3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyForward3.Location = New System.Drawing.Point(21, 1)
        Me.CopyForward3.Name = "CopyForward3"
        Me.CopyForward3.Size = New System.Drawing.Size(15, 23)
        Me.CopyForward3.TabIndex = 27
        Me.CopyForward3.TabStop = False
        Me.CopyForward3.Text = ">"
        Me.CopyForward3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyForward3, "Copier vers mercredi")
        Me.CopyForward3.UseVisualStyleBackColor = True
        '
        'CopyBackward2
        '
        Me.CopyBackward2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyBackward2.Location = New System.Drawing.Point(0, 1)
        Me.CopyBackward2.Name = "CopyBackward2"
        Me.CopyBackward2.Size = New System.Drawing.Size(15, 23)
        Me.CopyBackward2.TabIndex = 26
        Me.CopyBackward2.TabStop = False
        Me.CopyBackward2.Text = "<"
        Me.CopyBackward2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyBackward2, "Copier vers lundi")
        Me.CopyBackward2.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        panel4.Anchor = System.Windows.Forms.AnchorStyles.None
        panel4.Controls.Add(Me.CopyForward4)
        panel4.Controls.Add(Me.CopyBackward3)
        panel4.Location = New System.Drawing.Point(247, 112)
        panel4.Name = "Panel4"
        panel4.Size = New System.Drawing.Size(36, 24)
        panel4.TabIndex = 22
        '
        'CopyForward4
        '
        Me.CopyForward4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyForward4.Location = New System.Drawing.Point(21, 1)
        Me.CopyForward4.Name = "CopyForward4"
        Me.CopyForward4.Size = New System.Drawing.Size(15, 23)
        Me.CopyForward4.TabIndex = 29
        Me.CopyForward4.TabStop = False
        Me.CopyForward4.Text = ">"
        Me.CopyForward4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyForward4, "Copier vers jeudi")
        Me.CopyForward4.UseVisualStyleBackColor = True
        '
        'CopyBackward3
        '
        Me.CopyBackward3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyBackward3.Location = New System.Drawing.Point(0, 1)
        Me.CopyBackward3.Name = "CopyBackward3"
        Me.CopyBackward3.Size = New System.Drawing.Size(15, 23)
        Me.CopyBackward3.TabIndex = 28
        Me.CopyBackward3.TabStop = False
        Me.CopyBackward3.Text = "<"
        Me.CopyBackward3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyBackward3, "Copier vers mardi")
        Me.CopyBackward3.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        panel5.Anchor = System.Windows.Forms.AnchorStyles.None
        panel5.Controls.Add(Me.CopyForward5)
        panel5.Controls.Add(Me.CopyBackward4)
        panel5.Location = New System.Drawing.Point(310, 112)
        panel5.Name = "Panel5"
        panel5.Size = New System.Drawing.Size(36, 24)
        panel5.TabIndex = 25
        '
        'CopyForward5
        '
        Me.CopyForward5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyForward5.Location = New System.Drawing.Point(21, 1)
        Me.CopyForward5.Name = "CopyForward5"
        Me.CopyForward5.Size = New System.Drawing.Size(15, 23)
        Me.CopyForward5.TabIndex = 31
        Me.CopyForward5.TabStop = False
        Me.CopyForward5.Text = ">"
        Me.CopyForward5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyForward5, "Copier vers vendredi")
        Me.CopyForward5.UseVisualStyleBackColor = True
        '
        'CopyBackward4
        '
        Me.CopyBackward4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyBackward4.Location = New System.Drawing.Point(0, 1)
        Me.CopyBackward4.Name = "CopyBackward4"
        Me.CopyBackward4.Size = New System.Drawing.Size(15, 23)
        Me.CopyBackward4.TabIndex = 30
        Me.CopyBackward4.TabStop = False
        Me.CopyBackward4.Text = "<"
        Me.CopyBackward4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyBackward4, "Copier vers mercredi")
        Me.CopyBackward4.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        panel6.Anchor = System.Windows.Forms.AnchorStyles.None
        panel6.Controls.Add(Me.CopyForward6)
        panel6.Controls.Add(Me.CopyBackward5)
        panel6.Location = New System.Drawing.Point(373, 112)
        panel6.Name = "Panel6"
        panel6.Size = New System.Drawing.Size(36, 24)
        panel6.TabIndex = 26
        '
        'CopyForward6
        '
        Me.CopyForward6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyForward6.Location = New System.Drawing.Point(21, 1)
        Me.CopyForward6.Name = "CopyForward6"
        Me.CopyForward6.Size = New System.Drawing.Size(15, 23)
        Me.CopyForward6.TabIndex = 33
        Me.CopyForward6.TabStop = False
        Me.CopyForward6.Text = ">"
        Me.CopyForward6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyForward6, "Copier vers samedi")
        Me.CopyForward6.UseVisualStyleBackColor = True
        '
        'CopyBackward5
        '
        Me.CopyBackward5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyBackward5.Location = New System.Drawing.Point(0, 1)
        Me.CopyBackward5.Name = "CopyBackward5"
        Me.CopyBackward5.Size = New System.Drawing.Size(15, 23)
        Me.CopyBackward5.TabIndex = 32
        Me.CopyBackward5.TabStop = False
        Me.CopyBackward5.Text = "<"
        Me.CopyBackward5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyBackward5, "Copier vers jeudi")
        Me.CopyBackward5.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        panel7.Anchor = System.Windows.Forms.AnchorStyles.None
        panel7.Controls.Add(Me.CopyBackward6)
        panel7.Location = New System.Drawing.Point(438, 112)
        panel7.Name = "Panel7"
        panel7.Size = New System.Drawing.Size(36, 24)
        panel7.TabIndex = 27
        '
        'CopyBackward6
        '
        Me.CopyBackward6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CopyBackward6.Location = New System.Drawing.Point(0, 1)
        Me.CopyBackward6.Name = "CopyBackward6"
        Me.CopyBackward6.Size = New System.Drawing.Size(15, 23)
        Me.CopyBackward6.TabIndex = 34
        Me.CopyBackward6.TabStop = False
        Me.CopyBackward6.Text = "<"
        Me.CopyBackward6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.CopyBackward6, "Copier vers vendredi")
        Me.CopyBackward6.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'modifWeek
        '
        Me.modifWeek.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.modifWeek.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifWeek.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.modifWeek.Location = New System.Drawing.Point(423, 182)
        Me.modifWeek.Name = "modifWeek"
        Me.modifWeek.Size = New System.Drawing.Size(24, 24)
        Me.modifWeek.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.modifWeek, "Commencer à modifier cet utilisateur")
        Me.modifWeek.UseVisualStyleBackColor = True
        '
        'SaveAs
        '
        Me.SaveAs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveAs.Enabled = False
        Me.SaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveAs.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.SaveAs.Location = New System.Drawing.Point(483, 182)
        Me.SaveAs.Name = "SaveAs"
        Me.SaveAs.Size = New System.Drawing.Size(24, 24)
        Me.SaveAs.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.SaveAs, "Enregistrer sous un autre semaine la semaine en cours")
        Me.SaveAs.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save.Enabled = False
        Me.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Save.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Save.Location = New System.Drawing.Point(453, 182)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(24, 24)
        Me.Save.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.Save, "Enregistrer la semaine en cours")
        Me.Save.UseVisualStyleBackColor = True
        '
        'RefMenu
        '
        Me.RefMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuRefAutre, Me.menuRefCompte, Me.menuPreRefList, Me.menuRefKP})
        '
        'menuRefAutre
        '
        Me.menuRefAutre.Index = 0
        Me.menuRefAutre.Text = "Autre"
        '
        'menuRefCompte
        '
        Me.menuRefCompte.Index = 1
        Me.menuRefCompte.Text = "Compte client"
        '
        'menuPreRefList
        '
        Me.menuPreRefList.Index = 2
        Me.menuPreRefList.Text = "Liste prédéterminée"
        '
        'menuRefKP
        '
        Me.menuRefKP.Index = 3
        Me.menuRefKP.Text = "Personne ou organisme clé"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Utilisateur :"
        '
        'Users
        '
        Me.Users.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Users.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Users.FormattingEnabled = True
        Me.Users.Location = New System.Drawing.Point(78, 6)
        Me.Users.Name = "Users"
        Me.Users.Size = New System.Drawing.Size(229, 22)
        Me.Users.Sorted = True
        Me.Users.TabIndex = 1
        '
        'Weeks
        '
        Me.Weeks.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Weeks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Weeks.FormattingEnabled = True
        Me.Weeks.Location = New System.Drawing.Point(373, 6)
        Me.Weeks.Name = "Weeks"
        Me.Weeks.Size = New System.Drawing.Size(134, 22)
        Me.Weeks.Sorted = True
        Me.Weeks.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(313, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Semaine :"
        '
        'Approuved
        '
        Me.Approuved.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Approuved.AutoSize = True
        Me.Approuved.Location = New System.Drawing.Point(12, 188)
        Me.Approuved.Name = "Approuved"
        Me.Approuved.Size = New System.Drawing.Size(191, 18)
        Me.Approuved.TabIndex = 20
        Me.Approuved.Text = "Heures de la semaine approuvées"
        Me.Approuved.UseVisualStyleBackColor = True
        '
        'workHours
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(519, 218)
        Me.Controls.Add(tableLayoutPanel1)
        Me.Controls.Add(Me.Approuved)
        Me.Controls.Add(Me.Weeks)
        Me.Controls.Add(Me.Users)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.SaveAs)
        Me.Controls.Add(Me.modifWeek)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(277, 266)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "workHours"
        Me.ShowInTaskbar = False
        Me.Text = "Gestion des quarts de travail"
        tableLayoutPanel1.ResumeLayout(False)
        tableLayoutPanel1.PerformLayout()
        panel1.ResumeLayout(False)
        panel2.ResumeLayout(False)
        panel3.ResumeLayout(False)
        panel4.ResumeLayout(False)
        panel5.ResumeLayout(False)
        panel6.ResumeLayout(False)
        panel7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private currentlySaving As Boolean = False
    Private imgModifSave As ImageList
    Private formModified As Boolean = False
    Private oldWeek As String = ""
    Private oldNoUser As Integer = 0

    Private Sub workHours_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Approuved.Visible = PreferencesManager.getGeneralPreferences()("ActivateWorkHoursApprobation")

        'Associe les bouttons copiers
        CopyBackward1.Tag = LuDe
        CopyBackward2.Tag = MaDe
        CopyBackward3.Tag = MeDe
        CopyBackward4.Tag = JeDe
        CopyBackward5.Tag = VeDe
        CopyBackward6.Tag = SaDe
        CopyForward1.Tag = DiDe
        CopyForward2.Tag = LuDe
        CopyForward3.Tag = MaDe
        CopyForward4.Tag = MeDe
        CopyForward5.Tag = JeDe
        CopyForward6.Tag = VeDe

        resetItems()

        lockItems(True)

        'Load Users
        loadUsers()

        'Ajout ds la barre d'outils
        formModified = False
    End Sub

    Private Sub resetItems()
        DiDe.SelectedIndex = 0
        DiA.SelectedIndex = 0
        LuDe.SelectedIndex = 0
        LuA.SelectedIndex = 0
        MaDe.SelectedIndex = 0
        MaA.SelectedIndex = 0
        MeDe.SelectedIndex = 0
        MeA.SelectedIndex = 0
        JeDe.SelectedIndex = 0
        JeA.SelectedIndex = 0
        VeDe.SelectedIndex = 0
        VeA.SelectedIndex = 0
        SaDe.SelectedIndex = 0
        SaA.SelectedIndex = 0
        oldWeek = ""
        Weeks.Items.Clear()
        CurSemaine.Text = "Semaine : Aucune"
        Approuved.Checked = False
    End Sub

    Public Sub loadUsers()
        Users.Items.Clear()

        Dim loadingUsers As Generic.List(Of User) = UsersManager.getInstance.getUsers(False)
        Users.Items.AddRange(loadingUsers.ToArray)

        If Users.Items.Count <> 0 And Users.SelectedIndex < 0 Then Users.SelectedIndex = 0
    End Sub

    Private Sub workHours_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If ToolTip1.GetToolTip(modifWeek).StartsWith("C") = False Then
            lockSecteur("WorkHours-" & oldNoUser, False, "Quarts de travail")

            If formModified = True Then
                If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    If saving() = "" Then e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub workHours_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If ToolTip1.GetToolTip(modifWeek).StartsWith("C") = False Then
            lockSecteur("WorkHours-" & oldNoUser, False, "Quarts de travail")

            If formModified = True Then saving()
        End If
    End Sub

    Private Sub users_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Users.SelectedIndexChanged
        loadWeeks()
    End Sub

    Private Sub loadWeeks()
        resetItems()

        Dim sNoUser() As String = System.Text.RegularExpressions.Regex.Split(Users.Text, " \(")
        Dim noUser As Integer = sNoUser(1).Substring(0, sNoUser(1).Length - 1)

        Dim myWeeks() As String = DBLinker.getInstance.readOneDBField("WorkHours", "Week", "WHERE NoUser=" & noUser)

        If Not myWeeks Is Nothing AndAlso myWeeks.Length <> 0 Then
            Dim i As Integer
            For i = 0 To myWeeks.GetUpperBound(0)
                Weeks.Items.Add(DateFormat.getTextDate(CDate(myWeeks(i)), DateFormat.TextDateOptions.YYYYMMDD))
            Next i

            Weeks.SelectedIndex = 0
        End If
    End Sub

    Private Sub loadWeek()
        If Weeks.Text = "" Then Exit Sub
        If currentlySaving Then Exit Sub

        Dim sNoUser() As String = System.Text.RegularExpressions.Regex.Split(Users.Text, " \(")
        Dim noUser As Integer = sNoUser(1).Substring(0, sNoUser(1).Length - 1)

        Dim myWeek As Date = CDate(Weeks.Text)
        Dim myTimes(,) As String = DBLinker.getInstance.readDB("WorkHours", "DiDe,DiA,LuDe,LuA,MaDe,MaA,MeDe,MeA,JeDe,JeA,VeDe,VeA,SaDe,SaA,Approuved", "WHERE NoUser=" & noUser & " AND Week='" & myWeek.Year & "/" & myWeek.Month & "/" & myWeek.Day & "'")

        If Not myTimes Is Nothing AndAlso myTimes.Length <> 0 Then
            If myTimes(0, 0) <> "" Then DiDe.Text = myTimes(0, 0) Else DiDe.Text = "--:--"
            If myTimes(1, 0) <> "" Then DiA.Text = myTimes(1, 0) Else DiA.Text = "--:--"
            If myTimes(2, 0) <> "" Then LuDe.Text = myTimes(2, 0) Else LuDe.Text = "--:--"
            If myTimes(3, 0) <> "" Then LuA.Text = myTimes(3, 0) Else LuA.Text = "--:--"
            If myTimes(4, 0) <> "" Then MaDe.Text = myTimes(4, 0) Else MaDe.Text = "--:--"
            If myTimes(5, 0) <> "" Then MaA.Text = myTimes(5, 0) Else MaA.Text = "--:--"
            If myTimes(6, 0) <> "" Then MeDe.Text = myTimes(6, 0) Else MeDe.Text = "--:--"
            If myTimes(7, 0) <> "" Then MeA.Text = myTimes(7, 0) Else MeA.Text = "--:--"
            If myTimes(8, 0) <> "" Then JeDe.Text = myTimes(8, 0) Else JeDe.Text = "--:--"
            If myTimes(9, 0) <> "" Then JeA.Text = myTimes(9, 0) Else JeA.Text = "--:--"
            If myTimes(10, 0) <> "" Then VeDe.Text = myTimes(10, 0) Else VeDe.Text = "--:--"
            If myTimes(11, 0) <> "" Then VeA.Text = myTimes(11, 0) Else VeA.Text = "--:--"
            If myTimes(12, 0) <> "" Then SaDe.Text = myTimes(12, 0) Else SaDe.Text = "--:--"
            If myTimes(13, 0) <> "" Then SaA.Text = myTimes(13, 0) Else SaA.Text = "--:--"

            CurSemaine.Text = "Semaine : " & DateFormat.getTextDate(myWeek) & " au " & DateFormat.getTextDate(myWeek.AddDays(6))
            oldWeek = DateFormat.getTextDate(myWeek)
            Approuved.Checked = myTimes(myTimes.GetUpperBound(0), 0)
        Else
            oldWeek = ""
            CurSemaine.Text = "Semaine : Aucune"
        End If
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        DiDe.Enabled = Not trueFalse
        DiA.Enabled = Not trueFalse
        LuDe.Enabled = Not trueFalse
        LuA.Enabled = Not trueFalse
        MaDe.Enabled = Not trueFalse
        MaA.Enabled = Not trueFalse
        MeDe.Enabled = Not trueFalse
        MeA.Enabled = Not trueFalse
        JeDe.Enabled = Not trueFalse
        JeA.Enabled = Not trueFalse
        VeDe.Enabled = Not trueFalse
        VeA.Enabled = Not trueFalse
        SaDe.Enabled = Not trueFalse
        SaA.Enabled = Not trueFalse

        Approuved.Enabled = Not trueFalse
    End Sub

    Private Sub modifWeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifWeek.Click
        Dim sNoUser() As String = System.Text.RegularExpressions.Regex.Split(Users.Text, " \(")
        Dim noUser As Integer = sNoUser(1).Substring(0, sNoUser(1).Length - 1)

        If ToolTip1.GetToolTip(modifWeek).StartsWith("C") Then
            If lockSecteur("WorkHours-" & noUser, True, "Quarts de travail") = False Then Exit Sub

            oldNoUser = noUser
            lockItems(False)
            Save.Enabled = True
            SaveAs.Enabled = True
            Users.Enabled = False
            modifWeek.Image = imgModifSave.Images(2)
            ToolTip1.SetToolTip(modifWeek, "Arrêter de modifier cet utilisateur")
        Else
            lockSecteur("WorkHours-" & noUser, False, "Quarts de travail")

            lockItems(True)
            Save.Enabled = False
            SaveAs.Enabled = False
            Users.Enabled = True
            modifWeek.Image = imgModifSave.Images(0)
            ToolTip1.SetToolTip(modifWeek, "Commencer à modifier cet utilisateur")
        End If
    End Sub

    Private Sub weeks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Weeks.SelectedIndexChanged
        loadWeek()
    End Sub

    Private Function savingAs() As String
        Dim newDate As Boolean = False
        Dim MySelDate, transitiveDate As Date

        If Weeks.SelectedIndex > 0 Then MySelDate = CDate(oldWeek)
        Dim myDateChoice As New DateChoice()
        Dim dateReturn As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Today.Year + 1, , , , , , True, Date.Today, , , , MySelDate, True)
        If dateReturn.Count = 0 Then Return ""

        transitiveDate = dateReturn(0)

        currentlySaving = True
        oldWeek = DateFormat.getTextDate(transitiveDate.AddDays(transitiveDate.DayOfWeek * -1))

        Dim foundedWeek As Integer = Weeks.FindStringExact(oldWeek)
        If foundedWeek < 0 Then
            Weeks.Items.Add(oldWeek)
            Weeks.SelectedIndex = Weeks.FindStringExact(oldWeek)
            newDate = True
        Else
            If MessageBox.Show("Voulez-vous enregistrer par dessus la semaine du & " & oldWeek & " ?", "Semaine existante", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then currentlySaving = False : Return ""
            Weeks.SelectedIndex = foundedWeek
        End If
        currentlySaving = False

        Return saving(newDate)
    End Function

    Private Function saving(Optional ByVal newDate As Boolean = False) As String
        Dim sNoUser() As String = System.Text.RegularExpressions.Regex.Split(Users.Text, " \(")
        Dim noUser As Integer = sNoUser(1).Substring(0, sNoUser(1).Length - 1)
        Dim verbe As String = "enregistrés"

        If newDate Then
            verbe = "ajoutés"
            DBLinker.getInstance.writeDB("WorkHours", "NoUser,Week,DiDe,DiA,LuDe,LuA,MaDe,MaA,MeDe,MeA,JeDe,JeA,VeDe,VeA,SaDe,SaA,Approuved", noUser & ",'" & oldWeek & "','" & DiDe.Text & "','" & DiA.Text & "','" & LuDe.Text & "','" & LuA.Text & "','" & MaDe.Text & "','" & MaA.Text & "','" & MeDe.Text & "','" & MeA.Text & "','" & JeDe.Text & "','" & JeA.Text & "','" & VeDe.Text & "','" & VeA.Text & "','" & SaDe.Text & "','" & SaA.Text & "','" & Approuved.Checked & "'")
        Else
            DBLinker.getInstance.updateDB("WorkHours", "DiDe='" & DiDe.Text & "',DiA='" & DiA.Text & "',LuDe='" & LuDe.Text & "',LuA='" & LuA.Text & "',MaDe='" & MaDe.Text & "',MaA='" & MaA.Text & "',MeDe='" & MeDe.Text & "',MeA='" & MeA.Text & "',JeDe='" & JeDe.Text & "',JeA='" & JeA.Text & "',VeDe='" & VeDe.Text & "',VeA='" & VeA.Text & "',SaDe='" & SaDe.Text & "',SaA='" & SaA.Text & "',Approuved='" & Approuved.Checked & "'", "NoUser", noUser & " AND Week='" & oldWeek & "'", False)
        End If

        myMainWin.StatusText = "Quarts de travail pour " & Users.Text & " de la semaine du " & oldWeek & " " & verbe

        updatePunch(noUser)
        Return "DONE"
    End Function

    Private Sub save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If oldWeek = "" Then savingAs() : Exit Sub

        saving()
    End Sub

    Private Sub saveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAs.Click
        savingAs()
    End Sub

    Private Sub copyForward_s_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyForward1.Click, CopyForward2.Click, CopyForward3.Click, CopyForward4.Click, CopyForward5.Click, CopyForward6.Click
        With CType(sender, Button)
            Dim CopyingComboBoxDe1, CopyingComboBoxDe2, CopyingComboBoxA1, copyingComboBoxA2 As ComboBox
            CopyingComboBoxDe1 = CType(.Tag, ComboBox)
            CopyingComboBoxA1 = Me.GetNextControl(CopyingComboBoxDe1, True)
            CopyingComboBoxDe2 = Me.GetNextControl(CopyingComboBoxA1, True)
            copyingComboBoxA2 = Me.GetNextControl(CopyingComboBoxDe2, True)

            CopyingComboBoxDe2.SelectedIndex = CopyingComboBoxDe1.SelectedIndex
            copyingComboBoxA2.SelectedIndex = CopyingComboBoxA1.SelectedIndex
        End With
    End Sub
    Private Sub copyBackward_s_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyBackward1.Click, CopyBackward2.Click, CopyBackward3.Click, CopyBackward4.Click, CopyBackward5.Click, CopyBackward6.Click
        With CType(sender, Button)
            Dim CopyingComboBoxDe1, CopyingComboBoxDe2, CopyingComboBoxA1, copyingComboBoxA2 As ComboBox
            CopyingComboBoxDe1 = CType(.Tag, ComboBox)
            CopyingComboBoxA1 = Me.GetNextControl(CopyingComboBoxDe1, True)
            copyingComboBoxA2 = Me.GetNextControl(CopyingComboBoxDe1, False)
            CopyingComboBoxDe2 = Me.GetNextControl(copyingComboBoxA2, False)

            CopyingComboBoxDe2.SelectedIndex = CopyingComboBoxDe1.SelectedIndex
            copyingComboBoxA2.SelectedIndex = CopyingComboBoxA1.SelectedIndex
        End With
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf workHours_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
