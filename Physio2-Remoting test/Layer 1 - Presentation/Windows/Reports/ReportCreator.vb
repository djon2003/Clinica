Friend Class ReportCreator
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
    Friend WithEvents RapportTypeProperties As System.Windows.Forms.PropertyGrid
    Friend WithEvents RapportTypesList As System.Windows.Forms.ListBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.RapportTypeProperties = New System.Windows.Forms.PropertyGrid
        Me.RapportTypesList = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'RapportTypeProperties
        '
        Me.RapportTypeProperties.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RapportTypeProperties.Location = New System.Drawing.Point(372, 12)
        Me.RapportTypeProperties.Name = "RapportTypeProperties"
        Me.RapportTypeProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
        Me.RapportTypeProperties.Size = New System.Drawing.Size(178, 329)
        Me.RapportTypeProperties.TabIndex = 0
        Me.RapportTypeProperties.ToolbarVisible = False
        '
        'RapportTypesList
        '
        Me.RapportTypesList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RapportTypesList.FormattingEnabled = True
        Me.RapportTypesList.Location = New System.Drawing.Point(12, 12)
        Me.RapportTypesList.Name = "RapportTypesList"
        Me.RapportTypesList.Size = New System.Drawing.Size(354, 329)
        Me.RapportTypesList.TabIndex = 1
        Me.RapportTypesList.Sorted = True
        '
        'RapportCreator
        '
        Me.ClientSize = New System.Drawing.Size(562, 350)
        Me.Controls.Add(Me.RapportTypesList)
        Me.Controls.Add(Me.RapportTypeProperties)
        Me.Name = "RapportCreator"
        Me.Text = "Créateur de rapport"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "RapportCreator Events"
    Private Sub rapportCreator_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim rtList As ArrayList = ReportsManager.getInstance.getReportTypes
        Dim RT(rtList.Count - 1) As Object
        rtList.CopyTo(RT)

        RapportTypesList.Items.AddRange(RT)
    End Sub
#End Region

    Private Sub rapportTypesList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RapportTypesList.SelectedIndexChanged
        If RapportTypesList.SelectedIndex <> -1 Then
            RapportTypeProperties.SelectedObject = RapportTypesList.SelectedItem
        End If
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            'REM Shall be something
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
