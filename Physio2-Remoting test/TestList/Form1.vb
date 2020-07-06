Public Class Form1

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nb As Integer = InputBox("Veuillez entrer le # d'items à ajouter", "Nb items", "50")
        Dim text As String = InputBox("Veuillez entrer le nom du début de l'item", "Titre item", "Testing enough long to test scrollbar horizontal")
        Dim i As Integer = 0

        For i = 1 To nb
            ListBox1.Add(text & i)
        Next i

        ListBox1.Draw = True
        ListBox1.Draw = False
    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox1.Cls()
    End Sub

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ListBox1.ForceRedraw()
        ListBox1.ShowItem(ListBox1.Selected, CyberInternautes.Controls.List.PosType.Middle)
    End Sub

    Private Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PropertyGrid1.CollapseAllGridItems()
    End Sub

    Private Sub button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim filePath As String = InputBox("Enter path to save", "Path", "C:\list.obj")
        If filePath = "" Then Exit Sub

        ListBox1.SaveTo(filePath)
    End Sub

    Private Sub button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim filePath As String = InputBox("Enter path to load", "Path", "C:\list.obj")
        If filePath = "" Then Exit Sub

        ListBox1.ReadFrom(filePath)
    End Sub

    Private Sub button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ListBox1.Remove(ListBox1.Selected)
    End Sub

    Private Sub button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim nbToTie As Integer = InputBox("Nombre à grouper ensemble (selected + followers)", "NbGrouped", "3")
        ListBox1.TieItem(nbToTie, ListBox1.Selected)
    End Sub

    Private Sub button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        ListBox1.Cls()
        Dim firstChar As String = ""
        Dim secondChar As String = ""
        Dim thirdChar As String = ""
        Dim fourthChar As String = ""

        For i As Integer = 1 To 50
            If FirstChar = "" OrElse i Mod 10 = 0 Then FirstChar = GenChar()
            If SecondChar = "" OrElse i Mod 5 = 0 Then SecondChar = GenChar()
            If ThirdChar = "" OrElse i Mod 3 = 0 Then ThirdChar = GenChar()
            If FourthChar = "" OrElse i Mod 2 = 0 Then FourthChar = GenChar()

            ListBox1.Add(FirstChar & SecondChar & ThirdChar & FourthChar & GenChar() & GenChar() & GenChar())
        Next i

        ListBox1.ForceRedraw()
    End Sub


    Dim rnd As New Random()

    Private Function genChar() As String
        Dim alpha() As String = New String() {"a", "b", "c", "d", "e", "d", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v"}

        Return alpha(rnd.Next(0, alpha.GetUpperBound(0)))
    End Function
End Class
