Public Class PropertiesHelper
    Public Shared Function transformProperties(ByVal myPropertiesLine As String, Optional ByVal lowerKeys As Boolean = False) As Hashtable
        Dim myProperties As New Hashtable
        If myPropertiesLine = "" Then Return myProperties

        Dim myKeysValues As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(myPropertiesLine, "((^§)|§§)(?<key>[^§]*)§\=§(?<value>(.(?!(§§|§$)))*[^§]*)")

        myProperties.Clear()
        Dim myMatch As System.Text.RegularExpressions.Match
        With myKeysValues
            For Each myMatch In myKeysValues
                Dim myValue As String = myMatch.Groups("value").Value
                Dim myKey As String = myMatch.Groups("key").Value
                If lowerKeys Then myKey = myKey.ToLower()

                If myValue.StartsWith("{") Then
                    myValue = myValue.Substring(1, myValue.Length - 2)
                    Dim myValues() As String = myValue.Split(New Char() {"£"})
                    myProperties.Add(myKey, myValues)
                Else
                    myProperties.Add(myKey, myValue.Trim)
                End If
            Next
        End With

        Return myProperties
    End Function

    Public Shared Function transformProperties(ByVal myProperties As Hashtable, Optional ByVal skipEmpty As Boolean = True) As String
        Dim transformSB As New System.Text.StringBuilder()
        For Each entry As DictionaryEntry In myProperties
            Dim curValue As String = ""
            If entry.Value IsNot Nothing Then
                If TypeOf entry.Value Is Array Then
                    curValue = "{" & String.Join("£", entry.Value) & "}"
                ElseIf TypeOf entry.Value Is Hashtable Then
                    For Each curEntry As DictionaryEntry In entry.Value
                        curValue &= "£" & curEntry.Key & "=" & curEntry.Value
                    Next
                    If curValue <> "" Then curValue = curValue.Substring(1)
                    curValue = "{" & curValue & "}"
                Else
                    curValue = entry.Value.ToString
                End If
            End If
            If skipEmpty = False AndAlso curValue = "" Then curValue = " "
            If curValue <> "" AndAlso curValue <> "{}" Then transformSB.Append("§").Append(entry.Key).Append("§=§").Append(curValue).Append("§")
        Next

        Return transformSB.ToString
    End Function
End Class
