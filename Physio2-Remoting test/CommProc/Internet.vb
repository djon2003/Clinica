Imports System.Net
Imports System.Net.Mail

Module Internet

    Public Function extractEmails(ByVal emailStr As String) As String
        Dim sEmails() As String, Emails As String = ""
        Dim Pos, i As Short

        If emailStr.IndexOf(";") <> -1 Then
            sEmails = emailStr.Split(New Char() {";"})
            For i = 0 To sEmails.Length - 1
                If sEmails(i) <> "" Then
                    Pos = sEmails(i).IndexOf("<")
                    If Pos >= 0 Then sEmails(i) = sEmails(i).Substring(Pos + 1)
                    If sEmails(i).EndsWith(">") Then sEmails(i) = sEmails(i).Substring(0, sEmails(i).Length - 1)
                    Emails &= sEmails(i) & ";"
                End If
            Next i
        Else
            Emails = emailStr
            Pos = Emails.IndexOf("<")
            If Pos >= 0 Then Emails = Emails.Substring(Pos)
            If Emails.EndsWith(">") Then Emails = Emails.Substring(0, Emails.Length - 1)
        End If
        If Emails.EndsWith(";") Then Emails = Emails.Substring(0, Emails.Length - 1)

        Return Emails
    End Function


    Private Function _emailSending(ByVal mySmtpClient As SmtpClient, ByVal mailFrom As String, ByVal mailTo As String, ByVal mailCC As String, ByVal mailBCC As String, ByVal mailSubject As String, ByVal isbodyHTML As Boolean, ByVal body As String, Optional ByVal attachements As String = "", Optional ByVal firstStatusText As String = "Envoi du message externe...", Optional ByVal lastStatusText As String = "Message(s) externe(s) envoyé(s)", Optional ByVal showErrMsgBox As Boolean = True, Optional ByVal askReturnReceipt As Boolean = False) As Boolean
        If Not isConnectionAvailable() Then
            If showErrMsgBox Then MessageBox.Show("Impossible d'envoyer un message externe lorsqu'aucune connexion internet est établie", "Envoi impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False
        End If

        If firstStatusText Is Nothing Then firstStatusText = "Envoi du message externe..."
        If lastStatusText Is Nothing Then lastStatusText = "Message(s) externe(s) envoyé(s)"

        Dim sent As Boolean = False
        Dim i As Short
        If firstStatusText <> "" Then myMainWin.StatusText = firstStatusText

        Dim myMail As Net.Mail.MailMessage
        Dim attachPath As String = String.Empty

        'Detect images from DB
        Dim imagesToTranslate As New Generic.Dictionary(Of String, String)
        Dim imgAttrib As String = " imageDBPath=""DB:"
        Dim foundPos As Integer = body.IndexOf(imgAttrib)
        While foundPos <> -1
            foundPos += imgAttrib.Length - 3
            Dim endPos As Integer = body.IndexOf("""", foundPos)
            Dim finalPos As Integer = endPos
            Dim imgURL As String = body.Substring(foundPos, endPos - foundPos)
            foundPos = body.LastIndexOf("src=""", foundPos)
            foundPos += 5 'For tag name
            endPos = body.IndexOf("""", foundPos)
            Dim curURL As String = body.Substring(foundPos, endPos - foundPos)
            If imagesToTranslate.ContainsKey(imgURL) = False Then imagesToTranslate.Add(imgURL, curURL)
            If attachements.IndexOf(imgURL & ";") = -1 Then attachements &= imgURL & ";"

            foundPos = body.IndexOf(imgAttrib, finalPos)
        End While

        'Detect link to DB
        Dim linksToTranslate As New Generic.Dictionary(Of String, String)
        Dim linkTagStart As String = "<A href="""
        Dim linkProtocol As String = WebTextControl.PROTOCOL_CLINICA & "DB"
        foundPos = body.IndexOf(linkTagStart & linkProtocol)
        While foundPos <> -1
            foundPos += linkTagStart.Length
            Dim endPos As Integer = body.IndexOf("""", foundPos)
            Dim linkURL As String = body.Substring(foundPos, endPos - foundPos)
            Dim curURL As String = linkURL
            Dim curDBItem As InternalDBItem = InternalDBItem.getItemFromLink(linkURL)
            If curDBItem IsNot Nothing AndAlso curDBItem.getDBFolder() IsNot Nothing Then
                linkURL = "DB:\" & curDBItem.uniqueNo & "\" & curDBItem.getDBFolder().toString() & "\" & curDBItem.dbItem
                If linksToTranslate.ContainsKey(linkURL) = False Then linksToTranslate.Add(linkURL, curURL)
                If attachements.IndexOf(linkURL & ";") = -1 Then attachements &= linkURL & ";"
            End If

            foundPos = body.IndexOf(linkTagStart & linkProtocol, endPos)
        End While

        Try
            myMail = New MailMessage()
            With myMail
                .From = New MailAddress(mailFrom)
                Dim toEmails() As String = mailTo.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
                For Each curEmail As String In toEmails
                    .To.Add(New MailAddress(curEmail))
                Next
                .Subject = mailSubject.Replace(vbCrLf, " \n ")
                If mailCC <> "" Then
                    Dim ccEmails() As String = mailCC.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
                    For Each curEmail As String In ccEmails
                        .CC.Add(New MailAddress(curEmail))
                    Next
                End If
                If mailBCC <> "" Then
                    Dim bccEmails() As String = mailBCC.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
                    For Each curEmail As String In bccEmails
                        .Bcc.Add(New MailAddress(curEmail))
                    Next
                End If
                .IsBodyHtml = isbodyHTML
                .BodyEncoding = System.Text.Encoding.Default
                If askReturnReceipt Then .Headers.Add("disposition-notification-to", mailFrom)
                .Priority = MailPriority.Normal
                If attachements.Length <> 0 Then
                    Dim sAttach() As String = attachements.Split(New Char() {";"})
                    For i = 0 To sAttach.Length - 1
                        If sAttach(i).Length > 0 Then
                            attachPath = sAttach(i)
                            If sAttach(i).StartsWith("DB:\") Then
                                attachPath = attachPath.Substring(4)
                                Dim curNoDBItem As Integer = attachPath.Substring(0, attachPath.IndexOf("\"))

                                Dim curDBItem As InternalDBItem
                                If InternalDBItem.exists(curNoDBItem) Then
                                    curDBItem = New InternalDBItem(curNoDBItem)
                                Else
                                    Dim folderPath As String = attachPath.Substring(attachPath.IndexOf("\") + 1)
                                    Dim fileName As String = getLastDir(folderPath)
                                    folderPath = folderPath.Substring(0, fileName.Length - 1)
                                    curDBItem = New InternalDBItem(folderPath, fileName)
                                End If

                                attachPath = appPath & bar(appPath) & "DB\" & curDBItem.dbItemFile
                                body = body.Replace(sAttach(i), "")
                            End If
                            Dim mailattachmnt As New System.Net.Mail.Attachment(attachPath)
                            If imagesToTranslate.ContainsKey(sAttach(i)) Then body = body.Replace(imagesToTranslate(sAttach(i)), "cid:" & mailattachmnt.ContentId)
                            If linksToTranslate.ContainsKey(sAttach(i)) Then body = body.Replace(linksToTranslate(sAttach(i)), "cid:" & mailattachmnt.ContentId)
                            .Attachments.Add(mailattachmnt)
                        End If
                    Next i
                End If
                .Body = body
            End With
            mySmtpClient.Send(myMail)

            If lastStatusText <> "" Then myMainWin.StatusText = lastStatusText

            sent = True
        Catch ex As System.IO.FileNotFoundException
            Dim strMessage As String = "Impossible d'envoyer le message, car le fichier suivant n'existe pas:" & vbCrLf & attachPath
            myMainWin.StatusText = strMessage
            If showErrMsgBox Then MessageBox.Show(strMessage, "Erreur de messagerie")

        Catch ex As System.IO.IOException
            Dim strMessage As String = "Impossible d'envoyer le message, car le fichier suivant est en cours d'utilisation:" & vbCrLf & attachPath
            myMainWin.StatusText = strMessage
            If showErrMsgBox Then MessageBox.Show(strMessage, "Erreur de messagerie")

        Catch ex As System.Net.Sockets.SocketException
            Dim strMessage As String = "Impossible de contacter le serveur d'envoi de courriel. Soit le nom de l'hôte ou le port ne sont valides."
            myMainWin.StatusText = strMessage
            If showErrMsgBox Then MessageBox.Show(strMessage, "Erreur de messagerie")

        Catch ex As Exception
            Dim strMessage As String = ex.Message
            If TypeOf ex Is System.Net.Sockets.SocketException Then
                strMessage = "Impossible de contacter le serveur d'envoi de courriel. Soit le nom de l'hôte ou le port ne sont valides soit le serveur SMTP nécessite une authentification."
            Else
                addErrorLog(New Exception("host=" & mySmtpClient.Host & ", mailFrom=" & mailFrom & ", mailTo=" & mailTo & ", mailSubject=" & mailSubject & ", body=" & body, ex))

                'check the InnerException
                While Not (ex.InnerException Is Nothing)
                    ex = ex.InnerException
                    strMessage &= vbCrLf & ex.Message
                End While

                If System.Text.RegularExpressions.Regex.IsMatch(strMessage.Substring(0, 1), "[0-9]+") = True Then strMessage = "Erreur lors de l'envoi du courrier externe." & vbCrLf & "Vérifier les adresses de courriels dans les champs : À, CC, BCC"
            End If
            strMessage = "Erreur lors de l'envoi d'un courriel vers " & mailTo & " : " & vbCrLf & strMessage
            myMainWin.StatusText = strMessage
            If showErrMsgBox Then MessageBox.Show(strMessage, "Erreur de messagerie")
        Finally
            myMail = Nothing
        End Try

        Return sent
    End Function

    Public Function emailSending(ByVal sendingMailAccount As MailAccount, ByVal mailFrom As String, ByVal mailTo As String, ByVal mailCC As String, ByVal mailBCC As String, ByVal mailSubject As String, ByVal isbodyHTML As Boolean, ByVal body As String, Optional ByVal attachements As String = "", Optional ByVal firstStatusText As String = "Envoi du message externe...", Optional ByVal lastStatusText As String = "Message(s) externe(s) envoyé(s)", Optional ByVal showErrMsgBox As Boolean = True, Optional ByVal askReturnReceipt As Boolean = False) As Boolean
        If currentUserName = "Administrateur" AndAlso myMainWin IsNot Nothing Then myMainWin.StatusText = "emailSending : Entered"

        If sendingMailAccount Is Nothing Then
            sendingMailAccount = defaultSMTPServer
            mailFrom = currentClinicName & " <" & currentClinicEmail & ">"
        End If
        If sendingMailAccount Is Nothing Then
            MessageBox.Show("Impossible d'envoyer un courriel, car aucun compte de messagerie ne possède de serveur d'envoi configuré", "Envoi externe impossible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False
        End If

        If currentUserName = "Administrateur" AndAlso myMainWin IsNot Nothing Then myMainWin.StatusText = "emailSending : Before SmtpClient"

        Dim mySmtpMail As New SmtpClient(sendingMailAccount.smtpServer.server, sendingMailAccount.smtpServer.port)
        mySmtpMail.EnableSsl = sendingMailAccount.smtpServer.isSecured
        mySmtpMail.Timeout = sendingMailAccount.timeoutInSeconds * 1000

        If currentUserName = "Administrateur" AndAlso myMainWin IsNot Nothing Then myMainWin.StatusText = "emailSending : Before credentials"

        'Add user/password either from incoming or outgoing
        Try
            If sendingMailAccount.smtpNeedAuthentication Then
                Dim myCredentials As NetworkCredential
                If sendingMailAccount.smtpSpecificCredential Then
                    Dim password As String = decrypt(sendingMailAccount.smtpPassword, sendingMailAccount.smtpPasswordKey)
                    If sendingMailAccount.smtpSavePassword = False OrElse sendingMailAccount.smtpPassword = "" Then
                        password = InputBox("Veuillez entrer le mot de passe du compte de courriel " & sendingMailAccount.toString, "Mot de passe requis")
                        If password = "" Then Return False

                        If sendingMailAccount.smtpSavePassword Then
                            If sendingMailAccount.smtpPasswordKey = "" Then sendingMailAccount.smtpPasswordKey = Chaines.getPasswordKey
                            sendingMailAccount.smtpPassword = encrypt(password, sendingMailAccount.smtpPasswordKey)
                            sendingMailAccount.saveData()
                        End If
                    Else
                        password = decrypt(sendingMailAccount.smtpPassword, sendingMailAccount.smtpPasswordKey)
                    End If

                    myCredentials = New NetworkCredential(sendingMailAccount.smtpAuthenUsername, password)
                Else
                    Dim password As String = ""
                    If sendingMailAccount.savePassword = False OrElse sendingMailAccount.password = "" Then
                        password = InputBox("Veuillez entrer le mot de passe du compte de courriel " & sendingMailAccount.toString, "Mot de passe requis")
                        If password = "" Then Return False

                        If sendingMailAccount.savePassword Then
                            If sendingMailAccount.passwordKey = "" Then sendingMailAccount.passwordKey = Chaines.getPasswordKey
                            sendingMailAccount.password = encrypt(password, sendingMailAccount.passwordKey)
                            sendingMailAccount.saveData()
                        End If
                    Else
                        password = decrypt(sendingMailAccount.password, sendingMailAccount.passwordKey)
                    End If
                    myCredentials = New NetworkCredential(sendingMailAccount.username, password)
                End If
                mySmtpMail.Credentials = myCredentials
            End If

            If mailFrom Is Nothing OrElse mailFrom.Trim = "" Then mailFrom = """" & sendingMailAccount.sendingName & """<" & sendingMailAccount.email & ">"

            If currentUserName = "Administrateur" AndAlso myMainWin IsNot Nothing Then myMainWin.StatusText = "emailSending : Before real sending"

            Return _emailSending(mySmtpMail, mailFrom, mailTo, mailCC, mailBCC, mailSubject, isbodyHTML, body, attachements, firstStatusText, lastStatusText, showErrMsgBox, askReturnReceipt)
        Catch ex As Exception
            If currentUserName = "Administrateur" AndAlso myMainWin IsNot Nothing Then
                myMainWin.StatusText = "emailSending : ERROR : " & ex.Message
                addErrorLog(ex, 0, True)
            End If

            If myMainWin IsNot Nothing Then
                myMainWin.Text = "Assurez vous d'avoir accès aux dossiers suivants (s'ils existent) :" & vbCrLf & _
                "C:\Documents and Settings\All Users\ApplicationData\Microsoft\Crypto\RSA\MachineKeys" & vbCrLf & _
                "C:\Documents and Settings\All Users\Microsoft\Crypto\RSA\MachineKeys"
            End If
            
            Return False
        End Try
        
    End Function


    Public Sub sendEmailTo(ByVal email As String, Optional ByVal isAnswering As Boolean = False, Optional ByVal message As String = "", Optional ByVal subject As String = "")
        'Droit & Accès
        If currentDroitAcces(38) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'envoyer de courriel externe." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        Dim myMSGSending As msgSending = openUniqueWindow(New msgSending)
        myMSGSending.isAnswering = isAnswering
        myMSGSending.setExternalTo(email)
        If subject <> "" Then myMSGSending.setsujet(subject)
        If message <> "" Then myMSGSending.setmessage(message)
        myMSGSending.Show()
    End Sub

End Module
