Public MustInherit Class FileReports
    Inherits File

    Private Const ERROR_MODEL_MISSING As String = "Le rapport ne contient pas le modèle de texte lié au TEXT_TITLE. Veuillez appliquer le modèle correspondant à ce texte et le remplir adéquatement."

    Public Sub New(ByVal xmlFile As String)
        MyBase.New(xmlFile)
    End Sub

    Public Sub New(ByVal outPath As String, ByVal data As DataSet)
        MyBase.New(outPath, data)
    End Sub

    Protected Function writeFirstTreatmentDate(ByVal takenInChargeDate As Date)
        Dim firstTreamentDate As Date = takenInChargeDate.AddDays(1)
        If curRow.Table.Columns.Contains("FirstTreamentDate") = False OrElse curRow("FirstTreamentDate") Is DBNull.Value Then
            If curRow("ClosingDate") Is DBNull.Value Then
                errors.Add(New FieldValidationException("En attente de la présence du premier traitement.", False, CsstTask.RESULT_INFO_COLOR))
            Else
                Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsRefused, "NoFolder", curRow("NoFolder"), False)
                errors.Add(New FieldValidationException("Le dossier a été changé de statut à inactif et donc le module CSST a modifié le statut externe du dossier à ""Refusé"". Ainsi, le dossier ne sera plus traité par le processus et tout doit être envoyé manuellement à la CSST. (Cette action automatique s'est produite uniquement parce qu'il n'y a pas de premier traitement.)", False, CsstTask.RESULT_INFO_COLOR))
            End If
        Else
            firstTreamentDate = curRow("FirstTreamentDate")
            If firstTreamentDate.Date > Date.Today Then errors.Add(New FieldValidationException("La date du premier traitement ne peut pas être dans le futur.", True))
            If firstTreamentDate.Date <= takenInChargeDate.Date Then errors.Add(New FieldValidationException("La date du premier traitement doit être supérieure à la date de prise en charge (Date d'évaluation).", True))
        End If
        writeDateField(firstTreamentDate)

        Return firstTreamentDate
    End Function

    Protected Function getSoap(ByVal maxLength As Integer) As String
        Dim soap_s As String = "", soap_o As String = "", soap_a As String = "", soap_p As String = ""
        
        Dim textTitle As String = getLineObject()
        textTitle = textTitle.Substring(0, 1).ToLower() & textTitle.Substring(1)

        'Ensure FolderTexte is based upon the good model
        Dim modelMissingSoapS As Boolean = curRow("SoapS") Is DBNull.Value OrElse curRow("SoapS") Is Nothing OrElse curRow("SoapS").ToString = "FILLEDBYTEXT"
        Dim modelMissingSoapO As Boolean = curRow("SoapO") Is DBNull.Value OrElse curRow("SoapO") Is Nothing OrElse curRow("SoapO").ToString = "FILLEDBYTEXT"
        Dim modelMissingSoapA As Boolean = curRow("SoapA") Is DBNull.Value OrElse curRow("SoapA") Is Nothing OrElse curRow("SoapA").ToString = "FILLEDBYTEXT"
        Dim modelMissingSoapP As Boolean = curRow("SoapP") Is DBNull.Value OrElse curRow("SoapP") Is Nothing OrElse curRow("SoapP").ToString = "FILLEDBYTEXT"
        If modelMissingSoapS OrElse modelMissingSoapO OrElse modelMissingSoapA OrElse modelMissingSoapP Then
            errors.Add(New FieldValidationException(ERROR_MODEL_MISSING.Replace("TEXT_TITLE", textTitle), True))
        Else
            soap_s = curRow("SoapS")
            soap_o = curRow("SoapO")
            soap_a = curRow("SoapA")
            soap_p = curRow("SoapP")

            If soap_s = String.Empty AndAlso soap_o = String.Empty AndAlso soap_a = String.Empty AndAlso soap_p = String.Empty Then
                errors.Add(New FieldValidationException("Les champs du SOAP sont manquants.", True))
            End If
        End If

        Dim textsLength As Double = soap_s.Length + soap_o.Length + soap_a.Length + soap_p.Length
        Dim s, o, a, p As Integer
        If textsLength > maxLength Then
            Dim minChar As Integer = maxLength >> 3

            s = If(soap_s.Length < minChar, soap_s.Length, minChar)
            o = If(soap_o.Length < minChar, soap_o.Length, minChar)
            a = If(soap_a.Length < minChar, soap_a.Length, minChar)
            p = If(soap_p.Length < minChar, soap_p.Length, minChar)

            Dim ratio As Double = (textsLength - (s + o + a + p)) / (maxLength - (s + o + a + p))
            soap_s = soap_s.Substring(0, Math.Floor((soap_s.Length - s) / ratio + s))
            soap_o = soap_o.Substring(0, Math.Floor((soap_o.Length - o) / ratio + o))
            soap_a = soap_a.Substring(0, Math.Floor((soap_a.Length - a) / ratio + a))
            soap_p = soap_p.Substring(0, Math.Floor((soap_p.Length - p) / ratio + p))
        End If

        Return "S: " & soap_s & vbTab & "O: " & soap_o & vbTab & "A: " & soap_a & vbTab & "P: " & soap_p
    End Function

    Protected Sub writeClientInfo()
        Dim country As String = curRow("ClientCountry")
        Dim isDefaultCountry As Boolean = country.ToUpper = DEFAULT_COUNTRY

        'Code de traitement (P : Physiothérapie    E:      Ergothérapie)
        Dim service As String = curRow("FolderService").ToString.ToUpper()
        If service.Substring(0, 1) <> "P" AndAlso service.Substring(0, 1) <> "E" Then
            errors.Add(New FieldValidationException("Le service du dossier doit commencé par P ou E pour Physiothérapie et Ergothérapie respectivement.", True))
        End If
        writeTextField(service, 1)

        writeNoFolderField()

        'Nom du travailleur   X(30)
        writeTextField(curRow("ClientLastName"), 30)

        'Prénom du travailleur   X(20)
        writeTextField(curRow("ClientFirstName"), 20)

        'No civique    X(09) - Optionel
        writeSpaces(9)

        'Nom de la rue   X(30)  - Optionel
        writeSpaces(30)

        'Ville ou municipalité    X(40) - Optionel
        writeSpaces(40)

        'Province/État   X(10) 
        Dim province As String = curRow("ClientProvinceState")
        writeProvinceField(province, isDefaultCountry)

        'Code postal   X(6)
        writePostalCode(curRow("ClientPostalCode"), "ClientPostalCode", isDefaultCountry, province)

        'Zip-code ou autre   X(7)
        Dim zipCode As String = curRow("ClientZipCode")
        If isDefaultCountry AndAlso zipCode <> "" Then zipCode = ""
        writeTextField(zipCode, 7)

        'Pays   X(10)
        writeTextField(country, 10)

        'App., Suite, C.P.   X(15) - Optionel
        writeSpaces(15)

        'NAM  X(12)
        Dim nam As String = curRow("ClientNAM")
        If validateNAM(nam, String.Empty, String.Empty) = False Then errors.Add(New FieldValidationException("NAM invalide.", True))
        writeTextField(nam, 12)

        'NAS   9(9) - Optionel
        writeSpaces(9)

        writeEventDate()
    End Sub

    Protected Sub writeMiddleInfos()
        'Date de l'ordonnance   SSAAMMJJ  9(8)
        If curRow("FolderOrdonnanceDate") Is DBNull.Value Then
            errors.Add(New FieldValidationException("La date de l'ordonnance (Date de référence) est manquante.", True))
        Else
            Dim ordonnanceDate As Date = curRow("FolderOrdonnanceDate")
            Dim eventDate As Date = ordonnanceDate
            If Not (curRow.Table.Columns.Contains("FolderEventDate") = False OrElse curRow("FolderEventDate") Is DBNull.Value) Then
                eventDate = curRow("FolderEventDate")
            End If
            If ordonnanceDate > Date.Today Then errors.Add(New FieldValidationException("La date de l'ordonnance (Date de référence) ne peut être dans le futur.", True))
            If ordonnanceDate < eventDate Then errors.Add(New FieldValidationException("La date de l'ordonnance (Date de référence) doit être supérieure ou égale à la date d'événement (Date d'accident).", True))
            writeDateField(ordonnanceDate)
        End If

        'No de l'établissement de santé   9(8)
        writeNumericField(clinicData("ClinicNoCSST"), 8)

        'Nom de l’établissement de santé   X(40)
        writeTextField(clinicData("ClinicName"), 40)

        If curRow("NoFolder") = 33414 Then
            Dim a As Byte = 0
        End If

        'No du médecin   9(6)
        If curRow("MedecinNoRef") Is DBNull.Value Then
            errors.Add(New FieldValidationException("Le numéro de référence du médecin associé au dossier est manquant.", True))
        Else
            Dim medecinNoRef As String = curRow("MedecinNoRef")
            medecinNoRef = System.Text.RegularExpressions.Regex.Replace(medecinNoRef, "[^0-9]", "")
            If medecinNoRef.StartsWith("1") = False Then
                errors.Add(New FieldValidationException("Le numéro de référence du médecin associé au dossier ne débute pas par le chiffre un.", True))
            ElseIf medecinNoRef.Length <> 6 Then
                errors.Add(New FieldValidationException("Le numéro de référence du médecin associé au dossier ne contient pas exactement six chiffres.", True))
            Else
                writeNumericField(curRow("MedecinNoRef"), 6)
            End If
        End If

        'Nom du médecin   X(30) - Optionel
        writeSpaces(30)

        'Prénom du médecin   X(20) - Optionel
        writeSpaces(20)

        'Diagnostic   X(316)
        If curRow("FolderDiagnostic") IsNot DBNull.Value AndAlso curRow("FolderDiagnostic") <> "" Then
            writeTextField(curRow("FolderDiagnostic"), 316)
        Else
            errors.Add(New FieldValidationException("Le diagnostic est manquant.", True))
        End If
    End Sub

    Protected Sub writeFolderFrequency()
        'Fréquence des traitements   9(1)
        Dim frequency As Integer = curRow("FolderFrequency")
        If frequency < 1 OrElse frequency > 7 Then errors.Add(New FieldValidationException("La fréquence du dossier doit être entre 1 et 7.", True))
        writeNumericField(frequency, 1)

        'Justification de fréquence   X(1) (M: Maintien,C:      Contrôle(),A:      Approche(particulière),I:      Contre(-indication),O : Ordonnance du médecin)
        Dim frequencyJustification As String = ""
        If curRow("FolderFrequencyJustification") IsNot DBNull.Value Then frequencyJustification = curRow("FolderFrequencyJustification")
        If frequency > 2 Then
            frequencyJustification = ""
        ElseIf frequencyJustification = "" Then
            errors.Add(New FieldValidationException("La fréquence doit être justifiée lorsqu'elle est inférieure à trois.", True))
        End If

        writeTextField(frequencyJustification, 1)
    End Sub

    Protected Sub writeEndInfos()
        'Numéro du physiothérapeute /  ergothérapeute ou du médecin   X(8)
        If curRow("UserNoRef") Is DBNull.Value Then
            errors.Add(New FieldValidationException("Il n'existe pas de thérapeute dans les rendez-vous du dossier ayant pour titre Physiothérapeute ou Ergothérapeute.", True))
        Else
            Dim userNoRef As String = curRow("UserNoRef")
            Dim userNameId As String = curRow("UserNameId")
            userNoRef = userNoRef.ToUpper()
            If System.Text.RegularExpressions.Regex.IsMatch(userNoRef, "^(1[0-9]{5,6}|[0-9]{8}|(PHY|ERG|DUP)[0-9]{5})$") = False Then errors.Add(New FieldValidationException("Le numéro de permis du thérapeute n'est pas valide : " & userNoRef & " de " & userNameId & "<br/>- Exemples valides : PHY et 5 chiffres, ERG et 5 chiffres, DUP et 5 chiffres, 8 chiffres, 1 et (5 ou 6 chiffres).", True))
            If curRow("FolderService").ToString().StartsWith("P") AndAlso userNoRef.StartsWith("ERG") = True Then
                errors.Add(New FieldValidationException("Le numéro de permis du thérapeute ne correspond pas au service offert """ & curRow("FolderService") & """ : " & userNoRef & " de " & userNameId & "<br/>- Exemples valides : PHY et 5 chiffres, DUP et 5 chiffres, 8 chiffres, 1 et (5 ou 6 chiffres).", True))
            End If
            If curRow("FolderService").ToString().StartsWith("E") AndAlso (userNoRef.StartsWith("PHY") = True OrElse userNoRef.StartsWith("DUP") = True) Then
                errors.Add(New FieldValidationException("Le numéro de permis du thérapeute ne correspond pas au service offert """ & curRow("FolderService") & """ : " & userNoRef & " de " & userNameId & "<br/>- Exemples valides : ERG et 5 chiffres, 8 chiffres, 1 et (5 ou 6 chiffres).", True))
            End If
            writeTextField(userNoRef, 8)
        End If

        'Date de signature   SSAAMMJJ  9(8)
        If curRow("SignatureDate") IsNot DBNull.Value Then
            Dim signatureDate As Date = curRow("SignatureDate")

            If signatureDate.Date > Date.Today Then errors.Add(New FieldValidationException("La date de signature (Informations dossier->Date du premier traitement) ne doit pas être dans le futur.", True))
            writeDateField(signatureDate)
        End If

        'Add a line change
        outLine.AppendLine()
    End Sub

    Protected MustOverride Overrides ReadOnly Property nbReports() As Integer
    Protected MustOverride Overrides ReadOnly Property dataTableName() As String
    Protected MustOverride Overrides Sub writeLine()

    Protected Overrides Function getLineObject() As String
        Return curRow("TextTitle")
    End Function
End Class
