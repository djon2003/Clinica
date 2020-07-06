Public MustInherit Class File

    Public Const PURSUIT_REASON As String = "Le dossier #OLD_FOLDER a atteint le maximum de rapport d'étape (100). Ainsi, une procédure automatique a créé un nouveau dossier ayant les mêmes informations et le rapport d'étape #100 correspond maintenant au rapport d'étape #1 dans ce nouveau dossier #NEW_FOLDER. Veuillez exécuter l'analyse des textes via Clinica pour ajouter les textes de base à ce nouveau dossier."

    Public fileResults As New List(Of FileResult)

    Private pastFilesResults As Generic.List(Of FileResult)
    Private outPath As String = ""
    Private Const APPLICATION_CODE As String = "050"
    Private data As DataSet

    Private _nbProcessed As Integer = 0
    Private _nbErrors As Integer = 0
    Private generatedFileName As String = String.Empty

    Protected Const MIN_FOLDER_EVENT_DATE As Date = #1/1/1929#
    Protected Const DEFAULT_COUNTRY As String = "CANADA"
    Protected clinicData As DataRow
    Private out As New System.Text.StringBuilder()
    Protected outLine As New System.Text.StringBuilder()
    Protected errors As New List(Of FieldValidationException)
    Protected Shared provinces As Generic.Dictionary(Of String, String)
    Protected Shared provincesKey() As String = {"Québec", "Ontario", "Terre-Neuve", "Nouvelle-Écosse", "Île du Prince-Édouard ", "Nouveau Brunswick", "Manitoba", "Saskatchewan", "Alberta", "Colombie-Britannique", "Territoires du Nord-Ouest", "Yukon"}
    Protected Shared provincesPostalCodeFirstLetters As New Generic.Dictionary(Of String, String())
    Protected curRow As DataRow

    Protected Class LineType
        Public Const HEADER As String = "01"
        Public Const INITIAL As String = "02"
        Public Const [STEP] As String = "03"
        Public Const FINAL As String = "04"
        Public Const PRESENCES As String = "05"
        Public Const FOOTER As String = "99"
    End Class

    Public Event creationProgressed(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub generateProvinces()
        If provinces Is Nothing Then
            Dim shortenProvinces() As String = {"Quebec", "Ontario", "Terre-Neuv", "Nouvelle-E", "Ile du Pri", "Nouveau Br", "Manitoba", "Saskatchew", "Alberta", "Colombie-B", "Territoire", "Yukon"}

            Dim postalCodeFirstLetters As String() = {"G,H,J", "K,L,M,N,P", "A", "B", "C", "E", "R", "S", "T", "V", "X", "Y"}
            provinces = New Generic.Dictionary(Of String, String)()
            For i As Integer = 0 To provincesKey.GetUpperBound(0)
                provinces.Add(provincesKey(i), shortenProvinces(i))
                provincesPostalCodeFirstLetters.Add(provincesKey(i), postalCodeFirstLetters(i).Split(New Char() {","}))
            Next i
        End If
    End Sub

    Public Sub New(ByVal xmlFile As String)
        loadFileResults(xmlFile)
    End Sub

    Public Sub New(ByVal outPath As String, ByVal data As DataSet)
        generateProvinces()
        Me.outPath = outPath
        Me.data = data

        clinicData = data.Tables("clinic").Rows(0)
    End Sub

    Public ReadOnly Property nbErrors() As Integer
        Get
            Return _nbErrors
        End Get
    End Property

    Public ReadOnly Property nbProcessed() As Integer
        Get
            Return _nbProcessed
        End Get
    End Property

    Public Shared Function createType(ByVal xmlSaveFile As String) As File
        Dim filename As String = xmlSaveFile.Substring(xmlSaveFile.LastIndexOf("\") + 1)
        Dim newFile As File = Nothing
        Select Case True
            Case filename.StartsWith(FileInitialReports.FILE_PREFIX)
                newFile = New FileInitialReports(xmlSaveFile)
            Case filename.StartsWith(FileStepReports.FILE_PREFIX)
                newFile = New FileStepReports(xmlSaveFile)
            Case filename.StartsWith(FileFinalReports.FILE_PREFIX)
                newFile = New FileFinalReports(xmlSaveFile)
            Case filename.StartsWith(FilePresences.FILE_PREFIX)
                newFile = New FilePresences(xmlSaveFile)
        End Select

        Return newFile
    End Function


    Public Shared Function createType(ByVal tableName As String, ByVal outPath As String, ByVal data As DataSet) As File
        Select Case tableName
            Case "initials"
                Return New FileInitialReports(outPath, data)
            Case "steps"
                Return New FileStepReports(outPath, data)
            Case "finals"
                Return New FileFinalReports(outPath, data)
            Case "presences"
                Return New FilePresences(outPath, data)
        End Select

        Return Nothing
    End Function

    Protected Sub onCreationProgressed(ByVal e As EventArgs)
        RaiseEvent creationProgressed(Me, e)
    End Sub

    Private Function checkProvinceVersusPostalCode(ByVal postalCode As String, ByVal province As String) As Boolean
        Dim firstLetter As String = postalCode.Substring(0, 1).ToUpper
        For Each letter As String In provincesPostalCodeFirstLetters(province)
            If letter = firstLetter Then Return True
        Next

        Return False
    End Function

    Protected Function isAlreadyAnError(ByVal noFolder As Integer) As Boolean
        For Each curResult In pastFilesResults
            If curResult.noFolder = noFolder AndAlso curResult.isError Then Return True
        Next

        Return False
    End Function

    Public Shared Function validateNAM(ByVal nam As String, ByVal clientLastName As String, ByVal clientFirstName As String) As Boolean
        If System.Text.RegularExpressions.Regex.IsMatch(nam, "[a-zA-Z]{4}[0-9]{6}[abcdefghjklmnpqrstuvwyzABCDEFGHJKLMNPQRSTUVWYZ1-9][0-9]", System.Text.RegularExpressions.RegexOptions.IgnoreCase) = False Then Return False

        If clientLastName <> String.Empty AndAlso clientFirstName <> String.Empty Then
            clientFirstName = replaceAccents(clientFirstName)
            clientLastName = replaceAccents(clientLastName)

            Dim firstFourChars As String = String.Empty
            If clientLastName.Length = 1 Then
                firstFourChars = clientLastName & "XX"
            ElseIf clientLastName.Length = 2 Then
                firstFourChars = clientLastName & "X"
            Else
                firstFourChars = clientLastName.Substring(0, 3)
            End If
            firstFourChars &= clientFirstName.Substring(0, 1)
            firstFourChars = firstFourChars.ToUpper()
            If nam.ToUpper.Substring(0, 4) <> firstFourChars Then Return False
        End If

        Dim month As Integer = nam.Substring(6, 2) Mod 50
        If month > 12 Then Return False

        Dim day As Integer = nam.Substring(8, 2) Mod 50
        If day > Date.DaysInMonth(2004, month) Then Return False 'The 2004 year is taken because its a leap year. Not taking real year, because only two numbers, so tiny possibility of error

        Dim validator As Integer = nam.Substring(11, 1)
        'TODO : NAM : Implement validator checking when received how to do it

        Return True
    End Function

    Protected Sub writeEventDate()
        'Date d'événement   SSAAMMJJ  9(8)
        If curRow("FolderEventDate") Is DBNull.Value Then
            errors.Add(New FieldValidationException("La date d'événement (Date d'accident) est manquante.", True))
        Else
            Dim eventDate As Date = curRow("FolderEventDate")
            If eventDate < MIN_FOLDER_EVENT_DATE Then errors.Add(New FieldValidationException("La date d'événement (Date d'accident) ne doit pas être inférieure à " & CI.Base.DateFormat.getTextDate(MIN_FOLDER_EVENT_DATE) & ".", True))
            writeDateField(eventDate)
        End If
    End Sub

    Protected Sub writePostalCode(ByVal postalCode As String, ByVal fieldName As String, ByVal isDefaultCountry As Boolean, ByVal province As String)
        postalCode = postalCode.Replace(" ", "")
        If isDefaultCountry AndAlso postalCode.Length <> 6 Then
            errors.Add(New FieldValidationException("Le code postal (" & fieldName & ") doit être de 6 caractères.", True))
        Else
            If isDefaultCountry AndAlso checkProvinceVersusPostalCode(postalCode, province) = False Then
                errors.Add(New FieldValidationException("Le code postal (" & fieldName & ") doit commencer par l'une de ces lettres : " & String.Join(", ", provincesPostalCodeFirstLetters(province)) & ".", True))
            End If
        End If

        'Code postal    X(6)
        writeTextField(postalCode, 6)
    End Sub

    Protected Sub writeNoFolderField()
        'This field is optional and if given it has to be the number from CSST. So let it blank.
        Dim noFolder As String = String.Empty
        If curRow("FolderNoRef") IsNot DBNull.Value Then noFolder = curRow("FolderNoRef")
        If noFolder.Length = 0 Then
            If Not TypeOf Me Is FileInitialReports AndAlso Not TypeOf Me Is FilePresences Then
                errors.Add(New FieldValidationException("Le numéro de référence (Dossier->Infos) est manquant.", True))
            End If
            writeSpaces(9)
        Else
            Dim otherData As String = System.Text.RegularExpressions.Regex.Replace(noFolder, "[0-9]", String.Empty)

            'Some CSST numbers seem to be less than 9 numbers.
            'If noFolder.Length <> 9 Then
            '    errors.Add(New FieldValidationException("Le numéro de référence (Dossier->Infos) doit contenir extactement 9 chiffres"))
            'Else
            If otherData <> String.Empty Then
                errors.Add(New FieldValidationException("Le numéro de référence (Dossier->Infos) est invalide. Il doit contenir extactement 9 chiffres.", True))
            Else
                'TODO : Shall validate last number.. when will receive the method to do it
                writeNumericField(noFolder, 9)
                'If noFolder.Length > 8 Then noFolder = noFolder.Substring(0, 8)
                ''No dossier   9(9)
                'writeNumericField(noFolder, 8)
                'outLine.Append(noFolder Mod 10)
            End If
        End If
    End Sub

    Protected Sub writeProvinceField(ByVal province As String, ByVal isDefaultCountry As Boolean)
        If isDefaultCountry Then
            If provinces.ContainsKey(province) = False Then
                errors.Add(New FieldValidationException("La province du client doit être dans l'une des valeurs suivantes lorsque son pays est " & DEFAULT_COUNTRY & " : " & String.Join(", ", provincesKey) & ".", True))
            Else
                writeTextField(provinces(province), 10)
            End If
        Else
            writeTextField(province, 10)
        End If
    End Sub

    Protected Sub writeTextField(ByVal data As String, ByVal length As Integer)
        If data.Length > length Then data = data.Substring(0, length)

        data = data.Replace(vbCrLf, "  ").Replace(vbTab, " ")
        outLine.Append(data)
        writeSpaces(length - data.Length)
    End Sub

    Protected Sub writeNumericField(ByVal data As String, ByVal length As Integer)
        data = System.Text.RegularExpressions.Regex.Replace(data, "[^0-9]", "")
        If data.Length > length Then data = data.Substring(data.Length - length, length)

        outLine.Append(addZeros(data, length))
    End Sub

    Protected Sub writeDateField(ByVal data As Date)
        If data.Year = 1 Then data = Date.Now

        outLine.Append(data.Year & addZeros(data.Month, 2) & addZeros(data.Day, 2))
    End Sub

    Protected Sub writeSpaces(ByVal nbSpaces As Integer)
        For i As Integer = 1 To nbSpaces
            outLine.Append(" ")
        Next
    End Sub

    Private Sub createHeader()
        Dim clinicCountry As String = clinicData("ClinicCountry")
        Dim isDefaultCountry As Boolean = clinicCountry.ToUpper = DEFAULT_COUNTRY
        Dim province As String = clinicData("ClinicProvinceState")

        outLine.Append(LineType.HEADER)
        'No d'enregistrement au service des échan- ges électroniques   9(8)
        writeNumericField(clinicData("ClinicNoCSST"), 8)

        'Date d'aujoud'hui
        writeDateField(Date.Now)

        'Code postal de la clinique    X(6)
        writePostalCode(clinicData("ClinicPostalCode"), "ClinicPostalCode", isDefaultCountry, province)

        'Blanc requis par le système
        writeSpaces(51)

        outLine.Append(APPLICATION_CODE)

        'Blanc requis par le système
        writeSpaces(8)

        'Logon ID (rempli par le serveur CSST)
        writeSpaces(8)

        'Nom  de  la clinique    X(70)
        writeTextField(clinicData("ClinicName"), 70)

        'Blanc requis par le système
        writeSpaces(111)

        outLine.AppendLine()

        out.Append(outLine.ToString)
    End Sub

    Protected Sub createBody()
        With data.Tables(dataTableName).Rows
            For i As Integer = 0 To .Count - 1
                curRow = .Item(i)
                Try
                    outLine = New System.Text.StringBuilder()
                    errors = New List(Of FieldValidationException)
                    writeLine()
                    If errors.Count = 0 Then
                        out.Append(outLine.ToString)
                        Dim nbLines() As String = outLine.ToString().Split(New String() {vbCrLf}, StringSplitOptions.None)

                        Dim extraResultInfo As String = getExtraResultInfo()
                        If extraResultInfo <> String.Empty Then extraResultInfo = " " & extraResultInfo
                        fileResults.Add(New FileResult(False, curRow("NoClient"), curRow("ClientLastName") & "," & curRow("ClientFirstName"), curRow("NoFolder"), getLineObject(), "Créé(e)" & extraResultInfo, Me.fileName, curRow("ClientNAM"), CsstTask.RESULT_OK_COLOR, curRow("NoRVsList"), curRow("FolderEventDate"), curRow("FolderService")))
                        _nbProcessed += nbLines.Length - 1
                    Else
                        Dim errorText As String = ""
                        For Each curError As FieldValidationException In errors
                            If curError.isError Then _nbErrors += 1
                            If errorText <> "" Then errorText &= "<BR/>"
                            If curError.fontColor <> CsstTask.RESULT_ERROR_COLOR Then errorText &= "<font color=" & curError.fontColor & ">"
                            errorText &= curError.Message
                            If curError.fontColor <> CsstTask.RESULT_ERROR_COLOR Then errorText &= "</font>"
                        Next
                        Dim folderEventDate = Base.LIMIT_DATE
                        If curRow("FolderEventDate") IsNot DBNull.Value Then folderEventDate = curRow("FolderEventDate")
                        fileResults.Add(New FileResult(True, curRow("NoClient"), curRow("ClientLastName") & "," & curRow("ClientFirstName"), curRow("NoFolder"), getLineObject(), errorText, Me.fileName, curRow("ClientNAM"), CsstTask.RESULT_ERROR_COLOR, "", folderEventDate, curRow("FolderService")))
                    End If
                Catch ex As FilesCreationSkippingException
                    'No result to add because skipping this entry
                Catch ex As FieldValidationException
                    If ex.isError Then _nbErrors += 1
                    Dim folderEventDate = Base.LIMIT_DATE
                    If curRow("FolderEventDate") IsNot DBNull.Value Then folderEventDate = curRow("FolderEventDate")
                    fileResults.Add(New FileResult(True, curRow("NoClient"), curRow("ClientLastName") & "," & curRow("ClientFirstName"), curRow("NoFolder"), getLineObject(), ex.Message, Me.fileName, curRow("ClientNAM"), ex.fontColor, "", folderEventDate, curRow("FolderService")))
                End Try

                onCreationProgressed(EventArgs.Empty)
            Next i
        End With
    End Sub

    Protected MustOverride Function getExtraResultInfo() As String
    Protected MustOverride Function getLineObject() As String
    Protected MustOverride Sub writeLine()
    Protected MustOverride ReadOnly Property nbReports() As Integer
    Protected MustOverride ReadOnly Property dataTableName() As String
    Protected MustOverride ReadOnly Property filePrefix() As String


    Protected ReadOnly Property fileName() As String
        Get
            If generatedFileName = "" Then
                generatedFileName = filePrefix & addZeros(Params.current.nextFileNumber, 5)
                Params.current.nextFileNumber += 1
                Params.current.save()
            End If

            Return generatedFileName
        End Get
    End Property

    Private Sub createFooter()
        outLine = New System.Text.StringBuilder()
        outLine.Append(LineType.FOOTER)

        'Blanc requis par le système
        writeSpaces(10)

        'Nombre total de rapports transmis   9(7)
        writeNumericField(nbProcessed, 7)

        outLine.AppendLine()

        out.Append(outLine.ToString)
    End Sub

    Public Sub createFile(ByVal pastFilesResults As Generic.List(Of FileResult))
        out = New System.Text.StringBuilder()
        Me.pastFilesResults = pastFilesResults

        createHeader()
        createBody()
        If _nbProcessed = 0 Then Exit Sub 'Only errors

        createFooter()

        saveFileResults()

        IO.File.WriteAllText(outPath & addSlash(outPath) & fileName & ".tomark", out.ToString(), System.Text.Encoding.GetEncoding("iso-8859-1"))
    End Sub

    Public Sub createTestFile()
        out = New System.Text.StringBuilder()
        createHeader()
        createFooter()
        Dim time As String = Date.Now.Hour & Date.Now.Minute & Date.Now.Second
        If time.Length > 4 Then
            time = time.Substring(time.Length - 4, 4)
        Else
            time = addZeros(time, 4)
        End If

        IO.File.WriteAllText(outPath & addSlash(outPath) & "test" & time & ".cst", out.ToString(), System.Text.Encoding.GetEncoding("iso-8859-1"))
    End Sub

    'TODO : Change back to private
    Public Sub saveFileResults()
        Dim xml As New System.Text.StringBuilder()
        xml.AppendLine("<" & Me.GetType().Name & ">")

        For Each curFR As FileResult In fileResults
            xml.Append(curFR.getXML())
        Next

        xml.AppendLine("</" & Me.GetType().Name & ">")

        IO.File.WriteAllText(outPath & addSlash(outPath) & fileName & ".save", xml.ToString())
    End Sub

    Private Sub loadFileResults(ByVal xmlFile As String)
        If xmlFile = String.Empty Then Exit Sub

        Dim data As New DataSet
        data.ReadXml(xmlFile)
        For Each curRow As DataRow In data.Tables(0).Rows
            fileResults.Add(New FileResult(curRow))
        Next
    End Sub

    ''' <summary>
    ''' Pursuit action when a folder reach it's 100th step report. It's create a copy of the current folder, move the futur RVs and create fake final/initial reports.
    ''' </summary>
    ''' <param name="noFolder"></param>
    ''' <remarks></remarks>
    Public Shared Function pursuitFolderWithNew(ByVal noFolder As Integer) As Integer
        Dim folderInfos As DataRow = Base.DBLinker.getInstance.readDBForGrid("InfoFolders", "*", "NoFolder=" & noFolder).Tables(0).Rows(0)
        Dim ftTable As DataTable = Base.DBLinker.getInstance.readDBForGrid("FolderTextes", "*", "NoFolder=" & noFolder & " AND NoMultiple=100").Tables(0)
        If ftTable.Rows.Count = 0 Then
            Throw New Exception("Text 'Rapport d'étape 100' of folder #" & noFolder & " not found. NoRef=" & folderInfos("NoRef"))
        End If
        Dim folderText As DataRow = ftTable.Rows(0)
        Dim folderTextDateStarted As Date = folderText("DateStarted")
        Dim finalDate As Date = folderTextDateStarted.AddDays(-2)
        Dim initialDate As Date = folderTextDateStarted.AddDays(-1)

        'Desactivate current folder
        Base.DBLinker.getInstance.updateDB("InfoFolders", "StatutOuvert=0", "NoFolder", noFolder, False)
        Base.DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient", 15 & "," & noFolder & "," & folderInfos("NoClient"), finalDate)

        'Create new folder as a copy of the current ensuring NoRef is empty
        Dim newNoFolder As Integer = 0
        Dim fields As String = String.Empty, values As String = String.Empty
        Dim fieldsSkipped As String() = {"NoFolder", "Remarques", "DateRef", "DateReceptionRef"}
        For Each curColumn As DataColumn In folderInfos.Table.Columns
            If Array.IndexOf(fieldsSkipped, curColumn.ColumnName) <> -1 Then Continue For
            If folderInfos(curColumn.ColumnName) Is DBNull.Value Then Continue For

            fields &= ",[" & curColumn.ColumnName & "]"
            values &= ",'" & folderInfos(curColumn.ColumnName).ToString.Replace("'", "''") & "'"
        Next
        fields = "DateReceptionRef,DateRef,Remarques" & fields
        'Both dates below are fake dates which are replaced later by first RV date of new folder
        values = "'2000/01/01','2000/01/01','Suite du dossier #" & noFolder & " --- " & folderInfos("Remarques").ToString().Replace("'", "''") & "'" & values
        Base.DBLinker.getInstance.writeDB("InfoFolders", fields, values, False, , , newNoFolder)

        'Write stat of new folder
        Base.DBHelper.writeStats("StatFolders", "NoAction, NoFolder, NoClient", "13," & newNoFolder & "," & folderInfos("NoClient"), folderTextDateStarted)

        'Create fake FinalReport text
        Base.DBLinker.getInstance.writeDB("FolderTextes", _
                                          "NoFolderTexteType, NoFolder, TexteTitle, DateStarted, DateFinished, Texte, TextePos, NoMultiple, IsTexte, ExternalStatus", _
                                          Params.current.textType_noFinalReport & "," & noFolder & ",'Rapport CSST final', '" & finalDate.ToString("yyyy-MM-dd") & "','" & finalDate.ToString("yyyy-MM-dd") & "','" & _
                                          "S:<BR>" & createTextArea("SOAP-S", "Le nombre de rapport d''étape a atteint cent et un nouveau dossier sera créé.") & "<br><br>O:<BR>" & createTextArea("SOAP-O", "") & "<br><br>A:<BR>" & createTextArea("SOAP-A", "") & "<br><br>P:<BR>" & createTextArea("SOAP-P", "") & _
                                          "',-1,1,1," & Params.current.markedAsNotProcessed)

        'Create fake InitialReport text
        Base.DBLinker.getInstance.writeDB("FolderTextes", _
                                          "NoFolderTexteType, NoFolder, TexteTitle, DateStarted, DateFinished, Texte, TextePos, NoMultiple, IsTexte, ExternalStatus", _
                                          Params.current.textType_noInitialReport & "," & newNoFolder & ",'Rapport CSST initial', '" & initialDate.ToString("yyyy-MM-dd") & "','" & initialDate.ToString("yyyy-MM-dd") & "','" & _
                                          "Justification par défaut : <INPUT TYPE=RADIO name=FolderFrequencyJustification VALUE=M CHECKED><br>A:<BR>" & createTextArea("Analyse", "Continuité du dossier " & folderInfos("NoRef") & ", car le nombre de rapport d''étape avait atteint cent dans le dossier précédent.") & "<br><br>B:<BR>" & createTextArea("Buts", "") & "<br><br>P:<BR>" & createTextArea("Plan", "") & _
                                          "',-1,1,1," & Params.current.markedAsNotProcessed)

        'Move futur RV between folders
        Base.DBLinker.getInstance.updateDB("InfoVisites", "NoFolder=" & newNoFolder, "NoFolder", noFolder & " AND DateHeure >= '" & folderTextDateStarted.ToString("yyyy-MM-dd") & "' AND ExternalStatus=" & Params.current.markedAsNotProcessed, False)

        'Set first RV as Evaluation
        Base.DBLinker.getInstance.updateDB("InfoVisites", "Evaluation=1,Confirmed=1", "NoVisite", "(SELECT TOP 1 NoVisite FROM InfoVisites WHERE NoFolder=" & newNoFolder & " ORDER BY DateHeure)", False, "IN")

        'Correct dates on new folder
        Base.DBLinker.getInstance.updateDB("InfoFolders", "DateReceptionRef=(SELECT TOP 1 DateHeure FROM InfoVisites WHERE NoFolder=" & newNoFolder & " AND Evaluation=1),DateRef=(SELECT TOP 1 DateHeure FROM InfoVisites WHERE NoFolder=" & newNoFolder & " AND Evaluation=1)", "NoFolder", newNoFolder, False)

        'Ensure at least one physio or ergo
        Dim isOneData As DataRow = Base.DBLinker.getInstance().readDBForGrid("SELECT (SELECT TOP 1 U.NoPermis FROM Utilisateurs U INNER JOIN Titres T ON T.NoTitre = U.NoTitre WHERE (T.Titre LIKE 'physio%' OR T.Titre LIKE 'ergo%') AND U.NoUser IN (SELECT NoTRP FROM InfoVisites IV WHERE IV.NoFolder = " & newNoFolder & ")) AS HasUser,(SELECT TOP 1 U.NoUser FROM Utilisateurs U INNER JOIN Titres T ON T.NoTitre = U.NoTitre WHERE (T.Titre LIKE 'physio%' OR T.Titre LIKE 'ergo%') AND U.NoUser IN (SELECT NoTRP FROM InfoVisites IV WHERE IV.NoFolder = " & noFolder & ")) AS NewNoUser, (SELECT TOP 1 NoTRP FROM InfoVisites WHERE NoFolder=" & newNoFolder & " AND Evaluation=1) AS OldNoUser, (SELECT TOP 1 NoVisite FROM InfoVisites WHERE NoFolder=" & newNoFolder & " AND Evaluation=1) AS NoVisite").Tables(0).Rows(0)
        If isOneData.Item(0) Is DBNull.Value Then
            'Write stat of change of TRP
            Dim users As DataRow = Base.DBLinker.getInstance().readDBForGrid("SELECT (SELECT TOP 1 Nom + ',' + Prenom + ' (' + CAST(NoUser AS VARCHAR(MAX)) + ')' FROM Utilisateurs WHERE NoUser=" & isOneData("OldNoUser") & ") AS OldUser,(SELECT TOP 1 Nom + ',' + Prenom + ' (' + CAST(NoUser AS VARCHAR(MAX)) + ')' FROM Utilisateurs WHERE NoUser=" & isOneData("NewNoUser") & ") AS NewUser").Tables(0).Rows(0)
            Base.DBLinker.getInstance().updateDB("InfoVisites", "NoTRP=" & isOneData("NewNoUser"), "NoVisite", isOneData("NoVisite"), False)
            Base.DBHelper.writeStats("StatVisites", "NoAction, NoFolder, NoClient, NoVisite, Comments", "34," & newNoFolder & "," & folderInfos("NoClient") & "," & isOneData("NoVisite") & ",'Modification dûe à la poursuite de ce dossier depuis le dossier #" & noFolder & " : De " & users("OldUser").ToString.Replace("'", "''") & " à " & users("NewUser").ToString.Replace("'", "''") & "'")
        End If

        'Modify/Adjust StepReport text and next alert
        Base.DBLinker.getInstance.updateDB("FolderTextes", "NoFolder=" & newNoFolder & ",TexteTitle='Rapport CSST d''étape ' + CAST((NoMultiple - 99) AS VARCHAR(MAX)),NoMultiple = NoMultiple - 99,ExternalStatus=" & Params.current.markedAsNotProcessed, "NoFolder", noFolder & " AND NoMultiple > 99 AND TexteTitle LIKE 'Rapport CSST d''étape%'", False)
        Base.DBLinker.getInstance.updateDB("UsersAlerts", _
                                           "Message=(select REPLACE(REPLACE(UA.Message, ' #" & noFolder & "', ' #' + CAST(FT.NoFolder AS VARCHAR(MAX)) + ' '), ' ' + CAST((FT.NoMultiple+99) AS VARCHAR(MAX)) + ' ', ' ' + CAST((FT.NoMultiple) AS VARCHAR(MAX)) + ' ') AS M from UsersAlerts UA, (select NoUserAlert,NoFolderTexte from FoldertexteAlerts where nofoldertexte in (select nofoldertexte from foldertextes where NoFolder=" & newNoFolder & ")) AS FTA, FolderTextes FT where UA.nouseralert =FTA.NoUserAlert AND FT.NoFolderTexte = FTA.NoFolderTexte AND UA.NoUserAlert=UsersAlerts.NoUserAlert)", _
                                           "NoUserAlert", _
                                           "(select NoUserAlert from FoldertexteAlerts where nofoldertexte in (select nofoldertexte from foldertextes where NoFolder=" & newNoFolder & "))", _
                                           False, "IN")
        Base.DBLinker.getInstance.updateDB("UsersAlerts", _
                                           "AlarmData=(select REPLACE(UA.AlarmData, ':" & folderInfos("NoClient") & ":" & noFolder & ":', ':" & folderInfos("NoClient") & ":' + CAST(FT.NoFolder AS VARCHAR(MAX)) + ':') AS M from UsersAlerts UA, (select NoUserAlert,NoFolderTexte from FoldertexteAlerts where nofoldertexte in (select nofoldertexte from foldertextes where NoFolder=" & newNoFolder & ")) AS FTA, FolderTextes FT where UA.nouseralert =FTA.NoUserAlert AND FT.NoFolderTexte = FTA.NoFolderTexte AND UA.NoUserAlert=UsersAlerts.NoUserAlert)", _
                                           "NoUserAlert", _
                                           "(select NoUserAlert from FoldertexteAlerts where nofoldertexte in (select nofoldertexte from foldertextes where NoFolder=" & newNoFolder & "))", _
                                           False, "IN")

        'Send intersoftware updates of ClientFolders & Agenda & RVs
        Base.InternalUpdatesManager.getInstance().sendUpdate("AccountsVisites(" & folderInfos("NoClient") & "," & noFolder & ")")
        Base.InternalUpdatesManager.getInstance().sendUpdate("AccountsVisites(" & folderInfos("NoClient") & "," & newNoFolder & ")")
        Base.InternalUpdatesManager.getInstance().sendUpdate("AccountsBills(" & folderInfos("NoClient") & ")")
        Base.InternalUpdatesManager.getInstance().sendUpdate("Agendas()")
        Base.InternalUpdatesManager.getInstance().sendUpdate("AccountsDossierTextBoxes(" & folderInfos("NoClient") & "," & noFolder & ")")
        Base.InternalUpdatesManager.getInstance().sendUpdate("AccountsDossiers(" & folderInfos("NoClient") & ")")
        Dim alertsAffected As DataSet = Base.DBLinker.getInstance().readDBForGrid("SELECT NoUser, NoUserAlert FROM UsersAlerts WHERE NoUserAlert IN (select NoUserAlert from FoldertexteAlerts where nofoldertexte in (select nofoldertexte from foldertextes where NoFolder=" & newNoFolder & "))")
        If alertsAffected IsNot Nothing AndAlso alertsAffected.Tables.Count <> 0 Then
            For Each curRow As DataRow In alertsAffected.Tables(0).Rows
                Base.InternalUpdatesManager.getInstance().sendUpdate("Alerts(" & curRow("NoUser") & "," & curRow("NoUserAlert") & ")")
            Next
        End If

        Return newNoFolder
    End Function

    Private Shared Function createTextArea(ByVal name As String, ByVal text As String) As String
        Return "<textarea name=" & name & ">" & text & "</textarea>"
    End Function
End Class
