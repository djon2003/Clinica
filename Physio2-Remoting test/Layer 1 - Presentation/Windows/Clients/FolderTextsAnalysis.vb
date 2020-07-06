Friend Class FolderTextsAnalysis
    Inherits SingleWindow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        Me.MdiParent = myMainWin
        Me.Erronus.SelectedIndex = 0
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents dgAnalyse As DataGridPlus
    Friend WithEvents generateReport As System.Windows.Forms.Button
    Friend WithEvents analyse As System.Windows.Forms.Button
    Friend WithEvents correctErrors As System.Windows.Forms.Button
    Friend WithEvents Erronus As System.Windows.Forms.ComboBox
    Friend WithEvents filterPerso As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents analyseStatus As System.Windows.Forms.Label
    Friend WithEvents dgContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents nbFolderTexte As System.Windows.Forms.Label
    Friend WithEvents nbErrors As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.generateReport = New System.Windows.Forms.Button
        Me.analyse = New System.Windows.Forms.Button
        Me.correctErrors = New System.Windows.Forms.Button
        Me.Erronus = New System.Windows.Forms.ComboBox
        Me.filterPerso = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.analyseStatus = New System.Windows.Forms.Label
        Me.dgContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dgAnalyse = New DataGridPlus
        Me.Label2 = New System.Windows.Forms.Label
        Me.nbFolderTexte = New System.Windows.Forms.Label
        Me.nbErrors = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dgContextMenu.SuspendLayout()
        CType(Me.dgAnalyse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'generateReport
        '
        Me.generateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.generateReport.Enabled = False
        Me.generateReport.Location = New System.Drawing.Point(12, 330)
        Me.generateReport.Name = "generateReport"
        Me.generateReport.Size = New System.Drawing.Size(114, 23)
        Me.generateReport.TabIndex = 1
        Me.generateReport.Text = "Générer le rapport"
        Me.generateReport.UseVisualStyleBackColor = True
        '
        'analyse
        '
        Me.analyse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.analyse.Location = New System.Drawing.Point(12, 359)
        Me.analyse.Name = "analyse"
        Me.analyse.Size = New System.Drawing.Size(75, 23)
        Me.analyse.TabIndex = 1
        Me.analyse.Text = "Analyse"
        Me.analyse.UseVisualStyleBackColor = True
        '
        'correctErrors
        '
        Me.correctErrors.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.correctErrors.Enabled = False
        Me.correctErrors.Location = New System.Drawing.Point(385, 327)
        Me.correctErrors.Name = "correctErrors"
        Me.correctErrors.Size = New System.Drawing.Size(124, 23)
        Me.correctErrors.TabIndex = 1
        Me.correctErrors.Text = "Corriger les erreurs"
        Me.correctErrors.UseVisualStyleBackColor = True
        '
        'Erronus
        '
        Me.Erronus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Erronus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Erronus.FormattingEnabled = True
        Me.Erronus.Items.AddRange(New Object() {"* Toutes les erreurs *", "Textes et/ou alertes inexistants(es)", "Dates de début erronées", "Textes en trop", "Textes doublon", "Textes inexistants seulement", "Alertes inexistantes seulement"})
        Me.Erronus.Location = New System.Drawing.Point(132, 329)
        Me.Erronus.Name = "Erronus"
        Me.Erronus.Size = New System.Drawing.Size(247, 21)
        Me.Erronus.TabIndex = 2
        '
        'filterPerso
        '
        Me.filterPerso.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.filterPerso.Location = New System.Drawing.Point(595, 324)
        Me.filterPerso.Name = "filterPerso"
        Me.filterPerso.Size = New System.Drawing.Size(179, 20)
        Me.filterPerso.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(515, 327)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Filtre avancé :"
        '
        'analyseStatus
        '
        Me.analyseStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.analyseStatus.AutoSize = True
        Me.analyseStatus.Location = New System.Drawing.Point(93, 359)
        Me.analyseStatus.Name = "analyseStatus"
        Me.analyseStatus.Size = New System.Drawing.Size(127, 26)
        Me.analyseStatus.TabIndex = 4
        Me.analyseStatus.Text = "Statut :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Aucune analyse en cours"
        '
        'dgContextMenu
        '
        Me.dgContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem, Me.OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem})
        Me.dgContextMenu.Name = "dgContextMenu"
        Me.dgContextMenu.Size = New System.Drawing.Size(371, 70)
        '
        'AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem
        '
        Me.AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem.Name = "AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem"
        Me.AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem.Size = New System.Drawing.Size(370, 22)
        Me.AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem.Text = "Afficher tous les textes des dossiers présentement affichés"
        '
        'dgAnalyse
        '
        Me.dgAnalyse.AllowUserToAddRows = False
        Me.dgAnalyse.AllowUserToDeleteRows = False
        Me.dgAnalyse.AllowUserToResizeRows = False
        Me.dgAnalyse.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgAnalyse.autoSelectOnDataSourceChanged = False
        Me.dgAnalyse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgAnalyse.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.dgAnalyse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgAnalyse.ContextMenuStrip = Me.dgContextMenu
        Me.dgAnalyse.Location = New System.Drawing.Point(12, 12)
        Me.dgAnalyse.Name = "dgAnalyse"
        Me.dgAnalyse.ReadOnly = True
        Me.dgAnalyse.RowHeadersVisible = False
        Me.dgAnalyse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgAnalyse.Size = New System.Drawing.Size(765, 312)
        Me.dgAnalyse.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(435, 359)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Nombre de textes :"
        '
        'nbFolderTexte
        '
        Me.nbFolderTexte.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nbFolderTexte.AutoSize = True
        Me.nbFolderTexte.Location = New System.Drawing.Point(537, 359)
        Me.nbFolderTexte.Name = "nbFolderTexte"
        Me.nbFolderTexte.Size = New System.Drawing.Size(13, 13)
        Me.nbFolderTexte.TabIndex = 4
        Me.nbFolderTexte.Text = "0"
        '
        'nbErrors
        '
        Me.nbErrors.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nbErrors.AutoSize = True
        Me.nbErrors.Location = New System.Drawing.Point(691, 359)
        Me.nbErrors.Name = "nbErrors"
        Me.nbErrors.Size = New System.Drawing.Size(13, 13)
        Me.nbErrors.TabIndex = 4
        Me.nbErrors.Text = "0"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(592, 359)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Nombre d'erreurs :"
        '
        'OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem
        '
        Me.OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem.Enabled = False
        Me.OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem.Name = "OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem"
        Me.OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem.Size = New System.Drawing.Size(370, 22)
        Me.OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem.Text = "Ouvrir le compte client du dernier texte sélectionné"
        '
        'fenAnalyseFT
        '
        Me.ClientSize = New System.Drawing.Size(789, 394)
        Me.Controls.Add(Me.analyseStatus)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.nbFolderTexte)
        Me.Controls.Add(Me.nbErrors)
        Me.Controls.Add(Me.filterPerso)
        Me.Controls.Add(Me.analyse)
        Me.Controls.Add(Me.Erronus)
        Me.Controls.Add(Me.generateReport)
        Me.Controls.Add(Me.correctErrors)
        Me.Controls.Add(Me.dgAnalyse)
        Me.MinimumSize = New System.Drawing.Size(797, 428)
        Me.Name = "fenAnalyseFT"
        Me.Text = "Analyse des textes des dossiers"
        Me.dgContextMenu.ResumeLayout(False)
        CType(Me.dgAnalyse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private sqlConnection As SqlClient.SqlConnection

    Private Enum analyseResults As Byte
        MissingTextNAlert = 1
        DateStartedErronus = 2
        UselessText = 3
        DoubleText = 4
        MissingText = 5
        MissingAlert = 6
    End Enum

    Private Function createAnalyseTable() As Boolean
        Dim startTime As Date = DateTime.Now
        Dim nbFuturForNbDaysMultiple As Integer = 5
        Dim fttCategorie As String = 5

        'Choix de la catégorie
        Dim categories As Generic.List(Of ModelCategory) = ModelsManager.getInstance.getModelsCategories(New Integer() {1, 3, 4, 5})
        Dim cats As String = ""
        For i As Integer = 0 To categories.Count - 1
            cats &= "§" & categories(i).category
        Next i
        cats = cats.Substring(1)
        Dim myMultichoice As New multichoice()
        Dim choosen As String = myMultichoice.GetChoice("Veuillez choisir la catégorie", cats, "INDEX", "§", False, "* Tous *")
        If choosen = -1 Then Return False

        If choosen = 0 Then
            fttCategorie = "null"
        Else
            fttCategorie = categories(choosen - 1).noCategory
        End If

        'TRP choice
        Dim trpChoosen As User = UsersManager.getInstance().chooseUser(True, , , , , , False)
        If trpChoosen Is Nothing Then Return False
        Dim trpFilter As String = ""
        If Not trpChoosen.Equals(UserAll.getInstance()) Then trpFilter = " AND FolderDates.NoTRPTraitant = " & trpChoosen.noUser

        'FolderCode choice
        Dim codeNames As Generic.List(Of String) = Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNames()
        codeNames.Sort()
        codeNames.Insert(0, "* Toutes *")
        myMultichoice = New multichoice()
        choosen = myMultichoice.GetChoice("Veuillez choisir la codification dossier", String.Join("§", codeNames.ToArray()), "INDEX", "§", False, "* Toutes *")
        If choosen = -1 Then Return False

        Dim codeFilter As String = String.Empty
        If choosen <> 0 Then
            codeFilter = " AND CodeName= '" & codeNames(choosen).Replace("'", "''") & "'"
        End If

        'Period choice
        Dim minSelectDate As Date = LIMIT_DATE
        Dim maxSelectDate As Date = LIMIT_DATE
        Dim myDateChoice As New DateChoice()
        myDateChoice.texteEnHaut.Text = "Date de début :"
        Dim chosenDate As Generic.List(Of Date) = myDateChoice.choose(firstUsageDate.Year, Date.Now.Year, , , , , , True, firstUsageDate)
        If chosenDate.Count <> 0 Then 'Choix de la date de fin
            minSelectDate = chosenDate(0)
            myDateChoice = New DateChoice()
            myDateChoice.texteEnHaut.Text = "Date de fin :"
            chosenDate = myDateChoice.choose(firstUsageDate.Year, Date.Now.Year, , , , , , True, firstUsageDate)
            If chosenDate.Count <> 0 Then maxSelectDate = chosenDate(0)
        End If
        Dim fNoclient As New FilterNoClient()
        fNoclient.askNoFolder = True
        fNoclient.filter()
        Dim chosenNoFolder As String = IIf(fNoclient.currentReturn.canceling = True, "null", fNoclient.currentReturn.noFolder)

        sqlConnection = DBLinker.getInstance.makeNewConnection()
        sqlConnection.Open()

        analyseStatus.Text = "Statut :" & vbCrLf & "1 - Chargement des données requises"
        Application.DoEvents()

        nbFuturForNbDaysMultiple += 1
        DBLinker.executeSQLScript("IF NOT object_id('tempdb..#FolderTextesAnalyse') IS NULL DROP TABLE #FolderTextesAnalyse" & vbCrLf & "CREATE TABLE #FolderTextesAnalyse (NoFolderTexte int null,NoFolderTexteType int, NoFolder int, TexteTitle varchar(MAX),DateStarted datetime,DateFinished datetime,NoMultiple int,HasText bit, Erronus int, Status varchar(MAX), ExternalStatus int, Texte varchar(MAX))", False, sqlConnection)

        'Gather folders to analyse
        Dim datesFilter As String = If(minSelectDate.Equals(LIMIT_DATE), "", " AND (CreationDate<='" & DateFormat.getTextDate(If(maxSelectDate.Equals(LIMIT_DATE), minSelectDate, maxSelectDate)) & "' AND ((LastTraitement IS NULL AND ((ClosingDate IS NULL AND CreationDate>='" & DateFormat.getTextDate(minSelectDate) & "') OR ClosingDate >= '" & DateFormat.getTextDate(minSelectDate) & "')) OR LastTraitement>='" & DateFormat.getTextDate(minSelectDate) & "'))")
        Dim foldersToAnalyse As DataSet = DBLinker.getInstance.readDBForGrid("WITH t (NoFolder,NoFTT) AS (SELECT NoFolder,FTT.NoFolderTexteType FROM FolderTexteTypes AS FTT INNER JOIN CodesDossiersFolderTexteTypes AS CDFTT ON FTT.NoFolderTexteType = CDFTT.NoFolderTexteType INNER JOIN CodesDossiersCodes CDC ON CDC.NoCodeUnique = CDFTT.NoUnique INNER JOIN FolderDates ON FolderDates.NoCodeUnique=CDFTT.NoUnique AND (CDFTT.NoUser = dbo.fnGetFolderTextCodeUser(FolderDates.NoCodeUnique,FolderDates.NoCodeUser) OR (dbo.fnGetFolderTextCodeUser(FolderDates.NoCodeUnique,FolderDates.NoCodeUser) IS NULL AND CDFTT.NoUser IS NULL)) WHERE (NoModeleCategorie=" & fttCategorie & " OR " & fttCategorie & " IS null) " & datesFilter & codeFilter & trpFilter & " AND (NoFolder=" & chosenNoFolder & " OR " & chosenNoFolder & " IS null)) " & _
                                                                             "SELECT t.NoFolder,t.noftt as NoFolderTexteType, CreationDate AS FolderCreation,NoClient,LastTraitement AS LastPresence,NbMultipleEnding,DATEADD(second,DATEPART(second,ClosingDate)*-1,DATEADD(mi,DATEPART(mi,ClosingDate)*-1,DATEADD(hh,DATEPART(hh,ClosingDate)*-1,ClosingDate))) AS FolderClosing,WhenToBeStopped,StatutOuvert AS FolderActive,TypeForMultiple,NbPresencesX,NbDaysX,NbDaysMultiple,NbPresencesMultiple,Multiple,TexteTitle AS FTTTexteTitle,WhenToBeCreated,ShowAlert, StartingExternalStatus, CASE WHEN ModelAppliedOnCreation IS NULL THEN '' ELSE (SELECT Modele FROM Modeles WHERE NoModele = ModelAppliedOnCreation) END AS TextToApply FROM FolderTexteTypes AS FTT, FolderDates, t WHERE folderdates.nofolder = t.NoFolder AND ftt.nofoldertextetype = t.NoFTT order by folderdates.nofolder", , , , , False, sqlConnection)
        If foldersToAnalyse Is Nothing OrElse foldersToAnalyse.Tables.Count = 0 OrElse foldersToAnalyse.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Aucun dossier ou aucun type de texte dans les codifications sous-jacentes aux dossiers à analyser pour les filtres choisis", "Aucune analyse possible")
            analyseStatus.Text = "Statut :" & vbCrLf & "Aucune analyse en cours"
            Return False
        End If

        Me.analyse.Enabled = False
        Me.generateReport.Enabled = False
        Me.correctErrors.Enabled = False
        Dim foldersList As String = ","
        With foldersToAnalyse.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                Dim folderNoStr As String = .Item(i)("NoFolder").ToString()
                If foldersList.IndexOf("," & folderNoStr & ",") = -1 Then foldersList &= folderNoStr & ","
            Next i
        End With
        foldersList = foldersList.Substring(1)
        foldersList = foldersList.Substring(0, foldersList.Length - 1)
        Dim folderTextes As DataSet = DBLinker.getInstance.readDBForGrid("folderTextes", "NoFolderTexte,NoFolderTexteType,NoFolder,TexteTitle,DateStarted,DateFinished,NoMultiple,IsTexte", "NoFolder IN (" & foldersList & ")", , , , , False, sqlConnection)
        Dim usersAlerts As DataSet = DBLinker.getInstance.readDBForGrid("usersAlerts", "*", , , , , , False, sqlConnection)
        'Dim InfoVisites As DataSet = DBLinker.GetInstance.ReadDBForGrid("InfoVisites", "*", IIf(MinDate.Equals(LIMIT_DATE), "", "DateHeure>='" & DateFormat.AffTextDate(MinDate) & "'"))

        Dim noFolderTexte As String = ""
        Dim lastWasErronus As Boolean = False
        Dim dataToBulk As New System.Text.StringBuilder()

        'Analyse by creating virtually the texts
        With foldersToAnalyse.Tables(0)
            Dim curRVDates As New Hashtable(.Rows.Count * 100)
            Dim curNbFolder As Integer = 0
            Dim lastFolder As Integer = 0
            Dim nbFolder As Integer
            For Each curRow As DataRow In .Rows
                If lastFolder <> curRow("NoFolder") Then
                    lastFolder = curRow("NoFolder")
                    nbFolder += 1
                End If
            Next

            analyseStatus.Text = "Statut : (0 / " & nbFolder & " dossiers)"
            Application.DoEvents()

            lastFolder = 0
            For Each curRow As DataRow In .Rows
                If lastFolder <> curRow("NoFolder") Then
                    lastFolder = curRow("NoFolder")
                    curNbFolder += 1
                End If

                analyseStatus.Text = "Statut : (" & curNbFolder & " / " & nbFolder & " dossiers)" & vbCrLf & "Dossier #" & curRow("NoFolder")
                Application.DoEvents()

                Dim curNoMultiple As Integer = 1
                Dim lastDateFinished As Date = LIMIT_DATE
                Dim posVisite As Integer = curRow("NbPresencesX")
                Dim looping As Boolean = True
                Dim dateOverToday As Integer = 0
                Dim nbFuturs As Integer = 0
                lastWasErronus = False
                Dim texteTitle As String = String.Empty
                Dim rvDate As Date = LIMIT_DATE

                While looping

                    rvDate = LIMIT_DATE
                    texteTitle = curRow("FTTtexteTitle")

                    If (curRow("WhenToBeCreated") < 2 And curRow("TypeForMultiple") <> 1) Or (curRow("WhenToBeCreated") < 2 And curRow("TypeForMultiple") = 1 And curNoMultiple <> 1) Then
                        rvDate = curRow("FolderCreation")
                    Else
                        If curRow("WhenToBeCreated") = 3 Then
                            If curRow("FolderActive") = True Then
                                rvDate = LIMIT_DATE
                            Else
                                rvDate = curRow("FolderClosing")
                            End If
                        Else
                            If curRVDates.ContainsKey(curRow("NoFolder") & "-" & posVisite) Then
                                rvDate = curRVDates(curRow("NoFolder") & "-" & posVisite)
                            Else
                                Dim rvDateByPosition() As String = DBLinker.getInstance.readOneDBField("WITH T AS (SELECT ROW_NUMBER() OVER(ORDER BY DateHeure) AS PosVisite,InfoVisites.* FROM InfoVisites WHERE NoFolder=" & curRow("NoFolder") & " AND NoStatut=4) SELECT DATEADD(mi,DATEPART(mi,DateHeure)*-1,DATEADD(hh,DATEPART(hh,DateHeure)*-1,DateHeure)) as RVDate FROM T WHERE PosVisite=" & posVisite, , , , False, sqlConnection)
                                If rvDateByPosition Is Nothing OrElse rvDateByPosition.Length = 0 Then
                                    rvDate = LIMIT_DATE
                                Else
                                    rvDate = rvDateByPosition(0)
                                End If
                                curRVDates(curRow("NoFolder") & "-" & posVisite) = rvDate
                            End If
                        End If
                    End If
                    If rvDate.Equals(LIMIT_DATE) = False AndAlso curRow("WhenToBeCreated") <= 2 And (curNoMultiple = 1 Or curRow("TypeForMultiple") <> 2) Then
                        rvDate = rvDate.AddDays(curRow("NbDaysX"))
                    End If

                    '--Adjust date for multiple where TypeForMultiple = NbDaysX
                    If curRow("Multiple") = True And curRow("TypeForMultiple") = 0 And curNoMultiple > 1 Then
                        rvDate = rvDate.AddDays((curNoMultiple - 1) * curRow("NbDaysMultiple"))
                        '--Block to 1 in the future max
                    End If

                    If date1Infdate2(Date.Today, rvDate, True) AndAlso curRow("Multiple") = True And curRow("TypeForMultiple") = 0 And curNoMultiple > 1 Then
                        If dateOverToday < nbFuturForNbDaysMultiple Then
                            dateOverToday += 1
                        Else
                            dateOverToday = 0
                            rvDate = LIMIT_DATE
                        End If
                    End If

                    '		-- Multiple is stopped on folderclosed
                    If curRow("Multiple") = True And curRow("WhenToBeStopped") = 0 And curRow("FolderActive") = False And (curRow("FolderClosing") IsNot DBNull.Value AndAlso date1Infdate2(curRow("FolderClosing"), rvDate)) Then
                        rvDate = LIMIT_DATE
                    End If


                    '-- Multiple is stopped on max
                    If curRow("Multiple") = True And curNoMultiple > 1 And curRow("WhenToBeStopped") = 1 And curRow("NbMultipleEnding") < curNoMultiple Then
                        rvDate = LIMIT_DATE
                    End If

                    If rvDate.Equals(LIMIT_DATE) Then looping = False
                    If looping = False Then Exit While

                    If curRow("Multiple") = True Then texteTitle = texteTitle & " " & curNoMultiple

                    analyseStatus.Text = "Statut : (" & curNbFolder & " / " & nbFolder & " dossiers)" & vbCrLf & "Dossier #" & curRow("NoFolder") & "  Texte : " & texteTitle
                    Application.DoEvents()

                    Dim erronus As Integer = 0
                    Dim status As String = ""
                    Dim lastNoFolderTexte As String = noFolderTexte
                    noFolderTexte = ""
                    Dim noUserAlert As String = ""
                    Dim dateFinished As Date = LIMIT_DATE
                    Dim dateStarted As Date = LIMIT_DATE
                    Dim hasText As Boolean = False
                    Dim ftData() As DataRow = folderTextes.Tables(0).Select("NoFolder=" & curRow("NoFolder") & " AND TexteTitle='" & texteTitle.Replace("'", "''") & "'")

                    Dim alertData() As DataRow = usersAlerts.Tables(0).Select("([Message] LIKE '%" & texteTitle.Replace("'", "''") & "%' OR [Message] LIKE '%" & curRow("FTTTexteTitle").replace("'", "''") & " #" & curNoMultiple & "%') AND AlertData='" & curRow("NoClient") & "' AND AlarmData LIKE '%:" & curRow("NoClient") & ":" & curRow("NoFolder") & ":True'")
                    If ftData IsNot Nothing AndAlso ftData.Length <> 0 Then
                        'Vérifie s'il y a des doublons de ce texte
                        For d As Integer = 1 To ftData.GetUpperBound(0)
                            If ftData(d)("IsTexte") AndAlso ftData(0)("IsTexte") = False Then
                                Dim tmpRow As DataRow = ftData(0)
                                ftData(0) = ftData(d)
                                ftData(d) = tmpRow
                            End If

                            DBLinker.executeSQLScript("INSERT INTO #FolderTextesAnalyse (NoFolderTexte,NoFolderTexteType,NoMultiple, NoFolder, TexteTitle,DateStarted,DateFinished, HasText, Erronus , Status, ExternalStatus, Texte ) " & _
                            "VALUES (" & ftData(d)("NoFolderTexte") & "," & curRow("NoFolderTexteType") & "," & curNoMultiple & "," & curRow("NoFolder") & ",'" & texteTitle.Replace("'", "''") & "','" & ftData(d)("DateStarted") & "'," & IIf(ftData(d)("DateFinished").ToString = "", "null", "'" & ftData(d)("DateFinished") & "'") & ",'" & CType(ftData(d)("IsTexte"), Boolean) & "'," & analyseResults.DoubleText & ",'Le texte est un doublon', 1, '')", False, sqlConnection)
                        Next d

                        noFolderTexte = ftData(0)("NoFolderTexte")
                        If ftData(0)("DateFinished").ToString <> "" Then dateFinished = ftData(0)("DateFinished")
                        If ftData(0)("DateStarted").ToString <> "" Then dateStarted = ftData(0)("DateStarted")
                        hasText = ftData(0)("IsTexte")
                        lastDateFinished = dateFinished
                    End If
                    If alertData IsNot Nothing AndAlso alertData.Length <> 0 Then
                        noUserAlert = alertData(0)("NoUserAlert")
                    End If

                    '-- Texte doesn't exist
                    If (noFolderTexte = "" OrElse (dateOverToday <> 0)) Then '--AND (@Multiple=0 OR @TypeForMultiple<>0 OR (@Multiple=1 AND @TypeForMultiple=0 AND @NoUserAlert IS NULL))
                        Dim checkForMultipleRepeatingOnTextEnded As Boolean = (curRow("TypeForMultiple") <> 2 OrElse (curRow("TypeForMultiple") = 2 AndAlso lastDateFinished <> LIMIT_DATE))
                        If checkForMultipleRepeatingOnTextEnded AndAlso dateOverToday < 1 AndAlso lastWasErronus = False Then
                            Select Case True
                                Case noFolderTexte = "" AndAlso noUserAlert = "" AndAlso curRow("ShowAlert") = True
                                    status = "Le texte et l''alerte"
                                    erronus = analyseResults.MissingTextNAlert
                                Case noFolderTexte = ""
                                    status = "Le texte"
                                    erronus = analyseResults.MissingText
                                Case Else
                                    status = "L''alerte"
                                    erronus = analyseResults.MissingAlert
                            End Select

                            lastWasErronus = True
                            Dim folderclosed As String = "(ClosingDate=)"
                            If curRow("FolderClosing") IsNot DBNull.Value Then folderclosed = IIf(rvDate.Equals(curRow("FolderClosing")), "Same ", "") & "ClosingDate=" & IIf(curRow("FolderActive") = 0, DateFormat.getTextDate(curRow("FolderClosing")), "")
                            Dim lastPresence As String = "(LastPresence=)"
                            If curRow("LastPresence") IsNot DBNull.Value Then lastPresence = "LastPresence=" & DateFormat.getTextDate(curRow("LastPresence"))
                            status &= " est/sont inexistant(e)(s) (" & folderclosed & ") (" & lastPresence & ")"
                        Else
                            nbFuturs += 1
                            status = "Texte futur"
                        End If

                        dateStarted = rvDate
                    Else
                        lastWasErronus = False
                        If dateOverToday < 1 And dateStarted.Date.Equals(rvDate.Date) = False Then
                            erronus = 2
                            status = "Le texte n''a pas la bonne date de début (" + DateFormat.getTextDate(rvDate) & ")"
                        End If
                    End If


                    'dataToBulk.AppendLine(IIf(NoFolderTexte = "", "", NoFolderTexte) & vbTab & curRow("NoFolderTexteType") & vbTab & curRow("NoFolder") & vbTab & "'" & TexteTitle.Replace("'", "''") & "'" & vbTab & IIf(DateStarted.Equals(LIMIT_DATE), "", DateFormat.AffTextDate(DateStarted)) & vbTab & IIf(DateFinished.Equals(LIMIT_DATE), "", DateFormat.AffTextDate(DateFinished)) & vbTab & CurNoMultiple & vbTab & Erronus & vbTab & "'" & Status & "'")
                    DBLinker.executeSQLScript("INSERT INTO #FolderTextesAnalyse (NoFolderTexte,NoFolderTexteType,NoMultiple, NoFolder, TexteTitle,DateStarted,DateFinished,HasText, Erronus , Status, ExternalStatus, Texte ) " & _
                    "VALUES (" & IIf(noFolderTexte = "", "null", noFolderTexte) & "," & curRow("NoFolderTexteType") & "," & curNoMultiple & "," & curRow("NoFolder") & ",'" & texteTitle.Replace("'", "''") & "'," & IIf(dateStarted.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(dateStarted) & "'") & "," & IIf(dateFinished.Equals(LIMIT_DATE), "null", "'" & DateFormat.getTextDate(dateFinished) & "'") & ",'" & hasText & "'," & erronus & ",'" & status & "'," & curRow("StartingExternalStatus") & ",'" & curRow("TextToApply").ToString().Replace("'", "''").Replace("\n", "") & "')", False, sqlConnection)

                    '--Set next
                    If curRow("Multiple") = True AndAlso nbFuturs < (nbFuturForNbDaysMultiple - 1) Then
                        curNoMultiple += 1
                        If curRow("TypeForMultiple") = 1 Then posVisite += curRow("NbPresencesMultiple")
                    Else
                        looping = False
                    End If
                End While

                '-- Analyse existing texts
                'Texte en trop
                Dim folderClosing As String = ""
                If curRow("FolderClosing") IsNot DBNull.Value Then folderClosing = DateFormat.getTextDate(curRow("FolderClosing"))
                Dim fta As DataSet = DBLinker.getInstance.readDBForGrid("#FolderTextesAnalyse", "*", "WHERE TexteTitle = '" & texteTitle.Replace("'", "''") & "'", , , , , , sqlConnection)

                DBLinker.executeSQLScript("INSERT INTO #FolderTextesAnalyse (NoFolderTexte,NoFolderTexteType,NoMultiple, NoFolder, TexteTitle,DateStarted,DateFinished,HasText, Erronus , Status, ExternalStatus, Texte ) " & _
                "SELECT FT.NoFolderTexte,FT.NoFolderTexteType,FT.NoMultiple,FT.NoFolder,FT.TexteTitle,FT.DateStarted,FT.DateFinished,FT.IsTexte," & analyseResults.UselessText & ",'Le texte est en trop (ClosingDate=" & folderClosing & ")" & If(rvDate = LIMIT_DATE, " (Il n''y aucune présence)", "") & "', 1, '' FROM FolderTextes AS FT INNER JOIN FolderDates as FD ON FD.NoFolder=FT.NoFolder  WHERE (SELECT COUNT(*) FROM #FolderTextesAnalyse AS FTA WHERE FTA.NoFolderTexte=FT.NoFolderTexte)=0 AND FT.NoFolder=" & curRow("NoFolder") & " AND FT.NoFolderTexteType=" & curRow("NoFolderTexteType"), False, sqlConnection)

                ''Texte en double
                'DBLinker.ExecuteSQLScript("INSERT INTO #FolderTextesAnalyse (NoFolderTexte,NoFolderTexteType,NoMultiple, NoFolder, TexteTitle,DateStarted,DateFinished,HasText, Erronus , Status ) " & _
                '    "SELECT nofoldertexte,nofoldertextetype,nomultiple,foldertextes.nofolder,foldertextes.textetitle,datestarted,datefinished,F" & AnalyseResults.DoubleText & ",'Le texte est en double' from foldertextes,(SELECT nofolder,textetitle from foldertextes group by nofolder,textetitle having count(*)>1) as t where foldertextes.nofolder=t.nofolder and foldertextes.textetitle=t.textetitle AND (SELECT COUNT(*) FROM #FolderTextesAnalyse AS FTA WHERE FTA.NoFolderTexte=FolderTextes.NoFolderTexte)=0 AND foldertextes.NoFolder=" & curRow("NoFolder") & " AND Foldertextes.NoFolderTexteType=" & curRow("NoFolderTexteType"))
            Next
        End With

        ''Bulk data in table
        'Dim tmpFile As String = AppPath & Bar(AppPath) & "Users\Temp\" & ConnectionsManager.currentUser
        'IO.Directory.CreateDirectory(tmpFile)
        'tmpFile &= "\faa-table.txt"
        'IO.File.WriteAllText(tmpFile, dataToBulk.ToString)
        'DBLinker.ExecuteSQLScript("BULK INSERT #FolderTextesAnalyse FROM '" & tmpFile & "'")

        '-- Analyse terminé

        analyseStatus.Text = "Statut :" & vbCrLf & "Analyse terminé en " & Base.DateFormat.transformIntervalToString(DateTime.Now.Subtract(startTime)) & " secondes"

        Return True
    End Function

    Private Sub analyse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles analyse.Click
        'Close previous connection
        If sqlConnection IsNot Nothing Then
            If sqlConnection.State <> ConnectionState.Closed AndAlso sqlConnection.State <> ConnectionState.Broken Then
                sqlConnection.Close()
            End If

            sqlConnection = Nothing
        End If

        'Do analysis
        If Me.createAnalyseTable() Then
            Me.generateReport.Enabled = True
            Me.correctErrors.Enabled = True
            Me.analyse.Enabled = True

            dgAnalyse.DataSource = DBLinker.getInstance.readDBForGrid("#FolderTextesAnalyse AS FTA INNER JOIN FolderTexteTypes AS FTT ON FTT.NoFolderTexteType=FTA.NoFolderTexteType INNER JOIN InfoFolders AS IF1 ON IF1.NoFolder=FTA.NoFolder INNER JOIN InfoClients AS IC ON IC.NoClient = IF1.NoClient", "Nom + ',' + Prenom + ' / ' + CAST(FTA.NoFolder AS varchar(max)) + ' / ' + IF1.Service as [Client / # dossier / Service],FTA.TexteTitle AS [Texte du],DATEADD(d,FTT.NbDaysDiff*-1,FTA.DateStarted) AS [Date de début],DateStarted as [Date du texte],DateFinished AS [Fait le], DATEADD(d,FTT.AlertNbDaysForExpiry,FTA.DateStarted) AS [Maximum le],CASE WHEN HasText=0 THEN 'Vrai' ELSE 'Faux' END AS Vide, Erronus as [Erreur],Status AS Statut", , , , , , False, sqlConnection).Tables(0)
        Else
            dgAnalyse.DataSource = Nothing
        End If
    End Sub

    Private Sub filterPerso_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles filterPerso.KeyUp
        If e.KeyCode = Keys.A AndAlso e.Control Then filterPerso.SelectAll()
    End Sub

    Private Sub filterPerso_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles filterPerso.MouseDoubleClick
        filterPerso.SelectAll()
    End Sub

    Private Sub filterPerso_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles filterPerso.MouseWheel

    End Sub

    Private Sub filterPerso_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles filterPerso.Validated
        Try
            If dgAnalyse.DataSource IsNot Nothing Then CType(dgAnalyse.DataSource, DataTable).DefaultView.RowFilter = filterPerso.Text
        Catch ex As Exception 'Accepte les erreurs (Invalid Filter)
            MessageBox.Show("Filtre invalide", "Filtre invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
            filterPerso.Text = String.Empty
        End Try
    End Sub

    Private Sub correctErrors_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles correctErrors.Click
        If MessageBox.Show("Avez-vous vérifier que les données de l'analyse correspondaient aux bons correctifs à appliquer ? Êtes-vous sûr de vouloir corriger ces erreurs ?", "Confirmation de correction", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = System.Windows.Forms.DialogResult.No Then Exit Sub

        correctErrorsSelected()
    End Sub

    Private Sub correctErrorsSelected()
        Dim error5Script As String = vbCrLf & "INSERT INTO FolderTextes (NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,TextePos,NoMultiple,IsTexte, ExternalStatus)" & vbCrLf & "SELECT NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,-1,NoMultiple,'0', ExternalStatus FROM #FolderTextesAnalyse WHERE  (Erronus=5 OR Erronus=1) ###ADVANCEDFILTER###"
        Dim error6Script As String = vbCrLf & "INSERT INTO UsersAlerts(NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message)" & vbCrLf & "SELECT NoTRPTraitant,'OpenClientAccount',CAST(if1.NoClient AS varchar(MAX)),DATEADD(d,NbDaysDiff*-1,DateStarted),DATEADD(d,1,DateStarted),CAST(dbo.AffTextDate(DATEADD(d,NbDaysDiff*-1,DateStarted)) as varchar(10)) + ':00:00:' + CAST(if1.NoClient as varchar(max)) + ':'+ CAST(FTA.NoFolder as varchar(max)) + ':True',1,1,ftt.AlertMessageArticle + CASE WHEN ftt.AlertMessageArticle='' THEN '' ELSE ' ' END + ftt.TexteTitle + ' ' + CAST(NoMultiple as varchar(max)) + ' pour le client ' + ic.Nom + ',' + ic.Prenom + ' du dossier #' + CAST(fta.nofolder as varchar(max)) + ' est dû le ' + dbo.AffTextDate(datestarted) FROM #FolderTextesAnalyse AS FTA INNER JOIN infofolders as if1 on if1.nofolder=fta.nofolder inner join infoclients as ic on ic.noclient=if1.noclient inner join foldertextetypes as ftt on ftt.nofoldertextetype=fta.nofoldertextetype WHERE  (Erronus=6 OR Erronus=1) ###ADVANCEDFILTER###" & vbCrLf & "INSERT INTO FolderTexteAlerts(NoUserAlert,NoFolderTexte)" & vbCrLf & "SELECT (SELECT NoUserAlert FROM UsersAlerts WHERE Message=t.mes),(SELECT NoFolderTexte FROM FolderTextes as ft WHERE ft.NoFolder=t.nofolder and ft.nofoldertextetype=t.nofoldertextetype and ft.nomultiple = t.nomultiple) FROM (SELECT fta.NoFolderTexteType,NoMultiple,fta.NoFolder,ftt.AlertMessageArticle + CASE WHEN ftt.AlertMessageArticle='' THEN '' ELSE ' ' END + ftt.TexteTitle + ' ' + CAST(NoMultiple as varchar(max)) + ' pour le client ' + ic.Nom + ',' + ic.Prenom + ' du dossier #' + CAST(fta.nofolder as varchar(max)) + ' est dû le ' + dbo.AffTextDate(datestarted) as mes FROM #FolderTextesAnalyse AS FTA INNER JOIN infofolders as if1 on if1.nofolder=fta.nofolder inner join infoclients as ic on ic.noclient=if1.noclient inner join foldertextetypes as ftt on ftt.nofoldertextetype=fta.nofoldertextetype WHERE  (Erronus=6 OR Erronus=1) ###ADVANCEDFILTER###) as t"
        Dim error1Script As String = error5Script & error6Script
        Dim error2Script As String = vbCrLf & "UPDATE FolderTextes SET DateStarted=SUBSTRING(Status,CHARINDEX('(',Status)+1,10) FROM FolderTextes AS FT INNER JOIN #FolderTextesAnalyse AS FTA ON FTA.NoFolderTexte=FT.NoFolderTexte WHERE FTA.Erronus=2 ###ADVANCEDFILTER###"
        Dim error3Script As String = vbCrLf & "DELETE FROM UsersAlerts WHERE NoUserAlert IN (SELECT NoUserAlert FROM #FolderTextesAnalyse FTA INNER JOIN FolderTexteAlerts FTA1 ON FTA1.NoFolderTexte = FTA.NoFolderTexte WHERE Erronus=3)" & vbCrLf & "DELETE FROM FolderTexteAlerts WHERE NoFolderTexte IN (SELECT NoFolderTexte FROM #FolderTextesAnalyse WHERE Erronus=3)" & vbCrLf & "DELETE FROM FolderTextes WHERE NoFolderTexte IN (SELECT NoFolderTexte FROM #FolderTextesAnalyse WHERE Erronus=3 ###ADVANCEDFILTER###)"
        Dim error4Script As String = vbCrLf & "DELETE FROM UsersAlerts WHERE NoUserAlert IN (SELECT NoUserAlert FROM #FolderTextesAnalyse FTA INNER JOIN FolderTexteAlerts FTA1 ON FTA1.NoFolderTexte = FTA.NoFolderTexte WHERE Erronus=4)" & vbCrLf & "DELETE FROM FolderTexteAlerts WHERE NoFolderTexte IN (SELECT NoFolderTexte FROM #FolderTextesAnalyse WHERE Erronus=4)" & vbCrLf & "DELETE FROM FolderTextes WHERE NoFolderTexte IN (SELECT NoFolderTexte FROM #FolderTextesAnalyse WHERE Erronus=4 ###ADVANCEDFILTER###)"
        Dim scriptAll As String = error1Script & error2Script & error3Script & error4Script

        Dim errorsToCorrect As String = ""
        Select Case Erronus.SelectedIndex
            Case 1
                errorsToCorrect = error1Script
            Case 2
                errorsToCorrect = error2Script
            Case 3
                errorsToCorrect = error3Script
            Case 4
                errorsToCorrect = error4Script
            Case 5
                errorsToCorrect = error5Script
            Case 6
                errorsToCorrect = error6Script
            Case Else
                errorsToCorrect = scriptAll
        End Select

        'TODO : To work, it shall replace column name of grid to dataset ones
        errorsToCorrect = errorsToCorrect.Replace("###ADVANCEDFILTER###", "")  'If(filterPerso.Text.Trim() = String.Empty, String.Empty, " AND (" & filterPerso.Text & ")"))

        analyseStatus.Text = "Correction des erreurs en cours... Veuillez patienter, merci !"
        correctErrors.Enabled = False
        Application.DoEvents()
        DBLinker.executeSQLScript(errorsToCorrect, , sqlConnection)
        analyseStatus.Text = "Correction des erreurs terminés"
    End Sub

    Private Sub ouvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem.Click
        Dim folderColName As String = "Client / # dossier / Service"
        Dim nofolder As String
        Dim firstPos As Integer

        nofolder = dgAnalyse.currentRow.Cells(folderColName).Value.ToString
        firstPos = nofolder.IndexOf("/") + 2
        nofolder = nofolder.Substring(firstPos, nofolder.LastIndexOf("/") - firstPos - 1)

        Dim noAccount() As String = DBLinker.getInstance.readOneDBField("InfoFolders", "TOP 1 NoClient", "WHERE NoFolder=" & nofolder)
        openAccount(noAccount(0))
    End Sub

    Private Sub afficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfficherTousLesTextesDesDossiersPrésentementsAffichésToolStripMenuItem.Click
        If filterPerso.Text = "" OrElse CType(dgAnalyse.DataSource, DataTable).DefaultView.Count = 0 Then Exit Sub

        Dim folderColName As String = "Client / # dossier / Service"
        Dim nofolder As String
        Dim fPerso As String = ""
        Dim firstPos As Integer
        With CType(dgAnalyse.DataSource, DataTable).DefaultView
            For i As Integer = 0 To .Count - 1
                nofolder = .Item(i)(folderColName).ToString
                firstPos = nofolder.IndexOf("/") + 2
                nofolder = nofolder.Substring(firstPos, nofolder.LastIndexOf("/") - firstPos - 1)
                If fPerso.IndexOf("," & nofolder & ",") = -1 And fPerso.EndsWith("," & nofolder) = False Then
                    fPerso &= "," & nofolder
                End If
            Next i
        End With

        If fPerso <> "" Then
            fPerso = fPerso.Substring(1)
            fPerso = "[" & folderColName & "] LIKE '%/ " & fPerso.Replace(",", " /%' OR [" & folderColName & "] LIKE '%/ ") & " /%'"
            filterPerso.Text = fPerso
            filterPerso_Validated(Me, e)
        End If
    End Sub

    Private Sub generateReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles generateReport.Click
        ReportGeneration.startRapportGeneration("Analyse des textes des dossiers", Nothing, CType(dgAnalyse.DataSource, DataTable).DefaultView.ToTable())
    End Sub

    Private Sub dgAnalyse_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgAnalyse.DataBindingComplete
        countNb()
    End Sub

    Private Sub countNb()
        Dim nbErrors As Integer = 0
        With dgAnalyse.Rows
            For i As Integer = 0 To .Count - 1
                If .Item(i).Cells("Erreur").Value <> 0 Then nbErrors += 1
            Next i

            nbFolderTexte.Text = .Count
            Me.nbErrors.Text = nbErrors
        End With
    End Sub

    Public Overrides ReadOnly Property savingMethod() As System.Delegate
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)

    End Sub

    Private Sub dgAnalyse_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgAnalyse.MouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            OuvrirLeCompteClientDuDernierTexteSélectionnéToolStripMenuItem.Enabled = dgAnalyse.SelectedRows.Count <> 0
        End If
    End Sub

    Private Sub FolderTextsAnalysis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If sqlConnection IsNot Nothing Then
            If sqlConnection.State <> ConnectionState.Closed AndAlso sqlConnection.State <> ConnectionState.Broken Then
                sqlConnection.Close()
            End If

            sqlConnection = Nothing
        End If
    End Sub
End Class
