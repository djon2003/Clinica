Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus


        Public Class RVStatusChangeExtraInfos

            Private _FirstTraitement As Date = LIMIT_DATE
            Private _LastRVDate As Date = LIMIT_DATE
            Private _NoTRPTraitant, _NoFTDefault, _NoTRPToTransfer, _NbPresences As Integer
            Private _StatutOuvert As FoldersStatus.FolderPossibleStatuses = FoldersStatus.FolderPossibleStatuses.Active
            Private _TexteFTDefault As String = ""
            Private _NoFTTs As String = ""
            Private _NoStatut As RVPossibleStatuses = RVPossibleStatuses.Normal

            Public Sub New(ByVal status As RVStatusChange)
                'REM This read could probably be removed.. though, some like FirstTreatment is not getable from RendezVous.. How to solution this ?
                Dim visiteInfos As DataSet = DBLinker.getInstance.readDBForGrid("InfoClients INNER JOIN InfoFolders ON InfoClients.NoClient = InfoFolders.NoClient INNER JOIN InfoVisites INNER JOIN Utilisateurs ON InfoVisites.NoTRP = Utilisateurs.NoUser ON InfoFolders.NoFolder = InfoVisites.NoFolder", _
                               "(SELECT TOP 1 DateHeure FROM InfoVisites IV WHERE IV.NoFolder=InfoVisites.NoFolder AND IV.NoStatut>=3 AND (IV.Evaluation=0 OR IV.Evaluation IS NULL) ORDER BY DateHeure) AS FirstTraitement," & _
                               "InfoFolders.NoTRPTraitant," & _
                               "InfoFolders.NoTRPToTransfer," & _
                               "InfoFolders.StatutOuvert," & _
                               "(SELECT COUNT(*) FROM InfoVisites AS IV2 WHERE IV2.NoStatut=4 AND IV2.NoFolder=InfoVisites.NoFolder) AS NbPresences," & _
                               "(SELECT MAX(DateHeure) FROM InfoVisites IV2 WHERE IV2.NoFolder=InfoVisites.NoFolder AND NoStatut=4) AS LastRVDate," & _
                               "(SELECT TOP 1 FT.NoFolderTexte FROM FolderTextes AS FT INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FT.NoFolderTexteType WHERE FT.NoFolder=InfoVisites.NoFolder AND FTT.IsDefault=1) AS NoFTDefault, " & _
                               "(SELECT TOP 1 Texte FROM FolderTextes AS FT INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FT.NoFolderTexteType WHERE FT.NoFolder=InfoVisites.NoFolder AND FTT.IsDefault=1) AS TexteFTDefault," & _
                               "dbo.[fnGetFolderNoFTTs](" & status.rv.noFolder & ") AS NoFTTs," & _
                               "InfoVisites.NoStatut", _
                               "WHERE (((InfoVisites.NoVisite)=" & status.rv.noVisite & "));")
                If visiteInfos Is Nothing OrElse visiteInfos.Tables.Count = 0 OrElse visiteInfos.Tables(0).Rows.Count = 0 Then
                    Throw New RVStatusException("La visite n'existe plus. Veuillez fermer et rouvrir l'agenda ou le compte client pour résoudre le problème.")
                End If

                Dim data As DataRow = visiteInfos.Tables(0).Rows(0)

                If data("FirstTraitement") IsNot DBNull.Value Then _FirstTraitement = data("FirstTraitement")
                If data("LastRVDate") IsNot DBNull.Value Then _LastRVDate = data("LastRVDate")
                If data("NoTRPTraitant") IsNot DBNull.Value Then _NoTRPTraitant = data("NoTRPTraitant")
                If data("NoFTDefault") IsNot DBNull.Value Then _NoFTDefault = data("NoFTDefault")
                If data("NoTRPToTransfer") IsNot DBNull.Value Then _NoTRPToTransfer = data("NoTRPToTransfer")
                If data("NbPresences") IsNot DBNull.Value Then _NbPresences = data("NbPresences")
                If data("StatutOuvert") IsNot DBNull.Value Then _StatutOuvert = data("StatutOuvert")
                If data("TexteFTDefault") IsNot DBNull.Value Then _TexteFTDefault = data("TexteFTDefault")
                If data("NoFTTs") IsNot DBNull.Value Then _NoFTTs = data("NoFTTs")
                If data("NoStatut") IsNot DBNull.Value Then _NoStatut = data("NoStatut")
            End Sub

#Region "Properties"
            Public ReadOnly Property noFTTs() As String
                Get
                    Return _NoFTTs
                End Get
            End Property

            Public ReadOnly Property textFTDefault() As String
                Get
                    Return _TexteFTDefault
                End Get
            End Property

            Public ReadOnly Property rendezVousStatus() As RVPossibleStatuses
                Get
                    Return _NoStatut
                End Get
            End Property

            Public ReadOnly Property folderStatus() As FoldersStatus.FolderPossibleStatuses
                Get
                    Return _StatutOuvert
                End Get
            End Property
            Public ReadOnly Property nbPresences() As Integer
                Get
                    Return _NbPresences
                End Get
            End Property

            Public ReadOnly Property noTRPToTransfer() As Integer
                Get
                    Return _NoTRPToTransfer
                End Get
            End Property


            Public ReadOnly Property noFTDefault() As Integer
                Get
                    Return _NoFTDefault
                End Get
            End Property


            Public ReadOnly Property noTRPTraitant() As Integer
                Get
                    Return _NoTRPTraitant
                End Get
            End Property

            Public ReadOnly Property firstTraitement() As Date
                Get
                    Return _FirstTraitement
                End Get
            End Property

            Public ReadOnly Property lastRVDate() As Date
                Get
                    Return _LastRVDate
                End Get
            End Property
#End Region

        End Class


    End Class
End Namespace
