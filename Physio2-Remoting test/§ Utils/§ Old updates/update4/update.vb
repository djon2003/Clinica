Imports Microsoft.Win32.Registry
Imports Clinica

'Mise à jour des préférences du mode de stockage en fichier vers SQL

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
        updatePrefFile(AppPath & "Data\pref.sav", 0)
        For Each curUser As User In UserManager.getInstance.getUsers()
            updatePrefFile(AppPath & "Users\Pref\" & curUser.noUser, curUser.noUser)
        Next

        If IO.Directory.Exists(AppPath & "Users\Pref") Then IO.Directory.Delete(AppPath & "Users\Pref")
    End Sub

    Private Sub updatePrefFile(ByVal file As String, ByVal noUser As Integer)
        If IO.File.Exists(file) = False Then Exit Sub
        Dim prefWin As New preferencesWin
        prefWin.createPrefsFile()
        Dim prefsLink() As String = IO.File.ReadAllLines(Environment.CurrentDirectory & IIf(Environment.CurrentDirectory.EndsWith("\"), "", "\") & "prefs.link")
        Dim links As New Hashtable
        For p As Integer = 0 To prefsLink.Length - 1
            Dim line() As String = prefsLink(p).Split(New Char() {":"})
            If (noUser = 0 AndAlso line(0).StartsWith("G") OrElse (noUser <> 0 AndAlso line(0).StartsWith("U"))) Then links.Add(line(1).Trim, line(2).Trim)
        Next p

        Dim prefTemp As Object = ""
        Dim pref() As Object
        Dim i, FileNum, maximum As Integer

        Dim readingTries As Integer = 0
        FileNum = FreeFile()
        FileOpen(FileNum, file, OpenMode.Random, OpenAccess.Read)
        FileGetObject(FileNum, Maximum, 1)
        ReDim pref(Maximum - 1)

        For i = 1 To Maximum
            FileGetObject(1, PrefTemp, i + 1)
            If PrefTemp Is Nothing Then
                pref(i - 1) = ""
            Else
                pref(i - 1) = PrefTemp.ToString.Replace("§", vbTab).Replace(vbCrLf, vbTab)
            End If
        Next i
        FileClose(FileNum)

        Dim curPref As Preferences = PreferencesManager.getInstance.getPreferences(noUser)
        For i = 0 To pref.Length - 1
            If links(i.ToString) IsNot Nothing Then curPref.setProperty(links(i.ToString), pref(i))
        Next i
        curPref.saveData()

        IO.File.Delete(file)
    End Sub

End Module
