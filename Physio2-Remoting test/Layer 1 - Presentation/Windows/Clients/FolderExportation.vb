Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Friend Class FolderExportation

    Private Const ALL_FOLDERS_CHOICE As String = "* Tous les dossiers *"
    Private Const BODY_START_TAG As String = "<body>"
    Private Const BODY_END_TAG As String = "</body>"
    Private Const LINK_UNVAILABLE_FUNCTION As String = "javascript:alert('Fonction disponible uniquement dans Clinica');"


    Private noClient As Integer
    Private noFolder As Integer
    Private noClientText As String = String.Empty
    Private cancelText As String = String.Empty
    Private isExporting As Boolean = False


    Private changingChecks As Boolean = False

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        noClientText = selectedClientFolder.Text
        cancelText = btnCancel.Text
        Me.MdiParent = myMainWin
        Me.selectClientFolder.Image = DrawingManager.getInstance.getImage("selection16.gif")
        Me.removeClientFolder.Image = DrawingManager.iconToImage(DrawingManager.getInstance.getIcon("delete16.ico"), New Size(16, 16))
        Me.selectExportPath.Image = DrawingManager.getInstance.getImage("selection16.gif")

    End Sub

    Public Sub setFolder(ByVal fullClientName As String, ByVal noClient As Integer, ByVal noFolder As Integer)
        If noClient <> 0 Then
            selectedClientFolder.Text = fullClientName & " (" & noClient & ")"
            If noFolder <> 0 Then selectedClientFolder.Text &= " - Dossier #" & noFolder
        Else
            selectedClientFolder.Text = noClientText
        End If

        Me.noClient = noClient
        Me.noFolder = noFolder
    End Sub

    Private Sub loadForm()
        'Load lists
        Dim codes As Generic.List(Of String) = FolderCodesManager.getInstance().getCodeNames()
        codes.Sort()
        codifications.Items.AddRange(codes.ToArray())
        therapists.Items.AddRange(UsersManager.getInstance().getUsers(, True, False).ToArray())

        'Load settings
        Dim strSettings As String = UsersManager.currentUser.settings.folderExportation
        If strSettings <> String.Empty Then
            Dim settings() As String = strSettings.Split(New String() {"|"}, StringSplitOptions.None)
            Me.Height = settings(0)
            Me.Width = settings(1)
            Me.exportPath.Text = settings(2)
        End If
    End Sub

    Private Sub closeForm()
        'Save settings
        Dim curUser As User = UsersManager.currentUser
        curUser.settings.folderExportation = Me.Height & "|" & Me.Width & "|" & exportPath.Text
        curUser.settings.saveData()
    End Sub

    Private Sub export()
        Dim codesIds As New Generic.List(Of String)
        Dim trpsIds As New Generic.List(Of String)

        'Fill selected ids
        For i As Integer = 0 To therapists.Items.Count - 1
            If therapists.GetItemChecked(i) Then trpsIds.Add(CType(therapists.Items(i), User).noUser)
        Next i
        For i As Integer = 0 To codifications.Items.Count - 1
            If codifications.GetItemChecked(i) Then codesIds.Add(FolderCodesManager.getInstance().getNoUniqueByCodeName(codifications.Items(i)))
        Next i

        'Check if all mandatory fields
        If noClient = 0 Then
            If trpsIds.Count = 0 Then MessageBox.Show("Veuillez sélectionner au moins un thérapeute lorsque tous les clients sont sélectionnés", "Thérapeute manquant") : Exit Sub
            If codesIds.Count = 0 Then MessageBox.Show("Veuillez sélectionner au moins une codification dossier lorsque tous les clients sont sélectionnés", "Codification manquante") : Exit Sub
        End If
        If exportPath.Text = String.Empty Then MessageBox.Show("Veuillez sélectionner un chemin d'exportation", "Chemin manquant") : Exit Sub
        If Not IO.Directory.Exists(exportPath.Text) Then
            MessageBox.Show("Le chemin """ & exportPath.Text & """ n'existe pas", "Chemin inexistant")
            exportPath.Select()
            Exit Sub
        End If

        Dim startTime As Date = Date.Now
        lockForm(True)
        Me.output.Text = "Lecture des données"
        Application.DoEvents()

        'Select folders
        Dim sql As String = "SELECT IF1.NoClient,IF1.NoFolder,IC.Nom, IC.Prenom, SiteLesion.SiteLesion, IF1.NoCodeUnique, IF1.Service FROM (SiteLesion RIGHT JOIN Infofolders IF1 ON SiteLesion.NoSiteLesion = IF1.NoSiteLesion) INNER JOIN InfoClients IC ON IC.NoClient = IF1.NoClient"
        If Not (allCodes.Checked AndAlso allTRP.Checked AndAlso noClient = 0) Then
            Dim wheres As New Generic.List(Of String)
            If trpsIds.Count <> 0 Then wheres.Add("IF1.NoTRPTraitant IN (" & String.Join(",", trpsIds.ToArray()) & ")")
            If codesIds.Count <> 0 Then wheres.Add("IF1.NoCodeUnique IN (" & String.Join(",", codesIds.ToArray()) & ")")

            Dim clientFolderWhere As String = String.Empty
            If noFolder <> 0 Then
                wheres.Add("IF1.NoFolder=" & noFolder)
            ElseIf noClient <> 0 Then
                wheres.Add("IF1.NoClient=" & noClient)
            End If

            sql &= " WHERE " & String.Join(" AND ", wheres.ToArray())
        End If
        sql &= " ORDER BY Nom,Prenom, NoFolder"
        Dim folders As DataSet = DBLinker.getInstance().readDBForGrid(sql)
        If folders Is Nothing OrElse folders.Tables.Count = 0 OrElse folders.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Aucun dossier trouvé via les options sélectionnées", "Aucun dossier", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            lockForm(False)
            changeOutput(String.Empty)
            Exit Sub
        End If

        'Do last validation if export folder contains stuff
        Dim nbItemsInFolder As Integer = IO.Directory.GetDirectories(exportPath.Text).Length + IO.Directory.GetFiles(exportPath.Text).Length
        If nbItemsInFolder > 0 Then
            Dim myMsgBox As New MsgBox1()
            Dim myAnswer As Byte = myMsgBox("Le dossier sélectionné n'est pas vide. Que désirez-vous faire ?", "Dossier non vide", 3, "Annuler", "Continuer et laisser le contenu intact", "Continuer en supprimant le contenu")
            If myAnswer < 2 Then
                lockForm(False)
                changeOutput(String.Empty)
                Exit Sub
            End If
            If myAnswer = 3 Then 'Delete content
                Fichiers.deltree(exportPath.Text)
                Threading.Thread.Sleep(500)
                IO.Directory.CreateDirectory(exportPath.Text)
            End If
        End If

        '''''''''''''''''''''''''''''''
        '''''Do exportation
        '''''''''''''''''''''''''''''''
        Dim exportThread As New Threading.Thread(DirectCast(Function() doExportation(folders, startTime), Threading.ThreadStart))
        exportThread.Start()
    End Sub

    Private Function changeOutput(ByVal output As String) As Object
        Me.output.Text = output
        Return Nothing
    End Function

    Private Function doExportation(ByVal folders As DataSet, ByVal startTime As Date) As Object
        Try
            Dim dbFiles As New Generic.List(Of String)
            Dim commFiles As New Generic.List(Of String)
            Dim exportFolder As String = exportPath.Text & bar(exportPath.Text)
            Dim isCreatingIndex As Boolean = folders.Tables(0).Rows.Count > 1
            Dim indexBuilder As New System.Text.StringBuilder()
            Dim folderPageTemplate As String = IO.File.ReadAllText(appPath & bar(appPath) & "Data\exportFolderPage.html")


            Dim tpFilter As New FilteringComposite
            Dim noClientFilter As New FilteringNoClient
            tpFilter.add(noClientFilter)

            Dim n As Integer = 0, total As Integer = folders.Tables(0).Rows.Count
            For Each folderData As DataRow In folders.Tables(0).Rows
                If isExporting = False Then Return Nothing

                commFiles.Clear()
                Me.BeginInvoke(Function() changeOutput("Dossier " & n & " / " & total))

                Dim fullClientName As String = folderData("Nom") & "," & folderData("Prenom") & " (" & folderData("NoClient") & ")"
                Dim folderPath As String = fullClientName & " - Dossier " & folderData("NoFolder")
                If isCreatingIndex Then
                    Dim siteLesion As String = ""
                    If folderData("SiteLesion") IsNot DBNull.Value Then siteLesion = " - " & folderData("SiteLesion")
                    indexBuilder.Append("<A href=""" & folderPath.Replace("#", "%23") & "\dossier.html"" target=""folder"" onclick=""javascript:document.getElementById('frameTitle').innerHTML = this.innerHTML;"">" & folderPath & siteLesion & " (" & Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNameByNoUnique(folderData("NoCodeUnique")) & ") de " & folderData("Service").ToString.ToLower() & "</A>")
                End If

                'Create folder directory
                IO.Directory.CreateDirectory(exportFolder & folderPath)

                'Adjust filter for reports
                Dim folderPage As String = folderPageTemplate
                noClientFilter.noClient = folderData("NoClient")
                noClientFilter.noFolder = folderData("NoFolder")
                noClientFilter.clientFullName = fullClientName

                'Create reports
                folderPage = generateFolderPart(folderPage, "Dossier détaillé", "FOLDER", tpFilter, dbFiles, commFiles)
                folderPage = generateFolderPart(folderPage, "Communications d'un compte client", "COMM", tpFilter, dbFiles, commFiles)
                folderPage = generateFolderPart(folderPage, "Liste des équipements prêtés et vendus", "EQUIP", tpFilter, dbFiles, commFiles)

                'Create file
                IO.File.AppendAllText(exportFolder & folderPath & "\dossier.html", folderPage)

                'Copy comm files
                copyFiles(commFiles, appPath & bar(appPath) & "Clients\" & noClientFilter.noClient & "\Comm\", exportFolder & folderPath & "\.comm\")

                n += 1
            Next

            'Copy db files
            copyFiles(dbFiles, appPath & bar(appPath) & "DB\", exportFolder & ".db\")

            Me.BeginInvoke(Function() changeOutput(n & " dossiers exportés en " & (Base.DateFormat.transformIntervalToString(Date.Now.Subtract(startTime)))))

            If isCreatingIndex Then
                Dim indexPage As String = IO.File.ReadAllText(appPath & bar(appPath) & "Data\exportFolderIndex.html")
                indexPage = indexPage.Replace("###INDEX###", indexBuilder.ToString())
                IO.File.WriteAllText(exportFolder & "index.html", indexPage)
            End If
        Catch ex As Exception
            addErrorLog(ex)
        Finally
            Me.BeginInvoke(Function() lockForm(False))
        End Try

        Return Nothing
    End Function

    Private Sub copyFiles(ByVal files As Generic.List(Of String), ByVal sourcePath As String, ByVal filesOutputPath As String)
        Dim commFiles As New Generic.List(Of String)
        Dim dbFiles As New Generic.List(Of String)

        IO.Directory.CreateDirectory(filesOutputPath)
        sourcePath = sourcePath & bar(sourcePath)
        filesOutputPath = filesOutputPath & bar(filesOutputPath)
        For Each file As String In files
            Dim fileToCopy As String = sourcePath & file
            Dim fileDestination As String = filesOutputPath & file
            If IO.File.Exists(fileToCopy) AndAlso Not IO.File.Exists(fileDestination) Then
                file = file.ToLower()
                If file.EndsWith(".html") Or file.EndsWith(".htmlrpt") Then
                    Dim htmlBuilder As New System.Text.StringBuilder(IO.File.ReadAllText(fileToCopy))
                    Dim fileContent As String = replaceLinks(htmlBuilder, dbFiles, commFiles)
                    IO.File.WriteAllText(fileDestination, fileContent)
                Else
                    IO.File.Copy(fileToCopy, fileDestination)
                End If
            End If
        Next

        If dbFiles.Count > 0 Then copyFiles(dbFiles, appPath & bar(appPath) & "DB\", exportPath.Text & bar(exportPath.Text) & ".db")
        If commFiles.Count > 0 Then copyFiles(commFiles, sourcePath, filesOutputPath)
    End Sub

    Private Function replaceLinks(ByVal htmlBuilder As System.Text.StringBuilder, ByVal dbFiles As Generic.List(Of String), ByVal commFiles As Generic.List(Of String)) As String
        Dim matches As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(htmlBuilder.ToString(), "(<a\s+href\s*=\s*""?([^>""]+)""?\s*>)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        'Replace clinica links
        For Each match As System.Text.RegularExpressions.Match In matches

            Dim link As String = match.Groups(2).Value
            Dim decodedlink As String = Web.HttpUtility.UrlDecode(link)
            Dim newUrl As String = LINK_UNVAILABLE_FUNCTION
            Dim openLinkInNewWindow As Boolean = False
            If decodedlink.StartsWith(WebTextControl.PROTOCOL_CLINICA) Then
                Dim command As String = decodedlink.Substring(WebTextControl.PROTOCOL_CLINICA.Length, decodedlink.IndexOf("|") - WebTextControl.PROTOCOL_CLINICA.Length).ToUpper()

                Try
                    Select Case command
                        Case "CLIENT", "KP", "CONTACT", "USER"
                        Case "FILE"
                            Dim filePath As String = decodedlink.Substring(WebTextControl.PROTOCOL_CLINICA.Length + command.Length + 1)
                            filePath = If(filePath.EndsWith("/"), filePath.Substring(0, filePath.Length - 1), filePath)
                            filePath = filePath.Substring(filePath.LastIndexOf("\") + 1)
                            commFiles.Add(filePath)
                            newUrl = ".comm/" & filePath

                        Case "DB"
                            newUrl = getDBFilePath(decodedlink)
                            dbFiles.Add(newUrl)
                            newUrl = "../.db/" & newUrl

                        Case "COMM"
                            Dim subCommand As String = decodedlink.Substring(WebTextControl.PROTOCOL_CLINICA.Length + command.Length + 1)
                            subCommand = subCommand.Substring(subCommand.IndexOf("|") + 1)
                            Dim subCommandEndPos As Integer = subCommand.IndexOf("|")
                            Dim args As String = subCommand.Substring(subCommandEndPos + 1)
                            subCommand = subCommand.Substring(0, subCommandEndPos)
                            If subCommand = "DB" Then
                                decodedlink = WebTextControl.PROTOCOL_CLINICA & subCommand & "|" & args

                                newUrl = getDBFilePath(decodedlink)
                                dbFiles.Add(newUrl)
                                newUrl = "../.db/" & newUrl
                            Else
                                newUrl = ".comm/" & args
                                commFiles.Add(args)
                            End If
                    End Select
                Catch ex As Exception
                    'Invalid link (maybe not a link, but a string building a link)
                End Try
            Else
                openLinkInNewWindow = True
                newUrl = decodedlink
            End If
            Dim newValue As String = "<a href=""" & newUrl & """" & If(openLinkInNewWindow, " target=""_blank""", "") & ">"
            htmlBuilder = htmlBuilder.Replace(match.Groups(0).Value, newValue)
        Next

        Return htmlBuilder.ToString()
    End Function

    Private Function generateFolderPart(ByVal folderPage As String, ByVal reportTitle As String, ByVal htmlVarName As String, ByVal tpFilter As FilteringComposite, ByVal dbFiles As Generic.List(Of String), ByVal commFiles As Generic.List(Of String)) As String
        Dim myReport As Report = ReportsManager.getInstance().createReport(reportTitle, tpFilter)
        Dim html As String = myReport.generateHTML()
        Dim htmlBuilder As New System.Text.StringBuilder(html)

        'Take only page's body part
        Dim endBodyTagPos As Integer = html.LastIndexOf(BODY_END_TAG)
        htmlBuilder = htmlBuilder.Remove(endBodyTagPos, html.Length - endBodyTagPos)
        htmlBuilder = htmlBuilder.Remove(0, html.IndexOf(BODY_START_TAG) + BODY_START_TAG.Length)

        'Replace links
        html = replaceLinks(htmlBuilder, dbFiles, commFiles)

        Return folderPage.Replace("###" & htmlVarName & "###", html)
    End Function

    Private Function getDBFilePath(ByVal url As String) As String
        Dim curDBItem As InternalDBItem
        Try
            curDBItem = InternalDBItem.getItemFromLink(url)
        Catch ex As InexistantInternalDBItemException
            Return ""
        End Try

        Return curDBItem.dbItemFile.Trim
    End Function

    Private Function lockForm(ByVal isLocked As Boolean) As Object
        allCodes.Enabled = Not isLocked
        allTRP.Enabled = Not isLocked
        therapists.Enabled = Not isLocked
        codifications.Enabled = Not isLocked
        selectClientFolder.Enabled = Not isLocked
        removeClientFolder.Enabled = Not isLocked
        selectExportPath.Enabled = Not isLocked
        exportPath.Enabled = Not isLocked
        btnExport.Visible = Not isLocked
        btnCancel.Visible = isLocked
        btnCancel.Enabled = True
        isExporting = isLocked

        If Not isLocked Then
            btnCancel.Text = cancelText
        End If
        Return Nothing
    End Function

    Private Sub selectAll(ByVal checksList As CheckedListBox, ByVal isChecked As Boolean)
        If changingChecks Then Exit Sub

        changingChecks = True
        For i As Integer = 0 To checksList.Items.Count - 1
            checksList.SetItemChecked(i, isChecked)
        Next i
        changingChecks = False
    End Sub

    Private Sub changeAll(ByVal checksList As CheckedListBox, ByVal checkBox As CheckBox, ByVal indexChecked As Integer, ByVal indexValue As Boolean)
        If changingChecks Then Exit Sub

        changingChecks = True

        Dim allChecked As Boolean = indexValue
        If indexValue Then
            For i As Integer = 0 To checksList.Items.Count - 1
                If indexChecked <> i AndAlso Not checksList.GetItemChecked(i) Then
                    allChecked = False
                    Exit For
                End If
            Next i
        End If

        checkBox.Checked = allChecked
        changingChecks = False
    End Sub

    Private Sub allCodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles allCodes.CheckedChanged
        selectAll(codifications, allCodes.Checked)
    End Sub

    Private Sub allTRP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles allTRP.CheckedChanged
        selectAll(therapists, allTRP.Checked)
    End Sub

    Private Sub therapists_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles therapists.ItemCheck
        changeAll(therapists, allTRP, e.Index, e.NewValue)
    End Sub

    Private Sub codifications_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles codifications.ItemCheck
        changeAll(codifications, allCodes, e.Index, e.NewValue)
    End Sub

    Private Sub selectClientFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectClientFolder.Click
        Dim lastFoundClient As Integer = -1
        If Not foundClient Is Nothing AndAlso foundClient.Length <> 0 Then lastFoundClient = foundClient.GetUpperBound(0)

        Dim myRecherche As New clientSearch
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.Visible = False
        myRecherche.ShowDialog()

        If Not foundClient Is Nothing AndAlso foundClient.Length <> 0 Then
            If foundClient.GetUpperBound(0) > lastFoundClient Then
                Dim selectedClient As ClientKeys = foundClient(foundClient.GetUpperBound(0))
                Dim folders As DataSet = DBLinker.getInstance.readDBForGrid("SiteLesion RIGHT JOIN Infofolders ON SiteLesion.NoSiteLesion = Infofolders.NoSiteLesion", "Infofolders.NoFolder, SiteLesion.SiteLesion, Infofolders.NoCodeUnique, InfoFolders.Service", "WHERE ((Infofolders.NoClient)=" & selectedClient.noClient & ");")
                If folders Is Nothing OrElse folders.Tables.Count = 0 OrElse folders.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Ce client ne possède aucun dossier", "Client invalide", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Else
                    Dim myMultiChoice As New multichoice
                    Dim choices As String = ""
                    For Each curRow As DataRow In folders.Tables(0).Rows
                        Dim siteLesion As String = ""
                        If curRow("SiteLesion") IsNot DBNull.Value Then siteLesion = " - " & curRow("SiteLesion")
                        choices &= "§#" & curRow("NoFolder") & siteLesion & " (" & Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNameByNoUnique(curRow("NoCodeUnique")) & ") de " & curRow("Service").ToString.ToLower()
                    Next
                    choices = ALL_FOLDERS_CHOICE & choices

                    Dim monFolder As String = myMultiChoice.GetChoice("Veuillez choisir le dossier", choices, , "§", False)
                    If monFolder <> "" And monFolder.StartsWith("ERROR") = False Then
                        Dim noFolder As Integer = 0
                        If monFolder <> ALL_FOLDERS_CHOICE Then
                            Dim sMonFolder() As String = monFolder.Split(" ")
                            noFolder = Chaines.onlyNumeric(sMonFolder(0))
                        End If
                        Me.setFolder(selectedClient.fullName, selectedClient.noClient, noFolder)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub removeClientFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removeClientFolder.Click
        noClient = 0
        noFolder = 0
        selectedClientFolder.Text = noClientText
    End Sub

    Private Sub selectExportPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectExportPath.Click
        If pathDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            exportPath.Text = pathDialog.SelectedPath
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        export()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        isExporting = False
        btnCancel.Text = "..."
        btnCancel.Enabled = False
    End Sub

    Private Sub FolderExportation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        closeForm()
    End Sub

    Private Sub FolderExportation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If isExporting Then
            If MessageBox.Show("Une exportation est en cours. Désirez-vous l'annuler ?", "Exporation en cours", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                isExporting = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub FolderExportation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadForm()
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As Base.DataInternalUpdate)

    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property
End Class