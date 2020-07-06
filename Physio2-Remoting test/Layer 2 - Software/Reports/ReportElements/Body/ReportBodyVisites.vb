Public Class ReportBodyVisites
    Inherits ReportBodyTable

    Private myNbColumnAfterVisites As Integer = 0
    Private mySQLVisitesStatuts As String = ""
    Private myFolderColName As String = ""
    Private nbDaysInMonth As Byte = 0
    Private oldNbColumns As Integer = 0

    Public Sub New(ByRef rapport As Report)
        MyBase.New(rapport)

        MyBase.isGrouped = False
    End Sub

#Region "Properties"
    Public Property nbColumnAfterVisites() As Integer
        Get
            Return myNbColumnAfterVisites
        End Get
        Set(ByVal value As Integer)
            myNbColumnAfterVisites = value
        End Set
    End Property

    Public Property sqlVisitesStatuts() As String
        Get
            Return mySQLVisitesStatuts
        End Get
        Set(ByVal value As String)
            mySQLVisitesStatuts = value
        End Set
    End Property

    Public Property folderColName() As String
        Get
            Return myFolderColName
        End Get
        Set(ByVal value As String)
            myFolderColName = value
        End Set
    End Property
#End Region

    Protected Overrides Sub populateColumnsName()
        If _ColumnsName Is Nothing OrElse _ColumnsName.Length = 0 Then MyBase.populateColumnsName()

        If nbDaysInMonth > 0 Then
            Dim nbHiddenColumns As Integer = _ColumnsName.GetUpperBound(0) - If(Me.nbColumns = 0, _ColumnsName.GetUpperBound(0), Math.Min(_ColumnsName.GetUpperBound(0), Me.nbColumns - Me.nbGroupColumns - 1))
            ReDim Preserve _ColumnsName(_ColumnsName.GetUpperBound(0) + nbDaysInMonth)

            'Transfer columns name which should be after month grid
            If myNbColumnAfterVisites > 0 Then
                For i As Integer = _ColumnsName.GetUpperBound(0) - myNbColumnAfterVisites - nbDaysInMonth To _ColumnsName.GetUpperBound(0) - nbDaysInMonth
                    _ColumnsName(i + nbDaysInMonth) = _ColumnsName(i)
                Next i
            End If

            Dim curDayNo As Byte = 1
            For i As Integer = _ColumnsName.GetUpperBound(0) - myNbColumnAfterVisites - nbDaysInMonth To _ColumnsName.GetUpperBound(0) - myNbColumnAfterVisites - nbHiddenColumns
                _ColumnsName(i) = addZeros(curDayNo.ToString, 2)
                curDayNo += 1
            Next i
        End If
    End Sub

    Protected Overrides Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
        Dim currentFilter As FilterMonth
        With CType(myReport.reportFilter, FilterComposite)
            currentFilter = .Item(.indexOf("FilterMonth"))
        End With

        If currentFilter Is Nothing Then
            MessageBox.Show("Veuillez ajouter un filtre de mois pour ce type de rapport.Contactez votre responsable logiciel.")
            Exit Sub
        End If

        If currentFilter.currentReturn.month = 0 Or currentFilter.currentReturn.year = 0 Then
            MessageBox.Show("Veuillez configurer le filtre de mois pour qu'il n'accepte pas tous les mois et toutes les années.Contactez votre responsable logiciel.")
            Exit Sub
        End If

        Dim nbColumns As Integer = Date.DaysInMonth(currentFilter.currentReturn.year, currentFilter.currentReturn.month)
        nbDaysInMonth = nbColumns
        If nbColumns < 1 Then Exit Sub

        populateColumnsName()

        Dim wherePartDate As String = ""
        If Not myReport.reportFilter Is Nothing Then
            With CType(myReport.reportFilter, FilterComposite)
                If .indexOf("Month") <> -1 Then
                    If CType(.Item(.indexOf("Month")), FilterMonth).all = False Then
                        Dim tmpFiltering As New FilteringMonth()
                        tmpFiltering.month = CType(.Item(.indexOf("Month")), FilterMonth).currentReturn.month
                        tmpFiltering.year = CType(.Item(.indexOf("Month")), FilterMonth).currentReturn.year
                        CType(.Item(.indexOf("Month")), FilterMonth).subquery = ""
                        CType(.Item(.indexOf("Month")), FilterMonth).filter(tmpFiltering)
                        wherePartDate = CType(.Item(.indexOf("Month")), FilterMonth).currentReturn.whereStr
                    Else
                        wherePartDate = ""
                    End If
                ElseIf .indexOf("FromTo") <> -1 Then
                    If Not CType(.Item(.indexOf("FromTo")), FilterFromTo).currentReturn Is Nothing Then
                        wherePartDate = CType(.Item(.indexOf("FromTo")), FilterFromTo).currentReturn.whereStr
                    Else
                        wherePartDate = ""
                    End If
                End If
            End With
            If wherePartDate <> "" Then wherePartDate = " AND " & wherePartDate
        End If
        mySQLStatement = MyBase.transformSQLStatement(mySQLStatement)
        mySQLVisitesStatuts = mySQLVisitesStatuts.Replace("WHEREPART:DATES", wherePartDate)

        Dim myDataSet As DataSet = DBLinker.getInstance.readDBForGrid(mySQLStatement, False, , "Visites", , False)
        If myDataSet Is Nothing Then Exit Sub
        If myDataSet.Tables("Visites") Is Nothing Then Exit Sub

        Dim myStatutDataSet As DataSet = DBLinker.getInstance.readDBForGrid(mySQLVisitesStatuts, False, , "Statuts", , False)

        Dim i, j As Integer
        Dim visiteTable As New DataTable

        With myDataSet.Tables("Visites")
            If Me.nbColumns = 0 Then Me.nbColumns = .Columns.Count 'Set property if not set to real number of columns
            Dim nbHiddenCols As Integer = .Columns.Count - Me.nbColumns
            If nbHiddenCols < 0 Then nbHiddenCols = 0
            Me.nbColumns += nbColumns 'Add the number of days in the month
            nbColumns += myDataSet.Tables("Visites").Columns.Count 'Add the number of cols of data


            'Table Header Cells
            For i = 0 To .Columns.Count - myNbColumnAfterVisites - nbHiddenCols - 1
                visiteTable.Columns.Add(.Columns(i).ColumnName)
            Next i

            For i = 1 To nbDaysInMonth
                visiteTable.Columns.Add(addZeros(i.ToString, 2))
            Next i

            For i = .Columns.Count - myNbColumnAfterVisites - nbHiddenCols To .Columns.Count - 1
                visiteTable.Columns.Add(.Columns(i).ColumnName)
            Next i

            'Table body
            Dim curRow() As Object
            Dim n As Integer
            ReDim curRow(visiteTable.Columns.Count - 1)

            For i = 0 To .Rows.Count - 1
                n = 0

                'Colonnes avant les visites
                For j = 0 To .Columns.Count - myNbColumnAfterVisites - nbHiddenCols - 1
                    curRow(n) = .Rows(i)(j).ToString
                    n += 1
                Next j

                'Visites
                Dim currentClassVisite As String
                Dim currentVisiteStatus As String
                For j = 1 To nbDaysInMonth
                    currentVisiteStatus = ""
                    currentClassVisite = ""
                    Dim statusRows() As DataRow = myStatutDataSet.Tables("Statuts").Select(myStatutDataSet.Tables("Statuts").Columns(2).ColumnName & "=" & .Rows(i)(myFolderColName).ToString)
                    'MyStatutView.RowFilter = MyStatutDataSet.Tables("Statuts").Columns(0).ColumnName & ">='" & DateFormat.AffTextDate(New Date(CurrentFilter.CurrentReturn.Year, CurrentFilter.CurrentReturn.Month, j)) & " 00:00' AND " & MyStatutDataSet.Tables("Statuts").Columns(0).ColumnName & "<'" & DateFormat.AffTextDate(New Date(CurrentFilter.CurrentReturn.Year, CurrentFilter.CurrentReturn.Month, j).AddDays(1)) & " 00:00' AND " & MyStatutDataSet.Tables("Statuts").Columns(2).ColumnName & "=" & .Rows(i)(MyFolderColName).ToString
                    For Each currentRow As DataRow In statusRows
                        'MyStatutDataSet.Tables("Statuts").Columns(0).ColumnName & ">='" & DateFormat.AffTextDate(New Date(CurrentFilter.CurrentReturn.Year, CurrentFilter.CurrentReturn.Month, j)) & " 00:00' AND " & MyStatutDataSet.Tables("Statuts").Columns(0).ColumnName & "<'" & DateFormat.AffTextDate(New Date(CurrentFilter.CurrentReturn.Year, CurrentFilter.CurrentReturn.Month, j).AddDays(1)) & " 00:00' AND " &
                        If CDate(currentRow(0)).Date = New Date(currentFilter.currentReturn.year, currentFilter.currentReturn.month, j) Then
                            If statusRows Is Nothing OrElse statusRows.Length = 0 Then
                                currentClassVisite = ""
                            Else
                                Select Case currentRow(1)
                                    Case 1
                                        currentClassVisite = " PlageAbsenceNonMotivee"
                                        currentVisiteStatus = "A"
                                    Case 2
                                        currentClassVisite = " PlageAbsenceMotivee"
                                        currentVisiteStatus = "A"
                                    Case 3
                                        currentClassVisite = " PlageRV"
                                        currentVisiteStatus = "R"
                                    Case 4
                                        currentClassVisite = " PlagePresence"
                                        currentVisiteStatus = "P"
                                End Select
                            End If

                            Exit For
                        End If
                    Next currentRow

                    curRow(n) = "CELLCSSCLASS:BodyCell VisiteCell" & currentClassVisite & ":" & currentVisiteStatus
                    n += 1
                Next j

                'Colonnes après les visites
                For j = .Columns.Count - myNbColumnAfterVisites - nbHiddenCols To .Columns.Count - 1
                    curRow(n) = .Rows(i)(j).ToString
                    n += 1
                Next j

                visiteTable.Rows.Add(curRow)
            Next i
        End With

        visiteTable.TableName = "Body"
        Dim realDataSet As New DataSet
        realDataSet.Tables.Add(visiteTable)
        generateHTMLTable(htmlBuilder, realDataSet)
    End Sub

    Protected Overrides Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
        MyBase.generateHTMLProperties(htmlBuilder)
        htmlBuilder.Append("<!-- SQLVisitesStatuts=").Append(mySQLVisitesStatuts).AppendLine(" -->")
        htmlBuilder.Append("<!-- NbColumnAfterVisites=").Append(myNbColumnAfterVisites).AppendLine(" -->")
        htmlBuilder.Append("<!-- FolderColName=").Append(myFolderColName).AppendLine(" -->")
    End Sub

    Public Overrides Sub loadproperties(ByVal properties As System.Collections.Hashtable)
        Dim loadHoraires As Boolean = False
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "NbColumnAfterVisites"
                    myNbColumnAfterVisites = myKey.Value
                Case "SQLVisitesStatuts"
                    mySQLVisitesStatuts = myKey.Value
                Case "FolderColName"
                    myFolderColName = myKey.Value
                Case "NbColumns"
                    oldNbColumns = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadproperties(properties)
    End Sub

    Public Overrides Sub saveproperties(ByRef properties As System.Collections.Hashtable)
        properties.Add("NbColumnAfterVisites", myNbColumnAfterVisites)
        properties.Add("SQLVisitesStatuts", mySQLVisitesStatuts)
        properties.Add("FolderColName", myFolderColName)

        MyBase.saveproperties(properties)
    End Sub

    Public Overloads Shared Function findBody(ByVal classElementName As String, ByRef leRapport As Report, Optional ByVal firstDone As Boolean = False, Optional ByVal firstClass As String = "") As ReportBody
        Dim myRapportElement As New ReportBodyVisites(leRapport)
        If myRapportElement.GetType.Name.ToLower = classElementName.ToLower Then Return myRapportElement

        If firstDone = True And firstClass = myRapportElement.GetType.Name Then Return Nothing
        If firstClass = "" Then firstClass = myRapportElement.GetType.Name

        'Connect to other bodys
        Return ReportBasicBody.findBody(classElementName, leRapport, True, firstClass)
    End Function

End Class
