Imports System.Text.RegularExpressions
Imports System.IO
Imports Microsoft.Win32.SafeHandles

Module Fichiers
    Private foundFiles As New ArrayList()

    Public Function compareFile(ByVal cheminSource As String, ByVal cheminCible As String) As Boolean
        Dim firstFile As New IO.FileStream(cheminSource, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        Dim secondFile As New IO.FileStream(cheminCible, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        Dim compared As Boolean = True
        Dim firstBuffer(firstFile.Length) As Byte
        Dim secondBuffer(secondFile.Length) As Byte
        Dim curPos As Long = 0
        Dim firstLength As Long = firstFile.Length
        Dim moding As Long = firstLength / 1024 / 100
        For i As Integer = 0 To firstLength - 1 Step 1024
            Dim nbToRead As Integer = 1024
            If (curPos + 1024) > firstLength Then nbToRead = firstLength - curPos
            Dim nbFirstRead As Integer = firstFile.Read(firstBuffer, curPos, nbToRead)
            Dim nbSecondRead As Integer = secondFile.Read(secondBuffer, curPos, nbToRead)
            compared = compareData(firstBuffer, curPos + nbToRead, secondBuffer, curPos + nbToRead, curPos)
            curPos += 1024

            If compared = False Then
                firstFile.Close()
                secondFile.Close()
                Return False
            End If
        Next i

        firstFile.Close()
        secondFile.Close()

        Return True
    End Function

    Private Function compareData(ByVal buf1() As Byte, ByVal len1 As Integer, ByVal buf2() As Byte, ByVal len2 As Integer, Optional ByVal starting As Long = 0) As Boolean
        ' Use this method to compare data from two different buffers.
        If len1 <> len2 Then
            Return False
        End If

        Dim i As Integer
        For i = starting To len1 - 1
            If buf1(i) <> buf2(i) Then
                Return False
            End If
        Next i

        Return True
    End Function 'CompareData

    Public Function readFile(ByVal pathFromApp As String, Optional ByVal fileMask() As Boolean = Nothing, Optional ByVal returnWhiteLine As Boolean = True, Optional ByVal useAppBase As Boolean = True) As Array
        Dim Init, accept As Boolean
        Dim lineCounter As Integer
        Dim Line, currentFile As String
        Dim lines As New ArrayList()

        If useAppBase Then
            currentFile = appPath & bar(appPath) & pathFromApp
        Else
            currentFile = pathFromApp
        End If
        lineCounter = 1
        Init = False

        Line = ""
        If IO.File.Exists(currentFile) = False Then
            lines.Add("ERROR:NOFILE")
            Return lines.ToArray(Line.GetType)
        End If

        Dim myFile As IO.FileStream
        Try
            myFile = IO.File.Open(currentFile, IO.FileMode.Open, IO.FileAccess.Read)
            Dim myFileReader As New IO.StreamReader(myFile, System.Text.Encoding.UTF8)

            Line = myFileReader.ReadLine()
            Do While (Not Line Is Nothing)
                accept = False
                If fileMask Is Nothing OrElse fileMask.Length = 0 Then
                    accept = True
                Else
                    If fileMask.GetUpperBound(0) < lineCounter Then
                        If fileMask(0) = True Then accept = True
                    Else
                        accept = fileMask(lineCounter)
                    End If
                End If
                If returnWhiteLine = False And Line = "" Then accept = False
                If Line <> "" And Line.Length > 1 Then If Line.Substring(0, 2) = "#§" Then accept = False
                If accept = True Then
                    Init = True
                    lines.Add(Line)
                End If

                lineCounter += 1
                Line = myFileReader.ReadLine()
            Loop
        Catch
            lines.Clear()
            lines.Add("ERROR:FILECANTBEOPENED")
        Finally
            If Not myFile Is Nothing Then
                myFile.Close()
            End If
        End Try

        If Init = False Then
            lines.Add("ERROR:FILEEMPTY")
        End If

        Line = ""
        Return lines.ToArray(Line.GetType)
    End Function

    Public Function readFile(ByVal pathFromApp As String, ByVal binaryReading As Boolean, Optional ByVal fileMask() As Boolean = Nothing, Optional ByVal returnWhiteLine As Boolean = True, Optional ByVal useAppBase As Boolean = True) As Array
        If binaryReading = False Then Return readFile(pathFromApp, fileMask, returnWhiteLine, useAppBase)

        Dim data As Byte() = Nothing
        Dim sPath As String = pathFromApp
        If useAppBase = True Then sPath = appPath & bar(appPath) & pathFromApp

        Dim fInfo As New IO.FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length
        Dim fStream As New IO.FileStream(sPath, IO.FileMode.Open, IO.FileAccess.Read)
        Dim br As New BinaryReader(fStream)
        data = br.ReadBytes(numBytes)

        Return data
    End Function


    Public Function lastFile(ByRef pathToSearch As String, Optional ByVal sorted As Boolean = True) As String
        Dim LFile2, LFile, files() As String
        ReDim files(0)
        Dim n As Integer = 0

        If IO.Directory.GetFiles(pathToSearch).Length = 0 Then Return "ERROR:NOFILE"
        LFile = Dir(pathToSearch)

        Do
            LFile2 = LFile
            If LFile2 <> "" Then
                ReDim Preserve files(n)
                files(n) = LFile2
                n += 1
            End If
            LFile = Dir()
        Loop Until LFile = ""

        If sorted = False Then
            If LFile2 <> "" Then
                Return LFile2
            Else
                Return "ERROR:NOFILE"
            End If
        Else
            Try
                Array.Sort(files)
                Return files(files.Length - 1)
            Catch
                Return "ERROR:NOFILE"
            End Try
        End If
    End Function

    Public Function replaceIllegalChars(ByVal fileName As String) As String
        'Remove illegal characters
        '\/:*?"<>|
        Return fileName.Replace("/", "-").Replace("\", "-").Replace(":", "=").Replace("*", " ").Replace("?", " ").Replace("""", "'").Replace("<", "(").Replace(">", ")").Replace("|", "-")
    End Function

    Public Function lockFile(ByVal pathFromApp As String, Optional ByVal quitIfQueued As Boolean = False) As Boolean
        If ConnectionsManager.isLoaded = False OrElse ConnectionsManager.currentUser < 1 Then Return True

        Dim MyUser, NoFile, myFiles(), myFile As String
        Dim fileNum As Integer
        Dim t As Byte
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
StartBack:
        'Ajout dans la file d'attente
        IO.Directory.CreateDirectory(appPath & bar(appPath) & "Data\Queues\" & pathFromApp)
        'TODO : genUniqueNo() now uses the SQL database... it shouldn't use it for files.. though there is an error in it.
        '-->Even more, almost all places using lockFile function should be replaced by a DB counterpart
        NoFile = addZeros(genUniqueNo(), 5)

        myFile = NoFile & "-" & ConnectionsManager.currentUser
        If IO.File.Exists(appPath & bar(appPath) & "Data\Queues\" & pathFromApp & "\" & myFile) = True Then GoTo StartBack
        Dim newFile As IO.Stream
        Dim skipLocking As Boolean = False
        Try
            newFile = IO.File.Create(appPath & bar(appPath) & "Data\Queues\" & pathFromApp & "\" & myFile)
        Catch ex As Exception
            skipLocking = True
        Finally
            If newFile IsNot Nothing Then newFile.Close()
        End Try
        If skipLocking Then Return True

        fileNum = FreeFile()
        FileOpen(fileNum, appPath & bar(appPath) & "Data\Queues\" & pathFromApp & "\" & NoFile & "-" & ConnectionsManager.currentUser, OpenMode.Binary)
        FileClose(fileNum)

        t = "2"
        Do
            myFiles = IO.Directory.GetFiles(appPath & bar(appPath) & "Data\Queues\" & pathFromApp)
            If myFiles.Length = 0 Then Exit Do

            Try
                MyUser = myFiles(0).Substring(CStr(appPath & bar(appPath) & "Data\Queues\" & pathFromApp).Length + 1)
                If MyUser = myFile Then Exit Do
            Catch
                Exit Do
            End Try

            If quitIfQueued = True Then Return False
        Loop Until t = "1"
fin:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Return True
    End Function

    Public Sub unLockFile(ByVal pathFromApp As String)
        If ConnectionsManager.isLoaded = False OrElse ConnectionsManager.currentUser < 1 Then Exit Sub
        If IO.Directory.Exists(appPath & bar(appPath) & "Data\Queues\" & pathFromApp) = False Then Exit Sub

        Dim myFiles() As String = IO.Directory.GetFiles(appPath & bar(appPath) & "Data\Queues\" & pathFromApp)
        If myFiles.Length <> 0 Then IO.File.Delete(myFiles(0))
    End Sub

    Private m_objFileLockMutex As New System.Threading.Mutex

    Public Sub writeFile(ByRef pathFromApp As String, ByVal fileContent() As String, Optional ByVal sortFile As Boolean = False, Optional ByVal delFileIfEmpty As Boolean = False, Optional ByVal keepWhiteLine As Boolean = True, Optional ByVal useAppBase As Boolean = True, Optional ByVal internalCount As Integer = 0)
        Dim tempFileContent As New ArrayList
        Dim i As Integer
        Dim currentFile As String
        Dim fEmpty As Boolean = True
        Dim isReadOnly As Boolean = False
        If sortFile = True Then Array.Sort(fileContent)
        If useAppBase Then
            currentFile = appPath & bar(appPath) & pathFromApp
        Else
            currentFile = pathFromApp
        End If

        IO.Directory.CreateDirectory(currentFile.Substring(0, currentFile.Length - Fichiers.getLastDir(currentFile).Length - 1))

        If Not fileContent Is Nothing AndAlso fileContent.Length <> 0 Then
            If keepWhiteLine = False Then
                For i = 0 To fileContent.GetUpperBound(0)
                    If fileContent(i) <> "" Then tempFileContent.Add(fileContent(i))
                Next i
            Else
                tempFileContent.AddRange(fileContent)
            End If
        End If

        If tempFileContent.Count = 0 And delFileIfEmpty = True Then
            IO.File.Delete(currentFile)
            Exit Sub
        End If

        If IO.File.Exists(currentFile) Then
            If (IO.File.GetAttributes(currentFile) And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then
                isReadOnly = True
                IO.File.SetAttributes(currentFile, IO.File.GetAttributes(currentFile) Xor IO.FileAttributes.ReadOnly)
            End If
        End If

        Dim retry As Boolean = False
        Dim bLockAcquired As Boolean = False
        Dim nTimeoutPeriod As Integer = -1
        Try
            'All threads in all processes wait for same mutex. 
            'bLockAcquired will be false if something is blocking 
            'and the thread had to wait too long. In this context 
            'it is common to use an infinite timeout. 
            bLockAcquired = m_objFileLockMutex.WaitOne(nTimeoutPeriod, False)
            If bLockAcquired Then
                Dim writerStream As IO.FileStream
                Dim content As String = String.Join(vbCrLf, tempFileContent.ToArray(currentFile.GetType))
                Try
                    writerStream = New IO.FileStream(currentFile, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                    Dim ws_Writer As New IO.StreamWriter(writerStream, System.Text.Encoding.UTF8)
                    With CType(ws_Writer, IO.TextWriter)
                        .Write(content)
                        .Flush()
                        .Close()
                    End With

                    ws_Writer = Nothing
                Catch ex As Exception 'REM Exception not handle
                    If internalCount < 5 Then
                        retry = True
                    Else
                        addErrorLog(New Exception("Écriture du fichier - " & currentFile, ex))
                    End If
                Finally
                    If Not writerStream Is Nothing Then writerStream.Close()
                End Try
            End If
        Catch ex As Exception 'REM Exception not handle
            addErrorLog(New Exception("Écriture du fichier - " & currentFile, ex))
        Finally
            If bLockAcquired Then m_objFileLockMutex.ReleaseMutex()
        End Try

        If retry Then
            writeFile(pathFromApp, fileContent, sortFile, delFileIfEmpty, keepWhiteLine, useAppBase, internalCount + 1)
            Exit Sub
        End If
        If isReadOnly Then
            IO.File.SetAttributes(currentFile, IO.File.GetAttributes(currentFile) Or IO.FileAttributes.ReadOnly)
        End If
    End Sub

    Public Function hddListing(ByVal fullPath As String, Optional ByVal getDir As Boolean = True, Optional ByVal getFile As Boolean = False, Optional ByVal typeListing As String = "*.*", Optional ByVal attributs As FileAttribute = FileAttribute.Hidden Or FileAttribute.ReadOnly Or FileAttribute.Archive) As ArrayList
        Dim myArray As New ArrayList()

        If IO.Directory.Exists(fullPath) = False Then Return myArray

        Dim wildcards(0) As String
        wildcards(0) = "*.*"
        If typeListing <> "*.*" Then wildcards = typeListing.Split(New Char() {";"})

        If getFile Then
            Dim MyAttr, sAttributs() As String
            attributs = attributs Or FileAttribute.Normal
            sAttributs = Split(attributs.ToString, ", ")

            Dim myFiles As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetFiles(fullPath, FileIO.SearchOption.SearchTopLevelOnly, wildcards)
            For i As Integer = 0 To myFiles.Count - 1
                MyAttr = GetAttr(myFiles(i)).ToString.Replace(",", "").Replace(" ", "")
                MyAttr = Replace(MyAttr, "Normal", "", , , CompareMethod.Text)
                For j As Integer = 0 To sAttributs.GetUpperBound(0)
                    MyAttr = Replace(MyAttr, sAttributs(j), "", , , CompareMethod.Text)
                Next j

                If MyAttr = "" Then myArray.Add(getLastDir(myFiles(i)))
            Next i
        End If
        If getDir Then
            Dim myDirs As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetDirectories(fullPath, FileIO.SearchOption.SearchTopLevelOnly, wildcards)
            For i As Integer = 0 To myDirs.Count - 1
                myArray.Add(getLastDir(myDirs(i)))
            Next i
        End If

        Return myArray
    End Function

    Public Sub deleteFile(ByVal file As String, Optional ByVal maxDelCount As Integer = 100)
        Dim myDeleteFile As New IO.FileInfo(file)
        Dim delCount As Byte = 0
        While delCount < 100 AndAlso Fichiers.fileInUse(file) = True
            Threading.Thread.Sleep(500)
            delCount += 1
        End While

        If delCount = 100 AndAlso Fichiers.fileInUse(file) = True Then Throw New IO.IOException("Impossible de supprimer le fichier (" & file & "), car le délai d'expiration est écoulé")

        myDeleteFile.Attributes = IO.FileAttributes.Normal
        Dim deleted As Boolean = False
        While delCount < maxDelCount AndAlso deleted = False
            Try
                myDeleteFile.Delete()
                deleted = True
            Catch ex As IO.IOException 'Currently in use
            End Try
            delCount += 1
        End While
    End Sub

    Public Sub deltree(ByVal sourceD As String, Optional ByRef typeD As String = "*.*")
        Dim place As String
        Dim counter As Integer
        If Dir(sourceD, FileAttribute.Directory Or FileAttribute.Hidden) = "" Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'TODO  : REMOVED On Error Resume Next
        '
        'EXPLICATION
        '
        'SourceD = Source du répertoire à détruire
        'TypeD = Type des fichiers à détruire (Ex : *.*)

        'Changer les attributs des fichiers à Normal
        Dim myFiles() As String = IO.Directory.GetFiles(sourceD, typeD)
        Dim tries As Integer = 0
        For counter = 0 To myFiles.Length - 1
            System.IO.File.SetAttributes(myFiles(counter), IO.FileAttributes.Normal)
            Try
                IO.File.Delete(myFiles(counter))
                tries = 0
            Catch ex As System.IO.IOException
                tries += 1
                If tries < 10 Then counter -= 1
            End Try
        Next counter

        'Prise des sous-dossiers
        Dim myDirs() As String = IO.Directory.GetDirectories(sourceD)
        For counter = 0 To myDirs.Length - 1
            place = myDirs(counter)
            deltree(place, typeD)
        Next counter

        If typeD = "*.*" Then
            Dim done As Boolean = False
            Dim n As Integer = 1
            While done = False
                If n > 100 Then Exit While
                Try
                    IO.Directory.Delete(sourceD)
                Catch ex As IOException
                End Try
                done = True
            End While
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub xCopy(ByVal sourceD As String, ByVal destination As String, Optional ByRef typeD As String = "*.*", Optional ByVal copyDates As Boolean = True)
        Dim place, place2 As String
        Dim counter As Integer
        If Dir(sourceD, FileAttribute.Directory Or FileAttribute.Hidden) = "" Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'TODO  : REMOVED On Error Resume Next
        '
        'EXPLICATION
        '
        'SourceD = Source du répertoire à copier
        'Destination = ... du répertoire à copier
        'TypeD = Type des fichiers à détruire (Ex : *.*)

        ensureGoodPath(destination)
        Dim myDISource As New System.IO.DirectoryInfo(sourceD)
        Dim myDIDestination As New System.IO.DirectoryInfo(destination)
        myDIDestination.Attributes = myDISource.Attributes
        Dim myFiles As ArrayList = hddListing(sourceD, False, True, typeD)
        For counter = 0 To myFiles.Count - 1
            Dim myAttr As FileAttribute = System.IO.File.GetAttributes(sourceD & bar(sourceD) & myFiles(counter).ToString)
            FileCopy(sourceD & bar(sourceD) & myFiles(counter).ToString, destination & bar(destination) & myFiles(counter).ToString)
            System.IO.File.SetAttributes(destination & bar(destination) & myFiles(counter).ToString, myAttr)
            If copyDates = True Then
                Dim myCreationDate As Date = System.IO.File.GetCreationTime(sourceD & bar(sourceD) & myFiles(counter).ToString)
                Dim myLastAccessDate As Date = System.IO.File.GetLastAccessTime(sourceD & bar(sourceD) & myFiles(counter).ToString)
                Dim myLastWriteDate As Date = System.IO.File.GetLastWriteTime(sourceD & bar(sourceD) & myFiles(counter).ToString)
                System.IO.File.SetCreationTime(destination & bar(destination) & myFiles(counter).ToString, myCreationDate)
                System.IO.File.SetLastAccessTime(destination & bar(destination) & myFiles(counter).ToString, myLastAccessDate)
                System.IO.File.SetLastWriteTime(destination & bar(destination) & myFiles(counter).ToString, myLastWriteDate)
            End If
        Next counter

        'Prise des sous-dossiers
        Dim myDirs As ArrayList = hddListing(sourceD)
        For counter = 0 To myDirs.Count - 1
            place = sourceD & bar(sourceD) & myDirs(counter).ToString
            place2 = destination & bar(destination) & myDirs(counter).ToString
            xCopy(place, place2, typeD, copyDates)
        Next counter

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub xMove(ByVal sourceD As String, ByVal destination As String, Optional ByRef typeD As String = "*.*", Optional ByVal copyDates As Boolean = True)
        If sourceD = destination & bar(destination) & getLastDir(sourceD) Then Exit Sub
        xCopy(sourceD, destination, typeD, copyDates)
        deltree(sourceD, typeD)
    End Sub

    Public Sub fileMove(ByVal sourceFile As String, ByVal destinationFile As String)
        Dim myAttr As FileAttribute = System.IO.File.GetAttributes(sourceFile)
        FileCopy(sourceFile, destinationFile)
        System.IO.File.SetAttributes(destinationFile, myAttr)
        System.IO.File.SetAttributes(sourceFile, IO.FileAttributes.Normal)
        IO.File.Delete(sourceFile)
    End Sub

    Public Function getLastDir(ByVal myPath As String, Optional ByVal separator As Char = "\") As String
        If myPath Is Nothing Then Return ""

        If myPath.EndsWith("\") Then myPath = myPath.Substring(0, myPath.Length - 1)
        Dim myPathArray() As String = Split(myPath, separator)

        If myPathArray.GetUpperBound(0) >= 0 Then Return myPathArray(myPathArray.GetUpperBound(0))

        Return ""
    End Function

    Private lockForFileLists As New Threading.Mutex()

    Public Function addItemToAList(ByRef pathFromAppOfList As String, ByRef newEntry As String, Optional ByRef acceptDouble As Boolean = False, Optional ByVal sortFile As Boolean = False, Optional ByVal maximumItems As Integer = 0, Optional ByVal keepWhiteLine As Boolean = True) As Boolean
        Dim i As Integer
        Dim accepted As Boolean = True
        Dim file() As String

        lockForFileLists.WaitOne()

        Try
            file = readFile(pathFromAppOfList, , keepWhiteLine)
            Dim pathLength As Integer = pathFromAppOfList.Length - getLastDir(pathFromAppOfList).Length - 1
            If Not pathLength < 0 Then IO.Directory.CreateDirectory(appPath & bar(appPath) & pathFromAppOfList.Substring(0, pathLength))
            Using WriterStream As New IO.FileStream(appPath & bar(appPath) & pathFromAppOfList, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                Dim ws_Writer As New IO.StreamWriter(WriterStream)
                'LockFile(PathFromAppOfList)
                If file(0).ToUpper.StartsWith("ERROR") Then
                    file(0) = newEntry
                Else
                    If acceptDouble = False Then
                        For i = 0 To file.GetUpperBound(0)
                            If file(i).ToUpper = newEntry.ToUpper Then accepted = False : Exit For
                        Next i
                    End If

                    If accepted = True Then
                        ReDim Preserve file(file.GetUpperBound(0) + 1)
                        file(file.GetUpperBound(0)) = newEntry
                    End If
                End If

                If maximumItems > 0 And file.Length > maximumItems And accepted = True Then
                    Array.Reverse(file)
                    ReDim Preserve file(maximumItems - 1)
                    Array.Reverse(file)
                End If

                With CType(ws_Writer, IO.TextWriter)
                    .Write(String.Join(vbCrLf, file))
                    .Flush()
                    .Close()
                End With

                ws_Writer = Nothing
            End Using
        Catch ex As Exception
            addErrorLog(ex)
        Finally
            lockForFileLists.ReleaseMutex()
        End Try

        Return True
    End Function

    Public Function modifItemToAList(ByRef pathFromAppOfList As String, ByRef newEntry As String, ByRef strToSearch As String, Optional ByVal keepWhiteLine As Boolean = True) As Boolean
        Dim i As Integer
        Dim file() As String
        Dim modified As Boolean = False

        lockFile(pathFromAppOfList)
        file = readFile(pathFromAppOfList, , keepWhiteLine)
        If file(0).ToUpper.StartsWith("ERROR") Then Return False

        For i = 0 To file.GetUpperBound(0)
            If file(i).IndexOf(strToSearch) <> -1 Then modified = True : file(i) = newEntry : Exit For
        Next i

        writeFile(pathFromAppOfList, file)
        unLockFile(pathFromAppOfList)
        Return modified
    End Function

    Public Function removeItemToAList(ByVal pathFromAppOfList As String, ByVal entryToDel As String, Optional ByVal allOccurences As Boolean = True, Optional ByVal delFileIfEmpty As Boolean = False, Optional ByVal startsWith As Boolean = False, Optional ByVal keepWhiteLine As Boolean = True, Optional ByVal endsWith As Boolean = False) As Integer
        Dim path As String = appPath & bar(appPath) & pathFromAppOfList
        If IO.File.Exists(path) = False Then Return 0

        Dim newPath As String = path & ".newlist"
        Dim occ As Integer
        Dim file As New System.Text.StringBuilder()
        Dim fileStream As IO.FileStream
        Dim curLine As String = String.Empty
        Dim fileWriter As IO.StreamWriter

        Try
            fileWriter = New IO.StreamWriter(newPath)
            fileStream = IO.File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete)
            Dim fileReader As New IO.StreamReader(fileStream)

            curLine = fileReader.ReadLine()
            Dim firstLine As Boolean = True
            Dim hasContent As Boolean = False
            While curLine IsNot Nothing
                If ((Not curLine.ToUpper = entryToDel.ToUpper And startsWith = False And endsWith = False) Or (startsWith = True And endsWith = False And curLine.ToUpper.StartsWith(entryToDel.ToUpper) = False)) Or (startsWith = False And endsWith = True And curLine.ToUpper.EndsWith(entryToDel.ToUpper) = False) Or (startsWith = True And endsWith = True And curLine.ToUpper.IndexOf(entryToDel.ToUpper) < 0) Or (allOccurences = False And occ = 1) Then
                    file.Append(If(firstLine, String.Empty, vbCrLf))
                    file.Append(curLine)
                    hasContent = True
                    firstLine = False
                Else
                    If allOccurences = True Then
                        occ += 1
                    Else
                        occ = 1
                    End If
                End If

                curLine = fileReader.ReadLine()
            End While

            IO.File.Delete(path)

            If hasContent OrElse Not delFileIfEmpty Then
                fileWriter.Write(file.ToString())
                fileWriter.Flush()
            End If
        Catch ex As Exception
            addErrorLog(ex)
            Return 0
        Finally
            If fileStream IsNot Nothing Then
                fileStream.Close()
                fileStream = Nothing
            End If

            If fileWriter IsNot Nothing Then fileWriter.Close()
        End Try

        If IO.File.Exists(newPath) Then IO.File.Move(newPath, path)

        'lockFile(pathFromAppOfList)
        'File = readFile(pathFromAppOfList, , keepWhiteLine)
        'If File(0).ToUpper.StartsWith("ERROR") Then
        '    Return 0
        '    Exit Function
        'Else
        '    ReDim file2(File.GetUpperBound(0))
        '    occ = 0
        '    For i = 0 To File.GetUpperBound(0)
        '        If ((Not File(i).ToUpper = entryToDel.ToUpper And startsWith = False And endsWith = False) Or (startsWith = True And endsWith = False And File(i).ToUpper.StartsWith(entryToDel.ToUpper) = False)) Or (startsWith = False And endsWith = True And File(i).ToUpper.EndsWith(entryToDel.ToUpper) = False) Or (startsWith = True And endsWith = True And File(i).ToUpper.IndexOf(entryToDel.ToUpper) < 0) Or (allOccurences = False And occ = 1) Then
        '            file2(i - occ) = File(i)
        '        Else
        '            If allOccurences = True Then
        '                occ += 1
        '            Else
        '                occ = 1
        '            End If
        '        End If
        '    Next i

        '    If (file2.GetUpperBound(0) - occ) < 0 Then
        '        ReDim file2(0)
        '    Else
        '        ReDim Preserve file2(file2.GetUpperBound(0) - occ)
        '    End If
        'End If

        'If file2.GetUpperBound(0) = 0 And file2(0) Is Nothing And delFileIfEmpty = True Then
        '    IO.File.Delete(appPath & bar(appPath) & pathFromAppOfList)
        'Else
        '    writeFile(pathFromAppOfList, file2)
        'End If
        'unLockFile(pathFromAppOfList)

        Return occ
    End Function

    Public Sub modifFile(ByVal pathFromApp As String, ByVal indexToModif As Integer, Optional ByVal newLine As String = "", Optional ByVal sortFile As Boolean = False, Optional ByVal keepWhiteLine As Boolean = True)
        Dim myFile() As String
        lockFile(pathFromApp)
        myFile = readFile(pathFromApp, , keepWhiteLine)
        If myFile(0).ToUpper.StartsWith("ERROR") Then myFile(0) = ""
        If indexToModif > myFile.GetUpperBound(0) Then ReDim Preserve myFile(indexToModif)

        If keepWhiteLine = False And newLine = "" Then
            Dim i As Integer
            For i = indexToModif To myFile.GetUpperBound(0) - 1
                myFile(i) = myFile(i + 1)
            Next i
            ReDim Preserve myFile(myFile.GetUpperBound(0) - 1)
        Else
            myFile(indexToModif) = newLine
        End If

        writeFile(pathFromApp, myFile, sortFile)
        unLockFile(pathFromApp)
    End Sub

    Public Sub ensureGoodPath(ByVal fullPath As String)
        If Dir(fullPath, FileAttribute.Directory Or FileAttribute.Archive Or FileAttribute.Hidden Or FileAttribute.ReadOnly Or FileAttribute.System) = "" Then IO.Directory.CreateDirectory(fullPath)
    End Sub

    Public Function searchInAFile(ByVal pathFromApp As String, ByVal strToSearch As String, Optional ByVal returnIndex As Boolean = False, Optional ByVal fileMask() As Boolean = Nothing, Optional ByVal returnWhiteLine As Boolean = False) As String()
        Dim myReturn() As String = {}
        Dim n, i As Integer
        Dim myFile() As String = readFile(pathFromApp, fileMask, returnWhiteLine)

        If myFile(0).ToUpper.StartsWith("ERROR") Then Return Nothing

        n = 0
        For i = 0 To myFile.Length - 1
            If myFile(i).ToUpper.IndexOf(strToSearch.ToUpper) <> -1 Then
                ReDim Preserve myReturn(n)
                If returnIndex = False Then
                    myReturn(n) = myFile(i)
                Else
                    myReturn(n) = i
                End If
                n += 1
            End If
        Next i

        Return myReturn
    End Function

    Public Function searchFiles(ByVal firstDir As String, ByVal searchWord As String, Optional ByRef fileType As String = "*.*", Optional ByRef searchType As SType = SType.Normal, Optional ByVal maxSubDirs As Integer = -1, Optional ByVal searchInFileNames As Boolean = True, Optional ByVal returnResult As RRType = RRType.Both, Optional ByVal considerExtension As Boolean = False, Optional ByVal firstPartOfPathNotToConsider As String = "", Optional ByVal caseSensitive As Boolean = False, Optional ByVal acceptFileIfContain() As String = Nothing, Optional ByVal refuseFileIfContain() As String = Nothing, Optional ByVal attributs As FileAttribute = FileAttribute.Normal Or FileAttribute.Hidden Or FileAttribute.ReadOnly Or FileAttribute.Archive, Optional ByVal searchInFileContent As Boolean = True, Optional ByVal fileContentExt As String = "", Optional ByVal datafileType As String = "", Optional ByVal dataFileMask() As Boolean = Nothing, Optional ByVal refusedPath() As String = Nothing, Optional ByVal refuseDirPath() As String = Nothing, Optional ByVal searchInDirNames As Boolean = False, Optional ByVal trunkWhiteSpaces As Boolean = False, Optional ByVal upToFolder As Integer = 0) As ArrayList
        Dim place, MyFileContent(), MyFCStr, SExts(), SExt(), MyExt, sFile() As String
        Dim Counter, i As Integer
        Dim AcceptFile, AcceptDFExt, AcceptFCExt, refusedDir As Boolean
        Dim myRegEx As Regex
        Dim mc As MatchCollection
        Dim reOpt As RegexOptions = RegexOptions.Multiline Or RegexOptions.Singleline
        Dim searchMethod As Microsoft.VisualBasic.CompareMethod = CompareMethod.Binary

        If Dir(firstDir, FileAttribute.Directory Or FileAttribute.Hidden) = "" Then Return New ArrayList()

        If upToFolder = 0 Then foundFiles.Clear()
        attributs = attributs Or FileAttribute.Volume Or FileAttribute.Directory
        If caseSensitive = False Then reOpt = reOpt Or RegexOptions.IgnoreCase : searchMethod = CompareMethod.Text

        Try
            myRegEx = New Regex(searchWord, reOpt)
        Catch exp As Exception
            searchType = SType.Normal
        End Try

        searchWord = trunkingWhiteSpaces(searchWord, trunkWhiteSpaces)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '
        'EXPLICATION
        '
        'FirstDir = Dossier en cours pour la recherche (Dossier où commencer)
        'SearchWord = Expression à rechercher
        'FileType = Extensions acceptées
        'SearchType = Type de recherche (Normal / Expression régulière)
        'MaxSubDirs = Nombre de sous-dossier maximal qu'il est possible d'aller
        'SearchInFileNames = Recherche également dans le nom du fichier
        'ReturnResult = Possibilités : Fichier / Dossier / Les deux
        'ConsiderExtension = Considère l'extension lors de la vérification de doublet
        'FirstPartOfPathNotToConsider = Partie du chemin à ne pas considérer lors de la vérification de doublet
        'CaseSensitive = Respecte la casse (Vrai/Faux)
        'AcceptFileIfContain() = Recherche dans le fichier si contient l'un des mots
        'RefuseFileIfContain() = Ne recherche pas dans le fichier si contient l'un des mots
        'Attributs = Attributs acceptés
        'SearchInFileContent = Recherche dans le contenu des fichiers qui ne sont pas des fichiers de données
        'FileContentExt = Extension(s) acceptée(s) pour le contenu des fichiers qui ne sont pas des fichiers de données
        'DataFileType = Extension(s) des fichiers de données (Séparé par ;)
        'DataFileMask() = Masque indiquant les lignes à considérées. (Array As Boolean, Index=0 -> Default acceptation, Index>0 -> Acceptation (True/False))
        'RefusedPath() = Chemins refusés à la recherche (Uniquement ce chemin pour la recherche dans les fichiers) La recherche de ces sous-dossiers n'est pas désactivée
        'RefuseDirPath() = Si le chemin du dossier correspond, ne pas rechercher dans le nom de ce dossier
        'SearchInDirNames = Recherche également dans le nom du dossier
        'TrunkWhiteSpaces = Enlève les espaces
        'UpToDir = Ne pas passer de paramètre. Utilisation en boucle interne.

        refusedDir = False
        If Not refusedPath Is Nothing AndAlso refusedPath.Length <> 0 Then
            For i = 0 To refusedPath.GetUpperBound(0)
                If refusedPath(i) = firstDir Then refusedDir = True : Exit For
            Next i
        End If

        If returnResult <> RRType.Dirs And refusedDir = False Then
            Dim myFiles As ArrayList = hddListing(firstDir, False, True, fileType, attributs)
            For Counter = 0 To myFiles.Count - 1
                'Vérification d'acception ou d'exclusion du fichier
                If Not acceptFileIfContain Is Nothing AndAlso acceptFileIfContain.Length <> 0 Then
                    AcceptFile = False
                    For i = 0 To acceptFileIfContain.GetUpperBound(0)
                        If InStr(myFiles(Counter), acceptFileIfContain(i), searchMethod) > 0 Then AcceptFile = True : Exit For
                    Next i
                Else
                    AcceptFile = True
                End If
                If AcceptFile = False Then GoTo NextFile
                If Not refuseFileIfContain Is Nothing AndAlso refuseFileIfContain.Length <> 0 Then
                    For i = 0 To refuseFileIfContain.GetUpperBound(0)
                        If InStr(myFiles(Counter), refuseFileIfContain(i), searchMethod) > 0 Then GoTo nextfile
                    Next i
                End If

                AcceptDFExt = False
                If datafileType <> "" Then
                    SExts = datafileType.Split(New Char() {";"})
                    sFile = myFiles(Counter).ToString.Split(New Char() {"."})
                    For Each MyExt In SExts
                        SExt = MyExt.Split(New Char() {"."})
                        If MyExt = "*.*" Then
                            AcceptDFExt = True : Exit For
                        Else
                            If sFile.Length > 1 Then If SExt(1).ToUpper = sFile(1).ToUpper Then AcceptDFExt = True : Exit For
                        End If
                    Next MyExt
                End If

                If AcceptDFExt = False Then
                    AcceptFCExt = False
                    If fileContentExt <> "" Then
                        SExts = fileContentExt.Split(New Char() {";"})
                        sFile = myFiles(Counter).ToString.Split(New Char() {"."})
                        For Each MyExt In SExts
                            If MyExt.EndsWith("*") Then
                                AcceptFCExt = True : Exit For
                            Else
                                If sFile.Length > 1 Then If MyExt.ToUpper = sFile(1).ToUpper Then AcceptFCExt = True : Exit For
                            End If
                        Next MyExt
                    Else
                        AcceptFCExt = True
                    End If

                    If searchInFileContent = True And AcceptFCExt = True Then
                        MyFileContent = readFile(Right(firstDir, firstDir.Length - (appPath.Length + bar(appPath).Length)) & bar(firstDir) & myFiles(Counter).ToString)
                        MyFCStr = String.Join(vbCrLf, MyFileContent)
                        If searchType = SType.Normal Then
                            If InStr(trunkingWhiteSpaces(MyFCStr, trunkWhiteSpaces), searchWord, searchMethod) > 0 Then foundAFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, considerExtension, firstPartOfPathNotToConsider)
                        Else
                            mc = myRegEx.Matches(trunkingWhiteSpaces(MyFCStr, trunkWhiteSpaces))
                            If mc.Count > 0 Then foundAFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, considerExtension, firstPartOfPathNotToConsider)
                        End If
                    ElseIf AcceptFCExt And fileContentExt <> "" Then
                        foundAFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, considerExtension, firstPartOfPathNotToConsider)
                    End If
                Else
                    Dim extensionPos As Byte = 0
                    If dataFileMask(1) = True Then extensionPos += 1
                    If dataFileMask(2) = True Then extensionPos += 1
                    dataFileMask(3) = True

                    MyFileContent = readFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, dataFileMask, , False)

                    AcceptFCExt = False
                    If fileContentExt <> "" Then
                        SExts = fileContentExt.Split(New Char() {";"})
                        sFile = MyFileContent(extensionPos).ToString.Split(New Char() {"."})
                        For Each MyExt In SExts
                            If MyExt.EndsWith("*") Then
                                AcceptFCExt = True : Exit For
                            Else
                                If sFile.Length > 1 Then If MyExt.ToUpper = sFile(1).ToUpper Then AcceptFCExt = True : Exit For
                            End If
                        Next MyExt
                    Else
                        AcceptFCExt = True
                    End If
                    MyFileContent(extensionPos) = ""

                    If AcceptFCExt Then 'S'il s'agit du type de fichier demandé
                        MyFCStr = String.Join(vbCrLf, MyFileContent)
                        If searchType = SType.Normal Then
                            If InStr(trunkingWhiteSpaces(MyFCStr, trunkWhiteSpaces), searchWord, searchMethod) > 0 Then foundAFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, considerExtension, firstPartOfPathNotToConsider)
                        Else
                            mc = myRegEx.Matches(trunkingWhiteSpaces(MyFCStr, trunkWhiteSpaces))
                            If mc.Count > 0 Then foundAFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, considerExtension, firstPartOfPathNotToConsider)
                        End If
                    End If

                    If AcceptFCExt And searchInFileNames = True Then
                        If searchType = SType.Normal Then
                            If InStr(trunkingWhiteSpaces(myFiles(Counter), trunkWhiteSpaces), searchWord, searchMethod) > 0 Then foundAFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, considerExtension, firstPartOfPathNotToConsider) : GoTo NextFile
                        Else
                            mc = myRegEx.Matches(trunkingWhiteSpaces(myFiles(Counter), trunkWhiteSpaces))
                            If mc.Count > 0 Then foundAFile(firstDir & bar(firstDir) & myFiles(Counter).ToString, considerExtension, firstPartOfPathNotToConsider) : GoTo NextFile
                        End If
                    End If
                End If
NextFile:
            Next Counter
        End If

        'Prise des sous-dossiers
        Dim myDirs As ArrayList = hddListing(firstDir, , , , attributs)
        For Counter = 0 To myDirs.Count - 1
            place = firstDir & bar(firstDir) & myDirs(Counter).ToString

            refusedDir = False
            If Not refuseDirPath Is Nothing AndAlso refuseDirPath.Length <> 0 Then
                For i = 0 To refuseDirPath.GetUpperBound(0)
                    If refuseDirPath(i) = place Then refusedDir = True : Exit For
                Next i
            End If

            If Not refusedPath Is Nothing AndAlso refusedPath.Length <> 0 Then
                For i = 0 To refusedPath.GetUpperBound(0)
                    If refusedPath(i) = firstDir Then refusedDir = True : Exit For
                Next i
            End If

            If searchInDirNames = True And Not returnResult = RRType.Files And refusedDir = False Then
                If searchType = SType.Normal Then
                    If InStr(trunkingWhiteSpaces(myDirs(Counter), trunkWhiteSpaces), searchWord, searchMethod) > 0 Then foundAFile(place, True, firstPartOfPathNotToConsider)
                Else
                    mc = myRegEx.Matches(trunkingWhiteSpaces(myDirs(Counter), trunkWhiteSpaces))
                    If mc.Count > 0 Then foundAFile(place, True, firstPartOfPathNotToConsider)
                End If
            End If

            If ((upToFolder + 1) <= maxSubDirs Or maxSubDirs = -1) Then searchFiles(place, searchWord, fileType, searchType, maxSubDirs, searchInFileNames, returnResult, considerExtension, firstPartOfPathNotToConsider, caseSensitive, acceptFileIfContain, refuseFileIfContain, attributs, searchInFileContent, fileContentExt, datafileType, dataFileMask, refusedPath, refuseDirPath, searchInDirNames, trunkWhiteSpaces, upToFolder + 1)
        Next Counter

        If upToFolder = 0 Then Return foundFiles
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Return New ArrayList
    End Function

    Private Sub foundAFile(ByVal fullPath As String, ByVal considerExtension As Boolean, ByVal firstPartOfPathNotToConsider As String)
        Dim i, j, nbFPOPNTC As Integer
        Dim Splitting(), Splitting2(), MyFFPath, MyPath, myExt As String
        MyPath = fullPath

        If considerExtension = False And getLastDir(MyPath).IndexOf(New Char() {"."}) <> -1 Then
            Splitting = MyPath.Split(New Char() {"."})
            myExt = Splitting(Splitting.GetUpperBound(0))
            ReDim Preserve Splitting(Splitting.GetUpperBound(0) - 1)
            MyPath = String.Join(".", Splitting)
        End If

        If firstPartOfPathNotToConsider <> "" Then
            Splitting = MyPath.Split(New Char() {"\"})
            nbFPOPNTC = firstPartOfPathNotToConsider.Split(New Char() {"\"}).Length
            ReDim Splitting2(Splitting.Length - nbFPOPNTC - 1)
            For j = 0 To Splitting2.Length - 1
                Splitting2(j) = Splitting(nbFPOPNTC + j)
            Next j
            MyPath = String.Join("\", Splitting2)
        End If

        'Vérification d'un doublet
        For i = 0 To foundFiles.Count - 1
            MyFFPath = foundFiles(i)

            If considerExtension = False And getLastDir(MyFFPath).IndexOf(".") <> -1 Then
                Splitting = MyFFPath.Split(New Char() {"."})
                ReDim Preserve Splitting(Splitting.GetUpperBound(0) - 1)
                MyFFPath = String.Join(".", Splitting)
            End If

            If firstPartOfPathNotToConsider <> "" Then
                Splitting = MyFFPath.Split(New Char() {"\"})
                nbFPOPNTC = firstPartOfPathNotToConsider.Split(New Char() {"\"}).Length
                ReDim Splitting2(Splitting.Length - nbFPOPNTC - 1)
                For j = 0 To Splitting2.Length - 1
                    Splitting2(j) = Splitting(nbFPOPNTC + j)
                Next j
                MyFFPath = String.Join("\", Splitting2)
            End If

            If MyPath.ToUpper = MyFFPath.ToUpper Then Exit Sub
        Next i

        fullPath = fullPath.Replace("DB\Content", "DB\DB")
        If considerExtension = False Then fullPath = fullPath.Replace(myExt, "DB")
        foundFiles.Add(fullPath)
    End Sub

    Public Function importer(ByVal newFileNameWOExt As String, ByVal importToPathFromApp As String, Optional ByVal initDirLine As Integer = 2, Optional ByVal extensions As String = "", Optional ByVal oldFileName As String = "", Optional ByVal withOutExtension As Boolean = False) As String
        Dim myCopyToFile As IO.FileInfo

        Try
            Dim myExts() As String = extensions.Split(New Char() {";"})
            Dim MyExtStr, InitDir, theExt As String
            Dim allExts As String = ""
            Dim i As Integer

            Try
                If extensions <> "" Then
                    For i = 0 To myExts.GetUpperBound(0)
                        allExts &= ";*." & myExts(i)
                        myExts(i) = "Fichiers " & myExts(i) & "|*." & myExts(i)
                    Next i
                    allExts = allExts.Substring(1)

                    MyExtStr = "Tous|" & allExts & "|" & String.Join("|", myExts)
                Else
                    MyExtStr = TypesFilesManager.getGeneralFiltering()
                End If
            Catch
                MyExtStr = "Tous les fichiers|*.*"
            End Try

            myMainWin.FDialog.Reset()
            myMainWin.FDialog.Filter = MyExtStr
            myMainWin.FDialog.FilterIndex = 1
            If infoDivers.Length > initDirLine - 1 Then
                InitDir = infoDivers(initDirLine, 0)
            Else
                InitDir = "C:\"
            End If
            myMainWin.FDialog.InitialDirectory = InitDir
            myMainWin.FDialog.ShowDialog()

            If myMainWin.FDialog.FileName = "" Then Return ""
            If IO.File.Exists(myMainWin.FDialog.FileName) = False Then Return ""

            Dim myName As String
            If initDirLine = 2 Then
                myName = "DB"
            Else
                myName = "Comm"
            End If

            Dim myImportFile As New System.IO.FileInfo(myMainWin.FDialog.FileName)
            DBLinker.getInstance.updateDB("InfoLogicielDivers", "CheminImporter" & myName & "='" & myImportFile.DirectoryName.Replace("'", "''") & "'")
            If infoDivers.Length > initDirLine - 1 Then infoDivers(initDirLine, 0) = myImportFile.DirectoryName

            If oldFileName <> "" Then
                If IO.File.Exists(appPath & bar(appPath) & importToPathFromApp & bar(importToPathFromApp) & oldFileName) And oldFileName <> (newFileNameWOExt & myImportFile.Extension) Then
                    Try
                        deleteFile(appPath & bar(appPath) & importToPathFromApp & bar(importToPathFromApp) & oldFileName)
                    Catch ex As IOException
                    End Try
                End If
            End If

            If withOutExtension = False Then theExt = myImportFile.Extension

            IO.Directory.CreateDirectory(appPath & bar(appPath) & importToPathFromApp)
            Dim fileAlreadyExists As Boolean = False
            Try
                If IO.File.Exists(appPath & bar(appPath) & importToPathFromApp & bar(importToPathFromApp) & newFileNameWOExt & theExt) Then
                    fileAlreadyExists = True
                    myCopyToFile = New IO.FileInfo(appPath & bar(appPath) & importToPathFromApp & bar(importToPathFromApp) & newFileNameWOExt & theExt)
                    If myCopyToFile.Exists Then myCopyToFile.Attributes = IO.FileAttributes.Normal
                End If
            Catch 'REM Exception not handle
            Finally
                myCopyToFile = Nothing
            End Try

            'REM An error can happen here of File already in use (New file)
            Try
                myImportFile.CopyTo(appPath & bar(appPath) & importToPathFromApp & bar(importToPathFromApp) & newFileNameWOExt & theExt, True)
            Catch ex As Exception 'REM Exception not handle
                addErrorLog(New Exception("fileAlreadyExists=" & fileAlreadyExists, ex))
            End Try

            Select Case CStr(PreferencesManager.getUserPreferences()("ImportOriginFileAction")).Substring(0, 2).ToUpper
                Case "DE"
                    Dim myMsgBox1 As New MsgBox1()
                    If myMsgBox1.Message("Que désirez-vous faire avec le fichier d'origine ?", "Fichier d'origine", 2, "Supprimer le fichier", "Laisser le fichier intact", , False) = 1 Then
                        Try
                            deleteFile(myMainWin.FDialog.FileName)
                        Catch ioEx As IO.IOException
                            MessageBox.Show("Le fichier d'origine est présentement en cours d'utilisation." & vbCrLf & myMainWin.FDialog.FileName & vbCrLf & vbCrLf & "Il fut impossible de le supprimer. Veuillez le faire manuellement.", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ioEx As Exception
                            addErrorLog(ioEx)
                            MessageBox.Show("Le fichier d'origine est présentement en cours d'utilisation." & vbCrLf & myMainWin.FDialog.FileName & vbCrLf & vbCrLf & "Il fut impossible de le supprimer. Veuillez le faire manuellement.", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If

                Case "DÉ"
                    Try
                        deleteFile(myMainWin.FDialog.FileName)
                    Catch ioEx As IO.IOException
                        MessageBox.Show("Le fichier d'origine est présentement en cours d'utilisation." & vbCrLf & myMainWin.FDialog.FileName & vbCrLf & vbCrLf & "Il fut impossible de le supprimer. Veuillez le faire manuellement.", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ioEx As Exception
                        addErrorLog(ioEx)
                        MessageBox.Show("Le fichier d'origine est présentement en cours d'utilisation." & vbCrLf & myMainWin.FDialog.FileName & vbCrLf & vbCrLf & "Il fut impossible de le supprimer. Veuillez le faire manuellement.", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
            End Select

            Return newFileNameWOExt & theExt
        Catch exp As Exception 'REM Exception not handle
            MessageBox.Show("Une erreur est survenue. Veuillez fermer la fenêtre, la rouvrir et réessayer.", "Erreur inattendue")
            addErrorLog(exp)
            Return ""
        Finally

        End Try
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Function fileInUse(ByVal sFile As String) As Boolean
        If System.IO.File.Exists(sFile) Then
            Try
                Dim f As Short = FreeFile()
                FileOpen(f, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FileClose(f)
            Catch
                Return True
            End Try
        End If

        Return False
    End Function

    Public Function transformFileSizeToText(ByVal nbBytes As Long) As String
        Dim test, test2 As Double
        test = nbBytes / 1024
        If test < 1 Then Return nbBytes & " octets"

        test2 = test
        test = test / 1024
        If test < 1 Then Return Math.Round(test2, 2) & " Ko"

        test2 = test
        test = test / 1024
        If test < 1 Then Return Math.Round(test2, 2) & " Mo"

        test2 = test
        test = test / 1024
        If test < 1 Then Return Math.Round(test2, 2) & " Go"

        test2 = test
        test = test / 1024
        If test < 1 Then Return Math.Round(test2, 2) & " To"

        Return nbBytes & " octets"
    End Function

End Module
