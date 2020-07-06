Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus

        Private MustInherit Class RVStatus

            Public MustOverride Sub changeToStatus(ByVal status As RVStatusChange)
            Public MustOverride Sub undoStatus(ByVal status As RVStatusChange)
            Public MustOverride Sub verifyChange(ByVal status As RVStatusChange)

            Protected Const errorTxt As String = "Une erreur est servenue lors du changement de statut. Veuillez réesssayer."


        End Class

    End Class
End Namespace
