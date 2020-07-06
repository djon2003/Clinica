Public Class ItemBorrowed
    Inherits Item

    Private _returningDate As Date = LIMIT_DATE
    Private _costRefundPercentage, _depositRefundPercentage, _costAmountBy, _depositAmount, _cost, _amountRefund As Double
    Private _costRepetitionBy As Equipment.costAmountFrequency
    Private _VerifiedByTRP, _isRefund, _isReturned As Boolean
    Private _description As String = ""

#Region "Propriétés"
    Public Property costRepetitionBy() As Equipment.costAmountFrequency
        Get
            Return _costRepetitionBy
        End Get
        Set(ByVal value As Equipment.costAmountFrequency)
            _costRepetitionBy = value
        End Set
    End Property

    Public Property costRefundPercentage() As Double
        Get
            Return _costRefundPercentage
        End Get
        Set(ByVal value As Double)
            _costRefundPercentage = value
        End Set
    End Property

    Public Property depositRefundPercentage() As Double
        Get
            Return _depositRefundPercentage
        End Get
        Set(ByVal value As Double)
            _depositRefundPercentage = value
        End Set
    End Property

    Public Property costAmountBy() As Double
        Get
            Return _costAmountBy
        End Get
        Set(ByVal value As Double)
            _costAmountBy = value
        End Set
    End Property

    Protected ReadOnly Property noItemBorrowed() As Integer
        Get
            Return _noItemBoth
        End Get
    End Property

    Public Property description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property returningDate() As Date
        Get
            Return _returningDate
        End Get
        Set(ByVal value As Date)
            _returningDate = value
        End Set
    End Property

    Public Property verifiedByTRP() As Boolean
        Get
            Return _VerifiedByTRP
        End Get
        Set(ByVal value As Boolean)
            _VerifiedByTRP = value
        End Set
    End Property

    Public Property isReturned() As Boolean
        Get
            Return _isReturned
        End Get
        Set(ByVal value As Boolean)
            _isReturned = value
        End Set
    End Property

    Public Property isRefund() As Boolean
        Get
            Return _isRefund
        End Get
        Set(ByVal value As Boolean)
            _isRefund = value
        End Set
    End Property

    Public Property cost() As Double
        Get
            Return _cost
        End Get
        Set(ByVal value As Double)
            _cost = value
        End Set
    End Property

    Public Property amountRefund() As Double
        Get
            Return _amountRefund
        End Get
        Set(ByVal value As Double)
            _amountRefund = value
        End Set
    End Property

    Public Property depositAmount() As Double
        Get
            Return _depositAmount
        End Get
        Set(ByVal value As Double)
            _depositAmount = value
        End Set
    End Property

#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub
#End Region

    Public Overrides Sub delete()
        getEquipement().removeNoItemBorrowed(noItem)

        MyBase.delete()
    End Sub

    Public Overrides Function getTotal() As Double
        Dim total As Double
        If noBill <> 0 Then
            total = MyBase.getTotal()
        Else
            total = profitAmount
            If getEquipement.applyTax Then total = calcAmountPlusTaxes(total)
            If _isRefund = False Then total += _amountRefund
            total = roundAmount(total)
        End If

        Return total
    End Function

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _returningDate = curData("DateRetour")
        _depositAmount = curData("Depot")
        _cost = curData("CoutPret")
        _amountRefund = curData("MontantRembourse")
        _VerifiedByTRP = curData("VerifiedByTRP")
        _isRefund = curData("Rembourse")
        _isReturned = curData("Retourne")
        _description = curData("Remarques").Replace("<BR>", vbCrLf)

        _costRefundPercentage = curData("CostRefundPercentage")
        _depositRefundPercentage = curData("DepositRefundPercentage")
        _costAmountBy = curData("CostAmountBy")
        _costRepetitionBy = curData("CostRepetitionBy")

        MyBase.loadData(data)
    End Sub

    Public Overrides Sub saveData()
        Dim strNoFacture As String = "null"
        If noBill <> 0 Then strNoFacture = noBill
        Dim changedBill As Boolean = False

        If Me.noItemBorrowed = 0 Then
            Dim curEquip As Equipment = Me.getEquipement()
            'Take infos from Equipment
            _costRefundPercentage = curEquip.costRefundPercentage
            _depositRefundPercentage = curEquip.depositRefundPercentage
            _costAmountBy = curEquip.costAmountBy
            _costRepetitionBy = curEquip.costRepetitionBy

            'Write to DB
            DBLinker.getInstance.writeDB("Prets", "CostRepetitionBy,CostAmountBy,DepositRefundPercentage,CostRefundPercentage,Remarques,Retourne,Rembourse,VerifiedByTRP,MontantRembourse,CoutPret,Depot,DateRetour,MontantProfit,NoClient,NoFolder,NoEquipement,NoTRP,NoFacture,NoItem,DateHeure", _
                                        _costRepetitionBy & "," & _costAmountBy.ToString().Replace(",", ".") & "," & _depositRefundPercentage.ToString().Replace(",", ".") & "," & _costRefundPercentage.ToString().Replace(",", ".") & ",'" & description.Replace("'", "''").Replace(vbCrLf, "<BR>") & "','" & isReturned & "','" & isRefund & "','" & verifiedByTRP & "'," & amountRefund.ToString.Replace(",", ".") & "," & cost.ToString.Replace("'", ".") & "," & depositAmount.ToString.Replace(",", ".") & ",'" & DateFormat.getTextDate(returningDate) & "'," & profitAmount.ToString.Replace(",", ".") & "," & Me.noClient & "," & Me.noFolder & "," & Me.noEquipment & "," & Me.noTRP & "," & strNoFacture & ",'" & Me.noItem.Replace("'", "''") & "','" & DateFormat.getTextDate(Me.dateTime) & "'", , , , _noItemBoth)

            recreateBill(amountRefund, getTotal(), , , True)
            changedBill = True

            curEquip.addNoItemBorrowed(noItem)
        Else
            If oldTotal <> getCreatedTotal() Then 'Recrée la facture si le montant a changé
                recreateBill(amountRefund, getCreatedTotal())
                changedBill = True
            End If

            DBLinker.getInstance.updateDB("Prets", "Remarques='" & description.Replace("'", "''").Replace(vbCrLf, "<BR>") & "',Retourne='" & isReturned & "',Rembourse='" & isRefund & "',VerifiedByTRP='" & verifiedByTRP & "',CoutPret=" & cost.ToString.Replace(",", ".") & ",MontantRembourse=" & amountRefund.ToString.Replace(",", ".") & ",Depot=" & depositAmount.ToString.Replace(",", ".") & ",DateRetour='" & DateFormat.getTextDate(returningDate) & "',MontantProfit=" & profitAmount.ToString.Replace(",", ".") & ",NoTRP=" & Me.noTRP & ",NoFacture=" & noBill, "NoPret", noItemBorrowed, False)
            onDataChanged()
        End If


        If autoSendUpdateOnSave Then
            If changedBill Then
                'Sending "AccountsEquipements" is not necessary because "Paiements" causes a refresh too
                InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & noClient & ",-1)")
                InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & noClient & ")")
            Else
                InternalUpdatesManager.getInstance.sendUpdate("AccountsEquipements(" & noClient & "," & noFolder & ")")
            End If
        End If
    End Sub

    Protected Overrides Function getNoItemBothColumnName() As String
        Return "NoPret"
    End Function

    Protected Overrides Function getTableName() As String
        Return "Prets"
    End Function

    Protected Overrides Function getTypeName() As String
        Return "Prêt"
    End Function

    Public Function returnItem(ByVal createReceipt As Boolean) As Boolean
        'Modification de la date de retour
        Dim newDateRetour As Date = LIMIT_DATE
        Dim myDateChoice As New DateChoice()
        myDateChoice.Text = "Date de retour"
        Dim dateChosen As Generic.List(Of Date) = myDateChoice.choose(Date.Now.Year - 1, Date.Now.Year, , , , , , False, , , , , IIf(date1Infdate2(returningDate, Date.Today, True), returningDate, Date.Today), , , , , , Date.Today)
        If dateChosen.Count = 0 Then Return False

        newDateRetour = dateChosen(0)
        Dim myBill As Bill = curBill
        Dim newCoutPret As Double = Me.cost

        'Calculate/confirm new cost
        If _returningDate.Date <> newDateRetour.Date AndAlso Me.costRepetitionBy <> Equipment.costAmountFrequency.Unique Then
            Dim totalDays As Integer = newDateRetour.Subtract(dateTime).TotalDays
            newCoutPret = IIf(Me.costRepetitionBy = Equipment.costAmountFrequency.Day, totalDays, IIf(Me.costRepetitionBy = Equipment.costAmountFrequency.Week, Math.Ceiling(totalDays / 7), 1)) * Me.costAmountBy

            If newCoutPret <> Me.cost Then
                Dim myInputBoxPlus As New InputBoxPlus()
                Dim costChangeText As String = "Le coût du prêt a changé de " & Me.cost.ToString().Replace(".", ",") & "$ à " & newCoutPret.ToString().Replace(".", ",") & "$." & vbCrLf & "Veuillez confirmer ce changement ou modifier le montant."
                Dim chosenCost As String = myInputBoxPlus(costChangeText, "Changement du coût de prêt", newCoutPret.ToString().Replace(".", ","))
                If chosenCost = String.Empty Then Return False

                If Double.TryParse(chosenCost.Replace(",", "."), newCoutPret) = False AndAlso Double.TryParse(chosenCost.Replace(".", ","), newCoutPret) = False Then
                    MessageBox.Show("Le nouveau coût de prêt choisi n'est pas valide." & vbCrLf & " Il doit avoir le format suivant:" & vbCrLf & "###,## ou ###.##" & vbCrLf & vbCrLf & "- # signifie un chiffre" & vbCrLf & "- Il peut y avoir plus de trois chiffres avant les décimales", "Coût de prêt invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If

            Me.cost = roundAmount(newCoutPret)
        End If

        'Adjust billing
        Dim newMontantRembourse As Double = (newCoutPret * Me.costRefundPercentage + Me.depositAmount * Me.depositRefundPercentage) / 100
        Me.profitAmount = (Me.depositAmount + newCoutPret) - newMontantRembourse
        Dim newMF As Double = calcAmountPlusTaxes(profitAmount, myBill.taxe1, myBill.taxe2, myBill.taxesApplication) + newMontantRembourse
        Dim newMP As Double = myBill.getBillPaymentTotal()
        newMontantRembourse = (myBill.getBillTotal - newMF) + newMontantRembourse
        newMF = myBill.getBillTotal() - newMontantRembourse
        If newMontantRembourse > 0 Then newMP -= newMontantRembourse
        'Réajuste la facturation du prêt.. nécessaire, car on change aussi le montant payé et que le 
        If newMF <> myBill.getBillTotal() Then recreateBill(0, newMF, newMP, "Modification dû au retour du prêt")

        'Create receipt
        If createReceipt Then myBill.generateReceipt("C")

        'Modifie l'objet de prêt
        Me.returningDate = newDateRetour
        Me.isReturned = True
        Me.isRefund = True
        Me.amountRefund = IIf(newMontantRembourse < 0, 0, newMontantRembourse)

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance.dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        'Indique à l'équipement que l'item n'est plus prêté
        getEquipement().removeNoItemBorrowed(Me.noItem)

        'Enregistre et propage la mise à jour
        Me.saveData()

        'Met à jour la facturation
        InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & noClient & ",-1)")
        InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & noClient & ")")

        If newMontantRembourse > 0 Then
            MessageBox.Show("Veuillez rembourser " & newMontantRembourse & " $ au client pour le retour du prêt", "Retour du prêt", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf newMontantRembourse < 0 Then
            MessageBox.Show("Veuillez réclamer la somme de " & (newMontantRembourse * -1) & " $ au client pour le retour du prêt", "Retour du prêt", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If


        If selfOpened Then DBLinker.getInstance().dbConnected = False

        Return True
    End Function

    Private Function getCreatedTotal() As Double
        Dim total As Double = profitAmount
        If curBill IsNot Nothing Then
            total = calcAmountPlusTaxes(total, curBill.taxe1, curBill.taxe2, curBill.taxesApplication)
        ElseIf getEquipement().applyTax Then
            total = calcAmountPlusTaxes(total)
        End If
        If _isRefund = False Then total += _amountRefund

        Return roundAmount(total)
    End Function

    Private Sub recreateBill(ByVal amountRefund As Double, ByVal amountBilled As Double, Optional ByVal amountPaid As Double = -1, Optional ByVal comments As String = "", Optional ByVal isNew As Boolean = False)
        amountBilled = roundAmount(amountBilled)
        amountPaid = roundAmount(amountPaid)
        Dim taxe1, taxe2 As Double
        If isNew Then
            If getEquipement().applyTax Then
                taxe1 = PreferencesManager.getGeneralPreferences()("Taxe1")
                taxe2 = PreferencesManager.getGeneralPreferences()("Taxe2")
            Else
                taxe1 = 0
                taxe2 = 0
            End If
        Else
            Dim taxes() As Double = curBill.getLastTaxes()
            taxe1 = taxes(0)
            taxe2 = taxes(1)
            adjustFacture(noBill, getTotal() - Me.amountRefund, 0, , , comments, , comments, , , comments, 0, 0, 20)
            adjustFacture(noBill, 0, , , , comments, , , , , comments, taxe1, taxe2, 20)
        End If
        'Créer la facture d'abord avec le montant remboursé qui est sans taxe (même si zéro)
        Me.noBill = createFacturation(noClient, amountRefund, "Prêt : " & Me.toString, dateTime, noFolder, , noItemBorrowed, , , noBill, , , , , noClient, , , , , , , , , , , , 0, 0, comments, comments)
        'Le numéro de facture est enregistrée via la méthode CreateFacturation
        'Ajuste la facture avec le montant du profit avec les taxes

        If amountBilled <> 0 OrElse amountPaid <> 0 Then adjustFacture(noBill, amountBilled, amountPaid, , , comments, , comments, , , comments, taxe1, taxe2, 5)

        oldTotal = amountBilled
    End Sub
End Class
