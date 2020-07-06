Namespace Accounts.Clients.Folders
    Partial Public Class FoldersStatus

        ''' <summary>
        ''' Change the status of a folder from inactive to active or vice-versa.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class FolderStatusApplier
            Inherits ManagerBase(Of FolderStatusApplier)

            Private statusActive As New FolderStatusActive()
            Private statusInactive As New FolderStatusInactive()

            Protected Sub New()
            End Sub

            Public Sub changeStatus(ByVal status As FolderStatusChange)
                If status.old = status.[new] Then Throw New FolderStatusException("Le changement de statut est impossible, car le dossier est déjà à ce statut")

                'Droit & Accès
                If currentDroitAcces(55) = False Then
                    'Message & Exit
                    Throw New UserRightException("Vous n'avez pas le droit de modifier le statut d'un dossier." & vbCrLf & "Merci!")
                End If

                'Vérifie et barre le dossier
                If lockSecteur("ClientFolderInfos-" & status.noClient & "-" & status.noFolder & "-", True, "Dossier d'un client", False) = False Then
                    Throw New UserAlreadyUsingException("Le dossier " & status.noFolder & " est déjà en cours de modification par un utilisateur")
                End If

                Try
                    'Change folder status
                    If status.[new] = FolderPossibleStatuses.Inactive Then
                        statusInactive.changeToStatus(status)
                    Else
                        statusActive.changeToStatus(status)
                    End If

                    'Update propagation
                    InternalUpdatesManager.getInstance.sendUpdate("AccountsDossiers(" & status.noClient & ")")
                    InternalUpdatesManager.getInstance.sendUpdate("AccountsDossierTextBoxes(" & status.noClient & "," & status.noFolder & ")")

                Catch ex As Exception
                    Throw ex
                Finally
                    lockSecteur("ClientFolderInfos-" & status.noClient & "-" & status.noFolder & "-", False, "Dossier d'un client", False)
                End Try
            End Sub
        End Class
    End Class
End Namespace
