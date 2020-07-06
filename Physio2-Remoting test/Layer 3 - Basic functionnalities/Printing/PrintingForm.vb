Public Class PrintingForm

    Private Shared mySelf As PrintingForm

    Private Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

    Public Shared Function getInstance() As PrintingForm
        If mySelf Is Nothing Then mySelf = New PrintingForm

        Return mySelf
    End Function

    Public Sub setHtml(ByVal html As String)
        WebControl1.waitForDoc()
        WebControl1.setHtml(html)
    End Sub

    Public Sub print(ByVal promptUser As Boolean, ByVal waitForSpooling As Boolean)
        WebControl1.waitForDoc()
        WebControl1.print(promptUser, waitForSpooling)
    End Sub
End Class