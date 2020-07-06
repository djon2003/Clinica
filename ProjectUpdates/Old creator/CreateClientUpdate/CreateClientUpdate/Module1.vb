Module Module1

    Const BaseFrameworkPath As String = "C:\WINDOWS\Microsoft.NET\Framework"
    Private spinWait As Boolean = False

    Private basePath As String = ""

    Sub main()
        basePath = My.Application.Info.DirectoryPath
        basePath &= IIf(basePath.EndsWith("\"), "", "\")

        Dim action As String = ""
        If My.Application.CommandLineArgs.Contains("/clean") Then action = "clean"
        If My.Application.CommandLineArgs.Contains("/restore") Then action = "restore"

        Select Case action
            Case "clean"
                Clean()
            Case "restore"
                Restore()
            Case Else
                Create()
        End Select

        If My.Application.CommandLineArgs.Contains("/noend") = False Then Console.ReadLine()
    End Sub

    Private Sub restore()

    End Sub

    Private Sub clean()
        'Copy clients EXE supporting files to image folder
        For Each curFile As String In IO.Directory.GetFiles("Updates\client2")
            IO.File.Copy(curFile.Substring(curFile.LastIndexOf("\") + 1), curFile, True)
        Next

        My.Computer.FileSystem.DeleteDirectory(basePath & "Updates\client2\new", FileIO.DeleteDirectoryOption.DeleteAllContents)
        IO.Directory.CreateDirectory(basePath & "Updates\client2\new")
    End Sub

    Private Function compareFiles(ByRef filesToCopy, ByRef filesToDelete) As Integer
        Dim n As Integer
        For Each curFilepath As String In IO.Directory.GetFiles(basePath & "Updates\client2")
            Dim curVerFile As New IO.FileInfo(curFilepath)
            Dim curFile As New IO.FileInfo(curFilepath.Substring(curFilepath.LastIndexOf("\") + 1))
            If curFilepath.IndexOf("html") <> -1 Then
                Dim a As Byte = 0
            End If
            If curFile.Exists Then
                Console.WriteLine("Comparaison de : " & curFile.FullName)
                If CompareFile(curVerFile.FullName, curFile.FullName) = False Then
                    filesToCopy.Add(curFile, curVerFile)
                Else
                    filesToDelete.Add(curFile.Name)
                End If
                n += 1
            Else
                filesToCopy.Add(curFile, curVerFile)
            End If
        Next

        Return n
    End Function

    Private Sub doCompilation()
        'Ask if compile
        Console.Write("Voulez-vous compiler ? (O/N)")
        Dim myKey As String = Console.ReadLine.ToLower
        If Not myKey.StartsWith("o") OrElse Not myKey.StartsWith("y") Then Exit Sub

        'Compile EXE
        Dim waitThread As New Threading.Thread(AddressOf WriteDot)
        SpinWait = True
        waitThread.Start()
        Dim compilerPath As String = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\MSEnvCommunityContent\ContentTypes\Code Snippet\ContentHosts\1.0\Visual Basic 2008 Express Edition").GetValue("ApplicationPath")
        Environment.SetEnvironmentVariable("PATH", "", EnvironmentVariableTarget.Process)
        Shell(compilerPath & " """ & basePath & "Physio.sln"" /rebuild debug /log """ & basePath & "compileClinica.txt""", AppWinStyle.NormalNoFocus, True)
        Threading.Thread.Sleep(1000)
        SpinWait = False
    End Sub


    Private Sub create()
        Console.WriteLine("Vérification si version différente...")
        Dim filesList As String = ""
        Dim filesToCopy As New Hashtable
        Dim filesToDelete As New List(Of String)
        Dim hasToRecompare As Boolean = True
        'Looks for files to copy
        Console.WriteLine("Vérification pour les nouveaux fichiers")
        Dim n As Integer = IO.Directory.GetFiles(basePath & "Updates\client2\new", "*.*", IO.SearchOption.AllDirectories).Length
        If n = 0 Then
            Console.WriteLine("Comparaison des fichiers")
            n = CompareFiles(filesToCopy, filesToDelete)
            If n <> 0 Then hasToRecompare = False
        End If

        Console.Clear()

        If filesToCopy.Count = 0 And n <> 0 And hasToRecompare = False Then
            Console.WriteLine("Mise à jour client déjà OK")
            If My.Application.CommandLineArgs.Count = 0 Then Console.ReadLine()
            Exit Sub
        End If

        Console.WriteLine("Ajuste les numéros versions")
        'Ajust assemblyinfo file
        Dim assemblyFile As String = IO.File.ReadAllText(basePath & "assemblyinfo.vb")
        Dim tagBegin As String = "<Assembly: AssemblyVersion("
        Dim curVerIndexStart As Integer = assemblyFile.IndexOf(tagBegin)
        Dim curVerIndexEnd As Integer = assemblyFile.IndexOf(")>", curVerIndexStart)
        Dim curVer As String = assemblyFile.Substring(curVerIndexStart + tagBegin.Length + 1, curVerIndexEnd - curVerIndexStart - tagBegin.Length - 2)

startBackVer:
        Console.WriteLine()
        Console.WriteLine("Veuillez entrer le numéro de version (2 divisions) :")
        Dim textShowed As String = curVer.Substring(0, curVer.LastIndexOf(".", curVer.LastIndexOf(".") - 1))
        System.Windows.Forms.SendKeys.SendWait(textShowed)
        Console.SetCursorPosition(0, Console.CursorTop)
        Dim typed As String = Console.ReadLine()

        If System.Text.RegularExpressions.Regex.IsMatch(typed, "[0-9]+\.[0-9]+") = False Then
            Console.Clear()
            Console.WriteLine("Veuillez entrer un numéro de version correcte... (1.1)...")
            GoTo startBackVer
        End If

        'Ajust current.version file
        Dim startingAddress As String = "http://clinica.cints.net/updates/"
        Dim wc As New System.Net.WebClient()
        Dim curUpdateVer As String = wc.DownloadString(StartingAddress & "current.version")
        wc.Dispose()
        curUpdateVer += 1
        IO.File.WriteAllText(basePath & "Updates\current.version", curUpdateVer)

        'Rewrite assemblyinfo.vb file
        Dim newVer As String = typed & "." & Date.Today.Year.ToString.Substring(2, 2) & IIf(Date.Today.Month < 10, "0", "") & Date.Today.Month & "." & curUpdateVer
        assemblyFile = assemblyFile.Replace("<Assembly: AssemblyVersion(""" & curVer & """)>", "<Assembly: AssemblyVersion(""" & newVer & """)>")
        IO.File.WriteAllText(basePath & "AssemblyInfo.vb", assemblyFile)

        Console.WriteLine("Crée la mise à jour " & newVer)

        doCompilation()

        If hasToRecompare Then CompareFiles(filesToCopy, filesToDelete)

        'Delete old client versions
        If IO.Directory.GetDirectories("Updates\client2").Length <> 0 Then
            For Each curDir As String In IO.Directory.GetDirectories("Updates\client2")
                If curDir.EndsWith("\new") = False Then My.Computer.FileSystem.DeleteDirectory(curDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Next
        End If

        Dim updateDir As String = basePath & "Updates\client2\" & curUpdateVer
        IO.Directory.CreateDirectory(updateDir)
        Console.Write(".")

        'Add files of the new directory
        Dim newDir As String = basePath & "Updates\client2\new"
        For Each curFile As String In IO.Directory.GetFiles(newDir, "*.*", IO.SearchOption.AllDirectories)
            Dim relFile As String = curFile.Substring(curFile.IndexOf(newDir) + newDir.Length + 1)
            If relFile.IndexOf("\") <> -1 Then IO.Directory.CreateDirectory(updateDir & "\" & relFile.Substring(0, relFile.LastIndexOf("\")))

            IO.File.Copy(curFile, updateDir & "\" & relFile)
            If filesList.IndexOf("§" & relFile & "§") = -1 Then
                Dim sizeFile As New IO.FileInfo(curFile)
                filesList &= vbCrLf & relFile.Replace("\", "/") & "§" & relFile & "§" & sizeFile.Length & "§False§Binary"
            End If
        Next

        'Copy files (Clinica + linked files)
        For Each curFileEntry As DictionaryEntry In filesToCopy
            Dim curFile As IO.FileInfo = curFileEntry.Key
            Dim curVerFile As IO.FileInfo = curFileEntry.Value
            curFile = New IO.FileInfo(curFile.FullName)
            curVerFile = New IO.FileInfo(curVerFile.FullName)

            filesList &= vbCrLf & curFile.Name & "§" & curFile.Name & "§" & curFile.Length & "§True§Binary"

            curFile.CopyTo(curVerFile.FullName.Insert(curVerFile.FullName.LastIndexOf("\"), "\" & curUpdateVer), True)
            Console.Write(".")
        Next

        Console.Write(".")
        filesList = filesList.Substring(2)

        'Ensure/Create files.list
        IO.File.WriteAllText(updateDir & "\files.list", filesList, System.Text.Encoding.Default)
        Console.Write(".")

        Console.WriteLine()
        Console.Write("Terminé")
    End Sub

    Private Sub writeDot()
        While SpinWait
            Console.Write(".")
            Threading.Thread.Sleep(500)
        End While
    End Sub


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
                compared = CompareData(firstBuffer, curPos + nbToRead, secondBuffer, curPos + nbToRead, curPos)
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

End Module
