Public Class ToolStripButtonPlus
    Inherits ToolStripButton

    Public Sub New(ByVal text As String, ByVal image As Image, ByVal onClick As EventHandler)
        MyBase.New(text, image, onClick)
        Me.Font = New Font("Courier New", Me.Font.SizeInPoints, Me.Font.Style)
        If _TextShowedCharByCharVertically Then
            MyBase.ForeColor = MyBase.BackColor
            Me.AutoSize = False
        Else
            Me.AutoSize = True
        End If
    End Sub

    Private _TextShowedCharByCharVertically As Boolean = True

    Private _Text As String = ""

    Public Property textShowedCharByCharVertically() As Boolean
        Get
            Return _TextShowedCharByCharVertically
        End Get
        Set(ByVal value As Boolean)
            _TextShowedCharByCharVertically = value
            Me.AutoSize = Not value
            'If value Then
            '    MyBase.ForeColor = SystemColors.Control
            'Else
            '    MyBase.ForeColor = Color.Black
            'End If
        End Set
    End Property

    Public Overrides Property text() As String
        Get
            'REM OverFlow elements showed horizontally : If _TextShowedCharByCharVertically = False OrElse Me.IsOnOverflow = True Then Return _Text
            If _TextShowedCharByCharVertically = False Then Return _Text

            Return ""
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            _Text = value

            If _TextShowedCharByCharVertically Then
                'MeasureString(String.Join(vbCrLf, Array.ConvertAll(Of Char, String)(value.ToCharArray(), New Converter(Of Char, String)(AddressOf Convert.ToString))), Me.Font).Height - value.Length * 3
                Dim tempText As String = value.Replace("(", "")

                Me.Height = (value.Length - (value.Length - tempText.Length) * 2) * Me.Font.SizeInPoints + 13
            End If
        End Set
    End Property

    Protected Overrides Sub onLayout(ByVal e As System.Windows.Forms.LayoutEventArgs)
        If _Text = "Note - Test2" Then
            Dim a As Integer = 1
        End If

        MyBase.OnLayout(e)
        If _Text = "Note - Test2" Then
            Dim a As Integer = 1
        End If
    End Sub

    Protected Overrides Sub onPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        If _TextShowedCharByCharVertically Then 'REM OverFlow elements showed horizontally  : AndAlso Me.IsOnOverflow = False Then
            'If Me.Selected Then
            '    If Me.Pressed Then
            '        Me.ForeColor = ProfessionalColors.ButtonPressedHighlight
            '    Else
            '        Me.ForeColor = ProfessionalColors.ButtonSelectedHighlight
            '    End If
            'Else
            '    If CType(Me.Tag, Controllable).HasToBlink Then
            '        Me.ForeColor = ColorTranslator.FromOle(PreferencesManager.getGeneralPreferences()("ColorObjActivatedBySoftware"))
            '    Else
            '        Me.ForeColor = SystemColors.Control
            '    End If
            'End If
            Dim x, y As Integer
            x = Me.Width / 2 - 5
            y = Me.Margin.Top + 5
            Dim chars() As Char = _Text.ToCharArray
            For i As Integer = 0 To chars.GetUpperBound(0)
                Dim toPrint As String = chars(i)
                If toPrint = "(" Then toPrint &= chars(i + 1) & chars(i + 2) : i += 2
                e.Graphics.DrawString(toPrint, Me.Font, Brushes.Black, x - IIf(toPrint.Length = 1, 0, 7), y)
                y += Me.Font.SizeInPoints
            Next i
        End If
    End Sub
End Class
