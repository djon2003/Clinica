Public Class FacturationBoxMoreInfos
    Inherits Clinica.Legend

    Protected Friend client As String = "Aucun"
    Protected Friend user As String = "Aucun"
    Protected Friend kp As String = "Aucun"
    Protected Friend entiteLie As String = "Aucun"
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip

    Protected Friend Event pourcentageChanged(ByVal sender As Object)

#Region "Constructeur"
    Public Sub New(ByVal clientLocked As Boolean, ByVal kpLocked As Boolean, ByVal userLocked As Boolean)
        MyBase.New()
        Me.caption = "Entité(s) payeuse(s)"
        _LinkedFacture = New Bill()

        initComponent()

        AddHandler Me.Paint, AddressOf Me.facturationBoxMoreInfos_Paint

        pourcentClientLocked = clientLocked
        pourcentKPLocked = kpLocked
        pourcentUserLocked = userLocked

        pourcentageLocked = False
        Me.DoubleBuffered = True
        Me.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, True)
        Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, True)

        MyBase.attachObjectsMouseEvents()

        Me.scrollOrientation = Legend.LegendeOrientation.ScrollRight
    End Sub

    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            RemoveHandler Me.Paint, AddressOf Me.facturationBoxMoreInfos_Paint
            _LinkedFacture = Nothing
            MyBase.detachObjectsMouseEvents()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub initComponent()
        Me.toolTip1 = New System.Windows.Forms.ToolTip()

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

    Private _LinkedFacture As Bill
    Private _PourcentageLocked As Boolean = False
    Private pourcentClientLocked As Boolean = False
    Private pourcentKPLocked As Boolean = False
    Private pourcentUserLocked As Boolean = False
    Private _NoFactureRefStr As String = "Aucune"
    Private _MontantPaiementClient, _MontantPaiementKP, _MontantPaiementUser, _MontantPaiementClinique As Double
    Private _PourcentParNoClient, _PourcentParNoKP, _PourcentParNoUser As Double

#Region "Propriétés"
    Public Property pourcentParNoClient() As Double
        Get
            Return _PourcentParNoClient
        End Get
        Set(ByVal value As Double)
            _PourcentParNoClient = value
        End Set
    End Property

    Public Property pourcentParNoKP() As Double
        Get
            Return _PourcentParNoKP
        End Get
        Set(ByVal value As Double)
            _PourcentParNoKP = value
        End Set
    End Property

    Public Property pourcentParNoUser() As Double
        Get
            Return _PourcentParNoUser
        End Get
        Set(ByVal value As Double)
            _PourcentParNoUser = value
        End Set
    End Property

    Public Property montantPaiementClient() As Double
        Get
            Return _MontantPaiementClient
        End Get
        Set(ByVal value As Double)
            _MontantPaiementClient = value
        End Set
    End Property

    Public Property montantPaiementKP() As Double
        Get
            Return _MontantPaiementKP
        End Get
        Set(ByVal value As Double)
            _MontantPaiementKP = value
        End Set
    End Property

    Public Property montantPaiementUser() As Double
        Get
            Return _MontantPaiementUser
        End Get
        Set(ByVal value As Double)
            _MontantPaiementUser = value
        End Set
    End Property

    Public Property montantPaiementClinique() As Double
        Get
            Return _MontantPaiementClinique
        End Get
        Set(ByVal value As Double)
            _MontantPaiementClinique = value
        End Set
    End Property

    Public Property noFactureRefStr() As String
        Get
            Return _NoFactureRefStr
        End Get
        Set(ByVal value As String)
            _NoFactureRefStr = value
        End Set
    End Property

    Public Property linkedFacture() As Bill
        Get
            Return _LinkedFacture
        End Get
        Set(ByVal value As Bill)
            _LinkedFacture = value
            Me.Invalidate(True)
        End Set
    End Property

    Public Property pourcentageLocked() As Boolean
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

    Public Property entiteType() As FacturationBox.DedicatedType
        Get
            Return _EntiteType
        End Get
        Set(ByVal value As FacturationBox.DedicatedType)
            _EntiteType = value
        End Set
    End Property
#End Region

    Private _EntiteType As FacturationBox.DedicatedType

    Private Sub facturationBoxMoreInfos_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim headerSpacing As Integer = 8
        Dim normalFont As New Font("Microsoft Sans Sherif", 8.25!, FontStyle.Regular)
        Dim boldFont As New Font(normalFont, FontStyle.Bold)
        Dim linkFont As New Font(normalFont, FontStyle.Underline)

        'Draw Taxe1
        If _LinkedFacture.taxe1 = -1 Then
            e.Graphics.DrawString("Mixte", normalFont, Brushes.Black, 360, 16 + headerSpacing)
        Else
            e.Graphics.DrawString(_LinkedFacture.taxe1.ToString & " %", normalFont, Brushes.Black, 360, 16 + headerSpacing)
        End If

        'Draw Taxe2
        If _LinkedFacture.taxe2 = -1 Then
            If Not isOverEntiteLie Then e.Graphics.DrawString("Mixte", normalFont, Brushes.Black, 360, 32 + headerSpacing)
        Else
            If Not isOverEntiteLie Then e.Graphics.DrawString(_LinkedFacture.taxe2.ToString & " %", normalFont, Brushes.Black, 360, 32 + headerSpacing)
        End If

        'Draw general strings
        e.Graphics.DrawString("$", normalFont, Brushes.Black, 520, 32 + headerSpacing)
        e.Graphics.DrawString("$", normalFont, Brushes.Black, 520, 48 + headerSpacing)
        e.Graphics.DrawString("$", normalFont, Brushes.Black, 520, 64 + headerSpacing)
        e.Graphics.DrawString("$", normalFont, Brushes.Black, 520, 80 + headerSpacing)
        e.Graphics.DrawString("%", boldFont, Brushes.Black, 160, 16 + headerSpacing)
        e.Graphics.DrawString("Taxe 1 :", boldFont, Brushes.Black, 312, 16 + headerSpacing)
        If Not isOverEntiteLie Then e.Graphics.DrawString("Taxe 2 :", boldFont, Brushes.Black, 312, 32 + headerSpacing)
        e.Graphics.DrawString("Entité liée :", boldFont, Brushes.Black, 192, 16 + headerSpacing)
        e.Graphics.DrawString("-> Utilisateur :", boldFont, Brushes.Black, 404, 48 + headerSpacing)
        If Not isOverEntiteLie Then e.Graphics.DrawString("-> Client :", boldFont, Brushes.Black, 404, 32 + headerSpacing)
        e.Graphics.DrawString("-> Clinique :", boldFont, Brushes.Black, 404, 80 + headerSpacing)
        e.Graphics.DrawString("Factures liées :", boldFont, Brushes.Black, 312, 80 + headerSpacing)
        e.Graphics.DrawString("Montants payés :", boldFont, Brushes.Black, 400, 16 + headerSpacing)
        e.Graphics.DrawString("N° du reçu :", boldFont, Brushes.Black, 192, 48 + headerSpacing)
        e.Graphics.DrawString("Facture transférée :", boldFont, Brushes.Black, 192, 80 + headerSpacing)
        e.Graphics.DrawString("Utilisateur :", boldFont, Brushes.Black, 0, 48 + headerSpacing)
        e.Graphics.DrawString("Client :", boldFont, Brushes.Black, 0, 16 + headerSpacing)
        e.Graphics.DrawString("-> P/O :", boldFont, Brushes.Black, 404, 72)
        e.Graphics.DrawString("P/O clé :", boldFont, Brushes.Black, 0, 88)

        e.Graphics.DrawString(Me.client, linkFont, Brushes.Blue, 0, 40)
        e.Graphics.DrawString(Me.kp, linkFont, Brushes.Blue, 0, 104)
        e.Graphics.DrawString(Me.user, linkFont, Brushes.Blue, 0, 72)
        If Not isOverEntiteLie Then e.Graphics.Clip = New Region(New Rectangle(192, 40, 112, normalFont.Size * 2))
        e.Graphics.DrawString(Me.entiteLie, linkFont, Brushes.Blue, 192, 40, New StringFormat(StringFormatFlags.FitBlackBox))
        If Not isOverEntiteLie Then e.Graphics.Clip = New Region(e.ClipRectangle)
        e.Graphics.DrawString(Me.pourcentParNoClient, normalFont, Brushes.Black, 152 + (32 - measureString(Me.pourcentParNoClient, normalFont).Width) / 2, 32 + headerSpacing)
        e.Graphics.DrawString(Me.pourcentParNoKP, normalFont, Brushes.Black, 152 + (32 - measureString(Me.pourcentParNoKP, normalFont).Width) / 2, 96 + headerSpacing)
        e.Graphics.DrawString(Me.pourcentParNoUser, normalFont, Brushes.Black, 152 + (32 - measureString(Me.pourcentParNoUser, normalFont).Width) / 2, 64 + headerSpacing)
        If _LinkedFacture.getNoReceiptsString(True) = "" Then
            e.Graphics.DrawString("Reçu non desservi", normalFont, Brushes.Black, 192, 64 + headerSpacing)
        Else
            e.Graphics.DrawString(_LinkedFacture.getNoReceiptsString(True), normalFont, Brushes.Black, 192, 64 + headerSpacing)
        End If
        If _LinkedFacture.noBillTransfered = 0 Then
            e.Graphics.DrawString("Facture non transférée", normalFont, Brushes.Black, 192, 96 + headerSpacing)
        Else
            e.Graphics.DrawString(_LinkedFacture.noBillTransfered, normalFont, Brushes.Black, 192, 96 + headerSpacing)
        End If
        e.Graphics.DrawString(_MontantPaiementClient.ToString.Replace(".", ","), normalFont, Brushes.Black, 525 - measureString(_MontantPaiementClient.ToString.Replace(".", ","), normalFont).Width, 32 + headerSpacing)
        e.Graphics.DrawString(_MontantPaiementKP.ToString.Replace(".", ","), normalFont, Brushes.Black, 525 - measureString(_MontantPaiementKP.ToString.Replace(".", ","), normalFont).Width, 64 + headerSpacing)
        e.Graphics.DrawString(_MontantPaiementUser.ToString.Replace(".", ","), normalFont, Brushes.Black, 525 - measureString(_MontantPaiementUser.ToString.Replace(".", ","), normalFont).Width, 48 + headerSpacing)
        e.Graphics.DrawString(_MontantPaiementClinique.ToString.Replace(".", ","), normalFont, Brushes.Black, 525 - measureString(_MontantPaiementClinique.ToString.Replace(".", ","), normalFont).Width, 80 + headerSpacing)
        e.Graphics.DrawString(noFactureRefStr, normalFont, Brushes.Black, 312, 96 + headerSpacing)

        normalFont.Dispose()
        boldFont.Dispose()
    End Sub

    Private isOverClient As Boolean
    Private isOverKP As Boolean
    Private isOverUser As Boolean
    Private isOverEntiteLie As Boolean
    Private isOverKPLabel1 As Boolean
    Private isOverKPLabel2 As Boolean

    Private Sub facturationBoxMoreInfos_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        If isOverClient Then
            If Me.client.ToUpper.StartsWith("AUCUN") = False Then
                openAccount(_LinkedFacture.parNoClient)
            End If
        ElseIf isOverKP Then
            If kp.ToUpper.StartsWith("AUCUN") = False Then
                openAccount(_LinkedFacture.parNoKP, CompteType.KP)
            End If
        ElseIf isOverUser Then
            If user.ToUpper.StartsWith("AUCUN") = False Then
                openAccount(_LinkedFacture.parNoUser, CompteType.User)
            End If
        ElseIf isOverEntiteLie Then
            Dim myEntiteType As FacturationBox.DedicatedType = Me.entiteType

            Select Case myEntiteType
                Case FacturationBox.DedicatedType.Client
                    openAccount(_LinkedFacture.noClient)
                Case FacturationBox.DedicatedType.KP
                    openAccount(_LinkedFacture.noKP, CompteType.KP)
                Case FacturationBox.DedicatedType.User
                    openAccount(_LinkedFacture.noUserFacture, CompteType.User)
            End Select
        End If
    End Sub

    Dim curToolTip As String = ""
    Private Sub facturationBoxMoreInfos_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim OldIsOverClient, OldIsOverKP, OldIsOverUser, OldIsOverEL, OldIsOverKPLabel1, oldIsOverKPLabel2 As Boolean
        OldIsOverClient = isOverClient
        OldIsOverKP = isOverKP
        OldIsOverUser = isOverUser
        OldIsOverEL = isOverEntiteLie
        OldIsOverKPLabel1 = isOverKPLabel1
        oldIsOverKPLabel2 = isOverKPLabel2

        isOverClient = e.X > 0 And e.X < 152 And e.Y > 40 And e.Y < 53
        isOverKP = e.X > 0 And e.X < 152 And e.Y > 104 And e.Y < 117
        isOverUser = e.X > 0 And e.X < 152 And e.Y > 72 And e.Y < 85
        isOverEntiteLie = e.X > 192 And e.X < 304 And e.Y > 40 And e.Y < 53
        isOverKPLabel1 = e.X > 404 And e.X < 455 And e.Y > 72 And e.Y < 89
        isOverKPLabel2 = e.X > 0 And e.X < 70 And e.Y > 88 And e.Y < 105
        'OldIsOverClient <> IsOverClient OrOr OldIsOverKP <> IsOverKP Or OldIsOverUser <> IsOverUser Or OldIsOverKPLabel1 <> IsOverKPLabel1 Or OldIsOverKPLabel2 <> IsOverKPLabel2 
        If OldIsOverEL <> isOverEntiteLie Then Me.Invalidate()
        Dim isLink As Boolean = (isOverClient Or isOverKP Or isOverUser Or isOverEntiteLie)
        Dim isKpTT As Boolean = (isOverKPLabel1 Or isOverKPLabel2)

        If isLink And curToolTip <> "Double-clique pour accéder à ce compte" Then
            curToolTip = "Double-clique pour accéder à ce compte"
            toolTip1.SetToolTip(Me, curToolTip)
            Me.Cursor = Cursors.Hand
        ElseIf isKpTT And curToolTip <> "Personne / organisme clé" Then
            toolTip1.SetToolTip(Me, "Personne / organisme clé")
            Me.Cursor = Cursors.Arrow
        ElseIf Not isKpTT And Not isLink And curToolTip <> "" Then
            curToolTip = ""
            toolTip1.SetToolTip(Me, curToolTip)
            Me.Cursor = Cursors.Arrow
        End If
    End Sub

    Private Sub facturationBoxMoreInfos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.Width < 534 Then Me.Width = 534
        If Me.Height <> 128 Then Me.Height = 128
    End Sub
End Class
