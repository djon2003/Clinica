Imports System.IO

Class DirectoryTreeView
    Inherits TreeViewPlus

    Private _ProperDrag As Boolean = False
    Private _Dragging As Boolean = False
    'Private _ExpandAllNodes As Boolean = False
    Friend WithEvents MyCM As System.Windows.Forms.ContextMenu
    Private _ShowMenu As Boolean = True
    Private totalNodes As Integer = 0
    Private _ShowHiddenFiles As Boolean = True
    Private _ShowMenuSearch As Boolean = True
    Private _ShowMenuDel As Boolean = True
    Private _ShowMenuAdd As Boolean = True
    Private _ShowMenuRename As Boolean = True
    Private oldPath As String = "" 'Last item clicked
    Private lastButton As MouseButtons
    Private lastReturnPath As String = ""

#Region "Menu click"
    Private Sub catClick(ByVal sender As Object, ByVal e As EventArgs)
        If Me.SelectedNode Is Nothing Then Exit Sub
        Dim myText As String = Me.SelectedNode.Text
        Dim endPath As String = Me.SelectedNode.FullPath
        If myText = "" Then Exit Sub

        Select Case CType(sender, MenuItem).Text
            Case "Ajouter un dossier"
                addingACat()

            Case "Renommer ce dossier"
                Me.LabelEdit = True
                Me.SelectedNode.BeginEdit()

            Case "Supprimer ce dossier"
                If MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce dossier ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

                Dim folderToDelete As InternalDBFolder = CType(Me.SelectedNode.Tag, InternalDBFolder)
                folderToDelete.delete()
                Me.refreshTree()
                Me.selectNode(folderToDelete.toString.Substring(0, folderToDelete.toString.LastIndexOf(Me.PathSeparator)))

            Case "Rechercher dans..."
                Dim mySearchDB As SearchDB = openUniqueWindow(New SearchDB)
                mySearchDB.selectedCat = endPath
                mySearchDB.Show()

            Case "Caché"
                With CType(Me.SelectedNode.Tag, InternalDBFolder)
                    .isHidden = Not .isHidden
                    .saveData()
                End With
        End Select
    End Sub
#End Region

#Region "Propriétés"
    Public ReadOnly Property count() As Integer
        Get
            Return totalNodes
        End Get
    End Property

    Public Property showMenu() As Boolean
        Get
            Return _ShowMenu
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenu = Value
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

    Public Property properDrag() As Boolean
        Get
            Return _ProperDrag
        End Get
        Set(ByVal Value As Boolean)
            _ProperDrag = Value
        End Set
    End Property

    Public ReadOnly Property lastPath() As String
        Get
            Return lastReturnPath
        End Get
    End Property

    Public Property dragging() As Boolean
        Get
            Return _Dragging
        End Get
        Set(ByVal Value As Boolean)
            _Dragging = Value
        End Set
    End Property

    Public Property showHiddenFiles() As Boolean
        Get
            Return _ShowHiddenFiles
        End Get
        Set(ByVal Value As Boolean)
            _ShowHiddenFiles = Value
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

    Public Property showMenuDel() As Boolean
        Get
            Return _ShowMenuDel
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuDel = Value
        End Set
    End Property

    Public Property showMenuSearch() As Boolean
        Get
            Return _ShowMenuSearch
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenuSearch = Value
        End Set
    End Property
#End Region

    Public Sub New()
        Dim catArray() As String = {"Ajouter un dossier", "Rechercher dans...", "Renommer ce dossier", "Supprimer ce dossier", "Caché"}
        Dim i As Byte
        Me.Width *= 2

        Me.MyCM = New System.Windows.Forms.ContextMenu
        For i = 0 To 4
            Dim myMenuItem As New MenuItem
            myMenuItem.Text = catArray(i)
            AddHandler myMenuItem.Click, New EventHandler(AddressOf Me.catClick)
            Me.MyCM.MenuItems.Add(myMenuItem)
        Next i
    End Sub

    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            For i As Integer = 0 To MyCM.MenuItems.Count - 1
                RemoveHandler MyCM.MenuItems(i).Click, New EventHandler(AddressOf Me.catClick)
            Next i
        End If
        MyBase.Dispose(disposing)
    End Sub

    Public Overrides Sub refreshTree(ByVal expanded As Generic.List(Of String))
        Dim dbfolders() As InternalDBFolder
        Dim curDBFolders As Generic.List(Of InternalDBFolder) = InternalDBManager.getInstance.getDBFolders()
        ReDim dbfolders(curDBFolders.Count)
        dbfolders(0) = New InternalDBFolder()
        dbfolders(0).noUser = -1
        dbfolders(0).dbFolder = "Utilisateurs"
        dbfolders(0).iconIndex = 2
        dbfolders(0).iconSelectedIndex = 2
        curDBFolders.CopyTo(0, dbfolders, 1, curDBFolders.Count)

        Me.tree = dbfolders
        If expanded.Count = 0 AndAlso Me.SelectedNode IsNot Nothing Then expanded.Add(Me.SelectedNode.Tag.ToString)
        MyBase.refreshTree(expanded)
    End Sub

    Private Sub directoryTreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.AfterSelect
        If e.Node Is Nothing Then
            lastReturnPath = ""
        Else
            lastReturnPath = e.Node.FullPath
        End If
    End Sub

    Private Sub directoryTreeView_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode") Then
            properDrag = True
        Else
            properDrag = False
        End If
    End Sub

    Private Sub directoryTreeView_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles MyBase.AfterLabelEdit
        If Me.LabelEdit = False Then Exit Sub
        If justBeingEdit Then justBeingEdit = False : e.Node.BeginEdit() : Exit Sub
        Me.LabelEdit = False

        If e.Label Is Nothing Then e.CancelEdit = True : Exit Sub
        If e.Node.Text.ToUpper = e.Label.ToUpper Then e.CancelEdit = True : Exit Sub
        If e.Label = "" Then e.CancelEdit = True : Exit Sub
        If e.Node.Parent.Nodes.Find(e.Label, False).Length > 0 Then e.CancelEdit = True : Exit Sub

        With CType(e.Node.Tag, InternalDBFolder)
            .dbFolder = .dbFolder.Substring(0, .dbFolder.LastIndexOf(Me.PathSeparator) + 1) & e.Label.Replace(Me.PathSeparator, "-")
            Me.SelectedNode.Text = getLastDir(.dbFolder)
            oldPath = Me.SelectedNode.FullPath
            .saveData()
        End With
    End Sub

    Private Sub directoryTreeView_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles MyBase.BeforeLabelEdit
        Dim curDBFolder As InternalDBFolder = Me.SelectedNode.Tag
        If curDBFolder.dbFolder = "" Or curDBFolder.noUser = -1 Then Me.SelectedNode.EndEdit(True)
    End Sub

    Private Sub directoryTreeView_BeforeCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles MyBase.BeforeCollapse
        If Me.ShowPlusMinus = False Then e.Cancel = True
    End Sub

    Public Sub addingACat()
        Dim endPath As String = Me.SelectedNode.FullPath

        Dim myInputBoxPlus As New InputBoxPlus
        myInputBoxPlus.firstLetterCapital = True
        Dim newCat As String = myInputBoxPlus.Prompt("Veuillez entrer le nom du nouveau dossier à insérer dans :" & vbCrLf & Me.SelectedNode.FullPath, "Nouveau dossier", "Nouveau dossier")
        If newCat = "" Then Me.AllowDrop = True : Exit Sub

        Dim returnOfAdding As String = InternalDBManager.getInstance.addDBFolder(Me.SelectedNode.FullPath & Me.PathSeparator & newCat)
        If returnOfAdding <> "" Then
            MessageBox.Show(returnOfAdding, "Erreur")
            Exit Sub
        End If

        If Not TypeOf Me.FindForm Is DB Then Me.refreshTree()
        Me.selectNode(endPath & Me.PathSeparator & newCat)
    End Sub

    Private justBeingEdit As Boolean = False
    Private Sub directoryTreeView_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        Dim curDBFolder As InternalDBFolder = CType(e.Node.Tag, InternalDBFolder)
        If ((currentDroitAcces(5) = True And curDBFolder.noUser = 0) Or (currentDroitAcces(96) And curDBFolder.noUser <> 0) Or curDBFolder.noUser = ConnectionsManager.currentUser) AndAlso (curDBFolder.dbFolder <> "" And curDBFolder.noUser <> -1) AndAlso e.Node.FullPath = oldPath Then
            If e.X >= e.Node.Bounds.X And e.X <= (e.Node.Bounds.X + e.Node.Bounds.Width) And e.Y >= e.Node.Bounds.Y And e.Y < (e.Node.Bounds.Y + e.Node.Bounds.Height) Then
                Me.LabelEdit = True
                Me.SelectedNode.BeginEdit()
                justBeingEdit = True
            End If
            oldPath = ""
        Else
            oldPath = e.Node.FullPath
        End If
    End Sub

    Private Sub directoryTreeView_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        oldPath = ""
    End Sub

    Private Sub DirectoryTreeView_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Try
            If Me.SelectedNode Is Nothing Then Exit Sub
            MyCM.MenuItems(0).Enabled = True
            MyCM.MenuItems(1).Enabled = True
            Dim tmpNode As TreeNode = Me.GetNodeAt(New Point(e.X, e.Y))
            If tmpNode Is Nothing Then Exit Sub
            If Not tmpNode Is Me.SelectedNode Then Me.SelectedNode = Me.GetNodeAt(New Point(e.X, e.Y))
            Dim hierarchyDepth As Integer = Me.SelectedNode.FullPath.Split(New Char() {Me.PathSeparator}).Length
            If Me.SelectedNode.FullPath = "Généraux" Or (Me.SelectedNode.FullPath.StartsWith("Utilisateurs") And hierarchyDepth <= 2) Then
                If Me.SelectedNode.FullPath = "Utilisateurs" Then MyCM.MenuItems(0).Enabled = False
                MyCM.MenuItems(2).Enabled = False
                MyCM.MenuItems(3).Enabled = False
                MyCM.MenuItems(4).Enabled = False
                MyCM.MenuItems(4).Checked = False
            Else
                If CType(Me.SelectedNode.Tag, InternalDBFolder).isHidden Then
                    MyCM.MenuItems(4).Checked = True
                Else
                    MyCM.MenuItems(4).Checked = False
                End If

                'Droit & Acces
                With CType(Me.SelectedNode.Tag, InternalDBFolder)
                    If .noUser = 0 Then
                        MyCM.MenuItems(4).Enabled = currentDroitAcces(5)
                        MyCM.MenuItems(2).Enabled = currentDroitAcces(5)
                        MyCM.MenuItems(3).Enabled = currentDroitAcces(5)
                    ElseIf .noUser <> ConnectionsManager.currentUser Then
                        MyCM.MenuItems(4).Enabled = currentDroitAcces(96)
                        MyCM.MenuItems(2).Enabled = currentDroitAcces(96)
                        MyCM.MenuItems(3).Enabled = currentDroitAcces(96)
                    Else
                        MyCM.MenuItems(4).Enabled = True
                        MyCM.MenuItems(2).Enabled = True
                        MyCM.MenuItems(3).Enabled = True
                    End If
                End With
            End If

            'Droit & Acces
            With CType(Me.SelectedNode.Tag, InternalDBFolder)
                If .noUser = 0 Then
                    MyCM.MenuItems(0).Enabled = currentDroitAcces(5)
                ElseIf .noUser <> ConnectionsManager.currentUser AndAlso .noUser > 0 Then
                    MyCM.MenuItems(0).Enabled = currentDroitAcces(96)
                End If
            End With
        Catch
            MyCM.MenuItems(0).Enabled = False
            MyCM.MenuItems(1).Enabled = False
            MyCM.MenuItems(2).Enabled = False
            MyCM.MenuItems(3).Enabled = False
            MyCM.MenuItems(4).Enabled = False
            MyCM.MenuItems(4).Checked = False
        Finally
            If showMenuAdd = False Then MyCM.MenuItems(0).Visible = False
            If showMenuRename = False Then MyCM.MenuItems(2).Visible = False
            If showMenuSearch = False Then MyCM.MenuItems(1).Visible = False
            If showMenuDel = False Then MyCM.MenuItems(3).Visible = False
            If showHiddenFiles = False Then MyCM.MenuItems(4).Visible = False

            If e.Button = MouseButtons.Right And showMenu = True Then
                justBeingEdit = False
                Me.LabelEdit = False
                MyCM.Show(Me, New Point(e.X, e.Y))
            End If
        End Try
    End Sub
End Class
