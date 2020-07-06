Public Class UserManager
    Inherits ManagerBase(Of UserManager)
    Implements IEmailsValidable, DataConsumer(Of DataInternalUpdate)

    Private users As New Generic.List(Of User)

    Protected Sub new()
        load()
        EmailValidator.addValidable(Me)
        InternalUpdatesManager.GetInstance.AddConsumer(Me)
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
            If (onlyTherapist = False OrElse curUser.isTherapist) AndAlso (getTerminated = True OrElse (Date1InfDate2(curUser.dateFin, Date.Today) = False)) AndAlso (getNotStarted = True OrElse curUser.dateDebut.Equals(LimitDate) OrElse (Date1InfDate2(curUser.dateDebut, Date.Today) = True)) Then
                users.Add(curUser)
            End If
        Next

        Return users
    End Function

    Private Sub load()
        Dim users As DataSet = DBLinker.GetInstance.ReadDBForGrid("Utilisateurs LEFT JOIN Villes ON Villes.NoVille = Utilisateurs.NoVille LEFT JOIN Titres ON Titres.NoTitre = Utilisateurs.NoTitre", "Utilisateurs.*,NomVille as Ville,Titre", "WHERE 1=1 ORDER BY Utilisateurs.Nom + ',' + Utilisateurs.Prenom")
        If users Is Nothing OrElse users.Tables.Count = 0 Then Exit Sub

        Me.users.Clear()

        For Each curRow As DataRow In users.Tables(0).Rows
            Dim newUser As New User
            newUser.loadData(New DBItemableData(curRow))
            If newUser.noUser = CurrentUser Then
                Dim daLine As String = newUser.droitAcces
                If DALine.StartsWith("3") Then DALine = DALine.Substring(1)
                CurrentDroitAcces = SplitStr(DALine, 1)
            End If
            Me.users.Add(newUser)
        Next
    End Sub

    Public Function validateEmails(Optional ByVal where As String = "", Optional ByVal reportOnly As Boolean = False) As System.Data.DataTable Implements IEmailsValidable.validateEmails
        Dim invalidTable As DataTable = EmailValidator.getBaseValidationTable()
        Dim emails As New Generic.List(Of String)
        Dim clients As DataSet = DBLinker.GetInstance.ReadDBForGrid("Utilisateurs", "*", where & IIf(where <> "", " AND ", "") & "Courriel<>'' ORDER BY Nom,Prenom")
        If clients Is Nothing OrElse clients.Tables.Count = 0 OrElse clients.Tables(0).Rows.Count = 0 Then Return invalidTable

        For Each curRow As DataRow In clients.Tables(0).Rows
            If emails.Contains(curRow("Courriel")) = False Then emails.Add(curRow("Courriel"))
        Next

        Dim answer As Generic.Dictionary(Of String, Integer()) = EmailValidator.isEmailsValid(emails)
        Dim sqlUpdate As New System.Text.StringBuilder()
        For Each curKey As String In answer.Keys
            If answer(curKey)(0) = False Then
                sqlUpdate.Append(" OR Courriel='" & curKey.Replace("'", "''") & "'")
                Dim rows() As DataRow = clients.Tables(0).Select("Courriel='" & curKey.Replace("'", "''") & "'")
                For Each curRow As DataRow In rows
                    invalidTable.Rows.Add(New Object() {"Utilisateur", curRow("Nom") & "," & curRow("Prenom"), curKey, answer(curKey)(1)})
                Next
            End If
        Next
        Dim whereUpdate As String = sqlUpdate.ToString
        If reportOnly = False AndAlso whereUpdate <> "" Then
            whereUpdate = "UPDATE Utilisateurs SET Courriel='' WHERE " & whereUpdate.Substring(4)
            DBLinker.ExecuteSQLScript(whereUpdate, False)
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
        If EmployeeType = 2 Then
            TypeUser = "travailleur autonome"
        ElseIf EmployeeType = 1 Then
            TypeUser = "employé"
        End If
        Dim userTypeName As String = "thérapeute"
        If onlyTherapist = False Then UserTypeName = "utilisateur"
        Dim myUsers As Generic.List(Of User) = Me.getUsers(getTerminated, onlyTherapist, getNotStarted)

        For Each curUser As User In myUsers
            If SkipCurrentUser AndAlso curUser.noUser = CurrentUser Then Continue For

            If onlyTherapist Then
                If curUser.isTherapist AndAlso (EmployeeType = 0 Or EmployeeType = curUser.noTypeEmploye) Then MyUserList &= "§" & curUser.ToString
            Else
                If (EmployeeType = 0 Or EmployeeType = curUser.noTypeEmploye) Then MyUserList &= "§" & curUser.ToString
            End If
        Next
        Dim plural As String = ""
        Dim singular As Char = "'"
        If onlyTherapist Then singular = "e"
        Dim monChoix As String = SelectedUser
        If AddAllChoice Then
            plural = "(s)"
            If MonChoix = "" Then MonChoix = "* Tous les " & UserTypeName & "s *"
            MyUserList = "* Tous les " & UserTypeName & "s *" & MyUserList
        Else
            If MyUserList <> "" Then MyUserList = MyUserList.Substring(1)
        End If
        If MyUserList = "" Then Return Nothing

        Dim myMultiChoice As New multichoice()
        Dim choosen As String = MyMultiChoice.GetChoice("Veuillez sélectionner l" & IIf(plural <> "", "e", singular) & plural & IIf(plural <> "" Or singular <> "'", " ", "") & UserTypeName & plural & " désiré" & plural, MyUserList, , "§", , MonChoix)
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

    Public Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements DataConsumer(Of DataInternalUpdate).DataConsume
        If dataReceived.function <> "UsersList" OrElse dataReceived.fromExternal = False Then Exit Sub

        load()
        UpdatingALLTRPMenu(False)
    End Sub

    Public Sub update()
        load()
        UpdatingALLTRPMenu(False)
        InternalUpdatesManager.GetInstance.SendUpdate("UsersList()")
    End Sub

    Public ReadOnly Property priority() As Integer Implements DataConsumer(Of DataInternalUpdate).priority
        Get
            Return 0
        End Get
    End Property

    Public Function compareTo(ByVal other As DataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of DataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function
End Class
