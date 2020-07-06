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
        dropDownBtnImgs = New ImageList()
        If Me.DesignMode = False Then
            Try
                With dropDownBtnImgs.Images
                    .Add(DrawingManager.getInstance.getImage("DownArrow.jpg"))
                    .Add(DrawingManager.getInstance.getImage("UpArrow.jpg"))
                End With
                dropDownBtnImgs.ImageSize = New Size(7, 4)
                DropDownBtn.Image = dropDownBtnImgs.Images(0)
            Catch 'Protection for desgin mode.. seems the property is not doing desired behavior
            End Try
        End If
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

    Private dropDownBtnImgs As ImageList
    Private forceResize As Boolean = False
    Private _BottomShowed As Boolean = False
    Private _DropDownHeight As Integer = 220
    Private _DownText As String = "Cliquer sur la flèche pour cacher la liste"
    Private _UpText As String = "Cliquer sur la flèche pour afficher la liste"
    Private _tooltipTitle As String = "Ligne(s) sélectionnée(s) :"

    Private buttonDown As MouseButtons

    Public Event checkBoxChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    Public Shadows Event textChanged(ByVal sender As Object, ByVal e As System.EventArgs)

#Region "Proprieties"
    Public Overrides Property text() As String
        Get
            Return TheText.Text
        End Get
        Set(ByVal value As String)
            TheText.Text = value
        End Set
    End Property

    Public Property tooltipTitle() As String
        Get
            Return _tooltipTitle
        End Get
        Set(ByVal value As String)
            _tooltipTitle = value
        End Set
    End Property

    Public Property [readOnly]() As Boolean
        Get
            Return TheTreeView.readOnly
        End Get
        Set(ByVal value As Boolean)
            TheTreeView.readOnly = value
        End Set
    End Property

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
            TheText.Text = Value
        End Set
    End Property

    Public Property sorted() As Boolean
        Get
            Return TheTreeView.Sorted
        End Get
        Set(ByVal value As Boolean)
            TheTreeView.Sorted = value
        End Set
    End Property

    Public Property showLines() As Boolean
        Get
            Return TheTreeView.ShowLines
        End Get
        Set(ByVal value As Boolean)
            TheTreeView.ShowLines = value
        End Set
    End Property

    Public Property showRootLines() As Boolean
        Get
            Return TheTreeView.ShowRootLines
        End Get
        Set(ByVal value As Boolean)
            TheTreeView.ShowRootLines = value
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
        TheTreeView.expansion(myPath, checkLast, opposite)
    End Sub

    Public Sub selectANode(ByVal myNodePath As String, Optional ByVal opposite As Boolean = False)
        TheTreeView.selectNode(myNodePath, opposite)
    End Sub

    Public Function searchANode(ByVal myNode As String) As String
        Return TheTreeView.searchNode(myNode)
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

    Public Function getSelected(Of Managed)(ByVal opposite As Boolean) As Generic.List(Of Managed)
        Return TheTreeView.getSelected(Of Managed)(opposite)
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
            DropDownBtn.Image = dropDownBtnImgs.Images(1)
            TheText.Text = downText
            Me.ToolTip1.SetToolTip(Me.DropDownBtn, "Cacher la liste")
            Me.ToolTip1.SetToolTip(Me.TheText, "")
        Else
            Me.Height = 20
            DropDownBtn.Image = dropDownBtnImgs.Images(0)
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

    Private treeObjects As New Generic.Dictionary(Of String, Object)

    Public Overridable Sub refreshTree(Optional ByVal expandedTree As TreeNode = Nothing)
        TheTreeView.refreshTree(expandedTree)
    End Sub

    Protected Sub onCheckBoxChanged(ByVal e As System.Windows.Forms.TreeViewEventArgs)
        RaiseEvent checkBoxChanged(Me, e)
    End Sub

    Private Sub theTreeView_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TheTreeView.AfterCheck
        onCheckBoxChanged(e)

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
            Me.ToolTip1.ToolTipTitle = tooltipTitle
        End If
    End Sub

    Private Sub theText_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TheText.MouseLeave
        Me.ToolTip1.ShowAlways = False
        Me.ToolTip1.ToolTipTitle = ""
    End Sub

    Private Sub theText_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TheText.MouseMove
        If _BottomShowed = False Then Me.ToolTip1.SetToolTip(TheText, Me.ToolTip1.GetToolTip(TheText))
    End Sub

    Private Sub theTreeView_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TheTreeView.BeforeCheck

    End Sub

    Private Sub theText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TheText.TextChanged
        Me.OnTextChanged(e)
        RaiseEvent textChanged(Me, EventArgs.Empty)
    End Sub
End Class
