Public Class FilePresences
    Inherits File


    Public Const FILE_PREFIX As String = "rpe"
    Private _nbReports As Integer = 0
    Private data As DataSet
    Private currentClientNbRVSent As Integer = 0
    Private currentClientNbPresenceSent As Integer = 0

    Public Sub New(ByVal xmlFile As String)
        MyBase.New(xmlFile)
    End Sub

    Public Sub New(ByVal outPath As String, ByVal data As DataSet)
        MyBase.New(outPath, data)
        Me.data = data
    End Sub

    Protected Overrides ReadOnly Property filePrefix() As String
        Get
            Return FILE_PREFIX
        End Get
    End Property

    Protected Overrides Sub writeLine()
        currentClientNbPresenceSent = 0
        currentClientNbRVSent = 0

        If isAlreadyAnError(curRow("NoFolder")) Then
            errors.Add(New FieldValidationException("Ce dossier a des erreurs dans les autres types de rapport. Veuillez les corriger avant de pouvoir envoyer les présences.", True))
            Exit Sub
        End If

        Dim firstPeriod As Date = Date.Today
        If curRow("FirstPeriod") Is DBNull.Value Then
            'This error have to be thrown and not added because it's an elementary information
            Throw New FieldValidationException("Il n'existe aucune présence pour ce dossier.", True)
        Else
            firstPeriod = curRow("FirstPeriod")
        End If
        Dim lastPeriod As Date = curRow("LastPeriod")

        If curRow("IsEvalDate") = False Then
            Throw New FieldValidationException("La première présence du dossier doit être de type 'Évaluation'.", True)
        End If

        Dim curFirstDate As Date = firstPeriod
        Dim curLastDate As Date = firstPeriod
        While True
            'Set last date
            If curLastDate.Month = lastPeriod.Month Then
                curLastDate = lastPeriod
            Else
                curLastDate = curLastDate.AddMonths(1)
                curLastDate = New Date(curLastDate.Year, curLastDate.Month, 1)
                curLastDate = curLastDate.AddDays(-1) 'Go back current month on last day
            End If

            writeMonthLine(curFirstDate, curLastDate)

            'Set firstDate and temp last date
            If curLastDate <> lastPeriod Then
                curFirstDate = curLastDate.AddDays(1)
                curLastDate = curFirstDate
            Else
                Exit While
            End If
        End While

        'If not RV to send
        If currentClientNbRVSent = 0 Then Throw New FilesCreationSkippingException()
    End Sub

    Private Sub writeMonthLine(ByVal firstPeriod As Date, ByVal lastPeriod As Date)
        Dim clinicCountry As String = clinicData("ClinicCountry")
        Dim isDefaultCountry As Boolean = clinicCountry.ToUpper = DEFAULT_COUNTRY

        'Prepare daysString and if no RV, than quit method (This is possible when a large period is given and a month in this is empty... Rare, but possible)
        Dim noRVs As String = String.Empty
        Dim noFolder As String = curRow("NoFolder")
        Dim rvs() As DataRow = data.Tables("rvs").Select("NoFolder=" & noFolder & " AND RVDate>='" & firstPeriod.ToString("yyyy-MM-dd") & "' AND RVDate<'" & lastPeriod.AddDays(1).ToString("yyyy-MM-dd") & "'")
        Dim days(30) As String
        For i As Byte = 0 To 30
            days(i) = " "
        Next i
        For Each rv As DataRow In rvs
            If rv("RVStatus") = "X" Then
                errors.Add(New FieldValidationException("Le rendez-vous au " & CType(rv("RVDate"), Date).ToString("yyyy-MM-dd HH:mm") & " est encore au statut de rendez-vous. Veuillez changer son statut pour envoyer les présences de ce dossier.", True))
            Else
                currentClientNbRVSent += 1
                If rv("RVStatus") = "P" OrElse rv("RVStatus") = "I" Then currentClientNbPresenceSent += 1
                days(CType(rv("RVDate"), Date).Day - 1) = rv("RVStatus")
                noRVs &= "," & rv("NoRV")
            End If
        Next
        Dim daysString As String = String.Join("", days)
        If daysString.Trim = String.Empty Then Exit Sub
        If noRVs <> String.Empty Then
            If curRow("NoRVsList") = String.Empty Then noRVs = noRVs.Substring(1)
            curRow("NoRVsList") &= noRVs
        End If

        'Write line code
        outLine.Append(LineType.PRESENCES)

        'Code de traitement (P : Physiothérapie    E:      Ergothérapie)
        writeTextField(curRow("FolderService"), 1)

        'Période de présence « du »   SSAAMMJJ  9(8)
        If firstPeriod.Date > lastPeriod.Date Then errors.Add(New FieldValidationException("La date de début de période doit être inférieure ou égale à la date de fin de période.", True))
        writeDateField(firstPeriod)

        'Période de présence « au »   SSAAMMJJ  9(8)
        writeDateField(lastPeriod)

        'No de l'établissement de santé   9(8)
        writeNumericField(clinicData("ClinicNoCSST"), 8)

        'Nom de l'établisse-ment de santé   X(40)
        writeTextField(clinicData("ClinicName"), 40)

        'No civique (adresse de l'établissement)   X(09) - Optionel
        writeSpaces(9)

        'Nom de la rue (adresse de l'établissement)   X(30) - Optionel
        writeSpaces(30)

        'Ville ou municipalité (adresse de l'établissement)   X(40) - Optionel
        writeSpaces(40)

        'Province /État (adresse de l'établissement)   X(10)
        Dim province As String = clinicData("ClinicProvinceState")
        writeProvinceField(province, isDefaultCountry)

        'Code postal  (adresse de l'établissement)   X(6)
        writePostalCode(clinicData("ClinicPostalCode"), "ClinicPostalCode", isDefaultCountry, province)

        'Zip-code ou autre (adresse de l'établissement)   X(7)
        Dim zipCode As String = clinicData("ClinicZipCode")
        If isDefaultCountry AndAlso zipCode <> "" Then zipCode = ""
        writeTextField(zipCode, 7)

        'Pays (adresse de l'établissement)   X(10)
        writeTextField(clinicCountry, 10)

        'App., Suite, C.P. (adresse de l'établissement)   X(15) - Optionel
        writeSpaces(15)

        'Nom du travailleur   X(30)
        writeTextField(curRow("ClientLastName"), 30)

        'Prénom du travailleur   X(20)
        writeTextField(curRow("ClientFirstName"), 20)

        'NAM  X(12)
        Dim nam As String = curRow("ClientNAM")
        If validateNAM(nam, String.Empty, String.Empty) = False Then errors.Add(New FieldValidationException("NAM invalide.", True))
        writeTextField(nam, 12)

        writeEventDate()

        'Date de réception de l'ordonnance   SSAAMMJJ  9(8) - Optionel
        writeSpaces(8)

        'Date de début des traitements   SSAAMMJJ  9(8)
        Dim firstTreamentDate As Date = Date.Today
        If curRow("FirstTreamentDate") Is DBNull.Value Then
            errors.Add(New FieldValidationException("En attente de la présence du premier traitement.", False, CsstTask.RESULT_INFO_COLOR))
        Else
            firstTreamentDate = curRow("FirstTreamentDate")
        End If
        If firstTreamentDate.Date > Date.Today Then errors.Add(New FieldValidationException("La date du premier traitement ne peut pas être dans le futur.", True))
        writeDateField(firstTreamentDate)

        'Code de présences   X(31)
        outLine.Append(daysString)

        'Nbre jours présence   9(2) - Optionel
        writeNumericField(rvs.Length, 2)

        writeNoFolderField()

        outLine.AppendLine()

        _nbReports += 1
    End Sub

    Protected Overrides ReadOnly Property nbReports() As Integer
        Get
            Return _nbReports
        End Get
    End Property

    Protected Overrides ReadOnly Property dataTableName() As String
        Get
            Return "presences"
        End Get
    End Property

    Protected Overrides Function getLineObject() As String
        Return "Présences"
    End Function

    Protected Overrides Function getExtraResultInfo() As String
        Return "pour " & currentClientNbRVSent & " rendez-vous dont " & currentClientNbPresenceSent & " présences"
    End Function
End Class
