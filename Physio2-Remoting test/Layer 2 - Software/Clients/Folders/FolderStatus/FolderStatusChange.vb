Namespace Accounts.Clients.Folders
    Partial Public Class FoldersStatus


        Public Enum FolderPossibleStatuses
            Active = 0
            Inactive = 1
        End Enum

        Public Class FolderStatusChange

            Public Sub New(ByVal old As FolderPossibleStatuses, ByVal [new] As FolderPossibleStatuses, ByVal noClient As Integer, ByVal noFolder As Integer, Optional ByVal comments As String = "", Optional ByVal statusDate As Date = LIMIT_DATE)
                _old = old
                _new = [new]
                _noClient = noClient
                _noFolder = noFolder
                _comments = comments
                _newStatusDate = statusDate
            End Sub

            Private _old As FolderPossibleStatuses = FolderPossibleStatuses.Active
            Private _new As FolderPossibleStatuses = FolderPossibleStatuses.Inactive
            Private _noClient, _noFolder As Integer
            Private _comments As String = String.Empty
            Private _newStatusDate As Date = LIMIT_DATE

#Region "Properties"
            Public ReadOnly Property old() As FolderPossibleStatuses
                Get
                    Return _old
                End Get
            End Property

            Public ReadOnly Property [new]() As FolderPossibleStatuses
                Get
                    Return _new
                End Get
            End Property

            Public ReadOnly Property noClient() As Integer
                Get
                    Return _noClient
                End Get
            End Property

            Public ReadOnly Property noFolder() As Integer
                Get
                    Return _noFolder
                End Get
            End Property

            Public ReadOnly Property comments() As String
                Get
                    Return _comments
                End Get
            End Property

            Public ReadOnly Property [date]() As Date
                Get
                    Return _newStatusDate
                End Get
            End Property
#End Region

        End Class


    End Class
End Namespace
