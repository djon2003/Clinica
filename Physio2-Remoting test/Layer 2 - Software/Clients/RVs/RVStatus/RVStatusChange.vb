Imports CI.Clinica.Accounts.Clients.Folders.RVs

Namespace Accounts.Clients.Folders
    Partial Public Class RVsStatus


        Public Class RVStatusChange

            Public Sub New(ByVal old As RVPossibleStatuses, ByVal [new] As RVPossibleStatuses, ByVal rv As RendezVous)
                _old = old
                _new = [new]
                _rv = rv
            End Sub

            Private _old As RVPossibleStatuses = RVPossibleStatuses.Normal
            Private _new As RVPossibleStatuses = RVPossibleStatuses.Normal
            Private _rv As RendezVous
            Private _showPayment As Boolean = False
            Private _hasToEnsureFTDates As Boolean = False
            Private _bill As Bill
            Private _extraInfos As RVStatusChangeExtraInfos

#Region "Properties"
            Public Property hasToEnsureFTDates() As Boolean
                Get
                    Return _hasToEnsureFTDates
                End Get
                Set(ByVal value As Boolean)
                    _hasToEnsureFTDates = value
                End Set
            End Property

            Public Property extraInfos() As RVStatusChangeExtraInfos
                Get
                    Return _extraInfos
                End Get
                Set(ByVal value As RVStatusChangeExtraInfos)
                    _extraInfos = value
                End Set
            End Property

            Public Property bill() As Bill
                Get
                    Return _bill
                End Get
                Set(ByVal value As Bill)
                    _bill = value
                End Set
            End Property

            Public Property showPayment() As Boolean
                Get
                    Return _showPayment
                End Get
                Set(ByVal value As Boolean)
                    _showPayment = value
                End Set
            End Property

            Public ReadOnly Property old() As RVPossibleStatuses
                Get
                    Return _old
                End Get
            End Property

            Public ReadOnly Property [new]() As RVPossibleStatuses
                Get
                    Return _new
                End Get
            End Property

            Public ReadOnly Property rv() As RendezVous
                Get
                    Return _rv
                End Get
            End Property
#End Region

        End Class


    End Class
End Namespace
