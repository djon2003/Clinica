Public Class User
    Inherits DBItemableBase
    Implements IListable

    Private _noUser, _noType, _noEmployeeType As Integer
    Private _city As String = "", _title As String = "", _passwordKey As String = "", _password As String = "", _lastName As String = "", _firstName As String = "", _url As String = "", _address As String = "", _postalCode As String = "", _telephones As String = "", _services As String = "", _rights As String = "", _email As String = "", _noPermit As String = ""
    Private _isTherapist, _notConfirmRVOnPasteOfDTRP, _isMessagesTransferedToEmail As Boolean
    Private _startingDate As Date = LIMIT_DATE
    Private _endingDate As Date = LIMIT_DATE
    Private oldEndingDate As Date = LIMIT_DATE
    Private _settings As New UserSettings
    Private myConn As UserConnection
    Private Shared namCharToNumber As Generic.Dictionary(Of String, Integer)


    Private Shared Sub loadNAMCharToNumber()
        If namCharToNumber IsNot Nothing Then Exit Sub

        namCharToNumber = New Generic.Dictionary(Of String, Integer)
        namCharToNumber.Add("A", 193)
        namCharToNumber.Add("B", 194)
        namCharToNumber.Add("C", 195)
        namCharToNumber.Add("D", 196)
        namCharToNumber.Add("E", 197)
        namCharToNumber.Add("F", 198)
        namCharToNumber.Add("G", 199)
        namCharToNumber.Add("H", 200)
        namCharToNumber.Add("I", 201)
        namCharToNumber.Add("J", 209)
        namCharToNumber.Add("K", 210)
        namCharToNumber.Add("L", 211)
        namCharToNumber.Add("M", 212)
        namCharToNumber.Add("N", 213)
        namCharToNumber.Add("O", 214)
        namCharToNumber.Add("P", 215)
        namCharToNumber.Add("Q", 216)
        namCharToNumber.Add("R", 217)
        namCharToNumber.Add("S", 226)
        namCharToNumber.Add("T", 227)
        namCharToNumber.Add("U", 228)
        namCharToNumber.Add("V", 229)
        namCharToNumber.Add("W", 230)
        namCharToNumber.Add("X", 231)
        namCharToNumber.Add("Y", 232)
        namCharToNumber.Add("Z", 233)
        namCharToNumber.Add("0", 240)
        namCharToNumber.Add("1", 241)
        namCharToNumber.Add("2", 242)
        namCharToNumber.Add("3", 243)
        namCharToNumber.Add("4", 244)
        namCharToNumber.Add("5", 245)
        namCharToNumber.Add("6", 246)
        namCharToNumber.Add("7", 247)
        namCharToNumber.Add("8", 248)
        namCharToNumber.Add("9", 249)
    End Sub

#Region "Properties"

    Public Property settings() As UserSettings
        Get
            Return _settings
        End Get
        Set(ByVal value As UserSettings)
            _settings = value
        End Set
    End Property

    Public Property startingDate() As Date
        Get
            If _startingDate.Equals(LIMIT_DATE) Then Return New Date(2000, 1, 1)

            Return _startingDate
        End Get
        Set(ByVal value As Date)
            _startingDate = value
        End Set
    End Property

    Public Property endingDate() As Date
        Get
            Return _endingDate
        End Get
        Set(ByVal value As Date)
            _endingDate = value
        End Set
    End Property

    Public ReadOnly Property noUser() As Integer
        Get
            Return _noUser
        End Get
    End Property

    Public Property passwordKey() As String
        Get
            Return _passwordKey
        End Get
        Set(ByVal value As String)
            _passwordKey = value
        End Set
    End Property

    Public Property password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Public Property lastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
        End Set
    End Property

    Public Property firstName() As String
        Get
            Return _firstName
        End Get
        Set(ByVal value As String)
            _firstName = value
        End Set
    End Property

    Public Property url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Public Property address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property

    Public Property city() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property

    Public Property postalCode() As String
        Get
            Return _postalCode
        End Get
        Set(ByVal value As String)
            _postalCode = value
        End Set
    End Property

    Public Property telephones() As String
        Get
            Return _telephones
        End Get
        Set(ByVal value As String)
            _telephones = value
        End Set
    End Property

    Public Property services() As String
        Get
            Return _services
        End Get
        Set(ByVal value As String)
            _services = value
        End Set
    End Property

    Public Property rights() As String
        Get
            Return _rights
        End Get
        Set(ByVal value As String)
            _rights = value
        End Set
    End Property

    Public Property title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Public Property noPermit() As String
        Get
            Return _noPermit
        End Get
        Set(ByVal value As String)
            _noPermit = value
        End Set
    End Property

    Public Property noType() As Integer
        Get
            Return _noType
        End Get
        Set(ByVal value As Integer)
            _noType = value
        End Set
    End Property

    Public Property isTherapist() As Boolean
        Get
            Return _isTherapist
        End Get
        Set(ByVal value As Boolean)
            _isTherapist = value
        End Set
    End Property

    Public Property notConfirmRVOnPasteOfDTRP() As Boolean
        Get
            Return _notConfirmRVOnPasteOfDTRP
        End Get
        Set(ByVal value As Boolean)
            _notConfirmRVOnPasteOfDTRP = value
        End Set
    End Property

    Public Property noEmployeeType() As Integer
        Get
            Return _noEmployeeType
        End Get
        Set(ByVal value As Integer)
            _noEmployeeType = value
        End Set
    End Property


#End Region

    'TODO : This is not in the appropriate class. Should be Client one !!
    Public Shared Function validateNAM(ByVal nam As String) As Boolean
        If System.Text.RegularExpressions.Regex.IsMatch(nam, "[a-zA-Z]{4}[0-9]{6}[abcdefghjklmnpqrstuvwyzABCDEFGHJKLMNPQRSTUVWYZ1-9][0-9]", System.Text.RegularExpressions.RegexOptions.IgnoreCase) = False Then Return False

        Dim month As Integer = nam.Substring(6, 2) Mod 50
        If month > 12 OrElse month < 1 Then Return False

        Dim day As Integer = nam.Substring(8, 2) Mod 50
        If day < 1 OrElse day > Date.DaysInMonth(2004, month) Then Return False 'The 2004 year is taken because its a leap year. Not taking real year, because only two numbers, so tiny possibility of error


        'Validate last number of NAM
        loadNAMCharToNumber()

        Dim validator As Integer = nam.Substring(11, 1)
        'TODO : Implement validator checking --> Not working for now

        Dim multiplicators() As Integer = {137, 9, 17, 13, 4, 57, 69, 1}
        'Dim multiplicators() As Integer = {9, 17, 13, 4, 57, 69, 1, 1}

        Dim strNom As String = nam.Substring(0, 3).ToUpper()
        strNom = namCharToNumber(nam.Chars(0)) & namCharToNumber(nam.Chars(1)) & namCharToNumber(nam.Chars(2))
        Dim nbNom As Double = Double.Parse(strNom) * multiplicators(0)
        Dim strPrenom As String = nam.Substring(3, 1).ToUpper()
        Dim nbPrenom As Double = namCharToNumber(strPrenom) * multiplicators(1)
        Dim strAnnee As String = nam.Substring(4, 2)
        Dim strCC As String = "19" & strAnnee
        'If Integer.Parse(strAnnee) > Integer.Parse(Date.Today.Year.ToString().Substring(2, 2)) Then nbCC = 18
        If Integer.Parse(strAnnee) < 39 Then strCC = "20" & strAnnee

        strCC = "1919"
        strCC = "2019"
        strCC = namCharToNumber(strCC.Chars(0)) & namCharToNumber(strCC.Chars(1)) & namCharToNumber(strCC.Chars(2)) & namCharToNumber(strCC.Chars(3))
        'Dim strCC2 As String = namCharToNumber("1") & namCharToNumber("9") & namCharToNumber("1") & namCharToNumber("8")
        Dim strCC2 As String = namCharToNumber("2") & namCharToNumber("0") & namCharToNumber("1") & namCharToNumber("8")
        Dim strCC3 As String = namCharToNumber("2") & namCharToNumber("0") & namCharToNumber("8") & namCharToNumber("3")
        Dim nbCC As Double = Double.Parse(strCC) * multiplicators(2)
        strAnnee = namCharToNumber(strAnnee.Chars(0)) & namCharToNumber(strAnnee.Chars(1))
        Dim nbAnnee As Double = Double.Parse(strAnnee) * multiplicators(3)

        Dim strS As String = "H"
        If nam.Substring(6, 2) > 50 Then strS = "F"
        Dim nbS As Double = namCharToNumber(strS) * multiplicators(4)

        Dim strMonth As String = If(month < 10, "0", String.Empty) & month
        strMonth = namCharToNumber(strMonth.Chars(0)) & namCharToNumber(strMonth.Chars(1))
        Dim nbMonth As Double = Double.Parse(strMonth) * multiplicators(5)

        Dim strDay As String = If(day < 10, "0", String.Empty) & day
        strDay = namCharToNumber(strDay.Chars(0)) & namCharToNumber(strDay.Chars(1))
        Dim nbDay As Double = Double.Parse(strDay) * multiplicators(6)

        Dim nbX As Double = namCharToNumber(nam.Substring(10, 1).ToUpper())
        'Dim nbX As Double = nam.Substring(10, 1)

        Dim sumProducts As Double = nbNom + nbPrenom + nbCC + nbAnnee + nbS + nbMonth + nbDay + nbX


        Return True
    End Function

    Public Sub connect(ByVal password As String)

        If PreferencesManager.getGeneralPreferences()("UserMDPRespectCasse") = False Then password = password.ToUpper
        Dim curMDP As String = mdpProcessToModif(passwordKey, password)
        If Not curMDP.Replace(",", ".") = password.Replace(",", ".") Then Throw New UserConnectionException()

        Dim daLine As String = rights
        If daLine.StartsWith("3") Then daLine = daLine.Substring(1)
        currentDroitAcces = splitStr(daLine, 1)

        myConn = ConnectionsManager.getInstance.createConnection(noUser)
    End Sub

    Public Sub disconnect()
        myConn.delete()
    End Sub

    Public Shared Function extractNo(ByVal userString As String) As Integer
        If userString Is Nothing OrElse userString = "" Then Return 0
        Dim parenthesisPosition As Integer = userString.LastIndexOf("(")
        If parenthesisPosition = -1 Then Return 0

        userString = userString.Substring(parenthesisPosition + 1)
        userString = userString.Substring(0, userString.Length - 1)

        Return userString
    End Function


    Public Function getUserType() As UserType
        Return UserTypeManager.getInstance().getUserType(_noType)
    End Function

    Public Function isOfferingService(ByVal service As String) As Boolean
        If (services = service Or services.StartsWith(service & "§") Or services.EndsWith("§" & service) Or services.IndexOf("§" & service & "§") <> -1) Then Return True
        Return False
    End Function

    Public Sub changePassword()
        Dim myInputBoxPlus As New InputBoxPlus()
        myInputBoxPlus.maxLength = 10
        myInputBoxPlus.passwordChar = "*"c

        Dim newMDP As String = myInputBoxPlus("Veuillez entrer votre nouveau mot de passe", "Nouveau mot de passe")
        If newMDP = "" Then Exit Sub
        Dim confirmedMDP As String = myInputBoxPlus("Veuillez confirmer votre nouveau mot de passe", "Nouveau mot de passe")
        If confirmedMDP = "" Then Exit Sub

        If newMDP <> confirmedMDP Then
            MessageBox.Show("Veuillez vous assurer d'inscrire le même mot de passe deux fois", "Nouveau mot de passe")
            Exit Sub
        End If

        Dim cle As String = DateFormat.getTextDate(Date.Today) & Date.Now.ToString("HH:mm:ss")
        newMDP = mdpProcessToModif(cle, newMDP)
        Me.password = newMDP
        Me.passwordKey = cle
        Me.saveData()
    End Sub

    Public Function getFullName() As String
        Return lastName & "," & firstName
    End Function

    Public Overrides Function toString() As String
        Return lastName & "," & firstName & " (" & _noUser & ")"
    End Function

    Public Function getText() As String Implements IListable.getText
        Return toString()
    End Function

    Public Function getValue() As Object Implements IListable.getValue
        Return _noUser
    End Function

    Public Overrides Sub delete()
        'Droit & Accès
        If currentDroitAcces(46) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de gérer les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        If hddListing(appPath & bar(appPath) & "Users\Connected", False, True).Count > 1 Or myMainWin.formOuvertes.items.Count > 1 Then
            MessageBox.Show("Vous devez avoir uniquement cette fenêtre d'ouverte et vous devez être le seul utilisateur de connecter pour supprimer des utilisateurs.")
            Exit Sub
        End If

        If MessageBox.Show("Êtes-vous certain de vouloir supprimer cet utilisateur ?", "Supprimer un utilisateur", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim isUsed(,) As String = DBLinker.getInstance.readDB("SELECT TOP 1 NoFacture FROM Factures WHERE Factures.NoUserFacture=" & noUser & " OR Factures.ParNoUser=" & noUser & " UNION SELECT TOP 1 NoFacture FROM StatFactures WHERE StatFactures.NoUserFacture=" & noUser & " OR StatFactures.ParNoUser=" & noUser & " UNION SELECT TOP 1 NoFacture FROM StatPaiements WHERE StatPaiements.ParNoUser=" & noUser)
            Dim wontDelMsg As String = "Impossible de supprimer cet utilisateur, car il est présentement associé à d'autres éléments du logiciel"
            If isUsed IsNot Nothing AndAlso isUsed.Length <> 0 Then
                MessageBox.Show(wontDelMsg, "Suppression impossible")
                Exit Sub
            End If

            DBLinker.getInstance.beginTransaction()
            'Supprime les tables où la suppression auto était impossible
            'REM Maybe triggers could have been used ? --> Nope, because DBFolders delete external files
            'REM The conditions before the deletes should be useless, though a bug happened... So shall investigate why a user wouldn't have it's personal folder (Change in name !!?)
            Dim curUserDBFolder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder("Utilisateurs\" & Me.toString)
            If curUserDBFolder IsNot Nothing Then curUserDBFolder.delete()
            Dim curUserContactFolder As ContactFolder = ContactsManager.getInstance.getContactFolder("Utilisateurs\" & Me.toString)
            If curUserContactFolder IsNot Nothing Then curUserContactFolder.delete()
            Dim curUserMailFolder As MailFolder = MailsManager.getInstance.getMailFolder("Utilisateurs\" & Me.toString)
            If curUserMailFolder IsNot Nothing Then curUserMailFolder.delete()
            'REM_CODES
            DBLinker.executeSQLScript("DELETE FROM CodificationsDossiers WHERE NoUser=" & noUser & ";DELETE FROM Horaires WHERE NoUser=" & noUser & ";DELETE FROM Modeles WHERE NoUser=" & noUser & ";" & "DELETE FROM DBItemsDroitsAcces WHERE NoUser=" & noUser & ";DELETE FROM FileTypesDroitsAcces WHERE NoUser=" & noUser & ";")

            If DBLinker.getInstance.delDB("Utilisateurs", "NoUser", noUser, False, , False) = False Then
                DBLinker.getInstance.rollbackTransaction()
                MessageBox.Show(wontDelMsg, "Suppression impossible")
            Else
                DBLinker.getInstance.commitTransaction()
                'Supprime les dossiers de l'utilisateur
                Dim dirsForUsers() As String = IO.Directory.GetDirectories(appPath & bar(appPath) & "Users")
                For i As Integer = 0 To dirsForUsers.GetUpperBound(0)
                    If IO.Directory.Exists(appPath & bar(appPath) & "Users\" & dirsForUsers(i) & "\" & noUser) Then deltree(appPath & bar(appPath) & "Users\" & dirsForUsers(i) & "\" & noUser)
                    If IO.File.Exists(appPath & bar(appPath) & "Users\" & dirsForUsers(i) & "\" & noUser) Then IO.File.Delete(appPath & bar(appPath) & "Users\" & dirsForUsers(i) & "\" & noUser)
                Next i
                'Recharge la liste et envoi l'update
                onDeleted()
                If autoSendUpdateOnDelete Then UsersManager.getInstance.update()
            End If
        End If
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _noUser = curData("NoUser")
        If curData Is Nothing OrElse curData("Cle") IsNot DBNull.Value Then _passwordKey = curData("cle")
        If curData Is Nothing OrElse curData("noType") IsNot DBNull.Value Then _noType = curData("noType")
        If curData Is Nothing OrElse curData("ville") IsNot DBNull.Value Then _city = curData("ville")
        If curData Is Nothing OrElse curData("titre") IsNot DBNull.Value Then _title = curData("titre")
        If curData Is Nothing OrElse curData("mdp") IsNot DBNull.Value Then _password = curData("mdp")
        If curData Is Nothing OrElse curData("nom") IsNot DBNull.Value Then _lastName = curData("nom")
        If curData Is Nothing OrElse curData("prenom") IsNot DBNull.Value Then _firstName = curData("prenom")
        If curData Is Nothing OrElse curData("url") IsNot DBNull.Value Then _url = curData("url")
        If curData Is Nothing OrElse curData("adresse") IsNot DBNull.Value Then _address = curData("adresse")
        If curData Is Nothing OrElse curData("codepostal") IsNot DBNull.Value Then _postalCode = curData("codepostal")
        If curData Is Nothing OrElse curData("telephones") IsNot DBNull.Value Then _telephones = curData("telephones")
        If curData Is Nothing OrElse curData("services") IsNot DBNull.Value Then _services = curData("services")
        If curData Is Nothing OrElse curData("droitAcces") IsNot DBNull.Value Then _rights = curData("droitAcces")
        If curData Is Nothing OrElse curData("courriel") IsNot DBNull.Value Then _email = curData("courriel")
        If curData Is Nothing OrElse curData("noPermis") IsNot DBNull.Value Then _noPermit = curData("noPermis")
        If curData Is Nothing OrElse curData("dateDebut") IsNot DBNull.Value Then _startingDate = curData("dateDebut")
        If curData Is Nothing OrElse curData("dateFin") IsNot DBNull.Value Then
            _endingDate = curData("dateFin")
            oldEndingDate = _endingDate
        End If
        If curData Is Nothing OrElse curData("isTherapist") IsNot DBNull.Value Then _isTherapist = curData("isTherapist")
        If curData Is Nothing OrElse curData("notConfirmRVOnPasteOfDTRP") IsNot DBNull.Value Then _notConfirmRVOnPasteOfDTRP = curData("notConfirmRVOnPasteOfDTRP")
        If curData Is Nothing OrElse curData("noTypeEmploye") IsNot DBNull.Value Then _noEmployeeType = curData("noTypeEmploye")
    End Sub

    Public Overrides Sub saveData()
        Dim startDate As String = IIf(Me.startingDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.startingDate) & "'")
        Dim endDate As String = IIf(Me.endingDate.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(Me.endingDate) & "'")

        If Me.noUser <> 0 Then
            If oldEndingDate.Date.Equals(_endingDate.Date) = False AndAlso endingDate.Equals(LIMIT_DATE) = False Then
                'Set the last date of the user specific folder codifications to the user's end date if FolderCodes aren't already in modification
                If lockSecteur("CodificationsDossiers.lock", True, "Codifications dossiers", False) = False Then
                    Throw New UserAlreadyUsingException("Les codifications dossiers sont en cours de modification et elles ne doivent pas l'être pour affecter une date de fin à un utilisateur.")
                Else
                    oldEndingDate = _endingDate
                    DBLinker.getInstance.updateDB("CodificationsDossiers", "LastEffectiveTime = " & endDate, "NoUser", _noUser & " AND LastEffectiveTime IS NULL", False)
                    Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.update()
                    lockSecteur("CodificationsDossiers.lock", False)
                End If
            End If

            DBLinker.getInstance.updateDB("Utilisateurs", "Utilisateurs.Cle = '" & passwordKey & "', Utilisateurs.MDP = '" & password & "', Utilisateurs.Nom = '" & lastName.Replace("'", "''") & "', Utilisateurs.Prenom = '" & firstName.Replace("'", "''") & "', Utilisateurs.NoType = " & IIf(noType = 0, "null", noType) & ", Utilisateurs.URL = '" & url.Replace("'", "''") & "', Utilisateurs.Adresse = '" & address.Replace("'", "''") & "', Utilisateurs.NoVille = " & DBHelper.addItemToADBList("Villes", "NomVille", city, "NoVille") & ", Utilisateurs.CodePostal = '" & postalCode & "', Utilisateurs.Telephones = '" & telephones.Replace("'", "''") & "', Utilisateurs.Courriel = '" & email.Replace("'", "''") & "', Utilisateurs.NoTitre = " & DBHelper.addItemToADBList("Titres", "Titre", title, "NoTitre") & ", Utilisateurs.NoPermis = '" & noPermit.Replace("'", "''") & "', Utilisateurs.DateDebut = " & startDate & ", Utilisateurs.DateFin = " & endDate & ", Utilisateurs.NoTypeEmploye = " & noEmployeeType & ", Utilisateurs.Services = '" & services.Replace("'", "''") & "', Utilisateurs.IsTherapist = '" & isTherapist & "', Utilisateurs.DroitAcces = '" & rights & "',NotConfirmRVOnPasteOfDTRP='" & notConfirmRVOnPasteOfDTRP & "'", "NoUser", _noUser, False)
            myMainWin.StatusText = "Utilisateur " & Me.toString() & " enregistré"
        Else
            DBLinker.getInstance.writeDB("Utilisateurs", "Cle, MDP, Nom, Prenom, NoType, URL, Adresse, NoVille, Telephones, CodePostal, Courriel, NoTitre, NoPermis, DateDebut, DateFin, NoTypeEmploye, Services, IsTherapist, DroitAcces,NotConfirmRVOnPasteOfDTRP", "'" & passwordKey & "','" & password & "','" & lastName.Replace("'", "''") & "','" & firstName.Replace("'", "''") & "'," & IIf(noType = 0, "null", noType) & ",'" & url.Replace("'", "''") & "','" & address.Replace("'", "''") & "'," & DBHelper.addItemToADBList("Villes", "NomVille", city, "NoVille") & ",'" & telephones.Replace("'", "''") & "','" & postalCode & "','" & email.Replace("'", "''") & "'," & DBHelper.addItemToADBList("Titres", "Titre", title, "NoTitre") & ",'" & noPermit.Replace("'", "''") & "'," & startDate & "," & endDate & "," & noEmployeeType & ",'" & services.Replace("'", "''") & "','" & isTherapist & "','" & rights & "','" & notConfirmRVOnPasteOfDTRP & "'", , , , _noUser)
            settings.noUser = _noUser
            DBLinker.executeSQLScript("INSERT SoftwareNewsUsers (NoSoftwareNews,NoUser,Viewed) SELECT NoSoftwareNews, NoUser,1 FROM SoftwareNews, Utilisateurs WHERE NoUser=" & _noUser)

            PreferencesManager.getInstance.update() 'Update preferences (A trigger in SQL added a row)
            SchedulesManager.getInstance.update() 'Update schedules (A trigger in SQL added a row)

            onDataChanged()

            myMainWin.StatusText = "Ajout d'un utilisateur"
        End If

        If autoSendUpdateOnSave Then UsersManager.getInstance.update()
    End Sub

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noUser
        End Get
    End Property
End Class
