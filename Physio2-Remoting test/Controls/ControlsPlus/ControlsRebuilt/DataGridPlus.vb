Public Class DataGridPlus
    Inherits System.Windows.Forms.DataGridView

    Private _AutoSelectOnDataSourceChanged As Boolean = False
    Private asodsCisDone As Boolean = False

    '#Region "Propriétés"
    '    Public Property AutoSelectFirstOnDataSourceChanged() As Boolean
    '        Get
    '            Return _AutoSelectFirstOnDataSourceChanged
    '        End Get
    '        Set(ByVal value As Boolean)
    '            _AutoSelectFirstOnDataSourceChanged = value
    '        End Set
    '    End Property
    '#End Region


    Protected Overrides Function processKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        MyBase.ProcessKeyPreview(m)
        Dim myKeyCode As Int32 = m.WParam.ToInt32
        If myKeyCode >= 37 And myKeyCode <= 40 Then
            'Enabling Arrows to raise the KeyPress event
            Me.OnKeyPress(New KeyPressEventArgs(Chr(myKeyCode)))
        End If
    End Function

    Private Sub dataGridPlus_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
        If asodsCisDone = False AndAlso _AutoSelectOnDataSourceChanged = False AndAlso Me.Rows.Count <> 0 AndAlso Me.Rows(0).Selected = True Then
            Me.ClearSelection()
            REM Had to remove this, because otherwise it keeps autoselecting the first row in viewmodifclients.ListEquipement
            'ASODSCisDone = True
        End If
    End Sub


End Class
