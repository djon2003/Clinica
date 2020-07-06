Public MustInherit Class Item
    Inherits DBItemableBase

    Protected _noItemBoth As Integer
    Private _NoClient As Integer
    Private _NoFolder As Integer
    Private _noEquipment As Integer
    Private _NoTRP As Integer
    Private _noBill As Integer
    Private _NoItem As String = ""
    Private _dateTime As Date = LIMIT_DATE
    Private _profitAmount As Double
    Protected oldTotal As Double = -1
    Protected oldMontantProfit As Double = -1
    Private _curBill As Bill


#Region "Properties"
    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me._noItemBoth
        End Get
    End Property

    Public ReadOnly Property curBill() As Bill
        Get
            If _noBill = 0 Then Return Nothing

            If _curBill Is Nothing Then _curBill = New Bill(_noBill)
            Return _curBill
        End Get
    End Property

    Public ReadOnly Property noItemBoth() As Integer
        Get
            Return _noItemBoth
        End Get
    End Property

    Public Property noClient() As Integer
        Get
            Return _NoClient
        End Get
        Set(ByVal value As Integer)
            _NoClient = value
        End Set
    End Property

    Public Property dateTime() As Date
        Get
            Return _dateTime
        End Get
        Set(ByVal value As Date)
            _dateTime = value
        End Set
    End Property

    Public Property noItem() As String
        Get
            Return _NoItem
        End Get
        Set(ByVal value As String)
            _NoItem = value
        End Set
    End Property

    Public Property noBill() As Integer
        Get
            Return _noBill
        End Get
        Set(ByVal value As Integer)
            _noBill = value
        End Set
    End Property

    Public Property noTRP() As Integer
        Get
            Return _NoTRP
        End Get
        Set(ByVal value As Integer)
            _NoTRP = value
        End Set
    End Property

    Public Property noFolder() As Integer
        Get
            Return _NoFolder
        End Get
        Set(ByVal value As Integer)
            _NoFolder = value
        End Set
    End Property

    Public Property profitAmount() As Double
        Get
            Return _profitAmount
        End Get
        Set(ByVal value As Double)
            _profitAmount = value
        End Set
    End Property

    Public Property noEquipment() As Integer
        Get
            Return _noEquipment
        End Get
        Set(ByVal value As Integer)
            _noEquipment = value
        End Set
    End Property
#End Region

    Public Overrides Function toString() As String
        Return getEquipement().item & " - " & _NoItem
    End Function

    Public Function getEquipement() As Equipment
        Return EquipmentsManager.getInstance.getItemable(_noEquipment)
    End Function

    Public Overridable Function getTotal() As Double
        Dim total As Double = _profitAmount

        If _noBill = 0 Then
            If getEquipement().applyTax Then total = calcAmountPlusTaxes(total)
        Else
            total = curBill.getBillTotal()
        End If

        Return roundAmount(total)
    End Function

    Protected MustOverride Function getNoItemBothColumnName() As String
    Protected MustOverride Function getTableName() As String
    Protected MustOverride Function getTypeName() As String

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB(getTableName(), getNoItemBothColumnName(), _noItemBoth, False)

        deleteFacturation(Me.noBill)

        If autoSendUpdateOnDelete Then
            InternalUpdatesManager.getInstance.sendUpdate("Paiements(" & noClient & ",-1)")
            InternalUpdatesManager.getInstance.sendUpdate("AccountsBills(" & noClient & ")")
        End If

        onDeleted()
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData
        If curData.Table.Columns.Contains(getNoItemBothColumnName) Then
            _noItemBoth = curData(getNoItemBothColumnName)
        ElseIf curData.Table.Columns.Contains("NoItemPV") Then
            _noItemBoth = curData("NoItemPV")
        End If
        _profitAmount = curData("MontantProfit")
        _NoClient = curData("NoClient")
        _NoFolder = curData("NoFolder")
        _noEquipment = curData("NoEquipement")
        _NoTRP = curData("NoTRP")
        _noBill = curData("NoFacture")
        _NoItem = curData("NoItem")
        _dateTime = curData("DateHeure")

        oldTotal = getTotal()
        oldMontantProfit = _profitAmount
    End Sub


End Class
