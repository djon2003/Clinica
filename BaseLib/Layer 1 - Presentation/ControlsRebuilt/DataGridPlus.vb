Namespace Windows.Forms


    Public Class DataGridPlus
        Inherits System.Windows.Forms.DataGridView

        Private _AutoSelectOnDataSourceChanged As Boolean = True
        Private asodsCisDone As Boolean = False
        Private _currentRow As DataGridViewRow
        Private _currentCell As DataGridViewCell

#Region "Propriétés"
        Public Property isDoubleBuffered() As Boolean
            Get
                Return Me.DoubleBuffered
            End Get
            Set(ByVal value As Boolean)
                Me.DoubleBuffered = value
            End Set
        End Property

        Public Property autoSelectOnDataSourceChanged() As Boolean
            Get
                Return _AutoSelectOnDataSourceChanged
            End Get
            Set(ByVal value As Boolean)
                _AutoSelectOnDataSourceChanged = value
            End Set
        End Property

        'Overloaded this property to ensure that the SelectionChanged event has the good CurrentRow when using selection by moving the mouse with the left button down
        Public Overloads ReadOnly Property currentRow() As DataGridViewRow
            Get
                Return _currentRow
            End Get
        End Property

        'Overloaded this property to ensure that the SelectionChanged event has the good CurrentRow when using selection by moving the mouse with the left button down
        Public Overloads Property currentCell() As DataGridViewCell
            Get
                Return _currentCell
            End Get
            Set(ByVal value As DataGridViewCell)
                _currentCell = value
                MyBase.CurrentCell = _currentCell
            End Set
        End Property
#End Region

        Protected Overrides Sub onCellEnter(ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
            MyBase.OnCellEnter(e)


            _currentRow = Me.Rows(e.RowIndex)
            _currentCell = _currentRow.Cells(e.ColumnIndex)

            If Control.MouseButtons = System.Windows.Forms.MouseButtons.Left Then MyBase.OnSelectionChanged(EventArgs.Empty)
        End Sub

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
                'REM Had to remove this, because otherwise it keeps autoselecting the first row in viewmodifclients.ListEquipement
                'ASODSCisDone = True
            End If
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            If isPaintingSuspended Then Exit Sub

            MyBase.OnPaint(e)
        End Sub

        Private isPaintingSuspended As Boolean = False

        ''' <summary>
        ''' A stronger "SuspendLayout" completely holds the controls painting until ResumePaint is called
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SuspendPaint()
            SuspendDrawing(Me)
            'Me.SetStyle(ControlStyles.UserPaint, False)
            'isPaintingSuspended = True
            'Common.SuspendPaint(Me)
        End Sub

        ''' <summary>
        ''' Resume from SuspendPaint method
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ResumePaint()
            isPaintingSuspended = False
            'Common.ResumePaint(Me)
        End Sub

    End Class


End Namespace