Public Class ClipboardHelper

    Private Shared isClipboardSaved As Boolean = False
    Private Shared cbDataBackup As New Dictionary(Of String, Object)
    Private Shared cbDataObjectBackup As Object

    ''' <summary>
    ''' Allow to save current clipboard content so it can be restored later using "restore"
    ''' </summary>
    ''' <remarks>Taken from : http://stackoverflow.com/a/6263468/214898</remarks>
    Public Shared Sub save()
        cbDataObjectBackup = Clipboard.GetDataObject()

        cbDataBackup.Clear()
        Dim lFormats() As String = cbDataObjectBackup.GetFormats(False)
        For Each lFormat As String In lFormats
            cbDataBackup.Add(lFormat, cbDataObjectBackup.GetData(lFormat, False))
        Next

        isClipboardSaved = True
    End Sub

    ''' <summary>
    ''' Restore the clipboard content from the last "save" call.
    ''' </summary>
    ''' <remarks>Taken from : http://stackoverflow.com/a/6263468/214898</remarks>
    Public Shared Sub restore()
        If Not isClipboardSaved Then Exit Sub

        For Each lFormat As String In cbDataBackup.Keys
            cbDataObjectBackup.SetData(lFormat, False, cbDataBackup(lFormat))
        Next
        Try

            Clipboard.SetDataObject(cbDataObjectBackup, True, 3, 10)
        Catch ex As Exception
            ex = ex
        End Try
    End Sub

    Public Shared Function getHTMLFromClipboard() As String
        'Example of content after the ' line

        '''''''''''''''''''''''''''''''''''''''''''''''
        'Version:0.9
        'StartHTML:0000000229
        'EndHTML:0000001489
        'StartFragment:0000000267
        'EndFragment:0000001451
        'SourceURL:http://msdn.microsoft.com/en-us/library/windows/desktop/ms649013(v=vs.85).aspx#_win32_Standard_Clipboard_Formats
        '<html>
        '<body>
        '<!--StartFragment-->
        '<h3 style="color: rgb(219, 113, 0); font-family: 'Segoe UI Light', 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 18px; font-weight: normal; margin: 0px; padding-bottom: 5px; padding-top: 5px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; ">lipboard Formats</h3><p style="color: rgb(42, 42, 42); margin-top: 0px; margin-bottom: 0px; padding-bottom: 15px; line-height: 18px; font-family: 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; ">The system implicitly converts data between certain clipboard formats: if a window requests data in a format that is not on the clipboard, the system converts an available fo</p>
        '<!--EndFragment-->
        '</body>
        '</html>

        Dim html As String = ""
        With My.Computer.Clipboard
            Select Case True
                Case .ContainsData("HTML Format") = True
                    html = .GetData("HTML Format")
                    Dim versionTag As String = "Version:"
                    Dim startTag As String = "StartFragment:" ' "<!--StartFragment-->"
                    Dim endTag As String = "EndFragment:" '"<!--EndFragment-->"

                    Dim versionPos As Integer = html.IndexOf(versionTag) + versionTag.Length
                    Dim version As String = html.Substring(versionPos, html.IndexOf(vbCr, versionPos) - versionPos)

                    'Get HTML
                    Dim startPos As Integer = html.IndexOf(startTag) + startTag.Length
                    startPos = html.Substring(startPos, html.IndexOf(vbCr, startPos) - startPos)
                    Dim endPos As Integer = html.IndexOf(endTag) + endTag.Length
                    endPos = html.Substring(endPos, html.IndexOf(vbCr, endPos) - endPos)
                    html = html.Substring(startPos, endPos - startPos)

                    'Adjust encoding
                    'Line below doesn't work in WIN7
                    'html = html.Replace("Â ", "&nbsp;") '(WINXP ONLY) This is done first to ensure spaces beginning lines are considered
                    html = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(html))

                    ''''''''''''''''''''''''''''''''''''''''''''''
                    'First spaces of lines correction
                    'WINXP (Could be replaced directly with &nbsp; but WIN7 require more treatment. So, convert to get same functionality
                    html = html.Replace(Chr(160), " ")

                    'The same character gives 160 for WINXP and 32 for WIN7... IMPOSSIBLE !

                    Dim tags() As String = {"BR", "DIV", "TD", "SPAN", "P"}
                    Dim nbsp As String = "&nbsp;"
                    'For Each curTag As String In tags
                    Dim curPos As Integer = html.IndexOf("<", 0, StringComparison.OrdinalIgnoreCase)
                    Dim lastPos As Integer
                    While curPos <> -1
                        lastPos = curPos
                        curPos = html.IndexOf(">", curPos + 1, StringComparison.OrdinalIgnoreCase)
                        If curPos = -1 Then curPos = lastPos

                        curPos += 1

                        While (curPos + 1) < html.Length AndAlso html.Substring(curPos, 1) = " "
                            html = html.Substring(0, curPos) & nbsp & html.Substring(curPos + 1)
                            curPos += nbsp.Length
                            'Can't skip finally.. had a bug on some XP
                            'pos += 1  '(Not removing one because we can skip a space each 2 spaces)
                        End While

                        curPos = html.IndexOf("<", curPos, StringComparison.OrdinalIgnoreCase)
                    End While
                    'Next
                    ''''''''''''''''''''''''''''''''''''''''''''''
                Case Else
                    Throw New Exception("Normal handling")
            End Select
        End With

        Return html
    End Function
End Class
