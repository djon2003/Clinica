Imports Microsoft.Win32.Registry

Module cleanUselessFolders

    Sub main()
        Dim appPath As String = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("DataPath").ToString
        If AppPath.EndsWith("\") = False Then AppPath &= "\"

        If IO.Directory.Exists(AppPath & "Data\DefTypes") Then My.Computer.FileSystem.DeleteDirectory(AppPath & "Data\DefTypes", FileIO.DeleteDirectoryOption.DeleteAllContents)
        If IO.Directory.Exists(AppPath & "Data\Modeles") Then My.Computer.FileSystem.DeleteDirectory(AppPath & "Data\Modeles", FileIO.DeleteDirectoryOption.DeleteAllContents)
        If IO.Directory.Exists(AppPath & "Users\AddressBook") Then My.Computer.FileSystem.DeleteDirectory(AppPath & "Users\AddressBook", FileIO.DeleteDirectoryOption.DeleteAllContents)
        If IO.Directory.Exists(AppPath & "Users\Alert") Then My.Computer.FileSystem.DeleteDirectory(AppPath & "Users\Alert", FileIO.DeleteDirectoryOption.DeleteAllContents)
        If IO.Directory.Exists(AppPath & "Users\Mail") Then My.Computer.FileSystem.DeleteDirectory(AppPath & "Users\Mail", FileIO.DeleteDirectoryOption.DeleteAllContents)
        If IO.Directory.Exists(AppPath & "Users\Search") Then My.Computer.FileSystem.DeleteDirectory(AppPath & "Users\Search", FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub

End Module
