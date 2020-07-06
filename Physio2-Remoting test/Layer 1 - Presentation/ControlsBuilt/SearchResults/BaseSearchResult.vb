Public Class BaseSearchResult

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
    End Sub

    Protected Overridable Sub loadImages()

    End Sub

    Protected Overridable Sub doClosingActions()

    End Sub

    Private Sub BaseSearchResult_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadImages()
    End Sub
End Class
