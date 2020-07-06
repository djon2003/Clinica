Option Strict Off
Option Explicit On
Friend Class multichoice
    Inherits System.Windows.Forms.Form
#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public toolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents annuler As System.Windows.Forms.Button
    Public WithEvents elements As System.Windows.Forms.ListBox
    Public WithEvents ok As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.annuler = New System.Windows.Forms.Button
        Me.ok = New System.Windows.Forms.Button
        Me.elements = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'annuler
        '
        Me.annuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.annuler.BackColor = System.Drawing.SystemColors.Control
        Me.annuler.Cursor = System.Windows.Forms.Cursors.Default
        Me.annuler.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.annuler.ForeColor = System.Drawing.SystemColors.ControlText
        Me.annuler.Location = New System.Drawing.Point(184, 176)
        Me.annuler.Name = "annuler"
        Me.annuler.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.annuler.Size = New System.Drawing.Size(81, 25)
        Me.annuler.TabIndex = 2
        Me.annuler.Text = "Annuler"
        Me.toolTip1.SetToolTip(Me.annuler, "Choisir un élément")
        Me.annuler.UseVisualStyleBackColor = False
        '
        'ok
        '
        Me.ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ok.BackColor = System.Drawing.SystemColors.Control
        Me.ok.Cursor = System.Windows.Forms.Cursors.Default
        Me.ok.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ok.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ok.Location = New System.Drawing.Point(48, 176)
        Me.ok.Name = "ok"
        Me.ok.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ok.Size = New System.Drawing.Size(81, 25)
        Me.ok.TabIndex = 0
        Me.ok.Text = "Ok"
        Me.toolTip1.SetToolTip(Me.ok, "Choisir un élément")
        Me.ok.UseVisualStyleBackColor = False
        '
        'elements
        '
        Me.elements.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.elements.BackColor = System.Drawing.SystemColors.Window
        Me.elements.Cursor = System.Windows.Forms.Cursors.Default
        Me.elements.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.elements.ForeColor = System.Drawing.SystemColors.WindowText
        Me.elements.ItemHeight = 14
        Me.elements.Location = New System.Drawing.Point(0, 8)
        Me.elements.Name = "elements"
        Me.elements.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.elements.Size = New System.Drawing.Size(313, 158)
        Me.elements.Sorted = True
        Me.elements.TabIndex = 1
        '
        'multichoice
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(313, 207)
        Me.Controls.Add(Me.annuler)
        Me.Controls.Add(Me.elements)
        Me.Controls.Add(Me.ok)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(208, 182)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "multichoice"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Titre"
        Me.ResumeLayout(False)

    End Sub
#End Region

    Dim canceling As Boolean = False
    Dim accepting As Boolean = False

    Public ReadOnly Property GetChoice(ByVal title As String, ByVal Choices As String, Optional ByVal Index As String = "NOINDEX", Optional ByVal Separe As String = "", Optional ByVal Sort As Boolean = True, Optional ByVal ItemSelected As String = "", Optional ByVal NoDuplicateChoice As Boolean = False) As String
        Get
            elements.Sorted = Sort

            Dim Choi(), c As String, MaxChoice As String = ""
            Dim maxChars As Integer = 0
            If Separe = "" Then Separe = "§"
            Me.Text = title
            toolTip1.SetToolTip(elements, title)

            Choi = Choices.Split(New Char() {Separe})
            For Each c In Choi
                If NoDuplicateChoice = False Or (elements.FindStringExact(c) < 0 And NoDuplicateChoice = True) Then elements.Items.Add(c)
                If Math.Max(maxChars, c.Length) > maxChars Then
                    maxChars = Math.Max(maxChars, c.Length)
                    MaxChoice = c
                End If
            Next c

            Dim minWidth As Integer = Me.Width
            Me.Width = measureString(MaxChoice, elements.Font).Width + 35
            If Me.Width < minWidth Then Me.Width = minWidth

            If ItemSelected <> "" Then elements.SelectedIndex = elements.FindStringExact(ItemSelected)

            Me.ShowDialog()

            If canceling = False Then
                If Index.ToUpper = "INDEX" Then
                    If elements.SelectedIndex >= 0 Then
                        Return elements.SelectedIndex
                    Else
                        Return -1
                    End If
                Else
                    If elements.SelectedIndex >= 0 Then
                        Return elements.GetItemText(elements.SelectedItem)
                    Else
                        Return "ERROR"
                    End If
                End If
            Else
                If Index.ToUpper = "INDEX" Then
                    Return -1
                Else
                    Return "ERROR"
                End If
            End If
        End Get
    End Property
    Private Sub annuler_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles annuler.Click
        canceling = True
        Me.Close()
    End Sub

    Private Sub elements_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles elements.DoubleClick
        accepting = True
        Me.Close()
    End Sub

    Private Sub elements_Keyup(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles elements.KeyUp
        If e.KeyCode = Keys.Enter Then elements_DoubleClick(sender, EventArgs.Empty) : e.Handled = True
    End Sub

    Private Sub multichoice_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub ok_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ok.Click
        accepting = True
        Me.Close()
    End Sub

    Private Sub multichoice_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If accepting = False Then canceling = True
    End Sub
End Class
