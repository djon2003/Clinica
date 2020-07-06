Public Class FacturationBoxMoreInfos
    Inherits Clinica.Legendes

    Protected Friend Client As String = "Aucun"
    Protected Friend User As String = "Aucun"
    Protected Friend KP As String = "Aucun"
    Protected Friend EntiteLie As String = "Aucun"
    Private WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    Protected Friend Event PourcentageChanged(ByVal sender As Object)

#Region "Constructeur"
    Public Sub New(ByVal ClientLocked As Boolean, ByVal KPLocked As Boolean, ByVal UserLocked As Boolean)
        MyBase.New()
        Me.Caption = "Entité(s) payeuse(s)"
        _LinkedFacture = New Facture()

        InitComponent()

        AddHandler Me.Paint, AddressOf Me.FacturationBoxMoreInfos_Paint

        PourcentClientLocked = ClientLocked
        PourcentKPLocked = KPLocked
        PourcentUserLocked = UserLocked

        PourcentageLocked = False
        Me.DoubleBuffered = True
        Me.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, True)

        MyBase.AttachObjectsMouseEvents()

        Me.ScrollOrientation = Legendes.LegendeOrientation.ScrollRight
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            RemoveHandler Me.Paint, AddressOf Me.FacturationBoxMoreInfos_Paint
            _LinkedFacture = Nothing
            MyBase.DetachObjectsMouseEvents()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitComponent()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()

        Me.Size = New Size(534, 128)

    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'EntitesPayeurs
        '
        Me.Name = "EntitesPayeurs"
        Me.Size = New System.Drawing.Size(536, 288)
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private _LinkedFacture As Facture
    Private _PourcentageLocked As Boolean = False
    Private PourcentClientLocked As Boolean = False
    Private PourcentKPLocked As Boolean = False
    Private PourcentUserLocked As Boolean = False
    Private _NoFactureRefStr As String = "Aucune"
    Private _MontantPaiementClient, _MontantPaiementKP, _MontantPaiementUser, _MontantPaiementClinique As Double
    Private _PourcentParNoClient, _PourcentParNoKP, _PourcentParNoUser As Double

#Region "Propriétés"
    Public Property PourcentParNoClient() As Double
        Get
            Return _PourcentParNoClient
        End Get
        Set(ByVal value As Double)
            _PourcentParNoClient = value
        End Set
    End Property

    Public Property PourcentParNoKP() As Double
        Get
            Return _PourcentParNoKP
        End Get
        Set(ByVal value As Double)
            _PourcentParNoKP = value
        End Set
    End Property

    Public Property PourcentParNoUser() As Double
        Get
            Return _PourcentParNoUser
        End Get
        Set(ByVal value As Double)
            _PourcentParNoUser = value
        End Set
    End Property

    Public Property MontantPaiementClient() As Double
        Get
            Return _MontantPaiementClient
        End Get
        Set(ByVal value As Double)
            _MontantPaiementClient = value
        End Set
    End Property

    Public Property MontantPaiementKP() As Double
        Get
            Return _MontantPaiementKP
        End Get
        Set(ByVal value As Double)
            _MontantPaiementKP = value
        End Set
    End Property

    Public Property MontantPaiementUser() As Double
        Get
            Return _MontantPaiementUser
        End Get
        Set(ByVal value As Double)
            _MontantPaiementUser = value
        End Set
    End Property

    Public Property MontantPaiementClinique() As Double
        Get
            Return _MontantPaiementClinique
        End Get
        Set(ByVal value As Double)
            _MontantPaiementClinique = value
        End Set
    End Property

    Public Property NoFactureRefStr() As String
        Get
            Return _NoFactureRefStr
        End Get
        Set(ByVal value As String)
            _NoFactureRefStr = value
        End Set
    End Property

    Public Property LinkedFacture() As Facture
        Get
            Return _LinkedFacture
        End Get
        Set(ByVal value As Facture)
            _LinkedFacture = value
            Me.Invalidate(True)
        End Set
    End Property

    Public Property PourcentageLocked() As Boolean
        Get
            Return _PourcentageLocked
        End Get
        Set(ByVal Value As Boolean)
            _PourcentageLocked = Value
            'If PourcentClientLocked Or Client.Text = "Aucun" Then
            '    PourcentClient.ReadOnly = True
            'Else
            '    PourcentClient.ReadOnly = Value
            'End If
            'If PourcentKPLocked Or KP.Text = "Aucun(e)" Then
            '    PourcentKP.ReadOnly = True
            'Else
            '    PourcentKP.ReadOnly = Value
            'End If
            'If PourcentUserLocked Or User.Text = "Aucun" Then
            '    PourcentUser.ReadOnly = True
            'Else
            '    PourcentUser.ReadOnly = Value
            'End If
        End Set
    End Property

    Public Property EntiteType() As FacturationBox.DedicatedType
        Get
            Return _EntiteType
        End Get
        Set(ByVal value As FacturationBox.DedicatedType)
            _EntiteType = value
        End Set
    End Property
#End Region

    Private _EntiteType As FacturationBox.DedicatedType

    Private Sub FacturationBoxMoreInfos_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim HeaderSpacing As Integer = 8
        Dim NormalFont As New Font("Microsoft Sans Sherif", 8.25!, FontStyle.Regular)
        Dim BoldFont As New Font(NormalFont, FontStyle.Bold)
        Dim LinkFont As New Font(NormalFont, FontStyle.Underline)

        'Draw Taxe1
        If _LinkedFacture.Taxe1 = -1 Then
            e.Graphics.DrawString("Mixte", NormalFont, Brushes.Black, 360, 16 + HeaderSpacing)
        Else
            e.Graphics.DrawString(_LinkedFacture.Taxe1.ToString & " %", NormalFont, Brushes.Black, 360, 16 + HeaderSpacing)
        End If

        'Draw Taxe2
        If _LinkedFacture.Taxe2 = -1 Then
            If Not IsOverEntiteLie Then e.Graphics.DrawString("Mixte", NormalFont, Brushes.Black, 360, 32 + HeaderSpacing)
        Else
            If Not IsOverEntiteLie Then e.Graphics.DrawString(_LinkedFacture.Taxe2.ToString & " %", NormalFont, Brushes.Black, 360, 32 + HeaderSpacing)
        End If

        'Draw general strings
        e.Graphics.DrawString("$", NormalFont, Brushes.Black, 520, 32 + HeaderSpacing)
        e.Graphics.DrawString("$", NormalFont, Brushes.Black, 520, 48 + HeaderSpacing)
        e.Graphics.DrawString("$", NormalFont, Brushes.Black, 520, 64 + HeaderSpacing)
        e.Graphics.DrawString("$", NormalFont, Brushes.Black, 520, 80 + HeaderSpacing)
        e.Graphics.DrawString("%", BoldFont, Brushes.Black, 160, 16 + HeaderSpacing)
        e.Graphics.DrawString("Taxe 1 :", BoldFont, Brushes.Black, 312, 16 + HeaderSpacing)
        If Not IsOverEntiteLie Then e.Graphics.DrawString("Taxe 2 :", BoldFont, Brushes.Black, 312, 32 + HeaderSpacing)
        e.Graphics.DrawString("Entité liée :", BoldFont, Brushes.Black, 192, 16 + HeaderSpacing)
        e.Graphics.DrawString("-> Utilisateur :", BoldFont, Brushes.Black, 404, 48 + HeaderSpacing)
        If Not IsOverEntiteLie Then e.Graphics.DrawString("-> Client :", BoldFont, Brushes.Black, 404, 32 + HeaderSpacing)
        e.Graphics.DrawString("-> Clinique :", BoldFont, Brushes.Black, 404, 80 + HeaderSpacing)
        e.Graphics.DrawString("Factures liées :", BoldFont, Brushes.Black, 312, 80 + HeaderSpacing)
        e.Graphics.DrawString("Montants payés :", BoldFont, Brushes.Black, 400, 16 + HeaderSpacing)
        e.Graphics.DrawString("N° du reçu :", BoldFont, Brushes.Black, 192, 48 + HeaderSpacing)
        e.Graphics.DrawString("Facture transférée :", BoldFont, Brushes.Black, 192, 80 + HeaderSpacing)
        e.Graphics.DrawString("Utilisateur :", BoldFont, Brushes.Black, 0, 48 + HeaderSpacing)
        e.Graphics.DrawString("Client :", BoldFont, Brushes.Black, 0, 16 + HeaderSpacing)
        e.Graphics.DrawString("-> P/O :", BoldFont, Brushes.Black, 404, 72)
        e.Graphics.DrawString("P/O clé :", BoldFont, Brushes.Black, 0, 88)

        e.Graphics.DrawString(Me.Client, LinkFont, Brushes.Blue, 0, 40)
        e.Graphics.DrawString(Me.KP, LinkFont, Brushes.Blue, 0, 104)
        e.Graphics.DrawString(Me.User, LinkFont, Brushes.Blue, 0, 72)
        If Not IsOverEntiteLie Then e.Graphics.Clip = New Region(New Rectangle(192, 40, 112, NormalFont.Size * 2))
        e.Graphics.DrawString(Me.EntiteLie, LinkFont, Brushes.Blue, 192, 40, New StringFormat(StringFormatFlags.FitBlackBox))
        If Not IsOverEntiteLie Then e.Graphics.Clip = New Region(e.ClipRectangle)
        e.Graphics.DrawString(Me.PourcentParNoClient, NormalFont, Brushes.Black, 152 + (32 - MeasureString(Me.PourcentParNoClient, NormalFont).Width) / 2, 32 + HeaderSpacing)
        e.Graphics.DrawString(Me.PourcentParNoKP, NormalFont, Brushes.Black, 152 + (32 - MeasureString(Me.PourcentParNoKP, NormalFont).Width) / 2, 96 + HeaderSpacing)
        e.Graphics.DrawString(Me.PourcentParNoUser, NormalFont, Brushes.Black, 152 + (32 - MeasureString(Me.PourcentParNoUser, NormalFont).Width) / 2, 64 + HeaderSpacing)
        If _LinkedFacture.GetNoRecusString(True) = "" Then
            e.Graphics.DrawString("Reçu non desservi", NormalFont, Brushes.Black, 192, 64 + HeaderSpacing)
        Else
            e.Graphics.DrawString(_LinkedFacture.GetNoRecusString(True), NormalFont, Brushes.Black, 192, 64 + HeaderSpacing)
        End If
        If _LinkedFacture.NoFactureTransfere = 0 Then
            e.Graphics.DrawString("Facture non transférée", NormalFont, Brushes.Black, 192, 96 + HeaderSpacing)
        Else
            e.Graphics.DrawString(_LinkedFacture.NoFactureTransfere, NormalFont, Brushes.Black, 192, 96 + HeaderSpacing)
        End If
        e.Graphics.DrawString(_MontantPaiementClient.ToString.Replace(".", ","), NormalFont, Brushes.Black, 525 - MeasureString(_MontantPaiementClient.ToString.Replace(".", ","), NormalFont).Width, 32 + HeaderSpacing)
        e.Graphics.DrawString(_MontantPaiementKP.ToString.Replace(".", ","), NormalFont, Brushes.Black, 525 - MeasureString(_MontantPaiementKP.ToString.Replace(".", ","), NormalFont).Width, 64 + HeaderSpacing)
        e.Graphics.DrawString(_MontantPaiementUser.ToString.Replace(".", ","), NormalFont, Brushes.Black, 525 - MeasureString(_MontantPaiementUser.ToString.Replace(".", ","), NormalFont).Width, 48 + HeaderSpacing)
        e.Graphics.DrawString(_MontantPaiementClinique.ToString.Replace(".", ","), NormalFont, Brushes.Black, 525 - MeasureString(_MontantPaiementClinique.ToString.Replace(".", ","), NormalFont).Width, 80 + HeaderSpacing)
        e.Graphics.DrawString(NoFactureRefStr, NormalFont, Brushes.Black, 312, 96 + HeaderSpacing)

        NormalFont.Dispose()
        BoldFont.Dispose()
    End Sub

    Private IsOverClient As Boolean
    Private IsOverKP As Boolean
    Private IsOverUser As Boolean
    Private IsOverEntiteLie As Boolean
    Private IsOverKPLabel1 As Boolean
    Private IsOverKPLabel2 As Boolean

    Private Sub FacturationBoxMoreInfos_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        If IsOverClient Then
            If Me.Client.ToUpper.StartsWith("AUCUN") = False Then
                OpenAccount(_LinkedFacture.ParNoClient)
            End If
        ElseIf IsOverKP Then
            If KP.ToUpper.StartsWith("AUCUN") = False Then
                OpenAccount(_LinkedFacture.ParNoKP, CompteType.KP)
            End If
        ElseIf IsOverUser Then
            If User.ToUpper.StartsWith("AUCUN") = False Then
                OpenAccount(_LinkedFacture.ParNoUser, CompteType.User)
            End If
        ElseIf IsOverEntiteLie Then
            Dim MyEntiteType As FacturationBox.DedicatedType = Me.EntiteType

            Select Case MyEntiteType
                Case FacturationBox.DedicatedType.Client
                    OpenAccount(_LinkedFacture.NoClient)
                Case FacturationBox.DedicatedType.KP
                    OpenAccount(_LinkedFacture.NoKP, CompteType.KP)
                Case FacturationBox.DedicatedType.User
                    OpenAccount(_LinkedFacture.NoUserFacture, CompteType.User)
            End Select
        End If
    End Sub

    Dim CurToolTip As String = ""
    Private Sub FacturationBoxMoreInfos_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim OldIsOverClient, OldIsOverKP, OldIsOverUser, OldIsOverEL, OldIsOverKPLabel1, OldIsOverKPLabel2 As Boolean
        OldIsOverClient = IsOverClient
        OldIsOverKP = IsOverKP
        OldIsOverUser = IsOverUser
        OldIsOverEL = IsOverEntiteLie
        OldIsOverKPLabel1 = IsOverKPLabel1
        OldIsOverKPLabel2 = IsOverKPLabel2

        IsOverClient = e.X > 0 And e.X < 152 And e.Y > 40 And e.Y < 53
        IsOverKP = e.X > 0 And e.X < 152 And e.Y > 104 And e.Y < 117
        IsOverUser = e.X > 0 And e.X < 152 And e.Y > 72 And e.Y < 85
        IsOverEntiteLie = e.X > 192 And e.X < 304 And e.Y > 40 And e.Y < 53
        IsOverKPLabel1 = e.X > 404 And e.X < 455 And e.Y > 72 And e.Y < 89
        IsOverKPLabel2 = e.X > 0 And e.X < 70 And e.Y > 88 And e.Y < 105
        'OldIsOverClient <> IsOverClient OrOr OldIsOverKP <> IsOverKP Or OldIsOverUser <> IsOverUser Or OldIsOverKPLabel1 <> IsOverKPLabel1 Or OldIsOverKPLabel2 <> IsOverKPLabel2 
        If OldIsOverEL <> IsOverEntiteLie Then Me.Invalidate()
        Dim isLink As Boolean = (IsOverClient Or IsOverKP Or IsOverUser Or IsOverEntiteLie)
        Dim isKpTT As Boolean = (IsOverKPLabel1 Or IsOverKPLabel2)

        If isLink And CurToolTip <> "Double-clique pour accéder à ce compte" Then
            CurToolTip = "Double-clique pour accéder à ce compte"
            ToolTip1.SetToolTip(Me, CurToolTip)
            Me.Cursor = Cursors.Hand
        ElseIf isKpTT And CurToolTip <> "Personne / organisme clé" Then
            ToolTip1.SetToolTip(Me, "Personne / organisme clé")
            Me.Cursor = Cursors.Arrow
        ElseIf Not isKpTT And Not isLink And CurToolTip <> "" Then
            CurToolTip = ""
            ToolTip1.SetToolTip(Me, CurToolTip)
            Me.Cursor = Cursors.Arrow
        End If
    End Sub

    Private Sub FacturationBoxMoreInfos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.Width < 534 Then Me.Width = 534
        If Me.Height <> 128 Then Me.Height = 128
    End Sub
End Class
