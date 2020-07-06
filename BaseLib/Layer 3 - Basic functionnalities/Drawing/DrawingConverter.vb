Imports System.Drawing

Public Class DrawingConverter
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Convert a bitmap to its base64 representation
    ''' </summary>
    ''' <param name="bmp"></param>
    ''' <returns></returns>
    ''' <remarks>Taken from : http://stackoverflow.com/a/30047647/214898</remarks>
    Public Shared Function convertToBase64(ByVal bmp As Bitmap) As String
        If bmp Is Nothing Then Return String.Empty

        Dim ms As New System.IO.MemoryStream()
        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim byteImage() As Byte = ms.ToArray()
        Return Convert.ToBase64String(byteImage)
    End Function

End Class
