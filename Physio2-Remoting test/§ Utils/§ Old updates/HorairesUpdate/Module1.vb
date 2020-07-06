Imports Microsoft.Win32.Registry

Module Module1


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
            DBLinker.GetInstance.InitConnection(LastServer, "Clinica")
        Else
            DBLinker.GetInstance.InitConnection(LastServer, Integer.Parse(LastPort), "Clinica")
        End If

        'Ouverture de la connexion à la base de données
        DBLinker.GetInstance().DBConnected = True
        DBLinker.GetInstance.KeepAlive = True
    End Sub

    Sub main()
        SetSQLConnexion()
        DBLinker.GetInstance.DBConnected = True

        Dim horaires As DataSet = DBLinker.GetInstance.ReadDBForGrid("Horaires", "*")
        With horaires.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim lundi As String = .Item(i)("Lundi")
                Dim mardi As String = .Item(i)("mardi")
                Dim mercredi As String = .Item(i)("mercredi")
                Dim jeudi As String = .Item(i)("jeudi")
                Dim vendredi As String = .Item(i)("vendredi")
                Dim samedi As String = .Item(i)("samedi")
                Dim dimanche As String = .Item(i)("dimanche")

                DBLinker.GetInstance.UpdateDB("Horaires", "Lundi='" & demultiply(lundi) & "',mardi='" & demultiply(mardi) _
                & "',mercredi='" & demultiply(mercredi) & "',jeudi='" & demultiply(jeudi) & "',vendredi='" & demultiply(vendredi) _
                & "',samedi='" & demultiply(samedi) & "',dimanche='" & demultiply(dimanche) & "'", "NoHoraire", .Item(i)("NoHoraire"), False)
            Next i
        End With

        DBLinker.GetInstance.DBConnected = False
    End Sub

    Private Function demultiply(ByVal input As String) As String
        Dim output As String = ""
        For i As Integer = 0 To input.Length - 1
            For j As Integer = 1 To 15
                output &= input.Chars(i)
            Next j
        Next i

        Return "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" & output
    End Function

End Module
