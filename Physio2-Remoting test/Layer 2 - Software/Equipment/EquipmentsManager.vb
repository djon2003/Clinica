Public Class EquipmentsManager
    Inherits DBItemableManagerBase(Of EquipmentsManager, Equipment)

#Region "Constructors"
    Protected Sub New()
        MyBase.New()
        load()
    End Sub
#End Region

    Public Overrides Sub load()
        load(0)
    End Sub
    Private Overloads Sub load(ByVal noEquipement As Integer)
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("Equipements left join ecategorie on ecategorie.noecategorie=equipements.noecategorie", "categorie,*", IIf(noEquipement = 0, "", "NoEquipement=" & noEquipement))

        If (data Is Nothing OrElse data.Tables.Count = 0 OrElse data.Tables(0).Rows.Count = 0) Then
            If noEquipement <> 0 Then
                Dim equip As Equipment = getItemable(noEquipement)
                If equip IsNot Nothing Then MyBase.removeItemable(equip)
            Else
                Me.clear()
            End If

            Exit Sub
        End If
        
        If noEquipement <> 0 Then
            Dim equip As Equipment = getItemable(noEquipement)
            Dim equipData As New DBItemableData(data.Tables(0).Rows(0))
            If equip Is Nothing Then
                MyBase.addItemable(New Equipment(equipData))
                Exit Sub
            End If
            equip.loadData(equipData)
        Else
            changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
            Me.clear()
            For Each curRow As DataRow In data.Tables(0).Rows
                Dim equipment As New Equipment(New DBItemableData(curRow))
                If Me.getItemable(equipment.noItemable) IsNot Nothing Then Continue For ' The equipment had been modified by another at the same time as loading

                MyBase.addItemable(equipment)
            Next
            changingItemablesLock.ReleaseWriterLock()
        End If
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function.StartsWith("Equipement") = False OrElse dataReceived.fromExternal = False Then Exit Sub

        Select Case dataReceived.function
            Case "Equipement"
                load(dataReceived.params(0))
            Case "Equipement-Del"
                Dim equip As Equipment = getItemable(dataReceived.params(0))
                If equip IsNot Nothing Then removeItemable(equip)
            Case "Equipements"
                load()
        End Select
    End Sub

    Protected Overrides Sub sendUpdate()
        InternalUpdatesManager.getInstance.sendUpdate("Equipements()")
    End Sub
End Class
