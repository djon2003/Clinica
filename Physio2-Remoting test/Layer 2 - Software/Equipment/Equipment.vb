Public Class Equipment
    Inherits DBItemableBase

    Public Enum [EquipmentType] As Byte
        Pret = 1
        Vente = 2
        PretVente = 3
        Inventaire = 4
    End Enum

    Public Enum costAmountFrequency As Byte
        Unique = 1
        Day = 2
        Week = 3
    End Enum

    Dim _noEquipment As Integer = 0
    Dim _item As String = ""
    Dim _type As EquipmentType = EquipmentType.Inventaire
    Dim _noItems As New Generic.List(Of String)
    Dim _noItemsBorrowed As New Generic.List(Of String)
    Dim _depositAmount As Double
    Dim _costAmountBy As Double
    Dim _costRepetitionBy As costAmountFrequency = costAmountFrequency.Unique
    Dim _itemSoldAmount As Double
    Dim _amountPaidByItem As Double
    Dim _applyTax As Boolean = True
    Dim _depositRefundPercentage As Double
    Dim _costRefundPercentage As Double
    Dim _category As String = ""
    Dim _description As String = ""
    Dim _nbSold As Integer = 0


#Region "Properties"
    Public ReadOnly Property noEquipment() As Integer
        Get
            Return _noEquipment
        End Get
    End Property

    Public ReadOnly Property nbSold() As Integer
        Get
            Return _nbSold
        End Get
    End Property

    Public Property item() As String
        Get
            Return _item
        End Get
        Set(ByVal value As String)
            _item = value
        End Set
    End Property

    Public Property category() As String
        Get
            Return _category
        End Get
        Set(ByVal value As String)
            _category = value
        End Set
    End Property

    Public Property description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property type() As EquipmentType
        Get
            Return _type
        End Get
        Set(ByVal value As EquipmentType)
            _type = value
        End Set
    End Property

    Public Property noItems() As Generic.List(Of String)
        Get
            Return _noItems
        End Get
        Set(ByVal value As Generic.List(Of String))
            _noItems = value
        End Set
    End Property

    Public Property noItemsBorrowed() As Generic.List(Of String)
        Get
            Return _noItemsBorrowed
        End Get
        Set(ByVal value As Generic.List(Of String))
            _noItemsBorrowed = value
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

    Public Property depositRefundPercentage() As Double
        Get
            Return _depositRefundPercentage
        End Get
        Set(ByVal value As Double)
            _depositRefundPercentage = value
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

    Public Property costAmountBy() As Double
        Get
            Return _costAmountBy
        End Get
        Set(ByVal value As Double)
            _costAmountBy = value
        End Set
    End Property

    Public Property costRepetitionBy() As costAmountFrequency
        Get
            Return _costRepetitionBy
        End Get
        Set(ByVal value As costAmountFrequency)
            _costRepetitionBy = value
        End Set
    End Property

    Public Property itemSoldAmount() As Double
        Get
            Return _itemSoldAmount
        End Get
        Set(ByVal value As Double)
            _itemSoldAmount = value
        End Set
    End Property

    Public Property amountPaidByItem() As Double
        Get
            Return _amountPaidByItem
        End Get
        Set(ByVal value As Double)
            _amountPaidByItem = value
        End Set
    End Property

    Public Property applyTax() As Boolean
        Get
            Return _applyTax
        End Get
        Set(ByVal value As Boolean)
            _applyTax = value
        End Set
    End Property
#End Region

#Region "Constructors"
    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub

    Public Sub New()
    End Sub
#End Region

    Public Sub addNoItemSold(ByVal noItem As String)
        If Me.noItems.Contains(noItem) = False Then Throw New Exception("NoItem non existant : " & noItem)

        _nbSold += 1
        Me.noItems.Remove(noItem)
        DBLinker.getInstance.updateDB("Equipements", "NbVendu=" & _nbSold & ", NoDesItems='" & String.Join("§", Me.noItems.ToArray).Replace("'", "''") & "'", "NoEquipement", noEquipment, False)
        InternalUpdatesManager.getInstance.sendUpdate("Equipement(" & Me.noEquipment & ")")
    End Sub

    Public Sub removeNoItemSold(ByVal noItem As String)
        _nbSold -= 1
        Me.noItems.Add(noItem)
        DBLinker.getInstance.updateDB("Equipements", "NbVendu=" & _nbSold & ", NoDesItems='" & String.Join("§", Me.noItems.ToArray).Replace("'", "''") & "'", "NoEquipement", noEquipment, False)
        InternalUpdatesManager.getInstance.sendUpdate("Equipement(" & Me.noEquipment & ")")
    End Sub

    Public Sub addNoItemBorrowed(ByVal noItem As String)
        Me.noItemsBorrowed.Add(noItem)
        DBLinker.getInstance.updateDB("Equipements", "ItemPrete='" & String.Join("§", Me.noItemsBorrowed.ToArray).Replace("'", "''") & "'", "NoEquipement", noEquipment, False)
        InternalUpdatesManager.getInstance.sendUpdate("Equipement(" & Me.noEquipment & ")")
    End Sub

    Public Sub removeNoItemBorrowed(ByVal noItem As String)
        Me.noItemsBorrowed.Remove(noItem)
        DBLinker.getInstance.updateDB("Equipements", "ItemPrete='" & String.Join("§", Me.noItemsBorrowed.ToArray).Replace("'", "''") & "'", "NoEquipement", noEquipment, False)
        InternalUpdatesManager.getInstance.sendUpdate("Equipement(" & Me.noEquipment & ")")
    End Sub

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("Equipements", "NoEquipement", noEquipment, False)
        onDeleted()
        If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("Equipement-Del(" & Me.noEquipment & ")")
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData
        _noEquipment = curData("NoEquipement")
        _item = curData("nomitem")
        _type = curData("typeitem")

        _noItems.Clear()
        _noItemsBorrowed.Clear()
        For Each noItem As String In curData("NoDesItems").ToString.Split(New Char() {"§"})
            If noItem <> "" Then _noItems.Add(noItem)
        Next
        For Each noItem As String In curData("ItemPrete").ToString.Split(New Char() {"§"})
            If noItem <> "" Then _noItemsBorrowed.Add(noItem)
        Next

        _depositAmount = curData("depot")
        _costAmountBy = curData("cout")
        _costRepetitionBy = curData("coutBy")
        _itemSoldAmount = curData("vente")
        _amountPaidByItem = curData("achat")
        _applyTax = curData("applyTax")
        _depositRefundPercentage = curData("refundDepot")
        _costRefundPercentage = curData("refundCout")
        _category = IIf(curData("categorie") Is DBNull.Value, "", curData("categorie"))
        _description = curData("description").ToString.Replace("<br>", vbCrLf)
        _nbSold = curData("nbVendu")
    End Sub

    Public Overrides Sub saveData()
        'Save catégories
        Dim noCat As String = "null"
        If _category <> "" Then
            Dim tCat() As String = Split(_category, ":")
            Dim curCat As String = ""
            For i As Integer = 0 To tCat.Length - 1
                If curCat = "" Then
                    curCat = tCat(i)
                Else
                    curCat = curCat & ":" & tCat(i)
                End If

                Dim myNoCat As Object = DBHelper.addItemToADBList("ECategorie", "Categorie", curCat, "NoECategorie")
                If curCat = _category Then noCat = myNoCat
            Next i
        End If

        'Save/Insert Equipement
        If Me.noEquipment = 0 Then
            DBLinker.getInstance.writeDB("Equipements", "nomitem,typeitem,NoDesItems,ItemPrete,depot,cout,coutBy,vente,achat,applyTax,refundDepot,refundCout,NoEcategorie,description,nbvendu", _
            "'" & _item.Replace("'", "''") & "'," & CByte(_type) & ",'" & String.Join("§", _noItems.ToArray).Replace("'", "''") & "',''," & _depositAmount.ToString.Replace(",", ".") & "," & _costAmountBy.ToString.Replace(",", ".") & "," & CByte(_costRepetitionBy) & "," & _itemSoldAmount.ToString.Replace(",", ".") & "," & _amountPaidByItem.ToString.Replace(",", ".") & ",'" & _applyTax & "'," & _depositRefundPercentage.ToString.Replace(",", ".") & "," & _costRefundPercentage.ToString.Replace(",", ".") & "," & noCat & ",''," & _nbSold, , , , _noEquipment)

            EquipmentsManager.getInstance.addItemable(Me)
        Else
            DBLinker.getInstance.updateDB("Equipements", "RefundDepot=" & _depositRefundPercentage.ToString.Replace(",", ".") & ",ApplyTax='" & _applyTax & "',TypeItem=" & CByte(_type) & ",NoDesItems='" & String.Join("§", _noItems.ToArray).Replace("'", "''") & "',RefundCout=" & _costRefundPercentage.ToString.Replace(",", ".") & ",vente=" & _itemSoldAmount.ToString.Replace(",", ".") & ",depot=" & _depositAmount.ToString.Replace(",", ".") & ",Cout=" & _costAmountBy.ToString.Replace(",", ".") & ",CoutBy=" & CByte(_costRepetitionBy) & ",Description='" & _description.Replace("'", "''").Replace(vbCrLf, "<br>") & "',Achat=" & _amountPaidByItem.ToString.Replace(",", ".") & ",NomItem='" & _item.Replace("'", "''") & "',NoECategorie=" & noCat, "NoEquipement", noEquipment, False)
            onDataChanged()
        End If
        If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("Equipement(" & Me.noEquipment & ")")
    End Sub

    Public Overrides Function toString() As String
        Return _item & " (" & _noEquipment & ")"
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noEquipment
        End Get
    End Property
End Class
