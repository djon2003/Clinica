Option Strict Off
Option Explicit On
Friend Class typesuser
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New(ByVal myFrom As Form)
        MyBase.New()
        from = myFrom

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        linkdroitaccesArray()
        Me.GroupAutre.Height -= 22
        Me.MdiParent = myMainWin

        'Chargement des images
        Me.add_Renamed.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.renommer.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("rename16.ico"), New Size(16, 16))
        Me.modif.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.enlever.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            For Each CurPanel As Control In Me.DAPanel.Controls
                For Each CurControl As Control In CurPanel.Controls
                    If CurControl.Name.StartsWith("_droitacces_") Then RemoveHandler CType(CurControl, CheckBox).CheckedChanged, AddressOf alLdroitacces_CheckedChanged
                Next
            Next

            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents therapist As System.Windows.Forms.CheckBox
    Public WithEvents ok As System.Windows.Forms.Button
    Public WithEvents modif As System.Windows.Forms.Button
    Public WithEvents add_Renamed As System.Windows.Forms.Button
    Public WithEvents typeu As System.Windows.Forms.ComboBox
    Public WithEvents _droitacces_4 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_3 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_2 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_0 As System.Windows.Forms.CheckBox
    Public WithEvents droitacces As BaseObjArray
    Public WithEvents label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents enlever As System.Windows.Forms.Button
    Public WithEvents frameDroitAcces As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents DAPanel As System.Windows.Forms.Panel
    Friend WithEvents GroupDB As System.Windows.Forms.GroupBox
    Friend WithEvents GroupAgenda As System.Windows.Forms.GroupBox
    Friend WithEvents GroupClient As System.Windows.Forms.GroupBox
    Friend WithEvents GroupRV_QL As System.Windows.Forms.GroupBox
    Public WithEvents _droitacces_8 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_7 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_6 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_1 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_5 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupKP As System.Windows.Forms.GroupBox
    Public WithEvents _droitacces_14 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_13 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_12 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_11 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_10 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_16 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_15 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupGestion As System.Windows.Forms.GroupBox
    Public WithEvents _droitacces_19 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_18 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_17 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_24 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_26 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_23 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_21 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_20 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_22 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_9 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupRapport As System.Windows.Forms.GroupBox
    Friend WithEvents GroupMessagerie As System.Windows.Forms.GroupBox
    Public WithEvents _droitacces_28 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_31 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_32 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_30 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_29 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_50 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_49 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_48 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_47 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_45 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_33 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupUser As System.Windows.Forms.GroupBox
    Public WithEvents _droitacces_51 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_35 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_39 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_44 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_38 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_37 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_36 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_34 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_42 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_40 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_41 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_43 As System.Windows.Forms.CheckBox
    Public WithEvents selectAllDA As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_52 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_25 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_56 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_57 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_55 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_58 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupAutre As System.Windows.Forms.GroupBox
    Public WithEvents _droitacces_60 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_63 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_64 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_66 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_65 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_67 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_68 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_73 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_74 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_72 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_70 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_69 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_71 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_75 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupClinique As System.Windows.Forms.GroupBox
    Public WithEvents _droitacces_62 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_27 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_54 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_59 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_53 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_84 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_83 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_82 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_76 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_77 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_81 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_78 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_79 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_85 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_80 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_46 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_87 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_86 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_88 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_91 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_90 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_89 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_92 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_93 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_96 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_94 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_95 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_98 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_97 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_99 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_101 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_100 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_102 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_103 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_104 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_105 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_106 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_107 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_108 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_109 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_61 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_110 As System.Windows.Forms.CheckBox
    Public WithEvents _droitacces_111 As System.Windows.Forms.CheckBox
    Public WithEvents renommer As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.therapist = New System.Windows.Forms.CheckBox
        Me.ok = New System.Windows.Forms.Button
        Me.modif = New System.Windows.Forms.Button
        Me.add_Renamed = New System.Windows.Forms.Button
        Me.typeu = New System.Windows.Forms.ComboBox
        Me.frameDroitAcces = New System.Windows.Forms.GroupBox
        Me.selectAllDA = New System.Windows.Forms.CheckBox
        Me.DAPanel = New System.Windows.Forms.Panel
        Me.GroupUser = New System.Windows.Forms.GroupBox
        Me._droitacces_81 = New System.Windows.Forms.CheckBox
        Me._droitacces_92 = New System.Windows.Forms.CheckBox
        Me._droitacces_93 = New System.Windows.Forms.CheckBox
        Me._droitacces_79 = New System.Windows.Forms.CheckBox
        Me._droitacces_78 = New System.Windows.Forms.CheckBox
        Me._droitacces_51 = New System.Windows.Forms.CheckBox
        Me._droitacces_45 = New System.Windows.Forms.CheckBox
        Me._droitacces_49 = New System.Windows.Forms.CheckBox
        Me._droitacces_47 = New System.Windows.Forms.CheckBox
        Me._droitacces_50 = New System.Windows.Forms.CheckBox
        Me._droitacces_46 = New System.Windows.Forms.CheckBox
        Me._droitacces_48 = New System.Windows.Forms.CheckBox
        Me.GroupRapport = New System.Windows.Forms.GroupBox
        Me._droitacces_71 = New System.Windows.Forms.CheckBox
        Me._droitacces_70 = New System.Windows.Forms.CheckBox
        Me._droitacces_69 = New System.Windows.Forms.CheckBox
        Me.GroupMessagerie = New System.Windows.Forms.GroupBox
        Me._droitacces_35 = New System.Windows.Forms.CheckBox
        Me._droitacces_43 = New System.Windows.Forms.CheckBox
        Me._droitacces_42 = New System.Windows.Forms.CheckBox
        Me._droitacces_107 = New System.Windows.Forms.CheckBox
        Me._droitacces_40 = New System.Windows.Forms.CheckBox
        Me._droitacces_41 = New System.Windows.Forms.CheckBox
        Me._droitacces_39 = New System.Windows.Forms.CheckBox
        Me._droitacces_44 = New System.Windows.Forms.CheckBox
        Me._droitacces_68 = New System.Windows.Forms.CheckBox
        Me._droitacces_38 = New System.Windows.Forms.CheckBox
        Me._droitacces_37 = New System.Windows.Forms.CheckBox
        Me._droitacces_36 = New System.Windows.Forms.CheckBox
        Me._droitacces_102 = New System.Windows.Forms.CheckBox
        Me._droitacces_101 = New System.Windows.Forms.CheckBox
        Me._droitacces_100 = New System.Windows.Forms.CheckBox
        Me._droitacces_34 = New System.Windows.Forms.CheckBox
        Me.GroupGestion = New System.Windows.Forms.GroupBox
        Me._droitacces_106 = New System.Windows.Forms.CheckBox
        Me._droitacces_105 = New System.Windows.Forms.CheckBox
        Me._droitacces_28 = New System.Windows.Forms.CheckBox
        Me._droitacces_108 = New System.Windows.Forms.CheckBox
        Me._droitacces_103 = New System.Windows.Forms.CheckBox
        Me._droitacces_72 = New System.Windows.Forms.CheckBox
        Me._droitacces_66 = New System.Windows.Forms.CheckBox
        Me._droitacces_65 = New System.Windows.Forms.CheckBox
        Me._droitacces_33 = New System.Windows.Forms.CheckBox
        Me._droitacces_31 = New System.Windows.Forms.CheckBox
        Me._droitacces_32 = New System.Windows.Forms.CheckBox
        Me._droitacces_30 = New System.Windows.Forms.CheckBox
        Me._droitacces_29 = New System.Windows.Forms.CheckBox
        Me.GroupKP = New System.Windows.Forms.GroupBox
        Me._droitacces_24 = New System.Windows.Forms.CheckBox
        Me._droitacces_52 = New System.Windows.Forms.CheckBox
        Me._droitacces_77 = New System.Windows.Forms.CheckBox
        Me._droitacces_26 = New System.Windows.Forms.CheckBox
        Me._droitacces_23 = New System.Windows.Forms.CheckBox
        Me._droitacces_21 = New System.Windows.Forms.CheckBox
        Me._droitacces_20 = New System.Windows.Forms.CheckBox
        Me._droitacces_22 = New System.Windows.Forms.CheckBox
        Me.GroupRV_QL = New System.Windows.Forms.GroupBox
        Me._droitacces_88 = New System.Windows.Forms.CheckBox
        Me._droitacces_19 = New System.Windows.Forms.CheckBox
        Me._droitacces_18 = New System.Windows.Forms.CheckBox
        Me._droitacces_17 = New System.Windows.Forms.CheckBox
        Me._droitacces_58 = New System.Windows.Forms.CheckBox
        Me._droitacces_75 = New System.Windows.Forms.CheckBox
        Me._droitacces_16 = New System.Windows.Forms.CheckBox
        Me.GroupClient = New System.Windows.Forms.GroupBox
        Me._droitacces_104 = New System.Windows.Forms.CheckBox
        Me._droitacces_76 = New System.Windows.Forms.CheckBox
        Me._droitacces_63 = New System.Windows.Forms.CheckBox
        Me._droitacces_15 = New System.Windows.Forms.CheckBox
        Me._droitacces_56 = New System.Windows.Forms.CheckBox
        Me._droitacces_89 = New System.Windows.Forms.CheckBox
        Me._droitacces_57 = New System.Windows.Forms.CheckBox
        Me._droitacces_14 = New System.Windows.Forms.CheckBox
        Me._droitacces_25 = New System.Windows.Forms.CheckBox
        Me._droitacces_13 = New System.Windows.Forms.CheckBox
        Me._droitacces_67 = New System.Windows.Forms.CheckBox
        Me._droitacces_55 = New System.Windows.Forms.CheckBox
        Me._droitacces_12 = New System.Windows.Forms.CheckBox
        Me._droitacces_110 = New System.Windows.Forms.CheckBox
        Me._droitacces_11 = New System.Windows.Forms.CheckBox
        Me._droitacces_10 = New System.Windows.Forms.CheckBox
        Me._droitacces_9 = New System.Windows.Forms.CheckBox
        Me.GroupClinique = New System.Windows.Forms.GroupBox
        Me._droitacces_80 = New System.Windows.Forms.CheckBox
        Me._droitacces_85 = New System.Windows.Forms.CheckBox
        Me._droitacces_83 = New System.Windows.Forms.CheckBox
        Me._droitacces_82 = New System.Windows.Forms.CheckBox
        Me._droitacces_62 = New System.Windows.Forms.CheckBox
        Me._droitacces_27 = New System.Windows.Forms.CheckBox
        Me.GroupDB = New System.Windows.Forms.GroupBox
        Me._droitacces_99 = New System.Windows.Forms.CheckBox
        Me._droitacces_8 = New System.Windows.Forms.CheckBox
        Me._droitacces_7 = New System.Windows.Forms.CheckBox
        Me._droitacces_6 = New System.Windows.Forms.CheckBox
        Me._droitacces_1 = New System.Windows.Forms.CheckBox
        Me._droitacces_97 = New System.Windows.Forms.CheckBox
        Me._droitacces_98 = New System.Windows.Forms.CheckBox
        Me._droitacces_96 = New System.Windows.Forms.CheckBox
        Me._droitacces_5 = New System.Windows.Forms.CheckBox
        Me._droitacces_2 = New System.Windows.Forms.CheckBox
        Me._droitacces_94 = New System.Windows.Forms.CheckBox
        Me._droitacces_95 = New System.Windows.Forms.CheckBox
        Me._droitacces_3 = New System.Windows.Forms.CheckBox
        Me._droitacces_4 = New System.Windows.Forms.CheckBox
        Me.GroupAgenda = New System.Windows.Forms.GroupBox
        Me._droitacces_73 = New System.Windows.Forms.CheckBox
        Me._droitacces_87 = New System.Windows.Forms.CheckBox
        Me._droitacces_74 = New System.Windows.Forms.CheckBox
        Me._droitacces_0 = New System.Windows.Forms.CheckBox
        Me.GroupAutre = New System.Windows.Forms.GroupBox
        Me._droitacces_61 = New System.Windows.Forms.CheckBox
        Me._droitacces_54 = New System.Windows.Forms.CheckBox
        Me._droitacces_86 = New System.Windows.Forms.CheckBox
        Me._droitacces_109 = New System.Windows.Forms.CheckBox
        Me._droitacces_91 = New System.Windows.Forms.CheckBox
        Me._droitacces_90 = New System.Windows.Forms.CheckBox
        Me._droitacces_84 = New System.Windows.Forms.CheckBox
        Me._droitacces_59 = New System.Windows.Forms.CheckBox
        Me._droitacces_53 = New System.Windows.Forms.CheckBox
        Me._droitacces_64 = New System.Windows.Forms.CheckBox
        Me._droitacces_60 = New System.Windows.Forms.CheckBox
        Me.label1 = New System.Windows.Forms.Label
        Me.enlever = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.renommer = New System.Windows.Forms.Button
        Me._droitacces_111 = New System.Windows.Forms.CheckBox
        Me.frameDroitAcces.SuspendLayout()
        Me.DAPanel.SuspendLayout()
        Me.GroupUser.SuspendLayout()
        Me.GroupRapport.SuspendLayout()
        Me.GroupMessagerie.SuspendLayout()
        Me.GroupGestion.SuspendLayout()
        Me.GroupKP.SuspendLayout()
        Me.GroupRV_QL.SuspendLayout()
        Me.GroupClient.SuspendLayout()
        Me.GroupClinique.SuspendLayout()
        Me.GroupDB.SuspendLayout()
        Me.GroupAgenda.SuspendLayout()
        Me.GroupAutre.SuspendLayout()
        Me.SuspendLayout()
        '
        'therapist
        '
        Me.therapist.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.therapist.BackColor = System.Drawing.SystemColors.Control
        Me.therapist.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.therapist.Cursor = System.Windows.Forms.Cursors.Default
        Me.therapist.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.therapist.ForeColor = System.Drawing.SystemColors.ControlText
        Me.therapist.Location = New System.Drawing.Point(172, 32)
        Me.therapist.Name = "therapist"
        Me.therapist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.therapist.Size = New System.Drawing.Size(152, 17)
        Me.therapist.TabIndex = 12
        Me.therapist.Text = "Type prennant des clients"
        Me.therapist.UseVisualStyleBackColor = False
        '
        'ok
        '
        Me.ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ok.BackColor = System.Drawing.SystemColors.Control
        Me.ok.Cursor = System.Windows.Forms.Cursors.Default
        Me.ok.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ok.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ok.Location = New System.Drawing.Point(376, 8)
        Me.ok.Name = "ok"
        Me.ok.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ok.Size = New System.Drawing.Size(112, 24)
        Me.ok.TabIndex = 7
        Me.ok.Text = "OK"
        Me.ok.UseVisualStyleBackColor = False
        Me.ok.Visible = False
        '
        'modif
        '
        Me.modif.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.modif.BackColor = System.Drawing.SystemColors.Control
        Me.modif.Cursor = System.Windows.Forms.Cursors.Default
        Me.modif.Enabled = False
        Me.modif.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modif.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modif.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modif.Location = New System.Drawing.Point(400, 8)
        Me.modif.Name = "modif"
        Me.modif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modif.Size = New System.Drawing.Size(24, 24)
        Me.modif.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.modif, "Enregistrer le type en cours")
        Me.modif.UseVisualStyleBackColor = False
        '
        'add_Renamed
        '
        Me.add_Renamed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.add_Renamed.BackColor = System.Drawing.SystemColors.Control
        Me.add_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.add_Renamed.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.add_Renamed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.add_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.add_Renamed.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.add_Renamed.Location = New System.Drawing.Point(368, 8)
        Me.add_Renamed.Name = "add_Renamed"
        Me.add_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.add_Renamed.Size = New System.Drawing.Size(24, 24)
        Me.add_Renamed.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.add_Renamed, "Ajout d'un type")
        Me.add_Renamed.UseVisualStyleBackColor = False
        '
        'typeu
        '
        Me.typeu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.typeu.BackColor = System.Drawing.SystemColors.Window
        Me.typeu.Cursor = System.Windows.Forms.Cursors.Default
        Me.typeu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.typeu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.typeu.ForeColor = System.Drawing.SystemColors.WindowText
        Me.typeu.Location = New System.Drawing.Point(48, 8)
        Me.typeu.Name = "typeu"
        Me.typeu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.typeu.Size = New System.Drawing.Size(310, 22)
        Me.typeu.Sorted = True
        Me.typeu.TabIndex = 3
        '
        'frameDroitAcces
        '
        Me.frameDroitAcces.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.frameDroitAcces.BackColor = System.Drawing.SystemColors.Control
        Me.frameDroitAcces.Controls.Add(Me.selectAllDA)
        Me.frameDroitAcces.Controls.Add(Me.DAPanel)
        Me.frameDroitAcces.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameDroitAcces.ForeColor = System.Drawing.SystemColors.Control
        Me.frameDroitAcces.Location = New System.Drawing.Point(8, 48)
        Me.frameDroitAcces.Name = "frameDroitAcces"
        Me.frameDroitAcces.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameDroitAcces.Size = New System.Drawing.Size(481, 366)
        Me.frameDroitAcces.TabIndex = 0
        Me.frameDroitAcces.TabStop = False
        Me.frameDroitAcces.Text = "Droits et accès"
        '
        'selectAllDA
        '
        Me.selectAllDA.AutoSize = True
        Me.selectAllDA.BackColor = System.Drawing.Color.Transparent
        Me.selectAllDA.Cursor = System.Windows.Forms.Cursors.Default
        Me.selectAllDA.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectAllDA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.selectAllDA.Location = New System.Drawing.Point(6, -2)
        Me.selectAllDA.Name = "selectAllDA"
        Me.selectAllDA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.selectAllDA.Size = New System.Drawing.Size(108, 18)
        Me.selectAllDA.TabIndex = 1
        Me.selectAllDA.Text = "Droits et accès"
        Me.selectAllDA.UseVisualStyleBackColor = False
        '
        'DAPanel
        '
        Me.DAPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DAPanel.AutoScroll = True
        Me.DAPanel.AutoScrollMargin = New System.Drawing.Size(2, 0)
        Me.DAPanel.Controls.Add(Me.GroupUser)
        Me.DAPanel.Controls.Add(Me.GroupRapport)
        Me.DAPanel.Controls.Add(Me.GroupMessagerie)
        Me.DAPanel.Controls.Add(Me.GroupGestion)
        Me.DAPanel.Controls.Add(Me.GroupKP)
        Me.DAPanel.Controls.Add(Me.GroupRV_QL)
        Me.DAPanel.Controls.Add(Me.GroupClient)
        Me.DAPanel.Controls.Add(Me.GroupClinique)
        Me.DAPanel.Controls.Add(Me.GroupDB)
        Me.DAPanel.Controls.Add(Me.GroupAgenda)
        Me.DAPanel.Controls.Add(Me.GroupAutre)
        Me.DAPanel.Location = New System.Drawing.Point(6, 16)
        Me.DAPanel.Name = "DAPanel"
        Me.DAPanel.Size = New System.Drawing.Size(469, 344)
        Me.DAPanel.TabIndex = 12
        '
        'GroupUser
        '
        Me.GroupUser.Controls.Add(Me._droitacces_81)
        Me.GroupUser.Controls.Add(Me._droitacces_92)
        Me.GroupUser.Controls.Add(Me._droitacces_93)
        Me.GroupUser.Controls.Add(Me._droitacces_79)
        Me.GroupUser.Controls.Add(Me._droitacces_78)
        Me.GroupUser.Controls.Add(Me._droitacces_51)
        Me.GroupUser.Controls.Add(Me._droitacces_45)
        Me.GroupUser.Controls.Add(Me._droitacces_49)
        Me.GroupUser.Controls.Add(Me._droitacces_47)
        Me.GroupUser.Controls.Add(Me._droitacces_50)
        Me.GroupUser.Controls.Add(Me._droitacces_46)
        Me.GroupUser.Controls.Add(Me._droitacces_48)
        Me.GroupUser.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupUser.Location = New System.Drawing.Point(0, 2075)
        Me.GroupUser.Name = "GroupUser"
        Me.GroupUser.Size = New System.Drawing.Size(452, 236)
        Me.GroupUser.TabIndex = 19
        Me.GroupUser.TabStop = False
        Me.GroupUser.Text = "Utilisateur"
        '
        '_droitacces_81
        '
        Me._droitacces_81.AutoSize = True
        Me._droitacces_81.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_81.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_81.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_81.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_81.Location = New System.Drawing.Point(6, 91)
        Me._droitacces_81.Name = "_droitacces_81"
        Me._droitacces_81.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_81.Size = New System.Drawing.Size(286, 18)
        Me._droitacces_81.TabIndex = 13
        Me._droitacces_81.Text = "Accès aux paies / facturations de tous les utilisateurs"
        Me._droitacces_81.UseVisualStyleBackColor = False
        '
        '_droitacces_92
        '
        Me._droitacces_92.AutoSize = True
        Me._droitacces_92.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_92.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_92.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_92.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_92.Location = New System.Drawing.Point(6, 145)
        Me._droitacces_92.Name = "_droitacces_92"
        Me._droitacces_92.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_92.Size = New System.Drawing.Size(257, 18)
        Me._droitacces_92.TabIndex = 13
        Me._droitacces_92.Text = "Droit de modifier les heures de travail d'une paie"
        Me._droitacces_92.UseVisualStyleBackColor = False
        '
        '_droitacces_93
        '
        Me._droitacces_93.AutoSize = True
        Me._droitacces_93.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_93.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_93.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_93.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_93.Location = New System.Drawing.Point(6, 217)
        Me._droitacces_93.Name = "_droitacces_93"
        Me._droitacces_93.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_93.Size = New System.Drawing.Size(188, 18)
        Me._droitacces_93.TabIndex = 13
        Me._droitacces_93.Text = "Accès aux utilisateurs connectés"
        Me._droitacces_93.UseVisualStyleBackColor = False
        '
        '_droitacces_79
        '
        Me._droitacces_79.AutoSize = True
        Me._droitacces_79.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_79.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_79.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_79.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_79.Location = New System.Drawing.Point(6, 199)
        Me._droitacces_79.Name = "_droitacces_79"
        Me._droitacces_79.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_79.Size = New System.Drawing.Size(376, 18)
        Me._droitacces_79.TabIndex = 13
        Me._droitacces_79.Text = "Droit de créer une nouvelle facture pour soi-même (travailleur autonome)"
        Me._droitacces_79.UseVisualStyleBackColor = False
        '
        '_droitacces_78
        '
        Me._droitacces_78.AutoSize = True
        Me._droitacces_78.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_78.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_78.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_78.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_78.Location = New System.Drawing.Point(6, 181)
        Me._droitacces_78.Name = "_droitacces_78"
        Me._droitacces_78.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_78.Size = New System.Drawing.Size(372, 18)
        Me._droitacces_78.TabIndex = 13
        Me._droitacces_78.Text = "Droit de créer une nouvelle facture pour tous les travailleurs autonomes"
        Me._droitacces_78.UseVisualStyleBackColor = False
        '
        '_droitacces_51
        '
        Me._droitacces_51.AutoSize = True
        Me._droitacces_51.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_51.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_51.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_51.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_51.Location = New System.Drawing.Point(6, 163)
        Me._droitacces_51.Name = "_droitacces_51"
        Me._droitacces_51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_51.Size = New System.Drawing.Size(196, 18)
        Me._droitacces_51.TabIndex = 13
        Me._droitacces_51.Text = "Droit de gérer les types d'utilisateur"
        Me._droitacces_51.UseVisualStyleBackColor = False
        '
        '_droitacces_45
        '
        Me._droitacces_45.AutoSize = True
        Me._droitacces_45.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_45.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_45.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_45.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_45.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_45.Name = "_droitacces_45"
        Me._droitacces_45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_45.Size = New System.Drawing.Size(192, 18)
        Me._droitacces_45.TabIndex = 13
        Me._droitacces_45.Text = "Accès à la gestion des utilisateurs"
        Me._droitacces_45.UseVisualStyleBackColor = False
        '
        '_droitacces_49
        '
        Me._droitacces_49.AutoSize = True
        Me._droitacces_49.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_49.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_49.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_49.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_49.Location = New System.Drawing.Point(6, 109)
        Me._droitacces_49.Name = "_droitacces_49"
        Me._droitacces_49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_49.Size = New System.Drawing.Size(316, 18)
        Me._droitacces_49.TabIndex = 13
        Me._droitacces_49.Text = "Droit de gérer les paies / facturations de tous les utilisateurs"
        Me._droitacces_49.UseVisualStyleBackColor = False
        '
        '_droitacces_47
        '
        Me._droitacces_47.AutoSize = True
        Me._droitacces_47.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_47.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_47.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_47.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_47.Location = New System.Drawing.Point(6, 55)
        Me._droitacces_47.Name = "_droitacces_47"
        Me._droitacces_47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_47.Size = New System.Drawing.Size(244, 18)
        Me._droitacces_47.TabIndex = 13
        Me._droitacces_47.Text = "Droit de gérer l'horaire de tous les utilisateurs"
        Me._droitacces_47.UseVisualStyleBackColor = False
        '
        '_droitacces_50
        '
        Me._droitacces_50.AutoSize = True
        Me._droitacces_50.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_50.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_50.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_50.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_50.Location = New System.Drawing.Point(6, 127)
        Me._droitacces_50.Name = "_droitacces_50"
        Me._droitacces_50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_50.Size = New System.Drawing.Size(226, 18)
        Me._droitacces_50.TabIndex = 13
        Me._droitacces_50.Text = "Droit de gérer sa propre paie / facturation"
        Me._droitacces_50.UseVisualStyleBackColor = False
        '
        '_droitacces_46
        '
        Me._droitacces_46.AutoSize = True
        Me._droitacces_46.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_46.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_46.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_46.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_46.Location = New System.Drawing.Point(6, 37)
        Me._droitacces_46.Name = "_droitacces_46"
        Me._droitacces_46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_46.Size = New System.Drawing.Size(164, 18)
        Me._droitacces_46.TabIndex = 13
        Me._droitacces_46.Text = "Droit de gérer les utilisateurs"
        Me._droitacces_46.UseVisualStyleBackColor = False
        '
        '_droitacces_48
        '
        Me._droitacces_48.AutoSize = True
        Me._droitacces_48.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_48.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_48.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_48.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_48.Location = New System.Drawing.Point(6, 73)
        Me._droitacces_48.Name = "_droitacces_48"
        Me._droitacces_48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_48.Size = New System.Drawing.Size(185, 18)
        Me._droitacces_48.TabIndex = 13
        Me._droitacces_48.Text = "Droit de gérer son propre horaire"
        Me._droitacces_48.UseVisualStyleBackColor = False
        '
        'GroupRapport
        '
        Me.GroupRapport.Controls.Add(Me._droitacces_71)
        Me.GroupRapport.Controls.Add(Me._droitacces_70)
        Me.GroupRapport.Controls.Add(Me._droitacces_69)
        Me.GroupRapport.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupRapport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupRapport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupRapport.Location = New System.Drawing.Point(0, 2000)
        Me.GroupRapport.Name = "GroupRapport"
        Me.GroupRapport.Size = New System.Drawing.Size(452, 75)
        Me.GroupRapport.TabIndex = 18
        Me.GroupRapport.TabStop = False
        Me.GroupRapport.Text = "Rapport"
        '
        '_droitacces_71
        '
        Me._droitacces_71.AutoSize = True
        Me._droitacces_71.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_71.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_71.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_71.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_71.Location = New System.Drawing.Point(6, 53)
        Me._droitacces_71.Name = "_droitacces_71"
        Me._droitacces_71.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_71.Size = New System.Drawing.Size(177, 18)
        Me._droitacces_71.TabIndex = 13
        Me._droitacces_71.Text = "Droit d'enregistrer des rapports"
        Me._droitacces_71.UseVisualStyleBackColor = False
        '
        '_droitacces_70
        '
        Me._droitacces_70.AutoSize = True
        Me._droitacces_70.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_70.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_70.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_70.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_70.Location = New System.Drawing.Point(6, 36)
        Me._droitacces_70.Name = "_droitacces_70"
        Me._droitacces_70.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_70.Size = New System.Drawing.Size(241, 18)
        Me._droitacces_70.TabIndex = 13
        Me._droitacces_70.Text = "Droit de générer un rapport via le générateur"
        Me._droitacces_70.UseVisualStyleBackColor = False
        '
        '_droitacces_69
        '
        Me._droitacces_69.AutoSize = True
        Me._droitacces_69.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_69.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_69.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_69.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_69.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_69.Name = "_droitacces_69"
        Me._droitacces_69.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_69.Size = New System.Drawing.Size(182, 18)
        Me._droitacces_69.TabIndex = 13
        Me._droitacces_69.Text = "Accès au générateur de rapport"
        Me._droitacces_69.UseVisualStyleBackColor = False
        '
        'GroupMessagerie
        '
        Me.GroupMessagerie.Controls.Add(Me._droitacces_35)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_43)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_42)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_107)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_40)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_41)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_39)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_44)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_68)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_38)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_37)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_36)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_102)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_101)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_100)
        Me.GroupMessagerie.Controls.Add(Me._droitacces_34)
        Me.GroupMessagerie.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupMessagerie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupMessagerie.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupMessagerie.Location = New System.Drawing.Point(0, 1666)
        Me.GroupMessagerie.Name = "GroupMessagerie"
        Me.GroupMessagerie.Size = New System.Drawing.Size(452, 334)
        Me.GroupMessagerie.TabIndex = 17
        Me.GroupMessagerie.TabStop = False
        Me.GroupMessagerie.Text = "Messagerie"
        '
        '_droitacces_35
        '
        Me._droitacces_35.AutoSize = True
        Me._droitacces_35.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_35.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_35.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_35.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_35.Location = New System.Drawing.Point(6, 88)
        Me._droitacces_35.Name = "_droitacces_35"
        Me._droitacces_35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_35.Size = New System.Drawing.Size(233, 18)
        Me._droitacces_35.TabIndex = 13
        Me._droitacces_35.Text = "Droit de gérer le carnet d'adresses général"
        Me._droitacces_35.UseVisualStyleBackColor = False
        '
        '_droitacces_43
        '
        Me._droitacces_43.AutoSize = True
        Me._droitacces_43.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_43.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_43.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_43.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_43.Location = New System.Drawing.Point(6, 294)
        Me._droitacces_43.Name = "_droitacces_43"
        Me._droitacces_43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_43.Size = New System.Drawing.Size(137, 18)
        Me._droitacces_43.TabIndex = 13
        Me._droitacces_43.Text = "Accès au publipostage"
        Me._droitacces_43.UseVisualStyleBackColor = False
        '
        '_droitacces_42
        '
        Me._droitacces_42.AutoSize = True
        Me._droitacces_42.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_42.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_42.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_42.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_42.Location = New System.Drawing.Point(6, 277)
        Me._droitacces_42.Name = "_droitacces_42"
        Me._droitacces_42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_42.Size = New System.Drawing.Size(395, 18)
        Me._droitacces_42.TabIndex = 13
        Me._droitacces_42.Text = "Droit de gérer la réception personnelle des messages de tous les utilisateurs"
        Me._droitacces_42.UseVisualStyleBackColor = False
        '
        '_droitacces_107
        '
        Me._droitacces_107.AutoSize = True
        Me._droitacces_107.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_107.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_107.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_107.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_107.Location = New System.Drawing.Point(6, 242)
        Me._droitacces_107.Name = "_droitacces_107"
        Me._droitacces_107.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_107.Size = New System.Drawing.Size(418, 18)
        Me._droitacces_107.TabIndex = 13
        Me._droitacces_107.Text = "Droit de télécharger les messages provenant d'un compte de messagerie externe"
        Me._droitacces_107.UseVisualStyleBackColor = False
        '
        '_droitacces_40
        '
        Me._droitacces_40.AutoSize = True
        Me._droitacces_40.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_40.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_40.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_40.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_40.Location = New System.Drawing.Point(6, 224)
        Me._droitacces_40.Name = "_droitacces_40"
        Me._droitacces_40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_40.Size = New System.Drawing.Size(270, 18)
        Me._droitacces_40.TabIndex = 13
        Me._droitacces_40.Text = "Droit de gérer la réception générale des messages"
        Me._droitacces_40.UseVisualStyleBackColor = False
        '
        '_droitacces_41
        '
        Me._droitacces_41.AutoSize = True
        Me._droitacces_41.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_41.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_41.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_41.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_41.Location = New System.Drawing.Point(6, 260)
        Me._droitacces_41.Name = "_droitacces_41"
        Me._droitacces_41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_41.Size = New System.Drawing.Size(370, 18)
        Me._droitacces_41.TabIndex = 13
        Me._droitacces_41.Text = "Accès à la réception personnelle des messages de tous les utilisateurs"
        Me._droitacces_41.UseVisualStyleBackColor = False
        '
        '_droitacces_39
        '
        Me._droitacces_39.AutoSize = True
        Me._droitacces_39.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_39.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_39.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_39.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_39.Location = New System.Drawing.Point(6, 207)
        Me._droitacces_39.Name = "_droitacces_39"
        Me._droitacces_39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_39.Size = New System.Drawing.Size(245, 18)
        Me._droitacces_39.TabIndex = 13
        Me._droitacces_39.Text = "Accès à la réception générale des messages"
        Me._droitacces_39.UseVisualStyleBackColor = False
        '
        '_droitacces_44
        '
        Me._droitacces_44.AutoSize = True
        Me._droitacces_44.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_44.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_44.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_44.Location = New System.Drawing.Point(6, 311)
        Me._droitacces_44.Name = "_droitacces_44"
        Me._droitacces_44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_44.Size = New System.Drawing.Size(277, 18)
        Me._droitacces_44.TabIndex = 13
        Me._droitacces_44.Text = "Droit de gérer les paramètres des comptes courriels"
        Me._droitacces_44.UseVisualStyleBackColor = False
        '
        '_droitacces_68
        '
        Me._droitacces_68.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_68.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_68.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_68.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_68.Location = New System.Drawing.Point(6, 139)
        Me._droitacces_68.Name = "_droitacces_68"
        Me._droitacces_68.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_68.Size = New System.Drawing.Size(327, 51)
        Me._droitacces_68.TabIndex = 13
        Me._droitacces_68.Text = "Pour l'envoi d'un message, accès seulement aux comptes de courriel commun ou dont" & _
            " les messages sont transférés dans le dossier de réception personnel de cet util" & _
            "isateur"
        Me._droitacces_68.UseVisualStyleBackColor = False
        '
        '_droitacces_38
        '
        Me._droitacces_38.AutoSize = True
        Me._droitacces_38.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_38.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_38.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_38.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_38.Location = New System.Drawing.Point(6, 190)
        Me._droitacces_38.Name = "_droitacces_38"
        Me._droitacces_38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_38.Size = New System.Drawing.Size(219, 18)
        Me._droitacces_38.TabIndex = 13
        Me._droitacces_38.Text = "Droit d'envoyer des messages externes"
        Me._droitacces_38.UseVisualStyleBackColor = False
        '
        '_droitacces_37
        '
        Me._droitacces_37.AutoSize = True
        Me._droitacces_37.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_37.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_37.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_37.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_37.Location = New System.Drawing.Point(6, 122)
        Me._droitacces_37.Name = "_droitacces_37"
        Me._droitacces_37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_37.Size = New System.Drawing.Size(356, 18)
        Me._droitacces_37.TabIndex = 13
        Me._droitacces_37.Text = "Droit de gérer le carnet d'adresses personnel de tous les utilisateurs"
        Me._droitacces_37.UseVisualStyleBackColor = False
        '
        '_droitacces_36
        '
        Me._droitacces_36.AutoSize = True
        Me._droitacces_36.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_36.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_36.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_36.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_36.Location = New System.Drawing.Point(6, 105)
        Me._droitacces_36.Name = "_droitacces_36"
        Me._droitacces_36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_36.Size = New System.Drawing.Size(326, 18)
        Me._droitacces_36.TabIndex = 13
        Me._droitacces_36.Text = "Accès au carnet d'adresses personnel de tous les utilisateurs"
        Me._droitacces_36.UseVisualStyleBackColor = False
        '
        '_droitacces_102
        '
        Me._droitacces_102.AutoSize = True
        Me._droitacces_102.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_102.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_102.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_102.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_102.Location = New System.Drawing.Point(6, 53)
        Me._droitacces_102.Name = "_droitacces_102"
        Me._droitacces_102.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_102.Size = New System.Drawing.Size(240, 18)
        Me._droitacces_102.TabIndex = 13
        Me._droitacces_102.Text = "Accès au carnet d'adresses des utilisateurs"
        Me._droitacces_102.UseVisualStyleBackColor = False
        '
        '_droitacces_101
        '
        Me._droitacces_101.AutoSize = True
        Me._droitacces_101.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_101.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_101.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_101.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_101.Location = New System.Drawing.Point(6, 36)
        Me._droitacces_101.Name = "_droitacces_101"
        Me._droitacces_101.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_101.Size = New System.Drawing.Size(322, 18)
        Me._droitacces_101.TabIndex = 13
        Me._droitacces_101.Text = "Accès au carnet d'adresses des personnes/organismes clés"
        Me._droitacces_101.UseVisualStyleBackColor = False
        '
        '_droitacces_100
        '
        Me._droitacces_100.AutoSize = True
        Me._droitacces_100.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_100.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_100.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_100.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_100.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_100.Name = "_droitacces_100"
        Me._droitacces_100.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_100.Size = New System.Drawing.Size(219, 18)
        Me._droitacces_100.TabIndex = 13
        Me._droitacces_100.Text = "Accès au carnet d'adresses des clients"
        Me._droitacces_100.UseVisualStyleBackColor = False
        '
        '_droitacces_34
        '
        Me._droitacces_34.AutoSize = True
        Me._droitacces_34.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_34.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_34.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_34.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_34.Location = New System.Drawing.Point(6, 71)
        Me._droitacces_34.Name = "_droitacces_34"
        Me._droitacces_34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_34.Size = New System.Drawing.Size(203, 18)
        Me._droitacces_34.TabIndex = 13
        Me._droitacces_34.Text = "Accès au carnet d'adresses général"
        Me._droitacces_34.UseVisualStyleBackColor = False
        '
        'GroupGestion
        '
        Me.GroupGestion.Controls.Add(Me._droitacces_106)
        Me.GroupGestion.Controls.Add(Me._droitacces_105)
        Me.GroupGestion.Controls.Add(Me._droitacces_28)
        Me.GroupGestion.Controls.Add(Me._droitacces_108)
        Me.GroupGestion.Controls.Add(Me._droitacces_103)
        Me.GroupGestion.Controls.Add(Me._droitacces_72)
        Me.GroupGestion.Controls.Add(Me._droitacces_66)
        Me.GroupGestion.Controls.Add(Me._droitacces_65)
        Me.GroupGestion.Controls.Add(Me._droitacces_33)
        Me.GroupGestion.Controls.Add(Me._droitacces_31)
        Me.GroupGestion.Controls.Add(Me._droitacces_32)
        Me.GroupGestion.Controls.Add(Me._droitacces_30)
        Me.GroupGestion.Controls.Add(Me._droitacces_29)
        Me.GroupGestion.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupGestion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupGestion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupGestion.Location = New System.Drawing.Point(0, 1414)
        Me.GroupGestion.Name = "GroupGestion"
        Me.GroupGestion.Size = New System.Drawing.Size(452, 252)
        Me.GroupGestion.TabIndex = 16
        Me.GroupGestion.TabStop = False
        Me.GroupGestion.Text = "Différentes gestions"
        '
        '_droitacces_106
        '
        Me._droitacces_106.AutoSize = True
        Me._droitacces_106.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_106.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_106.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_106.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_106.Location = New System.Drawing.Point(6, 71)
        Me._droitacces_106.Name = "_droitacces_106"
        Me._droitacces_106.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_106.Size = New System.Drawing.Size(330, 18)
        Me._droitacces_106.TabIndex = 13
        Me._droitacces_106.Text = "Accès à la gestion des types de texte des dossiers des clients"
        Me._droitacces_106.UseVisualStyleBackColor = False
        '
        '_droitacces_105
        '
        Me._droitacces_105.AutoSize = True
        Me._droitacces_105.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_105.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_105.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_105.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_105.Location = New System.Drawing.Point(6, 53)
        Me._droitacces_105.Name = "_droitacces_105"
        Me._droitacces_105.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_105.Size = New System.Drawing.Size(326, 18)
        Me._droitacces_105.TabIndex = 13
        Me._droitacces_105.Text = "Accès à la gestion des types d'alerte des dossiers des clients"
        Me._droitacces_105.UseVisualStyleBackColor = False
        '
        '_droitacces_28
        '
        Me._droitacces_28.AutoSize = True
        Me._droitacces_28.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_28.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_28.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_28.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_28.Name = "_droitacces_28"
        Me._droitacces_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_28.Size = New System.Drawing.Size(188, 18)
        Me._droitacces_28.TabIndex = 13
        Me._droitacces_28.Text = "Accès aux codifications dossiers"
        Me._droitacces_28.UseVisualStyleBackColor = False
        '
        '_droitacces_108
        '
        Me._droitacces_108.AutoSize = True
        Me._droitacces_108.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_108.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_108.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_108.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_108.Location = New System.Drawing.Point(6, 231)
        Me._droitacces_108.Name = "_droitacces_108"
        Me._droitacces_108.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_108.Size = New System.Drawing.Size(229, 18)
        Me._droitacces_108.TabIndex = 13
        Me._droitacces_108.Text = "Accès à la gestion des tâches du serveur"
        Me._droitacces_108.UseVisualStyleBackColor = False
        '
        '_droitacces_103
        '
        Me._droitacces_103.AutoSize = True
        Me._droitacces_103.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_103.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_103.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_103.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_103.Location = New System.Drawing.Point(6, 213)
        Me._droitacces_103.Name = "_droitacces_103"
        Me._droitacces_103.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_103.Size = New System.Drawing.Size(203, 18)
        Me._droitacces_103.TabIndex = 13
        Me._droitacces_103.Text = "Droit de gérer les journées spéciales"
        Me._droitacces_103.UseVisualStyleBackColor = False
        '
        '_droitacces_72
        '
        Me._droitacces_72.AutoSize = True
        Me._droitacces_72.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_72.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_72.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_72.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_72.Location = New System.Drawing.Point(6, 194)
        Me._droitacces_72.Name = "_droitacces_72"
        Me._droitacces_72.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_72.Size = New System.Drawing.Size(237, 18)
        Me._droitacces_72.TabIndex = 13
        Me._droitacces_72.Text = "Droit d'enregistrer les options de fin de mois"
        Me._droitacces_72.UseVisualStyleBackColor = False
        '
        '_droitacces_66
        '
        Me._droitacces_66.AutoSize = True
        Me._droitacces_66.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_66.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_66.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_66.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_66.Location = New System.Drawing.Point(6, 176)
        Me._droitacces_66.Name = "_droitacces_66"
        Me._droitacces_66.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_66.Size = New System.Drawing.Size(170, 18)
        Me._droitacces_66.TabIndex = 13
        Me._droitacces_66.Text = "Droit d'effectuer la fin de mois"
        Me._droitacces_66.UseVisualStyleBackColor = False
        '
        '_droitacces_65
        '
        Me._droitacces_65.AutoSize = True
        Me._droitacces_65.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_65.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_65.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_65.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_65.Location = New System.Drawing.Point(6, 158)
        Me._droitacces_65.Name = "_droitacces_65"
        Me._droitacces_65.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_65.Size = New System.Drawing.Size(197, 18)
        Me._droitacces_65.TabIndex = 13
        Me._droitacces_65.Text = "Accès à la gestion de la fin de mois"
        Me._droitacces_65.UseVisualStyleBackColor = False
        '
        '_droitacces_33
        '
        Me._droitacces_33.AutoSize = True
        Me._droitacces_33.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_33.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_33.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_33.Location = New System.Drawing.Point(6, 140)
        Me._droitacces_33.Name = "_droitacces_33"
        Me._droitacces_33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_33.Size = New System.Drawing.Size(223, 18)
        Me._droitacces_33.TabIndex = 13
        Me._droitacces_33.Text = "Droit de gérer les préférences générales"
        Me._droitacces_33.UseVisualStyleBackColor = False
        '
        '_droitacces_31
        '
        Me._droitacces_31.AutoSize = True
        Me._droitacces_31.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_31.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_31.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_31.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_31.Location = New System.Drawing.Point(6, 123)
        Me._droitacces_31.Name = "_droitacces_31"
        Me._droitacces_31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_31.Size = New System.Drawing.Size(243, 18)
        Me._droitacces_31.TabIndex = 13
        Me._droitacces_31.Text = "Droit de gérer les modèles de texte généraux"
        Me._droitacces_31.UseVisualStyleBackColor = False
        '
        '_droitacces_32
        '
        Me._droitacces_32.AutoSize = True
        Me._droitacces_32.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_32.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_32.Location = New System.Drawing.Point(6, 106)
        Me._droitacces_32.Name = "_droitacces_32"
        Me._droitacces_32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_32.Size = New System.Drawing.Size(173, 18)
        Me._droitacces_32.TabIndex = 13
        Me._droitacces_32.Text = "Droit de gérer les équipements"
        Me._droitacces_32.UseVisualStyleBackColor = False
        '
        '_droitacces_30
        '
        Me._droitacces_30.AutoSize = True
        Me._droitacces_30.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_30.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_30.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_30.Location = New System.Drawing.Point(6, 89)
        Me._droitacces_30.Name = "_droitacces_30"
        Me._droitacces_30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_30.Size = New System.Drawing.Size(143, 18)
        Me._droitacces_30.TabIndex = 13
        Me._droitacces_30.Text = "Accès aux équipements"
        Me._droitacces_30.UseVisualStyleBackColor = False
        '
        '_droitacces_29
        '
        Me._droitacces_29.AutoSize = True
        Me._droitacces_29.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_29.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_29.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_29.Location = New System.Drawing.Point(6, 36)
        Me._droitacces_29.Name = "_droitacces_29"
        Me._droitacces_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_29.Size = New System.Drawing.Size(218, 18)
        Me._droitacces_29.TabIndex = 13
        Me._droitacces_29.Text = "Droit de gérer les codifications dossiers"
        Me._droitacces_29.UseVisualStyleBackColor = False
        '
        'GroupKP
        '
        Me.GroupKP.Controls.Add(Me._droitacces_24)
        Me.GroupKP.Controls.Add(Me._droitacces_52)
        Me.GroupKP.Controls.Add(Me._droitacces_77)
        Me.GroupKP.Controls.Add(Me._droitacces_26)
        Me.GroupKP.Controls.Add(Me._droitacces_23)
        Me.GroupKP.Controls.Add(Me._droitacces_21)
        Me.GroupKP.Controls.Add(Me._droitacces_20)
        Me.GroupKP.Controls.Add(Me._droitacces_22)
        Me.GroupKP.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupKP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupKP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupKP.Location = New System.Drawing.Point(0, 1255)
        Me.GroupKP.Name = "GroupKP"
        Me.GroupKP.Size = New System.Drawing.Size(452, 159)
        Me.GroupKP.TabIndex = 15
        Me.GroupKP.TabStop = False
        Me.GroupKP.Text = "Compte personne / organisme clé"
        '
        '_droitacces_24
        '
        Me._droitacces_24.AutoSize = True
        Me._droitacces_24.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_24.Location = New System.Drawing.Point(6, 104)
        Me._droitacces_24.Name = "_droitacces_24"
        Me._droitacces_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_24.Size = New System.Drawing.Size(174, 18)
        Me._droitacces_24.TabIndex = 13
        Me._droitacces_24.Text = "Droit de modifier la comptabilité"
        Me._droitacces_24.UseVisualStyleBackColor = False
        '
        '_droitacces_52
        '
        Me._droitacces_52.AutoSize = True
        Me._droitacces_52.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_52.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_52.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_52.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_52.Location = New System.Drawing.Point(6, 36)
        Me._droitacces_52.Name = "_droitacces_52"
        Me._droitacces_52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_52.Size = New System.Drawing.Size(260, 18)
        Me._droitacces_52.TabIndex = 13
        Me._droitacces_52.Text = "Droit de supprimer une personne / organisme clé"
        Me._droitacces_52.UseVisualStyleBackColor = False
        '
        '_droitacces_77
        '
        Me._droitacces_77.AutoSize = True
        Me._droitacces_77.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_77.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_77.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_77.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_77.Location = New System.Drawing.Point(6, 138)
        Me._droitacces_77.Name = "_droitacces_77"
        Me._droitacces_77.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_77.Size = New System.Drawing.Size(194, 18)
        Me._droitacces_77.TabIndex = 13
        Me._droitacces_77.Text = "Droit de créer une nouvelle facture"
        Me._droitacces_77.UseVisualStyleBackColor = False
        '
        '_droitacces_26
        '
        Me._droitacces_26.AutoSize = True
        Me._droitacces_26.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_26.Location = New System.Drawing.Point(6, 121)
        Me._droitacces_26.Name = "_droitacces_26"
        Me._droitacces_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_26.Size = New System.Drawing.Size(189, 18)
        Me._droitacces_26.TabIndex = 13
        Me._droitacces_26.Text = "Droit de gérer les communications"
        Me._droitacces_26.UseVisualStyleBackColor = False
        '
        '_droitacces_23
        '
        Me._droitacces_23.AutoSize = True
        Me._droitacces_23.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_23.Location = New System.Drawing.Point(6, 87)
        Me._droitacces_23.Name = "_droitacces_23"
        Me._droitacces_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_23.Size = New System.Drawing.Size(137, 18)
        Me._droitacces_23.TabIndex = 13
        Me._droitacces_23.Text = "Accès à la comptabilité"
        Me._droitacces_23.UseVisualStyleBackColor = False
        '
        '_droitacces_21
        '
        Me._droitacces_21.AutoSize = True
        Me._droitacces_21.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_21.Location = New System.Drawing.Point(6, 53)
        Me._droitacces_21.Name = "_droitacces_21"
        Me._droitacces_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_21.Size = New System.Drawing.Size(260, 18)
        Me._droitacces_21.TabIndex = 13
        Me._droitacces_21.Text = "Accès aux comptes personnes / organismes clé"
        Me._droitacces_21.UseVisualStyleBackColor = False
        '
        '_droitacces_20
        '
        Me._droitacces_20.AutoSize = True
        Me._droitacces_20.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_20.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_20.Name = "_droitacces_20"
        Me._droitacces_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_20.Size = New System.Drawing.Size(293, 18)
        Me._droitacces_20.TabIndex = 13
        Me._droitacces_20.Text = "Droit d'ajouter de nouvelles personnes / organismes clé"
        Me._droitacces_20.UseVisualStyleBackColor = False
        '
        '_droitacces_22
        '
        Me._droitacces_22.AutoSize = True
        Me._droitacces_22.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_22.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_22.Location = New System.Drawing.Point(6, 70)
        Me._droitacces_22.Name = "_droitacces_22"
        Me._droitacces_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_22.Size = New System.Drawing.Size(225, 18)
        Me._droitacces_22.TabIndex = 13
        Me._droitacces_22.Text = "Droit de modifier les informations de base"
        Me._droitacces_22.UseVisualStyleBackColor = False
        '
        'GroupRV_QL
        '
        Me.GroupRV_QL.Controls.Add(Me._droitacces_88)
        Me.GroupRV_QL.Controls.Add(Me._droitacces_19)
        Me.GroupRV_QL.Controls.Add(Me._droitacces_18)
        Me.GroupRV_QL.Controls.Add(Me._droitacces_17)
        Me.GroupRV_QL.Controls.Add(Me._droitacces_58)
        Me.GroupRV_QL.Controls.Add(Me._droitacces_75)
        Me.GroupRV_QL.Controls.Add(Me._droitacces_16)
        Me.GroupRV_QL.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupRV_QL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupRV_QL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupRV_QL.Location = New System.Drawing.Point(0, 1113)
        Me.GroupRV_QL.Name = "GroupRV_QL"
        Me.GroupRV_QL.Size = New System.Drawing.Size(452, 142)
        Me.GroupRV_QL.TabIndex = 14
        Me.GroupRV_QL.TabStop = False
        Me.GroupRV_QL.Text = "Rendez-vous && Liste d'attente"
        '
        '_droitacces_88
        '
        Me._droitacces_88.AutoSize = True
        Me._droitacces_88.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_88.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_88.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_88.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_88.Location = New System.Drawing.Point(6, 122)
        Me._droitacces_88.Name = "_droitacces_88"
        Me._droitacces_88.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_88.Size = New System.Drawing.Size(393, 18)
        Me._droitacces_88.TabIndex = 13
        Me._droitacces_88.Text = "Droit de modifier le filtrage lorsque la liste d'attente apparaît automatiquement" & _
            ""
        Me._droitacces_88.UseVisualStyleBackColor = False
        '
        '_droitacces_19
        '
        Me._droitacces_19.AutoSize = True
        Me._droitacces_19.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_19.Location = New System.Drawing.Point(6, 104)
        Me._droitacces_19.Name = "_droitacces_19"
        Me._droitacces_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_19.Size = New System.Drawing.Size(169, 18)
        Me._droitacces_19.TabIndex = 13
        Me._droitacces_19.Text = "Droit de gérer la liste d'attente"
        Me._droitacces_19.UseVisualStyleBackColor = False
        '
        '_droitacces_18
        '
        Me._droitacces_18.AutoSize = True
        Me._droitacces_18.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_18.Location = New System.Drawing.Point(6, 87)
        Me._droitacces_18.Name = "_droitacces_18"
        Me._droitacces_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_18.Size = New System.Drawing.Size(144, 18)
        Me._droitacces_18.TabIndex = 13
        Me._droitacces_18.Text = "Accès à la liste d'attente"
        Me._droitacces_18.UseVisualStyleBackColor = False
        '
        '_droitacces_17
        '
        Me._droitacces_17.AutoSize = True
        Me._droitacces_17.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_17.Location = New System.Drawing.Point(6, 70)
        Me._droitacces_17.Name = "_droitacces_17"
        Me._droitacces_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_17.Size = New System.Drawing.Size(176, 18)
        Me._droitacces_17.TabIndex = 13
        Me._droitacces_17.Text = "Accès aux rendez-vous futurs"
        Me._droitacces_17.UseVisualStyleBackColor = False
        '
        '_droitacces_58
        '
        Me._droitacces_58.AutoSize = True
        Me._droitacces_58.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_58.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_58.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_58.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_58.Location = New System.Drawing.Point(6, 53)
        Me._droitacces_58.Name = "_droitacces_58"
        Me._droitacces_58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_58.Size = New System.Drawing.Size(235, 18)
        Me._droitacces_58.TabIndex = 13
        Me._droitacces_58.Text = "Droit de changer le statut d'un rendez-vous"
        Me._droitacces_58.UseVisualStyleBackColor = False
        '
        '_droitacces_75
        '
        Me._droitacces_75.AutoSize = True
        Me._droitacces_75.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_75.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_75.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_75.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_75.Location = New System.Drawing.Point(6, 36)
        Me._droitacces_75.Name = "_droitacces_75"
        Me._droitacces_75.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_75.Size = New System.Drawing.Size(194, 18)
        Me._droitacces_75.TabIndex = 13
        Me._droitacces_75.Text = "Droit de supprimer un rendez-vous"
        Me._droitacces_75.UseVisualStyleBackColor = False
        '
        '_droitacces_16
        '
        Me._droitacces_16.AutoSize = True
        Me._droitacces_16.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_16.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_16.Name = "_droitacces_16"
        Me._droitacces_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_16.Size = New System.Drawing.Size(280, 18)
        Me._droitacces_16.TabIndex = 13
        Me._droitacces_16.Text = "Droit d'ajouter un nouveau rendez-vous à un dossier"
        Me._droitacces_16.UseVisualStyleBackColor = False
        '
        'GroupClient
        '
        Me.GroupClient.Controls.Add(Me._droitacces_104)
        Me.GroupClient.Controls.Add(Me._droitacces_76)
        Me.GroupClient.Controls.Add(Me._droitacces_63)
        Me.GroupClient.Controls.Add(Me._droitacces_15)
        Me.GroupClient.Controls.Add(Me._droitacces_56)
        Me.GroupClient.Controls.Add(Me._droitacces_89)
        Me.GroupClient.Controls.Add(Me._droitacces_57)
        Me.GroupClient.Controls.Add(Me._droitacces_14)
        Me.GroupClient.Controls.Add(Me._droitacces_25)
        Me.GroupClient.Controls.Add(Me._droitacces_13)
        Me.GroupClient.Controls.Add(Me._droitacces_67)
        Me.GroupClient.Controls.Add(Me._droitacces_55)
        Me.GroupClient.Controls.Add(Me._droitacces_12)
        Me.GroupClient.Controls.Add(Me._droitacces_111)
        Me.GroupClient.Controls.Add(Me._droitacces_110)
        Me.GroupClient.Controls.Add(Me._droitacces_11)
        Me.GroupClient.Controls.Add(Me._droitacces_10)
        Me.GroupClient.Controls.Add(Me._droitacces_9)
        Me.GroupClient.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupClient.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupClient.Location = New System.Drawing.Point(0, 759)
        Me.GroupClient.Name = "GroupClient"
        Me.GroupClient.Size = New System.Drawing.Size(452, 354)
        Me.GroupClient.TabIndex = 13
        Me.GroupClient.TabStop = False
        Me.GroupClient.Text = "Compte client && Dossier"
        '
        '_droitacces_104
        '
        Me._droitacces_104.AutoSize = True
        Me._droitacces_104.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_104.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_104.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_104.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_104.Location = New System.Drawing.Point(6, 325)
        Me._droitacces_104.Name = "_droitacces_104"
        Me._droitacces_104.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_104.Size = New System.Drawing.Size(232, 18)
        Me._droitacces_104.TabIndex = 13
        Me._droitacces_104.Text = "Accès à l'analyse des textes des dossiers"
        Me._droitacces_104.UseVisualStyleBackColor = False
        '
        '_droitacces_76
        '
        Me._droitacces_76.AutoSize = True
        Me._droitacces_76.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_76.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_76.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_76.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_76.Location = New System.Drawing.Point(6, 307)
        Me._droitacces_76.Name = "_droitacces_76"
        Me._droitacces_76.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_76.Size = New System.Drawing.Size(194, 18)
        Me._droitacces_76.TabIndex = 13
        Me._droitacces_76.Text = "Droit de créer une nouvelle facture"
        Me._droitacces_76.UseVisualStyleBackColor = False
        '
        '_droitacces_63
        '
        Me._droitacces_63.AutoSize = True
        Me._droitacces_63.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_63.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_63.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_63.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_63.Location = New System.Drawing.Point(6, 289)
        Me._droitacces_63.Name = "_droitacces_63"
        Me._droitacces_63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_63.Size = New System.Drawing.Size(215, 18)
        Me._droitacces_63.TabIndex = 13
        Me._droitacces_63.Text = "Droit de modifier le montant d'une vente"
        Me._droitacces_63.UseVisualStyleBackColor = False
        '
        '_droitacces_15
        '
        Me._droitacces_15.AutoSize = True
        Me._droitacces_15.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_15.Location = New System.Drawing.Point(6, 271)
        Me._droitacces_15.Name = "_droitacces_15"
        Me._droitacces_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_15.Size = New System.Drawing.Size(174, 18)
        Me._droitacces_15.TabIndex = 13
        Me._droitacces_15.Text = "Droit de modifier la comptabilité"
        Me._droitacces_15.UseVisualStyleBackColor = False
        '
        '_droitacces_56
        '
        Me._droitacces_56.AutoSize = True
        Me._droitacces_56.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_56.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_56.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_56.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_56.Location = New System.Drawing.Point(6, 217)
        Me._droitacces_56.Name = "_droitacces_56"
        Me._droitacces_56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_56.Size = New System.Drawing.Size(185, 18)
        Me._droitacces_56.TabIndex = 13
        Me._droitacces_56.Text = "Droit de modifier le bilan de santé"
        Me._droitacces_56.UseVisualStyleBackColor = False
        '
        '_droitacces_89
        '
        Me._droitacces_89.AutoSize = True
        Me._droitacces_89.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_89.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_89.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_89.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_89.Location = New System.Drawing.Point(6, 145)
        Me._droitacces_89.Name = "_droitacces_89"
        Me._droitacces_89.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_89.Size = New System.Drawing.Size(303, 18)
        Me._droitacces_89.TabIndex = 13
        Me._droitacces_89.Text = "Droit de transférer un dossier vers un autre compte client"
        Me._droitacces_89.UseVisualStyleBackColor = False
        '
        '_droitacces_57
        '
        Me._droitacces_57.AutoSize = True
        Me._droitacces_57.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_57.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_57.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_57.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_57.Location = New System.Drawing.Point(6, 235)
        Me._droitacces_57.Name = "_droitacces_57"
        Me._droitacces_57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_57.Size = New System.Drawing.Size(201, 18)
        Me._droitacces_57.TabIndex = 13
        Me._droitacces_57.Text = "Droit de modifier les communications"
        Me._droitacces_57.UseVisualStyleBackColor = False
        '
        '_droitacces_14
        '
        Me._droitacces_14.AutoSize = True
        Me._droitacces_14.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_14.Location = New System.Drawing.Point(6, 253)
        Me._droitacces_14.Name = "_droitacces_14"
        Me._droitacces_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_14.Size = New System.Drawing.Size(137, 18)
        Me._droitacces_14.TabIndex = 13
        Me._droitacces_14.Text = "Accès à la comptabilité"
        Me._droitacces_14.UseVisualStyleBackColor = False
        '
        '_droitacces_25
        '
        Me._droitacces_25.AutoSize = True
        Me._droitacces_25.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_25.Location = New System.Drawing.Point(6, 37)
        Me._droitacces_25.Name = "_droitacces_25"
        Me._droitacces_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_25.Size = New System.Drawing.Size(157, 18)
        Me._droitacces_25.TabIndex = 13
        Me._droitacces_25.Text = "Droit de supprimer un client"
        Me._droitacces_25.UseVisualStyleBackColor = False
        '
        '_droitacces_13
        '
        Me._droitacces_13.AutoSize = True
        Me._droitacces_13.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_13.Location = New System.Drawing.Point(6, 199)
        Me._droitacces_13.Name = "_droitacces_13"
        Me._droitacces_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_13.Size = New System.Drawing.Size(287, 18)
        Me._droitacces_13.TabIndex = 13
        Me._droitacces_13.Text = "Droit de modifier les informations de base d'un dossier"
        Me._droitacces_13.UseVisualStyleBackColor = False
        '
        '_droitacces_67
        '
        Me._droitacces_67.AutoSize = True
        Me._droitacces_67.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_67.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_67.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_67.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_67.Location = New System.Drawing.Point(6, 163)
        Me._droitacces_67.Name = "_droitacces_67"
        Me._droitacces_67.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_67.Size = New System.Drawing.Size(291, 18)
        Me._droitacces_67.TabIndex = 13
        Me._droitacces_67.Text = "Droit de modifier la demande d'autorisation d'un dossier"
        Me._droitacces_67.UseVisualStyleBackColor = False
        '
        '_droitacces_55
        '
        Me._droitacces_55.AutoSize = True
        Me._droitacces_55.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_55.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_55.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_55.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_55.Location = New System.Drawing.Point(6, 127)
        Me._droitacces_55.Name = "_droitacces_55"
        Me._droitacces_55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_55.Size = New System.Drawing.Size(209, 18)
        Me._droitacces_55.TabIndex = 13
        Me._droitacces_55.Text = "Droit de changer le statut d'un dossier"
        Me._droitacces_55.UseVisualStyleBackColor = False
        '
        '_droitacces_12
        '
        Me._droitacces_12.AutoSize = True
        Me._droitacces_12.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_12.Location = New System.Drawing.Point(6, 181)
        Me._droitacces_12.Name = "_droitacces_12"
        Me._droitacces_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_12.Size = New System.Drawing.Size(383, 18)
        Me._droitacces_12.TabIndex = 13
        Me._droitacces_12.Text = "Droit de modifier un dossier (excepté les informations de base du dossier)"
        Me._droitacces_12.UseVisualStyleBackColor = False
        '
        '_droitacces_110
        '
        Me._droitacces_110.AutoSize = True
        Me._droitacces_110.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_110.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_110.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_110.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_110.Location = New System.Drawing.Point(6, 91)
        Me._droitacces_110.Name = "_droitacces_110"
        Me._droitacces_110.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_110.Size = New System.Drawing.Size(154, 18)
        Me._droitacces_110.TabIndex = 13
        Me._droitacces_110.Text = "Droit d'exporter un dossier"
        Me._droitacces_110.UseVisualStyleBackColor = False
        '
        '_droitacces_11
        '
        Me._droitacces_11.AutoSize = True
        Me._droitacces_11.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_11.Location = New System.Drawing.Point(6, 109)
        Me._droitacces_11.Name = "_droitacces_11"
        Me._droitacces_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_11.Size = New System.Drawing.Size(306, 18)
        Me._droitacces_11.TabIndex = 13
        Me._droitacces_11.Text = "Droit de modifier les informations de base du compte client"
        Me._droitacces_11.UseVisualStyleBackColor = False
        '
        '_droitacces_10
        '
        Me._droitacces_10.AutoSize = True
        Me._droitacces_10.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_10.Location = New System.Drawing.Point(6, 55)
        Me._droitacces_10.Name = "_droitacces_10"
        Me._droitacces_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_10.Size = New System.Drawing.Size(157, 18)
        Me._droitacces_10.TabIndex = 13
        Me._droitacces_10.Text = "Accès aux comptes clients"
        Me._droitacces_10.UseVisualStyleBackColor = False
        '
        '_droitacces_9
        '
        Me._droitacces_9.AutoSize = True
        Me._droitacces_9.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_9.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_9.Name = "_droitacces_9"
        Me._droitacces_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_9.Size = New System.Drawing.Size(192, 18)
        Me._droitacces_9.TabIndex = 13
        Me._droitacces_9.Text = "Droit d'ajouter de nouveaux clients"
        Me._droitacces_9.UseVisualStyleBackColor = False
        '
        'GroupClinique
        '
        Me.GroupClinique.Controls.Add(Me._droitacces_80)
        Me.GroupClinique.Controls.Add(Me._droitacces_85)
        Me.GroupClinique.Controls.Add(Me._droitacces_83)
        Me.GroupClinique.Controls.Add(Me._droitacces_82)
        Me.GroupClinique.Controls.Add(Me._droitacces_62)
        Me.GroupClinique.Controls.Add(Me._droitacces_27)
        Me.GroupClinique.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupClinique.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupClinique.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupClinique.Location = New System.Drawing.Point(0, 626)
        Me.GroupClinique.Name = "GroupClinique"
        Me.GroupClinique.Size = New System.Drawing.Size(452, 133)
        Me.GroupClinique.TabIndex = 19
        Me.GroupClinique.TabStop = False
        Me.GroupClinique.Text = "Clinique"
        '
        '_droitacces_80
        '
        Me._droitacces_80.AutoSize = True
        Me._droitacces_80.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_80.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_80.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_80.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_80.Location = New System.Drawing.Point(6, 110)
        Me._droitacces_80.Name = "_droitacces_80"
        Me._droitacces_80.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_80.Size = New System.Drawing.Size(173, 18)
        Me._droitacces_80.TabIndex = 15
        Me._droitacces_80.Text = "Droit d'effectuer les paiements"
        Me._droitacces_80.UseVisualStyleBackColor = False
        '
        '_droitacces_85
        '
        Me._droitacces_85.AutoSize = True
        Me._droitacces_85.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_85.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_85.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_85.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_85.Location = New System.Drawing.Point(6, 91)
        Me._droitacces_85.Name = "_droitacces_85"
        Me._droitacces_85.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_85.Size = New System.Drawing.Size(194, 18)
        Me._droitacces_85.TabIndex = 15
        Me._droitacces_85.Text = "Droit de créer une nouvelle facture"
        Me._droitacces_85.UseVisualStyleBackColor = False
        '
        '_droitacces_83
        '
        Me._droitacces_83.AutoSize = True
        Me._droitacces_83.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_83.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_83.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_83.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_83.Location = New System.Drawing.Point(6, 72)
        Me._droitacces_83.Name = "_droitacces_83"
        Me._droitacces_83.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_83.Size = New System.Drawing.Size(218, 18)
        Me._droitacces_83.TabIndex = 15
        Me._droitacces_83.Text = "Droit de gérer les factures de la clinique"
        Me._droitacces_83.UseVisualStyleBackColor = False
        '
        '_droitacces_82
        '
        Me._droitacces_82.AutoSize = True
        Me._droitacces_82.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_82.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_82.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_82.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_82.Location = New System.Drawing.Point(6, 54)
        Me._droitacces_82.Name = "_droitacces_82"
        Me._droitacces_82.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_82.Size = New System.Drawing.Size(188, 18)
        Me._droitacces_82.TabIndex = 15
        Me._droitacces_82.Text = "Accès aux factures de la clinique"
        Me._droitacces_82.UseVisualStyleBackColor = False
        '
        '_droitacces_62
        '
        Me._droitacces_62.AutoSize = True
        Me._droitacces_62.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_62.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_62.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_62.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_62.Location = New System.Drawing.Point(6, 36)
        Me._droitacces_62.Name = "_droitacces_62"
        Me._droitacces_62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_62.Size = New System.Drawing.Size(210, 18)
        Me._droitacces_62.TabIndex = 15
        Me._droitacces_62.Text = "Droit de modifier l'horaire de la clinique"
        Me._droitacces_62.UseVisualStyleBackColor = False
        '
        '_droitacces_27
        '
        Me._droitacces_27.AutoSize = True
        Me._droitacces_27.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_27.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_27.Name = "_droitacces_27"
        Me._droitacces_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_27.Size = New System.Drawing.Size(236, 18)
        Me._droitacces_27.TabIndex = 14
        Me._droitacces_27.Text = "Droit de gérer les informations de la clinique"
        Me._droitacces_27.UseVisualStyleBackColor = False
        '
        'GroupDB
        '
        Me.GroupDB.Controls.Add(Me._droitacces_99)
        Me.GroupDB.Controls.Add(Me._droitacces_8)
        Me.GroupDB.Controls.Add(Me._droitacces_7)
        Me.GroupDB.Controls.Add(Me._droitacces_6)
        Me.GroupDB.Controls.Add(Me._droitacces_1)
        Me.GroupDB.Controls.Add(Me._droitacces_97)
        Me.GroupDB.Controls.Add(Me._droitacces_98)
        Me.GroupDB.Controls.Add(Me._droitacces_96)
        Me.GroupDB.Controls.Add(Me._droitacces_5)
        Me.GroupDB.Controls.Add(Me._droitacces_2)
        Me.GroupDB.Controls.Add(Me._droitacces_94)
        Me.GroupDB.Controls.Add(Me._droitacces_95)
        Me.GroupDB.Controls.Add(Me._droitacces_3)
        Me.GroupDB.Controls.Add(Me._droitacces_4)
        Me.GroupDB.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupDB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupDB.Location = New System.Drawing.Point(0, 351)
        Me.GroupDB.Name = "GroupDB"
        Me.GroupDB.Size = New System.Drawing.Size(452, 275)
        Me.GroupDB.TabIndex = 13
        Me.GroupDB.TabStop = False
        Me.GroupDB.Text = "Banque de données"
        '
        '_droitacces_99
        '
        Me._droitacces_99.AutoSize = True
        Me._droitacces_99.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_99.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_99.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_99.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_99.Location = New System.Drawing.Point(6, 253)
        Me._droitacces_99.Name = "_droitacces_99"
        Me._droitacces_99.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_99.Size = New System.Drawing.Size(222, 18)
        Me._droitacces_99.TabIndex = 13
        Me._droitacces_99.Text = "Droit de supprimer les mots-clés existant"
        Me._droitacces_99.UseVisualStyleBackColor = False
        '
        '_droitacces_8
        '
        Me._droitacces_8.AutoSize = True
        Me._droitacces_8.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_8.Location = New System.Drawing.Point(6, 235)
        Me._droitacces_8.Name = "_droitacces_8"
        Me._droitacces_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_8.Size = New System.Drawing.Size(193, 18)
        Me._droitacces_8.TabIndex = 13
        Me._droitacces_8.Text = "Droit de gérer les types de fichiers"
        Me._droitacces_8.UseVisualStyleBackColor = False
        '
        '_droitacces_7
        '
        Me._droitacces_7.AutoSize = True
        Me._droitacces_7.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_7.Location = New System.Drawing.Point(6, 217)
        Me._droitacces_7.Name = "_droitacces_7"
        Me._droitacces_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_7.Size = New System.Drawing.Size(163, 18)
        Me._droitacces_7.TabIndex = 13
        Me._droitacces_7.Text = "Accès aux types de fichiers"
        Me._droitacces_7.UseVisualStyleBackColor = False
        '
        '_droitacces_6
        '
        Me._droitacces_6.AutoSize = True
        Me._droitacces_6.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_6.Location = New System.Drawing.Point(6, 199)
        Me._droitacces_6.Name = "_droitacces_6"
        Me._droitacces_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_6.Size = New System.Drawing.Size(131, 18)
        Me._droitacces_6.TabIndex = 13
        Me._droitacces_6.Text = "Accès à la recherche"
        Me._droitacces_6.UseVisualStyleBackColor = False
        '
        '_droitacces_1
        '
        Me._droitacces_1.AutoSize = True
        Me._droitacces_1.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_1.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_1.Name = "_droitacces_1"
        Me._droitacces_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_1.Size = New System.Drawing.Size(177, 18)
        Me._droitacces_1.TabIndex = 13
        Me._droitacces_1.Text = "Accès à la banque de données"
        Me._droitacces_1.UseVisualStyleBackColor = False
        '
        '_droitacces_97
        '
        Me._droitacces_97.AutoSize = True
        Me._droitacces_97.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_97.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_97.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_97.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_97.Location = New System.Drawing.Point(6, 55)
        Me._droitacces_97.Name = "_droitacces_97"
        Me._droitacces_97.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_97.Size = New System.Drawing.Size(173, 18)
        Me._droitacces_97.TabIndex = 12
        Me._droitacces_97.Text = "Accès aux dossiers généraux"
        Me._droitacces_97.UseVisualStyleBackColor = False
        '
        '_droitacces_98
        '
        Me._droitacces_98.AutoSize = True
        Me._droitacces_98.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_98.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_98.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_98.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_98.Location = New System.Drawing.Point(6, 127)
        Me._droitacces_98.Name = "_droitacces_98"
        Me._droitacces_98.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_98.Size = New System.Drawing.Size(210, 18)
        Me._droitacces_98.TabIndex = 12
        Me._droitacces_98.Text = "Accès à tous les espaces personnels"
        Me._droitacces_98.UseVisualStyleBackColor = False
        '
        '_droitacces_96
        '
        Me._droitacces_96.AutoSize = True
        Me._droitacces_96.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_96.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_96.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_96.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_96.Location = New System.Drawing.Point(6, 145)
        Me._droitacces_96.Name = "_droitacces_96"
        Me._droitacces_96.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_96.Size = New System.Drawing.Size(312, 18)
        Me._droitacces_96.TabIndex = 12
        Me._droitacces_96.Text = "Droit de gérer les dossiers de tous les espaces personnels"
        Me._droitacces_96.UseVisualStyleBackColor = False
        '
        '_droitacces_5
        '
        Me._droitacces_5.AutoSize = True
        Me._droitacces_5.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_5.Location = New System.Drawing.Point(6, 73)
        Me._droitacces_5.Name = "_droitacces_5"
        Me._droitacces_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_5.Size = New System.Drawing.Size(203, 18)
        Me._droitacces_5.TabIndex = 12
        Me._droitacces_5.Text = "Droit de gérer les dossiers généraux"
        Me._droitacces_5.UseVisualStyleBackColor = False
        '
        '_droitacces_2
        '
        Me._droitacces_2.AutoSize = True
        Me._droitacces_2.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_2.Location = New System.Drawing.Point(6, 37)
        Me._droitacces_2.Name = "_droitacces_2"
        Me._droitacces_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_2.Size = New System.Drawing.Size(224, 18)
        Me._droitacces_2.TabIndex = 9
        Me._droitacces_2.Text = "Accès aux dossiers et aux items cachés"
        Me._droitacces_2.UseVisualStyleBackColor = False
        '
        '_droitacces_94
        '
        Me._droitacces_94.AutoSize = True
        Me._droitacces_94.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_94.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_94.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_94.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_94.Location = New System.Drawing.Point(6, 163)
        Me._droitacces_94.Name = "_droitacces_94"
        Me._droitacces_94.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_94.Size = New System.Drawing.Size(435, 18)
        Me._droitacces_94.TabIndex = 10
        Me._droitacces_94.Text = "Droit de gérer les items de tous les espaces personnels (sauf ceux en lecture seu" & _
            "le)"
        Me._droitacces_94.UseVisualStyleBackColor = False
        '
        '_droitacces_95
        '
        Me._droitacces_95.AutoSize = True
        Me._droitacces_95.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_95.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_95.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_95.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_95.Location = New System.Drawing.Point(6, 181)
        Me._droitacces_95.Name = "_droitacces_95"
        Me._droitacces_95.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_95.Size = New System.Drawing.Size(375, 18)
        Me._droitacces_95.TabIndex = 11
        Me._droitacces_95.Text = "Droit de gérer les items de tous les espaces personnels en lecture seule"
        Me._droitacces_95.UseVisualStyleBackColor = False
        '
        '_droitacces_3
        '
        Me._droitacces_3.AutoSize = True
        Me._droitacces_3.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_3.Location = New System.Drawing.Point(6, 91)
        Me._droitacces_3.Name = "_droitacces_3"
        Me._droitacces_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_3.Size = New System.Drawing.Size(326, 18)
        Me._droitacces_3.TabIndex = 10
        Me._droitacces_3.Text = "Droit de gérer les items généraux (sauf ceux en lecture seule)"
        Me._droitacces_3.UseVisualStyleBackColor = False
        '
        '_droitacces_4
        '
        Me._droitacces_4.AutoSize = True
        Me._droitacces_4.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_4.Location = New System.Drawing.Point(6, 109)
        Me._droitacces_4.Name = "_droitacces_4"
        Me._droitacces_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_4.Size = New System.Drawing.Size(266, 18)
        Me._droitacces_4.TabIndex = 11
        Me._droitacces_4.Text = "Droit de gérer les items généraux en lecture seule"
        Me._droitacces_4.UseVisualStyleBackColor = False
        '
        'GroupAgenda
        '
        Me.GroupAgenda.Controls.Add(Me._droitacces_73)
        Me.GroupAgenda.Controls.Add(Me._droitacces_87)
        Me.GroupAgenda.Controls.Add(Me._droitacces_74)
        Me.GroupAgenda.Controls.Add(Me._droitacces_0)
        Me.GroupAgenda.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupAgenda.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupAgenda.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupAgenda.Location = New System.Drawing.Point(0, 259)
        Me.GroupAgenda.Name = "GroupAgenda"
        Me.GroupAgenda.Size = New System.Drawing.Size(452, 92)
        Me.GroupAgenda.TabIndex = 12
        Me.GroupAgenda.TabStop = False
        Me.GroupAgenda.Text = "Agenda"
        '
        '_droitacces_73
        '
        Me._droitacces_73.AutoSize = True
        Me._droitacces_73.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_73.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_73.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_73.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_73.Location = New System.Drawing.Point(6, 36)
        Me._droitacces_73.Name = "_droitacces_73"
        Me._droitacces_73.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_73.Size = New System.Drawing.Size(197, 18)
        Me._droitacces_73.TabIndex = 1
        Me._droitacces_73.Text = "Droit de gérer les plages réservées"
        Me._droitacces_73.UseVisualStyleBackColor = False
        '
        '_droitacces_87
        '
        Me._droitacces_87.AutoSize = True
        Me._droitacces_87.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_87.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_87.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_87.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_87.Location = New System.Drawing.Point(6, 72)
        Me._droitacces_87.Name = "_droitacces_87"
        Me._droitacces_87.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_87.Size = New System.Drawing.Size(215, 18)
        Me._droitacces_87.TabIndex = 1
        Me._droitacces_87.Text = "Droit de modifier l'horaire du thérapeute"
        Me._droitacces_87.UseVisualStyleBackColor = False
        '
        '_droitacces_74
        '
        Me._droitacces_74.AutoSize = True
        Me._droitacces_74.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_74.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_74.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_74.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_74.Location = New System.Drawing.Point(6, 54)
        Me._droitacces_74.Name = "_droitacces_74"
        Me._droitacces_74.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_74.Size = New System.Drawing.Size(191, 18)
        Me._droitacces_74.TabIndex = 1
        Me._droitacces_74.Text = "Droit de gérer les plages bloquées"
        Me._droitacces_74.UseVisualStyleBackColor = False
        '
        '_droitacces_0
        '
        Me._droitacces_0.AutoSize = True
        Me._droitacces_0.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_0.Location = New System.Drawing.Point(6, 19)
        Me._droitacces_0.Name = "_droitacces_0"
        Me._droitacces_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_0.Size = New System.Drawing.Size(153, 18)
        Me._droitacces_0.TabIndex = 1
        Me._droitacces_0.Text = "Accès à tous les agendas"
        Me._droitacces_0.UseVisualStyleBackColor = False
        '
        'GroupAutre
        '
        Me.GroupAutre.Controls.Add(Me._droitacces_61)
        Me.GroupAutre.Controls.Add(Me._droitacces_54)
        Me.GroupAutre.Controls.Add(Me._droitacces_86)
        Me.GroupAutre.Controls.Add(Me._droitacces_109)
        Me.GroupAutre.Controls.Add(Me._droitacces_91)
        Me.GroupAutre.Controls.Add(Me._droitacces_90)
        Me.GroupAutre.Controls.Add(Me._droitacces_84)
        Me.GroupAutre.Controls.Add(Me._droitacces_59)
        Me.GroupAutre.Controls.Add(Me._droitacces_53)
        Me.GroupAutre.Controls.Add(Me._droitacces_64)
        Me.GroupAutre.Controls.Add(Me._droitacces_60)
        Me.GroupAutre.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupAutre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupAutre.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupAutre.Location = New System.Drawing.Point(0, 0)
        Me.GroupAutre.Name = "GroupAutre"
        Me.GroupAutre.Size = New System.Drawing.Size(452, 259)
        Me.GroupAutre.TabIndex = 18
        Me.GroupAutre.TabStop = False
        Me.GroupAutre.Text = "Autre"
        '
        '_droitacces_61
        '
        Me._droitacces_61.AutoSize = True
        Me._droitacces_61.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_61.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_61.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_61.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_61.Location = New System.Drawing.Point(6, 66)
        Me._droitacces_61.Name = "_droitacces_61"
        Me._droitacces_61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_61.Size = New System.Drawing.Size(357, 32)
        Me._droitacces_61.TabIndex = 14
        Me._droitacces_61.Text = "Droit d'ajouter un élément des listes déroulantes dans les formulaires" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ex : (Ajo" & _
            "uter une ville dans le formulaire d'ajout d'un client)"
        Me._droitacces_61.UseVisualStyleBackColor = False
        '
        '_droitacces_54
        '
        Me._droitacces_54.AutoSize = True
        Me._droitacces_54.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_54.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me._droitacces_54.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_54.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_54.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_54.Location = New System.Drawing.Point(6, 98)
        Me._droitacces_54.Name = "_droitacces_54"
        Me._droitacces_54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_54.Size = New System.Drawing.Size(387, 32)
        Me._droitacces_54.TabIndex = 16
        Me._droitacces_54.Text = "Droit de supprimer les éléments des listes déroulantes dans les formulaires" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ex :" & _
            " (Supprimer une ville dans le formulaire d'ajout d'un client)"
        Me._droitacces_54.UseVisualStyleBackColor = False
        '
        '_droitacces_86
        '
        Me._droitacces_86.AutoSize = True
        Me._droitacces_86.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_86.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_86.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_86.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_86.Location = New System.Drawing.Point(6, 166)
        Me._droitacces_86.Name = "_droitacces_86"
        Me._droitacces_86.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_86.Size = New System.Drawing.Size(172, 18)
        Me._droitacces_86.TabIndex = 15
        Me._droitacces_86.Text = "Droit de mettre à jour le logiciel"
        Me._droitacces_86.UseVisualStyleBackColor = False
        '
        '_droitacces_109
        '
        Me._droitacces_109.AutoSize = True
        Me._droitacces_109.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_109.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_109.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_109.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_109.Location = New System.Drawing.Point(6, 220)
        Me._droitacces_109.Name = "_droitacces_109"
        Me._droitacces_109.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_109.Size = New System.Drawing.Size(238, 18)
        Me._droitacces_109.TabIndex = 15
        Me._droitacces_109.Text = "Droit de modifier les configurations du poste"
        Me._droitacces_109.UseVisualStyleBackColor = False
        '
        '_droitacces_91
        '
        Me._droitacces_91.AutoSize = True
        Me._droitacces_91.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_91.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_91.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_91.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_91.Location = New System.Drawing.Point(6, 202)
        Me._droitacces_91.Name = "_droitacces_91"
        Me._droitacces_91.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_91.Size = New System.Drawing.Size(323, 18)
        Me._droitacces_91.TabIndex = 15
        Me._droitacces_91.Text = "Droit de modifier le montant payé même si la facture a un reçu"
        Me._droitacces_91.UseVisualStyleBackColor = False
        '
        '_droitacces_90
        '
        Me._droitacces_90.AutoSize = True
        Me._droitacces_90.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_90.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_90.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_90.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_90.Location = New System.Drawing.Point(6, 184)
        Me._droitacces_90.Name = "_droitacces_90"
        Me._droitacces_90.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_90.Size = New System.Drawing.Size(291, 18)
        Me._droitacces_90.TabIndex = 15
        Me._droitacces_90.Text = "Droit de choisir la date de l'ajustement pour une facture"
        Me._droitacces_90.UseVisualStyleBackColor = False
        '
        '_droitacces_84
        '
        Me._droitacces_84.AutoSize = True
        Me._droitacces_84.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_84.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_84.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_84.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_84.Location = New System.Drawing.Point(6, 148)
        Me._droitacces_84.Name = "_droitacces_84"
        Me._droitacces_84.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_84.Size = New System.Drawing.Size(367, 18)
        Me._droitacces_84.TabIndex = 15
        Me._droitacces_84.Text = "Droit de créer une nouvelle facture à partir du menu Gestion \ Factures"
        Me._droitacces_84.UseVisualStyleBackColor = False
        '
        '_droitacces_59
        '
        Me._droitacces_59.AutoSize = True
        Me._droitacces_59.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_59.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_59.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_59.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_59.Location = New System.Drawing.Point(6, 130)
        Me._droitacces_59.Name = "_droitacces_59"
        Me._droitacces_59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_59.Size = New System.Drawing.Size(358, 18)
        Me._droitacces_59.TabIndex = 15
        Me._droitacces_59.Text = "Droit de modifier le montant d'une facture lors de la prise du paiement"
        Me._droitacces_59.UseVisualStyleBackColor = False
        '
        '_droitacces_53
        '
        Me._droitacces_53.AutoSize = True
        Me._droitacces_53.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_53.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_53.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_53.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_53.Location = New System.Drawing.Point(6, 48)
        Me._droitacces_53.Name = "_droitacces_53"
        Me._droitacces_53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_53.Size = New System.Drawing.Size(302, 18)
        Me._droitacces_53.TabIndex = 14
        Me._droitacces_53.Text = "Droit d'ajouter des liens externes dans les boîtes de texte"
        Me._droitacces_53.UseVisualStyleBackColor = False
        '
        '_droitacces_64
        '
        Me._droitacces_64.AutoSize = True
        Me._droitacces_64.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_64.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_64.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_64.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_64.Location = New System.Drawing.Point(6, 238)
        Me._droitacces_64.Name = "_droitacces_64"
        Me._droitacces_64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_64.Size = New System.Drawing.Size(100, 18)
        Me._droitacces_64.TabIndex = 1
        Me._droitacces_64.Text = "Droit de flagger"
        Me._droitacces_64.UseVisualStyleBackColor = False
        Me._droitacces_64.Visible = False
        '
        '_droitacces_60
        '
        Me._droitacces_60.AutoSize = True
        Me._droitacces_60.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_60.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_60.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_60.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_60.Location = New System.Drawing.Point(6, 16)
        Me._droitacces_60.Name = "_droitacces_60"
        Me._droitacces_60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_60.Size = New System.Drawing.Size(388, 32)
        Me._droitacces_60.TabIndex = 1
        Me._droitacces_60.Text = "Ne pas avoir accès aux options de l'administrateur " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Désactiver cet accès unique" & _
            "ment pour avoir accès aux options spéciales)"
        Me._droitacces_60.UseVisualStyleBackColor = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(8, 11)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(42, 14)
        Me.label1.TabIndex = 2
        Me.label1.Text = "Types :"
        '
        'enlever
        '
        Me.enlever.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.enlever.BackColor = System.Drawing.SystemColors.Control
        Me.enlever.Cursor = System.Windows.Forms.Cursors.Default
        Me.enlever.Enabled = False
        Me.enlever.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.enlever.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.enlever.ForeColor = System.Drawing.SystemColors.ControlText
        Me.enlever.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.enlever.Location = New System.Drawing.Point(464, 8)
        Me.enlever.Name = "enlever"
        Me.enlever.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.enlever.Size = New System.Drawing.Size(24, 24)
        Me.enlever.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.enlever, "Supprimer le type en cours")
        Me.enlever.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'renommer
        '
        Me.renommer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.renommer.BackColor = System.Drawing.SystemColors.Control
        Me.renommer.Cursor = System.Windows.Forms.Cursors.Default
        Me.renommer.Enabled = False
        Me.renommer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.renommer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renommer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.renommer.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.renommer.Location = New System.Drawing.Point(432, 8)
        Me.renommer.Name = "renommer"
        Me.renommer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.renommer.Size = New System.Drawing.Size(24, 24)
        Me.renommer.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.renommer, "Renommer le type en cours")
        Me.renommer.UseVisualStyleBackColor = False
        '
        '_droitacces_111
        '
        Me._droitacces_111.AutoSize = True
        Me._droitacces_111.BackColor = System.Drawing.SystemColors.Control
        Me._droitacces_111.Cursor = System.Windows.Forms.Cursors.Default
        Me._droitacces_111.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._droitacces_111.ForeColor = System.Drawing.SystemColors.ControlText
        Me._droitacces_111.Location = New System.Drawing.Point(6, 73)
        Me._droitacces_111.Name = "_droitacces_111"
        Me._droitacces_111.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._droitacces_111.Size = New System.Drawing.Size(246, 18)
        Me._droitacces_111.TabIndex = 13
        Me._droitacces_111.Text = "Accès à la désactivaction des dossiers en lot"
        Me._droitacces_111.UseVisualStyleBackColor = False
        '
        'typesuser
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(497, 420)
        Me.Controls.Add(Me.modif)
        Me.Controls.Add(Me.add_Renamed)
        Me.Controls.Add(Me.enlever)
        Me.Controls.Add(Me.renommer)
        Me.Controls.Add(Me.therapist)
        Me.Controls.Add(Me.typeu)
        Me.Controls.Add(Me.frameDroitAcces)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.ok)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.Name = "typesuser"
        Me.ShowInTaskbar = False
        Me.Text = "Types d'utilisateurs"
        Me.frameDroitAcces.ResumeLayout(False)
        Me.frameDroitAcces.PerformLayout()
        Me.DAPanel.ResumeLayout(False)
        Me.GroupUser.ResumeLayout(False)
        Me.GroupUser.PerformLayout()
        Me.GroupRapport.ResumeLayout(False)
        Me.GroupRapport.PerformLayout()
        Me.GroupMessagerie.ResumeLayout(False)
        Me.GroupMessagerie.PerformLayout()
        Me.GroupGestion.ResumeLayout(False)
        Me.GroupGestion.PerformLayout()
        Me.GroupKP.ResumeLayout(False)
        Me.GroupKP.PerformLayout()
        Me.GroupRV_QL.ResumeLayout(False)
        Me.GroupRV_QL.PerformLayout()
        Me.GroupClient.ResumeLayout(False)
        Me.GroupClient.PerformLayout()
        Me.GroupClinique.ResumeLayout(False)
        Me.GroupClinique.PerformLayout()
        Me.GroupDB.ResumeLayout(False)
        Me.GroupDB.PerformLayout()
        Me.GroupAgenda.ResumeLayout(False)
        Me.GroupAgenda.PerformLayout()
        Me.GroupAutre.ResumeLayout(False)
        Me.GroupAutre.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub linkdroitaccesArray()
        Dim CurControl, curPanel As Control
        Dim tempControlHash As New Hashtable()

        For Each curPanel In Me.DAPanel.Controls
            For Each CurControl In curPanel.Controls
                If CurControl.Name.StartsWith("_droitacces_") Then
                    Dim myNo As Integer
                    If Integer.TryParse(CurControl.Name.Substring("_droitacces_".Length), myNo) Then tempControlHash.Add(myNo, CurControl)
                    AddHandler CType(CurControl, CheckBox).CheckedChanged, AddressOf alLdroitacces_CheckedChanged
                End If
            Next
        Next

        Me.droitacces = New BaseObjArray()
        Dim i As Integer
        For i = 0 To tempControlHash.Count - 1
            Me.droitacces.add(tempControlHash.Item(i))
        Next i
    End Sub
#End Region

    Dim from As Form
    Private formModified As Boolean = False
    Private oldType As UserType
    Private personnalized As Boolean = False
    Private clickedOK As Boolean = False
    Private selectingAll As Boolean = False
    Private selectingOne As Boolean = False

    Public Function personnalize(ByVal daLine As String) As String
        personnalized = True
        Dim oldDALine As String = daLine
        Dim da() As Boolean
        Dim i As Short
        Me.Text = "Type d'utilisateur personnalisé" 'DON'T CHANGE THIS ONE FOR THE UPDATETEXT FUNCTION BECAUSE THE WINDOW IS NOT OPENED YET!
        typeu.Text = "Personnalisé"
        typeu.Enabled = False
        add_Renamed.Visible = False
        modif.Visible = False
        enlever.Visible = False
        renommer.Visible = False
        ok.Visible = True

        If daLine.Substring(0, 1) = "3" Then
            daLine = daLine.Substring(1)
            therapist.Checked = True
        Else
            therapist.Checked = False
        End If
        da = splitStr(daLine, 1)
        For i = 0 To da.Length - 1
            droitacces(i).Checked = da(i)
        Next i

        Me.MdiParent = Nothing
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.ShowDialog()

        If clickedOK = True Then
            Return buildDroitAccesLine()
        Else
            Return oldDALine
        End If
    End Function

    Private Sub add_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles add_Renamed.Click
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modif_Click(add_Renamed, eventArgs.Empty)

        Dim DALine, NomType, inputMSG As String

        inputMSG = "Veuillez entrer un nom pour ce nouveau type d'utilisateur"

ReAsk:
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        myInputBoxPlus.refusedChars = ",§("
        NomType = myInputBoxPlus(inputMSG, "Type d'utilisateur", "")
        If NomType = "" Then Exit Sub
        If typeu.FindStringExact(NomType) >= 0 Then inputMSG = "Veuillez entrer un nom qui n'existe pas déjà" : GoTo ReAsk

        Dim newUserType As New UserType
        'Ajout dans la base de données
        DALine = buildDroitAccesLine()

        newUserType.name = NomType
        newUserType.rights = DALine
        newUserType.isTherapist = therapist.Checked

        newUserType.saveData()

        'Ajout dans le menu
        typeu.Items.Add(newUserType)
        formModified = False
        typeu.SelectedItem = newUserType

        'Ensure all enable if first added
        lockItems(False)
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        add_Renamed.Enabled = Not trueFalse
        modif.Enabled = Not trueFalse
        renommer.Enabled = Not trueFalse
        enlever.Enabled = Not trueFalse
        therapist.Enabled = Not trueFalse
        selectAllDA.Enabled = Not trueFalse
        Dim i As Short
        With frameDroitAcces
            For i = 0 To droitacces.Count - 1
                droitacces(i).Enabled = Not trueFalse
            Next i
        End With
    End Sub

    Private Sub typesuser_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        If personnalized = False Then
            If lockSecteur("TypeUtilisateur.lock", True, "Types d'utilisateur") = False Then lockItems(True)

            loading()

            typeu.SelectedIndex = typeu.FindStringExact(PreferencesManager.getGeneralPreferences()("LastUserType"))
            If typeu.SelectedIndex = -1 And typeu.Items.Count > 0 Then typeu.SelectedIndex = 0
        End If
    End Sub

    Private Sub typesuser_Closed(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Closed
        If personnalized = False Then
            If PreferencesManager.getGeneralPreferences()("AffLastUserType") = True Then
                PreferencesManager.getGeneralPreferences().setProperty("LastUserType", oldType.toString)
                PreferencesManager.getGeneralPreferences().saveData()
            End If
        End If

        updatingALLTRPMenu()
    End Sub

    Private Sub modif_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles modif.Click
        If oldType Is Nothing Then Exit Sub

        oldType.rights = buildDroitAccesLine()
        oldType.isTherapist = therapist.Checked
        oldType.saveData()

        formModified = False
        loading()
        typeu.SelectedIndex = typeu.FindStringExact(oldType.toString)
    End Sub

    Private Sub ok_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ok.Click
        formModified = False
        clickedOK = True
        Me.Close()
    End Sub

    Private Sub enlever_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles enlever.Click
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modif_Click(enlever, eventArgs.Empty)

        CType(typeu.SelectedItem, UserType).delete()

        loading()

        typeu.SelectedIndex = typeu.FindStringExact(PreferencesManager.getGeneralPreferences()("LastUserType"))
        If typeu.SelectedIndex = -1 AndAlso typeu.Items.Count <> 0 Then typeu.SelectedIndex = 0

        formModified = False 'Important : Pour ne pas redemander la question d'enregistrement
        typeu_SelectedIndexChanged(typeu, New System.EventArgs())
        formModified = False
    End Sub

    Public Sub loading()
        Dim selected As Integer = 0
        If typeu.Items.Count <> 0 Then selected = CType(typeu.SelectedItem, UserType).noUserType

        typeu.Items.Clear()
        typeu.Items.AddRange(UserTypeManager.getInstance.getUserTypes.ToArray)
        If typeu.Items.Count = 0 Then
            lockItems(True)
            add_Renamed.Enabled = True
            Exit Sub
        End If

        If selected <> 0 Then
            For Each curUT As UserType In typeu.Items
                If curUT.noUserType = selected Then
                    typeu.SelectedItem = curUT
                    Exit For
                End If
            Next
        End If
        If typeu.SelectedIndex = -1 Then typeu.SelectedIndex = 0
    End Sub

    Private Sub typeu_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles typeu.SelectedIndexChanged
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modif_Click(typeu, eventArgs.Empty)

        Dim i As Integer
        Dim daLine As String
        Dim da() As Char
        If add_Renamed.Enabled = True Then
            If typeu.GetItemText(typeu.SelectedItem).ToUpper = "* DÉFAUT *" Or typeu.SelectedIndex < 0 Then
                enlever.Enabled = False
                renommer.Enabled = False
            Else
                enlever.Enabled = True
                renommer.Enabled = True
            End If

            If typeu.SelectedIndex > -1 Then
                modif.Enabled = True
            Else
                modif.Enabled = False
            End If
        End If

        'DA=Droits et accès
        If typeu.SelectedIndex > -1 Then
            daLine = CType(typeu.SelectedItem, UserType).rights
            If daLine.Substring(0, 1) = "3" Then
                daLine = daLine.Substring(1)
                therapist.Checked = True
            Else
                therapist.Checked = False
            End If
            da = splitStr(daLine)
            For i = 1 To da.Length - 1
                droitacces(i - 1).Checked = da(i).ToString
            Next i
        End If

        formModified = False
        oldType = typeu.SelectedItem
    End Sub

    Private Sub typesuser_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        from.Focus()
    End Sub

    Private Sub therapist_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles therapist.CheckedChanged
        formModified = True
    End Sub

    Private Sub typesuser_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If add_Renamed.Enabled = True And personnalized = False Then
            lockSecteur("TypeUtilisateur.lock", False)
            If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modif_Click(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub typesuser_Saving(ByVal sender As Object, ByVal e As EventArgs)
        If add_Renamed.Enabled = True And personnalized = False Then
            lockSecteur("TypeUtilisateur.lock", False)
            If formModified = True Then modif_Click(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub alLdroitacces_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        formModified = True
        If selectingAll = False Then
            Dim i As Integer
            Dim selectAllChecked As Boolean = True
            For i = 0 To droitacces.Count - 1
                If CType(droitacces(i), CheckBox).Checked = False And CType(droitacces(i), CheckBox).Name <> "_droitacces_64" Then selectAllChecked = False : Exit For
            Next i
            selectingOne = True
            selectAllDA.Checked = selectAllChecked
            selectingOne = False
        End If
    End Sub

    Private Sub renommer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles renommer.Click
        If formModified = True Then If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = DialogResult.Yes Then modif_Click(typeu, EventArgs.Empty)

        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        Dim lastType As Integer = -1

        With typeu
            Dim oldName As String = typeu.Text
            Dim myName As String = myInputBoxPlus("Veuillez entrer un nouveau nom pour ce type", "Nouveau nom", oldName)
            If myName = "" Or myName = oldName Then Exit Sub
            Dim i As Short

            For i = 0 To .Items.Count - 1
                If .Items(i).ToString = myName Then MessageBox.Show("Un type possède déjà ce nom. Veuillez en choisir un autre", "Nom déjà existant") : Exit Sub
            Next i

            With CType(typeu.SelectedItem, UserType)
                .name = myName
                .saveData()
            End With

            formModified = False
            lastType = typeu.SelectedIndex
            loading()
            typeu.SelectedIndex = lastType

            myMainWin.StatusText = "Types d'utilisateur : Type " & oldName & " renommé pour " & myName
        End With
    End Sub

    Private Sub selectAllDA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectAllDA.CheckedChanged
        If selectingOne = True Then Exit Sub

        Dim i As Integer
        selectingAll = True
        For i = 0 To droitacces.Count - 1
            If CType(droitacces(i), CheckBox).Name <> "_droitacces_64" Then CType(droitacces(i), CheckBox).Checked = selectAllDA.Checked
        Next i
        selectingAll = False
    End Sub

    Private Function buildDroitAccesLine() As String
        Dim daLine As String = "2"
        Dim i As Integer
        For i = 0 To droitacces.Count - 1
            If droitacces(i).Checked = True Then
                daLine = daLine & "1"
            Else
                daLine = daLine & "0"
            End If
        Next i
        If therapist.Checked = True Then daLine = "3" & daLine

        Return daLine
    End Function

    Private keysDown As New ArrayList

    Private Sub typesuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me._droitacces_64.Visible Then Exit Sub

        If e.Control = True And e.Shift = True And e.Alt = True And e.KeyCode <> Keys.Menu Then
            e.SuppressKeyPress = False
            If keysDown.Contains(e.KeyCode) = False Then keysDown.Add(e.KeyCode)
            If keysDown.Contains(Keys.B) And keysDown.Contains(Keys.U) And keysDown.Contains(Keys.D9) And keysDown.Contains(Keys.P) Then
                Me._droitacces_64.Visible = True
                Me.GroupAutre.Height += 22
            End If
        End If
    End Sub

    Private Sub typesuser_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.Control = True And e.Shift = True And e.Alt = True Then
            e.SuppressKeyPress = False
            If keysDown.Contains(e.KeyCode.ToString()) = True Then keysDown.Remove(e.KeyCode.ToString())
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf typesuser_Saving)
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If personnalized OrElse dataReceived.function <> "UserTypes" OrElse dataReceived.fromExternal = False Then Exit Sub

        loading()
    End Sub
End Class
