Public Class ClientCopyBoxStrip
    Inherits System.Windows.Forms.ToolStripControlHost

    Private myClientCopyBox As ClientCopyBox

    Public Sub New(ByVal newClientCopyBox As ClientCopyBox)
        MyBase.New(newClientCopyBox)

        Me.Dock = DockStyle.Right
        Me.Anchor = AnchorStyles.None
        Me.Overflow = ToolStripItemOverflow.Never
        Me.Visible = True
    End Sub

    Private Sub clientCopyBoxStrip_OwnerChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.OwnerChanged
        Me.BackColor = Me.Owner.BackColor
    End Sub
End Class
