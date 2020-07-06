Public Class ErrorInput

    Private _out As FileResponseOutput
    Private _in As FileResponseLine
    Private _previousResults As List(Of FileResult)
    Private _fileResponse As FileResponse
    
    Public shallAddResult As Boolean = True

    Public Sub New(ByVal [in] As FileResponseLine, ByVal out As FileResponseOutput, ByVal previousResults As List(Of FileResult), ByVal fileResponse As FileResponse)
        _in = [in]
        _out = out
        _previousResults = previousResults
        _fileResponse = fileResponse
    End Sub

#Region "Properties"
    Public ReadOnly Property out() As FileResponseOutput
        Get
            Return _out
        End Get
    End Property

    Public ReadOnly Property fileResponse() As FileResponse
        Get
            Return _fileResponse
        End Get
    End Property

    Public ReadOnly Property [in]() As FileResponseLine
        Get
            Return _in
        End Get
    End Property

    Public ReadOnly Property previousResults() As List(Of FileResult)
        Get
            Return _previousResults
        End Get
    End Property


#End Region

End Class
