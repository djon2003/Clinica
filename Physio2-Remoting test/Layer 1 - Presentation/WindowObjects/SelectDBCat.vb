Public Class SelectDBCat

    Private returning As String = ""
    Private checkItemsDroit As Boolean = True

    Default Public ReadOnly Property Prompt(ByVal Titre As String, Optional ByVal CheckItemsDroit As Boolean = True) As String
        Get
            If Titre <> "" Then Me.Text = Titre

            Me.checkItemsDroit = CheckItemsDroit
            returning = ""
            Me.ShowDialog()
            Return returning
        End Get
    End Property

    Private Sub ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ok.Click
        returning = categories.SelectedNode.FullPath

        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        categories.showMenuSearch = False
        Me.categories.Sorted = True
        categories.refreshTree()
        Me.CenterToScreen()
    End Sub

    Private Sub annuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles annuler.Click
        returning = ""
        Me.Close()
    End Sub

    Private Sub categories_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles categories.AfterSelect
        If checkItemsDroit = False Then Exit Sub

        'Droit & Acces
        With CType(categories.SelectedNode.Tag, InternalDBFolder)
            If .noUser <> -1 AndAlso ((currentDroitAcces(3) And .noUser = 0) Or (currentDroitAcces(94) = True And .noUser <> ConnectionsManager.currentUser And .noUser <> 0) Or .noUser = ConnectionsManager.currentUser) Then
                ok.Enabled = True
            Else
                ok.Enabled = False
            End If
        End With
    End Sub

    Private Sub categories_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles categories.KeyDown
        If e.KeyCode = Keys.Enter And ok.Enabled = True Then
            e.SuppressKeyPress = True
            e.Handled = True
            ok_Click(sender, EventArgs.Empty)
        End If
    End Sub
End Class
