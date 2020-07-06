Public Class FilterListFieldData
    Inherits BasicFilter

    Private _All, _StartsWith, _EndsWith, _AcceptNulls As Boolean
    Private _ListTDF As String = ""
    Private _ValueTDF As String = ""
    Private _OneEntryName As String = ""
    Private _CurrentReturn As PersoSelectorReturn
    Private _WinTitle As String = "Veuillez faire un choix"

#Region "Properties"
    Public Property listTDF() As String
        Get
            Return _ListTDF
        End Get
        Set(ByVal value As String)
            _ListTDF = value
        End Set
    End Property

    Public Property valueTDF() As String
        Get
            Return _ValueTDF
        End Get
        Set(ByVal value As String)
            _ValueTDF = value
        End Set
    End Property

    Public Property oneEntryName() As String
        Get
            Return _OneEntryName
        End Get
        Set(ByVal value As String)
            _OneEntryName = value
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

    Public Property acceptNulls() As Boolean
        Get
            Return _AcceptNulls
        End Get
        Set(ByVal value As Boolean)
            _AcceptNulls = value
        End Set
    End Property

    Public Property winTitle() As String
        Get
            Return _WinTitle
        End Get
        Set(ByVal value As String)
            _WinTitle = value
        End Set
    End Property

    Public Property startsWith() As Boolean
        Get
            Return _StartsWith
        End Get
        Set(ByVal value As Boolean)
            _StartsWith = value
        End Set
    End Property

    Public Property endsWith() As Boolean
        Get
            Return _EndsWith
        End Get
        Set(ByVal value As Boolean)
            _EndsWith = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As PersoSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Private Function choosePerso(ByVal selectPersoField As String, ByVal wherePersoField As String, Optional ByVal choosePersoAll As Boolean = False, Optional ByVal startingWith As Boolean = False) As PersoSelectorReturn
        Dim myReturn As New PersoSelectorReturn
        Dim monChoix As String = ""
        myReturn.whereStr = ""
        myReturn.filtrageTexte = ""
        myReturn.persoChoice = ""

        Dim tableName As String = ""
        Dim fieldName As String = ""
        If selectPersoField.IndexOf(".") <> -1 Then
            tableName = selectPersoField.Substring(0, selectPersoField.LastIndexOf("."))
            fieldName = selectPersoField.Substring(selectPersoField.LastIndexOf(".") + 1)
        End If

        Dim choices() As String = DBLinker.getInstance.readOneDBField(tableName, fieldName)
        Dim values() As String = Nothing
        If valueTDF <> "" Then values = DBLinker.getInstance.readOneDBField(valueTDF.Split(New Char() {"."})(0), valueTDF)

        Dim mesChoix As String = String.Join("§", choices)
        If choosePersoAll Then mesChoix &= "§* Tous / toutes *" : monChoix = "* Tous / toutes *"

        Dim myMultiChoice As New multichoice
        monChoix = myMultiChoice.GetChoice(_WinTitle, mesChoix, , "§", , monChoix, True)

        If monChoix.ToUpper.StartsWith("ERROR") Then myReturn.canceling = True : Return myReturn

        myReturn.persoChoice = monChoix
        If monChoix <> "* Tous / toutes *" Then
            myReturn.filtrageTexte = "<tr><td>" & _OneEntryName & " filtré par</td><td> : </td><td>" & monChoix & "</td></tr>"
            Dim whereChoix As String = monChoix.Replace("'", "''")
            If valueTDF <> "" AndAlso values IsNot Nothing Then
                Dim indexOfChoix As Integer = Array.IndexOf(choices, monChoix)
                If indexOfChoix <= values.GetUpperBound(0) Then whereChoix = values(indexOfChoix).Replace("'", "''")
            End If

            If startingWith And endsWith = False Then
                myReturn.whereStr = wherePersoField & " LIKE '" & whereChoix & "%'"
            ElseIf startingWith = False And endsWith Then
                myReturn.whereStr = wherePersoField & " LIKE '%" & whereChoix & "'"
            ElseIf startingWith And endsWith Then
                myReturn.whereStr = wherePersoField & " LIKE '%" & whereChoix & "%'"
            Else
                myReturn.whereStr = wherePersoField & "='" & whereChoix & "'"
            End If

            If acceptNulls = True And myReturn.whereStr <> "" Then myReturn.whereStr = "(" & myReturn.whereStr & " OR " & wherePersoField & " IS NULL)"
        Else
            myReturn.filtrageTexte = "<tr><td>" & _OneEntryName & "</td><td> : </td><td>Tous/Toutes</td></tr>"
        End If

        Return myReturn
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        If Not TypeOf filtered Is FilteringListFieldData Then Return Nothing

        With CType(filtered, FilteringListFieldData)
            _CurrentReturn = .currentReturn
            _CurrentReturn.filtrageTexte = "<tr><td>Filtré par</td><td> : </td><td>" & _CurrentReturn.persoChoice & "</td></tr>"
            If startsWith And endsWith = False Then
                _CurrentReturn.whereStr = tableDotField & " LIKE '" & _CurrentReturn.persoChoice.Replace("'", "''") & "%'"
            ElseIf startsWith = False And endsWith Then
                _CurrentReturn.whereStr = tableDotField & " LIKE '%" & _CurrentReturn.persoChoice.Replace("'", "''") & "'"
            ElseIf startsWith And endsWith Then
                _CurrentReturn.whereStr = tableDotField & " LIKE '%" & _CurrentReturn.persoChoice.Replace("'", "''") & "%'"
            Else
                _CurrentReturn.whereStr = tableDotField & "='" & _CurrentReturn.persoChoice.Replace("'", "''") & "'"
            End If
            If acceptNulls = True And _CurrentReturn.whereStr <> "" Then _CurrentReturn.whereStr = "(" & _CurrentReturn.whereStr & " OR " & Me.tableDotField & " IS NULL)"
            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Dim myPerso As PersoSelectorReturn = Me.choosePerso(Me.listTDF, Me.tableDotField, Me.all, Me.startsWith)
        _CurrentReturn = myPerso
        Return New FilterResult(myPerso, Me.filterOnReportParts, myPerso.canceling)
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "All"
                    all = myKey.Value
                Case "AcceptNulls"
                    acceptNulls = myKey.Value
                Case "ListTDF"
                    listTDF = myKey.Value
                Case "StartsWith"
                    startsWith = myKey.Value
                Case "EndsWith"
                    endsWith = myKey.Value
                Case "WinTitle"
                    winTitle = myKey.Value
                Case "OneEntryName"
                    _OneEntryName = myKey.Value
                Case "ValueTDF"
                    valueTDF = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
