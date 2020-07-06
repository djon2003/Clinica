Namespace Accounts.Clients.Folders.Codifications

    Public Class FolderCodesManager
        Inherits DBItemableManagerBase(Of FolderCodesManager, FolderCode)

        Private names_noUnique As New Generic.Dictionary(Of String, Integer)
        Private noUnique_names As New Generic.Dictionary(Of Integer, String)

        Protected Sub New()
            load()
        End Sub

        Private Function containsCodeName(ByVal name As String) As Boolean
            Dim containingCode As Boolean = False
            changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
            containingCode = names_noUnique.ContainsKey(name)
            changingItemablesLock.ReleaseReaderLock()

            Return containingCode
        End Function

        Private Function containsCodeUnique(ByVal noUnique As Integer) As Boolean
            Dim containingCode As Boolean = False
            changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
            containingCode = noUnique_names.ContainsKey(noUnique)
            changingItemablesLock.ReleaseReaderLock()

            Return containingCode
        End Function

        Public Function getNoUniqueByCodeName(ByVal name As String) As Integer
            name = name.ToUpper
            If containsCodeName(name) = False Then Return 0

            changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
            Dim myCode As Integer = names_noUnique(name)
            changingItemablesLock.ReleaseReaderLock()

            Return myCode
        End Function


        Public Function getCodeNames() As Generic.List(Of String)
            changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
            Dim myNames As New Generic.List(Of String)(noUnique_names.Values)
            changingItemablesLock.ReleaseReaderLock()

            Return myNames
        End Function

        Public Function getCodeNameByNoUnique(ByVal noUnique As Integer) As String
            If containsCodeUnique(noUnique) = False Then Return "Code non trouvé"

            changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
            Dim myCode As String = noUnique_names(noUnique)
            changingItemablesLock.ReleaseReaderLock()

            Return myCode
        End Function

        Private Function getMinDefaultDate() As Date
            Dim minDate As Date = LIMIT_DATE

            For Each curCode As FolderCode In getItemables(0)
                minDate = If(minDate < curCode.firstEffectiveTime, minDate, curCode.firstEffectiveTime)
            Next

            Return minDate
        End Function

        Public Function getAllDatesOfUser(ByVal noUser As Integer) As Generic.List(Of DateInterval)
            Dim dateDebutUser As Date = LIMIT_DATE
            Dim dates As New Generic.List(Of DateInterval)()

            If noUser = 0 Then
                dateDebutUser = UserDefault.getInstance.startingDate
            Else
                dateDebutUser = UsersManager.getInstance.getUser(noUser).startingDate
                Dim minDefDate As Date = getMinDefaultDate()
                If minDefDate <> LIMIT_DATE AndAlso minDefDate > dateDebutUser Then dates.Add(New DateInterval(minDefDate))
            End If
            dates.Add(New DateInterval(dateDebutUser))

            For Each curCode As FolderCode In getItemables(noUser)
                Dim fet As Date = curCode.firstEffectiveTime
                If fet < dateDebutUser Then fet = dateDebutUser

                If fet <= curCode.lastEffectiveTime Then dates.Add(New DateInterval(fet, If(curCode.lastEffectiveTime = LIMIT_DATE, LIMIT_DATE, curCode.lastEffectiveTime.AddDays(1))))
            Next


            dates = DateInterval.createTimeline(dates)
            dates.Sort()

            Return dates
        End Function

        Public Overloads Function getItemable(ByVal noUnique As Integer, ByVal noUser As Integer, ByVal applicationDate As Date) As FolderCode
            For Each curCode As FolderCode In MyBase.getItemables()
                If curCode.noUser = noUser AndAlso curCode.noUnique = noUnique AndAlso curCode.isEffective(applicationDate) Then
                    Return curCode
                End If
            Next

            'Get default code if none found for specific user
            If noUser > 0 Then
                Return getItemable(noUnique, 0, applicationDate)
            End If

            Return Nothing
        End Function

        Public Overloads Function getItemable(ByVal codeName As String, ByVal noUser As Integer, ByVal applicationDate As Date) As FolderCode
            Return getItemable(getNoUniqueByCodeName(codeName), noUser, applicationDate)
        End Function

        Private Function getNextPreviousCode(ByVal noUnique As Integer, ByVal noUser As Integer, ByVal applicationDate As Date, ByVal isPrevious As Boolean) As FolderCode
            Dim userDates As Generic.List(Of DateInterval) = getAllDatesOfUser(noUser)
            If Not isPrevious Then userDates.Reverse()

            Dim curCode As FolderCode
            Dim i As Integer = 0
            For Each curUserDate As DateInterval In userDates
                If curUserDate.isDateBetween(applicationDate) Then
                    applicationDate = If(isPrevious, curUserDate.from.AddDays(-1), If(curUserDate.to = LIMIT_DATE, LIMIT_DATE, curUserDate.to.AddDays(1)))
                    curCode = getItemable(noUnique, noUser, curUserDate.from)
                    Exit For
                End If
                i += 1
            Next

            Dim searchedCode As FolderCode = getItemable(noUnique, noUser, applicationDate)
            i += 1
            While i < userDates.Count AndAlso searchedCode IsNot Nothing AndAlso searchedCode.Equals(curCode) = True
                applicationDate = If(isPrevious, userDates(i).from.AddDays(-1), If(userDates(i).to = LIMIT_DATE, LIMIT_DATE, userDates(i).to.AddDays(1)))
                searchedCode = getItemable(noUnique, noUser, applicationDate)
                i += 1
            End While

            If searchedCode IsNot Nothing AndAlso searchedCode.Equals(curCode) Then searchedCode = Nothing

            Return searchedCode
        End Function

        Public Function getPreviousCode(ByVal noUnique As Integer, ByVal noUser As Integer, ByVal applicationDate As Date) As FolderCode
            Return getNextPreviousCode(noUnique, noUser, applicationDate, True)
        End Function

        Public Function getNextCode(ByVal noUnique As Integer, ByVal noUser As Integer, ByVal applicationDate As Date) As FolderCode
            Return getNextPreviousCode(noUnique, noUser, applicationDate, False)
        End Function


        Public Overloads Function getItemables(ByVal noUser As Integer, ByVal applicationDate As Date) As Generic.List(Of FolderCode)
            Return _getItemables(noUser, applicationDate, True)
        End Function

        Public Overloads Function getItemables(ByVal noUser As Integer) As Generic.List(Of FolderCode)
            Return _getItemables(noUser, LIMIT_DATE, False)
        End Function

        Private Function sortCodesByDate(ByVal first As FolderCode, ByVal second As FolderCode) As Integer
            Return first.firstEffectiveTime.CompareTo(second.firstEffectiveTime)
        End Function

        Public Overloads Function getItemables(ByVal noUnique As Integer, ByVal dateSorting As SortOrder) As Generic.List(Of FolderCode)
            Dim returnCodes As New Generic.List(Of FolderCode)
            Dim curCodes As Generic.List(Of FolderCode) = getItemables()
            curCodes.Sort(New Comparison(Of FolderCode)(AddressOf sortCodesByDate))
            If dateSorting = SortOrder.Descending Then curCodes.Reverse()

            For Each curCode As FolderCode In curCodes
                If curCode.noUnique = noUnique Then returnCodes.Add(curCode)
            Next

            Return returnCodes
        End Function

        Private Function _getItemables(ByVal noUser As Integer, ByVal applicationDate As Date, ByVal useDate As Boolean) As Generic.List(Of FolderCode)
            Dim returnCodes As New Generic.List(Of FolderCode)
            Dim codesUniqueNo As New Generic.Dictionary(Of Integer, Integer)
            Dim addDefCodes As Boolean = noUser <> 0 AndAlso PreferencesManager.getGeneralPreferences()("AffDefCodesInSpecificTRP").ToString <> "" AndAlso CType(PreferencesManager.getGeneralPreferences()("AffDefCodesInSpecificTRP"), Boolean) = True
            Dim curCodes As Generic.List(Of FolderCode) = getItemables()
            curCodes.Sort()
            Dim nos As String = ""
            For Each curCode As FolderCode In curCodes
                nos &= "," & curCode.noUser
            Next

            For Each curCode As FolderCode In curCodes
                If useDate = False OrElse curCode.isEffective(applicationDate) Then
                    If curCode.noUser = noUser Then
                        If codesUniqueNo.ContainsKey(curCode.noUnique) = False Then
                            codesUniqueNo.Add(curCode.noUnique, returnCodes.Count)
                            returnCodes.Add(curCode)
                        Else
                            If returnCodes(codesUniqueNo(curCode.noUnique)).noUser <> curCode.noUser Then
                                returnCodes(codesUniqueNo(curCode.noUnique)) = curCode
                            Else
                                returnCodes.Add(curCode)
                            End If
                        End If
                    ElseIf curCode.noUser = 0 AndAlso addDefCodes Then
                        If useDate AndAlso codesUniqueNo.ContainsKey(curCode.noUnique) = False Then
                            codesUniqueNo.Add(curCode.noUnique, returnCodes.Count)
                            returnCodes.Add(curCode)
                        ElseIf useDate = False Then
                            returnCodes.Add(curCode)
                        End If
                    End If
                End If
            Next

            'If no codes found (because def codes were not added due to a pref), then return all the def ones
            If returnCodes.Count = 0 AndAlso noUser <> 0 Then Return _getItemables(0, applicationDate, useDate)

            Return returnCodes
        End Function

        Public Function isUserDefault(ByVal noUser As Integer, ByVal applicationDate As Date) As Boolean
            For Each curCode As FolderCode In getItemables()
                If curCode.noUser = noUser AndAlso curCode.isEffective(applicationDate) Then Return False
            Next

            Return True
        End Function

        Private Sub _load(ByVal noCodification As Integer)
            Dim dsFoldersCodes As DataSet
            'Read from SQL server
            dsFoldersCodes = DBLinker.getInstance.readDBForGrid("CodificationsDossiers INNER JOIN CodesDossiersCodes ON NoCodeUnique = NoUnique", "CodificationsDossiers.*, CodeName as Nom", "WHERE " & If(noCodification = 0, "1=1", "noCodification=" & noCodification) & " ORDER BY nouser desc, firsteffectivetime desc", , , "CodeTable")
            DBLinker.getInstance.readDBForGrid("CodesDossiersPeriodes LEFT JOIN KeyPeople ON CodesDossiersPeriodes.NoKP=KeyPeople.NoKP", "NoCDPeriode,NoCodification,IsEval,IsDefault,NoPeriode,Montant,PourcentAbsence,PourcentClient,null AS Button,CASE WHEN CodesDossiersPeriodes.NoKP=0 OR CodesDossiersPeriodes.NoKP IS NULL THEN 'Aucun(e)' ELSE KeyPeople.Nom END AS KPName, CodesDossiersPeriodes.NoKP", If(noCodification = 0, "", "WHERE noCodification=" & noCodification), , , "CodePeriodeTable", dsFoldersCodes)
            DBLinker.getInstance.readDBForGrid("CodesDossiersFolderTexteTypes", "*", , , , "FTTTable", dsFoldersCodes)
            DBLinker.getInstance.readDBForGrid("CodesDossiersFolderAlertTypes", "*", , , , "FATTable", dsFoldersCodes)

            'Transpose to objects
            Dim curCodes As New Generic.List(Of FolderCode)
            With dsFoldersCodes.Tables("CodeTable")
                Dim n As Integer = 0
                Dim curNoCodification As Integer = 0
                Dim curNoUnique As Integer = 0
                Dim curNoUser As Integer = 0
                For Each curRow As DataRow In .Rows
                    curNoCodification = curRow("NoCodification")
                    curNoUnique = curRow("NoUnique")
                    If curRow("NoUser") Is DBNull.Value Then
                        curNoUser = 0
                    Else
                        curNoUser = curRow("NoUser")
                    End If

                    'Periods
                    Dim dtCodesPeriodes As DataTable = DBHelper.copyDataTable(dsFoldersCodes, "CodePeriodeTable", "NoCodification=" & curNoCodification, "IsEval,NoPeriode")
                    dtCodesPeriodes.DefaultView.Sort = "IsEval,NoPeriode"
                    dtCodesPeriodes.AcceptChanges()

                    'FolderTexteTypes links
                    Dim dtTexteTypes As DataTable = DBHelper.copyDataTable(dsFoldersCodes, "FTTTable", "NoUnique=" & curNoUnique & " AND NoUser " & If(curNoUser = 0, "IS NULL", "=" & curNoUser), "")

                    'FolderCodeAlerts links
                    Dim dtCodesAlerts As DataTable = DBHelper.copyDataTable(dsFoldersCodes, "FATTable", "NoUnique=" & curNoUnique & " AND NoUser " & If(curNoUser = 0, "IS NULL", "=" & curNoUser), "")

                    Dim linkTables As New Generic.Dictionary(Of String, DataTable)
                    linkTables.Add("periods", dtCodesPeriodes)
                    linkTables.Add("fttlinks", dtTexteTypes)
                    linkTables.Add("fatlinks", dtCodesAlerts)
                    Dim extraData As New Generic.Dictionary(Of String, Object)
                    extraData.Add("index", n)
                    Dim fcData As New DBItemableData(curRow, linkTables, extraData)

                    Dim newFolderCode As New FolderCode(fcData)
                    If noCodification = 0 Then
                        curCodes.Add(newFolderCode)
                    Else
                        MyBase.removeItemable(noCodification)
                        MyBase.addItemable(newFolderCode)
                    End If
                    n += 1
                Next
            End With

            If noCodification = 0 Then
                changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
                MyBase.clear()
                For Each curCode As FolderCode In curCodes
                    MyBase.addItemable(curCode)

                    If Not names_noUnique.ContainsKey(curCode.name.ToUpper) Then names_noUnique.Add(curCode.name.ToUpper, curCode.noUnique)
                    If Not noUnique_names.ContainsKey(curCode.noUnique) Then noUnique_names.Add(curCode.noUnique, curCode.name)
                Next
                changingItemablesLock.ReleaseWriterLock()
            End If
        End Sub

        Public Overrides Sub load()
            _load(0)
        End Sub

        Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
            If dataReceived.function.StartsWith("CodesDossiers") = False OrElse Not dataReceived.fromExternal Then Exit Sub

            If dataReceived.function = "CodesDossiers-Del" Then
                'Remove deleted
                MyBase.removeItemable(Integer.Parse(dataReceived.params(0)))
            Else
                If dataReceived.params.Length >= 1 Then
                    'Load a specific one
                    _load(dataReceived.params(0))
                Else
                    'Load all
                    load()
                End If
            End If
        End Sub

        Public Function setTherapistCodesToDefault(ByVal noTRP As Integer, Optional ByVal onlyNoUnique As Integer = 0) As Boolean
            Dim minCodes As New Generic.Dictionary(Of Integer, FolderCode)
            'Adjust inner manager cache + get all current or minimum future codes of each noUnique
            For Each curCode As FolderCode In getItemables()
                If curCode.noUser = noTRP AndAlso (onlyNoUnique = 0 OrElse onlyNoUnique = curCode.noUnique) Then
                    'Add code to ensure default if 
                    If minCodes.ContainsKey(curCode.noUnique) Then
                        If minCodes(curCode.noUnique).firstEffectiveTime > curCode.firstEffectiveTime _
                        AndAlso (curCode.firstEffectiveTime > Date.Today OrElse curCode.isEffective(Date.Today)) Then
                            minCodes(curCode.noUnique) = curCode
                        End If
                    Else
                        minCodes.Add(curCode.noUnique, curCode)
                    End If

                    'Apply same changes than database
                    If date1Infdate2(curCode.firstEffectiveTime, Date.Today, True) Then
                        curCode.setLastEffectiveTimeToToday()
                    Else
                        MyBase.removeItemable(curCode)
                    End If
                End If
            Next

            'Ensure all codes have a default one
            Dim prevValue As Boolean = autoSaveOnAdd
            autoSaveOnAdd = False
            For Each curCode As FolderCode In minCodes.Values
                Dim defCode As FolderCode = getItemable(curCode.noUnique, 0, curCode.firstEffectiveTime)
                If defCode Is Nothing Then
                    Dim newCode As FolderCode = curCode.clone(0)
                    newCode.saveData(False)
                End If
            Next
            autoSaveOnAdd = prevValue

            'Delete them in database
            If DBLinker.getInstance.delDB("CodificationsDossiers", "NoUser", noTRP & If(onlyNoUnique = 0, "", " AND NoUnique=" & onlyNoUnique) & " AND (LastEffectiveTime IS NULL OR LastEffectiveTime>'" & DateFormat.getTextDate(Date.Today) & "')", False, , False) = False Then Return False
            'Do it twice because when futur ones are deleted, they remove the ending of their previous
            If DBLinker.getInstance.delDB("CodificationsDossiers", "NoUser", noTRP & If(onlyNoUnique = 0, "", " AND NoUnique=" & onlyNoUnique) & " AND (LastEffectiveTime IS NULL OR LastEffectiveTime>'" & DateFormat.getTextDate(Date.Today) & "')", False, , False) = False Then Return False

            'Send update to other clients
            InternalUpdatesManager.getInstance.sendUpdate("CodesDossiers()")

            Return True
        End Function

        Public Overrides Sub removeItemable(ByVal delItem As FolderCode)
            MyBase.removeItemable(delItem)
        End Sub

        Protected Overrides Sub sendUpdate()
            InternalUpdatesManager.getInstance.sendUpdate("CodesDossiers()")
        End Sub
    End Class

End Namespace
