Imports Microsoft.Win32.Registry


Module Module1
    Public Const limitDate As Date = #1/27/9999#
    Private LastServer, lastPort As String

    Sub Main()
        Console.Write("Connection to database...")
        setSQLConnexion()
        Console.WriteLine("Connected.")

        Console.Write("Loading schedules...")
        SchedulesManager.getInstance()
        Console.WriteLine("Loaded.")

        Console.Write("Modification of schedules...")
        For Each curSchedule As Schedule In SchedulesManager.getInstance.getItemables()
            curSchedule.saveData()
        Next
        Console.WriteLine("Done.")
    End Sub

    Private Sub setSQLConnexion()
        'Charge le serveur et le port
        LastServer = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("ServerIP", "").ToString
        lastPort = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("ServerPort", "").ToString

        Dim dbName As String = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("DBName", "Clinica").ToString
        Dim username As String = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("DBUsername", "").ToString
        Dim password As String = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("DBPassword", "").ToString

        If lastPort = "" Or lastPort = "-1" Then
            lastPort = "-1"
            DBLinker.getInstance.initConnection(LastServer, dbName, username, password)
        Else
            DBLinker.getInstance.initConnection(LastServer, Integer.Parse(lastPort), dbName, username, password)
        End If

        'Ouverture de la connexion à la base de données
        Try
            DBLinker.getInstance().dbConnected = True
        Catch ex As Exception
        End Try

        
        DBLinker.getInstance.keepAlive = True
    End Sub

End Module
