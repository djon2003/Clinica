Imports Chilkat

Imports System.Xml
Module Backup

    Const Error_Min_Params = "ABORDED:This program require that a filename with his path is passed as parameter"

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

    Dim WithEvents zipper As New Chilkat.Zip
    Dim WithEvents progBarFile As New Windows.Forms.ProgressBar
    Dim WithEvents progBarTotal As New Windows.Forms.ProgressBar
    Dim WithEvents curFile As New System.Windows.Forms.Label
    Dim WithEvents progressChecker As New Timers.Timer(10000)

    Private Sub createXMLInput()
        Dim newDT As New DataTable
        newDT.Columns.Add(New DataColumn("LocationName"))
        newDT.Columns.Add(New DataColumn("CopyType"))
        newDT.Columns.Add(New DataColumn("LocationPath"))
        newDT.Columns.Add(New DataColumn("VPNLocation"))
        newDT.Columns.Add(New DataColumn("VPNUserName"))
        newDT.Columns.Add(New DataColumn("VPNPassword"))
        newDT.Columns.Add(New DataColumn("KeepOnlyOneCopy"))
        Dim newRow As DataRow
        newRow = newDT.NewRow
        newRow(0) = "Local network"
        newRow(1) = "DirectCopy"
        newRow(2) = "\\87b8a1a0459a4cb\e"
        newRow(3) = ""
        newRow(4) = ""
        newRow(5) = ""
        newRow(6) = True
        newDT.Rows.Add(newRow)

        newRow = newDT.NewRow
        newRow(0) = "Same disk"
        newRow(1) = "DirectCopy"
        newRow(2) = "C:\"
        newRow(3) = ""
        newRow(4) = ""
        newRow(5) = ""
        newRow(6) = False
        newDT.Rows.Add(newRow)

        'newRow = newDT.NewRow
        'newRow(0) = "VPN copy"
        'newRow(1) = "VPNCopy"
        'newRow(2) = "\\cp01\sys\"
        'newRow(3) = "physiopat.dnsalias.net"
        'newRow(4) = "administrateur"
        'newRow(5) = "clinique"
        'newRow(6) = True
        'newDT.Rows.Add(newRow)

        newDT.TableName = "CopyLocations"
        newDT.WriteXml("C:\.Cl.n.ca.bkp.prep\test.xml", True)
    End Sub

    Function main() As Integer
        Dim errors As New Text.StringBuilder()

        If My.Application.CommandLineArgs.Count = 0 Then
            errors.AppendLine(Error_Min_Params)
            errors.AppendLine(Microsoft.VisualBasic.StrDup(60, "-"))
            Console.WriteLine(Error_Min_Params)
            Return 2
        End If
        Dim settingFile As String = My.Application.CommandLineArgs(0)

        'Dim backupFile As String = PrepareBackupFile()
        Dim backupFile As String = "C:\.Cl.n.ca.bkp.prep\ClinicaBackup.200968.zip"

        createXMLInput()
        Console.WriteLine("Input created")
        Dim copyLocations As New DataTable("CopyLocations")
        copyLocations.ReadXmlSchema(settingFile)
        copyLocations.ReadXml(settingFile)

        For i As Integer = 0 To copyLocations.Rows.Count - 1
            Try
                Dim curCL As CopyLocation = CopyLocation.createNew(copyLocations.Rows(i), backupFile)
                curCL.copy()
            Catch ex As Exception
                errors.AppendLine(ex.Message)
                errors.AppendLine(Microsoft.VisualBasic.StrDup(60, "-"))
                Console.WriteLine(ex.Message)
            End Try
        Next i

        If errors.Length <> 0 Then IO.File.WriteAllText("C:\.Cl.n.ca.bkp.prep\errors.log", errors.ToString)

        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Backup processing terminated" & IIf(errors.Length <> 0, " with errors (see C:\.Cl.n.ca.bkp.prep\errors.log for details)", ""))
        Console.ReadLine()
    End Function

    Private Function prepareBackupFile() As String
        Dim startTime As Date = Date.Now

        Dim folderName As String = ".Cl.n.ca.bkp.prep"
        IO.Directory.CreateDirectory("C:\" & folderName)

        If IO.File.Exists("C:\" & folderName & "\dbfile.dat") = False Then
            Microsoft.VisualBasic.Shell("sqlcmd -S"".\sqlexpress"" -E -Q""SELECT filename FROM sysdatabases where name='Clinica'"" -o ""C:\" & folderName & "\dbfile.dat""")
            While IO.File.Exists("C:\" & folderName & "\dbfile.dat") = False OrElse fileInUse("C:\" & folderName & "\dbfile.dat") = True
                Threading.Thread.SpinWait(1000)
            End While
        End If

        Dim output() As String = {}
        While output.Length = 0
            output = IO.File.ReadAllLines("C:\" & folderName & "\dbfile.dat")
        End While
        Dim clinicaPath As String = output(2).Substring(0, output(2).LastIndexOf("\") + 1)

        Try
            Microsoft.VisualBasic.Shell("sqlcmd -S"".\sqlexpress"" -E -Q""BACKUP DATABASE Clinica TO DISK = 'c:\" & folderName & "\clinica.bak' WITH INIT""", AppWinStyle.Hide, True)
        Catch ex As Exception
            Return ""
        End Try
        'compressor.SetCodeProgress(compressorProgress)


        'progressChecker.Start()

        'IO.Directory.CreateDirectory("C:\" & folderName & "\zipTemp")
        Dim zipComment As String = "Backup Clinica " & Date.Now.ToString("yy-MM-dd") & vbCrLf & clinicaPath

        If IO.File.Exists("C:\" & folderName & "\clients.zip") Then IO.File.Delete("C:\" & folderName & "\clients.zip")
        Compression.compress(IO.Directory.GetFiles(clinicaPath & "Clients", "*.*", IO.SearchOption.AllDirectories), "C:\" & folderName & "\clients.zip", zipComment & "Clients", clinicaPath & "Clients")

        If IO.File.Exists("C:\" & folderName & "\kp.zip") Then IO.File.Delete("C:\" & folderName & "\kp.zip")
        Compression.compress(IO.Directory.GetFiles(clinicaPath & "KP", "*.*", IO.SearchOption.AllDirectories), "C:\" & folderName & "\kp.zip", zipComment & "KP", clinicaPath & "KP")

        If IO.File.Exists("C:\" & folderName & "\data.zip") Then IO.File.Delete("C:\" & folderName & "\data.zip")
        Compression.compress(IO.Directory.GetFiles(clinicaPath & "Data", "*.*", IO.SearchOption.AllDirectories), "C:\" & folderName & "\data.zip", zipComment & "Data", clinicaPath & "Data")

        If IO.File.Exists("C:\" & folderName & "\users.zip") Then IO.File.Delete("C:\" & folderName & "\users.zip")
        Compression.compress(IO.Directory.GetFiles(clinicaPath & "Users", "*.*", IO.SearchOption.AllDirectories), "C:\" & folderName & "\users.zip", zipComment & "Users", clinicaPath & "Users")

        'Append all final files to zip
        Dim bkpFilename As String = "C:\" & folderName & "\ClinicaBackup." & Date.Now.Year & Date.Now.Month & Date.Now.Day & ".zip"
        If IO.File.Exists(bkpFilename) Then IO.File.Delete(bkpFilename)
        Compression.compress(New String() {"C:\" & folderName & "\clinica.bak", "C:\" & folderName & "\kp.zip", "C:\" & folderName & "\clients.zip", "C:\" & folderName & "\users.zip", "C:\" & folderName & "\data.zip"}, "" & bkpFilename & "", "", "C:\" & folderName) ' zipComment & vbCrLf & "All folders zipped + Clinica.bkp from C:\" & folderName)

        'Extract and compare
        zipper.OpenZip(bkpFilename)
        IO.Directory.CreateDirectory("C:\" & folderName & "\ClinicaData-New")
        zipper.Unzip("C:\" & folderName & "\ClinicaData-New")

        Dim compared As Boolean = True
        compared = compared AndAlso compareFile("C:\" & folderName & "\ClinicaData-New\clinica.bak", "C:\" & folderName & "\clinica.bak")
        compared = compared AndAlso compareFile("C:\" & folderName & "\ClinicaData-New\clients.zip", "C:\" & folderName & "\clients.zip")
        compared = compared AndAlso compareFile("C:\" & folderName & "\ClinicaData-New\kp.zip", "C:\" & folderName & "\kp.zip")
        compared = compared AndAlso compareFile("C:\" & folderName & "\ClinicaData-New\data.zip", "C:\" & folderName & "\data.zip")
        compared = compared AndAlso compareFile("C:\" & folderName & "\ClinicaData-New\users.zip", "C:\" & folderName & "\users.zip")
        If compared = False Then Console.WriteLine("Base zip files comparaison failed")

        compared = compared AndAlso extractCompareDir(clinicaPath, "C:\" & folderName & "\ClinicaData-New", "clients")
        compared = compared AndAlso extractCompareDir(clinicaPath, "C:\" & folderName & "\ClinicaData-New", "kp")
        compared = compared AndAlso extractCompareDir(clinicaPath, "C:\" & folderName & "\ClinicaData-New", "data")
        compared = compared AndAlso extractCompareDir(clinicaPath, "C:\" & folderName & "\ClinicaData-New", "users")

        '        progressChecker.Stop()

        cleanUpPrep("C:\" & folderName)
        If compared = False Then
            My.Computer.FileSystem.DeleteDirectory("C:\" & folderName, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
            Console.WriteLine("COMPARE:FAILURE:Deleted all")
        End If

        Dim timeE As TimeSpan = Date.Now.Subtract(startTime)
        Console.WriteLine("Total time for preparation : " & timeE.Hours.ToString("00") & ":" & timeE.Minutes.ToString("00") & ":" & timeE.Seconds.ToString("00") & ":" & timeE.Milliseconds.ToString("000"))

        Return bkpFilename
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub cleanUpPrep(ByVal cleanupFolder As String)
        If IO.File.Exists(cleanupFolder & "\clinica.bak") Then IO.File.Delete(cleanupFolder & "\clinica.bak")
        If IO.File.Exists(cleanupFolder & "\clients.zip") Then IO.File.Delete(cleanupFolder & "\clients.zip")
        If IO.File.Exists(cleanupFolder & "\data.zip") Then IO.File.Delete(cleanupFolder & "\data.zip")
        If IO.File.Exists(cleanupFolder & "\kp.zip") Then IO.File.Delete(cleanupFolder & "\kp.zip")
        If IO.File.Exists(cleanupFolder & "\users.zip") Then IO.File.Delete(cleanupFolder & "\users.zip")
        If IO.File.Exists(cleanupFolder & "\dbfile.dat") Then IO.File.Delete(cleanupFolder & "\dbfile.dat")
        If IO.Directory.Exists(cleanupFolder & "\ClinicaData-New") Then
            Dim done As Boolean = False
            Dim delTries As Integer = 0
            While done = False AndAlso delTries < 1000
                Try
                    My.Computer.FileSystem.DeleteDirectory(cleanupFolder & "\ClinicaData-New", FileIO.DeleteDirectoryOption.DeleteAllContents)
                    done = True
                Catch
                    Threading.Thread.SpinWait(1000)
                End Try
                delTries += 1
            End While
            If delTries = 1000 Then Console.WriteLine("Folder " & cleanupFolder & "\ClinicaData-New" & " could not be completely deleted")
        End If
    End Sub

    Public Function extractCompareDir(ByVal clinicaPath As String, ByVal basePath As String, ByVal fileDirName As String)
        IO.Directory.CreateDirectory(basePath & "\" & fileDirName)

        zipper.OpenZip(basePath & "\" & fileDirName & ".zip")
        zipper.Unzip(basePath & "\" & fileDirName)
        zipper.CloseZip()

        Dim compared As Boolean = compareDir(clinicaPath & fileDirName, basePath & "\" & fileDirName)
        If compared = False Then Console.WriteLine(fileDirName & " zip comparaison failed")

        Return compared
    End Function

    Public Function compareDir(ByVal cheminSource As String, ByVal cheminCible As String) As Boolean
        Dim files() As String = IO.Directory.GetFiles(cheminSource, "*.*", IO.SearchOption.AllDirectories)
        Dim fileCompared As Boolean = True

        For i As Integer = 0 To files.Length - 1
            Dim fichierCible As String = cheminCible & files(i).Substring(cheminSource.Length)
            If IO.File.Exists(fichierCible) = False Then
                Console.WriteLine("COMPARE:File " & fichierCible & " doesn't exist")
                Return False
            End If

            fileCompared = compareFile(files(i), fichierCible)
            If fileCompared = False Then Return False
        Next

        Return True
    End Function

    Public Function compareFile(ByVal cheminSource As String, ByVal cheminCible As String) As Boolean
        Dim firstFile As New IO.FileStream(cheminSource, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        Dim secondFile As New IO.FileStream(cheminCible, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
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
                Console.WriteLine("COMPARE:File " & cheminCible & " is not equals to " & cheminSource)
                firstFile.Close()
                secondFile.Close()
                Return False
            End If
        Next i

        firstFile.Close()
        secondFile.Close()

        Return True
    End Function

    Public Function copyFile(ByVal cheminSource As String, ByVal cheminCible As String) As Boolean
        Dim firstFile As New IO.FileStream(cheminSource, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        Dim secondFile As New IO.FileStream(cheminCible, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite, IO.FileShare.None)
        Try
            Dim compared As Boolean = True
            Dim firstBuffer(firstFile.Length) As Byte
            Dim firstLength As Long = firstFile.Length
            Dim moding As Long = firstLength / 1024 / 100
            For i As Integer = secondFile.Length To firstLength - 1 Step 1024
                If i Mod moding = 0 Then
                    If i = 0 Then Console.Write("0") Else Console.Write((i + 1) / firstLength * 100)
                    Console.WriteLine(" %")
                End If
                Dim nbToRead As Integer = 1024
                If (i + 1024) > firstLength Then nbToRead = firstLength - i
                Dim nbFirstRead As Integer = firstFile.Read(firstBuffer, i, nbToRead)
                secondFile.Write(firstBuffer, i, nbToRead)
            Next i
        Catch ex As Exception
            Throw ex
        Finally
            firstFile.Close()
            secondFile.Close()
        End Try

        Return True
    End Function

    Public Function compareData(ByVal buf1() As Byte, ByVal len1 As Integer, ByVal buf2() As Byte, ByVal len2 As Integer, Optional ByVal starting As Long = 0) As Boolean
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

    Private Sub curFile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles curFile.TextChanged
        Console.WriteLine(curFile.Text)
    End Sub

    Private Sub zipper_OnFileUnzipped(ByVal sender As Object, ByVal args As Chilkat.FileUnzippedEventArgs) Handles zipper.OnFileUnzipped
        Console.WriteLine("File " & args.FileName & " unzipped (from " & args.CompressedSize & " bytes to " & args.FileSize & ")")
    End Sub

    Private Sub zipper_OnUnzipPercentDone(ByVal sender As Object, ByVal args As Chilkat.UnzipPercentDoneEventArgs) Handles zipper.OnUnzipPercentDone
        Console.WriteLine(args.PercentDone & " %")
    End Sub
End Module
