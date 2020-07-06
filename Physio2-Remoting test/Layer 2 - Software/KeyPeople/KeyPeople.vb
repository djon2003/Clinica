Namespace Accounts

    Public Class KeyPeople
        Inherits AccountBase


#Region "Constructors"
        Public Sub New()
        End Sub

        Public Sub New(ByVal noAccount As Integer)
            MyBase.New(noAccount)
        End Sub
#End Region


        Protected Shared lockSectorFileNames() As String = New String() {"KPFacturation", "KPGenInfo", "KPComm"}
        Protected Shared lockSectorNames() As String = New String() {"Facturation de la personne/organisme clé", "Informations de base de la personne/organisme clé", "Communications de la personne/orgarnisme clé"}

        Protected Overrides Function getLockSectorFileNames() As String()
            Return lockSectorFileNames
        End Function
    End Class

End Namespace
