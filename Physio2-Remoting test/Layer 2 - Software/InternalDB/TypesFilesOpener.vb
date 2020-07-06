Public Class TypesFilesOpener
    Inherits ManagerBase(Of TypesFilesOpener)
    Implements IOpenable

    Private internalTypes As New Generic.List(Of IOpenable)

    Protected Sub New()
        loadInternalTypes()
    End Sub

    Private Sub loadInternalTypes()
        'Order is important
        internalTypes.Add(New InternalType_InternalDBItem())
        internalTypes.Add(New InternalType_Browser())
        internalTypes.Add(New InternalType_Report())
        internalTypes.Add(New InternalType_Text())
    End Sub

    Public Function open(ByVal uri As String, ByVal options As IOpenableOptions) As IOpenable.OpenableReturn Implements IOpenable.open
        'Internal opening (If it matches)
        For Each openable As IOpenable In internalTypes
            Dim returning As IOpenable.OpenableReturn = openable.open(uri, options)
            If returning <> IOpenable.OpenableReturn.NotOpenable Then Return returning
        Next

        'Ensure file extension existing in a TypeFile, other refuse opening
        Dim myExts() As String = uri.Split(New Char() {"."})
        Dim curType As TypeFile = TypesFilesManager.getInstance.getTypeFileFromExt(myExts(myExts.GetUpperBound(0)))
        If curType Is Nothing Then
            MessageBox.Show("Le type de fichier supportant ce fichier a été supprimé ou n'a jamais existé." & vbCrLf & vbCrLf & "Veuillez aller dans la section Banque de données >> Types de fichiers et ajouter un type de fichier contenant l'extension (" & myExts(myExts.GetUpperBound(0)) & ") ou ajouter cet extension à un type de fichier existant.", "Type de fichier inexistant", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return IOpenable.OpenableReturn.ErrorManaged
        End If

        'Open URI
        Try
            launchAProccess(uri)
        Catch ex As Exception
            Return IOpenable.OpenableReturn.Opened
        End Try

        Return IOpenable.OpenableReturn.NotOpenable
    End Function
End Class
