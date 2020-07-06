Public Class PaymentBox
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        AddTypePaiement(TypePaiement)

        'Add any initialization after the InitializeComponent() call
        Facture.ReadOnly = Not CurrentDroitAcces(59)
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Friend WithEvents Facture As ManagedText
    Friend WithEvents Paiement As ManagedText
    Friend WithEvents Restant As Clinica.ManagedText
    Friend WithEvents TypePaiement As System.Windows.Forms.ComboBox
    Friend WithEvents Paying As System.Windows.Forms.CheckBox
    Friend WithEvents lblBillType As System.Windows.Forms.Label
    Friend Shared WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Friend WithEvents InfoFolder As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.InfoFolder = New System.Windows.Forms.TextBox
        Me.Paying = New System.Windows.Forms.CheckBox
        Me.TypePaiement = New System.Windows.Forms.ComboBox
        Me.Restant = New Clinica.ManagedText
        Me.Paiement = New Clinica.ManagedText
        Me.Facture = New Clinica.ManagedText
        Me.lblBillType = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'InfoFolder
        '
        Me.InfoFolder.BackColor = System.Drawing.SystemColors.Control
        Me.InfoFolder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.InfoFolder.Location = New System.Drawing.Point(321, 3)
        Me.InfoFolder.Name = "InfoFolder"
        Me.InfoFolder.Size = New System.Drawing.Size(174, 13)
        Me.InfoFolder.TabIndex = 20
        '
        'Paying
        '
        Me.Paying.Location = New System.Drawing.Point(5, 16)
        Me.Paying.Name = "Paying"
        Me.Paying.Size = New System.Drawing.Size(16, 16)
        Me.Paying.TabIndex = 16
        '
        'TypePaiement
        '
        Me.TypePaiement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypePaiement.Location = New System.Drawing.Point(621, 24)
        Me.TypePaiement.Name = "TypePaiement"
        Me.TypePaiement.Size = New System.Drawing.Size(88, 21)
        Me.TypePaiement.Sorted = True
        Me.TypePaiement.TabIndex = 11
        '
        'Restant
        '
        Me.Restant.AcceptAlpha = False
        Me.Restant.AcceptedChars = ",§."
        Me.Restant.AcceptNumeric = True
        Me.Restant.AllCapital = False
        Me.Restant.AllLower = False
        Me.Restant.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Restant.BlockOnMaximum = False
        Me.Restant.BlockOnMinimum = False
        Me.Restant.CB_AcceptLeftZeros = False
        Me.Restant.CB_AcceptNegative = False
        Me.Restant.CurrencyBox = True
        Me.Restant.FirstLetterCapital = False
        Me.Restant.FirstLettersCapital = False
        Me.Restant.Location = New System.Drawing.Point(557, 1)
        Me.Restant.ManageText = True
        Me.Restant.MatchExp = ""
        Me.Restant.Maximum = 0
        Me.Restant.Minimum = 0
        Me.Restant.Name = "Restant"
        Me.Restant.NbDecimals = CType(2, Short)
        Me.Restant.OnlyAlphabet = False
        Me.Restant.ReadOnly = True
        Me.Restant.RefuseAccents = False
        Me.Restant.RefusedChars = ""
        Me.Restant.ShowInternalContextMenu = True
        Me.Restant.Size = New System.Drawing.Size(48, 20)
        Me.Restant.TabIndex = 9
        Me.Restant.Text = "0"
        Me.Restant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Restant.TrimText = False
        '
        'Paiement
        '
        Me.Paiement.AcceptAlpha = False
        Me.Paiement.AcceptedChars = ",§."
        Me.Paiement.AcceptNumeric = True
        Me.Paiement.AllCapital = False
        Me.Paiement.AllLower = False
        Me.Paiement.BlockOnMaximum = False
        Me.Paiement.BlockOnMinimum = False
        Me.Paiement.CB_AcceptLeftZeros = False
        Me.Paiement.CB_AcceptNegative = False
        Me.Paiement.CurrencyBox = True
        Me.Paiement.FirstLetterCapital = False
        Me.Paiement.FirstLettersCapital = False
        Me.Paiement.Location = New System.Drawing.Point(557, 25)
        Me.Paiement.ManageText = True
        Me.Paiement.MatchExp = ""
        Me.Paiement.Maximum = 0
        Me.Paiement.Minimum = 0
        Me.Paiement.Name = "Paiement"
        Me.Paiement.NbDecimals = CType(2, Short)
        Me.Paiement.OnlyAlphabet = False
        Me.Paiement.RefuseAccents = False
        Me.Paiement.RefusedChars = ""
        Me.Paiement.ShowInternalContextMenu = True
        Me.Paiement.Size = New System.Drawing.Size(48, 20)
        Me.Paiement.TabIndex = 4
        Me.Paiement.Text = "0"
        Me.Paiement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Paiement.TrimText = False
        '
        'Facture
        '
        Me.Facture.AcceptAlpha = False
        Me.Facture.AcceptedChars = ",§."
        Me.Facture.AcceptNumeric = True
        Me.Facture.AllCapital = False
        Me.Facture.AllLower = False
        Me.Facture.BlockOnMaximum = False
        Me.Facture.BlockOnMinimum = False
        Me.Facture.CB_AcceptLeftZeros = False
        Me.Facture.CB_AcceptNegative = False
        Me.Facture.CurrencyBox = True
        Me.Facture.FirstLetterCapital = False
        Me.Facture.FirstLettersCapital = False
        Me.Facture.Location = New System.Drawing.Point(133, 24)
        Me.Facture.ManageText = True
        Me.Facture.MatchExp = ""
        Me.Facture.Maximum = 0
        Me.Facture.Minimum = 0
        Me.Facture.Name = "Facture"
        Me.Facture.NbDecimals = CType(2, Short)
        Me.Facture.OnlyAlphabet = False
        Me.Facture.RefuseAccents = False
        Me.Facture.RefusedChars = ""
        Me.Facture.ShowInternalContextMenu = True
        Me.Facture.Size = New System.Drawing.Size(48, 20)
        Me.Facture.TabIndex = 3
        Me.Facture.Text = "0"
        Me.Facture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Facture.TrimText = False
        '
        'lblBillType
        '
        Me.lblBillType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillType.Location = New System.Drawing.Point(21, 3)
        Me.lblBillType.Name = "lblBillType"
        Me.lblBillType.Size = New System.Drawing.Size(160, 13)
        Me.lblBillType.TabIndex = 21
        '
        'PaymentBox
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblBillType)
        Me.Controls.Add(Me.InfoFolder)
        Me.Controls.Add(Me.Paying)
        Me.Controls.Add(Me.TypePaiement)
        Me.Controls.Add(Me.Restant)
        Me.Controls.Add(Me.Facture)
        Me.Controls.Add(Me.Paiement)
        Me.Name = "PaymentBox"
        Me.Size = New System.Drawing.Size(720, 48)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _ValueA As Object
    Private _ValueB As Object
    Private _MontantPaye As Double
    Private _OldValue As Double
    Private _OldFacture As Double
    Private _Locked As Boolean = False
    Private _NoFacture As Integer = 0
    Private _PourcentPaiement As Double = 100
    Private billType As String = ""
    Private entiteLiee As String = ""
    Private billDate As String = ""

    Public Event MontantPaiementChanged(ByVal sender As Object, ByVal OldAmount As Double, ByVal NewAmount As Double)
    Public Event MontantFactureChanged(ByVal sender As Object, ByVal OldAmount As Double, ByVal NewAmount As Double)
    Public Event UsingPaymentBoxChanged(ByVal sender As Object)

#Region "Propriété"
    Public Property Locked() As Boolean
        Get
            Return _Locked
        End Get
        Set(ByVal Value As Boolean)
            _Locked = Value
            If CurrentDroitAcces(59) = False And Value = False Then
                Facture.ReadOnly = True
            Else
                Facture.ReadOnly = Value
            End If
            Paiement.ReadOnly = Value
            Paying.Enabled = Not Value
            TypePaiement.Enabled = Not Value
        End Set
    End Property

    Public Property MontantFactureLocked() As Boolean
        Get
            Return facture.ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            If CurrentDroitAcces(59) = False And Value = False Then
                Facture.ReadOnly = True
            Else
                Facture.ReadOnly = Value
            End If
        End Set
    End Property


    Public Property NoFacture() As Integer
        Get
            Return _NoFacture
        End Get
        Set(ByVal Value As Integer)
            _NoFacture = Value
        End Set
    End Property

    Public Property PourcentPaiement() As Double
        Get
            Return _PourcentPaiement
        End Get
        Set(ByVal Value As Double)
            _PourcentPaiement = Value
        End Set
    End Property

    Public Property NegativePaiement() As Boolean
        Get
            Return Paiement.CB_AcceptNegative
        End Get
        Set(ByVal Value As Boolean)
            Paiement.CB_AcceptNegative = Value
        End Set
    End Property

    Public Property ReadOnlyFacture() As Boolean
        Get
            Return Facture.ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            Facture.ReadOnly = Value
        End Set
    End Property

    Public Property InfoOnFolder() As String
        Get
            Return InfoFolder.Text
        End Get
        Set(ByVal Value As String)
            InfoFolder.Text = Value
        End Set
    End Property

    Public Property EntiteLiée() As String
        Get
            Return entiteLiee
        End Get
        Set(ByVal Value As String)
            entiteLiee = Value
        End Set
    End Property

    Public Property UsingPaymentBox(Optional ByVal RaisingEvent As Boolean = True) As Boolean
        Get
            Return Paying.Checked
        End Get
        Set(ByVal Value As Boolean)
            Paying.Checked = Value
            If RaisingEvent = True Then RaiseEvent UsingPaymentBoxChanged(Me)
        End Set
    End Property

    Public Property MontantPaiement(Optional ByVal ChangeValue As Boolean = True) As Double
        Get
            Return Paiement.Text
        End Get
        Set(ByVal Value As Double)
            RaiseEvent MontantPaiementChanged(Me, _OldValue, Value)
            If ChangeValue = True Then Paiement.Text = Value
            _OldValue = Value
        End Set
    End Property

    Public Property MontantFacture() As Double
        Get
            Return Facture.Text
        End Get
        Set(ByVal Value As Double)
            RaiseEvent MontantFactureChanged(Me, Facture.Text, Value)
            Facture.Text = Value
            _OldFacture = Value
        End Set
    End Property

    Public Property MontantRestant() As Double
        Get
            Return Restant.Text
        End Get
        Set(ByVal Value As Double)
            Restant.Text = RoundAmount(Value)
        End Set
    End Property

    Public Property TypeFacture() As String
        Get
            Return billType
        End Get
        Set(ByVal Value As String)
            billType = Value
            Dim textSize As Size = MeasureString(Value, lblBillType.Font)
            If textSize.Width > lblBillType.Width Then
                lblBillType.Text = Value.Substring(0, Value.Length * lblBillType.Width / textSize.Width - 5) & "..."
            Else
                lblBillType.Text = Value
            End If
            ToolTip1.SetToolTip(lblBillType, Value)
        End Set
    End Property
    Public Property DateFacture() As String
        Get
            Return billDate
        End Get
        Set(ByVal Value As String)
            billDate = Value
        End Set
    End Property

    Public Property ValueA() As Object
        Get
            Return _ValueA
        End Get
        Set(ByVal Value As Object)
            _ValueA = Value
        End Set
    End Property

    Public Property ValueB() As Object
        Get
            Return _ValueB
        End Get
        Set(ByVal Value As Object)
            _ValueB = Value
        End Set
    End Property

    Public Property MontantPaye() As Double
        Get
            Return _MontantPaye
        End Get
        Set(ByVal Value As Double)
            _MontantPaye = Value
        End Set
    End Property

    Public Property PaymentMethod() As String
        Get
            Return TypePaiement.SelectedItem
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then TypePaiement.SelectedIndex = TypePaiement.FindStringExact(Value)
        End Set
    End Property
#End Region

    Private Sub PaymentBox_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim boldFont As Font = New Font(Me.Font, FontStyle.Bold)

        e.Graphics.DrawString("Montant de la facture :", Me.Font, Brushes.Black, 21, 24)
        e.Graphics.DrawString("$", Me.Font, Brushes.Black, 181, 24)
        e.Graphics.DrawString("Entitée liée :", Me.Font, Brushes.Black, 197, 24)
        e.Graphics.DrawString(Me.EntiteLiée, Me.Font, Brushes.Black, 262, 24)
        e.Graphics.DrawString("Informations du dossier :", Me.Font, Brushes.Black, 197, 3)
        e.Graphics.DrawString("Montant du paiement :", Me.Font, Brushes.Black, 445, 25)
        e.Graphics.DrawString("À payer :", Me.Font, Brushes.Black, 509, 1)
        e.Graphics.DrawString("$", Me.Font, Brushes.Black, 605, 1)
        e.Graphics.DrawString("$", Me.Font, Brushes.Black, 605, 25)
        e.Graphics.DrawString(Me.DateFacture, Me.Font, Brushes.Black, 645, 0)
    End Sub

    Private Sub PaymentBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Width = 720
        Me.Height = 48
    End Sub

    Private Sub Paying_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Paying.CheckedChanged
        UsingPaymentBox = Paying.Checked
    End Sub

    Private Sub Facture_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Facture.Validating
        If Me.DesignMode = True Then Exit Sub
        If (Facture.Text * (CDbl(_PourcentPaiement) / 100)) < MontantPaye Then MontantFacture = MontantPaye / (CDbl(_PourcentPaiement) / 100)
        If _OldFacture <> CDbl(Facture.Text) Then
            MontantFacture = Facture.Text
            Restant.Text = RoundAmount((Facture.Text * (CDbl(_PourcentPaiement) / 100)) - MontantPaye)
            Paiement.Text = Restant.Text
        End If
    End Sub

    Private Sub Paiement_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Paiement.Validating
        If Me.DesignMode = True Then Exit Sub
        If CDbl(Paiement.Text) > CDbl(Restant.Text) Then Paiement.Text = Restant.Text
        MontantPaiement(False) = Paiement.Text
    End Sub
End Class
