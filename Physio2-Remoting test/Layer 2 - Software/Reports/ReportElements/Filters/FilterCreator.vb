Public Class FilterCreator
    Public Function createFilter(ByVal name As String) As ReportFilter
        Select Case name.ToUpper
            Case "FROMTO"
                Return New FilterFromTo()
            Case "NOCLIENT"
                Return New FilterNoClient
            Case "NOKP"
                Return New FilterNoKP
            Case "USER"
                Return New FilterUser
            Case "LISTFIELDDATA"
                Return New FilterListFieldData
            Case "MONTH"
                Return New FilterMonth
            Case "REFERERTYPE"
                Return New FilterRefererType
            Case "PASSIF"
                Return New FilterPassive
            Case "INPUTFIELDDATA"
                Return New FilterInputFieldData
            Case "NOFACTURE"
                Return New FilterNoFacture
        End Select

        Throw New Exception("Not a valid filter name")
    End Function
End Class
