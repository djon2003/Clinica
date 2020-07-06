Public Class ContentType
    Inherits System.Net.Mime.ContentType

    Private Shared mimesExtension As Generic.Dictionary(Of String, String)

    Private Const BOUNDARY_STRING As String = "boundary="
    Private Const TEXT_PLAIN_TYPE As String = "text/plain;"
    Private Const CHARSET_STRING As String = "charset="
    Private Const FILE_EXTENSIONS_VS_MIME As String = "Data\extensions.mime"
    Private Const MULTIPART_TYPE As String = "multipart"
    Private Const NAME_PARAM As String = "name="

    Public Sub New(ByVal contentType As String)
        MyBase.New(correctString(contentType))

        If Me.CharSet IsNot Nothing Then
            If Me.CharSet.ToLower() = "utf8" Then
                Me.CharSet = "utf-8"
            ElseIf Me.CharSet.ToLower() = "utf16" Then
                Me.CharSet = "utf-16"
            ElseIf Me.CharSet.ToLower() = "utf32" Then
                Me.CharSet = "utf-32"
            End If
        End If
    End Sub

    Private Shared Function correctString(ByVal contentType As String) As String
        'Test if corrections are needed
        Dim currentFormNotSupported As Boolean = False
        Try
            Dim testCT As New System.Net.Mime.ContentType(contentType)
        Catch ex As Exception
            currentFormNotSupported = True
        End Try

        'Correct contentType errors
        If currentFormNotSupported Then
            contentType = correctSimpleErrors(contentType)
            contentType = correctBoundaryError(contentType)
            contentType = correctTextPlainMissingCharsetError(contentType)
        End If

        Return contentType
    End Function

    Private Shared Function correctSimpleErrors(ByVal contentType As String) As String
        If Not contentType.StartsWith(MULTIPART_TYPE) Then contentType = contentType.ToLower()

        contentType = contentType.Trim.Replace(" = ", "=").Replace("{""", "").Replace("""}", "")
        contentType = replaceAccents(contentType)
        If contentType.IndexOf(";") = -1 Then
            Dim firstSpace As Integer = contentType.IndexOf(" ")
            If firstSpace <> -1 Then
                contentType = contentType.Substring(0, firstSpace) & ";" & contentType.Substring(firstSpace)
            End If
        End If

        'Remove spaces when type is multipart
        If contentType.StartsWith(MULTIPART_TYPE) Then contentType = contentType.Replace(" ", "")

        Return contentType
    End Function

    Private Shared Function correctTextPlainMissingCharsetError(ByVal contentType As String) As String
        Dim textPlainPos As Integer = contentType.IndexOf(TEXT_PLAIN_TYPE)
        If textPlainPos <> -1 Then
            Dim isCharsetExists As Boolean = contentType.IndexOf(CHARSET_STRING) <> -1

            If Not isCharsetExists Then
                Dim encoding As String = contentType.Substring(textPlainPos + TEXT_PLAIN_TYPE.Length).Trim()
                contentType = contentType.Substring(0, textPlainPos + TEXT_PLAIN_TYPE.Length)

                If encoding <> String.Empty Then contentType &= " " & CHARSET_STRING & encoding
            End If
        End If

        Return contentType
    End Function

    Private Shared Function correctBoundaryError(ByVal contentType As String) As String
        'Ensure multipart boundary have " because sometimes it bugs
        Dim boundaryIndex As Integer = contentType.IndexOf(BOUNDARY_STRING)
        If boundaryIndex <> -1 Then
            boundaryIndex += BOUNDARY_STRING.Length
            If contentType.Substring(boundaryIndex, 1) <> """" Then
                contentType = contentType.Substring(0, boundaryIndex) & """" & contentType.Substring(boundaryIndex)
                Dim boundaryEnd As Integer = contentType.IndexOf(" ", boundaryIndex)
                If boundaryEnd = -1 Then boundaryEnd = contentType.IndexOf(";", boundaryIndex)

                If boundaryEnd = -1 Then
                    contentType &= """"
                Else
                    contentType = contentType.Substring(0, boundaryEnd) & """" & contentType.Substring(boundaryEnd)
                End If
            End If
        End If

        Return contentType
    End Function

    Public Function getMimeExtension() As String
        If mimesExtension Is Nothing Then loadMimesExtension()

        If mimesExtension.ContainsKey(Me.MediaType) Then Return mimesExtension(Me.MediaType)

        Return Nothing
    End Function

    Private Shared Sub loadMimesExtension()
        mimesExtension = New Generic.Dictionary(Of String, String)()

        Dim mimeFile As String = appPath & bar(appPath) & FILE_EXTENSIONS_VS_MIME
        If Not IO.File.Exists(mimeFile) Then Exit Sub

        Dim extensions() As String = IO.File.ReadAllLines(mimeFile)
        For Each extension As String In extensions
            If extension.Trim() = String.Empty Then Continue For

            Dim data() As String = extension.Split(New Char() {"/"}, 2)
            If data.Length <> 2 Then Continue For
            If mimesExtension.ContainsKey(data(1)) Then Continue For

            mimesExtension.Add(data(1), data(0))
        Next
    End Sub

End Class
