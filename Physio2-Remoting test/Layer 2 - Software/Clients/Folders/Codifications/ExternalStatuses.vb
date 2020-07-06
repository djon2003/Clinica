Namespace Accounts.Clients.Folders.Codifications


    Public Class ExternalStatuses
        Inherits ManagerBase(Of ExternalStatuses)

        Private _statuses As New Generic.List(Of ExternalStatus)

        Protected Sub New()
            Dim statuses As DataSet = DBLinker.getInstance.readDBForGrid("ExternalStatuses", "*")
            For Each curRow As DataRow In statuses.Tables(0).Rows
                Me._statuses.Add(New ExternalStatus(curRow))
            Next
        End Sub

        Public ReadOnly Property statuses() As Generic.List(Of ExternalStatus)
            Get
                Return New Generic.List(Of ExternalStatus)(Me._statuses.ToArray)
            End Get
        End Property

        Public Function getStatus(ByVal noExternalStatus As Integer) As ExternalStatus
            For Each curStatus As ExternalStatus In _statuses
                If curStatus.noExternalStatus = noExternalStatus Then Return curStatus
            Next

            Return Nothing
        End Function
    End Class


End Namespace