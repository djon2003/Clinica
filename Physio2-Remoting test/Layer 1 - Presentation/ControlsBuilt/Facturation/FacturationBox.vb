Public Class FacturationBox
    Inherits System.Windows.Forms.UserControl

    Private WithEvents Payeurs As FacturationBoxMoreInfos

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal ClientLocked As Boolean, ByVal KPLocked As Boolean, ByVal UserLocked As Boolean, ByVal BillDedicatedTo As DedicatedType)
        MyBase.New()

        DedicatedTo = BillDedicatedTo

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        '
        'Payeurs
        '
        Payeurs = New FacturationBoxMoreInfos(ClientLocked, KPLocked, UserLocked)
        Payeurs.Visible = True
        Payeurs.StartingPosition = (Me.Groupe.DisplayRectangle.Height - 10)
        Payeurs.EndingPosition = Me.Groupe.DisplayRectangle.Height - Payeurs.Height
        Payeurs.MovingSpeed = 5
        Payeurs.Top = Me.Groupe.DisplayRectangle.Height - 10
        Payeurs.Left = 0
        Payeurs.ScrollOrientation = Legendes.LegendeOrientation.ScrollUp
        Me.Groupe.Controls.Add(Payeurs)
        Payeurs.BringToFront()

        Me.saveDesc.BackgroundImage = DrawingManager.GetInstance.GetImage("save.jpg")
        Me.cancelDesc.BackgroundImage = DrawingManager.GetInstance.GetImage("annuler16.gif")

        Me.TempMontantFacture.SendToBack()
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            If Not Payeurs Is Nothing Then Payeurs.Dispose()
            _ValueA = Nothing
            _ValueB = Nothing
            ThisFacture.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents Groupe As System.Windows.Forms.Panel
    Private WithEvents Facture As ManagedText
    Private WithEvents Paiement As ManagedText
    Private WithEvents Restant As Clinica.ManagedText
    Private WithEvents HistoPaiements As Clinica.ListCombo
    Private WithEvents HistoFactures As Clinica.ListCombo
    Private WithEvents Description As Clinica.ManagedText
    Private WithEvents SelectF As System.Windows.Forms.RadioButton
    Private WithEvents SelectP As System.Windows.Forms.RadioButton
    Private WithEvents Adjusting As System.Windows.Forms.Button
    Private WithEvents saveDesc As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents cancelDesc As System.Windows.Forms.Button
    Friend WithEvents lblTypeFacture As System.Windows.Forms.Label

    Private WithEvents TempMontantFacture As Clinica.ManagedText

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FacturationBox))
        Me.Groupe = New System.Windows.Forms.Panel
        Me.lblTypeFacture = New System.Windows.Forms.Label
        Me.HistoPaiements = New Clinica.ListCombo
        Me.HistoFactures = New Clinica.ListCombo
        Me.saveDesc = New System.Windows.Forms.Button
        Me.cancelDesc = New System.Windows.Forms.Button
        Me.Adjusting = New System.Windows.Forms.Button
        Me.Description = New Clinica.ManagedText
        Me.Paiement = New Clinica.ManagedText
        Me.SelectP = New System.Windows.Forms.RadioButton
        Me.Facture = New Clinica.ManagedText
        Me.SelectF = New System.Windows.Forms.RadioButton
        Me.Restant = New Clinica.ManagedText
        Me.TempMontantFacture = New Clinica.ManagedText
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Groupe.SuspendLayout()
        Me.SuspendLayout()
        '
        'Groupe
        '
        Me.Groupe.AutoScrollMargin = New System.Drawing.Size(5, 0)
        Me.Groupe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Groupe.Controls.Add(Me.HistoPaiements)
        Me.Groupe.Controls.Add(Me.HistoFactures)
        Me.Groupe.Controls.Add(Me.saveDesc)
        Me.Groupe.Controls.Add(Me.cancelDesc)
        Me.Groupe.Controls.Add(Me.Adjusting)
        Me.Groupe.Controls.Add(Me.Description)
        Me.Groupe.Controls.Add(Me.Paiement)
        Me.Groupe.Controls.Add(Me.SelectP)
        Me.Groupe.Controls.Add(Me.Facture)
        Me.Groupe.Controls.Add(Me.SelectF)
        Me.Groupe.Controls.Add(Me.Restant)
        Me.Groupe.Controls.Add(Me.lblTypeFacture)
        Me.Groupe.Location = New System.Drawing.Point(0, 0)
        Me.Groupe.Name = "Groupe"
        Me.Groupe.Size = New System.Drawing.Size(536, 120)
        Me.Groupe.TabIndex = 2
        '
        'lblTypeFacture
        '
        Me.lblTypeFacture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTypeFacture.Location = New System.Drawing.Point(8, 24)
        Me.lblTypeFacture.Name = "lblTypeFacture"
        Me.lblTypeFacture.Size = New System.Drawing.Size(347, 13)
        Me.lblTypeFacture.TabIndex = 28
        Me.lblTypeFacture.Text = "TypeFacture"
        '
        'HistoPaiements
        '
        Me.HistoPaiements.ChangeText = "Historique des paiements / ajustements"
        Me.HistoPaiements.ClickEnabled = True
        Me.HistoPaiements.Draw = False
        Me.HistoPaiements.DropDownHeight = 98
        Me.HistoPaiements.DroppedDown = False
        Me.HistoPaiements.HSValue = CType(0, Short)
        Me.HistoPaiements.Location = New System.Drawing.Point(267, 0)
        Me.HistoPaiements.Name = "HistoPaiements"
        Me.HistoPaiements.Selected = CType(-1, Short)
        Me.HistoPaiements.Size = New System.Drawing.Size(267, 20)
        Me.HistoPaiements.Sorted = False
        Me.HistoPaiements.TabIndex = 20
        Me.HistoPaiements.VSValue = CType(0, Short)
        '
        'HistoFactures
        '
        Me.HistoFactures.ChangeText = "Historique de la facture"
        Me.HistoFactures.ClickEnabled = True
        Me.HistoFactures.Draw = False
        Me.HistoFactures.DropDownHeight = 98
        Me.HistoFactures.DroppedDown = False
        Me.HistoFactures.HSValue = CType(0, Short)
        Me.HistoFactures.Location = New System.Drawing.Point(0, 0)
        Me.HistoFactures.Name = "HistoFactures"
        Me.HistoFactures.Selected = CType(-1, Short)
        Me.HistoFactures.Size = New System.Drawing.Size(267, 20)
        Me.HistoFactures.Sorted = False
        Me.HistoFactures.TabIndex = 19
        Me.HistoFactures.VSValue = CType(0, Short)
        '
        'saveDesc
        '
        Me.saveDesc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.saveDesc.Location = New System.Drawing.Point(425, 26)
        Me.saveDesc.Name = "saveDesc"
        Me.saveDesc.Size = New System.Drawing.Size(16, 16)
        Me.saveDesc.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.saveDesc, "Enregistrer la description de la facture")
        Me.saveDesc.Visible = False
        '
        'cancelDesc
        '
        Me.cancelDesc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cancelDesc.Location = New System.Drawing.Point(440, 26)
        Me.cancelDesc.Name = "cancelDesc"
        Me.cancelDesc.Size = New System.Drawing.Size(16, 16)
        Me.cancelDesc.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.cancelDesc, "Ne pas enregistrer les modifications apportées à la description")
        Me.cancelDesc.Visible = False
        '
        'Adjusting
        '
        Me.Adjusting.Location = New System.Drawing.Point(464, 96)
        Me.Adjusting.Name = "Adjusting"
        Me.Adjusting.Size = New System.Drawing.Size(64, 20)
        Me.Adjusting.TabIndex = 27
        Me.Adjusting.Text = "Ajuster"
        '
        'Description
        '
        Me.Description.AcceptAlpha = True
        Me.Description.AcceptedChars = ""
        Me.Description.AcceptNumeric = True
        Me.Description.AllCapital = False
        Me.Description.AllLower = False
        Me.Description.BlockOnMaximum = False
        Me.Description.BlockOnMinimum = False
        Me.Description.CB_AcceptLeftZeros = False
        Me.Description.CB_AcceptNegative = False
        Me.Description.CurrencyBox = False
        Me.Description.FirstLetterCapital = True
        Me.Description.FirstLettersCapital = False
        Me.Description.Location = New System.Drawing.Point(88, 96)
        Me.Description.ManageText = True
        Me.Description.MatchExp = ""
        Me.Description.Maximum = 0
        Me.Description.Minimum = 0
        Me.Description.Multiline = True
        Me.Description.Name = "Description"
        Me.Description.NbDecimals = CType(2, Short)
        Me.Description.OnlyAlphabet = False
        Me.Description.RefuseAccents = False
        Me.Description.RefusedChars = ""
        Me.Description.ShowInternalContextMenu = True
        Me.Description.Size = New System.Drawing.Size(368, 20)
        Me.Description.TabIndex = 23
        Me.Description.TrimText = False
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
        Me.Paiement.Location = New System.Drawing.Point(288, 72)
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
        'SelectP
        '
        Me.SelectP.Checked = True
        Me.SelectP.Location = New System.Drawing.Point(192, 72)
        Me.SelectP.Name = "SelectP"
        Me.SelectP.Size = New System.Drawing.Size(112, 16)
        Me.SelectP.TabIndex = 25
        Me.SelectP.TabStop = True
        Me.SelectP.Text = "Montant payé :"
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
        Me.Facture.Location = New System.Drawing.Point(112, 72)
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
        'SelectF
        '
        Me.SelectF.Location = New System.Drawing.Point(8, 72)
        Me.SelectF.Name = "SelectF"
        Me.SelectF.Size = New System.Drawing.Size(112, 16)
        Me.SelectF.TabIndex = 24
        Me.SelectF.Text = "Montant facturé :"
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
        Me.Restant.Location = New System.Drawing.Point(472, 72)
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
        'TempMontantFacture
        '
        Me.TempMontantFacture.AcceptAlpha = False
        Me.TempMontantFacture.AcceptedChars = ",§."
        Me.TempMontantFacture.AcceptNumeric = True
        Me.TempMontantFacture.AllCapital = False
        Me.TempMontantFacture.AllLower = False
        Me.TempMontantFacture.BlockOnMaximum = False
        Me.TempMontantFacture.BlockOnMinimum = False
        Me.TempMontantFacture.CB_AcceptLeftZeros = False
        Me.TempMontantFacture.CB_AcceptNegative = False
        Me.TempMontantFacture.CurrencyBox = True
        Me.TempMontantFacture.FirstLetterCapital = False
        Me.TempMontantFacture.FirstLettersCapital = False
        Me.TempMontantFacture.Location = New System.Drawing.Point(0, 0)
        Me.TempMontantFacture.ManageText = True
        Me.TempMontantFacture.MatchExp = ""
        Me.TempMontantFacture.Maximum = 0
        Me.TempMontantFacture.Minimum = 0
        Me.TempMontantFacture.Name = "TempMontantFacture"
        Me.TempMontantFacture.NbDecimals = CType(2, Short)
        Me.TempMontantFacture.OnlyAlphabet = False
        Me.TempMontantFacture.RefuseAccents = False
        Me.TempMontantFacture.RefusedChars = ""
        Me.TempMontantFacture.ShowInternalContextMenu = True
        Me.TempMontantFacture.Size = New System.Drawing.Size(48, 20)
        Me.TempMontantFacture.TabIndex = 3
        Me.TempMontantFacture.Text = "0"
        Me.TempMontantFacture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TempMontantFacture.TrimText = False
        '
        'FacturationBox
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.TempMontantFacture)
        Me.Controls.Add(Me.Groupe)
        Me.Name = "FacturationBox"
        Me.Size = New System.Drawing.Size(536, 130)
        Me.Groupe.ResumeLayout(False)
        Me.Groupe.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _ValueA, _ValueB As Object
    Private _OldValue As Double
    Private MyPath, _NAM As String
    Private _Locked As Boolean = False
    Private _NoFacture As Integer
    Private _IsUnified As Boolean = False
    Private _IsLinked As Boolean = False
    Private _EntiteType As DedicatedType
    Private _EntiteLie As Integer
    Private LoadingTime As Double

    Private DedicatedTo As DedicatedType

    Private ThisFacture As New Facture
    Private InfoFolder As String = ""

    Public CurrentFacture, CurrentFactureClient, CurrentFactureKP, CurrentFactureUser, CurrentFactureClinique, CurrentPaiementClient, CurrentPaiementKP, CurrentPaiementUser, CurrentPaiementClinique As Double
    Public Event MPChanged(ByVal sender As Object, ByVal OldAmount As Double, ByVal NewAmount As Double)
    Public Event UPBChanged(ByVal sender As Object)
    Public Event CreatedFacturation(ByVal sender As Object)

    Public Enum DedicatedType
        Client = 0
        KP = 1
        User = 2
        All = 3
        Clinique = 4
    End Enum


#Region "Propriété"
    Public ReadOnly Property CurFacture() As Facture
        Get
            Return ThisFacture
        End Get
    End Property

    Public Property CurrentType() As DedicatedType
        Get
            Return DedicatedTo
        End Get
        Set(ByVal value As DedicatedType)
            DedicatedTo = value
        End Set
    End Property

    Public Property Locked() As Boolean
        Get
            Return _Locked
        End Get
        Set(ByVal Value As Boolean)
            _Locked = Value

            If Me.IsUnified Then Value = True
            Payeurs.PourcentageLocked = True
            Description.ReadOnly = Value
            Facture.ReadOnly = Value
            If CurFacture.GetNoRecusString <> "" And CurrentDroitAcces(91) = False Then 'Si un reçu a été émis pour cette facture, bloque la modif du paiement
                Paiement.ReadOnly = True
            Else
                Paiement.ReadOnly = Value
            End If
            Adjusting.Enabled = Not Value
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

    Public Property MontantPaiement(Optional ByVal ChangeValue As Boolean = True) As Double
        Get
            Return Paiement.Text
        End Get
        Set(ByVal Value As Double)
            RaiseEvent MPChanged(Me, _OldValue, Value)
            If ChangeValue = True Then Paiement.Text = Value
            _OldValue = Value
        End Set
    End Property

    Public Property MontantFacture() As Double
        Get
            Return Facture.Text
        End Get
        Set(ByVal Value As Double)
            Facture.Text = Value
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

    Public Property SetNom() As String
        Get
            Return CurFacture.TypeFacture
        End Get
        Set(ByVal Value As String)
            CurFacture.TypeFacture = Value
            Me.Invalidate(True)
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

    Public ReadOnly Property IsUnified() As Boolean
        Get
            Return CurFacture.NoFactureRef <> ""
        End Get
    End Property

    Public ReadOnly Property IsLinked() As Boolean
        Get
            Return (CurFacture.NoFactureTransfere <> 0)
        End Get
    End Property

    Public ReadOnly Property EntiteLie() As Integer
        Get
            Return _EntiteLie
        End Get
    End Property

    Public ReadOnly Property EntiteType() As DedicatedType
        Get
            Return _EntiteType
        End Get
    End Property

    Private Property CurrentPaiement() As Double
        Get
            Select Case DedicatedTo
                Case DedicatedType.All
                    Return (CurrentPaiementClient + CurrentPaiementKP + CurrentPaiementUser + CurrentPaiementClinique)
                Case DedicatedType.Client
                    Return CurrentPaiementClient
                Case DedicatedType.KP
                    Return CurrentPaiementKP
                Case DedicatedType.User
                    Return CurrentPaiementUser
                Case DedicatedType.Clinique
                    Return CurrentPaiementClinique
            End Select
        End Get
        Set(ByVal Value As Double)
            Select Case DedicatedTo
                Case DedicatedType.Client
                    CurrentPaiementClient = Value
                Case DedicatedType.KP
                    CurrentPaiementKP = Value
                Case DedicatedType.User
                    CurrentPaiementUser = Value
                Case DedicatedType.Clinique
                    CurrentPaiementClinique = Value
            End Select
        End Set
    End Property
#End Region

    Public Function LockSecteur(ByVal Secteur As String, ByRef CurrentLocks As ArrayList) As Boolean
        Dim IsCurLocked As Boolean = True
        Dim Locking As Boolean = False
        Dim CurEntiteLock As String = ""
        Dim CurEntiteLocks As New Collections.Generic.List(Of String)
        With Me
            'Entité liée
            If .CurFacture.NoClient > 0 Then CurEntiteLock = "ClientFacturation-" & .CurFacture.NoClient
            If .CurFacture.NoKP > 0 Then CurEntiteLock = "KPFacturation-" & .CurFacture.NoKP
            If .CurFacture.NoUserFacture > 0 Then CurEntiteLock = "UserFacturation-" & .CurFacture.NoUserFacture
            If CurEntiteLock <> "" AndAlso CurrentLocks.IndexOf(CurEntiteLock) < 0 AndAlso Clinica.LockSecteur(CurEntiteLock, True, Secteur, False) = False Then
                .Locked = True
                Return False
            Else
                If CurEntiteLock <> "" AndAlso CurrentLocks.IndexOf(CurEntiteLock) = -1 Then CurEntiteLocks.Add(CurEntiteLock)
            End If

            'Payeurs
            If .CurFacture.ParNoClinique > 0 AndAlso CurrentLocks.IndexOf("CliniqueFacturation-" & .CurFacture.ParNoClinique) < 0 AndAlso Clinica.LockSecteur("CliniqueFacturation-" & .CurFacture.ParNoClinique, True, Secteur, False) = False Then
                Locking = True
            Else
                If .CurFacture.ParNoClinique > 0 AndAlso CurrentLocks.IndexOf("CliniqueFacturation-" & .CurFacture.ParNoClinique) < 0 Then CurEntiteLocks.Add("CliniqueFacturation-" & .CurFacture.ParNoClinique)
            End If
            If Locking = False AndAlso .CurFacture.ParNoUser > 0 AndAlso .CurFacture.ParNoUser <> .CurFacture.NoUserFacture AndAlso CurrentLocks.IndexOf("UserFacturation-" & .CurFacture.ParNoUser) < 0 AndAlso Clinica.LockSecteur("UserFacturation-" & .CurFacture.ParNoUser, True, Secteur, False) = False Then
                Locking = True
            Else
                If Locking = False AndAlso .CurFacture.ParNoUser <> .CurFacture.NoUserFacture AndAlso .CurFacture.ParNoUser > 0 AndAlso CurrentLocks.IndexOf("UserFacturation-" & .CurFacture.ParNoUser) < 0 Then CurEntiteLocks.Add("UserFacturation-" & .CurFacture.ParNoUser)
            End If
            If Locking = False AndAlso .CurFacture.ParNoKP > 0 AndAlso .CurFacture.ParNoKP <> .CurFacture.NoKP AndAlso CurrentLocks.IndexOf("KPFacturation-" & .CurFacture.ParNoKP) < 0 AndAlso Clinica.LockSecteur("KPFacturation-" & .CurFacture.ParNoKP, True, Secteur, False) = False Then
                Locking = True
            Else
                If Locking = False AndAlso .CurFacture.ParNoKP <> .CurFacture.NoKP AndAlso .CurFacture.ParNoKP > 0 AndAlso CurrentLocks.IndexOf("KPFacturation-" & .CurFacture.ParNoKP) < 0 Then CurEntiteLocks.Add("KPFacturation-" & .CurFacture.ParNoKP)
            End If
            If Locking = False AndAlso .CurFacture.ParNoClient > 0 AndAlso .CurFacture.ParNoClient <> .CurFacture.NoClient AndAlso CurrentLocks.IndexOf("ClientFacturation-" & .CurFacture.ParNoClient) < 0 AndAlso Clinica.LockSecteur("ClientFacturation-" & .CurFacture.ParNoClient, True, Secteur, False) = False Then
                Locking = True
            Else
                If Locking = False AndAlso .CurFacture.ParNoClient <> .CurFacture.NoClient AndAlso .CurFacture.ParNoClient > 0 AndAlso CurrentLocks.IndexOf("ClientFacturation-" & .CurFacture.ParNoClient) < 0 Then CurEntiteLocks.Add("ClientFacturation-" & .CurFacture.ParNoClient)
            End If

            If Locking = True Then
                Dim i As Integer
                For i = 0 To CurEntiteLocks.Count - 1
                    If Clinica.LockSecteur(CurEntiteLocks(i), False, Secteur, False) = False Then
                        IsCurLocked = False
                        Exit For
                    End If
                Next i

                If IsCurLocked = False Then 'Annule les locks effectués précédemment le dernier qui a été refusé
                    For j As Integer = 0 To i - 1
                        Clinica.LockSecteur(CurEntiteLocks(j), False)
                    Next j
                End If
            Else
                For Each Lock As String In CurEntiteLocks
                    CurrentLocks.Add(Lock)
                Next
            End If

            .Locked = Locking
        End With

        Return IsCurLocked
    End Function

    Public Function Loading(ByVal MyNoFacture As Integer, Optional ByRef MyPaiements As DataSet = Nothing, Optional ByRef MyFactures As DataSet = Nothing, Optional ByRef StartingIndexFacture As Integer = 0, Optional ByRef StartingIndexPaiement As Integer = 0, Optional ByVal InfoFolder As String = "") As Boolean
        If CurrentUserName = "Administrateur" Then LoadingTime = Date.Now.Ticks
        Me.InfoFolder = InfoFolder
        CurrentFacture = 0
        CurrentFactureClient = 0
        CurrentFactureKP = 0
        CurrentFactureUser = 0
        CurrentFactureClinique = 0
        CurrentPaiementClient = 0
        CurrentPaiementKP = 0
        CurrentPaiementUser = 0
        CurrentPaiementClinique = 0
        Paiement.Text = 0
        Restant.Text = 0
        Facture.Text = 0
        NoFacture = MyNoFacture

        Dim TempDedicated As DedicatedType
        Dim DedicatedString, BasicInfo(,) As String
        Dim HistoPaiementLines, HistoFactureLines, j, FirstRowForFacture, FirstRowForPaiement As Integer

        Dim HPaiements, HFactures As New DBHelper.StatType
        'Set Facture to display
        If IsNothing(MyFactures) Then
            Dim SelfOpened As Boolean = False
            If DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpened = True

            ThisFacture = New Facture(MyNoFacture)
            If IsNothing(ThisFacture) Then
                If SelfOpened = True Then DBLinker.GetInstance().DBConnected = False
                Return False
            End If

            If ThisFacture.NoFolder > 0 And Me.InfoFolder = "" Then
                'REM_CODES
                BasicInfo = DBLinker.GetInstance.ReadDB("Utilisateurs INNER JOIN InfoFolders ON Utilisateurs.NoUser = InfoFolders.NoTRPTraitant", "InfoFolders.NoCodeUnique, Utilisateurs.[Nom]+','+ Utilisateurs.[Prenom] AS FullName", "WHERE (NoFolder=" & ThisFacture.NoFolder & ");")
                If Not BasicInfo Is Nothing AndAlso BasicInfo.Length <> 0 Then
                    Me.InfoFolder = ThisFacture.NoFolder & "-" & BasicInfo(1, 0) & "-" & Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNameByNoUnique(BasicInfo(0, 0))
                End If

            End If

            StartingIndexPaiement = 0 : StartingIndexFacture = 0
            'If Facture liée then grow Histos
            If ThisFacture.NoFactureRef <> "" Then
                Dim FacturesNos As String = "(" & ThisFacture.GetAllFacturesLinked(False) & ")"
                FacturesNos = " IN " & FacturesNos
                '                HPaiements = DBHelper.ReadStats("StatPaiements", "NoFacture", 0, "StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending)
                '               HFactures = DBHelper.ReadStats("StatFactures", "NoFacture", 0, "StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending)

                HPaiements = DBHelper.ReadStats("StatPaiements", "NoFacture", FacturesNos, "NoFacture,StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending, , , False, New String(,) {{"ListeNoRecus"}, {"NoNoRecu"}})
                HFactures = DBHelper.ReadStats("StatFactures", "NoFacture", FacturesNos, "NoFacture,StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending, , , False)
                FirstRowForFacture = 0
                FirstRowForPaiement = 0
            Else
                HPaiements = DBHelper.ReadStats("StatPaiements", "NoFacture", MyNoFacture, "StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending, , , , New String(,) {{"ListeNoRecus"}, {"NoNoRecu"}})
                HFactures = DBHelper.ReadStats("StatFactures", "NoFacture", MyNoFacture, "StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending)
                FirstRowForFacture = StartingIndexFacture
                FirstRowForPaiement = StartingIndexPaiement
            End If

            If SelfOpened = True Then DBLinker.GetInstance().DBConnected = False
        Else
            ThisFacture = New Facture
            HPaiements.StatDataSet = MyPaiements
            HFactures.StatDataSet = MyFactures

            ThisFacture.LoadingData(HFactures.StatDataSet.Tables("Table").Rows(StartingIndexFacture))

            'If Facture liée then grow Histos
            If ThisFacture.NoFactureRef <> "" Then
                Dim FacturesNos As String = "(" & ThisFacture.GetAllFacturesLinked(False) & ")"
                FacturesNos = " IN " & FacturesNos
                HPaiements = DBHelper.ReadStats("StatPaiements", "NoFacture", FacturesNos, "NoFacture,StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending, , , False, New String(,) {{"ListeNoRecus"}, {"NoNoRecu"}})
                HFactures = DBHelper.ReadStats("StatFactures", "NoFacture", FacturesNos, "NoFacture,StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending, , , False)
                FirstRowForFacture = 0
                FirstRowForPaiement = 0
            Else
                FirstRowForFacture = StartingIndexFacture
                FirstRowForPaiement = StartingIndexPaiement
            End If
        End If

        'Set Payeurs & Entité lié
        If ThisFacture.NoClient > 0 Then
            _EntiteType = DedicatedType.Client
            _EntiteLie = ThisFacture.NoClient
        ElseIf ThisFacture.NoKP > 0 Then
            _EntiteType = DedicatedType.KP
            _EntiteLie = ThisFacture.NoKP
        Else
            _EntiteType = DedicatedType.User
            _EntiteLie = ThisFacture.NoUser
        End If
        Me.Payeurs.EntiteType = Me.EntiteType

        'Set Histos of Facture and set fields to display
        If IsNothing(HFactures) = False Then
            HistoFactureLines = 0
            HistoPaiementLines = 0
            Dim lastHistoLine As String = ""
            Dim newHistoLine As String = ""
            With Me
                .NoFacture = MyNoFacture

                'TypeFacture
                Dim typeFacture As String = ThisFacture.TypeFacture
                Dim sizeTypeFacture As Size = MeasureString(typeFacture, lblTypeFacture.Font)
                If sizeTypeFacture.Width > lblTypeFacture.Width Then
                    typeFacture = typeFacture.Substring(0, typeFacture.Length * lblTypeFacture.Width / sizeTypeFacture.Width - 5) & "..."
                End If
                lblTypeFacture.Text = typeFacture
                ToolTip1.SetToolTip(lblTypeFacture, ThisFacture.TypeFacture)
                If ThisFacture.IsSouffrance Then
                    lblTypeFacture.ForeColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorTitreFactureSouffrance"))
                Else
                    lblTypeFacture.ForeColor = Color.Black
                End If

                'Entité
                Dim ClientName As String = ""
                Dim KPName As String = ""
                If ThisFacture.NoClient > 0 Then
                    ClientName = GetClientName(ThisFacture.NoClient) & " (" & ThisFacture.NoClient & ")"
                    Me.Payeurs.EntiteLie = ClientName & " " & Me.InfoFolder
                End If
                If ThisFacture.NoKP > 0 Then KPName = GetKPName(ThisFacture.NoKP) & " (" & ThisFacture.NoKP & ")" : Me.Payeurs.EntiteLie = KPName
                If ThisFacture.NoUserFacture > 0 Then Me.Payeurs.EntiteLie = UserManager.getInstance.getUser(ThisFacture.NoUserFacture).ToString()

                'Payeurs
                If ThisFacture.ParNoClient > 0 Then
                    If ClientName = "" Or ThisFacture.ParNoClient <> ThisFacture.NoClient Then ClientName = GetClientName(ThisFacture.ParNoClient) & " (" & ThisFacture.ParNoClient & ")"
                    Me.Payeurs.Client = ClientName
                Else
                    Me.Payeurs.Client = "Aucun"
                End If
                If ThisFacture.ParNoKP > 0 Then
                    If KPName = "" Or ThisFacture.ParNoKP <> ThisFacture.NoKP Then KPName = GetKPName(ThisFacture.ParNoKP) & " (" & ThisFacture.ParNoKP & ")"
                    Me.Payeurs.KP = KPName
                Else
                    Me.Payeurs.KP = "Aucun"
                End If
                If ThisFacture.ParNoUser > 0 Then
                    Me.Payeurs.User = UserManager.getInstance.getUser(ThisFacture.ParNoUser).ToString()
                Else
                    Me.Payeurs.User = "Aucun"
                End If

                Me.Payeurs.LinkedFacture = ThisFacture

                'Factures liés
                If ThisFacture.NoFactureTransfere <> 0 Then
                    _IsLinked = True
                End If
                If ThisFacture.NoFactureRef <> "" Then
                    _IsUnified = True
                    Me.Payeurs.NoFactureRefStr = ThisFacture.GetAllFacturesLinked(True)
                Else
                    Me.Payeurs.NoFactureRefStr = "Aucune"
                End If

                'Chargement des historiques
                Dim PaymentType, Verbe, MyComment, RecuString, RecusString, NoFactureString, CurFactureString As String
                Dim LastNoFacture As Integer
                Dim CurrentFactureShowed As Double

                .HistoFactures.Cls()
                .HistoFactures.Config(0)
                If HFactures.StatDataSet Is Nothing Then GoTo SkipLoadingFactureHisto
                With HFactures.StatDataSet.Tables("Table")
                    For j = FirstRowForFacture To .Rows.Count - 1
                        MyComment = ""
                        'Try
                        With .Rows(j)
                            If Integer.Parse(.Item("NoFacture")) <> MyNoFacture And Not IsUnified Then
                                If HistoFactureLines <> 0 Then HistoFactures.Remove(HistoFactures.Maximum)
                                StartingIndexFacture = j
                                Exit For
                            ElseIf Integer.Parse(.Item("NoFacture")) <> LastNoFacture And Me.IsUnified Then
                                If PreferencesManager.getGeneralPreferences()("MontantFactureHistoIsCumulative") = False Then CurrentFactureShowed = 0
                            End If

                            Dim curMF As Double = Double.Parse(.Item("MontantFacture").ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                            LastNoFacture = Integer.Parse(.Item("NoFacture"))
                            CurrentFacture += curMF
                            CurrentFactureShowed += curMF
                            CurrentFactureShowed = RoundAmount(CurrentFactureShowed)
                            CurrentFacture = RoundAmount(CurrentFacture)
                            If .Item("NoEntitePayeur") = 1 Then CurrentFactureClient += curMF
                            If .Item("NoEntitePayeur") = 2 Then CurrentFactureKP += curMF
                            If .Item("NoEntitePayeur") = 3 Then CurrentFactureUser += curMF
                            If .Item("NoEntitePayeur") = 4 Then CurrentFactureClinique += curMF

                            If Me.IsUnified Then
                                NoFactureString = EnterChar & "N° de la facture : " & .Item("NoFacture").ToString
                                If PreferencesManager.getGeneralPreferences()("MontantFactureHistoIsCumulative") = True Then
                                    CurFactureString = CurrentFactureShowed
                                Else
                                    CurFactureString = curMF
                                End If
                            Else
                                NoFactureString = ""
                                If PreferencesManager.getGeneralPreferences()("MontantFactureHistoIsCumulative") = True Then
                                    CurFactureString = CurrentFacture
                                Else
                                    CurFactureString = curMF
                                End If
                            End If

                            If .Item("Commentaires").ToString <> "" Then MyComment = EnterChar & .Item("Commentaires").ToString

                            If .Item("NoFactureRef").ToString.ToUpper = "" Then
                                newHistoLine = "(" & Format(.Item("DateHeureCreation"), "yyyy-MM-dd") & ") " & CurFactureString & "$ confirmé par " & .Item("FullName").ToString & NoFactureString & MyComment

                                If newHistoLine <> lastHistoLine Then
                                    lastHistoLine = newHistoLine
                                    HistoFactures.Add(newHistoLine)
                                    HistoFactureLines += 1
                                    If j <> (.Table.Rows.Count - 1) Then
                                        Dim MyLine As Short = HistoFactures.Add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                                        HistoFactures.ItemFont(MyLine) = New Font(HistoFactures.ItemFont(MyLine).FontFamily, 3, HistoFactures.ItemFont(MyLine).Style)
                                        HistoFactures.MyList.ItemClickable(MyLine) = False
                                    End If
                                End If
                            Else
                                HistoFactures.Remove(HistoFactures.Maximum)
                            End If
                        End With
                        'Catch
                        'End Try
                    Next j
                End With
                .HistoFactures.Reverse()
SkipLoadingFactureHisto:
                .HistoFactures.Draw = True : .HistoFactures.Draw = False

                .HistoPaiements.Cls()
                .HistoPaiements.Config(0)
                RecusString = ""
                lastHistoLine = ""

                If IsNothing(HPaiements.StatDataSet) = False Then
                    If IsNothing(HPaiements.StatDataSet.Tables("Table")) = False Then
                        With HPaiements.StatDataSet.Tables("Table")
                            For j = FirstRowForPaiement To .Rows.Count - 1
                                MyComment = "" : PaymentType = "" : DedicatedString = EnterChar
                                With .Rows(j)
                                    If Integer.Parse(.Item("NoFacture")) <> MyNoFacture And Not IsUnified Then
                                        If HistoPaiementLines <> 0 Then HistoPaiements.Remove(HistoPaiements.Maximum)
                                        StartingIndexPaiement = j
                                        Exit For
                                    End If

                                    If .Item("Commentaires").ToString <> "" Then MyComment = EnterChar & .Item("Commentaires").ToString
                                    If .Item("NoAction") <> 12 Then
                                        Verbe = "ajusté"
                                        DedicatedString &= "Ajusté pour "
                                    Else
                                        Verbe = "reçu"
                                        PaymentType = EnterChar & "Méthode de paiement : " & .Item("TypePaiement").ToString
                                        DedicatedString &= "Payé par "
                                    End If

                                    Dim curMP As Double = Double.Parse(.Item("MontantPaiement").ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                                    Select Case .Item("NoEntitePayeur")
                                        Case 1
                                            TempDedicated = DedicatedType.Client
                                            DedicatedString &= "le client."
                                            CurrentPaiementClient += curMP
                                        Case 3
                                            TempDedicated = DedicatedType.User
                                            DedicatedString &= "l'utilisateur."
                                            TempDedicated = DedicatedType.User
                                            CurrentPaiementUser += curMP
                                        Case 2
                                            TempDedicated = DedicatedType.KP
                                            DedicatedString &= "la personne / organisme clé."
                                            TempDedicated = DedicatedType.KP
                                            CurrentPaiementKP += curMP
                                        Case 4
                                            TempDedicated = DedicatedType.Clinique
                                            DedicatedString &= "la clinique."
                                            CurrentPaiementClinique += curMP
                                    End Select

                                    RecuString = ""
                                    If .Item("NoRecu").ToString <> "" Then
                                        If RecusString.IndexOf("," & .Item("NoRecu").ToString) < 0 Then RecusString &= "," & .Item("NoRecu")
                                        RecuString = EnterChar & "N° du reçu : " & .Item("NoRecu")
                                    End If

                                    If IsUnified Then
                                        NoFactureString = EnterChar & "N° de la facture : " & .Item("NoFacture").ToString
                                    Else
                                        NoFactureString = ""
                                    End If

                                    If TempDedicated = DedicatedTo Or DedicatedTo = DedicatedType.All Then
                                        If DedicatedTo <> DedicatedType.All Then DedicatedString = ""
                                        Dim MyMontantPaiement As String = .Item("MontantPaiement")
                                        ForceManaging(MyMontantPaiement, True, "", False, True, False, True, ",§.§-", , , , , , , 2, True)

                                        newHistoLine = "(" & DateFormat.AffTextDate(Date.Parse(.Item("DateHeureCreation")), DateFormat.TextDateOptions.YYYYMMDD) & ") " & MyMontantPaiement & "$ " & Verbe & " par " & .Item("FullName").ToString & PaymentType & DedicatedString & RecuString & NoFactureString & MyComment

                                        If lastHistoLine <> newHistoLine Then
                                            lastHistoLine = newHistoLine
                                            HistoPaiements.Add(newHistoLine)
                                            HistoPaiementLines += 1
                                            If j <> (.Table.Rows.Count - 1) Then
                                                Dim MyLine As Short = HistoPaiements.Add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                                                HistoPaiements.ItemFont(MyLine) = New Font(HistoPaiements.ItemFont(MyLine).FontFamily, 3, HistoPaiements.ItemFont(MyLine).Style)
                                                HistoPaiements.MyList.ItemClickable(MyLine) = False
                                            End If
                                        End If
                                    End If
                                End With
                            Next j
                        End With
                    End If
                End If
                .HistoPaiements.Reverse()
                .HistoPaiements.Draw = True : .HistoPaiements.Draw = False

                .Facture.Text = .CurrentFacture
                If Not IsUnified Then SetPaiementNRestant(True)

                'Reçu(s) string preloading
                Me.ThisFacture.NoRecusReadDB = False
                Me.ThisFacture.NoRecusReadReal = False
                Me.ThisFacture.GetNoRecusString(True)
            End With

            'Affichage Pourcentages
            If CurrentFacture > 0 Then
                Me.Payeurs.PourcentParNoClient = Math.Round(ThisFacture.GetPourcentClient() * 100, 4)
                Me.Payeurs.PourcentParNoKP = Math.Round(ThisFacture.GetPourcentKP() * 100, 4)
                Me.Payeurs.PourcentParNoUser = Math.Round(ThisFacture.GetPourcentUser() * 100, 4)
            Else
                Me.Payeurs.PourcentParNoClient = 0
                Me.Payeurs.PourcentParNoKP = 0
                Me.Payeurs.PourcentParNoUser = 0
            End If

            'Ajustement pour une facture unifiée
            If IsUnified Then
                StartingIndexFacture += 1
                Facture.Text = CurrentFacture
                Paiement.Text = CurrentPaiement
                Restant.Text = RoundAmount(CurrentFacture - CurrentPaiement)
                Payeurs.MontantPaiementClient = CurrentPaiementClient
                Payeurs.MontantPaiementKP = CurrentPaiementKP
                Payeurs.MontantPaiementUser = CurrentPaiementUser
                Payeurs.MontantPaiementClinique = CurrentPaiementClinique
            End If
        End If

        Me.Description.Text = ThisFacture.Description.Replace("\n", EnterChar)

        HFactures = Nothing
        HPaiements = Nothing

        If Me.CurFacture.Taxe1 = -1 Then
            Me.SelectF.Enabled = False
            Me.SelectP.Checked = True
        Else
            Me.SelectF.Enabled = True
        End If

        MyBase.Invalidate(True)

        If CurrentUserName = "Administrateur" Then
            LoadingTime = DateTime.Now.Ticks - LoadingTime
            LoadingTime /= 10000000 'To get in seconds
            Me.Description.Text = "Time to load -> " & LoadingTime & "s"
        End If

        Return True
    End Function

    Private Sub SetPaiementNRestant(Optional ByVal MinimumCurrent As Boolean = False)
        With Me
            Select Case DedicatedTo
                Case DedicatedType.All
                    If MinimumCurrent And (.CurrentPaiementClient + .CurrentPaiementKP + .CurrentPaiementUser + .CurrentPaiementClinique) > .Paiement.Text Then .Paiement.Text = .CurrentPaiementClient + .CurrentPaiementKP + .CurrentPaiementUser + .CurrentPaiementClinique
                    .TempMontantFacture.Text = Facture.Text
                Case DedicatedType.Client
                    If MinimumCurrent And .CurrentPaiementClient > .Paiement.Text Then .Paiement.Text = .CurrentPaiementClient
                    .TempMontantFacture.Text = (Facture.Text * ThisFacture.GetPourcentClient)
                Case DedicatedType.KP
                    If MinimumCurrent And .CurrentPaiementKP > .Paiement.Text Then .Paiement.Text = .CurrentPaiementKP
                    .TempMontantFacture.Text = (Facture.Text * ThisFacture.GetPourcentKP)
                Case DedicatedType.User
                    If MinimumCurrent And .CurrentPaiementUser > .Paiement.Text Then .Paiement.Text = .CurrentPaiementUser
                    .TempMontantFacture.Text = (Facture.Text * ThisFacture.GetPourcentUser)
                Case DedicatedType.Clinique
                    If MinimumCurrent And .CurrentPaiementClinique > .Paiement.Text Then .Paiement.Text = .CurrentPaiementClinique
                    .TempMontantFacture.Text = Facture.Text * ThisFacture.GetPourcentClinique
            End Select
            If (TempMontantFacture.Text - .Paiement.Text) < 0 And Not IsUnified Then
                .Restant.Text = 0
                .Paiement.Text = .TempMontantFacture.Text
            Else
                .Restant.Text = RoundAmount(.TempMontantFacture.Text - .Paiement.Text)
            End If

            If .Payeurs IsNot Nothing Then
                Select Case DedicatedTo
                    Case DedicatedType.All
                        .Payeurs.MontantPaiementClient = CurrentPaiementClient
                        .Payeurs.MontantPaiementKP = CurrentPaiementKP
                        .Payeurs.MontantPaiementUser = CurrentPaiementUser
                        .Payeurs.MontantPaiementClinique = CurrentPaiementClinique
                    Case DedicatedType.Client
                        .Payeurs.MontantPaiementClient = .Paiement.Text
                        .Payeurs.MontantPaiementKP = CurrentPaiementKP
                        .Payeurs.MontantPaiementUser = CurrentPaiementUser
                        .Payeurs.MontantPaiementClinique = CurrentPaiementClinique
                    Case DedicatedType.KP
                        .Payeurs.MontantPaiementClient = CurrentPaiementClient
                        .Payeurs.MontantPaiementKP = .Paiement.Text
                        .Payeurs.MontantPaiementUser = CurrentPaiementUser
                        .Payeurs.MontantPaiementClinique = CurrentPaiementClinique
                    Case DedicatedType.User
                        .Payeurs.MontantPaiementClient = .Paiement.Text
                        .Payeurs.MontantPaiementKP = CurrentPaiementKP
                        .Payeurs.MontantPaiementUser = CurrentPaiementUser
                        .Payeurs.MontantPaiementClinique = CurrentPaiementClinique
                    Case DedicatedType.Clinique
                        .Payeurs.MontantPaiementClient = CurrentPaiementClient
                        .Payeurs.MontantPaiementKP = CurrentPaiementKP
                        .Payeurs.MontantPaiementUser = CurrentPaiementUser
                        .Payeurs.MontantPaiementClinique = .Paiement.Text
                End Select
            End If
        End With
    End Sub

    Private Sub FacturationBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Width = 536
        Me.Height = 130
        Me.Groupe.Size = Me.Size
    End Sub

    Private Sub Selecting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectF.CheckedChanged, SelectP.CheckedChanged
        Paiement.Enabled = Not SelectF.Checked
        Facture.Enabled = SelectF.Checked
        Facture.Text = CurrentFacture
        Paiement.Text = CurrentPaiement
        SetPaiementNRestant()
    End Sub

    Private Sub Adjusting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Adjusting.Click
        Dim newAmountPaiement As Double = Paiement.Text.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        Dim newAmountFacture As Double = Facture.Text.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        If CurrentPaiement = newAmountPaiement And CurrentFacture = newAmountFacture Then MsgBox("Vous devez absolument modifier un des montants pour ajuster la facture.", , "Impossible d'ajuster") : Exit Sub

        'Commentaires
        Dim MyInputBoxPlus As New InputBoxPlus()
        MyInputBoxPlus.FirstLetterCapital = True
        Dim MyComment As String = MyInputBoxPlus("Veuillez entrer le commentaire de l'ajustement", "Commentaire")
        If PreferencesManager.getGeneralPreferences()("AdjustingCommentsForced") = True And MyComment = "" Then
            MessageBox.Show("Le commentaire est obligatoire.", "Champ obligatoire")
            Exit Sub
        End If

        Adjusting.Enabled = False

        Dim SelfOpened As Boolean = False
        Dim MyMontantFacture As Double = -1
        Dim MyMontantPaiement As Double = -1
        If DBLinker.GetInstance().DBConnected = False Then DBLinker.GetInstance().DBConnected = True : SelfOpened = True

        Dim MyFacture As Facture = New Facture(Me.NoFacture)

        With MyFacture
            If SelectF.Checked = True Then
                If CurrentFacture = newAmountFacture Then GoTo fin
                'Ajustement de la facture
                MyMontantFacture = newAmountFacture
            Else
                If CurrentPaiement = newAmountPaiement Then GoTo fin
                'Ajustement du paiement
                MyMontantPaiement = newAmountPaiement
            End If

            Dim statDate As Date = LimitDate
            If CurrentDroitAcces(90) = True Then 'Si l'utilisateur a le droit de choisir la date d'ajustement
                Dim MyDateChoice As New DateChoice
                Dim MyStatDate As String = MyDateChoice.Choisir(Date.Now.Year - 2, Date.Now.Year, , , , , , , , , , , Date.Today)
                If MyStatDate <> "" Then
                    Dim SDate() As String = MyStatDate.Split(New Char() {"/"})
                    statDate = CDate(SDate(2) & "/" & SDate(1) & "/" & SDate(0))
                End If
            End If

            Dim MyFactureAction As FactureAction
            If DedicatedTo = DedicatedType.All And SelectF.Checked = False Then
                Dim RejectedFilters As New ArrayList
                Dim Filter As String = .GetFilterPayeur(, "pour qui ajusté le montant du paiement")
                RejectedFilters.Add(Filter)
                Dim DiffP As Double = MyMontantPaiement - Me.CurrentPaiement
                Dim Restant As Double = 0

                Select Case Filter
                    Case "C" 'Client
                        If (CurrentPaiementClient + DiffP) > (CurrentFacture * .GetPourcentClient) Then
                            MyMontantPaiement = (CurrentFacture * .GetPourcentClient)
                            Restant = DiffP - (MyMontantPaiement - CurrentPaiementClient)
                        ElseIf (CurrentPaiementClient + DiffP) < 0 Then
                            MyMontantPaiement = 0
                            Restant = DiffP - (MyMontantPaiement - CurrentPaiementClient)
                        Else
                            MyMontantPaiement = CurrentPaiementClient + DiffP
                        End If
                    Case "K" 'Personne / organisme clé
                        If (CurrentPaiementKP + DiffP) > (CurrentFacture * .GetPourcentKP) Then
                            MyMontantPaiement = (CurrentFacture * .GetPourcentKP)
                            Restant = DiffP - (MyMontantPaiement - CurrentPaiementKP)
                        ElseIf (CurrentPaiementKP + DiffP) < 0 Then
                            MyMontantPaiement = 0
                            Restant = DiffP - (MyMontantPaiement - CurrentPaiementKP)
                        Else
                            MyMontantPaiement = CurrentPaiementKP + DiffP
                        End If
                    Case "U" 'Utilisateur
                        If (CurrentPaiementUser + DiffP) > (CurrentFacture * .GetPourcentUser) Then
                            MyMontantPaiement = (CurrentFacture * .GetPourcentUser)
                            Restant = DiffP - (MyMontantPaiement - CurrentPaiementUser)
                        ElseIf (CurrentPaiementUser + DiffP) < 0 Then
                            MyMontantPaiement = 0
                            Restant = DiffP - (MyMontantPaiement - CurrentPaiementUser)
                        Else
                            MyMontantPaiement = CurrentPaiementUser + DiffP
                        End If
                    Case "" 'Clinique
                        If (CurrentPaiementClinique + DiffP) > CurrentFacture Then
                            MyMontantPaiement = CurrentFacture
                            Restant = 0
                        Else
                            MyMontantPaiement = CurrentPaiementClinique + DiffP
                        End If
                End Select

                MyFactureAction = AdjustFacture(Me.NoFacture, MyMontantFacture, MyMontantPaiement, , , MyComment, , , Filter, statDate)

                If .NbPayeurs > 1 And Restant <> 0 Then
                    Filter = .GetFilterPayeur(, "pour qui ajusté le montant du paiement", RejectedFilters)
                    RejectedFilters.Add(Filter)
                    Select Case Filter
                        Case "C" 'Client
                            If (CurrentPaiementClient + Restant) > (CurrentFacture * .GetPourcentClient) Then
                                MyMontantPaiement = (CurrentFacture * .GetPourcentClient)
                                Restant -= (MyMontantPaiement - CurrentPaiementClient)
                            ElseIf (CurrentPaiementClient + Restant) < 0 Then
                                MyMontantPaiement = 0
                                Restant -= (MyMontantPaiement - CurrentPaiementClient)
                            Else
                                MyMontantPaiement = CurrentPaiementClient + Restant
                            End If
                        Case "K" 'Personne / organisme clé
                            If (CurrentPaiementKP + Restant) > (CurrentFacture * .GetPourcentKP) Then
                                MyMontantPaiement = (CurrentFacture * .GetPourcentKP)
                                Restant = Restant - (MyMontantPaiement - CurrentPaiementKP)
                            ElseIf (CurrentPaiementKP + Restant) < 0 Then
                                MyMontantPaiement = 0
                                Restant = Restant - (MyMontantPaiement - CurrentPaiementKP)
                            Else
                                MyMontantPaiement = CurrentPaiementKP + Restant
                            End If
                        Case "U" 'Utilisateur
                            If (CurrentPaiementUser + Restant) > (CurrentFacture * .GetPourcentUser) Then
                                MyMontantPaiement = (CurrentFacture * .GetPourcentUser)
                                Restant = Restant - (MyMontantPaiement - CurrentPaiementUser)
                            ElseIf (CurrentPaiementUser + Restant) < 0 Then
                                MyMontantPaiement = 0
                                Restant = Restant - (MyMontantPaiement - CurrentPaiementUser)
                            Else
                                MyMontantPaiement = CurrentPaiementUser + Restant
                            End If
                    End Select

                    MyFactureAction = AdjustFacture(Me.NoFacture, MyMontantFacture, MyMontantPaiement, , , MyComment, , , Filter, statDate)

                    If .NbPayeurs > 2 And Restant <> 0 Then
                        Filter = .GetFilterPayeur(, "pour qui ajusté le montant du paiement", RejectedFilters)
                        Select Case Filter
                            Case "C" 'Client
                                If (CurrentPaiementClient + Restant) > (CurrentFacture * .GetPourcentClient) Then
                                    MyMontantPaiement = (CurrentFacture * .GetPourcentClient)
                                ElseIf (CurrentPaiementClient + Restant) < 0 Then
                                    MyMontantPaiement = 0
                                Else
                                    MyMontantPaiement = CurrentPaiementClient + Restant
                                End If
                            Case "K" 'Personne / organisme clé
                                If (CurrentPaiementKP + Restant) > (CurrentFacture * .GetPourcentKP) Then
                                    MyMontantPaiement = (CurrentFacture * .GetPourcentKP)
                                ElseIf (CurrentPaiementKP + Restant) < 0 Then
                                    MyMontantPaiement = 0
                                Else
                                    MyMontantPaiement = CurrentPaiementKP + Restant
                                End If
                            Case "U" 'Utilisateur
                                If (CurrentPaiementUser + Restant) > (CurrentFacture * .GetPourcentUser) Then
                                    MyMontantPaiement = (CurrentFacture * .GetPourcentUser)
                                ElseIf (CurrentPaiementUser + Restant) < 0 Then
                                    MyMontantPaiement = 0
                                Else
                                    MyMontantPaiement = CurrentPaiementUser + Restant
                                End If
                        End Select

                        MyFactureAction = AdjustFacture(Me.NoFacture, MyMontantFacture, MyMontantPaiement, , , MyComment, , , Filter, statDate)
                    End If
                End If
            Else
                Dim EntitePayeur As String = DedicatedTo.ToString.Substring(0, 1).ToUpper
                If .ParNoClient = 0 And .ParNoKP = 0 And .ParNoUser = 0 Then EntitePayeur = ""
                MyFactureAction = AdjustFacture(Me.NoFacture, MyMontantFacture, MyMontantPaiement, , , MyComment, , , EntitePayeur, statDate)
            End If
            Select Case MyFactureAction
                Case CommonProc.FactureAction.Created
                    RaiseEvent CreatedFacturation(Me)
                    MyMainWin.StatusText = "Facture #" & Me.NoFacture & " ajusté. " & Facture.Text & "$ facturé, " & Paiement.Text & "$ payé."
                Case CommonProc.FactureAction.Adjusted Or CommonProc.FactureAction.Deleted
                    MyMainWin.StatusText = "Facture #" & Me.NoFacture & " ajusté. " & Facture.Text & "$ facturé, " & Paiement.Text & "$ payé."
                Case CommonProc.FactureAction.Err
                    MessageBox.Show("Le montant facturé doit absolument être supérieur au montant payé et les pourcentages doivent égalés 100%.", "Impossible d'ajuster la facture")
                    GoTo fin
            End Select

            If .NoVisite > 0 Then UpdateVisites(.NoClient, .NoFolder, .DateFacture, , False)
            If (DedicatedTo = DedicatedType.All Or DedicatedTo = DedicatedType.Client) And (.ParNoClient > 0 Or .NoClient > 0) Then
                InternalUpdatesManager.getInstance.SendUpdate("Paiements(" & .ParNoClient & ",-1)")
                InternalUpdatesManager.getInstance.SendUpdate("AccountsBills(" & .ParNoClient & ")")
            End If
            If (DedicatedTo = DedicatedType.All Or DedicatedTo = DedicatedType.KP) And (.ParNoKP > 0 Or .NoKP > 0) Then
                InternalUpdatesManager.GetInstance.SendUpdate("PaiementsKP(" & .ParNoKP & ",-1)")
                InternalUpdatesManager.GetInstance.SendUpdate("AccountsBillsKP(" & .ParNoKP & ")")
            End If
            If .NoVisite > 0 Then
                Dim getNoTrp() As String = DBLinker.GetInstance.ReadOneDBField("InfoVisites", "NoTRP", "WHERE NoVisite=" & .NoVisite)
                UpdateVisites(.NoClient, .NoFolder, .DateFacture, , False, getNoTrp(0))
            End If

            'Ask if the user want to generate bill receipt on positive payment adjustment
            If (newAmountPaiement - .MontantPaiement) > 0 AndAlso MessageBox.Show("Voulez-vous générer de reçu de cet ajustement ?", "Demande de création de reçu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then .GenerateRecu(.GetFilterPayeur())
        End With

        CurrentPaiement = newAmountPaiement
        CurrentFacture = newAmountFacture
        Loading(Me.NoFacture)

fin:
        If SelfOpened = True Then DBLinker.GetInstance().DBConnected = False

        Adjusting.Enabled = True
        If SelectF.Checked = True Then
            Facture.Focus()
            Facture.SelectAll()
        Else
            Paiement.Focus()
            Paiement.SelectAll()
        End If

    End Sub

    Private Sub Paiement_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Paiement.Validating
        Try
            If Me.DesignMode = True Then Exit Sub
            If CDbl(Paiement.Text) > CDbl(CurrentFacture) Then Paiement.Text = CurrentFacture
            MontantPaiement(False) = Paiement.Text
            SetPaiementNRestant()
        Catch 'REM Exception not handle
        End Try
    End Sub

    Private Sub Facture_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Facture.Validating
        Try
            If Me.DesignMode = True Then Exit Sub
            Dim MinimumAmount As Double
            If ThisFacture.GetPourcentClient > 0 Then MinimumAmount = CurrentPaiementClient / (ThisFacture.GetPourcentClient)
            If ThisFacture.GetPourcentKP > 0 Then MinimumAmount = Math.Max(MinimumAmount, CurrentPaiementKP / (ThisFacture.GetPourcentKP))
            If ThisFacture.GetPourcentUser > 0 Then MinimumAmount = Math.Max(MinimumAmount, CurrentPaiementUser / (ThisFacture.GetPourcentUser))
            If ThisFacture.GetPourcentClinique > 0 Then MinimumAmount = Math.Max(MinimumAmount, CurrentPaiementClinique / (ThisFacture.GetPourcentClinique))

            If Facture.Text < MinimumAmount Then Facture.Text = MinimumAmount
            SetPaiementNRestant()
        Catch 'REM Exception not handle
        End Try
    End Sub

    Private Sub Groupe_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Groupe.Paint
        Dim NormalFont As New Font("Microsoft Sans Sherif", 8.25!, FontStyle.Regular)
        With Me.CurFacture
            'Draw DateFacture
            e.Graphics.DrawString(DateFormat.AffTextDate(.DateFacture), NormalFont, Brushes.Black, 472, 24)
            e.Graphics.DrawString("Date de facturation :", NormalFont, Brushes.Black, 367, 24)
        End With

        'Draw general strings
        e.Graphics.DrawString("# facture :", NormalFont, Brushes.Black, 8, 48)
        e.Graphics.DrawString(Me.NoFacture, NormalFont, Brushes.Black, 60, 48)
        e.Graphics.DrawString("Montant à payer :", NormalFont, Brushes.Black, 368, 72)
        e.Graphics.DrawString("$", NormalFont, Brushes.Black, 520, 72)
        e.Graphics.DrawString("$", NormalFont, Brushes.Black, 336, 72)
        e.Graphics.DrawString("$", NormalFont, Brushes.Black, 160, 72)
        e.Graphics.DrawString("Description :", NormalFont, Brushes.Black, 8, 96)
        NormalFont.Dispose()
    End Sub

    Private Sub Description_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Description.Click
        ResizingDescription = True

        Description.Top = 26
        Description.Height = 90
        Description.ScrollBars = ScrollBars.Vertical
        If Me.Locked = False Then
            saveDesc.Visible = True
            cancelDesc.Visible = True
        End If

        ResizingDescription = False
    End Sub

    Private ResizingDescription As Boolean = False

    Private Sub Description_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Description.Leave
        'Dim a As Boolean = saveDesc.Focused
        If Description.Height <> 20 And ResizingDescription = False And saveDesc.Focused = False And cancelDesc.Focused = False Then
            HideDescription()
        End If
    End Sub

    Private Sub HideDescription()
        Description.Top = 96
        Description.Height = 20
        Description.ScrollBars = ScrollBars.None
        saveDesc.Visible = False
        cancelDesc.Visible = False
    End Sub

    Private Sub saveDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveDesc.Click
        CurFacture.Description = Description.Text
        CurFacture.SaveDescription()
        HideDescription()
    End Sub

    Private Sub cancelDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelDesc.Click
        Description.Text = CurFacture.Description
        HideDescription()
    End Sub
End Class
