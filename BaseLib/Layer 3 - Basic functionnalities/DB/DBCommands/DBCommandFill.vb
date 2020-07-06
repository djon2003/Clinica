Public Class DBCommandFill
    Inherits DBCommand

    Private da As SqlClient.SqlDataAdapter
    Private ds As DataSet
    Private tableMappingName As String
    Private sqlType As System.Data.CommandType
    Private params() As String
    Private spParameters As Hashtable

    Public Sub New(ByRef da As SqlClient.SqlDataAdapter, ByRef ds As DataSet, ByVal tableMappingName As String, ByVal sqlType As System.Data.CommandType, ByVal params() As String, ByRef spParameters As Hashtable)
        If da Is Nothing Then da = New SqlClient.SqlDataAdapter
        If ds Is Nothing Then ds = New DataSet()

        Me.da = da
        Me.ds = ds
        Me.tableMappingName = tableMappingName
        Me.sqlType = sqlType
        Me.params = params
        Me.spParameters = spParameters
    End Sub

    Public Overrides Sub execute(ByVal cmd As System.Data.SqlClient.SqlCommand)
        cmd.CommandType = sqlType

        If sqlType = CommandType.StoredProcedure AndAlso params IsNot Nothing AndAlso params.Length <> 0 Then
            If spParameters.Contains(cmd.CommandText) Then
                cmd.Parameters.AddRange(spParameters(cmd.CommandText))
            Else
                SqlClient.SqlCommandBuilder.DeriveParameters(cmd)
                Dim cmdParams() As SqlClient.SqlParameter
                ReDim cmdParams(cmd.Parameters.Count - 1)
                cmd.Parameters.CopyTo(cmdParams, 0)
                spParameters.Add(cmd.CommandText, cmdParams)
            End If
            For i As Integer = 0 To Math.Min(params.Length, cmd.Parameters.Count) - 1
                cmd.Parameters.Item(i + 1).Value = params(i)
            Next i
        End If

        da.SelectCommand = cmd
        da.Fill(ds, tableMappingName)
    End Sub
End Class
