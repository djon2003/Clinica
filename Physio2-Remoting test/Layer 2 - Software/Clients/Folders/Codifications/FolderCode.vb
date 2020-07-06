Namespace Accounts.Clients.Folders.Codifications

    Public Class FolderCode
        Inherits DBItemableBase
        Implements IDisposable, ICloneable, IComparable(Of FolderCode)


#Region "Definitions"
        Private oldCode As FolderCode
        Private _parentClone As FolderCode

        Private _NoCodification As Integer
        Private _noUnique As Integer
        Private _NoUser As Integer = 0
        Private _firstEffectiveTime As Date = LIMIT_DATE
        Private _lastEffectiveTime As Date = LIMIT_DATE

        Private _Index As Integer = 0
        Private _Periods As System.Data.DataTable
        Private _name As String
        Private _askReceipt As Boolean
        Private _autoSelectBillWhenPaying As Boolean
        Private _confirmReference As Boolean
        Private _confirmDiagnostic As Boolean
        Private _accidentDate As Boolean
        Private _relaspeDate As Boolean
        Private _autoShowPayment As Boolean
        Private _askPourcentage As Boolean
        Private _Confirmation As Byte
        Private _evaluationPonderation As Double
        Private _treatmentPonderation As Double
        Private _DefaultPaymentMethod As String
        Private _authorizationProcessActivated As Boolean
        Private _NotConfirmRVOnPasteOfDTRP As Boolean
        Private _startingExternalStatus As Integer

        Private Shared fttLinks As New Generic.Dictionary(Of Integer, Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
        Private Shared fatLinks As New Generic.Dictionary(Of Integer, Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
#End Region

#Region "Constructors"
        Friend Sub New(ByVal data As DBItemableData)
            Me.loadData(data)
        End Sub

        Public Sub New()
            Me.firstEffectiveTime = Date.Today.AddDays(1)
            Me.oldCode = New FolderCode(True)
        End Sub

        Private Sub New(ByVal dontInitOldCode As Boolean)

        End Sub
#End Region

#Region "Properties"
        Public Property startingExternalStatus() As Integer
            Get
                Return _startingExternalStatus
            End Get
            Set(ByVal value As Integer)
                _startingExternalStatus = value
            End Set
        End Property

        Public ReadOnly Property noUnique() As Integer
            Get
                Return _noUnique
            End Get
        End Property

        Public Property firstEffectiveTime() As Date
            Get
                Return _firstEffectiveTime
            End Get
            Set(ByVal value As Date)
                If value.Date = _firstEffectiveTime.Date Then Exit Property

                If date1Infdate2(value, Date.Today, True) Then
                    Throw New DBItemableInvalidPropertyValue("New date have to be in the future")
                End If

                _firstEffectiveTime = value
            End Set
        End Property

        Public ReadOnly Property lastEffectiveTime() As Date
            Get
                Return _lastEffectiveTime
            End Get
        End Property

        Public ReadOnly Property folderTexteTypesCount() As Integer
            Get
                Return fttLinks(_noUnique)(_NoUser).Count
            End Get
        End Property

        Public ReadOnly Property folderAlertTypesCount() As Integer
            Get
                Return fatLinks(_noUnique)(_NoUser).Count
            End Get
        End Property

        Private Function getTypes(Of Managed)(ByVal manager As IDBItemableManager, ByRef list As Generic.List(Of Integer)) As Generic.List(Of Managed)
            Dim types As New Generic.List(Of Managed)

            For Each curType As IItemable In manager.getItemables()
                If list.Contains(curType.noItemable) Then types.Add(curType)
            Next

            Return types
        End Function

        Private Sub setTypes(Of Managed)(ByRef list As Generic.List(Of Integer), ByVal values As Generic.List(Of Managed))
            list = New Generic.List(Of Integer)

            For Each curType As IItemable In values
                list.Add(curType.noItemable)
            Next
        End Sub


        Public Property folderTexteTypes() As Generic.List(Of FolderTextType)
            Get
                Return getTypes(Of FolderTextType)(FolderTextTypesManager.getInstance, fttLinks(_noUnique)(_NoUser))
            End Get
            Set(ByVal value As Generic.List(Of FolderTextType))
                If fttLinks.ContainsKey(_noUnique) = False Then fttLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
                If fttLinks(_noUnique).ContainsKey(_NoUser) = False Then fttLinks(_noUnique).Add(_NoUser, Nothing)
                fttLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)

                If value Is Nothing Then value = New Generic.List(Of FolderTextType)

                setTypes(fttLinks(_noUnique)(_NoUser), value)
            End Set
        End Property

        Public Property folderTexteTypesAsIItemable() As Generic.List(Of IItemable)
            Get
                Return getTypes(Of IItemable)(FolderTextTypesManager.getInstance, fttLinks(_noUnique)(_NoUser))
            End Get
            Set(ByVal value As Generic.List(Of IItemable))
                If fttLinks.ContainsKey(_noUnique) = False Then fttLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
                If fttLinks(_noUnique).ContainsKey(_NoUser) = False Then fttLinks(_noUnique).Add(_NoUser, Nothing)
                fttLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)

                If value Is Nothing Then value = New Generic.List(Of IItemable)

                setTypes(fttLinks(_noUnique)(_NoUser), value)
            End Set
        End Property

        Public Property folderAlertTypes() As Generic.List(Of FolderAlertType)
            Get
                Return getTypes(Of FolderAlertType)(FolderAlertTypesManager.getInstance, fatLinks(_noUnique)(_NoUser))
            End Get
            Set(ByVal value As Generic.List(Of FolderAlertType))
                If fatLinks.ContainsKey(_noUnique) = False Then fatLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
                If fatLinks(_noUnique).ContainsKey(_NoUser) = False Then fatLinks(_noUnique).Add(_NoUser, Nothing)
                fatLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)

                If value Is Nothing Then value = New Generic.List(Of FolderAlertType)

                setTypes(fatLinks(_noUnique)(_NoUser), value)
            End Set
        End Property

        Public Property folderAlertTypesAsIItemable() As Generic.List(Of IItemable)
            Get
                Return getTypes(Of IItemable)(FolderAlertTypesManager.getInstance, fatLinks(_noUnique)(_NoUser))
            End Get
            Set(ByVal value As Generic.List(Of IItemable))
                If fatLinks.ContainsKey(_noUnique) = False Then fatLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
                If fatLinks(_noUnique).ContainsKey(_NoUser) = False Then fatLinks(_noUnique).Add(_NoUser, Nothing)
                fatLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)

                If value Is Nothing Then value = New Generic.List(Of IItemable)

                setTypes(fatLinks(_noUnique)(_NoUser), value)
            End Set
        End Property

        Public Property periods() As DataTable
            Get
                Return _Periods
            End Get
            Set(ByVal value As DataTable)
                If oldCode IsNot Nothing AndAlso oldCode.periods Is Nothing Then oldCode.periods = value

                _Periods = value
            End Set
        End Property

        Public ReadOnly Property noCodification() As Integer
            Get
                Return _NoCodification
            End Get
        End Property
        Public ReadOnly Property index() As Integer
            Get
                Return _Index
            End Get
        End Property
        Public ReadOnly Property noUser() As Integer
            Get
                Return _NoUser
            End Get
        End Property

        Public Property confirmation() As Byte
            Get
                Return _Confirmation
            End Get
            Set(ByVal value As Byte)
                _Confirmation = value
            End Set
        End Property

        Public Property evaluationPonderation() As Double
            Get
                Return _evaluationPonderation
            End Get
            Set(ByVal value As Double)
                _evaluationPonderation = value
            End Set
        End Property

        Public Property treatmentPonderation() As Double
            Get
                Return _treatmentPonderation
            End Get
            Set(ByVal value As Double)
                _treatmentPonderation = value
            End Set
        End Property

        Public Property name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property defaultPaymentMethod() As String
            Get
                Return _DefaultPaymentMethod
            End Get
            Set(ByVal value As String)
                _DefaultPaymentMethod = value
            End Set
        End Property

        Public Property notConfirmRVOnPasteOfDTRP() As Boolean
            Get
                Return _NotConfirmRVOnPasteOfDTRP
            End Get
            Set(ByVal value As Boolean)
                _NotConfirmRVOnPasteOfDTRP = value
            End Set
        End Property

        Public Property askPourcentage() As Boolean
            Get
                Return _askPourcentage
            End Get
            Set(ByVal value As Boolean)
                _askPourcentage = value
            End Set
        End Property

        Public Property authorizationProcessActivated() As Boolean
            Get
                Return _authorizationProcessActivated
            End Get
            Set(ByVal value As Boolean)
                _authorizationProcessActivated = value
            End Set
        End Property

        Public Property askReceipt() As Boolean
            Get
                Return _askReceipt
            End Get
            Set(ByVal value As Boolean)
                _askReceipt = value
            End Set
        End Property

        Public Property autoSelectBillWhenPaying() As Boolean
            Get
                Return _autoSelectBillWhenPaying
            End Get
            Set(ByVal value As Boolean)
                _autoSelectBillWhenPaying = value
                '_lastEffectiveTime = LIMIT_DATE
            End Set
        End Property

        Public Property accidentDate() As Boolean
            Get
                Return _accidentDate
            End Get
            Set(ByVal value As Boolean)
                _accidentDate = value
            End Set
        End Property

        Public Property relaspeDate() As Boolean
            Get
                Return _relaspeDate
            End Get
            Set(ByVal value As Boolean)
                _relaspeDate = value
            End Set
        End Property

        Public Property autoShowPayment() As Boolean
            Get
                Return _autoShowPayment
            End Get
            Set(ByVal value As Boolean)
                _autoShowPayment = value
            End Set
        End Property

        Public Property confirmReference() As Boolean
            Get
                Return _confirmReference
            End Get
            Set(ByVal value As Boolean)
                _confirmReference = value
            End Set
        End Property

        Public Property confirmDiagnostic() As Boolean
            Get
                Return _confirmDiagnostic
            End Get
            Set(ByVal value As Boolean)
                _confirmDiagnostic = value
            End Set
        End Property
#End Region

        Public Sub copyTo(ByVal noTRP As Integer)
            Dim existingCode As FolderCode = FolderCodesManager.getInstance.getItemable(Me.noUnique, noTRP, Me.firstEffectiveTime)
            Dim newCode As FolderCode = Me.clone(noTRP)
            If newCode.firstEffectiveTime <= Date.Today Then newCode.firstEffectiveTime = Date.Today.AddDays(1)

            If existingCode Is Nothing OrElse existingCode.noUser <> noTRP OrElse newCode.firstEffectiveTime <> existingCode.firstEffectiveTime Then
                Try
                    newCode.saveData(False)
                Catch ex As DBItemableUnsaveble
                    Throw New DBItemableUncopiable()
                End Try
            Else
                Throw New DBItemableUncopiable()
            End If
        End Sub

        ''' <summary>
        ''' Look if the FolderCode is effective ensuring that the applicationDate is between firstEffectiveTime and lastEffectiveTime
        ''' </summary>
        ''' <param name="applicationDate">date to verify if the FolderCode is effective</param>
        ''' <returns>true if the FolderCode is effective on the applicationDate</returns>
        ''' <remarks></remarks>
        Public Function isEffective(ByVal applicationDate As Date) As Boolean
            Return date1Infdate2(_firstEffectiveTime, applicationDate, True) AndAlso (_lastEffectiveTime.Equals(LIMIT_DATE) OrElse date1Infdate2(applicationDate, _lastEffectiveTime, True))
        End Function

        Public Function isTherapistDifferent() As Boolean
            Return oldCode Is Nothing OrElse oldCode.noUser <> Me.noUser
        End Function

        Private Function getInfoFromPeriods(ByVal period As Integer, ByVal isEval As Boolean, ByVal columnName As String) As Double
            Dim noPeriod As Integer = period / 15
            Dim defaultInfo As Double = -1
            Dim myInfo As Double = -1
            For i As Integer = 0 To _Periods.Rows.Count - 1
                If _Periods.Rows(i)("IsEval") = isEval Then
                    If _Periods.Rows(i)("NoPeriode") = noPeriod Then myInfo = IIf(_Periods.Rows(i)(columnName) Is DBNull.Value, 0, _Periods.Rows(i)(columnName))
                    If _Periods.Rows(i)("IsDefault") Then defaultInfo = IIf(_Periods.Rows(i)(columnName) Is DBNull.Value, 0, _Periods.Rows(i)(columnName))
                End If
            Next i

            If myInfo = -1 Then myInfo = defaultInfo

            Return myInfo
        End Function

        Public Function getEvaluationAbsence(ByVal period As Integer) As Double
            Return getAbsence(period, True)
        End Function

        Public Function getTreatmentAbsence(ByVal periode As Integer) As Double
            Return getAbsence(periode, False)
        End Function

        Public Function getAbsence(ByVal period As Integer, ByVal isEval As Boolean) As Double
            Return getInfoFromPeriods(period, isEval, "PourcentAbsence") * 100
        End Function

        Public Function getEvaluationPourcent(ByVal period As Integer) As Double
            Return getPourcentage(period, True)
        End Function

        Public Function getTreatmentPourcent(ByVal period As Integer) As Double
            Return getPourcentage(period, False)
        End Function

        Public Function getPourcentage(ByVal period As Integer, ByVal isEval As Boolean) As Double
            Return getInfoFromPeriods(period, isEval, "PourcentClient") * 100
        End Function

        Public Function getEvaluationNoKP(ByVal period As Integer) As Double
            Return getNoKP(period, True)
        End Function

        Public Function getTreatmentNoKP(ByVal period As Integer) As Double
            Return getNoKP(period, False)
        End Function

        Public Function getNoKP(ByVal period As Integer, ByVal isEval As Boolean) As Double
            Return getInfoFromPeriods(period, isEval, "NoKP")
        End Function

        Public Function getEvaluationPrice(ByVal period As Integer) As Double
            Return getPrice(period, True)
        End Function

        Public Function getTreatmentPrice(ByVal period As Integer) As Double
            Return getPrice(period, False)
        End Function

        Public Function getPrice(ByVal period As Integer, ByVal isEval As Boolean) As Double
            Return getInfoFromPeriods(period, isEval, "Montant")
        End Function

        Public Function getDefaultPeriod(ByVal isEval As Boolean) As Integer
            For i As Integer = 0 To _Periods.Rows.Count - 1
                If _Periods.Rows(i)("IsEval") = isEval And _Periods.Rows(i)("IsDefault") Then
                    Return _Periods.Rows(i)("NoPeriode") * 15
                End If
            Next i
        End Function

        Public Overrides Function toString() As String
            Return Me.name
        End Function

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' free managed resources when explicitly called
                    _Periods.Dispose()
                End If

                ' free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        Public Function getPreviousUnique() As FolderCode
            Return FolderCodesManager.getInstance.getItemable(noUnique, noUser, oldCode.firstEffectiveTime.AddDays(-1))
        End Function

        Private Function _clone(ByVal noTRP As Integer, ByVal keepNoCode As Boolean, Optional ByVal cloneOldCode As Boolean = True) As FolderCode
            If noTRP < 0 Then noTRP = Me.noUser

            Dim newFolderCode As New FolderCode()

            newFolderCode._parentClone = Me
            newFolderCode._askPourcentage = Me.askPourcentage
            newFolderCode._autoShowPayment = Me.autoShowPayment
            newFolderCode._Confirmation = Me.confirmation
            newFolderCode._accidentDate = Me.accidentDate
            newFolderCode._relaspeDate = Me.relaspeDate
            newFolderCode._DefaultPaymentMethod = Me.defaultPaymentMethod
            newFolderCode._authorizationProcessActivated = Me.authorizationProcessActivated
            newFolderCode._evaluationPonderation = Me.evaluationPonderation
            newFolderCode._firstEffectiveTime = Me.firstEffectiveTime
            newFolderCode._Index = 0
            newFolderCode._lastEffectiveTime = Me.lastEffectiveTime
            newFolderCode._confirmDiagnostic = Me.confirmDiagnostic
            newFolderCode._confirmReference = Me.confirmReference
            newFolderCode._name = Me.name
            If keepNoCode Then newFolderCode._NoCodification = Me.noCodification
            newFolderCode._NotConfirmRVOnPasteOfDTRP = Me.notConfirmRVOnPasteOfDTRP
            newFolderCode._noUnique = Me.noUnique
            newFolderCode._NoUser = noTRP
            newFolderCode._autoSelectBillWhenPaying = Me.autoSelectBillWhenPaying
            newFolderCode._Periods = DBHelper.copyDataTable(Me.periods, "", "")
            newFolderCode._askReceipt = Me.askReceipt
            newFolderCode._treatmentPonderation = Me.treatmentPonderation
            newFolderCode._startingExternalStatus = Me.startingExternalStatus
            If Me.oldCode IsNot Nothing Then
                newFolderCode.oldCode = Me.oldCode._clone(oldCode.noUser, True, False)
            End If

            Return newFolderCode
        End Function

        Public Function clone(ByVal noTRP As Integer) As FolderCode
            Return _clone(noTRP, False)
        End Function

        Public Function clone() As Object Implements System.ICloneable.Clone
            Return clone(Me.noUser)
        End Function

        Public Sub setLastEffectiveTimeToToday()
            _lastEffectiveTime = Date.Today
        End Sub

        Public Overrides Sub delete()
            DBLinker.getInstance.delDB("CodificationsDossiers", "NoCodification", noCodification, False)

            Dim isCodeInFutur As Boolean = date1Infdate2(Date.Today, Me.firstEffectiveTime)
            If isCodeInFutur Then
                onDeleted()
            Else
                Me._lastEffectiveTime = Date.Today
            End If

            Dim prevCode As FolderCode = FolderCodesManager.getInstance.getItemable(noUnique, noUser, oldCode.firstEffectiveTime.AddDays(-1))
            If isCodeInFutur AndAlso prevCode IsNot Nothing Then
                prevCode._lastEffectiveTime = lastEffectiveTime
                prevCode.oldCode._lastEffectiveTime = prevCode._lastEffectiveTime
            End If

            If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("CodesDossiers-Del(" & Me.noCodification & ")")
        End Sub

        Public Overrides Sub loadData(ByVal data As DBItemableData)
            Dim curNoUser As Integer = 0

            _Index = data.extraData("index")

            'Load main data
            With data.mainData
                If (.Item("NoUser") IsNot DBNull.Value AndAlso .Item("NoUser").ToString <> "0") Then curNoUser = .Item("NoUser")

                _name = .Item("Nom")
                _NoUser = curNoUser
                _Periods = data.linkTables("periods")
                _askReceipt = .Item("Recu")
                _autoSelectBillWhenPaying = .Item("Paiement")
                _confirmReference = .Item("MsgNoRef")
                _confirmDiagnostic = .Item("MsgDiagnostic")
                _accidentDate = .Item("DateAccidentActif")
                _relaspeDate = .Item("DateRechuteActif")
                _autoShowPayment = .Item("AutoOpenPaiement")
                _NoCodification = .Item("NoCodification")
                _askPourcentage = .Item("AffPourcentAllTimes")
                _Confirmation = .Item("Confirmation")
                _evaluationPonderation = .Item("PonderationEval")
                _treatmentPonderation = .Item("PonderationPresence")
                _DefaultPaymentMethod = .Item("MethodePaiementDefaut")
                _authorizationProcessActivated = .Item("DemandeAuthorisation")
                _NotConfirmRVOnPasteOfDTRP = .Item("NotConfirmRVOnPasteOfDTRP")
                _noUnique = .Item("NoUnique")
                _firstEffectiveTime = .Item("FirstEffectiveTime")
                If .Item("StartingExternalStatus") IsNot DBNull.Value Then _startingExternalStatus = .Item("StartingExternalStatus")
                If .Item("LastEffectiveTime") IsNot DBNull.Value Then _lastEffectiveTime = .Item("LastEffectiveTime")
            End With

            'Transpose FTT rows into fttLinks list
            If FolderCode.fttLinks.ContainsKey(_noUnique) = False Then FolderCode.fttLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
            If FolderCode.fttLinks(_noUnique).ContainsKey(_NoUser) = False Then FolderCode.fttLinks(_noUnique).Add(_NoUser, Nothing)
            FolderCode.fttLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)
            Dim fttLinks As Generic.List(Of Integer) = FolderCode.fttLinks(_noUnique)(_NoUser)
            With data.linkTables("fttlinks")
                For Each curRow As DataRow In .Rows
                    fttLinks.Add(curRow("NoFolderTexteType"))
                Next
            End With

            'Transpose FAT rows into fatLinks list
            If FolderCode.fatLinks.ContainsKey(_noUnique) = False Then FolderCode.fatLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
            If FolderCode.fatLinks(_noUnique).ContainsKey(_NoUser) = False Then FolderCode.fatLinks(_noUnique).Add(_NoUser, Nothing)
            FolderCode.fatLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)
            Dim fatLinks As Generic.List(Of Integer) = FolderCode.fatLinks(_noUnique)(_NoUser)
            With data.linkTables("fatlinks")
                For Each curRow As DataRow In .Rows
                    fatLinks.Add(curRow("NoFolderAlertType"))
                Next
            End With

            oldCode = Me._clone(Me.noUser, True)
        End Sub

        Private Sub writeData()
            Dim isCodeNew As Boolean = Me.noCodification = 0
            Dim changeOldCode As Boolean = False

            _NoCodification = 0
            _noUnique = FolderCodesManager.getInstance.getNoUniqueByCodeName(Me.name)
            If _noUnique <> 0 Then _name = FolderCodesManager.getInstance.getCodeNameByNoUnique(_noUnique) 'Ensure small cast

            'Update overlapping codes
            If oldCode IsNot Nothing AndAlso oldCode.noUser = Me.noUser AndAlso Not isCodeNew Then
                Me._lastEffectiveTime = oldCode.lastEffectiveTime
                changeOldCode = True 'Differed to be able to restore the current lastEffectiveTime value when db crash
            Else
                Dim prevCode As FolderCode = FolderCodesManager.getInstance.getItemable(Me.noUnique, Me.noUser, Me.firstEffectiveTime)
                If prevCode IsNot Nothing AndAlso prevCode.noUser = Me.noUser AndAlso prevCode.firstEffectiveTime <> Me.firstEffectiveTime Then
                    Me._lastEffectiveTime = prevCode.lastEffectiveTime
                    prevCode._lastEffectiveTime = Me.firstEffectiveTime.AddDays(-1)
                    prevCode.oldCode._lastEffectiveTime = prevCode._lastEffectiveTime

                    'Send intersoftware update
                    If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("CodesDossiers(" & prevCode.noCodification & ")")
                End If
            End If
            Dim nextCode As FolderCode = FolderCodesManager.getInstance.getItemable(Me.noUnique, Me.noUser, Me.lastEffectiveTime)
            If nextCode IsNot Nothing AndAlso nextCode.Equals(Me) = False AndAlso nextCode.noUser = Me.noUser Then
                Me._lastEffectiveTime = nextCode.firstEffectiveTime.AddDays(-1)
            End If

            'Save FolderCode infos
            Try
                DBLinker.getInstance.writeDB("CodificationsDossiers", "NoUser, Recu, Paiement, MsgNoRef, MsgDiagnostic, DateAccidentActif, DateRechuteActif, AutoOpenPaiement, AffPourcentAllTimes, Confirmation,PonderationEval,PonderationPresence, MethodePaiementDefaut,DemandeAuthorisation,NotConfirmRVOnPasteOfDTRP, FirstEffectiveTime, NoUnique, LastEffectiveTime,StartingExternalStatus", IIf(Me.noUser = 0, "null", Me.noUser) & ",'" & Me.askReceipt & "','" & Me.autoSelectBillWhenPaying & "','" & Me.confirmReference & "','" & Me.confirmDiagnostic & "','" & Me.accidentDate & "','" & Me.relaspeDate & "','" & Me.autoShowPayment & "','" & Me.askPourcentage & "'," & Me.confirmation & "," & Me.evaluationPonderation & "," & Me.treatmentPonderation & ",'" & Me.defaultPaymentMethod.Replace("'", "''") & "','" & Me.authorizationProcessActivated & "','" & Me.notConfirmRVOnPasteOfDTRP & "', '" & DateFormat.getTextDate(firstEffectiveTime) & " " & DateFormat.getTextDate(firstEffectiveTime, DateFormat.TextDateOptions.FullTime) & "'," & _noUnique & "," & If(_lastEffectiveTime = LIMIT_DATE, "null", "'" & DateFormat.getTextDate(_lastEffectiveTime) & "'") & "," & _startingExternalStatus, , , , _NoCodification)

            Catch ex As DBLinkerSQLException
                Me._lastEffectiveTime = oldCode._lastEffectiveTime
                With CType(ex.InnerException, SqlClient.SqlException).Errors(0)
                    If .Number = 50000 Then
                        Throw New DBItemableUnsaveble(.Message, ex)
                    Else
                        Throw ex
                    End If
                End With
            Catch ex As Exception
                Me._lastEffectiveTime = oldCode._lastEffectiveTime
                Throw ex
            End Try

            If changeOldCode Then oldCode._lastEffectiveTime = Me.firstEffectiveTime.AddDays(-1)

            If Me.noUnique = 0 Then
                _noUnique = _NoCodification
                DBLinker.getInstance.updateDB("CodesDossiersCodes", "CodeName='" & name.Replace("'", "''") & "'", "NoCodeUnique", _noUnique, False)

                If fttLinks.ContainsKey(0) AndAlso fttLinks(0).ContainsKey(_NoUser) Then
                    Me.folderTexteTypes = getTypes(Of FolderTextType)(FolderTextTypesManager.getInstance(), fttLinks(0)(_NoUser))
                    fttLinks(0).Remove(_NoUser)
                    If fttLinks(0).Count = 0 Then fttLinks.Remove(0)
                ElseIf fttLinks.ContainsKey(oldCode.noUnique) AndAlso fttLinks(oldCode.noUnique).ContainsKey(_NoUser) Then
                    Me.folderTexteTypes = getTypes(Of FolderTextType)(FolderTextTypesManager.getInstance(), fttLinks(oldCode.noUnique)(_NoUser))
                Else
                    If fttLinks.ContainsKey(_noUnique) = False Then fttLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
                    If fttLinks(_noUnique).ContainsKey(_NoUser) = False Then fttLinks(_noUnique).Add(_NoUser, Nothing)

                    fttLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)
                End If
                If fatLinks.ContainsKey(0) AndAlso fatLinks(0).ContainsKey(_NoUser) Then
                    Me.folderAlertTypes = getTypes(Of FolderAlertType)(FolderTextTypesManager.getInstance(), fatLinks(0)(_NoUser))
                    fatLinks(0).Remove(_NoUser)
                    If fatLinks(0).Count = 0 Then fatLinks.Remove(0)
                ElseIf fatLinks.ContainsKey(oldCode.noUnique) AndAlso fatLinks(oldCode.noUnique).ContainsKey(_NoUser) Then
                    Me.folderAlertTypes = getTypes(Of FolderAlertType)(FolderAlertTypesManager.getInstance(), fatLinks(oldCode.noUnique)(_NoUser))
                Else
                    If fatLinks.ContainsKey(_noUnique) = False Then fatLinks.Add(_noUnique, New Generic.Dictionary(Of Integer, Generic.List(Of Integer)))
                    If fatLinks(_noUnique).ContainsKey(_NoUser) = False Then fatLinks(_noUnique).Add(_NoUser, Nothing)

                    fatLinks(_noUnique)(_NoUser) = New Generic.List(Of Integer)
                End If
            End If

            'Save FolderCode periods
            With Me.periods.Rows
                For i As Integer = 0 To .Count - 1
                    DBLinker.getInstance.writeDB("CodesDossiersPeriodes", "NoCodification,IsEval,NoPeriode,Montant,IsDefault,PourcentAbsence,PourcentClient,NoKP", _NoCodification & ",'" & .Item(i)("IsEval") & "'," & .Item(i)("NoPeriode") & "," & .Item(i)("Montant").ToString.Replace(",", ".") & ",'" & .Item(i)("IsDefault") & "'," & .Item(i)("PourcentAbsence").ToString.Replace(",", ".") & "," & .Item(i)("PourcentClient").ToString.Replace(",", ".") & "," & IIf(.Item(i)("NoKP").ToString = "0" OrElse .Item(i)("NoKP").ToString = "", "null", .Item(i)("NoKP")))
                Next i
            End With

            Dim newPeriodsDS As DataSet = DBLinker.getInstance.readDBForGrid("CodesDossiersPeriodes LEFT JOIN KeyPeople ON CodesDossiersPeriodes.NoKP=KeyPeople.NoKP", "NoCDPeriode,NoCodification,IsEval,IsDefault,NoPeriode,Montant,PourcentAbsence,PourcentClient,null AS Button,CASE WHEN CodesDossiersPeriodes.NoKP=0 OR CodesDossiersPeriodes.NoKP IS NULL THEN 'Aucun(e)' ELSE KeyPeople.Nom END AS KPName, CodesDossiersPeriodes.NoKP", , , , "CodePeriodeTable")
            Dim newPeriods As DataTable = DBHelper.copyDataTable(newPeriodsDS, "CodePeriodeTable", "NoCodification=" & noCodification, "IsEval,NoPeriode")
            newPeriods.DefaultView.Sort = "IsEval,NoPeriode"
            newPeriods.AcceptChanges()

            'Update FolderCodeManagers
            Dim prevValue As Boolean = FolderCodesManager.getInstance.autoSaveOnAdd
            FolderCodesManager.getInstance.autoSaveOnAdd = False

            If oldCode IsNot Nothing AndAlso isCodeNew = False Then
                'Reset the old code the one modified
                If oldCode IsNot Nothing Then
                    FolderCodesManager.getInstance.removeItemable(oldCode.noItemable)
                    oldCode.oldCode = oldCode._clone(oldCode.noUser, True)
                    FolderCodesManager.getInstance.addItemable(oldCode)
                End If
            End If

            FolderCodesManager.getInstance.addItemable(Me)
            FolderCodesManager.getInstance.autoSaveOnAdd = prevValue
            Me._Periods = newPeriods
        End Sub

        Private Sub updateData()
            'Save FolderCode infos
            DBLinker.getInstance.updateDB("CodificationsDossiers", "Recu='" & Me.askReceipt & "',Paiement='" & Me.autoSelectBillWhenPaying & "',MsgNoRef='" & Me.confirmReference & "',MsgDiagnostic='" & Me.confirmDiagnostic & "',DateAccidentActif='" & Me.accidentDate & "',DateRechuteActif='" & Me.relaspeDate & "',AutoOpenPaiement='" & Me.autoShowPayment & "', AffPourcentAllTimes='" & Me.askPourcentage & "',Confirmation=" & Me.confirmation & ",PonderationEval=" & Me.evaluationPonderation & ",PonderationPresence=" & Me.treatmentPonderation & ",MethodePaiementDefaut='" & Me.defaultPaymentMethod.Replace("'", "''") & "',DemandeAuthorisation='" & Me.authorizationProcessActivated & "',NotConfirmRVOnPasteOfDTRP='" & Me.notConfirmRVOnPasteOfDTRP & "', firstEffectiveTime='" & DateFormat.getTextDate(Me.firstEffectiveTime) & " " & DateFormat.getTextDate(Me.firstEffectiveTime, DateFormat.TextDateOptions.FullTime) & "',StartingExternalStatus=" & _startingExternalStatus, "NoCodification", Me.noCodification, False)

            'Delete removed Periods
            Dim nosCodePeriodes As String = ""
            With Me.periods.Rows
                For i As Integer = 0 To .Count - 1
                    If .Item(i)("NoCDPeriode") IsNot DBNull.Value Then nosCodePeriodes &= "," & .Item(i)("NoCDPeriode")
                Next i
            End With
            If nosCodePeriodes <> "" Then
                nosCodePeriodes = nosCodePeriodes.Substring(1)
                DBLinker.getInstance.delDB("CodesDossiersPeriodes", "NoCDPeriode", "(" & nosCodePeriodes & ") AND NoCodification=" & Me.noCodification, False, , , , , " NOT IN ")
            End If

            'Save new/current periods
            With Me.periods.Rows
                For i As Integer = 0 To .Count - 1
                    If .Item(i)("NoCDPeriode") Is DBNull.Value Then
                        DBLinker.getInstance.writeDB("CodesDossiersPeriodes", "NoCodification,IsEval,NoPeriode,Montant,IsDefault,PourcentAbsence,PourcentClient,NoKP", Me.noCodification & ",'" & .Item(i)("IsEval") & "'," & .Item(i)("NoPeriode") & "," & .Item(i)("Montant").ToString.Replace(",", ".") & ",'" & .Item(i)("IsDefault") & "'," & .Item(i)("PourcentAbsence").ToString.Replace(",", ".") & "," & .Item(i)("PourcentClient").ToString.Replace(",", ".") & "," & IIf(.Item(i)("NoKP").ToString = "0" OrElse .Item(i)("NoKP").ToString = "", "null", .Item(i)("NoKP")))
                    Else
                        Dim noCDPeriode As Integer = .Item(i)("noCDPeriode")
                        DBLinker.getInstance.updateDB("CodesDossiersPeriodes", "IsEval='" & .Item(i)("IsEval") & "',NoPeriode=" & .Item(i)("NoPeriode") & ",Montant=" & .Item(i)("Montant").ToString.Replace(",", ".") & ",IsDefault='" & .Item(i)("IsDefault") & "',PourcentAbsence=" & .Item(i)("PourcentAbsence").ToString.Replace(",", ".") & ",PourcentClient=" & .Item(i)("PourcentClient").ToString.Replace(",", ".") & ",NoKP=" & IIf(.Item(i)("NoKP").ToString = "0" OrElse .Item(i)("NoKP").ToString = "", "null", .Item(i)("NoKP")), "NoCDPeriode", noCDPeriode, False)
                    End If
                Next i
            End With

            'Update previous code with same NoUnique and NoUser
            If oldCode.firstEffectiveTime.Date <> firstEffectiveTime.Date Then
                Dim prevCode As FolderCode = FolderCodesManager.getInstance.getItemable(noUnique, noUser, oldCode.firstEffectiveTime.AddDays(-1))
                If prevCode IsNot Nothing AndAlso prevCode.Equals(Me) = False Then
                    prevCode._lastEffectiveTime = firstEffectiveTime.AddDays(-1)
                    prevCode.oldCode._lastEffectiveTime = prevCode._lastEffectiveTime

                    'Send intersoftware update
                    If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("CodesDossiers(" & prevCode.noCodification & ")")
                End If
            End If
        End Sub

        Public Overrides Sub saveData()
            _saveData(False)
        End Sub

        Public Function do_NotShared_HaveChanged(Optional ByVal includeEffectiveness As Boolean = True) As Boolean
            Return oldCode Is Nothing OrElse _name <> oldCode._name OrElse _
                    _NoUser <> oldCode._NoUser OrElse _
                    _Periods.GetChanges() IsNot Nothing OrElse _
                    _askReceipt <> oldCode._askReceipt OrElse _
                    _autoSelectBillWhenPaying <> oldCode._autoSelectBillWhenPaying OrElse _
                    _confirmReference <> oldCode._confirmReference OrElse _
                    _confirmDiagnostic <> oldCode._confirmDiagnostic OrElse _
                    _accidentDate <> oldCode._accidentDate OrElse _
                    _relaspeDate <> oldCode._relaspeDate OrElse _
                    _autoShowPayment <> oldCode._autoShowPayment OrElse _
                    _NoCodification <> oldCode._NoCodification OrElse _
                    _askPourcentage <> oldCode._askPourcentage OrElse _
                    _Confirmation <> oldCode._Confirmation OrElse _
                    _evaluationPonderation <> oldCode._evaluationPonderation OrElse _
                    _treatmentPonderation <> oldCode._treatmentPonderation OrElse _
                    _DefaultPaymentMethod <> oldCode._DefaultPaymentMethod OrElse _
                    _authorizationProcessActivated <> oldCode._authorizationProcessActivated OrElse _
                    _NotConfirmRVOnPasteOfDTRP <> oldCode._NotConfirmRVOnPasteOfDTRP OrElse _
                    _noUnique <> oldCode._noUnique OrElse _
                    _firstEffectiveTime <> oldCode._firstEffectiveTime OrElse _
                    _lastEffectiveTime <> oldCode._lastEffectiveTime OrElse _
                    _startingExternalStatus <> oldCode._startingExternalStatus
        End Function

        Public Sub revertChanges()
            _name = oldCode._name
            _NoUser = oldCode._NoUser
            _Periods.RejectChanges()
            _askReceipt = oldCode._askReceipt
            _autoSelectBillWhenPaying = oldCode._autoSelectBillWhenPaying
            _confirmReference = oldCode._confirmReference
            _confirmDiagnostic = oldCode._confirmDiagnostic
            _accidentDate = oldCode._accidentDate
            _relaspeDate = oldCode._relaspeDate
            _autoShowPayment = oldCode._autoShowPayment
            _NoCodification = oldCode._NoCodification
            _askPourcentage = oldCode._askPourcentage
            _Confirmation = oldCode._Confirmation
            _evaluationPonderation = oldCode._evaluationPonderation
            _treatmentPonderation = oldCode._treatmentPonderation
            _DefaultPaymentMethod = oldCode._DefaultPaymentMethod
            _authorizationProcessActivated = oldCode._authorizationProcessActivated
            _NotConfirmRVOnPasteOfDTRP = oldCode._NotConfirmRVOnPasteOfDTRP
            _noUnique = oldCode._noUnique
            _firstEffectiveTime = oldCode._firstEffectiveTime
            _lastEffectiveTime = oldCode._lastEffectiveTime
            _startingExternalStatus = oldCode._startingExternalStatus
        End Sub

        Private Sub _saveData(ByVal resetNoUnique As Boolean)
            Dim isDifferentUser As Boolean = oldCode Is Nothing OrElse oldCode.noUser <> Me.noUser
            Dim isNewCode As Boolean = Me._NoCodification < 1
            Dim isPastCodeChanged As Boolean = (do_NotShared_HaveChanged(False) AndAlso date1Infdate2(oldCode.firstEffectiveTime, Date.Today, True))
            Dim hasToUpdate As Boolean = do_NotShared_HaveChanged()
            Me.periods.AcceptChanges()

            If resetNoUnique OrElse isDifferentUser OrElse isNewCode OrElse isPastCodeChanged Then
                If resetNoUnique Then Me._noUnique = 0
                writeData()
            ElseIf hasToUpdate Then
                updateData()
            End If

            'Save FTT links
            DBLinker.getInstance.delDB("CodesDossiersFolderTexteTypes", "NoUnique", noUnique & " AND NoUser = " & If(_NoUser = 0, "null", _NoUser.ToString()), False)
            For i As Integer = 0 To fttLinks(_noUnique)(_NoUser).Count - 1
                DBLinker.getInstance.writeDB("CodesDossiersFolderTexteTypes", "NoUnique,NoUser,NoFolderTexteType", noUnique & "," & If(noUser = 0, "null", noUser.ToString()) & "," & fttLinks(_noUnique)(_NoUser)(i))
            Next i

            'Save FAT links
            DBLinker.getInstance.delDB("CodesDossiersFolderAlertTypes", "NoUnique", noUnique & " AND NoUser = " & If(_NoUser = 0, "null", _NoUser.ToString()), False)
            For i As Integer = 0 To fatLinks(_noUnique)(_NoUser).Count - 1
                DBLinker.getInstance.writeDB("CodesDossiersFolderAlertTypes", "NoUnique,NoUser,NoFolderAlertType", noUnique & "," & If(noUser = 0, "null", noUser.ToString()) & "," & fatLinks(_noUnique)(_NoUser)(i))
            Next i

            'Send intersoftware update
            If autoSendUpdateOnSave Then
                InternalUpdatesManager.getInstance.sendUpdate("CodesDossiers(" & Me.noCodification & ")")
                If oldCode.noCodification <> Me.noCodification Then InternalUpdatesManager.getInstance.sendUpdate("CodesDossiers(" & oldCode.noCodification & ")")
            End If

            oldCode = Me._clone(Me.noUser, True)
        End Sub

        Public Overloads Sub saveData(ByVal resetNoUnique As Boolean)
            Me._saveData(resetNoUnique)
        End Sub

        Public Overrides ReadOnly Property noItemable() As Integer
            Get
                Return Me.noCodification
            End Get
        End Property

        Public Function compareTo(ByVal other As FolderCode) As Integer Implements System.IComparable(Of FolderCode).CompareTo
            Return other.noUser.CompareTo(Me.noUser) * 10 + other.noUnique.CompareTo(Me.noUnique) * 10 + other.firstEffectiveTime.CompareTo(Me.firstEffectiveTime)
        End Function
    End Class

End Namespace
