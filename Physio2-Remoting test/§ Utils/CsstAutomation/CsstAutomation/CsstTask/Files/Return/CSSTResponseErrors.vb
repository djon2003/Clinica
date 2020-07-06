Public Class CSSTResponseErrors
    Inherits List(Of CSSTResponseError)

    '' Errors
    

    '' Information
    Public Const PRESENCE_LIMIT_REACHED As String = "MF10263I"

    Private Const LINE_BREAKER As String = "<br/>"

    Public Function isErrorCodeExists(ByVal errorCode As String) As Boolean
        For Each curError As CSSTResponseError In Me
            If curError.errorCode = errorCode Then Return True
        Next

        Return False
    End Function

    Public Sub setErrorMessage(ByVal errorCode As String, ByVal errorMessage As String)
        For Each curError As CSSTResponseError In Me
            If curError.errorCode = errorCode Then
                curError.errorMessage = errorMessage
            End If
        Next
    End Sub

    Public Function getErrorMessage(ByVal errorCode As String) As String
        Dim errorMessage As String = String.Empty
        For Each curError As CSSTResponseError In Me
            If curError.errorCode = errorCode Then
                errorMessage = curError.errorMessage
            End If
        Next

        Return errorMessage
    End Function

    Public Function getReturnMessage()
        Dim returnMessage As String = String.Empty

        For Each curError As CSSTResponseError In Me
            If curError.errorMessage = String.Empty Then Continue For

            returnMessage &= LINE_BREAKER
            If curError.errorColor <> String.Empty Then returnMessage &= "<font color=""" & curError.errorColor & """>"
            returnMessage &= curError.errorMessage
            If curError.errorColor <> String.Empty Then returnMessage &= "</font>"
        Next

        If returnMessage <> String.Empty Then returnMessage = returnMessage.Substring(LINE_BREAKER.Length)

        Return returnMessage
    End Function
End Class
