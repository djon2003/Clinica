Public Class ErrorsManager


    Private Shared mySelf As ErrorsManager
    Private Const contentPath As String = "C:\DropBox\CI\Projects\Physio2-Remoting test\§ Utils\§ ErrorLog To check"
    Private Const baseEmailHeaders As String = "MIME-version: 1.0" & vbCrLf & "Content-type: text/plain; charset=utf-8" & vbCrLf & "Content-transfer-encoding: quoted-printable" & vbCrLf & "From: Errors importer <djon@cints.net>" & vbCrLf & "To: clinica-error@cints.net" & vbCrLf

    Private _errors As New Generic.List(Of Erreur)
    Private sorted As Boolean = False

    Public Sub clearErrors()
        _errors.Clear()
    End Sub

    Public ReadOnly Property errors() As Generic.List(Of Erreur)
        Get
            If sorted = False Then
                _errors.Sort()
                sorted = True
            End If

            Return _errors
        End Get
    End Property

    Private Sub New()
        IO.Directory.CreateDirectory(contentPath)
    End Sub

    Public Shared Function getInstance() As ErrorsManager
        If mySelf Is Nothing Then mySelf = New ErrorsManager

        Return mySelf
    End Function

    Private Sub loadErrors()
        Dim files() As String = IO.Directory.GetFiles(contentPath, "*.err", IO.SearchOption.AllDirectories)
        For Each curFile As String In files
            Dim loadError As New Erreur(IO.File.ReadAllText(curFile))
            _errors.Add(loadError)
        Next
    End Sub

    Private Function addToExisting(ByVal newError As Erreur, ByRef errors As Generic.List(Of Erreur)) As Boolean
        If newError.message = "The INSERT statement conflicted with the FOREIGN KEY constraint ""FK_StatVisites_InfoVisites"". The conflict occurred in database ""Clinica"", table ""dbo.InfoVisites"", column 'NoVisite'." Then
            Dim a As Byte = 0
        End If
        For Each curError As Erreur In errors
            If curError.equals(newError) AndAlso (errors.Equals(_errors) OrElse curError.message = newError.message) Then
                If errors.Equals(_errors) AndAlso addToExisting(newError, curError.identicalErrors) Then Return True

                curError.identicalErrors.Add(newError)
                If newError.date > curError.lastDate Then curError.lastDate = newError.date
                Return True
            End If
        Next

        Return False
    End Function

    Public Sub addError(ByVal newError As Erreur)
        If addToExisting(newError, _errors) Then Exit Sub

        sorted = False
        _errors.Add(newError)
    End Sub

    Public Sub writeErrorsToEmail(ByVal outputDir As String)
        If outputDir.EndsWith("\") = False Then outputDir &= "\"

        If IO.Directory.Exists(outputDir) Then
            My.Computer.FileSystem.DeleteDirectory(outputDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If

        Threading.Thread.Sleep(1000)

        IO.Directory.CreateDirectory(outputDir)

        Dim n As Integer = 1
        For Each curError As Erreur In _errors
            writeOneErrorToEmail(curError, outputDir, n)
            n += 1
        Next
    End Sub

    Private monthsAbbr() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}

    Private Sub writeOneErrorToEmail(ByVal curError As Erreur, ByVal outputDir As String, ByRef index As String)
        Dim curFileContent As String = baseEmailHeaders & _
                                        "Date: " & curError.date.ToString("dd ## yyyy HH:mm:ss").Replace("##", monthsAbbr(curError.date.Month - 1)) & " -0400" & vbCrLf & _
                                        "Subject: [Clinica] Error : " & curError.message & vbCrLf & vbCrLf
        'curFileContent &= getEmailBody(curError).Replace("=", "=3D")
        curFileContent &= curError.fullContent.Replace("=", "=3D")

        Dim outputFile As String = outputDir & index & ".eml"
        IO.File.WriteAllText(outputFile, curFileContent)

        For Each inError As Erreur In curError.identicalErrors
            index += 1
            writeOneErrorToEmail(inError, outputFile & "-", index)
        Next
    End Sub

    Public Sub sendErrorsByEmail()
        For Each curError As Erreur In _errors
            sendOneErrorByEmail(curError)
        Next
    End Sub

    'Private Function getEmailBody(ByVal curError As Erreur) As String
    '    Dim body As String = "Date : " & curError.date.ToString("yyyy-MM-dd HH:mm:ss") & vbCrLf & _
    '                "Version de Clinica : " & curError.clinicaVersion & vbCrLf & _
    '                "Ordinateur: " & curError.computer & vbCrLf & _
    '                "Utilisateur(Windows) : " & curError.winUser & vbCrLf & _
    '                "Utilisateur Clinica : " & curError.clinicaUser & vbCrLf & _
    '                "Message : " & vbCrLf & curError.message & vbCrLf & vbCrLf & curError.content

    '    Return body
    'End Function

    Private Sub sendOneErrorByEmail(ByVal curError As Erreur)
        Dim myMailAccount As New MailAccount()
        myMailAccount.email = "djon@cints.net"
        myMailAccount.sendingName = "Errors importer"
        myMailAccount.smtpServer = New MailAccount.MailAccountServer("relais.videotron.ca", 25)

        Dim subject As String = "[Clinica] Error : " & curError.message
        Dim body As String = curError.fullContent

        emailSending(myMailAccount, "clinica-error@cints.net", "clinica-error@cints.net", "", "", subject, False, body, curError.date, , "", "", False)

        For Each inError As Erreur In curError.identicalErrors
            sendOneErrorByEmail(inError)
        Next
    End Sub
End Class
