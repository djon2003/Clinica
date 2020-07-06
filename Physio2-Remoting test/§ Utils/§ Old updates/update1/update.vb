Imports Microsoft.Win32.Registry
Imports Clinica

Module update

    Public Sub setSQLConnexion()
        Dim lastServer As String = ""
        Dim lastPort As String = ""
        'Charge le serveur et le port
        Try
            LastServer = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("ServerIP").ToString
            LastPort = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("ServerPort").ToString
        Catch
            LastServer = ""
            LastPort = ""
        End Try

        If LastPort = "" Or LastPort = "-1" Then
            LastPort = "-1"
            DBLinker.GetInstance.InitConnection(LastServer)
        Else
            DBLinker.GetInstance.InitConnection(LastServer, Integer.Parse(LastPort))
        End If

        'Ouverture de la connexion à la base de données
        DBLinker.GetInstance().DBConnected = True
        DBLinker.GetInstance.KeepAlive = True
    End Sub

    Sub main()
        SetSQLConnexion()

        Dim appPath As String = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("DataPath").ToString
        If AppPath.EndsWith("\") = False Then AppPath &= "\"
        Dim baseDBPath As String = AppPath & "DB\DB"
        Dim allFolders() As String = IO.Directory.GetDirectories(BaseDBPath, "*", IO.SearchOption.AllDirectories)

        '' UPDATE FROM FILE MODE TO SQL MODE FOR DBs related sectors
        'Update TypesFiles
        Dim types() As String = IO.Directory.GetFiles(AppPath & "DB\Types")
        For Each file As String In types
            Dim content() As String = IO.File.ReadAllLines(file)
            Dim curType As New TypeFile
            CurType.FileType = file.Substring(file.LastIndexOf("\") + 1)
            Dim baseType As TypeFile.BaseFileTypeEnum
            BaseType = [Enum].Parse(BaseType.GetType, content(0), True)
            CurType.BaseFileType = BaseType

            If content(1).ToUpper = "INTERNE" Then
                CurType.IsInterne = True
            Else
                CurType.IsInterne = False
            End If

            If content.Length > 2 Then CurType.Extensions = content(2)

            If content.Length > 3 Then CurType.IsHidden = content(3)
            If content.Length > 4 Then CurType.IsReadOnly = content(4)
            If content.Length > 5 Then CurType.SearchInContent = content(5)
            If content.GetUpperBound(0) > 6 Then
                CurType.Printable = content(7)
            Else
                CurType.Printable = False
            End If

            CurType.saveData()
        Next file

        'UPDATE DBFolders
        If AllFolders IsNot Nothing Then
            For i As Integer = 0 To AllFolders.Length - 1
                Dim newDBFolder As New DBFolder
                newDBFolder.DBFolder = AllFolders(i).Substring(BaseDBPath.Length + 1)
                Dim dirInfo As New IO.DirectoryInfo(AllFolders(i))
                If dirInfo.Attributes And IO.FileAttributes.Hidden Then newDBFolder.IsHidden = True
                newDBFolder.saveData()
            Next i
        End If

        'UPDATE DBItems
        Dim allFiles() As String = IO.Directory.GetFiles(BaseDBPath, "*.DB", IO.SearchOption.AllDirectories)
        If AllFiles IsNot Nothing Then
            Dim genNoDBFolder As Integer = DBManager.GetInstance.GetDBFolder("Généraux").NoDBFolder
            For i As Integer = 0 To AllFiles.Length - 1
                Dim curFile() As String = IO.File.ReadAllLines(AllFiles(i))
                Dim curFileInfo As New IO.FileInfo(AllFiles(i))
                Dim newDBItem As New Clinica.DBItem()
                newDBItem.NoDBFolder = GenNoDBFolder
                If CurFileInfo.DirectoryName <> BaseDBPath Then newDBItem.NoDBFolder = DBManager.GetInstance.GetDBFolder("Généraux\" & CurFileInfo.DirectoryName.Substring(BaseDBPath.Length + 1)).NoDBFolder
                newDBItem.DBItem = CurFileInfo.Name.Substring(0, CurFileInfo.Name.Length - 3)
                newDBItem.NoFileType = Clinica.TypesFilesManager.GetInstance.GetTypeFile(CurFile(0)).NoTypeFile
                newDBItem.IsHidden = CurFileInfo.Attributes And IO.FileAttributes.Hidden
                newDBItem.IsReadOnly = CurFileInfo.Attributes And IO.FileAttributes.ReadOnly
                If CurFile(1) <> "" Then newDBItem.MotsCles = CurFile(1).Split(New Char() {"§"})
                If CurFile.GetUpperBound(0) >= 4 Then newDBItem.Description = CurFile(4)
                For j As Integer = 5 To CurFile.GetUpperBound(0)
                    newDBItem.Description &= vbCrLf & CurFile(j)
                Next j
                newDBItem.saveData()

                If CurFile(2) <> "" Then
                    Dim contentExtension() As String = CurFile(2).Split(New Char() {"."})
                    If IO.File.Exists(CurFileInfo.DirectoryName.Replace("\DB\DB", "\DB\Content") & "\" & CurFile(2)) Then
                        IO.File.Copy(CurFileInfo.DirectoryName.Replace("\DB\DB", "\DB\Content") & "\" & CurFile(2), AppPath & "DB\" & newDBItem.NoDBItem & "." & newDBItem.DBItem & "." & ContentExtension(ContentExtension.GetUpperBound(0)))
                        IO.File.SetAttributes(AppPath & "DB\" & newDBItem.NoDBItem & "." & newDBItem.DBItem & "." & ContentExtension(ContentExtension.GetUpperBound(0)), IO.FileAttributes.Normal)
                    Else
                        IO.File.AppendAllText(AppPath & "transferDB.errors", "Content file missing : " & newDBItem.NoDBItem & "." & newDBItem.DBItem & "." & ContentExtension(ContentExtension.GetUpperBound(0)))
                    End If
                    newDBItem.DBItemFile = newDBItem.NoDBItem & "." & newDBItem.DBItem & "." & ContentExtension(ContentExtension.GetUpperBound(0))
                    newDBItem.saveData()
                End If
            Next i
        End If

        'UPDATE FolderTextes & Modeles
        Dim textes(,) As String = DBLinker.GetInstance.ReadDB("SELECT NoFolderTexte,Texte FROM FolderTextes WHERE [Texte] LIKE '%<a href=""clinica://DB%7cDB%5cDB%'")
        If Textes IsNot Nothing Then
            For i As Integer = 0 To Textes.GetUpperBound(1)
                DBLinker.GetInstance.UpdateDB("FolderTextes", "Texte='" & Textes(1, i).Replace("clinica://DB%7cDB%5cDB", "clinica://G%c3%a9n%c3%a9raux").Replace(".DB/""", """").Replace("'", "''") & "'", "NoFolderTexte", Textes(0, i), False)
            Next i
        End If
        Dim modeles(,) As String = DBLinker.GetInstance.ReadDB("SELECT NoModele,Modele FROM Modeles WHERE [Modele] LIKE '%<a href=""clinica://DB%7cDB%5cDB%'")
        If Modeles IsNot Nothing Then
            For i As Integer = 0 To Modeles.GetUpperBound(1)
                DBLinker.GetInstance.UpdateDB("Modeles", "Modele='" & Modeles(1, i).Replace("clinica://DB%7cDB%5cDB", "clinica://G%c3%a9n%c3%a9raux").Replace(".DB/""", """").Replace("'", "''") & "'", "NoModele", Modeles(0, i), False)
            Next i
        End If
    End Sub

End Module
