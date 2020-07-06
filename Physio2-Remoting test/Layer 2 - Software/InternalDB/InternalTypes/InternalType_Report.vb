Public Class InternalType_Report
    Implements IOpenable

    Public Function open(ByVal uri As String, ByVal options As IOpenableOptions) As IOpenable.OpenableReturn Implements IOpenable.open
        If uri.ToLower.EndsWith(".htmlrpt") = False Then Return IOpenable.OpenableReturn.NotOpenable

        TextWindow.getInstance.isHTML = True
        TextWindow.getInstance.currentData = IO.File.ReadAllText(uri, System.Text.Encoding.UTF7)
        TextWindow.getInstance.Text = "Visualisation : Rapport"
        TextWindow.getInstance.isLocked = True
        Dim mysel As String = TextWindow.getInstance.ShowTexteModif(0)

        Return IOpenable.OpenableReturn.Opened
    End Function
End Class
