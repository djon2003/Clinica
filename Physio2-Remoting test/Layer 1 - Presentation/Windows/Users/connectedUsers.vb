Friend Class connectedUsers
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents users As System.Windows.Forms.ListBox
    Friend WithEvents disconnectUser As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents msgToUser As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.users = New System.Windows.Forms.ListBox
        Me.disconnectUser = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.msgToUser = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'users
        '
        Me.users.FormattingEnabled = True
        Me.users.Location = New System.Drawing.Point(12, 12)
        Me.users.Name = "users"
        Me.users.Size = New System.Drawing.Size(386, 225)
        Me.users.TabIndex = 3
        '
        'disconnectUser
        '
        Me.disconnectUser.Location = New System.Drawing.Point(404, 75)
        Me.disconnectUser.Name = "disconnectUser"
        Me.disconnectUser.Size = New System.Drawing.Size(91, 23)
        Me.disconnectUser.TabIndex = 0
        Me.disconnectUser.Text = "Déconnecter"
        Me.disconnectUser.UseVisualStyleBackColor = True
        '
        'refresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(404, 12)
        Me.btnRefresh.Name = "refresh"
        Me.btnRefresh.Size = New System.Drawing.Size(91, 23)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "Rafraîchir"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'msgToUser
        '
        Me.msgToUser.Location = New System.Drawing.Point(404, 46)
        Me.msgToUser.Name = "msgToUser"
        Me.msgToUser.Size = New System.Drawing.Size(91, 23)
        Me.msgToUser.TabIndex = 0
        Me.msgToUser.Text = "Message"
        Me.msgToUser.UseVisualStyleBackColor = True
        '
        'connectedUsers
        '
        Me.ClientSize = New System.Drawing.Size(506, 250)
        Me.Controls.Add(Me.users)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.msgToUser)
        Me.Controls.Add(Me.disconnectUser)
        Me.Name = "connectedUsers"
        Me.Text = "Utilisateurs connectés"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "delSelectedTables Events"
    Private Sub delSelectedTables_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        loadTable()
    End Sub
#End Region

    Private curConnectedFiles() As String

    Private Sub loadTable()
        users.Items.Clear()

        curConnectedFiles = IO.Directory.GetFiles(appPath & bar(appPath) & "Users\Connected")
        For i As Integer = 0 To curConnectedFiles.Length - 1
            Dim connectedTil As String = IO.File.ReadAllLines(curConnectedFiles(i))(0)
            Dim fileName As String = getLastDir(curConnectedFiles(i))
            Dim sNoUser() As String = fileName.Split(New Char() {"-"})
            users.Items.Add(sNoUser(0) & "-" & UsersManager.getInstance.getUser(sNoUser(0)).getFullName() & " sur le poste " & sNoUser(1) & " depuis " & connectedTil)
        Next i
    End Sub

    Private Sub refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        loadTable()
    End Sub

    Private Sub lockItems(ByVal locked As Boolean)
        users.Enabled = Not locked
        btnRefresh.Enabled = Not locked
        msgToUser.Enabled = Not locked
        disconnectUser.Enabled = Not locked
    End Sub

    Private Sub disconnectUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles disconnectUser.Click
        lockItems(True)
        disconnectUser.Text = "Déconnexion..."

        Dim sUser() As String = users.Items(users.SelectedIndex).ToString.Split(New Char() {"-"})
        InternalUpdatesManager.getInstance.sendUpdate("Close(" & sUser(0) & "," & curConnectedFiles(users.SelectedIndex).Substring(curConnectedFiles(users.SelectedIndex).IndexOf("-") + 1) & ")")

        Dim timeOutLoop As Integer
        While IO.File.Exists(curConnectedFiles(users.SelectedIndex)) = True
            Threading.Thread.Sleep(500)
            timeOutLoop += 1
            Application.DoEvents()
            If timeOutLoop > 100 Then Exit While
        End While

        If timeOutLoop >= 100 Then
            Dim computerName As String = curConnectedFiles(users.SelectedIndex).Substring(curConnectedFiles(users.SelectedIndex).LastIndexOf("-") + 1)
            If MessageBox.Show("Impossible de déconnecter l'utilisateur." & vbCrLf & "Veuillez vérifier l'ordinateur nommé """ & computerName & """. Désirez-vous supprimer le fichier de connexion quand même ?", "Utilisateur déjà connecté", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                IO.File.Delete(curConnectedFiles(users.SelectedIndex))
                loadTable()
            End If
        Else
            loadTable()
        End If

        disconnectUser.Text = "Déconnecter"
        lockItems(False)
    End Sub

    Private Sub msgToUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msgToUser.Click
        Dim myInputBoxPlus As New InputBoxPlus
        myInputBoxPlus.refusedChars = "(§)§,"
        Dim message As String = myInputBoxPlus.Prompt("Entrer le message à envoyer", "Message", "")
        If message = "" Then Exit Sub

        Dim sUser() As String = users.Items(users.SelectedIndex).ToString.Split(New Char() {"-"})
        InternalUpdatesManager.getInstance.sendUpdate("Message(" & sUser(0) & "," & ConnectionsManager.currentUser & "," & message & ")")
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub
End Class
