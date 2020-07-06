Public Interface IErrorImporter

    Sub import(ByVal filename As String)
    Sub import(ByVal folder As String, ByVal recursive As Boolean)
End Interface
