Public Class FileInitialReports
    Inherits FileReports

    Public Const FILE_PREFIX As String = "rin"
    Private Const ERROR_MODEL_MISSING As String = "Le rapport ne contient pas le modèle de texte lié au rapport initial CSST. Veuillez appliquer le modèle correspondant à ce texte et le remplir adéquatement."

    Public Sub New(ByVal xmlFile As String)
        MyBase.New(xmlFile)
    End Sub

    Public Sub New(ByVal outPath As String, ByVal data As DataSet)
        MyBase.New(outPath, data)
    End Sub

    Private _nbReports As Integer = 0

    Protected Overrides ReadOnly Property filePrefix() As String
        Get
            Return FILE_PREFIX
        End Get
    End Property

    Protected Overrides Sub writeLine()
        If curRow("TakenInChargeDate") Is DBNull.Value Then
            Throw New FieldValidationException("Il n'existe pas d'évaluation pour ce dossier. Veuillez changer le type du premier rendez-vous pour ""Évaluation"".", True)
        End If

        If curRow("OtherFolderWithSameNoRefNotCompleted") <> 0 Then
            errors.Add(New FieldValidationException("En attente d'un dossier précédent ayant un numéro de référence identique et dont tous les textes n'ont pas été confirmés.", False, CsstTask.RESULT_INFO_COLOR))
        End If

        Dim takenInChargeDate As Date = curRow("TakenInChargeDate")

        Dim analyze As String = "", goal As String = "", plan As String = ""
        
        'Ensure FolderTexte is based upon the good model
        Dim modelMissingAnalyze As Boolean = curRow("Analyze") Is DBNull.Value OrElse curRow("Analyze") Is Nothing OrElse curRow("Analyze").ToString = "FILLEDBYTEXT"
        Dim modelMissingGoal As Boolean = curRow("Goal") Is DBNull.Value OrElse curRow("Goal") Is Nothing OrElse curRow("Goal").ToString = "FILLEDBYTEXT"
        Dim modelMissingPlan As Boolean = curRow("Plan") Is DBNull.Value OrElse curRow("Plan") Is Nothing OrElse curRow("Plan").ToString = "FILLEDBYTEXT"
        If modelMissingAnalyze OrElse modelMissingGoal OrElse modelMissingPlan Then
            errors.Add(New FieldValidationException(ERROR_MODEL_MISSING, True))
        Else
            analyze = curRow("Analyze")
            goal = curRow("Goal")
            plan = curRow("Plan")

            If analyze = String.Empty AndAlso goal = String.Empty AndAlso plan = String.Empty Then
                errors.Add(New FieldValidationException("Les champs ""Analyse"", ""But"" et ""Plan de traitement"" sont manquants.", True))
            End If
        End If

        Dim receptionOfOrdonnanceDate As Date = takenInChargeDate
        If curRow.Table.Columns.Contains("FolderReceptionOfOrdonnanceDate") = False OrElse curRow("FolderReceptionOfOrdonnanceDate") Is DBNull.Value Then
            errors.Add(New FieldValidationException("La date de réception de l'ordonnance (Date de réception de la référence) est manquante.", True))
        Else
            receptionOfOrdonnanceDate = curRow("FolderReceptionOfOrdonnanceDate")
        End If
        Dim ordonnanceDate As Date = takenInChargeDate
        If curRow.Table.Columns.Contains("FolderOrdonnanceDate") = False OrElse curRow("FolderOrdonnanceDate") Is DBNull.Value Then
            errors.Add(New FieldValidationException("La date de l'ordonnance (Date de référence) est manquante.", True))
        Else
            ordonnanceDate = curRow("FolderOrdonnanceDate")
        End If
        Dim eventDate As Date = takenInChargeDate
        If curRow.Table.Columns.Contains("FolderEventDate") = False OrElse curRow("FolderEventDate") Is DBNull.Value Then
            errors.Add(New FieldValidationException("La date d'événement (Date d'accident) est manquante.", True))
        Else
            eventDate = curRow("FolderEventDate")
        End If

        outLine.Append(LineType.INITIAL)

        writeClientInfo()

        'Date de rechute   SSAAMMJJ  9(8) - Optionel
        writeSpaces(8)

        writeMiddleInfos()

        'Date de réception de l'ordonnance   SSAAMMJJ  9(8)
        If receptionOfOrdonnanceDate.Date > Date.Today Then errors.Add(New FieldValidationException("La date de réception de l'ordonnance (Date de réception de la référence) ne peut pas être dans le futur.", True))
        If receptionOfOrdonnanceDate.Date > takenInChargeDate.Date Then errors.Add(New FieldValidationException("La date de réception de l'ordonnance (Date de réception de la référence) doit être inférieure ou égale à la date de prise en charge (Date d'évaluation).", True))
        If receptionOfOrdonnanceDate.Date < eventDate.Date Then errors.Add(New FieldValidationException("La date de réception de l'ordonnance (Date de réception de la référence) doit être supérieure ou égale à la date d'événement (Date d'accident).", True))
        If receptionOfOrdonnanceDate.Date < ordonnanceDate.Date Then errors.Add(New FieldValidationException("La date de réception de l'ordonnance (Date de réception de la référence) doit être supérieure ou égale à la date de l'ordonnance (Date de référence).", True))
        writeDateField(receptionOfOrdonnanceDate)

        'Date de prise en charge   SSAAMMJJ  9(8)
        If takenInChargeDate.Date > Date.Today Then errors.Add(New FieldValidationException("La date de prise en charge (Date d'évaluation) ne peut pas être dans le futur.", True))
        If takenInChargeDate.Date < eventDate.Date Then errors.Add(New FieldValidationException("La date de prise en charge (Date d'évaluation) doit être supérieure ou égale à la date d'événement (Date d'accident).", True))
        writeDateField(takenInChargeDate)

        'Durée prévue ou pres- crite des traitements   9(3) - Optionel
        writeSpaces(3)

        'Date de début des traitements   SSAAMMJJ  9(8)
        Dim firstTreamentDate As Date = writeFirstTreatmentDate(takenInChargeDate)

        writeFolderFrequency()

        'Problèmes identifiés, but et plan de traitement   X(790)
        Dim goalAndPlan As String = String.Empty
        Dim textsLength As Double = analyze.Length + goal.Length + plan.Length
        Dim a, b, p As Integer
        If textsLength > 770 Then
            Dim minChar As Integer = 770.0 / 6

            a = If(analyze.Length < minChar, analyze.Length, minChar)
            b = If(goal.Length < minChar, goal.Length, minChar)
            p = If(plan.Length < minChar, plan.Length, minChar)

            Dim ratio As Double = (textsLength - (a + b + p)) / (770.0 - (a + b + p))
            analyze = analyze.Substring(0, Math.Floor((analyze.Length - a) / ratio + a))
            goal = goal.Substring(0, Math.Floor((goal.Length - b) / ratio + b))
            plan = plan.Substring(0, Math.Floor((plan.Length - p) / ratio + p))
        End If
        goalAndPlan = "A: " & analyze & vbTab & "B: " & goal & vbTab & "P: " & plan
        writeTextField(goalAndPlan, 790)

        writeEndInfos()

        _nbReports += 1
    End Sub

    Protected Overrides ReadOnly Property nbReports() As Integer
        Get
            Return _nbReports
        End Get
    End Property

    Protected Overrides ReadOnly Property dataTableName() As String
        Get
            Return "initials"
        End Get
    End Property

    Protected Overrides Function getExtraResultInfo() As String
        Return String.Empty
    End Function
End Class
