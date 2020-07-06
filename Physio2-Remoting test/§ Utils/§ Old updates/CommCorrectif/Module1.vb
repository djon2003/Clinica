Imports Clinica
Imports Microsoft.Win32.Registry

Module corrector

    Private Sub setSQLConnexion()
        Dim LastServer, lastPort As String
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

        'Console.WriteLine("Taper l'entrée incorrecte de la colonne NameOfFile :")
        'Dim CurErronus As String = Console.ReadLine()
        'SELECT * FROM Communications WHERE CommDate='2009-02-28 00:00:00'
        Dim comms As DataSet = DBLinker.GetInstance.ReadDBForGrid("Communications", "*", "WHERE CommDate='1900-01-01' ORDER BY NoClient,NoCommunication") 'CommDate='2009-02-28 00:00:00'
        Dim appPath As String = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("DataPath").ToString
        AppPath = AppPath & IIf(AppPath.EndsWith("\"), "", "\")

        Dim curClient As Integer = 0
        Dim script As New Text.StringBuilder
        Dim changingSubject As String = ""
        Dim changingDate As String = ""
        With comms.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim curNo As Integer = .Item(i)("NoCommunication")
                Dim fileName As String = .Item(i)("NameOfFile").ToString
                FileName = FileName.Substring(FileName.IndexOf("|") + 1)
                Try
                    If IO.File.Exists(AppPath & "Clients\" & .Item(i)("NoClient") & "\Comm\" & FileName) Then
                        Dim curFile As New IO.FileInfo(AppPath & "Clients\" & .Item(i)("NoClient") & "\Comm\" & FileName)
                        
                        script.AppendLine("UPDATE Communications SET CommDate='" & curFile.CreationTime.Year & "/" & curFile.CreationTime.Month & "/" & curFile.CreationTime.Day & "' WHERE NoCommunication=" & CurNo)
                    End If
                Catch
                    Console.WriteLine("BUG WITH FILE : " & AppPath & "Clients\" & .Item(i)("NoClient") & "\Comm\" & FileName)
                End Try
            Next i

            IO.File.WriteAllText(AppPath & "commcorrection.sql", script.ToString)
        End With
    End Sub

End Module
