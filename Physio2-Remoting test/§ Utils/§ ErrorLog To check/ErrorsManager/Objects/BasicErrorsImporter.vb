Public MustInherit Class BasicErrorsImporter
    Implements IErrorImporter

    Public MustOverride Sub import(ByVal filename As String) Implements IErrorImporter.import
    Public MustOverride Sub import(ByVal folder As String, ByVal recursive As Boolean) Implements IErrorImporter.import

    Protected Sub importOneError(ByVal errorContent As String)
        If errorContent.Trim = "" Then Exit Sub

        'Dim lines() As String = errorContent.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        'Dim newError As New Erreur(defineSoftwareSource(lines))
        'newError.date = extractHeaderLine(lines(0))
        'newError.ClinicaVersion = extractHeaderLine(lines(1))
        'newError.computer = extractHeaderLine(lines(2))
        'newError.winUser = extractHeaderLine(lines(3))
        'newError.ClinicaUser = extractHeaderLine(lines(4))
        'newError.message = lines(6)
        'newError.source = lines(8)
        'newError.content = errorContent.Substring(errorContent.IndexOf("Exception Stack Trace"))

        Dim newError As New Erreur(errorContent)

        ErrorsManager.getInstance.addError(newError)
    End Sub
End Class
