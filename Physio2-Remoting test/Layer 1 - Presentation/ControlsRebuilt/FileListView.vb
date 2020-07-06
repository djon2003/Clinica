Imports System.IO
Imports System.Diagnostics

Class FileListView
    Inherits DataGridPlus

#Region "Constructeur"
    Public Sub New(Optional ByVal showDirColumn As Boolean = False)
        _ShowDirColumn = showDirColumn

        loading()

        buildColumns()
        buildContextMenu()

        'Quitte si en mode Design
        If DrawingManager.getInstance.getIcon("TEXTsmall.ico") Is Nothing Then Exit Sub

        Dim imgSmall As New ImageList()
        With imgSmall.Images
            .Add(DrawingManager.getInstance.getIcon("FolderClosed.ico"))
            .Add(DrawingManager.getInstance.getIcon("TEXTsmall.ico"))
            .Add(DrawingManager.getInstance.getIcon("Picturesmall.ico"))
            .Add(DrawingManager.getInstance.getIcon("Linksmall.ico"))
            .Add(DrawingManager.getInstance.getIcon("Soundsmall.ico"))
            .Add(DrawingManager.getInstance.getIcon("Videosmall.ico"))
        End With

        Dim imgBig As New ImageList
        With imgBig.Images
            .Add(DrawingManager.getInstance.getIcon("FolderClosedbig.ico"))
            .Add(DrawingManager.getInstance.getIcon("TEXTbig.ico"))
            .Add(DrawingManager.getInstance.getIcon("Picturebig.ico"))
            .Add(DrawingManager.getInstance.getIcon("Linkbig.ico"))
            .Add(DrawingManager.getInstance.getIcon("Soundbig.ico"))
            .Add(DrawingManager.getInstance.getIcon("Videobig.ico"))
        End With

        Me.smallImageList = imgSmall
        Me.smallImageList.ImageSize = New Size(16, 16)
        Me.largeImageList = imgBig
        Me.largeImageList.ImageSize = New Size(32, 32)
    End Sub
#End Region

    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim i As Integer
            For i = 0 To 1
                RemoveHandler MyCM.MenuItems(0).MenuItems(i).Click, AddressOf Me.sortClick
            Next i
            For i = 0 To MyCM.MenuItems(1).MenuItems.Count - 1
                RemoveHandler MyCM.MenuItems(i).Click, AddressOf Me.commandClick
            Next i
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Définitions"
    'Événements
    Public Event menuAddACat()
    Public Event itemSelected(ByVal sender As Object, ByVal selectedItem() As InternalDBItem)

    Private curRowDBItem As DataGridViewRow
    Private curMouseBouton As MouseButtons
    Private smallImageList As New ImageList
    Private largeImageList As New ImageList
    Friend WithEvents MyCM As System.Windows.Forms.ContextMenu
    Private curDBFolder As InternalDBFolder
    Private oldName As String = ""
    Private listMousePosition As Point
    Private _ShowDirColumn As Boolean = False

    Private _ShowMenu As Boolean = True
    Private _ShowMenuOpen As Boolean = True
    Private _ShowMenuAdd As Boolean = True
    Private _ShowMenuModif As Boolean = True
    Private _ShowMenuSearchIn As Boolean = True
    Private _ShowMenuDel As Boolean = True
    Private _ShowMenuCut As Boolean = True
    Private _ShowMenuCopy As Boolean = True
    Private _ShowMenuPaste As Boolean = True
    Private _ShowMenuAddCat As Boolean = True
    Private _ShowMenuRename As Boolean = True
    Private _ShowMenuAddFavoris As Boolean = False 'v2.0 Désactivé pour le menu n'apparaisse pas
    Private _ShowMenuSearchDB As Boolean = False
    Private _ShowMenuSelect As Boolean = False
    Private _ShowHiddenFiles As Boolean = True
    Private normalLoading As Boolean = True
    Private holdingFiles() As String = Nothing
    Public cutting As Boolean = False
    Private noItems As New Label
    Private LastFromStr, lastWhereStr As String
    Private didDoDragDrop As Boolean = False
    Private _AllowUserToDragRows As Boolean = True
#End Region

#Region "Menu Click"
    Private Sub sortClick(ByVal sender As Object, ByVal e As EventArgs)
        columnHeaderClicked = False
        With Me
            If .SortOrder = SortOrder.Ascending Then
                .Sort(.SortedColumn, System.ComponentModel.ListSortDirection.Descending)
                MyCM.MenuItems(0).MenuItems(1).Checked = True
                MyCM.MenuItems(0).MenuItems(0).Checked = False
            Else
                .Sort(.SortedColumn, System.ComponentModel.ListSortDirection.Ascending)
                MyCM.MenuItems(0).MenuItems(1).Checked = False
                MyCM.MenuItems(0).MenuItems(0).Checked = True
            End If
        End With
    End Sub

    Private Sub openSelected()
        For i As Integer = 0 To Me.SelectedRows.Count - 1
            TypesFilesOpener.getInstance.open("db:\" & Me.SelectedRows(i).Cells("NoDBItem").Value & "\" & InternalDBManager.getInstance.getDBFolder(Me.SelectedRows(i).Cells("NoDBFolder").Value).ToString & "\" & Me.SelectedRows(i).Cells("DBItem").Value, Nothing)
        Next i
    End Sub

    Private Sub commandClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim myCommandText As String = sender.text

        Dim treeNodePath As String = ""
        If curRowDBItem IsNot Nothing Then
            treeNodePath = InternalDBManager.getInstance.getDBFolder(curRowDBItem.Cells("NoDBFolder").Value).ToString()
        ElseIf curDBFolder IsNot Nothing Then
            treeNodePath = curDBFolder.toString
        End If

        Select Case myCommandText
            Case "Ouvrir"
                openSelected()

            Case "Ajouter"
                Dim myAddModifDB As New AddModifDB()
                myAddModifDB.curDBFolder = Me.curDBFolder
                myAddModifDB.loading()
                myAddModifDB.ShowDialog()

            Case "Modifier"
                Dim myAddModifDB As New AddModifDB
                myAddModifDB.curDBFolder = Me.curDBFolder
                myAddModifDB.loading(curRowDBItem.Cells("NoDBItem").Value)
                myAddModifDB.ShowDialog()

            Case "Renommer"
                Me.currentCell = curRowDBItem.Cells("DBItem")
                Me.BeginEdit(True)

            Case "Supprimer"
                If MessageBox.Show("Êtes-vous sûr de vouloir supprimer " & IIf(Me.SelectedRows.Count = 1, "cet", "ces") & " item" & IIf(Me.SelectedRows.Count = 1, "", "s") & " ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = MsgBoxResult.No Then Exit Sub
                Dim noFoldersToUpdate As New Generic.List(Of Integer)
                For i As Integer = 0 To Me.SelectedRows.Count - 1
                    Dim curDBItem As New InternalDBItem(CInt(Me.SelectedRows(i).Cells("NoDBItem").Value))
                    Try
                        curDBItem.delete()
                        If noFoldersToUpdate.Contains(curDBItem.noDBFolder) = False Then noFoldersToUpdate.Add(curDBItem.noDBFolder)
                    Catch ex As DBItemableUndeletable
                        MessageBox.Show("Impossible de supprimer l'item (" & InternalDBManager.getInstance.getDBFolder(curDBItem.noDBFolder).toString & "\" & curDBItem.dbItem & "), car il est en cours de visualisation.", "Erreur de suppression")
                    End Try
                Next i
                Me.reloadFiles()
                For i As Integer = 0 To noFoldersToUpdate.Count - 1
                    InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & noFoldersToUpdate(i) & ")")
                Next i

            Case "Imprimer le contenu"
                For i As Integer = 0 To Me.SelectedRows.Count - 1
                    Dim contenu As String = IO.File.ReadAllText(appPath & bar(appPath) & "DB\" & Me.SelectedRows(i).Cells("DBItemFile").Value)
                    PrintingHelper.printHtml(contenu, Me.FindForm().Text & " : " & Me.SelectedRows(i).Cells("DBItem").Value, True, True)
                Next i
            Case "Rechercher dans ce dossier"
                Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB())
                If treeNodePath <> "" Then
                    mySearchDB.selectedCat = treeNodePath
                Else
                    mySearchDB.selectedCat = ""
                End If
                mySearchDB.Show()

            Case "Couper"
                cutting = True
                copyDBFile(True)

            Case "Copier"
                copyDBFile()

            Case "Coller"
                Dim curTempItems() As String = IO.Directory.GetDirectories(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy", "*.*", SearchOption.TopDirectoryOnly)
                For i As Integer = 0 To curTempItems.Length - 1
                    InternalDBItem.paste(getLastDir(curTempItems(i)), curDBFolder)
                Next i
                InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & curDBFolder.noDBFolder & ")")

            Case "Ajout d'un dossier"
                RaiseEvent menuAddACat()

            Case "Ajout dans les favoris"

            Case "Rechercher dans la banque de données"
                Dim myDB As DB = openUniqueWindow(New DB())
                myDB.selectedCat = treeNodePath
                myDB.selectedItem = curRowDBItem.Cells("DBItem").Value
                myDB.Show()

            Case "Sélectionner"
                RaiseEvent itemSelected(Me, getDBItemsSelected)
        End Select
    End Sub
#End Region

#Region "Propriétés"
    Public Property allowUserToDragRows() As Boolean
        Get
            Return _AllowUserToDragRows
        End Get
        Set(ByVal value As Boolean)
            _AllowUserToDragRows = value
        End Set
    End Property

    Public Property ColumnSize(ByVal myTitle As String) As Integer
        Get
            Dim myColumn As DataGridViewColumn
            For Each myColumn In Me.Columns
                If myColumn.HeaderText = myTitle Then Return myColumn.Width
            Next myColumn
        End Get
        Set(ByVal Value As Integer)
            Dim myColumn As DataGridViewColumn
            For Each myColumn In Me.Columns
                If myColumn.HeaderText = myTitle Then myColumn.Width = Value
            Next myColumn
        End Set
    End Property

    Public Property showMenu() As Boolean
        Get
            Return _ShowMenu
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenu = Value
        End Set
    End Property

    Public ReadOnly Property showDirColumn() As Boolean
        Get
            Return _ShowDirColumn
        End Get
    End Property

    Public Property showHiddenFiles() As Boolean
        Get
            Return _ShowHiddenFiles
        End Get
        Set(ByVal Value As Boolean)
            _ShowHiddenFiles = Value
        End Set
    End Property

    Public Property showMenuSearchDB() As Boolean
        Get
            Return _ShowMenuSearchDB
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuSearchDB = Value
        End Set
    End Property

    Public Property showMenuSelect() As Boolean
        Get
            Return _ShowMenuSelect
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuSelect = Value
            If Value = True Then
                MyCM.MenuItems(1).DefaultItem = False
                MyCM.MenuItems(16).DefaultItem = True
            Else
                MyCM.MenuItems(1).DefaultItem = True
                MyCM.MenuItems(16).DefaultItem = False
            End If
        End Set
    End Property

    Public Property showMenuOpen() As Boolean
        Get
            Return _ShowMenuOpen
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuOpen = Value
        End Set
    End Property

    Public Property showMenuAdd() As Boolean
        Get
            Return _ShowMenuAdd
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuAdd = Value
        End Set
    End Property

    Public Property showMenuModif() As Boolean
        Get
            Return _ShowMenuModif
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuModif = Value
        End Set
    End Property

    Public Property showMenuSearchIn() As Boolean
        Get
            Return _ShowMenuSearchIn
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuSearchIn = Value
        End Set
    End Property

    Public Property showMenuRename() As Boolean
        Get
            Return _ShowMenuRename
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuRename = Value
        End Set
    End Property

    Public Property showMenuDel() As Boolean
        Get
            Return _ShowMenuDel
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuDel = Value
        End Set
    End Property

    Public Property showMenuCut() As Boolean
        Get
            Return _ShowMenuCut
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuCut = Value
        End Set
    End Property

    Public Property showMenuCopy() As Boolean
        Get
            Return _ShowMenuCopy
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuCopy = Value
        End Set
    End Property

    Public Property showMenuPaste() As Boolean
        Get
            Return _ShowMenuPaste
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuPaste = Value
        End Set
    End Property

    Public Property showMenuAddCat() As Boolean
        Get
            Return _ShowMenuAddCat
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuAddCat = Value
        End Set
    End Property

    Public Property showMenuAddFavoris() As Boolean
        Get
            Return _ShowMenuAddFavoris
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuAddFavoris = False
            'REM v2.0 Changer pour toujours Invisible, car cet option devrait être valide dans la version 2.0 du logiciel.
        End Set
    End Property

    Public Property dbFolder() As Clinica.InternalDBFolder
        Get
            Return curDBFolder
        End Get
        Set(ByVal value As Clinica.InternalDBFolder)
            curDBFolder = value
            If value IsNot Nothing Then showFiles(curDBFolder)
        End Set
    End Property
#End Region

#Region "Private subs"
    Private Sub buildContextMenu()
        Dim viewArray() As String = {"Ascendant", "Descendant"}
        Dim commandArray() As String = {"Ouvrir", "Ajouter", "Modifier", "Rechercher dans ce dossier", "Renommer", "Supprimer", "Imprimer le contenu", "-", "Couper", "Copier", "Coller", "-", "Ajout d'un dossier", "Ajouter dans les favoris", "Rechercher dans la banque de données", "Sélectionner"}
        Dim i As Byte

        'Ajout du menu affichage
        Me.MyCM = New System.Windows.Forms.ContextMenu
        Me.MyCM.MenuItems.Add("Affichage")
        For i = 0 To 1
            Dim myMenuItem As New MenuItem
            myMenuItem.RadioCheck = True
            myMenuItem.Text = viewArray(i)
            AddHandler myMenuItem.Click, AddressOf Me.sortClick
            Me.MyCM.MenuItems(0).MenuItems.Add(myMenuItem)
        Next i
        Me.MyCM.MenuItems(0).MenuItems(0).Checked = True

        'Ajout des autres menus
        For i = 0 To 15
            Dim myMenuItem As New MenuItem
            myMenuItem.Text = commandArray(i)
            If i = 0 Then myMenuItem.DefaultItem = True
            AddHandler myMenuItem.Click, AddressOf Me.commandClick
            Me.MyCM.MenuItems.Add(myMenuItem)
        Next i
    End Sub

    Private Sub loading()
        Me.AllowUserToAddRows = False
        Me.AllowUserToDeleteRows = False
        Me.AllowUserToOrderColumns = True
        Me.AllowUserToResizeRows = False
        Me.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        'Me.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IsFileAttached, Me.IsCompteLie, Me.AffDate, Me.BothFrom, Me.Subject, Me.FilesAttached, Me.IsRead, Me.NoMail, Me.ColTo, Me.CC, Me.NoClient, Me.FilesAttached2, Me.HasSentFeedBack, Me.Message})
        Me.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.RowHeadersVisible = False
        Me.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BackgroundColor = Color.White
        Me.MultiSelect = True

        'noItems label
        noItems.Text = "Aucun item"
        noItems.AutoSize = True
        noItems.BackColor = Color.Transparent
        noItems.Visible = True
        noItems.Font = New Font(noItems.Font.FontFamily, 12, FontStyle.Bold)
        Me.Controls.Add(noItems)
    End Sub

    Private Sub buildColumns()
        With Columns
            Dim newColumn As DataGridViewColumn

            newColumn = New DataGridViewImageColumn(False)
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            newColumn.HeaderText = ""
            newColumn.MinimumWidth = 16
            newColumn.ReadOnly = True
            newColumn.DataPropertyName = "LISTIMG"
            newColumn.Name = "LISTIMG"
            newColumn.Width = 16
            .Add(newColumn)

            newColumn = New DataGridViewTextBoxColumn
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            newColumn.HeaderText = "Titre"
            newColumn.DataPropertyName = "DBItem"
            newColumn.MinimumWidth = 16
            newColumn.Name = "DBItem"
            newColumn.Width = 150
            .Add(newColumn)

            If showDirColumn = True Then
                newColumn = New DataGridViewTextBoxColumn
                newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
                newColumn.HeaderText = "Dossier"
                newColumn.DataPropertyName = "DBFolder"
                newColumn.MinimumWidth = 16
                newColumn.Name = "DBFolder"
                newColumn.Width = 150
                .Add(newColumn)
            End If

            newColumn = New DataGridViewTextBoxColumn
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            newColumn.HeaderText = "Taille"
            newColumn.DataPropertyName = "Size"
            newColumn.MinimumWidth = 16
            newColumn.Name = "Size"
            newColumn.Width = 50
            newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Add(newColumn)

            newColumn = New DataGridViewTextBoxColumn
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            newColumn.HeaderText = "Mots-clés"
            newColumn.DataPropertyName = "MotsCles"
            newColumn.MinimumWidth = 16
            newColumn.Name = "MotsCles"
            newColumn.Width = 100
            .Add(newColumn)

            newColumn = New DataGridViewTextBoxColumn
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            newColumn.HeaderText = "Description"
            newColumn.DataPropertyName = "Description"
            newColumn.MinimumWidth = 16
            newColumn.Name = "Description"
            newColumn.Width = 100
            .Add(newColumn)

            newColumn = New DataGridViewTextBoxColumn
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            newColumn.HeaderText = "Modifié le"
            newColumn.DataPropertyName = "LastModifDate"
            newColumn.MinimumWidth = 16
            newColumn.Name = "LastModifDate"
            newColumn.Width = 100
            .Add(newColumn)

            newColumn = New DataGridViewCheckBoxColumn
            newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            newColumn.HeaderText = "Caché"
            newColumn.DataPropertyName = "IsHidden"
            newColumn.MinimumWidth = 16
            newColumn.Name = "IsHidden"
            newColumn.Width = 50
            .Add(newColumn)

            Dim hiddenColumns() As String = {"NoDBItem", "NoDBFolder", "DBItemFile", "NoFileType", "IsReadOnly", "CreationDate"}

            For i As Integer = 0 To hiddenColumns.GetUpperBound(0)
                newColumn = New DataGridViewTextBoxColumn
                newColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
                newColumn.HeaderText = hiddenColumns(i)
                newColumn.DataPropertyName = hiddenColumns(i)
                newColumn.MinimumWidth = 16
                newColumn.Name = hiddenColumns(i)
                newColumn.Width = 50
                newColumn.Visible = False
                .Add(newColumn)
            Next i
        End With
    End Sub
#End Region

#Region "Méthodes"
    Public Sub showFiles(ByVal curDBFolder As InternalDBFolder)
        Me.curDBFolder = curDBFolder
        internalShowFiles("DBItems INNER JOIN DBFolders ON DBFolders.NoDBFolder = DBItems.NoDBFolder", "WHERE DBItems.NoDBFolder=" & curDBFolder.noDBFolder)
        normalLoading = True
    End Sub

    Private Sub reloadFiles()
        internalShowFiles(LastFromStr, lastWhereStr)
    End Sub

    Private Sub internalShowFiles(ByVal fromStr As String, ByVal whereStr As String)
        LastFromStr = fromStr
        lastWhereStr = whereStr

        If currentDroitAcces(2) = False Then
            If whereStr <> "" Then
                If whereStr.Trim.StartsWith("WHERE") Then whereStr = whereStr.Substring(whereStr.IndexOf("WHERE") + 5)
                whereStr = "WHERE (" & whereStr & ") AND "
            End If
            whereStr &= "DBItems.IsHidden=0"
        End If

        Dim itemSelected As DataGridViewSelectedRowCollection = Me.SelectedRows
        Dim noItemSelected As New ArrayList
        For i As Integer = 0 To itemSelected.Count - 1
            noItemSelected.Add(itemSelected(i).Cells("NoDBItem").Value)
        Next i
        If Me.SortedColumn Is Nothing Then Me.Sort(Me.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        Dim orderingColName As String = Me.SortedColumn.Name
        Dim order As SortOrder = Me.SortOrder
        Dim sqlDirColumn As String = ""
        If showDirColumn Then sqlDirColumn = ",CAST(DBItems.NoDBFolder AS VARCHAR(MAX)) as DBFolder"
        Dim dsFiles As DataSet = DBLinker.getInstance().readDBForGrid(fromStr, "DBItems.DBItem" & sqlDirColumn & ",'0 octet' AS Size,[dbo].[fnGetDBItemMotsCles](NoDBItem) AS MotsCles,REPLACE(DBItems.Description,'<BR>',' ') AS Description,DBItems.LastModifDate,DBItems.IsHidden,DBItems.NoDBItem,DBItems.NoDBFolder,DBItems.DBItemFile,DBItems.NoFileType,DBItems.IsReadOnly,DBItems.CreationDate", whereStr)
        dsFiles.Tables(0).Columns.Add("LISTIMG", GetType(System.Drawing.Image))

        'Ensure DBFolders are OK. + Set Item size + Set Item icon
        With dsFiles.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim noDBFolder As Integer = 0
                If _ShowDirColumn AndAlso .Item(i)("DBFolder") IsNot Nothing AndAlso Integer.TryParse(.Item(i)("DBFolder"), noDBFolder) Then
                    .Item(i)("DBFolder") = InternalDBManager.getInstance.getDBFolder(noDBFolder)
                End If

                Dim myContentFile As String = appPath & bar(appPath) & "DB\" & .Item(i)("DBItemFile")
                If IO.File.Exists(myContentFile) Then
                    Dim contentFileInfo As New FileInfo(myContentFile)
                    .Item(i)("Size") = Fichiers.transformFileSizeToText(contentFileInfo.Length)
                End If

                Dim curType As TypeFile = TypesFilesManager.getInstance.getItemable(.Item(i)("NoFileType"))
                If curType IsNot Nothing Then
                    .Item(i)("LISTIMG") = Me.smallImageList.Images(curType.baseFileType)
                Else
                    .Item(i)("LISTIMG") = Me.smallImageList.Images(0)
                End If
            Next i
        End With
        Me.DataSource = dsFiles
        Me.DataMember = "Table"

        If order = SortOrder.Ascending Then
            Me.Sort(Me.Columns(orderingColName), System.ComponentModel.ListSortDirection.Ascending)
        Else
            Me.Sort(Me.Columns(orderingColName), System.ComponentModel.ListSortDirection.Descending)
        End If

        If CType(Me.DataSource, DataSet).Tables(0).Rows.Count = 0 Then
            noItems.Visible = True
        Else
            noItems.Visible = False
            If noItemSelected.Count <> 0 Then 'DoReselection AndAlso
                For i As Integer = 0 To Me.Rows.Count - 1
                    If noItemSelected.Contains(Me.Rows(i).Cells("NoDBItem").Value) Then
                        Me.Rows(i).Selected = True
                    Else
                        Me.Rows(i).Selected = False
                    End If
                Next i
            End If

            If Me.SelectedRows.Count = 0 Then Me.Rows(0).Selected = True
        End If
    End Sub

    Public Sub showFiles(ByVal fromStr As String, ByVal whereStr As String) 'Path of MyFiles from AppPath.
        If fromStr.IndexOf("DBFolders") < 0 And _ShowDirColumn Then fromStr &= " INNER JOIN DBFolders ON DBFolders.NoDBFolder = DBItems.NoDBFolder"
        internalShowFiles(fromStr, whereStr)
    End Sub
#End Region

    Protected Overrides Sub onMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim info As HitTestInfo = Me.HitTest(e.X, e.Y)
        If _AllowUserToDragRows AndAlso e.Button = System.Windows.Forms.MouseButtons.Left AndAlso didDoDragDrop = False AndAlso info.RowIndex <> -1 AndAlso Me.Rows(info.RowIndex).Selected = True Then
            didDoDragDrop = True
            Dim allowedEffects As DragDropEffects = DragDropEffects.Copy Or DragDropEffects.Move
            For Each curDBItem As InternalDBItem In Me.getDBItemsSelected
                If curDBItem.getDBFolder.noUser = 0 AndAlso curDBItem.isReadOnly AndAlso currentDroitAcces(4) = False Then allowedEffects = DragDropEffects.Copy : Exit For
                If curDBItem.getDBFolder.noUser = 0 AndAlso curDBItem.isReadOnly = False AndAlso currentDroitAcces(3) = False Then allowedEffects = DragDropEffects.Copy : Exit For
                If curDBItem.getDBFolder.noUser <> 0 AndAlso curDBItem.getDBFolder.noUser <> ConnectionsManager.currentUser AndAlso curDBItem.isReadOnly AndAlso currentDroitAcces(95) = False Then allowedEffects = DragDropEffects.Copy : Exit For
                If curDBItem.getDBFolder.noUser <> 0 AndAlso curDBItem.getDBFolder.noUser <> ConnectionsManager.currentUser AndAlso curDBItem.isReadOnly = False AndAlso currentDroitAcces(94) = False Then allowedEffects = DragDropEffects.Copy : Exit For
            Next
            Me.DoDragDrop(Me.getDBItemsSelected, allowedEffects)
        Else
            didDoDragDrop = False
            MyBase.OnMouseDown(e)
        End If
    End Sub

    Protected Overrides Sub onDataBindingComplete(ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs)
        If e.ListChangedType <> System.ComponentModel.ListChangedType.Reset Then
            MyBase.OnDataBindingComplete(e)
            Exit Sub
        End If

        Me.Columns("DBItem").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

        Dim totalWidth As Integer = 0
        For i As Integer = 0 To Columns.Count - 1
            If Columns(i).Visible Then totalWidth += Columns(i).Width
        Next i

        If Me.Width > totalWidth Then Me.Columns("DBItem").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        MyBase.OnDataBindingComplete(e)
    End Sub

    Private Sub fileListView_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles Me.CellBeginEdit

    End Sub

    Private Sub fileListView_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellClick
        If e.RowIndex = -1 Then Exit Sub
        If Me.Columns(e.ColumnIndex).Name <> "DBItem" Then
            oldName = Me.Rows(e.RowIndex).Cells("DBItem").Value
            Exit Sub
        End If
        If curRowDBItem Is Nothing Then
            oldName = ""
            Exit Sub
        End If


        If Me.SelectedRows.Count = 0 Then
            oldName = ""
        Else
            Dim currentItemDBFolder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(CInt(Me.Rows(e.RowIndex).Cells("NoDBFolder").Value))
            Dim currentItem As New InternalDBItem(CInt(Me.Rows(e.RowIndex).Cells("NoDBItem").Value))
            If currentDroitAcces(3) = True AndAlso ((currentDroitAcces(4) = True And currentItem.isReadOnly And currentItemDBFolder.noUser = 0) Or (currentDroitAcces(95) = True And currentItem.isReadOnly And currentItemDBFolder.noUser <> 0 And currentItemDBFolder.noUser <> ConnectionsManager.currentUser) Or currentItem.isReadOnly = False) AndAlso Me.SelectedRows.Count > 0 AndAlso oldName = Me.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Then
                Me.BeginEdit(True)
                'OldName = ""
            Else
                oldName = Me.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            End If
        End If
    End Sub

    Private Sub fileListView_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellEndEdit
        Dim curDBItem As New InternalDBItem(CInt(Me.Rows(e.RowIndex).Cells("NoDBItem").Value))
        Dim newName As String = Me.Rows(e.RowIndex).Cells("DBItem").Value
        If newName = curDBItem.dbItem Then Exit Sub
        curDBItem.dbItem = newName.Trim()
        Try
            curDBItem.saveData()
        Catch ex As Exception
            MessageBox.Show("Impossible de renommer l'item, car il est en cours de visualisation", "Erreur")
            Me.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = curDBItem.dbItem
        End Try
    End Sub

    Private Function getDBItemsSelected() As InternalDBItem()
        Dim selected(Me.SelectedRows.Count - 1) As InternalDBItem
        For i As Integer = 0 To selected.GetUpperBound(0)
            selected(i) = New InternalDBItem(CInt(Me.SelectedRows(i).Cells("NoDBItem").Value))
        Next i

        Return selected
    End Function

    Private Sub fileListView_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Me.CellMouseDoubleClick
        If e.RowIndex >= 0 Then
            If Me.showMenuSelect Then
                RaiseEvent itemSelected(Me, getDBItemsSelected)
            Else
                openSelected()
            End If
        End If
    End Sub

    Private Sub fileListView_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Me.CellMouseDown
        curMouseBouton = e.Button
    End Sub

    Private Sub fileListView_CellParsing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellParsingEventArgs) Handles Me.CellParsing

    End Sub

    Private Sub fileListView_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles Me.CellValidating
        If Me.Columns(e.ColumnIndex).Name <> "DBItem" Then Exit Sub
        If Me.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue = e.FormattedValue Then Exit Sub

        If e.FormattedValue = "" Then e.Cancel = True : Exit Sub
        Dim curDBItem As New InternalDBItem(CInt(Me.Rows(e.RowIndex).Cells("NoDBItem").Value))
        Dim path As String = curDBItem.getDBFolder.toString
        If InternalDBItem.exists(path, e.FormattedValue) = True AndAlso (New InternalDBItem(path, e.FormattedValue)).noDBItem <> curDBItem.noDBItem Then e.Cancel = True : Exit Sub
    End Sub

    Private Sub fileListView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click

    End Sub

    Private curItemEnLectureSeule As Boolean = False
    Private columnHeaderClicked As Boolean = False

    Private Sub fileListView_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Me.ColumnHeaderMouseClick
        columnHeaderClicked = True
    End Sub

    Private Sub fileListView_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles Me.DataError
        Dim a As Byte = 0

    End Sub

    Private Sub fileListView_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DragLeave

    End Sub

    Private Sub fileListView_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        AddHandler e.Control.TextChanged, AddressOf editingControl_TextChanged
    End Sub

    Private Sub editingControl_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Me.IsCurrentCellInEditMode Then
            Dim newText As String = Fichiers.replaceIllegalChars(Me.currentCell.EditedFormattedValue).TrimStart()
            If newText <> Me.EditingControl.Text Then
                With CType(Me.EditingControl, DataGridViewTextBoxEditingControl)
                    Dim oldPos As Integer = .SelectionStart
                    .Text = newText
                    If oldPos > .Text.Length Then
                        .SelectionStart = .Text.Length
                    Else
                        .SelectionStart = oldPos
                    End If
                End With
            End If
        End If
    End Sub

    Private Sub fileListView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If curRowDBItem IsNot Nothing AndAlso Me.IsCurrentCellInEditMode = False AndAlso e.KeyCode = Keys.Enter Then
            fileListView_CellMouseDoubleClick(sender, New System.Windows.Forms.DataGridViewCellMouseEventArgs(0, curRowDBItem.Index, MousePosition.X, MousePosition.Y, New MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 2, MousePosition.X, MousePosition.Y, 0)))
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub fileListView_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        listMousePosition = e.Location
        If e.Button = System.Windows.Forms.MouseButtons.Right AndAlso Me.HitTest(e.X, e.Y).RowIndex = -1 Then showListContextMenu(False)
    End Sub

    Private Sub showListContextMenu(ByVal fromRow As Boolean)
        If showMenu = False Then Exit Sub

        If fromRow = False Then
            Dim currentItemDBFolder As InternalDBFolder = Me.curDBFolder
            If currentItemDBFolder Is Nothing OrElse ((currentDroitAcces(3) = False AndAlso currentItemDBFolder.noUser = 0) Or (currentDroitAcces(94) = False AndAlso currentItemDBFolder.noUser <> 0 AndAlso currentItemDBFolder.noUser <> ConnectionsManager.currentUser)) Then
                MyCM.MenuItems(2).Enabled = False
                MyCM.MenuItems(11).Enabled = False
            Else
                MyCM.MenuItems(2).Enabled = True

                If Dir(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy", FileAttribute.Directory) = "" Then
                    MyCM.MenuItems(11).Enabled = False
                Else
                    MyCM.MenuItems(11).Enabled = True
                End If
            End If
            MyCM.MenuItems(1).Enabled = False
            MyCM.MenuItems(3).Enabled = False
            MyCM.MenuItems(5).Enabled = False
            MyCM.MenuItems(6).Enabled = False
            MyCM.MenuItems(7).Enabled = False
            MyCM.MenuItems(9).Enabled = False
            MyCM.MenuItems(10).Enabled = False
            MyCM.MenuItems(14).Enabled = False
            MyCM.MenuItems(15).Enabled = False
            MyCM.MenuItems(16).Enabled = False
        Else
            Dim currentItemDBFolder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(CInt(curRowDBItem.Cells("NoDBFolder").Value))
            curItemEnLectureSeule = curRowDBItem.Cells("IsReadOnly").Value
            Dim hasRightForNotReadOnly As Boolean = Not (curItemEnLectureSeule = False AndAlso ((currentDroitAcces(3) = False AndAlso currentItemDBFolder.noUser = 0) Or (currentDroitAcces(94) = False AndAlso currentItemDBFolder.noUser <> 0 AndAlso currentItemDBFolder.noUser <> ConnectionsManager.currentUser)))
            Dim hasRightForReadOnly As Boolean = Not (curItemEnLectureSeule AndAlso ((currentDroitAcces(4) = False AndAlso currentItemDBFolder.noUser = 0) Or (currentDroitAcces(95) = False AndAlso currentItemDBFolder.noUser <> ConnectionsManager.currentUser AndAlso currentItemDBFolder.noUser <> 0)))

            MyCM.MenuItems(2).Enabled = Not ((currentDroitAcces(3) = False AndAlso currentItemDBFolder.noUser = 0) Or (currentDroitAcces(94) = False AndAlso currentItemDBFolder.noUser <> 0 AndAlso currentItemDBFolder.noUser <> ConnectionsManager.currentUser))

            If hasRightForNotReadOnly = False OrElse hasRightForReadOnly = False Then
                MyCM.MenuItems(3).Enabled = False
                MyCM.MenuItems(11).Enabled = False
                MyCM.MenuItems(5).Enabled = False
            Else
                MyCM.MenuItems(3).Enabled = True
                MyCM.MenuItems(5).Enabled = True

                If Dir(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy", FileAttribute.Directory) = "" Then
                    MyCM.MenuItems(11).Enabled = False
                Else
                    MyCM.MenuItems(11).Enabled = True
                End If
            End If
            Dim allowedMultiple As Boolean = True
            For i As Integer = 0 To Me.SelectedRows.Count - 1
                curItemEnLectureSeule = Me.SelectedRows(i).Cells("IsReadOnly").Value
                currentItemDBFolder = InternalDBManager.getInstance.getDBFolder(CInt(Me.SelectedRows(i).Cells("NoDBFolder").Value))
                If curItemEnLectureSeule = False AndAlso ((currentDroitAcces(3) = False AndAlso currentItemDBFolder.noUser = 0) Or (currentDroitAcces(94) = False AndAlso currentItemDBFolder.noUser <> 0 AndAlso currentItemDBFolder.noUser <> ConnectionsManager.currentUser)) Then
                    allowedMultiple = False
                    Exit For
                End If
                If curItemEnLectureSeule AndAlso ((currentDroitAcces(4) = False AndAlso currentItemDBFolder.noUser = 0) Or (currentDroitAcces(95) = False AndAlso currentItemDBFolder.noUser <> ConnectionsManager.currentUser AndAlso currentItemDBFolder.noUser <> 0)) Then
                    allowedMultiple = False
                    Exit For
                End If
            Next i
            MyCM.MenuItems(1).Enabled = True
            MyCM.MenuItems(6).Enabled = allowedMultiple
            MyCM.MenuItems(7).Enabled = True
            MyCM.MenuItems(9).Enabled = allowedMultiple
            MyCM.MenuItems(10).Enabled = True
            MyCM.MenuItems(13).Enabled = True
            MyCM.MenuItems(14).Enabled = True
            MyCM.MenuItems(15).Enabled = True
            MyCM.MenuItems(16).Enabled = True

            'Droit & accès : Item en lecture seul
            curItemEnLectureSeule = curRowDBItem.Cells("IsReadOnly").Value
            currentItemDBFolder = InternalDBManager.getInstance.getDBFolder(CInt(curRowDBItem.Cells("NoDBFolder").Value))
            If (curItemEnLectureSeule AndAlso ((currentDroitAcces(4) = False AndAlso currentItemDBFolder.noUser = 0) Or (currentDroitAcces(95) = False AndAlso currentItemDBFolder.noUser <> ConnectionsManager.currentUser AndAlso currentItemDBFolder.noUser <> 0))) Then
                MyCM.MenuItems(3).Enabled = False
                MyCM.MenuItems(5).Enabled = False
                MyCM.MenuItems(6).Enabled = False
                MyCM.MenuItems(9).Enabled = False
            End If

            Dim allPrintable As Boolean = True
            For i As Integer = 0 To Me.SelectedRows.Count - 1
                Dim curType As TypeFile = TypesFilesManager.getInstance.getItemable(Me.SelectedRows(i).Cells("NoFileType").Value)
                If curType.printable = False Then allPrintable = False : Exit For
            Next i
            MyCM.MenuItems(7).Enabled = allPrintable
        End If

        'Droit d'ajouter un dossier ds le dossier présentement sélectionné
        If Me.curDBFolder IsNot Nothing Then
            If Me.curDBFolder.noUser = 0 Then
                MyCM.MenuItems(13).Enabled = currentDroitAcces(5)
            ElseIf Me.curDBFolder.noUser <> ConnectionsManager.currentUser Then
                MyCM.MenuItems(13).Enabled = currentDroitAcces(96)
            Else
                MyCM.MenuItems(13).Enabled = True
            End If
        End If

        MyCM.MenuItems(1).Visible = showMenuOpen
        MyCM.MenuItems(2).Visible = showMenuAdd
        MyCM.MenuItems(3).Visible = showMenuModif
        MyCM.MenuItems(4).Visible = showMenuSearchIn
        MyCM.MenuItems(5).Visible = showMenuRename
        MyCM.MenuItems(6).Visible = showMenuDel
        MyCM.MenuItems(9).Visible = showMenuCut
        MyCM.MenuItems(10).Visible = showMenuCopy
        MyCM.MenuItems(11).Visible = showMenuPaste
        MyCM.MenuItems(13).Visible = showMenuAddCat
        MyCM.MenuItems(14).Visible = showMenuAddFavoris
        MyCM.MenuItems(15).Visible = showMenuSearchDB
        MyCM.MenuItems(16).Visible = showMenuSelect

        MyCM.Show(Me, listMousePosition)
    End Sub

    Private Sub copyDBFile(Optional ByVal delOriginFile As Boolean = False)
        deltree(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy")

        Dim noFoldersToUpdate As New Generic.List(Of Integer)
        For i As Integer = 0 To Me.SelectedRows.Count - 1
            Dim curDBItem As New InternalDBItem(CInt(Me.SelectedRows(i).Cells("NoDBItem").Value))
            If delOriginFile Then
                Try
                    curDBItem.cut()
                    If noFoldersToUpdate.Contains(curDBItem.noDBFolder) = False Then noFoldersToUpdate.Add(curDBItem.noDBFolder)
                Catch ex As Exception
                    MessageBox.Show("Impposible de couper l'item (" & curDBItem.getDBFolder.toString & "\" & curDBItem.dbItem & "), car il est en cours de visualisation.", "Erreur")
                End Try
            Else
                curDBItem.copy()
            End If
        Next i
        If delOriginFile Then
            For i As Integer = 0 To noFoldersToUpdate.Count - 1
                InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & noFoldersToUpdate(i) & ")")
            Next i
            reloadFiles()
        End If
    End Sub

    Protected Overrides Sub finalize()
        MyBase.Finalize()
    End Sub

    Private Sub fileListView_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        'If curMouseBouton = Windows.Forms.MouseButtons.Left Then
        '    Dim pt As Point = Me.PointToClient(Control.MousePosition)
        '    Dim info As DataGridView.HitTestInfo = Me.HitTest(pt.X, pt.Y)
        '    If info.RowIndex <> -1 Then
        '        Dim view As DataRowView = Me.Rows(info.RowIndex).DataBoundItem
        '        Me.DoDragDrop(Me.GetDBItemsSelected, DragDropEffects.Move)
        '    End If
        'End If
    End Sub

    Private didDragDrop As Boolean = False

    Private Sub fileListView_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'If curMouseBouton = Windows.Forms.MouseButtons.Left And Me.SelectedRows.Count <> 0 Then
        '    Dim pt As Point = Me.PointToClient(Control.MousePosition)
        '    Dim info As DataGridView.HitTestInfo = Me.HitTest(pt.X, pt.Y)
        '    If info.RowIndex = -1 Then Exit Sub
        '    MyMainWin.StatusText = Me.Rows(info.RowIndex).Cells("DBItem").Value & ":" & Me.Rows(info.RowIndex).Selected
        '    If didDragDrop = False Then

        '    End If
        '    Me.DoDragDrop(Me.GetDBItemsSelected, DragDropEffects.Move)
        '    didDragDrop = True
        'End If
    End Sub

    Private Sub fileListView_RowContextMenuStripNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs) Handles Me.RowContextMenuStripNeeded
        If curMouseBouton = MouseButtons.Right Then
            '            
            If e.RowIndex <> -1 And Me.SelectedRows.Contains(Me.Rows(e.RowIndex)) = False Then
                If Form.ModifierKeys <> Keys.Control Then Me.ClearSelection()
                'Ensure message loaded
                Me.Rows(e.RowIndex).Selected = True
                curRowDBItem = Me.Rows(e.RowIndex)
            ElseIf e.RowIndex <> -1 Then
                curRowDBItem = Me.Rows(e.RowIndex)
            End If

            showListContextMenu(e.RowIndex <> -1)
        End If
    End Sub

    Protected Overrides Sub onSorted(ByVal e As System.EventArgs)
        MyBase.OnSorted(e)
    End Sub

    'Public Overloads Property CurrentCell() As DataGridViewCell
    '    Get
    '        Return MyBase.CurrentCell
    '    End Get
    '    Set(ByVal value As DataGridViewCell)
    '        MyBase.CurrentCell = value
    '    End Set
    'End Property

    Protected Overrides Sub onResize(ByVal e As System.EventArgs)
        noItems.Left = (Me.Width - noItems.Width) / 2
        noItems.Top = (Me.Height - noItems.Height) / 2

        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub onCurrentCellChanged(ByVal e As System.EventArgs)
        MyBase.OnCurrentCellChanged(e)
    End Sub

    Protected Overrides Sub onSelectionChanged(ByVal e As System.EventArgs)
        MyBase.OnSelectionChanged(e)
    End Sub

    Private Sub fileListView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SelectionChanged
        If Me.SelectedRows.Count = 0 Then
            curRowDBItem = Nothing
        Else
            curRowDBItem = Me.SelectedRows(0)
        End If
    End Sub

    Private Sub fileListView_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Sorted
        With Me
            If .SortOrder = SortOrder.Descending Then
                MyCM.MenuItems(0).MenuItems(1).Checked = True
                MyCM.MenuItems(0).MenuItems(0).Checked = False
            Else
                MyCM.MenuItems(0).MenuItems(1).Checked = False
                MyCM.MenuItems(0).MenuItems(0).Checked = True
            End If
        End With
    End Sub
End Class
