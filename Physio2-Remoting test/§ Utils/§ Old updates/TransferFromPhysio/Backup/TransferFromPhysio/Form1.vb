Public Class Form1

    Private noVisite As Integer = 0
    Private noFacture As Integer = 0
    Private nbErreurs As Integer = 0
    Private sbSQL As New System.Text.StringBuilder()

    Private Sub lockItems(ByVal trueFalse As Boolean)
        Button1.Enabled = Not TrueFalse
        Client.Enabled = Not TrueFalse
        scripting.Enabled = Not TrueFalse
        mapfromfile.Enabled = Not TrueFalse
        Clinique.Enabled = Not TrueFalse
        RVs.Enabled = Not TrueFalse
        medecins.Enabled = Not TrueFalse
        Users.Enabled = Not TrueFalse
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub

        LockItems(True)

        Dim dbName As String = InputBox("Entrer le nom de la base de données de Clinica", "DB name", "Clinica")

        Dim startingTime As Date = Now
        Dim n As Integer = 0
        DBLinker.GetInstance(False).InitConnection(OpenFileDialog1.FileName)
        Dim serveur As String = InputBox("Adresse serveur", "serveur", ".\SQLEXPRESS")
        Dim port As String = InputBox("port du serveur", "Serveur", "")
        If Port = "" Then
            DBLinker.GetInstance(True).InitConnection(Serveur, , dbName)
        Else
            DBLinker.GetInstance(True).InitConnection(Serveur, CInt(Port), , dbName)
        End If
        DBLinker.GetInstance(False).DBConnected = True
        DBLinker.GetInstance(True).DBConnected = True

        Dim dsDosClient, dsMedecin, dsPara, dsParaUtil, dsTTBase As DataSet

        If medecins.Checked = True Then
            dsMedecin = DBLinker.GetInstance(False).ReadDBForGrid("MEDECIN", "*")

            DataGridView1.DataSource = dsMedecin
            DataGridView1.DataMember = "Table"
            n = 0
            NbErreurs = 0
            With dsMedecin.Tables(0).Rows
                For i As Integer = .Count - 1 To 0 Step -1
                    Me.Text = "Médecin (Restant/ajoutés) " & i & ":" & n
                    Application.DoEvents()
                    Dim clientExists As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("KeyPeople", "NoKP", "WHERE NoRef='" & .Item(i)("NO").ToString.Replace("'", "''") & "'")
                    If .Item(i)("NO").ToString().Trim().ToUpper <> "NIL" And (ClientExists Is Nothing OrElse ClientExists.Tables(0).Rows.Count = 0) Then
                        Try
                            Dim remarques As String = .Item(i)("AD1").ToString().Trim()
                            Dim adresse As String = .Item(i)("AD2").ToString().Trim()
                            Dim ville As String = .Item(i)("AD3").ToString().Trim()
                            Dim codePostal As String = .Item(i)("AD4").ToString().Trim()
                            If CodePostal = "" Then
                                CodePostal = Ville
                                Ville = Adresse
                                Remarques = ""
                            End If
                            Dim categorie As String = "Médecin"
                            If .Item(i)("SPEC").ToString.Trim <> "" Then
                                Categorie &= ":" & .Item(i)("SPEC").ToString.Trim
                            ElseIf .Item(i)("TITRE").ToString.Trim.ToUpper.IndexOf("MEDECIN") < 0 Then
                                Categorie &= ":" & .Item(i)("TITRE").ToString.Trim
                            End If
                            DBLinker.GetInstance(True).WriteDB("KeyPeople", "Nom,Adresse,NoVille,CodePostal,AutreInfos,Telephones,NoCategorie,NoRef", _
                            "'" & FirstLetterCapital(.Item(i)("NOM").ToString.Trim.ToLower, True).Replace("'", "''") & "','" & FirstLetterCapital(Adresse.ToLower, True).Replace("'", "''") & "'," & DBLinker.GetInstance(True).AddItemToADBList("Villes", "NomVille", FirstLetterCapital(Ville.ToString.ToLower, True), "NoVille") & ",'" & CodePostal.Replace(" ", "").Replace("-", "").ToUpper & "','" & FirstLetterCapital(Remarques.Replace("'", "''").ToLower) & "','" & _
                            "Tél 1:" & .Item(i)("TEL1").ToString.Trim.Replace("'", "''") & IIf(.Item(i)("TEL2").ToString.Trim <> "", "§Tél 2:" & .Item(i)("TEL2").ToString.Trim, "") & "'," & DBLinker.GetInstance(True).AddItemToADBList("KPCategorie", "Categorie", FirstLetterCapital(Categorie.ToLower, True), "NoCategorie") & ",'" & .Item(i)("NO").ToString.Trim().Replace("'", "''") & "'")
                            n += 1
                        Catch ex As Exception
                            erreurs.Text &= "Médecin de la table MEDECIN - entrée no:" & (i).ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                            erreurs.SelectionStart = erreurs.Text.Length - 1
                            NbErreurs += 1
                        End Try
                    End If
                Next i
            End With

            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de médecins ajoutés : " & n & "  Nombre d'erreurs : " & NbErreurs & vbCrLf & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1
        End If

        If Clinique.Checked Then
            dsPara = DBLinker.GetInstance(False).ReadDBForGrid("PARA", "*")

            DataGridView1.DataSource = dsPara
            DataGridView1.DataMember = "Table"

            With dsPara.Tables(0).Rows(0)
                Dim exists As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("InfoClinique", "Count(*)")
                Try
                    If Exists.Tables(0).Rows(0)(0) > 0 Then
                        DBLinker.GetInstance(True).UpdateDB("InfoClinique", "Nom=" & "'" & FirstLetterCapital(.Item("NOM").ToString().ToLower, True).Trim.Replace("'", "''") & "',Adresse='" & FirstLetterCapital(.Item("AD1").ToString().Trim.ToLower, True).Replace("'", "''") & "',NoVille=" & _
                        DBLinker.GetInstance(True).AddItemToADBList("Villes", "NomVille", FirstLetterCapital(.Item("AD2").ToString.Trim, True), "NoVille") & ",CodePostal='" & .Item("AD3").ToString().Trim().ToUpper.Replace(" ", "").Replace("'", "''") & "',Telephone='" & .Item("TEL").ToString.Trim.Replace("(", "").Replace(")", "").Replace(" ", "-") & "'")
                    Else
                        DBLinker.GetInstance(True).WriteDB("InfoClinique", "Nom,Adresse,NoVille,CodePostal,Telephone", "'" & FirstLetterCapital(.Item("NOM").ToString().Trim.ToLower, True).Replace("'", "''") & "','" & FirstLetterCapital(.Item("AD1").ToString().Trim.ToLower, True).Replace("'", "''") & "'," & _
                        DBLinker.GetInstance(True).AddItemToADBList("Villes", "NomVille", FirstLetterCapital(.Item("AD2").ToString.Trim.ToLower, True), "NoVille") & ",'" & .Item("AD3").ToString().Trim().ToUpper.Replace(" ", "").Replace("'", "''") & "','" & .Item("TEL").ToString.Trim.Replace("(", "").Replace(")", "").Replace(" ", "-") & "'")
                    End If
                Catch ex As Exception
                    erreurs.Text &= "Clinique de la table PARA - entrée no:1 - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                    erreurs.SelectionStart = erreurs.Text.Length - 1
                End Try
            End With
        End If

        Dim htTRP As New Hashtable
        Dim htNIP As New Hashtable
        Dim htModeleTypes As New Hashtable
        htModeleTypes.Add("A", "6")
        htModeleTypes.Add("B", "6")
        htModeleTypes.Add("C", "2")
        htModeleTypes.Add("D", "3")
        htModeleTypes.Add("E", "4")
        htModeleTypes.Add("F", "8")
        htModeleTypes.Add("G", "7")
        htModeleTypes.Add("H", "1")


        dsParaUtil = DBLinker.GetInstance(False).ReadDBForGrid("PARAUTIL", "*")
        dsTTBase = DBLinker.GetInstance(False).ReadDBForGrid("TTBASE", "*")

        'Utilisateurs
        If Users.Checked Then
            DataGridView1.DataSource = dsParaUtil
            DataGridView1.DataMember = "Table"
        End If
        n = 0
        NbErreurs = 0
        With dsParaUtil.Tables(0).Rows
            For i As Integer = .Count - 1 To 0 Step -1
                Me.Text = "Utilisateurs (Restant/ajoutés) " & i & ":" & n
                Application.DoEvents()
                If .Item(i)("NOM").ToString().Trim() = "" Then Continue For
                Dim noms(), nom As String
                Nom = .Item(i).Item("NOM").ToString.Trim.ToLower
                If Nom.IndexOf(",") >= 0 Then
                    Noms = Nom.Split(New Char() {","})
                Else
                    Noms = Nom.Split(New Char() {" "})
                    Nom = Noms(1)
                    Noms(1) = Noms(0)
                    Noms(0) = Nom
                End If

                Dim exists As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("Utilisateurs", "NoUser", "WHERE Nom='" & Noms(0) & "' AND Prenom='" & Noms(1) & "'")
                If Users.Checked Then
                    If (Exists Is Nothing OrElse Exists.Tables(0).Rows.Count = 0) Then
                        Try
                            DBLinker.GetInstance(True).WriteDB("Utilisateurs", "DroitAcces,IsTherapist,Cle,MDP,Nom,Prenom,DateDebut,DateFin,NoTitre,NoType,NoPermis,NoTypeEmploye", "'21111111111111111111111111111111111111111111111111111111111110111011111111111111111111111',0,'2008-04-0818:51:28.5312500','00','" & FirstLetterCapital(Noms(0).ToLower, True).Replace("'", "''") & "','" & FirstLetterCapital(Noms(1).ToLower, True).Replace("'", "''") & "','" & .Item(i)("DATEARRIVE").ToString.Trim & "'," & IIf(.Item(i)("DATETERMIN").ToString.Trim <> "", "'" & .Item(i)("DATETERMIN").ToString.Trim & "'", "null") & "," & DBLinker.GetInstance(True).AddItemToADBList("Titres", "Titre", FirstLetterCapital(.Item(i)("FONCT").ToString.Trim.ToLower, True), "NoTitre") & ",0,'" & .Item(i)("NUMPERMIS").ToString.Trim.Replace("'", "''") & "',1")
                            n += 1
                        Catch ex As Exception
                            erreurs.Text &= "Utilisateur de la table PARAUTIL - entrée no:" & (i).ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                            erreurs.SelectionStart = erreurs.Text.Length - 1
                            NbErreurs += 1
                        End Try
                    End If
                    Exists = DBLinker.GetInstance(True).ReadDBForGrid("Utilisateurs", "NoUser", "WHERE Nom='" & Noms(0) & "' AND Prenom='" & Noms(1) & "'")
                End If
                If Exists.Tables(0).Rows.Count > 0 Then
                    If htNIP.ContainsKey(.Item(i)("NIP").ToString.Trim) = False Then htNIP.Add(.Item(i)("NIP").ToString.Trim, Exists.Tables(0).Rows(0)(0))
                    If htTRP.ContainsKey(.Item(i)("EQUIPE").ToString.Trim & "-" & .Item(i)("THERAPEUTE").ToString.Trim) = False Then htTRP.Add(.Item(i)("EQUIPE").ToString.Trim & "-" & .Item(i)("THERAPEUTE").ToString.Trim, Exists.Tables(0).Rows(0)(0))
                End If
            Next i
        End With

        If Users.Checked Then
            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre d'utilisateurs ajoutés : " & n & "  Nombre d'erreurs : " & NbErreurs & vbCrLf & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1

            DBLinker.ExecuteSQLScript("INSERT INTO SettingsUser (NoUser) SELECT NoUser FROM Utilisateurs WHERE Utilisateurs.NoUser NOT IN (SELECT NoUser FROM SettingsUser)")


            'Modèles
            DataGridView1.DataSource = dsTTBase
            DataGridView1.DataMember = "Table"
            n = 0
            NbErreurs = 0
            With dsTTBase.Tables(0).Rows
                For i As Integer = .Count - 1 To 0 Step -1
                    Me.Text = "Modèles (Restant/ajoutés) " & i & ":" & n
                    Application.DoEvents()

                    Try
                        Dim noUser As Integer = 0
                        If .Item(i)("NIPTHE").ToString.Trim <> "" Then NoUser = GetNoNIP(htNIP, .Item(i)("NIPTHE").ToString.Trim, True)

                        Dim exists As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("Modeles", "NoModele", "WHERE Nom='" & .Item(i)("DESCRIPT").ToString.Replace("'", "''") & "' AND NoUser=" & NoUser & " AND NoCategorie=" & htModeleTypes(.Item(i)("SECTION").ToString))
                        If Exists IsNot Nothing AndAlso Exists.Tables(0).Rows.Count = 0 And .Item(i)("SECTION").ToString <> "H" Then
                            DBLinker.GetInstance(True).WriteDB("Modeles", "NoCategorie,NoUser,Nom,Modele", htModeleTypes(.Item(i)("SECTION").ToString) & "," & NoUser & ",'" & FirstLetterCapital(.Item(i)("DESCRIPT").ToString.ToLower, True).Replace("'", "''") & "','" & FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(i)("NOTE").ToString.Replace("'", "''"), "(\r\n|\r|\n)", "<BR>").ToLower, True) & "'")
                            n += 1
                        End If
                    Catch ex As Exception
                        erreurs.Text &= "Modèle de la table TTBASE - entrée no:" & (i).ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                        erreurs.SelectionStart = erreurs.Text.Length - 1
                        NbErreurs += 1
                    End Try
                Next i
            End With

            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de modèles ajoutés : " & n & "  Nombre d'erreurs : " & NbErreurs & vbCrLf & vbCrLf & vbCrLf

        End If

        erreurs.Text &= vbCrLf & "Starting Clients generation : " & Now.ToString & vbCrLf & vbCrLf
        erreurs.SelectionStart = erreurs.Text.Length - 1

        Dim n2 As Integer = 0
        Dim htDossiers As New Hashtable(10000, 0.5)
        Dim htNAM As New Hashtable(10000, 0.5)
        Dim htTypeNotes As New Hashtable
        htTypeNotes.Add("ANA", "0")
        htTypeNotes.Add("BUT", "0")
        htTypeNotes.Add("EVO", "0")
        htTypeNotes.Add("HIS", "")
        htTypeNotes.Add("PLA", "0")
        htTypeNotes.Add("RAE", "")
        htTypeNotes.Add("RAF", "")
        htTypeNotes.Add("RAM", "")
        htTypeNotes.Add("SUB", "")
        Dim htCodes As New Hashtable
        htCodes.Add("ASSU", "8")
        htCodes.Add("CSST", "1")
        htCodes.Add("ERGO", "13")
        htCodes.Add("EXT", "12")
        htCodes.Add("MASO", "4")
        htCodes.Add("OSTE", "5")
        htCodes.Add("PCSS", "7")
        htCodes.Add("SAAN", "11")
        htCodes.Add("SAAQ", "2")

        If Client.Checked = False And RVs.Checked = False Then
            LockItems(False)
            Exit Sub 'Skip Clients, Dossiers & RVs
        End If

        If mapfromfile.Checked And IO.File.Exists("C:\transfer.dossiers") Then
            Dim fileNam() As String = IO.File.ReadAllText("C:\transfer.clients").Split(New Char() {vbCrLf})
            Dim fileDossiers() As String = IO.File.ReadAllText("C:\transfer.dossiers").Split(New Char() {vbCrLf})
            For i As Integer = 0 To fileNam.GetUpperBound(0) - 1 Step 2
                htNAM.Add(fileNam(i).Trim, fileNam(i + 1).Trim)
            Next i
            For i As Integer = 0 To fileDossiers.GetUpperBound(0) - 1 Step 2
                htDossiers.Add(fileDossiers(i).Trim, fileDossiers(i + 1).Trim)
            Next i
        Else
            dsDosClient = DBLinker.GetInstance(False).ReadDBForGrid("DOSClien", "*")
            DataGridView1.DataSource = dsDosClient
            DataGridView1.DataMember = "Table"

            Dim dsInfoClients As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("InfoClients", "*")
            Dim dsKeyPeople As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("KeyPeople", "*")
            n = 0
            Dim siteLesion As Integer = DBLinker.GetInstance(True).AddItemToADBList("siteLesion", "siteLesion", "Inconnu", "NositeLesion")
            Dim nbClients As Integer = 0
            Dim nbDossiers As Integer = 0
            Dim lastNbErreurs As Integer = 0
            With dsDosClient.Tables(0).Rows
                For i As Integer = .Count - 1 To 0 Step -1
                    Me.Text = "Clients (Restant/ajoutés) " & i & ":" & n
                    Application.DoEvents()
                    'Compte client
                    If .Item(i)("NOM").ToString().Trim <> "" Then
                        Dim noms() As String
                        If .Item(i)("NOM").ToString().IndexOf(",") < 0 Then
                            Noms = .Item(i)("NOM").ToString().Trim.Split(New Char() {" "}, 2)
                        Else
                            Noms = .Item(i)("NOM").ToString.Trim.Split(New Char() {","})
                        End If

                        'Dim ClientExists As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("InfoClients", "NoClient", "WHERE NAM='" & .Item(i)("NAM").ToString.Replace("-", "") & "' OR (Nom='" & Noms(0).Replace("'", "''") & "' AND Prenom='" & Noms(1).Replace("'", "''") & "' AND REPLACE(Telephones,'-','') LIKE 'Résidence:%" & .Item(i)("TELERES").ToString.Replace(":", "").Replace("'", "''").Replace("-", "").Trim & "%" & "')")
                        Dim isClient() As DataRow = dsInfoClients.Tables(0).Select("NAM='" & .Item(i)("NAM").ToString.Replace("-", "") & "' OR (Nom='" & Noms(0).Replace("'", "''") & "' AND Prenom='" & Noms(1).Replace("'", "''") & "' AND (Telephones LIKE '%" & .Item(i)("TELERES").ToString.Replace(":", "").Replace("'", "''").Trim & "%" & "'" & IIf(.Item(i)("DATE_NAI").ToString = "", "", " OR DateNaissance='" & .Item(i)("DATE_NAI").ToString & "'") & "))")

                        If Client.Checked = True AndAlso (IsClient Is Nothing OrElse IsClient.Length = 0) Then
                            nbClients += 1
                            Try
                                Dim noVille As String = DBLinker.GetInstance(True).AddItemToADBList("Villes", "NomVille", FirstLetterCapital(.Item(i)("AD2").ToString.ToLower.Trim, True), "noVille")
                                Dim noEmployeur As String = IIf(.Item(i)("ENTREPRISE").ToString().Trim.ToUpper = "NIL", "null", DBLinker.GetInstance(True).AddItemToADBList("Employeurs", "Employeur", FirstLetterCapital(.Item(i)("ENTREPRISE").ToString().Trim.ToLower), "noEmployeur"))
                                Dim noMetier As String = IIf(.Item(i)("TYPETRAVAI").ToString().Trim.ToUpper = "NIL", "null", DBLinker.GetInstance(True).AddItemToADBList("Metiers", "Metier", FirstLetterCapital(.Item(i)("TYPETRAVAI").ToString().Trim.ToLower), "noMetier"))
                                DBLinker.GetInstance(True).WriteDB("InfoClients", "Publipostage,Nom,Prenom,Adresse,NoVille,CodePostal,DateNaissance,NoEmployeur,NAM,SexeHomme,Telephones,NoMetier,AutreNom", _
                                "0,'" & FirstLetterCapital(Noms(0).ToLower, True).Replace("'", "''") & "','" & FirstLetterCapital(Noms(1).ToLower, True).Replace("'", "''") & "','" & FirstLetterCapital(.Item(i)("AD1").ToString.Trim.ToLower, True).Replace("'", "''") & "'," & NoVille & ",'" & .Item(i)("AD3").ToString.Replace(" ", "").Replace("-", "").ToUpper & "','" & .Item(i)("DATE_NAI").ToString().Trim & "'," & _
                                NoEmployeur & ",'" & .Item(i)("NAM").ToString().ToUpper.Replace("-", "").Trim & "'," & IIf(.Item(i)("SEXE").ToString().Trim = "M", "1", "0").ToString() & ",'Résidence:" & .Item(i)("TELERES").ToString.Replace(":", "").Replace("'", "''").Trim & IIf(.Item(i)("TELEBUR").ToString.Trim.ToUpper <> "NIL" And .Item(i)("TELEBUR").ToString <> "", "§Bureau:" & .Item(i)("TELEBUR").ToString().Replace(":", "").Replace("'", "''").Trim, "") & "'," & _
                                NoMetier & ",'" & FirstLetterCapital(.Item(i)("AUTRENOM").ToString().ToLower.Trim, True).Replace("'", "''") & "'")

                                If lastNbErreurs <> NbErreurs Then
                                    lastNbErreurs = NbErreurs
                                    nbClients = DBLinker.GetInstance(True).ReadDBForGrid("InfoClients", "MAX(NoClient)").Tables(0).Rows(0)(0)
                                End If

                                Dim t As DataRow = dsInfoClients.Tables(0).NewRow
                                t.Item("Publipostage") = 0
                                t.Item("Nom") = FirstLetterCapital(Noms(0).ToLower, True)
                                t.Item("Prenom") = FirstLetterCapital(Noms(1).ToLower, True)
                                t.Item("Adresse") = FirstLetterCapital(.Item(i)("AD1").ToString.Trim.ToLower, True)
                                If NoVille <> "null" Then t.Item("NoVille") = NoVille
                                t.Item("CodePostal") = .Item(i)("AD3").ToString.Replace(" ", "").Replace("-", "").ToUpper
                                If .Item(i)("DATE_NAI").ToString().Trim <> "" Then t.Item("DateNaissance") = .Item(i)("DATE_NAI").ToString().Trim
                                If NoEmployeur <> "null" Then t.Item("NoEmployeur") = NoEmployeur
                                t.Item("NAM") = .Item(i)("NAM").ToString().ToUpper.Replace("-", "").Trim
                                t.Item("SexeHomme") = IIf(.Item(i)("SEXE").ToString().Trim = "M", True, False)
                                t.Item("Telephones") = "Résidence:" & .Item(i)("TELERES").ToString.Replace(":", "").Trim & IIf(.Item(i)("TELEBUR").ToString.Trim.ToUpper <> "NIL" And .Item(i)("TELEBUR").ToString <> "", "§Bureau:" & .Item(i)("TELEBUR").ToString().Replace(":", "").Trim, "")
                                t.Item("AutreNom") = FirstLetterCapital(.Item(i)("AUTRENOM").ToString().ToLower.Trim, True)
                                t.Item("NoClient") = nbClients
                                If NoMetier <> "null" Then t.Item("NoMetier") = NoMetier
                                dsInfoClients.Tables(0).Rows.Add(t)
                                IsClient = dsInfoClients.Tables(0).Select("", "NoClient DESC")
                                DBLinker.GetInstance(True).WriteDB("ClientsAntecedents", "NoClient,Antecedents", IsClient(0)("NoClient") & ",''")
                                n += 1
                            Catch ex As Exception
                                erreurs.Text &= "Client de la table DOSClien - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                                erreurs.SelectionStart = erreurs.Text.Length - 1
                                NbErreurs += 1
                                Continue For
                            End Try
                        End If

                        If htNAM.ContainsKey(.Item(i)("NAM").ToString) = False AndAlso IsClient.Length > 0 Then htNAM.Add(.Item(i)("NAM").ToString, IsClient(0)("NoClient"))
                    End If
                Next i
                For i As Integer = 0 To .Count - 1
                    If .Item(i)("NOM").ToString().Trim <> "" Then
                        Me.Text = "Dossiers (Restant/ajoutés) " & (.Count - 1 - i) & ":" & n2
                        Application.DoEvents()

                        Dim dsNotes As DataSet = Nothing
                        If Client.Checked = True Then If .Item(i)("FILENOTE").ToString <> "" Then dsNotes = DBLinker.GetInstance(False).ReadDBForGrid(.Item(i)("FILENOTE").ToString, "*", "WHERE FILECLE='" & .Item(i)("FILECLE").ToString & "'")
                        Dim antecedents As String = ""
                        Dim notes As String = ""
                        Dim eval As String = ""
                        Dim analyse As String = ""
                        Dim but As String = ""
                        Dim plan As String = ""
                        Dim rapMed As String = ""
                        Dim rapFinal As String = ""
                        Dim rapEtape As String = ""

                        If dsNotes IsNot Nothing Then
                            With dsNotes.Tables(0).Rows
                                For j As Integer = 0 To .Count - 1
                                    Select Case .Item(j)("TYPENOT").ToString
                                        Case "HIS"
                                            Antecedents = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "ANA"
                                            Analyse = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "BUT"
                                            But = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "PLA"
                                            Plan = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "SUB"
                                            Eval = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "EVO"
                                            Notes = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "RAE"
                                            RapEtape = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "RAF"
                                            RapFinal = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "RAM"
                                            RapMed = FirstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                    End Select
                                Next j
                            End With
                        End If

                        'Dossier
                        If Client.Checked = True Then
                            If .Item(i)("NOM").ToString().Trim <> "" And htNAM.ContainsKey(.Item(i)("NAM").ToString) Then
                                Try
                                    'InfoFolder
                                    Dim noClient As Integer = htNAM(.Item(i)("NAM").ToString)
                                    Dim noCodification As Integer = IIf(htCodes.ContainsKey(.Item(i)("CODE").ToString.Trim), htCodes(.Item(i)("CODE").ToString.Trim), 1)
                                    Dim service As String = "Physiothérapie"
                                    Select Case .Item(i)("CODE").ToString.Trim
                                        Case "ERGO"
                                            Service = "Ergothérapie"
                                        Case "MASO"
                                            Service = "Massothérapie"
                                        Case "OSTE"
                                            Service = "Ostéopathie"
                                    End Select
                                    If .Item(i)("NOM").ToString.IndexOf("(ERGO)") > 0 Then Service = "Ergothérapie"

                                    Dim noTRP As Integer = GetNoNIP(htNIP, .Item(i)("NIPTHE").ToString.Trim, True)
                                    Dim duree As Integer = 0
                                    If .Item(i)("DUREEPREVU").ToString <> "" AndAlso Integer.TryParse(.Item(i)("DUREEPREVU").ToString, 0) Then Duree = Math.Ceiling(.Item(i)("DUREEPREVU") / 5)
                                    Dim frequence As Integer = 1
                                    Dim strFreq As String = .Item(i)("FREQUENCE").ToString
                                    If strFreq <> "" AndAlso Integer.TryParse(strFreq.Substring(0, 1), 0) Then Frequence = strFreq.Substring(0, 1)
                                    If Frequence < 1 Then Frequence = 1
                                    Dim noMedecin As Integer = 0
                                    Dim myMedecin() As DataRow = dsKeyPeople.Tables(0).Select("NoRef='" & .Item(i)("MEDECIN").ToString.Trim.Replace("'", "''") & "'")
                                    If MyMedecin IsNot Nothing AndAlso MyMedecin.Length = 1 Then NoMedecin = MyMedecin(0)("NoKP")
                                    nbDossiers += 1
                                    DBLinker.GetInstance(True).WriteDB("InfoFolders", "NoSiteLesion,Frequence,StatutOuvert, NoClient,NoCodification,DateRechute,DateRef,Diagnostic,Duree,NoKP,NoTRPTraitant,Service", _
                                    SiteLesion & "," & Frequence & "," & IIf(.Item(i)("FINTRAIT").ToString = "", "1", "0") & "," & NoClient & "," & NoCodification & "," & IIf(Date.TryParse(.Item(i)("DATERECHUT").ToString, Date.Today) = False, "null", "'" & .Item(i)("DATERECHUT").ToString & "'") & "," & IIf(Date.TryParse(.Item(i)("DATEREF").ToString, Date.Today) = False, "null", "'" & .Item(i)("DATEREF").ToString & "'") & ",'" & IIf(.Item(i)("DIAGNOSTIC").ToString.Trim.ToUpper = "NIL", "", FirstLetterCapital(.Item(i)("DIAGNOSTIC").ToString.Trim.ToLower, True).Replace("'", "''")) & "'," & Duree & "," & NoMedecin & "," & NoTRP & ",'" & Service & "'")
                                    Dim noFolder As Integer = nbDossiers
                                    Dim folderOpeningDate As String = IIf(.Item(i)("DEBUTRAIT") Is DBNull.Value OrElse .Item(i)("DEBUTRAIT").ToString = "", .Item(i)("DATECREAT").ToString, .Item(i)("DEBUTRAIT").ToString)
                                    DBLinker.GetInstance(True).WriteDB("StatFolders", "DateHeureCreation,NoAction,NoFolder,NoClient,NoTRP", "'" & folderOpeningDate & "',13," & NoFolder & "," & NoClient & "," & GetNoNIP(htNIP, .Item(i)("NIPCR").ToString.Trim, False))
                                    If .Item(i)("FINTRAIT").ToString <> "" Then DBLinker.GetInstance(True).WriteDB("StatFolders", "DateHeureCreation,NoAction,NoFolder,NoClient,NoTRP", "'" & .Item(i)("FINTRAIT").ToString & "',15," & NoFolder & "," & NoClient & "," & GetNoNIP(htNIP, .Item(i)("NIPCR").ToString.Trim, False))

                                    'FolderTextes
                                    Dim eabp As String = ""
                                    If Antecedents <> "" Then EABP &= "<b>Historique :</b><br>" & Antecedents & "<br><br>"
                                    If Eval <> "" Then EABP &= "<b>Évaluation :</b><br>" & Eval & "<br><br>"
                                    If Analyse <> "" Then EABP &= "<b>Analyse :</b><br>" & Analyse & "<br><br>"
                                    If But <> "" Then EABP &= "<b>But :</b><br>" & But & "<br><br>"
                                    If Plan <> "" Then EABP &= "<b>Plan de traitement :</b><br>" & Plan & "<br><br>"

                                    DBLinker.GetInstance(True).WriteDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte", "0," & NoFolder & ",'Évaluation,Analyse,But,Plan','" & Date.Today.Year & "/" & Date.Today.Month & "/" & Date.Today.Day & "','" & EABP.Replace("'", "''") & "'")
                                    DBLinker.GetInstance(True).WriteDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte", "1," & NoFolder & ",'Notes','" & Date.Today.Year & "/" & Date.Today.Month & "/" & Date.Today.Day & "','" & Notes.Replace("'", "''") & "'")
                                    DBLinker.GetInstance(True).WriteDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte", "2," & NoFolder & ",'Rapport au médecin','" & Date.Today.Year & "/" & Date.Today.Month & "/" & Date.Today.Day & "','" & RapMed.Replace("'", "''") & "'")
                                    Dim numEtape As String = .Item(i)("NUMETAPE").ToString
                                    If .Item(i)("CODE").ToString.Trim = "CSST" Then
                                        Dim expiryDate As Date = CDate(folderOpeningDate)
                                        Dim alarmDate As Date = ExpiryDate.AddMinutes(2)

                                        Dim clientName() As DataRow = dsInfoClients.Tables(0).Select("NoClient=" & NoClient)
                                        Dim lastRapport As Char = "I"c
                                        Dim dateEnded As String = "null"
                                        'Rapport CSST initial
                                        If NoFolder = 18387 Then
                                            Dim a As Byte = 0
                                        End If
                                        'If ExpiryDate.AddDays(7).CompareTo(CDate(dataDate.Text)) < 0 Then
                                        '    DateEnded = "'" & AffTextDate(ExpiryDate.AddDays(7)) & "'"
                                        'Else
                                        If Analyse <> "" And But <> "" And Plan <> "" Then
                                            DateEnded = "'" & AffTextDate(ExpiryDate) & "'"
                                            LastRapport = " "c
                                        Else
                                            If ExpiryDate.AddDays(15).Year < 2008 Then
                                                DateEnded = "'" & ExpiryDate.AddDays(15) & "'"
                                                LastRapport = " "c
                                            Else
                                                DateEnded = "null"
                                                LastRapport = "E"c
                                            End If
                                        End If
                                        'End If
                                        DBLinker.GetInstance(True).WriteDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,Texte", "3," & NoFolder & ",'Rapport CSST initial','" & AffTextDate(ExpiryDate) & "'," & DateEnded & ",''")
                                        If Not .Item(i)("RAPINITIAL").ToString <> "O" Then 'Si plus que le rapport initial
                                            'Rapport CSST d'étape
                                            If NumEtape <> "" AndAlso NumEtape > 0 Then
                                                For r As Integer = 1 To NumEtape
                                                    ExpiryDate = ExpiryDate.AddDays(21)
                                                    'If ExpiryDate.AddDays(7).CompareTo(CDate(dataDate.Text)) < 0 Then
                                                    '    DateEnded = "'" & AffTextDate(ExpiryDate.AddDays(7)) & "'"
                                                    'Else
                                                    If Notes.IndexOf("transfert du rapport d'etape #  " & r) <> -1 Or Notes.IndexOf("transfert du rapport d'etape # " & r) <> -1 Then
                                                        DateEnded = "'" & AffTextDate(ExpiryDate) & "'"
                                                        LastRapport = " "c
                                                    Else
                                                        If ExpiryDate.AddDays(15).Year < 2008 Then
                                                            DateEnded = "'" & ExpiryDate.AddDays(15) & "'"
                                                            LastRapport = " "c
                                                        Else
                                                            DateEnded = "null"
                                                            LastRapport = "E"c
                                                        End If
                                                    End If
                                                    '      End If

                                                    DBLinker.GetInstance(True).WriteDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,Texte", "4," & NoFolder & ",'Rapport CSST d''étape " & r & "','" & AffTextDate(ExpiryDate) & "'," & DateEnded & ",''")
                                                Next
                                            End If

                                            'Rapport CSST final
                                            If .Item(i)("FINTRAIT").ToString <> "" Then
                                                ExpiryDate = CDate(.Item(i)("FINTRAIT").ToString)
                                                If DateEnded = "null" Then DBLinker.ExecuteSQLScript("DELETE FROM FolderTextes WHERE DateFinished IS NULL AND NoFolder=" & NoFolder)
                                                'If ExpiryDate.AddDays(15).CompareTo(CDate(dataDate.Text)) < 0 Then
                                                '    DateEnded = "'" & AffTextDate(ExpiryDate.AddDays(15)) & "'"
                                                'Else
                                                If Notes.IndexOf("transfert du rapport final") <> -1 Then
                                                    DateEnded = "'" & AffTextDate(ExpiryDate) & "'"
                                                    LastRapport = " "
                                                Else
                                                    If ExpiryDate.AddDays(15).Year < 2008 Then
                                                        DateEnded = "'" & ExpiryDate.AddDays(15) & "'"
                                                        LastRapport = " "
                                                    Else
                                                        DateEnded = "null"
                                                        LastRapport = "F"c
                                                    End If
                                                End If
                                                'End If

                                                DBLinker.GetInstance(True).WriteDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,Texte", "5," & NoFolder & ",'Rapport CSST final','" & AffTextDate(ExpiryDate.AddDays(1)) & "'," & DateEnded & ",''")
                                            End If
                                        End If

                                        Dim alertAdded As Boolean = False
                                        ExpiryDate = CDate(folderOpeningDate)
                                        Select Case LastRapport
                                            Case "I"
                                                ExpiryDate = ExpiryDate.AddDays(7)
                                                REM If ExpiryDate.CompareTo(CDate(Me.dataDate.Text)) >= 0 Then 
                                                DBLinker.GetInstance(True).WriteDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", NoTRP & ",'OpenClientAccount','" & NoFolder & "',null,'" & AffTextDate(ExpiryDate.AddDays(1)) & "','" & AffTextDate(AlarmDate) & ":00:00:" & NoClient & ":" & NoFolder & ":True',1,1,'Le rapport CSST initial pour le client " & clientName(0)("Nom").ToString.Replace("'", "''") & "," & clientName(0)("Prenom").ToString.Replace("'", "''") & " du dossier #" & NoFolder & " est dû le " & AffTextDate(ExpiryDate) & "'") : AlertAdded = True
                                            Case "F"
                                                ExpiryDate = CDate(.Item(i)("FINTRAIT").ToString)
                                                AlarmDate = ExpiryDate.AddMinutes(2)
                                                ExpiryDate = ExpiryDate.AddDays(15)
                                                DBLinker.GetInstance(True).WriteDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", NoTRP & ",'OpenClientAccount','" & NoFolder & "',null,'" & AffTextDate(ExpiryDate.AddDays(1)) & "','" & AffTextDate(AlarmDate) & ":00:00:" & NoClient & ":" & NoFolder & ":False',1,1,'Le rapport CSST final pour le client " & clientName(0)("Nom").ToString.Replace("'", "''") & "," & clientName(0)("Prenom").ToString.Replace("'", "''") & " du dossier #" & NoFolder & " est dû le " & AffTextDate(ExpiryDate) & "'") : AlertAdded = True
                                            Case "E"
                                                ExpiryDate = ExpiryDate.AddDays(21 * NumEtape)
                                                AlarmDate = ExpiryDate.AddMinutes(2)
                                                ExpiryDate = ExpiryDate.AddDays(7)
                                                DBLinker.GetInstance(True).WriteDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", NoTRP & ",'OpenClientAccount','" & NoFolder & "',null,'" & AffTextDate(ExpiryDate.AddDays(1)) & "','" & AffTextDate(AlarmDate) & ":00:00:" & NoClient & ":" & NoFolder & ":True',1,1,'Le rapport CSST d''étape #" & NumEtape & " pour le client " & clientName(0)("Nom").ToString.Replace("'", "''") & "," & clientName(0)("Prenom").ToString.Replace("'", "''") & " du dossier #" & NoFolder & " est dû le " & AffTextDate(ExpiryDate) & "'") : AlertAdded = True
                                            Case Else
                                        End Select

                                        If AlertAdded Then
                                            Dim noUsersAlerts As DataTable = DBLinker.GetInstance(True).ReadDBForGrid("UsersAlerts", "TOP 1 *", "WHERE 1=1 ORDER BY NoUserAlert DESC").Tables(0)
                                            DBLinker.GetInstance(True).UpdateDB("InfoFolders", "LastCSSTAlert='" & NoUsersAlerts.Rows(0)("NoUserAlert") & "'", "NoFolder", NoFolder, False)
                                        End If
                                    End If

                                    Dim nbDossier As Integer = .Item(i)("RECLAMATIO") ' .Item(i)("NB_DOSSIER")
                                    'While htDossiers.ContainsKey(.Item(i)("NAM").ToString.Trim & IIf(NbDossier.ToString.Length = 1, "0" & NbDossier.ToString, NbDossier.ToString)) = True
                                    '    NbDossier -= 1
                                    'End While
                                    Dim whiteSpaces As String = ""
                                    For k As Integer = 1 To (14 - .Item(i)("NAM").ToString.Length)
                                        whiteSpaces &= " "
                                    Next k
                                    htDossiers.Add(.Item(i)("NAM").ToString & whiteSpaces & IIf(NbDossier.ToString.Length = 1, "0" & NbDossier.ToString, NbDossier.ToString), NoFolder)
                                    n2 += 1
                                Catch ex As Exception
                                    erreurs.Text &= "Dossier de la table DOSClien - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                                    erreurs.SelectionStart = erreurs.Text.Length - 1
                                    NbErreurs += 1
                                End Try
                            End If
                        Else
                            Dim nbDossier As Integer
                            Integer.TryParse(.Item(i)("RECLAMATIO").ToString, NbDossier) '.Item(i)("NB_DOSSIER")
                            'While htDossiers.ContainsKey(.Item(i)("NAM").ToString.Trim & IIf(NbDossier.ToString.Length = 1, "0" & NbDossier.ToString, NbDossier.ToString)) = True
                            '    NbDossier -= 1
                            'End While
                            Dim whiteSpaces As String = ""
                            For k As Integer = 1 To (14 - .Item(i)("NAM").ToString.Length)
                                whiteSpaces &= " "
                            Next k
                            htDossiers.Add(.Item(i)("NAM").ToString & whiteSpaces & IIf(NbDossier.ToString.Length = 1, "0" & NbDossier.ToString, NbDossier.ToString), .Count - i)
                        End If
                    End If
                Next i
            End With

            Dim sbWritingMap As New System.Text.StringBuilder()
            For Each myKey As DictionaryEntry In htDossiers
                sbWritingMap.AppendLine(myKey.Key)
                sbWritingMap.AppendLine(myKey.Value)
            Next
            IO.File.WriteAllText("C:\transfer.dossiers", sbWritingMap.ToString)
            sbWritingMap.Remove(0, sbWritingMap.Length)
            For Each myKey As DictionaryEntry In htNAM
                sbWritingMap.AppendLine(myKey.Key)
                sbWritingMap.AppendLine(myKey.Value)
            Next
            IO.File.WriteAllText("C:\transfer.clients", sbWritingMap.ToString)
        End If

        erreurs.Text &= vbCrLf & "Ended Clients generation : " & Now.ToString & vbCrLf
        erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de clients ajoutés : " & n & " Nombre de dossiers ajoutés :" & n2 & "  Nombre d'erreurs : " & NbErreurs & vbCrLf & vbCrLf & vbCrLf
        erreurs.SelectionStart = erreurs.Text.Length - 1

        If RVs.Checked = True Then
            NbErreurs = 0
            n = 0
            Dim dsInfoFolders As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("InfoFolders INNER JOIN InfoClients ON InfoClients.NoClient=InfoFolders.NoClient", "Nom,Prenom,InfoFolders.*, (SELECT TOP 1 DateHeureCreation FROM StatFolders WHERE StatFolders.NoFolder=InfoFolders.NoFolder AND NoAction=13) AS Debut, (SELECT TOP 1 DateHeureCreation FROM StatFolders WHERE StatFolders.NoFolder=InfoFolders.NoFolder AND NoAction=15) AS Fin")
            Dim objNoVisite As String = DBLinker.GetInstance(True).ReadDBForGrid("InfoVisites", "MAX(NoVisite)").Tables(0).Rows(0)(0).ToString
            Dim objNoFacture As String = DBLinker.GetInstance(True).ReadDBForGrid("StatFactures", "MAX(NoFacture)").Tables(0).Rows(0)(0).ToString
            If objNoVisite <> "" Then NoVisite = objNoVisite
            If objNoFacture <> "" Then NoFacture = objNoFacture

            erreurs.Text &= vbCrLf & "Starting RVs generation : " & Now.ToString & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1

            Dim dsVisites As DataSet = DBLinker.GetInstance(False).ReadDBForGrid("VISITES", "*")
            Dim dsPaiement As DataSet = DBLinker.GetInstance(False).ReadDBForGrid("PAIEMENT", "*")
            'Try
            TransferOneAgenda(n, "AGENDA1", htDossiers, htNAM, htNIP, htTRP, dsVisites, dsPaiement, dsInfoFolders, htCodes)
            TransferOneAgenda(n, "AGENDA2", htDossiers, htNAM, htNIP, htTRP, dsVisites, dsPaiement, dsInfoFolders, htCodes)
            TransferOneAgenda(n, "AGENDA3", htDossiers, htNAM, htNIP, htTRP, dsVisites, dsPaiement, dsInfoFolders, htCodes)
            TransferOneAgenda(n, "AGENDA4", htDossiers, htNAM, htNIP, htTRP, dsVisites, dsPaiement, dsInfoFolders, htCodes)
            If scripting.Checked Then IO.File.WriteAllText("C:\transfer-agendas" & Math.Ceiling((n / 1000)) & ".sql", sbSQL.ToString())
            'Catch ex As Exception
            'erreurs.Text &= vbCrLf & vbCrLf & ex.Message
            'erreurs.SelectionStart = erreurs.Text.Length - 1
            'End Try

            erreurs.Text &= vbCrLf & "Ended RVs generation : " & Now.ToString & vbCrLf
            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de rendez-vous ajoutés : " & n & "  Nombre d'erreurs : " & NbErreurs & vbCrLf & vbCrLf & vbCrLf
        End If


        erreurs.Text &= vbCrLf & vbCrLf & "Total time for transfer : " & Now.Subtract(StartingTime).Hours & " hour(s) " & (Now.Subtract(StartingTime).Minutes - Now.Subtract(StartingTime).Hours * 60) & " minutes"

        DBLinker.GetInstance(False).DBConnected = False
        DBLinker.GetInstance(True).DBConnected = False

        LockItems(False)
    End Sub

    Private Function getNoNIP(ByRef htNIP As Hashtable, ByVal nip As String, ByVal isTRP As Boolean) As Integer
        If htNIP.ContainsKey(nip) Then Return htNIP(nip)

        Dim userTable As DataTable = DBLinker.GetInstance(True).ReadDBForGrid("Utilisateurs", "NoUser", "WHERE (Nom='" & nip & "' AND Prenom='" & nip & "') OR NoPermis='" & nip & "'").Tables(0)
        Dim noUser As Integer = 0
        If userTable.Rows.Count = 0 Then
            DBLinker.GetInstance(True).WriteDB("Utilisateurs", "DroitAcces,IsTherapist,Cle,MDP,Nom,Prenom,DateDebut,DateFin,NoTitre,NoType,NoPermis,NoTypeEmploye", "'" & IIf(isTRP, "3", "") & "21111111111111111111111111111111111111111111111111111111111110111011111111111111111111111','" & isTRP & "','2008-04-0818:51:28.5312500','00','" & nip.Replace("'", "''") & "','" & nip.Replace("'", "''") & "','2008/01/01','2008/01/01'," & DBLinker.GetInstance(True).AddItemToADBList("Titres", "Titre", "Utilisateur supprimé", "NoTitre") & ",0,'',1")
            NoUser = DBLinker.GetInstance(True).ReadDBForGrid("Utilisateurs", "MAX(NoUser)").Tables(0).Rows(0)(0)
        Else
            NoUser = userTable.Rows(0)(0)
        End If

        htNIP.Add(nip, NoUser)

        Return NoUser
    End Function

    Private Sub transferOneAgenda(ByRef n As Integer, ByVal agendaName As String, ByRef htDossiers As Hashtable, ByRef htNAM As Hashtable, ByRef htNIP As Hashtable, ByRef htTRP As Hashtable, ByRef dsVisites As DataSet, ByRef dsPaiement As DataSet, ByRef dsInfoFolders As DataSet, ByRef htCodes As Hashtable)
        Dim dsAgenda1 As DataSet = DBLinker.GetInstance(False).ReadDBForGrid(AgendaName, "*")
        DataGridView1.DataSource = dsAgenda1
        DataGridView1.DataMember = "Table"

        Dim equipe As String = AgendaName.Substring(6)

        Dim nbErreurs As Integer = 0
        Dim curDateHeure As Date
        With dsAgenda1.Tables(0).Rows
            For i As Integer = .Count - 1 To 0 Step -4
                'Try
                CurDateHeure = CDate(.Item(i)("DATE").ToString)
                CurDateHeure = CurDateHeure.AddHours(23).AddMinutes(45)
                For j As Integer = 0 To 3
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "345", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "330", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "315", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "300", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "245", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "230", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "215", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "200", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "145", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "130", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "115", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "100", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "045", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "030", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "015", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                    TransferOneAgendaPart(dsAgenda1, i - j, n, "000", htDossiers, htNAM, htNIP, htTRP, dsVisites, CurDateHeure, dsPaiement, Equipe, dsInfoFolders, htCodes)
                    CurDateHeure = CurDateHeure.AddMinutes(-15)
                Next j
                'Catch ex As Exception
                '    erreurs.Text &= "Rendez-vous de la table " & AgendaName & " - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                '    erreurs.SelectionStart = erreurs.Text.Length - 1
                '    NbErreurs += 1
                'End Try
            Next i
        End With
    End Sub

    Private Sub transferOneAgendaPart(ByRef dsAgenda As DataSet, ByVal i As Integer, ByRef n As Integer, ByVal timeString As String, ByRef htDossiers As Hashtable, ByRef htNAM As Hashtable, ByRef htNIP As Hashtable, ByRef htTRP As Hashtable, ByRef dsVisites As DataSet, ByVal curDateHeure As Date, ByRef dsPaiement As DataSet, ByVal equipe As String, ByRef dsInfoFolders As DataSet, ByRef htCodes As Hashtable)
        If CurDateHeure.Equals(New Date(2008, 7, 14, 15, 15, 0)) Then
            Dim a As Byte = 0
        ElseIf CurDateHeure.Equals(New Date(2008, 7, 7, 17, 30, 0)) Then
            Dim a As Byte = 0
        ElseIf CurDateHeure.Equals(New Date(2008, 7, 2, 10, 0, 0)) Then
            Dim a As Byte = 0
        End If

        With dsAgenda.Tables(0).Rows
            Me.Text = "Agenda " & Equipe & " (Restant/ajoutés) " & i & ":" & n
            Application.DoEvents()
            Dim nosDossiers As String = .Item(i)("N" & timeString).ToString
            Dim textAgenda As String = .Item(i)("H" & timeString).ToString.Replace("0", "")
            If textAgenda = "" Then Exit Sub
            If textAgenda.Replace("1", "").Trim = "" Then Exit Sub

            Dim noDossier1 As String = ""
            Dim noDossier2 As String = ""
            Dim noDossier3 As String = ""
            If nosDossiers.Length < 17 Then
                noDossier1 = nosDossiers
            Else
                noDossier1 = nosDossiers.Substring(0, 16)
                If nosDossiers.Length < 33 Then
                    noDossier2 = nosDossiers.Substring(16)
                Else
                    noDossier2 = nosDossiers.Substring(16, 16)
                    noDossier3 = nosDossiers.Substring(32)
                End If
            End If

            Dim textAgenda1 As String = ""
            Dim textAgenda2 As String = ""
            Dim textAgenda3 As String = ""
            If textAgenda.Length < 41 Then
                textAgenda1 = textAgenda.Trim
            Else
                textAgenda1 = textAgenda.Substring(0, 40).Trim
                If textAgenda.Length < 81 Then
                    textAgenda2 = textAgenda.Substring(40).Trim
                    'If textAgenda2 <> "" And StartsWithNumeric(textAgenda2) = False And noDossier2 = "" And (textAgenda1 = "" Or StartsWithNumeric(textAgenda1)) And noDossier1 <> "" Then
                    '    noDossier2 = noDossier1
                    '    noDossier1 = ""
                    'End If
                Else
                    textAgenda2 = textAgenda.Substring(40, 40).Trim
                    textAgenda3 = textAgenda.Substring(80).Trim
                    'If textAgenda2 <> "" And StartsWithNumeric(textAgenda2) = False And noDossier2 = "" And (textAgenda1 = "" Or StartsWithNumeric(textAgenda1)) And noDossier1 <> "" Then
                    '    noDossier2 = noDossier1
                    '    noDossier1 = ""
                    'End If
                End If
            End If

            Dim NoFolder1, NoClient1, NoFolder2, NoClient2, NoFolder3, noClient3 As Integer
            If noDossier1 <> "" AndAlso htNAM.ContainsKey(noDossier1.Substring(0, 14).Trim) Then NoClient1 = htNAM(noDossier1.Substring(0, 14).Trim)
            If noDossier2 <> "" AndAlso htNAM.ContainsKey(noDossier2.Substring(0, 14).Trim) Then NoClient2 = htNAM(noDossier2.Substring(0, 14).Trim)
            If noDossier3 <> "" AndAlso htNAM.ContainsKey(noDossier3.Substring(0, 14).Trim) Then NoClient3 = htNAM(noDossier3.Substring(0, 14).Trim)

            Dim drVisite1 As DataRow = Nothing
            Dim drVisite2 As DataRow = Nothing
            Dim drVisite3 As DataRow = Nothing

            Dim service1 As String = "Physiothérapie"
            Dim service2 As String = "Physiothérapie"
            Dim service3 As String = "Physiothérapie"
            If textAgenda1 <> "" And NoClient1 > 0 Then
                drVisite1 = GetDrVisite(dsVisites, CurDateHeure, noDossier1.Substring(0, 14), Equipe, 1)
                If drVisite1 IsNot Nothing Then
                    Select Case drVisite1("CODE").ToString
                        Case "ERGO"
                            Service1 = "Ergothérapie"
                        Case "MASO"
                            Service1 = "Massothérapie"
                        Case "OSTE"
                            Service1 = "Ostéopathie"
                    End Select
                End If
                If textAgenda1.IndexOf("(ERGO)") > 0 Then Service1 = "Ergothérapie"
                NoFolder1 = GetNoFolder(GetNoTRP(drVisite1, htNIP, htTRP, Equipe, 1), dsInfoFolders, NoClient1, CurDateHeure, drVisite1, htCodes, Service1)
            End If
            If textAgenda2 <> "" And NoClient2 > 0 Then
                drVisite2 = GetDrVisite(dsVisites, CurDateHeure, noDossier2.Substring(0, 14), Equipe, 2)
                If drVisite2 IsNot Nothing Then
                    Select Case drVisite2("CODE").ToString
                        Case "ERGO"
                            Service2 = "Ergothérapie"
                        Case "MASO"
                            Service2 = "Massothérapie"
                        Case "OSTE"
                            Service2 = "Ostéopathie"
                    End Select
                End If
                If textAgenda2.IndexOf("(ERGO)") > 0 Then Service2 = "Ergothérapie"
                NoFolder2 = GetNoFolder(GetNoTRP(drVisite2, htNIP, htTRP, Equipe, 2), dsInfoFolders, NoClient2, CurDateHeure, drVisite2, htCodes, Service2)
            End If
            If textAgenda3 <> "" And NoClient3 > 0 Then
                drVisite3 = GetDrVisite(dsVisites, CurDateHeure, noDossier3.Substring(0, 14), Equipe, 3)
                If drVisite3 IsNot Nothing Then
                    Select Case drVisite3("CODE").ToString
                        Case "ERGO"
                            Service3 = "Ergothérapie"
                        Case "MASO"
                            Service3 = "Massothérapie"
                        Case "OSTE"
                            Service3 = "Ostéopathie"
                    End Select
                End If
                If textAgenda3.IndexOf("(ERGO)") > 0 Then Service3 = "Ergothérapie"
                NoFolder3 = GetNoFolder(GetNoTRP(drVisite3, htNIP, htTRP, Equipe, 3), dsInfoFolders, NoClient3, CurDateHeure, drVisite3, htCodes, Service3)
            End If

            If textAgenda1 <> "" And StartsWithNumeric(textAgenda1) = False Then TransferOneVisite(n, 1, timeString, dsAgenda, i, drVisite1, Equipe, CurDateHeure, htTRP, htNIP, dsPaiement, NoClient1, NoFolder1, dsInfoFolders, textAgenda1, dsVisites, htNAM, htCodes, Service1)
            If textAgenda2 <> "" And StartsWithNumeric(textAgenda2) = False Then TransferOneVisite(n, 2, timeString, dsAgenda, i, drVisite2, Equipe, CurDateHeure, htTRP, htNIP, dsPaiement, NoClient2, NoFolder2, dsInfoFolders, textAgenda2, dsVisites, htNAM, htCodes, Service2)
            If textAgenda3 <> "" And StartsWithNumeric(textAgenda3) = False Then TransferOneVisite(n, 3, timeString, dsAgenda, i, drVisite3, Equipe, CurDateHeure, htTRP, htNIP, dsPaiement, NoClient3, NoFolder3, dsInfoFolders, textAgenda3, dsVisites, htNAM, htCodes, Service3)
        End With
    End Sub

    Private Function getDrVisite(ByRef dsVisites As DataSet, ByVal curDateHeure As Date, ByVal nam As String, ByVal equipe As Byte, ByVal position As Byte) As DataRow
        Dim rows() As DataRow = dsVisites.Tables(0).Select("DATEVISITE='" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & "' AND HEUREVIS='" & CurDateHeure.Hour & "," & CurDateHeure.Minute & "' AND NAM='" & NAM & "'")
        Dim drVisite As DataRow = Nothing
        If rows IsNot Nothing AndAlso rows.Length > 0 Then drVisite = rows(0)

        If drVisite Is Nothing Then
            rows = dsVisites.Tables(0).Select("DATEVISITE='" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & "' AND HEUREVIS='" & CurDateHeure.Hour & "," & CurDateHeure.Minute & "' AND PHYSIO='" & position & "' AND (EQUIPE=' " & Equipe & "')")
            If rows IsNot Nothing AndAlso rows.Length > 0 Then drVisite = rows(0)
        End If


        Return drVisite
    End Function

    Private Function getNoTRP(ByRef drVisite As DataRow, ByRef htNIP As Hashtable, ByRef htTRP As Hashtable, ByVal equipe As Byte, ByVal position As Byte) As Integer
        Dim noTRP As Integer = 0
        If drVisite IsNot Nothing Then
            NoTRP = GetNoNIP(htNIP, drVisite("NIPTHE").ToString.Trim, True)
        Else
            REM this one can't be taken from GetNoNIP because it's not a nip but an Equipe & Position
            NoTRP = IIf(htTRP.ContainsKey(Equipe & "-" & position), htTRP(Equipe & "-" & position), 0)
        End If

        Return NoTRP
    End Function

    Private Function getNoFolder(ByVal noTRP As Integer, ByRef dsInfoFolders As DataSet, ByVal noClient1 As Integer, ByVal curDateHeure As Date, ByRef drVisite As DataRow, ByRef htCodes As Hashtable, ByVal service As String) As Integer
        Dim rows() As DataRow = dsInfoFolders.Tables(0).Select("NoClient=" & NoClient1)
        If rows IsNot Nothing AndAlso rows.Length > 0 Then
            Dim noCodification As Integer = 0
            If drVisite IsNot Nothing Then
                If htCodes.ContainsKey(drVisite("CODE").ToString.Trim) Then
                    NoCodification = htCodes(drVisite("CODE").ToString.Trim)
                Else
                    NoCodification = 12
                End If
            End If

            For Each curRow As DataRow In rows
                If NoTRP = curRow("NoTRPTraitant") Then
                    If drVisite IsNot Nothing Then
                        If curRow.Item("NoCodification") = NoCodification AndAlso curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(CurDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(CurDateHeure) >= 0) Then
                            Return curRow("NoFolder")
                        End If
                    Else
                        If curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(CurDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(CurDateHeure) >= 0) Then
                            Return curRow("NoFolder")
                        End If
                    End If
                Else
                    If drVisite IsNot Nothing Then
                        If curRow.Item("NoCodification") = NoCodification AndAlso curRow("Service") = Service AndAlso curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(CurDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(CurDateHeure) > 0) Then
                            Return curRow("NoFolder")
                        End If
                    Else
                        If curRow("Service") = Service AndAlso curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(CurDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(CurDateHeure) >= 0) Then
                            Return curRow("NoFolder")
                        End If
                    End If
                End If
            Next
        End If

        Return 0
    End Function

    Private Function startsWithNumeric(ByVal myText As String) As Boolean
        If myText.StartsWith("0") Then Return True
        If myText.StartsWith("1") Then Return True
        If myText.StartsWith("2") Then Return True
        If myText.StartsWith("3") Then Return True
        If myText.StartsWith("4") Then Return True
        If myText.StartsWith("5") Then Return True
        If myText.StartsWith("6") Then Return True
        If myText.StartsWith("7") Then Return True
        If myText.StartsWith("8") Then Return True
        If myText.StartsWith("9") Then Return True

        Return False
    End Function

    Private Sub transferOneVisite(ByRef n As Integer, ByVal position As Byte, ByVal timeString As String, ByRef dsAgenda As DataSet, ByVal i As Integer, ByRef drVisite As DataRow, ByVal equipe As Integer, ByVal curDateHeure As Date, ByRef htTRP As Hashtable, ByRef htNIP As Hashtable, ByRef dsPaiement As DataSet, ByVal noClient1 As Integer, ByVal noFolder1 As Integer, ByRef dsInfoFolders As DataSet, ByVal textAgenda As String, ByRef dsVisites As DataSet, ByRef htNAM As Hashtable, ByRef htCodes As Hashtable, ByVal service As String)
        Try
            Dim statut As String = ""
            Dim noStatut As Integer = 0
            Dim noTRP As Integer
            Dim drPaiement As DataRow

            With dsAgenda.Tables(0).Rows
                Statut = .Item(i)("C" & timeString).ToString
                If Statut.Length > (position - 1) * 3 Then Statut = Statut.Substring((position - 1) * 3)
                If Statut.Length > 3 Then Statut = Statut.Substring(0, 3)
                If statut.IndexOf("C") >= 0 Then Exit Sub 'Quitte, car il ne s'agit pas d'un RV
                If Statut = "" Then
                    NoStatut = 3
                Else
                    Select Case Statut.Substring(0, 1)
                        Case "A"
                            NoStatut = 1
                        Case "X"
                            NoStatut = 2
                        Case " "
                            NoStatut = 3
                        Case "P", "Z"
                            NoStatut = 4
                            If InStr(Statut, "A") > 0 Then NoStatut = 1
                    End Select
                End If
                If NoStatut = 0 Then NoStatut = 3

                If NoStatut = 3 And Date.Compare(CurDateHeure, CDate(dataDate.Text)) < 0 Then Exit Sub

                If drVisite Is Nothing Then
                    drVisite = GetDrVisite(dsVisites, CurDateHeure, "------------", Equipe, position)
                    If drVisite IsNot Nothing AndAlso NoClient1 = 0 Then
                        If htNAM.ContainsKey(drVisite("NAM").ToString.Trim) = False Then Exit Sub

                        NoClient1 = htNAM(drVisite("NAM").ToString.Trim)
                    End If
                    If NoClient1 > 0 And NoFolder1 = 0 Then NoFolder1 = GetNoFolder(GetNoTRP(drVisite, htNIP, htTRP, Equipe, position), dsInfoFolders, NoClient1, CurDateHeure, drVisite, htCodes, Service)
                End If

                'S'assure que le numéro du dossier avait été trouvé
                If NoFolder1 = 0 And textAgenda.IndexOf(",") <> -1 Then
                    Dim externes() As Integer = {12, 4, 5, 8, 9, 10, 13}
                    Dim noms() As String = textAgenda.Split(",")
                    Dim nbFoundTRP, nbFoundExt, nbFoundNonExt As Integer
                    Dim lastFound As Byte = 0
                    Dim dossiers() As DataRow = dsInfoFolders.Tables(0).Select("Nom='" & Noms(0).Replace("'", "''") & "' AND Prenom='" & Noms(1).Replace("'", "''") & "'")
                    If dossiers IsNot Nothing AndAlso dossiers.Length <> 0 Then
                        For d As Integer = 0 To dossiers.GetUpperBound(0)
                            If dossiers(d)("NoTRPTraitant") = GetNoTRP(drVisite, htNIP, htTRP, Equipe, position) Then
                                NoFolder1 = dossiers(d)("NoFolder")
                                NoClient1 = dossiers(d)("NoClient")
                                nbFoundTRP += 1
                                lastFound = 1
                            Else
                                If Statut.IndexOf("$") >= 0 And Array.IndexOf(externes, dossiers(d)("NoCodification")) >= 0 Then
                                    NoFolder1 = dossiers(d)("NoFolder")
                                    NoClient1 = dossiers(d)("NoClient")
                                    nbFoundExt += 1
                                    lastFound = 2
                                ElseIf Statut.IndexOf("$") < 0 And Array.IndexOf(externes, dossiers(d)("NoCodification")) < 0 Then
                                    NoFolder1 = dossiers(d)("NoFolder")
                                    NoClient1 = dossiers(d)("NoClient")
                                    nbFoundNonExt += 1
                                    lastFound = 3
                                End If
                            End If
                        Next d

                        If lastFound = 1 And nbFoundTRP > 1 Then Exit Sub
                        If lastFound = 2 And nbFoundExt > 1 Then Exit Sub
                        If lastFound = 3 And nbFoundNonExt > 1 Then Exit Sub
                    End If
                End If

                Dim rows() As DataRow
                NoTRP = 0
                drPaiement = Nothing
                If drVisite IsNot Nothing Then
                    If NoStatut = 3 Then NoStatut = 4
                    NoTRP = GetNoNIP(htNIP, drVisite("NIPTHE").ToString.Trim, True)
                    If drVisite("DATEPMT").ToString <> "" Then
                        rows = dsPaiement.Tables(0).Select("DATE='" & drVisite("DATEPMT").ToString & "' AND CODE='" & drVisite("CODE").ToString & "' AND NAM='" & drVisite("NAM").ToString & "'")
                        If rows IsNot Nothing AndAlso rows.Length > 0 Then drPaiement = rows(0)
                    End If
                Else
                    REM this one can't be taken from GetNoNIP because it's not a nip but an Equipe & Position
                    NoTRP = IIf(htTRP.ContainsKey(Equipe & "-" & position), htTRP(Equipe & "-" & position), 0)
                End If

                If NoFolder1 = 0 Then
                    DBLinker.GetInstance(True).WriteDB("Agenda", "DateHeure,Periode,NoTRP,Reserve,NoStatut", "'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "',15," & NoTRP & ",'" & textAgenda.Replace("'", "''") & "',6")
                    Exit Sub 'Quitte si toujours aucun numéro de dossier
                End If

                NoVisite += 1
                If scripting.Checked Then
                    sbSQL.Append("INSERT INTO InfoVisites (NoClient,NoFolder,NoStatut,NoFacture,NoTRP,DateHeure,Periode,Service,Confirmed,Evaluation) VALUES(").Append(NoClient1).Append(",").Append(NoFolder1).Append(",").Append(NoStatut).Append(",0,").Append(NoTRP).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append(" ").Append(CurDateHeure.Hour).Append(":").Append(CurDateHeure.Minute).Append("',15,'Physiothérapie',").Append(IIf(CurDateHeure.Date.CompareTo(CDate(dataDate.Text)) < 0, 1, 0)).Append(",").Append(IIf(Statut.IndexOf("I") >= 0, 1, 0)).AppendLine(")")
                Else
                    DBLinker.GetInstance(True).WriteDB("InfoVisites", "NoClient,NoFolder,NoStatut,NoFacture,NoTRP,DateHeure,Periode,Service,Confirmed,Evaluation", _
                    NoClient1 & "," & NoFolder1 & "," & NoStatut & ",0," & NoTRP & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "',15,'" & Service & "'," & IIf(CurDateHeure.Date.CompareTo(CDate(Me.dataDate.Text)) < 0, 1, 0) & "," & IIf(Statut.IndexOf("I") >= 0, 1, 0))
                End If

                Dim billStats As Boolean = False
                NoFacture += 1
                If drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString = "0" Then Exit Sub

                If drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString <> "" AndAlso drPaiement IsNot Nothing Then
                    Dim totalPaiement As Double = drVisite("PAIEMENT")

                    If TotalPaiement < drVisite("MONTANT") Then
                        If scripting.Checked Then
                            sbSQL.Append("INSERT INTO Factures (MontantPaiementUser,MontantPaiementClinique,ParNoClinique,IsSouffrance,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere,NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP) VALUES(0,0,0,0,'',0,'',0,0,0,0,0,0,0,0,0,").Append(NoFacture).Append(",").Append(GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append("',").Append(NoFolder1).Append(",").Append(NoClient1).Append(",").Append(drVisite("MONTANT").ToString.Replace(",", ".")).Append( _
                                    ",'").Append(Service).Append("',").Append(IIf(drPaiement("MONTANT") > drVisite("MONTANT"), drVisite("MONTANT").ToString.Replace(",", "."), drPaiement("MONTANT").ToString.Replace(",", "."))).Append(",").Append(NoVisite).Append(",0,").Append(NoClient1).AppendLine(",0,0)")
                        Else
                            DBLinker.GetInstance(True).WriteDB("Factures", "MontantPaiementUser,MontantPaiementClinique,ParNoClinique,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere" _
                                    & ",NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP,IsSouffrance", _
                                    "0,0,0,'',0,'',0,0,0,0,0,0,0,0,0," & _
                                    NoFacture & "," & GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & "'," & NoFolder1 & "," & NoClient1 & "," & drVisite("MONTANT").ToString.Replace(",", ".") & _
                                    ",'" & Service & "'," & TotalPaiement.ToString.Replace(",", ".") & "," & NoVisite & ",0," & NoClient1 & ",0,0," & IIf(drVisite("AJUSTEMEN") < 0, "1", "0"))
                        End If
                    End If

                    If TotalPaiement > drVisite("MONTANT") Then TotalPaiement = drVisite("MONTANT")

                    Dim typePaiement As String = ""
                    Select Case drPaiement("TYPEPMT").ToString
                        Case "AR"
                            TypePaiement = "Argent"
                        Case "CH"
                            TypePaiement = "Chèque"
                        Case "MC"
                            TypePaiement = "MasterCard"
                        Case "VI"
                            TypePaiement = "Visa"
                    End Select
                    If drPaiement("NOREF").ToString.StartsWith("D") Then TypePaiement = "Débit"

                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO StatPaiements (Commentaires,NoAction,ParNoUser,ParNoClinique,NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,EntitePayeur,ParNoClient,ParNoKP) VALUES('',12,0,0,").Append(GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append(" ").Append(CurDateHeure.Hour).Append(":").Append(CurDateHeure.Minute).Append("',").Append(NoFacture).Append(",").Append(IIf(drPaiement("MONTANT") > drVisite("MONTANT"), drVisite("MONTANT").ToString.Replace(",", "."), drPaiement("MONTANT").ToString.Replace(",", "."))).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append(" ").Append(CurDateHeure.Hour).Append(":").Append(CurDateHeure.Minute).Append("','").Append(TypePaiement).Append("',").Append(NoClient1).Append(",").Append(NoFolder1).Append(",'C',").Append(NoClient1).AppendLine(",0)")
                    Else
                        DBLinker.GetInstance(True).WriteDB("StatPaiements", "Commentaires,NoAction,ParNoUser,ParNoClinique," & _
                                        "NoRecu,NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,EntitePayeur,ParNoClient,ParNoKP", _
                                        "'',12,0,0," & _
                                        "'" & IIf(drVisite("NORECU") Is DBNull.Value, "", drVisite("NORECU").ToString.Trim.Replace("'", "''")) & "'," & GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "'," & NoFacture & "," & TotalPaiement.ToString.Replace(",", ".") & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "','" & TypePaiement & "'," & NoClient1 & "," & NoFolder1 & ",'C'," & NoClient1 & ",0")
                    End If

                    BillStats = True
                ElseIf drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString <> "" AndAlso drPaiement Is Nothing AndAlso drVisite("PAIEMENT") > 0 Then
                    If drVisite("PAIEMENT") < drVisite("MONTANT") Then
                        If scripting.Checked Then
                            sbSQL.Append("INSERT INTO Factures (MontantPaiementUser,MontantPaiementClinique,ParNoClinique,IsSouffrance,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere,NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP) VALUES(0,0,0,0,'',0,'',0,0,0,0,0,0,0,0,0,").Append(NoFacture).Append(",").Append(GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append("',").Append(NoFolder1).Append(",").Append(NoClient1).Append(",").Append(drVisite("MONTANT").ToString.Replace(",", ".")).Append( _
                                    ",'").Append(Service).Append("',").Append(IIf(drPaiement("MONTANT") > drVisite("MONTANT"), drVisite("MONTANT").ToString.Replace(",", "."), drPaiement("MONTANT").ToString.Replace(",", "."))).Append(",").Append(NoVisite).Append(",0,").Append(NoClient1).AppendLine(",0,0)")
                        Else
                            DBLinker.GetInstance(True).WriteDB("Factures", "MontantPaiementUser,MontantPaiementClinique,ParNoClinique,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere" _
                                    & ",NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP,IsSouffrance", _
                                    "0,0,0,'',0,'',0,0,0,0,0,0,0,0,0," & _
                                    NoFacture & "," & GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & "'," & NoFolder1 & "," & NoClient1 & "," & drVisite("MONTANT").ToString.Replace(",", ".") & _
                                    ",'" & Service & "'," & drVisite("PAIEMENT").ToString.Replace(",", ".") & "," & NoVisite & ",0," & NoClient1 & ",0,0," & IIf(drVisite("AJUSTEMEN") < 0, "1", "0"))
                        End If
                    End If

                    Dim typePaiement As String = "Inconnu"

                    DBLinker.GetInstance(True).WriteDB("StatPaiements", "Commentaires,NoAction,ParNoUser,ParNoClinique," & _
                                        "NoRecu,NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,EntitePayeur,ParNoClient,ParNoKP", _
                                        "'',12,0,0," & _
                                        "'" & IIf(drVisite("NORECU") Is DBNull.Value, "", drVisite("NORECU").ToString.Trim.Replace("'", "''")) & "'," & GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "'," & NoFacture & "," & drVisite("PAIEMENT").ToString.Replace(",", ".") & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "','" & TypePaiement & "'," & NoClient1 & "," & NoFolder1 & ",'C'," & NoClient1 & ",0")
                    BillStats = True
                ElseIf drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString <> "" Then
                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO Factures (MontantPaiementUser,MontantPaiementClinique,ParNoClinique,IsSouffrance,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere,NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP) VALUES(0,0,0,0,'',0,'',0,0,0,0,0,0,0,0,0,").Append(NoFacture).Append(",").Append(GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append("',").Append(NoFolder1).Append(",").Append(NoClient1).Append(",").Append(drVisite("MONTANT").ToString.Replace(",", ".")).Append( _
                                    ",'").Append(Service).Append("',0,").Append(NoVisite).Append(",0,").Append(NoClient1).AppendLine(",0,0)")
                    Else
                        DBLinker.GetInstance(True).WriteDB("Factures", "MontantPaiementUser,MontantPaiementClinique,ParNoClinique,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere" _
                                    & ",NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP,IsSouffrance", _
                                    "0,0,0,'',0,'',0,0,0,0,0,0,0,0,0," & _
                                    NoFacture & "," & GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & "'," & NoFolder1 & "," & NoClient1 & "," & drVisite("MONTANT").ToString.Replace(",", ".") & _
                                    ",'" & Service & "',0," & NoVisite & ",0," & NoClient1 & ",0,0," & IIf(drVisite("AJUSTEMEN") < 0, "1", "0"))
                    End If
                    BillStats = True
                End If

                If BillStats Then 'Ajoute les stat de la facture
                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO StatFactures (NoAction,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,ParNoUser,NoKP,NoUserFacture,NoFactureTransfere,Commentaires,ParNoClinique,NoUser,DateHeureCreation,NoFacture,NoFolder,NoClient,MontantFacture,TypeFacture,NoVisite,DateFacture,ParNoKp,ParNoClient,EntitePayeur) VALUES(5,'',0,'',0,0,0,0,0,0,0,'',0,").Append(GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append(" ").Append(CurDateHeure.Hour).Append(":").Append(CurDateHeure.Minute).Append("',").Append(NoFacture).Append(",").Append(NoFolder1).Append(",").Append(NoClient1).Append(",").Append(drVisite("MONTANT").ToString.Replace(",", ".")).Append( _
                                    ",'").Append(Service).Append("',").Append(NoVisite).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append(" ").Append(CurDateHeure.Hour).Append(":").Append(CurDateHeure.Minute).Append("',0,").Append(NoClient1).AppendLine(",'C')")
                        sbSQL.Append("UPDATE InfoVisites SET NoFacture=").Append(NoFacture).Append(" WHERE NoVisite=").AppendLine(NoVisite)
                    Else
                        DBLinker.GetInstance(True).WriteDB("StatFactures", "NoAction,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,ParNoUser,NoKP,NoUserFacture,NoFactureTransfere,Commentaires,ParNoClinique," & _
                                    "NoUser,DateHeureCreation,NoFacture,NoFolder,NoClient,MontantFacture,TypeFacture,NoVisite,DateFacture,ParNoKp,ParNoClient,EntitePayeur", _
                                    "5,'',0,'',0,0,0,0,0,0,0,'',0," & _
                                    GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "'," & NoFacture & "," & NoFolder1 & "," & NoClient1 & "," & drVisite("MONTANT").ToString.Replace(",", ".") & _
                                    ",'" & Service & "'," & NoVisite & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "',0," & NoClient1 & ",'C'")
                        DBLinker.GetInstance(True).UpdateDB("InfoVisites", "NoFacture=" & NoFacture, "NoVisite", NoVisite, False)
                    End If
                End If

                Dim noCreateur As Integer = 0
                If drVisite IsNot Nothing Then NoCreateur = GetNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)
                If NoCreateur > 0 Then
                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO StatVisites (NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder) VALUES(").Append(NoCreateur).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append(" ").Append(CurDateHeure.Hour).Append(":").Append(CurDateHeure.Minute).Append("',6,").Append(NoVisite).Append(",").Append(NoClient1).Append(",").Append(NoFolder1).AppendLine(")")
                    Else
                        DBLinker.GetInstance(True).WriteDB("StatVisites", "NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder", _
                                        NoCreateur & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "',6," & NoVisite & "," & NoClient1 & "," & NoFolder1)
                    End If

                    If NoStatut <> 3 Then
                        If scripting.Checked Then
                            sbSQL.Append("INSERT INTO StatVisites (NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder) VALUES(").Append(NoCreateur).Append(",'").Append(CurDateHeure.Year).Append("/").Append(CurDateHeure.Month).Append("/").Append(CurDateHeure.Day).Append(" ").Append(CurDateHeure.Hour).Append(":").Append(CurDateHeure.Minute).Append("',").Append(NoStatut).Append(",").Append(NoVisite).Append(",").Append(NoClient1).Append(",").Append(NoFolder1).AppendLine(")")
                        Else
                            DBLinker.GetInstance(True).WriteDB("StatVisites", "NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder", _
                                            NoCreateur & ",'" & CurDateHeure.Year & "/" & CurDateHeure.Month & "/" & CurDateHeure.Day & " " & CurDateHeure.Hour & ":" & CurDateHeure.Minute & "'," & NoStatut & "," & NoVisite & "," & NoClient1 & "," & NoFolder1)
                        End If
                    End If
                End If
            End With
            n += 1
        Catch ex As Exception
            ex = ex
            erreurs.Text &= "Rendez-vous de la table AGENDA" & Equipe & " - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf
            If ex.InnerException IsNot Nothing Then erreurs.Text &= ex.InnerException.Message & vbCrLf
            erreurs.Text &= vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1
            NbErreurs += 1
        End Try

        If scripting.Checked AndAlso n Mod 1000 = 0 Then
            IO.File.WriteAllText("C:\transfer-agendas" & (n / 1000) & ".sql", sbSQL.ToString())
            sbSQL = New System.Text.StringBuilder
            GC.Collect(5)
        End If
    End Sub

    Public Function firstLetterCapital(ByVal stringToConvert As String, Optional ByVal allWords As Boolean = False) As String
        Dim i As Integer
        Dim previousChar As Char = Nothing
        Dim myChar As Char = Nothing
        Dim LeftPart, rightPart As String
        Dim upTo As Integer = 0

        For i = 1 To StringToConvert.Length - 1
            MyChar = Mid(StringToConvert, i, 1) : LeftPart = "" : RightPart = ""
            If PreviousChar = Nothing Or PreviousChar = " " Or PreviousChar = "-" Or PreviousChar = "'" Then
                UpTo += 1
                If i > 1 Then LeftPart = StringToConvert.Substring(0, i - 1)
                If i < StringToConvert.Length Then RightPart = StringToConvert.Substring(i, StringToConvert.Length - i)
                StringToConvert = LeftPart & Char.ToUpper(MyChar) & RightPart
                If AllWords = False And UpTo = 1 Then Exit For
            End If
            PreviousChar = MyChar
        Next i

        Return StringToConvert
    End Function

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim curLog As String = erreurs.Text
        IO.File.WriteAllText("C:\transfer.log", CurLog)
        End
    End Sub

    Public Function affTextDate(ByVal dateToConvert As Date, Optional ByVal options As DateFormat.TextDateoptions = DateFormat.TextDateoptions.YYYYMMDD, Optional ByVal shortSeparator As String = ".", Optional ByVal dateSeparator As String = "/") As String
        Dim MyYear, MyMonth, MyDay, MyHour, MyMinute, mySecond As String
        MyYear = DateToConvert.Year
        MyMonth = DateToConvert.Month
        MyDay = DateToConvert.Day
        MyHour = DateToConvert.Hour
        MyMinute = DateToConvert.Minute
        MySecond = DateToConvert.Second

        If MyMonth < 10 Then MyMonth = "0" & MyMonth
        If MyDay < 10 Then MyDay = "0" & MyDay
        If MyHour < 10 Then MyHour = "0" & MyHour
        If MyMinute < 10 Then MyMinute = "0" & MyMinute
        If MySecond < 10 Then MySecond = "0" & MySecond

        Select Case Options
            Case DateFormat.TextDateOptions.YYYYMMDD
                Return MyYear & DateSeparator & MyMonth & DateSeparator & MyDay
            Case DateFormat.TextDateOptions.DDMMYYYY
                Return MyDay & DateSeparator & MyMonth & DateSeparator & MyYear
            Case DateFormat.TextDateOptions.MMDDYYYY
                Return MyMonth & DateSeparator & MyDay & DateSeparator & MyYear
            Case DateFormat.TextDateOptions.FullTime
                Return MyHour & ":" & MyMinute & ":" & MySecond
            Case DateFormat.TextDateOptions.ShortTime
                Return MyHour & ":" & MyMinute
        End Select

        'Return default
        Return MyYear & DateSeparator & MyMonth & DateSeparator & MyDay
    End Function
End Class

Public Class DateFormat
    Public Enum TextDateOptions
        YYYYMMDD = 0
        DDMMYYYY = 1
        MMDDYYYY = 2
        FullDayMonthNames = 3
        ShortDayMonthNames = 4
        ShortDayNameDDMMYY = 5
        ShortDayNameMMDDYY = 6
        ShortDayNameYYMMDD = 7
        FullTime = 8
        ShortTime = 9
        YYYYMMDDShortDayName = 10
        WeekDayName = 11
    End Enum
End Class
