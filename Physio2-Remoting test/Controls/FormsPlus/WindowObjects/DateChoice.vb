Option Strict Off
Option Explicit On
Friend Class DateChoice
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        linkcasesArray()

        AddHandler Me.KeyDown, AddressOf keyDownCentralized
        AddHandler Me.KeyUp, AddressOf keyUpCentralized
        Dim myControl As Control
        For Each myControl In Me.Controls
            AddHandler myControl.KeyDown, AddressOf keyDownCentralized
            AddHandler myControl.KeyUp, AddressOf keyUpCentralized
        Next myControl
        REM TO DO IN OVERRIDE Me.Icon = DrawingManager.GetInstance.GetIcon("calendrier16.ico")
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            RemoveHandler Me.KeyDown, AddressOf keyDownCentralized
            RemoveHandler Me.KeyUp, AddressOf keyUpCentralized
            For Each MyControl As Control In Me.Controls
                RemoveHandler MyControl.KeyDown, AddressOf keyDownCentralized
                RemoveHandler MyControl.KeyUp, AddressOf keyUpCentralized
            Next MyControl
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private WithEvents hh As System.Windows.Forms.ComboBox
    Private WithEvents mm As System.Windows.Forms.ComboBox
    Private WithEvents okbutton As System.Windows.Forms.Button
    Private WithEvents annee As System.Windows.Forms.ComboBox
    Private WithEvents mois As System.Windows.Forms.ComboBox
    Private WithEvents dateSelected As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents _cases_41 As System.Windows.Forms.Label
    Private WithEvents _cases_40 As System.Windows.Forms.Label
    Private WithEvents _cases_39 As System.Windows.Forms.Label
    Private WithEvents _cases_38 As System.Windows.Forms.Label
    Private WithEvents _cases_37 As System.Windows.Forms.Label
    Private WithEvents _cases_36 As System.Windows.Forms.Label
    Private WithEvents _cases_35 As System.Windows.Forms.Label
    Private WithEvents _cases_34 As System.Windows.Forms.Label
    Private WithEvents _cases_33 As System.Windows.Forms.Label
    Private WithEvents _cases_32 As System.Windows.Forms.Label
    Private WithEvents _cases_31 As System.Windows.Forms.Label
    Private WithEvents _cases_30 As System.Windows.Forms.Label
    Private WithEvents _cases_29 As System.Windows.Forms.Label
    Private WithEvents _cases_28 As System.Windows.Forms.Label
    Private WithEvents _cases_27 As System.Windows.Forms.Label
    Private WithEvents _cases_26 As System.Windows.Forms.Label
    Private WithEvents _cases_25 As System.Windows.Forms.Label
    Private WithEvents _cases_24 As System.Windows.Forms.Label
    Private WithEvents _cases_23 As System.Windows.Forms.Label
    Private WithEvents _cases_22 As System.Windows.Forms.Label
    Private WithEvents _cases_21 As System.Windows.Forms.Label
    Private WithEvents _cases_20 As System.Windows.Forms.Label
    Private WithEvents _cases_19 As System.Windows.Forms.Label
    Private WithEvents _cases_18 As System.Windows.Forms.Label
    Private WithEvents _cases_17 As System.Windows.Forms.Label
    Private WithEvents _cases_16 As System.Windows.Forms.Label
    Private WithEvents _cases_15 As System.Windows.Forms.Label
    Private WithEvents _cases_14 As System.Windows.Forms.Label
    Private WithEvents _cases_13 As System.Windows.Forms.Label
    Private WithEvents _cases_12 As System.Windows.Forms.Label
    Private WithEvents _cases_11 As System.Windows.Forms.Label
    Private WithEvents _cases_10 As System.Windows.Forms.Label
    Private WithEvents _cases_9 As System.Windows.Forms.Label
    Private WithEvents _cases_8 As System.Windows.Forms.Label
    Private WithEvents _cases_7 As System.Windows.Forms.Label
    Private WithEvents _cases_6 As System.Windows.Forms.Label
    Private WithEvents _cases_5 As System.Windows.Forms.Label
    Private WithEvents _cases_4 As System.Windows.Forms.Label
    Private WithEvents _cases_3 As System.Windows.Forms.Label
    Private WithEvents _cases_2 As System.Windows.Forms.Label
    Private WithEvents _cases_1 As System.Windows.Forms.Label
    Private WithEvents _cases_0 As System.Windows.Forms.Label
    Private WithEvents cases As BaseObjArray
    Private WithEvents _Label2_6 As System.Windows.Forms.Label
    Private WithEvents _Label2_5 As System.Windows.Forms.Label
    Private WithEvents _Label2_4 As System.Windows.Forms.Label
    Private WithEvents _Label2_3 As System.Windows.Forms.Label
    Private WithEvents _Label2_2 As System.Windows.Forms.Label
    Private WithEvents _Label2_1 As System.Windows.Forms.Label
    Private WithEvents _Label2_0 As System.Windows.Forms.Label
    Private WithEvents texteEnHaut As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Private WithEvents _Lines_5 As System.Windows.Forms.Label
    Private WithEvents _Lines_10 As System.Windows.Forms.Label
    Private WithEvents _Lines_9 As System.Windows.Forms.Label
    Private WithEvents _Lines_8 As System.Windows.Forms.Label
    Private WithEvents _Lines_6 As System.Windows.Forms.Label
    Private WithEvents _Lines_4 As System.Windows.Forms.Label
    Private WithEvents _Lines_18 As System.Windows.Forms.Label
    Private WithEvents _Lines_17 As System.Windows.Forms.Label
    Private WithEvents _Lines_16 As System.Windows.Forms.Label
    Private WithEvents _Lines_15 As System.Windows.Forms.Label
    Private WithEvents _Lines_14 As System.Windows.Forms.Label
    Private WithEvents _Lines_13 As System.Windows.Forms.Label
    Private WithEvents _Lines_12 As System.Windows.Forms.Label
    Private WithEvents _Lines_11 As System.Windows.Forms.Label
    Private WithEvents _Lines_7 As System.Windows.Forms.Label
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.hh = New System.Windows.Forms.ComboBox
        Me.mm = New System.Windows.Forms.ComboBox
        Me.okbutton = New System.Windows.Forms.Button
        Me.annee = New System.Windows.Forms.ComboBox
        Me.mois = New System.Windows.Forms.ComboBox
        Me.dateSelected = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me._cases_41 = New System.Windows.Forms.Label
        Me._cases_40 = New System.Windows.Forms.Label
        Me._cases_39 = New System.Windows.Forms.Label
        Me._cases_38 = New System.Windows.Forms.Label
        Me._cases_37 = New System.Windows.Forms.Label
        Me._cases_36 = New System.Windows.Forms.Label
        Me._cases_35 = New System.Windows.Forms.Label
        Me._cases_34 = New System.Windows.Forms.Label
        Me._cases_33 = New System.Windows.Forms.Label
        Me._cases_32 = New System.Windows.Forms.Label
        Me._cases_31 = New System.Windows.Forms.Label
        Me._cases_30 = New System.Windows.Forms.Label
        Me._cases_29 = New System.Windows.Forms.Label
        Me._cases_28 = New System.Windows.Forms.Label
        Me._cases_27 = New System.Windows.Forms.Label
        Me._cases_26 = New System.Windows.Forms.Label
        Me._cases_25 = New System.Windows.Forms.Label
        Me._cases_24 = New System.Windows.Forms.Label
        Me._cases_23 = New System.Windows.Forms.Label
        Me._cases_22 = New System.Windows.Forms.Label
        Me._cases_21 = New System.Windows.Forms.Label
        Me._cases_20 = New System.Windows.Forms.Label
        Me._cases_19 = New System.Windows.Forms.Label
        Me._cases_18 = New System.Windows.Forms.Label
        Me._cases_17 = New System.Windows.Forms.Label
        Me._cases_16 = New System.Windows.Forms.Label
        Me._cases_15 = New System.Windows.Forms.Label
        Me._cases_14 = New System.Windows.Forms.Label
        Me._cases_13 = New System.Windows.Forms.Label
        Me._cases_12 = New System.Windows.Forms.Label
        Me._cases_11 = New System.Windows.Forms.Label
        Me._cases_10 = New System.Windows.Forms.Label
        Me._cases_9 = New System.Windows.Forms.Label
        Me._cases_8 = New System.Windows.Forms.Label
        Me._cases_7 = New System.Windows.Forms.Label
        Me._cases_6 = New System.Windows.Forms.Label
        Me._cases_5 = New System.Windows.Forms.Label
        Me._cases_4 = New System.Windows.Forms.Label
        Me._cases_3 = New System.Windows.Forms.Label
        Me._cases_2 = New System.Windows.Forms.Label
        Me._cases_1 = New System.Windows.Forms.Label
        Me._cases_0 = New System.Windows.Forms.Label
        Me._Label2_6 = New System.Windows.Forms.Label
        Me._Label2_5 = New System.Windows.Forms.Label
        Me._Label2_4 = New System.Windows.Forms.Label
        Me._Label2_3 = New System.Windows.Forms.Label
        Me._Label2_2 = New System.Windows.Forms.Label
        Me._Label2_1 = New System.Windows.Forms.Label
        Me._Label2_0 = New System.Windows.Forms.Label
        Me.texteEnHaut = New System.Windows.Forms.Label
        Me._Lines_5 = New System.Windows.Forms.Label
        Me._Lines_10 = New System.Windows.Forms.Label
        Me._Lines_9 = New System.Windows.Forms.Label
        Me._Lines_8 = New System.Windows.Forms.Label
        Me._Lines_6 = New System.Windows.Forms.Label
        Me._Lines_4 = New System.Windows.Forms.Label
        Me._Lines_18 = New System.Windows.Forms.Label
        Me._Lines_17 = New System.Windows.Forms.Label
        Me._Lines_16 = New System.Windows.Forms.Label
        Me._Lines_15 = New System.Windows.Forms.Label
        Me._Lines_14 = New System.Windows.Forms.Label
        Me._Lines_13 = New System.Windows.Forms.Label
        Me._Lines_12 = New System.Windows.Forms.Label
        Me._Lines_11 = New System.Windows.Forms.Label
        Me._Lines_7 = New System.Windows.Forms.Label
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'hh
        '
        Me.hh.BackColor = System.Drawing.SystemColors.Window
        Me.hh.Cursor = System.Windows.Forms.Cursors.Default
        Me.hh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.hh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hh.ForeColor = System.Drawing.SystemColors.WindowText
        Me.hh.Location = New System.Drawing.Point(12, 184)
        Me.hh.Name = "hh"
        Me.hh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.hh.Size = New System.Drawing.Size(41, 22)
        Me.hh.TabIndex = 53
        '
        'mm
        '
        Me.mm.BackColor = System.Drawing.SystemColors.Window
        Me.mm.Cursor = System.Windows.Forms.Cursors.Default
        Me.mm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mm.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mm.Items.AddRange(New Object() {"00", "15", "30", "45"})
        Me.mm.Location = New System.Drawing.Point(52, 184)
        Me.mm.Name = "mm"
        Me.mm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mm.Size = New System.Drawing.Size(41, 22)
        Me.mm.TabIndex = 54
        '
        'okbutton
        '
        Me.okbutton.BackColor = System.Drawing.SystemColors.Control
        Me.okbutton.Cursor = System.Windows.Forms.Cursors.Default
        Me.okbutton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.okbutton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.okbutton.Location = New System.Drawing.Point(104, 176)
        Me.okbutton.Name = "okbutton"
        Me.okbutton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.okbutton.Size = New System.Drawing.Size(73, 25)
        Me.okbutton.TabIndex = 56
        Me.okbutton.Text = "OK"
        Me.okbutton.UseVisualStyleBackColor = False
        '
        'annee
        '
        Me.annee.BackColor = System.Drawing.SystemColors.Window
        Me.annee.Cursor = System.Windows.Forms.Cursors.Default
        Me.annee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.annee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.annee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.annee.Location = New System.Drawing.Point(104, 144)
        Me.annee.Name = "annee"
        Me.annee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.annee.Size = New System.Drawing.Size(81, 22)
        Me.annee.Sorted = True
        Me.annee.TabIndex = 9
        '
        'mois
        '
        Me.mois.BackColor = System.Drawing.SystemColors.Window
        Me.mois.Cursor = System.Windows.Forms.Cursors.Default
        Me.mois.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mois.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mois.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mois.Items.AddRange(New Object() {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"})
        Me.mois.Location = New System.Drawing.Point(8, 144)
        Me.mois.Name = "mois"
        Me.mois.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mois.Size = New System.Drawing.Size(89, 22)
        Me.mois.TabIndex = 8
        '
        'DateSelected
        '
        Me.dateSelected.AutoSize = True
        Me.dateSelected.BackColor = System.Drawing.SystemColors.Control
        Me.dateSelected.Cursor = System.Windows.Forms.Cursors.Default
        Me.dateSelected.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateSelected.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dateSelected.Location = New System.Drawing.Point(8, 48)
        Me.dateSelected.Name = "DateSelected"
        Me.dateSelected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dateSelected.Size = New System.Drawing.Size(0, 14)
        Me.dateSelected.TabIndex = 55
        Me.dateSelected.Visible = False
        '
        'Label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Location = New System.Drawing.Point(5, 168)
        Me.label3.Name = "Label3"
        Me.label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label3.Size = New System.Drawing.Size(94, 14)
        Me.label3.TabIndex = 52
        Me.label3.Text = "Heure (HH/MM) :"
        '
        '_cases_41
        '
        Me._cases_41.BackColor = System.Drawing.Color.Transparent
        Me._cases_41.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_41.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_41.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_41.Location = New System.Drawing.Point(136, 120)
        Me._cases_41.Name = "_cases_41"
        Me._cases_41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_41.Size = New System.Drawing.Size(16, 16)
        Me._cases_41.TabIndex = 51
        Me._cases_41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_40
        '
        Me._cases_40.BackColor = System.Drawing.Color.Transparent
        Me._cases_40.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_40.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_40.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_40.Location = New System.Drawing.Point(120, 120)
        Me._cases_40.Name = "_cases_40"
        Me._cases_40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_40.Size = New System.Drawing.Size(16, 16)
        Me._cases_40.TabIndex = 50
        Me._cases_40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_39
        '
        Me._cases_39.BackColor = System.Drawing.Color.Transparent
        Me._cases_39.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_39.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_39.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_39.Location = New System.Drawing.Point(104, 120)
        Me._cases_39.Name = "_cases_39"
        Me._cases_39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_39.Size = New System.Drawing.Size(16, 16)
        Me._cases_39.TabIndex = 49
        Me._cases_39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_38
        '
        Me._cases_38.BackColor = System.Drawing.Color.Transparent
        Me._cases_38.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_38.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_38.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_38.Location = New System.Drawing.Point(88, 120)
        Me._cases_38.Name = "_cases_38"
        Me._cases_38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_38.Size = New System.Drawing.Size(16, 16)
        Me._cases_38.TabIndex = 48
        Me._cases_38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_37
        '
        Me._cases_37.BackColor = System.Drawing.Color.Transparent
        Me._cases_37.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_37.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_37.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_37.Location = New System.Drawing.Point(72, 120)
        Me._cases_37.Name = "_cases_37"
        Me._cases_37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_37.Size = New System.Drawing.Size(16, 16)
        Me._cases_37.TabIndex = 47
        Me._cases_37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_36
        '
        Me._cases_36.BackColor = System.Drawing.Color.Transparent
        Me._cases_36.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_36.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_36.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_36.Location = New System.Drawing.Point(56, 120)
        Me._cases_36.Name = "_cases_36"
        Me._cases_36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_36.Size = New System.Drawing.Size(16, 16)
        Me._cases_36.TabIndex = 46
        Me._cases_36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_35
        '
        Me._cases_35.BackColor = System.Drawing.Color.Transparent
        Me._cases_35.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_35.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_35.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_35.Location = New System.Drawing.Point(40, 120)
        Me._cases_35.Name = "_cases_35"
        Me._cases_35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_35.Size = New System.Drawing.Size(16, 16)
        Me._cases_35.TabIndex = 45
        Me._cases_35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_34
        '
        Me._cases_34.BackColor = System.Drawing.Color.Transparent
        Me._cases_34.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_34.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_34.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_34.Location = New System.Drawing.Point(136, 104)
        Me._cases_34.Name = "_cases_34"
        Me._cases_34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_34.Size = New System.Drawing.Size(16, 16)
        Me._cases_34.TabIndex = 44
        Me._cases_34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_33
        '
        Me._cases_33.BackColor = System.Drawing.Color.Transparent
        Me._cases_33.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_33.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_33.Location = New System.Drawing.Point(120, 104)
        Me._cases_33.Name = "_cases_33"
        Me._cases_33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_33.Size = New System.Drawing.Size(16, 16)
        Me._cases_33.TabIndex = 43
        Me._cases_33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_32
        '
        Me._cases_32.BackColor = System.Drawing.Color.Transparent
        Me._cases_32.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_32.Location = New System.Drawing.Point(104, 104)
        Me._cases_32.Name = "_cases_32"
        Me._cases_32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_32.Size = New System.Drawing.Size(16, 16)
        Me._cases_32.TabIndex = 42
        Me._cases_32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_31
        '
        Me._cases_31.BackColor = System.Drawing.Color.Transparent
        Me._cases_31.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_31.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_31.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_31.Location = New System.Drawing.Point(88, 104)
        Me._cases_31.Name = "_cases_31"
        Me._cases_31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_31.Size = New System.Drawing.Size(16, 16)
        Me._cases_31.TabIndex = 41
        Me._cases_31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_30
        '
        Me._cases_30.BackColor = System.Drawing.Color.Transparent
        Me._cases_30.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_30.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_30.Location = New System.Drawing.Point(72, 104)
        Me._cases_30.Name = "_cases_30"
        Me._cases_30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_30.Size = New System.Drawing.Size(16, 16)
        Me._cases_30.TabIndex = 40
        Me._cases_30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_29
        '
        Me._cases_29.BackColor = System.Drawing.Color.Transparent
        Me._cases_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_29.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_29.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_29.Location = New System.Drawing.Point(56, 104)
        Me._cases_29.Name = "_cases_29"
        Me._cases_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_29.Size = New System.Drawing.Size(16, 16)
        Me._cases_29.TabIndex = 39
        Me._cases_29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_28
        '
        Me._cases_28.BackColor = System.Drawing.Color.Transparent
        Me._cases_28.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_28.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_28.Location = New System.Drawing.Point(40, 104)
        Me._cases_28.Name = "_cases_28"
        Me._cases_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_28.Size = New System.Drawing.Size(16, 16)
        Me._cases_28.TabIndex = 38
        Me._cases_28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_27
        '
        Me._cases_27.BackColor = System.Drawing.Color.Transparent
        Me._cases_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_27.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_27.Location = New System.Drawing.Point(136, 88)
        Me._cases_27.Name = "_cases_27"
        Me._cases_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_27.Size = New System.Drawing.Size(16, 16)
        Me._cases_27.TabIndex = 37
        Me._cases_27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_26
        '
        Me._cases_26.BackColor = System.Drawing.Color.Transparent
        Me._cases_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_26.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_26.Location = New System.Drawing.Point(120, 88)
        Me._cases_26.Name = "_cases_26"
        Me._cases_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_26.Size = New System.Drawing.Size(16, 16)
        Me._cases_26.TabIndex = 36
        Me._cases_26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_25
        '
        Me._cases_25.BackColor = System.Drawing.Color.Transparent
        Me._cases_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_25.Location = New System.Drawing.Point(104, 88)
        Me._cases_25.Name = "_cases_25"
        Me._cases_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_25.Size = New System.Drawing.Size(16, 16)
        Me._cases_25.TabIndex = 35
        Me._cases_25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_24
        '
        Me._cases_24.BackColor = System.Drawing.Color.Transparent
        Me._cases_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_24.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_24.Location = New System.Drawing.Point(88, 88)
        Me._cases_24.Name = "_cases_24"
        Me._cases_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_24.Size = New System.Drawing.Size(16, 16)
        Me._cases_24.TabIndex = 34
        Me._cases_24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_23
        '
        Me._cases_23.BackColor = System.Drawing.Color.Transparent
        Me._cases_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_23.Location = New System.Drawing.Point(72, 88)
        Me._cases_23.Name = "_cases_23"
        Me._cases_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_23.Size = New System.Drawing.Size(16, 16)
        Me._cases_23.TabIndex = 33
        Me._cases_23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_22
        '
        Me._cases_22.BackColor = System.Drawing.Color.Transparent
        Me._cases_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_22.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_22.Location = New System.Drawing.Point(56, 88)
        Me._cases_22.Name = "_cases_22"
        Me._cases_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_22.Size = New System.Drawing.Size(16, 16)
        Me._cases_22.TabIndex = 32
        Me._cases_22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_21
        '
        Me._cases_21.BackColor = System.Drawing.Color.Transparent
        Me._cases_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_21.Location = New System.Drawing.Point(40, 88)
        Me._cases_21.Name = "_cases_21"
        Me._cases_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_21.Size = New System.Drawing.Size(16, 16)
        Me._cases_21.TabIndex = 31
        Me._cases_21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_20
        '
        Me._cases_20.BackColor = System.Drawing.Color.Transparent
        Me._cases_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_20.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_20.Location = New System.Drawing.Point(136, 72)
        Me._cases_20.Name = "_cases_20"
        Me._cases_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_20.Size = New System.Drawing.Size(16, 16)
        Me._cases_20.TabIndex = 30
        Me._cases_20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_19
        '
        Me._cases_19.BackColor = System.Drawing.Color.Transparent
        Me._cases_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_19.Location = New System.Drawing.Point(120, 72)
        Me._cases_19.Name = "_cases_19"
        Me._cases_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_19.Size = New System.Drawing.Size(16, 16)
        Me._cases_19.TabIndex = 29
        Me._cases_19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_18
        '
        Me._cases_18.BackColor = System.Drawing.Color.Transparent
        Me._cases_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_18.Location = New System.Drawing.Point(104, 72)
        Me._cases_18.Name = "_cases_18"
        Me._cases_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_18.Size = New System.Drawing.Size(16, 16)
        Me._cases_18.TabIndex = 28
        Me._cases_18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_17
        '
        Me._cases_17.BackColor = System.Drawing.Color.Transparent
        Me._cases_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_17.Location = New System.Drawing.Point(88, 72)
        Me._cases_17.Name = "_cases_17"
        Me._cases_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_17.Size = New System.Drawing.Size(16, 16)
        Me._cases_17.TabIndex = 27
        Me._cases_17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_16
        '
        Me._cases_16.BackColor = System.Drawing.Color.Transparent
        Me._cases_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_16.Location = New System.Drawing.Point(72, 72)
        Me._cases_16.Name = "_cases_16"
        Me._cases_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_16.Size = New System.Drawing.Size(16, 16)
        Me._cases_16.TabIndex = 26
        Me._cases_16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_15
        '
        Me._cases_15.BackColor = System.Drawing.Color.Transparent
        Me._cases_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_15.Location = New System.Drawing.Point(56, 72)
        Me._cases_15.Name = "_cases_15"
        Me._cases_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_15.Size = New System.Drawing.Size(16, 16)
        Me._cases_15.TabIndex = 25
        Me._cases_15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_14
        '
        Me._cases_14.BackColor = System.Drawing.Color.Transparent
        Me._cases_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_14.Location = New System.Drawing.Point(40, 72)
        Me._cases_14.Name = "_cases_14"
        Me._cases_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_14.Size = New System.Drawing.Size(16, 16)
        Me._cases_14.TabIndex = 24
        Me._cases_14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_13
        '
        Me._cases_13.BackColor = System.Drawing.Color.Transparent
        Me._cases_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_13.Location = New System.Drawing.Point(136, 56)
        Me._cases_13.Name = "_cases_13"
        Me._cases_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_13.Size = New System.Drawing.Size(16, 16)
        Me._cases_13.TabIndex = 23
        Me._cases_13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_12
        '
        Me._cases_12.BackColor = System.Drawing.Color.Transparent
        Me._cases_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_12.Location = New System.Drawing.Point(120, 56)
        Me._cases_12.Name = "_cases_12"
        Me._cases_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_12.Size = New System.Drawing.Size(16, 16)
        Me._cases_12.TabIndex = 22
        Me._cases_12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_11
        '
        Me._cases_11.BackColor = System.Drawing.Color.Transparent
        Me._cases_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_11.Location = New System.Drawing.Point(104, 56)
        Me._cases_11.Name = "_cases_11"
        Me._cases_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_11.Size = New System.Drawing.Size(16, 16)
        Me._cases_11.TabIndex = 21
        Me._cases_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_10
        '
        Me._cases_10.BackColor = System.Drawing.Color.Transparent
        Me._cases_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_10.Location = New System.Drawing.Point(88, 56)
        Me._cases_10.Name = "_cases_10"
        Me._cases_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_10.Size = New System.Drawing.Size(16, 16)
        Me._cases_10.TabIndex = 20
        Me._cases_10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_9
        '
        Me._cases_9.BackColor = System.Drawing.Color.Transparent
        Me._cases_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_9.Location = New System.Drawing.Point(72, 56)
        Me._cases_9.Name = "_cases_9"
        Me._cases_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_9.Size = New System.Drawing.Size(16, 16)
        Me._cases_9.TabIndex = 19
        Me._cases_9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_8
        '
        Me._cases_8.BackColor = System.Drawing.Color.Transparent
        Me._cases_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_8.Location = New System.Drawing.Point(56, 56)
        Me._cases_8.Name = "_cases_8"
        Me._cases_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_8.Size = New System.Drawing.Size(16, 16)
        Me._cases_8.TabIndex = 18
        Me._cases_8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_7
        '
        Me._cases_7.BackColor = System.Drawing.Color.Transparent
        Me._cases_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_7.Location = New System.Drawing.Point(40, 56)
        Me._cases_7.Name = "_cases_7"
        Me._cases_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_7.Size = New System.Drawing.Size(16, 16)
        Me._cases_7.TabIndex = 17
        Me._cases_7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_6
        '
        Me._cases_6.BackColor = System.Drawing.Color.Transparent
        Me._cases_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_6.Location = New System.Drawing.Point(136, 40)
        Me._cases_6.Name = "_cases_6"
        Me._cases_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_6.Size = New System.Drawing.Size(16, 16)
        Me._cases_6.TabIndex = 16
        Me._cases_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_5
        '
        Me._cases_5.BackColor = System.Drawing.Color.Transparent
        Me._cases_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_5.Location = New System.Drawing.Point(120, 40)
        Me._cases_5.Name = "_cases_5"
        Me._cases_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_5.Size = New System.Drawing.Size(16, 16)
        Me._cases_5.TabIndex = 15
        Me._cases_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_4
        '
        Me._cases_4.BackColor = System.Drawing.Color.Transparent
        Me._cases_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_4.Location = New System.Drawing.Point(104, 40)
        Me._cases_4.Name = "_cases_4"
        Me._cases_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_4.Size = New System.Drawing.Size(16, 16)
        Me._cases_4.TabIndex = 14
        Me._cases_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_3
        '
        Me._cases_3.BackColor = System.Drawing.Color.Transparent
        Me._cases_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_3.Location = New System.Drawing.Point(88, 40)
        Me._cases_3.Name = "_cases_3"
        Me._cases_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_3.Size = New System.Drawing.Size(16, 16)
        Me._cases_3.TabIndex = 13
        Me._cases_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_2
        '
        Me._cases_2.BackColor = System.Drawing.Color.Transparent
        Me._cases_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_2.Location = New System.Drawing.Point(72, 40)
        Me._cases_2.Name = "_cases_2"
        Me._cases_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_2.Size = New System.Drawing.Size(16, 16)
        Me._cases_2.TabIndex = 12
        Me._cases_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_1
        '
        Me._cases_1.BackColor = System.Drawing.Color.Transparent
        Me._cases_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_1.Location = New System.Drawing.Point(56, 40)
        Me._cases_1.Name = "_cases_1"
        Me._cases_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_1.Size = New System.Drawing.Size(16, 16)
        Me._cases_1.TabIndex = 11
        Me._cases_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_cases_0
        '
        Me._cases_0.BackColor = System.Drawing.Color.Transparent
        Me._cases_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cases_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cases_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cases_0.Location = New System.Drawing.Point(40, 40)
        Me._cases_0.Name = "_cases_0"
        Me._cases_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cases_0.Size = New System.Drawing.Size(16, 16)
        Me._cases_0.TabIndex = 10
        Me._cases_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_Label2_6
        '
        Me._Label2_6.AutoSize = True
        Me._Label2_6.BackColor = System.Drawing.Color.Transparent
        Me._Label2_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_6.Location = New System.Drawing.Point(76, 24)
        Me._Label2_6.Name = "_Label2_6"
        Me._Label2_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_6.Size = New System.Drawing.Size(17, 14)
        Me._Label2_6.TabIndex = 7
        Me._Label2_6.Text = "M"
        '
        '_Label2_5
        '
        Me._Label2_5.AutoSize = True
        Me._Label2_5.BackColor = System.Drawing.Color.Transparent
        Me._Label2_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_5.Location = New System.Drawing.Point(92, 24)
        Me._Label2_5.Name = "_Label2_5"
        Me._Label2_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_5.Size = New System.Drawing.Size(17, 14)
        Me._Label2_5.TabIndex = 6
        Me._Label2_5.Text = "M"
        '
        '_Label2_4
        '
        Me._Label2_4.AutoSize = True
        Me._Label2_4.BackColor = System.Drawing.Color.Transparent
        Me._Label2_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_4.Location = New System.Drawing.Point(108, 24)
        Me._Label2_4.Name = "_Label2_4"
        Me._Label2_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_4.Size = New System.Drawing.Size(13, 14)
        Me._Label2_4.TabIndex = 5
        Me._Label2_4.Text = "J"
        '
        '_Label2_3
        '
        Me._Label2_3.AutoSize = True
        Me._Label2_3.BackColor = System.Drawing.Color.Transparent
        Me._Label2_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_3.Location = New System.Drawing.Point(124, 24)
        Me._Label2_3.Name = "_Label2_3"
        Me._Label2_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_3.Size = New System.Drawing.Size(15, 14)
        Me._Label2_3.TabIndex = 4
        Me._Label2_3.Text = "V"
        '
        '_Label2_2
        '
        Me._Label2_2.AutoSize = True
        Me._Label2_2.BackColor = System.Drawing.Color.Transparent
        Me._Label2_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_2.Location = New System.Drawing.Point(140, 24)
        Me._Label2_2.Name = "_Label2_2"
        Me._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_2.Size = New System.Drawing.Size(14, 14)
        Me._Label2_2.TabIndex = 3
        Me._Label2_2.Text = "S"
        '
        '_Label2_1
        '
        Me._Label2_1.AutoSize = True
        Me._Label2_1.BackColor = System.Drawing.Color.Transparent
        Me._Label2_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_1.Location = New System.Drawing.Point(44, 24)
        Me._Label2_1.Name = "_Label2_1"
        Me._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_1.Size = New System.Drawing.Size(14, 14)
        Me._Label2_1.TabIndex = 2
        Me._Label2_1.Text = "D"
        '
        '_Label2_0
        '
        Me._Label2_0.AutoSize = True
        Me._Label2_0.BackColor = System.Drawing.Color.Transparent
        Me._Label2_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label2_0.Location = New System.Drawing.Point(60, 24)
        Me._Label2_0.Name = "_Label2_0"
        Me._Label2_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_0.Size = New System.Drawing.Size(14, 14)
        Me._Label2_0.TabIndex = 1
        Me._Label2_0.Text = "L"
        '
        'TexteEnHaut
        '
        Me.texteEnHaut.AutoSize = True
        Me.texteEnHaut.BackColor = System.Drawing.Color.Transparent
        Me.texteEnHaut.Cursor = System.Windows.Forms.Cursors.Default
        Me.texteEnHaut.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.texteEnHaut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.texteEnHaut.Location = New System.Drawing.Point(8, 8)
        Me.texteEnHaut.Name = "TexteEnHaut"
        Me.texteEnHaut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.texteEnHaut.Size = New System.Drawing.Size(181, 14)
        Me.texteEnHaut.TabIndex = 0
        Me.texteEnHaut.Text = "Veuillez sélectionner une date :"
        '
        '_Lines_5
        '
        Me._Lines_5.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_5.Location = New System.Drawing.Point(40, 136)
        Me._Lines_5.Name = "_Lines_5"
        Me._Lines_5.Size = New System.Drawing.Size(112, 1)
        Me._Lines_5.TabIndex = 213
        '
        '_Lines_10
        '
        Me._Lines_10.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_10.Location = New System.Drawing.Point(40, 56)
        Me._Lines_10.Name = "_Lines_10"
        Me._Lines_10.Size = New System.Drawing.Size(112, 1)
        Me._Lines_10.TabIndex = 214
        '
        '_Lines_9
        '
        Me._Lines_9.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_9.Location = New System.Drawing.Point(40, 72)
        Me._Lines_9.Name = "_Lines_9"
        Me._Lines_9.Size = New System.Drawing.Size(112, 1)
        Me._Lines_9.TabIndex = 215
        '
        '_Lines_8
        '
        Me._Lines_8.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_8.Location = New System.Drawing.Point(40, 88)
        Me._Lines_8.Name = "_Lines_8"
        Me._Lines_8.Size = New System.Drawing.Size(112, 1)
        Me._Lines_8.TabIndex = 216
        '
        '_Lines_6
        '
        Me._Lines_6.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_6.Location = New System.Drawing.Point(40, 120)
        Me._Lines_6.Name = "_Lines_6"
        Me._Lines_6.Size = New System.Drawing.Size(112, 1)
        Me._Lines_6.TabIndex = 217
        '
        '_Lines_4
        '
        Me._Lines_4.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_4.Location = New System.Drawing.Point(40, 40)
        Me._Lines_4.Name = "_Lines_4"
        Me._Lines_4.Size = New System.Drawing.Size(112, 1)
        Me._Lines_4.TabIndex = 218
        '
        '_Lines_18
        '
        Me._Lines_18.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_18.Location = New System.Drawing.Point(56, 40)
        Me._Lines_18.Name = "_Lines_18"
        Me._Lines_18.Size = New System.Drawing.Size(1, 96)
        Me._Lines_18.TabIndex = 204
        '
        '_Lines_17
        '
        Me._Lines_17.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_17.Location = New System.Drawing.Point(40, 40)
        Me._Lines_17.Name = "_Lines_17"
        Me._Lines_17.Size = New System.Drawing.Size(1, 96)
        Me._Lines_17.TabIndex = 205
        '
        '_Lines_16
        '
        Me._Lines_16.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_16.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_16.Location = New System.Drawing.Point(72, 40)
        Me._Lines_16.Name = "_Lines_16"
        Me._Lines_16.Size = New System.Drawing.Size(1, 96)
        Me._Lines_16.TabIndex = 206
        '
        '_Lines_15
        '
        Me._Lines_15.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_15.Location = New System.Drawing.Point(88, 40)
        Me._Lines_15.Name = "_Lines_15"
        Me._Lines_15.Size = New System.Drawing.Size(1, 96)
        Me._Lines_15.TabIndex = 207
        '
        '_Lines_14
        '
        Me._Lines_14.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_14.Location = New System.Drawing.Point(104, 40)
        Me._Lines_14.Name = "_Lines_14"
        Me._Lines_14.Size = New System.Drawing.Size(1, 96)
        Me._Lines_14.TabIndex = 208
        '
        '_Lines_13
        '
        Me._Lines_13.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_13.Location = New System.Drawing.Point(120, 40)
        Me._Lines_13.Name = "_Lines_13"
        Me._Lines_13.Size = New System.Drawing.Size(1, 96)
        Me._Lines_13.TabIndex = 209
        '
        '_Lines_12
        '
        Me._Lines_12.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_12.Location = New System.Drawing.Point(136, 40)
        Me._Lines_12.Name = "_Lines_12"
        Me._Lines_12.Size = New System.Drawing.Size(1, 96)
        Me._Lines_12.TabIndex = 210
        '
        '_Lines_11
        '
        Me._Lines_11.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_11.Location = New System.Drawing.Point(152, 40)
        Me._Lines_11.Name = "_Lines_11"
        Me._Lines_11.Size = New System.Drawing.Size(1, 96)
        Me._Lines_11.TabIndex = 211
        '
        '_Lines_7
        '
        Me._Lines_7.BackColor = System.Drawing.SystemColors.WindowText
        Me._Lines_7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lines_7.Location = New System.Drawing.Point(40, 104)
        Me._Lines_7.Name = "_Lines_7"
        Me._Lines_7.Size = New System.Drawing.Size(112, 1)
        Me._Lines_7.TabIndex = 212
        '
        'DateChoice
        '
        Me.AcceptButton = Me.okbutton
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(194, 209)
        Me.Controls.Add(Me._Lines_5)
        Me.Controls.Add(Me._Lines_10)
        Me.Controls.Add(Me._Lines_9)
        Me.Controls.Add(Me._Lines_8)
        Me.Controls.Add(Me._Lines_6)
        Me.Controls.Add(Me._Lines_4)
        Me.Controls.Add(Me._Lines_18)
        Me.Controls.Add(Me._Lines_17)
        Me.Controls.Add(Me._Lines_16)
        Me.Controls.Add(Me._Lines_15)
        Me.Controls.Add(Me._Lines_14)
        Me.Controls.Add(Me._Lines_13)
        Me.Controls.Add(Me._Lines_12)
        Me.Controls.Add(Me._Lines_11)
        Me.Controls.Add(Me._Lines_7)
        Me.Controls.Add(Me.hh)
        Me.Controls.Add(Me.mm)
        Me.Controls.Add(Me.okbutton)
        Me.Controls.Add(Me.annee)
        Me.Controls.Add(Me.mois)
        Me.Controls.Add(Me.dateSelected)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me._Label2_6)
        Me.Controls.Add(Me._Label2_5)
        Me.Controls.Add(Me._Label2_4)
        Me.Controls.Add(Me._Label2_3)
        Me.Controls.Add(Me._Label2_2)
        Me.Controls.Add(Me._Label2_1)
        Me.Controls.Add(Me._Label2_0)
        Me.Controls.Add(Me.texteEnHaut)
        Me.Controls.Add(Me._cases_41)
        Me.Controls.Add(Me._cases_40)
        Me.Controls.Add(Me._cases_39)
        Me.Controls.Add(Me._cases_38)
        Me.Controls.Add(Me._cases_37)
        Me.Controls.Add(Me._cases_36)
        Me.Controls.Add(Me._cases_35)
        Me.Controls.Add(Me._cases_34)
        Me.Controls.Add(Me._cases_33)
        Me.Controls.Add(Me._cases_32)
        Me.Controls.Add(Me._cases_31)
        Me.Controls.Add(Me._cases_30)
        Me.Controls.Add(Me._cases_29)
        Me.Controls.Add(Me._cases_28)
        Me.Controls.Add(Me._cases_27)
        Me.Controls.Add(Me._cases_26)
        Me.Controls.Add(Me._cases_25)
        Me.Controls.Add(Me._cases_24)
        Me.Controls.Add(Me._cases_23)
        Me.Controls.Add(Me._cases_22)
        Me.Controls.Add(Me._cases_21)
        Me.Controls.Add(Me._cases_20)
        Me.Controls.Add(Me._cases_19)
        Me.Controls.Add(Me._cases_18)
        Me.Controls.Add(Me._cases_17)
        Me.Controls.Add(Me._cases_16)
        Me.Controls.Add(Me._cases_15)
        Me.Controls.Add(Me._cases_14)
        Me.Controls.Add(Me._cases_13)
        Me.Controls.Add(Me._cases_12)
        Me.Controls.Add(Me._cases_11)
        Me.Controls.Add(Me._cases_10)
        Me.Controls.Add(Me._cases_9)
        Me.Controls.Add(Me._cases_8)
        Me.Controls.Add(Me._cases_7)
        Me.Controls.Add(Me._cases_6)
        Me.Controls.Add(Me._cases_5)
        Me.Controls.Add(Me._cases_4)
        Me.Controls.Add(Me._cases_3)
        Me.Controls.Add(Me._cases_2)
        Me.Controls.Add(Me._cases_1)
        Me.Controls.Add(Me._cases_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(364, 329)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DateChoice"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Choix de la date"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub linkcasesArray()
        Me.cases = New BaseObjArray()
        Me.cases.add(Me._cases_0)
        Me.cases.add(Me._cases_1)
        Me.cases.add(Me._cases_2)
        Me.cases.add(Me._cases_3)
        Me.cases.add(Me._cases_4)
        Me.cases.add(Me._cases_5)
        Me.cases.add(Me._cases_6)
        Me.cases.add(Me._cases_7)
        Me.cases.add(Me._cases_8)
        Me.cases.add(Me._cases_9)
        Me.cases.add(Me._cases_10)
        Me.cases.add(Me._cases_11)
        Me.cases.add(Me._cases_12)
        Me.cases.add(Me._cases_13)
        Me.cases.add(Me._cases_14)
        Me.cases.add(Me._cases_15)
        Me.cases.add(Me._cases_16)
        Me.cases.add(Me._cases_17)
        Me.cases.add(Me._cases_18)
        Me.cases.add(Me._cases_19)
        Me.cases.add(Me._cases_20)
        Me.cases.add(Me._cases_21)
        Me.cases.add(Me._cases_22)
        Me.cases.add(Me._cases_23)
        Me.cases.add(Me._cases_24)
        Me.cases.add(Me._cases_25)
        Me.cases.add(Me._cases_26)
        Me.cases.add(Me._cases_27)
        Me.cases.add(Me._cases_28)
        Me.cases.add(Me._cases_29)
        Me.cases.add(Me._cases_30)
        Me.cases.add(Me._cases_31)
        Me.cases.add(Me._cases_32)
        Me.cases.add(Me._cases_33)
        Me.cases.add(Me._cases_34)
        Me.cases.add(Me._cases_35)
        Me.cases.add(Me._cases_36)
        Me.cases.add(Me._cases_37)
        Me.cases.add(Me._cases_38)
        Me.cases.add(Me._cases_39)
        Me.cases.add(Me._cases_40)
        Me.cases.add(Me._cases_41)
    End Sub
#End Region

#Region "Cases Events"
    Private Sub all_cases_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_41.DoubleClick, _cases_0.DoubleClick, _cases_1.DoubleClick, _cases_2.DoubleClick, _cases_3.DoubleClick, _cases_4.DoubleClick, _cases_5.DoubleClick, _cases_6.DoubleClick, _cases_7.DoubleClick, _cases_8.DoubleClick, _cases_9.DoubleClick, _cases_10.DoubleClick, _cases_11.DoubleClick, _cases_12.DoubleClick, _cases_13.DoubleClick, _cases_14.DoubleClick, _cases_15.DoubleClick, _cases_16.DoubleClick, _cases_17.DoubleClick, _cases_18.DoubleClick, _cases_19.DoubleClick, _cases_20.DoubleClick, _cases_21.DoubleClick, _cases_22.DoubleClick, _cases_23.DoubleClick, _cases_24.DoubleClick, _cases_25.DoubleClick, _cases_26.DoubleClick, _cases_27.DoubleClick, _cases_28.DoubleClick, _cases_29.DoubleClick, _cases_30.DoubleClick, _cases_31.DoubleClick, _cases_32.DoubleClick, _cases_33.DoubleClick, _cases_34.DoubleClick, _cases_35.DoubleClick, _cases_36.DoubleClick, _cases_40.DoubleClick, _cases_39.DoubleClick, _cases_38.DoubleClick, _cases_37.DoubleClick
        cases_DoubleClick(-1, sender, e)
    End Sub
    Private Sub _cases_0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_0.Click
        cases_Click(0, sender, e)
    End Sub
    Private Sub _cases_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_1.Click
        cases_Click(1, sender, e)
    End Sub
    Private Sub _cases_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_2.Click
        cases_Click(2, sender, e)
    End Sub
    Private Sub _cases_3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_3.Click
        cases_Click(3, sender, e)
    End Sub
    Private Sub _cases_4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_4.Click
        cases_Click(4, sender, e)
    End Sub
    Private Sub _cases_5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_5.Click
        cases_Click(5, sender, e)
    End Sub
    Private Sub _cases_6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_6.Click
        cases_Click(6, sender, e)
    End Sub
    Private Sub _cases_7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_7.Click
        cases_Click(7, sender, e)
    End Sub
    Private Sub _cases_8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_8.Click
        cases_Click(8, sender, e)
    End Sub
    Private Sub _cases_9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_9.Click
        cases_Click(9, sender, e)
    End Sub
    Private Sub _cases_10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_10.Click
        cases_Click(10, sender, e)
    End Sub
    Private Sub _cases_11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_11.Click
        cases_Click(11, sender, e)
    End Sub
    Private Sub _cases_12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_12.Click
        cases_Click(12, sender, e)
    End Sub
    Private Sub _cases_13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_13.Click
        cases_Click(13, sender, e)
    End Sub
    Private Sub _cases_14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_14.Click
        cases_Click(14, sender, e)
    End Sub
    Private Sub _cases_15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_15.Click
        cases_Click(15, sender, e)
    End Sub
    Private Sub _cases_16_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_16.Click
        cases_Click(16, sender, e)
    End Sub
    Private Sub _cases_17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_17.Click
        cases_Click(17, sender, e)
    End Sub
    Private Sub _cases_18_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_18.Click
        cases_Click(18, sender, e)
    End Sub
    Private Sub _cases_19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_19.Click
        cases_Click(19, sender, e)
    End Sub
    Private Sub _cases_20_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_20.Click
        cases_Click(20, sender, e)
    End Sub
    Private Sub _cases_21_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_21.Click
        cases_Click(21, sender, e)
    End Sub
    Private Sub _cases_22_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_22.Click
        cases_Click(22, sender, e)
    End Sub
    Private Sub _cases_23_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_23.Click
        cases_Click(23, sender, e)
    End Sub
    Private Sub _cases_24_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_24.Click
        cases_Click(24, sender, e)
    End Sub
    Private Sub _cases_25_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_25.Click
        cases_Click(25, sender, e)
    End Sub
    Private Sub _cases_26_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_26.Click
        cases_Click(26, sender, e)
    End Sub
    Private Sub _cases_27_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_27.Click
        cases_Click(27, sender, e)
    End Sub
    Private Sub _cases_28_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_28.Click
        cases_Click(28, sender, e)
    End Sub
    Private Sub _cases_29_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_29.Click
        cases_Click(29, sender, e)
    End Sub
    Private Sub _cases_30_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_30.Click
        cases_Click(30, sender, e)
    End Sub
    Private Sub _cases_31_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_31.Click
        cases_Click(31, sender, e)
    End Sub
    Private Sub _cases_32_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_32.Click
        cases_Click(32, sender, e)
    End Sub
    Private Sub _cases_33_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_33.Click
        cases_Click(33, sender, e)
    End Sub
    Private Sub _cases_34_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_34.Click
        cases_Click(34, sender, e)
    End Sub
    Private Sub _cases_35_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_35.Click
        cases_Click(35, sender, e)
    End Sub
    Private Sub _cases_36_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_36.Click
        cases_Click(36, sender, e)
    End Sub
    Private Sub _cases_37_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_37.Click
        cases_Click(37, sender, e)
    End Sub
    Private Sub _cases_38_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_38.Click
        cases_Click(38, sender, e)
    End Sub
    Private Sub _cases_39_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_39.Click
        cases_Click(39, sender, e)
    End Sub
    Private Sub _cases_40_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_40.Click
        cases_Click(40, sender, e)
    End Sub
    Private Sub _cases_41_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _cases_41.Click
        cases_Click(41, sender, e)
    End Sub
#End Region

    Private myChoice As Generic.List(Of Date)
    Private _selectionType As selections = selections.Day
    Private ctrlPressed As Boolean = False
    Private _allowMultipleSelection As Boolean = False
    Private forceErase As Boolean = False
    Private ADC, _showTime As Boolean
    Private clicking As Boolean = False
    Private _selectionBackColor As Color = Color.Red
    Private _selectionForeColor As Color = Color.Black
    Private _specialDateBackColor As Color = Color.Green
    Private _specialDateForeColor As Color = Color.Black
    Private _startYear As Integer = 1900
    Private _endYear As Integer = Date.Today.Year
    Private _autoSelectNow As Boolean = True
    Private _selectedDate As Date = limitDate


    Public Event drawingDay(ByVal selectedDate As Date, ByRef day As DateChoiceDay)
    Public Event validatingDaySelection(ByVal selectedDate As Date, ByRef isValid As Boolean)

#Region "Propriétés"
    Public Property selectionBackColor() As Color
        Get
            Return _selectionBackColor
        End Get
        Set(ByVal value As Color)
            _selectionBackColor = value
        End Set
    End Property

    Public Property selectionForeColor() As Color
        Get
            Return _selectionForeColor
        End Get
        Set(ByVal value As Color)
            _selectionForeColor = value
        End Set
    End Property

    Public Property specialDateBackColor() As Color
        Get
            Return _specialDateBackColor
        End Get
        Set(ByVal value As Color)
            _specialDateBackColor = value
        End Set
    End Property

    Public Property specialDateForeColor() As Color
        Get
            Return _specialDateForeColor
        End Get
        Set(ByVal value As Color)
            _specialDateForeColor = value
        End Set
    End Property

    Public Enum selections As Byte
        Day = 0
        Week = 1
    End Enum

    Public Property selectionType() As selections
        Get
            Return _selectionType
        End Get
        Set(ByVal value As selections)
            _selectionType = value
        End Set
    End Property

    Public Property allowMultipleSelection() As Boolean
        Get
            Return _allowMultipleSelection
        End Get
        Set(ByVal value As Boolean)
            _allowMultipleSelection = value
        End Set
    End Property


    Public Property startYear() As Integer
        Get
            Return _startYear
        End Get
        Set(ByVal value As Integer)
            _startYear = value
        End Set
    End Property

    Public Property endYear() As Integer
        Get
            Return _endYear
        End Get
        Set(ByVal value As Integer)
            _endYear = value
        End Set
    End Property

    Public Property showTime() As Boolean
        Get
            Return _showTime
        End Get
        Set(ByVal value As Boolean)
            _showTime = value
        End Set
    End Property

    Public Property autoSelectNow() As Boolean
        Get
            Return _autoSelectNow
        End Get
        Set(ByVal value As Boolean)
            _autoSelectNow = value
        End Set
    End Property

    Public Property selectedDate() As Date
        Get
            Return _selectedDate
        End Get
        Set(ByVal value As Date)
            _selectedDate = value
        End Set
    End Property

    Default Public ReadOnly Property choose(ByVal showTime As Boolean) As Generic.List(Of Date)
        Get
            Dim i As Integer
            myChoice = New Generic.List(Of Date)

            If _selectionType = selections.Week Then
                If _allowMultipleSelection Then
                    Me.Text = "Choix de(s) semaine(s)"
                    texteEnHaut.Text = "Sélectionnez une(des) semaine(s)"
                Else
                    Me.Text = "Choix de la semaine"
                    texteEnHaut.Text = "Sélectionnez une semaine"
                End If
            End If

            _showTime = showTime
            If selectedDate.Year = 9999 And _autoSelectNow = True Then selectedDate = Date.Now
            REM This validation is not done anymore ? If SelectedDate.Year <> 9999 And MinimumDate.Year <> 9999 Then If Date1InfDate2(SelectedDate, MinimumDate) Then SelectedDate = MinimumDate

            Dim myAnnee As Integer
            For i = startYear To endYear
                myAnnee = annee.Items.Add(i)
                If selectedDate.Year <> 9999 Then If selectedDate.Year = i Then annee.SelectedIndex = myAnnee
            Next i

            If selectedDate.Year = 9999 Or selectedDate.Year.ToString = "1" Then
                mois.SelectedIndex = 0 : annee.SelectedIndex = 0
            Else
                mois.SelectedIndex = selectedDate.Month - 1
            End If

            placeDays(selectedDate)

            If showTime = False Then
                label3.Visible = False
                hh.Visible = False
                mm.Visible = False
                okbutton.Left = (Me.ClientRectangle.Width - okbutton.Width) / 2
            Else
                okbutton.Left = 104
                label3.Visible = True
                hh.Visible = True
                mm.Visible = True

                If _selectedDate.Equals(limitDate) Then
                    hh.SelectedIndex = 0
                    mm.SelectedIndex = 0
                Else
                    Dim myHour As String = _selectedDate.Hour
                    If myHour < 10 Then myHour = "0" & myHour
                    Dim myMinute As String = _selectedDate.Minute - _selectedDate.Minute Mod CType(cases(dateSelected.Text).Tag, DateChoiceDay).minutesInterval
                    If myMinute < 10 Then myMinute = "0" & myMinute
                    hh.SelectedIndex = hh.FindString(myHour)
                    If hh.SelectedIndex = -1 Then
                        hh.SelectedIndex = 0
                        mm.SelectedIndex = 0
                    Else
                        mm.SelectedIndex = mm.FindString(myMinute)
                    End If
                End If
            End If

            Me.ShowDialog()

            Return myChoice
        End Get
    End Property
#End Region

    ''' <summary>
    ''' This function is called when a specific day is about to be drawn.
    ''' </summary>
    ''' <param name="selectedDate">date that will be draw</param>
    ''' <param name="day">current parameters of the day (can be changed on the fly)</param>
    ''' <remarks></remarks>
    Protected Overridable Sub onDrawingDay(ByVal selectedDate As Date, ByRef day As DateChoiceDay)
        RaiseEvent drawingDay(selectedDate, day)
    End Sub

    ''' <summary>
    ''' This function is called when a day require validation
    ''' </summary>
    ''' <param name="selectedDate">date that needs the validation</param>
    ''' <param name="isValid">boolean to </param>
    ''' <remarks></remarks>
    Protected Overridable Sub onValidatingDaySelection(ByVal selectedDate As Date, ByRef isValid As Boolean)
        RaiseEvent validatingDaySelection(selectedDate, isValid)
    End Sub

#Region "Internal Functions"
    Private Sub fillTime(ByVal startTime As Date, ByVal endTime As Date, ByVal minutesInterval As Integer, ByVal onlyMinutes As Boolean)
        If minutesInterval = 0 Then minutesInterval = 1

        Dim hmTemp As String = ""
        If onlyMinutes = False Then
            hh.Items.Clear()
            For i As Integer = startTime.Hour To endTime.Hour
                If i < 10 Then
                    hmTemp = "0" & i
                Else
                    hmTemp = i
                End If

                hh.Items.Add(hmTemp)
            Next i
        End If

        mm.Items.Clear()
        Dim curHour As Integer = -1
        If hh.SelectedItem IsNot Nothing Then curHour = hh.SelectedItem
        For i As Integer = IIf(startTime.Hour = curHour, startTime.Minute - startTime.Minute Mod minutesInterval, 0) To IIf(endTime.Hour = curHour, endTime.Minute - endTime.Minute Mod minutesInterval, 59) Step minutesInterval
            If i < 10 Then
                hmTemp = "0" & i
            Else
                hmTemp = i
            End If
            mm.Items.Add(hmTemp)
        Next i
    End Sub

    Private Sub placeDays(Optional ByRef selectedDate As Date = limitDate)
        Dim i As Short
        Dim monthDays() As Integer = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        Dim fwd As Integer
        Dim casesToClick As Integer = -1
        Dim myMonth As String
        Dim myDate As Date
        If mois.Text = "" Or annee.Text = "" Then Exit Sub
        myMonth = mois.SelectedIndex + 1
        If myMonth < 10 Then myMonth = "0" & myMonth

        myDate = annee.Text & "/" & myMonth & "/01"
        fwd = myDate.DayOfWeek + 1

        If anBissextile(CInt(annee.Text)) Then monthDays(1) = 29

        Dim multiTT As String = IIf(_allowMultipleSelection, "Tenir contrôle (CTRL) pour effectuer une sélection multiple.", "")
        toolTip1.RemoveAll()
        For i = 0 To 41
            cases(i).Text = ""
            cases(i).BackColor = Color.White
            cases(i).Cursor = System.Windows.Forms.Cursors.Default
            cases(i).Tag = New DateChoiceDay
            toolTip1.SetToolTip(cases(i), multiTT)
        Next i

        For i = 1 To monthDays(mois.SelectedIndex)
            With CType(cases(i - 2 + fwd), Label)
                .Text = i
                If selectedDate.Year <> 9999 AndAlso selectedDate.Year.ToString <> "1" AndAlso selectedDate.Month = mois.SelectedIndex + 1 AndAlso i = selectedDate.Day Then casesToClick = i - 2 + fwd
                .Cursor = System.Windows.Forms.Cursors.Hand

                Dim day As New DateChoiceDay
                day.font = .Font
                day.backColor = .BackColor
                day.foreColor = .ForeColor
                day.toolTip = toolTip1.GetToolTip(cases(i - 2 + fwd))
                onDrawingDay(New Date(annee.Text, mois.SelectedIndex + 1, IIf(Date.DaysInMonth(annee.Text, mois.SelectedIndex + 1) < i, Date.DaysInMonth(annee.Text, mois.SelectedIndex + 1), i)), day)
                .Font = day.font
                .BackColor = day.backColor
                .ForeColor = day.foreColor
                .Tag = day
                toolTip1.SetToolTip(cases(i - 2 + fwd), day.toolTip)

                REM to child in override of the function above onDrawDay
                'If PreferencesManager.getGeneralPreferences()("AffSpecialDatesInCalendar") = True Then
                '    Dim sd As Generic.List(Of SpecialDate) = SpecialDatesManager.GetInstance.GetSpecialDates(MyDate)
                '    If sd.Count > 0 Then
                '        .BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorSpecialDates"))
                '        Dim tt As String = sd(0).Nom
                '        For j As Integer = 1 To sd.Count - 1
                '            tt &= vbCrLf & sd(j).Nom
                '        Next j

                '        If MultiTT <> "" Then tt = MultiTT & vbCrLf & vbCrLf & tt
                '        ToolTip1.SetToolTip(cases(i - 2 + FWD), tt)
                '        .Tag = "SPECIAL"
                '    End If
                'End If
            End With
            myDate = myDate.AddDays(1)
        Next i

        If casesToClick <> -1 Then cases_Click(casesToClick, cases(casesToClick), EventArgs.Empty)
    End Sub

    Private Sub keyDownCentralized(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control = True Then ctrlPressed = True
        e.Handled = True
    End Sub

    Private Sub keyUpCentralized(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ctrlPressed = False
        e.Handled = True
    End Sub
#End Region

    Private Sub annee_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles annee.SelectedIndexChanged
        mois_SelectedIndexChanged(mois, New System.EventArgs())
    End Sub

    Private Sub dateChoice_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim MyDate, otherDate As Date

        Select Case e.KeyCode
            Case 27 'ESC
                Me.Close()

            Case 37 'GAUCHE
                If dateSelected.Text = "" Then Exit Sub
                If cases(dateSelected.Text).text = "" Then Exit Sub
                MyDate = CDate(annee.Text & "/" & mois.SelectedIndex + 1 & "/" & cases(dateSelected.Text).Text)
                otherDate = MyDate.AddDays(-1)
                If annee.FindStringExact(otherDate.Year) < 0 Then Exit Sub
                annee.SelectedIndex = annee.FindStringExact(otherDate.Year)
                mois.SelectedIndex = otherDate.Month - 1
                placeDays(otherDate)

            Case 38 'HAUT
                If dateSelected.Text = "" Then Exit Sub
                If cases(dateSelected.Text).text = "" Then Exit Sub
                MyDate = CDate(annee.Text & "/" & mois.SelectedIndex + 1 & "/" & cases(dateSelected.Text).Text)
                otherDate = MyDate.AddDays(-7)
                If annee.FindStringExact(otherDate.Year) < 0 Then Exit Sub
                annee.SelectedIndex = annee.FindStringExact(otherDate.Year)
                mois.SelectedIndex = otherDate.Month - 1
                placeDays(otherDate)

            Case 39 'DROITE
                If dateSelected.Text = "" Then Exit Sub
                If cases(dateSelected.Text).text = "" Then Exit Sub
                MyDate = CDate(annee.Text & "/" & mois.SelectedIndex + 1 & "/" & cases(dateSelected.Text).Text)
                otherDate = MyDate.AddDays(1)
                If annee.FindStringExact(otherDate.Year) < 0 Then Exit Sub
                annee.SelectedIndex = annee.FindStringExact(otherDate.Year)
                mois.SelectedIndex = otherDate.Month - 1
                placeDays(otherDate)

            Case 40 'BAS
                If dateSelected.Text = "" Then Exit Sub
                If cases(dateSelected.Text).text = "" Then Exit Sub
                MyDate = CDate(annee.Text & "/" & mois.SelectedIndex + 1 & "/" & cases(dateSelected.Text).Text)
                otherDate = MyDate.AddDays(7)
                If annee.FindStringExact(otherDate.Year) < 0 Then Exit Sub
                annee.SelectedIndex = annee.FindStringExact(otherDate.Year)
                mois.SelectedIndex = otherDate.Month - 1
                placeDays(otherDate)
        End Select

        e.Handled = True
    End Sub

    Private Sub mois_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mois.SelectedIndexChanged
        placeDays()

        If dateSelected.Text <> "" Then forceErase = True : cases_Click(dateSelected.Text, eventSender, eventArgs)
        Exit Sub
        If Not dateSelected.Text = "" Then
            With cases(dateSelected.Text)
                If Not .Text = "" Then .BackColor = System.Drawing.ColorTranslator.FromOle(&HFF)
            End With
        End If
        Me.Focus()
    End Sub

    Private Sub okbutton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles okbutton.Click
        myChoice = New Generic.List(Of Date)
        Dim theMonth As String
        Dim First, last As Integer
        Dim i As Byte
        Dim myDate2 As Date = limitDate
        theMonth = mois.SelectedIndex + 1

        If dateSelected.Text = "" Then MessageBox.Show("Veuillez sélectionner une date", "Sélection d'une date") : Exit Sub
        If cases(dateSelected.Text).Text = "" And _selectionType = selections.Day Then MessageBox.Show("Veuillez sélectionner une date", "Sélection d'une date") : Exit Sub

        Dim hour As Byte = 0
        Dim minute As Byte = 0
        If hh.Visible Then
            hour = hh.Text
            minute = mm.Text
        End If

        If _selectionType = selections.Day Then
            For i = 0 To 41
                If CType(cases(i), Label).BackColor.Equals(_selectionBackColor) Then myChoice.Add(New Date(annee.GetItemText(annee.SelectedItem), (mois.Items.IndexOf(mois.SelectedItem) + 1), cases(i).Text, hour, minute, 0))
            Next i
        Else
            For i = 6 To 41 Step 7
                First = i - ((i / 7 - Math.Floor(i / 7)) * 7)
                last = First + 6

                If CType(cases(i), Label).BackColor.Equals(_selectionBackColor) Then
                    If cases(last).text <> "" Then
                        myDate2 = New Date(annee.GetItemText(annee.SelectedItem), theMonth, cases(last).Text, hour, minute, 0)
                    Else
                        myDate2 = New Date(annee.GetItemText(annee.SelectedItem), theMonth, cases(First).Text, hour, minute, 0)
                        myDate2.AddDays(6)
                    End If
                    myChoice.Add(myDate2)
                End If
            Next i
        End If

        If myChoice.Count = 0 Then MessageBox.Show("Veuillez sélectionner une date", "Sélection d'une date") : Exit Sub

        Me.Close()
    End Sub

    Private Sub unselectDays()
        Dim First, Last, i As Byte
        'Déselectionne les cases
        If Not dateSelected.Text = "" And ((_allowMultipleSelection = False Or (_allowMultipleSelection = True And ctrlPressed = False)) Or forceErase = True) Then
            If _allowMultipleSelection = False Then
                If _selectionType = selections.Day Then
                    cases(dateSelected.Text).BackColor = CType(cases(dateSelected.Text).Tag, DateChoiceDay).backColor
                    cases(dateSelected.Text).ForeColor = CType(cases(dateSelected.Text).Tag, DateChoiceDay).foreColor
                Else
                    First = dateSelected.Text - ((dateSelected.Text / 7 - Math.Floor(dateSelected.Text / 7)) * 7)
                    Last = First + 6
                    For i = First To Last
                        cases(i).BackColor = CType(cases(i).tag, DateChoiceDay).backColor
                        cases(i).ForeColor = CType(cases(i).tag, DateChoiceDay).foreColor
                    Next i
                End If
            Else
                For i = 0 To 41
                    cases(i).BackColor = CType(cases(i).tag, DateChoiceDay).backColor
                    cases(i).ForeColor = CType(cases(i).tag, DateChoiceDay).foreColor
                Next i
            End If
        End If

        For i = 0 To 41
            If cases(i).BackColor = CType(cases(i).tag, DateChoiceDay).backColor AndAlso CType(cases(i).tag, DateChoiceDay).isSpecial Then
                cases(i).BackColor = _specialDateBackColor
                cases(i).ForeColor = _specialDateForeColor
            End If
        Next i
    End Sub

    Private Sub cases_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles cases.click
        If cases(index).Text = "" Then Exit Sub
        clicking = True
        hh.Enabled = True
        mm.Enabled = True
        If hh.Items.Count <> 0 Then hh.SelectedIndex = 0
        If mm.Items.Count <> 0 Then mm.SelectedIndex = 0
        okbutton.Enabled = True

        Dim First, Last, i As Byte
        unselectDays()

        dateSelected.Text = CStr(index)
        'Sélectionne les cases
        If _selectionType = selections.Week Then
            First = dateSelected.Text - ((dateSelected.Text / 7 - Math.Floor(dateSelected.Text / 7)) * 7)
            Last = First + 6
            dateSelected.Text = Last

            If cases(First).Text = "" And cases(Last).Text = "" Then Exit Sub

            For i = First To Last
                If CType(cases(dateSelected.Text), Label).BackColor.Equals(_selectionBackColor) Then
                    With CType(cases(i).Tag, DateChoiceDay)
                        cases(i).BackColor = IIf(.isSpecial, _specialDateBackColor, .backColor)
                        cases(i).ForeColor = IIf(.isSpecial, _specialDateForeColor, .foreColor)
                    End With
                Else
                    cases(i).BackColor = _selectionBackColor
                    cases(i).ForeColor = _selectionForeColor
                End If
            Next i
        Else
            If cases(index).Text = "" Then Exit Sub

            If CType(cases(dateSelected.Text), Label).BackColor.Equals(_selectionBackColor) Then
                With CType(cases(dateSelected.Text).Tag, DateChoiceDay)
                    cases(dateSelected.Text).BackColor = IIf(.isSpecial, _specialDateBackColor, .backColor)
                    cases(dateSelected.Text).ForeColor = IIf(.isSpecial, _specialDateForeColor, .foreColor)
                End With
            Else
                cases(dateSelected.Text).BackColor = _selectionBackColor
                cases(dateSelected.Text).ForeColor = _selectionForeColor
            End If

        End If

        'Détermine la date
        Dim myDate As Date = Nothing
        If _allowMultipleSelection = False Then
            If cases(dateSelected.Text).Text <> "" Then myDate = CDate(annee.GetItemText(annee.SelectedItem) & "/" & mois.Items.IndexOf(mois.SelectedItem) + 1 & "/" & cases(dateSelected.Text).Text)
        Else
            If _selectionType = selections.Week Then
                For i = 6 To 41 Step 7
                    First = i - ((i / 7 - Math.Floor(dateSelected.Text / 7)) * 7)
                    Last = First + 6

                    If CType(cases(i), Label).BackColor.Equals(_selectionBackColor) Then
                        If cases(Last).Text <> "" Then
                            myDate = annee.GetItemText(annee.SelectedItem) & "/" & mois.Items.IndexOf(mois.SelectedItem) + 1 & "/" & cases(Last).Text : Exit For
                        Else
                            myDate = annee.GetItemText(annee.SelectedItem) & "/" & mois.Items.IndexOf(mois.SelectedItem) + 1 & "/" & cases(First).Text
                            myDate.AddDays(6)
                        End If
                    End If
                Next i
            Else
                For i = 0 To 41
                    If CType(cases(i), Label).BackColor.Equals(_selectionBackColor) Then myDate = annee.GetItemText(annee.SelectedItem) & "/" & mois.Items.IndexOf(mois.SelectedItem) + 1 & "/" & cases(i).Text : Exit For
                Next i
            End If
        End If

        Dim isValid As Boolean = True
        onValidatingDaySelection(myDate, isValid)

        If isValid = False Then
            hh.SelectedIndex = -1
            mm.SelectedIndex = -1
            hh.Enabled = False
            mm.Enabled = False
            okbutton.Enabled = False
        ElseIf showTime Then
            With CType(cases(dateSelected.Text).Tag, DateChoiceDay)
                fillTime(.startTime, .endTime, .minutesInterval, False)
            End With
            hh.SelectedIndex = 0
            mm.SelectedIndex = 0
        End If


        'TO TRANSFER INTO VALIDATION
        ''Assure que la date sélectionnée est valide
        'If MyDate <> Nothing Then
        '    If MinDate.Equals(LimitDate) = False AndAlso Date1InfDate2(MyDate, MinDate) Then
        '        hh.SelectedIndex = -1
        '        mm.SelectedIndex = -1
        '        hh.Enabled = False
        '        mm.Enabled = False
        '        okbutton.Enabled = False
        '        Exit Sub
        '    End If
        '    If MaxDate.Equals(LimitDate) = False AndAlso Date1InfDate2(MaxDate, MyDate) Then
        '        hh.SelectedIndex = -1
        '        mm.SelectedIndex = -1
        '        hh.Enabled = False
        '        mm.Enabled = False
        '        okbutton.Enabled = False
        '        Exit Sub
        '    End If
        'End If

        ''Vérifie les conflicts d'horaire s'il y a un thérapeute
        'If TRP <> "" And ShowingTime = True Then
        '    hh.Items.Clear()

        '    Dim SHH, EHH, HTemp, SNoUser() As String
        '    Dim NoUser As Integer
        '    SNoUser = System.Text.RegularExpressions.Regex.Split(TRP, " \(")
        '    NoUser = SNoUser(1).Substring(0, SNoUser(1).Length - 1)

        '    'Horaire spécifique
        '    MyDate = CDate(annee.SelectedItem & "/" & (mois.SelectedIndex + 1) & "/" & cases(Index).text)
        '    Dim curHoraire As Horaire = HoraireManager.GetInstance.getHoraire(NoUser, MyDate.AddDays(MyDate.DayOfWeek * -1))

        '    VStartTime = curHoraire.FindTime(Horaire.WTType.First, MyDate)
        '    VEndTime = curHoraire.FindTime(Horaire.WTType.Last, MyDate)
        '    SHH = Microsoft.VisualBasic.Left(VStartTime, 2)
        '    EHH = Microsoft.VisualBasic.Left(VEndTime, 2)
        '    For i = SHH To EHH
        '        If i < 10 Then
        '            HTemp = "0" & i
        '        Else
        '            HTemp = i
        '        End If

        '        hh.Items.Add(HTemp)
        '    Next i

        '    If SelectingNow = False Then
        '        hh.SelectedIndex = 0
        '        mm.SelectedIndex = CDbl(VStartTime.Substring(3)) / 60 * 4
        '    Else
        '        Dim MyHour As String = Date.Now.Hour
        '        If MyHour < 10 Then MyHour = "0" & MyHour
        '        hh.SelectedIndex = hh.FindString(MyHour)
        '        MSel = CInt(Date.Now.Minute / 60 * 4) + 1
        '        If MSel > 2 Then
        '            If (hh.SelectedIndex + 1) <= hh.Items.Count - 1 Then
        '                MSel = 0 : hh.SelectedIndex = hh.SelectedIndex + 1
        '            Else
        '                MSel = 2
        '            End If
        '        End If
        '        mm.SelectedIndex = MSel
        '    End If

        '    If hh.FindStringExact("00") >= 0 Then
        '        hh.SelectedIndex = -1
        '        mm.SelectedIndex = -1
        '        hh.Enabled = False
        '        mm.Enabled = False
        '        okbutton.Enabled = False
        '    Else
        '        hh.Enabled = True
        '        mm.Enabled = True
        '        okbutton.Enabled = True
        '    End If
        'End If

        'Try
        '    If hh.SelectedIndex < 0 And hh.FindStringExact("00") < 0 And hh.Items.Count > 0 Then hh.SelectedIndex = 0
        '    If mm.SelectedIndex < 0 And hh.FindStringExact("00") < 0 And mm.Items.Count > 0 Then mm.SelectedIndex = 0
        'Catch
        'End Try
        forceErase = False
        clicking = False
    End Sub

    Private Sub cases_DoubleClick(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles cases.doubleClick
        okbutton_Click(sender, EventArgs.Empty)
    End Sub

    Private Sub hh_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles hh.SelectedIndexChanged
        With CType(cases(dateSelected.Text).tag, DateChoiceDay)
            fillTime(.startTime, .endTime, .minutesInterval, True)
            mm.SelectedIndex = 0
        End With
    End Sub
End Class
