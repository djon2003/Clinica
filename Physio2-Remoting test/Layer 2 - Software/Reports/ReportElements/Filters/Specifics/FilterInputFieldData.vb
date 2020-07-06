Public Class FilterInputFieldData
    Inherits BasicFilter

    Private _StartsWith, _EndsWith, _All, _OnlyAlphabet, _RefuseAccents, _CurrencyBox, _cb_AcceptNegative As Boolean
    Private _AcceptAlpha As Boolean = True
    Private _AcceptNumeric As Boolean = True
    Private _ShowFilterOnReport As Boolean = True
    Private _NbDecimals As Integer
    Private _OneEntryName As String = ""
    Private _PromptText As String = "Veuillez entrer votre choix"
    Protected _CurrentReturn As PersoSelectorReturn
    Private _WinTitle As String = "Sélection d'un choix"

#Region "Properties"
    Public Property oneEntryName() As String
        Get
            Return _OneEntryName
        End Get
        Set(ByVal value As String)
            _OneEntryName = value
        End Set
    End Property

    Public Property showFilterOnReport() As Boolean
        Get
            Return _ShowFilterOnReport
        End Get
        Set(ByVal value As Boolean)
            _ShowFilterOnReport = value
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

    Public Property promptText() As String
        Get
            Return _PromptText
        End Get
        Set(ByVal value As String)
            _PromptText = value
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

    Public ReadOnly Property currentReturn() As PersoSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property

    Public Property all() As Boolean
        Get
            Return _All
        End Get
        Set(ByVal value As Boolean)
            _All = value
        End Set
    End Property

    Public Property currencyBox() As Boolean
        Get
            Return _CurrencyBox
        End Get
        Set(ByVal value As Boolean)
            _CurrencyBox = value
        End Set
    End Property

    Public Property nbDecimals() As Boolean
        Get
            Return _NbDecimals
        End Get
        Set(ByVal value As Boolean)
            _NbDecimals = value
        End Set
    End Property

    Public Property cb_AcceptNegative() As Boolean
        Get
            Return _cb_AcceptNegative
        End Get
        Set(ByVal value As Boolean)
            _cb_AcceptNegative = value
        End Set
    End Property

    Public Property onlyAlphabet() As Boolean
        Get
            Return _OnlyAlphabet
        End Get
        Set(ByVal value As Boolean)
            _OnlyAlphabet = value
        End Set
    End Property

    Public Property refuseAccents() As Boolean
        Get
            Return _RefuseAccents
        End Get
        Set(ByVal value As Boolean)
            _RefuseAccents = value
        End Set
    End Property
    Public Property acceptNumeric() As Boolean
        Get
            Return _AcceptNumeric
        End Get
        Set(ByVal value As Boolean)
            _AcceptNumeric = value
        End Set
    End Property
    Public Property acceptAlpha() As Boolean
        Get
            Return _AcceptAlpha
        End Get
        Set(ByVal value As Boolean)
            _AcceptAlpha = value
        End Set
    End Property
#End Region

    Protected Sub choosePerso()
        Dim myReturn As New PersoSelectorReturn
        myReturn.whereStr = ""
        myReturn.filtrageTexte = ""
        myReturn.persoChoice = ""

        Dim myInputBoxPlus As New InputBoxPlus
        myInputBoxPlus.acceptAlpha = acceptAlpha
        myInputBoxPlus.acceptNumeric = acceptNumeric
        myInputBoxPlus.cb_AcceptNegative = cb_AcceptNegative
        myInputBoxPlus.onlyAlphabet = onlyAlphabet
        myInputBoxPlus.currencyBox = currencyBox
        myInputBoxPlus.nbDecimals = nbDecimals
        myInputBoxPlus.refuseAccents = refuseAccents

        Dim myChoice As String = myInputBoxPlus.Prompt(promptText, winTitle)
        If myChoice = "" And all = False Then
            myReturn.canceling = True
            _CurrentReturn = myReturn
            Exit Sub
        End If

        myReturn.persoChoice = myChoice
        If myChoice <> "" Then
            If _ShowFilterOnReport Then myReturn.filtrageTexte = "<tr><td>Filtré par</td><td> : </td><td>" & myChoice & "</td></tr>"
            Dim startsWithStr As String = ""
            Dim endsWithStr As String = ""
            Dim whereOperator As String = "="
            If startsWith Then startsWithStr = "%" : whereOperator = " LIKE "
            If endsWith Then endsWithStr = "%" : whereOperator = " LIKE "
            myReturn.whereStr = tableDotField & whereOperator & "'" & endsWithStr & myChoice.Replace("'", "''") & startsWithStr & "'"
        End If
        If _ShowFilterOnReport And myReturn.filtrageTexte = "" Then myReturn.filtrageTexte = "<tr><td>" & _OneEntryName & "</td><td> : </td><td>Tous/Toutes</td></tr>"

        _CurrentReturn = myReturn
    End Sub

    Protected Overrides Function internalFilter() As FilterResult
        choosePerso()
        Return New FilterResult(currentReturn, Me.filterOnReportParts, currentReturn.canceling)
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        With CType(filtered, FilteringInputFieldData)
            _CurrentReturn = .currentReturn
            If _CurrentReturn.filtrageTexte = "" And showFilterOnReport Then _CurrentReturn.filtrageTexte = "<tr><td>Filtré par</td><td> : </td><td>" & _CurrentReturn.persoChoice & "</td></tr>"
            Dim startsWithStr As String = ""
            Dim endsWithStr As String = ""
            If startsWith Then startsWithStr = "%"
            If endsWith Then endsWithStr = "%"
            If _CurrentReturn.whereStr = "" Then _CurrentReturn.whereStr = tableDotField & "='" & endsWithStr & _CurrentReturn.persoChoice.Replace("'", "''") & startsWithStr & "'"

            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "All"
                    all = myKey.Value
                Case "AcceptAlpha"
                    acceptAlpha = myKey.Value
                Case "AcceptNumeric"
                    acceptNumeric = myKey.Value
                Case "OnlyAlphabet"
                    onlyAlphabet = myKey.Value
                Case "RefuseAccents"
                    refuseAccents = myKey.Value
                Case "NbDecimals"
                    nbDecimals = myKey.Value
                Case "CurrencyBox"
                    currencyBox = myKey.Value
                Case "CB_AcceptNegative"
                    cb_AcceptNegative = myKey.Value
                Case "PromptText"
                    promptText = myKey.Value
                Case "WinTitle"
                    winTitle = myKey.Value
                Case "ShowFilterOnRapport"
                    showFilterOnReport = myKey.Value
                Case "OneEntryName"
                    oneEntryName = myKey.Value
                Case "StartsWith"
                    startsWith = myKey.Value
                Case "EndsWith"
                    endsWith = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
