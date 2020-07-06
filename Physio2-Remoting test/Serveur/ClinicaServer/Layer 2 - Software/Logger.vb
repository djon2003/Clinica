Public Class Logger

    Private Const MAX_LOGS_LENGTH As Integer = 1000000
    Private Const DATE_SPLITTER As String = " | "
    Private Const SPACE As String = " "
    Private Const LOG_RECEVEIVED_INDICATOR As String = ">>"
    Private Const LOG_SENT_INDICATOR As String = "<<"

    Private Shared logs As New System.Text.StringBuilder()

    Public Shared Sub logText(ByVal [text] As String, ByVal fromTo As String, Optional ByVal isReceived As Boolean = True)
        'Clear logs and save it to file
        If logs.Length > MAX_LOGS_LENGTH Then saveLog(Date.Now)

        If [text] <> String.Empty Then
            Try
                logs.Append(Date.Now.ToString("yyyy/MM/dd HH:mm:ss")).Append(DATE_SPLITTER).Append(fromTo).Append(SPACE).Append(IIf(isReceived, LOG_RECEVEIVED_INDICATOR, LOG_SENT_INDICATOR)).Append(SPACE).Append([text]).Append(If([text].EndsWith(vbCrLf), String.Empty, vbCrLf))
            Catch ex As System.OutOfMemoryException
                addErrorLog(New Exception("logs.Length=" & logs.Length & " : MAX_LOGS_LENGTH=" & MAX_LOGS_LENGTH, ex))
            End Try
        End If

        mainWin.logText(logs.ToString)
    End Sub


    Public Shared Sub saveLog(ByVal curDate As Date)
        Dim path As String = My.Application.Info.DirectoryPath & IIf(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & "Logs"
        IO.Directory.CreateDirectory(path)

        path &= "\" & Base.DateFormat.getTextDate(curDate, Base.DateFormat.TextDateOptions.YYYYMMDD, , ".") & ".log"
        Dim curLogs As String = logs.ToString
        logs = New System.Text.StringBuilder()
        IO.File.AppendAllText(path, curLogs)
        mainWin.logText(String.Empty)
    End Sub

End Class
