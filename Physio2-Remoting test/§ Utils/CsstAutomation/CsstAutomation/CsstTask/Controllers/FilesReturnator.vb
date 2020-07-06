Public Class FilesReturnator

    Private Const NB_DAYS_MAX_RETURN_SHALL_BE_EXISTING As Integer = 2


    Private nbResponsesReports As Integer = 0
    Private nbResponsesReportsAnalyzed As Integer = 0
    Private fwi As CI.CsstAutomation.FilesWebInteractor
    Private errorsLinks As New ErrorsLinks()
    Private nbProcessed As Integer = 0
    Private nbErrors As Integer = 0

    Public filesResults As New List(Of FileResult)

    Public Shared finalErrorSetConfirmed As New Dictionary(Of Integer, Boolean)

    Public Event returnProgressed(ByVal sender As Object, ByVal progression As Double)
    Public Event changeStep(ByVal sender As Object, ByVal e As EventArgs)


    Public Sub New(ByVal fwi As CI.CsstAutomation.FilesWebInteractor)
        Me.fwi = fwi
        finalErrorSetConfirmed = New Dictionary(Of Integer, Boolean) 'Reset the shared variables so that next module execution doesn't have old data
    End Sub

    Public Sub checkReturn()
        deleteNotProcessedReturns()
        nbResponsesReports = 0
        nbResponsesReportsAnalyzed = 0

        errorsLinks.load()

        Dim responses As List(Of FileResponse) = getReturns()

        RaiseEvent changeStep(Me, EventArgs.Empty)
        RaiseEvent returnProgressed(Me, 0)
        If nbResponsesReports <> 0 Then
            Base.DBLinker.getInstance.beginTransaction()
            Try
                Base.DBLinker.getInstance().beginBatching() 'Batching so it doesn't intefer with SELECT during analysis

                analyzeReturns(responses)

                Base.DBLinker.getInstance().endBatching()
            Catch ex As Exception
                Throw New Exception("", ex)
            Finally
                Base.DBLinker.getInstance().clearBatching()
            End Try

            If nbErrors <> 0 Then
                Base.DBLinker.getInstance.rollbackAllTransactions()
                removeNoneErrorResults()
            Else
                Try
                    fwi.deleteMails()
                    Base.DBLinker.getInstance.commitTransaction()

                    ensureFilesSentGetReturn()

                    'Delete imported files
                    Dim importPath As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "import"
                    If IO.Directory.Exists(importPath) Then
                        Dim files() As String = IO.Directory.GetFiles(importPath)
                        For Each curFile As String In files
                            IO.File.Delete(curFile)
                        Next
                        IO.Directory.Delete(importPath)
                    End If
                Catch ex As Exception
                    Base.External.propagateErrorLog(ex)
                    Base.DBLinker.getInstance.rollbackAllTransactions()
                End Try
            End If
            RaiseEvent returnProgressed(Me, 100) 'Ensure 100% after check
        Else
            filesResults.AddRange(fwi.results) 'Ensure errors are transmitted

            'Advise of steps not skipped (do it now)
            RaiseEvent changeStep(Me, EventArgs.Empty)
        End If

        errorsLinks.save(nbErrors)

        'TODO : Remove the two lines below after 2014-11-01
        Dim returnsLogFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "returns.log"
        If IO.File.Exists(returnsLogFile) Then IO.File.Delete(returnsLogFile)
    End Sub

    Private Sub analyzeReturns(ByVal responses As List(Of FileResponse))
        Dim nbDone As Integer = 0
        'responses.Reverse() 'Set newer to older order (Really important this way... So if something was accepted, but resent anyway... the newest one will be in error and the older one ok.. the correct one is the older one, so it shall be the last to be executed)
        For Each curResponse In responses
            curResponse.analyze(Me.filesResults)

            nbDone += curResponse.nbReports
            nbErrors += curResponse.nbErrors
            nbProcessed += curResponse.nbProcessed
            RaiseEvent returnProgressed(Me, ((nbDone / nbResponsesReports) * 100.0))
            filesResults.AddRange(curResponse.results)
        Next
    End Sub

    Private Sub removeNoneErrorResults()
        Dim curResults() As FileResult
        ReDim curResults(filesResults.Count - 1)
        filesResults.CopyTo(curResults)
        For Each curResult As FileResult In curResults
            If Not curResult.isError Then filesResults.Remove(curResult)
        Next
    End Sub

    Private Sub deleteNotProcessedReturns()
        Dim files() As String = IO.Directory.GetFiles(Config.current.outputFolder, "*.web")
        For Each curFile As String In files
            IO.File.Delete(curFile)
        Next
    End Sub


    Private Sub ensureFilesSentGetReturn()
        RaiseEvent changeStep(Me, EventArgs.Empty)
        RaiseEvent returnProgressed(Me, 0)

        Dim doneFiles() As String = IO.Directory.GetFiles(Config.current.outputFolder, "*.done", IO.SearchOption.TopDirectoryOnly)

        Dim returnsNotExistingInError As New Generic.List(Of FileResult)
        Dim nbFilesChecked As Integer = 0

        Dim ensuredDir As String = Config.current.outputFolder
        ensuredDir &= addSlash(ensuredDir) & "verified"
        IO.Directory.CreateDirectory(ensuredDir)
        ensuredDir &= "\"

        'Move ret files which are linked directly to done files sent
        For Each filename In doneFiles
            Dim retFile As String = filename.Substring(0, filename.Length - 4) & "ret"
            Dim saveFile As String = filename.Substring(0, filename.Length - 4) & "save"
            Dim fileInfo As New IO.FileInfo(filename)
            Dim fileDoneDaysPast As Integer = Date.Now.Subtract(fileInfo.CreationTime).Days

            Dim retFilenameOnly As String = retFile.Substring(retFile.LastIndexOf("\") + 1)
            Dim doneFilenameOnly As String = filename.Substring(filename.LastIndexOf("\") + 1)
            Dim saveFilenameOnly As String = saveFile.Substring(saveFile.LastIndexOf("\") + 1)
            Dim newDoneFilename As String = ensuredDir & doneFilenameOnly
            Dim newRetFilename As String = ensuredDir & retFilenameOnly
            Dim newSaveFilename As String = ensuredDir & saveFilenameOnly

            If fileDoneDaysPast >= NB_DAYS_MAX_RETURN_SHALL_BE_EXISTING Then
                If IO.File.Exists(retFile) = False Then
                    Dim noRetFile As New FileResult(False, 0, "", 0, "Module CSST", "Le fichier de retour nommé """ & retFilenameOnly & """ n'existe toujours pas après " & fileDoneDaysPast & " jours que le fichier """ & doneFilenameOnly & """ est été téléversé à la CSST. Veuillez contacter la CSST pour qu'il renvoit le fichier de "".ret"" via le guichet automatisé. Après cette étape, le module CSST pourra être réexécuter et il pourra ainsi traiter ce fichier manquant.", retFilenameOnly, "", CsstTask.RESULT_INFO_COLOR, "", Base.LIMIT_DATE, "")
                    returnsNotExistingInError.Add(noRetFile)

                    'TODO : This error is there to know if the problem is coming from the module or from CSST... if from CSST, then it could be deleted
                    Base.External.propagateErrorLog(New Exception("CSST return file not gotten : " & retFilenameOnly))
                Else
                    If IO.File.Exists(newDoneFilename) Then IO.File.Delete(newDoneFilename)
                    If IO.File.Exists(newRetFilename) Then IO.File.Delete(newRetFilename)
                    If IO.File.Exists(newSaveFilename) Then IO.File.Delete(newSaveFilename)

                    IO.File.Move(filename, newDoneFilename)
                    IO.File.Move(retFile, newRetFilename)
                    If IO.File.Exists(saveFile) Then IO.File.Move(saveFile, newSaveFilename)
                End If
            End If

            nbFilesChecked += 1
            RaiseEvent returnProgressed(Me, ((nbFilesChecked / doneFiles.Length) * 100.0))
        Next
        RaiseEvent returnProgressed(Me, 100)

        If returnsNotExistingInError.Count > 0 Then filesResults.AddRange(returnsNotExistingInError)

        'Move ret files which are indirectly linked to done files (like .NUMBER.ret where NUMBER is an integer number)
        'Those are not ensured because no done file was sent for that and it's presumed that the main is the most important. Even more, module can not determinate how many of these files will exist
        Dim retFiles() As String = IO.Directory.GetFiles(Config.current.outputFolder, "*.ret", IO.SearchOption.TopDirectoryOnly)
        For Each retFile As String In retFiles
            Dim retFilenameOnly As String = retFile.Substring(retFile.LastIndexOf("\") + 1)
            Dim newRetFilename As String = ensuredDir & retFilenameOnly

            Dim nbDots As Integer = retFile.Split(New Char() {"."}).Length - 1
            If nbDots = 2 Then
                If IO.File.Exists(newRetFilename) Then IO.File.Delete(newRetFilename)
                IO.File.Move(retFile, newRetFilename)
            End If
        Next
    End Sub


    Private Function getReturns() As List(Of FileResponse)
        Dim returns As New List(Of String)
        Dim responses As New List(Of FileResponse)

        'Files to import
        Dim importPath As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "import"
        Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso8859-1")
        If IO.Directory.Exists(importPath) Then
            Dim files() As String = IO.Directory.GetFiles(importPath)
            For Each curFile As String In files
                returns.Add(IO.File.ReadAllText(curFile, encoding))
            Next
        End If

        'Get returns from web
        returns.AddRange(fwi.getReturn())

        RaiseEvent changeStep(Me, EventArgs.Empty)
        RaiseEvent returnProgressed(Me, 0)
        Dim i As Integer = 0
        'Create FileReponse objects
        For Each curReturn In returns
            Dim curFileResponse As New FileResponse(Date.Today.ToString("yyyy-MM-dd") & "." & Params.current.nextFileNumber & ".web", curReturn, errorsLinks)
            nbResponsesReports += curFileResponse.nbReports
            responses.Add(curFileResponse)

            Params.current.nextFileNumber += 1
            Params.current.save()

            AddHandler curFileResponse.reportAnalyzed, AddressOf reportAnalyzed
            i += 1
            RaiseEvent returnProgressed(Me, (i / returns.Count * 100))
        Next
        RaiseEvent returnProgressed(Me, 100)

        Return responses
    End Function

    Private Sub reportAnalyzed(ByVal sender As FileResponse)
        If nbResponsesReports = 0 Then Exit Sub

        nbResponsesReportsAnalyzed += 1
        RaiseEvent returnProgressed(Me, ((nbResponsesReportsAnalyzed / nbResponsesReports) * 100))
    End Sub

End Class
