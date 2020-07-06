Public Class InternalType_InternalDBItem
    Implements IOpenable

    Public Class InternalType_InternalDBItem_Options
        Implements IOpenableOptions

        Public Sub New()
        End Sub

        Public Sub New(ByVal showAsDialog As Boolean)
            Me.showAsDialog = showAsDialog
        End Sub

        Public showAsDialog As Boolean = False
    End Class

    Public Function open(ByVal uri As String, ByVal options As IOpenableOptions) As IOpenable.OpenableReturn Implements IOpenable.open
        If uri.ToLower.StartsWith("db:\") = False AndAlso uri.ToLower().StartsWith(WebTextControl.PROTOCOL_CLINICA & "db|") = False Then Return IOpenable.OpenableReturn.NotOpenable
        If options IsNot Nothing AndAlso Not TypeOf options Is InternalType_InternalDBItem_Options Then Return IOpenable.OpenableReturn.NotOpenable

        Dim myOptions As InternalType_InternalDBItem_Options = options
        If myOptions Is Nothing Then myOptions = New InternalType_InternalDBItem_Options()

        Dim curDBItem As InternalDBItem
        Try
            curDBItem = InternalDBItem.getItemFromLink(uri)
        Catch ex As InexistantInternalDBItemException
            MessageBox.Show(ex.Message, "Banque de données : Item introuvable")
            Return IOpenable.OpenableReturn.ErrorManaged
        End Try
        
        'Open URI
        If curDBItem.getTypeFile.isInternal Then
            Dim myAddModifDB As AddModifDB = openUniqueWindow(New AddModifDB(), "Banque de données : " & curDBItem.getDBFolder.toString & "\" & curDBItem.dbItem, , True, False)
            myAddModifDB.allowModification = False
            myAddModifDB.loading(curDBItem.noDBItem)
            If myOptions.showAsDialog Then
                myAddModifDB.Visible = False
                myAddModifDB.ShowDialog()
            Else
                myAddModifDB.MdiParent = myMainWin
                myAddModifDB.Show()
            End If
        Else
            Dim fullPath As String = appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile.Trim
            If curDBItem.dbItemFile.Trim <> "" AndAlso IO.File.Exists(fullPath) Then
                launchAProccess(fullPath)
            Else
                MessageBox.Show("Item : " & curDBItem.getDBFolder.toString() & "\" & curDBItem.dbItem & vbCrLf & "L'item ne peut être ouvert, car le fichier lié n'existe pas. Veuillez importer un fichier à l'item.", "Impossible d'ouvrir l'item")
                Return IOpenable.OpenableReturn.ErrorManaged
            End If
        End If

        Return IOpenable.OpenableReturn.Opened
    End Function
End Class
