Option Strict Off
Option Explicit On
Imports System.Data.OleDb

Friend Class Access2
    Inherits System.Windows.Forms.Form

    Private logoWin As logo
    Private showLogoBackground As Boolean
    Private Const ws_ex_transparent As Int32 = &H20
    Private baseCP As CreateParams = Nothing

#Region "Windows Form Designer generated code "


    Public Sub New(ByVal logoWin As logo, ByVal showLogoBackground As Boolean)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()


        Me.SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()
        Me.BackColor = Color.Transparent
        Me.ResizeRedraw = True
        'Me.DoubleBuffered = True

        Me.logoWin = logoWin
        Me.showLogoBackground = showLogoBackground
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
    Public WithEvents mdp As System.Windows.Forms.TextBox
    Public WithEvents user As System.Windows.Forms.ComboBox
    Public WithEvents entrer As System.Windows.Forms.Button
    Public WithEvents quit As System.Windows.Forms.Button
    Public WithEvents label2 As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.mdp = New System.Windows.Forms.TextBox
        Me.user = New System.Windows.Forms.ComboBox
        Me.entrer = New System.Windows.Forms.Button
        Me.quit = New System.Windows.Forms.Button
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'mdp
        '
        Me.mdp.AcceptsReturn = True
        Me.mdp.BackColor = System.Drawing.SystemColors.Window
        Me.mdp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.mdp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mdp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.mdp.Location = New System.Drawing.Point(10, 68)
        Me.mdp.MaxLength = 0
        Me.mdp.Name = "mdp"
        Me.mdp.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.mdp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mdp.Size = New System.Drawing.Size(283, 20)
        Me.mdp.TabIndex = 13
        '
        'user
        '
        Me.user.BackColor = System.Drawing.SystemColors.Window
        Me.user.Cursor = System.Windows.Forms.Cursors.Default
        Me.user.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.user.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.user.ForeColor = System.Drawing.SystemColors.WindowText
        Me.user.Location = New System.Drawing.Point(10, 13)
        Me.user.Name = "user"
        Me.user.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.user.Size = New System.Drawing.Size(283, 22)
        Me.user.Sorted = True
        Me.user.TabIndex = 12
        '
        'entrer
        '
        Me.entrer.BackColor = System.Drawing.Color.White
        Me.entrer.Cursor = System.Windows.Forms.Cursors.Default
        Me.entrer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.entrer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.entrer.Location = New System.Drawing.Point(45, 105)
        Me.entrer.Name = "entrer"
        Me.entrer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.entrer.Size = New System.Drawing.Size(73, 25)
        Me.entrer.TabIndex = 14
        Me.entrer.Text = "Entrer"
        Me.entrer.UseVisualStyleBackColor = False
        '
        'quit
        '
        Me.quit.BackColor = System.Drawing.Color.White
        Me.quit.Cursor = System.Windows.Forms.Cursors.Default
        Me.quit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.quit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.quit.Location = New System.Drawing.Point(187, 105)
        Me.quit.Name = "quit"
        Me.quit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.quit.Size = New System.Drawing.Size(73, 25)
        Me.quit.TabIndex = 15
        Me.quit.Text = "Quitter"
        Me.quit.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Location = New System.Drawing.Point(10, 52)
        Me.label2.Name = "Label2"
        Me.label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label2.Size = New System.Drawing.Size(88, 14)
        Me.label2.TabIndex = 17
        Me.label2.Text = "Mot de passe :"
        '
        'Label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(10, -3)
        Me.label1.Name = "Label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(69, 14)
        Me.label1.TabIndex = 16
        Me.label1.Text = "Utilisateur :"
        '
        'Acces
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.Pink
        Me.ClientSize = New System.Drawing.Size(305, 130)
        Me.ControlBox = False
        Me.Controls.Add(Me.mdp)
        Me.Controls.Add(Me.user)
        Me.Controls.Add(Me.entrer)
        Me.Controls.Add(Me.quit)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Acces"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Accès à Clinica"
        Me.TopMost = True
        'Me.TransparencyKey = System.Drawing.Color.Pink
        Me.ResumeLayout(False)
    End Sub
#End Region

    Protected Overrides ReadOnly Property createParams() As CreateParams
        Get
            If baseCP Is Nothing Then baseCP = MyBase.CreateParams
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or ws_ex_transparent
            Return cp
        End Get
    End Property


    Protected Overrides Sub onPaintBackground(ByVal e As PaintEventArgs)
        'If isShown Then MyBase.OnPaintBackground(e)
        e = e
        'Important - DO NOT DELETE
    End Sub

    Private Sub entrer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles entrer.Click
        If user.SelectedIndex < 0 Then MessageBox.Show("Veuillez choisir un utilisateur", "Utilisateur non-choisi") : Exit Sub

        If user.GetItemText(user.SelectedItem) = "* Administrateur *" Then
            ConnectionsManager.currentUser = 0
            currentUserName = "Administrateur"
        Else
            ConnectionsManager.currentUser = CType(user.SelectedItem, User).noUser
            currentUserName = CType(user.SelectedItem, User).getFullName()
        End If

        'Vérification du mot de passe
        'Administrateur
        If ConnectionsManager.currentUser = 0 And Not mdp.Text = PreferencesManager.getGeneralPreferences()("AdministratorPassword") Then MessageBox.Show("Mot de passe incorrect", "Mot de passe") : Exit Sub

        'Utilisateur
        If Not ConnectionsManager.currentUser = 0 Then
            Dim curUser As User = user.SelectedItem

            Dim myMdp As String = mdp.Text
            If PreferencesManager.getGeneralPreferences()("UserMDPRespectCasse") = False Then myMdp = myMdp.ToUpper
            Dim curMDP As String = mdpProcessToModif(curUser.passwordKey, myMdp)
            If Not curMDP.Replace(",", ".") = curUser.password.Replace(",", ".") Then MessageBox.Show("Mot de passe incorrect", "Mot de passe") : Exit Sub

            Dim daLine As String = curUser.rights
            If daLine.StartsWith("3") Then daLine = daLine.Substring(1)
            currentDroitAcces = splitStr(daLine, 1)
        Else
            currentDroitAcces = splitStr("211111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", 1)
        End If

        If checkForExistingConnections() = False Then Exit Sub

        modifFile("Users\Connected\" & ConnectionsManager.currentUser & "-" & Environment.MachineName, 0, DateFormat.getTextDate(Date.Now) & " " & DateFormat.getTextDate(Date.Now, DateFormat.TextDateOptions.ShortTime))

        Me.Hide()
    End Sub

    Private Function checkForExistingConnections() As Boolean
        Dim myFiles() As String = IO.Directory.GetFiles(appPath & bar(appPath) & "Users\Connected", ConnectionsManager.currentUser & "-*")
        If myFiles.Length = 1 AndAlso myFiles(0).EndsWith("\" & ConnectionsManager.currentUser & "-" & Environment.MachineName) Then
            'If same computer with only one file
            IO.File.Delete(myFiles(0))
        ElseIf myFiles.Length <> 0 Then
            'Scan for connection lost file on same computer
            Dim sameFound As Boolean = False
            Dim i As Integer = 0
            For Each MyFile As String In myFiles
                'Reposition array if same found
                If sameFound Then
                    myFiles(i - 1) = MyFile
                End If

                If MyFile.EndsWith("\" & ConnectionsManager.currentUser & "-" & Environment.MachineName) Then
                    'Même ordinateur, même utilisateur, alors on supprime simplement le fichier
                    IO.File.Delete(MyFile)
                    sameFound = True
                End If

                i += 1
            Next
            If sameFound Then ReDim Preserve myFiles(myFiles.Length - 2)

            'Look for other connection files
            i = 1
            For Each MyFile As String In myFiles
                Dim thisRegEx As New System.Text.RegularExpressions.Regex("\-")
                Dim sMyFile() As String = thisRegEx.Split(MyFile, 2)
                Dim remoteComputerName As String = sMyFile(1)

                Dim myMsgBox As New MsgBox1
                myMsgBox.TopMost = True
                Dim msgReturn As Byte = myMsgBox("Cet utilisateur est déjà connecté au logiciel sur le poste """ & remoteComputerName & """." & vbCrLf & "Que désirez-vous faire ?", "Utilisateur déjà connecté", 3, "Continuer en laissant l'autre poste ouvert", "Déconnecter le poste " & remoteComputerName, "Annuler")
                If msgReturn = 3 Then Return False

                If msgReturn = 2 Then
                    Me.Hide()
                    If Software.disconnectRemoteComputer(ConnectionsManager.currentUser, remoteComputerName) = False Then
                        Me.Show()
                        Return False
                    End If
                End If

                i += 1
            Next
        End If

        Return True
    End Function

    Private Sub Access_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Invalidate()
    End Sub

    Private Sub acces_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub acces_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim i As Short

        Dim users As Generic.List(Of User) = UsersManager.getInstance.getUsers(False)
        user.Items.AddRange(users.ToArray)

        If users.Count = 0 OrElse forceShowAdmin = True OrElse PreferencesManager.getGeneralPreferences() Is Nothing OrElse PreferencesManager.getGeneralPreferences()("AffAdminInAcces") = True Then
            user.Items.Add("* Administrateur *")
        End If
        user.SelectedIndex = 0

        If PreferencesManager.getGeneralPreferences() IsNot Nothing AndAlso PreferencesManager.getGeneralPreferences()("AutoSelectLastUserInAcces") Then
            Dim myLastUser As Integer
            myLastUser = Software.config.lastUserConnected

            If myLastUser > 0 Then
                For i = 0 To user.Items.Count - 1
                    If user.GetItemText(user.Items(i)).EndsWith("(" & myLastUser & ")") Then user.SelectedIndex = i
                Next i
            End If
        End If
        Me.CenterToScreen()

        logoWin.showBackground = showLogoBackground
    End Sub

    Private Sub mdp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles mdp.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        If keyAscii = 13 Then keyAscii = 0 : entrer_Click(entrer, New System.EventArgs())
        If keyAscii = 0 Then eventArgs.Handled = True
    End Sub

    Private Sub quit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles quit.Click
        Try
            Software.doEndProcess()
        Catch ex As Exception
            addErrorLog(ex)
        End Try
        End
    End Sub

    Private Sub user_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles User.SelectedIndexChanged
        If user.GetItemText(user.SelectedItem) = "* Administrateur *" Then
            mdp.MaxLength = 100
        Else
            mdp.Text = Microsoft.VisualBasic.Left(mdp.Text, 10)
            mdp.MaxLength = 10
        End If
    End Sub

    Private Sub user_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles User.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        If keyAscii = 13 Then keyAscii = 0 : mdp.Focus()
        If keyAscii = 0 Then eventArgs.Handled = True
    End Sub

    Private Sub acces_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' e.Cancel = True
    End Sub

    Private Sub Access_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        '        Me.Invalidate(True)
    End Sub

    Private Sub Access_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown

    End Sub

    Private isShown As Boolean = False

    Private Sub Access_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Me.BackColor = Color.Transparent
        'Me.Refresh()
        isShown = True
        Me.Invalidate(True)
    End Sub
End Class
