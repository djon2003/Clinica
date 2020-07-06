Public Class Form1

    Private presencesCodes As New Dictionary(Of String, Integer)
    Private noVisite As Integer = 0
    Private noFacture As Integer = 0
    Private nbErreurs As Integer = 0
    Private sbSQL As New System.Text.StringBuilder()
    Private lastRVByDateByTRP As New Dictionary(Of Integer, Dictionary(Of String, Date))
    Private initTemplate As String = String.Empty
    Private stepTemplate As String = String.Empty


    Private htDossiers As New Dictionary(Of String, Integer)(10000)
    Private htNAM As New Dictionary(Of String, Integer)(10000)
    Private htClients As New Dictionary(Of String, Integer)(10000)
    Private htReports As New Dictionary(Of String, DataRow())(10000)

    Private Sub loadRVsTimes()
        lastRVByDateByTRP.Clear()

        Dim hours As DataSet = DBLinker.getInstance(True).readDBForGrid("SELECT NoTRP,D, MAX(DH) AS DH FROM (SELECT NoTRP,DateHeure AS DH, DATEADD(dd, 0, DATEDIFF(dd, 0, DateHeure)) AS D FROM InfoVisites) AS T GROUP BY NoTRP, D")
        If hours Is Nothing OrElse hours.Tables.Count = 0 OrElse hours.Tables(0).Rows.Count = 0 Then Exit Sub

        For Each curRow As DataRow In hours.Tables(0).Rows
            Dim d As String = CDate(curRow("D")).ToString("yyyy/MM/dd")
            Dim dh As Date = curRow("DH")
            If lastRVByDateByTRP.ContainsKey(curRow("NoTRP")) = False Then lastRVByDateByTRP.Add(curRow("NoTRP"), New Dictionary(Of String, Date))
            If lastRVByDateByTRP(curRow("NoTRP")).ContainsKey(d) = False Then
                lastRVByDateByTRP(curRow("NoTRP")).Add(d, dh)
            ElseIf lastRVByDateByTRP(curRow("NoTRP"))(d) < dh Then
                lastRVByDateByTRP(curRow("NoTRP"))(d) = dh
            End If
        Next
    End Sub

    Private Sub lockItems(ByVal trueFalse As Boolean)
        allParts.Enabled = Not trueFalse

        Button1.Enabled = Not trueFalse
        Client.Enabled = Not trueFalse
        scripting.Enabled = Not trueFalse
        mapfromfile.Enabled = Not trueFalse
        Clinique.Enabled = Not trueFalse
        RVs.Enabled = Not trueFalse
        medecins.Enabled = Not trueFalse
        Users.Enabled = Not trueFalse
        If trueFalse Then
            updateTRPNoPermis.Enabled = False
        Else
            activateUpdateNoPermis()
        End If
    End Sub

    Private Sub activateUpdateNoPermis()
        Dim allChecked As Boolean = medecins.Checked AndAlso Clinique.Checked AndAlso Users.Checked AndAlso Client.Checked AndAlso RVs.Checked
        Dim noneChecked As Boolean = Not medecins.Checked AndAlso Not Clinique.Checked AndAlso Not Users.Checked AndAlso Not Client.Checked AndAlso Not RVs.Checked
        allParts.Checked = allChecked AndAlso updateTRPNoPermis.Checked
        updateTRPNoPermis.Enabled = allChecked OrElse noneChecked
        updateTRPNoPermis.Checked = updateTRPNoPermis.Checked AndAlso updateTRPNoPermis.Enabled
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        transferData()
    End Sub

    Private Function initDB() As Boolean
        Dim dbName As String = InputBox("Entrer le nom de la base de données de Clinica", "DB name", "ClinicaCM")

        Dim n As Integer = 0
        DBLinker.getInstance(False).initConnection(OpenFileDialog1.FileName)

        Dim serveur As String = InputBox("Adresse serveur", "serveur", ".\SQLEXPRESS")
        If serveur = String.Empty Then Return False

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


    Private Sub transferData()
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub

        lockItems(True)

        Dim startingTime As Date = Now

        If Not initDB() OrElse Not initTemplates() Then
            lockItems(False)
            Exit Sub
        End If

        transferDoctors()
        transferClinic()
        Dim htTRP As Dictionary(Of String, Integer) = transferUsers()

        If Client.Checked OrElse RVs.Checked Then
            loadRVsTimes()

            Dim dsPresence As DataSet = DBLinker.getInstance(False).readDBForGrid("PRESENCE", "*")
            transferClientsFoldersAndTexts(dsPresence, htTRP)

            transferPresences(dsPresence, htTRP)
            'Adjust table to generate BillNo
            DBLinker.executeSQLScript("BEGIN DBCC CHECKIDENT ('FacturesNumbers',RESEED," & noFacture & ") END")
        End If

        If updateTRPNoPermis.Checked Then
            DBLinker.getInstance(True).updateDB("Utilisateurs", "NoPermis = Adresse, Adresse = ''", "Adresse", "", , "<>")
        End If

        erreurs.Text &= vbCrLf & vbCrLf & "Total time for transfer : " & Now.Subtract(startingTime).Hours & " hour(s) " & (Now.Subtract(startingTime).Minutes - Now.Subtract(startingTime).Hours * 60) & " minutes"

        DBLinker.getInstance(False).dbConnected = False
        DBLinker.getInstance(True).dbConnected = False

        lockItems(False)
    End Sub

    Private Function getText(ByVal textsData() As DataRow, ByVal noText As Integer) As DataRow
        For Each curRow As DataRow In textsData
            If curRow("NO_ETAP") = noText Then Return curRow
        Next

        Return Nothing
    End Function

    Private Function getNoTRP(ByRef htNIP As Dictionary(Of String, Integer), ByVal reportsData() As DataRow) As Integer
        Dim nip As String = reportsData(reportsData.Length - 1)("NO_THERA_CORPO")
        Dim nip2 As String = String.Empty
        If reportsData(reportsData.Length - 1)("NO_THERA_READAP_PHYS") IsNot DBNull.Value Then nip2 = reportsData(reportsData.Length - 1)("NO_THERA_READAP_PHYS")
        If htNIP.ContainsKey(nip) Then Return htNIP(nip)
        If htNIP.ContainsKey(nip2) Then Return htNIP(nip2)

        Dim userTable As DataTable = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "NoUser,NoPermis", "WHERE NoPermis='" & nip.Replace("'", "''") & "' OR NoPermis='" & nip2.Replace("'", "''") & "'").Tables(0)
        Dim noUser As Integer = 0
        If userTable.Rows.Count = 0 Then
            DBLinker.getInstance(True).writeDB("Utilisateurs", "DroitAcces,IsTherapist,Cle,MDP,Nom,Prenom,DateDebut,DateFin,NoTitre,NoType,NoPermis,NoTypeEmploye,Services,NotConfirmRVOnPasteOfDTRP,CodePostal,Telephones,Courriel,Adresse,URL", "'" & "321111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111',1,'2008-04-0818:51:28.5312500','00','" & nip.Replace("'", "''") & "','" & nip.Replace("'", "''") & "','2008/01/01','2008/01/01'," & DBLinker.getInstance(True).addItemToADBList("Titres", "Titre", "Utilisateur supprimé", "NoTitre") & ",0,'',1,'Physiothérapie',0,'','','','',''")
            noUser = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "MAX(NoUser)").Tables(0).Rows(0)(0)
        Else
            noUser = userTable.Rows(0)(0)
            nip = userTable.Rows(0)("NoPermis")
        End If

        htNIP.Add(nip, noUser)

        Return noUser
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
        Me.Close()
    End Sub

    Public Function affTextDate(ByVal dateToConvert As Date, Optional ByVal options As DateFormat.TextDateoptions = DateFormat.TextDateoptions.YYYYMMDD, Optional ByVal shortSeparator As String = ".", Optional ByVal dateSeparator As String = "/") As String
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

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        presencesCodes.Add("A", 2)
        presencesCodes.Add("I", 4)
        presencesCodes.Add("O", 4)
        presencesCodes.Add("P", 4)
        presencesCodes.Add("G", 4)
        presencesCodes.Add("J", 4)
        presencesCodes.Add("K", 4)
        presencesCodes.Add("L", 4)
        presencesCodes.Add("Z", 4)
        presencesCodes.Add("W", 4)
        presencesCodes.Add("V", 4)

        'Presences codes which are not real presences
        presencesCodes.Add("F", 0)
        presencesCodes.Add("E", 0)
        presencesCodes.Add("S", 0)
        presencesCodes.Add("X", 0)
        presencesCodes.Add("Y", 0)
    End Sub

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
                    Dim clientExists As DataSet = DBLinker.getInstance(True).readDBForGrid("KeyPeople", "NoKP", "WHERE NoRef='" & .Item(i)("NO_MEDE").ToString.Replace("'", "''") & "'")
                    If .Item(i)("NO_MEDE").ToString().Trim().ToUpper <> "0" And (clientExists Is Nothing OrElse clientExists.Tables(0).Rows.Count = 0) Then
                        Try
                            Dim nom As String = firstLetterCapital(.Item(i)("NOM").ToString.Trim.ToLower, True) & "," & firstLetterCapital(.Item(i)("PREN").ToString.Trim.ToLower, True)
                            Dim categorie As String = "Médecin"
                            DBLinker.getInstance(True).writeDB("KeyPeople", "DateHeureCreation,NoUser,Nom,Adresse,NoVille,CodePostal,AutreInfos,Telephones,NoCategorie,NoRef", _
                            "'" & .Item(i)("DAT_CREA").ToString() & "',0,'" & nom.Replace("'", "''") & "','',null,'','','" & _
                            "'," & DBLinker.getInstance(True).addItemToADBList("KPCategorie", "Categorie", firstLetterCapital(categorie.ToLower, True), "NoCategorie") & ",'" & .Item(i)("NO_MEDE").ToString.Trim().Replace("'", "''") & "'")
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
            Dim dsEtablissement As DataSet = DBLinker.getInstance(False).readDBForGrid("ETABLISSEMENT", "*")

            DataGridView1.DataSource = dsEtablissement
            DataGridView1.DataMember = "Table"

            With dsEtablissement.Tables(0).Rows(0)
                Dim exists As DataSet = DBLinker.getInstance(True).readDBForGrid("InfoClinique", "Count(*)")
                Try
                    Dim suite As String = .Item("APP_SUIT_CP").ToString.Trim()
                    suite = If(suite = String.Empty, suite, " #" & suite)
                    If exists.Tables(0).Rows(0)(0) > 0 Then
                        DBLinker.getInstance(True).updateDB("InfoClinique", "NoEtablissement='" & .Item("NO_ETAB").ToString.Trim.Replace("'", "''") & "',Nom=" & "'" & firstLetterCapital(.Item("NOM").ToString().ToLower, True).Trim.Replace("'", "''") & "',Adresse='" & .Item("NO_CIVIQ") & " " & firstLetterCapital(.Item("NOM_RUE").ToString().Trim.ToLower, True).Replace("'", "''") & suite & "',NoVille=" & _
                        DBLinker.getInstance(True).addItemToADBList("Villes", "NomVille", firstLetterCapital(.Item("NOM_VIL_MUNIC").ToString.Trim & " (" & firstLetterCapital(.Item("PROV_ETAT").ToString().ToLower, True).Trim & ")", True), "NoVille") & ",CodePostal='" & .Item("COD_POSTAL").ToString().Trim().ToUpper.Replace(" ", "").Replace("'", "''") & "',Telephone=''")
                    Else
                        DBLinker.getInstance(True).writeDB("InfoClinique", "NoEtablissement,Nom,Adresse,NoVille,CodePostal,Telephone", "'" & .Item("NO_ETAB").ToString.Trim.Replace("'", "''") & "','" & firstLetterCapital(.Item("NOM").ToString().Trim.ToLower, True).Replace("'", "''") & "','" & .Item("NO_CIVIQ") & " " & firstLetterCapital(.Item("NOM_RUE").ToString().Trim.ToLower, True).Replace("'", "''") & suite & "'," & _
                        DBLinker.getInstance(True).addItemToADBList("Villes", "NomVille", firstLetterCapital(.Item("NOM_VIL_MUNIC").ToString.Trim & " (" & firstLetterCapital(.Item("PROV_ETAT").ToString().ToLower, True).Trim & ")", True), "NoVille") & ",'" & .Item("COD_POSTAL").ToString().Trim().ToUpper.Replace(" ", "").Replace("'", "''") & "',''")
                    End If
                Catch ex As Exception
                    erreurs.Text &= "Clinique de la table PARA - entrée no:1 - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                    erreurs.SelectionStart = erreurs.Text.Length - 1
                End Try
            End With
        End If
    End Sub

    Private Function transferUsers() As Dictionary(Of String, Integer)
        Dim htTRP As New Dictionary(Of String, Integer)(10000)

        Dim dsTherapeute As DataSet = DBLinker.getInstance(False).readDBForGrid("THERAPEUTE", "*")

        'Utilisateurs
        If Users.Checked Then
            DataGridView1.DataSource = dsTherapeute
            DataGridView1.DataMember = "Table"
        End If
        Dim n As Integer = 0
        nbErreurs = 0
        With dsTherapeute.Tables(0).Rows
            For i As Integer = .Count - 1 To 0 Step -1
                Dim noPermis As String = .Item(i)("NO_THERA").ToString()
                If noPermis = "0" Then
                    n += 1
                    Continue For
                End If

                Me.Text = "Utilisateurs (Restant/ajoutés) " & i & ":" & n
                Application.DoEvents()

                Dim prenom, nom As String
                nom = firstLetterCapital(.Item(i).Item("NOM").ToString.Trim.ToLower, True)
                prenom = firstLetterCapital(.Item(i).Item("PREN").ToString.Trim.ToLower, True)
                Dim numbersInPrenom As String = System.Text.RegularExpressions.Regex.Replace(prenom, "[^0-9]", "")
                Dim trpNoPermis As String = String.Empty
                If numbersInPrenom <> String.Empty Then
                    Dim pos As Integer = prenom.IndexOf(numbersInPrenom)
                    pos = prenom.LastIndexOf(" ", pos)
                    trpNoPermis = prenom.Substring(pos).Trim
                    prenom = prenom.Substring(0, pos).Trim
                End If

                Dim exists As DataSet = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "NoUser", "WHERE NoPermis='" & noPermis & "'")
                If Users.Checked Then
                    If (exists Is Nothing OrElse exists.Tables(0).Rows.Count = 0) Then
                        Try
                            Dim titre As String = "Physiothérapeute"
                            Dim service As String = "Physiothérapie"
                            If noPermis.StartsWith("ERG") Then
                                titre = "Ergothérapeute"
                                service = "Ergothérapie"
                            ElseIf trpNoPermis <> String.Empty AndAlso trpNoPermis.StartsWith("PHY") = False Then
                                titre = "Thérapeute en réadaptation physique"
                            End If
                            DBLinker.getInstance(True).writeDB("Utilisateurs", "Adresse,Services,NotConfirmRVOnPasteOfDTRP,CodePostal,Telephones,Courriel,URL,DroitAcces,IsTherapist,Cle,MDP,Nom,Prenom,DateDebut,DateFin,NoTitre,NoType,NoPermis,NoTypeEmploye", _
                                                               "'" & trpNoPermis & "','" & service.Replace("'", "''") & "',0,'','','','','3211111111111111111111111111111111111111111111111111111111111101110111111111111111111111111111111111111111111111',1,'1958-08-0823:05:28.5375100','00','" & firstLetterCapital(nom.ToLower, True).Replace("'", "''") & "','" & firstLetterCapital(prenom.ToLower, True).Replace("'", "''") & "','" & .Item(i)("DAT_CREA").ToString.Trim & "',null," & DBLinker.getInstance(True).addItemToADBList("Titres", "Titre", titre, "NoTitre") & ",null,'" & noPermis.Trim.Replace("'", "''") & "',1")
                            n += 1
                        Catch ex As Exception
                            erreurs.Text &= "Utilisateur de la table PARAUTIL - entrée no:" & (i).ToString & " - entrée valeurs : " & ex.Message & vbCrLf & ex.InnerException.Message & vbCrLf & vbCrLf
                            erreurs.SelectionStart = erreurs.Text.Length - 1
                            nbErreurs += 1
                        End Try
                    End If
                    exists = DBLinker.getInstance(True).readDBForGrid("Utilisateurs", "NoUser", "WHERE NoPermis='" & noPermis & "'")
                End If
                If exists.Tables(0).Rows.Count > 0 Then
                    If htTRP.ContainsKey(noPermis) = False Then htTRP.Add(noPermis, exists.Tables(0).Rows(0)(0))
                End If
            Next i
        End With

        If Users.Checked Then
            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre d'utilisateurs ajoutés : " & n & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1
        End If

        Return htTRP
    End Function

    Private Function getFirstDate(ByVal presencesData() As DataRow, ByVal isEval As Boolean) As Date
        For Each curPresence As DataRow In presencesData
            If curPresence("COD_PRES") = "I" AndAlso isEval Then
                Return curPresence("DAT_PRES")
            ElseIf presencesCodes(curPresence("COD_PRES")) = 4 AndAlso curPresence("COD_PRES") <> "I" Then
                Return curPresence("DAT_PRES")
            End If
        Next

        Return PresencePrice.LIMIT_DATE
    End Function

    Private Sub transferClientsFoldersAndTexts(ByVal dsPresence As DataSet, ByVal htTRP As Dictionary(Of String, Integer))
        erreurs.Text &= vbCrLf & "Starting Clients generation : " & Now.ToString & vbCrLf & vbCrLf
        erreurs.SelectionStart = erreurs.Text.Length - 1

        Dim n As Integer = 0
        Dim n2 As Integer = 0
        htDossiers.Clear()
        htNAM.Clear()
        htClients.Clear()
        htReports.Clear()

        If mapfromfile.Checked And IO.File.Exists("C:\temp\transfer.dossiers") Then
            Dim dsRapport As DataSet = DBLinker.getInstance(False).readDBForGrid("RAPPORT", "*")

            Dim fileNam() As String = IO.File.ReadAllText("C:\temp\transfer.clients").Split(New Char() {vbCrLf})
            Dim fileDossiers() As String = IO.File.ReadAllText("C:\temp\transfer.dossiers").Split(New Char() {vbCrLf})
            Dim fileClients() As String = IO.File.ReadAllText("C:\temp\transfer.clients.folders").Split(New Char() {vbCrLf})
            For i As Integer = 0 To fileNam.GetUpperBound(0) - 1 Step 2
                htNAM.Add(fileNam(i).Trim, fileNam(i + 1).Trim)
            Next i
            For i As Integer = 0 To fileDossiers.GetUpperBound(0) - 1 Step 2
                Dim curKey As String = fileDossiers(i).Trim
                htDossiers.Add(curKey, fileDossiers(i + 1).Trim)
                Dim reportsData() As DataRow
                If htReports.ContainsKey(curKey) = False Then
                    reportsData = dsRapport.Tables(0).Select("NO_DOSS='" & curKey.Substring(0, curKey.Length - 1) & "' AND COD_TRAIT='" & curKey.Substring(curKey.Length - 1) & "'", "NO_ETAP")

                    If reportsData.Length = 0 Then
                        erreurs.Text &= "---> NO REPORTS FOR FOLDER NoRef #" & curKey & vbCrLf
                        Continue For
                    Else
                        htReports.Add(curKey, reportsData)
                    End If
                Else
                    reportsData = htReports(curKey)
                End If
            Next i
            For i As Integer = 0 To fileClients.GetUpperBound(0) - 1 Step 2
                htClients.Add(fileClients(i).Trim, fileClients(i + 1).Trim)
            Next i
        Else

            Dim dsTravailleur As DataSet = DBLinker.getInstance(False).readDBForGrid("TRAVAILLEUR", "*")
            DataGridView1.DataSource = dsTravailleur
            DataGridView1.DataMember = "Table"

            Dim dsInfoClients As DataSet = DBLinker.getInstance(True).readDBForGrid("InfoClients", "*")
            Dim dsInfoFolders As DataSet = DBLinker.getInstance(True).readDBForGrid("InfoFolders", "*")
            Dim dsKeyPeople As DataSet = DBLinker.getInstance(True).readDBForGrid("KeyPeople", "*")
            Dim siteLesionInconnu As Integer = DBLinker.getInstance(True).addItemToADBList("siteLesion", "siteLesion", "Inconnu", "NositeLesion")
            Dim nbClients As Integer = 0
            Dim nbDossiers As Integer = 0
            Dim lastNbErreurs As Integer = 0
            With dsTravailleur.Tables(0).Rows
                For i As Integer = .Count - 1 To 0 Step -1
                    Me.Text = "Clients (Restant/ajoutés) " & i & ":" & n
                    Application.DoEvents()
                    'Compte client
                    If .Item(i)("NAM").ToString().Trim <> "" Then
                        Dim isClient() As DataRow = dsInfoClients.Tables(0).Select("NAM='" & .Item(i)("NAM").ToString.Replace("-", "") & "'")

                        If Client.Checked = True AndAlso (isClient Is Nothing OrElse isClient.Length = 0) Then
                            nbClients += 1
                            Try
                                Dim nom As String = firstLetterCapital(.Item(i)("NOM").ToLower, True)
                                Dim prenom As String = firstLetterCapital(.Item(i)("PREN").ToLower, True)
                                Dim suite As String = .Item(i)("APP_SUIT").ToString.Trim()
                                suite = If(suite = String.Empty, suite, " #" & suite)
                                Dim adresse As String = .Item(i)("NO_CIVIQ").ToString.Trim.ToLower & " " & firstLetterCapital(.Item(i)("NOM_RUE").ToString.Trim.ToLower, True) & suite
                                Dim noVille As String = DBLinker.getInstance(True).addItemToADBList("Villes", "NomVille", firstLetterCapital(.Item(i)("NOM_VIL_MUNIC").ToString.ToLower.Trim, True) & " (" & firstLetterCapital(.Item(i)("PROV_ETAT").ToString.ToLower.Trim, True) & ")", "noVille")
                                Dim noEmployeur As String = "null"
                                Dim dateNaissance As String = .Item(i)("NAM").ToString().Substring(4, 6)
                                Dim dnExtraction() As String = (dateNaissance.Substring(0, 2) & "/" & dateNaissance.Substring(2, 2) & "/" & dateNaissance.Substring(4, 2)).Split(New Char() {"/"})
                                Dim isMan As Boolean = dnExtraction(1) < 50
                                dnExtraction(0) += 2000
                                If dnExtraction(0) > Date.Today.Year Then dnExtraction(0) -= 100
                                dnExtraction(1) = dnExtraction(1) Mod 50
                                dnExtraction(2) = dnExtraction(2) Mod 50
                                dateNaissance = String.Join("/", dnExtraction)

                                Dim noMetier As String = "null"
                                DBLinker.getInstance(True).writeDB("InfoClients", "DateHeureCreation,NoUser,Publipostage,Nom,Prenom,Adresse,NoVille,CodePostal,DateNaissance,NoEmployeur,NAM,SexeHomme,Telephones,NoMetier,AutreNom", _
                                "'1990/01/01',0,0,'" & nom.Replace("'", "''") & "','" & prenom.Replace("'", "''") & "','" & adresse.Replace("'", "''") & "'," & noVille & ",'" & .Item(i)("COD_POSTAL").ToString.Replace(" ", "").Replace("-", "").ToUpper & "','" & dateNaissance & "'," & _
                                noEmployeur & ",'" & .Item(i)("NAM").ToString().ToUpper.Replace("-", "").Trim & "'," & IIf(isMan, "1", "0").ToString() & ",''," & _
                                noMetier & ",''")

                                If lastNbErreurs <> nbErreurs Then
                                    lastNbErreurs = nbErreurs
                                    nbClients = DBLinker.getInstance(True).readDBForGrid("InfoClients", "MAX(NoClient)").Tables(0).Rows(0)(0)
                                End If

                                Dim t As DataRow = dsInfoClients.Tables(0).NewRow
                                t.Item("DateHeureCreation") = New Date(1990, 1, 1)
                                t.Item("NoUser") = 0
                                t.Item("Publipostage") = 0
                                t.Item("Nom") = nom
                                t.Item("Prenom") = prenom
                                t.Item("Adresse") = adresse
                                If noVille <> "null" Then t.Item("NoVille") = noVille
                                t.Item("CodePostal") = .Item(i)("COD_POSTAL").ToString.Replace(" ", "").Replace("-", "").ToUpper
                                t.Item("DateNaissance") = dateNaissance
                                If noEmployeur <> "null" Then t.Item("NoEmployeur") = noEmployeur
                                t.Item("NAM") = .Item(i)("NAM").ToString().ToUpper.Replace("-", "").Trim
                                t.Item("SexeHomme") = isMan
                                t.Item("Telephones") = ""
                                t.Item("AutreNom") = ""
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

                        If htNAM.ContainsKey(.Item(i)("NAM").ToString) = False AndAlso isClient.Length > 0 Then htNAM.Add(.Item(i)("NAM").ToString, isClient(0)("NoClient"))
                    End If
                Next i
            End With

            Dim dsRapport As DataSet = DBLinker.getInstance(False).readDBForGrid("RAPPORT", "*")
            Dim dsDossier As DataSet = DBLinker.getInstance(False).readDBForGrid("DOSSIER", "*")
            Dim dsDescription As DataSet = DBLinker.getInstance(False).readDBForGrid("DESCRIPTION", "*")
            DataGridView1.DataSource = dsDossier
            DataGridView1.DataMember = "Table"

            With dsDossier.Tables(0).Rows
                For i As Integer = 0 To .Count - 1
                    Me.Text = "Dossiers (Restant/ajoutés) " & (.Count - 1 - i) & ":" & n2
                    Application.DoEvents()

                    'Extract services
                    Dim reportsData() As DataRow = dsRapport.Tables(0).Select("NO_DOSS='" & .Item(i)("NO_DOSS").ToString & "'", "NO_ETAP")
                    Dim services As New Generic.Dictionary(Of String, Boolean)
                    For Each curReport As DataRow In reportsData
                        If services.ContainsKey(curReport("COD_TRAIT")) = False Then services.Add(curReport("COD_TRAIT"), True)
                    Next

                    'Dossier
                    For Each curService As String In services.Keys
                        transferOneFolder(.Item(i), curService, i, n2, nbDossiers, siteLesionInconnu, htTRP, dsInfoClients, dsInfoFolders, dsKeyPeople, dsRapport, dsPresence, dsDescription)
                    Next
                Next i
            End With

            Dim sbWritingMap As New System.Text.StringBuilder()
            For Each myKey As String In htDossiers.Keys
                sbWritingMap.AppendLine(myKey)
                sbWritingMap.AppendLine(htDossiers(myKey))
            Next
            IO.File.WriteAllText("C:\temp\transfer.dossiers", sbWritingMap.ToString)
            sbWritingMap.Remove(0, sbWritingMap.Length)
            For Each myKey As String In htNAM.Keys
                sbWritingMap.AppendLine(myKey)
                sbWritingMap.AppendLine(htNAM(myKey))
            Next
            IO.File.WriteAllText("C:\temp\transfer.clients", sbWritingMap.ToString)
            sbWritingMap.Remove(0, sbWritingMap.Length)
            For Each myKey As String In htClients.Keys
                sbWritingMap.AppendLine(myKey)
                sbWritingMap.AppendLine(htClients(myKey))
            Next
            IO.File.WriteAllText("C:\temp\transfer.clients.folders", sbWritingMap.ToString)
        End If

        erreurs.Text &= vbCrLf & "Ended Clients generation : " & Now.ToString & vbCrLf
        erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de clients ajoutés : " & n & " Nombre de dossiers ajoutés :" & n2 & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf
        erreurs.SelectionStart = erreurs.Text.Length - 1
    End Sub

    Private Sub transferPresences(ByVal dsPresence As DataSet, ByVal htTRP As Dictionary(Of String, Integer))
        If RVs.Checked = True Then
            nbErreurs = 0
            Dim n As Integer = 0
            Dim dsInfoVisites As DataSet = DBLinker.getInstance(True).readDBForGrid("InfoVisites", "NoFolder, DateHeure")

            Dim objNoVisite As String = DBLinker.getInstance(True).readDBForGrid("InfoVisites", "MAX(NoVisite)").Tables(0).Rows(0)(0).ToString
            Dim objNoFacture As String = DBLinker.getInstance(True).readDBForGrid("StatFactures", "MAX(NoFacture)").Tables(0).Rows(0)(0).ToString
            If objNoVisite <> "" Then noVisite = objNoVisite
            If objNoFacture <> "" Then noFacture = objNoFacture

            erreurs.Text &= vbCrLf & "Starting RVs generation : " & Now.ToString & vbCrLf & vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1

            Dim i As Integer = dsPresence.Tables(0).Rows.Count
            For Each curPresence As DataRow In dsPresence.Tables(0).Rows
                Me.Text = "Présences (Restantes/ajoutées) " & i & ":" & n
                Application.DoEvents()

                If curPresence("NO_DOSS") > 0 Then
                    Dim rvDate As Date = curPresence("DAT_PRES")
                    Dim rvExists() As DataRow = dsInfoVisites.Tables(0).Select("NoFolder=" & htDossiers(curPresence("NO_DOSS") & curPresence("COD_TRAIT")) & " AND DateHeure >= '" & rvDate.ToString("yyyy/MM/dd") & "' AND DateHeure < '" & rvDate.AddDays(1).ToString("yyyy/MM/dd") & "'")

                    If rvExists.Length = 0 Then transferOnePresence(curPresence, n, i, htTRP)
                    Application.DoEvents()
                End If

                i -= 1
            Next

            'Set FacturesNumbers seed to what it is supposed to be
            Dim lastNoFactureDS As DataSet = DBLinker.getInstance(True).readDBForGrid("StatFactures", "MAX(NoFacture)")
            Dim lastNoFacture As Integer = 0
            If lastNoFactureDS IsNot Nothing AndAlso lastNoFactureDS.Tables(0).Rows(0)(0) IsNot DBNull.Value Then lastNoFacture = lastNoFactureDS.Tables(0).Rows(0)(0)

            DBLinker.executeSQLScript("DBCC CHECKIDENT (FacturesNumbers, reseed, " & lastNoFacture & ")")


            'Ensure that first RV of physiotherapy folders is a physiotherapist
            DBLinker.executeSQLScript("update InfoVisites set NoTRP = (select top 1 nouser from Utilisateurs U inner join Titres TT on tt.NoTitre=u.NoTitre  where Titre like '%Physiothérapeute%' and DateFin is null) from InfoVisites, (select T.nofolder,notrp, novisite from InfoVisites IV, (select nofolder,min(dateheure) as dateheure from infovisites where nofolder in (select nofolder from InfoFolders where NoCodeUnique=1 and Service='Physiothérapie') group by NoFolder) AS T             where(t.NoFolder = IV.NoFolder And t.dateheure = iv.DateHeure) and NoTRP not in (select nouser from Utilisateurs U inner join Titres TT on tt.NoTitre=u.NoTitre  where Titre like '%Physiothérapeute%')) as D             where(d.NoVisite = InfoVisites.NoVisite)")

            Me.Text = "Présences (Restantes/ajoutées) " & i & ":" & n
            Application.DoEvents()

            If scripting.Checked Then IO.File.WriteAllText("C:\transfer-agendas" & Math.Ceiling((n / 1000)) & ".sql", sbSQL.ToString())

            erreurs.Text &= vbCrLf & "Ended RVs generation : " & Now.ToString & vbCrLf
            erreurs.Text &= "---------------------------------------" & vbCrLf & "Nombre de rendez-vous ajoutés : " & n & "  Nombre d'erreurs : " & nbErreurs & vbCrLf & vbCrLf & vbCrLf
        End If
    End Sub

    Private Function getRVDateTime(ByVal noTRP As Integer, ByVal rvDate As Date) As Date
        Dim d As String = rvDate.ToString("yyyy/MM/dd")

        If lastRVByDateByTRP.ContainsKey(noTRP) = False Then lastRVByDateByTRP.Add(noTRP, New Dictionary(Of String, Date))
        If lastRVByDateByTRP(noTRP).ContainsKey(d) = False Then
            rvDate = New Date(rvDate.Year, rvDate.Month, rvDate.Day, 8, 0, 0)
            lastRVByDateByTRP(noTRP).Add(d, rvDate)
        Else
            rvDate = lastRVByDateByTRP(noTRP)(d).AddMinutes(15)
            lastRVByDateByTRP(noTRP)(d) = rvDate
        End If

        Return rvDate
    End Function

    Private Sub transferOnePresence(ByVal curPresence As DataRow, ByRef n As Integer, ByVal i As Integer, ByVal htTRP As Dictionary(Of String, Integer))
        Try
            Dim noStatut As Integer = 0
            If presencesCodes.ContainsKey(curPresence("COD_PRES")) Then noStatut = presencesCodes(curPresence("COD_PRES"))
            If noStatut = 0 Then Exit Sub 'Quit not a real presence

            Dim curNoRef As String = curPresence("NO_DOSS")
            If curNoRef <= 0 Then Exit Sub 'Not a real presence

            Dim noTRP As Integer = getNoTRP(htTRP, htReports(curNoRef & curPresence("COD_TRAIT")))
            Dim rvDateTime As Date = getRVDateTime(noTRP, curPresence("DAT_PRES"))

            If htReports.ContainsKey(curNoRef & curPresence("COD_TRAIT")) = False Then
                erreurs.Text &= "=== ERROR WITH PRESENCE of NoRef #" & curNoRef & " at date " & rvDateTime.ToString("yyyy/MM/dd") & vbCrLf
                Exit Sub
            End If

            Dim noFolder As Integer = htDossiers(curNoRef & curPresence("COD_TRAIT"))
            Dim noClient As Integer = htClients(curNoRef)
            Dim service As String = "Physiothérapie"
            If curPresence("COD_TRAIT") = "E" Then service = "Ergothérapie"
            Dim isEval As Boolean = curPresence("COD_PRES") = "I"
            Dim isOnAgenda As Boolean = noStatut < 3
            Dim noFactureToWrite As String = "null"
            If noStatut = 4 Then
                noFacture += 1
                noFactureToWrite = noFacture
            End If


            'Write presence itself
            noVisite += 1
            If scripting.Checked Then
                sbSQL.Append("INSERT INTO InfoVisites (NoClient,NoFolder,NoStatut,NoFacture,NoTRP,DateHeure,Periode,Service,Confirmed,Evaluation,Flagged,IsOnAgenda,IsAnnounced,ExternalStatus) VALUES(").Append(noClient).Append(",").Append(noFolder).Append(",").Append(noStatut).Append(",").Append(noFactureToWrite).Append(",").Append(noTRP).Append(",'").Append(rvDateTime.Year).Append("/").Append(rvDateTime.Month).Append("/").Append(rvDateTime.Day).Append(" ").Append(rvDateTime.Hour).Append(":").Append(rvDateTime.Minute).Append("',15,'Physiothérapie',1,").Append(If(isEval, 1, 0)).Append(",0,").Append(If(isOnAgenda, 1, 0)).Append(",1,4").AppendLine(")")
            Else
                DBLinker.getInstance(True).writeDB("InfoVisites", "NoClient,NoFolder,NoStatut,NoFacture,NoTRP,DateHeure,Periode,Service,Confirmed,Evaluation,Flagged,IsOnAgenda,IsAnnounced,ExternalStatus", _
                noClient & "," & noFolder & "," & noStatut & "," & noFactureToWrite & "," & noTRP & ",'" & rvDateTime.Year & "/" & rvDateTime.Month & "/" & rvDateTime.Day & " " & rvDateTime.Hour & ":" & rvDateTime.Minute & "',15,'" & service & "',1," & If(isEval, 1, 0) & ",0," & If(isOnAgenda, "1", "0") & ",1,4")
            End If

            'Write stats of RV (Add + Change of status)
            Dim noCreateur As Integer = 0
            If scripting.Checked Then
                sbSQL.Append("INSERT INTO StatVisites (NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder) VALUES(").Append(noCreateur).Append(",'").Append(rvDateTime.Year).Append("/").Append(rvDateTime.Month).Append("/").Append(rvDateTime.Day).Append(" ").Append(rvDateTime.Hour).Append(":").Append(rvDateTime.Minute).Append("',6,").Append(noVisite).Append(",").Append(noClient).Append(",").Append(noFolder).AppendLine(")")
            Else
                DBLinker.getInstance(True).writeDB("StatVisites", "NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder", _
                                noCreateur & ",'" & rvDateTime.Year & "/" & rvDateTime.Month & "/" & rvDateTime.Day & " " & rvDateTime.Hour & ":" & rvDateTime.Minute & "',6," & noVisite & "," & noClient & "," & noFolder)
            End If
            If noStatut <> 3 Then
                If scripting.Checked Then
                    sbSQL.Append("INSERT INTO StatVisites (NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder) VALUES(").Append(noCreateur).Append(",'").Append(rvDateTime.Year).Append("/").Append(rvDateTime.Month).Append("/").Append(rvDateTime.Day).Append(" ").Append(rvDateTime.Hour).Append(":").Append(rvDateTime.Minute).Append("',").Append(noStatut).Append(",").Append(noVisite).Append(",").Append(noClient).Append(",").Append(noFolder).AppendLine(")")
                Else
                    DBLinker.getInstance(True).writeDB("StatVisites", "NoUser,DateHeureCreation,NoAction,NoVisite,NoClient,NoFolder", _
                                    noCreateur & ",'" & rvDateTime.Year & "/" & rvDateTime.Month & "/" & rvDateTime.Day & " " & rvDateTime.Hour & ":" & rvDateTime.Minute & "'," & noStatut & "," & noVisite & "," & noClient & "," & noFolder)
                End If
            End If

            If noStatut = 4 Then
                Dim rvPrice As Double = PresencePrices.getPrice(rvDateTime)
                'Write bill
                If scripting.Checked Then
                    sbSQL.Append("INSERT INTO StatFactures (NoAction,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,ParNoUser,NoKP,NoUserFacture,NoFactureTransfere,Commentaires,ParNoClinique,NoUser,DateHeureCreation,NoFacture,NoFolder,NoClient,MontantFacture,TypeFacture,NoVisite,DateFacture,ParNoKp,ParNoClient,NoEntitePayeur) VALUES(5,'',null,'',null,0,0,null,null,null,null,'',null,0,'").Append(rvDateTime.Year).Append("/").Append(rvDateTime.Month).Append("/").Append(rvDateTime.Day).Append(" ").Append(rvDateTime.Hour).Append(":").Append(rvDateTime.Minute).Append("',").Append(noFacture).Append(",").Append(noFolder).Append(",").Append(noClient).Append(",").Append(rvPrice.ToString.Replace(",", ".")).Append( _
                                ",'").Append(service).Append("',").Append(noVisite).Append(",'").Append(rvDateTime.Year).Append("/").Append(rvDateTime.Month).Append("/").Append(rvDateTime.Day).Append(" ").Append(rvDateTime.Hour).Append(":").Append(rvDateTime.Minute).Append("',1,").Append(noClient).AppendLine(",2)")
                Else
                    DBLinker.getInstance(True).writeDB("StatFactures", "NoAction,Description,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,ParNoUser,NoKP,NoUserFacture,NoFactureTransfere,Commentaires,ParNoClinique," & _
                                "NoUser,DateHeureCreation,NoFacture,NoFolder,NoClient,MontantFacture,TypeFacture,NoVisite,DateFacture,ParNoKp,ParNoClient,NoEntitePayeur,TaxesApplication", _
                                "5,'',null,'',null,0,0,null,null,null,null,'',null," & _
                                "0,'" & rvDateTime.ToString("yyyy/MM/dd HH:mm") & "'," & noFacture & "," & noFolder & "," & noClient & "," & rvPrice.ToString.Replace(",", ".") & _
                                ",'" & service & "'," & noVisite & ",'" & rvDateTime.Year & "/" & rvDateTime.Month & "/" & rvDateTime.Day & " " & rvDateTime.Hour & ":" & rvDateTime.Minute & "',1," & noClient & ",2,0")
                End If

                'Write payment
                If scripting.Checked Then
                    sbSQL.Append("INSERT INTO StatPaiements (Commentaires,NoAction,NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,NoEntitePayeur,ParNoClient,ParNoKP,NoVisite) VALUES('',12,0,'").Append(rvDateTime.Year).Append("/").Append(rvDateTime.Month).Append("/").Append(rvDateTime.Day).Append(" ").Append(rvDateTime.Hour).Append(":").Append(rvDateTime.Minute).Append("',").Append(noFacture).Append(",").Append(rvPrice.ToString.Replace(",", ".")).Append(",'").Append(rvDateTime.Year).Append("/").Append(rvDateTime.Month).Append("/").Append(rvDateTime.Day).Append(" ").Append(rvDateTime.Hour).Append(":").Append(rvDateTime.Minute).Append("','Argent',").Append(noClient).Append(",").Append(noFolder).Append(",2,").Append(noClient).Append(",1,").Append(noVisite).AppendLine(")")
                Else
                    DBLinker.getInstance(True).writeDB("StatPaiements", "Commentaires,NoAction," & _
                                    "NoUser, DateHeureCreation, NoFacture, MontantPaiement, DateTransaction, TypePaiement, NoClient,NoFolder,NoEntitePayeur,ParNoClient,ParNoKP,NoVisite", _
                                    "'',12," & _
                                    "0,'" & rvDateTime.Year & "/" & rvDateTime.Month & "/" & rvDateTime.Day & " " & rvDateTime.Hour & ":" & rvDateTime.Minute & "'," & noFacture & "," & rvPrice.ToString.Replace(",", ".") & ",'" & rvDateTime.Year & "/" & rvDateTime.Month & "/" & rvDateTime.Day & " " & rvDateTime.Hour & ":" & rvDateTime.Minute & "','Argent'," & noClient & "," & noFolder & ",2," & noClient & ",1," & noVisite)
                End If
            End If

            n += 1
        Catch ex As Exception
            ex = ex
            erreurs.Text &= "Rendez-vous de la table PRESENCE - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & vbCrLf
            If ex.InnerException IsNot Nothing Then erreurs.Text &= ex.InnerException.Message & vbCrLf
            erreurs.Text &= vbCrLf
            erreurs.SelectionStart = erreurs.Text.Length - 1
            nbErreurs += 1
        End Try

        If scripting.Checked AndAlso n Mod 1000 = 0 Then
            IO.File.WriteAllText("C:\temp\transfer-rvs" & (n / 1000) & ".sql", sbSQL.ToString())
            sbSQL = New System.Text.StringBuilder
            GC.Collect(5)
        End If
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim curLog As String = erreurs.Text
        IO.File.WriteAllText("C:\temp\transfer.log", curLog)
        End
    End Sub

    Private Sub TransferParts_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Client.Click, Clinique.Click, Users.Click, medecins.Click, RVs.Click
        activateUpdateNoPermis()
    End Sub

    Private Sub allParts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles allParts.Click
        Clinique.Checked = allParts.Checked
        Users.Checked = allParts.Checked
        medecins.Checked = allParts.Checked
        Client.Checked = allParts.Checked
        RVs.Checked = allParts.Checked
        updateTRPNoPermis.Checked = allParts.Checked

        activateUpdateNoPermis()
    End Sub

    Private Sub transferOneFolder(ByVal curFolder As DataRow, ByVal serviceCode As String, ByRef i As Integer, ByRef n2 As Integer, ByRef nbDossiers As Integer, ByVal siteLesionInconnu As Integer, ByVal htTRP As Dictionary(Of String, Integer), ByVal dsInfoClients As DataSet, ByVal dsInfoFolders As DataSet, ByVal dsKeyPeople As DataSet, ByVal dsRapport As DataSet, ByVal dsPresence As DataSet, ByVal dsDescription As DataSet)
        Dim curNoRef As String = curFolder("NO_DOSS").ToString
        If curNoRef <= 0 Then Exit Sub

        Dim reportsData() As DataRow
        If htReports.ContainsKey(curNoRef & serviceCode) = False Then
            reportsData = dsRapport.Tables(0).Select("NO_DOSS='" & curNoRef & "' AND COD_TRAIT = '" & serviceCode & "'", "NO_ETAP")

            If reportsData.Length = 0 Then
                erreurs.Text &= "---> NO REPORTS FOR FOLDER NoRef #" & curNoRef & vbCrLf
                Exit Sub
            Else
                htReports.Add(curNoRef & serviceCode, reportsData)
            End If
        Else
            reportsData = htReports(curNoRef & serviceCode)
        End If

        Dim isFolder() As DataRow = dsInfoFolders.Tables(0).Select("NoRef='" & curNoRef.Replace("'", "''") & "' AND Service LIKE '" & serviceCode & "%'")
        If isFolder.Length = 0 Then
            If htNAM.ContainsKey(curFolder("NAM").ToString) Then
                Try

                    'InfoFolder
                    Dim noClient As Integer = htNAM(curFolder("NAM").ToString)
                    Dim noCodification As Integer = 1

                    If htClients.ContainsKey(curNoRef) = False Then htClients.Add(curNoRef, noClient)

                    Dim presencesData() As DataRow = dsPresence.Tables(0).Select("NO_DOSS='" & curNoRef & "' AND COD_TRAIT = '" & serviceCode & "'", "DAT_PRES")

                    Dim textsData() As DataRow = dsDescription.Tables(0).Select("NO_DOSS='" & curNoRef & "' AND COD_TRAIT = '" & serviceCode & "'", "NO_ETAP")
                    Dim service As String = If(serviceCode = "P", "Physiothérapie", "Ergothérapie")

                    Dim noTRP As Integer = getNoTRP(htTRP, reportsData)
                    Dim duree As Integer = 0
                    If presencesData.Length <> 0 Then
                        duree = Math.Ceiling(CDate(presencesData(presencesData.Length - 1)("DAT_PRES")).Subtract(CDate(presencesData(0)("DAT_PRES"))).TotalDays / 5)
                        If duree > 74 Then duree = 74
                    End If
                    Dim frequence As Integer = 1
                    Dim strFreq As String = reportsData(0)("FREQ_TRAIT").ToString
                    If strFreq <> "" AndAlso Integer.TryParse(strFreq.Substring(0, 1), 0) Then frequence = strFreq.Substring(0, 1)
                    If frequence < 1 Then frequence = 1
                    frequence -= 1 'Adjust 

                    Dim noMedecin As Integer = 0
                    Dim myMedecin() As DataRow = dsKeyPeople.Tables(0).Select("NoRef='" & reportsData(0)("NO_MEDE").ToString.Trim.Replace("'", "''") & "'")
                    If myMedecin IsNot Nothing AndAlso myMedecin.Length = 1 Then noMedecin = myMedecin(0)("NoKP")

                    Dim dateRecRef As String = "null"
                    If textsData.Length <> 0 Then dateRecRef = "'" & textsData(0)("DAT_RECEP_ORD") & "'"

                    nbDossiers += 1

                    Dim diagnostic As String = firstLetterCapital(reportsData(0)("DIAGN").ToString.ToLower().Trim(), False)
                    Dim dateRechute As String = reportsData(0)("DAT_RECHU").ToString.Trim()
                    If dateRechute = "" Then
                        dateRechute = "null"
                    Else
                        dateRechute = "'" & dateRechute & "'"
                    End If

                    Dim folderOpeningDateFromCreation As String = curFolder("DAT_CREA").ToString()
                    Dim folderOpeningDateFromPresences As String = String.Empty
                    If presencesData.Length <> 0 Then folderOpeningDateFromPresences = getFirstDate(presencesData, True)

                    'TODO : Take minimal date between creation, presences and (missing and if field exists in MDB DB) text dates

                    Dim isFolderClosed As Boolean = reportsData(reportsData.Length - 1)("NO_ETAP") = 999
                    Dim externalStatus As Integer = If(textsData.Length = 0 AndAlso CDate(folderOpeningDate).AddDays(15) < Date.Now, 2, 4)
                    DBLinker.getInstance(True).writeDB("InfoFolders", "ExternalStatus,NoRef,Remarques,NbVisiteHavingCAR,Flagged,NoCodeUser,NoCodeDate,DateRef,DateReceptionRef,NoSiteLesion,Frequence,StatutOuvert, NoClient,NoCodeUnique,DateRechute,DateAccident,Diagnostic,Duree,NoKP,NoTRPTraitant,Service", _
                    externalStatus & ",'" & curNoRef & "','Importé depuis le logiciel PhyioErgo de la CSST. À vérifier !',0,0,null,'" & folderOpeningDate & "','" & reportsData(0)("DAT_ORD") & "'," & dateRecRef & "," & siteLesionInconnu & "," & frequence & "," & If(isFolderClosed, "0", "1") & "," & noClient & "," & noCodification & "," & dateRechute & ",'" & curFolder("DAT_EVEN").ToString() & "','" & diagnostic.Replace("'", "''") & "'," & duree & "," & noMedecin & "," & noTRP & ",'" & service & "'")

                    Dim noFolder As Integer = DBLinker.getInstance(True).readDBForGrid("InfoFolders", "NoFolder", "WHERE NoRef='" & curNoRef & "' AND Service LIKE '" & serviceCode & "%'").Tables(0).Rows(0)(0)
                    DBLinker.getInstance(True).writeDB("StatFolders", "DateHeureCreation,NoAction,NoFolder,NoClient,NoUser", "'" & folderOpeningDate & "',13," & noFolder & "," & noClient & ",0")
                    If presencesData.Length <> 0 AndAlso isFolderClosed Then DBLinker.getInstance(True).writeDB("StatFolders", "DateHeureCreation,NoAction,NoFolder,NoClient,NoUser", "'" & presencesData(presencesData.Length - 1)("DAT_PRES").ToString & "',15," & noFolder & "," & noClient & "," & noTRP)

                    'FolderTextes
                    Dim eabp As String = ""

                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "1," & noFolder & ",'Historique,Évaluation,Analyse,But,Plan','" & affTextDate(folderOpeningDate) & "','" & eabp.Replace("'", "''") & "',-1,1," & If(eabp = String.Empty, 0, 1) & ",1")
                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "2," & noFolder & ",'Notes','" & affTextDate(folderOpeningDate) & "','',-1,1,0,1")
                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "3," & noFolder & ",'Rapport au médecin','" & affTextDate(folderOpeningDate) & "','',-1,1,0,1")
                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "7," & noFolder & ",'Archive des rapports au médecin','" & affTextDate(folderOpeningDate) & "','',-1,1,0,1")

                    Dim expiryDate As Date = CDate(folderOpeningDate)
                    Dim alarmDate As Date = expiryDate.AddMinutes(2)
                    Dim clientName() As DataRow = dsInfoClients.Tables(0).Select("NoClient=" & noClient)
                    Dim lastRapport As Char = "I"c
                    Dim dateEnded As String = "null"
                    'Rapport CSST initial
                    Dim initText As String = String.Empty
                    If textsData.Length <> 0 Then
                        dateEnded = "'" & affTextDate(expiryDate) & "'"
                        lastRapport = " "c
                        For Each curRow As DataRow In textsData
                            If curRow("NO_ETAP") = 0 Then initText &= "<b>" & curRow("MOT_CLE") & " :</b><br>" & curRow("DESC") & "<br><br>"
                        Next
                    Else
                        If expiryDate.AddDays(15) < Date.Now Then
                            dateEnded = "'" & expiryDate.AddDays(15) & "'"
                            lastRapport = " "c
                        Else
                            dateEnded = "null"
                            initText = initTemplate
                        End If
                    End If
                    DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "4," & noFolder & ",'Rapport CSST initial','" & affTextDate(expiryDate) & "'," & dateEnded & ",'" & initText.Replace("'", "''") & "',-1,1," & If(initText = String.Empty OrElse initText = initTemplate, 0, 1) & "," & If(lastRapport = "I", 2, 4))

                    If presencesData.Length = 0 Then
                        erreurs.Text &= "---> LOOK FOR FOLDER #" & noFolder & " : no presence"
                        Exit Sub
                    End If

                    'Rapport CSST d'étape
                    'Dim steps() As DataRow = dsDescription.Tables(0).Select("NO_DOSS = '" & curNoRef & "' AND NO_ETAP <> 0  AND NO_ETAP <> 999")
                    'If steps.Length <> 0 Then 'Si plus que le rapport initial
                    Dim firstTreatmentDate As Date = getFirstDate(presencesData, False)
                    Dim maxNbStep As Integer = 0
                    If firstTreatmentDate.Equals(PresencePrice.LIMIT_DATE) = False Then
                        For Each curStep As DataRow In textsData
                            Dim noStep As Integer = curStep("NO_ETAP")
                            If noStep = 0 OrElse noStep = 999 Then Continue For

                            'Finally, to correct some missing text, let's take them sequencially
                            'maxNbStep = Math.Max(maxNbStep, noStep)
                            maxNbStep += 1
                            noStep = maxNbStep

                            expiryDate = firstTreatmentDate.AddDays(21 * noStep - 1)
                            dateEnded = "'" & affTextDate(expiryDate) & "'"
                            lastRapport = " "c

                            DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "5," & noFolder & ",'Rapport CSST d''étape " & noStep & "','" & affTextDate(expiryDate) & "'," & dateEnded & ",'" & curStep("DESC").ToString.Replace("'", "''") & "',-1," & noStep & "," & If(curStep("DESC").ToString.Trim = String.Empty, 0, 1) & ",4")
                        Next

                        If Not isFolderClosed Then
                            expiryDate = expiryDate.AddDays(21)
                            dateEnded = "null"
                            lastRapport = "E"c
                            maxNbStep += 1
                            DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "5," & noFolder & ",'Rapport CSST d''étape " & maxNbStep & "','" & affTextDate(expiryDate) & "'," & dateEnded & ",'" & stepTemplate.Replace("'", "''") & "',-1," & maxNbStep & ",0,2")
                        End If
                    Else
                        Dim noTreatment As Boolean = True
                    End If

                    'Rapport CSST final
                    If isFolderClosed Then
                        expiryDate = CDate(presencesData(presencesData.Length - 1)("DAT_PRES").ToString)
                        dateEnded = "'" & affTextDate(expiryDate) & "'"
                        lastRapport = " "c
                        Dim finalText As String = textsData(textsData.Length - 1)("DESC").ToString.Trim()

                        DBLinker.getInstance(True).writeDB("FolderTextes", "NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,Texte,TextePos,NoMultiple,IsTexte,ExternalStatus", "6," & noFolder & ",'Rapport CSST final','" & affTextDate(expiryDate) & "'," & dateEnded & ",'" & finalText.Replace("'", "''") & "',-1,1," & If(finalText = String.Empty, 0, 1) & ",4")
                    End If

                    Dim alertAdded As Boolean = False
                    expiryDate = CDate(folderOpeningDate)
                    Select Case lastRapport
                        Case "I"
                            expiryDate = expiryDate.AddDays(7)
                            REM If ExpiryDate.CompareTo(CDate(Me.dataDate.Text)) >= 0 Then 
                            DBLinker.getInstance(True).writeDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", noTRP & ",'OpenClientAccount','" & noFolder & "',null,'" & affTextDate(expiryDate.AddDays(1)) & "','" & affTextDate(alarmDate) & ":00:00:" & noClient & ":" & noFolder & ":True',1,1,'Le rapport CSST initial pour le client " & clientName(0)("Nom").ToString.Replace("'", "''") & "," & clientName(0)("Prenom").ToString.Replace("'", "''") & " du dossier #" & noFolder & " est dû le " & affTextDate(expiryDate) & "'") : alertAdded = True

                            'Final alerts is never added because when it's in data it's considered as DONE
                            'Case "F"
                            '    expiryDate = CDate(curFolder("FINTRAIT").ToString)
                            '    alarmDate = expiryDate.AddMinutes(2)
                            '    expiryDate = expiryDate.AddDays(15)
                            '    DBLinker.getInstance(True).writeDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", noTRP & ",'OpenClientAccount','" & noFolder & "',null,'" & affTextDate(expiryDate.AddDays(1)) & "','" & affTextDate(alarmDate) & ":00:00:" & noClient & ":" & noFolder & ":False',1,1,'Le rapport CSST final pour le client " & clientName(0)("Nom").ToString.Replace("'", "''") & "," & clientName(0)("Prenom").ToString.Replace("'", "''") & " du dossier #" & noFolder & " est dû le " & affTextDate(expiryDate) & "'") : alertAdded = True
                        Case "E"
                            expiryDate = getFirstDate(presencesData, False)
                            If expiryDate.Equals(PresencePrice.LIMIT_DATE) = False Then
                                expiryDate = expiryDate.AddDays(21 * maxNbStep - 1)
                                alarmDate = expiryDate.AddDays(-7)
                                expiryDate = expiryDate.AddDays(7)
                                DBLinker.getInstance(True).writeDB("UsersAlerts", "NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message", noTRP & ",'OpenClientAccount','" & noFolder & "',null,'" & affTextDate(expiryDate.AddDays(1)) & "','" & affTextDate(alarmDate) & ":00:00:" & noClient & ":" & noFolder & ":True',1,1,'Le rapport CSST d''étape #" & maxNbStep & " pour le client " & clientName(0)("Nom").ToString.Replace("'", "''") & "," & clientName(0)("Prenom").ToString.Replace("'", "''") & " du dossier #" & noFolder & " est dû le " & affTextDate(expiryDate) & "'") : alertAdded = True
                            End If
                        Case Else
                    End Select

                    If alertAdded Then
                        DBLinker.executeSQLScript("INSERT INTO FolderTexteAlerts(NoFolderTexte,NoUserAlert) VALUES(IDENT_CURRENT('FolderTextes'),IDENT_CURRENT('UsersAlerts'));")
                    End If


                    htDossiers.Add(curNoRef & serviceCode, noFolder)
                    n2 += 1
                Catch ex As Exception
                    erreurs.Text &= "Dossier de la table DOSClien - entrée no:" & i.ToString & " - entrée valeurs : " & ex.Message & If(ex.InnerException Is Nothing, String.Empty, vbCrLf & ex.InnerException.Message) & vbCrLf & vbCrLf
                    erreurs.SelectionStart = erreurs.Text.Length - 1
                    nbErreurs += 1
                End Try
            End If
        Else
            htDossiers.Add(curNoRef & serviceCode, isFolder(0)(0))
            Dim noClient As Integer = htNAM(curFolder("NAM").ToString)
            If htClients.ContainsKey(curNoRef) = False Then htClients.Add(curNoRef, noClient)
        End If
    End Sub
End Class
