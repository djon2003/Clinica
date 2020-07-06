Public Class TimeGrid

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        dayTimeLabels = New Label() {hoursDimanche, hoursLundi, hoursMardi, hoursMercredi, hoursJeudi, hoursVendredi, hoursSamedi}

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        dsTimeGrid.Columns.Add(New DataColumn("colTime"))
        dsTimeGrid.Columns.Add(New DataColumn("colDimanche"))
        dsTimeGrid.Columns.Add(New DataColumn("colLundi"))
        dsTimeGrid.Columns.Add(New DataColumn("colMardi"))
        dsTimeGrid.Columns.Add(New DataColumn("colMercredi"))
        dsTimeGrid.Columns.Add(New DataColumn("colJeudi"))
        dsTimeGrid.Columns.Add(New DataColumn("colVendredi"))
        dsTimeGrid.Columns.Add(New DataColumn("colSamedi"))

        buildTimeRows()

        grdTimeGrid.DataSource = dsTimeGrid
        addTooltipOnCells()
        grdTimeGrid.Columns(0).HeaderCell.Style.BackColor = _scheduleOff

        horaireModifCursor = DrawingManager.getInstance.getCursor("PENCIL.CUR")
    End Sub

    Private Sub addTooltipOnCells()
        For Each row As DataGridViewRow In grdTimeGrid.Rows
            For Each cell As DataGridViewCell In row.Cells
                cell.ToolTipText = "Double clique débute la modification  |  Simple clique arrête la modification"
            Next
        Next
    End Sub


    Private dsTimeGrid As New DataTable("TimeGrid")
    Private curInterval As Integer = 15
    Private _scheduleBlocked As Color = Color.Black
    Private _scheduleOn As Color = Color.Blue
    Private _scheduleOff As Color = Color.LightGray
    Private curTimeColor As Color = Color.LightGray
    Private _isModifying As Boolean = False
    Private _isDrawingScheduleOn As Boolean = False
    Private lastRowIndex As Integer = 0
    Private lastColIndex As Integer = 0
    Private _blockingSchedule As Schedule
    Private _schedule As Schedule
    Private _startingTime As Date = New Date(2000, 1, 1, 6, 0, 0)
    Private dimanche, lundi, mardi, mercredi, jeudi, vendredi, samedi As Integer
    Private horaireModifCursor As Cursor

    Public Event totalChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event timeChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event isDrawingScheduleOnOnChanged(ByVal sender As Object, ByVal e As EventArgs)

#Region "Propriétés"
    Public ReadOnly Property isModifying() As Boolean
        Get
            Return _isModifying
        End Get
    End Property

    Public ReadOnly Property blockingSchedule() As Schedule
        Get
            Return _blockingSchedule
        End Get
    End Property

    Public ReadOnly Property schedule() As Schedule
        Get
            Return _schedule
        End Get
    End Property

    Public Property isDrawingScheduleOn() As Boolean
        Get
            Return _isDrawingScheduleOn
        End Get
        Set(ByVal value As Boolean)
            curTimeColor = IIf(value, _scheduleOn, _scheduleOff)
            grdTimeGrid.Columns(0).HeaderCell.Style.BackColor = curTimeColor
            If _isDrawingScheduleOn <> value Then RaiseEvent isDrawingScheduleOnOnChanged(Me, EventArgs.Empty)
            _isDrawingScheduleOn = value
        End Set
    End Property

    Public Property scheduleBlocked() As Color
        Get
            Return _scheduleBlocked
        End Get
        Set(ByVal value As Color)
            _scheduleBlocked = value
        End Set
    End Property

    Public Property scheduleOn() As Color
        Get
            Return _scheduleOn
        End Get
        Set(ByVal value As Color)
            _scheduleOn = value
            isDrawingScheduleOn = isDrawingScheduleOn 'To refresh color of current selection
        End Set
    End Property

    Public Property scheduleOff() As Color
        Get
            Return _scheduleOff
        End Get
        Set(ByVal value As Color)
            If value <> _scheduleOff Then
                _scheduleOff = value
                resetGrid()
                isDrawingScheduleOn = isDrawingScheduleOn 'To refresh color of current selection
            End If
        End Set
    End Property

    Public Property startingTime() As Date
        Get
            Return _startingTime
        End Get
        Set(ByVal value As Date)
            _startingTime = value
        End Set
    End Property
#End Region

#Region "Internal functions to manage drawing of cells"
    Protected Overrides Sub onClientSizeChanged(ByVal e As System.EventArgs)
        MyBase.OnClientSizeChanged(e)

        '  If Me.Width <> 624 Then Me.Width = 624
    End Sub

    Private Sub buildTimeRows()
        dsTimeGrid.Rows.Clear()

        Dim curTime As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, startingTime.Hour, startingTime.Minute, 0)
        Dim tomorrow As Date = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day).AddDays(1)
        While date1Infdate2(curTime, tomorrow)
            dsTimeGrid.Rows.Add(New Object() {Clinica.DateFormat.getTextDate(curTime, DateFormat.TextDateOptions.ShortTime), "", "", "", "", "", "", ""})
            curTime = curTime.AddMinutes(curInterval)
        End While
    End Sub

    Private Sub resetGrid()
        For i As Integer = 0 To grdTimeGrid.Rows.Count - 1
            For j As Integer = 1 To grdTimeGrid.Columns.Count - 1
                grdTimeGrid.Rows(i).Cells(j).Style.BackColor = _scheduleOff
            Next j
        Next i
    End Sub

    Private Sub grdTimeGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTimeGrid.CellClick
        _isModifying = False
        grdTimeGrid.Cursor = Cursors.Default
    End Sub

    Private Sub grdTimeGrid_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTimeGrid.CellMouseDoubleClick
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            _isModifying = Not _isModifying
            grdTimeGrid.Cursor = IIf(_isModifying, horaireModifCursor, Cursors.Default)
        Else
            isDrawingScheduleOn = Not _isDrawingScheduleOn
        End If

        If _isModifying AndAlso e.ColumnIndex > 0 AndAlso e.RowIndex >= 0 AndAlso grdTimeGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor.Equals(_scheduleBlocked) = False AndAlso grdTimeGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor.Equals(curTimeColor) = False Then
            grdTimeGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = curTimeColor
            calculateTotals()
            RaiseEvent timeChanged(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub grdTimeGrid_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTimeGrid.CellMouseEnter
        If e.ColumnIndex = lastColIndex AndAlso e.RowIndex = lastRowIndex Then Exit Sub

        For i As Integer = 1 To grdTimeGrid.ColumnCount - 1
            grdTimeGrid.Columns(i).HeaderCell.Style.BackColor = Color.White
        Next

        If lastRowIndex <> -1 AndAlso grdTimeGrid.Rows.Count > lastRowIndex Then grdTimeGrid.Rows(lastRowIndex).Cells(0).Style.BackColor = Color.White

        If e.ColumnIndex > 0 Then grdTimeGrid.Columns(e.ColumnIndex).HeaderCell.Style.BackColor = Color.Red
        If e.RowIndex <> -1 Then grdTimeGrid.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.Red

        If _isModifying AndAlso e.ColumnIndex > 0 AndAlso e.RowIndex >= 0 AndAlso grdTimeGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor.Equals(_scheduleBlocked) = False AndAlso grdTimeGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor.Equals(curTimeColor) = False Then
            grdTimeGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = curTimeColor
            calculateTotals()
            RaiseEvent timeChanged(Me, EventArgs.Empty)
        End If

        lastRowIndex = e.RowIndex
        lastColIndex = e.ColumnIndex
    End Sub

    Private Sub grdTimeGrid_CellMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTimeGrid.CellMouseMove
        
    End Sub

    Private Sub grdTimeGrid_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles grdTimeGrid.DataBindingComplete
        If schedule Is Nothing Then resetGrid()
    End Sub

    Private Sub grdTimeGrid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTimeGrid.SelectionChanged
        For Each curcell As DataGridViewCell In grdTimeGrid.SelectedCells
            curcell.Selected = False
        Next
    End Sub

    Private Sub calculateTotals()
        dimanche = 0
        lundi = 0
        mardi = 0
        mercredi = 0
        jeudi = 0
        vendredi = 0
        samedi = 0

        With grdTimeGrid.Rows
            For i As Integer = 0 To .Count - 1
                If .Item(i).Cells(1).Style.BackColor.Equals(_scheduleOn) Then dimanche += 1
                If .Item(i).Cells(2).Style.BackColor.Equals(_scheduleOn) Then lundi += 1
                If .Item(i).Cells(3).Style.BackColor.Equals(_scheduleOn) Then mardi += 1
                If .Item(i).Cells(4).Style.BackColor.Equals(_scheduleOn) Then mercredi += 1
                If .Item(i).Cells(5).Style.BackColor.Equals(_scheduleOn) Then jeudi += 1
                If .Item(i).Cells(6).Style.BackColor.Equals(_scheduleOn) Then vendredi += 1
                If .Item(i).Cells(7).Style.BackColor.Equals(_scheduleOn) Then samedi += 1
            Next i
        End With

        dimanche *= curInterval
        lundi *= curInterval
        mardi *= curInterval
        mercredi *= curInterval
        jeudi *= curInterval
        vendredi *= curInterval
        samedi *= curInterval

        hoursDimanche.Text = Math.Round(dimanche / 60, 2) & " h"
        hoursLundi.Text = Math.Round(lundi / 60, 2) & " h"
        hoursMardi.Text = Math.Round(mardi / 60, 2) & " h"
        hoursMercredi.Text = Math.Round(mercredi / 60, 2) & " h"
        hoursJeudi.Text = Math.Round(jeudi / 60, 2) & " h"
        hoursVendredi.Text = Math.Round(vendredi / 60, 2) & " h"
        hoursSamedi.Text = Math.Round(samedi / 60, 2) & " h"

        RaiseEvent totalChanged(Me, EventArgs.Empty)
    End Sub
#End Region

    Public Function getNbMinutes(ByVal day As Integer) As Integer
        Select Case day
            Case 0 'Dimanche
                Return dimanche
            Case 1 'Lundi
                Return lundi
            Case 2 'Mardi
                Return mardi
            Case 3 'Mercredi
                Return mercredi
            Case 4 'Jeudi
                Return jeudi
            Case 5 'Vendredi
                Return vendredi
            Case 6 'Samedi
                Return samedi
            Case Else
                Return dimanche + lundi + mardi + mercredi + jeudi + vendredi + samedi
        End Select
    End Function

    Public Sub loadSchedule(ByVal schedule As Schedule, Optional ByVal blockingSchedule As Schedule = Nothing)
        _schedule = schedule
        _blockingSchedule = blockingSchedule

        curInterval = schedule.intervalMinutes
        buildTimeRows()
        resetGrid()
        addTooltipOnCells()

        If _blockingSchedule IsNot Nothing Then loadGraph(_blockingSchedule)
        loadGraph(_schedule)

        calculateTotals()
    End Sub

    Public Sub saveScheduleTo(ByVal schedule As Schedule)
        If schedule Is Nothing Then schedule = _schedule
        saveGraph(schedule)
    End Sub

    Private Sub loadGraph(ByVal schedule As Schedule)
        Dim startingDate As Date = schedule.scheduleDate
        If startingDate.Equals(LIMIT_DATE) Then startingDate = Date.Today.AddDays(Date.Today.DayOfWeek * -1)
        For d As Byte = 1 To 7
            startingDate = startingDate.AddHours(startingTime.Hour).AddMinutes(startingTime.Minute)
            For i As Integer = 0 To grdTimeGrid.Rows.Count - 1
                If schedule Is _blockingSchedule Then
                    grdTimeGrid.Rows(i).Cells(d).Style.BackColor = IIf(schedule.isOpened(startingDate), Me.scheduleOff, Me.scheduleBlocked)
                ElseIf grdTimeGrid.Rows(i).Cells(d).Style.BackColor.Equals(_scheduleBlocked) = False Then
                    grdTimeGrid.Rows(i).Cells(d).Style.BackColor = IIf(schedule.isOpened(startingDate), Me.scheduleOn, Me.scheduleOff)
                End If
                startingDate = startingDate.AddMinutes(schedule.intervalMinutes)
            Next i
        Next d
    End Sub


    Private Sub saveGraph(ByVal schedule As Schedule)
        Dim startingDate As Date = schedule.scheduleDate
        If startingDate.Equals(LIMIT_DATE) Then startingDate = Date.Today.AddDays(Date.Today.DayOfWeek * -1)
        Dim opened As Boolean = False
        For d As Byte = 1 To 7
            startingDate = startingDate.AddHours(startingTime.Hour).AddMinutes(startingTime.Minute)
            For i As Integer = 0 To grdTimeGrid.Rows.Count - 1
                opened = grdTimeGrid.Rows(i).Cells(d).Style.BackColor = _scheduleOn
                schedule.setOpened(startingDate, opened)
                If schedule.isOpened(startingDate) <> opened Then
                    Dim a As Byte = 0
                End If
                startingDate = startingDate.AddMinutes(schedule.intervalMinutes)
                If startingDate.Hour = 12 Then
                    Dim a As Byte = 0
                End If
            Next i
        Next d
    End Sub

    Private Sub timeGrid_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        For i As Integer = 1 To grdTimeGrid.ColumnCount - 1
            grdTimeGrid.Columns(i).HeaderCell.Style.BackColor = Color.White
        Next

        If lastRowIndex <> -1 Then grdTimeGrid.Rows(lastRowIndex).Cells(0).Style.BackColor = Color.White
        lastRowIndex = -1
    End Sub

    Private Const scroll_bar_width As Integer = 20
    Private dayTimeLabels() As Label

    Private Sub timeGrid_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With grdTimeGrid
            .SuspendLayout()

            Dim skipFirst As Boolean = True
            Dim columnWidth As Integer = (.Width - .Columns(0).Width - scroll_bar_width) / 7
            Dim curTotalWidth As Integer = .Columns(0).Width
            Dim maxWidth As Integer = .Width - scroll_bar_width
            Dim l As Integer = 0
            For Each curCol As DataGridViewColumn In .Columns
                If skipFirst Then
                    skipFirst = False
                    Continue For
                End If

                curCol.Width = columnWidth
                curTotalWidth += columnWidth
                If curTotalWidth > maxWidth Then
                    curCol.Width -= curTotalWidth - maxWidth
                    curTotalWidth = maxWidth
                End If

                If dayTimeLabels IsNot Nothing Then dayTimeLabels(l).Left = curTotalWidth - dayTimeLabels(l).Width
                l += 1
            Next

            .ResumeLayout()
        End With
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
