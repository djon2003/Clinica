Public Class SingleWindowSaveEventArgs
    Inherits EventArgs

    Private _cancel As Boolean = False
    Private _cancelReason As String = ""

    Public Sub New()
        MyBase.New()
    End Sub

#Region "Properties"
    Public ReadOnly Property isCancelled() As Boolean
        Get
            Return _cancel
        End Get
    End Property

    Public ReadOnly Property cancelReason() As String
        Get
            Return _cancelReason
        End Get
    End Property

#End Region

    Public Sub cancel(ByVal reason As String)
        If reason Is Nothing OrElse reason = "" Then Throw New InvalidOperationException("Reason can not be nothing or an empty string")

        _cancel = True
        _cancelReason = reason
    End Sub

    Public Sub resetCancel()
        _cancel = False
        _cancelReason = ""
    End Sub

End Class
