Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus


        Private Class RVStatusNormal
            Inherits RVStatus


            Public Overrides Sub changeToStatus(ByVal status As RVStatusChange)
                DBLinker.getInstance.updateDB("InfoVisites", "NoStatut=" & status.[new] & ",NoFacture=null", "NoVisite", status.rv.noVisite, False)

                DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite, NoRaison", status.[new] & "," & status.rv.noFolder & "," & status.rv.noClient & "," & status.rv.noVisite & ",null")
            End Sub

            Public Overrides Sub undoStatus(ByVal status As RVStatusChange)

            End Sub

            Public Overrides Sub verifyChange(ByVal status As RVStatusChange)

            End Sub

            Public Overrides Function toString() As String
                Return "Rendez-vous"
            End Function
        End Class

    End Class
End Namespace

