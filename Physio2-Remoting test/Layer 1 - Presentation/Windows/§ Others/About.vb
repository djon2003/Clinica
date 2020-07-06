Friend Class About
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.MdiParent = myMainWin
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Lien3 As System.Windows.Forms.LinkLabel
    Friend WithEvents Lien1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Email2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Email1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Lien4 As System.Windows.Forms.LinkLabel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents NewsList As DataGridPlus
    Friend WithEvents NewsDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents News As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewsType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Viewed As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents version As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Lien2 As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Lien3 = New System.Windows.Forms.LinkLabel
        Me.Lien1 = New System.Windows.Forms.LinkLabel
        Me.Email2 = New System.Windows.Forms.LinkLabel
        Me.Email1 = New System.Windows.Forms.LinkLabel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Lien4 = New System.Windows.Forms.LinkLabel
        Me.Lien2 = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.NewsList = New CI.Base.Windows.Forms.DataGridPlus
        Me.NewsDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.News = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NewsType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Viewed = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Label3 = New System.Windows.Forms.Label
        Me.version = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.NewsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Lien3)
        Me.GroupBox1.Controls.Add(Me.Lien1)
        Me.GroupBox1.Controls.Add(Me.Email2)
        Me.GroupBox1.Controls.Add(Me.Email1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Lien4)
        Me.GroupBox1.Controls.Add(Me.Lien2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(492, 123)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Contacts"
        '
        'Lien3
        '
        Me.Lien3.AutoSize = True
        Me.Lien3.Location = New System.Drawing.Point(80, 48)
        Me.Lien3.Name = "Lien3"
        Me.Lien3.Size = New System.Drawing.Size(105, 13)
        Me.Lien3.TabIndex = 7
        Me.Lien3.TabStop = True
        Me.Lien3.Text = "http://www.cints.net"
        '
        'Lien1
        '
        Me.Lien1.AutoSize = True
        Me.Lien1.Location = New System.Drawing.Point(80, 16)
        Me.Lien1.Name = "Lien1"
        Me.Lien1.Size = New System.Drawing.Size(141, 13)
        Me.Lien1.TabIndex = 6
        Me.Lien1.TabStop = True
        Me.Lien1.Text = "http://www.cints.net/Clinica"
        '
        'Email2
        '
        Me.Email2.AutoSize = True
        Me.Email2.Location = New System.Drawing.Point(80, 104)
        Me.Email2.Name = "Email2"
        Me.Email2.Size = New System.Drawing.Size(144, 13)
        Me.Email2.TabIndex = 5
        Me.Email2.TabStop = True
        Me.Email2.Text = "clinica@cyberinternautes.net"
        '
        'Email1
        '
        Me.Email1.AutoSize = True
        Me.Email1.Location = New System.Drawing.Point(80, 88)
        Me.Email1.Name = "Email1"
        Me.Email1.Size = New System.Drawing.Size(88, 13)
        Me.Email1.TabIndex = 4
        Me.Email1.TabStop = True
        Me.Email1.Text = "clinica@cints.net"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Courriel :"
        '
        'Lien4
        '
        Me.Lien4.AutoSize = True
        Me.Lien4.Location = New System.Drawing.Point(80, 64)
        Me.Lien4.Name = "Lien4"
        Me.Lien4.Size = New System.Drawing.Size(166, 13)
        Me.Lien4.TabIndex = 2
        Me.Lien4.TabStop = True
        Me.Lien4.Text = "http://www.cyberinternautes.net/"
        '
        'Lien2
        '
        Me.Lien2.AutoSize = True
        Me.Lien2.Location = New System.Drawing.Point(80, 32)
        Me.Lien2.Name = "Lien2"
        Me.Lien2.Size = New System.Drawing.Size(197, 13)
        Me.Lien2.TabIndex = 1
        Me.Lien2.TabStop = True
        Me.Lien2.Text = "http://www.cyberinternautes.net/Clinica"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sites internet :"
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 144)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(512, 164)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(504, 138)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Contacts"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.NewsList)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(512, 143)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Nouveautés"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'NewsList
        '
        Me.NewsList.AllowUserToAddRows = False
        Me.NewsList.AllowUserToDeleteRows = False
        Me.NewsList.AllowUserToResizeRows = False
        Me.NewsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewsList.autoSelectOnDataSourceChanged = True
        Me.NewsList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.NewsList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NewsList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.NewsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.NewsList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NewsDate, Me.News, Me.NewsType, Me.Viewed})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.NewsList.DefaultCellStyle = DataGridViewCellStyle3
        Me.NewsList.isDoubleBuffered = False
        Me.NewsList.Location = New System.Drawing.Point(3, 3)
        Me.NewsList.MultiSelect = False
        Me.NewsList.Name = "NewsList"
        Me.NewsList.ReadOnly = True
        Me.NewsList.RowHeadersVisible = False
        Me.NewsList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NewsList.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.NewsList.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.NewsList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NewsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.NewsList.Size = New System.Drawing.Size(503, 134)
        Me.NewsList.TabIndex = 0
        Me.NewsList.VirtualMode = True
        '
        'NewsDate
        '
        Me.NewsDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NewsDate.DataPropertyName = "Date"
        Me.NewsDate.HeaderText = "Date"
        Me.NewsDate.Name = "NewsDate"
        Me.NewsDate.ReadOnly = True
        Me.NewsDate.Width = 55
        '
        'News
        '
        Me.News.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.News.DataPropertyName = "Nouveauté"
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.News.DefaultCellStyle = DataGridViewCellStyle2
        Me.News.HeaderText = "Nouveauté"
        Me.News.Name = "News"
        Me.News.ReadOnly = True
        '
        'NewsType
        '
        Me.NewsType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NewsType.DataPropertyName = "Type"
        Me.NewsType.HeaderText = "Type"
        Me.NewsType.Name = "NewsType"
        Me.NewsType.ReadOnly = True
        Me.NewsType.Width = 56
        '
        'Viewed
        '
        Me.Viewed.DataPropertyName = "Viewed"
        Me.Viewed.HeaderText = "Viewed"
        Me.Viewed.Name = "Viewed"
        Me.Viewed.ReadOnly = True
        Me.Viewed.Visible = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(367, 298)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Version :"
        '
        'version
        '
        Me.version.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.version.AutoSize = True
        Me.version.Location = New System.Drawing.Point(421, 298)
        Me.version.Name = "version"
        Me.version.Size = New System.Drawing.Size(40, 13)
        Me.version.TabIndex = 2
        Me.version.Text = "0.0.0.0"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(504, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = ":"
        '
        'About
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(536, 320)
        Me.Controls.Add(Me.version)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TabControl1)
        Me.MinimumSize = New System.Drawing.Size(552, 359)
        Me.Name = "About"
        Me.ShowInTaskbar = False
        Me.Text = "À propos de Clinica"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.NewsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub aPropos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadNews()

        version.Text = My.Application.Info.Version.ToString
    End Sub

    Private Sub liens_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Lien1.LinkClicked, Lien2.LinkClicked, Lien3.LinkClicked, Lien4.LinkClicked

    End Sub

    Private Sub emails_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles Email1.LinkClicked, Email2.LinkClicked
        sendemailTo(CType(sender, LinkLabel).Text)
    End Sub

    Private Sub loadNews()
        Dim newsData As DataSet = DBLinker.getInstance().readDBForGrid("SoftwareNews INNER JOIN SoftwareNewsUsers ON SoftwareNews.NoSoftwareNews=SoftwareNewsUsers.NoSoftwareNews", "NewsDate AS [Date],SoftwareNews as [Nouveauté], NewsType AS [Type], SoftwareNewsUsers.Viewed", "WHERE NoUser=" & ConnectionsManager.currentUser & " ORDER BY Date DESC,Viewed,SoftwareNews.NoSoftwareNews DESC")
        NewsList.DataSource = newsData
        NewsList.DataMember = "Table"

        Dim isNew As Boolean = False
        Dim nbNew As Integer = 0
        For Each curRow As DataRow In newsData.Tables(0).Rows
            If curRow("Viewed") = False Then isNew = True : nbNew += 1
        Next

        If isNew Then TabPage2.Text = "* " & TabPage2.Text & " (" & nbNew & ") *"

        NewsList.Refresh()
    End Sub


    Private newsNewSet As Boolean = False

    Private Sub newsList_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles NewsList.DataBindingComplete
        With CType(NewsList.DataSource, DataSet).Tables(0)
            For i As Integer = 0 To .Rows.Count - 1
                If .Rows(i)(1).ToString.IndexOf("\n") <> -1 Then
                    .Rows(i)(1) = .Rows(i)(1).ToString.Replace("\n", vbCrLf)
                End If
            Next i
        End With
    End Sub

    Private Sub newsList_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles NewsList.Paint
        If newsNewSet = False Then
            Software.setNewsHasSeen()
            For i As Integer = 0 To NewsList.RowCount - 1
                If NewsList.Rows(i).Cells("Viewed").Value = False Then
                    NewsList.Rows(i).DefaultCellStyle.BackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
                    NewsList.Rows(i).DefaultCellStyle.SelectionBackColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
                End If
            Next i
            newsNewSet = True
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
