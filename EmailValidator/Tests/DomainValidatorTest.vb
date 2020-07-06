Public Class DomainValidatorTest

    Public Sub New()
    End Sub

    Public Function TestTest() As Boolean
        Dim rgTest As New DomainValidator()
        Dim testing As Boolean
        'Shall be true
        testing = rgTest.test("djon@cints.net")
        testing = testing AndAlso rgTest.test("gastonboivin@videotron.ca")
        testing = testing AndAlso rgTest.test("gastonboivin@hotmail.com")
        testing = testing AndAlso rgTest.test("gastonboivin@google.com")

        'Shall be false
        testing = testing AndAlso Not rgTest.test("22djon@cint22s.net")
        testing = testing AndAlso Not rgTest.test("gaston-.\boivin@videontron.qc.ca")
        testing = testing AndAlso Not rgTest.test("djon@cints.ne2t")

        Return testing
    End Function
End Class
