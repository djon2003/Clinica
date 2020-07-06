Imports CI.Base

Public Class DataSelector

    Private Const SELECT_TRP_NAME_ID As String = "(SELECT TOP 1 U.Nom + ',' + U.Prenom + ' (' + CAST(U.NoUser AS VARCHAR(MAX)) + ')' FROM Utilisateurs U INNER JOIN Titres T ON T.NoTitre = U.NoTitre INNER JOIN InfoVisites IV ON IV.NoTRP = U.NoUser WHERE (T.Titre LIKE 'physio%' OR T.Titre LIKE 'ergo%') AND IV.NoFolder = IF1.NoFolder ORDER BY IV.DateHeure) AS UserNameId"
    Private Const SELECT_TRP_NOREF As String = "(SELECT TOP 1 U.NoPermis FROM Utilisateurs U INNER JOIN Titres T ON T.NoTitre = U.NoTitre INNER JOIN InfoVisites IV ON IV.NoTRP = U.NoUser WHERE (T.Titre LIKE 'physio%' OR T.Titre LIKE 'ergo%') AND IV.NoFolder = IF1.NoFolder ORDER BY IV.DateHeure) AS UserNoRef"

    Private Shared stepHtmlFields() As String = {"Evolution", "TreamentsSuspended", "FolderSuspendedTreamentDate", "FolderFrequencyJustification"}

    Private startingTime As Date = Date.Now

    Public Event selectionProgressed(ByVal sender As Object, ByVal progression As Double)

    Private Sub selectClinic(ByRef data As DataSet)
        'TODO : Change hardcoded fields to DB fields
        DBLinker.getInstance.readDBForGrid("InfoClinique", _
                                            "CodePostal as ClinicPostalCode," & _
                                            "Nom as ClinicName," & _
                                            "'Québec' AS ClinicProvinceState," & _
                                            "'' AS ClinicZipCode," & _
                                            "'Canada' AS ClinicCountry," & _
                                            Config.current.noCSST & " as ClinicNoCSST", _
                                            , , , "clinic", data, False)
    End Sub

    Private Sub selectInitialReports(ByRef data As DataSet)
        'TODO : Change hardcoded fields to DB fields

        DBLinker.getInstance.readDBForGrid("(FolderTextes FT INNER JOIN FolderDates IF1 ON IF1.NoFolder = FT.NoFolder INNER JOIN InfoClients IC ON IC.NoClient = IF1.NoClient) LEFT JOIN KeyPeople KP ON KP.NoKP = IF1.NoKP", _
                                            "IF1.Service  As FolderService," & _
                                            "FT.NoFolder as NoFolder," & _
                                            "IC.Nom AS ClientLastName," & _
                                            "IC.Prenom AS ClientFirstName," & _
                                            "'Québec' AS ClientProvinceState," & _
                                            "IC.CodePostal AS ClientPostalCode," & _
                                            "'' AS ClientZipCode," & _
                                            "'Canada' AS ClientCountry," & _
                                            "'' AS NoRVsList," & _
                                            "IC.NAM AS ClientNAM," & _
                                            "IC.NoClient," & _
                                            "IF1.DateAccident AS FolderEventDate," & _
                                            "IF1.DateRef AS FolderOrdonnanceDate," & _
                                            "IF1.DateReceptionRef AS FolderReceptionOfOrdonnanceDate," & _
                                            "IF1.NoRef AS FolderNoRef," & _
                                            "KP.NoRef AS MedecinNoRef," & _
                                            "IF1.Diagnostic AS FolderDiagnostic," & _
                                            SELECT_TRP_NOREF & "," & _
                                            SELECT_TRP_NAME_ID & "," & _
                                            "IF1.FirstTraitement AS FirstTreamentDate," & _
                                            "IF1.FirstEval AS TakenInChargeDate," & _
                                            "IF1.ClosingDate AS ClosingDate," & _
                                            "FT.Texte AS [Text]," & _
                                            "FT.TexteTitle AS [TextTitle]," & _
                                            "'FILLEDBYTEXT' AS Analyze," & _
                                            "'FILLEDBYTEXT' AS Goal," & _
                                            "'FILLEDBYTEXT' AS [Plan]," & _
                                            "(IF1.Frequence+1)  AS FolderFrequency," & _
                                            "'FILLEDBYTEXT' AS FolderFrequencyJustification," & _
                                            "(SELECT COUNT(*) FROM InfoFolders IF2 WHERE IF2.ExternalStatus <> " & Params.current.markedAsRefused & " AND IF2.Service = IF1.Service AND IF2.NoClient = IF1.NoClient AND IF2.NoFolder <> IF1.NoFolder AND IF2.NoRef = IF1.NoRef AND (SELECT COUNT(*) FROM FolderTextes FT2 WHERE FT2.NoFolder=IF2.NoFolder AND FT2.ExternalStatus >= " & Params.current.markedAsNotProcessed & " AND FT2.ExternalStatus < " & Params.current.markedAsConfirmed & ")<>0) AS OtherFolderWithSameNoRefNotCompleted," & _
                                            "CASE WHEN FT.DateStarted < IF1.FirstTraitement THEN IF1.FirstTraitement ELSE FT.DateStarted END AS SignatureDate", _
                                            "IF1.ExternalStatus <> " & Params.current.markedAsRefused & " AND FT.ExternalStatus = " & Params.current.markedAsNotProcessed & " AND FT.DateStarted <= '" & startingTime.ToString("yyyy/MM/dd") & "' AND NOT FT.DateFinished IS NULL AND FT.TexteTitle LIKE 'Rapport CSST initial%'", , , "initials", data, False)

        'Get some fields from the Text field
        For i As Integer = 0 To data.Tables("initials").Rows.Count - 1
            With data.Tables("initials").Rows(i)
                Dim curText As String = .Item("Text")
                If curText = "" Then Continue For
                Dim extractor As New HtmlValuesExtractor(curText, getHtmlValuesForInitials())
                .Item("Analyze") = extractor.getValue("Analyse")
                .Item("Goal") = extractor.getValue("Buts")
                .Item("Plan") = extractor.getValue("Plan")
                .Item("FolderFrequencyJustification") = extractor.getValue("FolderFrequencyJustification")
            End With
        Next
    End Sub

    Private Sub selectFinalReports(ByRef data As DataSet)
        'TODO : Change hardcoded fields to DB fields

        DBLinker.getInstance.readDBForGrid("(FolderTextes FT INNER JOIN FolderDates IF1 ON IF1.NoFolder = FT.NoFolder INNER JOIN InfoClients IC ON IC.NoClient = IF1.NoClient) LEFT JOIN KeyPeople KP ON KP.NoKP = IF1.NoKP", _
                                            "IF1.Service  As FolderService," & _
                                            "FT.NoFolder as NoFolder," & _
                                            "IC.Nom AS ClientLastName," & _
                                            "IC.Prenom AS ClientFirstName," & _
                                            "'Québec' AS ClientProvinceState," & _
                                            "IC.CodePostal AS ClientPostalCode," & _
                                            "'' AS ClientZipCode," & _
                                            "'Canada' AS ClientCountry," & _
                                            "'' AS NoRVsList," & _
                                            "IC.NAM AS ClientNAM," & _
                                            "IC.NoClient," & _
                                            "IF1.DateAccident AS FolderEventDate," & _
                                            "IF1.DateRef AS FolderOrdonnanceDate," & _
                                            "IF1.NoRef AS FolderNoRef," & _
                                            "KP.NoRef AS MedecinNoRef," & _
                                            "IF1.Diagnostic AS FolderDiagnostic," & _
                                            SELECT_TRP_NOREF & "," & _
                                            SELECT_TRP_NAME_ID & "," & _
                                            "IF1.ClosingDate AS LastTreamentDate," & _
                                            "FT.Texte AS [Text]," & _
                                            "FT.TexteTitle AS [TextTitle]," & _
                                            "'FILLEDBYTEXT' AS SoapS," & _
                                            "'FILLEDBYTEXT' AS SoapO," & _
                                            "'FILLEDBYTEXT' AS SoapA," & _
                                            "'FILLEDBYTEXT' AS SoapP," & _
                                            "CASE WHEN FT.DateStarted < IF1.FirstTraitement THEN IF1.FirstTraitement ELSE FT.DateStarted END AS SignatureDate", _
                                            "IF1.ExternalStatus <> " & Params.current.markedAsRefused & " AND (SELECT COUNT(*) FROM FolderTextes FT2 WHERE FT2.NoFolder = FT.NoFolder AND FT2.TexteTitle <> 'Rapport CSST final' AND FT2.TexteTitle LIKE 'Rapport CSST%' AND FT2.ExternalStatus <> " & Params.current.markedAsConfirmed & ") = 0 AND FT.ExternalStatus = " & Params.current.markedAsNotProcessed & " AND FT.DateStarted <= '" & startingTime.ToString("yyyy/MM/dd") & "' AND NOT FT.DateFinished IS NULL AND FT.TexteTitle LIKE 'Rapport CSST final%'", , , "finals", data, False)

        'Get some fields from the Text field
        For i As Integer = 0 To data.Tables("finals").Rows.Count - 1
            With data.Tables("finals").Rows(i)
                Dim curText As String = .Item("Text")
                If curText = "" Then Continue For
                Dim extractor As New HtmlValuesExtractor(curText, getHtmlValuesForFinals())
                .Item("SoapS") = extractor.getValue("SOAP-S")
                .Item("SoapO") = extractor.getValue("SOAP-O")
                .Item("SoapA") = extractor.getValue("SOAP-A")
                .Item("SoapP") = extractor.getValue("SOAP-P")
            End With
        Next
    End Sub

    Private Sub selectStepReports(ByRef data As DataSet)
        'TODO : Change hardcoded fields to DB fields
        DBLinker.getInstance.readDBForGrid("(FolderTextes FT INNER JOIN FolderDates IF1 ON IF1.NoFolder = FT.NoFolder INNER JOIN InfoClients IC ON IC.NoClient = IF1.NoClient) LEFT JOIN KeyPeople KP ON KP.NoKP = IF1.NoKP", _
                                            "IF1.Service  As FolderService," & _
                                            "FT.NoFolder as NoFolder," & _
                                            "IC.Nom AS ClientLastName," & _
                                            "IC.Prenom AS ClientFirstName," & _
                                            "'Québec' AS ClientProvinceState," & _
                                            "IC.CodePostal AS ClientPostalCode," & _
                                            "'' AS ClientZipCode," & _
                                            "'Canada' AS ClientCountry," & _
                                            "IC.NAM AS ClientNAM," & _
                                            "'' AS NoRVsList," & _
                                            "IC.NoClient," & _
                                            "IF1.DateAccident AS FolderEventDate," & _
                                            "IF1.DateRef AS FolderOrdonnanceDate," & _
                                            "IF1.NoRef AS FolderNoRef," & _
                                            "KP.NoRef AS MedecinNoRef," & _
                                            "IF1.Diagnostic AS FolderDiagnostic," & _
                                            SELECT_TRP_NOREF & "," & _
                                            SELECT_TRP_NAME_ID & "," & _
                                            "(SELECT COUNT(*) FROM InfoVisites IV WHERE (FT.NoFolder = IV.NoFolder) AND (Evaluation = 0) AND (NoStatut = 4)) AS FolderTotalTreaments," & _
                                            "FT.NoMultiple AS TextNoStep," & _
                                            "FT.TexteTitle AS [TextTitle]," & _
                                            "FT.Texte AS [Text]," & _
                                            "IF1.FirstTraitement AS FirstTreamentDate," & _
                                            "IF1.FirstEval AS TakenInChargeDate," & _
                                            "IF1.ClosingDate AS ClosingDate," & _
                                            "'FILLEDBYTEXT' AS SoapS," & _
                                            "'FILLEDBYTEXT' AS SoapO," & _
                                            "'FILLEDBYTEXT' AS SoapA," & _
                                            "'FILLEDBYTEXT' AS SoapP," & _
                                            "(IF1.Frequence+1) AS FolderFrequency," & _
                                            "'FILLEDBYTEXT' AS FolderFrequencyJustification," & _
                                            "'FILLEDBYTEXT' AS TreamentsSuspended," & _
                                            "'FILLEDBYTEXT' AS Evolution," & _
                                            "'FILLEDBYTEXT' AS FolderSuspendedTreamentDate," & _
                                            "CASE WHEN FT.DateStarted < IF1.FirstTraitement THEN IF1.FirstTraitement ELSE FT.DateStarted END AS SignatureDate, FT.TexteTitle", _
                                            "IF1.ExternalStatus <> " & Params.current.markedAsRefused & " AND (SELECT COUNT(*) FROM FolderTextes FT2 WHERE FT2.NoFolder = FT.NoFolder AND (FT2.TexteTitle = 'Rapport CSST initial' OR FT2.TexteTitle LIKE 'Rapport CSST d''étape%') AND FT2.NoMultiple <= FT.NoMultiple AND FT2.NoFolderTexte <> FT.NoFolderTexte AND FT2.ExternalStatus <> " & Params.current.markedAsConfirmed & ") = 0 AND FT.ExternalStatus = " & Params.current.markedAsNotProcessed & " AND FT.DateStarted <= '" & startingTime.ToString("yyyy/MM/dd") & "' AND NOT FT.DateFinished IS NULL AND FT.TexteTitle LIKE 'Rapport CSST d''étape%'", , , "steps", data, False)

        'Get some fields from the Text field
        For i As Integer = 0 To data.Tables("steps").Rows.Count - 1
            With data.Tables("steps").Rows(i)
                Dim curText As String = .Item("Text")
                If curText = "" Then Continue For

                Dim extractor As New HtmlValuesExtractor(curText, getHtmlValuesForSteps())
                For Each curField As String In stepHtmlFields
                    Dim value As Object = extractor.getValue(curField)
                    .Item(curField) = value
                Next

                .Item("SoapS") = extractor.getValue("SOAP-S")
                .Item("SoapO") = extractor.getValue("SOAP-O")
                .Item("SoapA") = extractor.getValue("SOAP-A")
                .Item("SoapP") = extractor.getValue("SOAP-P")
            End With
        Next
    End Sub

    Private Function getHtmlValuesForSteps()
        Dim htmlValues As New List(Of HtmlValue)
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.INPUT, "Evolution"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.SELECT, "TreamentsSuspended"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "FolderSuspendedTreamentDate"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-S"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-O"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-A"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-P"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.INPUT, "FolderFrequencyJustification"))

        Return htmlValues
    End Function

    Private Function getHtmlValuesForInitials()
        Dim htmlValues As New List(Of HtmlValue)
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "Analyse"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "Buts"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "Plan"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.INPUT, "FolderFrequencyJustification"))

        Return htmlValues
    End Function

    Private Function getHtmlValuesForFinals()
        Dim htmlValues As New List(Of HtmlValue)
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-S"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-O"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-A"))
        htmlValues.Add(New HtmlValue(HtmlValue.HtmlTags.TEXTAREA, "SOAP-P"))

        Return htmlValues
    End Function

    'Private Function getGoalAndPlanFromHTML(ByVal htmlExtractor As HtmlValuesExtractor) As String
    '    Dim analyse As String = htmlExtractor.getValue("Analyse")
    '    Dim buts As String = htmlExtractor.getValue("Buts")
    '    Dim plan As String = htmlExtractor.getValue("Plan")

    '    If analyse Is Nothing OrElse buts Is Nothing OrElse plan Is Nothing Then
    '        Return Nothing
    '    End If

    '    If String.IsNullOrEmpty(analyse.Trim) AndAlso String.IsNullOrEmpty(buts.Trim) AndAlso String.IsNullOrEmpty(plan.Trim) Then
    '        Return String.Empty
    '    End If

    '    Dim goalAndPlan = "A :" & vbTab & String.Join(vbCrLf & vbTab, analyse.Split(New String() {vbCrLf}, StringSplitOptions.None)) & vbCrLf & vbCrLf & _
    '                        "B :" & vbTab & String.Join(vbCrLf & vbTab, buts.Split(New String() {vbCrLf}, StringSplitOptions.None)) & vbCrLf & vbCrLf & _
    '                        "P :" & vbTab & String.Join(vbCrLf & vbTab, plan.Split(New String() {vbCrLf}, StringSplitOptions.None))

    '    Return goalAndPlan
    'End Function

    'Private Function getSOAPFromHTML(ByVal htmlExtractor As HtmlValuesExtractor) As String
    '    Dim SOAP_S As String = htmlExtractor.getValue("SOAP-S")
    '    Dim SOAP_O As String = htmlExtractor.getValue("SOAP-O")
    '    Dim SOAP_A As String = htmlExtractor.getValue("SOAP-A")
    '    Dim SOAP_P As String = htmlExtractor.getValue("SOAP-P")

    '    If SOAP_A Is Nothing OrElse SOAP_O Is Nothing OrElse SOAP_P Is Nothing OrElse SOAP_S Is Nothing Then
    '        Return Nothing
    '    End If


    '    If String.IsNullOrEmpty(SOAP_S.Trim) AndAlso String.IsNullOrEmpty(SOAP_O.Trim) AndAlso String.IsNullOrEmpty(SOAP_A.Trim) AndAlso String.IsNullOrEmpty(SOAP_P.Trim) Then
    '        Return String.Empty
    '    End If

    '    Dim SOAP = "S :" & vbTab & String.Join(vbCrLf & vbTab, SOAP_S.Split(New String() {vbCrLf}, StringSplitOptions.None)) & vbCrLf & vbCrLf & _
    '                "O :" & vbTab & String.Join(vbCrLf & vbTab, SOAP_O.Split(New String() {vbCrLf}, StringSplitOptions.None)) & vbCrLf & vbCrLf & _
    '                "A :" & vbTab & String.Join(vbCrLf & vbTab, SOAP_A.Split(New String() {vbCrLf}, StringSplitOptions.None)) & vbCrLf & vbCrLf & _
    '                "P :" & vbTab & String.Join(vbCrLf & vbTab, SOAP_P.Split(New String() {vbCrLf}, StringSplitOptions.None))

    '    Return SOAP
    'End Function

    Private Sub selectPresences(ByRef data As DataSet)
        'Build code names WHERE part
        'Dim codeNames() As String = Config.current.folderCodeNames.Split(New Char() {","})
        'Dim where As String = ""
        'For Each codeName As String In codeNames
        '    If where <> "" Then where &= ","
        '    where &= "'" & codeName.Replace("'", "''") & "'"
        'Next
        'If where <> "" Then where = "CDC.CodeName IN (" & where & ")"

        DBLinker.getInstance.readDBForGrid("(CodesDossiersCodes CDC INNER JOIN FolderDates IF1 ON IF1.NoCodeUnique = CDC.NoCodeUnique INNER JOIN InfoClients IC ON IC.NoClient = IF1.NoClient) LEFT JOIN (SELECT DateStarted,NoFolder FROM FolderTextes WHERE TexteTitle='Rapport CSST final') AS Final ON IF1.NoFolder=Final.NoFolder", _
                                            "IF1.Service  As FolderService," & _
                                            "IF1.NoFolder as NoFolder," & _
                                            "IC.NoClient," & _
                                            "IC.Nom AS ClientLastName," & _
                                            "IC.Prenom AS ClientFirstName," & _
                                            "IC.NAM AS ClientNAM," & _
                                            "IF1.NoRef AS FolderNoRef," & _
                                            "'' AS NoRVsList," & _
                                            "(SELECT TOP 1 DateHeure FROM InfoVisites IV WHERE IV.NoStatut<>3 AND IV.NoFolder = IF1.NoFolder AND (IF1.FirstEval IS NULL OR DateHeure >= IF1.FirstEval) AND DateHeure < CASE WHEN Final.DateStarted IS NULL THEN '" & startingTime.ToString("yyyy/MM/dd") & "' ELSE DATEADD(day, 1, Final.DateStarted) END AND ExternalStatus = " & Params.current.markedAsNotProcessed & " ORDER BY DateHeure ASC)  AS FirstPeriod," & _
                                            "(SELECT TOP 1 DateHeure FROM InfoVisites IV WHERE IV.NoStatut<>3 AND IV.NoFolder = IF1.NoFolder AND (IF1.FirstEval IS NULL OR DateHeure >= IF1.FirstEval) AND DateHeure < CASE WHEN Final.DateStarted IS NULL THEN '" & startingTime.ToString("yyyy/MM/dd") & "' ELSE DATEADD(day, 1, Final.DateStarted) END AND ExternalStatus = " & Params.current.markedAsNotProcessed & " ORDER BY DateHeure DESC)  AS LastPeriod," & _
                                            "IF1.FirstTraitement AS FirstTreamentDate," & _
                                            "(SELECT TOP 1 Evaluation FROM InfoVisites IV WHERE IV.NoFolder = IF1.NoFolder AND NoStatut=4 ORDER BY DateHeure ASC) AS IsEvalDate," & _
                                            "IF1.DateAccident AS FolderEventDate", _
                                            "IF1.ExternalStatus <> " & Params.current.markedAsRefused & " AND (SELECT TOP 1 ExternalStatus FROM FolderTextes FT WHERE FT.NoFolder = IF1.NoFolder AND FT.DateFinished IS NOT NULL AND FT.TexteTitle = 'Rapport CSST initial') = " & Params.current.markedAsConfirmed & " AND (SELECT COUNT(*) FROM InfoVisites IV WHERE IV.NoFolder = IF1.NoFolder AND NoStatut<>3 AND (IF1.FirstEval IS NULL OR DateHeure >= IF1.FirstEval) AND DateHeure < CASE WHEN Final.DateStarted IS NULL THEN '" & startingTime.ToString("yyyy/MM/dd") & "' ELSE DATEADD(day, 1, Final.DateStarted) END  AND IV.ExternalStatus = " & Params.current.markedAsNotProcessed & ") <> 0", , , "presences", data, False)

        RaiseEvent selectionProgressed(Me, 64)

        'Build Folders list to gather their rendez-vous
        Dim noFolders As New List(Of String)
        For i As Integer = 0 To data.Tables("presences").Rows.Count - 1
            With data.Tables("presences").Rows(i)
                If noFolders.Contains(.Item("NoFolder").ToString) = False Then noFolders.Add(.Item("NoFolder").ToString)
            End With
        Next

        'Get needed RVs
        If noFolders.Count <> 0 Then
            'TODO : Certain codes ne sont pas gérés : G et Y
            DBLinker.getInstance.readDBForGrid("InfoVisites IV INNER JOIN FolderDates IF1 ON IF1.NoFolder = IV.NoFolder LEFT JOIN (SELECT DateStarted,NoFolder FROM FolderTextes WHERE TexteTitle='Rapport CSST final') AS Final ON IF1.NoFolder=Final.NoFolder", _
                                                "IV.NoVisite as NoRV," & _
                                                "IV.NoFolder as NoFolder," & _
                                                "IV.DateHeure as RVDate," & _
                                                "CASE WHEN IV.NoStatut <= 3 THEN CASE WHEN IV.NoStatut = 3 THEN 'X' ELSE 'A' END ELSE CASE WHEN IV.Evaluation = 1 AND IV.DateHeure = IF1.FirstDate THEN 'I' ELSE 'P' END END AS RVStatus" _
                                                , "IV.NoFolder IN (" & String.Join(",", noFolders.ToArray) & ") AND (IF1.FirstEval IS NULL OR DateHeure >= IF1.FirstEval) AND DateHeure <= CASE WHEN Final.DateStarted IS NULL THEN '" & startingTime.AddDays(-1).ToString("yyyy/MM/dd") & "' ELSE DATEADD(day, 1, Final.DateStarted) END AND IV.ExternalStatus = " & Params.current.markedAsNotProcessed, , , "rvs", data, False)
        End If
    End Sub

    Public Function selectData(ByVal startingTime As Date) As DataSet
        Dim data As New DataSet

        Me.startingTime = startingTime

        selectClinic(data)
        RaiseEvent selectionProgressed(Me, 10)

        selectInitialReports(data)
        RaiseEvent selectionProgressed(Me, 28)
        selectStepReports(data)
        RaiseEvent selectionProgressed(Me, 46)
        selectPresences(data)
        RaiseEvent selectionProgressed(Me, 82)
        selectFinalReports(data)
        RaiseEvent selectionProgressed(Me, 100)

        Return data
    End Function
End Class
