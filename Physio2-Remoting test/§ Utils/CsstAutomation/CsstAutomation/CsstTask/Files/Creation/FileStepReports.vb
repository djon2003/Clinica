Public Class FileStepReports
    Inherits FileReports

    Public Const FILE_PREFIX As String = "rap"
    Private _nbReports As Integer = 0

    Public Sub New(ByVal xmlFile As String)
        MyBase.New(xmlFile)
    End Sub

    Public Sub New(ByVal outPath As String, ByVal data As DataSet)
        MyBase.New(outPath, data)
    End Sub


    Protected Overrides Sub writeLine()
        If curRow("TakenInChargeDate") Is DBNull.Value Then
            Throw New FieldValidationException("Il n'existe pas d'évaluation pour ce dossier. Veuillez changer le type du premier rendez-vous pour ""Évaluation"".", True)
        End If

        outLine.Append(LineType.STEP)

        writeClientInfo()

        'Date de début des traitements   SSAAMMJJ  9(8)
        Dim takenInChargeDate As Date = curRow("TakenInChargeDate")
        Dim firstTreamentDate As Date = writeFirstTreatmentDate(takenInChargeDate)

        'Validation de la date d'événement (accident) VS firstTreament date
        Dim eventDate As Date = firstTreamentDate.AddDays(-1)
        If curRow("FolderEventDate") IsNot DBNull.Value Then eventDate = curRow("FolderEventDate")
        If firstTreamentDate.Date <= eventDate Then errors.Add(New FieldValidationException("La date d'événement (Date d'accident) ne peut pas être supérieure au premier traitement.", True))

        writeMiddleInfos()

        'Numéro d'étape   9(2)
        Dim noStep As Integer = curRow("TextNoStep")
        If noStep = 100 Then
            Dim newNoFolder As Integer = pursuitFolderWithNew(curRow("NoFolder"))
            'Throwing an error is necessary to ensure modified that are reselected from DB and it allows the user to see the message below so he knows what happened
            Throw New FilesCreationException(PURSUIT_REASON.Replace("OLD_FOLDER", curRow("NoFolder")).Replace("NEW_FOLDER", newNoFolder) & "Le processus doit être relancé manuellement pour poursuivre étant donné la nature spéciale de l'erreur.")
        End If
        writeNumericField(noStep, 2)

        'Nombre total de traitements à ce jour   9(2)
        writeNumericField(curRow("FolderTotalTreaments"), 2)

        writeFolderFrequency()

        'Évolution    X(1) (A : Amélioré,S:      Stable,D:      Détérioré)
        If curRow("Evolution") Is DBNull.Value Then
            errors.Add(New FieldValidationException("Le statut d'évolution du patient (Inclus dans le modèle de texte Rapport CSST d'étape) est manquant.", True))
        Else
            writeTextField(curRow("Evolution"), 1)
        End If


        'Les traitements ont-ils été suspendus?   X(1) (O: Oui, N: Non)
        Dim treatmentsSuspended As Boolean = False
        If curRow("TreamentsSuspended") Is DBNull.Value Then
            errors.Add(New FieldValidationException("L'indication si les traitements ont été suspendus (Inclus dans le modèle de texte Rapport CSST d'étape) est manquante.", True))
        Else
            If curRow("TreamentsSuspended") <> "FILLEDBYTEXT" Then
                treatmentsSuspended = curRow("TreamentsSuspended")
                writeTextField(If(treatmentsSuspended, "O", "N"), 1)
            End If
        End If

        'Date de suspension   SSAAMMJJ  9(8)
        Dim strSuspensionDate As String = ""
        If curRow.Table.Columns("FolderSuspendedTreamentDate") IsNot Nothing AndAlso curRow("FolderSuspendedTreamentDate") IsNot Nothing AndAlso curRow("FolderSuspendedTreamentDate") IsNot DBNull.Value AndAlso curRow("FolderSuspendedTreamentDate") <> "FILLEDBYTEXT" Then strSuspensionDate = curRow("FolderSuspendedTreamentDate")
        If strSuspensionDate = "" Then
            If treatmentsSuspended Then errors.Add(New FieldValidationException("La date de suspension des traitements doit être présente lorsqu'ils ont été suspendus.", True))
            writeSpaces(8)
        Else
            Dim suspensionDate As Date = Date.Parse(strSuspensionDate)
            If suspensionDate.Date > Date.Today Then errors.Add(New FieldValidationException("La date de suspension des traitements ne doit pas être dans le futur.", True))
            writeDateField(firstTreamentDate)
        End If

        'Durée supplémentaire prévue ou prescrite des traitements   9(3) - Optionel
        writeSpaces(3)

        'SOAP   X(474)
        writeTextField(getSoap(454), 474)

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
            Return "steps"
        End Get
    End Property

    Protected Overrides ReadOnly Property filePrefix() As String
        Get
            Return FILE_PREFIX
        End Get
    End Property

    Protected Overrides Function getExtraResultInfo() As String
        Return String.Empty
    End Function
End Class
