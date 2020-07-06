Imports CI.Clinica.Accounts.Clients.Folders.FoldersStatus

Namespace Accounts.Clients.Folders

    Public Class ClientFolder
        Public Shared frequencies() As String = New String() {"1xSemaine", "2xSemaine", "3xSemaine", "4xSemaine", "5xSemaine", "6xSemaine", "7xSemaine", "1x2 semaines", "1xMois"}

        Public Sub New()
        End Sub

        Public Shared Function changeStatus(ByVal oldStatus As FolderPossibleStatuses, ByVal newStatus As FolderPossibleStatuses, ByVal noClient As Integer, ByVal noFolder As Integer, Optional ByVal throwErrorInsteadOfMessage As Boolean = False) As Boolean
            Return changeStatus(New FolderStatusChange(oldStatus, newStatus, noClient, noFolder), throwErrorInsteadOfMessage)
        End Function

        Public Shared Function changeStatus(ByVal statusChange As FolderStatusChange, Optional ByVal throwErrorInsteadOfMessage As Boolean = False) As Boolean
            Dim changeError As Exception
            Dim changed As Boolean = False
            Try
                FolderStatusApplier.getInstance.changeStatus(statusChange)
                changed = True
            Catch ex As FolderStatusException
                changeError = ex
            Catch ex As UserRightException
                changeError = ex
            Catch ex As UserAlreadyUsingException
                changeError = ex
            End Try

            If changed = False Then
                If throwErrorInsteadOfMessage Then
                    Throw changeError
                Else
                    MessageBox.Show(changeError.Message, "Impossible de changer le statut du dossier #" & statusChange.noFolder)
                End If
            End If

            Return changed
        End Function
    End Class

End Namespace
