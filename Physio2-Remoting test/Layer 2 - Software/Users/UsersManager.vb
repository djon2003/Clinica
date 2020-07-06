Public Class UsersManager
    Inherits ManagerBase(Of UsersManager)
    Implements IEmailsValidable, IDataConsumer(Of DataInternalUpdate)

    Private users As New Generic.List(Of User)

    Public Shared ReadOnly Property currentUser() As User
        Get
            If ConnectionsManager.currentUser = 0 Then Return UserAdmin.getInstance()

            Return UsersManager.getInstance.getUser(ConnectionsManager.currentUser)
        End Get
    End Property

    Protected Sub New()
        load()
        EmailValidator.addValidable(Me)
        InternalUpdatesManager.getInstance.addConsumer(Me)
    End Sub

    Public Function getUser(ByVal noUser As Integer) As User
        For Each curUser As User In users
            If curUser.noUser = noUser Then Return curUser
        Next

        Return Nothing
    End Function

    Public Function getUsers(Optional ByVal getTerminated As Boolean = True, Optional ByVal onlyTherapist As Boolean = False, Optional ByVal getNotStarted As Boolean = True) As Generic.List(Of User)
        Dim users As New Generic.List(Of User)
        For Each curUser As User In Me.users
            If (onlyTherapist = False OrElse curUser.isTherapist) AndAlso (getTerminated = True OrElse (date1Infdate2(curUser.endingDate, Date.Today) = False)) AndAlso (getNotStarted = True OrElse curUser.startingDate.Equals(LIMIT_DATE) OrElse (date1Infdate2(curUser.startingDate, Date.Today) = True)) Then
                users.Add(curUser)
            End If
        Next

        Return users
    End Function

    Private Sub load()
        Dim users As DataSet = DBLinker.getInstance.readDBForGrid("Utilisateurs LEFT JOIN Villes ON Villes.NoVille = Utilisateurs.NoVille LEFT JOIN Titres ON Titres.NoTitre = Utilisateurs.NoTitre", "Utilisateurs.*,NomVille as Ville,Titre", "WHERE NoUser<>0 ORDER BY Utilisateurs.Nom + ',' + Utilisateurs.Prenom")
        If users Is Nothing OrElse users.Tables.Count = 0 Then Exit Sub

        DBLinker.getInstance.readDBForGrid("UsersSettings", "*", , , , "Settings", users)
        Dim settingsTable As DataTable = users.Tables("Settings")

        Me.users.Clear()

        For Each curRow As DataRow In users.Tables(0).Rows
            Dim newUser As New User
            newUser.loadData(New DBItemableData(curRow))

            Dim settings() As DataRow = settingsTable.Select("NoUser=" & newUser.noUser)
            If settings.Length <> 0 Then
                newUser.settings.loadData(New DBItemableData(settings))
            Else
                newUser.settings.noUser = newUser.noUser
            End If
            If newUser.noUser = ConnectionsManager.currentUser Then
                Dim daLine As String = newUser.rights
                If daLine.StartsWith("3") Then daLine = daLine.Substring(1)
                currentDroitAcces = splitStr(daLine, 1)
            End If
            Me.users.Add(newUser)
        Next
    End Sub

    Public Function validateEmails(Optional ByVal where As String = "", Optional ByVal reportOnly As Boolean = False) As System.Data.DataTable Implements IEmailsValidable.validateEmails
        Dim invalidTable As DataTable = EmailValidator.getBaseValidationTable()
        Dim emails As New Generic.List(Of String)
        Dim clients As DataSet = DBLinker.getInstance.readDBForGrid("Utilisateurs", "*", where & IIf(where <> "", " AND ", "") & "Courriel<>'' ORDER BY Nom,Prenom")
        If clients Is Nothing OrElse clients.Tables.Count = 0 OrElse clients.Tables(0).Rows.Count = 0 Then Return invalidTable

        For Each curRow As DataRow In clients.Tables(0).Rows
            If emails.Contains(curRow("Courriel")) = False Then emails.Add(curRow("Courriel"))
        Next

        Dim answer As Generic.Dictionary(Of String, Integer()) = EmailValidator.isEmailsValid(MailsManager.mainFromEmailAddress, emails)
        Dim sqlUpdate As New System.Text.StringBuilder()
        For Each curKey As String In answer.Keys
            If answer(curKey)(0) = False Then
                sqlUpdate.Append(" OR Courriel='" & curKey.Replace("'", "''") & "'")
                Dim rows() As DataRow = clients.Tables(0).Select("Courriel='" & curKey.Replace("'", "''") & "'")
                For Each curRow As DataRow In rows
                    invalidTable.Rows.Add(New Object() {"Utilisateur", curRow("Nom") & "," & curRow("Prenom"), curKey, answer(curKey)(1), "USER|" & curRow("NoUser")})
                Next
            End If
        Next
        Dim whereUpdate As String = sqlUpdate.ToString
        If reportOnly = False AndAlso whereUpdate <> "" Then
            whereUpdate = "UPDATE Utilisateurs SET Courriel='' WHERE " & whereUpdate.Substring(4)
            DBLinker.executeSQLScript(whereUpdate, False)
        End If

        Return invalidTable
    End Function

    Public ReadOnly Property type() As String Implements IEmailsValidable.type
        Get
            Return "Utilisateur"
        End Get
    End Property

    Public Function chooseUser(Optional ByVal addAllChoice As Boolean = False, Optional ByVal onlyTherapist As Boolean = True, Optional ByVal employeeType As Integer = 0, Optional ByVal selectedUser As String = "", Optional ByVal skipCurrentUser As Boolean = False, Optional ByVal getTerminated As Boolean = True, Optional ByVal getNotStarted As Boolean = True) As User
        If users.Count = 0 Then Return Nothing

        Dim myUserList As String = ""
        Dim typeUser As String = ""
        If employeeType = 2 Then
            typeUser = "travailleur autonome"
        ElseIf employeeType = 1 Then
            typeUser = "employé"
        End If
        Dim userTypeName As String = "thérapeute"
        If onlyTherapist = False Then userTypeName = "utilisateur"
        Dim myUsers As Generic.List(Of User) = Me.getUsers(getTerminated, onlyTherapist, getNotStarted)

        For Each curUser As User In myUsers
            If skipCurrentUser AndAlso curUser.noUser = ConnectionsManager.currentUser Then Continue For

            If onlyTherapist Then
                If curUser.isTherapist AndAlso (employeeType = 0 Or employeeType = curUser.noEmployeeType) Then myUserList &= "§" & curUser.toString
            Else
                If (employeeType = 0 Or employeeType = curUser.noEmployeeType) Then myUserList &= "§" & curUser.toString
            End If
        Next
        Dim plural As String = ""
        Dim singular As Char = "'"
        If onlyTherapist Then singular = "e"
        Dim monChoix As String = selectedUser
        If addAllChoice Then
            plural = "(s)"
            If monChoix = "" Then monChoix = "* Tous les " & userTypeName & "s *"
            myUserList = "* Tous les " & userTypeName & "s *" & myUserList
        Else
            If myUserList <> "" Then myUserList = myUserList.Substring(1)
        End If
        If myUserList = "" Then Return Nothing

        Dim myMultiChoice As New multichoice()
        Dim choosen As String = myMultiChoice.GetChoice("Veuillez sélectionner l" & IIf(plural <> "", "e", singular) & plural & IIf(plural <> "" Or singular <> "'", " ", "") & userTypeName & plural & " désiré" & plural, myUserList, , "§", , monChoix)
        If choosen.StartsWith("ERROR") OrElse choosen = "" Then Return Nothing
        Dim chosenUser As User
        If choosen.StartsWith("*") Then
            chosenUser = UserAll.getInstance()
        Else
            Dim sNoUser() As String = choosen.Split(New Char() {"("})
            Dim noUser As Integer = sNoUser(1).Substring(0, sNoUser(1).Length - 1)
            chosenUser = getUser(noUser)
        End If

        Return chosenUser
    End Function

    Public Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
        If dataReceived.function <> "UsersList" OrElse dataReceived.fromExternal = False Then Exit Sub

        load()
        updatingALLTRPMenu(False)
    End Sub

    Public Sub update()
        load()
        updatingALLTRPMenu(False)
        InternalUpdatesManager.getInstance.sendUpdate("UsersList()")
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return 0
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function
End Class
