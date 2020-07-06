Namespace Accounts.Clients.Folders.Codifications

    Public Class FolderTextType
        Inherits DBItemableBase
        Implements IComparable(Of FolderTextType), IComparable, ICloneable

        Private Shared lastNewNo As Integer = 1
        Private curData As DataRow
        Private _isDeleted As Boolean = False
        Private _terminatedOnCreation As Boolean = False
        Private Shared dataColumns As Generic.List(Of DataColumn)

        Public Enum WhenToBeCreate
            OnFolderCreation = 0
            OnDayX = 1
            OnPresenceX = 2
            OnFolderClosing = 3
        End Enum

        Public Enum WhenToBeStop
            OnFolderClosing = 0
            OnMaxReached = 1
        End Enum

        Public Enum TypeMultiple
            NbDaysX = 0
            NbPresencesX = 1
            OnTexteEnded = 2
        End Enum


#Region "Properties"
        Public ReadOnly Property isDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
        End Property

        Public Property terminatedOnCreation() As Boolean
            Get
                Return curData("TerminatedOnCreation")
            End Get
            Set(ByVal value As Boolean)
                curData("TerminatedOnCreation") = value
            End Set
        End Property

        Public Property isDefault() As Boolean
            Get
                Return curData("IsDefault")
            End Get
            Set(ByVal value As Boolean)
                curData("IsDefault") = value
            End Set
        End Property

        Public Property resetTextOnCopy() As Boolean
            Get
                Return curData("ResetTextOnCopy")
            End Get
            Set(ByVal value As Boolean)
                curData("ResetTextOnCopy") = value
            End Set
        End Property

        Public Property position() As Integer
            Get
                Return curData("Position")
            End Get
            Set(ByVal value As Integer)
                curData("Position") = value
            End Set
        End Property

        Public Property noModelCategory() As Integer
            Get
                Return curData("NoModeleCategorie")
            End Get
            Set(ByVal value As Integer)
                curData("NoModeleCategorie") = value
            End Set
        End Property

        Public ReadOnly Property noFolderTexteType() As Integer
            Get
                Return curData("NoFolderTexteType")
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

        Public Property copyTextToOtherText() As Integer
            Get
                Return curData("CopyTextToOtherText")
            End Get
            Set(ByVal value As Integer)
                curData("CopyTextToOtherText") = value
            End Set
        End Property

        Public Property isActive() As Boolean
            Get
                Return curData("IsActive")
            End Get
            Set(ByVal value As Boolean)
                curData("IsActive") = value
            End Set
        End Property

        Public Property nbDaysX() As Integer
            Get
                Return curData("NbDaysX")
            End Get
            Set(ByVal value As Integer)
                curData("NbDaysX") = value
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

        Public Property whenToBeCreated() As WhenToBeCreate
            Get
                Return curData("WhenToBeCreated")
            End Get
            Set(ByVal value As WhenToBeCreate)
                curData("WhenToBeCreated") = CInt(value)
            End Set
        End Property

        Public Property multiple() As Boolean
            Get
                Return curData("Multiple")
            End Get
            Set(ByVal value As Boolean)
                curData("Multiple") = value
            End Set
        End Property

        Public Property typeForMultiple() As TypeMultiple
            Get
                Return curData("TypeForMultiple")
            End Get
            Set(ByVal value As TypeMultiple)
                curData("TypeForMultiple") = CInt(value)
            End Set
        End Property

        Public Property nbDaysMultiple() As Integer
            Get
                Return curData("NbDaysMultiple")
            End Get
            Set(ByVal value As Integer)
                curData("NbDaysMultiple") = value
            End Set
        End Property

        Public Property nbPresencesMultiple() As Integer
            Get
                Return curData("NbPresencesMultiple")
            End Get
            Set(ByVal value As Integer)
                curData("NbPresencesMultiple") = value
            End Set
        End Property

        Public Property nbMultipleEnding() As Integer
            Get
                Return curData("NbMultipleEnding")
            End Get
            Set(ByVal value As Integer)
                curData("NbMultipleEnding") = value
            End Set
        End Property

        Public Property whenToBeStopped() As WhenToBeStop
            Get
                Return curData("WhenToBeStopped")
            End Get
            Set(ByVal value As WhenToBeStop)
                curData("WhenToBeStopped") = CInt(value)
            End Set
        End Property

        Public Property isNbDaysDiffBefore() As Boolean
            Get
                Return curData("IsNbDaysDiffBefore")
            End Get
            Set(ByVal value As Boolean)
                curData("IsNbDaysDiffBefore") = value
            End Set
        End Property

        Public Property nbDaysDiff() As Integer
            Get
                Return curData("NbDaysDiff")
            End Get
            Set(ByVal value As Integer)
                curData("NbDaysDiff") = value
            End Set
        End Property

        Public Property showAlert() As Boolean
            Get
                Return curData("ShowAlert")
            End Get
            Set(ByVal value As Boolean)
                curData("ShowAlert") = value
            End Set
        End Property

        Public Property showAlarm() As Boolean
            Get
                Return curData("ShowAlarm")
            End Get
            Set(ByVal value As Boolean)
                curData("ShowAlarm") = value
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

        Public Property startingExternalStatus() As Integer
            Get
                Return curData("StartingExternalStatus")
            End Get
            Set(ByVal value As Integer)
                curData("StartingExternalStatus") = value
            End Set
        End Property

        Public Property modelAppliedOnCreation() As Integer
            Get
                Return curData("ModelAppliedOnCreation")
            End Get
            Set(ByVal value As Integer)
                curData("ModelAppliedOnCreation") = value
            End Set
        End Property

        Public Property alertMessageArticle() As String
            Get
                Return curData("AlertMessageArticle")
            End Get
            Set(ByVal value As String)
                curData("AlertMessageArticle") = value
            End Set
        End Property
#End Region

        Friend Sub New()
            If dataColumns Is Nothing Then
                dataColumns = DBHelper.getTableColumns("FolderTexteTypes")
            Else
                dataColumns = DBHelper.cloneDataColumns(dataColumns)
            End If

            Dim dt As New DataTable()
            dt.Columns.AddRange(dataColumns.ToArray)
            curData = dt.NewRow()

            setDataDefaultValues()
        End Sub

        Public Sub New(ByVal data As DBItemableData)
            loadData(data)
        End Sub

        Private Sub New(ByVal data As DBItemableData, ByVal newNoFTT As Integer)
            loadData(data)
            curData("NoFolderTexteType") = newNoFTT
        End Sub

        Public Function getFolderTextInitialContent() As String
            If modelAppliedOnCreation = 0 Then Return String.Empty

            Dim model() As String = DBLinker.getInstance.readOneDBField("Modeles", "Modele", "WHERE NoModele=" & modelAppliedOnCreation)
            If model IsNot Nothing AndAlso model.Length <> 0 Then
                Return model(0).Replace("\n", vbCrLf)
            End If

            Return ""
        End Function

        ''' <summary>
        ''' Mark the FolderTextType as deleted. True deletion happens upon saveData call.
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub delete()
            Dim textCount As String(,) = FolderTextTypesManager.getInstance.countFolderTexts()
            For i As Integer = 0 To textCount.GetUpperBound(1)
                If textCount(0, i) = Me.noFolderTexteType Then
                    Dim mySelf As New Generic.List(Of IDBItemable)
                    mySelf.Add(Me)
                    If textCount(1, i) <> 0 Then Throw New DBItemableUndeletable(mySelf)
                    Exit For
                End If
            Next

            _isDeleted = True
        End Sub

        Private Sub _delete()
            DBLinker.getInstance.delDB("FolderTexteTypes", "NoFolderTexteType", Me.noFolderTexteType, False)

            If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("FTT-Del(" & Me.noFolderTexteType & ")")
            onDeleted()
        End Sub

        Public Overrides Sub loadData(ByVal data As DBItemableData)
            curData = data.mainData
            setDataDefaultValues()
        End Sub

        Private Sub setDataDefaultValues()
            If curData("NoFolderTexteType") Is DBNull.Value Then
                lastNewNo -= 1
                curData("NoFolderTexteType") = lastNewNo
            End If
            If curData("WhenToBeStopped") Is DBNull.Value Then whenToBeStopped = WhenToBeStop.OnFolderClosing
            If curData("WhenToBeCreated") Is DBNull.Value Then whenToBeCreated = WhenToBeCreate.OnFolderCreation
            If curData("TypeForMultiple") Is DBNull.Value Then typeForMultiple = 0
            If curData("ShowAlert") Is DBNull.Value Then showAlert = False
            If curData("ShowAlarm") Is DBNull.Value Then showAlarm = False
            If curData("NbPresencesX") Is DBNull.Value Then nbPresencesX = 0
            If curData("NbPresencesMultiple") Is DBNull.Value Then nbPresencesMultiple = 0
            If curData("NbMultipleEnding") Is DBNull.Value Then nbMultipleEnding = 0
            If curData("NbDaysX") Is DBNull.Value Then nbDaysX = 0
            If curData("NbDaysMultiple") Is DBNull.Value Then nbDaysMultiple = 0
            If curData("NbDaysDiff") Is DBNull.Value Then nbDaysDiff = 0
            If curData("Multiple") Is DBNull.Value Then multiple = False
            If curData("IsNbDaysDiffBefore") Is DBNull.Value Then isNbDaysDiffBefore = True
            If curData("CopyTextToOtherText") Is DBNull.Value Then copyTextToOtherText = 0
            If curData("AlertNbDaysForExpiry") Is DBNull.Value Then alertNbDaysForExpiry = 0
            If curData("AlertMessageArticle") Is DBNull.Value Then alertMessageArticle = "Le"
            If curData("TexteTitle") Is DBNull.Value Then textTitle = ""
            If curData("IsActive") Is DBNull.Value Then isActive = True
            If curData("IsDefault") Is DBNull.Value Then isDefault = False
            If curData("NoModeleCategorie") Is DBNull.Value Then noModelCategory = 3
            If curData("ResetTextOnCopy") Is DBNull.Value Then resetTextOnCopy = False
            If curData("Position") Is DBNull.Value Then position = 0
            If curData("ModelAppliedOnCreation") Is DBNull.Value Then modelAppliedOnCreation = 0
            If curData("StartingExternalStatus") Is DBNull.Value Then startingExternalStatus = 1
            If curData("TerminatedOnCreation") Is DBNull.Value Then terminatedOnCreation = 0
        End Sub

        Public Overrides Sub saveData()
            If Not _isDeleted Then
                If noFolderTexteType <= 0 Then
                    DBLinker.getInstance.writeDB("FolderTexteTypes", "TexteTitle,NoModeleCategorie,AlertMessageArticle,AlertNbDaysForExpiry, CopyTextToOtherText, IsNbDaysDiffBefore, Multiple, NbDaysDiff, NbDaysMultiple, NbDaysX, NbMultipleEnding,NbPresencesMultiple, NbPresencesX, ShowAlarm, ShowAlert, TypeForMultiple, WhenToBeCreated,WhenToBeStopped,IsActive, IsDefault,ResetTextOnCopy,Position,StartingExternalStatus, ModelAppliedOnCreation, TerminatedOnCreation", "'" & curData("TexteTitle").ToString.Replace("'", "''") & "'," & curData("NoModeleCategorie") & ",'" & curData("AlertMessageArticle").ToString.Replace("'", "''") & "'," & curData("AlertNbDaysForExpiry") & "," & curData("CopyTextToOtherText") & ",'" & curData("IsNbDaysDiffBefore") & "','" & curData("Multiple") & "'," & curData("NbDaysDiff") & "," & curData("NbDaysMultiple") & "," & curData("NbDaysX") & "," & curData("NbMultipleEnding") & "," & curData("NbPresencesMultiple") & "," & curData("NbPresencesX") & ",'" & curData("ShowAlarm") & "','" & curData("ShowAlert") & "'," & curData("TypeForMultiple") & "," & curData("WhenToBeCreated") & "," & curData("WhenToBeStopped") & ",'" & curData("IsActive") & "','" & curData("IsDefault") & "','" & curData("ResetTextOnCopy") & "'," & curData("Position") & "," & If(curData("ModelAppliedOnCreation") = 0, "null", curData("ModelAppliedOnCreation")) & "," & curData("StartingExternalStatus") & ",'" & curData("TerminatedOnCreation") & "'", , , , curData("NoFolderTexteType"))
                Else
                    DBLinker.getInstance.updateDB("FolderTexteTypes", "TexteTitle='" & curData("TexteTitle").ToString.Replace("'", "''") & "',NoModeleCategorie=" & curData("NoModeleCategorie") & ",AlertMessageArticle='" & curData("AlertMessageArticle").ToString.Replace("'", "''") & "',AlertNbDaysForExpiry=" & curData("AlertNbDaysForExpiry") & ", CopyTextToOtherText=" & curData("CopyTextToOtherText") & ", IsNbDaysDiffBefore='" & curData("IsNbDaysDiffBefore") & "', Multiple='" & curData("Multiple") & "', NbDaysDiff=" & curData("NbDaysDiff") & ", NbDaysMultiple=" & curData("NbDaysMultiple") & ", NbDaysX=" & curData("NbDaysX") & ", NbMultipleEnding=" & curData("NbMultipleEnding") & ",NbPresencesMultiple=" & curData("NbPresencesMultiple") & ", NbPresencesX=" & curData("NbPresencesX") & ", ShowAlarm='" & curData("ShowAlarm") & "', ShowAlert='" & curData("ShowAlert") & "', TypeForMultiple=" & curData("TypeForMultiple") & ", WhenToBeCreated=" & curData("WhenToBeCreated") & ",WhenToBeStopped=" & curData("WhenToBeStopped") & ",IsActive='" & curData("IsActive") & "',IsDefault='" & curData("IsDefault") & "',ResetTextOnCopy='" & curData("ResetTextOnCopy") & "',Position=" & curData("Position") & ",ModelAppliedOnCreation=" & If(curData("ModelAppliedOnCreation") = 0, "null", curData("ModelAppliedOnCreation")) & ",StartingExternalStatus=" & curData("StartingExternalStatus") & ",TerminatedOnCreation='" & curData("TerminatedOnCreation") & "'", "NoFolderTexteType", noFolderTexteType, False)
                    onDataChanged()
                End If
                If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("FTT(" & Me.noFolderTexteType & ")")
            Else
                _delete()
            End If
        End Sub

        Public Overrides Function toString() As String
            Return Me.textTitle
        End Function

        Public Function compareTo(ByVal other As FolderTextType) As Integer Implements System.IComparable(Of FolderTextType).CompareTo
            Return Me.position.CompareTo(other.position)
        End Function

        Public Function compareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            If Not TypeOf obj Is FolderTextType Then Return -1

            Return compareTo(CType(obj, FolderTextType))
        End Function

        Public Function countFolderTexts(Optional ByVal noFolder As Integer = 0) As Integer
            Dim nbFT As String() = DBLinker.getInstance.readOneDBField("FolderTextes", "COUNT(*)", "WHERE NoFolderTexteType = (" & Me.noFolderTexteType & ")" & IIf(noFolder = 0, "", " AND NoFolder=" & noFolder))
            Return nbFT(0)
        End Function

        Public Function clone() As Object Implements System.ICloneable.Clone
            Dim newFTT As New FolderTextType(New DBItemableData(Me.curData), 0)

            Return newFTT
        End Function

        Public Overrides ReadOnly Property noItemable() As Integer
            Get
                Return noFolderTexteType
            End Get
        End Property
    End Class

End Namespace
