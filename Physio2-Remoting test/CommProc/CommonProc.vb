Option Strict Off
Option Explicit On

Imports System

Module CommonProc

#Region "Définitions"
    Private fileErrorLock As New System.Threading.Mutex()

    'REM ErrorEmailing
    Public defaultSMTPServer As MailAccount

    Public Const wm_ctlcolorlistbox As UInt32 = &H134


    Public tmModified As Boolean = False
    Public emptyHTMLPath As String
    Public infoDivers(,) As String

    Public localLogNumber As Integer = 0
    Public localLogFilePath As String = IO.Directory.GetParent(Application.ExecutablePath).FullName & "\Clinica.test.log"

    Public myRegEx As System.Text.RegularExpressions.Regex
    Public firstUsageDate As Date
    Public appPath As String
    Public currentDroitAcces() As Boolean
    Public currentClinic As Integer = 0 'Clinique courante
    Public currentClinicName As String = String.Empty
    Public currentClinicEmail As String = String.Empty
    Public currentUserName As String 'Nom de l'utilisateur courant
    Public maxi As Byte 'maximum de caractères pour le test de mot de passe
    Public noTemp As Object
    Public justAppliedFocus As Boolean
    Public forceShowAdmin As Boolean = False
    Public firstLoading As Boolean = True
    Public hasRestarted As Boolean = False
    Public noTypes() As String
    Public openedNewWindow As Boolean = False 'Indique si la fonction OpenUniqueWindow ouvre une nouvelle fenêtre (true) ou sélectionne une existante (false)

    Public Structure ClientKeys
        Dim nam As String
        Dim noClient As Integer
        Dim fullName As String
    End Structure
    Public Structure AnalysedExpression
        Dim conditions As String
        Dim selTables() As String
        Dim ErrorPos, errorLength As Integer
    End Structure
    Public Structure PositionType
        Dim x As Double
        Dim y As Double
    End Structure
    Public foundClient() As ClientKeys
    Public fileContent() As String
    Public myMainWin As MainWin

    Public Enum FactureAction
        Adjusted = 0
        Created = 1
        Deleted = 2
        Err = 3
    End Enum

    Public Enum SType
        Regular_Expression = 0
        Normal = 1
    End Enum

    Public Enum RRType
        Files = 0
        Dirs = 1
        Both = 2
    End Enum

    Public Enum OpeningType
        FILE = 0
        DB = 1
        REPORT = 2
        EMAIL = 3
        HTMLPAGE = 4
    End Enum
#End Region

#Region "Horaire"
    Public Sub openModifHoraire(ByVal noUser As Integer)
        'Droit & Accès
        If currentDroitAcces(62) = False And noUser = 0 Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas accès à la modification de l'horaire de la clinique." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If
        If currentDroitAcces(47) = False And noUser > 0 And noUser <> ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier l'horaire de tous les utilisateurs." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If
        If currentDroitAcces(48) = False And noUser > 0 And noUser = ConnectionsManager.currentUser Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit de modifier votre horaire." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myModifHoraire As modifhoraire = openUniqueWindow(New modifhoraire())
        myModifHoraire.loadHoraire(noUser)
        myModifHoraire.Show()
    End Sub

    'Public Sub updateHoraires(ByVal changedDate As Date, Optional ByVal allDates As Boolean = False, Optional ByVal sender As Object = Nothing, Optional ByVal addToUpdateList As Boolean = True, Optional ByVal noTRP As Integer = 0, Optional ByVal onlyDate As Boolean = False, Optional ByVal newHoraire As Boolean = False)
    '    With MyMainWin
    '        .HoraireDataSet.Clear()
    '        .HoraireDataSet = DBLinker.GetInstance.ReadDBForGrid("Horaires", "*")
    '        .HoraireView.Table = .HoraireDataSet.Tables("Table")
    '    End With

    '    If WindowsManager.GetInstance.Count > 0 Then
    '        Dim i As Integer

    '        If OnlyDate Then
    '            UpdateAgendas(ChangedDate, sender, False, , NoTRP)
    '        Else
    '            If AllDates = False Then
    '                For i = 0 To 6
    '                    UpdateAgendas(ChangedDate.AddDays(i), sender, False, , NoTRP)
    '                Next i
    '            Else
    '                UpdateAgendas(ChangedDate, sender, False, True, NoTRP)
    '            End If
    '        End If

    '        Dim MySingleWindows() As SingleWindow = WindowsManager.GetInstance.FindWindowsByName("modifhoraire")
    '        If Not MySingleWindows Is Nothing AndAlso MySingleWindows.Length <> 0 Then
    '            For i = 0 To MySingleWindows.GetUpperBound(0)
    '                With MySingleWindows(i)
    '                    If Not sender Is .GetForm Then
    '                        With CType(.GetForm, modifhoraire)
    '                            If NewHoraire = False Then
    '                                .ReLoadGraph(True)
    '                            Else
    '                                .ReLoadHoraireList(True)
    '                            End If
    '                        End With
    '                    End If
    '                End With
    '            Next i
    '        End If
    '    End If

    '    If AddToUpdateList = True Then InternalUpdatesManager.GetInstance.SendUpdate("Horaires(" & ChangedDate & "," & AllDates & "," & NoTRP & "," & OnlyDate & "," & NewHoraire & ")")
    'End Sub


#End Region

#Region "Mot de passe"
    Public Function mdpProcessToModif(ByRef cle As String, ByRef mdp As String) As String
        Dim NoG, NoD, NewMDP, PartieG, PartieD, G, D, DPart, gPart As String
        NoG = "" : NoD = "" : NewMDP = "" : PartieG = "" : PartieD = "" : G = "" : D = "" : DPart = "" : gPart = ""
        Dim lenD, lenG, Max, l As Integer
        Dim i, j As Integer
        Dim T4, T2, T1, T3, T5, t6 As Integer
        Dim MultipleG, multipleD As Double
        'Dans la procédure ci-dessous G signifie Gauche
        'Dans la procédure ci-dessous D signifie Droit(e)

        'Détermination des Multiplicateurs pour la partie gauche et droite
        'du mot de passe
        T1 = CInt(Left(cle, 4))
        T2 = CInt(Mid(cle, 6, 2))
        T3 = CInt(Mid(cle, 9, 2))
        T4 = CInt(Mid(cle, 11, 2))
        T5 = CInt(Mid(cle, 14, 2))
        t6 = CInt(Right(cle, 2))
        multipleD = (T1 * 1000 + T2 * 100 + T3) / 10000
        MultipleG = T4 + T5 + t6

        'Détermination de la séparation du mot de passe
        'Également si la longueur est paire
        'Un caractère de plus à gauche si impaire
        l = Len(mdp)
        If verifImpair(l) Then
            lenD = Int(l / 2)
            lenG = lenD + 1
        Else
            lenD = l / 2
            lenG = l / 2
        End If

        'Transformation en nombre des parties G & D du mot de passe
        PartieG = Left(mdp, lenG)
        PartieD = Right(mdp, lenD)

        For i = 1 To lenG
            NoG = NoG & Asc(Mid(PartieG, i, 1))
        Next i
        For j = 1 To lenD
            NoD = NoD & Asc(Mid(PartieD, j, 1))
        Next j
        If NoG = "" Then NoG = "0"
        If NoD = "" Then NoD = "0"

        'Multiplacation des parties G & D par les multiplicateurs
        NoG = NoG * MultipleG
        NoD = NoD * multipleD

        NewMDP = CStr(NoG & NoD)

        'Inversion des caractères deux à deux si le premier chiffre est impair
        If verifImpair(Left(NewMDP, 1)) Then
            'Vérification si longueur impair du NewMDP
            If verifImpair(Len(NewMDP)) Then
                Max = Len(NewMDP) - 1
            Else
                Max = Len(NewMDP)
            End If

            For i = 1 To Max Step 2
                G = Mid(NewMDP, i, 1)
                D = Mid(NewMDP, i + 1, 1)
                gPart = Left(NewMDP, i - 1)
                DPart = Right(NewMDP, Len(NewMDP) - i - 1)
                NewMDP = gPart & D & G & DPart
            Next i

            Return NewMDP
        Else
            Return NewMDP
        End If
    End Function

    Public Sub generateMDP(ByVal pos As Byte, ByRef from As addmodifusers)
        Dim p As Integer
        Dim i As Short
        Dim AlphaNum(), Cle, noword As String

        'Formation du tableau Alpha Numérique
        from.nochar.Text = CStr(Len(from.mdp.Text))

        AlphaNum = Split("0:1:2:3:4:5:6:7:8:9:a:b:c:d:e:f:g:h:i:j:k:l:m:n:o:p:q:r:s:t:u:v:w:x:y:z", ":")

        from.mdpTested.Visible = True

        With from.mdp
            For i = 0 To UBound(AlphaNum)
                'Minuscule
                System.Windows.Forms.Application.DoEvents()
                If from.mdpTested.Lines.Length > 100 Or pos > 20 Then
                    maxi = 0 : System.Windows.Forms.Application.DoEvents() : Exit Sub
                End If
                If maxi = 0 Then Exit Sub

                If pos = 1 And Len(.Text) = 1 Then
                    .Text = AlphaNum(i)
                ElseIf pos = 1 And Len(.Text) > 1 Then
                    .Text = AlphaNum(i) & Right(.Text, Len(.Text) - 1)
                ElseIf pos = 2 And Len(.Text) = 2 Then
                    .Text = Left(.Text, pos - 1) & AlphaNum(i)
                    generateMDP(pos - 1, from)
                Else
                    .Text = Left(.Text, pos - 1) & AlphaNum(i) & Right(.Text, Len(.Text) - pos)
                    generateMDP(pos - 1, from)
                End If

                Cle = DateFormat.getTextDate(Date.Today) & Date.Now.ToString("HH:mm:ss")
                noword = mdpProcessToModif(Cle, .Text)
                p = InStr(1, noword, "E")
                If p > 0 Then from.mdpTested.Text &= .Text & " - " & noword & " - " & Cle & vbCrLf : WriteLine(1, .Text & " - " & noword & " - " & Cle)

                If i = UBound(AlphaNum) And pos = Len(.Text) Then
                    .Text = .Text & "0"
                    generateMDP(pos + 1, from)
                End If
            Next i
        End With
    End Sub
#End Region

    Public Sub logToLocal(ByVal str As String)
        IO.File.AppendAllText(localLogFilePath, str & vbCrLf)
    End Sub

    Public Function getEasterDate(ByVal year As Integer) As Date
        Dim easter As New Date(year, 1, 1)
        Dim s, t, a, b, c, d, e, i As Integer
        t = year - 1900
        s = Int(t / 4)
        a = t Mod 19
        i = 11 * a
        b = i Mod 30
        c = (t + s) Mod 7
        d = b Mod 7
        i = 6 + c - d
        e = i Mod 7
        If (b <= 24) Then
            Return easter.AddDays(52 - b - e + 59)
        Else
            i = 49 - b - e
            If (i = 15 Or i = 16) Then
                i = i + 7
            ElseIf ((i = 17 Or i = 18) And e = 6) Then
                i = i + 7
            End If

            Return easter.AddDays(i + 90)
        End If
    End Function

    Private Sub changePrefs()
        Dim content() As String = IO.File.ReadAllLines(My.Application.Info.DirectoryPath & bar(My.Application.Info.DirectoryPath) & "prefs.link")
        Dim files() As String = IO.Directory.GetFiles(My.Application.Info.DirectoryPath, "*.vb", IO.SearchOption.AllDirectories)
        Dim searchWord As String = ""
        Dim replaceWord As String = ""
        For f As Integer = 0 To files.Length - 1
            If files(f).Contains("Assembly") Then Continue For
            If f Mod (files.Length / 100) = 0 Then Console.WriteLine("Files replaced " & f Mod (files.Length / 100) & "%")

            Dim curFileContent As String = IO.File.ReadAllText(files(f), System.Text.Encoding.GetEncoding("latin1"))
            Dim oldContent As String = curFileContent
            For c As Integer = 0 To content.Length - 1
                Dim line() As String = content(c).Split(New Char() {":"})
                If line(0).Trim = "" Then Continue For

                If line(0).StartsWith("U") Then
                    searchWord = "PrefUser"
                    replaceWord = "PreferencesManager.getUserPreferences().getProperty"
                Else
                    searchWord = "Pref"
                    replaceWord = "PreferencesManager.getGeneralPreferences().getProperty"
                End If

                searchWord &= "(" & line(1).Trim & ")"
                replaceWord &= "(""" & line(2).Trim & """)"
                curFileContent = curFileContent.Replace(searchWord, replaceWord)
            Next c

            If oldContent <> curFileContent Then
                IO.File.WriteAllText("C:\Temp" & files(f).Substring(files(f).LastIndexOf("\")), oldContent, System.Text.Encoding.GetEncoding("LATIN1"))
                IO.File.WriteAllText(files(f), curFileContent, System.Text.Encoding.GetEncoding("LATIN1"))
            End If
        Next f
    End Sub

    Private Sub mainSub()
        'TODO : Test for globalization : Transpose elsewhere and use Preferences to set it (See TODO below too !!!)
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo(Threading.Thread.CurrentThread.CurrentCulture.Name, False)
        Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "$"

        'TODO : Ensure Thread creation is done via a method which set the CurrentCulture
        'Dim t As New Threading.Thread(AddressOf test)
        't.CurrentCulture = Threading.Thread.CurrentThread.CurrentCulture
        't.Start()

        Base.External.setCurrentExternal(New Clinica.BaseLibExternal())
        CI.ProjectUpdates.ProjectUpdateLibrary.External.setCurrentExternal(New Clinica.ExternalUpdatesExternal())

        Console.WriteLine("Entered Software by " & Threading.Thread.CurrentThread.ManagedThreadId)
        Software.start()
    End Sub

    Public Sub main()
        AddHandler Application.ThreadException, AddressOf application_ThreadException
        AddHandler System.AppDomain.CurrentDomain.UnhandledException, AddressOf domain_UnhandledException
        If IO.File.Exists("dev.env") Then
            'If in development environment, than skip try catch so that error stop where it crashes
            mainSub()
        Else
            ' Add global error handling
            Try
                mainSub()
            Catch ex As Exception
                Loading.getInstance.Close()
                Software.doEndProcess()
                MessageBox.Show("Clinica doit se fermer dû à une cause inconnue. Veuillez redémarrer manuellement le logiciel." & vbCrLf & vbCrLf & "Erreur :" & vbCrLf & ex.Message, "Erreur inconnue", MessageBoxButtons.OK)
                addErrorLog(ex)
            End Try
        End If
    End Sub

    Private Sub domain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        If e.ExceptionObject IsNot Nothing AndAlso TypeOf e.ExceptionObject Is Exception Then
            addErrorLog(New Exception("Domain level exception", e.ExceptionObject))
        ElseIf e.ExceptionObject IsNot Nothing Then
            addErrorLog(New Exception("Domain level exception. TypeOf e.ExceptionObject=" & e.ExceptionObject.GetType.ToString))
        Else
            addErrorLog(New Exception("Domain level exception."))
        End If
    End Sub

    Private Sub application_ThreadException(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
        If Not TypeOf e.Exception Is AlreadyLoggedException Then addErrorLog(New Exception("Application level exception", e.Exception))
    End Sub

    Public Function genUniqueNo() As Integer
        Dim sql As String = "INSERT INTO UniqueNumber(Fake) VALUES(null);" & _
                            "SELECT @@IDENTITY;" & _
                            "DELETE FROM UniqueNumber WHERE NoUnique=@@IDENTITY;"

        Dim newNo As Integer = DBLinker.getInstance.readScalar(sql)

        Return newNo

        'OLD METHOD USE a FILE QUEUE.. BUGGING SOME TIMES
        '        If ConnectionsManager.currentUser < 1 Then Return 0

        '        Dim MyUser, NoFile, LFile, myFiles(), myFile As String
        '        Dim myNo As Integer
        '        Dim t As Byte
        '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '        'Ajout dans la file d'attente
        'StartBack:
        '        LFile = lastFile(appPath & bar(appPath) & "Data\UniqueNoQueue\")
        '        If LFile = "ERROR:NOFILE" Then
        '            NoFile = "00001"
        '        Else
        '            NoFile = Left(LFile, 5)
        '            If NoFile = "" Then NoFile = "0"
        '            NoFile = addZeros(Integer.Parse(NoFile) + 1, 5)
        '        End If

        '        myFile = NoFile & "-" & ConnectionsManager.currentUser
        '        If IO.File.Exists(appPath & bar(appPath) & "Data\UniqueNoQueue\" & myFile) = True Then GoTo StartBack
        '        Dim newFile As IO.Stream = IO.File.Create(appPath & bar(appPath) & "Data\UniqueNoQueue\" & myFile)
        '        newFile.Close()

        '        t = "2"
        '        Do
        '            myFiles = IO.Directory.GetFiles(appPath & bar(appPath) & "Data\UniqueNoQueue")
        '            If myFiles.Length = 0 Then Exit Do

        '            MyUser = myFiles(0).Substring(CStr(appPath & bar(appPath) & "Data\UniqueNoQueue").Length + 1)
        '            If MyUser = myFile Then Exit Do
        '        Loop Until t = "1"

        '        Dim openUniqueFile As System.IO.StreamWriter
        '        Try
        '            Dim uniqueFile As New IO.FileInfo(appPath & bar(appPath) & "Data\unique.no")
        '            If uniqueFile.Exists = False Then
        '                myNo = 1
        '            Else
        '                Dim myReader As System.IO.StreamReader = uniqueFile.OpenText
        '                myNo = myReader.ReadLine()
        '                myReader.Close()
        '            End If

        '            myNo += 1

        '            openUniqueFile = uniqueFile.CreateText
        '            openUniqueFile.WriteLine(myNo)
        '            openUniqueFile.Close()
        '        Catch
        '            GoTo StartBack
        '        End Try

        '        myFile = appPath & bar(appPath) & "Data\UniqueNoQueue\" & myFile
        '        If IO.File.Exists(myFile) = True Then IO.File.Delete(myFile)
        '        Return myNo

        'fin:
        '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private methodStackTrace As New StackTraceSymbolProvider(My.Application.Info.DirectoryPath & bar(My.Application.Info.DirectoryPath) & My.Application.Info.AssemblyName & ".pdb")

    Private Sub fillErrorLog(ByVal sbError As System.Text.StringBuilder, ByVal errorMsg As Exception, ByRef lastErrorMessage As String, Optional ByVal printingLineSpacing As String = "")
        Dim innerCount As Integer = 1
        Dim printingLineError As String = ""

        sbError.AppendLine("Date :" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        sbError.AppendLine("Version de Clinica : " & My.Application.Info.Version.ToString)
        sbError.AppendLine("Ordinateur : " & Environment.MachineName)
        sbError.AppendLine("Utilisateur Windows : " & Environment.UserDomainName & "/" & Environment.UserName)
        Dim curDALine As String = ""
        If currentDroitAcces IsNot Nothing Then curDALine = Chaines.copyArrayToString(currentDroitAcces)
        'TODO : Reactivate after verification it is not causing problems when errors happen before ConnectionsManager is loaded
        'If ConnectionsManager.isLoaded Then sbError.AppendLine("Utilisateur Clinica : " & currentClinique & "/" & ConnectionsManager.currentUser & "/" & currentUserName & "/" & curDALine)
        sbError.AppendLine("HasRestarted : " & hasRestarted)
        sbError.AppendLine("Version OS :" & Environment.OSVersion.VersionString)
        Do
            Try
                sbError.AppendLine(printingLineSpacing & "Error code : " & CType(errorMsg, Object).ErrorCode)
            Catch ex As Exception
            End Try
            If TypeOf (errorMsg) Is System.ComponentModel.Win32Exception Then
                sbError.AppendLine(printingLineSpacing & "NativeErrorCode : " & CType(errorMsg, ComponentModel.Win32Exception).NativeErrorCode)
            End If
            If TypeOf (errorMsg) Is SqlClient.SqlException Then
                sbError.AppendLine(printingLineSpacing & "Sql-Number : " & CType(errorMsg, SqlClient.SqlException).Number)
                sbError.AppendLine(printingLineSpacing & "Sql-LineNumber : " & CType(errorMsg, SqlClient.SqlException).LineNumber)
                sbError.AppendLine(printingLineSpacing & "Sql-Procedure : " & CType(errorMsg, SqlClient.SqlException).Procedure)
            End If

            sbError.AppendLine(printingLineSpacing & "Error type : " & errorMsg.GetType.ToString)
            lastErrorMessage = errorMsg.Message
            Dim multiLineSubject As Integer = lastErrorMessage.IndexOf(vbCrLf)
            If multiLineSubject <> -1 Then lastErrorMessage = lastErrorMessage.Substring(0, multiLineSubject) 'Keep only first line of message
            If lastErrorMessage.Length > 500 Then lastErrorMessage = lastErrorMessage.Substring(0, 500) & "... (cropped)"

            Dim mysource As String = ""
            If errorMsg.Source IsNot Nothing Then
                mysource = vbCrLf & vbCrLf & "Source :" & vbCrLf & errorMsg.Source
                If errorMsg.TargetSite IsNot Nothing Then mysource &= " -- " & errorMsg.TargetSite.ToString
            End If

            If errorMsg.TargetSite IsNot Nothing Then
                Try
                    Dim methodLocation As Integer = methodStackTrace.GetSourceLoc(errorMsg.TargetSite, 0)
                    sbError.AppendLine(printingLineSpacing & "Method starting line : " & methodLocation)
                Catch ex As Exception
                    'Not working
                    'TODO : Look what happens with this error from getting method starting code line number
                End Try
            End If

            printingLineError = printingLineSpacing & "Message :" & vbCrLf & errorMsg.Message & mysource & vbCrLf & vbCrLf & "Exception Stack Trace :" & vbCrLf & IIf(errorMsg.StackTrace Is Nothing, "Trace not available", errorMsg.StackTrace) & vbCrLf & vbCrLf & "Environment stack trace :" & vbCrLf & System.Environment.StackTrace
            printingLineError = printingLineError.Replace(vbCrLf, vbCrLf & printingLineSpacing)
            sbError.AppendLine(printingLineError)
            If errorMsg.Data IsNot Nothing AndAlso errorMsg.Data.Count <> 0 Then
                sbError.AppendLine()
                sbError.AppendLine("Data :")
                For Each curKey As System.Collections.DictionaryEntry In errorMsg.Data
                    sbError.AppendLine("   " & curKey.Key.ToString & "=" & curKey.Value.ToString)
                Next
                sbError.AppendLine()
            End If

            If TypeOf (errorMsg) Is Reflection.ReflectionTypeLoadException Then
                sbError.AppendLine(printingLineSpacing & "LoaderExceptions --->")
                For Each curEx As Exception In CType(errorMsg, Reflection.ReflectionTypeLoadException).LoaderExceptions
                    fillErrorLog(sbError, curEx, lastErrorMessage, printingLineSpacing)
                Next
                sbError.AppendLine(printingLineSpacing & "LoaderExceptions <---")
            End If

            sbError.AppendLine(printingLineSpacing & "InnerException " & innerCount & " --->")
            errorMsg = errorMsg.InnerException
            innerCount += 1
            printingLineSpacing &= "   "
        Loop While (Not errorMsg Is Nothing)
        sbError.AppendLine(printingLineSpacing & "Aucune")
        sbError.AppendLine("--------------------------------------------------------------------")

    End Sub

    Private errorsAlreadyLogged As New Generic.Dictionary(Of String, Boolean)

    Public Sub addErrorLog(ByVal errorMsg As Exception, Optional ByVal internalCount As Byte = 0, Optional ByVal forceErrorInFile As Boolean = False)
        'Annule la présente transaction si existe
        'REM DBLinker.getInstance.rollbackAllTransactions() --> BAD because error could be caused by something not in transaction

        'Ensure error is not due to ClinicaServer closed
        If TypeOf errorMsg Is System.Net.Sockets.SocketException Then
            Dim socketError As System.Net.Sockets.SocketException = errorMsg
            If socketError.SocketErrorCode = Net.Sockets.SocketError.ConnectionRefused Then Exit Sub
        End If

        'Get most deep error
        Dim deepestError As Exception = errorMsg
        While deepestError.InnerException IsNot Nothing
            deepestError = deepestError.InnerException
        End While

        'Ensure error has not been already logged
        Dim errorKey As String = deepestError.StackTrace
        If errorKey Is Nothing Then errorKey = Environment.StackTrace
        If errorsAlreadyLogged.ContainsKey(errorKey) Then Exit Sub 'Error already log

        'Add to error logged list
        errorsAlreadyLogged.Add(errorKey, True)


        'Start error log
        Dim writeStarted As Boolean = False
        Dim hasToRetry As Boolean = False
        Dim sbError As New Text.StringBuilder()
        Dim baseErrorMsg As Exception = errorMsg
        Dim fileNum As Integer = FreeFile()
        Dim subject As String = ""

        Try
backToTesting:
            If IO.Directory.Exists(appPath) = False Then
                If MessageBox.Show("Le chemin pour les données de Clinica est présentement introuvable. Vérifier que votre réseau fonctionne et que l'emplacement suivant existe :" & vbCrLf & appPath & vbCrLf & vbCrLf & "Désirez-vous réparer le problème et ensuite répondre OUI ou fermer le logiciel en répondant NON ?", "Erreur innattendue", MessageBoxButtons.YesNo) = DialogResult.No Then
                    End
                Else
                    GoTo backToTesting
                End If
            End If

            fillErrorLog(sbError, errorMsg, subject)

            REM            IO.File.AppendAllText(My.Application.Info.DirectoryPath & "\ErrorTest.txt", "===================================" & vbCrLf & sbError.ToString())

            'Sending error by email to clinica-error@cints.net
            If Not forceErrorInFile OrElse (System.Diagnostics.Debugger.IsAttached = False AndAlso Process.GetCurrentProcess.ProcessName.EndsWith("vshost") = False AndAlso defaultSMTPServer IsNot Nothing AndAlso defaultSMTPServer.smtpServer.server <> "") Then
                errorMsg = baseErrorMsg
                subject = "[Clinica] Error : " & subject
                Dim body As String = sbError.ToString

                If emailSending(defaultSMTPServer, "clinica-error@cints.net", "clinica-error@cints.net", "", "", subject, False, body, , "", "", False) Then Exit Sub
            End If

            'Writing error in errors.log file
            fileErrorLock.WaitOne()
            writeStarted = True
            Dim errorFilePath As String = appPath & bar(appPath) & "erreurs.log"
            If IO.Directory.Exists(IO.Path.GetDirectoryName(errorFilePath)) = False Then
                errorFilePath = Environment.CurrentDirectory & bar(Environment.CurrentDirectory) & "erreurs.log"
            End If
            Using errorFile As IO.FileStream = IO.File.Open(errorFilePath, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Read)
                Dim writeStream As New IO.StreamWriter(errorFile, System.Text.Encoding.Unicode)
                writeStream.Write(sbError.ToString)
                writeStream.Flush()
            End Using
            fileErrorLock.ReleaseMutex()
            writeStarted = False
        Catch ex As Exception
            If writeStarted Then fileErrorLock.ReleaseMutex()

            hasToRetry = True
        Finally
        End Try

        'Retry or log impossible to retry
        If hasToRetry Then
            If internalCount < 5 Then
                addErrorLog(errorMsg, internalCount + 1)
            Else
                MessageBox.Show("Impossible d'écrire le message d'erreur dans le fichier erreurs.log : " & errorMsg.Message, "Erreur")
            End If
        End If
    End Sub

    Public Sub updatePunch(ByVal userToUpdate As Integer, Optional ByVal sender As Object = Nothing, Optional ByVal addToUpdateList As Boolean = True)
        With myMainWin
            .PunchClock.loading()
        End With

        If addToUpdateList = True Then InternalUpdatesManager.getInstance.sendUpdate("Punch(" & userToUpdate & ")")
    End Sub

    Public Sub openDelTables()
        Dim myDelTables As DelTables = openUniqueWindow(New DelTables)
        myDelTables.Show()
    End Sub
End Module
