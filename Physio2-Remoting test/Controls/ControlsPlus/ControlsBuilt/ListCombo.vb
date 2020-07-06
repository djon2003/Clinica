Public Class ListCombo
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
                DropDownBtn.Image = Nothing
                'DropDownBtnImgs.Dispose()
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
    Public WithEvents myList As CI.Controls.List
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ListCombo))
        Me.DropDownBtn = New System.Windows.Forms.Button()
        Me.myList = New CI.Controls.List
        Me.SuspendLayout()
        '
        'DropDownBtn
        '
        Me.DropDownBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DropDownBtn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DropDownBtn.Location = New System.Drawing.Point(248, 2)
        Me.DropDownBtn.Name = "DropDownBtn"
        Me.DropDownBtn.Size = New System.Drawing.Size(17, 16)
        Me.DropDownBtn.TabIndex = 10
        '
        'MyList
        '
        Me.myList.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.myList.autoAdjust = True
        Me.myList.autoSizeVertically = False
        Me.myList.autoSizeHorizontally = False
        Me.myList.BackColor = System.Drawing.Color.White
        Me.myList.baseAlignment = CI.Controls.IListItem.AlignmentType.LeftA
        Me.myList.baseBackColor = System.Drawing.Color.White
        Me.myList.baseFont = New System.Drawing.Font("Arial", 8.0!)
        Me.myList.baseForeColor = System.Drawing.SystemColors.ControlText
        Me.myList.bgColor = System.Drawing.Color.White
        Me.myList.borderColor = System.Drawing.Color.Empty
        Me.myList.borderSelColor = System.Drawing.Color.Empty
        Me.myList.borderStyle = CI.Controls.List.BSType.FixedSingle
        Me.myList.clickEnabled = True
        Me.myList.do3D = False
        Me.myList.draw = False
        Me.myList.hScrollColor = System.Drawing.SystemColors.Control
        Me.myList.hScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.myList.hScrolling = True
        Me.myList.hsValue = CType(0, Short)
        Me.myList.itemBorder = CType(0, Short)
        Me.myList.Location = New System.Drawing.Point(0, 20)
        Me.myList.itemMargin = CType(0, Short)
        Me.myList.mouseMove3D = False
        Me.myList.mouseSpeed = 0
        Me.myList.Name = "MyList"
        Me.myList.objMaxHeight = 0.0!
        Me.myList.objMaxWidth = 0.0!
        Me.myList.selected = CType(-1, Short)
        Me.myList.Size = New System.Drawing.Size(267, 212)
        Me.myList.sorted = False
        Me.myList.TabIndex = 11
        Me.myList.vScrollColor = System.Drawing.SystemColors.Control
        Me.myList.vScrollForeColor = System.Drawing.SystemColors.ControlText
        Me.myList.vScrolling = True
        Me.myList.vsValue = CType(0, Short)
        '
        'ListCombo
        '
        Me.Controls.Add(Me.myList)
        Me.Controls.Add(Me.DropDownBtn)
        Me.Name = "ListCombo"
        Me.Size = New System.Drawing.Size(267, 232)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Private DropDownBtnImgs As ImageList
    Private forceResize As Boolean = False
    Private _BottomShowed As Boolean = False
    Private myText As String = "Cliquer pour dérouler"
    Private _upArrowImage As Bitmap
    Private _downArrowImage As Bitmap

    Public Enum AType
        LeftA = 1
        RightA = 4
        CenterA = 2
    End Enum

    Public Event listConfigNeeded(ByVal list As CI.Controls.List)

#Region "Propriétés"
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

    Public Property changeText() As String
        Get
            Return myText
        End Get
        Set(ByVal Value As String)
            myText = Value
        End Set
    End Property

    Public Property droppedDown() As Boolean
        Get
            Return _BottomShowed
        End Get
        Set(ByVal Value As Boolean)
            _BottomShowed = Value
            showBottom(Value)
        End Set
    End Property

    Public Property dropDownHeight() As Integer
        Get
            Return myList.Height
        End Get
        Set(ByVal Value As Integer)
            myList.Height = Value
            If droppedDown Then showBottom(True)
        End Set
    End Property

    Public Property clickEnabled() As Boolean
        Get
            Return myList.clickEnabled
        End Get
        Set(ByVal Value As Boolean)
            myList.clickEnabled = Value
        End Set
    End Property

    Public Property draw() As Boolean
        Get
            Return myList.draw
        End Get
        Set(ByVal Value As Boolean)
            myList.draw = Value
        End Set
    End Property

    Public Property hsValue() As Short
        Get
            Return myList.hsValue
        End Get
        Set(ByVal Value As Short)
            myList.hsValue = Value
        End Set
    End Property

    Public Property vsValue() As Short
        Get
            Return myList.vsValue
        End Get
        Set(ByVal Value As Short)
            myList.vsValue = Value
        End Set
    End Property

    Public Property selected() As Short
        Get
            Return myList.selected
        End Get
        Set(ByVal Value As Short)
            myList.selected = Value
        End Set
    End Property

    Public Property sorted() As Boolean
        Get
            Return myList.sorted
        End Get
        Set(ByVal Value As Boolean)
            myList.sorted = Value
        End Set
    End Property
#End Region

#Region "External functions"
    Public Sub reverse()
        myList.reverse()
    End Sub

    Public Function listCount() As Short
        Return myList.listCount
    End Function

    Public Function mapItem(ByVal x As Double, ByVal y As Double) As Short
        Return myList.mapItem(x, y)
    End Function

    Public Function maximum() As Short
        Return myList.maximum
    End Function

    Public Sub remove(ByVal noItem As Short)
        myList.remove(noItem)
    End Sub

    Public Sub showItem(ByVal noItem As Short, Optional ByVal position As CI.Controls.List.PosType = 0)
        myList.showItem(noItem, position)
    End Sub

    Public Sub tieItem(ByVal nbItemToGroup As Short, ByVal startingItem As Short)
        myList.tieItem(nbItemToGroup, startingItem)
    End Sub

    Public Function itemVisible(ByVal noItem As Short) As CI.Controls.List.ItemVisibility
        Return myList.itemVisible(noItem)
    End Function

    Public Property ItemBackColor(ByVal noItem As Short) As Color
        Get
            Return myList.ItemBackColor(noItem)
        End Get
        Set(ByVal Value As Color)
            myList.ItemBackColor(noItem) = Value
        End Set
    End Property

    Public Property ItemForeColor(ByVal noItem As Short) As Color
        Get
            Return myList.ItemForeColor(noItem)
        End Get
        Set(ByVal Value As Color)
            myList.ItemForeColor(noItem) = Value
        End Set
    End Property

    Public Property ItemCaption(ByVal noItem As Short) As String
        Get
            Return myList.ItemText(noItem)
        End Get
        Set(ByVal Value As String)
            myList.ItemText(noItem) = Value
        End Set
    End Property

    Public Property ItemAlignment(ByVal noItem As Short) As AType
        Get
            Return myList.ItemAlignment(noItem)
        End Get
        Set(ByVal Value As AType)
            myList.ItemAlignment(noItem) = Value
        End Set
    End Property

    Public Property ItemFont(ByVal noItem As Short) As Font
        Get
            Return myList.ItemFont(noItem)
        End Get
        Set(ByVal Value As Font)
            myList.ItemFont(noItem) = Value
        End Set
    End Property

    Public Property ItemToolTipText(ByVal noItem As Short) As String
        Get
            Return myList.ItemToolTipText(noItem)
        End Get
        Set(ByVal Value As String)
            myList.ItemValueB(noItem) = Value
        End Set
    End Property

    Public Property ItemValueA(ByVal noItem As Short) As Object
        Get
            Return myList.ItemValueA(noItem)
        End Get
        Set(ByVal Value As Object)
            myList.ItemValueA(noItem) = Value
        End Set
    End Property

    Public Property ItemValueB(ByVal noItem As Short) As Object
        Get
            Return myList.ItemValueB(noItem)
        End Get
        Set(ByVal Value As Object)
            myList.ItemValueB(noItem) = Value
        End Set
    End Property

    Public Sub config(Optional ByVal mouseSpeed As Integer = -1)
        RaiseEvent listConfigNeeded(myList)
        If mouseSpeed > -1 Then myList.mouseSpeed = mouseSpeed
    End Sub

    Public Function add(Optional ByVal nomItem As String = "") As Short
        Return myList.add(nomItem)
    End Function

    Public Sub all(ByVal propertyToChange As CI.Controls.List.PType, ByVal value As Object)
        myList.all(propertyToChange, value)
    End Sub

    Public Sub cls()
        myList.cls()
    End Sub

    Public Function findFirstItem() As Short
        Return myList.findFirstItem()
    End Function

    Public Function findLastItem() As Short
        Return myList.findLastItem()
    End Function

    Public Function findPreviousItem(ByVal noItem As Short) As Short
        Return myList.findPreviousItem(noItem)
    End Function

    Public Function findNextItem(ByVal noItem As Short) As Short
        Return myList.findNextItem(noItem)
    End Function

    Public Function findString(ByVal strToSearch As String, Optional ByVal respectCase As Boolean = True, Optional ByVal startsWith As Boolean = False) As Short
        Return myList.findString(strToSearch, respectCase, , startsWith)
    End Function

    Public Function findStringExact(ByVal strToSearch As String, Optional ByVal respectCase As Boolean = True) As Short
        Return myList.findStringExact(strToSearch, respectCase)
    End Function
#End Region

    Private Sub showBottom(ByVal showIt As Boolean)
        forceResize = showIt
        If showIt = True Then
            Me.Height = myList.Height + 21
            DropDownBtn.Image = upArrowImage
        Else
            Me.Height = 20
            DropDownBtn.Image = downArrowImage
        End If
    End Sub

    Private Sub listCombo_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawString(myText, Me.Font, Brushes.Black, (Me.Width - Chaines.measureString(myText, Me.Font).Width - DropDownBtn.Width - 2) / 2, (20 - Me.Font.Size) / 2 - 2)
        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(0, 0, Me.Width - 1, 19))
    End Sub

    Private Sub listCombo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If forceResize = False Then Me.Height = 20
        Me.Width = 267
    End Sub

    Private Sub dropDownBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownBtn.Click
        droppedDown = Not droppedDown
    End Sub
End Class
