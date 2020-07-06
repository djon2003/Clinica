Friend Class DelTables
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        Me.MdiParent = MyMainWin
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Tables As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents delete As System.Windows.Forms.Button
    Friend WithEvents TablesCount As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents delUserFile As System.Windows.Forms.Button
    Friend WithEvents delKPFile As System.Windows.Forms.Button
    Friend WithEvents delClientFile As System.Windows.Forms.Button
    Friend WithEvents delDBFile As System.Windows.Forms.Button
    Friend WithEvents delDataFile As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Tables = New System.Windows.Forms.CheckedListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.delete = New System.Windows.Forms.Button
        Me.TablesCount = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.delClientFile = New System.Windows.Forms.Button
        Me.delKPFile = New System.Windows.Forms.Button
        Me.delUserFile = New System.Windows.Forms.Button
        Me.delDBFile = New System.Windows.Forms.Button
        Me.delDataFile = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'Tables
        '
        Me.Tables.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tables.FormattingEnabled = True
        Me.Tables.Location = New System.Drawing.Point(12, 25)
        Me.Tables.Name = "Tables"
        Me.Tables.Size = New System.Drawing.Size(457, 229)
        Me.Tables.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tables :"
        '
        'delete
        '
        Me.delete.Location = New System.Drawing.Point(475, 256)
        Me.delete.Name = "delete"
        Me.delete.Size = New System.Drawing.Size(95, 49)
        Me.delete.TabIndex = 2
        Me.delete.Text = "Delete tables"
        Me.delete.UseVisualStyleBackColor = True
        '
        'TablesCount
        '
        Me.TablesCount.FormattingEnabled = True
        Me.TablesCount.Location = New System.Drawing.Point(475, 25)
        Me.TablesCount.Name = "TablesCount"
        Me.TablesCount.Size = New System.Drawing.Size(95, 225)
        Me.TablesCount.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(472, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Tables count :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.delDataFile)
        Me.GroupBox1.Controls.Add(Me.delDBFile)
        Me.GroupBox1.Controls.Add(Me.delUserFile)
        Me.GroupBox1.Controls.Add(Me.delKPFile)
        Me.GroupBox1.Controls.Add(Me.delClientFile)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 256)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(454, 49)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Files content"
        '
        'delClientFile
        '
        Me.delClientFile.Location = New System.Drawing.Point(6, 19)
        Me.delClientFile.Name = "delClientFile"
        Me.delClientFile.Size = New System.Drawing.Size(75, 23)
        Me.delClientFile.TabIndex = 0
        Me.delClientFile.Text = "Clients"
        Me.delClientFile.UseVisualStyleBackColor = True
        '
        'delKPFile
        '
        Me.delKPFile.Location = New System.Drawing.Point(87, 19)
        Me.delKPFile.Name = "delKPFile"
        Me.delKPFile.Size = New System.Drawing.Size(75, 23)
        Me.delKPFile.TabIndex = 0
        Me.delKPFile.Text = "KeyPeople"
        Me.delKPFile.UseVisualStyleBackColor = True
        '
        'delUserFile
        '
        Me.delUserFile.Location = New System.Drawing.Point(168, 19)
        Me.delUserFile.Name = "delUserFile"
        Me.delUserFile.Size = New System.Drawing.Size(75, 23)
        Me.delUserFile.TabIndex = 0
        Me.delUserFile.Text = "Users"
        Me.delUserFile.UseVisualStyleBackColor = True
        '
        'delDBFile
        '
        Me.delDBFile.Location = New System.Drawing.Point(249, 19)
        Me.delDBFile.Name = "delDBFile"
        Me.delDBFile.Size = New System.Drawing.Size(75, 23)
        Me.delDBFile.TabIndex = 0
        Me.delDBFile.Text = "DB"
        Me.delDBFile.UseVisualStyleBackColor = True
        '
        'delDataFile
        '
        Me.delDataFile.Location = New System.Drawing.Point(330, 19)
        Me.delDataFile.Name = "delDataFile"
        Me.delDataFile.Size = New System.Drawing.Size(118, 23)
        Me.delDataFile.TabIndex = 0
        Me.delDataFile.Text = "General data"
        Me.delDataFile.UseVisualStyleBackColor = True
        '
        'delTables
        '
        Me.ClientSize = New System.Drawing.Size(582, 307)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TablesCount)
        Me.Controls.Add(Me.delete)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Tables)
        Me.Name = "delTables"
        Me.Text = "Delete tables content"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "delSelectedTables Events"
    Private Sub delSelectedTables_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        LoadTables()
    End Sub
#End Region

    Private Sub loadTables(Optional ByVal onlyTablesCount As Boolean = False)
        Dim curTables As DataSet = DBLinker.GetInstance.ExecuteStoredProcedure("getTablesCount", Nothing, , "Tables")

        If OnlyTablesCount = False Then Tables.Items.Clear()
        TablesCount.Items.Clear()
        With CurTables.Tables("Tables").Rows
            For i As Integer = 0 To .Count - 1
                If OnlyTablesCount = False Then Tables.Items.Add(.Item(i)(0))
                TablesCount.Items.Add(.Item(i)(1))
            Next i
        End With

        TablesCount.SelectedIndex = Tables.SelectedIndex
    End Sub

    Private Sub tables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tables.SelectedIndexChanged
        TablesCount.SelectedIndex = Tables.SelectedIndex
    End Sub

    Private Sub tablesCount_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TablesCount.MouseDoubleClick
        Dim selected As Integer = TablesCount.IndexFromPoint(e.X, e.Y)
        If Selected >= 0 Then Tables.SetItemChecked(Selected, Not Tables.GetItemChecked(Selected))
    End Sub

    Private Sub tablesCount_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TablesCount.SelectedIndexChanged
        Tables.SelectedIndex = TablesCount.SelectedIndex
    End Sub

    Private Sub delDataFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delDataFile.Click
        Deltree(AppPath & Bar(AppPath) & "Data\Email")
        Deltree(AppPath & Bar(AppPath) & "Data\Lists")
        Deltree(AppPath & Bar(AppPath) & "Data\LockSecteur")
        Deltree(AppPath & Bar(AppPath) & "Data\Modeles")
        Deltree(AppPath & Bar(AppPath) & "Data\Queues")
        Deltree(AppPath & Bar(AppPath) & "Data\UniqueNoQueue")
        IO.File.Delete(AppPath & Bar(AppPath) & "Data\pref.sav")
        IO.File.Delete(AppPath & Bar(AppPath) & "Data\unique.no")

        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\Email\Mail\Interne")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\Email\Mail\Externe")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\Email\AddressBook")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\Lists")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\LockSecteur")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\Modeles")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\Queues")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Data\UniqueNoQueue")
        XCopy(AppPath & Bar(AppPath) & "Data\DefLists", AppPath & Bar(AppPath) & "Data\Lists")
    End Sub

    Private Sub delDBFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delDBFile.Click
        Deltree(AppPath & Bar(AppPath) & "DB")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "DB\Types")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "DB\DB")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "DB\Content")

        XCopy(AppPath & Bar(AppPath) & "Data\DefTypes", AppPath & Bar(AppPath) & "DB\Types")
    End Sub

    Private Sub delUserFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delUserFile.Click
        Deltree(AppPath & Bar(AppPath) & "Users")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Users\Connected")
    End Sub

    Private Sub delKPFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delKPFile.Click
        Deltree(AppPath & Bar(AppPath) & "KP")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "KP")
    End Sub

    Private Sub delClientFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delClientFile.Click
        Deltree(AppPath & Bar(AppPath) & "Clients")
        IO.Directory.CreateDirectory(AppPath & Bar(AppPath) & "Clients")
    End Sub

    Private Sub delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click
        Dim deletingTables As String = "exec sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';" & vbCrLf & "exec sp_MSforeachtable 'ALTER TABLE ? DISABLE TRIGGER ALL';" & vbCrLf
        For i As Integer = 0 To Tables.CheckedItems.Count - 1
            DeletingTables &= "DELETE FROM " & Tables.CheckedItems(i).ToString() & ";IF OBJECTPROPERTY(OBJECT_ID('" & Tables.CheckedItems(i).ToString() & "'), 'TableHasIdentity') = 1 BEGIN DBCC CHECKIDENT ('" & Tables.CheckedItems(i).ToString() & "',RESEED,0) END;" & vbCrLf
        Next i

        DeletingTables &= "exec sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'" & vbCrLf & "exec sp_MSforeachtable 'ALTER TABLE ? ENABLE TRIGGER ALL'"

        DBLinker.ExecuteSQLScript(DeletingTables)
        LoadTables(True)
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
