Friend Class ClientSearch2

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        MyBase.setSearchResultsControl(myClientSearchResult)
        Me.Icon = DrawingManager.imageToIcon(DrawingManager.getInstance.getImage("searchClient16.gif"))
    End Sub

End Class