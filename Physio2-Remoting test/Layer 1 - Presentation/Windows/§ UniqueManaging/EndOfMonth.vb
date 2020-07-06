Friend Class EndOfMonth

    Private formModified As Boolean = False

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MdiParent = myMainWin

        'Icônes
        Me.save.Image = DrawingManager.getInstance.getImage("save.jpg")
        Me.selectDBPath.Image = DrawingManager.getInstance.getImage("selection16.gif")

        If currentDroitAcces(66) = False Then doFinMois.Enabled = False
    End Sub

#Region "FinMois Events"
    Private Sub finMois_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If formModified = True Then
            If MessageBox.Show("Désirez-vous enregistrer les modifications ?", "Enregistrement", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                saving(sender, EventArgs.Empty)
            End If
        End If
        If EndOfMonth.abortingThread = False AndAlso myThread IsNot Nothing AndAlso myThread.IsAlive Then
            EndOfMonth.abortThread = True
            MessageBox.Show("Le générateur termine se rapport et la fin de mois se fermera automatiquement.", "En cours de génération")
            e.Cancel = True
        End If
    End Sub

    Private Sub finMois_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        loadTypesRapports()
    End Sub
#End Region

    Private Sub loadTypesRapports()
        For Each CurRapportType As ReportType In ReportsManager.getInstance.getReportTypes
            Dim myRT As ReportType = CurRapportType.clone
            myRT.showCatInName = PreferencesManager.getUserPreferences()("AffRapportCatInFinDeMois")
            If CurRapportType.isLeastOneRequiredFilter = False AndAlso CurRapportType.isInternal = False Then typesReports.Items.Add(myRT)
        Next

        Dim tpChecked() As String = DBLinker.getInstance.readOneDBField("FinMoisTypesRapports", "NoRapportType", "WHERE NoClinique=" & currentClinic)
        If Not tpChecked Is Nothing AndAlso tpChecked.Length <> 0 Then
            Dim i As Integer
            For i = 0 To typesReports.Items.Count - 1
                If Array.IndexOf(tpChecked, CType(typesReports.Items(i), ReportType).noReportType.ToString) <> -1 Then typesReports.SetItemChecked(i, True)
            Next i
        End If

        Dim dbPath() As String = DBLinker.getInstance.readOneDBField("InfoClinique", "FinMoisDBPath")
        If Not dbPath Is Nothing AndAlso dbPath.Length <> 0 Then
            If dbPath(0) <> "" Then
                Me.DBPath.Text = dbPath(0)
                Me.DBPath.Tag = "1"
            End If
        End If

        formModified = False
    End Sub

    Public Sub saving(ByVal sender As Object, ByVal e As EventArgs)
        'Droit & Accès
        If currentDroitAcces(72) = False Then
            'Message & Exit
            MessageBox.Show("Vous n'avez pas le droit d'enregistrer les options de fin de mois." & vbCrLf & "Merci!", "Droit & Accès")
            Exit Sub
        End If

        DBLinker.getInstance.delDB("FinMoisTypesRapports", , , , , , , False)

        Dim curRapportType As ReportType
        For Each curRapportType In typesReports.CheckedItems
            DBLinker.getInstance.writeDB("FinMoisTypesRapports", "NoClinique,NoRapportType", currentClinic & "," & curRapportType.noReportType)
        Next

        If Not DBPath.Tag Is Nothing Then DBLinker.getInstance.updateDB("InfoClinique", "FinMoisDBPath='" & DBPath.Text.Replace("'", "''") & "'")
        formModified = False
    End Sub

    Public Delegate Sub callingMethod()

    Private Sub threadFinMois()
        Try
            myMainWin.StatusText = "Début de la génération des rapports de fin de mois"

            Dim tpChecked(,) As String = DBLinker.getInstance.readDB("FinMoisTypesRapports INNER JOIN (RapportTypes LEFT JOIN RapportCategories ON RapportCategories.NoRapportCategorie=RapportTypes.NoRapportCategorie) ON RapportTypes.NoRapportType = FinMoisTypesRapports.NoRapportType", "RapportTitle,RapportCategorie", "WHERE NoClinique=" & currentClinic, , False)
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
                        Dim dbPath As String = Me.DBPath.Text
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

                            If EndOfMonth.abortThread Then
                                EndOfMonth.abortingThread = True
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
        Catch ex As Exception
            addErrorLog(ex)
        End Try

        EndOfMonth.abortThread = False
        EndOfMonth.abortingThread = False

        Dim d As New setLockItemsCallback(AddressOf lockItems)
        Me.BeginInvoke(d, New Object() {False})
    End Sub

    Delegate Sub setLockItemsCallback(ByVal trueFalse As Boolean)

    Private Sub lockItems(ByVal trueFalse As Boolean)
        doFinMois.Enabled = Not trueFalse
        selectDBPath.Enabled = Not trueFalse
        save.Enabled = Not trueFalse
        typesReports.Enabled = Not trueFalse
    End Sub

    Private myThread As Threading.Thread
    Private Shared abortThread As Boolean = False
    Private Shared abortingThread As Boolean = False

    Private Sub doFinMois_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles doFinMois.Click
        If DBPath.Tag Is Nothing Then
            selectDBPath_Click(sender, e)
            If DBPath.Tag Is Nothing Then Exit Sub
        End If

        If InternalDBManager.getInstance.folderExists(DBPath.Text) = False Then
            MessageBox.Show("Le dossier où enregistrée les rapports de fin de mois n'existe plus. Veuillez en sélectionner un autre.", "Dossier inexistant")
            selectDBPath_Click(sender, e)
            If InternalDBManager.getInstance.folderExists(DBPath.Text) = False Then Exit Sub
        End If

        EndOfMonth.abortThread = False
        EndOfMonth.abortingThread = False
        myThread = New Threading.Thread(AddressOf threadFinMois)
        myThread.IsBackground = True
        lockItems(True)
        myThread.Start()
    End Sub

    Private Sub selectDBPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectDBPath.Click
        Dim mySelectDBCat As New SelectDBCat
        Dim myPath As String = mySelectDBCat("", False)
        If myPath = "" Then Exit Sub

        formModified = True
        DBPath.Text = myPath
        DBPath.Tag = "1"
    End Sub

    Private Sub save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles save.Click
        saving(sender, e)
    End Sub

    Private Sub typesRapports_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles typesReports.ItemCheck
        formModified = True
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return New EventHandler(AddressOf saving)
        End Get
    End Property
End Class
