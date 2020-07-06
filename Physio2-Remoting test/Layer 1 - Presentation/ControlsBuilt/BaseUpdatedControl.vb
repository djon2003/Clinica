Public Class BaseUpdatedControl
    Inherits UserControl
    Implements IDataConsumer(Of DataInternalUpdate)

    Delegate Sub updateCallback(ByVal update As DataInternalUpdate)

    Public Sub New()
        InternalUpdatesManager.getInstance.addConsumer(Me)
    End Sub

    '    Public MustOverride Sub dataConsuming(ByVal dataReceived As DataInternalUpdate)
    Public Overridable Sub dataConsuming(ByVal dataReceived As DataInternalUpdate)

    End Sub


    Private Sub dataConsumingCall(ByVal dataReceived As DataInternalUpdate)
        Me.dataConsuming(dataReceived)
    End Sub

    Public Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
        If Me.InvokeRequired Then
            Dim d As New updateCallback(AddressOf dataConsuming)
            Me.BeginInvoke(d, New Object() {dataReceived})
        Else
            dataConsuming(dataReceived)
        End If
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return -1
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function
End Class
