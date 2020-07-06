Public Class NoInternetException
    Inherits ExceptionBase

    Public Sub New()
        MyBase.New("No internet connection available")
    End Sub

End Class
