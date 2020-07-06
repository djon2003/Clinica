Public Class Form1

    Private Sub addLines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addLines.Click
        Dim started As Date = Date.Now

        List1.draw = False
        Dim colors As Color() = New Color() {Color.Yellow, Color.White, Color.Gray, Color.Red}
        Dim currentLineNumber As Integer = List1.listCount() + 1
        Dim boldLines As New Font(List1.baseFont, FontStyle.Bold)
        Dim c As Integer = 0
        For i As Integer = 1 To nbLinesToAdd.Value
            Dim n As Integer = List1.add("Item #" & currentLineNumber)
            List1.ItemBackColor(n) = colors(c)
            If currentLineNumber Mod 2 = 0 Then List1.ItemFont(n) = boldLines

            c += 1
            c = c Mod colors.Length
            currentLineNumber += 1
        Next i

        List1.draw = True

        actionMessage.Text = "Added " & nbLinesToAdd.Value & " lines in " & (Date.Now.Subtract(started).TotalMilliseconds) & "ms"
    End Sub
End Class
