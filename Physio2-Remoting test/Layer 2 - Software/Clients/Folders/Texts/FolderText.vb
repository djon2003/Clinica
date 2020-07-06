Imports CI.Clinica.Accounts.Clients.Folders.Codifications

Namespace Accounts.Clients.Folders

    Public Class FolderText

        Private curData As DataRow
        Private curFTT As FolderTextType

        Public Sub New(ByVal data As DataRow)
            curData = data
        End Sub

#Region "Properties"
        Public ReadOnly Property noFolderTexte() As Integer
            Get
                Return curData("NoFolderTexte")
            End Get
        End Property

        Public ReadOnly Property noFolderTexteType() As Integer
            Get
                Return curData("NoFolderTexteType")
            End Get
        End Property

        Public ReadOnly Property noFolder() As Integer
            Get
                Return curData("NoFolder")
            End Get
        End Property

        Public ReadOnly Property noMultiple() As Integer
            Get
                Return IIf(curData("NoMultiple") Is DBNull.Value, 1, curData("NoMultiple"))
            End Get
        End Property

        Public Property textTitle() As String
            Get
                Return curData("TexteTitle")
            End Get
            Set(ByVal value As String)
                curData("TexteTitle") = value
            End Set
        End Property

        Public Property text() As String
            Get
                Return curData("Texte")
            End Get
            Set(ByVal value As String)
                curData("Texte") = value
            End Set
        End Property

        Public Property textPosition() As String
            Get
                Return curData("TextePos")
            End Get
            Set(ByVal value As String)
                curData("TextePos") = value
            End Set
        End Property

        Public Property dateStarted() As Date
            Get
                Dim returnDate As Date = LIMIT_DATE
                If curData("DateStarted") IsNot DBNull.Value Then returnDate = curData("DateStarted")
                Return returnDate
            End Get
            Set(ByVal value As Date)
                curData("DateStarted") = IIf(value.Equals(LIMIT_DATE), DBNull.Value, value)
            End Set
        End Property

        Public Property dateFinished() As Date
            Get
                Dim returnDate As Date = LIMIT_DATE
                If curData("DateFinished") IsNot DBNull.Value Then returnDate = curData("DateFinished")
                Return returnDate
            End Get
            Set(ByVal value As Date)
                curData("DateFinished") = IIf(value.Equals(LIMIT_DATE), DBNull.Value, value)
            End Set
        End Property
#End Region

        Public Function getFolderTexteType() As FolderTextType
            curFTT = FolderTextTypesManager.getInstance.getItemable(Me.noFolderTexteType)

            Return curFTT
        End Function

        Public Overrides Function toString() As String
            Return textTitle
        End Function

        Public Shared Function add(ByVal noFolderTexteType As Integer, ByVal noClient As Integer, ByVal noFolder As Integer, ByVal texteTitle As String, ByVal dateStarted As Date, ByVal noMultiple As Integer, ByVal noUserOfAlert As Integer, Optional ByVal clientName As String = "", Optional ByVal executeScript As Boolean = True) As String
            Dim curFTT As FolderTextType = FolderTextTypesManager.getInstance.getItemable(noFolderTexteType)

            Dim modelText As String = ""
            If curFTT.modelAppliedOnCreation <> 0 Then
                Dim model() As String = DBLinker.getInstance.readOneDBField("Modeles", "Modele", "WHERE NoModele=" & curFTT.modelAppliedOnCreation)
                If model IsNot Nothing AndAlso model.Length <> 0 Then modelText = model(0).Replace("\n", vbCrLf)
            End If

            Dim inserting As String = "INSERT INTO FolderTextes (NoFolderTexteType,NoFolder,TexteTitle,DateStarted,NoMultiple, ExternalStatus, Texte, IsTexte" & If(curFTT.terminatedOnCreation, ",DateFinished", String.Empty) & ") VALUES(" & noFolderTexteType & "," & noFolder & ",'" & texteTitle.Replace("'", "''") & "','" & DateFormat.getTextDate(dateStarted) & "'," & noMultiple & "," & curFTT.startingExternalStatus & ",'" & modelText.Replace("'", "''") & "','" & (modelText <> "") & "'" & If(curFTT.terminatedOnCreation, ",'" & DateFormat.getTextDate(dateStarted) & "'", String.Empty) & ");"
            If curFTT.showAlert Then
                dateStarted = New Date(dateStarted.Year, dateStarted.Month, dateStarted.Day)

                Dim newAlert As New AlertOfClientAccount(noClient)
                newAlert.noUser = noUserOfAlert
                newAlert.isHidden = curFTT.showAlarm
                newAlert.showingDate = dateStarted.AddDays(curFTT.nbDaysDiff * IIf(curFTT.isNbDaysDiffBefore, -1, 1))
                newAlert.expiryDate = CType(IIf(date1Infdate2(dateStarted, newAlert.showingDate), newAlert.showingDate, dateStarted), Date).AddDays(curFTT.alertNbDaysForExpiry + 1)

                newAlert.message = curFTT.alertMessageArticle
                If newAlert.message <> "" Then
                    newAlert.message &= IIf(Chaines.isAlpha(newAlert.message.Substring(newAlert.message.Length - 1)) = True, " ", "") & texteTitle.Substring(0, 1).ToLower & texteTitle.Substring(1)
                Else
                    newAlert.message &= texteTitle
                End If
                newAlert.message &= " pour le client " & IIf(clientName = "", getClientName(noClient), clientName) & " du dossier #" & noFolder & " est dû le " & DateFormat.getTextDate(newAlert.expiryDate.AddDays(-1))

                If curFTT.showAlarm Then
                    Dim newAlarm As New AlarmOfClientAccount(newAlert.showingDate, noClient, noFolder, curFTT.multiple AndAlso curFTT.typeForMultiple = FolderTextType.TypeMultiple.NbDaysX)
                    newAlert.alertAlarm = newAlarm
                End If

                inserting &= newAlert.getSqlQuery(DBLinker.QueryTypes.Add)
                If inserting.EndsWith(";") = False Then inserting &= ";"
                inserting &= "INSERT INTO FolderTexteAlerts(NoFolderTexte,NoUserAlert) VALUES(IDENT_CURRENT('FolderTextes'),IDENT_CURRENT('UsersAlerts'));"
            End If

            If executeScript Then DBLinker.executeSQLScript(inserting)

            Return inserting
        End Function

        Public Shared Function getFromAlert(ByVal noUserAlert As Integer) As FolderText
            Dim dsFT As DataSet = DBLinker.getInstance.readDBForGrid("SELECT FolderTextes.* FROM FolderTextes INNER JOIN FolderTexteAlerts ON FolderTexteAlerts.NoFolderTexte = FolderTextes.NoFolderTexte WHERE FolderTexteAlerts.NoUserAlert=" & noUserAlert)
            If dsFT Is Nothing OrElse dsFT.Tables(0).Rows.Count = 0 Then Return Nothing

            Return New FolderText(dsFT.Tables(0).Rows(0))
        End Function

    End Class

End Namespace
