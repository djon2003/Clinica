Imports System.Net
Imports System.Net.Mail

Module Internet
    Private Function _emailSending(ByVal mySmtpClient As SmtpClient, ByVal mailFrom As String, ByVal mailTo As String, ByVal mailCC As String, ByVal mailBCC As String, ByVal mailSubject As String, ByVal isbodyHTML As Boolean, ByVal body As String, ByVal sentDate As Date, Optional ByVal attachements As String = "", Optional ByVal firstStatusText As String = "Envoi du message externe...", Optional ByVal lastStatusText As String = "Message(s) externe(s) envoyé(s)", Optional ByVal showErrMsgBox As Boolean = True, Optional ByVal askReturnReceipt As Boolean = False) As Boolean
        If firstStatusText Is Nothing Then firstStatusText = "Envoi du message externe..."
        If lastStatusText Is Nothing Then lastStatusText = "Message(s) externe(s) envoyé(s)"

        Dim sent As Boolean = False
        Dim i As Short

        Dim myMail As Net.Mail.MailMessage
        Try
            myMail = New MailMessage()
            With myMail
                .From = New MailAddress(mailFrom)
                Dim toEmails() As String = mailTo.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
                For Each curEmail As String In toEmails
                    .To.Add(curEmail)
                Next
                .Subject = mailSubject.Replace(vbCrLf, " \n ")
                If mailCC <> "" Then
                    Dim ccEmails() As String = mailCC.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
                    For Each curEmail As String In ccEmails
                        .CC.Add(curEmail)
                    Next
                End If
                If mailBCC <> "" Then
                    Dim bccEmails() As String = mailBCC.Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries)
                    For Each curEmail As String In bccEmails
                        .Bcc.Add(curEmail)
                    Next
                End If
                .IsBodyHtml = isbodyHTML
                .BodyEncoding = System.Text.Encoding.Default
                If askReturnReceipt Then .Headers.Add("disposition-notification-to", mailFrom)
                .Priority = MailPriority.Normal
                .Body = body
            End With
            Dim currentTime As Date = Date.Now
            TimeOfDay = sentDate
            Microsoft.VisualBasic.DateString = sentDate.ToString("MM-dd-yyyy")
            mySmtpClient.Send(myMail)
            Threading.Thread.Sleep(500)
            TimeOfDay = currentTime
            Microsoft.VisualBasic.DateString = currentTime.ToString("MM-dd-yyyy")


            sent = True
        Catch ex As System.Net.Sockets.SocketException
            Dim strMessage As String = "Impossible de contacter le serveur d'envoi de courriel. Soit le nom de l'hôte ou le port ne sont valides."
            If showErrMsgBox Then MessageBox.Show(strMessage, "Erreur de messagerie")

        Catch ex As Exception
            Dim strMessage As String = ex.Message
            If TypeOf ex Is System.Net.Sockets.SocketException Then
                strMessage = "Impossible de contacter le serveur d'envoi de courriel. Soit le nom de l'hôte ou le port ne sont valides soit le serveur SMTP nécessite une authentification."
            Else
                'check the InnerException
                While Not (ex.InnerException Is Nothing)
                    ex = ex.InnerException
                    strMessage &= vbCrLf & ex.Message
                End While

                If System.Text.RegularExpressions.Regex.IsMatch(strMessage.Substring(0, 1), "[0-9]+") = True Then strMessage = "Erreur lors de l'envoi du courrier externe." & vbCrLf & "Vérifier les adresses de courriels dans les champs : À, CC, BCC"
            End If
            strMessage = "Erreur lors de l'envoi d'un courriel vers " & mailTo & " : " & vbCrLf & strMessage
            If showErrMsgBox Then MessageBox.Show(strMessage, "Erreur de messagerie")
        Finally
            myMail = Nothing
        End Try

        Return sent
    End Function

    Public Function emailSending(ByVal sendingMailAccount As MailAccount, ByVal mailFrom As String, ByVal mailTo As String, ByVal mailCC As String, ByVal mailBCC As String, ByVal mailSubject As String, ByVal isbodyHTML As Boolean, ByVal body As String, ByVal sentDate As Date, Optional ByVal attachements As String = "", Optional ByVal firstStatusText As String = "Envoi du message externe...", Optional ByVal lastStatusText As String = "Message(s) externe(s) envoyé(s)", Optional ByVal showErrMsgBox As Boolean = True, Optional ByVal askReturnReceipt As Boolean = False) As Boolean
        Dim mySmtpMail As New SmtpClient(sendingMailAccount.smtpServer.server, sendingMailAccount.smtpServer.port)
        mySmtpMail.EnableSsl = sendingMailAccount.smtpServer.isSecured
        mySmtpMail.Timeout = sendingMailAccount.timeoutInSeconds * 1000

        Return _emailSending(mySmtpMail, """" & sendingMailAccount.sendingName & """<" & sendingMailAccount.email & ">", mailTo, mailCC, mailBCC, mailSubject, isbodyHTML, body, sentDate, attachements, firstStatusText, lastStatusText, showErrMsgBox, askReturnReceipt)
    End Function

End Module
