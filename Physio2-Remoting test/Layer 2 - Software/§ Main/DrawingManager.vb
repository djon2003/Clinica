Public Class DrawingManager
    Inherits ManagerBase(Of DrawingManager)

    Private icons As New IconsCollection()
    Private images As New ImagesCollection()
    Private cursors As New CursorsCollection()


    Private iconToimagesCache As New Generic.Dictionary(Of Icon, Image)

    Private imagesPath As String

    Protected Sub New()
        imagesPath = Application.StartupPath & bar(Application.StartupPath) & "Images"
        If IO.Directory.Exists(imagesPath) = False Then IO.Directory.CreateDirectory(imagesPath)
    End Sub

    Public Sub loadManager()
        Dim devEnv As Boolean = IO.File.Exists("dev.env")

        Dim files() As String = IO.Directory.GetFiles(If(devEnv, imagesPath, appPath & bar(appPath) & "Data\Images"))
        Dim i As Integer

        If Not files Is Nothing AndAlso files.Length <> 0 Then
            For i = 0 To files.GetUpperBound(0)
                Dim fileName As String = getLastDir(files(i)).ToLower
                If fileName = "Thumbs.db" Then 'Fichier thumbs inutile de windows.. supprime auto
                    Try
                        IO.File.Delete(files(i))
                    Catch ex As Exception 'Juste au cas où
                    End Try

                    Continue For
                End If
                If Not devEnv Then
                    If IO.File.Exists(imagesPath & "\" & fileName) = False Then
                        IO.File.Copy(files(i), imagesPath & "\" & fileName, True)
                    Else
                        Dim thisFileInfo As New IO.FileInfo(files(i))
                        Dim curFileInfo As New IO.FileInfo(imagesPath & "\" & fileName)
                        If thisFileInfo.Length <> curFileInfo.Length Then IO.File.Copy(files(i), imagesPath & "\" & fileName, True)
                    End If
                End If
                Try
                    If fileName.EndsWith("gif") Or fileName.EndsWith("jpg") Or fileName.EndsWith("bmp") Or fileName.EndsWith("png") Then
                        images.add(fileName, New Bitmap(imagesPath & "\" & fileName))
                    ElseIf fileName.EndsWith("ico") Then
                        icons.add(fileName, New Icon(imagesPath & "\" & fileName))
                    ElseIf fileName.EndsWith("cur") Then
                        cursors.add(fileName, New Cursor(imagesPath & "\" & fileName))
                    End If
                Catch ex As Exception
                    'Skip errors
                End Try
            Next i
        End If
    End Sub

    Public Function getImage(ByVal fileName As String) As Image
        Return images.item(fileName.ToLower)
    End Function

    Public Function getIcon(ByVal fileName As String) As Icon
        Return icons.item(fileName.ToLower)
    End Function

    Public Function getCursor(ByVal fileName As String) As Cursor
        Return cursors.item(fileName.ToLower)
    End Function

    Public Shared Function cursorToImage(ByVal changingCursor As Cursor, ByVal mySize As Size) As Image
        If changingCursor Is Nothing Then Return Nothing

        Dim g As Graphics
        changingCursor.DrawStretched(g, New Rectangle(New Point(0, 0), mySize))
        Return New Bitmap(mySize.Width, mySize.Height)
    End Function

    Public Shared Function imageToIcon(ByVal changingImage As Image) As Icon
        If changingImage Is Nothing Then Return Nothing

        Dim tmpBmp As New Bitmap(changingImage)
        Dim returnedIcon As Icon = Icon.FromHandle(tmpBmp.GetHicon())
        tmpBmp.Dispose()
        Return returnedIcon
    End Function

    Public Shared Function iconToImage(ByVal changingIcon As Icon, ByVal mySize As Size) As Image
        If changingIcon Is Nothing Then Return Nothing

        'TODO: implement caching that use size too (no caching done for that now)

        Return New Bitmap(changingIcon.ToBitmap(), mySize)
    End Function

    Public Shared Function iconToBytes(ByVal changingIcon As Icon) As Byte()
        Dim tmpMemoryStream As New IO.MemoryStream()
        changingIcon.Save(tmpMemoryStream)
        Dim iconBytes(tmpMemoryStream.Length - 1) As Byte
        tmpMemoryStream.Read(iconBytes, 0, tmpMemoryStream.Length)

        Return iconBytes
    End Function

    Public Shared Function imageToBytes(ByVal changingImage As Image) As Byte()
        Dim tmpMemoryStream As New IO.MemoryStream()
        changingImage.Save(tmpMemoryStream, Drawing.Imaging.ImageFormat.Bmp)
        Dim imgBytes(tmpMemoryStream.Length - 1) As Byte
        tmpMemoryStream.Read(imgBytes, 0, tmpMemoryStream.Length)

        Return imgBytes
    End Function

    Public Shared Function resizeImage(ByRef myImage As Image, ByVal scale As Double) As Image
        If myImage Is Nothing Then Return Nothing

        ' Get the source bitmap.
        Dim bm_source As New Bitmap(myImage)

        Return resizeImage(bm_source, CInt(bm_source.Height) * scale, CInt(bm_source.Width * scale))
    End Function

    Public Shared Function resizeImage(ByRef myImage As Image, ByVal newHeight As Integer, ByVal newWidth As Integer) As Image
        If myImage Is Nothing Then Return Nothing

        ' Get the source bitmap.
        Dim bm_source As New Bitmap(myImage)

        If newWidth < 1 Then newWidth = bm_source.Width
        If newHeight < 1 Then newHeight = bm_source.Height

        ' Make a bitmap for the result.
        Dim bm_dest As New Bitmap(newWidth, newHeight)

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1)

        Return bm_dest
    End Function


#Region "Class IconsCollection"
    Public Class IconsCollection
        Inherits DictionaryBase

        Public Sub add(ByVal fileName As String, ByVal newIcon As Icon)
            MyBase.InnerHashtable.Add(fileName, newIcon)
        End Sub

        Public Sub remove(ByVal fileName As String)
            MyBase.InnerHashtable.Remove(fileName)
        End Sub

        Public Function item(ByVal fileName As String) As Icon
            Return CType(MyBase.InnerHashtable.Item(fileName), Icon)
        End Function
    End Class
#End Region

#Region "Class ImagesCollection"
    Public Class ImagesCollection
        Inherits DictionaryBase

        Public Sub add(ByVal fileName As String, ByVal newImage As Image)
            MyBase.InnerHashtable.Add(fileName, newImage)
        End Sub

        Public Sub remove(ByVal fileName As String)
            MyBase.InnerHashtable.Remove(fileName)
        End Sub

        Public Function item(ByVal fileName As String) As Image
            Return CType(MyBase.InnerHashtable.Item(fileName), Image)
        End Function
    End Class
#End Region

#Region "Class CursorsCollection"
    Public Class CursorsCollection
        Inherits DictionaryBase

        Public Sub add(ByVal fileName As String, ByVal newCursor As Cursor)
            MyBase.InnerHashtable.Add(fileName, newCursor)
        End Sub

        Public Sub remove(ByVal fileName As String)
            MyBase.InnerHashtable.Remove(fileName)
        End Sub

        Public Function item(ByVal fileName As String) As Cursor
            Return CType(MyBase.InnerHashtable.Item(fileName), Cursor)
        End Function
    End Class
#End Region

End Class

