Public Class FormTestDateChoice

    Private WithEvents dc As DateChoice

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        dc = PropertyGrid1.SelectedObject
        dc.MdiParent = Nothing
        dc.Visible = False
        dc.Enabled = True
        Dim chosen As Generic.List(Of Date) = dc.choose(showTime.Checked)
        dc = New DateChoice
        PropertyGrid1.SelectedObject = dc
        dc.MdiParent = Me
        dc.Enabled = False
        dc.Location = New Point(Me.Width - dc.Width - 30, 0)
        dc.Visible = True
        dc.StartPosition = FormStartPosition.Manual
        results.Text = ""
        For Each curDate As Date In chosen
            results.Text &= DateFormat.affTextDate(curDate) & " " & DateFormat.affTextDate(curDate, DateFormat.TextDateOptions.ShortTime) & vbCrLf
        Next
    End Sub

    Public Sub New()
        ' Cet appel est requis par le Concepteur Windows Form.
        initializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

    Private Sub formTestDateChoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dc = New DateChoice()
        dc.MdiParent = Me
        dc.Enabled = False
        dc.Location = New Point(Me.Width - dc.Width - 30, 0)
        dc.StartPosition = FormStartPosition.Manual
        dc.Show()
        PropertyGrid1.SelectedObject = dc
    End Sub

    Private Sub dc_DrawingDay(ByVal selectedDate As Date, ByRef day As DateChoiceDay) Handles dc.drawingDay
        'day.backColor = Color.Blue
        'day.foreColor = Color.White
        'If selectedDate.Equals(Date.Today) Then
        '    day.isSpecial = True
        '    day.toolTip = "Party de Noël"
        'End If
        'If selectedDate.Equals(Date.Today) Then
        '    day.startTime = New Date(selectedDate.Year, selectedDate.Month, selectedDate.Day, 9, 15, 0)
        '    day.endTime = New Date(selectedDate.Year, selectedDate.Month, selectedDate.Day, 21, 1, 0)
        '    day.minutesInterval = 15
        'Else
        '    day.startTime = New Date(selectedDate.Year, selectedDate.Month, selectedDate.Day, 10, 7, 0)
        '    day.endTime = New Date(selectedDate.Year, selectedDate.Month, selectedDate.Day, 18, 1, 0)
        '    day.minutesInterval = 15
        'End If
    End Sub

    Private Sub dc_ValidatingDaySelection(ByVal selectedDate As Date, ByRef isValid As Boolean) Handles dc.validatingDaySelection
        'WORKS FINE ! isValid = selectedDate.Year >= Date.Now.Year AndAlso selectedDate.Month <> 10 AndAlso selectedDate.Day <> 12
    End Sub
End Class
