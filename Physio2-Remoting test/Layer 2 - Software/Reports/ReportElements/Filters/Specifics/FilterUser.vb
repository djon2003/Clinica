Public Class FilterUser
    Inherits BasicFilter
    Private _All As Boolean = False
    Private _AskTRPTraitantOrVisite As Boolean = False
    Private _AlternateTableDotField As String = ""
    Private _AskTRPTraitantOrVisiteOrDemanded As Boolean = False
    Private _ThirdTableDotField As String = ""
    Private _CurrentReturn As UserSelectorReturn
    Private _IsTRP As Boolean = False
    Private _EmployeeType As Integer = 0


#Region "Properties"
    Public Property employeeType() As Integer
        Get
            Return _EmployeeType
        End Get
        Set(ByVal value As Integer)
            _EmployeeType = value
        End Set
    End Property

    Public Property all() As Boolean
        Get
            Return _All
        End Get
        Set(ByVal value As Boolean)
            _All = value
        End Set
    End Property

    Public Property isTRP() As Boolean
        Get
            Return _IsTRP
        End Get
        Set(ByVal value As Boolean)
            _IsTRP = value
        End Set
    End Property

    Public Property askTRPTraitantOrVisiteOrDemanded() As Boolean
        Get
            Return _AskTRPTraitantOrVisiteOrDemanded
        End Get
        Set(ByVal value As Boolean)
            _AskTRPTraitantOrVisiteOrDemanded = value
        End Set
    End Property

    Public Property thirdTableDotField() As String
        Get
            Return _ThirdTableDotField
        End Get
        Set(ByVal value As String)
            _ThirdTableDotField = value
        End Set
    End Property
    Public Property askTRPTraitantOrVisite() As Boolean
        Get
            Return _AskTRPTraitantOrVisite
        End Get
        Set(ByVal value As Boolean)
            _AskTRPTraitantOrVisite = value
        End Set
    End Property

    Public Property alternateTableDotField() As String
        Get
            Return _AlternateTableDotField
        End Get
        Set(ByVal value As String)
            _AlternateTableDotField = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As UserSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        If Not TypeOf filtered Is FilteringUser Then Return Nothing

        With CType(filtered, FilteringUser)
            _CurrentReturn = .currentReturn
            Dim tdf As String = Me.tableDotField
            If _AskTRPTraitantOrVisite Then
                Dim myMessageBox As New Clinica.MsgBox1
                If myMessageBox("S'agit-il du thérapeute traitant (celui associé au dossier) ou du thérapeute de la visite (réel) ?", "Choix du thérapeute", 2, "Dossier", "Réel", , False) = 2 Then tdf = _AlternateTableDotField
            ElseIf _AskTRPTraitantOrVisiteOrDemanded Then
                Dim myMessageBox As New Clinica.MsgBox1
                Dim selected As Byte = myMessageBox("S'agit-il du thérapeute traitant (celui associé au dossier), du thérapeute de la visite (réel) ou du thérapeute demandé ?", "Choix du thérapeute", 3, "Dossier", "Réel", "Demandé", False)
                If selected = 2 Then tdf = _AlternateTableDotField
                If selected = 3 Then tdf = _ThirdTableDotField
            End If
            If isTRP Then
                _CurrentReturn.filtrageTexte = "<tr><td>Thérapeute</td><td> : </td><td>" & .currentReturn.user & "</td></tr>"
            Else
                _CurrentReturn.filtrageTexte = "<tr><td>Utilisateur</td><td> : </td><td>" & .currentReturn.user & "</td></tr>"
            End If
            _CurrentReturn.whereStr = "(" & tdf & "=" & _CurrentReturn.noUser & ")"
            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Dim userFiltrage As String = ""
        Dim canceling As Boolean = False
        Dim myUser As User = UsersManager.getInstance.chooseUser(all, isTRP, employeeType)
        If myUser Is Nothing Then canceling = True

        If canceling = False AndAlso myUser.noUser <> 0 Then
            Dim tdf As String = Me.tableDotField
            Dim trpType As String = ""
            If _AskTRPTraitantOrVisite Then
                trpType = " traitant"
                Dim myMessageBox As New Clinica.MsgBox1
                If myMessageBox("S'agit-il du thérapeute traitant (celui associé au dossier) ou du thérapeute de la visite (réel) ?", "Choix du thérapeute", 2, "Dossier", "Réel", , False) = 2 Then
                    tdf = _AlternateTableDotField
                    trpType = " réel"
                End If
            ElseIf _AskTRPTraitantOrVisiteOrDemanded Then
                trpType = " traitant"
                Dim myMessageBox As New Clinica.MsgBox1
                Dim selected As Byte = myMessageBox("S'agit-il du thérapeute traitant (celui associé au dossier), du thérapeute de la visite (réel) ou du thérapeute demandé ?", "Choix du thérapeute", 3, "Dossier", "Réel", "Demandé", False)
                If selected = 2 Then
                    tdf = _AlternateTableDotField
                    trpType = " réel"
                End If
                If selected = 3 Then
                    tdf = _ThirdTableDotField
                    trpType = " demandé"
                End If
            End If
            If isTRP Then
                userFiltrage = "<tr><td>Thérapeute" & trpType & "</td><td> : </td><td>" & myUser.toString & "</td></tr>"
            Else
                userFiltrage = "<tr><td>Utilisateur</td><td> : </td><td>" & myUser.toString & "</td></tr>"
            End If

            _CurrentReturn = New UserSelectorReturn()
            _CurrentReturn.canceling = canceling
            _CurrentReturn.filtrageTexte = userFiltrage
            _CurrentReturn.user = myUser.toString
            _CurrentReturn.noUser = myUser.noUser
            _CurrentReturn.whereStr = "(" & tdf & "=" & myUser.noUser & ")"
            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, canceling)
        End If

        _CurrentReturn = New UserSelectorReturn()
        _CurrentReturn.canceling = canceling
        If isTRP Then
            _CurrentReturn.filtrageTexte = "<tr><td>Thérapeute</td><td> : </td><td>Tous</td></tr>"
        Else
            _CurrentReturn.filtrageTexte = "<tr><td>Utilisateur</td><td> : </td><td>Tous</td></tr>"
        End If
        _CurrentReturn.user = ""
        _CurrentReturn.noUser = 0
        _CurrentReturn.whereStr = ""
        Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, _CurrentReturn.canceling)
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "All"
                    all = myKey.Value
                Case "AskTRPTraitantOrVisite"
                    _AskTRPTraitantOrVisite = myKey.Value
                Case "AlternateTableDotField"
                    _AlternateTableDotField = myKey.Value
                Case "AskTRPTraitantOrVisiteOrDemanded"
                    _AskTRPTraitantOrVisiteOrDemanded = myKey.Value
                Case "ThirdTableDotField"
                    _ThirdTableDotField = myKey.Value
                Case "IsTRP"
                    _IsTRP = myKey.Value
                Case "EmployeeType"
                    _EmployeeType = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
