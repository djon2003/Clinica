Namespace Accounts.Clients.Folders.Codifications

    Public Class FolderAlertType
        Inherits DBItemableBase
        Implements IComparable(Of FolderAlertType), IComparable, ICloneable

        Private Shared lastNewNo As Integer = 1
        Private curData As DataRow
        Private _isDeleted As Boolean = False
        Private Shared dataColumns As Generic.List(Of DataColumn)

        Public Enum StartingDateTypes
            OnFolderCreation = 0
            OnDateAccident = 1
            OnDateRechute = 2
            OnDateReferencce = 3
            OnPresenceX = 4
        End Enum

#Region "Properties"
        Public ReadOnly Property isDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
        End Property

        Public ReadOnly Property noFolderAlertType() As Integer
            Get
                Return curData("NoFolderAlertType")
            End Get
        End Property

        Public Property alertTitle() As String
            Get
                Return curData("AlertTitle")
            End Get
            Set(ByVal value As String)
                curData("AlertTitle") = value
            End Set
        End Property

        Public Property nbPresencesX() As Integer
            Get
                Return curData("NbPresencesX")
            End Get
            Set(ByVal value As Integer)
                curData("NbPresencesX") = value
            End Set
        End Property

        Public Property startingDate_NbPresencesX() As Integer
            Get
                Return curData("DateDebutNbPresencesX")
            End Get
            Set(ByVal value As Integer)
                curData("DateDebutNbPresencesX") = value
            End Set
        End Property

        Public Property startingDate_NbDaysX() As Integer
            Get
                Return curData("DateDebutNbDaysX")
            End Get
            Set(ByVal value As Integer)
                curData("DateDebutNbDaysX") = value
            End Set
        End Property

        Public Property startingDate_Type() As StartingDateTypes
            Get
                Return curData("TypeDateDebut")
            End Get
            Set(ByVal value As StartingDateTypes)
                curData("TypeDateDebut") = CInt(value)
            End Set
        End Property

        Public Property alertNbDaysDiff() As Integer
            Get
                Return curData("AlertNbDaysDiff")
            End Get
            Set(ByVal value As Integer)
                curData("AlertNbDaysDiff") = value
            End Set
        End Property

        Public Property alertNbPresencesDiff() As Integer
            Get
                Return curData("AlertNbPresencesDiff")
            End Get
            Set(ByVal value As Integer)
                curData("AlertNbPresencesDiff") = value
            End Set
        End Property

        Public Property alertNbDaysForExpiry() As Integer
            Get
                Return curData("AlertNbDaysForExpiry")
            End Get
            Set(ByVal value As Integer)
                curData("AlertNbDaysForExpiry") = value
            End Set
        End Property

        Public Property alertMessage() As String
            Get
                Return curData("AlertMessage")
            End Get
            Set(ByVal value As String)
                curData("AlertMessage") = value
            End Set
        End Property
#End Region

        Friend Sub New()
            If dataColumns Is Nothing Then
                dataColumns = DBHelper.getTableColumns("FolderAlertTypes")
            Else
                dataColumns = DBHelper.cloneDataColumns(dataColumns)
            End If

            Dim dt As New DataTable()
            dt.Columns.AddRange(dataColumns.ToArray)
            curData = dt.NewRow()

            setDefaultValues()
        End Sub

        Public Sub New(ByVal data As DBItemableData)
            loadData(data)
        End Sub

        Private Sub New(ByVal data As DBItemableData, ByVal newNoFCA As Integer)
            loadData(data)
            curData("NoFolderAlertType") = newNoFCA
        End Sub

        Public Overrides Sub loadData(ByVal data As DBItemableData)
            curData = data.mainData
            setDefaultValues()
        End Sub

        Private Sub setDefaultValues()
            If curData("NoFolderAlertType") Is DBNull.Value Then
                lastNewNo -= 1
                curData("NoFolderAlertType") = lastNewNo
            End If
            If curData("TypeDateDebut") Is DBNull.Value Then startingDate_Type = StartingDateTypes.OnFolderCreation
            If curData("NbPresencesX") Is DBNull.Value Then nbPresencesX = 0
            If curData("DateDebutNbDaysX") Is DBNull.Value Then startingDate_NbDaysX = 0
            If curData("DateDebutNbPresencesX") Is DBNull.Value Then startingDate_NbPresencesX = 0
            If curData("AlertNbDaysDiff") Is DBNull.Value Then alertNbDaysDiff = 0
            If curData("AlertNbPresencesDiff") Is DBNull.Value Then alertNbPresencesDiff = 0
            If curData("AlertNbDaysForExpiry") Is DBNull.Value Then alertNbDaysForExpiry = 0
            If curData("AlertMessage") Is DBNull.Value Then alertMessage = ""
            If curData("AlertTitle") Is DBNull.Value Then alertTitle = ""
        End Sub

        ''' <summary>
        ''' Mark the FolderAlertType as deleted. True deletion happens upon saveData call.
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub delete()
            Dim alertCount As String(,) = FolderAlertTypesManager.getInstance.countFolderAlerts
            For i As Integer = 0 To alertCount.GetUpperBound(1)
                If alertCount(0, i) = Me.noFolderAlertType Then
                    Dim mySelf As New Generic.List(Of IDBItemable)
                    mySelf.Add(Me)
                    If alertCount(1, i) <> 0 Then Throw New DBItemableUndeletable(mySelf)
                    Exit For
                End If
            Next

            _isDeleted = True
        End Sub

        Private Sub _delete()
            DBLinker.getInstance.delDB("FolderAlertTypes", "NoFolderAlertType", Me.noFolderAlertType, False)

            If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("FAT-Del(" & Me.noFolderAlertType & ")")
            onDeleted()
        End Sub


        Public Overrides Sub saveData()
            If Not _isDeleted Then

                If noFolderAlertType <= 0 Then
                    DBLinker.getInstance.writeDB("CodesDossiersAlerts", "AlertTitle,NoCodification,AlertMessage,AlertNbDaysForExpiry, AlertNbDaysDiff, NbPresencesX, AlertNbPresencesDiff,DateDebutNbDaysX,DateDebutNbPresencesX,TypeDateDebut", "'" & curData("AlertTitle").ToString.Replace("'", "''") & "'," & curData("NoCodification") & ",'" & curData("AlertMessage").ToString.Replace("'", "''") & "'," & curData("AlertNbDaysForExpiry") & "," & curData("AlertNbDaysDiff") & "," & curData("NbPresencesX") & "," & curData("AlertNbPresencesDiff") & "," & curData("DateDebutNbDaysX") & "," & curData("DateDebutNbPresencesX") & "," & curData("TypeDateDebut"))
                Else
                    DBLinker.getInstance.updateDB("CodesDossiersAlerts", "AlertTitle='" & curData("AlertTitle").ToString.Replace("'", "''") & "',NoCodification=" & curData("NoCodification") & ",AlertMessage='" & curData("AlertMessage").ToString.Replace("'", "''") & "',AlertNbDaysForExpiry=" & curData("AlertNbDaysForExpiry") & ",AlertNbDaysDiff=" & curData("AlertNbDaysDiff") & ", NbPresencesX=" & curData("NbPresencesX") & ", AlertNbPresencesDiff=" & curData("AlertNbPresencesDiff") & ",DateDebutNbDaysX=" & curData("DateDebutNbDaysX") & ",DateDebutNbPresencesX=" & curData("DateDebutNbPresencesX") & ",TypeDateDebut=" & curData("TypeDateDebut"), "NoFolderAlertType", noFolderAlertType, False)
                    onDataChanged()
                End If
                If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("FTT(" & Me.noFolderAlertType & ")")
            Else
                _delete()
            End If
        End Sub

        Public Overrides Function toString() As String
            Return Me.alertTitle
        End Function

        Public Function compareTo(ByVal other As FolderAlertType) As Integer Implements System.IComparable(Of FolderAlertType).CompareTo
            Return Me.alertTitle.CompareTo(other.alertTitle)
        End Function

        Public Function compareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            If Not TypeOf obj Is FolderAlertType Then Return -1

            Return compareTo(CType(obj, FolderAlertType))
        End Function

        Public Function clone() As Object Implements System.ICloneable.Clone
            Dim newFCA As New FolderAlertType(New DBItemableData(Me.curData), 0)

            Return newFCA
        End Function

        Public Function isAlertDone(ByVal noFolder As Integer) As Boolean
            Dim isDid() As String = DBLinker.getInstance.readOneDBField("FolderAlerts", "IsAlertDone", "WHERE NoFolderAlertType=" & noFolderAlertType & " AND NoFolder=" & noFolder)
            If isDid Is Nothing OrElse isDid.Length = 0 Then Return True

            Return isDid(0)
        End Function

        Public Overrides ReadOnly Property noItemable() As Integer
            Get
                Return Me.noFolderAlertType
            End Get
        End Property
    End Class

End Namespace
