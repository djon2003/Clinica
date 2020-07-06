Public Interface IHTMLGenerator
    Event htmlGenerated(ByVal html As String, ByVal isHTMLGenerated As Boolean)

    Function generateHTML() As String
    Sub startHTMLGeneration()
    Sub stopHTMLGeneration()
End Interface
