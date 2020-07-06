Namespace Creator


    Friend Class ExternalUpdateClient
        Inherits ExternalUpdateType

        Private Const TABLENAME As String = "updateClient"
        Private filesToCopy As New Dictionary(Of String, Dictionary(Of IO.FileInfo, IO.FileInfo))
        Private filesToDelete As New Dictionary(Of String, List(Of String))
        Private hasToRecompare As New Dictionary(Of String, Boolean)
        Private spinWait As Boolean = False

        Public Sub New(ByVal data As DataTable)
            MyBase.New(data)
        End Sub

        Protected Overloads Shared Sub ensureRequiredColumnsExist(ByVal data As DataTable)
            Dim columnsToCheck() As String = New String() {"CompareFolder", "DevFolder"}

            For Each curColumn As String In columnsToCheck
                If data.Columns.Contains(curColumn) = False Then onStopCreation("The column """ & curColumn & """ in the table """ & data.TableName & """ is required. Ensure is presence please.")
            Next

            ExternalUpdateType.ensureRequiredColumnsExist(data)
        End Sub

        Public Overloads Shared Function getUpdateClass(ByVal data As DataTable) As ExternalUpdateType
            If data.TableName <> TABLENAME Then Return ExternalUpdateData.getUpdateClass(data)

            ensureRequiredColumnsExist(data)

            Return New ExternalUpdateClient(data)
        End Function

        Protected Overloads Overrides Function isUpdateExists(ByVal curUpdateRow As System.Data.DataRow) As Boolean
            Console.WriteLine("Ensure update exists...")
            'Looks for files to copy
            Console.WriteLine("Verification of new files")
            Dim newDir As String = getFolderOrFilePath(curUpdateRow, "NewVersionFolder")
            Dim n As Integer = IO.Directory.GetFiles(newDir, "*.*", IO.SearchOption.AllDirectories).Length
            Dim softKey As String = curUpdateRow("softKey")
            If hasToRecompare.ContainsKey(softKey) = False Then hasToRecompare.Add(softKey, True)

            If Not filesToCopy.ContainsKey(softKey) Then filesToCopy(softKey) = New Dictionary(Of IO.FileInfo, IO.FileInfo)
            If Not filesToDelete.ContainsKey(softKey) Then filesToDelete(softKey) = New List(Of String)

            If n = 0 Then
                Dim compareDir As String = getFolderOrFilePath(curUpdateRow, "CompareFolder")
                Dim devDir As String = getFolderOrFilePath(curUpdateRow, "DevFolder")
                Console.WriteLine("Comparaison of files")
                n = compareFiles(compareDir, devDir, filesToCopy(softKey), filesToDelete(softKey))
                hasToRecompare(softKey) = n = 0
            End If

            Console.Clear()

            If filesToCopy(softKey).Count = 0 And n <> 0 And hasToRecompare(softKey) = False Then Return False

            Return True
        End Function


        Private Function compareFiles(ByVal compareDir As String, ByVal devDir As String, ByRef filesToCopy As Dictionary(Of IO.FileInfo, IO.FileInfo), ByRef filesToDelete As List(Of String)) As Integer
            Dim n As Integer
            devDir &= IIf(devDir.EndsWith("\"), "", "\")
            compareDir &= IIf(compareDir.EndsWith("\"), "", "\")
            For Each curFilepath As String In IO.Directory.GetFiles(compareDir, "*.*", IO.SearchOption.AllDirectories)
                Dim curBaseFile As New IO.FileInfo(curFilepath)
                Dim relativeFile As String = curFilepath.Substring(compareDir.Length)
                Dim curDevFile As New IO.FileInfo(devDir & relativeFile)
                If curDevFile.Exists Then
                    Console.WriteLine("Comparaison of : " & curDevFile.FullName)
                    If compareFile(curBaseFile.FullName, curDevFile.FullName) = False Then
                        filesToCopy.Add(curDevFile, curBaseFile)
                    End If
                    n += 1
                Else
                    filesToDelete.Add(curDevFile.FullName)
                    'TODO : Shall activate next line when ExternalUpdate.download manages deleted files
                    'n += 1
                End If
            Next

            Return n
        End Function

        Private Function compareFile(ByVal cheminSource As String, ByVal cheminCible As String) As Boolean
            Dim isIdentical As Boolean = True

            Dim firstFile As New IO.FileStream(cheminSource, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            Dim secondFile As New IO.FileStream(cheminCible, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            If firstFile.Length = secondFile.Length Then
                Dim compared As Boolean = True
                Dim firstBuffer(firstFile.Length) As Byte
                Dim secondBuffer(secondFile.Length) As Byte
                Dim curPos As Long = 0
                Dim firstLength As Long = firstFile.Length
                Dim moding As Long = firstLength / 1024 / 100
                If moding = 0 Then moding = 1
                For i As Integer = 0 To firstLength - 1 Step 1024
                    If i Mod moding = 0 Then
                        If i = 0 Then Console.Write("0") Else Console.Write((i + 1) / firstLength * 100)
                        Console.WriteLine(" %")
                    End If
                    Dim nbToRead As Integer = 1024
                    If (curPos + 1024) > firstLength Then nbToRead = firstLength - curPos
                    Dim nbFirstRead As Integer = firstFile.Read(firstBuffer, curPos, nbToRead)
                    Dim nbSecondRead As Integer = secondFile.Read(secondBuffer, curPos, nbToRead)
                    compared = compareData(firstBuffer, curPos + nbToRead, secondBuffer, curPos + nbToRead, curPos)
                    curPos += 1024

                    If compared = False Then
                        isIdentical = False
                        Exit For
                    End If
                Next i
            Else
                isIdentical = False
            End If

            firstFile.Close()
            secondFile.Close()

            Return isIdentical
        End Function

        Private Function compareData(ByVal buf1() As Byte, ByVal len1 As Integer, ByVal buf2() As Byte, ByVal len2 As Integer, Optional ByVal starting As Long = 0) As Boolean
            ' Use this method to compare data from two different buffers.
            If len1 <> len2 Then
                '           MsgBox("Number of bytes in two buffer are different" & len1 & ":" & len2)
                Return False
            End If

            Dim i As Integer
            For i = starting To len1 - 1
                If buf1(i) <> buf2(i) Then
                    '                MsgBox("byte " & i & " is different " & buf1(i) & "|" & buf2(i))
                    Return False
                End If
            Next i
            'MsgBox("All bytes compare.")
            Return True
        End Function 'CompareData

        Private Sub changeAssemblyFile(ByVal curUpdateRow As DataRow, ByVal webVersions As System.Collections.Generic.Dictionary(Of String, Integer))
            'Ajust assemblyinfo file
            Dim assemblyFilePath As String = getFolderOrFilePath(curUpdateRow, "AssemblyInfoFile")
            If assemblyFilePath = "" Then Exit Sub
            If IO.File.Exists(assemblyFilePath) = False Then Exit Sub

            Dim softKey As String = curUpdateRow("SoftKey")
            Console.Clear()
            Console.WriteLine("Adjust version numbers for " & getSoftwareName(softKey))

            Dim assemblyFileLines() As String = IO.File.ReadAllLines(assemblyFilePath)
            Dim versionLineIndex As Integer = -1
            Dim tagBegin As String = "<Assembly: AssemblyVersion("
            Dim curVer As String = ""
            Dim i As Integer = -1
            For Each assemblyLine As String In assemblyFileLines
                i += 1
                assemblyLine = assemblyLine.Trim()
                If assemblyLine.StartsWith(tagBegin) = False Then Continue For

                versionLineIndex = i
                assemblyLine = assemblyLine.Substring(tagBegin.Length)

                Dim curVerIndexEnd As Integer = assemblyLine.IndexOf(")>")
                curVer = assemblyLine.Substring(0, curVerIndexEnd)
                curVer = curVer.Replace("""", "")
                Exit For
            Next

startBackVer:
            Console.WriteLine()
            Console.WriteLine("Please enter the new version (2 divisions) :")
            Dim textShowed As String = curVer.Substring(0, curVer.LastIndexOf(".", curVer.LastIndexOf(".") - 1))
            System.Windows.Forms.SendKeys.SendWait(textShowed)
            Console.SetCursorPosition(0, Console.CursorTop)
            Dim typed As String = Console.ReadLine()

            If System.Text.RegularExpressions.Regex.IsMatch(typed, "^[0-9]+\.[0-9]+$") = False Then
                Console.Clear()
                Console.WriteLine("Veuillez entrer un numéro de version correcte... (1.1)...")
                GoTo startBackVer
            End If

            'Rewrite assemblyinfo.vb file
            Dim newVer As String = typed & "." & Date.Today.Year.ToString.Substring(2, 2) & IIf(Date.Today.Month < 10, "0", "") & Date.Today.Month & "." & webVersions(softKey)

            newVer = "<Assembly: AssemblyVersion(""" & newVer & """)>"

            'Tag not found, so create a new line to insert it
            If versionLineIndex = -1 Then
                ReDim Preserve assemblyFileLines(assemblyFileLines.Length)
                versionLineIndex = assemblyFileLines.Length - 1
            End If

            assemblyFileLines(versionLineIndex) = newVer
            IO.File.WriteAllLines(assemblyFilePath, assemblyFileLines)
        End Sub

        Private Sub writeDot()
            While spinWait
                Console.Write(".")
                Threading.Thread.Sleep(500)
            End While
        End Sub

        Private Sub doCompilation(ByVal solutionFile As String, ByVal devPath As String, ByVal msBuildPath As String)
            Console.WriteLine("Compiling...")

            'Compile EXE
            'TODO : Compilation : Shall try this code.. doesn't seems to work anymore.. maybe add config to file too.

            Dim waitThread As New Threading.Thread(AddressOf writeDot)
            spinWait = True
            waitThread.Start()
            Dim logFilePath As String = devPath & addSlash(devPath) & "compile-log.txt"
            msBuildPath = msBuildPath & addSlash(msBuildPath) & "msbuild.exe"
            'TODO : MsBuild options could be externalized --> though if not present default ones. If set, then add default configs not present
            Dim cmdLine As String = """" & msBuildPath & """ /noconlog /t:rebuild /p:Configuration=Debug;Platform=x86 /fl ""/flp:LogFile=" & logFilePath & """ """ & solutionFile & """"
            Shell(cmdLine, AppWinStyle.NormalNoFocus, True)

            Threading.Thread.Sleep(1000)
            spinWait = False
        End Sub

        Private Sub copyFiles(ByVal softKey As String, ByVal newVersionFolder As String, ByVal devDir As String)
            devDir &= If(devDir.EndsWith("\"), "", "\")
            newVersionFolder &= If(newVersionFolder.EndsWith("\"), "", "\")

            IO.Directory.CreateDirectory(newVersionFolder)
            Console.Write(".")

            'Copy files (Clinica + linked files)
            For Each curVerFile As IO.FileInfo In filesToCopy(softKey).Keys
                curVerFile = New IO.FileInfo(curVerFile.FullName)
                Dim relativeFile As String = curVerFile.FullName.Substring(devDir.Length)
                Dim newFile As New IO.FileInfo(newVersionFolder & relativeFile)
                IO.Directory.CreateDirectory(newFile.DirectoryName)
                curVerFile.CopyTo(newVersionFolder & relativeFile, True)
                Console.Write(".")

                addFileToList(FileActions.Add, newVersionFolder & relativeFile, newVersionFolder, newVersionFolder, curVerFile.Length, True, "Binary")
            Next
        End Sub

        Protected Overrides Sub doPreCopyActions(ByVal curUpdateRow As System.Data.DataRow, ByVal webVersions As System.Collections.Generic.Dictionary(Of String, Integer))
            Dim softKey As String = curUpdateRow("SoftKey")
            Dim newVersionFolder As String = getFolderOrFilePath(curUpdateRow, "UpdatesFolder")
            newVersionFolder &= IIf(newVersionFolder.EndsWith("\"), "", "\") & webVersions(softKey)

            changeAssemblyFile(curUpdateRow, webVersions)

            Console.WriteLine("Creation of the update #" & webVersions(softKey))

            Dim solutionFile As String = getFolderOrFilePath(curUpdateRow, "SolutionFile")
            Dim compareDir As String = getFolderOrFilePath(curUpdateRow, "CompareFolder")
            Dim devDir As String = getFolderOrFilePath(curUpdateRow, "DevFolder")
            Dim msBuildPath As String = getFolderOrFilePath(curUpdateRow, "MsBuildPath")

            If solutionFile <> String.Empty Then doCompilation(solutionFile, devDir, msBuildPath)

            If hasToRecompare(softKey) Then
                compareFiles(compareDir, devDir, filesToCopy(softKey), filesToDelete(softKey))
            End If

            copyFiles(softKey, newVersionFolder, devDir)
            addDeletedFiles(softKey, newVersionFolder)
        End Sub

        Private Sub addDeletedFiles(ByVal softKey As String, ByVal newVersionFolder As String)
            For Each curDeletedFile As String In filesToDelete(softKey)
                Dim filename As String = curDeletedFile.Substring(curDeletedFile.LastIndexOf("\") + 1)
                addFileToList(FileActions.Delete, newVersionFolder & "\" & filename, newVersionFolder, newVersionFolder, 0, True, "Binary")
            Next
        End Sub

        Protected Overloads Overrides Sub clean(ByVal curUpdateRow As System.Data.DataRow)
            'Copy clients EXE supporting files to image folder
            Dim compareDir As String = getFolderOrFilePath(curUpdateRow, "CompareFolder")
            Dim devDir As String = getFolderOrFilePath(curUpdateRow, "DevFolder")
            devDir &= IIf(devDir.EndsWith("\"), "", "\")
            compareDir &= IIf(compareDir.EndsWith("\"), "", "\")

            For Each curFile As String In IO.Directory.GetFiles(compareDir, "*.*", IO.SearchOption.AllDirectories)
                IO.File.Copy(devDir & curFile.Substring(compareDir.Length), curFile, True)
            Next
        End Sub

        Protected Overrides ReadOnly Property uploadTypeFolder() As String
            Get
                Return "client"
            End Get
        End Property
    End Class


End Namespace