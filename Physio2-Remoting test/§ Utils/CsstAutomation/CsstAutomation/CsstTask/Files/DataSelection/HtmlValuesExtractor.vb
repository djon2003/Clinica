Public Class HtmlValuesExtractor

    Private html As String
    Private htmlValues As List(Of HtmlValue)
    Private _evolution As String
    Private _treamentsSuspended As Boolean
    Private _folderFrequency As Integer
    Private _folderFrequencyJustification As String


    Public Sub New(ByVal html As String, ByVal htmlValues As List(Of HtmlValue))
        Me.html = html
        Me.htmlValues = htmlValues

        extractValues()
    End Sub

    Private Sub extractValues()
        For Each curHV As HtmlValue In htmlValues
            Dim curValue As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(html, curHV.getRegExPattern(), System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            If curValue IsNot Nothing Then
                If curValue.Groups.Count > curHV.getMatchCaptureIndex() + 1 Then
                    curHV.value = curValue.Groups("value").Value
                End If
            End If
        Next
    End Sub

    Public Function getValue(ByVal name As String) As Object
        For Each curHV As HtmlValue In htmlValues
            If curHV.name = name Then
                Dim value As Object = curHV.value
                Return value
            End If
        Next

        Return Nothing
    End Function

End Class
