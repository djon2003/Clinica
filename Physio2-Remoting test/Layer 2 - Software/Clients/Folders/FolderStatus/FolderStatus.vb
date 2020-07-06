Imports CI.Clinica.Accounts.Clients.Folders.RVs

Namespace Accounts.Clients.Folders

    Partial Public Class FoldersStatus

        Private MustInherit Class FolderStatus

            Public MustOverride Sub changeToStatus(ByVal status As FolderStatusChange)


            Protected Sub removeFutureRVs(ByVal noClient As Integer, ByVal noFolder As Integer, ByVal from As Date)
                'Suppression des rendez-vous futur
                Dim futureVisites As Generic.List(Of RendezVous) = RendezVousManager.getInstance.loadRendezVous(noClient, from.AddDays(1), , False, noFolder)
                For i As Integer = 0 To futureVisites.Count - 1
                    futureVisites(i).confirmDeletion = False
                    futureVisites(i).delete()
                Next i
            End Sub
        End Class

    End Class

End Namespace
