Public Class Bill
    Implements ICloneable, IDisposable

    Public Sub New()

    End Sub

    Public Sub New(ByVal noFacture As Integer)
        loadingData(noFacture)
    End Sub

    Public Sub New(ByVal noFolder As Integer, ByVal noVisite As Integer)
        loadingData(noFolder, noVisite)
    End Sub

#Region "Declarations - Public"
    Public noFacture As Integer
    Public dateFacture As Date
    Public noClient As Integer
    Public noFolder As Integer
    Public noPret As Integer
    Public noVente As Integer
    Public noVisite As Integer
    Public amountBilledToClient As Double
    Public amountBilledToKP As Double
    Public amountBilledToUser As Double
    Public amountBilledToClinic As Double
    Public amountPaidByClient As Double
    Public noBillTransfered As Integer
    Public taxe1 As Double
    Public taxe2 As Double
    Public noUser As Integer
    Public takenFromStats As Boolean
    Public noKP As Integer
    Public noUserFacture As Integer
    Public parNoClient As Integer
    Public parNoKP As Integer
    Public parNoUser As Integer
    Public parNoClinique As Integer
    Public amountPaidByKP As Double
    Public amountPaidByUser As Double
    Public amountPaidByClinic As Double
    Public dateHeureLastConfirmed As Date
    Friend taxesApplication As TaxesApplications
#End Region

    Private _IsSouffrance As Boolean = False
    Private _Description As String
    Private _type As String
    Private _noBillRef As String
    Private _Comments As String
    Private noReceiptsStringReal As String = ""
    Private noReceiptsStringDB As String = ""
    Private linkedNoString As String = ""
    Private linkedNoStringGrouped As String = ""
    Public noReceiptsReadReal As Boolean = False
    Public noReceiptsReadDB As Boolean = False

#Region "Properties"
    Public Property description() As String
        Get
            If _Description Is Nothing Then Return ""

            Return _Description.Replace("\n", vbCrLf)
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _Description = ""
            Else
                _Description = value
            End If
        End Set
    End Property

    Public Property type() As String
        Get
            If _type Is Nothing Then Return ""

            Return _type
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _type = ""
            Else
                _type = value
            End If
        End Set
    End Property

    Public Property noBillRef() As String
        Get
            If _noBillRef Is Nothing Then Return ""

            Return _noBillRef
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _noBillRef = ""
            Else
                _noBillRef = value
            End If
        End Set
    End Property

    Public Property comments() As String
        Get
            If _Comments Is Nothing Then Return ""

            Return _Comments
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _Comments = ""
            Else
                _Comments = value
            End If
        End Set
    End Property

    Public ReadOnly Property entityLinked() As Integer
        Get
            If noClient > 0 Then Return noClient
            If noKP > 0 Then Return noKP
            If noUserFacture > 0 Then Return noUser

            Return 0
        End Get
    End Property

    Public ReadOnly Property entityType() As FacturationBox.DedicatedType
        Get
            If noClient > 0 Then Return FacturationBox.DedicatedType.Client
            If noKP > 0 Then Return FacturationBox.DedicatedType.KP
            If noUserFacture > 0 Then Return FacturationBox.DedicatedType.User

            Return FacturationBox.DedicatedType.All
        End Get
    End Property

    Public ReadOnly Property isSouffrance() As Boolean
        Get
            Return _IsSouffrance
        End Get
    End Property
#End Region

    Public Function getBillPaymentTotal() As Double
        Return amountPaidByClient + amountPaidByKP + amountPaidByUser + amountPaidByClinic
    End Function

    Public Function getBillTotal() As Double
        Return roundAmount(amountBilledToClient + amountBilledToClinic + amountBilledToKP + amountBilledToUser)
    End Function

    Public Function getPourcentClient() As Double
        Dim total As Double = getBillTotal()
        If total = 0 Then Return 1 'REM Should be changed for real pourcentage.. Shouldn't be base on amounts. Stocking % in sql db is obligatory

        Return amountBilledToClient / total
    End Function

    Public Function getPourcentKP() As Double
        Dim total As Double = getBillTotal()
        If total = 0 Then Return 0

        Return amountBilledToKP / total
    End Function

    Public Function getPourcentUser() As Double
        Dim total As Double = getBillTotal()
        If total = 0 Then Return 0

        Return amountBilledToUser / total
    End Function

    Public Function getPourcentClinic() As Double
        Dim total As Double = getBillTotal()
        If total = 0 Then Return 0

        Return amountBilledToClinic / total
    End Function

    Public Function isJoinableBills() As Boolean
        Dim jBills() As Integer = getJoinableNoBills()
        If jBills Is Nothing Then Return False
        If jBills.Length = 0 Then Return False

        Return True
    End Function

    Public Function isReceiptToDo() As Boolean
        Return _isReceiptToDo(Nothing)
    End Function

    Private Function _isReceiptToDo(Optional ByVal payingEntity As Char = Nothing) As Boolean
        Dim whereEP As String = ""
        If payingEntity <> Nothing Then whereEP = " AND NoEntitePayeur=" & BillsManager.translatePayingEntity(payingEntity)
        Dim whereStr As String = "WHERE NoFacture=" & Me.noFacture & whereEP
        If Me.noBillRef <> "" Then whereStr = "WHERE NoFacture IN (" & getAllBillsLinked() & ")"
        Dim nbPaiementWORecu() As String = DBLinker.getInstance.readOneDBField("FacturesRecusLeft", "COUNT(*)", whereStr)

        If nbPaiementWORecu(0) = 0 Then Return False
        Return True
    End Function

    Public Function isReceiptToDo(ByVal entitePayeur As Char) As Boolean
        Return _isReceiptToDo(entitePayeur)
    End Function

#Region "IsPaymentsToDo functions"
    Public Function isPaymentsToDo() As Boolean
        If isSouffrance Then Return False

        Return getBillPaymentTotal() < getBillTotal()
    End Function

    Public Function isPaymentsToDo(ByVal entitePayeur As Char) As Boolean
        If isSouffrance Then Return False

        Select Case entitePayeur
            Case "C"
                Return amountPaidByClient < amountBilledToClient
            Case "K"
                Return amountPaidByKP < amountBilledToKP
            Case "U"
                Return amountPaidByUser < amountPaidByUser
        End Select

        Return isPaymentsToDo
    End Function

    Public Shared Function isPaymentsToDo(ByVal noFacture As Integer) As Boolean
        Dim whereStr As String = ""
        If noFacture > 0 Then whereStr = "WHERE (NoFacture=" & noFacture & " AND IsSouffrance=0);"
        Dim myPaiements() As String = DBLinker.getInstance.readOneDBField("Factures", "NoFacture", whereStr)
        If myPaiements Is Nothing OrElse myPaiements.Length = 0 Then Return False
        If myPaiements(0) = "" Then Return False
        If myPaiements(0) = 0 Then Return False

        Return True
    End Function

    Public Shared Function isPaymentsToDoByKP(ByVal noKP As Integer) As Boolean
        Dim myPaiements() As String = DBLinker.getInstance.readOneDBField("Factures", "sum(MontantFactureKP-MontantPaiementKP)", "WHERE (ParNoKP=" & noKP & " AND IsSouffrance=0);")
        If myPaiements Is Nothing Then Return False
        If myPaiements(0) = "" OrElse myPaiements.Length = 0 Then Return False
        If myPaiements(0) = 0 Then Return False

        Return True
    End Function

    Public Shared Function isPaymentsToDoByUser(ByVal noUser As Integer) As Boolean
        Dim myPaiements() As String = DBLinker.getInstance.readOneDBField("Factures", "sum(MontantFactureUser-MontantPaiementUser)", "WHERE (ParNoUser=" & noUser & " AND IsSouffrance=0);")
        If myPaiements Is Nothing OrElse myPaiements.Length = 0 Then Return False
        If myPaiements(0) = "" Then Return False
        If myPaiements(0) = 0 Then Return False

        Return True
    End Function

    Public Shared Function isPaymentsToDoByClient(ByVal noClient As Integer) As Boolean
        Dim myPaiements() As String = DBLinker.getInstance.readOneDBField("Factures", "sum(MontantFacture-MontantPaiement)", "WHERE (ParNoClient=" & noClient & " AND IsSouffrance=0);")
        If myPaiements Is Nothing OrElse myPaiements.Length = 0 Then Return False
        If myPaiements(0) = "" Then Return False
        If myPaiements(0) = 0 Then Return False

        Return True
    End Function


    Public Shared Function isPaymentsToDoByClinic(ByVal noClinique As Integer) As Boolean
        Dim myPaiements() As String = DBLinker.getInstance.readOneDBField("Factures", "count(*)", "WHERE (ParNoClinique=" & currentClinic & " AND IsSouffrance=0);")
        If myPaiements Is Nothing OrElse myPaiements.Length = 0 Then Return False
        If myPaiements(0) = "" Then Return False
        If myPaiements(0) = 0 Then Return False

        Return True
    End Function
#End Region

    Public Sub toggleSouffrance()
        _IsSouffrance = Not _IsSouffrance
        DBLinker.getInstance.updateDB("Factures", "IsSouffrance='" & _IsSouffrance & "'", "NoFacture", noFacture, False)
    End Sub

    Public Shared Function joinBills(ByVal bills As Collections.Generic.List(Of Bill)) As String
        'Vérification si les factures peuvent être unifiées
        Dim acceptedMSG As String = ""
        Dim i, PayeurClient, PayeurKP, PayeurUser, EntiteLie, totalToUnify As Integer
        Dim entiteType As FacturationBox.DedicatedType
        Dim firstBillChecked As Boolean = True
        Dim noFactureRef As String = ""
        Dim typeFacture As String = ""
        Dim noFolder As Integer = 0
        For Each CurBill As Bill In bills
            If CurBill.noBillTransfered > 0 Then
                acceptedMSG = "Impossible d'unifier ces factures : au moins une de ces factures est déjà liée à un autre"
                Exit For
            End If

            If firstBillChecked Then
                PayeurClient = CurBill.parNoClient
                PayeurKP = CurBill.parNoKP
                PayeurUser = CurBill.parNoUser
                EntiteLie = CurBill.entityLinked
                entiteType = CurBill.entityType
                noFolder = CurBill.noFolder
                typeFacture = CurBill.type
            Else
                If PayeurClient <> CurBill.parNoClient Or PayeurKP <> CurBill.parNoKP Or PayeurUser <> CurBill.parNoUser Or EntiteLie <> CurBill.entityLinked Or entiteType <> CurBill.entityType Or (typeFacture.StartsWith(CurBill.type) = False And CurBill.type.StartsWith(typeFacture) = False) Then
                    acceptedMSG = "Impossible d'unifier ces factures : toutes les factures doivent avoir les mêmes payeurs, être liées à la même entité et avoir le même type de facturation"
                    Exit For
                End If
            End If

            noFactureRef &= "§" & CurBill.noFacture

            firstBillChecked = False
            totalToUnify += 1
        Next

        If totalToUnify < 2 And acceptedMSG = "" Then acceptedMSG = "Veuillez sélectionner au moins deux factures à unifier"

        'Unification refusée
        If acceptedMSG <> "" Then Return acceptedMSG

        noFactureRef = noFactureRef.Substring(1)

        'Ajout de la nouvelle facture unifée
        Dim newFactureNo As Integer
        Select Case entiteType
            Case FacturationBox.DedicatedType.Client
                newFactureNo = createFacturation(EntiteLie, 0, "Facture unifiée", Date.Today, noFolder, , , noFactureRef, , , , , , , PayeurClient, PayeurKP, PayeurUser, 0)
            Case FacturationBox.DedicatedType.KP
                newFactureNo = createFacturation(0, 0, "Facture unifiée", Date.Today, , , , noFactureRef, , , , , EntiteLie, , PayeurClient, PayeurKP, PayeurUser, 0)
            Case FacturationBox.DedicatedType.User
                newFactureNo = createFacturation(0, 0, "Facture unifiée", Date.Today, , , , noFactureRef, , , , , , EntiteLie, PayeurClient, PayeurKP, PayeurUser, 0)
        End Select

        ''Modifications des sous-factures
        Dim subFactures() As String = noFactureRef.Split(New Char() {"§"})
        'Build where STRING
        Dim whereStr As String
        For i = 1 To subFactures.GetUpperBound(0)
            whereStr &= " OR (NoFacture = " & subFactures(i) & ")"
        Next i

        Dim curNoFacture As Integer = subFactures(0)
        DBLinker.getInstance.updateDB("Factures", "NoFactureTransfere = " & newFactureNo, "NoFacture", curNoFacture & whereStr, False)
        DBLinker.getInstance.updateDB("StatFactures", "NoFactureTransfere = " & newFactureNo, "NoFacture", curNoFacture & whereStr, False)

        Return ""
    End Function

    Public Sub saveDescription()
        DBLinker.getInstance.updateDB("Factures", "Description='" & description.Replace("'", "''") & "'", "NoFacture", noFacture, False)
        DBLinker.getInstance.updateDB("StatFactures", "Description='" & description.Replace("'", "''") & "'", "NoFacture", noFacture, False)
    End Sub

    Private Sub setAmounts(ByRef myBill As Bill)
        Dim MontantFactures(,), MontantPaiements(,), whereString As String
        Dim i As Integer
        Dim MontantFacture, MontantPaiement, MontantPaiementKP, MontantPaiementUser, montantPaiementClinique As Double

        If myBill.noBillRef = "" Then
            whereString = "(NoFacture)=" & myBill.noFacture
        Else
            whereString = "(NoFacture) IN (" & Me.getSubBillsLinked() & ")"
        End If

        MontantFactures = DBLinker.getInstance.readDB("StatFactures", "MontantFacture, NoEntitePayeur,Taxe1,Taxe2", "WHERE (" & whereString & ");")
        If MontantFactures Is Nothing OrElse MontantFactures.Length = 0 Then GoTo TryNext

        MontantFacture = 0
        Dim taxe1 As Double = -1, taxe2 As Double = -1
        For i = 0 To MontantFactures.GetUpperBound(1)
            Select Case MontantFactures(1, i)
                Case 1
                    MontantFacture += CDbl(MontantFactures(0, i))
                    MontantFacture = Math.Round(MontantFacture, 4)
                Case 2
                    amountBilledToKP += CDbl(MontantFactures(0, i))
                    amountBilledToKP = Math.Round(amountBilledToKP, 4)
                Case 3
                    amountBilledToUser += CDbl(MontantFactures(0, i))
                    amountBilledToUser = Math.Round(amountBilledToUser, 4)
                Case 4
                    amountBilledToClinic += CDbl(MontantFactures(0, i))
                    amountBilledToClinic = Math.Round(amountBilledToClinic, 4)
            End Select
            If taxe1 = -1 Then
                taxe1 = MontantFactures(2, i)
                taxe2 = MontantFactures(3, i)
            Else
                If taxe1 <> MontantFactures(2, i) OrElse taxe2 <> MontantFactures(3, i) Then
                    Me.taxe1 = -1
                    Me.taxe2 = -1
                End If
            End If
        Next i

        myBill.amountBilledToClient = MontantFacture
TryNext:

        MontantPaiements = DBLinker.getInstance.readDB("StatPaiements", "MontantPaiement, NoEntitePayeur", "WHERE (" & whereString & ");")
        If MontantPaiements Is Nothing OrElse MontantPaiements.Length = 0 Then Exit Sub

        For i = 0 To MontantPaiements.GetUpperBound(1)
            Select Case MontantPaiements(1, i)
                Case 1
                    MontantPaiement += MontantPaiements(0, i)
                Case 2
                    MontantPaiementKP += MontantPaiements(0, i)
                Case 3
                    MontantPaiementUser += MontantPaiements(0, i)
                Case 4
                    montantPaiementClinique += MontantPaiements(0, i)
            End Select
        Next i

        myBill.amountPaidByClient = MontantPaiement
        myBill.amountPaidByKP = MontantPaiementKP
        myBill.amountPaidByUser = MontantPaiementUser
        myBill.amountPaidByClinic = montantPaiementClinique
    End Sub

    Public Function getJoinableBills(Optional ByVal recuIsRequired As Boolean = True) As System.Collections.Generic.IList(Of Bill)
        Dim joinableBills As New System.Collections.Generic.List(Of Bill)

        Dim noBills() As Integer = getJoinableNoBills(recuIsRequired)
        If noBills Is Nothing OrElse noBills.Length = 0 Then Return Nothing

        For i As Integer = 0 To noBills.Length - 1
            joinableBills.Add(New Bill(noBills(i)))
        Next i

        Return joinableBills
    End Function

    Public Function getJoinableNoBills(Optional ByVal recuIsRequired As Boolean = True) As Integer()
        Dim joinableBills() As Integer
        Dim recuCond As String = " (SELECT COUNT(*) FROM FacturesRecusLeft WHERE FacturesRecusLeft.NoFacture=StatFactures.NoFacture)>0 AND "
        Dim bills() As String = DBLinker.getInstance.readOneDBField("StatFactures", "DISTINCT NoFacture", "WHERE " & IIf(recuIsRequired, recuCond, "") & " NoFactureTransfere IS NULL AND ParNoClient " & IIf(Me.parNoClient = 0, "IS NULL", "=" & Me.parNoClient) & " AND ParNoKP " & IIf(Me.parNoKP = 0, "IS NULL", "=" & Me.parNoKP) & " AND ParNoUser " & IIf(Me.parNoUser = 0, "IS NULL", "=" & Me.parNoUser) & " AND NoClient " & IIf(Me.noClient = 0, "IS NULL", "=" & Me.noClient) & " AND NoFolder " & IIf(Me.noFolder = 0, "IS NULL", "=" & Me.noFolder) & " AND NoKP " & IIf(Me.noKP = 0, "IS NULL", "=" & Me.noKP) & " AND NoUserFacture " & IIf(Me.noUserFacture = 0, "IS NULL", "=" & Me.noUserFacture) & " AND (TypeFacture LIKE '" & Me.type.Replace("'", "''") & "%' OR '" & Me.type.Replace("'", "''") & "' LIKE TypeFacture + '%') AND (SELECT TOP 1 NoAction FROM StatFactures AS SF2 WHERE SF2.NoFacture=StatFactures.NoFacture ORDER BY NoStat DESC)<>20 AND (SELECT COUNT(*) FROM Factures F WHERE F.NoFacture=StatFactures.NoFacture AND F.IsSouffrance = 1)=0")
        If bills Is Nothing OrElse bills.Length = 0 Then Return Nothing

        joinableBills = Array.ConvertAll(bills, New Converter(Of String, Integer)(AddressOf convertStringToInt))

        Return joinableBills
    End Function

    Private Function convertStringToInt(ByVal input As String) As Integer
        Return Integer.Parse(input)
    End Function

    Public Function getLastTaxes() As Double()
        Dim taxes(2) As Double
        taxes(0) = 0
        taxes(1) = 0

        Dim lastTaxes(,) As String = DBLinker.getInstance.readDB("StatFactures", "Taxe1,Taxe2", "WHERE (Taxe1<>0 OR Taxe2<>0) AND NoFacture=" & noFacture)
        If lastTaxes IsNot Nothing AndAlso lastTaxes.Length <> 0 Then
            taxes(0) = lastTaxes(0, 0)
            taxes(1) = lastTaxes(1, 0)
        End If

        Return taxes
    End Function

    Private Sub getBill(ByRef myBill As Bill, ByVal whereField As String, ByVal whereCondition As Integer)
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim factures(,) As String

        myBill.noFacture = 0
        myBill.takenFromStats = False

        If whereCondition = 0 Then Exit Sub

        factures = DBLinker.getInstance.readDB("Factures", "*", "WHERE ((" & whereField & ")=" & whereCondition & ");")
        If Not factures Is Nothing AndAlso factures.Length <> 0 Then
            With myBill
                .noFacture = factures(0, 0)
                .noUser = factures(1, 0)
                .dateFacture = factures(2, 0)
                .noFolder = IIf(factures(3, 0) = "", 0, factures(3, 0))
                .noClient = IIf(factures(4, 0) = "", 0, factures(4, 0))
                .type = factures(6, 0)
                .description = factures(8, 0)
                .noVisite = IIf(factures(9, 0) = "", 0, factures(9, 0))
                .noPret = IIf(factures(10, 0) = "", 0, factures(10, 0))
                .noBillRef = factures(11, 0)
                .noVente = IIf(factures(12, 0) = "", 0, factures(12, 0))
                .taxe1 = factures(13, 0)
                .taxe2 = factures(14, 0)
                .noKP = IIf(factures(15, 0) = "", 0, factures(15, 0))
                .noUserFacture = IIf(factures(16, 0) = "", 0, factures(16, 0))
                .parNoKP = IIf(factures(17, 0) = "", 0, factures(17, 0))
                .parNoClient = IIf(factures(18, 0) = "", 0, factures(18, 0))
                .parNoUser = IIf(factures(19, 0) = "", 0, factures(19, 0))
                .noBillTransfered = IIf(factures(23, 0) = "", 0, factures(23, 0))
                .comments = ""

                If myBill.noBillRef <> "" Then
                    setAmounts(myBill)
                Else
                    .amountBilledToClient = factures(5, 0)
                    .amountBilledToKP = factures(20, 0)
                    .amountBilledToUser = factures(21, 0)
                    .amountBilledToClinic = factures(22, 0)
                    .amountPaidByClient = factures(7, 0)
                    .amountPaidByKP = factures(24, 0)
                    .amountPaidByUser = factures(25, 0)
                    .amountPaidByClinic = factures(26, 0)
                End If

                .parNoClinique = If(factures(27, 0) = "", 0, Integer.Parse(factures(27, 0)))
                ._IsSouffrance = factures(28, 0)
                .taxesApplication = If(factures(29, 0) = "", 0, Integer.Parse(factures(29, 0)))

                'REM Removed for faster speed.
                'Dim StatFactures(,) As String = DBLinker.GetInstance.ReadDB("StatFactures", "DateHeureCreation", "WHERE ((" & WhereField & ")=" & WhereCondition & ") ORDER BY NoStat DESC;")
                .dateHeureLastConfirmed = LIMIT_DATE 'StatFactures(0, 0)
            End With
        Else
            factures = DBLinker.getInstance.readDB("StatFactures", "*", "WHERE ((" & whereField & ")=" & whereCondition & ") ORDER BY NoStat DESC;")

            If Not factures Is Nothing AndAlso factures.Length <> 0 Then
                With myBill
                    .takenFromStats = True
                    .noUser = factures(1, 0)
                    .dateHeureLastConfirmed = factures(2, 0)
                    .noFacture = factures(3, 0)
                    .noFolder = IIf(factures(4, 0) = "", 0, factures(4, 0))
                    .noClient = IIf(factures(5, 0) = "", 0, factures(5, 0))
                    .type = factures(7, 0)
                    .description = factures(8, 0)
                    .noVisite = IIf(factures(9, 0) = "", 0, factures(9, 0))
                    .noPret = IIf(factures(10, 0) = "", 0, factures(10, 0))
                    .noBillRef = factures(11, 0)
                    .noVente = IIf(factures(12, 0) = "", 0, factures(12, 0))
                    .taxe1 = factures(13, 0)
                    .taxe2 = factures(14, 0)
                    .dateFacture = factures(16, 0)
                    .parNoKP = IIf(factures(17, 0) = "", 0, factures(17, 0))
                    .parNoClient = IIf(factures(18, 0) = "", 0, factures(18, 0))
                    .parNoUser = IIf(factures(19, 0) = "", 0, factures(19, 0))
                    .noKP = IIf(factures(20, 0) = "", 0, factures(20, 0))
                    .noUserFacture = IIf(factures(21, 0) = "", 0, factures(21, 0))
                    .noBillTransfered = IIf(factures(22, 0) = "", 0, factures(22, 0))
                    .parNoClinique = IIf(factures(24, 0) = "", 0, factures(24, 0))
                    ._IsSouffrance = False
                    .taxesApplication = If(factures(26, 0) = "", 0, Integer.Parse(factures(26, 0)))

                    setAmounts(myBill)
                End With
            End If
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
    End Sub

    Public Sub loadingData(ByVal noFacture As Integer)
        noReceiptsReadDB = False
        noReceiptsReadReal = False
        getBill(Me, "NoFacture", noFacture)
    End Sub

    Public Sub loadingData(ByVal noFolder As Integer, ByVal noVisite As Integer)
        noReceiptsReadDB = False
        noReceiptsReadReal = False
        getBill(Me, "NoVisite", noVisite)
    End Sub

    Public Sub loadingData(ByVal statDataRow As DataRow)
        noReceiptsReadDB = False
        noReceiptsReadReal = False
        With statDataRow
            Me.dateFacture = .Item("DateFacture")
            Me.dateHeureLastConfirmed = .Item("DateHeureCreation")
            Me.type = .Item("TypeFacture")
            If .Item("NoClient") Is DBNull.Value Then
                Me.noClient = 0
            Else
                Me.noClient = .Item("NoClient")
            End If
            Me.noFacture = .Item("NoFacture")
            Me.noBillRef = .Item("NoFactureRef")
            If .Item("NoFactureTransfere") Is DBNull.Value OrElse .Item("NoFactureTransfere").ToString = "" Then
                Me.noBillTransfered = 0
            Else
                Me.noBillTransfered = .Item("NoFactureTransfere")
            End If
            If .Item("NoFolder") Is DBNull.Value Then
                Me.noFolder = 0
            Else
                Me.noFolder = .Item("NoFolder")
            End If
            If .Item("NoKP") Is DBNull.Value Then
                Me.noKP = 0
            Else
                Me.noKP = .Item("NoKP")
            End If
            If .Item("NoPret") Is DBNull.Value Then
                Me.noPret = 0
            Else
                Me.noPret = .Item("NoPret")
            End If
            If .Item("NoUserFacture") Is DBNull.Value Then
                Me.noUserFacture = 0
            Else
                Me.noUserFacture = .Item("NoUserFacture")
            End If
            Me.noUser = .Item("NoUser")
            If .Item("NoVente") Is DBNull.Value Then
                Me.noVente = 0
            Else
                Me.noVente = .Item("NoVente")
            End If
            If .Item("NoVisite") Is DBNull.Value Then
                Me.noVisite = 0
            Else
                Me.noVisite = .Item("NoVisite")
            End If
            If .Item("ParNoClient") Is DBNull.Value Then
                Me.parNoClient = 0
            Else
                Me.parNoClient = .Item("ParNoClient")
            End If
            If .Item("ParNoKP") Is DBNull.Value Then
                Me.parNoKP = 0
            Else
                Me.parNoKP = .Item("ParNoKP")
            End If
            If .Item("ParNoUser") Is DBNull.Value Then
                Me.parNoUser = 0
            Else
                Me.parNoUser = .Item("ParNoUser")
            End If
            Me.taxe1 = .Item("Taxe1")
            Me.taxe2 = .Item("Taxe2")
            Me.description = .Item("Description")
            If .Item("ParNoClinique") Is DBNull.Value Then
                Me.parNoClinique = 0
            Else
                Me.parNoClinique = .Item("ParNoClinique")
            End If
            If .Table.Columns.IndexOf("IsSouffrance") = -1 Then
                Me._IsSouffrance = False
            Else
                Me._IsSouffrance = .Item("IsSouffrance")
            End If

            setAmounts(Me)
        End With
    End Sub

    Private Function getBaseType() As String
        If noVente <> 0 Then
            Return "Vente"
        ElseIf noPret <> 0 Then
            Return "Prêt"
        ElseIf noVisite <> 0 Then
            Return "Service:" & type.Replace(": ", ":")
        End If

        Return "Autre:" & type
    End Function

    'Filter = (C|K|U) --> C:Client, K:Personne/Organisme clé, U: Utilisateur
    Public Function generateReceipt(Optional ByVal filter As String = "") As String
        Dim tmp As Long = Date.Now.Ticks

        'Demande de joindre les bills s'il y en a
        Dim joined As Boolean = False
        Dim joinableBills As System.Collections.Generic.List(Of Bill) = getJoinableBills()
        If joinableBills IsNot Nothing AndAlso joinableBills.Count > 1 Then
            If MessageBox.Show("Il existe des factures nécessitant un reçu pouvant être unifiés à celles-ci (N°:" & Me.noFacture & ")." & vbCrLf & "Voulez-vous les joindre ?", "Factures joignables", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim erreurMsg As String = joinBills(joinableBills)
                If erreurMsg <> "" Then
                    MessageBox.Show(erreurMsg, "Impossible de joindre")
                Else
                    joined = True
                End If
            End If
        End If

        'Si une facture unifiée a été crée, Crée un reçu pour la facture unifiée
        If joined Then
            Dim joinedBill As Bill
            Me.loadingData(Me.noFacture)
            joinedBill = New Bill(Me.noBillTransfered)
            Return joinedBill.generateReceipt(filter)
        End If

        Dim noRecu As String = ""
        Dim noNoRecu As Integer = createReceipt(filter, noRecu)
        If noNoRecu = 0 Then
            MessageBox.Show("Le reçu pour cette facture a déjà été généré", "Reçu déjà généré")
            Return ""
        End If

        Dim myRapport As Report = createReport(noNoRecu, True)
        myRapport.generateHTML()
        Dim tmp2 As Long = Date.Now.Ticks
        printReceipt(myRapport)
        tmp2 -= Date.Now.Ticks

        myRapport = createReport(noNoRecu, False)

        If filter = "C" Then
            Dim myPath As String = "Clients\" & parNoClient & "\Comm"
            If IO.Directory.Exists(appPath & bar(appPath) & myPath) = False Then IO.Directory.CreateDirectory(appPath & bar(appPath) & myPath)

            Dim newFileName As String = Directory.getNewFileName(appPath & bar(appPath) & myPath, noNoRecu & ".HTMLRPT")

            myRapport.saveToFile(appPath & bar(appPath) & myPath & "\" & newFileName)

            addingComm(Me.parNoClient, 0, True, "Reçu:" & getBaseType(), "Reçu émis", Date.Today, "N° du reçu : " & noRecu, Me.noFolder, "REPORT|" & newFileName)
        End If
        If filter = "K" Then
            Dim myPath As String = "KP\" & parNoKP & "\Comm"
            If IO.Directory.Exists(appPath & bar(appPath) & myPath) = False Then IO.Directory.CreateDirectory(appPath & bar(appPath) & myPath)

            Dim newFileName As String = Directory.getNewFileName(appPath & bar(appPath) & myPath, noNoRecu & ".HTMLRPT")

            myRapport.saveToFile(appPath & bar(appPath) & myPath & "\" & newFileName)

            addingCommKP(Me.parNoKP, 0, True, "Reçu:" & getBaseType(), "Reçu émis", Date.Today, "N° du reçu : " & noRecu, "REPORT|" & newFileName)
        End If

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Time for generate a Recu:" & (Date.Now.Ticks - tmp) / 10000000 & "s which " & tmp2 / 10000000 & "s is for printing"
        Return noNoRecu
    End Function

    Public Function nbPayers() As Byte
        Dim totalPayeur As Integer = 0
        If Me.parNoClient > 0 Then totalPayeur += 1
        If Me.parNoClinique > 0 Then totalPayeur += 1
        If Me.parNoKP > 0 Then totalPayeur += 1
        If Me.parNoUser > 0 Then totalPayeur += 1

        Return totalPayeur
    End Function

    Public Shared Function askPourcentages(ByVal parNoClient As Integer, ByVal parNoKP As Integer, ByVal parNoUser As Integer, ByRef pClient As Double, ByRef pKP As Double, ByRef pUser As Double) As Boolean
        Dim done As Boolean = False

        If parNoClient > 0 Then done = askPourcentage(FacturationBox.DedicatedType.Client, pClient)
        If parNoKP > 0 Then done = done AndAlso askPourcentage(FacturationBox.DedicatedType.KP, pKP)
        If parNoUser > 0 Then done = done AndAlso askPourcentage(FacturationBox.DedicatedType.User, pUser)
        If done = False Then Return False

        If pClient + pKP + pUser <> 100 Then
            MessageBox.Show("La somme de tous les pourcentages doivent être égale à 100%", "Pourcentage erroné", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return askPourcentages(parNoClient, parNoKP, parNoUser, pClient, pKP, pUser)
        End If

        Return True
    End Function

    Private Shared Function askPourcentage(ByVal dt As FacturationBox.DedicatedType, ByRef pourcent As Double) As Boolean
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.currencyBox = True
        myInputBoxPlus.nbDecimals = 4
        myInputBoxPlus.blockOnMaximum = True
        myInputBoxPlus.blockOnMinimum = True
        myInputBoxPlus.minimum = 0
        myInputBoxPlus.maximum = 100

        Dim et As String = ""
        Select Case dt
            Case FacturationBox.DedicatedType.Client
                et = "le client"
            Case FacturationBox.DedicatedType.KP
                et = "la personne/organisme clé"
            Case FacturationBox.DedicatedType.User
                et = "l'utilisateur"
        End Select

        Dim tmpPourcent As Double = -1
        Dim answer As String = myInputBoxPlus("Veuillez entrer le pourcentage payé par " & et, "Pourcentage d'un payeur", "0")
        If answer = "" OrElse Double.TryParse(answer, tmpPourcent) = False Then Return False


        pourcent = tmpPourcent
        Return True
    End Function

    Public Function getFilterPayeur(Optional ByRef filter As String = "", Optional ByVal reason As String = "pour qui créé le reçu", Optional ByVal rejectedFilters As ArrayList = Nothing) As String
        If rejectedFilters Is Nothing Then rejectedFilters = New ArrayList

        Dim answer As Byte
        Dim myMessageBox1 As New MsgBox1
        If Me.parNoClient > 0 And Me.parNoKP > 0 And Me.parNoUser > 0 And rejectedFilters.IndexOf("C") < 0 And rejectedFilters.IndexOf("K") < 0 And rejectedFilters.IndexOf("U") < 0 Then
            answer = myMessageBox1("Veuillez sélectionner le payeur " & reason, "Sélection du payeur", 3, "Client", "Personne / organisme clé", "Utilisateur", False)
            Select Case answer
                Case 1
                    filter = "C"
                Case 2
                    filter = "K"
                Case 3
                    filter = "U"
            End Select
        End If

        If Me.parNoClient > 0 And Me.parNoKP > 0 And rejectedFilters.IndexOf("C") < 0 And rejectedFilters.IndexOf("K") < 0 And (Me.parNoUser = 0 Or (Me.parNoUser > 0 And rejectedFilters.IndexOf("U") >= 0)) Then
            answer = myMessageBox1("Veuillez sélectionner le payeur " & reason, "Sélection du payeur", 2, "Client", "Personne / organisme clé", False)
            Select Case answer
                Case 1
                    filter = "C"
                Case 2
                    filter = "K"
            End Select
        End If

        If Me.parNoClient > 0 And rejectedFilters.IndexOf("C") < 0 And (Me.parNoKP = 0 Or (Me.parNoKP > 0 And rejectedFilters.IndexOf("K") >= 0)) And Me.parNoUser > 0 Then
            answer = myMessageBox1("Veuillez sélectionner le payeur " & reason, "Sélection du payeur", 2, "Client", "Utilisateur", False)
            Select Case answer
                Case 1
                    filter = "C"
                Case 2
                    filter = "U"
            End Select
        End If

        If (Me.parNoClient = 0 Or (Me.parNoClient > 0 And rejectedFilters.IndexOf("C") >= 0)) And Me.parNoKP > 0 And rejectedFilters.IndexOf("K") < 0 And Me.parNoUser > 0 Then
            answer = myMessageBox1("Veuillez sélectionner le payeur " & reason, "Sélection du payeur", 2, "Personne / organisme clé", "Utilisateur", False)
            Select Case answer
                Case 1
                    filter = "K"
                Case 2
                    filter = "U"
            End Select
        End If

        If Me.parNoClient > 0 And rejectedFilters.IndexOf("C") < 0 And (Me.parNoKP = 0 Or (Me.parNoKP > 0 And rejectedFilters.IndexOf("K") >= 0)) And (Me.parNoUser = 0 Or (Me.parNoUser > 0 And rejectedFilters.IndexOf("U") >= 0)) Then filter = "C"
        If (Me.parNoClient = 0 Or (Me.parNoClient > 0 And rejectedFilters.IndexOf("C") >= 0)) And Me.parNoKP > 0 And rejectedFilters.IndexOf("K") < 0 And Me.parNoUser = 0 Then filter = "K"
        If (Me.parNoClient = 0 Or (Me.parNoClient > 0 And rejectedFilters.IndexOf("C") >= 0)) And (Me.parNoKP = 0 Or (Me.parNoKP > 0 And rejectedFilters.IndexOf("K") >= 0)) And Me.parNoUser > 0 And rejectedFilters.IndexOf("U") < 0 Then filter = "U"

        Return filter
    End Function

    Private Function createReceipt(Optional ByRef filter As String = "", Optional ByRef noRecu As String = "") As Integer
        If noFacture = 0 Then Return 0
        If isReceiptToDo() = False Then Return 0

        Dim myNoRecu As Integer = genUniqueNo()
        Dim preNoRecu As String = PreferencesManager.getGeneralPreferences()("PrefixNoRecu")
        noRecu = preNoRecu & myNoRecu
        Dim WhereStr, WhereOperator, whereCondition As String
        If filter = "" Then
            Dim rejected As New ArrayList
            If isReceiptToDo("C") = False Then rejected.Add("C")
            If isReceiptToDo("K") = False Then rejected.Add("K")
            If isReceiptToDo("U") = False Then rejected.Add("U")

            filter = getFilterPayeur(filter, , rejected)
        End If

        WhereStr = " AND NoEntitePayeur=" & CInt(BillsManager.translatePayingEntity(filter)) & " AND (NoNoRecu IS NULL) "

        If Me.noBillRef <> "" Or Me.noBillTransfered > 0 Then
            whereCondition = "(" & getAllBillsLinked() & ")"
            WhereOperator = " IN "
        Else
            whereCondition = Me.noFacture
            WhereOperator = "="
        End If

        Dim noNoRecu As Integer = DBHelper.addItemToADBList("ListeNoRecus", "NoRecu", noRecu, "noNoRecu")
        DBLinker.getInstance.updateDB("StatPaiements", "NoNoRecu=" & noNoRecu & ",DateRecu='" & DateFormat.getTextDate(Date.Today) & "'", "NoFacture", whereCondition & WhereStr, False, WhereOperator)

        Return noNoRecu
    End Function

    Public Function getNoReceiptsString(Optional ByVal realNoReceipt As Boolean = False) As String
        If realNoReceipt Then
            If noReceiptsReadReal Then Return noReceiptsStringReal
            noReceiptsReadReal = True
        Else
            If noReceiptsReadDB Then Return noReceiptsStringDB
            noReceiptsReadDB = True
        End If

        Dim WhereCondition, whereOperator As String
        If Me.noBillRef <> "" Or Me.noBillTransfered > 0 Then
            WhereCondition = "(" & getAllBillsLinked() & ")"
            whereOperator = " IN "
        Else
            WhereCondition = Me.noFacture
            whereOperator = "="
        End If

        Dim noRecus() As String = DBLinker.getInstance.readOneDBField("StatPaiements" & IIf(realNoReceipt, " INNER JOIN ListeNoRecus ON StatPaiements.NoNoRecu=ListeNoRecus.NoNoRecu", ""), "DISTINCT " & IIf(realNoReceipt, "NoRecu", "StatPaiements.NoNoRecu"), "WHERE NoFacture" & whereOperator & WhereCondition, True, True)
        If noRecus Is Nothing OrElse noRecus.Length = 0 Then Return ""

        Dim noRecusString As String = String.Join(",", noRecus)
        If realNoReceipt Then
            noReceiptsStringReal = noRecusString
        Else
            noReceiptsStringDB = noRecusString
        End If

        Return noRecusString
    End Function

    Public Function getAllBillsLinked() As String
        Return getAllBillsLinked(False) 'NosFactures
    End Function

    Public Function getAllBillsLinked(ByVal grouped As Boolean) As String
        Return _getBillsLinked(grouped, "fn_getAllNoFactures")
    End Function

    Public Function getSubBillsLinked() As String
        If Me.noBillRef = "" Then Return Nothing

        Return getSubBillsLinked(False)
    End Function

    Public Function getSubBillsLinked(ByVal grouped As Boolean) As String
        If Me.noBillRef = "" Then Return Nothing

        Return _getBillsLinked(grouped, "fn_getSubNoFactures")
    End Function

    Private Function _getBillsLinked(ByVal grouped As Boolean, ByVal sqlFunction As String) As String
        If grouped AndAlso linkedNoStringGrouped <> "" Then Return linkedNoStringGrouped
        If Not grouped AndAlso linkedNoString <> "" Then Return linkedNoString

        Dim myNos As DataSet = DBLinker.getInstance.readDBForGrid("SELECT * FROM " & sqlFunction & "(" & Me.noFacture.ToString & ")")

        'REM Grouped bills aren't well represented now
        Dim nosString As String = Me.noFacture.ToString
        Dim nosStringGrouped As String = Me.noFacture.ToString
        Dim i, j As Integer
        For i = 0 To myNos.Tables.Count - 1
            nosStringGrouped &= " ("
            For j = 0 To myNos.Tables(i).Rows.Count - 1
                If j > 0 Then nosStringGrouped &= ","
                nosString &= ","

                nosString &= myNos.Tables(i).Rows(j)(0).ToString
                nosStringGrouped &= myNos.Tables(i).Rows(j)(0).ToString
            Next j
            nosStringGrouped &= ")"
        Next i

        linkedNoStringGrouped = nosStringGrouped
        linkedNoString = nosString

        If grouped Then Return nosStringGrouped

        Return nosString
    End Function

    Private Function createReport(ByVal noNoRecu As String, ByVal firstTime As Boolean) As Report
        Dim duplicata As String = ""
        If firstTime = False Then duplicata = "Duplicata"
        Dim filtering As New FilteringComposite
        Dim fp As New FilteringPassive(duplicata)
        filtering.add(fp)
        Dim fifd As New FilteringInputFieldData()
        fifd.currentReturn.filtrageTexte = "" 'No need, because it is showed in header elsewhere
        Dim noRecus() As String = noNoRecu.Split(New Char() {","})
        Dim whereStr As String = ""
        For i As Integer = 0 To noRecus.Length - 1
            whereStr &= " OR StatPaiements.NoNoRecu =" & noRecus(i)
        Next i
        If whereStr <> "" Then whereStr = "(" & whereStr.Substring(4) & ")"
        fifd.currentReturn.whereStr = whereStr

        fifd.currentReturn.persoChoice = noNoRecu
        filtering.add(fifd)

        Dim customVars As New Hashtable
        customVars.Add("###CLINICAPATH###", appPath & bar(appPath))
        customVars.Add("###TAUXAUTONOMIEMAX###", PreferencesManager.getGeneralPreferences()("NbEvalTo100TauxAutonomie"))
        customVars.Add("###TAX1NAME###", PreferencesManager.getGeneralPreferences()("tax1Name"))
        customVars.Add("###TAX2NAME###", PreferencesManager.getGeneralPreferences()("tax2Name"))
        Dim textToReplaceAbsNotMotivatedInReceipt As String = PreferencesManager.getGeneralPreferences()("textToReplaceAbsNotMotivatedInReceipt")
        If PreferencesManager.getGeneralPreferences()("changeAbsenceTypeForSpecificText") = False Then
            textToReplaceAbsNotMotivatedInReceipt = String.Empty
        End If
        customVars.Add("###ABSNOTMOTIVATEDTYPE###", textToReplaceAbsNotMotivatedInReceipt)


        Dim myRapport As Report = ReportsManager.getInstance.createReport("Reçu", filtering, customVars)
        Return myRapport
    End Function

    Public Sub printReceipt(Optional ByVal curReport As Report = Nothing)
        If curReport Is Nothing Then
            Dim noRecus As String = Me.getNoReceiptsString()
            curReport = createReport(noRecus, False)
        End If

        curReport.print(False, True)
    End Sub

    Public Function clone() As Object Implements System.ICloneable.Clone
        Dim newFacture As New Bill
        newFacture.noFacture = Me.noFacture
        newFacture.dateFacture = Me.dateFacture
        newFacture.noClient = Me.noClient
        newFacture.noFolder = Me.noFolder
        newFacture.noPret = Me.noPret
        newFacture.noVente = Me.noVente
        newFacture.noVisite = Me.noVisite
        newFacture.amountBilledToClient = Me.amountBilledToClient
        newFacture.amountPaidByClient = Me.amountPaidByClient
        newFacture.type = Me.type.Clone()
        newFacture.description = Me.description.Clone()
        newFacture.noBillRef = Me.noBillRef.Clone()
        newFacture.noBillTransfered = Me.noBillTransfered
        newFacture.taxe1 = Me.taxe1
        newFacture.taxe2 = Me.taxe2
        newFacture.comments = Me.comments.Clone()
        newFacture.noUser = Me.noUser
        newFacture.takenFromStats = Me.takenFromStats
        newFacture.noKP = Me.noKP
        newFacture.noUserFacture = Me.noUserFacture
        newFacture.parNoClient = Me.parNoClient
        newFacture.parNoKP = Me.parNoKP
        newFacture.parNoUser = Me.parNoUser
        newFacture.amountBilledToClinic = Me.amountBilledToClinic
        newFacture.amountBilledToKP = Me.amountBilledToKP
        newFacture.amountBilledToUser = Me.amountBilledToUser
        newFacture.amountPaidByKP = Me.amountPaidByKP
        newFacture.amountPaidByUser = Me.amountPaidByUser
        newFacture.parNoClinique = Me.parNoClinique
        newFacture.amountPaidByClinic = Me.amountPaidByClinic

        Return CType(newFacture, Object)
    End Function

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
