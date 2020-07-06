Namespace Windows.Forms


    Public Class ConfigurationsPage


        Public Sub New()

            ' Cet appel est requis par le Concepteur Windows Form.
            InitializeComponent()

            ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

        End Sub

        Public Sub load(ByVal config As ConfigBase)
            Me.Text = config.name
            Me.PropertyGrid1.SelectedObject = config
        End Sub
    End Class


End Namespace
