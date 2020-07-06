Public Class InternalDBManager
    Inherits DBItemableManagerBase(Of InternalDBManager, InternalDBItem)

    Private _AskForReplacing As Boolean = False
    Private _AutoReplacing As Boolean = False
    Private _FoundItemFromDB As String = ""
    Private foldersList As InternalDBFoldersList

    Protected Sub New()
        MyBase.New()
        foldersList = InternalDBFoldersList.getInstance()
    End Sub

    Public Event internalFileSaving(ByVal type As String, ByVal filePath As String)

#Region "Properties"

    Public Property autoReplacing() As Boolean
        Get
            Return _AutoReplacing
        End Get
        Set(ByVal value As Boolean)
            _AutoReplacing = value
        End Set
    End Property

    Public Property askForReplacing() As Boolean
        Get
            Return _AskForReplacing
        End Get
        Set(ByVal value As Boolean)
            _AskForReplacing = value
        End Set
    End Property

    Public Property foundItemFromDB() As String
        Get
            Return _FoundItemFromDB
        End Get
        Set(ByVal value As String)
            _FoundItemFromDB = value
        End Set
    End Property

    Public ReadOnly Property name() As String
        Get
            Return Me.GetType.ToString
        End Get
    End Property
#End Region

#Region "Private functions"
    Private Sub updateSearchableColumn(ByVal filePath As String)
        Dim updateThread As New Threading.Thread(AddressOf _updateSearchableColumn)
        updateThread.Start(filePath)
    End Sub

    Private Sub _updateSearchableColumn(ByVal filePath As String)
        Try
            Threading.Thread.Sleep(1000) 'Wait to give time to file to finish writing

            Dim noDBItem As String = filePath.Substring(filePath.LastIndexOf("\") + 1)
            noDBItem = noDBItem.Substring(0, noDBItem.IndexOf("."))
            DBLinker.getInstance().updateDB("DBItems", "SearchableContent=dbo.fnGetDBItemContent(NoDBItem)", "NoDBItem", noDBItem, False)
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Private Function addModifItem(ByRef curDBItem As InternalDBItem, ByVal nom As String, ByVal noDBFolder As Integer, ByVal type As String, ByVal importing As Boolean, ByVal motsCles() As String, ByVal description() As String, ByVal isHidden As Boolean, ByVal isReadOnly As Boolean, Optional ByVal contentFile As String = "") As String
        Dim curType As TypeFile = TypesFilesManager.getInstance.getItemable(type)
        Dim SDBFile(), OldFileName, oldType As String

        curDBItem.dbItem = nom
        If contentFile <> "" Then curDBItem.dbItemFile = contentFile
        curDBItem.noDBFolder = noDBFolder
        If curDBItem.getTypeFile IsNot Nothing Then
            oldType = curDBItem.getTypeFile.fileType
        Else
            oldType = ""
        End If

        curDBItem.noFileType = curType.noTypeFile

        If curDBItem.noDBItem = 0 Then curDBItem.saveData()
        If contentFile <> "" Then contentFile = curDBItem.noDBItem & "." & contentFile

        If importing Then
            OldFileName = curDBItem.dbItemFile
            contentFile = importer(curDBItem.noDBItem & "." & curDBItem.dbItem, "DB", , curType.extensions, OldFileName)
            If contentFile = "" Then Return "CANCELED"
        End If

        curDBItem.keywords = motsCles

        Dim myExtension As String = ""
        If contentFile = "" Then
            'Choix de l'extension
            If oldType <> curType.fileType Then
                If curType.extensions.IndexOf(";") <> -1 Then
                    Dim myMultiChoice As New multichoice()
                    myExtension = myMultiChoice.GetChoice("Veuillez sélectionner l'extension approprié", curType.extensions, , ";")
                    If myExtension.ToUpper.StartsWith("ERROR") Then Return "CANCELED"
                Else
                    myExtension = curType.extensions
                End If
            Else
                SDBFile = curDBItem.dbItemFile.Split(New Char() {"."})
                myExtension = SDBFile(SDBFile.GetUpperBound(0))
            End If

            'Changement pour l'ancien fichier de contenu
            If curDBItem.noDBItem <> 0 AndAlso oldType <> curType.fileType Then
                If IO.File.Exists(appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile) = True Then
                    If fileInUse(appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile) Then
                        Return "Impossible de modifier l'item, car le contenu est présentement en cours d'utilisation."
                    Else
                        IO.File.Delete(appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile)
                    End If
                End If
            End If

            curDBItem.dbItemFile = curDBItem.noDBItem & "." & curDBItem.dbItem & "." & myExtension

            If curType.isInternal And importing = False Then
                RaiseEvent internalFileSaving(myExtension, appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile)
                updateSearchableColumn(appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile)
            End If
        Else
            curDBItem.dbItemFile = contentFile
            If importing = False Then
                RaiseEvent internalFileSaving(myExtension, appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile)
                updateSearchableColumn(appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile)
            End If
        End If

        curDBItem.isHidden = isHidden
        curDBItem.isReadOnly = isReadOnly
        curDBItem.description = String.Join(vbCrLf, description)

        Try
            curDBItem.saveData()
        Catch ex As Exception
            Return "Impossible de renommer l'item, car le contenu est en cours de visualisation"
        End Try

        Return ""
    End Function

    Private Function getFromDB(Optional ByVal selectedType As String = "") As String
        foundItemFromDB = ""
        Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB, , , True, False)
        mySearchDB.Visible = False
        mySearchDB.MdiParent = Nothing
        mySearchDB.from = Me
        mySearchDB.selectedFileType = selectedType
        mySearchDB.useWinAsSelection = True
        mySearchDB.StartPosition = FormStartPosition.CenterScreen
        mySearchDB.ShowDialog()

        Return foundItemFromDB
    End Function


    Private Function checkForExistance(ByVal noDBFolder As Integer, ByVal nom As String) As Boolean
        Dim isExisting As Boolean = False
        nom = Fichiers.replaceIllegalChars(nom)
        Dim exists() As String = DBLinker.getInstance.readOneDBField("DBItems", "TOP 1 NoDBItem", "WHERE NoDBFolder=" & noDBFolder & " AND DBItem='" & nom.Replace("'", "''") & "'")
        If exists IsNot Nothing AndAlso exists.Length <> 0 AndAlso exists(0) <> 0 Then isExisting = True

        If isExisting AndAlso (_AskForReplacing Or _AutoReplacing) Then
            If _AutoReplacing OrElse MessageBox.Show("Un item porte déjà le nom '" & nom & "'." & vbCrLf & "Voulez-vous le remplacer ?", "Item existant", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                isExisting = False
                Dim tmpItem As New InternalDBItem(exists(0))
                tmpItem.delete()
            End If
        End If

        Return isExisting
    End Function
#End Region


    Public ReadOnly Property folderType() As Type
        Get
            Return foldersList.managedType()
        End Get
    End Property

    Public Function getImageFromDB() As InternalDBItem
        getFromDB("Image")
        If foundItemFromDB = "" Then Return Nothing

        Dim myItems() As String = foundItemFromDB.Split(New Char() {"§"})
        Dim myItem() As String = myItems(0).Split(New Char() {"\"})
        Dim curDBItem As New InternalDBItem(myItem(0))

        Return curDBItem
    End Function


    Public Function getURLFromDB(ByRef dbTitle As String) As String
        'Affiche la recherche dans la banque de données pour sélectionner l'item à lier
        'Lien interne
        getFromDB()
        If foundItemFromDB = "" Then Return ""

        Dim myItems() As String = foundItemFromDB.Split(New Char() {"§"})
        dbTitle = getLastDir(myItems(0))
        If dbTitle.EndsWith(".DB") Then dbTitle = dbTitle.Substring(0, dbTitle.Length - 3)

        Return WebTextControl.PROTOCOL_CLINICA & Web.HttpUtility.UrlEncode("DB|" & myItems(0))
    End Function


    Public Function addItem(ByVal nom As String, ByVal dbPath As InternalDBFolder, ByVal type As String, ByVal importing As Boolean, ByVal motsCles() As String, ByVal description() As String, ByVal isHidden As Boolean, ByVal isReadOnly As Boolean, Optional ByVal contentFile As String = "") As String
        If nom = "" Then Return "Veuillez entrer un titre pour l'item"
        If checkForExistance(dbPath.noDBFolder, nom) Then Return "Un item porte déjà ce titre dans le dossier : " & dbPath.toString

        Dim newDBItem As New InternalDBItem
        Dim returning As String = addModifItem(newDBItem, nom, dbPath.noDBFolder, type, importing, motsCles, description, isHidden, isReadOnly, contentFile)

        If returning = "" Then InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & newDBItem.noDBFolder & ")")

        Return returning
    End Function

    Public Function addItem(ByVal newDBItem As InternalDBItem, Optional ByVal sendUpdate As Boolean = True) As String
        If newDBItem.dbItem = "" Then Return "Veuillez entrer un titre pour l'item"
        If checkForExistance(newDBItem.noDBFolder, newDBItem.dbItem) Then Return "Un item porte déjà ce titre dans le dossier : " & newDBItem.getDBFolder.toString

        newDBItem.saveData()

        If sendUpdate Then InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & newDBItem.noDBFolder & ")")
    End Function

    Public Function modifItem(ByVal noDBItem As Integer, ByVal nom As String, ByVal dbPath As InternalDBFolder, ByVal type As String, ByVal importing As Boolean, ByVal motsCles() As String, ByVal description() As String, ByVal isHidden As Boolean, ByVal isReadOnly As Boolean, Optional ByVal contentFile As String = "") As String
        If nom = "" Then Return "Veuillez entrer un titre pour l'item"
        If DBLinker.getInstance.readOneDBField("DBItems", "COUNT(*)", "WHERE NoDBItem<>" & noDBItem & " AND NoDBFolder=" & dbPath.noDBFolder & " AND DBItem='" & nom.Replace("'", "''") & "'")(0) <> 0 Then Return "Un item porte déjà ce titre dans le dossier : " & dbPath.toString

        Dim newDBItem As New InternalDBItem(noDBItem)
        Dim returning As String = addModifItem(newDBItem, nom, dbPath.noDBFolder, type, importing, motsCles, description, isHidden, isReadOnly, contentFile)

        Return returning
    End Function

    Public Function folderExists(ByVal dbPath As String) As Boolean
        Return InternalDBFolder.exists(dbPath)
    End Function

    Public Function getDBFolders() As Generic.List(Of InternalDBFolder)
        Return foldersList.getItemables()
    End Function

    Public Function addDBFolder(ByVal path As String, Optional ByRef noDBFolder As Integer = 0) As String
        Return foldersList.addItemable(path, noDBFolder)
    End Function

    Private Sub folderSaved(ByVal sender As FolderBase, ByVal e As FolderChangedEventArgs)
        'REM The update is done via IDBFolder.DBFolder_ShouldRefreshFolder(), maybe it shall be here, or some where else like in internalupdates
        'If e.oldFolder <> IDBFolder.getRealPath(sender.ToString, sender.NoUser) OrElse e.oldUser <> sender.NoUser Then
        '    _DBFoldersByPath.Remove(IDBFolder.getPath(e.oldUser, e.oldFolder))
        '    _DBFoldersByPath.Add(sender.ToString, sender)
        'End If
    End Sub


    Public Function getDBFolder(ByVal path As String) As InternalDBFolder
        Return foldersList.getItemable(path)
    End Function

    Public Function getDBFolder(ByVal noDBFolder As Integer) As InternalDBFolder
        Return foldersList.getItemable(noDBFolder)
    End Function

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        'REM Throw New NotImplementedException()
    End Sub

    Public Overrides Sub load()
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Sub sendUpdate()
        Throw New NotImplementedException()
    End Sub
End Class
