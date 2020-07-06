Public Class DataInternalUpdate
    Inherits DataToConsume

    Private [_function] As String
    Private _params() As String
    Private _fromExternal As Boolean
    Private _noMessage As Integer

#Region "Propriétés"
    Public ReadOnly Property noMessage() As String
        Get
            Return _noMessage
        End Get
    End Property

    Public ReadOnly Property [function]() As String
        Get
            Return [_function]
        End Get
    End Property

    Public ReadOnly Property params() As String()
        Get
            Return _params
        End Get
    End Property

    Public ReadOnly Property fromExternal() As Boolean
        Get
            Return _fromExternal
        End Get
    End Property
#End Region

    Public Sub New(ByVal noMessage As Integer, ByVal update As String, ByVal fromExternal As Boolean)
        _noMessage = noMessage
        Dim myFunction() As String = update.Split(New Char() {"("})
        myFunction(1) = myFunction(1).Substring(0, myFunction(1).Length - 1) 'Remove ")" last char
        Dim params() As String = myFunction(1).Split(New Char() {","})

        Me.[_function] = myFunction(0)
        Me._params = params
        _fromExternal = fromExternal
    End Sub

    Protected Sub New(ByVal data As DataInternalUpdate)
        Me.[_function] = data.function
        Me._params = data.params
        _fromExternal = data.fromExternal
    End Sub

End Class
