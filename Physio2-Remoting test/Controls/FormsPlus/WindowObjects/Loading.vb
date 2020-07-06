Public Class Loading
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Private Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.TopMost = True
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
    Private WithEvents loadingDetail As System.Windows.Forms.Label

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents loadingLabel As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.loadingLabel = New System.Windows.Forms.Label
        Me.loadingDetail = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'LoadingLabel
        '
        Me.loadingLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.loadingLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loadingLabel.Location = New System.Drawing.Point(0, 0)
        Me.loadingLabel.Name = "LoadingLabel"
        Me.loadingLabel.Size = New System.Drawing.Size(344, 27)
        Me.loadingLabel.TabIndex = 0
        Me.loadingLabel.Text = "Chargement."
        Me.loadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LoadingDetail
        '
        Me.loadingDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.loadingDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loadingDetail.Location = New System.Drawing.Point(0, 26)
        Me.loadingDetail.Name = "LoadingDetail"
        Me.loadingDetail.Size = New System.Drawing.Size(344, 27)
        Me.loadingDetail.TabIndex = 0
        Me.loadingDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Loading
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(344, 54)
        Me.Controls.Add(Me.loadingDetail)
        Me.Controls.Add(Me.loadingLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Loading"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Loading"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Shared mySelf As Loading = Nothing
    Private Shared loaded As Boolean = False

    Public Sub forward(Optional ByVal nbPoints As Integer = 1)
        Dim sb As New System.Text.StringBuilder(loadingLabel.Text)
        sb.Append(".", nbPoints)

        loadingLabel.Text = sb.ToString
        loadingDetail.Text = ""
        Application.DoEvents()
    End Sub

    Public Sub forward(ByVal curLoadingDetail As String, Optional ByVal nbPoints As Integer = 1)
        Dim sb As New System.Text.StringBuilder(loadingLabel.Text)
        sb.Append(".", nbPoints)

        loadingLabel.Text = sb.ToString
        loadingDetail.Text = curLoadingDetail
        Application.DoEvents()
    End Sub

    Public Shared Function getInstance() As Loading
        If loaded = False Then mySelf = New Loading() : loaded = True

        Return mySelf
    End Function

    Private Sub loading_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        loaded = False
        REM SHALL BE TRANSFERED TO END OF Software.STart    If Not MyMainWin Is Nothing Then MyMainWin.LockItems(False)
    End Sub
End Class
