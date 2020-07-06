Public Interface IEmailsValidable

    ReadOnly Property type() As String

    Function validateEmails(Optional ByVal where As String = "", Optional ByVal reportOnly As Boolean = False) As DataTable

End Interface
