Public Class DBCommandNonQuery
    Inherits DBCommand


    Private _nbAffectedRows As Integer = -1
    Private getScopeIdentity As Boolean = False
    Private _scopeIdentity As Integer = -1

    Public Sub New(Optional ByVal getScopeIdentity As Boolean = False)
        Me.getScopeIdentity = getScopeIdentity
    End Sub

    Public ReadOnly Property nbAffectedRows() As Integer
        Get
            Return _nbAffectedRows
        End Get
    End Property

    Public ReadOnly Property scopeIdentity() As Integer
        Get
            Return _scopeIdentity
        End Get
    End Property

    Public Overrides Sub execute(ByVal cmd As System.Data.SqlClient.SqlCommand)
        _nbAffectedRows = cmd.ExecuteNonQuery()

        If getScopeIdentity Then
            Dim noIdent As DataSet = DBLinker.getInstance().readDBForGrid("SELECT SCOPE_IDENTITY()", , , , , , cmd.Connection)
            If noIdent Is Nothing OrElse noIdent.Tables(0).Rows.Count = 0 Then
                _scopeIdentity = 0
            Else
                _scopeIdentity = noIdent.Tables(0).Rows(0)(0)
            End If
        End If
    End Sub
End Class
