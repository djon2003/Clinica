Namespace Accounts

    Public Class Client
        Inherits AccountBase


#Region "Constructors"
        Public Sub New()
        End Sub

        Public Sub New(ByVal noAccount As Integer)
            MyBase.New(noAccount)
        End Sub
#End Region

        Protected Shared lockSectorFileNames() As String = New String() {"ClientFolderEquip", "ClientFolderInfos", "ClientFolderText", "ClientFacturation", "ClientGenInfo", "ClientAntecedents", "ClientComm", "ClientRV-ChangeStatus"}
        Protected Shared lockSectorNames() As String = New String() {"Équipement du client", "Informations de base du dossier", "Textes du dossier", "Facturation du client", "Bilan de santé de client", "Communications du client"}

        Protected Overrides Function getLockSectorFileNames() As String()
            Return lockSectorFileNames
        End Function
    End Class

End Namespace
