Public Class EmailValidator
    Implements Testable(Of String)

    Private from As String = ""
    Private isInternetAvailable As Boolean
    Public Enum LevelReached
        None = 0
        RegEx = 1
        Domain = 2
        MX = 3
    End Enum
    Private _lastLevelReached As LevelReached

    Public ReadOnly Property lastLevelReached() As LevelReached
        Get
            Return _lastLevelReached
        End Get
    End Property

    Public Sub New(ByVal from As String, ByVal isInternetAvailable As Boolean)
        Me.from = [from]
        Me.isInternetAvailable = isInternetAvailable
    End Sub

    Public Function test(ByVal objToTest As String) As Boolean Implements Testable(Of String).test
        Dim regEx As New RegExTest()
        Dim domain As New DomainValidator()
        Dim mx As New MXVerificator(from)

        Dim domainTest As Boolean = True
        Dim mxTest As Boolean = True
        Dim regExTest As Boolean = regEx.test(objToTest)
        _lastLevelReached = LevelReached.RegEx
        If regExTest AndAlso isInternetAvailable Then
            domainTest = domain.test(objToTest)
            mx.nsResult = domain.lastResult
            _lastLevelReached = LevelReached.Domain
            mxTest = domainTest AndAlso mx.test(objToTest)
            _lastLevelReached = IIf(domainTest = False, _lastLevelReached, LevelReached.MX)
        End If

        Return regExTest AndAlso domainTest AndAlso mxTest
    End Function

    Public Sub testFile(ByVal filePath As String)
        If IO.File.Exists(filePath) = False Then
            Console.WriteLine("Le fichier (" & filePath & ") n'existe pas")
            Exit Sub
        End If

        Dim fileContent() As String = IO.File.ReadAllLines(filePath)
        Dim errors As New Text.StringBuilder()
        For i As Integer = 0 To fileContent.Length - 1
            Dim valid As String = "0"
            Try
                If Me.test(fileContent(i)) Then valid = "1"
                fileContent(i) = CInt(_lastLevelReached) & ":" & valid & ":" & fileContent(i)
                Console.WriteLine(fileContent(i))
            Catch ex As Exception
                fileContent(i) = CInt(_lastLevelReached) & ":1:" & fileContent(i)
                errors.AppendLine(fileContent(i))
                errors.AppendLine(ex.StackTrace)
                errors.AppendLine("---------------------------------------------------------------------------------")
            End Try

            Console.WriteLine("UPTO : " & (i + 1) & "/" & fileContent.Length)
        Next i

        Dim strErrors As String = errors.ToString
        If strErrors <> "" Then IO.File.AppendAllText(My.Application.Info.DirectoryPath & IIf(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & "emailvalidator.errors", strErrors)
        IO.File.WriteAllText(filePath, String.Join(vbCrLf, fileContent))
    End Sub
End Class
