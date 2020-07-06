Public Class InternalType_Browser
    Implements IOpenable

    Private Shared OPENABLE_STARTWITHS() As String = {"http://", "www.", "https://"}

    Public Function open(ByVal uri As String, ByVal options As IOpenableOptions) As IOpenable.OpenableReturn Implements IOpenable.open
        Dim shallOpen As Boolean = False
        For i As Integer = 0 To OPENABLE_STARTWITHS.Length - 1
            If uri.StartsWith(OPENABLE_STARTWITHS(i)) Then
                shallOpen = True
                Exit For
            End If
        Next i
        If Not shallOpen Then Return IOpenable.OpenableReturn.NotOpenable

        Dim myBrowser As Browser = openUniqueWindow(New Browser())
        myBrowser.htmlPageUrl = uri
        myBrowser.showPage()
        myBrowser.Show()

        Return IOpenable.OpenableReturn.Opened
    End Function
End Class
