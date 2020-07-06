Public Class CsstTask
    Inherits CI.Base.TaskBase

    Private Const ERROR_PLEASE_RETRY As String = "Erreur - Veuillez essayer de nouveau"
    
    Public Const RESULT_OK_COLOR As String = "green"
    Public Const RESULT_ERROR_COLOR As String = "red"
    Public Const RESULT_UNKNOWN_ERROR_COLOR As String = "orange"
    Public Const RESULT_INFO_COLOR As String = "blue"


    Private startingTime As Date = Date.Now
    Private Shared stepNames() As String = New String() {"Connexion au site de la CSST", "Vérification du retour de la CSST", "Téléchargement du retour de la CSST", "Traitement du retour de la CSST", "Analyse du retour de la CSST", "Assurance de l'existance des retours", "Sélection des données", "Création des fichiers", "Téléversement des fichiers"}
    Private filesResults As New List(Of FileResult)
    Private fwi As CI.CsstAutomation.FilesWebInteractor

    Private taskThread As Threading.Thread
    Private errorToThrow As Exception = Nothing
    'TODO : Look if doTask support multiple instance to be launched at once....  if yes, transfor errorToThrow and taskThread to support this.
    '--> Private errorToThrow As New Dictionary(Of Integer, Exception)

    Public Sub New(ByVal creator As CI.Base.PluginTaskBase)
        MyBase.New(creator)
    End Sub


    Private Sub _doTask()
        resetSteps()

        startingTime = Date.Now

        Try
            'Look at CSST return first to ensure setting the noCSSTFolder to the InfoFolders.NoRef field
            If Base.isConnectionAvailable() Then
                If Config.current.doOnly_UploadTestFile Then
                    gotoNextStep()

                    createFilesWebInteractor()

                    gotoNextStep()
                    gotoNextStep()
                    gotoNextStep()
                    gotoNextStep()
                Else
                    checkReturn()
                End If
            Else
                Dim noInternet As New FileResult(True, 0, "", 0, "Module CSST", "Aucune connexion Internet. Vous devez être connecté pour utiliser le module.", "", "", RESULT_ERROR_COLOR, "", Base.LIMIT_DATE, "")
                addFilesResults(New List(Of FileResult)(New FileResult() {noInternet}))
            End If

            Base.DBLinker.getInstance().clearBatching() 'Ensure to no batching left even though it shouldn't happen anymore

            'Continue only if no errors to give the user a chance to correct these
            If nbErrors = 0 Then
                'Data selection
                Dim data As DataSet = selectData()

                'Writing files
                Dim fc As FilesCreator = Nothing
                Base.DBLinker.getInstance().beginTransaction()
                Try
                    fc = createFiles(data)
                    Base.DBLinker.getInstance().commitTransaction()
                Catch ex As Exception
                    Base.DBLinker.getInstance().rollbackTransaction()
                    Throw New Exception("See inner exception", ex)
                End Try

                'Upload files to csst web site
                uploadFiles(fc.results)
            End If
        Catch ex As Threading.ThreadInterruptedException
            'TODO : If interrupted between mail deleted & commit... Shall commit anyway !?
            Base.DBLinker.getInstance.rollbackAllTransactions() 'Ensure things are rollback if task cancelled
        Catch ex As FilesWebException
            Dim errorResult As New FileResult(True, 0, "", 0, "Module CSST", ex.Message, "", "", CsstTask.RESULT_ERROR_COLOR, "", Base.LIMIT_DATE, "")
            addFilesResults(New List(Of FileResult)(New FileResult() {errorResult}))

            Base.DBLinker.getInstance.rollbackAllTransactions() 'Ensure things are rollback if task cancelled
        Catch ex As FilesCreationException
            Dim errorResult As New FileResult(True, 0, "", 0, "Module CSST", ex.Message, "", "", CsstTask.RESULT_ERROR_COLOR, "", Base.LIMIT_DATE, "")
            addFilesResults(New List(Of FileResult)(New FileResult() {errorResult}))

            'Do not rollback so that corrections are kept
        Catch ex As Exception
            Dim errorResult As New FileResult(True, 0, "", 0, "Module CSST", "Erreur inconnue ayant pour message : " & ex.Message & "<br/>Veuillez contacter CyberInternautes.", "", "", CsstTask.RESULT_UNKNOWN_ERROR_COLOR, "", Base.LIMIT_DATE, "")
            addFilesResults(New List(Of FileResult)(New FileResult() {errorResult}))

            Base.External.propagateErrorLog(ex)
            Base.DBLinker.getInstance.rollbackAllTransactions() 'Ensure things are rollback
        Finally
            'Ensure webbrowser close
            If fwi IsNot Nothing Then
                fwi.close()
                fwi.Dispose()
                fwi = Nothing
            End If
        End Try


        'Pass result, nbErrors, nbProcessed & taskEnded event
        informEnd()
    End Sub

    Protected Overrides Sub gotoNextStep()
        If currentStep >= stepNames.Length Then Exit Sub

        setCurrentStepName(stepNames(currentStep))

        MyBase.gotoNextStep()
    End Sub

    Private Sub changeStep(ByVal sender As Object, ByVal e As EventArgs)
        gotoNextStep()
    End Sub

    Private Sub progressed(ByVal sender As Object, ByVal progression As Double)
        setTaskProgession(progression)
    End Sub

    Private Function selectData() As DataSet
        gotoNextStep()
        setTaskProgession(0) 'Ensure showing progress bar at 0%
        Dim ds As New DataSelector()
        AddHandler ds.selectionProgressed, AddressOf progressed
        Dim data As DataSet = ds.selectData(startingTime)
        setTaskProgession(100) 'Ensure 100% after selection

        Return data
    End Function

    Private Sub checkReturn()
        gotoNextStep()

        createFilesWebInteractor()

        Dim fr As New FilesReturnator(fwi)
        AddHandler fr.returnProgressed, AddressOf progressed
        AddHandler fr.changeStep, AddressOf changeStep
        fr.checkReturn()
        addFilesResults(fr.filesResults)

    End Sub

    Private Function createFiles(ByRef data As DataSet) As FilesCreator
        gotoNextStep()
        setTaskProgession(0) 'Ensure showing progress bar at 0%
        Dim fc As New FilesCreator(Config.current.outputFolder, data)
        If Not Config.current.doOnly_UploadTestFile Then
            AddHandler fc.creationProgressed, AddressOf progressed
            fc.createFiles()
            addFilesResults(fc.results)
        Else
            fc.createTestFile()
        End If
        setTaskProgession(100) 'Ensure 100% after creation

        Return fc
    End Function


    Private Sub addFilesResults(ByVal results As List(Of FileResult))
        For Each newFileResult As FileResult In results
            Dim toChange As FileResult = Nothing

            newFileResult.resultMessage = "<font color=" & newFileResult.fontColor & ">" & newFileResult.resultMessage & "</font>"

            For Each curFileResult As FileResult In filesResults
                'If same entry with previous return, get it to be combined
                Dim clientIdentified As Boolean = (newFileResult.noClient = curFileResult.noClient AndAlso newFileResult.noClient <> 0) OrElse (newFileResult.noClient = 0 AndAlso newFileResult.nam = curFileResult.nam) 'This method is to ensure only noClient verif when not 0 so that NAM correction are combined. Though still supporting error of client not found.
                If clientIdentified AndAlso newFileResult.noFolder = curFileResult.noFolder AndAlso newFileResult.resultObject = curFileResult.resultObject Then
                    toChange = curFileResult
                    Exit For
                End If
            Next

            If toChange IsNot Nothing Then
                toChange.isError = toChange.isError OrElse newFileResult.isError
                If toChange.resultMessage.IndexOf(newFileResult.resultMessage) = -1 Then toChange.resultMessage &= "<br/>--------------------<br/>" & newFileResult.resultMessage
            Else
                filesResults.Add(newFileResult)
            End If
        Next

        Dim nbErrors, nbProcessed As Integer
        For Each curFileResult As FileResult In filesResults
            nbErrors += If(curFileResult.isError, 1, 0)
            nbProcessed += If(Not curFileResult.isError, 1, 0)
        Next
        setNbErrors(nbErrors)
        setNbProcessed(nbProcessed)

        setResultHtml(createHtmlResult()) 'In case of crash or cancel
    End Sub


    Private Sub createFilesWebInteractor()
        If fwi IsNot Nothing Then Exit Sub

        fwi = New CI.CsstAutomation.FilesWebInteractor(Config.current.outputFolder, startingTime)

        AddHandler fwi.taskProgressed, AddressOf progressed
        AddHandler fwi.startingReturnDownload, AddressOf startingReturnDownload

        fwi.connect()
        gotoNextStep()
    End Sub

    Private Sub startingReturnDownload()
        gotoNextStep()
        setTaskProgession(0) 'Ensure showing progress bar at 0%
    End Sub

    Private Function uploadFiles(ByVal createdResults As List(Of FileResult)) As FilesWebInteractor
        gotoNextStep()

        createFilesWebInteractor()

        setTaskProgession(0) 'Ensure showing progress bar at 0%
        fwi.upload(createdResults)
        setTaskProgession(100) 'Ensure 100% after creation

        addFilesResults(fwi.results)

        Return fwi
    End Function


    Private Function createHtmlResult() As String
        'TODO : Shall be a report (from Clinica)... so shall transfer reports to BaseLib.. holy shit !
        Dim outHtml As New System.Text.StringBuilder()
        'Header
        outHtml.AppendLine("<HTML>").AppendLine("<HEAD>").AppendLine("<TITLE>Résultats de la tâche d'automatisation de la CSST</TITLE>")
        outHtml.AppendLine("</HEAD>").AppendLine("<BODY>")
        outHtml.AppendLine("<STYLE>TD {padding:3;vertical-align:top;border-top:solid 1px black;border-bottom:solid 1px black;} TABLE {border-collapse:collapse;border:solid 1px black;}</STYLE>")
        outHtml.AppendLine("<h1>Résultats de la tâche d'automatisation de la CSST</h1>")
        outHtml.AppendLine("Étape atteinte : " & currentStep & "/" & maximumSteps & "<br>")
        outHtml.AppendLine("Nombre en erreur : " & nbErrors & "<br>Nombre en succès : " & nbProcessed & "<br>Total : " & (nbErrors + nbProcessed))
        outHtml.AppendLine("<p><b>Légende :</b><br><table border=1 cellspacing=0><tr><td style=""color:white;background-color:" & RESULT_OK_COLOR & ";"">Succès</td><td style=""color:white;background-color:" & RESULT_INFO_COLOR & ";"">Information importante</td><td style=""color:white;background-color:" & RESULT_ERROR_COLOR & ";"">Erreur à corriger</td><td style=""color:white;background-color:" & RESULT_UNKNOWN_ERROR_COLOR & ";"">Erreur inconnue (Appeler CyberInternautes)</td></tr></table></p>")
        outHtml.AppendLine("<TABLE><TR style=""background-color:rgb(200,200,200)""><TD>Client</TD><TD># dossier</TD><TD>Objet</TD><TD>Résultat</TD></TR>")

        'Rows
        filesResults.Sort(Function(fr1, fr2) fr1.clientName.CompareTo(fr2.clientName))
        For Each curFR As FileResult In filesResults
            outHtml.Append("<TR><TD data-file=""" & curFR.filename & """><A href=""clinica://CLIENT|").Append(curFR.noClient).Append(""">").Append(curFR.clientName).Append("</A></TD><TD>").Append(curFR.noFolder).Append("</TD><TD>").Append(curFR.resultObject).Append("</TD><TD>").Append(curFR.resultMessage).Append("</TD></TR>")
            outHtml.AppendLine()
        Next

        'Footer
        outHtml.AppendLine("</TABLE>")
        outHtml.AppendLine("</BODY></HTML>")

        Return outHtml.ToString
    End Function


    Private Sub prepareEnd()
        setResultHtml(createHtmlResult())
        setNbErrors(nbErrors)
        setNbProcessed(nbProcessed)
    End Sub

    Private Sub informEnd()
        prepareEnd()
        onTaskEnded(EventArgs.Empty)
    End Sub

    Protected Overrides Sub onTaskAborted(ByVal e As System.EventArgs)
        If taskThread IsNot Nothing AndAlso taskThread.IsAlive Then taskThread.Abort()

        prepareEnd()

        MyBase.onTaskAborted(e)
    End Sub

    Protected Overrides Sub doTask()
        errorToThrow = Nothing

        taskThread = New Threading.Thread(AddressOf __doTask)
        If taskThread.TrySetApartmentState(Threading.ApartmentState.STA) = False Then
            setCurrentStepName(ERROR_PLEASE_RETRY)
            onTaskEnded(EventArgs.Empty)
            Exit Sub
        End If

        taskThread.IsBackground = True
        taskThread.Start()
        taskThread.Join()

        If errorToThrow IsNot Nothing Then Throw New Exception("Relaunched exception", errorToThrow)
    End Sub

    ''' <summary>
    ''' In between method for doTask to ensure real one (_doTask) can throw an error which won't make crash ClinicaServer.
    ''' The error thrown is redirected to main thread and than thrown back. The framework supports this and will catch it.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __doTask()
        Try
            _doTask()
        Catch ex As Exception
            errorToThrow = ex
        End Try
    End Sub


    Public Overrides ReadOnly Property maximumSteps() As Integer
        Get
            Return stepNames.Length
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
