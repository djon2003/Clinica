Imports System.IO
Imports System.IO.Compression


Public Class Compression
    Private Shared WithEvents zipper As New Chilkat.Zip

    Public Shared Sub compress(ByVal cheminSource() As String, ByVal cheminCible As String, ByVal zipComment As String, ByVal extraPath As String)
        zipper.EnableEvents = True
        zipper.UnlockComponent("blabla")
        zipper.NewZip(cheminCible)
        zipper.AppendFromDir = extraPath
        For i As Integer = 0 To cheminSource.Length - 1
            zipper.AppendOneFileOrDir(cheminSource(i).Substring(extraPath.Length + 1), True)
        Next i
        zipper.WriteZipAndClose()


        'Dim zipper As New Zip()
        'AddHandler zipper.ReceivePrintMessage, AddressOf zipPrint
        'AddHandler zipper.ReceiveServiceMessage, AddressOf zipService
        'zipper.Comment = zipComment
        'zipper.CommentOption = CommentEnum.True
        'zipper.FilesToZip = cheminSource
        'zipper.NoDirEntries = DirectoryEntriesIgnoredEnum.False
        'zipper.System = SystemFilesIgnoredEnum.False
        'zipper.Volume = VolumeLabelStoredEnum.False
        'zipper.ZipFileName = cheminCible
        'zipper.ZipFiles()
    End Sub

    Private Shared Function innerCompress(ByVal cheminSource As String, ByVal cheminCible As String) As String
        Try
            Dim hbread As New BinaryReader(File.OpenRead(cheminSource))
            Dim f_array(hbread.BaseStream.Length) As Byte
            hbread.BaseStream.Seek(0, SeekOrigin.Begin)
            hbread.BaseStream.Read(f_array, 0, f_array.Length)
            hbread.BaseStream.Flush()
            hbread.Close()
            Dim tmpstream As FileStream = New FileStream(cheminCible, FileMode.OpenOrCreate)
            Dim gzipper As GZipStream = New GZipStream(tmpstream, CompressionMode.Compress, True)
            Dim originalFile() As Byte = System.Text.Encoding.ASCII.GetBytes(cheminSource)
            gzipper.Write(f_array, 0, f_array.Length)
            gzipper.Flush()
            gzipper.Close()
            Return "compression effectuée avec succès"
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function

    Public Shared Function compress(ByVal cheminSource As String, ByVal cheminCible As String) As String
        innerCompress(cheminSource, cheminCible)
    End Function

    Public Shared Function decompress(ByVal cheminSource As String, ByVal cheminCible As String) As String
        innerDecompress(cheminSource, cheminCible)
    End Function



    Private Const buffer_size As Integer = 100

    Public Shared Function readAllBytesFromStream(ByVal stream As Stream, ByVal buffer() As Byte) As Integer
        ' Use this method is used to read all bytes from a stream.
        Dim offset As Integer = 0
        Dim totalCount As Integer = 0
        ReDim buffer(buffer.Length + 1000)
        While True
            If offset Mod 1000 = 0 Then Console.Write(".")
            Dim bytesRead As Integer = stream.Read(buffer, offset, buffer_size)
            If bytesRead = 0 Then
                Exit While
            End If
            offset += bytesRead
            totalCount += bytesRead
        End While
        Return totalCount
    End Function 'ReadAllBytesFromStream

    Public Shared Function compareFile(ByVal cheminSource As String, ByVal cheminCible As String) As Boolean
        Dim firstFile As New FileStream(cheminSource, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim secondFile As New FileStream(cheminCible, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim compared As Boolean = True
        Dim firstBuffer(firstFile.Length) As Byte
        Dim secondBuffer(secondFile.Length) As Byte
        Dim curPos As Long = 0
        Dim firstLength As Long = firstFile.Length
        For i As Integer = 0 To firstLength - 1
            If i Mod 1048576 = 0 Then
                If i = 0 Then Console.Write("0") Else Console.Write((i + 1) / firstLength * 100)
                Console.WriteLine(" %")
            End If
            Dim nbFirstRead As Integer = firstFile.Read(firstBuffer, curPos, 1024)
            Dim nbSecondRead As Integer = secondFile.Read(secondBuffer, curPos, 1024)
            compared = compareData(firstBuffer, curPos + 1024, secondBuffer, curPos + 1024, curPos)
            curPos += 1024
        Next i

    End Function

    Public Shared Function compareData(ByVal buf1() As Byte, ByVal len1 As Integer, ByVal buf2() As Byte, ByVal len2 As Integer, Optional ByVal starting As Long = 0) As Boolean
        ' Use this method to compare data from two different buffers.
        If len1 <> len2 Then
            MsgBox("Number of bytes in two buffer are different" & len1 & ":" & len2)
            Return False
        End If

        Dim i As Integer
        For i = starting To len1 - 1
            If buf1(i) <> buf2(i) Then
                MsgBox("byte " & i & " is different " & buf1(i) & "|" & buf2(i))
                Return False
            End If
        Next i
        'MsgBox("All bytes compare.")
        Return True
    End Function 'CompareData

    Public Shared Sub innerCompress2(ByVal cheminSource As String, ByVal cheminCible As String)
        Dim originalFile() As Byte = System.Text.Encoding.ASCII.GetBytes(cheminSource)

        Dim infile As FileStream
        Try
            ' Open the file as a FileStream object.
            infile = New FileStream(cheminSource, FileMode.Open, FileAccess.Read, FileShare.Read)

            Dim buffer(infile.Length + originalFile.Length) As Byte
            Array.Copy(originalFile, buffer, originalFile.Length)
            buffer(originalFile.Length) = 0

            ' Read the file to ensure it is readable.
            Dim count As Integer = infile.Read(buffer, originalFile.Length + 1, infile.Length)
            'If count <> buffer.Length Then
            '    infile.Close()
            '    MsgBox("Test Failed: Unable to read data from file")
            '    Return
            'End If
            infile.Close()
            Dim ms As New MemoryStream()
            ' Use the newly created memory stream for the compressed data.
            Dim compressedzipStream As New GZipStream(ms, CompressionMode.Compress, True)
            compressedzipStream.Write(buffer, 0, buffer.Length)

            ReDim buffer(ms.Length - 1)
            readAllBytesFromStream(ms, buffer)

            Dim outfile As New FileStream(cheminCible, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)
            outfile.Write(buffer, 0, buffer.Length)

            ' Close the stream.
            outfile.Close()
            compressedzipStream.Close()
        Catch e As Exception
            MsgBox("Error: The file being read contains invalid data.")
        End Try

    End Sub 'GZipCompressDecompress

    Public Shared Function innerDecompress(ByVal cheminSource As String, ByVal cheminCible As String) As String
        Try
            Dim bufferLen As Integer = 1024
            Dim hbread As New BinaryReader(File.OpenRead(cheminSource))
            Dim gzipper As GZipStream = New GZipStream(hbread.BaseStream, CompressionMode.Decompress)
            Dim hbwrite As New BinaryWriter(File.OpenWrite(cheminCible))
            Dim f_array(bufferLen) As Byte
            Dim readlen As Integer
            Dim flag As Boolean = True
            Dim lastZeroByte As Integer
            Do
                '        lastZeroByte = Array.IndexOf(F_array, CByte(0))
                readlen = gzipper.Read(f_array, 0, f_array.Length)

                If readlen > 0 Then
                    'Dim fileName As String = System.Text.Encoding.ASCII.GetString(F_array, 0, lastZeroByte)
                    flag = True
                    If readlen = (bufferLen + 1) Then hbwrite.Write(f_array, 0, readlen) Else hbwrite.Write(f_array, 0, readlen - 1)
                Else
                    flag = False
                End If
            Loop While (flag)
            hbwrite.Close()
            gzipper.Close()
            Return "decompression effectuée avec succés"
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function

    Public Shared Sub innerDecompress2(ByVal cheminSource As String, ByVal cheminCible As String)
        Dim infile As New FileStream(cheminSource, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim ms As New MemoryStream()
        Dim buffer(infile.Length) As Byte
        readAllBytesFromStream(infile, buffer)
        ms.Write(buffer, 0, buffer.Length)

        Dim zipStream As New GZipStream(ms, CompressionMode.Decompress, True)
        Dim decompressedBuffer(ms.Length - 1) As Byte
        ' Use the ReadAllBytesFromStream to read the stream.
        Dim totalCount As Integer = readAllBytesFromStream(zipStream, decompressedBuffer)
        Dim indexOf0Byte As Integer = Array.IndexOf(decompressedBuffer, CByte(0))
        Dim bytHeader(indexOf0Byte - 1) As Byte
        Array.Copy(decompressedBuffer, bytHeader, bytHeader.Length)
        Dim fileBytes(decompressedBuffer.Length - indexOf0Byte - 1) As Byte
        decompressedBuffer.CopyTo(fileBytes, indexOf0Byte + 1)
        Dim fileName As String = System.Text.Encoding.ASCII.GetString(bytHeader, 0, bytHeader.Length)
        MsgBox(fileName)

        Dim outfile As New FileStream(cheminCible, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read)
        outfile.Write(fileBytes, 0, fileBytes.Length)


        'If Not GZipTest.CompareData(Buffer, Buffer.Length, decompressedBuffer, totalCount) Then
        '    msg = "Error. The two buffers did not compare."
        '    MsgBox(msg)

        'End If
        zipStream.Close()
    End Sub

    Private Shared Sub zipper_OnFileAdded(ByVal sender As Object, ByVal args As Chilkat.FileAddedEventArgs) Handles zipper.OnFileAdded
        Console.WriteLine("File " & args.FileName & " added of " & args.FileSize & " bytes")
    End Sub

    Private Shared Sub zipper_OnFileZipped(ByVal sender As Object, ByVal args As Chilkat.FileZippedEventArgs) Handles zipper.OnFileZipped
        Console.WriteLine("File " & args.FileName & " zipped (from " & args.FileSize & " bytes to " & args.CompressedSize & ")")
    End Sub

    Private Shared Sub zipper_OnWriteZipPercentDone(ByVal sender As Object, ByVal args As Chilkat.WriteZipPercentDoneEventArgs) Handles zipper.OnWriteZipPercentDone
        Console.WriteLine(args.PercentDone & " %")
    End Sub
End Class

