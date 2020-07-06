Public Class TreeViewPlus
    Inherits System.Windows.Forms.TreeView

    Private Class BaseTreeComparer
        Implements IComparer

        Public Function compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            If y Is Nothing OrElse x Is Nothing Then Return False
            Return x.ToString.CompareTo(y.ToString)
        End Function
    End Class

    Private _ExpandAllNodes As Boolean
    Private _Tree() As Object
    Private totalNodes As Integer = 0
    Private mySelection As New ArrayList
    Private _sorted As Boolean = True
    Private _readOnly As Boolean = False
    Private _showImages As Boolean = True

    Public Sub New()
        Me.ImageList = New ImageList()

        Try
            With Me.ImageList.Images
                .Add(DrawingManager.getInstance.getIcon("FolderClosed.ico"))
                .Add(DrawingManager.getInstance.getIcon("FolderOpened.ico"))
            End With
        Catch 'Protection for form designer
        End Try
    End Sub


#Region "Properties"

    Public Property [readOnly]() As Boolean
        Get
            Return _readOnly
        End Get
        Set(ByVal value As Boolean)
            _readOnly = value
        End Set
    End Property

    Public Property showImages() As Boolean
        Get
            Return _showImages
        End Get
        Set(ByVal value As Boolean)
            _showImages = value
        End Set
    End Property

    Public Property expandAllNodes() As Boolean
        Get
            Return _ExpandAllNodes
        End Get
        Set(ByVal Value As Boolean)
            _ExpandAllNodes = Value
            If _Tree Is Nothing OrElse _Tree.Length = 0 Then Exit Property

            If Value = True Then
                totalNodes = 0
                expandingAll(Me)
            Else
                collapsingAll(Me)
            End If
        End Set
    End Property

    Public Property tree() As Object()
        Get
            Return _Tree
        End Get
        Set(ByVal Value As Object())
            _Tree = Value
        End Set
    End Property
#End Region

    Public Overloads Sub refreshTree()
        refreshTree("")
    End Sub

    Public Overloads Sub refreshTree(ByVal selectedTree As TreeNode)
        Dim expanded As String = ""
        If selectedTree IsNot Nothing Then expanded = selectedTree.Tag.ToString
        refreshTree(expanded)
        If expanded <> "" Then expansion(expanded)
    End Sub

    Public Overloads Sub refreshTree(ByVal expanded As String)
        Dim expandedList As New Generic.List(Of String)
        If expanded IsNot Nothing AndAlso expanded <> "" Then expandedList.Add(expanded)
        refreshTree(expandedList)
    End Sub

    Public Overridable Overloads Sub refreshTree(ByVal expanded As Generic.List(Of String))
        If tree Is Nothing Then Exit Sub

        With Me
            .BeginUpdate()

            .SelectedNode = Nothing
            .Nodes.Clear()

            Dim strTree As Object
            If Me.Sorted Then Array.Sort(tree, New BaseTreeComparer())
            For i As Integer = 0 To tree.GetUpperBound(0)
                strTree = tree(i)
                If strTree IsNot Nothing Then
                    If strTree.ToString.IndexOf(Me.PathSeparator) < 0 And strTree.ToString <> "" Then
                        addSubNodes(addNode(Me, strTree), i)
                    End If
                End If
            Next

            setExpanded(expanded)
            .EndUpdate()
        End With
    End Sub

    Private Function addNode(ByVal parent As Object, ByVal strTree As Object) As TreeNode
        Dim sstrTree() As String = Split(strTree.ToString, PathSeparator)
        Dim tnFolder As TreeNode
        'Add icon
        If _showImages Then
            Dim iconIndex As Integer = 0
            Dim selectedIndex As Integer = 1
            If TypeOf strTree Is FolderBase Then
                With CType(strTree, FolderBase)
                    If .iconIndex >= 0 AndAlso .iconIndex < Me.ImageList.Images.Count Then iconIndex = .iconIndex
                    If .iconSelectedIndex >= 0 AndAlso .iconSelectedIndex < Me.ImageList.Images.Count Then selectedIndex = .iconSelectedIndex
                End With
            End If
            tnFolder = New TreeNode(sstrTree(sstrTree.GetUpperBound(0)), iconIndex, selectedIndex)
        Else
            tnFolder = New TreeNode(sstrTree(sstrTree.GetUpperBound(0)))
        End If

        'Bold item
        Dim nbNewItems As Integer = 0
        If TypeOf strTree Is FolderBase Then
            With CType(strTree, FolderBase)
                If .bold Then
                    If tnFolder.NodeFont IsNot Nothing Then
                        tnFolder.NodeFont = New Font(tnFolder.NodeFont, FontStyle.Bold)
                    Else
                        tnFolder.NodeFont = New Font(Me.Font, FontStyle.Bold)
                    End If
                End If
                nbNewItems = .nbUnreadItems
            End With
        End If

        tnFolder.Tag = strTree
        tnFolder.Name = getLastDir(strTree.ToString, PathSeparator) & If(nbNewItems = 0, "", " (" & nbNewItems & ")")
        tnFolder.Text = tnFolder.Name
        parent.Nodes.Add(tnFolder)

        Return tnFolder
    End Function

    Public Function searchNode(ByVal myNode As String) As String
        If tree Is Nothing OrElse tree.Length = 0 Then Return ""

        For i As Integer = 0 To tree.GetUpperBound(0)
            If tree(i) IsNot Nothing AndAlso tree(i).ToString.IndexOf(myNode) >= 0 Then Return tree(i)
        Next i

        Return ""
    End Function

    Private Sub addSubNodes(ByVal tn As TreeNode, ByRef index As Integer)
        Dim passedIndex As Integer = index
        tn.Nodes.Clear()
        Dim currentDirPos As Short = Split(tn.Tag.ToString, PathSeparator).Length + 1

        Dim sstrTree() As String
        Dim strTree As Object
        Dim curTree As New ArrayList

        For i As Integer = index To tree.GetUpperBound(0)
            strTree = tree(i)
            If strTree IsNot Nothing Then
                sstrTree = Split(strTree.ToString, PathSeparator)
                If sstrTree.Length = currentDirPos And strTree.ToString.StartsWith(tn.Tag.ToString & PathSeparator) Then
                    'Dim tnDir As New TreeNode(sstrTree(sstrTree.GetUpperBound(0)), 0, 1)
                    'tnDir.Tag = strTree
                    'tnDir.Name = getLastDir(strTree.ToString)
                    'tn.Nodes.Add(tnDir)

                    addSubNodes(addNode(tn, strTree), i)
                    If expandAllNodes = True Then tn.ExpandAll()
                End If
            End If
        Next i

        index = passedIndex
    End Sub

    Public Sub expansion(ByVal myPath As String, Optional ByVal checkLast As Boolean = False, Optional ByVal opposite As Boolean = False)
        If myPath = "" Then Exit Sub

        Dim setPath() As String
        Dim i, n As Short
        Dim curNode As TreeNode = Nothing
        Dim curParent As Object
        curParent = Me

        setPath = Split(myPath, Me.PathSeparator)
        Dim curPath As String = ""
        For i = 0 To setPath.GetUpperBound(0)
            If curPath <> "" Then curPath &= PathSeparator
            curPath &= setPath(i)

            n = 0
            Try
                Do
                    If curParent.Nodes.Count <= n Then Exit Do
                    curNode = curParent.Nodes(n)
                    n += 1
                Loop Until curNode.Tag.ToString = curPath
            Catch
            End Try

            If curNode IsNot Nothing Then
                curNode.Expand()
                curParent = curNode
                If checkLast = True And setPath.GetUpperBound(0) = i Then curNode.Checked = Not opposite
            Else
                Exit For
            End If
        Next i

        If curNode IsNot Nothing Then Me.SelectedNode = curNode
    End Sub

    Private Sub expandingAll(ByVal myObj As Object)
        Dim tn As TreeNode
        myObj.ExpandAll()

        For Each tn In myObj.nodes
            totalNodes += 1
            expandingAll(tn)
        Next tn
    End Sub

    Protected Sub collapsingAll(ByVal myObj As Object)
        Dim tn As TreeNode
        Try
            myObj.collapse()
        Catch
        End Try
        For Each tn In myObj.nodes
            collapsingAll(tn)
        Next tn
    End Sub

    Public Sub selectNode(ByVal myNodePath As String, Optional ByVal opposite As Boolean = False)
        setSelection(New String() {myNodePath}, Me, opposite)
    End Sub

    Public Sub checkAll(Optional ByVal opposite As Boolean = False)
        For i As Integer = 0 To Me.Nodes.Count - 1
            expansion(Me.Nodes(i).FullPath, True, opposite)
        Next i
    End Sub

    Public Function isCheckedAll() As Boolean
        Return _IsCheckedAll(Me)
    End Function

    Public Function isCheckedNone() As Boolean
        Return _IsCheckedAll(Me, True)
    End Function

    Private Function _IsCheckedAll(ByVal curNode As Object, Optional ByVal opposite As Boolean = False) As Boolean
        For i As Integer = 0 To curNode.Nodes.Count - 1
            If curNode.Nodes(i).Checked = opposite Then Return False
            If curNode.Nodes(i).Nodes.Count <> 0 AndAlso _IsCheckedAll(curNode.Nodes(i), opposite) = False Then Return False
        Next i

        Return True
    End Function

    Public Function getSelected(ByVal opposite As Boolean, ByVal groupedAllCheckedNodes As Boolean) As String()
        mySelection.Clear()
        mySelection = getSelection(Me, opposite, groupedAllCheckedNodes)

        Return mySelection.ToArray(GetType(String))
    End Function

    Public Function getSelected(Of Managed)(ByVal opposite As Boolean) As Generic.List(Of Managed)
        mySelection.Clear()
        mySelection = getSelection(Me, opposite, False, True)

        Dim returning As New Generic.List(Of Managed)
        For Each curSelected As Managed In mySelection
            returning.Add(curSelected)
        Next

        Return returning
    End Function

    Public Sub setSelected(ByVal selected() As String, Optional ByVal opposite As Boolean = False)
        setSelection(selected, Me, opposite)
    End Sub

    Private Sub setSelection(ByVal selected() As String, ByVal myObj As Object, Optional ByVal opposite As Boolean = False)
        If selected Is Nothing Then Exit Sub

        Dim tn As TreeNode
        For Each tn In myObj.nodes
            If Array.IndexOf(selected, tn.Tag.ToString) >= 0 Then
                If tn.IsExpanded = False Then tn.Expand()
                tn.Checked = Not opposite
                Me.SelectedNode = tn
            Else
                tn.Checked = opposite
            End If
            setSelection(selected, tn, opposite)
        Next tn
    End Sub

    Public Sub setExpanded(ByVal expanded As Generic.List(Of String))
        For Each curExpanded As String In expanded
            setExpanded(curExpanded)
        Next
    End Sub

    Public Sub setExpanded(ByVal path As String)
        Dim pathFolders() As String = path.Split(New String() {PathSeparator}, StringSplitOptions.None)
        Dim curPath As String = ""
        Dim curParent As Object = Me
        For i As Integer = 0 To pathFolders.Length - 1
            If curPath <> "" Then curPath &= PathSeparator
            curPath &= pathFolders(i)

            For Each curNode As TreeNode In curParent.Nodes
                If curNode.Tag.ToString = curPath Then
                    curNode.Expand()
                    curParent = curNode
                    Exit For
                End If
            Next
        Next
    End Sub

    Public Function getExpanded() As Generic.List(Of String)
        Dim expanded As New Generic.List(Of String)(_getExpanded(Me).Keys)
        Dim expandedCleaned As New Generic.Dictionary(Of String, Boolean)
        For Each curExpanded As String In expanded
            Dim pathFolders() As String = curExpanded.Split(New String() {PathSeparator}, StringSplitOptions.None)
            Dim curPath As String = ""
            For i As Integer = 0 To pathFolders.Length - 1
                If curPath <> "" Then curPath &= PathSeparator
                curPath &= pathFolders(i)

                If expandedCleaned.ContainsKey(curPath) Then expandedCleaned.Remove(curPath)
            Next
            expandedCleaned.Add(curExpanded, True)
        Next

        Return New Generic.List(Of String)(expandedCleaned.Keys)
    End Function

    Private Function _getExpanded(ByVal obj As Object) As Generic.Dictionary(Of String, Boolean)
        Dim expandedNodes As New Generic.Dictionary(Of String, Boolean)
        For Each curNode As TreeNode In obj.Nodes
            If curNode.IsExpanded Then
                expandedNodes.Add(curNode.Tag.ToString, True)
                Dim subNodes As Generic.Dictionary(Of String, Boolean) = _getExpanded(curNode)
                For Each curKey As String In subNodes.Keys
                    expandedNodes.Add(curKey, True)
                Next
            End If
        Next

        Return expandedNodes
    End Function

    Private Function getSelection(ByVal myObj As Object, Optional ByVal opposite As Boolean = False, Optional ByVal groupedAllCheckedNodes As Boolean = False, Optional ByVal getUnderlyingOjbect As Boolean = False) As ArrayList
        Try
            Dim tn As TreeNode
            For Each tn In myObj.nodes
                If tn.Checked = Not opposite Then
                    If getUnderlyingOjbect Then
                        mySelection.Add(tn.Tag)
                        getSelection(tn, opposite, groupedAllCheckedNodes, getUnderlyingOjbect)
                    Else
                        If groupedAllCheckedNodes AndAlso tn.Nodes.Count <> 0 AndAlso _IsCheckedAll(tn) Then
                            mySelection.Add(tn.Tag.ToString & "\(* Tous *)")
                        Else
                            mySelection.Add(tn.Tag.ToString)
                            getSelection(tn, opposite, groupedAllCheckedNodes, getUnderlyingOjbect)
                        End If
                    End If
                Else
                    getSelection(tn, opposite, groupedAllCheckedNodes, getUnderlyingOjbect)
                End If
            Next tn
        Catch
        End Try

        Return mySelection
    End Function


    Private Sub treeViewPlus_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.AfterCheck
        Dim tn As TreeNode
        For Each tn In e.Node.Nodes
            If tn.Tag.ToString <> e.Node.FullPath Then tn.Checked = e.Node.Checked
        Next tn
    End Sub

    Private Sub treeViewPlus_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles Me.BeforeCheck
        If e.Action = TreeViewAction.ByKeyboard OrElse e.Action = TreeViewAction.ByMouse Then e.Cancel = _readOnly
    End Sub

    Private Sub treeViewPlus_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        If _readOnly = False Then
            If Me.isCheckedAll = False Then
                Me.checkAll()
            Else
                Me.checkAll(True)
            End If
        End If
    End Sub
End Class
