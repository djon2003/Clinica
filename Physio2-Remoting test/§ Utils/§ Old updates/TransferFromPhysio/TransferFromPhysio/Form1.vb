

Public Class Form1

    Private noVisite As Integer = 0
    Private noFacture As Integer = 0
    Private nbErreurs As Integer = 0
    Private sbSQL As New System.Text.StringBuilder()
    Private initTemplate As String = String.Empty
    Private stepTemplate As String = String.Empty


    Private Sub lockItems(ByVal trueFalse As Boolean)
        Button1.Enabled = Not trueFalse
        Client.Enabled = Not trueFalse
        scripting.Enabled = Not trueFalse
        mapfromfile.Enabled = Not trueFalse
        Clinique.Enabled = Not trueFalse
        RVs.Enabled = Not trueFalse
        medecins.Enabled = Not trueFalse
        Users.Enabled = Not trueFalse
    End Sub

    Private Function initTemplates() As Boolean
        Dim found As Boolean = False
        Dim dsModels As DataSet = DBLinker.getInstance(True).readDBForGrid("Modeles", "*", "where Nom like 'Rapport CSST%'")
        For Each curRow As DataRow In dsModels.Tables(0).Rows
            If curRow("Nom") = "Rapport CSST d'étape" Then
                stepTemplate = curRow("Modele").ToString().Replace("\n", vbCrLf)
            ElseIf curRow("Nom") = "Rapport CSST initial" Then
                initTemplate = curRow("Modele").ToString().Replace("\n", vbCrLf)
                found = True
            End If
        Next

        If Not found Then
            Dim scriptFile As String = My.Application.Info.DirectoryPath
            scriptFile &= "\ScriptToRunAfterClean.sql"

            If IO.File.Exists(scriptFile) = False Then
                'Version for DEV
                scriptFile = My.Application.Info.DirectoryPath
                scriptFile = scriptFile.Substring(0, scriptFile.LastIndexOf("\")) 'Move up one dir
                scriptFile = scriptFile.Substring(0, scriptFile.LastIndexOf("\")) 'Move up one dir
                If IO.File.Exists(scriptFile & "\DBLinker.vb") = False Then scriptFile = scriptFile.Substring(0, scriptFile.LastIndexOf("\")) 'Move up one dir
                scriptFile &= "\ScriptToRunAfterClean.sql"
            End If

            If IO.File.Exists(scriptFile) Then
                Dim script As String = IO.File.ReadAllText(scriptFile)
                DBLinker.executeSQLScript(script)

                Return initTemplates()
            Else
                MessageBox.Show("Les données de base pour effectuer le transfert n'ont pas été insérées et le fichier nécessaire n'existe pas : " & vbCrLf & scriptFile, "Impossible de transférer les données", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        Return found
    End Function

    Private Function initDB() As Boolean
        Dim dbName As String = InputBox("Entrer le nom de la base de données de Clinica", "DB name", "ClinicaST")

        Dim startingTime As Date = Now
        Dim n As Integer = 0
        DBLinker.getInstance(False).initConnection(OpenFileDialog1.FileName)
        Dim serveur As String = InputBox("Adresse serveur", "serveur", ".\SQLEXPRESS")
        Dim port As String = InputBox("port du serveur", "Serveur", "")
        If port = "" Then
            DBLinker.getInstance(True).initConnection(serveur, , dbName)
        Else
            DBLinker.getInstance(True).initConnection(serveur, CInt(port), , dbName)
        End If
        DBLinker.getInstance(False).dbConnected = True
        DBLinker.getInstance(True).dbConnected = True

        Return True
    End Function

    Private Sub transferDoctors()
        If medecins.Checked = True Then
            Dim dsMedecin As DataSet = DBLinker.getInstance(False).readDBForGrid("MEDECIN", "*")

            DataGridView1.DataSource = dsMedecin
            DataGridView1.DataMember = "Table"
            Dim n As Integer = 0
            nbErreurs = 0
            With dsMedecin.Tables(0).Rows
                For i As Integer = .Count - 1 To 0 Step -1
                    Me.Text = "Médecin (Restant/ajoutés) " & i & ":" & n
                    Application.DoEvents()
                    Dim clientExists As DataSet = DBLinker.getInstance(True).readDBForGrid("KeyPeople", "NoKP", "WHERE NoRef='" & .Item(i)("NO").ToString.Replace("'", "''") & "'")
                    If .Item(i)("NO").ToString().Trim().ToUpper <> "NIL" And (clientExists Is Nothing OrElse clientExists.Tables(0).Rows.Count = 0) Then
                        Try
                            Dim remarques As String = .Item(i)("AD1").ToString().Trim()
                            Dim adresse As String = .Item(i)("AD2").ToString().Trim()
                            Dim ville As String = .Item(i)("AD3").ToString().Trim()
                            Dim codePostal As String = .Item(i)("AD4").ToString().Trim()
                            If codePostal = "" Then
                                codePostal = ville
                                ville = adresse
                                remarques = ""
                            End If
                            Dim categorie As String = "Médecin"
                            If .Item(i)("SPEC").ToString.Trim <> "" Then
                                categorie &= ":" & .Item(i)("SPEC").ToString.Trim
                            ElseIf .Item(i)("TITRE").ToString.Trim.ToUpper.IndexOf("MEDECIN") < 0 Then
                                categorie &= ":" & .Item(i)("TITRE").ToString.Trim
                            End If

                            Dim nomDr As String = firstLetterCapital(.Item(i)("NOM").ToString.Trim.ToLower, True)
                            Dim nomDrs() As String = nomDr.Split(New Char() {" "})
                            If nomDrs.Length = 2 Then nomDr = nomDrs(1) & "," & nomDrs(0)

                            DBLinker.getInstance(True).writeDB("KeyPeople", "DateHeureCreation, NoUser, Nom,Adresse,NoVille,CodePostal,AutreInfos,Telephones,NoCategorie,NoRef", _
                            "'2014/02/16',0,'" & nomDr.Replace("'", "''") & "','" & firstLetterCapital(adresse.ToLower, True).Replace("'", "''") & "'," & DBLinker.getInstance(True).addItemToADBList("Villes", "NomVille", firstLetterCapital(ville.ToString.ToLower, True), "NoVille") & ",'" & codePostal.Replace(" ", "").Replace("-", "").ToUpper & "','" & firstLetterCapital(remarques.Replace("'", "''").ToLower) & "','" & _
                            "Tél 1:" & .Item(i)("TEL1").ToString.Trim.Replace("'", "''") & IIf(.Item(i)("TEL2").ToString.Trim <> "", "§Tél 2:" & .Item(i)("TEL2").ToString.Trim, "") & "'," & DBLinker.getInstance(True).addItemToADBList("KPCategorie", "Categorie", firstLetterCapital(categorie.ToLower, True), "NoCategorie") & ",'" & .Item(i)("NO").ToString.Trim().Replace("-", "").Replace("'", "''") & "'")
                            n += 1
                        Catch ex As Exception
                            erreurs.Text &= "Médecin de la table MEDECIN - entrée no:" & (i).ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                            erreurs.SelectionStart = erreurs.Text.Length - 1
                            nbErreurs += 1
                        End Try
                    End If
                Next i
            End With

            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de médecins ajoutés : " & n & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1
        End If
    End Sub

    Private Sub transferClinic()
        If Clinique.Checked Then
            Dim dsPara As DataSet = DBLinker.getInstance(False).readDBForGrid("PARA", "*")

            DataGridView1.DataSource = dsPara
            DataGridView1.DataMember = "Table"

            With dsPara.Tables(0).Rows(0)
                Dim exists As DataSet = DBLinker.getInstance(True).readDBForGrid("InfoClinique", "Count(*)")
                Try
                    If exists.Tables(0).Rows(0)(0) > 0 Then
                        DBLinker.getInstance(True).updateDB("InfoClinique", "Nom=" & "'" & firstLetterCapital(.Item("NOM").ToString().ToLower, True).Trim.Replace("'", "''") & "',Adresse='" & firstLetterCapital(.Item("AD1").ToString().Trim.ToLower, True).Replace("'", "''") & "',NoVille=" & _
                        DBLinker.getInstance(True).addItemToADBList("Villes", "NomVille", firstLetterCapital(.Item("AD2").ToString.Trim, True), "NoVille") & ",CodePostal='" & .Item("AD3").ToString().Trim().ToUpper.Replace(" ", "").Replace("'", "''") & "',Telephone='" & .Item("TEL").ToString.Trim.Replace("(", "").Replace(")", "").Replace(" ", "-") & "'")
                    Else
                        DBLinker.getInstance(True).writeDB("InfoClinique", "Nom,Adresse,NoVille,CodePostal,Telephone", "'" & firstLetterCapital(.Item("NOM").ToString().Trim.ToLower, True).Replace("'", "''") & "','" & firstLetterCapital(.Item("AD1").ToString().Trim.ToLower, True).Replace("'", "''") & "'," & _
                        DBLinker.getInstance(True).addItemToADBList("Villes", "NomVille", firstLetterCapital(.Item("AD2").ToString.Trim.ToLower, True), "NoVille") & ",'" & .Item("AD3").ToString().Trim().ToUpper.Replace(" ", "").Replace("'", "''") & "','" & .Item("TEL").ToString.Trim.Replace("(", "").Replace(")", "").Replace(" ", "-") & "'")
                    End If
                Catch ex As Exception
                    erreurs.Text &= "Clinique de la table PARA - entrée no:1 - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                    erreurs.SelectionStart = erreurs.Text.Length - 1
                End Try
            End With
        End If
    End Sub

    Dim htTRP As New Hashtable
    Dim htNIP As New Hashtable

    Private Sub transferUsersAndModels()
        htTRP = New Hashtable
        htNIP = New Hashtable

        Dim htModeleTypes As New Hashtable
        htModeleTypes.Add("A", "3")
        htModeleTypes.Add("B", "3")
        htModeleTypes.Add("C", "2")
        htModeleTypes.Add("D", "3")
        htModeleTypes.Add("E", "4")
        htModeleTypes.Add("F", "8")
        htModeleTypes.Add("G", "7")
        htModeleTypes.Add("H", "1")


        Dim dsParaUtil As DataSet = DBLinker.getInstance(False).readDBForGrid("PARAUTIL", "*")
        Dim dsTTBase As DataSet = DBLinker.getInstance(False).readDBForGrid("TTBASE", "*")

        'Utilisateurs
        If Users.Checked Then
            DataGridView1.DataSource = dsParaUtil
            DataGridView1.DataMember = "Table"
        End If
        Dim n As Integer = 0
        nbErreurs = 0
        With dsParaUtil.Tables(0).Rows
            For i As Integer = .Count - 1 To 0 Step -1
                Me.Text = "Utilisateurs (Restant/ajoutés) " & i & ":" & n
                Application.DoEvents()
                If .Item(i)("NOM").ToString().Trim() = "" Then Continue For
                Dim noms(), nom As String
                nom = .Item(i).Item("NOM").ToString.Trim.ToLower
                If nom.IndexOf(",") >= 0 Then
                    noms = nom.Split(New Char() {","})
                Else
                    noms = nom.Split(New Char() {" "})
                    nom = noms(1)
                    noms(1) = noms(0)
                    noms(0) = nom
                End If

                Dim exists As DataSet = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "NoUser", "WHERE Nom='" & noms(0) & "' AND Prenom='" & noms(1) & "'")
                If Users.Checked Then
                    If (exists Is Nothing OrElse exists.Tables(0).Rows.Count = 0) Then
                        Try
                            Dim titre As String = "Physiothérapeute"
                            Dim service As String = String.Empty
                            Dim isTherapist As Boolean = If(.Item(i)("FONCT") = "SECRETAIRE", False, True)
                            If Not isTherapist Then titre = "Secrétaire"
                            If isTherapist AndAlso Not .Item(i)("FONCT").ToString().StartsWith("P") Then titre = "Thérapeute en réadaptation physique"

                            Dim dateDebut As String = .Item(i)("DATEARRIVE").ToString.Trim
                            If dateDebut = String.Empty Then dateDebut = "2014/02/16"

                            DBLinker.getInstance(True).writeDB("Utilisateurs", "Services, NotConfirmRVOnPasteOfDTRP, Adresse, CodePostal, Telephones, Courriel, URL, DroitAcces,IsTherapist,Cle,MDP,Nom,Prenom,DateDebut,DateFin,NoTitre,NoType,NoPermis,NoTypeEmploye", _
                            "'" & service & "',0,'','','','',''," & _
                            "'" & If(isTherapist, "3", "") & "211111111111111111111111111111111111111111111111111111111111111110111111111111111111111111111111111111111111111'," & If(isTherapist, "1", "0") & ",'2008-04-0818:51:28.5312500','00','" & firstLetterCapital(noms(0).ToLower, True).Replace("'", "''") & "','" & firstLetterCapital(noms(1).ToLower, True).Replace("'", "''") & "','" & dateDebut & "'," & IIf(.Item(i)("DATETERMIN").ToString.Trim <> "", "'" & .Item(i)("DATETERMIN").ToString.Trim & "'", "null") & "," & DBLinker.getInstance(True).addItemToADBList("Titres", "Titre", titre, "NoTitre") & ",null,'" & .Item(i)("NUMPERMIS").ToString.Trim.Replace("'", "''") & "',1")
                            n += 1
                        Catch ex As Exception
                            erreurs.Text &= "Utilisateur de la table PARAUTIL - entrée no:" & (i).ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                            erreurs.SelectionStart = erreurs.Text.Length - 1
                            nbErreurs += 1
                        End Try
                    End If
                    exists = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "NoUser", "WHERE Nom='" & noms(0) & "' AND Prenom='" & noms(1) & "'")
                End If
                If exists.Tables(0).Rows.Count > 0 Then
                    If htNIP.ContainsKey(.Item(i)("NIP").ToString.Trim) = False Then htNIP.Add(.Item(i)("NIP").ToString.Trim, exists.Tables(0).Rows(0)(0))
                    If htTRP.ContainsKey(.Item(i)("EQUIPE").ToString.Trim & "-" & .Item(i)("THERAPEUTE").ToString.Trim) = False Then htTRP.Add(.Item(i)("EQUIPE").ToString.Trim & "-" & .Item(i)("THERAPEUTE").ToString.Trim, exists.Tables(0).Rows(0)(0))
                End If
            Next i
        End With

        If Users.Checked Then
            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre d'utilisateurs ajoutés : " & n & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1

            DBLinker.executeSQLScript("INSERT INTO SettingsUser (NoUser) SELECT NoUser FROM Utilisateurs WHERE Utilisateurs.NoUser NOT IN (SELECT NoUser FROM SettingsUser)")


            'Modèles
            DataGridView1.DataSource = dsTTBase
            DataGridView1.DataMember = "Table"
            n = 0
            nbErreurs = 0
            With dsTTBase.Tables(0).Rows
                For i As Integer = .Count - 1 To 0 Step -1
                    Me.Text = "Modèles (Restant/ajoutés) " & i & ":" & n
                    Application.DoEvents()

                    Try
                        Dim noUser As Integer = 0
                        If .Item(i)("NIPTHE").ToString.Trim <> "" Then noUser = getNoNIP(htNIP, .Item(i)("NIPTHE").ToString.Trim, True)

                        Dim exists As DataSet = DBLinker.getInstance(True).readDBForGrid("Modeles", "NoModele", "WHERE Nom='" & .Item(i)("DESCRIPT").ToString.Replace("'", "''") & "' AND NoUser=" & noUser & " AND NoCategorie=" & htModeleTypes(.Item(i)("SECTION").ToString))
                        If exists IsNot Nothing AndAlso exists.Tables(0).Rows.Count = 0 And .Item(i)("SECTION").ToString <> "H" Then
                            DBLinker.getInstance(True).writeDB("Modeles", "NoCategorie,NoUser,Nom,Modele", htModeleTypes(.Item(i)("SECTION").ToString) & ",null,'" & firstLetterCapital(.Item(i)("DESCRIPT").ToString.ToLower, True).Replace("'", "''") & "','" & firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(i)("NOTE").ToString.Replace("'", "''"), "(\r\n|\r|\n)", "<BR>").ToLower, False) & "'")
                            n += 1
                        End If
                    Catch ex As Exception
                        erreurs.Text &= "Modèle de la table TTBASE - entrée no:" & (i).ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                        erreurs.SelectionStart = erreurs.Text.Length - 1
                        nbErreurs += 1
                    End Try
                Next i
            End With

            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de modèles ajoutés : " & n & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf

        End If
    End Sub

    Dim htDossiers As New Hashtable(10000, 0.5)
    Dim htNAM As New Hashtable(10000, 0.5)
    Dim htCodes As New Hashtable


    Private Sub transferClientsAndFolders()
        htDossiers = New Hashtable(10000, 0.5)
        htNAM = New Hashtable(10000, 0.5)
        htCodes = New Hashtable

        Dim dsDosClient As DataSet
        erreurs.Text &= vbCrLf & "Starting Clients generation : " & Now.ToString & vbCrLf & vbCrLf
        erreurs.SelectionStart = erreurs.Text.Length - 1

        Dim n As Integer = 0
        Dim n2 As Integer = 0
        htCodes.Add("CSST", "1")
        htCodes.Add("EXT", "2")
        htCodes.Add("SAAQ", "3")
        htCodes.Add("MASS", "4")
        htCodes.Add("PILA", "5")

        If mapfromfile.Checked And IO.File.Exists("C:\temp\transfer.dossiers") Then
            Dim fileNam() As String = IO.File.ReadAllText("C:\temp\transfer.clients").Split(New Char() {vbCrLf})
            Dim fileDossiers() As String = IO.File.ReadAllText("C:\temp\transfer.dossiers").Split(New Char() {vbCrLf})
            For i As Integer = 0 To fileNam.GetUpperBound(0) - 1 Step 2
                htNAM.Add(fileNam(i).Trim, fileNam(i + 1).Trim)
            Next i
            For i As Integer = 0 To fileDossiers.GetUpperBound(0) - 1 Step 2
                htDossiers.Add(fileDossiers(i).Trim, fileDossiers(i + 1).Trim)
            Next i
        Else
            dsDosClient = DBLinker.getInstance(False).readDBForGrid("DOSClien", "*")
            DataGridView1.DataSource = dsDosClient
            DataGridView1.DataMember = "Table"

            Dim dsInfoClients As DataSet = DBLinker.getInstance(True).readDBForGrid("InfoClients", "*")
            Dim dsKeyPeople As DataSet = DBLinker.getInstance(True).readDBForGrid("KeyPeople", "*")
            Dim siteLesion As Integer = DBLinker.getInstance(True).addItemToADBList("siteLesion", "siteLesion", "Inconnu", "NositeLesion")
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
                            noms = .Item(i)("NOM").ToString().Trim.Split(New Char() {" "}, 2)
                        Else
                            noms = .Item(i)("NOM").ToString.Trim.Split(New Char() {","})
                        End If

                        Dim nam As String = .Item(i)("NAM").ToString.Replace("-", "").ToUpper.Trim

                        'Dim ClientExists As DataSet = DBLinker.GetInstance(True).ReadDBForGrid("InfoClients", "NoClient", "WHERE NAM='" & .Item(i)("NAM").ToString.Replace("-", "") & "' OR (Nom='" & Noms(0).Replace("'", "''") & "' AND Prenom='" & Noms(1).Replace("'", "''") & "' AND REPLACE(Telephones,'-','') LIKE 'Résidence:%" & .Item(i)("TELERES").ToString.Replace(":", "").Replace("'", "''").Replace("-", "").Trim & "%" & "')")
                        Dim isClient() As DataRow = dsInfoClients.Tables(0).Select("NAM='" & nam & "' OR (Nom='" & noms(0).Replace("'", "''") & "' AND Prenom='" & noms(1).Replace("'", "''") & "' AND (Telephones LIKE '%" & .Item(i)("TELERES").ToString.Replace(":", "").Replace("'", "''").Trim & "%" & "'" & IIf(.Item(i)("DATE_NAI").ToString = "", "", " OR DateNaissance='" & .Item(i)("DATE_NAI").ToString & "'") & "))")

                        If Client.Checked = True AndAlso (isClient Is Nothing OrElse isClient.Length = 0) Then
                            nbClients += 1
                            Try
                                Dim ville As String = .Item(i)("AD2").ToString.ToLower.Trim.Replace("<", "'").Replace(", qc", "").Replace(",qc", "").Replace("(qc)", "").Replace(";", "'").Replace(" qc", "").Replace("''", "'").Replace(".qc", "").Replace(".", "").Trim()
                                ville = firstLetterCapital(ville, True)
                                Dim noVille As String = If(ville = String.Empty, "null", DBLinker.getInstance(True).addItemToADBList("Villes", "NomVille", ville, "noVille"))

                                Dim autreNom As String = .Item(i)("AUTRENOM").ToString().ToLower.Trim
                                Dim employeur As String = .Item(i)("ENTREPRISE").ToString().Trim.ToLower
                                Dim firstTelTitle As String = "Résidence"
                                Dim employeurContainsNumber As Boolean = onlyNumeric(employeur) <> String.Empty
                                Dim employeurContainsCell As Boolean = employeur.IndexOf("cell") <> -1
                                If employeurContainsCell AndAlso Not employeurContainsNumber Then firstTelTitle = "Cellulaire"

                                Dim autreNomContainsNumber As Boolean = onlyNumeric(employeur) <> String.Empty
                                Dim autreNomContainsCell As Boolean = employeur.IndexOf("cell") <> -1

                                Dim telephones As String = .Item(i)("TELERES").ToString.Replace(":", "").Replace("'", "''").Trim
                                If telephones <> String.Empty Then telephones = firstTelTitle & ":" & telephones
                                If employeurContainsCell AndAlso employeurContainsNumber Then
                                    telephones &= If(telephones <> String.Empty, "§", "") & "Cellulaire:" & employeur.Substring(getFirstCellularIndex(employeur))
                                End If
                                If employeurContainsCell OrElse employeurContainsNumber Then employeur = String.Empty

                                If autreNomContainsCell AndAlso autreNomContainsNumber Then
                                    telephones &= If(telephones <> String.Empty, "§", "") & "Cellulaire:" & autreNom.Substring(getFirstCellularIndex(autreNom))
                                End If
                                If autreNomContainsCell Then autreNom = String.Empty

                                telephones &= If(.Item(i)("TELEBUR").ToString.Trim.ToUpper <> "NIL" And .Item(i)("TELEBUR").ToString <> "", If(telephones <> String.Empty, "§", "") & "Bureau:" & .Item(i)("TELEBUR").ToString().Replace(":", "").Replace("'", "''").Trim, "")

                                Dim noMetier As String = .Item(i)("TYPETRAVAI").ToString().Trim.ToLower.Replace("<", "'")
                                If onlyNumeric(noMetier) <> String.Empty Then noMetier = String.Empty
                                If noMetier.IndexOf("clsc") <> -1 OrElse noMetier.Replace(" ", "").IndexOf("postecanada") OrElse noMetier.Replace(" ", "").IndexOf("portmtl") Then
                                    employeur = noMetier
                                    noMetier = String.Empty
                                End If
                                noMetier = If(noMetier = String.Empty, "null", DBLinker.getInstance(True).addItemToADBList("Metiers", "Metier", firstLetterCapital(noMetier), "noMetier"))

                                Dim noEmployeur As String = IIf(employeur <> String.Empty, "null", DBLinker.getInstance(True).addItemToADBList("Employeurs", "Employeur", firstLetterCapital(employeur.ToLower), "noEmployeur"))


                                Dim dateNaissance As String = "'" & .Item(i)("DATE_NAI").ToString().Trim & "'"
                                If dateNaissance = String.Empty Then dateNaissance = "null"


                                DBLinker.getInstance(True).writeDB("InfoClients", "DateHeureCreation, NoUser, Publipostage,Nom,Prenom,Adresse,NoVille,CodePostal,DateNaissance,NoEmployeur,NAM,SexeHomme,Telephones,NoMetier,AutreNom", _
                                "'" & .Item(i)("DATECREAT").ToString().Trim & "',0,0,'" & firstLetterCapital(noms(0).ToLower, True).Replace("'", "''") & "','" & firstLetterCapital(noms(1).ToLower, True).Replace("'", "''") & "','" & firstLetterCapital(.Item(i)("AD1").ToString.Trim.ToLower, True).Replace("'", "''") & "'," & noVille & ",'" & .Item(i)("AD3").ToString.Replace(" ", "").Replace("-", "").ToUpper & "'," & dateNaissance & "," & _
                                noEmployeur & ",'" & nam & "'," & IIf(.Item(i)("SEXE").ToString().Trim = "M", "1", "0").ToString() & ",'" & telephones & "'," & _
                                noMetier & ",'" & firstLetterCapital(autreNom, True).Replace("'", "''") & "'")

                                If lastNbErreurs <> nbErreurs Then
                                    lastNbErreurs = nbErreurs
                                    nbClients = DBLinker.getInstance(True).readDBForGrid("InfoClients", "MAX(NoClient)").Tables(0).Rows(0)(0)
                                End If

                                Dim t As DataRow = dsInfoClients.Tables(0).NewRow
                                t.Item("Publipostage") = 0
                                t.Item("Nom") = firstLetterCapital(noms(0).ToLower, True)
                                t.Item("Prenom") = firstLetterCapital(noms(1).ToLower, True)
                                t.Item("Adresse") = firstLetterCapital(.Item(i)("AD1").ToString.Trim.ToLower, True)
                                If noVille <> "null" Then t.Item("NoVille") = noVille
                                t.Item("CodePostal") = .Item(i)("AD3").ToString.Replace(" ", "").Replace("-", "").ToUpper
                                If .Item(i)("DATE_NAI").ToString().Trim <> "" Then t.Item("DateNaissance") = .Item(i)("DATE_NAI").ToString().Trim
                                If noEmployeur <> "null" Then t.Item("NoEmployeur") = noEmployeur
                                t.Item("NAM") = .Item(i)("NAM").ToString().ToUpper.Replace("-", "").Trim
                                t.Item("SexeHomme") = IIf(.Item(i)("SEXE").ToString().Trim = "M", True, False)
                                t.Item("Telephones") = "Résidence:" & .Item(i)("TELERES").ToString.Replace(":", "").Trim & IIf(.Item(i)("TELEBUR").ToString.Trim.ToUpper <> "NIL" And .Item(i)("TELEBUR").ToString <> "", "§Bureau:" & .Item(i)("TELEBUR").ToString().Replace(":", "").Trim, "")
                                t.Item("AutreNom") = firstLetterCapital(.Item(i)("AUTRENOM").ToString().ToLower.Trim, True)
                                t.Item("NoClient") = nbClients
                                If noMetier <> "null" Then t.Item("NoMetier") = noMetier
                                dsInfoClients.Tables(0).Rows.Add(t)
                                isClient = dsInfoClients.Tables(0).Select("", "NoClient DESC")
                                DBLinker.getInstance(True).writeDB("ClientsAntecedents", "NoClient,Antecedents", isClient(0)("NoClient") & ",''")
                                n += 1
                            Catch ex As Exception
                                erreurs.Text &= "Client de la table DOSClien - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                                erreurs.SelectionStart = erreurs.Text.Length - 1
                                nbErreurs += 1
                                Continue For
                            End Try
                        End If

                        If htNAM.ContainsKey(nam) = False AndAlso isClient.Length > 0 Then htNAM.Add(nam, isClient(0)("NoClient"))
                    End If
                Next i
                For i As Integer = 0 To .Count - 1
                    If .Item(i)("NOM").ToString().Trim <> "" Then
                        Me.Text = "Dossiers (Restant/ajoutés) " & (.Count - 1 - i) & ":" & n2
                        Application.DoEvents()

                        Dim dsNotes As DataSet = Nothing
                        If Client.Checked = True AndAlso .Item(i)("FILENOTE").ToString <> "" Then dsNotes = DBLinker.getInstance(False).readDBForGrid(.Item(i)("FILENOTE").ToString, "*", "WHERE FILECLE='" & .Item(i)("FILECLE").ToString & "'")
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
                                            antecedents = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "ANA"
                                            analyse = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "BUT"
                                            but = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "PLA"
                                            plan = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "SUB"
                                            eval = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "EVO"
                                            notes = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "RAE"
                                            rapEtape = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "RAF"
                                            rapFinal = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                        Case "RAM"
                                            rapMed = firstLetterCapital(System.Text.RegularExpressions.Regex.Replace(.Item(j)("NOTEMEMO").ToString, "(\r\n|\r|\n)", "<BR>").ToLower, False)
                                    End Select
                                Next j
                            End With
                        End If

                        'Dossier
                        If Client.Checked = True Then
                            Dim nam As String = .Item(i)("NAM").ToString.Replace("-", "").ToUpper.Trim
                            If .Item(i)("NOM").ToString().Trim <> "" AndAlso htNAM.ContainsKey(nam) Then
                                Try
                                    'InfoFolder
                                    Dim noClient As Integer = htNAM(nam)
                                    Dim noCodification As Integer = IIf(htCodes.ContainsKey(.Item(i)("CODE").ToString.Trim), htCodes(.Item(i)("CODE").ToString.Trim), 1)
                                    Dim service As String = "Physiothérapie"
                                    Select Case .Item(i)("CODE").ToString.Trim
                                        Case "MASO"
                                            service = "Massothérapie"
                                    End Select

                                    Dim noTRP As Integer = getNoNIP(htNIP, .Item(i)("NIPTHE").ToString.Trim, True)
                                    Dim duree As Integer = 0
                                    If .Item(i)("DUREEPREVU").ToString <> "" AndAlso Integer.TryParse(.Item(i)("DUREEPREVU").ToString, 0) Then duree = Math.Ceiling(.Item(i)("DUREEPREVU") / 5)
                                    Dim frequence As Integer = 1
                                    Dim strFreq As String = .Item(i)("FREQUENCE").ToString
                                    If strFreq <> "" AndAlso Integer.TryParse(strFreq.Substring(0, 1), 0) Then frequence = strFreq.Substring(0, 1)
                                    If frequence < 1 Then frequence = 1
                                    Dim noMedecin As Integer = 0
                                    Dim strNoMedecin As String = .Item(i)("MEDECIN").ToString.Trim.Replace("-", "")
                                    Dim myMedecin() As DataRow = dsKeyPeople.Tables(0).Select("NoRef='" & strNoMedecin.Replace("'", "''") & "'")
                                    If myMedecin IsNot Nothing AndAlso myMedecin.Length = 1 Then noMedecin = myMedecin(0)("NoKP")
                                    nbDossiers += 1

                                    Dim folderOpeningDate As String = IIf(.Item(i)("DEBUTRAIT") Is DBNull.Value OrElse .Item(i)("DEBUTRAIT").ToString = "", .Item(i)("DATECREAT").ToString, .Item(i)("DEBUTRAIT").ToString)
                                    DBLinker.getInstance(True).writeDB("InfoFolders", "ExternalStatus, NoRef, Remarques, NbVisiteHavingCAR, Flagged, NoCodeUser, NoCodeDate, DateRef, DateReceptionRef, NoSiteLesion,Frequence,StatutOuvert, NoClient,NoCodeUnique,DateRechute,DateAccident,Diagnostic,Duree,NoKP,NoTRPTraitant,Service", _
                                    If(noCodification = 1, "2", "1") & ",'" & .Item(i)("NOREFDOS").ToString.Trim() & "','" & .Item(i)("REMARQUES").ToString().Trim().Replace("'", "''") & "',0,0," & noTRP & ",'" & folderOpeningDate & "'," & IIf(Date.TryParse(.Item(i)("DATEREF").ToString, Date.Today) = False, "null", "'" & .Item(i)("DATEREF").ToString & "'") & ",null," & siteLesion & "," & frequence & "," & IIf(.Item(i)("FINTRAIT").ToString = "", "1", "0") & "," & noClient & "," & noCodification & "," & IIf(Date.TryParse(.Item(i)("DATERECHUT").ToString, Date.Today) = False, "null", "'" & .Item(i)("DATERECHUT").ToString & "'") & "," & IIf(Date.TryParse(.Item(i)("DATE_ACC").ToString, Date.Today) = False, "null", "'" & .Item(i)("DATE_ACC").ToString & "'") & ",'" & IIf(.Item(i)("DIAGNOSTIC").ToString.Trim.ToUpper = "NIL", "", firstLetterCapital(.Item(i)("DIAGNOSTIC").ToString.Trim.ToLower, True).Replace("'", "''")) & "'," & duree & "," & If(noMedecin = 0, "null", noMedecin.ToString()) & "," & noTRP & ",'" & service & "'")
                                    Dim noFolder As Integer = nbDossiers
                                    DBLinker.getInstance(True).writeDB("StatFolders", "DateHeureCreation,NoAction,NoFolder,NoClient,NoUser", "'" & folderOpeningDate & "',13," & noFolder & "," & noClient & "," & getNoNIP(htNIP, .Item(i)("NIPCR").ToString.Trim, False))
                                    If .Item(i)("FINTRAIT").ToString <> "" Then DBLinker.getInstance(True).writeDB("StatFolders", "DateHeureCreation,NoAction,NoFolder,NoClient,NoUser", "'" & .Item(i)("FINTRAIT").ToString & "',15," & noFolder & "," & noClient & "," & getNoNIP(htNIP, .Item(i)("NIPCR").ToString.Trim, False))

                                    'FolderTextes
                                    Dim eabp As String = ""
                                    If antecedents <> "" Then eabp &= "<b>Historique :</b><br>" & antecedents & "<br><br>"
                                    If eval <> "" Then eabp &= "<b>Évaluation :</b><br>" & eval & "<br><br>"
                                    If analyse <> "" Then eabp &= "<b>Analyse :</b><br>" & analyse & "<br><br>"
                                    If but <> "" Then eabp &= "<b>But :</b><br>" & but & "<br><br>"
                                    If plan <> "" Then eabp &= "<b>Plan de traitement :</b><br>" & plan & "<br><br>"


                                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte, TextePos, NoMultiple, IsTexte, ExternalStatus", "1," & noFolder & ",'Historique,Évaluation,Analyse,But,Plan','" & folderOpeningDate & "','" & eabp.Replace("'", "''") & "',-1,1," & If(eabp = String.Empty, "0", "1") & ",1")
                                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte, TextePos, NoMultiple, IsTexte, ExternalStatus", "2," & noFolder & ",'Notes','" & folderOpeningDate & "','" & notes.Replace("'", "''") & "',-1,1," & If(notes = String.Empty, "0", "1") & ",1")
                                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte, TextePos, NoMultiple, IsTexte, ExternalStatus", "3," & noFolder & ",'Rapport au médecin','" & folderOpeningDate & "','',-1,1,0,1")
                                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte, TextePos, NoMultiple, IsTexte, ExternalStatus", "7," & noFolder & ",'Archive des rapports au médecin','" & folderOpeningDate & "','" & rapMed.Replace("'", "''") & "',-1,1," & If(rapMed = String.Empty, "0", "1") & ",1")

                                    Dim nbDossier As Integer = .Item(i)("RECLAMATIO") ' .Item(i)("NB_DOSSIER")
                                    'While htDossiers.ContainsKey(.Item(i)("NAM").ToString.Trim & IIf(NbDossier.ToString.Length = 1, "0" & NbDossier.ToString, NbDossier.ToString)) = True
                                    '    NbDossier -= 1
                                    'End While
                                    Dim whiteSpaces As String = ""
                                    For k As Integer = 1 To (14 - .Item(i)("NAM").ToString.Length)
                                        whiteSpaces &= " "
                                    Next k
                                    htDossiers.Add(.Item(i)("NAM").ToString & whiteSpaces & IIf(nbDossier.ToString.Length = 1, "0" & nbDossier.ToString, nbDossier.ToString), noFolder)
                                    n2 += 1
                                Catch ex As Exception
                                    erreurs.Text &= "Dossier de la table DOSClien - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                                    erreurs.SelectionStart = erreurs.Text.Length - 1
                                    nbErreurs += 1
                                End Try
                            End If
                        Else
                            Dim nbDossier As Integer
                            Integer.TryParse(.Item(i)("RECLAMATIO").ToString, nbDossier) '.Item(i)("NB_DOSSIER")
                            'While htDossiers.ContainsKey(.Item(i)("NAM").ToString.Trim & IIf(NbDossier.ToString.Length = 1, "0" & NbDossier.ToString, NbDossier.ToString)) = True
                            '    NbDossier -= 1
                            'End While
                            Dim whiteSpaces As String = ""
                            For k As Integer = 1 To (14 - .Item(i)("NAM").ToString.Length)
                                whiteSpaces &= " "
                            Next k
                            htDossiers.Add(.Item(i)("NAM").ToString & whiteSpaces & IIf(nbDossier.ToString.Length = 1, "0" & nbDossier.ToString, nbDossier.ToString), .Count - i)
                        End If
                    End If
                Next i
            End With

            Dim sbWritingMap As New System.Text.StringBuilder()
            For Each myKey As DictionaryEntry In htDossiers
                sbWritingMap.AppendLine(myKey.Key)
                sbWritingMap.AppendLine(myKey.Value)
            Next
            IO.File.WriteAllText("C:\temp\transfer.dossiers", sbWritingMap.ToString)
            sbWritingMap.Remove(0, sbWritingMap.Length)
            For Each myKey As DictionaryEntry In htNAM
                sbWritingMap.AppendLine(myKey.Key)
                sbWritingMap.AppendLine(myKey.Value)
            Next
            IO.File.WriteAllText("C:\temp\transfer.clients", sbWritingMap.ToString)
        End If

        erreurs.Text &= vbCrLf & "Ended Clients generation : " & Now.ToString & vbCrLf
        erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de clients ajoutés : " & n & " Nombre de dossiers ajoutés :" & n2 & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf
        erreurs.SelectionStart = erreurs.Text.Length - 1
    End Sub

    Private Sub transferRVs()
        If RVs.Checked = True Then
            nbErreurs = 0
            Dim n As Integer = 0
            Dim dsInfoFolders As DataSet = DBLinker.getInstance(True).readDBForGrid("InfoFolders INNER JOIN InfoClients ON InfoClients.NoClient=InfoFolders.NoClient", "Nom,Prenom,InfoFolders.*, (SELECT TOP 1 DateHeureCreation FROM StatFolders WHERE StatFolders.NoFolder=InfoFolders.NoFolder AND NoAction=13) AS Debut, (SELECT TOP 1 DateHeureCreation FROM StatFolders WHERE StatFolders.NoFolder=InfoFolders.NoFolder AND NoAction=15) AS Fin")
            Dim objNoVisite As String = DBLinker.getInstance(True).readDBForGrid("InfoVisites", "MAX(NoVisite)").Tables(0).Rows(0)(0).ToString
            Dim objNoFacture As String = DBLinker.getInstance(True).readDBForGrid("StatFactures", "MAX(NoFacture)").Tables(0).Rows(0)(0).ToString
            If objNoVisite <> "" Then noVisite = objNoVisite
            If objNoFacture <> "" Then noFacture = objNoFacture

            erreurs.Text &= vbCrLf & "Starting RVs generation : " & Now.ToString & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1

            Dim dsVisites As DataSet = DBLinker.getInstance(False).readDBForGrid("VISITES", "*")
            Dim dsPaiement As DataSet = DBLinker.getInstance(False).readDBForGrid("PAIEMENT", "*")
            'Try
            transferOneAgenda(n, "AGENDA1", htDossiers, htNAM, htNIP, htTRP, dsVisites, dsPaiement, dsInfoFolders, htCodes)
            transferOneAgenda(n, "AGENDA2", htDossiers, htNAM, htNIP, htTRP, dsVisites, dsPaiement, dsInfoFolders, htCodes)
            If scripting.Checked Then IO.File.WriteAllText("C:\temp\transfer-agendas" & Math.Ceiling((n / 1000)) & ".sql", sbSQL.ToString())
            'Catch ex As Exception
            'erreurs.Text &= vbCrLf & vbCrLf & ex.Message
            'erreurs.SelectionStart = erreurs.Text.Length - 1
            'End Try

            erreurs.Text &= vbCrLf & "Ended RVs generation : " & Now.ToString & vbCrLf
            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de rendez-vous ajoutés : " & n & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf
        End If
    End Sub

    Private Sub transferData()
        Dim n As Integer = 0
        Dim startingTime As Date = Date.Now
        lockItems(True)

        If Not initDB() OrElse Not initTemplates() Then
            lockItems(False)
            Exit Sub
        End If

        transferDoctors()
        transferClinic()
        transferUsersAndModels()

        If Client.Checked = False And RVs.Checked = False Then
            lockItems(False)
            Exit Sub 'Skip Clients, Dossiers & RVs
        End If

        transferClientsAndFolders()
        transferRVs()

        erreurs.Text &= vbCrLf & vbCrLf & "Total time for transfer : " & Now.Subtract(StartingTime).Hours & " hour(s) " & (Now.Subtract(StartingTime).Minutes - Now.Subtract(StartingTime).Hours * 60) & " minutes"

        DBLinker.getInstance(False).dbConnected = False
        DBLinker.getInstance(True).dbConnected = False

        lockItems(False)
    End Sub

    Private Function getNoNIP(ByRef htNIP As Hashtable, ByVal nip As String, ByVal isTRP As Boolean) As Integer
        If htNIP.ContainsKey(nip) Then Return htNIP(nip)

        Dim userTable As DataTable = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "NoUser", "WHERE (Nom='" & nip & "' AND Prenom='" & nip & "') OR NoPermis='" & nip & "'").Tables(0)
        Dim noUser As Integer = 0
        If userTable.Rows.Count = 0 Then
            DBLinker.getInstance(True).writeDB("Utilisateurs", "Services, NotConfirmRVOnPasteOfDTRP, Adresse, CodePostal, Telephones, Courriel, URL, DroitAcces,IsTherapist,Cle,MDP,Nom,Prenom,DateDebut,DateFin,NoTitre,NoType,NoPermis,NoTypeEmploye", _
            "'" & If(isTRP, "Physiothérapie", "") & "',0,'','','','','','" & IIf(isTRP, "3", "") & "21111111111111111111111111111111111111111111111111111111111110111011111111111111111111111','" & isTRP & "','2008-04-0818:51:28.5312500','00','" & nip.Replace("'", "''") & "','" & nip.Replace("'", "''") & "','2008/01/01','2008/01/01'," & DBLinker.getInstance(True).addItemToADBList("Titres", "Titre", "Utilisateur supprimé", "NoTitre") & ",null,'',1")
            noUser = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "MAX(NoUser)").Tables(0).Rows(0)(0)
        Else
            noUser = userTable.Rows(0)(0)
        End If

        htNIP.Add(nip, noUser)

        Return noUser
    End Function

    Private Sub transferOneAgenda(ByRef n As Integer, ByVal agendaName As String, ByRef htDossiers As Hashtable, ByRef htNAM As Hashtable, ByRef htNIP As Hashtable, ByRef htTRP As Hashtable, ByRef dsVisites As DataSet, ByRef dsPaiement As DataSet, ByRef dsInfoFolders As DataSet, ByRef htCodes As Hashtable)
        Dim dsAgenda1 As DataSet = DBLinker.getInstance(False).readDBForGrid(agendaName, "*")
        DataGridView1.DataSource = dsAgenda1
        DataGridView1.DataMember = "Table"

        Dim equipe As String = agendaName.Substring(6)

        Dim nbErreurs As Integer = 0
        Dim curDateHeure As Date
        With dsAgenda1.Tables(0).Rows
            For i As Integer = .Count - 1 To 0 Step -4
                Try
                    curDateHeure = CDate(.Item(i)("DATE").ToString)
                    curDateHeure = curDateHeure.AddHours(23).AddMinutes(45)
                    For j As Integer = 0 To 3
                        transferOneAgendaPart(dsAgenda1, i - j, n, "345", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "330", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "315", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "300", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "245", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "230", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "215", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "200", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "145", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "130", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "115", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "100", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "045", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "030", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "015", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                        transferOneAgendaPart(dsAgenda1, i - j, n, "000", htDossiers, htNAM, htNIP, htTRP, dsVisites, curDateHeure, dsPaiement, equipe, dsInfoFolders, htCodes)
                        curDateHeure = curDateHeure.AddMinutes(-15)
                    Next j
                Catch ex As Exception
                    erreurs.Text &= "Rendez-vous de la table " & agendaName & " - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                    erreurs.SelectionStart = erreurs.Text.Length - 1
                    nbErreurs += 1
                End Try
            Next i
        End With
    End Sub

    Private Sub transferOneAgendaPart(ByRef dsAgenda As DataSet, ByVal i As Integer, ByRef n As Integer, ByVal timeString As String, ByRef htDossiers As Hashtable, ByRef htNAM As Hashtable, ByRef htNIP As Hashtable, ByRef htTRP As Hashtable, ByRef dsVisites As DataSet, ByVal curDateHeure As Date, ByRef dsPaiement As DataSet, ByVal equipe As String, ByRef dsInfoFolders As DataSet, ByRef htCodes As Hashtable)
        With dsAgenda.Tables(0).Rows
            Me.Text = "Agenda " & equipe & " (Restant/ajoutés) " & i & ":" & n
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
            Dim nam1 As String = If(noDossier1 = String.Empty, String.Empty, noDossier1.Substring(0, 14).ToString.Replace("-", "").ToUpper.Trim)
            Dim nam2 As String = If(noDossier2 = String.Empty, String.Empty, noDossier2.Substring(0, 14).ToString.Replace("-", "").ToUpper.Trim)
            Dim nam3 As String = If(noDossier3 = String.Empty, String.Empty, noDossier3.Substring(0, 14).ToString.Replace("-", "").ToUpper.Trim)

            If noDossier1 <> "" AndAlso htNAM.ContainsKey(nam1) Then NoClient1 = htNAM(nam1)
            If noDossier2 <> "" AndAlso htNAM.ContainsKey(nam2) Then NoClient2 = htNAM(nam2)
            If noDossier3 <> "" AndAlso htNAM.ContainsKey(nam3) Then noClient3 = htNAM(nam3)

            Dim drVisite1 As DataRow = Nothing
            Dim drVisite2 As DataRow = Nothing
            Dim drVisite3 As DataRow = Nothing

            Dim service1 As String = "Physiothérapie"
            Dim service2 As String = "Physiothérapie"
            Dim service3 As String = "Physiothérapie"
            If textAgenda1 <> "" And NoClient1 > 0 Then
                drVisite1 = getDrVisite(dsVisites, curDateHeure, nam1, equipe, 1)
                If drVisite1 IsNot Nothing Then
                    Select Case drVisite1("CODE").ToString
                        Case "ERGO"
                            service1 = "Ergothérapie"
                        Case "MASS"
                            service1 = "Massothérapie"
                        Case "OSTE"
                            service1 = "Ostéopathie"
                    End Select
                End If
                If textAgenda1.IndexOf("(ERGO)") > 0 Then service1 = "Ergothérapie"
                NoFolder1 = getNoFolder(getNoTRP(drVisite1, htNIP, htTRP, equipe, 1), dsInfoFolders, NoClient1, curDateHeure, drVisite1, htCodes, service1)
            End If
            If textAgenda2 <> "" And NoClient2 > 0 Then
                drVisite2 = getDrVisite(dsVisites, curDateHeure, nam2, equipe, 2)
                If drVisite2 IsNot Nothing Then
                    Select Case drVisite2("CODE").ToString
                        Case "ERGO"
                            service2 = "Ergothérapie"
                        Case "MASS"
                            service2 = "Massothérapie"
                        Case "OSTE"
                            service2 = "Ostéopathie"
                    End Select
                End If
                If textAgenda2.IndexOf("(ERGO)") > 0 Then service2 = "Ergothérapie"
                NoFolder2 = getNoFolder(getNoTRP(drVisite2, htNIP, htTRP, equipe, 2), dsInfoFolders, NoClient2, curDateHeure, drVisite2, htCodes, service2)
            End If
            If textAgenda3 <> "" And noClient3 > 0 Then
                drVisite3 = getDrVisite(dsVisites, curDateHeure, nam3, equipe, 3)
                If drVisite3 IsNot Nothing Then
                    Select Case drVisite3("CODE").ToString
                        Case "ERGO"
                            service3 = "Ergothérapie"
                        Case "MASS"
                            service3 = "Massothérapie"
                        Case "OSTE"
                            service3 = "Ostéopathie"
                    End Select
                End If
                If textAgenda3.IndexOf("(ERGO)") > 0 Then service3 = "Ergothérapie"
                NoFolder3 = getNoFolder(getNoTRP(drVisite3, htNIP, htTRP, equipe, 3), dsInfoFolders, noClient3, curDateHeure, drVisite3, htCodes, service3)
            End If

            If textAgenda1 <> "" And startsWithNumeric(textAgenda1) = False Then transferOneVisite(n, 1, timeString, dsAgenda, i, drVisite1, equipe, curDateHeure, htTRP, htNIP, dsPaiement, NoClient1, NoFolder1, dsInfoFolders, textAgenda1, dsVisites, htNAM, htCodes, service1)
            If textAgenda2 <> "" And startsWithNumeric(textAgenda2) = False Then transferOneVisite(n, 2, timeString, dsAgenda, i, drVisite2, equipe, curDateHeure, htTRP, htNIP, dsPaiement, NoClient2, NoFolder2, dsInfoFolders, textAgenda2, dsVisites, htNAM, htCodes, service2)
            If textAgenda3 <> "" And startsWithNumeric(textAgenda3) = False Then transferOneVisite(n, 3, timeString, dsAgenda, i, drVisite3, equipe, curDateHeure, htTRP, htNIP, dsPaiement, noClient3, NoFolder3, dsInfoFolders, textAgenda3, dsVisites, htNAM, htCodes, service3)
        End With
    End Sub

    Private Function getDrVisite(ByRef dsVisites As DataSet, ByVal curDateHeure As Date, ByVal nam As String, ByVal equipe As Byte, ByVal position As Byte) As DataRow
        Dim newNam As String = String.Empty
        If nam.Length >= 4 Then
            newNam = nam.Substring(0, 4) & "-"
        End If
        If nam.Length >= 8 Then
            newNam &= nam.Substring(4, 4) & "-"
        Else
            newNam &= "    -"
        End If
        If nam.Length = 12 Then
            newNam &= nam.Substring(8, 4)
        End If
        If newNam.Length <> 14 Then
            newNam &= "    "
        End If

        Dim rows() As DataRow = dsVisites.Tables(0).Select("DATEVISITE='" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & "' AND HEUREVIS='" & curDateHeure.Hour & "," & curDateHeure.Minute & "' AND NAM ='" & newNam & "'")
        Dim drVisite As DataRow = Nothing
        If rows IsNot Nothing AndAlso rows.Length > 0 Then drVisite = rows(0)

        If drVisite Is Nothing Then
            rows = dsVisites.Tables(0).Select("DATEVISITE='" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & "' AND HEUREVIS='" & curDateHeure.Hour & "," & curDateHeure.Minute & "' AND PHYSIO='" & position & "' AND (EQUIPE=' " & equipe & "')")
            If rows IsNot Nothing AndAlso rows.Length > 0 Then drVisite = rows(0)
        End If


        Return drVisite
    End Function

    Private Function getNoTRP(ByRef drVisite As DataRow, ByRef htNIP As Hashtable, ByRef htTRP As Hashtable, ByVal equipe As Byte, ByVal position As Byte) As Integer
        Dim noTRP As Integer = 0
        If drVisite IsNot Nothing Then
            noTRP = getNoNIP(htNIP, drVisite("NIPTHE").ToString.Trim, True)
        Else
            REM this one can't be taken from GetNoNIP because it's not a nip but an Equipe & Position
            noTRP = IIf(htTRP.ContainsKey(equipe & "-" & position), htTRP(equipe & "-" & position), 0)
        End If

        Return noTRP
    End Function

    Private Function getNoFolder(ByVal noTRP As Integer, ByRef dsInfoFolders As DataSet, ByVal noClient1 As Integer, ByVal curDateHeure As Date, ByRef drVisite As DataRow, ByRef htCodes As Hashtable, ByVal service As String) As Integer
        Dim rows() As DataRow = dsInfoFolders.Tables(0).Select("NoClient=" & noClient1)
        If rows IsNot Nothing AndAlso rows.Length > 0 Then
            Dim noCodification As Integer = 0
            If drVisite IsNot Nothing Then
                If htCodes.ContainsKey(drVisite("CODE").ToString.Trim) Then
                    noCodification = htCodes(drVisite("CODE").ToString.Trim)
                Else
                    noCodification = 2
                End If
            End If

            For Each curRow As DataRow In rows
                If noTRP = curRow("NoTRPTraitant") Then
                    If drVisite IsNot Nothing Then
                        If curRow.Item("NoCodeUnique") = noCodification AndAlso curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(curDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(curDateHeure) >= 0) Then
                            Return curRow("NoFolder")
                        End If
                    Else
                        If curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(curDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(curDateHeure) >= 0) Then
                            Return curRow("NoFolder")
                        End If
                    End If
                Else
                    If drVisite IsNot Nothing Then
                        If curRow.Item("NoCodeUnique") = noCodification AndAlso curRow("Service") = service AndAlso curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(curDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(curDateHeure) > 0) Then
                            Return curRow("NoFolder")
                        End If
                    Else
                        If curRow("Service") = service AndAlso curRow("Debut") IsNot DBNull.Value AndAlso CType(curRow("Debut"), Date).CompareTo(curDateHeure) <= 0 AndAlso (curRow("Fin") Is DBNull.Value OrElse CType(curRow("Fin"), Date).AddDays(1).CompareTo(curDateHeure) >= 0) Then
                            Return curRow("NoFolder")
                        End If
                    End If
                End If
            Next
        End If

        Return 0
    End Function


    Public Function isAlpha(ByRef myChars As String, Optional ByVal extraChar As String = "", Optional ByRef acceptNumeric As Boolean = False, Optional ByVal onlyAlphabet As Boolean = True, Optional ByVal refuseAccents As Boolean = False, Optional ByVal onlyextraChars As Boolean = False) As Boolean
        Dim i As Integer
        Dim AlphaNum(), characters As String
        Dim found As Boolean
        'Formation du tableau Alpha Numérique
        'Index des différentes plages de caractères
        '0-9 : 0-9
        'A-Z : 10-35
        'Accents : 36-56
        'ExtraChar starts at 57
        If Not extraChar = "" And onlyextraChars = False Then extraChar = "§" & extraChar
        Dim accents As String = "§à§â§ä§á§è§é§ê§ë§ç§ï§î§ì§í§ò§ó§ô§ö§ù§ú§û§ü"
        Dim numbers As String = "0§1§2§3§4§5§6§7§8§9§"
        If onlyextraChars Then
            characters = extraChar
        Else
            characters = IIf(acceptNumeric, numbers, "") & "a§b§c§d§e§f§g§h§i§j§k§l§m§n§o§p§q§r§s§t§u§v§w§x§y§z" & IIf(refuseAccents, "", accents) & extraChar
        End If
        AlphaNum = Split(characters, "§")

        'If LastChar = -1 Then LastChar = UBound(AlphaNum)

        Dim curChars() As Char = myChars.ToCharArray()
        For i = 1 To myChars.Length
            found = characters.IndexOf(Char.ToLowerInvariant(curChars(i - 1))) <> -1
            'Found = Array.BinarySearch(AlphaNum, curChars(i - 1).ToString.ToLower) >= 0
            'Found = Array.IndexOf(AlphaNum, curChars(i - 1).ToString.ToLower) <> -1

            If onlyextraChars = False AndAlso found = False AndAlso onlyAlphabet = False AndAlso IsNumeric(Mid(myChars, i, 1)) = False Then found = True

            If found = False Then Return False
        Next i

        isAlpha = True
    End Function


    Public Function onlyNumeric(ByRef texte As String, Optional ByVal acceptedChar As String = "") As String
        Dim n, t As Integer
        n = 1
        If acceptedChar <> "" Then acceptedChar &= "§"
        acceptedChar &= "0§1§2§3§4§5§6§7§8§9"

        Do
            If Not texte = "" Then
                If isAlpha(Mid(texte, n, 1), acceptedChar, True, False, False, True) = False Then
                    t = Len(texte)
                    If t = 1 Then
                        texte = ""
                    ElseIf t = 2 And n = 1 Then
                        texte = Microsoft.VisualBasic.Right(texte, 1)
                    ElseIf t = 2 And n = 2 Then
                        texte = Microsoft.VisualBasic.Left(texte, 1)
                    Else
                        texte = Microsoft.VisualBasic.Left(texte, n - 1) & Microsoft.VisualBasic.Right(texte, Len(texte) - n)
                    End If
                Else
                    n = n + 1
                End If
            End If
        Loop Until n > Len(texte)

        onlyNumeric = texte
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
            Dim noCode As Integer = 0

            With dsAgenda.Tables(0).Rows
                statut = .Item(i)("C" & timeString).ToString
                If statut.Length > (position - 1) * 3 Then statut = statut.Substring((position - 1) * 3)
                If statut.Length > 3 Then statut = statut.Substring(0, 3)
                If statut.IndexOf("C") >= 0 Then Exit Sub 'Quitte, car il ne s'agit pas d'un RV
                If statut = "" Then
                    noStatut = 3
                Else
                    Select Case statut.Substring(0, 1)
                        Case "A"
                            noStatut = 1
                        Case "X"
                            noStatut = 2
                        Case " "
                            noStatut = 3
                        Case "P", "Z"
                            noStatut = 4
                            If InStr(statut, "A") > 0 Then noStatut = 1
                    End Select
                End If
                If noStatut = 0 Then noStatut = 3

                If noStatut = 3 And Date.Compare(curDateHeure, CDate(dataDate.Text)) < 0 Then Exit Sub

                If drVisite Is Nothing Then
                    drVisite = getDrVisite(dsVisites, curDateHeure, "------------", equipe, position)
                    If drVisite IsNot Nothing AndAlso noClient1 = 0 Then
                        Dim nam As String = drVisite("NAM").ToString.Trim.Replace("-", "")
                        If htNAM.ContainsKey(nam) = False Then Exit Sub

                        noClient1 = htNAM(nam)
                    End If
                    If noClient1 > 0 And noFolder1 = 0 Then noFolder1 = getNoFolder(getNoTRP(drVisite, htNIP, htTRP, equipe, position), dsInfoFolders, noClient1, curDateHeure, drVisite, htCodes, service)
                End If

                'S'assure que le numéro du dossier avait été trouvé
                If noFolder1 = 0 And textAgenda.IndexOf(",") <> -1 Then
                    Dim externes() As Integer = {2, 4, 5}
                    Dim noms() As String = textAgenda.Split(",")
                    Dim nbFoundTRP, nbFoundExt, nbFoundNonExt As Integer
                    Dim lastFound As Byte = 0
                    Dim dossiers() As DataRow = dsInfoFolders.Tables(0).Select("Nom='" & noms(0).Replace("'", "''") & "' AND Prenom='" & noms(1).Replace("'", "''") & "'")
                    If dossiers IsNot Nothing AndAlso dossiers.Length <> 0 Then
                        For d As Integer = 0 To dossiers.GetUpperBound(0)
                            noCode = dossiers(d)("NoCodeUnique")

                            If dossiers(d)("NoTRPTraitant") = getNoTRP(drVisite, htNIP, htTRP, equipe, position) Then
                                noFolder1 = dossiers(d)("NoFolder")
                                noClient1 = dossiers(d)("NoClient")
                                nbFoundTRP += 1
                                lastFound = 1
                            Else
                                If statut.IndexOf("$") >= 0 And Array.IndexOf(externes, dossiers(d)("NoCodeUnique")) >= 0 Then
                                    noFolder1 = dossiers(d)("NoFolder")
                                    noClient1 = dossiers(d)("NoClient")
                                    nbFoundExt += 1
                                    lastFound = 2
                                ElseIf statut.IndexOf("$") < 0 And Array.IndexOf(externes, dossiers(d)("NoCodeUnique")) < 0 Then
                                    noFolder1 = dossiers(d)("NoFolder")
                                    noClient1 = dossiers(d)("NoClient")
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
                noTRP = 0
                drPaiement = Nothing
                If drVisite IsNot Nothing Then
                    If noStatut = 3 Then noStatut = 4
                    noTRP = getNoNIP(htNIP, drVisite("NIPTHE").ToString.Trim, True)
                    If drVisite("DATEPMT").ToString <> "" Then
                        rows = dsPaiement.Tables(0).Select("DATE='" & drVisite("DATEPMT").ToString & "' AND CODE='" & drVisite("CODE").ToString & "' AND NAM='" & drVisite("NAM").ToString & "'")
                        If rows IsNot Nothing AndAlso rows.Length > 0 Then drPaiement = rows(0)
                    End If
                Else
                    REM this one can't be taken from GetNoNIP because it's not a nip but an Equipe & Position
                    noTRP = IIf(htTRP.ContainsKey(equipe & "-" & position), htTRP(equipe & "-" & position), 0)
                End If

                If noFolder1 = 0 Then
                    DBLinker.getInstance(True).writeDB("Agenda", "DateHeure,Periode,NoTRP,Reserve,NoStatut", "'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "',15," & noTRP & ",'" & textAgenda.Replace("'", "''") & "',6")
                    Exit Sub 'Quitte si toujours aucun numéro de dossier
                End If

                noVisite += 1
                If scripting.Checked Then
                    sbSQL.Append("INSERT INTO InfoVisites (NoClient,NoFolder,NoStatut,NoFacture,NoTRP,DateHeure,Periode,Service,Confirmed,Evaluation, Flagged, IsOnAgenda,Remarques,IsAnnounced,ExternalStatus) VALUES(").Append(noClient1).Append(",").Append(noFolder1).Append(",").Append(noStatut).Append(",null,").Append(noTRP).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append(" ").Append(curDateHeure.Hour).Append(":").Append(curDateHeure.Minute).Append("',15,'Physiothérapie',").Append(IIf(curDateHeure.Date.CompareTo(CDate(dataDate.Text)) < 0, 1, 0)).Append(",").Append(IIf(statut.IndexOf("I") >= 0, 1, 0)).Append(",0,1,'',1," & If(noCode = 1, "2", "1")).AppendLine(")")
                Else
                    DBLinker.getInstance(True).writeDB("InfoVisites", "NoClient,NoFolder,NoStatut,NoFacture,NoTRP,DateHeure,Periode,Service,Confirmed,Evaluation, Flagged, IsOnAgenda,Remarques,IsAnnounced,ExternalStatus", _
                    noClient1 & "," & noFolder1 & "," & noStatut & ",null," & noTRP & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "',15,'" & service & "'," & IIf(curDateHeure.Date.CompareTo(CDate(Me.dataDate.Text)) < 0, 1, 0) & "," & IIf(statut.IndexOf("I") >= 0, 1, 0) & ",0,1,'',1," & If(noCode = 1, "2", "1"))
                End If

                Dim billStats As Boolean = False
                noFacture += 1
                If drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString = "0" Then Exit Sub

                Dim uselessFacturesFields As String = "0,0,null,'',null,'',null,0,0,null,null,null,0,0,null,"
                Dim montantFacture As String = If(drVisite Is Nothing, "0", drVisite("MONTANT").ToString.Replace(",", "."))
                Dim drVisiteMontant As Double = 0
                If drVisite IsNot Nothing AndAlso drVisite("MONTANT") IsNot DBNull.Value Then drVisiteMontant = drVisite("MONTANT")
                Dim drPaiementMontant As Double = 0
                If drPaiement IsNot Nothing AndAlso drPaiement("MONTANT") IsNot DBNull.Value Then drPaiementMontant = drPaiement("MONTANT")

                Dim paiement As Double = If(drVisite Is Nothing, 0, If(drPaiement Is Nothing, drVisiteMontant, IIf(drPaiementMontant > drVisiteMontant, drVisiteMontant, drPaiementMontant)))
                Dim montantPaiement As String = paiement.ToString.Replace(",", ".")
                Dim noRecu As String = If(drVisite Is Nothing, "null", IIf(drVisite("NORECU") Is DBNull.Value, "null", DBLinker.getInstance(True).addItemToADBList("ListeNoRecus", "NoRecu", drVisite("NORECU").ToString.Trim, "NoNoRecu").ToString()))
                Dim typePaiement As String = ""
                Dim comments As String = String.Empty

                If drPaiement IsNot Nothing Then
                    Select Case drPaiement("TYPEPMT").ToString
                        Case "AR"
                            typePaiement = "Argent"
                        Case "CH"
                            typePaiement = "Chèque"
                        Case "MC"
                            typePaiement = "MasterCard"
                        Case "VI"
                            typePaiement = "Visa"
                    End Select

                    comments = drPaiement("NOREF").ToString
                    If comments.ToUpper.StartsWith("D") Then
                        typePaiement = "Débit"
                        comments = ""
                    End If

                    If comments <> String.Empty Then
                        comments = onlyNumeric(comments)
                        If comments.Length < 3 Then comments = ""
                    End If
                End If



                If drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString <> "" AndAlso drPaiement IsNot Nothing Then

                    If paiement < drVisite("MONTANT") Then
                        If scripting.Checked Then
                            sbSQL.Append("INSERT INTO Factures (MontantPaiementUser,MontantPaiementClinique,ParNoClinique,IsSouffrance,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere,NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP,TaxesApplication) VALUES(").Append(uselessFacturesFields).Append(noFacture).Append(",").Append(getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append("',").Append(noFolder1).Append(",").Append(noClient1).Append(",").Append(If(noCode = 1, "0", montantFacture)).Append( _
                                    ",'").Append(service).Append("',").Append(If(noCode = 1, "0", montantPaiement)).Append(",").Append(noVisite).Append("," & If(noCode = 1, "1", "null") & ",").Append(noClient1).AppendLine("," & If(noCode = 1, montantFacture, "0") & "," & If(noCode = 1, montantPaiement, "0") & ",0)")
                        Else
                            DBLinker.getInstance(True).writeDB("Factures", "MontantPaiementUser,MontantPaiementClinique,ParNoClinique,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere" _
                                    & ",NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP,IsSouffrance,TaxesApplication", _
                                    uselessFacturesFields & _
                                    noFacture & "," & getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & "'," & noFolder1 & "," & noClient1 & "," & If(noCode = 1, "0", montantFacture) & _
                                    ",'" & service & "'," & If(noCode = 1, "0", montantPaiement) & "," & noVisite & "," & If(noCode = 1, "1", "null") & "," & noClient1 & "," & If(noCode = 1, montantFacture, "0") & "," & If(noCode = 1, montantPaiement, "0") & ",0,0")
                        End If
                    End If

                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO StatPaiements (Commentaires,NoAction,ParNoUser,ParNoClinique,NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,EntitePayeur,ParNoClient,ParNoKP,noVisite) VALUES(" & comments & ",12,null,null,").Append(getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append(" ").Append(curDateHeure.Hour).Append(":").Append(curDateHeure.Minute).Append("',").Append(noFacture).Append(",").Append(montantPaiement).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append(" ").Append(curDateHeure.Hour).Append(":").Append(curDateHeure.Minute).Append("','").Append(typePaiement).Append("',").Append(noClient1).Append(",").Append(noFolder1).Append("," & If(noCode = 1, "2", "1") & ",").Append(noClient1).AppendLine("," & If(noCode = 1, "1", "null") & "," & noVisite & ")")
                    Else
                        DBLinker.getInstance(True).writeDB("StatPaiements", "Commentaires,NoAction,ParNoUser,ParNoClinique," & _
                                        "NoNoRecu,NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,NoEntitePayeur,ParNoClient,ParNoKP,noVisite", _
                                        "'" & comments.Replace("'", "''") & "',12,null,null," & _
                                         noRecu & "," & getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "'," & noFacture & "," & montantPaiement & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "','" & typePaiement & "'," & noClient1 & "," & noFolder1 & "," & If(noCode = 1, "2", "1") & "," & noClient1 & "," & If(noCode = 1, "1", "null") & "," & noVisite)
                    End If

                    billStats = True
                ElseIf drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString <> "" AndAlso drPaiement Is Nothing AndAlso drVisite("PAIEMENT") > 0 Then
                    If drVisite("PAIEMENT") < drVisite("MONTANT") Then
                        If scripting.Checked Then
                            sbSQL.Append("INSERT INTO Factures (MontantPaiementUser,MontantPaiementClinique,ParNoClinique,IsSouffrance,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere,NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP) VALUES(0,0,0,0,'',0,'',0,0,0,0,0,0,0,0,0,").Append(noFacture).Append(",").Append(getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append("',").Append(noFolder1).Append(",").Append(noClient1).Append(",").Append(drVisite("MONTANT").ToString.Replace(",", ".")).Append( _
                                    ",'").Append(service).Append("',").Append(IIf(drPaiement("MONTANT") > drVisite("MONTANT"), drVisite("MONTANT").ToString.Replace(",", "."), drPaiement("MONTANT").ToString.Replace(",", "."))).Append(",").Append(noVisite).Append(",0,").Append(noClient1).AppendLine(",0,0)")
                        Else
                            DBLinker.getInstance(True).writeDB("Factures", "MontantPaiementUser,MontantPaiementClinique,ParNoClinique,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere" _
                                    & ",NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP,IsSouffrance,TaxesApplication", _
                                    uselessFacturesFields & _
                                    noFacture & "," & getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & "'," & noFolder1 & "," & noClient1 & "," & If(noCode = 1, "0", montantFacture) & _
                                    ",'" & service & "'," & If(noCode = 1, "0", montantPaiement) & "," & noVisite & "," & If(noCode = 1, "1", "null") & "," & noClient1 & "," & If(noCode = 1, montantFacture, "0") & "," & If(noCode = 1, montantPaiement, "0") & ",0,0")
                        End If
                    End If

                    typePaiement = "Inconnu"

                    DBLinker.getInstance(True).writeDB("StatPaiements", "Commentaires,NoAction,ParNoUser,ParNoClinique," & _
                                        "NoNoRecu,NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,NoEntitePayeur,ParNoClient,ParNoKP,NoVisite", _
                                        "'',12,null,null," & _
                                        noRecu & "," & getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "'," & noFacture & "," & montantPaiement & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "','" & typePaiement & "'," & noClient1 & "," & noFolder1 & "," & If(noCode = 1, "2", "1") & "," & noClient1 & "," & If(noCode = 1, "1", "null") & "," & noVisite)
                    billStats = True
                ElseIf drVisite IsNot Nothing AndAlso drVisite("MONTANT").ToString <> "" Then
                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO Factures (MontantPaiementUser,MontantPaiementClinique,ParNoClinique,IsSouffrance,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere,NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP) VALUES(0,0,0,0,'',0,'',0,0,0,0,0,0,0,0,0,").Append(noFacture).Append(",").Append(getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append("',").Append(noFolder1).Append(",").Append(noClient1).Append(",").Append(drVisite("MONTANT").ToString.Replace(",", ".")).Append( _
                                    ",'").Append(service).Append("',0,").Append(noVisite).Append(",0,").Append(noClient1).AppendLine(",0,0)")
                    Else
                        DBLinker.getInstance(True).writeDB("Factures", "MontantPaiementUser,MontantPaiementClinique,ParNoClinique,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,NoKP,NoUserFacture,ParNoUser,MontantFactureUser,MontantFactureClinique,NoFactureTransfere" _
                                    & ",NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,MontantPaiement,NoVisite,ParNoKP,ParNoClient,MontantFactureKP,MontantPaiementKP,IsSouffrance,TaxesApplication", _
                                    uselessFacturesFields & _
                                    noFacture & "," & getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & "'," & noFolder1 & "," & noClient1 & "," & If(noCode = 1, "0", montantFacture) & _
                                    ",'" & service & "',0," & noVisite & "," & If(noCode = 1, "1", "null") & "," & noClient1 & "," & If(noCode = 1, montantFacture, "0") & ",0,0,0")
                    End If
                    billStats = True
                End If

                If billStats Then 'Ajoute les stat de la facture
                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO StatFactures (NoAction,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,ParNoUser,NoKP,NoUserFacture,NoFactureTransfere,Commentaires,ParNoClinique,NoUser,DateHeureCreation,NoFacture,NoFolder,NoClient,MontantFacture,TypeFacture,NoVisite,DateFacture,ParNoKp,ParNoClient,EntitePayeur) VALUES(5,'',0,'',0,0,0,0,0,0,0,'',0,").Append(getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append(" ").Append(curDateHeure.Hour).Append(":").Append(curDateHeure.Minute).Append("',").Append(noFacture).Append(",").Append(noFolder1).Append(",").Append(noClient1).Append(",").Append(drVisite("MONTANT").ToString.Replace(",", ".")).Append( _
                                    ",'").Append(service).Append("',").Append(noVisite).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append(" ").Append(curDateHeure.Hour).Append(":").Append(curDateHeure.Minute).Append("',0,").Append(noClient1).AppendLine(",'C')")
                        sbSQL.Append("UPDATE InfoVisites SET NoFacture=").Append(noFacture).Append(" WHERE NoVisite=").AppendLine(noVisite)
                    Else
                        DBLinker.getInstance(True).writeDB("StatFactures", "NoAction,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,ParNoUser,NoKP,NoUserFacture,NoFactureTransfere,Commentaires,ParNoClinique," & _
                                    "NoUser,DateHeureCreation,NoFacture,NoFolder,NoClient,MontantFacture,TypeFacture,NoVisite,DateFacture,ParNoKp,ParNoClient,NoEntitePayeur,TaxesApplication", _
                                    "5,'',null,'',null,0,0,null,null,null,null,'" & comments.Replace("'", "''") & "',null," & _
                                    getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False) & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "'," & noFacture & "," & noFolder1 & "," & noClient1 & "," & If(noCode = 1, "0", montantFacture) & _
                                    ",'" & service & "'," & noVisite & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "'," & If(noCode = 1, "1", "null") & "," & noClient1 & "," & If(noCode = 1, "2", "1") & ",0")
                        DBLinker.getInstance(True).updateDB("InfoVisites", "NoFacture=" & noFacture, "NoVisite", noVisite, False)
                    End If
                End If

                Dim noCreateur As Integer = 0
                If drVisite IsNot Nothing Then noCreateur = getNoNIP(htNIP, drVisite("NIPCR").ToString.Trim, False)
                If scripting.Checked Then
                    sbSQL.Append("INSERT INTO StatVisites (NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder) VALUES(").Append(noCreateur).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append(" ").Append(curDateHeure.Hour).Append(":").Append(curDateHeure.Minute).Append("',6,").Append(noVisite).Append(",").Append(noClient1).Append(",").Append(noFolder1).AppendLine(")")
                Else
                    DBLinker.getInstance(True).writeDB("StatVisites", "NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder,Comments", _
                                    noCreateur & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "',6," & noVisite & "," & noClient1 & "," & noFolder1 & ",''")
                End If

                If noStatut <> 3 Then
                    If scripting.Checked Then
                        sbSQL.Append("INSERT INTO StatVisites (NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder) VALUES(").Append(noCreateur).Append(",'").Append(curDateHeure.Year).Append("/").Append(curDateHeure.Month).Append("/").Append(curDateHeure.Day).Append(" ").Append(curDateHeure.Hour).Append(":").Append(curDateHeure.Minute).Append("',").Append(noStatut).Append(",").Append(noVisite).Append(",").Append(noClient1).Append(",").Append(noFolder1).AppendLine(")")
                    Else
                        DBLinker.getInstance(True).writeDB("StatVisites", "NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder,Comments", _
                                        noCreateur & ",'" & curDateHeure.Year & "/" & curDateHeure.Month & "/" & curDateHeure.Day & " " & curDateHeure.Hour & ":" & curDateHeure.Minute & "'," & noStatut & "," & noVisite & "," & noClient1 & "," & noFolder1 & ",''")
                    End If
                End If
            End With
            n += 1
        Catch ex As Exception
            ex = ex
            erreurs.Text &= "Rendez-vous de la table AGENDA" & equipe & " - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf
            If ex.InnerException IsNot Nothing Then erreurs.Text &= ex.InnerException.Message & vbCrLf
            erreurs.Text &= vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1
            nbErreurs += 1
        End Try

        If scripting.Checked AndAlso n Mod 1000 = 0 Then
            IO.File.WriteAllText("C:\temp\transfer-agendas" & (n / 1000) & ".sql", sbSQL.ToString())
            sbSQL = New System.Text.StringBuilder
            GC.Collect(5)
        End If
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub

        transferData()
    End Sub

    Public Function getFirstCellularIndex(ByVal text As String)
        Dim index As Integer = text.IndexOf("5")
        If index <> -1 Then Return index

        Return text.IndexOf("4")
    End Function

    Public Function firstLetterCapital(ByVal stringToConvert As String, Optional ByVal allWords As Boolean = False) As String
        Dim i As Integer
        Dim previousChar As Char = Nothing
        Dim myChar As Char = Nothing
        Dim LeftPart, rightPart As String
        Dim upTo As Integer = 0

        For i = 1 To stringToConvert.Length - 1
            myChar = Mid(stringToConvert, i, 1) : LeftPart = "" : rightPart = ""
            If previousChar = Nothing Or previousChar = " " Or previousChar = "-" Or previousChar = "'" Then
                upTo += 1
                If i > 1 Then LeftPart = stringToConvert.Substring(0, i - 1)
                If i < stringToConvert.Length Then rightPart = stringToConvert.Substring(i, stringToConvert.Length - i)
                stringToConvert = LeftPart & Char.ToUpper(myChar) & rightPart
                If allWords = False And upTo = 1 Then Exit For
            End If
            previousChar = myChar
        Next i

        Return stringToConvert
    End Function

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim curLog As String = erreurs.Text
        IO.File.WriteAllText("C:\temp\transfer.log", curLog)
        End
    End Sub

    Public Function affTextDate(ByVal dateToConvert As Date, Optional ByVal options As DateFormat.TextDateOptions = DateFormat.TextDateOptions.YYYYMMDD, Optional ByVal shortSeparator As String = ".", Optional ByVal dateSeparator As String = "/") As String
        Dim MyYear, MyMonth, MyDay, MyHour, MyMinute, mySecond As String
        MyYear = dateToConvert.Year
        MyMonth = dateToConvert.Month
        MyDay = dateToConvert.Day
        MyHour = dateToConvert.Hour
        MyMinute = dateToConvert.Minute
        mySecond = dateToConvert.Second

        If MyMonth < 10 Then MyMonth = "0" & MyMonth
        If MyDay < 10 Then MyDay = "0" & MyDay
        If MyHour < 10 Then MyHour = "0" & MyHour
        If MyMinute < 10 Then MyMinute = "0" & MyMinute
        If mySecond < 10 Then mySecond = "0" & mySecond

        Select Case options
            Case DateFormat.TextDateOptions.YYYYMMDD
                Return MyYear & dateSeparator & MyMonth & dateSeparator & MyDay
            Case DateFormat.TextDateOptions.DDMMYYYY
                Return MyDay & dateSeparator & MyMonth & dateSeparator & MyYear
            Case DateFormat.TextDateOptions.MMDDYYYY
                Return MyMonth & dateSeparator & MyDay & dateSeparator & MyYear
            Case DateFormat.TextDateOptions.FullTime
                Return MyHour & ":" & MyMinute & ":" & mySecond
            Case DateFormat.TextDateOptions.ShortTime
                Return MyHour & ":" & MyMinute
        End Select

        'Return default
        Return MyYear & dateSeparator & MyMonth & dateSeparator & MyDay
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
