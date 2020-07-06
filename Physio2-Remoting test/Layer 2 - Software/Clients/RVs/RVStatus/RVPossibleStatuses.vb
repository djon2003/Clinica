Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus

        Public Enum RVPossibleStatuses
            NotPresentNotMotivated = 1
            NotPresentMotivated = 2
            Normal = 3
            Present = 4
        End Enum

        Private Shared Function getStatusClass(ByVal status As RVPossibleStatuses) As RVStatus
            Dim newStatus As RVStatus
            Select Case status
                Case RVPossibleStatuses.NotPresentMotivated
                    newStatus = New RVStatusNotPresentMotivated()
                Case RVPossibleStatuses.NotPresentNotMotivated
                    newStatus = New RVStatusNotPresentNotMotivated()
                Case RVPossibleStatuses.Present
                    newStatus = New RVStatusPresent()
                Case Else
                    newStatus = New RVStatusNormal()
            End Select

            Return newStatus
        End Function

    End Class
End Namespace
