Public Class FilesWebInteractor
    Implements IDisposable

    Private Const CONNECTION_URL As String = "https://www.pes.csst.qc.ca/CookieAuth.dll?Logon"
    Private Const UPLOAD_URL1 As String = "https://www.ee.csst.qc.ca/cgi-bin/safileup/upload.asp"

    Private Const MAXIMUM_UPLOAD_TRIES As Integer = 5

    Private startingTime As Date
    Private outPath As String
    Private files As List(Of String)
    Private uploadUrls() As String = {CONNECTION_URL, UPLOAD_URL1}
    Private myBrowser As New frmCSSTBrowser
    Private isConnected As Boolean = False
    Private nbReturnDownloaded As Integer

    Private Enum UploadActions As Integer
        Connect = 0
        Upload = 1
    End Enum

    Public Event taskProgressed(ByVal sender As Object, ByVal progression As Double)
    Public Event startingReturnDownload()

    Public Sub New(ByVal outPath As String, ByVal startingTime As Date)
        Me.startingTime = startingTime

        IO.Directory.CreateDirectory(outPath) 'Ensure directory exists
        Me.outPath = outPath
        AddHandler myBrowser.startingReturnDownload, AddressOf onStartingReturnDownload
        AddHandler myBrowser.returnDownloaded, AddressOf returnDownloaded
    End Sub

    Private Sub loadFiles()
        Dim filesToMark() As String = IO.Directory.GetFiles(outPath, "*.tomark", IO.SearchOption.TopDirectoryOnly)
        Dim filesToSendOnly() As String = IO.Directory.GetFiles(outPath, "*.cst", IO.SearchOption.TopDirectoryOnly)

        Me.files = New List(Of String)(filesToMark)
        Me.files.AddRange(filesToSendOnly)
        'Ensure files are sorted by their generated number (so same order as creation)
        Me.files.Sort(New Comparison(Of String)(Function(a As String, b As String) System.Text.RegularExpressions.Regex.Replace(a, "[^0-9]", "") < System.Text.RegularExpressions.Regex.Replace(b, "[^0-9]", "")))
    End Sub


    Public Sub connect()
        isConnected = myBrowser.connect(Config.current.csstWebSiteUsername, Config.current.csstWebSitePassword)
    End Sub

    Public Sub close()
        myBrowser.Close()
    End Sub

    Public Shared Function testConnection() As Boolean
        Dim myBrowser As New CSSTBrowser()
        Return myBrowser.testConnection(Config.current.csstWebSiteUsername, Config.current.csstWebSitePassword)
    End Function

    Public Function getReturn() As List(Of String)
        If isConnected = False Then
            results.Add(New FileResult(True, 0, "", 0, "Téléchargement des retours", "Impossible de télécharger les réponses depuis le site de la CSST, car la connexion au site web à échouer", "", "", CsstTask.RESULT_ERROR_COLOR, "", Base.LIMIT_DATE, ""))
            Return New List(Of String)
        End If

        nbReturnDownloaded = 0

        Dim returns As Generic.List(Of String) = Nothing
        Try
            returns = myBrowser.getReturn()
        Catch ex As Exception
            Throw New FilesWebException(ex.Message, ex)
        End Try

        Return returns
    End Function

    Private Sub returnDownloaded()
        nbReturnDownloaded += 1

        RaiseEvent taskProgressed(Me, nbReturnDownloaded / myBrowser.nbReturns * 100)
    End Sub

    Private Sub onStartingReturnDownload()
        RaiseEvent startingReturnDownload()
    End Sub

    Public Sub deleteMails()
        myBrowser.deleteMails()
    End Sub

    Private _results As New List(Of FileResult)

    Public ReadOnly Property results() As List(Of FileResult)
        Get
            Return _results
        End Get
    End Property

    Private Function markFileResults(ByVal curFile As String, ByVal createdResults As List(Of FileResult)) As String
        'Mark into database uploading
        If curFile.EndsWith(".tomark") Then
            Dim tomorrow As Date = Date.Today.AddDays(1)
            Dim newMarkage As Integer = Params.current.markedAsUploaded

            If Not Config.current.doOnly_UploadTestFile Then
                Base.DBLinker.getInstance.beginTransaction()

                For Each curResult As FileResult In createdResults
                    If Not curFile.EndsWith("\" & curResult.filename & ".tomark") OrElse curResult.isError Then Continue For

                    results.Add(New FileResult(False, curResult.noClient, curResult.clientName, curResult.noFolder, curResult.resultObject, curResult.resultMessage.Replace("Créé(e)", "Envoyé(e)"), curResult.filename, curResult.nam, CsstTask.RESULT_OK_COLOR, curResult.noVisites, curResult.refDate, curResult.service))

                    If curResult.filename.StartsWith(FileInitialReports.FILE_PREFIX) Then
                        Base.DBLinker.getInstance.updateDB("FolderTextes", "ExternalStatus=" & newMarkage, _
                                       "NoFolder", curResult.noFolder & " AND TexteTitle = '" & curResult.resultObject.Replace("'", "''") & "' AND ExternalStatus=" & Params.current.markedAsNotProcessed & " AND DateStarted <= '" & startingTime.ToString("yyyy-MM-dd") & "'", False)
                        Base.DBLinker.getInstance.updateDB("InfoFolders", "ExternalStatus=" & Params.current.markedAsUploaded, "NoFolder", curResult.noFolder, False)
                    ElseIf curResult.filename.StartsWith(FileStepReports.FILE_PREFIX) Then
                        Base.DBLinker.getInstance.updateDB("FolderTextes", "ExternalStatus=" & newMarkage, _
                                                           "NoFolder", curResult.noFolder & " AND TexteTitle  = '" & curResult.resultObject.Replace("'", "''") & "' AND ExternalStatus=" & Params.current.markedAsNotProcessed & " AND DateStarted <= '" & startingTime.ToString("yyyy-MM-dd") & "'", False)
                    ElseIf curResult.filename.StartsWith(FileFinalReports.FILE_PREFIX) Then
                        Base.DBLinker.getInstance.updateDB("FolderTextes", "ExternalStatus=" & newMarkage, _
                                                           "NoFolder", curResult.noFolder & " AND TexteTitle  = '" & curResult.resultObject.Replace("'", "''") & "' AND ExternalStatus=" & Params.current.markedAsNotProcessed & " AND DateStarted <= '" & startingTime.ToString("yyyy-MM-dd") & "'", False)
                    ElseIf curResult.filename.StartsWith(FilePresences.FILE_PREFIX) AndAlso curResult.noVisites <> String.Empty Then
                        Base.DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=" & newMarkage, _
                                                                   "NoFolder", curResult.noFolder & " AND NoVisite IN (" & curResult.noVisites & ") AND NoStatut <> 3 AND ExternalStatus=" & Params.current.markedAsNotProcessed, False)
                    End If
                Next
            End If

            'Rename file for upload processing and crash recuperation posssibility
            'Renaming included into transaction, so if it crashes, the results won't be marked
            Dim newFile As String = curFile.Replace(".tomark", ".cst")
            IO.File.Move(curFile, newFile)
            curFile = newFile

            If Not Config.current.doOnly_UploadTestFile Then Base.DBLinker.getInstance.commitTransaction()
        End If

        Return curFile
    End Function

    Private Sub adjustFileResults(ByVal curFile As String, ByVal createdResults As List(Of FileResult))
        'Change file results to SENT
        For Each curResult As FileResult In createdResults.ToArray
            If Not curFile.EndsWith("\" & curResult.filename & ".cst") OrElse curResult.isError Then Continue For

            results.Add(New FileResult(False, curResult.noClient, curResult.clientName, curResult.noFolder, curResult.resultObject, "Envoyé(e)", curResult.filename, curResult.nam, CsstTask.RESULT_OK_COLOR, curResult.noVisites, curResult.refDate, curResult.service))
        Next
    End Sub

    Public Sub upload(ByVal createdResults As List(Of FileResult))
        If isConnected = False Then
            results.Add(New FileResult(True, 0, "", 0, "Téléversement des fichiers", "Impossible de téléverser les fichiers vers le site de la CSST, car la connexion au site web à échouer", "", "", CsstTask.RESULT_ERROR_COLOR, "", Base.LIMIT_DATE, ""))
            Exit Sub
        End If

        Plugin.log("----------------------------------------------------------------------------")
        Plugin.log("New upload started at " & Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        Plugin.log("-------------------------------------------")
        Plugin.log("will load files")
        loadFiles()
        Plugin.log("files loaded. count = " & files.Count)
        results.Clear()
        If files.Count = 0 Then Exit Sub

        Dim nbFilesUploaded As Integer = 0
        Dim pourcentToGrow As Integer = (100 / files.Count) / MAXIMUM_UPLOAD_TRIES
        Dim currentPourcent As Integer = 0

        For Each curFile As String In files
            Plugin.log("--> File : " & curFile)

            Dim filenameOnly As String = curFile.Substring(curFile.LastIndexOf("\") + 1)
            If Config.current.doOnly_UploadTestFile AndAlso filenameOnly.StartsWith("test") = False Then Continue For

            Dim saveFile As String = curFile.Replace(".cst", ".save").Replace(".tomark", ".save")
            If curFile.EndsWith(".cst") AndAlso IO.File.Exists(saveFile) AndAlso Not Config.current.doOnly_UploadTestFile Then
                Plugin.log("File recovery")
                'This part exists when the software or process crashes/fails after marking
                'It is done before, otherwise, it would consider the .tomark which are always recreated
                Dim loadedFile As File = File.createType(saveFile)
                Dim fileResults As List(Of FileResult) = loadedFile.fileResults
                For Each curFR As FileResult In fileResults
                    curFR.resultMessage = curFR.resultMessage.Replace("&gt;", ">").Replace("&lt;", "<")
                Next
                results.AddRange(fileResults)
                Plugin.log("File recovery ended")
            End If

            Plugin.log("will mark file results")
            curFile = markFileResults(curFile, createdResults)
            Plugin.log("file results marked")

            Dim fileUploadedAndConfirmed As Boolean = False
            Dim maxTries As Integer = MAXIMUM_UPLOAD_TRIES
            While maxTries > 0 AndAlso Not fileUploadedAndConfirmed
                Try
                    Plugin.log("Attempt #" & (MAXIMUM_UPLOAD_TRIES - maxTries + 1) & " of upload")
                    myBrowser.uploadFile(curFile)
                    Plugin.log("File uploaded")
                Catch ex As FilesWebException
                    'ZE20037E : Indicate file of same name had already been uploaded (so it's now sure it was uploaded)
                    If ex.Message.IndexOf("ZE20037E") <> -1 Then
                        Plugin.log("File uploaded and confirmed")
                        fileUploadedAndConfirmed = True
                    Else
                        Plugin.log("File upload crashed 1... see errors log")
                        'Other errors from the website (as Closed temporarily)
                        results.Add(New FileResult(True, 0, "", 0, "Téléversement des fichiers", ex.Message, curFile, "", CsstTask.RESULT_ERROR_COLOR, "", Base.LIMIT_DATE, ""))
                        Plugin.log("File upload crashed 1... see errors log - will write error")
                        Base.External.propagateErrorLog(ex)
                        Plugin.log("File upload crashed 1... see errors log end")

                        Exit While
                    End If
                Catch ex As Exception
                    Plugin.log("File upload crashed 2... see error logs")
                    results.Add(New FileResult(True, 0, "", 0, "Téléversement des fichiers", ex.Message, curFile, "", CsstTask.RESULT_UNKNOWN_ERROR_COLOR, "", Base.LIMIT_DATE, ""))
                    Plugin.log("File upload crashed 2... see errors log - will write error")
                    Base.External.propagateErrorLog(New Exception("myBrowser.viewLocation=" & myBrowser.viewLocation, ex))
                    Plugin.log("File upload crashed 2... see errors log end")

                    Exit While
                End Try

                currentPourcent += pourcentToGrow
                RaiseEvent taskProgressed(Me, currentPourcent)

                maxTries -= 1
            End While

            If fileUploadedAndConfirmed Then
                Plugin.log("File will be renamed from CST to DONE")
                IO.File.Move(curFile, curFile.Replace(".cst", ".done"))
                Plugin.log("File renamed from CST to DONE")
                'TODO: Shall be reactivated when all bugs fixed  If IO.File.Exists(saveFile) Then IO.File.Delete(saveFile)

                Plugin.log("File results will be adjusted")
                adjustFileResults(curFile, results)
                Plugin.log("File results adjusted")
            Else
                results.Add(New FileResult(True, 0, "", 0, "Téléversement des fichiers", "Impossible de téléverser le fichier """ & curFile & """ :<br/>. Veuillez le faire manuellement ou tenter d'exécuter à nouveau le processus.", curFile, "", CsstTask.RESULT_ERROR_COLOR, "", Base.LIMIT_DATE, ""))
                Exit For 'Quit to preserve file upload order...
            End If

            nbFilesUploaded += 1
            currentPourcent = (nbFilesUploaded / files.Count) * 100.0
            RaiseEvent taskProgressed(Me, currentPourcent)
        Next

        Plugin.log("-------------------------------------------")
        Plugin.log("Files upload ended")
        Plugin.log("-------------------------------------------")
    End Sub


    Private disposedValue As Boolean = False        ' Pour détecter les appels redondants

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing AndAlso myBrowser IsNot Nothing Then
                myBrowser.Dispose()
                myBrowser = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' Ce code a été ajouté par Visual Basic pour permettre l'implémentation correcte du modèle pouvant être supprimé.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ne modifiez pas ce code. Ajoutez du code de nettoyage dans Dispose(ByVal disposing As Boolean) ci-dessus.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
