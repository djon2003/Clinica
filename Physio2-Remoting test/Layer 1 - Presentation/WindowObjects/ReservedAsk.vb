Option Strict Off
Option Explicit On
Friend Class ReservedAsk
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        

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
    Public WithEvents okbtn As System.Windows.Forms.Button
    Public WithEvents periode As System.Windows.Forms.ComboBox
    Public WithEvents textReserved As ManagedText
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents annuler As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.annuler = New System.Windows.Forms.Button
        Me.okbtn = New System.Windows.Forms.Button
        Me.periode = New System.Windows.Forms.ComboBox
        Me.textReserved = New ManagedText
        Me.SuspendLayout()
        '
        'annuler
        '
        Me.annuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.annuler.BackColor = System.Drawing.SystemColors.Control
        Me.annuler.Cursor = System.Windows.Forms.Cursors.Default
        Me.annuler.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.annuler.ForeColor = System.Drawing.SystemColors.ControlText
        Me.annuler.Location = New System.Drawing.Point(281, 65)
        Me.annuler.Name = "annuler"
        Me.annuler.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.annuler.Size = New System.Drawing.Size(80, 21)
        Me.annuler.TabIndex = 4
        Me.annuler.Text = "Annuler"
        Me.annuler.UseVisualStyleBackColor = False
        '
        'okbtn
        '
        Me.okbtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.okbtn.BackColor = System.Drawing.SystemColors.Control
        Me.okbtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.okbtn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.okbtn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.okbtn.Location = New System.Drawing.Point(281, 33)
        Me.okbtn.Name = "okbtn"
        Me.okbtn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.okbtn.Size = New System.Drawing.Size(80, 21)
        Me.okbtn.TabIndex = 3
        Me.okbtn.Text = "OK"
        Me.okbtn.UseVisualStyleBackColor = False
        '
        'periode
        '
        Me.periode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.periode.BackColor = System.Drawing.SystemColors.Window
        Me.periode.Cursor = System.Windows.Forms.Cursors.Default
        Me.periode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.periode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.periode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.periode.Items.AddRange(New Object() {"15 minutes", "30 minutes", "45 minutes", "1 heure", "1h15min", "1h30min", "1h45min", "2 heures", "2h15min", "2h30min", "2h45min", "3 heures", "3h15min", "3h30min", "3h45min", "4 heures", "4h15min", "4h30min", "4h45min", "5 heures", "5h15min", "5h30min", "5h45min", "6 heures"})
        Me.periode.Location = New System.Drawing.Point(281, 1)
        Me.periode.Name = "periode"
        Me.periode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.periode.Size = New System.Drawing.Size(81, 22)
        Me.periode.TabIndex = 2
        '
        'textReserved
        '
        Me.textReserved.acceptAlpha = True
        Me.textReserved.acceptedChars = ""
        Me.textReserved.acceptNumeric = True
        Me.textReserved.AcceptsReturn = True
        Me.textReserved.allCapital = False
        Me.textReserved.allLower = False
        Me.textReserved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textReserved.BackColor = System.Drawing.SystemColors.Window
        Me.textReserved.blockOnMaximum = False
        Me.textReserved.blockOnMinimum = False
        Me.textReserved.cb_AcceptLeftZeros = False
        Me.textReserved.cb_AcceptNegative = False
        Me.textReserved.currencyBox = False
        Me.textReserved.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.textReserved.firstLetterCapital = True
        Me.textReserved.firstLettersCapital = False
        Me.textReserved.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textReserved.ForeColor = System.Drawing.SystemColors.WindowText
        Me.textReserved.Location = New System.Drawing.Point(1, 1)
        Me.textReserved.manageText = True
        Me.textReserved.matchExp = ""
        Me.textReserved.maximum = 0
        Me.textReserved.MaxLength = 0
        Me.textReserved.minimum = 0
        Me.textReserved.Multiline = True
        Me.textReserved.Name = "textReserved"
        Me.textReserved.nbDecimals = CType(-1, Short)
        Me.textReserved.onlyAlphabet = False
        Me.textReserved.refuseAccents = False
        Me.textReserved.refusedChars = ""
        Me.textReserved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.textReserved.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.textReserved.showInternalContextMenu = True
        Me.textReserved.Size = New System.Drawing.Size(273, 88)
        Me.textReserved.TabIndex = 1
        Me.textReserved.trimText = False
        '
        'ReservedAsk
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(363, 90)
        Me.Controls.Add(Me.annuler)
        Me.Controls.Add(Me.okbtn)
        Me.Controls.Add(Me.textReserved)
        Me.Controls.Add(Me.periode)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(371, 116)
        Me.Name = "ReservedAsk"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Plage réservée"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Yellow
        Me.ResumeLayout(False)

    End Sub
#End Region
    Private from As System.Windows.Forms.Control
    Private TRP, _LoadingData, LTime, _CurrentPeriode As String
    Private dDate As Date = Nothing
    Private _NoAgenda, _NoClient As Integer

#Region "Propriétés"
    Public Property listFrom() As System.Windows.Forms.Control
        Get
            listFrom = from
        End Get
        Set(ByVal Value As System.Windows.Forms.Control)
            from = Value
        End Set
    End Property

    Public Property currentTRP() As String
        Get
            currentTRP = TRP
        End Get
        Set(ByVal Value As String)
            TRP = Value
        End Set
    End Property

    Public Property lineTime() As String
        Get
            lineTime = LTime
        End Get
        Set(ByVal Value As String)
            LTime = Value
        End Set
    End Property

    Public Property dayDate() As Date
        Get
            dayDate = dDate
        End Get
        Set(ByVal Value As Date)
            dDate = Value
        End Set
    End Property

    Public Property loadingData() As String
        Get
            Return _LoadingData
        End Get
        Set(ByVal Value As String)
            _LoadingData = Value
        End Set
    End Property

    Public Property currentPeriode() As String
        Get
            Return _CurrentPeriode
        End Get
        Set(ByVal Value As String)
            _CurrentPeriode = Value
        End Set
    End Property

    Public Property noAgenda() As Integer
        Get
            Return _NoAgenda
        End Get
        Set(ByVal Value As Integer)
            _NoAgenda = Value
        End Set
    End Property

    Public Property noClient() As Integer
        Get
            Return _NoClient
        End Get
        Set(ByVal Value As Integer)
            _NoClient = Value
        End Set
    End Property
#End Region

    Private Sub reservedAsk_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Me.Load
        periode.SelectedIndex = 0
        textReserved.Text = loadingData
        If currentPeriode <> "" Then periode.Enabled = False : periode.SelectedIndex = (currentPeriode / 15) - 1
    End Sub

    Private Sub okbtn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles okbtn.Click
        Dim MyText, tc As String
        MyText = textReserved.Text
        If MyText = "" Then Me.Close() : Exit Sub
        MyText = MyText.Replace(vbCrLf, "<br>")

        'Ajout/Modification dans l'agenda
        Dim noTRP As Integer = 0
        If currentTRP <> "" Then
            Dim sCurrentTRP() As String
            sCurrentTRP = System.Text.RegularExpressions.Regex.Split(currentTRP, " \(")
            noTRP = sCurrentTRP(1).Substring(0, sCurrentTRP(1).Length - 1)
        End If
        If loadingData = "" Then
            tc = AgendaManager.getInstance.checkTimeConflict(CDate(DateFormat.getTextDate(dayDate, DateFormat.TextDateOptions.YYYYMMDD) & " " & lineTime), (periode.SelectedIndex + 1) * 15, User.extractNo(currentTRP))
            If Not tc = "" Then MessageBox.Show(tc, "Conflit") : Exit Sub

            dayDate = CDate(dayDate.Date & " " & lineTime)
            If DBLinker.getInstance.writeDB("Agenda", "DateHeure,Periode,NoTRP,Reserve", "'" & DateFormat.getTextDate(dayDate, DateFormat.TextDateOptions.YYYYMMDD) & " " & DateFormat.getTextDate(dayDate, DateFormat.TextDateOptions.ShortTime) & "'," & (periode.SelectedIndex + 1) * 15 & "," & noTRP & ",'" & MyText.Replace("'", "''") & "'") = False Then GoTo fin
            myMainWin.StatusText = "Ajout d'une plage réservée le " & DateFormat.getTextDate(dayDate) & " à " & lineTime
        Else
            DBLinker.getInstance.updateDB("Agenda", "Periode=" & (periode.SelectedIndex + 1) * 15 & ",Reserve='" & MyText.Replace("'", "''") & "'", "NoAgenda", noAgenda, False)
        End If

        InternalUpdatesManager.getInstance.sendUpdate("Agendas(" & DateFormat.getTextDate(dayDate, DateFormat.TextDateOptions.YYYYMMDD_FullTime) & ",False," & noTRP & ")")
fin:
        Me.Close()
    End Sub

    Private Sub annuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles annuler.Click
        Me.Close()
    End Sub
End Class
