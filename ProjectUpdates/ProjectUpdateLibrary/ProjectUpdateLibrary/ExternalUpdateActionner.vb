Friend Class ExternalUpdateActionner

#Region "Definitions"
    Private Const DEFAULT_CHANNEL = "stable"
    Private Const URL_PARAM_UPDATE_FOLDER = "uf"
    Private Const URL_PARAM_FILE_FOLDER = "f"
    Private Const URL_PARAM_FILE_NAME = "file"
    Private Const URL_PARAM_ACTION = "a"
    Private Const URL_PARAM_SOFTWARE_KEY = "s"
    Private Const URL_PARAM_USER_SOFTWARE_KEY = "us"
    Private Const URL_PARAM_USERNAME = "l"
    Private Const URL_PARAM_PASSWORD = "p"
    Private Const URL_PARAM_UPDATE_VERSION = "ve"
    Private Const URL_PARAM_USER_TYPE = "t"
    Private Const URL_PARAM_UPDATE_CHANNEL = "c"
    Private Const URL_PARAM_SERVER_IS_OFF = "off"

    Private _updateURL As String = "", _softKey As String = "", _channel As String = DEFAULT_CHANNEL
    Private _username As String = "", _password As String = ""
    Private _userType As ExternalUpdateUserTypes = ExternalUpdateUserTypes.Administrator
    Private updateTypes As New Generic.Dictionary(Of String, ExternalUpdateUserTypes)
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(ByVal curUpdateRow As DataRow)
        updateTypes.Add("Admin", ExternalUpdateUserTypes.Administrator)
        updateTypes.Add("User", ExternalUpdateUserTypes.User)

        _updateURL = curUpdateRow("UpdateURL")
        _channel = curUpdateRow("DefaultChannel")
        _username = curUpdateRow("UpdateUsername")
        _password = curUpdateRow("UpdatePassword")
        _softKey = curUpdateRow("SoftKey")
        _userType = updateTypes(curUpdateRow("UpdateType"))
    End Sub
#End Region

#Region "Properties"
    Public Property userType() As ExternalUpdateUserTypes
        Get
            Return _userType
        End Get
        Set(ByVal value As ExternalUpdateUserTypes)
            _userType = value
        End Set
    End Property

    Public Property username() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property

    Public Property password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Public Property channel() As String
        Get
            Return _channel
        End Get
        Set(ByVal value As String)
            If value Is Nothing OrElse value.Trim = "" Then
                _channel = DEFAULT_CHANNEL
            Else
                _channel = value
            End If
        End Set
    End Property

    Public Property softKey() As String
        Get
            Return _softKey
        End Get
        Set(ByVal value As String)
            _softKey = value
        End Set
    End Property

    Public Property updateURL() As String
        Get
            Return _updateURL
        End Get
        Set(ByVal value As String)
            _updateURL = value.Trim
        End Set
    End Property
#End Region

    Private Function getWebFunctionReturn(ByVal functionToDo As ExternalUpdateActions, ByVal wc As Net.WebClient, Optional ByVal params As Generic.Dictionary(Of String, String) = Nothing) As String
        Dim createdWC As Boolean = False
        If wc Is Nothing Then
            wc = New Net.WebClient()
            createdWC = True
        End If

        Dim functionReturn As String = ""
        Try
            functionReturn = wc.DownloadString(getWebFunctionUrl(functionToDo, params))
        Catch ex As Net.WebException
            If ex.Message.IndexOf("401") > 0 OrElse ex.Message.IndexOf("404") > 0 Then
                Throw New ExternalUpdateException(ExternalUpdateException.ErrorTypes.UPDATES_FILE_CONTAINS_WRONG_XML_ENTRY)
            End If

            If ex.Status = Net.WebExceptionStatus.NameResolutionFailure Then
                Throw New ExternalUpdateException(ExternalUpdateException.ErrorTypes.NO_INTERNET_CONNECTION)
            End If

            Throw ex
        Finally
            If createdWC Then wc.Dispose()
        End Try

        functionReturn = Text.Encoding.UTF8.GetString(Text.Encoding.Default.GetBytes(functionReturn))

        Return functionReturn
    End Function

    Private Function getWebFunctionUrl(ByVal functionToDo As ExternalUpdateActions, Optional ByVal params As Generic.Dictionary(Of String, String) = Nothing) As String
        Dim paramsStr As String = ""
        If params IsNot Nothing Then
            For Each curKey As String In params.Keys
                paramsStr &= "&" & curKey & "=" & Uri.EscapeUriString(params(curKey))
            Next
        End If

        Dim url As String = _updateURL & "?" & URL_PARAM_ACTION & "=" & functionToDo & "&" & _
                                URL_PARAM_USER_TYPE & "=" & userType & "&" & _
                                URL_PARAM_USERNAME & "=" & Uri.EscapeUriString(username) & "&" & _
                                URL_PARAM_PASSWORD & "=" & Uri.EscapeUriString(password) & "&" & _
                                URL_PARAM_UPDATE_CHANNEL & "=" & Uri.EscapeUriString(channel) & "&" & _
                                If(userType = ExternalUpdateUserTypes.Administrator, URL_PARAM_SOFTWARE_KEY, URL_PARAM_USER_SOFTWARE_KEY) & "=" & softKey & _
                                paramsStr

        Return url
    End Function

    Public Function checkForMajorUpdate(ByVal fromVersion As Integer, ByVal toVersion As Integer, Optional ByVal wc As Net.WebClient = Nothing) As Integer
        Dim params As New Dictionary(Of String, String)
        params.Add("minVer", fromVersion)
        params.Add("maxVer", toVersion)
        Dim functionReturn As String = getWebFunctionReturn(ExternalUpdateActions.getMajorUpdate, wc, params)

        Return functionReturn
    End Function

    Public Function getVersion(Optional ByVal wc As Net.WebClient = Nothing, Optional ByVal nextVersion As Boolean = False) As ExternalUpdateSoftwareInfos
        Dim params As New Dictionary(Of String, String)
        params.Add("next", nextVersion)

        Dim functionReturn As String = getWebFunctionReturn(ExternalUpdateActions.getVersion, wc, params)
        Try
            Dim infos As New ExternalUpdateSoftwareInfos(functionReturn)
            Return infos
        Catch ex As Exception
            'Bug search
            Throw New Exception("url=" & getWebFunctionUrl(ExternalUpdateActions.getVersion, params) & vbCrLf & "functionReturn=" & functionReturn, ex)
        End Try
    End Function

    Public Function getServerIsOff(Optional ByRef onlineTime As Date = Nothing, Optional ByVal wc As Net.WebClient = Nothing) As Boolean
        Dim returnedOnlineTime As String = getWebFunctionReturn(ExternalUpdateActions.isServerOff, wc)
        If returnedOnlineTime <> "" Then
            onlineTime = Date.Parse(returnedOnlineTime)

            Return True
        End If

        Return False
    End Function

    Public Sub setServerIsOff(ByVal isOff As Boolean, ByVal dateServerOn As Date, Optional ByVal wc As Net.WebClient = Nothing)
        Dim params As New Dictionary(Of String, String)
        If isOff Then params.Add("off", dateServerOn.ToString("yyyy-MM-dd HH:mm"))
        getWebFunctionReturn(ExternalUpdateActions.setServerOff, wc, params)
    End Sub

    Public Sub upgradeVersion(Optional ByVal wc As Net.WebClient = Nothing)
        getWebFunctionReturn(ExternalUpdateActions.upgradeVersion, wc)
    End Sub

    'Another example of this code: https://github.com/MuhammadUsmaann/File-Uploading-using-VB-PHP/blob/master/UploadFile/Module1.vb
    Public Sub uploadFile(ByVal filepath As String, ByVal relativeFolder As String, ByVal updateFolder As String)
        Dim params As New Dictionary(Of String, String)
        params.Add(URL_PARAM_FILE_FOLDER, relativeFolder)
        params.Add(URL_PARAM_UPDATE_FOLDER, updateFolder)

        'Create headers
        Dim boundary As String = IO.Path.GetRandomFileName
        Dim header As New System.Text.StringBuilder()
        header.AppendLine("--" & boundary)
        header.Append("Content-Disposition: form-data; name=""userfile"";")
        header.AppendFormat("filename=""{" & 0 & "}""", IO.Path.GetFileName(filepath))
        header.AppendLine()
        header.AppendLine("Content-Type: application/octet-stream")
        header.AppendLine()

        'Transpose headers/footer in bytes
        Dim headerbytes() As Byte = System.Text.Encoding.UTF8.GetBytes(header.ToString)
        Dim endboundarybytes() As Byte = System.Text.Encoding.ASCII.GetBytes(vbNewLine & "--" & boundary & "--" & vbNewLine)
        Dim filebytes() As Byte = My.Computer.FileSystem.ReadAllBytes(filepath)

        'Create request
        Try
            Dim req As Net.HttpWebRequest = Net.HttpWebRequest.Create(getWebFunctionUrl(ExternalUpdateActions.uploadFile, params))
            req.ServicePoint.Expect100Continue = False
            req.ContentType = "multipart/form-data; boundary=" & boundary
            req.Method = "POST"

            ''''
            ' I tried to make work the ContentLength header, but it doesn't with a file over 65KB
            ' Anyhow, without AllowWriteStreamBuffering==False, ContentLength is included automatically, but would be great to have this property to False.
            ''''
            'req.ContentLength = headerbytes.Length + filebytes.Length + endboundarybytes.Length
            'req.AllowWriteStreamBuffering = False
            'req.KeepAlive = True
            'req.AllowAutoRedirect = True
            'req.Timeout = 600000

            'Write file to stream + headers/footer
            Dim bLength As Integer
            Using s As IO.Stream = req.GetRequestStream
                s.WriteTimeout = 600000
                s.Write(headerbytes, 0, headerbytes.Length)
                For i As Integer = 0 To filebytes.Length Step 1000
                    bLength = filebytes.Length - i
                    If bLength > 1000 Then bLength = 1000
                    s.Write(filebytes, i, bLength)
                    s.Flush()
                Next
                s.Write(endboundarybytes, 0, endboundarybytes.Length)
                s.Flush()
            End Using

            'Get the response of these request
            Dim response As Net.WebResponse = Nothing
            Try

                response = req.GetResponse()

                Using responseStream As IO.Stream = response.GetResponseStream()

                    Using responseReader As New IO.StreamReader(responseStream)

                        Dim responseText = responseReader.ReadToEnd()
                        If responseText.IndexOf("has been uploaded") = -1 Then
                            Console.WriteLine(response)
                        End If

                    End Using

                End Using

            Catch exception As Net.WebException

                response = exception.Response

                If (response IsNot Nothing) Then

                    Using reader As New IO.StreamReader(response.GetResponseStream())

                        Dim responseText = reader.ReadToEnd()
                        Diagnostics.Debug.Write(responseText)

                    End Using

                    response.Close()

                End If
            End Try
        Catch ex As Exception
            Console.WriteLine("Failed")
        End Try
    End Sub

    Public Sub prepareFiles(ByVal version As Integer, ByVal updateFolder As String, ByRef filesToDownload As Generic.Dictionary(Of String, String), ByRef filesToDelete As Generic.Dictionary(Of String, String), Optional ByVal wc As Net.WebClient = Nothing)
        If wc Is Nothing Then wc = New Net.WebClient()
        If filesToDownload Is Nothing Then filesToDownload = New Generic.Dictionary(Of String, String)
        If filesToDelete Is Nothing Then filesToDelete = New Generic.Dictionary(Of String, String)

        'Download files list
        Dim filesListContent As String = ""
        Dim params As New Dictionary(Of String, String)
        params.Add(URL_PARAM_UPDATE_FOLDER, updateFolder)
        params.Add(URL_PARAM_UPDATE_VERSION, version)
        filesListContent = getWebFunctionReturn(ExternalUpdateActions.getVersionFiles, wc, params)

        If filesListContent = "" Then Exit Sub

        'Add files to Dictionary and update existing ones
        Dim filesInListFile() As String = System.Text.RegularExpressions.Regex.Split(filesListContent, "\n")
        For j As Integer = 0 To filesInListFile.Length - 1
            'Split data 
            Dim file() As String = filesInListFile(j).Split(New Char() {"§"})
            If file(0).Trim = "major.update" Then Continue For 'Skip major update files

            Dim action As FileActions = FileActions.Add
            If file.Length >= 6 Then action = file(5)

            Dim modifiedFileLine As String = updateFolder & addSlash(updateFolder, False) & version & "/" & filesInListFile(j)
            Select Case action
                Case FileActions.Add
                    'Ensure not in delete list
                    If filesToDelete.ContainsKey(file(0)) Then filesToDelete.Remove(file(0))

                    'Add / change files list
                    If filesToDownload.ContainsKey(file(0)) Then
                        filesToDownload(file(0)) = modifiedFileLine
                    Else
                        filesToDownload.Add(file(0), modifiedFileLine)
                    End If

                Case FileActions.Delete
                    'Ensure not in delete list
                    If filesToDownload.ContainsKey(file(0)) Then filesToDownload.Remove(file(0))

                    'Ensure in delete list
                    If filesToDelete.ContainsKey(file(0)) = False Then filesToDelete.Add(file(0), modifiedFileLine)
            End Select

        Next j
    End Sub


    Public Sub downloadFiles(ByVal dirRepository As String, ByVal version As Integer, ByVal updateFolder As String, ByRef filesToDownload As Generic.Dictionary(Of String, String), ByRef filesToDelete As Generic.Dictionary(Of String, String), Optional ByVal wc As Net.WebClient = Nothing)
        prepareFiles(version, updateFolder, filesToDownload, filesToDelete, wc)

        Dim arrFilesToDownload() As String
        ReDim arrFilesToDownload(filesToDownload.Values.Count - 1)
        filesToDownload.Values.CopyTo(arrFilesToDownload, 0)

        _downloadFiles(dirRepository, String.Join(vbCrLf, arrFilesToDownload), wc)
    End Sub

    Public Sub downloadFiles(ByVal dirRepository As String, ByVal strFilesToDownload As String, ByVal wc As Net.WebClient)
        _downloadFiles(dirRepository, strFilesToDownload, wc)
    End Sub


    Private Sub _downloadFiles(ByVal dirRepository As String, ByVal strFilesToDownload As String, ByVal wc As Net.WebClient)
        If wc Is Nothing Then wc = New Net.WebClient()
        dirRepository = dirRepository.Replace("/", "\")
        dirRepository = dirRepository & IIf(dirRepository.EndsWith("\"), "", "\")

        'Download files
        If strFilesToDownload <> "" Then
            Dim params As New Dictionary(Of String, String)
            params.Add(URL_PARAM_FILE_NAME, "")
            Dim updaterList As String = ""
            Dim filesToDownload() As String = System.Text.RegularExpressions.Regex.Split(strFilesToDownload, "\n")
            For j As Integer = 0 To filesToDownload.GetUpperBound(0)
                'Split data  + create dir if necessary
                Dim file() As String = filesToDownload(j).Trim().Split(New Char() {"§"})
                Dim diskFileName As String = file(1).Replace(Chr(13), "").Replace("/", "\")
                If diskFileName.IndexOf("\") > 0 Then IO.Directory.CreateDirectory(dirRepository & diskFileName.Substring(0, diskFileName.LastIndexOf("\")))

                Try
                    'Download file
                    diskFileName = IIf(file(3) = True, diskFileName & ".new", diskFileName)
                    params(URL_PARAM_FILE_NAME) = file(0)
                    Dim downloadURL As String = getWebFunctionUrl(ExternalUpdateActions.downloadFile, params)
                    If file(4).ToUpper = "BINARY" Then
                        wc.DownloadFile(downloadURL, dirRepository & diskFileName)
                    Else
                        Dim asciiFileContent As String = wc.DownloadString(downloadURL)
                        IO.File.WriteAllText(dirRepository & diskFileName, asciiFileContent, System.Text.Encoding.GetEncoding(1252))
                    End If

                    'Ensure file has good size
                    Dim newFile As New IO.FileInfo(dirRepository & diskFileName)
                    If newFile.Length <> file(2) Then Throw New Net.WebException("File downloaded is corrupted : " & file(0) & vbCrLf & "Size in files.list=" & file(2) & vbCrLf & "Size of download=" & newFile.Length)

                    'Add to updater list if necessary
                    If file(3) = True Then updaterList &= "§" & file(1) & ".new" & "|" & file(1)
                Catch ex As Net.WebException
                    Throw New Exception("Downloading file : " & file(0), ex)
                End Try
            Next j

            If updaterList <> "" Then
                updaterList = updaterList.Substring(1).Replace("§", vbCrLf)
                IO.File.WriteAllText(dirRepository & "updater.list", updaterList)
            ElseIf IO.File.Exists(dirRepository & "updater.list") Then
                IO.File.Delete(dirRepository & "updater.list")
            End If
        End If
    End Sub
End Class