Public Class TestRunner

    Public Shared Sub RunAllTests()
        Dim rett As New RegExTestTest()
        Console.WriteLine("RegExTest.test(*) : " & rett.TestTest())
        Dim dvt As New DomainValidatorTest
        Console.WriteLine("DomainValidator.test(*) : " & dvt.TestTest())
    End Sub
End Class
