Public Class ItemSold
    Inherits Item

#Region "Properties"
    Protected ReadOnly Property noItemSold() As Integer
        Get
            Return noItemBoth
        End Get
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
        getEquipement().removeNoItemSold(noItem)

        MyBase.delete()
    End Sub

    Protected Overrides Function getNoItemBothColumnName() As String
        Return "NoVente"
    End Function

    Protected Overrides Function getTableName() As String
        Return "Ventes"
    End Function

    Protected Overrides Function getTypeName() As String
        Return "Vente"
    End Function

    Public Overrides Sub saveData()
        Dim strNoFacture As String = "null"
        If noBill <> 0 Then strNoFacture = noBill

        Dim changedBill As Boolean = False

        If noItemBoth = 0 Then
            DBLinker.getInstance.writeDB("Ventes", "MontantProfit,NoClient,NoFolder,NoEquipement,NoTRP,NoFacture,NoItem,DateHeure", profitAmount.ToString.Replace(",", ".") & "," & Me.noClient & "," & Me.noFolder & "," & Me.noEquipment & "," & Me.noTRP & "," & strNoFacture & ",'" & Me.noItem.Replace("'", "''") & "','" & DateFormat.getTextDate(Me.dateTime) & "'", , , , _noItemBoth)
            Me.noBill = createFacturation(noClient, getTotal(), "Vente : " & Me.toString, dateTime, noFolder, , , , noItemSold, , , , , , noClient, , , , , , , , , , Me.getEquipement().applyTax)
            'Le numéro de facture est enregistrée via la méthode CreateFacturation

            Me.getEquipement.addNoItemSold(noItem)
            changedBill = True
        Else
            'TODO : Shall ensure "profitAmount" is not modifiable (in object) if payment had been done even if UI already do it.
            If oldMontantProfit <> Me.profitAmount Then 'Recrée la facture si le montant a changé, uniquement accessible si aucun paiement de fait
                adjustFacture(noBill, calcAmountPlusTaxes(Me.profitAmount, curBill.taxe1, curBill.taxe2, curBill.taxesApplication))
                oldTotal = getTotal()
                changedBill = True
            End If
            DBLinker.getInstance.updateDB("Ventes", "MontantProfit=" & profitAmount.ToString.Replace(",", ".") & ",NoTRP=" & Me.noTRP & ",NoFacture=" & noBill, "NoVente", noItemSold, False)
            onDataChanged()
        End If

        If changedBill Then
            InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & noClient & ",-1)")
            InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & noClient & ")")
        Else
            InternalUpdatesManager.getInstance.sendUpdate("AccountsEquipements(" & noClient & "," & noFolder & ")")
        End If
    End Sub
End Class
