Imports mshtml
Imports MsHtmHstInterop

Public Class CSSTBrowser
    Inherits Base.Windows.Forms.WebControl

    Private Const CONNECTION_USERNAME_FIELD_ID As String = "cnesstUserNameInput"
    Private Const CONNECTION_PASSWORD_FIELD_ID As String = "passwordInput"
    Private Const CONNECTION_SUBMIT_BUTTON_ID As String = "submitButton"
    Private Const CONNECTION_SAML_TAG As String = "<saml"

    Private Const UPLOAD_URL2 As String = "https://ee.pes.csst.qc.ca/cgi-bin/ge_secur.cgi?page=rp/rpfindex1.htm"
    Private Const VERIFICATION_URL As String = "https://www.pes.csst.qc.ca/employeur/guichet/authentification_messagerie/LoginNotes.aspx?typeVue=$$ViewTemplate+For+($InBox)"
    '"https://www.pes.csst.qc.ca/authentification_messagerie/LoginNotes.aspx?typeVue=$$ViewTemplate+For+($InBox)"

    Private Const DISCONNECTION_URL As String = "https://formauth.pes.csst.qc.ca/adfs/ls/?wa=wsignout1.0"
    Private Const DISCONNECTED_URL As String = "https://www.pes.csst.qc.ca/deconnexion/fermeture"
    Private Const CONNECTION_URL As String = "https://www.pes.csst.qc.ca/aiguilleur/"
    Private Const AFTER_CONNECTION_URL As String = "https://formauth.pes.csst.qc.ca/adfs/ls/"
    Private Const CONNECTED_URL As String = "https://www.pes.csst.qc.ca/employeur/guichet/"
    Private Const MAIL_BASE_URL As String = "https://courrier.pes.csst.qc.ca"
    Private Const MAINTENANCE_PAGE As String = "https://www.pes.csst.qc.ca/commun/pages_communes/Messages/PageMessage.aspx"

    Private Const RETURN_MESSAGE_TAG_START As String = "</TABLE>" & vbCrLf & "<HR>"
    Private Const RETURN_MESSAGE_TAG_END As String = "</FORM>"
    Public Const RETURN_MESSAGE_MARKING As String = "<!-- CSST-MSG -->"

    Private Const IMG_TAG_START As String = "<IMG src="""
    Private Const ANCHOR_URL_ATTRIBUTE As String = "href"
    Private Const RETURN_MESSAGE_ATTACHMENT_URL_START = "/mail/"
    Private Const IMG_URL_ATTRIBUTE As String = "src"

    Private Const CONNECTION_MESSAGE_TAG_START As String = "<SPAN id=lblMessage>"
    Private Const CONNECTION_MESSAGE_TAG_END As String = "</SPAN>"
    Private Const CONNECTION_ERROR_TAG_ID As String = "errorMessage"
    Private Const CONNECTION_ERROR_HAS_TO_DISCONNECT_URL_KEYWORD = "wsignout"
    Private isConnectionErrorHasToDisconnectAlreadyTried As Boolean = False

    Private Const WAITING_TIMEOUT_MS As Integer = 300000

    Private enterFileIntoFormTimeout As Boolean = False
    Private _nbReturns As Integer = 0
    Private waiting As Boolean = True
    Private curFile As String = ""
    Private fileFormButtonClicked As Boolean = False
    Private fileFormButtonClickCancelled As Boolean = False

    Public ReadOnly Property nbReturns() As Integer
        Get
            Return _nbReturns
        End Get
    End Property

    Public Event returnDownloaded()
    Public Event startingReturnDownload()

    Public Sub New()
        Me.Silent = True
        Me.allowPopupWindows = False
        Me.useNavigationCache = False
    End Sub

    Private Sub wait(Optional ByVal skipFlag As Boolean = False)
        If Not skipFlag Then waiting = True

        Dim startingTime As Date = Date.Now
        While waiting
            Application.DoEvents()

            If Date.Now.Subtract(startingTime).TotalMilliseconds > WAITING_TIMEOUT_MS Then
                Throw New TimeoutException("Le délais d'attente pour une page a été atteint. Veuillez contacter CyberInternautes à ce sujet.", New Exception("Me.viewLocation=" & Me.viewLocation))
            End If
        End While
    End Sub

    Private Sub waitScript(ByVal script As String)
        waiting = True

        callViewJavaScript(script)

        wait(True)
    End Sub

    Private Sub waitDocument(ByVal url As String)
        waiting = True

        Me.htmlPageURL = url
        Me.showPage()

        wait(True)
    End Sub

    Public Function connect(ByVal username As String, ByVal password As String) As Boolean
        'Wait twice times for complete load
        waitDocument(CONNECTION_URL)
        If Me.viewLocation.StartsWith(CONNECTED_URL) Then Return True
        If Me.viewLocation.StartsWith(DISCONNECTED_URL) Then 'Disconnected page detected, going back to login page
            Dim nbTries As Integer = 0
            While Me.viewLocation.StartsWith(DISCONNECTED_URL) And nbTries < 10
                waitDocument(CONNECTION_URL & "?curl=Z2F&reason=0&formdir=6")
                nbTries += 1
            End While
        ElseIf Me.viewLocation = "about:blank" Then
            wait()
        End If

        'Look if site is closed for maintenance
        If Not Me.viewLocation.StartsWith(CONNECTED_URL) Then
            Dim html As String = Me.getHTML()
            Dim isUsernameFieldPresent As Boolean = html.IndexOf(CONNECTION_USERNAME_FIELD_ID) <> -1
            Dim isSamlPresent As Boolean = html.IndexOf(CONNECTION_SAML_TAG) <> -1
            If Not isUsernameFieldPresent AndAlso Not isSamlPresent Then
                Dim startingMessagePos As Integer = html.IndexOf(CONNECTION_MESSAGE_TAG_START)
                If startingMessagePos <> -1 Then
                    startingMessagePos += CONNECTION_MESSAGE_TAG_START.Length
                    Dim message As String = "Impossible de se connecter au site de la CSST pour la raison suivante :<br>" & html.Substring(startingMessagePos, html.IndexOf(CONNECTION_MESSAGE_TAG_END, startingMessagePos) - startingMessagePos)
                    Throw New FilesWebException(message)
                Else
                    Base.External.propagateErrorLog(New FilesWebException("CSST-CANT-CONNECT. URL=//" & Me.viewLocation & "//HTML=" & html))
                    Throw New FilesWebException("Impossible de se connecter au site de la CSST pour une raison inconnue")
                End If
            End If

            'Enter login info and submit form
            If isUsernameFieldPresent Then
                Try
                    'Wait for login
                    waitScript("document.getElementById('" & CONNECTION_USERNAME_FIELD_ID & "').value=""" & username & """;document.getElementById('" & CONNECTION_PASSWORD_FIELD_ID & "').value=""" & password & """;document.getElementById('" & CONNECTION_SUBMIT_BUTTON_ID & "').onclick()")
                Catch ex As System.UnauthorizedAccessException
                    Throw New FilesWebException("Impossible de se connecter au site web de la CSST." & vbCrLf & "Veuillez ajouter le site ""https://*.csst.qc.ca"" à la liste des sites de confiance dans Panneau de configuration de Windows >> Options Internet >> Onglet Sécurité")
                End Try
            End If
            If Me.viewLocation.StartsWith(AFTER_CONNECTION_URL) OrElse isSamlPresent Then waitScript("document.forms[0].submit()")
        End If

        'Check if page has changed
        Dim isConnected As Boolean = Me.viewLocation.StartsWith(CONNECTED_URL)

        'Check if error in page
        If Not isConnected AndAlso Me.getHTML().IndexOf(CONNECTION_ERROR_TAG_ID) <> -1 Then
            Dim doc As HTMLDocument = browserForView.Document
            For i As Integer = 0 To doc.all.length - 1
                Dim domElement As IHTMLElement = doc.all.item(i)
                If domElement.id = CONNECTION_ERROR_TAG_ID Then
                    If Not isConnectionErrorHasToDisconnectAlreadyTried AndAlso domElement.innerHTML.IndexOf(CONNECTION_ERROR_HAS_TO_DISCONNECT_URL_KEYWORD) Then
                        isConnectionErrorHasToDisconnectAlreadyTried = True
                        waitDocument(DISCONNECTION_URL)
                        Try
                            isConnected = connect(username, password)
                        Catch ex As Exception
                            Throw ex
                        Finally
                            isConnectionErrorHasToDisconnectAlreadyTried = False
                        End Try
                        Return isConnected
                    End If

                    Dim message As String = "Impossible de se connecter au site de la CSST pour la raison suivante :<br>" & domElement.innerHTML
                    Throw New FilesWebException(message)
                End If
            Next i
        End If

        Return isConnected
    End Function

    Public Function testConnection(ByVal username As String, ByVal password As String) As Boolean
        Dim isOK As Boolean = connect(username, password)
        Me.htmlPageURL = "about:blank"
        Me.showPage()

        Return isOK
    End Function

    Private Sub gotoMailsPage()
        waitDocument(VERIFICATION_URL)

        If Me.viewLocation.StartsWith(MAINTENANCE_PAGE) Then
            Throw New Exception("Impossible d'accéder au site web de la CSST, car il est probablement en maintenance.")
        End If

        wait() 'Waiting page

        Dim html As String = getHTML()
        'These seems to be true only on Chrome (Which doesn't apply here due to IE component)
        'Dim enterMailJS As String = "window.frames['rpfdata'].document.getElementsByTagName('a')[0].click();"
        'callViewJavaScript(enterMailJS)
        If Me.viewLocation.StartsWith(MAIL_BASE_URL) = False Then wait()

        Dim html2 As String = getHTML()
    End Sub

    Public Function getReturn() As List(Of String)
        gotoMailsPage()

        Dim html As String = getHTML()
        Dim baseAddress As String = MAIL_BASE_URL
        Dim returnLinkStartTag As String = "<A href="""
        Dim returnLinkEndTag As String = "?OpenDocument"">"
        Dim returnAddresses As List(Of String) = gatherAddresses(html, baseAddress, returnLinkStartTag, returnLinkEndTag, "?OpenDocument")

        _nbReturns = returnAddresses.Count
        RaiseEvent startingReturnDownload()
        Dim returns As New List(Of String)
        For Each curReturnLink As String In returnAddresses
            returns.AddRange(getOneReturn(curReturnLink))
            RaiseEvent returnDownloaded()
        Next

        Return returns
    End Function

    Private Function gatherLinks(ByVal parent As IHTMLElement, ByVal hrefStartsWith As String, ByVal hrefEndsWith As String) As List(Of HTMLAnchorElement)
        Dim returnAddresses As New List(Of HTMLAnchorElement)

        Dim doc As HTMLDocument = browserForView.Document
        For i As Integer = 0 To doc.links.length - 1
            Dim link As HTMLAnchorElement = doc.links.item(i)
            Dim href As String = getUrlFromElement(link, ANCHOR_URL_ATTRIBUTE)
            If isNodeInParent(link, parent) AndAlso href.StartsWith(hrefStartsWith) AndAlso href.EndsWith(hrefEndsWith) Then
                returnAddresses.Add(link)
            End If
        Next i

        Return returnAddresses
    End Function

    Private Function gatherAddresses(ByVal html As String, ByVal baseAddress As String, ByVal returnLinkStartTag As String, ByVal returnLinkEndTag As String, ByVal endAddress As String) As List(Of String)
        Dim returnAddresses As New List(Of String)

        Dim curPos As Integer = html.IndexOf(returnLinkEndTag)
        Dim endPos As Integer = 0
        Dim url As String
        While curPos <> -1
            endPos = curPos
            curPos = html.LastIndexOf(returnLinkStartTag, curPos) + returnLinkStartTag.Length

            url = baseAddress & html.Substring(curPos, endPos - curPos) & endAddress
            returnAddresses.Add(url)

            curPos = html.IndexOf(returnLinkEndTag, endPos + 1)
        End While

        Return returnAddresses
    End Function

    Private Function getMessage() As String
        Dim doc As HTMLDocument = browserForView.Document
        Dim tables As IHTMLElementCollection = doc.getElementsByTagName("table")
        If tables.length < 4 Then Throw New FilesWebException("Courrier de type inconnu sur le site web de la CSST. Veuillez contacter CyberInternautes.")
        Dim table As HTMLTable = tables.item(3)
        
        'Look for attachment files and replace them by their base 64 version
        Dim attachments As List(Of HTMLAnchorElement) = gatherLinks(table, RETURN_MESSAGE_ATTACHMENT_URL_START, String.Empty)
        If attachments.Count <> 0 Then
            For i As Integer = 0 To attachments.Count - 1
                Dim fileBytes() As Byte = downloadFile(attachments(i).href)
                Dim fileBase64 As String = Convert.ToBase64String(fileBytes)

                attachments(i).setAttribute("download", attachments(i).title)
                attachments(i).href = "data:application/octet-stream;base64," & fileBase64
                attachments(i).innerHTML = "<span style=""display:none"">" & fileBase64 & "</span>" & attachments(i).innerHTML
            Next
        End If

        'Replace images by their base 64 version
        Dim urlDone As New List(Of String)
        For Each img As mshtml.IHTMLImgElement In doc.images
            If isNodeInParent(img, table) Then
                Dim imgUrl As String = getUrlFromElement(img, IMG_URL_ATTRIBUTE)
                urlDone.Add(imgUrl)

                Dim fileBytes() As Byte = downloadFile(img.src)
                Dim imgBase64 As String = Convert.ToBase64String(fileBytes)
                imgBase64 = "data:image/jpeg;base64," & imgBase64
                img.src = imgBase64
            End If
        Next

        Dim html As String = CSSTBrowser.RETURN_MESSAGE_MARKING & tables.item(1).outerHTML & tables.item(2).outerHTML & table.outerHTML
        Return html
    End Function

    Private Function getOneReturn(ByVal url As String) As List(Of String)
        waitDocument(url)

        Dim html As String = getHTML()
        Dim baseAddress As String = MAIL_BASE_URL
        Dim returnLinkStartTag As String = "href="""
        Dim returnLinkEndTag As String = ".rap""><IMG"
        Dim returnAddresses As List(Of String) = gatherAddresses(html, baseAddress, returnLinkStartTag, returnLinkEndTag, ".rap")
        returnLinkEndTag = ".rap?OpenElement""><IMG" 'It seems that some clinic have this link end tag instead of first one
        returnAddresses.AddRange(gatherAddresses(html, baseAddress, returnLinkStartTag, returnLinkEndTag, ".rap"))

        Dim returns As New List(Of String)
        If returnAddresses.Count = 0 Then
            'If no reports return, then it's a message
            returns.Add(getMessage())
        Else
            'Get the report return
            Dim startTag As String = "<PRE>"
            Dim endTag As String = "</PRE>"
            Dim returned As String = ""
            For Each curReturnLink As String In returnAddresses
                waitDocument(curReturnLink)

                returned = getHTML()
                returned = returned.Substring(returned.IndexOf(startTag) + startTag.Length)
                returned = returned.Substring(0, returned.IndexOf(endTag))

                returns.Add(returned)
            Next
        End If

        Return returns
    End Function


    Public Sub uploadFile(ByVal file As String)
        curFile = file

        Plugin.log("Will load upload page")
        'Upload url page loading
        waitDocument(UPLOAD_URL2)
        Plugin.log("Upload page loaded")

        If Not viewLocation.StartsWith(UPLOAD_URL2) Then
            Plugin.log("Will throw error of not good page" & vbCrLf & "->viewLocation=" & viewLocation & vbCrLf & "->=UPLOAD_URL2" & UPLOAD_URL2)
            Throw New FilesWebException("La page d'envoi de fichier sur le site web de la CSST ne fonctionne pas présentement. Veuillez réessayer plus tard et/ou appeler la CSST pour obtenir plus d'informations.")
        End If

        'Start thread to wait for file input window
        Plugin.log("starting enterFile thread")
        Dim enterFileThread As New Threading.Thread(AddressOf enterFileIntoForm)
        enterFileThread.Start()

        'Click on the "upload choose file" button
        Dim uploadFieldSelectionJS As String = "window.frames['rpfdata'].document.getElementsByName('client-file')[0].click();"
        Try
            Plugin.log("first attempt to click on button of file selection")
            callViewJavaScript(uploadFieldSelectionJS)
        Catch ex As System.Runtime.InteropServices.COMException
            'Retry in case it works...
            Try
                Plugin.log("second attempt to click on button of file selection")
                callViewJavaScript(uploadFieldSelectionJS)
            Catch ex1 As Exception
                Plugin.log("second attempt failed")
                fileFormButtonClickCancelled = True
                Throw ex1
            End Try
        End Try
        Plugin.log("click on button of file selection done")
        fileFormButtonClicked = True

        'Wait for the file input to be filled automatically
        enterFileThread.Join()
        Plugin.log("enter file thread joined")
        If enterFileIntoFormTimeout Then
            Plugin.log("timeout reached on file enter")
            Throw New FilesWebException("Impossible d'envoyer un fichier vers le site de la CSST. Veuillez réessayer ou contacter CyberInternautes si le problème persiste.")
        End If

        Plugin.log("calling doevents")
        Application.DoEvents()

        'Submit upload form
        Plugin.log("will click on send button")
        waitScript("var ele = window.frames['rpfdata'].document.getElementsByTagName('INPUT');ele[ele.length-1].click();")
        Plugin.log("send button clicked")

        Plugin.log("will get html of upload result")
        Dim resultPageHTML As String = Me.getHTML("rpfdata")
        Plugin.log("html of upload result gathered")
        If resultPageHTML.IndexOf("***ERREUR***") <> -1 OrElse resultPageHTML.IndexOf("***ERROR***") <> -1 Then
            Dim matches As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(resultPageHTML, ".*?<body.*?>(.*?)</body>.*?", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Singleline)

            resultPageHTML = matches.Groups(1).Value
            Throw New FilesWebException(resultPageHTML)
        End If
    End Sub

    Private Sub enterFileIntoForm()
        While Windows.Forms.Form.ActiveForm IsNot Nothing AndAlso (fileFormButtonClicked = False OrElse fileFormButtonClickCancelled = False)
            Threading.Thread.Sleep(100)
        End While

        If fileFormButtonClickCancelled = True Then Exit Sub 'Quit because JS to call button had crashed twice

        Threading.Thread.Sleep(1000) 'REALLY IMPORTANT !!! Otherwise, an AccessViolationException is thrown by the JS to call the upload button

        enterFileIntoFormTimeout = False
        Dim nbLoop As Integer = 1

        Dim timeout As Integer = WAITING_TIMEOUT_MS / 1000
        While autoFillFileSelection(curFile) = False
            Threading.Thread.Sleep(100)

            If timeout = nbLoop Then
                enterFileIntoFormTimeout = True
                Exit While
            End If
            nbLoop += 1
        End While
    End Sub

    Public Sub deleteMails()
        gotoMailsPage()

        callViewJavaScript("var ele = document.getElementsByName('docs');for (var i =0 ; i < ele.length; i++) {ele[i].checked = true;}")
        callViewJavaScript("var ele = document.getElementsByTagName('a');for (var i =0 ; i < ele.length; i++) {if (ele[i].innerHTML.match('Supprimer$')=='Supprimer') {ele[i].click();break;}}")

        Application.DoEvents() 'Give time to JS to execute
    End Sub

    Private Sub CSSTBrowser_DocumentComplete(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent) Handles Me.DocumentComplete
        waiting = False
    End Sub
End Class
