Public Class TreeViewPlus
    Inherits System.Windows.Forms.TreeView

    Private Class TreeComparer
        Implements IComparer

        Public Function compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            If y Is Nothing OrElse x Is Nothing Then Return False
            Return x.ToString.CompareTo(y.ToString)
        End Function
    End Class

    Public Interface TreeViewItem
        Property iconIndex() As Integer
        Property iconSelectedIndex() As Integer
        Property isBold() As Boolean
    End Interface

    Private _ExpandAllNodes As Boolean
    Private _Tree() As Object
    Private totalNodes As Integer = 0
    Private mySelection As New ArrayList

#Region "Propriétés"


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


    Public Overridable Sub refreshTree(Optional ByVal expandedTree As TreeNode = Nothing)
        Dim myETPath As String = ""
        If Not expandedTree Is Nothing Then myETPath = expandedTree.FullPath
        With Me
            .BeginUpdate()

            .SelectedNode = Nothing
            .Nodes.Clear()

            Dim strTree As Object
            Array.Sort(tree, New TreeComparer())
            For i As Integer = 0 To tree.GetUpperBound(0)
                strTree = tree(i)
                If strTree IsNot Nothing Then
                    If strTree.ToString.IndexOf(Me.PathSeparator) < 0 And strTree.ToString <> "" Then
                        Dim iconIndex As Integer = 0
                        Dim selectedIndex As Integer = 1
                        Dim isBold As Boolean = False
                        If TypeOf strTree Is TreeViewItem Then
                            With CType(strTree, TreeViewItem)
                                If .iconIndex >= 0 AndAlso .iconIndex < Me.ImageList.Images.Count Then iconIndex = .iconIndex
                                If .iconSelectedIndex >= 0 AndAlso .iconSelectedIndex < Me.ImageList.Images.Count Then selectedIndex = .iconSelectedIndex
                                isBold = .isBold
                            End With
                        End If
                        Dim tnFolder As New TreeNode(strTree.ToString, iconIndex, selectedIndex)

                        tnFolder.Tag = strTree
                        tnFolder.Name = strTree.ToString.Substring(strTree.ToString.LastIndexOf(Me.PathSeparator) + 1)
                        If isBold Then tnFolder.NodeFont = New Font(tnFolder.NodeFont, FontStyle.Bold)
                        .Nodes.Add(tnFolder)
                        addSubNodes(tnFolder, i)
                    End If
                End If
            Next

            .EndUpdate()
        End With
        expansion(myETPath)
    End Sub

    Private Sub addSubNodes(ByVal tn As TreeNode, ByRef index As Integer)
        Dim passedIndex As Integer = index
        tn.Nodes.Clear()
        Dim currentDirPos As Short = Split(tn.FullPath, PathSeparator).Length + 1

        Dim sstrTree() As String
        Dim strTree As Object
        Dim curTree As New ArrayList

        For i As Integer = index To tree.GetUpperBound(0)
            strTree = tree(i)
            If strTree IsNot Nothing Then
                sstrTree = Split(strTree.ToString, PathSeparator)
                If sstrTree.Length = currentDirPos And strTree.ToString.StartsWith(tn.FullPath & PathSeparator) Then
                    Dim tnDir As New TreeNode(sstrTree(sstrTree.GetUpperBound(0)), 0, 1)
                    tnDir.Tag = strTree
                    tnDir.Name = strTree.ToString.Substring(strTree.ToString.LastIndexOf(Me.PathSeparator) + 1)
                    tn.Nodes.Add(tnDir)
                    addSubNodes(tnDir, i)
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
        For i = 0 To setPath.GetUpperBound(0)
            n = 0
            Try
                Do
                    If curParent.Nodes.Count <= n Then Exit Do
                    curNode = curParent.Nodes(n)
                    n += 1
                Loop Until curNode.Text = setPath(i)
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

    Public Sub selectANode(ByVal myNodePath As String, Optional ByVal opposite As Boolean = False)
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

    Public Function getSelected(Optional ByVal opposite As Boolean = False, Optional ByVal groupedAllCheckedNodes As Boolean = False) As String()
        mySelection.Clear()
        mySelection = getSelection(Me, opposite, groupedAllCheckedNodes)

        Return mySelection.ToArray(GetType(String))
    End Function

    Public Sub setSelected(ByVal selected() As String, Optional ByVal opposite As Boolean = False)
        setSelection(selected, Me, opposite)
    End Sub

    Private Sub setSelection(ByVal selected() As String, ByVal myObj As Object, Optional ByVal opposite As Boolean = False)
        If selected Is Nothing Then Exit Sub

        Dim tn As TreeNode
        For Each tn In myObj.nodes
            If Array.IndexOf(selected, tn.FullPath) >= 0 Then
                If tn.IsExpanded = False Then tn.Expand()
                tn.Checked = Not opposite
                Me.SelectedNode = tn
            Else
                tn.Checked = opposite
            End If
            setSelection(selected, tn, opposite)
        Next tn
    End Sub

    Private Function getSelection(ByVal myObj As Object, Optional ByVal opposite As Boolean = False, Optional ByVal groupedAllCheckedNodes As Boolean = False) As ArrayList
        Try
            Dim tn As TreeNode
            For Each tn In myObj.nodes
                If tn.Checked = Not opposite Then
                    If groupedAllCheckedNodes AndAlso tn.Nodes.Count <> 0 AndAlso _IsCheckedAll(tn) Then
                        mySelection.Add(tn.FullPath & "\(* Tous *)")
                    Else
                        mySelection.Add(tn.FullPath)
                        getSelection(tn, opposite, groupedAllCheckedNodes)
                    End If
                Else
                    getSelection(tn, opposite, groupedAllCheckedNodes)
                End If
            Next tn
        Catch
        End Try

        Return mySelection
    End Function

    Public Sub New()
        Me.PathSeparator = "\"
        Me.ImageList = New ImageList()
        'TODO SHALL be in a derived class
        'Try
        '    With Me.ImageList.Images
        '        .Add(DrawingManager.GetInstance.GetIcon("FolderClosed.ico"))
        '        .Add(DrawingManager.GetInstance.GetIcon("FolderOpened.ico"))
        '    End With
        'Catch
        'End Try
    End Sub

    Private Sub treeViewPlus_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.AfterCheck
        Dim tn As TreeNode
        For Each tn In e.Node.Nodes
            If tn.FullPath <> e.Node.FullPath Then tn.Checked = e.Node.Checked
        Next tn
    End Sub

    Private Sub treeViewPlus_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        If Me.isCheckedAll = False Then
            Me.checkAll()
        Else
            Me.checkAll(True)
        End If

    End Sub
End Class
