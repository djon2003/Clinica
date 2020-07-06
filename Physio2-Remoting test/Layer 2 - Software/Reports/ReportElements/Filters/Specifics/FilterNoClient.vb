Public Class FilterNoClient
    Inherits BasicFilter

    Private Const ALL_FOLDERS_CHOICE As String = "* Tous les dossiers *"

    Private _CurrentReturn As ClientSelectorReturn
    Private _AskNoFolder As Boolean = False
    Private _askFolderText As Boolean = False
    Private _All As Boolean = False
    Private _AllOnlyFolders As Boolean = False
    Private _ShowInactiveFolders As Boolean = True
    Private _folderTextTilteFilter As String = String.Empty
    Private _FolderField As String = String.Empty

#Region "Properties"
    Public Property all() As Boolean
        Get
            Return _All
        End Get
        Set(ByVal value As Boolean)
            _All = value
        End Set
    End Property

    Public Property folderTextTilteFilter() As String
        Get
            Return _folderTextTilteFilter
        End Get
        Set(ByVal value As String)
            _folderTextTilteFilter = value
        End Set
    End Property

    Public Property askFolderText() As Boolean
        Get
            Return _askFolderText
        End Get
        Set(ByVal value As Boolean)
            _askFolderText = value
        End Set
    End Property

    Public Property showInactiveFolders() As Boolean
        Get
            Return _ShowInactiveFolders
        End Get
        Set(ByVal value As Boolean)
            _ShowInactiveFolders = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As ClientSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property

    Public Property askNoFolder() As Boolean
        Get
            Return _AskNoFolder
        End Get
        Set(ByVal value As Boolean)
            _AskNoFolder = value
        End Set
    End Property
#End Region

    Private Function chooseNoClient() As ClientSelectorReturn
        Dim myReturn As New ClientSelectorReturn
        myReturn.whereStr = ""
        myReturn.filtrageTexte = ""
        myReturn.canceling = False

        Dim lastFoundClient As Integer = -1
        If Not foundClient Is Nothing AndAlso foundClient.Length <> 0 Then lastFoundClient = foundClient.GetUpperBound(0)

        Dim myRecherche As New clientSearch
        myRecherche.from = Me
        myRecherche.MdiParent = Nothing
        myRecherche.Visible = False
        myRecherche.ShowDialog()

        Dim shouldCancelOnWindowClose As Boolean = all = False OrElse (_AskNoFolder AndAlso _AllOnlyFolders)

        If Not foundClient Is Nothing AndAlso foundClient.Length <> 0 Then
            If foundClient.GetUpperBound(0) > lastFoundClient Then
                lastFoundClient = foundClient.GetUpperBound(0)
                myReturn.noClient = foundClient(lastFoundClient).noClient
                myReturn.clientFullName = foundClient(lastFoundClient).fullName
                myReturn.noFolder = 0
                myReturn.filtrageTexte = "<tr><td>Client</td><td> : </td><td>" & foundClient(lastFoundClient).fullName & "</td></tr>"
                myReturn.whereStr = tableDotField & "=" & myReturn.noClient
            Else
                If shouldCancelOnWindowClose Then
                    myReturn.canceling = True
                Else
                    myReturn.filtrageTexte = "<tr><td>Client</td><td> : </td><td>Tous</td></tr>"
                End If
            End If
        Else
            If shouldCancelOnWindowClose Then
                myReturn.canceling = True
            Else
                myReturn.filtrageTexte = "<tr><td>Client</td><td> : </td><td>Tous</td></tr>"
            End If
        End If

        Return myReturn
    End Function

    Private Function chooseNoFolder() As ClientSelectorReturn
        Dim myReturn As ClientSelectorReturn = chooseNoClient()
        If myReturn.canceling Or myReturn.whereStr = "" Then Return myReturn

        'REM_CODES
        Dim folders As DataSet = DBLinker.getInstance.readDBForGrid("SiteLesion RIGHT JOIN Infofolders ON SiteLesion.NoSiteLesion = Infofolders.NoSiteLesion", "Infofolders.NoFolder, SiteLesion.SiteLesion, Infofolders.NoCodeUnique, InfoFolders.Service", "WHERE ((Infofolders.NoClient)=" & myReturn.noClient & IIf(showInactiveFolders, "", " AND Infofolders.StatutOuvert=1") & ");")
        If folders Is Nothing OrElse folders.Tables.Count = 0 OrElse folders.Tables(0).Rows.Count = 0 Then
            myReturn.canceling = True
        Else
            Dim myMultiChoice As New multichoice
            Dim choices As String = ""
            For Each curRow As DataRow In folders.Tables(0).Rows
                Dim siteLesion As String = ""
                If curRow("SiteLesion") IsNot DBNull.Value Then siteLesion = " - " & curRow("SiteLesion")
                choices &= "§#" & curRow("NoFolder") & siteLesion & " (" & Accounts.Clients.Folders.Codifications.FolderCodesManager.getInstance.getCodeNameByNoUnique(curRow("NoCodeUnique")) & ") de " & curRow("Service").ToString.ToLower()
            Next
            If all AndAlso _FolderField <> String.Empty Then
                choices = ALL_FOLDERS_CHOICE & choices
            Else
                choices = choices.Substring(1)
            End If
            Dim monFolder As String = myMultiChoice.GetChoice("Veuillez choisir le dossier", choices, , "§", False)
            If monFolder <> "" And monFolder.StartsWith("ERROR") = False Then
                myReturn.whereStr = Me.tableDotField & "=" & myReturn.noClient
                If monFolder = ALL_FOLDERS_CHOICE Then
                    myReturn.noFolder = 0
                    myReturn.filtrageTexte = "<tr><td>Client / dossier</td><td> : </td><td>" & myReturn.clientFullName & " / " & ALL_FOLDERS_CHOICE & "</td></tr>"
                Else
                    Dim sMonFolder() As String = monFolder.Split(" ")
                    myReturn.noFolder = Chaines.onlyNumeric(sMonFolder(0))
                    myReturn.filtrageTexte = "<tr><td>Client / dossier</td><td> : </td><td>" & myReturn.clientFullName & " / " & sMonFolder(0) & "</td></tr>"
                    If _FolderField <> String.Empty Then
                        myReturn.whereStr &= " AND (" & _FolderField & "=" & sMonFolder(0) & " OR " & _FolderField & "=0 OR " & _FolderField & " IS NULL)"
                    Else
                        myReturn.whereStr = Me.tableDotField & "=" & sMonFolder(0)
                    End If
                End If
            Else
                myReturn.canceling = True
            End If
        End If

        Return myReturn
    End Function

    Private Function chooseFolderText() As ClientSelectorReturn
        _All = False 'Ensure that All is disabled

        Dim myReturn As ClientSelectorReturn = chooseNoFolder()
        If myReturn.canceling Or myReturn.whereStr = "" Then Return myReturn

        Dim textTitleFilter As String = String.Empty
        If folderTextTilteFilter <> String.Empty Then
            textTitleFilter = " AND TexteTitle LIKE '%" & folderTextTilteFilter.Replace("'", "''") & "%'"
        End If
        Dim texts() As String = DBLinker.getInstance.readOneDBField("FolderTextes", "CAST(NoFolderTexte AS varchar(MAX)) + '-' + TexteTitle", "DateStarted <= GETDATE() AND NoFolder=" & myReturn.noFolder & textTitleFilter)

        If texts Is Nothing OrElse texts.Length = 0 Then
            myReturn.canceling = True
        Else
            Dim myMultiChoice As New multichoice
            Dim choices As String = String.Join("§", texts)
            Dim myText As String = myMultiChoice.GetChoice("Veuillez choisir le texte du dossier", choices, , "§", False)
            If myText <> "" And myText.StartsWith("ERROR") = False Then
                Dim sMyText() As String = myText.Split(New Char() {"-"}, 2)
                myReturn.noFolderTexte = Chaines.onlyNumeric(sMyText(0))
                myReturn.folderTextTitle = sMyText(1)
                myReturn.filtrageTexte = "<tr><td>Client / dossier / texte</td><td> : </td><td>" & myReturn.clientFullName & " / " & myReturn.noFolder & " / " & myReturn.folderTextTitle & "</td></tr>"
                myReturn.whereStr = Me.tableDotField & "=" & myReturn.noFolderTexte
            Else
                myReturn.canceling = True
            End If
        End If

        Return myReturn
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        With CType(filtered, FilteringNoClient)
            _CurrentReturn = .currentReturn
            If .currentReturn.noFolder = 0 Then
                _CurrentReturn.filtrageTexte = "<tr><td>Client</td><td> : </td><td>" & _CurrentReturn.clientFullName & "</td></tr>"
                _CurrentReturn.whereStr = tableDotField & "=" & _CurrentReturn.noClient
            ElseIf .currentReturn.noFolderTexte = 0 Then
                _CurrentReturn.filtrageTexte = "<tr><td>Client / dossier</td><td> : </td><td>" & _CurrentReturn.clientFullName & " / " & _CurrentReturn.noFolder & "</td></tr>"
                If _FolderField <> String.Empty Then
                    _CurrentReturn.whereStr = Me.tableDotField & "=" & _CurrentReturn.noClient & " AND (" & _FolderField & "=" & _CurrentReturn.noFolder & " OR " & _FolderField & "=0 OR " & _FolderField & " IS NULL )"
                Else
                    _CurrentReturn.whereStr = Me.tableDotField & "=" & _CurrentReturn.noFolder
                End If
            Else
                _CurrentReturn.filtrageTexte = "<tr><td>Client / dossier / texte</td><td> : </td><td>" & _CurrentReturn.clientFullName & " / " & _CurrentReturn.noFolder & " / " & _CurrentReturn.folderTextTitle & "</td></tr>"
                _CurrentReturn.whereStr = tableDotField & "=" & _CurrentReturn.noFolderTexte
            End If
            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Dim myReturn As ClientSelectorReturn
        If askFolderText Then
            myReturn = Me.chooseFolderText()
        ElseIf askNoFolder Then
            myReturn = Me.chooseNoFolder()
        Else
            myReturn = Me.chooseNoClient()
        End If
        Me._CurrentReturn = myReturn

        Return New FilterResult(myReturn, Me.filterOnReportParts, myReturn.canceling)
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "AskNoFolder"
                    _AskNoFolder = myKey.Value
                Case "All"
                    _All = myKey.Value
                Case "ShowInactiveFolders"
                    _ShowInactiveFolders = myKey.Value
                Case "AskFolderText"
                    _askFolderText = myKey.Value
                Case "FolderTextTilteFilter"
                    _folderTextTilteFilter = myKey.Value
                Case "AllOnlyFolders"
                    _AllOnlyFolders = myKey.Value
                Case "FolderField"
                    _FolderField = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
