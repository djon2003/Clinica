Public Class TreeViewComboPlus
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.upText = "Cliquer sur la flèche pour afficher la liste"
        Me.downText = "Cliquer sur la flèche pour cacher la liste"

        'Add any initialization after the InitializeComponent() call
    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents DropDownBtn As System.Windows.Forms.Button
    Friend WithEvents TheTreeView As TreeViewPlus
    Friend WithEvents TheText As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SizingTool As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TreeViewComboPlus))
        Me.DropDownBtn = New System.Windows.Forms.Button()
        Me.TheText = New System.Windows.Forms.TextBox()
        Me.TheTreeView = New TreeViewPlus
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SizingTool = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DropDownBtn
        '
        Me.DropDownBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DropDownBtn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DropDownBtn.Location = New System.Drawing.Point(277, 2)
        Me.DropDownBtn.Name = "DropDownBtn"
        Me.ToolTip1.SetToolTip(Me.DropDownBtn, "Afficher la liste")
        Me.DropDownBtn.Size = New System.Drawing.Size(17, 16)
        Me.DropDownBtn.TabIndex = 8
        '
        'TheText
        '
        Me.TheText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TheText.Location = New System.Drawing.Point(0, 0)
        Me.TheText.Name = "TheText"
        Me.TheText.ReadOnly = True
        Me.TheText.Size = New System.Drawing.Size(296, 20)
        Me.TheText.TabIndex = 5
        Me.TheText.Text = "Cliquer sur la flèche pour afficher la liste"
        '
        'TheTreeView
        '
        Me.TheTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TheTreeView.CheckBoxes = True
        Me.TheTreeView.ImageIndex = -1
        Me.TheTreeView.Location = New System.Drawing.Point(0, 20)
        Me.TheTreeView.Name = "TheTreeView"
        Me.TheTreeView.Size = New System.Drawing.Size(296, 192)
        Me.TheTreeView.Sorted = True
        Me.TheTreeView.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.TheTreeView, "Double-clique pour tout sélectionner ou tout désélectionner")
        '
        'SizingTool
        '
        Me.SizingTool.BackColor = System.Drawing.Color.Black
        Me.SizingTool.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.SizingTool.Location = New System.Drawing.Point(0, 208)
        Me.SizingTool.Name = "SizingTool"
        Me.SizingTool.Size = New System.Drawing.Size(167, 2)
        Me.SizingTool.TabIndex = 10
        '
        'CheckedTreeViewCombo
        '
        Me.Controls.Add(Me.SizingTool)
        Me.Controls.Add(Me.TheTreeView)
        Me.Controls.Add(Me.DropDownBtn)
        Me.Controls.Add(Me.TheText)
        Me.Name = "CheckedTreeViewCombo"
        Me.Size = New System.Drawing.Size(328, 220)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private forceResize As Boolean = False
    Private _BottomShowed As Boolean = False
    Private _DropDownHeight As Integer = 220
    Private _DownText As String = "Cliquer sur la flèche pour cacher la liste"
    Private _UpText As String = "Cliquer sur la flèche pour afficher la liste"
    Private _upArrowImage As Bitmap
    Private _downArrowImage As Bitmap

    Private buttonDown As MouseButtons

#Region "Propriétés"
    Public Property expandAllNodes() As Boolean
        Get
            Return TheTreeView.expandAllNodes
        End Get
        Set(ByVal Value As Boolean)
            TheTreeView.expandAllNodes = Value
        End Set
    End Property

    Public Property imageList() As ImageList
        Get
            Return TheTreeView.ImageList
        End Get
        Set(ByVal Value As ImageList)
            TheTreeView.ImageList = Value
        End Set
    End Property

    Public Property dropDownHeight() As Integer
        Get
            Return _DropDownHeight
        End Get
        Set(ByVal Value As Integer)
            If Value > 0 Then
                _DropDownHeight = Value
            Else
                _DropDownHeight = 0
            End If

            If Me.DesignMode = True Then Exit Property

            showBottom(Me.droppedDown)
        End Set
    End Property

    Public Property tree() As Object()
        Get
            Return Me.TheTreeView.tree
        End Get
        Set(ByVal Value As Object())
            Me.TheTreeView.tree = Value
        End Set
    End Property

    Public Property upText() As String
        Get
            Return _UpText
        End Get
        Set(ByVal Value As String)
            _UpText = Value
        End Set
    End Property

    Public Property downText() As String
        Get
            Return _DownText
        End Get
        Set(ByVal Value As String)
            _DownText = Value
        End Set
    End Property

    Public Property upArrowImage() As Bitmap
        Get
            Return _upArrowImage
        End Get
        Set(ByVal value As Bitmap)
            _upArrowImage = value
            If droppedDown Then Me.DropDownBtn.Image = _upArrowImage
        End Set
    End Property

    Public Property downArrowImage() As Bitmap
        Get
            Return _downArrowImage
        End Get
        Set(ByVal value As Bitmap)
            _downArrowImage = value
            If droppedDown = False Then Me.DropDownBtn.Image = _downArrowImage
        End Set
    End Property


    Public Property pathSeparator() As String
        Get
            Return TheTreeView.PathSeparator
        End Get
        Set(ByVal Value As String)
            TheTreeView.PathSeparator = Value
        End Set
    End Property

    Public Property droppedDown() As Boolean
        Get
            Return _BottomShowed
        End Get
        Set(ByVal Value As Boolean)
            _BottomShowed = Value
            If Me.DesignMode = False Then showBottom(Value)
        End Set
    End Property
#End Region

    Public Sub expansion(ByVal myPath As String, Optional ByVal checkLast As Boolean = False, Optional ByVal opposite As Boolean = False)
        If myPath = "" Then Exit Sub

        Dim setPath() As String
        Dim i, n As Short
        Dim curNode As TreeNode = Nothing
        Dim curParent As Object
        curParent = TheTreeView

        setPath = myPath.Split(New Char() {"\"})
        For i = 0 To setPath.GetUpperBound(0)
            n = 0
            Dim exitFor As Boolean = False
            Try
                Do
                    If n < curParent.Nodes.Count Then
                        curNode = curParent.Nodes(n)
                    Else
                        exitFor = True
                    End If
                    n += 1
                Loop Until curNode.Text = setPath(i)
            Catch
            End Try

            If curNode IsNot Nothing Then
                TheTreeView.SelectedNode = curNode
                curNode.Expand()
                If checkLast = True And setPath.GetUpperBound(0) = i Then curNode.Checked = Not opposite
                curParent = curNode
                If Me.droppedDown = False Then Me.showBottom(False)
                If exitFor Then Exit For
            Else
                Exit For
            End If
        Next i
    End Sub

    Public Sub selectANode(ByVal myNodePath As String, Optional ByVal opposite As Boolean = False)
        expansion(myNodePath, True, opposite)
    End Sub

    Public Function searchANode(ByVal myNode As String) As String
        If tree Is Nothing OrElse tree.Length = 0 Then Return ""

        For i As Integer = 0 To tree.GetUpperBound(0)
            If tree(i).IndexOf(myNode) >= 0 Then Return tree(i)
        Next i

        Return ""
    End Function

    Public Function isCheckedAll() As Boolean
        Return TheTreeView.isCheckedAll
    End Function

    Public Function isCheckedNone() As Boolean
        Return TheTreeView.isCheckedNone
    End Function

    Public Function getSelected(Optional ByVal opposite As Boolean = False, Optional ByVal groupedAllCheckedNodes As Boolean = False) As String()
        Return TheTreeView.getSelected(opposite, groupedAllCheckedNodes)
    End Function


    Public Sub setSelected(ByVal selected() As String, Optional ByVal opposite As Boolean = False)
        TheTreeView.setSelected(selected, opposite)
        If Me.droppedDown = False Then Me.showBottom(False)
    End Sub

    Public Sub checkAll(ByVal trueFalse As Boolean)
        TheTreeView.checkAll(Not trueFalse)

        If Me.droppedDown = False Then Me.showBottom(False)
    End Sub

    Private Sub treeViewCombo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If forceResize = False Or Me.Height < 20 Then Me.Height = 20

        TheText.Location = New Point(0, 0)
        TheText.Width = Me.Width
        DropDownBtn.Left = Me.Width - 2 - DropDownBtn.Width
        TheTreeView.Width = Me.Width
        SizingTool.Width = Me.Width
        SizingTool.Left = 0
        SizingTool.Top = Me.Height - 2
    End Sub

    Private Sub showBottom(ByVal showIt As Boolean)
        forceResize = showIt
        SizingTool.Visible = showIt
        If showIt = True Then
            Me.Height = dropDownHeight + 20
            TheTreeView.Height = dropDownHeight
            DropDownBtn.Image = upArrowImage
            TheText.Text = downText
            Me.ToolTip1.SetToolTip(Me.DropDownBtn, "Cacher la liste")
            Me.ToolTip1.SetToolTip(Me.TheText, "")
        Else
            Me.Height = 20
            DropDownBtn.Image = downArrowImage
            Dim selected() As String = getSelected(, True)
            Dim curselected As String = String.Join("; ", selected)
            If curselected = "" Then
                TheText.Text = upText
                Me.ToolTip1.SetToolTip(Me.TheText, "")
            Else
                TheText.Text = curselected
                Me.ToolTip1.SetToolTip(Me.TheText, String.Join(vbCrLf, selected))
            End If
            Me.ToolTip1.SetToolTip(Me.DropDownBtn, "Afficher la liste")
        End If
    End Sub

    Private Sub dropDownBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownBtn.Click
        droppedDown = Not droppedDown
    End Sub

    Public Overridable Sub refreshTree(Optional ByVal expandedTree As TreeNode = Nothing)
        Dim myETPath As String = ""

        If Not expandedTree Is Nothing Then myETPath = expandedTree.FullPath
        With TheTreeView
            .BeginUpdate()

            .SelectedNode = Nothing
            .Nodes.Clear()

            Dim strTree As String
            For Each strTree In tree
                If strTree.IndexOf("\") < 0 Then
                    Dim tnFolder As New TreeNode(strTree, 0, 1)
                    .Nodes.Add(tnFolder)
                    addSubNodes(tnFolder)
                End If
            Next

            .EndUpdate()
        End With
        expansion(myETPath)
    End Sub

    Private Sub addSubNodes(ByVal tn As TreeNode)
        tn.Nodes.Clear()
        Dim currentDirPos As Short = Split(tn.FullPath, pathSeparator).Length + 1

        Dim strTree, sstrTree() As String
        For Each strTree In tree
            sstrTree = Split(strTree, pathSeparator)
            If sstrTree.Length = currentDirPos And strTree.StartsWith(tn.FullPath & "\") Then
                Dim tnDir As New TreeNode(sstrTree(sstrTree.GetUpperBound(0)), 0, 1)
                tn.Nodes.Add(tnDir)
                addSubNodes(tnDir)
                If expandAllNodes = True Then tn.ExpandAll()
            End If
        Next
    End Sub

    Private Sub theTreeView_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TheTreeView.AfterCheck
        'Dim tn As TreeNode
        'For Each tn In e.Node.Nodes
        '    If tn.FullPath <> e.Node.FullPath Then tn.Checked = e.Node.Checked
        'Next tn
    End Sub

    'Private Sub theTreeView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TheTreeView.DoubleClick
    '    SetSelection(TheTreeView, CurCheckedValue)
    '    CurCheckedValue = Not CurCheckedValue
    'End Sub

    Private Sub sizingTool_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SizingTool.MouseDown
        buttonDown = e.Button
    End Sub

    Private Sub sizingTool_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SizingTool.MouseMove
        If buttonDown = MouseButtons.Left Then
            If (Me.dropDownHeight + e.Y + 20 + Me.Top) < Me.Parent.ClientSize.Height Then
                Me.dropDownHeight += e.Y
            Else
                Me.dropDownHeight = Me.Parent.ClientSize.Height - 20 - Me.Top
            End If
        End If
    End Sub

    Private Sub sizingTool_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SizingTool.MouseUp
        buttonDown = MouseButtons.None
    End Sub

    Private Sub theText_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TheText.MouseEnter
        If _BottomShowed = False Then
            Me.ToolTip1.ShowAlways = True
            Me.ToolTip1.ToolTipTitle = "Ligne(s) sélectionnée(s) :"
        End If
    End Sub

    Private Sub theText_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TheText.MouseLeave
        Me.ToolTip1.ShowAlways = False
        Me.ToolTip1.ToolTipTitle = ""
    End Sub

    Private Sub theText_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TheText.MouseMove
        If _BottomShowed = False Then Me.ToolTip1.SetToolTip(TheText, Me.ToolTip1.GetToolTip(TheText))
    End Sub
End Class
