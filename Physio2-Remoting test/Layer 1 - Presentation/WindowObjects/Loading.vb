Public Class Loading
    Inherits Base.Loading

    Protected Sub New()
        MyBase.New()

        Try
            Me.Icon = DrawingManager.getInstance().getIcon("Clinica.ico")
        Catch ex As Exception
            'Ensure designer is functionnal
        End Try
    End Sub

    Public Overloads Shared Function getInstance() As Loading
        If Not isLoaded OrElse Not TypeOf Base.Loading.getInstance() Is Clinica.Loading Then Base.Loading.setInstance(New Clinica.Loading())

        Return Base.Loading.getInstance()
    End Function
End Class
