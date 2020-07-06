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
        Dim basePath As String = AppPath & "Data\Email\"

        If IO.File.Exists(BasePath & "accounts.param") = False Then Exit Sub

        '' UPDATE MailAccouts from FILE format to DB format
        Dim accounts() As String = IO.File.ReadAllLines(BasePath & "accounts.param")
        Dim serverOptions() As String
        Dim accountOptions() As String
        Dim defPOPServer As New MailAccount.MailAccountServer("", 110)
        Dim defSMTPServer As New MailAccount.MailAccountServer("", 21)
        For Each curAccount As String In accounts
            Dim accountParams() As String = curAccount.Split(New Char() {"§"})
            Dim newMailAccount As New MailAccount
            newMailAccount.AccountName = accountParams(0)
            newMailAccount.SendingName = accountParams(1)
            newMailAccount.Email = accountParams(2)
            If accountParams(3) <> "" Then
                ServerOptions = accountParams(3).Split(New Char() {":"})
                newMailAccount.POPServer = New MailAccount.MailAccountServer(ServerOptions(0), ServerOptions(1), ServerOptions(2))
            Else
                newMailAccount.POPServer = defPOPServer
            End If
            If accountParams(4) <> "" Then
                ServerOptions = accountParams(4).Split(New Char() {":"})
                newMailAccount.SMTPServer = New MailAccount.MailAccountServer(ServerOptions(0), ServerOptions(1), ServerOptions(2))
            Else
                newMailAccount.SMTPServer = defSMTPServer
            End If
            newMailAccount.Username = accountParams(5)
            AccountOptions = accountParams(8).Split(New Char() {":"})
            newMailAccount.SavePassword = AccountOptions(0)
            newMailAccount.KeepMSGOnServer = AccountOptions(1)
            newMailAccount.IncludeInGeneralReception = AccountOptions(2)
            newMailAccount.CommonAccount = AccountOptions(3)
            newMailAccount.InboxFolderName = accountParams(9)
            newMailAccount.saveData()
        Next

        IO.Directory.Delete(BasePath, True)
    End Sub

End Module
