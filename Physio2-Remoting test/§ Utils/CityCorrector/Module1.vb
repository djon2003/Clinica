Imports Microsoft.Win32.Registry

Module Module1


    Private Sub setSQLConnexion()
        Dim LastServer, LastPort, lastDB As String
        'Charge le serveur et le port
        LastDB = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("DBName", "Clinica").ToString

        Try
            LastServer = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("ServerIP").ToString
            LastPort = LocalMachine.OpenSubKey("Software", True).CreateSubKey("CyberInternautes").CreateSubKey("Clinica").CreateSubKey("Préférences").GetValue("ServerPort").ToString
        Catch
            LastServer = ""
            LastPort = ""
        End Try

        If LastPort = "" Or LastPort = "-1" Then
            DBLinker.GetInstance.InitConnection(LastServer, LastDB)
        Else
            DBLinker.GetInstance.InitConnection(LastServer, Integer.Parse(LastPort), LastDB)
        End If

        'Ouverture de la connexion à la base de données
        DBLinker.GetInstance().DBConnected = True
        DBLinker.GetInstance.KeepAlive = True
    End Sub

    Sub main()
        SetSQLConnexion()
        DBLinker.GetInstance.DBConnected = True

        Dim nbChanged As Integer = 0
        Dim clients As DataSet = DBLinker.GetInstance.ReadDBForGrid("InfoClients", "*", "WHERE NoVille IS NULL")
        If clients Is Nothing Then Exit Sub

        Console.WriteLine(clients.Tables(0).Rows.Count & " ville(s) à corriger")

        With clients.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim cp As String = .Item(i)("CodePostal").ToString.Replace(" ", "")

                If cp.Length = 6 Then
                    Dim newCity As String = getCity(cp)
                    If newCity = "" Then newCity = getCity(.Item(i)("Adresse").ToString & " quebec")

                    If newCity <> "" Then
                        newCity = System.Text.Encoding.Default.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Default, System.Text.Encoding.Default.GetBytes(newCity)))
                        DBLinker.GetInstance.UpdateDB("InfoClients", "NoVille=" & DBLinker.GetInstance.AddItemToADBList("Villes", "NomVille", newCity, "NoVille"), "NoClient", .Item(i)("NoClient"), False)
                        nbChanged += 1
                    End If
                End If

                Console.Write(".")
            Next i
        End With

        DBLinker.GetInstance.DBConnected = False

        Console.Clear()
        Console.WriteLine(nbChanged & " ville(s) ont été corrigées sur " & clients.Tables(0).Rows.Count)
        Console.ReadKey()
    End Sub

    Private wc As New System.Net.WebClient
    Private Const cityTagName As String = "long_name"

    Private Function getCity(ByVal search As String) As String
        search = search.Replace(" ", "%20")
        Dim xml As String = wc.DownloadString("http://maps.googleapis.com/maps/api/geocode/xml?sensor=false&language=fr&address=" & search)

        Dim posLocality As Integer = xml.IndexOf("<type>locality</type>")
        If posLocality = -1 Then Return ""

        Dim posLocality2 As Integer = xml.IndexOf("<type>locality</type>", posLocality + 1)
        If posLocality2 <> -1 Then Return ""

        Dim firstPos As Integer = xml.LastIndexOf("<" & cityTagName & ">", posLocality)
        firstPos += cityTagName.Length + 2
        Dim lastPos As Integer = xml.IndexOf("</" & cityTagName & ">", firstPos)

        Return xml.Substring(firstPos, lastPos - firstPos)
    End Function

End Module
