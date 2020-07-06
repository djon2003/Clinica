Public Class DirectoryTreeViewCombo
    Inherits TreeViewComboPlus

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        MyBase.Controls.Remove(MyBase.TheTreeView)
        MyBase.TheTreeView.Dispose()
        MyBase.TheTreeView = New DirectoryTreeView
        MyBase.Controls.Add(MyBase.TheTreeView)
        With CType(Me.TheTreeView, DirectoryTreeView)
            .BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            .CheckBoxes = True
            .dragging = False
            .expandAllNodes = True
            .Location = New System.Drawing.Point(0, 20)
            .Name = "MyCats"
            .properDrag = False
            .showMenu = False
            .ShowPlusMinus = False
            .Size = New System.Drawing.Size(296, 200)
            .Sorted = True
            .TabIndex = 9
        End With
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

#End Region

    Public Overrides Sub refreshTree(Optional ByVal expandedTree As TreeNode = Nothing)
        CType(MyBase.TheTreeView, DirectoryTreeView).refreshTree(expandedTree)
    End Sub
End Class
