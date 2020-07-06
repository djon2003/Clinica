Public Class FilteringCreator
    Public Function createFiltering(ByVal name As String) As FilteringElement
        Select Case name.ToUpper
            Case "FROMTO"
                Return New FilteringFromTo()
            Case "NOCLIENT"
                Return New FilteringNoClient
            Case "NOKP"
                Return New FilteringNoKP
            Case "USER"
                Return New FilteringUser
            Case "LISTFIELDDATA"
                Return New FilteringListFieldData
            Case "MONTH"
                Return New FilteringMonth
            Case "REFERERTYPE"
                Return New FilteringRefererType
            Case "PASSIF"
                Return New FilteringPassive
        End Select

        Throw New Exception("Not a valid filtering name")
    End Function
End Class
