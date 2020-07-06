Public Class RegExTestTest

    Public Sub New()
    End Sub

    Public Function TestTest() As Boolean
        Dim rgTest As New RegExTest()
        Dim testing As Boolean
        'Shall be true
        testing = rgTest.test("djon@cints.net")
        testing = testing AndAlso rgTest.test("djon22@cints.net")
        testing = testing AndAlso rgTest.test("22djon@cint22s.net")
        testing = testing AndAlso rgTest.test("gastonboivin@videotron.qc.ca")
        testing = testing AndAlso rgTest.test("gaston-.boivin@videotron.qc.ca")
        testing = testing AndAlso rgTest.test("djon@cints.net.net.net")

        'Shall be false
        testing = testing AndAlso Not rgTest.test("gaston-.!boivin@videontron.qc.ca")
        testing = testing AndAlso Not rgTest.test("gaston-. boivin@videontron.qc.ca")
        testing = testing AndAlso Not rgTest.test("gaston-.\boivin@videontron.qc.ca")
        testing = testing AndAlso Not rgTest.test("djon@cints.ne2t")
        testing = testing AndAlso Not rgTest.test("djo@n@cints.net")
        testing = testing AndAlso Not rgTest.test("dj?on@cints.net")

        Return testing
    End Function
End Class
