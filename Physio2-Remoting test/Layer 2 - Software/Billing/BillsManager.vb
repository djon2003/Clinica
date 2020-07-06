Public Class BillsManager
    Inherits ManagerBase(Of BillsManager)

    Protected Sub New()
        MyBase.New()
    End Sub


    Public Enum payingEntity As Byte
        Client = 1
        KP = 2
        User = 3
        Clinique = 4
    End Enum

    Public Shared Function translatePayingEntity(ByVal payingEntity As Char) As payingEntity
        If payingEntity = "" Or payingEntity = " " Then Return BillsManager.payingEntity.Clinique
        If payingEntity = "U" Then Return BillsManager.payingEntity.User
        If payingEntity = "K" Then Return BillsManager.payingEntity.KP

        Return BillsManager.payingEntity.Client
    End Function


    Public Function getPaymentTypes() As Generic.List(Of String)
        Dim paymentTypes As New Generic.List(Of String)
        'Ajoute les types de paiement existant dans une ListBox ou ComboBox
        If PreferencesManager.getGeneralPreferences()("MethodesPaiment").ToString = "" Then Return paymentTypes

        Dim types() As String = Split(PreferencesManager.getGeneralPreferences()("MethodesPaiment"), vbTab)
        For i As Integer = 0 To types.GetUpperBound(0)
            If types(i) <> "" Then paymentTypes.Add(types(i))
        Next i

        Return paymentTypes
    End Function
End Class
