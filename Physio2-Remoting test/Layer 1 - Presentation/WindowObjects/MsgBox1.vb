Option Strict Off
Option Explicit On
Friend Class MsgBox1
    Inherits System.Windows.Forms.Form
    Public WithEvents boutton As BaseObjArray

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.boutton = New BaseObjArray
        Me.boutton.add(Me._boutton_0)
        Me.boutton.add(Me._boutton_1)
        Me.boutton.add(Me._boutton_2)
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
    Public WithEvents _boutton_1 As System.Windows.Forms.Button
    Public WithEvents _boutton_2 As System.Windows.Forms.Button
    Public WithEvents _boutton_0 As System.Windows.Forms.Button
    Public WithEvents message1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents calculateWidth As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me._boutton_1 = New System.Windows.Forms.Button
        Me._boutton_2 = New System.Windows.Forms.Button
        Me._boutton_0 = New System.Windows.Forms.Button
        Me.message1 = New System.Windows.Forms.Label
        Me.calculateWidth = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        '_boutton_1
        '
        Me._boutton_1.BackColor = System.Drawing.SystemColors.Control
        Me._boutton_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._boutton_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._boutton_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._boutton_1.Location = New System.Drawing.Point(8, 72)
        Me._boutton_1.Name = "_boutton_1"
        Me._boutton_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._boutton_1.Size = New System.Drawing.Size(65, 25)
        Me._boutton_1.TabIndex = 2
        Me._boutton_1.Text = "Ok"
        Me._boutton_1.UseVisualStyleBackColor = False
        Me._boutton_1.Visible = False
        '
        '_boutton_2
        '
        Me._boutton_2.BackColor = System.Drawing.SystemColors.Control
        Me._boutton_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._boutton_2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._boutton_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._boutton_2.Location = New System.Drawing.Point(8, 104)
        Me._boutton_2.Name = "_boutton_2"
        Me._boutton_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._boutton_2.Size = New System.Drawing.Size(65, 25)
        Me._boutton_2.TabIndex = 3
        Me._boutton_2.Text = "Ok"
        Me._boutton_2.UseVisualStyleBackColor = False
        Me._boutton_2.Visible = False
        '
        '_boutton_0
        '
        Me._boutton_0.BackColor = System.Drawing.SystemColors.Control
        Me._boutton_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._boutton_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._boutton_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._boutton_0.Location = New System.Drawing.Point(8, 40)
        Me._boutton_0.Name = "_boutton_0"
        Me._boutton_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._boutton_0.Size = New System.Drawing.Size(65, 25)
        Me._boutton_0.TabIndex = 0
        Me._boutton_0.Text = "Ok"
        Me._boutton_0.UseVisualStyleBackColor = False
        Me._boutton_0.Visible = False
        '
        'message1
        '
        Me.message1.AutoSize = True
        Me.message1.BackColor = System.Drawing.Color.Transparent
        Me.message1.Cursor = System.Windows.Forms.Cursors.Default
        Me.message1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.message1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.message1.Location = New System.Drawing.Point(16, 8)
        Me.message1.Name = "message1"
        Me.message1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.message1.Size = New System.Drawing.Size(51, 14)
        Me.message1.TabIndex = 1
        Me.message1.Text = "Message"
        '
        'calculateWidth
        '
        Me.calculateWidth.AutoSize = True
        Me.calculateWidth.BackColor = System.Drawing.Color.Transparent
        Me.calculateWidth.Cursor = System.Windows.Forms.Cursors.Default
        Me.calculateWidth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.calculateWidth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.calculateWidth.Location = New System.Drawing.Point(8, 128)
        Me.calculateWidth.Name = "calculateWidth"
        Me.calculateWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.calculateWidth.Size = New System.Drawing.Size(51, 14)
        Me.calculateWidth.TabIndex = 4
        Me.calculateWidth.Text = "Message"
        Me.calculateWidth.Visible = False
        '
        'MsgBox1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(124, 147)
        Me.Controls.Add(Me.calculateWidth)
        Me.Controls.Add(Me._boutton_1)
        Me.Controls.Add(Me._boutton_2)
        Me.Controls.Add(Me._boutton_0)
        Me.Controls.Add(Me.message1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(342, 213)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MsgBox1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Titre du message"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region "Boutton Events"
    Private Sub _boutton_0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _boutton_0.Click
        boutton_Click(0, sender, e)
    End Sub
    Private Sub _boutton_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _boutton_1.Click
        boutton_Click(1, sender, e)
    End Sub
    Private Sub _boutton_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _boutton_2.Click
        boutton_Click(2, sender, e)
    End Sub
#End Region

    Dim msgReturn As Byte = 0
    Dim closeMe As Boolean
    Default Public ReadOnly Property Message(ByVal Msg As String, ByVal Titre As String, ByVal Button As Byte, Optional ByVal Titre1 As String = "Bouton 1", Optional ByVal Titre2 As String = "Bouton 2", Optional ByVal Titre3 As String = "Bouton 3", Optional ByVal Closable As Boolean = True) As Byte
        Get
            Dim i, n, startingIndex As Short
            '############
            'EXPLICATION
            '############
            'Msg = Message
            'Titre = Titre de la MessageBox
            'Button = Nombre de bouton nécessaire, max 3
            'From = Feuille de provenance
            'Titre1 = Caption du bouton 1
            'Titre2 = Caption du bouton 2
            'Titre3 = Caption du bouton 3

            'MWidth = MessageWidth
            'BWidth = BoutonWidth
            'BTop = BoutonTop
            'BL = BoutonLeft par rapport au # du bouton
            'FWidth = FormWidth
            Dim BL, BWidth, MinWidth, BTop, fWidth As Integer
            closeMe = Closable

            Me.Text = Titre
            message1.Text = Msg
            If Button > 3 Then Button = 3

            'Définition de la largeur des boutons
            Dim minBWidth As Integer = 0
            For i = 0 To Button - 1
                Select Case i
                    Case 0
                        calculateWidth.Text = Titre1
                    Case 1
                        calculateWidth.Text = Titre2
                    Case 2
                        calculateWidth.Text = Titre3
                End Select

                If (calculateWidth.Width + 8) > minBWidth Then minBWidth = calculateWidth.Width + 8
            Next i

            For i = 0 To Button - 1
                boutton(i).width = minBWidth
            Next i

            n = 1 : startingIndex = 0
            Do Until Msg.IndexOf(vbCrLf, startingIndex) = -1
                n += 1
                startingIndex = Msg.IndexOf(vbCrLf, startingIndex) + vbCrLf.Length
            Loop

            'Définition de variables
            fWidth = 32 + (message1.Width)
            message1.AutoSize = False
            message1.Width = fWidth - 32
            message1.Height = message1.Height * n
            MinWidth = 88
            BWidth = (Button * (boutton(0).Width + 8)) + 8
            BTop = 16 + message1.Height

            'Définition des grandeurs
            'Feuille
            If fWidth < MinWidth Then fWidth = MinWidth
            Me.Height = 81 + message1.Height
            If fWidth <= BWidth Then fWidth = BWidth
            Me.Width = (fWidth)
            BL = CInt(((Me.ClientRectangle.Width) - (BWidth - 16)) / 2)

            'Déplacement de tous les objets
            message1.Left = (((Me.ClientRectangle.Width) - (message1.Width)) / 2)
            message1.Top = 8

            'Les boutons
            For i = 0 To Button - 1

                Select Case i
                    Case 0
                        With boutton(i)
                            .Text = Titre1
                            toolTip1.SetToolTip(boutton(i), Titre1)
                        End With
                    Case 1
                        With boutton(i)
                            .Text = Titre2
                            toolTip1.SetToolTip(boutton(i), Titre2)
                        End With
                    Case 2
                        With boutton(i)
                            .Text = Titre3
                            toolTip1.SetToolTip(boutton(i), Titre3)
                        End With
                End Select

                With boutton(i)
                    .Left = (BL)
                    .Top = (BTop)
                    .Visible = True
                End With
                BL += CInt((boutton(i).Width)) + 8
            Next i

            Me.Top = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Me.Height) / 2
            Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Me.Width) / 2
            Me.ShowDialog()
            Return msgReturn
fin:
        End Get
    End Property

    Private Sub boutton_Click(ByRef index As Short, ByVal sender As Object, ByVal e As System.EventArgs) Handles boutton.click
        msgReturn = index + 1
        closeMe = True
        Me.Close()
    End Sub

    Private Sub msgBox1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If closeMe = False Then e.Cancel = True
    End Sub

    Private Sub msgBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub
End Class
