Option Strict Off
Option Explicit On
Friend Class modifusers
    Inherits SingleWindow

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.
        Me.MdiParent = myMainWin

        With DrawingManager.getInstance
            Me.adduser.Image = .getImage("ajouter16.gif")
            Me.modifuser.Image = .getImage("modifier16.gif")
            Me.deluser.Image = DrawingManager.iconToImage(.getIcon("delete16.ico"), New Size(16, 16))
            Me.Icon = DrawingManager.imageToIcon(.getImage("user.gif"))
        End With
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
    Public WithEvents deluser As System.Windows.Forms.Button
    Public WithEvents modifuser As System.Windows.Forms.Button
    Public WithEvents facturation As System.Windows.Forms.Button
    Public WithEvents paye As System.Windows.Forms.Button
    Public WithEvents horaire As System.Windows.Forms.Button
    Public WithEvents adduser As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents DirUsers As System.Windows.Forms.ListBox
    Public WithEvents modiftypeuser As System.Windows.Forms.Button
    Friend WithEvents ShowAllUsers As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.deluser = New System.Windows.Forms.Button
        Me.modifuser = New System.Windows.Forms.Button
        Me.facturation = New System.Windows.Forms.Button
        Me.paye = New System.Windows.Forms.Button
        Me.horaire = New System.Windows.Forms.Button
        Me.adduser = New System.Windows.Forms.Button
        Me.DirUsers = New System.Windows.Forms.ListBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.modiftypeuser = New System.Windows.Forms.Button
        Me.ShowAllUsers = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'deluser
        '
        Me.deluser.BackColor = System.Drawing.SystemColors.Control
        Me.deluser.Cursor = System.Windows.Forms.Cursors.Default
        Me.deluser.Enabled = False
        Me.deluser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.deluser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.deluser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.deluser.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.deluser.Location = New System.Drawing.Point(240, 114)
        Me.deluser.Name = "deluser"
        Me.deluser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.deluser.Size = New System.Drawing.Size(24, 24)
        Me.deluser.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.deluser, "Supprimer un utilisateur." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Pour supprimer un utilisateur il faut être l'unique ut" & _
                "illisateur connecté et n'avoir que cette fenêtre d'ouverte")
        Me.deluser.UseVisualStyleBackColor = False
        '
        'modifuser
        '
        Me.modifuser.BackColor = System.Drawing.SystemColors.Control
        Me.modifuser.Cursor = System.Windows.Forms.Cursors.Default
        Me.modifuser.Enabled = False
        Me.modifuser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.modifuser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifuser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modifuser.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.modifuser.Location = New System.Drawing.Point(240, 60)
        Me.modifuser.Name = "modifuser"
        Me.modifuser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modifuser.Size = New System.Drawing.Size(24, 24)
        Me.modifuser.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.modifuser, "Modification d'un utilisateur")
        Me.modifuser.UseVisualStyleBackColor = False
        '
        'facturation
        '
        Me.facturation.BackColor = System.Drawing.SystemColors.Control
        Me.facturation.Cursor = System.Windows.Forms.Cursors.Default
        Me.facturation.Enabled = False
        Me.facturation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.facturation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.facturation.Location = New System.Drawing.Point(281, 68)
        Me.facturation.Name = "facturation"
        Me.facturation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.facturation.Size = New System.Drawing.Size(108, 24)
        Me.facturation.TabIndex = 8
        Me.facturation.Text = "Facturation"
        Me.facturation.UseVisualStyleBackColor = False
        '
        'paye
        '
        Me.paye.BackColor = System.Drawing.SystemColors.Control
        Me.paye.Cursor = System.Windows.Forms.Cursors.Default
        Me.paye.Enabled = False
        Me.paye.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.paye.ForeColor = System.Drawing.SystemColors.ControlText
        Me.paye.Location = New System.Drawing.Point(281, 38)
        Me.paye.Name = "paye"
        Me.paye.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.paye.Size = New System.Drawing.Size(108, 24)
        Me.paye.TabIndex = 7
        Me.paye.Text = "Paye"
        Me.paye.UseVisualStyleBackColor = False
        '
        'horaire
        '
        Me.horaire.BackColor = System.Drawing.SystemColors.Control
        Me.horaire.Cursor = System.Windows.Forms.Cursors.Default
        Me.horaire.Enabled = False
        Me.horaire.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.horaire.ForeColor = System.Drawing.SystemColors.ControlText
        Me.horaire.Location = New System.Drawing.Point(281, 8)
        Me.horaire.Name = "horaire"
        Me.horaire.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.horaire.Size = New System.Drawing.Size(108, 24)
        Me.horaire.TabIndex = 6
        Me.horaire.Text = "Horaire"
        Me.horaire.UseVisualStyleBackColor = False
        '
        'adduser
        '
        Me.adduser.BackColor = System.Drawing.SystemColors.Control
        Me.adduser.Cursor = System.Windows.Forms.Cursors.Default
        Me.adduser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.adduser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adduser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.adduser.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.adduser.Location = New System.Drawing.Point(240, 8)
        Me.adduser.Name = "adduser"
        Me.adduser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.adduser.Size = New System.Drawing.Size(24, 24)
        Me.adduser.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.adduser, "Ajout d'un utilisateur")
        Me.adduser.UseVisualStyleBackColor = False
        '
        'DirUsers
        '
        Me.DirUsers.HorizontalScrollbar = True
        Me.DirUsers.ItemHeight = 14
        Me.DirUsers.Location = New System.Drawing.Point(8, 8)
        Me.DirUsers.Name = "DirUsers"
        Me.DirUsers.Size = New System.Drawing.Size(224, 130)
        Me.DirUsers.Sorted = True
        Me.DirUsers.TabIndex = 11
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'modiftypeuser
        '
        Me.modiftypeuser.BackColor = System.Drawing.SystemColors.Control
        Me.modiftypeuser.Cursor = System.Windows.Forms.Cursors.Default
        Me.modiftypeuser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modiftypeuser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.modiftypeuser.Location = New System.Drawing.Point(281, 114)
        Me.modiftypeuser.Name = "modiftypeuser"
        Me.modiftypeuser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.modiftypeuser.Size = New System.Drawing.Size(108, 24)
        Me.modiftypeuser.TabIndex = 5
        Me.modiftypeuser.Text = "Types d'utilisateurs"
        Me.modiftypeuser.UseVisualStyleBackColor = False
        '
        'ShowAllUsers
        '
        Me.ShowAllUsers.AutoSize = True
        Me.ShowAllUsers.Checked = True
        Me.ShowAllUsers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowAllUsers.Location = New System.Drawing.Point(8, 143)
        Me.ShowAllUsers.Name = "ShowAllUsers"
        Me.ShowAllUsers.Size = New System.Drawing.Size(290, 18)
        Me.ShowAllUsers.TabIndex = 12
        Me.ShowAllUsers.Text = "Afficher les utilisateurs ayant une date de fin de travail"
        Me.ShowAllUsers.UseVisualStyleBackColor = True
        '
        'modifusers
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(395, 166)
        Me.Controls.Add(Me.ShowAllUsers)
        Me.Controls.Add(Me.DirUsers)
        Me.Controls.Add(Me.deluser)
        Me.Controls.Add(Me.modifuser)
        Me.Controls.Add(Me.facturation)
        Me.Controls.Add(Me.paye)
        Me.Controls.Add(Me.horaire)
        Me.Controls.Add(Me.modiftypeuser)
        Me.Controls.Add(Me.adduser)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(374, 370)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "modifusers"
        Me.ShowInTaskbar = False
        Me.Text = "Gestion des utilisateurs"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Public Sub loadUsers()
        lockItems(True)
        DirUsers.Items.Clear()

        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(ShowAllUsers.Checked)
        DirUsers.Items.AddRange(users.ToArray)
    End Sub

    Public Function getNoUser() As Integer
        If DirUsers.SelectedIndex < 0 Then Exit Function

        Dim sUsers() As String
        sUsers = DirUsers.GetItemText(DirUsers.SelectedItem).Split(New Char() {"("})
        sUsers(1) = sUsers(1).Substring(0, sUsers(1).Length - 1)

        Return sUsers(1)
    End Function

    Private Sub adduser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles adduser.Click
        Comptes.addUser()
        loadUsers()
    End Sub

    Private Sub deluser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles deluser.Click
        CType(DirUsers.SelectedItem, User).delete()
        loadUsers()
    End Sub

    Private Sub dirUsers_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles DirUsers.SelectedIndexChanged
        If DirUsers.SelectedIndex > -1 Then
            lockItems(False)
        Else
            lockItems(True)
        End If
    End Sub

    Private Sub facturation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles facturation.Click
        Dim noUser As Integer = getNoUser()
        'Droit & Accès
        If currentDroitAcces(81) = False And noUser <> ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Facturation de tous les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myModifFacturation As modiffacturation = openUniqueWindow(New modiffacturation(), "Facturation d'un travailleur autonome")
        myModifFacturation.dedicatedType = FacturationBox.DedicatedType.User
        myModifFacturation.noUser = noUser
        myModifFacturation.Show()
    End Sub

    Private Sub modifusers_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        loadUsers()
    End Sub

    Private Sub horaire_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles horaire.Click
        openModifHoraire(getNoUser)
    End Sub

    Private Sub modiftypeuser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles modiftypeuser.Click
        openTypesUser(Me)
    End Sub

    Private Sub modifuser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles modifuser.Click
        openAccount(Me.getNoUser(), CompteType.User)
        loadUsers()
        DirUsers.SelectedIndex = -1
    End Sub


    Private Sub paye_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles paye.Click
        Dim noUser As Integer = getNoUser()
        'Droit & Accès
        If currentDroitAcces(81) = False And noUser <> ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à ce secteur du logiciel : Gestion des paies de tous les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myShowPaye As showpaye = openUniqueWindow(New showpaye(), , , True)
        myShowPaye.loading(noUser)
        myShowPaye.Show()
    End Sub

    Public Sub lockItems(ByRef trueFalse As Boolean)
        Dim noUser As Integer
        noUser = getNoUser()

        modifuser.Enabled = Not trueFalse
        deluser.Enabled = Not trueFalse
        horaire.Enabled = Not trueFalse
        facturation.Enabled = Not trueFalse
        paye.Enabled = Not trueFalse

        If trueFalse = False Then
            Dim curUser As User = UsersManager.getInstance.getUser(noUser)
            If curUser IsNot Nothing Then
                If curUser.noEmployeeType = 1 Then
                    facturation.Enabled = False
                Else
                    paye.Enabled = False
                End If
            Else
                paye.Enabled = False
                facturation.Enabled = False
            End If
        End If
    End Sub

    Private Sub showAllUsers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowAllUsers.CheckedChanged
        loadUsers()
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
