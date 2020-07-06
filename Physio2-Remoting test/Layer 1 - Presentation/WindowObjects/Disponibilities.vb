Public Class Disponibilities
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        DDe.SelectedIndex = 0
        LDe.SelectedIndex = 0
        M1De.SelectedIndex = 0
        M2De.SelectedIndex = 0
        JDe.SelectedIndex = 0
        VDe.SelectedIndex = 0
        SDe.SelectedIndex = 0
        DDe.Tag = D
        LDe.Tag = L
        M1De.Tag = M1
        M2De.Tag = M2
        JDe.Tag = J
        VDe.Tag = V
        SDe.Tag = S
        DA.Tag = D
        LA.Tag = L
        M1A.Tag = M1
        M2A.Tag = M2
        JA.Tag = J
        VA.Tag = V
        SA.Tag = S

        Me.selectDate.Image = DrawingManager.getInstance.getImage("selection16.gif")
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents D As System.Windows.Forms.CheckBox
    Friend WithEvents DDe As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DA As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents LA As System.Windows.Forms.ComboBox
    Friend WithEvents L As System.Windows.Forms.CheckBox
    Friend WithEvents LDe As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents M1A As System.Windows.Forms.ComboBox
    Friend WithEvents M1 As System.Windows.Forms.CheckBox
    Friend WithEvents M1De As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents M2A As System.Windows.Forms.ComboBox
    Friend WithEvents M2 As System.Windows.Forms.CheckBox
    Friend WithEvents M2De As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents JA As System.Windows.Forms.ComboBox
    Friend WithEvents J As System.Windows.Forms.CheckBox
    Friend WithEvents JDe As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents VA As System.Windows.Forms.ComboBox
    Friend WithEvents V As System.Windows.Forms.CheckBox
    Friend WithEvents VDe As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents SA As System.Windows.Forms.ComboBox
    Friend WithEvents S As System.Windows.Forms.CheckBox
    Friend WithEvents SDe As System.Windows.Forms.ComboBox
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents annuler As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents DateDebut As System.Windows.Forms.Label
    Friend WithEvents selectDate As System.Windows.Forms.Button
    Friend WithEvents CheckAll As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.DA = New System.Windows.Forms.ComboBox
        Me.D = New System.Windows.Forms.CheckBox
        Me.DDe = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.LA = New System.Windows.Forms.ComboBox
        Me.L = New System.Windows.Forms.CheckBox
        Me.LDe = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.M1A = New System.Windows.Forms.ComboBox
        Me.M1 = New System.Windows.Forms.CheckBox
        Me.M1De = New System.Windows.Forms.ComboBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.M2A = New System.Windows.Forms.ComboBox
        Me.M2 = New System.Windows.Forms.CheckBox
        Me.M2De = New System.Windows.Forms.ComboBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.JA = New System.Windows.Forms.ComboBox
        Me.J = New System.Windows.Forms.CheckBox
        Me.JDe = New System.Windows.Forms.ComboBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.VA = New System.Windows.Forms.ComboBox
        Me.V = New System.Windows.Forms.CheckBox
        Me.VDe = New System.Windows.Forms.ComboBox
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.SA = New System.Windows.Forms.ComboBox
        Me.S = New System.Windows.Forms.CheckBox
        Me.SDe = New System.Windows.Forms.ComboBox
        Me.OK = New System.Windows.Forms.Button
        Me.annuler = New System.Windows.Forms.Button
        Me.CheckAll = New System.Windows.Forms.CheckBox
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.DateDebut = New System.Windows.Forms.Label
        Me.selectDate = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DA)
        Me.GroupBox1.Controls.Add(Me.D)
        Me.GroupBox1.Controls.Add(Me.DDe)
        Me.GroupBox1.Location = New System.Drawing.Point(48, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(88, 72)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'DA
        '
        Me.DA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.DA.Location = New System.Drawing.Point(8, 48)
        Me.DA.Name = "DA"
        Me.DA.Size = New System.Drawing.Size(64, 21)
        Me.DA.TabIndex = 9
        '
        'D
        '
        Me.D.BackColor = System.Drawing.Color.Transparent
        Me.D.Location = New System.Drawing.Point(8, 8)
        Me.D.Name = "D"
        Me.D.Size = New System.Drawing.Size(80, 16)
        Me.D.TabIndex = 1
        Me.D.Text = "Dimanche"
        Me.D.UseVisualStyleBackColor = False
        '
        'DDe
        '
        Me.DDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.DDe.Location = New System.Drawing.Point(8, 24)
        Me.DDe.Name = "DDe"
        Me.DDe.Size = New System.Drawing.Size(64, 21)
        Me.DDe.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "De ->"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "À   ->"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LA)
        Me.GroupBox2.Controls.Add(Me.L)
        Me.GroupBox2.Controls.Add(Me.LDe)
        Me.GroupBox2.Location = New System.Drawing.Point(128, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(88, 72)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'LA
        '
        Me.LA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.LA.Location = New System.Drawing.Point(8, 48)
        Me.LA.Name = "LA"
        Me.LA.Size = New System.Drawing.Size(64, 21)
        Me.LA.TabIndex = 9
        '
        'L
        '
        Me.L.BackColor = System.Drawing.Color.Transparent
        Me.L.Location = New System.Drawing.Point(8, 8)
        Me.L.Name = "L"
        Me.L.Size = New System.Drawing.Size(80, 16)
        Me.L.TabIndex = 1
        Me.L.Text = "Lundi"
        Me.L.UseVisualStyleBackColor = False
        '
        'LDe
        '
        Me.LDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.LDe.Location = New System.Drawing.Point(8, 24)
        Me.LDe.Name = "LDe"
        Me.LDe.Size = New System.Drawing.Size(64, 21)
        Me.LDe.TabIndex = 8
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.M1A)
        Me.GroupBox3.Controls.Add(Me.M1)
        Me.GroupBox3.Controls.Add(Me.M1De)
        Me.GroupBox3.Location = New System.Drawing.Point(208, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(88, 72)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        '
        'M1A
        '
        Me.M1A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.M1A.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.M1A.Location = New System.Drawing.Point(8, 48)
        Me.M1A.Name = "M1A"
        Me.M1A.Size = New System.Drawing.Size(64, 21)
        Me.M1A.TabIndex = 9
        '
        'M1
        '
        Me.M1.BackColor = System.Drawing.Color.Transparent
        Me.M1.Location = New System.Drawing.Point(8, 8)
        Me.M1.Name = "M1"
        Me.M1.Size = New System.Drawing.Size(80, 16)
        Me.M1.TabIndex = 1
        Me.M1.Text = "Mardi"
        Me.M1.UseVisualStyleBackColor = False
        '
        'M1De
        '
        Me.M1De.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.M1De.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.M1De.Location = New System.Drawing.Point(8, 24)
        Me.M1De.Name = "M1De"
        Me.M1De.Size = New System.Drawing.Size(64, 21)
        Me.M1De.TabIndex = 8
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.M2A)
        Me.GroupBox4.Controls.Add(Me.M2)
        Me.GroupBox4.Controls.Add(Me.M2De)
        Me.GroupBox4.Location = New System.Drawing.Point(288, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(88, 72)
        Me.GroupBox4.TabIndex = 12
        Me.GroupBox4.TabStop = False
        '
        'M2A
        '
        Me.M2A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.M2A.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.M2A.Location = New System.Drawing.Point(8, 48)
        Me.M2A.Name = "M2A"
        Me.M2A.Size = New System.Drawing.Size(64, 21)
        Me.M2A.TabIndex = 9
        '
        'M2
        '
        Me.M2.BackColor = System.Drawing.Color.Transparent
        Me.M2.Location = New System.Drawing.Point(8, 8)
        Me.M2.Name = "M2"
        Me.M2.Size = New System.Drawing.Size(80, 16)
        Me.M2.TabIndex = 1
        Me.M2.Text = "Mercredi"
        Me.M2.UseVisualStyleBackColor = False
        '
        'M2De
        '
        Me.M2De.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.M2De.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.M2De.Location = New System.Drawing.Point(8, 24)
        Me.M2De.Name = "M2De"
        Me.M2De.Size = New System.Drawing.Size(64, 21)
        Me.M2De.TabIndex = 8
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.JA)
        Me.GroupBox5.Controls.Add(Me.J)
        Me.GroupBox5.Controls.Add(Me.JDe)
        Me.GroupBox5.Location = New System.Drawing.Point(368, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(96, 72)
        Me.GroupBox5.TabIndex = 13
        Me.GroupBox5.TabStop = False
        '
        'JA
        '
        Me.JA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.JA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.JA.Location = New System.Drawing.Point(8, 48)
        Me.JA.Name = "JA"
        Me.JA.Size = New System.Drawing.Size(64, 21)
        Me.JA.TabIndex = 9
        '
        'J
        '
        Me.J.BackColor = System.Drawing.Color.Transparent
        Me.J.Location = New System.Drawing.Point(8, 8)
        Me.J.Name = "J"
        Me.J.Size = New System.Drawing.Size(80, 16)
        Me.J.TabIndex = 1
        Me.J.Text = "Jeudi"
        Me.J.UseVisualStyleBackColor = False
        '
        'JDe
        '
        Me.JDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.JDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.JDe.Location = New System.Drawing.Point(8, 24)
        Me.JDe.Name = "JDe"
        Me.JDe.Size = New System.Drawing.Size(64, 21)
        Me.JDe.TabIndex = 8
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.VA)
        Me.GroupBox6.Controls.Add(Me.V)
        Me.GroupBox6.Controls.Add(Me.VDe)
        Me.GroupBox6.Controls.Add(Me.GroupBox8)
        Me.GroupBox6.Location = New System.Drawing.Point(448, 0)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(88, 72)
        Me.GroupBox6.TabIndex = 14
        Me.GroupBox6.TabStop = False
        '
        'VA
        '
        Me.VA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.VA.Location = New System.Drawing.Point(8, 48)
        Me.VA.Name = "VA"
        Me.VA.Size = New System.Drawing.Size(64, 21)
        Me.VA.TabIndex = 9
        '
        'V
        '
        Me.V.BackColor = System.Drawing.Color.Transparent
        Me.V.Location = New System.Drawing.Point(8, 8)
        Me.V.Name = "V"
        Me.V.Size = New System.Drawing.Size(80, 16)
        Me.V.TabIndex = 1
        Me.V.Text = "Vendredi"
        Me.V.UseVisualStyleBackColor = False
        '
        'VDe
        '
        Me.VDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.VDe.Location = New System.Drawing.Point(8, 24)
        Me.VDe.Name = "VDe"
        Me.VDe.Size = New System.Drawing.Size(64, 21)
        Me.VDe.TabIndex = 8
        '
        'GroupBox8
        '
        Me.GroupBox8.Location = New System.Drawing.Point(80, 0)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(80, 72)
        Me.GroupBox8.TabIndex = 15
        Me.GroupBox8.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.SA)
        Me.GroupBox7.Controls.Add(Me.S)
        Me.GroupBox7.Controls.Add(Me.SDe)
        Me.GroupBox7.Location = New System.Drawing.Point(528, 0)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(80, 72)
        Me.GroupBox7.TabIndex = 15
        Me.GroupBox7.TabStop = False
        '
        'SA
        '
        Me.SA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SA.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.SA.Location = New System.Drawing.Point(8, 48)
        Me.SA.Name = "SA"
        Me.SA.Size = New System.Drawing.Size(64, 21)
        Me.SA.TabIndex = 9
        '
        'S
        '
        Me.S.BackColor = System.Drawing.Color.Transparent
        Me.S.Location = New System.Drawing.Point(8, 8)
        Me.S.Name = "S"
        Me.S.Size = New System.Drawing.Size(80, 16)
        Me.S.TabIndex = 1
        Me.S.Text = "Samedi"
        Me.S.UseVisualStyleBackColor = False
        '
        'SDe
        '
        Me.SDe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SDe.Items.AddRange(New Object() {"--:--", "06:00", "06:15", "06:30", "06:45", "07:00", "07:15", "07:30", "07:45", "08:00", "08:15", "08:30", "08:45", "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45", "14:00", "14:15", "14:30", "14:45", "15:00", "15:15", "15:30", "15:45", "16:00", "16:15", "16:30", "16:45", "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15", "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00", "23:15", "23:30", "23:45"})
        Me.SDe.Location = New System.Drawing.Point(8, 24)
        Me.SDe.Name = "SDe"
        Me.SDe.Size = New System.Drawing.Size(64, 21)
        Me.SDe.TabIndex = 8
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(712, 8)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(56, 24)
        Me.OK.TabIndex = 16
        Me.OK.Text = "OK"
        '
        'annuler
        '
        Me.annuler.Location = New System.Drawing.Point(712, 48)
        Me.annuler.Name = "annuler"
        Me.annuler.Size = New System.Drawing.Size(56, 24)
        Me.annuler.TabIndex = 17
        Me.annuler.Text = "Annuler"
        '
        'CheckAll
        '
        Me.CheckAll.Location = New System.Drawing.Point(24, 8)
        Me.CheckAll.Name = "CheckAll"
        Me.CheckAll.Size = New System.Drawing.Size(16, 16)
        Me.CheckAll.TabIndex = 18
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.DateDebut)
        Me.GroupBox9.Controls.Add(Me.selectDate)
        Me.GroupBox9.Location = New System.Drawing.Point(606, 0)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(92, 72)
        Me.GroupBox9.TabIndex = 19
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Date de début"
        '
        'DateDebut
        '
        Me.DateDebut.AutoSize = True
        Me.DateDebut.Location = New System.Drawing.Point(14, 47)
        Me.DateDebut.Name = "DateDebut"
        Me.DateDebut.Size = New System.Drawing.Size(65, 13)
        Me.DateDebut.TabIndex = 17
        Me.DateDebut.Text = ""
        '
        'selectDate
        '
        Me.selectDate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.selectDate.Location = New System.Drawing.Point(8, 18)
        Me.selectDate.Name = "selectDate"
        Me.selectDate.Size = New System.Drawing.Size(78, 19)
        Me.selectDate.TabIndex = 16
        '
        'Disponibilites
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(780, 80)
        Me.Controls.Add(Me.CheckAll)
        Me.Controls.Add(Me.annuler)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox9)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Disponibilites"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Disponibilités du client"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private checking As Boolean = False
    Private answer As String
    Private loaded As Boolean = False

    Public ReadOnly Property GetDisponisibilites(Optional ByVal curDisponibilites As String = "", Optional ByVal DisabledItems As Boolean = False) As String
        Get
            Dim myDispo() As String = curDisponibilites.Split(New Char() {";"})
            If curDisponibilites <> "" Then
                If myDispo.Length >= 14 Then
                    If DisabledItems = True Then disableItems(Me)

                    changeTime(DDe, myDispo(0))
                    changeTime(DA, myDispo(1))
                    changeTime(LDe, myDispo(2))
                    changeTime(LA, myDispo(3))
                    changeTime(M1De, myDispo(4))
                    changeTime(M1A, myDispo(5))
                    changeTime(M2De, myDispo(6))
                    changeTime(M2A, myDispo(7))
                    changeTime(JDe, myDispo(8))
                    changeTime(JA, myDispo(9))
                    changeTime(VDe, myDispo(10))
                    changeTime(VA, myDispo(11))
                    changeTime(SDe, myDispo(12))
                    changeTime(SA, myDispo(13))

                    If myDispo.Length = 15 Then DateDebut.Text = myDispo(14)
                End If
            End If

            loaded = True
            Me.ShowDialog()
            Return answer
        End Get
    End Property

    Private Sub checkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckAll.CheckedChanged
        If checking = False Then
            checking = True
            D.Checked = sender.Checked
            L.Checked = sender.Checked
            M1.Checked = sender.Checked
            M2.Checked = sender.Checked
            J.Checked = sender.Checked
            V.Checked = sender.Checked
            S.Checked = sender.Checked
            checking = False
        End If
    End Sub

    Private Sub checkedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles D.CheckedChanged, L.CheckedChanged, M1.CheckedChanged, M2.CheckedChanged, J.CheckedChanged, V.CheckedChanged, S.CheckedChanged
        If checking = False Then
            checking = True
            If D.Checked = True And L.Checked = True And M1.Checked = True And M2.Checked = True And J.Checked = True And V.Checked = True And S.Checked = True Then
                CheckAll.Checked = True
            Else
                CheckAll.Checked = False
            End If
            checking = False
        End If
    End Sub

    Private copyingTime As Boolean = False

    Private Sub selectedIndexChange(ByVal sender As Object, ByVal e As EventArgs) Handles DDe.SelectedIndexChanged, DA.SelectedIndexChanged, LDe.SelectedIndexChanged, LA.SelectedIndexChanged, M1De.SelectedIndexChanged, M1A.SelectedIndexChanged, M2De.SelectedIndexChanged, M2A.SelectedIndexChanged, JDe.SelectedIndexChanged, JA.SelectedIndexChanged, VDe.SelectedIndexChanged, VA.SelectedIndexChanged, SDe.SelectedIndexChanged, SA.SelectedIndexChanged
        If copyingTime Then Exit Sub

        With CType(sender, ComboBox)
            Dim controlName As String = ""
            Dim DeIndex, aIndex As Integer
            If loaded = True Then
                If .Name.ToUpper.EndsWith("DE") Then
                    controlName = .Name.Substring(0, .Name.Length - 2)
                    DeIndex = .SelectedIndex
                    aIndex = CType(.Parent.Controls(controlName & "A"), ComboBox).SelectedIndex
                Else
                    controlName = .Name.Substring(0, .Name.Length - 1)
                    aIndex = .SelectedIndex
                    DeIndex = CType(.Parent.Controls(controlName & "De"), ComboBox).SelectedIndex
                End If

                Dim dayCheck As CheckBox = .Parent.Controls(controlName)
                dayCheck.Checked = True
            End If

            If .SelectedIndex = 0 Then
                Dim i As Integer
                For i = 0 To .Parent.Controls.Count - 1
                    If .Parent.Controls(i).GetType.Name.ToUpper = "COMBOBOX" Then
                        CType(.Parent.Controls(i), ComboBox).SelectedIndex = 0
                    End If
                Next i
            Else
                copyingTime = True
                If .Name.ToUpper.EndsWith("DE") Then
                    deSelectedIndexChange(sender)
                Else
                    aSelectedIndexChange(sender)
                End If
                copyingTime = False
            End If

            If controlName <> "" AndAlso Form.ModifierKeys = Keys.Control Then
                DeIndex = CType(.Parent.Controls(controlName & "De"), ComboBox).SelectedIndex
                aIndex = CType(.Parent.Controls(controlName & "A"), ComboBox).SelectedIndex
                copyingTime = True
                For Each curContainer As Control In Me.Controls
                    If TypeOf curContainer Is GroupBox And curContainer IsNot .Parent Then
                        changeDayTime(curContainer, DeIndex, aIndex)
                    End If
                Next
                copyingTime = False
            End If
        End With
    End Sub

    Private Sub changeDayTime(ByVal curContainer As GroupBox, ByVal deIndex As Integer, ByVal aIndex As Integer)
        Dim deControl As ComboBox = Nothing
        Dim aControl As ComboBox = Nothing
        Dim checkControl As CheckBox = Nothing
        For Each curControl As Control In curContainer.Controls
            If curControl.Name.EndsWith("De") Then
                deControl = curControl
            ElseIf curControl.Name.EndsWith("A") Then
                aControl = curControl
            Else
                If TypeOf curControl Is GroupBox Then
                    changeDayTime(curControl, deIndex, aIndex)
                Else
                    If TypeOf curControl Is CheckBox Then checkControl = curControl
                End If
            End If
        Next

        If checkControl IsNot Nothing AndAlso checkControl.Checked Then
            If deControl IsNot Nothing Then deControl.SelectedIndex = deIndex
            If aControl IsNot Nothing Then aControl.SelectedIndex = aIndex
        End If
    End Sub

    Private Sub deSelectedIndexChange(ByVal sender As Object)
        With CType(sender, ComboBox)
            Dim i As Integer
            For i = 0 To .Parent.Controls.Count - 1
                With .Parent
                    If .Controls(i).GetType.Name.ToUpper = "COMBOBOX" And .Controls(i).Name.ToUpper.EndsWith("DE") = False Then
                        With CType(.Controls(i), ComboBox)
                            Try
                                If .SelectedItem.ToString <> "--:--" AndAlso time1Suptime2(CDate(sender.selecteditem), CDate(.SelectedItem)) Then
                                    .SelectedIndex = sender.selectedindex
                                End If

                                If .SelectedItem.ToString = "--:--" Then .SelectedIndex = sender.selectedindex
                            Catch
                                .SelectedIndex = sender.selectedindex
                            End Try
                        End With
                    End If
                End With
            Next i
        End With
    End Sub

    Private Sub aSelectedIndexChange(ByVal sender As Object)
        With CType(sender, ComboBox)
            Dim i As Integer
            For i = 0 To .Parent.Controls.Count - 1
                With .Parent
                    If .Controls(i).GetType.Name.ToUpper = "COMBOBOX" And .Controls(i).Name.ToUpper.EndsWith("A") = False Then
                        With CType(.Controls(i), ComboBox)
                            If .SelectedItem.ToString <> "--:--" AndAlso time1Suptime2(CDate(sender.selecteditem), CDate(.SelectedItem), True) = False Then
                                .SelectedIndex = sender.selectedindex
                            End If

                            If .SelectedItem.ToString = "--:--" Then .SelectedIndex = sender.selectedindex
                        End With
                    End If
                End With
            Next i
        End With
    End Sub

    Private Sub disponibilites_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If DateDebut.Text = "" Then DateDebut.Text = DateFormat.getTextDate(Date.Today)
        Me.Top = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Me.Height) / 2
        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Me.Width) / 2
    End Sub

    Private Sub disponibilites_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub annuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles annuler.Click
        Me.Close()
    End Sub

    Private Sub disableItems(ByVal myObj As Object)
        Dim i As Byte
        For i = 0 To myObj.controls.count - 1
            If myObj.GetType.Name.ToUpper.StartsWith("GROUP") Then
                disableItems(myObj.controls(i))
            Else
                myObj.controls(i).enabled = False
            End If
        Next i
    End Sub

    Private Sub changeTime(ByVal myObj As ComboBox, ByVal myTime As String)
        If myTime = "" Then Exit Sub
        With myObj
            .SelectedIndex = .FindStringExact(myTime)
            If .SelectedIndex < 0 Then
                CType(.Tag, CheckBox).Checked = False
                .SelectedIndex = 0
            Else
                CType(.Tag, CheckBox).Checked = True
            End If
        End With
    End Sub

    Private Sub ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If D.Checked = False And L.Checked = False And M1.Checked = False And M2.Checked = False And J.Checked = False And V.Checked = False And S.Checked = False Then MessageBox.Show("Veuillez sélectionner au moins une journée de disponibilité", "Information manquante") : Exit Sub

        loaded = False

        selectedIndexChange(DDe, EventArgs.Empty)
        selectedIndexChange(DA, EventArgs.Empty)
        selectedIndexChange(LDe, EventArgs.Empty)
        selectedIndexChange(LA, EventArgs.Empty)
        selectedIndexChange(M1De, EventArgs.Empty)
        selectedIndexChange(M1A, EventArgs.Empty)
        selectedIndexChange(M2De, EventArgs.Empty)
        selectedIndexChange(M2A, EventArgs.Empty)
        selectedIndexChange(JDe, EventArgs.Empty)
        selectedIndexChange(JA, EventArgs.Empty)
        selectedIndexChange(VDe, EventArgs.Empty)
        selectedIndexChange(VA, EventArgs.Empty)
        selectedIndexChange(SDe, EventArgs.Empty)
        selectedIndexChange(SA, EventArgs.Empty)

        If D.Checked = True Then
            answer &= ";" & DDe.SelectedItem
            answer &= ";" & DA.SelectedItem
        Else
            answer &= ";;"
        End If
        If L.Checked = True Then
            answer &= ";" & LDe.SelectedItem
            answer &= ";" & LA.SelectedItem
        Else
            answer &= ";;"
        End If
        If M1.Checked = True Then
            answer &= ";" & M1De.SelectedItem
            answer &= ";" & M1A.SelectedItem
        Else
            answer &= ";;"
        End If
        If M2.Checked = True Then
            answer &= ";" & M2De.SelectedItem
            answer &= ";" & M2A.SelectedItem
        Else
            answer &= ";;"
        End If
        If J.Checked = True Then
            answer &= ";" & JDe.SelectedItem
            answer &= ";" & JA.SelectedItem
        Else
            answer &= ";;"
        End If
        If V.Checked = True Then
            answer &= ";" & VDe.SelectedItem
            answer &= ";" & VA.SelectedItem
        Else
            answer &= ";;"
        End If
        If S.Checked = True Then
            answer &= ";" & SDe.SelectedItem
            answer &= ";" & SA.SelectedItem
        Else
            answer &= ";;"
        End If

        answer &= ";" & DateDebut.Text

        answer = answer.Substring(1)
        Me.Close()
    End Sub

    Private Sub selectDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectDate.Click
        Dim myDateChoice As New DateChoice
        Dim newDate As Generic.List(Of Date) = myDateChoice.choose(Date.Today.Year, Date.Today.Year + 1, , , , , , , Date.Today, , , , CDate(DateDebut.Text))
        If newDate.Count <> 0 Then DateDebut.Text = DateFormat.getTextDate(newDate(0))
    End Sub
End Class
