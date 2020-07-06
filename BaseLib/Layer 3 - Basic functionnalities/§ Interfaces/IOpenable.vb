Public Interface IOpenable
    Enum OpenableReturn
        NotOpenable = 1
        Opened = 2
        Cancelled = 3
        [ErrorManaged] = 4
    End Enum
    Function open(ByVal uri As String, ByVal options As IOpenableOptions) As OpenableReturn
End Interface


