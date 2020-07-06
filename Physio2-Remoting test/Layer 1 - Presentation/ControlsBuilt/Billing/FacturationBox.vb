Public Class FacturationBox
    Inherits System.Windows.Forms.UserControl

    Private WithEvents payeurs As FacturationBoxMoreInfos

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal clientLocked As Boolean, ByVal kpLocked As Boolean, ByVal userLocked As Boolean, ByVal billDedicatedTo As DedicatedType)
        MyBase.New()

        dedicatedTo = billDedicatedTo

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        '
        'Payeurs
        '
        payeurs = New FacturationBoxMoreInfos(clientLocked, kpLocked, userLocked)
        payeurs.Visible = True
        payeurs.startingPosition = (Me.groupe.DisplayRectangle.Height - 10)
        payeurs.endingPosition = Me.groupe.DisplayRectangle.Height - payeurs.Height
        payeurs.movingSpeed = 5
        payeurs.Top = Me.groupe.DisplayRectangle.Height - 10
        payeurs.Left = 0
        payeurs.scrollOrientation = Legend.LegendeOrientation.ScrollUp
        Me.groupe.Controls.Add(payeurs)
        payeurs.BringToFront()

        Me.saveDesc.BackgroundImage = DrawingManager.getInstance.getImage("save.jpg")
        Me.cancelDesc.BackgroundImage = DrawingManager.getInstance.getImage("annuler16.gif")

        Me.tempMontantFacture.SendToBack()
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            If Not payeurs Is Nothing Then payeurs.Dispose()
            _ValueA = Nothing
            _ValueB = Nothing
            thisFacture.dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents groupe As System.Windows.Forms.Panel
    Private WithEvents facture As ManagedText
    Private WithEvents paiement As ManagedText
    Private WithEvents restant As ManagedText
    Private WithEvents histoPaiements As Clinica.ListCombo
    Private WithEvents histoFactures As Clinica.ListCombo
    Private WithEvents description As ManagedText
    Private WithEvents selectF As System.Windows.Forms.RadioButton
    Private WithEvents selectP As System.Windows.Forms.RadioButton
    Private WithEvents adjusting As System.Windows.Forms.Button
    Private WithEvents saveDesc As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents cancelDesc As System.Windows.Forms.Button
    Friend WithEvents lblTypeFacture As System.Windows.Forms.Label

    Private WithEvents tempMontantFacture As ManagedText

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FacturationBox))
        Me.groupe = New System.Windows.Forms.Panel
        Me.lblTypeFacture = New System.Windows.Forms.Label
        Me.histoPaiements = New Clinica.ListCombo
        Me.histoFactures = New Clinica.ListCombo
        Me.saveDesc = New System.Windows.Forms.Button
        Me.cancelDesc = New System.Windows.Forms.Button
        Me.adjusting = New System.Windows.Forms.Button
        Me.description = New ManagedText
        Me.paiement = New ManagedText
        Me.selectP = New System.Windows.Forms.RadioButton
        Me.facture = New ManagedText
        Me.selectF = New System.Windows.Forms.RadioButton
        Me.restant = New ManagedText
        Me.tempMontantFacture = New ManagedText
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.groupe.SuspendLayout()
        Me.SuspendLayout()
        '
        'Groupe
        '
        Me.groupe.AutoScrollMargin = New System.Drawing.Size(5, 0)
        Me.groupe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.groupe.Controls.Add(Me.histoPaiements)
        Me.groupe.Controls.Add(Me.histoFactures)
        Me.groupe.Controls.Add(Me.saveDesc)
        Me.groupe.Controls.Add(Me.cancelDesc)
        Me.groupe.Controls.Add(Me.adjusting)
        Me.groupe.Controls.Add(Me.description)
        Me.groupe.Controls.Add(Me.paiement)
        Me.groupe.Controls.Add(Me.selectP)
        Me.groupe.Controls.Add(Me.facture)
        Me.groupe.Controls.Add(Me.selectF)
        Me.groupe.Controls.Add(Me.restant)
        Me.groupe.Controls.Add(Me.lblTypeFacture)
        Me.groupe.Location = New System.Drawing.Point(0, 0)
        Me.groupe.Name = "Groupe"
        Me.groupe.Size = New System.Drawing.Size(536, 120)
        Me.groupe.TabIndex = 2
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
        Me.histoPaiements.Text = "Historique des paiements / ajustements"
        Me.histoPaiements.clickEnabled = True
        Me.histoPaiements.draw = False
        Me.histoPaiements.dropDownHeight = 98
        Me.histoPaiements.droppedDown = False
        Me.histoPaiements.hsValue = CType(0, Short)
        Me.histoPaiements.Location = New System.Drawing.Point(267, 0)
        Me.histoPaiements.Name = "HistoPaiements"
        Me.histoPaiements.selected = CType(-1, Short)
        Me.histoPaiements.Size = New System.Drawing.Size(267, 20)
        Me.histoPaiements.sorted = False
        Me.histoPaiements.TabIndex = 20
        Me.histoPaiements.vsValue = CType(0, Short)
        '
        'HistoFactures
        '
        Me.histoFactures.Text = "Historique de la facture"
        Me.histoFactures.clickEnabled = True
        Me.histoFactures.draw = False
        Me.histoFactures.dropDownHeight = 98
        Me.histoFactures.droppedDown = False
        Me.histoFactures.hsValue = CType(0, Short)
        Me.histoFactures.Location = New System.Drawing.Point(0, 0)
        Me.histoFactures.Name = "HistoFactures"
        Me.histoFactures.selected = CType(-1, Short)
        Me.histoFactures.Size = New System.Drawing.Size(267, 20)
        Me.histoFactures.sorted = False
        Me.histoFactures.TabIndex = 19
        Me.histoFactures.vsValue = CType(0, Short)
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
        Me.adjusting.Location = New System.Drawing.Point(464, 96)
        Me.adjusting.Name = "Adjusting"
        Me.adjusting.Size = New System.Drawing.Size(64, 20)
        Me.adjusting.TabIndex = 27
        Me.adjusting.Text = "Ajuster"
        '
        'Description
        '
        Me.description.acceptAlpha = True
        Me.description.acceptedChars = ""
        Me.description.acceptNumeric = True
        Me.description.allCapital = False
        Me.description.allLower = False
        Me.description.blockOnMaximum = False
        Me.description.blockOnMinimum = False
        Me.description.cb_AcceptLeftZeros = False
        Me.description.cb_AcceptNegative = False
        Me.description.currencyBox = False
        Me.description.firstLetterCapital = True
        Me.description.firstLettersCapital = False
        Me.description.Location = New System.Drawing.Point(88, 96)
        Me.description.manageText = True
        Me.description.matchExp = ""
        Me.description.maximum = 0
        Me.description.minimum = 0
        Me.description.Multiline = True
        Me.description.Name = "Description"
        Me.description.nbDecimals = CType(2, Short)
        Me.description.onlyAlphabet = False
        Me.description.refuseAccents = False
        Me.description.refusedChars = ""
        Me.description.showInternalContextMenu = True
        Me.description.Size = New System.Drawing.Size(368, 20)
        Me.description.TabIndex = 23
        Me.description.trimText = False
        '
        'Paiement
        '
        Me.paiement.acceptAlpha = False
        Me.paiement.acceptedChars = ",§."
        Me.paiement.acceptNumeric = True
        Me.paiement.allCapital = False
        Me.paiement.allLower = False
        Me.paiement.blockOnMaximum = False
        Me.paiement.blockOnMinimum = False
        Me.paiement.cb_AcceptLeftZeros = False
        Me.paiement.cb_AcceptNegative = False
        Me.paiement.currencyBox = True
        Me.paiement.firstLetterCapital = False
        Me.paiement.firstLettersCapital = False
        Me.paiement.Location = New System.Drawing.Point(288, 72)
        Me.paiement.manageText = True
        Me.paiement.matchExp = ""
        Me.paiement.maximum = 0
        Me.paiement.minimum = 0
        Me.paiement.Name = "Paiement"
        Me.paiement.nbDecimals = CType(2, Short)
        Me.paiement.onlyAlphabet = False
        Me.paiement.refuseAccents = False
        Me.paiement.refusedChars = ""
        Me.paiement.showInternalContextMenu = True
        Me.paiement.Size = New System.Drawing.Size(48, 20)
        Me.paiement.TabIndex = 4
        Me.paiement.Text = "0"
        Me.paiement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.paiement.trimText = False
        '
        'SelectP
        '
        Me.selectP.Checked = True
        Me.selectP.Location = New System.Drawing.Point(192, 72)
        Me.selectP.Name = "SelectP"
        Me.selectP.Size = New System.Drawing.Size(112, 16)
        Me.selectP.TabIndex = 25
        Me.selectP.TabStop = True
        Me.selectP.Text = "Montant payé :"
        '
        'Facture
        '
        Me.facture.acceptAlpha = False
        Me.facture.acceptedChars = ",§."
        Me.facture.acceptNumeric = True
        Me.facture.allCapital = False
        Me.facture.allLower = False
        Me.facture.blockOnMaximum = False
        Me.facture.blockOnMinimum = False
        Me.facture.cb_AcceptLeftZeros = False
        Me.facture.cb_AcceptNegative = False
        Me.facture.currencyBox = True
        Me.facture.firstLetterCapital = False
        Me.facture.firstLettersCapital = False
        Me.facture.Location = New System.Drawing.Point(112, 72)
        Me.facture.manageText = True
        Me.facture.matchExp = ""
        Me.facture.maximum = 0
        Me.facture.minimum = 0
        Me.facture.Name = "Facture"
        Me.facture.nbDecimals = CType(2, Short)
        Me.facture.onlyAlphabet = False
        Me.facture.refuseAccents = False
        Me.facture.refusedChars = ""
        Me.facture.showInternalContextMenu = True
        Me.facture.Size = New System.Drawing.Size(48, 20)
        Me.facture.TabIndex = 3
        Me.facture.Text = "0"
        Me.facture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.facture.trimText = False
        '
        'SelectF
        '
        Me.selectF.Location = New System.Drawing.Point(8, 72)
        Me.selectF.Name = "SelectF"
        Me.selectF.Size = New System.Drawing.Size(112, 16)
        Me.selectF.TabIndex = 24
        Me.selectF.Text = "Montant facturé :"
        '
        'Restant
        '
        Me.restant.acceptAlpha = False
        Me.restant.acceptedChars = ",§."
        Me.restant.acceptNumeric = True
        Me.restant.allCapital = False
        Me.restant.allLower = False
        Me.restant.BackColor = System.Drawing.SystemColors.ControlLight
        Me.restant.blockOnMaximum = False
        Me.restant.blockOnMinimum = False
        Me.restant.cb_AcceptLeftZeros = False
        Me.restant.cb_AcceptNegative = False
        Me.restant.currencyBox = True
        Me.restant.firstLetterCapital = False
        Me.restant.firstLettersCapital = False
        Me.restant.Location = New System.Drawing.Point(472, 72)
        Me.restant.manageText = True
        Me.restant.matchExp = ""
        Me.restant.maximum = 0
        Me.restant.minimum = 0
        Me.restant.Name = "Restant"
        Me.restant.nbDecimals = CType(2, Short)
        Me.restant.onlyAlphabet = False
        Me.restant.ReadOnly = True
        Me.restant.refuseAccents = False
        Me.restant.refusedChars = ""
        Me.restant.showInternalContextMenu = True
        Me.restant.Size = New System.Drawing.Size(48, 20)
        Me.restant.TabIndex = 9
        Me.restant.Text = "0"
        Me.restant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.restant.trimText = False
        '
        'TempMontantFacture
        '
        Me.tempMontantFacture.acceptAlpha = False
        Me.tempMontantFacture.acceptedChars = ",§."
        Me.tempMontantFacture.acceptNumeric = True
        Me.tempMontantFacture.allCapital = False
        Me.tempMontantFacture.allLower = False
        Me.tempMontantFacture.blockOnMaximum = False
        Me.tempMontantFacture.blockOnMinimum = False
        Me.tempMontantFacture.cb_AcceptLeftZeros = False
        Me.tempMontantFacture.cb_AcceptNegative = False
        Me.tempMontantFacture.currencyBox = True
        Me.tempMontantFacture.firstLetterCapital = False
        Me.tempMontantFacture.firstLettersCapital = False
        Me.tempMontantFacture.Location = New System.Drawing.Point(0, 0)
        Me.tempMontantFacture.manageText = True
        Me.tempMontantFacture.matchExp = ""
        Me.tempMontantFacture.maximum = 0
        Me.tempMontantFacture.minimum = 0
        Me.tempMontantFacture.Name = "TempMontantFacture"
        Me.tempMontantFacture.nbDecimals = CType(2, Short)
        Me.tempMontantFacture.onlyAlphabet = False
        Me.tempMontantFacture.refuseAccents = False
        Me.tempMontantFacture.refusedChars = ""
        Me.tempMontantFacture.showInternalContextMenu = True
        Me.tempMontantFacture.Size = New System.Drawing.Size(48, 20)
        Me.tempMontantFacture.TabIndex = 3
        Me.tempMontantFacture.Text = "0"
        Me.tempMontantFacture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tempMontantFacture.trimText = False
        '
        'FacturationBox
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.tempMontantFacture)
        Me.Controls.Add(Me.groupe)
        Me.Name = "FacturationBox"
        Me.Size = New System.Drawing.Size(536, 130)
        Me.groupe.ResumeLayout(False)
        Me.groupe.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _ValueA, _ValueB As Object
    Private _OldValue As Double
    Private MyPath, _nam As String
    Private _Locked As Boolean = False
    Private _NoFacture As Integer
    Private _IsUnified As Boolean = False
    Private _IsLinked As Boolean = False
    Private _EntiteType As DedicatedType
    Private _EntiteLie As Integer
    Private loadingTime As Double

    Private dedicatedTo As DedicatedType

    Private thisFacture As New Bill
    Private infoFolder As String = ""

    Public CurrentFacture, CurrentFactureClient, CurrentFactureKP, CurrentFactureUser, CurrentFactureClinique, CurrentPaiementClient, CurrentPaiementKP, CurrentPaiementUser, currentPaiementClinique As Double
    Public Event mpChanged(ByVal sender As Object, ByVal oldAmount As Double, ByVal newAmount As Double)
    Public Event upbChanged(ByVal sender As Object)
    Public Event createdFacturation(ByVal sender As Object)

    Public Enum DedicatedType
        Client = 0
        KP = 1
        User = 2
        All = 3
        Clinique = 4
    End Enum


#Region "Propriété"
    Public ReadOnly Property curFacture() As Bill
        Get
            Return thisFacture
        End Get
    End Property

    Public Property currentType() As DedicatedType
        Get
            Return dedicatedTo
        End Get
        Set(ByVal value As DedicatedType)
            dedicatedTo = value
        End Set
    End Property

    Public Property locked() As Boolean
        Get
            Return _Locked
        End Get
        Set(ByVal Value As Boolean)
            _Locked = Value

            If Me.isUnified Then Value = True
            payeurs.pourcentageLocked = True
            description.ReadOnly = Value
            facture.ReadOnly = Value
            If curFacture.getNoReceiptsString <> "" And currentDroitAcces(91) = False Then 'Si un reçu a été émis pour cette facture, bloque la modif du paiement
                paiement.ReadOnly = True
            Else
                paiement.ReadOnly = Value
            End If
            adjusting.Enabled = Not Value
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

    Public Property negativePaiement() As Boolean
        Get
            Return paiement.cb_AcceptNegative
        End Get
        Set(ByVal Value As Boolean)
            paiement.cb_AcceptNegative = Value
        End Set
    End Property

    Public Property readOnlyFacture() As Boolean
        Get
            Return facture.ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            facture.ReadOnly = Value
        End Set
    End Property

    Public Property MontantPaiement(Optional ByVal changeValue As Boolean = True) As Double
        Get
            Return paiement.Text
        End Get
        Set(ByVal Value As Double)
            RaiseEvent mpChanged(Me, _OldValue, Value)
            If changeValue = True Then paiement.Text = Value
            _OldValue = Value
        End Set
    End Property

    Public Property montantFacture() As Double
        Get
            Return facture.Text
        End Get
        Set(ByVal Value As Double)
            facture.Text = Value
        End Set
    End Property

    Public Property montantRestant() As Double
        Get
            Return restant.Text
        End Get
        Set(ByVal Value As Double)
            restant.Text = roundAmount(Value)
        End Set
    End Property

    Public Property setNom() As String
        Get
            Return curFacture.type
        End Get
        Set(ByVal Value As String)
            curFacture.type = Value
            Me.Invalidate(True)
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

    Public ReadOnly Property isUnified() As Boolean
        Get
            Return curFacture.noBillRef <> ""
        End Get
    End Property

    Public ReadOnly Property isLinked() As Boolean
        Get
            Return (curFacture.noBillTransfered <> 0)
        End Get
    End Property

    Public ReadOnly Property entiteLie() As Integer
        Get
            Return _EntiteLie
        End Get
    End Property

    Public ReadOnly Property entiteType() As DedicatedType
        Get
            Return _EntiteType
        End Get
    End Property

    Private Property currentPaiement() As Double
        Get
            Select Case dedicatedTo
                Case DedicatedType.All
                    Return (CurrentPaiementClient + CurrentPaiementKP + CurrentPaiementUser + currentPaiementClinique)
                Case DedicatedType.Client
                    Return CurrentPaiementClient
                Case DedicatedType.KP
                    Return CurrentPaiementKP
                Case DedicatedType.User
                    Return CurrentPaiementUser
                Case DedicatedType.Clinique
                    Return currentPaiementClinique
            End Select
        End Get
        Set(ByVal Value As Double)
            Select Case dedicatedTo
                Case DedicatedType.Client
                    CurrentPaiementClient = Value
                Case DedicatedType.KP
                    CurrentPaiementKP = Value
                Case DedicatedType.User
                    CurrentPaiementUser = Value
                Case DedicatedType.Clinique
                    currentPaiementClinique = Value
            End Select
        End Set
    End Property
#End Region

    Public Function lockSecteur(ByVal secteur As String, ByRef currentLocks As ArrayList) As Boolean
        Dim isCurLocked As Boolean = True
        Dim locking As Boolean = False
        Dim curEntiteLock As String = ""
        Dim curEntiteLocks As New Collections.Generic.List(Of String)
        With Me
            'Entité liée
            If .curFacture.noClient > 0 Then curEntiteLock = "ClientFacturation-" & .curFacture.noClient & "-"
            If .curFacture.noKP > 0 Then curEntiteLock = "KPFacturation-" & .curFacture.noKP & "-"
            If .curFacture.noUserFacture > 0 Then curEntiteLock = "UserFacturation-" & .curFacture.noUserFacture
            If curEntiteLock <> "" AndAlso currentLocks.IndexOf(curEntiteLock) < 0 AndAlso Clinica.lockSecteur(curEntiteLock, True, secteur, False) = False Then
                .locked = True
                Return False
            Else
                If curEntiteLock <> "" AndAlso currentLocks.IndexOf(curEntiteLock) = -1 Then curEntiteLocks.Add(curEntiteLock)
            End If

            'Payeurs
            If .curFacture.parNoClinique > 0 AndAlso currentLocks.IndexOf("CliniqueFacturation-" & .curFacture.parNoClinique) < 0 AndAlso Clinica.lockSecteur("CliniqueFacturation-" & .curFacture.parNoClinique, True, secteur, False) = False Then
                locking = True
            Else
                If .curFacture.parNoClinique > 0 AndAlso currentLocks.IndexOf("CliniqueFacturation-" & .curFacture.parNoClinique) < 0 Then curEntiteLocks.Add("CliniqueFacturation-" & .curFacture.parNoClinique)
            End If
            If locking = False AndAlso .curFacture.parNoUser > 0 AndAlso .curFacture.parNoUser <> .curFacture.noUserFacture AndAlso currentLocks.IndexOf("UserFacturation-" & .curFacture.parNoUser) < 0 AndAlso Clinica.lockSecteur("UserFacturation-" & .curFacture.parNoUser, True, secteur, False) = False Then
                locking = True
            Else
                If locking = False AndAlso .curFacture.parNoUser <> .curFacture.noUserFacture AndAlso .curFacture.parNoUser > 0 AndAlso currentLocks.IndexOf("UserFacturation-" & .curFacture.parNoUser) < 0 Then curEntiteLocks.Add("UserFacturation-" & .curFacture.parNoUser)
            End If
            If locking = False AndAlso .curFacture.parNoKP > 0 AndAlso .curFacture.parNoKP <> .curFacture.noKP AndAlso currentLocks.IndexOf("KPFacturation-" & .curFacture.parNoKP & "-") < 0 AndAlso Clinica.lockSecteur("KPFacturation-" & .curFacture.parNoKP & "-", True, secteur, False) = False Then
                locking = True
            Else
                If locking = False AndAlso .curFacture.parNoKP <> .curFacture.noKP AndAlso .curFacture.parNoKP > 0 AndAlso currentLocks.IndexOf("KPFacturation-" & .curFacture.parNoKP & "-") < 0 Then curEntiteLocks.Add("KPFacturation-" & .curFacture.parNoKP & "-")
            End If
            If locking = False AndAlso .curFacture.parNoClient > 0 AndAlso .curFacture.parNoClient <> .curFacture.noClient AndAlso currentLocks.IndexOf("ClientFacturation-" & .curFacture.parNoClient & "-") < 0 AndAlso Clinica.lockSecteur("ClientFacturation-" & .curFacture.parNoClient & "-", True, secteur, False) = False Then
                locking = True
            Else
                If locking = False AndAlso .curFacture.parNoClient <> .curFacture.noClient AndAlso .curFacture.parNoClient > 0 AndAlso currentLocks.IndexOf("ClientFacturation-" & .curFacture.parNoClient & "-") < 0 Then curEntiteLocks.Add("ClientFacturation-" & .curFacture.parNoClient & "-")
            End If

            If locking = True Then
                Dim i As Integer
                For i = 0 To curEntiteLocks.Count - 1
                    If Clinica.lockSecteur(curEntiteLocks(i), False, secteur, False) = False Then
                        isCurLocked = False
                        Exit For
                    End If
                Next i

                If isCurLocked = False Then 'Annule les locks effectués précédemment le dernier qui a été refusé
                    For j As Integer = 0 To i - 1
                        Clinica.lockSecteur(curEntiteLocks(j), False)
                    Next j
                End If
            Else
                For Each Lock As String In curEntiteLocks
                    currentLocks.Add(Lock)
                Next
            End If

            .locked = locking
        End With

        Return isCurLocked
    End Function

    Public Function loading(ByVal myNoFacture As Integer, Optional ByRef myPaiements As DataSet = Nothing, Optional ByRef myFactures As DataSet = Nothing, Optional ByRef startingIndexFacture As Integer = 0, Optional ByRef startingIndexPaiement As Integer = 0, Optional ByVal infoFolder As String = "") As Boolean
        If currentUserName = "Administrateur" Then loadingTime = Date.Now.Ticks
        Me.infoFolder = infoFolder
        CurrentFacture = 0
        CurrentFactureClient = 0
        CurrentFactureKP = 0
        CurrentFactureUser = 0
        CurrentFactureClinique = 0
        CurrentPaiementClient = 0
        CurrentPaiementKP = 0
        CurrentPaiementUser = 0
        currentPaiementClinique = 0
        paiement.Text = 0
        restant.Text = 0
        facture.Text = 0
        noFacture = myNoFacture

        Dim tempDedicated As DedicatedType
        Dim DedicatedString, basicInfo(,) As String
        Dim HistoPaiementLines, HistoFactureLines, j, FirstRowForFacture, firstRowForPaiement As Integer

        Dim HPaiements, hFactures As New DBHelper.StatType
        'Set Facture to display
        If IsNothing(myFactures) Then
            Dim selfOpened As Boolean = False
            If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

            thisFacture = New Bill(myNoFacture)
            If IsNothing(thisFacture) Then
                If selfOpened = True Then DBLinker.getInstance().dbConnected = False
                Return False
            End If

            If thisFacture.noFolder > 0 And Me.infoFolder = "" Then
                'REM_CODES
                basicInfo = DBLinker.getInstance.readDB("Utilisateurs INNER JOIN InfoFolders ON Utilisateurs.NoUser = InfoFolders.NoTRPTraitant", "InfoFolders.NoCodeUnique, Utilisateurs.[Nom]+','+ Utilisateurs.[Prenom] AS FullName", "WHERE (NoFolder=" & thisFacture.noFolder & ");")
                If Not basicInfo Is Nothing AndAlso basicInfo.Length <> 0 Then
                    Me.infoFolder = thisFacture.noFolder & "-" & basicInfo(1, 0) & "-" & Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNameByNoUnique(basicInfo(0, 0))
                End If

            End If

            startingIndexPaiement = 0 : startingIndexFacture = 0
            'If Facture liée then grow Histos
            If thisFacture.noBillRef <> "" Then
                Dim facturesNos As String = "(" & thisFacture.getAllBillsLinked(False) & ")"
                facturesNos = " IN " & facturesNos
                '                HPaiements = DBHelper.ReadStats("StatPaiements", "NoFacture", 0, "StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending)
                '               HFactures = DBHelper.ReadStats("StatFactures", "NoFacture", 0, "StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending)

                HPaiements = DBHelper.readStats("StatPaiements", "NoFacture", facturesNos, "NoFacture,StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending, , , False, New String(,) {{"ListeNoRecus"}, {"NoNoRecu"}})
                hFactures = DBHelper.readStats("StatFactures", "NoFacture", facturesNos, "NoFacture,StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending, , , False)
                FirstRowForFacture = 0
                firstRowForPaiement = 0
            Else
                HPaiements = DBHelper.readStats("StatPaiements", "NoFacture", myNoFacture, "StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending, , , , New String(,) {{"ListeNoRecus"}, {"NoNoRecu"}})
                hFactures = DBHelper.readStats("StatFactures", "NoFacture", myNoFacture, "StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending)
                FirstRowForFacture = startingIndexFacture
                firstRowForPaiement = startingIndexPaiement
            End If

            If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        Else
            thisFacture = New Bill
            HPaiements.statDataSet = myPaiements
            hFactures.statDataSet = myFactures

            thisFacture.loadingData(hFactures.statDataSet.Tables("Table").Rows(startingIndexFacture))

            'If Facture liée then grow Histos
            If thisFacture.noBillRef <> "" Then
                Dim facturesNos As String = "(" & thisFacture.getAllBillsLinked(False) & ")"
                facturesNos = " IN " & facturesNos
                HPaiements = DBHelper.readStats("StatPaiements", "NoFacture", facturesNos, "NoFacture,StatPaiements.NoStat", False, DBLinker.SortOrderType.Ascending, , , False, New String(,) {{"ListeNoRecus"}, {"NoNoRecu"}})
                hFactures = DBHelper.readStats("StatFactures", "NoFacture", facturesNos, "NoFacture,StatFactures.NoStat", False, DBLinker.SortOrderType.Ascending, , , False)
                FirstRowForFacture = 0
                firstRowForPaiement = 0
            Else
                FirstRowForFacture = startingIndexFacture
                firstRowForPaiement = startingIndexPaiement
            End If
        End If

        'Set Payeurs & Entité lié
        If thisFacture.noClient > 0 Then
            _EntiteType = DedicatedType.Client
            _EntiteLie = thisFacture.noClient
        ElseIf thisFacture.noKP > 0 Then
            _EntiteType = DedicatedType.KP
            _EntiteLie = thisFacture.noKP
        Else
            _EntiteType = DedicatedType.User
            _EntiteLie = thisFacture.noUser
        End If
        Me.payeurs.entiteType = Me.entiteType

        'Set Histos of Facture and set fields to display
        If IsNothing(hFactures) = False Then
            HistoFactureLines = 0
            HistoPaiementLines = 0
            Dim lastHistoLine As String = ""
            Dim newHistoLine As String = ""
            With Me
                .noFacture = myNoFacture

                'TypeFacture
                Dim typeFacture As String = thisFacture.type
                Dim sizeTypeFacture As Size = measureString(typeFacture, lblTypeFacture.Font)
                If sizeTypeFacture.Width > lblTypeFacture.Width Then
                    typeFacture = typeFacture.Substring(0, typeFacture.Length * lblTypeFacture.Width / sizeTypeFacture.Width - 5) & "..."
                End If
                lblTypeFacture.Text = typeFacture
                ToolTip1.SetToolTip(lblTypeFacture, thisFacture.type)
                If thisFacture.isSouffrance Then
                    lblTypeFacture.ForeColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorTitreFactureSouffrance"))
                Else
                    lblTypeFacture.ForeColor = Color.Black
                End If

                'Entité
                Dim clientName As String = ""
                Dim kpName As String = ""
                If thisFacture.noClient > 0 Then
                    clientName = getClientName(thisFacture.noClient) & " (" & thisFacture.noClient & ")"
                    Me.payeurs.entiteLie = clientName & " " & Me.infoFolder
                End If
                If thisFacture.noKP > 0 Then kpName = getKPName(thisFacture.noKP) & " (" & thisFacture.noKP & ")" : Me.payeurs.entiteLie = kpName
                If thisFacture.noUserFacture > 0 Then Me.payeurs.entiteLie = UsersManager.getInstance.getUser(thisFacture.noUserFacture).toString()

                'Payeurs
                If thisFacture.parNoClient > 0 Then
                    If clientName = "" Or thisFacture.parNoClient <> thisFacture.noClient Then clientName = getClientName(thisFacture.parNoClient) & " (" & thisFacture.parNoClient & ")"
                    Me.payeurs.client = clientName
                Else
                    Me.payeurs.client = "Aucun"
                End If
                If thisFacture.parNoKP > 0 Then
                    If kpName = "" Or thisFacture.parNoKP <> thisFacture.noKP Then kpName = getKPName(thisFacture.parNoKP) & " (" & thisFacture.parNoKP & ")"
                    Me.payeurs.kp = kpName
                Else
                    Me.payeurs.kp = "Aucun"
                End If
                If thisFacture.parNoUser > 0 Then
                    Me.payeurs.user = UsersManager.getInstance.getUser(thisFacture.parNoUser).toString()
                Else
                    Me.payeurs.user = "Aucun"
                End If

                Me.payeurs.linkedFacture = thisFacture

                'Factures liés
                If thisFacture.noBillTransfered <> 0 Then
                    _IsLinked = True
                End If
                If thisFacture.noBillRef <> "" Then
                    _IsUnified = True
                    Me.payeurs.noFactureRefStr = thisFacture.getAllBillsLinked(True)
                Else
                    Me.payeurs.noFactureRefStr = "Aucune"
                End If

                'Chargement des historiques
                Dim PaymentType, Verbe, MyComment, RecuString, RecusString, NoFactureString, curFactureString As String
                Dim lastNoFacture As Integer
                Dim currentFactureShowed As Double

                .histoFactures.cls()
                .histoFactures.config(0)
                If hFactures.statDataSet Is Nothing Then GoTo SkipLoadingFactureHisto
                With hFactures.statDataSet.Tables("Table")
                    For j = FirstRowForFacture To .Rows.Count - 1
                        MyComment = ""
                        'Try
                        With .Rows(j)
                            If Integer.Parse(.Item("NoFacture")) <> myNoFacture And Not isUnified Then
                                If HistoFactureLines <> 0 Then histoFactures.remove(histoFactures.maximum)
                                startingIndexFacture = j
                                Exit For
                            ElseIf Integer.Parse(.Item("NoFacture")) <> lastNoFacture And Me.isUnified Then
                                If PreferencesManager.getGeneralPreferences()("MontantFactureHistoIsCumulative") = False Then currentFactureShowed = 0
                            End If

                            Dim curMF As Double = Double.Parse(.Item("MontantFacture").ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                            lastNoFacture = Integer.Parse(.Item("NoFacture"))
                            CurrentFacture += curMF
                            currentFactureShowed += curMF
                            currentFactureShowed = roundAmount(currentFactureShowed)
                            CurrentFacture = roundAmount(CurrentFacture)
                            If .Item("NoEntitePayeur") = 1 Then CurrentFactureClient += curMF
                            If .Item("NoEntitePayeur") = 2 Then CurrentFactureKP += curMF
                            If .Item("NoEntitePayeur") = 3 Then CurrentFactureUser += curMF
                            If .Item("NoEntitePayeur") = 4 Then CurrentFactureClinique += curMF

                            If Me.isUnified Then
                                NoFactureString = vbCrLf & "N° de la facture : " & .Item("NoFacture").ToString
                                If PreferencesManager.getGeneralPreferences()("MontantFactureHistoIsCumulative") = True Then
                                    curFactureString = currentFactureShowed
                                Else
                                    curFactureString = curMF
                                End If
                            Else
                                NoFactureString = ""
                                If PreferencesManager.getGeneralPreferences()("MontantFactureHistoIsCumulative") = True Then
                                    curFactureString = CurrentFacture
                                Else
                                    curFactureString = curMF
                                End If
                            End If

                            If .Item("Commentaires").ToString <> "" Then MyComment = vbCrLf & .Item("Commentaires").ToString

                            If .Item("NoFactureRef").ToString.ToUpper = "" Then
                                newHistoLine = "(" & Format(.Item("DateHeureCreation"), "yyyy-MM-dd") & ") " & curFactureString & "$ confirmé par " & .Item("FullName").ToString & NoFactureString & MyComment

                                If newHistoLine <> lastHistoLine Then
                                    lastHistoLine = newHistoLine
                                    histoFactures.add(newHistoLine)
                                    HistoFactureLines += 1
                                    If j <> (.Table.Rows.Count - 1) Then
                                        Dim myLine As Short = histoFactures.add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                                        histoFactures.ItemFont(myLine) = New Font(histoFactures.ItemFont(myLine).FontFamily, 3, histoFactures.ItemFont(myLine).Style)
                                        histoFactures.myList.ItemClickable(myLine) = False
                                    End If
                                End If
                            Else
                                histoFactures.remove(histoFactures.maximum)
                            End If
                        End With
                        'Catch
                        'End Try
                    Next j
                End With
                .histoFactures.reverse()
SkipLoadingFactureHisto:
                .histoFactures.draw = True : .histoFactures.draw = False

                .histoPaiements.cls()
                .histoPaiements.config(0)
                RecusString = ""
                lastHistoLine = ""

                If IsNothing(HPaiements.statDataSet) = False Then
                    If IsNothing(HPaiements.statDataSet.Tables("Table")) = False Then
                        With HPaiements.statDataSet.Tables("Table")
                            For j = firstRowForPaiement To .Rows.Count - 1
                                MyComment = "" : PaymentType = "" : DedicatedString = vbCrLf
                                With .Rows(j)
                                    If Integer.Parse(.Item("NoFacture")) <> myNoFacture And Not isUnified Then
                                        If HistoPaiementLines <> 0 Then histoPaiements.remove(histoPaiements.maximum)
                                        startingIndexPaiement = j
                                        Exit For
                                    End If

                                    If .Item("Commentaires").ToString <> "" Then MyComment = vbCrLf & .Item("Commentaires").ToString
                                    If .Item("NoAction") <> 12 Then
                                        Verbe = "ajusté"
                                        DedicatedString &= "Ajusté pour "
                                    Else
                                        Verbe = "reçu"
                                        PaymentType = vbCrLf & "Méthode de paiement : " & .Item("TypePaiement").ToString
                                        DedicatedString &= "Payé par "
                                    End If

                                    Dim curMP As Double = Double.Parse(.Item("MontantPaiement").ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                                    Select Case .Item("NoEntitePayeur")
                                        Case 1
                                            tempDedicated = DedicatedType.Client
                                            DedicatedString &= "le client."
                                            CurrentPaiementClient += curMP
                                        Case 3
                                            tempDedicated = DedicatedType.User
                                            DedicatedString &= "l'utilisateur."
                                            tempDedicated = DedicatedType.User
                                            CurrentPaiementUser += curMP
                                        Case 2
                                            tempDedicated = DedicatedType.KP
                                            DedicatedString &= "la personne / organisme clé."
                                            tempDedicated = DedicatedType.KP
                                            CurrentPaiementKP += curMP
                                        Case 4
                                            tempDedicated = DedicatedType.Clinique
                                            DedicatedString &= "la clinique."
                                            currentPaiementClinique += curMP
                                    End Select

                                    RecuString = ""
                                    If .Item("NoRecu").ToString <> "" Then
                                        If RecusString.IndexOf("," & .Item("NoRecu").ToString) < 0 Then RecusString &= "," & .Item("NoRecu")
                                        RecuString = vbCrLf & "N° du reçu : " & .Item("NoRecu")
                                    End If

                                    If isUnified Then
                                        NoFactureString = vbCrLf & "N° de la facture : " & .Item("NoFacture").ToString
                                    Else
                                        NoFactureString = ""
                                    End If

                                    If tempDedicated = dedicatedTo Or dedicatedTo = DedicatedType.All Then
                                        If dedicatedTo <> DedicatedType.All Then DedicatedString = ""
                                        Dim myMontantPaiement As String = .Item("MontantPaiement")
                                        forceManaging(myMontantPaiement, True, "", False, True, False, True, ",§.§-", , , , , , , 2, True)

                                        newHistoLine = "(" & DateFormat.getTextDate(Date.Parse(.Item("DateHeureCreation")), DateFormat.TextDateOptions.YYYYMMDD) & ") " & myMontantPaiement & "$ " & Verbe & " par " & .Item("FullName").ToString & PaymentType & DedicatedString & RecuString & NoFactureString & MyComment

                                        If lastHistoLine <> newHistoLine Then
                                            lastHistoLine = newHistoLine
                                            histoPaiements.add(newHistoLine)
                                            HistoPaiementLines += 1
                                            If j <> (.Table.Rows.Count - 1) Then
                                                Dim myLine As Short = histoPaiements.add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                                                histoPaiements.ItemFont(myLine) = New Font(histoPaiements.ItemFont(myLine).FontFamily, 3, histoPaiements.ItemFont(myLine).Style)
                                                histoPaiements.myList.ItemClickable(myLine) = False
                                            End If
                                        End If
                                    End If
                                End With
                            Next j
                        End With
                    End If
                End If
                .histoPaiements.reverse()
                .histoPaiements.draw = True : .histoPaiements.draw = False

                .facture.Text = .CurrentFacture
                If Not isUnified Then setPaiementNRestant(True)

                'Reçu(s) string preloading
                Me.thisFacture.noReceiptsReadDB = False
                Me.thisFacture.noReceiptsReadReal = False
                Me.thisFacture.getNoReceiptsString(True)
            End With

            'Affichage Pourcentages
            If CurrentFacture > 0 Then
                Me.payeurs.pourcentParNoClient = Math.Round(thisFacture.getPourcentClient() * 100, 4)
                Me.payeurs.pourcentParNoKP = Math.Round(thisFacture.getPourcentKP() * 100, 4)
                Me.payeurs.pourcentParNoUser = Math.Round(thisFacture.getPourcentUser() * 100, 4)
            Else
                Me.payeurs.pourcentParNoClient = 0
                Me.payeurs.pourcentParNoKP = 0
                Me.payeurs.pourcentParNoUser = 0
            End If

            'Ajustement pour une facture unifiée
            If isUnified Then
                startingIndexFacture += 1
                facture.Text = CurrentFacture
                paiement.Text = currentPaiement
                restant.Text = roundAmount(CurrentFacture - currentPaiement)
                payeurs.montantPaiementClient = CurrentPaiementClient
                payeurs.montantPaiementKP = CurrentPaiementKP
                payeurs.montantPaiementUser = CurrentPaiementUser
                payeurs.montantPaiementClinique = currentPaiementClinique
            End If
        End If

        Me.description.Text = thisFacture.description.Replace("\n", vbCrLf)

        hFactures = Nothing
        HPaiements = Nothing

        If Me.curFacture.taxe1 = -1 Then
            Me.selectF.Enabled = False
            Me.selectP.Checked = True
        Else
            Me.selectF.Enabled = True
        End If

        MyBase.Invalidate(True)

        If currentUserName = "Administrateur" Then
            loadingTime = DateTime.Now.Ticks - loadingTime
            loadingTime /= 10000000 'To get in seconds
            Me.description.Text = "Time to load -> " & loadingTime & "s"
        End If

        Return True
    End Function

    Private Sub setPaiementNRestant(Optional ByVal minimumCurrent As Boolean = False)
        With Me
            Select Case dedicatedTo
                Case DedicatedType.All
                    If minimumCurrent And (.CurrentPaiementClient + .CurrentPaiementKP + .CurrentPaiementUser + .currentPaiementClinique) > .paiement.Text Then .paiement.Text = .CurrentPaiementClient + .CurrentPaiementKP + .CurrentPaiementUser + .currentPaiementClinique
                    .tempMontantFacture.Text = facture.Text
                Case DedicatedType.Client
                    If minimumCurrent And .CurrentPaiementClient > .paiement.Text Then .paiement.Text = .CurrentPaiementClient
                    .tempMontantFacture.Text = (facture.Text * thisFacture.getPourcentClient)
                Case DedicatedType.KP
                    If minimumCurrent And .CurrentPaiementKP > .paiement.Text Then .paiement.Text = .CurrentPaiementKP
                    .tempMontantFacture.Text = (facture.Text * thisFacture.getPourcentKP)
                Case DedicatedType.User
                    If minimumCurrent And .CurrentPaiementUser > .paiement.Text Then .paiement.Text = .CurrentPaiementUser
                    .tempMontantFacture.Text = (facture.Text * thisFacture.getPourcentUser)
                Case DedicatedType.Clinique
                    If minimumCurrent And .currentPaiementClinique > .paiement.Text Then .paiement.Text = .currentPaiementClinique
                    .tempMontantFacture.Text = facture.Text * thisFacture.getPourcentClinic
            End Select
            If (tempMontantFacture.Text - .paiement.Text) < 0 And Not isUnified Then
                .restant.Text = 0
                .paiement.Text = .tempMontantFacture.Text
            Else
                .restant.Text = roundAmount(.tempMontantFacture.Text - .paiement.Text)
            End If

            If .payeurs IsNot Nothing Then
                Select Case dedicatedTo
                    Case DedicatedType.All
                        .payeurs.montantPaiementClient = CurrentPaiementClient
                        .payeurs.montantPaiementKP = CurrentPaiementKP
                        .payeurs.montantPaiementUser = CurrentPaiementUser
                        .payeurs.montantPaiementClinique = currentPaiementClinique
                    Case DedicatedType.Client
                        .payeurs.montantPaiementClient = .paiement.Text
                        .payeurs.montantPaiementKP = CurrentPaiementKP
                        .payeurs.montantPaiementUser = CurrentPaiementUser
                        .payeurs.montantPaiementClinique = currentPaiementClinique
                    Case DedicatedType.KP
                        .payeurs.montantPaiementClient = CurrentPaiementClient
                        .payeurs.montantPaiementKP = .paiement.Text
                        .payeurs.montantPaiementUser = CurrentPaiementUser
                        .payeurs.montantPaiementClinique = currentPaiementClinique
                    Case DedicatedType.User
                        .payeurs.montantPaiementClient = .paiement.Text
                        .payeurs.montantPaiementKP = CurrentPaiementKP
                        .payeurs.montantPaiementUser = CurrentPaiementUser
                        .payeurs.montantPaiementClinique = currentPaiementClinique
                    Case DedicatedType.Clinique
                        .payeurs.montantPaiementClient = CurrentPaiementClient
                        .payeurs.montantPaiementKP = CurrentPaiementKP
                        .payeurs.montantPaiementUser = CurrentPaiementUser
                        .payeurs.montantPaiementClinique = .paiement.Text
                End Select
            End If
        End With
    End Sub

    Private Sub facturationBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Width = 536
        Me.Height = 130
        Me.groupe.Size = Me.Size
    End Sub

    Private Sub selecting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectF.CheckedChanged, selectP.CheckedChanged
        paiement.Enabled = Not selectF.Checked
        facture.Enabled = selectF.Checked
        facture.Text = CurrentFacture
        paiement.Text = currentPaiement
        setPaiementNRestant()
    End Sub

    Private Sub adjusting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles adjusting.Click
        Dim newAmountPaiement As Double = paiement.Text.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        Dim newAmountFacture As Double = facture.Text.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        If currentPaiement = newAmountPaiement And CurrentFacture = newAmountFacture Then MessageBox.Show("Vous devez absolument modifier un des montants pour ajuster la facture.", "Impossible d'ajuster") : Exit Sub

        'Commentaires
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.firstLetterCapital = True
        Dim myComment As String = myInputBoxPlus("Veuillez entrer le commentaire de l'ajustement", "Commentaire")
        If PreferencesManager.getGeneralPreferences()("AdjustingCommentsForced") = True And myComment = "" Then
            MessageBox.Show("Le commentaire est obligatoire.", "Champ obligatoire")
            Exit Sub
        End If

        adjusting.Enabled = False

        Dim selfOpened As Boolean = False
        Dim myMontantFacture As Double = -1
        Dim myMontantPaiement As Double = -1
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim myFacture As Bill = New Bill(Me.noFacture)

        With myFacture
            If selectF.Checked = True Then
                If CurrentFacture = newAmountFacture Then GoTo fin
                'Ajustement de la facture
                myMontantFacture = newAmountFacture
            Else
                If currentPaiement = newAmountPaiement Then GoTo fin
                'Ajustement du paiement
                myMontantPaiement = newAmountPaiement
            End If

            Dim statDate As Date = LIMIT_DATE
            If currentDroitAcces(90) = True Then 'Si l'utilisateur a le droit de choisir la date d'ajustement
                Dim myDateChoice As New DateChoice
                Dim myStatDate As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year - 2, Date.Now.Year, , , , , , , , , , , Date.Today)
                If myStatDate.Count <> 0 Then statDate = myStatDate(0)
            End If

            Dim myFactureAction As FactureAction
            If dedicatedTo = DedicatedType.All And selectF.Checked = False Then
                Dim rejectedFilters As New ArrayList
                Dim filter As String = .getFilterPayeur(, "pour qui ajusté le montant du paiement")
                rejectedFilters.Add(filter)
                Dim diffP As Double = myMontantPaiement - Me.currentPaiement
                Dim restant As Double = 0

                Select Case filter
                    Case "C" 'Client
                        If (CurrentPaiementClient + diffP) > (CurrentFacture * .getPourcentClient) Then
                            myMontantPaiement = (CurrentFacture * .getPourcentClient)
                            restant = diffP - (myMontantPaiement - CurrentPaiementClient)
                        ElseIf (CurrentPaiementClient + diffP) < 0 Then
                            myMontantPaiement = 0
                            restant = diffP - (myMontantPaiement - CurrentPaiementClient)
                        Else
                            myMontantPaiement = CurrentPaiementClient + diffP
                        End If
                    Case "K" 'Personne / organisme clé
                        If (CurrentPaiementKP + diffP) > (CurrentFacture * .getPourcentKP) Then
                            myMontantPaiement = (CurrentFacture * .getPourcentKP)
                            restant = diffP - (myMontantPaiement - CurrentPaiementKP)
                        ElseIf (CurrentPaiementKP + diffP) < 0 Then
                            myMontantPaiement = 0
                            restant = diffP - (myMontantPaiement - CurrentPaiementKP)
                        Else
                            myMontantPaiement = CurrentPaiementKP + diffP
                        End If
                    Case "U" 'Utilisateur
                        If (CurrentPaiementUser + diffP) > (CurrentFacture * .getPourcentUser) Then
                            myMontantPaiement = (CurrentFacture * .getPourcentUser)
                            restant = diffP - (myMontantPaiement - CurrentPaiementUser)
                        ElseIf (CurrentPaiementUser + diffP) < 0 Then
                            myMontantPaiement = 0
                            restant = diffP - (myMontantPaiement - CurrentPaiementUser)
                        Else
                            myMontantPaiement = CurrentPaiementUser + diffP
                        End If
                    Case "" 'Clinique
                        If (currentPaiementClinique + diffP) > CurrentFacture Then
                            myMontantPaiement = CurrentFacture
                            restant = 0
                        Else
                            myMontantPaiement = currentPaiementClinique + diffP
                        End If
                End Select

                myFactureAction = adjustFacture(Me.noFacture, myMontantFacture, myMontantPaiement, , , myComment, , , filter, statDate)

                If .nbPayers > 1 And restant <> 0 Then
                    filter = .getFilterPayeur(, "pour qui ajusté le montant du paiement", rejectedFilters)
                    rejectedFilters.Add(filter)
                    Select Case filter
                        Case "C" 'Client
                            If (CurrentPaiementClient + restant) > (CurrentFacture * .getPourcentClient) Then
                                myMontantPaiement = (CurrentFacture * .getPourcentClient)
                                restant -= (myMontantPaiement - CurrentPaiementClient)
                            ElseIf (CurrentPaiementClient + restant) < 0 Then
                                myMontantPaiement = 0
                                restant -= (myMontantPaiement - CurrentPaiementClient)
                            Else
                                myMontantPaiement = CurrentPaiementClient + restant
                            End If
                        Case "K" 'Personne / organisme clé
                            If (CurrentPaiementKP + restant) > (CurrentFacture * .getPourcentKP) Then
                                myMontantPaiement = (CurrentFacture * .getPourcentKP)
                                restant = restant - (myMontantPaiement - CurrentPaiementKP)
                            ElseIf (CurrentPaiementKP + restant) < 0 Then
                                myMontantPaiement = 0
                                restant = restant - (myMontantPaiement - CurrentPaiementKP)
                            Else
                                myMontantPaiement = CurrentPaiementKP + restant
                            End If
                        Case "U" 'Utilisateur
                            If (CurrentPaiementUser + restant) > (CurrentFacture * .getPourcentUser) Then
                                myMontantPaiement = (CurrentFacture * .getPourcentUser)
                                restant = restant - (myMontantPaiement - CurrentPaiementUser)
                            ElseIf (CurrentPaiementUser + restant) < 0 Then
                                myMontantPaiement = 0
                                restant = restant - (myMontantPaiement - CurrentPaiementUser)
                            Else
                                myMontantPaiement = CurrentPaiementUser + restant
                            End If
                    End Select

                    myFactureAction = adjustFacture(Me.noFacture, myMontantFacture, myMontantPaiement, , , myComment, , , filter, statDate)

                    If .nbPayers > 2 And restant <> 0 Then
                        filter = .getFilterPayeur(, "pour qui ajusté le montant du paiement", rejectedFilters)
                        Select Case filter
                            Case "C" 'Client
                                If (CurrentPaiementClient + restant) > (CurrentFacture * .getPourcentClient) Then
                                    myMontantPaiement = (CurrentFacture * .getPourcentClient)
                                ElseIf (CurrentPaiementClient + restant) < 0 Then
                                    myMontantPaiement = 0
                                Else
                                    myMontantPaiement = CurrentPaiementClient + restant
                                End If
                            Case "K" 'Personne / organisme clé
                                If (CurrentPaiementKP + restant) > (CurrentFacture * .getPourcentKP) Then
                                    myMontantPaiement = (CurrentFacture * .getPourcentKP)
                                ElseIf (CurrentPaiementKP + restant) < 0 Then
                                    myMontantPaiement = 0
                                Else
                                    myMontantPaiement = CurrentPaiementKP + restant
                                End If
                            Case "U" 'Utilisateur
                                If (CurrentPaiementUser + restant) > (CurrentFacture * .getPourcentUser) Then
                                    myMontantPaiement = (CurrentFacture * .getPourcentUser)
                                ElseIf (CurrentPaiementUser + restant) < 0 Then
                                    myMontantPaiement = 0
                                Else
                                    myMontantPaiement = CurrentPaiementUser + restant
                                End If
                        End Select

                        myFactureAction = adjustFacture(Me.noFacture, myMontantFacture, myMontantPaiement, , , myComment, , , filter, statDate)
                    End If
                End If
            Else
                Dim entitePayeur As String = dedicatedTo.ToString.Substring(0, 1).ToUpper
                If .parNoClient = 0 And .parNoKP = 0 And .parNoUser = 0 Then entitePayeur = ""
                myFactureAction = adjustFacture(Me.noFacture, myMontantFacture, myMontantPaiement, , , myComment, , , entitePayeur, statDate)
            End If
            Select Case myFactureAction
                Case CommonProc.FactureAction.Created
                    RaiseEvent createdFacturation(Me)
                    myMainWin.StatusText = "Facture #" & Me.noFacture & " ajusté. " & facture.Text & "$ facturé, " & paiement.Text & "$ payé."
                Case CommonProc.FactureAction.Adjusted Or CommonProc.FactureAction.Deleted
                    myMainWin.StatusText = "Facture #" & Me.noFacture & " ajusté. " & facture.Text & "$ facturé, " & paiement.Text & "$ payé."
                Case CommonProc.FactureAction.Err
                    MessageBox.Show("Le montant facturé doit absolument être supérieur au montant payé et les pourcentages doivent égalés 100%.", "Impossible d'ajuster la facture")
                    GoTo fin
            End Select

            If .noVisite > 0 Then updateVisites(.noClient, .noFolder, .dateFacture, , False)
            If (dedicatedTo = DedicatedType.All Or dedicatedTo = DedicatedType.Client) And (.parNoClient > 0 Or .noClient > 0) Then
                InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & .parNoClient & ",-1)")
                InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & .parNoClient & ")")
            End If
            If (dedicatedTo = DedicatedType.All Or dedicatedTo = DedicatedType.KP) And (.parNoKP > 0 Or .noKP > 0) Then
                InternalUpdatesManager.getInstance.sendUpdate("PaiementsKP(" & .parNoKP & ",-1)")
                InternalUpdatesManager.getInstance.sendUpdate("AccountsBillsKP(" & .parNoKP & ")")
            End If
            If .noVisite > 0 Then
                Dim getNoTrp() As String = DBLinker.getInstance.readOneDBField("InfoVisites", "NoTRP", "WHERE NoVisite=" & .noVisite)
                updateVisites(.noClient, .noFolder, .dateFacture, , False, getNoTrp(0))
            End If

            'Ask if the user want to generate bill receipt on positive payment adjustment
            If (newAmountPaiement - .amountPaidByClient) > 0 AndAlso MessageBox.Show("Voulez-vous générer de reçu de cet ajustement ?", "Demande de création de reçu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then .generateReceipt(.getFilterPayeur())
        End With

        currentPaiement = newAmountPaiement
        CurrentFacture = newAmountFacture
        loading(Me.noFacture)

fin:
        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        adjusting.Enabled = True
        If selectF.Checked = True Then
            facture.Focus()
            facture.SelectAll()
        Else
            paiement.Focus()
            paiement.SelectAll()
        End If

    End Sub

    Private Sub paiement_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles paiement.Validating
        Try
            If Me.DesignMode = True Then Exit Sub
            If CDbl(paiement.Text) > CDbl(CurrentFacture) Then paiement.Text = CurrentFacture
            MontantPaiement(False) = paiement.Text
            setPaiementNRestant()
        Catch 'REM Exception not handle
        End Try
    End Sub

    Private Sub facture_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles facture.Validating
        Try
            If Me.DesignMode = True Then Exit Sub
            Dim minimumAmount As Double
            If thisFacture.getPourcentClient > 0 Then minimumAmount = CurrentPaiementClient / (thisFacture.getPourcentClient)
            If thisFacture.getPourcentKP > 0 Then minimumAmount = Math.Max(minimumAmount, CurrentPaiementKP / (thisFacture.getPourcentKP))
            If thisFacture.getPourcentUser > 0 Then minimumAmount = Math.Max(minimumAmount, CurrentPaiementUser / (thisFacture.getPourcentUser))
            If thisFacture.getPourcentClinic > 0 Then minimumAmount = Math.Max(minimumAmount, currentPaiementClinique / (thisFacture.getPourcentClinic))

            If facture.Text < minimumAmount Then facture.Text = minimumAmount
            setPaiementNRestant()
        Catch 'REM Exception not handle
        End Try
    End Sub

    Private Sub groupe_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles groupe.Paint
        Dim normalFont As New Font("Microsoft Sans Sherif", 8.25!, FontStyle.Regular)
        With Me.curFacture
            'Draw DateFacture
            e.Graphics.DrawString(DateFormat.getTextDate(.dateFacture), normalFont, Brushes.Black, 472, 24)
            e.Graphics.DrawString("Date de facturation :", normalFont, Brushes.Black, 367, 24)
        End With

        'Draw general strings
        e.Graphics.DrawString("# facture :", normalFont, Brushes.Black, 8, 48)
        e.Graphics.DrawString(Me.noFacture, normalFont, Brushes.Black, 60, 48)
        e.Graphics.DrawString("Montant à payer :", normalFont, Brushes.Black, 368, 72)
        e.Graphics.DrawString("$", normalFont, Brushes.Black, 520, 72)
        e.Graphics.DrawString("$", normalFont, Brushes.Black, 336, 72)
        e.Graphics.DrawString("$", normalFont, Brushes.Black, 160, 72)
        e.Graphics.DrawString("Description :", normalFont, Brushes.Black, 8, 96)
        normalFont.Dispose()
    End Sub

    Private Sub description_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles description.Click
        resizingDescription = True

        description.Top = 26
        description.Height = 90
        description.ScrollBars = ScrollBars.Vertical
        If Me.locked = False Then
            saveDesc.Visible = True
            cancelDesc.Visible = True
        End If

        resizingDescription = False
    End Sub

    Private resizingDescription As Boolean = False

    Private Sub description_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles description.Leave
        'Dim a As Boolean = saveDesc.Focused
        If description.Height <> 20 And resizingDescription = False And saveDesc.Focused = False And cancelDesc.Focused = False Then
            hideDescription()
        End If
    End Sub

    Private Sub hideDescription()
        description.Top = 96
        description.Height = 20
        description.ScrollBars = ScrollBars.None
        saveDesc.Visible = False
        cancelDesc.Visible = False
    End Sub

    Private Sub saveDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveDesc.Click
        curFacture.description = description.Text
        curFacture.saveDescription()
        hideDescription()
    End Sub

    Private Sub cancelDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelDesc.Click
        description.Text = curFacture.description
        hideDescription()
    End Sub
End Class
