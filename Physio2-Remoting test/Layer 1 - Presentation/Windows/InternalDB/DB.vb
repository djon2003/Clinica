Friend Class DB
    Inherits SingleWindow

    Friend WithEvents LeftPanel As Panel
    Friend WithEvents RightPanel As Panel
    Friend WithEvents TitleCat As StatusBar
    Friend WithEvents TitleItem As StatusBar
    Friend WithEvents flvFiles As FileListView
    Friend WithEvents dtvwDirectory As DirectoryTreeView
    Friend WithEvents splitter As System.Windows.Forms.Splitter


#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.MdiParent = myMainWin
        'Add any initialization after the InitializeComponent() call
        Me.TitleCat.Panels("SPDossiers").Icon = DrawingManager.getInstance.getIcon("FolderClosed.ico")
        Me.dtvwDirectory.showHiddenFiles = currentDroitAcces(2)
        Me.dtvwDirectory.showMenuSearch = currentDroitAcces(6)
        Me.dtvwDirectory.ImageList.Images.Add(DrawingManager.getInstance.getImage("user.gif"))
        Me.dtvwDirectory.refreshTree()
        Me.flvFiles.showHiddenFiles = currentDroitAcces(2)
        Me.flvFiles.showMenuSearchDB = False
        Me.flvFiles.showMenuSearchIn = currentDroitAcces(6)
        Me.flvFiles.Sort(Me.flvFiles.Columns(1), System.ComponentModel.ListSortDirection.Ascending)


        Dim curUser As User = UsersManager.currentUser
        If curUser IsNot Nothing AndAlso curUser.settings.dbStyle <> String.Empty Then
            Try
                Dim thisSettings() As String = curUser.settings.dbStyle.Split(New Char() {"§"})
                Me.Height = thisSettings(0)
                Me.Width = thisSettings(1)

                flvFiles.ColumnSize("Titre") = thisSettings(3)
                flvFiles.ColumnSize("Taille") = thisSettings(4)
                flvFiles.ColumnSize("Mots-clés") = thisSettings(5)
                flvFiles.ColumnSize("Description") = thisSettings(6)
                flvFiles.ColumnSize("Modifié le...") = thisSettings(7)
                flvFiles.ColumnSize("Caché") = thisSettings(8)

                If flvFiles.Columns(thisSettings(2)) IsNot Nothing Then flvFiles.Sort(flvFiles.Columns(thisSettings(2)), IIf(thisSettings(9) = "D", 1, 0))

                splitter.Left = thisSettings(10)

                If thisSettings.Length > 11 AndAlso thisSettings(11) <> "" AndAlso thisSettings(11).LastIndexOf("\") <> -1 Then
                    selectedCat = thisSettings(11).Substring(0, thisSettings(11).LastIndexOf("\"))
                    selectedItem = thisSettings(11).Substring(thisSettings(11).LastIndexOf("\") + 1)
                Else
                    selectedCat = "Généraux"
                End If
            Catch
            End Try
        End If
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(DB))
        Me.RightPanel = New Panel
        Me.LeftPanel = New Panel
        Me.TitleCat = New StatusBar
        Me.TitleItem = New StatusBar
        Me.flvFiles = New FileListView
        Me.dtvwDirectory = New Clinica.DirectoryTreeView
        Me.splitter = New System.Windows.Forms.Splitter

        Me.flvFiles.AllowUserToOrderColumns = True
        Me.flvFiles.allowUserToDragRows = True
        Me.flvFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flvFiles.Location = New System.Drawing.Point(186, 0)
        Me.flvFiles.MultiSelect = True
        Me.flvFiles.Name = "flvFiles"
        Me.flvFiles.Size = New System.Drawing.Size(526, 400)
        Me.flvFiles.TabIndex = 4

        Me.dtvwDirectory.AllowDrop = True
        Me.dtvwDirectory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtvwDirectory.dragging = False
        Me.dtvwDirectory.expandAllNodes = False
        Me.dtvwDirectory.HideSelection = False
        Me.dtvwDirectory.Location = New System.Drawing.Point(0, 0)
        Me.dtvwDirectory.Name = "dtvwDirectory"
        Me.dtvwDirectory.properDrag = False
        Me.dtvwDirectory.showMenu = True
        Me.dtvwDirectory.Size = New System.Drawing.Size(184, 400)
        Me.dtvwDirectory.Sorted = True
        Me.dtvwDirectory.TabIndex = 0

        Me.splitter.Location = New System.Drawing.Point(184, 0)
        Me.splitter.Name = "splitter"
        Me.splitter.Size = New System.Drawing.Size(2, 400)
        Me.splitter.TabIndex = 1
        Me.splitter.TabStop = False

        Dim statusPanel As StatusBarPanel = New StatusBarPanel
        statusPanel.AutoSize = StatusBarPanelAutoSize.Spring
        statusPanel.Text = "Items"
        statusPanel.Name = "SPItems"
        statusPanel.ToolTipText = "Liste des items du dossier présentement sélectionné"
        Me.TitleItem.Font = New Font("Arial Black", 14)
        Me.TitleItem.Panels.Add(statusPanel)
        Me.TitleItem.Dock = DockStyle.Top
        Me.TitleItem.ShowPanels = True
        Me.TitleItem.SizingGrip = False
        Me.TitleItem.Height = 28

        Dim statusPanel2 As StatusBarPanel = New StatusBarPanel
        statusPanel2.AutoSize = StatusBarPanelAutoSize.Spring
        statusPanel2.Text = "Dossiers"
        statusPanel2.Name = "SPDossiers"
        statusPanel2.ToolTipText = "Liste des dossiers"
        Me.TitleCat.Font = New Font("Arial Black", 14)
        Me.TitleCat.Panels.Add(statusPanel2)
        Me.TitleCat.Dock = DockStyle.Top
        Me.TitleCat.SizingGrip = False
        Me.TitleCat.ShowPanels = True
        Me.TitleCat.Height = 28

        Me.LeftPanel.Dock = DockStyle.Left
        Me.RightPanel.Dock = DockStyle.Fill

        LeftPanel.Controls.Add(Me.dtvwDirectory)
        LeftPanel.Controls.Add(Me.TitleCat)
        RightPanel.Controls.Add(Me.flvFiles)
        RightPanel.Controls.Add(Me.TitleItem)
        Me.Controls.Add(Me.RightPanel)
        Me.Controls.Add(Me.splitter)
        Me.Controls.Add(Me.LeftPanel)

        '
        'DB
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(712, 400)
        Me.Name = "DB"
        Me.KeyPreview = True
        Me.ShowInTaskbar = False
        Me.Text = "Banque de données"

    End Sub

#End Region

    Private originationNode As Object
    Private _SelectedCat, _SelectedItem As String
    Private draggingFilePath As InternalDBFolder
    Private currentImportingFile As String = ""

#Region "Propriétés"
    Public Property selectedCat() As String
        Get
            Return _SelectedCat
        End Get
        Set(ByVal Value As String)
            _SelectedCat = Value
            selection()
        End Set
    End Property

    Public Property selectedItem() As String
        Get
            Return _SelectedItem
        End Get
        Set(ByVal Value As String)
            _SelectedItem = Value
            selection()
        End Set
    End Property
#End Region

    Private Sub db_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Enregistrement de l'affichage personnalisée de la DB
        Dim dbItemSelected As String = ""
        If flvFiles.currentRow IsNot Nothing AndAlso flvFiles.currentRow.DataGridView IsNot Nothing Then dbItemSelected = flvFiles.currentRow.Cells("DBItem").Value
        Dim curUser As User = UsersManager.currentUser
        curUser.settings.dbStyle = Me.Height & "§" & Me.Width & "§" & flvFiles.SortedColumn.Name & "§" & flvFiles.ColumnSize("Titre") & "§" & flvFiles.ColumnSize("Taille") & "§" & flvFiles.ColumnSize("Mots-clés") & "§" & flvFiles.ColumnSize("Description") & "§" & flvFiles.ColumnSize("Modifié le...") & "§" & flvFiles.ColumnSize("Caché") & "§" & IIf(flvFiles.SortOrder = SortOrder.Ascending, "A", "D") & "§" & Me.splitter.Left.ToString & "§" & dtvwDirectory.lastPath.Replace("'", "''") & "\" & dbItemSelected
        curUser.settings.saveData()
    End Sub

    Private Sub dtvwDirectory_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles dtvwDirectory.AfterSelect
        flvFiles.showMenu = Not e.Node.FullPath = "Utilisateurs"
        flvFiles.showFiles(CType(e.Node.Tag, InternalDBFolder))
    End Sub

    Private Sub itemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles dtvwDirectory.ItemDrag
        If e.Button <> System.Windows.Forms.MouseButtons.Left Then Exit Sub

        If sender.GetType.Name = "DirectoryTreeView" Then
            With CType(CType(sender, DirectoryTreeView).SelectedNode.Tag, InternalDBFolder)
                If .dbFolder = "" Or .noUser = -1 Then Exit Sub
                If (currentDroitAcces(5) = False AndAlso .noUser = 0) Or (currentDroitAcces(96) = False AndAlso .noUser <> ConnectionsManager.currentUser AndAlso .noUser <> 0) Then Exit Sub
            End With
        End If
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub dtvwDirectory_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles dtvwDirectory.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode") Or e.Data.GetDataPresent("Clinica.InternalDBItem[]") Then
            Dim sourceFolder As InternalDBFolder
            Dim allowDrag As Boolean = True

            If dtvwDirectory.properDrag = True Then
                e.Effect = DragDropEffects.Move
                originationNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
                sourceFolder = originationNode.Tag
                If (currentDroitAcces(5) = False AndAlso sourceFolder.noUser = 0) Or (currentDroitAcces(96) = False AndAlso sourceFolder.noUser <> ConnectionsManager.currentUser) Then allowDrag = False
            Else
                If e.KeyState And 8 Then
                    e.Effect = DragDropEffects.Copy
                Else
                    e.Effect = DragDropEffects.Move
                    Dim dbItems() As InternalDBItem = CType(e.Data.GetData("Clinica.InternalDBItem[]"), Clinica.InternalDBItem())
                    For Each curDBItem As InternalDBItem In dbItems
                        If curDBItem.getDBFolder.noUser = 0 AndAlso curDBItem.isReadOnly AndAlso currentDroitAcces(4) = False Then allowDrag = False : Exit For
                        If curDBItem.getDBFolder.noUser = 0 AndAlso curDBItem.isReadOnly = False AndAlso currentDroitAcces(3) = False Then allowDrag = False : Exit For
                        If curDBItem.getDBFolder.noUser <> 0 AndAlso curDBItem.getDBFolder.noUser <> ConnectionsManager.currentUser AndAlso curDBItem.isReadOnly AndAlso currentDroitAcces(95) = False Then allowDrag = False : Exit For
                        If curDBItem.getDBFolder.noUser <> 0 AndAlso curDBItem.getDBFolder.noUser <> ConnectionsManager.currentUser AndAlso curDBItem.isReadOnly = False AndAlso currentDroitAcces(94) = False Then allowDrag = False : Exit For
                    Next
                    'REM Useless If (CurrentDroitAcces(5) = False AndAlso sourceFolder.NoUser = 0) Or (CurrentDroitAcces(96) = False AndAlso sourceFolder.NoUser <> ConnectionsManager.currentUser) Then allowDrag = False
                End If
            End If

            If allowDrag Then
                dtvwDirectory.dragging = True
            Else
                dtvwDirectory.dragging = False
                e.Effect = DragDropEffects.None
            End If
        Else
            dtvwDirectory.dragging = False
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub internalFileSaving(ByVal type As String, ByVal filePath As String)
        IO.File.Copy(currentImportingFile, filePath, True)
    End Sub

    Private Sub dtvwDirectory_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles dtvwDirectory.DragDrop
        Dim formats() As String = e.Data.GetFormats()
        Dim pt As Point
        pt = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        Dim destinationNode As TreeNode = CType(sender, TreeView).GetNodeAt(pt)
        Dim endPath As String = destinationNode.FullPath
        Dim myOPP As String = ""
        If endPath = "Utilisateurs" Then Exit Sub

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            If Not originationNode.parent Is Nothing Then myOPP = originationNode.parent.fullpath
            Dim sw As Boolean = False
            sw = endPath.StartsWith(originationNode.fullpath & "\")
            If CType(destinationNode, TreeNode).Nodes.Find(CType(originationNode, TreeNode).Text, False).Length = 0 AndAlso Not endPath = originationNode.fullpath And Not endPath = myOPP And sw = False Then
                If PreferencesManager.getGeneralPreferences()("ConfirmDBDragDrop") = True AndAlso MessageBox.Show("Êtes-vous sûr de vouloir déplacer ce dossier ?", "Confirmation de déplacement", MessageBoxButtons.YesNo) = DialogResult.No Then e.Effect = DragDropEffects.None : Exit Sub

                Dim oNode As InternalDBFolder = CType(CType(originationNode, TreeNode).Tag, InternalDBFolder)
                Dim dNode As InternalDBFolder = CType(CType(destinationNode, TreeNode).Tag, InternalDBFolder)
                oNode.dbFolder = dNode.dbFolder & bar(dNode.dbFolder) & oNode.dbFolder.Substring(oNode.dbFolder.LastIndexOf("\") + 1)
                oNode.noUser = dNode.noUser
                oNode.saveData()

                dtvwDirectory.refreshTree(destinationNode)
            End If
        ElseIf e.Data.GetDataPresent("FileDrop", False) Then
            Dim dNode As InternalDBFolder = CType(CType(destinationNode, TreeNode).Tag, InternalDBFolder)
            Dim filename As String = ""
            Dim curExt As String = ""
            Dim itemName As String = ""
            Dim myMotsCles() As String = New String() {}
            Dim myDescription() As String = New String() {}
            AddHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving

            For Each curFile As String In e.Data.GetData("FileDrop")
                currentImportingFile = curFile
                filename = curFile.Substring(curFile.LastIndexOf("\") + 1)
                curExt = filename.Substring(filename.LastIndexOf(".") + 1)
                itemName = filename.Substring(0, filename.Length - curExt.Length - 1)
                Dim curType As TypeFile = TypesFilesManager.getInstance.getTypeFileFromExt(curExt)

                Dim returning As String = InternalDBManager.getInstance.addItem(itemName, dNode, curType.toString, False, myMotsCles, myDescription, False, True, filename)
                If returning <> "" Then
                    MessageBox.Show(returning, "Erreur")
                    Exit For
                End If
            Next

            RemoveHandler InternalDBManager.getInstance.internalFileSaving, AddressOf internalFileSaving
        ElseIf e.Data.GetDataPresent(GetType(InternalDBItem).ToString & "[]", False) Then
            Dim dbItems() As InternalDBItem = e.Data.GetData(GetType(InternalDBItem).ToString & "[]")
            Dim plural As String = ""
            If dbItems.Length > 1 Then plural = "s"
            Dim verb As String = "déplacer"
            Dim action As String = "déplacement"
            If e.Effect = DragDropEffects.Copy Then verb = "copier" : action = "copie"

            If PreferencesManager.getGeneralPreferences()("ConfirmDBDragDrop") = True AndAlso MessageBox.Show("Êtes-vous sûr de vouloir " & verb & " ce" & IIf(plural = "", "t", plural) & " item" & plural & " ?", "Confirmation de " & action, MessageBoxButtons.YesNo) = DialogResult.No Then e.Effect = DragDropEffects.None : Exit Sub
            Dim dNode As InternalDBFolder = CType(CType(destinationNode, TreeNode).Tag, InternalDBFolder)
            Dim oldNoDBFolder As Integer = dbItems(0).noDBFolder
            If oldNoDBFolder = dNode.noDBFolder Then Exit Sub

            For i As Integer = 0 To dbItems.Length - 1
                If e.Effect = DragDropEffects.Copy Then
                    deltree(appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy")
                    dbItems(i).copy()
                    InternalDBItem.paste(dbItems(i).noDBItem, dNode)
                Else
                    If InternalDBItem.exists(dNode.toString, dbItems(i).dbItem) Then Continue For
                    dbItems(i).noDBFolder = dNode.noDBFolder
                    dbItems(i).saveData()
                End If
            Next i
            If e.Effect = DragDropEffects.Move Then InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & oldNoDBFolder & ")")
            InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & dNode.noDBFolder & ")")
        End If
        dtvwDirectory.dragging = False
    End Sub

    Private Sub dtvwDirectory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dtvwDirectory.MouseUp
        If dtvwDirectory.dragging = True Then
            If dtvwDirectory.properDrag = True Then dtvwDirectory.SelectedNode = originationNode
            dtvwDirectory.dragging = False
        End If
    End Sub

    Private Sub dtvwDirectory_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtvwDirectory.DragLeave
        dtvwDirectory.dragging = False
    End Sub

    Private Sub dtvwDirectory_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles dtvwDirectory.DragOver
        Dim pt As Point
        pt = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        dtvwDirectory.SelectedNode = dtvwDirectory.GetNodeAt(pt)
        If dtvwDirectory.SelectedNode Is Nothing Then
            e.Effect = DragDropEffects.None
        Else
            Dim curDBFolder As InternalDBFolder = dtvwDirectory.SelectedNode.Tag
            Dim canDrop As Boolean = (currentDroitAcces(5) = True AndAlso curDBFolder.noUser = 0) OrElse (currentDroitAcces(96) = True AndAlso curDBFolder.noUser <> ConnectionsManager.currentUser AndAlso curDBFolder.noUser <> 0) OrElse curDBFolder.noUser = ConnectionsManager.currentUser
            Dim forceCopy As Boolean = False
            If e.Data.GetDataPresent(GetType(InternalDBItem).ToString & "[]") Then
                canDrop = True
                Dim dbItems() As InternalDBItem = CType(e.Data.GetData(GetType(InternalDBItem).ToString & "[]"), Clinica.InternalDBItem())
                For Each curDBItem As InternalDBItem In dbItems
                    If curDBFolder.noDBFolder = curDBItem.getDBFolder.noDBFolder Then canDrop = False : Exit For
                    If curDBFolder.noUser = 0 AndAlso curDBItem.isReadOnly AndAlso currentDroitAcces(4) = False Then canDrop = False : Exit For
                    If curDBFolder.noUser = 0 AndAlso curDBItem.isReadOnly = False AndAlso currentDroitAcces(3) = False Then canDrop = False : Exit For
                    If curDBFolder.noUser <> 0 AndAlso curDBFolder.noUser <> ConnectionsManager.currentUser AndAlso curDBItem.isReadOnly AndAlso currentDroitAcces(95) = False Then canDrop = False : Exit For
                    If curDBFolder.noUser <> 0 AndAlso curDBFolder.noUser <> ConnectionsManager.currentUser AndAlso curDBItem.isReadOnly = False AndAlso currentDroitAcces(94) = False Then canDrop = False : Exit For
                Next
            ElseIf e.Data.GetDataPresent("FileDrop") Then
                forceCopy = True
                Dim testedExtensions As New Generic.List(Of String)
                Dim curExt As String
                For Each curFile As String In e.Data.GetData("FileDrop")
                    curExt = curFile.Substring(curFile.LastIndexOf(".") + 1)
                    If Not testedExtensions.Contains(curExt) Then
                        If TypesFilesManager.getInstance.getTypeFileFromExt(curExt) Is Nothing Then
                            canDrop = False
                            Exit For
                        End If
                        testedExtensions.Add(curExt)
                    End If
                Next
            Else
                Dim myOPP As String = ""
                If Not originationNode.parent Is Nothing Then myOPP = originationNode.parent.fullpath
                Dim sw As Boolean = False
                sw = dtvwDirectory.SelectedNode.FullPath.StartsWith(originationNode.fullpath & "\")

                If dtvwDirectory.SelectedNode.Nodes.Find(CType(originationNode, TreeNode).Text, False).Length <> 0 OrElse dtvwDirectory.SelectedNode.FullPath = originationNode.fullpath OrElse dtvwDirectory.SelectedNode.FullPath = myOPP OrElse sw Then canDrop = False
            End If
            If curDBFolder.noUser = -1 OrElse canDrop = False OrElse (e.KeyState <> 9 AndAlso (e.AllowedEffect And DragDropEffects.Move) <> DragDropEffects.Move) Then
                e.Effect = DragDropEffects.None
            Else
                If forceCopy OrElse (dtvwDirectory.properDrag = False AndAlso (e.KeyState = 9)) Then
                    e.Effect = DragDropEffects.Copy
                Else
                    e.Effect = DragDropEffects.Move
                End If
            End If
        End If
    End Sub

    Private Sub flvFiles_MenuAddACat() Handles flvFiles.menuAddACat
        dtvwDirectory.addingACat()
    End Sub

    Private Sub selection()
        If selectedCat <> "" AndAlso (dtvwDirectory.SelectedNode Is Nothing OrElse dtvwDirectory.SelectedNode.FullPath <> selectedCat) Then dtvwDirectory.selectNode(selectedCat)
        If dtvwDirectory.SelectedNode Is Nothing AndAlso dtvwDirectory.Nodes.Count <> 0 Then dtvwDirectory.SelectedNode = dtvwDirectory.Nodes(0)

        If selectedItem = "" Then Exit Sub
        Me.flvFiles.ClearSelection()
        For Each curRow As DataGridViewRow In flvFiles.Rows
            If curRow.Cells("DBItem").Value = selectedItem Then curRow.Selected = True : Exit For
        Next curRow
        If Me.flvFiles.Rows.Count <> 0 And Me.flvFiles.SelectedRows.Count = 0 Then Me.flvFiles.Rows(0).Selected = True
    End Sub


    Private Sub db_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        selection()
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function = "DBFolderItems" Then
            Dim noDBFolder As Integer = dataReceived.params(0)
            If noDBFolder = 0 OrElse flvFiles.dbFolder.noDBFolder = noDBFolder Then flvFiles.showFiles(flvFiles.dbFolder)
        End If
        If dataReceived.function = "DBItems" Then
            flvFiles.showFiles(flvFiles.dbFolder)
        End If

        If dataReceived.function.StartsWith("FLB-" & InternalDBManager.getInstance.folderType.ToString) Then
            dtvwDirectory.refreshTree()
        End If
    End Sub
End Class
