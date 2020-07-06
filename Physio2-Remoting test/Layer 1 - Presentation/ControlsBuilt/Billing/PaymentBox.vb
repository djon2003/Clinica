Public Class PaymentBox
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'addTypePaiement(TypePaiement)

        'Add any initialization after the InitializeComponent() call
        Facture.ReadOnly = Not currentDroitAcces(59)
    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents Facture As ManagedText
    Friend WithEvents Paiement As ManagedText
    Friend WithEvents Restant As ManagedText
    Friend WithEvents TypePaiement As System.Windows.Forms.ComboBox
    Friend WithEvents Paying As System.Windows.Forms.CheckBox
    Friend WithEvents lblBillType As System.Windows.Forms.Label
    Friend Shared WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Friend WithEvents InfoFolder As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.InfoFolder = New System.Windows.Forms.TextBox
        Me.Paying = New System.Windows.Forms.CheckBox
        Me.TypePaiement = New System.Windows.Forms.ComboBox
        Me.Restant = New ManagedText
        Me.Paiement = New ManagedText
        Me.Facture = New ManagedText
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
        Me.Restant.acceptAlpha = False
        Me.Restant.acceptedChars = ",§."
        Me.Restant.acceptNumeric = True
        Me.Restant.allCapital = False
        Me.Restant.allLower = False
        Me.Restant.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Restant.blockOnMaximum = False
        Me.Restant.blockOnMinimum = False
        Me.Restant.cb_AcceptLeftZeros = False
        Me.Restant.cb_AcceptNegative = False
        Me.Restant.currencyBox = True
        Me.Restant.firstLetterCapital = False
        Me.Restant.firstLettersCapital = False
        Me.Restant.Location = New System.Drawing.Point(557, 1)
        Me.Restant.manageText = True
        Me.Restant.matchExp = ""
        Me.Restant.maximum = 0
        Me.Restant.minimum = 0
        Me.Restant.Name = "Restant"
        Me.Restant.nbDecimals = CType(2, Short)
        Me.Restant.onlyAlphabet = False
        Me.Restant.ReadOnly = True
        Me.Restant.refuseAccents = False
        Me.Restant.refusedChars = ""
        Me.Restant.showInternalContextMenu = True
        Me.Restant.Size = New System.Drawing.Size(48, 20)
        Me.Restant.TabIndex = 9
        Me.Restant.Text = "0"
        Me.Restant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Restant.trimText = False
        '
        'Paiement
        '
        Me.Paiement.acceptAlpha = False
        Me.Paiement.acceptedChars = ",§."
        Me.Paiement.acceptNumeric = True
        Me.Paiement.allCapital = False
        Me.Paiement.allLower = False
        Me.Paiement.blockOnMaximum = False
        Me.Paiement.blockOnMinimum = False
        Me.Paiement.cb_AcceptLeftZeros = False
        Me.Paiement.cb_AcceptNegative = False
        Me.Paiement.currencyBox = True
        Me.Paiement.firstLetterCapital = False
        Me.Paiement.firstLettersCapital = False
        Me.Paiement.Location = New System.Drawing.Point(557, 25)
        Me.Paiement.manageText = True
        Me.Paiement.matchExp = ""
        Me.Paiement.maximum = 0
        Me.Paiement.minimum = 0
        Me.Paiement.Name = "Paiement"
        Me.Paiement.nbDecimals = CType(2, Short)
        Me.Paiement.onlyAlphabet = False
        Me.Paiement.refuseAccents = False
        Me.Paiement.refusedChars = ""
        Me.Paiement.showInternalContextMenu = True
        Me.Paiement.Size = New System.Drawing.Size(48, 20)
        Me.Paiement.TabIndex = 4
        Me.Paiement.Text = "0"
        Me.Paiement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Paiement.trimText = False
        '
        'Facture
        '
        Me.Facture.acceptAlpha = False
        Me.Facture.acceptedChars = ",§."
        Me.Facture.acceptNumeric = True
        Me.Facture.allCapital = False
        Me.Facture.allLower = False
        Me.Facture.blockOnMaximum = False
        Me.Facture.blockOnMinimum = False
        Me.Facture.cb_AcceptLeftZeros = False
        Me.Facture.cb_AcceptNegative = False
        Me.Facture.currencyBox = True
        Me.Facture.firstLetterCapital = False
        Me.Facture.firstLettersCapital = False
        Me.Facture.Location = New System.Drawing.Point(133, 24)
        Me.Facture.manageText = True
        Me.Facture.matchExp = ""
        Me.Facture.maximum = 0
        Me.Facture.minimum = 0
        Me.Facture.Name = "Facture"
        Me.Facture.nbDecimals = CType(2, Short)
        Me.Facture.onlyAlphabet = False
        Me.Facture.refuseAccents = False
        Me.Facture.refusedChars = ""
        Me.Facture.showInternalContextMenu = True
        Me.Facture.Size = New System.Drawing.Size(48, 20)
        Me.Facture.TabIndex = 3
        Me.Facture.Text = "0"
        Me.Facture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Facture.trimText = False
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

    Public Event montantPaiementChanged(ByVal sender As Object, ByVal oldAmount As Double, ByVal newAmount As Double)
    Public Event montantFactureChanged(ByVal sender As Object, ByVal oldAmount As Double, ByVal newAmount As Double)
    Public Event usingPaymentBoxChanged(ByVal sender As Object)

#Region "Propriété"
    Public Property locked() As Boolean
        Get
            Return _Locked
        End Get
        Set(ByVal Value As Boolean)
            _Locked = Value
            If currentDroitAcces(59) = False And Value = False Then
                Facture.ReadOnly = True
            Else
                Facture.ReadOnly = Value
            End If
            Paiement.ReadOnly = Value
            Paying.Enabled = Not Value
            TypePaiement.Enabled = Not Value
        End Set
    End Property

    Public Property montantFactureLocked() As Boolean
        Get
            Return Facture.ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            If currentDroitAcces(59) = False And Value = False Then
                Facture.ReadOnly = True
            Else
                Facture.ReadOnly = Value
            End If
        End Set
    End Property


    Public Property noFacture() As Integer
        Get
            Return _NoFacture
        End Get
        Set(ByVal Value As Integer)
            _NoFacture = Value
        End Set
    End Property

    Public Property pourcentPaiement() As Double
        Get
            Return _PourcentPaiement
        End Get
        Set(ByVal Value As Double)
            _PourcentPaiement = Value
        End Set
    End Property

    Public Property negativePaiement() As Boolean
        Get
            Return Paiement.cb_AcceptNegative
        End Get
        Set(ByVal Value As Boolean)
            Paiement.cb_AcceptNegative = Value
        End Set
    End Property

    Public Property readOnlyFacture() As Boolean
        Get
            Return Facture.ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            Facture.ReadOnly = Value
        End Set
    End Property

    Public Property infoOnFolder() As String
        Get
            Return InfoFolder.Text
        End Get
        Set(ByVal Value As String)
            InfoFolder.Text = Value
        End Set
    End Property

    Public Property entiteLiée() As String
        Get
            Return entiteLiee
        End Get
        Set(ByVal Value As String)
            entiteLiee = Value
        End Set
    End Property

    Public Property UsingPaymentBox(Optional ByVal raisingEvent As Boolean = True) As Boolean
        Get
            Return Paying.Checked
        End Get
        Set(ByVal Value As Boolean)
            Paying.Checked = Value
            If raisingEvent = True Then RaiseEvent usingPaymentBoxChanged(Me)
        End Set
    End Property

    Public Property MontantPaiement(Optional ByVal changeValue As Boolean = True) As Double
        Get
            Return Paiement.Text
        End Get
        Set(ByVal Value As Double)
            RaiseEvent montantPaiementChanged(Me, _OldValue, Value)
            If changeValue = True Then Paiement.Text = Value
            _OldValue = Value
        End Set
    End Property

    Public Property montantFacture() As Double
        Get
            Return Facture.Text
        End Get
        Set(ByVal Value As Double)
            RaiseEvent montantFactureChanged(Me, Facture.Text, Value)
            Facture.Text = Value
            _OldFacture = Value
        End Set
    End Property

    Public Property montantRestant() As Double
        Get
            Return Restant.Text
        End Get
        Set(ByVal Value As Double)
            Restant.Text = roundAmount(Value)
        End Set
    End Property

    Public Property typeFacture() As String
        Get
            Return billType
        End Get
        Set(ByVal Value As String)
            billType = Value
            Dim textSize As Size = measureString(Value, lblBillType.Font)
            If textSize.Width > lblBillType.Width Then
                lblBillType.Text = Value.Substring(0, Value.Length * lblBillType.Width / textSize.Width - 5) & "..."
            Else
                lblBillType.Text = Value
            End If
            ToolTip1.SetToolTip(lblBillType, Value)
        End Set
    End Property
    Public Property dateFacture() As String
        Get
            Return billDate
        End Get
        Set(ByVal Value As String)
            billDate = Value
        End Set
    End Property

    Public Property valueA() As Object
        Get
            Return _ValueA
        End Get
        Set(ByVal Value As Object)
            _ValueA = Value
        End Set
    End Property

    Public Property valueB() As Object
        Get
            Return _ValueB
        End Get
        Set(ByVal Value As Object)
            _ValueB = Value
        End Set
    End Property

    Public Property montantPaye() As Double
        Get
            Return _MontantPaye
        End Get
        Set(ByVal Value As Double)
            _MontantPaye = Value
        End Set
    End Property

    Public Property paymentMethod() As String
        Get
            Return TypePaiement.SelectedItem
        End Get
        Set(ByVal Value As String)
            If Value <> "" Then TypePaiement.SelectedIndex = TypePaiement.FindStringExact(Value)
        End Set
    End Property
#End Region

    Private Sub paymentBox_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim boldFont As Font = New Font(Me.Font, FontStyle.Bold)

        e.Graphics.DrawString("Montant de la facture :", Me.Font, Brushes.Black, 21, 24)
        e.Graphics.DrawString("$", Me.Font, Brushes.Black, 181, 24)
        e.Graphics.DrawString("Entitée liée :", Me.Font, Brushes.Black, 197, 24)
        e.Graphics.DrawString(Me.entiteLiée, Me.Font, Brushes.Black, 262, 24)
        e.Graphics.DrawString("Informations du dossier :", Me.Font, Brushes.Black, 197, 3)
        e.Graphics.DrawString("Montant du paiement :", Me.Font, Brushes.Black, 445, 25)
        e.Graphics.DrawString("À payer :", Me.Font, Brushes.Black, 509, 1)
        e.Graphics.DrawString("$", Me.Font, Brushes.Black, 605, 1)
        e.Graphics.DrawString("$", Me.Font, Brushes.Black, 605, 25)
        e.Graphics.DrawString(Me.dateFacture, Me.Font, Brushes.Black, 645, 0)
    End Sub

    Private Sub paymentBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Width = 720
        Me.Height = 48
    End Sub

    Private Sub paying_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Paying.CheckedChanged
        UsingPaymentBox = Paying.Checked
    End Sub

    Private Sub facture_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Facture.Validating
        If Me.DesignMode = True Then Exit Sub
        If (Facture.Text * (CDbl(_PourcentPaiement) / 100)) < montantPaye Then montantFacture = montantPaye / (CDbl(_PourcentPaiement) / 100)
        If _OldFacture <> CDbl(Facture.Text) Then
            montantFacture = Facture.Text
            Restant.Text = roundAmount((Facture.Text * (CDbl(_PourcentPaiement) / 100)) - montantPaye)
            Paiement.Text = Restant.Text
        End If
    End Sub

    Private Sub paiement_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Paiement.Validating
        If Me.DesignMode = True Then Exit Sub
        If CDbl(Paiement.Text) > CDbl(Restant.Text) Then Paiement.Text = Restant.Text
        MontantPaiement(False) = Paiement.Text
    End Sub
End Class
