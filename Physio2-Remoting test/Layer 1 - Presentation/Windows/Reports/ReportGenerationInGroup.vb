Friend Class ReportGenerationInGroup

    Private formModified As Boolean = False

    Public Sub New()
        'MessageBox.Show("TEST")
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MdiParent = myMainWin
        'Me.typesRapports

        'Icônes
        Me.save.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.add.Image = DrawingManager.getInstance.getImage("ajouter16.gif")
        Me.selectDBPath.Image = DrawingManager.getInstance.getImage("selection16.gif")


        'REM If CurrentDroitAcces(66) = False Then doReportGenerationInGroup.Enabled = False
    End Sub

#Region "ReportGenerationInGroup Events"
    Private Sub reportGenerationInGroup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If formModified = True Then
            If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                saving(sender, EventArgs.Empty)
            End If
        End If
        If ReportGenerationInGroup.abortingThread = False AndAlso myThread IsNot Nothing AndAlso myThread.IsAlive Then
            ReportGenerationInGroup.abortThread = True
            MessageBox.Show("Le générateur termine se rapport et la fin de mois se fermera automatiquement.", "En cours de génération")
            e.Cancel = True
        End If
    End Sub

    Private Sub reportGenerationInGroup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        loadTypesRapports()
        save.Enabled = False
        doReportGenerationInGroup.Enabled = False
    End Sub
#End Region

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub

    Private Sub loadTypesRapports()
        Dim showCat As Boolean = PreferencesManager.getUserPreferences()("AffRapportCatInFinDeMois")
        Dim reportsList As New Generic.List(Of String)
        For Each CurRapportType As ReportType In ReportsManager.getInstance.getReportTypes
            Dim myRT As ReportType = CurRapportType.clone
            myRT.showCatInName = showCat
            If showCat AndAlso reportsList.Contains(myRT.reportCategorie) = False Then reportsList.Add(myRT.reportCategorie)
            If CurRapportType.isLeastOneRequiredFilter = False AndAlso CurRapportType.isInternal = False Then reportsList.Add(myRT.toString) '.Replace(" \\ ", "§"))
        Next
        typesRapports.tree = reportsList.ToArray()
        typesRapports.refreshTree()

        'Dim TPChecked() As String = DBLinker.GetInstance.ReadOneDBField("ReportGenerationInGroupTypesRapports", "NoRapportType", "WHERE NoClinique=" & CurrentClinique)
        'If Not TPChecked Is Nothing AndAlso TPChecked.Length <> 0 Then
        '    Dim i As Integer
        '    For i = 0 To typesRapports.Items.Count - 1
        '        If Array.IndexOf(TPChecked, CType(typesRapports.Items(i), RapportType).NoRapportType.ToString) <> -1 Then typesRapports.SetItemChecked(i, True)
        '    Next i
        'End If

        'Dim DBPath() As String = DBLinker.GetInstance.ReadOneDBField("InfoClinique", "ReportGenerationInGroupDBPath")
        'If Not DBPath Is Nothing AndAlso DBPath.Length <> 0 Then
        '    If DBPath(0) <> "" Then
        '        Me.DBPath.Text = DBPath(0)
        '        Me.DBPath.Tag = "1"
        '    End If
        'End If

        'FormModified = False
    End Sub

    Public Sub saving(ByVal sender As Object, ByVal e As EventArgs)
        'Droit & Accès
        If currentDroitAcces(72) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'enregistrer les options de fin de mois." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        DBLinker.getInstance.delDB("ReportGenerationInGroupTypesRapports", , , , , , , False)

        Dim curRapportType As ReportType
        'For Each CurRapportType In typesRapports.CheckedItems
        '    DBLinker.GetInstance.WriteDB("ReportGenerationInGroupTypesRapports", "NoClinique,NoRapportType", CurrentClinique & "," & CurRapportType.NoRapportType)
        'Next

        If Not dbPath.Tag Is Nothing Then DBLinker.getInstance.updateDB("InfoClinique", "ReportGenerationInGroupDBPath='" & dbPath.Text.Replace("'", "''") & "'")
        formModified = False
    End Sub

    Public Delegate Sub callingMethod()

    Private Sub threadReportGenerationInGroup()
        Try
            myMainWin.StatusText = "Début de la génération des rapports de fin de mois"

            Dim tpChecked(,) As String = DBLinker.getInstance.readDB("ReportGenerationInGroupTypesRapports INNER JOIN (RapportTypes LEFT JOIN RapportCategories ON RapportCategories.NoRapportCategorie=RapportTypes.NoRapportCategorie) ON RapportTypes.NoRapportType = ReportGenerationInGroupTypesRapports.NoRapportType", "RapportTitle,RapportCategorie", "WHERE NoClinique=" & currentClinic, , False)
            If Not tpChecked Is Nothing AndAlso tpChecked.Length <> 0 Then
                'Choix de l'année
                Dim myMultiChoice As New multichoice
                Dim myYears As String = ""
                For i As Integer = firstUsageDate.Year To Date.Today.Year
                    myYears &= "§" & i
                Next i
                If myYears <> "" Then myYears = myYears.Substring(1)
                Dim myYear As String = myMultiChoice.GetChoice("Veuillez sélectionner l'année", myYears, , "§", , Date.Today.Year.ToString)

                If myYear.StartsWith("ERROR") = False Then
                    'Choix du mois
                    Dim myMonth As Integer = DateFormat.chooseMonth()

                    If myMonth <> -1 Then
                        Dim oldAutoReplacing As Boolean = InternalDBManager.getInstance.autoReplacing
                        InternalDBManager.getInstance.autoReplacing = True
                        Dim dbPath As String = Me.dbPath.Text
                        dbPath &= "\" & myYear
                        InternalDBManager.getInstance.addDBFolder(dbPath)
                        dbPath &= "\" & addZeros(myMonth + 1, 2) & " (" & DateFormat.getMonthName(myMonth) & ")"
                        InternalDBManager.getInstance.addDBFolder(dbPath)
                        Dim dbFolderMonth As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(dbPath)
                        Dim tpFilter As New FilteringComposite
                        Dim fromToFilter As New FilteringFromTo()
                        fromToFilter.firstDate = New Date(Integer.Parse(myYear), myMonth + 1, 1)
                        fromToFilter.secondDate = New Date(Integer.Parse(myYear), myMonth + 1, 1).AddMonths(1).AddDays(-1)
                        tpFilter.add(fromToFilter)
                        Dim monthFilter As New FilteringMonth()
                        monthFilter.month = myMonth + 1
                        monthFilter.year = myYear
                        tpFilter.add(monthFilter)
                        Dim customVars As New Hashtable
                        customVars.Add("###CLINICAPATH###", appPath & bar(appPath))
                        customVars.Add("###TAUXAUTONOMIEMAX###", PreferencesManager.getGeneralPreferences()("NbEvalTo100TauxAutonomie"))
                        customVars.Add("###TAX1NAME###", PreferencesManager.getGeneralPreferences()("tax1Name"))
                        customVars.Add("###TAX2NAME###", PreferencesManager.getGeneralPreferences()("tax2Name"))
                        For i As Integer = 0 To tpChecked.GetUpperBound(1)
                            Dim curDBFolder As InternalDBFolder = dbFolderMonth
                            If tpChecked(1, i) <> "" Then
                                InternalDBManager.getInstance.addDBFolder(dbPath & "\" & tpChecked(1, i))
                                curDBFolder = InternalDBManager.getInstance.getDBFolder(dbPath & "\" & tpChecked(1, i))
                            End If

                            myMainWin.StatusText = "Fin de mois : Rapport en génération - " & tpChecked(0, i)
                            InternalDBManager.getInstance.addItem(tpChecked(0, i), curDBFolder, "Rapport", False, New String() {"Rapport", tpChecked(0, i)}, New String() {""}, False, False)
                            Dim curItem As New InternalDBItem(curDBFolder.toString, tpChecked(0, i))
                            Dim newRapport As Report = ReportsManager.getInstance.createReport(tpChecked(0, i), tpFilter, customVars)
                            newRapport.saveToFile(appPath & bar(appPath) & "DB\" & curItem.dbItemFile)
                            myMainWin.StatusText = "Fin de mois : Rapport généré - " & tpChecked(0, i) & " : " & (i + 1) & " / " & (tpChecked.GetUpperBound(1) + 1) & " rapports générés"
                            Application.DoEvents()

                            If ReportGenerationInGroup.abortThread Then
                                ReportGenerationInGroup.abortingThread = True
                                InternalDBManager.getInstance.autoReplacing = oldAutoReplacing
                                myMainWin.StatusText = "Fin de mois : Génération annulée par l'utilisateur"
                                Me.Invoke(System.Delegate.CreateDelegate(GetType(callingMethod), Me, "Close"))
                                Exit Sub
                            End If
                        Next i

                        InternalDBManager.getInstance.autoReplacing = oldAutoReplacing
                    End If
                End If
            End If

            DBLinker.getInstance.updateDB("InfoVisites", "Flagged=0", "Flagged", "1", False)
            myMainWin.StatusText = "Fin de la génération des rapports de fin de mois"
        Catch ex As Threading.ThreadAbortException
            'Thread abort because window has been closed
        End Try

        ReportGenerationInGroup.abortThread = False
        ReportGenerationInGroup.abortingThread = False

        Dim d As New setLockItemsCallback(AddressOf lockItems)
        Me.BeginInvoke(d, New Object() {False})
    End Sub

    Delegate Sub setLockItemsCallback(ByVal trueFalse As Boolean)

    Private Sub lockItems(ByVal trueFalse As Boolean)
        doReportGenerationInGroup.Enabled = Not trueFalse
        selectDBPath.Enabled = Not trueFalse
        save.Enabled = Not trueFalse
        add.Enabled = Not trueFalse
        typesRapports.Enabled = Not trueFalse
        groupName.ReadOnly = trueFalse
    End Sub

    Private myThread As Threading.Thread
    Private Shared abortThread As Boolean = False
    Private Shared abortingThread As Boolean = False

    Private Sub doReportGenerationInGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles doReportGenerationInGroup.Click
        cdf()
        If dbPath.Tag Is Nothing Then
            selectDBPath_Click(sender, e)
            If dbPath.Tag Is Nothing Then Exit Sub
        End If

        If InternalDBManager.getInstance.folderExists(dbPath.Text) = False Then
            MessageBox.Show("Le dossier où enregistrée les rapports de fin de mois n'existe plus. Veuillez en sélectionner un autre.", "Dossier inexistant")
            selectDBPath_Click(sender, e)
            If InternalDBManager.getInstance.folderExists(dbPath.Text) = False Then Exit Sub
        End If

        ReportGenerationInGroup.abortThread = False
        ReportGenerationInGroup.abortingThread = False
        myThread = New Threading.Thread(AddressOf threadReportGenerationInGroup)
        myThread.IsBackground = True
        lockItems(True)
        myThread.Start()
    End Sub

    Private Sub selectDBPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectDBPath.Click
        Dim mySelectDBCat As New SelectDBCat
        Dim myPath As String = mySelectDBCat("", False)
        If myPath = "" Then Exit Sub

        formModified = True
        dbPath.Text = myPath
        dbPath.Tag = "1"
    End Sub

    Private Sub save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles save.Click
        saving(sender, e)
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf saving)
        End Get
    End Property

    Private Sub add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles add.Click

    End Sub

    Private Sub groupName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles groupName.TextChanged
        formModified = True
    End Sub

    Private Sub typesRapports_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles typesRapports.AfterCheck
        formModified = True
    End Sub

#Region "CDF & CVF methods"

    Private Sub cdf()
        Dim strFolderFlagged As String = "(SELECT NoFolder FROM InfoFolders WHERE Flagged=1"
        'Dim strFolderFlaggedActive As String = strFolderFlagged & ")"
        strFolderFlagged &= ")"

        'Print Folders that will be deleted
        Dim noFolders(,) As String = DBLinker.getInstance.readDB("InfoFolders", "NoClient,NoFolder", "WHERE Flagged=1")
        If noFolders IsNot Nothing AndAlso noFolders.Length <> 0 Then
            For i As Integer = 0 To noFolders.GetUpperBound(1)
                Dim tpFilter As New FilteringComposite
                Dim noClientFilter As New FilteringNoClient
                noClientFilter.noClient = noFolders(0, i)
                noClientFilter.noFolder = noFolders(1, i)
                noClientFilter.clientFullName = getClientName(noFolders(0, i))
                tpFilter.add(noClientFilter)

                'folderRap = RapportManager.GetInstance.CreateRapport("Dossier détaillé", TPFilter)
                'folderRap.GenerateHTML()
                'folderRap.Print(False, True)
            Next i
        End If

        'Del visites
        cvf("(SELECT NoVisite FROM InfoVisites WHERE NoFolder IN " & strFolderFlagged & ")")

        'Del folders
        DBLinker.getInstance.delDB("StatFactures", "NoFolder", strFolderFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("StatPaiements", "NoFolder", strFolderFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("Factures", "NoFolder", strFolderFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("ListeAttente", "NoFolder", strFolderFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("DemandesAuthorisations", "NoFolder", strFolderFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("FolderTextes", "NoFolder", strFolderFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("StatFolders", "NoFolder", strFolderFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("InfoFolders", "Flagged", "True", , , , , False)
    End Sub

    Private Sub cvf(Optional ByVal strVisitesFlagged As String = "(SELECT NoVisite FROM InfoVisites WHERE Flagged=1)")
        DBLinker.getInstance.delDB("StatFactures", "NoVisite", strVisitesFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("StatPaiements", "NoFacture", strVisitesFlagged.Replace("NoVisite", "NoFacture"), False, , , , False, "IN")
        DBLinker.getInstance.delDB("Factures", "NoVisite", strVisitesFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("ListeAttente", "NoVisite", strVisitesFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("StatVisites", "NoVisite", strVisitesFlagged, False, , , , False, "IN")
        DBLinker.getInstance.delDB("InfoVisites", "NoVisite", strVisitesFlagged, False, , , , False, "IN")
    End Sub
#End Region

End Class
